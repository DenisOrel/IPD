// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.AttributesContainerImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class AttributesContainerImporter(ImportArgs args) : ObjectUnitImporter(args)
{
  public override ImportedInfo Import()
  {
    XmlNode rootNode = XmlHelper.ReadMainFile(this.args.Unit, this.args.Path);
    string siteID = string.Empty;
    ObjectInfo objectAttributes = AttributesFile.GetObjectAttributes(rootNode, out siteID);
    try
    {
      ObjectRecord objectRecord = new ObjectRecord()
      {
        ObjectGuid = (object) objectAttributes.ObjectGuid
      };
      if (string.IsNullOrEmpty(siteID) && this.args.Unit.Tag is ObjectTag tag)
        siteID = Convert.ToString(tag.CreatorCode);
      objectRecord.SiteID = siteID;
      if (string.IsNullOrEmpty(siteID))
        throw new Exception("Невозможно определить информацию об узле-источнике!");
      IDBObjectType objType = this.CheckObjectType(objectAttributes);
      objectRecord.ObjectType = objType.ObjectType;
      ImportingObject importingObject = new ImportingObject(objectRecord);
      Dictionary<Guid, long> measures = new Dictionary<Guid, long>(1);
      if (objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedArticles || objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedDocuments || objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedObjects)
      {
        AttributeRecord attribute = new AttributeRecord(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeObjTypeName))
        {
          StringValue = (object) objectAttributes.ObjTypeName
        };
        importingObject.AddAttribute(attribute);
      }
      ISitesCacheService customService = (ISitesCacheService) this.args.Session.GetCustomService(typeof (ISitesCacheService));
      SiteInfo site = customService.GetSite(objectRecord.SiteID[0]);
      this.ParseAttributes(objType, importingObject, rootNode, measures, (ImportReceipt) null, site, out List<int> _);
      ImportPublishObject importPublishObject = new ImportPublishObject(this.args.Session as UserSession, importingObject, customService.Info.Code, true, site);
      IDBObject importObj;
      TypedImportedInfo info = importPublishObject.ImportAttributes(false, out importObj);
      if (importPublishObject.NeedRefreshFolderKey != 0L)
        this.args.UpdateFolderKeyObjects.Add(importPublishObject.NeedRefreshFolderKey);
      return importObj != null ? (ImportedInfo) new ExtendedImportedInfo(info, $"Обновлен {importObj.NameInMessages} (ид.версии={importObj.ObjectID})") : (ImportedInfo) info;
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1100"), (object) objectAttributes.ObjectGuid, (object) ex.Message), ex);
    }
  }
}
