
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class DataBaseFixture : IDisposable
    {
        public ManagerDbContext Context { get; private set; }
        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<ManagerDbContext>()
                .UseSqlServer("Server=srv2\\pupils;Database=Test_Store_Project;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            Context = new ManagerDbContext(options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

