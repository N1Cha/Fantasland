using Fantasland.MainMenu;

namespace Fantasland
{
    public partial class MainScreen
    {
        private MainScreenViewModel viewModel;

        public MainScreen()
        {
            InitializeComponent();
            this.DataContext = new MainScreenViewModel();
        }
    }
}
