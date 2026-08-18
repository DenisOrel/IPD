// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC32Fast
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal abstract class CRC32Fast : Hash, IChecksum, IBlockHash, IHash, IHash16, ITransformBlock
{
  protected uint CurrentCRC;

  public CRC32Fast()
    : base(4, 1)
  {
  }

  public override void Initialize() => this.CurrentCRC = 0U;

  public override IHashResult TransformFinal()
  {
    HashResult hashResult = new HashResult(this.CurrentCRC);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  protected void LocalCRCCompute(uint[] a_CRCTable, byte[] a_data, int a_index, int a_length)
  {
    uint num1 = ~this.CurrentCRC;
    uint[] numArray = a_CRCTable;
    for (; a_length >= 16 /*0x10*/; a_length -= 16 /*0x10*/)
    {
      uint num2 = numArray[768 /*0x0300*/ + (int) a_data[a_index + 12]] ^ numArray[512 /*0x0200*/ + (int) a_data[a_index + 13]] ^ numArray[256 /*0x0100*/ + (int) a_data[a_index + 14]] ^ numArray[(int) a_data[a_index + 15]];
      uint num3 = numArray[1792 /*0x0700*/ + (int) a_data[a_index + 8]] ^ numArray[1536 /*0x0600*/ + (int) a_data[a_index + 9]] ^ numArray[1280 /*0x0500*/ + (int) a_data[a_index + 10]] ^ numArray[1024 /*0x0400*/ + (int) a_data[a_index + 11]];
      uint num4 = numArray[2816 /*0x0B00*/ + (int) a_data[a_index + 4]] ^ numArray[2560 /*0x0A00*/ + (int) a_data[a_index + 5]] ^ numArray[2304 /*0x0900*/ + (int) a_data[a_index + 6]] ^ numArray[2048 /*0x0800*/ + (int) a_data[a_index + 7]];
      num1 = numArray[3840 /*0x0F00*/ + ((int) num1 & (int) byte.MaxValue ^ (int) a_data[a_index])] ^ numArray[3584 /*0x0E00*/ + ((int) (num1 >> 8) & (int) byte.MaxValue ^ (int) a_data[a_index + 1])] ^ numArray[3328 /*0x0D00*/ + ((int) (num1 >> 16 /*0x10*/) & (int) byte.MaxValue ^ (int) a_data[a_index + 2])] ^ numArray[3072 /*0x0C00*/ + ((int) (num1 >> 24) ^ (int) a_data[a_index + 3])] ^ num4 ^ num3 ^ num2;
      a_index += 16 /*0x10*/;
    }
    for (--a_length; a_length >= 0; --a_length)
    {
      num1 = numArray[(int) (byte) (num1 ^ (uint) a_data[a_index])] ^ num1 >> 8;
      ++a_index;
    }
    this.CurrentCRC = ~num1;
  }

  public static uint[] Init_CRC_Table(uint a_polynomial)
  {
    uint[] numArray = new uint[4096 /*0x1000*/];
    for (int index1 = 0; index1 < 256 /*0x0100*/; ++index1)
    {
      uint num = (uint) index1;
      for (int index2 = 0; index2 < 16 /*0x10*/; ++index2)
      {
        for (int index3 = 0; index3 < 8; ++index3)
        {
          num = (uint) ((ulong) (num >> 1) ^ (ulong) -((int) num & 1) & (ulong) a_polynomial);
          numArray[index2 * 256 /*0x0100*/ + index1] = num;
        }
      }
    }
    return numArray;
  }
}
