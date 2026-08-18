
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.PdfWriterUtils




using Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class PdfWriterUtils
    {
      public static void PrintWatermark(
        this PdfWriter pdfWriter,
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings,
        iTextSharp.text.Rectangle pageRect)
      {
        if (watermarkSettings == null || watermarkSettings.Text == "")
          return;
        PdfContentByte pdfContentByte = watermarkSettings.Layer == WatermarkLayer.Under ? pdfWriter.DirectContentUnder : pdfWriter.DirectContent;
        BaseFont font = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), "Identity-H", false);
        pdfContentByte.BeginText();
        pdfContentByte.SetColorFill(BaseColor.LIGHT_GRAY);
        pdfContentByte.SetFontAndSize(font, (float) watermarkSettings.FontSize);
        float widthPoint1 = font.GetWidthPoint(watermarkSettings.Text, (float) watermarkSettings.FontSize);
        float widthPoint2 = font.GetWidthPoint("W", (float) watermarkSettings.FontSize);
        PointF pointF = new PointF();
        switch (watermarkSettings.Position)
        {
          case WatermarkPosition.DownLeft:
            pdfContentByte.ShowTextAligned(0, watermarkSettings.Text, widthPoint2, widthPoint2, (float) watermarkSettings.Angle);
            break;
          case WatermarkPosition.DownRight:
            pdfContentByte.ShowTextAligned(0, watermarkSettings.Text, pageRect.Width - widthPoint2 - widthPoint1, widthPoint2, (float) watermarkSettings.Angle);
            break;
          case WatermarkPosition.UpLeft:
            pdfContentByte.ShowTextAligned(0, watermarkSettings.Text, widthPoint2, pageRect.Height - widthPoint2 * 2f, (float) watermarkSettings.Angle);
            break;
          case WatermarkPosition.UpRight:
            pdfContentByte.ShowTextAligned(0, watermarkSettings.Text, pageRect.Width - widthPoint2 - widthPoint1, pageRect.Height - widthPoint2 * 2f, (float) watermarkSettings.Angle);
            break;
          case WatermarkPosition.Tile:
            for (pointF.Y = 0.0f; (double) pointF.Y < (double) pageRect.Height; pointF.Y += widthPoint1)
            {
              for (pointF.X = 0.0f; (double) pointF.X < (double) pageRect.Width; pointF.X += widthPoint1)
                pdfContentByte.ShowTextAligned(1, watermarkSettings.Text, pointF.X, pointF.Y, (float) watermarkSettings.Angle);
            }
            break;
        }
        pdfContentByte.EndText();
      }
    }
}
