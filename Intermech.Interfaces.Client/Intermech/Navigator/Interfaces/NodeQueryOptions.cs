// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeQueryOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Перечислитель (набор битовых флажков, [Flags]) позволяет задать некоторые
/// опции для интерфейса-запроса к источнику данных INodeQuery.
/// </summary>
[Flags]
public enum NodeQueryOptions
{
  /// <summary>Нет никаких опций</summary>
  None = 0,
  /// <summary>
  /// Перед первым пакетным чтением определить, сколько всего записей может быть получено
  /// </summary>
  ReceiveTotalRecordsCount = 1,
}
