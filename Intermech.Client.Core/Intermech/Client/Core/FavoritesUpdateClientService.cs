
// Type: Intermech.Client.Core.FavoritesUpdateClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client.Core;

/// <summary>Служба для обновления дерева Избранного</summary>
public class FavoritesUpdateClientService : IFavoritesUpdateClientService
{
  public event EventHandler OnFavoritesChanged;

  public event EventHandler OnAddObjectTypeToFavorites;

  public void Update() => throw new NotImplementedException();

  public void Notify() => throw new NotImplementedException();
}
