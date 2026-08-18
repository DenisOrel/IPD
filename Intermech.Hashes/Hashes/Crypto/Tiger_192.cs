// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger_192
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger_192 : Tiger
{
  public override IHash Clone()
  {
    Tiger_192 tiger192 = new Tiger_192(HashSizeEnum.HashSize192, Tiger.GetHashRound(this.rounds));
    tiger192.buffer = this.buffer.Clone();
    tiger192.processed_bytes = this.processed_bytes;
    tiger192.hash = this.hash.DeepCopy();
    tiger192.BufferSize = this.BufferSize;
    return (IHash) tiger192;
  }

  private Tiger_192(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger_192(HashSizeEnum.HashSize192, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger_192(HashSizeEnum.HashSize192, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger_192(HashSizeEnum.HashSize192, HashRounds.Rounds5);
  }
}
