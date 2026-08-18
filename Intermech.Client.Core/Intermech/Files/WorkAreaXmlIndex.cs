
// Type: Intermech.Files.WorkAreaXmlIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Intermech.Files;

internal sealed class WorkAreaXmlIndex : IWorkAreaIndex
{
  private readonly string indexFilePath;
  private readonly XmlDocument index;

  public WorkAreaXmlIndex(string indexFilePath)
  {
    this.indexFilePath = !string.IsNullOrEmpty(indexFilePath) ? indexFilePath : throw new ArgumentNullException();
    this.index = new XmlDocument();
    this.InitIndex();
  }

  private void UpdateIndexFormat()
  {
    XmlNode node = this.index.DocumentElement.SelectSingleNode("@format");
    if (node == null)
    {
      node = (XmlNode) this.index.CreateAttribute("format");
      node.Value = "1";
      this.index.DocumentElement.Attributes.Append((XmlAttribute) node);
    }
    int int32 = XmlConvert.ToInt32(node.Value);
    if (int32 == 2)
      return;
    try
    {
      if (int32 == 1)
        this.UpdateFromVersion1();
      node.Value = XmlConvert.ToString(2);
      this.AcceptChanges();
    }
    catch
    {
      this.RejectChanges();
      throw;
    }
  }

  private void UpdateFromVersion1()
  {
    foreach (XmlNode selectNode in this.index.SelectNodes("//ObjectState/ObjectFile"))
      selectNode.ParentNode.RemoveChild(selectNode);
  }

  public void Append(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException();
    this.AppendCore(objectState);
    this.AcceptChanges();
  }

  private void AppendCore(DBObjectState objectState)
  {
    this.index.DocumentElement.AppendChild((XmlNode) this.ObjectStateToNode(objectState));
  }

  public void Remove(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException();
    this.RemoveCore(objectState.Id);
    this.AcceptChanges();
  }

  private void RemoveCore(long id)
  {
    XmlNode nodeById = this.FindNodeById(id);
    if (nodeById == null || nodeById.ParentNode == null)
      return;
    nodeById.ParentNode.RemoveChild(nodeById);
  }

  public void Update(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException();
    this.RemoveCore(objectState.Id);
    this.AppendCore(objectState);
    this.AcceptChanges();
  }

  public void BatchAppend(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException();
    if (list.Count <= 0)
      return;
    try
    {
      foreach (DBObjectState objectState in (IEnumerable<DBObjectState>) list)
        this.AppendCore(objectState);
      this.AcceptChanges();
    }
    catch
    {
      this.RejectChanges();
      throw;
    }
  }

  public void BatchRemove(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException();
    if (list.Count <= 0)
      return;
    try
    {
      foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) list)
        this.RemoveCore(dbObjectState.Id);
      this.AcceptChanges();
    }
    catch
    {
      this.RejectChanges();
      throw;
    }
  }

  public void BatchUpdate(
    ICollection<DBObjectState> updateList,
    ICollection<DBObjectState> appendList)
  {
    if (updateList == null)
      throw new ArgumentNullException();
    if (appendList == null)
      throw new ArgumentNullException();
    if (updateList.Count <= 0 && appendList.Count <= 0)
      return;
    try
    {
      foreach (DBObjectState update in (IEnumerable<DBObjectState>) updateList)
      {
        this.RemoveCore(update.Id);
        this.AppendCore(update);
      }
      foreach (DBObjectState append in (IEnumerable<DBObjectState>) appendList)
        this.AppendCore(append);
      this.AcceptChanges();
    }
    catch
    {
      this.RejectChanges();
      throw;
    }
  }

  public bool Contains(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    return this.FindNodeByVersionId(objectId) != null;
  }

  public DBObjectState Find(long id)
  {
    XmlNode node = id != -1L ? this.FindNodeById(id) : throw new ArgumentException();
    return node == null ? (DBObjectState) null : this.NodeToObjectState(node);
  }

  public DBObjectState FindByVersionId(long objectId)
  {
    XmlNode node = objectId != 0L ? this.FindNodeByVersionId(objectId) : throw new ArgumentException();
    return node == null ? (DBObjectState) null : this.NodeToObjectState(node);
  }

  public DateTime? GetPublishTime(long objectId) => throw new NotSupportedException();

  public List<DBObjectState> Query()
  {
    XmlNodeList xmlNodeList = this.index.SelectNodes("//ObjectState");
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>(xmlNodeList.Count);
    for (int i = 0; i < xmlNodeList.Count; ++i)
      dbObjectStateList.Add(this.NodeToObjectState(xmlNodeList[i]));
    return dbObjectStateList;
  }

  public List<DBObjectState> QueryNotUsed(DateTime noUseSinceDate)
  {
    XmlNodeList xmlNodeList = this.index.SelectNodes($"//ObjectState[not (@LastUsed) or (@LastUsed < {XmlConvert.ToString(noUseSinceDate.Date.Ticks)})]");
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>(xmlNodeList.Count);
    for (int i = 0; i < xmlNodeList.Count; ++i)
      dbObjectStateList.Add(this.NodeToObjectState(xmlNodeList[i]));
    return dbObjectStateList;
  }

  public void Flush()
  {
  }

  private void InitIndex()
  {
    if (File.Exists(this.indexFilePath))
    {
      this.index.Load(this.indexFilePath);
      this.UpdateIndexFormat();
    }
    else
    {
      this.index.AppendChild((XmlNode) this.index.CreateXmlDeclaration("1.0", "UTF-16", (string) null));
      this.index.AppendChild((XmlNode) this.index.CreateElement("Index"));
    }
  }

  private void AcceptChanges() => this.index.Save(this.indexFilePath);

  private void RejectChanges()
  {
    this.index.RemoveAll();
    this.InitIndex();
  }

  private XmlNode FindNodeByVersionId(long objectId)
  {
    return this.index.SelectSingleNode($"//ObjectState[@ObjectId = '{XmlConvert.ToString(objectId)}']");
  }

  private XmlNode FindNodeById(long id)
  {
    return this.index.SelectSingleNode($"//ObjectState[@Id = '{XmlConvert.ToString(id)}']");
  }

  private XmlElement ObjectStateToNode(DBObjectState objectState)
  {
    XmlElement element = this.index.CreateElement("ObjectState");
    element.Attributes.Append(this.CreateAttribute("Id", XmlConvert.ToString(objectState.Id)));
    element.Attributes.Append(this.CreateAttribute("ObjectId", XmlConvert.ToString(objectState.ObjectId)));
    element.Attributes.Append(this.CreateAttribute("ModifyMode", Enum.GetName(typeof (ObjectModifyModes), (object) objectState.ModifyMode)));
    element.Attributes.Append(this.CreateAttribute("Caption", objectState.Caption));
    XmlAttributeCollection attributes = element.Attributes;
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.Date;
    XmlAttribute attribute = this.CreateAttribute("LastUsed", XmlConvert.ToString(dateTime.Ticks));
    attributes.Append(attribute);
    return element;
  }

  private DBObjectState NodeToObjectState(XmlNode node)
  {
    long int64_1 = XmlConvert.ToInt64(this.GetAttribute(node, "Id"));
    long int64_2 = XmlConvert.ToInt64(this.GetAttribute(node, "ObjectId"));
    ObjectModifyModes objectModifyModes = (ObjectModifyModes) Enum.Parse(typeof (ObjectModifyModes), this.GetAttribute(node, "ModifyMode"));
    string attribute = this.GetAttribute(node, "Caption");
    long objectId = int64_2;
    int modifyMode = (int) objectModifyModes;
    string caption = attribute;
    return new DBObjectState(int64_1, objectId, (ObjectModifyModes) modifyMode, caption);
  }

  private XmlAttribute CreateAttribute(string name, string value)
  {
    XmlAttribute attribute = this.index.CreateAttribute(name);
    attribute.Value = value;
    return attribute;
  }

  private string GetAttribute(XmlNode node, string name)
  {
    XmlAttribute attribute = node.Attributes[name];
    return attribute != null && attribute.Value != null ? attribute.Value : throw new NotImplementedException();
  }
}
