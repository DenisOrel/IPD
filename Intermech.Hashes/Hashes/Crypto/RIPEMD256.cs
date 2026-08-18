// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.RIPEMD256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class RIPEMD256 : MDBase, ITransformBlock
{
  private uint[] data;

  public RIPEMD256()
    : base(8, 32 /*0x20*/)
  {
    this.data = new uint[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    RIPEMD256 ripemD256 = new RIPEMD256();
    ripemD256.buffer = this.buffer.Clone();
    ripemD256.processed_bytes = this.processed_bytes;
    ripemD256.state = this.state.DeepCopy();
    ripemD256.BufferSize = this.BufferSize;
    return (IHash) ripemD256;
  }

  public override void Initialize()
  {
    this.state[4] = 1985229328U;
    this.state[5] = 4275878552U;
    this.state[6] = 2309737967U;
    this.state[7] = 19088743U;
    base.Initialize();
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    fixed (uint* dest = this.data)
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
    uint num1 = this.state[0];
    uint num2 = this.state[1];
    uint num3 = this.state[2];
    uint num4 = this.state[3];
    uint num5 = this.state[4];
    uint num6 = this.state[5];
    uint num7 = this.state[6];
    uint num8 = this.state[7];
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
    uint num25 = Bits.RotateLeft32(num5 + (uint) ((int) this.data[5] + (int) MDBase.C1 + ((int) num6 & (int) num8 | (int) num7 & ~(int) num8)), 8);
    uint num26 = Bits.RotateLeft32(num8 + (uint) ((int) this.data[14] + (int) MDBase.C1 + ((int) num25 & (int) num7 | (int) num6 & ~(int) num7)), 9);
    uint num27 = Bits.RotateLeft32(num7 + (uint) ((int) this.data[7] + (int) MDBase.C1 + ((int) num26 & (int) num6 | (int) num25 & ~(int) num6)), 9);
    uint num28 = Bits.RotateLeft32(num6 + (uint) ((int) this.data[0] + (int) MDBase.C1 + ((int) num27 & (int) num25 | (int) num26 & ~(int) num25)), 11);
    uint num29 = Bits.RotateLeft32(num25 + (uint) ((int) this.data[9] + (int) MDBase.C1 + ((int) num28 & (int) num26 | (int) num27 & ~(int) num26)), 13);
    uint num30 = Bits.RotateLeft32(num26 + (uint) ((int) this.data[2] + (int) MDBase.C1 + ((int) num29 & (int) num27 | (int) num28 & ~(int) num27)), 15);
    uint num31 = Bits.RotateLeft32(num27 + (uint) ((int) this.data[11] + (int) MDBase.C1 + ((int) num30 & (int) num28 | (int) num29 & ~(int) num28)), 15);
    uint num32 = Bits.RotateLeft32(num28 + (uint) ((int) this.data[4] + (int) MDBase.C1 + ((int) num31 & (int) num29 | (int) num30 & ~(int) num29)), 5);
    uint num33 = Bits.RotateLeft32(num29 + (uint) ((int) this.data[13] + (int) MDBase.C1 + ((int) num32 & (int) num30 | (int) num31 & ~(int) num30)), 7);
    uint num34 = Bits.RotateLeft32(num30 + (uint) ((int) this.data[6] + (int) MDBase.C1 + ((int) num33 & (int) num31 | (int) num32 & ~(int) num31)), 7);
    uint num35 = Bits.RotateLeft32(num31 + (uint) ((int) this.data[15] + (int) MDBase.C1 + ((int) num34 & (int) num32 | (int) num33 & ~(int) num32)), 8);
    uint num36 = Bits.RotateLeft32(num32 + (uint) ((int) this.data[8] + (int) MDBase.C1 + ((int) num35 & (int) num33 | (int) num34 & ~(int) num33)), 11);
    uint num37 = Bits.RotateLeft32(num33 + (uint) ((int) this.data[1] + (int) MDBase.C1 + ((int) num36 & (int) num34 | (int) num35 & ~(int) num34)), 14);
    uint num38 = Bits.RotateLeft32(num34 + (uint) ((int) this.data[10] + (int) MDBase.C1 + ((int) num37 & (int) num35 | (int) num36 & ~(int) num35)), 14);
    uint num39 = Bits.RotateLeft32(num35 + (uint) ((int) this.data[3] + (int) MDBase.C1 + ((int) num38 & (int) num36 | (int) num37 & ~(int) num36)), 12);
    uint num40 = Bits.RotateLeft32(num36 + (uint) ((int) this.data[12] + (int) MDBase.C1 + ((int) num39 & (int) num37 | (int) num38 & ~(int) num37)), 6);
    uint num41 = Bits.RotateLeft32(num37 + (uint) ((int) this.data[7] + (int) MDBase.C2 + ((int) num24 & (int) num23 | ~(int) num24 & (int) num22)), 7);
    uint num42 = Bits.RotateLeft32(num22 + (uint) ((int) this.data[4] + (int) MDBase.C2 + ((int) num41 & (int) num24 | ~(int) num41 & (int) num23)), 6);
    uint num43 = Bits.RotateLeft32(num23 + (uint) ((int) this.data[13] + (int) MDBase.C2 + ((int) num42 & (int) num41 | ~(int) num42 & (int) num24)), 8);
    uint num44 = Bits.RotateLeft32(num24 + (uint) ((int) this.data[1] + (int) MDBase.C2 + ((int) num43 & (int) num42 | ~(int) num43 & (int) num41)), 13);
    uint num45 = Bits.RotateLeft32(num41 + (uint) ((int) this.data[10] + (int) MDBase.C2 + ((int) num44 & (int) num43 | ~(int) num44 & (int) num42)), 11);
    uint num46 = Bits.RotateLeft32(num42 + (uint) ((int) this.data[6] + (int) MDBase.C2 + ((int) num45 & (int) num44 | ~(int) num45 & (int) num43)), 9);
    uint num47 = Bits.RotateLeft32(num43 + (uint) ((int) this.data[15] + (int) MDBase.C2 + ((int) num46 & (int) num45 | ~(int) num46 & (int) num44)), 7);
    uint num48 = Bits.RotateLeft32(num44 + (uint) ((int) this.data[3] + (int) MDBase.C2 + ((int) num47 & (int) num46 | ~(int) num47 & (int) num45)), 15);
    uint num49 = Bits.RotateLeft32(num45 + (uint) ((int) this.data[12] + (int) MDBase.C2 + ((int) num48 & (int) num47 | ~(int) num48 & (int) num46)), 7);
    uint num50 = Bits.RotateLeft32(num46 + (uint) ((int) this.data[0] + (int) MDBase.C2 + ((int) num49 & (int) num48 | ~(int) num49 & (int) num47)), 12);
    uint num51 = Bits.RotateLeft32(num47 + (uint) ((int) this.data[9] + (int) MDBase.C2 + ((int) num50 & (int) num49 | ~(int) num50 & (int) num48)), 15);
    uint num52 = Bits.RotateLeft32(num48 + (uint) ((int) this.data[5] + (int) MDBase.C2 + ((int) num51 & (int) num50 | ~(int) num51 & (int) num49)), 9);
    uint num53 = Bits.RotateLeft32(num49 + (uint) ((int) this.data[2] + (int) MDBase.C2 + ((int) num52 & (int) num51 | ~(int) num52 & (int) num50)), 11);
    uint num54 = Bits.RotateLeft32(num50 + (uint) ((int) this.data[14] + (int) MDBase.C2 + ((int) num53 & (int) num52 | ~(int) num53 & (int) num51)), 7);
    uint num55 = Bits.RotateLeft32(num51 + (uint) ((int) this.data[11] + (int) MDBase.C2 + ((int) num54 & (int) num53 | ~(int) num54 & (int) num52)), 13);
    uint num56 = Bits.RotateLeft32(num52 + (uint) ((int) this.data[8] + (int) MDBase.C2 + ((int) num55 & (int) num54 | ~(int) num55 & (int) num53)), 12);
    uint num57 = Bits.RotateLeft32(num21 + (uint) ((int) this.data[6] + (int) MDBase.C3 + (((int) num40 | ~(int) num39) ^ (int) num38)), 9);
    uint num58 = Bits.RotateLeft32(num38 + (uint) ((int) this.data[11] + (int) MDBase.C3 + (((int) num57 | ~(int) num40) ^ (int) num39)), 13);
    uint num59 = Bits.RotateLeft32(num39 + (uint) ((int) this.data[3] + (int) MDBase.C3 + (((int) num58 | ~(int) num57) ^ (int) num40)), 15);
    uint num60 = Bits.RotateLeft32(num40 + (uint) ((int) this.data[7] + (int) MDBase.C3 + (((int) num59 | ~(int) num58) ^ (int) num57)), 7);
    uint num61 = Bits.RotateLeft32(num57 + (uint) ((int) this.data[0] + (int) MDBase.C3 + (((int) num60 | ~(int) num59) ^ (int) num58)), 12);
    uint num62 = Bits.RotateLeft32(num58 + (uint) ((int) this.data[13] + (int) MDBase.C3 + (((int) num61 | ~(int) num60) ^ (int) num59)), 8);
    uint num63 = Bits.RotateLeft32(num59 + (uint) ((int) this.data[5] + (int) MDBase.C3 + (((int) num62 | ~(int) num61) ^ (int) num60)), 9);
    uint num64 = Bits.RotateLeft32(num60 + (uint) ((int) this.data[10] + (int) MDBase.C3 + (((int) num63 | ~(int) num62) ^ (int) num61)), 11);
    uint num65 = Bits.RotateLeft32(num61 + (uint) ((int) this.data[14] + (int) MDBase.C3 + (((int) num64 | ~(int) num63) ^ (int) num62)), 7);
    uint num66 = Bits.RotateLeft32(num62 + (uint) ((int) this.data[15] + (int) MDBase.C3 + (((int) num65 | ~(int) num64) ^ (int) num63)), 7);
    uint num67 = Bits.RotateLeft32(num63 + (uint) ((int) this.data[8] + (int) MDBase.C3 + (((int) num66 | ~(int) num65) ^ (int) num64)), 12);
    uint num68 = Bits.RotateLeft32(num64 + (uint) ((int) this.data[12] + (int) MDBase.C3 + (((int) num67 | ~(int) num66) ^ (int) num65)), 7);
    uint num69 = Bits.RotateLeft32(num65 + (uint) ((int) this.data[4] + (int) MDBase.C3 + (((int) num68 | ~(int) num67) ^ (int) num66)), 6);
    uint num70 = Bits.RotateLeft32(num66 + (uint) ((int) this.data[9] + (int) MDBase.C3 + (((int) num69 | ~(int) num68) ^ (int) num67)), 15);
    uint num71 = Bits.RotateLeft32(num67 + (uint) ((int) this.data[1] + (int) MDBase.C3 + (((int) num70 | ~(int) num69) ^ (int) num68)), 13);
    uint num72 = Bits.RotateLeft32(num68 + (uint) ((int) this.data[2] + (int) MDBase.C3 + (((int) num71 | ~(int) num70) ^ (int) num69)), 11);
    uint num73 = Bits.RotateLeft32(num53 + (uint) ((int) this.data[3] + (int) MDBase.C4 + (((int) num72 | ~(int) num55) ^ (int) num54)), 11);
    uint num74 = Bits.RotateLeft32(num54 + (uint) ((int) this.data[10] + (int) MDBase.C4 + (((int) num73 | ~(int) num72) ^ (int) num55)), 13);
    uint num75 = Bits.RotateLeft32(num55 + (uint) ((int) this.data[14] + (int) MDBase.C4 + (((int) num74 | ~(int) num73) ^ (int) num72)), 6);
    uint num76 = Bits.RotateLeft32(num72 + (uint) ((int) this.data[4] + (int) MDBase.C4 + (((int) num75 | ~(int) num74) ^ (int) num73)), 7);
    uint num77 = Bits.RotateLeft32(num73 + (uint) ((int) this.data[9] + (int) MDBase.C4 + (((int) num76 | ~(int) num75) ^ (int) num74)), 14);
    uint num78 = Bits.RotateLeft32(num74 + (uint) ((int) this.data[15] + (int) MDBase.C4 + (((int) num77 | ~(int) num76) ^ (int) num75)), 9);
    uint num79 = Bits.RotateLeft32(num75 + (uint) ((int) this.data[8] + (int) MDBase.C4 + (((int) num78 | ~(int) num77) ^ (int) num76)), 13);
    uint num80 = Bits.RotateLeft32(num76 + (uint) ((int) this.data[1] + (int) MDBase.C4 + (((int) num79 | ~(int) num78) ^ (int) num77)), 15);
    uint num81 = Bits.RotateLeft32(num77 + (uint) ((int) this.data[2] + (int) MDBase.C4 + (((int) num80 | ~(int) num79) ^ (int) num78)), 14);
    uint num82 = Bits.RotateLeft32(num78 + (uint) ((int) this.data[7] + (int) MDBase.C4 + (((int) num81 | ~(int) num80) ^ (int) num79)), 8);
    uint num83 = Bits.RotateLeft32(num79 + (uint) ((int) this.data[0] + (int) MDBase.C4 + (((int) num82 | ~(int) num81) ^ (int) num80)), 13);
    uint num84 = Bits.RotateLeft32(num80 + (uint) ((int) this.data[6] + (int) MDBase.C4 + (((int) num83 | ~(int) num82) ^ (int) num81)), 6);
    uint num85 = Bits.RotateLeft32(num81 + (uint) ((int) this.data[13] + (int) MDBase.C4 + (((int) num84 | ~(int) num83) ^ (int) num82)), 5);
    uint num86 = Bits.RotateLeft32(num82 + (uint) ((int) this.data[11] + (int) MDBase.C4 + (((int) num85 | ~(int) num84) ^ (int) num83)), 12);
    uint num87 = Bits.RotateLeft32(num83 + (uint) ((int) this.data[5] + (int) MDBase.C4 + (((int) num86 | ~(int) num85) ^ (int) num84)), 7);
    uint num88 = Bits.RotateLeft32(num84 + (uint) ((int) this.data[12] + (int) MDBase.C4 + (((int) num87 | ~(int) num86) ^ (int) num85)), 5);
    uint num89 = Bits.RotateLeft32(num69 + (uint) ((int) this.data[15] + (int) MDBase.C5 + ((int) num56 & (int) num71 | ~(int) num56 & (int) num70)), 9);
    uint num90 = Bits.RotateLeft32(num70 + (uint) ((int) this.data[5] + (int) MDBase.C5 + ((int) num89 & (int) num56 | ~(int) num89 & (int) num71)), 7);
    uint num91 = Bits.RotateLeft32(num71 + (uint) ((int) this.data[1] + (int) MDBase.C5 + ((int) num90 & (int) num89 | ~(int) num90 & (int) num56)), 15);
    uint num92 = Bits.RotateLeft32(num56 + (uint) ((int) this.data[3] + (int) MDBase.C5 + ((int) num91 & (int) num90 | ~(int) num91 & (int) num89)), 11);
    uint num93 = Bits.RotateLeft32(num89 + (uint) ((int) this.data[7] + (int) MDBase.C5 + ((int) num92 & (int) num91 | ~(int) num92 & (int) num90)), 8);
    uint num94 = Bits.RotateLeft32(num90 + (uint) ((int) this.data[14] + (int) MDBase.C5 + ((int) num93 & (int) num92 | ~(int) num93 & (int) num91)), 6);
    uint num95 = Bits.RotateLeft32(num91 + (uint) ((int) this.data[6] + (int) MDBase.C5 + ((int) num94 & (int) num93 | ~(int) num94 & (int) num92)), 6);
    uint num96 = Bits.RotateLeft32(num92 + (uint) ((int) this.data[9] + (int) MDBase.C5 + ((int) num95 & (int) num94 | ~(int) num95 & (int) num93)), 14);
    uint num97 = Bits.RotateLeft32(num93 + (uint) ((int) this.data[11] + (int) MDBase.C5 + ((int) num96 & (int) num95 | ~(int) num96 & (int) num94)), 12);
    uint num98 = Bits.RotateLeft32(num94 + (uint) ((int) this.data[8] + (int) MDBase.C5 + ((int) num97 & (int) num96 | ~(int) num97 & (int) num95)), 13);
    uint num99 = Bits.RotateLeft32(num95 + (uint) ((int) this.data[12] + (int) MDBase.C5 + ((int) num98 & (int) num97 | ~(int) num98 & (int) num96)), 5);
    uint num100 = Bits.RotateLeft32(num96 + (uint) ((int) this.data[2] + (int) MDBase.C5 + ((int) num99 & (int) num98 | ~(int) num99 & (int) num97)), 14);
    uint num101 = Bits.RotateLeft32(num97 + (uint) ((int) this.data[10] + (int) MDBase.C5 + ((int) num100 & (int) num99 | ~(int) num100 & (int) num98)), 13);
    uint num102 = Bits.RotateLeft32(num98 + (uint) ((int) this.data[0] + (int) MDBase.C5 + ((int) num101 & (int) num100 | ~(int) num101 & (int) num99)), 13);
    uint num103 = Bits.RotateLeft32(num99 + (uint) ((int) this.data[4] + (int) MDBase.C5 + ((int) num102 & (int) num101 | ~(int) num102 & (int) num100)), 7);
    uint num104 = Bits.RotateLeft32(num100 + (uint) ((int) this.data[13] + (int) MDBase.C5 + ((int) num103 & (int) num102 | ~(int) num103 & (int) num101)), 5);
    uint num105 = Bits.RotateLeft32(num85 + (uint) ((int) this.data[1] + (int) MDBase.C6 + ((int) num88 & (int) num86 | (int) num103 & ~(int) num86)), 11);
    uint num106 = Bits.RotateLeft32(num86 + (uint) ((int) this.data[9] + (int) MDBase.C6 + ((int) num105 & (int) num103 | (int) num88 & ~(int) num103)), 12);
    uint num107 = Bits.RotateLeft32(num103 + (uint) ((int) this.data[11] + (int) MDBase.C6 + ((int) num106 & (int) num88 | (int) num105 & ~(int) num88)), 14);
    uint num108 = Bits.RotateLeft32(num88 + (uint) ((int) this.data[10] + (int) MDBase.C6 + ((int) num107 & (int) num105 | (int) num106 & ~(int) num105)), 15);
    uint num109 = Bits.RotateLeft32(num105 + (uint) ((int) this.data[0] + (int) MDBase.C6 + ((int) num108 & (int) num106 | (int) num107 & ~(int) num106)), 14);
    uint num110 = Bits.RotateLeft32(num106 + (uint) ((int) this.data[8] + (int) MDBase.C6 + ((int) num109 & (int) num107 | (int) num108 & ~(int) num107)), 15);
    uint num111 = Bits.RotateLeft32(num107 + (uint) ((int) this.data[12] + (int) MDBase.C6 + ((int) num110 & (int) num108 | (int) num109 & ~(int) num108)), 9);
    uint num112 = Bits.RotateLeft32(num108 + (uint) ((int) this.data[4] + (int) MDBase.C6 + ((int) num111 & (int) num109 | (int) num110 & ~(int) num109)), 8);
    uint num113 = Bits.RotateLeft32(num109 + (uint) ((int) this.data[13] + (int) MDBase.C6 + ((int) num112 & (int) num110 | (int) num111 & ~(int) num110)), 9);
    uint num114 = Bits.RotateLeft32(num110 + (uint) ((int) this.data[3] + (int) MDBase.C6 + ((int) num113 & (int) num111 | (int) num112 & ~(int) num111)), 14);
    uint num115 = Bits.RotateLeft32(num111 + (uint) ((int) this.data[7] + (int) MDBase.C6 + ((int) num114 & (int) num112 | (int) num113 & ~(int) num112)), 5);
    uint num116 = Bits.RotateLeft32(num112 + (uint) ((int) this.data[15] + (int) MDBase.C6 + ((int) num115 & (int) num113 | (int) num114 & ~(int) num113)), 6);
    uint num117 = Bits.RotateLeft32(num113 + (uint) ((int) this.data[14] + (int) MDBase.C6 + ((int) num116 & (int) num114 | (int) num115 & ~(int) num114)), 8);
    uint num118 = Bits.RotateLeft32(num114 + (uint) ((int) this.data[5] + (int) MDBase.C6 + ((int) num117 & (int) num115 | (int) num116 & ~(int) num115)), 6);
    uint num119 = Bits.RotateLeft32(num115 + (uint) ((int) this.data[6] + (int) MDBase.C6 + ((int) num118 & (int) num116 | (int) num117 & ~(int) num116)), 5);
    uint num120 = Bits.RotateLeft32(num116 + (uint) ((int) this.data[2] + (int) MDBase.C6 + ((int) num119 & (int) num117 | (int) num118 & ~(int) num117)), 12);
    uint num121 = Bits.RotateLeft32(num101 + (this.data[8] + (num104 ^ num87 ^ num102)), 15);
    uint num122 = Bits.RotateLeft32(num102 + (this.data[6] + (num121 ^ num104 ^ num87)), 5);
    uint num123 = Bits.RotateLeft32(num87 + (this.data[4] + (num122 ^ num121 ^ num104)), 8);
    uint num124 = Bits.RotateLeft32(num104 + (this.data[1] + (num123 ^ num122 ^ num121)), 11);
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
    this.state[0] = this.state[0] + num117;
    this.state[1] = this.state[1] + num120;
    this.state[2] = this.state[2] + num119;
    this.state[3] = this.state[3] + num134;
    this.state[4] = this.state[4] + num133;
    this.state[5] = this.state[5] + num136;
    this.state[6] = this.state[6] + num135;
    this.state[7] = this.state[7] + num118;
    Intermech.Hashes.Utils.Utils.Memset(ref this.data, (byte) 0);
  }
}
