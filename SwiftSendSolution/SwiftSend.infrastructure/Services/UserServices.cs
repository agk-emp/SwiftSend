using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.app.Abstracts.Services;
using SwiftSend.app.Dtos.UserDtos.Inputs;
using SwiftSend.app.Dtos.UserDtos.Outputs;
using SwiftSend.data.Entities.Identity;
using SwiftSend.infrastructure.Context;
using SwiftSend.infrastructure.Options.JwtOpts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SwiftSend.infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserServices(IOptions<JwtOptions> jwtOptions,
            TokenValidationParameters tokenValidationParameters,
            AppDbContext appDbContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _jwtOptions = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<RefreshTokenResultDto> RefreshTokenForUser(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            CheckJwtTokenForRefreshing(refreshTokenRequestDto);
            AppUser? user = await CheckRefreshToken(refreshTokenRequestDto);

            return await RetuenJWTResult(user);
        }

        public async Task<RefreshTokenResultDto> Register(RegisterDto registerDto)
        {
            var userByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            var userByUserName = await _userManager.FindByNameAsync(registerDto.Username);

            if (await FindUserByUsernameOrEmail(registerDto.Username, registerDto.Email) is not null)
            {
                throw new Exception("User already exists"); //To do customize it and adding middleware to handle errors globally
            }

            var user = new AppUser() //to do add automapper
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Something went wrong");
            }

            return await RetuenJWTResult(user);
        }

        public async Task<RefreshTokenResultDto> Login(LoginDto loginDto)
        {
            var user = await FindUserByUsernameOrEmail(loginDto.EmailOrUsername,
                loginDto.EmailOrUsername);
            if (user is null)
            {
                throw new Exception("There is no such user");
            }

            var passwordMatching = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordMatching)
            {
                throw new Exception("User entries are invalid");
            }

            await RevokeAlreadyExistingTokensForUser(user.Id);

            return await RetuenJWTResult(user);
        }

        #region Private methods

        private string GenerateRefreshTokenCode()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAccessToken(AppUser user)
        {
            var jwtToken = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: await GetUserClaims(user),
                    expires: DateTime.UtcNow.AddSeconds(_jwtOptions.AccessTokenExpiration),
                    signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

        private async Task<List<Claim>> GetUserClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"userId"),
                new Claim(ClaimTypes.Email,"email"),
                new Claim(ClaimTypes.Name,"userName")
            };
            await AddRolesToClaims(user, claims);

            return claims;
        }

        private async Task AddRolesToClaims(AppUser user, List<Claim> claims)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(role + "Role", role));
                await AddRoleClaimsToClaims(claims, role);
            }
        }

        private async Task AddRoleClaimsToClaims(List<Claim> claims, string role)
        {
            var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
            foreach (var claim in roleClaims)
            {
                claims.Add(new Claim(claim.Type + "Claim", claim.Value));
            }
        }

        private bool ValidateAccessToken(string accessToken, JwtSecurityToken jwtToken)
        {
            if (string.IsNullOrEmpty(accessToken) || jwtToken is null) return false;

            if (jwtToken.Header.Alg is not SecurityAlgorithms.HmacSha256)
            {
                return false;
            }

            try
            {
                var result = new JwtSecurityTokenHandler().ValidateToken(accessToken,
                        _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private JwtSecurityToken ReadAccessToken(string accessToken)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            return jwtToken;
        }

        private async Task<AppUser> FindUserByUsernameOrEmail(string username, string email)
        {
            var userByUsername = await _userManager.FindByNameAsync(username);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail is null && userByUsername is null)
            {
                return null;
            }
            return userByEmail ?? userByUsername;
        }

        private async Task<RefreshTokenResultDto> RetuenJWTResult(AppUser user)
        {
            var tokens = new RefreshTokenResultDto()
            {
                JWTToken = await GenerateAccessToken(user),
                RefreshToken = GenerateRefreshTokenCode()
            };

            var userRefreshToken = new UserRefreshToken()
            {
                Expiration = DateTime.UtcNow.AddSeconds(_jwtOptions.RefreshTokenExpiration),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id,
                JWTToken = tokens.JWTToken,
                RefreshToken = tokens.RefreshToken,
            };

            await _userRefreshTokenRepository.Add(userRefreshToken);
            return tokens;
        }

        private void CheckJwtTokenForRefreshing(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var jwtToken = ReadAccessToken(refreshTokenRequestDto.JWTToken);

            if (!ValidateAccessToken(refreshTokenRequestDto.JWTToken, jwtToken))
            {
                throw new Exception("Not a valid token");
            }


            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new Exception("No need to refresh");
            }
        }

        private async Task<AppUser?> CheckRefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {

            if (string.IsNullOrWhiteSpace(refreshTokenRequestDto.RefreshToken))
            {
                throw new Exception("Tokens are missing");
            }

            var refToken = await _userRefreshTokenRepository.GetByAccessTokenAndRefreshToken(refreshTokenRequestDto.JWTToken,
                refreshTokenRequestDto.RefreshToken);

            if (refToken is null)
            {
                throw new Exception("Tokens are missing");
            }

            if (refToken.IsRevoked == true)
            {
                throw new Exception("The token was revoked");
            }

            if (refToken.IsUsed == true)
            {
                throw new Exception("The token was used");
            }

            if (refToken.Expiration < DateTime.UtcNow)
            {
                throw new Exception("The refresh token already expired");
            }

            var userId = refToken.UserId;

            var user = await _userManager.FindByIdAsync(refToken.UserId.ToString());

            if (user is null)
            {
                throw new Exception("There is no such user");
            }

            refToken.IsUsed = true;
            await _userRefreshTokenRepository.Update(refToken.Id, refToken);
            await RevokeAlreadyExistingTokensForUser(refToken.UserId);
            return user;
        }

        private async Task RevokeAlreadyExistingTokensForUser(string userId)
        {
            var tokenToBeRevoked = await _userRefreshTokenRepository.GetAllUserRefreshTokens(userId);
            foreach (var token in tokenToBeRevoked)
            {
                token.IsRevoked = true;
                await _userRefreshTokenRepository.Update(token.Id, token);
            }
        }
        #endregion
    }
}