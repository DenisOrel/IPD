// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.RelationTypePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class RelationTypePropertyFactory : 
  TypePropertyFactory<AttributeType4RelationTypePropertyFactory, RelationTypeOptions>
{
  public RelationTypeProperties GetRelationTypeProperties(Guid guid)
  {
    return new RelationTypeProperties(-1, this.GetPropertyValue<string>("F_TYPE_NAME", string.Empty), this.GetPropertyValue<string>("F_REVERSE_NAME", string.Empty), this.GetPropertyValue<string>("F_NOTE", string.Empty), this.GetPropertyValue<bool>("F_CHKOUTFILE", false), this.GetPropertyValue<bool>("F_SAVE_HISTORY", false), this.GetPropertyValue<string>("F_DESCRIPTION", string.Empty), guid, this.GetPropertyValue<string>("F_AREA_ID", string.Empty), this.GetPropertyValue<bool>("F_ANY_ATTRIBUTES", true), this.GetPropertyValue<string>("F_SHORT_NAME", string.Empty), this.GetOptions(RelationTypeOptions.None));
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    return nodeID == "F_CHKOUTFILE" || nodeID == "F_SAVE_HISTORY" ? (IPropertyNode) new BooleanNode(session, node, nodeID) : base.GetPropertyNode(session, node, nodeID);
  }

  protected override AttributeType4RelationTypePropertyFactory GetAttributeType4TypePropertyFactory(
    Guid attributeGuid,
    int attributeID)
  {
    return new AttributeType4RelationTypePropertyFactory(attributeGuid, attributeID);
  }
}
