// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA3
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class SHA3 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  protected ulong[] state;
  protected HashMode hash_mode;
  private static readonly ulong[] RC = new ulong[24]
  {
    1UL,
    32898UL,
    9223372036854808714UL /*0x800000000000808A*/,
    9223372039002292224UL /*0x8000000080008000*/,
    32907UL,
    2147483649UL /*0x80000001*/,
    9223372039002292353UL /*0x8000000080008081*/,
    9223372036854808585UL /*0x8000000000008009*/,
    138UL,
    136UL,
    2147516425UL /*0x80008009*/,
    2147483658UL /*0x8000000A*/,
    2147516555UL,
    9223372036854775947UL /*0x800000000000008B*/,
    9223372036854808713UL /*0x8000000000008089*/,
    9223372036854808579UL /*0x8000000000008003*/,
    9223372036854808578UL /*0x8000000000008002*/,
    9223372036854775936UL /*0x8000000000000080*/,
    32778UL,
    9223372039002259466UL /*0x800000008000000A*/,
    9223372039002292353UL /*0x8000000080008081*/,
    9223372036854808704UL /*0x8000000000008080*/,
    2147483649UL /*0x80000001*/,
    9223372039002292232UL /*0x8000000080008008*/
  };

  protected SHA3(int a_hash_size)
    : base(a_hash_size, 200 - a_hash_size * 2)
  {
    this.state = new ulong[25];
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.state);
    base.Initialize();
  }

  public override string Name
  {
    get
    {
      switch (this.hash_mode)
      {
        case HashMode.Keccak:
          return $"{"Keccak"}_{this.hash_size * 8}";
        case HashMode.CShake:
        case HashMode.Shake:
          return $"{this.GetType().Name}_{"XOFSizeInBytes"}_{(this as IXOF).XOFSizeInBits >> 3}";
        case HashMode.SHA3:
          return this.GetType().Name;
        default:
          throw new ArgumentInvalidHashLibException(string.Format(Global.InvalidHashMode, (object) "Keccak, SHA3, Shake, CShake"));
      }
    }
  }

  protected void KeccakF1600_StatePermute()
  {
    ulong num1 = this.state[0];
    ulong num2 = this.state[1];
    ulong num3 = this.state[2];
    ulong num4 = this.state[3];
    ulong num5 = this.state[4];
    ulong num6 = this.state[5];
    ulong num7 = this.state[6];
    ulong num8 = this.state[7];
    ulong num9 = this.state[8];
    ulong num10 = this.state[9];
    ulong num11 = this.state[10];
    ulong num12 = this.state[11];
    ulong num13 = this.state[12];
    ulong num14 = this.state[13];
    ulong num15 = this.state[14];
    ulong num16 = this.state[15];
    ulong num17 = this.state[16 /*0x10*/];
    ulong num18 = this.state[17];
    ulong num19 = this.state[18];
    ulong num20 = this.state[19];
    ulong num21 = this.state[20];
    ulong num22 = this.state[21];
    ulong num23 = this.state[22];
    ulong num24 = this.state[23];
    ulong num25 = this.state[24];
    for (int index = 0; index < 24; index += 2)
    {
      ulong a_value1 = num1 ^ num6 ^ num11 ^ num16 ^ num21;
      ulong a_value2 = num2 ^ num7 ^ num12 ^ num17 ^ num22;
      ulong a_value3 = num3 ^ num8 ^ num13 ^ num18 ^ num23;
      ulong a_value4 = num4 ^ num9 ^ num14 ^ num19 ^ num24;
      ulong a_value5 = num5 ^ num10 ^ num15 ^ num20 ^ num25;
      ulong num26 = a_value5 ^ Bits.RotateLeft64(a_value2, 1);
      ulong num27 = a_value1 ^ Bits.RotateLeft64(a_value3, 1);
      ulong num28 = a_value2 ^ Bits.RotateLeft64(a_value4, 1);
      ulong num29 = a_value3 ^ Bits.RotateLeft64(a_value5, 1);
      ulong num30 = a_value4 ^ Bits.RotateLeft64(a_value1, 1);
      ulong num31 = num1 ^ num26;
      ulong num32 = Bits.RotateLeft64(num7 ^ num27, 44);
      ulong num33 = Bits.RotateLeft64(num13 ^ num28, 43);
      ulong num34 = Bits.RotateLeft64(num19 ^ num29, 21);
      ulong num35 = Bits.RotateLeft64(num25 ^ num30, 14);
      long num36 = (long) num31 ^ ~(long) num32 & (long) num33 ^ (long) SHA3.RC[index];
      ulong num37 = num32 ^ ~num33 & num34;
      ulong num38 = num33 ^ ~num34 & num35;
      ulong num39 = num34 ^ ~num35 & num31;
      ulong num40 = num35 ^ ~num31 & num32;
      ulong num41 = Bits.RotateLeft64(num4 ^ num29, 28);
      ulong num42 = Bits.RotateLeft64(num10 ^ num30, 20);
      ulong num43 = Bits.RotateLeft64(num11 ^ num26, 3);
      ulong num44 = Bits.RotateLeft64(num17 ^ num27, 45);
      ulong num45 = Bits.RotateLeft64(num23 ^ num28, 61);
      ulong num46 = num41 ^ ~num42 & num43;
      ulong num47 = num42 ^ ~num43 & num44;
      ulong num48 = num43 ^ ~num44 & num45;
      ulong num49 = num44 ^ ~num45 & num41;
      ulong num50 = num45 ^ ~num41 & num42;
      ulong num51 = Bits.RotateLeft64(num2 ^ num27, 1);
      ulong num52 = Bits.RotateLeft64(num8 ^ num28, 6);
      ulong num53 = Bits.RotateLeft64(num14 ^ num29, 25);
      ulong num54 = Bits.RotateLeft64(num20 ^ num30, 8);
      ulong num55 = Bits.RotateLeft64(num21 ^ num26, 18);
      ulong num56 = num51 ^ ~num52 & num53;
      ulong num57 = num52 ^ ~num53 & num54;
      ulong num58 = num53 ^ ~num54 & num55;
      ulong num59 = num54 ^ ~num55 & num51;
      ulong num60 = num55 ^ ~num51 & num52;
      ulong num61 = Bits.RotateLeft64(num5 ^ num30, 27);
      ulong num62 = Bits.RotateLeft64(num6 ^ num26, 36);
      ulong num63 = Bits.RotateLeft64(num12 ^ num27, 10);
      ulong num64 = Bits.RotateLeft64(num18 ^ num28, 15);
      ulong num65 = Bits.RotateLeft64(num24 ^ num29, 56);
      ulong num66 = num61 ^ ~num62 & num63;
      ulong num67 = num62 ^ ~num63 & num64;
      ulong num68 = num63 ^ ~num64 & num65;
      ulong num69 = num64 ^ ~num65 & num61;
      ulong num70 = num65 ^ ~num61 & num62;
      ulong num71 = Bits.RotateLeft64(num3 ^ num28, 62);
      ulong num72 = Bits.RotateLeft64(num9 ^ num29, 55);
      ulong num73 = Bits.RotateLeft64(num15 ^ num30, 39);
      ulong num74 = Bits.RotateLeft64(num16 ^ num26, 41);
      ulong num75 = Bits.RotateLeft64(num22 ^ num27, 2);
      ulong num76 = num71 ^ ~num72 & num73;
      ulong num77 = num72 ^ ~num73 & num74;
      ulong num78 = num73 ^ ~num74 & num75;
      ulong num79 = num74 ^ ~num75 & num71;
      ulong num80 = num75 ^ ~num71 & num72;
      ulong a_value6 = (ulong) num36 ^ num46 ^ num56 ^ num66 ^ num76;
      ulong a_value7 = num37 ^ num47 ^ num57 ^ num67 ^ num77;
      ulong a_value8 = num38 ^ num48 ^ num58 ^ num68 ^ num78;
      ulong a_value9 = num39 ^ num49 ^ num59 ^ num69 ^ num79;
      ulong a_value10 = num40 ^ num50 ^ num60 ^ num70 ^ num80;
      ulong num81 = a_value10 ^ Bits.RotateLeft64(a_value7, 1);
      ulong num82 = a_value6 ^ Bits.RotateLeft64(a_value8, 1);
      ulong num83 = a_value7 ^ Bits.RotateLeft64(a_value9, 1);
      ulong num84 = a_value8 ^ Bits.RotateLeft64(a_value10, 1);
      ulong num85 = a_value9 ^ Bits.RotateLeft64(a_value6, 1);
      ulong num86 = (ulong) num36 ^ num81;
      ulong num87 = Bits.RotateLeft64(num47 ^ num82, 44);
      ulong num88 = Bits.RotateLeft64(num58 ^ num83, 43);
      ulong num89 = Bits.RotateLeft64(num69 ^ num84, 21);
      ulong num90 = Bits.RotateLeft64(num80 ^ num85, 14);
      num1 = num86 ^ ~num87 & num88 ^ SHA3.RC[index + 1];
      num2 = num87 ^ ~num88 & num89;
      num3 = num88 ^ ~num89 & num90;
      num4 = num89 ^ ~num90 & num86;
      num5 = num90 ^ ~num86 & num87;
      ulong num91 = Bits.RotateLeft64(num39 ^ num84, 28);
      ulong num92 = Bits.RotateLeft64(num50 ^ num85, 20);
      ulong num93 = Bits.RotateLeft64(num56 ^ num81, 3);
      ulong num94 = Bits.RotateLeft64(num67 ^ num82, 45);
      ulong num95 = Bits.RotateLeft64(num78 ^ num83, 61);
      num6 = num91 ^ ~num92 & num93;
      num7 = num92 ^ ~num93 & num94;
      num8 = num93 ^ ~num94 & num95;
      num9 = num94 ^ ~num95 & num91;
      num10 = num95 ^ ~num91 & num92;
      ulong num96 = Bits.RotateLeft64(num37 ^ num82, 1);
      ulong num97 = Bits.RotateLeft64(num48 ^ num83, 6);
      ulong num98 = Bits.RotateLeft64(num59 ^ num84, 25);
      ulong num99 = Bits.RotateLeft64(num70 ^ num85, 8);
      ulong num100 = Bits.RotateLeft64(num76 ^ num81, 18);
      num11 = num96 ^ ~num97 & num98;
      num12 = num97 ^ ~num98 & num99;
      num13 = num98 ^ ~num99 & num100;
      num14 = num99 ^ ~num100 & num96;
      num15 = num100 ^ ~num96 & num97;
      ulong num101 = Bits.RotateLeft64(num40 ^ num85, 27);
      ulong num102 = Bits.RotateLeft64(num46 ^ num81, 36);
      ulong num103 = Bits.RotateLeft64(num57 ^ num82, 10);
      ulong num104 = Bits.RotateLeft64(num68 ^ num83, 15);
      ulong num105 = Bits.RotateLeft64(num79 ^ num84, 56);
      num16 = num101 ^ ~num102 & num103;
      num17 = num102 ^ ~num103 & num104;
      num18 = num103 ^ ~num104 & num105;
      num19 = num104 ^ ~num105 & num101;
      num20 = num105 ^ ~num101 & num102;
      ulong num106 = Bits.RotateLeft64(num38 ^ num83, 62);
      ulong num107 = Bits.RotateLeft64(num49 ^ num84, 55);
      ulong num108 = Bits.RotateLeft64(num60 ^ num85, 39);
      ulong num109 = Bits.RotateLeft64(num66 ^ num81, 41);
      ulong num110 = Bits.RotateLeft64(num77 ^ num82, 2);
      num21 = num106 ^ ~num107 & num108;
      num22 = num107 ^ ~num108 & num109;
      num23 = num108 ^ ~num109 & num110;
      num24 = num109 ^ ~num110 & num106;
      num25 = num110 ^ ~num106 & num107;
    }
    this.state[0] = num1;
    this.state[1] = num2;
    this.state[2] = num3;
    this.state[3] = num4;
    this.state[4] = num5;
    this.state[5] = num6;
    this.state[6] = num7;
    this.state[7] = num8;
    this.state[8] = num9;
    this.state[9] = num10;
    this.state[10] = num11;
    this.state[11] = num12;
    this.state[12] = num13;
    this.state[13] = num14;
    this.state[14] = num15;
    this.state[15] = num16;
    this.state[16 /*0x10*/] = num17;
    this.state[17] = num18;
    this.state[18] = num19;
    this.state[19] = num20;
    this.state[20] = num21;
    this.state[21] = num22;
    this.state[22] = num23;
    this.state[23] = num24;
    this.state[24] = num25;
  }

  protected override unsafe void Finish()
  {
    int position = this.buffer.Position;
    byte[] bytesZeroPadded = this.buffer.GetBytesZeroPadded();
    bytesZeroPadded[position] = (byte) this.hash_mode;
    bytesZeroPadded[this.BlockSize - 1] = (byte) ((uint) bytesZeroPadded[this.BlockSize - 1] ^ 128U /*0x80*/);
    fixed (byte* a_data = bytesZeroPadded)
      this.TransformBlock((IntPtr) (void*) a_data, bytesZeroPadded.Length, 0);
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[this.HashSize];
    fixed (ulong* src = this.state)
      fixed (byte* dest = result)
        Converters.le64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    ulong[] array = new ulong[21];
    fixed (ulong* dest = array)
      Converters.le64_copy(a_data, a_index, (IntPtr) (void*) dest, 0, a_data_length);
    int index1 = 0;
    for (int index2 = this.BlockSize >> 3; index1 < index2; ++index1)
      this.state[index1] = this.state[index1] ^ array[index1];
    this.KeccakF1600_StatePermute();
    Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
  }
}
