// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.MSOfficeReviewsServerModule
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

internal sealed class MSOfficeReviewsServerModule
{
  public void Load()
  {
    (ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (IMSOfficeReviewsServerService), (object) new MSOfficeReviewsServerService());
    IEventLogHelper eventLogHelper = ServiceLocator.Get<IEventLogHelper>();
    if (eventLogHelper == null)
      return;
    eventLogHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(this.EventLogHelper_AfterCreateRelationExEvent);
    eventLogHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.EventLogHelper_AfterNextLCStepEvent);
  }

  public void Unload()
  {
    (ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).RemoveService(typeof (IMSOfficeReviewsServerService));
    IEventLogHelper eventLogHelper = ServiceLocator.Get<IEventLogHelper>();
    if (eventLogHelper == null)
      return;
    eventLogHelper.AfterCreateRelationExEvent -= new CreateRelationExHandler(this.EventLogHelper_AfterCreateRelationExEvent);
    eventLogHelper.AfterNextLCStepEvent -= new NextLCStepHandler(this.EventLogHelper_AfterNextLCStepEvent);
  }

  private void EventLogHelper_AfterCreateRelationExEvent(
    IDBRelation sender,
    IUserSession session,
    int assignMode)
  {
    try
    {
      if (sender.RelationType != MSOfficeReviewsConstants.ReviewsRelationTypeID)
        return;
      (session.GetCustomService(typeof (IMSOfficeReviewsServerService)) as IMSOfficeReviewsServerService).ReplaceOrRemoveReviewForNewDocumentVersion(session.SessionGUID, sender.ProjID, sender.PartID);
    }
    catch (Exception ex)
    {
      Trace.Write((object) ex);
    }
  }

  private void EventLogHelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    try
    {
      if (!((IEnumerable<int>) MSOfficeReviewsConstants.DocumentObjectTypesIds).Contains<int>(sender.ObjectType) || !(session.GetCustomService(typeof (IRedliningService)) is IRedliningService customService) || !customService.DeleteFiles)
        return;
      IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(sender.LCStep);
      if (lcStep == null || lcStep.LevelID != customService.LevelID)
        return;
      (session.GetCustomService(typeof (IMSOfficeReviewsServerService)) as IMSOfficeReviewsServerService).RemoveAllReviewsForDocument(session.SessionGUID, sender.ObjectID);
    }
    catch (Exception ex)
    {
      Trace.Write((object) ex);
    }
  }
}
