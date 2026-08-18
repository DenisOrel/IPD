// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.ConfigurationCollection
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Editor;

internal class ConfigurationCollection : IConfigurationCollection, ICollection, IEnumerable
{
  private List<IConfiguration> _configurations;

  public ConfigurationCollection() => this._configurations = new List<IConfiguration>();

  public void Add(IConfiguration configuration) => this._configurations.Add(configuration);

  public void Clear() => this._configurations.Clear();

  public void CopyTo(Array array, int index)
  {
    for (int index1 = index; index1 < this._configurations.Count; ++index1)
      array.SetValue((object) this._configurations[index1], index - index1);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this._configurations.GetEnumerator();

  public void Remove(IConfiguration configuration) => this._configurations.Remove(configuration);

  public void RemoveAt(int index) => this._configurations.RemoveAt(index);

  public int Count => this._configurations.Count;

  public bool IsSynchronized => false;

  public IConfiguration this[int index] => this._configurations[index];

  public IConfiguration this[string name]
  {
    get
    {
      for (int index = 0; index < this._configurations.Count; ++index)
      {
        if (this._configurations[index].Name == name)
          return this._configurations[index];
      }
      return (IConfiguration) null;
    }
  }

  public object SyncRoot => (object) null;
}
