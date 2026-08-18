// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.MatrixTransform
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
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [XmlRoot("MatrixTransform", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [DesignerCategory("code")]
    [Serializable]
    public class MatrixTransform
    {
      private string keyField;
      private string matrixField;

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
      public string Key
      {
        get => this.keyField;
        set => this.keyField = value;
      }

      [XmlAttribute]
      public string Matrix
      {
        get => this.matrixField;
        set => this.matrixField = value;
      }
    }
}
