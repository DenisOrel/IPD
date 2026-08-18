// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Services.LayoutsAnalyzerService
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.PdfPrintCenter.Services
{
    internal class LayoutsAnalyzerService : ILayoutsAnalyzerService
    {
        private readonly object syncRoot = new object();
        private ILayoutSettingsService layoutSettingsService;
        private IPrintersSettingsService printersSettingsService;

        public LayoutsAnalyzerService(
          ILayoutSettingsService layoutSettingsService,
          IPrintersSettingsService printersSettingsService)
        {
            this.layoutSettingsService = layoutSettingsService;
            this.printersSettingsService = printersSettingsService;
        }

        public string FindCommonPrinter(List<KnownPaperFormat> formats)
        {
            lock (this.syncRoot)
            {
                IDictionary<string, List<string>> formatsToPrinters = this.printersSettingsService.GetPrintersSettings().FormatsToPrinters;
                foreach (string key in (IEnumerable<string>)this.printersSettingsService.GetPrintersSettings().PrintersOrder)
                {
                    if (formatsToPrinters.ContainsKey(key))
                    {
                        List<string> availableFormats = formatsToPrinters[key];
                        if (formats.All<KnownPaperFormat>((Func<KnownPaperFormat, bool>)(format => availableFormats.Contains(format.BaseName))))
                            return key;
                    }
                }
            }
            return (string)null;
        }

        public string FindFirstAptPrinter(KnownPaperFormat format)
        {
            lock (this.syncRoot)
            {
                IDictionary<string, List<string>> formatsToPrinters = this.printersSettingsService.GetPrintersSettings().FormatsToPrinters;
                foreach (string key in (IEnumerable<string>)this.printersSettingsService.GetPrintersSettings().PrintersOrder)
                {
                    if (formatsToPrinters.ContainsKey(key) && formatsToPrinters[key].Contains(format.BaseName))
                        return key;
                }
            }
            return (string)null;
        }

        public string FindMaxAptFormat(List<KnownPaperFormat> formats)
        {
            int val1_1 = -1;
            int val1_2 = -1;
            foreach (KnownPaperFormat format in formats)
            {
                val1_1 = Math.Max(val1_1, Math.Min(format.Width, format.Height));
                val1_2 = Math.Max(val1_2, Math.Max(format.Height, format.Width));
            }
            lock (this.syncRoot)
            {
                List<KnownPaperFormat> list = KnownPaperFormats.Formats.ToList<KnownPaperFormat>();
                list.Sort((Comparison<KnownPaperFormat>)((lhs, rhs) => lhs.Width == rhs.Width ? lhs.Height - rhs.Height : lhs.Width - rhs.Width));
                foreach (KnownPaperFormat format in list.Where<KnownPaperFormat>((Func<KnownPaperFormat, bool>)(format => format.IsPortait)))
                {
                    string firstAptPrinter = this.FindFirstAptPrinter(format);
                    if (format.Width >= val1_1 && format.Height >= val1_2 && !string.IsNullOrEmpty(firstAptPrinter))
                        return format.BaseName;
                }
            }
            return "";
        }

        public LayoutDescriptor FindMinAptLayout(KnownPaperFormat format)
        {
            lock (this.syncRoot)
            {
                List<LayoutDescriptor> layoutDescriptorList = this.layoutSettingsService.LoadAllLayouts();
                layoutDescriptorList.Sort((Comparison<LayoutDescriptor>)((lhs, rhs) =>
                {
                    Size size1 = new Size(Math.Min(lhs.Width, lhs.Height), Math.Max(lhs.Width, lhs.Height));
                    Size size2 = new Size(Math.Min(rhs.Width, rhs.Height), Math.Max(rhs.Width, rhs.Height));
                    return size1.Width == size2.Height ? size1.Height - size2.Height : size1.Width - size2.Width;
                }));
                LayoutDescriptor minAptLayout = (LayoutDescriptor)null;
                FormatLocation formatLocation1 = (FormatLocation)null;
                foreach (LayoutDescriptor layoutDescriptor in layoutDescriptorList)
                {
                    LayoutDescriptor layout = layoutDescriptor;
                    if (this.printersSettingsService.GetDefaultPrintersSettings().FormatsToPrinters.Values.Any<List<string>>((Func<List<string>, bool>)(item => item.Contains(layout.MainFormat.BaseName))))
                    {
                        layout.InternalFormats.Sort((Comparison<FormatLocation>)((lhs, rhs) => lhs.Format.Width == rhs.Format.Width ? lhs.Format.Height - rhs.Format.Height : lhs.Format.Width - rhs.Format.Width));
                        FormatLocation formatLocation2 = layout.InternalFormats.FirstOrDefault<FormatLocation>((Func<FormatLocation, bool>)(internalFormat => internalFormat.Format.Width >= format.Width && internalFormat.Format.Height >= format.Height));
                        if (formatLocation2 != null && (formatLocation1 == null || formatLocation2.Format.Width < formatLocation1.Format.Width && formatLocation2.Format.Height < formatLocation1.Format.Height))
                        {
                            formatLocation1 = formatLocation2;
                            minAptLayout = layout;
                        }
                    }
                }
                return minAptLayout;
            }
        }
    }
}
