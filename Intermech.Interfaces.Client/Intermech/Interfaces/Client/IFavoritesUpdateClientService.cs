// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IFavoritesUpdateClientService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Служба для обновления дерева Избранного</summary>
public interface IFavoritesUpdateClientService
{
  /// <summary>Событие "Изменился список Избранного"</summary>
  event EventHandler OnFavoritesChanged;

  /// <summary>Событие "Добавлен новый тип объектов в Избранное"</summary>
  event EventHandler OnAddObjectTypeToFavorites;

  void Update();

  /// <summary>
  /// Принудительно обновить недавние объекты, разослав уведомление
  /// </summary>
  void Notify();
}
