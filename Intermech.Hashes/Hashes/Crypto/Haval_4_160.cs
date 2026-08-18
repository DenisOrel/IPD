// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_4_160
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_4_160 : Haval4
{
  public Haval_4_160()
    : base(HashSizeEnum.HashSize160)
  {
  }

  public override IHash Clone()
  {
    Haval_4_160 haval4160 = new Haval_4_160();
    haval4160.buffer = this.buffer.Clone();
    haval4160.processed_bytes = this.processed_bytes;
    haval4160.rounds = this.rounds;
    haval4160.hash = this.hash.DeepCopy();
    haval4160.BufferSize = this.BufferSize;
    return (IHash) haval4160;
  }
}
