// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IGridContextSearchHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер настроек для контекстного поиска в элементе управления Навигатора, содержащем список строк
/// (для программного поиска)
/// </summary>
public interface IGridContextSearchHolder : IAssignable
{
  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  string Mask { get; set; }

  /// <summary>Опции для поиска</summary>
  GridContextSearchOptions Options { get; set; }
}
