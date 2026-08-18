// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBProjRelationID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Идентифицирует связь с помощью ее глобального идентификатора и идентификатора
/// версии объекта, из которого эта связь выходит.
/// </summary>
public interface IDBProjRelationID
{
  /// <summary>
  /// Возвращает идентификатор версии объекта, из которого связь выходит.
  /// </summary>
  long ProjectId { get; }

  /// <summary>Возвращает глобальный идентификатор связи.</summary>
  Guid RelationGuid { get; }
}
