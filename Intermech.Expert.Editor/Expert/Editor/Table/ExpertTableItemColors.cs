// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableItemColors
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

[TypeConverter(typeof (ExpertTableItemColorsConverter))]
internal class ExpertTableItemColors : IExpertTableItemColors
{
  private Color _foreColor;
  private Color _backColor;

  public ExpertTableItemColors()
    : this(SystemColors.ControlText, SystemColors.Control)
  {
  }

  internal ExpertTableItemColors(Color foreColor, Color backColor)
  {
    this._foreColor = foreColor;
    this._backColor = backColor;
  }

  [CustomDisplayName("Attribute.Expert.Editor_1")]
  public Color ForeColor
  {
    get => this._foreColor;
    set => this._foreColor = value;
  }

  [CustomDisplayName("Attribute.Expert.Editor_2")]
  public Color BackColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  public event EventHandler ForeColorChanged;

  public event EventHandler BackColorChanged;

  protected virtual void OnForeColorChanded()
  {
    EventHandler foreColorChanged = this.ForeColorChanged;
    if (foreColorChanged == null)
      return;
    foreColorChanged((object) this, new EventArgs());
  }

  protected virtual void OnBackColorChanded()
  {
    EventHandler backColorChanged = this.BackColorChanged;
    if (backColorChanged == null)
      return;
    backColorChanged((object) this, new EventArgs());
  }

  public virtual XmlNode Save(XmlDocument xmlDoc)
  {
    XmlElement element1 = xmlDoc.CreateElement("ItemColors");
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("ForeColor");
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("BackColor");
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("IsNamedColor");
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("IsNamedColor");
    attribute1.Value = this._foreColor.IsNamedColor.ToString();
    element2.InnerText = !this._foreColor.IsNamedColor ? this._foreColor.ToArgb().ToString() : this._foreColor.Name;
    attribute2.Value = this._backColor.IsNamedColor.ToString();
    element3.InnerText = !this._backColor.IsNamedColor ? this._backColor.ToArgb().ToString() : this._backColor.Name;
    element2.Attributes.Append(attribute1);
    element3.Attributes.Append(attribute2);
    element1.AppendChild(element2);
    element1.AppendChild(element3);
    return (XmlNode) element1;
  }

  public static ExpertTableItemColors Load(XmlNode xmlNode)
  {
    if (!xmlNode.Name.Equals("ItemColors"))
      return new ExpertTableItemColors();
    XmlElement xmlElement1 = xmlNode["ForeColor"];
    XmlElement xmlElement2 = xmlNode["BackColor"];
    XmlAttribute attribute1 = xmlElement1.Attributes["IsNamedColor"];
    XmlAttribute attribute2 = xmlElement2.Attributes["IsNamedColor"];
    Color empty1 = Color.Empty;
    Color empty2 = Color.Empty;
    Color foreColor;
    try
    {
      foreColor = !Convert.ToBoolean(attribute1.Value) ? Color.FromArgb(Convert.ToInt32(xmlElement1.InnerText)) : Color.FromName(xmlElement1.InnerText);
    }
    catch
    {
      foreColor = SystemColors.ControlText;
    }
    Color backColor;
    try
    {
      backColor = !Convert.ToBoolean(attribute2.Value) ? Color.FromArgb(Convert.ToInt32(xmlElement2.InnerText)) : Color.FromName(xmlElement2.InnerText);
    }
    catch
    {
      backColor = SystemColors.Control;
    }
    return new ExpertTableItemColors(foreColor, backColor);
  }
}
