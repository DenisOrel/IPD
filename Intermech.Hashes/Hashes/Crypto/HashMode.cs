// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.HashMode
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Crypto;

internal enum HashMode
{
  Keccak = 1,
  CShake = 4,
  SHA3 = 6,
  Shake = 31, // 0x0000001F
}
