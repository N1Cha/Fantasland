using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.WarehouseModule
{
    public partial class FullStockView
    {
        public FullStockView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<FullStockViewModel>();
        }
    }
}
