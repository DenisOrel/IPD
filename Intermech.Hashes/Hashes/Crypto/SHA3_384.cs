// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA3_384
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA3_384 : SHA3
{
  public SHA3_384()
    : base(48 /*0x30*/)
  {
    this.hash_mode = HashMode.SHA3;
  }

  public override IHash Clone()
  {
    SHA3_384 shA3384 = new SHA3_384();
    shA3384.buffer = this.buffer.Clone();
    shA3384.processed_bytes = this.processed_bytes;
    shA3384.state = this.state.DeepCopy();
    shA3384.BufferSize = this.BufferSize;
    return (IHash) shA3384;
  }
}
