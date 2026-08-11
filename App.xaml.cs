using System.Windows;
using AynanayUninstaller.Services.Localization;
using AynanayUninstaller.ViewModels;
using AynanayUninstaller.Views;

namespace AynanayUninstaller
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Initialize localization
                LocalizationService.Instance.Initialize();

                // Create and show main window
                MainWindow mainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting application: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }
    }
}