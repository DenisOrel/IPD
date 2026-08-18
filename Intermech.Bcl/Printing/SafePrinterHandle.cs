using Intermech.Diagnostics;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace Intermech.Printing
{
    [CLSCompliant(false)]
    public sealed class SafePrinterHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      public SafePrinterHandle()
        : base(true)
      {
      }

      public SafePrinterHandle([NotNull] string printername)
        : base(true)
      {
        this.OpenPrinter(printername);
      }

      [return: MarshalAs(UnmanagedType.U1)]
      protected override bool ReleaseHandle()
      {
        if (this.IsInvalid)
          return true;
        if (!Static.ClosePrinter(this.handle))
          return false;
        this.SetHandle(IntPtr.Zero);
        return true;
      }

      private bool OpenPrinter([NotNull] string printername)
      {
        PrinterDefaults PrinterDefaults = new PrinterDefaults(true);
        if (Static.OpenPrinter(printername, out this.handle, ref PrinterDefaults))
        {
          this.ResetPrinterInfo();
          return true;
        }
        PrinterDefaults = new PrinterDefaults(false);
        if (!Static.OpenPrinter(printername, out this.handle, ref PrinterDefaults))
        {
          this.ResetPrinterInfo();
          return false;
        }
        this.ResetPrinterInfo();
        return true;
      }

      [CanBeNull]
      public string PrinterName
      {
        get => !this.IsInvalid ? this.PrinterInfo2.PrinterName2 : string.Empty;
        set
        {
          if (!(value != this.PrinterName))
            return;
          this.ReleaseHandle();
          this.OpenPrinter(value);
        }
      }

      public void ResetPrinterInfo()
      {
        this.PrinterInfo2 = !this.IsInvalid ? new PrinterInfo2((SafeHandle) this) : (PrinterInfo2) null;
      }

      [Category("Printer")]
      [TypeConverter(typeof (ExpandableObjectConverter))]
      [Browsable(true)]
      [Description("Maximal printer information")]
      public PrinterInfo2 PrinterInfo2 { get; private set; }
    }
}
