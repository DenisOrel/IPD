// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.BorderLineUIEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Редактор BorderLines</summary>
public class BorderLineUIEditor : UITypeEditor
{
  /// <summary>Поддерживает ли отрисовка значения</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Поддерживает ли отрисовка значения</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext context)
  {
    object obj = context.PropertyDescriptor.GetValue(context.Instance);
    return (obj == null || !(obj is BorderLineTE) || (obj as BorderLineTE).ColorTE.HasValue || (obj as BorderLineTE).StyleTE.HasValue || (obj as BorderLineTE).WidthTE.HasValue) && obj != null;
  }

  /// <summary>Нарисовать значение</summary>
  /// <param name="e">Аргументы</param>
  public override void PaintValue(PaintValueEventArgs e)
  {
    if (e.Value == null)
      return;
    if (e.Value is BorderLine borderLine)
    {
      Rectangle rect = Rectangle.Round((RectangleF) e.Bounds);
      e.Graphics.FillRectangle(SystemBrushes.Window, rect);
      Pen pen1 = borderLine.GetPen();
      if (pen1 != null)
      {
        using (Pen pen2 = (Pen) pen1.Clone())
        {
          float num1 = borderLine.Width;
          float serifWidth = borderLine.SerifWidth;
          int style = (int) borderLine.Style;
          if ((double) num1 > (double) (rect.Height - 4))
            num1 = (float) (rect.Height - 4);
          pen2.Width = num1;
          int num2 = UnitsConverter.MmToPixels(serifWidth, e.Graphics.DpiX);
          if (num2 > rect.Width)
            num2 = rect.Width - 1 - (int) ((double) num1 / 2.0);
          Point pt1 = new Point(rect.X + (int) ((double) num1 / 2.0), rect.Y + rect.Height / 2);
          Point pt2 = style != 6 ? new Point(rect.Right - 1 - (int) ((double) num1 / 2.0), rect.Y + rect.Height / 2) : new Point(rect.X + num2, rect.Y + rect.Height / 2);
          e.Graphics.DrawLine(pen2, pt1, pt2);
        }
      }
    }
    if (!(e.Value is BorderLineTE borderLineTe))
      return;
    Rectangle rect1 = Rectangle.Round((RectangleF) e.Bounds);
    e.Graphics.FillRectangle(SystemBrushes.Window, rect1);
    float width = 0.0f;
    float? nullable = borderLineTe.WidthTE;
    if (nullable.HasValue)
    {
      nullable = borderLineTe.WidthTE;
      width = nullable.Value;
    }
    float mm = 1.5f;
    nullable = borderLineTe.SerifWidthTE;
    if (nullable.HasValue)
    {
      nullable = borderLineTe.SerifWidthTE;
      mm = nullable.Value;
    }
    int pixels = UnitsConverter.MmToPixels(mm, e.Graphics.DpiX);
    BorderStyles style1 = BorderStyles.SolidLine;
    BorderStyles? styleTe = borderLineTe.StyleTE;
    if (styleTe.HasValue)
    {
      styleTe = borderLineTe.StyleTE;
      style1 = styleTe.Value;
    }
    Color gray = Color.Gray;
    Color? colorTe = borderLineTe.ColorTE;
    if (colorTe.HasValue)
    {
      colorTe = borderLineTe.ColorTE;
      gray = colorTe.Value;
    }
    if ((double) width > (double) (rect1.Height - 4))
      width = (float) (rect1.Height - 4);
    using (Pen pen = new Pen(gray, width))
    {
      pen.DashStyle = BorderLine.ConvertToDashStyle(style1);
      if (style1 == BorderStyles.None)
        return;
      Point pt1 = new Point(rect1.X + (int) ((double) width / 2.0), rect1.Y + rect1.Height / 2);
      Point pt2 = style1 != BorderStyles.Serif ? new Point(rect1.Right - 1 - (int) ((double) width / 2.0), rect1.Y + rect1.Height / 2) : new Point(rect1.X + (int) ((double) width / 2.0) + pixels, rect1.Y + rect1.Height / 2);
      e.Graphics.DrawLine(pen, pt1, pt2);
    }
  }
}
