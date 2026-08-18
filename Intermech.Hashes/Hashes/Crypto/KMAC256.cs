// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.KMAC256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class KMAC256 : KMACNotBuildInAdapter
{
  private KMAC256(
    IHash a_hash,
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_OutputLengthInBits)
    : base(32 /*0x20*/)
  {
    this.key = a_KMACKey.DeepCopy();
    this.Customization = a_Customization.DeepCopy();
    this.hash = a_hash;
    (this.hash as IXOF).XOFSizeInBits = a_OutputLengthInBits;
  }

  private KMAC256(byte[] a_KMACKey, byte[] a_Customization, ulong a_OutputLengthInBits)
    : this((IHash) new CShake_256(KMACNotBuildInAdapter.KMAC_Bytes, a_Customization), a_KMACKey, a_Customization, a_OutputLengthInBits)
  {
  }

  public override IHash Clone()
  {
    KMAC256 kmaC256 = new KMAC256(this.hash.Clone(), this.Key, this.Customization, (this.hash as IXOF).XOFSizeInBits);
    kmaC256.BufferSize = this.BufferSize;
    return (IHash) kmaC256;
  }

  public static IKMAC CreateKMAC256(
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_OutputLengthInBits)
  {
    return (IKMAC) new KMAC256(a_KMACKey, a_Customization, a_OutputLengthInBits);
  }
}
