// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AdditionalAttributeCollection
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Коллекция атрибутов. Вспомогательный класс для внутреннего пользования</summary>
[TypeConverter(typeof (AdditionalAttributesConverter))]
[Serializable]
public class AdditionalAttributeCollection : ICloneable
{
  private IDictionary attributes = (IDictionary) new HybridDictionary();
  private DocumentTreeNode owner;

  /// <summary>Владелец коллекции</summary>
  public virtual DocumentTreeNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
    set => this.owner = value;
  }

  /// <summary>Конструктор. Создает коллекцию для заданного владельца</summary>
  /// <param name="owner">Владелец коллекции</param>
  public AdditionalAttributeCollection(DocumentTreeNode owner) => this.Owner = owner;

  /// <summary>Количество атрибутов</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.attributes.Count;
  }

  /// <summary>Проверяет содержит ли коллекция атрибут с заданным именем</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <returns>Возвращеает true, если коллекция содержит атрибут с заданным именем</returns>
  public bool ContainsAttribute(string attributeName)
  {
    return this.attributes.Count != 0 && this.attributes.Contains((object) attributeName);
  }

  /// <summary>Коллекция имен атрибутов</summary>
  public ICollection Keys
  {
    [DebuggerStepThrough] get => this.attributes.Keys;
  }

  /// <summary>Установить строковое значение атрибута.
  /// Если атрибута нет в коллекции, то он будет добавлен.</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  [DebuggerStepThrough]
  public virtual void SetAttributeStringValue(string attributeName, string attributeValue)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (this.Owner.IsVirtualNode)
      AdditionalAttributeCollection.SetAdditionalAttributes(this.Owner, attributeName, attributeValue);
    else
      this.attributes[(object) attributeName] = (object) new AddAttrValue((object) attributeValue, typeof (string), true);
  }

  /// <summary>Установить значение атрибута.
  /// Если атрибута нет в коллекции, то он будет добавлен.</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение (не обязательно строковое) атрибута</param>
  /// <param name="attributeType">Тип значения атрибута</param>
  [DebuggerStepThrough]
  public virtual void SetAttributeValue(
    string attributeName,
    object attributeValue,
    Type attributeType = null,
    bool showInPropGrid = false,
    TypeConverter converter = null)
  {
    if (string.IsNullOrWhiteSpace(attributeName))
      throw new ArgumentNullException(nameof (attributeName));
    AddAttrValue addAttrValue = attributeValue as AddAttrValue;
    if (this.Owner.IsVirtualNode)
    {
      string attributeValue1 = addAttrValue?.ToString() ?? attributeValue.ToString();
      AdditionalAttributeCollection.SetAdditionalAttributes(this.Owner, attributeName, attributeValue1);
    }
    else if (addAttrValue != null)
      this.attributes[(object) attributeName] = (object) addAttrValue;
    else if (this.attributes[(object) attributeName] is AddAttrValue attribute && (attributeValue == null || attributeValue.GetType() == attribute.Type))
      attribute.Value = attributeValue;
    else
      this.attributes[(object) attributeName] = (object) new AddAttrValue(attributeValue, attributeType, converter)
      {
        IsShownInPropertyGrid = showInPropGrid
      };
  }

  /// <summary>Установить атрибуты дочерних ячеек</summary>
  private static void SetAdditionalAttributes(
    DocumentTreeNode node,
    string attributeName,
    string attributeValue)
  {
    if (node is RectangleElement rectangleElement && !rectangleElement.IsSingleCell)
    {
      int index = 0;
      for (int count = rectangleElement.Nodes.Count; index < count; ++index)
        AdditionalAttributeCollection.SetAdditionalAttributes(rectangleElement.Nodes[index], attributeName, attributeValue);
    }
    else
      node.SetAttributeValue(attributeName, attributeValue);
  }

  /// <summary>Получить значение атрибута с заданным именем</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <returns>Возвращает значение атрибута с заданным именем.
  /// Если атрибута нет в коллекции, то вернет null.</returns>
  [DebuggerStepThrough]
  public virtual object GetAttributeValue(string attributeName)
  {
    if (this.attributes.Count == 0)
      return (object) null;
    return this.attributes[(object) attributeName] is AddAttrValue attribute ? attribute.Value : this.attributes[(object) attributeName];
  }

  /// <summary>Получить строковое значение атрибута с заданным именем</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку ("") вместо null</param>
  /// <returns>Возвращает строковое значение атрибута. Если notNull = true,
  /// то вернет пустую строку вместо null.</returns>
  public virtual string GetAttributeStringValue(string attributeName, bool notNull)
  {
    object obj = (object) null;
    if (this.attributes.Count != 0)
      obj = this.attributes[(object) attributeName];
    if (obj != null)
      return obj.ToString();
    return notNull ? "" : (string) null;
  }

  /// <summary>Удалить атрибут с заданным именем из коллекции</summary>
  /// <param name="attributeName">Имя атрибута</param>
  public virtual void RemoveAttribute(string attributeName)
  {
    this.attributes.Remove((object) attributeName);
  }

  /// <summary>Словарь с парами имя-значение</summary>
  internal IDictionary Attributes
  {
    [DebuggerStepThrough] get => this.attributes;
    set => this.attributes = value;
  }

  /// <summary>Создать полную копию коллекции</summary>
  /// <returns>Возвращает полную копию коллекции</returns>
  public object Clone()
  {
    AdditionalAttributeCollection attributeCollection = new AdditionalAttributeCollection((DocumentTreeNode) null);
    attributeCollection.attributes = (IDictionary) new HybridDictionary();
    foreach (DictionaryEntry attribute in this.attributes)
    {
      string key = attribute.Key.ToString();
      if (attribute.Value is string str)
        attributeCollection.attributes.Add((object) key, (object) new AddAttrValue((object) str, typeof (string), true));
      else if (attribute.Value is AddAttrValue addAttrValue)
        attributeCollection.attributes.Add((object) key, (object) addAttrValue.Clone());
      else
        attributeCollection.attributes.Add((object) key, attribute.Value);
    }
    return (object) attributeCollection;
  }
}
