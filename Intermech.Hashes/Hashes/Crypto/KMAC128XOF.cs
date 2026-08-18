// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.KMAC128XOF
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class KMAC128XOF : KMACNotBuildInAdapter, IXOF, IHash
{
  private KMAC128XOF(byte[] a_KMACKey, byte[] a_Customization)
    : this((IHash) new CShake_128(KMACNotBuildInAdapter.KMAC_Bytes, a_Customization), a_KMACKey, a_Customization)
  {
  }

  private KMAC128XOF(IHash a_hash, byte[] a_KMACKey, byte[] a_Customization)
    : base(16 /*0x10*/)
  {
    this.key = a_KMACKey.DeepCopy();
    this.Customization = a_Customization.DeepCopy();
    this.hash = a_hash;
  }

  public override IHash Clone()
  {
    KMAC128XOF kmaC128Xof = new KMAC128XOF(this.hash.Clone(), this.Key, this.Customization);
    kmaC128Xof.XOFSizeInBits = this.XOFSizeInBits;
    kmaC128Xof.BufferSize = this.BufferSize;
    return (IHash) kmaC128Xof;
  }

  private IXOF SetXOFSizeInBitsInternal(ulong a_XofSizeInBits)
  {
    ulong num = a_XofSizeInBits >> 3;
    if (((long) num & 7L) != 0L || num < 1UL)
      throw new ArgumentInvalidHashLibException(Global.InvalidXOFSize);
    (this.hash as IXOF).XOFSizeInBits = a_XofSizeInBits;
    return (IXOF) this;
  }

  public ulong XOFSizeInBits
  {
    get => (this.hash as IXOF).XOFSizeInBits;
    set => this.SetXOFSizeInBitsInternal(value);
  }

  public static IKMAC CreateKMAC128XOF(
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_XofSizeInBits)
  {
    IXOF kmaC128Xof = new KMAC128XOF(a_KMACKey, a_Customization) as IXOF;
    kmaC128Xof.XOFSizeInBits = a_XofSizeInBits;
    return kmaC128Xof as IKMAC;
  }
}
