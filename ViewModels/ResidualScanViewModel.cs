using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AynanayUninstaller.Models;

namespace AynanayUninstaller.ViewModels
{
    public class ResidualScanViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ResidualEntry> _residualEntries = new();
        private ScanLevel _selectedScanLevel = ScanLevel.Safe;
        private bool _isScanning = false;
        private string _scanProgress = "0%";
        private long _totalSizeToDelete = 0;

        public ObservableCollection<ResidualEntry> ResidualEntries
        {
            get => _residualEntries;
            set { _residualEntries = value; OnPropertyChanged(); }
        }

        public ScanLevel SelectedScanLevel
        {
            get => _selectedScanLevel;
            set { _selectedScanLevel = value; OnPropertyChanged(); }
        }

        public bool IsScanning
        {
            get => _isScanning;
            set { _isScanning = value; OnPropertyChanged(); }
        }

        public string ScanProgress
        {
            get => _scanProgress;
            set { _scanProgress = value; OnPropertyChanged(); }
        }

        public long TotalSizeToDelete
        {
            get => _totalSizeToDelete;
            set { _totalSizeToDelete = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}