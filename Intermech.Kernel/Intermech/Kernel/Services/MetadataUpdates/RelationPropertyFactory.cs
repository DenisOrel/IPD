// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.RelationPropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class RelationPropertyFactory : AttributablePropertyFactory
{
  public Guid RelationGuid { get; }

  public RelationPropertyFactory(Guid guid) => this.RelationGuid = guid;

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_RELATION_TYPE":
        propertyNode = (IPropertyNode) new RelationTypeNode(session, node, nodeID);
        break;
      case "F_PROJ_ID":
        propertyNode = (IPropertyNode) new ObjectIDNode(session, node, nodeID);
        break;
      case "F_PART_ID":
        propertyNode = (IPropertyNode) new IDNode(session, node, nodeID);
        break;
      case "F_CREATE_DATE":
        propertyNode = (IPropertyNode) new DateTimeNode(session, node, nodeID, DateTime.Now);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }
}
