
// Type: Intermech.Client.Core.SaveToDiskService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Client.Core;

/// <summary>Служба подписчиков окна команды "Сохранить на диск"</summary>
public class SaveToDiskService : ISaveToDiskService
{
  private Hashtable providers = Hashtable.Synchronized(new Hashtable());

  public void RegisterProvider(ISaveToDiskPageProvider provider)
  {
    this.providers[(object) provider] = (object) 0;
  }

  public void UnregisterProvider(ISaveToDiskPageProvider provider)
  {
    if (!this.providers.ContainsKey((object) provider))
      return;
    this.providers.Remove((object) provider);
  }

  public ISaveToDiskPageProvider[] Providers
  {
    get
    {
      List<ISaveToDiskPageProvider> diskPageProviderList = new List<ISaveToDiskPageProvider>();
      foreach (DictionaryEntry provider in this.providers)
        diskPageProviderList.Add((ISaveToDiskPageProvider) provider.Key);
      return diskPageProviderList.ToArray();
    }
  }
}
