// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridCell
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Syncfusion.Pdf.Grid
{
    public class PdfGridCell
    {
      private bool m_bIsCellMergeContinue;
      private bool m_bIsCellMergeStart;
      private bool m_bIsRowMergeContinue;
      private bool m_bIsRowMergeStart;
      private int m_colSpan;
      private bool m_finsh;
      private PdfStringFormat m_format;
      private float m_height;
      private PdfGridImagePosition m_imagePosition;
      private string m_remainingString;
      private PdfGridRow m_row;
      private int m_rowSpan;
      private PdfGridCellStyle m_style;
      private object m_value;
      private float m_width;

      public PdfGridCell()
      {
        this.m_width = float.MinValue;
        this.m_height = float.MinValue;
        this.m_finsh = true;
        this.m_imagePosition = PdfGridImagePosition.Stretch;
        this.m_rowSpan = 1;
        this.m_colSpan = 1;
      }

      public PdfGridCell(PdfGridRow row)
        : this()
      {
        this.m_row = row;
      }

      private RectangleF AdjustContentLayoutArea(RectangleF bounds)
      {
        if (this.m_value is PdfGrid)
        {
          SizeF size = (this.m_value as PdfGrid).Size;
          bounds.Width -= this.m_row.Grid.Style.CellPadding.Right + this.m_row.Grid.Style.CellPadding.Left;
          bounds.Height -= this.m_row.Grid.Style.CellPadding.Bottom + this.m_row.Grid.Style.CellPadding.Top;
          if (this.StringFormat.Alignment == PdfTextAlignment.Center)
          {
            bounds.X += this.m_row.Grid.Style.CellPadding.Left + (float) (((double) bounds.Width - (double) size.Width) / 2.0);
            bounds.Y += this.m_row.Grid.Style.CellPadding.Top + (float) (((double) bounds.Height - (double) size.Height) / 2.0);
            return bounds;
          }
          if (this.StringFormat.Alignment == PdfTextAlignment.Left)
          {
            bounds.X += this.m_row.Grid.Style.CellPadding.Left;
            bounds.Y += this.m_row.Grid.Style.CellPadding.Top;
            return bounds;
          }
          if (this.StringFormat.Alignment == PdfTextAlignment.Right)
          {
            bounds.X += this.m_row.Grid.Style.CellPadding.Left + (bounds.Width - size.Width);
            bounds.Y += this.m_row.Grid.Style.CellPadding.Top;
          }
          return bounds;
        }
        bounds.X += this.m_row.Grid.Style.CellPadding.Left;
        bounds.Y += this.m_row.Grid.Style.CellPadding.Top;
        bounds.Width -= this.m_row.Grid.Style.CellPadding.Right + this.m_row.Grid.Style.CellPadding.Left;
        bounds.Height -= this.m_row.Grid.Style.CellPadding.Bottom + this.m_row.Grid.Style.CellPadding.Top;
        return bounds;
      }

      private RectangleF AdjustOuterLayoutArea(RectangleF bounds, PdfGraphics g)
      {
        bool flag = false;
        float cellSpacing = this.Row.Grid.Style.CellSpacing;
        if ((double) cellSpacing > 0.0)
          bounds = new RectangleF(bounds.X + cellSpacing, bounds.Y + cellSpacing, bounds.Width - cellSpacing, bounds.Height - cellSpacing);
        int index1 = this.Row.Cells.IndexOf(this);
        if (this.ColumnSpan > 1 || this.Row.RowOverflowIndex > 0 && index1 == this.Row.RowOverflowIndex + 1 && this.m_bIsCellMergeContinue)
        {
          int columnSpan = this.ColumnSpan;
          if (columnSpan == 1 && this.m_bIsCellMergeContinue)
          {
            for (int index2 = index1 + 1; index2 < this.Row.Grid.Columns.Count && this.Row.Cells[index2].m_bIsCellMergeContinue; ++index2)
              ++columnSpan;
          }
          float num1 = 0.0f;
          for (int index3 = index1; index3 < index1 + columnSpan; ++index3)
          {
            if (this.Row.Grid.Style.AllowHorizontalOverflow)
            {
              double num2 = (double) bounds.X + (double) num1 + (double) this.Row.Grid.Columns[index3].Width;
              SizeF sizeF = this.Row.Grid.Size;
              double width1 = (double) sizeF.Width;
              sizeF = g.ClientSize;
              double width2 = (double) sizeF.Width;
              double width3;
              if (width1 >= width2)
              {
                sizeF = g.ClientSize;
                width3 = (double) sizeF.Width;
              }
              else
              {
                sizeF = this.Row.Grid.Size;
                width3 = (double) sizeF.Width;
              }
              double num3 = width3;
              if (num2 > num3)
                break;
            }
            num1 += this.Row.Grid.Columns[index3].Width;
          }
          float num4 = num1 - this.Row.Grid.Style.CellSpacing;
          bounds.Width = num4;
        }
        if (this.RowSpan > 1 || this.Row.RowSpanExists)
        {
          int rowSpan = this.RowSpan;
          int num5 = this.Row.Grid.Rows.IndexOf(this.Row);
          if (num5 == -1)
          {
            num5 = this.Row.Grid.Headers.IndexOf(this.Row);
            if (num5 != -1)
              flag = true;
          }
          if (rowSpan == 1 && this.m_bIsCellMergeContinue)
          {
            for (int index4 = num5 + 1; index4 < this.Row.Grid.Rows.Count && (flag ? (this.Row.Grid.Headers[index4].Cells[index1].m_bIsCellMergeContinue ? 1 : 0) : (this.Row.Grid.Rows[index4].Cells[index1].m_bIsCellMergeContinue ? 1 : 0)) != 0; ++index4)
              ++rowSpan;
          }
          float num6 = 0.0f;
          for (int index5 = num5; index5 < num5 + rowSpan; ++index5)
            num6 += flag ? this.Row.Grid.Headers[index5].Height : this.Row.Grid.Rows[index5].Height;
          float num7 = num6 - this.Row.Grid.Style.CellSpacing;
          bounds.Height = num7;
        }
        return bounds;
      }

      private float CalculateWidth()
      {
        int num = this.Row.Cells.IndexOf(this);
        int columnSpan = this.ColumnSpan;
        float width = 0.0f;
        for (int index = 0; index < columnSpan; ++index)
          width += this.Row.Grid.Columns[num + index].Width;
        return width;
      }

      internal PdfStringLayoutResult Draw(
        PdfGraphics graphics,
        RectangleF bounds,
        bool cancelSubsequentSpans)
      {
        PdfStringLayoutResult stringLayoutResult = (PdfStringLayoutResult) null;
        if (cancelSubsequentSpans)
        {
          int num = this.Row.Cells.IndexOf(this);
          for (int index = num + 1; index <= num + this.m_colSpan; ++index)
          {
            this.Row.Cells[index].IsCellMergeContinue = false;
            this.Row.Cells[index].IsRowMergeContinue = false;
          }
          this.m_colSpan = 1;
        }
        if ((this.m_bIsCellMergeContinue || this.m_bIsRowMergeContinue) && (!this.m_bIsCellMergeContinue || !this.Row.Grid.Style.AllowHorizontalOverflow || this.Row.RowOverflowIndex > 0 && this.Row.Cells.IndexOf(this) != this.Row.RowOverflowIndex + 1 || this.Row.RowOverflowIndex == 0 && this.m_bIsCellMergeContinue))
          return stringLayoutResult;
        bounds = this.AdjustOuterLayoutArea(bounds, graphics);
        this.DrawCellBackground(ref graphics, bounds);
        PdfPen textPen = this.GetTextPen();
        PdfBrush textBrush = this.GetTextBrush();
        PdfFont textFont = this.GetTextFont();
        PdfStringFormat stringFormat = this.GetStringFormat();
        RectangleF rectangleF = bounds;
        double height1 = (double) rectangleF.Height;
        SizeF sizeF = graphics.ClientSize;
        double height2 = (double) sizeF.Height;
        if (height1 >= height2)
        {
          if (this.Row.Grid.AllowRowBreakAcrossPages)
          {
            rectangleF.Height -= rectangleF.Y;
            bounds.Height -= bounds.Y;
          }
          else
          {
            ref RectangleF local1 = ref rectangleF;
            sizeF = graphics.ClientSize;
            double height3 = (double) sizeF.Height;
            local1.Height = (float) height3;
            ref RectangleF local2 = ref bounds;
            sizeF = graphics.ClientSize;
            double height4 = (double) sizeF.Height;
            local2.Height = (float) height4;
          }
        }
        rectangleF = this.AdjustContentLayoutArea(rectangleF);
        if (this.m_value is PdfGrid)
        {
          sizeF = (this.m_value as PdfGrid).Size;
          double width1 = (double) sizeF.Width;
          sizeF = rectangleF.Size;
          double width2 = (double) sizeF.Width;
          if (width1 > width2)
            throw new PdfException("Can't draw one or more inner grids, no enough space available for it.");
          PdfGrid grid = this.m_value as PdfGrid;
          grid.IsChildGrid = true;
          grid.ParentCell = this;
          PdfGridLayouter pdfGridLayouter = new PdfGridLayouter(grid);
          PdfLayoutFormat pdfLayoutFormat = (PdfLayoutFormat) new PdfGridLayoutFormat();
          if (this.Row.Grid.LayoutFormat != null)
            pdfLayoutFormat = this.Row.Grid.LayoutFormat;
          else
            pdfLayoutFormat.Layout = PdfLayoutType.Paginate;
          if (graphics.Layer != null)
          {
            PdfLayoutParams pdfLayoutParams = new PdfLayoutParams();
            pdfLayoutParams.Page = graphics.Page as PdfPage;
            pdfLayoutParams.Bounds = rectangleF;
            pdfLayoutParams.Format = pdfLayoutFormat;
            PdfLayoutResult pdfLayoutResult = pdfGridLayouter.Layout(pdfLayoutParams);
            if (pdfLayoutParams.Page != pdfLayoutResult.Page)
            {
              this.Row.NestedGridLayoutResult = pdfLayoutResult;
              ref RectangleF local = ref bounds;
              sizeF = graphics.ClientSize;
              double num = (double) sizeF.Height - (double) bounds.Y;
              local.Height = (float) num;
            }
          }
          else
            new PdfGridLayouter(this.m_value as PdfGrid).Layout(graphics, rectangleF);
        }
        else if (this.m_value is string || this.m_remainingString != null)
        {
          if (this.m_finsh)
          {
            string s = this.m_remainingString == string.Empty ? this.m_remainingString : (string) this.m_value;
            graphics.DrawString(s, textFont, textPen, textBrush, rectangleF, stringFormat);
          }
          else
            graphics.DrawString(this.m_remainingString, textFont, textPen, textBrush, rectangleF, stringFormat);
          stringLayoutResult = graphics.StringLayoutResult;
        }
        if (this.Style.Borders != null)
          this.DrawCellBorders(ref graphics, bounds);
        return stringLayoutResult;
      }

      private void DrawCellBackground(ref PdfGraphics graphics, RectangleF bounds)
      {
        PdfBrush backgroundBrush = this.GetBackgroundBrush();
        if (backgroundBrush != null)
        {
          graphics.Save();
          graphics.DrawRectangle(backgroundBrush, bounds);
          graphics.Restore();
        }
        if (this.Style.BackgroundImage == null)
          return;
        PdfImage backgroundImage = this.Style.BackgroundImage;
        if (this.m_imagePosition == PdfGridImagePosition.Stretch)
          graphics.DrawImage(this.Style.BackgroundImage, bounds);
        else if (this.m_imagePosition == PdfGridImagePosition.Center)
        {
          double num1 = (double) bounds.X + (double) bounds.Width / 2.0;
          float num2 = bounds.Y + bounds.Height / 2f;
          double num3 = (double) (backgroundImage.Width / 2);
          float x = (float) (num1 - num3);
          float y = num2 - (float) (backgroundImage.Height / 2);
          graphics.DrawImage(backgroundImage, x, y, (float) backgroundImage.Width, (float) backgroundImage.Height);
        }
        else if (this.m_imagePosition == PdfGridImagePosition.Fit)
        {
          float width1 = bounds.Width;
          float height1 = bounds.Height;
          float width2 = backgroundImage.PhysicalDimension.Width;
          float height2 = backgroundImage.PhysicalDimension.Height;
          if ((double) height1 <= (double) width1)
          {
            if ((double) height2 > (double) width2)
            {
              float y = bounds.Y;
              float num = height1 / height2;
              float height3 = height1;
              float width3 = width2 * num;
              float x = bounds.X + (float) (((double) bounds.Width - (double) width3) / 2.0);
              graphics.DrawImage(backgroundImage, x, y, width3, height3);
            }
            else
            {
              float x = bounds.X;
              float num = width1 / width2;
              float width4 = width1;
              float height4 = height2 * num;
              float y = bounds.Y + (float) (((double) bounds.Height - (double) height4) / 2.0);
              graphics.DrawImage(backgroundImage, x, y, width4, height4);
            }
          }
          else
          {
            if ((double) height1 <= (double) width1)
              return;
            if ((double) height2 < (double) width2)
            {
              float x = bounds.X;
              float num = width1 / width2;
              float width5 = width1;
              float height5 = height2 * num;
              float y = bounds.Y + (float) (((double) bounds.Height - (double) height5) / 2.0);
              graphics.DrawImage(backgroundImage, x, y, width5, height5);
            }
            else
            {
              float y = bounds.Y;
              float num = height1 / height2;
              float height6 = height1;
              float width6 = width2 * num;
              float x = bounds.X + (float) (((double) bounds.Width - (double) width6) / 2.0);
              graphics.DrawImage(backgroundImage, x, y, width6, height6);
            }
          }
        }
        else
        {
          if (this.m_imagePosition != PdfGridImagePosition.Tile)
            return;
          float x1 = bounds.X;
          float y = bounds.Y;
          double width7 = (double) bounds.Width;
          PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
          double num4;
          double height7;
          for (; (double) y < (double) bounds.Bottom; y = (float) (num4 + height7))
          {
            SizeF physicalDimension;
            double num5;
            double width8;
            for (float x2 = x1; (double) x2 < (double) bounds.Right; x2 = (float) (num5 + width8))
            {
              double num6 = (double) x2;
              physicalDimension = backgroundImage.PhysicalDimension;
              double width9 = (double) physicalDimension.Width;
              if (num6 + width9 > (double) bounds.Right)
              {
                double num7 = (double) y;
                physicalDimension = backgroundImage.PhysicalDimension;
                double height8 = (double) physicalDimension.Height;
                if (num7 + height8 > (double) bounds.Bottom)
                {
                  Rectangle rect = new Rectangle(0, 0, (int) pdfUnitConvertor.ConvertToPixels(bounds.Right - x2, PdfGraphicsUnit.Point), (int) pdfUnitConvertor.ConvertToPixels(bounds.Bottom - y, PdfGraphicsUnit.Point));
                  Bitmap bitmap1 = new Bitmap(backgroundImage.InternalImage, new Size(backgroundImage.Width, backgroundImage.Height));
                  Bitmap bitmap2 = bitmap1.Clone(rect, bitmap1.PixelFormat);
                  MemoryStream memoryStream = new MemoryStream();
                  if (Image.IsAlphaPixelFormat(backgroundImage.InternalImage.PixelFormat) || backgroundImage.InternalImage is Metafile)
                    bitmap2.Save((Stream) memoryStream, ImageFormat.Png);
                  else
                    bitmap2.Save((Stream) memoryStream, ImageFormat.Jpeg);
                  PdfBitmap image = new PdfBitmap((Stream) memoryStream);
                  graphics.DrawImage((PdfImage) image, x2, y);
                  memoryStream.Dispose();
                  bitmap1.Dispose();
                  bitmap2.Dispose();
                  image.Dispose();
                  goto label_36;
                }
              }
              double num8 = (double) x2;
              physicalDimension = backgroundImage.PhysicalDimension;
              double width10 = (double) physicalDimension.Width;
              if (num8 + width10 > (double) bounds.Right)
              {
                Rectangle rect = new Rectangle(0, 0, (int) pdfUnitConvertor.ConvertToPixels(bounds.Right - x2, PdfGraphicsUnit.Point), backgroundImage.Height);
                Bitmap bitmap3 = new Bitmap(backgroundImage.InternalImage, new Size(backgroundImage.Width, backgroundImage.Height));
                Bitmap bitmap4 = bitmap3.Clone(rect, bitmap3.PixelFormat);
                MemoryStream memoryStream = new MemoryStream();
                if (Image.IsAlphaPixelFormat(backgroundImage.InternalImage.PixelFormat) || backgroundImage.InternalImage is Metafile)
                  bitmap4.Save((Stream) memoryStream, ImageFormat.Png);
                else
                  bitmap4.Save((Stream) memoryStream, ImageFormat.Jpeg);
                PdfBitmap image = new PdfBitmap((Stream) memoryStream);
                graphics.DrawImage((PdfImage) image, x2, y);
                memoryStream.Dispose();
                bitmap3.Dispose();
                bitmap4.Dispose();
                image.Dispose();
              }
              else
              {
                double num9 = (double) y;
                physicalDimension = backgroundImage.PhysicalDimension;
                double height9 = (double) physicalDimension.Height;
                if (num9 + height9 > (double) bounds.Bottom)
                {
                  float pixels = pdfUnitConvertor.ConvertToPixels(bounds.Bottom - y, PdfGraphicsUnit.Point);
                  Rectangle rect = new Rectangle(0, 0, backgroundImage.Width, (int) pixels);
                  Bitmap bitmap5 = new Bitmap(backgroundImage.InternalImage, new Size(backgroundImage.Width, backgroundImage.Height));
                  Bitmap bitmap6 = bitmap5.Clone(rect, bitmap5.PixelFormat);
                  MemoryStream memoryStream = new MemoryStream();
                  if (Image.IsAlphaPixelFormat(backgroundImage.InternalImage.PixelFormat) || backgroundImage.InternalImage is Metafile)
                    bitmap6.Save((Stream) memoryStream, ImageFormat.Png);
                  else
                    bitmap6.Save((Stream) memoryStream, ImageFormat.Jpeg);
                  PdfBitmap image = new PdfBitmap((Stream) memoryStream);
                  graphics.DrawImage((PdfImage) image, x2, y);
                  memoryStream.Dispose();
                  bitmap5.Dispose();
                  bitmap6.Dispose();
                  image.Dispose();
                }
                else
                  graphics.DrawImage(backgroundImage, new PointF(x2, y));
              }
    label_36:
              num5 = (double) x2;
              physicalDimension = backgroundImage.PhysicalDimension;
              width8 = (double) physicalDimension.Width;
            }
            num4 = (double) y;
            physicalDimension = backgroundImage.PhysicalDimension;
            height7 = (double) physicalDimension.Height;
          }
        }
      }

      internal void DrawCellBorders(ref PdfGraphics graphics, RectangleF bounds)
      {
        if (this.Row.Grid.Style.BorderOverlapStyle == PdfBorderOverlapStyle.Inside)
        {
          bounds.X += this.Style.Borders.Left.Width;
          bounds.Y += this.Style.Borders.Top.Width;
          bounds.Width -= this.Style.Borders.Right.Width;
          bounds.Height -= this.Style.Borders.Bottom.Width;
        }
        if (this.Style.Borders.IsAll)
        {
          this.SetTransparency(ref graphics, this.m_style.Borders.Left);
          graphics.DrawRectangle(this.m_style.Borders.Left, bounds);
          graphics.Restore();
        }
        else
        {
          PointF point1 = new PointF(bounds.X, bounds.Y + bounds.Height);
          PointF point2 = bounds.Location;
          PdfPen pen1 = this.m_style.Borders.Left;
          if (pen1.IsImmutable)
            pen1 = new PdfPen(this.m_style.Borders.Left.Color, this.m_style.Borders.Left.Width);
          pen1.LineCap = PdfLineCap.Square;
          this.SetTransparency(ref graphics, pen1);
          graphics.DrawLine(pen1, point1, point2);
          graphics.Restore();
          point1 = new PointF(bounds.X + bounds.Width, bounds.Y);
          point2 = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
          PdfPen pen2 = this.m_style.Borders.Right;
          if (pen2.IsImmutable)
            pen2 = new PdfPen(this.m_style.Borders.Right.Color, this.m_style.Borders.Right.Width);
          pen2.LineCap = PdfLineCap.Square;
          this.SetTransparency(ref graphics, pen2);
          graphics.DrawLine(pen2, point1, point2);
          graphics.Restore();
          point1 = bounds.Location;
          point2 = new PointF(bounds.X + bounds.Width, bounds.Y);
          PdfPen pen3 = this.m_style.Borders.Top;
          if (pen3.IsImmutable)
            pen3 = new PdfPen(this.m_style.Borders.Top.Color, this.m_style.Borders.Top.Width);
          pen3.LineCap = PdfLineCap.Square;
          this.SetTransparency(ref graphics, pen3);
          graphics.DrawLine(pen3, point1, point2);
          graphics.Restore();
          point1 = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
          point2 = new PointF(bounds.X, bounds.Y + bounds.Height);
          PdfPen pen4 = this.m_style.Borders.Bottom;
          if (pen4.IsImmutable)
            pen4 = new PdfPen(this.m_style.Borders.Bottom.Color, this.m_style.Borders.Bottom.Width);
          pen4.LineCap = PdfLineCap.Square;
          this.SetTransparency(ref graphics, pen4);
          graphics.DrawLine(pen4, point1, point2);
          graphics.Restore();
        }
      }

      private PdfBrush GetBackgroundBrush()
      {
        return this.Style.BackgroundBrush ?? this.Row.Style.BackgroundBrush ?? this.Row.Grid.Style.BackgroundBrush;
      }

      private PdfGridCell GetNextCell()
      {
        int num = this.m_row.Cells.IndexOf(this);
        return num + 1 <= this.m_row.Cells.Count ? this.m_row.Cells[num + 1] : (PdfGridCell) null;
      }

      private PdfStringFormat GetStringFormat() => this.Style.StringFormat ?? this.StringFormat;

      private PdfBrush GetTextBrush()
      {
        return this.Style.TextBrush ?? this.Row.Style.TextBrush ?? this.Row.Grid.Style.TextBrush ?? PdfBrushes.Black;
      }

      private PdfFont GetTextFont()
      {
        return this.Style.Font ?? this.Row.Style.Font ?? this.Row.Grid.Style.Font ?? PdfDocument.DefaultFont;
      }

      private PdfPen GetTextPen()
      {
        return this.Style.TextPen ?? this.Row.Style.TextPen ?? this.Row.Grid.Style.TextPen;
      }

      internal float MeasureHeight()
      {
        float width = this.CalculateWidth() - (this.m_row.Grid.Style.CellPadding.Right + this.m_row.Grid.Style.CellPadding.Left) - (this.Style.Borders.Left.Width + this.Style.Borders.Right.Width);
        float num = 0.0f;
        PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
        if (this.m_value is string || this.m_remainingString != null)
        {
          string text = (string) this.m_value;
          if (!this.m_finsh)
            text = !string.IsNullOrEmpty(this.m_remainingString) ? this.m_remainingString : (string) this.m_value;
          PdfStringLayoutResult stringLayoutResult = pdfStringLayouter.Layout(text, this.GetTextFont(), this.StringFormat, new SizeF(width, float.MaxValue));
          num = num + stringLayoutResult.ActualSize.Height + (float) (((double) this.Style.Borders.Top.Width + (double) this.Style.Borders.Bottom.Width) * 2.0);
        }
        else if (this.m_value is PdfGrid)
          num = (this.m_value as PdfGrid).Size.Height;
        return num + (this.Row.Grid.Style.CellPadding.Top + this.Row.Grid.Style.CellPadding.Bottom) + this.Row.Grid.Style.CellSpacing;
      }

      private float MeasureWidth()
      {
        float num = 0.0f;
        PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
        if (this.m_value is string)
        {
          PdfStringLayoutResult stringLayoutResult = pdfStringLayouter.Layout((string) this.m_value, this.GetTextFont(), this.StringFormat, new SizeF(float.MaxValue, float.MaxValue));
          num = num + stringLayoutResult.ActualSize.Width + (float) (((double) this.Style.Borders.Left.Width + (double) this.Style.Borders.Right.Width) * 2.0);
        }
        else if (this.m_value is PdfGrid)
          num = (this.m_value as PdfGrid).Size.Width;
        return num + (this.Row.Grid.Style.CellPadding.Left + this.Row.Grid.Style.CellPadding.Right) + this.Row.Grid.Style.CellSpacing;
      }

      private void SetTransparency(ref PdfGraphics graphics, PdfPen pen)
      {
        float alpha = (float) pen.Color.A / (float) byte.MaxValue;
        graphics.Save();
        graphics.SetTransparency(alpha);
      }

      public int ColumnSpan
      {
        get => this.m_colSpan;
        set
        {
          if (value < 1)
            throw new ArgumentException("Invalid span specified, must be greater than or equal to 1");
          if (value <= 1)
            return;
          this.m_colSpan = value;
          this.Row.ColumnSpanExists = true;
        }
      }

      internal bool FinishedDrawingCell
      {
        get => this.m_finsh;
        set => this.m_finsh = value;
      }

      public float Height
      {
        get
        {
          if ((double) this.m_height == -3.4028234663852886E+38)
            this.m_height = this.MeasureHeight();
          return this.m_height;
        }
        internal set => this.m_height = value;
      }

      public PdfGridImagePosition ImagePosition
      {
        get => this.m_imagePosition;
        set => this.m_imagePosition = value;
      }

      internal bool IsCellMergeContinue
      {
        get => this.m_bIsCellMergeContinue;
        set => this.m_bIsCellMergeContinue = value;
      }

      internal bool IsCellMergeStart
      {
        get => this.m_bIsCellMergeStart;
        set => this.m_bIsCellMergeStart = value;
      }

      internal bool IsRowMergeContinue
      {
        get => this.m_bIsRowMergeContinue;
        set => this.m_bIsRowMergeContinue = value;
      }

      internal bool IsRowMergeStart
      {
        get => this.m_bIsRowMergeStart;
        set => this.m_bIsRowMergeStart = value;
      }

      internal PdfGridCell NextCell => this.GetNextCell();

      internal string RemainingString
      {
        get => this.m_remainingString;
        set => this.m_remainingString = value;
      }

      internal PdfGridRow Row
      {
        get => this.m_row;
        set => this.m_row = value;
      }

      public int RowSpan
      {
        get => this.m_rowSpan;
        set
        {
          if (value < 1)
            throw new ArgumentException("Invalid span specified, must be greater than or equal to 1");
          if (value <= 1)
            return;
          this.m_rowSpan = value;
          this.Row.RowSpanExists = true;
        }
      }

      public PdfStringFormat StringFormat
      {
        get
        {
          if (this.m_format == null)
            this.m_format = new PdfStringFormat();
          return this.m_format;
        }
        set => this.m_format = value;
      }

      public PdfGridCellStyle Style
      {
        get
        {
          if (this.m_style == null)
            this.m_style = new PdfGridCellStyle();
          return this.m_style;
        }
        set => this.m_style = value;
      }

      public object Value
      {
        get => this.m_value;
        set
        {
          this.m_value = value;
          if (!(this.m_value is PdfGrid))
            return;
          (this.m_value as PdfGrid).Style.AllowHorizontalOverflow = false;
        }
      }

      public float Width
      {
        get
        {
          if ((double) this.m_width == -3.4028234663852886E+38)
            this.m_width = this.MeasureWidth();
          return (float) Math.Round((double) this.m_width, 4);
        }
        internal set => this.m_width = value;
      }
    }
}
