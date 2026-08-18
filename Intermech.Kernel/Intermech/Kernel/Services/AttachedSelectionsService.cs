// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.AttachedSelectionsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services;

public class AttachedSelectionsService : 
  LongLifeObject,
  IAttachedSelectionsService,
  IAttachedSelectionsServerService
{
  private int _attrAttachedSelectionID;
  private int _selectionPersonTypeID;
  private int _selectionCommonTypeID;
  private int _reportPersonTypeID;
  private int _reportCommonTypeID;
  private Dictionary<long, AttachedSelectionsService.AttachedInfo> _cache;

  public AttachedSelectionsService(IUserSession session)
  {
    this._cache = new Dictionary<long, AttachedSelectionsService.AttachedInfo>();
    this.Init(session);
  }

  private void Init(IUserSession session)
  {
    try
    {
      this._attrAttachedSelectionID = session.GetAttributeType(new Guid("cadd920c-306c-11d8-b4e9-00304f19f545")).AttributeID;
      this._reportCommonTypeID = session.GetObjectType(new Guid("cad00289-306c-11d8-b4e9-00304f19f545")).ObjectType;
      this._reportPersonTypeID = session.GetObjectType(new Guid("cad0028a-306c-11d8-b4e9-00304f19f545")).ObjectType;
      this._selectionCommonTypeID = session.GetObjectType(new Guid("cad00122-306c-11d8-b4e9-00304f19f545")).ObjectType;
      this._selectionPersonTypeID = session.GetObjectType(new Guid("cad00123-306c-11d8-b4e9-00304f19f545")).ObjectType;
    }
    catch
    {
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1137"));
    }
  }

  private void AddValuesToCache(IUserSession session, int categoryID, long objectID)
  {
    IDBObject dbObject = session.GetObject(objectID);
    IDBAttribute attributeById = dbObject.GetAttributeByID(this._attrAttachedSelectionID);
    List<long> selectionIds = new List<long>(attributeById.ValuesCount);
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById.AsInteger != 0L)
        selectionIds.Add(attributeById.AsInteger);
    }
    if (selectionIds.Count <= 0)
      return;
    this._cache.Add(dbObject.ObjectID, new AttachedSelectionsService.AttachedInfo(dbObject.ObjectType, selectionIds));
  }

  public AttachedSelObjectInfo[] GetObjectsForSelection(
    long selectionID,
    params int[] objectTypeIDs)
  {
    if (objectTypeIDs == null)
      return (AttachedSelObjectInfo[]) null;
    List<AttachedSelObjectInfo> attachedSelObjectInfoList = new List<AttachedSelObjectInfo>();
    foreach (KeyValuePair<long, AttachedSelectionsService.AttachedInfo> keyValuePair in this._cache)
    {
      if (Array.IndexOf<int>(objectTypeIDs, keyValuePair.Value.ObjectTypeID) >= 0 && keyValuePair.Value.SelectionIDs.Contains(selectionID))
        attachedSelObjectInfoList.Add(new AttachedSelObjectInfo(keyValuePair.Key, keyValuePair.Value.ObjectTypeID));
    }
    return attachedSelObjectInfoList.Count > 0 ? attachedSelObjectInfoList.ToArray() : (AttachedSelObjectInfo[]) null;
  }

  public void OnDeleteObject(long objectID)
  {
    if (!this._cache.ContainsKey(objectID))
      return;
    this._cache.Remove(objectID);
  }

  public void OnDeleteSelection(long reportID, long selectionID)
  {
    AttachedSelectionsService.AttachedInfo attachedInfo;
    if (!this._cache.TryGetValue(reportID, out attachedInfo) || !attachedInfo.SelectionIDs.Contains(selectionID))
      return;
    attachedInfo.SelectionIDs.Remove(selectionID);
  }

  public void OnSetSelections(IDBObject obj, object[] selectionIDs)
  {
    long key = Math.Abs(obj.ObjectID);
    AttachedSelectionsService.AttachedInfo attachedInfo;
    if (!this._cache.TryGetValue(key, out attachedInfo))
    {
      attachedInfo = new AttachedSelectionsService.AttachedInfo(obj.ObjectType, new List<long>());
      this._cache.Add(key, attachedInfo);
    }
    else
      attachedInfo.SelectionIDs = new List<long>(selectionIDs.Length);
    for (int index = 0; index < selectionIDs.Length; ++index)
      attachedInfo.SelectionIDs.Add(Convert.ToInt64(selectionIDs[index]));
  }

  public void SetObjectsForSelection(
    Guid sessionGuid,
    long selectionID,
    int[] objectTypeIDs,
    AttachedSelObjectInfo[] objects)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (objects == null)
    {
      List<long> longList = new List<long>();
      foreach (KeyValuePair<long, AttachedSelectionsService.AttachedInfo> keyValuePair in this._cache)
      {
        if (Array.IndexOf<int>(objectTypeIDs, keyValuePair.Value.ObjectTypeID) >= 0 && keyValuePair.Value.SelectionIDs.Contains(selectionID))
          longList.Add(keyValuePair.Key);
      }
      for (int index1 = 0; index1 < longList.Count; ++index1)
      {
        IDBAttribute attributeById = sessionById.GetObject(longList[index1]).GetAttributeByID(this._attrAttachedSelectionID);
        if (attributeById != null && attributeById.ValuesCount > 0)
        {
          for (int index2 = 0; index2 < attributeById.ValuesCount; ++index2)
          {
            attributeById.Index = index2;
            if (!attributeById.IsNull && attributeById.AsInteger == selectionID)
            {
              if (index2 == 0 && attributeById.ValuesCount == 1)
              {
                attributeById.Clear();
                break;
              }
              attributeById.DeleteValue();
              break;
            }
          }
        }
      }
      longList.Clear();
    }
    else
    {
      for (int index = 0; index < objects.Length; ++index)
      {
        IDBObject dbObject = sessionById.GetObject(objects[index].ObjectID);
        AttachedSelectionsService.AttachedInfo attachedInfo;
        if (!this._cache.TryGetValue(objects[index].ObjectID, out attachedInfo))
          dbObject.Attributes.AddAttribute(this._attrAttachedSelectionID, false, new object[1]
          {
            (object) selectionID
          });
        else if (!attachedInfo.SelectionIDs.Contains(selectionID))
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(this._attrAttachedSelectionID);
          if (attributeById.IsNull)
            attributeById.Value = (object) selectionID;
          else
            attributeById.AddValue((object) selectionID);
        }
      }
    }
  }

  public void ExcludeObjects(Guid sessionGuid, long selectionID, long[] objectIDs)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    for (int index1 = 0; index1 < objectIDs.Length; ++index1)
    {
      AttachedSelectionsService.AttachedInfo attachedInfo;
      if (this._cache.TryGetValue(objectIDs[index1], out attachedInfo) && attachedInfo.SelectionIDs.Contains(selectionID))
      {
        IDBAttribute attributeById = sessionById.GetObject(objectIDs[index1]).GetAttributeByID(this._attrAttachedSelectionID);
        if (attributeById != null && attributeById.ValuesCount > 0)
        {
          for (int index2 = 0; index2 < attributeById.ValuesCount; ++index2)
          {
            attributeById.Index = index2;
            if (!attributeById.IsNull && attributeById.AsInteger == selectionID)
            {
              if (index2 == 0 && attributeById.ValuesCount == 1)
              {
                attributeById.Clear();
                break;
              }
              attributeById.DeleteValue();
              break;
            }
          }
        }
      }
    }
  }

  public void RegisterCategory(IUserSession session, int objectTypeID)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(this._attrAttachedSelectionID, RelationalOperators.AttributeExists, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(this._attrAttachedSelectionID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new object[1]{ (object) -2 });
    DataTable dataTable = session.GetObjectCollection(objectTypeID).Select(paramSet);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      this.AddValuesToCache(session, objectTypeID, Convert.ToInt64(dataTable.Rows[index][0]));
  }

  private class AttachedInfo
  {
    public int ObjectTypeID;
    public List<long> SelectionIDs;

    public AttachedInfo(int objectType, List<long> selectionIds)
    {
      this.ObjectTypeID = objectType;
      this.SelectionIDs = selectionIds;
    }
  }
}
