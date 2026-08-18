// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public abstract class XmpCollection : XmpType
{
  protected const string c_itemName = "li";

  internal XmpCollection(
    XmpMetadata xmp,
    XmlNode parent,
    string prefix,
    string localName,
    string namespaceURI)
    : base(xmp, parent, prefix, localName, namespaceURI)
  {
  }

  protected override void CreateEntity()
  {
    if (this.ArrayType == XmpArrayType.Unknown)
      return;
    base.CreateEntity();
    this.XmlData.AppendChild((XmlNode) this.Xmp.CreateElement("rdf", this.GetArrayName(), "http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
  }

  protected XmlNodeList GetArrayItems()
  {
    return this.ItemsContainer.SelectNodes("./rdf:li", this.Xmp.NamespaceManager);
  }

  private string GetArrayName()
  {
    string arrayName = XmpArrayType.Bag.ToString();
    if (this.XmlData.InnerXml.Contains("rdf:Seq"))
      return XmpArrayType.Seq.ToString();
    if (this.XmlData.InnerXml.Contains("rdf:Alt"))
      arrayName = XmpArrayType.Alt.ToString();
    return arrayName;
  }

  private int GetItemsCount() => this.GetArrayItems().Count;

  protected abstract XmpArrayType ArrayType { get; }

  public int Count => this.GetItemsCount();

  protected XmlElement ItemsContainer
  {
    get
    {
      return (this.XmlData.SelectSingleNode("./rdf:" + this.GetArrayName(), this.Xmp.NamespaceManager) ?? throw new ArgumentNullException("node")) as XmlElement;
    }
  }
}
