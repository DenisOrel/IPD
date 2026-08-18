
// Type: Intermech.PdfPrintCenter.FormatLocation




using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using iTextSharp.text;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter
{
    internal class FormatLocation
    {
      public int Left;
      public int Top;

      public bool IsRotate => this.Format.Width > this.Format.Height;

      public KnownPaperFormat Format { get; set; }

      public double LeftD => (double) this.Left * (360.0 / (double) sbyte.MaxValue);

      public double TopD => (double) this.Top * (360.0 / (double) sbyte.MaxValue);

      public string BaseName => this.Format.BaseName;

      public bool IsSameSize(Rectangle rect)
      {
        bool isRotate = this.IsRotate;
        return PdfUtils.IsSameSize(this.Format.WidthF, this.Format.HeightF, rect.Width, rect.Height, ref isRotate);
      }

      public override bool Equals(object obj)
      {
        return obj is FormatLocation formatLocation && this.Left == formatLocation.Left && this.Top == formatLocation.Top && this.Format.Equals((object) formatLocation.Format);
      }

      public override int GetHashCode()
      {
        return ((1051042622 * -1521134295 + this.Left.GetHashCode()) * -1521134295 + this.Top.GetHashCode()) * -1521134295 + EqualityComparer<KnownPaperFormat>.Default.GetHashCode(this.Format);
      }

      public override string ToString() => this.Format.FullName;
    }
}
