// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMRUItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Элемент коллекции "Наиболее часто используемые"</summary>
public interface IMRUItem
{
  /// <summary>Дата и время последнего доступа (в UTC-формате)</summary>
  DateTime LastAccess { get; set; }

  /// <summary>Количество "попаданий" в элемент</summary>
  int HintCount { get; set; }

  /// <summary>Текстовое пояснение элемента</summary>
  string Caption { get; set; }

  /// <summary>Основное значение элемента</summary>
  object Value { get; set; }

  /// <summary>Дополнительное значение элемента</summary>
  object Tag { get; set; }
}
