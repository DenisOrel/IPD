// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingService
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Methods;
using Intermech.CompositionTracking.Server.Params;
using Intermech.CompositionTracking.Server.Services;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingService : LongLifeObject, ICompositionTrackingService
{
  private readonly ConcurrentDictionary<Guid, CompositionTrackingSession> _trackingSessions;
  private readonly CompositionTrackingSettingContainer _trackingSettings;

  private ICompositionTrackingSession CreateTrackingSession(Guid sessionGuid, out bool alreadyExist)
  {
    alreadyExist = false;
    if (sessionGuid == Guid.Empty)
      return (ICompositionTrackingSession) null;
    CompositionTrackingSession trackingSession;
    if (!this._trackingSessions.TryGetValue(sessionGuid, out trackingSession))
    {
      trackingSession = new CompositionTrackingSession(sessionGuid);
      this._trackingSessions.TryAdd(sessionGuid, trackingSession);
    }
    else
      alreadyExist = true;
    return (ICompositionTrackingSession) trackingSession;
  }

  public CompositionTrackingService()
  {
    this._trackingSettings = new CompositionTrackingSettingContainer();
    this._trackingSessions = new ConcurrentDictionary<Guid, CompositionTrackingSession>();
  }

  internal void Execute(
    CompositionTrackingBaseMethod method,
    CompositionTrackingParams trackingParams)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    if (trackingParams == null)
      throw new ArgumentNullException(nameof (trackingParams));
    if (!method.Validate(trackingParams) || !this.Settings.IsRegisteredTrackConfig((IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(-1, trackingParams.DbObject.ObjectType), true) || !this.Settings.GetConfigValues(trackingParams.DbObject.ObjectType, method.Command, out CompositionTypeSettingDataList _))
      return;
    Guid sessionGuid = trackingParams.Session.SessionGUID;
    bool flag = false;
    try
    {
      bool alreadyExist;
      if (!(this.CreateTrackingSession(sessionGuid, out alreadyExist) is CompositionTrackingSession trackingSession))
        return;
      flag = !alreadyExist;
      trackingSession.Execute(method, trackingParams);
    }
    finally
    {
      if (flag)
        CompositionTrackingServerHolder.TrackingService.DisposeTrackingSession(sessionGuid);
    }
  }

  internal void RegisterService(IServiceProvider serviceProvider)
  {
    if (serviceProvider == null)
      throw new ArgumentNullException(nameof (serviceProvider));
    ServiceUtils.GetService<ICustomServices>((object) serviceProvider, false)?.AddService(typeof (ICompositionTrackingService), (object) this);
    ServerServices.AddService(typeof (ICompositionTrackingService), (object) this);
  }

  internal void UnRegisterService(IServiceProvider serviceProvider)
  {
    ServiceUtils.GetService<ICustomServices>((object) serviceProvider, false)?.RemoveService(typeof (ICompositionTrackingService));
    ServerServices.RemoveService(typeof (ICompositionTrackingService));
  }

  public ICompositionTrackingSession CreateTrackingSession(Guid sessionGuid)
  {
    return this.CreateTrackingSession(sessionGuid, out bool _);
  }

  public void DisposeTrackingSession(Guid sessionGuid)
  {
    if (sessionGuid == Guid.Empty)
      return;
    this._trackingSessions.TryRemove(sessionGuid, out CompositionTrackingSession _);
  }

  public bool GetConfigValue(
    int objTypeId,
    int inObjTypeId,
    int relTypeId,
    out CompositionsTrackingSettings value,
    Guid sessionGuid)
  {
    return this._trackingSettings.GetConfigValue(sessionGuid, (IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(objTypeId, inObjTypeId, relTypeId), out value);
  }

  public void SetConfigValue(
    int objTypeId,
    int inObjTypeId,
    int relTypeId,
    CompositionsTrackingSettings value,
    Guid sessionGuid)
  {
    this._trackingSettings.SetConfigValue(sessionGuid, (IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(objTypeId, inObjTypeId, relTypeId), value);
  }

  public bool GetConfigValue(
    Guid sessionGuid,
    IObjectTypeApplicabilityContext objectTypeContext,
    out CompositionsTrackingSettings value)
  {
    return this.Settings.GetConfigValue(sessionGuid, objectTypeContext, out value);
  }

  public void SetConfigValue(
    Guid sessionGuid,
    IObjectTypeApplicabilityContext objectTypeContext,
    CompositionsTrackingSettings value)
  {
    this._trackingSettings.SetConfigValue(sessionGuid, objectTypeContext, value);
  }

  public void RegisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext)
  {
    this._trackingSettings.RegisterTrackConfig(objectTypeContext);
  }

  public void UnregisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext)
  {
    this._trackingSettings.UnRegisterTrackConfig(objectTypeContext);
  }

  public bool IsRegisteredTrackConfig(
    IObjectTypeApplicabilityContext objectTypeContext,
    bool inheritMode = true)
  {
    return this._trackingSettings.IsRegisteredTrackConfig(objectTypeContext, inheritMode);
  }

  internal CompositionTrackingSettingContainer Settings => this._trackingSettings;
}
