// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBEditingContextsObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel;

public class DBEditingContextsObject : 
  DBObject,
  IDBEditingContextsObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  internal static TimeSpan syncDelta = new TimeSpan(0, 5, 0);
  internal static IDBEditingContextsServerService contextService;
  private long _prevLinkedID;
  private List<EditingContextsObjectVersion> _contextObjects;

  public DBEditingContextsObject(UserSession uSession, DataTable objectParams)
    : base(uSession, objectParams)
  {
    if (DBEditingContextsObject.contextService == null)
      DBEditingContextsObject.contextService = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
    this.CheckContextObject();
  }

  protected override void DoBeforeCommitCreation()
  {
    this.AfterAddContextInfo();
    base.DoBeforeCommitCreation();
  }

  protected override void DoCommitCreation() => base.DoCommitCreation();

  protected override void DoDelete()
  {
    if (this.ObjectID > 0L)
      this.Clear(true);
    base.DoDelete();
  }

  protected override void DoPurge(long DeleteMode)
  {
    if (this.ObjectID > 0L)
    {
      this.Clear(true);
    }
    else
    {
      List<long> ecoComposiotions = DBEditingContextsObject.contextService.GetDeltaECOComposiotions((object) this.UserSession, this.ObjectID);
      DBEditingContextsObject.contextService.ForceClearModificationGroupID((object) this.UserSession, ecoComposiotions, true);
    }
    base.DoPurge(DeleteMode);
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545"))
    {
      this._prevLinkedID = DataSetProcessor.GetInt64Value(attribute.Value, 0L);
      long linkedContextNumber = Math.Abs(DataSetProcessor.GetInt64Value(newValue, 0L));
      List<long> linkedContexts = DBEditingContextsObject.contextService.GetLinkedContexts((object) this.Session, linkedContextNumber);
      if (linkedContexts != null && linkedContexts.Count > 0 && linkedContexts.IndexOf(-Math.Abs(this._prevLinkedID)) < 0 && linkedContexts.IndexOf(Math.Abs(this._prevLinkedID)) < 0)
      {
        EditingContextsObjectContainer contextsObjectContainer = this.GetEditingContextsObjectContainer(false, false);
        EditingContextsObjectContainer editingContextsObject = DBEditingContextsObject.contextService.GetEditingContextsObject((object) this.Session, linkedContexts[0], false, false);
        DBEditingContextsObject.contextService.CanLinkContexts((object) this.Session, contextsObjectContainer, editingContextsObject, true);
      }
      this._contextObjects = DBObject.EditingContextsServerService.SelectContextInfo(this.ContextID, this._prevLinkedID, this.Session);
    }
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545");
    if (attribute.AttributeID == attributeTypeId)
    {
      long newModificationID = DataSetProcessor.GetInt64Value(attribute.Value, 0L);
      if (this._prevLinkedID != 0L && newModificationID != this._prevLinkedID)
        newModificationID = Math.Abs(newModificationID);
      if (newModificationID == 0L)
        newModificationID = this.ObjectID;
      if (this._contextObjects != null && this._contextObjects.Count > 0)
      {
        for (int index = 0; index < this._contextObjects.Count; ++index)
        {
          if (this.Session.GetObject(this._contextObjects[index].F_OBJECT_ID, false) is DBObject dbObject && dbObject.ModificationID == this._prevLinkedID)
            dbObject.ModificationID = newModificationID;
        }
      }
      DBEditingContextsObject.contextService.Replace_ModificationID_IMS_VERSIONS_CONTEXT((object) this.Session, this.ContextID, newModificationID, true);
      this._prevLinkedID = newModificationID;
      this._contextObjects = (List<EditingContextsObjectVersion>) null;
      DBObject.EditingContextsServerService.UpdateModificationInCache(Math.Abs(this.ObjectID), this.LinkedContextNumber);
    }
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }

  private void AfterAddContextInfo()
  {
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid == null || DataSetProcessor.GetInt64Value(attributeByGuid.Value, 0L) != 0L)
      return;
    attributeByGuid.Value = (object) Math.Abs(this.ObjectID);
  }

  public void CheckContextObject()
  {
  }

  public virtual long ContextID
  {
    [DebuggerStepThrough] get => this.ObjectID;
  }

  public virtual long LinkedContextNumber
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid == null)
        return Math.Abs(this.ObjectID);
      long int64Value = DataSetProcessor.GetInt64Value(attributeByGuid.Value, 0L);
      return int64Value == 0L ? Math.Abs(this.ObjectID) : Math.Abs(int64Value);
    }
    set
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid == null)
        return;
      value = Math.Abs(value);
      if (value == 0L)
        value = Math.Abs(this.ObjectID);
      List<long> linkedContexts = DBEditingContextsObject.contextService.GetLinkedContexts((object) this.Session, value);
      if (linkedContexts != null && linkedContexts.Count > 0 && linkedContexts.IndexOf(-Math.Abs(this.ObjectID)) < 0 && linkedContexts.IndexOf(Math.Abs(this.ObjectID)) < 0)
      {
        EditingContextsObjectContainer contextsObjectContainer = this.GetEditingContextsObjectContainer(false, false);
        EditingContextsObjectContainer editingContextsObject = DBEditingContextsObject.contextService.GetEditingContextsObject((object) this.Session, linkedContexts[0], false, false);
        DBEditingContextsObject.contextService.CanLinkContexts((object) this.Session, editingContextsObject, contextsObjectContainer, true);
      }
      attributeByGuid.Value = (object) Math.Abs(value);
    }
  }

  public virtual bool SimpleContext => MetaDataHelper.IsSimpleEditingContext(this.ObjectType);

  public virtual EditingContextsObjectContainer GetEditingContextsObjectContainer(
    bool withDescriptions,
    bool useCache)
  {
    return DBEditingContextsObject.contextService.GetEditingContextsObject((object) this.UserSession, this.ContextID, withDescriptions, useCache);
  }

  public virtual List<EditingContextsObjectVersion> GetObjectsID(bool includeLinked)
  {
    return this.GetObjectsID(includeLinked, true);
  }

  private List<EditingContextsObjectVersion> GetObjectsID(bool includeLinked, bool useCache)
  {
    List<EditingContextsObjectVersion> objectsId = new List<EditingContextsObjectVersion>();
    if (DBEditingContextsObject.contextService == null)
      return objectsId;
    EditingContextsObjectContainer contextsObjectContainer = this.GetEditingContextsObjectContainer(false, useCache);
    long contextId = this.ContextID;
    foreach (EditingContextsObjectVersion contextsObjectVersion in contextsObjectContainer.Objects)
    {
      if (Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(contextId) || includeLinked)
        objectsId.Add(contextsObjectVersion);
    }
    return objectsId;
  }

  public virtual bool AddVersionID(long fID, long versionID, bool exceptIfFail)
  {
    return DBEditingContextsObject.contextService.AddToContext((object) this.UserSession, this.ContextID, this.LinkedContextNumber, fID, versionID, true, exceptIfFail);
  }

  public virtual bool DeleteFromContext(long versionID, bool exceptIfFail, bool clearModifiationID)
  {
    return DBEditingContextsObject.contextService.DeleteFromContext((object) this.UserSession, this.ContextID, versionID, exceptIfFail, clearModifiationID);
  }

  public virtual bool DeleteObjectFromContext(long fID, bool exceptIfFail, bool clearModifiationID)
  {
    return DBEditingContextsObject.contextService.DeleteObjectFromContext((object) this.UserSession, this.ContextID, fID, exceptIfFail, clearModifiationID);
  }

  public virtual void Clear(bool exceptIfFail)
  {
    if (this.ContextID <= 0L)
      return;
    DBEditingContextsObject.contextService.ReleaseContextObjects((object) this.UserSession, this.ContextID, exceptIfFail);
    DBEditingContextsObject.contextService.ClearContext((object) this.UserSession, this.ContextID, exceptIfFail);
  }

  public virtual EditingContextsObjectVersion FindObjectByVersionID(
    long versionID,
    bool checkLinked)
  {
    return this.FindObjectByVersionID(versionID, checkLinked, true);
  }

  private EditingContextsObjectVersion FindObjectByVersionID(
    long versionID,
    bool checkLinked,
    bool useCache)
  {
    if (versionID == 0L)
      return (EditingContextsObjectVersion) null;
    List<EditingContextsObjectVersion> objectsId = this.GetObjectsID(checkLinked, useCache);
    for (int index = 0; index < objectsId.Count; ++index)
    {
      if (Math.Abs(objectsId[index].F_OBJECT_ID) == Math.Abs(versionID))
        return objectsId[index];
    }
    return (EditingContextsObjectVersion) null;
  }

  public EditingContextsObjectVersion FindObjectByID(long fID, bool checkLinked)
  {
    return this.FindObjectByID(fID, checkLinked, true);
  }

  public EditingContextsObjectVersion FindObjectByID(long fID, bool checkLinked, bool useCache)
  {
    if (fID == 0L)
      return (EditingContextsObjectVersion) null;
    List<EditingContextsObjectVersion> objectsId = this.GetObjectsID(checkLinked, useCache);
    for (int index = 0; index < objectsId.Count; ++index)
    {
      if (objectsId[index].F_ID == fID)
        return objectsId[index];
    }
    return (EditingContextsObjectVersion) null;
  }

  public virtual bool ExistsVersionID(long versionID, bool checkLinked)
  {
    return this.ExistsVersionID(versionID, checkLinked, true);
  }

  private bool ExistsVersionID(long objectVersionID, bool checkLinked, bool useCache)
  {
    return this.FindObjectByVersionID(objectVersionID, checkLinked, useCache) != null;
  }

  public virtual bool ExistsObject(long fID, bool checkLinked)
  {
    return this.ExistsObject(fID, checkLinked, true);
  }

  public bool ExistsObject(long fID, bool checkLinked, bool useCache)
  {
    return this.FindObjectByID(fID, checkLinked, useCache) != null;
  }

  public virtual bool ReplaceVersionID(
    long oldVersionID,
    long newfID,
    long newVersionID,
    bool exceptIfFail)
  {
    if (this.ExistsVersionID(newVersionID, false, false))
      return false;
    this.DeleteObjectFromContext(newfID, exceptIfFail, true);
    return this.AddVersionID(newfID, newVersionID, exceptIfFail);
  }

  public virtual void ResetCache()
  {
  }

  public virtual long GetVersionContextID(
    List<EditingContextsObjectVersion> versions,
    long versionID,
    long userID)
  {
    if (versions == null || versions.Count == 0 || versionID == 0L || userID == 0L)
      return 0;
    for (int index = 0; index < versions.Count; ++index)
    {
      EditingContextsObjectVersion version = versions[index];
      if (Math.Abs(version.F_OBJECT_ID) == Math.Abs(versionID))
        return version.F_CONTEXT_ID;
    }
    return 0;
  }
}
