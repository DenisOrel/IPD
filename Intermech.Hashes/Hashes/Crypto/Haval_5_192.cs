// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_5_192
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_5_192 : Haval5
{
  public Haval_5_192()
    : base(HashSizeEnum.HashSize192)
  {
  }

  public override IHash Clone()
  {
    Haval_5_192 haval5192 = new Haval_5_192();
    haval5192.buffer = this.buffer.Clone();
    haval5192.processed_bytes = this.processed_bytes;
    haval5192.rounds = this.rounds;
    haval5192.hash = this.hash.DeepCopy();
    haval5192.BufferSize = this.BufferSize;
    return (IHash) haval5192;
  }
}
