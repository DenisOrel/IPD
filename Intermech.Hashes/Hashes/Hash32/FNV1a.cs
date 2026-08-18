// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.FNV1a
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class FNV1a : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;

  public FNV1a()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    FNV1a fnV1a = new FNV1a();
    fnV1a.hash = this.hash;
    fnV1a.BufferSize = this.BufferSize;
    return (IHash) fnV1a;
  }

  public override void Initialize() => this.hash = 2166136261U;

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
      this.hash = (uint) (((int) this.hash ^ (int) a_data[index]) * 16777619);
      ++index;
    }
  }
}
