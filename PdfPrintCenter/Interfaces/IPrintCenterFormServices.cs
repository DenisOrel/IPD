using Intermech.PdfPrintCenter.Services;


namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IPrintCenterFormServices
    {
        ILayoutsAnalyzerService LayoutsAnalyzerService { get; }

        ILayoutSettingsService LayoutSettingsService { get; }

        IPDMSystemService PDMSystemService { get; }

        IPrintCenterSettingsFactory PrintCenterSettingsFactory { get; }

        IPrintersSettingsService PrintersSettingsService { get; }

        IWatermarkSettingsService WatermarkSettingsService { get; }

        IWindowSettingsService WindowSettingsService { get; }

        PrintCenterStartupService PrintCenterStartupService { get; }
    }
}
