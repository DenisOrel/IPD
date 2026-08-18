// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.KMAC256XOF
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class KMAC256XOF : KMACNotBuildInAdapter, IXOF, IHash
{
  private KMAC256XOF(byte[] a_KMACKey, byte[] a_Customization)
    : this((IHash) new CShake_256(KMACNotBuildInAdapter.KMAC_Bytes, a_Customization), a_KMACKey, a_Customization)
  {
  }

  private KMAC256XOF(IHash a_hash, byte[] a_KMACKey, byte[] a_Customization)
    : base(32 /*0x20*/)
  {
    this.key = a_KMACKey.DeepCopy();
    this.Customization = a_Customization.DeepCopy();
    this.hash = a_hash;
  }

  public override IHash Clone()
  {
    KMAC256XOF kmaC256Xof = new KMAC256XOF(this.hash.Clone(), this.Key, this.Customization);
    kmaC256Xof.XOFSizeInBits = this.XOFSizeInBits;
    kmaC256Xof.BufferSize = this.BufferSize;
    return (IHash) kmaC256Xof;
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

  public static IKMAC CreateKMAC256XOF(
    byte[] a_KMACKey,
    byte[] a_Customization,
    ulong a_XofSizeInBits)
  {
    IXOF kmaC256Xof = new KMAC256XOF(a_KMACKey, a_Customization) as IXOF;
    kmaC256Xof.XOFSizeInBits = a_XofSizeInBits;
    return kmaC256Xof as IKMAC;
  }
}
