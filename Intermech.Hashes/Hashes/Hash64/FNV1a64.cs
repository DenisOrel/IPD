// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.FNV1a64
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal sealed class FNV1a64 : Hash, IHash64, IHash, ITransformBlock
{
  private ulong hash;

  public FNV1a64()
    : base(8, 1)
  {
  }

  public override IHash Clone()
  {
    FNV1a64 fnV1a64 = new FNV1a64();
    fnV1a64.hash = this.hash;
    fnV1a64.BufferSize = this.BufferSize;
    return (IHash) fnV1a64;
  }

  public override void Initialize() => this.hash = 14695981039346656037UL;

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
      this.hash = (ulong) (((long) this.hash ^ (long) a_data[index]) * 1099511628211L /*0x0100000001B3*/);
      ++index;
    }
  }
}
