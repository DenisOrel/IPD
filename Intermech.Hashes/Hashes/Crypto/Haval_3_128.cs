// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval_3_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Haval_3_128 : Haval3
{
  public Haval_3_128()
    : base(HashSizeEnum.HashSize128)
  {
  }

  public override IHash Clone()
  {
    Haval_3_128 haval3128 = new Haval_3_128();
    haval3128.buffer = this.buffer.Clone();
    haval3128.processed_bytes = this.processed_bytes;
    haval3128.rounds = this.rounds;
    haval3128.hash = this.hash.DeepCopy();
    haval3128.BufferSize = this.BufferSize;
    return (IHash) haval3128;
  }
}
