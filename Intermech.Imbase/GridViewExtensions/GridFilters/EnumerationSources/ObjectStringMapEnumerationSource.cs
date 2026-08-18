// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.EnumerationSources.ObjectStringMapEnumerationSource
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;

#nullable disable
namespace GridViewExtensions.GridFilters.EnumerationSources;

public class ObjectStringMapEnumerationSource : IEnumerationSource
{
  private Hashtable _hash;
  private object[] _allValues;

  public ObjectStringMapEnumerationSource() => this._hash = new Hashtable();

  public ObjectStringMapEnumerationSource(object[] values, string[] names)
    : this()
  {
    if (values.Length != names.Length)
      throw new ArgumentException("Number of values and strings must match.");
    for (int index = 0; index < values.Length; ++index)
      this._hash.Add((object) names[index], values[index]);
  }

  public void AddMapping(object value, string name)
  {
    this._hash[(object) name] = value;
    this._allValues = (object[]) null;
  }

  public void RemoveMapping(string name)
  {
    this._hash.Remove((object) name);
    this._allValues = (object[]) null;
  }

  public void Clear() => this._hash.Clear();

  public object[] AllValues
  {
    get
    {
      if (this._allValues == null)
      {
        ICollection keys = this._hash.Keys;
        this._allValues = new object[keys.Count];
        keys.CopyTo((Array) this._allValues, 0);
      }
      return this._allValues;
    }
  }

  public string GetFilterFromValue(object value) => this._hash[value].ToString();

  public object GetValueFromFilter(string filter)
  {
    foreach (string allValue in this.AllValues)
    {
      object obj = this._hash[(object) allValue];
      if (obj != null && obj.ToString().Equals(filter))
        return (object) allValue;
    }
    throw new ArgumentException("Unexpected filter.", nameof (filter));
  }
}
