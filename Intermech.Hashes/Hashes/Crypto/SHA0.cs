// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA0
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal class SHA0 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  protected uint[] state;
  private static readonly uint C1 = 1518500249;
  private static readonly uint C2 = 1859775393;
  private static readonly uint C3 = 2400959708;
  private static readonly uint C4 = 3395469782;

  public SHA0()
    : base(20, 64 /*0x40*/)
  {
    this.state = new uint[5];
  }

  public override IHash Clone()
  {
    SHA0 shA0 = new SHA0();
    shA0.buffer = this.buffer.Clone();
    shA0.processed_bytes = this.processed_bytes;
    shA0.state = this.state.DeepCopy();
    shA0.BufferSize = this.BufferSize;
    return (IHash) shA0;
  }

  public override void Initialize()
  {
    this.state[0] = 1732584193U;
    this.state[1] = 4023233417U;
    this.state[2] = 2562383102U;
    this.state[3] = 271733878U;
    this.state[4] = 3285377520U;
    base.Initialize();
  }

  protected virtual unsafe void Expand(uint* a_data)
  {
    a_data[16 /*0x10*/] = a_data[13] ^ a_data[8] ^ a_data[2] ^ *a_data;
    a_data[17] = a_data[14] ^ a_data[9] ^ a_data[3] ^ a_data[1];
    a_data[18] = a_data[15] ^ a_data[10] ^ a_data[4] ^ a_data[2];
    a_data[19] = a_data[16 /*0x10*/] ^ a_data[11] ^ a_data[5] ^ a_data[3];
    a_data[20] = a_data[17] ^ a_data[12] ^ a_data[6] ^ a_data[4];
    a_data[21] = a_data[18] ^ a_data[13] ^ a_data[7] ^ a_data[5];
    a_data[22] = a_data[19] ^ a_data[14] ^ a_data[8] ^ a_data[6];
    a_data[23] = a_data[20] ^ a_data[15] ^ a_data[9] ^ a_data[7];
    a_data[24] = a_data[21] ^ a_data[16 /*0x10*/] ^ a_data[10] ^ a_data[8];
    a_data[25] = a_data[22] ^ a_data[17] ^ a_data[11] ^ a_data[9];
    a_data[26] = a_data[23] ^ a_data[18] ^ a_data[12] ^ a_data[10];
    a_data[27] = a_data[24] ^ a_data[19] ^ a_data[13] ^ a_data[11];
    a_data[28] = a_data[25] ^ a_data[20] ^ a_data[14] ^ a_data[12];
    a_data[29] = a_data[26] ^ a_data[21] ^ a_data[15] ^ a_data[13];
    a_data[30] = a_data[27] ^ a_data[22] ^ a_data[16 /*0x10*/] ^ a_data[14];
    a_data[31 /*0x1F*/] = a_data[28] ^ a_data[23] ^ a_data[17] ^ a_data[15];
    a_data[32 /*0x20*/] = a_data[29] ^ a_data[24] ^ a_data[18] ^ a_data[16 /*0x10*/];
    a_data[33] = a_data[30] ^ a_data[25] ^ a_data[19] ^ a_data[17];
    a_data[34] = a_data[31 /*0x1F*/] ^ a_data[26] ^ a_data[20] ^ a_data[18];
    a_data[35] = a_data[32 /*0x20*/] ^ a_data[27] ^ a_data[21] ^ a_data[19];
    a_data[36] = a_data[33] ^ a_data[28] ^ a_data[22] ^ a_data[20];
    a_data[37] = a_data[34] ^ a_data[29] ^ a_data[23] ^ a_data[21];
    a_data[38] = a_data[35] ^ a_data[30] ^ a_data[24] ^ a_data[22];
    a_data[39] = a_data[36] ^ a_data[31 /*0x1F*/] ^ a_data[25] ^ a_data[23];
    a_data[40] = a_data[37] ^ a_data[32 /*0x20*/] ^ a_data[26] ^ a_data[24];
    a_data[41] = a_data[38] ^ a_data[33] ^ a_data[27] ^ a_data[25];
    a_data[42] = a_data[39] ^ a_data[34] ^ a_data[28] ^ a_data[26];
    a_data[43] = a_data[40] ^ a_data[35] ^ a_data[29] ^ a_data[27];
    a_data[44] = a_data[41] ^ a_data[36] ^ a_data[30] ^ a_data[28];
    a_data[45] = a_data[42] ^ a_data[37] ^ a_data[31 /*0x1F*/] ^ a_data[29];
    a_data[46] = a_data[43] ^ a_data[38] ^ a_data[32 /*0x20*/] ^ a_data[30];
    a_data[47] = a_data[44] ^ a_data[39] ^ a_data[33] ^ a_data[31 /*0x1F*/];
    a_data[48 /*0x30*/] = a_data[45] ^ a_data[40] ^ a_data[34] ^ a_data[32 /*0x20*/];
    a_data[49] = a_data[46] ^ a_data[41] ^ a_data[35] ^ a_data[33];
    a_data[50] = a_data[47] ^ a_data[42] ^ a_data[36] ^ a_data[34];
    a_data[51] = a_data[48 /*0x30*/] ^ a_data[43] ^ a_data[37] ^ a_data[35];
    a_data[52] = a_data[49] ^ a_data[44] ^ a_data[38] ^ a_data[36];
    a_data[53] = a_data[50] ^ a_data[45] ^ a_data[39] ^ a_data[37];
    a_data[54] = a_data[51] ^ a_data[46] ^ a_data[40] ^ a_data[38];
    a_data[55] = a_data[52] ^ a_data[47] ^ a_data[41] ^ a_data[39];
    a_data[56] = a_data[53] ^ a_data[48 /*0x30*/] ^ a_data[42] ^ a_data[40];
    a_data[57] = a_data[54] ^ a_data[49] ^ a_data[43] ^ a_data[41];
    a_data[58] = a_data[55] ^ a_data[50] ^ a_data[44] ^ a_data[42];
    a_data[59] = a_data[56] ^ a_data[51] ^ a_data[45] ^ a_data[43];
    a_data[60] = a_data[57] ^ a_data[52] ^ a_data[46] ^ a_data[44];
    a_data[61] = a_data[58] ^ a_data[53] ^ a_data[47] ^ a_data[45];
    a_data[62] = a_data[59] ^ a_data[54] ^ a_data[48 /*0x30*/] ^ a_data[46];
    a_data[63 /*0x3F*/] = a_data[60] ^ a_data[55] ^ a_data[49] ^ a_data[47];
    a_data[64 /*0x40*/] = a_data[61] ^ a_data[56] ^ a_data[50] ^ a_data[48 /*0x30*/];
    a_data[65] = a_data[62] ^ a_data[57] ^ a_data[51] ^ a_data[49];
    a_data[66] = a_data[63 /*0x3F*/] ^ a_data[58] ^ a_data[52] ^ a_data[50];
    a_data[67] = a_data[64 /*0x40*/] ^ a_data[59] ^ a_data[53] ^ a_data[51];
    a_data[68] = a_data[65] ^ a_data[60] ^ a_data[54] ^ a_data[52];
    a_data[69] = a_data[66] ^ a_data[61] ^ a_data[55] ^ a_data[53];
    a_data[70] = a_data[67] ^ a_data[62] ^ a_data[56] ^ a_data[54];
    a_data[71] = a_data[68] ^ a_data[63 /*0x3F*/] ^ a_data[57] ^ a_data[55];
    a_data[72] = a_data[69] ^ a_data[64 /*0x40*/] ^ a_data[58] ^ a_data[56];
    a_data[73] = a_data[70] ^ a_data[65] ^ a_data[59] ^ a_data[57];
    a_data[74] = a_data[71] ^ a_data[66] ^ a_data[60] ^ a_data[58];
    a_data[75] = a_data[72] ^ a_data[67] ^ a_data[61] ^ a_data[59];
    a_data[76] = a_data[73] ^ a_data[68] ^ a_data[62] ^ a_data[60];
    a_data[77] = a_data[74] ^ a_data[69] ^ a_data[63 /*0x3F*/] ^ a_data[61];
    a_data[78] = a_data[75] ^ a_data[70] ^ a_data[64 /*0x40*/] ^ a_data[62];
    a_data[79] = a_data[76] ^ a_data[71] ^ a_data[65] ^ a_data[63 /*0x3F*/];
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[20];
    fixed (uint* src = this.state)
      fixed (byte* dest = result)
        Converters.be32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override void Finish()
  {
    long x = (long) this.processed_bytes * 8L;
    int a_index = this.buffer.Position >= 56 ? 120 - this.buffer.Position : 56 - this.buffer.Position;
    byte[] a_out = new byte[a_index + 8];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.be2me_64((ulong) x), ref a_out, a_index);
    int a_length = a_index + 8;
    this.TransformBytes(a_out, 0, a_length);
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] array = new uint[80 /*0x50*/];
    fixed (uint* numPtr = array)
    {
      Converters.be32_copy(a_data, a_index, (IntPtr) (void*) numPtr, 0, 64 /*0x40*/);
      this.Expand(numPtr);
    }
    uint a_value1 = this.state[0];
    uint a_value2 = this.state[1];
    uint num1 = this.state[2];
    uint num2 = this.state[3];
    uint num3 = this.state[4];
    uint a_value3 = (uint) ((int) array[0] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value1, 5) + ((int) num2 ^ (int) a_value2 & ((int) num1 ^ (int) num2))) + num3;
    uint num4 = Bits.RotateLeft32(a_value2, 30);
    uint a_value4 = (uint) ((int) array[1] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value3, 5) + ((int) num1 ^ (int) a_value1 & ((int) num4 ^ (int) num1))) + num2;
    uint num5 = Bits.RotateLeft32(a_value1, 30);
    uint a_value5 = (uint) ((int) array[2] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value4, 5) + ((int) num4 ^ (int) a_value3 & ((int) num5 ^ (int) num4))) + num1;
    uint num6 = Bits.RotateLeft32(a_value3, 30);
    uint a_value6 = (uint) ((int) array[3] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value5, 5) + ((int) num5 ^ (int) a_value4 & ((int) num6 ^ (int) num5))) + num4;
    uint num7 = Bits.RotateLeft32(a_value4, 30);
    uint a_value7 = (uint) ((int) array[4] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value6, 5) + ((int) num6 ^ (int) a_value5 & ((int) num7 ^ (int) num6))) + num5;
    uint num8 = Bits.RotateLeft32(a_value5, 30);
    uint a_value8 = (uint) ((int) array[5] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value7, 5) + ((int) num7 ^ (int) a_value6 & ((int) num8 ^ (int) num7))) + num6;
    uint num9 = Bits.RotateLeft32(a_value6, 30);
    uint a_value9 = (uint) ((int) array[6] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value8, 5) + ((int) num8 ^ (int) a_value7 & ((int) num9 ^ (int) num8))) + num7;
    uint num10 = Bits.RotateLeft32(a_value7, 30);
    uint a_value10 = (uint) ((int) array[7] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value9, 5) + ((int) num9 ^ (int) a_value8 & ((int) num10 ^ (int) num9))) + num8;
    uint num11 = Bits.RotateLeft32(a_value8, 30);
    uint a_value11 = (uint) ((int) array[8] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value10, 5) + ((int) num10 ^ (int) a_value9 & ((int) num11 ^ (int) num10))) + num9;
    uint num12 = Bits.RotateLeft32(a_value9, 30);
    uint a_value12 = (uint) ((int) array[9] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value11, 5) + ((int) num11 ^ (int) a_value10 & ((int) num12 ^ (int) num11))) + num10;
    uint num13 = Bits.RotateLeft32(a_value10, 30);
    uint a_value13 = (uint) ((int) array[10] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value12, 5) + ((int) num12 ^ (int) a_value11 & ((int) num13 ^ (int) num12))) + num11;
    uint num14 = Bits.RotateLeft32(a_value11, 30);
    uint a_value14 = (uint) ((int) array[11] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value13, 5) + ((int) num13 ^ (int) a_value12 & ((int) num14 ^ (int) num13))) + num12;
    uint num15 = Bits.RotateLeft32(a_value12, 30);
    uint a_value15 = (uint) ((int) array[12] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value14, 5) + ((int) num14 ^ (int) a_value13 & ((int) num15 ^ (int) num14))) + num13;
    uint num16 = Bits.RotateLeft32(a_value13, 30);
    uint a_value16 = (uint) ((int) array[13] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value15, 5) + ((int) num15 ^ (int) a_value14 & ((int) num16 ^ (int) num15))) + num14;
    uint num17 = Bits.RotateLeft32(a_value14, 30);
    uint a_value17 = (uint) ((int) array[14] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value16, 5) + ((int) num16 ^ (int) a_value15 & ((int) num17 ^ (int) num16))) + num15;
    uint num18 = Bits.RotateLeft32(a_value15, 30);
    uint a_value18 = (uint) ((int) array[15] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value17, 5) + ((int) num17 ^ (int) a_value16 & ((int) num18 ^ (int) num17))) + num16;
    uint num19 = Bits.RotateLeft32(a_value16, 30);
    uint a_value19 = (uint) ((int) array[16 /*0x10*/] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value18, 5) + ((int) num18 ^ (int) a_value17 & ((int) num19 ^ (int) num18))) + num17;
    uint num20 = Bits.RotateLeft32(a_value17, 30);
    uint a_value20 = (uint) ((int) array[17] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value19, 5) + ((int) num19 ^ (int) a_value18 & ((int) num20 ^ (int) num19))) + num18;
    uint num21 = Bits.RotateLeft32(a_value18, 30);
    uint a_value21 = (uint) ((int) array[18] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value20, 5) + ((int) num20 ^ (int) a_value19 & ((int) num21 ^ (int) num20))) + num19;
    uint num22 = Bits.RotateLeft32(a_value19, 30);
    uint a_value22 = (uint) ((int) array[19] + (int) SHA0.C1 + (int) Bits.RotateLeft32(a_value21, 5) + ((int) num21 ^ (int) a_value20 & ((int) num22 ^ (int) num21))) + num20;
    uint num23 = Bits.RotateLeft32(a_value20, 30);
    uint a_value23 = (uint) ((int) array[20] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value22, 5) + ((int) a_value21 ^ (int) num23 ^ (int) num22)) + num21;
    uint num24 = Bits.RotateLeft32(a_value21, 30);
    uint a_value24 = (uint) ((int) array[21] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value23, 5) + ((int) a_value22 ^ (int) num24 ^ (int) num23)) + num22;
    uint num25 = Bits.RotateLeft32(a_value22, 30);
    uint a_value25 = (uint) ((int) array[22] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value24, 5) + ((int) a_value23 ^ (int) num25 ^ (int) num24)) + num23;
    uint num26 = Bits.RotateLeft32(a_value23, 30);
    uint a_value26 = (uint) ((int) array[23] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value25, 5) + ((int) a_value24 ^ (int) num26 ^ (int) num25)) + num24;
    uint num27 = Bits.RotateLeft32(a_value24, 30);
    uint a_value27 = (uint) ((int) array[24] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value26, 5) + ((int) a_value25 ^ (int) num27 ^ (int) num26)) + num25;
    uint num28 = Bits.RotateLeft32(a_value25, 30);
    uint a_value28 = (uint) ((int) array[25] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value27, 5) + ((int) a_value26 ^ (int) num28 ^ (int) num27)) + num26;
    uint num29 = Bits.RotateLeft32(a_value26, 30);
    uint a_value29 = (uint) ((int) array[26] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value28, 5) + ((int) a_value27 ^ (int) num29 ^ (int) num28)) + num27;
    uint num30 = Bits.RotateLeft32(a_value27, 30);
    uint a_value30 = (uint) ((int) array[27] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value29, 5) + ((int) a_value28 ^ (int) num30 ^ (int) num29)) + num28;
    uint num31 = Bits.RotateLeft32(a_value28, 30);
    uint a_value31 = (uint) ((int) array[28] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value30, 5) + ((int) a_value29 ^ (int) num31 ^ (int) num30)) + num29;
    uint num32 = Bits.RotateLeft32(a_value29, 30);
    uint a_value32 = (uint) ((int) array[29] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value31, 5) + ((int) a_value30 ^ (int) num32 ^ (int) num31)) + num30;
    uint num33 = Bits.RotateLeft32(a_value30, 30);
    uint a_value33 = (uint) ((int) array[30] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value32, 5) + ((int) a_value31 ^ (int) num33 ^ (int) num32)) + num31;
    uint num34 = Bits.RotateLeft32(a_value31, 30);
    uint a_value34 = (uint) ((int) array[31 /*0x1F*/] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value33, 5) + ((int) a_value32 ^ (int) num34 ^ (int) num33)) + num32;
    uint num35 = Bits.RotateLeft32(a_value32, 30);
    uint a_value35 = (uint) ((int) array[32 /*0x20*/] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value34, 5) + ((int) a_value33 ^ (int) num35 ^ (int) num34)) + num33;
    uint num36 = Bits.RotateLeft32(a_value33, 30);
    uint a_value36 = (uint) ((int) array[33] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value35, 5) + ((int) a_value34 ^ (int) num36 ^ (int) num35)) + num34;
    uint num37 = Bits.RotateLeft32(a_value34, 30);
    uint a_value37 = (uint) ((int) array[34] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value36, 5) + ((int) a_value35 ^ (int) num37 ^ (int) num36)) + num35;
    uint num38 = Bits.RotateLeft32(a_value35, 30);
    uint a_value38 = (uint) ((int) array[35] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value37, 5) + ((int) a_value36 ^ (int) num38 ^ (int) num37)) + num36;
    uint num39 = Bits.RotateLeft32(a_value36, 30);
    uint a_value39 = (uint) ((int) array[36] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value38, 5) + ((int) a_value37 ^ (int) num39 ^ (int) num38)) + num37;
    uint num40 = Bits.RotateLeft32(a_value37, 30);
    uint a_value40 = (uint) ((int) array[37] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value39, 5) + ((int) a_value38 ^ (int) num40 ^ (int) num39)) + num38;
    uint num41 = Bits.RotateLeft32(a_value38, 30);
    uint a_value41 = (uint) ((int) array[38] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value40, 5) + ((int) a_value39 ^ (int) num41 ^ (int) num40)) + num39;
    uint num42 = Bits.RotateLeft32(a_value39, 30);
    uint a_value42 = (uint) ((int) array[39] + (int) SHA0.C2 + (int) Bits.RotateLeft32(a_value41, 5) + ((int) a_value40 ^ (int) num42 ^ (int) num41)) + num40;
    uint num43 = Bits.RotateLeft32(a_value40, 30);
    uint a_value43 = (uint) ((int) array[40] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value42, 5) + ((int) a_value41 & (int) num43 | (int) num42 & ((int) a_value41 | (int) num43))) + num41;
    uint num44 = Bits.RotateLeft32(a_value41, 30);
    uint a_value44 = (uint) ((int) array[41] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value43, 5) + ((int) a_value42 & (int) num44 | (int) num43 & ((int) a_value42 | (int) num44))) + num42;
    uint num45 = Bits.RotateLeft32(a_value42, 30);
    uint a_value45 = (uint) ((int) array[42] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value44, 5) + ((int) a_value43 & (int) num45 | (int) num44 & ((int) a_value43 | (int) num45))) + num43;
    uint num46 = Bits.RotateLeft32(a_value43, 30);
    uint a_value46 = (uint) ((int) array[43] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value45, 5) + ((int) a_value44 & (int) num46 | (int) num45 & ((int) a_value44 | (int) num46))) + num44;
    uint num47 = Bits.RotateLeft32(a_value44, 30);
    uint a_value47 = (uint) ((int) array[44] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value46, 5) + ((int) a_value45 & (int) num47 | (int) num46 & ((int) a_value45 | (int) num47))) + num45;
    uint num48 = Bits.RotateLeft32(a_value45, 30);
    uint a_value48 = (uint) ((int) array[45] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value47, 5) + ((int) a_value46 & (int) num48 | (int) num47 & ((int) a_value46 | (int) num48))) + num46;
    uint num49 = Bits.RotateLeft32(a_value46, 30);
    uint a_value49 = (uint) ((int) array[46] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value48, 5) + ((int) a_value47 & (int) num49 | (int) num48 & ((int) a_value47 | (int) num49))) + num47;
    uint num50 = Bits.RotateLeft32(a_value47, 30);
    uint a_value50 = (uint) ((int) array[47] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value49, 5) + ((int) a_value48 & (int) num50 | (int) num49 & ((int) a_value48 | (int) num50))) + num48;
    uint num51 = Bits.RotateLeft32(a_value48, 30);
    uint a_value51 = (uint) ((int) array[48 /*0x30*/] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value50, 5) + ((int) a_value49 & (int) num51 | (int) num50 & ((int) a_value49 | (int) num51))) + num49;
    uint num52 = Bits.RotateLeft32(a_value49, 30);
    uint a_value52 = (uint) ((int) array[49] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value51, 5) + ((int) a_value50 & (int) num52 | (int) num51 & ((int) a_value50 | (int) num52))) + num50;
    uint num53 = Bits.RotateLeft32(a_value50, 30);
    uint a_value53 = (uint) ((int) array[50] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value52, 5) + ((int) a_value51 & (int) num53 | (int) num52 & ((int) a_value51 | (int) num53))) + num51;
    uint num54 = Bits.RotateLeft32(a_value51, 30);
    uint a_value54 = (uint) ((int) array[51] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value53, 5) + ((int) a_value52 & (int) num54 | (int) num53 & ((int) a_value52 | (int) num54))) + num52;
    uint num55 = Bits.RotateLeft32(a_value52, 30);
    uint a_value55 = (uint) ((int) array[52] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value54, 5) + ((int) a_value53 & (int) num55 | (int) num54 & ((int) a_value53 | (int) num55))) + num53;
    uint num56 = Bits.RotateLeft32(a_value53, 30);
    uint a_value56 = (uint) ((int) array[53] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value55, 5) + ((int) a_value54 & (int) num56 | (int) num55 & ((int) a_value54 | (int) num56))) + num54;
    uint num57 = Bits.RotateLeft32(a_value54, 30);
    uint a_value57 = (uint) ((int) array[54] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value56, 5) + ((int) a_value55 & (int) num57 | (int) num56 & ((int) a_value55 | (int) num57))) + num55;
    uint num58 = Bits.RotateLeft32(a_value55, 30);
    uint a_value58 = (uint) ((int) array[55] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value57, 5) + ((int) a_value56 & (int) num58 | (int) num57 & ((int) a_value56 | (int) num58))) + num56;
    uint num59 = Bits.RotateLeft32(a_value56, 30);
    uint a_value59 = (uint) ((int) array[56] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value58, 5) + ((int) a_value57 & (int) num59 | (int) num58 & ((int) a_value57 | (int) num59))) + num57;
    uint num60 = Bits.RotateLeft32(a_value57, 30);
    uint a_value60 = (uint) ((int) array[57] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value59, 5) + ((int) a_value58 & (int) num60 | (int) num59 & ((int) a_value58 | (int) num60))) + num58;
    uint num61 = Bits.RotateLeft32(a_value58, 30);
    uint a_value61 = (uint) ((int) array[58] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value60, 5) + ((int) a_value59 & (int) num61 | (int) num60 & ((int) a_value59 | (int) num61))) + num59;
    uint num62 = Bits.RotateLeft32(a_value59, 30);
    uint a_value62 = (uint) ((int) array[59] + (int) SHA0.C3 + (int) Bits.RotateLeft32(a_value61, 5) + ((int) a_value60 & (int) num62 | (int) num61 & ((int) a_value60 | (int) num62))) + num60;
    uint num63 = Bits.RotateLeft32(a_value60, 30);
    uint a_value63 = (uint) ((int) array[60] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value62, 5) + ((int) a_value61 ^ (int) num63 ^ (int) num62)) + num61;
    uint num64 = Bits.RotateLeft32(a_value61, 30);
    uint a_value64 = (uint) ((int) array[61] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value63, 5) + ((int) a_value62 ^ (int) num64 ^ (int) num63)) + num62;
    uint num65 = Bits.RotateLeft32(a_value62, 30);
    uint a_value65 = (uint) ((int) array[62] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value64, 5) + ((int) a_value63 ^ (int) num65 ^ (int) num64)) + num63;
    uint num66 = Bits.RotateLeft32(a_value63, 30);
    uint a_value66 = (uint) ((int) array[63 /*0x3F*/] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value65, 5) + ((int) a_value64 ^ (int) num66 ^ (int) num65)) + num64;
    uint num67 = Bits.RotateLeft32(a_value64, 30);
    uint a_value67 = (uint) ((int) array[64 /*0x40*/] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value66, 5) + ((int) a_value65 ^ (int) num67 ^ (int) num66)) + num65;
    uint num68 = Bits.RotateLeft32(a_value65, 30);
    uint a_value68 = (uint) ((int) array[65] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value67, 5) + ((int) a_value66 ^ (int) num68 ^ (int) num67)) + num66;
    uint num69 = Bits.RotateLeft32(a_value66, 30);
    uint a_value69 = (uint) ((int) array[66] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value68, 5) + ((int) a_value67 ^ (int) num69 ^ (int) num68)) + num67;
    uint num70 = Bits.RotateLeft32(a_value67, 30);
    uint a_value70 = (uint) ((int) array[67] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value69, 5) + ((int) a_value68 ^ (int) num70 ^ (int) num69)) + num68;
    uint num71 = Bits.RotateLeft32(a_value68, 30);
    uint a_value71 = (uint) ((int) array[68] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value70, 5) + ((int) a_value69 ^ (int) num71 ^ (int) num70)) + num69;
    uint num72 = Bits.RotateLeft32(a_value69, 30);
    uint a_value72 = (uint) ((int) array[69] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value71, 5) + ((int) a_value70 ^ (int) num72 ^ (int) num71)) + num70;
    uint num73 = Bits.RotateLeft32(a_value70, 30);
    uint a_value73 = (uint) ((int) array[70] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value72, 5) + ((int) a_value71 ^ (int) num73 ^ (int) num72)) + num71;
    uint num74 = Bits.RotateLeft32(a_value71, 30);
    uint a_value74 = (uint) ((int) array[71] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value73, 5) + ((int) a_value72 ^ (int) num74 ^ (int) num73)) + num72;
    uint num75 = Bits.RotateLeft32(a_value72, 30);
    uint a_value75 = (uint) ((int) array[72] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value74, 5) + ((int) a_value73 ^ (int) num75 ^ (int) num74)) + num73;
    uint num76 = Bits.RotateLeft32(a_value73, 30);
    uint a_value76 = (uint) ((int) array[73] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value75, 5) + ((int) a_value74 ^ (int) num76 ^ (int) num75)) + num74;
    uint num77 = Bits.RotateLeft32(a_value74, 30);
    uint a_value77 = (uint) ((int) array[74] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value76, 5) + ((int) a_value75 ^ (int) num77 ^ (int) num76)) + num75;
    uint num78 = Bits.RotateLeft32(a_value75, 30);
    uint a_value78 = (uint) ((int) array[75] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value77, 5) + ((int) a_value76 ^ (int) num78 ^ (int) num77)) + num76;
    uint num79 = Bits.RotateLeft32(a_value76, 30);
    uint a_value79 = (uint) ((int) array[76] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value78, 5) + ((int) a_value77 ^ (int) num79 ^ (int) num78)) + num77;
    uint num80 = Bits.RotateLeft32(a_value77, 30);
    uint a_value80 = (uint) ((int) array[77] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value79, 5) + ((int) a_value78 ^ (int) num80 ^ (int) num79)) + num78;
    uint num81 = Bits.RotateLeft32(a_value78, 30);
    uint a_value81 = (uint) ((int) array[78] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value80, 5) + ((int) a_value79 ^ (int) num81 ^ (int) num80)) + num79;
    uint num82 = Bits.RotateLeft32(a_value79, 30);
    uint num83 = (uint) ((int) array[79] + (int) SHA0.C4 + (int) Bits.RotateLeft32(a_value81, 5) + ((int) a_value80 ^ (int) num82 ^ (int) num81)) + num80;
    uint num84 = Bits.RotateLeft32(a_value80, 30);
    this.state[0] = this.state[0] + num83;
    this.state[1] = this.state[1] + a_value81;
    this.state[2] = this.state[2] + num84;
    this.state[3] = this.state[3] + num82;
    this.state[4] = this.state[4] + num81;
    Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
  }
}
