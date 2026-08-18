// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.TableCodeHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Helper;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Imbase.Server.Sync.Services;
using Intermech.ImpExp.Interface;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class TableCodeHandler : CodeHandler
{
  private const int Mask = 67170304 /*0x0400F000*/;
  private bool _terminateOnError;

  public TableCodeHandler(Guid taskGuid, bool terminateOnError)
    : base(taskGuid)
  {
    this._terminateOnError = terminateOnError;
  }

  public override void Handle(EventRecord eventRec, IDataBase sourceDB, IUserSession session)
  {
    TableRecord tableRec = (TableRecord) null;
    if (eventRec.Code != 202)
      tableRec = CodeHandler.GetTableRecord(sourceDB, eventRec.Table);
    if (tableRec == null && eventRec.Code != 202)
    {
      this.AddEventInfo(EventType.Warning, $"Таблица {eventRec.Table} в таблице IM_TABLES базы-источника не найден");
    }
    else
    {
      switch (eventRec.Code)
      {
        case 200:
          this.Add(eventRec, tableRec, sourceDB, session);
          this.ChangeVisibility(tableRec, session);
          break;
        case 202:
          this.Delete(eventRec, session);
          break;
        case 203:
          this.ChangeVisibility(tableRec, session);
          break;
        case 205:
          this.RenameTable(tableRec, session);
          break;
        case 207:
        case 210:
          this.ChangeData(eventRec, tableRec, sourceDB, eventRec.Code == 210, session);
          break;
        case 216:
          this.ChangeImage(tableRec, eventRec.ObjKey, sourceDB, session);
          break;
        case 217:
          this.ChangeNote(tableRec, sourceDB, session);
          break;
      }
    }
  }

  internal void ChangeNote(TableRecord tableRec, IDataBase sourceDB, IUserSession session)
  {
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID), tableRec.Key, 0, out string _);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить описание таблицы. Таблица не найдена в базе-приемнике по коду Imbase {tableRec.Key}");
    }
    else
    {
      IDBObject dbObject = session.GetObject(objectByImbaseCode);
      string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      if (tableRec.TextID == 0)
      {
        dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid))?.Delete(0L);
        this.AddEventInfo(EventType.Text, $"Описание {str} удалено.");
      }
      else
      {
        BlobRecord blobRecord = CodeHandler.CreateBlobRecord(sourceDB, tableRec.TextID);
        if (blobRecord == null)
          return;
        dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false).AsString = blobRecord.Memo;
        this.AddEventInfo(EventType.Text, $"Описание {str} изменено.");
      }
    }
  }

  internal void ChangeImage(
    TableRecord tableRec,
    int imbaseLinkInd,
    IDataBase sourceDB,
    IUserSession session)
  {
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), imbaseLinkInd, 0, out string _);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить изображение таблицы. Ярлык не найден в базе-приемнике по коду Imbase {imbaseLinkInd}");
    }
    else
    {
      long oldPictureObjectId = 0;
      IDBObject dbObject = session.GetObject(objectByImbaseCode);
      string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID));
      if (attributeById != null)
        oldPictureObjectId = attributeById.AsInteger;
      if (tableRec.GraphID == 0)
      {
        if (attributeById == null)
          return;
        try
        {
          attributeById.Delete(0L);
          this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' удален.");
        }
        catch (Exception ex1)
        {
          this.AddEventInfo(EventType.Warning, $"Ошибка при удалении атрибута 'Изображение' для {str}: {ex1.Message}");
          try
          {
            attributeById.Clear();
            this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' очищен.");
          }
          catch (Exception ex2)
          {
            this.AddEventInfo(EventType.Warning, $"Ошибка при очистке атрибута 'Изображение'  для {str}: {ex2.Message}");
          }
        }
      }
      else
      {
        try
        {
          long num = this.AddNewPicture(session, sourceDB, tableRec.GraphID, oldPictureObjectId);
          if (num == 0L)
            return;
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
          if (dbAttribute == null)
            return;
          dbAttribute.Value = (object) num;
          this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' изменен.");
        }
        catch (Exception ex)
        {
          this.AddEventInfo(EventType.Warning, $"Ошибка при добавлении изображения для {str}: {ex.Message}");
        }
      }
    }
  }

  internal void ChangeData(
    EventRecord eventRec,
    TableRecord tableRec,
    IDataBase sourceDB,
    bool onlyDataChanged,
    IUserSession session)
  {
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID), tableRec.Key, 0, out string _);
    if (objectByImbaseCode == 0L)
    {
      this.Add(eventRec, tableRec, sourceDB, session);
      new RecordCodeHandler(this.TaskGuid).Add(eventRec, sourceDB, session);
    }
    else
      this.ChangeData(session, sourceDB, eventRec, tableRec, objectByImbaseCode, onlyDataChanged);
  }

  private void ChangeData(
    IUserSession session,
    IDataBase sourceDB,
    EventRecord eventRec,
    TableRecord tableRec,
    long objTableId,
    bool onlyDataChanged)
  {
    DataSet dataSet = TableLoadHelper.CreateDataSet();
    List<string> badFileNames = new List<string>();
    string fullPath = this.GetFullPath(sourceDB, eventRec);
    string tablePath = string.IsNullOrEmpty(fullPath) ? $"Таблица: '{tableRec.Description}' ({tableRec.TableName})." : $"Каталог: '{fullPath}' таблица: '{tableRec.Description}' ({tableRec.TableName}).";
    try
    {
      DataTable table1 = dataSet.Tables["IMS_ATTR_TYPES"];
      DataTable table2 = dataSet.Tables["IMS_DATA"];
      DataSet tables = TableLoadHelper.GetTables(session, objTableId, true);
      DataTable table3 = tables?.Tables["IMS_DATA"];
      DataTable table4 = tables?.Tables["IMS_ATTR_TYPES"];
      onlyDataChanged = false;
      if (onlyDataChanged)
      {
        onlyDataChanged = false;
        if (tables != null && table4 != null && table4.Rows.Count > 0)
        {
          dataSet.Tables.Remove(table1);
          tables.Tables.Remove(table4);
          dataSet.Tables.Add(table4);
          dataSet.Tables.Remove(table2);
          dataSet.Tables.Add(table3.Clone());
          table2 = dataSet.Tables["IMS_DATA"];
          onlyDataChanged = true;
        }
      }
      DataTable sourceDt = sourceDB.ExecuteDataTable($"SELECT * FROM {tableRec.TableName}");
      switch (tableRec.Openmode)
      {
        case 0:
          this.SetTableData(session, sourceDB, eventRec, tableRec, sourceDt, table1, table2, badFileNames, table3, table4, tablePath, onlyDataChanged);
          break;
        case 2:
          this.SetTableMixData(session, table1, table2, sourceDt, table3, tablePath);
          break;
      }
      TableLoadHelper.StoreData(session, objTableId, dataSet, (ITablesIndexer) null);
      this.AddChangedTableId(objTableId);
    }
    catch (Exception ex)
    {
      throw new Exception($"{ex.Message} {tablePath}", ex);
    }
  }

  private void SetTableMixData(
    IUserSession session,
    DataTable tableAttrs,
    DataTable dataTable,
    DataTable sourceDt,
    DataTable oldData,
    string tablePath)
  {
    DataRow dataRow1 = tableAttrs.NewRow();
    IDBAttributeType attributeType1 = session.GetAttributeType(Intermech.Imbase.Consts.LinkToCompoundObjectAttID);
    this.SetAttrTypesRowParams(dataRow1, attributeType1.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType1.UniqueMode, attributeType1.DefaultValue, attributeType1.Options, attributeType1.Mask, string.Empty);
    tableAttrs.Rows.Add(dataRow1);
    TableLoadHelper.CreateDataColumn(dataTable, attributeType1);
    IDBAttributeType attributeType2 = session.GetAttributeType(Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttID);
    DataRow dataRow2 = tableAttrs.NewRow();
    this.SetAttrTypesRowParams(dataRow2, attributeType2.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType2.UniqueMode, attributeType2.DefaultValue, attributeType2.Options, attributeType2.Mask, string.Empty);
    tableAttrs.Rows.Add(dataRow2);
    TableLoadHelper.CreateDataColumn(dataTable, attributeType2);
    IDBAttributeType attributeType3 = session.GetAttributeType(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"));
    DataRow dataRow3 = tableAttrs.NewRow();
    this.SetAttrTypesRowParams(dataRow3, attributeType3.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType3.UniqueMode, attributeType3.DefaultValue, attributeType3.Options, attributeType3.Mask, string.Empty);
    tableAttrs.Rows.Add(dataRow3);
    dataTable.Columns.Add(attributeType3.GUID.ToString(), AttributesTypeHelper.GetTypeOfAttributeValue(FieldTypes.ftMeasured));
    string columnName1 = Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString();
    string columnName2 = Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString();
    for (int index = 0; index < sourceDt.Rows.Count; ++index)
    {
      DataRow row = dataTable.NewRow();
      string sourceValue1 = Convert.ToString(sourceDt.Rows[index]["F_OWNER"]);
      object imbaseRecordLinkValue1 = CodeHandler.GetImbaseRecordLinkValue(session, (object) sourceValue1);
      row[columnName1] = imbaseRecordLinkValue1;
      string sourceValue2 = Convert.ToString(sourceDt.Rows[index]["F_MIX"]);
      object imbaseRecordLinkValue2 = CodeHandler.GetImbaseRecordLinkValue(session, (object) sourceValue2);
      row[columnName2] = imbaseRecordLinkValue2;
      long measureID = 0;
      string str = Convert.ToString(sourceDt.Rows[index]["F_UNITS"]);
      if (string.IsNullOrEmpty(str) && this.DefaultMeasureId != 0L)
      {
        measureID = this.DefaultMeasureId;
      }
      else
      {
        string newShortMeasureName;
        if (PumpSettings.TryFoundMeasure(str, out newShortMeasureName))
          str = newShortMeasureName;
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(str);
        if (!descriptor.Empty)
          measureID = descriptor.MeasureID;
      }
      if (measureID != 0L)
      {
        double result;
        if (double.TryParse(Convert.ToString(sourceDt.Rows[index]["F_VALUE"]), out result))
        {
          MeasuredValue measuredValue = new MeasuredValue(result, measureID);
          row["cad00267-306c-11d8-b4e9-00304f19f545"] = (object) measuredValue;
        }
        row["F_KEY"] = sourceDt.Rows[index]["F_KEY"];
        row["F_GUID"] = (object) this.GetOrCreateNewRowGuid(oldData, Convert.ToInt32(row["F_KEY"]));
        dataTable.Rows.Add(row);
      }
      else
      {
        this.AddEventInfo(EventType.Warning, $"Невозможно изменить данные в таблице. В {tablePath} не удалось найти единицу измерения по короткому имени = '{str}' и единицу измерения по умолчанию.");
        return;
      }
    }
    tableAttrs.AcceptChanges();
    dataTable.AcceptChanges();
  }

  private void SetTableData(
    IUserSession session,
    IDataBase sourceDB,
    EventRecord eventRec,
    TableRecord tableRec,
    DataTable sourceDt,
    DataTable tableAttrs,
    DataTable dataTable,
    List<string> badFileNames,
    DataTable oldDataTable,
    DataTable oldAttTable,
    string tablePath,
    bool onlyDataChanged)
  {
    FieldRecord[] fields = this.GetFields(sourceDB, tableRec.Key);
    Dictionary<Guid, string> formules;
    this.LinkAttributes(session, fields, sourceDB, eventRec, tableRec.TableName, tableRec.TableName, tableRec.Key, out formules);
    if (!onlyDataChanged)
    {
      foreach (FieldRecord field in fields)
      {
        IDBAttributeType attributeType = session.GetAttributeType(field.GUID);
        if (tableAttrs.Rows.Find((object) field.GUID.ToString()) == null)
        {
          int num1 = (int) ImbaseImpHelper.SetOptionsForAttribute(attributeType, field.EnterMode);
          AttributeOptions options = (AttributeOptions) (field.Flags & 67170304 /*0x0400F000*/) | attributeType.Options;
          DataRow dataRow1 = oldAttTable?.Rows.Find((object) field.GUID.ToString());
          if (dataRow1 != null)
          {
            int num2 = Convert.ToInt32(dataRow1["F_OPTIONS"]) & -67170305;
            options |= (AttributeOptions) num2;
          }
          string formula = (string) null;
          formules?.TryGetValue(field.GUID, out formula);
          RequiredModes addMode = RequiredModes.Manual;
          ComputeValueModes computeMode = ComputeValueModes.NotComputableValue;
          ImbaseImpHelper.FormingComputedFlags(field.EnterMode, !string.IsNullOrEmpty(formula), ref addMode, ref computeMode);
          string empty = string.Empty;
          if (attributeType.AttributeType == FieldTypes.ftMeasured)
          {
            Guid measureGuid = this.GetMeasureGuid(field, tablePath);
            if (measureGuid == Guid.Empty)
            {
              this.AddEventInfo(EventType.Warning, $"Невозможно изменить данные в таблице. В {tablePath} для поля = '{field.LongName}' не удалось найти единицу измерения по короткому имени = '{field.Units}' и единицу измерения по умолчанию.");
              return;
            }
            empty = measureGuid.ToString();
          }
          DataRow dataRow2 = tableAttrs.NewRow();
          this.SetAttrTypesRowParams(dataRow2, field.GUID, addMode, computeMode, formula, attributeType.UniqueMode, attributeType.DefaultValue, options, attributeType.Mask, empty);
          tableAttrs.Rows.Add(dataRow2);
          TableLoadHelper.CreateDataColumn(dataTable, attributeType);
        }
      }
      this.ProcessNaimColumn(tableAttrs);
      tableAttrs.AcceptChanges();
    }
    for (int index1 = 0; index1 < sourceDt.Rows.Count; ++index1)
    {
      DataRow row = dataTable.NewRow();
      for (int index2 = 0; index2 < sourceDt.Columns.Count; ++index2)
      {
        string sourceColumnName = sourceDt.Columns[index2].ColumnName;
        object sourceValue = sourceDt.Rows[index1][sourceColumnName];
        FieldRecord field = ((IEnumerable<FieldRecord>) fields).FirstOrDefault<FieldRecord>((System.Func<FieldRecord, bool>) (x => x.Field == sourceColumnName));
        if (field != null)
        {
          DataColumn column = dataTable.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ColumnName == field.GUID.ToString()));
          if (column != null)
          {
            switch (field.DataMode)
            {
              case ImDataMode.IDM_IMAGE:
                if (!this.GetImageValue(sourceDB, session, badFileNames, ref sourceValue, tablePath, index1))
                  continue;
                break;
              case ImDataMode.IDM_TEXT:
                if (!this.GetNoteValue(sourceDB, session, badFileNames, ref sourceValue, tablePath, index1))
                  continue;
                break;
            }
            switch (field.EnterMode)
            {
              case ImEnterMode.IEM_FOLDER:
                sourceValue = CodeHandler.GetFolderLinkValue(session, sourceValue);
                break;
              case ImEnterMode.IEM_TABLE:
                sourceValue = CodeHandler.GetTableLinkValue(session, sourceValue);
                break;
              case ImEnterMode.IEM_RECORD:
                sourceValue = CodeHandler.GetImbaseRecordLinkValue(session, sourceValue);
                break;
              case ImEnterMode.IEM_SEARCH_DOCUMENT:
                sourceValue = SearchLinksHelper.GetSearchDocumentLinkValue(session, sourceValue);
                break;
              case ImEnterMode.IEM_SEARCH_OBJECT:
                sourceValue = SearchLinksHelper.GetSearchObjectLinkValue(session, sourceValue);
                break;
            }
            if (field.DataType == FieldTypes.ftBoolean)
              sourceValue = (object) this.TryConvertToBool(sourceValue);
            if (typeof (ValuesArray).Equals(column.DataType))
              sourceValue = (object) TableLoadHelper.CreateArray(column.ExtendedProperties[(object) "dataType"] as Type, sourceValue);
            try
            {
              row[column] = sourceValue;
            }
            catch (Exception ex)
            {
              if (this._terminateOnError)
                throw;
              this.AddEventInfo(EventType.Error, $"Невозможно присвоить данные таблицы '{tablePath}' для поля '{field.LongName}'. Значение : '{sourceValue}'. Текст ошибки : {ex.Message}.");
            }
          }
        }
      }
      row["F_KEY"] = sourceDt.Rows[index1]["F_KEY"];
      row["F_GUID"] = (object) this.GetOrCreateNewRowGuid(oldDataTable, Convert.ToInt32(row["F_KEY"]));
      dataTable.Rows.Add(row);
    }
    dataTable.AcceptChanges();
  }

  private bool TryConvertToBool(object sourceValue) => Convert.ToString(sourceValue) == "T";

  private Guid GetMeasureGuid(FieldRecord field, string tablePath)
  {
    Guid measureGuid = Guid.Empty;
    if (!string.IsNullOrEmpty(field.Units))
    {
      string newShortMeasureName;
      if (PumpSettings.TryFoundMeasure(field.Units, out newShortMeasureName))
        field.Units = newShortMeasureName;
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(field.Units);
      if (!descriptor.Empty)
        measureGuid = descriptor.MeasureGuid;
    }
    if (measureGuid == Guid.Empty)
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(this.DefaultMeasureId);
      if (!descriptor.Empty)
      {
        measureGuid = descriptor.MeasureGuid;
        this.AddEventInfo(EventType.Text, $"В {tablePath} для поля = '{field.LongName}' не удалось найти единицу измерения по короткому имени = '{field.Units}'. Будет применена единица измерения по умолчанию.");
      }
    }
    return measureGuid;
  }

  private void ProcessNaimColumn(DataTable tableAttrs)
  {
    TableAttribute tableAttribute = ImbaseImpHelper.CheckNameColumn(tableAttrs.AsEnumerable().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (x => new Guid(Convert.ToString(x["F_ATTRIBUTE_GUID"])))).ToList<Guid>());
    if (tableAttribute == null)
      return;
    IMSAttributeType attrName = MetaDataHelper.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    DataRow newRow = tableAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => new Guid(Convert.ToString(x["F_ATTRIBUTE_GUID"])) == attrName.AttributeGuid));
    if (newRow != null)
    {
      this.SetAttrTypesRowParams(newRow, tableAttribute.AttributeGuid, tableAttribute.AddMode, tableAttribute.ComputeMode, tableAttribute.ImFormula, attrName.Unique, attrName.DefaultValue, attrName.Options, attrName.Mask, string.Empty);
    }
    else
    {
      DataRow dataRow = tableAttrs.NewRow();
      this.SetAttrTypesRowParams(dataRow, tableAttribute.AttributeGuid, tableAttribute.AddMode, tableAttribute.ComputeMode, tableAttribute.ImFormula, attrName.Unique, attrName.DefaultValue, attrName.Options, attrName.Mask, string.Empty);
      tableAttrs.Rows.Add(dataRow);
    }
  }

  private bool GetNoteValue(
    IDataBase sourceDB,
    IUserSession session,
    List<string> badFileNames,
    ref object sourceValue,
    string tablePath,
    int i)
  {
    if (Convert.IsDBNull(sourceValue))
      return false;
    if (sourceValue is string str)
    {
      if (badFileNames.Contains(str))
        return false;
      badFileNames.Add(str);
      this.AddEventInfo(EventType.Warning, $"{tablePath} в строке №{i} содержит строковую ссылку на описание '{str}'");
      return false;
    }
    Guid guid = this.AddNewNoteBlob(session, sourceDB, this.ConvertToInt(sourceValue), 0L);
    if (!(guid != Guid.Empty))
      return false;
    sourceValue = (object) guid.ToString();
    return true;
  }

  private bool GetImageValue(
    IDataBase sourceDB,
    IUserSession session,
    List<string> badFileNames,
    ref object sourceValue,
    string tablePath,
    int i)
  {
    if (Convert.IsDBNull(sourceValue))
      return false;
    if (sourceValue is string str)
    {
      if (badFileNames.Contains(str))
        return false;
      badFileNames.Add(str);
      this.AddEventInfo(EventType.Warning, $"Таблица '{tablePath}' в строке №{i} содержит строковую ссылку на изображение '{str}'");
      return false;
    }
    try
    {
      long objectID = this.AddNewPicture(session, sourceDB, this.ConvertToInt(sourceValue), 0L);
      if (objectID != 0L)
      {
        IDBObject dbObject = session.GetObject(objectID);
        sourceValue = (object) dbObject.ObjectGUID.ToString();
      }
    }
    catch (Exception ex)
    {
      this.AddEventInfo(EventType.Warning, $"Ошибка при добавлении изображения для строки №{i} '{tablePath}': {ex.Message}");
      return false;
    }
    return true;
  }

  private int ConvertToInt(object value)
  {
    int result;
    return value == null || DBNull.Value.Equals(value) || !int.TryParse(value.ToString(), out result) ? 0 : result;
  }

  private void SetAttrTypesRowParams(
    DataRow newRow,
    Guid attrGuid,
    RequiredModes addMode,
    ComputeValueModes computeMode,
    string formula,
    UniqueValueModes uniqueMode,
    object defaultValue,
    AttributeOptions options,
    string mask,
    string measureGuid)
  {
    newRow["F_ATTRIBUTE_GUID"] = (object) attrGuid.ToString();
    newRow["F_REQUIRED"] = (object) (int) addMode;
    newRow["F_COMPUTED"] = (object) (int) computeMode;
    newRow["F_FORMULA"] = (object) formula;
    newRow["F_UNIQUE"] = (object) (int) uniqueMode;
    newRow["F_DEFAULT_VALUE"] = (object) Convert.ToString(defaultValue);
    newRow["F_OPTIONS"] = (object) (int) options;
    newRow["F_MASK"] = (object) mask;
    newRow["F_UNITS"] = (object) measureGuid;
    newRow["F_DISPLAY"] = (object) string.Empty;
  }

  private Guid GetOrCreateNewRowGuid(DataTable oldData, int fKey)
  {
    if (oldData != null)
    {
      DataRow[] dataRowArray = oldData.Select($"[{"F_KEY"}]={fKey}");
      if (dataRowArray.Length != 0)
        return (Guid) dataRowArray[0]["F_GUID"];
    }
    return Guid.NewGuid();
  }

  private void RenameTable(TableRecord tableRec, IUserSession session)
  {
    bool flag = false;
    StringBuilder stringBuilder = new StringBuilder();
    IDBTransactions customService = (IDBTransactions) session.GetCustomService(typeof (IDBTransactions));
    customService.StartTransaction();
    try
    {
      string msgInfo;
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID), tableRec.Key, 0, out msgInfo);
      if (objectByImbaseCode == 0L)
      {
        flag = true;
        this.AddEventInfo(EventType.Warning, $"Невозможно переименовать таблицу. Таблица не найдена в базе-приемнике по коду Imbase {tableRec.Key}");
      }
      else
      {
        IDBObject dbObject1 = session.GetObject(objectByImbaseCode);
        string str1 = $"{dbObject1.NameInMessages} [{dbObject1.ObjectID}]";
        dbObject1.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = tableRec.Description;
        stringBuilder.AppendLine($"Объект '{str1}' переименован в '{tableRec.Description}'. {msgInfo}");
        foreach (long allTableLink in CodeHandler.GetAllTableLinks(session, dbObject1.ObjectID))
        {
          IDBObject dbObject2 = session.GetObject(allTableLink, false);
          if (dbObject2 != null)
          {
            IDBAttribute byId = dbObject2.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
            string str2 = $"{dbObject2.NameInMessages} [{dbObject2.ObjectID}]";
            string description = tableRec.Description;
            byId.AsString = description;
            stringBuilder.AppendLine($"Объект '{str2}' переименован в '{tableRec.Description}'.");
          }
          else
            stringBuilder.AppendLine($"Ярлык id = {allTableLink} не найден в базе-приемнике.");
        }
        if (stringBuilder.Length <= 0)
          return;
        this.AddEventInfo(EventType.Text, stringBuilder.ToString());
      }
    }
    catch
    {
      flag = true;
      throw;
    }
    finally
    {
      if (flag)
        customService.Rollback();
      else
        customService.Commit();
    }
  }

  internal void ChangeVisibility(TableRecord tableRec, IUserSession session)
  {
    bool flag = false;
    StringBuilder stringBuilder = new StringBuilder();
    IDBTransactions customService = (IDBTransactions) session.GetCustomService(typeof (IDBTransactions));
    customService.StartTransaction();
    try
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID), tableRec.Key, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        flag = true;
        this.AddEventInfo(EventType.Warning, $"Невозможно изменить видимость таблицы. Таблица не найдена в базе-приемнике по коду Imbase {tableRec.Key}");
      }
      else
      {
        IDBObject dbObject1 = session.GetObject(objectByImbaseCode);
        string str1 = $"{dbObject1.NameInMessages} [{dbObject1.ObjectID}]";
        if (CodeHandler.UpdateVisibleObjectState(dbObject1, tableRec.State, true))
          stringBuilder.AppendLine($"Изменена видимость '{str1}'");
        foreach (long allTableLink in CodeHandler.GetAllTableLinks(session, dbObject1.ObjectID))
        {
          IDBObject dbObject2 = session.GetObject(allTableLink, false);
          if (dbObject2 == null)
          {
            this.AddEventInfo(EventType.Warning, $"Изменение видимости {str1}. Ярлык не найден в базе-приемнике {allTableLink}");
          }
          else
          {
            string str2 = $"{dbObject2.NameInMessages} [{dbObject2.ObjectID}]";
            if (CodeHandler.UpdateVisibleObjectState(dbObject2, tableRec.State, true))
              stringBuilder.AppendLine($"Изменена видимость '{str2}'");
          }
        }
        if (stringBuilder.Length <= 0)
          return;
        this.AddEventInfo(EventType.Text, stringBuilder.ToString());
      }
    }
    catch
    {
      flag = true;
      throw;
    }
    finally
    {
      if (flag)
        customService.Rollback();
      else
        customService.Commit();
    }
  }

  private void Delete(EventRecord record, IUserSession session)
  {
    if (record.Text.EndsWith("_REC", true, CultureInfo.CurrentCulture))
      return;
    DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), RelationalOperators.Equal, (object) record.Text, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }));
    if (dataTable.Rows.Count == 0)
    {
      this.AddEventInfo(EventType.Warning, $"Невозможно удалить каталог. Каталог c внутренним именем таблицы {record.Text} не найден в базе-приемнике.");
    }
    else
    {
      IDBObject dbObject = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
      string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      dbObject.Delete(0L);
      this.AddEventInfo(EventType.Text, $"Объект {str} удален.");
    }
  }

  private void Add(
    EventRecord eventRec,
    TableRecord tableRec,
    IDataBase sourceDB,
    IUserSession session)
  {
    string fullPath = this.GetFullPath(sourceDB, eventRec);
    string str = string.IsNullOrEmpty(fullPath) ? $"Таблица: '{tableRec.Description}' ({tableRec.TableName})." : $"Каталог:'{fullPath}' таблица:'{tableRec.Description}' ({tableRec.TableName}).";
    try
    {
      IDBObject newTableObject = (IDBObject) null;
      bool flag1 = false;
      bool flag2 = true;
      if (tableRec.Openmode == 0)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID));
        ConditionStructure conditionStructure = new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), RelationalOperators.Equal, (object) tableRec.TableName, LogicalOperators.AND, 0, false);
        DataTable dataTable = objectCollection.SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
        {
          conditionStructure
        }, new object[1]{ (object) -2 }));
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          newTableObject = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
          if (newTableObject.OwnerID != 0L && newTableObject.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            newTableObject = newTableObject.CheckOut();
            flag1 = true;
          }
          flag2 = false;
        }
        else
          newTableObject = objectCollection.Create();
      }
      else if (tableRec.Openmode == 2)
      {
        DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableMixTypeGUID)).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID, RelationalOperators.Equal, (object) tableRec.TableName, LogicalOperators.AND, 0, false)
        }, new object[1]{ (object) -2 }));
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          newTableObject = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
          if (newTableObject.OwnerID != 0L && newTableObject.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            newTableObject = newTableObject.CheckOut();
            flag1 = true;
          }
          flag2 = false;
        }
        else
        {
          this.AddEventInfo(EventType.Warning, $"Таблица рецептур по коду Imbase {tableRec.Key} в базе-приемнике не найдена. Событие будет обработано позже.");
          this.AddDelayedEvent(eventRec);
        }
      }
      if (newTableObject == null)
        return;
      this.AddAttributesToTable(session, sourceDB, eventRec, newTableObject, tableRec);
      this.ChangeData(session, sourceDB, eventRec, tableRec, newTableObject.ObjectID, false);
      newTableObject.OwnerID = CodeHandler.GetUserID(session, tableRec.User);
      if (newTableObject.IsCreationMode)
        newTableObject.CommitCreation(true);
      if (flag1)
        newTableObject.CheckIn();
      this.AddEventInfo(EventType.Text, $"Объект {$"{newTableObject.NameInMessages} [{newTableObject.ObjectID}]"} {(flag2 ? (object) "создан" : (object) "изменен")}");
    }
    catch (Exception ex)
    {
      throw new Exception($"{ex.Message} {str}", ex);
    }
  }

  private void AddAttributesToTable(
    IUserSession session,
    IDataBase sourceDB,
    EventRecord eventRecord,
    IDBObject newTableObject,
    TableRecord tableRec)
  {
    IDBAttribute dbAttribute1 = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    if (dbAttribute1 != null)
      dbAttribute1.AsString = tableRec.Description;
    IDBAttribute dbAttribute2 = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false);
    if (dbAttribute2 != null)
      dbAttribute2.AsInteger = (long) tableRec.Key;
    IDBAttribute dbAttribute3 = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), false);
    if (dbAttribute3 != null)
      dbAttribute3.AsString = tableRec.TableName;
    if (tableRec.TextID > 0)
    {
      BlobRecord blobRecord = CodeHandler.CreateBlobRecord(sourceDB, tableRec.TextID);
      if (blobRecord != null)
      {
        IDBAttribute dbAttribute4 = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false);
        if (dbAttribute4 != null)
          dbAttribute4.AsString = blobRecord.Memo;
      }
    }
    if (tableRec.GraphID > 0)
    {
      try
      {
        long num = this.AddNewPicture(session, sourceDB, tableRec.GraphID, 0L);
        if (num != 0L)
        {
          IDBAttribute dbAttribute5 = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
          if (dbAttribute5 != null)
            dbAttribute5.Value = (object) num;
        }
      }
      catch (Exception ex)
      {
        this.AddEventInfo(EventType.Warning, $"Ошибка при добавлении изображения для {newTableObject.NameInMessages}: {ex.Message}");
      }
    }
    FieldRecord[] fields = this.GetFields(sourceDB, tableRec.Key);
    this.LinkAttributes(session, fields, sourceDB, eventRecord, tableRec.TableName, tableRec.TableName, tableRec.Key);
    foreach (FieldRecord fieldRecord in fields)
    {
      if (fieldRecord.LongName.ToUpper() == "ШАБЛОН" && fieldRecord.EnterMode == ImEnterMode.IEM_CHARSET && !string.IsNullOrEmpty(fieldRecord.Data))
      {
        string s = fieldRecord.Data.Substring(0, fieldRecord.Data.IndexOf('|'));
        int result;
        if (!string.IsNullOrEmpty(s) && int.TryParse(s, out result) && result > 0)
        {
          string str = Path.GetExtension(CodeHandler.CreateBlobRecord(sourceDB, result).Source);
          if (str != null && str.ToUpper().Equals(".SETCHR"))
          {
            IDBObject dbObject = this.AddNewTemplate(session, sourceDB, result);
            if (dbObject != null)
            {
              this.AddEventInfo(EventType.Text, $"Создан объект {dbObject.NameInMessages}.");
              newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseTemplateRefAttGUID), false).Value = (object) dbObject.ObjectID;
            }
          }
        }
      }
    }
  }

  private IDBObject AddNewTemplate(IUserSession session, IDataBase db, int blobID)
  {
    BlobRecord blobRecord = CodeHandler.CreateBlobRecord(db, blobID);
    if (blobRecord == null)
      return (IDBObject) null;
    IDBObject destObj = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTemplateTypeGUID)).Create();
    destObj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) blobRecord.Source
    });
    this.WriteTemplate(destObj, blobRecord);
    destObj.CommitCreation(true);
    return destObj;
  }

  protected Guid AddNewNoteBlob(
    IUserSession session,
    IDataBase db,
    int noteId,
    long oldNoteObjectId)
  {
    Guid empty = Guid.Empty;
    BlobRecord blobRecord = CodeHandler.CreateBlobRecord(db, noteId);
    if (blobRecord == null)
      return empty;
    IDBObject dbObject = (IDBObject) null;
    bool flag = false;
    if (oldNoteObjectId != 0L)
      dbObject = session.GetObject(oldNoteObjectId, false);
    if (dbObject == null)
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseBLOBTypeGUID), noteId, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        dbObject = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseBLOBTypeGUID)).Create();
        flag = true;
      }
      else
        dbObject = session.GetObject(objectByImbaseCode);
    }
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) blobRecord.Source
    });
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false, new object[1]
    {
      (object) noteId
    });
    if (blobRecord.Length > 0)
      dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false).AsString = blobRecord.Memo;
    if (flag)
      dbObject.CommitCreation(true);
    return dbObject.ObjectGUID;
  }

  private void WriteTemplate(IDBObject destObj, BlobRecord blobRec)
  {
    if (blobRec == null || blobRec.Length <= 0 || !(destObj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseTemplateDataAttGUID), false) is IBlobWriter blobWriter))
      return;
    using (MemoryStream inStream = new MemoryStream(blobRec.Blob))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        long length = inStream.Length;
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, 9);
        BlobInformation blobInfo = new BlobInformation(length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, blobRec.Source);
        blobWriter.OpenBlob(blobInfo, false);
        outStream.Position = 0L;
        blobWriter.WriteDataBlock(outStream.ToArray());
      }
    }
  }

  private void AddChangedTableId(long tableObjId)
  {
    ApplicationServices.Container.GetService<IChangedTableIndexer>()?.AddTableObjID(tableObjId);
  }
}
