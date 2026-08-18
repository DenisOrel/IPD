// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpLangArray
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class XmpLangArray : XmpCollection
{
  private const string c_langAttribute = "lang";
  private const string c_langName = "x-default";

  internal XmpLangArray(
    XmpMetadata xmp,
    XmlNode parent,
    string prefix,
    string localName,
    string namespaceURI)
    : base(xmp, parent, prefix, localName, namespaceURI)
  {
  }

  public void Add(string lang, string value)
  {
    if (lang == null)
      throw new ArgumentNullException(nameof (lang));
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    XmpUtils.SetTextValue(this.CreateItem(lang), value);
  }

  protected override void CreateEntity()
  {
    base.CreateEntity();
    this.CreateItem("x-default");
  }

  private XmlElement CreateItem(string lang)
  {
    if (lang == null)
      throw new ArgumentNullException(nameof (lang));
    XmlElement element = this.Xmp.CreateElement("rdf", "li", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
    this.ItemsContainer.AppendChild((XmlNode) element);
    XmlAttribute attribute = this.Xmp.CreateAttribute("xml", nameof (lang), "http://www.w3.org/XML/1998/namespace", lang);
    element.Attributes.Append(attribute);
    return element;
  }

  private XmlElement GetItem(string lang)
  {
    if (lang == null)
      throw new ArgumentNullException(nameof (lang));
    return this.ItemsContainer.SelectSingleNode($"./rdf:li[@xml:lang=\"{lang}\"]", this.Xmp.NamespaceManager) as XmlElement;
  }

  protected override XmpArrayType ArrayType => XmpArrayType.Alt;

  public string DefaultText
  {
    get
    {
      return !this.XmlData.InnerXml.Contains("rdf") ? this.XmlData.InnerText : (this.GetItem("x-default") ?? this.CreateItem("x-default")).InnerXml;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (DefaultText));
      XmpUtils.SetTextValue(this.GetItem("x-default") ?? this.CreateItem("x-default"), value);
    }
  }

  public string this[string lang]
  {
    get
    {
      string str = (string) null;
      XmlElement xmlElement = this.GetItem(lang);
      if (xmlElement != null)
        str = xmlElement.InnerXml;
      return str;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      XmpUtils.SetTextValue(this.GetItem(lang) ?? this.CreateItem(lang), value);
    }
  }
}
