// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger_Base
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger_Base(int a_hash_size, HashRounds a_rounds) : Tiger(a_hash_size, a_rounds)
{
  public override IHash Clone()
  {
    Tiger_Base tigerBase = new Tiger_Base(this.HashSize, Tiger.GetHashRound(this.rounds));
    tigerBase.buffer = this.buffer.Clone();
    tigerBase.processed_bytes = this.processed_bytes;
    tigerBase.hash = this.hash.DeepCopy();
    tigerBase.BufferSize = this.BufferSize;
    return (IHash) tigerBase;
  }
}
