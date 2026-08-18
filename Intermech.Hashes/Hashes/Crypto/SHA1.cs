// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA1
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA1 : SHA0
{
  public override IHash Clone()
  {
    SHA1 shA1 = new SHA1();
    shA1.buffer = this.buffer.Clone();
    shA1.processed_bytes = this.processed_bytes;
    shA1.state = this.state.DeepCopy();
    shA1.BufferSize = this.BufferSize;
    return (IHash) shA1;
  }

  protected override unsafe void Expand(uint* a_data)
  {
    uint a_value1 = a_data[13] ^ a_data[8] ^ a_data[2] ^ *a_data;
    a_data[16 /*0x10*/] = Bits.RotateLeft32(a_value1, 1);
    uint a_value2 = a_data[14] ^ a_data[9] ^ a_data[3] ^ a_data[1];
    a_data[17] = Bits.RotateLeft32(a_value2, 1);
    uint a_value3 = a_data[15] ^ a_data[10] ^ a_data[4] ^ a_data[2];
    a_data[18] = Bits.RotateLeft32(a_value3, 1);
    uint a_value4 = a_data[16 /*0x10*/] ^ a_data[11] ^ a_data[5] ^ a_data[3];
    a_data[19] = Bits.RotateLeft32(a_value4, 1);
    uint a_value5 = a_data[17] ^ a_data[12] ^ a_data[6] ^ a_data[4];
    a_data[20] = Bits.RotateLeft32(a_value5, 1);
    uint a_value6 = a_data[18] ^ a_data[13] ^ a_data[7] ^ a_data[5];
    a_data[21] = Bits.RotateLeft32(a_value6, 1);
    uint a_value7 = a_data[19] ^ a_data[14] ^ a_data[8] ^ a_data[6];
    a_data[22] = Bits.RotateLeft32(a_value7, 1);
    uint a_value8 = a_data[20] ^ a_data[15] ^ a_data[9] ^ a_data[7];
    a_data[23] = Bits.RotateLeft32(a_value8, 1);
    uint a_value9 = a_data[21] ^ a_data[16 /*0x10*/] ^ a_data[10] ^ a_data[8];
    a_data[24] = Bits.RotateLeft32(a_value9, 1);
    uint a_value10 = a_data[22] ^ a_data[17] ^ a_data[11] ^ a_data[9];
    a_data[25] = Bits.RotateLeft32(a_value10, 1);
    uint a_value11 = a_data[23] ^ a_data[18] ^ a_data[12] ^ a_data[10];
    a_data[26] = Bits.RotateLeft32(a_value11, 1);
    uint a_value12 = a_data[24] ^ a_data[19] ^ a_data[13] ^ a_data[11];
    a_data[27] = Bits.RotateLeft32(a_value12, 1);
    uint a_value13 = a_data[25] ^ a_data[20] ^ a_data[14] ^ a_data[12];
    a_data[28] = Bits.RotateLeft32(a_value13, 1);
    uint a_value14 = a_data[26] ^ a_data[21] ^ a_data[15] ^ a_data[13];
    a_data[29] = Bits.RotateLeft32(a_value14, 1);
    uint a_value15 = a_data[27] ^ a_data[22] ^ a_data[16 /*0x10*/] ^ a_data[14];
    a_data[30] = Bits.RotateLeft32(a_value15, 1);
    uint a_value16 = a_data[28] ^ a_data[23] ^ a_data[17] ^ a_data[15];
    a_data[31 /*0x1F*/] = Bits.RotateLeft32(a_value16, 1);
    uint a_value17 = a_data[29] ^ a_data[24] ^ a_data[18] ^ a_data[16 /*0x10*/];
    a_data[32 /*0x20*/] = Bits.RotateLeft32(a_value17, 1);
    uint a_value18 = a_data[30] ^ a_data[25] ^ a_data[19] ^ a_data[17];
    a_data[33] = Bits.RotateLeft32(a_value18, 1);
    uint a_value19 = a_data[31 /*0x1F*/] ^ a_data[26] ^ a_data[20] ^ a_data[18];
    a_data[34] = Bits.RotateLeft32(a_value19, 1);
    uint a_value20 = a_data[32 /*0x20*/] ^ a_data[27] ^ a_data[21] ^ a_data[19];
    a_data[35] = Bits.RotateLeft32(a_value20, 1);
    uint a_value21 = a_data[33] ^ a_data[28] ^ a_data[22] ^ a_data[20];
    a_data[36] = Bits.RotateLeft32(a_value21, 1);
    uint a_value22 = a_data[34] ^ a_data[29] ^ a_data[23] ^ a_data[21];
    a_data[37] = Bits.RotateLeft32(a_value22, 1);
    uint a_value23 = a_data[35] ^ a_data[30] ^ a_data[24] ^ a_data[22];
    a_data[38] = Bits.RotateLeft32(a_value23, 1);
    uint a_value24 = a_data[36] ^ a_data[31 /*0x1F*/] ^ a_data[25] ^ a_data[23];
    a_data[39] = Bits.RotateLeft32(a_value24, 1);
    uint a_value25 = a_data[37] ^ a_data[32 /*0x20*/] ^ a_data[26] ^ a_data[24];
    a_data[40] = Bits.RotateLeft32(a_value25, 1);
    uint a_value26 = a_data[38] ^ a_data[33] ^ a_data[27] ^ a_data[25];
    a_data[41] = Bits.RotateLeft32(a_value26, 1);
    uint a_value27 = a_data[39] ^ a_data[34] ^ a_data[28] ^ a_data[26];
    a_data[42] = Bits.RotateLeft32(a_value27, 1);
    uint a_value28 = a_data[40] ^ a_data[35] ^ a_data[29] ^ a_data[27];
    a_data[43] = Bits.RotateLeft32(a_value28, 1);
    uint a_value29 = a_data[41] ^ a_data[36] ^ a_data[30] ^ a_data[28];
    a_data[44] = Bits.RotateLeft32(a_value29, 1);
    uint a_value30 = a_data[42] ^ a_data[37] ^ a_data[31 /*0x1F*/] ^ a_data[29];
    a_data[45] = Bits.RotateLeft32(a_value30, 1);
    uint a_value31 = a_data[43] ^ a_data[38] ^ a_data[32 /*0x20*/] ^ a_data[30];
    a_data[46] = Bits.RotateLeft32(a_value31, 1);
    uint a_value32 = a_data[44] ^ a_data[39] ^ a_data[33] ^ a_data[31 /*0x1F*/];
    a_data[47] = Bits.RotateLeft32(a_value32, 1);
    uint a_value33 = a_data[45] ^ a_data[40] ^ a_data[34] ^ a_data[32 /*0x20*/];
    a_data[48 /*0x30*/] = Bits.RotateLeft32(a_value33, 1);
    uint a_value34 = a_data[46] ^ a_data[41] ^ a_data[35] ^ a_data[33];
    a_data[49] = Bits.RotateLeft32(a_value34, 1);
    uint a_value35 = a_data[47] ^ a_data[42] ^ a_data[36] ^ a_data[34];
    a_data[50] = Bits.RotateLeft32(a_value35, 1);
    uint a_value36 = a_data[48 /*0x30*/] ^ a_data[43] ^ a_data[37] ^ a_data[35];
    a_data[51] = Bits.RotateLeft32(a_value36, 1);
    uint a_value37 = a_data[49] ^ a_data[44] ^ a_data[38] ^ a_data[36];
    a_data[52] = Bits.RotateLeft32(a_value37, 1);
    uint a_value38 = a_data[50] ^ a_data[45] ^ a_data[39] ^ a_data[37];
    a_data[53] = Bits.RotateLeft32(a_value38, 1);
    uint a_value39 = a_data[51] ^ a_data[46] ^ a_data[40] ^ a_data[38];
    a_data[54] = Bits.RotateLeft32(a_value39, 1);
    uint a_value40 = a_data[52] ^ a_data[47] ^ a_data[41] ^ a_data[39];
    a_data[55] = Bits.RotateLeft32(a_value40, 1);
    uint a_value41 = a_data[53] ^ a_data[48 /*0x30*/] ^ a_data[42] ^ a_data[40];
    a_data[56] = Bits.RotateLeft32(a_value41, 1);
    uint a_value42 = a_data[54] ^ a_data[49] ^ a_data[43] ^ a_data[41];
    a_data[57] = Bits.RotateLeft32(a_value42, 1);
    uint a_value43 = a_data[55] ^ a_data[50] ^ a_data[44] ^ a_data[42];
    a_data[58] = Bits.RotateLeft32(a_value43, 1);
    uint a_value44 = a_data[56] ^ a_data[51] ^ a_data[45] ^ a_data[43];
    a_data[59] = Bits.RotateLeft32(a_value44, 1);
    uint a_value45 = a_data[57] ^ a_data[52] ^ a_data[46] ^ a_data[44];
    a_data[60] = Bits.RotateLeft32(a_value45, 1);
    uint a_value46 = a_data[58] ^ a_data[53] ^ a_data[47] ^ a_data[45];
    a_data[61] = Bits.RotateLeft32(a_value46, 1);
    uint a_value47 = a_data[59] ^ a_data[54] ^ a_data[48 /*0x30*/] ^ a_data[46];
    a_data[62] = Bits.RotateLeft32(a_value47, 1);
    uint a_value48 = a_data[60] ^ a_data[55] ^ a_data[49] ^ a_data[47];
    a_data[63 /*0x3F*/] = Bits.RotateLeft32(a_value48, 1);
    uint a_value49 = a_data[61] ^ a_data[56] ^ a_data[50] ^ a_data[48 /*0x30*/];
    a_data[64 /*0x40*/] = Bits.RotateLeft32(a_value49, 1);
    uint a_value50 = a_data[62] ^ a_data[57] ^ a_data[51] ^ a_data[49];
    a_data[65] = Bits.RotateLeft32(a_value50, 1);
    uint a_value51 = a_data[63 /*0x3F*/] ^ a_data[58] ^ a_data[52] ^ a_data[50];
    a_data[66] = Bits.RotateLeft32(a_value51, 1);
    uint a_value52 = a_data[64 /*0x40*/] ^ a_data[59] ^ a_data[53] ^ a_data[51];
    a_data[67] = Bits.RotateLeft32(a_value52, 1);
    uint a_value53 = a_data[65] ^ a_data[60] ^ a_data[54] ^ a_data[52];
    a_data[68] = Bits.RotateLeft32(a_value53, 1);
    uint a_value54 = a_data[66] ^ a_data[61] ^ a_data[55] ^ a_data[53];
    a_data[69] = Bits.RotateLeft32(a_value54, 1);
    uint a_value55 = a_data[67] ^ a_data[62] ^ a_data[56] ^ a_data[54];
    a_data[70] = Bits.RotateLeft32(a_value55, 1);
    uint a_value56 = a_data[68] ^ a_data[63 /*0x3F*/] ^ a_data[57] ^ a_data[55];
    a_data[71] = Bits.RotateLeft32(a_value56, 1);
    uint a_value57 = a_data[69] ^ a_data[64 /*0x40*/] ^ a_data[58] ^ a_data[56];
    a_data[72] = Bits.RotateLeft32(a_value57, 1);
    uint a_value58 = a_data[70] ^ a_data[65] ^ a_data[59] ^ a_data[57];
    a_data[73] = Bits.RotateLeft32(a_value58, 1);
    uint a_value59 = a_data[71] ^ a_data[66] ^ a_data[60] ^ a_data[58];
    a_data[74] = Bits.RotateLeft32(a_value59, 1);
    uint a_value60 = a_data[72] ^ a_data[67] ^ a_data[61] ^ a_data[59];
    a_data[75] = Bits.RotateLeft32(a_value60, 1);
    uint a_value61 = a_data[73] ^ a_data[68] ^ a_data[62] ^ a_data[60];
    a_data[76] = Bits.RotateLeft32(a_value61, 1);
    uint a_value62 = a_data[74] ^ a_data[69] ^ a_data[63 /*0x3F*/] ^ a_data[61];
    a_data[77] = Bits.RotateLeft32(a_value62, 1);
    uint a_value63 = a_data[75] ^ a_data[70] ^ a_data[64 /*0x40*/] ^ a_data[62];
    a_data[78] = Bits.RotateLeft32(a_value63, 1);
    uint a_value64 = a_data[76] ^ a_data[71] ^ a_data[65] ^ a_data[63 /*0x3F*/];
    a_data[79] = Bits.RotateLeft32(a_value64, 1);
  }
}
