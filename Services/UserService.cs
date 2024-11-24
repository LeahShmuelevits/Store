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

        public User PostLoginS(string username, string password)
        {

            return _iuserRepository.PostLoginR(username, password);
        }
        public User Post(User user)
        {            //check password strength

            return _iuserRepository.Post(user);
        }

        public void Put(int id, User user)
        {  //check password strength
            _iuserRepository.Put(id, user);
        }

        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
