// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2SMACNotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Blake2SMACNotBuildInAdapter : 
  Hash,
  IBlake2SMACNotBuiltIn,
  IBlake2SMAC,
  IMAC,
  IHash,
  ICryptoNotBuiltIn,
  ICrypto
{
  private IHash hash;
  private byte[] key;

  public byte[] Key
  {
    get => this.key;
    set => this.key = value;
  }

  ~Blake2SMACNotBuildInAdapter() => this.Clear();

  public override IHash Clone()
  {
    Blake2SMACNotBuildInAdapter notBuildInAdapter = new Blake2SMACNotBuildInAdapter(this.hash, this.Key);
    notBuildInAdapter.BufferSize = this.BufferSize;
    return (IHash) notBuildInAdapter;
  }

  public void Clear() => ArrayUtils.ZeroFill(ref this.key);

  public override void Initialize() => this.hash?.Initialize();

  public override IHashResult TransformFinal() => this.hash?.TransformFinal();

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.hash?.TransformBytes(a_data, a_index, a_length);
  }

  public static IBlake2SMAC CreateBlake2SMAC(
    byte[] a_Blake2SKey,
    byte[] a_Salt,
    byte[] a_Personalisation,
    int a_OutputLengthInBits)
  {
    Blake2SConfig a_Config = new Blake2SConfig(a_OutputLengthInBits >> 3);
    a_Config.Key = a_Blake2SKey.DeepCopy();
    a_Config.Salt = a_Salt.DeepCopy();
    a_Config.Personalisation = a_Personalisation.DeepCopy();
    return (IBlake2SMAC) new Blake2SMACNotBuildInAdapter((IHash) new Blake2S((IBlake2SConfig) a_Config, (IBlake2STreeConfig) null), a_Blake2SKey);
  }

  private Blake2SMACNotBuildInAdapter(IHash a_Hash, byte[] a_Blake2SKey)
    : base(a_Hash != null ? a_Hash.HashSize : -1, a_Hash != null ? a_Hash.BlockSize : -1)
  {
    this.Key = a_Blake2SKey.DeepCopy();
    this.hash = a_Hash?.Clone();
  }
}
