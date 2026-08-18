// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEventArgsItemsCount
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс позволяет службе уведомлений уточнять количество заданий в аргументах
/// (например, количество созданных/изменённых/удалёных объектов, т.п.).
/// Используется для оптимизации обработки уведомлений
/// </summary>
public interface IEventArgsItemsCount
{
  /// <summary>Количество заданий в аргументах</summary>
  int ItemsCount { get; }
}
