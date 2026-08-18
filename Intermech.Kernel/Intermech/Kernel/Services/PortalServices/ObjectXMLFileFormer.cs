// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public class ObjectXMLFileFormer : XMLFileFormer
{
  private readonly List<IDBAttribute> remarks = new List<IDBAttribute>();
  protected IDBObject dbObject;

  public ObjectXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    Attributes4ObjectTag tag)
    : base(session, unit, writer, (Attributes4Tag) tag)
  {
    this.dbObject = obj;
  }

  protected override void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlNodeSysAttribute);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_GUID", this.dbObject.GUID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJECT_GUID", this.dbObject.ObjectGUID.ToString());
    if (this.dbObject.ParentVersionID != -1L)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_PARENT_GUID", this.GetObjectGuid(this.session, this.dbObject.ParentVersionID));
    IDBObjectType objectType = this.session.GetObjectType(this.dbObject.ObjectType);
    XmlDocument xmlDocument1 = xmlDocument;
    XmlNode node1 = element;
    Guid guid1 = (objectType as IDBGuid).GUID;
    string str1 = guid1.ToString();
    XMLFileHelper.AddAttribute(xmlDocument1, node1, "F_OBJTYPE_GUID", str1);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJ_TYPE_NAME", objectType.ObjectTypeName);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJ_NAME", objectType.ObjectInstanceName);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJTYPE_SHORTNAME", objectType.ObjectTypeShortName);
    XMLFileHelper.AddAttribute(xmlDocument, element, "CAPTION", this.dbObject.Caption);
    if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).Contains(this.dbObject.ObjectType))
    {
      DocumentTypeSettings settings = (this.session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(this.session.SessionGUID, objectType.ObjectType);
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_DOCTYPE_EXT", settings.DocumentFileExt);
    }
    Guid guid2 = Guid.Empty;
    if (this.session.GetCustomService(typeof (IContainerService)) is IContainerService customService)
      guid2 = this.GetPublishType(this.session, customService, objectType);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_PUBLISH_OBJTYPE", guid2.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OWNER_ID", this.GetObjectGuid(this.session, this.dbObject.OwnerID));
    IDBLifecycleStep lifecycleStep = this.session.GetLifecycleStep(this.dbObject.LCStep);
    XmlDocument xmlDocument2 = xmlDocument;
    XmlNode node2 = element;
    guid1 = (lifecycleStep as IDBGuid).GUID;
    string str2 = guid1.ToString();
    XMLFileHelper.AddAttribute(xmlDocument2, node2, "F_LC_STEP", str2);
    IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(lifecycleStep.LevelID);
    XmlDocument xmlDocument3 = xmlDocument;
    XmlNode node3 = element;
    guid1 = lifecycleLevel.GUID;
    string str3 = guid1.ToString();
    XMLFileHelper.AddAttribute(xmlDocument3, node3, "F_LEVEL_ID", str3);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_CREATE_DATE", DateTimeHelper.ToString(this.ToUTCDateTime(this.dbObject.CreateDate, this.session)));
    if (this.dbObject.ProjectID != 0L)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_PROJECT_ID", this.GetObjectGuid(this.session, this.dbObject.ProjectID));
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_BASE_VERSION", this.dbObject.IsBaseVersion ? "1" : "0");
    Attributes4ObjectTag tag = this.tag as Attributes4ObjectTag;
    XmlDocument xmlDocument4 = xmlDocument;
    XmlNode node4 = element;
    int num;
    string str4;
    if (tag == null)
    {
      str4 = 0.ToString();
    }
    else
    {
      num = (int) tag.RootType;
      str4 = num.ToString();
    }
    XMLFileHelper.AddAttribute(xmlDocument4, node4, "F_ROOT_TYPE", str4);
    if (tag != null && tag.LinkedGuid != string.Empty)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_LINKED_GUID", tag.LinkedGuid);
    XmlDocument xmlDocument5 = xmlDocument;
    XmlNode node5 = element;
    num = this.dbObject.AccessLevel;
    string str5 = num.ToString();
    XMLFileHelper.AddAttribute(xmlDocument5, node5, "F_ACCESS", str5);
    if (this.dbObject.ModificationID != 0L)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_MODIFICATION_ID", this.dbObject.ModificationID.ToString());
    if (this.dbObject.CreatorID != 0L)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_CREATOR_ID", this.GetObjectGuid(this.session, this.dbObject.CreatorID));
    xmlRootNode.AppendChild(element);
  }

  protected override Guid TypeGuid => MetaDataHelper.GetObjectTypeGuid(this.Attributes.ObjectType);

  protected override IDBAttributeCollection Attributes => this.dbObject.Attributes;

  protected override bool IsEnablePublishFile(BlobInformation biFile)
  {
    if (!string.IsNullOrEmpty(biFile.FileName))
    {
      FileInfo fileInfo = new FileInfo(biFile.FileName);
      if (!string.IsNullOrEmpty(fileInfo.Extension) && fileInfo.Extension.ToLower() == ".rxml" || biFile.FileType == FileTypes.ftNotContent)
        return false;
    }
    return true;
  }

  private Guid GetPublishType(
    IUserSession session,
    IContainerService containerService,
    IDBObjectType objType)
  {
    Guid publishType = Guid.Empty;
    IDBObject containerForObjectType = containerService.GetContainerForObjectType((object) session.SessionGUID, (objType as IDBGuid).GUID);
    if (containerForObjectType != null)
    {
      IDBAttribute attributeByGuid = containerForObjectType.GetAttributeByGuid(PortalConsts.attributePublishObjTypeGuid);
      if (attributeByGuid != null && GuidHelper.IsGuid(attributeByGuid.AsString))
        publishType = new Guid(attributeByGuid.AsString);
    }
    if (publishType == Guid.Empty && objType.ParentTypeID != -1)
      publishType = this.GetPublishType(session, containerService, session.GetObjectType(objType.ParentTypeID));
    return publishType;
  }
}
