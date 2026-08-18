// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19222
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19222
{
  private static byte[] sspq = new byte[32 /*0x20*/]
  {
    (byte) 207,
    (byte) 231,
    (byte) 218,
    (byte) 24,
    (byte) 57,
    (byte) 152,
    (byte) 68,
    (byte) 1,
    (byte) 85,
    (byte) 247,
    (byte) 79,
    (byte) 119,
    (byte) 202,
    byte.MaxValue,
    (byte) 37,
    (byte) 78,
    (byte) 181,
    (byte) 79,
    (byte) 105,
    (byte) 174,
    (byte) 212,
    (byte) 119,
    (byte) 131,
    (byte) 169,
    (byte) 206,
    (byte) 217,
    (byte) 149,
    (byte) 14,
    (byte) 182,
    (byte) 63 /*0x3F*/,
    (byte) 240 /*0xF0*/,
    (byte) 235
  };
  private static byte[] sspr = new byte[32 /*0x20*/]
  {
    (byte) 152,
    (byte) 208 /*0xD0*/,
    (byte) 126,
    (byte) 245,
    (byte) 121,
    (byte) 153,
    (byte) 220,
    (byte) 60,
    (byte) 44,
    (byte) 236,
    (byte) 158,
    (byte) 83,
    (byte) 222,
    (byte) 38,
    (byte) 19,
    (byte) 211,
    (byte) 117,
    (byte) 166,
    (byte) 26,
    (byte) 107,
    (byte) 196,
    (byte) 180,
    (byte) 81,
    (byte) 180,
    (byte) 55,
    (byte) 153,
    (byte) 115,
    (byte) 33,
    (byte) 46,
    (byte) 140,
    (byte) 79,
    (byte) 115
  };

  internal static string ssp_techcard_19223()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 35,
        (byte) 220,
        (byte) 185,
        (byte) 39,
        (byte) 171,
        (byte) 135,
        (byte) 10,
        byte.MaxValue,
        (byte) 247,
        (byte) 62,
        (byte) 164,
        (byte) 83,
        (byte) 224 /*0xE0*/,
        (byte) 98,
        (byte) 4,
        (byte) 243,
        (byte) 70,
        (byte) 18,
        (byte) 34
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 167,
        (byte) 230,
        (byte) 244,
        (byte) 170,
        (byte) 225,
        (byte) 220,
        (byte) 180,
        (byte) 44,
        (byte) 100,
        (byte) 0,
        (byte) 108,
        (byte) 196,
        (byte) 186,
        byte.MaxValue,
        (byte) 171,
        (byte) 243,
        (byte) 24,
        (byte) 58,
        (byte) 180
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
      (byte) 43,
      (byte) 5,
      (byte) 157,
      (byte) 246,
      (byte) 176 /*0xB0*/,
      (byte) 213,
      (byte) 71,
      (byte) 145,
      (byte) 67,
      (byte) 202,
      (byte) 225,
      (byte) 158,
      (byte) 212,
      (byte) 107,
      (byte) 148,
      (byte) 58,
      (byte) 253,
      (byte) 167,
      (byte) 73
    };
    byte[] numArray6 = new byte[19];
    numArray6[16 /*0x10*/] = (byte) 253;
    numArray6[8] = (byte) 219;
    numArray6[17] = (byte) 133;
    numArray6[3] = (byte) 197;
    numArray6[1] = (byte) 118;
    numArray6[14] = (byte) 83;
    numArray6[2] = (byte) 91;
    numArray6[7] = (byte) 53;
    numArray6[0] = (byte) 138;
    numArray6[15] = (byte) 14;
    numArray6[10] = (byte) 176 /*0xB0*/;
    numArray6[11] = (byte) 127 /*0x7F*/;
    numArray6[12] = (byte) 42;
    numArray6[9] = (byte) 109;
    numArray6[6] = (byte) 134;
    numArray6[13] = (byte) 164;
    numArray6[4] = (byte) 91;
    numArray6[5] = (byte) 45;
    numArray6[18] = (byte) 135;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19224()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 250,
        (byte) 217,
        (byte) 114,
        (byte) 66,
        (byte) 141,
        (byte) 152,
        (byte) 95,
        (byte) 11,
        (byte) 20,
        (byte) 122,
        (byte) 151,
        (byte) 187,
        (byte) 227,
        (byte) 211,
        (byte) 160 /*0xA0*/,
        (byte) 99,
        (byte) 126,
        (byte) 19,
        (byte) 145
      };
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 126;
      numArray3[3] = (byte) 113;
      numArray3[2] = (byte) 236;
      numArray3[16 /*0x10*/] = (byte) 6;
      numArray3[5] = (byte) 51;
      numArray3[0] = (byte) 67;
      numArray3[4] = (byte) 6;
      numArray3[7] = (byte) 156;
      numArray3[11] = (byte) 13;
      numArray3[6] = (byte) 248;
      numArray3[1] = (byte) 135;
      numArray3[10] = (byte) 26;
      numArray3[9] = (byte) 218;
      numArray3[13] = (byte) 87;
      numArray3[14] = (byte) 120;
      numArray3[15] = (byte) 176 /*0xB0*/;
      numArray3[12] = (byte) 220;
      numArray3[17] = (byte) 106;
      numArray3[18] = (byte) 122;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[4] = (byte) 117;
    numArray5[5] = (byte) 71;
    numArray5[15] = (byte) 134;
    numArray5[2] = (byte) 253;
    numArray5[8] = (byte) 38;
    numArray5[13] = (byte) 162;
    numArray5[6] = (byte) 220;
    numArray5[7] = (byte) 20;
    numArray5[0] = (byte) 194;
    numArray5[18] = (byte) 227;
    numArray5[10] = (byte) 195;
    numArray5[9] = (byte) 54;
    numArray5[12] = (byte) 223;
    numArray5[3] = (byte) 210;
    numArray5[14] = (byte) 93;
    numArray5[1] = (byte) 145;
    numArray5[16 /*0x10*/] = (byte) 250;
    numArray5[11] = (byte) 162;
    numArray5[17] = (byte) 213;
    byte[] numArray6 = new byte[19]
    {
      (byte) 160 /*0xA0*/,
      (byte) 96 /*0x60*/,
      (byte) 13,
      (byte) 205,
      (byte) 3,
      (byte) 102,
      (byte) 177,
      (byte) 12,
      (byte) 41,
      (byte) 167,
      (byte) 222,
      (byte) 164,
      (byte) 59,
      (byte) 34,
      (byte) 148,
      (byte) 201,
      (byte) 181,
      (byte) 171,
      (byte) 87
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19225()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[14] = (byte) 198;
      numArray2[7] = (byte) 5;
      numArray2[2] = (byte) 13;
      numArray2[3] = (byte) 114;
      numArray2[4] = (byte) 102;
      numArray2[15] = (byte) 83;
      numArray2[17] = byte.MaxValue;
      numArray2[1] = (byte) 18;
      numArray2[8] = (byte) 204;
      numArray2[5] = (byte) 12;
      numArray2[10] = (byte) 42;
      numArray2[9] = (byte) 96 /*0x60*/;
      numArray2[12] = (byte) 143;
      numArray2[6] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 79;
      numArray2[0] = (byte) 137;
      numArray2[11] = (byte) 205;
      numArray2[13] = (byte) 103;
      numArray2[18] = (byte) 19;
      byte[] numArray3 = new byte[19]
      {
        (byte) 25,
        (byte) 252,
        (byte) 63 /*0x3F*/,
        (byte) 23,
        (byte) 217,
        (byte) 192 /*0xC0*/,
        (byte) 14,
        (byte) 222,
        (byte) 100,
        (byte) 67,
        (byte) 89,
        (byte) 135,
        (byte) 202,
        (byte) 216,
        (byte) 106,
        (byte) 187,
        (byte) 216,
        (byte) 113,
        (byte) 215
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      byte[] response = new byte[32 /*0x20*/];
      Array.Copy((Array) sc_19222.sspq, 0, (Array) numArray4, 0, 32 /*0x20*/);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19222.sspr, 0, (Array) numArray4, 0, 32 /*0x20*/);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 240 /*0xF0*/,
      (byte) 240 /*0xF0*/,
      (byte) 187,
      (byte) 6,
      (byte) 227,
      (byte) 164,
      (byte) 220,
      (byte) 10,
      (byte) 12,
      (byte) 47,
      (byte) 0,
      (byte) 229,
      (byte) 50,
      (byte) 136,
      (byte) 234,
      (byte) 210,
      (byte) 147,
      (byte) 78,
      (byte) 90
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 172,
      (byte) 97,
      (byte) 31 /*0x1F*/,
      (byte) 109,
      (byte) 73,
      (byte) 244,
      (byte) 5,
      (byte) 188,
      (byte) 82,
      (byte) 152,
      (byte) 182,
      (byte) 245,
      (byte) 136,
      (byte) 12,
      (byte) 126,
      (byte) 5,
      (byte) 92,
      (byte) 1,
      (byte) 66
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19226()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 87,
        (byte) 100,
        (byte) 49,
        (byte) 74,
        (byte) 96 /*0x60*/,
        (byte) 3,
        (byte) 54,
        (byte) 74,
        (byte) 21,
        (byte) 7,
        (byte) 99,
        (byte) 134,
        (byte) 218,
        (byte) 67,
        (byte) 30,
        (byte) 106,
        (byte) 17,
        (byte) 114,
        (byte) 95
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 149,
        (byte) 179,
        (byte) 14,
        (byte) 169,
        (byte) 13,
        (byte) 242,
        (byte) 82,
        (byte) 178,
        (byte) 176 /*0xB0*/,
        (byte) 39,
        (byte) 113,
        (byte) 114,
        (byte) 40,
        (byte) 75,
        (byte) 113,
        (byte) 97,
        (byte) 25,
        (byte) 223,
        (byte) 122
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[7] = (byte) 229;
    numArray5[1] = (byte) 118;
    numArray5[15] = (byte) 89;
    numArray5[14] = (byte) 178;
    numArray5[2] = (byte) 69;
    numArray5[4] = (byte) 194;
    numArray5[9] = (byte) 44;
    numArray5[17] = (byte) 6;
    numArray5[8] = (byte) 49;
    numArray5[18] = (byte) 214;
    numArray5[10] = (byte) 41;
    numArray5[11] = (byte) 180;
    numArray5[12] = (byte) 100;
    numArray5[0] = (byte) 158;
    numArray5[3] = (byte) 238;
    numArray5[16 /*0x10*/] = (byte) 124;
    numArray5[5] = (byte) 122;
    numArray5[6] = (byte) 104;
    numArray5[13] = (byte) 161;
    byte[] numArray6 = new byte[19];
    numArray6[6] = (byte) 159;
    numArray6[1] = (byte) 205;
    numArray6[14] = (byte) 130;
    numArray6[3] = (byte) 54;
    numArray6[7] = (byte) 210;
    numArray6[4] = (byte) 143;
    numArray6[8] = (byte) 50;
    numArray6[2] = (byte) 241;
    numArray6[11] = (byte) 252;
    numArray6[9] = (byte) 175;
    numArray6[10] = (byte) 4;
    numArray6[15] = (byte) 210;
    numArray6[5] = (byte) 65;
    numArray6[13] = (byte) 38;
    numArray6[17] = (byte) 169;
    numArray6[12] = (byte) 88;
    numArray6[16 /*0x10*/] = (byte) 206;
    numArray6[0] = (byte) 162;
    numArray6[18] = (byte) 196;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19227()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[4] = (byte) 3;
      numArray2[8] = (byte) 247;
      numArray2[7] = (byte) 219;
      numArray2[10] = (byte) 10;
      numArray2[16 /*0x10*/] = (byte) 151;
      numArray2[5] = (byte) 30;
      numArray2[6] = (byte) 131;
      numArray2[14] = (byte) 186;
      numArray2[11] = (byte) 198;
      numArray2[3] = (byte) 205;
      numArray2[17] = (byte) 165;
      numArray2[9] = (byte) 85;
      numArray2[12] = (byte) 26;
      numArray2[13] = (byte) 3;
      numArray2[2] = (byte) 229;
      numArray2[15] = (byte) 8;
      numArray2[1] = (byte) 156;
      numArray2[0] = (byte) 133;
      numArray2[18] = (byte) 234;
      byte[] numArray3 = new byte[19];
      numArray3[6] = (byte) 100;
      numArray3[1] = (byte) 57;
      numArray3[2] = (byte) 250;
      numArray3[17] = (byte) 179;
      numArray3[15] = (byte) 166;
      numArray3[5] = (byte) 31 /*0x1F*/;
      numArray3[8] = (byte) 101;
      numArray3[13] = (byte) 153;
      numArray3[0] = (byte) 100;
      numArray3[12] = (byte) 29;
      numArray3[10] = (byte) 194;
      numArray3[11] = (byte) 217;
      numArray3[9] = (byte) 242;
      numArray3[3] = (byte) 26;
      numArray3[14] = (byte) 112 /*0x70*/;
      numArray3[4] = (byte) 11;
      numArray3[16 /*0x10*/] = (byte) 195;
      numArray3[7] = (byte) 85;
      numArray3[18] = (byte) 232;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[7] = (byte) 101;
    numArray5[9] = (byte) 37;
    numArray5[16 /*0x10*/] = (byte) 105;
    numArray5[17] = (byte) 47;
    numArray5[4] = (byte) 148;
    numArray5[5] = (byte) 119;
    numArray5[6] = (byte) 184;
    numArray5[11] = (byte) 46;
    numArray5[8] = (byte) 167;
    numArray5[12] = (byte) 76;
    numArray5[10] = (byte) 23;
    numArray5[1] = (byte) 48 /*0x30*/;
    numArray5[2] = (byte) 213;
    numArray5[13] = (byte) 226;
    numArray5[18] = (byte) 107;
    numArray5[0] = (byte) 70;
    numArray5[15] = (byte) 85;
    numArray5[3] = (byte) 228;
    numArray5[14] = (byte) 84;
    byte[] numArray6 = new byte[19];
    numArray6[6] = (byte) 218;
    numArray6[1] = (byte) 202;
    numArray6[2] = (byte) 96 /*0x60*/;
    numArray6[3] = (byte) 212;
    numArray6[17] = (byte) 68;
    numArray6[16 /*0x10*/] = (byte) 174;
    numArray6[5] = (byte) 109;
    numArray6[7] = (byte) 108;
    numArray6[8] = byte.MaxValue;
    numArray6[0] = (byte) 178;
    numArray6[14] = (byte) 233;
    numArray6[11] = (byte) 228;
    numArray6[12] = (byte) 106;
    numArray6[13] = (byte) 91;
    numArray6[18] = (byte) 100;
    numArray6[15] = (byte) 148;
    numArray6[9] = (byte) 8;
    numArray6[10] = (byte) 199;
    numArray6[4] = (byte) 57;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
