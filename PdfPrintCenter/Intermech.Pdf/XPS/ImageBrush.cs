// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.ImageBrush
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

[DebuggerStepThrough]
[GeneratedCode("xsd", "2.0.50727.3038")]
[XmlRoot("ImageBrush", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[Serializable]
public class ImageBrush
{
  private Syncfusion.XPS.Transform imageBrushTransformField;
  private string imageSourceField;
  private string keyField;
  private double opacityField = 1.0;
  private TileMode tileModeField;
  private string transformField;
  private string viewboxField;
  private ViewUnits viewboxUnitsField;
  private string viewportField;
  private ViewUnits viewportUnitsField;

  [XmlElement("ImageBrush.Transform")]
  public Syncfusion.XPS.Transform ImageBrushTransform
  {
    get => this.imageBrushTransformField;
    set => this.imageBrushTransformField = value;
  }

  [XmlAttribute]
  public string ImageSource
  {
    get => this.imageSourceField;
    set => this.imageSourceField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
  public string Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }

  [DefaultValue(1)]
  [XmlAttribute]
  public double Opacity
  {
    get => this.opacityField;
    set => this.opacityField = value;
  }

  [XmlAttribute]
  [DefaultValue(0)]
  public TileMode TileMode
  {
    get => this.tileModeField;
    set => this.tileModeField = value;
  }

  [XmlAttribute]
  public string Transform
  {
    get => this.transformField;
    set => this.transformField = value;
  }

  [XmlAttribute]
  public string Viewbox
  {
    get => this.viewboxField;
    set => this.viewboxField = value;
  }

  [XmlAttribute]
  public ViewUnits ViewboxUnits
  {
    get => this.viewboxUnitsField;
    set => this.viewboxUnitsField = value;
  }

  [XmlAttribute]
  public string Viewport
  {
    get => this.viewportField;
    set => this.viewportField = value;
  }

  [XmlAttribute]
  public ViewUnits ViewportUnits
  {
    get => this.viewportUnitsField;
    set => this.viewportUnitsField = value;
  }
}
