using System;
using System.Windows.Media.Imaging;

namespace AynanayUninstaller.Models
{
    public class InstalledProgram
    {
        public string DisplayName { get; set; } = string.Empty;
        public string? UninstallString { get; set; }
        public string? QuietUninstallString { get; set; }
        public string? Publisher { get; set; }
        public string? Version { get; set; }
        public DateTime? InstallDate { get; set; }
        public string? InstallLocation { get; set; }
        public long? Size { get; set; }
        public BitmapImage? Icon { get; set; }
        public string RegistryPath { get; set; } = string.Empty;
        public RegistryHive RegistryHive { get; set; }
        public Guid? IdentifyingNumber { get; set; }

        public string SizeFormatted => FormatSize(Size ?? 0);
        public string InstallDateFormatted => InstallDate?.ToString("yyyy-MM-dd") ?? "Unknown";

        private static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }

    public enum RegistryHive
    {
        LocalMachine64,
        LocalMachine32,
        CurrentUser
    }
}