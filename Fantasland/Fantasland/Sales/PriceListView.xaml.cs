using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.Sales
{
    public partial class PriceListView
    {
        public PriceListView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<PriceListViewModel>();
        }
    }
}
