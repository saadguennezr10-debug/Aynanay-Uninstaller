using System;

namespace AynanayUninstaller.Models
{
    public class ResidualEntry
    {
        public string DisplayName { get; set; } = string.Empty;
        public ResidualEntryType Type { get; set; }
        public string Path { get; set; } = string.Empty;
        public long? Size { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsSelected { get; set; }
        public string? Description { get; set; }
        public SafetyLevel SafetyLevel { get; set; }

        public string SizeFormatted => FormatSize(Size ?? 0);

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

    public enum ResidualEntryType
    {
        File,
        Folder,
        RegistryKey,
        StartupEntry,
        Service,
        ScheduledTask
    }

    public enum SafetyLevel
    {
        Safe,
        Moderate,
        Dangerous
    }
}