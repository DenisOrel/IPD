using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace Intermech.Printing
{
    [CLSCompliant(false)]
    public sealed class PrinterInfo2 : SafeInfo
    {
      internal PrinterInfo2(SafeHandle printerHandle)
        : base(printerHandle, 2)
      {
      }

      [PrintingPermission(SecurityAction.Demand, Level = PrintingPermissionLevel.DefaultPrinting)]
      protected override void RefreshInfo()
      {
        if (Static.GetPrinter(this.PrinterHandle, this._Level, this.handle, this._Size, out this._BytesNeeded) || this._BytesNeeded <= 0)
          return;
        this.AllocMem();
        if (!Static.GetPrinter(this.PrinterHandle, this._Level, this.handle, this._Size, out this._BytesNeeded) && this._BytesNeeded > 0)
          throw new Win32Exception($"{this.GetType().FullName}{this._Level.ToString((IFormatProvider) CultureInfo.InvariantCulture)} Error");
      }

      public override string ToString() => this.PrintProcessor ?? string.Empty;

      [CanBeNull]
      [Description("The server that controls the printer. If this string is NULL, the printer is controlled locally.")]
      public string ServerName => this.GetStringField(0);

      [CanBeNull]
      [Description("The name of the printer.")]
      [DisplayName("Printer Name")]
      public string PrinterName2 => this.GetStringField(1);

      [CanBeNull]
      [Description("The sharepoint for the printer.")]
      public string ShareName => this.GetStringField(2);

      [CanBeNull]
      [Description("The port(s) used to transmit data to the printer. If a printer is connected to more than one port, the names of each port must be separated by commas (for example, 'LPT1:,LPT2:,LPT3:').")]
      public string PortName => this.GetStringField(3);

      [CanBeNull]
      [Description("The name of the printer driver.")]
      public string DriverName => this.GetStringField(4);

      [CanBeNull]
      [Description("A brief description of the printer.")]
      public string Comment => this.GetStringField(5);

      [CanBeNull]
      [Description("The physical location of the printer (for example, 'Bldg. 38, Room 1164'). ")]
      public string Location => this.GetStringField(6);

      [CanBeNull]
      [Description("The name of the file used to create the separator page. This page is used to separate print jobs sent to the printer.")]
      public string SeparatorFile => this.GetStringField(8);

      [CanBeNull]
      [Description("The name of the print processor used by the printer.")]
      public string PrintProcessor => this.GetStringField(9);

      [CanBeNull]
      [Description("The data type used to record the print job.")]
      public string DataType => this.GetStringField(10);

      [CanBeNull]
      [Description("The default print-processor parameters.")]
      public string Parameters => this.GetStringField(11);

      [Description("A SECURITY_DESCRIPTOR structure for the printer. This member may be NULL.")]
      [Browsable(false)]
      public int SecurityDescriptor => this.GetIntField(12);

      [Description("Specifies a priority value that the spooler uses to route print jobs.")]
      public int Priority => this.GetIntField(14);

      [Description("Specifies the default priority value assigned to each print job.")]
      public int DefaultPriority => this.GetIntField(15);

      [NotNull]
      [Description("The Status of the Printer.")]
      public CPrinterStatus Status => new CPrinterStatus(this.GetIntField(18));

      [Description("The number of print jobs that have been queued for the printer.")]
      public int Jobs => this.GetIntField(19);

      [Description("The average number of pages per minute that have been printed on the printer.")]
      public int AveragePPM => this.GetIntField(20);
    }
}
