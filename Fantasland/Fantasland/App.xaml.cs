using Fantasland.Infrastructure;
using System.Windows;

namespace Fantasland
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstraper.Init();
        }
    }
}
