using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.WarehouseModule
{
    public partial class CategoryView
    {
        public CategoryView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<CategoryViewModel>();
        }
    }
}
