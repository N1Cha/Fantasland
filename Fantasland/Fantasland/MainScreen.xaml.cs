using Autofac;
using Data;
using Fantasland.Infrastructure;
using System.Data.Entity;

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
