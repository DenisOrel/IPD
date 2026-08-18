// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.StringExtensions
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Extensions;

/// <summary>Extensions for strings</summary>
public static class StringExtensions
{
  /// <summary>Преобразовать строку вида #FA12CC в цвет</summary>
  /// <exception cref="T:System.FormatException">Если формат строки не позволяет конвертировать</exception>
  [NotEmpty]
  public static Color ConvertToColorFromHEX([NotNull] this string str)
  {
    Color result;
    if (str.TryConvertToColorFromHEX(out result))
      return result;
    throw new FormatException($"Строка \"{str}\" не кодирует цвет в формате HEX!");
  }

  /// <summary>Преобразовать строку вида #FA12CC в цвет</summary>
  [CanBeNull]
  [NotEmpty]
  public static Color? ConvertToColorOrNullFromHEX([CanBeNull] this string str)
  {
    Color result;
    return str.TryConvertToColorFromHEX(out result) ? new Color?(result) : new Color?();
  }

  /// <summary>Попытаться преобразовать строку вида #FA12CC в цвет</summary>
  public static bool TryConvertToColorFromHEX([CanBeNull] this string str, out Color result)
  {
    if (!string.IsNullOrWhiteSpace(str))
    {
      string str1 = str.TrimStart('#').TrimStart("0X", StringComparison.InvariantCultureIgnoreCase);
      switch (str1.Length)
      {
        case 6:
          int result1;
          int result2;
          int result3;
          if (int.TryParse(str1.Substring(0, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result1) && int.TryParse(str1.Substring(2, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result2) && int.TryParse(str1.Substring(4, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result3))
          {
            result = Color.FromArgb((int) byte.MaxValue, result1, result2, result3);
            return result != Color.Empty;
          }
          break;
        case 8:
          int result4;
          int result5;
          int result6;
          int result7;
          if (int.TryParse(str1.Substring(0, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result4) && int.TryParse(str1.Substring(2, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result5) && int.TryParse(str1.Substring(4, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result6) && int.TryParse(str1.Substring(4, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result7))
          {
            result = Color.FromArgb(result4, result5, result6, result7);
            return result != Color.Empty;
          }
          break;
      }
    }
    result = Color.Empty;
    return false;
  }
}
