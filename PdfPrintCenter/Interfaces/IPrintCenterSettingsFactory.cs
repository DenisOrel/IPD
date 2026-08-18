using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings;


namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IPrintCenterSettingsFactory
    {
        LayoutEditor CreateLayoutEditor();

        PrintersSettingsForm CreatePrintersSettingsForm();

        WatermarkForm CreateWatermarkForm();
    }
}
