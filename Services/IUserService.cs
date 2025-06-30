using Entities;

namespace Services
{
    public interface IUserService
    {
        int CheckPassword(string password);
        Task<User> Post(User user);
        Task<(User user, string token)> PostLoginS(string username, string password);
        Task<User> GetById(int id);
        Task<User> Put(int id, User user);
        string GenerateJwtToken(User user, string secretKey);
    }
}