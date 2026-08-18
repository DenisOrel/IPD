// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.OwnCompleteHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class OwnCompleteHelper
{
  public static bool ExecuteCommand(
    UserSession session,
    long[] objectIDs,
    Guid[] objectGuids,
    string ownerSites,
    bool withComposition,
    bool autoUpdate)
  {
    IPublishTypesConfiguration customService1 = session.GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration;
    IPortalConnector customService2 = (IPortalConnector) session.GetCustomService(typeof (IPortalConnector));
    session.StartTransaction();
    bool flag = false;
    try
    {
      session.GetRelationCollection(-1);
      List<long> longList = new List<long>(objectGuids.Length);
      for (int index = 0; index < objectGuids.Length; ++index)
      {
        IDBObject dbObject = session.GetObject(objectGuids[index]);
        longList.Add(dbObject.ObjectID);
        OwnCompleteHelper.CheckObjectBeforeOwnComplete(dbObject);
      }
      ISelectionsService customService3 = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      IDBObject dbObject1 = session.GetObject(PortalConsts.selectionAutoPublish);
      // ISSUE: variable of a boxed type
      __Boxed<Guid> sessionGuid = (ValueType) session.SessionGUID;
      long objectId = dbObject1.ObjectID;
      long[] array = longList.ToArray();
      customService3.ExcludeObjects((object) sessionGuid, objectId, array);
      CompositionApplicabilities applic = (CompositionApplicabilities) null;
      if (withComposition)
        applic = customService1.GetCompositionApplicabilities();
      string[] strArray = customService2.OwnComplete(session.SessionGUID, objectIDs, ownerSites, applic, withComposition, autoUpdate);
      for (int index = 0; index < strArray.Length; ++index)
      {
        try
        {
          IDBObject dbObject2 = session.GetObject(new Guid(strArray[index]));
          OwnCompleteHelper.SetAttributesAfterOwnComplete((IUserSession) session, dbObject2);
        }
        catch (Exception ex)
        {
          flag = true;
          TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1086"), (object) strArray[index], (object) Helper.FormingLogError(ex)));
        }
      }
      session.Commit();
      return !flag;
    }
    catch
    {
      session.Rollback();
      throw;
    }
  }

  private static void CheckObjectBeforeOwnComplete(IDBObject obj)
  {
    if (obj.SiteID == null || obj.SiteID == string.Empty)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1088"), (object) obj.NameInMessages));
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributePublicationNecessary, false);
    if (attributeByGuid == null || attributeByGuid != null && (int) attributeByGuid.AsInteger != 0)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1089"), (object) obj.NameInMessages));
  }

  private static void SetAttributesAfterOwnComplete(IUserSession session, IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributePublicationNecessary, false);
    if (attributeByGuid != null)
      attributeByGuid.AsInteger = 0L;
    (obj as DBObject).SetSiteID(Convert.ToString(obj.SiteID[0]));
  }
}
