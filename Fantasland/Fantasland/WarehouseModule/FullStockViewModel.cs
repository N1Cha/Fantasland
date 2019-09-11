using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Fantasland.WarehouseModule 
{
    public class FullStockViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<Product> allProducts;

        public FullStockViewModel()
        {
            this.AllProducts = new ObservableCollection<Product>();

            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Products.Include(x => x.Category).Load();
                this.AllProducts = context.Products.Local;
            }
        }

        public ObservableCollection<Product> AllProducts
        {
            get { return this.allProducts; }
            set { this.allProducts = value; }
        }

    }
}
