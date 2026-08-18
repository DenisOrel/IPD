// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.DocumentStructure
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

[DebuggerStepThrough]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
[XmlRoot("DocumentStructure", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
[Serializable]
public class DocumentStructure
{
  private Outline documentStructureOutlineField;
  private Syncfusion.XPS.Story[] storyField;

  [XmlElement("DocumentStructure.Outline")]
  public Outline DocumentStructureOutline
  {
    get => this.documentStructureOutlineField;
    set => this.documentStructureOutlineField = value;
  }

  [XmlElement("Story")]
  public Syncfusion.XPS.Story[] Story
  {
    get => this.storyField;
    set => this.storyField = value;
  }
}
