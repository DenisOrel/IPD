// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA3_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA3_224 : SHA3
{
  public SHA3_224()
    : base(28)
  {
    this.hash_mode = HashMode.SHA3;
  }

  public override IHash Clone()
  {
    SHA3_224 shA3224 = new SHA3_224();
    shA3224.buffer = this.buffer.Clone();
    shA3224.processed_bytes = this.processed_bytes;
    shA3224.state = this.state.DeepCopy();
    shA3224.BufferSize = this.BufferSize;
    return (IHash) shA3224;
  }
}
