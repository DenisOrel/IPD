// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICreatorMultiObjectHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для перекрывающего создателя объектов, позволяющий вернуть в мастер создания перечень созданных объектов
/// </summary>
public interface ICreatorMultiObjectHandler
{
  /// <summary>Перечень созданных объектов</summary>
  IEnumerable<Intermech.Interfaces.Client.ObjectCreatedInfo> ObjectCreatedInfo { get; }
}
