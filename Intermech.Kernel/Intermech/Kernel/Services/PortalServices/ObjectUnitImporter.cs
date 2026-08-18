// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectUnitImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class ObjectUnitImporter : UnitImporter
{
  public ObjectUnitImporter(ImportArgs args)
    : base(args)
  {
  }

  protected IDBObjectType CheckObjectType(ObjectInfo objInfo)
  {
    UserSession session = this.args.Session as UserSession;
    IDBObjectType dbObjectType = (IDBObjectType) null;
    string objTypeName = objInfo.ObjTypeName;
    if (objInfo.ObjectTypeGuid != Guid.Empty)
      dbObjectType = session.GetObjectType(objInfo.ObjectTypeGuid, false);
    if (dbObjectType == null && objTypeName != null && objTypeName != string.Empty)
    {
      dbObjectType = session.GetObjectType(objTypeName, false);
      if (dbObjectType == null)
      {
        DataRow[] dataRowArray = session.DBCache.GetTable("IMS_OBJECT_TYPES").Select($"{"F_OBJ_NAME"}={SqlHelper.QString(objTypeName)}");
        if (dataRowArray != null && dataRowArray.Length == 1)
          dbObjectType = session.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
      }
      if (dbObjectType == null)
      {
        Dictionary<string, Guid> complianceObjectTypes = (session.GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration).ComplianceObjectTypes;
        Guid anObjectTypeGuid;
        if (complianceObjectTypes != null && complianceObjectTypes.TryGetValue(objTypeName, out anObjectTypeGuid))
          dbObjectType = session.GetObjectType(anObjectTypeGuid, false);
      }
    }
    if (dbObjectType == null)
    {
      switch (objInfo.RootType)
      {
        case PublishObjectRootType.rtUnknown:
          dbObjectType = session.GetObjectType(PortalConsts.objtypeImportedObjects, false);
          break;
        case PublishObjectRootType.rtArticle:
          dbObjectType = session.GetObjectType(PortalConsts.objtypeImportedArticles, false);
          break;
        case PublishObjectRootType.rtDocument:
          dbObjectType = session.GetObjectType(PortalConsts.objtypeImportedDocuments, false);
          break;
      }
    }
    return dbObjectType != null ? dbObjectType : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1102"), (object) objInfo.ObjectTypeGuid, (object) objTypeName));
  }
}
