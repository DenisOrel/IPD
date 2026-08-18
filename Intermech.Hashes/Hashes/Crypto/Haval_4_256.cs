// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_4_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_4_256 : Haval4
{
  public Haval_4_256()
    : base(HashSizeEnum.HashSize256)
  {
  }

  public override IHash Clone()
  {
    Haval_4_256 haval4256 = new Haval_4_256();
    haval4256.buffer = this.buffer.Clone();
    haval4256.processed_bytes = this.processed_bytes;
    haval4256.rounds = this.rounds;
    haval4256.hash = this.hash.DeepCopy();
    haval4256.BufferSize = this.BufferSize;
    return (IHash) haval4256;
  }
}
