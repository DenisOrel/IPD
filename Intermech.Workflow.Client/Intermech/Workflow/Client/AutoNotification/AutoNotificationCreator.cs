// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AutoNotificationCreator
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

internal class AutoNotificationCreator : 
  IObjectCreatorRiderCustomService,
  IObjectCreatorCustomService
{
  private NotificationEventType _notifEventType;
  private AutoNotificationControl _autoNotifCtrl;
  private long _templateObjectID;

  public IDictionary<ObjectCreatePages, bool> VisiblePages { get; private set; }

  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return -1;
  }

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    this._templateObjectID = TemplateObjectID;
    return false;
  }

  public bool AfterCreate(long newObjectID)
  {
    return (this._templateObjectID == 0L ? 0 : (this._templateObjectID != -1L ? 1 : 0)) != 0 || this.SetAutoNotificationType(newObjectID);
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object сreatedObject, int propPageIndex)
  {
    if (!(сreatedObject is CreatedObjectItem createdObject))
      return (Dictionary<UserControl, int>) null;
    if (createdObject.PrototypeID != 0L && createdObject.PrototypeID != -1L)
      this.CreateControlByPrototype(createdObject);
    else
      this.CreateControlWithEmptySettings(createdObject);
    return new Dictionary<UserControl, int>(1)
    {
      {
        (UserControl) this._autoNotifCtrl,
        1
      }
    };
  }

  private NotificationEventType NotificationEventTypeChoice()
  {
    EventChosingForm eventChosingForm = new EventChosingForm();
    int num = (int) eventChosingForm.ShowDialog();
    return eventChosingForm.EventType;
  }

  private void SetAutoNotificationTypeAttr(long newObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int attrNotifTypeValue = this.GetAttrNotifTypeValue();
      sessionKeeper.Session.AddObjectAttribute(newObjectID, wfConsts.AttrAutoNotificationTypeID, false, false, new object[1]
      {
        (object) attrNotifTypeValue
      });
    }
  }

  private int GetAttrNotifTypeValue()
  {
    switch (this._notifEventType)
    {
      case NotificationEventType.AddLink:
        return 9;
      case NotificationEventType.DeleteLink:
        return 11;
      case NotificationEventType.Create:
        return 7;
      case NotificationEventType.CreateVersion:
        return 8;
      case NotificationEventType.Delete:
        return 10;
      case NotificationEventType.NextLCStep:
        return 6;
      case NotificationEventType.NextLCLevel:
        return 5;
      case NotificationEventType.Cancel:
        return 4;
      case NotificationEventType.CheckIn:
        return 1;
      case NotificationEventType.CheckOut:
        return 0;
      case NotificationEventType.Write:
        return 2;
      case NotificationEventType.GetAccess:
        return 3;
      default:
        return 12;
    }
  }

  private static bool CheckSendAttrs2DelayedNotificationMode()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!sessionKeeper.Session.SendAttrs2DelayedNotificationMode)
      {
        if (MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Client_88"), LocalizationHolder.rm.GetString("Workflow.Client_89"), MessageBoxButtons.OKCancel) == DialogResult.Cancel)
          return false;
      }
    }
    return true;
  }

  private void CreateControlWithEmptySettings(CreatedObjectItem createdObject)
  {
    this._autoNotifCtrl = new AutoNotificationControl(AutoNotificationSettings.CreateEmptyNotifSettings(this._notifEventType, Math.Abs(createdObject.ObjectID)), createdObject.ObjectID);
  }

  private void CreateControlByPrototype(CreatedObjectItem createdObject)
  {
    this._autoNotifCtrl = new AutoNotificationControl(createdObject.PrototypeID, createdObject.ObjectID);
  }

  private bool SetAutoNotificationType(long newObjectID)
  {
    this._notifEventType = this.NotificationEventTypeChoice();
    if (this._notifEventType == NotificationEventType.None || this._notifEventType == NotificationEventType.Write && !AutoNotificationCreator.CheckSendAttrs2DelayedNotificationMode())
      return false;
    this.SetAutoNotificationTypeAttr(newObjectID);
    return true;
  }
}
