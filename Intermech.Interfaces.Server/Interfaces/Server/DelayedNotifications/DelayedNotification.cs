// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DelayedNotifications.DelayedNotification
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

public abstract class DelayedNotification
{
  private long _UserID;
  private ActionType _NotificationType;

  public DelayedNotification(long userID, ActionType notificationType)
  {
    this._UserID = userID;
    this._NotificationType = notificationType;
  }

  public long UserID => this._UserID;

  public ActionType NotificationType => this._NotificationType;

  public virtual bool IsSuitableForSettings(AutoNotificationSettings settings, IUserSession session)
  {
    switch (this._NotificationType)
    {
      case ActionType.Create:
        if (settings.NotifEventType == NotificationEventType.Create || settings.NotifEventType == NotificationEventType.CreateVersion)
          return true;
        break;
      case ActionType.Delete:
      case ActionType.AddLink:
      case ActionType.DeleteLink:
      case ActionType.GetAccess:
      case ActionType.Cancel:
      case ActionType.CheckOut:
      case ActionType.CheckIn:
      case ActionType.Restore:
        if (settings.NotifEventType.ToString() == this._NotificationType.ToString())
          return true;
        break;
      case ActionType.Write:
        if (settings.NotifEventType.ToString() == this._NotificationType.ToString() && wfConsts.SendAttrs2DelayedNotificationMode)
          return true;
        break;
      case ActionType.NextLCStep:
        if (settings.NotifEventType == NotificationEventType.NextLCLevel || settings.NotifEventType == NotificationEventType.NextLCStep)
          return true;
        break;
    }
    return false;
  }

  public abstract void BuildMessage(ref string message, IUserSession session, bool isForEmail);

  public virtual void Send(
    AutoNotificationSettings settings,
    List<long> adresseeIds,
    IUserSession session)
  {
    string mailSubject = this.CreateMailSubject(settings, session);
    string message1 = settings.Message;
    string message2 = settings.Message;
    this.BuildMessage(ref message2, session, true);
    this.BuildMessage(ref message1, session, false);
    this.Send(settings, adresseeIds, session, mailSubject, message2, message1);
  }

  protected virtual string CreateMailSubject(
    AutoNotificationSettings settings,
    IUserSession session)
  {
    return settings.NotifEventType.GetDescription<NotificationEventType>();
  }

  protected void Send(
    AutoNotificationSettings settings,
    List<long> adresseeIds,
    IUserSession session,
    string subject,
    string emailMessage,
    string imailMessage)
  {
    DelayedNotification.SendToSpecificEmails(settings, session, subject, emailMessage);
    if (adresseeIds.Count <= 0)
      return;
    switch (settings.WayOfNotification)
    {
      case WayOfNotificationEnum.InternalMail:
        DelayedNotification.SendToInternalMail(adresseeIds, session, subject, imailMessage);
        break;
      case WayOfNotificationEnum.ExternalMail:
        DelayedNotification.SendToExternalMail(adresseeIds, session, subject, emailMessage, imailMessage);
        break;
      case WayOfNotificationEnum.InternalAndExternalMail:
        DelayedNotification.SendToExternalAndInternalMail(adresseeIds, session, subject, emailMessage, imailMessage);
        break;
    }
  }

  private static void SendToExternalAndInternalMail(
    List<long> adresseeIds,
    IUserSession session,
    string subject,
    string emailMessage,
    string imailMessage)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IAutoNotificationsService)) is IAutoNotificationsService service))
      return;
    service.InternalMailProcessing(session, adresseeIds, subject, imailMessage);
    List<MyElement> myElementList = service.EmailProcessing(session, adresseeIds.ToArray(), subject, emailMessage);
    if (myElementList.Count <= 0)
      return;
    foreach (MyElement myElement in myElementList)
    {
      string message = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("Interfaces.Workflow_39"), (object) myElement.Caption);
      service.AddMessageToLog(message);
    }
  }

  private static void SendToInternalMail(
    List<long> adresseeIds,
    IUserSession session,
    string subject,
    string imailMessage)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IAutoNotificationsService)) is IAutoNotificationsService service))
      return;
    service.InternalMailProcessing(session, adresseeIds, subject, imailMessage);
  }

  private static void SendToExternalMail(
    List<long> adresseeIds,
    IUserSession session,
    string subject,
    string emailMessage,
    string imailMessage)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IAutoNotificationsService)) is IAutoNotificationsService service))
      return;
    List<MyElement> source = service.EmailProcessing(session, adresseeIds.ToArray(), subject, emailMessage);
    if (source.Count <= 0)
      return;
    List<long> list = source.Select<MyElement, long>((Func<MyElement, long>) (user => (long) user.Value)).ToList<long>();
    service.InternalMailProcessing(session, list, subject, imailMessage);
    foreach (MyElement myElement in source)
    {
      string message = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("Interfaces.Workflow_39"), (object) myElement.Caption);
      service.AddMessageToLog(message);
    }
  }

  private static void SendToSpecificEmails(
    AutoNotificationSettings settings,
    IUserSession session,
    string subject,
    string emailMessage)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IAutoNotificationsService)) is IAutoNotificationsService service))
      return;
    List<string> specificEmails = settings.GetSpecificEmails();
    if (specificEmails.Count <= 0)
      return;
    service.SendToSpecificEmails(specificEmails, subject, emailMessage, session);
  }
}
