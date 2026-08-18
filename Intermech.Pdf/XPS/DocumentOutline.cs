// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.DocumentOutline
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


namespace Syncfusion.XPS
{
    [DesignerCategory("code")]
    [XmlRoot("DocumentOutline", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
    [DebuggerStepThrough]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
    [Serializable]
    public class DocumentOutline
    {
      private string langField;
      private Syncfusion.XPS.OutlineEntry[] outlineEntryField;

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
      public string lang
      {
        get => this.langField;
        set => this.langField = value;
      }

      [XmlElement("OutlineEntry")]
      public Syncfusion.XPS.OutlineEntry[] OutlineEntry
      {
        get => this.outlineEntryField;
        set => this.outlineEntryField = value;
      }
    }
}
