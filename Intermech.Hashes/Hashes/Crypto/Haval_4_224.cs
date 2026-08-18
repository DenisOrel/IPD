// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_4_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_4_224 : Haval4
{
  public Haval_4_224()
    : base(HashSizeEnum.HashSize224)
  {
  }

  public override IHash Clone()
  {
    Haval_4_224 haval4224 = new Haval_4_224();
    haval4224.buffer = this.buffer.Clone();
    haval4224.processed_bytes = this.processed_bytes;
    haval4224.rounds = this.rounds;
    haval4224.hash = this.hash.DeepCopy();
    haval4224.BufferSize = this.BufferSize;
    return (IHash) haval4224;
  }
}
