// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.CharFormatUIEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

public class CharFormatUIEditor : UITypeEditor
{
  /// <summary>Получает стиль редактирования, используемый методом EditValue</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <returns>Значение UITypeEditorEditStyle, которое указывает на стиль редактирования,
  /// используемый EditValue. Если UITypeEditor не поддерживает данный метод,
  /// то GetEditStyle возвращает None</returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    object instance = context.Instance;
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>Поддерживает ли отрисовка значения</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Поддерживает ли отрисовка значения</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext context)
  {
    object obj = context.PropertyDescriptor.GetValue(context.Instance);
    return (obj == null || !(obj is CharFormat) || (obj as CharFormat).FontFamily != null || (obj as CharFormat).FontSize.HasValue) && obj != null;
  }

  /// <summary>Нарисовать значение</summary>
  /// <param name="e">Аргументы</param>
  public override void PaintValue(PaintValueEventArgs e)
  {
    if (e.Value == null || !(e.Value is CharFormat))
      return;
    if (!(e.Context.Instance is RectangleElement instance))
      instance = (e.Context.Instance as object[])[0] as RectangleElement;
    SolidBrush solidBrush1 = new SolidBrush(Color.White);
    if (instance != null)
      solidBrush1.Color = instance.BackColor;
    CharFormat charFormat = e.Value as CharFormat;
    Color? nullable1 = charFormat.TextBkColorForUser;
    if (nullable1.HasValue)
    {
      SolidBrush solidBrush2 = solidBrush1;
      nullable1 = charFormat.TextBkColorForUser;
      Color color = nullable1.Value;
      solidBrush2.Color = color;
    }
    Rectangle rectangle = Rectangle.Round((RectangleF) e.Bounds);
    float emSize = (float) e.Bounds.Height / 1.2f;
    e.Graphics.FillRectangle((Brush) solidBrush1, rectangle);
    if (!charFormat.Strike.HasValue || charFormat.FontFamily == null || !charFormat.Underline.HasValue)
      return;
    float? nullable2 = charFormat.FontSize;
    if (!nullable2.HasValue)
      return;
    nullable2 = charFormat.FontSizeMm;
    if (!nullable2.HasValue || !charFormat.BoldItalic.HasValue)
      return;
    nullable1 = charFormat.TextColorForUser;
    if (!nullable1.HasValue)
      return;
    SolidBrush solidBrush3 = solidBrush1;
    nullable1 = charFormat.TextColorForUser;
    Color color1 = nullable1.Value;
    solidBrush3.Color = color1;
    Font font1 = charFormat.GetFont();
    Font font2 = new Font(font1.FontFamily, emSize, font1.Style, GraphicsUnit.Pixel, font1.GdiCharSet);
    try
    {
      e.Graphics.DrawString("abcd", font2, (Brush) solidBrush1, (RectangleF) rectangle);
    }
    finally
    {
      font1.Dispose();
      solidBrush1.Dispose();
      font2.Dispose();
    }
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
    CharFormat CharFormat = new CharFormat();
    FontSetupDlg fontSetupDlg = (FontSetupDlg) null;
    object obj1 = context.Instance;
    if (obj1 is object[])
    {
      TableElement virtualTable = TableElement.CreateVirtualTable((DocumentTreeNode) null, (DocumentTreeNode) null);
      foreach (object obj2 in obj1 as object[])
      {
        if (obj2 is RectangleElement child)
          virtualTable.AddChildNode((DocumentTreeNode) child, false, false);
      }
      if (virtualTable.NodesCount > 0)
        obj1 = (object) virtualTable;
    }
    if (obj1 is TableElement)
    {
      TableElement tableElement = (TableElement) obj1;
      if (tableElement.CharFormat != null)
        CharFormat = tableElement.CharFormat.Clone();
      fontSetupDlg = new FontSetupDlg(CharFormat, tableElement.Text);
    }
    if (obj1 is VirtualColumn)
    {
      VirtualColumn virtualColumn = (VirtualColumn) obj1;
      if (virtualColumn.CharFormat != null)
        CharFormat = virtualColumn.CharFormat.Clone();
      fontSetupDlg = new FontSetupDlg(CharFormat, virtualColumn.Text);
    }
    if (obj1 is TextData)
    {
      TextData textData = (TextData) obj1;
      if (textData.CharFormat != null)
        CharFormat = textData.CharFormat.Clone();
      fontSetupDlg = new FontSetupDlg(CharFormat, textData.Text);
    }
    if (fontSetupDlg != null && fontSetupDlg.ShowDialog() == DialogResult.OK)
      fontSetupDlg.Save();
    return (object) CharFormat;
  }
}
