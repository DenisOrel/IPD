// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpFontStruct
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class XmpFontStruct : XmpStructure
{
  private const string c_childFontFiles = "childFontFiles";
  private const string c_composite = "composite";
  private const string c_fontFace = "fontFace";
  private const string c_fontFamily = "fontFamily";
  private const string c_fontFileName = "fontFileName";
  private const string c_fontName = "fontName";
  private const string c_fontType = "fontType";
  private const string c_name = "http:ns.adobe.com/xap/1.0/sType/Font#";
  private const string c_prefix = "stFnt";
  private const string c_versionString = "versionString";

  internal XmpFontStruct(
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

  public XmpArray ChildFontFiles => this.GetArray("childFontFiles", XmpArrayType.Seq);

  public bool Composite
  {
    get => this.GetSimpleProperty("composite").GetBool();
    set => this.GetSimpleProperty("composite").SetBool(value);
  }

  public string FontFace
  {
    get => this.GetSimpleProperty("fontFace").Value;
    set
    {
      this.GetSimpleProperty("fontFace").Value = value != null ? value : throw new ArgumentNullException("fontFace");
    }
  }

  public string FontFamily
  {
    get => this.GetSimpleProperty("fontFamily").Value;
    set
    {
      this.GetSimpleProperty("fontFamily").Value = value != null ? value : throw new ArgumentNullException("fontFamily");
    }
  }

  public string FontFileName
  {
    get => this.GetSimpleProperty("fontFileName").Value;
    set
    {
      this.GetSimpleProperty("fontFileName").Value = value != null ? value : throw new ArgumentNullException("fontFileName");
    }
  }

  public string FontName
  {
    get => this.GetSimpleProperty("fontName").Value;
    set
    {
      this.GetSimpleProperty("fontName").Value = value != null ? value : throw new ArgumentNullException("fontName");
    }
  }

  public string FontType
  {
    get => this.GetSimpleProperty("fontType").Value;
    set
    {
      this.GetSimpleProperty("fontType").Value = value != null ? value : throw new ArgumentNullException("fontType");
    }
  }

  protected override string StructurePrefix => "stFnt";

  protected override string StructureURI => "http:ns.adobe.com/xap/1.0/sType/Font#";

  public string VersionString
  {
    get => this.GetSimpleProperty("versionString").Value;
    set
    {
      this.GetSimpleProperty("versionString").Value = value != null ? value : throw new ArgumentNullException("versionString");
    }
  }
}
