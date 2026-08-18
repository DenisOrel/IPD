// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.EnumerationSources.IntStringMapEnumerationSource
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;

#nullable disable
namespace GridViewExtensions.GridFilters.EnumerationSources;

public class IntStringMapEnumerationSource : IEnumerationSource
{
  private Hashtable _hash;
  private object[] _allValues;

  public IntStringMapEnumerationSource() => this._hash = new Hashtable();

  public IntStringMapEnumerationSource(int[] integerValues, string[] stringValues)
    : this()
  {
    if (integerValues.Length != stringValues.Length)
      throw new ArgumentException("Number of integers and strings must match.");
    for (int index = 0; index < integerValues.Length; ++index)
      this._hash.Add((object) stringValues[index], (object) integerValues[index]);
  }

  public void AddMapping(int integerValue, string stringValue)
  {
    this._hash.Add((object) stringValue, (object) integerValue);
    this._allValues = (object[]) null;
  }

  public void RemoveMapping(string stringValue)
  {
    this._hash.Remove((object) stringValue);
    this._allValues = (object[]) null;
  }

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
    int int32 = Convert.ToInt32(filter);
    foreach (string allValue in this.AllValues)
    {
      if ((int) this._hash[(object) allValue] == int32)
        return (object) allValue;
    }
    throw new ArgumentException("Unexpected filter.", nameof (filter));
  }
}
