// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.HexEncodedValuesConverter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Text;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class HexEncodedValuesConverter
{
  public bool IsHexEncodedValue(string value)
  {
    if (value.Length <= 2)
      return false;
    int index1 = value.Length - 1;
    if (index1 % 2 == 0 || value[0] != '<' || value[index1] != '>')
      return false;
    for (int index2 = 1; index2 < index1; ++index2)
    {
      if (!this.IsHexDigit(value[index2]))
        return false;
    }
    return true;
  }

  public bool TryConvertToString(string value, Encoding encoding, out string result)
  {
    byte[] bytes = new byte[(value.Length - 2) / 2];
    for (int index1 = 0; index1 < bytes.Length; ++index1)
    {
      int index2 = 2 * index1 + 1;
      int number1 = this.HexDigitToNumber(value[index2]);
      int number2 = this.HexDigitToNumber(value[index2 + 1]);
      bytes[index1] = (byte) (number1 * 16 /*0x10*/ + number2);
    }
    try
    {
      result = encoding.GetString(bytes);
      return true;
    }
    catch
    {
      result = (string) null;
      return false;
    }
  }

  private bool IsHexDigit(char digit)
  {
    return digit >= '0' && digit <= '9' || digit >= 'A' && digit <= 'F' || digit >= 'a' && digit <= 'f';
  }

  private int HexDigitToNumber(char digit)
  {
    if (digit >= '0' && digit <= '9')
      return (int) digit - 48 /*0x30*/;
    if (digit >= 'A' && digit <= 'F')
      return 10 + ((int) digit - 65);
    if (digit >= 'a' && digit <= 'f')
      return 10 + ((int) digit - 97);
    throw new Exception($"Invalid hex digit '{digit}'");
  }
}
