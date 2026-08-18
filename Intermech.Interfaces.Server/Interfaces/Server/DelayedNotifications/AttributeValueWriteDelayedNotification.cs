// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.AttributeValueWriteDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class AttributeValueWriteDelayedNotification : AttributableDelayedNotification
{
  private object _Value;
  private int _AttributeID;
  private int _Index;

  public AttributeValueWriteDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    object value,
    int attrID,
    int index)
    : base(userID, notificationType, oldValues, newValues, instanceID, typeID)
  {
    this._Value = value;
    this._AttributeID = attrID;
    this._Index = index;
  }

  public object Value => this._Value;

  public int AttributeID => this._AttributeID;

  public int Index => this._Index;

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

  private void SubstAttrValue(ref string message, IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this.InstanceID, false);
    if (dbObject == null)
      return;
    string[] descriptionsById = dbObject.GetDescriptionsByID(this.AttributeID, false);
    if (descriptionsById == null || descriptionsById.Length == 0 || descriptionsById.Length <= this._Index)
      return;
    string newValue = descriptionsById[this._Index];
    if (string.IsNullOrWhiteSpace(newValue))
      newValue = "Пусто";
    message = message.Replace(AutoNotificationMessageHelper.NewAttrValue, newValue);
  }

  private void SubstAttrType(ref string message, IUserSession session)
  {
    string name = session.GetAttributeType(this._AttributeID).Name;
    message = message.Replace(AutoNotificationMessageHelper.ChangedAttrName, name);
  }
}
