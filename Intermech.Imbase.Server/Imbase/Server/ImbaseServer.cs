// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseServer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Collections;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Imbase.Receptures;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseServer : LongLifeObject, IImbaseServer
{
  private Hashtable _catalogsInfoCache = new Hashtable();
  private static ImbaseServer _instance;
  private static ConcurrentDictionary<int, Stack<long>> NamingReferencesStacks = new ConcurrentDictionary<int, Stack<long>>();
  private static ConcurrentDictionary<int, Stack<Tuple<long, long>>> ObjectCreateStacks = new ConcurrentDictionary<int, Stack<Tuple<long, long>>>();
  private int contentModifyDateAttId = -1;

  public event ImbaseServer.DataSetChangedEventHandler DataSetChanged;

  private static int FindAttributeId(string paramName)
  {
    ICacheDataset service = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    DataTable table = service.GetTable("IMS_ATTRIBUTES");
    service.EnterReadLocker();
    try
    {
      DataRow[] dataRowArray = table.Select(string.Format("F_NAME='{0}' OR F_SHORT_NAME='{0}' OR F_ALIAS='{0}'", (object) paramName));
      if (dataRowArray.Length != 0)
        return Convert.ToInt32(dataRowArray[0][0]);
    }
    finally
    {
      service.ExitReadLocker();
    }
    return -1;
  }

  private static void GetNewRecordId(
    IUserSession session,
    string key,
    out long linkId,
    out long recordId,
    out ScanOldKeyStatus status)
  {
    linkId = -1L;
    recordId = -1L;
    status = ScanOldKeyStatus.BadImbaseKey;
    if (key == null || key.Length != 20 || char.ToUpper(key[0]) != 'I' || key[1] != '6')
      return;
    string s1 = key.Substring(2, 6);
    int conditionValue = 0;
    ref int local1 = ref conditionValue;
    if (!int.TryParse(s1, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local1))
      return;
    string s2 = key.Substring(8, 6);
    int num = 0;
    ref int local2 = ref num;
    if (!int.TryParse(s2, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local2))
      return;
    string s3 = key.Substring(14, 6);
    recordId = 0L;
    ref long local3 = ref recordId;
    if (!long.TryParse(s3, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local3))
      return;
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    DBRecordSetParams paramSet1 = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    });
    DataTable dataTable1 = objectCollection1.Select(paramSet1);
    if (dataTable1.Rows.Count == 0)
    {
      status = ScanOldKeyStatus.CatalogNotFound;
    }
    else
    {
      Convert.ToInt64(dataTable1.Rows[0][0]);
      Convert.ToString(dataTable1.Rows[0][1]);
      IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      paramSet1.Conditions[0].Value = (object) num;
      DBRecordSetParams paramSet2 = paramSet1;
      DataTable dataTable2 = objectCollection2.Select(paramSet2);
      if (dataTable2.Rows.Count == 0)
      {
        status = ScanOldKeyStatus.CatalogRecordNotFound;
      }
      else
      {
        linkId = Convert.ToInt64(dataTable2.Rows[0][0]);
        if (ImbaseServer.GetRecordRow(session, linkId, recordId) == null)
          status = ScanOldKeyStatus.TableRecordNotFound;
        else
          status = ScanOldKeyStatus.Success;
      }
    }
  }

  private long ResolveCatalogDef(
    string catalogDef,
    IUserSession session,
    ref ICatalogInfo catalogInfo)
  {
    if (catalogDef == null || catalogDef.Length == 0)
      return -1;
    catalogInfo = this.GetCatalogInfoFromCache(catalogDef);
    if (catalogInfo != null)
      return catalogInfo.Id;
    long objectID = -1;
    IDBObject dbObject = (IDBObject) null;
    if (catalogDef.Length > 0)
    {
      if (catalogDef[0] == '#')
        objectID = long.Parse(catalogDef.Substring(1));
      else if (ImbaseServer.IsGuid(catalogDef))
      {
        dbObject = session.GetObject(new Guid(catalogDef), false);
        if (dbObject != null)
          objectID = dbObject.ObjectID;
      }
      else
      {
        if (catalogDef[0] == '$')
          catalogDef = catalogDef.Substring(1);
        objectID = this.GetTableIdByTableName(catalogDef, Intermech.Imbase.Consts.ImbaseCatalogTypeID, session);
      }
    }
    if (objectID == -1L)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_15"), (object) catalogDef));
    if (dbObject == null)
      dbObject = session.GetObject(objectID);
    catalogInfo = (ICatalogInfo) new Intermech.Interfaces.Imbase.CatalogInfo(dbObject, catalogDef);
    this._catalogsInfoCache[(object) catalogDef] = (object) catalogInfo;
    return objectID;
  }

  private ICatalogInfo GetCatalogInfoFromCache(string catalogDef)
  {
    return this._catalogsInfoCache[(object) catalogDef] as ICatalogInfo;
  }

  private long GetTableIdByTableName(string value, int objectType, IUserSession session)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID, RelationalOperators.Equal, (object) value, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable1 = objectCollection.Select(paramSet);
    if (dataTable1.Rows.Count > 0)
      return Convert.ToInt64(dataTable1.Rows[0][0]);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-50, RelationalOperators.Equal, (object) value, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable2 = objectCollection.Select(paramSet);
    return dataTable2.Rows.Count > 0 ? Convert.ToInt64(dataTable2.Rows[0][0]) : -1L;
  }

  private List<long> GetTableLinks(string objectDef, int attId, IUserSession session)
  {
    List<long> longList = new List<long>(8);
    List<long> list = new List<long>(32 /*0x20*/);
    bool flag = false;
    if (ImbaseServer.IsGuid(objectDef))
    {
      IDBObject dbObject = session.GetObject(new Guid(objectDef));
      longList.Add(dbObject.ObjectID);
    }
    else
      flag = this.GetTablesByTableDef(objectDef, attId, longList, session);
    if (longList.Count == 0)
      return (List<long>) null;
    if (flag)
    {
      list.AddRange((IEnumerable<long>) longList);
    }
    else
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      dbRecordSetParams.Conditions[0].Content = ColumnContents.ID;
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataRowCollection rows = objectCollection.Select(paramSet).Rows;
      int count = rows.Count;
      for (int index = 0; index < count; ++index)
        CollectionUtils.AddSorted<long>(list, Convert.ToInt64(rows[index][0]));
    }
    return list;
  }

  private bool GetTablesByTableDef(
    string objectDef,
    int attId,
    List<long> ids,
    IUserSession session)
  {
    ids.Clear();
    bool tablesByTableDef = false;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID);
    if (attId != Intermech.Imbase.Consts.ImbaseInternalTableNameAttID)
    {
      objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      tablesByTableDef = true;
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attId, RelationalOperators.Equal, (object) objectDef, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    int length = objectDef.IndexOf('*');
    switch (length)
    {
      case -1:
        if (objectDef.IndexOf(',') != -1)
        {
          string[] strArray = objectDef.Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries);
          paramSet.Conditions[0].RelationalOperator = RelationalOperators.In;
          paramSet.Conditions[0].Value = (object) strArray;
          break;
        }
        break;
      case 0:
        paramSet.Conditions[0].RelationalOperator = RelationalOperators.EndString;
        paramSet.Conditions[0].Value = (object) objectDef.Substring(1);
        break;
      default:
        if (length == objectDef.Length - 1)
        {
          paramSet.Conditions[0].RelationalOperator = RelationalOperators.StartString;
          paramSet.Conditions[0].Value = (object) objectDef.Substring(0, length);
          break;
        }
        break;
    }
    DataTable dataTable = objectCollection.Select(paramSet);
    int count = dataTable.Rows.Count;
    for (int index = 0; index < count; ++index)
      ids.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    return tablesByTableDef;
  }

  internal static bool IsGuid(string objectDef)
  {
    bool flag = true;
    if (objectDef == null || objectDef.Length != 36 || objectDef[8] != '-' || objectDef[13] != '-' || objectDef[18] != '-')
      return false;
    if (objectDef[23] != '-')
      return false;
    try
    {
      Guid guid = new Guid(objectDef);
    }
    catch (Exception ex)
    {
      flag = false;
    }
    return flag;
  }

  internal void AssignAttributesFromObjectID(
    IUserSession session,
    IDBObject destObject,
    long baseObjectId,
    int parentAttId,
    bool onlyNull,
    bool onlyReferences,
    List<int> attIds)
  {
    this.AssignAttributesFromObject(session, destObject, session.GetObject(baseObjectId), parentAttId, onlyNull, onlyReferences, attIds, (List<AttributeValues>) null);
  }

  private void AssignAttributesFromObject(
    IUserSession session,
    IDBObject destObject,
    IDBObject baseObject,
    int parentAttId,
    bool onlyNull,
    bool onlyReferences,
    List<int> attIds,
    List<AttributeValues> aValuesList)
  {
    ExtendedServiceHelper.GetObjTypeData(destObject.ObjectType, session);
    bool flag = false;
    List<AttributeValues> attributeValuesList = aValuesList;
    if (attributeValuesList == null)
    {
      attributeValuesList = new List<AttributeValues>();
      flag = true;
    }
    IDBAttributeCollection attributes1 = baseObject.Attributes;
    IDBAttributeCollection attributes2 = destObject.Attributes;
    IDBObjectType objectType = session.GetObjectType(destObject.ObjectType);
    string keyValue = (string) null;
    IDBAttribute4TypeCollection attributes3 = objectType.Attributes;
    if (!onlyReferences)
    {
      int count = attributes1.Count;
      for (int AttrIndex = 0; AttrIndex < count; ++AttrIndex)
      {
        IDBAttribute att = attributes1[AttrIndex];
        if (!att.IsNull)
        {
          if ((MetaDataHelper.GetAttributeType(att.AttributeID).Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
          {
            if (att.AttributeID == Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID && (attIds == null || attIds.Contains(att.AttributeID)))
            {
              keyValue = att.AsString;
            }
            else
            {
              long linkId;
              long recordId;
              if (ImbaseHelper.TryParseRecordReference(session, att.AsString, out linkId, out recordId))
              {
                DataRow recordRow = ImbaseServer.GetRecordRow(session, linkId, recordId, false);
                if (recordRow != null)
                  this.AssignAttributesFromRow(session, destObject, -1L, recordRow, -1, true, false, attIds);
              }
            }
          }
          else if (attIds == null || attIds.Contains(att.AttributeID))
          {
            IDBAttribute attribute = this.FindAttribute(attributes2, att);
            if (attribute == null)
            {
              if ((attributes3 == null || attributes3.GetAttributeByID(att.AttributeID) != null) && !this.IsInternal(att) && (MetaDataHelper.GetAttribute4ObjectType(destObject.ObjectType, att.AttributeID).Options & AttributeOptions.DontCopyPrototypeValue) != AttributeOptions.DontCopyPrototypeValue && !this.ValueInList(attributeValuesList, att.AttributeID))
                attributeValuesList.Add(new AttributeValues(att.AttributeID, (object) att.Values));
            }
            else
            {
              if (!attribute.ReadOnly)
              {
                if (onlyNull && attIds == null)
                {
                  if (attribute.IsNull && !this.ValueInList(attributeValuesList, att.AttributeID))
                    attributeValuesList.Add(new AttributeValues(att.AttributeID, (object) att.Values));
                }
                else if (!AttributeValues.ValuesEquals(att.Values, attribute.Values) && !this.ValueInList(attributeValuesList, att.AttributeID))
                  attributeValuesList.Add(new AttributeValues(att.AttributeID, (object) att.Values));
              }
              attIds?.Remove(att.AttributeID);
            }
          }
        }
      }
    }
    if (parentAttId != -1)
    {
      if (attIds == null || attIds.Contains(parentAttId))
      {
        if (!this.ValueInList(attributeValuesList, parentAttId))
          attributeValuesList.Add(new AttributeValues(parentAttId, (object) baseObject.ObjectID));
        attIds?.Remove(parentAttId);
      }
      if (!onlyReferences && parentAttId == Intermech.Imbase.Consts.ImbaseObjectRefAttID && (attIds == null || attIds.Count > 0))
      {
        foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetParentSostavData(new ObjInfoItem(baseObject), session, (IEnumerable<int>) new int[1]
        {
          MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"))
        }, true).Rows)
        {
          if (Convert.ToInt32(row["F_OBJECT_TYPE"]) == Intermech.Imbase.Consts.ImbaseFolderTypeID)
          {
            long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
            this.AssignAttributesFromObject(session, destObject, session.GetObject(int64), -1, true, onlyReferences, attIds, attributeValuesList);
          }
          else
            break;
        }
      }
    }
    else if (baseObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID && session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService && customService.Info != null)
    {
      char code = customService.Info.Code;
      string siteId = baseObject.SiteID;
      if (siteId.Length == 1 || siteId.Length >= 2 && (int) code != (int) siteId[1])
        (destObject as DBObject).SetSiteID(baseObject.SiteID);
    }
    if (flag && attributeValuesList != null && attributeValuesList.Count > 0)
      destObject.SetAttributesValues(attributeValuesList.ToArray());
    if (string.IsNullOrEmpty(keyValue))
      return;
    int materialGradeAttrD = Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD;
    IDBAttribute attributeById = destObject.GetAttributeByID(materialGradeAttrD);
    long linkId1;
    long recordId1;
    if (attributeById != null && !attributeById.IsNull || attributes3 != null && attributes3.GetAttributeByID(materialGradeAttrD) == null || !ImbaseHelper.TryParseRecordReference(session, keyValue, out linkId1, out recordId1))
      return;
    long num = this.CreateObject(session.SessionGUID, -1L, linkId1, recordId1, true, -1);
    destObject.Attributes.AddAttribute(materialGradeAttrD, false, new object[1]
    {
      (object) num
    });
  }

  private bool ValueInList(List<AttributeValues> valuesList, int attId)
  {
    if (valuesList == null)
      return false;
    int count = valuesList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (valuesList[index].AttributeID == attId)
        return true;
    }
    return false;
  }

  private void AssignAttributesFromRow(
    IUserSession session,
    IDBObject destObject,
    long parentId,
    DataRow row,
    int parentAttId,
    bool onlyNull,
    bool onlyReferences,
    List<int> attIds)
  {
    IDBAttributeCollection attributes1 = destObject.Attributes;
    IDBObjectType objectType = session.GetObjectType(destObject.ObjectType);
    IDBAttribute dbAttribute = (IDBAttribute) null;
    Dictionary<IDBAttributeType, string> refDict = new Dictionary<IDBAttributeType, string>(32 /*0x20*/);
    IDBAttribute4TypeCollection attributes2 = objectType.Attributes;
    Guid g = new Guid("cad0038c-306c-11d8-b4e9-00304f19f545");
    if (!onlyReferences)
    {
      int count = row.Table.Columns.Count;
      for (int index = 0; index < count; ++index)
      {
        DataColumn column = row.Table.Columns[index];
        int result1;
        if (int.TryParse(column.Caption, out result1) && !this.IsInternal(result1))
        {
          if (attIds != null)
          {
            if (attIds.Contains(result1))
              attIds.Remove(result1);
            else
              continue;
          }
          object obj = row[index];
          if (!TableLoadHelper.IsNull(obj))
          {
            IDBAttributeType attributeType = session.GetAttributeType(result1);
            bool flag = TableLoadHelper.IsArray(attributeType) && obj is ValuesArray;
            object[] objArray = (object[]) null;
            if (flag)
            {
              objArray = (obj as ValuesArray).GetArray();
              if (attributeType.AttributeType == FieldTypes.ftObjectLink)
                this.RenameObjectLinks(session, objArray);
            }
            if (column.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
            {
              long int64 = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
              if (flag)
                this.ConvertToMeasuredValues(objArray, int64);
              double result2;
              if (double.TryParse(obj.ToString(), out result2))
                obj = (object) new MeasuredValue(result2, Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]));
            }
            else if (attributeType.AttributeType == FieldTypes.ftMeasured)
            {
              long num = attributeType.SizeType;
              if (num < 1L)
                num = Intermech.Imbase.Consts.mmUnitID;
              if (flag)
                this.ConvertToMeasuredValues(objArray, num);
              try
              {
                obj = (object) new MeasuredValue(Convert.ToDouble(obj), num);
              }
              catch
              {
                throw new Exception($"Атрибут '{attributeType.Name}' id={attributeType.AttributeID}: невозможно преобразовать значение '{obj}' в вещественное число, выраженное в единицах измерения.");
              }
            }
            else if (attributeType.AttributeType == FieldTypes.ftObjectLink && obj is string)
            {
              string str = Convert.ToString(obj);
              if (GuidHelper.IsGuid(str))
                obj = (object) new Guid(str);
            }
            IDBAttribute byId = attributes1.FindByID(result1);
            bool assignValue = false;
            if (byId == null)
            {
              if (attributes2 == null || attributes2.GetAttributeByID(result1) != null)
                assignValue = true;
            }
            else
              assignValue = true;
            if (!attributeType.PropertiesStructure.AttributeGuid.Equals(g))
            {
              if (this.ExtractReferenceData(session, attributeType, row, assignValue, ref obj, ref refDict))
                continue;
            }
            if (byId == null)
            {
              if (attributes2 == null || attributes2.GetAttributeByID(result1) != null)
              {
                if (flag)
                {
                  dbAttribute = attributes1.AddAttribute(result1, true, objArray);
                }
                else
                {
                  IDBAttribute destAtt = attributes1.AddAttribute(result1, true);
                  if (!destAtt.ReadOnly)
                    ImbaseServer.SetAttributeValue(destAtt, obj);
                }
              }
            }
            else if (!byId.ReadOnly)
            {
              if (onlyNull && attIds == null)
              {
                if (byId.IsNull)
                {
                  if (flag)
                    ImbaseServer.SetAttributeValue(byId, obj);
                  else
                    ImbaseServer.SetAttributeValue(byId, obj);
                }
              }
              else if (flag)
                ImbaseServer.SetAttributeValues(byId, objArray);
              else
                ImbaseServer.SetAttributeValue(byId, obj);
            }
          }
        }
      }
      if (refDict.Count > 0)
      {
        foreach (IDBAttributeType key in refDict.Keys)
        {
          string str = refDict[key];
          if (key.AttributeType == FieldTypes.ftObjectLink)
          {
            try
            {
              Guid objectGUID = new Guid(str);
              IDBObject baseObject = session.GetObject(objectGUID);
              this.AssignAttributesFromObject(session, destObject, baseObject, -1, true, false, attIds, (List<AttributeValues>) null);
            }
            catch
            {
            }
          }
          else
          {
            long linkId;
            long recordId;
            if (ImbaseHelper.TryParseRecordReference(session, str, out linkId, out recordId))
            {
              try
              {
                DataRow recordRow = ImbaseServer.GetRecordRow(session, linkId, recordId);
                if (recordRow != null)
                  this.AssignAttributesFromRow(session, destObject, -1L, recordRow, -1, true, false, attIds);
              }
              catch
              {
              }
            }
          }
        }
      }
    }
    if (parentAttId == -1)
      return;
    IDBAttribute byId1 = attributes1.FindByID(parentAttId);
    if (byId1 == null)
      attributes1.AddAttribute(parentAttId, false, new object[1]
      {
        (object) parentId
      });
    else
      byId1.AsInteger = parentId;
  }

  private static void SetAttributeValue(IDBAttribute destAtt, object value)
  {
    try
    {
      destAtt.Value = value;
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("Imbase_InvalidCast_Error"), value, (object) destAtt.Name), ex.InnerException);
    }
  }

  private static void SetAttributeValues(IDBAttribute destAtt, object[] values)
  {
    try
    {
      destAtt.Values = values;
    }
    catch (InvalidCastException ex)
    {
      throw new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("Imbase_InvalidCast_Error"), (object) values, (object) destAtt.Name), ex.InnerException);
    }
  }

  private void ConvertToMeasuredValues(object[] values, long measureId)
  {
    if (values == null)
      return;
    int length = values.Length;
    for (int index = 0; index < length; ++index)
    {
      double result;
      if (!double.TryParse(values[index].ToString(), out result))
        result = 0.0;
      values[index] = (object) new MeasuredValue(result, measureId);
    }
  }

  private void RenameObjectLinks(IUserSession session, object[] array)
  {
    if (array == null)
      return;
    int length = array.Length;
    for (int index = 0; index < length; ++index)
    {
      object obj = array.GetValue(index);
      if (obj != null)
      {
        try
        {
          string g = obj.ToString();
          if (!string.IsNullOrEmpty(g))
          {
            QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(g));
            if (!objectInfo.Empty)
              array.SetValue((object) objectInfo.ObjectID, index);
          }
        }
        catch
        {
        }
      }
    }
  }

  private bool ExtractReferenceData(
    IUserSession session,
    IDBAttributeType att,
    DataRow row,
    bool assignValue,
    ref object value,
    ref Dictionary<IDBAttributeType, string> refDict)
  {
    string str = (att as IDBGuid).GUID.ToString();
    if (str.Equals("2063eafd-e6b2-4e7e-aaf8-196d34709a82", StringComparison.InvariantCultureIgnoreCase))
      return true;
    if (value is ValuesArray)
      return false;
    if (att.AttributeType == FieldTypes.ftObjectLink)
    {
      if (assignValue)
      {
        try
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(value.ToString()));
          if (!objectInfo.Empty)
            value = (object) Math.Abs(objectInfo.ObjectID);
        }
        catch
        {
          return true;
        }
        return false;
      }
      refDict.Add(att, value.ToString());
      return true;
    }
    AttributeOptions attributeOptions = AttributeOptions.None;
    DataRow[] dataRowArray = row.Table.DataSet.Tables["IMS_ATTR_TYPES"].Select($"[{"F_ATTRIBUTE_GUID"}]='{str}'");
    if (dataRowArray != null && dataRowArray.Length != 0)
      attributeOptions = (AttributeOptions) Convert.ToInt32(dataRowArray[0]["F_OPTIONS"]);
    if ((att.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef || (attributeOptions & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
    {
      if (assignValue)
      {
        long linkId;
        long recordId;
        if (!ImbaseHelper.TryParseRecordReference(session, value.ToString(), out linkId, out recordId))
          return true;
        DataRow recordRow = ImbaseServer.GetRecordRow(session, linkId, recordId);
        if (recordRow != null)
        {
          int columnIndex = recordRow.Table.Columns.IndexOf(att.AttributeID.ToString());
          if (columnIndex != -1)
          {
            value = recordRow[columnIndex];
            return TableLoadHelper.IsNull(value);
          }
        }
      }
      else
      {
        refDict.Add(att, value.ToString());
        return true;
      }
    }
    return false;
  }

  private bool IsInternal(int attId)
  {
    return attId < 0 || ImbaseHelper.IsSystemAttribute(attId) || ImbaseHelper.SkipAtttribute(attId);
  }

  private bool IsInternal(IDBAttribute att)
  {
    if (att == null)
      return false;
    return (att.AttributeType.Options & AttributeOptions.Internal) != AttributeOptions.None || this.IsInternal(att.AttributeID);
  }

  private IDBAttribute FindAttribute(IDBAttributeCollection atts, IDBAttribute att)
  {
    return atts.FindByID(att.AttributeID);
  }

  private int GetNewObjectType(
    IUserSession session,
    long objectVerId,
    ArrayList path,
    ref bool createNew)
  {
    int newObjectType = -1;
    bool flag1 = false;
    ImbaseObjCreateInfo objCreateInfo;
    if (ServerServices.GetService(typeof (IImbaseObjInfoService)) is IImbaseObjInfoService service && service.GetCreationMode(objectVerId, session.SessionGUID, out objCreateInfo))
    {
      createNew = objCreateInfo.CreateMode == ImbaseObjCreateMode.iocmCreateNew;
      return objCreateInfo.ObjectType;
    }
    IDBObject dbObject1 = session.GetObject(objectVerId);
    IDBAttributeCollection attributes1 = dbObject1.Attributes;
    long id = dbObject1.ID;
    path?.Add((object) new ImbaseServer.PathItem(objectVerId, dbObject1.ObjectType));
    IDBAttribute byId1 = attributes1.FindByID(Intermech.Imbase.Consts.CreatedObjectAttID);
    if (byId1 != null)
      newObjectType = this.GetObjectTypeFromAtt(session, byId1);
    IDBAttribute byId2 = attributes1.FindByID(Intermech.Imbase.Consts.CreateNewObjectAttID);
    if (byId2 != null)
    {
      flag1 = true;
      createNew = byId2.AsBoolean;
    }
    bool flag2 = true;
    IDBRelationCollection relationCollection = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseDefaultLinkID);
    relationCollection.LocalTypesMode = true;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    })
    {
      TableName = "tb"
    };
    while (flag2)
    {
      DataTable dataTable = relationCollection.EntersIn(paramSet, id, false);
      if (dataTable.Rows.Count == 1)
      {
        objectVerId = Convert.ToInt64(dataTable.Rows[0][0]);
        path?.Add((object) new ImbaseServer.PathItem(objectVerId, Convert.ToInt32(dataTable.Rows[0][1])));
        IDBObject dbObject2 = session.GetObject(objectVerId);
        id = dbObject2.ID;
        IDBAttributeCollection attributes2 = dbObject2.Attributes;
        if (newObjectType == -1)
        {
          IDBAttribute byId3 = attributes2.FindByID(Intermech.Imbase.Consts.CreatedObjectAttID);
          if (byId3 != null)
            newObjectType = this.GetObjectTypeFromAtt(session, byId3);
        }
        else
          flag2 = false;
        if (!flag1)
        {
          flag2 = true;
          IDBAttribute byId4 = attributes2.FindByID(Intermech.Imbase.Consts.CreateNewObjectAttID);
          if (byId4 != null)
          {
            flag2 = false;
            flag1 = true;
            createNew = byId4.AsBoolean;
          }
        }
      }
      else
        flag2 = false;
    }
    return newObjectType;
  }

  private int GetNewObjectType(IUserSession session, long objectVerId, ArrayList path)
  {
    ImbaseObjCreateInfo objCreateInfo;
    if (ServerServices.GetService(typeof (IImbaseObjInfoService)) is IImbaseObjInfoService service && service.GetCreationMode(objectVerId, session.SessionGUID, out objCreateInfo))
      return objCreateInfo.ObjectType;
    int newObjectType = -1;
    IDBObject dbObject1 = session.GetObject(objectVerId);
    IDBAttributeCollection attributes1 = dbObject1.Attributes;
    long id = dbObject1.ID;
    path?.Add((object) new ImbaseServer.PathItem(objectVerId, dbObject1.ObjectType));
    IDBAttribute byId1 = attributes1.FindByID(Intermech.Imbase.Consts.CreatedObjectAttID);
    if (byId1 != null)
      return this.GetObjectTypeFromAtt(session, byId1);
    bool flag = true;
    IDBRelationCollection relationCollection = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseDefaultLinkID);
    relationCollection.LocalTypesMode = true;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    })
    {
      TableName = "tb"
    };
    while (flag)
    {
      DataTable dataTable = relationCollection.EntersIn(paramSet, id, false);
      if (dataTable.Rows.Count == 1)
      {
        objectVerId = Convert.ToInt64(dataTable.Rows[0][0]);
        path?.Add((object) new ImbaseServer.PathItem(objectVerId, Convert.ToInt32(dataTable.Rows[0][1])));
        IDBObject dbObject2 = session.GetObject(objectVerId);
        id = dbObject2.ID;
        IDBAttributeCollection attributes2 = dbObject2.Attributes;
        if (newObjectType == -1)
        {
          IDBAttribute byId2 = attributes2.FindByID(Intermech.Imbase.Consts.CreatedObjectAttID);
          if (byId2 != null)
          {
            newObjectType = this.GetObjectTypeFromAtt(session, byId2);
            flag = false;
          }
        }
        else
          flag = false;
      }
      else
        flag = false;
    }
    return newObjectType;
  }

  private long FindCopy(
    IUserSession session,
    int objectType,
    long imbaseObjectId,
    int referenceAttType,
    long tableRecordId,
    int linkAttType)
  {
    DataTable dataTable = ImbaseServer.FinCopies(session, objectType, imbaseObjectId, referenceAttType, tableRecordId, linkAttType);
    if (dataTable.Rows.Count <= 0)
      return -1;
    long objectID = Convert.ToInt64(dataTable.Rows[0][0]);
    if (dataTable.Rows.Count == 1)
      return objectID;
    DataRowCollection rows = dataTable.Rows;
    int count = rows.Count;
    long userId = session.UserID;
    int columnIndex1 = 0;
    int columnIndex2 = 2;
    int columnIndex3 = 5;
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      if (Convert.ToInt64(dataRow[columnIndex2]) == userId)
        return Convert.ToInt64(dataRow[columnIndex1]);
    }
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      if (Convert.ToInt32(dataRow[columnIndex3]) == 0)
        return Convert.ToInt64(dataRow[columnIndex1]);
    }
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      if (Convert.ToInt32(dataRow[columnIndex3]) == 1)
        return Convert.ToInt64(dataRow[columnIndex1]);
    }
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      if (Convert.ToInt32(dataRow[columnIndex3]) == 2)
        return Convert.ToInt64(dataRow[columnIndex1]);
    }
    QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
    IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(objectInfo.ID, "cad005aa-306c-11d8-b4e9-00304f19f545", false);
    if (objectByVersionsRule != null)
      objectID = objectByVersionsRule.ObjectID;
    return objectID;
  }

  private static DataTable FinCopies(
    IUserSession session,
    int objectType,
    long imbaseObjectId,
    int referenceAttType,
    long tableRecordId,
    int linkAttType)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
    ConditionStructure[] conditions;
    if (tableRecordId == -1L)
      conditions = new ConditionStructure[1]
      {
        new ConditionStructure(referenceAttType, RelationalOperators.Equal, (object) imbaseObjectId, LogicalOperators.NONE, 0, false)
      };
    else
      conditions = new ConditionStructure[2]
      {
        new ConditionStructure(referenceAttType, RelationalOperators.Equal, (object) imbaseObjectId, LogicalOperators.AND, 0, false),
        new ConditionStructure(linkAttType, RelationalOperators.Equal, (object) tableRecordId, LogicalOperators.NONE, 0, false)
      };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[5]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_VERSION_ID,
      (object) ObligatoryObjectAttributes.F_CHKOUT_BY,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.F_LC_STEP
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_VERSION_ID
    }, new SortOrders[1]{ SortOrders.DESC })
    {
      TableName = "tbl"
    };
    DataTable dt = objectCollection.Select(paramSet);
    DataColumn column = new DataColumn("ModifyMode", typeof (ObjectModifyModes))
    {
      DefaultValue = (object) ObjectModifyModes.CantModify
    };
    dt.Columns.Add(column);
    ImbaseServer.FillObjectModifyMode(session, dt, 3, 4, 5);
    return dt;
  }

  private static void FillObjectModifyMode(
    IUserSession session,
    DataTable dt,
    int colType,
    int colLCStep,
    int colMode)
  {
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(Convert.ToInt32(row[colLCStep]), Convert.ToInt32(row[colType]));
      row[colMode] = (object) lifecycleStep.ObjectModifyMode;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void AppendPath(
    string path,
    ICollection<string> catalogs,
    IDictionary<string, bool> folders)
  {
    for (int length = path.Length - 2; length > 0; length -= 2)
    {
      string key = path.Substring(0, length);
      if (length == 2)
      {
        if (catalogs is ISet<string> || !catalogs.Contains(key))
          catalogs.Add(key);
      }
      else
        folders[key] = false;
    }
  }

  private static List<Guid> TranslateTypeToGuids(IUserSession session, object needTypes)
  {
    List<Guid> guids = new List<Guid>();
    Type type = needTypes.GetType();
    if (type == typeof (string))
    {
      string str = needTypes.ToString();
      if (GuidHelper.IsGuid(str))
        guids.Add(new Guid(str));
      return guids;
    }
    if (type == typeof (Guid))
    {
      guids.Add((Guid) needTypes);
      return guids;
    }
    if (type == typeof (int))
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(needTypes));
      if (objectType != null)
        guids.Add(objectType.Guid);
      return guids;
    }
    switch (needTypes)
    {
      case IEnumerable<int> _:
        foreach (int needType in needTypes as IEnumerable<int>)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(needType);
          if (objectType != null)
            guids.Add(objectType.Guid);
        }
        return guids;
      case IEnumerable<string> _:
        foreach (string needType in needTypes as IEnumerable<string>)
        {
          if (GuidHelper.IsGuid(needType))
            guids.Add(new Guid(needType));
        }
        return guids;
      case IEnumerable<Guid> _:
        guids.AddRange(needTypes as IEnumerable<Guid>);
        return guids;
      default:
        return guids;
    }
  }

  private static void GetClassifKeys(
    ICollection<string> catalogFilter,
    long[] catalogList,
    IUserSession session)
  {
    if (catalogFilter == null)
      return;
    catalogFilter.Clear();
    if (catalogList == null)
      return;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID), RelationalOperators.In, (object) catalogList, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    }, (object[]) null, (SortOrders[]) null)
    {
      ColumnNames = new ColumnNameMapping[2]
      {
        ColumnNameMapping.ID,
        ColumnNameMapping.ID
      },
      TableName = "f",
      FailIfNotFound = false
    };
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
    {
      if (row[1] != null && row[1] != DBNull.Value)
      {
        string str = Convert.ToString(row[1]);
        if (catalogFilter is ISet<string> || !catalogFilter.Contains(str))
          catalogFilter.Add(str);
      }
    }
  }

  private bool GetFoldersForCreateType_Folders4Type(
    IUserSession session,
    object needType,
    long[] catalogs,
    DBRecordSetParams paramsSet,
    out DataTable table)
  {
    table = (DataTable) null;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (needType == null)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_17"), nameof (needType));
    HashSet<string> catalogFilter = new HashSet<string>();
    List<Guid> guids = ImbaseServer.TranslateTypeToGuids(session, needType);
    List<Guid> childrenGuidRecursive = MetaDataHelper.GetObjectTypeChildrenGuidRecursive((IEnumerable<Guid>) guids);
    childrenGuidRecursive.AddRange((IEnumerable<Guid>) guids);
    if (childrenGuidRecursive.Count == 0)
    {
      table = new DataTable();
      return false;
    }
    List<string> stringList = new List<string>((IEnumerable<string>) childrenGuidRecursive.ConvertAll<string>((Converter<Guid, string>) (item => item.ToString())));
    ImbaseServer.GetClassifKeys((ICollection<string>) catalogFilter, catalogs, session);
    ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.In, (object) stringList.ToArray(), LogicalOperators.AND, 1, false),
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, -1, false)
    };
    paramsSet.Conditions = conditionStructureArray;
    DataTable resultTable = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    if (resultTable == null)
    {
      table = new DataTable();
      return false;
    }
    string name = $"{Intermech.Imbase.Consts.ClassifFolderKeyAttId}";
    if (!resultTable.Columns.Contains(name))
    {
      table = new DataTable();
      return false;
    }
    DataColumn column = ImbaseServer.AppendFilterColumn(resultTable, false);
    List<DataRow> dataRowList = new List<DataRow>();
    if (catalogFilter.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) resultTable.Rows)
      {
        string str1 = Convert.ToString(row[4]);
        if (str1.Length > 1)
        {
          string str2 = str1.Substring(0, 2);
          if (!catalogFilter.Contains(str2))
            dataRowList.Add(row);
          else
            row[column] = (object) true;
        }
      }
      if (dataRowList.Count > 0)
      {
        foreach (DataRow dataRow in dataRowList)
          dataRow.Delete();
        resultTable.AcceptChanges();
      }
    }
    if (resultTable.Columns.Count > 0)
      ImbaseServer.RenameColumns(resultTable);
    table = resultTable;
    return true;
  }

  private bool GetFoldersForCreateType_LoadSubFolderInfo(
    IUserSession session,
    DBRecordSetParams paramsSet,
    DataTable tableSource,
    out List<DataRow> subFolderDataRows)
  {
    if (tableSource == null)
      throw new ArgumentNullException(nameof (tableSource));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    subFolderDataRows = new List<DataRow>();
    List<StrWithFlag> strWithFlagList = new List<StrWithFlag>(tableSource.Rows.Count);
    int columnIndex = tableSource.Columns.IndexOf("F_PATH");
    foreach (DataRow row in (InternalDataCollectionBase) tableSource.Rows)
      strWithFlagList.Add(new StrWithFlag(Convert.ToString(row[columnIndex]), false));
    strWithFlagList.Sort();
    List<string> stringList = new List<string>();
    string str1 = " ";
    foreach (StrWithFlag strWithFlag in strWithFlagList)
    {
      if (!strWithFlag.Str.StartsWith(str1))
        stringList.Add(str1 = strWithFlag.Str);
    }
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    int num1 = 0;
    int num2 = 0;
    string columnName = $"{Intermech.Imbase.Consts.ClassifFolderKeyAttId}";
    while (num1 < stringList.Count)
    {
      int num3 = stringList.Count - num1;
      num2 += num3 < 500 ? num3 : 500;
      conditionStructureList.Clear();
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.AND, 0, false));
      for (int index = num1; index < num2 - 1; ++index)
        conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) stringList[index], LogicalOperators.OR, conditionStructureList.Count == 1 ? 1 : 0, true));
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) stringList[num2 - 1], LogicalOperators.NONE, num2 == 1 ? 0 : -1, true));
      num1 = num2;
      paramsSet.Conditions = conditionStructureList.ToArray();
      DataTable resultTable = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
      if (resultTable != null)
      {
        ImbaseServer.AppendFilterColumn(resultTable, false);
        foreach (DataRow row in (InternalDataCollectionBase) resultTable.Rows)
        {
          subFolderDataRows.Add(row);
          strWithFlagList.Add(new StrWithFlag(Convert.ToString(row[columnName]), true));
        }
      }
    }
    if (subFolderDataRows.Count == 0)
      return false;
    strWithFlagList.Sort();
    int index1 = 0;
    while (index1 < strWithFlagList.Count)
    {
      StrWithFlag strWithFlag1 = strWithFlagList[index1];
      if (strWithFlag1.Flag)
      {
        string str2 = strWithFlag1.Str;
        if (str2.Length > 2)
        {
          StrWithFlag strWithFlag2 = new StrWithFlag(str2.Substring(0, str2.Length - 2), true);
          if (strWithFlagList.BinarySearch(strWithFlag2) < 0)
          {
            strWithFlagList.RemoveAt(index1);
            continue;
          }
        }
      }
      ++index1;
    }
    for (int index2 = subFolderDataRows.Count - 1; index2 >= 0; --index2)
    {
      DataRow dataRow = subFolderDataRows[index2];
      if (dataRow != null)
      {
        StrWithFlag strWithFlag = new StrWithFlag(Convert.ToString(dataRow[columnName]), false);
        if (strWithFlagList.BinarySearch(strWithFlag) < 0)
          subFolderDataRows.RemoveAt(index2);
      }
    }
    return subFolderDataRows.Count != 0;
  }

  private bool GetFoldersForCreateType_ExtractSubFolderInfo(
    DataTable tableSource,
    DataTable tableAllCatalogs,
    out List<DataRow> subFolderDataRowList)
  {
    subFolderDataRowList = (List<DataRow>) null;
    if (tableSource == null)
      throw new ArgumentNullException(nameof (tableSource));
    if (tableAllCatalogs == null)
      throw new ArgumentNullException(nameof (tableAllCatalogs));
    List<StrWithFlag> strWithFlagList = new List<StrWithFlag>(tableSource.Rows.Count);
    Dictionary<string, bool> sourcePathCache = new Dictionary<string, bool>(tableSource.Rows.Count);
    int columnIndex1 = tableSource.Columns.IndexOf("F_PATH");
    foreach (DataRow row in (InternalDataCollectionBase) tableSource.Rows)
    {
      string str = Convert.ToString(row[columnIndex1]);
      sourcePathCache[str] = true;
      strWithFlagList.Add(new StrWithFlag(str, false));
    }
    int idxColCreatedObjectAll = tableAllCatalogs.Columns.IndexOf(Intermech.Imbase.Consts.CreatedObjectAttID.ToString());
    subFolderDataRowList = tableAllCatalogs.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => string.IsNullOrEmpty(Convert.ToString(row[idxColCreatedObjectAll])))).ToList<DataRow>();
    if (subFolderDataRowList.Count == 0)
      return false;
    int columnIndex2 = tableAllCatalogs.Columns.IndexOf("F_PATH");
    List<string> stringList = new List<string>();
    foreach (DataRow dataRow in subFolderDataRowList)
    {
      if (dataRow != null)
      {
        string str = Convert.ToString(dataRow[columnIndex2]);
        int length = str.Length - 2;
        if (length > 0)
        {
          bool couldAdd = false;
          stringList.Clear();
          stringList.Add(str);
          for (; length > 0; length -= 2)
          {
            string key = str.Substring(0, length);
            stringList.Add(key);
            if (length >= 2 && sourcePathCache.TryGetValue(key, out couldAdd))
              break;
          }
          stringList.ForEach((Action<string>) (item => sourcePathCache[item] = couldAdd));
          if (couldAdd)
            strWithFlagList.Add(new StrWithFlag(str, true));
        }
      }
    }
    strWithFlagList.Sort();
    int num = 0;
    while (num < strWithFlagList.Count)
    {
      StrWithFlag strWithFlag1 = strWithFlagList[num];
      if (strWithFlag1.Flag)
      {
        string str = strWithFlag1.Str;
        if (str.Length > 2)
        {
          StrWithFlag strWithFlag2 = new StrWithFlag(str.Substring(0, str.Length - 2), true);
          if (strWithFlagList.BinarySearch(0, num, strWithFlag2, (IComparer<StrWithFlag>) null) < 0)
          {
            strWithFlagList.RemoveAt(num);
            continue;
          }
        }
      }
      ++num;
    }
    for (int index = subFolderDataRowList.Count - 1; index >= 0; --index)
    {
      DataRow dataRow = subFolderDataRowList[index];
      if (dataRow != null)
      {
        StrWithFlag strWithFlag = new StrWithFlag(Convert.ToString(dataRow[columnIndex2]), false);
        if (strWithFlagList.BinarySearch(strWithFlag) < 0)
          subFolderDataRowList.RemoveAt(index);
      }
    }
    return subFolderDataRowList.Count != 0;
  }

  internal static ImbaseServer Instance => ImbaseServer._instance;

  internal int GetObjectTypeFromAtt(IUserSession session, IDBAttribute att)
  {
    string asString = att.AsString;
    return asString.Length > 0 ? session.GetObjectType(new Guid(asString)).ObjectType : -1;
  }

  internal static DBRecordSetParams CreateParamsSet(
    ConditionStructure[] conds,
    AttributableElements elementType = AttributableElements.Object)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 2),
      new ColumnDescriptor((object) (elementType == AttributableElements.Relation ? Intermech.Imbase.Consts.RelationSortIndex : Convert.ToInt32((object) ObligatoryObjectAttributes.F_LC_STEP)), AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseUsingAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramsSet = new DBRecordSetParams(conds, columns)
    {
      TableName = "f",
      FailIfNotFound = false
    };
    if (paramsSet.Tags == null)
      paramsSet.Tags = new HybridDictionary();
    return paramsSet;
  }

  internal static void RenameColumns(DataTable resultTable)
  {
    DataColumnCollection columns = resultTable.Columns;
    columns[0].ColumnName = "F_OBJECT_ID";
    columns[1].ColumnName = "F_OBJECT_TYPE";
    columns[2].ColumnName = "CAPTION";
    columns[3].ColumnName = "F_SORT";
    columns[4].ColumnName = "F_PATH";
    columns[5].ColumnName = "F_APPLICABILITY";
  }

  internal static void BuildUpTree(DataTable dt, IUserSession session, DBRecordSetParams paramsSet)
  {
    if (dt == null || session == null)
      return;
    Dictionary<string, bool> folders = new Dictionary<string, bool>();
    HashSet<string> stringSet = new HashSet<string>();
    int columnIndex = dt.Columns.IndexOf("F_PATH");
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      ImbaseServer.AppendPath(Convert.ToString(row[columnIndex]), (ICollection<string>) stringSet, (IDictionary<string, bool>) folders);
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      string key = Convert.ToString(row[columnIndex]);
      folders.Remove(key);
      stringSet.Remove(key);
    }
    DataTable dataTable = dt.Clone();
    if (stringSet.Count > 0)
    {
      paramsSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) stringSet.ToArray<string>(), LogicalOperators.NONE, 0, true)
      };
      DataTable source = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID).Select(paramsSet);
      ImbaseServer.AppendRows(dataTable, source, 5);
    }
    string[] strArray = new string[folders.Count];
    folders.Keys.CopyTo(strArray, 0);
    if (folders.Count > 0)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      int length1 = strArray.Length;
      int sourceIndex = 0;
      int val2 = 256 /*0x0100*/;
      paramsSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) strArray, LogicalOperators.NONE, 0, true)
      };
      string[] destinationArray = (string[]) null;
      while (length1 > 0)
      {
        int length2 = Math.Min(length1, val2);
        if (destinationArray == null || destinationArray.Length != length2)
          destinationArray = new string[length2];
        Array.Copy((Array) strArray, sourceIndex, (Array) destinationArray, 0, length2);
        paramsSet.Conditions[0].Value = (object) destinationArray;
        DataTable source = objectCollection.Select(paramsSet);
        ImbaseServer.AppendRows(dataTable, source, 5);
        length1 -= val2;
        sourceIndex += val2;
      }
    }
    ImbaseServer.BuildUpTree_FilterData(dt, dataTable, (IEnumerable<string>) stringSet, (IEnumerable<string>) new List<string>((IEnumerable<string>) strArray));
  }

  internal static void BuildUpTree(DataTable tableSource, DataTable tableCatalogInfo)
  {
    if (tableSource == null)
      return;
    if (tableCatalogInfo == null)
      throw new ArgumentNullException(nameof (tableCatalogInfo));
    Dictionary<string, bool> folders = new Dictionary<string, bool>();
    HashSet<string> stringSet = new HashSet<string>();
    int columnIndex = tableSource.Columns.IndexOf("F_PATH");
    foreach (DataRow row in (InternalDataCollectionBase) tableSource.Rows)
      ImbaseServer.AppendPath(Convert.ToString(row[columnIndex]), (ICollection<string>) stringSet, (IDictionary<string, bool>) folders);
    foreach (DataRow row in (InternalDataCollectionBase) tableSource.Rows)
    {
      string key = Convert.ToString(row[columnIndex]);
      folders.Remove(key);
      stringSet.Remove(key);
    }
    foreach (string key in stringSet)
      folders.Add(key, false);
    if (folders.Count == 0)
      return;
    DataTable dataTable = tableSource.Clone();
    int idxColPath = tableCatalogInfo.Columns.IndexOf("F_PATH");
    EnumerableRowCollection<DataRow> enumerableRowCollection = tableCatalogInfo.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => folders.ContainsKey(Convert.ToString(row[idxColPath]))));
    if (enumerableRowCollection.Any<DataRow>())
      ImbaseServer.AppendRows(dataTable, (IEnumerable) enumerableRowCollection, 5, false);
    IEnumerable<string> foldersPath = folders.Select<KeyValuePair<string, bool>, string>((System.Func<KeyValuePair<string, bool>, string>) (item => item.Key));
    ImbaseServer.BuildUpTree_FilterData(tableSource, dataTable, (IEnumerable<string>) stringSet, foldersPath);
  }

  internal static void BuildUpTree_FilterData(
    DataTable tableSource,
    DataTable tableUpTreeData,
    IEnumerable<string> catalogsPath,
    IEnumerable<string> foldersPath)
  {
    int columnIndex = tableUpTreeData != null ? tableUpTreeData.Columns.IndexOf("F_PATH") : throw new ArgumentNullException("tableData");
    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
    foreach (string key in foldersPath)
      dictionary[key] = false;
    foreach (string key in catalogsPath)
      dictionary[key] = false;
    foreach (DataRow row in (InternalDataCollectionBase) tableUpTreeData.Rows)
      dictionary.Remove(row[columnIndex].ToString());
    if (dictionary.Count > 0)
    {
      Dictionary<string, bool> classifFixedList = new Dictionary<string, bool>();
      for (int index = tableSource.Rows.Count - 1; index >= 0; --index)
      {
        DataRow row = tableSource.Rows[index];
        if (row != null)
        {
          string str = row[columnIndex].ToString();
          int length = str.Length - 2;
          if (length > 0)
          {
            List<string> stringList = new List<string>(str.Length / 2);
            bool flag = false;
            for (; length > 0; length -= 2)
            {
              string key = str.Substring(0, length);
              if (dictionary.ContainsKey(key))
              {
                flag = true;
                break;
              }
              stringList.Add(key);
            }
            if (!flag)
              stringList.ForEach((Action<string>) (item => classifFixedList[item] = false));
            else
              row.Delete();
          }
        }
      }
      if (classifFixedList.Count == 0)
      {
        tableUpTreeData.Rows.Clear();
      }
      else
      {
        for (int index = tableUpTreeData.Rows.Count - 1; index >= 0; --index)
        {
          DataRow row = tableUpTreeData.Rows[index];
          if (row != null)
          {
            string key = row[columnIndex].ToString();
            if (!classifFixedList.ContainsKey(key))
              row.Delete();
          }
        }
      }
    }
    tableSource.AcceptChanges();
    tableUpTreeData.AcceptChanges();
    ImbaseServer.AppendRows(tableSource, tableUpTreeData, 5);
  }

  internal static bool LoadAllCatalogsInfo(
    IUserSession session,
    DBRecordSetParams paramsSet,
    DataTable tableSource,
    out DataTable tableCatalogInfo)
  {
    if (tableSource == null)
      throw new ArgumentNullException(nameof (tableSource));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    tableCatalogInfo = (DataTable) null;
    if (tableSource.Rows.Count == 0)
      return false;
    int columnIndex = tableSource.Columns.IndexOf("F_PATH");
    if (columnIndex == -1)
      columnIndex = tableSource.Columns.IndexOf(Intermech.Imbase.Consts.ClassifFolderKeyAttId.ToString());
    HashSet<string> source = new HashSet<string>();
    foreach (DataRow row in (InternalDataCollectionBase) tableSource.Rows)
    {
      if (row != null)
      {
        string str = Convert.ToString(row[columnIndex]);
        if (!string.IsNullOrEmpty(str))
          source.Add(str.Substring(0, 2));
      }
    }
    if (source.Count == 0)
      return false;
    IList list = (IList) source.ToList<string>();
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    for (int index = 0; index < list.Count - 1; ++index)
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, list[index], LogicalOperators.OR, index == 0 ? 1 : 0, true));
    conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, list[list.Count - 1], paramsSet.Conditions == null || paramsSet.Conditions.Length == 0 ? LogicalOperators.NONE : LogicalOperators.AND, list.Count > 1 ? -1 : 0, true));
    if (paramsSet.Conditions != null)
      conditionStructureList.AddRange((IEnumerable<ConditionStructure>) paramsSet.Conditions);
    paramsSet.Conditions = conditionStructureList.ToArray();
    tableCatalogInfo = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    if (tableCatalogInfo.Columns.Count > 0)
      ImbaseServer.RenameColumns(tableCatalogInfo);
    return true;
  }

  internal static void AppendRows(DataTable dest, DataTable source, int nFields)
  {
    ImbaseServer.AppendRows(dest, source, nFields, false);
  }

  internal static void AppendRows(
    DataTable dest,
    DataTable source,
    int nFields,
    bool updateDuplicates)
  {
    ImbaseServer.AppendRows(dest, (IEnumerable) source.Rows, nFields, updateDuplicates);
  }

  internal static void AppendRows(
    DataTable dest,
    IEnumerable sourceRows,
    int nFields,
    bool updateDuplicates)
  {
    if (sourceRows == null)
      return;
    bool flag = false;
    IDictionary<long, DataRow> dictionary = (IDictionary<long, DataRow>) new Dictionary<long, DataRow>();
    foreach (DataRow row in (InternalDataCollectionBase) dest.Rows)
    {
      if (row.RowState.HasFlag((Enum) DataRowState.Detached) || row.RowState.HasFlag((Enum) DataRowState.Deleted))
        flag = true;
      else
        dictionary[Convert.ToInt64(row[0])] = row;
    }
    object[] destinationArray = new object[nFields];
    if (sourceRows is ICollection collection && dest.MinimumCapacity - dest.Rows.Count < collection.Count)
      dest.MinimumCapacity = dest.Rows.Count + collection.Count;
    foreach (DataRow sourceRow in sourceRows)
    {
      if (!sourceRow.RowState.HasFlag((Enum) DataRowState.Detached) && !sourceRow.RowState.HasFlag((Enum) DataRowState.Deleted))
      {
        long int64 = Convert.ToInt64(sourceRow[0]);
        DataRow dataRow;
        if (dictionary.TryGetValue(int64, out dataRow))
        {
          if (updateDuplicates)
          {
            object[] itemArray = dataRow.ItemArray;
            Array.Copy((Array) sourceRow.ItemArray, 1, (Array) itemArray, 1, nFields - 1);
            dataRow.ItemArray = itemArray;
          }
        }
        else
        {
          object[] sourceArray = sourceRow.ItemArray;
          if (sourceArray.Length > nFields)
          {
            Array.Copy((Array) sourceArray, (Array) destinationArray, nFields);
            sourceArray = destinationArray;
          }
          dictionary[int64] = dest.Rows.Add(sourceArray);
        }
      }
    }
    if (!flag)
      return;
    dest.AcceptChanges();
  }

  internal static IUserSession GetSession(Guid sessionGuid)
  {
    return (!sessionGuid.Equals(Guid.Empty) ? UserSession.GetSessionByID(sessionGuid) : throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_19"), "SessionGuid")) ?? throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_20"), (object) sessionGuid.ToString()), "SessionGuid");
  }

  public ImbaseServer() => ImbaseServer._instance = this;

  public long CreateObject(
    Guid sessionGuid,
    long catalogId,
    long baseId,
    long recordId,
    bool commitCreation,
    int needType)
  {
    return this.CreateObject(sessionGuid, catalogId, baseId, recordId, commitCreation, needType, (List<string>) null);
  }

  private long CreateObject(
    Guid sessionGuid,
    long catalogId,
    long baseId,
    long recordId,
    bool commitCreation,
    int needType,
    List<string> receptureComposition)
  {
    Tuple<long, long> tuple1 = new Tuple<long, long>(baseId, recordId);
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    Stack<Tuple<long, long>> tupleStack = ImbaseServer.ObjectCreateStacks.AddOrUpdate(managedThreadId, new Stack<Tuple<long, long>>(), (Func<int, Stack<Tuple<long, long>>, Stack<Tuple<long, long>>>) ((key, oldValue) => oldValue));
    if (tupleStack.Contains(tuple1))
      return 0;
    try
    {
      tupleStack.Push(tuple1);
      ImbaseServer.ObjectCreateMode objectCreateMode = ImbaseServer.ObjectCreateMode.Unknown;
      long[] numArray = (long[]) null;
      if (baseId <= 0L)
        throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_1"), nameof (baseId));
      IUserSession session = ImbaseServer.GetSession(sessionGuid);
      long num1 = -1;
      long num2 = -1;
      IDBObject dbObject1 = (IDBObject) null;
      int imbaseObjectRefAttId = Intermech.Imbase.Consts.ImbaseObjectRefAttID;
      QuickObjectInfo objectInfo = session.GetObjectInfo(baseId);
      int num3 = !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_2"), (object) baseId));
      ArrayList path = new ArrayList();
      bool createNew = false;
      int num4 = needType;
      int newObjectType = this.GetNewObjectType(session, baseId, path, ref createNew);
      if (num4 == -1)
        num4 = newObjectType;
      if (num4 == -1)
        num4 = Intermech.Imbase.Consts.ImbaseItemTypeID;
      bool flag1 = false;
      if (this.DerviedFrom(num3, Intermech.Imbase.Consts.ImbaseTableRefTypeID, session) || this.DerviedFrom(num3, Intermech.Imbase.Consts.ImbaseTableTypeID, session))
      {
        if (recordId == -1L)
        {
          IDBObjectType objectType = session.GetObjectType(num3);
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_3"), (object) objectType.ObjectTypeName));
        }
        objectCreateMode = ImbaseServer.ObjectCreateMode.FromTable;
        IDBObject dbObject2 = session.GetObject(baseId);
        if (dbObject2.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          IDBAttribute attributeById = dbObject2.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          if (attributeById == null || attributeById.Values[0] == null)
          {
            IDBObjectType objectType = session.GetObjectType(num3);
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_4"), (object) objectType.ObjectTypeName));
          }
          num2 = Convert.ToInt64(attributeById.Values[0]);
          session.GetObject(num2);
        }
        else
        {
          num2 = baseId;
          recordId = -1L;
        }
        if (!createNew)
          num1 = this.FindCopy(session, num4, baseId, imbaseObjectRefAttId, recordId, Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
        if (num1 == -1L)
        {
          numArray = session.GetObjectCollection(num4).CreateEx();
          dbObject1 = session.GetObject(numArray[0]);
          num1 = dbObject1.ObjectID;
          this.FillObjectAttributesInternal(session, dbObject1, baseId, num2, recordId, createNew, (List<int>) null);
        }
        else
          flag1 = true;
        if (num1 != -1L)
        {
          IRecepturesService service = ServiceUtils.GetService<IRecepturesService>((object) session, true);
          ReceptureItemInfo recordInfo = new ReceptureItemInfo(baseId, recordId);
          if (service.RecordHasRecepture(recordInfo))
          {
            IDBObject dbObject3 = session.GetObject(num1);
            List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(dbObject3.ObjectType);
            foreach (Tuple<ReceptureItemInfo, MeasuredValue> tuple2 in service.GetReceptureComposition(session, recordInfo))
            {
              if (receptureComposition != null)
              {
                if (receptureComposition.Contains(tuple2.Item1.ImbaseKey))
                  continue;
              }
              else
                receptureComposition = new List<string>();
              receptureComposition.Add(tuple2.Item1.ImbaseKey);
              long num5 = this.CreateObject(sessionGuid, catalogId, tuple2.Item1.LinkId, tuple2.Item1.RecordId, commitCreation, -1, receptureComposition);
              if (num5 != 0L)
              {
                QuickObjectInfo childObj = session.GetObjectInfo(num5);
                IMSApplicability imsApplicability = typeApplicabilities.FirstOrDefault<IMSApplicability>((System.Func<IMSApplicability, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(childObj.ObjectTypeID, x.ChildObjectTypeID) && x.Options.HasFlag((Enum) ApplicabilityOptions.DefaultRelation)));
                if (imsApplicability == null)
                  throw new Exception(string.Format(LocalizationHolder.rm.GetString("CantCreateRecepture"), (object) dbObject3.NameInMessages, (object) MetaDataHelper.GetObjectTypeName(dbObject3.ObjectType), (object) MetaDataHelper.GetObjectTypeName(childObj.ObjectTypeID)));
                if (session.GetRelation(num1, num5, imsApplicability.RelationTypeID, true) == null)
                  session.GetRelationCollection(imsApplicability.RelationTypeID).Create(num1, num5, new AttributeValues[1]
                  {
                    new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), (object) tuple2.Item2)
                  });
              }
            }
          }
        }
      }
      else if (ImbaseHelper.IsFolder(num3) || ImbaseHelper.IsCatalogRecord(num3))
      {
        if (!createNew)
          num1 = this.FindCopy(session, num4, baseId, imbaseObjectRefAttId, -1L, -1);
        objectCreateMode = ImbaseServer.ObjectCreateMode.FromFolder;
        if (num1 == -1L)
        {
          dbObject1 = session.GetObjectCollection(num4).Create();
          num1 = dbObject1.ObjectID;
          this.FillObjectAttributesInternal(session, dbObject1, baseId, -1L, -1L, createNew, (List<int>) null);
        }
        else
          flag1 = true;
      }
      if (dbObject1 != null && (commitCreation || !createNew))
      {
        long sameUniqeObject = ImbaseServer.FindSameUniqeObject(session, dbObject1, num4);
        if (sameUniqeObject == 0L)
        {
          dbObject1.CommitCreation(true);
          if (numArray != null && numArray.Length > 1)
          {
            for (int index = 1; index < numArray.Length; ++index)
              session.GetObject(numArray[index], false)?.CommitCreation(true);
          }
        }
        else
        {
          dbObject1.Delete(0L);
          if (numArray != null && numArray.Length > 1)
          {
            for (int index = 1; index < numArray.Length; ++index)
              session.GetObject(numArray[index], false)?.Delete(0L);
          }
          dbObject1 = session.GetObject(sameUniqeObject);
          if (dbObject1.ObjectModifyMode != ObjectModifyModes.CantModify)
          {
            bool flag2 = false;
            try
            {
              if (dbObject1.CheckoutBy == 0L && dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
              {
                dbObject1 = dbObject1.CheckOut();
                flag2 = true;
              }
              switch (objectCreateMode)
              {
                case ImbaseServer.ObjectCreateMode.FromFolder:
                  this.AssignAttributesFromObjectID(session, dbObject1, baseId, imbaseObjectRefAttId, false, true, (List<int>) null);
                  break;
                case ImbaseServer.ObjectCreateMode.FromTable:
                  this.FillObjectAttributesInternal(session, dbObject1, baseId, num2, recordId, createNew, (List<int>) null);
                  break;
              }
              if (flag2)
                dbObject1.CheckIn();
            }
            catch (AccessDeniedException ex)
            {
            }
          }
        }
        num1 = dbObject1.ObjectID;
      }
      else if (flag1)
      {
        IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService<IDBTimedEvents>().GetSystemSessionTemporaryClone("imbase.getlocalsession");
        try
        {
          IDBObject destObj = sessionTemporaryClone.GetObject(num1);
          if (destObj != null)
            this.FillObjectAttributesInternal(sessionTemporaryClone, destObj, baseId, num2, recordId, createNew, (List<int>) null);
        }
        finally
        {
          sessionTemporaryClone.Logout("imbase.getlocalsession");
        }
      }
      return num1;
    }
    finally
    {
      tupleStack.Pop();
      if (tupleStack.Count == 0)
        ImbaseServer.ObjectCreateStacks.TryRemove(managedThreadId, out Stack<Tuple<long, long>> _);
    }
  }

  public List<IMSAttribute4ObjectType> GetAttributesForObjectType(
    IUserSession session,
    int objTypeID)
  {
    List<IMSAttribute4ObjectType> source = (List<IMSAttribute4ObjectType>) null;
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList1 = MetaDataHelper.GetAttribute4ObjectTypeList(objTypeID);
    if (attribute4ObjectTypeList1 != null && attribute4ObjectTypeList1.Count > 0)
    {
      source = new List<IMSAttribute4ObjectType>(attribute4ObjectTypeList1.Count);
      int modifyContentDateId = session.IdentHelper.ModifyContentDateID;
      foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList1)
      {
        int attributeId = attribute4ObjectType.AttributeID;
        if (attributeId >= 0 && attributeId != modifyContentDateId && attributeId != Intermech.Imbase.Consts.CreateNewObjectAttID && attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue && !ImbaseHelper.IsSystemAttribute(attributeId) && !ImbaseHelper.SkipAtttribute(attributeId))
          source.Add(attribute4ObjectType);
      }
      IMSAttribute4ObjectType attribute4ObjectType1 = source.FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD));
      if (attribute4ObjectType1 != null && source.FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID)) == null)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList2 = source;
        IMSAttribute4ObjectType attribute4ObjectType2 = new IMSAttribute4ObjectType();
        attribute4ObjectType2.AttributeID = Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID;
        attribute4ObjectType2.ObjectTypeID = objTypeID;
        attribute4ObjectType2.IsContent = attribute4ObjectType1.IsContent;
        attribute4ObjectTypeList2.Add(attribute4ObjectType2);
      }
    }
    return source != null && source.Count != 0 ? source : throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Synch_ObjType_Attrs_Empty"));
  }

  internal bool CheckUpdateAttributes(
    IUserSession session,
    IDBObject newObject,
    long baseId,
    long tableId,
    long recordId,
    ref bool inPlace,
    ref bool error,
    out List<int> objectAttIds)
  {
    if (this.contentModifyDateAttId == -1)
      this.contentModifyDateAttId = session.GetAttributeType(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545")).AttributeID;
    bool flag1 = false;
    bool flag2 = true;
    inPlace = false;
    error = false;
    List<int> intList = new List<int>();
    AttributeValues[] attributesValues1 = newObject.GetAttributesValues(GetAttributeValuesModes.CheckWriteAccess);
    IDBObjectType objectType = session.GetObjectType(newObject.ObjectType);
    IDBAttribute4TypeCollection attributes1 = objectType.Attributes;
    IDBAttribute4TypeCollection attributes2 = objectType.Attributes;
    objectAttIds = new List<int>();
    int result1;
    foreach (DataRow row in (InternalDataCollectionBase) attributes2.Select(string.Empty, (object[]) null).Rows)
    {
      result1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      IDBAttributeType4 attributeById = attributes2.GetAttributeByID(result1);
      if (!this.IsInternal(result1) && result1 != this.contentModifyDateAttId && attributeById.Computed == ComputeValueModes.NotComputableValue)
        objectAttIds.Add(result1);
    }
    DataTable dataTable = (DataTable) null;
    if (recordId != -1L && baseId != 0L)
    {
      DataRow recordRow = ImbaseServer.GetRecordRow(session, baseId, recordId, false);
      if (recordRow == null)
      {
        inPlace = false;
        error = true;
        return false;
      }
      dataTable = recordRow.Table;
      foreach (DataColumn column in (InternalDataCollectionBase) dataTable.Columns)
      {
        if (int.TryParse(column.Caption, out result1))
        {
          object y = recordRow[column];
          string str = Convert.ToString(y).Trim();
          if (string.IsNullOrEmpty(str))
          {
            objectAttIds.Remove(result1);
          }
          else
          {
            AttributeValues attValue = this.FindAttValue(attributesValues1, result1);
            bool flag3 = false;
            if (attValue != null && attValue.ComputeMode == ComputeValueModes.NotComputableValue)
            {
              object baseMeasure = attValue.Values[0];
              if (attValue.AttributeType == FieldTypes.ftMeasured && baseMeasure != null)
              {
                if (baseMeasure is MeasuredValue measuredValue)
                  baseMeasure = (object) MeasureHelper.ConvertToBaseMeasure(measuredValue);
                long measureID = Intermech.Imbase.Consts.mmUnitID;
                if (column.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
                  measureID = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
                y = (object) MeasureHelper.ConvertToBaseMeasure(new MeasuredValue(Convert.ToDouble(y), measureID));
                if (baseMeasure is MeasuredValue val1 && y is MeasuredValue val2 && MeasureHelper.Compare(val1, val2) == CompareResult.Equal)
                  flag3 = true;
              }
              else if (attValue.AttributeType == FieldTypes.ftObjectLink)
              {
                QuickObjectInfo quickObjectInfo = new QuickObjectInfo()
                {
                  ObjectTypeID = -1
                };
                if (GuidHelper.IsGuid(str))
                {
                  quickObjectInfo = session.GetObjectInfo(new Guid(str));
                }
                else
                {
                  long result2 = 0;
                  if (long.TryParse(str, out result2))
                    quickObjectInfo = session.GetObjectInfo(result2);
                }
                if (!quickObjectInfo.Empty)
                  y = (object) quickObjectInfo.ObjectID;
              }
              if (flag3 || AttributeValues.ValueEquals(baseMeasure, y) || Convert.ToString(baseMeasure).Equals(Convert.ToString(y)))
              {
                objectAttIds.Remove(result1);
              }
              else
              {
                flag1 = true;
                if (attValue.ReadOnly)
                  flag2 = false;
                intList.Add(result1);
              }
            }
          }
        }
      }
      AttributeValues attValue1 = this.FindAttValue(attributesValues1, Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
      if (attValue1 != null)
      {
        if (attValue1.ComputeMode == ComputeValueModes.NotComputableValue)
        {
          if (!AttributeValues.ValueEquals(attValue1.Values[0], (object) recordId))
          {
            flag1 = true;
            if (attValue1.ReadOnly)
              flag2 = false;
          }
          else
            objectAttIds.Remove(attValue1.AttributeID);
        }
      }
      else
      {
        IDBAttributeType4 attributeById = attributes2.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
        if (attributeById != null)
        {
          flag1 = true;
          if ((attributeById.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
            flag2 = false;
        }
      }
    }
    if (baseId != 0L)
    {
      AttributeValues[] attributesValues2 = session.GetObject(baseId).GetAttributesValues(GetAttributeValuesModes.None);
      foreach (int num in new List<int>((IEnumerable<int>) objectAttIds))
      {
        if (!intList.Contains(num))
        {
          AttributeValues attValue2 = this.FindAttValue(attributesValues2, num);
          AttributeValues attValue3 = this.FindAttValue(attributesValues1, num);
          if (attValue2 != null && attValue2.Values[0] != null && attValue2.Values[0] != DBNull.Value)
          {
            if (attValue3 != null)
            {
              if (!AttributeValues.ValueEquals(attValue2.Values[0], attValue3.Values[0]))
              {
                flag1 = true;
                if (attValue3.ReadOnly)
                  flag2 = false;
              }
              else
                objectAttIds.Remove(num);
            }
            else
            {
              IDBAttributeType4 attributeById = attributes2.GetAttributeByID(num);
              if (attributeById.Required != RequiredModes.Manual)
              {
                flag1 = true;
                if (attributeById != null && (attributeById.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
                  flag2 = false;
              }
            }
          }
          else if (attValue2 != null || attValue3 == null)
          {
            string columnName = num.ToString();
            if (dataTable == null || dataTable.Columns.IndexOf(columnName) == -1)
            {
              objectAttIds.Remove(num);
            }
            else
            {
              IDBAttributeType4 attributeById = attributes2.GetAttributeByID(num);
              flag1 = true;
              if (attributeById != null && (attributeById.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
                flag2 = false;
            }
          }
        }
      }
    }
    AttributeValues attValue4 = this.FindAttValue(attributesValues1, Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    if (attValue4 != null && AttributeValues.ValueEquals(attValue4.Values[0], (object) baseId))
      objectAttIds.Remove(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    else if (attValue4 == null || attValue4.ComputeMode == ComputeValueModes.NotComputableValue && !AttributeValues.ValueEquals(attValue4.Values[0], (object) baseId))
    {
      flag1 = true;
      if (attValue4 != null)
      {
        if (attValue4.ReadOnly)
          flag2 = false;
      }
      else
      {
        IDBAttributeType4 attributeById = attributes2.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
        flag1 = true;
        if ((attributeById.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
          flag2 = false;
      }
    }
    inPlace = flag2;
    return flag1;
  }

  public DataTable GetCreatedObjects(Guid sessionGuid, long objectId)
  {
    return this.GetCreatedObjects(ImbaseServer.GetSession(sessionGuid), objectId);
  }

  public DataTable GetCreatedObjects(IUserSession isession, long objectId)
  {
    objectId = Math.Abs(objectId);
    QuickObjectInfo objectInfo = isession.GetObjectInfo(objectId);
    if (objectInfo.Empty)
      return (DataTable) null;
    ArrayList path = new ArrayList();
    bool createNew = false;
    this.GetNewObjectType(isession, objectId, path, ref createNew);
    if (createNew)
      return (DataTable) null;
    if (!(isession is UserSession userSession))
      return (DataTable) null;
    IDbManager dataManager = userSession.DataManager;
    if (dataManager == null)
      return (DataTable) null;
    long num;
    if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      string commandText = $"SELECT F_TOOBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = {Intermech.Imbase.Consts.ImbaseTableRefAttID} AND F_OBJECT_ID = {objectId}";
      object obj = dataManager.ExecuteScalar(commandText);
      if (obj == null || DBNull.Value.Equals(obj))
        return (DataTable) null;
      num = Convert.ToInt64(obj);
    }
    else
    {
      if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableTypeID)
        return (DataTable) null;
      num = objectId;
    }
    if (num == -1L)
      return (DataTable) null;
    string commandText1 = $"SELECT F_OBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = {Intermech.Imbase.Consts.ImbaseTableRefAttID} AND F_TOOBJECT_ID = {num.ToString()}";
    DataTable dataTable1 = dataManager.ExecuteDataTable(commandText1);
    DataRowCollection rows1 = dataTable1.Rows;
    string commandText2;
    switch (rows1.Count)
    {
      case 0:
        return (DataTable) null;
      case 1:
        commandText2 = $"SELECT F_OBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = {Intermech.Imbase.Consts.ImbaseObjectRefAttID} AND F_TOOBJECT_ID = {dataTable1.Rows[0][0].ToString()}";
        break;
      default:
        commandText2 = $"SELECT F_OBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = {Intermech.Imbase.Consts.ImbaseObjectRefAttID} AND (";
        int count1 = rows1.Count;
        for (int index = 0; index < count1; ++index)
        {
          string str = $"{commandText2} F_TOOBJECT_ID={rows1[index][0].ToString()}";
          commandText2 = index != count1 - 1 ? str + " OR " : str + ")";
        }
        break;
    }
    DataTable dataTable2 = dataManager.ExecuteDataTable(commandText2);
    List<long> objIdList = new List<long>();
    DataRowCollection rows2 = dataTable2.Rows;
    int count2 = rows2.Count;
    if (count2 == 0)
      return (DataTable) null;
    for (int index = 0; index < count2; ++index)
      objIdList.Add(Convert.ToInt64(rows2[index][0]));
    DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 2),
      new ColumnDescriptor((object) Convert.ToInt32(Intermech.Imbase.Consts.ImbaseObjectRefAttID), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    })
    {
      TableName = "tb"
    };
    DataTable objectData = DataHelper.GetObjectData(-1, (IUserSession) userSession, dbRsp, (IEnumerable<long>) objIdList);
    objectData.RemotingFormat = SerializationFormat.Binary;
    return objectData;
  }

  private AttributeValues FindAttValue(AttributeValues[] values, int attId)
  {
    if (values == null)
      return (AttributeValues) null;
    int length = values.Length;
    for (int index = 0; index < length; ++index)
    {
      AttributeValues attValue = values[index];
      if (attValue.AttributeID == attId)
        return attValue;
    }
    return (AttributeValues) null;
  }

  private static long FindSameUniqeObject(
    IUserSession session,
    IDBObject newObject,
    int newObjectType)
  {
    long sameUniqeObject = 0;
    DataTable dataTable1 = session.GetObjectType(newObjectType).Attributes.Select(string.Empty);
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      UniqueValueModes int32_1 = (UniqueValueModes) Convert.ToInt32(dataTable1.Rows[index1]["F_UNIQUE"]);
      if (int32_1 != UniqueValueModes.NotUnique)
      {
        int int32_2 = Convert.ToInt32(dataTable1.Rows[index1]["F_ATTRIBUTE_ID"]);
        IDBAttribute attributeById1 = newObject.GetAttributeByID(int32_2);
        if (attributeById1 != null)
        {
          bool flag = true;
          for (int index2 = 0; index2 < attributeById1.ValuesCount; ++index2)
          {
            attributeById1.Index = index2;
            if (attributeById1.Value != null && attributeById1.Value != DBNull.Value && attributeById1.Value is string && ((string) attributeById1.Value).Trim() != string.Empty)
            {
              flag = false;
              break;
            }
          }
          if (!flag)
          {
            int num = -1;
            if (int32_1 != UniqueValueModes.AllVerTypes)
            {
              num = newObjectType;
              InheritModes inheritModes = (InheritModes) Convert.ToInt32(dataTable1.Rows[index1]["F_PUBLIC"]);
              while (inheritModes == InheritModes.Inherited)
              {
                int objectTypeParentId = (session as UserSession).DBCache.GetObjectTypeParentID(num);
                if (objectTypeParentId != -1)
                {
                  if (session.GetObjectType(objectTypeParentId).Attributes.GetAttributeByID(int32_2, false) is IDBAttributeType4Object attributeById2)
                  {
                    num = objectTypeParentId;
                    inheritModes = attributeById2.InheritMode;
                  }
                  else
                    break;
                }
              }
            }
            DBRecordSetParams paramSet = new DBRecordSetParams(1)
            {
              Columns = new object[1]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID
              }
            };
            List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
            if (int32_1 == UniqueValueModes.TypeOnly)
            {
              conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) Math.Abs(newObject.ObjectID), LogicalOperators.AND, 0, true));
              conditionStructureList.Add(new ConditionStructure(-3, RelationalOperators.NotEqual, (object) newObject.ID, LogicalOperators.AND, 0, true));
            }
            else
            {
              if (newObject.ObjectID < 0L)
                conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) newObject.ObjectID, LogicalOperators.AND, 0, true));
              conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) Math.Abs(newObject.ObjectID), LogicalOperators.AND, 0, true));
            }
            for (int index3 = 0; index3 < attributeById1.ValuesCount; ++index3)
            {
              if (attributeById1.Values[index3] != DBNull.Value)
                conditionStructureList.Add(new ConditionStructure(int32_2, RelationalOperators.Equal, attributeById1.Values[index3], LogicalOperators.OR, 0, true));
            }
            paramSet.Conditions = conditionStructureList.ToArray();
            DataTable dataTable2 = (session.GetObjectCollection(num) as DBObjectCollection).Select(paramSet, false);
            if (dataTable2.Rows.Count > 0)
            {
              sameUniqeObject = Convert.ToInt64(dataTable2.Rows[0][0]);
              break;
            }
          }
        }
      }
    }
    return sameUniqeObject;
  }

  public void LoadRecords(
    Guid sessionGuid,
    long objectId,
    string filter,
    string decimalSeparator,
    out DataTable recordsTable,
    out AttributeTypeProperties[] columnsAttributes,
    out ImbaseKeyInfo keyInfo)
  {
    CultureInfo cultureInfo1 = (CultureInfo) null;
    try
    {
      if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator != decimalSeparator)
      {
        cultureInfo1 = Thread.CurrentThread.CurrentCulture;
        CultureInfo cultureInfo2 = cultureInfo1.Clone() as CultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo2;
        NumberFormatInfo numberFormatInfo = cultureInfo2.NumberFormat.Clone() as NumberFormatInfo;
        numberFormatInfo.NumberDecimalSeparator = decimalSeparator;
        cultureInfo2.NumberFormat = numberFormatInfo;
      }
      IUserSession session = ImbaseServer.GetSession(sessionGuid);
      keyInfo = new ImbaseKeyInfo(-1L);
      TableLoader.LoadTable(session, objectId, filter, out recordsTable, out columnsAttributes, ref keyInfo);
      int length = columnsAttributes.Length;
      for (int index = 0; index < length; ++index)
      {
        string name = columnsAttributes[index].AttributeID.ToString();
        if (recordsTable.Columns.Contains(name))
          recordsTable.Columns[name]?.SetOrdinal(index + 2);
      }
      this.CheckViewAccess(session, keyInfo.TableId, recordsTable, ref columnsAttributes);
      ImbaseServer.FillApplicability(session, keyInfo.TableId, recordsTable);
      this.FillUserFilter(session, objectId, recordsTable);
      List<DataColumn> dataColumnList = new List<DataColumn>();
      foreach (DataColumn column in (InternalDataCollectionBase) recordsTable.Columns)
        dataColumnList.Add(column);
      dataColumnList.RemoveAt(0);
      dataColumnList.RemoveAt(0);
      int count1 = dataColumnList.Count;
      if (count1 <= 0 || recordsTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) recordsTable.Rows)
      {
        int count2 = dataColumnList.Count;
        if (count2 != 0)
        {
          for (int index = 0; index < count2; ++index)
          {
            DataColumn column = dataColumnList[index];
            object obj = row[column];
            if (obj != null && !Convert.IsDBNull(obj) && !string.IsNullOrWhiteSpace(obj.ToString()))
            {
              dataColumnList.Remove(column);
              --count2;
              --index;
            }
          }
        }
        else
          break;
      }
      if (dataColumnList.Count == count1)
        return;
      foreach (DataColumn dataColumn in dataColumnList)
        dataColumn.ExtendedProperties.Add((object) "F_VISIBLE", (object) false);
    }
    finally
    {
      if (cultureInfo1 != null)
        Thread.CurrentThread.CurrentCulture = cultureInfo1;
    }
  }

  private void CheckViewAccess(
    IUserSession session,
    long tableId,
    DataTable recordsTable,
    ref AttributeTypeProperties[] columnsAttributes)
  {
    DBObject table = session.GetObject(tableId) as DBObject;
    List<AttributeTypeProperties> attributeTypePropertiesList = new List<AttributeTypeProperties>();
    ImbaseAttSecurity imbaseAttSecurity = new ImbaseAttSecurity(session as UserSession, table, 0);
    if (imbaseAttSecurity.LoadCache(ActionType.View) > 0)
    {
      foreach (AttributeTypeProperties attributeTypeProperties in columnsAttributes)
      {
        imbaseAttSecurity.SetAttId(attributeTypeProperties.AttributeID);
        if (!imbaseAttSecurity.CheckAccess(ActionType.View, true, false))
        {
          int index = recordsTable.Columns.IndexOf(attributeTypeProperties.AttributeID.ToString());
          if (index != -1)
            recordsTable.Columns.RemoveAt(index);
        }
        else
          attributeTypePropertiesList.Add(attributeTypeProperties);
      }
      columnsAttributes = attributeTypePropertiesList.ToArray();
    }
    if (!ApplicationServices.Container.GetService<IImbaseParamsService>().CommonParams.AnalizeHiddenRecords)
      return;
    ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(session as UserSession, table, 0L);
    if (imbaseRecordSecurity.LoadCache(ActionType.View) <= 0)
      return;
    List<DataRow> dataRowList = new List<DataRow>();
    foreach (DataRow row in (InternalDataCollectionBase) recordsTable.Rows)
    {
      long int64 = Convert.ToInt64(row[1]);
      imbaseRecordSecurity.SetRecordId(int64);
      if (!imbaseRecordSecurity.CheckAccess(ActionType.View, true, false))
        dataRowList.Add(row);
    }
    if (dataRowList.Count <= 0)
      return;
    foreach (DataRow dataRow in dataRowList)
      dataRow.Delete();
    recordsTable.AcceptChanges();
  }

  private static void FillApplicability(IUserSession session, long tableId, DataTable recordsData)
  {
    try
    {
      recordsData.BeginLoadData();
      DataColumn column = new DataColumn("F_APPLICABILITY", typeof (bool))
      {
        Caption = string.Empty,
        DefaultValue = (object) true
      };
      recordsData.Columns.Add(column);
      int columnIndex1 = recordsData.Columns.IndexOf(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseUsingAttGUID).ToString());
      int columnIndex2 = recordsData.Columns.IndexOf(-2.ToString());
      DBObject table = session.GetObject(tableId) as DBObject;
      ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(session as UserSession, table, 0L);
      int num = imbaseRecordSecurity.LoadCache(ActionType.Use);
      foreach (DataRow row in (InternalDataCollectionBase) recordsData.Rows)
      {
        if ((columnIndex1 == -1 || string.IsNullOrEmpty(Convert.ToString(row[columnIndex1])) ? 1 : (!(Convert.ToString(row[columnIndex1]) == "-") ? 1 : 0)) != 0)
        {
          if (num != 0)
          {
            long int64 = Convert.ToInt64(row[columnIndex2]);
            imbaseRecordSecurity.SetRecordId(int64);
            row[column] = (object) imbaseRecordSecurity.CheckAccess(ActionType.Use, true, false);
          }
        }
        else
          row[column] = (object) false;
      }
    }
    finally
    {
      recordsData.AcceptChanges();
      recordsData.EndLoadData();
    }
  }

  private void FillUserFilter(IUserSession session, long tableLinkId, DataTable recordsData)
  {
    ICustomUsersTableFilterService service = ServiceUtils.GetService<ICustomUsersTableFilterService>((object) ServerServices.ServiceContainer, true);
    if (service == null)
      return;
    try
    {
      recordsData.BeginLoadData();
      Guid versionGuid = session.GetObjectInfo(tableLinkId).VersionGuid;
      DataColumn column = new DataColumn("F_USERFILTER", typeof (bool))
      {
        Caption = string.Empty,
        DefaultValue = (object) false
      };
      recordsData.Columns.Add(column);
      int columnIndex = recordsData.Columns.IndexOf(-12.ToString());
      UserFilter userFilter = service.GetUserFilter(session.SessionGUID, versionGuid);
      foreach (DataRow row in (InternalDataCollectionBase) recordsData.Rows)
      {
        string input = Convert.ToString(row[columnIndex]);
        Guid result;
        if (userFilter != null && Guid.TryParse(input, out result) && userFilter.RecordGuids.Contains(result))
          row[column] = (object) true;
      }
    }
    finally
    {
      recordsData.AcceptChanges();
      recordsData.EndLoadData();
    }
  }

  public string FillObjectAttributes(
    Guid sessionGuid,
    long destObjId,
    long baseId,
    long recordId,
    bool createNew)
  {
    string str = string.Empty;
    IUserSession session = (IUserSession) null;
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    bool flag = false;
    try
    {
      if (Guid.Empty.Equals(sessionGuid))
      {
        session = service.GetSystemSessionTemporaryClone("Imbase.FillAttributes");
        flag = true;
        sessionGuid = session.SessionGUID;
      }
      else
        session = ImbaseServer.GetSession(sessionGuid);
      if (destObjId == 0L)
        throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_5"), "destObjID");
      if (baseId <= 0L)
        throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_6"), nameof (baseId));
      if (session.GetObject(destObjId, false) == null)
        return str;
      QuickObjectInfo objectInfo = session.GetObjectInfo(baseId);
      int num = !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_7"), (object) baseId));
      if (this.DerviedFrom(num, Intermech.Imbase.Consts.ImbaseTableRefTypeID, session))
      {
        if (recordId == -1L)
        {
          IDBObjectType objectType = session.GetObjectType(num);
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_8"), (object) objectType.ObjectTypeName));
        }
        IDBObject dbObject = session.GetObject(baseId);
        long tableId;
        if (dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          if (attributeById == null || attributeById.Values[0] == null)
          {
            IDBObjectType objectType = session.GetObjectType(num);
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_9"), (object) objectType.ObjectTypeName));
          }
          tableId = Convert.ToInt64(attributeById.Values[0]);
        }
        else
        {
          tableId = recordId;
          recordId = -1L;
        }
        str = this.FillObjectAttributes(sessionGuid, destObjId, baseId, tableId, recordId, createNew);
      }
      else if (ImbaseHelper.IsFolder(num) || ImbaseHelper.IsCatalogRecord(num))
        this.FillObjectAttributes(sessionGuid, destObjId, baseId, -1L, -1L, createNew);
      return str;
    }
    finally
    {
      if (flag && session != null)
        session.Logout("Imbase.FillAttributes");
    }
  }

  public string FillObjectAttributes(
    Guid sessionGuid,
    long destObjId,
    long linkId,
    long tableId,
    long recordId,
    bool createNew)
  {
    if (destObjId == 0L)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_10"), "destObjID");
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBObject destObj = session.GetObject(destObjId, false);
    return destObj == null ? string.Empty : this.FillObjectAttributesInternal(session, destObj, linkId, tableId, recordId, createNew, (List<int>) null);
  }

  private string FillObjectAttributesInternal(
    IUserSession session,
    IDBObject destObj,
    long linkId,
    long tableId,
    long recordId,
    bool createNew,
    List<int> attIds)
  {
    string str = string.Empty;
    if (linkId <= 0L)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_11"), "baseId");
    int imbaseObjectRefAttId = Intermech.Imbase.Consts.ImbaseObjectRefAttID;
    QuickObjectInfo objectInfo = session.GetObjectInfo(linkId);
    int objectType = !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_12"), (object) linkId));
    this.AssignCopyAtt(session, destObj, createNew);
    if (recordId != -1L)
    {
      string message;
      if (ApplicationServices.Container.GetService<ISynchronizationObjService>().Synchronize(session, destObj.ObjectID, linkId, recordId, false, out message) == SynchObjectsStatus.NotSynchronized)
        str = message;
    }
    else if (ImbaseHelper.IsFolder(objectType) || ImbaseHelper.IsCatalogRecord(objectType))
    {
      this.AssignAttributesFromObjectID(session, destObj, linkId, imbaseObjectRefAttId, false, false, attIds);
      ImbaseNtdDocLink.CheckNtdObjects(session, linkId, -1L, destObj.ObjectID);
    }
    return str;
  }

  private void AssignCopyAtt(IUserSession session, IDBObject destObj, bool createNew)
  {
    IDBAttributeCollection attributes1 = destObj.Attributes;
    IDBAttribute4TypeCollection attributes2 = session.GetObjectType(destObj.ObjectType).Attributes;
    IDBAttribute byId = attributes1.FindByID(Intermech.Imbase.Consts.CreateNewObjectAttID);
    if (byId == null)
    {
      if (attributes2 != null && attributes2.GetAttributeByID(Intermech.Imbase.Consts.CreateNewObjectAttID) == null)
        return;
      attributes1.AddAttribute(Intermech.Imbase.Consts.CreateNewObjectAttID, true, new object[1]
      {
        (object) createNew
      });
    }
    else
    {
      if (byId.ReadOnly)
        return;
      byId.Value = (object) createNew;
    }
  }

  public void FillObjectLinkAttributes(
    Guid sessionGuid,
    long destObjID,
    int masterAttributeID,
    long imbaseObjID)
  {
    if (sessionGuid == Guid.Empty)
      return;
    if (destObjID == 0L)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_10"), nameof (destObjID));
    if (masterAttributeID == 0)
      return;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBObject dbObject1 = session.GetObject(destObjID, false);
    if (dbObject1 == null)
      return;
    IDBObject dbObject2 = session.GetObject(imbaseObjID, false);
    if (dbObject2 == null)
      return;
    IDBObjectType objectType = session.GetObjectType(dbObject1.ObjectType, false);
    if (objectType == null)
      return;
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    foreach (DataRow row in (InternalDataCollectionBase) objectType.Attributes.Select("", (object[]) null).Rows)
    {
      int result1 = 0;
      int.TryParse(row["F_MASTER_ID"].ToString(), out result1);
      if (result1 == masterAttributeID)
      {
        int result2 = 0;
        int result3 = 0;
        int.TryParse(row["F_ATTRIBUTE_ID"].ToString(), out result2);
        int.TryParse(row["F_SOURCE_ID"].ToString(), out result3);
        if (result2 != 0 && result3 != 0)
          dictionary.Add(result2, result3);
      }
    }
    List<AttributeValues> attributeValuesList = new List<AttributeValues>()
    {
      new AttributeValues(masterAttributeID, (object) imbaseObjID)
    };
    foreach (KeyValuePair<int, int> keyValuePair in dictionary)
    {
      IDBAttribute attributeById = dbObject2.GetAttributeByID(keyValuePair.Value);
      if (attributeById != null)
        attributeValuesList.Add(new AttributeValues(keyValuePair.Key, attributeById.Value));
    }
    dbObject1.SetAttributesValues(attributeValuesList.ToArray());
  }

  public static DataRow GetRecordRow(
    IUserSession session,
    long linkId,
    long recordId,
    bool throwIfNotFound = true)
  {
    DataRow recordRow = (DataRow) null;
    try
    {
      ImbaseKeyInfo keyInfo = new ImbaseKeyInfo();
      DataTable recordsTable;
      TableLoader.LoadTable(session, linkId, string.Empty, out recordsTable, out AttributeTypeProperties[] _, ref keyInfo);
      DataRow[] dataRowArray = recordsTable.Select($"[-2]={recordId}");
      recordRow = dataRowArray.Length == 1 ? dataRowArray[0] : throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_13"), (object) linkId, (object) recordId));
    }
    catch (Exception ex)
    {
      if (throwIfNotFound)
        throw;
    }
    return recordRow;
  }

  private bool DerviedFrom(int objectType, int checkType, IUserSession session)
  {
    if (objectType == checkType)
      return true;
    int anObjectType = objectType;
    do
    {
      anObjectType = session.GetObjectType(anObjectType).ParentTypeID;
      if (anObjectType == checkType)
        return true;
    }
    while (anObjectType != -1);
    return false;
  }

  public DataTable GetTreePart(
    Guid sessionGuid,
    long[] catalogs,
    object conditions,
    ImbaseObjectTypes objectTypes)
  {
    ImbaseServer.GetSession(sessionGuid);
    return (DataTable) null;
  }

  public long GetObjectIdByOldImbaseKey(
    Guid sessionGuid,
    string oldImbaseKey,
    int objectType,
    bool createIfNotFound,
    out ScanOldKeyStatus status)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    long linkId;
    long recordId;
    ImbaseServer.GetNewRecordId(session, oldImbaseKey, out linkId, out recordId, out status);
    if (status != ScanOldKeyStatus.Success)
      return -1;
    bool createNew = false;
    int type = -1;
    long[] existingObjects = (long[]) null;
    this.GetObjectCreateInfo(session.SessionGUID, linkId, recordId, ref createNew, ref type, ref existingObjects);
    if (createIfNotFound)
      return this.CreateObject(session.SessionGUID, -1L, linkId, recordId, true, objectType);
    return existingObjects != null && existingObjects.Length != 0 ? existingObjects[0] : -1L;
  }

  public long ResolveObjectDef(
    Guid sessionGuid,
    string objectDef,
    string catalogDef,
    out List<long> resids,
    out DataTable tree)
  {
    resids = new List<long>();
    tree = (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    ICatalogInfo catalogInfo = (ICatalogInfo) new Intermech.Interfaces.Imbase.CatalogInfo();
    long num1 = this.ResolveCatalogDef(catalogDef, session, ref catalogInfo);
    int attId = Intermech.Imbase.Consts.ImbaseInternalTableNameAttID;
    int length = objectDef.IndexOf('=');
    if (length != -1)
    {
      string paramName = objectDef.Substring(0, length);
      string s = objectDef.Substring(length + 1, objectDef.Length - length - 1);
      objectDef = s;
      if (paramName.ToUpper() == "F_KEY")
      {
        long num2 = long.Parse(s);
        resids.Add(num2);
        return num2;
      }
      attId = ImbaseServer.FindAttributeId(paramName);
    }
    resids = this.GetTableLinks(objectDef, attId, session);
    if (resids == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_14"), (object) objectDef));
    if (resids.Count > 1)
    {
      tree = this.GetFoldersForObjects(sessionGuid, resids.ToArray(), new long[1]
      {
        num1
      });
      tree.RemotingFormat = SerializationFormat.Binary;
    }
    return resids[0];
  }

  public long GetFoldersForTable(
    Guid sessionGuid,
    long tablesID,
    string catalogDef,
    out List<long> resids,
    out DataTable tree)
  {
    resids = new List<long>();
    tree = (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    ICatalogInfo catalogInfo = (ICatalogInfo) new Intermech.Interfaces.Imbase.CatalogInfo();
    long num = this.ResolveCatalogDef(catalogDef, session, ref catalogInfo);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tablesID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    paramSet.Conditions[0].Content = ColumnContents.ID;
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(paramSet).Rows)
      CollectionUtils.AddSorted<long>(resids, Convert.ToInt64(row[0]));
    if (resids.Count > 1)
    {
      tree = this.GetFoldersForObjects(sessionGuid, resids.ToArray(), new long[1]
      {
        num
      });
      tree.RemotingFormat = SerializationFormat.Binary;
    }
    return resids[0];
  }

  public DataTable GetAllSubfolders(Guid sessionGuid, long parentId, int[] addTypes)
  {
    List<int> list = new List<int>()
    {
      Intermech.Imbase.Consts.ImbaseFolderTypeID
    };
    if (addTypes != null)
    {
      foreach (int addType in addTypes)
        CollectionUtils.AddSorted<int>(list, addType);
    }
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    IDBObject dbObject = session.GetObject(parentId, false);
    if (dbObject == null)
      return (DataTable) null;
    IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    string empty = string.Empty;
    if (attributeById != null && !attributeById.Value.Equals((object) DBNull.Value))
      empty = attributeById.Value.ToString();
    if (empty.Equals(string.Empty))
      return (DataTable) null;
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(new List<ConditionStructure>()
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) empty, LogicalOperators.AND, 0, true),
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false)
    }.ToArray());
    paramsSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    DataTable allSubfolders = ImbaseHelper.SelectObjects(session, paramsSet, list.ToArray());
    if (allSubfolders == null)
      return (DataTable) null;
    ImbaseServer.RenameColumns(allSubfolders);
    Dictionary<string, bool> folders = new Dictionary<string, bool>();
    HashSet<string> stringSet = new HashSet<string>();
    int columnIndex = allSubfolders.Columns.IndexOf("F_PATH");
    foreach (DataRow row in (InternalDataCollectionBase) allSubfolders.Rows)
      ImbaseServer.AppendPath(Convert.ToString(row[columnIndex]), (ICollection<string>) stringSet, (IDictionary<string, bool>) folders);
    stringSet.Clear();
    List<string> foldersPath = new List<string>((IEnumerable<string>) folders.Keys);
    for (int index = foldersPath.Count - 1; index >= 0; --index)
    {
      if (!foldersPath[index].StartsWith(empty))
        foldersPath.RemoveAt(index);
    }
    DataTable tableUpTreeData = allSubfolders.Copy();
    ImbaseServer.BuildUpTree_FilterData(allSubfolders, tableUpTreeData, (IEnumerable<string>) stringSet, (IEnumerable<string>) foldersPath);
    allSubfolders.AcceptChanges();
    allSubfolders.RemotingFormat = SerializationFormat.Binary;
    return allSubfolders;
  }

  public int[] GetAttributesForType(
    Guid sessionGuid,
    Guid rowsTypeGuid,
    out AttributeTypeProperties[] rowsProps,
    out Attribute4ObjectTypeProperties[] rowsProps4,
    out int rowsTypeID)
  {
    IDBObjectType objectType = ImbaseServer.GetSession(sessionGuid).GetObjectType(rowsTypeGuid);
    rowsTypeID = objectType.ObjectType;
    IDBAttribute4TypeCollection attributes = objectType.Attributes;
    DataTable dataTable = attributes.Select("");
    int count = dataTable.Rows.Count;
    int[] attributesForType = new int[count];
    int num = 0;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      attributesForType[num++] = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
    rowsProps = new AttributeTypeProperties[count];
    rowsProps4 = new Attribute4ObjectTypeProperties[count];
    for (int index = 0; index < count; ++index)
    {
      IDBAttributeType4Object attributeById = attributes.GetAttributeByID(attributesForType[index]) as IDBAttributeType4Object;
      rowsProps[index] = attributeById.PropertiesStructure;
      rowsProps4[index] = attributeById.Attribute4ObjectPropertiesStructure;
    }
    return attributesForType;
  }

  public DataTable GetSubfolders(Guid sessionGuid, long parentId, int[] addTypes)
  {
    return this.GetSubfolders(sessionGuid, new long[1]
    {
      parentId
    }, addTypes);
  }

  public DataTable GetSubfolders(Guid sessionGuid, long[] parentIds, int[] addTypes)
  {
    List<int> list1 = new List<int>()
    {
      Intermech.Imbase.Consts.ImbaseFolderTypeID
    };
    if (addTypes != null)
    {
      foreach (int addType in addTypes)
        CollectionUtils.AddSorted<int>(list1, addType);
    }
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), RelationalOperators.In, (object) list1.ToArray(), LogicalOperators.NONE, 0, false)
    }, AttributableElements.Relation);
    paramsSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) parentIds), session, (IEnumerable<int>) new int[1]
    {
      Intermech.Imbase.Consts.ImbaseDefaultLinkID
    }, false, paramsSet);
    if (childSostavData == null)
      return childSostavData;
    for (int index = childSostavData.Columns.Count - 1; index >= paramsSet.Columns.Length; --index)
      childSostavData.Columns.RemoveAt(index);
    DataColumn column = childSostavData.Columns.Add("F_EXP", typeof (bool));
    paramsSet.RecordCount = 1;
    if (childSostavData.Rows.Count > 0)
    {
      List<ObjInfoItem> projObjList = new List<ObjInfoItem>(childSostavData.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        if (Convert.ToInt32(row[1]) == Intermech.Imbase.Consts.ImbaseFolderTypeID)
          projObjList.Add(new ObjInfoItem(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])));
        else
          row[column] = (object) false;
      }
      if (projObjList.Count > 0)
      {
        int columnIndex = 4;
        DataTable tableCatalogInfo = (DataTable) null;
        if (75 < projObjList.Count)
        {
          paramsSet.Conditions = (ConditionStructure[]) null;
          paramsSet.RecordCount = -1;
          ImbaseServer.LoadAllCatalogsInfo(session, paramsSet, childSostavData, out tableCatalogInfo);
        }
        else
          tableCatalogInfo = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) new int[1]
          {
            Intermech.Imbase.Consts.ImbaseDefaultLinkID
          }, false, paramsSet);
        if (tableCatalogInfo != null)
        {
          List<string> list2 = new List<string>(tableCatalogInfo.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) tableCatalogInfo.Rows)
          {
            string str = row[columnIndex].ToString();
            if (string.IsNullOrEmpty(str))
              throw new Exception($"Ошибка в атрибуте \"Ключ папки классификатора\" объекта '{row[2]}'[{row[0]}]");
            list2.Add(str.Remove(str.Length - 2));
          }
          GenericListHelper.MakeUnique<string>(list2);
          foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
          {
            if (Convert.ToInt32(row[1]) == Intermech.Imbase.Consts.ImbaseFolderTypeID)
              row[column] = (object) (list2.BinarySearch(row[columnIndex].ToString()) >= 0);
          }
        }
      }
      ImbaseServer.RenameColumns(childSostavData);
    }
    childSostavData.RemotingFormat = SerializationFormat.Binary;
    return childSostavData;
  }

  public DataTable GetFoldersForObjects(Guid sessionGuid, long[] objectList, long[] catalogs)
  {
    HashSet<string> catalogFilter = new HashSet<string>();
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (objectList == null)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_16"), nameof (objectList));
    ImbaseServer.GetClassifKeys((ICollection<string>) catalogFilter, catalogs, session);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID), RelationalOperators.In, (object) objectList, LogicalOperators.NONE, 0, false)
    });
    paramsSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    DataTable foldersForObjects = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    if (foldersForObjects == null)
      return foldersForObjects;
    List<DataRow> dataRowList = new List<DataRow>();
    DataColumn column = ImbaseServer.AppendFilterColumn(foldersForObjects);
    if (catalogFilter.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) foldersForObjects.Rows)
      {
        string str1 = Convert.ToString(row[4]);
        if (str1.Length > 1)
        {
          string str2 = str1.Substring(0, 2);
          if (!catalogFilter.Contains(str2))
            dataRowList.Add(row);
          else
            row[column] = (object) true;
        }
      }
      if (dataRowList.Count > 0)
      {
        foreach (DataRow dataRow in dataRowList)
          dataRow.Delete();
        foldersForObjects.AcceptChanges();
      }
    }
    if (foldersForObjects.Columns.Count > 0)
      ImbaseServer.RenameColumns(foldersForObjects);
    if (foldersForObjects.Rows.Count > 0)
      ImbaseServer.BuildUpTree(foldersForObjects, session, paramsSet);
    foldersForObjects.AcceptChanges();
    foldersForObjects.RemotingFormat = SerializationFormat.Binary;
    return foldersForObjects;
  }

  internal static DataColumn AppendFilterColumn(DataTable resultTable)
  {
    return resultTable == null ? (DataColumn) null : ImbaseServer.AppendFilterColumn(resultTable, true);
  }

  internal static DataColumn AppendFilterColumn(DataTable resultTable, bool value)
  {
    if (resultTable == null)
      return (DataColumn) null;
    resultTable.BeginLoadData();
    DataColumn column;
    try
    {
      column = new DataColumn("#FLT", typeof (bool));
      object defaultValue = column.DefaultValue;
      column.DefaultValue = (object) value;
      resultTable.Columns.Add(column);
      column.DefaultValue = defaultValue;
    }
    finally
    {
      resultTable.EndLoadData();
    }
    return column;
  }

  public DataTable GetFoldersForCreateType(
    Guid sessionGuid,
    object needType,
    long[] catalogs,
    bool buildTree)
  {
    return this.GetFoldersForCreateType(sessionGuid, needType, catalogs, buildTree, true);
  }

  public DataTable GetFoldersForCreateType(
    Guid sessionGuid,
    object needType,
    long[] catalogs,
    bool buildTree,
    bool needSubFolders)
  {
    DataTable table = (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    DBRecordSetParams paramsSet1 = ImbaseServer.CreateParamsSet((ConditionStructure[]) null);
    paramsSet1.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    paramsSet1.SortColumns = new object[1]
    {
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    };
    paramsSet1.SortSources = (AttributeSourceTypes[]) null;
    paramsSet1.SortContents = (ColumnContents[]) null;
    paramsSet1.Orders = new SortOrders[1]{ SortOrders.ASC };
    try
    {
      if (!this.GetFoldersForCreateType_Folders4Type(session, needType, catalogs, paramsSet1, out table) || table == null || table.Rows.Count == 0)
        return table;
      DataTable tableCatalogInfo = (DataTable) null;
      if (table.Rows.Count > 50)
      {
        DBRecordSetParams paramsSet2 = paramsSet1;
        paramsSet2.AddColumnDescriptors(new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) Intermech.Imbase.Consts.CreatedObjectAttID, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
        }, new List<int>((IEnumerable<int>) new int[1]
        {
          paramsSet1.Columns.Length
        }));
        ImbaseServer.LoadAllCatalogsInfo(session, paramsSet2, table, out tableCatalogInfo);
        DataColumn dataColumn = ImbaseServer.AppendFilterColumn(tableCatalogInfo, false);
        dataColumn.SetOrdinal(dataColumn.Ordinal - 1);
      }
      DataTable dataTable = table.Copy();
      if (tableCatalogInfo != null && tableCatalogInfo.Rows.Count != 0)
        ImbaseServer.BuildUpTree(dataTable, tableCatalogInfo);
      else
        ImbaseServer.BuildUpTree(dataTable, session, paramsSet1);
      DataTable tableSource;
      if (dataTable.Rows.Count == 0)
      {
        table = dataTable;
        tableSource = dataTable;
      }
      else
      {
        if (!buildTree | needSubFolders)
        {
          HashSet<string> stringSet = new HashSet<string>();
          int columnIndex1 = dataTable.Columns.IndexOf("F_PATH");
          if (columnIndex1 != -1)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              stringSet.Add(Convert.ToString(row[columnIndex1]));
          }
          int columnIndex2 = table.Columns.IndexOf("F_PATH");
          if (columnIndex2 != -1)
          {
            bool flag = false;
            for (int index = table.Rows.Count - 1; index >= 0; --index)
            {
              DataRow row = table.Rows[index];
              if (row != null && !stringSet.Contains(row[columnIndex2].ToString()))
              {
                flag = true;
                row.Delete();
              }
            }
            if (flag)
              table.AcceptChanges();
          }
        }
        tableSource = table;
        if (buildTree)
          table = dataTable;
      }
      if (!needSubFolders)
        return table;
      List<DataRow> sourceRows;
      if (tableCatalogInfo != null && tableCatalogInfo.Rows.Count != 0)
      {
        if (!this.GetFoldersForCreateType_ExtractSubFolderInfo(tableSource, tableCatalogInfo, out sourceRows))
          return table;
      }
      else if (!this.GetFoldersForCreateType_LoadSubFolderInfo(session, paramsSet1, tableSource, out sourceRows))
        return table;
      if (sourceRows != null)
      {
        if (sourceRows.Count > 0)
        {
          int count = sourceRows[0].Table.Columns.Count;
          ImbaseServer.AppendRows(table, (IEnumerable) sourceRows, Math.Min(table.Columns.Count, count), true);
        }
      }
    }
    finally
    {
      if (table != null)
      {
        table.AcceptChanges();
        table.RemotingFormat = SerializationFormat.Binary;
      }
    }
    return table;
  }

  public ICatalogInfo GetCatalogInfo(Guid sessionGuid, string catalogDef)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    ICatalogInfo catalogInfo = (ICatalogInfo) null;
    this.ResolveCatalogDef(catalogDef, session, ref catalogInfo);
    return catalogInfo;
  }

  public IDBObject GetPrototypeObject(Guid sessionGuid, object objectDef)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBObject prototypeObject = (IDBObject) null;
    if (objectDef != null)
    {
      Type type = objectDef.GetType();
      if (type == typeof (long))
        prototypeObject = session.GetObject(Convert.ToInt64(objectDef));
      else if (type == typeof (string))
      {
        prototypeObject = session.GetObject(new Guid((string) objectDef));
      }
      else
      {
        if (!(type == typeof (Guid)))
          throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_18"), (object) type.FullName));
        prototypeObject = session.GetObject((Guid) objectDef);
      }
      IDBObject dbObject = prototypeObject;
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseLinkRefAttID);
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
      if (attributeById2 != null && attributeById2.Values[0] != null && attributeById2.Values[0] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(attributeById2.Values[0]);
        prototypeObject = session.GetObject(int64);
      }
      if (prototypeObject != null && attributeById1 != null && attributeById1.Values[0] != null && prototypeObject is IContextedDBObject contextedDbObject)
        contextedDbObject.ContextId = Convert.ToInt64(attributeById1.Values[0]);
    }
    return prototypeObject;
  }

  public IDBObject GetContextedObject(Guid sessionGuid, long recordId, long linkId)
  {
    IDBObject contextedObject = ImbaseServer.GetSession(sessionGuid).GetObject(recordId);
    (contextedObject as IContextedDBObject).ContextId = linkId;
    return contextedObject;
  }

  public bool GetPrototypeDetails(
    Guid sessionGuid,
    Guid objectGuid,
    ref long linkId,
    ref long recordId)
  {
    IDBObject objectByVersionsRule = ImbaseServer.GetSession(sessionGuid).GetObjectByVersionsRule(objectGuid, "cad005aa-306c-11d8-b4e9-00304f19f545", true);
    return this.GetPrototypeDetails(ref linkId, ref recordId, objectByVersionsRule);
  }

  public bool GetPrototypeDetailsByVersion(
    Guid sessionGuid,
    Guid objectGuid,
    ref long linkId,
    ref long recordId)
  {
    IDBObject parentObject = ImbaseServer.GetSession(sessionGuid).GetObject(objectGuid);
    return this.GetPrototypeDetails(ref linkId, ref recordId, parentObject);
  }

  internal bool GetPrototypeDetails(ref long linkId, ref long recordId, IDBObject parentObject)
  {
    IDBObject dbObject = parentObject;
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    if (attributeById1 != null && attributeById1.Values[0] != null && attributeById1.Values[0] != DBNull.Value)
    {
      linkId = Convert.ToInt64(attributeById1.Values[0]);
      if (attributeById2 != null && attributeById2.Values[0] != null)
      {
        recordId = Convert.ToInt64(attributeById2.Values[0]);
        return true;
      }
    }
    return false;
  }

  public long[] GetCatalogsList(Guid sessionGuid)
  {
    long[] catalogsList = (long[]) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams()
    {
      TableName = "tbl",
      RecordCount = -1,
      Columns = new object[1]
      {
        (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
      }
    };
    DataTable dataTable1 = objectCollection.Select(paramSet);
    SortedList<string, string> sortedList = new SortedList<string, string>(16 /*0x10*/);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      string str = Convert.ToString(row[0]);
      if (str.Length > 1)
      {
        string key = str.Substring(0, 2);
        if (sortedList.IndexOfKey(key) == -1)
          sortedList.Add(key, string.Empty);
      }
    }
    if (sortedList.Keys.Count > 0)
    {
      string[] strArray = new string[sortedList.Keys.Count];
      sortedList.Keys.CopyTo(strArray, 0);
      paramSet.Columns[0] = (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
      paramSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) strArray, LogicalOperators.NONE, 0, true)
      };
      DataTable dataTable2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID).Select(paramSet);
      catalogsList = new long[dataTable2.Rows.Count];
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        catalogsList[num++] = Convert.ToInt64(row[0]);
    }
    return catalogsList;
  }

  public void GetObjectCreateInfo(
    Guid sessionGuid,
    long linkId,
    long recordId,
    ref bool createNew,
    ref int type,
    ref long[] existingObjects)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    existingObjects = (long[]) null;
    type = this.GetNewObjectType(session, linkId, (ArrayList) null, ref createNew);
    DataTable dataTable = ImbaseServer.FinCopies(session, type, linkId, Intermech.Imbase.Consts.ImbaseObjectRefAttID, recordId, Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    int count = dataTable.Rows.Count;
    List<long> longList = new List<long>(count);
    for (int index = 0; index < count; ++index)
      longList.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    existingObjects = longList.ToArray();
  }

  public int GetObjectType(Guid sessionGuid, long linkId)
  {
    return this.GetNewObjectType(ImbaseServer.GetSession(sessionGuid), linkId, (ArrayList) null);
  }

  public Dictionary<string, string> NameRecordReferences(Guid sessionGuid, List<string> keyValues)
  {
    Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
    long result = -1;
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    Stack<long> longStack = ImbaseServer.NamingReferencesStacks.AddOrUpdate(managedThreadId, new Stack<long>(), (Func<int, Stack<long>, Stack<long>>) ((key, oldValue) => oldValue));
    try
    {
      if (keyValues != null)
      {
        IUserSession session = ImbaseServer.GetSession(sessionGuid);
        dictionary = new Dictionary<string, string>();
        DataTable recordsTable = (DataTable) null;
        int num1 = -1;
        int columnIndex = -1;
        int count = keyValues.Count;
        string empty = string.Empty;
        AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
        long num2 = 0;
        ImbaseKeyInfo keyInfo = new ImbaseKeyInfo();
        HashSet<long> longSet = new HashSet<long>();
        keyValues.Sort();
        for (int index = 0; index < count; ++index)
        {
          string keyValue = keyValues[index];
          long linkId;
          long recordId;
          if (ImbaseHelper.TryParseRecordReference(session, keyValue, out linkId, out recordId))
          {
            if (!longSet.Contains(linkId))
            {
              if (result == linkId)
              {
                dictionary[keyValue] = keyValue;
              }
              else
              {
                num1 = -1;
                if (num2 != linkId)
                {
                  try
                  {
                    if (!longStack.Contains(linkId) && longStack.Count < 2)
                    {
                      longStack.Push(linkId);
                      try
                      {
                        TableLoader.LoadTable(session, linkId, string.Empty, out recordsTable, out columnsAttributes, ref keyInfo);
                        num2 = linkId;
                      }
                      finally
                      {
                        longStack.Pop();
                      }
                    }
                    else
                    {
                      dictionary[keyValue] = keyValue;
                      continue;
                    }
                  }
                  catch (ArgumentException ex)
                  {
                    longSet.Add(linkId);
                    continue;
                  }
                  int num3 = -1;
                  int objectType = this.GetObjectType(sessionGuid, linkId);
                  if (objectType != -1)
                    num3 = session.GetObjectType(objectType).CaptionAttribute;
                  if (num3 == -1)
                    num3 = session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID;
                  columnIndex = recordsTable.Columns.IndexOf(num3.ToString());
                  if (columnIndex == -1)
                    columnIndex = recordsTable.Columns.IndexOf(session.IdentHelper.GetAttributeID("cad00020-306c-11d8-b4e9-00304f19f545").ToString());
                  if (columnIndex == -1 && columnsAttributes.Length != 0)
                    columnIndex = recordsTable.Columns.IndexOf(columnsAttributes[0].AttributeID.ToString());
                }
                DataRow[] dataRowArray = recordsTable.Select("[-2]=" + recordId.ToString());
                if (dataRowArray != null && dataRowArray.Length != 0)
                {
                  if (columnIndex != -1)
                    dictionary.Add(keyValue, dataRowArray[0][columnIndex].ToString());
                  else
                    dictionary.Add(keyValue, keyValue);
                }
              }
            }
          }
          else if (keyValue.IndexOf("BadId=") < 0)
            dictionary[keyValue] = keyValue;
          else
            long.TryParse(keyValue.Substring(6), out result);
        }
      }
      return dictionary ?? new Dictionary<string, string>(0);
    }
    finally
    {
      if (longStack != null && longStack.Count == 0)
        ImbaseServer.NamingReferencesStacks.TryRemove(managedThreadId, out Stack<long> _);
    }
  }

  public Dictionary<string, Tuple<string, bool>> NameRecordReferencesWithApplicability(
    Guid sessionGuid,
    List<string> keyValues)
  {
    Dictionary<string, Tuple<string, bool>> dictionary = (Dictionary<string, Tuple<string, bool>>) null;
    if (keyValues != null)
    {
      IUserSession session = ImbaseServer.GetSession(sessionGuid);
      dictionary = new Dictionary<string, Tuple<string, bool>>(keyValues.Count);
      long num1 = -1;
      DataTable recordsTable = (DataTable) null;
      int num2 = -1;
      int count = keyValues.Count;
      string str1 = string.Empty;
      AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
      ImbaseKeyInfo keyInfo = new ImbaseKeyInfo();
      HashSet<long> longSet = new HashSet<long>();
      keyValues.Sort();
      for (int index = 0; index < count; ++index)
      {
        string keyValue = keyValues[index];
        int num3 = keyValue.IndexOf('.');
        if (num3 != -1)
        {
          string str2 = keyValue.Substring(2, num3 - 2);
          string s = keyValue.Substring(num3 + 1);
          long result1;
          if (!long.TryParse(str2, out result1))
          {
            try
            {
              if (str1 != str2)
              {
                Guid objectGUID = new Guid(str2);
                QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
                IDBObject objectActualCopy = session.GetObjectActualCopy(Math.Abs(objectInfo.ObjectID), false);
                if (objectActualCopy != null)
                {
                  result1 = objectActualCopy.ObjectID;
                  str1 = str2;
                }
                else
                  continue;
              }
              else
                result1 = num1;
            }
            catch
            {
              continue;
            }
          }
          else
            str1 = string.Empty;
          if (!longSet.Contains(result1))
          {
            long result2 = 0;
            if (!long.TryParse(s, out result2))
            {
              dictionary.Add(keyValue, new Tuple<string, bool>(keyValue, false));
            }
            else
            {
              if (num1 != result1)
              {
                num1 = result1;
                num2 = -1;
                try
                {
                  TableLoader.LoadTable(session, num1, string.Empty, out recordsTable, out columnsAttributes, ref keyInfo);
                }
                catch (ArgumentException ex)
                {
                  longSet.Add(num1);
                  continue;
                }
                int objectType = this.GetObjectType(sessionGuid, num1);
                if (objectType != -1)
                  num2 = session.GetObjectType(objectType).CaptionAttribute;
                if (num2 == -1)
                  num2 = session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID;
              }
              DataRow[] dataRowArray = recordsTable.Select("[-2]=" + (object) result2);
              if (dataRowArray != null && dataRowArray.Length != 0)
              {
                int columnIndex1 = recordsTable.Columns.IndexOf(num2.ToString());
                int columnIndex2 = recordsTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttID.ToString());
                bool flag = true;
                if (columnIndex2 != -1 && Convert.ToString(dataRowArray[0][columnIndex2]).Trim() == "-")
                  flag = false;
                if (columnIndex1 == -1)
                  columnIndex1 = recordsTable.Columns.IndexOf(session.IdentHelper.GetAttributeID("cad00020-306c-11d8-b4e9-00304f19f545").ToString());
                if (columnIndex1 == -1 && columnsAttributes.Length != 0)
                  columnIndex1 = recordsTable.Columns.IndexOf(columnsAttributes[0].AttributeID.ToString());
                if (columnIndex1 != -1)
                  dictionary.Add(keyValue, new Tuple<string, bool>(dataRowArray[0][columnIndex1].ToString(), flag));
              }
            }
          }
        }
      }
    }
    return dictionary ?? new Dictionary<string, Tuple<string, bool>>(0);
  }

  public Dictionary<string, string> NameObjectReferences(Guid sessionGuid, List<string> keyValues)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    // ISSUE: explicit non-virtual call
    Dictionary<string, string> dictionary = new Dictionary<string, string>(keyValues != null ? __nonvirtual (keyValues.Count) : 0);
    if (keyValues == null || keyValues.Count == 0)
      return dictionary;
    string[] array = keyValues.ToArray();
    keyValues.Clear();
    foreach (string input in array)
    {
      if (!string.IsNullOrEmpty(input) && input.Length >= 36 && Guid.TryParse(input, out Guid _))
        keyValues.Add(input);
    }
    if (keyValues.Count == 0)
      return dictionary;
    object[] columns = new object[2]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID), RelationalOperators.In, (object) keyValues.ToArray(), LogicalOperators.NONE, 0, false)
    }, columns, (object[]) null, (SortOrders[]) null)
    {
      TableName = "f",
      FailIfNotFound = false
    };
    DataRowCollection rows = session.GetObjectCollection(-1).Select(paramSet).Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      string key = Convert.ToString(rows[index][0]);
      string str = Convert.ToString(rows[index][1]);
      if (!string.IsNullOrEmpty(str))
      {
        keyValues.Remove(key);
        dictionary.Add(key, str.Trim());
      }
    }
    if (keyValues.Count > 0)
    {
      foreach (string keyValue in keyValues)
      {
        try
        {
          if (!string.IsNullOrEmpty(keyValue))
          {
            if (!dictionary.ContainsKey(keyValue))
            {
              Guid objectGUID = new Guid(keyValue);
              QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
              if (objectInfo.Caption.Length > 0)
                dictionary.Add(keyValue, objectInfo.Caption.Trim());
            }
          }
        }
        catch
        {
        }
      }
    }
    return dictionary;
  }

  public DataTable GetTableRefs(Guid sessionGuid, long tableId, DBRecordSetParams queryParams)
  {
    ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tableId, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
    queryParams.Conditions = conditionStructureArray;
    return ImbaseServer.GetSession(sessionGuid).GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(queryParams);
  }

  public string ConvertOldImbaseKey(
    Guid sessionGuid,
    string oldImbaseKey,
    out ScanOldKeyStatus status)
  {
    string str = string.Empty;
    long linkId;
    long recordId;
    ImbaseServer.GetNewRecordId(ImbaseServer.GetSession(sessionGuid), oldImbaseKey, out linkId, out recordId, out status);
    if (status == ScanOldKeyStatus.Success)
      str = ImbaseHelper.MakeInternalImbaseKey(linkId, recordId);
    return str;
  }

  public DataTable GetUnlinkedTables(Guid sessionGuid)
  {
    DataTable unlinkedTables = (DataTable) null;
    if (ImbaseServer.GetSession(sessionGuid) is UserSession session)
    {
      IDbManager dataManager = session.DataManager;
      if (dataManager != null)
      {
        string commandText = $"SELECT F_OBJECT_ID, CAPTION FROM IMV_O{Intermech.Imbase.Consts.ImbaseTableTypeID} a WHERE (NOT EXISTS (SELECT F_TOOBJECT_ID FROM IMS_OBJECT_LINKS b WHERE (a.F_OBJECT_ID = F_TOOBJECT_ID)))";
        unlinkedTables = dataManager.ExecuteDataTable(commandText);
      }
    }
    return unlinkedTables;
  }

  public void CapitalizeFolders(Guid sessionGuid, long folderId, bool upperCase)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    DataRowCollection rows = this.GetAllSubfolders(sessionGuid, folderId, new int[1]
    {
      Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID
    }).Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      long int64 = Convert.ToInt64(dataRow["F_OBJECT_ID"]);
      int captionAttribute = MetaDataHelper.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).CaptionAttribute;
      IDBAttribute attributeById = session.GetObject(int64).GetAttributeByID(captionAttribute);
      if (attributeById != null)
      {
        string asString = attributeById.AsString;
        string str = this.CapitalizeString(asString, upperCase);
        if (str != asString)
          attributeById.AsString = str;
      }
    }
  }

  public int FindItemByValue(
    Guid sessionGuid,
    string fieldName,
    string fieldValue,
    ref string imbaseKey)
  {
    imbaseKey = string.Empty;
    if (!(ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service))
      return 2;
    if (string.IsNullOrWhiteSpace(fieldName))
      return 3;
    if (string.IsNullOrWhiteSpace(fieldValue))
      return 4;
    if (fieldName.Equals("ПОЛНОЕ ОБОЗНАЧЕНИЕ", StringComparison.InvariantCultureIgnoreCase))
      fieldName = "НАИМЕНОВАНИЕ";
    DataTable indexes = service.GetIndexes(sessionGuid, (List<long>) null);
    if (indexes == null)
      return 5;
    int attrID = 0;
    DataColumn column = indexes.Columns["F_ATTRIBUTE_ID"];
    foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
    {
      int int32 = Convert.ToInt32(row[column]);
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(int32);
      if (string.Equals(fieldName, attributeTypeName, StringComparison.InvariantCultureIgnoreCase))
      {
        attrID = int32;
        break;
      }
    }
    if (attrID == 0)
      return 6;
    long tableRefID;
    long recID;
    if (!service.FindByIndex(sessionGuid, attrID, fieldValue, out tableRefID, out recID))
      return 1;
    imbaseKey = ImbaseHelper.MakeInternalImbaseKey(tableRefID, recID);
    return 0;
  }

  public IDBSecurity GetSecurityForRecord(Guid sessionGuid, long tableId, long recordId)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    return (IDBSecurity) new ImbaseRecordSecurity(session as UserSession, session.GetObject(tableId) as DBObject, recordId);
  }

  public IDBSecurity GetSecurityForAtt(Guid sessionGuid, long tableId, int attId)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    return (IDBSecurity) new ImbaseAttSecurity(session as UserSession, session.GetObject(tableId) as DBObject, attId);
  }

  public IDBSecurity GetSecurityForIndex(Guid sessionGuid, long catalogId, int attId)
  {
    return (IDBSecurity) new ImbaseIndexSecurity(ImbaseServer.GetSession(sessionGuid) as UserSession, catalogId, attId);
  }

  public void PurgeSecurityForIndex(Guid sessionGuid, long catalogId, int attId)
  {
    new ImbaseIndexSecurity(ImbaseServer.GetSession(sessionGuid) as UserSession, catalogId, attId).PurgeSecurity();
  }

  public void LogDataChanges(Guid sessionGuid, long tableId, DataSet newDataSet)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    DataSet tablesInternal = TableLoadHelper.GetTablesInternal(session, tableId);
    if (this.DataSetChanged != null)
    {
      try
      {
        ImbaseServer.DataSetChangedEventHandler dataSetChanged = this.DataSetChanged;
        if (dataSetChanged != null)
          dataSetChanged(session, tableId, tablesInternal, newDataSet);
      }
      catch
      {
      }
    }
    if (tablesInternal == null)
      return;
    DataTable table1 = newDataSet.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = newDataSet.Tables["IMS_DATA"];
    DataTable table3 = tablesInternal.Tables["IMS_ATTR_TYPES"];
    DataTable table4 = tablesInternal.Tables["IMS_DATA"];
    AttributeTypeProperties[] attProperties1 = TableLoadHelper.GetAttProperties(session, table1);
    AttributeTypeProperties[] attProperties2 = TableLoadHelper.GetAttProperties(session, table3);
    StringBuilder stringBuilder1 = new StringBuilder();
    List<string> stringList = new List<string>();
    foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
    {
      string g = row["F_ATTRIBUTE_GUID"].ToString();
      Guid guid = new Guid(g);
      DataRow[] dataRowArray = table3.Select($"[F_ATTRIBUTE_GUID]='{guid.ToString()}'");
      int index = TableLoadHelper.IndexOfAttProp(guid, attProperties1);
      if (index != -1)
      {
        AttributeTypeProperties attributeTypeProperties = attProperties1[index];
        if ((session.GetAttributeType(guid).Options & AttributeOptions.SaveInLog) != AttributeOptions.None)
          stringList.Add(g);
        if (dataRowArray == null || dataRowArray.Length == 0)
        {
          (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 26, (long) attributeTypeProperties.AttributeID, attributeTypeProperties.Name, $"Добавлен атрибут '{attributeTypeProperties.Name}'", ActionType.Create, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
        }
        else
        {
          stringBuilder1.Clear();
          DataRow dataRow = dataRowArray[0];
          int int32_1 = Convert.ToInt32(dataRow["F_COMPUTED"]);
          int int32_2 = Convert.ToInt32(row["F_COMPUTED"]);
          if (int32_1 != int32_2)
          {
            stringBuilder1.Append(";");
            stringBuilder1.Append("Вычисляемое : ");
            stringBuilder1.Append(int32_1 == 0 ? "Нет" : "Да");
            stringBuilder1.Append(" -> ");
            stringBuilder1.Append(int32_2 == 0 ? "Нет" : "Да");
          }
          string objA1 = Convert.ToString(dataRow["F_FORMULA"]);
          string objB1 = Convert.ToString(row["F_FORMULA"]);
          if (!object.Equals((object) objA1, (object) objB1))
          {
            stringBuilder1.Append(";");
            stringBuilder1.Append("Формула : ");
            stringBuilder1.Append($"{objA1} -> {objB1}");
          }
          string objA2 = Convert.ToString(dataRow["F_DEFAULT_VALUE"]);
          string objB2 = Convert.ToString(row["F_DEFAULT_VALUE"]);
          if (!object.Equals((object) objA2, (object) objB2))
          {
            stringBuilder1.Append(";");
            stringBuilder1.Append("Значение по умолчанию : ");
            stringBuilder1.Append($"{objA2} -> {objB2}");
          }
          if (stringBuilder1.Length > 0)
          {
            string Note = "Изменены свойства атрибута." + stringBuilder1.ToString();
            (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 26, (long) attributeTypeProperties.AttributeID, attributeTypeProperties.Name, Note, ActionType.Edit, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
          }
        }
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) table3.Rows)
    {
      Guid attGuid = new Guid(row["F_ATTRIBUTE_GUID"].ToString());
      DataRow[] dataRowArray = table1.Select($"[F_ATTRIBUTE_GUID]='{attGuid.ToString()}'");
      int index = TableLoadHelper.IndexOfAttProp(attGuid, attProperties2);
      if (index != -1)
      {
        AttributeTypeProperties attributeTypeProperties = attProperties2[index];
        if (dataRowArray == null || dataRowArray.Length == 0)
          (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 26, (long) attributeTypeProperties.AttributeID, attributeTypeProperties.Name, $"Удален атрибут '{attributeTypeProperties.Name}'", ActionType.Delete, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
      }
    }
    StringBuilder stringBuilder2 = new StringBuilder();
    foreach (DataRow row in (InternalDataCollectionBase) table2.Rows)
    {
      long int64 = Convert.ToInt64(row["F_KEY"]);
      DataRow[] dataRowArray = table4.Select($"[{"F_KEY"}]={int64}");
      if (dataRowArray == null || dataRowArray.Length == 0)
      {
        (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 25, int64, string.Empty, $"Добавлена запись '{row["F_KEY"].ToString()}:{row["F_GUID"].ToString()}'", ActionType.Create, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
      }
      else
      {
        stringBuilder1.Clear();
        stringBuilder2.Clear();
        DataRow dataRow = dataRowArray[0];
        foreach (DataColumn column in (InternalDataCollectionBase) table2.Columns)
        {
          if (column.Ordinal >= 2)
          {
            string columnName = column.ColumnName;
            if (columnName.Length >= 4)
            {
              if (stringList.Contains(columnName))
              {
                try
                {
                  int index = TableLoadHelper.IndexOfAttProp(new Guid(columnName), attProperties1);
                  if (index != -1)
                  {
                    AttributeTypeProperties attributeTypeProperties = attProperties1[index];
                    if (table4.Columns.Contains(columnName))
                    {
                      if (!object.Equals(row[column], dataRow[columnName]))
                      {
                        if (stringBuilder2.Length > 0)
                          stringBuilder2.Append(',');
                        stringBuilder2.Append(attributeTypeProperties.Name);
                        if (stringBuilder1.Length > 0)
                          stringBuilder1.Append(';');
                        stringBuilder1.Append($"атрибут '{attributeTypeProperties.Name}':");
                        stringBuilder1.Append($"'{Convert.ToString(dataRow[columnName])}' -> '{Convert.ToString(row[column])}'");
                      }
                    }
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
        if (stringBuilder1.Length > 0)
        {
          string Note = "Изменены значения :" + stringBuilder1.ToString();
          (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 25, int64, stringBuilder2.ToString(), Note, ActionType.Edit, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
        }
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) table4.Rows)
    {
      long int64 = Convert.ToInt64(row["F_KEY"].ToString());
      DataRow[] dataRowArray = table2.Select($"[{"F_KEY"}]={int64}");
      if (dataRowArray == null || dataRowArray.Length == 0)
        (session as UserSession).EventLogHelper.AddEvent(tableId, 0L, 25, int64, string.Empty, $"Удалена '{row["F_KEY"].ToString()}:{row["F_GUID"].ToString()}'", ActionType.Delete, EventlogRecordType.Information, session.UserID, session.ComputerName, session);
    }
  }

  private string CapitalizeString(string sourceString, bool upper)
  {
    if (string.IsNullOrEmpty(sourceString))
      return string.Empty;
    char[] charArray = sourceString.ToCharArray();
    int length = charArray.Length;
    bool flag = false;
    charArray[0] = char.ToUpperInvariant(charArray[0]);
    for (int index = 1; index < length; ++index)
    {
      char c = charArray[index];
      switch (c)
      {
        case '[':
          flag = true;
          break;
        case ']':
          flag = false;
          break;
        default:
          if (!flag)
          {
            charArray[index] = !upper ? char.ToLowerInvariant(c) : char.ToUpperInvariant(c);
            break;
          }
          break;
      }
    }
    return new string(charArray);
  }

  public List<long> GetTablesWithAtt(Guid sessionGuid, int attributeId)
  {
    List<long> tablesWithAtt = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) (ImbaseServer.GetSession(sessionGuid) as UserSession).DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_IMBASE_ATTRS WHERE F_ATTRIBUTE_ID={attributeId}").Rows)
      tablesWithAtt.Add(Convert.ToInt64(row[0]));
    return tablesWithAtt;
  }

  public void UpdateSystemIndexes(Guid sessionGuid)
  {
    TablesIndexer.Instance.CheckUpdateIndexes(ImbaseServer.GetSession(sessionGuid), true);
  }

  public long[] GetCatalogsForCreateType(Guid sessionGuid, object needType, bool derivedTypes)
  {
    List<long> list1 = new List<long>();
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet((ConditionStructure[]) null);
    paramsSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    paramsSet.SortColumns = new object[1]
    {
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    };
    paramsSet.SortSources = (AttributeSourceTypes[]) null;
    paramsSet.SortContents = (ColumnContents[]) null;
    paramsSet.Orders = new SortOrders[1]{ SortOrders.ASC };
    if (session == null)
      throw new ArgumentNullException("session");
    List<Guid> parentTypeGuids = needType != null ? ImbaseServer.TranslateTypeToGuids(session, needType) : throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Server_17"), nameof (needType));
    if (derivedTypes)
    {
      List<Guid> childrenGuidRecursive = MetaDataHelper.GetObjectTypeChildrenGuidRecursive((IEnumerable<Guid>) parentTypeGuids);
      parentTypeGuids.AddRange((IEnumerable<Guid>) childrenGuidRecursive);
    }
    if (parentTypeGuids.Count == 0)
      return list1.ToArray();
    List<string> stringList1 = new List<string>((IEnumerable<string>) parentTypeGuids.ConvertAll<string>((Converter<Guid, string>) (item => item.ToString())));
    ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.In, (object) stringList1.ToArray(), LogicalOperators.AND, 1, false),
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, -1, false)
    };
    paramsSet.Conditions = conditionStructureArray;
    DataTable dataTable = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    if (dataTable == null || dataTable.Rows.Count == 0)
      return list1.ToArray();
    List<string> list2 = new List<string>();
    DataColumn column = dataTable.Columns[Intermech.Imbase.Consts.ClassifFolderKeyAttId.ToString()];
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      string str1 = Convert.ToString(row[column]);
      if (!string.IsNullOrEmpty(str1) && str1.Length > 1)
      {
        string str2 = str1.Substring(0, 2);
        CollectionUtils.AddSorted<string>(list2, str2);
      }
    }
    List<string> stringList2 = new List<string>();
    foreach (string str in list2)
    {
      DataRow[] dataRowArray = dataTable.Select($"[{Intermech.Imbase.Consts.ClassifFolderKeyAttId}]='{str}'");
      if (dataRowArray != null && dataRowArray.Length != 0)
      {
        long int64 = Convert.ToInt64(dataRowArray[0][0]);
        CollectionUtils.AddSorted<long>(list1, int64);
      }
      else
        stringList2.Add(str);
    }
    foreach (string classifKey in stringList2)
    {
      long catalogByClassyfKey = this.GetCatalogByClassyfKey(session, classifKey);
      if (catalogByClassyfKey != 0L)
        CollectionUtils.AddSorted<long>(list1, catalogByClassyfKey);
    }
    return list1.ToArray();
  }

  public long GetCatalogByClassyfKey(IUserSession session, string classifKey)
  {
    string conditionValue = classifKey;
    if (conditionValue.Length > 2)
      conditionValue = conditionValue.Substring(0, 2);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[1]{ columnDescriptor });
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable == null || dataTable.Rows.Count <= 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  public void ForceImportImbaseTable(Guid sessionGuid, long tableObjectId, long linkParentId)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    try
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(tableObjectId, true);
      ImportTableBlobHandler.LinkParentId = linkParentId;
      ImportTableBlobHandler.Forced = true;
      ImportTableBlobHandler.ObjectImportedEvent((object) this, new ObjectImportedEventArgs(session, objectActualCopy));
    }
    finally
    {
      ImportTableBlobHandler.LinkParentId = -1L;
      ImportTableBlobHandler.Forced = false;
    }
  }

  public string CalcOldImbaseKey(Guid sessionGuid, long linkObjectId, long recordId)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.Equal, (object) linkObjectId, LogicalOperators.NONE, 0, false)
    }, new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    });
    DataTable dataTable1 = objectCollection1.Select(paramSet);
    if (dataTable1 == null || dataTable1.Rows.Count == 0)
      return $"Объект \"{MetaDataHelper.GetObjectName(Intermech.Imbase.Consts.ImbaseTableRefTypeID)}\" id:{linkObjectId} не найден";
    string conditionValue = Convert.ToString(dataTable1.Rows[0][2]).Substring(0, 2);
    long int64Value1 = DataSetProcessor.GetInt64Value(dataTable1.Rows[0], 1, 0L);
    if (int64Value1 == 0L)
      return $"В объекте \"{MetaDataHelper.GetObjectName(Intermech.Imbase.Consts.ImbaseTableRefTypeID)}\" id:{linkObjectId} нет значения атрибута \"{MetaDataHelper.GetAttributeTypeName(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID)}\"";
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, false)
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID
    });
    DataTable dataTable2 = objectCollection2.Select(paramSet);
    if (dataTable2 == null || dataTable2.Rows.Count == 0)
      return $"{MetaDataHelper.GetObjectName(Intermech.Imbase.Consts.ImbaseCatalogTypeID)} не найден";
    long int64Value2 = DataSetProcessor.GetInt64Value(dataTable2.Rows[0], 1, 0L);
    if (int64Value2 != 0L)
      return $"I6{int64Value2:X6}{int64Value1:X6}{recordId:X6}";
    long int64Value3 = DataSetProcessor.GetInt64Value(dataTable2.Rows[0], 0, 0L);
    return $"В объекте \"{MetaDataHelper.GetObjectName(Intermech.Imbase.Consts.ImbaseCatalogTypeID)}\" id:{int64Value3} нет значения атрибута \"{MetaDataHelper.GetAttributeTypeName(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID)}\"";
  }

  public int GetBaseVersionGuids(Guid sessionGuid, string[] objectGuids, out object[] baseData)
  {
    baseData = (object[]) null;
    List<object> objectList = new List<object>();
    List<Guid> objectGUIDs = new List<Guid>();
    UserSession session = ImbaseServer.GetSession(sessionGuid) as UserSession;
    foreach (string objectGuid in objectGuids)
    {
      objectList.Add((object) new string[2]
      {
        string.Empty,
        string.Empty
      });
      Guid result;
      if (objectGuid.StartsWith("IV", StringComparison.CurrentCultureIgnoreCase))
      {
        if (Guid.TryParse(objectGuid.Substring(2), out result))
        {
          objectGUIDs.Add(result);
          continue;
        }
      }
      else if (objectGuid.StartsWith("IG", StringComparison.CurrentCultureIgnoreCase) && Guid.TryParse(objectGuid.Substring(2), out result))
      {
        IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(result, "cad005aa-306c-11d8-b4e9-00304f19f545", false);
        if (objectByVersionsRule != null)
        {
          objectGUIDs.Add(objectByVersionsRule.ObjectGUID);
          continue;
        }
      }
      objectGUIDs.Add(Guid.Empty);
    }
    DataTable objectInfoByGuiDs = SqlHelper.GetObjectInfoByGUIDs((ICollection<Guid>) objectGUIDs, session.DataManager);
    int count = objectGUIDs.Count;
    string caption = (string) null;
    long ID = 0;
    for (int index = 0; index < count; ++index)
    {
      string[] strArray = (string[]) objectList[index];
      Guid guid = objectGUIDs[index];
      switch (this.CheckBaseVersion(guid, objectInfoByGuiDs, ref caption, ref ID))
      {
        case -1:
          objectList[index] = (object) null;
          break;
        case 0:
          IDBObject objectBaseVersionById = session.GetObjectBaseVersionByID(ID, false);
          strArray[0] = "IV" + objectBaseVersionById.ObjectGUID.ToString();
          strArray[1] = objectBaseVersionById.Caption;
          break;
        case 1:
          strArray[0] = "IV" + guid.ToString();
          strArray[1] = caption;
          break;
      }
    }
    baseData = objectList.ToArray();
    return 0;
  }

  private int CheckBaseVersion(Guid guid, DataTable dt, ref string caption, ref long ID)
  {
    DataColumn column1 = dt.Columns["F_GUID"];
    DataColumn column2 = dt.Columns["F_BASE_VERSION"];
    string str = guid.ToString();
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      if (Convert.ToString(row[column1]).Equals(str))
      {
        caption = Convert.ToString(row["CAPTION"]);
        ID = Convert.ToInt64(row["F_ID"]);
        return Convert.ToInt32(row[column2]);
      }
    }
    return -1;
  }

  public DataTable GetFavoriteFoldersForCatalogs(
    Guid sessionGuid,
    long[] catalogIds,
    bool needContent)
  {
    DataTable resultTable = (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    List<int> intList = new List<int>()
    {
      Intermech.Imbase.Consts.ImbaseFavoritesTypeID
    };
    if (needContent)
      intList.AddRange((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
    }, AttributableElements.Relation);
    paramsSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    try
    {
      resultTable = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) catalogIds), session, (IEnumerable<int>) new int[1]
      {
        Intermech.Imbase.Consts.ImbaseFavoritesRelationID
      }, true, paramsSet);
      if (resultTable != null)
      {
        if (resultTable.Columns.Count > 0)
          ImbaseServer.RenameColumns(resultTable);
      }
    }
    finally
    {
      if (resultTable != null)
        resultTable.RemotingFormat = SerializationFormat.Binary;
    }
    return resultTable;
  }

  protected internal void CheckDBVersion()
  {
    new Action(this.StartCheckDBVersion).BeginInvoke((AsyncCallback) null, (object) null);
  }

  private void StartCheckDBVersion()
  {
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("imbase.getlocalsession") as UserSession;
    try
    {
      if (sessionTemporaryClone == null)
        return;
      int version = 1;
      int revision = 1;
      if (!this.IsNeedUpdateModuleDBVersion(sessionTemporaryClone, "IMBASE", version, revision))
        return;
      IDBTransactions customService = (IDBTransactions) sessionTemporaryClone.GetCustomService(typeof (IDBTransactions));
      if (customService == null)
        return;
      customService.StartTransaction();
      try
      {
        if (this.PatchImbaseParams(sessionTemporaryClone))
          this.UpdateModuleDBVersion(sessionTemporaryClone, "IMBASE", version, revision);
        customService.Commit();
      }
      catch (Exception ex)
      {
        customService.Rollback();
        sessionTemporaryClone.EventLog.AddToTrace($"Ошибка во время обновления версии базы Imbase: {ex.Message}{Environment.NewLine}{ex.StackTrace}", 0, string.Empty);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("imbase.getlocalsession");
    }
  }

  private bool IsNeedUpdateModuleDBVersion(
    UserSession session,
    string moduleName,
    int version,
    int revision)
  {
    int version1 = 0;
    int revision1 = 0;
    session.GetDBVersionEx(moduleName, ref version1, ref revision1);
    return version1 != version || revision1 != revision;
  }

  private bool PatchImbaseParams(UserSession userSession)
  {
    bool flag = false;
    ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetImbaseExtendedItem((IUserSession) userSession, -1, MetaDataHelper.GetAttributeTypeID("cad0038c-306c-11d8-b4e9-00304f19f545"));
    if (imbaseExtendedItem != null)
    {
      DataTable dataTable = userSession.Configurations.ReadSection("KERNEL", "Imbase directories", 0L);
      List<long> list = dataTable.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
      if (list.Count > 0)
      {
        foreach (long objectID in list)
        {
          if (!userSession.GetObjectInfo(objectID).Empty && !imbaseExtendedItem.CatalogIDs.Contains(objectID))
            imbaseExtendedItem.CatalogIDs.Add(objectID);
        }
        ExtendedServiceHelper.CurObjTypeInfo.SetValue(MetaDataHelper.GetAttributeTypeID("cad0038c-306c-11d8-b4e9-00304f19f545"), imbaseExtendedItem);
        ExtendedServiceHelper.CurObjTypeInfo.SaveData((IUserSession) userSession);
        dataTable.Rows.Clear();
        userSession.Configurations.WriteSection("KERNEL", "Imbase directories", dataTable, 0L);
      }
      flag = true;
    }
    return flag;
  }

  private void UpdateModuleDBVersion(
    UserSession session,
    string moduleName,
    int version,
    int revision)
  {
    session.SetDBVersion(moduleName, version, revision);
  }

  public delegate void DataSetChangedEventHandler(
    IUserSession session,
    long tableId,
    DataSet oldDataSet,
    DataSet newDataSet);

  internal enum ObjectCreateMode
  {
    Unknown,
    FromFolder,
    FromTable,
  }

  internal struct PathItem(long objectId, int typeId)
  {
    private long _objectId = objectId;
    private int _typeId = typeId;
  }
}
