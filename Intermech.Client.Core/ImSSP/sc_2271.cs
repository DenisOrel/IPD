
// Type: ImSSP.sc_2271
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2271
{
  private static byte[] sspq = new byte[11]
  {
    (byte) 163,
    (byte) 64 /*0x40*/,
    (byte) 114,
    (byte) 57,
    (byte) 97,
    (byte) 198,
    (byte) 243,
    (byte) 105,
    (byte) 182,
    (byte) 23,
    (byte) 132
  };
  private static byte[] sspr = new byte[11]
  {
    (byte) 197,
    (byte) 210,
    (byte) 118,
    (byte) 239,
    (byte) 140,
    (byte) 251,
    (byte) 61,
    (byte) 187,
    (byte) 217,
    (byte) 74,
    (byte) 225
  };

  internal static string ssp_imclient_2272()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[26];
      byte[] numArray2 = new byte[26];
      numArray2[19] = (byte) 182;
      numArray2[9] = (byte) 205;
      numArray2[6] = (byte) 40;
      numArray2[16 /*0x10*/] = (byte) 113;
      numArray2[4] = (byte) 180;
      numArray2[22] = (byte) 206;
      numArray2[1] = (byte) 43;
      numArray2[18] = (byte) 225;
      numArray2[8] = (byte) 126;
      numArray2[0] = (byte) 202;
      numArray2[10] = (byte) 162;
      numArray2[13] = (byte) 114;
      numArray2[12] = (byte) 247;
      numArray2[2] = (byte) 248;
      numArray2[14] = (byte) 195;
      numArray2[17] = (byte) 124;
      numArray2[5] = (byte) 0;
      numArray2[25] = (byte) 46;
      numArray2[15] = (byte) 81;
      numArray2[11] = (byte) 53;
      numArray2[20] = (byte) 145;
      numArray2[21] = (byte) 177;
      numArray2[3] = (byte) 67;
      numArray2[23] = (byte) 183;
      numArray2[24] = (byte) 81;
      numArray2[7] = (byte) 27;
      byte[] numArray3 = new byte[26]
      {
        (byte) 184,
        (byte) 190,
        (byte) 64 /*0x40*/,
        (byte) 243,
        (byte) 240 /*0xF0*/,
        (byte) 123,
        (byte) 157,
        (byte) 99,
        (byte) 186,
        (byte) 93,
        (byte) 244,
        (byte) 33,
        (byte) 216,
        (byte) 168,
        (byte) 36,
        (byte) 235,
        (byte) 181,
        (byte) 152,
        (byte) 75,
        (byte) 222,
        (byte) 3,
        (byte) 242,
        (byte) 252,
        (byte) 7,
        (byte) 249,
        (byte) 243
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[26];
    byte[] numArray5 = new byte[26]
    {
      (byte) 24,
      (byte) 92,
      (byte) 64 /*0x40*/,
      (byte) 241,
      (byte) 134,
      (byte) 42,
      (byte) 201,
      (byte) 150,
      (byte) 40,
      (byte) 135,
      (byte) 164,
      (byte) 204,
      byte.MaxValue,
      (byte) 104,
      (byte) 133,
      (byte) 49,
      (byte) 181,
      (byte) 119,
      (byte) 147,
      (byte) 118,
      (byte) 90,
      (byte) 243,
      (byte) 141,
      (byte) 76,
      (byte) 238,
      (byte) 4
    };
    byte[] numArray6 = new byte[26]
    {
      (byte) 59,
      (byte) 61,
      (byte) 102,
      (byte) 135,
      (byte) 36,
      (byte) 151,
      (byte) 245,
      byte.MaxValue,
      (byte) 22,
      (byte) 100,
      (byte) 92,
      (byte) 234,
      (byte) 199,
      (byte) 222,
      (byte) 248,
      (byte) 90,
      (byte) 233,
      (byte) 53,
      (byte) 182,
      byte.MaxValue,
      (byte) 93,
      (byte) 234,
      (byte) 77,
      (byte) 120,
      (byte) 132,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 26);
    for (int index = 0; index < 26; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2273()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 221,
        (byte) 82,
        (byte) 169,
        (byte) 249,
        (byte) 241,
        (byte) 225,
        (byte) 223,
        (byte) 203,
        (byte) 126,
        (byte) 35,
        (byte) 119,
        (byte) 160 /*0xA0*/,
        (byte) 149,
        (byte) 186,
        (byte) 199,
        (byte) 150,
        (byte) 42,
        byte.MaxValue,
        (byte) 47,
        (byte) 76,
        (byte) 6,
        (byte) 240 /*0xF0*/,
        (byte) 23,
        (byte) 73,
        (byte) 163,
        (byte) 37,
        (byte) 207
      };
      byte[] numArray3 = new byte[27];
      numArray3[12] = (byte) 29;
      numArray3[1] = (byte) 237;
      numArray3[2] = (byte) 253;
      numArray3[25] = (byte) 15;
      numArray3[23] = (byte) 207;
      numArray3[5] = (byte) 219;
      numArray3[6] = (byte) 89;
      numArray3[19] = (byte) 19;
      numArray3[22] = (byte) 244;
      numArray3[7] = (byte) 58;
      numArray3[4] = (byte) 10;
      numArray3[11] = (byte) 230;
      numArray3[14] = (byte) 158;
      numArray3[3] = (byte) 10;
      numArray3[21] = (byte) 123;
      numArray3[15] = (byte) 90;
      numArray3[16 /*0x10*/] = (byte) 247;
      numArray3[24] = (byte) 200;
      numArray3[18] = (byte) 125;
      numArray3[10] = (byte) 103;
      numArray3[20] = byte.MaxValue;
      numArray3[17] = (byte) 166;
      numArray3[9] = (byte) 149;
      numArray3[0] = (byte) 110;
      numArray3[8] = (byte) 150;
      numArray3[13] = (byte) 193;
      numArray3[26] = (byte) 77;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 96 /*0x60*/,
      (byte) 85,
      (byte) 203,
      (byte) 18,
      (byte) 234,
      (byte) 200,
      (byte) 251,
      (byte) 102,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 7,
      (byte) 16 /*0x10*/,
      (byte) 118,
      (byte) 116,
      (byte) 36,
      (byte) 135,
      (byte) 140,
      (byte) 250,
      (byte) 28,
      (byte) 248,
      (byte) 167,
      (byte) 74,
      (byte) 234,
      (byte) 200,
      (byte) 252,
      (byte) 111,
      (byte) 170
    };
    byte[] numArray6 = new byte[27]
    {
      (byte) 5,
      (byte) 99,
      (byte) 189,
      (byte) 123,
      (byte) 203,
      (byte) 201,
      (byte) 35,
      (byte) 95,
      (byte) 20,
      (byte) 241,
      (byte) 22,
      (byte) 169,
      (byte) 53,
      (byte) 60,
      (byte) 43,
      (byte) 214,
      (byte) 29,
      (byte) 251,
      (byte) 36,
      (byte) 252,
      (byte) 132,
      (byte) 15,
      (byte) 27,
      (byte) 69,
      (byte) 151,
      (byte) 123,
      (byte) 98
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2274()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 251,
        (byte) 188,
        (byte) 22,
        (byte) 193,
        (byte) 177,
        (byte) 172,
        (byte) 145,
        (byte) 103,
        (byte) 33,
        (byte) 253,
        (byte) 9,
        (byte) 166,
        (byte) 169,
        (byte) 85,
        (byte) 194,
        (byte) 14,
        (byte) 227,
        (byte) 181,
        (byte) 230,
        (byte) 238,
        (byte) 190,
        (byte) 118,
        (byte) 244,
        (byte) 148,
        (byte) 22,
        (byte) 107,
        (byte) 3,
        (byte) 22,
        (byte) 122,
        (byte) 29,
        (byte) 82,
        (byte) 23,
        (byte) 179,
        (byte) 57,
        (byte) 11,
        (byte) 83,
        (byte) 95,
        (byte) 178,
        (byte) 114,
        (byte) 219,
        (byte) 250
      };
      byte[] numArray3 = new byte[41]
      {
        (byte) 170,
        (byte) 69,
        (byte) 98,
        (byte) 103,
        (byte) 217,
        (byte) 67,
        (byte) 7,
        (byte) 25,
        (byte) 246,
        (byte) 223,
        (byte) 194,
        (byte) 48 /*0x30*/,
        (byte) 17,
        (byte) 17,
        (byte) 113,
        (byte) 206,
        (byte) 69,
        (byte) 203,
        (byte) 93,
        (byte) 47,
        (byte) 203,
        (byte) 94,
        (byte) 247,
        (byte) 204,
        (byte) 14,
        (byte) 106,
        (byte) 249,
        (byte) 32 /*0x20*/,
        (byte) 50,
        (byte) 114,
        (byte) 160 /*0xA0*/,
        (byte) 129,
        (byte) 239,
        (byte) 147,
        (byte) 143,
        (byte) 177,
        (byte) 234,
        (byte) 160 /*0xA0*/,
        (byte) 47,
        (byte) 183,
        (byte) 77
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_2271.sspq, 0, (Array) numArray4, 0, 11);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_2271.sspr, 0, (Array) numArray4, 0, 11);
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
    byte[] numArray5 = new byte[41];
    byte[] numArray6 = new byte[41]
    {
      (byte) 199,
      (byte) 130,
      (byte) 171,
      (byte) 69,
      (byte) 168,
      (byte) 122,
      (byte) 237,
      (byte) 4,
      (byte) 170,
      (byte) 242,
      (byte) 242,
      (byte) 111,
      (byte) 73,
      (byte) 110,
      (byte) 174,
      (byte) 49,
      (byte) 66,
      (byte) 10,
      (byte) 243,
      (byte) 44,
      (byte) 124,
      (byte) 249,
      (byte) 247,
      (byte) 114,
      (byte) 42,
      (byte) 145,
      (byte) 221,
      (byte) 25,
      (byte) 86,
      (byte) 95,
      (byte) 174,
      (byte) 253,
      (byte) 33,
      (byte) 135,
      (byte) 139,
      (byte) 221,
      (byte) 7,
      (byte) 45,
      (byte) 51,
      (byte) 128 /*0x80*/,
      (byte) 165
    };
    byte[] numArray7 = new byte[41]
    {
      (byte) 88,
      (byte) 187,
      (byte) 142,
      (byte) 84,
      (byte) 80 /*0x50*/,
      (byte) 96 /*0x60*/,
      (byte) 26,
      (byte) 86,
      (byte) 225,
      (byte) 110,
      (byte) 253,
      (byte) 253,
      (byte) 245,
      (byte) 12,
      (byte) 218,
      (byte) 2,
      (byte) 83,
      (byte) 148,
      (byte) 184,
      (byte) 60,
      (byte) 184,
      (byte) 168,
      (byte) 108,
      (byte) 16 /*0x10*/,
      (byte) 198,
      (byte) 186,
      (byte) 206,
      (byte) 222,
      (byte) 208 /*0xD0*/,
      (byte) 92,
      (byte) 103,
      (byte) 25,
      (byte) 38,
      (byte) 74,
      (byte) 8,
      (byte) 208 /*0xD0*/,
      (byte) 19,
      (byte) 244,
      (byte) 21,
      (byte) 113,
      (byte) 225
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
