
// Type: Intermech.PdfPrintCenter.Services.PDMSystemService




using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.Remoting.Ipc;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Services
{
    internal sealed class PDMSystemService : IPDMSystemService
    {
      private readonly object syncRoot = new object();
      private readonly IpcConnector<IPDMSystem> _pdmConnector = new IpcConnector<IPDMSystem>();

      public bool IsPDMSystemConnected => this._pdmConnector.IsConnected;

      public object ChooseLayout()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().ChooseLayout();
      }

      public List<object> GetLayoutsId()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().GetLayoutsId();
      }

      public PDMLayoutInfo LoadLayout(object layoutId)
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().LoadLayout(layoutId);
      }

      public object SaveLayout(PDMLayoutInfo layout, object layoutId = null)
      {
        lock (this.syncRoot)
        {
          layoutId = this._pdmConnector.GetOrConnect().SaveLayout(layout, layoutId);
          return layoutId;
        }
      }

      public string GetPrintersSettings()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().GetPrintersSettings();
      }

      public void PutPrintersSettings(string xmlPrintersSettings)
      {
        lock (this.syncRoot)
          this._pdmConnector.GetOrConnect().PutPrintersSettings(xmlPrintersSettings);
      }

      public string GetWatermarkSettings()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().GetWatermakSettings();
      }

      public void PutWatermarkSettings(string xmlWatermarkSettings)
      {
        lock (this.syncRoot)
          this._pdmConnector.GetOrConnect().PutWatermarkSettings(xmlWatermarkSettings);
      }

      public string GetCurrentUserName()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().GetCurrentUserName();
      }

      public string GetWindowSettings()
      {
        lock (this.syncRoot)
          return this._pdmConnector.GetOrConnect().GetWindowSettings();
      }

      public void PutWindowSettings(string xmlWindowSettings)
      {
        lock (this.syncRoot)
          this._pdmConnector.GetOrConnect().PutWindowSettings(xmlWindowSettings);
      }

      public void SetPDMSystem(IpcConnectionInfo connectionInfo)
      {
        lock (this.syncRoot)
          this._pdmConnector.ConnectionInfo = connectionInfo;
      }
    }
}
