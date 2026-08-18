// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpArray
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public class XmpArray : XmpCollection
    {
      internal const string c_dateFormat = "yyyy-MM-dd'T'HH:mm:ss.ffzzz";
      private XmpArrayType m_arrayType;

      internal XmpArray(
        XmpMetadata xmp,
        XmlNode parent,
        string prefix,
        string localName,
        string namespaceURI,
        XmpArrayType type)
        : base(xmp, parent, prefix, localName, namespaceURI)
      {
        this.m_arrayType = type;
        this.Initialize();
      }

      public void Add(XmpStructure structure)
      {
        if (structure == null)
          throw new ArgumentNullException(nameof (structure));
        XmlElement parent = this.CreateItem();
        XmpUtils.SetXmlValue(parent, structure.XmlData);
        this.ChangeParent((XmlNode) parent, (XmpEntityBase) structure);
      }

      public void Add(DateTime value) => this.Add(value.ToString("yyyy-MM-dd'T'HH:mm:ss.ffzzz"));

      public void Add(int value) => XmpUtils.SetIntValue(this.CreateItem(), value);

      public void Add(float value) => XmpUtils.SetRealValue(this.CreateItem(), value);

      public void Add(string value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        XmpUtils.SetTextValue(this.CreateItem(), value);
      }

      public void Add(DateTime value, string format)
      {
        if (format == null)
          throw new ArgumentNullException(nameof (format));
        this.Add(value.ToString(format));
      }

      private void ChangeParent(XmlNode parent, XmpEntityBase entity)
      {
        if (parent == null)
          throw new ArgumentNullException(nameof (parent));
        if (entity == null)
          throw new ArgumentNullException(nameof (entity));
        entity.SetXmlParent(parent);
      }

      private XmlElement CreateItem()
      {
        XmlElement element = this.Xmp.CreateElement("rdf", "li", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        this.ItemsContainer.AppendChild((XmlNode) element);
        return element;
      }

      private string[] GetArrayValues()
      {
        string[] arrayValues1 = new string[1];
        if (this.XmlData.InnerXml.Contains("rdf"))
        {
          XmlNodeList arrayItems = this.GetArrayItems();
          if (arrayItems.Count == 0)
          {
            arrayValues1[0] = string.Empty;
            return arrayValues1;
          }
          string[] arrayValues2 = new string[arrayItems.Count];
          int i = 0;
          for (int count = arrayItems.Count; i < count; ++i)
          {
            XmlNode xmlNode = arrayItems[i];
            arrayValues2[i] = xmlNode.InnerXml;
          }
          return arrayValues2;
        }
        arrayValues1[0] = this.XmlData.InnerText;
        return arrayValues1;
      }

      protected override XmpArrayType ArrayType => this.m_arrayType;

      public string[] Items => this.GetArrayValues();
    }
}
