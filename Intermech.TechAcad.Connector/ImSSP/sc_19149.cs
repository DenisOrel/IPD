// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19149
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19149
{
  private static byte[] sspq = new byte[20]
  {
    (byte) 120,
    (byte) 249,
    (byte) 151,
    (byte) 43,
    (byte) 142,
    (byte) 85,
    (byte) 118,
    (byte) 88,
    (byte) 144 /*0x90*/,
    (byte) 168,
    (byte) 148,
    (byte) 8,
    (byte) 41,
    (byte) 245,
    (byte) 137,
    (byte) 123,
    (byte) 129,
    (byte) 151,
    (byte) 201,
    (byte) 165
  };
  private static byte[] sspr = new byte[20]
  {
    (byte) 215,
    (byte) 2,
    (byte) 4,
    (byte) 21,
    (byte) 125,
    (byte) 223,
    (byte) 130,
    (byte) 138,
    (byte) 95,
    (byte) 250,
    (byte) 109,
    (byte) 36,
    (byte) 12,
    (byte) 28,
    (byte) 229,
    (byte) 4,
    (byte) 4,
    (byte) 129,
    (byte) 93,
    (byte) 203
  };

  internal static string ssp_techacad_19150()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[34];
      byte[] numArray2 = new byte[34]
      {
        (byte) 79,
        (byte) 157,
        (byte) 235,
        (byte) 84,
        (byte) 82,
        (byte) 168,
        (byte) 77,
        (byte) 232,
        (byte) 249,
        (byte) 125,
        (byte) 59,
        (byte) 161,
        (byte) 224 /*0xE0*/,
        (byte) 19,
        (byte) 5,
        (byte) 183,
        (byte) 120,
        (byte) 181,
        (byte) 195,
        (byte) 114,
        (byte) 111,
        (byte) 226,
        (byte) 31 /*0x1F*/,
        (byte) 190,
        (byte) 140,
        (byte) 217,
        (byte) 194,
        (byte) 30,
        (byte) 152,
        (byte) 169,
        (byte) 185,
        (byte) 172,
        (byte) 205,
        (byte) 90
      };
      byte[] numArray3 = new byte[34];
      numArray3[27] = (byte) 20;
      numArray3[25] = (byte) 150;
      numArray3[13] = (byte) 170;
      numArray3[3] = (byte) 77;
      numArray3[0] = (byte) 210;
      numArray3[5] = (byte) 186;
      numArray3[24] = (byte) 57;
      numArray3[19] = (byte) 87;
      numArray3[8] = (byte) 41;
      numArray3[12] = (byte) 6;
      numArray3[10] = (byte) 77;
      numArray3[7] = (byte) 248;
      numArray3[33] = (byte) 106;
      numArray3[11] = (byte) 205;
      numArray3[14] = (byte) 21;
      numArray3[4] = (byte) 19;
      numArray3[9] = (byte) 172;
      numArray3[17] = (byte) 109;
      numArray3[18] = (byte) 211;
      numArray3[29] = (byte) 68;
      numArray3[16 /*0x10*/] = (byte) 249;
      numArray3[21] = (byte) 172;
      numArray3[22] = (byte) 158;
      numArray3[23] = (byte) 248;
      numArray3[15] = (byte) 24;
      numArray3[28] = (byte) 102;
      numArray3[26] = (byte) 202;
      numArray3[1] = (byte) 77;
      numArray3[6] = (byte) 125;
      numArray3[20] = (byte) 213;
      numArray3[30] = (byte) 109;
      numArray3[31 /*0x1F*/] = (byte) 196;
      numArray3[32 /*0x20*/] = (byte) 154;
      numArray3[2] = (byte) 216;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[34];
    byte[] numArray5 = new byte[34]
    {
      (byte) 224 /*0xE0*/,
      (byte) 217,
      (byte) 159,
      (byte) 158,
      (byte) 109,
      (byte) 131,
      (byte) 32 /*0x20*/,
      (byte) 157,
      (byte) 91,
      (byte) 221,
      (byte) 42,
      (byte) 189,
      (byte) 206,
      (byte) 123,
      (byte) 214,
      (byte) 5,
      (byte) 194,
      (byte) 59,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 163,
      (byte) 207,
      (byte) 99,
      (byte) 62,
      (byte) 163,
      (byte) 178,
      (byte) 15,
      (byte) 59,
      (byte) 146,
      (byte) 92,
      (byte) 118,
      (byte) 210,
      (byte) 191,
      (byte) 227
    };
    byte[] numArray6 = new byte[34]
    {
      (byte) 121,
      (byte) 205,
      (byte) 126,
      (byte) 232,
      (byte) 22,
      (byte) 90,
      (byte) 120,
      (byte) 238,
      (byte) 89,
      (byte) 39,
      (byte) 226,
      (byte) 208 /*0xD0*/,
      (byte) 225,
      (byte) 116,
      (byte) 140,
      (byte) 251,
      (byte) 55,
      (byte) 19,
      (byte) 59,
      (byte) 67,
      (byte) 48 /*0x30*/,
      (byte) 157,
      (byte) 154,
      (byte) 131,
      (byte) 114,
      (byte) 174,
      (byte) 240 /*0xF0*/,
      (byte) 244,
      (byte) 72,
      (byte) 122,
      (byte) 133,
      (byte) 164,
      (byte) 168,
      (byte) 230
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 34);
    for (int index = 0; index < 34; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19151()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/];
      numArray2[25] = byte.MaxValue;
      numArray2[13] = (byte) 74;
      numArray2[21] = (byte) 187;
      numArray2[0] = (byte) 178;
      numArray2[4] = (byte) 108;
      numArray2[2] = (byte) 94;
      numArray2[29] = (byte) 234;
      numArray2[7] = (byte) 114;
      numArray2[8] = (byte) 129;
      numArray2[9] = (byte) 212;
      numArray2[10] = (byte) 237;
      numArray2[15] = (byte) 186;
      numArray2[12] = (byte) 17;
      numArray2[14] = (byte) 228;
      numArray2[26] = (byte) 102;
      numArray2[5] = (byte) 155;
      numArray2[22] = (byte) 116;
      numArray2[17] = (byte) 16 /*0x10*/;
      numArray2[18] = (byte) 118;
      numArray2[19] = (byte) 97;
      numArray2[20] = (byte) 251;
      numArray2[6] = (byte) 187;
      numArray2[11] = (byte) 137;
      numArray2[23] = (byte) 228;
      numArray2[24] = (byte) 11;
      numArray2[3] = (byte) 142;
      numArray2[1] = (byte) 234;
      numArray2[16 /*0x10*/] = (byte) 66;
      numArray2[28] = (byte) 254;
      numArray2[31 /*0x1F*/] = (byte) 205;
      numArray2[30] = (byte) 45;
      numArray2[27] = (byte) 88;
      byte[] numArray3 = new byte[32 /*0x20*/]
      {
        (byte) 92,
        (byte) 97,
        (byte) 53,
        (byte) 215,
        (byte) 1,
        (byte) 206,
        (byte) 26,
        (byte) 58,
        (byte) 188,
        (byte) 93,
        (byte) 76,
        (byte) 146,
        (byte) 211,
        (byte) 167,
        (byte) 204,
        (byte) 230,
        (byte) 209,
        (byte) 90,
        (byte) 122,
        (byte) 214,
        (byte) 144 /*0x90*/,
        (byte) 47,
        (byte) 224 /*0xE0*/,
        (byte) 248,
        (byte) 31 /*0x1F*/,
        (byte) 146,
        (byte) 91,
        (byte) 166,
        (byte) 21,
        (byte) 0,
        (byte) 136,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[32 /*0x20*/];
    byte[] numArray5 = new byte[32 /*0x20*/];
    numArray5[13] = (byte) 179;
    numArray5[28] = (byte) 31 /*0x1F*/;
    numArray5[2] = (byte) 237;
    numArray5[3] = (byte) 50;
    numArray5[4] = (byte) 45;
    numArray5[5] = (byte) 173;
    numArray5[6] = (byte) 174;
    numArray5[8] = (byte) 201;
    numArray5[18] = (byte) 239;
    numArray5[10] = (byte) 57;
    numArray5[23] = (byte) 128 /*0x80*/;
    numArray5[11] = (byte) 150;
    numArray5[12] = (byte) 3;
    numArray5[1] = (byte) 193;
    numArray5[31 /*0x1F*/] = (byte) 148;
    numArray5[15] = (byte) 106;
    numArray5[16 /*0x10*/] = (byte) 248;
    numArray5[25] = (byte) 146;
    numArray5[17] = (byte) 74;
    numArray5[19] = (byte) 58;
    numArray5[20] = (byte) 86;
    numArray5[7] = (byte) 222;
    numArray5[22] = (byte) 146;
    numArray5[30] = (byte) 153;
    numArray5[29] = (byte) 189;
    numArray5[0] = (byte) 186;
    numArray5[26] = (byte) 245;
    numArray5[27] = (byte) 147;
    numArray5[24] = (byte) 15;
    numArray5[21] = (byte) 230;
    numArray5[14] = (byte) 58;
    numArray5[9] = (byte) 138;
    byte[] numArray6 = new byte[32 /*0x20*/];
    numArray6[8] = (byte) 55;
    numArray6[15] = (byte) 121;
    numArray6[2] = (byte) 224 /*0xE0*/;
    numArray6[0] = (byte) 242;
    numArray6[4] = (byte) 2;
    numArray6[18] = (byte) 5;
    numArray6[6] = (byte) 96 /*0x60*/;
    numArray6[13] = (byte) 198;
    numArray6[14] = (byte) 225;
    numArray6[9] = (byte) 174;
    numArray6[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    numArray6[11] = (byte) 94;
    numArray6[12] = (byte) 229;
    numArray6[23] = (byte) 253;
    numArray6[27] = (byte) 244;
    numArray6[7] = (byte) 44;
    numArray6[10] = (byte) 16 /*0x10*/;
    numArray6[17] = (byte) 209;
    numArray6[29] = (byte) 190;
    numArray6[19] = (byte) 68;
    numArray6[20] = (byte) 226;
    numArray6[21] = (byte) 81;
    numArray6[22] = (byte) 23;
    numArray6[24] = (byte) 250;
    numArray6[1] = (byte) 133;
    numArray6[3] = (byte) 66;
    numArray6[26] = (byte) 127 /*0x7F*/;
    numArray6[5] = (byte) 0;
    numArray6[28] = (byte) 155;
    numArray6[30] = (byte) 17;
    numArray6[25] = (byte) 127 /*0x7F*/;
    numArray6[16 /*0x10*/] = (byte) 103;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_19149.sspq, 0, (Array) numArray7, 0, 20);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19149.sspr, 0, (Array) numArray7, 0, 20);
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
