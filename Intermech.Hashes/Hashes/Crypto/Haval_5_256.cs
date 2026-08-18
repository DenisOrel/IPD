// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_5_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_5_256 : Haval5
{
  public Haval_5_256()
    : base(HashSizeEnum.HashSize256)
  {
  }

  public override IHash Clone()
  {
    Haval_5_256 haval5256 = new Haval_5_256();
    haval5256.buffer = this.buffer.Clone();
    haval5256.processed_bytes = this.processed_bytes;
    haval5256.rounds = this.rounds;
    haval5256.hash = this.hash.DeepCopy();
    haval5256.BufferSize = this.BufferSize;
    return (IHash) haval5256;
  }
}
