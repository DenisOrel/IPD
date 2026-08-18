// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Path
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

[XmlRoot("Path", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[Serializable]
public class Path
{
  private string automationPropertiesHelpTextField;
  private string automationPropertiesNameField;
  private string clipField;
  private string dataField;
  private string fillField;
  private string fixedPageNavigateUriField;
  private string keyField;
  private string langField;
  private string nameField;
  private double opacityField = 1.0;
  private string opacityMaskField;
  private Geometry pathClipField;
  private Geometry pathDataField;
  private Brush pathFillField;
  private Brush pathOpacityMaskField;
  private Transform pathRenderTransformField;
  private Brush pathStrokeField;
  private string renderTransformField;
  private bool snapsToDevicePixelsField;
  private bool snapsToDevicePixelsFieldSpecified;
  private string strokeDashArrayField;
  private DashCap strokeDashCapField;
  private double strokeDashOffsetField;
  private LineCap strokeEndLineCapField;
  private string strokeField;
  private LineJoin strokeLineJoinField;
  private double strokeMiterLimitField = 10.0;
  private LineCap strokeStartLineCapField;
  private double strokeThicknessField = 1.0;

  [XmlAttribute("AutomationProperties.HelpText")]
  public string AutomationPropertiesHelpText
  {
    get => this.automationPropertiesHelpTextField;
    set => this.automationPropertiesHelpTextField = value;
  }

  [XmlAttribute("AutomationProperties.Name")]
  public string AutomationPropertiesName
  {
    get => this.automationPropertiesNameField;
    set => this.automationPropertiesNameField = value;
  }

  [XmlAttribute]
  public string Clip
  {
    get => this.clipField;
    set => this.clipField = value;
  }

  [XmlAttribute]
  public string Data
  {
    get => this.dataField;
    set => this.dataField = value;
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

  [XmlAttribute]
  [DefaultValue(1)]
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

  [XmlElement("Path.Clip")]
  public Geometry PathClip
  {
    get => this.pathClipField;
    set => this.pathClipField = value;
  }

  [XmlElement("Path.Data")]
  public Geometry PathData
  {
    get => this.pathDataField;
    set => this.pathDataField = value;
  }

  [XmlElement("Path.Fill")]
  public Brush PathFill
  {
    get => this.pathFillField;
    set => this.pathFillField = value;
  }

  [XmlElement("Path.OpacityMask")]
  public Brush PathOpacityMask
  {
    get => this.pathOpacityMaskField;
    set => this.pathOpacityMaskField = value;
  }

  [XmlElement("Path.RenderTransform")]
  public Transform PathRenderTransform
  {
    get => this.pathRenderTransformField;
    set => this.pathRenderTransformField = value;
  }

  [XmlElement("Path.Stroke")]
  public Brush PathStroke
  {
    get => this.pathStrokeField;
    set => this.pathStrokeField = value;
  }

  [XmlAttribute]
  public string RenderTransform
  {
    get => this.renderTransformField;
    set => this.renderTransformField = value;
  }

  [XmlAttribute]
  public bool SnapsToDevicePixels
  {
    get => this.snapsToDevicePixelsField;
    set => this.snapsToDevicePixelsField = value;
  }

  [XmlIgnore]
  public bool SnapsToDevicePixelsSpecified
  {
    get => this.snapsToDevicePixelsFieldSpecified;
    set => this.snapsToDevicePixelsFieldSpecified = value;
  }

  [XmlAttribute]
  public string Stroke
  {
    get => this.strokeField;
    set => this.strokeField = value;
  }

  [XmlAttribute]
  public string StrokeDashArray
  {
    get => this.strokeDashArrayField;
    set => this.strokeDashArrayField = value;
  }

  [DefaultValue(0)]
  [XmlAttribute]
  public DashCap StrokeDashCap
  {
    get => this.strokeDashCapField;
    set => this.strokeDashCapField = value;
  }

  [XmlAttribute]
  [DefaultValue(0)]
  public double StrokeDashOffset
  {
    get => this.strokeDashOffsetField;
    set => this.strokeDashOffsetField = value;
  }

  [DefaultValue(0)]
  [XmlAttribute]
  public LineCap StrokeEndLineCap
  {
    get => this.strokeEndLineCapField;
    set => this.strokeEndLineCapField = value;
  }

  [DefaultValue(0)]
  [XmlAttribute]
  public LineJoin StrokeLineJoin
  {
    get => this.strokeLineJoinField;
    set => this.strokeLineJoinField = value;
  }

  [XmlAttribute]
  [DefaultValue(10)]
  public double StrokeMiterLimit
  {
    get => this.strokeMiterLimitField;
    set => this.strokeMiterLimitField = value;
  }

  [XmlAttribute]
  [DefaultValue(0)]
  public LineCap StrokeStartLineCap
  {
    get => this.strokeStartLineCapField;
    set => this.strokeStartLineCapField = value;
  }

  [DefaultValue(1)]
  [XmlAttribute]
  public double StrokeThickness
  {
    get => this.strokeThicknessField;
    set => this.strokeThicknessField = value;
  }
}
