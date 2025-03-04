
using Entities;
using Repositories;

namespace TestProject
{
    public class IntegrationTestUserRepository : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _dbFixture;
        public IntegrationTestUserRepository()
        {
            _dbFixture = new();
        }

        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
            var _repository = new UserRepository(_dbFixture.Context);

            // Act
            var user = new User { UserName = "leah11@gmail.com", Password = "hfJG!@32h", FirstName = "Leah", LastName = "Shmuelevits" };
            var dbUser = await _repository.Post(user);

            // Assert
            Assert.NotNull(dbUser);
            //Assert.NotEqual(0, dbUser.Id);
            Assert.Equal(user.UserName, dbUser.UserName);
            Assert.Equal(user.Password, dbUser.Password);
            //  Assert.Equal("leah11@gmail.com", dbUser.UserName);
            _dbFixture.Dispose();



        }
    }
}

