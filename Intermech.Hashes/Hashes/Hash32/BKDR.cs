// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.BKDR
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class BKDR : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;
  private static readonly int SEED = 131;

  public BKDR()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    BKDR bkdr = new BKDR();
    bkdr.hash = this.hash;
    bkdr.BufferSize = this.BufferSize;
    return (IHash) bkdr;
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
      this.hash = this.hash * (uint) BKDR.SEED + (uint) a_data[index];
      ++index;
    }
  }
}
