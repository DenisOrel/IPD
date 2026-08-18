using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace Intermech.ComparisonPlugins.PDFComparison.Common
{
    public class SamplePage : IDisposable
    {
      public static readonly SamplePage Empty = new SamplePage(0, (Image) new Bitmap(1, 1, PixelFormat.Format32bppArgb));

      public int Number { get; }

      public Image Image { get; }

      public SamplePage(int number, Image image)
      {
        this.Number = number;
        this.Image = image;
      }

      public void Dispose() => this.Image?.Dispose();
    }
}
