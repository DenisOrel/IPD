// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Shake_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Shake_128 : Shake
{
  public Shake_128()
    : base(16 /*0x10*/)
  {
  }

  public override IHash Clone()
  {
    Shake_128 shake128_1 = new Shake_128();
    shake128_1.XOFSizeInBits = this.XOFSizeInBits;
    Shake_128 shake128_2 = shake128_1 as Shake_128;
    shake128_2.BufferPosition = this.BufferPosition;
    shake128_2.DigestPosition = this.DigestPosition;
    shake128_2.ShakeBufferPosition = this.ShakeBufferPosition;
    shake128_2.Finalized = this.Finalized;
    shake128_2.ShakeBuffer = this.ShakeBuffer.DeepCopy();
    shake128_2.buffer = this.buffer.Clone();
    shake128_2.processed_bytes = this.processed_bytes;
    shake128_2.state = this.state.DeepCopy();
    shake128_2.BufferSize = this.BufferSize;
    return (IHash) shake128_2;
  }
}
