// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellFlags
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Флаги для внутреннего пользования (вместо булевских полей)</summary>
[Flags]
public enum CellFlags : uint
{
  None = 0,
  /// <summary>Таблица требует второго прохода разбивки</summary>
  NeedSecondLayoutPass = 1,
  /// <summary>Результат попытки оставить ячейку целой в родной (первой) таблице</summary>
  TryNotBreak_Failed0 = 2,
  /// <summary>Попытка оставить ячейку целой в следующей (второй) таблице</summary>
  TryNotBreak_Failed1 = 4,
  /// <summary>Шаблон ячейки данных в таблице выбран для показа.
  /// Используется чтобы показывать только выбранные шаблоны строк, так как все могут не поместиться</summary>
  SelectedDataCellTemplate = 8,
  /// <summary>При отрисовке содержимого ячейки нужно обновить формулы</summary>
  NeedUpdateFormulas = 16, // 0x00000010
  /// <summary>В таблице помещаются только заголовоки</summary>
  TableAllocateOnlyHeaders = 32, // 0x00000020
}
