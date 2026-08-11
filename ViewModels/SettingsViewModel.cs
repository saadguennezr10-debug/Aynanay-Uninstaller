using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AynanayUninstaller.Models;
using AynanayUninstaller.Services.Localization;

namespace AynanayUninstaller.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string _selectedLanguage = LocalizationService.Instance.CurrentLanguage;
        private bool _autoUpdate = true;
        private bool _createRestorePoint = true;
        private bool _showHiddenPrograms = false;

        public ObservableCollection<string> AvailableLanguages 
        { 
            get => new ObservableCollection<string>(LocalizationService.Instance.AvailableLanguages);
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    LocalizationService.Instance.SetLanguage(value);
                    OnPropertyChanged();
                }
            }
        }

        public bool AutoUpdate
        {
            get => _autoUpdate;
            set { _autoUpdate = value; OnPropertyChanged(); }
        }

        public bool CreateRestorePoint
        {
            get => _createRestorePoint;
            set { _createRestorePoint = value; OnPropertyChanged(); }
        }

        public bool ShowHiddenPrograms
        {
            get => _showHiddenPrograms;
            set { _showHiddenPrograms = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}