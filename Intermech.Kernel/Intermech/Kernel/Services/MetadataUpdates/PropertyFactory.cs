// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.PropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class PropertyFactory : IPropertyFactory
{
  protected readonly List<IPropertyNode> propertyNodes = new List<IPropertyNode>();

  public string Directory { get; set; } = string.Empty;

  public void Read(IUserSession session, XmlNode rootNode)
  {
    if (rootNode == null)
      throw new ArgumentNullException(nameof (rootNode));
    this.propertyNodes.Clear();
    foreach (XmlNode childNode in rootNode.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element)
        this.HandleNode(session, childNode);
    }
  }

  private void HandleNode(IUserSession session, XmlNode node)
  {
    if (node.Name.Equals("Property"))
    {
      if (node.Attributes["Id"] == null)
        throw new Exception($"Нод должен иметь атрибут {"Id"} с идентификатором свойства!");
      this.propertyNodes.Add(this.GetPropertyNode(session, node, node.Attributes["Id"].Value));
    }
    else
      this.OnHandleNode(session, node);
  }

  protected virtual void OnHandleNode(IUserSession session, XmlNode node)
  {
  }

  protected virtual IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_ACCESS":
        propertyNode = (IPropertyNode) new AccessNode(session, node);
        break;
      case "F_AREA_ID":
        propertyNode = (IPropertyNode) new AreaNode(session, node, this.Directory);
        break;
      case "F_DEFAULT":
        propertyNode = (IPropertyNode) new BooleanNode(session, node, nodeID);
        break;
      case "F_EXTENSIONS":
        propertyNode = (IPropertyNode) new ExtensionNode(session, node);
        break;
      case "F_ICON":
        propertyNode = (IPropertyNode) new SimpleFileNode(session, node, nodeID, this.Directory);
        break;
      case "F_LANGUAGE_ID":
        propertyNode = (IPropertyNode) new LanguageNode(session, node);
        break;
      case "F_LEVEL_ID":
        propertyNode = (IPropertyNode) new LevelNode(session, node);
        break;
      default:
        propertyNode = (IPropertyNode) new StringNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }

  public TValue GetPropertyValue<TValue>(string propertyName)
  {
    return (TValue) (this.propertyNodes.Find((Predicate<IPropertyNode>) (x => x.Name.Equals(propertyName))) ?? throw new Exception($"Свойство {propertyName} не найдено.")).Value;
  }

  public TValue GetPropertyValue<TValue>(string propertyName, TValue defaultValue)
  {
    IPropertyNode propertyNode = this.propertyNodes.Find((Predicate<IPropertyNode>) (x => x.Name.Equals(propertyName)));
    return propertyNode != null ? (TValue) propertyNode.Value : defaultValue;
  }

  public TValue GetObligatoryPropertyValue<TValue>(string propertyName, TValue defaultValue)
  {
    IPropertyNode propertyNode = this.propertyNodes.Find((Predicate<IPropertyNode>) (x => x.Name.Equals(propertyName)));
    return propertyNode != null && propertyNode.Obligatory ? (TValue) propertyNode.Value : defaultValue;
  }

  public virtual List<ObligatoryElementKey> ObligatoryElements
  {
    get
    {
      List<ObligatoryElementKey> obligatoryElements = new List<ObligatoryElementKey>();
      foreach (IPropertyNode propertyNode in this.propertyNodes)
      {
        if (propertyNode.Obligatory)
          obligatoryElements.Add(ObligatoryElementKeys.GetKeyForObjectProperty(propertyNode.Name));
      }
      return obligatoryElements;
    }
  }

  public bool IsPropertyObligatory(string propertyName)
  {
    IPropertyNode propertyNode = this.propertyNodes.Find((Predicate<IPropertyNode>) (x => x.Name.Equals(propertyName)));
    return propertyNode != null && propertyNode.Obligatory;
  }
}
