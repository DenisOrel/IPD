// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.RelationDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces.Workflow.AutoNotification;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class RelationDelayedNotification : AttributableDelayedNotification
{
  private long _ProjID;
  private long _PartID;
  private long _PartObjectID;
  private string _PartObjectCaption;

  public RelationDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    long projID,
    long partID,
    long partObjID,
    string partCaption)
    : base(userID, notificationType, oldValues, newValues, instanceID, typeID)
  {
    this._ProjID = projID;
    this._PartID = partID;
    this._PartObjectID = partObjID;
    this._PartObjectCaption = partCaption;
  }

  public string PartObjectCaption => this._PartObjectCaption;

  public long ProjID => this._ProjID;

  public long PartID => this._PartID;

  public long PartObjectID => this._PartObjectID;

  public long RelationID => this.InstanceID;

  public int RelationTypeID => this.TypeID;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return base.IsSuitableForSettings(settings, session) && settings is RelationAutoNotificationSettings relationSettings && this.CheckObjTypes(session, relationSettings);
  }

  private bool CheckObjTypes(
    IUserSession session,
    RelationAutoNotificationSettings relationSettings)
  {
    if (relationSettings.ObjectTypeIds.Count == 0)
      return true;
    QuickObjectInfo objectInfo1 = session.GetObjectInfo(this.PartObjectID);
    QuickObjectInfo objectInfo2 = session.GetObjectInfo(this.ProjID);
    if (!objectInfo1.Empty)
    {
      foreach (int objectTypeId in relationSettings.ObjectTypeIds)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objectInfo1.ObjectTypeID, objectTypeId))
          return true;
      }
    }
    else
    {
      IDBObject objectActual = session.GetObjectActual(this.PartObjectID, false);
      if (objectActual != null)
      {
        foreach (int objectTypeId in relationSettings.ObjectTypeIds)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(objectActual.ObjectType, objectTypeId))
            return true;
        }
      }
    }
    if (!objectInfo2.Empty)
    {
      foreach (int objectTypeId in relationSettings.ObjectTypeIds)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objectInfo2.ObjectTypeID, objectTypeId))
          return true;
      }
    }
    else
    {
      IDBObject objectActual = session.GetObjectActual(this.PartObjectID, false);
      if (objectActual != null)
      {
        foreach (int objectTypeId in relationSettings.ObjectTypeIds)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(objectActual.ObjectType, objectTypeId))
            return true;
        }
      }
    }
    return false;
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
    this.SubstProjCaption(ref message, session, isForEmail);
    this.SubstPartCaption(ref message, session, isForEmail);
    this.SubstRelType(ref message, session);
  }

  protected override string CreateMailSubject(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return settings.NotifEventType.GetDescription<NotificationEventType>();
  }

  private void SubstRelType(ref string message, IUserSession session)
  {
    string description = session.GetRelationType(this.TypeID).Description;
    message = message.Replace(AutoNotificationMessageHelper.RelTypeName, description);
  }

  protected void SubstPartCaption(ref string message, IUserSession session, bool isForEmail)
  {
    if (this._PartID == 0L)
      return;
    long objectID = 0;
    if (this._PartObjectID == 0L)
    {
      IDBObject objectBaseVersionById = session.GetObjectBaseVersionByID(this._PartID, false);
      if (objectBaseVersionById != null)
        objectID = objectBaseVersionById.ObjectID;
    }
    else
      objectID = this._PartObjectID;
    string newValue = DataSetProcessor.QString(this._PartObjectCaption);
    string empty = string.Empty;
    if (objectID != 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
      string objectInstanceName;
      if (!objectInfo.Empty)
      {
        newValue = DataSetProcessor.QString(objectInfo.Caption);
        objectInstanceName = session.GetObjectType(objectInfo.ObjectTypeID).ObjectInstanceName;
      }
      else
      {
        IDBObject objectActual = session.GetObjectActual(objectID, false);
        newValue = DataSetProcessor.QString(objectActual.Caption);
        objectInstanceName = session.GetObjectType(objectActual.ObjectType).ObjectInstanceName;
      }
      if (!isForEmail)
        newValue = string.Format(AutoNotificationMessageHelper.ObjectLink, (object) objectInfo.VersionGuid, (object) objectInstanceName, (object) newValue);
    }
    message = message.Replace(AutoNotificationMessageHelper.PartAttrCaption, newValue);
  }

  protected void SubstProjCaption(ref string message, IUserSession session, bool isForEmail)
  {
    if (this._ProjID == 0L)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(this._ProjID);
    string newValue = DataSetProcessor.QString(objectInfo.Caption);
    IDBObjectType objectType = session.GetObjectType(objectInfo.ObjectTypeID);
    if (!isForEmail)
      newValue = string.Format(AutoNotificationMessageHelper.ObjectLink, (object) objectInfo.VersionGuid, (object) objectType.ObjectInstanceName, (object) newValue);
    message = message.Replace(AutoNotificationMessageHelper.ProjAttrCaption, newValue);
  }
}
