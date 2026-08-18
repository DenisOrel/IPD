// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeColumnSortOrder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Возможные направления сортировки данных в виртуальных колонках "Навигатора"
/// </summary>
public enum NodeColumnSortOrder
{
  /// <summary>Колонка не участвует в сортировке</summary>
  None,
  /// <summary>Содержимое колонки сортируется по возрастанию</summary>
  Ascending,
  /// <summary>Содержимое колонки сортируется по убыванию</summary>
  Descending,
}
