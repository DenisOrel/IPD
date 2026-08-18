// Decompiled with JetBrains decompiler
// Type: Intermech.Search.AutoConcretization.AutoConcretizationServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Search.Concretization;
using System;


namespace Intermech.Search.AutoConcretization;

public sealed class AutoConcretizationServerModule
{
  private ICustomServices _customServices;
  private IEventLogHelper _eventLogHelper;
  private IConcretizationServerService _concretizationServerService;
  private AutoConcretizationServerService _autoConcretizationServerService = new AutoConcretizationServerService();

  public AutoConcretizationServerModule(
    ICustomServices customServices,
    IEventLogHelper eventLogHelper,
    IConcretizationServerService concretizationServerService)
  {
    if (customServices == null)
      throw new ArgumentNullException(nameof (customServices));
    if (eventLogHelper == null)
      throw new ArgumentNullException(nameof (eventLogHelper));
    if (concretizationServerService == null)
      throw new ArgumentNullException(nameof (concretizationServerService));
    this._customServices = customServices;
    this._eventLogHelper = eventLogHelper;
    this._concretizationServerService = concretizationServerService;
  }

  public void Load()
  {
    this._customServices.AddService(typeof (IAutoConcretizationServerService), (object) this._autoConcretizationServerService);
    this._eventLogHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(this.EventLogHelper_AfterCreateRelationExEvent);
  }

  public void Unload()
  {
    this._customServices.RemoveService(typeof (IAutoConcretizationServerService));
    this._eventLogHelper.AfterCreateRelationExEvent -= new CreateRelationExHandler(this.EventLogHelper_AfterCreateRelationExEvent);
  }

  private void EventLogHelper_AfterCreateRelationExEvent(
    IDBRelation sender,
    IUserSession session,
    int assignMode)
  {
    try
    {
      IDBObject projObject = session.GetObject(sender.ProjID, false);
      if (projObject == null || !AutoConcretizationHelper.IsCompositionAutoConcretizationAttributeExists(projObject.ObjectType) || !this._autoConcretizationServerService.IsAutoConcretizationEnabled(session.SessionGUID, projObject))
        return;
      this._concretizationServerService.SetObjectVersionIDInComposition(session.SessionGUID, new Tuple<long, long>[1]
      {
        new Tuple<long, long>(sender.RelationID, sender.PartObjectID)
      });
    }
    catch
    {
    }
  }
}
