// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Glyphs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace Syncfusion.XPS;

[XmlRoot("Glyphs", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[Serializable]
public class Glyphs
{
  private string bidiLevelField = "0";
  private string caretStopsField;
  private string clipField;
  private string deviceFontNameField;
  private string fillField;
  private string fixedPageNavigateUriField;
  private double fontRenderingEmSizeField;
  private string fontUriField;
  private Geometry glyphsClipField;
  private Brush glyphsFillField;
  private Brush glyphsOpacityMaskField;
  private Transform glyphsRenderTransformField;
  private string indicesField;
  private bool isSidewaysField;
  private string keyField;
  private string langField;
  private string nameField;
  private double opacityField = 1.0;
  private string opacityMaskField;
  private double originXField;
  private double originYField;
  private string renderTransformField;
  private StyleSimulations styleSimulationsField;
  private string unicodeStringField;

  [XmlAttribute(DataType = "integer")]
  [DefaultValue("0")]
  public string BidiLevel
  {
    get => this.bidiLevelField;
    set => this.bidiLevelField = value;
  }

  [XmlAttribute]
  public string CaretStops
  {
    get => this.caretStopsField;
    set => this.caretStopsField = value;
  }

  [XmlAttribute]
  public string Clip
  {
    get => this.clipField;
    set => this.clipField = value;
  }

  [XmlAttribute]
  public string DeviceFontName
  {
    get => this.deviceFontNameField;
    set => this.deviceFontNameField = value;
  }

  [XmlAttribute]
  public string Fill
  {
    get => this.fillField;
    set => this.fillField = value;
  }

  [XmlAttribute("FixedPage.NavigateUri", DataType = "anyURI")]
  public string FixedPageNavigateUri
  {
    get => this.fixedPageNavigateUriField;
    set => this.fixedPageNavigateUriField = value;
  }

  [XmlAttribute]
  public double FontRenderingEmSize
  {
    get => this.fontRenderingEmSizeField;
    set => this.fontRenderingEmSizeField = value;
  }

  [XmlAttribute(DataType = "anyURI")]
  public string FontUri
  {
    get => this.fontUriField;
    set => this.fontUriField = value;
  }

  [XmlElement("Glyphs.Clip")]
  public Geometry GlyphsClip
  {
    get => this.glyphsClipField;
    set => this.glyphsClipField = value;
  }

  [XmlElement("Glyphs.Fill")]
  public Brush GlyphsFill
  {
    get => this.glyphsFillField;
    set => this.glyphsFillField = value;
  }

  [XmlElement("Glyphs.OpacityMask")]
  public Brush GlyphsOpacityMask
  {
    get => this.glyphsOpacityMaskField;
    set => this.glyphsOpacityMaskField = value;
  }

  [XmlElement("Glyphs.RenderTransform")]
  public Transform GlyphsRenderTransform
  {
    get => this.glyphsRenderTransformField;
    set => this.glyphsRenderTransformField = value;
  }

  [XmlAttribute]
  public string Indices
  {
    get => this.indicesField;
    set => this.indicesField = value;
  }

  [XmlAttribute]
  [DefaultValue(false)]
  public bool IsSideways
  {
    get => this.isSidewaysField;
    set => this.isSidewaysField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
  public string Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string lang
  {
    get => this.langField;
    set => this.langField = value;
  }

  [XmlAttribute(DataType = "ID")]
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  [DefaultValue(1)]
  [XmlAttribute]
  public double Opacity
  {
    get => this.opacityField;
    set => this.opacityField = value;
  }

  [XmlAttribute]
  public string OpacityMask
  {
    get => this.opacityMaskField;
    set => this.opacityMaskField = value;
  }

  [XmlAttribute]
  public double OriginX
  {
    get => this.originXField;
    set => this.originXField = value;
  }

  [XmlAttribute]
  public double OriginY
  {
    get => this.originYField;
    set => this.originYField = value;
  }

  [XmlAttribute]
  public string RenderTransform
  {
    get => this.renderTransformField;
    set => this.renderTransformField = value;
  }

  [XmlAttribute]
  [DefaultValue(0)]
  public StyleSimulations StyleSimulations
  {
    get => this.styleSimulationsField;
    set => this.styleSimulationsField = value;
  }

  [XmlAttribute]
  public string UnicodeString
  {
    get => this.unicodeStringField;
    set => this.unicodeStringField = value;
  }
}
