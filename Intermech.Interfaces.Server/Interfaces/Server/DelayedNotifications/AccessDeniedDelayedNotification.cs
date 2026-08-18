// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.AccessDeniedDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces.Workflow.AutoNotification;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class AccessDeniedDelayedNotification : TypableDelayedNotification
{
  private ActionType _AccessType;
  private string[] _DeniedMessage;

  public AccessDeniedDelayedNotification(
    long userID,
    ActionType notificationType,
    long instanceID,
    int typeID,
    ActionType accessType,
    string[] deniedMessage)
    : base(userID, notificationType, instanceID, typeID)
  {
    this._AccessType = accessType;
    this._DeniedMessage = deniedMessage;
  }

  public ActionType AccessType => this._AccessType;

  public string[] DeniedMessage => this._DeniedMessage;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return base.IsSuitableForSettings(settings, session) && settings is AccessDeniedAutoNotificationSettings notificationSettings && notificationSettings.AccessActionType == this._AccessType;
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
    this.SubstAccessStuff(ref message, session);
  }

  public override bool CheckInitiatorAttrsWithFormula(FormulaForAttribute formulaForAttribute)
  {
    return true;
  }

  private void SubstAccessStuff(ref string message, IUserSession session)
  {
    string description = this._AccessType.GetDescription<ActionType>();
    message = message.Replace(AutoNotificationMessageHelper.AccessType, description);
    string newValue = string.Join("<br>", this._DeniedMessage);
    message = message.Replace(AutoNotificationMessageHelper.NotificationTextAccessDenied, newValue);
  }
}
