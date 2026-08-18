// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.Outline
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

[XmlRoot("DocumentStructure.Outline", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
[DesignerCategory("code")]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[Serializable]
public class Outline
{
  private DocumentOutline documentOutlineField;

  public DocumentOutline DocumentOutline
  {
    get => this.documentOutlineField;
    set => this.documentOutlineField = value;
  }
}
