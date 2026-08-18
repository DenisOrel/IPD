
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators.ConcreteLayoutDocCreator




using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators
{
    internal class ConcreteLayoutDocCreator : DocCreator
    {
      private Dictionary<string, HashSet<ConcreteLayoutDocCreator.PageToLayoutInfo>> _pagesToFormats = new Dictionary<string, HashSet<ConcreteLayoutDocCreator.PageToLayoutInfo>>();

      public ConcreteLayoutDocCreator(LayoutDescriptor layoutDescriptor)
        : base((IPdfPageProducer) layoutDescriptor)
      {
        foreach (FormatLocation internalFormat in this.InternalFormats)
        {
          if (!this._pagesToFormats.ContainsKey(internalFormat.BaseName))
            this._pagesToFormats.Add(internalFormat.BaseName, new HashSet<ConcreteLayoutDocCreator.PageToLayoutInfo>());
        }
      }

      private List<FormatLocation> InternalFormats
      {
        get => (this.PdfPageProducer as LayoutDescriptor).InternalFormats;
      }

      private float Width => (this.PdfPageProducer as LayoutDescriptor).WidthF;

      private float Height => (this.PdfPageProducer as LayoutDescriptor).HeightF;

      public override AddingPagesToLayoutResult CreateDocument(
        List<string> inputFiles,
        List<string> ranges,
        int copies,
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark = null)
      {
        this.EnableUnethicalReading();
        AsItIsDocCreator asItIsDocCreator1 = new AsItIsDocCreator(new LayoutAsItIs());
        using (MemoryStream os = new MemoryStream())
        {
          using (Document document1 = new Document())
          {
            AddingPagesToLayoutResult document2 = new AddingPagesToLayoutResult();
            for (int index = 0; index < inputFiles.Count; ++index)
            {
              AsItIsDocCreator asItIsDocCreator2 = asItIsDocCreator1;
              List<string> inputFiles1 = new List<string>();
              inputFiles1.Add(inputFiles[index]);
              List<string> ranges1 = new List<string>();
              ranges1.Add(ranges[index]);
              int copies1 = copies;
              Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark1 = watermark;
              AddingPagesToLayoutResult document3 = asItIsDocCreator2.CreateDocument(inputFiles1, ranges1, copies1, watermark1);
              if (document3 == null || document3.PdfWithLayout == null)
                return document3;
              PdfReader pdfReader = new PdfReader(document3.PdfWithLayout);
              int pagesInIntervals = PageIntervalsUtils.GetNumberOfPagesInIntervals(PageIntervalsUtils.GetPages(ranges[index]));
              SizeF pagesSize = this.GetPagesSize(pdfReader);
              if (!this.TryDistributePages(pdfReader, pagesInIntervals, pagesSize))
              {
                document2.AddBadRanges(ranges[index]);
                return document2;
              }
            }
            if (document2.BadRanges.Any<string>())
              return document2;
            PdfWriter instance = PdfWriter.GetInstance(document1, (Stream) os);
            instance.CloseStream = false;
            document1.Open();
            while (this._pagesToFormats.Any<KeyValuePair<string, HashSet<ConcreteLayoutDocCreator.PageToLayoutInfo>>>((Func<KeyValuePair<string, HashSet<ConcreteLayoutDocCreator.PageToLayoutInfo>>, bool>) (item => item.Value.Count > 0)))
            {
              document1.SetPageSize(new iTextSharp.text.Rectangle(this.Width, this.Height));
              document1.NewPage();
              foreach (FormatLocation internalFormat in this.InternalFormats)
              {
                if (this._pagesToFormats[internalFormat.BaseName].Count != 0)
                {
                  PdfReader pdfReader = this._pagesToFormats[internalFormat.BaseName].First<ConcreteLayoutDocCreator.PageToLayoutInfo>().PdfReader;
                  int pageNumber = this._pagesToFormats[internalFormat.BaseName].First<ConcreteLayoutDocCreator.PageToLayoutInfo>().PageNumber;
                  PdfImportedPage importedPage = instance.GetImportedPage(pdfReader, pageNumber);
                  this.AddTemplateToDocument(instance.DirectContent, internalFormat, importedPage);
                  this._pagesToFormats[internalFormat.BaseName].Remove(this._pagesToFormats[internalFormat.BaseName].First<ConcreteLayoutDocCreator.PageToLayoutInfo>());
                }
              }
            }
            document1.Close();
          }
          return new AddingPagesToLayoutResult(os.ToArray());
        }
      }

      private void AddTemplateToDocument(
        PdfContentByte pdfContentByte,
        FormatLocation layout,
        PdfImportedPage page)
      {
        switch (this.GetPageRotation(layout, page))
        {
          case 0:
          case 180:
            float num = this.Height - layout.Format.HeightF;
            pdfContentByte.AddTemplate((PdfTemplate) page, layout.LeftD, -layout.TopD + (double) num, false);
            break;
          case 90:
          case 270:
            pdfContentByte.AddTemplate((PdfTemplate) page, 0.0, -1.0, 1.0, 0.0, layout.LeftD, (double) this.Height - layout.TopD);
            break;
        }
      }

      private int GetPageRotation(FormatLocation layout, PdfImportedPage page)
      {
        int num1 = layout.Format.Width < layout.Format.Height ? 1 : 0;
        bool flag = (double) page.Width < (double) page.Height && page.Rotation == 0 || (double) page.Width >= (double) page.Height && page.Rotation == 90;
        int pageRotation = page.Rotation;
        int num2 = flag ? 1 : 0;
        if ((num1 ^ num2) != 0)
          pageRotation = Math.Abs(pageRotation - 90);
        return pageRotation;
      }

      private SizeF GetPagesSize(PdfReader pdfReader)
      {
        iTextSharp.text.Rectangle pageSize = pdfReader.GetPageSize(1);
        return new SizeF((float) (int) Math.Round((double) pageSize.Width / (360.0 / (double) sbyte.MaxValue)), (float) (int) Math.Round((double) pageSize.Height / (360.0 / (double) sbyte.MaxValue)));
      }

      private bool TryDistributePages(
        PdfReader pdfReader,
        int numberOfPages,
        SizeF pagesSize,
        bool isMinAptFormat = true)
      {
        KnownPaperFormat aptFormat = LayoutsUtils.FindAptPageFormat(pagesSize);
        if (aptFormat == null)
          return false;
        FormatLocation formatLocation = this.InternalFormats.FirstOrDefault<FormatLocation>((Func<FormatLocation, bool>) (format => format.Format.BaseName == aptFormat.BaseName));
        if (formatLocation == null)
          return this.TryDistributePages(pdfReader, numberOfPages, new SizeF((float) (aptFormat.Width + 1), (float) (aptFormat.Height + 1)), false);
        for (int pageNumber = 1; pageNumber <= numberOfPages; ++pageNumber)
          this._pagesToFormats[formatLocation.BaseName].Add(new ConcreteLayoutDocCreator.PageToLayoutInfo(pdfReader, pageNumber, isMinAptFormat));
        return true;
      }

      private class PageToLayoutInfo
      {
        public PageToLayoutInfo(PdfReader pdfReader, int pageNumber, bool isAptFormat)
        {
          this.PdfReader = pdfReader;
          this.PageNumber = pageNumber;
          this.IsAptFormat = isAptFormat;
        }

        public PdfReader PdfReader { get; private set; }

        public int PageNumber { get; private set; }

        public bool IsAptFormat { get; private set; }

        public override bool Equals(object obj)
        {
          return obj is ConcreteLayoutDocCreator.PageToLayoutInfo pageToLayoutInfo && EqualityComparer<PdfReader>.Default.Equals(this.PdfReader, pageToLayoutInfo.PdfReader) && this.PageNumber == pageToLayoutInfo.PageNumber && this.IsAptFormat == pageToLayoutInfo.IsAptFormat;
        }

        public override int GetHashCode()
        {
          return ((-298511436 * -1521134295 + EqualityComparer<PdfReader>.Default.GetHashCode(this.PdfReader)) * -1521134295 + this.PageNumber.GetHashCode()) * -1521134295 + this.IsAptFormat.GetHashCode();
        }
      }
    }
}
