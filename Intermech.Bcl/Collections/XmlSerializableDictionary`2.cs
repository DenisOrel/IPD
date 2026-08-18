
// Type: Intermech.Collections.XmlSerializableDictionary`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Intermech.Collections
{
    [XmlRoot("Dictionary")]
    [Serializable]
    public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
      public XmlSerializableDictionary()
      {
      }

      public XmlSerializableDictionary(IDictionary<TKey, TValue> dict)
        : base(dict)
      {
      }

      protected XmlSerializableDictionary(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

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
          reader.ReadStartElement("item");
          reader.ReadStartElement("key");
          TKey key = (TKey) xmlSerializer1.Deserialize(reader);
          reader.ReadEndElement();
          reader.ReadStartElement("value");
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
          writer.WriteStartElement("item");
          writer.WriteStartElement("key");
          xmlSerializer1.Serialize(writer, (object) key);
          writer.WriteEndElement();
          writer.WriteStartElement("value");
          TValue o = this[key];
          xmlSerializer2.Serialize(writer, (object) o);
          writer.WriteEndElement();
          writer.WriteEndElement();
        }
      }
    }
}
