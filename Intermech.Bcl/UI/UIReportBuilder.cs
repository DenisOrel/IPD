
// Type: Intermech.UI.UIReportBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;


namespace Intermech.UI
{
    public class UIReportBuilder
    {
      private string separator;

      public UIReportBuilder() => this.separator = new string('=', 80 /*0x50*/);

      public void ReportStart(string text)
      {
        this.ReportSeparator();
        UIReport.ReportEvent(text);
        this.ReportCurrentTime();
        UIReport.ReportEvent(string.Empty);
      }

      public void ReportSuccess()
      {
        UIReport.ReportEvent(string.Empty);
        UIReport.ReportEvent(LocalizationHolder.rm.GetString("SR_816"));
        this.ReportCurrentTime();
      }

      public void ReportFail(Exception x)
      {
        UIReport.ReportEvent(string.Empty);
        UIReport.ReportEvent(LocalizationHolder.rm.GetString("SR_817"));
        UIReport.ReportEvent(x.Message);
      }

      public void ReportSeparator() => UIReport.ReportEvent(this.separator);

      public void ReportCurrentTime()
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Math.Truncate(DateTime.Now.TimeOfDay.TotalSeconds));
        UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_818"), (object) timeSpan));
      }
    }
}
