// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpStructure
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public abstract class XmpStructure : XmpType
{
  private bool m_bInitialized;
  private bool m_bInsideArray;
  private bool m_bSuspend;
  private Hashtable m_properties;

  internal XmpStructure(
    XmpMetadata xmp,
    XmlNode parent,
    string prefix,
    string localName,
    string namespaceURI)
    : this(xmp, parent, prefix, localName, namespaceURI, false)
  {
  }

  internal XmpStructure(
    XmpMetadata xmp,
    XmlNode parent,
    string prefix,
    string localName,
    string namespaceURI,
    bool insideArray)
    : base(xmp, parent, prefix, localName, namespaceURI)
  {
    this.m_bSuspend = true;
    this.m_bInsideArray = insideArray;
    this.m_bSuspend = false;
    this.m_properties = new Hashtable();
    this.Initialize();
  }

  protected override bool CheckIfExists()
  {
    bool flag = false;
    if (this.m_bInitialized)
      flag = base.CheckIfExists();
    return flag;
  }

  protected XmpArray CreateArray(string name, XmpArrayType arrayType)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (arrayType == XmpArrayType.Unknown)
      throw new ArgumentException("Wrong array type", nameof (arrayType));
    return new XmpArray(this.Xmp, (XmlNode) this.InnerXmlData, this.StructurePrefix, name, this.StructureURI, arrayType);
  }

  protected override void CreateEntity()
  {
    if (this.m_properties == null)
      return;
    this.Xmp.AddNamespace(this.StructurePrefix, this.StructureURI);
    if (!this.m_bInsideArray)
      base.CreateEntity();
    this.CreateStructureContent();
    this.InitializeEntities();
    this.m_bInitialized = true;
  }

  protected XmpSimpleType CreateSimpleProperty(string name)
  {
    return name != null ? this.CreateSimpleProperty(name, (XmlNode) this.InnerXmlData) : throw new ArgumentNullException(nameof (name));
  }

  protected XmpSimpleType CreateSimpleProperty(string name, XmlNode parent)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    return new XmpSimpleType(this.Xmp, parent, this.StructurePrefix, name, this.StructureURI);
  }

  protected void CreateStructureContent()
  {
    XmlNode contentParent = this.GetContentParent();
    XmlElement element = this.Xmp.CreateElement("rdf", "Description", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
    XmlElement newChild = element;
    contentParent.AppendChild((XmlNode) newChild);
    XmlAttribute attribute = this.Xmp.CreateAttribute("xmlns:" + this.StructurePrefix, this.StructureURI);
    element.Attributes.Append(attribute);
  }

  protected XmpArray GetArray(string name, XmpArrayType arrayType)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (!(this.m_properties[(object) name] is XmpArray array))
    {
      array = this.CreateArray(name, arrayType);
      this.m_properties[(object) name] = (object) array;
    }
    return array;
  }

  private XmlNode GetContentParent()
  {
    return !this.m_bInsideArray ? (XmlNode) this.XmlData : this.EntityParent;
  }

  private XmlElement GetDescriptionElement()
  {
    return this.GetContentParent().SelectSingleNode("./rdf:Description", this.Xmp.NamespaceManager) as XmlElement;
  }

  protected override XmlElement GetEntityXml()
  {
    return (this.m_bInsideArray ? this.GetDescriptionElement() : base.GetEntityXml()) ?? throw new ArgumentNullException("elm");
  }

  protected XmpSimpleType GetSimpleProperty(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (!(this.m_properties[(object) name] is XmpSimpleType simpleProperty))
    {
      simpleProperty = this.CreateSimpleProperty(name);
      this.m_properties[(object) name] = (object) simpleProperty;
    }
    return simpleProperty;
  }

  protected XmpSimpleType GetSimpleProperty(string name, XmlNode parent)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    if (!(this.m_properties[(object) name] is XmpSimpleType simpleProperty))
    {
      simpleProperty = this.CreateSimpleProperty(name, parent);
      this.m_properties[(object) name] = (object) simpleProperty;
    }
    return simpleProperty;
  }

  protected override bool GetSuspend() => this.m_bSuspend;

  protected abstract void InitializeEntities();

  protected internal XmlElement InnerXmlData
  {
    get
    {
      return (!this.m_bInsideArray ? this.GetDescriptionElement() : this.XmlData) ?? throw new ArgumentNullException("elm");
    }
  }

  protected abstract string StructurePrefix { get; }

  protected abstract string StructureURI { get; }
}
