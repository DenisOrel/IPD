
// Type: Intermech.Client.Core.ThicknessComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>настройка изменения толщины линии</summary>
public class ThicknessComboBox : IDisposable
{
  /// <summary>ссылка на ComboBox</summary>
  protected ComboBox _box;
  /// <summary>ссылка на Thickness</summary>
  protected Rclass<float> _thickness;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._box != null)
    {
      this._box.DrawItem -= new DrawItemEventHandler(ThicknessComboBox.PenThickness_DrawItem);
      this._box.SelectedIndexChanged -= new EventHandler(this.ComboBox_SelectedIndexChanged);
      this._box.Validating -= new CancelEventHandler(this.box_Validating);
      this._box = (ComboBox) null;
    }
    if (this._thickness == null)
      return;
    this._thickness.ValueChanged -= new EventHandler<EventArgs<float>>(this.Thickness_ValueChanged);
    this._thickness = (Rclass<float>) null;
  }

  /// <summary>Инициализация изменения толщины линии</summary>
  /// <param name="varbox">ссылка на ComboBox</param>
  /// <param name="varPenThickness">ссылка на Thickness</param>
  public void Initialize(ComboBox varbox, Rclass<float> varPenThickness)
  {
    if (varbox == null)
      throw new ArgumentNullException(nameof (varbox));
    if (varPenThickness == null)
      throw new ArgumentNullException(nameof (varPenThickness));
    this._box = varbox;
    this._thickness = varPenThickness;
    this._box.Items.AddRange(new object[10]
    {
      (object) 0.0f,
      (object) 0.25f,
      (object) 0.5f,
      (object) 0.75f,
      (object) 1f,
      (object) 1.25f,
      (object) 1.5f,
      (object) 1.75f,
      (object) 2f,
      (object) 2.5f
    });
    this._box.DrawMode = DrawMode.OwnerDrawFixed;
    this._box.DrawItem += new DrawItemEventHandler(ThicknessComboBox.PenThickness_DrawItem);
    this._box.TabStop = false;
    this._box.Enabled = true;
    this._box.SelectedIndex = -1;
    this._box.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._box.KeyUp += new KeyEventHandler(this._box_KeyUp);
    this._box.Validating += new CancelEventHandler(this.box_Validating);
    this._thickness.ValueChanged += new EventHandler<EventArgs<float>>(this.Thickness_ValueChanged);
    this._box.SelectedValue = (object) this._thickness.Value;
    double num = (double) this.UpdateBox(this._thickness.Value);
  }

  private void _box_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.Return)
      return;
    e.SuppressKeyPress = false;
  }

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    float result;
    if (!(sender as ComboBox).Text.TryParse(out result))
      result = 0.0f;
    this._thickness.Value = this.UpdateBox(result);
  }

  private void Thickness_ValueChanged(object sender, EventArgs<float> e)
  {
    double num = (double) this.UpdateBox(e.Value);
  }

  private float UpdateBox(float thickness)
  {
    thickness = Math.Abs(thickness);
    int num = this._box.Items.IndexOf((object) thickness);
    if (num == -1)
      num = this._box.Items.Add((object) thickness);
    if (this._box.SelectedIndex != num)
      this._box.SelectedIndex = num;
    this._box.Text = thickness.ToString();
    return thickness;
  }

  private void box_Validating(object sender, EventArgs e)
  {
    ComboBox comboBox = sender as ComboBox;
    float result;
    if (!comboBox.Text.TryParse(out result))
      result = 0.0f;
    this._thickness.Value = this.UpdateBox(result);
    comboBox.Invalidate();
  }

  /// <summary>прорисовка толщины линии в ComboBox</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private static void PenThickness_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (!(sender is ComboBox comboBox) || !(comboBox.Items[e.Index] is float))
      return;
    float num = (float) comboBox.Items[e.Index];
    float width1 = num;
    if ((double) width1 == 0.0)
      width1 = 0.0f;
    e.DrawBackground();
    int width2 = 30;
    using (Pen pen = new Pen(Color.Black, width1))
    {
      pen.DashStyle = DashStyle.Solid;
      pen.Color = e.ForeColor;
      GraphicsUnit pageUnit = e.Graphics.PageUnit;
      try
      {
        Rectangle rectangle = Rectangle.Round((RectangleF) e.Bounds);
        e.Graphics.PageUnit = GraphicsUnit.Millimeter;
        PointF dpi = new PointF(e.Graphics.DpiX, e.Graphics.DpiY);
        RectangleF mm1 = new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) width2, (float) rectangle.Height / 2f).PixelsToMm(dpi);
        e.Graphics.DrawString(num.ToString(), comboBox.Font, Brushes.Black, mm1, StringFormat.GenericTypographic);
        PointF mm2 = new Point(rectangle.X + width2, rectangle.Y + rectangle.Height / 2).PixelsToMm(dpi);
        PointF mm3 = new Point(rectangle.Right - 1, rectangle.Y + rectangle.Height / 2).PixelsToMm(dpi);
        e.Graphics.DrawLine(pen, mm2, mm3);
      }
      finally
      {
        e.Graphics.PageUnit = pageUnit;
      }
    }
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }
}
