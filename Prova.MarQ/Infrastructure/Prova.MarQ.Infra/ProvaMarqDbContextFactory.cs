using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Prova.MarQ.Infra
{
    public class ProvaMarqDbContextFactory : IDesignTimeDbContextFactory<ProvaMarqDbContext>
    {
        public ProvaMarqDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProvaMarqDbContext>();
            optionsBuilder.UseSqlite("Data Source=provaMarqDB.db"); // Use your actual connection string here

            return new ProvaMarqDbContext(optionsBuilder.Options);
        }
    }
}