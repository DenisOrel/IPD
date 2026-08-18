// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Persistence.PersistentState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Persistence;

/// <summary>
/// Реализует контейнер значений, предназначенный для хранения сериализованного представления объекта.
/// </summary>
public class PersistentState : IEnumerable<KeyValuePair<string, object>>, IEnumerable
{
  private Dictionary<string, object> items = new Dictionary<string, object>();
  private string fullTypeName = string.Empty;

  public void AddValue(string name, object value)
  {
    PersistentState.Validate(name);
    if (this.items.ContainsKey(name))
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_75"), (object) name));
    this.items.Add(name, value);
  }

  public object GetValue(string name)
  {
    PersistentState.Validate(name);
    return !this.items.ContainsKey(name) ? (object) null : this.items[name];
  }

  public bool Contains(string name)
  {
    PersistentState.Validate(name);
    return this.items.ContainsKey(name);
  }

  public string FullTypeName
  {
    get => this.fullTypeName;
    set => this.fullTypeName = value;
  }

  public int MemberCount => this.items.Count;

  private static void Validate(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name), LocalizationHolder.rm.GetString("Interfaces.Client_77"));
    if (name == string.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Client_78"), nameof (name));
  }

  public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<string, object>>) this.items.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();
}
