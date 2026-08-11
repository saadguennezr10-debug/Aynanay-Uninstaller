using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;

namespace AynanayUninstaller.Services.Programs
{
    public class InstalledProgramService
    {
        private static readonly Lazy<InstalledProgramService> Instance = new(() => new InstalledProgramService());
        public static InstalledProgramService GetInstance => Instance.Value;

        public ObservableCollection<Models.InstalledProgram> GetInstalledPrograms()
        {
            var programs = new ObservableCollection<Models.InstalledProgram>();

            // HKLM 64-bit
            programs = GetProgramsFromRegistry(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", Models.RegistryHive.LocalMachine64, programs);

            // HKLM 32-bit
            programs = GetProgramsFromRegistry(RegistryHive.LocalMachine, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", Models.RegistryHive.LocalMachine32, programs);

            // HKCU
            programs = GetProgramsFromRegistry(RegistryHive.CurrentUser, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", Models.RegistryHive.CurrentUser, programs);

            return programs;
        }

        private ObservableCollection<Models.InstalledProgram> GetProgramsFromRegistry(RegistryHive hive, string path, Models.RegistryHive registryHive, ObservableCollection<Models.InstalledProgram> programs)
        {
            try
            {
                using (var key = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
                using (var subKey = key.OpenSubKey(path))
                {
                    if (subKey != null)
                    {
                        foreach (var subKeyName in subKey.GetSubKeyNames())
                        {
                            using (var appKey = subKey.OpenSubKey(subKeyName))
                            {
                                if (appKey != null)
                                {
                                    var displayName = appKey.GetValue("DisplayName")?.ToString();
                                    if (!string.IsNullOrEmpty(displayName))
                                    {
                                        var program = new Models.InstalledProgram
                                        {
                                            DisplayName = displayName,
                                            Publisher = appKey.GetValue("Publisher")?.ToString(),
                                            Version = appKey.GetValue("DisplayVersion")?.ToString(),
                                            UninstallString = appKey.GetValue("UninstallString")?.ToString(),
                                            QuietUninstallString = appKey.GetValue("QuietUninstallString")?.ToString(),
                                            InstallLocation = appKey.GetValue("InstallLocation")?.ToString(),
                                            RegistryPath = subKeyName,
                                            RegistryHive = registryHive
                                        };

                                        if (int.TryParse(appKey.GetValue("InstallDate")?.ToString(), out int installDate))
                                        {
                                            try
                                            {
                                                program.InstallDate = DateTime.ParseExact(installDate.ToString(), "yyyyMMdd", null);
                                            }
                                            catch { }
                                        }

                                        if (long.TryParse(appKey.GetValue("EstimatedSize")?.ToString(), out long size))
                                        {
                                            program.Size = size * 1024; // Convert to bytes
                                        }

                                        programs.Add(program);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading registry: {ex.Message}");
            }

            return programs;
        }
    }
}