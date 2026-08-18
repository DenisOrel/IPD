// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ConditionList
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Expert;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Workflow;

/// <summary>Summary description for ConditionList.</summary>
public class ConditionList : IEnumerable<ConditionInfo>, IEnumerable
{
  private List<ConditionInfo> _list = new List<ConditionInfo>();
  public bool Modified;
  private IUserSession _session;
  private bool _writeGuids;

  public ConditionList()
  {
  }

  public ConditionList(IDBAttribute attr)
  {
    if (attr == null)
      return;
    this._session = attr.Session;
    this.Load(attr);
  }

  public bool IsEmpty => this.Count == 0;

  public ConditionList(IDBObject activity)
  {
    IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrConditionID);
    this._session = activity.Session;
    if (attributeById == null)
      return;
    this.Load(attributeById);
  }

  public ConditionList(IUserSession session) => this._session = session;

  public ConditionInfo this[int index]
  {
    get => this._list[index];
    set => this._list[index] = value;
  }

  public void Load(IDBAttribute attr)
  {
    using (MemoryStream stream = StreamHelper.BlobReaderToStream(attr as IBlobReader))
      this.LoadFromStream((Stream) stream);
  }

  public void LoadFromStream(Stream stream)
  {
    if (stream.Length <= 0L)
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(stream);
    XmlElement documentElement = xmlDocument.DocumentElement;
    int int32 = Convert.ToInt32(documentElement.Attributes["Count"].Value);
    for (int i1 = 0; i1 < int32; ++i1)
    {
      XmlNode childNode = documentElement.ChildNodes[i1];
      long linkID = (long) Convert.ToInt32(childNode.ChildNodes[0].InnerText);
      int i2 = 1;
      if (childNode.ChildNodes.Count > 1 && childNode.ChildNodes[1].Name == "LinkGuid")
      {
        ++i2;
        IDBObject dbObject = this._session?.GetObject(new Guid(childNode.ChildNodes[1].InnerText), false);
        if (dbObject != null)
          linkID = dbObject.ObjectID;
      }
      XmlAttribute attribute = childNode.ChildNodes[0].Attributes["Else"];
      TempFormula tf = attribute == null || !"1".Equals(attribute.Value) ? new TempFormula(childNode.ChildNodes[i2]) : (TempFormula) null;
      this.Add(linkID, tf);
    }
  }

  public void SaveToStream(Stream stream)
  {
    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartElement("Conditions");
    writer.WriteAttributeString("Count", this.Count.ToString());
    for (int index = 0; index < this.Count; ++index)
    {
      ConditionInfo conditionInfo = this[index];
      writer.WriteStartElement("c" + index.ToString());
      writer.WriteStartElement("LinkID");
      if (conditionInfo.ExpertFormula == null)
        writer.WriteAttributeString("Else", "1");
      writer.WriteString(conditionInfo.LinkID.ToString());
      writer.WriteEndElement();
      if (this.WriteGuids)
      {
        IDBObject dbObject = this._session?.GetObject(conditionInfo.LinkID, false);
        if (dbObject != null)
        {
          writer.WriteStartElement("LinkGuid");
          writer.WriteString(dbObject.ObjectGUID.ToString());
          writer.WriteEndElement();
        }
      }
      if (conditionInfo.ExpertFormula != null)
        conditionInfo.ExpertFormula.WriteToXML(ref writer);
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
    writer.Flush();
  }

  public void Save(IDBAttribute attr)
  {
    using (MemoryStream ms = new MemoryStream())
    {
      this.SaveToStream((Stream) ms);
      StreamHelper.StreamToBlobWriter(ms, attr as IBlobWriter);
    }
  }

  public void Add(long linkID, TempFormula tf)
  {
    this.Add(new ConditionInfo()
    {
      LinkID = Math.Abs(linkID),
      ExpertFormula = tf
    });
  }

  public ConditionInfo Find(long LinkID)
  {
    int index = this.IndexOf(LinkID);
    return index != -1 ? this[index] : (ConditionInfo) null;
  }

  public int IndexOf(long LinkID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].LinkID == Math.Abs(LinkID))
        return index;
    }
    return -1;
  }

  public bool ReplaceLink(long oldLinkID, long newLinkID)
  {
    ConditionInfo conditionInfo = this.Find(oldLinkID);
    if (conditionInfo == null)
      return false;
    conditionInfo.LinkID = Math.Abs(newLinkID);
    return true;
  }

  /// <summary>
  /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
  /// </summary>
  public bool WriteGuids
  {
    get => this._writeGuids;
    set => this._writeGuids = value;
  }

  public IEnumerator<ConditionInfo> GetEnumerator()
  {
    return (IEnumerator<ConditionInfo>) this._list.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

  public int Count => this._list.Count;

  public void Add(ConditionInfo item) => this._list.Add(item);

  public bool Remove(ConditionInfo item) => this._list.Remove(item);

  public void RemoveAt(int index) => this._list.RemoveAt(index);

  public void Clear() => this._list.Clear();
}
