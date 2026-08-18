// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Discard
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace Syncfusion.XPS;

[DesignerCategory("code")]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/discard-control")]
[XmlRoot("Discard", Namespace = "http://schemas.microsoft.com/xps/2005/06/discard-control", IsNullable = false)]
[Serializable]
public class Discard
{
  private string sentinelPageField;
  private string targetField;

  [XmlAttribute(DataType = "anyURI")]
  public string SentinelPage
  {
    get => this.sentinelPageField;
    set => this.sentinelPageField = value;
  }

  [XmlAttribute(DataType = "anyURI")]
  public string Target
  {
    get => this.targetField;
    set => this.targetField = value;
  }
}
