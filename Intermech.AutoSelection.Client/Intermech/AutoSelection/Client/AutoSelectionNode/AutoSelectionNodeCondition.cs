// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeCondition
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Expert;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeCondition : ICloneable
{
  private Guid _attributeGuid;
  private TempFormula _condition;
  private AutoSelectionNodeCondRule _rule;

  public AutoSelectionNodeCondition(
    Guid attributeGuid,
    TempFormula condition,
    AutoSelectionNodeCondRule rule)
  {
    this._attributeGuid = attributeGuid;
    this._condition = condition;
    this._rule = rule;
  }

  public Guid AttributeGUID
  {
    get => this._attributeGuid;
    set => this._attributeGuid = value;
  }

  public TempFormula Condition
  {
    get => this._condition;
    set => this._condition = value;
  }

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_11")]
  public AutoSelectionNodeCondRule Addon
  {
    get => this._rule;
    set => this._rule = value;
  }

  public XmlNode SaveToXml(XmlDocument doc)
  {
    if (this._attributeGuid.Equals(Guid.Empty))
      return (XmlNode) null;
    XmlNode element = (XmlNode) doc.CreateElement(nameof (AutoSelectionNodeCondition));
    XmlAttribute attribute1 = doc.CreateAttribute("AttributeGuid");
    attribute1.Value = this._attributeGuid.ToString();
    element.Attributes.Append(attribute1);
    if (this._condition != null)
    {
      XmlAttribute attribute2 = doc.CreateAttribute("Condition");
      using (MemoryStream output = new MemoryStream())
      {
        FormulaHeader.SaveFormula(new BinaryWriter((Stream) output), this._condition);
        output.Position = 0L;
        attribute2.Value = Convert.ToBase64String(output.ToArray());
        element.Attributes.Append(attribute2);
      }
    }
    XmlNode newChild = AutoSelEnumUtils.Save("Rule", (int) this._rule, EnumTypeHelper.GetCaption((Enum) this._rule), doc);
    element.AppendChild(newChild);
    return element;
  }

  public static AutoSelectionNodeCondition LoadFromXml(XmlNode node)
  {
    if (node == null || node.Attributes == null || !node.Name.Equals(nameof (AutoSelectionNodeCondition)))
      return (AutoSelectionNodeCondition) null;
    Guid attributeGuid = new Guid(node.Attributes["AttributeGuid"].Value);
    TempFormula condition = (TempFormula) null;
    XmlAttribute attribute = node.Attributes["Condition"];
    if (attribute != null)
    {
      using (MemoryStream input = new MemoryStream(Convert.FromBase64String(attribute.Value)))
        condition = FormulaHeader.LoadFormula(new BinaryReader((Stream) input));
    }
    int id;
    AutoSelEnumUtils.Load("Rule", node, out id);
    AutoSelectionNodeCondRule rule = (AutoSelectionNodeCondRule) id;
    return new AutoSelectionNodeCondition(attributeGuid, condition, rule);
  }

  public override bool Equals(object obj)
  {
    return obj is AutoSelectionNodeCondition selectionNodeCondition ? selectionNodeCondition._attributeGuid.Equals(this._attributeGuid) : base.Equals(obj);
  }

  public override int GetHashCode() => base.GetHashCode();

  public override string ToString()
  {
    string str = (string) null;
    switch (this._rule)
    {
      case AutoSelectionNodeCondRule.None:
        if (this._condition != null)
        {
          str = this._condition.ToString();
          break;
        }
        break;
      case AutoSelectionNodeCondRule.Min:
      case AutoSelectionNodeCondRule.Max:
        str = EnumTypeHelper.GetCaption((Enum) this._rule);
        if (this._condition != null)
        {
          str = $"{str}({this._condition})";
          break;
        }
        break;
    }
    return str ?? LocalizationHolder.rm.GetString("AutoSelection.Client_5");
  }

  public object Clone()
  {
    return (object) new AutoSelectionNodeCondition(this._attributeGuid, this._condition.Clone() as TempFormula, this._rule);
  }
}
