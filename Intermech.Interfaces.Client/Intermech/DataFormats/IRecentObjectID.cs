// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IRecentObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс, позволяющий получить информацию о недавнем объекте
/// </summary>
public interface IRecentObjectID
{
  /// <summary>Идентификатор версии объекта</summary>
  long ObjectID { get; }

  /// <summary>Действие, выполненное над объектом</summary>
  ObjectAction Action { get; }

  /// <summary>Дата и время (UTC) выполнения этого действия</summary>
  DateTime Date { get; }
}
