// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IArgon2ParametersBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.KDF;

#nullable disable
namespace Intermech.Interfaces.Hashes;

public interface IArgon2ParametersBuilder
{
  IArgon2ParametersBuilder WithParallelism(int a_parallelism);

  IArgon2ParametersBuilder WithSalt(byte[] a_salt);

  IArgon2ParametersBuilder WithSecret(byte[] a_secret);

  IArgon2ParametersBuilder WithAdditional(byte[] a_additional);

  IArgon2ParametersBuilder WithIterations(int a_iterations);

  IArgon2ParametersBuilder WithMemoryAsKB(int a_memory);

  IArgon2ParametersBuilder WithMemoryPowOfTwo(int a_memory);

  IArgon2ParametersBuilder WithVersion(Argon2Version a_version);

  void Clear();

  IArgon2Parameters Build();
}
