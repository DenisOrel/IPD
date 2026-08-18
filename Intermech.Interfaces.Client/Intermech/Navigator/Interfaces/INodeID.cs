// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Унифицированный идентификатор элемента пространства навигации
/// </summary>
public interface INodeID
{
  /// <summary>
  /// Идентификатор категории элемента пространства навигации
  /// </summary>
  int CategoryID { get; }

  /// <summary>
  /// Индентификатор типа элемента пространства навигации внутри категории
  /// </summary>
  int TypeID { get; }

  /// <summary>
  /// Возвращает или записывает в идентификатор элемента навигации объект произвольной природы.
  /// Это поле используется внутренними механизмами "Навигатора" и заполняться извне не должно
  /// </summary>
  object Cookie { get; set; }
}
