// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.ISelectorFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Для передачи в SelectorForm для фильтрации в дереве выбора
/// </summary>
public interface ISelectorFilter
{
  /// <summary>проверка на попадание в фильтр</summary>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор</param>
  /// <returns></returns>
  bool IsInFilter(int category, object id);
}
