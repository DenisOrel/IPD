// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC32_PKZIP
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC32_PKZIP : CRC32
{
  public CRC32_PKZIP()
    : base((ulong) CRC32Polynomials.PKZIP, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 3421780262UL, new string[3]
    {
      "CRC-32",
      "CRC-32/ADCCP",
      "PKZIP"
    })
  {
  }
}
