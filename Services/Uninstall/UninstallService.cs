using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AynanayUninstaller.Services.Uninstall
{
    public class UninstallService
    {
        private static readonly Lazy<UninstallService> Instance = new(() => new UninstallService());
        public static UninstallService GetInstance => Instance.Value;

        public async Task<bool> UninstallProgramAsync(Models.InstalledProgram program)
        {
            if (string.IsNullOrEmpty(program.UninstallString))
                return false;

            try
            {
                var uninstallString = program.UninstallString.Trim();

                // Handle quoted paths
                if (uninstallString.StartsWith('"'))
                {
                    var endQuote = uninstallString.IndexOf('"', 1);
                    if (endQuote != -1)
                    {
                        var exePath = uninstallString.Substring(1, endQuote - 1);
                        var args = uninstallString.Substring(endQuote + 1).Trim();

                        if (File.Exists(exePath))
                        {
                            return await RunUninstallerAsync(exePath, args);
                        }
                    }
                }
                else
                {
                    var parts = uninstallString.Split(' ');
                    var exePath = parts[0];
                    var args = string.Join(" ", parts, 1, parts.Length - 1);

                    if (File.Exists(exePath))
                    {
                        return await RunUninstallerAsync(exePath, args);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Uninstall error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> RunUninstallerAsync(string exePath, string args)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var process = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    using (var p = Process.Start(process))
                    {
                        if (p != null)
                        {
                            p.WaitForExit(300000); // 5 minutes timeout
                            return p.ExitCode == 0 || p.ExitCode == 1; // Some installers return 1 on success
                        }
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Run uninstaller error: {ex.Message}");
                    return false;
                }
            });
        }
    }
}