// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Brush
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
    [XmlRoot("Glyphs.OpacityMask", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [Serializable]
    public class Brush
    {
      private object itemField;

      [XmlElement("SolidColorBrush", typeof (SolidColorBrush))]
      [XmlElement("VisualBrush", typeof (VisualBrush))]
      [XmlElement("LinearGradientBrush", typeof (LinearGradientBrush))]
      [XmlElement("RadialGradientBrush", typeof (RadialGradientBrush))]
      [XmlElement("ImageBrush", typeof (ImageBrush))]
      public object Item
      {
        get => this.itemField;
        set => this.itemField = value;
      }
    }
}
