// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableColors
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

internal class ExpertTableColors : IExpertTableColors
{
  private ExpertTableDestColors _inputHorz;
  private ExpertTableDestColors _inputVert;
  private ExpertTableItemColors _output;
  private ExpertTableItemColors _data;

  public ExpertTableColors()
    : this(new ExpertTableDestColors(), new ExpertTableDestColors(), new ExpertTableItemColors(), new ExpertTableItemColors(SystemColors.WindowText, SystemColors.Window))
  {
  }

  private ExpertTableColors(
    ExpertTableDestColors inputHorz,
    ExpertTableDestColors inputVert,
    ExpertTableItemColors output,
    ExpertTableItemColors data)
  {
    this._inputHorz = inputHorz;
    this._inputVert = inputVert;
    this._output = output;
    this._data = data;
  }

  [TypeConverter(typeof (ExpertTableDestColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_5")]
  public IExpertTableDestColors InputVert => (IExpertTableDestColors) this._inputVert;

  [TypeConverter(typeof (ExpertTableDestColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_6")]
  public IExpertTableDestColors InputHorz => (IExpertTableDestColors) this._inputHorz;

  [TypeConverter(typeof (ExpertTableItemColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_7")]
  public IExpertTableItemColors Output => (IExpertTableItemColors) this._output;

  [TypeConverter(typeof (ExpertTableItemColorsConverter))]
  [CustomDisplayName("Attribute.Expert.Editor_8")]
  public IExpertTableItemColors Data => (IExpertTableItemColors) this._data;

  public event EventHandler Changed;

  public virtual XmlNode Save(XmlDocument xmlDoc)
  {
    XmlElement element = xmlDoc.CreateElement("TableColors");
    XmlNode newChild1 = this._inputHorz.Save(xmlDoc);
    XmlNode newChild2 = this._inputVert.Save(xmlDoc);
    XmlNode newChild3 = this._output.Save(xmlDoc);
    XmlNode newChild4 = this._data.Save(xmlDoc);
    element.AppendChild(newChild1);
    element.AppendChild(newChild2);
    element.AppendChild(newChild3);
    element.AppendChild(newChild4);
    return (XmlNode) element;
  }

  protected virtual void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  public static ExpertTableColors Load(XmlNode xmlNode)
  {
    if (!xmlNode.Name.Equals("TableColors") || !xmlNode.ChildNodes.Count.Equals(4))
      return new ExpertTableColors();
    ExpertTableDestColors inputHorz = ExpertTableDestColors.Load(xmlNode.ChildNodes[0]);
    ExpertTableDestColors expertTableDestColors = ExpertTableDestColors.Load(xmlNode.ChildNodes[1]);
    ExpertTableItemColors expertTableItemColors1 = ExpertTableItemColors.Load(xmlNode.ChildNodes[2]);
    ExpertTableItemColors expertTableItemColors2 = ExpertTableItemColors.Load(xmlNode.ChildNodes[3]);
    ExpertTableDestColors inputVert = expertTableDestColors;
    ExpertTableItemColors output = expertTableItemColors1;
    ExpertTableItemColors data = expertTableItemColors2;
    return new ExpertTableColors(inputHorz, inputVert, output, data);
  }
}
