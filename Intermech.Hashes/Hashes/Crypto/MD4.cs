// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.MD4
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class MD4 : MDBase, ITransformBlock
{
  private uint[] data;

  public MD4()
    : base(4, 16 /*0x10*/)
  {
    this.data = new uint[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    MD4 md4 = new MD4();
    md4.buffer = this.buffer.Clone();
    md4.processed_bytes = this.processed_bytes;
    md4.state = this.state.DeepCopy();
    md4.BufferSize = this.BufferSize;
    return (IHash) md4;
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    fixed (uint* dest = this.data)
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
    uint num1 = this.state[0];
    uint num2 = this.state[1];
    uint num3 = this.state[2];
    uint num4 = this.state[3];
    uint num5 = Bits.RotateLeft32(num1 + (this.data[0] + (uint) ((int) num2 & (int) num3 | ~(int) num2 & (int) num4)), 3);
    uint num6 = Bits.RotateLeft32(num4 + (this.data[1] + (uint) ((int) num5 & (int) num2 | ~(int) num5 & (int) num3)), 7);
    uint num7 = Bits.RotateLeft32(num3 + (this.data[2] + (uint) ((int) num6 & (int) num5 | ~(int) num6 & (int) num2)), 11);
    uint num8 = Bits.RotateLeft32(num2 + (this.data[3] + (uint) ((int) num7 & (int) num6 | ~(int) num7 & (int) num5)), 19);
    uint num9 = Bits.RotateLeft32(num5 + (this.data[4] + (uint) ((int) num8 & (int) num7 | ~(int) num8 & (int) num6)), 3);
    uint num10 = Bits.RotateLeft32(num6 + (this.data[5] + (uint) ((int) num9 & (int) num8 | ~(int) num9 & (int) num7)), 7);
    uint num11 = Bits.RotateLeft32(num7 + (this.data[6] + (uint) ((int) num10 & (int) num9 | ~(int) num10 & (int) num8)), 11);
    uint num12 = Bits.RotateLeft32(num8 + (this.data[7] + (uint) ((int) num11 & (int) num10 | ~(int) num11 & (int) num9)), 19);
    uint num13 = Bits.RotateLeft32(num9 + (this.data[8] + (uint) ((int) num12 & (int) num11 | ~(int) num12 & (int) num10)), 3);
    uint num14 = Bits.RotateLeft32(num10 + (this.data[9] + (uint) ((int) num13 & (int) num12 | ~(int) num13 & (int) num11)), 7);
    uint num15 = Bits.RotateLeft32(num11 + (this.data[10] + (uint) ((int) num14 & (int) num13 | ~(int) num14 & (int) num12)), 11);
    uint num16 = Bits.RotateLeft32(num12 + (this.data[11] + (uint) ((int) num15 & (int) num14 | ~(int) num15 & (int) num13)), 19);
    uint num17 = Bits.RotateLeft32(num13 + (this.data[12] + (uint) ((int) num16 & (int) num15 | ~(int) num16 & (int) num14)), 3);
    uint num18 = Bits.RotateLeft32(num14 + (this.data[13] + (uint) ((int) num17 & (int) num16 | ~(int) num17 & (int) num15)), 7);
    uint num19 = Bits.RotateLeft32(num15 + (this.data[14] + (uint) ((int) num18 & (int) num17 | ~(int) num18 & (int) num16)), 11);
    uint num20 = Bits.RotateLeft32(num16 + (this.data[15] + (uint) ((int) num19 & (int) num18 | ~(int) num19 & (int) num17)), 19);
    uint num21 = Bits.RotateLeft32(num17 + (uint) ((int) this.data[0] + (int) MDBase.C2 + ((int) num20 & ((int) num19 | (int) num18) | (int) num19 & (int) num18)), 3);
    uint num22 = Bits.RotateLeft32(num18 + (uint) ((int) this.data[4] + (int) MDBase.C2 + ((int) num21 & ((int) num20 | (int) num19) | (int) num20 & (int) num19)), 5);
    uint num23 = Bits.RotateLeft32(num19 + (uint) ((int) this.data[8] + (int) MDBase.C2 + ((int) num22 & ((int) num21 | (int) num20) | (int) num21 & (int) num20)), 9);
    uint num24 = Bits.RotateLeft32(num20 + (uint) ((int) this.data[12] + (int) MDBase.C2 + ((int) num23 & ((int) num22 | (int) num21) | (int) num22 & (int) num21)), 13);
    uint num25 = Bits.RotateLeft32(num21 + (uint) ((int) this.data[1] + (int) MDBase.C2 + ((int) num24 & ((int) num23 | (int) num22) | (int) num23 & (int) num22)), 3);
    uint num26 = Bits.RotateLeft32(num22 + (uint) ((int) this.data[5] + (int) MDBase.C2 + ((int) num25 & ((int) num24 | (int) num23) | (int) num24 & (int) num23)), 5);
    uint num27 = Bits.RotateLeft32(num23 + (uint) ((int) this.data[9] + (int) MDBase.C2 + ((int) num26 & ((int) num25 | (int) num24) | (int) num25 & (int) num24)), 9);
    uint num28 = Bits.RotateLeft32(num24 + (uint) ((int) this.data[13] + (int) MDBase.C2 + ((int) num27 & ((int) num26 | (int) num25) | (int) num26 & (int) num25)), 13);
    uint num29 = Bits.RotateLeft32(num25 + (uint) ((int) this.data[2] + (int) MDBase.C2 + ((int) num28 & ((int) num27 | (int) num26) | (int) num27 & (int) num26)), 3);
    uint num30 = Bits.RotateLeft32(num26 + (uint) ((int) this.data[6] + (int) MDBase.C2 + ((int) num29 & ((int) num28 | (int) num27) | (int) num28 & (int) num27)), 5);
    uint num31 = Bits.RotateLeft32(num27 + (uint) ((int) this.data[10] + (int) MDBase.C2 + ((int) num30 & ((int) num29 | (int) num28) | (int) num29 & (int) num28)), 9);
    uint num32 = Bits.RotateLeft32(num28 + (uint) ((int) this.data[14] + (int) MDBase.C2 + ((int) num31 & ((int) num30 | (int) num29) | (int) num30 & (int) num29)), 13);
    uint num33 = Bits.RotateLeft32(num29 + (uint) ((int) this.data[3] + (int) MDBase.C2 + ((int) num32 & ((int) num31 | (int) num30) | (int) num31 & (int) num30)), 3);
    uint num34 = Bits.RotateLeft32(num30 + (uint) ((int) this.data[7] + (int) MDBase.C2 + ((int) num33 & ((int) num32 | (int) num31) | (int) num32 & (int) num31)), 5);
    uint num35 = Bits.RotateLeft32(num31 + (uint) ((int) this.data[11] + (int) MDBase.C2 + ((int) num34 & ((int) num33 | (int) num32) | (int) num33 & (int) num32)), 9);
    uint num36 = Bits.RotateLeft32(num32 + (uint) ((int) this.data[15] + (int) MDBase.C2 + ((int) num35 & ((int) num34 | (int) num33) | (int) num34 & (int) num33)), 13);
    uint num37 = Bits.RotateLeft32(num33 + (uint) ((int) this.data[0] + (int) MDBase.C4 + ((int) num36 ^ (int) num35 ^ (int) num34)), 3);
    uint num38 = Bits.RotateLeft32(num34 + (uint) ((int) this.data[8] + (int) MDBase.C4 + ((int) num37 ^ (int) num36 ^ (int) num35)), 9);
    uint num39 = Bits.RotateLeft32(num35 + (uint) ((int) this.data[4] + (int) MDBase.C4 + ((int) num38 ^ (int) num37 ^ (int) num36)), 11);
    uint num40 = Bits.RotateLeft32(num36 + (uint) ((int) this.data[12] + (int) MDBase.C4 + ((int) num39 ^ (int) num38 ^ (int) num37)), 15);
    uint num41 = Bits.RotateLeft32(num37 + (uint) ((int) this.data[2] + (int) MDBase.C4 + ((int) num40 ^ (int) num39 ^ (int) num38)), 3);
    uint num42 = Bits.RotateLeft32(num38 + (uint) ((int) this.data[10] + (int) MDBase.C4 + ((int) num41 ^ (int) num40 ^ (int) num39)), 9);
    uint num43 = Bits.RotateLeft32(num39 + (uint) ((int) this.data[6] + (int) MDBase.C4 + ((int) num42 ^ (int) num41 ^ (int) num40)), 11);
    uint num44 = Bits.RotateLeft32(num40 + (uint) ((int) this.data[14] + (int) MDBase.C4 + ((int) num43 ^ (int) num42 ^ (int) num41)), 15);
    uint num45 = Bits.RotateLeft32(num41 + (uint) ((int) this.data[1] + (int) MDBase.C4 + ((int) num44 ^ (int) num43 ^ (int) num42)), 3);
    uint num46 = Bits.RotateLeft32(num42 + (uint) ((int) this.data[9] + (int) MDBase.C4 + ((int) num45 ^ (int) num44 ^ (int) num43)), 9);
    uint num47 = Bits.RotateLeft32(num43 + (uint) ((int) this.data[5] + (int) MDBase.C4 + ((int) num46 ^ (int) num45 ^ (int) num44)), 11);
    uint num48 = Bits.RotateLeft32(num44 + (uint) ((int) this.data[13] + (int) MDBase.C4 + ((int) num47 ^ (int) num46 ^ (int) num45)), 15);
    uint num49 = Bits.RotateLeft32(num45 + (uint) ((int) this.data[3] + (int) MDBase.C4 + ((int) num48 ^ (int) num47 ^ (int) num46)), 3);
    uint num50 = Bits.RotateLeft32(num46 + (uint) ((int) this.data[11] + (int) MDBase.C4 + ((int) num49 ^ (int) num48 ^ (int) num47)), 9);
    uint num51 = Bits.RotateLeft32(num47 + (uint) ((int) this.data[7] + (int) MDBase.C4 + ((int) num50 ^ (int) num49 ^ (int) num48)), 11);
    uint num52 = Bits.RotateLeft32(num48 + (uint) ((int) this.data[15] + (int) MDBase.C4 + ((int) num51 ^ (int) num50 ^ (int) num49)), 15);
    this.state[0] = this.state[0] + num49;
    this.state[1] = this.state[1] + num52;
    this.state[2] = this.state[2] + num51;
    this.state[3] = this.state[3] + num50;
    Intermech.Hashes.Utils.Utils.Memset(ref this.data, (byte) 0);
  }
}
