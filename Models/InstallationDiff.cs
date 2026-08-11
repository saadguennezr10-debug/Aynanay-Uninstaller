using System;
using System.Collections.Generic;

namespace AynanayUninstaller.Models
{
    public class InstallationDiff
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<string> NewPrograms { get; set; } = new();
        public List<string> ModifiedPrograms { get; set; } = new();
        public List<string> NewFiles { get; set; } = new();
        public List<string> ModifiedFiles { get; set; } = new();
        public List<string> NewRegistryKeys { get; set; } = new();
        public List<string> ModifiedRegistryKeys { get; set; } = new();
        public List<string> NewServices { get; set; } = new();
        public List<string> NewStartupEntries { get; set; } = new();

        public int TotalChanges =>
            NewPrograms.Count +
            ModifiedPrograms.Count +
            NewFiles.Count +
            ModifiedFiles.Count +
            NewRegistryKeys.Count +
            ModifiedRegistryKeys.Count +
            NewServices.Count +
            NewStartupEntries.Count;
    }
}