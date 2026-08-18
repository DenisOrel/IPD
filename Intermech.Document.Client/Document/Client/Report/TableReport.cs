// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReport
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Таблица отчета</summary>
internal class TableReport
{
  /// <summary>ID шаблона</summary>
  public long TemplateID;
  /// <summary>Название шаблона</summary>
  public string TemplateName;
  /// <summary>Наименование отчета</summary>
  public string ReportName;
  /// <summary>Заголовок отчета</summary>
  public string ReportCaption;
  /// <summary>Колонки</summary>
  public List<ReportColumn> Columns = new List<ReportColumn>();
  /// <summary>Номера строк</summary>
  public bool RowNumbers;
  /// <summary>Штрина колонки с номерами строк, в мм</summary>
  public int RowNumbersColumnWidth;
  /// <summary>Заголовок колонки с номерами строк</summary>
  public string RowNumbersColumnCaption = string.Empty;
  /// <summary>Итоговую строку</summary>
  public bool ResultItem;
  /// <summary>Количество позиций</summary>
  public bool CountItems;
  /// <summary>Дату распечатки</summary>
  public DatePrintFormats DatePrint;
  /// <summary>Номера страниц</summary>
  public PageNumberPosition PageNumber;
  /// <summary>Гуид типа генерируемого по умолчанию документа</summary>
  public string GeneratedDocTypeGuid = string.Empty;

  /// <summary>Формирование данными из БД</summary>
  /// <param name="reportID"></param>
  public void LoadData(long reportID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int[] attributesID = new int[6]
      {
        MetaDataHelper.GetAttributeTypeID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")),
        ReportGuids.AttrTemplateId,
        ReportGuids.AttributeGeneratedDocTypeId,
        ReportGuids.AttributeColumnsId,
        ReportGuids.AttributeReportCaptionId,
        ReportGuids.AttributeParametersId
      };
      AttributeValues[] attributesValues = sessionKeeper.Session.GetObjectAttributesValues(reportID, attributesID, GetAttributeValuesModes.None, false);
      AttributeValues attributeById1 = AttributeValuesHelper.GetAttributeByID(attributesValues, MetaDataHelper.GetAttributeTypeID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")), reportID, true);
      if (attributeById1.Value != null && attributeById1.Value != DBNull.Value && attributeById1.AsString != string.Empty)
        this.ReportName = attributeById1.AsString;
      AttributeValues attributeById2 = AttributeValuesHelper.GetAttributeByID(attributesValues, ReportGuids.AttrTemplateId, reportID, true);
      if (attributeById2.Value != null && attributeById2.Value != DBNull.Value && attributeById2.AsString != string.Empty)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(attributeById2.AsInteger);
        this.TemplateID = dbObject.ObjectID;
        this.TemplateName = dbObject.Caption;
      }
      AttributeValues attributeById3 = AttributeValuesHelper.GetAttributeByID(attributesValues, ReportGuids.AttributeGeneratedDocTypeId, reportID, true);
      if (attributeById3.Value != null && attributeById3.Value != DBNull.Value && attributeById3.AsString != string.Empty)
        this.GeneratedDocTypeGuid = attributeById3.AsString;
      AttributeValues attributeById4 = AttributeValuesHelper.GetAttributeByID(attributesValues, ReportGuids.AttributeColumnsId, reportID, true);
      this.Columns.Clear();
      if (attributeById4.Value != null && attributeById4.Value != DBNull.Value && attributeById4.AsString != string.Empty)
      {
        int num = 0;
        foreach (object obj in attributeById4.Values)
        {
          ReportColumn reportColumn = new ReportColumn(Convert.ToString(obj), sessionKeeper.Session);
          if (reportColumn.AttributeID != 0)
          {
            this.Columns.Add(reportColumn);
            num += reportColumn.Width;
          }
        }
        if (num > 100)
        {
          IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(reportID, (object) ReportGuids.AttributeColumnsId, false, false);
          for (int index = 0; index < this.Columns.Count; ++index)
          {
            this.Columns[index].Width = (int) Math.Floor((double) (this.Columns[index].Width * 100) / (double) num);
            objectAttribute.Index = index;
            objectAttribute.Value = (object) this.Columns[index].ToValue();
          }
        }
      }
      AttributeValues attributeById5 = AttributeValuesHelper.GetAttributeByID(attributesValues, ReportGuids.AttributeReportCaptionId, reportID, true);
      this.ReportCaption = attributeById5.Value == null || attributeById5.Value == DBNull.Value || !(attributeById5.AsString != string.Empty) ? string.Empty : attributeById5.AsString;
      AttributeValues attributeById6 = AttributeValuesHelper.GetAttributeByID(attributesValues, ReportGuids.AttributeParametersId, reportID, true);
      if (attributeById6 == null || attributeById6.Value == null || attributeById6.Value == DBNull.Value || !(attributeById6.AsString != string.Empty))
        return;
      TableReportPropAttProxy reportPropAttProxy = new TableReportPropAttProxy(attributeById6.AsString);
      if (!reportPropAttProxy.IsValid)
        return;
      this.ResultItem = reportPropAttProxy.ResultItem;
      this.CountItems = reportPropAttProxy.CountItems;
      this.DatePrint = reportPropAttProxy.DatePrint;
      this.PageNumber = reportPropAttProxy.PageNumber;
      this.RowNumbers = reportPropAttProxy.RowNumbers;
      this.RowNumbersColumnWidth = reportPropAttProxy.RowNumbersColumnWidth;
      this.RowNumbersColumnCaption = reportPropAttProxy.RowNumbersColumnCaption;
    }
  }

  /// <summary>Получить колонку по индексу</summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public ReportColumn GetColumnByID(int attributeID)
  {
    return this.Columns.Find((Predicate<ReportColumn>) (x => x.AttributeID == attributeID));
  }

  /// <summary>Получить колонку по индексу</summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public ReportColumn GetColumn(int index)
  {
    return this.Columns.Find((Predicate<ReportColumn>) (x => x.Index == index));
  }

  /// <summary>Исключить колонку из отчета</summary>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  public bool DeleteColumn(string fieldName)
  {
    int result;
    if (!int.TryParse(fieldName, out result))
      return false;
    ReportColumn columnById = this.GetColumnByID(result);
    if (columnById == null)
      return false;
    int index = columnById.Index;
    this.Columns.Remove(columnById);
    foreach (ReportColumn column in this.Columns)
    {
      if (column.Index > index)
        --column.Index;
    }
    return true;
  }

  /// <summary>Проверка, включен ли атрибут в коллекцию колонок</summary>
  /// <param name="AttributeID"></param>
  /// <returns></returns>
  public bool IsAttributePresent(int attributeID)
  {
    return this.Columns.Exists((Predicate<ReportColumn>) (x => x.AttributeID == attributeID));
  }
}
