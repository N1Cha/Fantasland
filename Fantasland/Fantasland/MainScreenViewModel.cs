using Autofac;
using Fantasland.Infrastructure;
using Fantasland.WarehouseModule;
using System.Windows.Input;

namespace Fantasland
{
    public class MainScreenViewModel
    {
        private ICommand productCommand;
        private ICommand stockCommand;
        private ICommand categoryCommand;

        public MainScreenViewModel()
        {
        }

        public ICommand ProductCommand
        {
            get { return productCommand = new Command<object>(OnProductCommand); }
        }

        public ICommand StockCommand
        {
            get { return stockCommand = new Command<object>(OnStockCommand); }
        }

        public ICommand CategoryCommand
        {
            get { return categoryCommand = new Command<object>(OnCategoryCommand); }
        }

        private void OnProductCommand(object data)
        {

        }

        private void OnStockCommand(object data)
        {
            Bootstraper.Container.Resolve<FullStockView>().ShowDialog();
        }

        private void OnCategoryCommand(object data)
        {
            Bootstraper.Container.Resolve<CategoryView>().ShowDialog();
        }
    }
}
