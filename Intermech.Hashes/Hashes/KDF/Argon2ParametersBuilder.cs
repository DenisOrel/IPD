// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.Argon2ParametersBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.KDF;

public abstract class Argon2ParametersBuilder : IArgon2ParametersBuilder
{
  private const int DEFAULT_ITERATIONS = 3;
  private const int DEFAULT_MEMORY_COST = 12;
  private const int DEFAULT_LANES = 1;
  private const Argon2Type DEFAULT_TYPE = Argon2Type.a2tARGON2_i;
  private const Argon2Version DEFAULT_VERSION = Argon2Version.a2vARGON2_VERSION_13;
  public byte[] Salt;
  public byte[] Secret;
  public byte[] Additional;
  public int Iterations;
  public int Memory;
  public int Lanes;
  public Argon2Type Type;
  public Argon2Version Version;

  protected Argon2ParametersBuilder(
    Argon2Type a_Type,
    byte[] a_Salt,
    byte[] a_Secret,
    byte[] a_Additional,
    int a_Iterations,
    int a_Memory,
    int a_Lanes,
    Argon2Version a_Version)
  {
    this.Salt = a_Salt.DeepCopy();
    this.Secret = a_Secret.DeepCopy();
    this.Additional = a_Additional.DeepCopy();
    this.Iterations = a_Iterations;
    this.Memory = a_Memory;
    this.Lanes = a_Lanes;
    this.Type = a_Type;
    this.Version = a_Version;
  }

  protected Argon2ParametersBuilder(Argon2Type a_Type)
  {
    this.Lanes = 1;
    this.Memory = 4096 /*0x1000*/;
    this.Iterations = 3;
    this.Type = a_Type;
    this.Version = Argon2Version.a2vARGON2_VERSION_13;
  }

  ~Argon2ParametersBuilder() => this.Clear();

  public IArgon2Parameters Build()
  {
    return (IArgon2Parameters) new Argon2Parameters(this.Type, this.Salt, this.Secret, this.Additional, this.Iterations, this.Memory, this.Lanes, this.Version);
  }

  public void Clear()
  {
    ArrayUtils.ZeroFill(ref this.Salt);
    ArrayUtils.ZeroFill(ref this.Secret);
    ArrayUtils.ZeroFill(ref this.Additional);
  }

  public IArgon2ParametersBuilder WithAdditional(byte[] a_Additional)
  {
    this.Additional = a_Additional.DeepCopy();
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithIterations(int a_Iterations)
  {
    this.Iterations = a_Iterations;
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithMemoryAsKB(int a_Memory)
  {
    this.Memory = a_Memory;
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithMemoryPowOfTwo(int a_Memory)
  {
    this.Memory = 1 << a_Memory;
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithParallelism(int a_Parallelism)
  {
    this.Lanes = a_Parallelism;
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithSalt(byte[] a_Salt)
  {
    this.Salt = a_Salt.DeepCopy();
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithSecret(byte[] a_Secret)
  {
    this.Secret = a_Secret.DeepCopy();
    return (IArgon2ParametersBuilder) this;
  }

  public IArgon2ParametersBuilder WithVersion(Argon2Version a_Version)
  {
    this.Version = a_Version;
    return (IArgon2ParametersBuilder) this;
  }
}
