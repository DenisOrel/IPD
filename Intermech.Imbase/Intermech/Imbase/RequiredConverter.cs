// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.RequiredConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class RequiredConverter : BaseTypeConverter
{
  internal RequiredConverter()
  {
    this._hash.Add((object) 0, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.True"));
    this._hash.Add((object) 2, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.False"));
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (this._values != null)
      return this._values;
    this._values = new TypeConverter.StandardValuesCollection((ICollection) new object[2]
    {
      (object) 0,
      (object) 2
    });
    return this._values;
  }
}
