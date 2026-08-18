
// Type: ImSSP.sc_3811
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3811
{
  private static byte[] sspq = new byte[69]
  {
    (byte) 34,
    (byte) 128 /*0x80*/,
    (byte) 241,
    (byte) 238,
    (byte) 181,
    (byte) 54,
    (byte) 53,
    (byte) 111,
    (byte) 62,
    (byte) 117,
    (byte) 53,
    (byte) 36,
    (byte) 211,
    (byte) 216,
    (byte) 116,
    (byte) 246,
    (byte) 148,
    (byte) 151,
    (byte) 214,
    (byte) 124,
    (byte) 215,
    (byte) 40,
    (byte) 238,
    (byte) 36,
    (byte) 167,
    (byte) 13,
    (byte) 0,
    (byte) 123,
    (byte) 134,
    (byte) 86,
    (byte) 237,
    (byte) 91,
    (byte) 20,
    (byte) 247,
    (byte) 138,
    (byte) 178,
    (byte) 240 /*0xF0*/,
    (byte) 144 /*0x90*/,
    (byte) 32 /*0x20*/,
    (byte) 198,
    (byte) 122,
    (byte) 71,
    (byte) 134,
    (byte) 201,
    (byte) 230,
    (byte) 227,
    (byte) 135,
    (byte) 21,
    (byte) 128 /*0x80*/,
    (byte) 73,
    (byte) 146,
    (byte) 1,
    (byte) 140,
    (byte) 105,
    (byte) 167,
    (byte) 83,
    (byte) 47,
    (byte) 57,
    (byte) 188,
    (byte) 106,
    (byte) 15,
    (byte) 133,
    (byte) 9,
    (byte) 74,
    (byte) 55,
    (byte) 226,
    (byte) 175,
    (byte) 41,
    (byte) 149
  };
  private static byte[] sspr = new byte[69]
  {
    (byte) 169,
    (byte) 84,
    (byte) 58,
    (byte) 138,
    (byte) 45,
    (byte) 21,
    (byte) 152,
    (byte) 194,
    (byte) 176 /*0xB0*/,
    (byte) 150,
    (byte) 206,
    (byte) 56,
    (byte) 212,
    (byte) 253,
    (byte) 67,
    (byte) 207,
    (byte) 134,
    (byte) 156,
    (byte) 6,
    (byte) 90,
    (byte) 161,
    (byte) 221,
    (byte) 226,
    (byte) 54,
    (byte) 197,
    (byte) 39,
    (byte) 250,
    (byte) 108,
    (byte) 204,
    (byte) 12,
    (byte) 66,
    (byte) 149,
    (byte) 62,
    (byte) 175,
    (byte) 252,
    (byte) 50,
    (byte) 56,
    (byte) 192 /*0xC0*/,
    (byte) 230,
    (byte) 154,
    (byte) 58,
    (byte) 224 /*0xE0*/,
    (byte) 14,
    (byte) 54,
    (byte) 41,
    (byte) 129,
    (byte) 211,
    (byte) 129,
    (byte) 248,
    (byte) 231,
    (byte) 184,
    (byte) 170,
    (byte) 147,
    (byte) 196,
    (byte) 47,
    (byte) 162,
    (byte) 215,
    (byte) 125,
    (byte) 201,
    (byte) 183,
    (byte) 162,
    (byte) 119,
    (byte) 28,
    (byte) 15,
    (byte) 99,
    (byte) 247,
    (byte) 163,
    (byte) 185,
    (byte) 77
  };

  internal static string ssp_imclient_3812()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 160 /*0xA0*/,
        (byte) 108,
        (byte) 92,
        (byte) 130,
        (byte) 228,
        (byte) 3,
        (byte) 112 /*0x70*/,
        (byte) 246,
        (byte) 66,
        (byte) 199,
        (byte) 135,
        (byte) 194,
        (byte) 247,
        (byte) 60,
        (byte) 59
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 110,
        (byte) 106,
        (byte) 165,
        (byte) 253,
        (byte) 249,
        (byte) 249,
        (byte) 56,
        (byte) 172,
        (byte) 191,
        (byte) 78,
        (byte) 91,
        (byte) 70,
        (byte) 150,
        (byte) 215,
        (byte) 100
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[12] = (byte) 171;
    numArray5[1] = (byte) 2;
    numArray5[7] = (byte) 154;
    numArray5[2] = (byte) 65;
    numArray5[14] = (byte) 236;
    numArray5[5] = (byte) 6;
    numArray5[10] = (byte) 112 /*0x70*/;
    numArray5[0] = (byte) 207;
    numArray5[6] = (byte) 200;
    numArray5[9] = (byte) 129;
    numArray5[4] = (byte) 59;
    numArray5[11] = (byte) 125;
    numArray5[13] = (byte) 212;
    numArray5[8] = (byte) 75;
    numArray5[3] = (byte) 134;
    byte[] numArray6 = new byte[15]
    {
      (byte) 117,
      (byte) 84,
      (byte) 79,
      (byte) 4,
      (byte) 141,
      (byte) 37,
      (byte) 92,
      (byte) 108,
      (byte) 97,
      (byte) 135,
      (byte) 36,
      (byte) 5,
      (byte) 168,
      (byte) 87,
      (byte) 87
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3813()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[13] = (byte) 178;
      numArray2[1] = (byte) 237;
      numArray2[2] = (byte) 15;
      numArray2[3] = (byte) 26;
      numArray2[0] = (byte) 158;
      numArray2[5] = (byte) 86;
      numArray2[6] = (byte) 186;
      numArray2[7] = (byte) 73;
      numArray2[8] = (byte) 183;
      numArray2[10] = (byte) 134;
      numArray2[9] = (byte) 238;
      numArray2[11] = (byte) 196;
      numArray2[12] = (byte) 28;
      numArray2[4] = (byte) 48 /*0x30*/;
      numArray2[14] = (byte) 214;
      byte[] numArray3 = new byte[15];
      numArray3[1] = (byte) 96 /*0x60*/;
      numArray3[14] = (byte) 52;
      numArray3[2] = (byte) 253;
      numArray3[13] = (byte) 134;
      numArray3[10] = (byte) 131;
      numArray3[4] = (byte) 238;
      numArray3[6] = (byte) 143;
      numArray3[7] = (byte) 211;
      numArray3[8] = (byte) 17;
      numArray3[9] = (byte) 191;
      numArray3[0] = (byte) 207;
      numArray3[11] = (byte) 166;
      numArray3[5] = (byte) 45;
      numArray3[3] = (byte) 46;
      numArray3[12] = (byte) 149;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_3811.sspq, 0, (Array) numArray4, 0, 25);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_3811.sspr, 0, (Array) numArray4, 0, 25);
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
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15]
    {
      (byte) 168,
      (byte) 53,
      (byte) 186,
      (byte) 232,
      (byte) 231,
      (byte) 135,
      (byte) 217,
      (byte) 15,
      (byte) 219,
      (byte) 77,
      (byte) 194,
      (byte) 6,
      (byte) 3,
      (byte) 119,
      (byte) 16 /*0x10*/
    };
    byte[] numArray7 = new byte[15];
    numArray7[9] = (byte) 246;
    numArray7[7] = (byte) 251;
    numArray7[2] = (byte) 97;
    numArray7[3] = (byte) 71;
    numArray7[12] = (byte) 202;
    numArray7[14] = (byte) 28;
    numArray7[13] = (byte) 27;
    numArray7[0] = (byte) 228;
    numArray7[5] = (byte) 194;
    numArray7[10] = (byte) 202;
    numArray7[6] = (byte) 246;
    numArray7[11] = (byte) 87;
    numArray7[8] = (byte) 134;
    numArray7[4] = (byte) 142;
    numArray7[1] = (byte) 221;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[44];
    byte[] response1 = new byte[44];
    Array.Copy((Array) sc_3811.sspq, 25, (Array) numArray8, 0, 44);
    key.Query(true, 348, numArray8, response1);
    Array.Copy((Array) sc_3811.sspr, 25, (Array) numArray8, 0, 44);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_3814()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[14] = (byte) 196;
      numArray2[5] = (byte) 237;
      numArray2[2] = (byte) 101;
      numArray2[3] = (byte) 134;
      numArray2[4] = (byte) 43;
      numArray2[6] = (byte) 115;
      numArray2[9] = (byte) 99;
      numArray2[0] = (byte) 174;
      numArray2[8] = (byte) 230;
      numArray2[11] = (byte) 235;
      numArray2[10] = (byte) 133;
      numArray2[7] = (byte) 201;
      numArray2[1] = (byte) 196;
      numArray2[13] = (byte) 60;
      numArray2[12] = (byte) 251;
      byte[] numArray3 = new byte[15];
      numArray3[13] = (byte) 56;
      numArray3[6] = (byte) 162;
      numArray3[0] = (byte) 229;
      numArray3[1] = (byte) 113;
      numArray3[4] = (byte) 126;
      numArray3[5] = (byte) 150;
      numArray3[2] = (byte) 87;
      numArray3[7] = (byte) 156;
      numArray3[8] = (byte) 218;
      numArray3[9] = (byte) 191;
      numArray3[10] = (byte) 15;
      numArray3[3] = (byte) 72;
      numArray3[12] = (byte) 87;
      numArray3[11] = (byte) 33;
      numArray3[14] = (byte) 231;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 133,
      (byte) 75,
      (byte) 19,
      (byte) 4,
      (byte) 217,
      (byte) 94,
      (byte) 3,
      (byte) 246,
      (byte) 240 /*0xF0*/,
      (byte) 176 /*0xB0*/,
      (byte) 207,
      (byte) 54,
      (byte) 175,
      (byte) 55,
      (byte) 55
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 39,
      (byte) 183,
      (byte) 226,
      (byte) 63 /*0x3F*/,
      (byte) 226,
      (byte) 45,
      (byte) 173,
      (byte) 50,
      (byte) 179,
      (byte) 197,
      (byte) 227,
      (byte) 58,
      (byte) 110,
      (byte) 123,
      (byte) 235
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
