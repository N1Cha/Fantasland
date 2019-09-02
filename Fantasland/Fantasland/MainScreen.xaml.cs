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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Products.Load();
                var t = context.Products.Local;
            }
        }
    }
}
