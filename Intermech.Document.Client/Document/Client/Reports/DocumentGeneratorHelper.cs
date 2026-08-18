// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.DocumentGeneratorHelper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Document.Client.Reports;

public class DocumentGeneratorHelper : IDisposable
{
  public ImDocument DocumentTemplate { get; }

  public DocumentGeneratorHelper(
    Guid documentTemplateGuid,
    IList<TableColumnSettings> columnsSettings,
    int? originalSettingsCellIndex)
    : this(documentTemplateGuid)
  {
    DocumentGeneratorHelper.CreateTableColumnsInDocument(columnsSettings, originalSettingsCellIndex, this.DocumentTemplate);
  }

  public DocumentGeneratorHelper(Guid documentTemplateGuid)
  {
    this.DocumentTemplate = DocumentGeneratorHelper.LoadImDocumentTemplate(documentTemplateGuid);
  }

  private static ImDocument LoadImDocumentTemplate(Guid documentTemplateGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(documentTemplateGuid, true), -1, false, true, false);
  }

  public static void SetupOrCreateColumnsInDataTable(
    TableData table,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    if (table.NodesCount == 0)
      throw new ArgumentException("В заданной таблице для размещения данных не содержится шаблонов строк", nameof (table));
    DocumentGeneratorHelper.SetupOrCreateTableColumns(table, columns, originalSettingsCellIndex);
  }

  public static void SetupExistColumnsInDataTable(
    IList<TableColumnSettings> columns,
    TableData table,
    int? originalSettingsCellIndex)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    for (int columnIndex = 0; columnIndex < columns.Count; ++columnIndex)
    {
      if (table.GridColumnsParams != null && columnIndex < table.GridColumnsParams.Count)
        DocumentGeneratorHelper.SetupTableColumnParams(table, columns, columnIndex, originalSettingsCellIndex);
    }
  }

  public static void CreateColumnsInHeaderRow(
    TableData row,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (row.NodesCount == 0)
      throw new ArgumentException("Заданная строка пуста", nameof (row));
    if (row.GridColumnsParams != null)
    {
      for (int gridColIndex = row.GridColumnsParams.Count - 1; gridColIndex >= columns.Count; --gridColIndex)
        row.RemoveGridColumn(gridColIndex, false, false, false);
    }
    for (int index = 0; index < columns.Count; ++index)
    {
      if (row.GridColumnsParams != null && index < row.GridColumnsParams.Count)
      {
        DocumentGeneratorHelper.SetupTableColumnWidth(row, columns[index].Width, index);
        if (row.Nodes[index] is RectangleElement node)
          node.WidthOverrided = false;
        DocumentGeneratorHelper.SetupHeaderRowCellParams(row, columns, index, originalSettingsCellIndex);
      }
      else
        DocumentGeneratorHelper.InsertNewTableColumnForOneRowTable(row, columns, index, originalSettingsCellIndex, row, (TableData) null);
    }
  }

  public static void SetupColumnsParamsInHeaderRow(
    TableData table,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    DocumentGeneratorHelper.SetupExistRowColumnsParams(table, columns, originalSettingsCellIndex);
  }

  public static void CreateTableColumnsInDocument(
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex,
    ImDocument documentTemplate)
  {
    TableData mainTableTemplate = documentTemplate.FindFirstMainTableTemplate();
    if (mainTableTemplate == null)
      throw new ArgumentException("В заданном шаблоне документа отсутствует таблица для размещения данных", nameof (documentTemplate));
    if (mainTableTemplate.NodesCount == 0)
      throw new ArgumentException("В заданном шаблоне документа таблица для размещения данных не содержит шаблонов строк", nameof (documentTemplate));
    DocumentGeneratorHelper.SetupOrCreateTableColumns(mainTableTemplate, columns, originalSettingsCellIndex);
    documentTemplate.UpdateLayout(false);
  }

  private static void SetupOrCreateTableColumns(
    TableData table,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    if (table.GridColumnsParams != null)
    {
      for (int gridColIndex = table.GridColumnsParams.Count - 1; gridColIndex >= columns.Count; --gridColIndex)
        table.RemoveGridColumn(gridColIndex, false, false, false);
    }
    for (int index = 0; index < columns.Count; ++index)
    {
      if (table.GridColumnsParams != null && index < table.GridColumnsParams.Count)
      {
        DocumentGeneratorHelper.SetupTableColumnParams(table, columns, index, originalSettingsCellIndex);
        DocumentGeneratorHelper.SetupTableColumnWidth(table, columns[index].Width, index);
      }
      else
        DocumentGeneratorHelper.InsertNewTableColumn(table, columns, index, originalSettingsCellIndex);
    }
  }

  protected static void SetupOrCreateRowColumns(
    TableData row,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (row.GridColumnsParams != null)
    {
      for (int gridColIndex = row.GridColumnsParams.Count - 1; gridColIndex >= columns.Count; --gridColIndex)
        row.RemoveGridColumn(gridColIndex, false, false, false);
    }
    for (int index = 0; index < columns.Count; ++index)
    {
      if (row.GridColumnsParams != null && index < row.GridColumnsParams.Count)
      {
        DocumentGeneratorHelper.SetupTableColumnWidth(row, columns[index].Width, index);
        DocumentGeneratorHelper.SetupTableColumnParams(row, columns, index, originalSettingsCellIndex);
      }
      else
        DocumentGeneratorHelper.InsertNewTableColumnForOneRowTable(row, columns, index, originalSettingsCellIndex, row, (TableData) null);
    }
  }

  protected static void SetupExistRowColumnsParams(
    TableData row,
    IList<TableColumnSettings> columns,
    int? originalSettingsCellIndex)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (columns == null)
      throw new ArgumentNullException(nameof (columns));
    if (row.NodesCount == 0)
      throw new ArgumentException("Заданная строка пуста", nameof (row));
    for (int columnIndex = 0; columnIndex < columns.Count; ++columnIndex)
    {
      if (row.GridColumnsParams != null && columnIndex < row.GridColumnsParams.Count)
        DocumentGeneratorHelper.SetupTableColumnParams(row, columns, columnIndex, originalSettingsCellIndex);
    }
  }

  private static void InsertNewTableColumnForOneRowTable(
    TableData table,
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex,
    TableData headerRowTemplate,
    TableData dataRowTemplate)
  {
    table.InsertNewGridColumn(columnIndex, new RowColParams(table, true, -1, columns[columnIndex].Caption, columns[columnIndex].Width), false, false, false);
    if (headerRowTemplate?.Nodes[columnIndex] is TextData node1)
      DocumentGeneratorHelper.SetupHeaderCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, headerRowTemplate, node1);
    if (!(dataRowTemplate?.Nodes[columnIndex] is TextData node2))
      return;
    DocumentGeneratorHelper.SetupDataCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, dataRowTemplate, node2);
  }

  private static void InsertNewTableColumn(
    TableData table,
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex)
  {
    table.InsertNewGridColumn(columnIndex, new RowColParams(table, true, -1, columns[columnIndex].Caption, columns[columnIndex].Width), false, false, false);
    int headersCount = table.HeadersCount;
    if (headersCount > 0 && table.Nodes[0] is TableData node1 && node1.GridColumnsParams == table.GridColumnsParams && node1.Nodes[columnIndex] is TextData node2)
      DocumentGeneratorHelper.SetupHeaderCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, node1, node2);
    foreach (TableData dataRowTemplate in table.Nodes.Skip<DocumentTreeNode>(headersCount).OfType<TableData>())
    {
      if (dataRowTemplate.GridColumnsParams == table.GridColumnsParams && dataRowTemplate.Nodes[columnIndex] is TextData node3)
        DocumentGeneratorHelper.SetupDataCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, dataRowTemplate, node3);
    }
  }

  private static void SetupHeaderCellColumnSettings(
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex,
    TableData headerRowTemplate,
    TextData headerCell)
  {
    DocumentGeneratorHelper.CopyCellSettings(DocumentGeneratorHelper.GetOriginalSettingsCell(headerRowTemplate, columnIndex, originalSettingsCellIndex), headerCell);
    headerCell.AssignText(columns[columnIndex].Caption, false, false, false, false, false);
  }

  private static void SetupDataCellColumnSettings(
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex,
    TableData dataRowTemplate,
    TextData dataCell)
  {
    DocumentGeneratorHelper.CopyCellSettings(DocumentGeneratorHelper.GetOriginalSettingsCell(dataRowTemplate, columnIndex, originalSettingsCellIndex), dataCell);
    dataCell.Name = columns[columnIndex].Caption;
    dataCell.ParagraphFormat.HorzAlignment = new HorzAlignment?(columns[columnIndex].TextAlignment);
  }

  private static TextData GetOriginalSettingsCell(
    TableData row,
    int destinationCellIndex,
    int? originalSettingsCellIndex)
  {
    int index = originalSettingsCellIndex ?? destinationCellIndex - 1;
    if (index < 0)
      return (TextData) null;
    return index >= destinationCellIndex ? (TextData) null : row.Nodes[index] as TextData;
  }

  /// <summary>Настроить столбец в одиночной строке заголовка с собственными столбцами</summary>
  private static void SetupHeaderRowCellParams(
    TableData row,
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex)
  {
    row.GridColumnsParams[columnIndex].ColRowName = columns[columnIndex].Caption;
    if (!(row.Nodes[columnIndex] is TextData node))
      return;
    DocumentGeneratorHelper.SetupHeaderCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, row, node);
  }

  private static void SetupTableColumnParams(
    TableData table,
    IList<TableColumnSettings> columns,
    int columnIndex,
    int? originalSettingsCellIndex)
  {
    table.GridColumnsParams[columnIndex].ColRowName = columns[columnIndex].Caption;
    int headersCount = table.HeadersCount;
    for (int index = 0; index < headersCount; ++index)
    {
      if (table.Nodes[index] is TableData node1 && node1.GridColumnsParams == table.GridColumnsParams && node1.Nodes[columnIndex] is TextData node2)
        DocumentGeneratorHelper.SetupHeaderCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, node1, node2);
    }
    for (int index = headersCount; index < table.NodesCount; ++index)
    {
      if (table.Nodes[index] is TableData node3 && node3.GridColumnsParams == table.GridColumnsParams && node3.Nodes[columnIndex] is TextData node4)
        DocumentGeneratorHelper.SetupDataCellColumnSettings(columns, columnIndex, originalSettingsCellIndex, node3, node4);
    }
  }

  private static void SetupTableColumnWidth(TableData table, float width, int columnIndex)
  {
    table.GridColumnsParams[columnIndex].AssignSize(width, false, false);
    if (!table.IsColumn)
      return;
    foreach (TableData tableData in table.Nodes.OfType<TableData>())
    {
      if (tableData.GridColumnsParams == table.GridColumnsParams && tableData.Nodes[columnIndex] is RectangleElement node)
      {
        node.WidthOverrided = false;
        node.SetNeedUpdateLayoutFlag(true, false, false, false);
      }
    }
  }

  private static void CopyCellSettings(TextData sourceCell, TextData destinationCell)
  {
    if (sourceCell == null)
      return;
    destinationCell.SetParagraphFormat(sourceCell.ParagraphFormat.Clone(), false, false, true);
    destinationCell.SetCharFormat(sourceCell.CharFormat.Clone(), false, false);
    destinationCell.SetOrientation(sourceCell.Orientation, false, false);
    destinationCell.BackColor = sourceCell.BackColor;
    destinationCell.ForeColor = sourceCell.ForeColor;
    destinationCell.AssignReadOnly(sourceCell.ReadOnly);
    destinationCell.Borders = sourceCell.Borders?.Clone();
    if (!(sourceCell is TextBoxElement textBoxElement1) || !(destinationCell is TextBoxElement textBoxElement2))
      return;
    textBoxElement2.AssignAutoSizeHeight(textBoxElement1.AutoSizeHeight, false, false, true);
  }

  public virtual ImDocument GenerateDocument(DataTable sourceDataTable, string tableCaption = "")
  {
    ImDocument doc = new ImDocument(this.DocumentTemplate, true, true);
    this.SetTableCaption(doc, tableCaption);
    TableData defaultDataRowTemplate = DocumentGeneratorHelper.FindDefaultDataRowTemplate(doc);
    TableData nodeFromTemplate = (TableData) doc.FindFirstNodeFromTemplate((DocumentTreeNode) defaultDataRowTemplate.TopLevelTable);
    for (int index1 = 0; index1 < sourceDataTable.Rows.Count; ++index1)
    {
      TableData tableData = (TableData) nodeFromTemplate.InsertRowByTemplate(index1, (RectangleElement) defaultDataRowTemplate);
      for (int index2 = 0; index2 < tableData.NodesCount && index2 < sourceDataTable.Columns.Count; ++index2)
        ((TextData) tableData.Nodes[index2]).AssignText(sourceDataTable.Rows[index1][index2].ToString(), false, false, false, false, false);
    }
    doc.UpdateLayout(false);
    return doc;
  }

  /// <summary>
  /// Устанавливаем заголовок отчета, если юзер указывает какой-то конкретный
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="tableCaption"></param>
  protected void SetTableCaption(ImDocument doc, string tableCaption)
  {
    if (!(tableCaption != string.Empty) || !(doc.FindNode("Заголовок отчёта") is TextData node))
      return;
    node.AssignText(tableCaption, false, false, false, false, false);
  }

  protected static TableData FindDefaultDataRowTemplate(ImDocument doc)
  {
    return doc != null ? (TableData) doc.FindFirstMainTableTemplate().FindFirstCellInDataFlow() : throw new ArgumentNullException(nameof (doc));
  }

  public void Dispose() => this.DocumentTemplate?.Dispose();
}
