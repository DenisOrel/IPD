// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.ListItem
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
    [DesignerCategory("code")]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
    [XmlRoot("ListItemStructure", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
    [DebuggerStepThrough]
    [Serializable]
    public class ListItem
    {
      private object[] itemsField;
      private string markerField;

      [XmlElement("ListStructure", typeof (List))]
      [XmlElement("ParagraphStructure", typeof (Paragraph))]
      [XmlElement("TableStructure", typeof (Table))]
      [XmlElement("FigureStructure", typeof (Figure))]
      public object[] Items
      {
        get => this.itemsField;
        set => this.itemsField = value;
      }

      [XmlAttribute(DataType = "ID")]
      public string Marker
      {
        get => this.markerField;
        set => this.markerField = value;
      }
    }
}
