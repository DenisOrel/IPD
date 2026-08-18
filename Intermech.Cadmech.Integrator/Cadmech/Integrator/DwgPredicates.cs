// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgPredicates
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class DwgPredicates
{
  public static bool StampIsEmptyOrBadScanned(ValueBag stampTable)
  {
    if (stampTable == null)
      throw new ArgumentNullException(nameof (stampTable));
    string str1 = stampTable.Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
    string str2 = stampTable.Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
    return string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2);
  }

  public static ICollection<StringKey> ParametersForStampIsEmptyOrBadScanned()
  {
    return (ICollection<StringKey>) new StringKey[2]
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.Name.Text
    };
  }

  public static bool StampIsValid(ValueBag stampTable)
  {
    if (stampTable == null)
      throw new ArgumentNullException(nameof (stampTable));
    return !DwgPredicates.StampIsEmptyOrBadScanned(stampTable);
  }

  public static ICollection<StringKey> ParametersForStampIsValid()
  {
    return DwgPredicates.ParametersForStampIsEmptyOrBadScanned();
  }
}
