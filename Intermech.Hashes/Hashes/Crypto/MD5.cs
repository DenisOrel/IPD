// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.MD5
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class MD5 : MDBase, ITransformBlock
{
  public MD5()
    : base(4, 16 /*0x10*/)
  {
  }

  public override IHash Clone()
  {
    MD5 md5 = new MD5();
    md5.buffer = this.buffer.Clone();
    md5.processed_bytes = this.processed_bytes;
    md5.state = this.state.DeepCopy();
    md5.BufferSize = this.BufferSize;
    return (IHash) md5;
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] array = new uint[16 /*0x10*/];
    fixed (uint* dest = array)
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, a_data_length);
    uint num1 = this.state[0];
    uint num2 = this.state[1];
    uint num3 = this.state[2];
    uint num4 = this.state[3];
    uint num5 = Bits.RotateLeft32((uint) ((int) array[0] - 680876936 + (int) num1 + ((int) num2 & (int) num3 | ~(int) num2 & (int) num4)), 7) + num2;
    uint num6 = Bits.RotateLeft32((uint) ((int) array[1] - 389564586 + (int) num4 + ((int) num5 & (int) num2 | ~(int) num5 & (int) num3)), 12) + num5;
    uint num7 = Bits.RotateLeft32((uint) ((int) array[2] + 606105819 + (int) num3 + ((int) num6 & (int) num5 | ~(int) num6 & (int) num2)), 17) + num6;
    uint num8 = Bits.RotateLeft32((uint) ((int) array[3] - 1044525330 + (int) num2 + ((int) num7 & (int) num6 | ~(int) num7 & (int) num5)), 22) + num7;
    uint num9 = Bits.RotateLeft32((uint) ((int) array[4] - 176418897 + (int) num5 + ((int) num8 & (int) num7 | ~(int) num8 & (int) num6)), 7) + num8;
    uint num10 = Bits.RotateLeft32((uint) ((int) array[5] + 1200080426 + (int) num6 + ((int) num9 & (int) num8 | ~(int) num9 & (int) num7)), 12) + num9;
    uint num11 = Bits.RotateLeft32((uint) ((int) array[6] - 1473231341 + (int) num7 + ((int) num10 & (int) num9 | ~(int) num10 & (int) num8)), 17) + num10;
    uint num12 = Bits.RotateLeft32((uint) ((int) array[7] - 45705983 + (int) num8 + ((int) num11 & (int) num10 | ~(int) num11 & (int) num9)), 22) + num11;
    uint num13 = Bits.RotateLeft32((uint) ((int) array[8] + 1770035416 + (int) num9 + ((int) num12 & (int) num11 | ~(int) num12 & (int) num10)), 7) + num12;
    uint num14 = Bits.RotateLeft32((uint) ((int) array[9] - 1958414417 + (int) num10 + ((int) num13 & (int) num12 | ~(int) num13 & (int) num11)), 12) + num13;
    uint num15 = Bits.RotateLeft32((uint) ((int) array[10] - 42063 + (int) num11 + ((int) num14 & (int) num13 | ~(int) num14 & (int) num12)), 17) + num14;
    uint num16 = Bits.RotateLeft32((uint) ((int) array[11] - 1990404162 + (int) num12 + ((int) num15 & (int) num14 | ~(int) num15 & (int) num13)), 22) + num15;
    uint num17 = Bits.RotateLeft32((uint) ((int) array[12] + 1804603682 + (int) num13 + ((int) num16 & (int) num15 | ~(int) num16 & (int) num14)), 7) + num16;
    uint num18 = Bits.RotateLeft32((uint) ((int) array[13] - 40341101 + (int) num14 + ((int) num17 & (int) num16 | ~(int) num17 & (int) num15)), 12) + num17;
    uint num19 = Bits.RotateLeft32((uint) ((int) array[14] - 1502002290 + (int) num15 + ((int) num18 & (int) num17 | ~(int) num18 & (int) num16)), 17) + num18;
    uint num20 = Bits.RotateLeft32((uint) ((int) array[15] + 1236535329 + (int) num16 + ((int) num19 & (int) num18 | ~(int) num19 & (int) num17)), 22) + num19;
    uint num21 = Bits.RotateLeft32((uint) ((int) array[1] - 165796510 + (int) num17 + ((int) num20 & (int) num18 | (int) num19 & ~(int) num18)), 5) + num20;
    uint num22 = Bits.RotateLeft32((uint) ((int) array[6] - 1069501632 + (int) num18 + ((int) num21 & (int) num19 | (int) num20 & ~(int) num19)), 9) + num21;
    uint num23 = Bits.RotateLeft32((uint) ((int) array[11] + 643717713 + (int) num19 + ((int) num22 & (int) num20 | (int) num21 & ~(int) num20)), 14) + num22;
    uint num24 = Bits.RotateLeft32((uint) ((int) array[0] - 373897302 + (int) num20 + ((int) num23 & (int) num21 | (int) num22 & ~(int) num21)), 20) + num23;
    uint num25 = Bits.RotateLeft32((uint) ((int) array[5] - 701558691 + (int) num21 + ((int) num24 & (int) num22 | (int) num23 & ~(int) num22)), 5) + num24;
    uint num26 = Bits.RotateLeft32((uint) ((int) array[10] + 38016083 + (int) num22 + ((int) num25 & (int) num23 | (int) num24 & ~(int) num23)), 9) + num25;
    uint num27 = Bits.RotateLeft32((uint) ((int) array[15] - 660478335 + (int) num23 + ((int) num26 & (int) num24 | (int) num25 & ~(int) num24)), 14) + num26;
    uint num28 = Bits.RotateLeft32((uint) ((int) array[4] - 405537848 + (int) num24 + ((int) num27 & (int) num25 | (int) num26 & ~(int) num25)), 20) + num27;
    uint num29 = Bits.RotateLeft32((uint) ((int) array[9] + 568446438 + (int) num25 + ((int) num28 & (int) num26 | (int) num27 & ~(int) num26)), 5) + num28;
    uint num30 = Bits.RotateLeft32((uint) ((int) array[14] - 1019803690 + (int) num26 + ((int) num29 & (int) num27 | (int) num28 & ~(int) num27)), 9) + num29;
    uint num31 = Bits.RotateLeft32((uint) ((int) array[3] - 187363961 + (int) num27 + ((int) num30 & (int) num28 | (int) num29 & ~(int) num28)), 14) + num30;
    uint num32 = Bits.RotateLeft32((uint) ((int) array[8] + 1163531501 + (int) num28 + ((int) num31 & (int) num29 | (int) num30 & ~(int) num29)), 20) + num31;
    uint num33 = Bits.RotateLeft32((uint) ((int) array[13] - 1444681467 + (int) num29 + ((int) num32 & (int) num30 | (int) num31 & ~(int) num30)), 5) + num32;
    uint num34 = Bits.RotateLeft32((uint) ((int) array[2] - 51403784 + (int) num30 + ((int) num33 & (int) num31 | (int) num32 & ~(int) num31)), 9) + num33;
    uint num35 = Bits.RotateLeft32((uint) ((int) array[7] + 1735328473 + (int) num31 + ((int) num34 & (int) num32 | (int) num33 & ~(int) num32)), 14) + num34;
    uint num36 = Bits.RotateLeft32((uint) ((int) array[12] - 1926607734 + (int) num32 + ((int) num35 & (int) num33 | (int) num34 & ~(int) num33)), 20) + num35;
    uint num37 = Bits.RotateLeft32((uint) ((int) array[5] - 378558 + (int) num33 + ((int) num36 ^ (int) num35 ^ (int) num34)), 4) + num36;
    uint num38 = Bits.RotateLeft32((uint) ((int) array[8] - 2022574463 + (int) num34 + ((int) num37 ^ (int) num36 ^ (int) num35)), 11) + num37;
    uint num39 = Bits.RotateLeft32((uint) ((int) array[11] + 1839030562 + (int) num35 + ((int) num38 ^ (int) num37 ^ (int) num36)), 16 /*0x10*/) + num38;
    uint num40 = Bits.RotateLeft32((uint) ((int) array[14] - 35309556 + (int) num36 + ((int) num39 ^ (int) num38 ^ (int) num37)), 23) + num39;
    uint num41 = Bits.RotateLeft32((uint) ((int) array[1] - 1530992060 + (int) num37 + ((int) num40 ^ (int) num39 ^ (int) num38)), 4) + num40;
    uint num42 = Bits.RotateLeft32((uint) ((int) array[4] + 1272893353 + (int) num38 + ((int) num41 ^ (int) num40 ^ (int) num39)), 11) + num41;
    uint num43 = Bits.RotateLeft32((uint) ((int) array[7] - 155497632 + (int) num39 + ((int) num42 ^ (int) num41 ^ (int) num40)), 16 /*0x10*/) + num42;
    uint num44 = Bits.RotateLeft32((uint) ((int) array[10] - 1094730640 + (int) num40 + ((int) num43 ^ (int) num42 ^ (int) num41)), 23) + num43;
    uint num45 = Bits.RotateLeft32((uint) ((int) array[13] + 681279174 + (int) num41 + ((int) num44 ^ (int) num43 ^ (int) num42)), 4) + num44;
    uint num46 = Bits.RotateLeft32((uint) ((int) array[0] - 358537222 + (int) num42 + ((int) num45 ^ (int) num44 ^ (int) num43)), 11) + num45;
    uint num47 = Bits.RotateLeft32((uint) ((int) array[3] - 722521979 + (int) num43 + ((int) num46 ^ (int) num45 ^ (int) num44)), 16 /*0x10*/) + num46;
    uint num48 = Bits.RotateLeft32((uint) ((int) array[6] + 76029189 + (int) num44 + ((int) num47 ^ (int) num46 ^ (int) num45)), 23) + num47;
    uint num49 = Bits.RotateLeft32((uint) ((int) array[9] - 640364487 + (int) num45 + ((int) num48 ^ (int) num47 ^ (int) num46)), 4) + num48;
    uint num50 = Bits.RotateLeft32((uint) ((int) array[12] - 421815835 + (int) num46 + ((int) num49 ^ (int) num48 ^ (int) num47)), 11) + num49;
    uint num51 = Bits.RotateLeft32((uint) ((int) array[15] + 530742520 + (int) num47 + ((int) num50 ^ (int) num49 ^ (int) num48)), 16 /*0x10*/) + num50;
    uint num52 = Bits.RotateLeft32((uint) ((int) array[2] - 995338651 + (int) num48 + ((int) num51 ^ (int) num50 ^ (int) num49)), 23) + num51;
    uint num53 = Bits.RotateLeft32((uint) ((int) array[0] - 198630844 + (int) num49 + ((int) num51 ^ ((int) num52 | ~(int) num50))), 6) + num52;
    uint num54 = Bits.RotateLeft32((uint) ((int) array[7] + 1126891415 + (int) num50 + ((int) num52 ^ ((int) num53 | ~(int) num51))), 10) + num53;
    uint num55 = Bits.RotateLeft32((uint) ((int) array[14] - 1416354905 + (int) num51 + ((int) num53 ^ ((int) num54 | ~(int) num52))), 15) + num54;
    uint num56 = Bits.RotateLeft32((uint) ((int) array[5] - 57434055 + (int) num52 + ((int) num54 ^ ((int) num55 | ~(int) num53))), 21) + num55;
    uint num57 = Bits.RotateLeft32((uint) ((int) array[12] + 1700485571 + (int) num53 + ((int) num55 ^ ((int) num56 | ~(int) num54))), 6) + num56;
    uint num58 = Bits.RotateLeft32((uint) ((int) array[3] - 1894986606 + (int) num54 + ((int) num56 ^ ((int) num57 | ~(int) num55))), 10) + num57;
    uint num59 = Bits.RotateLeft32((uint) ((int) array[10] - 1051523 + (int) num55 + ((int) num57 ^ ((int) num58 | ~(int) num56))), 15) + num58;
    uint num60 = Bits.RotateLeft32((uint) ((int) array[1] - 2054922799 + (int) num56 + ((int) num58 ^ ((int) num59 | ~(int) num57))), 21) + num59;
    uint num61 = Bits.RotateLeft32((uint) ((int) array[8] + 1873313359 + (int) num57 + ((int) num59 ^ ((int) num60 | ~(int) num58))), 6) + num60;
    uint num62 = Bits.RotateLeft32((uint) ((int) array[15] - 30611744 + (int) num58 + ((int) num60 ^ ((int) num61 | ~(int) num59))), 10) + num61;
    uint num63 = Bits.RotateLeft32((uint) ((int) array[6] - 1560198380 + (int) num59 + ((int) num61 ^ ((int) num62 | ~(int) num60))), 15) + num62;
    uint num64 = Bits.RotateLeft32((uint) ((int) array[13] + 1309151649 + (int) num60 + ((int) num62 ^ ((int) num63 | ~(int) num61))), 21) + num63;
    uint num65 = Bits.RotateLeft32((uint) ((int) array[4] - 145523070 + (int) num61 + ((int) num63 ^ ((int) num64 | ~(int) num62))), 6) + num64;
    uint num66 = Bits.RotateLeft32((uint) ((int) array[11] - 1120210379 + (int) num62 + ((int) num64 ^ ((int) num65 | ~(int) num63))), 10) + num65;
    uint num67 = Bits.RotateLeft32((uint) ((int) array[2] + 718787259 + (int) num63 + ((int) num65 ^ ((int) num66 | ~(int) num64))), 15) + num66;
    uint num68 = Bits.RotateLeft32((uint) ((int) array[9] - 343485551 + (int) num64 + ((int) num66 ^ ((int) num67 | ~(int) num65))), 21) + num67;
    this.state[0] = this.state[0] + num65;
    this.state[1] = this.state[1] + num68;
    this.state[2] = this.state[2] + num67;
    this.state[3] = this.state[3] + num66;
    Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
  }
}
