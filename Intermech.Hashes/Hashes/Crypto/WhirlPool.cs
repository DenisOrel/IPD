// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.WhirlPool
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class WhirlPool : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private ulong[] hash;
  private static readonly uint ROUNDS = 10;
  private static readonly uint REDUCTION_POLYNOMIAL = 285;
  private static ulong[] C0 = new ulong[256 /*0x0100*/];
  private static ulong[] C1 = new ulong[256 /*0x0100*/];
  private static ulong[] C2 = new ulong[256 /*0x0100*/];
  private static ulong[] C3 = new ulong[256 /*0x0100*/];
  private static ulong[] C4 = new ulong[256 /*0x0100*/];
  private static ulong[] C5 = new ulong[256 /*0x0100*/];
  private static ulong[] C6 = new ulong[256 /*0x0100*/];
  private static ulong[] C7 = new ulong[256 /*0x0100*/];
  private static ulong[] rc = new ulong[(int) WhirlPool.ROUNDS + 1];
  private static readonly uint[] SBOX = new uint[256 /*0x0100*/]
  {
    24U,
    35U,
    198U,
    232U,
    135U,
    184U,
    1U,
    79U,
    54U,
    166U,
    210U,
    245U,
    121U,
    111U,
    145U,
    82U,
    96U /*0x60*/,
    188U,
    155U,
    142U,
    163U,
    12U,
    123U,
    53U,
    29U,
    224U /*0xE0*/,
    215U,
    194U,
    46U,
    75U,
    254U,
    87U,
    21U,
    119U,
    55U,
    229U,
    159U,
    240U /*0xF0*/,
    74U,
    218U,
    88U,
    201U,
    41U,
    10U,
    177U,
    160U /*0xA0*/,
    107U,
    133U,
    189U,
    93U,
    16U /*0x10*/,
    244U,
    203U,
    62U,
    5U,
    103U,
    228U,
    39U,
    65U,
    139U,
    167U,
    125U,
    149U,
    216U,
    251U,
    238U,
    124U,
    102U,
    221U,
    23U,
    71U,
    158U,
    202U,
    45U,
    191U,
    7U,
    173U,
    90U,
    131U,
    51U,
    99U,
    2U,
    170U,
    113U,
    200U,
    25U,
    73U,
    217U,
    242U,
    227U,
    91U,
    136U,
    154U,
    38U,
    50U,
    176U /*0xB0*/,
    233U,
    15U,
    213U,
    128U /*0x80*/,
    190U,
    205U,
    52U,
    72U,
    (uint) byte.MaxValue,
    122U,
    144U /*0x90*/,
    95U,
    32U /*0x20*/,
    104U,
    26U,
    174U,
    180U,
    84U,
    147U,
    34U,
    100U,
    241U,
    115U,
    18U,
    64U /*0x40*/,
    8U,
    195U,
    236U,
    219U,
    161U,
    141U,
    61U,
    151U,
    0U,
    207U,
    43U,
    118U,
    130U,
    214U,
    27U,
    181U,
    175U,
    106U,
    80U /*0x50*/,
    69U,
    243U,
    48U /*0x30*/,
    239U,
    63U /*0x3F*/,
    85U,
    162U,
    234U,
    101U,
    186U,
    47U,
    192U /*0xC0*/,
    222U,
    28U,
    253U,
    77U,
    146U,
    117U,
    6U,
    138U,
    178U,
    230U,
    14U,
    31U /*0x1F*/,
    98U,
    212U,
    168U,
    150U,
    249U,
    197U,
    37U,
    89U,
    132U,
    114U,
    57U,
    76U,
    94U,
    120U,
    56U,
    140U,
    209U,
    165U,
    226U,
    97U,
    179U,
    33U,
    156U,
    30U,
    67U,
    199U,
    252U,
    4U,
    81U,
    153U,
    109U,
    13U,
    250U,
    223U,
    126U,
    36U,
    59U,
    171U,
    206U,
    17U,
    143U,
    78U,
    183U,
    235U,
    60U,
    129U,
    148U,
    247U,
    185U,
    19U,
    44U,
    211U,
    231U,
    110U,
    196U,
    3U,
    86U,
    68U,
    (uint) sbyte.MaxValue,
    169U,
    42U,
    187U,
    193U,
    83U,
    220U,
    11U,
    157U,
    108U,
    49U,
    116U,
    246U,
    70U,
    172U,
    137U,
    20U,
    225U,
    22U,
    58U,
    105U,
    9U,
    112U /*0x70*/,
    182U,
    208U /*0xD0*/,
    237U,
    204U,
    66U,
    152U,
    164U,
    40U,
    92U,
    248U,
    134U
  };

  static WhirlPool()
  {
    for (uint index = 0; index < 256U /*0x0100*/; ++index)
    {
      uint num1 = WhirlPool.SBOX[(int) index];
      uint num2 = WhirlPool.maskWithReductionPolynomial(num1 << 1);
      uint num3 = WhirlPool.maskWithReductionPolynomial(num2 << 1);
      uint num4 = num3 ^ num1;
      uint num5 = WhirlPool.maskWithReductionPolynomial(num3 << 1);
      uint num6 = num5 ^ num1;
      WhirlPool.C0[(int) index] = WhirlPool.packIntoUInt64(num1, num1, num3, num1, num5, num4, num2, num6);
      WhirlPool.C1[(int) index] = WhirlPool.packIntoUInt64(num6, num1, num1, num3, num1, num5, num4, num2);
      WhirlPool.C2[(int) index] = WhirlPool.packIntoUInt64(num2, num6, num1, num1, num3, num1, num5, num4);
      WhirlPool.C3[(int) index] = WhirlPool.packIntoUInt64(num4, num2, num6, num1, num1, num3, num1, num5);
      WhirlPool.C4[(int) index] = WhirlPool.packIntoUInt64(num5, num4, num2, num6, num1, num1, num3, num1);
      WhirlPool.C5[(int) index] = WhirlPool.packIntoUInt64(num1, num5, num4, num2, num6, num1, num1, num3);
      WhirlPool.C6[(int) index] = WhirlPool.packIntoUInt64(num3, num1, num5, num4, num2, num6, num1, num1);
      WhirlPool.C7[(int) index] = WhirlPool.packIntoUInt64(num1, num3, num1, num5, num4, num2, num6, num1);
    }
    WhirlPool.rc[0] = 0UL;
    for (uint index1 = 1; index1 < WhirlPool.ROUNDS + 1U; ++index1)
    {
      uint index2 = (uint) (8 * ((int) index1 - 1));
      WhirlPool.rc[(int) index1] = (ulong) ((long) WhirlPool.C0[(int) index2] & -72057594037927936L /*0xFF00000000000000*/ ^ (long) WhirlPool.C1[(int) index2 + 1] & 71776119061217280L /*0xFF000000000000*/ ^ (long) WhirlPool.C2[(int) index2 + 2] & 280375465082880L /*0xFF0000000000*/ ^ (long) WhirlPool.C3[(int) index2 + 3] & 1095216660480L /*0xFF00000000*/ ^ (long) WhirlPool.C4[(int) index2 + 4] & 4278190080L /*0xFF000000*/ ^ (long) WhirlPool.C5[(int) index2 + 5] & 16711680L /*0xFF0000*/ ^ (long) WhirlPool.C6[(int) index2 + 6] & 65280L ^ (long) WhirlPool.C7[(int) index2 + 7] & (long) byte.MaxValue);
    }
  }

  public WhirlPool()
    : base(64 /*0x40*/, 64 /*0x40*/)
  {
    this.hash = new ulong[8];
  }

  public override IHash Clone()
  {
    WhirlPool whirlPool = new WhirlPool();
    whirlPool.buffer = this.buffer.Clone();
    whirlPool.processed_bytes = this.processed_bytes;
    whirlPool.hash = this.hash.DeepCopy();
    whirlPool.BufferSize = this.BufferSize;
    return (IHash) whirlPool;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.hash);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[64 /*0x40*/];
    fixed (ulong* src = this.hash)
      fixed (byte* dest = result)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override void Finish()
  {
    long x = (long) this.processed_bytes * 8L;
    int a_index = this.buffer.Position <= 31 /*0x1F*/ ? 56 - this.buffer.Position : 120 - this.buffer.Position;
    byte[] a_out = new byte[a_index + 8];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.be2me_64((ulong) x), ref a_out, a_index);
    int a_length = a_index + 8;
    this.TransformBytes(a_out, 0, a_length);
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    ulong[] array = new ulong[8];
    ulong[] dest1 = new ulong[8];
    ulong[] src = new ulong[8];
    ulong[] dest2 = new ulong[8];
    fixed (ulong* dest3 = array)
    {
      Converters.be64_copy(a_data, a_index, (IntPtr) (void*) dest3, 0, 64 /*0x40*/);
      for (int index = 0; index < 8; ++index)
      {
        dest1[index] = this.hash[index];
        dest2[index] = array[index] ^ dest1[index];
      }
      for (int index1 = 1; (long) index1 < (long) (WhirlPool.ROUNDS + 1U); ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
        {
          src[index2] = 0UL;
          src[index2] = src[index2] ^ WhirlPool.C0[(int) (byte) (dest1[index2 & 7] >> 56)];
          src[index2] = src[index2] ^ WhirlPool.C1[(int) (byte) (dest1[index2 - 1 & 7] >> 48 /*0x30*/)];
          src[index2] = src[index2] ^ WhirlPool.C2[(int) (byte) (dest1[index2 - 2 & 7] >> 40)];
          src[index2] = src[index2] ^ WhirlPool.C3[(int) (byte) (dest1[index2 - 3 & 7] >> 32 /*0x20*/)];
          src[index2] = src[index2] ^ WhirlPool.C4[(int) (byte) (dest1[index2 - 4 & 7] >> 24)];
          src[index2] = src[index2] ^ WhirlPool.C5[(int) (byte) (dest1[index2 - 5 & 7] >> 16 /*0x10*/)];
          src[index2] = src[index2] ^ WhirlPool.C6[(int) (byte) (dest1[index2 - 6 & 7] >> 8)];
          src[index2] = src[index2] ^ WhirlPool.C7[(int) (byte) dest1[index2 - 7 & 7]];
        }
        Intermech.Hashes.Utils.Utils.Memmove(ref dest1, src, 8);
        dest1[0] = dest1[0] ^ WhirlPool.rc[index1];
        for (int index3 = 0; index3 < 8; ++index3)
        {
          src[index3] = dest1[index3];
          src[index3] = src[index3] ^ WhirlPool.C0[(int) (byte) (dest2[index3 & 7] >> 56)];
          src[index3] = src[index3] ^ WhirlPool.C1[(int) (byte) (dest2[index3 - 1 & 7] >> 48 /*0x30*/)];
          src[index3] = src[index3] ^ WhirlPool.C2[(int) (byte) (dest2[index3 - 2 & 7] >> 40)];
          src[index3] = src[index3] ^ WhirlPool.C3[(int) (byte) (dest2[index3 - 3 & 7] >> 32 /*0x20*/)];
          src[index3] = src[index3] ^ WhirlPool.C4[(int) (byte) (dest2[index3 - 4 & 7] >> 24)];
          src[index3] = src[index3] ^ WhirlPool.C5[(int) (byte) (dest2[index3 - 5 & 7] >> 16 /*0x10*/)];
          src[index3] = src[index3] ^ WhirlPool.C6[(int) (byte) (dest2[index3 - 6 & 7] >> 8)];
          src[index3] = src[index3] ^ WhirlPool.C7[(int) (byte) dest2[index3 - 7 & 7]];
        }
        Intermech.Hashes.Utils.Utils.Memmove(ref dest2, src, 8);
      }
      for (int index = 0; index < 8; ++index)
        this.hash[index] = this.hash[index] ^ dest2[index] ^ array[index];
      Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
    }
  }

  private static uint maskWithReductionPolynomial(uint input)
  {
    uint num = input;
    if (num >= 256U /*0x0100*/)
      num ^= WhirlPool.REDUCTION_POLYNOMIAL;
    return num;
  }

  private static ulong packIntoUInt64(
    uint b7,
    uint b6,
    uint b5,
    uint b4,
    uint b3,
    uint b2,
    uint b1,
    uint b0)
  {
    return (ulong) ((long) b7 << 56 ^ (long) b6 << 48 /*0x30*/ ^ (long) b5 << 40 ^ (long) b4 << 32 /*0x20*/ ^ (long) b3 << 24 ^ (long) b2 << 16 /*0x10*/ ^ (long) b1 << 8) ^ (ulong) b0;
  }
}
