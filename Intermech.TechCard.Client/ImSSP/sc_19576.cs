// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19576
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19576
{
  private static byte[] sspq = new byte[51]
  {
    (byte) 247,
    (byte) 236,
    (byte) 78,
    (byte) 49,
    (byte) 176 /*0xB0*/,
    (byte) 18,
    (byte) 224 /*0xE0*/,
    (byte) 123,
    (byte) 217,
    (byte) 253,
    (byte) 171,
    (byte) 196,
    (byte) 81,
    (byte) 119,
    (byte) 161,
    (byte) 8,
    (byte) 88,
    (byte) 235,
    (byte) 216,
    (byte) 0,
    (byte) 42,
    (byte) 138,
    (byte) 221,
    (byte) 20,
    (byte) 6,
    (byte) 78,
    (byte) 154,
    byte.MaxValue,
    (byte) 0,
    (byte) 197,
    (byte) 55,
    (byte) 130,
    (byte) 227,
    (byte) 154,
    (byte) 12,
    (byte) 109,
    (byte) 87,
    (byte) 19,
    (byte) 85,
    (byte) 135,
    (byte) 170,
    (byte) 6,
    (byte) 227,
    (byte) 174,
    (byte) 126,
    (byte) 192 /*0xC0*/,
    (byte) 226,
    (byte) 47,
    (byte) 166,
    (byte) 59,
    (byte) 134
  };
  private static byte[] sspr = new byte[51]
  {
    (byte) 2,
    (byte) 59,
    (byte) 82,
    (byte) 207,
    (byte) 245,
    (byte) 231,
    (byte) 73,
    (byte) 10,
    (byte) 15,
    (byte) 57,
    (byte) 10,
    (byte) 180,
    (byte) 246,
    (byte) 196,
    (byte) 21,
    (byte) 235,
    (byte) 17,
    (byte) 158,
    (byte) 52,
    (byte) 83,
    (byte) 226,
    (byte) 160 /*0xA0*/,
    (byte) 227,
    (byte) 77,
    (byte) 55,
    (byte) 191,
    (byte) 230,
    (byte) 14,
    (byte) 246,
    (byte) 84,
    (byte) 63 /*0x3F*/,
    (byte) 10,
    (byte) 182,
    (byte) 74,
    (byte) 14,
    (byte) 236,
    (byte) 162,
    (byte) 251,
    (byte) 185,
    (byte) 174,
    (byte) 176 /*0xB0*/,
    (byte) 83,
    (byte) 97,
    (byte) 175,
    (byte) 171,
    (byte) 11,
    (byte) 136,
    (byte) 78,
    (byte) 127 /*0x7F*/,
    (byte) 128 /*0x80*/,
    (byte) 89
  };

  internal static string ssp_techcard_19577()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37]
      {
        (byte) 92,
        (byte) 97,
        (byte) 131,
        (byte) 3,
        (byte) 95,
        (byte) 248,
        (byte) 118,
        (byte) 140,
        (byte) 222,
        (byte) 122,
        (byte) 40,
        (byte) 102,
        (byte) 208 /*0xD0*/,
        (byte) 56,
        (byte) 97,
        (byte) 226,
        (byte) 228,
        (byte) 254,
        (byte) 239,
        (byte) 254,
        (byte) 47,
        (byte) 193,
        (byte) 210,
        (byte) 120,
        (byte) 11,
        (byte) 79,
        (byte) 71,
        (byte) 25,
        (byte) 52,
        (byte) 163,
        (byte) 221,
        (byte) 10,
        (byte) 6,
        (byte) 19,
        (byte) 99,
        (byte) 42,
        (byte) 133
      };
      byte[] numArray3 = new byte[37]
      {
        (byte) 109,
        (byte) 166,
        (byte) 110,
        (byte) 99,
        (byte) 53,
        (byte) 15,
        (byte) 243,
        (byte) 76,
        (byte) 127 /*0x7F*/,
        (byte) 238,
        (byte) 74,
        (byte) 41,
        (byte) 41,
        (byte) 57,
        (byte) 102,
        (byte) 38,
        (byte) 84,
        (byte) 10,
        (byte) 28,
        (byte) 137,
        (byte) 187,
        (byte) 216,
        (byte) 156,
        (byte) 129,
        (byte) 208 /*0xD0*/,
        (byte) 253,
        (byte) 237,
        (byte) 115,
        (byte) 197,
        (byte) 30,
        (byte) 36,
        (byte) 245,
        (byte) 183,
        (byte) 225,
        (byte) 16 /*0x10*/,
        (byte) 196,
        (byte) 141
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[37];
    byte[] numArray5 = new byte[37]
    {
      (byte) 70,
      (byte) 127 /*0x7F*/,
      (byte) 201,
      (byte) 109,
      (byte) 186,
      (byte) 62,
      (byte) 40,
      (byte) 0,
      (byte) 40,
      (byte) 174,
      (byte) 65,
      (byte) 145,
      (byte) 70,
      (byte) 19,
      (byte) 224 /*0xE0*/,
      (byte) 155,
      (byte) 143,
      (byte) 11,
      (byte) 248,
      (byte) 15,
      (byte) 176 /*0xB0*/,
      (byte) 92,
      (byte) 87,
      (byte) 58,
      (byte) 156,
      (byte) 30,
      (byte) 96 /*0x60*/,
      (byte) 188,
      (byte) 213,
      (byte) 55,
      (byte) 92,
      (byte) 87,
      (byte) 79,
      (byte) 170,
      (byte) 183,
      (byte) 175,
      (byte) 247
    };
    byte[] numArray6 = new byte[37];
    numArray6[2] = (byte) 139;
    numArray6[1] = (byte) 221;
    numArray6[3] = (byte) 36;
    numArray6[22] = (byte) 10;
    numArray6[32 /*0x20*/] = (byte) 115;
    numArray6[34] = (byte) 134;
    numArray6[12] = (byte) 190;
    numArray6[29] = (byte) 99;
    numArray6[19] = (byte) 79;
    numArray6[9] = (byte) 25;
    numArray6[10] = (byte) 137;
    numArray6[11] = (byte) 13;
    numArray6[4] = (byte) 152;
    numArray6[35] = (byte) 67;
    numArray6[14] = (byte) 110;
    numArray6[15] = (byte) 204;
    numArray6[16 /*0x10*/] = (byte) 15;
    numArray6[17] = (byte) 47;
    numArray6[18] = (byte) 173;
    numArray6[8] = (byte) 159;
    numArray6[5] = (byte) 55;
    numArray6[21] = (byte) 94;
    numArray6[0] = (byte) 140;
    numArray6[23] = (byte) 175;
    numArray6[24] = (byte) 188;
    numArray6[25] = (byte) 209;
    numArray6[26] = (byte) 34;
    numArray6[27] = (byte) 237;
    numArray6[20] = (byte) 65;
    numArray6[6] = (byte) 74;
    numArray6[30] = (byte) 234;
    numArray6[31 /*0x1F*/] = (byte) 234;
    numArray6[7] = (byte) 60;
    numArray6[28] = (byte) 163;
    numArray6[13] = (byte) 142;
    numArray6[33] = (byte) 41;
    numArray6[36] = (byte) 57;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19578()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[15] = (byte) 33;
      numArray2[1] = (byte) 80 /*0x50*/;
      numArray2[9] = (byte) 175;
      numArray2[3] = (byte) 109;
      numArray2[4] = (byte) 138;
      numArray2[5] = (byte) 12;
      numArray2[6] = (byte) 155;
      numArray2[7] = (byte) 226;
      numArray2[11] = (byte) 205;
      numArray2[12] = (byte) 135;
      numArray2[10] = (byte) 22;
      numArray2[13] = (byte) 104;
      numArray2[8] = (byte) 194;
      numArray2[16 /*0x10*/] = (byte) 237;
      numArray2[14] = (byte) 147;
      numArray2[0] = (byte) 215;
      numArray2[17] = (byte) 187;
      numArray2[2] = (byte) 90;
      numArray2[18] = (byte) 28;
      byte[] numArray3 = new byte[19]
      {
        (byte) 80 /*0x50*/,
        (byte) 36,
        (byte) 140,
        (byte) 25,
        (byte) 213,
        (byte) 3,
        (byte) 105,
        (byte) 5,
        (byte) 128 /*0x80*/,
        (byte) 184,
        (byte) 102,
        (byte) 237,
        (byte) 14,
        (byte) 3,
        (byte) 66,
        (byte) 19,
        (byte) 29,
        (byte) 80 /*0x50*/,
        (byte) 184
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
      (byte) 111,
      (byte) 39,
      (byte) 146,
      (byte) 133,
      (byte) 142,
      (byte) 169,
      (byte) 58,
      (byte) 151,
      (byte) 43,
      (byte) 62,
      (byte) 45,
      (byte) 77,
      (byte) 106,
      (byte) 157,
      (byte) 94,
      (byte) 70,
      (byte) 51,
      (byte) 214,
      (byte) 136
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 100,
      (byte) 64 /*0x40*/,
      (byte) 151,
      (byte) 223,
      (byte) 112 /*0x70*/,
      (byte) 216,
      (byte) 159,
      (byte) 62,
      (byte) 45,
      (byte) 212,
      (byte) 106,
      (byte) 175,
      (byte) 84,
      (byte) 196,
      (byte) 126,
      (byte) 96 /*0x60*/,
      (byte) 77,
      (byte) 86,
      (byte) 4
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[24];
    byte[] response = new byte[24];
    Array.Copy((Array) sc_19576.sspq, 0, (Array) numArray7, 0, 24);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19576.sspr, 0, (Array) numArray7, 0, 24);
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

  internal static string ssp_techcard_19579()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 88,
        (byte) 58,
        (byte) 13,
        (byte) 155,
        (byte) 89,
        (byte) 56,
        (byte) 41,
        (byte) 185,
        (byte) 206,
        (byte) 155,
        (byte) 153,
        (byte) 64 /*0x40*/,
        (byte) 119,
        (byte) 137,
        (byte) 133,
        (byte) 75,
        (byte) 194,
        (byte) 84
      };
      byte[] numArray3 = new byte[18];
      numArray3[5] = (byte) 200;
      numArray3[1] = (byte) 194;
      numArray3[7] = (byte) 226;
      numArray3[6] = (byte) 249;
      numArray3[11] = (byte) 224 /*0xE0*/;
      numArray3[4] = (byte) 87;
      numArray3[16 /*0x10*/] = (byte) 248;
      numArray3[3] = (byte) 222;
      numArray3[8] = (byte) 15;
      numArray3[12] = (byte) 102;
      numArray3[10] = (byte) 91;
      numArray3[13] = (byte) 231;
      numArray3[9] = (byte) 140;
      numArray3[2] = (byte) 207;
      numArray3[14] = (byte) 108;
      numArray3[15] = (byte) 43;
      numArray3[0] = (byte) 254;
      numArray3[17] = (byte) 80 /*0x50*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[0] = (byte) 196;
    numArray5[15] = (byte) 223;
    numArray5[1] = (byte) 129;
    numArray5[3] = (byte) 131;
    numArray5[4] = (byte) 220;
    numArray5[17] = (byte) 107;
    numArray5[14] = (byte) 85;
    numArray5[16 /*0x10*/] = (byte) 75;
    numArray5[8] = (byte) 97;
    numArray5[9] = (byte) 83;
    numArray5[2] = (byte) 11;
    numArray5[11] = (byte) 104;
    numArray5[12] = (byte) 54;
    numArray5[13] = (byte) 140;
    numArray5[5] = (byte) 8;
    numArray5[10] = (byte) 180;
    numArray5[7] = (byte) 224 /*0xE0*/;
    numArray5[6] = (byte) 199;
    byte[] numArray6 = new byte[18]
    {
      (byte) 25,
      (byte) 91,
      (byte) 152,
      (byte) 190,
      (byte) 86,
      (byte) 81,
      (byte) 195,
      (byte) 242,
      (byte) 2,
      (byte) 235,
      (byte) 165,
      (byte) 104,
      (byte) 220,
      (byte) 63 /*0x3F*/,
      (byte) 202,
      (byte) 205,
      (byte) 238,
      (byte) 220
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[27];
    byte[] response = new byte[27];
    Array.Copy((Array) sc_19576.sspq, 24, (Array) numArray7, 0, 27);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19576.sspr, 24, (Array) numArray7, 0, 27);
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
