
// Type: Intermech.WindowsDll.Winspool
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace Intermech.WindowsDll
{
    public static class Winspool
    {
      public const int DM_OUT_BUFFER = 14;

      [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool OpenPrinter(
        [NotNull] string printerName,
        out IntPtr printerHandle,
        ref PrinterDefaults printerDefaults);

      [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool OpenPrinter(
        string printerName,
        out IntPtr printerHandle,
        int printerDefaults);

      [DllImport("winspool.Drv", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool ClosePrinter(IntPtr hPrinter);

      [DllImport("winspool.drv", CharSet = CharSet.Unicode)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetDefaultPrinterW(string printerName, out int dwNeeded);

      [DllImport("winspool.drv", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetDefaultPrinter(string printerName);

      [DllImport("winspool.drv", EntryPoint = "GetPrinterW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetPrinter(
        IntPtr hPrinter,
        int level,
        IntPtr pPrinter,
        int cbBuf,
        out int dwNeeded);

      [DllImport("winspool.drv", EntryPoint = "GetPrinterDriverW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetPrinterDriver(
        IntPtr printerHandle,
        IntPtr pEnvironment,
        int level,
        IntPtr pPrinter,
        int cbBuf,
        out int dwNeeded);

      [DllImport("winspool.drv", EntryPoint = "EnumPrinterDriversW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool EnumPrinterDrivers(
        [In] string serverName,
        [In] string environmentName,
        [In] int level,
        [Out] IntPtr pDrivers,
        [In] int cbBuf,
        out int pcbNeeded,
        out int pcbReturned);

      [DllImport("winspool.drv", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      public static extern int DeviceCapabilitiesW(
        string sDevice,
        string port,
        int fwCapability,
        IntPtr output,
        IntPtr device);

      [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
      public static extern int DocumentProperties(
        IntPtr hwnd,
        IntPtr hPrinter,
        [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
        IntPtr pDevModeOutput,
        IntPtr pDevModeInput,
        int fMode);

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
      [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
      public struct PrinterDefaults
      {
        [MarshalAs(UnmanagedType.SysInt)]
        private readonly IntPtr dDataType;
        [MarshalAs(UnmanagedType.SysInt)]
        private readonly IntPtr dDeviceMode;
        [MarshalAs(UnmanagedType.U4)]
        public int DesiredAccess;

        public PrinterDefaults(bool allAccess)
        {
          this.dDataType = IntPtr.Zero;
          this.dDeviceMode = IntPtr.Zero;
          if (allAccess)
          {
            new PrintingPermission(PermissionState.Unrestricted).Demand();
            this.DesiredAccess = 983052 /*0x0F000C*/;
          }
          else
            this.DesiredAccess = 8;
        }
      }
    }
}
