// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5721
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5721
{
  private static byte[] sspq = new byte[66]
  {
    (byte) 207,
    (byte) 45,
    (byte) 122,
    (byte) 163,
    (byte) 195,
    (byte) 198,
    (byte) 165,
    (byte) 182,
    (byte) 92,
    (byte) 65,
    (byte) 210,
    (byte) 157,
    (byte) 78,
    (byte) 177,
    (byte) 76,
    (byte) 130,
    (byte) 201,
    (byte) 78,
    (byte) 57,
    (byte) 51,
    (byte) 199,
    (byte) 145,
    (byte) 195,
    (byte) 110,
    (byte) 177,
    (byte) 11,
    (byte) 200,
    (byte) 83,
    (byte) 94,
    (byte) 144 /*0x90*/,
    (byte) 168,
    (byte) 107,
    (byte) 26,
    (byte) 223,
    (byte) 165,
    (byte) 52,
    (byte) 124,
    (byte) 92,
    (byte) 183,
    (byte) 149,
    (byte) 130,
    (byte) 109,
    (byte) 220,
    (byte) 31 /*0x1F*/,
    (byte) 220,
    (byte) 158,
    (byte) 215,
    (byte) 176 /*0xB0*/,
    (byte) 52,
    (byte) 169,
    (byte) 60,
    (byte) 145,
    (byte) 225,
    (byte) 93,
    (byte) 92,
    (byte) 44,
    byte.MaxValue,
    (byte) 41,
    (byte) 9,
    (byte) 235,
    (byte) 68,
    (byte) 67,
    (byte) 16 /*0x10*/,
    (byte) 227,
    (byte) 46,
    (byte) 162
  };
  private static byte[] sspr = new byte[66]
  {
    (byte) 137,
    (byte) 11,
    (byte) 39,
    (byte) 155,
    (byte) 160 /*0xA0*/,
    (byte) 249,
    (byte) 80 /*0x50*/,
    (byte) 122,
    (byte) 98,
    (byte) 22,
    (byte) 84,
    (byte) 141,
    (byte) 51,
    (byte) 186,
    (byte) 211,
    (byte) 190,
    (byte) 68,
    (byte) 220,
    (byte) 8,
    (byte) 227,
    (byte) 71,
    (byte) 77,
    (byte) 6,
    (byte) 196,
    (byte) 146,
    (byte) 19,
    (byte) 205,
    (byte) 229,
    (byte) 49,
    (byte) 10,
    (byte) 240 /*0xF0*/,
    (byte) 120,
    (byte) 152,
    (byte) 207,
    (byte) 40,
    (byte) 128 /*0x80*/,
    (byte) 183,
    (byte) 129,
    (byte) 2,
    (byte) 104,
    (byte) 239,
    (byte) 23,
    (byte) 233,
    (byte) 38,
    (byte) 14,
    (byte) 252,
    (byte) 139,
    (byte) 172,
    (byte) 209,
    (byte) 140,
    (byte) 159,
    (byte) 97,
    (byte) 127 /*0x7F*/,
    (byte) 132,
    (byte) 250,
    (byte) 143,
    (byte) 200,
    (byte) 249,
    (byte) 238,
    (byte) 116,
    (byte) 20,
    (byte) 108,
    (byte) 178,
    (byte) 108,
    (byte) 69,
    (byte) 127 /*0x7F*/
  };

  internal static string ssp_imclient_5722()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[13] = (byte) 95;
      numArray2[3] = (byte) 142;
      numArray2[0] = (byte) 203;
      numArray2[19] = (byte) 77;
      numArray2[4] = (byte) 11;
      numArray2[16 /*0x10*/] = (byte) 150;
      numArray2[17] = (byte) 247;
      numArray2[18] = (byte) 80 /*0x50*/;
      numArray2[8] = (byte) 126;
      numArray2[2] = (byte) 250;
      numArray2[10] = (byte) 87;
      numArray2[11] = (byte) 28;
      numArray2[12] = (byte) 229;
      numArray2[1] = (byte) 29;
      numArray2[14] = (byte) 89;
      numArray2[21] = (byte) 11;
      numArray2[15] = (byte) 232;
      numArray2[7] = (byte) 145;
      numArray2[6] = (byte) 98;
      numArray2[9] = (byte) 9;
      numArray2[20] = (byte) 180;
      numArray2[5] = (byte) 218;
      numArray2[22] = (byte) 1;
      numArray2[23] = (byte) 214;
      byte[] numArray3 = new byte[24]
      {
        (byte) 102,
        (byte) 246,
        (byte) 47,
        (byte) 35,
        (byte) 145,
        (byte) 34,
        (byte) 42,
        (byte) 27,
        (byte) 32 /*0x20*/,
        (byte) 235,
        (byte) 116,
        (byte) 25,
        (byte) 162,
        (byte) 55,
        (byte) 216,
        (byte) 92,
        (byte) 18,
        (byte) 170,
        (byte) 108,
        (byte) 237,
        (byte) 70,
        (byte) 172,
        (byte) 234,
        (byte) 72
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 173,
      (byte) 244,
      (byte) 185,
      (byte) 142,
      (byte) 191,
      (byte) 28,
      (byte) 36,
      (byte) 90,
      (byte) 149,
      (byte) 249,
      (byte) 46,
      (byte) 54,
      (byte) 88,
      (byte) 18,
      (byte) 216,
      (byte) 78,
      (byte) 196,
      (byte) 179,
      (byte) 231,
      (byte) 68,
      (byte) 213,
      (byte) 9,
      (byte) 49,
      (byte) 243
    };
    byte[] numArray6 = new byte[24];
    numArray6[6] = (byte) 145;
    numArray6[12] = (byte) 16 /*0x10*/;
    numArray6[2] = (byte) 221;
    numArray6[3] = (byte) 12;
    numArray6[20] = (byte) 217;
    numArray6[17] = (byte) 8;
    numArray6[4] = (byte) 142;
    numArray6[7] = (byte) 195;
    numArray6[8] = (byte) 89;
    numArray6[9] = (byte) 200;
    numArray6[21] = (byte) 171;
    numArray6[5] = (byte) 129;
    numArray6[11] = (byte) 115;
    numArray6[13] = (byte) 14;
    numArray6[16 /*0x10*/] = (byte) 56;
    numArray6[10] = (byte) 186;
    numArray6[15] = (byte) 213;
    numArray6[1] = (byte) 72;
    numArray6[22] = (byte) 44;
    numArray6[19] = (byte) 94;
    numArray6[18] = (byte) 113;
    numArray6[14] = (byte) 145;
    numArray6[0] = (byte) 126;
    numArray6[23] = (byte) 23;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[30];
    byte[] response = new byte[30];
    Array.Copy((Array) sc_5721.sspq, 0, (Array) numArray7, 0, 30);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_5721.sspr, 0, (Array) numArray7, 0, 30);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_5723()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24]
      {
        (byte) 40,
        (byte) 64 /*0x40*/,
        (byte) 40,
        (byte) 173,
        (byte) 188,
        (byte) 91,
        (byte) 165,
        (byte) 198,
        (byte) 78,
        (byte) 235,
        (byte) 53,
        (byte) 249,
        (byte) 63 /*0x3F*/,
        (byte) 148,
        (byte) 186,
        (byte) 214,
        (byte) 121,
        (byte) 73,
        (byte) 39,
        (byte) 101,
        (byte) 203,
        (byte) 36,
        (byte) 132,
        (byte) 187
      };
      byte[] numArray3 = new byte[24]
      {
        (byte) 28,
        (byte) 191,
        (byte) 246,
        (byte) 239,
        (byte) 238,
        (byte) 236,
        (byte) 146,
        (byte) 34,
        (byte) 223,
        (byte) 166,
        (byte) 84,
        (byte) 246,
        (byte) 188,
        (byte) 250,
        (byte) 41,
        (byte) 9,
        (byte) 244,
        (byte) 152,
        (byte) 105,
        (byte) 94,
        (byte) 167,
        (byte) 49,
        (byte) 46,
        (byte) 83
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 241,
      (byte) 183,
      (byte) 253,
      (byte) 82,
      (byte) 88,
      (byte) 204,
      (byte) 120,
      (byte) 170,
      (byte) 92,
      (byte) 38,
      (byte) 4,
      (byte) 144 /*0x90*/,
      (byte) 39,
      (byte) 35,
      (byte) 135,
      (byte) 235,
      (byte) 71,
      (byte) 145,
      (byte) 152,
      (byte) 131,
      (byte) 218,
      (byte) 247,
      (byte) 53,
      (byte) 78
    };
    byte[] numArray6 = new byte[24]
    {
      (byte) 83,
      (byte) 32 /*0x20*/,
      (byte) 171,
      (byte) 214,
      (byte) 189,
      (byte) 38,
      (byte) 153,
      (byte) 144 /*0x90*/,
      (byte) 153,
      (byte) 59,
      (byte) 132,
      (byte) 40,
      (byte) 73,
      (byte) 133,
      (byte) 57,
      (byte) 254,
      (byte) 207,
      (byte) 221,
      (byte) 216,
      (byte) 167,
      (byte) 170,
      (byte) 106,
      (byte) 121,
      (byte) 52
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_5724()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[22] = (byte) 61;
      numArray2[1] = (byte) 187;
      numArray2[2] = (byte) 108;
      numArray2[7] = (byte) 15;
      numArray2[18] = (byte) 0;
      numArray2[19] = (byte) 65;
      numArray2[13] = (byte) 233;
      numArray2[6] = (byte) 179;
      numArray2[15] = (byte) 175;
      numArray2[9] = (byte) 153;
      numArray2[10] = (byte) 221;
      numArray2[5] = (byte) 79;
      numArray2[12] = (byte) 198;
      numArray2[0] = (byte) 3;
      numArray2[4] = (byte) 82;
      numArray2[3] = (byte) 111;
      numArray2[16 /*0x10*/] = (byte) 117;
      numArray2[17] = (byte) 184;
      numArray2[21] = (byte) 57;
      numArray2[8] = (byte) 80 /*0x50*/;
      numArray2[20] = (byte) 68;
      numArray2[14] = (byte) 93;
      numArray2[11] = (byte) 45;
      numArray2[23] = (byte) 41;
      byte[] numArray3 = new byte[24]
      {
        (byte) 19,
        (byte) 240 /*0xF0*/,
        (byte) 194,
        (byte) 6,
        (byte) 115,
        (byte) 124,
        (byte) 33,
        (byte) 234,
        (byte) 78,
        (byte) 115,
        (byte) 98,
        (byte) 39,
        (byte) 43,
        (byte) 195,
        (byte) 157,
        (byte) 248,
        (byte) 209,
        (byte) 81,
        (byte) 222,
        (byte) 216,
        (byte) 105,
        (byte) 8,
        (byte) 168,
        (byte) 131
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24];
    numArray5[23] = (byte) 209;
    numArray5[1] = (byte) 54;
    numArray5[21] = (byte) 122;
    numArray5[3] = (byte) 184;
    numArray5[4] = (byte) 202;
    numArray5[5] = (byte) 126;
    numArray5[20] = (byte) 224 /*0xE0*/;
    numArray5[0] = (byte) 81;
    numArray5[9] = (byte) 28;
    numArray5[2] = (byte) 233;
    numArray5[10] = (byte) 185;
    numArray5[11] = (byte) 140;
    numArray5[12] = (byte) 161;
    numArray5[7] = (byte) 201;
    numArray5[14] = (byte) 187;
    numArray5[6] = (byte) 67;
    numArray5[16 /*0x10*/] = (byte) 74;
    numArray5[19] = (byte) 245;
    numArray5[8] = (byte) 107;
    numArray5[17] = (byte) 67;
    numArray5[13] = (byte) 82;
    numArray5[18] = (byte) 188;
    numArray5[22] = (byte) 39;
    numArray5[15] = (byte) 140;
    byte[] numArray6 = new byte[24];
    numArray6[18] = (byte) 82;
    numArray6[1] = (byte) 47;
    numArray6[15] = (byte) 125;
    numArray6[3] = (byte) 179;
    numArray6[14] = (byte) 240 /*0xF0*/;
    numArray6[2] = (byte) 161;
    numArray6[6] = (byte) 233;
    numArray6[7] = (byte) 29;
    numArray6[8] = (byte) 201;
    numArray6[9] = (byte) 81;
    numArray6[5] = (byte) 127 /*0x7F*/;
    numArray6[10] = (byte) 147;
    numArray6[12] = (byte) 4;
    numArray6[16 /*0x10*/] = (byte) 228;
    numArray6[17] = (byte) 47;
    numArray6[0] = (byte) 94;
    numArray6[23] = (byte) 162;
    numArray6[21] = (byte) 136;
    numArray6[11] = (byte) 146;
    numArray6[19] = (byte) 7;
    numArray6[20] = (byte) 97;
    numArray6[13] = (byte) 108;
    numArray6[22] = (byte) 112 /*0x70*/;
    numArray6[4] = (byte) 163;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[36];
    byte[] response = new byte[36];
    Array.Copy((Array) sc_5721.sspq, 30, (Array) numArray7, 0, 36);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_5721.sspr, 30, (Array) numArray7, 0, 36);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }
}
