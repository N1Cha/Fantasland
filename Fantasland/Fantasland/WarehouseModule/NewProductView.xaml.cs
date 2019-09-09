using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.WarehouseModule
{
    public partial class NewProductView
    {
        public NewProductView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<NewProductViewModel>();
        }
    }
}
