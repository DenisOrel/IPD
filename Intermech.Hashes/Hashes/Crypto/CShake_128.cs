// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.CShake_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class CShake_128(byte[] N, byte[] S) : CShake(16 /*0x10*/, N, S)
{
  public override IHash Clone()
  {
    CShake_128 cshake128_1 = new CShake_128(this.FN, this.FS);
    cshake128_1.XOFSizeInBits = this.XOFSizeInBits;
    CShake_128 cshake128_2 = cshake128_1 as CShake_128;
    cshake128_2.InitBlock = this.InitBlock.DeepCopy();
    cshake128_2.BufferPosition = this.BufferPosition;
    cshake128_2.DigestPosition = this.DigestPosition;
    cshake128_2.ShakeBufferPosition = this.ShakeBufferPosition;
    cshake128_2.Finalized = this.Finalized;
    cshake128_2.ShakeBuffer = this.ShakeBuffer.DeepCopy();
    cshake128_2.buffer = this.buffer.Clone();
    cshake128_2.processed_bytes = this.processed_bytes;
    cshake128_2.state = this.state.DeepCopy();
    cshake128_2.BufferSize = this.BufferSize;
    return (IHash) cshake128_2;
  }
}
