
// Type: Intermech.Client.Core.DwgColor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace Intermech.Client.Core;

/// <summary> Описание класса ColorDwg. </summary>
[DebuggerDisplay("{_color,h}")]
public struct DwgColor : IComparable<DwgColor>, IComparable, IEquatable<DwgColor>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private const uint wEmpty = 3238002688 /*0xC1000000*/;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private uint _color;
  /// <summary>цвета подложки ACAD</summary>
  public static readonly DwgColor Empty = new DwgColor(3238002688U /*0xC1000000*/);

  public DwgColor(byte color)
  {
    this._color = (uint) color;
    this.AcadIndex = (uint) color;
  }

  public DwgColor(uint color)
  {
    this._color = color;
    this.Rgb = color;
  }

  [DebuggerDisplay("{UInt,h}")]
  public uint UInt
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._color;
  }

  public bool IsRgb
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ((int) (this._color >> 24) & (int) byte.MaxValue) != 194;
    }
  }

  /// <summary>есть код цвета подложки ACAD</summary>
  public bool IsEmpty
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._color == 3238002688U /*0xC1000000*/;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareTo(object other)
  {
    if (other is DwgColor other1)
      return this.CompareTo(other1);
    throw new ArgumentException("object is not a DwgColor");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareTo(DwgColor other)
  {
    return (int) this.UInt == (int) other.UInt ? 0 : this.UInt.CompareTo(other.UInt);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(DwgColor other) => this.CompareTo(other) == 0;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => (int) this.UInt;

  public Color GdiColor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) this.Rgb & 16777215 /*0xFFFFFF*/));
    }
  }

  [DebuggerDisplay("{Rgb,h}")]
  public uint Rgb
  {
    get
    {
      if (this.IsEmpty)
        return 0;
      return this.IsRgb ? this._color & 16777215U /*0xFFFFFF*/ : this.ToRgb((byte) (this._color & (uint) byte.MaxValue)) & 16777215U /*0xFFFFFF*/;
    }
    set
    {
      if (value == 0U)
      {
        this._color = 3238002688U /*0xC1000000*/;
      }
      else
      {
        uint acadIndex = this.ToAcadIndex(this._color = (uint) (((int) value & 16777215 /*0xFFFFFF*/) - 1040187392 /*0x3E000000*/));
        if (((int) this.ToRgb((byte) acadIndex) & 16777215 /*0xFFFFFF*/) != ((int) value & 16777215 /*0xFFFFFF*/))
          return;
        this._color = acadIndex;
      }
    }
  }

  [DebuggerDisplay("{AcadIndex,d}")]
  public uint AcadIndex
  {
    get
    {
      if (this.IsEmpty)
        return 0;
      return this.IsRgb ? this.ToAcadIndex(this._color) & (uint) byte.MaxValue : this._color & (uint) byte.MaxValue;
    }
    set
    {
      this._color = value != 0U ? 3254779904U /*0xC2000000*/ + value : 3238002688U /*0xC1000000*/;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public uint ToAcadIndex(uint rgb)
  {
    byte num1 = (byte) (rgb >> 16 /*0x10*/);
    byte num2 = (byte) (rgb >> 8);
    byte num3 = (byte) rgb;
    int num4 = 12288 /*0x3000*/;
    int num5 = (int) num1 != (int) num2 || (int) num1 != (int) num3 ? 1 : 250;
    for (int acadIndex = num5; acadIndex <= (int) byte.MaxValue; ++acadIndex)
    {
      int rgb1 = (int) this.ToRgb((byte) acadIndex);
      int num6 = (int) (byte) (rgb1 >>> 16 /*0x10*/) - (int) num1;
      int num7 = (int) (byte) (rgb1 >>> 8) - (int) num2;
      int num8 = (int) (byte) rgb1 - (int) num3;
      int num9 = num6 * num6 + num7 * num7 + num8 * num8;
      if (num9 < num4)
      {
        num5 = acadIndex;
        if ((num4 = num9) == 0)
          break;
      }
    }
    if (num5 == (int) byte.MaxValue)
      num5 = 7;
    return 3271557120U /*0xC3000000*/ + (uint) (byte) num5;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public uint ToRgb(byte acadIndex)
  {
    switch (acadIndex)
    {
      case 0:
        return 3238002688 /*0xC1000000*/;
      case 1:
        return 3271491584;
      case 2:
        return 3271556864;
      case 3:
        return 3254845184;
      case 4:
        return 3254845439;
      case 5:
        return 3254780159;
      case 6:
        return 3271491839;
      case 7:
        return 3254779904 /*0xC2000000*/;
      case 8:
        return 3263201408;
      case 9:
        return 3267412160;
      default:
        if (acadIndex >= (byte) 250)
        {
          uint num = (uint) ((0.33 + (double) ((int) acadIndex - 250) * 0.134) * (double) byte.MaxValue);
          return (uint) (49664 + ((int) num << 16 /*0x10*/) + ((int) num << 8)) + num;
        }
        int num1 = ((int) acadIndex - 10) / 10;
        if (num1 >= 24)
          num1 -= 24;
        int num2;
        double num3 = (double) (num2 = (int) ((double) num1 / 4.0)) - (double) num2;
        int num4 = (int) acadIndex % 10;
        double num5 = (num4 & 1) == 1 ? 0.5 : 1.0;
        double num6 = 0.0;
        double num7 = 0.0;
        double num8 = 0.0;
        switch (num2)
        {
          case 0:
            num6 = 1.0;
            num7 = 1.0 - num5 * (1.0 - num3);
            num8 = 1.0 - num5;
            break;
          case 1:
            num6 = 1.0 - num5 * num3;
            num7 = 1.0;
            num8 = 1.0 - num5;
            break;
          case 2:
            num6 = 1.0 - num5;
            num7 = 1.0;
            num8 = 1.0 - num5 * (1.0 - num3);
            break;
          case 3:
            num6 = 1.0 - num5;
            num7 = 1.0 - num5 * num3;
            num8 = 1.0;
            break;
          case 4:
            num6 = 1.0 - num5 * (1.0 - num3);
            num7 = 1.0 - num5;
            num8 = 1.0;
            break;
          case 5:
            num6 = 1.0;
            num7 = 1.0 - num5;
            num8 = 1.0 - num5 * num3;
            break;
        }
        double num9 = new double[5]
        {
          1.0,
          0.65,
          0.5,
          0.3,
          0.15
        }[num4 >> 1];
        return (uint) (((int) (uint) (num6 * num9 * (double) byte.MaxValue) << 16 /*0x10*/) - 1040187392 /*0x3E000000*/ + ((int) (uint) (num7 * num9 * (double) byte.MaxValue) << 8)) + (uint) (num8 * num9 * (double) byte.MaxValue);
    }
  }
}
