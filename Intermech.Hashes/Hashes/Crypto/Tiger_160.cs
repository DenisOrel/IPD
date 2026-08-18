// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger_160
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger_160 : Tiger
{
  public override IHash Clone()
  {
    Tiger_160 tiger160 = new Tiger_160(HashSizeEnum.HashSize160, Tiger.GetHashRound(this.rounds));
    tiger160.buffer = this.buffer.Clone();
    tiger160.processed_bytes = this.processed_bytes;
    tiger160.hash = this.hash.DeepCopy();
    tiger160.BufferSize = this.BufferSize;
    return (IHash) tiger160;
  }

  private Tiger_160(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger_160(HashSizeEnum.HashSize160, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger_160(HashSizeEnum.HashSize160, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger_160(HashSizeEnum.HashSize160, HashRounds.Rounds5);
  }
}
