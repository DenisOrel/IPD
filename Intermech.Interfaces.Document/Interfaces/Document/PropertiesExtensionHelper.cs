// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PropertiesExtensionHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections;

#nullable disable
namespace Intermech.Interfaces.Document;

public static class PropertiesExtensionHelper
{
  /// <summary>Удалить свойство из списка</summary>
  /// <param name="properties">Список свойств</param>
  /// <param name="propertyName">Имя свойства</param>
  public static void RemoveProperty(this IDictionary properties, string propertyName)
  {
    properties.Remove((object) propertyName);
  }

  /// <summary>Установить флаг ReadOnly для свойства, если оно есть</summary>
  /// <param name="properties">Словарь свойств</param>
  /// <param name="propertyName">Имя свойства</param>
  /// <param name="value">Значение</param>
  public static void SetReadOnlyProperty(
    this IDictionary properties,
    string propertyName,
    bool value)
  {
    if (!(properties[(object) propertyName] is CustomPropertyDescriptor property))
      return;
    property.SetIsReadOnly(value);
  }
}
