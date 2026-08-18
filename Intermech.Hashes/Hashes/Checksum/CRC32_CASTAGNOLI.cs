// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC32_CASTAGNOLI
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC32_CASTAGNOLI : CRC32
{
  public CRC32_CASTAGNOLI()
    : base((ulong) CRC32Polynomials.Castagnoli, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 3808858755UL, new string[3]
    {
      "CRC-32C",
      "CRC-32/ISCSI",
      "CRC-32/CASTAGNOLI"
    })
  {
  }
}
