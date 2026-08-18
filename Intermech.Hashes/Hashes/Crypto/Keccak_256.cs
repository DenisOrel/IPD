// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Keccak_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Keccak_256 : SHA3
{
  public Keccak_256()
    : base(32 /*0x20*/)
  {
    this.hash_mode = HashMode.Keccak;
  }

  public override IHash Clone()
  {
    Keccak_256 keccak256 = new Keccak_256();
    keccak256.buffer = this.buffer.Clone();
    keccak256.processed_bytes = this.processed_bytes;
    keccak256.state = this.state.DeepCopy();
    keccak256.BufferSize = this.BufferSize;
    return (IHash) keccak256;
  }
}
