// Decompiled with JetBrains decompiler
// Type: IMLauncher.XMLSettingsStorage
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using System;
using System.IO;
using System.Xml;

#nullable disable
namespace IMLauncher;

public class XMLSettingsStorage
{
  private XmlDocument _document = new XmlDocument();

  public XMLSettingsStorage() => this._document.LoadXml(LauncherConsts.xmlEmptyDoc);

  public XMLSettingsStorage(string FileName)
  {
    this._document.LoadXml(LauncherConsts.xmlEmptyDoc);
    this.Load(FileName);
  }

  public XMLSettingsStorage(Stream stream)
  {
    this._document.LoadXml(LauncherConsts.xmlEmptyDoc);
    this.Load(stream);
  }

  public XmlDocument document
  {
    get => this._document;
    set
    {
      if (value == null || this._document == value || value.DocumentElement == null)
        return;
      this._document = value;
    }
  }

  public bool Save(string FileName)
  {
    try
    {
      string directoryName = Path.GetDirectoryName(FileName);
      if (!Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
      try
      {
        return this.Save((Stream) fileStream);
      }
      finally
      {
        fileStream.Close();
        fileStream.Dispose();
      }
    }
    catch
    {
      return false;
    }
  }

  public bool Save(Stream stream)
  {
    if (stream == null)
      return false;
    try
    {
      this._document.Save(stream);
    }
    catch
    {
      return false;
    }
    return true;
  }

  public bool Load(string FileName)
  {
    if (!new FileInfo(FileName).Exists)
      return false;
    try
    {
      FileStream fileStream = new FileStream(FileName, FileMode.Open);
      try
      {
        return this.Load((Stream) fileStream);
      }
      finally
      {
        fileStream.Close();
        fileStream.Dispose();
      }
    }
    catch
    {
      return false;
    }
  }

  public bool Load(Stream stream)
  {
    if (stream != null && stream.Length != 0L)
    {
      if (stream.Position != stream.Length - 1L)
      {
        try
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.LoadXml(LauncherConsts.xmlEmptyDoc);
          xmlDocument.Load(stream);
          if (xmlDocument.DocumentElement == null)
            return false;
          this._document = xmlDocument;
        }
        catch
        {
          return false;
        }
        return true;
      }
    }
    return false;
  }

  public XmlNode AddNode(XmlNode parentNode, string childName)
  {
    if (parentNode == null || childName == string.Empty)
      return (XmlNode) null;
    XmlElement element = parentNode.OwnerDocument.CreateElement(childName);
    return parentNode.AppendChild((XmlNode) element);
  }

  public XmlNode FindNode(XmlNode parentNode, string childName, bool autoCreate)
  {
    if (parentNode == null || childName == string.Empty)
      return (XmlNode) null;
    for (int i = 0; i < parentNode.ChildNodes.Count; ++i)
    {
      if (!(parentNode.ChildNodes[i].Name != childName))
        return parentNode.ChildNodes[i];
    }
    if (!autoCreate)
      return (XmlNode) null;
    XmlElement element = parentNode.OwnerDocument.CreateElement(childName);
    return parentNode.AppendChild((XmlNode) element);
  }

  public XmlNode FindNodeWithAttr(
    XmlNode parentNode,
    string childName,
    string attrName,
    string attrValue,
    bool autoCreate)
  {
    if (parentNode == null || childName == string.Empty)
      return (XmlNode) null;
    for (int i = 0; i < parentNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = parentNode.ChildNodes[i];
      if (!(childNode.Name != childName))
      {
        XmlNode namedItem = childNode.Attributes.GetNamedItem(attrName);
        if (namedItem != null && namedItem.InnerText == attrValue)
          return childNode;
      }
    }
    if (!autoCreate)
      return (XmlNode) null;
    XmlElement element = parentNode.OwnerDocument.CreateElement(childName);
    XmlNode node = parentNode.AppendChild((XmlNode) element);
    this.SetAttributeValue(node, attrName, attrValue);
    return node;
  }

  public string GetAttributeValue(XmlNode node, string attrName, string defValue)
  {
    if (node == null || attrName == string.Empty || node.Attributes.Count == 0)
      return defValue;
    XmlNode namedItem = node.Attributes.GetNamedItem(attrName);
    return namedItem == null ? defValue : namedItem.InnerText;
  }

  public Guid GetAttributeAsGuid(XmlNode node, string attrName, Guid defValue)
  {
    if (node == null || attrName == string.Empty || node.Attributes.Count == 0)
      return defValue;
    XmlNode namedItem = node.Attributes.GetNamedItem(attrName);
    if (namedItem == null)
      return defValue;
    try
    {
      return new Guid(namedItem.InnerText);
    }
    catch
    {
      return defValue;
    }
  }

  public int GetAttributeAsInt32(XmlNode node, string attrName, int defValue)
  {
    if (node == null || attrName == string.Empty || node.Attributes.Count == 0)
      return defValue;
    XmlNode namedItem = node.Attributes.GetNamedItem(attrName);
    if (namedItem == null)
      return defValue;
    int result = defValue;
    return !int.TryParse(namedItem.InnerText, out result) ? defValue : result;
  }

  public long GetAttributeAsInt64(XmlNode node, string attrName, long defValue)
  {
    if (node == null || attrName == string.Empty || node.Attributes.Count == 0)
      return defValue;
    XmlNode namedItem = node.Attributes.GetNamedItem(attrName);
    if (namedItem == null)
      return defValue;
    long result = defValue;
    return !long.TryParse(namedItem.InnerText, out result) ? defValue : result;
  }

  public void SetAttributeValue(XmlNode node, string attrName, string value)
  {
    if (node == null || attrName == string.Empty)
      return;
    XmlNode namedItem = node.Attributes.GetNamedItem(attrName);
    if (namedItem != null)
    {
      namedItem.InnerText = value;
    }
    else
    {
      XmlAttribute attribute = node.OwnerDocument.CreateAttribute(attrName);
      attribute.InnerText = value;
      node.Attributes.Append(attribute);
    }
  }
}
