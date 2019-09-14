using Autofac;
using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Fantasland.Sales
{
    public class PriceListViewModel : NotifyPropertyChanged
    {
        private ICommand savePriceListCommand;

        public PriceListViewModel()
        {
            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Products.Include(x => x.Category).Load();
                this.AllProducts = context.Products.Local;
            }
        }

        public ObservableCollection<Product> AllProducts { get; set; }

        public ICommand SavePriceListCommand
        {
            get { return savePriceListCommand = new Command<object>(OnSavePriceListCommand); }
        }

        private void OnSavePriceListCommand()
        {
            if(this.IsPricesValid())
            {
                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Products.Load();

                    foreach (Product product in context.Products.Local)
                    {
                        product.Price = this.AllProducts.First(i => i.Id == product.Id).Price;
                    }

                    context.SaveChanges();
                    MessageBox.Show("The price are chaned successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool IsPricesValid()
        {
            foreach (Product product in this.AllProducts)
            {
                if(product.Price <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

