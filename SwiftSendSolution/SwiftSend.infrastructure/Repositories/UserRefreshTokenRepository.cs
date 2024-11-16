using MongoDB.Driver;
using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.data.Entities.Identity;
using SwiftSend.infrastructure.Context;

namespace SwiftSend.infrastructure.Repositories
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public UserRefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(UserRefreshToken userRefreshToken)
        {
            await _context.UserRefreshToken.InsertOneAsync(userRefreshToken);
        }

        public async Task<List<UserRefreshToken>> GetAllUserRefreshTokens(string userId)
        {
            var result = await _context.UserRefreshToken.Find(userRefreshTokens =>
            userRefreshTokens.UserId == userId).ToListAsync();
            return result;
        }

        public async Task<UserRefreshToken> GetByAccessTokenAndRefreshToken(string accessToken, string refreshToken)
        {
            var result = await _context.UserRefreshToken.Find(userRefreshTokens =>
            userRefreshTokens.RefreshToken == refreshToken && userRefreshTokens.JWTToken == accessToken)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task Update(string id, UserRefreshToken userRefreshToken)
        {
            var filter = Builders<UserRefreshToken>.Filter.Eq(refTok => refTok.Id, id);
            await _context.UserRefreshToken.ReplaceOneAsync(filter, userRefreshToken);
        }
    }
}
