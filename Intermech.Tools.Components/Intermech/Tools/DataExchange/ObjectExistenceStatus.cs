// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ObjectExistenceStatus
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Описывает статусы существования объекта IPS в базе данных на момент начала анализа изменений.
/// </summary>
public enum ObjectExistenceStatus
{
  /// <summary>Существующий объект</summary>
  ExistingObject,
  /// <summary>Новый объект, созданны в процессе анализа изменений</summary>
  NewObject,
  /// <summary>
  /// Новый объект, полученный путем изменения типа существующего объекта
  /// </summary>
  ConvertedObject,
}
