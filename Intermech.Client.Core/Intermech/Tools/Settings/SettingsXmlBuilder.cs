
// Type: Intermech.Tools.Settings.SettingsXmlBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Tools.Settings;

public sealed class SettingsXmlBuilder
{
  private static readonly Type[] convertTypes = new Type[6]
  {
    typeof (string),
    typeof (long),
    typeof (int),
    typeof (Guid),
    typeof (bool),
    typeof (double)
  };
  private XmlDocument xml;

  public SettingsXmlBuilder(XmlDocument xml)
  {
    this.xml = xml != null ? xml : throw new ArgumentNullException(nameof (xml));
  }

  public XmlDocument Xml => this.xml;

  public XmlNode AppendElement(XmlNode topLevelElement)
  {
    if (topLevelElement == null)
      throw new ArgumentNullException(nameof (topLevelElement));
    this.xml.DocumentElement.AppendChild(topLevelElement);
    return topLevelElement;
  }

  public XmlElement CreateElement(string name) => this.xml.CreateElement(name);

  public XmlNodeList SelectNodes(string xpath) => this.xml.DocumentElement.SelectNodes(xpath);

  public XmlNode SelectSingleNode(string xpath) => this.xml.DocumentElement.SelectSingleNode(xpath);

  public XmlElement EncodeMetadataList(
    string listName,
    string itemName,
    IEnumerable<GlobalId<int>> list)
  {
    if (listName == null)
      throw new ArgumentNullException(nameof (listName));
    if (itemName == null)
      throw new ArgumentNullException(nameof (itemName));
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    XmlElement element1 = this.xml.CreateElement(listName);
    foreach (GlobalId<int> globalId in list)
    {
      XmlElement element2 = this.xml.CreateElement(itemName);
      this.AppendAttribute((XmlNode) element2, "guid", (object) globalId.Guid);
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  public XmlElement EncodeObjectTypes(
    string listName,
    string itemName,
    IEnumerable<GlobalId<int>> list)
  {
    return this.EncodeMetadataList(listName, itemName, list);
  }

  public XmlElement EncodeDocumentTypes(string listName, IEnumerable<GlobalId<int>> list)
  {
    return this.EncodeObjectTypes(listName, "Document", list);
  }

  public List<GlobalId<int>> DecodeObjectTypes(
    string listName,
    string itemName,
    XmlNode parentNode)
  {
    if (listName == null)
      throw new ArgumentNullException(nameof (listName));
    if (itemName == null)
      throw new ArgumentNullException(nameof (itemName));
    parentNode = this.ValidateParentNode(parentNode);
    XmlNodeList xmlNodeList = parentNode.SelectNodes($"{listName}/{itemName}[@guid]");
    List<GlobalId<int>> globalIdList = new List<GlobalId<int>>(xmlNodeList.Count);
    if (xmlNodeList.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (XmlNode parentNode1 in xmlNodeList)
        {
          Guid guid = this.ReadAttribute<Guid>(parentNode1, "guid", Guid.Empty);
          if (!(guid == Guid.Empty))
          {
            IDBObjectType objectType = sessionKeeper.Session.GetObjectType(guid, false);
            if (objectType != null)
              globalIdList.Add(new GlobalId<int>(guid, objectType.ObjectType, objectType.ObjectTypeName));
          }
        }
      }
    }
    return globalIdList;
  }

  public List<GlobalId<int>> DecodeDocumentTypes(string listName, XmlNode parentNode)
  {
    return this.DecodeObjectTypes(listName, "Document", parentNode);
  }

  public List<GlobalId<int>> DecodeDocumentTypes(string listName)
  {
    return this.DecodeDocumentTypes(listName, (XmlNode) null);
  }

  public XmlElement EncodeObjectAttributes(string listName, IEnumerable<GlobalId<int>> list)
  {
    return this.EncodeMetadataList(listName, "Attribute", list);
  }

  public List<GlobalId<int>> DecodeObjectAttributes(
    string listName,
    string itemName,
    XmlNode parentNode)
  {
    if (listName == null)
      throw new ArgumentNullException(nameof (listName));
    if (itemName == null)
      throw new ArgumentNullException(nameof (itemName));
    parentNode = this.ValidateParentNode(parentNode);
    XmlNodeList xmlNodeList = parentNode.SelectNodes($"{listName}/{itemName}[@guid]");
    List<GlobalId<int>> globalIdList = new List<GlobalId<int>>(xmlNodeList.Count);
    if (xmlNodeList.Count > 0)
    {
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      foreach (XmlNode parentNode1 in xmlNodeList)
      {
        Guid guid = this.ReadAttribute<Guid>(parentNode1, "guid", Guid.Empty);
        if (!(guid == Guid.Empty))
        {
          IDBAttributeTypeInfo attributeType = service.GetAttributeType(guid, false);
          if (attributeType != null)
            globalIdList.Add(new GlobalId<int>(guid, attributeType.AttributeID, attributeType.Name));
        }
      }
    }
    return globalIdList;
  }

  public List<GlobalId<int>> DecodeObjectAttributes(string listName, XmlNode parentNode)
  {
    return this.DecodeObjectAttributes(listName, "Attribute", parentNode);
  }

  public List<GlobalId<int>> DecodeObjectAttributes(string listName)
  {
    return this.DecodeObjectAttributes(listName, (XmlNode) null);
  }

  public XmlElement EncodeTextList(string listName, string itemName, ICollection<string> list)
  {
    if (listName == null)
      throw new ArgumentNullException(nameof (listName));
    if (itemName == null)
      throw new ArgumentNullException(nameof (itemName));
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    XmlElement element = this.xml.CreateElement(listName);
    if (list.Count > 0)
    {
      foreach (string text in (IEnumerable<string>) list)
        element.AppendChild((XmlNode) this.EncodeText(itemName, text));
    }
    return element;
  }

  public List<string> DecodeTextList(string listName, string itemName, XmlNode parentNode)
  {
    if (listName == null)
      throw new ArgumentNullException(nameof (listName));
    if (itemName == null)
      throw new ArgumentNullException(nameof (itemName));
    parentNode = this.ValidateParentNode(parentNode);
    XmlNode xmlNode1 = parentNode.SelectSingleNode(listName);
    if (xmlNode1 == null)
      return new List<string>(0);
    XmlNodeList xmlNodeList = xmlNode1.SelectNodes(itemName);
    List<string> stringList = new List<string>(xmlNodeList.Count);
    foreach (XmlNode xmlNode2 in xmlNodeList)
    {
      if (xmlNode2.FirstChild != null)
        stringList.Add(xmlNode2.FirstChild.Value);
    }
    return stringList;
  }

  public List<string> DecodeTextList(string listName, string itemName)
  {
    return this.DecodeTextList(listName, itemName, (XmlNode) null);
  }

  public XmlElement EncodeText(string nodeName, string text)
  {
    XmlElement xmlElement = nodeName != null ? this.xml.CreateElement(nodeName) : throw new ArgumentNullException(nameof (nodeName));
    if (text != null)
      xmlElement.AppendChild((XmlNode) this.xml.CreateTextNode(text));
    return xmlElement;
  }

  public string DecodeText(string nodeName, string defaultText)
  {
    return this.DecodeText((XmlNode) null, nodeName, defaultText);
  }

  public string DecodeText(XmlNode parentNode, string nodeName, string defaultText)
  {
    if (nodeName == null)
      throw new ArgumentNullException(nameof (nodeName));
    parentNode = this.ValidateParentNode(parentNode);
    return !(parentNode.SelectSingleNode(nodeName) is XmlElement node) ? defaultText : this.ReadText((XmlNode) node, defaultText);
  }

  public string ReadText(XmlNode node, string defaultText)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    return node.FirstChild == null ? defaultText : node.FirstChild.Value;
  }

  public XmlAttribute AppendAttribute(XmlNode parentNode, string name, object value)
  {
    if (parentNode == null)
      throw new ArgumentNullException(nameof (parentNode));
    this.CheckOwnerDocument(parentNode);
    XmlAttribute attribute = this.CreateAttribute(name, value);
    parentNode.Attributes.Append(attribute);
    return attribute;
  }

  public XmlAttribute CreateAttribute(string name, object value)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    string text = SettingsXmlBuilder.AttributeValueToString(value);
    XmlAttribute attribute = this.xml.CreateAttribute(name);
    attribute.AppendChild((XmlNode) this.xml.CreateTextNode(text));
    return attribute;
  }

  public string ReadAttribute(XmlNode parentNode, string name, string defaultValue)
  {
    if (parentNode == null)
      throw new ArgumentNullException(nameof (parentNode));
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    XmlAttribute attribute = parentNode.Attributes[name];
    return attribute == null || attribute.Value == null ? defaultValue : attribute.Value;
  }

  public T ReadAttribute<T>(XmlNode parentNode, string name, T defaultValue)
  {
    string stringValue = this.ReadAttribute(parentNode, name, (string) null);
    if (string.IsNullOrEmpty(stringValue))
      return defaultValue;
    try
    {
      return (T) SettingsXmlBuilder.StringToAttributeValue(stringValue, typeof (T));
    }
    catch (InvalidCastException ex)
    {
      throw;
    }
    catch
    {
      return defaultValue;
    }
  }

  private XmlNode ValidateParentNode(XmlNode parentNode)
  {
    if (parentNode == null)
      parentNode = (XmlNode) this.xml.DocumentElement;
    else
      this.CheckOwnerDocument(parentNode);
    return parentNode;
  }

  private void CheckOwnerDocument(XmlNode node)
  {
    if (node.OwnerDocument != null && node.OwnerDocument != this.xml)
      throw new InvalidOperationException("The xml node is owned by another xml document.");
  }

  private static string AttributeValueToString(object value)
  {
    if (value != null)
    {
      Type type = value.GetType();
      if (type == SettingsXmlBuilder.convertTypes[0])
        return (string) value;
      if (type == SettingsXmlBuilder.convertTypes[1])
        return XmlConvert.ToString((long) value);
      if (type == SettingsXmlBuilder.convertTypes[2])
        return XmlConvert.ToString((int) value);
      if (type == SettingsXmlBuilder.convertTypes[3])
        return XmlConvert.ToString((Guid) value);
      if (type == SettingsXmlBuilder.convertTypes[4])
        return XmlConvert.ToString((bool) value);
      if (type == SettingsXmlBuilder.convertTypes[5])
        return XmlConvert.ToString((double) value);
    }
    throw new FormatException($"Can't convert value '{value}' to a xml attribute. Unsupported data type.");
  }

  private static object StringToAttributeValue(string stringValue, Type dataType)
  {
    if (dataType == (Type) null)
      throw new ArgumentNullException(nameof (dataType));
    if (dataType == SettingsXmlBuilder.convertTypes[0])
      return (object) stringValue;
    if (dataType == SettingsXmlBuilder.convertTypes[1])
      return (object) XmlConvert.ToInt64(stringValue);
    if (dataType == SettingsXmlBuilder.convertTypes[2])
      return (object) XmlConvert.ToInt32(stringValue);
    if (dataType == SettingsXmlBuilder.convertTypes[3])
      return (object) XmlConvert.ToGuid(stringValue);
    if (dataType == SettingsXmlBuilder.convertTypes[4])
      return (object) XmlConvert.ToBoolean(stringValue);
    if (dataType == SettingsXmlBuilder.convertTypes[5])
      return (object) XmlConvert.ToDouble(stringValue);
    throw new FormatException($"Can't convert value '{stringValue}' from a xml attribute. Unsupported data type.");
  }
}
