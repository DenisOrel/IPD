// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger_128 : Tiger
{
  public override IHash Clone()
  {
    Tiger_128 tiger128 = new Tiger_128(HashSizeEnum.HashSize128, Tiger.GetHashRound(this.rounds));
    tiger128.buffer = this.buffer.Clone();
    tiger128.processed_bytes = this.processed_bytes;
    tiger128.hash = this.hash.DeepCopy();
    tiger128.BufferSize = this.BufferSize;
    return (IHash) tiger128;
  }

  private Tiger_128(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger_128(HashSizeEnum.HashSize128, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger_128(HashSizeEnum.HashSize128, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger_128(HashSizeEnum.HashSize128, HashRounds.Rounds5);
  }
}
