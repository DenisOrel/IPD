// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Keccak_288
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Keccak_288 : SHA3
{
  public Keccak_288()
    : base(36)
  {
    this.hash_mode = HashMode.Keccak;
  }

  public override IHash Clone()
  {
    Keccak_288 keccak288 = new Keccak_288();
    keccak288.buffer = this.buffer.Clone();
    keccak288.processed_bytes = this.processed_bytes;
    keccak288.state = this.state.DeepCopy();
    keccak288.BufferSize = this.BufferSize;
    return (IHash) keccak288;
  }
}
