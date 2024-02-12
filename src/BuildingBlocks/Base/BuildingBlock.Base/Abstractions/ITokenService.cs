using BuildingBlock.Base.Models;

namespace BuildingBlock.Base.Abstractions
{
    public interface ITokenService
    {
        string GetEmailWithToken(string token);
        bool TokenIsValid(string token);
        Task<UserModel> GetUserWithTokenAsync(string token);
        string GetTokenInHeader();
    }

    public interface ITokenService<T> where T : class
    {
        string GetEmailWithToken(string token);
        bool TokenIsValid(string token);
        Task<UserModel> GetUserWithTokenAsync(string token);
        string GetTokenInHeader();
    }
}
