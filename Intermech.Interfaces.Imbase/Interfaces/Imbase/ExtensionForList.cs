// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ExtensionForList
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Методы расширения.</summary>
public static class ExtensionForList
{
  /// <summary>Добавление элементов в список.</summary>
  /// <typeparam name="TSource">Тип элементов</typeparam>
  /// <param name="list">Исходный список</param>
  /// <param name="newList">Добавляемый список</param>
  public static void Merge<TSource>(this List<TSource> list, List<TSource> newList)
  {
    if (newList == null || newList.Count <= 0)
      return;
    list.AddRange((IEnumerable<TSource>) newList);
  }

  /// <summary>Слияние таблиц.</summary>
  /// <param name="dtSource">Исходная таблица</param>
  /// <param name="dt">Таблица, данные которой нужно добавить в исходную таблицу</param>
  public static void MergeEx(this DataTable dtSource, DataTable dt)
  {
    if (dt == null || dt.Rows.Count <= 0)
      return;
    dtSource.Merge(dt, false, MissingSchemaAction.Ignore);
  }
}
