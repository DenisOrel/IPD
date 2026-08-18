// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeType4TypePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class AttributeType4TypePropertyFactory : AttributeTypePropertyFactory
{
  public Guid AttributeGUID { get; private set; }

  public int AttributeID { get; private set; }

  public AttributeType4TypePropertyFactory(Guid attributeGuid, int attributeID)
  {
    this.AttributeGUID = attributeGuid;
    this.AttributeID = attributeID;
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_REQUIRED":
        propertyNode = (IPropertyNode) new EnumNode<RequiredModes>(session, node, nodeID);
        break;
      case "F_PUBLIC":
        propertyNode = (IPropertyNode) new EnumNode<InheritModes>(session, node, nodeID);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }

  public override List<ObligatoryElementKey> ObligatoryElements
  {
    get
    {
      List<ObligatoryElementKey> obligatoryElements = new List<ObligatoryElementKey>();
      foreach (IPropertyNode propertyNode in this.propertyNodes)
      {
        if (propertyNode.Obligatory)
          obligatoryElements.Add(ObligatoryElementKeys.GetKeyForAttributeProperty(this.AttributeID, propertyNode.Name));
      }
      return obligatoryElements;
    }
  }
}
