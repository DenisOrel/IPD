// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesStructureScheme
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives;

/// <summary>схема колонок Структура архива</summary>
public class ArchivesStructureScheme : ObjectColumnScheme
{
  /// <summary>
  /// 
  /// </summary>
  public static Guid ArchivesStructureSchemeGuid = new Guid("08975EF6-4250-4939-9F59-E092900444F6");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public override string Name => ServiceHolder.rm.GetString("Archives_74");

  /// <summary>
  /// Создает виртуальную колонку без сортировки по указанному
  /// идентификатору. Если колонки с заданным идентификатором в схеме нет -
  /// то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Виртуальная колонка</returns>
  public new NodeColumn CreateColumn(Guid schemeGuid, object columnID)
  {
    return this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  public new NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    NodeColumn column = base.CreateColumn(schemeGuid, columnID, sortOrder, sortIndex);
    if (column != null)
      column.Priority = SchemeColumnPriority.Highest;
    return column;
  }
}
