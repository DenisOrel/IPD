// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.PathGeometry
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

[GeneratedCode("xsd", "2.0.50727.3038")]
[XmlRoot("PathGeometry", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[Serializable]
public class PathGeometry
{
  private string figuresField;
  private FillRule fillRuleField;
  private string keyField;
  private Syncfusion.XPS.PathFigure[] pathFigureField;
  private Syncfusion.XPS.Transform pathGeometryTransformField;
  private string transformField;

  [XmlAttribute]
  public string Figures
  {
    get => this.figuresField;
    set => this.figuresField = value;
  }

  [DefaultValue(0)]
  [XmlAttribute]
  public FillRule FillRule
  {
    get => this.fillRuleField;
    set => this.fillRuleField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
  public string Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }

  [XmlElement("PathFigure")]
  public Syncfusion.XPS.PathFigure[] PathFigure
  {
    get => this.pathFigureField;
    set => this.pathFigureField = value;
  }

  [XmlElement("PathGeometry.Transform")]
  public Syncfusion.XPS.Transform PathGeometryTransform
  {
    get => this.pathGeometryTransformField;
    set => this.pathGeometryTransformField = value;
  }

  [XmlAttribute]
  public string Transform
  {
    get => this.transformField;
    set => this.transformField = value;
  }
}
