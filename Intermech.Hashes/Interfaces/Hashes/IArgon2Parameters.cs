// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IArgon2Parameters
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.KDF;

#nullable disable
namespace Intermech.Interfaces.Hashes;

public interface IArgon2Parameters
{
  void Clear();

  IArgon2Parameters Clone();

  byte[] Salt { get; }

  byte[] Secret { get; }

  byte[] Additional { get; }

  int Iterations { get; }

  int Memory { get; }

  int Lanes { get; }

  Argon2Type Type { get; }

  Argon2Version Version { get; }
}
