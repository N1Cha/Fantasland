using Data.Models;
using System.Data.Entity;

namespace Data
{
    public class AppDbContext : DbContext
    {
        AppDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        DbSet<Product> Products { get; set; }
    }
}
