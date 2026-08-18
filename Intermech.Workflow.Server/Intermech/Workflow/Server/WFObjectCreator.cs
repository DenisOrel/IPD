// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFObjectCreator
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFObjectCreator : IDBObjectCreator
{
  private Dictionary<Guid, Type> _knownTypes = new Dictionary<Guid, Type>();

  public Dictionary<Guid, Type> KnownTypes => this._knownTypes;

  public WFObjectCreator()
  {
    this._knownTypes.Add(wfConsts.SchemesGuid, typeof (WFScheme));
    this._knownTypes.Add(wfConsts.ProcessesGuid, typeof (WFProcess));
    this._knownTypes.Add(wfConsts.LinksGuid, typeof (WFLink));
    this._knownTypes.Add(wfConsts.TaskGuid, typeof (Task));
    this._knownTypes.Add(wfConsts.StartGuid, typeof (Start));
    this._knownTypes.Add(wfConsts.ApproveGuid, typeof (Approve));
    this._knownTypes.Add(wfConsts.CondGuid, typeof (Condition));
    this._knownTypes.Add(wfConsts.CaseGuid, typeof (Case));
    this._knownTypes.Add(wfConsts.SubProcessGuid, typeof (SubProcess));
    this._knownTypes.Add(wfConsts.AbortGuid, typeof (Abort));
    this._knownTypes.Add(wfConsts.StopGuid, typeof (Stop));
    this._knownTypes.Add(wfConsts.LifeCycleGuid, typeof (LifeCycle));
    this._knownTypes.Add(wfConsts.TimerGuid, typeof (Timer));
    this._knownTypes.Add(wfConsts.RegisterGuid, typeof (Register));
    this._knownTypes.Add(wfConsts.ScriptGuid, typeof (Script));
    this._knownTypes.Add(wfConsts.RemoteSubProcessGuid, typeof (RemoteProcess));
    this._knownTypes.Add(wfConsts.MessageTypeGuid, typeof (DBMessage));
    this._knownTypes.Add(wfConsts.WorkOfferTypeGuid, typeof (DBMessage));
    this._knownTypes.Add(wfConsts.SchemeCategoriesGuid, typeof (DBSchemeCategory));
    foreach (ActivityInfo activityInfo in (List<ActivityInfo>) ActivityInfos.Items)
    {
      if (!this._knownTypes.ContainsKey(activityInfo.TypeGuid))
        this._knownTypes.Add(activityInfo.TypeGuid, typeof (WFActivity));
    }
    foreach (Guid key in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(wfConsts.MessageTypeID))
    {
      if (!this._knownTypes.ContainsKey(key))
        this._knownTypes.Add(key, typeof (DBMailObject));
    }
  }

  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    Type type = (Type) null;
    if (!this._knownTypes.TryGetValue(guid, out type))
      return (IDBObject) null;
    return Activator.CreateInstance(type, (object) (UserSession) uSession, (object) objectParams) as IDBObject;
  }
}
