// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IMHHelper
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using Intermech.Imbase;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
public class IMHHelper
{
  /// <summary>
  /// 
  /// </summary>
  public static Dictionary<int, List<NodeInfo>> ChildNodesColl;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static string GetClassifFolderKey(string name)
  {
    string classifFolderKey = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IIMHSystemSettingsService)) is IIMHSystemSettingsService customService)
      {
        Guid objectGuidByName = customService.GetObjectGuidByName(name);
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectGuidByName);
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectInfo.ObjectID, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
          classifFolderKey = attributeById != null ? attributeById.AsString : string.Empty;
        }
      }
    }
    return classifFolderKey;
  }

  /// <summary>Получение DataSet объекта IPS таблица IMBASE.</summary>
  /// <param name="settingsTableName">Наименование константы, связанной с определенной таблицей</param>
  /// <returns>DataSet объекта IPS таблица IMBASE</returns>
  public static DataSet GetImbaseDS(string settingsTableName)
  {
    DataSet imbaseDs = (DataSet) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IIMHSystemSettingsService)) is IIMHSystemSettingsService customService)
      {
        Guid objectGuidByName = customService.GetObjectGuidByName(settingsTableName);
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectGuidByName);
        if (!objectInfo.Empty)
        {
          imbaseDs = TableLoadHelper.GetTables(sessionKeeper.Session, objectInfo.ObjectID, true);
          if (imbaseDs == null)
          {
            long tableIdByTableRefId = IMHHelper.GetTableIDByTableRefID(objectInfo.ObjectID);
            imbaseDs = TableLoadHelper.GetTables(sessionKeeper.Session, tableIdByTableRefId, true);
          }
        }
      }
    }
    return imbaseDs;
  }

  /// <summary>
  /// Получение иерархии элементов IMBASE вверх от указанного элемента.
  /// </summary>
  /// <param name="objID">Идентификатор элемента, от которого начат поиск</param>
  /// <returns>Таблица элементов</returns>
  public static DataTable GetImbaseTableTree(long objID)
  {
    DataTable imbaseTableTree = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      {
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        long[] objectList = new long[1]{ objID };
        imbaseTableTree = customService.GetFoldersForObjects(sessionGuid, objectList, (long[]) null);
      }
    }
    return imbaseTableTree;
  }

  /// <summary>
  /// Получить идентификатор объекта (каталога/папки/таблицы) по наименованию константы
  /// </summary>
  /// <param name="name">Наименование константы</param>
  /// <returns>Идентификатор объекта</returns>
  public static long GetObjectIDByConstName(string name)
  {
    long objectIdByConstName = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IIMHSystemSettingsService)) is IIMHSystemSettingsService customService)
      {
        Guid objectGuidByName = customService.GetObjectGuidByName(name);
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectGuidByName);
        if (!objectInfo.Empty)
          objectIdByConstName = objectInfo.ObjectID;
      }
    }
    return objectIdByConstName;
  }

  /// <summary>
  /// Получение идентификатора родительского элемента IMBASE.
  /// </summary>
  /// <param name="dt">Таблица иерархии элементов IMBASE</param>
  /// <param name="objID">Идентификатор элемента IMBASE</param>
  /// <returns>Идентификатор родительского элемента IMBASE</returns>
  public static long GetParentID(DataTable dt, long objID)
  {
    long result = 0;
    if (dt.Columns.Contains("F_OBJECT_ID") && dt.Columns.Contains("F_PATH"))
    {
      DataRow[] dataRowArray1 = dt.Select($"F_OBJECT_ID={objID}");
      if (dataRowArray1.Length != 0)
      {
        object obj1 = dataRowArray1[0]["F_PATH"];
        string str = obj1 != null ? obj1.ToString() : string.Empty;
        if (str.Length > 2)
        {
          DataRow[] dataRowArray2 = dt.Select($"F_PATH='{str.Substring(0, str.Length - 2)}'");
          if (dataRowArray2.Length != 0)
          {
            object obj2 = dataRowArray2[0]["F_OBJECT_ID"];
            if (obj2 != null && !long.TryParse(obj2.ToString(), out result))
              result = 0L;
          }
        }
      }
    }
    return result;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrType"></param>
  /// <returns></returns>
  public static DataTable GetPossibleValues(IDBAttributeTypeInfo attrType)
  {
    DataTable possibleValues = (DataTable) null;
    if (attrType != null)
    {
      possibleValues = attrType.GetPossibleValues();
      if (possibleValues != null)
      {
        if (possibleValues.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
          {
            object obj1 = row[attrType.PossibleValueFieldName];
            object obj2 = row["F_DESCRIPTION"];
            string str1 = obj1 == null || obj1 == DBNull.Value ? string.Empty : obj1.ToString();
            string str2 = obj2 == null || obj2 == DBNull.Value ? str1 : (string.IsNullOrEmpty(obj2.ToString()) ? str1 : obj2.ToString());
            row[attrType.PossibleValueFieldName] = (object) str1;
            row["F_DESCRIPTION"] = (object) str2;
          }
          if ((attrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
          {
            DataRow row = possibleValues.NewRow();
            row[attrType.PossibleValueFieldName] = (object) DBNull.Value;
            row["F_DESCRIPTION"] = (object) string.Empty;
            possibleValues.Rows.InsertAt(row, 0);
          }
          possibleValues.DefaultView.Sort = "F_DESCRIPTION";
        }
        else
          possibleValues = (DataTable) null;
      }
    }
    return possibleValues;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrType"></param>
  /// <param name="measureID"></param>
  /// <returns></returns>
  public static DataTable GetPossibleValues(IDBAttributeTypeInfo attrType, long measureID)
  {
    DataTable possibleValues = attrType?.GetPossibleValues();
    if (possibleValues == null)
      return (DataTable) null;
    possibleValues.Columns.Add(new DataColumn("NUM_VALUE"));
    if (possibleValues.Rows.Count > 0)
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measureID);
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        object obj1 = row[attrType.PossibleValueFieldName];
        object obj2 = row["F_DESCRIPTION"];
        string mValue = obj1 == null || obj1 == DBNull.Value ? string.Empty : obj1.ToString();
        string str = obj2 == null || obj2 == DBNull.Value ? mValue : (string.IsNullOrEmpty(obj2.ToString()) ? mValue : obj2.ToString());
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, descriptor, true);
        if (measuredValue.MeasureID != measureID)
          measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, measureID);
        row[attrType.PossibleValueFieldName] = (object) mValue;
        row["F_DESCRIPTION"] = (object) str;
        row["NUM_VALUE"] = (object) measuredValue.Value;
      }
      if ((attrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      {
        DataRow row = possibleValues.NewRow();
        row[attrType.PossibleValueFieldName] = (object) DBNull.Value;
        row["F_DESCRIPTION"] = (object) string.Empty;
        row["NUM_VALUE"] = (object) DBNull.Value;
        possibleValues.Rows.Add(row);
      }
      possibleValues.DefaultView.Sort = "F_DESCRIPTION";
    }
    else
      possibleValues = (DataTable) null;
    return possibleValues;
  }

  /// <summary>
  /// Получение идентификатор таблицы IMBASE по идентификатору ярлыка IMBASE.
  /// </summary>
  /// <param name="tableRefID">Идентификатор ярлыка IMBASE</param>
  /// <returns>Идентификатор таблицы IMBASE</returns>
  public static long GetTableIDByTableRefID(long tableRefID)
  {
    long tableIdByTableRefId = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(tableRefID, false);
      if (objectActualCopy != null)
      {
        IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
        if (attributeById != null)
          tableIdByTableRefId = attributeById.AsInteger;
      }
    }
    return tableIdByTableRefId;
  }

  /// <summary>
  /// Получение таблиц данных по идентификатору идентификатору ярлыка IMBASE.
  /// </summary>
  /// <param name="tableRefID">Идентификатор ярлыка IMBASE</param>
  /// <param name="dtData"></param>
  /// <param name="dtAttrs"></param>
  /// <param name="props"></param>
  public static DataSet GetTables(
    long tableRefID,
    out DataTable dtData,
    out DataTable dtAttrs,
    out AttributeTypeProperties[] props)
  {
    DataSet tables1 = (DataSet) null;
    dtData = (DataTable) null;
    dtAttrs = (DataTable) null;
    props = (AttributeTypeProperties[]) null;
    if (tableRefID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, tableRefID);
        DataSet tables2 = TableLoadHelper.GetTables(sessionKeeper.Session, tableReference, true);
        if (tables2 != null)
        {
          if (tables2.Tables.Contains("IMS_DATA"))
          {
            if (tables2.Tables.Contains("IMS_ATTR_TYPES"))
            {
              tables1 = tables2;
              dtAttrs = tables2.Tables["IMS_ATTR_TYPES"];
              dtData = tables2.Tables["IMS_DATA"];
              ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
              TableLoadHelper.AssignAttributes(sessionKeeper.Session, tableRefID, tableReference, dtData, dtAttrs, out props, new List<CalculatedColumn>(), ref keyInfo);
              tables2.AcceptChanges();
            }
          }
        }
      }
    }
    return tables1;
  }

  /// <summary>
  /// 
  /// </summary>
  public static void WriteBlob(long objID, Guid attrGuid)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load("d:\\IPS\\Temp\\Files\\Сталь 4Х5МФ1С.xml");
    string innerXml = xmlDocument.InnerXml;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObjectActualCopy(objID, false).GetAttributeByGuid(attrGuid);
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream memoryStream = new MemoryStream(innerXml.Length))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(innerXml);
          binaryWriter.Flush();
          memoryStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) memoryStream.Length / 2))
          {
            service.PackStream((Stream) outStream, (Stream) memoryStream, 9);
            outStream.Position = 0L;
            byte[] buffer = outStream.GetBuffer();
            byte[] data = new byte[outStream.Length];
            byte[] dst = data;
            int length = (int) outStream.Length;
            Buffer.BlockCopy((Array) buffer, 0, (Array) dst, 0, length);
            try
            {
              BlobInformation blobInfo = new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
              IBlobWriter blobWriter = attributeByGuid as IBlobWriter;
              blobWriter.OpenBlob(blobInfo, false);
              blobWriter.WriteDataBlock(data);
            }
            catch
            {
            }
          }
        }
      }
    }
  }
}
