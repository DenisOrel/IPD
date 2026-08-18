// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.StringBuilderExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Extensions;

public static class StringBuilderExtensions
{
  [NotNull]
  public static StringBuilder AppendWithDelimiter(
    [NotNull] this StringBuilder stringBuilder,
    [CanBeNull, CanBeEmpty] string text,
    [NotNull, NotEmpty] string delimiter = ", ",
    int startLength = 0)
  {
    if (!string.IsNullOrWhiteSpace(text))
    {
      if (stringBuilder.Length > startLength)
        stringBuilder.Append(delimiter);
      stringBuilder.Append(text);
    }
    return stringBuilder;
  }
}
