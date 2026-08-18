// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger2_Base
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Tiger2_Base(int a_hash_size, HashRounds a_rounds) : Tiger2(a_hash_size, a_rounds)
{
  public override IHash Clone()
  {
    Tiger2_Base tiger2Base = new Tiger2_Base(this.HashSize, Tiger.GetHashRound(this.rounds));
    tiger2Base.buffer = this.buffer.Clone();
    tiger2Base.processed_bytes = this.processed_bytes;
    tiger2Base.hash = this.hash.DeepCopy();
    tiger2Base.BufferSize = this.BufferSize;
    return (IHash) tiger2Base;
  }
}
