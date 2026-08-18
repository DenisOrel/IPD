// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseClearDisplayTablesCacheService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseClearDisplayTablesCacheService : ImbaseEventsSupportBaseService
{
  private Dictionary<long, IMSLifeCycleStep> _obj2LCStepBefore = new Dictionary<long, IMSLifeCycleStep>();
  private int _typeUser = -1;
  private int _typeRole = -1;

  public ImbaseClearDisplayTablesCacheService()
  {
    this._typeUser = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    this._typeRole = MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545");
  }

  protected override void DoDeleteRelationHandler(IDBRelation sender, IUserSession session)
  {
  }

  protected override void DoBeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null)
      return;
    int objectType = sender.ObjectType;
    if (objectType != this._typeUser && objectType != this._typeRole && objectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID && objectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
      return;
    this._obj2LCStepBefore.Remove(sender.ObjectID);
    this._obj2LCStepBefore.Add(sender.ObjectID, MetaDataHelper.GetLCStep(sender.LCStep));
  }

  protected override void DoAfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null)
      return;
    int objectType = sender.ObjectType;
    if (objectType != this._typeUser && objectType != this._typeRole && objectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID && objectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
      return;
    IMSLifeCycleStep imsLifeCycleStep = (IMSLifeCycleStep) null;
    if (!this._obj2LCStepBefore.TryGetValue(sender.ObjectID, out imsLifeCycleStep))
      return;
    try
    {
      if (imsLifeCycleStep == null || nextstep.LevelID != session.IdentHelper.DeletedID || !(session.GetCustomService(typeof (ITablesDisplayService)) is ITablesDisplayService customService))
        return;
      Guid objectGuid = sender.ObjectGUID;
      if (objectType == this._typeUser)
        customService.RemoveSettingsForUser(new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          objectGuid
        }));
      else if (objectType == this._typeRole)
        customService.RemoveSettingsForRole(new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          objectGuid
        }));
      else
        customService.RemoveSettingsForObject(new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          objectGuid
        }));
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
  }

  protected override void DoDeleteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeDeleteEventArgs args)
  {
  }
}
