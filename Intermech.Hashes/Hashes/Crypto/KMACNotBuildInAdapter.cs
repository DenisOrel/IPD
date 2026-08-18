// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.KMACNotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class KMACNotBuildInAdapter(int a_hash_size) : 
  Hash(a_hash_size, 200 - a_hash_size * 2),
  IKMAC,
  IMAC,
  IHash,
  IKMACNotBuiltIn,
  ICrypto,
  ICryptoNotBuiltIn
{
  protected IHash hash;
  protected byte[] key;
  protected byte[] Customization;
  protected static readonly byte[] KMAC_Bytes = new byte[4]
  {
    (byte) 75,
    (byte) 77,
    (byte) 65,
    (byte) 67
  };

  ~KMACNotBuildInAdapter() => this.Clear();

  public override void Initialize()
  {
    this.hash.Initialize();
    this.TransformBytes(CShake.BytePad(CShake.EncodeString(this.Key), this.BlockSize));
  }

  protected virtual byte[] GetResult()
  {
    ulong outputLength = (this.hash as IXOF).XOFSizeInBits >> 3;
    byte[] destination = new byte[outputLength];
    this.DoOutput(ref destination, 0UL, outputLength);
    return destination;
  }

  public override IHashResult TransformFinal()
  {
    byte[] result = this.GetResult();
    this.Initialize();
    return (IHashResult) new HashResult(result);
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.hash.TransformBytes(a_data, a_index, a_length);
  }

  public virtual void Clear() => ArrayUtils.ZeroFill(ref this.key);

  public virtual byte[] Key
  {
    get => this.key.DeepCopy();
    set => this.key = value.DeepCopy();
  }

  public override string Name
  {
    get
    {
      return this is IXOF ? $"{this.GetType().Name}_{"XOFSizeInBytes"}_{(this.hash as IXOF).XOFSizeInBits >> 3}" : $"{this.GetType().Name}";
    }
  }

  public virtual void DoOutput(ref byte[] destination, ulong destinationOffset, ulong outputLength)
  {
    if (this is IXOF)
      this.TransformBytes(CShake.RightEncode(0UL));
    else
      this.TransformBytes(CShake.RightEncode((this.hash as IXOF).XOFSizeInBits));
    (this.hash as IXOF).DoOutput(ref destination, destinationOffset, outputLength);
  }
}
