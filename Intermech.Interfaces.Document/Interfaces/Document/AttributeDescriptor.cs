// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AttributeDescriptor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Описатель атрибута из AdditionalAttributeCollection для преставления его в PropertyGrid</summary>
[Serializable]
public class AttributeDescriptor : CustomPropertyDescriptor
{
  /// <summary>Имя атрибута</summary>
  private string attributeName;

  /// <summary>Конструктор</summary>
  /// <param name="attributeName">Имя атрибута</param>
  public AttributeDescriptor(string attributeName)
    : base(attributeName, (Attribute[]) null)
  {
    this.attributeName = attributeName;
  }

  /// <summary>Атрибуты свойства</summary>
  public override AttributeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      if (base.Attributes.Contains((Attribute) RefreshPropertiesAttribute.All))
        return base.Attributes;
      Attribute[] attributeArray = new Attribute[base.Attributes.Count + 1];
      base.Attributes.CopyTo((Array) attributeArray, 0);
      attributeArray[attributeArray.Length - 1] = (Attribute) RefreshPropertiesAttribute.All;
      return new AttributeCollection(attributeArray);
    }
  }

  private static string GetAdditionalAttributeValueStr(
    string attributeName,
    DocumentTreeNode node,
    string cur_var)
  {
    if (!(node is RectangleElement rectangleElement) || rectangleElement.IsSingleCell)
      return node.GetAttributeValue(attributeName, true);
    if (rectangleElement.Nodes.Count == 0)
      return cur_var;
    DocumentTreeNode node1 = rectangleElement.Nodes[0];
    string attributeValueStr;
    if ((attributeValueStr = AttributeDescriptor.GetAdditionalAttributeValueStr(attributeName, node1, cur_var)) == null)
      return (string) null;
    if (cur_var != null && attributeValueStr != cur_var)
      return (string) null;
    int index = 1;
    for (int count = rectangleElement.Nodes.Count; index < count; ++index)
    {
      DocumentTreeNode node2 = rectangleElement.Nodes[index];
      if (attributeValueStr != AttributeDescriptor.GetAdditionalAttributeValueStr(attributeName, node2, attributeValueStr))
        return (string) null;
    }
    return attributeValueStr;
  }

  private DocumentTreeNode SetAdditionalAttributeValueStr(
    string attributeName,
    DocumentTreeNode cell,
    string cur_var)
  {
    DocumentTreeNode documentTreeNode = cell;
    if (!(cell as RectangleElement).IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        DocumentTreeNode node = cell.Nodes[index];
        documentTreeNode = this.SetAdditionalAttributeValueStr(attributeName, node, cur_var);
      }
      return documentTreeNode;
    }
    cell.SetAttributeValue(attributeName, cur_var, updateUI: false, updateLayout: false);
    return cell;
  }

  /// <summary>Получить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component)
  {
    DocumentTreeNode owner = ((AdditionalAttributeCollection) component).Owner;
    return owner.IsVirtualNode ? (object) AttributeDescriptor.GetAdditionalAttributeValueStr(this.attributeName, owner, (string) null) : (object) ((AdditionalAttributeCollection) component).Owner.GetAttributeValue(this.attributeName, false);
  }

  /// <summary>Установить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    DocumentTreeNode owner = ((AdditionalAttributeCollection) component).Owner;
    if (owner.IsVirtualNode)
      this.SetAdditionalAttributeValueStr(this.attributeName, owner, (string) value).UpdateLayout(true);
    else
      ((AdditionalAttributeCollection) component).Owner.SetAttributeValue(this.attributeName, (string) value);
  }

  /// <summary>Отображаемое имя свойства</summary>
  public override string DisplayName
  {
    [DebuggerStepThrough] get => this.attributeName;
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => false;

  /// <summary>Тип владельца свойства</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => typeof (AdditionalAttributeCollection);
  }

  /// <summary>Тип свойства</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => typeof (string);
  }

  /// <summary>Сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component) => false;

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Категория свойства</summary>
  public override string Category
  {
    [DebuggerStepThrough] get
    {
      string category = base.Category;
      if (category == "Misc")
        category = LocalizationHolder.rm.GetString("Interfaces.Document_1");
      return category;
    }
  }
}
