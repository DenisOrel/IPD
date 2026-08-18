namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IPrintersSettingsService
    {
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings GetDefaultPrintersSettings();

        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings GetPrintersSettings();

        void PutPrintersSettings(Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings);
    }
}
