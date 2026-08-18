// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_5_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_5_224 : Haval5
{
  public Haval_5_224()
    : base(HashSizeEnum.HashSize224)
  {
  }

  public override IHash Clone()
  {
    Haval_5_224 haval5224 = new Haval_5_224();
    haval5224.buffer = this.buffer.Clone();
    haval5224.processed_bytes = this.processed_bytes;
    haval5224.rounds = this.rounds;
    haval5224.hash = this.hash.DeepCopy();
    haval5224.BufferSize = this.BufferSize;
    return (IHash) haval5224;
  }
}
