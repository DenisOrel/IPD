// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.ExtendedBitConverter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System;

#nullable disable
namespace Intermech.Hashes.Utils;

internal static class ExtendedBitConverter
{
  public static char GetHexValue(int i) => i < 10 ? (char) (i + 48 /*0x30*/) : (char) (i - 10 + 65);

  public static unsafe string ToString(IntPtr value, int StartIndex, int Length, char delimeter = '-')
  {
    int length = Length * 3;
    char[] chArray = new char[length];
    int index = 0;
    int num1 = StartIndex;
    for (; index < length; index += 3)
    {
      byte num2 = *(byte*) ((IntPtr) (void*) value + num1);
      ++num1;
      chArray[index] = ExtendedBitConverter.GetHexValue((int) num2 >> 4);
      chArray[index + 1] = ExtendedBitConverter.GetHexValue((int) num2 & 15);
      chArray[index + 2] = delimeter;
    }
    return new string(chArray, 0, length - 1);
  }
}
