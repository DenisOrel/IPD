// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ContinuationTableIdEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Document.Model;

internal class ContinuationTableIdEditor : UITypeEditor
{
  /// <summary>Получает стиль редактирования, используемый методом EditValue</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <returns>Значение UITypeEditorEditStyle, которое указывает на стиль редактирования,
  /// используемый EditValue. Если UITypeEditor не поддерживает данный метод,
  /// то GetEditStyle возвращает None</returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
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
    IServiceProvider provider,
    object value)
  {
    return (object) ContinuationTableIdEditor.ChooseContinuationForTable((TableElement) context.Instance);
  }

  /// <summary>Выбор таблицы продолжения на текущей странице</summary>
  /// <param name="curTable">Таблица, для которой выбирается таблица продолжения</param>
  /// <returns>Идентификатор таблицы продолжения, если выбрана, или пустая строка</returns>
  internal static string ChooseContinuationForTable(TableElement curTable)
  {
    DocumentTreeNode page = (DocumentTreeNode) curTable.Page;
    ContinuationTablesFilter filter = new ContinuationTablesFilter()
    {
      CurrentTable = curTable
    };
    TableElement tableElement = (TableElement) SelectNodeDlg.Execute(typeof (TableElement), (DocumentTreeNode) curTable, page, LocalizationHolder.rm.GetString("Document.Model_656"), 0, (TypeNodeFilter) filter);
    if (tableElement == null && curTable.HasContinuation())
      tableElement = (TableElement) curTable.NextTable;
    return tableElement != null && !(tableElement.Id == curTable.Id) ? tableElement.Id : string.Empty;
  }
}
