// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interface;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ObjectImporter(ImportArgs args) : ObjectUnitImporter(args)
{
  public override ImportedInfo Import()
  {
    ISitesCacheService customService = (ISitesCacheService) this.args.Session.GetCustomService(typeof (ISitesCacheService));
    XmlNode rootNode = XmlHelper.ReadMainFile(this.args.Unit, this.args.Path);
    ObjectInfo objectAttributes = AttributesFile.GetObjectAttributes(rootNode);
    try
    {
      if (objectAttributes.ObjTypeName == "Документация" && objectAttributes.ObjectTypeGuid == Guid.Empty)
      {
        AttributeValue attributeValue = AttributesFile.GetAttributeValue(AttributesFile.FindAttributeValueNode(rootNode, PortalConsts.MainDocGuidAttribute));
        if (attributeValue != null)
        {
          DocImportedInfo docImportedInfo = new DocImportedInfo(new Guid(attributeValue.StringValue), objectAttributes.ObjectGuid);
          docImportedInfo.IsLink = true;
          return (ImportedInfo) docImportedInfo;
        }
      }
      IDBObjectType objType = this.CheckObjectType(objectAttributes);
      if (this.args.Unit.Category == TransferedObjectCategory.ObjectLink)
      {
        IDBObject dbObject = this.args.Session.GetObject(objectAttributes.ObjectGuid, false);
        if (dbObject != null && SiteIDHelper.IsOwner(customService.Info.Code, dbObject.SiteID))
          return new ImportedInfo(objectAttributes.ObjectGuid, dbObject.ID, dbObject.ObjectID, this.args.Unit.Category, false)
          {
            IsLink = true
          };
      }
      ObjectRecord objectRecord1 = new ObjectRecord()
      {
        ObjectType = objType.ObjectType,
        Object_id = 0,
        ObjectGuid = (object) objectAttributes.ObjectGuid,
        Id = 0,
        IdGuid = (object) objectAttributes.Guid,
        ModifyDate = DateTime.UtcNow,
        ObjCreate = objectAttributes.CreateDate != DateTime.MinValue ? objectAttributes.CreateDate : DateTime.UtcNow,
        Caption = objectAttributes.Caption,
        IsBaseVersion = objectAttributes.BaseVersion,
        AccessLevel = objectAttributes.Access
      };
      bool flag = false;
      IDBLifecycleStepCollection stepsCollection = this.args.Session.GetLCSchema(objType.SchemaID).GetStepsCollection();
      DataSet schema = stepsCollection.GetSchema();
      if (objectAttributes.LCStep != Guid.Empty)
      {
        IDBLifecycleStep lifecycleStep = this.args.Session.GetLifecycleStep(objectAttributes.LCStep, false);
        if (lifecycleStep != null && schema.Tables["IMS_LC_STEPS"].Rows.Find((object) lifecycleStep.LCStep) != null)
        {
          objectRecord1.Lc_step = lifecycleStep.LCStep;
          objectRecord1.LevelId = lifecycleStep.LevelID;
          flag = true;
        }
      }
      if (!flag && objectAttributes.LCLevel != Guid.Empty)
      {
        IDBLifecycleLevelType lifecycleLevel = this.args.Session.GetLifecycleLevel(objectAttributes.LCLevel, false);
        if (lifecycleLevel != null)
        {
          DataRow[] dataRowArray = schema.Tables["IMS_LC_STEPS"].Select($"{"F_LEVEL_ID"}={lifecycleLevel.LevelID}");
          if (dataRowArray != null && dataRowArray.Length != 0)
          {
            objectRecord1.Lc_step = Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]);
            objectRecord1.LevelId = lifecycleLevel.LevelID;
            flag = true;
          }
        }
      }
      if (!flag)
      {
        objectRecord1.Lc_step = stepsCollection.GetFirstStep();
        DataRow dataRow = schema.Tables["IMS_LC_STEPS"].Rows.Find((object) objectRecord1.Lc_step);
        objectRecord1.LevelId = Convert.ToInt32(dataRow["F_LEVEL_ID"]);
      }
      objectRecord1.OwnerId = 0L;
      objectRecord1.OwnerGuid = (object) Guid.Empty;
      if (objectAttributes.OwnerGuid != Guid.Empty)
      {
        ImportedInfo importedInfo = this.FindObject(objectAttributes.OwnerGuid);
        if (importedInfo != null)
        {
          objectRecord1.OwnerId = importedInfo.ObjectId;
          objectRecord1.OwnerGuid = (object) importedInfo.Guid;
        }
      }
      if (objectRecord1.OwnerId == 0L)
      {
        long defaultObjectOwner = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, true).DefaultObjectOwner;
        if (defaultObjectOwner != 0L)
        {
          QuickObjectInfo objectInfo = this.args.Session.GetObjectInfo(defaultObjectOwner);
          if (!objectInfo.Empty)
          {
            objectRecord1.OwnerId = objectInfo.ObjectID;
            objectRecord1.OwnerGuid = (object) objectInfo.VersionGuid;
          }
        }
        else
        {
          objectRecord1.OwnerId = this.args.UserID;
          objectRecord1.OwnerGuid = (object) this.args.UserGuid;
        }
      }
      objectRecord1.ProjectId = 0L;
      if (objectAttributes.ProjectGuid != Guid.Empty)
      {
        ImportedInfo importedInfo = this.FindObject(objectAttributes.ProjectGuid);
        if (importedInfo != null)
        {
          objectRecord1.ProjectId = importedInfo.ObjectId;
          objectRecord1.ProjectGuid = (object) importedInfo.Guid;
        }
      }
      objectRecord1.CreatorID = 0L;
      if (objectAttributes.CreatorGuid != Guid.Empty)
      {
        ImportedInfo importedInfo = this.FindObject(objectAttributes.CreatorGuid);
        if (importedInfo != null)
          objectRecord1.CreatorID = importedInfo.ObjectId;
      }
      ImportingObject importingObject = new ImportingObject(objectRecord1);
      ObjectTag tag = this.args.Unit.Tag as ObjectTag;
      objectRecord1.SiteID = tag.CreatorCode.ToString();
      char ch;
      if (tag.OwnerCode.HasValue && tag.OwnerCode.HasValue)
      {
        ObjectRecord objectRecord2 = objectRecord1;
        string siteId = objectRecord2.SiteID;
        ch = tag.OwnerCode.Value;
        string str = ch.ToString();
        objectRecord2.SiteID = siteId + str;
      }
      else
        objectRecord1.SiteID += Consts.NoSymbol.ToString();
      if (tag.CompositionOwnerCode.HasValue && tag.CompositionOwnerCode.HasValue)
      {
        ObjectRecord objectRecord3 = objectRecord1;
        string siteId = objectRecord3.SiteID;
        ch = tag.CompositionOwnerCode.Value;
        string str = ch.ToString();
        objectRecord3.SiteID = siteId + str;
      }
      Dictionary<Guid, long> measures = new Dictionary<Guid, long>(1);
      if (tag.EnableSites != null && tag.EnableSites != string.Empty)
      {
        AttributeRecord rec = new AttributeRecord()
        {
          AttributeId = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeEnabledSites),
          InlistId = 0,
          StringValue = (object) tag.EnableSites
        };
        importingObject.AddAttribute(new AttributeRecord(rec, 0L));
      }
      if (objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedArticles || objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedDocuments || objType.PropertiesStructure.ObjectTypeGuid == PortalConsts.objtypeImportedObjects)
      {
        AttributeRecord attribute = new AttributeRecord(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeObjTypeName))
        {
          StringValue = (object) objectAttributes.ObjTypeName
        };
        importingObject.AddAttribute(attribute);
      }
      ImportReceipt receipt = (ImportReceipt) null;
      ImportVersionsModes importVersionsMode = ImportVersionsModes.None;
      if (this.args is ImportPacketObjectArgs)
      {
        receipt = ((ImportPacketObjectArgs) this.args).Receipt;
        importVersionsMode = ((ImportPacketObjectArgs) this.args).ImportVersionsMode;
      }
      SiteInfo site = customService.GetSite(tag.CreatorCode);
      List<int> addedAttributes;
      this.ParseAttributes(objType, importingObject, rootNode, measures, receipt, site, out addedAttributes);
      List<Intermech.Interface.TypeAttribute> typeAttributeList = new Attributes4ObjectReader(objType.ObjectType, forbiddenAttributeIDs: addedAttributes.ToArray()).Read();
      if (typeAttributeList.Count > 0)
      {
        foreach (Intermech.Interface.TypeAttribute typeAttribute in typeAttributeList)
          importingObject.AddAttribute(typeAttribute.ConvertTo(this.args.Session));
      }
      ImportPublishObject importPublishObject = new ImportPublishObject(this.args.Session as UserSession, importingObject, customService.Info.Code, receipt, importVersionsMode, site, tag.WithComposition);
      ImportedInfo importedInfo1 = (ImportedInfo) importPublishObject.Import();
      importedInfo1.SystemType = site.SystemType;
      if (importedInfo1.IsNew && objectAttributes.ParentGuid != Guid.Empty)
        this.args.ParentVersions.Add(importedInfo1.ObjectId, objectAttributes.ParentGuid);
      IDBObject dbObject1 = (IDBObject) null;
      if (site.SystemType == SystemTypes.Search && string.IsNullOrEmpty(objectAttributes.Caption) && objType.CaptionAttribute != 0)
      {
        dbObject1 = this.args.Session.GetObject(importedInfo1.ObjectId);
        IDBAttribute attributeById = dbObject1.GetAttributeByID(objType.CaptionAttribute);
        if (attributeById != null && !string.IsNullOrEmpty(attributeById.AsString))
          ((DBObject) dbObject1).SetCaption(attributeById.AsString);
      }
      if (site.SystemType == SystemTypes.Search)
      {
        if (MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad00348-306c-11d8-b4e9-00304f19f545")).Contains(objType.ObjectType))
          this.args.Contexts.Add(new Tuple<Guid, Guid, long, List<Guid>>(dbObject1.ObjectGUID, dbObject1.ObjectGUID, dbObject1.ObjectID, new List<Guid>()));
      }
      else
      {
        Guid guid = Guid.Empty;
        List<Guid> guidList = (List<Guid>) null;
        for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
        {
          XmlNode childNode = rootNode.ChildNodes[i];
          if (childNode.Name == PortalConsts.XmlNodeContext)
          {
            if (GuidHelper.IsGuid(childNode.Attributes["F_MODIFICATION_ID"].Value))
              guid = new Guid(childNode.Attributes["F_MODIFICATION_ID"].Value);
            string[] strArray = childNode.Attributes["F_OBJECTS"].Value.Split(';');
            guidList = new List<Guid>(strArray.Length);
            foreach (string str in strArray)
            {
              if (GuidHelper.IsGuid(str))
                guidList.Add(new Guid(str));
            }
            break;
          }
        }
        if (guidList != null)
        {
          if (dbObject1 == null)
            dbObject1 = this.args.Session.GetObject(importedInfo1.ObjectId);
          IDBAttribute dbAttribute = dbObject1.GetAttributeByGuid(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"));
          long num;
          if (importedInfo1.IsNew)
          {
            num = importedInfo1.ObjectId;
            if (dbAttribute == null)
              dbAttribute = dbObject1.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545"), false);
            dbAttribute.Value = (object) num;
          }
          else
            num = dbAttribute != null ? dbAttribute.AsInteger : 0L;
          this.args.Contexts.Add(new Tuple<Guid, Guid, long, List<Guid>>(objectAttributes.ObjectGuid, guid, num, guidList));
        }
      }
      if (importPublishObject.NeedRefreshFolderKey != 0L)
        this.args.UpdateFolderKeyObjects.Add(importPublishObject.NeedRefreshFolderKey);
      if (objectAttributes.ModificationID != 0L)
        this.args.ChangesGroupNums.Add(new Tuple<long, Guid, long>(importedInfo1.ObjectId, importedInfo1.Guid, objectAttributes.ModificationID));
      return importedInfo1;
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1101"), (object) objectAttributes.ObjectGuid, (object) ex.Message), ex);
    }
  }
}
