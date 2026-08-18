// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.RelationXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public class RelationXMLFileFormer : XMLFileFormer
{
  private readonly IDBRelation _relation;

  public RelationXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBRelation relation,
    Attributes4RelationTag tag)
    : base(session, unit, writer, (Attributes4Tag) tag)
  {
    this._relation = relation;
  }

  protected override void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
    XmlElement element = xmlDocument.CreateElement(PortalConsts.XmlNodeSysAttribute);
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_GUID", this._relation.GUID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_PROJECT_GUID", this.GetObjectGuid(this.session, this._relation.ProjID));
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_PART_GUID", (this.tag as Attributes4RelationTag).PartObjectGuid.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_CREATE_DATE", DateTimeHelper.ToString(this.ToUTCDateTime(this._relation.CreateDate, this.session)));
    IDBRelationType relationType = this.session.GetRelationType(this._relation.RelationType);
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_RELATION_TYPE_GUID", (relationType as IDBGuid).GUID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_RELATION_TYPE_NAME", relationType.Description);
    IDBAttribute attributeByGuid = this._relation.GetAttributeByGuid(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid != null && attributeByGuid.AsInteger != 0L)
      XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_COMP_VERSION_ID", this.GetObjectGuid(this.session, attributeByGuid.AsInteger));
    if (this._relation.CreatorID != 0L)
      XMLFileHelper.AddAttribute(xmlDocument, (XmlNode) element, "F_REL_CREATOR", this.GetObjectGuid(this.session, this._relation.CreatorID));
    xmlRootNode.AppendChild((XmlNode) element);
  }

  protected override IDBAttributeCollection Attributes => this._relation.Attributes;

  protected override Guid TypeGuid
  {
    get => MetaDataHelper.GetRelationTypeGuid(this.Attributes.ObjectType);
  }
}
