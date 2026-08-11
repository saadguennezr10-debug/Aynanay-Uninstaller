using System;
using System.Collections.Generic;

namespace AynanayUninstaller.Models
{
    public class ScanResult
    {
        public string ProgramName { get; set; } = string.Empty;
        public List<ResidualEntry> ResidualEntries { get; set; } = new();
        public long TotalSize { get; set; }
        public DateTime ScanDate { get; set; } = DateTime.Now;
        public ScanLevel ScanLevel { get; set; }
        public TimeSpan ScanDuration { get; set; }
        public int FilesFound { get; set; }
        public int FoldersFound { get; set; }
        public int RegistryKeysFound { get; set; }
        public string? Notes { get; set; }

        public string TotalSizeFormatted => FormatSize(TotalSize);

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

    public enum ScanLevel
    {
        Safe,
        Moderate,
        Advanced
    }
}