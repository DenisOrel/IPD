// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ApplicabilityPropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ApplicabilityPropertyFactory : OptionizedPropertyFactory<ApplicabilityOptions>
{
  public Guid RelationTypeGuid { get; }

  public ApplicabilityPropertyFactory(Guid guid) => this.RelationTypeGuid = guid;

  public RelationsApplicabilityProperties GetApplicabilityProperties(
    int objectType,
    int inObjectType,
    int relationType)
  {
    return new RelationsApplicabilityProperties(-1, objectType, inObjectType, relationType, this.GetPropertyValue<bool>("F_CLONE_RELATIONS", false), this.GetPropertyValue<int>("F_MAX_LINKS", int.MaxValue), this.GetPropertyValue<ApplicabilityModes>("F_MIN_LINKS", ApplicabilityModes.Enabled), this.GetPropertyValue<RelationConstraintModes>("F_CONSTRAINT_MODE", RelationConstraintModes.None), this.GetPropertyValue<bool>("F_CHKOUTFILE", false), this.GetPropertyValue<bool>("F_CONTENT", false), this.GetOptions(ApplicabilityOptions.None));
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_CHKOUTFILE":
      case "F_CLONE_RELATIONS":
      case "F_CONTENT":
        propertyNode = (IPropertyNode) new BooleanNode(session, node, nodeID);
        break;
      case "F_CONSTRAINT_MODE":
        propertyNode = (IPropertyNode) new EnumNode<RelationConstraintModes>(session, node, nodeID);
        break;
      case "F_MAX_LINKS":
        propertyNode = (IPropertyNode) new XMLPropertyNode<int>(session, node, nodeID);
        break;
      case "F_MIN_LINKS":
        propertyNode = (IPropertyNode) new EnumNode<ApplicabilityModes>(session, node, nodeID);
        break;
      case "F_OBJECT_TYPE":
        propertyNode = (IPropertyNode) new CalculateObjectTypeNode(session, node, nodeID);
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
}
