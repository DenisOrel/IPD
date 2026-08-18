
// Type: Intermech.UI.UIReportScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.UI
{
    public sealed class UIReportScope : IDisposable
    {
      private UIReportScopeData data;

      internal UIReportScope(UIReportScopeData scopeData) => this.data = scopeData;

      public void Dispose()
      {
        if (this.data == null)
          return;
        if (this.Closing != null)
          this.Closing((object) this, EventArgs.Empty);
        ICollection<UIReportItem> report = UIReport.ExtractReport();
        UIReport.ReleaseScope(this);
        this.data = (UIReportScopeData) null;
        if (this.Closed != null)
          this.Closed((object) this, EventArgs.Empty);
        if (report.Count == 0)
          return;
        this.Prepare(report);
        this.Display(report);
      }

      private void Prepare(ICollection<UIReportItem> report)
      {
        if (this.PrepareReport == null)
          return;
        this.PrepareReport((object) this, new UIReportDisplayArgs(report));
      }

      private void Display(ICollection<UIReportItem> report)
      {
        if (this.DisplayReport != null)
          this.DisplayReport((object) this, new UIReportDisplayArgs(report));
        else
          UIReport.RaiseDisplayReport(report);
      }

      internal UIReportScopeData Data => this.data;

      public event EventHandler Closing;

      public event EventHandler Closed;

      public event EventHandler<UIReportDisplayArgs> PrepareReport;

      public event EventHandler<UIReportDisplayArgs> DisplayReport;
    }
}
