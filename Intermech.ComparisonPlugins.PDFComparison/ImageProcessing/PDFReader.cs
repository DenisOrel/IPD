
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.PDFReader




using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing
{
    public class PDFReader
    {
      private static readonly int _dpi = 96 /*0x60*/;
      private static readonly string _gsDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "gsdll64.dll" : "gsdll32.dll");

      public static List<Image> ExtractImages(byte[] data)
      {
        List<Image> images = new List<Image>();
        GhostscriptVersionInfo versionInfo = new GhostscriptVersionInfo(PDFReader._gsDllPath);
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
          using (GhostscriptRasterizer ghostscriptRasterizer = new GhostscriptRasterizer())
          {
            ghostscriptRasterizer.Open((Stream) memoryStream, versionInfo, false);
            for (int pageNumber = 1; pageNumber <= ghostscriptRasterizer.PageCount; ++pageNumber)
            {
              using (Image page = ghostscriptRasterizer.GetPage(PDFReader._dpi, pageNumber))
                images.Add(PDFReader.ConvertToGrayscale(page));
            }
          }
        }
        return images;
      }

      private static Image ConvertToGrayscale(Image image)
      {
        Image image1 = (Image) new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
        ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
        {
          new float[5]{ 0.3f, 0.3f, 0.3f, 0.0f, 0.0f },
          new float[5]{ 0.59f, 0.59f, 0.59f, 0.0f, 0.0f },
          new float[5]{ 0.11f, 0.11f, 0.11f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, 1f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
        });
        ImageAttributes imageAttr = new ImageAttributes();
        imageAttr.SetColorMatrix(newColorMatrix);
        Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
        using (Graphics graphics = Graphics.FromImage(image1))
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
        return image1;
      }
    }
}
