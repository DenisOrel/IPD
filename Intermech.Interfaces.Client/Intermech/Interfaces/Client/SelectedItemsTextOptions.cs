// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SelectedItemsTextOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Опции для интерфейса ISelectedItemsText</summary>
[Flags]
[Serializable]
public enum SelectedItemsTextOptions
{
  /// <summary>Нет никаких опций</summary>
  None = 0,
  /// <summary>
  /// Добавлять в начало текста заголовки столбцов, разделённые указанной строкой
  /// </summary>
  ColumnsCaptions = 1,
  /// <summary>
  /// Добавлять в текст только содержимое первого выделенного элемента
  /// </summary>
  FirstItemOnly = 16, // 0x00000010
  /// <summary>
  /// Добавлять в текст только заголовки столбцов, разделённые указанной строкой.
  /// Больше ничего не будет добавлено
  /// </summary>
  ColumnsCaptionsOnly = 32, // 0x00000020
  /// <summary>Опции по умолчанию (ColumnsCaptions)</summary>
  Default = ColumnsCaptions, // 0x00000001
}
