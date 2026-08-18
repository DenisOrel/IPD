// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.DataTableImporter
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>Класс для импорта данных в IPS из файлов IMBASE</summary>
public class DataTableImporter
{
  private DataTable _fieldsTable;
  private DataTable _dataTable;
  private DataTable _infoTable;
  private DataSet _oldDataSet;
  private DataSet _dataSet;
  private DataTable _oldDataTable;
  private string _additionalData;
  private List<AttributesComparison> _comparisons;
  internal static MeasureAttributesService _measureAttService = new MeasureAttributesService();

  public List<AttributesComparison> Comparisons => this._comparisons;

  public DataTableImporter(IUserSession session, IDBObject tableObject)
  {
    this._oldDataSet = TableLoadHelper.GetTables(session, tableObject.ObjectID, true);
    if (this._oldDataSet != null)
      this._oldDataTable = this._oldDataSet.Tables["IMS_DATA"];
    this.ReadComparsions(tableObject);
  }

  public void ReadComparsions(IDBObject tableObject)
  {
    this._comparisons = AttributesComparisonHelper.ReadFromAttribute(tableObject.GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false));
  }

  public bool TryCreateDataTable(
    IUserSession session,
    IDBObject tableObject,
    IDBAttribute fileAtt,
    out DataSet dataSet)
  {
    dataSet = (DataSet) null;
    if (this.ReadPortalData(fileAtt) != 4)
      throw new Exception("Недостаточно файлов данных для импорта таблицы IMBASE");
    int num = this.AddStructAndData(session, tableObject) ? 1 : 0;
    if (num != 0 && fileAtt != null)
      fileAtt.Delete(0L);
    dataSet = this._dataSet;
    return num != 0;
  }

  public int ReadPortalData(IDBAttribute fileAtt)
  {
    int num = 0;
    int valuesCount = fileAtt.ValuesCount;
    for (int index = 0; index < valuesCount; ++index)
    {
      fileAtt.Index = index;
      IBlobReader br = fileAtt as IBlobReader;
      BlobInformation blobInfo = br.OpenBlob(0);
      string fileName = blobInfo.FileName;
      string str = this.ExtractString(br, blobInfo);
      if (fileName.StartsWith("structure.xml", StringComparison.InvariantCultureIgnoreCase))
      {
        this._fieldsTable = this.DataTableFromXml(str);
        ++num;
      }
      else if (fileName.StartsWith("data.xml", StringComparison.InvariantCultureIgnoreCase))
      {
        this._dataTable = this.DataTableFromXml(str);
        ++num;
      }
      else if (fileName.StartsWith("info.xml", StringComparison.InvariantCultureIgnoreCase))
      {
        this._infoTable = this.DataTableFromXml(str);
        ++num;
      }
      else if (fileName.StartsWith("data.txt", StringComparison.InvariantCultureIgnoreCase))
      {
        this._additionalData = str;
        ++num;
      }
    }
    return num;
  }

  private bool AddStructAndData(IUserSession session, IDBObject tableObject)
  {
    this.AddAttributesToTable(session, tableObject);
    this._dataSet = TableLoadHelper.CreateDataSet();
    return this.ChangeData(session, tableObject, this._dataSet);
  }

  private bool ChangeData(IUserSession session, IDBObject tableObject, DataSet dataSet)
  {
    FieldRecord[] fields = this.GetFields();
    List<Guid> fieldNames = new List<Guid>(fields.Length);
    Dictionary<Guid, string> formules = (Dictionary<Guid, string>) null;
    if (!this.LinkAttributes(session, tableObject, fields, ref formules))
      return false;
    DataTable table1 = dataSet.Tables["IMS_ATTR_TYPES"];
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    Guid guid;
    for (int index = 0; index < fields.Length; ++index)
    {
      IDBAttributeType attributeType = session.GetAttributeType(fields[index].GUID);
      if (table1.Rows.Find((object) fields[index].GUID.ToString()) == null)
      {
        fieldNames.Add(fields[index].GUID);
        ImbaseImportHelper.SetOptionsForAttribute(attributeType, fields[index].EnterMode == ImEnterMode.IEM_RECORD);
        string formula = (string) null;
        formules?.TryGetValue(fields[index].GUID, out formula);
        RequiredModes addMode = RequiredModes.Manual;
        ComputeValueModes computeMode = ComputeValueModes.NotComputableValue;
        ImbaseImportHelper.FormingComputedFlags(fields[index].EnterMode, formula != null && formula != string.Empty, ref addMode, ref computeMode);
        string empty = string.Empty;
        if (attributeType.AttributeType == FieldTypes.ftMeasured)
        {
          ImbaseMeasureDefine imbaseMeasureDefine = new ImbaseMeasureDefine(session);
          try
          {
            guid = imbaseMeasureDefine.GetMeasure(attributeType.SizeType, fields[index].Units);
            empty = guid.ToString();
          }
          catch (Exception ex)
          {
            throw new Exception($"Ошибка при получении единицы измерения '{fields[index].Units}' для атрибута '{attributeType.Name}", ex);
          }
          string str1 = empty;
          guid = Guid.Empty;
          string str2 = guid.ToString();
          if (str1 == str2)
            throw new Exception($"Единица измерения '{fields[index].Units}' в найдена!{attributeType.Name}");
        }
        DataRow dataRow = table1.NewRow();
        object defaultValue = attributeType.DefaultValue;
        if (computeMode == ComputeValueModes.NotComputableValue && !string.IsNullOrEmpty(fields[index].Data))
        {
          string data = fields[index].Data;
          switch (attributeType.AttributeType)
          {
            case FieldTypes.ftString:
              defaultValue = (object) data;
              break;
            case FieldTypes.ftInteger:
              int result1;
              if (int.TryParse(data, out result1))
              {
                defaultValue = (object) result1;
                break;
              }
              break;
            case FieldTypes.ftDouble:
            case FieldTypes.ftMeasured:
              double result2;
              if (double.TryParse(data, out result2))
              {
                defaultValue = (object) result2;
                break;
              }
              break;
          }
        }
        this.WriteDataRow(dataRow, fields[index].GUID, addMode, computeMode, formula, attributeType.UniqueMode, defaultValue, attributeType.Options, attributeType.Mask, empty);
        table1.Rows.Add(dataRow);
      }
    }
    TableAttribute tableAttribute = ImbaseImportHelper.CheckNameColumn(fieldNames);
    if (tableAttribute != null)
    {
      IDBAttributeType attributeType = session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
      bool flag = false;
      for (int index = 0; index < table1.Rows.Count; ++index)
      {
        guid = new Guid(table1.Rows[index]["F_ATTRIBUTE_GUID"].ToString());
        if (guid.Equals((attributeType as IDBGuid).GUID))
        {
          this.WriteDataRow(table1.Rows[index], tableAttribute.AttributeGuid, tableAttribute.AddMode, tableAttribute.ComputeMode, tableAttribute.ImFormula, attributeType.UniqueMode, attributeType.DefaultValue, attributeType.Options, attributeType.Mask, string.Empty);
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        DataRow dataRow = table1.NewRow();
        this.WriteDataRow(dataRow, tableAttribute.AttributeGuid, tableAttribute.AddMode, tableAttribute.ComputeMode, tableAttribute.ImFormula, attributeType.UniqueMode, attributeType.DefaultValue, attributeType.Options, attributeType.Mask, string.Empty);
        table1.Rows.Add(dataRow);
      }
    }
    table1.AcceptChanges();
    DataTable dataTable = this._dataTable;
    DataTable table2 = dataSet.Tables["IMS_DATA"];
    for (int index = 0; index < dataTable.Columns.Count; ++index)
    {
      string columnName = dataTable.Columns[index].ColumnName;
      Type type = dataTable.Columns[index].DataType;
      if (stringList1.Contains(columnName) || stringList2.Contains(columnName))
        type = typeof (string);
      if (!table2.Columns.Contains(columnName))
        table2.Columns.Add(columnName, type);
    }
    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
    {
      DataRow dataRow = table2.NewRow();
      for (int index2 = 0; index2 < dataTable.Columns.Count; ++index2)
      {
        string columnName = dataTable.Columns[index2].ColumnName;
        object obj = dataTable.Rows[index1][columnName];
        dataRow[columnName] = obj;
      }
      int int32 = Convert.ToInt32(dataRow["F_KEY"]);
      dataRow["F_GUID"] = (object) this.GetOrCreateNewRowGuid(int32);
      this.RestoreUnchangedColumns(int32, dataRow, table2, fields);
      table2.Rows.Add(dataRow);
    }
    table2.AcceptChanges();
    List<string> stringList3 = new List<string>(dataTable.Columns.Count);
    for (int index3 = 0; index3 < table2.Columns.Count; ++index3)
    {
      for (int index4 = 0; index4 < fields.Length; ++index4)
      {
        if (fields[index4].Field == table2.Columns[index3].ColumnName)
        {
          if (!table2.Columns.Contains(fields[index4].GUID.ToString()))
          {
            table2.Columns[index3].ColumnName = fields[index4].GUID.ToString();
            break;
          }
          stringList3.Add(table2.Columns[index3].ColumnName);
          break;
        }
      }
    }
    for (int index = 0; index < stringList3.Count; ++index)
      table2.Columns.Remove(stringList3[index]);
    DataColumnCollection columns = table2.Columns;
    guid = Intermech.Imbase.Consts.ImbaseUsingAttGUID;
    string columnName1 = guid.ToString();
    int columnIndex = columns.IndexOf(columnName1);
    try
    {
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) table2.Rows)
        {
          switch (row[columnIndex].ToString())
          {
            case "T":
              row[columnIndex] = (object) "+";
              continue;
            case "F":
              row[columnIndex] = (object) "-";
              continue;
            default:
              continue;
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
    table2.AcceptChanges();
    return true;
  }

  /// <summary>Восстанавливает данные из старой записи в новой</summary>
  /// <param name="rowKey">Код записи</param>
  /// <param name="newRow">Новая запись</param>
  /// <param name="fields">Список полей новой таблицы</param>
  private void RestoreUnchangedColumns(
    int rowKey,
    DataRow newRow,
    DataTable newTable,
    FieldRecord[] fields)
  {
    if (this._oldDataTable == null)
      return;
    int columnIndex1 = this._oldDataTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
    if (columnIndex1 == -1)
      return;
    string columnName = string.Empty;
    int length = fields.Length;
    for (int index = 0; index < length; ++index)
    {
      if (fields[index].GUID.Equals(Intermech.Imbase.Consts.ImbaseUsingAttGUID))
      {
        columnName = fields[index].Field;
        break;
      }
    }
    if (string.IsNullOrEmpty(columnName))
      return;
    int columnIndex2 = newTable.Columns.IndexOf(columnName);
    if (columnIndex2 == -1)
      return;
    DataRow[] dataRowArray = this._oldDataTable.Select($"[{"F_KEY"}]={rowKey}");
    if (dataRowArray == null || dataRowArray.Length == 0)
      return;
    newRow[columnIndex2] = dataRowArray[0][columnIndex1];
  }

  private Guid GetOrCreateNewRowGuid(int fKey)
  {
    if (this._oldDataTable != null)
    {
      DataRow[] dataRowArray = this._oldDataTable.Select($"[{"F_KEY"}]={fKey}");
      if (dataRowArray != null && dataRowArray.Length != 0)
        return (Guid) dataRowArray[0]["F_GUID"];
    }
    return Guid.NewGuid();
  }

  /// <summary>
  /// Проверим и подвяжем поля к существующим атрибутам, и создадим которых нет в базе
  /// </summary>
  public bool LinkAttributes(
    IUserSession session,
    IDBObject tableObject,
    FieldRecord[] fields,
    ref Dictionary<Guid, string> formules)
  {
    session.GetAttributeTypeCollection(-1);
    formules = new Dictionary<Guid, string>(fields.Length);
    for (int index = 0; index < fields.Length; ++index)
    {
      bool flag = false;
      if (fields[index].ShortName.StartsWith("$"))
      {
        fields[index].ShortName = string.Empty;
        flag = true;
      }
      int errorAttId = 0;
      IDBAttributeType dbAttributeType = this.CheckAttribute(session, fields[index], out string _, Guid.Empty, out errorAttId);
      if (dbAttributeType == null)
        return false;
      if (flag && dbAttributeType.AttributeType != FieldTypes.ftSystem)
        dbAttributeType.Options |= AttributeOptions.ImbaseFlag_IMHGen;
      if (fields[index].EnterMode == ImEnterMode.IEM_EXPRESSION)
        formules.Add(fields[index].GUID, fields[index].Data);
      if (dbAttributeType.AttributeType == FieldTypes.ftMeasured)
        DataTableImporter._measureAttService.AddMeasure(tableObject.ObjectID, dbAttributeType.AttributeID, dbAttributeType.SizeType);
    }
    if (formules.Count > 0)
    {
      Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(formules.Count);
      foreach (KeyValuePair<Guid, string> keyValuePair in formules)
      {
        ImbaseFormulaParser imbaseFormulaParser = new ImbaseFormulaParser(session, session.GetAttributeType(keyValuePair.Key), fields);
        dictionary.Add(keyValuePair.Key, imbaseFormulaParser.Parse(keyValuePair.Value));
      }
      formules = dictionary;
    }
    return true;
  }

  public IDBAttributeType CheckAttribute(
    IUserSession session,
    FieldRecord field,
    out string errorMessage,
    Guid destGuid,
    out int errorAttId)
  {
    errorAttId = 0;
    IDBAttributeType dbAttributeType = (IDBAttributeType) null;
    errorMessage = (string) null;
    if (!Guid.Empty.Equals(destGuid))
      dbAttributeType = session.GetAttributeType(destGuid);
    else if (this._comparisons != null)
    {
      AttributesComparison attributesComparison = this._comparisons.Find((Predicate<AttributesComparison>) (x => x.SourceName.Equals(field.Field)));
      if (attributesComparison != null)
        dbAttributeType = session.GetAttributeType(attributesComparison.DestinationGuid);
    }
    if (dbAttributeType == null)
    {
      field.LongName = ImbaseImportHelper.CheckSpecialNames(field.LongName, field.FieldType);
      dbAttributeType = session.GetAttributeType(field.LongName, false);
      if (dbAttributeType != null && dbAttributeType.AttributeID < 0)
      {
        field.LongName += "^";
        dbAttributeType = session.GetAttributeType(field.LongName, false);
      }
      if (dbAttributeType != null && field.ShortName != string.Empty && !dbAttributeType.ShortName.Equals(field.ShortName))
      {
        field.LongName = ImbaseImportHelper.GetDoubleName(field.LongName, field.ShortName, false);
        dbAttributeType = session.GetAttributeType(field.LongName, false);
      }
    }
    if (dbAttributeType == null)
    {
      errorMessage = $"Не найден атрибут для поля {field.LongName} [{field.ShortName}]";
      return (IDBAttributeType) null;
    }
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = false;
    if (dbAttributeType.AttributeType != field.FieldType && !dbAttributeType.IsCompatibleType(field.FieldType))
    {
      stringBuilder.AppendLine("Типы данных не совместимы");
      flag = true;
    }
    if (field.FieldType == FieldTypes.ftString && this._dataTable.Columns.Contains(field.Field))
    {
      DataColumn column = this._dataTable.Columns[field.Field];
      int maxLength = column.MaxLength;
      if (dbAttributeType.SizeType < (long) maxLength)
      {
        int num = this.ScanColumnSize(this._dataTable, column);
        if (dbAttributeType.SizeType < (long) num)
        {
          stringBuilder.AppendLine("Возможная длина значений в базе приемнике меньше");
          flag = true;
        }
      }
    }
    if (flag)
    {
      errorMessage = stringBuilder.ToString();
      if (dbAttributeType != null)
        errorAttId = dbAttributeType.AttributeID;
      return (IDBAttributeType) null;
    }
    if (dbAttributeType == null)
    {
      errorMessage = $"Не найден атрибут для поля {field.LongName}{field.ShortName}";
      return (IDBAttributeType) null;
    }
    field.GUID = (dbAttributeType as IDBGuid).GUID;
    return dbAttributeType;
  }

  private int ScanColumnSize(DataTable dataTable, DataColumn col)
  {
    int num = 0;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int length = row[col].ToString().Length;
      if (length > num)
        num = length;
    }
    return num;
  }

  public int GetDataSize(FieldRecord field)
  {
    int dataSize = 0;
    if (field.FieldType == FieldTypes.ftString && this._dataTable.Columns.Contains(field.Field))
      dataSize = this._dataTable.Columns[field.Field].MaxLength;
    return dataSize;
  }

  /// <summary>Сформировать значение исходя из типа</summary>
  /// <param name="fieldType">Тип</param>
  /// <param name="DefValue">Исходное значение</param>
  /// <returns>Значение, приведенное к типу</returns>
  public object FormingValue(FieldTypes fieldType, object DefValue)
  {
    if (CompareValuesHelper.NormalizedValue(DefValue) == null)
      return (object) null;
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftInteger:
          long result1;
          return long.TryParse(Convert.ToString(DefValue), out result1) ? (object) result1 : (object) null;
        case FieldTypes.ftDouble:
        case FieldTypes.ftMeasured:
          double result2;
          return double.TryParse(Convert.ToString(DefValue), out result2) ? (object) result2 : (object) null;
        case FieldTypes.ftDateTime:
          DateTime result3;
          return DateTime.TryParse(Convert.ToString(DefValue), out result3) ? (object) result3 : (object) null;
        default:
          return DefValue;
      }
    }
    catch
    {
      return (object) null;
    }
  }

  /// <summary>
  /// Получить список полей для таблицы/папки (из IM_FIELDS)
  /// </summary>
  /// <param name="db">БД</param>
  /// <param name="tableID">Значение поля F_TABLE_ID</param>
  /// <returns></returns>
  public FieldRecord[] GetFields()
  {
    DataTable fieldsTable = this._fieldsTable;
    List<FieldRecord> fieldRecordList = new List<FieldRecord>(fieldsTable.Rows.Count);
    for (int index = 0; index < fieldsTable.Rows.Count; ++index)
      fieldRecordList.Add(new FieldRecord(fieldsTable.Rows[index]));
    return fieldRecordList.ToArray();
  }

  private void WriteDataRow(
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

  private void AddAttributesToTable(IUserSession session, IDBObject tableObject)
  {
  }

  private DataTable DataTableFromXml(string value)
  {
    DataTable dataTable = new DataTable();
    using (StringReader reader = new StringReader(value))
    {
      int num = (int) dataTable.ReadXml((TextReader) reader);
    }
    return dataTable;
  }

  private string ExtractString(IBlobReader br, BlobInformation blobInfo)
  {
    string str = string.Empty;
    if (br != null && blobInfo.RealFileSize != 0L)
    {
      byte[] buffer = br.ReadDataBlock();
      br.CloseBlob();
      if (buffer != null && buffer.Length != 0)
      {
        using (MemoryStream inStream = new MemoryStream(buffer))
        {
          inStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) blobInfo.RealFileSize))
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
            outStream.Position = 0L;
            using (TextReader textReader = (TextReader) new StreamReader((Stream) outStream, Encoding.Unicode))
              str = textReader.ReadToEnd();
          }
        }
      }
    }
    return str;
  }

  public string AttList => this._additionalData;
}
