using System.Windows;
using AynanayUninstaller.ViewModels;

namespace AynanayUninstaller.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.LoadProgramsCommand.Execute(null);
            }
        }
    }
}