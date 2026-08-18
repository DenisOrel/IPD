// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailNodeID
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

internal class MailNodeID : NodeID
{
  private RecipStatus _recipStatus;
  private SenderStatus _senderStatus;
  private ActivityStatus _activityStatus = ActivityStatus.Executed;
  private DateTime _completedTerm = DateTime.MinValue;
  private long _processID;

  public MailNodeID(
    int objTypeId,
    long objId,
    long id,
    long checkedOutBy,
    int lcStepID,
    string caption,
    long recipStatus,
    long senderStatus,
    ActivityStatus status,
    DateTime completedTerm,
    long processID)
    : base(new CreateObjectNodeParams(objTypeId, objId, id, checkedOutBy, -1L, lcStepID, caption, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L))
  {
    this._recipStatus = (RecipStatus) recipStatus;
    this._senderStatus = (SenderStatus) senderStatus;
    this._activityStatus = status;
    this._completedTerm = completedTerm;
    this._processID = processID;
  }

  public SenderStatus SenderStatus => this._senderStatus;

  public RecipStatus RecipStatus
  {
    get => this._recipStatus;
    set
    {
      if (this.RecipStatus == value)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
          if (dbObject == null)
            return;
          this._recipStatus = value;
          dbObject.Attributes.AddAttribute(wfConsts.AttrRecipStatusID, false, new object[1]
          {
            (object) (int) value
          });
          if (dbObject is IActivity activity1)
          {
            activity1.Changed(ActivityChanged.UnreadStatus, (object) (int) value);
          }
          else
          {
            if (dbObject.TypeID != wfConsts.WorkOfferTypeID)
              return;
            IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrActivityID);
            if (attributeById == null)
              return;
            long asInteger = attributeById.AsInteger;
            if (!(sessionKeeper.Session.GetObject(asInteger, false) is IActivity activity) || !activity.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
              return;
            activity.Changed(ActivityChanged.UnreadStatus, (object) (int) value);
          }
        }
        catch (Exception ex)
        {
          if (!(ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service))
            return;
          service.WriteString("Ошибки", $"При задании статуса письма с идентификатором '{this.ObjectID}' произошла ошибка: {ex.Message}");
        }
      }
    }
  }

  public ActivityStatus ActivityStatus => this._activityStatus;

  public DateTime CompletedTerm => this._completedTerm;

  public long ProcessID => this._processID;
}
