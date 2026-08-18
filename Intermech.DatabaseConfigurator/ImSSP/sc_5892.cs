// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5892
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5892
{
  private static byte[] sspq = new byte[40]
  {
    (byte) 229,
    (byte) 184,
    (byte) 106,
    (byte) 55,
    (byte) 178,
    (byte) 218,
    (byte) 224 /*0xE0*/,
    (byte) 213,
    (byte) 243,
    (byte) 45,
    (byte) 73,
    (byte) 37,
    (byte) 238,
    (byte) 207,
    (byte) 142,
    (byte) 73,
    (byte) 241,
    (byte) 56,
    (byte) 241,
    (byte) 68,
    (byte) 72,
    (byte) 69,
    (byte) 130,
    (byte) 167,
    (byte) 50,
    (byte) 132,
    (byte) 140,
    (byte) 4,
    (byte) 110,
    (byte) 52,
    (byte) 85,
    (byte) 247,
    byte.MaxValue,
    (byte) 230,
    (byte) 139,
    (byte) 218,
    (byte) 66,
    (byte) 241,
    (byte) 144 /*0x90*/,
    (byte) 191
  };
  private static byte[] sspr = new byte[40]
  {
    (byte) 253,
    (byte) 178,
    byte.MaxValue,
    (byte) 20,
    (byte) 102,
    (byte) 239,
    (byte) 246,
    (byte) 115,
    (byte) 224 /*0xE0*/,
    (byte) 155,
    (byte) 10,
    (byte) 189,
    (byte) 14,
    (byte) 216,
    (byte) 90,
    (byte) 139,
    (byte) 196,
    (byte) 64 /*0x40*/,
    (byte) 187,
    (byte) 122,
    (byte) 87,
    (byte) 175,
    (byte) 153,
    (byte) 51,
    (byte) 87,
    (byte) 68,
    (byte) 175,
    (byte) 49,
    (byte) 33,
    (byte) 192 /*0xC0*/,
    (byte) 137,
    (byte) 228,
    (byte) 253,
    (byte) 146,
    (byte) 143,
    (byte) 213,
    (byte) 69,
    (byte) 123,
    (byte) 229,
    (byte) 71
  };

  internal static string ssp_imclient_5893()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[23] = (byte) 202;
      numArray2[16 /*0x10*/] = (byte) 182;
      numArray2[4] = (byte) 161;
      numArray2[3] = (byte) 47;
      numArray2[1] = (byte) 140;
      numArray2[5] = (byte) 158;
      numArray2[6] = (byte) 218;
      numArray2[9] = (byte) 214;
      numArray2[8] = (byte) 231;
      numArray2[7] = (byte) 154;
      numArray2[10] = (byte) 64 /*0x40*/;
      numArray2[11] = (byte) 33;
      numArray2[12] = (byte) 246;
      numArray2[0] = (byte) 32 /*0x20*/;
      numArray2[22] = (byte) 217;
      numArray2[15] = (byte) 139;
      numArray2[13] = (byte) 25;
      numArray2[18] = (byte) 176 /*0xB0*/;
      numArray2[20] = (byte) 220;
      numArray2[19] = (byte) 120;
      numArray2[17] = (byte) 222;
      numArray2[21] = (byte) 233;
      numArray2[14] = (byte) 123;
      numArray2[2] = (byte) 164;
      byte[] numArray3 = new byte[24]
      {
        (byte) 118,
        (byte) 216,
        (byte) 71,
        (byte) 135,
        (byte) 172,
        (byte) 88,
        (byte) 92,
        (byte) 131,
        (byte) 123,
        (byte) 229,
        (byte) 241,
        (byte) 71,
        (byte) 58,
        (byte) 24,
        (byte) 237,
        (byte) 170,
        (byte) 244,
        (byte) 76,
        (byte) 254,
        (byte) 174,
        (byte) 178,
        (byte) 101,
        (byte) 63 /*0x3F*/,
        (byte) 20
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40];
      byte[] response = new byte[40];
      Array.Copy((Array) sc_5892.sspq, 0, (Array) numArray4, 0, 40);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_5892.sspr, 0, (Array) numArray4, 0, 40);
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
    numArray6[4] = (byte) 235;
    numArray6[2] = (byte) 119;
    numArray6[12] = (byte) 103;
    numArray6[3] = (byte) 200;
    numArray6[9] = (byte) 20;
    numArray6[19] = (byte) 45;
    numArray6[6] = (byte) 102;
    numArray6[20] = (byte) 78;
    numArray6[21] = (byte) 220;
    numArray6[5] = (byte) 145;
    numArray6[18] = (byte) 4;
    numArray6[11] = (byte) 30;
    numArray6[8] = (byte) 188;
    numArray6[13] = (byte) 126;
    numArray6[14] = (byte) 184;
    numArray6[15] = (byte) 159;
    numArray6[16 /*0x10*/] = (byte) 53;
    numArray6[17] = (byte) 194;
    numArray6[10] = (byte) 133;
    numArray6[23] = (byte) 190;
    numArray6[0] = (byte) 145;
    numArray6[1] = (byte) 192 /*0xC0*/;
    numArray6[22] = (byte) 102;
    numArray6[7] = (byte) 110;
    byte[] numArray7 = new byte[24]
    {
      (byte) 93,
      (byte) 106,
      (byte) 35,
      (byte) 236,
      (byte) 163,
      (byte) 53,
      (byte) 183,
      (byte) 45,
      (byte) 252,
      (byte) 93,
      (byte) 93,
      (byte) 132,
      (byte) 219,
      (byte) 0,
      (byte) 153,
      (byte) 130,
      (byte) 78,
      (byte) 200,
      (byte) 30,
      (byte) 75,
      (byte) 139,
      (byte) 50,
      (byte) 68,
      (byte) 102
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
