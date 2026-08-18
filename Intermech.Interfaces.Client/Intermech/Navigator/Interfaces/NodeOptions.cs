// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Набор дополнительных свойств элемента пространства навигации
/// </summary>
[Flags]
[Serializable]
public enum NodeOptions
{
  /// <summary>Никаких опций нет</summary>
  None = 0,
  /// <summary>Узел может иметь состав</summary>
  CanContainsComposition = 1,
  /// <summary>Узел может содержать список объектов</summary>
  CanContainsObjectsList = 2,
  /// <summary>
  /// Узел может содержать список связей
  /// (например, виртуальный узел)
  /// </summary>
  CanContainsRelationsList = 4,
  /// <summary>Узел может содержать список типов объектов</summary>
  CanContainsObjectTypesList = 16, // 0x00000010
  /// <summary>Значение свойств по умолчанию - "Никаких опций нет"</summary>
  Default = 0,
}
