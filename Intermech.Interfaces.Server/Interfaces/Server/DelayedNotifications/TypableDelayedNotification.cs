// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.TypableDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.Localization;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public abstract class TypableDelayedNotification : DelayedNotification
{
  private int _TypeID;
  private long _InstanceID;

  public TypableDelayedNotification(
    long userID,
    ActionType notificationType,
    long instanceID,
    int typeID)
    : base(userID, notificationType)
  {
    this._InstanceID = instanceID;
    this._TypeID = typeID;
  }

  public int TypeID => this._TypeID;

  public long InstanceID => this._InstanceID;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return base.IsSuitableForSettings(settings, session) && (this.NotificationType.ToString() == ActionType.GetAccess.ToString() && (this._InstanceID == 0L || this._TypeID == -1) || this.IsTypeIdIsInFilterTypes(settings.FilterTypes));
  }

  private bool IsTypeIdIsInFilterTypes(List<int> filterTypes)
  {
    if (filterTypes == null || filterTypes.Count == 0)
      return false;
    if (this.NotificationType == ActionType.AddLink || this.NotificationType == ActionType.DeleteLink)
    {
      foreach (int filterType in filterTypes)
      {
        if (this._TypeID == filterType)
          return true;
      }
    }
    foreach (int filterType in filterTypes)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(this._TypeID, filterType))
        return true;
    }
    return false;
  }

  public abstract bool CheckInitiatorAttrsWithFormula(FormulaForAttribute formulaForAttribute);

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    this.SubstUser(ref message, session);
  }

  private void SubstUser(ref string message, IUserSession session)
  {
    if (this._InstanceID == 0L)
    {
      message = message.Replace(AutoNotificationMessageHelper.User, LocalizationHolder.rm.GetString("Interfaces.Workflow_41"));
    }
    else
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(this.UserID);
      message = message.Replace(AutoNotificationMessageHelper.User, objectInfo.Caption);
      message = message.Replace("..", ".");
    }
  }

  protected virtual void SubstObjectCaption(
    ref string message,
    IUserSession session,
    bool isForEmail)
  {
    if (this._InstanceID == 0L)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(this._InstanceID);
    string newValue = DataSetProcessor.QString(objectInfo.Caption);
    IDBObjectType objectType = session.GetObjectType(objectInfo.ObjectTypeID);
    if (!isForEmail)
      newValue = string.Format(AutoNotificationMessageHelper.ObjectLink, (object) objectInfo.VersionGuid, (object) objectType.ObjectInstanceName, (object) newValue);
    message = message.Replace(AutoNotificationMessageHelper.AttrCaption, newValue);
  }
}
