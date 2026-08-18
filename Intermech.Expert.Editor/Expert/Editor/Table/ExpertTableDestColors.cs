// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableDestColors
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

[TypeConverter(typeof (ExpertTableDestColorsConverter))]
internal class ExpertTableDestColors : IExpertTableDestColors
{
  private ExpertTableItemColors _header;
  private ExpertTableItemColors _data;

  public ExpertTableDestColors()
    : this(new ExpertTableItemColors(), new ExpertTableItemColors())
  {
  }

  private ExpertTableDestColors(ExpertTableItemColors header, ExpertTableItemColors data)
  {
    this._header = header;
    this._header.ForeColorChanged += new EventHandler(new EventHandler(this.OnChanged).Invoke);
    this._header.BackColorChanged += new EventHandler(new EventHandler(this.OnChanged).Invoke);
    this._data = data;
    this._data.ForeColorChanged += new EventHandler(new EventHandler(this.OnChanged).Invoke);
    this._data.BackColorChanged += new EventHandler(new EventHandler(this.OnChanged).Invoke);
  }

  [TypeConverter(typeof (ExpertTableItemColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_3")]
  public IExpertTableItemColors Header => (IExpertTableItemColors) this._header;

  [TypeConverter(typeof (ExpertTableItemColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_4")]
  public IExpertTableItemColors Data => (IExpertTableItemColors) this._data;

  public event EventHandler Changed;

  protected virtual void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  public virtual XmlNode Save(XmlDocument xmlDoc)
  {
    XmlElement element = xmlDoc.CreateElement("DestColors");
    XmlNode newChild1 = this._header.Save(xmlDoc);
    XmlNode newChild2 = this._data.Save(xmlDoc);
    element.AppendChild(newChild1);
    element.AppendChild(newChild2);
    return (XmlNode) element;
  }

  public static ExpertTableDestColors Load(XmlNode xmlNode)
  {
    return xmlNode.Name.Equals("DestColors") && xmlNode.ChildNodes.Count.Equals(2) ? new ExpertTableDestColors(ExpertTableItemColors.Load(xmlNode.ChildNodes[0]), ExpertTableItemColors.Load(xmlNode.ChildNodes[1])) : new ExpertTableDestColors();
  }
}
