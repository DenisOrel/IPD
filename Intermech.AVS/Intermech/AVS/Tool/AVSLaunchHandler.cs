// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Tool.AVSLaunchHandler
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Victor;
using Intermech.Client.Core;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Tools;
using Intermech.Tools.LaunchActions;
using System;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Tool;

internal sealed class AVSLaunchHandler : ParameterlessLaunchHandler
{
  private readonly AVSIntegrator integrator;

  public AVSLaunchHandler(AVSIntegrator integrator)
    : base(integrator.Id, AVSIntegrator.ApplicationName)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public override void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    SessionKeeper sessionKeeper = new SessionKeeper();
    long objectId = launchParams.ObjectId;
    using (sessionKeeper)
    {
      if (launchParams.LaunchType == LaunchType.Edit && AvsIDCache.IsSpecificationFromAnotherPortal(objectId, sessionKeeper.Session))
        throw new NotificationException("Запрещено редактировать спецификации пришедшие из другого узла информационной системы");
    }
  }

  /// <summary>Стартовать приложение службы инструментов</summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    bool flag = false;
    long num = launchParams.ObjectId;
    ILaunchHandler launchHandler = (ILaunchHandler) null;
    XmlDocument actionData = (XmlDocument) null;
    int fileAttributeID = launchParams.LaunchContext.Get<int>("FileAttributeID", -1);
    int fileIndex = launchParams.LaunchContext.Get<int>("FileIndex", -1);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (launchParams.LaunchType == LaunchType.Edit && AvsIDCache.IsSpecificationFromAnotherPortal(launchParams.ObjectId, sessionKeeper.Session))
        throw new NotificationException("Запрещено редактировать спецификации пришедшие из другого узла информационной системы");
      if (!MetaDataHelper.IsObjectTypeChildOf(launchParams.ObjectTypeId, AvsIDCache.ObjType_Document))
        num = AVSDocument.GetSpecificationIDForProduct(num, sessionKeeper.Session);
      IDBObject dbObject = (IDBObject) null;
      if (num.IsDefinedId())
        dbObject = sessionKeeper.Session.GetObject(num, false);
      if (launchParams.LaunchType == LaunchType.View && dbObject != null && DocumentEditorPlugin.DBObjectHasOldSPFileOnly(dbObject))
        launchHandler = DocumentEditorLaunchHandler.FindAnotherLaunchHandler(launchParams, this.Id, sessionKeeper.Session, out actionData);
      if (launchHandler == null)
      {
        if (dbObject != null)
        {
          if (fileAttributeID != -1)
          {
            if (fileAttributeID != DocIDCache.Attr_File)
              goto label_21;
          }
          flag = dbObject.GetAttributeByID(DocIDCache.Attr_DocumentFile) != null;
        }
      }
    }
label_21:
    if (launchHandler != null)
      launchHandler.Launch(launchParams, actionData);
    else if (launchParams.LaunchType != 0 & flag)
    {
      launchParams.ChangeObject(num, DBHelper.GetObjectType(num));
      ClientContext.LaunchActions.LaunchByShell(launchParams);
    }
    else if (launchParams.LaunchType == LaunchType.Print)
    {
      AVSPlugin.Instance.PrintAVSDocument(launchParams.ObjectId, launchParams.ObjectTypeId, fileAttributeID, fileIndex);
    }
    else
    {
      bool readOnly = !DocumentEditorLaunchHandler.CheckEditModeForOpenObject(launchParams.LaunchType, launchParams.ObjectId);
      if (MetaDataHelper.IsObjectTypeChildOf(launchParams.ObjectTypeId, AvsIDCache.ObjType_Vedomost) || MetaDataHelper.IsObjectTypeChildOf(launchParams.ObjectTypeId, AvsIDCache.ObjType_ConstrTabl) || MetaDataHelper.IsObjectTypeChildOf(launchParams.ObjectTypeId, AvsIDCache.ObjType_DocumsExpluat) || MetaDataHelper.IsObjectTypeChildOf(launchParams.ObjectTypeId, AvsIDCache.ObjType_DocumsProg))
        AVSPlugin.IInvokeService.InvokeFunc<ImDocumentEditorForm>(-1, (Func<ImDocumentEditorForm>) (() => DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(launchParams.ObjectId, readOnly, true, new DocumentWindowCreatorDelegate(VedomostEditorWindow.VedomostEditorWindowCreator))));
      else
        AVSPlugin.IInvokeService.InvokeFunc<AVSWindow>(-1, (Func<AVSWindow>) (() => AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(launchParams.ObjectId, launchParams.ObjectTypeId, readOnly: readOnly))));
    }
  }

  /// <summary>Проверяет назначен ли инструмент AVS на редактирование для заданного типа объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="session">Сессия</param>
  /// <returns></returns>
  internal static bool CanEditObjectTypeWithAVS(int objectType, IUserSession session)
  {
    ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ICurrentUserAndRole service2 = ServicesManager.GetService<ICurrentUserAndRole>();
    UserTarget userTarget1 = new UserTarget(service2.UserID, service2.UserGuid);
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objectType);
    UserTarget userTarget2 = userTarget1;
    return service1.LookupActionList(objectTypeGuid, (ITarget) userTarget2, LaunchType.Edit).Any<LaunchActionInfo>((Func<LaunchActionInfo, bool>) (a => a.HandlerId == AVSIntegrator.IntegratorId));
  }
}
