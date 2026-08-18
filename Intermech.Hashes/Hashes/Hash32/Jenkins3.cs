// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.Jenkins3
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class Jenkins3 : MultipleTransformNonBlock, IHash32, IHash, ITransformBlock
{
  private int InitialValue;

  public Jenkins3(int initialValue = 0)
    : base(4, 12)
  {
    this.InitialValue = initialValue;
  }

  public override IHash Clone()
  {
    Jenkins3 jenkins3 = new Jenkins3();
    jenkins3.Buffer = new MemoryStream();
    jenkins3.InitialValue = this.InitialValue;
    byte[] array = this.Buffer.ToArray();
    jenkins3.Buffer.Write(array, 0, array.Length);
    jenkins3.Buffer.Position = this.Buffer.Position;
    jenkins3.BufferSize = this.BufferSize;
    return (IHash) jenkins3;
  }

  protected override IHashResult ComputeAggregatedBytes(byte[] a_data)
  {
    if (a_data.Empty())
      return (IHashResult) new HashResult(0U);
    int length = a_data.Length;
    uint num1 = (uint) (length - 559038737 + this.InitialValue);
    uint a_value1 = num1;
    uint a_hash = a_value1;
    if (length == 0)
      return (IHashResult) new HashResult(a_hash);
    int index1 = 0;
    for (; length > 12; length -= 12)
    {
      int num2 = (int) a_data[index1];
      int index2 = index1 + 1;
      int num3 = (int) a_data[index2] << 8;
      int index3 = index2 + 1;
      int num4 = (int) a_data[index3] << 16 /*0x10*/;
      int index4 = index3 + 1;
      int num5 = (int) a_data[index4] << 24;
      int index5 = index4 + 1;
      uint num6 = num1 + (uint) (num2 | num3 | num4 | num5);
      int num7 = (int) a_data[index5];
      int index6 = index5 + 1;
      int num8 = (int) a_data[index6] << 8;
      int index7 = index6 + 1;
      int num9 = (int) a_data[index7] << 16 /*0x10*/;
      int index8 = index7 + 1;
      int num10 = (int) a_data[index8] << 24;
      int index9 = index8 + 1;
      uint num11 = a_value1 + (uint) (num7 | num8 | num9 | num10);
      int num12 = (int) a_data[index9];
      int index10 = index9 + 1;
      int num13 = (int) a_data[index10] << 8;
      int index11 = index10 + 1;
      int num14 = (int) a_data[index11] << 16 /*0x10*/;
      int index12 = index11 + 1;
      int num15 = (int) a_data[index12] << 24;
      index1 = index12 + 1;
      uint a_value2 = a_hash + (uint) (num12 | num13 | num14 | num15);
      uint a_value3 = num6 - a_value2 ^ Bits.RotateLeft32(a_value2, 4);
      uint num16 = a_value2 + num11;
      uint a_value4 = num11 - a_value3 ^ Bits.RotateLeft32(a_value3, 6);
      uint num17 = a_value3 + num16;
      uint a_value5 = num16 - a_value4 ^ Bits.RotateLeft32(a_value4, 8);
      uint num18 = a_value4 + num17;
      uint a_value6 = num17 - a_value5 ^ Bits.RotateLeft32(a_value5, 16 /*0x10*/);
      uint num19 = a_value5 + num18;
      uint a_value7 = num18 - a_value6 ^ Bits.RotateLeft32(a_value6, 19);
      num1 = a_value6 + num19;
      a_hash = num19 - a_value7 ^ Bits.RotateLeft32(a_value7, 4);
      a_value1 = a_value7 + num1;
    }
    switch (length - 1)
    {
      case 0:
        int num20 = (int) a_data[index1];
        num1 += (uint) num20;
        break;
      case 1:
        int num21 = (int) a_data[index1];
        int index13 = index1 + 1;
        int num22 = (int) a_data[index13] << 8;
        num1 += (uint) (num21 | num22);
        break;
      case 2:
        int num23 = (int) a_data[index1];
        int index14 = index1 + 1;
        int num24 = (int) a_data[index14] << 8;
        int index15 = index14 + 1;
        int num25 = (int) a_data[index15] << 16 /*0x10*/;
        num1 += (uint) (num23 | num24 | num25);
        break;
      case 3:
        int num26 = (int) a_data[index1];
        int index16 = index1 + 1;
        int num27 = (int) a_data[index16] << 8;
        int index17 = index16 + 1;
        int num28 = (int) a_data[index17] << 16 /*0x10*/;
        int index18 = index17 + 1;
        int num29 = (int) a_data[index18] << 24;
        num1 += (uint) (num26 | num27 | num28 | num29);
        break;
      case 4:
        int num30 = (int) a_data[index1];
        int index19 = index1 + 1;
        int num31 = (int) a_data[index19] << 8;
        int index20 = index19 + 1;
        int num32 = (int) a_data[index20] << 16 /*0x10*/;
        int index21 = index20 + 1;
        int num33 = (int) a_data[index21] << 24;
        int index22 = index21 + 1;
        num1 += (uint) (num30 | num31 | num32 | num33);
        int num34 = (int) a_data[index22];
        a_value1 += (uint) num34;
        break;
      case 5:
        int num35 = (int) a_data[index1];
        int index23 = index1 + 1;
        int num36 = (int) a_data[index23] << 8;
        int index24 = index23 + 1;
        int num37 = (int) a_data[index24] << 16 /*0x10*/;
        int index25 = index24 + 1;
        int num38 = (int) a_data[index25] << 24;
        int index26 = index25 + 1;
        num1 += (uint) (num35 | num36 | num37 | num38);
        int num39 = (int) a_data[index26];
        int index27 = index26 + 1;
        int num40 = (int) a_data[index27] << 8;
        a_value1 += (uint) (num39 | num40);
        break;
      case 6:
        int num41 = (int) a_data[index1];
        int index28 = index1 + 1;
        int num42 = (int) a_data[index28] << 8;
        int index29 = index28 + 1;
        int num43 = (int) a_data[index29] << 16 /*0x10*/;
        int index30 = index29 + 1;
        int num44 = (int) a_data[index30] << 24;
        int index31 = index30 + 1;
        num1 += (uint) (num41 | num42 | num43 | num44);
        int num45 = (int) a_data[index31];
        int index32 = index31 + 1;
        int num46 = (int) a_data[index32] << 8;
        int index33 = index32 + 1;
        int num47 = (int) a_data[index33] << 16 /*0x10*/;
        a_value1 += (uint) (num45 | num46 | num47);
        break;
      case 7:
        int num48 = (int) a_data[index1];
        int index34 = index1 + 1;
        int num49 = (int) a_data[index34] << 8;
        int index35 = index34 + 1;
        int num50 = (int) a_data[index35] << 16 /*0x10*/;
        int index36 = index35 + 1;
        int num51 = (int) a_data[index36] << 24;
        int index37 = index36 + 1;
        num1 += (uint) (num48 | num49 | num50 | num51);
        int num52 = (int) a_data[index37];
        int index38 = index37 + 1;
        int num53 = (int) a_data[index38] << 8;
        int index39 = index38 + 1;
        int num54 = (int) a_data[index39] << 16 /*0x10*/;
        int index40 = index39 + 1;
        int num55 = (int) a_data[index40] << 24;
        a_value1 += (uint) (num52 | num53 | num54 | num55);
        break;
      case 8:
        int num56 = (int) a_data[index1];
        int index41 = index1 + 1;
        int num57 = (int) a_data[index41] << 8;
        int index42 = index41 + 1;
        int num58 = (int) a_data[index42] << 16 /*0x10*/;
        int index43 = index42 + 1;
        int num59 = (int) a_data[index43] << 24;
        int index44 = index43 + 1;
        num1 += (uint) (num56 | num57 | num58 | num59);
        int num60 = (int) a_data[index44];
        int index45 = index44 + 1;
        int num61 = (int) a_data[index45] << 8;
        int index46 = index45 + 1;
        int num62 = (int) a_data[index46] << 16 /*0x10*/;
        int index47 = index46 + 1;
        int num63 = (int) a_data[index47] << 24;
        int index48 = index47 + 1;
        a_value1 += (uint) (num60 | num61 | num62 | num63);
        int num64 = (int) a_data[index48];
        a_hash += (uint) num64;
        break;
      case 9:
        int num65 = (int) a_data[index1];
        int index49 = index1 + 1;
        int num66 = (int) a_data[index49] << 8;
        int index50 = index49 + 1;
        int num67 = (int) a_data[index50] << 16 /*0x10*/;
        int index51 = index50 + 1;
        int num68 = (int) a_data[index51] << 24;
        int index52 = index51 + 1;
        num1 += (uint) (num65 | num66 | num67 | num68);
        int num69 = (int) a_data[index52];
        int index53 = index52 + 1;
        int num70 = (int) a_data[index53] << 8;
        int index54 = index53 + 1;
        int num71 = (int) a_data[index54] << 16 /*0x10*/;
        int index55 = index54 + 1;
        int num72 = (int) a_data[index55] << 24;
        int index56 = index55 + 1;
        a_value1 += (uint) (num69 | num70 | num71 | num72);
        int num73 = (int) a_data[index56];
        int index57 = index56 + 1;
        int num74 = (int) a_data[index57] << 8;
        a_hash += (uint) (num73 | num74);
        break;
      case 10:
        int num75 = (int) a_data[index1];
        int index58 = index1 + 1;
        int num76 = (int) a_data[index58] << 8;
        int index59 = index58 + 1;
        int num77 = (int) a_data[index59] << 16 /*0x10*/;
        int index60 = index59 + 1;
        int num78 = (int) a_data[index60] << 24;
        int index61 = index60 + 1;
        num1 += (uint) (num75 | num76 | num77 | num78);
        int num79 = (int) a_data[index61];
        int index62 = index61 + 1;
        int num80 = (int) a_data[index62] << 8;
        int index63 = index62 + 1;
        int num81 = (int) a_data[index63] << 16 /*0x10*/;
        int index64 = index63 + 1;
        int num82 = (int) a_data[index64] << 24;
        int index65 = index64 + 1;
        a_value1 += (uint) (num79 | num80 | num81 | num82);
        int num83 = (int) a_data[index65];
        int index66 = index65 + 1;
        int num84 = (int) a_data[index66] << 8;
        int index67 = index66 + 1;
        int num85 = (int) a_data[index67] << 16 /*0x10*/;
        a_hash += (uint) (num83 | num84 | num85);
        break;
      case 11:
        int num86 = (int) a_data[index1];
        int index68 = index1 + 1;
        int num87 = (int) a_data[index68] << 8;
        int index69 = index68 + 1;
        int num88 = (int) a_data[index69] << 16 /*0x10*/;
        int index70 = index69 + 1;
        int num89 = (int) a_data[index70] << 24;
        int index71 = index70 + 1;
        num1 += (uint) (num86 | num87 | num88 | num89);
        int num90 = (int) a_data[index71];
        int index72 = index71 + 1;
        int num91 = (int) a_data[index72] << 8;
        int index73 = index72 + 1;
        int num92 = (int) a_data[index73] << 16 /*0x10*/;
        int index74 = index73 + 1;
        int num93 = (int) a_data[index74] << 24;
        int index75 = index74 + 1;
        a_value1 += (uint) (num90 | num91 | num92 | num93);
        int num94 = (int) a_data[index75];
        int index76 = index75 + 1;
        int num95 = (int) a_data[index76] << 8;
        int index77 = index76 + 1;
        int num96 = (int) a_data[index77] << 16 /*0x10*/;
        int index78 = index77 + 1;
        int num97 = (int) a_data[index78] << 24;
        a_hash += (uint) (num94 | num95 | num96 | num97);
        break;
    }
    uint a_value8 = (a_hash ^ a_value1) - Bits.RotateLeft32(a_value1, 14);
    uint a_value9 = (num1 ^ a_value8) - Bits.RotateLeft32(a_value8, 11);
    uint a_value10 = (a_value1 ^ a_value9) - Bits.RotateLeft32(a_value9, 25);
    uint a_value11 = (a_value8 ^ a_value10) - Bits.RotateLeft32(a_value10, 16 /*0x10*/);
    uint a_value12 = (a_value9 ^ a_value11) - Bits.RotateLeft32(a_value11, 4);
    uint a_value13 = (a_value10 ^ a_value12) - Bits.RotateLeft32(a_value12, 14);
    return (IHashResult) new HashResult((a_value11 ^ a_value13) - Bits.RotateLeft32(a_value13, 24));
  }
}
