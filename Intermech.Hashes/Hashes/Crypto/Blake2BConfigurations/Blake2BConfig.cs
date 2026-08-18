// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2BConfigurations.Blake2BConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2BConfigurations;

public sealed class Blake2BConfig : IBlake2BConfig
{
  private int hash_size;
  private byte[] personalisation;
  private byte[] salt;
  private byte[] key;
  public static readonly string InvalidHashSize = "BLAKE2B HashSize must  of the following [1 .. 64], \"{0}\"";
  public static readonly string InvalidKeyLength = "\"Key\" Length Must Not Be Greatebe restricted to oner Than 64, \"{0}\"";
  public static readonly string InvalidPersonalisationLength = "\"Personalisation\" Length Must Be Equal To 16, \"{0}\"";
  public static readonly string InvalidSaltLength = "\"Salt\" Length Must Be Equal To 16, \"{0}\"";

  public int HashSize
  {
    get => this.hash_size;
    set
    {
      this.ValidateHashSize(value);
      this.hash_size = value;
    }
  }

  public byte[] Personalisation
  {
    get => this.personalisation;
    set
    {
      this.ValidatePersonalisationLength(value);
      this.personalisation = value;
    }
  }

  public byte[] Salt
  {
    get => this.salt;
    set
    {
      this.ValidateSaltLength(value);
      this.salt = value;
    }
  }

  public byte[] Key
  {
    get => this.key;
    set
    {
      this.ValidateKeyLength(value);
      this.key = value;
    }
  }

  public Blake2BConfig(HashSizeEnum a_hash_size = HashSizeEnum.HashSize512)
  {
    this.ValidateHashSize((int) a_hash_size);
    this.hash_size = (int) a_hash_size;
  }

  public Blake2BConfig(int a_hash_size)
  {
    this.ValidateHashSize(a_hash_size);
    this.hash_size = a_hash_size;
  }

  ~Blake2BConfig() => this.Clear();

  public static Blake2BConfig DefaultConfig => new Blake2BConfig();

  public IBlake2BConfig Clone()
  {
    return (IBlake2BConfig) new Blake2BConfig(this.HashSize)
    {
      Key = this.Key.DeepCopy(),
      Personalisation = this.Personalisation.DeepCopy(),
      Salt = this.Salt.DeepCopy()
    };
  }

  public void Clear() => ArrayUtils.ZeroFill(ref this.key);

  private void ValidateHashSize(int a_hash_size)
  {
    if (a_hash_size <= 0 || a_hash_size > 64 /*0x40*/ || (a_hash_size * 8 & 7) != 0)
      throw new ArgumentHashLibException(string.Format(Blake2BConfig.InvalidHashSize, (object) a_hash_size));
  }

  private void ValidateKeyLength(byte[] a_Key)
  {
    if (a_Key.Empty())
      return;
    int length = a_Key.Length;
    if (length > 64 /*0x40*/)
      throw new ArgumentOutOfRangeHashLibException(string.Format(Blake2BConfig.InvalidKeyLength, (object) length));
  }

  private void ValidatePersonalisationLength(byte[] a_Personalisation)
  {
    if (a_Personalisation.Empty())
      return;
    int length = a_Personalisation.Length;
    if (length != 16 /*0x10*/)
      throw new ArgumentOutOfRangeHashLibException(string.Format(Blake2BConfig.InvalidPersonalisationLength, (object) length));
  }

  private void ValidateSaltLength(byte[] a_Salt)
  {
    if (a_Salt.Empty())
      return;
    int length = a_Salt.Length;
    if (length != 16 /*0x10*/)
      throw new ArgumentOutOfRangeHashLibException(string.Format(Blake2BConfig.InvalidSaltLength, (object) length));
  }
}
