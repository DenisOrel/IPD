// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ReportFormer
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client.Reports;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Формирователь отчета</summary>
internal class ReportFormer
{
  /// <summary>Параметры отчета</summary>
  private ReportParameters _reportParameters;
  /// <summary>Таблица отчета</summary>
  private TableReport _tableReport;
  /// <summary>Таблица с данными отчета</summary>
  private DataTable _dataTable;
  private ReportFormer.AdditionalConditions _additionalConditions;

  /// <summary>Constructor</summary>
  /// <param name="reportParams">Параметры отчета</param>
  public ReportFormer(ReportParameters reportParams)
  {
    this._reportParameters = reportParams;
    this._tableReport = new TableReport();
    this._tableReport.LoadData(this._reportParameters.ReportID);
    this._dataTable = new DataTable();
  }

  /// <summary>Сформировать отчет</summary>
  public void Execute(ShowReport showReport)
  {
    if (this._tableReport.Columns == null || this._tableReport.Columns.Count == 0)
    {
      int num1 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Document.Client_29"), (object) this._tableReport.ReportName), MessageBoxButtons.OK, IMMessageBoxImage.Error);
    }
    else
    {
      ImDocument template = (ImDocument) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        template = DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObjectActual(this._tableReport.TemplateID, true), -1, false, true, false);
      if (template == null)
      {
        int num2 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Document.Client_30"), (object) this._tableReport.ReportName), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
      else if (!(template.FindNode("table") is TableElement node14))
      {
        int num3 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Document.Client_31"), (object) this._tableReport.ReportName), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
      else if (!(template.FindNode("table_header") is TableElement node13))
      {
        int num4 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Document.Client_32"), (object) this._tableReport.ReportName), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
      else if (!(template.FindNode("table_row") is TableElement node12))
      {
        int num5 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Document.Client_33"), (object) this._tableReport.ReportName), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
      else
      {
        this._dataTable = this.FormingData();
        if (template.FindNode("header") is TextData node1)
          node1.AssignText(this._tableReport.ReportCaption, false, false, false);
        if (this._tableReport.DatePrint != DatePrintFormats.None)
        {
          DatePrintFormatValue[] customAttributes = (DatePrintFormatValue[]) this._tableReport.DatePrint.GetType().GetField(this._tableReport.DatePrint.ToString()).GetCustomAttributes(typeof (DatePrintFormatValue), false);
          if (customAttributes.Length != 0 && customAttributes[0].FormatString != string.Empty && template.FindNode("date") is TextData node2)
          {
            string str = LocalizationHolder.rm.GetString("Document.Client_34") + DateTime.Now.ToString(customAttributes[0].FormatString);
            node2.AssignText(str, false, false, false);
          }
        }
        if (this._tableReport.CountItems)
        {
          string str = this._dataTable.Rows.Count.ToString();
          int int32 = Convert.ToInt32(str.Substring(str.Length - 1));
          string empty = string.Empty;
          if (this._dataTable.Rows.Count >= 11 && this._dataTable.Rows.Count < 15)
          {
            empty = LocalizationHolder.rm.GetString("Document.Client_36");
          }
          else
          {
            switch (int32)
            {
              case 0:
              case 5:
              case 6:
              case 7:
              case 8:
              case 9:
                empty = LocalizationHolder.rm.GetString("Document.Client_39");
                break;
              case 1:
                empty = LocalizationHolder.rm.GetString("Document.Client_37");
                break;
              case 2:
              case 3:
              case 4:
                empty = LocalizationHolder.rm.GetString("Document.Client_38");
                break;
            }
          }
          if (template.FindNode("count_row") is TextData node3)
            node3.AssignText(LocalizationHolder.rm.GetString("Document.Client_40") + str + empty, false, false, false);
        }
        TextData node4 = template.FindNode("page_number") as TextData;
        TextData node5 = template.FindNode("page_number_2") as TextData;
        if (node4 != null)
        {
          ParagraphFormat paragraphFormat = node4.ParagraphFormat.Clone();
          switch (this._tableReport.PageNumber)
          {
            case PageNumberPosition.None:
              node4.Visible = false;
              break;
            case PageNumberPosition.DownLeft:
              node4.Visible = true;
              paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Left);
              break;
            case PageNumberPosition.DownCenter:
              node4.Visible = true;
              paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
              break;
            case PageNumberPosition.DownRight:
              node4.Visible = true;
              paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Right);
              break;
          }
          node4.SetParagraphFormat(paragraphFormat, false, false, true);
          if (node5 != null)
          {
            node5.Visible = node4.Visible;
            node5.SetParagraphFormat(paragraphFormat, false, false, true);
          }
        }
        TextData baseHeaderCell = (TextData) null;
        TextData baseRowCell = (TextData) null;
        TextBoxElement txtEditor = (TextBoxElement) null;
        TextBoxElement baseTxtEditor = (TextBoxElement) null;
        TextBoxElement baseHeaderTxtEditor = (TextBoxElement) null;
        RectangleF bounds = node14.bounds;
        float width1 = node14.Size.Width;
        int num6 = 0;
        bool flag = this._tableReport.RowNumbers && this._tableReport.RowNumbersColumnWidth > 0;
        if (flag)
        {
          if ((double) this._tableReport.RowNumbersColumnWidth > (double) width1 / 2.0)
            throw new Exception("Колонка с порядковыми номерами слишком широка для отображения!");
          width1 -= (float) this._tableReport.RowNumbersColumnWidth;
          this.AddColumns(node14, node13, node12, node1, ref baseHeaderCell, txtEditor, ref baseHeaderTxtEditor, ref baseRowCell, ref baseTxtEditor, bounds, 0, (float) this._tableReport.RowNumbersColumnWidth, this._tableReport.RowNumbersColumnCaption, HorzAlignment.Right);
          ++num6;
        }
        for (int index = num6; index < this._tableReport.Columns.Count + num6; ++index)
        {
          ReportColumn column = this._tableReport.GetColumn(index - num6);
          float width2 = (float) ((double) column.Width * (double) width1 / 100.0);
          this.AddColumns(node14, node13, node12, node1, ref baseHeaderCell, txtEditor, ref baseHeaderTxtEditor, ref baseRowCell, ref baseTxtEditor, bounds, index, width2, column.Caption, column.Alignment);
        }
        template.UpdateLayout(0, true, false);
        ImDocument imDocument = new ImDocument(template, true, true);
        imDocument.Name = this._tableReport.ReportName;
        TableElement node6 = imDocument.FindNode("table") as TableElement;
        for (int index1 = 0; index1 < this._dataTable.Rows.Count; ++index1)
        {
          TableElement child = (TableElement) node12.CloneFromTemplate(true, true);
          int num7 = 0;
          if (flag)
          {
            if (child.Nodes[0] is TextData node7)
            {
              string str = (index1 + 1).ToString();
              node7.AssignText(str, false, false, false);
            }
            ++num7;
          }
          for (int index2 = num7; index2 < child.Nodes.Count + num7; ++index2)
          {
            ReportColumn column = this._tableReport.GetColumn(index2 - num7);
            if (column != null)
            {
              string str = ReportValuesFormatter.Format(column.AttributeType, column.FormatString, this._dataTable.Rows[index1][index2 - num7]);
              if (child.Nodes[index2] is TextData node8)
                node8.AssignText(str, false, false, false);
            }
          }
          node6.AddChildNode((DocumentTreeNode) child, false, false);
        }
        if (this._tableReport.ResultItem)
        {
          TableElement child1 = (TableElement) node12.CloneFromTemplate(true, true);
          if (child1.Nodes[!flag || this._dataTable.Columns.Count <= 0 ? 0 : 1] is TextData node9)
          {
            ParagraphFormat paragraphFormat = node9.ParagraphFormat.Clone();
            paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Left);
            node9.SetParagraphFormat(paragraphFormat, false, false, true);
            node9.SetCharFormat(node9.CharFormat.Clone(), false, false);
            node9.CharFormat.BoldItalic = new BoldItalicStyle?(BoldItalicStyle.Bold);
            node9.AssignText(LocalizationHolder.rm.GetString("Document.Client_41"), false, false, false);
          }
          for (int index = 0; index < child1.Nodes.Count - 1; ++index)
          {
            if (child1.Nodes[index] is TextData node10)
              node10.RightBorderLine = new BorderLine(BorderStyles.None);
          }
          node6.AddChildNode((DocumentTreeNode) child1, false, false);
          TableElement child2 = (TableElement) node12.CloneFromTemplate(true, true);
          int num8 = flag ? 1 : 0;
          for (int index = num8; index < this._tableReport.Columns.Count + num8; ++index)
          {
            ReportColumn column = this._tableReport.GetColumn(index - num8);
            string str = string.Empty;
            if (column.Result != MathTotal.None)
            {
              switch (column.Result)
              {
                case MathTotal.Summ:
                  str = this.MatchFunction(column, MathTotal.Summ);
                  break;
                case MathTotal.Mean:
                  str = this.MatchFunction(column, MathTotal.Mean);
                  break;
                case MathTotal.Min:
                  str = this.MatchFunction(column, MathTotal.Min);
                  break;
                case MathTotal.Max:
                  str = this.MatchFunction(column, MathTotal.Max);
                  break;
              }
            }
            if (child2.Nodes[index] is TextData node11)
            {
              node11.SetCharFormat(node11.CharFormat.Clone(), false, false);
              node11.CharFormat.BoldItalic = new BoldItalicStyle?(BoldItalicStyle.Bold);
              ParagraphFormat paragraphFormat = node11.ParagraphFormat.Clone();
              paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Right);
              node11.SetParagraphFormat(paragraphFormat, false, false, true);
              node11.AssignText(str, false, false, false);
            }
          }
          node6.AddChildNode((DocumentTreeNode) child2, false, false);
        }
        imDocument.UpdateLayout(0, true, false);
        int documentObjectType = string.IsNullOrWhiteSpace(this._tableReport.GeneratedDocTypeGuid) ? -1 : MetaDataHelper.GetObjectTypeID(this._tableReport.GeneratedDocTypeGuid);
        switch (showReport)
        {
          case ShowReport.InDoc:
            if (documentObjectType != -1)
              DocumentEditorPlugin.SaveDocumentInNewDBObject(imDocument, documentObjectType, true);
            ImDocumentEditorForm documentEditorForm1 = DocumentEditorPlugin.Instance.OpenDocument((DocumentTreeNode) imDocument, false, true);
            documentEditorForm1.Document.Modified = false;
            documentEditorForm1.AskForSaveBeforeClose = false;
            documentEditorForm1.Show(DocumentEditorPlugin.DockManager, DockState.Document);
            break;
          case ShowReport.InPreviewWindow:
            if (documentObjectType != -1)
              DocumentEditorPlugin.SaveDocumentInNewDBObject(imDocument, documentObjectType, true);
            ImDocumentEditorForm documentEditorForm2 = DocumentEditorPlugin.Instance.OpenDocument((DocumentTreeNode) imDocument, true, true);
            documentEditorForm2.Document.Modified = false;
            documentEditorForm2.AskForSaveBeforeClose = false;
            documentEditorForm2.Show(DocumentEditorPlugin.DockManager, DockState.Document);
            break;
          case ShowReport.Print:
            PrintDialog printDialog = new PrintDialog();
            imDocument.PrintDocument.PrinterSettings.MinimumPage = 1;
            imDocument.PrintDocument.PrinterSettings.FromPage = 1;
            imDocument.PrintDocument.PrinterSettings.MaximumPage = imDocument.Nodes.Count;
            imDocument.PrintDocument.PrinterSettings.ToPage = imDocument.Nodes.Count;
            printDialog.Document = imDocument.PrintDocument;
            printDialog.AllowSomePages = true;
            printDialog.AllowSelection = false;
            if (printDialog.ShowDialog() != DialogResult.OK)
              break;
            imDocument.PrintDocument.Print();
            break;
        }
      }
    }
  }

  private void AddColumns(
    TableElement docTable,
    TableElement headerRow,
    TableElement docTableRowTemplate,
    TextData cell,
    ref TextData baseHeaderCell,
    TextBoxElement txtEditor,
    ref TextBoxElement baseHeaderTxtEditor,
    ref TextData baseRowCell,
    ref TextBoxElement baseTxtEditor,
    RectangleF oldBounds,
    int index,
    float width,
    string columnCaption,
    HorzAlignment alignment)
  {
    if (docTable.GridColumnsParams != null && index < docTable.GridColumnsParams.Count)
    {
      docTable.GridColumnsParams[index].AssignSize(width, false, false);
      docTable.GridColumnsParams[index].ColRowName = columnCaption;
      baseHeaderCell = headerRow.Nodes[index] as TextData;
      baseHeaderTxtEditor = baseHeaderCell as TextBoxElement;
      baseRowCell = docTableRowTemplate.Nodes[index] as TextData;
      baseRowCell.AssignProperBounds(baseRowCell.properBounds.Location, new SizeF(width, baseRowCell.Size.Height), false, false, false);
      baseTxtEditor = baseRowCell as TextBoxElement;
    }
    else
    {
      docTable.InsertNewGridColumn(index, new RowColParams((TableData) docTable, true, -1, columnCaption, width), false, false);
      docTable.SetCellSizes(oldBounds, true, false, false, false, true);
    }
    if (headerRow.Nodes[index] is TextData node1)
    {
      node1.AssignText(columnCaption, false, false, false);
      node1.SetParagraphFormat(baseHeaderCell.ParagraphFormat.Clone(), false, false, true);
      node1.SetCharFormat(baseHeaderCell.CharFormat.Clone(), false, false);
      node1.BackColor = baseHeaderCell.BackColor;
      node1.ForeColor = baseHeaderCell.ForeColor;
      node1.AssignReadOnly(baseHeaderCell.ReadOnly);
      if (baseHeaderCell.Borders != null)
        node1.Borders = baseHeaderCell.Borders.Clone();
      else
        node1.Borders = (RectangleBorder) null;
      txtEditor = node1 as TextBoxElement;
      if (txtEditor != null && baseHeaderTxtEditor != null)
        txtEditor.AssignAutoSizeHeight(baseHeaderTxtEditor.AutoSizeHeight, false, false, true);
    }
    if (!(docTableRowTemplate.Nodes[index] is TextData node2))
      return;
    ParagraphFormat paragraphFormat = baseRowCell.ParagraphFormat.Clone();
    paragraphFormat.HorzAlignment = new HorzAlignment?(alignment);
    node2.SetParagraphFormat(paragraphFormat, false, false, true);
    node2.SetCharFormat(baseRowCell.CharFormat.Clone(), false, false);
    node2.BackColor = baseRowCell.BackColor;
    node2.ForeColor = baseRowCell.ForeColor;
    node2.AssignReadOnly(baseRowCell.ReadOnly);
    if (baseRowCell.Borders != null)
      node2.Borders = baseRowCell.Borders.Clone();
    else
      node2.Borders = (RectangleBorder) null;
    txtEditor = node2 as TextBoxElement;
    if (txtEditor == null || baseTxtEditor == null)
      return;
    txtEditor.AssignAutoSizeHeight(baseTxtEditor.AutoSizeHeight, false, false, true);
  }

  /// <summary>Вычисление итога</summary>
  /// <param name="reportColumn">Колонка отчета</param>
  /// <param name="mathTolal">Операция</param>
  /// <returns></returns>
  private string MatchFunction(ReportColumn reportColumn, MathTotal mathTolal)
  {
    int num1 = 0;
    try
    {
      if (reportColumn.AttributeType == FieldTypes.ftMeasured)
      {
        MeasuredValue measuredValue = (MeasuredValue) null;
        bool flag = true;
        foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
        {
          if (CompareValuesHelper.NormalizedValue(row[reportColumn.Index]) != null)
          {
            MeasuredValue baseMeasure = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(row[reportColumn.Index].ToString()));
            if (flag)
            {
              if (baseMeasure != null)
              {
                measuredValue = baseMeasure;
                flag = false;
              }
            }
            else
            {
              switch (mathTolal)
              {
                case MathTotal.Summ:
                  measuredValue = MeasureHelper.Add(measuredValue, baseMeasure);
                  break;
                case MathTotal.Mean:
                  measuredValue = MeasureHelper.Add(measuredValue, baseMeasure);
                  break;
                case MathTotal.Min:
                  if (baseMeasure.Value < measuredValue.Value)
                  {
                    measuredValue = baseMeasure;
                    break;
                  }
                  break;
                case MathTotal.Max:
                  if (baseMeasure.Value > measuredValue.Value)
                  {
                    measuredValue = baseMeasure;
                    break;
                  }
                  break;
              }
            }
            ++num1;
          }
        }
        if (measuredValue == null)
          return string.Empty;
        if (mathTolal == MathTotal.Mean && num1 > 0)
          measuredValue = new MeasuredValue(measuredValue.Value / (double) num1, measuredValue.MeasureID);
        return num1 > 0 ? ReportValuesFormatter.Format(reportColumn.AttributeType, reportColumn.FormatString, (object) measuredValue) : string.Empty;
      }
      System.Type type = Helper.ConvertType(reportColumn.AttributeType);
      if (type != (System.Type) null)
      {
        if (type == typeof (int) || type == typeof (long))
        {
          long fieldValue1 = long.MinValue;
          bool flag = true;
          foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
          {
            if (CompareValuesHelper.NormalizedValue(row[reportColumn.Index]) != null)
            {
              long int64 = Convert.ToInt64(row[reportColumn.Index]);
              if (flag)
              {
                fieldValue1 = int64;
                flag = false;
              }
              else
              {
                switch (mathTolal)
                {
                  case MathTotal.Summ:
                    fieldValue1 += int64;
                    break;
                  case MathTotal.Mean:
                    fieldValue1 += int64;
                    break;
                  case MathTotal.Min:
                    if (int64 < fieldValue1)
                    {
                      fieldValue1 = int64;
                      break;
                    }
                    break;
                  case MathTotal.Max:
                    if (int64 > fieldValue1)
                    {
                      fieldValue1 = int64;
                      break;
                    }
                    break;
                }
              }
              ++num1;
            }
          }
          if (mathTolal != MathTotal.Mean || num1 <= 0)
            return num1 > 0 ? ReportValuesFormatter.Format(reportColumn.AttributeType, reportColumn.FormatString, (object) fieldValue1) : string.Empty;
          double fieldValue2 = Math.Round((double) fieldValue1 / (double) num1, 2);
          return ReportValuesFormatter.Format(reportColumn.AttributeType, reportColumn.FormatString, (object) fieldValue2);
        }
        if (type == typeof (double) || type == typeof (float))
        {
          double fieldValue = double.MaxValue;
          bool flag = true;
          foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
          {
            if (CompareValuesHelper.NormalizedValue(row[reportColumn.Index]) != null)
            {
              double num2 = Convert.ToDouble(row[reportColumn.Index], (IFormatProvider) CultureInfo.CurrentCulture);
              if (flag)
              {
                fieldValue = num2;
                flag = false;
              }
              else
              {
                switch (mathTolal)
                {
                  case MathTotal.Summ:
                    fieldValue += num2;
                    break;
                  case MathTotal.Mean:
                    fieldValue += num2;
                    break;
                  case MathTotal.Min:
                    if (num2 < fieldValue)
                    {
                      fieldValue = num2;
                      break;
                    }
                    break;
                  case MathTotal.Max:
                    if (num2 > fieldValue)
                    {
                      fieldValue = num2;
                      break;
                    }
                    break;
                }
              }
              ++num1;
            }
          }
          if (mathTolal == MathTotal.Mean && num1 > 0)
            fieldValue = Math.Round(fieldValue / (double) num1, 2);
          return num1 > 0 ? ReportValuesFormatter.Format(reportColumn.AttributeType, reportColumn.FormatString, (object) fieldValue) : string.Empty;
        }
        if (type == typeof (DateTime))
        {
          DateTime fieldValue = DateTime.MaxValue;
          bool flag = true;
          foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
          {
            if (CompareValuesHelper.NormalizedValue(row[reportColumn.Index]) != null)
            {
              DateTime dateTime = Convert.ToDateTime(row[reportColumn.Index]);
              if (flag)
              {
                fieldValue = dateTime;
                flag = false;
              }
              else
              {
                switch (mathTolal)
                {
                  case MathTotal.Min:
                    if (dateTime < fieldValue)
                    {
                      fieldValue = dateTime;
                      break;
                    }
                    break;
                  case MathTotal.Max:
                    if (dateTime > fieldValue)
                    {
                      fieldValue = dateTime;
                      break;
                    }
                    break;
                }
              }
              ++num1;
            }
          }
          return num1 > 0 ? ReportValuesFormatter.Format(reportColumn.AttributeType, reportColumn.FormatString, (object) fieldValue) : string.Empty;
        }
      }
      return string.Empty;
    }
    catch
    {
      return string.Empty;
    }
  }

  private object NormalizeValue(object val) => val == null ? (object) DBNull.Value : val;

  /// <summary>Запрос в базу</summary>
  /// <returns></returns>
  private DataTable FormingData()
  {
    DataTable table = new DataTable();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._reportParameters.ReportID);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType);
      table.TableName = $"{objectType.ObjectInstanceName} \"{dbObject.Caption}\"";
      int index1 = -1;
      bool flag1 = false;
      int index2 = -1;
      bool flag2 = false;
      Dictionary<int, IColumnHandler> dictionary = new Dictionary<int, IColumnHandler>(1);
      int num = 0;
      IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
      for (int index3 = 0; index3 < this._tableReport.Columns.Count; ++index3)
      {
        ReportColumn column1 = this._tableReport.GetColumn(index3);
        Guid schemeGuid = Guid.Empty;
        switch (column1.AttributeSource)
        {
          case AttributeSourceTypes.Object:
            schemeGuid = column1.AttributeID < 0 ? Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid : Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
            break;
          case AttributeSourceTypes.Relation:
            schemeGuid = column1.AttributeID < 0 ? Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid : Intermech.Navigator.Consts.RelationColumnSchemeGuid;
            break;
        }
        if (schemeGuid == Guid.Empty)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Document.Client_138"), (object) column1.AttributeName));
        object columnID = column1.AttributeID >= 0 ? (object) column1.AttributeID : (object) (ObligatoryObjectAttributes) column1.AttributeID;
        NodeColumn column2 = service.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, 0);
        INodeColumnTransform defaultTransform = service.GetDefaultTransform(schemeGuid, columnID);
        this._reportParameters.Query.AddColumn(column2, defaultTransform);
        System.Type dataType;
        if (defaultTransform == null)
        {
          switch (column1.AttributeType)
          {
            case FieldTypes.ftInteger:
            case FieldTypes.ftAutoInc:
              dataType = typeof (long);
              break;
            case FieldTypes.ftDouble:
              dataType = typeof (double);
              break;
            case FieldTypes.ftDateTime:
              dataType = typeof (DateTime);
              break;
            case FieldTypes.ftBoolean:
              dataType = typeof (bool);
              break;
            default:
              dataType = typeof (string);
              break;
          }
        }
        else
          dataType = defaultTransform.DataType;
        DataColumn column3 = new DataColumn(column1.Caption, dataType);
        PropertyCollection extendedProperties = column3.ExtendedProperties;
        System.Type key = typeof (ColumnAttributeData);
        int attributeId = column1.AttributeID;
        int attributeType1 = (int) column1.AttributeType;
        int attributeSource = (int) column1.AttributeSource;
        int sortOrder = (int) column1.SortOrder;
        int orderBy;
        if (column1.SortOrder == SortOrders.NONE)
          orderBy = 0;
        else
          num = orderBy = num + 1;
        ColumnAttributeData columnAttributeData = new ColumnAttributeData(attributeId, (FieldTypes) attributeType1, (AttributeSourceTypes) attributeSource, (SortOrders) sortOrder, orderBy);
        extendedProperties.Add((object) key, (object) columnAttributeData);
        table.Columns.Add(column3);
        if (column1.AttributeID == -2)
        {
          index1 = index3;
          flag1 = true;
        }
        else if (column1.AttributeID == -20)
        {
          index2 = index3;
          flag2 = true;
        }
        IDBAttributeType attributeType2 = sessionKeeper.Session.GetAttributeType(column1.AttributeID, false);
        if (attributeType2 != null)
        {
          if (column1.AttributeType == FieldTypes.ftMemo)
            dictionary.Add(column1.AttributeID, (IColumnHandler) new MemoColumnHandler());
          else if (attributeType2.MultipleValued == MultiValueModes.MultiValues || attributeType2.MultipleValued == MultiValueModes.MultiValuesFromList)
            dictionary.Add(column1.AttributeID, (IColumnHandler) new MultiValuesColumnHandler());
        }
      }
      if (!flag1)
      {
        this._reportParameters.Query.AddColumn(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID, NodeColumnSortOrder.None, -1), (INodeColumnTransform) null);
        DataColumn column = new DataColumn("F_OBJECT_ID", typeof (long));
        index1 = table.Columns.Count;
        column.ExtendedProperties.Add((object) typeof (ColumnAttributeData), (object) new ColumnAttributeData(-2, FieldTypes.ftSystem, AttributeSourceTypes.Object));
        table.Columns.Add(column);
      }
      if (!flag2)
      {
        this._reportParameters.Query.AddColumn(service.CreateColumn(Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_PRJLINK_ID, NodeColumnSortOrder.None, -1), (INodeColumnTransform) null);
        DataColumn column = new DataColumn("F_PRJLINK_ID", typeof (long));
        index2 = table.Columns.Count;
        column.ExtendedProperties.Add((object) typeof (ColumnAttributeData), (object) new ColumnAttributeData(-20, FieldTypes.ftSystem, AttributeSourceTypes.Relation));
        table.Columns.Add(column);
      }
      if (this._reportParameters.SelectedItemsOnly)
      {
        List<long> acceptObjects = new List<long>(this._reportParameters.SelectedItems.Count);
        for (int index4 = 0; index4 < this._reportParameters.SelectedItems.Count; ++index4)
        {
          IDBTypedObjectID itemData = (IDBTypedObjectID) this._reportParameters.SelectedItems.GetItemData(index4, typeof (IDBTypedObjectID));
          acceptObjects.Add(itemData.ObjectID);
        }
        acceptObjects.Sort();
        this._additionalConditions = new ReportFormer.AdditionalConditions(sessionKeeper.Session.SessionGUID, new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) acceptObjects.ToArray(), LogicalOperators.AND, 0, false)
        }, acceptObjects);
      }
      QueryEvents.BeforeClientRecordsSelectEvent += new BeforeClientRecordsSelectHandler(this.QueryEvents_BeforeClientRecordsSelectEvent);
      try
      {
        this._reportParameters.Query.Execute((object) null, 2147483646);
      }
      finally
      {
        QueryEvents.BeforeClientRecordsSelectEvent -= new BeforeClientRecordsSelectHandler(this.QueryEvents_BeforeClientRecordsSelectEvent);
      }
      if (this._reportParameters.Query.RecordCount > 0)
      {
        for (int index5 = 0; index5 < this._reportParameters.Query.RecordCount; ++index5)
        {
          object[] recordValues = this._reportParameters.Query.GetRecordValues(index5);
          DataRow row = table.NewRow();
          long int64_1 = recordValues[index1] != DBNull.Value ? Convert.ToInt64(recordValues[index1]) : 0L;
          long int64_2 = recordValues[index2] != DBNull.Value ? Convert.ToInt64(recordValues[index2]) : 0L;
          if (this._additionalConditions == null || this._additionalConditions.Handled || int64_1 == 0L || this._additionalConditions.AcceptObjects.BinarySearch(int64_1) >= 0)
          {
            for (int index6 = 0; index6 < table.Columns.Count; ++index6)
            {
              ColumnAttributeData extendedProperty = (ColumnAttributeData) table.Columns[index6].ExtendedProperties[(object) typeof (ColumnAttributeData)];
              IColumnHandler columnHandler = (IColumnHandler) null;
              row[index6] = !dictionary.TryGetValue(extendedProperty.AttributeID, out columnHandler) ? this.NormalizeValue(recordValues[index6]) : this.NormalizeValue(columnHandler.GetValue(new ReportItemInfo(int64_1, int64_2, extendedProperty.AttributeID, extendedProperty.AttributeType, extendedProperty.AttributeSource), recordValues[index6]));
            }
            table.Rows.Add(row);
          }
        }
        table = ((ISortService) ServicesManager.GetService(typeof (ISortService))).SortTable(table);
      }
      this._additionalConditions = (ReportFormer.AdditionalConditions) null;
      if (!flag2)
        table.Columns.RemoveAt(index2);
      if (!flag1)
        table.Columns.RemoveAt(index1);
      table.AcceptChanges();
    }
    return table;
  }

  private void QueryEvents_BeforeClientRecordsSelectEvent(
    object sender,
    BeforeClientRecordsSelectEventArgs args)
  {
    if (this._additionalConditions == null || !this._additionalConditions.SessionGuid.Equals(args.Session.SessionGUID))
      return;
    if (args.NewParameters.HasValue)
    {
      DBRecordSetParams dbRecordSetParams = args.NewParameters.Value;
      dbRecordSetParams.Conditions = ConditionStructure.Join(this._additionalConditions.Conditions, dbRecordSetParams.Conditions);
      args.NewParameters = new DBRecordSetParams?(dbRecordSetParams);
    }
    else
    {
      args.OldParameters.Conditions = ConditionStructure.Join(this._additionalConditions.Conditions, args.OldParameters.Conditions);
      args.NewParameters = new DBRecordSetParams?(args.OldParameters);
    }
    this._additionalConditions.Handled = true;
  }

  private class AdditionalConditions
  {
    public Guid SessionGuid;
    public bool Handled;
    public ConditionStructure[] Conditions;
    public List<long> AcceptObjects;

    public AdditionalConditions(
      Guid sessionGuid,
      ConditionStructure[] conditions,
      List<long> acceptObjects)
    {
      this.SessionGuid = sessionGuid;
      this.Conditions = conditions;
      this.AcceptObjects = acceptObjects;
      this.Handled = false;
    }
  }
}
