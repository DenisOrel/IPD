// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.OneAtTime
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class OneAtTime : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;

  public OneAtTime()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    OneAtTime oneAtTime = new OneAtTime();
    oneAtTime.hash = this.hash;
    oneAtTime.BufferSize = this.BufferSize;
    return (IHash) oneAtTime;
  }

  public override void Initialize() => this.hash = 0U;

  public override IHashResult TransformFinal()
  {
    this.hash += this.hash << 3;
    this.hash ^= this.hash >> 11;
    this.hash += this.hash << 15;
    HashResult hashResult = new HashResult(this.hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int index = a_index;
    for (; a_length > 0; --a_length)
    {
      this.hash += (uint) a_data[index];
      this.hash += this.hash << 10;
      this.hash ^= this.hash >> 6;
      ++index;
    }
  }
}
