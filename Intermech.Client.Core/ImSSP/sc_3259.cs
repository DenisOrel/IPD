
// Type: ImSSP.sc_3259
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3259
{
  private static byte[] sspq = new byte[61]
  {
    (byte) 181,
    (byte) 201,
    (byte) 16 /*0x10*/,
    (byte) 62,
    (byte) 31 /*0x1F*/,
    (byte) 111,
    (byte) 80 /*0x50*/,
    (byte) 196,
    (byte) 154,
    (byte) 156,
    (byte) 98,
    (byte) 234,
    (byte) 134,
    (byte) 93,
    (byte) 224 /*0xE0*/,
    (byte) 165,
    (byte) 122,
    (byte) 66,
    (byte) 163,
    (byte) 135,
    (byte) 182,
    (byte) 139,
    (byte) 122,
    (byte) 135,
    (byte) 149,
    (byte) 9,
    (byte) 235,
    (byte) 136,
    (byte) 42,
    (byte) 122,
    (byte) 29,
    (byte) 109,
    (byte) 30,
    (byte) 166,
    (byte) 74,
    (byte) 104,
    (byte) 19,
    (byte) 202,
    (byte) 0,
    (byte) 90,
    (byte) 157,
    (byte) 17,
    (byte) 212,
    (byte) 103,
    (byte) 175,
    (byte) 176 /*0xB0*/,
    (byte) 50,
    (byte) 128 /*0x80*/,
    (byte) 221,
    (byte) 103,
    (byte) 35,
    (byte) 46,
    (byte) 213,
    (byte) 196,
    (byte) 253,
    (byte) 240 /*0xF0*/,
    (byte) 128 /*0x80*/,
    (byte) 55,
    (byte) 67,
    (byte) 58,
    (byte) 98
  };
  private static byte[] sspr = new byte[61]
  {
    (byte) 34,
    (byte) 164,
    (byte) 22,
    (byte) 78,
    (byte) 0,
    (byte) 27,
    (byte) 118,
    (byte) 251,
    (byte) 4,
    (byte) 107,
    (byte) 178,
    (byte) 64 /*0x40*/,
    (byte) 241,
    (byte) 231,
    (byte) 39,
    (byte) 13,
    (byte) 99,
    (byte) 176 /*0xB0*/,
    (byte) 18,
    (byte) 34,
    (byte) 241,
    (byte) 215,
    (byte) 62,
    (byte) 38,
    (byte) 96 /*0x60*/,
    (byte) 0,
    (byte) 19,
    (byte) 173,
    (byte) 1,
    (byte) 205,
    (byte) 191,
    (byte) 37,
    (byte) 251,
    (byte) 87,
    (byte) 172,
    (byte) 93,
    (byte) 50,
    (byte) 176 /*0xB0*/,
    (byte) 178,
    (byte) 99,
    (byte) 137,
    (byte) 235,
    (byte) 153,
    (byte) 142,
    (byte) 66,
    (byte) 191,
    (byte) 29,
    (byte) 115,
    (byte) 193,
    (byte) 240 /*0xF0*/,
    (byte) 210,
    (byte) 129,
    (byte) 225,
    (byte) 210,
    (byte) 125,
    (byte) 97,
    (byte) 149,
    (byte) 210,
    (byte) 131,
    (byte) 65,
    (byte) 122
  };

  internal static string ssp_imclient_3260()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[8] = (byte) 132;
      numArray2[14] = (byte) 92;
      numArray2[2] = (byte) 254;
      numArray2[5] = (byte) 124;
      numArray2[4] = (byte) 186;
      numArray2[7] = (byte) 181;
      numArray2[1] = (byte) 146;
      numArray2[12] = (byte) 111;
      numArray2[13] = (byte) 91;
      numArray2[9] = (byte) 35;
      numArray2[6] = (byte) 134;
      numArray2[10] = (byte) 92;
      numArray2[11] = (byte) 172;
      numArray2[3] = (byte) 252;
      numArray2[0] = (byte) 10;
      byte[] numArray3 = new byte[15]
      {
        (byte) 63 /*0x3F*/,
        (byte) 140,
        (byte) 60,
        (byte) 96 /*0x60*/,
        (byte) 107,
        (byte) 15,
        (byte) 69,
        (byte) 93,
        (byte) 142,
        (byte) 114,
        (byte) 167,
        (byte) 125,
        (byte) 208 /*0xD0*/,
        (byte) 73,
        (byte) 83
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 158,
      (byte) 123,
      (byte) 121,
      (byte) 111,
      (byte) 170,
      (byte) 190,
      (byte) 53,
      (byte) 130,
      (byte) 56,
      (byte) 94,
      (byte) 162,
      (byte) 197,
      (byte) 48 /*0x30*/,
      (byte) 166,
      (byte) 175
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 224 /*0xE0*/,
      (byte) 52,
      (byte) 197,
      (byte) 115,
      (byte) 68,
      (byte) 234,
      (byte) 57,
      (byte) 153,
      (byte) 213,
      (byte) 58,
      (byte) 183,
      (byte) 138,
      (byte) 122,
      (byte) 232,
      (byte) 141
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[11];
    byte[] response = new byte[11];
    Array.Copy((Array) sc_3259.sspq, 0, (Array) numArray7, 0, 11);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3259.sspr, 0, (Array) numArray7, 0, 11);
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

  internal static string ssp_imclient_3261()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 88,
        (byte) 38,
        (byte) 46,
        (byte) 208 /*0xD0*/,
        (byte) 224 /*0xE0*/,
        (byte) 226,
        (byte) 182,
        (byte) 229,
        (byte) 116,
        (byte) 34,
        (byte) 123,
        (byte) 223,
        (byte) 109,
        (byte) 209,
        (byte) 201
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 155,
        (byte) 120,
        (byte) 151,
        (byte) 209,
        (byte) 181,
        (byte) 137,
        (byte) 27,
        (byte) 58,
        (byte) 226,
        (byte) 148,
        (byte) 217,
        (byte) 152,
        (byte) 131,
        (byte) 67,
        (byte) 83
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[5] = (byte) 180;
    numArray5[4] = (byte) 128 /*0x80*/;
    numArray5[2] = (byte) 126;
    numArray5[10] = (byte) 89;
    numArray5[9] = byte.MaxValue;
    numArray5[8] = (byte) 90;
    numArray5[3] = (byte) 119;
    numArray5[7] = (byte) 60;
    numArray5[0] = (byte) 177;
    numArray5[6] = (byte) 142;
    numArray5[13] = (byte) 229;
    numArray5[11] = (byte) 250;
    numArray5[12] = (byte) 154;
    numArray5[1] = (byte) 220;
    numArray5[14] = (byte) 213;
    byte[] numArray6 = new byte[15]
    {
      (byte) 178,
      (byte) 72,
      (byte) 220,
      (byte) 207,
      (byte) 227,
      (byte) 250,
      (byte) 145,
      (byte) 143,
      (byte) 96 /*0x60*/,
      (byte) 98,
      (byte) 205,
      (byte) 198,
      (byte) 52,
      (byte) 148,
      (byte) 67
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[19];
    byte[] response = new byte[19];
    Array.Copy((Array) sc_3259.sspq, 11, (Array) numArray7, 0, 19);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3259.sspr, 11, (Array) numArray7, 0, 19);
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

  internal static string ssp_imclient_3262()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[3] = (byte) 59;
      numArray2[12] = (byte) 134;
      numArray2[2] = (byte) 26;
      numArray2[4] = (byte) 153;
      numArray2[7] = (byte) 93;
      numArray2[5] = (byte) 238;
      numArray2[0] = (byte) 100;
      numArray2[11] = (byte) 132;
      numArray2[8] = (byte) 108;
      numArray2[6] = (byte) 101;
      numArray2[10] = (byte) 89;
      numArray2[14] = (byte) 250;
      numArray2[1] = (byte) 92;
      numArray2[13] = (byte) 51;
      numArray2[9] = (byte) 13;
      byte[] numArray3 = new byte[15];
      numArray3[10] = (byte) 36;
      numArray3[2] = (byte) 87;
      numArray3[7] = (byte) 152;
      numArray3[12] = (byte) 183;
      numArray3[4] = (byte) 35;
      numArray3[5] = (byte) 122;
      numArray3[6] = (byte) 245;
      numArray3[3] = (byte) 181;
      numArray3[8] = (byte) 35;
      numArray3[14] = (byte) 129;
      numArray3[9] = (byte) 77;
      numArray3[11] = (byte) 250;
      numArray3[1] = (byte) 218;
      numArray3[13] = (byte) 42;
      numArray3[0] = (byte) 52;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[1] = (byte) 185;
    numArray5[5] = (byte) 242;
    numArray5[0] = (byte) 154;
    numArray5[2] = (byte) 199;
    numArray5[4] = (byte) 49;
    numArray5[3] = (byte) 31 /*0x1F*/;
    numArray5[8] = (byte) 142;
    numArray5[7] = (byte) 56;
    numArray5[11] = (byte) 53;
    numArray5[6] = (byte) 230;
    numArray5[10] = (byte) 131;
    numArray5[12] = (byte) 227;
    numArray5[9] = (byte) 16 /*0x10*/;
    numArray5[13] = (byte) 173;
    numArray5[14] = (byte) 53;
    byte[] numArray6 = new byte[15]
    {
      (byte) 248,
      (byte) 213,
      (byte) 182,
      (byte) 243,
      (byte) 16 /*0x10*/,
      (byte) 125,
      (byte) 202,
      (byte) 131,
      (byte) 218,
      (byte) 226,
      (byte) 248,
      (byte) 204,
      (byte) 241,
      (byte) 23,
      (byte) 20
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[31 /*0x1F*/];
    byte[] response = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_3259.sspq, 30, (Array) numArray7, 0, 31 /*0x1F*/);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3259.sspr, 30, (Array) numArray7, 0, 31 /*0x1F*/);
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
