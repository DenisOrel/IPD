// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributablePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class AttributablePropertyFactory : PropertyFactory
{
  public List<AttributeNode> AttributeNodes
  {
    get
    {
      return this.propertyNodes.FindAll((Predicate<IPropertyNode>) (x => x is AttributeNode)).ConvertAll<AttributeNode>((Converter<IPropertyNode, AttributeNode>) (node => (AttributeNode) node));
    }
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    return !UpdateScriptHelper.IsAttributeNode(nodeID) ? base.GetPropertyNode(session, node, nodeID) : (IPropertyNode) new AttributeNode(session, node, UpdateScriptHelper.RemoveAttributeNodeNamePrefix(nodeID), this.Directory);
  }
}
