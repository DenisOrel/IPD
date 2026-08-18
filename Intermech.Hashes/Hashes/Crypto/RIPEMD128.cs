// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.RIPEMD128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class RIPEMD128 : MDBase, ITransformBlock
{
  private uint[] data;

  public RIPEMD128()
    : base(4, 16 /*0x10*/)
  {
    this.data = new uint[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    RIPEMD128 ripemD128 = new RIPEMD128();
    ripemD128.buffer = this.buffer.Clone();
    ripemD128.processed_bytes = this.processed_bytes;
    ripemD128.state = this.state.DeepCopy();
    ripemD128.BufferSize = this.BufferSize;
    return (IHash) ripemD128;
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    fixed (uint* dest = this.data)
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
    uint num1 = this.state[0];
    uint num2 = this.state[1];
    uint num3 = this.state[2];
    uint num4 = this.state[3];
    uint num5 = num1;
    uint num6 = num2;
    uint num7 = num3;
    uint num8 = num4;
    uint num9 = Bits.RotateLeft32(num1 + (this.data[0] + (num2 ^ num3 ^ num4)), 11);
    uint num10 = Bits.RotateLeft32(num4 + (this.data[1] + (num9 ^ num2 ^ num3)), 14);
    uint num11 = Bits.RotateLeft32(num3 + (this.data[2] + (num10 ^ num9 ^ num2)), 15);
    uint num12 = Bits.RotateLeft32(num2 + (this.data[3] + (num11 ^ num10 ^ num9)), 12);
    uint num13 = Bits.RotateLeft32(num9 + (this.data[4] + (num12 ^ num11 ^ num10)), 5);
    uint num14 = Bits.RotateLeft32(num10 + (this.data[5] + (num13 ^ num12 ^ num11)), 8);
    uint num15 = Bits.RotateLeft32(num11 + (this.data[6] + (num14 ^ num13 ^ num12)), 7);
    uint num16 = Bits.RotateLeft32(num12 + (this.data[7] + (num15 ^ num14 ^ num13)), 9);
    uint num17 = Bits.RotateLeft32(num13 + (this.data[8] + (num16 ^ num15 ^ num14)), 11);
    uint num18 = Bits.RotateLeft32(num14 + (this.data[9] + (num17 ^ num16 ^ num15)), 13);
    uint num19 = Bits.RotateLeft32(num15 + (this.data[10] + (num18 ^ num17 ^ num16)), 14);
    uint num20 = Bits.RotateLeft32(num16 + (this.data[11] + (num19 ^ num18 ^ num17)), 15);
    uint num21 = Bits.RotateLeft32(num17 + (this.data[12] + (num20 ^ num19 ^ num18)), 6);
    uint num22 = Bits.RotateLeft32(num18 + (this.data[13] + (num21 ^ num20 ^ num19)), 7);
    uint num23 = Bits.RotateLeft32(num19 + (this.data[14] + (num22 ^ num21 ^ num20)), 9);
    uint num24 = Bits.RotateLeft32(num20 + (this.data[15] + (num23 ^ num22 ^ num21)), 8);
    uint num25 = Bits.RotateLeft32(num21 + (uint) ((int) this.data[7] + (int) MDBase.C2 + ((int) num24 & (int) num23 | ~(int) num24 & (int) num22)), 7);
    uint num26 = Bits.RotateLeft32(num22 + (uint) ((int) this.data[4] + (int) MDBase.C2 + ((int) num25 & (int) num24 | ~(int) num25 & (int) num23)), 6);
    uint num27 = Bits.RotateLeft32(num23 + (uint) ((int) this.data[13] + (int) MDBase.C2 + ((int) num26 & (int) num25 | ~(int) num26 & (int) num24)), 8);
    uint num28 = Bits.RotateLeft32(num24 + (uint) ((int) this.data[1] + (int) MDBase.C2 + ((int) num27 & (int) num26 | ~(int) num27 & (int) num25)), 13);
    uint num29 = Bits.RotateLeft32(num25 + (uint) ((int) this.data[10] + (int) MDBase.C2 + ((int) num28 & (int) num27 | ~(int) num28 & (int) num26)), 11);
    uint num30 = Bits.RotateLeft32(num26 + (uint) ((int) this.data[6] + (int) MDBase.C2 + ((int) num29 & (int) num28 | ~(int) num29 & (int) num27)), 9);
    uint num31 = Bits.RotateLeft32(num27 + (uint) ((int) this.data[15] + (int) MDBase.C2 + ((int) num30 & (int) num29 | ~(int) num30 & (int) num28)), 7);
    uint num32 = Bits.RotateLeft32(num28 + (uint) ((int) this.data[3] + (int) MDBase.C2 + ((int) num31 & (int) num30 | ~(int) num31 & (int) num29)), 15);
    uint num33 = Bits.RotateLeft32(num29 + (uint) ((int) this.data[12] + (int) MDBase.C2 + ((int) num32 & (int) num31 | ~(int) num32 & (int) num30)), 7);
    uint num34 = Bits.RotateLeft32(num30 + (uint) ((int) this.data[0] + (int) MDBase.C2 + ((int) num33 & (int) num32 | ~(int) num33 & (int) num31)), 12);
    uint num35 = Bits.RotateLeft32(num31 + (uint) ((int) this.data[9] + (int) MDBase.C2 + ((int) num34 & (int) num33 | ~(int) num34 & (int) num32)), 15);
    uint num36 = Bits.RotateLeft32(num32 + (uint) ((int) this.data[5] + (int) MDBase.C2 + ((int) num35 & (int) num34 | ~(int) num35 & (int) num33)), 9);
    uint num37 = Bits.RotateLeft32(num33 + (uint) ((int) this.data[2] + (int) MDBase.C2 + ((int) num36 & (int) num35 | ~(int) num36 & (int) num34)), 11);
    uint num38 = Bits.RotateLeft32(num34 + (uint) ((int) this.data[14] + (int) MDBase.C2 + ((int) num37 & (int) num36 | ~(int) num37 & (int) num35)), 7);
    uint num39 = Bits.RotateLeft32(num35 + (uint) ((int) this.data[11] + (int) MDBase.C2 + ((int) num38 & (int) num37 | ~(int) num38 & (int) num36)), 13);
    uint num40 = Bits.RotateLeft32(num36 + (uint) ((int) this.data[8] + (int) MDBase.C2 + ((int) num39 & (int) num38 | ~(int) num39 & (int) num37)), 12);
    uint num41 = Bits.RotateLeft32(num37 + (uint) ((int) this.data[3] + (int) MDBase.C4 + (((int) num40 | ~(int) num39) ^ (int) num38)), 11);
    uint num42 = Bits.RotateLeft32(num38 + (uint) ((int) this.data[10] + (int) MDBase.C4 + (((int) num41 | ~(int) num40) ^ (int) num39)), 13);
    uint num43 = Bits.RotateLeft32(num39 + (uint) ((int) this.data[14] + (int) MDBase.C4 + (((int) num42 | ~(int) num41) ^ (int) num40)), 6);
    uint num44 = Bits.RotateLeft32(num40 + (uint) ((int) this.data[4] + (int) MDBase.C4 + (((int) num43 | ~(int) num42) ^ (int) num41)), 7);
    uint num45 = Bits.RotateLeft32(num41 + (uint) ((int) this.data[9] + (int) MDBase.C4 + (((int) num44 | ~(int) num43) ^ (int) num42)), 14);
    uint num46 = Bits.RotateLeft32(num42 + (uint) ((int) this.data[15] + (int) MDBase.C4 + (((int) num45 | ~(int) num44) ^ (int) num43)), 9);
    uint num47 = Bits.RotateLeft32(num43 + (uint) ((int) this.data[8] + (int) MDBase.C4 + (((int) num46 | ~(int) num45) ^ (int) num44)), 13);
    uint num48 = Bits.RotateLeft32(num44 + (uint) ((int) this.data[1] + (int) MDBase.C4 + (((int) num47 | ~(int) num46) ^ (int) num45)), 15);
    uint num49 = Bits.RotateLeft32(num45 + (uint) ((int) this.data[2] + (int) MDBase.C4 + (((int) num48 | ~(int) num47) ^ (int) num46)), 14);
    uint num50 = Bits.RotateLeft32(num46 + (uint) ((int) this.data[7] + (int) MDBase.C4 + (((int) num49 | ~(int) num48) ^ (int) num47)), 8);
    uint num51 = Bits.RotateLeft32(num47 + (uint) ((int) this.data[0] + (int) MDBase.C4 + (((int) num50 | ~(int) num49) ^ (int) num48)), 13);
    uint num52 = Bits.RotateLeft32(num48 + (uint) ((int) this.data[6] + (int) MDBase.C4 + (((int) num51 | ~(int) num50) ^ (int) num49)), 6);
    uint num53 = Bits.RotateLeft32(num49 + (uint) ((int) this.data[13] + (int) MDBase.C4 + (((int) num52 | ~(int) num51) ^ (int) num50)), 5);
    uint num54 = Bits.RotateLeft32(num50 + (uint) ((int) this.data[11] + (int) MDBase.C4 + (((int) num53 | ~(int) num52) ^ (int) num51)), 12);
    uint num55 = Bits.RotateLeft32(num51 + (uint) ((int) this.data[5] + (int) MDBase.C4 + (((int) num54 | ~(int) num53) ^ (int) num52)), 7);
    uint num56 = Bits.RotateLeft32(num52 + (uint) ((int) this.data[12] + (int) MDBase.C4 + (((int) num55 | ~(int) num54) ^ (int) num53)), 5);
    uint num57 = Bits.RotateLeft32(num53 + (uint) ((int) this.data[1] + (int) MDBase.C6 + ((int) num56 & (int) num54 | (int) num55 & ~(int) num54)), 11);
    uint num58 = Bits.RotateLeft32(num54 + (uint) ((int) this.data[9] + (int) MDBase.C6 + ((int) num57 & (int) num55 | (int) num56 & ~(int) num55)), 12);
    uint num59 = Bits.RotateLeft32(num55 + (uint) ((int) this.data[11] + (int) MDBase.C6 + ((int) num58 & (int) num56 | (int) num57 & ~(int) num56)), 14);
    uint num60 = Bits.RotateLeft32(num56 + (uint) ((int) this.data[10] + (int) MDBase.C6 + ((int) num59 & (int) num57 | (int) num58 & ~(int) num57)), 15);
    uint num61 = Bits.RotateLeft32(num57 + (uint) ((int) this.data[0] + (int) MDBase.C6 + ((int) num60 & (int) num58 | (int) num59 & ~(int) num58)), 14);
    uint num62 = Bits.RotateLeft32(num58 + (uint) ((int) this.data[8] + (int) MDBase.C6 + ((int) num61 & (int) num59 | (int) num60 & ~(int) num59)), 15);
    uint num63 = Bits.RotateLeft32(num59 + (uint) ((int) this.data[12] + (int) MDBase.C6 + ((int) num62 & (int) num60 | (int) num61 & ~(int) num60)), 9);
    uint num64 = Bits.RotateLeft32(num60 + (uint) ((int) this.data[4] + (int) MDBase.C6 + ((int) num63 & (int) num61 | (int) num62 & ~(int) num61)), 8);
    uint num65 = Bits.RotateLeft32(num61 + (uint) ((int) this.data[13] + (int) MDBase.C6 + ((int) num64 & (int) num62 | (int) num63 & ~(int) num62)), 9);
    uint num66 = Bits.RotateLeft32(num62 + (uint) ((int) this.data[3] + (int) MDBase.C6 + ((int) num65 & (int) num63 | (int) num64 & ~(int) num63)), 14);
    uint num67 = Bits.RotateLeft32(num63 + (uint) ((int) this.data[7] + (int) MDBase.C6 + ((int) num66 & (int) num64 | (int) num65 & ~(int) num64)), 5);
    uint num68 = Bits.RotateLeft32(num64 + (uint) ((int) this.data[15] + (int) MDBase.C6 + ((int) num67 & (int) num65 | (int) num66 & ~(int) num65)), 6);
    uint num69 = Bits.RotateLeft32(num65 + (uint) ((int) this.data[14] + (int) MDBase.C6 + ((int) num68 & (int) num66 | (int) num67 & ~(int) num66)), 8);
    uint num70 = Bits.RotateLeft32(num66 + (uint) ((int) this.data[5] + (int) MDBase.C6 + ((int) num69 & (int) num67 | (int) num68 & ~(int) num67)), 6);
    uint num71 = Bits.RotateLeft32(num67 + (uint) ((int) this.data[6] + (int) MDBase.C6 + ((int) num70 & (int) num68 | (int) num69 & ~(int) num68)), 5);
    uint num72 = Bits.RotateLeft32(num68 + (uint) ((int) this.data[2] + (int) MDBase.C6 + ((int) num71 & (int) num69 | (int) num70 & ~(int) num69)), 12);
    uint num73 = Bits.RotateLeft32(num5 + (uint) ((int) this.data[5] + (int) MDBase.C1 + ((int) num6 & (int) num8 | (int) num7 & ~(int) num8)), 8);
    uint num74 = Bits.RotateLeft32(num8 + (uint) ((int) this.data[14] + (int) MDBase.C1 + ((int) num73 & (int) num7 | (int) num6 & ~(int) num7)), 9);
    uint num75 = Bits.RotateLeft32(num7 + (uint) ((int) this.data[7] + (int) MDBase.C1 + ((int) num74 & (int) num6 | (int) num73 & ~(int) num6)), 9);
    uint num76 = Bits.RotateLeft32(num6 + (uint) ((int) this.data[0] + (int) MDBase.C1 + ((int) num75 & (int) num73 | (int) num74 & ~(int) num73)), 11);
    uint num77 = Bits.RotateLeft32(num73 + (uint) ((int) this.data[9] + (int) MDBase.C1 + ((int) num76 & (int) num74 | (int) num75 & ~(int) num74)), 13);
    uint num78 = Bits.RotateLeft32(num74 + (uint) ((int) this.data[2] + (int) MDBase.C1 + ((int) num77 & (int) num75 | (int) num76 & ~(int) num75)), 15);
    uint num79 = Bits.RotateLeft32(num75 + (uint) ((int) this.data[11] + (int) MDBase.C1 + ((int) num78 & (int) num76 | (int) num77 & ~(int) num76)), 15);
    uint num80 = Bits.RotateLeft32(num76 + (uint) ((int) this.data[4] + (int) MDBase.C1 + ((int) num79 & (int) num77 | (int) num78 & ~(int) num77)), 5);
    uint num81 = Bits.RotateLeft32(num77 + (uint) ((int) this.data[13] + (int) MDBase.C1 + ((int) num80 & (int) num78 | (int) num79 & ~(int) num78)), 7);
    uint num82 = Bits.RotateLeft32(num78 + (uint) ((int) this.data[6] + (int) MDBase.C1 + ((int) num81 & (int) num79 | (int) num80 & ~(int) num79)), 7);
    uint num83 = Bits.RotateLeft32(num79 + (uint) ((int) this.data[15] + (int) MDBase.C1 + ((int) num82 & (int) num80 | (int) num81 & ~(int) num80)), 8);
    uint num84 = Bits.RotateLeft32(num80 + (uint) ((int) this.data[8] + (int) MDBase.C1 + ((int) num83 & (int) num81 | (int) num82 & ~(int) num81)), 11);
    uint num85 = Bits.RotateLeft32(num81 + (uint) ((int) this.data[1] + (int) MDBase.C1 + ((int) num84 & (int) num82 | (int) num83 & ~(int) num82)), 14);
    uint num86 = Bits.RotateLeft32(num82 + (uint) ((int) this.data[10] + (int) MDBase.C1 + ((int) num85 & (int) num83 | (int) num84 & ~(int) num83)), 14);
    uint num87 = Bits.RotateLeft32(num83 + (uint) ((int) this.data[3] + (int) MDBase.C1 + ((int) num86 & (int) num84 | (int) num85 & ~(int) num84)), 12);
    uint num88 = Bits.RotateLeft32(num84 + (uint) ((int) this.data[12] + (int) MDBase.C1 + ((int) num87 & (int) num85 | (int) num86 & ~(int) num85)), 6);
    uint num89 = Bits.RotateLeft32(num85 + (uint) ((int) this.data[6] + (int) MDBase.C3 + (((int) num88 | ~(int) num87) ^ (int) num86)), 9);
    uint num90 = Bits.RotateLeft32(num86 + (uint) ((int) this.data[11] + (int) MDBase.C3 + (((int) num89 | ~(int) num88) ^ (int) num87)), 13);
    uint num91 = Bits.RotateLeft32(num87 + (uint) ((int) this.data[3] + (int) MDBase.C3 + (((int) num90 | ~(int) num89) ^ (int) num88)), 15);
    uint num92 = Bits.RotateLeft32(num88 + (uint) ((int) this.data[7] + (int) MDBase.C3 + (((int) num91 | ~(int) num90) ^ (int) num89)), 7);
    uint num93 = Bits.RotateLeft32(num89 + (uint) ((int) this.data[0] + (int) MDBase.C3 + (((int) num92 | ~(int) num91) ^ (int) num90)), 12);
    uint num94 = Bits.RotateLeft32(num90 + (uint) ((int) this.data[13] + (int) MDBase.C3 + (((int) num93 | ~(int) num92) ^ (int) num91)), 8);
    uint num95 = Bits.RotateLeft32(num91 + (uint) ((int) this.data[5] + (int) MDBase.C3 + (((int) num94 | ~(int) num93) ^ (int) num92)), 9);
    uint num96 = Bits.RotateLeft32(num92 + (uint) ((int) this.data[10] + (int) MDBase.C3 + (((int) num95 | ~(int) num94) ^ (int) num93)), 11);
    uint num97 = Bits.RotateLeft32(num93 + (uint) ((int) this.data[14] + (int) MDBase.C3 + (((int) num96 | ~(int) num95) ^ (int) num94)), 7);
    uint num98 = Bits.RotateLeft32(num94 + (uint) ((int) this.data[15] + (int) MDBase.C3 + (((int) num97 | ~(int) num96) ^ (int) num95)), 7);
    uint num99 = Bits.RotateLeft32(num95 + (uint) ((int) this.data[8] + (int) MDBase.C3 + (((int) num98 | ~(int) num97) ^ (int) num96)), 12);
    uint num100 = Bits.RotateLeft32(num96 + (uint) ((int) this.data[12] + (int) MDBase.C3 + (((int) num99 | ~(int) num98) ^ (int) num97)), 7);
    uint num101 = Bits.RotateLeft32(num97 + (uint) ((int) this.data[4] + (int) MDBase.C3 + (((int) num100 | ~(int) num99) ^ (int) num98)), 6);
    uint num102 = Bits.RotateLeft32(num98 + (uint) ((int) this.data[9] + (int) MDBase.C3 + (((int) num101 | ~(int) num100) ^ (int) num99)), 15);
    uint num103 = Bits.RotateLeft32(num99 + (uint) ((int) this.data[1] + (int) MDBase.C3 + (((int) num102 | ~(int) num101) ^ (int) num100)), 13);
    uint num104 = Bits.RotateLeft32(num100 + (uint) ((int) this.data[2] + (int) MDBase.C3 + (((int) num103 | ~(int) num102) ^ (int) num101)), 11);
    uint num105 = Bits.RotateLeft32(num101 + (uint) ((int) this.data[15] + (int) MDBase.C5 + ((int) num104 & (int) num103 | ~(int) num104 & (int) num102)), 9);
    uint num106 = Bits.RotateLeft32(num102 + (uint) ((int) this.data[5] + (int) MDBase.C5 + ((int) num105 & (int) num104 | ~(int) num105 & (int) num103)), 7);
    uint num107 = Bits.RotateLeft32(num103 + (uint) ((int) this.data[1] + (int) MDBase.C5 + ((int) num106 & (int) num105 | ~(int) num106 & (int) num104)), 15);
    uint num108 = Bits.RotateLeft32(num104 + (uint) ((int) this.data[3] + (int) MDBase.C5 + ((int) num107 & (int) num106 | ~(int) num107 & (int) num105)), 11);
    uint num109 = Bits.RotateLeft32(num105 + (uint) ((int) this.data[7] + (int) MDBase.C5 + ((int) num108 & (int) num107 | ~(int) num108 & (int) num106)), 8);
    uint num110 = Bits.RotateLeft32(num106 + (uint) ((int) this.data[14] + (int) MDBase.C5 + ((int) num109 & (int) num108 | ~(int) num109 & (int) num107)), 6);
    uint num111 = Bits.RotateLeft32(num107 + (uint) ((int) this.data[6] + (int) MDBase.C5 + ((int) num110 & (int) num109 | ~(int) num110 & (int) num108)), 6);
    uint num112 = Bits.RotateLeft32(num108 + (uint) ((int) this.data[9] + (int) MDBase.C5 + ((int) num111 & (int) num110 | ~(int) num111 & (int) num109)), 14);
    uint num113 = Bits.RotateLeft32(num109 + (uint) ((int) this.data[11] + (int) MDBase.C5 + ((int) num112 & (int) num111 | ~(int) num112 & (int) num110)), 12);
    uint num114 = Bits.RotateLeft32(num110 + (uint) ((int) this.data[8] + (int) MDBase.C5 + ((int) num113 & (int) num112 | ~(int) num113 & (int) num111)), 13);
    uint num115 = Bits.RotateLeft32(num111 + (uint) ((int) this.data[12] + (int) MDBase.C5 + ((int) num114 & (int) num113 | ~(int) num114 & (int) num112)), 5);
    uint num116 = Bits.RotateLeft32(num112 + (uint) ((int) this.data[2] + (int) MDBase.C5 + ((int) num115 & (int) num114 | ~(int) num115 & (int) num113)), 14);
    uint num117 = Bits.RotateLeft32(num113 + (uint) ((int) this.data[10] + (int) MDBase.C5 + ((int) num116 & (int) num115 | ~(int) num116 & (int) num114)), 13);
    uint num118 = Bits.RotateLeft32(num114 + (uint) ((int) this.data[0] + (int) MDBase.C5 + ((int) num117 & (int) num116 | ~(int) num117 & (int) num115)), 13);
    uint num119 = Bits.RotateLeft32(num115 + (uint) ((int) this.data[4] + (int) MDBase.C5 + ((int) num118 & (int) num117 | ~(int) num118 & (int) num116)), 7);
    uint num120 = Bits.RotateLeft32(num116 + (uint) ((int) this.data[13] + (int) MDBase.C5 + ((int) num119 & (int) num118 | ~(int) num119 & (int) num117)), 5);
    uint num121 = Bits.RotateLeft32(num117 + (this.data[8] + (num120 ^ num119 ^ num118)), 15);
    uint num122 = Bits.RotateLeft32(num118 + (this.data[6] + (num121 ^ num120 ^ num119)), 5);
    uint num123 = Bits.RotateLeft32(num119 + (this.data[4] + (num122 ^ num121 ^ num120)), 8);
    uint num124 = Bits.RotateLeft32(num120 + (this.data[1] + (num123 ^ num122 ^ num121)), 11);
    uint num125 = Bits.RotateLeft32(num121 + (this.data[3] + (num124 ^ num123 ^ num122)), 14);
    uint num126 = Bits.RotateLeft32(num122 + (this.data[11] + (num125 ^ num124 ^ num123)), 14);
    uint num127 = Bits.RotateLeft32(num123 + (this.data[15] + (num126 ^ num125 ^ num124)), 6);
    uint num128 = Bits.RotateLeft32(num124 + (this.data[0] + (num127 ^ num126 ^ num125)), 14);
    uint num129 = Bits.RotateLeft32(num125 + (this.data[5] + (num128 ^ num127 ^ num126)), 6);
    uint num130 = Bits.RotateLeft32(num126 + (this.data[12] + (num129 ^ num128 ^ num127)), 9);
    uint num131 = Bits.RotateLeft32(num127 + (this.data[2] + (num130 ^ num129 ^ num128)), 12);
    uint num132 = Bits.RotateLeft32(num128 + (this.data[13] + (num131 ^ num130 ^ num129)), 9);
    uint num133 = Bits.RotateLeft32(num129 + (this.data[9] + (num132 ^ num131 ^ num130)), 12);
    uint num134 = Bits.RotateLeft32(num130 + (this.data[7] + (num133 ^ num132 ^ num131)), 5);
    uint num135 = Bits.RotateLeft32(num131 + (this.data[10] + (num134 ^ num133 ^ num132)), 15);
    uint num136 = Bits.RotateLeft32(num132 + (this.data[14] + (num135 ^ num134 ^ num133)), 8);
    uint num137 = num134 + num71 + this.state[1];
    this.state[1] = this.state[2] + num70 + num133;
    this.state[2] = this.state[3] + num69 + num136;
    this.state[3] = this.state[0] + num72 + num135;
    this.state[0] = num137;
    Intermech.Hashes.Utils.Utils.Memset(ref this.data, (byte) 0);
  }
}
