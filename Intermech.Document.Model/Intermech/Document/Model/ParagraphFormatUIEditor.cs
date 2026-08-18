// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ParagraphFormatUIEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

internal class ParagraphFormatUIEditor : UITypeEditor
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
  public override bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

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
    ParagraphFormat paragraphFormat = new ParagraphFormat();
    SetupParagraphDlg setupParagraphDlg = (SetupParagraphDlg) null;
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
      if (tableElement.ParagraphFormat != null)
        paragraphFormat = tableElement.ParagraphFormat.Clone();
      setupParagraphDlg = new SetupParagraphDlg(paragraphFormat, new float?(12f), true);
    }
    if (obj1 is VirtualColumn)
    {
      VirtualColumn virtualColumn = (VirtualColumn) obj1;
      if (virtualColumn.ParagraphFormat != null)
        paragraphFormat = virtualColumn.ParagraphFormat.Clone();
      setupParagraphDlg = new SetupParagraphDlg(paragraphFormat, new float?(12f), true);
    }
    if (obj1 is TextData)
    {
      TextData textData = (TextData) obj1;
      if (textData.ParagraphFormat != null)
        paragraphFormat = textData.ParagraphFormat.Clone();
      setupParagraphDlg = new SetupParagraphDlg(paragraphFormat, new float?(12f), true);
    }
    if (setupParagraphDlg != null && setupParagraphDlg.ShowDialog() == DialogResult.OK)
      paragraphFormat = setupParagraphDlg.ParagraphFormat.Clone();
    return (object) paragraphFormat;
  }
}
