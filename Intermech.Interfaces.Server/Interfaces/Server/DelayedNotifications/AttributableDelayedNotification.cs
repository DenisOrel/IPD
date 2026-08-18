// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.AttributableDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class AttributableDelayedNotification : TypableDelayedNotification
{
  private AttributeValues[] _OldValues;
  private AttributeValues[] _NewValues;

  public AttributableDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID)
    : base(userID, notificationType, instanceID, typeID)
  {
    this._NewValues = newValues;
    this._OldValues = oldValues;
  }

  public AttributeValues[] OldValues => this._OldValues;

  public AttributeValues[] NewValues => this._NewValues;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return base.IsSuitableForSettings(settings, session) && settings is AttributableAutoNotificationSettings notificationSettings && (wfConsts.SendAttrs2DelayedNotificationMode || !notificationSettings.HasActuationConditionFormula());
  }

  protected override string CreateMailSubject(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    string description = settings.NotifEventType.GetDescription<NotificationEventType>();
    if (this.InstanceID == 0L)
      return description;
    int anObjectType;
    string aValue;
    if (settings.NotifEventType == NotificationEventType.Delete)
    {
      anObjectType = (int) ((IEnumerable<AttributeValues>) this.OldValues).First<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == -7)).AsInteger;
      aValue = ((IEnumerable<AttributeValues>) this.OldValues).First<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == -50)).AsString;
    }
    else
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(this.InstanceID);
      anObjectType = objectInfo.ObjectTypeID;
      aValue = objectInfo.Caption;
    }
    string str = $" {session.GetObjectType(anObjectType).ObjectInstanceName} {DataSetProcessor.QString(aValue)}";
    return description.Insert(description.Length - 1, str);
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
  }

  public override bool CheckInitiatorAttrsWithFormula(FormulaForAttribute formulaForAttribute)
  {
    if (formulaForAttribute == null || string.IsNullOrWhiteSpace(formulaForAttribute.Formula))
      return true;
    AttributeValues[] attrValues = !formulaForAttribute.UseOldAttrValues ? this._NewValues : this._OldValues;
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service != null && service.CheckAttrsWithFormula(attrValues, formulaForAttribute.Formula);
  }
}
