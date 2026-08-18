// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.Argon2idParametersBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.KDF;

public sealed class Argon2idParametersBuilder : Argon2ParametersBuilder
{
  private Argon2idParametersBuilder()
    : base(Argon2Type.a2tARGON2_id)
  {
  }

  public static IArgon2ParametersBuilder Builder()
  {
    return (IArgon2ParametersBuilder) new Argon2idParametersBuilder();
  }
}
