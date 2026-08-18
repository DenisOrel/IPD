
// Type: Intermech.PdfPrintCenter.Services.PrintersSettingsService




using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Xml;


namespace Intermech.PdfPrintCenter.Services
{
    internal class PrintersSettingsService : IPrintersSettingsService
    {
      private static readonly string WordPattern = "[^:;,]+";
      private static readonly string[] AcceptableFormatWords = new string[3]
      {
        "(ISO)",
        "Transverse",
        "Rotated"
      };
      private readonly object syncRoot = new object();
      private IPDMSystemService pdmSystemService;
      private Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings _defaultCachedSettings;
      private Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings _cachedSettings;

      public PrintersSettingsService(IPDMSystemService pdmSystemService)
      {
        this.pdmSystemService = pdmSystemService;
      }

      public Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings GetDefaultPrintersSettings()
      {
        lock (this.syncRoot)
        {
          if (this._defaultCachedSettings == null)
          {
            this._defaultCachedSettings = new Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings()
            {
              FormatsToPrinters = (IDictionary<string, List<string>>) this.GetDefaultFormatsToPrintersSettings(),
              PrintersOrder = (IList<string>) this.GetSortedPrintersList()
            };
            this._defaultCachedSettings.Freeze();
          }
          return this._defaultCachedSettings;
        }
      }

      public Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings GetPrintersSettings()
      {
        lock (this.syncRoot)
        {
          if (this._cachedSettings == null)
            this.LoadCachedSettings();
          return this._cachedSettings;
        }
      }

      public void PutPrintersSettings(Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings)
      {
        printersSettings.RequireFrozen();
        lock (this.syncRoot)
        {
          using (StringWriter output = new StringWriter())
          {
            using (XmlWriter writer = XmlWriter.Create((TextWriter) output))
            {
              new DataContractSerializer(typeof (Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings)).WriteObject(writer, printersSettings.Clone());
              writer.Flush();
              this.pdmSystemService.PutPrintersSettings(output.ToString());
            }
          }
          this._cachedSettings = printersSettings;
        }
      }

      private Dictionary<string, List<string>> GetDefaultFormatsToPrintersSettings()
      {
        Dictionary<string, List<string>> printersSettings = new Dictionary<string, List<string>>();
        foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("select * from win32_printer where PrinterState is not null").Get())
        {
          string key = managementBaseObject["Name"].ToString();
          string[] strArray = managementBaseObject["PrinterPaperNames"] as string[];
          printersSettings.Add(key, new List<string>());
          if (strArray != null)
          {
            foreach (string formatName in strArray)
            {
              string fullFormatName = this.RemoveAcceptableFormatWords(formatName);
              if (KnownPaperFormats.IsFormatName(fullFormatName))
              {
                printersSettings[key].Add(KnownPaperFormats.GetFormat(fullFormatName).BaseName);
              }
              else
              {
                string str = KnownPaperFormats.AcceptsAllSizesOfFormat(formatName);
                if (str != null)
                {
                  foreach (int num in Enumerable.Range(1, 10))
                    printersSettings[key].Add($"{str}{num}");
                }
              }
            }
          }
        }
        return printersSettings;
      }

      private List<string> GetSortedPrintersList()
      {
        List<string> sortedPrintersList = new List<string>();
        foreach (object installedPrinter in PrinterSettings.InstalledPrinters)
        {
          if (new PrinterSettings()
          {
            PrinterName = (installedPrinter as string)
          }.IsValid)
            sortedPrintersList.Add(installedPrinter as string);
        }
        sortedPrintersList.Sort();
        return sortedPrintersList;
      }

      private string RemoveAcceptableFormatWords(string formatName)
      {
        foreach (string acceptableFormatWord in PrintersSettingsService.AcceptableFormatWords)
          formatName = formatName.Replace(acceptableFormatWord, "");
        return formatName;
      }

      private void LoadCachedSettings()
      {
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings1 = new Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings();
        string printersSettings2 = this.pdmSystemService.GetPrintersSettings();
        if (!string.IsNullOrEmpty(printersSettings2))
        {
          using (XmlReader reader = XmlReader.Create((TextReader) new StringReader(printersSettings2)))
            printersSettings1 = new DataContractSerializer(typeof (Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings)).ReadObject(reader) as Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings;
        }
        else
          printersSettings1 = this.GetDefaultPrintersSettings().Clone() as Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings;
        this._cachedSettings = printersSettings1;
        this._cachedSettings.Freeze();
      }
    }
}
