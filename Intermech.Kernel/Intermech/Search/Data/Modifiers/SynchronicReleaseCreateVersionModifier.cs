// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Modifiers.SynchronicReleaseCreateVersionModifier
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Search.Configuration;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Search.Data.Modifiers;

public sealed class SynchronicReleaseCreateVersionModifier
{
  private UserSession _userSession;
  private IPairedObjectsCreatorService _pairedObjectsService;
  [ThreadStatic]
  private static bool _disabled;

  public SynchronicReleaseCreateVersionModifier(
    UserSession userSession,
    IPairedObjectsCreatorService pairedObjectsService)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (pairedObjectsService == null)
      throw new ArgumentNullException(nameof (pairedObjectsService));
    this._userSession = userSession;
    this._pairedObjectsService = pairedObjectsService;
  }

  public void Apply(IDBObject @object, IDBObject prototype)
  {
    if (@object == null)
      throw new ArgumentNullException("@object");
    if (prototype == null)
      throw new ArgumentNullException(nameof (prototype));
    if (SynchronicReleaseCreateVersionModifier._disabled || !this.IsSynchronicReleaseInSoftConcretizationModeEnabled())
      return;
    if (this.IsProduct(@object))
    {
      this.Apply4Product(@object, prototype);
    }
    else
    {
      if (!this.IsDocument(@object))
        return;
      this.Apply4Document(@object, prototype);
    }
  }

  private bool IsSynchronicReleaseInSoftConcretizationModeEnabled()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.Configurations.ReadBool(ConfigurationOptionKeys.Versions_SyncReleaseInSoftMode.Module, ConfigurationOptionKeys.Versions_SyncReleaseInSoftMode.Section, ConfigurationOptionKeys.Versions_SyncReleaseInSoftMode.Name, false, DBConfigMode.GlobalOnly);
  }

  private void Apply4Product(IDBObject @object, IDBObject prototype)
  {
    foreach (IDBRelation relation in this.GetRelationsDown(@object))
    {
      long versionIdInComposition = this.GetVersionIDInComposition(relation);
      if (this.IsDocument(versionIdInComposition))
      {
        IDBObject version = this.CreateVersion(versionIdInComposition);
        this.SetVersionIDInComposition(relation, version.ObjectID);
      }
    }
  }

  private void Apply4Document(IDBObject @object, IDBObject prototype)
  {
    List<long> longList = new List<long>();
    foreach (IDBRelation relation in this.GetRelationsUp(@object))
    {
      if (this.GetVersionIDInComposition(relation) == Math.Abs(prototype.ObjectID))
      {
        IDBObject object1 = this._userSession.GetObject(relation.ProjID, false);
        if (object1 != null && this.IsProduct(object1) && !longList.Contains(object1.ID))
        {
          longList.Add(object1.ID);
          this.SetVersionIDInComposition(this._userSession.GetRelation(this.CreateVersion(object1.ObjectID).ObjectID, prototype.ID, false), @object.ObjectID);
        }
      }
    }
  }

  private List<IDBRelation> GetRelationsDown(IDBObject @object)
  {
    IDbManager dataManager = this._userSession.DataManager;
    return this.CreateRelations(dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PROJ_ID = :p1", dataManager.Parameter("p1", (object) @object.ObjectID)));
  }

  private List<IDBRelation> GetRelationsUp(IDBObject @object)
  {
    IDbManager dataManager = this._userSession.DataManager;
    return this.CreateRelations(dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PART_ID = :p1", dataManager.Parameter("p1", (object) @object.ID)));
  }

  private List<IDBRelation> CreateRelations(DataTable dataTable)
  {
    List<IDBRelation> relations = new List<IDBRelation>();
    int index = 0;
    for (int count = dataTable.Rows.Count; index < count; ++index)
    {
      IDBRelation relation = this._userSession.GetRelation(dataTable, index);
      relations.Add(relation);
    }
    return relations;
  }

  private long GetVersionIDInComposition(IDBRelation relation)
  {
    IDBAttribute attributeById = relation.GetAttributeByID(Constants.VersionIDInCompositionAttributeTypeID);
    return attributeById == null ? 0L : Math.Abs(attributeById.AsInteger);
  }

  private void SetVersionIDInComposition(IDBRelation relation, long versionID)
  {
    relation.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(Constants.VersionIDInCompositionAttributeTypeID, (object) Math.Abs(versionID))
    });
  }

  private IDBObject CreateVersion(long versionID)
  {
    SynchronicReleaseCreateVersionModifier._disabled = true;
    try
    {
      return this._pairedObjectsService.FindCreatedVersion((IUserSession) this._userSession, versionID) ?? this._userSession.GetObjectCollection(this._userSession.GetObject(versionID).TypeID).CreateVersion(versionID);
    }
    finally
    {
      SynchronicReleaseCreateVersionModifier._disabled = false;
    }
  }

  private bool IsDocument(IDBObject @object)
  {
    return MetaDataHelper.IsObjectTypeChildOf(@object.ObjectType, Constants.DocumentObjectTypeID);
  }

  private bool IsDocument(long objectVersionID)
  {
    IDBObject dbObject = this._userSession.GetObject(Math.Abs(objectVersionID), false);
    return dbObject != null && MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Constants.DocumentObjectTypeID);
  }

  private bool IsProduct(IDBObject @object)
  {
    return MetaDataHelper.IsObjectTypeChildOf(@object.ObjectType, Constants.ProductObjectTypeID);
  }
}
