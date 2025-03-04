using Entities;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> Post(User user);
        Task<User> PostLoginR(string username, string password);
        Task<User> Put(int id, User user1);
        Task<User> GetById(int id);
    }
}