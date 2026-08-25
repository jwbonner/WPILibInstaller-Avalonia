using System.Runtime.InteropServices;

namespace WPILibShortcutCreator;

internal static partial class NativeMethods
{
    public const uint ClsctxInprocServer = 0x1;
    public const int ErrorPathNotFound = 3;

    public const uint SHCNE_ASSOCCHANGED = 0x08000000;
    public const uint SHCNF_IDLIST = 0x0000;

    [LibraryImport("ole32", EntryPoint = "CoCreateInstance")]
    public static partial int CoCreateInstance(
        ref Guid rclsid,
        IntPtr pUnkOuter,
        uint dwClsContext,
        ref Guid riid,
        out IntPtr ppv);

    [LibraryImport("shell32", EntryPoint = "SHChangeNotify")]
    public static partial void SHChangeNotify(
        uint wEventId,
        uint uFlags,
        IntPtr dwItem1,
        IntPtr dwItem2);
}
