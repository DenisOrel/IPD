
// Type: Intermech.PdfPrintCenter.PrintCenterSystem




using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.Services;
using Intermech.Remoting.Ipc;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.PdfPrintCenter
{
    internal sealed class PrintCenterSystem : 
      MarshalByRefObject,
      IPrintCenterSystem,
      IReliableIpcObject,
      IDisposable
    {
      private readonly object syncRoot = new object();
      private Lazy<PrintCenterForm> printCenterForm;
      private IPDMSystemService pdmSystemService;
      private PrintCenterStartupService printCenterStartupService;
      private ManualResetEventSlim startedEvent;

      public PrintCenterSystem(
        Lazy<PrintCenterForm> printCenterForm,
        IPDMSystemService pdmSystemService,
        PrintCenterStartupService printCenterStartupService)
      {
        this.printCenterForm = printCenterForm;
        this.pdmSystemService = pdmSystemService;
        this.printCenterStartupService = printCenterStartupService;
        this.startedEvent = new ManualResetEventSlim(false);
      }

      public ManualResetEventSlim StartedEvent => this.startedEvent;

      public void Dispose()
      {
        if (this.startedEvent == null)
          return;
        this.startedEvent.Dispose();
        this.startedEvent = (ManualResetEventSlim) null;
      }

      public override object InitializeLifetimeService() => (object) null;

      public void KnockKnock()
      {
      }

      public void ClosePrintCenter()
      {
        lock (this.syncRoot)
        {
          try
          {
            this.printCenterForm.Value.Close();
          }
          catch (Exception ex)
          {
            throw IpcFaultException.FromOriginalException(ex);
          }
        }
      }

      public void SetPDMSystem(IpcConnectionInfo connectionInfo)
      {
        if (connectionInfo == null)
          throw new ArgumentNullException(nameof (connectionInfo));
        lock (this.syncRoot)
        {
          try
          {
            this.pdmSystemService.SetPDMSystem(connectionInfo);
            this.startedEvent.Set();
          }
          catch (Exception ex)
          {
            throw IpcFaultException.FromOriginalException(ex);
          }
        }
      }

      public void AddDocuments(List<PDMDocumentInfo> documents)
      {
        this.CheckDocuments(documents);
        this.WaitForStartupCompleted();
        lock (this.syncRoot)
        {
          try
          {
            this.printCenterForm.Value.AddFilesFromPdm(documents);
          }
          catch (Exception ex)
          {
            throw IpcFaultException.FromOriginalException(ex);
          }
        }
      }

      private void CheckDocuments(List<PDMDocumentInfo> documents)
      {
        if (documents == null)
          throw new ArgumentNullException(nameof (documents));
      }

      private void WaitForStartupCompleted()
      {
        if (!this.printCenterStartupService.StartedEvent.Wait(30000))
          throw new TimeoutException();
      }
    }
}
