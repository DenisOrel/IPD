// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Shake_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Shake_256 : Shake
{
  public Shake_256()
    : base(32 /*0x20*/)
  {
  }

  public override IHash Clone()
  {
    Shake_256 shake256_1 = new Shake_256();
    shake256_1.XOFSizeInBits = this.XOFSizeInBits;
    Shake_256 shake256_2 = shake256_1 as Shake_256;
    shake256_2.BufferPosition = this.BufferPosition;
    shake256_2.DigestPosition = this.DigestPosition;
    shake256_2.ShakeBufferPosition = this.ShakeBufferPosition;
    shake256_2.Finalized = this.Finalized;
    shake256_2.ShakeBuffer = this.ShakeBuffer.DeepCopy();
    shake256_2.buffer = this.buffer.Clone();
    shake256_2.processed_bytes = this.processed_bytes;
    shake256_2.state = this.state.DeepCopy();
    shake256_2.BufferSize = this.BufferSize;
    return (IHash) shake256_2;
  }
}
