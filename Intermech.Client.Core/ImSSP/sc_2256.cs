
// Type: ImSSP.sc_2256
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2256
{
  internal static string ssp_imclient_2257()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 62,
        (byte) 73,
        (byte) 43,
        (byte) 166,
        (byte) 214,
        (byte) 142,
        (byte) 131,
        (byte) 128 /*0x80*/,
        (byte) 246,
        (byte) 132,
        (byte) 21,
        (byte) 51,
        (byte) 211,
        (byte) 74,
        (byte) 61,
        (byte) 252,
        (byte) 176 /*0xB0*/,
        (byte) 136,
        (byte) 42,
        (byte) 108,
        (byte) 14,
        (byte) 154,
        (byte) 30,
        (byte) 166,
        (byte) 149,
        (byte) 202,
        (byte) 121
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 239,
        (byte) 25,
        (byte) 125,
        (byte) 89,
        byte.MaxValue,
        (byte) 215,
        (byte) 11,
        (byte) 25,
        (byte) 144 /*0x90*/,
        (byte) 82,
        (byte) 191,
        (byte) 247,
        (byte) 72,
        (byte) 205,
        (byte) 208 /*0xD0*/,
        (byte) 103,
        (byte) 159,
        (byte) 178,
        (byte) 210,
        (byte) 9,
        (byte) 182,
        (byte) 195,
        (byte) 145,
        (byte) 168,
        (byte) 227,
        (byte) 182,
        (byte) 249
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 46,
      (byte) 229,
      (byte) 28,
      (byte) 10,
      (byte) 48 /*0x30*/,
      (byte) 169,
      (byte) 5,
      (byte) 46,
      (byte) 126,
      (byte) 166,
      (byte) 182,
      (byte) 72,
      (byte) 102,
      (byte) 90,
      (byte) 183,
      (byte) 178,
      (byte) 113,
      (byte) 48 /*0x30*/,
      (byte) 35,
      (byte) 64 /*0x40*/,
      (byte) 136,
      (byte) 223,
      (byte) 21,
      (byte) 95,
      (byte) 114,
      (byte) 164,
      (byte) 42
    };
    byte[] numArray6 = new byte[27]
    {
      (byte) 57,
      (byte) 200,
      (byte) 27,
      (byte) 25,
      (byte) 3,
      (byte) 204,
      (byte) 194,
      (byte) 73,
      byte.MaxValue,
      (byte) 211,
      (byte) 208 /*0xD0*/,
      (byte) 222,
      (byte) 232,
      (byte) 181,
      (byte) 138,
      (byte) 221,
      (byte) 140,
      (byte) 171,
      (byte) 7,
      (byte) 105,
      (byte) 124,
      (byte) 125,
      (byte) 161,
      (byte) 213,
      (byte) 119,
      (byte) 170,
      (byte) 244
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2258()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 182,
        (byte) 2,
        (byte) 28,
        (byte) 85,
        (byte) 48 /*0x30*/,
        (byte) 23,
        (byte) 201,
        (byte) 26,
        (byte) 147,
        (byte) 247,
        (byte) 129,
        (byte) 9,
        (byte) 132,
        (byte) 181,
        (byte) 197,
        (byte) 51,
        (byte) 196,
        (byte) 238,
        (byte) 227,
        (byte) 89,
        (byte) 73,
        (byte) 170,
        (byte) 61,
        (byte) 81,
        (byte) 79,
        (byte) 207,
        (byte) 189,
        (byte) 151,
        (byte) 140,
        (byte) 127 /*0x7F*/,
        (byte) 128 /*0x80*/,
        (byte) 29,
        (byte) 114,
        (byte) 145,
        (byte) 0,
        (byte) 70,
        (byte) 74,
        (byte) 4,
        (byte) 135,
        (byte) 154,
        (byte) 167
      };
      byte[] numArray3 = new byte[41]
      {
        (byte) 55,
        (byte) 113,
        (byte) 206,
        (byte) 8,
        (byte) 3,
        (byte) 162,
        (byte) 37,
        (byte) 123,
        (byte) 71,
        (byte) 192 /*0xC0*/,
        (byte) 180,
        (byte) 160 /*0xA0*/,
        (byte) 136,
        (byte) 220,
        (byte) 149,
        (byte) 22,
        (byte) 37,
        (byte) 68,
        (byte) 104,
        (byte) 132,
        (byte) 37,
        (byte) 208 /*0xD0*/,
        (byte) 205,
        (byte) 252,
        (byte) 110,
        (byte) 160 /*0xA0*/,
        (byte) 17,
        (byte) 226,
        (byte) 137,
        (byte) 5,
        (byte) 186,
        (byte) 220,
        (byte) 205,
        (byte) 224 /*0xE0*/,
        (byte) 250,
        (byte) 0,
        (byte) 14,
        (byte) 134,
        (byte) 253,
        (byte) 200,
        (byte) 81
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 7,
      (byte) 214,
      (byte) 145,
      (byte) 170,
      (byte) 173,
      (byte) 112 /*0x70*/,
      (byte) 101,
      (byte) 150,
      (byte) 46,
      (byte) 178,
      (byte) 203,
      (byte) 11,
      (byte) 196,
      (byte) 202,
      (byte) 6,
      (byte) 97,
      (byte) 90,
      (byte) 50,
      (byte) 142,
      (byte) 20,
      (byte) 118,
      (byte) 100,
      (byte) 150,
      (byte) 243,
      (byte) 180,
      (byte) 127 /*0x7F*/,
      (byte) 232,
      (byte) 191,
      (byte) 102,
      (byte) 72,
      (byte) 177,
      (byte) 51,
      (byte) 27,
      (byte) 243,
      (byte) 74,
      (byte) 120,
      (byte) 61,
      (byte) 18,
      (byte) 234,
      (byte) 27,
      (byte) 25
    };
    byte[] numArray6 = new byte[41]
    {
      (byte) 210,
      (byte) 116,
      (byte) 204,
      (byte) 247,
      (byte) 90,
      (byte) 168,
      (byte) 156,
      (byte) 46,
      (byte) 12,
      (byte) 88,
      (byte) 239,
      (byte) 18,
      (byte) 33,
      (byte) 219,
      (byte) 83,
      (byte) 6,
      (byte) 142,
      (byte) 67,
      (byte) 38,
      (byte) 195,
      (byte) 27,
      (byte) 1,
      (byte) 251,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 179,
      (byte) 187,
      (byte) 253,
      (byte) 18,
      (byte) 53,
      (byte) 197,
      (byte) 185,
      (byte) 219,
      (byte) 65,
      (byte) 94,
      byte.MaxValue,
      (byte) 13,
      (byte) 197,
      (byte) 145,
      (byte) 145,
      (byte) 52
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2259()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33]
      {
        (byte) 245,
        (byte) 129,
        (byte) 128 /*0x80*/,
        (byte) 253,
        (byte) 38,
        (byte) 64 /*0x40*/,
        (byte) 117,
        (byte) 134,
        (byte) 122,
        (byte) 211,
        (byte) 163,
        (byte) 63 /*0x3F*/,
        (byte) 158,
        (byte) 246,
        (byte) 45,
        (byte) 135,
        (byte) 165,
        (byte) 48 /*0x30*/,
        (byte) 229,
        (byte) 1,
        (byte) 151,
        (byte) 240 /*0xF0*/,
        (byte) 209,
        (byte) 127 /*0x7F*/,
        (byte) 141,
        (byte) 35,
        (byte) 71,
        (byte) 71,
        (byte) 114,
        (byte) 251,
        (byte) 141,
        (byte) 116,
        (byte) 214
      };
      byte[] numArray3 = new byte[33]
      {
        (byte) 114,
        (byte) 182,
        (byte) 205,
        (byte) 124,
        (byte) 186,
        (byte) 32 /*0x20*/,
        (byte) 164,
        (byte) 221,
        (byte) 231,
        (byte) 54,
        (byte) 176 /*0xB0*/,
        (byte) 72,
        (byte) 214,
        (byte) 140,
        (byte) 66,
        (byte) 246,
        (byte) 202,
        (byte) 130,
        (byte) 164,
        (byte) 138,
        (byte) 230,
        (byte) 202,
        (byte) 43,
        (byte) 90,
        (byte) 9,
        (byte) 101,
        (byte) 156,
        (byte) 44,
        (byte) 19,
        (byte) 200,
        (byte) 53,
        (byte) 249,
        (byte) 60
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 104,
      (byte) 194,
      (byte) 64 /*0x40*/,
      (byte) 7,
      (byte) 59,
      (byte) 173,
      (byte) 160 /*0xA0*/,
      (byte) 32 /*0x20*/,
      (byte) 167,
      (byte) 57,
      (byte) 159,
      (byte) 28,
      (byte) 188,
      (byte) 148,
      (byte) 115,
      (byte) 185,
      (byte) 9,
      (byte) 190,
      (byte) 45,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 0,
      (byte) 111,
      (byte) 3,
      (byte) 248,
      (byte) 45,
      (byte) 20,
      (byte) 127 /*0x7F*/,
      (byte) 69,
      (byte) 46,
      (byte) 105,
      (byte) 113,
      (byte) 55
    };
    byte[] numArray6 = new byte[33];
    numArray6[29] = (byte) 57;
    numArray6[0] = byte.MaxValue;
    numArray6[2] = (byte) 54;
    numArray6[24] = (byte) 55;
    numArray6[4] = (byte) 70;
    numArray6[5] = (byte) 89;
    numArray6[14] = (byte) 202;
    numArray6[7] = (byte) 208 /*0xD0*/;
    numArray6[8] = (byte) 124;
    numArray6[9] = (byte) 32 /*0x20*/;
    numArray6[22] = (byte) 107;
    numArray6[3] = (byte) 81;
    numArray6[23] = (byte) 100;
    numArray6[13] = (byte) 78;
    numArray6[31 /*0x1F*/] = (byte) 30;
    numArray6[15] = (byte) 86;
    numArray6[1] = (byte) 47;
    numArray6[17] = (byte) 44;
    numArray6[18] = (byte) 139;
    numArray6[28] = (byte) 23;
    numArray6[12] = (byte) 157;
    numArray6[21] = (byte) 39;
    numArray6[16 /*0x10*/] = (byte) 186;
    numArray6[6] = (byte) 173;
    numArray6[11] = (byte) 64 /*0x40*/;
    numArray6[25] = (byte) 160 /*0xA0*/;
    numArray6[26] = (byte) 190;
    numArray6[27] = (byte) 180;
    numArray6[30] = (byte) 140;
    numArray6[19] = (byte) 240 /*0xF0*/;
    numArray6[10] = (byte) 201;
    numArray6[20] = (byte) 73;
    numArray6[32 /*0x20*/] = (byte) 253;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
