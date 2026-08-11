using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AynanayUninstaller.Models;

namespace AynanayUninstaller.ViewModels
{
    public class StartupViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<StartupEntry> _startupEntries = new();
        private bool _isLoading = false;

        public ObservableCollection<StartupEntry> StartupEntries
        {
            get => _startupEntries;
            set { _startupEntries = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}