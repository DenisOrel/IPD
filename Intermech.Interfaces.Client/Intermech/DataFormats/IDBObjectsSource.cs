// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBObjectsSource
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>Интерфейс источника информации об выбранных версиях объектов</summary>
public interface IDBObjectsSource
{
  /// <summary>Список идентификаторов версий объектов</summary>
  [NotNull]
  IReadOnlyList<long> ObjectVersionIDs { get; }

  /// <summary>Событие, информирующее о том, что список идентификаторов выбранных версий объектов изменился,
  /// например пользователем были выбраны другие версии объектов</summary>
  event SelectionChangedEventHandler Changed;
}
