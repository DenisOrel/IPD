// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavigatorContextSearchHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер настроек для контекстного поиска в элементе управления Навигатора, содержащем список строк
/// </summary>
public interface INavigatorContextSearchHolder : IAssignable
{
  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  string Mask { get; set; }

  /// <summary>История значений для поиска</summary>
  List<string> History { get; }

  /// <summary>Опции для поиска</summary>
  NavigatorContextSearchOptions Options { get; set; }

  /// <summary>
  /// Номер строки (y) и столбца (x), которые были найдены при последнем поиске.
  /// Данное поле является точкой отсчёта для дальнейшего поиска
  /// </summary>
  Point LastFoundItem { get; set; }
}
