using System;
using System.Collections.Generic;
using System.Linq;

namespace AynanayUninstaller.Security
{
    public class CleanupSafetyPolicy
    {
        private static readonly Lazy<CleanupSafetyPolicy> Instance =
            new(() => new CleanupSafetyPolicy());

        public static CleanupSafetyPolicy GetInstance => Instance.Value;

        private readonly ProtectedPaths _protectedPaths;

        public CleanupSafetyPolicy()
        {
            _protectedPaths = ProtectedPaths.GetInstance;
        }

        public (bool IsSafe, string? Reason) CanDeletePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return (false, "Path is empty");

            if (_protectedPaths.IsPathProtected(path))
                return (false, $"Path is protected by safety policy: {path}");

            if (IsSystemCriticalFile(path))
                return (false, "File is critical to Windows system");

            if (IsSharedComponent(path))
                return (false, "File/folder is shared with other applications");

            return (true, null);
        }

        public (bool IsSafe, string? Reason) CanDeleteRegistryKey(string registryPath)
        {
            if (string.IsNullOrWhiteSpace(registryPath))
                return (false, "Registry path is empty");

            var criticalHives = new[]
            {
                @"HKLM\SYSTEM",
                @"HKLM\SAM",
                @"HKLM\SECURITY",
                @"HKLM\HARDWARE",
                @"HKLM\COMPONENTS",
                @"HKLM\Services"
            };

            foreach (var hive in criticalHives)
            {
                if (registryPath.StartsWith(hive, StringComparison.OrdinalIgnoreCase))
                    return (false, $"Critical registry hive cannot be deleted: {hive}");
            }

            return (true, null);
        }

        private bool IsSystemCriticalFile(string path)
        {
            var criticalFiles = new[]
            {
                "ntoskrnl.exe",
                "hal.dll",
                "kernel32.dll",
                "ntdll.dll",
                "winload.exe",
                "bootmgr",
                "winload.efi"
            };

            var fileName = System.IO.Path.GetFileName(path).ToLower();
            return criticalFiles.Contains(fileName);
        }

        private bool IsSharedComponent(string path)
        {
            var sharedPaths = new[]
            {
                "System32",
                "SysWOW64",
                "Program Files",
                "Program Files (x86)",
                "Windows",
                "ProgramData"
            };

            return sharedPaths.Any(p => path.Contains(p, StringComparison.OrdinalIgnoreCase));
        }
    }
}