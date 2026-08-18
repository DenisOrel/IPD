// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.WindowSettingsCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>Коллекция настроек окон навигатора</summary>
[Serializable]
public class WindowSettingsCollection : IEnumerable<WindowSettingsBase>, IEnumerable
{
  private Dictionary<int, Dictionary<int, WindowSettingsBase>> _dictionary = new Dictionary<int, Dictionary<int, WindowSettingsBase>>();

  public void AddOrSet(int categoryID, int typeID, WindowSettingsBase settings)
  {
    Dictionary<int, WindowSettingsBase> dictionary = this.Get(categoryID);
    if (dictionary == null)
    {
      dictionary = new Dictionary<int, WindowSettingsBase>();
      this._dictionary.Add(categoryID, dictionary);
    }
    if (dictionary.ContainsKey(typeID))
      dictionary[typeID] = settings;
    else
      dictionary.Add(typeID, settings);
  }

  public void AddOrSet(int categoryID, Dictionary<int, WindowSettingsBase> settings)
  {
    if (this._dictionary.ContainsKey(categoryID))
      this._dictionary[categoryID] = settings;
    else
      this._dictionary.Add(categoryID, settings);
  }

  public Dictionary<int, WindowSettingsBase> Get(int categoryID)
  {
    Dictionary<int, WindowSettingsBase> dictionary = (Dictionary<int, WindowSettingsBase>) null;
    this._dictionary.TryGetValue(categoryID, out dictionary);
    return dictionary;
  }

  public WindowSettingsBase Get(int categoryID, int typeID)
  {
    Dictionary<int, WindowSettingsBase> dictionary = this.Get(categoryID);
    if (dictionary == null)
      return (WindowSettingsBase) null;
    WindowSettingsBase windowSettingsBase = (WindowSettingsBase) null;
    dictionary.TryGetValue(typeID, out windowSettingsBase);
    return windowSettingsBase;
  }

  public IEnumerator<WindowSettingsBase> GetEnumerator()
  {
    foreach (Dictionary<int, WindowSettingsBase> dictionary in this._dictionary.Values)
    {
      foreach (WindowSettingsBase windowSettingsBase in dictionary.Values)
        yield return windowSettingsBase;
    }
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
