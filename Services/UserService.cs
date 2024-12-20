﻿using Entities;
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
        public Task<User> Post(User user)
        {
            return _iuserRepository.Post(user);
        }

        public void Put(int id, User user)
        {
            _iuserRepository.Put(id, user);
        }

        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
