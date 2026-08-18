// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.PathFigure
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace Syncfusion.XPS;

[DesignerCategory("code")]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[XmlRoot("PathFigure", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[Serializable]
public class PathFigure
{
  private bool isClosedField;
  private bool isFilledField = true;
  private object[] itemsField;
  private string startPointField;

  [XmlAttribute]
  [DefaultValue(false)]
  public bool IsClosed
  {
    get => this.isClosedField;
    set => this.isClosedField = value;
  }

  [DefaultValue(true)]
  [XmlAttribute]
  public bool IsFilled
  {
    get => this.isFilledField;
    set => this.isFilledField = value;
  }

  [XmlElement("ArcSegment", typeof (ArcSegment))]
  [XmlElement("PolyLineSegment", typeof (PolyLineSegment))]
  [XmlElement("PolyQuadraticBezierSegment", typeof (PolyQuadraticBezierSegment))]
  [XmlElement("PolyBezierSegment", typeof (PolyBezierSegment))]
  public object[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  [XmlAttribute]
  public string StartPoint
  {
    get => this.startPointField;
    set => this.startPointField = value;
  }
}
