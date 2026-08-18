
// Type: Intermech.PdfPrintCenter.Interfaces.IWindowSettingsService





namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface IWindowSettingsService
    {
      Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings GetWindowSettings();

      void PutWindowSettings(Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowParameters);
    }
}
