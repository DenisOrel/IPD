
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.OptimalLayoutCreator




using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class OptimalLayoutCreator
    {
      private KnownPaperFormat _mainFormat;
      private KnownPaperFormat _portraitInternalFormat;
      private KnownPaperFormat _landscapeInternalFormat;

      public OptimalLayoutCreator(
        KnownPaperFormat mainFormat,
        KnownPaperFormat internalFormat,
        OptimalLayoutCreator.CreationOptions creationOption = OptimalLayoutCreator.CreationOptions.Fast)
      {
        this._mainFormat = mainFormat;
        this._portraitInternalFormat = KnownPaperFormats.GetFormat(internalFormat.BaseName, true);
        this._landscapeInternalFormat = KnownPaperFormats.GetFormat(internalFormat.BaseName, false);
        this.CurrentCreationOption = creationOption;
      }

      public OptimalLayoutCreator.CreationOptions CurrentCreationOption { get; set; }

      public List<FormatLocation> CreateOptimalLayout()
      {
        if (this._portraitInternalFormat.Width > this._mainFormat.Width || this._portraitInternalFormat.Height > this._mainFormat.Height)
          return new List<FormatLocation>();
        Point startCoordinates = new Point(0, 0);
        List<FormatLocation> optimalLayout = (List<FormatLocation>) null;
        switch (this.CurrentCreationOption)
        {
          case OptimalLayoutCreator.CreationOptions.Fast:
            optimalLayout = this.FastCreateOptimalLayout(startCoordinates);
            break;
          case OptimalLayoutCreator.CreationOptions.Long:
            optimalLayout = this.LongCreateOptimalLayout(startCoordinates);
            break;
        }
        return optimalLayout;
      }

      private List<FormatLocation> ArrangeFormatsOnLine(
        KnownPaperFormat internalFormat,
        Point startCoordinates)
      {
        if (startCoordinates.X >= this._mainFormat.Width || startCoordinates.Y >= this._mainFormat.Height)
          return new List<FormatLocation>();
        Point point = startCoordinates;
        List<FormatLocation> formatLocationList = new List<FormatLocation>();
        for (; point.X + internalFormat.Width <= this._mainFormat.Width && point.Y + internalFormat.Height <= this._mainFormat.Height; point = new Point(point.X, point.Y + internalFormat.Height))
          formatLocationList.Add(new FormatLocation()
          {
            Left = point.X,
            Top = point.Y,
            Format = internalFormat
          });
        return formatLocationList;
      }

      private List<FormatLocation> FastCreateOptimalLayout(Point startCoordinates)
      {
        List<FormatLocation> optimalLayout = new List<FormatLocation>();
        int num1 = this._mainFormat.Width / this._portraitInternalFormat.Width * (this._mainFormat.Height / this._portraitInternalFormat.Height);
        int num2 = this._mainFormat.Width / this._landscapeInternalFormat.Width * (this._mainFormat.Height / this._landscapeInternalFormat.Height);
        if (num1 == 0 && num2 == 0)
          return optimalLayout;
        KnownPaperFormat internalFormat = num2 > num1 ? this._landscapeInternalFormat : this._portraitInternalFormat;
        for (; startCoordinates.X < this._mainFormat.Width; startCoordinates = new Point(startCoordinates.X + internalFormat.Width, startCoordinates.Y))
        {
          List<FormatLocation> collection = this.ArrangeFormatsOnLine(internalFormat, startCoordinates);
          optimalLayout.AddRange((IEnumerable<FormatLocation>) collection);
        }
        return optimalLayout;
      }

      private List<FormatLocation> LongCreateOptimalLayout(Point startCoordinates)
      {
        if (startCoordinates.X >= this._mainFormat.Width || startCoordinates.Y >= this._mainFormat.Height)
          return new List<FormatLocation>();
        List<FormatLocation> source1 = this.ArrangeFormatsOnLine(this._portraitInternalFormat, startCoordinates);
        if (source1.Any<FormatLocation>())
        {
          List<FormatLocation> optimalLayout = this.LongCreateOptimalLayout(new Point(startCoordinates.X + source1.First<FormatLocation>().Format.Width, startCoordinates.Y));
          source1.AddRange((IEnumerable<FormatLocation>) optimalLayout);
        }
        List<FormatLocation> source2 = this.ArrangeFormatsOnLine(this._landscapeInternalFormat, startCoordinates);
        if (source2.Any<FormatLocation>())
        {
          List<FormatLocation> optimalLayout = this.LongCreateOptimalLayout(new Point(startCoordinates.X + source2.First<FormatLocation>().Format.Width, startCoordinates.Y));
          source2.AddRange((IEnumerable<FormatLocation>) optimalLayout);
        }
        return source2.Count > 1 && source2.Count == source1.Count ? (source2.First<FormatLocation>().Format.IsPortait != source2.Last<FormatLocation>().Format.IsPortait ? source1 : source2) : (source2.Count <= source1.Count ? source1 : source2);
      }

      public enum CreationOptions
      {
        Fast,
        Long,
      }
    }
}
