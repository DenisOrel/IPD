// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IHashResult
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Interfaces.Hashes;

public interface IHashResult
{
  byte[] GetBytes();

  byte GetUInt8();

  ushort GetUInt16();

  uint GetUInt32();

  int GetInt32();

  ulong GetUInt64();

  string ToString(bool a_group = false);

  int GetHashCode();

  bool CompareTo(IHashResult a_hashResult);
}
