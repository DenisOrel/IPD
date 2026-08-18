// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC32_CASTAGNOLI_Fast
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC32_CASTAGNOLI_Fast : CRC32Fast
{
  private static readonly uint CRC32_CASTAGNOLI_Polynomial = 2197175160;
  private uint[] CRC32_CASTAGNOLI_Table;

  public CRC32_CASTAGNOLI_Fast()
  {
    this.CRC32_CASTAGNOLI_Table = CRC32Fast.Init_CRC_Table(CRC32_CASTAGNOLI_Fast.CRC32_CASTAGNOLI_Polynomial);
  }

  public override IHash Clone()
  {
    CRC32_CASTAGNOLI_Fast c32CastagnoliFast = new CRC32_CASTAGNOLI_Fast();
    c32CastagnoliFast.CurrentCRC = this.CurrentCRC;
    c32CastagnoliFast.BufferSize = this.BufferSize;
    return (IHash) c32CastagnoliFast;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.LocalCRCCompute(this.CRC32_CASTAGNOLI_Table, a_data, a_index, a_length);
  }
}
