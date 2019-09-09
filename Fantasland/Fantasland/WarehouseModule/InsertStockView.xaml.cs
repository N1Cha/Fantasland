using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.WarehouseModule
{
    public partial class InsertStockView
    {
        public InsertStockView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<InsertStockViewModel>();
        }
    }
}
