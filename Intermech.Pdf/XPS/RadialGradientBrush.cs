// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.RadialGradientBrush
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


namespace Syncfusion.XPS
{
    [DebuggerStepThrough]
    [XmlRoot("RadialGradientBrush", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [Serializable]
    public class RadialGradientBrush
    {
      private string centerField;
      private ClrIntMode colorInterpolationModeField = ClrIntMode.SRgbLinearInterpolation;
      private string gradientOriginField;
      private string keyField;
      private MappingMode mappingModeField;
      private double opacityField = 1.0;
      private GradientStop[] radialGradientBrushGradientStopsField;
      private Syncfusion.XPS.Transform radialGradientBrushTransformField;
      private double radiusXField;
      private double radiusYField;
      private SpreadMethod spreadMethodField;
      private string transformField;

      [XmlAttribute]
      public string Center
      {
        get => this.centerField;
        set => this.centerField = value;
      }

      [DefaultValue(1)]
      [XmlAttribute]
      public ClrIntMode ColorInterpolationMode
      {
        get => this.colorInterpolationModeField;
        set => this.colorInterpolationModeField = value;
      }

      [XmlAttribute]
      public string GradientOrigin
      {
        get => this.gradientOriginField;
        set => this.gradientOriginField = value;
      }

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
      public string Key
      {
        get => this.keyField;
        set => this.keyField = value;
      }

      [XmlAttribute]
      public MappingMode MappingMode
      {
        get => this.mappingModeField;
        set => this.mappingModeField = value;
      }

      [DefaultValue(1)]
      [XmlAttribute]
      public double Opacity
      {
        get => this.opacityField;
        set => this.opacityField = value;
      }

      [XmlArrayItem("GradientStop", IsNullable = false)]
      [XmlArray("RadialGradientBrush.GradientStops")]
      public GradientStop[] RadialGradientBrushGradientStops
      {
        get => this.radialGradientBrushGradientStopsField;
        set => this.radialGradientBrushGradientStopsField = value;
      }

      [XmlElement("RadialGradientBrush.Transform")]
      public Syncfusion.XPS.Transform RadialGradientBrushTransform
      {
        get => this.radialGradientBrushTransformField;
        set => this.radialGradientBrushTransformField = value;
      }

      [XmlAttribute]
      public double RadiusX
      {
        get => this.radiusXField;
        set => this.radiusXField = value;
      }

      [XmlAttribute]
      public double RadiusY
      {
        get => this.radiusYField;
        set => this.radiusYField = value;
      }

      [XmlAttribute]
      [DefaultValue(0)]
      public SpreadMethod SpreadMethod
      {
        get => this.spreadMethodField;
        set => this.spreadMethodField = value;
      }

      [XmlAttribute]
      public string Transform
      {
        get => this.transformField;
        set => this.transformField = value;
      }
    }
}
