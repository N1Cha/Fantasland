using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland.Sales
{
    public partial class AllSalesView
    {
        public AllSalesView()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<AllSalesViewModel>();
        }
    }
}
