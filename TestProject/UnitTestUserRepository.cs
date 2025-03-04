using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
namespace TestProject
{
    public class UnitTestUserRepository
    {
        [Fact]
    public async Task GetUser_Login_ReturnsUser()
        {
            var user = new User { UserName = "Leah", Password = "Aa!123dsA" };
            var mockContext = new Mock<ManagerDbContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.PostLoginR(user.UserName, user.Password);
            Assert.Equal(user, result);

        }
        [Fact]
        public async Task GetById_ReturnsUser()
        {
            var user = new User { UserId = 1, UserName = "Leah", Password = "Aa!123dsA" };
            var mockContext = new Mock<ManagerDbContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.GetById(1);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetById_UserNotFound_ReturnsNull()
        {
            var user = new User { UserId = 1, UserName = "Leah", Password = "Aa!123dsA" };
            var mockContext = new Mock<ManagerDbContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.GetById(2);
            Assert.Null(result);
        }

    }
}