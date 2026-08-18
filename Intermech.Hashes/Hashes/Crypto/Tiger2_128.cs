// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger2_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger2_128 : Tiger2
{
  public override IHash Clone()
  {
    Tiger2_128 tiger2128 = new Tiger2_128(HashSizeEnum.HashSize128, Tiger.GetHashRound(this.rounds));
    tiger2128.buffer = this.buffer.Clone();
    tiger2128.processed_bytes = this.processed_bytes;
    tiger2128.hash = this.hash.DeepCopy();
    tiger2128.BufferSize = this.BufferSize;
    return (IHash) tiger2128;
  }

  private Tiger2_128(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger2_128(HashSizeEnum.HashSize128, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger2_128(HashSizeEnum.HashSize128, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger2_128(HashSizeEnum.HashSize128, HashRounds.Rounds5);
  }
}
