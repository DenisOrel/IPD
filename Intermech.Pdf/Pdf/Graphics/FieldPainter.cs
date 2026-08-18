// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.FieldPainter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    internal class FieldPainter
    {
      private static PdfBrush s_blackBrush = (PdfBrush) null;
      private static PdfStringFormat s_checkFieldFormat = (PdfStringFormat) null;
      private static PdfBrush s_grayBrush = (PdfBrush) null;
      private static Dictionary<string, PdfPen> s_pens = new Dictionary<string, PdfPen>();
      private static PdfBrush s_silverBrush = (PdfBrush) null;
      private static PdfBrush s_whiteBrush = (PdfBrush) null;

      private static void DrawBorder(
        PdfGraphics g,
        RectangleF bounds,
        PdfPen borderPen,
        PdfBorderStyle style,
        int borderWidth)
      {
        if (borderPen == null || borderWidth <= 0 || borderPen.Color.IsEmpty)
          return;
        if (style == PdfBorderStyle.Underline)
        {
          g.DrawLine(borderPen, bounds.X, (float) ((double) bounds.Y + (double) bounds.Height - (double) borderWidth / 2.0), bounds.X + bounds.Width, (float) ((double) bounds.Y + (double) bounds.Height - (double) borderWidth / 2.0));
        }
        else
        {
          RectangleF rectangle = new RectangleF(bounds.X + (float) borderWidth / 2f, bounds.Y + (float) borderWidth / 2f, bounds.Width - (float) borderWidth, bounds.Height - (float) borderWidth);
          g.DrawRectangle(borderPen, rectangle);
        }
      }

      public static void DrawButton(
        PdfGraphics g,
        PaintParams paintParams,
        string text,
        PdfFont font,
        PdfStringFormat format)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        FieldPainter.DrawRectangularControl(g, paintParams);
        PdfGraphics pdfGraphics = g;
        string s = text;
        PdfFont font1 = font;
        PdfBrush foreBrush = paintParams.ForeBrush;
        RectangleF bounds = paintParams.Bounds;
        double x = (double) bounds.X;
        bounds = paintParams.Bounds;
        double y = (double) bounds.Y;
        PointF location = new PointF((float) x, (float) y);
        bounds = paintParams.Bounds;
        SizeF size = bounds.Size;
        RectangleF layoutRectangle = new RectangleF(location, size);
        PdfStringFormat format1 = format;
        pdfGraphics.DrawString(s, font1, foreBrush, layoutRectangle, format1);
      }

      public static void DrawCheckBox(
        PdfGraphics g,
        PaintParams paintParams,
        string checkSymbol,
        PdfCheckFieldState state)
      {
        FieldPainter.DrawCheckBox(g, paintParams, checkSymbol, state, (PdfFont) null);
      }

      public static void DrawCheckBox(
        PdfGraphics g,
        PaintParams paintParams,
        string checkSymbol,
        PdfCheckFieldState state,
        PdfFont font)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        if (checkSymbol == null)
          throw new ArgumentNullException(nameof (checkSymbol));
        switch (state)
        {
          case PdfCheckFieldState.Unchecked:
          case PdfCheckFieldState.Checked:
            g.DrawRectangle(paintParams.BackBrush, paintParams.Bounds);
            break;
          case PdfCheckFieldState.PressedUnchecked:
          case PdfCheckFieldState.PressedChecked:
            if (paintParams.BorderStyle != PdfBorderStyle.Beveled && paintParams.BorderStyle != PdfBorderStyle.Underline)
            {
              g.DrawRectangle(paintParams.ShadowBrush, paintParams.Bounds);
              break;
            }
            g.DrawRectangle(paintParams.BackBrush, paintParams.Bounds);
            break;
        }
        FieldPainter.DrawBorder(g, paintParams.Bounds, paintParams.BorderPen, paintParams.BorderStyle, paintParams.BorderWidth);
        if (state == PdfCheckFieldState.PressedChecked || state == PdfCheckFieldState.PressedUnchecked)
        {
          switch (paintParams.BorderStyle)
          {
            case PdfBorderStyle.Beveled:
              FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, paintParams.ShadowBrush);
              FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.WhiteBrush);
              break;
            case PdfBorderStyle.Inset:
              FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.BlackBrush);
              FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.WhiteBrush);
              break;
          }
        }
        else
        {
          switch (paintParams.BorderStyle)
          {
            case PdfBorderStyle.Beveled:
              FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.WhiteBrush);
              FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, paintParams.ShadowBrush);
              break;
            case PdfBorderStyle.Inset:
              FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.GrayBrush);
              FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.SilverBrush);
              break;
          }
        }
        switch (state)
        {
          case PdfCheckFieldState.Checked:
          case PdfCheckFieldState.PressedChecked:
            font = font == null ? (PdfFont) new PdfStandardFont(PdfFontFamily.ZapfDingbats, (float) (int) ((double) paintParams.Bounds.Height * 0.4)) : (PdfFont) new PdfStandardFont(PdfFontFamily.ZapfDingbats, font.Size);
            if ((double) paintParams.Bounds.Height < (double) font.Size)
              throw new Exception("Font size cannot be greater than CheckBox height");
            g.DrawString(checkSymbol, font, paintParams.ForeBrush, paintParams.Bounds, FieldPainter.CheckFieldFormat);
            break;
        }
      }

      public static void DrawComboBox(PdfGraphics g, PaintParams paintParams)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        FieldPainter.DrawRectangularControl(g, paintParams);
      }

      private static void DrawLeftTopShadow(
        PdfGraphics g,
        RectangleF bounds,
        int width,
        PdfBrush brush)
      {
        PdfPath path = new PdfPath();
        PointF[] points = new PointF[6]
        {
          new PointF(bounds.X + (float) width, bounds.Y + (float) width),
          new PointF(bounds.X + (float) width, bounds.Bottom - (float) width),
          new PointF(bounds.X + (float) (2 * width), bounds.Bottom - (float) (2 * width)),
          new PointF(bounds.X + (float) (2 * width), bounds.Y + (float) (2 * width)),
          new PointF(bounds.Right - (float) (2 * width), bounds.Y + (float) (2 * width)),
          new PointF(bounds.Right - (float) width, bounds.Y + (float) width)
        };
        path.AddPolygon(points);
        g.DrawPath(brush, path);
      }

      public static void DrawListBox(
        PdfGraphics g,
        PaintParams paintParams,
        PdfListFieldItemCollection items,
        int[] selectedItem,
        PdfFont font,
        PdfStringFormat stringFormat)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        FieldPainter.DrawRectangularControl(g, paintParams);
        int index = 0;
        for (int count = items.Count; index < count; ++index)
        {
          PdfListFieldItem pdfListFieldItem = items[index];
          PointF empty = PointF.Empty;
          float borderWidth = (float) paintParams.BorderWidth;
          float num1 = 2f * borderWidth;
          bool flag1 = paintParams.BorderStyle == PdfBorderStyle.Inset || paintParams.BorderStyle == PdfBorderStyle.Beveled;
          if (flag1)
          {
            empty.X = 2f * num1;
            empty.Y = (float) ((double) (index + 2) * (double) borderWidth + (double) font.Size * (double) index);
          }
          else
          {
            empty.X = num1;
            empty.Y = (float) ((double) (index + 1) * (double) borderWidth + (double) font.Size * (double) index);
          }
          PdfBrush brush1 = paintParams.ForeBrush;
          RectangleF bounds = paintParams.Bounds;
          float width = bounds.Width - num1;
          RectangleF rectangle = bounds;
          if (flag1)
            rectangle.Height -= num1;
          else
            rectangle.Height -= borderWidth;
          g.SetClip(rectangle, PdfFillMode.Winding);
          bool flag2 = false;
          foreach (int num2 in selectedItem)
          {
            if (num2 == index)
              flag2 = true;
          }
          if (flag2)
          {
            float x = bounds.X + borderWidth;
            if (flag1)
            {
              x += borderWidth;
              width -= num1;
            }
            PdfBrush brush2 = (PdfBrush) new PdfSolidBrush(new PdfColor(byte.MaxValue, (byte) 51, (byte) 153, byte.MaxValue));
            g.DrawRectangle(brush2, x, empty.Y, width, font.Height);
            brush1 = (PdfBrush) new PdfSolidBrush(new PdfColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
          }
          string s = pdfListFieldItem.Text != null ? pdfListFieldItem.Text : pdfListFieldItem.Value;
          RectangleF layoutRectangle = new RectangleF(empty.X, empty.Y, width - empty.X, font.Height);
          g.DrawString(s, font, brush1, layoutRectangle, stringFormat);
        }
      }

      public static void DrawPressedButton(
        PdfGraphics g,
        PaintParams paintParams,
        string text,
        PdfFont font,
        PdfStringFormat format)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (paintParams.BorderStyle == PdfBorderStyle.Inset)
          g.DrawRectangle(paintParams.ShadowBrush, paintParams.Bounds);
        else
          g.DrawRectangle(paintParams.BackBrush, paintParams.Bounds);
        FieldPainter.DrawBorder(g, paintParams.Bounds, paintParams.BorderPen, paintParams.BorderStyle, paintParams.BorderWidth);
        RectangleF layoutRectangle;
        ref RectangleF local = ref layoutRectangle;
        double borderWidth1 = (double) paintParams.BorderWidth;
        double borderWidth2 = (double) paintParams.BorderWidth;
        SizeF size = paintParams.Bounds.Size;
        double width = (double) size.Width - (double) paintParams.BorderWidth;
        size = paintParams.Bounds.Size;
        double height = (double) size.Height - (double) paintParams.BorderWidth;
        local = new RectangleF((float) borderWidth1, (float) borderWidth2, (float) width, (float) height);
        g.DrawString(text, font, paintParams.ForeBrush, layoutRectangle, format);
        switch (paintParams.BorderStyle)
        {
          case PdfBorderStyle.Beveled:
            FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, paintParams.ShadowBrush);
            FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.WhiteBrush);
            break;
          case PdfBorderStyle.Inset:
            FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.GrayBrush);
            FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.SilverBrush);
            break;
          default:
            FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, paintParams.ShadowBrush);
            break;
        }
      }

      public static void DrawRadioButton(
        PdfGraphics g,
        PaintParams paintParams,
        string checkSymbol,
        PdfCheckFieldState state)
      {
        switch (state)
        {
          case PdfCheckFieldState.Unchecked:
          case PdfCheckFieldState.Checked:
            g.DrawEllipse(paintParams.BackBrush, paintParams.Bounds);
            break;
          case PdfCheckFieldState.PressedUnchecked:
          case PdfCheckFieldState.PressedChecked:
            if (paintParams.BorderStyle != PdfBorderStyle.Beveled && paintParams.BorderStyle != PdfBorderStyle.Underline)
            {
              g.DrawEllipse(paintParams.ShadowBrush, paintParams.Bounds);
              break;
            }
            g.DrawEllipse(paintParams.BackBrush, paintParams.Bounds);
            break;
        }
        FieldPainter.DrawRoundBorder(g, paintParams.Bounds, paintParams.BorderPen, paintParams.BorderWidth);
        FieldPainter.DrawRoundShadow(g, paintParams, state);
        switch (state - 1)
        {
          case PdfCheckFieldState.Unchecked:
          case PdfCheckFieldState.PressedUnchecked:
            float num = 0.0f;
            switch (paintParams.BorderStyle)
            {
              case PdfBorderStyle.Beveled:
              case PdfBorderStyle.Inset:
                num = (float) (((double) paintParams.Bounds.Height - (double) (4 * paintParams.BorderWidth)) / 1.5);
                break;
            }
            float size = (float) (((double) paintParams.Bounds.Height - (double) (2 * paintParams.BorderWidth)) / 1.5);
            if (paintParams.Bounds == RectangleF.Empty)
              size = 0.0f;
            PdfFont font = (PdfFont) new PdfStandardFont(PdfFontFamily.ZapfDingbats, size);
            g.DrawString(checkSymbol, font, paintParams.ForeBrush, paintParams.Bounds, FieldPainter.CheckFieldFormat);
            break;
        }
      }

      private static void DrawRectangularControl(PdfGraphics g, PaintParams paintParams)
      {
        g.DrawRectangle(paintParams.BackBrush, paintParams.Bounds);
        FieldPainter.DrawBorder(g, paintParams.Bounds, paintParams.BorderPen, paintParams.BorderStyle, paintParams.BorderWidth);
        switch (paintParams.BorderStyle)
        {
          case PdfBorderStyle.Beveled:
            FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.WhiteBrush);
            FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, paintParams.ShadowBrush);
            break;
          case PdfBorderStyle.Inset:
            FieldPainter.DrawLeftTopShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.GrayBrush);
            FieldPainter.DrawRightBottomShadow(g, paintParams.Bounds, paintParams.BorderWidth, FieldPainter.SilverBrush);
            break;
        }
      }

      private static void DrawRightBottomShadow(
        PdfGraphics g,
        RectangleF bounds,
        int width,
        PdfBrush brush)
      {
        PdfPath path = new PdfPath();
        PointF[] points = new PointF[6]
        {
          new PointF(bounds.X + (float) width, bounds.Bottom - (float) width),
          new PointF(bounds.X + (float) (2 * width), bounds.Bottom - (float) (2 * width)),
          new PointF(bounds.Right - (float) (2 * width), bounds.Bottom - (float) (2 * width)),
          new PointF(bounds.Right - (float) (2 * width), bounds.Y + (float) (2 * width)),
          new PointF(bounds.X + bounds.Width - (float) width, bounds.Y + (float) width),
          new PointF(bounds.Right - (float) width, bounds.Bottom - (float) width)
        };
        path.AddPolygon(points);
        g.DrawPath(brush, path);
      }

      private static void DrawRoundBorder(
        PdfGraphics g,
        RectangleF bounds,
        PdfPen borderPen,
        int borderWidth)
      {
        RectangleF rectangle = bounds;
        if (!(rectangle != RectangleF.Empty))
          return;
        rectangle = new RectangleF(bounds.X + (float) borderWidth / 2f, bounds.Y + (float) borderWidth / 2f, bounds.Width - (float) borderWidth, bounds.Height - (float) borderWidth);
        g.DrawEllipse(borderPen, rectangle);
      }

      private static void DrawRoundShadow(
        PdfGraphics g,
        PaintParams paintParams,
        PdfCheckFieldState state)
      {
        float borderWidth = (float) paintParams.BorderWidth;
        RectangleF bounds = paintParams.Bounds;
        bounds.Inflate(-1.5f * borderWidth, -1.5f * borderWidth);
        PdfPen pen1 = (PdfPen) null;
        PdfPen pen2 = (PdfPen) null;
        PdfColor color = ((PdfSolidBrush) paintParams.ShadowBrush).Color;
        switch (paintParams.BorderStyle)
        {
          case PdfBorderStyle.Beveled:
            switch (state)
            {
              case PdfCheckFieldState.Unchecked:
              case PdfCheckFieldState.Checked:
                pen1 = FieldPainter.GetPen(new PdfColor(byte.MaxValue, byte.MaxValue, byte.MaxValue), borderWidth);
                pen2 = FieldPainter.GetPen(color, borderWidth);
                break;
              case PdfCheckFieldState.PressedUnchecked:
              case PdfCheckFieldState.PressedChecked:
                pen1 = FieldPainter.GetPen(color, borderWidth);
                pen2 = FieldPainter.GetPen(new PdfColor(byte.MaxValue, byte.MaxValue, byte.MaxValue), borderWidth);
                break;
            }
            break;
          case PdfBorderStyle.Inset:
            switch (state)
            {
              case PdfCheckFieldState.Unchecked:
              case PdfCheckFieldState.Checked:
                pen1 = FieldPainter.GetPen(new PdfColor(byte.MaxValue, (byte) 128 /*0x80*/, (byte) 128 /*0x80*/, (byte) 128 /*0x80*/), borderWidth);
                pen2 = FieldPainter.GetPen(new PdfColor(byte.MaxValue, (byte) 192 /*0xC0*/, (byte) 192 /*0xC0*/, (byte) 192 /*0xC0*/), borderWidth);
                break;
              case PdfCheckFieldState.PressedUnchecked:
              case PdfCheckFieldState.PressedChecked:
                pen1 = FieldPainter.GetPen(new PdfColor((byte) 0, (byte) 0, (byte) 0), borderWidth);
                pen2 = FieldPainter.GetPen(new PdfColor((byte) 0, (byte) 0, (byte) 0), borderWidth);
                break;
            }
            break;
        }
        if (pen1 == null || pen2 == null)
          return;
        g.DrawArc(pen1, bounds, 135f, 180f);
        g.DrawArc(pen2, bounds, -45f, 180f);
      }

      public static void DrawSignature(PdfGraphics g, PaintParams paintParams)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        FieldPainter.DrawRectangularControl(g, paintParams);
        RectangleF bounds = paintParams.Bounds;
        if (paintParams.BorderStyle == PdfBorderStyle.Beveled || paintParams.BorderStyle == PdfBorderStyle.Inset)
        {
          bounds.X += (float) (4 * paintParams.BorderWidth);
          bounds.Width -= (float) (8 * paintParams.BorderWidth);
        }
        else
        {
          bounds.X += (float) (2 * paintParams.BorderWidth);
          bounds.Width -= (float) (4 * paintParams.BorderWidth);
        }
      }

      public static void DrawTextBox(
        PdfGraphics g,
        PaintParams paintParams,
        string text,
        PdfFont font,
        PdfStringFormat format,
        bool multiLine,
        bool scroll)
      {
        if (g == null)
          throw new ArgumentNullException(nameof (g));
        if (paintParams == null)
          throw new ArgumentNullException(nameof (paintParams));
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        FieldPainter.DrawRectangularControl(g, paintParams);
        RectangleF bounds = paintParams.Bounds;
        if (paintParams.BorderStyle == PdfBorderStyle.Beveled || paintParams.BorderStyle == PdfBorderStyle.Inset)
        {
          bounds.X += (float) (4 * paintParams.BorderWidth);
          bounds.Width -= (float) (8 * paintParams.BorderWidth);
        }
        else
        {
          bounds.X += (float) (2 * paintParams.BorderWidth);
          bounds.Width -= (float) (4 * paintParams.BorderWidth);
        }
        if (multiLine)
        {
          float num1 = format == null || (double) format.LineSpacing == 0.0 ? font.Height : format.LineSpacing;
          int num2 = format == null ? 0 : (format.SubSuperScript == PdfSubSuperScript.SubScript ? 1 : 0);
          float ascent = font.Metrics.GetAscent(format);
          float descent = font.Metrics.GetDescent(format);
          float num3 = num2 != 0 ? num1 - (font.Height + descent) : num1 - ascent;
          if (bounds.Location != PointF.Empty)
            bounds.Y -= num3;
          else
            bounds.Y = (float) -((double) bounds.Y - (double) num3);
        }
        bool flag = false;
        pdfDictionary2 = (PdfDictionary) null;
        if (g.Layer != null && g.Page != null)
        {
          if (g.Page.Dictionary.ContainsKey("Rotate"))
          {
            flag = true;
          }
          else
          {
            PdfDictionary pdfDictionary1 = new PdfDictionary();
            if ((g.Page.Dictionary["Parent"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary2 && pdfDictionary2.ContainsKey("Rotate"))
              flag = true;
          }
        }
        if (paintParams.RotationAngle > 0)
          flag = true;
        if (paintParams.RotationAngle > 0 & flag)
        {
          PdfGraphicsState state = g.Save();
          float x = bounds.X;
          bounds.X = (float) -((double) bounds.Y + (double) bounds.Height);
          bounds.Y = x;
          float height1 = bounds.Height;
          bounds.Height = (double) bounds.Width > (double) font.Height ? bounds.Width : font.Height;
          bounds.Width = height1;
          SizeF size;
          if (paintParams.RotationAngle == 90 && pdfDictionary2 == null)
          {
            PdfGraphics pdfGraphics = g;
            size = g.Size;
            double height2 = (double) size.Height;
            pdfGraphics.TranslateTransform((float) height2, 0.0f);
          }
          if (paintParams.RotationAngle == 180 && pdfDictionary2 == null)
          {
            PdfGraphics pdfGraphics = g;
            size = g.Size;
            double height3 = (double) size.Height;
            size = g.Size;
            double width = (double) size.Width;
            pdfGraphics.TranslateTransform((float) height3, (float) width);
          }
          if (paintParams.RotationAngle == 270 && pdfDictionary2 == null)
          {
            PdfGraphics pdfGraphics = g;
            size = g.Size;
            double width = (double) size.Width;
            pdfGraphics.TranslateTransform(0.0f, (float) width);
          }
          if (pdfDictionary2 != null)
            g.RotateTransform((float) -paintParams.RotationAngle);
          g.DrawString(text, font, paintParams.ForeBrush, bounds, format);
          g.Restore(state);
        }
        else
          g.DrawString(text, font, paintParams.ForeBrush, bounds, format);
      }

      private static PdfPen GetPen(PdfColor color, float width)
      {
        lock (FieldPainter.s_pens)
        {
          string key = $"{color}{width}";
          PdfPen pen = FieldPainter.s_pens.ContainsKey(key) ? FieldPainter.s_pens[key] : (PdfPen) null;
          if (pen == null)
          {
            pen = new PdfPen(color, width);
            FieldPainter.s_pens[key] = pen;
          }
          return pen;
        }
      }

      private static PdfBrush BlackBrush
      {
        get
        {
          lock (FieldPainter.s_pens)
          {
            if (FieldPainter.s_blackBrush == null)
              FieldPainter.s_blackBrush = PdfBrushes.Black;
            return FieldPainter.s_blackBrush;
          }
        }
      }

      private static PdfStringFormat CheckFieldFormat
      {
        get
        {
          lock (FieldPainter.s_pens)
          {
            if (FieldPainter.s_checkFieldFormat == null)
              FieldPainter.s_checkFieldFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            return FieldPainter.s_checkFieldFormat;
          }
        }
      }

      private static PdfBrush GrayBrush
      {
        get
        {
          lock (FieldPainter.s_pens)
          {
            if (FieldPainter.s_grayBrush == null)
              FieldPainter.s_grayBrush = PdfBrushes.Gray;
            return FieldPainter.s_grayBrush;
          }
        }
      }

      private static PdfBrush SilverBrush
      {
        get
        {
          lock (FieldPainter.s_pens)
          {
            if (FieldPainter.s_silverBrush == null)
              FieldPainter.s_silverBrush = PdfBrushes.Silver;
            return FieldPainter.s_silverBrush;
          }
        }
      }

      private static PdfBrush WhiteBrush
      {
        get
        {
          lock (FieldPainter.s_pens)
          {
            if (FieldPainter.s_whiteBrush == null)
              FieldPainter.s_whiteBrush = PdfBrushes.White;
            return FieldPainter.s_whiteBrush;
          }
        }
      }
    }
}
