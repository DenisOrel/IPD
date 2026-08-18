// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_631
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_631
{
  private static byte[] sspq = new byte[68]
  {
    (byte) 198,
    (byte) 106,
    (byte) 244,
    (byte) 200,
    (byte) 192 /*0xC0*/,
    (byte) 105,
    (byte) 114,
    (byte) 221,
    (byte) 182,
    (byte) 199,
    (byte) 40,
    (byte) 218,
    (byte) 141,
    (byte) 49,
    (byte) 105,
    (byte) 65,
    (byte) 181,
    (byte) 3,
    (byte) 223,
    (byte) 20,
    (byte) 73,
    (byte) 206,
    (byte) 143,
    (byte) 215,
    (byte) 212,
    (byte) 73,
    (byte) 159,
    (byte) 11,
    (byte) 114,
    (byte) 105,
    (byte) 243,
    (byte) 118,
    (byte) 201,
    (byte) 103,
    (byte) 107,
    (byte) 76,
    (byte) 235,
    (byte) 12,
    (byte) 165,
    (byte) 16 /*0x10*/,
    (byte) 170,
    (byte) 168,
    (byte) 135,
    (byte) 197,
    (byte) 124,
    (byte) 194,
    (byte) 147,
    (byte) 253,
    (byte) 220,
    (byte) 198,
    (byte) 168,
    (byte) 135,
    (byte) 14,
    (byte) 246,
    (byte) 116,
    (byte) 39,
    (byte) 68,
    (byte) 52,
    (byte) 227,
    (byte) 210,
    (byte) 162,
    (byte) 142,
    (byte) 230,
    (byte) 156,
    (byte) 252,
    (byte) 16 /*0x10*/,
    (byte) 83,
    (byte) 25
  };
  private static byte[] sspr = new byte[68]
  {
    (byte) 226,
    (byte) 210,
    (byte) 205,
    (byte) 136,
    (byte) 52,
    (byte) 113,
    (byte) 223,
    (byte) 150,
    (byte) 106,
    (byte) 108,
    (byte) 143,
    (byte) 213,
    (byte) 142,
    (byte) 233,
    (byte) 19,
    (byte) 150,
    (byte) 154,
    (byte) 7,
    (byte) 100,
    (byte) 21,
    (byte) 49,
    (byte) 169,
    (byte) 226,
    (byte) 25,
    (byte) 124,
    (byte) 201,
    (byte) 42,
    (byte) 148,
    (byte) 125,
    (byte) 21,
    (byte) 35,
    (byte) 36,
    (byte) 155,
    (byte) 6,
    (byte) 201,
    (byte) 36,
    (byte) 237,
    (byte) 24,
    (byte) 110,
    (byte) 199,
    (byte) 22,
    (byte) 44,
    (byte) 235,
    (byte) 22,
    (byte) 28,
    (byte) 108,
    (byte) 5,
    (byte) 87,
    (byte) 165,
    (byte) 178,
    (byte) 22,
    (byte) 54,
    (byte) 14,
    (byte) 252,
    (byte) 102,
    (byte) 72,
    (byte) 220,
    (byte) 150,
    (byte) 79,
    (byte) 48 /*0x30*/,
    (byte) 63 /*0x3F*/,
    (byte) 121,
    (byte) 9,
    (byte) 235,
    (byte) 149,
    (byte) 181,
    (byte) 12,
    (byte) 209
  };

  internal static string ssp_automatch_632()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 137,
        (byte) 180,
        (byte) 210,
        (byte) 126,
        (byte) 27,
        (byte) 105,
        (byte) 114,
        (byte) 3,
        (byte) 164,
        (byte) 220,
        (byte) 180,
        (byte) 175,
        (byte) 248,
        (byte) 88,
        (byte) 149,
        (byte) 123,
        (byte) 73,
        (byte) 245,
        (byte) 126,
        (byte) 128 /*0x80*/,
        (byte) 150,
        (byte) 60,
        (byte) 137
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 143,
        (byte) 98,
        (byte) 240 /*0xF0*/,
        (byte) 166,
        (byte) 5,
        (byte) 47,
        (byte) 100,
        (byte) 9,
        (byte) 181,
        (byte) 239,
        (byte) 192 /*0xC0*/,
        (byte) 169,
        (byte) 207,
        (byte) 172,
        (byte) 128 /*0x80*/,
        (byte) 198,
        (byte) 239,
        (byte) 116,
        (byte) 244,
        (byte) 121,
        (byte) 12,
        (byte) 76,
        (byte) 142
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[15] = (byte) 114;
    numArray5[0] = (byte) 227;
    numArray5[2] = (byte) 219;
    numArray5[20] = (byte) 18;
    numArray5[11] = (byte) 110;
    numArray5[6] = (byte) 136;
    numArray5[19] = (byte) 252;
    numArray5[7] = (byte) 47;
    numArray5[8] = (byte) 125;
    numArray5[13] = (byte) 102;
    numArray5[10] = (byte) 239;
    numArray5[4] = (byte) 76;
    numArray5[1] = (byte) 126;
    numArray5[17] = (byte) 31 /*0x1F*/;
    numArray5[14] = (byte) 9;
    numArray5[22] = (byte) 11;
    numArray5[16 /*0x10*/] = (byte) 118;
    numArray5[5] = (byte) 92;
    numArray5[3] = (byte) 251;
    numArray5[12] = (byte) 75;
    numArray5[18] = (byte) 47;
    numArray5[21] = (byte) 121;
    numArray5[9] = (byte) 149;
    byte[] numArray6 = new byte[23]
    {
      (byte) 180,
      (byte) 120,
      (byte) 66,
      (byte) 167,
      (byte) 68,
      (byte) 60,
      (byte) 25,
      (byte) 95,
      (byte) 37,
      (byte) 225,
      (byte) 136,
      (byte) 139,
      (byte) 207,
      (byte) 79,
      (byte) 79,
      (byte) 190,
      (byte) 158,
      (byte) 178,
      (byte) 75,
      (byte) 247,
      (byte) 183,
      (byte) 237,
      (byte) 198
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[30];
    byte[] response = new byte[30];
    Array.Copy((Array) sc_631.sspq, 0, (Array) numArray7, 0, 30);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_631.sspr, 0, (Array) numArray7, 0, 30);
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

  internal static string ssp_automatch_633()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 27,
        (byte) 53,
        (byte) 125,
        (byte) 134,
        (byte) 193,
        (byte) 185,
        (byte) 240 /*0xF0*/,
        (byte) 135,
        (byte) 29,
        (byte) 129,
        (byte) 75,
        (byte) 241,
        (byte) 200,
        (byte) 47,
        (byte) 76,
        (byte) 59,
        (byte) 169,
        (byte) 150,
        (byte) 74,
        (byte) 37,
        (byte) 166,
        (byte) 56,
        (byte) 99
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 46,
        (byte) 234,
        (byte) 139,
        (byte) 101,
        (byte) 155,
        byte.MaxValue,
        (byte) 66,
        (byte) 177,
        (byte) 45,
        (byte) 98,
        (byte) 155,
        (byte) 138,
        (byte) 125,
        (byte) 48 /*0x30*/,
        (byte) 119,
        (byte) 100,
        (byte) 1,
        (byte) 6,
        (byte) 37,
        (byte) 193,
        (byte) 98,
        (byte) 212,
        (byte) 134
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_631.sspq, 30, (Array) numArray4, 0, 38);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_631.sspr, 30, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23]
    {
      (byte) 224 /*0xE0*/,
      (byte) 164,
      (byte) 221,
      (byte) 34,
      (byte) 167,
      (byte) 43,
      (byte) 254,
      (byte) 238,
      (byte) 194,
      (byte) 96 /*0x60*/,
      (byte) 42,
      (byte) 117,
      (byte) 207,
      (byte) 221,
      (byte) 27,
      (byte) 20,
      (byte) 117,
      (byte) 108,
      (byte) 103,
      (byte) 211,
      (byte) 243,
      (byte) 67,
      (byte) 8
    };
    byte[] numArray7 = new byte[23];
    numArray7[0] = (byte) 3;
    numArray7[11] = (byte) 23;
    numArray7[2] = (byte) 189;
    numArray7[1] = (byte) 36;
    numArray7[7] = (byte) 253;
    numArray7[3] = (byte) 129;
    numArray7[6] = (byte) 115;
    numArray7[18] = (byte) 137;
    numArray7[4] = (byte) 131;
    numArray7[14] = (byte) 226;
    numArray7[10] = byte.MaxValue;
    numArray7[13] = (byte) 178;
    numArray7[9] = (byte) 188;
    numArray7[12] = (byte) 3;
    numArray7[8] = (byte) 99;
    numArray7[5] = (byte) 93;
    numArray7[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    numArray7[15] = (byte) 174;
    numArray7[17] = (byte) 12;
    numArray7[19] = (byte) 45;
    numArray7[20] = (byte) 146;
    numArray7[21] = (byte) 64 /*0x40*/;
    numArray7[22] = (byte) 131;
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_automatch_634()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 38,
        (byte) 144 /*0x90*/,
        (byte) 168,
        (byte) 127 /*0x7F*/,
        (byte) 38,
        (byte) 157,
        (byte) 155,
        (byte) 37,
        (byte) 248,
        (byte) 211,
        (byte) 189,
        (byte) 226,
        (byte) 44,
        (byte) 215,
        (byte) 151,
        (byte) 190,
        (byte) 6,
        (byte) 85,
        (byte) 191,
        (byte) 227,
        (byte) 43,
        (byte) 239,
        (byte) 139
      };
      byte[] numArray3 = new byte[23];
      numArray3[18] = byte.MaxValue;
      numArray3[1] = (byte) 148;
      numArray3[8] = (byte) 112 /*0x70*/;
      numArray3[20] = (byte) 250;
      numArray3[9] = (byte) 45;
      numArray3[5] = (byte) 253;
      numArray3[21] = (byte) 188;
      numArray3[7] = byte.MaxValue;
      numArray3[0] = (byte) 131;
      numArray3[15] = (byte) 124;
      numArray3[10] = (byte) 74;
      numArray3[11] = (byte) 154;
      numArray3[12] = (byte) 30;
      numArray3[14] = (byte) 97;
      numArray3[16 /*0x10*/] = (byte) 6;
      numArray3[2] = (byte) 191;
      numArray3[3] = (byte) 245;
      numArray3[4] = (byte) 179;
      numArray3[13] = (byte) 6;
      numArray3[17] = (byte) 232;
      numArray3[6] = (byte) 242;
      numArray3[19] = (byte) 213;
      numArray3[22] = (byte) 234;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[14] = (byte) 60;
    numArray5[3] = (byte) 6;
    numArray5[2] = (byte) 172;
    numArray5[5] = (byte) 166;
    numArray5[4] = (byte) 143;
    numArray5[15] = (byte) 2;
    numArray5[6] = (byte) 55;
    numArray5[1] = (byte) 102;
    numArray5[9] = (byte) 164;
    numArray5[8] = (byte) 36;
    numArray5[10] = (byte) 185;
    numArray5[11] = (byte) 191;
    numArray5[12] = (byte) 119;
    numArray5[7] = (byte) 4;
    numArray5[18] = (byte) 61;
    numArray5[17] = (byte) 56;
    numArray5[16 /*0x10*/] = (byte) 21;
    numArray5[0] = (byte) 76;
    numArray5[19] = (byte) 18;
    numArray5[13] = (byte) 121;
    numArray5[20] = (byte) 94;
    numArray5[21] = (byte) 250;
    numArray5[22] = (byte) 23;
    byte[] numArray6 = new byte[23];
    numArray6[0] = (byte) 115;
    numArray6[22] = (byte) 56;
    numArray6[2] = (byte) 1;
    numArray6[3] = (byte) 16 /*0x10*/;
    numArray6[4] = (byte) 173;
    numArray6[11] = (byte) 41;
    numArray6[1] = (byte) 0;
    numArray6[5] = (byte) 65;
    numArray6[8] = (byte) 165;
    numArray6[9] = (byte) 189;
    numArray6[18] = (byte) 168;
    numArray6[19] = (byte) 174;
    numArray6[12] = (byte) 66;
    numArray6[13] = (byte) 68;
    numArray6[7] = (byte) 23;
    numArray6[15] = (byte) 87;
    numArray6[16 /*0x10*/] = (byte) 89;
    numArray6[17] = (byte) 94;
    numArray6[6] = (byte) 118;
    numArray6[20] = (byte) 3;
    numArray6[21] = (byte) 136;
    numArray6[10] = (byte) 7;
    numArray6[14] = (byte) 156;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
