using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.Sales
{
    public partial class NewSaleView
    {
        public NewSaleView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<NewSaleViewModel>();
        }
    }
}
