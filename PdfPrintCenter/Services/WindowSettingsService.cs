// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Services.WindowSettingsService
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.Interfaces;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;


namespace Intermech.PdfPrintCenter.Services
{
    internal class WindowSettingsService : IWindowSettingsService
    {
        private readonly object syncRoot = new object();
        private IPDMSystemService pdmSystemService;
        private Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings _cachedSettings;

        public WindowSettingsService(IPDMSystemService pdmSystemService)
        {
            this.pdmSystemService = pdmSystemService;
        }

        public Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings GetWindowSettings()
        {
            lock (this.syncRoot)
            {
                if (this._cachedSettings == null)
                    this.LoadCachedSettings();
                return this._cachedSettings;
            }
        }

        public void PutWindowSettings(Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowSettings)
        {
            windowSettings.RequireFrozen();
            lock (this.syncRoot)
            {
                using (StringWriter output = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create((TextWriter)output))
                    {
                        new DataContractSerializer(typeof(Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings)).WriteObject(writer, windowSettings.Clone());
                        writer.Flush();
                        this.pdmSystemService.PutWindowSettings(output.ToString());
                    }
                }
                this._cachedSettings = windowSettings;
            }
        }

        private void LoadCachedSettings()
        {
            Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowSettings1 = new Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings();
            string windowSettings2 = this.pdmSystemService.GetWindowSettings();
            if (!string.IsNullOrEmpty(windowSettings2))
            {
                using (XmlReader reader = XmlReader.Create((TextReader)new StringReader(windowSettings2)))
                    windowSettings1 = new DataContractSerializer(typeof(Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings)).ReadObject(reader) as Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings;
            }
            this._cachedSettings = windowSettings1;
            this._cachedSettings.Freeze();
        }
    }
}
