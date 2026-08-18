// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Table
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
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
    [XmlRoot("TableStructure", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class Table
    {
      private TableRowGroup[] itemsField;

      [XmlElement("TableRowGroupStructure")]
      public TableRowGroup[] Items
      {
        get => this.itemsField;
        set => this.itemsField = value;
      }
    }
}
