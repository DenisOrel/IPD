// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_703
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_703
{
  private static byte[] sspq = new byte[16 /*0x10*/]
  {
    (byte) 35,
    (byte) 216,
    (byte) 55,
    (byte) 37,
    (byte) 47,
    (byte) 131,
    (byte) 211,
    (byte) 145,
    (byte) 84,
    (byte) 157,
    (byte) 243,
    (byte) 176 /*0xB0*/,
    (byte) 8,
    (byte) 136,
    (byte) 162,
    (byte) 132
  };
  private static byte[] sspr = new byte[16 /*0x10*/]
  {
    (byte) 94,
    (byte) 109,
    (byte) 229,
    (byte) 149,
    (byte) 233,
    (byte) 102,
    (byte) 238,
    (byte) 43,
    (byte) 248,
    (byte) 249,
    (byte) 54,
    (byte) 213,
    (byte) 187,
    (byte) 52,
    (byte) 152,
    (byte) 41
  };

  internal static string ssp_automatch_704()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 225,
        (byte) 144 /*0x90*/,
        (byte) 216,
        (byte) 102,
        (byte) 244,
        (byte) 222,
        (byte) 189,
        (byte) 215,
        (byte) 156,
        (byte) 207,
        (byte) 35,
        (byte) 183,
        (byte) 201,
        (byte) 33,
        (byte) 200,
        (byte) 55,
        (byte) 28,
        (byte) 85,
        (byte) 11,
        (byte) 140,
        (byte) 47,
        (byte) 33,
        (byte) 135
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 221,
        (byte) 7,
        (byte) 124,
        (byte) 96 /*0x60*/,
        (byte) 1,
        (byte) 181,
        (byte) 203,
        (byte) 103,
        (byte) 54,
        (byte) 183,
        (byte) 6,
        (byte) 22,
        (byte) 254,
        (byte) 198,
        (byte) 218,
        (byte) 147,
        (byte) 254,
        (byte) 97,
        (byte) 51,
        (byte) 136,
        (byte) 88,
        (byte) 179,
        (byte) 213
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
      (byte) 207,
      (byte) 35,
      (byte) 207,
      (byte) 234,
      (byte) 235,
      (byte) 181,
      (byte) 141,
      (byte) 226,
      (byte) 114,
      (byte) 30,
      (byte) 160 /*0xA0*/,
      (byte) 249,
      (byte) 124,
      (byte) 136,
      (byte) 202,
      (byte) 180,
      (byte) 163,
      (byte) 139,
      (byte) 193,
      (byte) 54,
      (byte) 248,
      (byte) 91,
      (byte) 149
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 56,
      (byte) 46,
      (byte) 217,
      (byte) 233,
      (byte) 53,
      (byte) 181,
      (byte) 4,
      (byte) 167,
      (byte) 162,
      (byte) 150,
      (byte) 242,
      (byte) 225,
      (byte) 19,
      (byte) 12,
      (byte) 7,
      (byte) 230,
      (byte) 187,
      (byte) 137,
      (byte) 220,
      (byte) 17,
      (byte) 100,
      (byte) 249,
      (byte) 157
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_705()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[3] = (byte) 8;
      numArray2[1] = (byte) 73;
      numArray2[2] = (byte) 192 /*0xC0*/;
      numArray2[4] = (byte) 208 /*0xD0*/;
      numArray2[20] = (byte) 130;
      numArray2[7] = (byte) 15;
      numArray2[6] = (byte) 21;
      numArray2[9] = (byte) 8;
      numArray2[5] = (byte) 53;
      numArray2[17] = (byte) 151;
      numArray2[11] = (byte) 227;
      numArray2[22] = (byte) 123;
      numArray2[12] = (byte) 109;
      numArray2[8] = (byte) 154;
      numArray2[14] = (byte) 231;
      numArray2[15] = (byte) 175;
      numArray2[16 /*0x10*/] = (byte) 170;
      numArray2[0] = (byte) 98;
      numArray2[18] = (byte) 35;
      numArray2[19] = (byte) 217;
      numArray2[13] = (byte) 41;
      numArray2[21] = (byte) 193;
      numArray2[10] = (byte) 178;
      byte[] numArray3 = new byte[23]
      {
        (byte) 66,
        (byte) 20,
        (byte) 102,
        (byte) 154,
        (byte) 196,
        (byte) 246,
        (byte) 32 /*0x20*/,
        (byte) 234,
        (byte) 238,
        (byte) 135,
        (byte) 235,
        (byte) 69,
        (byte) 60,
        (byte) 169,
        (byte) 250,
        (byte) 148,
        (byte) 198,
        (byte) 166,
        (byte) 82,
        (byte) 146,
        (byte) 45,
        (byte) 153,
        (byte) 237
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
      (byte) 171,
      (byte) 235,
      (byte) 120,
      (byte) 158,
      (byte) 21,
      (byte) 147,
      (byte) 160 /*0xA0*/,
      (byte) 206,
      (byte) 66,
      (byte) 12,
      (byte) 137,
      (byte) 58,
      (byte) 47,
      (byte) 154,
      (byte) 12,
      (byte) 72,
      (byte) 137,
      (byte) 230,
      (byte) 205,
      (byte) 254,
      (byte) 78,
      (byte) 253,
      (byte) 147
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 48 /*0x30*/,
      (byte) 138,
      (byte) 61,
      (byte) 146,
      (byte) 57,
      (byte) 49,
      (byte) 96 /*0x60*/,
      (byte) 174,
      (byte) 112 /*0x70*/,
      (byte) 18,
      (byte) 246,
      (byte) 240 /*0xF0*/,
      (byte) 29,
      (byte) 165,
      (byte) 167,
      (byte) 201,
      (byte) 100,
      (byte) 55,
      (byte) 94,
      (byte) 46,
      (byte) 184,
      (byte) 168,
      (byte) 227
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_706()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[13] = (byte) 138;
      numArray2[1] = (byte) 207;
      numArray2[2] = (byte) 140;
      numArray2[0] = (byte) 145;
      numArray2[4] = (byte) 242;
      numArray2[19] = (byte) 54;
      numArray2[6] = (byte) 212;
      numArray2[9] = (byte) 70;
      numArray2[5] = (byte) 238;
      numArray2[11] = (byte) 232;
      numArray2[10] = (byte) 137;
      numArray2[15] = (byte) 110;
      numArray2[12] = (byte) 12;
      numArray2[3] = (byte) 84;
      numArray2[14] = (byte) 202;
      numArray2[8] = (byte) 162;
      numArray2[16 /*0x10*/] = (byte) 10;
      numArray2[18] = (byte) 137;
      numArray2[20] = (byte) 222;
      numArray2[7] = (byte) 200;
      numArray2[21] = (byte) 36;
      numArray2[17] = (byte) 70;
      numArray2[22] = (byte) 99;
      byte[] numArray3 = new byte[23];
      numArray3[5] = (byte) 28;
      numArray3[1] = (byte) 238;
      numArray3[6] = (byte) 143;
      numArray3[14] = (byte) 61;
      numArray3[13] = (byte) 217;
      numArray3[20] = (byte) 76;
      numArray3[3] = (byte) 10;
      numArray3[7] = (byte) 75;
      numArray3[0] = (byte) 163;
      numArray3[9] = (byte) 117;
      numArray3[2] = (byte) 216;
      numArray3[22] = (byte) 116;
      numArray3[11] = (byte) 207;
      numArray3[18] = (byte) 54;
      numArray3[12] = (byte) 83;
      numArray3[15] = (byte) 247;
      numArray3[16 /*0x10*/] = (byte) 128 /*0x80*/;
      numArray3[17] = (byte) 208 /*0xD0*/;
      numArray3[10] = (byte) 52;
      numArray3[19] = (byte) 120;
      numArray3[8] = (byte) 86;
      numArray3[21] = (byte) 206;
      numArray3[4] = (byte) 87;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[11] = (byte) 31 /*0x1F*/;
    numArray5[8] = (byte) 198;
    numArray5[2] = (byte) 219;
    numArray5[19] = (byte) 146;
    numArray5[4] = (byte) 186;
    numArray5[5] = (byte) 97;
    numArray5[6] = (byte) 103;
    numArray5[20] = (byte) 35;
    numArray5[12] = (byte) 205;
    numArray5[16 /*0x10*/] = (byte) 95;
    numArray5[10] = (byte) 62;
    numArray5[22] = (byte) 106;
    numArray5[9] = (byte) 154;
    numArray5[3] = (byte) 12;
    numArray5[14] = (byte) 106;
    numArray5[1] = (byte) 62;
    numArray5[15] = (byte) 103;
    numArray5[17] = (byte) 162;
    numArray5[13] = (byte) 48 /*0x30*/;
    numArray5[7] = (byte) 194;
    numArray5[18] = (byte) 147;
    numArray5[21] = (byte) 43;
    numArray5[0] = (byte) 175;
    byte[] numArray6 = new byte[23]
    {
      (byte) 57,
      (byte) 91,
      (byte) 91,
      (byte) 249,
      (byte) 188,
      (byte) 227,
      (byte) 220,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 9,
      (byte) 95,
      (byte) 119,
      (byte) 94,
      (byte) 154,
      (byte) 195,
      (byte) 97,
      (byte) 41,
      (byte) 10,
      (byte) 10,
      (byte) 217,
      (byte) 24,
      (byte) 53,
      (byte) 113
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[16 /*0x10*/];
    byte[] response = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_703.sspq, 0, (Array) numArray7, 0, 16 /*0x10*/);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_703.sspr, 0, (Array) numArray7, 0, 16 /*0x10*/);
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
