// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.LinearGradientBrush
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

[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlRoot("LinearGradientBrush", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[Serializable]
public class LinearGradientBrush
{
  private ClrIntMode colorInterpolationModeField = ClrIntMode.SRgbLinearInterpolation;
  private string endPointField;
  private string keyField;
  private GradientStop[] linearGradientBrushGradientStopsField;
  private Syncfusion.XPS.Transform linearGradientBrushTransformField;
  private MappingMode mappingModeField;
  private double opacityField = 1.0;
  private SpreadMethod spreadMethodField = SpreadMethod.None;
  private string startPointField;
  private string transformField;

  [XmlAttribute]
  [DefaultValue(1)]
  public ClrIntMode ColorInterpolationMode
  {
    get => this.colorInterpolationModeField;
    set => this.colorInterpolationModeField = value;
  }

  [XmlAttribute]
  public string EndPoint
  {
    get => this.endPointField;
    set => this.endPointField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
  public string Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }

  [XmlArrayItem("GradientStop", IsNullable = false)]
  [XmlArray("LinearGradientBrush.GradientStops")]
  public GradientStop[] LinearGradientBrushGradientStops
  {
    get => this.linearGradientBrushGradientStopsField;
    set => this.linearGradientBrushGradientStopsField = value;
  }

  [XmlElement("LinearGradientBrush.Transform")]
  public Syncfusion.XPS.Transform LinearGradientBrushTransform
  {
    get => this.linearGradientBrushTransformField;
    set => this.linearGradientBrushTransformField = value;
  }

  [XmlAttribute]
  public MappingMode MappingMode
  {
    get => this.mappingModeField;
    set => this.mappingModeField = value;
  }

  [XmlAttribute]
  [DefaultValue(1)]
  public double Opacity
  {
    get => this.opacityField;
    set => this.opacityField = value;
  }

  [DefaultValue(0)]
  [XmlAttribute]
  public SpreadMethod SpreadMethod
  {
    get => this.spreadMethodField;
    set => this.spreadMethodField = value;
  }

  [XmlAttribute]
  public string StartPoint
  {
    get => this.startPointField;
    set => this.startPointField = value;
  }

  [XmlAttribute]
  public string Transform
  {
    get => this.transformField;
    set => this.transformField = value;
  }
}
