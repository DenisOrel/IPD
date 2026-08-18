// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.SetAttributesValuesDelayedNotification
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Server.DelayedNotifications;

public class SetAttributesValuesDelayedNotification : AttributableDelayedNotification
{
  private AttributeValues[] _Values;

  public SetAttributesValuesDelayedNotification(
    long userID,
    ActionType notificationType,
    AttributeValues[] oldValues,
    AttributeValues[] newValues,
    long instanceID,
    int typeID,
    AttributeValues[] values)
    : base(userID, notificationType, oldValues, newValues, instanceID, typeID)
  {
    this._Values = values;
  }

  public AttributeValues[] Values => this._Values;

  public override bool IsSuitableForSettings(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    if (!base.IsSuitableForSettings(settings, session) || !(settings is AttrChangingAutoNotificationSettings notificationSettings))
      return false;
    bool flag = false;
    foreach (AttributeValues attributeValues in this._Values)
    {
      if (notificationSettings.AttrIDs.Contains(attributeValues.AttributeID))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public override void Send(
    AutoNotificationSettings settings,
    List<long> adresseeIds,
    IUserSession session)
  {
    string mailSubject = this.CreateMailSubject(settings, session);
    foreach (int changedAttrId in this.GetChangedAttrIds(settings))
    {
      string message1 = settings.Message;
      string message2 = settings.Message;
      this.BuildMessage(ref message2, changedAttrId, session, true);
      this.BuildMessage(ref message1, changedAttrId, session, false);
      this.Send(settings, adresseeIds, session, mailSubject, message2, message1);
    }
  }

  private void BuildMessage(ref string message, int attrId, IUserSession session, bool isForEmail)
  {
    this.BuildMessage(ref message, session, isForEmail);
    this.SubstObjectCaption(ref message, session, isForEmail);
    this.SubstChangedAttr(ref message, session, attrId);
  }

  private void SubstChangedAttr(ref string message, IUserSession session, int attrId)
  {
    this.SubstAttrType(ref message, session, attrId);
    this.SubstAttrValue(ref message, session, attrId);
  }

  private void SubstAttrValue(ref string message, IUserSession session, int attrId)
  {
    IDBObject dbObject = session.GetObject(this.InstanceID, false);
    if (dbObject == null)
      return;
    string[] descriptionsById = dbObject.GetDescriptionsByID(attrId, false);
    if (descriptionsById == null || descriptionsById.Length == 0)
      return;
    for (int index = 0; index < ((IEnumerable<string>) descriptionsById).Count<string>(); ++index)
    {
      if (string.IsNullOrWhiteSpace(descriptionsById[index]))
        descriptionsById[index] = "Пусто";
    }
    string newValue = string.Join(", ", descriptionsById);
    message = message.Replace(AutoNotificationMessageHelper.NewAttrValue, newValue);
  }

  private void SubstAttrType(ref string message, IUserSession session, int attrId)
  {
    string name = session.GetAttributeType(attrId).Name;
    message = message.Replace(AutoNotificationMessageHelper.ChangedAttrName, name);
  }

  private List<int> GetChangedAttrIds(AutoNotificationSettings settings)
  {
    List<int> collection = new List<int>();
    if (!(settings is AttrChangingAutoNotificationSettings notificationSettings))
      return collection;
    List<int> settingsAttrs = notificationSettings.AttrIDs;
    foreach (AttributeValues attributeValues in ((IEnumerable<AttributeValues>) this.Values).Where<AttributeValues>((Func<AttributeValues, bool>) (attrValue => settingsAttrs.Contains(attrValue.AttributeID))).ToList<AttributeValues>())
      collection.SafeAdd<int>(attributeValues.AttributeID);
    return collection;
  }
}
