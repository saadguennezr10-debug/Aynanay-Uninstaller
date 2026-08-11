using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AynanayUninstaller.Services.Localization;
using AynanayUninstaller.Services.Programs;
using AynanayUninstaller.Models;

namespace AynanayUninstaller.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentPage = "Programs";
        private ObservableCollection<InstalledProgram> _programs = new();
        private string _searchText = string.Empty;
        private bool _isLoading = false;
        private string _statusMessage = "Ready";

        public string CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<InstalledProgram> Programs
        {
            get => _programs;
            set { _programs = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); FilterPrograms(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoadProgramsCommand { get; }
        public ICommand UninstallCommand { get; }
        public ICommand NavigateCommand { get; }

        public MainViewModel()
        {
            LoadProgramsCommand = new RelayCommand(_ => LoadPrograms());
            UninstallCommand = new RelayCommand(p => UninstallProgram(p as InstalledProgram));
            NavigateCommand = new RelayCommand(p => CurrentPage = p?.ToString() ?? "Programs");
        }

        public void LoadPrograms()
        {
            IsLoading = true;
            StatusMessage = LocalizationService.Instance.Translate("loading", "Loading...");

            var service = InstalledProgramService.GetInstance;
            Programs = service.GetInstalledPrograms();
            FilterPrograms();

            IsLoading = false;
            StatusMessage = LocalizationService.Instance.Translate("success", "Success");
        }

        private void FilterPrograms()
        {
            if (string.IsNullOrEmpty(SearchText))
                return;

            var filtered = new ObservableCollection<InstalledProgram>();
            foreach (var program in Programs)
            {
                if (program.DisplayName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    filtered.Add(program);
            }

            Programs = filtered;
        }

        private void UninstallProgram(InstalledProgram? program)
        {
            if (program == null) return;
            // Implementation will be in the next update
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}