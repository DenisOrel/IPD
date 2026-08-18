// Decompiled with JetBrains decompiler
// Type: Intermech.Search.ContextMenus.ContextMenuServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuServerService : LongLifeObject, IContextMenuServerService
{
  public ContextMenu FindContextMenu(Guid userSessionGuid, long contextMenuVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID) ? this.FindContextMenu(contextMenuVersionID) : throw new ArgumentException();
  }

  public Dictionary<long, ContextMenu> FindContextMenus(
    Guid userSessionGuid,
    long[] contextMenuVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return contextMenuVersionIds != null && contextMenuVersionIds.Length != 0 && !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) contextMenuVersionIds) ? this.FindContextMenus(contextMenuVersionIds) : throw new ArgumentException();
  }

  public void SaveContextMenu(
    Guid userSessionGuid,
    long contextMenuVersionID,
    ContextMenu contextMenu)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID))
        throw new ArgumentException();
      if (contextMenu == null)
        throw new ArgumentNullException(nameof (contextMenu));
      this.SaveContextMenu(contextMenuVersionID, contextMenu);
    }
  }

  public void AddContextMenusToObjectComposition(
    Guid userSessionGuid,
    long[] contextMenuVersionIds,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (contextMenuVersionIds == null || contextMenuVersionIds.Length == 0 || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) contextMenuVersionIds))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.AddContextMenusToObjectComposition(contextMenuVersionIds, objectVersionID);
    }
  }

  public void RemoveContextMenuFromObjectComposition(
    Guid userSessionGuid,
    long contextMenuVersionID,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.RemoveContextMenuFromObjectComposition(contextMenuVersionID, objectVersionID);
    }
  }

  public Dictionary<int, Tuple<long, ContextMenu>> GetContextMenuByObjectTypeDictionary(
    Guid userSessionGUID)
  {
    if (userSessionGUID == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGUID))
      return this.GetContextMenuByObjectTypeDictionary();
  }

  private ContextMenu FindContextMenu(long contextMenuVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(contextMenuVersionID, false);
      if (dbObject == null)
        return (ContextMenu) null;
      IDBAttribute byId = dbObject.Attributes.FindByID(ContextMenuConstants.SettingsBlobAttributeTypeID);
      if (byId == null || this.IsBlobEmpty(byId))
        byId = dbObject.Attributes.FindByID(ContextMenuConstants.SettingsAttributeTypeID);
      if (byId == null || this.IsBlobEmpty(byId))
        return (ContextMenu) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BlobProcReader(byId, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return memoryStream.Length == 0L ? (ContextMenu) null : new BinaryFormatter().Deserialize((Stream) memoryStream) as ContextMenu;
      }
    }
  }

  private bool IsBlobEmpty(IDBAttribute dbAttribute)
  {
    return (dbAttribute as IBlobReader).OpenBlob(-1).RealFileSize == 0L;
  }

  private Dictionary<long, ContextMenu> FindContextMenus(long[] contextMenuVersionIds)
  {
    Dictionary<long, ContextMenu> contextMenus = new Dictionary<long, ContextMenu>();
    foreach (long num in ((IEnumerable<long>) contextMenuVersionIds).Distinct<long>())
      contextMenus[num] = this.FindContextMenu(num);
    return contextMenus;
  }

  private void SaveContextMenu(long contextMenuVersionID, ContextMenu contextMenu)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) memoryStream, (object) contextMenu);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      BlobInformation aBlobInformation = new BlobInformation()
      {
        ArcMethod = ArcMethods.ZLibPacked,
        FileName = string.Empty,
        ModifyDate = DateTime.Now,
        Note = string.Empty,
        RealFileSize = memoryStream.Length
      };
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        new BlobProcWriter(sessionKeeper.Session.GetObject(contextMenuVersionID).Attributes.FindByID(ContextMenuConstants.SettingsBlobAttributeTypeID), 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
    }
  }

  private void AddContextMenusToObjectComposition(
    long[] contextMenuVersionIds,
    long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(ContextMenuConstants.ContextMenusRelationTypeID);
      foreach (long contextMenuVersionId in contextMenuVersionIds)
        relationCollection.Create(objectVersionID, contextMenuVersionId);
    }
  }

  private void RemoveContextMenuFromObjectComposition(
    long contextMenuVersionID,
    long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(contextMenuVersionID);
      sessionKeeper.Session.GetRelation(objectVersionID, dbObject.ID)?.Delete((long) Consts.PurgeMode);
    }
  }

  private Dictionary<int, Tuple<long, ContextMenu>> GetContextMenuByObjectTypeDictionary()
  {
    Dictionary<int, long> typeIdDictionary = this.CreateContextMenuVersionIDByObjectTypeIDDictionary(this.FindContextMenusForObject(this.GetCurrentRoleConfigurationVersionID()));
    if (this.CanUserCustomiseContextMenu())
    {
      foreach (KeyValuePair<int, long> idByObjectTypeId in this.CreateContextMenuVersionIDByObjectTypeIDDictionary(this.FindContextMenusForObject(this.GetCurrentUserConfigurationVersionID())))
        typeIdDictionary[idByObjectTypeId.Key] = idByObjectTypeId.Value;
    }
    Dictionary<long, ContextMenu> dictionary = new Dictionary<long, ContextMenu>();
    foreach (long num in typeIdDictionary.Values.Distinct<long>())
      dictionary[num] = this.FindContextMenu(num);
    Dictionary<int, Tuple<long, ContextMenu>> objectTypeDictionary = new Dictionary<int, Tuple<long, ContextMenu>>();
    foreach (KeyValuePair<int, long> keyValuePair in typeIdDictionary)
      objectTypeDictionary[keyValuePair.Key] = new Tuple<long, ContextMenu>(keyValuePair.Value, dictionary[keyValuePair.Value]);
    return objectTypeDictionary;
  }

  private Dictionary<int, long> CreateContextMenuVersionIDByObjectTypeIDDictionary(
    long[] contextMenuVersionIds)
  {
    Dictionary<int, long> typeIdDictionary = new Dictionary<int, long>();
    Dictionary<long, int[]> dictionary = new Dictionary<long, int[]>();
    foreach (long contextMenuVersionId in contextMenuVersionIds)
    {
      if (!dictionary.ContainsKey(contextMenuVersionId))
      {
        int[] typesForContextMenu = this.FindObjectTypesForContextMenu(contextMenuVersionId);
        dictionary.Add(contextMenuVersionId, typesForContextMenu);
      }
    }
    List<Tuple<int, long>> source = new List<Tuple<int, long>>();
    foreach (long contextMenuVersionId in contextMenuVersionIds)
    {
      int[] numArray = (int[]) null;
      dictionary.TryGetValue(contextMenuVersionId, out numArray);
      if (numArray != null)
      {
        foreach (int num in numArray)
          source.Add(new Tuple<int, long>(num, contextMenuVersionId));
      }
    }
    foreach (Tuple<int, long> tuple in (IEnumerable<Tuple<int, long>>) source.OrderBy<Tuple<int, long>, int>((System.Func<Tuple<int, long>, int>) (o => o.Item1)))
    {
      typeIdDictionary[tuple.Item1] = tuple.Item2;
      foreach (int key in (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(tuple.Item1).OrderBy<int, int>((System.Func<int, int>) (o => o)))
        typeIdDictionary[key] = tuple.Item2;
    }
    return typeIdDictionary;
  }

  private long[] FindContextMenusForObject(long objectVersionID)
  {
    List<long> longList = new List<long>();
    if (!ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(ContextMenuConstants.ContextMenusRelationTypeID);
        DBRecordSetParams paramSet = new DBRecordSetParams()
        {
          Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          }
        };
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, objectVersionID).Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
          longList.Add(int64Value);
        }
      }
    }
    return longList.ToArray();
  }

  private long GetCurrentUserConfigurationVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DataSetProcessor.GetInt64Value(sessionKeeper.Session.GetObjectCollection(Constants.UserConfigurationObjectTypeID).Select(new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        },
        RecordCount = -1
      }).Rows[0], 0, 0L);
  }

  private bool CanUserCustomiseContextMenu()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(sessionKeeper.Session.RoleID, (object) MetaDataHelper.GetAttributeTypeID("cadd93a9-306c-11d8-b4e9-00304f19f545"), false, false);
      return objectAttribute != null && !objectAttribute.AsBoolean;
    }
  }

  private long GetCurrentRoleConfigurationVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectAttributeByID(sessionKeeper.Session.RoleID, Constants.RoleConfigurationAttributeTypeID).AsInteger;
  }

  private int[] FindObjectTypesForContextMenu(long contextMenuVersionID)
  {
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (object obj in sessionKeeper.Session.GetObject(contextMenuVersionID).GetAttributeByID(ContextMenuConstants.ObjectTypesGuidsAttributeTypeID).Values)
      {
        Guid guidValue = DataSetProcessor.GetGuidValue(obj, Guid.Empty);
        if (guidValue != Guid.Empty)
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(guidValue);
          if (!ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeId) && !intList.Contains(objectTypeId))
            intList.Add(objectTypeId);
        }
      }
    }
    return intList.ToArray();
  }
}
