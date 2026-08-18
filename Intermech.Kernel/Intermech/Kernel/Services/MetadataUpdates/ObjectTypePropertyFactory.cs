// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObjectTypePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObjectTypePropertyFactory : 
  TypePropertyFactory<AttributeType4ObjectTypePropertyFactory, ObjectTypeOptions>
{
  public readonly List<ApplicabilityPropertyFactory> ApplicabilityFactories = new List<ApplicabilityPropertyFactory>();

  protected override IPropertyFactory OnGetFactory4Category(
    IUserSession session,
    int nodeCategoryID,
    Guid nodeGuid)
  {
    if (nodeCategoryID != 6)
      return base.OnGetFactory4Category(session, nodeCategoryID, nodeGuid);
    ApplicabilityPropertyFactory factory4Category = new ApplicabilityPropertyFactory(nodeGuid);
    this.ApplicabilityFactories.Add(factory4Category);
    return (IPropertyFactory) factory4Category;
  }

  public ObjectTypeProperties GetObjectTypeProperties(Guid guid)
  {
    return new ObjectTypeProperties(-1, this.GetPropertyValue<string>("F_OBJ_TYPE_NAME", string.Empty), this.GetPropertyValue<string>("F_OBJ_NAME", string.Empty), this.GetPropertyValue<string>("F_NOTE", string.Empty), this.GetPropertyValue<ObjectVersionModes>("F_VERSIONABLE", ObjectVersionModes.SingleVersion), this.GetPropertyValue<int>("F_DEFAULT_RELATION", -1), this.GetPropertyValue<string>("F_AREA_ID", string.Empty), guid, this.GetPropertyValue<int>("F_CAPTION_ATTRIBUTE", 0), this.GetPropertyValue<bool>("F_ANY_ATTRIBUTES", true), this.GetPropertyValue<InheritModes>("F_PUBLIC_LC", InheritModes.Public), this.GetPropertyValue<string>("F_SHORT_NAME", string.Empty), this.GetPropertyValue<int>("F_DEL_TIME", 0), this.GetOptions(ObjectTypeOptions.None), this.GetPropertyValue<int>("F_SCHEMA_ID", 0));
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_CAPTION_ATTRIBUTE":
        propertyNode = (IPropertyNode) new AttributeIDNode(session, node, nodeID);
        break;
      case "F_CLASSIFY_TYPE":
        propertyNode = (IPropertyNode) new EnumNode<ObjectsClassifyType>(session, node, nodeID);
        break;
      case "F_DEFAULT_RELATION":
        propertyNode = (IPropertyNode) new RelationTypeNode(session, node, nodeID, 0);
        break;
      case "F_DEL_TIME":
        propertyNode = (IPropertyNode) new XMLPropertyNode<int>(session, node, nodeID);
        break;
      case "F_PARENT_ID":
        propertyNode = (IPropertyNode) new ObjectTypeNode(session, node, nodeID);
        break;
      case "F_PUBLIC_LC":
        propertyNode = (IPropertyNode) new EnumNode<InheritModes>(session, node, nodeID);
        break;
      case "F_SCHEMA_ID":
        propertyNode = (IPropertyNode) new LCShemaNode(session, node, nodeID);
        break;
      case "F_VERSIONABLE":
        propertyNode = (IPropertyNode) new EnumNode<ObjectVersionModes>(session, node, nodeID);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }

  protected override AttributeType4ObjectTypePropertyFactory GetAttributeType4TypePropertyFactory(
    Guid attributeGuid,
    int attributeID)
  {
    return new AttributeType4ObjectTypePropertyFactory(attributeGuid, attributeID);
  }
}
