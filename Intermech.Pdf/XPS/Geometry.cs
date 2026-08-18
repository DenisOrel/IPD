// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Geometry
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
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlRoot("Glyphs.Clip", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [Serializable]
    public class Geometry
    {
      private PathGeometry pathGeometryField;

      public PathGeometry PathGeometry
      {
        get => this.pathGeometryField;
        set => this.pathGeometryField = value;
      }
    }
}
