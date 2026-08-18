// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorContextSearchOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Опции для контекстного поиска в элементе управления Навигатора, содержащем список строк
/// </summary>
[Flags]
[Serializable]
public enum NavigatorContextSearchOptions
{
  /// <summary>Нет никаких настроек</summary>
  None = 0,
  /// <summary>Стандартные опции для поиска</summary>
  Default = 0,
  /// <summary>Поиск выполняется с учётом регистра букв</summary>
  CaseSensitive = 1,
  /// <summary>Поиск слов целиком</summary>
  WholeWords = 2,
  /// <summary>Поиск с учётом маски (* / ?)</summary>
  WithMask = 4,
  /// <summary>Маска - регулярное выражение</summary>
  WithRegularExpression = 8,
  /// <summary>Поиск только в активной колонке</summary>
  InActiveColumn = 16, // 0x00000010
  /// <summary>Поиск начинается с начала (а не с текущей позиции)</summary>
  StartFromOrigin = 32, // 0x00000020
  /// <summary>Направление поиска - назад (а не в обратную сторону)</summary>
  BackDirection = 64, // 0x00000040
  /// <summary>
  /// Поиск найдёт и отметит все подходящие записи
  /// (игнорируются флажки InActiveColumn и ForwardDirection). Если флажка нет, то
  /// будет выполняться цикличный поиск
  /// </summary>
  FindAll = 128, // 0x00000080
}
