// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.ArcSegment
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
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [XmlRoot("ArcSegment", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [Serializable]
    public class ArcSegment
    {
      private bool isLargeArcField;
      private bool isStrokedField = true;
      private string pointField;
      private double rotationAngleField;
      private string sizeField;
      private SweepDirection sweepDirectionField;

      [XmlAttribute]
      public bool IsLargeArc
      {
        get => this.isLargeArcField;
        set => this.isLargeArcField = value;
      }

      [DefaultValue(true)]
      [XmlAttribute]
      public bool IsStroked
      {
        get => this.isStrokedField;
        set => this.isStrokedField = value;
      }

      [XmlAttribute]
      public string Point
      {
        get => this.pointField;
        set => this.pointField = value;
      }

      [XmlAttribute]
      public double RotationAngle
      {
        get => this.rotationAngleField;
        set => this.rotationAngleField = value;
      }

      [XmlAttribute]
      public string Size
      {
        get => this.sizeField;
        set => this.sizeField = value;
      }

      [XmlAttribute]
      public SweepDirection SweepDirection
      {
        get => this.sweepDirectionField;
        set => this.sweepDirectionField = value;
      }
    }
}
