// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.SipHash2_4
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal sealed class SipHash2_4 : SipHash
{
  public SipHash2_4()
    : base()
  {
  }

  public override IHash Clone()
  {
    SipHash2_4 sipHash24 = new SipHash2_4();
    sipHash24.v0 = this.v0;
    sipHash24.v1 = this.v1;
    sipHash24.v2 = this.v2;
    sipHash24.v3 = this.v3;
    sipHash24.key0 = this.key0;
    sipHash24.key1 = this.key1;
    sipHash24.total_length = this.total_length;
    sipHash24.cr = this.cr;
    sipHash24.fr = this.fr;
    sipHash24.idx = this.idx;
    sipHash24.buf = this.buf.DeepCopy();
    sipHash24.BufferSize = this.BufferSize;
    return (IHash) sipHash24;
  }
}
