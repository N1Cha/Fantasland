using Fantasland.Infrastructure;
using Fantasland.WarehouseModule;
using System.Windows.Input;

namespace Fantasland
{
    public class MainScreenViewModel
    {
        private ICommand productCommand;
        private ICommand stockCommand;

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

        private void OnProductCommand(object data)
        {

        }

        private void OnStockCommand(object data)
        {
            FullStockView form = new FullStockView();
            form.ShowDialog();
        }
    }
}
