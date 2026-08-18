// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Story
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
[XmlRoot("Story", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
[Serializable]
public class Story
{
  private Syncfusion.XPS.StoryFragmentReference[] storyFragmentReferenceField;
  private string storyNameField;

  [XmlElement("StoryFragmentReference")]
  public Syncfusion.XPS.StoryFragmentReference[] StoryFragmentReference
  {
    get => this.storyFragmentReferenceField;
    set => this.storyFragmentReferenceField = value;
  }

  [XmlAttribute]
  public string StoryName
  {
    get => this.storyNameField;
    set => this.storyNameField = value;
  }
}
