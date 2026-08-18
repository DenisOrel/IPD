// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.TableCell
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

[XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure")]
[DesignerCategory("code")]
[XmlRoot("TableCellStructure", Namespace = "http://schemas.microsoft.com/xps/2005/06/documentstructure", IsNullable = false)]
[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[Serializable]
public class TableCell
{
  private int columnSpanField = 1;
  private ItemsChoiceType[] itemsElementNameField;
  private object[] itemsField;
  private int rowSpanField = 1;

  [DefaultValue(1)]
  [XmlAttribute]
  public int ColumnSpan
  {
    get => this.columnSpanField;
    set => this.columnSpanField = value;
  }

  [XmlElement("TableStructure", typeof (Table))]
  [XmlChoiceIdentifier("ItemsElementName")]
  [XmlElement("ListStructure", typeof (List))]
  [XmlElement("ParagraphStructure", typeof (Paragraph))]
  [XmlElement("FigureStructure", typeof (Figure))]
  public object[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  [XmlIgnore]
  [XmlElement("ItemsElementName")]
  public ItemsChoiceType[] ItemsElementName
  {
    get => this.itemsElementNameField;
    set => this.itemsElementNameField = value;
  }

  [XmlAttribute]
  [DefaultValue(1)]
  public int RowSpan
  {
    get => this.rowSpanField;
    set => this.rowSpanField = value;
  }
}
