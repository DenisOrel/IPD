// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ColorExtensions
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.Drawing;

#nullable disable
namespace Intermech.Extensions;

/// <summary>Extensions for Color</summary>
public static class ColorExtensions
{
  [NotNull]
  private static readonly char[] _hexDigits = new char[16 /*0x10*/]
  {
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    'A',
    'B',
    'C',
    'D',
    'E',
    'F'
  };

  /// <summary>Преобразовать цвет в строку вида #FA12CC</summary>
  [NotNull]
  [NotWhitespace]
  public static string ToHexString(this Color color)
  {
    char[] chArray = new char[7];
    chArray[0] = '#';
    int r = (int) color.R;
    chArray[1] = ColorExtensions._hexDigits[r >> 4];
    chArray[2] = ColorExtensions._hexDigits[r & 15];
    int g = (int) color.G;
    chArray[3] = ColorExtensions._hexDigits[g >> 4];
    chArray[4] = ColorExtensions._hexDigits[g & 15];
    int b = (int) color.B;
    chArray[5] = ColorExtensions._hexDigits[b >> 4];
    chArray[6] = ColorExtensions._hexDigits[b & 15];
    return new string(chArray);
  }
}
