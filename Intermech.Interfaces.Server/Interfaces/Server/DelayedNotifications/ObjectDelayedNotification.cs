// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.ObjectDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Workflow.AutoNotification;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class ObjectDelayedNotification : AttributableDelayedNotification
{
  private long _ID;
  private string _Caption;
  private int _LevelID;
  private int _VersionID;

  public ObjectDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    long id,
    string caption,
    int levelID,
    int versionID)
    : base(userID, notificationType, oldValues, newValues, instanceID, typeID)
  {
    this._Caption = caption;
    this._ID = id;
    this._LevelID = levelID;
    this._VersionID = versionID;
  }

  public int VersionID => this._VersionID;

  public long ObjectID => this.InstanceID;

  public int ObjectTypeID => this.TypeID;

  public long ID => this._ID;

  public string Caption => this._Caption;

  public int LevelID => this._LevelID;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    if (!base.IsSuitableForSettings(settings, session))
      return false;
    switch (settings.NotifEventType)
    {
      case NotificationEventType.Create:
        if (this._VersionID == 0)
          return true;
        break;
      case NotificationEventType.CreateVersion:
        if (this._VersionID != 0)
          return true;
        break;
      default:
        return true;
    }
    return false;
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
    this.SubstObjectCaption(ref message, session, isForEmail);
    this.SubstObjParentCaption(ref message, session, isForEmail);
    this.SubstLCLevel(ref message, session);
  }

  private void SubstObjParentCaption(ref string message, IUserSession session, bool isForEmail)
  {
    if (!message.Contains(AutoNotificationMessageHelper.ParentVersionCaption))
      return;
    long parentVersionId = session.GetObject(this.InstanceID, false).ParentVersionID;
    QuickObjectInfo objectInfo = session.GetObjectInfo(parentVersionId);
    IDBObjectType objectType = session.GetObjectType(objectInfo.ObjectTypeID);
    string str = DataSetProcessor.QString(objectInfo.Caption);
    string newValue = isForEmail ? $"{objectType.ObjectInstanceName} {str}" : string.Format(AutoNotificationMessageHelper.ObjectLink, (object) objectInfo.VersionGuid, (object) objectType.ObjectInstanceName, (object) str);
    message = message.Replace(AutoNotificationMessageHelper.ParentVersionCaption, newValue);
  }

  protected override void SubstObjectCaption(
    ref string message,
    IUserSession session,
    bool isForEmail)
  {
    if (this.InstanceID == 0L)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(this.InstanceID);
    IDBObjectType objectType = session.GetObjectType(this.TypeID);
    string str = DataSetProcessor.QString(this._Caption);
    string newValue;
    if (!objectInfo.Empty)
    {
      newValue = DataSetProcessor.QString(objectInfo.Caption);
      if (!isForEmail)
        newValue = string.Format(AutoNotificationMessageHelper.ObjectLink, (object) objectInfo.VersionGuid, (object) objectType.ObjectInstanceName, (object) newValue);
    }
    else
      newValue = $"{objectType.ObjectInstanceName} {str}";
    message = message.Replace(AutoNotificationMessageHelper.AttrCaption, newValue);
  }

  private void SubstLCLevel(ref string message, IUserSession session)
  {
    if (!message.Contains(AutoNotificationMessageHelper.LCLevel))
      return;
    string levelName = session.GetLifecycleLevel(this._LevelID).LevelName;
    message = message.Replace(AutoNotificationMessageHelper.LCLevel, levelName);
  }
}
