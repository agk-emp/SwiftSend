using SwiftSend.data.Entities.Identity;

namespace SwiftSend.app.Abstracts.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        Task Update(string id, UserRefreshToken userRefreshToken);
        Task Add(UserRefreshToken userRefreshToken);
        Task<UserRefreshToken> GetByAccessTokenAndRefreshToken(string accessToken, string refreshToken);
        Task<List<UserRefreshToken>> GetAllUserRefreshTokens(string userId);
    }
}
