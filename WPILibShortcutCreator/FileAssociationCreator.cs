using System.Runtime.InteropServices;
using Microsoft.Win32;
using WPILibInstaller.Models;

namespace WPILibShortcutCreator;

internal static class FileAssociationCreator
{
    public static bool RegisterFileAssociations(IReadOnlyList<FileAssociationInfo> associations, bool isAdmin)
    {
        if (associations.Count == 0)
        {
            return true;
        }

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return false;
        }

        bool allSuccessful = true;

        try
        {
            RegistryKey rootKey = isAdmin ? Registry.LocalMachine : Registry.CurrentUser;
            using var classesKey = rootKey.OpenSubKey(@"Software\Classes", true);
            if (classesKey == null)
            {
                return false;
            }

            foreach (var assoc in associations)
            {
                if (string.IsNullOrEmpty(assoc.Extension) || string.IsNullOrEmpty(assoc.ExecutablePath))
                {
                    continue;
                }

                try
                {
                    // 1. Create ProgID key e.g. HKCU\Software\Classes\AdvantageScope.wpilog
                    using (var progIdKey = classesKey.CreateSubKey(assoc.ProgId))
                    {
                        if (progIdKey != null)
                        {
                            if (!string.IsNullOrEmpty(assoc.Name))
                            {
                                progIdKey.SetValue(string.Empty, assoc.Name);
                            }

                            // DefaultIcon: use custom icon if available, or fall back to "executable.exe,0"
                            string iconLocation = !string.IsNullOrEmpty(assoc.IconPath) && File.Exists(assoc.IconPath)
                                ? assoc.IconPath
                                : $"{assoc.ExecutablePath},0";

                            using (var iconKey = progIdKey.CreateSubKey("DefaultIcon"))
                            {
                                iconKey?.SetValue(string.Empty, iconLocation);
                            }

                            // shell\open\command
                            using (var commandKey = progIdKey.CreateSubKey(@"shell\open\command"))
                            {
                                commandKey?.SetValue(string.Empty, $"\"{assoc.ExecutablePath}\" \"%1\"");
                            }
                        }
                    }

                    // 2. Associate Extension e.g. HKCU\Software\Classes\.wpilog
                    using (var extKey = classesKey.CreateSubKey(assoc.Extension))
                    {
                        if (extKey != null)
                        {
                            extKey.SetValue(string.Empty, assoc.ProgId);

                            using (var openWithKey = extKey.CreateSubKey("OpenWithProgids"))
                            {
                                openWithKey?.SetValue(assoc.ProgId, string.Empty);
                            }
                        }
                    }
                }
                catch
                {
                    allSuccessful = false;
                }
            }

            // Notify Explorer that associations have changed
            NativeMethods.SHChangeNotify(NativeMethods.SHCNE_ASSOCCHANGED, NativeMethods.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }
        catch
        {
            return false;
        }

        return allSuccessful;
    }
}
