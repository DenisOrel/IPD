// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19486
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19486
{
  private static byte[] sspq = new byte[82]
  {
    (byte) 196,
    (byte) 32 /*0x20*/,
    (byte) 66,
    (byte) 34,
    (byte) 214,
    (byte) 136,
    (byte) 144 /*0x90*/,
    (byte) 97,
    (byte) 189,
    (byte) 38,
    (byte) 25,
    (byte) 192 /*0xC0*/,
    (byte) 5,
    (byte) 191,
    (byte) 14,
    (byte) 84,
    (byte) 79,
    (byte) 218,
    (byte) 49,
    (byte) 129,
    (byte) 108,
    (byte) 59,
    (byte) 130,
    (byte) 19,
    (byte) 215,
    (byte) 198,
    (byte) 170,
    (byte) 96 /*0x60*/,
    (byte) 244,
    (byte) 146,
    (byte) 36,
    (byte) 141,
    (byte) 177,
    (byte) 30,
    (byte) 206,
    (byte) 239,
    (byte) 249,
    (byte) 180,
    (byte) 245,
    (byte) 249,
    (byte) 239,
    (byte) 176 /*0xB0*/,
    (byte) 143,
    (byte) 17,
    (byte) 8,
    (byte) 113,
    (byte) 94,
    (byte) 221,
    (byte) 121,
    (byte) 167,
    (byte) 196,
    (byte) 154,
    (byte) 208 /*0xD0*/,
    (byte) 14,
    (byte) 219,
    (byte) 112 /*0x70*/,
    (byte) 46,
    (byte) 164,
    (byte) 227,
    (byte) 195,
    (byte) 240 /*0xF0*/,
    (byte) 208 /*0xD0*/,
    (byte) 50,
    (byte) 140,
    (byte) 103,
    (byte) 158,
    (byte) 2,
    (byte) 207,
    (byte) 214,
    (byte) 131,
    (byte) 187,
    (byte) 220,
    (byte) 245,
    (byte) 6,
    (byte) 194,
    (byte) 183,
    (byte) 116,
    (byte) 177,
    (byte) 2,
    (byte) 65,
    (byte) 12,
    (byte) 44
  };
  private static byte[] sspr = new byte[82]
  {
    (byte) 9,
    (byte) 103,
    (byte) 199,
    (byte) 79,
    (byte) 73,
    (byte) 134,
    (byte) 11,
    (byte) 113,
    (byte) 168,
    (byte) 102,
    (byte) 232,
    (byte) 212,
    (byte) 47,
    (byte) 102,
    (byte) 204,
    (byte) 8,
    (byte) 67,
    (byte) 221,
    (byte) 153,
    (byte) 106,
    (byte) 245,
    (byte) 163,
    (byte) 186,
    (byte) 214,
    (byte) 32 /*0x20*/,
    (byte) 12,
    (byte) 249,
    (byte) 149,
    (byte) 177,
    (byte) 44,
    (byte) 190,
    (byte) 76,
    (byte) 42,
    (byte) 196,
    (byte) 177,
    (byte) 230,
    (byte) 116,
    (byte) 24,
    (byte) 240 /*0xF0*/,
    (byte) 107,
    (byte) 130,
    (byte) 245,
    (byte) 50,
    (byte) 153,
    (byte) 126,
    (byte) 249,
    (byte) 113,
    (byte) 123,
    (byte) 34,
    (byte) 114,
    (byte) 88,
    (byte) 21,
    (byte) 147,
    (byte) 1,
    (byte) 17,
    (byte) 159,
    (byte) 252,
    (byte) 84,
    (byte) 245,
    (byte) 228,
    (byte) 114,
    (byte) 33,
    (byte) 184,
    (byte) 58,
    (byte) 51,
    (byte) 56,
    (byte) 29,
    (byte) 201,
    (byte) 82,
    (byte) 170,
    (byte) 113,
    (byte) 246,
    (byte) 93,
    (byte) 123,
    (byte) 37,
    (byte) 202,
    (byte) 138,
    (byte) 122,
    (byte) 10,
    (byte) 74,
    (byte) 220,
    (byte) 33
  };

  internal static string ssp_techcard_19487()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 92,
        (byte) 18,
        (byte) 11,
        (byte) 40,
        (byte) 211,
        (byte) 46,
        (byte) 110,
        (byte) 53,
        (byte) 91,
        (byte) 123,
        (byte) 251,
        (byte) 92,
        (byte) 236,
        (byte) 108,
        (byte) 76,
        (byte) 239,
        (byte) 41,
        (byte) 165,
        (byte) 51
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 168,
        (byte) 125,
        (byte) 244,
        (byte) 22,
        (byte) 21,
        (byte) 197,
        (byte) 234,
        (byte) 194,
        (byte) 174,
        (byte) 188,
        (byte) 230,
        (byte) 52,
        (byte) 76,
        (byte) 109,
        (byte) 50,
        (byte) 16 /*0x10*/,
        (byte) 69,
        (byte) 65,
        (byte) 137
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
      (byte) 150,
      (byte) 9,
      (byte) 144 /*0x90*/,
      (byte) 17,
      (byte) 8,
      (byte) 103,
      (byte) 23,
      (byte) 82,
      (byte) 82,
      (byte) 51,
      (byte) 181,
      (byte) 247,
      (byte) 201,
      (byte) 50,
      (byte) 189,
      (byte) 50,
      (byte) 217,
      (byte) 187,
      (byte) 8
    };
    byte[] numArray6 = new byte[19];
    numArray6[11] = (byte) 107;
    numArray6[1] = (byte) 111;
    numArray6[7] = (byte) 223;
    numArray6[18] = (byte) 31 /*0x1F*/;
    numArray6[4] = (byte) 188;
    numArray6[3] = (byte) 103;
    numArray6[6] = (byte) 168;
    numArray6[14] = (byte) 211;
    numArray6[15] = (byte) 90;
    numArray6[8] = (byte) 114;
    numArray6[10] = (byte) 120;
    numArray6[2] = (byte) 101;
    numArray6[9] = (byte) 180;
    numArray6[13] = (byte) 216;
    numArray6[17] = (byte) 111;
    numArray6[0] = (byte) 226;
    numArray6[16 /*0x10*/] = (byte) 185;
    numArray6[5] = (byte) 62;
    numArray6[12] = (byte) 222;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19488()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[9] = (byte) 119;
      numArray2[15] = (byte) 194;
      numArray2[2] = (byte) 166;
      numArray2[13] = (byte) 119;
      numArray2[4] = (byte) 162;
      numArray2[5] = (byte) 25;
      numArray2[3] = (byte) 244;
      numArray2[7] = (byte) 247;
      numArray2[8] = (byte) 203;
      numArray2[0] = (byte) 41;
      numArray2[10] = (byte) 21;
      numArray2[6] = (byte) 49;
      numArray2[1] = (byte) 200;
      numArray2[17] = (byte) 210;
      numArray2[14] = (byte) 157;
      numArray2[18] = (byte) 41;
      numArray2[16 /*0x10*/] = (byte) 201;
      numArray2[12] = (byte) 96 /*0x60*/;
      numArray2[11] = (byte) 111;
      byte[] numArray3 = new byte[19];
      numArray3[7] = (byte) 167;
      numArray3[15] = (byte) 237;
      numArray3[2] = (byte) 14;
      numArray3[13] = (byte) 110;
      numArray3[4] = (byte) 157;
      numArray3[5] = (byte) 95;
      numArray3[6] = (byte) 63 /*0x3F*/;
      numArray3[18] = (byte) 119;
      numArray3[1] = (byte) 80 /*0x50*/;
      numArray3[9] = (byte) 45;
      numArray3[16 /*0x10*/] = (byte) 53;
      numArray3[11] = (byte) 189;
      numArray3[8] = (byte) 3;
      numArray3[0] = (byte) 241;
      numArray3[14] = (byte) 159;
      numArray3[12] = (byte) 7;
      numArray3[3] = (byte) 220;
      numArray3[17] = (byte) 27;
      numArray3[10] = (byte) 11;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 99,
      (byte) 117,
      (byte) 93,
      (byte) 179,
      (byte) 225,
      (byte) 21,
      (byte) 228,
      (byte) 12,
      (byte) 130,
      (byte) 248,
      (byte) 154,
      (byte) 117,
      (byte) 63 /*0x3F*/,
      (byte) 5,
      (byte) 238,
      (byte) 32 /*0x20*/,
      (byte) 3,
      (byte) 78,
      (byte) 215
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 234,
      (byte) 48 /*0x30*/,
      (byte) 74,
      (byte) 5,
      (byte) 61,
      (byte) 34,
      (byte) 242,
      (byte) 219,
      (byte) 221,
      (byte) 198,
      (byte) 253,
      (byte) 68,
      (byte) 42,
      (byte) 133,
      (byte) 86,
      (byte) 121,
      (byte) 42,
      (byte) 89,
      (byte) 67
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_19486.sspq, 0, (Array) numArray7, 0, 34);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19486.sspr, 0, (Array) numArray7, 0, 34);
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

  internal static string ssp_techcard_19489()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 146,
        (byte) 159,
        (byte) 242,
        (byte) 14,
        (byte) 132,
        (byte) 242,
        (byte) 150,
        (byte) 220,
        (byte) 78,
        (byte) 38,
        (byte) 69,
        (byte) 232,
        (byte) 97,
        (byte) 244,
        (byte) 250,
        (byte) 136,
        (byte) 45,
        (byte) 185,
        (byte) 48 /*0x30*/
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 200,
        (byte) 146,
        (byte) 170,
        (byte) 244,
        (byte) 226,
        (byte) 207,
        (byte) 22,
        (byte) 48 /*0x30*/,
        (byte) 184,
        (byte) 183,
        (byte) 10,
        (byte) 157,
        (byte) 162,
        (byte) 45,
        (byte) 107,
        (byte) 116,
        (byte) 162,
        (byte) 212,
        (byte) 112 /*0x70*/
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
      (byte) 46,
      (byte) 74,
      (byte) 16 /*0x10*/,
      (byte) 221,
      (byte) 47,
      (byte) 109,
      (byte) 42,
      (byte) 1,
      (byte) 140,
      (byte) 15,
      (byte) 231,
      (byte) 38,
      (byte) 123,
      (byte) 180,
      (byte) 92,
      (byte) 204,
      (byte) 173,
      (byte) 143,
      (byte) 213
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 12,
      (byte) 188,
      (byte) 245,
      (byte) 214,
      (byte) 2,
      (byte) 50,
      (byte) 234,
      (byte) 197,
      (byte) 144 /*0x90*/,
      (byte) 127 /*0x7F*/,
      (byte) 164,
      (byte) 57,
      (byte) 152,
      (byte) 163,
      (byte) 131,
      (byte) 64 /*0x40*/,
      (byte) 252,
      (byte) 97,
      (byte) 28
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19490()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 221,
        (byte) 73,
        (byte) 108,
        (byte) 249,
        (byte) 10,
        (byte) 247,
        (byte) 211,
        (byte) 128 /*0x80*/,
        (byte) 50,
        (byte) 16 /*0x10*/,
        (byte) 245,
        (byte) 113,
        (byte) 167,
        (byte) 253,
        (byte) 166,
        (byte) 194,
        (byte) 41,
        (byte) 177,
        (byte) 76
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 177,
        (byte) 246,
        (byte) 65,
        (byte) 230,
        (byte) 184,
        (byte) 40,
        (byte) 206,
        (byte) 240 /*0xF0*/,
        (byte) 8,
        (byte) 246,
        (byte) 175,
        (byte) 27,
        (byte) 27,
        (byte) 206,
        (byte) 14,
        (byte) 8,
        (byte) 75,
        (byte) 247,
        (byte) 239
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
      (byte) 58,
      (byte) 121,
      (byte) 69,
      (byte) 43,
      (byte) 96 /*0x60*/,
      (byte) 12,
      (byte) 161,
      (byte) 21,
      (byte) 41,
      (byte) 105,
      (byte) 65,
      (byte) 184,
      (byte) 50,
      (byte) 148,
      (byte) 195,
      (byte) 116,
      (byte) 32 /*0x20*/,
      (byte) 10,
      (byte) 184
    };
    byte[] numArray6 = new byte[19];
    numArray6[14] = (byte) 172;
    numArray6[3] = (byte) 230;
    numArray6[2] = (byte) 11;
    numArray6[9] = (byte) 128 /*0x80*/;
    numArray6[4] = (byte) 71;
    numArray6[12] = (byte) 50;
    numArray6[11] = (byte) 210;
    numArray6[0] = (byte) 60;
    numArray6[5] = (byte) 149;
    numArray6[1] = (byte) 84;
    numArray6[10] = (byte) 70;
    numArray6[8] = (byte) 6;
    numArray6[13] = (byte) 144 /*0x90*/;
    numArray6[6] = (byte) 181;
    numArray6[7] = (byte) 41;
    numArray6[15] = (byte) 53;
    numArray6[16 /*0x10*/] = (byte) 123;
    numArray6[17] = (byte) 155;
    numArray6[18] = (byte) 41;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19491()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 231,
        (byte) 140,
        (byte) 48 /*0x30*/,
        (byte) 239,
        (byte) 203,
        (byte) 18,
        byte.MaxValue,
        (byte) 181,
        (byte) 144 /*0x90*/,
        (byte) 66,
        (byte) 224 /*0xE0*/,
        (byte) 167,
        (byte) 76,
        (byte) 36,
        (byte) 243,
        (byte) 2,
        (byte) 118,
        (byte) 146,
        (byte) 5
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 225,
        (byte) 185,
        (byte) 135,
        (byte) 133,
        (byte) 238,
        (byte) 234,
        (byte) 97,
        (byte) 158,
        (byte) 195,
        (byte) 5,
        (byte) 67,
        (byte) 250,
        (byte) 28,
        (byte) 89,
        (byte) 169,
        (byte) 218,
        (byte) 205,
        (byte) 104,
        (byte) 104
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
      (byte) 130,
      (byte) 237,
      (byte) 91,
      (byte) 85,
      (byte) 133,
      (byte) 208 /*0xD0*/,
      (byte) 192 /*0xC0*/,
      (byte) 66,
      (byte) 116,
      (byte) 240 /*0xF0*/,
      (byte) 174,
      (byte) 151,
      (byte) 46,
      (byte) 249,
      (byte) 19,
      (byte) 198,
      (byte) 3,
      (byte) 244,
      (byte) 204
    };
    byte[] numArray6 = new byte[19];
    numArray6[5] = (byte) 151;
    numArray6[7] = (byte) 243;
    numArray6[11] = (byte) 47;
    numArray6[2] = (byte) 196;
    numArray6[4] = (byte) 182;
    numArray6[1] = (byte) 78;
    numArray6[6] = (byte) 206;
    numArray6[0] = (byte) 158;
    numArray6[12] = (byte) 34;
    numArray6[9] = (byte) 188;
    numArray6[15] = (byte) 223;
    numArray6[8] = (byte) 208 /*0xD0*/;
    numArray6[3] = (byte) 224 /*0xE0*/;
    numArray6[10] = (byte) 141;
    numArray6[14] = (byte) 158;
    numArray6[13] = (byte) 205;
    numArray6[16 /*0x10*/] = (byte) 192 /*0xC0*/;
    numArray6[17] = (byte) 250;
    numArray6[18] = (byte) 86;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[48 /*0x30*/];
    byte[] response = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_19486.sspq, 34, (Array) numArray7, 0, 48 /*0x30*/);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19486.sspr, 34, (Array) numArray7, 0, 48 /*0x30*/);
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
