// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19282
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19282
{
  internal static string ssp_techcard_19283()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 114,
        (byte) 188,
        (byte) 68,
        (byte) 53,
        (byte) 106,
        (byte) 127 /*0x7F*/,
        (byte) 80 /*0x50*/,
        (byte) 13,
        (byte) 250,
        (byte) 35,
        (byte) 203,
        (byte) 152,
        (byte) 63 /*0x3F*/,
        (byte) 130,
        (byte) 115,
        (byte) 14,
        (byte) 26,
        (byte) 95,
        (byte) 191
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 217,
        (byte) 112 /*0x70*/,
        (byte) 161,
        (byte) 236,
        (byte) 67,
        (byte) 237,
        (byte) 95,
        (byte) 179,
        (byte) 226,
        (byte) 99,
        (byte) 91,
        (byte) 44,
        (byte) 35,
        (byte) 161,
        (byte) 150,
        (byte) 54,
        (byte) 157,
        (byte) 94,
        (byte) 93
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 71,
      (byte) 51,
      (byte) 98,
      (byte) 252,
      (byte) 179,
      (byte) 173,
      (byte) 96 /*0x60*/,
      (byte) 154,
      (byte) 122,
      (byte) 39,
      (byte) 145,
      (byte) 15,
      (byte) 24,
      (byte) 162,
      (byte) 3,
      (byte) 252,
      (byte) 63 /*0x3F*/,
      (byte) 218,
      (byte) 75
    };
    byte[] numArray6 = new byte[19];
    numArray6[18] = (byte) 6;
    numArray6[15] = (byte) 195;
    numArray6[2] = (byte) 13;
    numArray6[3] = (byte) 140;
    numArray6[1] = (byte) 208 /*0xD0*/;
    numArray6[13] = (byte) 252;
    numArray6[6] = (byte) 71;
    numArray6[7] = (byte) 103;
    numArray6[14] = (byte) 235;
    numArray6[9] = (byte) 103;
    numArray6[10] = (byte) 88;
    numArray6[12] = (byte) 151;
    numArray6[8] = (byte) 41;
    numArray6[4] = (byte) 27;
    numArray6[5] = (byte) 127 /*0x7F*/;
    numArray6[17] = (byte) 38;
    numArray6[16 /*0x10*/] = (byte) 128 /*0x80*/;
    numArray6[11] = (byte) 202;
    numArray6[0] = (byte) 103;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19284()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 169,
        (byte) 164,
        (byte) 7,
        (byte) 47,
        (byte) 105,
        (byte) 209,
        (byte) 10,
        (byte) 29,
        (byte) 32 /*0x20*/,
        (byte) 228,
        (byte) 52,
        (byte) 238,
        (byte) 209,
        (byte) 94,
        (byte) 130,
        (byte) 12,
        (byte) 249,
        (byte) 235,
        (byte) 3
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 58,
        (byte) 237,
        (byte) 141,
        (byte) 141,
        (byte) 111,
        (byte) 43,
        (byte) 106,
        (byte) 225,
        (byte) 196,
        (byte) 170,
        (byte) 178,
        (byte) 146,
        (byte) 253,
        (byte) 45,
        (byte) 210,
        (byte) 83,
        (byte) 11,
        (byte) 67,
        (byte) 41
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 56,
      (byte) 96 /*0x60*/,
      (byte) 14,
      (byte) 225,
      (byte) 44,
      (byte) 242,
      (byte) 41,
      (byte) 209,
      (byte) 15,
      (byte) 168,
      (byte) 52,
      (byte) 11,
      (byte) 63 /*0x3F*/,
      (byte) 12,
      (byte) 78,
      (byte) 210,
      (byte) 94,
      (byte) 216,
      (byte) 97
    };
    byte[] numArray6 = new byte[19];
    numArray6[15] = (byte) 248;
    numArray6[17] = (byte) 27;
    numArray6[14] = (byte) 188;
    numArray6[18] = (byte) 202;
    numArray6[7] = (byte) 213;
    numArray6[5] = (byte) 143;
    numArray6[16 /*0x10*/] = (byte) 142;
    numArray6[12] = (byte) 122;
    numArray6[6] = (byte) 70;
    numArray6[9] = (byte) 133;
    numArray6[10] = (byte) 129;
    numArray6[11] = (byte) 247;
    numArray6[4] = (byte) 151;
    numArray6[13] = (byte) 212;
    numArray6[1] = (byte) 38;
    numArray6[3] = (byte) 249;
    numArray6[0] = (byte) 31 /*0x1F*/;
    numArray6[8] = (byte) 69;
    numArray6[2] = (byte) 207;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19285()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 212,
        (byte) 140,
        (byte) 186,
        (byte) 197,
        (byte) 30,
        (byte) 117,
        (byte) 190,
        (byte) 38,
        (byte) 141,
        (byte) 42,
        (byte) 146,
        (byte) 160 /*0xA0*/,
        (byte) 204,
        (byte) 46,
        (byte) 100,
        (byte) 251,
        (byte) 124,
        (byte) 22,
        (byte) 100
      };
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 95;
      numArray3[14] = (byte) 39;
      numArray3[2] = (byte) 158;
      numArray3[3] = (byte) 229;
      numArray3[4] = (byte) 40;
      numArray3[9] = (byte) 86;
      numArray3[7] = (byte) 68;
      numArray3[6] = (byte) 57;
      numArray3[12] = (byte) 66;
      numArray3[5] = (byte) 32 /*0x20*/;
      numArray3[13] = (byte) 210;
      numArray3[11] = (byte) 162;
      numArray3[1] = (byte) 161;
      numArray3[10] = (byte) 53;
      numArray3[0] = (byte) 193;
      numArray3[15] = (byte) 180;
      numArray3[16 /*0x10*/] = (byte) 210;
      numArray3[17] = (byte) 76;
      numArray3[18] = (byte) 95;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 77,
      (byte) 82,
      (byte) 116,
      (byte) 38,
      (byte) 50,
      (byte) 201,
      (byte) 60,
      (byte) 66,
      (byte) 37,
      (byte) 11,
      (byte) 142,
      (byte) 179,
      (byte) 146,
      (byte) 241,
      (byte) 165,
      (byte) 131,
      (byte) 63 /*0x3F*/,
      (byte) 141,
      (byte) 116
    };
    byte[] numArray6 = new byte[19];
    numArray6[16 /*0x10*/] = (byte) 43;
    numArray6[1] = (byte) 19;
    numArray6[10] = (byte) 57;
    numArray6[0] = (byte) 248;
    numArray6[17] = (byte) 233;
    numArray6[13] = (byte) 7;
    numArray6[6] = (byte) 120;
    numArray6[7] = (byte) 246;
    numArray6[8] = (byte) 35;
    numArray6[2] = (byte) 2;
    numArray6[12] = (byte) 186;
    numArray6[5] = (byte) 186;
    numArray6[9] = (byte) 26;
    numArray6[4] = (byte) 246;
    numArray6[14] = (byte) 242;
    numArray6[18] = (byte) 195;
    numArray6[15] = (byte) 243;
    numArray6[11] = (byte) 246;
    numArray6[3] = (byte) 158;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19286()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 139,
        (byte) 203,
        (byte) 166,
        (byte) 150,
        (byte) 103,
        (byte) 5,
        (byte) 143,
        (byte) 236,
        (byte) 25,
        (byte) 17,
        (byte) 131,
        (byte) 68,
        (byte) 241,
        (byte) 215,
        (byte) 156,
        (byte) 126,
        (byte) 221,
        (byte) 139,
        (byte) 244
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 231,
        (byte) 199,
        (byte) 177,
        (byte) 166,
        (byte) 84,
        (byte) 165,
        (byte) 165,
        (byte) 134,
        (byte) 104,
        (byte) 197,
        (byte) 38,
        (byte) 135,
        (byte) 104,
        (byte) 162,
        (byte) 0,
        (byte) 185,
        (byte) 142,
        (byte) 69,
        (byte) 103
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 45,
      (byte) 112 /*0x70*/,
      (byte) 103,
      (byte) 56,
      (byte) 225,
      (byte) 200,
      (byte) 240 /*0xF0*/,
      (byte) 235,
      (byte) 135,
      (byte) 147,
      (byte) 110,
      (byte) 177,
      (byte) 233,
      (byte) 235,
      (byte) 80 /*0x50*/,
      (byte) 179,
      (byte) 63 /*0x3F*/,
      (byte) 207,
      (byte) 106
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 44,
      (byte) 137,
      (byte) 36,
      (byte) 218,
      (byte) 176 /*0xB0*/,
      (byte) 0,
      (byte) 248,
      (byte) 124,
      (byte) 1,
      (byte) 180,
      (byte) 105,
      (byte) 62,
      (byte) 202,
      (byte) 102,
      (byte) 179,
      (byte) 20,
      (byte) 9,
      (byte) 27,
      (byte) 58
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
