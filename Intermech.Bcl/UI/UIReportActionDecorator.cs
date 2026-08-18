
// Type: Intermech.UI.UIReportActionDecorator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.UI
{
    public sealed class UIReportActionDecorator : IAction
    {
      private readonly IAction action;
      private readonly string actionEvent;

      public UIReportActionDecorator(IAction action)
        : this(action, (string) null)
      {
      }

      public UIReportActionDecorator(IAction action, string actionEvent)
      {
        this.action = action != null ? action : throw new ArgumentNullException(nameof (action));
        this.actionEvent = actionEvent;
      }

      public void Perform()
      {
        if (UIReport.Enabled)
          UIReport.ReportEvent(string.IsNullOrEmpty(this.actionEvent) ? this.action.ToString() : this.actionEvent);
        this.action.Perform();
      }
    }
}
