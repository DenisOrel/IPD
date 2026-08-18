// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public abstract class XmpSchema : XmpEntityBase
{
  internal const string c_schemaTagName = "Description";
  private const string c_xPathDescription = "/x:xmpmeta/rdf:RDF/rdf:Description";
  private Hashtable m_properties;
  private XmpMetadata m_xmp;

  protected internal XmpSchema(XmpMetadata xmp)
    : base((XmlNode) xmp.Rdf, "rdf", "Description", "http://www.w3.org/1999/02/22-rdf-syntax-ns#")
  {
    this.m_xmp = xmp != null ? xmp : throw new ArgumentNullException(nameof (xmp));
    this.m_properties = new Hashtable();
    if (this.Prefix == null)
      return;
    this.Initialize();
  }

  protected XmpArray CreateArray(string name, XmpArrayType arrayType)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (arrayType == XmpArrayType.Unknown)
      throw new ArgumentException("Wrong array type", nameof (arrayType));
    return new XmpArray(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, arrayType);
  }

  protected override void CreateEntity()
  {
    XmlElement element = this.Xmp.CreateElement(this.EntityPrefix, this.EntityName, this.EntityNamespaceURI);
    this.EntityParent.AppendChild((XmlNode) element);
    XmlAttribute attribute1 = this.Xmp.CreateAttribute(this.EntityPrefix, "about", this.EntityNamespaceURI, string.Empty);
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = this.Xmp.CreateAttribute("xmlns:" + this.Prefix, this.Name);
    element.Attributes.Append(attribute2);
    this.Xmp.AddNamespace(this.Prefix, this.Name);
  }

  protected XmpLangArray CreateLangArray(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    return new XmpLangArray(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name);
  }

  protected XmpSimpleType CreateSimpleProperty(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    return new XmpSimpleType(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name);
  }

  public XmpStructure CreateStructure(XmpStructureType type)
  {
    return this.CreateStructure(string.Empty, type);
  }

  protected XmpStructure CreateStructure(string name, XmpStructureType type)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    bool insideArray = name.Length == 0;
    switch (type)
    {
      case XmpStructureType.Dimensions:
        return (XmpStructure) new XmpDimensionsStruct(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, insideArray);
      case XmpStructureType.Font:
        return (XmpStructure) new XmpFontStruct(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, insideArray);
      case XmpStructureType.Colorant:
        return (XmpStructure) new XmpColorantStruct(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, insideArray);
      case XmpStructureType.Thumbnail:
        return (XmpStructure) new XmpThumbnailStruct(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, insideArray);
      case XmpStructureType.Job:
        return (XmpStructure) new XmpJobStruct(this.Xmp, (XmlNode) this.XmlData, this.Prefix, name, this.Name, insideArray);
      default:
        return (XmpStructure) null;
    }
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

  protected override XmlElement GetEntityXml()
  {
    XmlNodeList xmlNodeList = this.EntityParent.SelectNodes($"./{this.EntityPrefix}:{this.EntityName}", this.Xmp.NamespaceManager);
    XmlNode entityXml = (XmlNode) null;
    int i = 0;
    for (int count = xmlNodeList.Count; i < count; ++i)
    {
      XmlNode xmlNode = xmlNodeList[i];
      XmlAttribute attribute = xmlNode.Attributes[this.Prefix, "http://www.w3.org/2000/xmlns/"];
      if (attribute != null && attribute.Value.Equals(this.Name))
      {
        entityXml = xmlNode;
        break;
      }
    }
    return entityXml as XmlElement;
  }

  protected XmpLangArray GetLangArray(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (!(this.m_properties[(object) name] is XmpLangArray langArray))
    {
      langArray = this.CreateLangArray(name);
      this.m_properties[(object) name] = (object) langArray;
    }
    return langArray;
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

  protected XmpStructure GetStructure(string name, XmpStructureType type)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (!(this.m_properties[(object) name] is XmpStructure structure))
    {
      structure = this.CreateStructure(name, type);
      this.m_properties[(object) name] = (object) structure;
    }
    return structure;
  }

  protected abstract string Name { get; }

  protected abstract string Prefix { get; }

  public abstract XmpSchemaType SchemaType { get; }

  protected internal XmpMetadata Xmp => this.m_xmp;
}
