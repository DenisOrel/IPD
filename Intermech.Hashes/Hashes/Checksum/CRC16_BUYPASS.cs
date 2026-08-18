// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC16_BUYPASS
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC16_BUYPASS : CRC16
{
  public CRC16_BUYPASS()
    : base((ulong) CRC16Polynomials.BUYPASS, 0UL, false, false, 0UL, 65256UL, new string[2]
    {
      "CRC-16/BUYPASS",
      "CRC-16/VERIFONE"
    })
  {
  }
}
