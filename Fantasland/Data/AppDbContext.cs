using Data.Models;
using System.Data.Entity;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Invoices> Invoices { get; set; }

        public DbSet<InvoiceItems> InvoiceItems { get; set; }
    }
}
