// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocRowAttributePropertyDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Обёртка для дополнительных атрибутов строки документа отображающей запись спецификации</summary>
[Serializable]
public class DocRowAttributePropertyDescriptor : PropertyDescriptor
{
  private string attributeName;

  public DocRowAttributePropertyDescriptor(string attributeName)
    : base(attributeName, (Attribute[]) null)
  {
    this.attributeName = attributeName;
  }

  public override string Description => this.attributeName;

  public override string DisplayName => this.attributeName;

  /// <summary>Атрибуты свойства</summary>
  public override AttributeCollection Attributes
  {
    get
    {
      Attribute[] attributeArray = new Attribute[base.Attributes.Count + 1];
      base.Attributes.CopyTo((Array) attributeArray, 0);
      attributeArray[attributeArray.Length - 1] = (Attribute) new CategoryAttribute(this.Category);
      return new AttributeCollection(attributeArray);
    }
  }

  public override string Category => "Дополнительные атрибуты записи";

  public override object GetValue(object component)
  {
    string str = (string) null;
    AVSRow avsRow = (AVSRow) component;
    if (avsRow.DocNode != null)
      str = avsRow.DocNode.GetAttributeValue(this.attributeName, false);
    return (object) str;
  }

  public override void SetValue(object component, object value)
  {
    AVSRow avsRow = (AVSRow) component;
    if (avsRow.DocNode == null)
      return;
    avsRow.DocNode.SetAttributeValue(this.attributeName, Convert.ToString(value), false);
  }

  public override bool CanResetValue(object component) => false;

  public override Type ComponentType => typeof (AVSRow);

  public override bool IsReadOnly => false;

  public override Type PropertyType => typeof (string);

  public override void ResetValue(object component)
  {
  }

  public override bool ShouldSerializeValue(object component) => true;
}
