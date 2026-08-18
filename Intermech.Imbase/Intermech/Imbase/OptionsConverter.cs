// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.OptionsConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

#nullable disable
namespace Intermech.Imbase;

internal class OptionsConverter : BaseTypeConverter
{
  private const string FLAG_CADMECH = "Cadmech";
  private const string FLAG_CADMECHT = "CadmechT";
  private const string FLAG_AVS = "AVS";
  private const string FLAG_SEARCH = "Search";
  private const string FLAG_CADPROPERTY = "CADProperty";

  internal OptionsConverter()
  {
    this._hash.Add((object) AttributeOptions.ImbaseFlag_CADMECH, (object) "Cadmech");
    this._hash.Add((object) AttributeOptions.ImbaseFlag_CADMECH_T, (object) "CadmechT");
    this._hash.Add((object) AttributeOptions.ImbaseFlag_AVS, (object) "AVS");
    this._hash.Add((object) AttributeOptions.ImbaseFlag_SEARCH, (object) "Search");
    this._hash.Add((object) AttributeOptions.ImbaseFlag_CADPROPERTY, (object) "CADProperty");
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return !(value is string) ? base.ConvertFrom(context, culture, value) : value;
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (!(value is int))
      return base.ConvertTo(context, culture, value, destinationType);
    int int32 = Convert.ToInt32(value);
    if (int32 == 0)
      return (object) stringBuilder.ToString();
    stringBuilder.Append((int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH)) == int32 ? $"{"Cadmech"}; " : string.Empty);
    stringBuilder.Append((int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH_T)) == int32 ? $"{"CadmechT"}; " : string.Empty);
    stringBuilder.Append((int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_AVS)) == int32 ? $"{"AVS"}; " : string.Empty);
    stringBuilder.Append((int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_SEARCH)) == int32 ? $"{"Search"}; " : string.Empty);
    stringBuilder.Append((int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADPROPERTY)) == int32 ? $"{"CADProperty"}; " : string.Empty);
    return (object) stringBuilder.ToString();
  }
}
