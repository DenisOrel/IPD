// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.CustomSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class CustomSchema : XmpSchema
{
  private string m_namespace;
  private string m_namespaceUri;

  public CustomSchema(XmpMetadata xmp, string xmlNamespace, string namespaceUri)
    : base(xmp)
  {
    if (xmlNamespace == null)
      throw new ArgumentNullException(nameof (xmlNamespace));
    if (namespaceUri == null)
      throw new ArgumentNullException(nameof (namespaceUri));
    this.m_namespace = xmlNamespace;
    this.m_namespaceUri = namespaceUri;
    this.Initialize();
  }

  protected override XmlElement GetEntityXml()
  {
    XmlElement entityXml = (XmlElement) null;
    if (this.m_namespace != null)
      entityXml = base.GetEntityXml();
    return entityXml;
  }

  public string this[string name]
  {
    get => this.GetSimpleProperty(name).Value;
    set => this.GetSimpleProperty(name).Value = value;
  }

  protected override string Name => this.m_namespaceUri;

  protected override string Prefix => this.m_namespace;

  public override XmpSchemaType SchemaType => XmpSchemaType.Custom;
}
