// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ReadFontArray
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal class ReadFontArray
{
  private byte[] m_data;
  private int m_pointer;

  public ReadFontArray(byte[] data) => this.m_data = data;

  public ReadFontArray(byte[] data, int pointer)
  {
    this.m_data = data;
    this.m_pointer = pointer;
  }

  public float Get2Dot14() => (float) this.getnextshort() / 16384f;

  public float getFixed()
  {
    return (float) this.getnextshort() + (float) ((int) this.getnextUshort() / 65536 /*0x010000*/);
  }

  public long getLongDateTime()
  {
    byte[] numArray = new byte[8];
    for (int index = 7; index >= 0; --index)
    {
      if (this.Data.Length != 0 && this.Pointer < this.Data.Length)
      {
        numArray[index] = this.Data[this.Pointer];
        ++this.Pointer;
      }
    }
    return BitConverter.ToInt64(numArray, 0);
  }

  public byte getnextbyte()
  {
    int num = (int) this.Data[this.Pointer];
    ++this.Pointer;
    return (byte) num;
  }

  public short getnextshort()
  {
    byte[] numArray = new byte[2];
    for (int index = 1; index >= 0; --index)
    {
      if (this.Data.Length != 0 && this.Pointer < this.Data.Length)
      {
        numArray[index] = this.Data[this.Pointer];
        ++this.Pointer;
      }
    }
    return BitConverter.ToInt16(numArray, 0);
  }

  public int getnextUint16()
  {
    int num1 = 0;
    for (int index = 0; index < 2; ++index)
    {
      if (this.Data.Length != 0)
      {
        int num2 = (int) this.Data[this.Pointer] & (int) byte.MaxValue;
        num1 += num2 << 8 * (1 - index);
      }
      ++this.Pointer;
    }
    return num1;
  }

  public int getnextUint32()
  {
    int num1 = 0;
    for (int index = 0; index < 4; ++index)
    {
      int num2 = this.Pointer >= this.Data.Length ? 0 : (int) this.Data[this.Pointer] & (int) byte.MaxValue;
      num1 += num2 << 8 * (3 - index);
      ++this.Pointer;
    }
    return num1;
  }

  public string getnextUint32AsTag()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < 4; ++index)
    {
      char ch = (char) this.Data[this.Pointer];
      stringBuilder.Append(ch);
      ++this.Pointer;
    }
    return stringBuilder.ToString();
  }

  public int getnextUint64()
  {
    int num1 = 0;
    for (int index = 0; index < 8; ++index)
    {
      int num2 = (int) this.Data[this.Pointer];
      if (num2 < 0)
        num2 = 256 /*0x0100*/ + num2;
      num1 += num2 << 8 * (7 - index);
      ++this.Pointer;
    }
    return num1;
  }

  public ulong getnextULong()
  {
    byte[] numArray = new byte[4];
    for (int index = 3; index >= 0; --index)
    {
      if (this.Data.Length != 0 && this.Pointer < this.Data.Length)
      {
        numArray[index] = this.Data[this.Pointer];
        ++this.Pointer;
      }
    }
    return (ulong) BitConverter.ToUInt32(numArray, 0);
  }

  public ushort getnextUshort()
  {
    byte[] numArray = new byte[2];
    for (int index = 1; index >= 0; --index)
    {
      if (this.Data.Length != 0 && this.Pointer < this.Data.Length)
      {
        numArray[index] = this.Data[this.Pointer];
        ++this.Pointer;
      }
    }
    return BitConverter.ToUInt16(numArray, 0);
  }

  public uint getULong()
  {
    byte[] numArray = new byte[4];
    for (int index = 3; index >= 0; --index)
    {
      if (this.Data.Length != 0 && this.Pointer < this.Data.Length)
      {
        numArray[index] = this.Data[this.Pointer];
        ++this.Pointer;
      }
    }
    return BitConverter.ToUInt32(numArray, 0);
  }

  public sbyte Read()
  {
    int num = (int) (sbyte) this.Data[this.Pointer];
    ++this.Pointer;
    return (sbyte) num;
  }

  public sbyte ReadChar() => this.Read();

  public byte[] Data
  {
    get => this.m_data;
    set => this.m_data = value;
  }

  public int Pointer
  {
    get => this.m_pointer;
    set => this.m_pointer = value;
  }
}
