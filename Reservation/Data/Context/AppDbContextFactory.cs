using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Data.Context;

namespace Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                            "Data Source=DESKTOP-9BEID0U\\AA;Initial Catalog=ReservationDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"
                        );
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
