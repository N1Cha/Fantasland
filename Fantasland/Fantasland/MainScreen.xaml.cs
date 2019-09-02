using Autofac;
using Fantasland.Infrastructure;

namespace Fantasland
{
    public partial class MainScreen
    {
        private MainScreenViewModel viewModel;

        public MainScreen()
        {
            InitializeComponent();
            this.DataContext = Bootstraper.Container.Resolve<MainScreenViewModel>();
        }
    }
}
