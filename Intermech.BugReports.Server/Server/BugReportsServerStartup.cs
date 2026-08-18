// Decompiled with JetBrains decompiler
// Type: Intermech.BugReports.Server.BugReportsServerStartup
// Assembly: Intermech.BugReports.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D5496885-D5AE-45E1-887A-E42A46AB4DD0
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.BugReports.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.HelpDesk;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Services;
using Intermech.Workflow;
using System;

#nullable disable
namespace Intermech.BugReports.Server;

public class BugReportsServerStartup : IPackage
{
  private IEventLogHelper eventLogHelper;

  public void Load(IServiceProvider serviceProvider)
  {
    if (serviceProvider.GetService(typeof (IDBObjectService)) is ICreatorContainer service1)
      service1.AddCreator((object) BugReportsHolder.OT.BugObjectType, (object) new BugDBObjectCreator());
    this.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    if (this.eventLogHelper != null)
    {
      this.eventLogHelper.AddAttributeWriteHandler((object) BugReportsHolder.AT.BugStatus, new WriteAttributeValueHandler(this.WriteBugStatus));
      this.eventLogHelper.AfterCreateObjectEvent += new AfterCreateObjectHandler(this.eventLogHelper_AfterCreateObjectEvent);
    }
    ICustomServices service2 = serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices;
    HelpDeskService serviceInstance = new HelpDeskService();
    ServerServices.AddService(typeof (IHelpDeskService), (object) serviceInstance);
    service2?.AddService(typeof (IHelpDeskService), (object) serviceInstance);
    (ServerServices.GetService(typeof (ISelectionsService)) as SelectionSrvService).AfterClassifyObjectsEvent += new OnClassifyObjectsHandler(this.OnClassifyObjects);
  }

  private void eventLogHelper_AfterCreateObjectEvent(
    IDBObject newobject,
    IDBObject prototype,
    IUserSession session)
  {
    if (prototype == null || prototype.ProjectID <= 0L || newobject.ID != prototype.ID)
      return;
    newobject.ProjectID = prototype.ProjectID;
  }

  public void OnClassifyObjects(
    IUserSession session,
    IDBObject classifier,
    IDBObject folder,
    long[] objectsID)
  {
    long num = folder.ProjectID <= 0L ? (classifier.ProjectID <= 0L ? 0L : classifier.ProjectID) : folder.ProjectID;
    if (num <= 0L)
      return;
    for (int index = 0; index < objectsID.Length; ++index)
    {
      IDBObject dbObject = session.GetObject(objectsID[index], false);
      if (dbObject != null)
        dbObject.ProjectID = num;
    }
  }

  public void Unload()
  {
    this.eventLogHelper.RemoveAttributeWriteHandler((object) BugReportsHolder.AT.BugStatus, new WriteAttributeValueHandler(this.WriteBugStatus));
  }

  public string Name => "Серверная часть плагина \"Ошибки и предложения\"";

  private void WriteBugStatus(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    IUserSession session = args.Session;
    if (session == null || args.Value == null || !(attribute is DBAttribute dbAttribute) || !(dbAttribute.ParentObject is IDBObject parentObject))
      return;
    if (args.Value.ToString() != "Новая" && args.Value.ToString() != "Обнаружена повторно" && args.Value.ToString() != "Новая с доп. информацией")
      BugReportsServerStartup.WriteFixUserAndDateFixAttributes(session, parentObject);
    IDBAttribute attributeByGuid = parentObject.GetAttributeByGuid(BugReportsHolder.AT.Enterprise, false);
    if (attributeByGuid == null || string.IsNullOrWhiteSpace(attributeByGuid.AsString) || parentObject.OwnerID == args.Session.UserID)
      return;
    BugReportsServerStartup.SetNotificationToBugOwner(session, parentObject, args.OldValue, args.Value);
  }

  private static void SetNotificationToBugOwner(
    IUserSession session,
    IDBObject bugObject,
    object oldValue,
    object value)
  {
    long ownerId = bugObject.OwnerID;
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service1) || !(service1.GetService(typeof (IRouterService)) is IRouterService service2))
      return;
    IDBAttribute attributeByGuid = bugObject.GetAttributeByGuid(BugReportsHolder.AT.HelpdeskID);
    string str = string.IsNullOrWhiteSpace(attributeByGuid.Value.ToString()) ? "-" : attributeByGuid.Value.ToString();
    string Subject = string.Format(NotifyHelper.MessageSubject, (object) EnumDescConverter.GetEnumDescription((Enum) NotifyOptions.AttributeValueChanged));
    string Text = $"Статус ошибки <a href =\"#object={bugObject.ObjectID}\">{bugObject.Caption}</a> изменен с <strong>{oldValue}</strong> на <strong>{value}</strong> пользователем <strong>{session.UserName}</strong> <br>Номер заявки в Helpdesk: {str}</br>";
    service2.CreateMessage(session.SessionGUID, ownerId, Subject, Text, session.UserID);
  }

  private static void WriteFixUserAndDateFixAttributes(IUserSession session, IDBObject bugObject)
  {
    IDBAttribute attributeByGuid1 = bugObject.GetAttributeByGuid(BugReportsHolder.AT.FixUser, false);
    if (attributeByGuid1 == null)
      return;
    attributeByGuid1.Value = (object) session.UserID;
    IDBAttribute attributeByGuid2 = bugObject.GetAttributeByGuid(BugReportsHolder.AT.FixData, false);
    if (attributeByGuid2 == null)
      return;
    attributeByGuid2.Value = (object) DateTime.Now;
  }
}
