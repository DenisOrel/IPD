// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevLaunchHandler
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.DBObjects;
using Intermech.Tools;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.ECO.Client;

internal sealed class RevLaunchHandler : ParameterlessLaunchHandler
{
  private readonly RevIntegrator integrator;

  public RevLaunchHandler(RevIntegrator integrator)
    : base(integrator.Id, RevIntegrator.ApplicationName)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objId = launchParams.ObjectId;
      int objectTypeId = sessionKeeper.Session.GetObjectInfo(objId).ObjectTypeID;
      IDBObject dbObject = (IDBObject) null;
      if (objId != -1L)
        dbObject = sessionKeeper.Session.GetObject(objId, false);
      LaunchType launchType = launchParams.LaunchType;
      if (launchType == LaunchType.Edit && objId > 0L)
        launchType = LaunchType.View;
      if (dbObject != null && launchType == LaunchType.View)
      {
        ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true);
        ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
        ITarget target = (ITarget) new UserTarget(service2.UserID, service2.UserGuid);
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objectTypeId);
        List<LaunchActionInfo> launchActionInfoList = service1.LookupActionList(objectTypeGuid, target, LaunchType.View);
        if (launchActionInfoList.Count > 1)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_File);
          if (attributeById != null)
          {
            int num = -1;
            for (int index = 0; index < attributeById.Values.Length; ++index)
            {
              if (ImDocumentData.GetFileExtensionWithoutDot(attributeById.Descriptions[index]).ToUpper() == "REV")
              {
                num = index;
                break;
              }
            }
            if (num != -1)
            {
              foreach (LaunchActionInfo launchActionInfo in launchActionInfoList)
              {
                if (launchActionInfo.HandlerId != this.Id)
                {
                  XmlDocument handlerData1 = new XmlDocument();
                  handlerData1.LoadXml(service1.GetActionData(launchActionInfo.ActionId));
                  ClientContext.LaunchActions.GetHandler(launchActionInfo.HandlerId, false).Launch(launchParams, handlerData1);
                  return;
                }
              }
            }
          }
        }
      }
      if (dbObject == null || dbObject.GetAttributeByID(DocIDCache.Attr_DocumentFile) == null || launchType == LaunchType.Edit)
      {
        ECOPlugin plugin = ECOPlugin.FindPlugin();
        if (launchType == LaunchType.Print)
        {
          if (objId != -1L)
            DocumentEditorPlugin.Instance.PrintImDocumentObject(objId);
          RecentObjectsNode.MRUObjects.Add(objId, ObjectAction.Print, DateTime.UtcNow);
        }
        else
        {
          bool readOnly = launchType != 0;
          if (objectTypeId == RevHelper.idChangeJournal)
            DocumentEditorPlugin.InvokeService.InvokeFunc<CJEditorForm>(-1, (Func<CJEditorForm>) (() => plugin.OpenCJEditorForObject(objId, readOnly, true, true, false)));
          else if (objectTypeId == RevHelper.idObj_DI || objectTypeId == RevHelper.idObj_DPI)
            DocumentEditorPlugin.InvokeService.InvokeFunc<DIEditorForm>(-1, (Func<DIEditorForm>) (() => plugin.OpenDIEditorForObject(objId, readOnly, true, true, false)));
          else
            DocumentEditorPlugin.InvokeService.InvokeFunc<ECOEditorForm>(-1, (Func<ECOEditorForm>) (() => plugin.OpenECOEditorForObject(objId, readOnly, true, true, false)));
        }
      }
      else
        ClientContext.LaunchActions.LaunchByShell(launchParams);
    }
  }
}
