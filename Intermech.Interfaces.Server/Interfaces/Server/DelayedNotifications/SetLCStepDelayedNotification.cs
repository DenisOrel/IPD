// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.SetLCStepDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Workflow.AutoNotification;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class SetLCStepDelayedNotification : ObjectDelayedNotification
{
  public int NewLCStepID { get; private set; }

  public int OldLCStepID { get; private set; }

  public int OldLevelID { get; private set; }

  public SetLCStepDelayedNotification(
    long userID,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    long id,
    string caption,
    int oldLevelID,
    int newLevelID,
    int versionID,
    int oldStepID,
    int newStepID)
    : base(userID, ActionType.NextLCStep, oldValues, newValues, instanceID, typeID, id, caption, newLevelID, versionID)
  {
    this.OldLCStepID = oldStepID;
    this.NewLCStepID = newStepID;
    this.OldLevelID = oldLevelID;
  }

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    if (!base.IsSuitableForSettings(settings, session))
      return false;
    switch (settings)
    {
      case LCStepAutoNotificationSettings notificationSettings1:
        return notificationSettings1.LCStepID == this.NewLCStepID;
      case LCLevelAutoNotificationSettings notificationSettings2:
        return notificationSettings2.LCLevelID == this.LevelID;
      default:
        return false;
    }
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
    this.SubstLCStep(ref message, session);
  }

  private void SubstLCStep(ref string message, IUserSession session)
  {
    if (!message.Contains(AutoNotificationMessageHelper.LCStep))
      return;
    string lcName = session.GetLifecycleStep(this.NewLCStepID, this.TypeID).LCName;
    message = message.Replace(AutoNotificationMessageHelper.LCStep, lcName);
  }
}
