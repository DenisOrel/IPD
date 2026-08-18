// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.PJW
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class PJW : Hash, IHash32, IHash, ITransformBlock
{
  private static readonly uint UInt32MaxValue = uint.MaxValue;
  private static readonly uint BitsInUnsignedInt = 32 /*0x20*/;
  private static readonly uint threeQuarters = PJW.BitsInUnsignedInt * 3U >> 2;
  private static readonly uint oneEighth = PJW.BitsInUnsignedInt >> 3;
  private static readonly uint highBits = PJW.UInt32MaxValue << (int) PJW.BitsInUnsignedInt - (int) PJW.oneEighth;
  private uint hash;

  public PJW()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    PJW pjw = new PJW();
    pjw.hash = this.hash;
    pjw.BufferSize = this.BufferSize;
    return (IHash) pjw;
  }

  public override void Initialize() => this.hash = 0U;

  public override IHashResult TransformFinal()
  {
    HashResult hashResult = new HashResult(this.hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int index = a_index;
    for (; a_length > 0; --a_length)
    {
      this.hash = (this.hash << (int) PJW.oneEighth) + (uint) a_data[index];
      uint num = this.hash & PJW.highBits;
      if (num != 0U)
        this.hash = (uint) (((int) this.hash ^ (int) (num >> (int) PJW.threeQuarters)) & ~(int) PJW.highBits);
      ++index;
    }
  }
}
