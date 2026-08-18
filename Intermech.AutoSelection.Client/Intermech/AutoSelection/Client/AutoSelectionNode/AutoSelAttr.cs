// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelAttr
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelAttr : ICloneable, IComparable
{
  protected Guid _attrGuid;
  protected readonly IAutoSelAttrType _attrType;

  public AutoSelAttr(IAutoSelAttrType attrType)
    : this(Guid.Empty, attrType)
  {
  }

  protected AutoSelAttr(Guid value, IAutoSelAttrType attrType)
  {
    this._attrGuid = value;
    this._attrType = attrType;
  }

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_16")]
  [CustomDescription("Attribute.AutoSelection.Client_17")]
  [TypeConverter(typeof (AttributeTypeConverter))]
  [Editor(typeof (AutoSelAttrEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public Guid AttrGuid
  {
    get => this._attrGuid;
    set => this._attrGuid = value;
  }

  [Browsable(false)]
  public IAutoSelAttrType AttrType => this._attrType;

  public virtual XmlNode Save(XmlDocument doc)
  {
    XmlElement element = doc.CreateElement(nameof (AutoSelAttr));
    XmlAttribute attribute = doc.CreateAttribute("AttrType");
    attribute.Value = this._attrGuid.ToString();
    XmlAttributeCollection attributes = element.Attributes;
    if (attributes == null)
      return (XmlNode) element;
    attributes.Append(attribute);
    return (XmlNode) element;
  }

  public static AutoSelAttr Load(XmlNode node, IAutoSelAttrType attrType)
  {
    return !node.Name.Equals(nameof (AutoSelAttr)) || node.Attributes == null ? (AutoSelAttr) null : new AutoSelAttr(new Guid(node.Attributes["AttrType"].Value), attrType);
  }

  public virtual object Clone() => (object) new AutoSelAttr(this._attrGuid, this._attrType);

  public int CompareTo(object obj)
  {
    return this.AttrGuid.CompareTo(obj is AutoSelAttr autoSelAttr ? autoSelAttr.AttrGuid : Guid.Empty);
  }

  public override string ToString()
  {
    if (!(this.AttrGuid != Guid.Empty))
      return LocalizationHolder.rm.GetString("AutoSelection.Client_9");
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttrGuid);
    return attributeType != null ? attributeType.Name : LocalizationHolder.rm.GetString("AutoSelection.Client_8");
  }
}
