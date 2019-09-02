using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland
{
    public partial class MainScreen
    {
        public MainScreen()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<MainScreenViewModel>();
        }
    }
}
