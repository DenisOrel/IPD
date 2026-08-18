// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingSubscriber
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Methods;
using Intermech.CompositionTracking.Server.Params;
using Intermech.CompositionTracking.Server.Session;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingSubscriber : LongLifeObject
{
  private void ExecuteMethod(
    CompositionTrackingBaseMethod method,
    CompositionTrackingParams trackingParams)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    if (trackingParams == null)
      throw new ArgumentNullException(nameof (trackingParams));
    if (CompositionTrackingServerHolder.TrackingService == null)
      return;
    CompositionTrackingServerHolder.TrackingService.Execute(method, trackingParams);
  }

  internal void Activate()
  {
    CompositionTrackingServerHolder.EventLogHelper.BeforeCheckinEvent += new ObjectEventHandler(this.elhelper_BeforeCheckinEvent);
    CompositionTrackingServerHolder.EventLogHelper.BeforeNextLCStepEvent += new NextLCStepHandler(this.elhelper_BeforeNextLCStepEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterCheckoutEvent += new ObjectEventHandler(this.elhelper_AfterCheckoutEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterUndoCheckoutEvent += new ObjectEventHandler(this.elhelper_AfterUndoCheckoutEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterCheckinEvent += new ObjectEventHandler(this.elhelper_AfterCheckinEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterSaveToArcCopy += new ObjectEventHandler(this.elhelper_AfterSaveToArcCopy);
    CompositionTrackingServerHolder.EventLogHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.elhelper_AfterNextLCStepEvent);
    CompositionTrackingServerHolder.EventLogHelper.CommitEvent += new TransactionHandler(this.elhelper_CommitEvent);
    CompositionTrackingServerHolder.EventLogHelper.RollbackEvent += new TransactionHandler(this.elhelper_RollbackEvent);
    ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true).RegisterCreator((Func<PairedObjectsCreator>) (() => (PairedObjectsCreator) new CompositionPairObjectCreator(new ObjectEventHandler(this.elhelper_CreateObjectEvent))));
    CompositionTrackingServerHolder.EventLogHelper.AfterDeleteObjectTypeEvent += new DeleteObjectTypeHandler(this.elHelper_AfterDeleteObjectTypeEvent);
    if (!(CompositionTrackingServerHolder.EventLogHelper is EventLogHelper eventLogHelper))
      return;
    eventLogHelper.AfterDeleteApplicability += new RelationsApplicabilityHandler(this.elHelper_AfterDeleteApplicability);
  }

  internal void Deactivate()
  {
    CompositionTrackingServerHolder.EventLogHelper.BeforeCheckinEvent -= new ObjectEventHandler(this.elhelper_BeforeCheckinEvent);
    CompositionTrackingServerHolder.EventLogHelper.BeforeNextLCStepEvent -= new NextLCStepHandler(this.elhelper_BeforeNextLCStepEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterCheckoutEvent -= new ObjectEventHandler(this.elhelper_AfterCheckoutEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterUndoCheckoutEvent -= new ObjectEventHandler(this.elhelper_AfterUndoCheckoutEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterCheckinEvent -= new ObjectEventHandler(this.elhelper_AfterCheckinEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterSaveToArcCopy -= new ObjectEventHandler(this.elhelper_AfterSaveToArcCopy);
    CompositionTrackingServerHolder.EventLogHelper.AfterNextLCStepEvent -= new NextLCStepHandler(this.elhelper_AfterNextLCStepEvent);
    CompositionTrackingServerHolder.EventLogHelper.CommitEvent -= new TransactionHandler(this.elhelper_CommitEvent);
    CompositionTrackingServerHolder.EventLogHelper.RollbackEvent -= new TransactionHandler(this.elhelper_RollbackEvent);
    CompositionTrackingServerHolder.EventLogHelper.AfterDeleteObjectTypeEvent -= new DeleteObjectTypeHandler(this.elHelper_AfterDeleteObjectTypeEvent);
    if (!(CompositionTrackingServerHolder.EventLogHelper is EventLogHelper eventLogHelper))
      return;
    eventLogHelper.AfterDeleteApplicability -= new RelationsApplicabilityHandler(this.elHelper_AfterDeleteApplicability);
  }

  private void elhelper_BeforeCheckinEvent(IDBObject sender, IUserSession session)
  {
  }

  private void elhelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingChangeLcStepMethod(), (CompositionTrackingParams) new CompositionTrackingChangeLcStepParams(sender, nextstep));
  }

  private void elhelper_BeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    CompositionTrackingSessionData data = CompositionTrackingSessionDataHolder.GetData(session);
    if (data == null)
      return;
    data.BeforeLifeCycleSteps[sender.ObjectID] = MetaDataHelper.GetLCStep(sender.LCStep);
  }

  private void elhelper_CreateObjectEvent(IDBObject sender, IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingCreateVersionMethod(), new CompositionTrackingParams(sender));
  }

  private void elhelper_AfterSaveToArcCopy(IDBObject sender, IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingSaveToArchCopyMethod(), new CompositionTrackingParams(sender));
  }

  private void elhelper_AfterCheckinEvent(IDBObject sender, IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingCheckInMethod(), new CompositionTrackingParams(sender));
  }

  private void elhelper_AfterUndoCheckoutEvent(IDBObject sender, IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingUndoCheckoutMethod(), new CompositionTrackingParams(sender));
  }

  private void elhelper_AfterCheckoutEvent(IDBObject sender, IUserSession session)
  {
    this.ExecuteMethod((CompositionTrackingBaseMethod) new CompositionTrackingCheckoutMethod(), new CompositionTrackingParams(sender));
  }

  private void elHelper_AfterDeleteObjectTypeEvent(IDBObjectType sender, IUserSession session)
  {
    if (sender == null || session == null || CompositionTrackingServerHolder.TrackingService == null)
      return;
    CompositionTrackingServerHolder.TrackingService.Settings.ClearGarbage(sender, session);
  }

  private void elHelper_AfterDeleteApplicability(
    IUserSession session,
    RelationsApplicabilityProperties applicabilityProperties)
  {
    if (session == null || CompositionTrackingServerHolder.TrackingService == null)
      return;
    CompositionTrackingServerHolder.TrackingService.Settings.ClearGarbage(applicabilityProperties, session);
  }

  private void elhelper_RollbackEvent(IUserSession session)
  {
    CompositionTrackingSessionDataHolder.RemoveData(session);
  }

  private void elhelper_CommitEvent(IUserSession session)
  {
    CompositionTrackingSessionDataHolder.RemoveData(session);
  }
}
