// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LineDashStyleUIEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Редактор BorderStyles</summary>
public class LineDashStyleUIEditor : UITypeEditor
{
  /// <summary>Поддерживает ли отрисовка значения</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Поддерживает ли отрисовка значения</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext context)
  {
    return context.PropertyDescriptor.GetValue(context.Instance) != null;
  }

  /// <summary>Нарисовать значение</summary>
  /// <param name="e">Аргументы</param>
  public override void PaintValue(PaintValueEventArgs e)
  {
    object obj;
    if (!((obj = e.Value) is LineDashStyle))
      return;
    LineDashStyle lineDashStyle = (LineDashStyle) obj;
    Rectangle rect = Rectangle.Round((RectangleF) e.Bounds);
    e.Graphics.FillRectangle(SystemBrushes.Window, rect);
    Pen pen = new Pen(VisualNode.InvertColor(SystemColors.Window));
    pen.DashStyle = (DashStyle) lineDashStyle;
    try
    {
      Point pt1 = new Point(rect.X, rect.Y + rect.Height / 2);
      Point pt2 = new Point(rect.Right - 1, rect.Y + rect.Height / 2);
      e.Graphics.DrawLine(pen, pt1, pt2);
    }
    finally
    {
      pen.Dispose();
    }
  }

  /// <summary>Получает стиль редактирования, используемый методом EditValue</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <returns>Значение UITypeEditorEditStyle, которое указывает на стиль редактирования,
  /// используемый EditValue. Если UITypeEditor не поддерживает данный метод,
  /// то GetEditStyle возвращает None</returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  /// <summary>Можно ли изменять размер выпадающего списка</summary>
  public override bool IsDropDownResizable
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Редактирует значение заданного объекта, используя стиль редактора,
  /// предоставляемого с помощью GetEditStyle</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <param name="provider">Поставщик IServiceProvider, который использует этот редактор
  /// для обслуживания</param>
  /// <param name="value">Объект редактирования</param>
  /// <returns>Новое значение объекта</returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
    if (formsEditorService != null)
    {
      ListBox listBox = new ListBox();
      listBox.BorderStyle = BorderStyle.None;
      listBox.DrawMode = DrawMode.OwnerDrawFixed;
      listBox.DrawItem += new DrawItemEventHandler(this.listBox_DrawItem);
      listBox.Click += new EventHandler(this.listBox_Click);
      listBox.Tag = (object) formsEditorService;
      ICollection standardValues = context.PropertyDescriptor.Converter.GetStandardValues();
      if (standardValues != null)
      {
        ArrayList arrayList = new ArrayList(standardValues);
        arrayList.Remove((object) null);
        object[] items = new object[arrayList.Count];
        arrayList.CopyTo((Array) items);
        listBox.Items.AddRange(items);
      }
      listBox.SelectedItem = value;
      formsEditorService.DropDownControl((Control) listBox);
      if (listBox.SelectedItem != null)
        value = listBox.SelectedItem;
    }
    return value;
  }

  private void listBox_Click(object sender, EventArgs e)
  {
    if (!(sender is ListBox listBox) || !(listBox.Tag is IWindowsFormsEditorService tag))
      return;
    tag.CloseDropDown();
  }

  private void listBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (sender is ListBox listBox && listBox.Items[e.Index] is LineDashStyle)
    {
      Rectangle rectangle = Rectangle.Round((RectangleF) e.Bounds);
      LineDashStyle lineDashStyle = (LineDashStyle) listBox.Items[e.Index];
      e.DrawBackground();
      Pen pen = new Pen(e.ForeColor, PageElementNode.DefaultLineWidth);
      pen.DashStyle = (DashStyle) lineDashStyle;
      pen.Color = e.ForeColor;
      GraphicsUnit pageUnit = e.Graphics.PageUnit;
      try
      {
        e.Graphics.PageUnit = GraphicsUnit.Millimeter;
        PointF dpi = new PointF(e.Graphics.DpiX, e.Graphics.DpiY);
        PointF mm1 = UnitsConverter.PixelsToMm(new Point(rectangle.X, rectangle.Y + rectangle.Height / 2), dpi);
        PointF mm2 = UnitsConverter.PixelsToMm(new Point(rectangle.Right - 1, rectangle.Y + rectangle.Height / 2), dpi);
        e.Graphics.DrawLine(pen, mm1, mm2);
      }
      finally
      {
        e.Graphics.PageUnit = pageUnit;
        pen.Dispose();
      }
    }
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }
}
