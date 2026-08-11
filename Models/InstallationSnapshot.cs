using System;
using System.Collections.Generic;

namespace AynanayUninstaller.Models
{
    public class InstallationSnapshot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Notes { get; set; }

        public List<string> InstalledPrograms { get; set; } = new();
        public Dictionary<string, string> RegistrySnapshot { get; set; } = new();
        public List<string> Services { get; set; } = new();
        public List<string> StartupEntries { get; set; } = new();
        public List<InstalledFileInfo> FilesSnapshot { get; set; } = new();
    }

    public class InstalledFileInfo
    {
        public string FullPath { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}