using System;

namespace AynanayUninstaller.Models
{
    public class StartupEntry
    {
        public string DisplayName { get; set; } = string.Empty;
        public string ExecutablePath { get; set; } = string.Empty;
        public StartupEntryType Type { get; set; }
        public bool IsEnabled { get; set; }
        public string? Description { get; set; }
        public string RegistryPath { get; set; } = string.Empty;
        public DateTime? LastModified { get; set; }
    }

    public enum StartupEntryType
    {
        RegistryRun,
        RegistryRunOnce,
        Folder,
        Service
    }
}