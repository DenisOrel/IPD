// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.PdfViewerUtils
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using PdfiumViewer;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class PdfViewerUtils
    {
        public static void GetCorrectPages(this PdfDocument document, List<PageInterval> pages)
        {
            for (int pageCount = document.PageCount; pageCount > pages.Last<PageInterval>().End; --pageCount)
                document.DeletePage(pageCount - 1);
            for (int index1 = pages.Count - 1; index1 > 0; --index1)
            {
                int num1 = pages[index1].Begin - 1;
                int num2 = pages[index1 - 1].End + 1;
                for (int index2 = num1; index2 >= num2; --index2)
                    document.DeletePage(index2 - 1);
            }
            for (int index = pages.First<PageInterval>().Begin - 1; index > 0; --index)
                document.DeletePage(index - 1);
        }
    }
}
