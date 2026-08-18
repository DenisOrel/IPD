
// Type: Intermech.PdfPrintCenter.Interfaces.IWatermarkSettingsService





namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IWatermarkSettingsService
    {
      Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings GetWatermarkSettings();

      void PutWatermarkSettings(Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings);

      Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings GetWatermarkSettingsWithSubstitutes();
    }
}
