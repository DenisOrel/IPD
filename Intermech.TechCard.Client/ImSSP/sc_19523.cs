// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19523
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19523
{
  internal static string ssp_techcard_19524()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 52,
        (byte) 83,
        (byte) 85,
        (byte) 103,
        (byte) 51,
        (byte) 235,
        (byte) 75,
        (byte) 106,
        (byte) 130,
        (byte) 209,
        (byte) 201,
        (byte) 36,
        (byte) 168,
        (byte) 16 /*0x10*/,
        (byte) 91,
        (byte) 76,
        (byte) 17,
        (byte) 200
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 141,
        (byte) 49,
        (byte) 141,
        (byte) 247,
        (byte) 82,
        (byte) 54,
        (byte) 214,
        (byte) 246,
        (byte) 96 /*0x60*/,
        (byte) 46,
        (byte) 181,
        (byte) 125,
        (byte) 191,
        (byte) 164,
        (byte) 147,
        (byte) 189,
        (byte) 3,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 207,
      (byte) 93,
      (byte) 160 /*0xA0*/,
      (byte) 79,
      (byte) 167,
      (byte) 132,
      (byte) 91,
      (byte) 110,
      (byte) 175,
      (byte) 229,
      (byte) 149,
      (byte) 196,
      (byte) 180,
      (byte) 147,
      (byte) 191,
      (byte) 101,
      (byte) 120,
      (byte) 237
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 194,
      (byte) 39,
      (byte) 133,
      (byte) 65,
      (byte) 23,
      (byte) 89,
      (byte) 55,
      (byte) 16 /*0x10*/,
      (byte) 207,
      (byte) 108,
      (byte) 217,
      (byte) 116,
      (byte) 71,
      (byte) 156,
      (byte) 242,
      (byte) 99,
      (byte) 193,
      (byte) 130
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_techcard_19525(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 238,
      (byte) 169,
      (byte) 223,
      (byte) 16 /*0x10*/,
      (byte) 8,
      (byte) 28,
      (byte) 79,
      (byte) 134,
      (byte) 50,
      (byte) 57,
      (byte) 3,
      (byte) 101,
      (byte) 245,
      (byte) 180,
      (byte) 23,
      (byte) 139,
      (byte) 102,
      (byte) 240 /*0xF0*/,
      (byte) 252,
      (byte) 211,
      (byte) 235,
      (byte) 51,
      (byte) 95,
      (byte) 244,
      (byte) 124,
      (byte) 156,
      (byte) 230,
      (byte) 152,
      (byte) 218,
      (byte) 199,
      (byte) 84,
      (byte) 116,
      (byte) 21,
      (byte) 38,
      (byte) 178,
      (byte) 166,
      (byte) 199,
      (byte) 116,
      (byte) 83,
      (byte) 81,
      (byte) 95,
      (byte) 95,
      (byte) 99,
      (byte) 138,
      (byte) 176 /*0xB0*/,
      (byte) 46,
      (byte) 202,
      (byte) 176 /*0xB0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 245,
      (byte) 87,
      (byte) 234,
      (byte) 71,
      (byte) 225,
      (byte) 113,
      (byte) 254,
      (byte) 149,
      (byte) 56,
      (byte) 222,
      (byte) 44,
      (byte) 145,
      (byte) 178,
      (byte) 173,
      (byte) 207,
      (byte) 155,
      (byte) 113,
      (byte) 146,
      (byte) 41,
      (byte) 155,
      (byte) 100,
      (byte) 119,
      (byte) 116,
      (byte) 201,
      (byte) 188,
      (byte) 2,
      (byte) 19,
      (byte) 98,
      (byte) 221,
      (byte) 47,
      byte.MaxValue,
      (byte) 133,
      (byte) 63 /*0x3F*/,
      (byte) 246,
      (byte) 76,
      (byte) 60,
      (byte) 197,
      (byte) 97,
      (byte) 81,
      (byte) 86,
      (byte) 164,
      (byte) 157,
      (byte) 185,
      (byte) 26,
      (byte) 158,
      (byte) 33,
      (byte) 172
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_techcard_19526()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 207,
        (byte) 231,
        (byte) 75,
        (byte) 186,
        (byte) 73,
        (byte) 235,
        (byte) 147,
        (byte) 108,
        (byte) 53,
        (byte) 182,
        (byte) 171,
        (byte) 107,
        (byte) 228,
        (byte) 143,
        (byte) 79,
        (byte) 206,
        (byte) 1,
        (byte) 113
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 31 /*0x1F*/,
        (byte) 5,
        (byte) 24,
        (byte) 171,
        (byte) 100,
        (byte) 74,
        (byte) 163,
        (byte) 170,
        (byte) 44,
        (byte) 162,
        (byte) 46,
        (byte) 219,
        (byte) 69,
        (byte) 9,
        (byte) 205,
        (byte) 169,
        (byte) 10,
        (byte) 248
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 170,
      (byte) 115,
      (byte) 140,
      (byte) 208 /*0xD0*/,
      (byte) 212,
      (byte) 67,
      (byte) 78,
      (byte) 209,
      (byte) 54,
      (byte) 55,
      (byte) 120,
      (byte) 90,
      (byte) 249,
      (byte) 164,
      (byte) 45,
      (byte) 219,
      (byte) 248,
      (byte) 92
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 9,
      (byte) 66,
      (byte) 93,
      (byte) 217,
      (byte) 78,
      (byte) 83,
      (byte) 203,
      (byte) 228,
      (byte) 35,
      (byte) 179,
      (byte) 221,
      (byte) 208 /*0xD0*/,
      (byte) 175,
      (byte) 104,
      (byte) 105,
      (byte) 126,
      (byte) 110,
      (byte) 146
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19527()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 168,
        (byte) 179,
        (byte) 248,
        (byte) 166,
        (byte) 32 /*0x20*/,
        (byte) 222,
        (byte) 251,
        (byte) 174,
        (byte) 239,
        (byte) 158,
        (byte) 122,
        (byte) 190,
        (byte) 36,
        (byte) 116,
        (byte) 191,
        (byte) 233,
        (byte) 117,
        (byte) 167
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 9,
        (byte) 90,
        (byte) 52,
        (byte) 141,
        (byte) 65,
        (byte) 68,
        (byte) 148,
        (byte) 229,
        (byte) 155,
        (byte) 46,
        (byte) 233,
        (byte) 176 /*0xB0*/,
        (byte) 233,
        (byte) 173,
        (byte) 229,
        (byte) 218,
        (byte) 62,
        (byte) 135
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 127 /*0x7F*/,
      (byte) 50,
      (byte) 16 /*0x10*/,
      (byte) 234,
      (byte) 234,
      (byte) 33,
      (byte) 243,
      (byte) 9,
      (byte) 20,
      (byte) 230,
      (byte) 16 /*0x10*/,
      (byte) 181,
      (byte) 143,
      (byte) 202,
      (byte) 216,
      (byte) 216,
      (byte) 46,
      (byte) 35
    };
    byte[] numArray6 = new byte[18];
    numArray6[9] = (byte) 141;
    numArray6[2] = (byte) 40;
    numArray6[10] = (byte) 110;
    numArray6[13] = (byte) 40;
    numArray6[0] = (byte) 115;
    numArray6[5] = (byte) 77;
    numArray6[6] = (byte) 170;
    numArray6[7] = (byte) 118;
    numArray6[8] = (byte) 12;
    numArray6[1] = (byte) 11;
    numArray6[15] = (byte) 241;
    numArray6[11] = (byte) 37;
    numArray6[3] = (byte) 145;
    numArray6[12] = (byte) 2;
    numArray6[14] = (byte) 211;
    numArray6[4] = (byte) 150;
    numArray6[16 /*0x10*/] = (byte) 92;
    numArray6[17] = (byte) 88;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
