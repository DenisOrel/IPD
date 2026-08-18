// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger2_192
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger2_192 : Tiger2
{
  public override IHash Clone()
  {
    Tiger2_192 tiger2192 = new Tiger2_192(HashSizeEnum.HashSize192, Tiger.GetHashRound(this.rounds));
    tiger2192.buffer = this.buffer.Clone();
    tiger2192.processed_bytes = this.processed_bytes;
    tiger2192.hash = this.hash.DeepCopy();
    tiger2192.BufferSize = this.BufferSize;
    return (IHash) tiger2192;
  }

  private Tiger2_192(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger2_192(HashSizeEnum.HashSize192, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger2_192(HashSizeEnum.HashSize192, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger2_192(HashSizeEnum.HashSize192, HashRounds.Rounds5);
  }
}
