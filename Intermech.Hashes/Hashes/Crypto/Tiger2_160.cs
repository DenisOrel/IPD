// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger2_160
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger2_160 : Tiger2
{
  public override IHash Clone()
  {
    Tiger2_160 tiger2160 = new Tiger2_160(HashSizeEnum.HashSize160, Tiger.GetHashRound(this.rounds));
    tiger2160.buffer = this.buffer.Clone();
    tiger2160.processed_bytes = this.processed_bytes;
    tiger2160.hash = this.hash.DeepCopy();
    tiger2160.BufferSize = this.BufferSize;
    return (IHash) tiger2160;
  }

  private Tiger2_160(HashSizeEnum a_hash_size, HashRounds a_rounds)
    : base((int) a_hash_size, a_rounds)
  {
  }

  public static IHash CreateRound3()
  {
    return (IHash) new Tiger2_160(HashSizeEnum.HashSize160, HashRounds.Rounds3);
  }

  public static IHash CreateRound4()
  {
    return (IHash) new Tiger2_160(HashSizeEnum.HashSize160, HashRounds.Rounds4);
  }

  public static IHash CreateRound5()
  {
    return (IHash) new Tiger2_160(HashSizeEnum.HashSize160, HashRounds.Rounds5);
  }
}
