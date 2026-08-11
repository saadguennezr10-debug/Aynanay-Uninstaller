using System;
using System.Collections.Generic;
using System.Linq;

namespace AynanayUninstaller.Security
{
    public class ProtectedPaths
    {
        private static readonly Lazy<ProtectedPaths> Instance =
            new(() => new ProtectedPaths());

        public static ProtectedPaths GetInstance => Instance.Value;

        private readonly HashSet<string> _protectedPaths;

        public ProtectedPaths()
        {
            _protectedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                // Windows System Directories
                "C:\\Windows",
                "C:\\Windows\\System32",
                "C:\\Windows\\SysWOW64",
                "C:\\Windows\\System",
                "C:\\Windows\\Temp",
                "C:\\Windows\\Prefetch",
                "C:\\Windows\\Servicing",
                "C:\\Windows\\WinSxS",
                "C:\\Windows\\Boot",
                "C:\\Windows\\Drivers",

                // Program Files
                "C:\\Program Files",
                "C:\\Program Files (x86)",

                // System Data
                "C:\\ProgramData",
                "C:\\ProgramData\\Application Data",

                // Boot Critical
                "C:\\Boot",
                "C:\\EFI",

                // Recovery
                "C:\\Recovery",
                "C:\\$Recycle.Bin",

                // System Volume Information
                "C:\\System Volume Information",

                // User Profile
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Environment.GetFolderPath(Environment.SpecialFolder.System),
                Environment.GetFolderPath(Environment.SpecialFolder.SystemX86),
            };
        }

        public bool IsPathProtected(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return true;

            if (_protectedPaths.Contains(path))
                return true;

            foreach (var protectedPath in _protectedPaths)
            {
                if (path.StartsWith(protectedPath, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public void AddProtectedPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                _protectedPaths.Add(path);
        }

        public void RemoveProtectedPath(string path)
        {
            _protectedPaths.Remove(path);
        }
    }
}