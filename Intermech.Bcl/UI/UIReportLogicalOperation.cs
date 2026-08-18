
// Type: Intermech.UI.UIReportLogicalOperation
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.UI
{
    public sealed class UIReportLogicalOperation : IDisposable
    {
      private object id;

      internal UIReportLogicalOperation(object id)
      {
        UIReport.StartLogicalOperation(id);
        this.id = id;
      }

      public void Dispose()
      {
        if (this.id == null)
          return;
        UIReport.StopLogicalOperation(this.id);
        this.id = (object) null;
      }
    }
}
