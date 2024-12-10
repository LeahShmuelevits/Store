using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ManagerDbContext _ManagerDbContext;
         
        public UserRepository(ManagerDbContext managerDbContext)
        {
            _ManagerDbContext = managerDbContext;
        }

        public async Task<User> PostLoginR(string username, string password)
        {
            User user = await _ManagerDbContext.Users.FirstOrDefaultAsync(user => user.UserName == username && user.Password == password);
            return user;
        }
        public async Task<User> Post(User user)
        {
            await _ManagerDbContext.Users.AddAsync(user);
            //var res=await _ManagerDbContext.Users.AddAsync(user);
            await _ManagerDbContext.SaveChangesAsync();
            //return res- the created user with the id
            return user; 
        }
        public async Task Put(int id, User user1)//return user
        {
              _ManagerDbContext.Users.Update(user1);
              await _ManagerDbContext.SaveChangesAsync();
        }
    }
}


