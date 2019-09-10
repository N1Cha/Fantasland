using Autofac;
using Fantasland.Infrastructure;
using Fantasland.Sales;
using Fantasland.WarehouseModule;
using System.Windows.Input;

namespace Fantasland
{
    public class MainScreenViewModel
    {
        private ICommand productCommand;
        private ICommand stockCommand;
        private ICommand categoryCommand;
        private ICommand insertStockCommand;
        private ICommand newSaleCommand;

        public MainScreenViewModel()
        {
        }

        public ICommand ProductCommand
        {
            get { return productCommand = new Command<object>(OnProductCommand); }
        }

        public ICommand InsertStockCommand
        {
            get { return insertStockCommand = new Command<object>(OnInsertStockCommand); }
        }

        public ICommand StockCommand
        {
            get { return stockCommand = new Command<object>(OnStockCommand); }
        }

        public ICommand CategoryCommand
        {
            get { return categoryCommand = new Command<object>(OnCategoryCommand); }
        }

        public ICommand NewSaleCommand
        {
            get { return newSaleCommand = new Command<object>(OnNewSaleCommand); }
        }

        private void OnProductCommand()
        {
            Bootstraper.Container.Resolve<NewProductView>().ShowDialog();
        }

        private void OnStockCommand()
        {
            Bootstraper.Container.Resolve<FullStockView>().ShowDialog();
        }

        private void OnCategoryCommand()
        {
            Bootstraper.Container.Resolve<CategoryView>().ShowDialog();
        }

        private void OnInsertStockCommand()
        {
            Bootstraper.Container.Resolve<InsertStockView>().ShowDialog();
        }

        private void OnNewSaleCommand()
        {
            Bootstraper.Container.Resolve<NewSaleView>().ShowDialog();
        }
    }
}
