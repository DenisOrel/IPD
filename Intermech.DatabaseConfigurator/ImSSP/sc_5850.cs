// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5850
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5850
{
  private static byte[] sspq = new byte[48 /*0x30*/]
  {
    (byte) 26,
    (byte) 120,
    (byte) 185,
    (byte) 213,
    (byte) 61,
    (byte) 5,
    (byte) 231,
    (byte) 6,
    (byte) 180,
    (byte) 144 /*0x90*/,
    (byte) 6,
    (byte) 153,
    (byte) 149,
    (byte) 64 /*0x40*/,
    (byte) 226,
    (byte) 198,
    (byte) 112 /*0x70*/,
    (byte) 215,
    (byte) 23,
    (byte) 64 /*0x40*/,
    (byte) 30,
    (byte) 246,
    (byte) 106,
    (byte) 204,
    (byte) 59,
    (byte) 84,
    (byte) 160 /*0xA0*/,
    (byte) 157,
    (byte) 226,
    (byte) 163,
    (byte) 30,
    (byte) 8,
    (byte) 167,
    (byte) 220,
    (byte) 172,
    (byte) 205,
    (byte) 138,
    (byte) 250,
    (byte) 186,
    (byte) 244,
    (byte) 184,
    (byte) 60,
    (byte) 130,
    (byte) 52,
    (byte) 99,
    (byte) 203,
    (byte) 30,
    (byte) 145
  };
  private static byte[] sspr = new byte[48 /*0x30*/]
  {
    (byte) 93,
    (byte) 155,
    (byte) 248,
    (byte) 100,
    (byte) 125,
    (byte) 40,
    (byte) 155,
    (byte) 85,
    (byte) 162,
    (byte) 46,
    (byte) 99,
    (byte) 61,
    (byte) 241,
    (byte) 189,
    (byte) 87,
    (byte) 223,
    (byte) 81,
    (byte) 169,
    (byte) 69,
    (byte) 243,
    (byte) 190,
    (byte) 120,
    (byte) 94,
    (byte) 191,
    (byte) 218,
    (byte) 1,
    (byte) 121,
    (byte) 225,
    (byte) 214,
    (byte) 188,
    (byte) 57,
    (byte) 222,
    (byte) 238,
    (byte) 190,
    (byte) 148,
    (byte) 228,
    (byte) 27,
    (byte) 58,
    (byte) 155,
    (byte) 60,
    (byte) 34,
    (byte) 217,
    (byte) 253,
    (byte) 153,
    (byte) 11,
    (byte) 60,
    (byte) 236,
    (byte) 220
  };

  internal static string ssp_imclient_5851()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 12,
        (byte) 133,
        (byte) 184,
        (byte) 80 /*0x50*/,
        (byte) 50,
        (byte) 138,
        (byte) 69,
        (byte) 214,
        (byte) 33,
        (byte) 240 /*0xF0*/,
        (byte) 60,
        (byte) 242,
        (byte) 54,
        (byte) 175,
        (byte) 183,
        (byte) 100,
        (byte) 213,
        (byte) 217,
        (byte) 23,
        (byte) 138,
        (byte) 188,
        (byte) 131,
        (byte) 121
      };
      byte[] numArray3 = new byte[23];
      numArray3[5] = (byte) 173;
      numArray3[7] = (byte) 88;
      numArray3[3] = (byte) 198;
      numArray3[13] = (byte) 43;
      numArray3[18] = (byte) 87;
      numArray3[4] = (byte) 246;
      numArray3[0] = (byte) 183;
      numArray3[22] = (byte) 70;
      numArray3[6] = (byte) 95;
      numArray3[9] = (byte) 220;
      numArray3[10] = (byte) 71;
      numArray3[2] = (byte) 247;
      numArray3[12] = (byte) 28;
      numArray3[8] = (byte) 154;
      numArray3[14] = (byte) 120;
      numArray3[15] = (byte) 93;
      numArray3[16 /*0x10*/] = (byte) 103;
      numArray3[17] = (byte) 195;
      numArray3[11] = (byte) 107;
      numArray3[19] = (byte) 116;
      numArray3[20] = (byte) 139;
      numArray3[21] = (byte) 129;
      numArray3[1] = (byte) 231;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 222,
      (byte) 58,
      (byte) 10,
      (byte) 23,
      (byte) 24,
      (byte) 65,
      (byte) 137,
      (byte) 12,
      (byte) 131,
      (byte) 253,
      (byte) 224 /*0xE0*/,
      (byte) 190,
      (byte) 243,
      (byte) 150,
      (byte) 196,
      (byte) 246,
      (byte) 75,
      (byte) 184,
      (byte) 150,
      (byte) 228,
      (byte) 141,
      (byte) 232,
      (byte) 125
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 8,
      (byte) 79,
      (byte) 245,
      (byte) 191,
      (byte) 173,
      (byte) 138,
      (byte) 150,
      (byte) 167,
      (byte) 202,
      (byte) 40,
      (byte) 108,
      (byte) 251,
      (byte) 206,
      (byte) 132,
      (byte) 15,
      (byte) 140,
      (byte) 246,
      (byte) 74,
      (byte) 32 /*0x20*/,
      (byte) 75,
      (byte) 249,
      (byte) 220,
      (byte) 76
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_5852()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[17] = (byte) 41;
      numArray2[1] = (byte) 61;
      numArray2[18] = (byte) 72;
      numArray2[3] = (byte) 29;
      numArray2[4] = (byte) 127 /*0x7F*/;
      numArray2[8] = (byte) 34;
      numArray2[2] = (byte) 114;
      numArray2[7] = (byte) 96 /*0x60*/;
      numArray2[5] = (byte) 143;
      numArray2[6] = (byte) 196;
      numArray2[10] = (byte) 217;
      numArray2[11] = (byte) 34;
      numArray2[21] = (byte) 119;
      numArray2[13] = (byte) 65;
      numArray2[19] = (byte) 168;
      numArray2[15] = (byte) 248;
      numArray2[16 /*0x10*/] = (byte) 90;
      numArray2[14] = (byte) 30;
      numArray2[9] = (byte) 238;
      numArray2[0] = (byte) 71;
      numArray2[20] = (byte) 240 /*0xF0*/;
      numArray2[12] = (byte) 95;
      numArray2[22] = (byte) 69;
      byte[] numArray3 = new byte[23];
      numArray3[17] = (byte) 127 /*0x7F*/;
      numArray3[1] = (byte) 212;
      numArray3[2] = (byte) 72;
      numArray3[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray3[4] = (byte) 224 /*0xE0*/;
      numArray3[5] = (byte) 113;
      numArray3[13] = (byte) 47;
      numArray3[7] = (byte) 68;
      numArray3[8] = (byte) 142;
      numArray3[9] = (byte) 65;
      numArray3[10] = (byte) 81;
      numArray3[11] = (byte) 152;
      numArray3[12] = (byte) 210;
      numArray3[18] = (byte) 190;
      numArray3[14] = (byte) 242;
      numArray3[15] = (byte) 4;
      numArray3[0] = (byte) 149;
      numArray3[22] = (byte) 63 /*0x3F*/;
      numArray3[19] = (byte) 17;
      numArray3[20] = (byte) 141;
      numArray3[3] = (byte) 192 /*0xC0*/;
      numArray3[21] = (byte) 121;
      numArray3[6] = (byte) 113;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_5850.sspq, 0, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_5850.sspr, 0, (Array) numArray4, 0, 48 /*0x30*/);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23];
    numArray6[1] = (byte) 141;
    numArray6[2] = (byte) 143;
    numArray6[16 /*0x10*/] = (byte) 113;
    numArray6[19] = (byte) 184;
    numArray6[4] = (byte) 208 /*0xD0*/;
    numArray6[14] = (byte) 35;
    numArray6[6] = (byte) 170;
    numArray6[13] = (byte) 73;
    numArray6[12] = (byte) 100;
    numArray6[9] = (byte) 237;
    numArray6[10] = (byte) 154;
    numArray6[11] = (byte) 233;
    numArray6[8] = (byte) 14;
    numArray6[18] = (byte) 242;
    numArray6[7] = (byte) 35;
    numArray6[15] = (byte) 17;
    numArray6[5] = (byte) 58;
    numArray6[3] = (byte) 120;
    numArray6[21] = (byte) 20;
    numArray6[0] = (byte) 21;
    numArray6[20] = (byte) 147;
    numArray6[17] = (byte) 253;
    numArray6[22] = (byte) 59;
    byte[] numArray7 = new byte[23]
    {
      (byte) 235,
      (byte) 250,
      (byte) 142,
      (byte) 250,
      (byte) 211,
      (byte) 151,
      (byte) 158,
      (byte) 121,
      (byte) 222,
      (byte) 181,
      (byte) 9,
      (byte) 203,
      (byte) 252,
      (byte) 252,
      byte.MaxValue,
      (byte) 224 /*0xE0*/,
      (byte) 99,
      (byte) 198,
      (byte) 148,
      (byte) 82,
      (byte) 141,
      (byte) 177,
      (byte) 243
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
