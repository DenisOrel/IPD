// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.PossibleValuesConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Editors;

internal sealed class PossibleValuesConverter : ArrayConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destType)
  {
    return !(value is string[]) ? (object) "<пусто>" : (object) "<список>";
  }
}
