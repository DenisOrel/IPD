// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.StoryFragment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;


namespace Syncfusion.XPS
{
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
    [XmlRoot("StoryFragment", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [Serializable]
    public class StoryFragment
    {
      private string fragmentNameField;
      private FragmentType fragmentTypeField;
      private object[] itemsField;
      private Break storyBreak1Field;
      private Break storyBreakField;
      private string storyNameField;

      [XmlAttribute]
      public string FragmentName
      {
        get => this.fragmentNameField;
        set => this.fragmentNameField = value;
      }

      [XmlAttribute]
      public FragmentType FragmentType
      {
        get => this.fragmentTypeField;
        set => this.fragmentTypeField = value;
      }

      [XmlElement("ListStructure", typeof (List), Order = 1)]
      [XmlElement("FigureStructure", typeof (Figure), Order = 1)]
      [XmlElement("ParagraphStructure", typeof (Paragraph), Order = 1)]
      [XmlElement("SectionStructure", typeof (Section), Order = 1)]
      [XmlElement("TableStructure", typeof (Table), Order = 1)]
      public object[] Items
      {
        get => this.itemsField;
        set => this.itemsField = value;
      }

      [XmlElement(Order = 0)]
      public Break StoryBreak
      {
        get => this.storyBreakField;
        set => this.storyBreakField = value;
      }

      [XmlElement("StoryBreak", Order = 2)]
      public Break StoryBreak1
      {
        get => this.storyBreak1Field;
        set => this.storyBreak1Field = value;
      }

      [XmlAttribute]
      public string StoryName
      {
        get => this.storyNameField;
        set => this.storyNameField = value;
      }
    }
}
