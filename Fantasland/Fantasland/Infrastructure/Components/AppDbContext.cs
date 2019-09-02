using System.Data.Entity;

namespace Data
{
    public class AppDbContext : DbContext
    {
        AppDbContext(string connectionString) : base(connectionString)
        {

        }
    }
}
