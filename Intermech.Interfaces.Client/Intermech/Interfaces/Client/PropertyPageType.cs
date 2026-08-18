// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.PropertyPageType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Тип страницы свойств</summary>
public enum PropertyPageType
{
  /// <summary>Не определен</summary>
  Unknown,
  /// <summary>
  /// Страничка представляет собой элемент, основанный на UserControl
  /// </summary>
  Control,
  /// <summary>
  /// На страничке отображаются свойства объекта. Свойства объекта изменяются в PropertyGrid
  /// </summary>
  Object,
}
