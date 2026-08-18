// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentEditorLaunchHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

public sealed class DocumentEditorLaunchHandler : ParameterlessLaunchHandler
{
  private readonly DocumentEditorIntegrator integrator;

  internal DocumentEditorLaunchHandler(DocumentEditorIntegrator integrator)
    : base(integrator.Id, DocumentEditorIntegrator.ApplicationName)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  /// <summary>Стартовать приложение службы инструментов</summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  /// <returns>Идентификатор версии объекта, который был открыт в интеграторе. Может не совпадать с objectId.</returns>
  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    int fileAttributeID = launchParams.LaunchContext.Get<int>("FileAttributeID", -1);
    int fileIndex = launchParams.LaunchContext.Get<int>("FileIndex", -1);
    if (launchParams.LaunchType == LaunchType.Print)
    {
      DocumentEditorPlugin.Instance.PrintImDocumentObject(launchParams.ObjectId, fileAttributeID, fileIndex);
    }
    else
    {
      ILaunchHandler launchHandler = (ILaunchHandler) null;
      XmlDocument actionData = (XmlDocument) null;
      if (launchParams.LaunchType == LaunchType.View)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(launchParams.ObjectId, false);
          if (dbObject != null)
          {
            if (DocumentEditorPlugin.DBObjectHasOldFormatFileOnly(dbObject))
              launchHandler = DocumentEditorLaunchHandler.FindAnotherLaunchHandler(launchParams, this.Id, sessionKeeper.Session, out actionData);
          }
        }
        if (launchHandler != null)
        {
          launchHandler.Launch(launchParams, actionData);
          return;
        }
      }
      bool readOnly = !DocumentEditorLaunchHandler.CheckEditModeForOpenObject(launchParams.LaunchType, launchParams.ObjectId);
      DocumentEditorPlugin.InvokeService.InvokeFunc<ImDocumentEditorForm>(-1, (Func<ImDocumentEditorForm>) (() => DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(launchParams.ObjectId, fileAttributeID, fileIndex, readOnly, true)));
    }
  }

  public static List<LaunchActionInfo> GetActionList(
    int objectType,
    LaunchType launchType,
    IUserSession session)
  {
    ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    ITarget target1 = (ITarget) new UserTarget(service2.UserID, service2.UserGuid);
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objectType);
    ITarget target2 = target1;
    int num = (int) launchType;
    return service1.LookupActionList(objectTypeGuid, target2, (LaunchType) num);
  }

  public static void LaunchAnotherAction(
    List<LaunchActionInfo> actionList,
    LaunchParams launchParams,
    IUserSession session,
    Guid notThislaunchHandlerId)
  {
    foreach (LaunchActionInfo action in actionList)
    {
      if (action.HandlerId != notThislaunchHandlerId)
      {
        ILaunchActionServer service = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
        XmlDocument handlerData = new XmlDocument();
        handlerData.LoadXml(service.GetActionData(action.ActionId));
        ClientContext.LaunchActions.GetHandler(action.HandlerId, false).Launch(launchParams, handlerData);
        break;
      }
    }
  }

  public static ILaunchHandler FindAnotherLaunchHandler(
    LaunchParams launchParams,
    Guid notThisLaunchHandlerId,
    IUserSession session,
    out XmlDocument actionData)
  {
    actionData = (XmlDocument) null;
    ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ICurrentUserAndRole service2 = ServicesManager.GetService<ICurrentUserAndRole>();
    LaunchActionInfo launchActionInfo = service1.LookupActionList(MetaDataHelper.GetObjectTypeGuid(launchParams.ObjectTypeId), (ITarget) new UserTarget(service2.UserID, service2.UserGuid), launchParams.LaunchType).FirstOrDefault<LaunchActionInfo>((Func<LaunchActionInfo, bool>) (a => a.HandlerId != notThisLaunchHandlerId));
    if (launchActionInfo == null)
      return (ILaunchHandler) null;
    actionData = new XmlDocument();
    actionData.LoadXml(service1.GetActionData(launchActionInfo.ActionId));
    return ClientContext.LaunchActions.GetHandler(launchActionInfo.HandlerId, false);
  }

  /// <summary>Проверить допустимость режима редактирования открываемого объекта</summary>
  /// <param name="openMode">Режим открытия объекта</param>
  /// <param name="objectId">Идентификатор версии открываемого объекта</param>
  /// <returns></returns>
  public static bool CheckEditModeForOpenObject(LaunchType openMode, long objectId)
  {
    bool flag = openMode == LaunchType.Edit;
    if (flag && objectId > 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(objectId).ObjectModifyMode != ObjectModifyModes.InBase)
          flag = false;
      }
    }
    return flag;
  }

  /// <summary>Расширенная проверка допустимости редактирования открываемого объекта</summary>
  /// <param name="openMode">Режим открытия объекта</param>
  /// <param name="objectId">Идентификатор версии открываемого объекта</param>
  /// <param name="reasonMessage">текст диагностического сообщения</param>
  /// <returns>Кортеж из 2 значений (можно ли редактировать, нужно ли сперва взять на изменение)</returns>
  public static (bool, bool) AdvancedEditModeCheckForObject(
    LaunchType openMode,
    long objectId,
    out string reasonMessage)
  {
    reasonMessage = string.Empty;
    bool flag1 = openMode == LaunchType.Edit && objectId.IsDefinedId();
    bool flag2 = false;
    if (!flag1 || objectId < -1L)
      return (flag1, flag2);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObj = sessionKeeper.Session.GetObject(objectId);
      if (dbObj != null)
        return DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(openMode, dbObj, out reasonMessage);
      reasonMessage = $"Объект с идентификатором {objectId} не найден.";
      return (false, false);
    }
  }

  public static (bool, bool) AdvancedEditModeCheckForObject(
    LaunchType openMode,
    IDBObject dbObj,
    out string reasonMessage)
  {
    reasonMessage = string.Empty;
    bool flag1 = openMode == LaunchType.Edit && dbObj != null;
    bool flag2 = false;
    if (!flag1)
      return (false, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (dbObj.ObjectModifyMode == ObjectModifyModes.InBase)
        return (true, flag2);
      if (dbObj.ObjectModifyMode == ObjectModifyModes.CantModify)
      {
        flag1 = false;
        reasonMessage = string.Format(KernelErrorMessages.GetErrorMessage(95), (object) dbObj.NameInMessages, (object) dbObj.ObjectID, (object) (sessionKeeper.Session.GetLifecycleStep(dbObj.LCStep)?.LCName ?? string.Empty));
        return (flag1, flag2);
      }
      if (dbObj.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      {
        flag1 = false;
        reasonMessage = string.Format(KernelErrorMessages.GetErrorMessage(240 /*0xF0*/), (object) dbObj.NameInMessages, (object) dbObj.ObjectID, (object) (sessionKeeper.Session.GetLifecycleStep(dbObj.LCStep)?.LCName ?? string.Empty));
        return (flag1, flag2);
      }
      if (dbObj.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        if (dbObj.CheckoutBy.IsUndefinedId())
        {
          flag2 = true;
          reasonMessage = $"Для модификации объекта \"{dbObj.Caption}\"  [{dbObj.ObjectID}], его необходимо взять на изменение.";
          return (flag1, flag2);
        }
        if (dbObj.CheckoutBy == sessionKeeper.Session.UserID)
        {
          flag1 = true;
          flag2 = false;
          return (flag1, flag2);
        }
        flag1 = false;
        flag2 = false;
        string str = sessionKeeper.Session.GetObject(dbObj.CheckoutBy)?.NameInMessages ?? string.Empty;
        reasonMessage = string.Format(KernelErrorMessages.GetErrorMessage(63 /*0x3F*/), (object) dbObj.NameInMessages, (object) dbObj.ObjectID, (object) str);
        return (flag1, flag2);
      }
    }
    return (flag1, flag2);
  }

  internal delegate void DelegateForInvokeLaunch(LaunchParams launchParams, XmlDocument handlerData);
}
