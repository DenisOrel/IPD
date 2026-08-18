// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.DataImporter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class DataImporter
{
  private DataTable _data;
  private ImportDataBackGroundTask _task;
  private Dictionary<string, long> _objectDict = new Dictionary<string, long>();
  private Dictionary<(int ObjTypeId, string AttrValue), long> _hierarchyDict = new Dictionary<(int, string), long>();
  private List<DataColumn> _objectAttributesColumns;
  private List<DataColumn> _objectUpdateAttributesColumns;
  private List<DataColumn> _relationUpdateAttributesColumns;
  private List<DataColumn> _relationAttributesColumns;
  private int _objTypeColIndx = -1;
  private int _entranceObjTypeColIndx = -1;
  private int _entranceColIndx = -1;
  private int _relTypeColIndx = -1;
  private int _hierarchyKeyColIndx = -1;
  private bool _skipFirstRow;
  private bool _skipObjectExistErrs;
  private bool _skipRelationExistErrs;
  private long _parentObjId;
  private int _imbaseSyncColIndx = -1;

  internal DataImporter(DataTable data, ImportDataBackGroundTask task)
  {
    this._data = data;
    this._task = task;
    if (!(this._data.ExtendedProperties[(object) Consts.ImportSettings] is ImportSettings extendedProperty))
      throw new Exception("Import settings doesn't set!");
    this._skipFirstRow = extendedProperty.CommonImportOptions.HasFlag((Enum) CommonImportOptions.SkipFirstRow);
    this._skipObjectExistErrs = extendedProperty.CommonImportOptions.HasFlag((Enum) CommonImportOptions.IgnoreExistingObjectErrs);
    this._skipRelationExistErrs = extendedProperty.CommonImportOptions.HasFlag((Enum) CommonImportOptions.IgnoreExistingRelationErrs);
    this._parentObjId = extendedProperty.ParentObjectId;
  }

  internal StringBuilder ImportData()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.InitializeData();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        StringBuilder objects = this.CreateObjects(session);
        if (objects.Length > 0)
          stringBuilder.Append((object) objects);
        if (this._entranceColIndx == -1 && this._parentObjId == 0L)
          return stringBuilder;
        StringBuilder relations = this.CreateRelations(session);
        if (relations.Length > 0)
          stringBuilder.Append((object) relations);
      }
    }
    catch (Exception ex)
    {
      stringBuilder.AppendLine(ex.Message);
    }
    return stringBuilder;
  }

  private void InitializeData()
  {
    this._objTypeColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.ObjectType)));
    this._entranceObjTypeColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.EntrancyObjectType)));
    this._entranceColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Entrancy)));
    this._relTypeColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.RelationType)));
    if (this._entranceColIndx != -1)
      this._hierarchyKeyColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).TypeId == ((ColumnConfiguration) this._data.Columns[this._entranceColIndx].ExtendedProperties[(object) Consts.ColumnPropName]).TypeId)));
    this._imbaseSyncColIndx = this._data.Columns.IndexOf(this._data.Columns.Cast<DataColumn>().FirstOrDefault<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SyncImbase)));
    this._objectAttributesColumns = this._data.Columns.Cast<DataColumn>().Where<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && !((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SyncImbase)).ToList<DataColumn>();
    this._objectUpdateAttributesColumns = this._data.Columns.Cast<DataColumn>().Where<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeUpdateMode == SettingItemAttributeUpdateMode.Update)).ToList<DataColumn>();
    this._relationAttributesColumns = this._data.Columns.Cast<DataColumn>().Where<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Relation)).ToList<DataColumn>();
    this._relationUpdateAttributesColumns = this._data.Columns.Cast<DataColumn>().Where<DataColumn>((System.Func<DataColumn, bool>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] != null && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).ItemType == SettingItemType.AttributeType && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeBelongs == SettingItemAttributeSourceType.Relation && ((ColumnConfiguration) x.ExtendedProperties[(object) Consts.ColumnPropName]).SettingItemAttributeUpdateMode == SettingItemAttributeUpdateMode.Update)).ToList<DataColumn>();
    this._task.MaximumValue = this._entranceColIndx != -1 || this._parentObjId != 0L ? this._data.Rows.Count * 2 : this._data.Rows.Count;
  }

  private StringBuilder CreateObjects(IUserSession session)
  {
    StringBuilder objects = new StringBuilder();
    this._hierarchyDict.Clear();
    this._objectDict.Clear();
    List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
    ColumnConfiguration extendedProperty1 = (ColumnConfiguration) this._data.Columns[this._objTypeColIndx].ExtendedProperties[(object) Consts.ColumnPropName];
    DataView defaultView = this._data.DefaultView;
    defaultView.Sort = string.Join(", ", this._objectAttributesColumns.Select<DataColumn, string>((System.Func<DataColumn, string>) (x => x.ColumnName)));
    string str1 = string.Empty;
    foreach (DataRowView dataRowView in defaultView)
    {
      if (!this._task.IsProcessStoped)
      {
        DataRow row = dataRowView.Row;
        string attributesString = this.GetObjectAttributesString(row);
        if (attributesString == str1)
        {
          this._task.Value = (object) (Convert.ToInt32(this._task.Value) + 1);
        }
        else
        {
          str1 = attributesString;
          long num = 0;
          try
          {
            if (this._imbaseSyncColIndx != -1 && !string.IsNullOrEmpty(Convert.ToString(row[this._imbaseSyncColIndx])))
            {
              ColumnConfiguration extendedProperty2 = (ColumnConfiguration) this._data.Columns[this._imbaseSyncColIndx].ExtendedProperties[(object) Consts.ColumnPropName];
              string request = Convert.ToString(row[this._imbaseSyncColIndx]).Trim();
              int typeId = extendedProperty2.TypeId;
              IImbaseIndexingService service = ServiceUtils.GetService<IImbaseIndexingService>((object) session, true);
              try
              {
                long tableRefID;
                long recID;
                if (service.FindByIndex(session.SessionGUID, typeId, request, out tableRefID, out recID))
                  num = ServiceUtils.GetService<IImbaseServer>((object) session, true).CreateObject(session.SessionGUID, -1L, tableRefID, recID, true, -1);
                else
                  throw new Exception($"Не  удалось найти объект IMBASE по значению атрибута '{MetaDataHelper.GetAttributeTypeName(typeId)}'='{request}'");
              }
              catch (IndexNotFoundException ex)
              {
                throw new IndexNotFoundException($"Невозможно найти объект IMBASE по значению атрибута '{typeId}'='{request}', так как не настроена индексация каталогов IMBASE по этому атрибуту. Обратитесь к администратору IPS.", (Exception) ex);
              }
            }
            else
            {
              int objectTypeId = this.GetObjectTypeId(objectTypesList, extendedProperty1, Convert.ToString(row[this._objTypeColIndx]));
              IDBObject dbObject = objectTypeId != -1 ? session.GetObjectCollection(objectTypeId).Create() : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_259"), row[this._objTypeColIndx]));
              string str2 = this.SetAttributeValues((IDBAttributable) dbObject, (IDBAttributableType) session.GetObjectType(objectTypeId), (IEnumerable<DataColumn>) this._objectAttributesColumns, row);
              dbObject.CommitCreation(true);
              num = dbObject.ObjectID;
              if (str2 != string.Empty)
                objects.AppendLine($"{dbObject.NameInMessages} - {str2}");
            }
          }
          catch (ObjectAlreadyExists ex1)
          {
            num = ex1.ObjectID;
            if (!this._skipObjectExistErrs)
              objects.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex1.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
            if (this._objectUpdateAttributesColumns.Count > 0)
            {
              bool flag = false;
              IDBObject dbObject = session.GetObject(ex1.ObjectID);
              try
              {
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == 0L)
                {
                  dbObject = dbObject.CheckOut();
                  flag = true;
                }
                string str3 = this.UpdateAttributeValues((IDBAttributable) dbObject, (IDBAttributableType) session.GetObjectType(dbObject.ObjectType), (IEnumerable<DataColumn>) this._objectUpdateAttributesColumns, row);
                if (str3 != string.Empty)
                  objects.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) str3, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
                if (flag)
                {
                  if (dbObject.CheckoutBy == session.UserID)
                    dbObject.CheckIn();
                }
              }
              catch (Exception ex2)
              {
                objects.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex2.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
              }
            }
          }
          catch (Exception ex)
          {
            objects.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
          }
          finally
          {
            if ((this._entranceColIndx != -1 || this._parentObjId != 0L) && num != 0L)
            {
              if (!this._objectDict.ContainsKey(attributesString))
                this._objectDict.Add(attributesString, num);
              if (this._hierarchyKeyColIndx != -1)
              {
                string attrValue = row[this._hierarchyKeyColIndx].ToString();
                (int, string) key = DataImporter.GenHashValueForHierarchy(-1, attrValue);
                if (attrValue != string.Empty && !this._hierarchyDict.ContainsKey(key))
                  this._hierarchyDict.Add(key, num);
              }
            }
            this._task.Value = (object) (Convert.ToInt32(this._task.Value) + 1);
          }
        }
      }
      else
        break;
    }
    return objects;
  }

  private StringBuilder CreateRelations(IUserSession session)
  {
    StringBuilder relations = new StringBuilder();
    List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    DataView defaultView = this._data.DefaultView;
    int num = -1;
    if (this._entranceColIndx != -1)
    {
      num = ((ColumnConfiguration) this._data.Columns[this._entranceColIndx].ExtendedProperties[(object) Consts.ColumnPropName]).TypeId;
      defaultView.Sort = this._data.Columns[this._entranceColIndx].ColumnName;
    }
    string str1 = "prevEntrance";
    IDBObject dbObject1 = (IDBObject) null;
    bool flag = false;
    List<IMSApplicability> source1 = new List<IMSApplicability>();
    Tuple<long, int>[] source2 = new Tuple<long, int>[0];
    ColumnConfiguration extendedProperty1 = this._entranceObjTypeColIndx >= 0 ? (ColumnConfiguration) this._data.Columns[this._entranceObjTypeColIndx].ExtendedProperties[(object) Consts.ColumnPropName] : (ColumnConfiguration) null;
    List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
    foreach (DataRowView dataRowView in defaultView)
    {
      if (!this._task.IsProcessStoped)
      {
        DataRow row = dataRowView.Row;
        string attrValue = this._entranceColIndx != -1 ? Convert.ToString(row[this._entranceColIndx]) : string.Empty;
        if (this._entranceColIndx != -1 && this._parentObjId == 0L && string.IsNullOrEmpty(attrValue))
        {
          this._task.Value = (object) (Convert.ToInt32(this._task.Value) + 1);
        }
        else
        {
          try
          {
            int objTypeId = this._entranceObjTypeColIndx >= 0 ? this.GetObjectTypeId(objectTypesList, extendedProperty1, Convert.ToString(row[this._entranceObjTypeColIndx])) : -1;
            if (attrValue != str1)
            {
              if (dbObject1 != null & flag && dbObject1.CheckoutBy == session.UserID)
                dbObject1.CheckIn();
              long objectID = this._entranceColIndx == -1 ? this._parentObjId : (!string.IsNullOrEmpty(attrValue) ? (objTypeId == -1 ? this.FindObjByAttrValueUniversal(session, num, attrValue) : this.FindObjByAttrValue(session, num, attrValue, objTypeId)) : this._parentObjId);
              if (objectID == 0L)
              {
                dbObject1 = (IDBObject) null;
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_261"), (object) attrValue));
              }
              dbObject1 = session.GetObject(objectID, true);
              flag = false;
              source2 = DataImporter.GetObjAllRelationComposition(session, service, dbObject1.ObjectID);
              source1 = MetaDataHelper.GetObjectTypeApplicabilities(dbObject1.ObjectType);
              if (source1.Count == 0)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_262"), (object) dbObject1.NameInMessages));
            }
            if (dbObject1 != null)
            {
              string attributesString = this.GetObjectAttributesString(row);
              long objectID;
              if (!this._objectDict.TryGetValue(attributesString, out objectID))
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_263"), (object) MetaDataHelper.GetAttributeTypeName(num), (object) attributesString));
              IDBObject dbObject2 = session.GetObject(objectID, true);
              int childObjType = dbObject2.ObjectType;
              IMSApplicability imsApplicability;
              if (this._relTypeColIndx == -1)
              {
                imsApplicability = source1.FirstOrDefault<IMSApplicability>((System.Func<IMSApplicability, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(childObjType, x.ChildObjectTypeID) && x.Options.HasFlag((Enum) ApplicabilityOptions.DefaultRelation)));
              }
              else
              {
                ColumnConfiguration extendedProperty2 = (ColumnConfiguration) this._data.Columns[this._relTypeColIndx].ExtendedProperties[(object) Consts.ColumnPropName];
                int relationTypeId = this.GetRelationTypeId(relationTypesList, extendedProperty2, row[this._relTypeColIndx].ToString());
                if (relationTypeId == -1)
                  throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_264"), row[this._relTypeColIndx]));
                imsApplicability = source1.FirstOrDefault<IMSApplicability>((System.Func<IMSApplicability, bool>) (x => MetaDataHelper.IsObjectTypeChildOf(childObjType, x.ChildObjectTypeID) && x.RelationTypeID == relationTypeId));
              }
              if (imsApplicability == null)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_265"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
              if (dbObject1.ReadOnly && imsApplicability.IsContent)
              {
                switch (dbObject1.ObjectModifyMode)
                {
                  case ObjectModifyModes.Checkout:
                    if (dbObject1.CheckoutBy != 0L && dbObject1.CheckoutBy != session.UserID)
                      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_268"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages, (object) session.GetObject(dbObject1.CheckoutBy).Caption));
                    if (dbObject1.CheckoutBy == 0L)
                    {
                      dbObject1 = dbObject1.CheckOut();
                      flag = true;
                      break;
                    }
                    break;
                  case ObjectModifyModes.CreateVersion:
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_267"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
                  case ObjectModifyModes.CantModify:
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_266"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
                }
              }
              int relationTypeId1 = imsApplicability.RelationTypeID;
              if (((IEnumerable<Tuple<long, int>>) source2).Contains<Tuple<long, int>>(new Tuple<long, int>(objectID, relationTypeId1)))
                throw new RelationAlreadyExists(dbObject1.ObjectID, dbObject2.ObjectID, relationTypeId1, dbObject1.NameInMessages, dbObject2.NameInMessages, MetaDataHelper.GetRelationTypeName(relationTypeId1));
              IDBRelation dbRelation = session.GetRelationCollection(relationTypeId1).Create(dbObject1.ObjectID, dbObject2.ObjectID);
              string str2 = this.SetAttributeValues((IDBAttributable) dbRelation, (IDBAttributableType) session.GetRelationType(relationTypeId1), (IEnumerable<DataColumn>) this._relationAttributesColumns, row);
              if (str2 != string.Empty)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_269"), (object) dbRelation.RelationID, (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages, (object) str2));
            }
          }
          catch (RelationAlreadyExists ex1)
          {
            if (!this._skipRelationExistErrs)
              relations.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex1.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
            if (this._relationUpdateAttributesColumns.Count > 0)
            {
              try
              {
                IDBRelation relation = session.GetRelation(ex1.ParentObjId, ex1.ChildObjId, ex1.RelTypeId, true);
                string str3 = this.UpdateAttributeValues((IDBAttributable) relation, (IDBAttributableType) session.GetRelationType(relation.RelationType), (IEnumerable<DataColumn>) this._relationUpdateAttributesColumns, row);
                if (str3 != string.Empty)
                  relations.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) str3, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
              }
              catch (Exception ex2)
              {
                relations.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex2.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
              }
            }
          }
          catch (Exception ex)
          {
            relations.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_260"), (object) ex.Message, (object) this.GetRowIndex(row), (object) this.GetRowNameInMessages(row)));
          }
          finally
          {
            this._task.Value = (object) (Convert.ToInt32(this._task.Value) + 1);
            str1 = attrValue;
          }
        }
      }
      else
        break;
    }
    if (dbObject1 != null & flag && dbObject1.CheckoutBy == session.UserID)
      dbObject1.CheckIn();
    return relations;
  }

  private static Tuple<long, int>[] GetObjAllRelationComposition(
    IUserSession session,
    ICompositionLoadService compositionLoadService,
    long parentObjId)
  {
    List<ObjInfoItem> itemInfoList = SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) new long[1]
    {
      parentObjId
    });
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) itemInfoList, session);
    DataTable dataTable = compositionLoadService.LoadComplexCompositions((object) session.SessionGUID, (IEnumerable<ObjInfoItem>) itemInfoList, (IEnumerable<int>) MetaDataHelper.GetRelationTypesList().Select<IMSRelationType, int>((System.Func<IMSRelationType, int>) (relType => relType.RelationTypeID)).ToList<int>(), (IEnumerable<int>) null, (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -23)
    }, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e0-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
    return dataTable != null ? dataTable.Rows.Cast<DataRow>().Select<DataRow, Tuple<long, int>>((System.Func<DataRow, Tuple<long, int>>) (row => new Tuple<long, int>(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])))).ToArray<Tuple<long, int>>() : new Tuple<long, int>[0];
  }

  private int GetObjectTypeId(
    List<IMSObjectType> objTypeList,
    ColumnConfiguration objTypeSettings,
    string rowValue)
  {
    if (objTypeSettings.ValueKind == SettingItemValueKind.Constant)
    {
      int result;
      if (!int.TryParse(rowValue, out result))
        result = -1;
      return result;
    }
    switch (objTypeSettings.DataType)
    {
      case SettingItemDataType.TypeName:
        return (objTypeList.FirstOrDefault<IMSObjectType>((System.Func<IMSObjectType, bool>) (x => string.Equals(x.ObjectName, rowValue, StringComparison.CurrentCultureIgnoreCase))) ?? throw new Exception("Не удалось определить тип объекта по имени объекта = " + rowValue)).ObjectTypeID;
      case SettingItemDataType.TypeId:
        int result1;
        if (!int.TryParse(rowValue, out result1))
          result1 = -1;
        return result1;
      case SettingItemDataType.TypeGuid:
        return MetaDataHelper.GetObjectTypeID(rowValue);
      default:
        return -1;
    }
  }

  private int GetRelationTypeId(
    List<IMSRelationType> relTypeList,
    ColumnConfiguration relTypeSettings,
    string rowValue)
  {
    if (relTypeSettings.ValueKind == SettingItemValueKind.Constant)
    {
      int result;
      if (!int.TryParse(rowValue, out result))
        result = -1;
      return result;
    }
    switch (relTypeSettings.DataType)
    {
      case SettingItemDataType.TypeName:
        IMSRelationType imsRelationType = relTypeList.FirstOrDefault<IMSRelationType>((System.Func<IMSRelationType, bool>) (x => x.Description == rowValue));
        return imsRelationType == null ? -1 : imsRelationType.RelationTypeID;
      case SettingItemDataType.TypeId:
        int result1;
        if (!int.TryParse(rowValue, out result1))
          result1 = -1;
        return result1;
      case SettingItemDataType.TypeGuid:
        return MetaDataHelper.GetRelationTypeID(rowValue);
      default:
        return -1;
    }
  }

  private static (int, string) GenHashValueForHierarchy(int objType, string attrValue)
  {
    return (objType, attrValue);
  }

  private long FindObjByAttrValue(
    IUserSession session,
    int attrId,
    string attrValue,
    int objTypeId)
  {
    long objByAttrValue1;
    if (this._hierarchyDict.TryGetValue(DataImporter.GenHashValueForHierarchy(objTypeId, attrValue), out objByAttrValue1))
      return objByAttrValue1;
    IDBObjectCollection objectCollection = session.GetObjectCollection(objTypeId);
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(attrId, RelationalOperators.Equal, (object) attrValue, LogicalOperators.NONE, 0, false)
    };
    object[] objArray = new object[1]{ (object) -2 };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, objArray, objArray, new SortOrders[1]
    {
      SortOrders.DESC
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    long objByAttrValue2;
    if (dataTable.Rows.Count != 0)
    {
      objByAttrValue2 = Convert.ToInt64(dataTable.Rows[0][0]);
      this._hierarchyDict.Add(DataImporter.GenHashValueForHierarchy(objTypeId, attrValue), objByAttrValue2);
    }
    else
      objByAttrValue2 = 0L;
    return objByAttrValue2;
  }

  private long FindObjByAttrValueUniversal(IUserSession session, int attrId, string attrValue)
  {
    long attrValueUniversal;
    if (!this._hierarchyDict.TryGetValue(DataImporter.GenHashValueForHierarchy(-1, attrValue), out attrValueUniversal))
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(attrId, RelationalOperators.Equal, (object) attrValue, LogicalOperators.NONE, 0, false)
      };
      object[] objArray = new object[2]
      {
        (object) -2,
        (object) -7
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, objArray, objArray, new SortOrders[2]
      {
        SortOrders.DESC,
        SortOrders.DESC
      });
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable.Rows.Count != 0)
      {
        attrValueUniversal = Convert.ToInt64(dataTable.Rows[0][0]);
        this._hierarchyDict.Add(DataImporter.GenHashValueForHierarchy(-1, attrValue), attrValueUniversal);
      }
    }
    if (attrValueUniversal == 0L)
    {
      foreach (int objTypeId in MetaDataHelper.GetAllAttributes4ObjectTypeList(attrId).Select<IMSAttribute4ObjectType, List<int>>((System.Func<IMSAttribute4ObjectType, List<int>>) (objType => MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objType.ObjectTypeID))).SelectMany<List<int>, int>((System.Func<List<int>, IEnumerable<int>>) (childs => (IEnumerable<int>) childs)).ToList<int>())
      {
        attrValueUniversal = this.FindObjByAttrValue(session, attrId, attrValue, objTypeId);
        if (attrValueUniversal != 0L)
          return attrValueUniversal;
      }
    }
    return attrValueUniversal;
  }

  private string SetAttributeValues(
    IDBAttributable dbAttributable,
    IDBAttributableType dbAttributableType,
    IEnumerable<DataColumn> attrCols,
    DataRow row)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataColumn attrCol in attrCols)
    {
      int num = this._data.Columns.IndexOf(attrCol);
      ColumnConfiguration extendedProperty = (ColumnConfiguration) this._data.Columns[num].ExtendedProperties[(object) Consts.ColumnPropName];
      if (extendedProperty != null)
      {
        int typeId = extendedProperty.TypeId;
        if (dbAttributableType.Attributes.GetAttributeByID(typeId, false) != null || dbAttributableType.AnyAttributes)
        {
          string str = this.SetAttributeValue(dbAttributable.Attributes.AddAttribute(typeId, false), row[num]);
          if (str != string.Empty)
            stringBuilder.AppendLine(str);
        }
        else
          stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_270"), (object) MetaDataHelper.GetAttributeTypeName(typeId)));
      }
    }
    return stringBuilder.ToString();
  }

  private string SetAttributeValue(IDBAttribute aAttribute, object aValue)
  {
    string str1 = string.Empty;
    try
    {
      switch (aAttribute.AttributeType.AttributeType)
      {
        case FieldTypes.ftString:
          aAttribute.Value = (object) Convert.ToString(aValue);
          break;
        case FieldTypes.ftInteger:
          long result1;
          if (long.TryParse(Convert.ToString(aValue), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
          {
            aAttribute.Value = (object) result1;
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_244"), (object) Convert.ToString(aValue));
          break;
        case FieldTypes.ftDouble:
          double result2;
          if (double.TryParse(Convert.ToString(aValue), NumberStyles.Any, (IFormatProvider) CultureInfo.InstalledUICulture.NumberFormat, out result2))
          {
            aAttribute.Value = (object) result2;
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_241"), (object) Convert.ToString(aValue));
          break;
        case FieldTypes.ftDateTime:
          DateTime result3;
          if (DateTime.TryParse(Convert.ToString(aValue), out result3))
          {
            aAttribute.Value = (object) result3;
            break;
          }
          double result4;
          if (double.TryParse(Convert.ToString(aValue), out result4))
          {
            result3 = DateTime.FromOADate(result4);
            aAttribute.Value = (object) result3;
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_240"), (object) Convert.ToString(aValue));
          break;
        case FieldTypes.ftFile:
          string str2 = Convert.ToString(aValue);
          if (File.Exists(str2))
          {
            using (FileStream aSourceStream = File.OpenRead(str2))
            {
              BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, new FileInfo(str2).Name, ArcMethods.ZLibPacked, string.Empty);
              new BlobProcWriter(aAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              break;
            }
          }
          if (Directory.Exists(str2))
          {
            DirectoryInfo directoryInfo = new DirectoryInfo(str2);
            int num = 0;
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
              if (num > 0)
                num = aAttribute.AddValue((object) null);
              aAttribute.Index = num;
              using (FileStream aSourceStream = File.OpenRead(file.FullName))
              {
                BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, file.Name, ArcMethods.ZLibPacked, string.Empty);
                new BlobProcWriter(aAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
                ++num;
              }
            }
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_242"), (object) Convert.ToString(aValue));
          break;
        case FieldTypes.ftMemo:
          aAttribute.Value = (object) Convert.ToString(aValue);
          break;
        case FieldTypes.ftBoolean:
          bool result5;
          if (bool.TryParse(Convert.ToString(aValue), out result5))
          {
            aAttribute.Value = (object) result5;
            break;
          }
          int result6;
          if (int.TryParse(Convert.ToString(aValue), out result6))
          {
            result5 = Convert.ToBoolean(result6);
            aAttribute.Value = (object) result5;
            break;
          }
          if (Convert.ToString(aValue).ToUpper() == "ДА")
          {
            aAttribute.Value = (object) true;
            break;
          }
          if (Convert.ToString(aValue).ToUpper() == "НЕТ")
          {
            aAttribute.Value = (object) false;
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_239"), (object) Convert.ToString(aValue));
          break;
        case FieldTypes.ftMeasured:
          try
          {
            aAttribute.Value = (object) Convert.ToString(aValue);
            break;
          }
          catch (Exception ex)
          {
            str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_245") + " {1}", (object) Convert.ToString(aValue), (object) ex.Message);
            break;
          }
        case FieldTypes.ftGuid:
          Guid result7;
          if (Guid.TryParse(Convert.ToString(aValue), out result7))
          {
            aAttribute.Value = (object) result7;
            break;
          }
          str1 = string.Format(LocalizationHolder.rm.GetString("Tools.Client_243"), (object) Convert.ToString(aValue));
          break;
      }
    }
    catch (Exception ex)
    {
      str1 = ex.Message;
    }
    return str1;
  }

  private string UpdateAttributeValues(
    IDBAttributable dbAttributable,
    IDBAttributableType dbAttributableType,
    IEnumerable<DataColumn> attrCols,
    DataRow row)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataColumn attrCol in attrCols)
    {
      int num = this._data.Columns.IndexOf(attrCol);
      int typeId = ((ColumnConfiguration) this._data.Columns[num].ExtendedProperties[(object) Consts.ColumnPropName]).TypeId;
      if (dbAttributableType.Attributes.GetAttributeByID(typeId, false) != null || dbAttributableType.AnyAttributes)
      {
        string str = this.SetAttributeValue(dbAttributable.Attributes.AddAttribute(typeId, false), row[num]);
        if (str != string.Empty)
          stringBuilder.AppendLine(str);
      }
      else
        stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Tools.Client_270"), (object) MetaDataHelper.GetAttributeTypeName(typeId)));
    }
    return stringBuilder.ToString();
  }

  private int GetRowIndex(DataRow row)
  {
    if (this._data.Rows.IndexOf(row) == -1)
      return -1;
    return !this._skipFirstRow ? this._data.Rows.IndexOf(row) + 1 : this._data.Rows.IndexOf(row) + 2;
  }

  private string GetRowNameInMessages(DataRow row)
  {
    return string.Join(" | ", ((IEnumerable<object>) row.ItemArray).Select<object, string>((System.Func<object, string>) (x => x.ToString())));
  }

  private string GetObjectAttributesString(DataRow row, string separator = "|")
  {
    List<string> list = this._objectAttributesColumns.Select<DataColumn, string>((System.Func<DataColumn, string>) (x => Convert.ToString(row[x]))).ToList<string>();
    list.Add(Convert.ToString(row[this._objTypeColIndx]));
    return string.Join(separator, (IEnumerable<string>) list);
  }
}
