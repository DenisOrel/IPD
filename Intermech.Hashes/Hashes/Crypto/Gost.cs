// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Gost
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Gost : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private uint[] state;
  private uint[] hash;
  private static uint[] sbox1 = new uint[256 /*0x0100*/];
  private static uint[] sbox2 = new uint[256 /*0x0100*/];
  private static uint[] sbox3 = new uint[256 /*0x0100*/];
  private static uint[] sbox4 = new uint[256 /*0x0100*/];

  static Gost()
  {
    uint[][] numArray = new uint[8][]
    {
      new uint[16 /*0x10*/]
      {
        4U,
        10U,
        9U,
        2U,
        13U,
        8U,
        0U,
        14U,
        6U,
        11U,
        1U,
        12U,
        7U,
        15U,
        5U,
        3U
      },
      new uint[16 /*0x10*/]
      {
        14U,
        11U,
        4U,
        12U,
        6U,
        13U,
        15U,
        10U,
        2U,
        3U,
        8U,
        1U,
        0U,
        7U,
        5U,
        9U
      },
      new uint[16 /*0x10*/]
      {
        5U,
        8U,
        1U,
        13U,
        10U,
        3U,
        4U,
        2U,
        14U,
        15U,
        12U,
        7U,
        6U,
        0U,
        9U,
        11U
      },
      new uint[16 /*0x10*/]
      {
        7U,
        13U,
        10U,
        1U,
        0U,
        8U,
        9U,
        15U,
        14U,
        4U,
        6U,
        12U,
        11U,
        2U,
        5U,
        3U
      },
      new uint[16 /*0x10*/]
      {
        6U,
        12U,
        7U,
        1U,
        5U,
        15U,
        13U,
        8U,
        4U,
        10U,
        9U,
        14U,
        0U,
        3U,
        11U,
        2U
      },
      new uint[16 /*0x10*/]
      {
        4U,
        11U,
        10U,
        0U,
        7U,
        2U,
        1U,
        13U,
        3U,
        6U,
        8U,
        5U,
        9U,
        12U,
        15U,
        14U
      },
      new uint[16 /*0x10*/]
      {
        13U,
        11U,
        4U,
        1U,
        3U,
        15U,
        5U,
        9U,
        0U,
        10U,
        14U,
        7U,
        6U,
        8U,
        2U,
        12U
      },
      new uint[16 /*0x10*/]
      {
        1U,
        15U,
        13U,
        0U,
        5U,
        7U,
        10U,
        4U,
        9U,
        2U,
        3U,
        14U,
        6U,
        11U,
        8U,
        12U
      }
    };
    int index1 = 0;
    for (int index2 = 0; index2 < 16 /*0x10*/; ++index2)
    {
      uint num1 = numArray[1][index2] << 15;
      uint num2 = numArray[3][index2] << 23;
      uint num3 = Bits.RotateRight32(numArray[5][index2], 1);
      uint num4 = numArray[7][index2] << 7;
      for (int index3 = 0; index3 < 16 /*0x10*/; ++index3)
      {
        Gost.sbox1[index1] = num1 | numArray[0][index3] << 11;
        Gost.sbox2[index1] = num2 | numArray[2][index3] << 19;
        Gost.sbox3[index1] = num3 | numArray[4][index3] << 27;
        Gost.sbox4[index1] = num4 | numArray[6][index3] << 3;
        ++index1;
      }
    }
  }

  public Gost()
    : base(32 /*0x20*/, 32 /*0x20*/)
  {
    this.state = new uint[8];
    this.hash = new uint[8];
  }

  public override IHash Clone()
  {
    Gost gost = new Gost();
    gost.buffer = this.buffer.Clone();
    gost.processed_bytes = this.processed_bytes;
    gost.state = this.state.DeepCopy();
    gost.hash = this.hash.DeepCopy();
    gost.BufferSize = this.BufferSize;
    return (IHash) gost;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.state);
    ArrayUtils.ZeroFill(ref this.hash);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[32 /*0x20*/];
    fixed (uint* src = this.hash)
      fixed (byte* dest = result)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override void Finish()
  {
    ulong num = this.processed_bytes * 8UL;
    if (this.buffer.Position > 0)
      this.TransformBytes(new byte[32 /*0x20*/ - this.buffer.Position], 0, 32 /*0x20*/ - this.buffer.Position);
    uint[] a_m = new uint[8];
    a_m[0] = (uint) num;
    a_m[1] = (uint) (num >> 32 /*0x20*/);
    this.Compress(a_m);
    this.Compress(this.state);
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] buffer1 = new uint[8];
    uint[] buffer2 = new uint[8];
    fixed (uint* dest = buffer2)
    {
      uint num1 = 0;
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 32 /*0x20*/);
      for (int index = 0; index < 8; ++index)
      {
        uint num2 = buffer2[index];
        buffer1[index] = num2;
        uint num3 = this.state[index];
        uint num4 = num2 + num1 + this.state[index];
        this.state[index] = num4;
        num1 = num4 < num2 || num4 < num3 ? 1U : 0U;
      }
      this.Compress(buffer1);
      ArrayUtils.ZeroFill(ref buffer2);
      ArrayUtils.ZeroFill(ref buffer1);
    }
  }

  private void Compress(uint[] a_m)
  {
    uint[] numArray = new uint[8];
    uint num1 = this.hash[0];
    uint num2 = this.hash[1];
    uint num3 = this.hash[2];
    uint num4 = this.hash[3];
    uint num5 = this.hash[4];
    uint num6 = this.hash[5];
    uint num7 = this.hash[6];
    uint num8 = this.hash[7];
    uint num9 = a_m[0];
    uint num10 = a_m[1];
    uint num11 = a_m[2];
    uint num12 = a_m[3];
    uint num13 = a_m[4];
    uint num14 = a_m[5];
    uint num15 = a_m[6];
    uint num16 = a_m[7];
    for (int index1 = 0; index1 < 8; index1 += 2)
    {
      int num17 = (int) num1 ^ (int) num9;
      uint num18 = num2 ^ num10;
      uint num19 = num3 ^ num11;
      uint num20 = num4 ^ num12;
      uint num21 = num5 ^ num13;
      uint num22 = num6 ^ num14;
      uint num23 = num7 ^ num15;
      uint num24 = num8 ^ num16;
      uint num25 = (uint) ((int) (byte) num17 | (int) (byte) num19 << 8 | (int) (byte) num21 << 16 /*0x10*/ | (int) (byte) num23 << 24);
      uint num26 = (uint) ((int) (byte) (num17 >>> 8) | (int) num19 & 65280 | ((int) num21 & 65280) << 8 | ((int) num23 & 65280) << 16 /*0x10*/);
      uint num27 = (uint) ((int) (byte) (num17 >>> 16 /*0x10*/) | (int) ((num19 & 16711680U /*0xFF0000*/) >> 8) | (int) num21 & 16711680 /*0xFF0000*/ | ((int) num23 & 16711680 /*0xFF0000*/) << 8);
      int num28 = num17 >>> 24 | (int) ((num19 & 4278190080U /*0xFF000000*/) >> 16 /*0x10*/) | (int) ((num21 & 4278190080U /*0xFF000000*/) >> 8) | (int) num23 & -16777216 /*0xFF000000*/;
      uint num29 = (uint) ((int) (byte) num18 | ((int) num20 & (int) byte.MaxValue) << 8 | ((int) num22 & (int) byte.MaxValue) << 16 /*0x10*/ | ((int) num24 & (int) byte.MaxValue) << 24);
      uint num30 = (uint) ((int) (byte) (num18 >> 8) | (int) num20 & 65280 | ((int) num22 & 65280) << 8 | ((int) num24 & 65280) << 16 /*0x10*/);
      uint num31 = (uint) ((int) (byte) (num18 >> 16 /*0x10*/) | (int) ((num20 & 16711680U /*0xFF0000*/) >> 8) | (int) num22 & 16711680 /*0xFF0000*/ | ((int) num24 & 16711680 /*0xFF0000*/) << 8);
      uint num32 = (uint) ((int) (num18 >> 24) | (int) ((num20 & 4278190080U /*0xFF000000*/) >> 16 /*0x10*/) | (int) ((num22 & 4278190080U /*0xFF000000*/) >> 8) | (int) num24 & -16777216 /*0xFF000000*/);
      uint num33 = this.hash[index1];
      uint num34 = this.hash[index1 + 1];
      uint index2 = num25 + num33;
      uint num35 = num34 ^ Gost.sbox1[(int) (byte) index2] ^ Gost.sbox2[(int) (byte) (index2 >> 8)] ^ Gost.sbox3[(int) (byte) (index2 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index2 >> 24)];
      uint index3 = num26 + num35;
      uint num36 = num33 ^ Gost.sbox1[(int) (byte) index3] ^ Gost.sbox2[(int) (byte) (index3 >> 8)] ^ Gost.sbox3[(int) (byte) (index3 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index3 >> 24)];
      uint index4 = num27 + num36;
      uint num37 = num35 ^ Gost.sbox1[(int) (byte) index4] ^ Gost.sbox2[(int) (byte) (index4 >> 8)] ^ Gost.sbox3[(int) (byte) (index4 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index4 >> 24)];
      uint index5 = (uint) num28 + num37;
      uint num38 = num36 ^ Gost.sbox1[(int) (byte) index5] ^ Gost.sbox2[(int) (byte) (index5 >> 8)] ^ Gost.sbox3[(int) (byte) (index5 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index5 >> 24)];
      uint index6 = num29 + num38;
      uint num39 = num37 ^ Gost.sbox1[(int) (byte) index6] ^ Gost.sbox2[(int) (byte) (index6 >> 8)] ^ Gost.sbox3[(int) (byte) (index6 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index6 >> 24)];
      uint index7 = num30 + num39;
      uint num40 = num38 ^ Gost.sbox1[(int) (byte) index7] ^ Gost.sbox2[(int) (byte) (index7 >> 8)] ^ Gost.sbox3[(int) (byte) (index7 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index7 >> 24)];
      uint index8 = num31 + num40;
      uint num41 = num39 ^ Gost.sbox1[(int) (byte) index8] ^ Gost.sbox2[(int) (byte) (index8 >> 8)] ^ Gost.sbox3[(int) (byte) (index8 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index8 >> 24)];
      uint index9 = num32 + num41;
      uint num42 = num40 ^ Gost.sbox1[(int) (byte) index9] ^ Gost.sbox2[(int) (byte) (index9 >> 8)] ^ Gost.sbox3[(int) (byte) (index9 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index9 >> 24)];
      uint index10 = num25 + num42;
      uint num43 = num41 ^ Gost.sbox1[(int) (byte) index10] ^ Gost.sbox2[(int) (byte) (index10 >> 8)] ^ Gost.sbox3[(int) (byte) (index10 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index10 >> 24)];
      uint index11 = num26 + num43;
      uint num44 = num42 ^ Gost.sbox1[(int) (byte) index11] ^ Gost.sbox2[(int) (byte) (index11 >> 8)] ^ Gost.sbox3[(int) (byte) (index11 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index11 >> 24)];
      uint index12 = num27 + num44;
      uint num45 = num43 ^ Gost.sbox1[(int) (byte) index12] ^ Gost.sbox2[(int) (byte) (index12 >> 8)] ^ Gost.sbox3[(int) (byte) (index12 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index12 >> 24)];
      uint index13 = (uint) num28 + num45;
      uint num46 = num44 ^ Gost.sbox1[(int) (byte) index13] ^ Gost.sbox2[(int) (byte) (index13 >> 8)] ^ Gost.sbox3[(int) (byte) (index13 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index13 >> 24)];
      uint index14 = num29 + num46;
      uint num47 = num45 ^ Gost.sbox1[(int) (byte) index14] ^ Gost.sbox2[(int) (byte) (index14 >> 8)] ^ Gost.sbox3[(int) (byte) (index14 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index14 >> 24)];
      uint index15 = num30 + num47;
      uint num48 = num46 ^ Gost.sbox1[(int) (byte) index15] ^ Gost.sbox2[(int) (byte) (index15 >> 8)] ^ Gost.sbox3[(int) (byte) (index15 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index15 >> 24)];
      uint index16 = num31 + num48;
      uint num49 = num47 ^ Gost.sbox1[(int) (byte) index16] ^ Gost.sbox2[(int) (byte) (index16 >> 8)] ^ Gost.sbox3[(int) (byte) (index16 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index16 >> 24)];
      uint index17 = num32 + num49;
      uint num50 = num48 ^ Gost.sbox1[(int) (byte) index17] ^ Gost.sbox2[(int) (byte) (index17 >> 8)] ^ Gost.sbox3[(int) (byte) (index17 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index17 >> 24)];
      uint index18 = num25 + num50;
      uint num51 = num49 ^ Gost.sbox1[(int) (byte) index18] ^ Gost.sbox2[(int) (byte) (index18 >> 8)] ^ Gost.sbox3[(int) (byte) (index18 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index18 >> 24)];
      uint index19 = num26 + num51;
      uint num52 = num50 ^ Gost.sbox1[(int) (byte) index19] ^ Gost.sbox2[(int) (byte) (index19 >> 8)] ^ Gost.sbox3[(int) (byte) (index19 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index19 >> 24)];
      uint index20 = num27 + num52;
      uint num53 = num51 ^ Gost.sbox1[(int) (byte) index20] ^ Gost.sbox2[(int) (byte) (index20 >> 8)] ^ Gost.sbox3[(int) (byte) (index20 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index20 >> 24)];
      uint index21 = (uint) num28 + num53;
      uint num54 = num52 ^ Gost.sbox1[(int) (byte) index21] ^ Gost.sbox2[(int) (byte) (index21 >> 8)] ^ Gost.sbox3[(int) (byte) (index21 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index21 >> 24)];
      uint index22 = num29 + num54;
      uint num55 = num53 ^ Gost.sbox1[(int) (byte) index22] ^ Gost.sbox2[(int) (byte) (index22 >> 8)] ^ Gost.sbox3[(int) (byte) (index22 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index22 >> 24)];
      uint index23 = num30 + num55;
      uint num56 = num54 ^ Gost.sbox1[(int) (byte) index23] ^ Gost.sbox2[(int) (byte) (index23 >> 8)] ^ Gost.sbox3[(int) (byte) (index23 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index23 >> 24)];
      uint index24 = num31 + num56;
      uint num57 = num55 ^ Gost.sbox1[(int) (byte) index24] ^ Gost.sbox2[(int) (byte) (index24 >> 8)] ^ Gost.sbox3[(int) (byte) (index24 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index24 >> 24)];
      uint index25 = num32 + num57;
      uint num58 = num56 ^ Gost.sbox1[(int) (byte) index25] ^ Gost.sbox2[(int) (byte) (index25 >> 8)] ^ Gost.sbox3[(int) (byte) (index25 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index25 >> 24)];
      uint index26 = num32 + num58;
      uint num59 = num57 ^ Gost.sbox1[(int) (byte) index26] ^ Gost.sbox2[(int) (byte) (index26 >> 8)] ^ Gost.sbox3[(int) (byte) (index26 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index26 >> 24)];
      uint index27 = num31 + num59;
      uint num60 = num58 ^ Gost.sbox1[(int) (byte) index27] ^ Gost.sbox2[(int) (byte) (index27 >> 8)] ^ Gost.sbox3[(int) (byte) (index27 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index27 >> 24)];
      uint index28 = num30 + num60;
      uint num61 = num59 ^ Gost.sbox1[(int) (byte) index28] ^ Gost.sbox2[(int) (byte) (index28 >> 8)] ^ Gost.sbox3[(int) (byte) (index28 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index28 >> 24)];
      uint index29 = num29 + num61;
      uint num62 = num60 ^ Gost.sbox1[(int) (byte) index29] ^ Gost.sbox2[(int) (byte) (index29 >> 8)] ^ Gost.sbox3[(int) (byte) (index29 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index29 >> 24)];
      uint index30 = (uint) num28 + num62;
      uint num63 = num61 ^ Gost.sbox1[(int) (byte) index30] ^ Gost.sbox2[(int) (byte) (index30 >> 8)] ^ Gost.sbox3[(int) (byte) (index30 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index30 >> 24)];
      uint index31 = num27 + num63;
      uint num64 = num62 ^ Gost.sbox1[(int) (byte) index31] ^ Gost.sbox2[(int) (byte) (index31 >> 8)] ^ Gost.sbox3[(int) (byte) (index31 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index31 >> 24)];
      uint index32 = num26 + num64;
      uint num65 = num63 ^ Gost.sbox1[(int) (byte) index32] ^ Gost.sbox2[(int) (byte) (index32 >> 8)] ^ Gost.sbox3[(int) (byte) (index32 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index32 >> 24)];
      uint index33 = num25 + num65;
      uint num66 = num64 ^ Gost.sbox1[(int) (byte) index33] ^ Gost.sbox2[(int) (byte) (index33 >> 8)] ^ Gost.sbox3[(int) (byte) (index33 >> 16 /*0x10*/)] ^ Gost.sbox4[(int) (index33 >> 24)];
      uint num67 = num65;
      uint num68 = num66;
      numArray[index1] = num67;
      numArray[index1 + 1] = num68;
      if (index1 != 6)
      {
        uint num69 = num1 ^ num3;
        uint num70 = num2 ^ num4;
        num1 = num3;
        num2 = num4;
        num3 = num5;
        num4 = num6;
        num5 = num7;
        num6 = num8;
        num7 = num69;
        num8 = num70;
        if (index1 == 2)
        {
          num1 ^= 4278255360U /*0xFF00FF00*/;
          num2 ^= 4278255360U /*0xFF00FF00*/;
          num3 ^= 16711935U;
          num4 ^= 16711935U;
          num5 ^= 16776960U;
          num6 ^= 4278190335U;
          num7 ^= (uint) byte.MaxValue;
          num8 ^= 4278255615U;
        }
        uint num71 = num9;
        uint num72 = num11;
        num9 = num13;
        num11 = num15;
        num13 = num71 ^ num72;
        num15 = num9 ^ num72;
        uint num73 = num10;
        uint num74 = num12;
        num10 = num14;
        num12 = num16;
        num14 = num73 ^ num74;
        num16 = num10 ^ num74;
      }
      else
        break;
    }
    uint num75 = a_m[0] ^ numArray[6];
    uint num76 = a_m[1] ^ numArray[7];
    uint num77 = (uint) ((int) a_m[2] ^ (int) numArray[0] << 16 /*0x10*/ ^ (int) (numArray[0] >> 16 /*0x10*/) ^ (int) numArray[0] & (int) ushort.MaxValue ^ (int) numArray[1] & (int) ushort.MaxValue ^ (int) (numArray[1] >> 16 /*0x10*/) ^ (int) numArray[2] << 16 /*0x10*/ ^ (int) numArray[6] ^ (int) numArray[6] << 16 /*0x10*/ ^ (int) numArray[7] & -65536) ^ numArray[7] >> 16 /*0x10*/;
    uint num78 = (uint) ((int) a_m[3] ^ (int) numArray[0] & (int) ushort.MaxValue ^ (int) numArray[0] << 16 /*0x10*/ ^ (int) numArray[1] & (int) ushort.MaxValue ^ (int) numArray[1] << 16 /*0x10*/ ^ (int) (numArray[1] >> 16 /*0x10*/) ^ (int) numArray[2] << 16 /*0x10*/ ^ (int) (numArray[2] >> 16 /*0x10*/) ^ (int) numArray[3] << 16 /*0x10*/ ^ (int) numArray[6] ^ (int) numArray[6] << 16 /*0x10*/ ^ (int) (numArray[6] >> 16 /*0x10*/) ^ (int) numArray[7] & (int) ushort.MaxValue ^ (int) numArray[7] << 16 /*0x10*/) ^ numArray[7] >> 16 /*0x10*/;
    uint num79 = (uint) ((int) a_m[4] ^ (int) numArray[0] & -65536 ^ (int) numArray[0] << 16 /*0x10*/ ^ (int) (numArray[0] >> 16 /*0x10*/) ^ (int) numArray[1] & -65536 ^ (int) (numArray[1] >> 16 /*0x10*/) ^ (int) numArray[2] << 16 /*0x10*/ ^ (int) (numArray[2] >> 16 /*0x10*/) ^ (int) numArray[3] << 16 /*0x10*/ ^ (int) (numArray[3] >> 16 /*0x10*/) ^ (int) numArray[4] << 16 /*0x10*/ ^ (int) numArray[6] << 16 /*0x10*/ ^ (int) (numArray[6] >> 16 /*0x10*/) ^ (int) numArray[7] & (int) ushort.MaxValue ^ (int) numArray[7] << 16 /*0x10*/) ^ numArray[7] >> 16 /*0x10*/;
    uint num80 = (uint) ((int) a_m[5] ^ (int) numArray[0] << 16 /*0x10*/ ^ (int) (numArray[0] >> 16 /*0x10*/) ^ (int) numArray[0] & -65536 ^ (int) numArray[1] & (int) ushort.MaxValue ^ (int) numArray[2] ^ (int) (numArray[2] >> 16 /*0x10*/) ^ (int) numArray[3] << 16 /*0x10*/ ^ (int) (numArray[3] >> 16 /*0x10*/) ^ (int) numArray[4] << 16 /*0x10*/ ^ (int) (numArray[4] >> 16 /*0x10*/) ^ (int) numArray[5] << 16 /*0x10*/ ^ (int) numArray[6] << 16 /*0x10*/ ^ (int) (numArray[6] >> 16 /*0x10*/) ^ (int) numArray[7] & -65536 ^ (int) numArray[7] << 16 /*0x10*/) ^ numArray[7] >> 16 /*0x10*/;
    uint num81 = (uint) ((int) a_m[6] ^ (int) numArray[0] ^ (int) (numArray[1] >> 16 /*0x10*/) ^ (int) numArray[2] << 16 /*0x10*/ ^ (int) numArray[3] ^ (int) (numArray[3] >> 16 /*0x10*/) ^ (int) numArray[4] << 16 /*0x10*/ ^ (int) (numArray[4] >> 16 /*0x10*/) ^ (int) numArray[5] << 16 /*0x10*/ ^ (int) (numArray[5] >> 16 /*0x10*/) ^ (int) numArray[6] ^ (int) numArray[6] << 16 /*0x10*/ ^ (int) (numArray[6] >> 16 /*0x10*/) ^ (int) numArray[7] << 16 /*0x10*/);
    uint num82 = (uint) ((int) a_m[7] ^ (int) numArray[0] & -65536 ^ (int) numArray[0] << 16 /*0x10*/ ^ (int) numArray[1] & (int) ushort.MaxValue ^ (int) numArray[1] << 16 /*0x10*/ ^ (int) (numArray[2] >> 16 /*0x10*/) ^ (int) numArray[3] << 16 /*0x10*/ ^ (int) numArray[4] ^ (int) (numArray[4] >> 16 /*0x10*/) ^ (int) numArray[5] << 16 /*0x10*/ ^ (int) (numArray[5] >> 16 /*0x10*/) ^ (int) (numArray[6] >> 16 /*0x10*/) ^ (int) numArray[7] & (int) ushort.MaxValue ^ (int) numArray[7] << 16 /*0x10*/) ^ numArray[7] >> 16 /*0x10*/;
    uint num83 = this.hash[0] ^ num76 << 16 /*0x10*/ ^ num75 >> 16 /*0x10*/;
    uint num84 = this.hash[1] ^ num77 << 16 /*0x10*/ ^ num76 >> 16 /*0x10*/;
    uint num85 = this.hash[2] ^ num78 << 16 /*0x10*/ ^ num77 >> 16 /*0x10*/;
    uint num86 = this.hash[3] ^ num79 << 16 /*0x10*/ ^ num78 >> 16 /*0x10*/;
    uint num87 = this.hash[4] ^ num80 << 16 /*0x10*/ ^ num79 >> 16 /*0x10*/;
    uint num88 = this.hash[5] ^ num81 << 16 /*0x10*/ ^ num80 >> 16 /*0x10*/;
    uint num89 = this.hash[6] ^ num82 << 16 /*0x10*/ ^ num81 >> 16 /*0x10*/;
    uint num90 = (uint) ((int) this.hash[7] ^ (int) num75 & -65536 ^ (int) num75 << 16 /*0x10*/ ^ (int) (num82 >> 16 /*0x10*/) ^ (int) num76 & -65536 ^ (int) num76 << 16 /*0x10*/ ^ (int) num81 << 16 /*0x10*/ ^ (int) num82 & -65536);
    this.hash[0] = (uint) ((int) num83 & -65536 ^ (int) num83 << 16 /*0x10*/ ^ (int) (num83 >> 16 /*0x10*/) ^ (int) (num84 >> 16 /*0x10*/) ^ (int) num84 & -65536 ^ (int) num85 << 16 /*0x10*/ ^ (int) (num86 >> 16 /*0x10*/) ^ (int) num87 << 16 /*0x10*/ ^ (int) (num88 >> 16 /*0x10*/) ^ (int) num88 ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num90 << 16 /*0x10*/ ^ (int) (num90 >> 16 /*0x10*/) ^ (int) num90 & (int) ushort.MaxValue);
    this.hash[1] = (uint) ((int) num83 << 16 /*0x10*/ ^ (int) (num83 >> 16 /*0x10*/) ^ (int) num83 & -65536 ^ (int) num84 & (int) ushort.MaxValue ^ (int) num85 ^ (int) (num85 >> 16 /*0x10*/) ^ (int) num86 << 16 /*0x10*/ ^ (int) (num87 >> 16 /*0x10*/) ^ (int) num88 << 16 /*0x10*/ ^ (int) num89 << 16 /*0x10*/ ^ (int) num89 ^ (int) num90 & -65536) ^ num90 >> 16 /*0x10*/;
    this.hash[2] = (uint) ((int) num83 & (int) ushort.MaxValue ^ (int) num83 << 16 /*0x10*/ ^ (int) num84 << 16 /*0x10*/ ^ (int) (num84 >> 16 /*0x10*/) ^ (int) num84 & -65536 ^ (int) num85 << 16 /*0x10*/ ^ (int) (num86 >> 16 /*0x10*/) ^ (int) num86 ^ (int) num87 << 16 /*0x10*/ ^ (int) (num88 >> 16 /*0x10*/) ^ (int) num89 ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num90 & (int) ushort.MaxValue ^ (int) num90 << 16 /*0x10*/) ^ num90 >> 16 /*0x10*/;
    this.hash[3] = (uint) ((int) num83 << 16 /*0x10*/ ^ (int) (num83 >> 16 /*0x10*/) ^ (int) num83 & -65536 ^ (int) num84 & -65536 ^ (int) (num84 >> 16 /*0x10*/) ^ (int) num85 << 16 /*0x10*/ ^ (int) (num85 >> 16 /*0x10*/) ^ (int) num85 ^ (int) num86 << 16 /*0x10*/ ^ (int) (num87 >> 16 /*0x10*/) ^ (int) num87 ^ (int) num88 << 16 /*0x10*/ ^ (int) num89 << 16 /*0x10*/ ^ (int) num90 & (int) ushort.MaxValue) ^ num90 >> 16 /*0x10*/;
    this.hash[4] = (uint) ((int) (num83 >> 16 /*0x10*/) ^ (int) num84 << 16 /*0x10*/ ^ (int) num84 ^ (int) (num85 >> 16 /*0x10*/) ^ (int) num85 ^ (int) num86 << 16 /*0x10*/ ^ (int) (num86 >> 16 /*0x10*/) ^ (int) num86 ^ (int) num87 << 16 /*0x10*/ ^ (int) (num88 >> 16 /*0x10*/) ^ (int) num88 ^ (int) num89 << 16 /*0x10*/ ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num90 << 16 /*0x10*/);
    this.hash[5] = (uint) ((int) num83 << 16 /*0x10*/ ^ (int) num83 & -65536 ^ (int) num84 << 16 /*0x10*/ ^ (int) (num84 >> 16 /*0x10*/) ^ (int) num84 & -65536 ^ (int) num85 << 16 /*0x10*/ ^ (int) num85 ^ (int) (num86 >> 16 /*0x10*/) ^ (int) num86 ^ (int) num87 << 16 /*0x10*/ ^ (int) (num87 >> 16 /*0x10*/) ^ (int) num87 ^ (int) num88 << 16 /*0x10*/ ^ (int) num89 << 16 /*0x10*/ ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num89 ^ (int) num90 << 16 /*0x10*/ ^ (int) (num90 >> 16 /*0x10*/) ^ (int) num90 & -65536);
    this.hash[6] = (uint) ((int) num83 ^ (int) num85 ^ (int) (num85 >> 16 /*0x10*/) ^ (int) num86 ^ (int) num86 << 16 /*0x10*/ ^ (int) num87 ^ (int) (num87 >> 16 /*0x10*/) ^ (int) num88 << 16 /*0x10*/ ^ (int) (num88 >> 16 /*0x10*/) ^ (int) num88 ^ (int) num89 << 16 /*0x10*/ ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num89 ^ (int) num90 << 16 /*0x10*/) ^ num90;
    this.hash[7] = (uint) ((int) num83 ^ (int) (num83 >> 16 /*0x10*/) ^ (int) num84 << 16 /*0x10*/ ^ (int) (num84 >> 16 /*0x10*/) ^ (int) num85 << 16 /*0x10*/ ^ (int) (num86 >> 16 /*0x10*/) ^ (int) num86 ^ (int) num87 << 16 /*0x10*/ ^ (int) num87 ^ (int) (num88 >> 16 /*0x10*/) ^ (int) num88 ^ (int) num89 << 16 /*0x10*/ ^ (int) (num89 >> 16 /*0x10*/) ^ (int) num90 << 16 /*0x10*/) ^ num90;
  }
}
