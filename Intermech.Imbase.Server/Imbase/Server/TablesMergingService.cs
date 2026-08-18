// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TablesMergingService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server;

internal sealed class TablesMergingService : LongLifeObject, ITablesMergingService
{
  public bool Merge(Guid sessionGuid, long tableID, DataSet importData, bool saveToBase)
  {
    bool flag1 = false;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBObject tableObject = sessionById.GetObject(tableID);
    IDBAttribute attributeById = tableObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableDataAttID);
    if (attributeById == null || attributeById.IsNull)
      attributeById = tableObject.GetAttributeByID(TableLoadHelper.LongBlobTableDataAttId);
    if (attributeById != null)
    {
      DataSet dataSet = this.UnpackDataSetFromAttribute(attributeById);
      DataTable table1 = dataSet?.Tables["IMS_ATTR_TYPES"];
      DataTable table2 = dataSet?.Tables["IMS_DATA"];
      DataTable table3 = importData.Tables["IMS_ATTR_TYPES"];
      DataTable table4 = importData.Tables["IMS_DATA"];
      bool flag2 = false;
      if (dataSet != null)
      {
        foreach (DataRow row1 in (InternalDataCollectionBase) table3.Rows)
        {
          if ((Convert.ToInt32(row1["F_OPTIONS"]) & 8388608 /*0x800000*/) == 8388608 /*0x800000*/)
          {
            Guid guid = new Guid(Convert.ToString(row1["F_ATTRIBUTE_GUID"]));
            foreach (DataRow row2 in (InternalDataCollectionBase) table1.Rows)
            {
              Guid g = new Guid(Convert.ToString(row2["F_ATTRIBUTE_GUID"]));
              if (guid.Equals(g))
              {
                IEnumerator enumerator = table4.Rows.GetEnumerator();
                try
                {
                  while (enumerator.MoveNext())
                  {
                    DataRow current = (DataRow) enumerator.Current;
                    Guid recordGuid = new Guid(Convert.ToString(current["F_GUID"]));
                    string columnName = guid.ToString();
                    object result;
                    if (this.GetCellValue(table2, columnName, recordGuid, out result))
                    {
                      current[columnName] = result;
                      flag2 = true;
                    }
                  }
                  break;
                }
                finally
                {
                  if (enumerator is IDisposable disposable)
                    disposable.Dispose();
                }
              }
            }
          }
        }
      }
      if (flag2)
      {
        table4.AcceptChanges();
        flag1 = true;
      }
    }
    if (saveToBase)
    {
      IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionById, true);
      service.StartTransaction();
      try
      {
        TableLoadHelper.StoreData(sessionById, importData, tableObject.ObjectID, tableObject, (ITablesIndexer) null);
        tableObject.GetAttributeByGuid(PortalConsts.attributeImportedTableData, false)?.Delete(0L);
        tableObject.GetAttributeByGuid(PortalConsts.attributeTableAttributes, false)?.Delete(0L);
        service.Commit();
      }
      catch
      {
        service.Rollback();
        throw;
      }
    }
    return flag1;
  }

  private DataSet UnpackDataSetFromAttribute(IDBAttribute attr)
  {
    IBlobReader blobReader = (IBlobReader) attr;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      DataSet dataSet = (DataSet) null;
      if (blobInformation.RealFileSize > 0L)
      {
        using (Stream stream = (Stream) new MemoryStream(blobReader.ReadDataBlock()))
        {
          stream.Position = 0L;
          if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
          {
            using (ImChunkedStream imChunkedStream = new ImChunkedStream())
            {
              ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) imChunkedStream, stream);
              imChunkedStream.Position = 0L;
              dataSet = (DataSet) binaryFormatter.Deserialize((Stream) imChunkedStream);
            }
          }
          else
            dataSet = (DataSet) binaryFormatter.Deserialize(stream);
        }
      }
      return dataSet;
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }

  private bool GetCellValue(
    DataTable table,
    string columnName,
    Guid recordGuid,
    out object result)
  {
    result = (object) null;
    DataRow[] dataRowArray = table.Select($"F_GUID='{recordGuid.ToString()}'");
    if (dataRowArray.Length != 1)
      return false;
    result = dataRowArray[0][columnName];
    return true;
  }

  private string GetPossibleValueFieldName(FieldTypes type, int attributeID)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    List<FieldTypes> convertList = new List<FieldTypes>();
    RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
    bool computableAttribute = false;
    AttributeCacheHelper.GetAttributeTypeValues(type, attributeID, ref empty1, ref empty2, ref convertList, ref enabledOperators, ref computableAttribute, ref empty3);
    return empty3;
  }

  private bool InPossibleValues(string ValueFieldName, object[] array, object searchValue)
  {
    foreach (object val1 in array)
    {
      switch (ValueFieldName)
      {
        case "F_INTEGER_VALUE":
          if (CompareValuesHelper.CompareIntValues(val1, searchValue))
            return true;
          break;
        case "F_DOUBLE_VALUE":
          if (CompareValuesHelper.CompareFloatValues(val1, searchValue))
            return true;
          break;
        case "F_DATE_VALUE":
          if (CompareValuesHelper.CompareDateTimeValues(val1, searchValue))
            return true;
          break;
        default:
          if (CompareValuesHelper.CompareStringValues(val1, searchValue))
            return true;
          break;
      }
    }
    return false;
  }

  public bool CheckAttribute(
    Guid sessionGuid,
    int attrTypeID,
    DataTable possibleValues,
    int inID,
    FieldTypes inFieldType,
    string inSize,
    MultiValueModes inMultiValueMode,
    out string errorMessage)
  {
    IDBAttributeType attributeType = UserSession.GetSessionByID(sessionGuid).GetAttributeType(attrTypeID, true);
    errorMessage = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = true;
    if (attributeType.AttributeType != inFieldType && !attributeType.IsCompatibleType(inFieldType))
    {
      stringBuilder.AppendLine("Типы данных не совместимы");
      flag = false;
    }
    if (attributeType.AttributeType == FieldTypes.ftString && !string.IsNullOrEmpty(inSize) && attributeType.SizeType < Convert.ToInt64(inSize))
    {
      stringBuilder.AppendLine("Возможная длина значений в базе приемнике меньше");
      flag = false;
    }
    if (attributeType.MultipleValued == MultiValueModes.MultiValuesFromList || attributeType.MultipleValued == MultiValueModes.SingleValueFromList || inMultiValueMode == MultiValueModes.MultiValuesFromList || inMultiValueMode == MultiValueModes.SingleValueFromList)
    {
      if (inMultiValueMode == MultiValueModes.MultiValues && attributeType.MultipleValued != MultiValueModes.MultiValues || inMultiValueMode == MultiValueModes.SingleValueFromList && (attributeType.MultipleValued == MultiValueModes.SingleValue || attributeType.MultipleValued == MultiValueModes.MultiValues) || inMultiValueMode == MultiValueModes.MultiValuesFromList && attributeType.MultipleValued != MultiValueModes.MultiValuesFromList && attributeType.MultipleValued != MultiValueModes.MultiValues)
      {
        stringBuilder.AppendLine("Несовместимые режимы работы со списковыми параметрами");
        flag = false;
      }
      object[] possibleValuesArray = attributeType.GetPossibleValuesArray();
      if (possibleValues != null)
      {
        DataRow[] dataRowArray = possibleValues.Select($"F_ATTRIBUTE_ID={inID}");
        string possibleValueFieldName1 = this.GetPossibleValueFieldName(inFieldType, inID);
        string possibleValueFieldName2 = this.GetPossibleValueFieldName(attributeType.AttributeType, attributeType.AttributeID);
        foreach (DataRow dataRow in dataRowArray)
        {
          if (!this.InPossibleValues(possibleValueFieldName2, possibleValuesArray, dataRow[possibleValueFieldName1]))
          {
            stringBuilder.AppendLine("Различия в допустимых значениях");
            flag = false;
            break;
          }
        }
      }
    }
    if (stringBuilder.Length > 0)
      errorMessage = stringBuilder.ToString();
    return flag;
  }
}
