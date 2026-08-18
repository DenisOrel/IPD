// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.FNV64
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal sealed class FNV64 : Hash, IHash64, IHash, ITransformBlock
{
  private ulong hash;

  public FNV64()
    : base(8, 1)
  {
  }

  public override IHash Clone()
  {
    FNV64 fnV64 = new FNV64();
    fnV64.hash = this.hash;
    fnV64.BufferSize = this.BufferSize;
    return (IHash) fnV64;
  }

  public override void Initialize() => this.hash = 0UL;

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
      this.hash = this.hash * 1099511628211UL /*0x0100000001B3*/ ^ (ulong) a_data[index];
      ++index;
    }
  }
}
