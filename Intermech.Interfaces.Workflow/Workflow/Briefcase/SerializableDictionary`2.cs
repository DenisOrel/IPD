// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Briefcase.SerializableDictionary`2
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Briefcase;

[XmlRoot("Dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
  public XmlSchema GetSchema() => (XmlSchema) null;

  public void ReadXml(XmlReader reader)
  {
    XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
    XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
    int num = reader.IsEmptyElement ? 1 : 0;
    reader.Read();
    if (num != 0)
      return;
    while (reader.NodeType != XmlNodeType.EndElement)
    {
      reader.ReadStartElement("Item");
      reader.ReadStartElement("Key");
      TKey key = (TKey) xmlSerializer1.Deserialize(reader);
      reader.ReadEndElement();
      reader.ReadStartElement("Value");
      TValue obj = (TValue) xmlSerializer2.Deserialize(reader);
      reader.ReadEndElement();
      this.Add(key, obj);
      reader.ReadEndElement();
      int content = (int) reader.MoveToContent();
    }
    reader.ReadEndElement();
  }

  public void WriteXml(XmlWriter writer)
  {
    XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
    XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
    foreach (TKey key in this.Keys)
    {
      writer.WriteStartElement("Item");
      writer.WriteStartElement("Key");
      xmlSerializer1.Serialize(writer, (object) key, SimpleBriefcase.EmptyNamespace);
      writer.WriteEndElement();
      writer.WriteStartElement("Value");
      TValue o = this[key];
      xmlSerializer2.Serialize(writer, (object) o, SimpleBriefcase.EmptyNamespace);
      writer.WriteEndElement();
      writer.WriteEndElement();
    }
  }
}
