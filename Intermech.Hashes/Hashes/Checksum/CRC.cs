// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class CRC : Hash, IChecksum, ICRC, IHash, ITransformBlock
{
  private string[] names { get; set; }

  private int width { get; set; }

  private ulong polynomial { get; set; }

  private ulong init { get; set; }

  private ulong xorOut { get; set; }

  private ulong checkValue { get; set; }

  private ulong CRCMask { get; set; }

  private ulong CRCHighBitMask { get; set; }

  private ulong hash { get; set; }

  private bool reflectIn { get; set; }

  private bool reflectOut { get; set; }

  private bool IsTableGenerated { get; set; }

  private ulong[] CRCTable { get; set; }

  private static int Delta => 7;

  public CRC(
    int _Width,
    ulong _poly,
    ulong _Init,
    bool _refIn,
    bool _refOut,
    ulong _XorOut,
    ulong _check,
    string[] _Names)
    : base(0, 0)
  {
    this.IsTableGenerated = false;
    if (_Width >= 0 && _Width <= 7)
    {
      this.hash_size = 1;
      this.block_size = 1;
    }
    else if (_Width >= 8 && _Width <= 16 /*0x10*/)
    {
      this.hash_size = 2;
      this.block_size = 1;
    }
    else if (_Width >= 17 && _Width <= 39)
    {
      this.hash_size = 4;
      this.block_size = 1;
    }
    else
    {
      this.hash_size = 8;
      this.block_size = 1;
    }
    this.names = new string[_Names.Length];
    for (int index = 0; index < _Names.Length; ++index)
      this.names[index] = _Names[index];
    this.width = _Width;
    this.polynomial = _poly;
    this.init = _Init;
    this.reflectIn = _refIn;
    this.reflectOut = _refOut;
    this.xorOut = _XorOut;
    this.checkValue = _check;
  }

  public override IHash Clone()
  {
    CRC crc = new CRC(this.width, this.polynomial, this.init, this.reflectIn, this.reflectOut, this.xorOut, this.checkValue, this.names);
    crc.CRCMask = this.CRCMask;
    crc.CRCHighBitMask = this.CRCHighBitMask;
    crc.hash = this.hash;
    crc.IsTableGenerated = this.IsTableGenerated;
    crc.CRCTable = this.CRCTable.DeepCopy();
    crc.BufferSize = this.BufferSize;
    return (IHash) crc;
  }

  public override string Name => this.Names[0];

  public override void Initialize()
  {
    this.CRCMask = (ulong) ((1L << this.Width - 1) - 1L << 1) | 1UL;
    this.CRCHighBitMask = 1UL << this.Width - 1;
    this.hash = this.init;
    if (this.Width <= CRC.Delta)
      return;
    if (!this.IsTableGenerated)
      this.GenerateTable();
    if (!this.reflectIn)
      return;
    this.hash = CRC.Reflect(this.hash, this.Width);
  }

  public override IHashResult TransformFinal()
  {
    if (this.Width > CRC.Delta)
    {
      if (this.reflectIn ^ this.reflectOut)
        this.hash = CRC.Reflect(this.hash, this.Width);
    }
    else if (this.reflectOut)
      this.hash = CRC.Reflect(this.hash, this.Width);
    this.hash ^= this.xorOut;
    this.hash &= this.CRCMask;
    if (this.width == 21)
    {
      HashResult hashResult = new HashResult((uint) this.hash);
      this.Initialize();
      return (IHashResult) hashResult;
    }
    switch ((long) (this.Width >> 3))
    {
      case 0:
        int hash1 = (int) (byte) this.hash;
        this.Initialize();
        return (IHashResult) new HashResult((byte) hash1);
      case 1:
      case 2:
        int hash2 = (int) (ushort) this.hash;
        this.Initialize();
        return (IHashResult) new HashResult((ushort) hash2);
      case 3:
      case 4:
        int hash3 = (int) (uint) this.hash;
        this.Initialize();
        return (IHashResult) new HashResult((uint) hash3);
      default:
        long hash4 = (long) this.hash;
        this.Initialize();
        return (IHashResult) new HashResult((ulong) hash4);
    }
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int a_index1 = a_index;
    fixed (byte* a_data1 = a_data)
    {
      if (this.Width > CRC.Delta)
        this.CalculateCRCbyTable((IntPtr) (void*) a_data1, a_length, a_index1);
      else
        this.CalculateCRCdirect((IntPtr) (void*) a_data1, a_length, a_index1);
    }
  }

  public static ICRC CreateCRCObject(CRCStandard a_value)
  {
    switch (a_value)
    {
      case CRCStandard.CRC3_GSM:
        return (ICRC) new CRC(3, 3UL, 0UL, false, false, 7UL, 4UL, new string[1]
        {
          "CRC-3/GSM"
        });
      case CRCStandard.CRC3_ROHC:
        return (ICRC) new CRC(3, 3UL, 7UL, true, true, 0UL, 6UL, new string[1]
        {
          "CRC-3/ROHC"
        });
      case CRCStandard.CRC4_INTERLAKEN:
        return (ICRC) new CRC(4, 3UL, 15UL, false, false, 15UL, 11UL, new string[1]
        {
          "CRC-4/INTERLAKEN"
        });
      case CRCStandard.CRC4_ITU:
        return (ICRC) new CRC(4, 3UL, 0UL, true, true, 0UL, 7UL, new string[1]
        {
          "CRC-4/ITU"
        });
      case CRCStandard.CRC5_EPC:
        return (ICRC) new CRC(5, 9UL, 9UL, false, false, 0UL, 0UL, new string[1]
        {
          "CRC-5/EPC"
        });
      case CRCStandard.CRC5_ITU:
        return (ICRC) new CRC(5, 21UL, 0UL, true, true, 0UL, 7UL, new string[1]
        {
          "CRC-5/ITU"
        });
      case CRCStandard.CRC5_USB:
        return (ICRC) new CRC(5, 5UL, 31UL /*0x1F*/, true, true, 31UL /*0x1F*/, 25UL, new string[1]
        {
          "CRC-5/USB"
        });
      case CRCStandard.CRC6_CDMA2000A:
        return (ICRC) new CRC(6, 39UL, 63UL /*0x3F*/, false, false, 0UL, 13UL, new string[1]
        {
          "CRC-6/CDMA2000-A"
        });
      case CRCStandard.CRC6_CDMA2000B:
        return (ICRC) new CRC(6, 7UL, 63UL /*0x3F*/, false, false, 0UL, 59UL, new string[1]
        {
          "CRC-6/CDMA2000-B"
        });
      case CRCStandard.CRC6_DARC:
        return (ICRC) new CRC(6, 25UL, 0UL, true, true, 0UL, 38UL, new string[1]
        {
          "CRC-6/DARC"
        });
      case CRCStandard.CRC6_GSM:
        return (ICRC) new CRC(6, 47UL, 0UL, false, false, 63UL /*0x3F*/, 19UL, new string[1]
        {
          "CRC-6/GSM"
        });
      case CRCStandard.CRC6_ITU:
        return (ICRC) new CRC(6, 3UL, 0UL, true, true, 0UL, 6UL, new string[1]
        {
          "CRC-6/ITU"
        });
      case CRCStandard.CRC7:
        return (ICRC) new CRC(7, 9UL, 0UL, false, false, 0UL, 117UL, new string[1]
        {
          "CRC-7"
        });
      case CRCStandard.CRC7_ROHC:
        return (ICRC) new CRC(7, 79UL, (ulong) sbyte.MaxValue, true, true, 0UL, 83UL, new string[1]
        {
          "CRC-7/ROHC"
        });
      case CRCStandard.CRC7_UMTS:
        return (ICRC) new CRC(7, 69UL, 0UL, false, false, 0UL, 97UL, new string[1]
        {
          "CRC-7/UMTS"
        });
      case CRCStandard.CRC8:
        return (ICRC) new CRC(8, 7UL, 0UL, false, false, 0UL, 244UL, new string[1]
        {
          "CRC-8"
        });
      case CRCStandard.CRC8_AUTOSAR:
        return (ICRC) new CRC(8, 47UL, (ulong) byte.MaxValue, false, false, (ulong) byte.MaxValue, 223UL, new string[1]
        {
          "CRC-8/AUTOSAR"
        });
      case CRCStandard.CRC8_BLUETOOTH:
        return (ICRC) new CRC(8, 167UL, 0UL, true, true, 0UL, 38UL, new string[1]
        {
          "CRC-8/BLUETOOTH"
        });
      case CRCStandard.CRC8_CDMA2000:
        return (ICRC) new CRC(8, 155UL, (ulong) byte.MaxValue, false, false, 0UL, 218UL, new string[1]
        {
          "CRC-8/CDMA2000"
        });
      case CRCStandard.CRC8_DARC:
        return (ICRC) new CRC(8, 57UL, 0UL, true, true, 0UL, 21UL, new string[1]
        {
          "CRC-8/DARC"
        });
      case CRCStandard.CRC8_DVBS2:
        return (ICRC) new CRC(8, 213UL, 0UL, false, false, 0UL, 188UL, new string[1]
        {
          "CRC-8/DVB-S2"
        });
      case CRCStandard.CRC8_EBU:
        return (ICRC) new CRC(8, 29UL, (ulong) byte.MaxValue, true, true, 0UL, 151UL, new string[2]
        {
          "CRC-8/EBU",
          "CRC-8/AES"
        });
      case CRCStandard.CRC8_GSMA:
        return (ICRC) new CRC(8, 29UL, 0UL, false, false, 0UL, 55UL, new string[1]
        {
          "CRC-8/GSM-A"
        });
      case CRCStandard.CRC8_GSMB:
        return (ICRC) new CRC(8, 73UL, 0UL, false, false, (ulong) byte.MaxValue, 148UL, new string[1]
        {
          "CRC-8/GSM-B"
        });
      case CRCStandard.CRC8_ICODE:
        return (ICRC) new CRC(8, 29UL, 253UL, false, false, 0UL, 126UL, new string[1]
        {
          "CRC-8/I-CODE"
        });
      case CRCStandard.CRC8_ITU:
        return (ICRC) new CRC(8, 7UL, 0UL, false, false, 85UL, 161UL, new string[1]
        {
          "CRC-8/ITU"
        });
      case CRCStandard.CRC8_LTE:
        return (ICRC) new CRC(8, 155UL, 0UL, false, false, 0UL, 234UL, new string[1]
        {
          "CRC-8/LTE"
        });
      case CRCStandard.CRC8_MAXIM:
        return (ICRC) new CRC(8, 49UL, 0UL, true, true, 0UL, 161UL, new string[2]
        {
          "CRC-8/MAXIM",
          "DOW-CRC"
        });
      case CRCStandard.CRC8_OPENSAFETY:
        return (ICRC) new CRC(8, 47UL, 0UL, false, false, 0UL, 62UL, new string[1]
        {
          "CRC-8/OPENSAFETY"
        });
      case CRCStandard.CRC8_ROHC:
        return (ICRC) new CRC(8, 7UL, (ulong) byte.MaxValue, true, true, 0UL, 208UL /*0xD0*/, new string[1]
        {
          "CRC-8/ROHC"
        });
      case CRCStandard.CRC8_SAEJ1850:
        return (ICRC) new CRC(8, 29UL, (ulong) byte.MaxValue, false, false, (ulong) byte.MaxValue, 75UL, new string[1]
        {
          "CRC-8/SAE-J1850"
        });
      case CRCStandard.CRC8_WCDMA:
        return (ICRC) new CRC(8, 155UL, 0UL, true, true, 0UL, 37UL, new string[1]
        {
          "CRC-8/WCDMA"
        });
      case CRCStandard.CRC10:
        return (ICRC) new CRC(10, 563UL, 0UL, false, false, 0UL, 409UL, new string[1]
        {
          "CRC-10"
        });
      case CRCStandard.CRC10_CDMA2000:
        return (ICRC) new CRC(10, 985UL, 1023UL /*0x03FF*/, false, false, 0UL, 563UL, new string[1]
        {
          "CRC-10/CDMA2000"
        });
      case CRCStandard.CRC10_GSM:
        return (ICRC) new CRC(10, 373UL, 0UL, false, false, 1023UL /*0x03FF*/, 298UL, new string[1]
        {
          "CRC-10/GSM"
        });
      case CRCStandard.CRC11:
        return (ICRC) new CRC(11, 901UL, 26UL, false, false, 0UL, 1443UL, new string[1]
        {
          "CRC-11"
        });
      case CRCStandard.CRC11_UMTS:
        return (ICRC) new CRC(11, 775UL, 0UL, false, false, 0UL, 97UL, new string[1]
        {
          "CRC-11/UMTS"
        });
      case CRCStandard.CRC12_CDMA2000:
        return (ICRC) new CRC(12, 3859UL, 4095UL /*0x0FFF*/, false, false, 0UL, 3405UL, new string[1]
        {
          "CRC-12/CDMA2000"
        });
      case CRCStandard.CRC12_DECT:
        return (ICRC) new CRC(12, 2063UL, 0UL, false, false, 0UL, 3931UL, new string[2]
        {
          "CRC-12/DECT",
          "X-CRC-12"
        });
      case CRCStandard.CRC12_GSM:
        return (ICRC) new CRC(12, 3377UL, 0UL, false, false, 4095UL /*0x0FFF*/, 2868UL, new string[1]
        {
          "CRC-12/GSM"
        });
      case CRCStandard.CRC12_UMTS:
        return (ICRC) new CRC(12, 2063UL, 0UL, false, true, 0UL, 3503UL, new string[2]
        {
          "CRC-12/UMTS",
          "CRC-12/3GPP"
        });
      case CRCStandard.CRC13_BBC:
        return (ICRC) new CRC(13, 7413UL, 0UL, false, false, 0UL, 1274UL, new string[1]
        {
          "CRC-13/BBC"
        });
      case CRCStandard.CRC14_DARC:
        return (ICRC) new CRC(14, 2053UL, 0UL, true, true, 0UL, 2093UL, new string[1]
        {
          "CRC-14/DARC"
        });
      case CRCStandard.CRC14_GSM:
        return (ICRC) new CRC(14, 8237UL, 0UL, false, false, 16383UL /*0x3FFF*/, 12462UL, new string[1]
        {
          "CRC-14/GSM"
        });
      case CRCStandard.CRC15:
        return (ICRC) new CRC(15, 17817UL, 0UL, false, false, 0UL, 1438UL, new string[1]
        {
          "CRC-15"
        });
      case CRCStandard.CRC15_MPT1327:
        return (ICRC) new CRC(15, 26645UL, 0UL, false, false, 1UL, 9574UL, new string[1]
        {
          "CRC-15/MPT1327"
        });
      case CRCStandard.ARC:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, 0UL, true, true, 0UL, 47933UL, new string[5]
        {
          "CRC-16",
          "ARC",
          "CRC-IBM",
          "CRC-16/ARC",
          "CRC-16/LHA"
        });
      case CRCStandard.CRC16_AUGCCITT:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 7439UL, false, false, 0UL, 58828UL, new string[2]
        {
          "CRC-16/AUG-CCITT",
          "CRC-16/SPI-FUJITSU"
        });
      case CRCStandard.CRC16_BUYPASS:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, 0UL, false, false, 0UL, 65256UL, new string[2]
        {
          "CRC-16/BUYPASS",
          "CRC-16/VERIFONE"
        });
      case CRCStandard.CRC16_CCITTFALSE:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, (ulong) ushort.MaxValue, false, false, 0UL, 10673UL, new string[1]
        {
          "CRC-16/CCITT-FALSE"
        });
      case CRCStandard.CRC16_CDMA2000:
        return (ICRC) new CRC(16 /*0x10*/, 51303UL, (ulong) ushort.MaxValue, false, false, 0UL, 19462UL, new string[1]
        {
          "CRC-16/CDMA2000"
        });
      case CRCStandard.CRC16_CMS:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, (ulong) ushort.MaxValue, false, false, 0UL, 44775UL, new string[1]
        {
          "CRC-16/CMS"
        });
      case CRCStandard.CRC16_DDS110:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, 32781UL, false, false, 0UL, 40655UL, new string[1]
        {
          "CRC-16/DDS-110"
        });
      case CRCStandard.CRC16_DECTR:
        return (ICRC) new CRC(16 /*0x10*/, 1417UL, 0UL, false, false, 1UL, 126UL, new string[2]
        {
          "CRC-16/DECT-R",
          "R-CRC-16"
        });
      case CRCStandard.CRC16_DECTX:
        return (ICRC) new CRC(16 /*0x10*/, 1417UL, 0UL, false, false, 0UL, (ulong) sbyte.MaxValue, new string[2]
        {
          "CRC-16/DECT-X",
          "X-CRC-16"
        });
      case CRCStandard.CRC16_DNP:
        return (ICRC) new CRC(16 /*0x10*/, 15717UL, 0UL, true, true, (ulong) ushort.MaxValue, 60034UL, new string[1]
        {
          "CRC-16/DNP"
        });
      case CRCStandard.CRC16_EN13757:
        return (ICRC) new CRC(16 /*0x10*/, 15717UL, 0UL, false, false, (ulong) ushort.MaxValue, 49847UL, new string[1]
        {
          "CRC-16/EN13757"
        });
      case CRCStandard.CRC16_GENIBUS:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, (ulong) ushort.MaxValue, false, false, (ulong) ushort.MaxValue, 54862UL, new string[4]
        {
          "CRC-16/GENIBUS",
          "CRC-16/EPC",
          "CRC-16/I-CODE",
          "CRC-16/DARC"
        });
      case CRCStandard.CRC16_GSM:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 0UL, false, false, (ulong) ushort.MaxValue, 52796UL, new string[1]
        {
          "CRC-16/GSM"
        });
      case CRCStandard.CRC16_LJ1200:
        return (ICRC) new CRC(16 /*0x10*/, 28515UL, 0UL, false, false, 0UL, 48628UL, new string[1]
        {
          "CRC-16/LJ1200"
        });
      case CRCStandard.CRC16_MAXIM:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, 0UL, true, true, (ulong) ushort.MaxValue, 17602UL, new string[1]
        {
          "CRC-16/MAXIM"
        });
      case CRCStandard.CRC16_MCRF4XX:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, (ulong) ushort.MaxValue, true, true, 0UL, 28561UL, new string[1]
        {
          "CRC-16/MCRF4XX"
        });
      case CRCStandard.CRC16_OPENSAFETYA:
        return (ICRC) new CRC(16 /*0x10*/, 22837UL, 0UL, false, false, 0UL, 23864UL, new string[1]
        {
          "CRC-16/OPENSAFETY-A"
        });
      case CRCStandard.CRC16_OPENSAFETYB:
        return (ICRC) new CRC(16 /*0x10*/, 30043UL, 0UL, false, false, 0UL, 8446UL, new string[1]
        {
          "CRC-16/OPENSAFETY-B"
        });
      case CRCStandard.CRC16_PROFIBUS:
        return (ICRC) new CRC(16 /*0x10*/, 7631UL, (ulong) ushort.MaxValue, false, false, (ulong) ushort.MaxValue, 43033UL, new string[2]
        {
          "CRC-16/PROFIBUS",
          "CRC-16/IEC-61158-2"
        });
      case CRCStandard.CRC16_RIELLO:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 45738UL, true, true, 0UL, 25552UL, new string[1]
        {
          "CRC-16/RIELLO"
        });
      case CRCStandard.CRC16_T10DIF:
        return (ICRC) new CRC(16 /*0x10*/, 35767UL, 0UL, false, false, 0UL, 53467UL, new string[1]
        {
          "CRC-16/T10-DIF"
        });
      case CRCStandard.CRC16_TELEDISK:
        return (ICRC) new CRC(16 /*0x10*/, 41111UL, 0UL, false, false, 0UL, 4019UL, new string[1]
        {
          "CRC-16/TELEDISK"
        });
      case CRCStandard.CRC16_TMS37157:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 35308UL, true, true, 0UL, 9905UL, new string[1]
        {
          "CRC-16/TMS37157"
        });
      case CRCStandard.CRC16_USB:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, (ulong) ushort.MaxValue, true, true, (ulong) ushort.MaxValue, 46280UL, new string[1]
        {
          "CRC-16/USB"
        });
      case CRCStandard.CRCA:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 50886UL, true, true, 0UL, 48901UL, new string[1]
        {
          "CRC-A"
        });
      case CRCStandard.KERMIT:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 0UL, true, true, 0UL, 8585UL, new string[4]
        {
          "KERMIT",
          "CRC-16/CCITT",
          "CRC-16/CCITT-TRUE",
          "CRC-CCITT"
        });
      case CRCStandard.MODBUS:
        return (ICRC) new CRC(16 /*0x10*/, 32773UL, (ulong) ushort.MaxValue, true, true, 0UL, 19255UL, new string[1]
        {
          "MODBUS"
        });
      case CRCStandard.X25:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, (ulong) ushort.MaxValue, true, true, (ulong) ushort.MaxValue, 36974UL, new string[4]
        {
          "X-25",
          "CRC-16/IBM-SDLC",
          "CRC-16/ISO-HDLC",
          "CRC-B"
        });
      case CRCStandard.XMODEM:
        return (ICRC) new CRC(16 /*0x10*/, 4129UL, 0UL, false, false, 0UL, 12739UL, new string[3]
        {
          "XMODEM",
          "ZMODEM",
          "CRC-16/ACORN"
        });
      case CRCStandard.CRC17_CANFD:
        return (ICRC) new CRC(17, 92251UL, 0UL, false, false, 0UL, 20227UL, new string[1]
        {
          "CRC-17/CAN-FD"
        });
      case CRCStandard.CRC21_CANFD:
        return (ICRC) new CRC(21, 1058969UL, 0UL, false, false, 0UL, 972865UL, new string[1]
        {
          "CRC-21/CAN-FD"
        });
      case CRCStandard.CRC24:
        return (ICRC) new CRC(24, 8801531UL, 11994318UL, false, false, 0UL, 2215682UL, new string[2]
        {
          "CRC-24",
          "CRC-24/OPENPGP"
        });
      case CRCStandard.CRC24_BLE:
        return (ICRC) new CRC(24, 1627UL, 5592405UL /*0x555555*/, true, true, 0UL, 12737110UL, new string[1]
        {
          "CRC-24/BLE"
        });
      case CRCStandard.CRC24_FLEXRAYA:
        return (ICRC) new CRC(24, 6122955UL, 16702650UL, false, false, 0UL, 7961021UL, new string[1]
        {
          "CRC-24/FLEXRAY-A"
        });
      case CRCStandard.CRC24_FLEXRAYB:
        return (ICRC) new CRC(24, 6122955UL, 11259375UL, false, false, 0UL, 2040760UL, new string[1]
        {
          "CRC-24/FLEXRAY-B"
        });
      case CRCStandard.CRC24_INTERLAKEN:
        return (ICRC) new CRC(24, 3312483UL, 16777215UL /*0xFFFFFF*/, false, false, 16777215UL /*0xFFFFFF*/, 11858918UL, new string[1]
        {
          "CRC-24/INTERLAKEN"
        });
      case CRCStandard.CRC24_LTEA:
        return (ICRC) new CRC(24, 8801531UL, 0UL, false, false, 0UL, 13494019UL, new string[1]
        {
          "CRC-24/LTE-A"
        });
      case CRCStandard.CRC24_LTEB:
        return (ICRC) new CRC(24, 8388707UL, 0UL, false, false, 0UL, 2355026UL, new string[1]
        {
          "CRC-24/LTE-B"
        });
      case CRCStandard.CRC30_CDMA:
        return (ICRC) new CRC(30, 540064199UL, 1073741823UL /*0x3FFFFFFF*/, false, false, 1073741823UL /*0x3FFFFFFF*/, 79907519UL, new string[1]
        {
          "CRC-30/CDMA"
        });
      case CRCStandard.CRC31_PHILIPS:
        return (ICRC) new CRC(31 /*0x1F*/, 79764919UL, (ulong) int.MaxValue, false, false, (ulong) int.MaxValue, 216654956UL, new string[1]
        {
          "CRC-31/PHILLIPS"
        });
      case CRCStandard.CRC32:
        return (ICRC) new CRC(32 /*0x20*/, 79764919UL, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 3421780262UL, new string[3]
        {
          "CRC-32",
          "CRC-32/ADCCP",
          "PKZIP"
        });
      case CRCStandard.CRC32_AUTOSAR:
        return (ICRC) new CRC(32 /*0x20*/, 4104977171UL, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 379048042UL, new string[1]
        {
          "CRC-32/AUTOSAR"
        });
      case CRCStandard.CRC32_BZIP2:
        return (ICRC) new CRC(32 /*0x20*/, 79764919UL, (ulong) uint.MaxValue, false, false, (ulong) uint.MaxValue, 4236843288UL, new string[4]
        {
          "CRC-32/BZIP2",
          "CRC-32/AAL5",
          "CRC-32/DECT-B",
          "B-CRC-32"
        });
      case CRCStandard.CRC32C:
        return (ICRC) new CRC(32 /*0x20*/, 517762881UL, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 3808858755UL, new string[4]
        {
          "CRC-32C",
          "CRC-32/ISCSI",
          "CRC-32/CASTAGNOLI",
          "CRC-32/INTERLAKEN"
        });
      case CRCStandard.CRC32D:
        return (ICRC) new CRC(32 /*0x20*/, 2821953579UL, (ulong) uint.MaxValue, true, true, (ulong) uint.MaxValue, 2268157302UL, new string[1]
        {
          "CRC-32D"
        });
      case CRCStandard.CRC32_MPEG2:
        return (ICRC) new CRC(32 /*0x20*/, 79764919UL, (ulong) uint.MaxValue, false, false, 0UL, 58124007UL, new string[1]
        {
          "CRC-32/MPEG-2"
        });
      case CRCStandard.CRC32_POSIX:
        return (ICRC) new CRC(32 /*0x20*/, 79764919UL, (ulong) uint.MaxValue, false, false, 0UL, 58124007UL, new string[2]
        {
          "CRC-32/POSIX",
          "CKSUM"
        });
      case CRCStandard.CRC32Q:
        return (ICRC) new CRC(32 /*0x20*/, 2168537515UL, 0UL, false, false, 0UL, 806403967UL, new string[1]
        {
          "CRC-32Q"
        });
      case CRCStandard.JAMCRC:
        return (ICRC) new CRC(32 /*0x20*/, 79764919UL, (ulong) uint.MaxValue, true, true, 0UL, 873187033UL, new string[1]
        {
          "JAMCRC"
        });
      case CRCStandard.XFER:
        return (ICRC) new CRC(32 /*0x20*/, 175UL, 0UL, false, false, 0UL, 3171672888UL, new string[1]
        {
          "XFER"
        });
      case CRCStandard.CRC40_GSM:
        return (ICRC) new CRC(40, 75628553UL, 0UL, false, false, 1099511627775UL /*0xFFFFFFFFFF*/, 910907393606UL, new string[1]
        {
          "CRC-40/GSM"
        });
      case CRCStandard.CRC64:
        return (ICRC) new CRC(64 /*0x40*/, 4823603603198064275UL, 0UL, false, false, 0UL, 7800480153909949255UL, new string[2]
        {
          "CRC-64",
          "CRC-64/ECMA-182"
        });
      case CRCStandard.CRC64_GOISO:
        return (ICRC) new CRC(64 /*0x40*/, 27UL, ulong.MaxValue, true, true, ulong.MaxValue, 13333283586479230977UL, new string[1]
        {
          "CRC-64/GO-ISO"
        });
      case CRCStandard.CRC64_WE:
        return (ICRC) new CRC(64 /*0x40*/, 4823603603198064275UL, ulong.MaxValue, false, false, ulong.MaxValue, 7128171145767219210UL, new string[1]
        {
          "CRC-64/WE"
        });
      case CRCStandard.CRC64_XZ:
        return (ICRC) new CRC(64 /*0x40*/, 4823603603198064275UL, ulong.MaxValue, true, true, ulong.MaxValue, 11051210869376104954UL, new string[2]
        {
          "CRC-64/XZ",
          "CRC-64/GO-ECMA"
        });
      default:
        throw new ArgumentHashLibException("Invalid CRCStandard object.");
    }
  }

  public string[] Names => this.names;

  public int Width => this.width;

  public ulong Polynomial => this.polynomial;

  public ulong Initial => this.init;

  public bool IsInputReflected => this.reflectIn;

  public bool IsOutputReflected => this.reflectOut;

  public ulong OutputXor => this.xorOut;

  public ulong CheckValue => this.checkValue;

  private unsafe void GenerateTable()
  {
    uint num1 = 0;
    this.CRCTable = new ulong[256 /*0x0100*/];
    fixed (ulong* numPtr = &this.CRCTable[0])
    {
      for (; num1 < 256U /*0x0100*/; ++num1)
      {
        ulong a_value1 = (ulong) num1;
        if (this.reflectIn)
          a_value1 = CRC.Reflect(a_value1, 8);
        ulong a_value2 = a_value1 << this.width - 8;
        for (uint index = 0; index < 8U; ++index)
        {
          long num2 = (long) a_value2 & (long) this.CRCHighBitMask;
          a_value2 <<= 1;
          if (num2 != 0L)
            a_value2 ^= this.polynomial;
        }
        if (this.reflectIn)
          a_value2 = CRC.Reflect(a_value2, this.width);
        ulong num3 = a_value2 & this.CRCMask;
        *(long*) ((IntPtr) numPtr + (IntPtr) ((long) num1 * 8L)) = (long) num3;
      }
    }
    this.IsTableGenerated = true;
  }

  private unsafe void CalculateCRCbyTable(IntPtr a_data, int a_data_length, int a_index)
  {
    int num1 = a_data_length;
    int num2 = a_index;
    ulong num3 = this.hash;
    fixed (ulong* numPtr = &this.CRCTable[0])
    {
      if (this.reflectIn)
      {
        for (; num1 > 0; --num1)
        {
          num3 = num3 >> 8 ^ numPtr[(byte) (num3 ^ (ulong) *(byte*) ((IntPtr) (void*) a_data + num2))];
          ++num2;
        }
      }
      else
      {
        for (; num1 > 0; --num1)
        {
          num3 = num3 << 8 ^ numPtr[(byte) (num3 >> this.width - 8 ^ (ulong) *(byte*) ((IntPtr) (void*) a_data + num2))];
          ++num2;
        }
      }
    }
    this.hash = num3;
  }

  private unsafe void CalculateCRCdirect(IntPtr a_data, int a_data_length, int a_index)
  {
    int num1 = a_data_length;
    int num2 = a_index;
    for (; num1 > 0; --num1)
    {
      ulong a_value = (ulong) *(byte*) ((IntPtr) (void*) a_data + num2);
      if (this.reflectIn)
        a_value = CRC.Reflect(a_value, 8);
      for (ulong index = 128 /*0x80*/; index > 0UL; index >>= 1)
      {
        ulong num3 = this.hash & this.CRCHighBitMask;
        this.hash <<= 1;
        if ((a_value & index) > 0UL)
          num3 ^= this.CRCHighBitMask;
        if (num3 > 0UL)
          this.hash ^= this.polynomial;
      }
      ++num2;
    }
  }

  private static ulong Reflect(ulong a_value, int a_width)
  {
    ulong num1 = 0;
    ulong num2 = 1;
    for (ulong index = 1UL << a_width - 1; index != 0UL; index >>= 1)
    {
      if (((long) a_value & (long) index) != 0L)
        num1 |= num2;
      num2 <<= 1;
    }
    return num1;
  }
}
