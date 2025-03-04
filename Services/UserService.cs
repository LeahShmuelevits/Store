using Entities;
using Repositories;
using Zxcvbn;
namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _iuserRepository;

        public UserService(IUserRepository iuserRepository)
        {
            _iuserRepository = iuserRepository;
        }

        public Task<User> PostLoginS(string username, string password)
        {
            return _iuserRepository.PostLoginR(username, password);
        }
        public async Task<User> Post(User user)
        {
            int resPassword = CheckPassword(user.Password);
            if (resPassword < 4)
                return null;
            return await _iuserRepository.Post(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _iuserRepository.GetById(id);
        }
        
        public async Task<User> Put(int id, User user)
        {
            int resPassword = CheckPassword(user.Password);
            if (resPassword < 4)
                return null;
            return await _iuserRepository.Put(id, user);
        }

        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
