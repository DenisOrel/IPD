// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC32_PKZIP_Fast
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC32_PKZIP_Fast : CRC32Fast
{
  private static readonly uint CRC32_PKZIP_Polynomial = 3988292384;
  private uint[] CRC32_PKZIP_Table;

  public CRC32_PKZIP_Fast()
  {
    this.CRC32_PKZIP_Table = CRC32Fast.Init_CRC_Table(CRC32_PKZIP_Fast.CRC32_PKZIP_Polynomial);
  }

  public override IHash Clone()
  {
    CRC32_PKZIP_Fast crC32PkzipFast = new CRC32_PKZIP_Fast();
    crC32PkzipFast.CurrentCRC = this.CurrentCRC;
    crC32PkzipFast.BufferSize = this.BufferSize;
    return (IHash) crC32PkzipFast;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.LocalCRCCompute(this.CRC32_PKZIP_Table, a_data, a_index, a_length);
  }
}
