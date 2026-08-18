// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionDefRow
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionDefRow : ICloneable
{
  protected long _rowId;

  public AutoSelectionDefRow(long rowId) => this._rowId = rowId;

  public long RowID
  {
    get => this._rowId;
    set => this._rowId = value;
  }

  public XmlNode SaveToXml(XmlDocument doc)
  {
    if (this._rowId.Equals(0L))
      return (XmlNode) null;
    XmlElement element = doc.CreateElement(nameof (AutoSelectionDefRow));
    XmlAttribute attribute = doc.CreateAttribute("RowID");
    attribute.Value = this._rowId.ToString();
    element.Attributes.Append(attribute);
    return (XmlNode) element;
  }

  public static AutoSelectionDefRow LoadFromXml(XmlNode node)
  {
    if (node == null || node.Attributes == null || !node.Name.Equals(nameof (AutoSelectionDefRow)))
      return (AutoSelectionDefRow) null;
    XmlAttribute attribute = node.Attributes["RowID"];
    return new AutoSelectionDefRow(attribute != null ? long.Parse(attribute.Value) : 0L);
  }

  public override bool Equals(object obj)
  {
    return obj is AutoSelectionDefRow autoSelectionDefRow ? autoSelectionDefRow._rowId.Equals(this._rowId) : base.Equals(obj);
  }

  public override int GetHashCode() => base.GetHashCode();

  public override string ToString() => base.ToString();

  public object Clone() => (object) new AutoSelectionDefRow(this._rowId);
}
