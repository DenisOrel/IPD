// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.SpecialValue
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace GridViewExtensions.GridFilters;

internal class SpecialValue
{
  public static readonly SpecialValue NoValue = new SpecialValue(SpecialValueType.None);
  public static readonly SpecialValue NullValue = new SpecialValue(SpecialValueType.Null);
  public static readonly SpecialValue NotNullValue = new SpecialValue(SpecialValueType.NotNull);
  internal readonly SpecialValueType _type;
  internal readonly string _str;

  internal static bool IsSpaces(string value)
  {
    if (string.IsNullOrEmpty(value))
      return false;
    for (int index = 0; index < value.Length; ++index)
    {
      if (value[index] != ' ')
        return false;
    }
    return true;
  }

  private SpecialValue(SpecialValueType type) => this._type = type;

  internal SpecialValue(string value)
  {
    this._type = SpecialValueType.Spaces;
    this._str = value;
  }

  public override string ToString()
  {
    switch (this._type)
    {
      case SpecialValueType.None:
        return "(*)";
      case SpecialValueType.Null:
        return "(пусто)";
      case SpecialValueType.NotNull:
        return "(не пусто)";
      case SpecialValueType.Spaces:
        return $"'{this._str}'";
      default:
        return base.ToString();
    }
  }
}
