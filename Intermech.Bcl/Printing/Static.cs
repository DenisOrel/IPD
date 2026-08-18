using Intermech.Diagnostics;
using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;


namespace Intermech.Printing
{
    public static class Static
    {
      private static readonly TimeSpan Span1 = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
      internal static int Hourdiv = Static.Span1.Hours * 60;

      [NotNull]
      [SecurityCritical]
      [SecuritySafeCritical]
      [PrintingPermission(SecurityAction.Demand, Level = PrintingPermissionLevel.DefaultPrinting)]
      public static string GetDefaultPrinter()
      {
        int dwNeeded;
        Static.GetDefaultPrinterW((string) null, out dwNeeded);
        string PrinterName = new string(' ', dwNeeded);
        if (Static.GetDefaultPrinterW(PrinterName, out dwNeeded))
          PrinterName = PrinterName.TrimEnd(new char[1]);
        return PrinterName;
      }

      [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool OpenPrinter(
        string PrinterName,
        out IntPtr PrinterHandle,
        ref PrinterDefaults PrinterDefaults);

      [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool OpenPrinter(
        string PrinterName,
        out IntPtr PrinterHandle,
        int PrinterDefaults);

      [DllImport("winspool.Drv", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool ClosePrinter(IntPtr hPrinter);

      [DllImport("winspool.drv", CharSet = CharSet.Unicode)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetDefaultPrinterW(string PrinterName, out int dwNeeded);

      [DllImport("winspool.drv", EntryPoint = "SetDefaultPrinter", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool SetDefaultprinter(string Printername);

      [DllImport("winspool.drv", EntryPoint = "GetPrinterW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetPrinter(
        IntPtr hPrinter,
        int Level,
        IntPtr pPrinter,
        int cbBuf,
        out int dwNeeded);

      [DllImport("winspool.drv", EntryPoint = "GetPrinterDriverW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetPrinterDriver(
        IntPtr PrinterHandle,
        IntPtr pEnvironment,
        int Level,
        IntPtr pPrinter,
        int cbBuf,
        out int dwNeeded);

      [DllImport("winspool.drv", EntryPoint = "EnumPrinterDriversW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool EnumPrinterDrivers(
        [In] string ServerName,
        [In] string Environmentname,
        [In] int Level,
        [Out] IntPtr pdrivers,
        [In] int cbBuf,
        out int pcbNeeded,
        out int pcbReturned);

      [DllImport("winspool.drv", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      internal static extern int DeviceCapabilitiesW(
        string sDevice,
        string Port,
        int fwCapability,
        IntPtr Output,
        IntPtr device);
    }
}
