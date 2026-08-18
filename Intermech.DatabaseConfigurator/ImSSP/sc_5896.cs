// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5896
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5896
{
  private static byte[] sspq = new byte[25]
  {
    (byte) 194,
    (byte) 19,
    (byte) 29,
    (byte) 13,
    (byte) 253,
    (byte) 43,
    (byte) 79,
    (byte) 130,
    (byte) 2,
    (byte) 225,
    (byte) 219,
    (byte) 108,
    (byte) 118,
    (byte) 49,
    (byte) 239,
    (byte) 125,
    (byte) 20,
    (byte) 87,
    (byte) 166,
    (byte) 7,
    (byte) 218,
    (byte) 145,
    (byte) 151,
    (byte) 137,
    (byte) 51
  };
  private static byte[] sspr = new byte[25]
  {
    (byte) 92,
    (byte) 233,
    (byte) 208 /*0xD0*/,
    (byte) 6,
    (byte) 234,
    (byte) 115,
    (byte) 137,
    (byte) 128 /*0x80*/,
    (byte) 157,
    (byte) 218,
    (byte) 230,
    (byte) 102,
    (byte) 230,
    (byte) 189,
    (byte) 52,
    (byte) 164,
    (byte) 123,
    (byte) 47,
    (byte) 193,
    (byte) 28,
    (byte) 50,
    (byte) 94,
    (byte) 49,
    (byte) 58,
    (byte) 243
  };

  internal static string ssp_imclient_5897()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[0] = (byte) 84;
      numArray2[14] = (byte) 227;
      numArray2[7] = (byte) 60;
      numArray2[3] = (byte) 223;
      numArray2[4] = (byte) 103;
      numArray2[16 /*0x10*/] = (byte) 120;
      numArray2[6] = (byte) 221;
      numArray2[13] = (byte) 223;
      numArray2[15] = (byte) 107;
      numArray2[1] = (byte) 193;
      numArray2[10] = (byte) 138;
      numArray2[11] = (byte) 241;
      numArray2[5] = (byte) 237;
      numArray2[22] = (byte) 195;
      numArray2[21] = (byte) 71;
      numArray2[8] = (byte) 229;
      numArray2[9] = (byte) 12;
      numArray2[17] = (byte) 74;
      numArray2[18] = (byte) 108;
      numArray2[19] = (byte) 60;
      numArray2[20] = (byte) 32 /*0x20*/;
      numArray2[12] = (byte) 128 /*0x80*/;
      numArray2[23] = (byte) 184;
      numArray2[2] = (byte) 228;
      byte[] numArray3 = new byte[24]
      {
        (byte) 145,
        (byte) 146,
        (byte) 198,
        (byte) 183,
        (byte) 248,
        (byte) 135,
        (byte) 33,
        (byte) 131,
        (byte) 190,
        (byte) 251,
        (byte) 113,
        (byte) 212,
        (byte) 154,
        (byte) 180,
        (byte) 130,
        (byte) 19,
        (byte) 237,
        (byte) 127 /*0x7F*/,
        (byte) 4,
        (byte) 90,
        (byte) 117,
        (byte) 0,
        (byte) 27,
        (byte) 192 /*0xC0*/
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_5896.sspq, 0, (Array) numArray4, 0, 25);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_5896.sspr, 0, (Array) numArray4, 0, 25);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[24];
    byte[] numArray6 = new byte[24];
    numArray6[14] = (byte) 154;
    numArray6[9] = (byte) 150;
    numArray6[2] = (byte) 80 /*0x50*/;
    numArray6[21] = (byte) 5;
    numArray6[4] = (byte) 254;
    numArray6[8] = (byte) 252;
    numArray6[6] = (byte) 17;
    numArray6[16 /*0x10*/] = (byte) 238;
    numArray6[7] = (byte) 63 /*0x3F*/;
    numArray6[10] = (byte) 239;
    numArray6[19] = (byte) 186;
    numArray6[13] = (byte) 141;
    numArray6[11] = (byte) 229;
    numArray6[3] = (byte) 239;
    numArray6[0] = (byte) 71;
    numArray6[12] = (byte) 189;
    numArray6[5] = (byte) 223;
    numArray6[17] = (byte) 216;
    numArray6[18] = (byte) 195;
    numArray6[15] = (byte) 33;
    numArray6[1] = (byte) 209;
    numArray6[20] = (byte) 166;
    numArray6[22] = (byte) 181;
    numArray6[23] = (byte) 80 /*0x50*/;
    byte[] numArray7 = new byte[24];
    numArray7[21] = (byte) 146;
    numArray7[1] = (byte) 236;
    numArray7[4] = (byte) 25;
    numArray7[3] = (byte) 200;
    numArray7[2] = (byte) 192 /*0xC0*/;
    numArray7[0] = (byte) 230;
    numArray7[14] = (byte) 220;
    numArray7[17] = (byte) 120;
    numArray7[8] = (byte) 151;
    numArray7[15] = (byte) 130;
    numArray7[11] = (byte) 214;
    numArray7[9] = (byte) 226;
    numArray7[5] = (byte) 216;
    numArray7[13] = (byte) 195;
    numArray7[16 /*0x10*/] = (byte) 11;
    numArray7[12] = (byte) 131;
    numArray7[20] = (byte) 77;
    numArray7[22] = (byte) 0;
    numArray7[18] = (byte) 182;
    numArray7[19] = (byte) 111;
    numArray7[6] = (byte) 64 /*0x40*/;
    numArray7[10] = (byte) 124;
    numArray7[7] = (byte) 8;
    numArray7[23] = (byte) 36;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
