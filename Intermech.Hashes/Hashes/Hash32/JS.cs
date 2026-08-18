// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.JS
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class JS : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;

  public JS()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    JS js = new JS();
    js.hash = this.hash;
    js.BufferSize = this.BufferSize;
    return (IHash) js;
  }

  public override void Initialize() => this.hash = 1315423911U;

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
      this.hash ^= (this.hash << 5) + (uint) a_data[index] + (this.hash >> 2);
      ++index;
    }
  }
}
