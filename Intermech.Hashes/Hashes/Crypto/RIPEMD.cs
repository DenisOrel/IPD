// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.RIPEMD
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class RIPEMD : MDBase, ITransformBlock
{
  private uint[] data;

  public RIPEMD()
    : base(4, 16 /*0x10*/)
  {
    this.data = new uint[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    RIPEMD ripemd = new RIPEMD();
    ripemd.buffer = this.buffer.Clone();
    ripemd.processed_bytes = this.processed_bytes;
    ripemd.state = this.state.DeepCopy();
    ripemd.BufferSize = this.BufferSize;
    return (IHash) ripemd;
  }

  private static uint P1(uint a, uint b, uint c) => (uint) ((int) a & (int) b | ~(int) a & (int) c);

  private static uint P2(uint a, uint b, uint c)
  {
    return (uint) ((int) a & (int) b | (int) a & (int) c | (int) b & (int) c);
  }

  private static uint P3(uint a, uint b, uint c) => a ^ b ^ c;

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    fixed (uint* dest = this.data)
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
    uint num1 = this.state[0];
    uint num2 = this.state[1];
    uint num3 = this.state[2];
    uint c1 = this.state[3];
    uint num4 = num1;
    uint num5 = num2;
    uint num6 = num3;
    uint c2 = c1;
    uint num7 = Bits.RotateLeft32(RIPEMD.P1(num2, num3, c1) + num1 + this.data[0], 11);
    uint num8 = Bits.RotateLeft32(RIPEMD.P1(num7, num2, num3) + c1 + this.data[1], 14);
    uint num9 = Bits.RotateLeft32(RIPEMD.P1(num8, num7, num2) + num3 + this.data[2], 15);
    uint num10 = Bits.RotateLeft32(RIPEMD.P1(num9, num8, num7) + num2 + this.data[3], 12);
    uint num11 = Bits.RotateLeft32(RIPEMD.P1(num10, num9, num8) + num7 + this.data[4], 5);
    uint num12 = Bits.RotateLeft32(RIPEMD.P1(num11, num10, num9) + num8 + this.data[5], 8);
    uint num13 = Bits.RotateLeft32(RIPEMD.P1(num12, num11, num10) + num9 + this.data[6], 7);
    uint num14 = Bits.RotateLeft32(RIPEMD.P1(num13, num12, num11) + num10 + this.data[7], 9);
    uint num15 = Bits.RotateLeft32(RIPEMD.P1(num14, num13, num12) + num11 + this.data[8], 11);
    uint num16 = Bits.RotateLeft32(RIPEMD.P1(num15, num14, num13) + num12 + this.data[9], 13);
    uint num17 = Bits.RotateLeft32(RIPEMD.P1(num16, num15, num14) + num13 + this.data[10], 14);
    uint num18 = Bits.RotateLeft32(RIPEMD.P1(num17, num16, num15) + num14 + this.data[11], 15);
    uint num19 = Bits.RotateLeft32(RIPEMD.P1(num18, num17, num16) + num15 + this.data[12], 6);
    uint num20 = Bits.RotateLeft32(RIPEMD.P1(num19, num18, num17) + num16 + this.data[13], 7);
    uint num21 = Bits.RotateLeft32(RIPEMD.P1(num20, num19, num18) + num17 + this.data[14], 9);
    uint num22 = Bits.RotateLeft32(RIPEMD.P1(num21, num20, num19) + num18 + this.data[15], 8);
    uint num23 = Bits.RotateLeft32(RIPEMD.P2(num22, num21, num20) + num19 + this.data[7] + MDBase.C2, 7);
    uint num24 = Bits.RotateLeft32(RIPEMD.P2(num23, num22, num21) + num20 + this.data[4] + MDBase.C2, 6);
    uint num25 = Bits.RotateLeft32(RIPEMD.P2(num24, num23, num22) + num21 + this.data[13] + MDBase.C2, 8);
    uint num26 = Bits.RotateLeft32(RIPEMD.P2(num25, num24, num23) + num22 + this.data[1] + MDBase.C2, 13);
    uint num27 = Bits.RotateLeft32(RIPEMD.P2(num26, num25, num24) + num23 + this.data[10] + MDBase.C2, 11);
    uint num28 = Bits.RotateLeft32(RIPEMD.P2(num27, num26, num25) + num24 + this.data[6] + MDBase.C2, 9);
    uint num29 = Bits.RotateLeft32(RIPEMD.P2(num28, num27, num26) + num25 + this.data[15] + MDBase.C2, 7);
    uint num30 = Bits.RotateLeft32(RIPEMD.P2(num29, num28, num27) + num26 + this.data[3] + MDBase.C2, 15);
    uint num31 = Bits.RotateLeft32(RIPEMD.P2(num30, num29, num28) + num27 + this.data[12] + MDBase.C2, 7);
    uint num32 = Bits.RotateLeft32(RIPEMD.P2(num31, num30, num29) + num28 + this.data[0] + MDBase.C2, 12);
    uint num33 = Bits.RotateLeft32(RIPEMD.P2(num32, num31, num30) + num29 + this.data[9] + MDBase.C2, 15);
    uint num34 = Bits.RotateLeft32(RIPEMD.P2(num33, num32, num31) + num30 + this.data[5] + MDBase.C2, 9);
    uint num35 = Bits.RotateLeft32(RIPEMD.P2(num34, num33, num32) + num31 + this.data[14] + MDBase.C2, 7);
    uint num36 = Bits.RotateLeft32(RIPEMD.P2(num35, num34, num33) + num32 + this.data[2] + MDBase.C2, 11);
    uint num37 = Bits.RotateLeft32(RIPEMD.P2(num36, num35, num34) + num33 + this.data[11] + MDBase.C2, 13);
    uint num38 = Bits.RotateLeft32(RIPEMD.P2(num37, num36, num35) + num34 + this.data[8] + MDBase.C2, 12);
    uint num39 = Bits.RotateLeft32(RIPEMD.P3(num38, num37, num36) + num35 + this.data[3] + MDBase.C4, 11);
    uint num40 = Bits.RotateLeft32(RIPEMD.P3(num39, num38, num37) + num36 + this.data[10] + MDBase.C4, 13);
    uint num41 = Bits.RotateLeft32(RIPEMD.P3(num40, num39, num38) + num37 + this.data[2] + MDBase.C4, 14);
    uint num42 = Bits.RotateLeft32(RIPEMD.P3(num41, num40, num39) + num38 + this.data[4] + MDBase.C4, 7);
    uint num43 = Bits.RotateLeft32(RIPEMD.P3(num42, num41, num40) + num39 + this.data[9] + MDBase.C4, 14);
    uint num44 = Bits.RotateLeft32(RIPEMD.P3(num43, num42, num41) + num40 + this.data[15] + MDBase.C4, 9);
    uint num45 = Bits.RotateLeft32(RIPEMD.P3(num44, num43, num42) + num41 + this.data[8] + MDBase.C4, 13);
    uint num46 = Bits.RotateLeft32(RIPEMD.P3(num45, num44, num43) + num42 + this.data[1] + MDBase.C4, 15);
    uint num47 = Bits.RotateLeft32(RIPEMD.P3(num46, num45, num44) + num43 + this.data[14] + MDBase.C4, 6);
    uint num48 = Bits.RotateLeft32(RIPEMD.P3(num47, num46, num45) + num44 + this.data[7] + MDBase.C4, 8);
    uint num49 = Bits.RotateLeft32(RIPEMD.P3(num48, num47, num46) + num45 + this.data[0] + MDBase.C4, 13);
    uint num50 = Bits.RotateLeft32(RIPEMD.P3(num49, num48, num47) + num46 + this.data[6] + MDBase.C4, 6);
    uint num51 = Bits.RotateLeft32(RIPEMD.P3(num50, num49, num48) + num47 + this.data[11] + MDBase.C4, 12);
    uint num52 = Bits.RotateLeft32(RIPEMD.P3(num51, num50, num49) + num48 + this.data[13] + MDBase.C4, 5);
    uint a1 = Bits.RotateLeft32(RIPEMD.P3(num52, num51, num50) + num49 + this.data[5] + MDBase.C4, 7);
    uint num53 = Bits.RotateLeft32(RIPEMD.P3(a1, num52, num51) + num50 + this.data[12] + MDBase.C4, 5);
    uint num54 = Bits.RotateLeft32(RIPEMD.P1(num5, num6, c2) + num4 + this.data[0] + MDBase.C1, 11);
    uint num55 = Bits.RotateLeft32(RIPEMD.P1(num54, num5, num6) + c2 + this.data[1] + MDBase.C1, 14);
    uint num56 = Bits.RotateLeft32(RIPEMD.P1(num55, num54, num5) + num6 + this.data[2] + MDBase.C1, 15);
    uint num57 = Bits.RotateLeft32(RIPEMD.P1(num56, num55, num54) + num5 + this.data[3] + MDBase.C1, 12);
    uint num58 = Bits.RotateLeft32(RIPEMD.P1(num57, num56, num55) + num54 + this.data[4] + MDBase.C1, 5);
    uint num59 = Bits.RotateLeft32(RIPEMD.P1(num58, num57, num56) + num55 + this.data[5] + MDBase.C1, 8);
    uint num60 = Bits.RotateLeft32(RIPEMD.P1(num59, num58, num57) + num56 + this.data[6] + MDBase.C1, 7);
    uint num61 = Bits.RotateLeft32(RIPEMD.P1(num60, num59, num58) + num57 + this.data[7] + MDBase.C1, 9);
    uint num62 = Bits.RotateLeft32(RIPEMD.P1(num61, num60, num59) + num58 + this.data[8] + MDBase.C1, 11);
    uint num63 = Bits.RotateLeft32(RIPEMD.P1(num62, num61, num60) + num59 + this.data[9] + MDBase.C1, 13);
    uint num64 = Bits.RotateLeft32(RIPEMD.P1(num63, num62, num61) + num60 + this.data[10] + MDBase.C1, 14);
    uint num65 = Bits.RotateLeft32(RIPEMD.P1(num64, num63, num62) + num61 + this.data[11] + MDBase.C1, 15);
    uint num66 = Bits.RotateLeft32(RIPEMD.P1(num65, num64, num63) + num62 + this.data[12] + MDBase.C1, 6);
    uint num67 = Bits.RotateLeft32(RIPEMD.P1(num66, num65, num64) + num63 + this.data[13] + MDBase.C1, 7);
    uint num68 = Bits.RotateLeft32(RIPEMD.P1(num67, num66, num65) + num64 + this.data[14] + MDBase.C1, 9);
    uint num69 = Bits.RotateLeft32(RIPEMD.P1(num68, num67, num66) + num65 + this.data[15] + MDBase.C1, 8);
    uint num70 = Bits.RotateLeft32(RIPEMD.P2(num69, num68, num67) + num66 + this.data[7], 7);
    uint num71 = Bits.RotateLeft32(RIPEMD.P2(num70, num69, num68) + num67 + this.data[4], 6);
    uint num72 = Bits.RotateLeft32(RIPEMD.P2(num71, num70, num69) + num68 + this.data[13], 8);
    uint num73 = Bits.RotateLeft32(RIPEMD.P2(num72, num71, num70) + num69 + this.data[1], 13);
    uint num74 = Bits.RotateLeft32(RIPEMD.P2(num73, num72, num71) + num70 + this.data[10], 11);
    uint num75 = Bits.RotateLeft32(RIPEMD.P2(num74, num73, num72) + num71 + this.data[6], 9);
    uint num76 = Bits.RotateLeft32(RIPEMD.P2(num75, num74, num73) + num72 + this.data[15], 7);
    uint num77 = Bits.RotateLeft32(RIPEMD.P2(num76, num75, num74) + num73 + this.data[3], 15);
    uint num78 = Bits.RotateLeft32(RIPEMD.P2(num77, num76, num75) + num74 + this.data[12], 7);
    uint num79 = Bits.RotateLeft32(RIPEMD.P2(num78, num77, num76) + num75 + this.data[0], 12);
    uint num80 = Bits.RotateLeft32(RIPEMD.P2(num79, num78, num77) + num76 + this.data[9], 15);
    uint num81 = Bits.RotateLeft32(RIPEMD.P2(num80, num79, num78) + num77 + this.data[5], 9);
    uint num82 = Bits.RotateLeft32(RIPEMD.P2(num81, num80, num79) + num78 + this.data[14], 7);
    uint num83 = Bits.RotateLeft32(RIPEMD.P2(num82, num81, num80) + num79 + this.data[2], 11);
    uint num84 = Bits.RotateLeft32(RIPEMD.P2(num83, num82, num81) + num80 + this.data[11], 13);
    uint num85 = Bits.RotateLeft32(RIPEMD.P2(num84, num83, num82) + num81 + this.data[8], 12);
    uint num86 = Bits.RotateLeft32(RIPEMD.P3(num85, num84, num83) + num82 + this.data[3] + MDBase.C3, 11);
    uint num87 = Bits.RotateLeft32(RIPEMD.P3(num86, num85, num84) + num83 + this.data[10] + MDBase.C3, 13);
    uint num88 = Bits.RotateLeft32(RIPEMD.P3(num87, num86, num85) + num84 + this.data[2] + MDBase.C3, 14);
    uint num89 = Bits.RotateLeft32(RIPEMD.P3(num88, num87, num86) + num85 + this.data[4] + MDBase.C3, 7);
    uint num90 = Bits.RotateLeft32(RIPEMD.P3(num89, num88, num87) + num86 + this.data[9] + MDBase.C3, 14);
    uint num91 = Bits.RotateLeft32(RIPEMD.P3(num90, num89, num88) + num87 + this.data[15] + MDBase.C3, 9);
    uint num92 = Bits.RotateLeft32(RIPEMD.P3(num91, num90, num89) + num88 + this.data[8] + MDBase.C3, 13);
    uint num93 = Bits.RotateLeft32(RIPEMD.P3(num92, num91, num90) + num89 + this.data[1] + MDBase.C3, 15);
    uint num94 = Bits.RotateLeft32(RIPEMD.P3(num93, num92, num91) + num90 + this.data[14] + MDBase.C3, 6);
    uint num95 = Bits.RotateLeft32(RIPEMD.P3(num94, num93, num92) + num91 + this.data[7] + MDBase.C3, 8);
    uint num96 = Bits.RotateLeft32(RIPEMD.P3(num95, num94, num93) + num92 + this.data[0] + MDBase.C3, 13);
    uint num97 = Bits.RotateLeft32(RIPEMD.P3(num96, num95, num94) + num93 + this.data[6] + MDBase.C3, 6);
    uint num98 = Bits.RotateLeft32(RIPEMD.P3(num97, num96, num95) + num94 + this.data[11] + MDBase.C3, 12);
    uint num99 = Bits.RotateLeft32(RIPEMD.P3(num98, num97, num96) + num95 + this.data[13] + MDBase.C3, 5);
    uint a2 = Bits.RotateLeft32(RIPEMD.P3(num99, num98, num97) + num96 + this.data[5] + MDBase.C3, 7);
    uint num100 = Bits.RotateLeft32(RIPEMD.P3(a2, num99, num98) + num97 + this.data[12] + MDBase.C3, 5);
    uint num101 = a2 + this.state[0] + num53;
    this.state[0] = this.state[1] + a1 + num99;
    this.state[1] = this.state[2] + num52 + num98;
    this.state[2] = this.state[3] + num51 + num100;
    this.state[3] = num101;
    Intermech.Hashes.Utils.Utils.Memset(ref this.data, (byte) 0);
  }
}
