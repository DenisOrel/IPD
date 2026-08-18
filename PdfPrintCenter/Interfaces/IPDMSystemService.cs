using Intermech.PdfPrintCenter.Connector;
using Intermech.Remoting.Ipc;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IPDMSystemService
    {
        bool IsPDMSystemConnected { get; }

        object ChooseLayout();

        List<object> GetLayoutsId();

        PDMLayoutInfo LoadLayout(object layoutId);

        object SaveLayout(PDMLayoutInfo layout, object layoutId = null);

        string GetPrintersSettings();

        void PutPrintersSettings(string xmlPrintersSettings);

        string GetWatermarkSettings();

        void PutWatermarkSettings(string xmlWatermarkSettings);

        string GetCurrentUserName();

        string GetWindowSettings();

        void PutWindowSettings(string xmlWindowSettings);

        void SetPDMSystem(IpcConnectionInfo connectionInfo);
    }
}
