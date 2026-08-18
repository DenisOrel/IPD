// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.AP
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class AP : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;
  private int index;

  public AP()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    AP ap = new AP();
    ap.hash = this.hash;
    ap.index = this.index;
    ap.BufferSize = this.BufferSize;
    return (IHash) ap;
  }

  public override void Initialize()
  {
    this.hash = 2863311530U /*0xAAAAAAAA*/;
    this.index = 0;
  }

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
      if ((this.index & 1) == 0)
        this.hash ^= (uint) ((int) this.hash << 7 ^ (int) a_data[index] * (int) (this.hash >> 3));
      else
        this.hash ^= (uint) ~((int) this.hash << 11 ^ (int) a_data[index] ^ (int) (this.hash >> 5));
      ++this.index;
      ++index;
    }
  }
}
