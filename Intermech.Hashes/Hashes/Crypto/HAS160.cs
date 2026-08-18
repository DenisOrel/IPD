// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.HAS160
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class HAS160 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private uint[] hash;
  private static readonly int[] rot = new int[20]
  {
    5,
    11,
    7,
    15,
    6,
    13,
    8,
    14,
    7,
    12,
    9,
    11,
    8,
    15,
    6,
    12,
    9,
    14,
    5,
    13
  };
  private static readonly int[] tor = new int[20]
  {
    27,
    21,
    25,
    17,
    26,
    19,
    24,
    18,
    25,
    20,
    23,
    21,
    24,
    17,
    26,
    20,
    23,
    18,
    27,
    19
  };
  private static readonly int[] index = new int[80 /*0x50*/]
  {
    18,
    0,
    1,
    2,
    3,
    19,
    4,
    5,
    6,
    7,
    16 /*0x10*/,
    8,
    9,
    10,
    11,
    17,
    12,
    13,
    14,
    15,
    18,
    3,
    6,
    9,
    12,
    19,
    15,
    2,
    5,
    8,
    16 /*0x10*/,
    11,
    14,
    1,
    4,
    17,
    7,
    10,
    13,
    0,
    18,
    12,
    5,
    14,
    7,
    19,
    0,
    9,
    2,
    11,
    16 /*0x10*/,
    4,
    13,
    6,
    15,
    17,
    8,
    1,
    10,
    3,
    18,
    7,
    2,
    13,
    8,
    19,
    3,
    14,
    9,
    4,
    16 /*0x10*/,
    15,
    10,
    5,
    0,
    17,
    11,
    6,
    1,
    12
  };

  public HAS160()
    : base(20, 64 /*0x40*/)
  {
    this.hash = new uint[5];
  }

  public override IHash Clone()
  {
    HAS160 haS160 = new HAS160();
    haS160.buffer = this.buffer.Clone();
    haS160.processed_bytes = this.processed_bytes;
    haS160.hash = this.hash.DeepCopy();
    haS160.BufferSize = this.BufferSize;
    return (IHash) haS160;
  }

  public override void Initialize()
  {
    this.hash[0] = 1732584193U;
    this.hash[1] = 4023233417U;
    this.hash[2] = 2562383102U;
    this.hash[3] = 271733878U;
    this.hash[4] = 3285377520U;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[20];
    fixed (uint* src = this.hash)
      fixed (byte* dest = result)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override void Finish()
  {
    long x = (long) this.processed_bytes * 8L;
    int a_index = this.buffer.Position < 56 ? 56 - this.buffer.Position : 120 - this.buffer.Position;
    byte[] a_out = new byte[a_index + 8];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.le2me_64((ulong) x), ref a_out, a_index);
    int a_length = a_index + 8;
    this.TransformBytes(a_out, 0, a_length);
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] numArray = new uint[20];
    fixed (uint* dest = numArray)
    {
      uint num1 = this.hash[0];
      uint num2 = this.hash[1];
      uint num3 = this.hash[2];
      uint num4 = this.hash[3];
      uint num5 = this.hash[4];
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
      numArray[16 /*0x10*/] = numArray[0] ^ numArray[1] ^ numArray[2] ^ numArray[3];
      numArray[17] = numArray[4] ^ numArray[5] ^ numArray[6] ^ numArray[7];
      numArray[18] = numArray[8] ^ numArray[9] ^ numArray[10] ^ numArray[11];
      numArray[19] = numArray[12] ^ numArray[13] ^ numArray[14] ^ numArray[15];
      for (uint index = 0; index < 20U; ++index)
      {
        int num6 = (int) numArray[HAS160.index[(int) index]] + ((int) num1 << HAS160.rot[(int) index] | (int) (num1 >> HAS160.tor[(int) index])) + ((int) num2 & (int) num3 | ~(int) num2 & (int) num4) + (int) num5;
        num5 = num4;
        num4 = num3;
        num3 = num2 << 10 | num2 >> 22;
        num2 = num1;
        num1 = (uint) num6;
      }
      numArray[16 /*0x10*/] = numArray[3] ^ numArray[6] ^ numArray[9] ^ numArray[12];
      numArray[17] = numArray[2] ^ numArray[5] ^ numArray[8] ^ numArray[15];
      numArray[18] = numArray[1] ^ numArray[4] ^ numArray[11] ^ numArray[14];
      numArray[19] = numArray[0] ^ numArray[7] ^ numArray[10] ^ numArray[13];
      for (uint index = 20; index < 40U; ++index)
      {
        int num7 = (int) numArray[HAS160.index[(int) index]] + 1518500249 + ((int) num1 << HAS160.rot[(int) index - 20] | (int) (num1 >> HAS160.tor[(int) index - 20])) + ((int) num2 ^ (int) num3 ^ (int) num4) + (int) num5;
        num5 = num4;
        num4 = num3;
        num3 = num2 << 17 | num2 >> 15;
        num2 = num1;
        num1 = (uint) num7;
      }
      numArray[16 /*0x10*/] = numArray[5] ^ numArray[7] ^ numArray[12] ^ numArray[14];
      numArray[17] = numArray[0] ^ numArray[2] ^ numArray[9] ^ numArray[11];
      numArray[18] = numArray[4] ^ numArray[6] ^ numArray[13] ^ numArray[15];
      numArray[19] = numArray[1] ^ numArray[3] ^ numArray[8] ^ numArray[10];
      for (uint index = 40; index < 60U; ++index)
      {
        int num8 = (int) numArray[HAS160.index[(int) index]] + 1859775393 + ((int) num1 << HAS160.rot[(int) index - 40] | (int) (num1 >> HAS160.tor[(int) index - 40])) + ((int) num3 ^ ((int) num2 | ~(int) num4)) + (int) num5;
        num5 = num4;
        num4 = num3;
        num3 = num2 << 25 | num2 >> 7;
        num2 = num1;
        num1 = (uint) num8;
      }
      numArray[16 /*0x10*/] = numArray[2] ^ numArray[7] ^ numArray[8] ^ numArray[13];
      numArray[17] = numArray[3] ^ numArray[4] ^ numArray[9] ^ numArray[14];
      numArray[18] = numArray[0] ^ numArray[5] ^ numArray[10] ^ numArray[15];
      numArray[19] = numArray[1] ^ numArray[6] ^ numArray[11] ^ numArray[12];
      for (uint index = 60; index < 80U /*0x50*/; ++index)
      {
        int num9 = (int) numArray[HAS160.index[(int) index]] - 1894007588 + ((int) num1 << HAS160.rot[(int) index - 60] | (int) (num1 >> HAS160.tor[(int) index - 60])) + ((int) num2 ^ (int) num3 ^ (int) num4) + (int) num5;
        num5 = num4;
        num4 = num3;
        num3 = num2 << 30 | num2 >> 2;
        num2 = num1;
        num1 = (uint) num9;
      }
      this.hash[0] = this.hash[0] + num1;
      this.hash[1] = this.hash[1] + num2;
      this.hash[2] = this.hash[2] + num3;
      this.hash[3] = this.hash[3] + num4;
      this.hash[4] = this.hash[4] + num5;
      Intermech.Hashes.Utils.Utils.Memset((IntPtr) (void*) dest, (byte) 0, numArray.Length * 4);
    }
  }
}
