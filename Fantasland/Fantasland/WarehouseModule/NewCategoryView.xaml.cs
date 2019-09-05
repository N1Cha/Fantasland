using Autofac;
using Fantasland.Infrastructure;
using System.Windows;

namespace Fantasland.WarehouseModule
{
    public partial class NewCategoryView
    {
        public NewCategoryView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<NewCategoryViewModel>();
        }
    }
}
