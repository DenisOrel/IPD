// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObjectPropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObjectPropertyFactory : AttributablePropertyFactory
{
  public readonly List<RelationPropertyFactory> RelationFactories = new List<RelationPropertyFactory>();

  protected override void OnHandleNode(IUserSession session, XmlNode node)
  {
    if (!node.Name.Equals("Object") || Convert.ToInt32(node.Attributes["CategoryID"].Value) != 5)
      return;
    RelationPropertyFactory relationPropertyFactory = new RelationPropertyFactory(new Guid(node.Attributes["Guid"].Value));
    this.RelationFactories.Add(relationPropertyFactory);
    relationPropertyFactory.Read(session, node);
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_LC_STEP":
        propertyNode = (IPropertyNode) new LCStepNode(session, node);
        break;
      case "F_OWNER_ID":
      case "F_PARENT_ID":
      case "F_PROJECT_ID":
        propertyNode = (IPropertyNode) new OutsideObjectIDNode(session, node, nodeID);
        break;
      case "F_OBJECT_TYPE":
        propertyNode = (IPropertyNode) new ObjectTypeNode(session, node, nodeID);
        break;
      case "F_GUID":
        propertyNode = (IPropertyNode) new GuidNode(session, node, nodeID);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }
}
