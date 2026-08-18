// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseObjInfoService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Collections;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseObjInfoService : ImbaseEventsSupportBaseService, IImbaseObjInfoService
{
  private int _fldCtlObjectId;
  private int _fldCtlCreateType;
  private int _fldCtlPath;
  private int _fldCmObjectId;
  private int _fldCmCreateType;
  private int _fldCmCreateMode;
  private int _fldCmClassiff;
  private bool _typesLoaded;
  private bool _modesLoaded;
  private int _imbaseCreatedObjectAttId;
  private int _imbaseCreateNewObjectAttId;
  internal Dictionary<long, IMSLifeCycleStep> _obj2LCStepBefore = new Dictionary<long, IMSLifeCycleStep>();
  internal DataTable _creationModes;
  internal DataTable _creationTypes;
  internal Dictionary<int, List<ImbaseObjInfoService.ImbaseTreeCacheRec>> _imbaseCreationTypes;
  internal Dictionary<int, List<ImbaseObjInfoService.ImbaseTreeCacheRec>> _imbaseCreationModes;

  private void InializeData()
  {
    this._imbaseCreatedObjectAttId = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreatedObjectAttGUID);
    this._imbaseCreateNewObjectAttId = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
  }

  public bool GetCreationTypes(IUserSession session, out List<int> objTypeIds)
  {
    if (!this.LoadCreationTypes(false, session))
    {
      objTypeIds = new List<int>();
      return false;
    }
    objTypeIds = new List<int>((IEnumerable<int>) this._imbaseCreationTypes.Keys);
    return objTypeIds != null;
  }

  private ICollection<ImClassiff> GetImbaseClassiffListForType(
    int objTypeId,
    bool checkTypeInCache,
    IUserSession session)
  {
    HashSet<ImClassiff> classiffListForType = new HashSet<ImClassiff>();
    if (objTypeId == 0)
      return (ICollection<ImClassiff>) classiffListForType;
    if (!this.LoadCreationTypes(false, session))
      return (ICollection<ImClassiff>) classiffListForType;
    if (checkTypeInCache && !this._imbaseCreationTypes.ContainsKey(objTypeId))
      return (ICollection<ImClassiff>) classiffListForType;
    foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in this._imbaseCreationTypes[objTypeId])
    {
      if (imbaseTreeCacheRec != null && imbaseTreeCacheRec.ObjectId != 0L && imbaseTreeCacheRec.Tag != null)
      {
        string str = imbaseTreeCacheRec.Tag.ToString();
        if (!str.Equals(string.Empty))
          classiffListForType.Add(new ImClassiff(imbaseTreeCacheRec.ObjectId, str));
      }
    }
    return (ICollection<ImClassiff>) classiffListForType;
  }

  internal bool LoadCreationTypes(bool forceMode, Guid sessionGuid)
  {
    IUserSession sessionById = !sessionGuid.Equals(Guid.Empty) ? UserSession.GetSessionByID(sessionGuid) : (IUserSession) null;
    return sessionById != null && this.LoadCreationTypes(forceMode, sessionById);
  }

  internal bool LoadCreationTypes(bool forceMode, IUserSession session)
  {
    if (this._typesLoaded && !forceMode)
      return true;
    lock (this._imbaseCreationTypes)
    {
      this._typesLoaded = false;
      this._imbaseCreationTypes.Clear();
      if (session == null)
        return false;
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      conditionStructureList.Add(new ConditionStructure(this._imbaseCreatedObjectAttId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false));
      List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
      columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptorList.Add(new ColumnDescriptor((object) this._imbaseCreatedObjectAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      this._fldCtlObjectId = 0;
      this._fldCtlCreateType = 1;
      this._fldCtlPath = 2;
      this._creationTypes = DataHelper.GetObjectData((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS, session, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) null);
      if (this._creationTypes == null)
        return false;
      this._creationTypes.CaseSensitive = true;
      HashSet<string> stringSet = new HashSet<string>();
      for (int index = this._creationTypes.Rows.Count - 1; index >= 0; --index)
      {
        string str = Convert.ToString(this._creationTypes.Rows[index][this._fldCtlPath]);
        if (string.IsNullOrEmpty(str) || stringSet.Contains(str))
          this._creationTypes.Rows.RemoveAt(index);
        else
          stringSet.Add(str);
      }
      this._creationTypes.AcceptChanges();
      this._creationTypes.PrimaryKey = new DataColumn[1]
      {
        this._creationTypes.Columns[this._fldCtlPath]
      };
      if (this._creationTypes.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._creationTypes.Rows)
        {
          long int64 = Convert.ToInt64(row[this._fldCtlObjectId]);
          string str = row[this._fldCtlCreateType].ToString();
          string tag = row[this._fldCtlPath].ToString();
          if (GuidHelper.IsGuid(str))
          {
            int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(str));
            if (objectTypeId != -1)
            {
              List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseTreeCacheRecList;
              if (!this._imbaseCreationTypes.TryGetValue(objectTypeId, out imbaseTreeCacheRecList))
              {
                imbaseTreeCacheRecList = new List<ImbaseObjInfoService.ImbaseTreeCacheRec>();
                this._imbaseCreationTypes.Add(objectTypeId, imbaseTreeCacheRecList);
              }
              imbaseTreeCacheRecList.Add(new ImbaseObjInfoService.ImbaseTreeCacheRec(int64, (ImbaseObjInfoService.ImbaseTreeCacheRec) null, (ImbaseObjInfoService.ImbaseTreeInfoRec) null, (object) tag));
            }
          }
        }
      }
      this._typesLoaded = true;
    }
    return true;
  }

  internal bool LoadCreationModes(bool forceMode, IUserSession session)
  {
    if (this._modesLoaded && !forceMode)
      return true;
    this._modesLoaded = false;
    if (session == null)
      return false;
    int classifFolderKeyAttId = Intermech.Imbase.Consts.ClassifFolderKeyAttId;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) this._imbaseCreatedObjectAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) this._imbaseCreateNewObjectAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) classifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0));
    this._fldCmObjectId = 0;
    this._fldCmCreateType = 1;
    this._fldCmCreateMode = 2;
    this._fldCmClassiff = 3;
    this._creationModes = DataHelper.GetObjectData((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS, session, (IEnumerable<ConditionStructure>) new List<ConditionStructure>()
    {
      new ConditionStructure(this._imbaseCreateNewObjectAttId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) null);
    if (this._creationModes == null)
      return false;
    this._creationModes.CaseSensitive = true;
    DataRow[] dataRowArray = this._creationModes.Select("", $"[{(object) Intermech.Imbase.Consts.ClassifFolderKeyAttId}]");
    if (dataRowArray.Length != 0)
    {
      for (int index = dataRowArray.Length - 1; index > 0; --index)
      {
        if (dataRowArray[index][this._fldCmClassiff].Equals(dataRowArray[index - 1][this._fldCmClassiff]))
          this._creationModes.Rows.Remove(dataRowArray[index]);
      }
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        if ((dataRowArray[index].RowState & DataRowState.Detached) != DataRowState.Detached)
        {
          object obj = dataRowArray[index][this._fldCmClassiff];
          if (obj == DBNull.Value || obj.ToString() == string.Empty)
            this._creationModes.Rows.Remove(dataRowArray[index]);
          else
            break;
        }
      }
      this._creationModes.AcceptChanges();
    }
    this._creationModes.PrimaryKey = new DataColumn[1]
    {
      this._creationModes.Columns[this._fldCmClassiff]
    };
    this._modesLoaded = true;
    return true;
  }

  internal bool LoadTypeCreationModes(
    int objTypeId,
    IUserSession session,
    out List<ImbaseObjCreateMode> objCreateModes)
  {
    objCreateModes = (List<ImbaseObjCreateMode>) null;
    if (objTypeId == -1 || session == null || !this.LoadCreationTypes(false, session) || !this._imbaseCreationTypes.ContainsKey(objTypeId) || !this.LoadCreationModes(false, session))
      return false;
    ICollection<ImClassiff> classiffListForType = this.GetImbaseClassiffListForType(objTypeId, false, session);
    if (classiffListForType.Count == 0)
      return false;
    HashSet<string> classifKeys = new HashSet<string>();
    foreach (ImClassiff imClassiff in (IEnumerable<ImClassiff>) classiffListForType)
      classifKeys.Add(imClassiff.Value);
    ICollection<string> bucket = (ICollection<string>) new HashSet<string>();
    ImbaseHelper.CollectAllClassificatorsCollection(bucket, (IEnumerable<string>) classifKeys);
    foreach (string str in classifKeys)
      bucket.Remove(str);
    int classifFolderKeyAttId = Intermech.Imbase.Consts.ClassifFolderKeyAttId;
    List<DataRow> dataRowList = new List<DataRow>();
    int columnIndex = this._creationModes.Columns.IndexOf(classifFolderKeyAttId.ToString());
    foreach (DataRow row in (InternalDataCollectionBase) this._creationModes.Rows)
    {
      string str = Convert.ToString(row[columnIndex]);
      if (bucket.Contains(str))
        dataRowList.Add(row);
    }
    foreach (DataRow row in (InternalDataCollectionBase) this._creationModes.Rows)
    {
      for (string str = Convert.ToString(row[columnIndex]); str.Length >= 2; str = str.Substring(0, str.Length - 2))
      {
        if (classifKeys.Contains(str))
        {
          dataRowList.Add(row);
          break;
        }
      }
    }
    objCreateModes = new List<ImbaseObjCreateMode>();
    List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseTreeCacheRecList = new List<ImbaseObjInfoService.ImbaseTreeCacheRec>();
    this._imbaseCreationModes[objTypeId] = imbaseTreeCacheRecList;
    if (dataRowList.Count == 0)
      return true;
    HashSet<string> stringSet = new HashSet<string>();
    ImClassiff imClassiff1 = new ImClassiff(0L);
    foreach (int key in this._imbaseCreationTypes.Keys)
    {
      if (key != objTypeId)
      {
        foreach (ImClassiff imClassiff2 in (IEnumerable<ImClassiff>) this.GetImbaseClassiffListForType(key, false, session))
        {
          for (imClassiff1.Value = imClassiff2.Value; imClassiff1.Value.Length >= 2; imClassiff1.Value = imClassiff1.Value.Substring(0, imClassiff1.Value.Length - 2))
          {
            if (classiffListForType.Contains(imClassiff1))
            {
              stringSet.Add(imClassiff2.Value);
              break;
            }
          }
        }
      }
    }
    HashSet<ImbaseObjCreateMode> collection = new HashSet<ImbaseObjCreateMode>();
    Dictionary<string, ImbaseObjCreateMode> dictionary = new Dictionary<string, ImbaseObjCreateMode>();
    foreach (ImClassiff imClassiff3 in (IEnumerable<ImClassiff>) classiffListForType)
      dictionary.Add(imClassiff3.Value, ImbaseObjCreateMode.iocmUseExists);
    foreach (DataRow dataRow in dataRowList)
    {
      if (dataRow != null)
      {
        string tag = dataRow[this._fldCmClassiff].ToString();
        bool flag = false;
        for (string str = tag; str.Length >= 2; str = str.Substring(0, str.Length - 2))
        {
          if (stringSet.Contains(str))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          ImbaseObjCreateMode objCreateMode = Convert.ToBoolean(dataRow[this._fldCmCreateMode]) ? ImbaseObjCreateMode.iocmCreateNew : ImbaseObjCreateMode.iocmUnknown;
          if (objCreateMode != ImbaseObjCreateMode.iocmUseExists)
          {
            for (imClassiff1.Value = tag; imClassiff1.Value.Length >= 2; imClassiff1.Value = imClassiff1.Value.Substring(0, imClassiff1.Value.Length - 2))
            {
              if (classiffListForType.Contains(imClassiff1))
              {
                dictionary[imClassiff1.Value] = objCreateMode;
                break;
              }
            }
          }
          long int64 = Convert.ToInt64(dataRow[this._fldCmObjectId]);
          string str = dataRow[this._fldCmCreateType].ToString();
          int objTypeId1 = GuidHelper.IsGuid(str) ? MetaDataHelper.GetObjectTypeID(new Guid(str)) : -1;
          ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec = new ImbaseObjInfoService.ImbaseTreeCacheRec(int64, (ImbaseObjInfoService.ImbaseTreeCacheRec) null, new ImbaseObjInfoService.ImbaseTreeInfoRec(objTypeId1, objCreateMode), (object) tag);
          imbaseTreeCacheRecList.Add(imbaseTreeCacheRec);
          collection.Add(objCreateMode);
        }
      }
    }
    foreach (ImClassiff tag in (IEnumerable<ImClassiff>) classiffListForType)
    {
      ImbaseObjCreateMode objCreateMode = dictionary[tag.Value];
      if (objCreateMode == ImbaseObjCreateMode.iocmUseExists)
      {
        ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec = new ImbaseObjInfoService.ImbaseTreeCacheRec(tag.ObjectId, (ImbaseObjInfoService.ImbaseTreeCacheRec) null, new ImbaseObjInfoService.ImbaseTreeInfoRec(objTypeId, objCreateMode), (object) tag);
        imbaseTreeCacheRecList.Add(imbaseTreeCacheRec);
        collection.Add(objCreateMode);
      }
    }
    objCreateModes.AddRange((IEnumerable<ImbaseObjCreateMode>) collection);
    return true;
  }

  internal bool LoadCreationModes(
    Dictionary<long, int> objects,
    Guid sessionGuid,
    out Dictionary<long, ImbaseObjCreateInfo> objCreateInfo)
  {
    objCreateInfo = new Dictionary<long, ImbaseObjCreateInfo>();
    if (objects == null || objects.Count == 0)
      return false;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return false;
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    foreach (KeyValuePair<long, int> keyValuePair in objects)
    {
      List<long> longList;
      if (!dictionary.TryGetValue(keyValuePair.Value, out longList))
      {
        longList = new List<long>();
        dictionary.Add(keyValuePair.Value, longList);
      }
      longList.Add(keyValuePair.Key);
      objCreateInfo.Add(keyValuePair.Key, new ImbaseObjCreateInfo(keyValuePair.Value, ImbaseObjCreateMode.iocmUseExists));
    }
    List<int> intList = new List<int>();
    foreach (int key in dictionary.Keys)
    {
      if (key != -1 && !this._imbaseCreationTypes.ContainsKey(key))
        intList.Add(key);
    }
    foreach (int key in intList)
      dictionary.Remove(key);
    if (dictionary.Count == 0)
      return true;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    int classifFolderKeyAttId = Intermech.Imbase.Consts.ClassifFolderKeyAttId;
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) this._imbaseCreatedObjectAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) this._imbaseCreateNewObjectAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) classifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0));
    int columnIndex1 = 0;
    int columnIndex2 = 1;
    int columnIndex3 = 2;
    int columnIndex4 = 3;
    List<long> objIdList = new List<long>(objects.Count);
    foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary)
      objIdList.AddRange((IEnumerable<long>) keyValuePair.Value);
    if (objIdList.Count == 0)
      return true;
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objIdList.ToArray(), LogicalOperators.NONE, 0, false)
    };
    DataTable objectData = DataHelper.GetObjectData((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS, sessionById, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) objIdList);
    if (objectData == null || objectData.Rows.Count == 0)
      return true;
    if (!this._typesLoaded)
      this.LoadCreationTypes(false, sessionGuid);
    DataTable creationTypes = this._creationTypes;
    if (creationTypes == null)
      return false;
    if (!this._modesLoaded)
      this.LoadCreationModes(false, sessionById);
    DataTable creationModes = this._creationModes;
    if (creationModes == null)
      return false;
    foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
    {
      if (row != null)
      {
        long int64 = Convert.ToInt64(row[columnIndex1]);
        string str1 = row[columnIndex4].ToString();
        int num = -1;
        if (row[columnIndex2] != DBNull.Value)
        {
          string str2 = row[columnIndex2].ToString();
          if (GuidHelper.IsGuid(str2))
            num = MetaDataHelper.GetObjectTypeID(str2);
        }
        if (num == -1 || num == 0)
        {
          foreach (string allClassificator in (IEnumerable<string>) ImbaseHelper.CollectAllClassificators(new string[1]
          {
            str1
          }))
          {
            DataRow dataRow = creationTypes.Rows.Find((object) allClassificator);
            if (dataRow != null)
            {
              string str3 = dataRow[this._fldCtlCreateType].ToString();
              if (GuidHelper.IsGuid(str3))
              {
                num = MetaDataHelper.GetObjectTypeID(str3);
                break;
              }
            }
          }
        }
        if (num != -1)
        {
          ImbaseObjCreateMode createMode = ImbaseObjCreateMode.iocmUnknown;
          if (row[columnIndex3] != DBNull.Value)
            createMode = Convert.ToBoolean(row[columnIndex3]) ? ImbaseObjCreateMode.iocmCreateNew : ImbaseObjCreateMode.iocmUseExists;
          if (createMode == ImbaseObjCreateMode.iocmUnknown)
          {
            List<ImbaseObjCreateMode> objCreateModes;
            if (this.GetCreationMode(num, sessionGuid, out objCreateModes) && objCreateModes != null)
            {
              if (objCreateModes.Count == 1)
                createMode = objCreateModes[0];
              else if (objCreateModes.Count > 1)
              {
                foreach (string allClassificator in (IEnumerable<string>) ImbaseHelper.CollectAllClassificators(new string[1]
                {
                  str1
                }))
                {
                  DataRow dataRow = creationModes.Rows.Find((object) allClassificator);
                  if (dataRow != null)
                  {
                    createMode = Convert.ToBoolean(dataRow[this._fldCmCreateMode]) ? ImbaseObjCreateMode.iocmCreateNew : ImbaseObjCreateMode.iocmUseExists;
                    break;
                  }
                }
              }
            }
            if (createMode == ImbaseObjCreateMode.iocmUnknown)
              createMode = ImbaseObjCreateMode.iocmUseExists;
          }
          objCreateInfo[int64] = new ImbaseObjCreateInfo(num, createMode);
        }
      }
    }
    return true;
  }

  internal void ClearCaches()
  {
    this.ClearTypeCaches();
    this.ClearModeCaches();
  }

  internal void ClearTypeCaches()
  {
    this._imbaseCreationTypes.Clear();
    this._typesLoaded = false;
    this._creationTypes = (DataTable) null;
  }

  internal void ClearModeCaches()
  {
    this._imbaseCreationModes.Clear();
    this._modesLoaded = false;
    this._creationModes = (DataTable) null;
  }

  internal void RemoveObjectFromCreationTypeCache(long objectId)
  {
    if (objectId == 0L || !this._typesLoaded)
      return;
    foreach (int key in this._imbaseCreationTypes.Keys)
    {
      List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove;
      if (this.RemoveObjectFromCreationTypeCache(objectId, key, out itemsToRemove) || this.RemoveObjectFromCreationModeCache(objectId, key, out itemsToRemove))
      {
        if (this._imbaseCreationModes.ContainsKey(key))
        {
          this._imbaseCreationModes.Remove(key);
          break;
        }
        break;
      }
      this.RemoveObjectFromCreationModeCache(objectId, key, out itemsToRemove);
    }
    this._creationTypes?.AcceptChanges();
    this._creationModes?.AcceptChanges();
  }

  internal bool RemoveObjectFromCreationTypeCache(
    long objectId,
    int objTypeId,
    out List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove)
  {
    return this.RemoveObjectFromCreationTypeCache(objectId, objTypeId, out itemsToRemove, out List<object[]> _, true);
  }

  internal bool RemoveObjectFromCreationTypeCache(
    long objectId,
    int objTypeId,
    out List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove,
    out List<object[]> rows2Remove,
    bool removeFromCache)
  {
    itemsToRemove = new List<ImbaseObjInfoService.ImbaseTreeCacheRec>();
    rows2Remove = (List<object[]>) null;
    List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseTreeCacheRecList;
    if (objectId == 0L || objTypeId == -1 || !this._typesLoaded || !this._imbaseCreationTypes.TryGetValue(objTypeId, out imbaseTreeCacheRecList) || imbaseTreeCacheRecList == null || imbaseTreeCacheRecList.Count == 0)
      return false;
    foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in imbaseTreeCacheRecList)
    {
      if (imbaseTreeCacheRec != null && imbaseTreeCacheRec.ObjectId == objectId)
      {
        itemsToRemove.Add(imbaseTreeCacheRec);
        break;
      }
    }
    if (removeFromCache)
    {
      foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in itemsToRemove)
        imbaseTreeCacheRecList.Remove(imbaseTreeCacheRec);
      if (imbaseTreeCacheRecList.Count == 0)
        this._imbaseCreationTypes.Remove(objTypeId);
      DataRow[] dataRowArray = this._creationTypes.Select($"[{(object) -2}] = {(object) objectId}");
      rows2Remove = new List<object[]>(dataRowArray.Length);
      foreach (DataRow row in dataRowArray)
      {
        rows2Remove.Add(row.ItemArray);
        this._creationTypes.Rows.Remove(row);
      }
    }
    if (itemsToRemove.Count > 0)
      return true;
    return rows2Remove != null && rows2Remove.Count > 0;
  }

  internal bool RemoveObjectFromCreationModeCache(
    long objectId,
    int objTypeId,
    out List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove)
  {
    return this.RemoveObjectFromCreationModeCache(objectId, objTypeId, out itemsToRemove, out List<object[]> _, true);
  }

  internal bool RemoveObjectFromCreationModeCache(
    long objectId,
    int objTypeId,
    out List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove,
    out List<object[]> rows2Remove,
    bool removeFromCache)
  {
    itemsToRemove = new List<ImbaseObjInfoService.ImbaseTreeCacheRec>();
    rows2Remove = (List<object[]>) null;
    if (objectId == 0L || objTypeId == -1 || !this._imbaseCreationModes.ContainsKey(objTypeId) || !this._modesLoaded)
      return false;
    List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseCreationMode = this._imbaseCreationModes[objTypeId];
    if (imbaseCreationMode == null || imbaseCreationMode.Count == 0)
      return false;
    foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in imbaseCreationMode)
    {
      if (imbaseTreeCacheRec != null && imbaseTreeCacheRec.ObjectId == (long) objTypeId)
      {
        itemsToRemove.Add(imbaseTreeCacheRec);
        break;
      }
    }
    if (removeFromCache)
    {
      foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in itemsToRemove)
        imbaseCreationMode.Remove(imbaseTreeCacheRec);
      DataRow[] dataRowArray = this._creationModes.Select($"[{(object) -2}] = {(object) objectId}");
      rows2Remove = new List<object[]>(dataRowArray.Length);
      foreach (DataRow row in dataRowArray)
      {
        rows2Remove.Add(row.ItemArray);
        this._creationModes.Rows.Remove(row);
      }
    }
    if (itemsToRemove.Count > 0)
      return true;
    return rows2Remove != null && rows2Remove.Count > 0;
  }

  internal bool UpdateObjectCreationTypeAttribute(
    IDBObject dbObject,
    object oldValue,
    object newValue,
    IUserSession session)
  {
    if (dbObject == null || session == null || oldValue == newValue || !this._typesLoaded)
      return false;
    int num = -1;
    if (oldValue != null)
      num = ImbaseEventsSupportBaseService.GetObjTypeId(oldValue);
    List<object[]> rows2Remove1 = (List<object[]>) null;
    List<object[]> rows2Remove2 = (List<object[]>) null;
    List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove1 = (List<ImbaseObjInfoService.ImbaseTreeCacheRec>) null;
    List<ImbaseObjInfoService.ImbaseTreeCacheRec> itemsToRemove2 = (List<ImbaseObjInfoService.ImbaseTreeCacheRec>) null;
    if (num != -1 && (this.RemoveObjectFromCreationTypeCache(dbObject.ObjectID, num, out itemsToRemove1, out rows2Remove1, true) || this.RemoveObjectFromCreationModeCache(dbObject.ObjectID, num, out itemsToRemove2, out rows2Remove2, true)) && this._imbaseCreationModes.ContainsKey(num))
      this._imbaseCreationModes.Remove(num);
    int objTypeId = ImbaseEventsSupportBaseService.GetObjTypeId(newValue);
    if (objTypeId == -1)
      return true;
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objTypeId);
    if (this._imbaseCreationModes.ContainsKey(objTypeId))
      this._imbaseCreationModes.Remove(objTypeId);
    if (itemsToRemove1 != null || rows2Remove1 != null || itemsToRemove2 != null || rows2Remove2 != null)
    {
      if (itemsToRemove1 != null)
      {
        List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseTreeCacheRecList;
        if (!this._imbaseCreationTypes.TryGetValue(objTypeId, out imbaseTreeCacheRecList))
        {
          imbaseTreeCacheRecList = new List<ImbaseObjInfoService.ImbaseTreeCacheRec>();
          this._imbaseCreationTypes.Add(objTypeId, imbaseTreeCacheRecList);
        }
        foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in itemsToRemove1)
        {
          if (imbaseTreeCacheRec.InfoRec != null)
            imbaseTreeCacheRec.InfoRec.ObjTypeID = objTypeId;
          imbaseTreeCacheRecList.Add(imbaseTreeCacheRec);
        }
      }
      if (rows2Remove1 != null)
      {
        foreach (object[] objArray in rows2Remove1)
        {
          objArray[this._fldCtlCreateType] = (object) objectTypeGuid.ToString();
          this._creationTypes.Rows.Add(objArray);
        }
        this._creationTypes.AcceptChanges();
      }
      if (rows2Remove2 != null)
      {
        foreach (object[] objArray in rows2Remove2)
        {
          objArray[this._fldCmCreateType] = (object) objectTypeGuid.ToString();
          this._creationModes.Rows.Add(objArray);
        }
        this._creationModes.AcceptChanges();
      }
    }
    else
      this.ClearCaches();
    return true;
  }

  internal bool UpdateObjectCreationModeAttribute(
    IDBObject dbObject,
    object oldValue,
    object newValue,
    IUserSession session)
  {
    if (dbObject == null || session == null || !this._modesLoaded)
      return false;
    this.ClearModeCaches();
    return true;
  }

  internal bool UpdateObjectClassiffAttribute(
    IDBObject dbObject,
    object oldValue,
    object newValue,
    IUserSession session)
  {
    if (dbObject == null || session == null)
      return false;
    this.ClearCaches();
    return true;
  }

  public ImbaseObjInfoService()
  {
    this._imbaseCreationModes = new Dictionary<int, List<ImbaseObjInfoService.ImbaseTreeCacheRec>>();
    this._imbaseCreationTypes = new Dictionary<int, List<ImbaseObjInfoService.ImbaseTreeCacheRec>>();
    this.InializeData();
  }

  public bool GetCreationTypes(Guid sessionGuid, out List<int> objTypeIds)
  {
    IUserSession sessionById = !sessionGuid.Equals(Guid.Empty) ? UserSession.GetSessionByID(sessionGuid) : (IUserSession) null;
    if (sessionById != null)
      return this.GetCreationTypes(sessionById, out objTypeIds);
    objTypeIds = new List<int>();
    return false;
  }

  public bool GetCreationMode(
    int objTypeId,
    Guid sessionGuid,
    out List<ImbaseObjCreateMode> objCreateModes)
  {
    return this.GetCreationMode(objTypeId, sessionGuid, out objCreateModes, true);
  }

  public bool GetCreationMode(
    int objTypeId,
    Guid sessionGuid,
    out List<ImbaseObjCreateMode> objCreateModes,
    bool checkApplicability)
  {
    objCreateModes = (List<ImbaseObjCreateMode>) null;
    if (sessionGuid == Guid.Empty || objTypeId == -1 || !this.LoadCreationTypes(false, sessionGuid) || checkApplicability && !this._imbaseCreationTypes.ContainsKey(objTypeId))
      return false;
    if (this._imbaseCreationModes.ContainsKey(objTypeId))
    {
      List<ImbaseObjInfoService.ImbaseTreeCacheRec> imbaseCreationMode = this._imbaseCreationModes[objTypeId];
      if (imbaseCreationMode == null)
        return false;
      objCreateModes = new List<ImbaseObjCreateMode>();
      foreach (ImbaseObjInfoService.ImbaseTreeCacheRec imbaseTreeCacheRec in imbaseCreationMode)
      {
        if (imbaseTreeCacheRec != null && imbaseTreeCacheRec.InfoRec != null && imbaseTreeCacheRec.InfoRec.ObjCreateMode != ImbaseObjCreateMode.iocmUnknown)
          CollectionUtils.AddSorted<ImbaseObjCreateMode>(objCreateModes, imbaseTreeCacheRec.InfoRec.ObjCreateMode);
      }
      if (objCreateModes.Count == 0)
        objCreateModes.Add(ImbaseObjCreateMode.iocmUseExists);
      return true;
    }
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    return sessionById != null && this.LoadTypeCreationModes(objTypeId, sessionById, out objCreateModes);
  }

  public bool GetCreationMode(
    long objectId,
    Guid sessionGuid,
    out ImbaseObjCreateInfo objCreateMode)
  {
    return this.GetCreationMode(objectId, -1, sessionGuid, out objCreateMode);
  }

  public bool GetCreationMode(
    long objectId,
    int objTypeId,
    Guid sessionGuid,
    out ImbaseObjCreateInfo objCreateInfo)
  {
    objCreateInfo = new ImbaseObjCreateInfo(objTypeId, ImbaseObjCreateMode.iocmUnknown);
    if (objectId == 0L)
      return false;
    Dictionary<long, ImbaseObjCreateInfo> objCreateInfo1;
    int num = this.GetCreationMode((IDictionary<long, int>) new Dictionary<long, int>()
    {
      {
        objectId,
        objTypeId
      }
    }, sessionGuid, out objCreateInfo1) ? 1 : 0;
    if (num == 0)
      return num != 0;
    objCreateInfo1.TryGetValue(objectId, out objCreateInfo);
    return num != 0;
  }

  public bool GetCreationMode(
    IList<long> objects,
    Guid sessionGuid,
    out Dictionary<long, ImbaseObjCreateInfo> objCreateInfo)
  {
    objCreateInfo = (Dictionary<long, ImbaseObjCreateInfo>) null;
    if (objects == null || objects.Count == 0)
      return false;
    Dictionary<long, int> objects1 = new Dictionary<long, int>();
    foreach (long key in (IEnumerable<long>) objects)
      objects1[key] = -1;
    return this.GetCreationMode((IDictionary<long, int>) objects1, sessionGuid, out objCreateInfo);
  }

  public bool GetCreationMode(
    IDictionary<long, int> objects,
    Guid sessionGuid,
    out Dictionary<long, ImbaseObjCreateInfo> objCreateInfo)
  {
    objCreateInfo = new Dictionary<long, ImbaseObjCreateInfo>();
    if (objects == null || objects.Count == 0 || !this.LoadCreationTypes(false, sessionGuid))
      return false;
    Dictionary<long, int> objects1 = new Dictionary<long, int>();
    Dictionary<int, ImbaseObjCreateMode> dictionary = new Dictionary<int, ImbaseObjCreateMode>();
    foreach (KeyValuePair<long, int> keyValuePair in (IEnumerable<KeyValuePair<long, int>>) objects)
    {
      if (keyValuePair.Key != 0L)
      {
        ImbaseObjCreateMode createMode = ImbaseObjCreateMode.iocmUnknown;
        if (keyValuePair.Value != -1)
        {
          if (dictionary.ContainsKey(keyValuePair.Value))
            createMode = dictionary[keyValuePair.Value];
          else if (!this._imbaseCreationTypes.ContainsKey(keyValuePair.Value))
          {
            createMode = ImbaseObjCreateMode.iocmCreateNew;
            dictionary.Add(keyValuePair.Value, createMode);
          }
          else
          {
            List<ImbaseObjCreateMode> objCreateModes;
            if (!this.GetCreationMode(keyValuePair.Value, sessionGuid, out objCreateModes, false))
            {
              createMode = ImbaseObjCreateMode.iocmUnknown;
              dictionary.Add(keyValuePair.Value, createMode);
            }
            else
            {
              createMode = objCreateModes == null || objCreateModes.Count != 1 ? ImbaseObjCreateMode.iocmUnknown : objCreateModes[0];
              dictionary.Add(keyValuePair.Value, createMode);
            }
          }
        }
        objCreateInfo.Add(keyValuePair.Key, new ImbaseObjCreateInfo(keyValuePair.Value, createMode));
        if (createMode == ImbaseObjCreateMode.iocmUnknown)
          objects1.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    Dictionary<long, ImbaseObjCreateInfo> objCreateInfo1;
    if (objects1.Count == 0 || !this.LoadCreationModes(objects1, sessionGuid, out objCreateInfo1))
      return true;
    foreach (KeyValuePair<long, ImbaseObjCreateInfo> keyValuePair in objCreateInfo1)
    {
      ImbaseObjCreateInfo imbaseObjCreateInfo = objCreateInfo1[keyValuePair.Key];
      if (imbaseObjCreateInfo.ObjectType == -1)
        imbaseObjCreateInfo.ObjectType = keyValuePair.Value.ObjectType;
      if (keyValuePair.Value.CreateMode != ImbaseObjCreateMode.iocmUnknown)
        imbaseObjCreateInfo.CreateMode = keyValuePair.Value.CreateMode;
      objCreateInfo[keyValuePair.Key] = imbaseObjCreateInfo;
    }
    return true;
  }

  public void RemoveImbaseObjectFromCaches(long objectId, IUserSession session)
  {
    if (objectId == 0L || session == null)
      return;
    this.RemoveObjectFromCreationTypeCache(objectId);
  }

  public void ImbaseObjectUpdateAttribute(
    IDBObject dbObject,
    int attributeId,
    object oldValue,
    object newValue,
    IUserSession session)
  {
    if (dbObject == null || attributeId == 0 || session == null)
      return;
    if (attributeId == this._imbaseCreatedObjectAttId)
      this.UpdateObjectCreationTypeAttribute(dbObject, oldValue, newValue, session);
    else if (attributeId == this._imbaseCreateNewObjectAttId)
    {
      this.UpdateObjectCreationModeAttribute(dbObject, oldValue, newValue, session);
    }
    else
    {
      if (attributeId != Intermech.Imbase.Consts.ClassifFolderKeyAttId)
        throw new Exception($"Method not implemented for attribute ID = \"{attributeId}\"");
      this.UpdateObjectClassiffAttribute(dbObject, oldValue, newValue, session);
    }
  }

  protected override void DoDeleteRelationHandler(IDBRelation sender, IUserSession session)
  {
  }

  protected override void DoBeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this._obj2LCStepBefore.Remove(sender.ObjectID);
    this._obj2LCStepBefore.Add(sender.ObjectID, MetaDataHelper.GetLCStep(sender.LCStep));
  }

  protected override void DoAfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    IMSLifeCycleStep imsLifeCycleStep;
    if (!this._obj2LCStepBefore.TryGetValue(sender.ObjectID, out imsLifeCycleStep))
      return;
    try
    {
      if (imsLifeCycleStep == null || imsLifeCycleStep.LevelID != session.IdentHelper.DeletedID && nextstep.LevelID != session.IdentHelper.DeletedID)
        return;
      if (nextstep.LevelID == session.IdentHelper.DeletedID)
        this.RemoveImbaseObjectFromCaches(sender.ObjectID, sender.Session);
      else
        this.ClearCaches();
    }
    finally
    {
      this._obj2LCStepBefore.Remove(sender.ObjectID);
    }
  }

  protected override void DoWriteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeValueEventArgs args)
  {
    if (attribute == null || attribute.Session == null || args == null || args.NewValue == args.OldValue || attribute.AttributeType == null || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute)
      return;
    IDBObject parentObject = (IDBObject) (dbAttribute.ParentObject as DBObject);
    if (parentObject != null && !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this.ImbaseObjectUpdateAttribute(parentObject, attribute.AttributeID, args.OldValue, args.Value, args.Session);
  }

  protected override void DoDeleteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeDeleteEventArgs args)
  {
    if (attribute == null || attribute.Value == null || attribute.Session == null || attribute.AttributeType == null || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute)
      return;
    IDBObject parentObject = (IDBObject) (dbAttribute.ParentObject as DBObject);
    if (parentObject != null && !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this.ImbaseObjectUpdateAttribute(parentObject, attribute.AttributeID, attribute.Value, (object) null, args.Session);
  }

  public override void AfterCacheReloadHandler(IDbManager db)
  {
    base.AfterCacheReloadHandler(db);
    this.ClearCaches();
  }

  public void SubscribeOnSystemlEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterDeleteRelationEvent += new Intermech.Interfaces.Server.DeleteRelationHandler(((ImbaseEventsSupportBaseService) this).DeleteRelationHandler);
    eventHelper.BeforeNextLCStepEvent += new NextLCStepHandler(((ImbaseEventsSupportBaseService) this).BeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent += new NextLCStepHandler(((ImbaseEventsSupportBaseService) this).AfterObjNextLCStepHandler);
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreatedObjectAttGUID);
    eventHelper.AddAttributeWriteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeTypeId1, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
    eventHelper.AddAttributeWriteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeTypeId2, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    eventHelper.AddAttributeWriteHandler((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    eventHelper.StartTransactionEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) this).StartTransaction);
    eventHelper.CommitEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) this).CommitTransaction);
    eventHelper.RollbackEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) this).RollBackTransaction);
    eventHelper.AfterCacheReload += new CacheReloadHandler(((ImbaseEventsSupportBaseService) this).AfterCacheReloadHandler);
  }

  public void UnSubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterDeleteRelationEvent -= new Intermech.Interfaces.Server.DeleteRelationHandler(((ImbaseEventsSupportBaseService) this).DeleteRelationHandler);
    eventHelper.BeforeNextLCStepEvent -= new NextLCStepHandler(((ImbaseEventsSupportBaseService) this).BeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent -= new NextLCStepHandler(((ImbaseEventsSupportBaseService) this).AfterObjNextLCStepHandler);
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreatedObjectAttGUID);
    eventHelper.RemoveAttributeWriteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeTypeId1, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
    eventHelper.RemoveAttributeWriteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeTypeId2, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    eventHelper.RemoveAttributeWriteHandler((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(((ImbaseEventsSupportBaseService) this).WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, new DeleteAttributeHandler(((ImbaseEventsSupportBaseService) this).DeleteAttributeValueHandler));
    eventHelper.StartTransactionEvent -= new TransactionHandler(((ImbaseEventsSupportBaseService) this).StartTransaction);
    eventHelper.CommitEvent -= new TransactionHandler(((ImbaseEventsSupportBaseService) this).CommitTransaction);
    eventHelper.RollbackEvent -= new TransactionHandler(((ImbaseEventsSupportBaseService) this).RollBackTransaction);
    eventHelper.AfterCacheReload -= new CacheReloadHandler(((ImbaseEventsSupportBaseService) this).AfterCacheReloadHandler);
  }

  internal class ImbaseTreeCacheRec
  {
    private readonly long _objId;
    internal ImbaseObjInfoService.ImbaseTreeCacheRec _owner;
    private ImbaseObjInfoService.ImbaseTreeInfoRec _infoRec;
    private object _tag;

    public ImbaseTreeCacheRec(long objId, ImbaseObjInfoService.ImbaseTreeCacheRec owner)
      : this(objId, owner, (ImbaseObjInfoService.ImbaseTreeInfoRec) null, (object) null)
    {
    }

    public ImbaseTreeCacheRec(
      long objId,
      ImbaseObjInfoService.ImbaseTreeCacheRec owner,
      ImbaseObjInfoService.ImbaseTreeInfoRec infoRec)
      : this(objId, owner, infoRec, (object) null)
    {
    }

    public ImbaseTreeCacheRec(
      long objId,
      ImbaseObjInfoService.ImbaseTreeCacheRec owner,
      ImbaseObjInfoService.ImbaseTreeInfoRec infoRec,
      object tag)
    {
      this._objId = objId;
      this._owner = owner;
      this._infoRec = infoRec;
      this._tag = tag;
    }

    public long ObjectId => this._objId;

    public ImbaseObjInfoService.ImbaseTreeCacheRec Owner => this._owner;

    public ImbaseObjInfoService.ImbaseTreeInfoRec InfoRec
    {
      get => this._infoRec;
      set => this._infoRec = value;
    }

    public object Tag
    {
      get => this._tag;
      set => this._tag = value;
    }
  }

  internal class ImbaseTreeInfoRec
  {
    public int ObjTypeID;
    public ImbaseObjCreateMode ObjCreateMode;

    public ImbaseTreeInfoRec()
      : this(-1, ImbaseObjCreateMode.iocmUnknown)
    {
    }

    public ImbaseTreeInfoRec(int objTypeId, ImbaseObjCreateMode objCreateMode)
    {
      this.ObjTypeID = objTypeId;
      this.ObjCreateMode = objCreateMode;
    }
  }
}
