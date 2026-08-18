// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.TypePropertyFactory`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class TypePropertyFactory<TAttributeFactory, TOptions> : 
  OptionizedPropertyFactory<TOptions>
  where TAttributeFactory : AttributeType4TypePropertyFactory
  where TOptions : Enum, IConvertible
{
  public readonly List<TAttributeFactory> AttributeFactories = new List<TAttributeFactory>();

  protected override void OnHandleNode(IUserSession session, XmlNode node)
  {
    if (!node.Name.Equals("Object"))
      return;
    int int32 = Convert.ToInt32(node.Attributes["CategoryID"].Value);
    this.OnGetFactory4Category(session, int32, new Guid(node.Attributes["Guid"].Value))?.Read(session, node);
  }

  protected virtual IPropertyFactory OnGetFactory4Category(
    IUserSession session,
    int nodeCategoryID,
    Guid nodeGuid)
  {
    if (nodeCategoryID != 3)
      return (IPropertyFactory) null;
    IDBAttributeType attributeType = session.GetAttributeType(nodeGuid);
    TAttributeFactory typePropertyFactory = this.GetAttributeType4TypePropertyFactory(nodeGuid, attributeType.AttributeID);
    typePropertyFactory.FieldType = attributeType.AttributeType;
    this.AttributeFactories.Add(typePropertyFactory);
    return (IPropertyFactory) typePropertyFactory;
  }

  protected abstract TAttributeFactory GetAttributeType4TypePropertyFactory(
    Guid attributeGuid,
    int attributeID);

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    return !(nodeID == "F_ANY_ATTRIBUTES") ? base.GetPropertyNode(session, node, nodeID) : (IPropertyNode) new BooleanNode(session, node, nodeID);
  }
}
