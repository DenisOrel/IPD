// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Canvas
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
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlRoot("Canvas", Namespace = "http://schemas.microsoft.com/xps/2005/06", IsNullable = false)]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06")]
    [Serializable]
    public class Canvas
    {
      private string automationPropertiesHelpTextField;
      private string automationPropertiesNameField;
      private Geometry canvasClipField;
      private Brush canvasOpacityMaskField;
      private Transform canvasRenderTransformField;
      private Resources canvasResourcesField;
      private string clipField;
      private string fixedPageNavigateUriField;
      private object[] itemsField;
      private string keyField;
      private string langField;
      internal object m_parent;
      private string nameField;
      private double opacityField = 1.0;
      private string opacityMaskField;
      private EdgeMode renderOptionsEdgeModeField;
      private bool renderOptionsEdgeModeFieldSpecified;
      private string renderTransformField;

      [XmlAttribute("AutomationProperties.HelpText")]
      public string AutomationPropertiesHelpText
      {
        get => this.automationPropertiesHelpTextField;
        set => this.automationPropertiesHelpTextField = value;
      }

      [XmlAttribute("AutomationProperties.Name")]
      public string AutomationPropertiesName
      {
        get => this.automationPropertiesNameField;
        set => this.automationPropertiesNameField = value;
      }

      [XmlElement("Canvas.Clip")]
      public Geometry CanvasClip
      {
        get => this.canvasClipField;
        set => this.canvasClipField = value;
      }

      [XmlElement("Canvas.OpacityMask")]
      public Brush CanvasOpacityMask
      {
        get => this.canvasOpacityMaskField;
        set => this.canvasOpacityMaskField = value;
      }

      [XmlElement("Canvas.RenderTransform")]
      public Transform CanvasRenderTransform
      {
        get => this.canvasRenderTransformField;
        set => this.canvasRenderTransformField = value;
      }

      [XmlElement("Canvas.Resources")]
      public Resources CanvasResources
      {
        get => this.canvasResourcesField;
        set => this.canvasResourcesField = value;
      }

      [XmlAttribute]
      public string Clip
      {
        get => this.clipField;
        set => this.clipField = value;
      }

      [XmlAttribute("FixedPage.NavigateUri", DataType = "anyURI")]
      public string FixedPageNavigateUri
      {
        get => this.fixedPageNavigateUriField;
        set => this.fixedPageNavigateUriField = value;
      }

      [XmlElement("Glyphs", typeof (Glyphs))]
      [XmlElement("Canvas", typeof (Canvas))]
      [XmlElement("Path", typeof (Path))]
      public object[] Items
      {
        get => this.itemsField;
        set => this.itemsField = value;
      }

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/xps/2005/06/resourcedictionary-key")]
      public string Key
      {
        get => this.keyField;
        set => this.keyField = value;
      }

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
      public string lang
      {
        get => this.langField;
        set => this.langField = value;
      }

      [XmlAttribute(DataType = "ID")]
      public string Name
      {
        get => this.nameField;
        set => this.nameField = value;
      }

      [XmlAttribute]
      [DefaultValue(1)]
      public double Opacity
      {
        get => this.opacityField;
        set => this.opacityField = value;
      }

      [XmlAttribute]
      public string OpacityMask
      {
        get => this.opacityMaskField;
        set => this.opacityMaskField = value;
      }

      [XmlAttribute("RenderOptions.EdgeMode")]
      public EdgeMode RenderOptionsEdgeMode
      {
        get => this.renderOptionsEdgeModeField;
        set => this.renderOptionsEdgeModeField = value;
      }

      [XmlIgnore]
      public bool RenderOptionsEdgeModeSpecified
      {
        get => this.renderOptionsEdgeModeFieldSpecified;
        set => this.renderOptionsEdgeModeFieldSpecified = value;
      }

      [XmlAttribute]
      public string RenderTransform
      {
        get => this.renderTransformField;
        set => this.renderTransformField = value;
      }
    }
}
