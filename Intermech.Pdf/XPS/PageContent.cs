// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.PageContent
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
    [XmlRoot("PageContent", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class PageContent
    {
      private double heightField;
      private bool heightFieldSpecified;
      private LinkTarget[] pageContentLinkTargetsField;
      private string sourceField;
      private double widthField;
      private bool widthFieldSpecified;

      [XmlAttribute]
      public double Height
      {
        get => this.heightField;
        set => this.heightField = value;
      }

      [XmlIgnore]
      public bool HeightSpecified
      {
        get => this.heightFieldSpecified;
        set => this.heightFieldSpecified = value;
      }

      [XmlArray("PageContent.LinkTargets")]
      [XmlArrayItem("LinkTarget", IsNullable = false)]
      public LinkTarget[] PageContentLinkTargets
      {
        get => this.pageContentLinkTargetsField;
        set => this.pageContentLinkTargetsField = value;
      }

      [XmlAttribute(DataType = "anyURI")]
      public string Source
      {
        get => this.sourceField;
        set => this.sourceField = value;
      }

      [XmlAttribute]
      public double Width
      {
        get => this.widthField;
        set => this.widthField = value;
      }

      [XmlIgnore]
      public bool WidthSpecified
      {
        get => this.widthFieldSpecified;
        set => this.widthFieldSpecified = value;
      }
    }
}
