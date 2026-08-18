
// Type: Intermech.Navigator.DBObjects.RecordMappingExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using Intermech.Navigator.Queries;
using Intermech.Navigator.VirtualColumns;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>Расширения класса RecordMapping для использования в AdvRelatedObjectsQuery и его потомках</summary>
public static class RecordMappingExtensions
{
  /// <summary>Проверка, что индекс поля найден в таблице, попытаться загрузить его, свалить exception</summary>
  private static void CheckFieldIndexInternal(
    [NotNull] this RecordMapping mapping,
    ref int fieldIndex,
    bool throwException,
    [CanBeNull] object field,
    [CanBeNull] string fieldName = null)
  {
    if (fieldIndex != -1)
      return;
    fieldIndex = Array.IndexOf<object>(mapping.Fields, field);
    if (fieldIndex == -1 & throwException)
      throw new KeyNotFoundException($"Can`t find {(object) fieldName ?? field} field");
  }

  /// <summary>Проверка, что индекс поля найден в таблице, попытаться загрузить его, свалить exception</summary>
  public static void CheckFieldIndex(
    [NotNull] this RecordMapping mapping,
    ref int fieldIndex,
    [NotNull] NodeColumnID nodeColumnID,
    [CanBeNull] string fieldName = null)
  {
    mapping.CheckFieldIndex(ref fieldIndex, true, nodeColumnID, fieldName);
  }

  /// <summary>Проверка, что индекс поля найден в таблице, попытаться загрузить его, свалить exception</summary>
  public static void CheckFieldIndex(
    [NotNull] this RecordMapping mapping,
    ref int fieldIndex,
    bool throwException,
    [NotNull] NodeColumnID nodeColumnID,
    [CanBeNull] string fieldName = null)
  {
    mapping.CheckFieldIndexInternal(ref fieldIndex, throwException, (object) nodeColumnID, fieldName);
  }

  /// <summary>Проверка, что индекс поля найден в таблице, попытаться загрузить его, свалить exception</summary>
  public static void CheckFieldIndex(
    [NotNull] this RecordMapping mapping,
    ref int fieldIndex,
    [NotNull] VirtualQueryResultColumn virtualColumn,
    [CanBeNull] string fieldName = null)
  {
    mapping.CheckFieldIndexInternal(ref fieldIndex, true, (object) virtualColumn, fieldName);
  }
}
