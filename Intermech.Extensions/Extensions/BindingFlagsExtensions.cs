// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.BindingFlagsExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Reflection;

#nullable disable
namespace Intermech.Extensions;

public static class BindingFlagsExtensions
{
  [ContractAnnotation("=> true, errorText: null; => false, errorText: notnull")]
  public static bool TryValidateForUse(this BindingFlags flags, out string errorText)
  {
    if ((flags & BindingFlags.Static) == BindingFlags.Default && (flags & BindingFlags.Instance) == BindingFlags.Default)
    {
      errorText = "Ether Static or Instance must be in BindingFlags!";
      return false;
    }
    if ((flags & BindingFlags.Public) != BindingFlags.Default && (flags & BindingFlags.NonPublic) == BindingFlags.Default)
    {
      errorText = "Ether Static or Instance must be in BindingFlags!";
      return false;
    }
    errorText = (string) null;
    return true;
  }
}
