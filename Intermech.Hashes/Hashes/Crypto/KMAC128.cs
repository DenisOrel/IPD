// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.KMAC128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class KMAC128 : KMACNotBuildInAdapter
{
  private KMAC128(
    IHash a_hash,
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_OutputLengthInBits)
    : base(16 /*0x10*/)
  {
    this.key = a_KMACKey.DeepCopy();
    this.Customization = a_Customization.DeepCopy();
    this.hash = a_hash;
    (this.hash as IXOF).XOFSizeInBits = a_OutputLengthInBits;
  }

  private KMAC128(byte[] a_KMACKey, byte[] a_Customization, ulong a_OutputLengthInBits)
    : this((IHash) new CShake_128(KMACNotBuildInAdapter.KMAC_Bytes, a_Customization), a_KMACKey, a_Customization, a_OutputLengthInBits)
  {
  }

  public override IHash Clone()
  {
    KMAC128 kmaC128 = new KMAC128(this.hash.Clone(), this.Key, this.Customization, (this.hash as IXOF).XOFSizeInBits);
    kmaC128.BufferSize = this.BufferSize;
    return (IHash) kmaC128;
  }

  public static IKMAC CreateKMAC128(
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_OutputLengthInBits)
  {
    return (IKMAC) new KMAC128(a_KMACKey, a_Customization, a_OutputLengthInBits);
  }
}
