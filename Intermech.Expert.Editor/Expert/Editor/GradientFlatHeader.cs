// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.GradientFlatHeader
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevAge.Drawing;
using SourceGrid3;
using SourceGrid3.Cells.Views;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Визуальная модель заголовка для SourceGrid. Плоский стиль с градиентами</summary>
public class GradientFlatHeader : Header
{
  /// <summary>Represents a default Header</summary>
  public static readonly GradientFlatHeader Default = new GradientFlatHeader();
  /// <summary>Represents a Column Header with the ability to draw an Image in the right to indicates the sort operation. You must use this model with a cell of type ICellSortableHeader.</summary>
  public static readonly GradientFlatHeader GradientColumnHeader = new GradientFlatHeader();
  /// <summary>Represents a Row Header.</summary>
  public static readonly GradientFlatHeader GradientRowHeader = new GradientFlatHeader();

  static GradientFlatHeader()
  {
    GradientFlatHeader.GradientRowHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
  }

  /// <summary>Use default setting</summary>
  public GradientFlatHeader()
  {
    this.UseTheme = false;
    this.BackColor = Color.FromKnownColor(KnownColor.Control);
    this.Border = new RectangleBorder(new DevAge.Drawing.Border(SystemColors.ControlDark), new DevAge.Drawing.Border(SystemColors.ControlDark));
    this.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
  }

  /// <summary>Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.</summary>
  /// <param name="p_Source"></param>
  public GradientFlatHeader(GradientFlatHeader p_Source)
    : base((Header) p_Source)
  {
  }

  /// <summary>
  /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
  /// </summary>
  /// <returns>Clone</returns>
  public override object Clone() => (object) new GradientFlatHeader(this);

  /// <summary>Нарисовать фон ячейки</summary>
  /// <param name="cellContext">Контекст</param>
  /// <param name="e">Аргументы события Paint</param>
  /// <param name="p_ClientRectangle">Область ячейки</param>
  protected override void DrawCell_Background(
    CellContext cellContext,
    PaintEventArgs e,
    Rectangle p_ClientRectangle)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(p_ClientRectangle, ControlPaint.Light(this.BackColor, 0.5f), this.BackColor, LinearGradientMode.Vertical))
      e.Graphics.FillRectangle((Brush) linearGradientBrush, p_ClientRectangle);
  }
}
