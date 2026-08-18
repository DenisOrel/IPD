// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpThumbnailStruct
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class XmpThumbnailStruct : XmpStructure
{
  private const string c_format = "format";
  private const string c_height = "height";
  private const string c_image = "image";
  private const string c_name = "http://ns.adobe.com/xap/1.0/g/img/";
  private const string c_prefix = "xapG";
  private const string c_width = "width";

  internal XmpThumbnailStruct(
    XmpMetadata xmp,
    XmlNode parent,
    string prefix,
    string localName,
    string namespaceURI,
    bool insideArray)
    : base(xmp, parent, prefix, localName, namespaceURI, insideArray)
  {
  }

  protected override void InitializeEntities()
  {
  }

  public string Format
  {
    get => this.GetSimpleProperty("format").Value;
    set
    {
      if (this.Format == null)
        throw new ArgumentNullException("format");
      this.GetSimpleProperty("format").Value = value;
    }
  }

  public float Height
  {
    get => this.GetSimpleProperty("height").GetReal();
    set => this.GetSimpleProperty("height").SetReal(value);
  }

  public byte[] Image
  {
    get => Convert.FromBase64String(this.GetSimpleProperty("image").Value);
    set
    {
      if (this.Image == null)
        throw new ArgumentNullException(nameof (Image));
      this.GetSimpleProperty("image").Value = Convert.ToBase64String(value);
    }
  }

  protected override string StructurePrefix => "xapG";

  protected override string StructureURI => "http://ns.adobe.com/xap/1.0/g/img/";

  public float Width
  {
    get => this.GetSimpleProperty("width").GetReal();
    set => this.GetSimpleProperty("width").SetReal(value);
  }
}
