// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.CShake_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class CShake_256(byte[] N, byte[] S) : CShake(32 /*0x20*/, N, S)
{
  public override IHash Clone()
  {
    CShake_256 cshake256_1 = new CShake_256(this.FN, this.FS);
    cshake256_1.XOFSizeInBits = this.XOFSizeInBits;
    CShake_256 cshake256_2 = cshake256_1 as CShake_256;
    cshake256_2.InitBlock = this.InitBlock.DeepCopy();
    cshake256_2.BufferPosition = this.BufferPosition;
    cshake256_2.DigestPosition = this.DigestPosition;
    cshake256_2.ShakeBufferPosition = this.ShakeBufferPosition;
    cshake256_2.Finalized = this.Finalized;
    cshake256_2.ShakeBuffer = this.ShakeBuffer.DeepCopy();
    cshake256_2.buffer = this.buffer.Clone();
    cshake256_2.processed_bytes = this.processed_bytes;
    cshake256_2.state = this.state.DeepCopy();
    cshake256_2.BufferSize = this.BufferSize;
    return (IHash) cshake256_2;
  }
}
