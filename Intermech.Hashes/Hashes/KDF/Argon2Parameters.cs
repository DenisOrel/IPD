// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.Argon2Parameters
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.KDF;

public sealed class Argon2Parameters : IArgon2Parameters
{
  private byte[] salt;
  private byte[] secret;
  private byte[] additional;

  public byte[] Salt
  {
    get => this.salt;
    set => this.salt = value;
  }

  public byte[] Secret
  {
    get => this.secret;
    set => this.secret = value;
  }

  public byte[] Additional
  {
    get => this.additional;
    set => this.additional = value;
  }

  public int Iterations { get; set; }

  public int Memory { get; set; }

  public int Lanes { get; set; }

  public Argon2Type Type { get; set; }

  public Argon2Version Version { get; set; }

  public Argon2Parameters(
    Argon2Type a_Type,
    byte[] a_Salt,
    byte[] a_Secret,
    byte[] a_Additional,
    int a_Iterations,
    int a_Memory,
    int a_Lanes,
    Argon2Version a_Version)
  {
    if (a_Salt == null)
      throw new ArgumentNullHashLibException(nameof (a_Salt));
    if (a_Secret == null)
      throw new ArgumentNullHashLibException(nameof (a_Secret));
    if (a_Additional == null)
      throw new ArgumentNullHashLibException(nameof (a_Additional));
    this.Salt = a_Salt.DeepCopy();
    this.Secret = a_Secret.DeepCopy();
    this.Additional = a_Additional.DeepCopy();
    this.Iterations = a_Iterations;
    this.Memory = a_Memory;
    this.Lanes = a_Lanes;
    this.Type = a_Type;
    this.Version = a_Version;
  }

  public void Clear()
  {
    ArrayUtils.ZeroFill(ref this.salt);
    ArrayUtils.ZeroFill(ref this.secret);
    ArrayUtils.ZeroFill(ref this.additional);
  }

  public IArgon2Parameters Clone()
  {
    return (IArgon2Parameters) new Argon2Parameters(this.Type, this.salt, this.secret, this.additional, this.Iterations, this.Memory, this.Lanes, this.Version);
  }
}
