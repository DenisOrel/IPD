
// Type: Intermech.Tools.Data.DBObjectErrorsBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Tools.Data;

public sealed class DBObjectErrorsBuilder
{
  private List<DBObjectErrorInfo> errors;

  public DBObjectErrorsBuilder() => this.errors = new List<DBObjectErrorInfo>();

  public DBObjectErrorsBuilder(string xmlString)
  {
    if (xmlString == null)
      throw new ArgumentNullException(nameof (xmlString));
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.PreserveWhitespace = true;
    xmlDocument.LoadXml(xmlString);
    XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Error[@uniqueId and @category and Text]");
    this.errors = new List<DBObjectErrorInfo>(xmlNodeList.Count);
    foreach (XmlNode node in xmlNodeList)
      this.errors.Add(this.ParseErrorNode(node));
  }

  public DBObjectErrorInfo TryGetById(string uniqueId)
  {
    return uniqueId != null ? this.FindErrorNode(uniqueId) : throw new ArgumentNullException(nameof (uniqueId));
  }

  private DBObjectErrorInfo FindErrorNode(string uniqueId)
  {
    return this.errors.Find((Predicate<DBObjectErrorInfo>) (error => error.UniqueId == uniqueId));
  }

  public List<DBObjectErrorInfo> GetAll()
  {
    return new List<DBObjectErrorInfo>((IEnumerable<DBObjectErrorInfo>) this.errors);
  }

  public List<DBObjectErrorInfo> GetAllByCategory(string category)
  {
    return category != null ? new List<DBObjectErrorInfo>((IEnumerable<DBObjectErrorInfo>) this.FindErrorNodesByCategory(category)) : throw new ArgumentNullException(nameof (category));
  }

  private List<DBObjectErrorInfo> FindErrorNodesByCategory(string category)
  {
    return this.errors.FindAll((Predicate<DBObjectErrorInfo>) (error => error.Category == category));
  }

  public void Add(DBObjectErrorInfo error)
  {
    if (error == null)
      throw new ArgumentNullException(nameof (error));
    if (this.FindErrorNode(error.UniqueId) != null)
      throw new InvalidOperationException($"Error id = '{error.UniqueId}' is not unique.");
    this.errors.Add(error);
  }

  public void AddRange(IEnumerable<DBObjectErrorInfo> errors)
  {
    if (errors == null)
      throw new ArgumentNullException(nameof (errors));
    foreach (DBObjectErrorInfo error in errors)
      this.Add(error);
  }

  public void Remove(DBObjectErrorInfo error)
  {
    if (error == null)
      throw new ArgumentNullException(nameof (error));
    this.errors.Remove(error);
  }

  public void RemoveById(string uniqueId)
  {
    if (uniqueId == null)
      throw new ArgumentNullException(nameof (uniqueId));
    this.errors.RemoveAll((Predicate<DBObjectErrorInfo>) (error => error.UniqueId == uniqueId));
  }

  public void RemoveByCategory(string category)
  {
    if (category == null)
      throw new ArgumentNullException(nameof (category));
    this.errors.RemoveAll((Predicate<DBObjectErrorInfo>) (error => error.Category == category));
  }

  public void Clear() => this.errors.Clear();

  public string ToXmlString()
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.PreserveWhitespace = true;
    xmlDocument.AppendChild((XmlNode) xmlDocument.CreateXmlDeclaration("1.0", "utf-8", (string) null));
    xmlDocument.AppendChild((XmlNode) xmlDocument.CreateElement("Errors"));
    foreach (DBObjectErrorInfo error in this.errors)
      xmlDocument.DocumentElement.AppendChild((XmlNode) this.CreateErrorNode(xmlDocument, error));
    return xmlDocument.OuterXml;
  }

  private XmlElement CreateErrorNode(XmlDocument xmlDocument, DBObjectErrorInfo error)
  {
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("uniqueId");
    attribute1.Value = error.UniqueId;
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("category");
    attribute2.Value = error.Category;
    XmlElement element1 = xmlDocument.CreateElement("Text");
    element1.AppendChild((XmlNode) xmlDocument.CreateTextNode(error.Text));
    XmlElement element2 = xmlDocument.CreateElement("Error");
    element2.Attributes.Append(attribute1);
    element2.Attributes.Append(attribute2);
    element2.AppendChild((XmlNode) element1);
    return element2;
  }

  private DBObjectErrorInfo ParseErrorNode(XmlNode node)
  {
    string uniqueId = node.Attributes["uniqueId"].Value;
    string str = node.Attributes["category"].Value;
    string innerText = node["Text"].InnerText;
    string category = str;
    string text = innerText;
    return new DBObjectErrorInfo(uniqueId, category, text);
  }
}
