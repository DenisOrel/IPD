// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.FixedPage
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

[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
[XmlRoot("FixedPage", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[Serializable]
public class FixedPage
{
  private string bleedBoxField;
  private string contentBoxField;
  private Resources fixedPageResourcesField;
  private double heightField;
  private object[] itemsField;
  private string langField;
  private string nameField;
  private double widthField;

  [XmlAttribute]
  public string BleedBox
  {
    get => this.bleedBoxField;
    set => this.bleedBoxField = value;
  }

  [XmlAttribute]
  public string ContentBox
  {
    get => this.contentBoxField;
    set => this.contentBoxField = value;
  }

  [XmlElement("FixedPage.Resources")]
  public Resources FixedPageResources
  {
    get => this.fixedPageResourcesField;
    set => this.fixedPageResourcesField = value;
  }

  [XmlAttribute]
  public double Height
  {
    get => this.heightField;
    set => this.heightField = value;
  }

  [XmlElement("Path", typeof (Path))]
  [XmlElement("Canvas", typeof (Canvas))]
  [XmlElement("Glyphs", typeof (Glyphs))]
  public object[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
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
  public double Width
  {
    get => this.widthField;
    set => this.widthField = value;
  }
}
