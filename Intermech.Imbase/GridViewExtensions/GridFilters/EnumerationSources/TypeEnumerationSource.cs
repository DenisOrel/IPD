// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.EnumerationSources.TypeEnumerationSource
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace GridViewExtensions.GridFilters.EnumerationSources;

public class TypeEnumerationSource : IEnumerationSource
{
  private Type _enumType;
  private object[] _allValues;

  public TypeEnumerationSource(Type dataType)
  {
    this._enumType = dataType.IsEnum ? dataType : throw new ArgumentException("Only enumeration types are valid arguments.");
  }

  public object[] AllValues
  {
    get
    {
      if (this._allValues == null)
      {
        Array values = Enum.GetValues(this._enumType);
        this._allValues = new object[values.Length];
        values.CopyTo((Array) this._allValues, 0);
      }
      return this._allValues;
    }
  }

  public string GetFilterFromValue(object value) => Convert.ToInt32(value).ToString();

  public object GetValueFromFilter(string filter)
  {
    return Enum.ToObject(this._enumType, Convert.ToInt32(filter));
  }
}
