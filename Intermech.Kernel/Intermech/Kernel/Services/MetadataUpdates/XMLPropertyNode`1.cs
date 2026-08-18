// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.XMLPropertyNode`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class XMLPropertyNode<TValue> : IPropertyNode
{
  public string Name { get; protected set; }

  public object Value { get; protected set; }

  public bool Obligatory { get; }

  public XMLPropertyNode(IUserSession session, XmlNode node, string nodeID, bool readValue)
  {
    this.Name = nodeID;
    if (node.Attributes[nameof (Obligatory)] == null)
      throw new Exception($"Нод должен иметь атрибут {nameof (Obligatory)} с флагом обязательности свойства!");
    this.Obligatory = Convert.ToBoolean(node.Attributes[nameof (Obligatory)].Value);
    if (!readValue)
      return;
    this.ReadValue(session, node);
  }

  public XMLPropertyNode(IUserSession session, XmlNode node, string nodeID)
    : this(session, node, nodeID, true)
  {
  }

  protected virtual void ReadValue(IUserSession session, XmlNode node)
  {
    if (!node.HasChildNodes)
      return;
    XmlNode childNode = node.ChildNodes[0];
    if (!(childNode.Name == "PropValue"))
      return;
    XmlAttribute attribute = childNode.Attributes["Value"];
    if (attribute == null)
      return;
    this.Value = (object) this.GetValue(session, attribute.Value);
  }

  protected virtual TValue GetValue(IUserSession session, string nodeAttributeValue)
  {
    return (TValue) Convert.ChangeType((object) nodeAttributeValue, typeof (TValue));
  }
}
