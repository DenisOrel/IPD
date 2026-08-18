// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC64_ECMA_182
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC64_ECMA_182 : CRC64
{
  public CRC64_ECMA_182()
    : base(CRC64Polynomials.ECMA_182, 0UL, false, false, 0UL, 7800480153909949255UL, new string[1]
    {
      "CRC-64/ECMA"
    })
  {
  }
}
