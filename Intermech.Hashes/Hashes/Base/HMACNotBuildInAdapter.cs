// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.HMACNotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Base;

internal class HMACNotBuildInAdapter : 
  Hash,
  IHMACNotBuiltIn,
  IHMAC,
  IMAC,
  IHash,
  ICryptoNotBuiltIn,
  ICrypto
{
  private IHash hash;
  private byte[] opad;
  private byte[] ipad;
  private byte[] key;
  private byte[] workingKey;

  private HMACNotBuildInAdapter(IHash a_underlyingHash)
    : base(a_underlyingHash.HashSize, a_underlyingHash.BlockSize)
  {
    this.hash = a_underlyingHash;
  }

  private HMACNotBuildInAdapter(IHash a_underlyingHash, byte[] a_HMACKey)
    : base(a_underlyingHash.HashSize, a_underlyingHash.BlockSize)
  {
    this.hash = a_underlyingHash.Clone();
    this.Key = a_HMACKey;
    this.ipad = new byte[this.hash.BlockSize];
    this.opad = new byte[this.hash.BlockSize];
  }

  public static IHMACNotBuiltIn CreateHMAC(IHash a_Hash, byte[] a_HMACKey)
  {
    if (a_HMACKey == null)
      throw new ArgumentNullHashLibException(nameof (a_HMACKey));
    if (a_Hash == null)
      throw new ArgumentNullHashLibException(nameof (a_Hash));
    return a_Hash is IHMACNotBuiltIn hmacNotBuiltIn ? (IHMACNotBuiltIn) hmacNotBuiltIn.Clone() : (IHMACNotBuiltIn) new HMACNotBuildInAdapter(a_Hash, a_HMACKey);
  }

  public void Clear()
  {
    ArrayUtils.ZeroFill(ref this.key);
    ArrayUtils.ZeroFill(ref this.workingKey);
  }

  public override IHash Clone()
  {
    HMACNotBuildInAdapter notBuildInAdapter = new HMACNotBuildInAdapter(this.hash.Clone());
    notBuildInAdapter.opad = this.opad.DeepCopy();
    notBuildInAdapter.ipad = this.ipad.DeepCopy();
    notBuildInAdapter.key = this.key.DeepCopy();
    notBuildInAdapter.workingKey = this.workingKey.DeepCopy();
    notBuildInAdapter.BufferSize = this.BufferSize;
    return (IHash) notBuildInAdapter;
  }

  public override void Initialize()
  {
    this.hash.Initialize();
    this.UpdatePads();
    this.hash.TransformBytes(this.ipad);
  }

  public override IHashResult TransformFinal()
  {
    IHashResult hashResult1 = this.hash.TransformFinal();
    this.hash.TransformBytes(this.opad);
    this.hash.TransformBytes(hashResult1.GetBytes());
    IHashResult hashResult2 = this.hash.TransformFinal();
    this.Initialize();
    return hashResult2;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.hash.TransformBytes(a_data, a_index, a_length);
  }

  public override string ToString() => this.Name;

  public override string Name => $"HMACNotBuiltIn({this.hash.Name})";

  public byte[] Key
  {
    get => this.key.DeepCopy();
    set
    {
      this.key = value != null ? value.DeepCopy() : throw new ArgumentNullHashLibException(nameof (value));
      this.TransformKey();
    }
  }

  public byte[] WorkingKey
  {
    get => this.workingKey.DeepCopy();
    private set
    {
      this.workingKey = value != null ? value.DeepCopy() : throw new ArgumentNullHashLibException(nameof (value));
    }
  }

  protected void UpdatePads()
  {
    int index = 0;
    int blockSize = this.hash.BlockSize;
    int length = this.workingKey.Length;
    ArrayUtils.Fill(ref this.ipad, 0, blockSize, (byte) 54);
    ArrayUtils.Fill(ref this.opad, 0, blockSize, (byte) 92);
    for (; index < length && index < blockSize; ++index)
    {
      this.ipad[index] = (byte) ((uint) this.ipad[index] ^ (uint) this.workingKey[index]);
      this.opad[index] = (byte) ((uint) this.opad[index] ^ (uint) this.workingKey[index]);
    }
  }

  private void TransformKey()
  {
    this.WorkingKey = this.key.Length > this.hash.BlockSize ? this.hash.ComputeBytes(this.key).GetBytes() : this.key;
  }
}
