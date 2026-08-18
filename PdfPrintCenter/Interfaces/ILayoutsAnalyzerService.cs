using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System.Collections.Generic;

namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface ILayoutsAnalyzerService
    {
        string FindCommonPrinter(List<KnownPaperFormat> formats);

        string FindFirstAptPrinter(KnownPaperFormat format);

        string FindMaxAptFormat(List<KnownPaperFormat> formats);

        LayoutDescriptor FindMinAptLayout(KnownPaperFormat format);
    }
}
