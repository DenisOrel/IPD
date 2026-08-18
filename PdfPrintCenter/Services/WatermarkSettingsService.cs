
// Type: Intermech.PdfPrintCenter.Services.WatermarkSettingsService




using Intermech.PdfPrintCenter.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;


namespace Intermech.PdfPrintCenter.Services
{
    internal class WatermarkSettingsService : IWatermarkSettingsService
    {
      private readonly object syncRoot = new object();
      private IPDMSystemService pdmSystemService;
      private Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings _cachedSettings;

      public WatermarkSettingsService(IPDMSystemService pdmSystemService)
      {
        this.pdmSystemService = pdmSystemService;
      }

      public Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings GetWatermarkSettings()
      {
        lock (this.syncRoot)
        {
          if (this._cachedSettings == null)
            this.LoadWatermarkSettings();
          return this._cachedSettings;
        }
      }

      public void PutWatermarkSettings(Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings)
      {
        watermarkSettings.RequireFrozen();
        lock (this.syncRoot)
        {
          using (StringWriter output = new StringWriter())
          {
            using (XmlWriter writer = XmlWriter.Create((TextWriter) output))
            {
              new DataContractSerializer(typeof (Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings)).WriteObject(writer, watermarkSettings.Clone());
              writer.Flush();
              this.pdmSystemService.PutWatermarkSettings(output.ToString());
            }
          }
          this._cachedSettings = watermarkSettings;
        }
      }

      public Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings GetWatermarkSettingsWithSubstitutes()
      {
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings = this.GetWatermarkSettings();
        string str = watermarkSettings.Text;
        if (str.Contains("[Пользователь]"))
        {
          string currentUserName = this.pdmSystemService.GetCurrentUserName();
          str = str.Replace("[Пользователь]", currentUserName);
        }
        if (str.Contains("[Дата печати]"))
        {
          string newValue = DateTime.Now.ToString(DateTime.Now.ToString("g"));
          str = str.Replace("[Дата печати]", newValue);
        }
        if (str.Contains("[Имя устройства]"))
        {
          DateTime.Now.ToString(DateTime.Now.ToString("g"));
          str = str.Replace("[Имя устройства]", Environment.MachineName);
        }
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings settingsWithSubstitutes = watermarkSettings.Clone() as Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings;
        settingsWithSubstitutes.Text = str;
        return settingsWithSubstitutes;
      }

      private void LoadWatermarkSettings()
      {
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings1 = new Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings();
        string watermarkSettings2 = this.pdmSystemService.GetWatermarkSettings();
        if (!string.IsNullOrEmpty(watermarkSettings2))
        {
          using (XmlReader reader = XmlReader.Create((TextReader) new StringReader(watermarkSettings2)))
            watermarkSettings1 = new DataContractSerializer(typeof (Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings)).ReadObject(reader) as Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings;
        }
        this._cachedSettings = watermarkSettings1;
        this._cachedSettings.Freeze();
      }
    }
}
