// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.SuperFast
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class SuperFast : MultipleTransformNonBlock, IHash32, IHash, ITransformBlock
{
  public SuperFast()
    : base(4, 4)
  {
  }

  public override IHash Clone()
  {
    SuperFast superFast = new SuperFast();
    superFast.Buffer = new MemoryStream();
    byte[] array = this.Buffer.ToArray();
    superFast.Buffer.Write(array, 0, array.Length);
    superFast.Buffer.Position = this.Buffer.Position;
    superFast.BufferSize = this.BufferSize;
    return (IHash) superFast;
  }

  protected override IHashResult ComputeAggregatedBytes(byte[] a_data)
  {
    if (a_data.Empty())
      return (IHashResult) new HashResult(0);
    int length = a_data.Length;
    uint num1 = (uint) length;
    int index1 = 0;
    for (; length >= 4; length -= 4)
    {
      int num2 = (int) a_data[index1];
      int index2 = index1 + 1;
      int num3 = (int) a_data[index2] << 8;
      int index3 = index2 + 1;
      uint num4 = (uint) (ushort) (num1 + (uint) (num2 | num3));
      int num5 = (int) a_data[index3];
      int index4 = index3 + 1;
      uint num6 = (uint) (((int) (byte) num5 | (int) a_data[index4] << 8) << 11) ^ num4;
      index1 = index4 + 1;
      uint num7 = num4 << 16 /*0x10*/ ^ num6;
      num1 = num7 + (num7 >> 11);
    }
    switch (length)
    {
      case 1:
        int num8 = (int) a_data[index1];
        uint num9 = num1 + (uint) num8;
        uint num10 = num9 ^ num9 << 10;
        num1 = num10 + (num10 >> 1);
        break;
      case 2:
        int num11 = (int) a_data[index1];
        int index5 = index1 + 1;
        int num12 = (int) a_data[index5];
        uint num13 = num1 + (uint) (ushort) (num11 | num12 << 8);
        uint num14 = num13 ^ num13 << 11;
        num1 = num14 + (num14 >> 17);
        break;
      case 3:
        int num15 = (int) a_data[index1];
        int index6 = index1 + 1;
        int num16 = (int) a_data[index6];
        int index7 = index6 + 1;
        uint num17 = num1 + (uint) (ushort) (num15 | num16 << 8);
        uint num18 = num17 ^ num17 << 16 /*0x10*/ ^ (uint) a_data[index7] << 18;
        num1 = num18 + (num18 >> 11);
        break;
    }
    uint num19 = num1 ^ num1 << 3;
    uint num20 = num19 + (num19 >> 5);
    uint num21 = num20 ^ num20 << 4;
    uint num22 = num21 + (num21 >> 17);
    uint num23 = num22 ^ num22 << 25;
    return (IHashResult) new HashResult(num23 + (num23 >> 6));
  }
}
