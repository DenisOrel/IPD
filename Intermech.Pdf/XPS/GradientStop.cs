// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.GradientStop
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
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [XmlRoot("GradientStop", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [Serializable]
    public class GradientStop
    {
      private string colorField;
      private double offsetField;

      [XmlAttribute]
      public string Color
      {
        get => this.colorField;
        set => this.colorField = value;
      }

      [XmlAttribute]
      public double Offset
      {
        get => this.offsetField;
        set => this.offsetField = value;
      }
    }
}
