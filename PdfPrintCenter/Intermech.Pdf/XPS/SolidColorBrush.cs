// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.SolidColorBrush
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

[XmlRoot("SolidColorBrush", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[Serializable]
public class SolidColorBrush
{
  private string colorField;
  private string keyField;
  private double opacityField = 1.0;

  [XmlAttribute]
  public string Color
  {
    get => this.colorField;
    set => this.colorField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
  public string Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }

  [XmlAttribute]
  [DefaultValue(1)]
  public double Opacity
  {
    get => this.opacityField;
    set => this.opacityField = value;
  }
}
