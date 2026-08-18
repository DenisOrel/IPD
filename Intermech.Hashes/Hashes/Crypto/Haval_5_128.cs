// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_5_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_5_128 : Haval5
{
  public Haval_5_128()
    : base(HashSizeEnum.HashSize128)
  {
  }

  public override IHash Clone()
  {
    Haval_5_128 haval5128 = new Haval_5_128();
    haval5128.buffer = this.buffer.Clone();
    haval5128.processed_bytes = this.processed_bytes;
    haval5128.rounds = this.rounds;
    haval5128.hash = this.hash.DeepCopy();
    haval5128.BufferSize = this.BufferSize;
    return (IHash) haval5128;
  }
}
