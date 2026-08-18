// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Keccak_384
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Keccak_384 : SHA3
{
  public Keccak_384()
    : base(48 /*0x30*/)
  {
    this.hash_mode = HashMode.Keccak;
  }

  public override IHash Clone()
  {
    Keccak_384 keccak384 = new Keccak_384();
    keccak384.buffer = this.buffer.Clone();
    keccak384.processed_bytes = this.processed_bytes;
    keccak384.state = this.state.DeepCopy();
    keccak384.BufferSize = this.BufferSize;
    return (IHash) keccak384;
  }
}
