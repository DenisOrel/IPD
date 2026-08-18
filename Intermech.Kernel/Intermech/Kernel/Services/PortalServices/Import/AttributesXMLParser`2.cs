// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.AttributesXMLParser`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal abstract class AttributesXMLParser<TParent, TImportingObject>
  where TParent : IDBAttributableType
  where TImportingObject : ImportingAttributable
{
  protected Dictionary<Guid, long> measures = new Dictionary<Guid, long>();
  protected Dictionary<Guid, ImportedInfo> links;
  protected string path;

  public AttributesXMLParser(Dictionary<Guid, ImportedInfo> links, string path)
  {
    this.links = links;
    this.path = path;
  }

  protected virtual bool CheckChildNode(string nodeName)
  {
    return nodeName.Equals(PortalConsts.XmlNodeAttribute);
  }

  protected virtual bool OnReadNode(
    AttributeInfo attrInfo,
    TImportingObject importingObject,
    XmlNode node)
  {
    return true;
  }

  protected virtual void OnAfterReadAttributes(
    UserSession session,
    XmlNode rootNode,
    TParent parent,
    TImportingObject importingObject)
  {
  }

  protected abstract long UnknownAttributableId { get; }

  protected virtual void AddNullAttribute(
    int attributeID,
    TImportingObject importingObject,
    XmlNode node)
  {
    importingObject.AddAttribute(new AttributeRecord(attributeID, this.UnknownAttributableId));
  }

  protected virtual void AddAttribute(
    AttributeRecord attribute,
    TImportingObject importingObject,
    XmlNode node)
  {
    if (attribute.AttributeId <= 0)
      return;
    importingObject.AddAttribute(attribute);
  }

  protected virtual void OnMessage(
    string message,
    string attributeName,
    TImportingObject importingObject)
  {
  }

  protected abstract bool CheckAttribute4Type(
    TParent parent,
    IDBAttributeType attrType,
    IEventLogHelper eventHelper);
}
