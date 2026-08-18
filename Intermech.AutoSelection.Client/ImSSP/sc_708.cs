// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_708
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_708
{
  private static byte[] sspq = new byte[10]
  {
    (byte) 48 /*0x30*/,
    (byte) 146,
    (byte) 98,
    (byte) 39,
    (byte) 105,
    (byte) 117,
    (byte) 172,
    (byte) 66,
    (byte) 195,
    (byte) 0
  };
  private static byte[] sspr = new byte[10]
  {
    (byte) 208 /*0xD0*/,
    (byte) 87,
    (byte) 240 /*0xF0*/,
    (byte) 188,
    (byte) 49,
    (byte) 35,
    (byte) 204,
    (byte) 72,
    (byte) 82,
    (byte) 149
  };

  internal static string ssp_automatch_709()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 151,
        (byte) 95,
        (byte) 211,
        (byte) 6,
        (byte) 65,
        (byte) 240 /*0xF0*/,
        (byte) 137,
        (byte) 213,
        (byte) 81,
        (byte) 134,
        (byte) 36,
        (byte) 118,
        (byte) 188,
        (byte) 160 /*0xA0*/,
        (byte) 8,
        (byte) 215,
        (byte) 219,
        (byte) 25,
        (byte) 31 /*0x1F*/,
        (byte) 130,
        (byte) 178,
        (byte) 6,
        (byte) 77
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 218,
        (byte) 240 /*0xF0*/,
        (byte) 21,
        (byte) 174,
        (byte) 28,
        (byte) 136,
        (byte) 51,
        (byte) 138,
        (byte) 243,
        (byte) 56,
        (byte) 165,
        (byte) 50,
        (byte) 96 /*0x60*/,
        (byte) 246,
        (byte) 246,
        (byte) 104,
        (byte) 129,
        (byte) 162,
        (byte) 32 /*0x20*/,
        (byte) 10,
        (byte) 217,
        (byte) 221,
        (byte) 181
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[20] = (byte) 151;
    numArray5[1] = (byte) 22;
    numArray5[17] = (byte) 253;
    numArray5[3] = (byte) 200;
    numArray5[2] = (byte) 11;
    numArray5[5] = (byte) 164;
    numArray5[9] = (byte) 5;
    numArray5[12] = (byte) 226;
    numArray5[4] = (byte) 28;
    numArray5[18] = (byte) 174;
    numArray5[21] = (byte) 253;
    numArray5[15] = (byte) 79;
    numArray5[0] = (byte) 20;
    numArray5[13] = (byte) 232;
    numArray5[14] = (byte) 173;
    numArray5[22] = (byte) 234;
    numArray5[16 /*0x10*/] = (byte) 222;
    numArray5[6] = (byte) 78;
    numArray5[10] = (byte) 11;
    numArray5[19] = (byte) 26;
    numArray5[8] = (byte) 230;
    numArray5[7] = (byte) 179;
    numArray5[11] = (byte) 215;
    byte[] numArray6 = new byte[23]
    {
      (byte) 120,
      (byte) 227,
      (byte) 236,
      (byte) 240 /*0xF0*/,
      (byte) 224 /*0xE0*/,
      (byte) 221,
      (byte) 206,
      (byte) 99,
      (byte) 244,
      (byte) 125,
      (byte) 234,
      byte.MaxValue,
      (byte) 180,
      (byte) 227,
      (byte) 139,
      (byte) 80 /*0x50*/,
      (byte) 211,
      (byte) 110,
      (byte) 247,
      (byte) 43,
      (byte) 187,
      (byte) 27,
      (byte) 229
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_710()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 40,
        (byte) 79,
        (byte) 20,
        (byte) 180,
        (byte) 49,
        (byte) 23,
        (byte) 43,
        (byte) 51,
        (byte) 155,
        (byte) 173,
        (byte) 33,
        (byte) 75,
        (byte) 30,
        (byte) 85,
        (byte) 243,
        (byte) 250,
        (byte) 159,
        (byte) 106,
        (byte) 11,
        (byte) 59,
        (byte) 133,
        (byte) 85,
        (byte) 148
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 132,
        (byte) 85,
        (byte) 248,
        (byte) 56,
        (byte) 186,
        (byte) 214,
        (byte) 215,
        (byte) 120,
        (byte) 111,
        (byte) 111,
        (byte) 169,
        (byte) 142,
        (byte) 149,
        (byte) 81,
        (byte) 177,
        (byte) 59,
        (byte) 93,
        (byte) 75,
        (byte) 211,
        (byte) 131,
        (byte) 28,
        (byte) 105,
        byte.MaxValue
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[15] = (byte) 150;
    numArray5[1] = (byte) 231;
    numArray5[2] = (byte) 12;
    numArray5[7] = (byte) 49;
    numArray5[4] = (byte) 56;
    numArray5[14] = (byte) 70;
    numArray5[0] = (byte) 67;
    numArray5[11] = (byte) 131;
    numArray5[17] = (byte) 188;
    numArray5[8] = (byte) 104;
    numArray5[10] = (byte) 251;
    numArray5[3] = (byte) 41;
    numArray5[9] = (byte) 193;
    numArray5[13] = (byte) 196;
    numArray5[21] = (byte) 100;
    numArray5[20] = (byte) 235;
    numArray5[16 /*0x10*/] = (byte) 237;
    numArray5[5] = (byte) 75;
    numArray5[18] = (byte) 95;
    numArray5[6] = (byte) 45;
    numArray5[19] = (byte) 221;
    numArray5[12] = (byte) 70;
    numArray5[22] = (byte) 15;
    byte[] numArray6 = new byte[23]
    {
      (byte) 137,
      (byte) 128 /*0x80*/,
      (byte) 46,
      (byte) 220,
      (byte) 232,
      (byte) 179,
      (byte) 66,
      (byte) 66,
      (byte) 175,
      (byte) 114,
      (byte) 54,
      (byte) 99,
      (byte) 185,
      (byte) 42,
      (byte) 64 /*0x40*/,
      (byte) 194,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 184,
      (byte) 97,
      (byte) 97,
      (byte) 129,
      (byte) 117
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_711()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 104,
        (byte) 216,
        (byte) 207,
        (byte) 20,
        (byte) 163,
        (byte) 222,
        (byte) 215,
        (byte) 58,
        (byte) 197,
        (byte) 107,
        (byte) 32 /*0x20*/,
        (byte) 148,
        (byte) 83,
        (byte) 149,
        (byte) 5,
        (byte) 21,
        (byte) 191,
        (byte) 163,
        (byte) 240 /*0xF0*/,
        (byte) 1,
        (byte) 111,
        (byte) 123,
        (byte) 71
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 165,
        (byte) 142,
        (byte) 81,
        (byte) 204,
        (byte) 171,
        (byte) 216,
        (byte) 44,
        (byte) 57,
        (byte) 49,
        (byte) 12,
        (byte) 127 /*0x7F*/,
        (byte) 10,
        (byte) 220,
        (byte) 228,
        (byte) 79,
        (byte) 239,
        (byte) 76,
        (byte) 248,
        (byte) 193,
        (byte) 108,
        (byte) 221,
        (byte) 16 /*0x10*/,
        (byte) 79
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 119,
      (byte) 221,
      (byte) 210,
      (byte) 71,
      (byte) 168,
      (byte) 228,
      (byte) 164,
      (byte) 240 /*0xF0*/,
      (byte) 106,
      (byte) 227,
      (byte) 22,
      (byte) 175,
      byte.MaxValue,
      (byte) 246,
      (byte) 109,
      (byte) 237,
      (byte) 172,
      (byte) 9,
      (byte) 181,
      (byte) 137,
      (byte) 153,
      (byte) 87,
      (byte) 44
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 66,
      (byte) 178,
      (byte) 144 /*0x90*/,
      (byte) 210,
      (byte) 201,
      (byte) 172,
      (byte) 182,
      (byte) 180,
      (byte) 234,
      (byte) 151,
      (byte) 132,
      (byte) 112 /*0x70*/,
      (byte) 249,
      (byte) 50,
      (byte) 21,
      (byte) 94,
      (byte) 59,
      (byte) 150,
      (byte) 146,
      (byte) 229,
      (byte) 165,
      (byte) 232,
      (byte) 114
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_712()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 243,
        (byte) 126,
        (byte) 236,
        (byte) 144 /*0x90*/,
        (byte) 70,
        (byte) 84,
        (byte) 165,
        (byte) 102,
        (byte) 249,
        (byte) 103,
        (byte) 125,
        (byte) 195,
        (byte) 60,
        (byte) 239,
        (byte) 99,
        (byte) 42,
        (byte) 34,
        byte.MaxValue,
        (byte) 16 /*0x10*/,
        (byte) 163,
        (byte) 249,
        (byte) 147,
        (byte) 130
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 163,
        (byte) 110,
        (byte) 239,
        (byte) 30,
        (byte) 39,
        (byte) 1,
        (byte) 154,
        (byte) 186,
        (byte) 248,
        (byte) 101,
        (byte) 157,
        (byte) 160 /*0xA0*/,
        (byte) 178,
        (byte) 99,
        (byte) 52,
        (byte) 72,
        (byte) 223,
        (byte) 237,
        (byte) 82,
        (byte) 213,
        (byte) 131,
        (byte) 72,
        (byte) 138
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[5] = (byte) 235;
    numArray5[1] = (byte) 100;
    numArray5[2] = (byte) 31 /*0x1F*/;
    numArray5[10] = (byte) 126;
    numArray5[12] = (byte) 208 /*0xD0*/;
    numArray5[16 /*0x10*/] = (byte) 116;
    numArray5[0] = (byte) 54;
    numArray5[7] = (byte) 140;
    numArray5[8] = (byte) 177;
    numArray5[11] = (byte) 82;
    numArray5[9] = (byte) 113;
    numArray5[13] = (byte) 90;
    numArray5[18] = (byte) 46;
    numArray5[3] = (byte) 176 /*0xB0*/;
    numArray5[14] = (byte) 90;
    numArray5[15] = (byte) 143;
    numArray5[17] = (byte) 133;
    numArray5[19] = (byte) 69;
    numArray5[4] = (byte) 233;
    numArray5[6] = (byte) 40;
    numArray5[20] = (byte) 121;
    numArray5[21] = (byte) 153;
    numArray5[22] = (byte) 54;
    byte[] numArray6 = new byte[23]
    {
      (byte) 40,
      (byte) 103,
      (byte) 55,
      (byte) 151,
      (byte) 163,
      (byte) 137,
      (byte) 105,
      (byte) 42,
      (byte) 180,
      (byte) 251,
      (byte) 107,
      (byte) 198,
      (byte) 95,
      (byte) 66,
      (byte) 203,
      (byte) 83,
      (byte) 202,
      (byte) 107,
      (byte) 147,
      (byte) 114,
      (byte) 155,
      (byte) 88,
      (byte) 125
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_713()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 31 /*0x1F*/,
        (byte) 252,
        (byte) 160 /*0xA0*/,
        (byte) 248,
        (byte) 90,
        (byte) 156,
        (byte) 45,
        (byte) 243,
        (byte) 166,
        byte.MaxValue,
        (byte) 159,
        (byte) 75,
        (byte) 47,
        (byte) 82,
        (byte) 116,
        (byte) 96 /*0x60*/,
        (byte) 0,
        (byte) 94,
        (byte) 59,
        (byte) 26,
        (byte) 72,
        (byte) 43,
        (byte) 7
      };
      byte[] numArray3 = new byte[23];
      numArray3[4] = (byte) 3;
      numArray3[22] = (byte) 224 /*0xE0*/;
      numArray3[1] = (byte) 55;
      numArray3[3] = (byte) 122;
      numArray3[21] = (byte) 23;
      numArray3[5] = (byte) 198;
      numArray3[6] = (byte) 199;
      numArray3[7] = (byte) 70;
      numArray3[9] = (byte) 234;
      numArray3[15] = (byte) 172;
      numArray3[8] = (byte) 74;
      numArray3[12] = (byte) 195;
      numArray3[0] = (byte) 72;
      numArray3[13] = (byte) 19;
      numArray3[14] = (byte) 8;
      numArray3[20] = (byte) 56;
      numArray3[10] = (byte) 117;
      numArray3[17] = (byte) 34;
      numArray3[18] = (byte) 35;
      numArray3[19] = (byte) 170;
      numArray3[16 /*0x10*/] = (byte) 244;
      numArray3[2] = (byte) 142;
      numArray3[11] = (byte) 233;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[15] = (byte) 68;
    numArray5[1] = (byte) 207;
    numArray5[0] = (byte) 75;
    numArray5[20] = (byte) 117;
    numArray5[4] = (byte) 46;
    numArray5[17] = (byte) 182;
    numArray5[6] = (byte) 244;
    numArray5[7] = (byte) 245;
    numArray5[3] = (byte) 122;
    numArray5[2] = (byte) 209;
    numArray5[21] = (byte) 227;
    numArray5[8] = (byte) 3;
    numArray5[12] = (byte) 232;
    numArray5[9] = (byte) 77;
    numArray5[19] = (byte) 18;
    numArray5[18] = (byte) 60;
    numArray5[10] = (byte) 145;
    numArray5[13] = (byte) 114;
    numArray5[5] = (byte) 34;
    numArray5[16 /*0x10*/] = (byte) 10;
    numArray5[11] = (byte) 64 /*0x40*/;
    numArray5[14] = (byte) 107;
    numArray5[22] = (byte) 166;
    byte[] numArray6 = new byte[23]
    {
      (byte) 223,
      (byte) 137,
      (byte) 245,
      (byte) 98,
      (byte) 157,
      (byte) 191,
      (byte) 126,
      (byte) 221,
      (byte) 140,
      (byte) 178,
      (byte) 81,
      (byte) 191,
      (byte) 21,
      (byte) 245,
      (byte) 60,
      (byte) 207,
      byte.MaxValue,
      (byte) 210,
      (byte) 4,
      (byte) 62,
      (byte) 224 /*0xE0*/,
      (byte) 244,
      (byte) 253
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_714()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 232,
        (byte) 152,
        (byte) 67,
        (byte) 217,
        (byte) 7,
        (byte) 104,
        (byte) 40,
        (byte) 138,
        (byte) 192 /*0xC0*/,
        (byte) 39,
        (byte) 46,
        (byte) 107,
        (byte) 109,
        (byte) 90,
        (byte) 242,
        (byte) 71,
        (byte) 209,
        (byte) 32 /*0x20*/,
        (byte) 61,
        byte.MaxValue,
        (byte) 91,
        (byte) 239,
        (byte) 81
      };
      byte[] numArray3 = new byte[23];
      numArray3[19] = (byte) 16 /*0x10*/;
      numArray3[22] = (byte) 195;
      numArray3[11] = (byte) 217;
      numArray3[18] = (byte) 44;
      numArray3[12] = (byte) 47;
      numArray3[5] = (byte) 80 /*0x50*/;
      numArray3[14] = (byte) 226;
      numArray3[7] = (byte) 78;
      numArray3[8] = (byte) 174;
      numArray3[9] = (byte) 125;
      numArray3[10] = (byte) 161;
      numArray3[16 /*0x10*/] = (byte) 98;
      numArray3[3] = (byte) 74;
      numArray3[0] = (byte) 187;
      numArray3[2] = (byte) 44;
      numArray3[15] = (byte) 45;
      numArray3[13] = (byte) 187;
      numArray3[17] = (byte) 239;
      numArray3[1] = (byte) 245;
      numArray3[6] = (byte) 33;
      numArray3[20] = (byte) 192 /*0xC0*/;
      numArray3[21] = (byte) 165;
      numArray3[4] = (byte) 174;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 61,
      (byte) 112 /*0x70*/,
      (byte) 10,
      (byte) 163,
      (byte) 54,
      (byte) 222,
      (byte) 249,
      (byte) 71,
      (byte) 213,
      (byte) 17,
      (byte) 156,
      (byte) 253,
      (byte) 142,
      (byte) 201,
      (byte) 72,
      (byte) 75,
      (byte) 27,
      (byte) 23,
      (byte) 30,
      (byte) 153,
      (byte) 48 /*0x30*/,
      (byte) 65,
      (byte) 58
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 244,
      (byte) 193,
      (byte) 75,
      (byte) 235,
      (byte) 155,
      (byte) 90,
      (byte) 189,
      (byte) 225,
      (byte) 192 /*0xC0*/,
      (byte) 102,
      (byte) 187,
      (byte) 66,
      (byte) 130,
      (byte) 29,
      (byte) 59,
      (byte) 98,
      (byte) 163,
      (byte) 204,
      (byte) 222,
      (byte) 176 /*0xB0*/,
      (byte) 184,
      (byte) 150,
      (byte) 117
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_715()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 149,
        (byte) 19,
        (byte) 204,
        (byte) 231,
        (byte) 207,
        (byte) 75,
        (byte) 128 /*0x80*/,
        (byte) 131,
        (byte) 7,
        (byte) 210,
        (byte) 111,
        (byte) 28,
        (byte) 111,
        (byte) 198,
        (byte) 122,
        (byte) 48 /*0x30*/,
        (byte) 133,
        (byte) 35,
        (byte) 207,
        (byte) 180,
        (byte) 150,
        (byte) 17,
        (byte) 242
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 147,
        (byte) 143,
        (byte) 28,
        (byte) 214,
        (byte) 242,
        (byte) 41,
        (byte) 35,
        (byte) 28,
        (byte) 54,
        (byte) 53,
        (byte) 78,
        (byte) 178,
        (byte) 215,
        (byte) 222,
        (byte) 62,
        (byte) 74,
        (byte) 18,
        (byte) 100,
        (byte) 176 /*0xB0*/,
        (byte) 83,
        (byte) 108,
        (byte) 5,
        (byte) 247
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 133,
      (byte) 233,
      (byte) 252,
      (byte) 53,
      (byte) 169,
      (byte) 50,
      (byte) 121,
      (byte) 227,
      (byte) 100,
      (byte) 52,
      (byte) 9,
      (byte) 41,
      (byte) 205,
      (byte) 177,
      (byte) 13,
      (byte) 69,
      (byte) 64 /*0x40*/,
      (byte) 121,
      (byte) 172,
      (byte) 250,
      (byte) 174,
      (byte) 210,
      (byte) 181
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 196,
      (byte) 159,
      (byte) 12,
      (byte) 231,
      (byte) 110,
      (byte) 245,
      (byte) 96 /*0x60*/,
      (byte) 49,
      (byte) 1,
      (byte) 234,
      (byte) 40,
      (byte) 68,
      (byte) 12,
      (byte) 236,
      (byte) 5,
      (byte) 160 /*0xA0*/,
      (byte) 224 /*0xE0*/,
      (byte) 48 /*0x30*/,
      (byte) 245,
      (byte) 154,
      (byte) 81,
      (byte) 78,
      (byte) 188
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_716()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 7,
        (byte) 101,
        (byte) 131,
        (byte) 32 /*0x20*/,
        (byte) 71,
        (byte) 31 /*0x1F*/,
        (byte) 81,
        (byte) 198,
        (byte) 151,
        (byte) 135,
        (byte) 210,
        (byte) 40,
        (byte) 61,
        (byte) 163,
        (byte) 140,
        (byte) 98,
        (byte) 69,
        (byte) 38,
        (byte) 91,
        (byte) 86,
        (byte) 29,
        (byte) 167,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 84,
        (byte) 34,
        (byte) 234,
        (byte) 154,
        (byte) 15,
        (byte) 120,
        (byte) 4,
        (byte) 212,
        (byte) 26,
        (byte) 143,
        (byte) 108,
        (byte) 97,
        (byte) 244,
        (byte) 195,
        (byte) 182,
        (byte) 225,
        (byte) 169,
        (byte) 63 /*0x3F*/,
        (byte) 178,
        (byte) 51,
        (byte) 39,
        (byte) 131,
        (byte) 228
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 205,
      (byte) 206,
      (byte) 178,
      (byte) 51,
      (byte) 234,
      (byte) 127 /*0x7F*/,
      (byte) 108,
      (byte) 144 /*0x90*/,
      (byte) 113,
      (byte) 126,
      (byte) 224 /*0xE0*/,
      (byte) 19,
      (byte) 194,
      (byte) 9,
      (byte) 123,
      (byte) 235,
      (byte) 118,
      (byte) 190,
      (byte) 110,
      (byte) 81,
      (byte) 111,
      (byte) 87,
      (byte) 34
    };
    byte[] numArray6 = new byte[23];
    numArray6[14] = (byte) 225;
    numArray6[1] = (byte) 158;
    numArray6[2] = (byte) 208 /*0xD0*/;
    numArray6[3] = (byte) 207;
    numArray6[11] = (byte) 52;
    numArray6[5] = (byte) 128 /*0x80*/;
    numArray6[0] = (byte) 127 /*0x7F*/;
    numArray6[9] = (byte) 83;
    numArray6[13] = (byte) 115;
    numArray6[12] = (byte) 175;
    numArray6[7] = (byte) 217;
    numArray6[8] = (byte) 54;
    numArray6[20] = (byte) 166;
    numArray6[17] = (byte) 134;
    numArray6[4] = (byte) 94;
    numArray6[15] = (byte) 204;
    numArray6[16 /*0x10*/] = (byte) 83;
    numArray6[22] = (byte) 89;
    numArray6[18] = (byte) 54;
    numArray6[21] = (byte) 218;
    numArray6[10] = (byte) 181;
    numArray6[6] = (byte) 128 /*0x80*/;
    numArray6[19] = (byte) 139;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_708.sspq, 0, (Array) numArray7, 0, 10);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_708.sspr, 0, (Array) numArray7, 0, 10);
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

  internal static string ssp_automatch_717()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 134,
        (byte) 11,
        (byte) 17,
        (byte) 105,
        (byte) 45,
        (byte) 107,
        (byte) 84,
        (byte) 183,
        (byte) 249,
        (byte) 215,
        (byte) 19,
        (byte) 34,
        (byte) 27,
        (byte) 149,
        (byte) 194,
        (byte) 38,
        (byte) 19,
        (byte) 238,
        (byte) 61,
        (byte) 133,
        (byte) 7,
        (byte) 11,
        (byte) 82
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 156,
        (byte) 13,
        (byte) 184,
        (byte) 34,
        (byte) 57,
        (byte) 150,
        (byte) 90,
        (byte) 175,
        (byte) 134,
        (byte) 52,
        (byte) 144 /*0x90*/,
        (byte) 66,
        (byte) 167,
        (byte) 159,
        (byte) 61,
        (byte) 247,
        (byte) 142,
        (byte) 82,
        (byte) 72,
        (byte) 50,
        (byte) 37,
        (byte) 142,
        (byte) 152
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 128 /*0x80*/,
      (byte) 5,
      (byte) 221,
      (byte) 213,
      (byte) 209,
      (byte) 159,
      (byte) 155,
      (byte) 44,
      (byte) 53,
      (byte) 161,
      (byte) 32 /*0x20*/,
      (byte) 164,
      (byte) 84,
      (byte) 198,
      (byte) 16 /*0x10*/,
      (byte) 154,
      (byte) 225,
      (byte) 90,
      (byte) 29,
      (byte) 111,
      (byte) 70,
      (byte) 146,
      (byte) 166
    };
    byte[] numArray6 = new byte[23];
    numArray6[0] = (byte) 215;
    numArray6[15] = (byte) 165;
    numArray6[10] = (byte) 242;
    numArray6[3] = (byte) 149;
    numArray6[5] = (byte) 56;
    numArray6[20] = (byte) 109;
    numArray6[19] = (byte) 140;
    numArray6[7] = (byte) 81;
    numArray6[8] = (byte) 88;
    numArray6[6] = (byte) 141;
    numArray6[9] = (byte) 208 /*0xD0*/;
    numArray6[11] = (byte) 80 /*0x50*/;
    numArray6[12] = (byte) 102;
    numArray6[2] = (byte) 85;
    numArray6[16 /*0x10*/] = (byte) 223;
    numArray6[13] = (byte) 24;
    numArray6[1] = (byte) 186;
    numArray6[17] = (byte) 168;
    numArray6[22] = (byte) 61;
    numArray6[14] = (byte) 81;
    numArray6[18] = (byte) 34;
    numArray6[21] = (byte) 142;
    numArray6[4] = (byte) 30;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_718()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 41,
        (byte) 248,
        (byte) 19,
        (byte) 18,
        (byte) 11,
        (byte) 238,
        (byte) 94,
        (byte) 141,
        (byte) 61,
        (byte) 126,
        (byte) 25,
        (byte) 32 /*0x20*/,
        (byte) 14,
        (byte) 185,
        (byte) 232,
        (byte) 63 /*0x3F*/,
        (byte) 189,
        (byte) 120,
        (byte) 220,
        (byte) 193,
        (byte) 192 /*0xC0*/,
        (byte) 190,
        (byte) 84
      };
      byte[] numArray3 = new byte[23];
      numArray3[4] = (byte) 32 /*0x20*/;
      numArray3[0] = (byte) 113;
      numArray3[18] = (byte) 28;
      numArray3[3] = (byte) 89;
      numArray3[13] = (byte) 165;
      numArray3[10] = (byte) 201;
      numArray3[6] = (byte) 241;
      numArray3[20] = (byte) 190;
      numArray3[5] = (byte) 195;
      numArray3[9] = (byte) 245;
      numArray3[14] = (byte) 195;
      numArray3[1] = (byte) 196;
      numArray3[12] = (byte) 155;
      numArray3[2] = (byte) 100;
      numArray3[22] = (byte) 13;
      numArray3[15] = (byte) 137;
      numArray3[16 /*0x10*/] = (byte) 141;
      numArray3[17] = (byte) 203;
      numArray3[8] = (byte) 9;
      numArray3[19] = (byte) 26;
      numArray3[11] = (byte) 136;
      numArray3[21] = (byte) 165;
      numArray3[7] = (byte) 4;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 199,
      (byte) 177,
      (byte) 34,
      (byte) 155,
      (byte) 180,
      (byte) 172,
      (byte) 107,
      (byte) 163,
      (byte) 105,
      (byte) 41,
      (byte) 179,
      (byte) 188,
      (byte) 217,
      (byte) 68,
      (byte) 19,
      (byte) 197,
      (byte) 11,
      (byte) 27,
      (byte) 93,
      (byte) 103,
      (byte) 40,
      (byte) 113,
      (byte) 232
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 171,
      (byte) 116,
      (byte) 35,
      (byte) 17,
      (byte) 50,
      (byte) 145,
      (byte) 75,
      (byte) 85,
      (byte) 116,
      (byte) 215,
      (byte) 10,
      (byte) 129,
      (byte) 156,
      (byte) 29,
      (byte) 150,
      (byte) 156,
      (byte) 171,
      (byte) 228,
      (byte) 10,
      (byte) 183,
      (byte) 157,
      (byte) 167,
      (byte) 154
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
