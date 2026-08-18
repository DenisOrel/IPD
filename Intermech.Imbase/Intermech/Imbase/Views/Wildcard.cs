// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.Wildcard
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Imbase.Views;

public class Wildcard : Regex
{
  public Wildcard(string pattern)
    : base(Wildcard.WildcardToRegex(pattern))
  {
  }

  public Wildcard(string pattern, RegexOptions options)
    : base(Wildcard.WildcardToRegex(pattern), options)
  {
  }

  public static string WildcardToRegex(string pattern)
  {
    return $"^{Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".")}$";
  }
}
