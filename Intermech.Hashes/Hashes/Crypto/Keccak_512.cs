// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Keccak_512
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Keccak_512 : SHA3
{
  public Keccak_512()
    : base(64 /*0x40*/)
  {
    this.hash_mode = HashMode.Keccak;
  }

  public override IHash Clone()
  {
    Keccak_512 keccak512 = new Keccak_512();
    keccak512.buffer = this.buffer.Clone();
    keccak512.processed_bytes = this.processed_bytes;
    keccak512.state = this.state.DeepCopy();
    keccak512.BufferSize = this.BufferSize;
    return (IHash) keccak512;
  }
}
