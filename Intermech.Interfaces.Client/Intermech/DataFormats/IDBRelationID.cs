// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBRelationID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс-формат для передачи данных об идентификаторах связей
/// базы данных через clipboard, а также между различными частями
/// универсального клиента.
/// </summary>
public interface IDBRelationID
{
  /// <summary>Идентификатор связи между объектами</summary>
  long Value { get; }

  /// <summary>
  /// Идентификатор версии объекта, входящего по этой связи в другой объект
  /// </summary>
  long PartID { get; }

  /// <summary>Идентификатор типа связи</summary>
  int RelationType { get; }

  /// <summary>Значение атрибута "Сортировка"</summary>
  long Sorting { get; }

  /// <summary>Идентификатор версии родительского объекта</summary>
  long ProjID { get; }

  /// <summary>Guid связи</summary>
  Guid RelGuid { get; }
}
