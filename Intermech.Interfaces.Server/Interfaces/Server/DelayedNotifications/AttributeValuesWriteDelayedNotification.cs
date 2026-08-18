// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.AttributeValuesWriteDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class AttributeValuesWriteDelayedNotification : AttributableDelayedNotification
{
  private readonly object[] _Values;
  private readonly int _AttributeID;

  public AttributeValuesWriteDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    object[] values,
    int attrID)
    : base(userID, notificationType, oldValues, newValues, instanceID, typeID)
  {
    this._Values = values;
    this._AttributeID = attrID;
  }

  public object[] Values => this._Values;

  public int AttributeID => this._AttributeID;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return base.IsSuitableForSettings(settings, session) && settings is AttrChangingAutoNotificationSettings notificationSettings && notificationSettings.AttrIDs.Contains(this._AttributeID);
  }

  public override bool CheckInitiatorAttrsWithFormula(FormulaForAttribute formulaForAttribute)
  {
    if (formulaForAttribute == null || formulaForAttribute.Formula == string.Empty)
      return true;
    AttributeValues[] oldValues = this.OldValues;
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service != null && service.CheckAttrsWithFormula(oldValues, formulaForAttribute.Formula);
  }

  public override void BuildMessage(ref string message, IUserSession session, bool isForEmail)
  {
    base.BuildMessage(ref message, session, isForEmail);
    this.SubstObjectCaption(ref message, session, isForEmail);
    this.SubstChangedAttr(ref message, session);
  }

  private void SubstChangedAttr(ref string message, IUserSession session)
  {
    this.SubstAttrType(ref message, session);
    this.SubstAttrValue(ref message, session);
  }

  private void SubstAttrType(ref string message, IUserSession session)
  {
    string name = session.GetAttributeType(this._AttributeID).Name;
    message = message.Replace(AutoNotificationMessageHelper.ChangedAttrName, name);
  }

  private void SubstAttrValue(ref string message, IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this.InstanceID, false);
    if (dbObject == null)
      return;
    string[] descriptionsById = dbObject.GetDescriptionsByID(this.AttributeID, false);
    if (descriptionsById == null || descriptionsById.Length == 0)
      return;
    for (int index = 0; index < ((IEnumerable<string>) descriptionsById).Count<string>(); ++index)
    {
      if (string.IsNullOrWhiteSpace(descriptionsById[index]))
        descriptionsById[index] = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("EmptyAttrValue"));
    }
    string newValue = string.Join(", ", descriptionsById);
    message = message.Replace(AutoNotificationMessageHelper.NewAttrValue, newValue);
  }
}
