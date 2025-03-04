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

        public async Task<User> GetById(int id)
        {
            return await _ManagerDbContext.Users.FirstOrDefaultAsync(u => u.UserId==id);
        }

        public async Task<User> Post(User user)
        {
           var user1=  await _ManagerDbContext.Users.AddAsync(user);
            await _ManagerDbContext.SaveChangesAsync();
           
            return user1.Entity; 
        }
        public async Task<User> Put(int id, User user1)
        {
            var user=_ManagerDbContext.Users.Update(user1);
              await _ManagerDbContext.SaveChangesAsync();
            return user.Entity;
        }
    }
}


