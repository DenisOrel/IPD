// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_697
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_697
{
  private static byte[] sspq = new byte[32 /*0x20*/]
  {
    (byte) 190,
    (byte) 142,
    (byte) 79,
    (byte) 62,
    (byte) 209,
    (byte) 9,
    (byte) 148,
    (byte) 234,
    (byte) 52,
    (byte) 130,
    (byte) 158,
    (byte) 55,
    (byte) 246,
    (byte) 199,
    (byte) 105,
    (byte) 187,
    (byte) 212,
    (byte) 61,
    (byte) 226,
    (byte) 184,
    (byte) 232,
    (byte) 230,
    (byte) 245,
    (byte) 8,
    (byte) 145,
    (byte) 30,
    (byte) 129,
    (byte) 209,
    (byte) 141,
    (byte) 163,
    (byte) 226,
    (byte) 2
  };
  private static byte[] sspr = new byte[32 /*0x20*/]
  {
    (byte) 30,
    (byte) 111,
    (byte) 35,
    (byte) 17,
    (byte) 196,
    (byte) 235,
    (byte) 230,
    (byte) 65,
    (byte) 216,
    (byte) 10,
    (byte) 15,
    (byte) 56,
    (byte) 166,
    (byte) 97,
    (byte) 113,
    (byte) 249,
    (byte) 193,
    (byte) 73,
    (byte) 141,
    (byte) 53,
    (byte) 129,
    (byte) 28,
    (byte) 128 /*0x80*/,
    (byte) 202,
    (byte) 150,
    (byte) 129,
    (byte) 3,
    (byte) 153,
    (byte) 75,
    (byte) 69,
    (byte) 117,
    (byte) 132
  };

  internal static string ssp_automatch_698()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[14] = (byte) 237;
      numArray2[2] = (byte) 221;
      numArray2[19] = (byte) 23;
      numArray2[3] = (byte) 181;
      numArray2[10] = (byte) 141;
      numArray2[11] = (byte) 61;
      numArray2[6] = (byte) 120;
      numArray2[17] = (byte) 53;
      numArray2[8] = (byte) 125;
      numArray2[9] = (byte) 112 /*0x70*/;
      numArray2[13] = (byte) 125;
      numArray2[20] = (byte) 209;
      numArray2[12] = (byte) 2;
      numArray2[21] = (byte) 213;
      numArray2[7] = (byte) 191;
      numArray2[15] = (byte) 11;
      numArray2[4] = (byte) 161;
      numArray2[16 /*0x10*/] = (byte) 173;
      numArray2[0] = (byte) 77;
      numArray2[5] = (byte) 167;
      numArray2[18] = (byte) 251;
      numArray2[1] = (byte) 196;
      numArray2[22] = (byte) 71;
      byte[] numArray3 = new byte[23];
      numArray3[3] = (byte) 170;
      numArray3[16 /*0x10*/] = (byte) 243;
      numArray3[2] = (byte) 244;
      numArray3[6] = (byte) 202;
      numArray3[4] = (byte) 42;
      numArray3[5] = (byte) 237;
      numArray3[0] = (byte) 34;
      numArray3[9] = (byte) 152;
      numArray3[13] = (byte) 165;
      numArray3[1] = (byte) 26;
      numArray3[18] = (byte) 46;
      numArray3[11] = (byte) 141;
      numArray3[12] = (byte) 246;
      numArray3[17] = (byte) 68;
      numArray3[14] = (byte) 81;
      numArray3[15] = (byte) 69;
      numArray3[7] = (byte) 200;
      numArray3[21] = (byte) 155;
      numArray3[10] = (byte) 161;
      numArray3[8] = (byte) 57;
      numArray3[20] = (byte) 249;
      numArray3[22] = (byte) 107;
      numArray3[19] = (byte) 181;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[18] = (byte) 144 /*0x90*/;
    numArray5[0] = (byte) 10;
    numArray5[6] = (byte) 141;
    numArray5[5] = (byte) 245;
    numArray5[4] = (byte) 177;
    numArray5[2] = (byte) 84;
    numArray5[12] = (byte) 168;
    numArray5[16 /*0x10*/] = (byte) 42;
    numArray5[13] = (byte) 166;
    numArray5[9] = (byte) 241;
    numArray5[10] = (byte) 124;
    numArray5[17] = (byte) 187;
    numArray5[11] = (byte) 219;
    numArray5[21] = (byte) 1;
    numArray5[14] = (byte) 241;
    numArray5[15] = (byte) 231;
    numArray5[8] = (byte) 171;
    numArray5[7] = (byte) 11;
    numArray5[1] = (byte) 86;
    numArray5[19] = (byte) 240 /*0xF0*/;
    numArray5[20] = (byte) 164;
    numArray5[3] = (byte) 171;
    numArray5[22] = (byte) 193;
    byte[] numArray6 = new byte[23]
    {
      (byte) 118,
      (byte) 140,
      (byte) 238,
      (byte) 212,
      (byte) 14,
      (byte) 185,
      (byte) 5,
      (byte) 110,
      (byte) 75,
      (byte) 225,
      (byte) 4,
      (byte) 198,
      (byte) 201,
      (byte) 214,
      (byte) 77,
      (byte) 216,
      (byte) 154,
      (byte) 81,
      (byte) 176 /*0xB0*/,
      (byte) 88,
      (byte) 254,
      (byte) 29,
      (byte) 225
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[32 /*0x20*/];
    byte[] response = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_697.sspq, 0, (Array) numArray7, 0, 32 /*0x20*/);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_697.sspr, 0, (Array) numArray7, 0, 32 /*0x20*/);
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

  internal static string ssp_automatch_699()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 251,
        (byte) 37,
        (byte) 249,
        (byte) 114,
        (byte) 199,
        (byte) 152,
        (byte) 187,
        (byte) 163,
        (byte) 20,
        (byte) 15,
        (byte) 68,
        (byte) 233,
        (byte) 39,
        (byte) 144 /*0x90*/,
        (byte) 183,
        (byte) 135,
        (byte) 33,
        (byte) 254,
        (byte) 15,
        (byte) 113,
        (byte) 100,
        (byte) 50,
        (byte) 215
      };
      byte[] numArray3 = new byte[23];
      numArray3[8] = (byte) 133;
      numArray3[1] = (byte) 205;
      numArray3[21] = (byte) 173;
      numArray3[22] = (byte) 74;
      numArray3[4] = (byte) 48 /*0x30*/;
      numArray3[6] = (byte) 83;
      numArray3[12] = (byte) 231;
      numArray3[7] = (byte) 112 /*0x70*/;
      numArray3[15] = (byte) 229;
      numArray3[11] = (byte) 141;
      numArray3[2] = (byte) 190;
      numArray3[3] = (byte) 252;
      numArray3[10] = (byte) 106;
      numArray3[13] = (byte) 11;
      numArray3[14] = (byte) 79;
      numArray3[18] = (byte) 160 /*0xA0*/;
      numArray3[16 /*0x10*/] = (byte) 219;
      numArray3[17] = (byte) 28;
      numArray3[0] = (byte) 251;
      numArray3[19] = (byte) 54;
      numArray3[20] = (byte) 207;
      numArray3[5] = (byte) 20;
      numArray3[9] = (byte) 217;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[2] = (byte) 164;
    numArray5[0] = (byte) 73;
    numArray5[17] = (byte) 78;
    numArray5[3] = (byte) 211;
    numArray5[14] = (byte) 183;
    numArray5[5] = (byte) 212;
    numArray5[18] = (byte) 40;
    numArray5[7] = (byte) 78;
    numArray5[8] = (byte) 96 /*0x60*/;
    numArray5[9] = (byte) 43;
    numArray5[10] = (byte) 25;
    numArray5[11] = (byte) 229;
    numArray5[12] = (byte) 52;
    numArray5[13] = (byte) 224 /*0xE0*/;
    numArray5[6] = (byte) 200;
    numArray5[4] = (byte) 145;
    numArray5[16 /*0x10*/] = (byte) 164;
    numArray5[15] = (byte) 126;
    numArray5[22] = (byte) 216;
    numArray5[19] = (byte) 149;
    numArray5[1] = (byte) 107;
    numArray5[21] = (byte) 24;
    numArray5[20] = (byte) 21;
    byte[] numArray6 = new byte[23];
    numArray6[1] = (byte) 99;
    numArray6[12] = (byte) 93;
    numArray6[0] = (byte) 55;
    numArray6[3] = (byte) 34;
    numArray6[10] = (byte) 11;
    numArray6[6] = (byte) 223;
    numArray6[2] = (byte) 20;
    numArray6[7] = (byte) 173;
    numArray6[18] = (byte) 221;
    numArray6[22] = (byte) 184;
    numArray6[11] = (byte) 103;
    numArray6[5] = (byte) 225;
    numArray6[19] = (byte) 93;
    numArray6[13] = (byte) 27;
    numArray6[4] = (byte) 204;
    numArray6[15] = (byte) 164;
    numArray6[16 /*0x10*/] = (byte) 50;
    numArray6[8] = (byte) 72;
    numArray6[9] = (byte) 2;
    numArray6[21] = (byte) 10;
    numArray6[20] = (byte) 198;
    numArray6[17] = (byte) 211;
    numArray6[14] = (byte) 24;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_700()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[15] = (byte) 7;
      numArray2[13] = (byte) 3;
      numArray2[2] = (byte) 193;
      numArray2[3] = (byte) 9;
      numArray2[1] = (byte) 98;
      numArray2[20] = (byte) 195;
      numArray2[18] = (byte) 57;
      numArray2[22] = (byte) 200;
      numArray2[5] = (byte) 254;
      numArray2[9] = (byte) 66;
      numArray2[6] = (byte) 170;
      numArray2[11] = (byte) 219;
      numArray2[12] = (byte) 86;
      numArray2[10] = (byte) 166;
      numArray2[14] = (byte) 42;
      numArray2[17] = (byte) 94;
      numArray2[4] = (byte) 186;
      numArray2[0] = (byte) 253;
      numArray2[21] = (byte) 165;
      numArray2[19] = (byte) 99;
      numArray2[7] = (byte) 88;
      numArray2[16 /*0x10*/] = (byte) 72;
      numArray2[8] = (byte) 26;
      byte[] numArray3 = new byte[23];
      numArray3[1] = (byte) 72;
      numArray3[19] = (byte) 252;
      numArray3[2] = (byte) 21;
      numArray3[3] = (byte) 230;
      numArray3[18] = byte.MaxValue;
      numArray3[10] = (byte) 55;
      numArray3[15] = (byte) 151;
      numArray3[4] = (byte) 29;
      numArray3[14] = (byte) 24;
      numArray3[9] = (byte) 189;
      numArray3[5] = (byte) 138;
      numArray3[12] = (byte) 64 /*0x40*/;
      numArray3[6] = (byte) 163;
      numArray3[13] = (byte) 68;
      numArray3[22] = (byte) 177;
      numArray3[7] = (byte) 199;
      numArray3[16 /*0x10*/] = (byte) 110;
      numArray3[17] = (byte) 113;
      numArray3[8] = (byte) 251;
      numArray3[11] = (byte) 15;
      numArray3[20] = (byte) 184;
      numArray3[21] = (byte) 224 /*0xE0*/;
      numArray3[0] = (byte) 21;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[11] = (byte) 11;
    numArray5[1] = (byte) 132;
    numArray5[2] = (byte) 164;
    numArray5[3] = (byte) 213;
    numArray5[17] = (byte) 234;
    numArray5[15] = (byte) 191;
    numArray5[6] = (byte) 94;
    numArray5[19] = (byte) 55;
    numArray5[8] = (byte) 109;
    numArray5[20] = (byte) 180;
    numArray5[10] = (byte) 103;
    numArray5[4] = (byte) 57;
    numArray5[12] = (byte) 177;
    numArray5[13] = (byte) 238;
    numArray5[18] = (byte) 252;
    numArray5[9] = (byte) 75;
    numArray5[16 /*0x10*/] = (byte) 12;
    numArray5[14] = (byte) 110;
    numArray5[22] = (byte) 211;
    numArray5[7] = (byte) 247;
    numArray5[0] = (byte) 132;
    numArray5[21] = (byte) 57;
    numArray5[5] = (byte) 161;
    byte[] numArray6 = new byte[23]
    {
      (byte) 19,
      (byte) 121,
      (byte) 128 /*0x80*/,
      (byte) 247,
      (byte) 87,
      (byte) 173,
      (byte) 32 /*0x20*/,
      (byte) 34,
      (byte) 91,
      (byte) 205,
      (byte) 65,
      (byte) 147,
      (byte) 254,
      (byte) 67,
      (byte) 49,
      (byte) 61,
      (byte) 141,
      (byte) 41,
      (byte) 243,
      (byte) 89,
      (byte) 52,
      (byte) 143,
      (byte) 152
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
