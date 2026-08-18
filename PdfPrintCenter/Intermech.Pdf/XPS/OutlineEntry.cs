// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.OutlineEntry
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

[XmlRoot("OutlineEntry", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
[Serializable]
public class OutlineEntry
{
  private string descriptionField;
  private string langField;
  private int outlineLevelField = 1;
  private string outlineTargetField;

  [XmlAttribute]
  public string Description
  {
    get => this.descriptionField;
    set => this.descriptionField = value;
  }

  [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string lang
  {
    get => this.langField;
    set => this.langField = value;
  }

  [DefaultValue(1)]
  [XmlAttribute]
  public int OutlineLevel
  {
    get => this.outlineLevelField;
    set => this.outlineLevelField = value;
  }

  [XmlAttribute(DataType = "anyURI")]
  public string OutlineTarget
  {
    get => this.outlineTargetField;
    set => this.outlineTargetField = value;
  }
}
