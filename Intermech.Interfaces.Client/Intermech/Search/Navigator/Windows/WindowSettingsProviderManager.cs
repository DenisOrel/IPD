// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.WindowSettingsProviderManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>Менеджер провайдеров настроек окон навигатора</summary>
internal sealed class WindowSettingsProviderManager : IWindowSettingsProviderManager
{
  private Dictionary<int, IWindowSettingsProvider> _providerDictionaryByCategoryID = new Dictionary<int, IWindowSettingsProvider>();

  public IWindowSettingsProvider Get(int categoryID)
  {
    IWindowSettingsProvider settingsProvider = (IWindowSettingsProvider) null;
    this._providerDictionaryByCategoryID.TryGetValue(categoryID, out settingsProvider);
    return settingsProvider;
  }

  public void Register(int categoryID, IWindowSettingsProvider provider)
  {
    if (this._providerDictionaryByCategoryID.ContainsKey(categoryID))
      this._providerDictionaryByCategoryID[categoryID] = provider;
    else
      this._providerDictionaryByCategoryID.Add(categoryID, provider);
  }
}
