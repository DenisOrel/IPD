// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Keccak_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Keccak_224 : SHA3
{
  public Keccak_224()
    : base(28)
  {
    this.hash_mode = HashMode.Keccak;
  }

  public override IHash Clone()
  {
    Keccak_224 keccak224 = new Keccak_224();
    keccak224.buffer = this.buffer.Clone();
    keccak224.processed_bytes = this.processed_bytes;
    keccak224.state = this.state.DeepCopy();
    keccak224.BufferSize = this.BufferSize;
    return (IHash) keccak224;
  }
}
