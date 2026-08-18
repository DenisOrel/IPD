// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7192
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7192
{
  private static byte[] sspq = new byte[53]
  {
    (byte) 138,
    (byte) 212,
    (byte) 1,
    (byte) 61,
    (byte) 146,
    (byte) 127 /*0x7F*/,
    (byte) 243,
    (byte) 49,
    (byte) 134,
    (byte) 21,
    (byte) 145,
    (byte) 161,
    (byte) 37,
    (byte) 176 /*0xB0*/,
    (byte) 228,
    (byte) 127 /*0x7F*/,
    (byte) 166,
    (byte) 160 /*0xA0*/,
    (byte) 238,
    (byte) 180,
    (byte) 176 /*0xB0*/,
    (byte) 15,
    (byte) 11,
    (byte) 16 /*0x10*/,
    (byte) 152,
    (byte) 246,
    (byte) 200,
    (byte) 55,
    (byte) 114,
    (byte) 248,
    (byte) 252,
    (byte) 176 /*0xB0*/,
    (byte) 13,
    (byte) 131,
    (byte) 19,
    (byte) 245,
    (byte) 120,
    (byte) 71,
    (byte) 192 /*0xC0*/,
    (byte) 42,
    (byte) 62,
    (byte) 154,
    (byte) 65,
    (byte) 145,
    (byte) 99,
    (byte) 58,
    (byte) 127 /*0x7F*/,
    (byte) 31 /*0x1F*/,
    (byte) 159,
    (byte) 80 /*0x50*/,
    (byte) 141,
    (byte) 35,
    (byte) 14
  };
  private static byte[] sspr = new byte[53]
  {
    (byte) 152,
    (byte) 83,
    (byte) 200,
    (byte) 176 /*0xB0*/,
    (byte) 175,
    (byte) 54,
    (byte) 11,
    (byte) 129,
    (byte) 141,
    (byte) 28,
    (byte) 233,
    (byte) 140,
    (byte) 138,
    (byte) 106,
    (byte) 199,
    (byte) 205,
    (byte) 107,
    (byte) 10,
    (byte) 43,
    (byte) 132,
    (byte) 43,
    (byte) 183,
    (byte) 23,
    (byte) 97,
    (byte) 184,
    (byte) 187,
    (byte) 75,
    (byte) 235,
    (byte) 54,
    (byte) 17,
    (byte) 3,
    (byte) 98,
    (byte) 173,
    (byte) 111,
    (byte) 247,
    (byte) 11,
    (byte) 241,
    (byte) 217,
    (byte) 51,
    (byte) 109,
    (byte) 248,
    (byte) 106,
    (byte) 58,
    (byte) 28,
    (byte) 96 /*0x60*/,
    (byte) 149,
    (byte) 30,
    (byte) 77,
    (byte) 161,
    (byte) 249,
    (byte) 248,
    (byte) 113,
    (byte) 178
  };

  internal static string ssp_imclient_7193()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[69];
      byte[] numArray2 = new byte[55]
      {
        (byte) 175,
        (byte) 97,
        (byte) 79,
        (byte) 133,
        (byte) 171,
        (byte) 228,
        (byte) 154,
        (byte) 205,
        (byte) 197,
        (byte) 180,
        (byte) 156,
        (byte) 142,
        (byte) 186,
        (byte) 21,
        (byte) 66,
        (byte) 74,
        (byte) 56,
        (byte) 220,
        (byte) 118,
        (byte) 71,
        (byte) 247,
        (byte) 189,
        (byte) 36,
        (byte) 101,
        (byte) 73,
        (byte) 215,
        (byte) 232,
        (byte) 215,
        (byte) 152,
        (byte) 126,
        (byte) 107,
        (byte) 228,
        (byte) 90,
        (byte) 182,
        (byte) 170,
        (byte) 164,
        (byte) 251,
        (byte) 155,
        (byte) 95,
        (byte) 89,
        (byte) 155,
        (byte) 207,
        (byte) 137,
        (byte) 90,
        (byte) 59,
        (byte) 172,
        (byte) 110,
        (byte) 37,
        (byte) 154,
        (byte) 176 /*0xB0*/,
        (byte) 213,
        (byte) 254,
        (byte) 93,
        (byte) 66,
        (byte) 107
      };
      byte[] numArray3 = new byte[55];
      numArray3[6] = (byte) 14;
      numArray3[11] = (byte) 28;
      numArray3[2] = (byte) 206;
      numArray3[54] = (byte) 85;
      numArray3[41] = (byte) 33;
      numArray3[5] = (byte) 123;
      numArray3[20] = (byte) 177;
      numArray3[1] = (byte) 171;
      numArray3[32 /*0x20*/] = (byte) 56;
      numArray3[9] = (byte) 243;
      numArray3[25] = (byte) 7;
      numArray3[52] = (byte) 91;
      numArray3[21] = (byte) 198;
      numArray3[13] = (byte) 26;
      numArray3[24] = (byte) 20;
      numArray3[4] = (byte) 185;
      numArray3[51] = (byte) 34;
      numArray3[28] = (byte) 64 /*0x40*/;
      numArray3[29] = (byte) 207;
      numArray3[19] = (byte) 130;
      numArray3[10] = (byte) 231;
      numArray3[7] = (byte) 142;
      numArray3[22] = (byte) 115;
      numArray3[23] = (byte) 221;
      numArray3[3] = (byte) 195;
      numArray3[30] = (byte) 69;
      numArray3[26] = (byte) 151;
      numArray3[16 /*0x10*/] = (byte) 7;
      numArray3[37] = (byte) 136;
      numArray3[8] = (byte) 58;
      numArray3[12] = (byte) 85;
      numArray3[31 /*0x1F*/] = (byte) 214;
      numArray3[27] = (byte) 110;
      numArray3[17] = (byte) 248;
      numArray3[34] = (byte) 137;
      numArray3[35] = (byte) 233;
      numArray3[36] = (byte) 250;
      numArray3[15] = (byte) 22;
      numArray3[0] = (byte) 119;
      numArray3[39] = (byte) 216;
      numArray3[40] = (byte) 235;
      numArray3[33] = (byte) 68;
      numArray3[42] = (byte) 56;
      numArray3[38] = (byte) 187;
      numArray3[44] = (byte) 230;
      numArray3[45] = (byte) 90;
      numArray3[46] = (byte) 135;
      numArray3[47] = (byte) 129;
      numArray3[48 /*0x30*/] = (byte) 46;
      numArray3[50] = (byte) 126;
      numArray3[18] = (byte) 234;
      numArray3[14] = (byte) 52;
      numArray3[49] = (byte) 84;
      numArray3[53] = (byte) 228;
      numArray3[43] = (byte) 36;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14]
      {
        (byte) 191,
        (byte) 157,
        (byte) 106,
        (byte) 58,
        (byte) 133,
        (byte) 75,
        (byte) 157,
        (byte) 220,
        (byte) 48 /*0x30*/,
        (byte) 28,
        (byte) 185,
        (byte) 178,
        (byte) 55,
        (byte) 61
      };
      byte[] numArray5 = new byte[14];
      numArray5[1] = (byte) 54;
      numArray5[7] = (byte) 135;
      numArray5[2] = (byte) 28;
      numArray5[5] = (byte) 174;
      numArray5[4] = (byte) 49;
      numArray5[10] = (byte) 208 /*0xD0*/;
      numArray5[3] = (byte) 247;
      numArray5[6] = (byte) 150;
      numArray5[0] = (byte) 24;
      numArray5[9] = (byte) 109;
      numArray5[8] = (byte) 52;
      numArray5[11] = (byte) 142;
      numArray5[12] = (byte) 249;
      numArray5[13] = (byte) 224 /*0xE0*/;
      key.Query(true, 348, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[69];
    byte[] numArray7 = new byte[55];
    numArray7[48 /*0x30*/] = byte.MaxValue;
    numArray7[1] = (byte) 251;
    numArray7[10] = (byte) 52;
    numArray7[3] = (byte) 82;
    numArray7[11] = (byte) 82;
    numArray7[29] = (byte) 196;
    numArray7[54] = (byte) 190;
    numArray7[41] = (byte) 129;
    numArray7[21] = (byte) 182;
    numArray7[9] = (byte) 164;
    numArray7[50] = (byte) 100;
    numArray7[35] = (byte) 183;
    numArray7[33] = (byte) 155;
    numArray7[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray7[2] = (byte) 244;
    numArray7[15] = (byte) 196;
    numArray7[8] = (byte) 79;
    numArray7[7] = (byte) 82;
    numArray7[6] = (byte) 33;
    numArray7[19] = (byte) 127 /*0x7F*/;
    numArray7[12] = (byte) 91;
    numArray7[47] = (byte) 77;
    numArray7[22] = (byte) 203;
    numArray7[17] = (byte) 251;
    numArray7[5] = (byte) 34;
    numArray7[25] = (byte) 120;
    numArray7[14] = (byte) 7;
    numArray7[27] = (byte) 50;
    numArray7[28] = (byte) 69;
    numArray7[51] = (byte) 117;
    numArray7[30] = (byte) 44;
    numArray7[46] = (byte) 201;
    numArray7[32 /*0x20*/] = (byte) 232;
    numArray7[16 /*0x10*/] = (byte) 35;
    numArray7[34] = (byte) 178;
    numArray7[26] = (byte) 107;
    numArray7[36] = (byte) 66;
    numArray7[37] = (byte) 235;
    numArray7[38] = (byte) 121;
    numArray7[39] = (byte) 206;
    numArray7[23] = (byte) 185;
    numArray7[0] = (byte) 7;
    numArray7[42] = (byte) 61;
    numArray7[43] = (byte) 77;
    numArray7[44] = (byte) 132;
    numArray7[45] = (byte) 83;
    numArray7[18] = (byte) 144 /*0x90*/;
    numArray7[52] = (byte) 90;
    numArray7[13] = (byte) 177;
    numArray7[49] = (byte) 123;
    numArray7[40] = (byte) 158;
    numArray7[4] = (byte) 194;
    numArray7[24] = (byte) 24;
    numArray7[53] = (byte) 199;
    numArray7[20] = (byte) 194;
    byte[] numArray8 = new byte[55]
    {
      (byte) 159,
      (byte) 227,
      (byte) 83,
      (byte) 122,
      (byte) 77,
      (byte) 11,
      (byte) 14,
      (byte) 170,
      (byte) 221,
      (byte) 103,
      (byte) 170,
      (byte) 88,
      (byte) 212,
      (byte) 196,
      (byte) 239,
      (byte) 119,
      (byte) 237,
      (byte) 202,
      (byte) 139,
      (byte) 134,
      (byte) 129,
      (byte) 0,
      (byte) 121,
      (byte) 199,
      (byte) 223,
      (byte) 228,
      (byte) 229,
      (byte) 236,
      (byte) 8,
      (byte) 26,
      (byte) 28,
      (byte) 19,
      (byte) 20,
      (byte) 24,
      (byte) 206,
      (byte) 176 /*0xB0*/,
      (byte) 104,
      (byte) 139,
      (byte) 220,
      (byte) 6,
      (byte) 213,
      (byte) 181,
      (byte) 74,
      (byte) 23,
      (byte) 216,
      (byte) 157,
      (byte) 207,
      (byte) 53,
      (byte) 7,
      (byte) 18,
      (byte) 27,
      (byte) 141,
      (byte) 122,
      (byte) 130,
      (byte) 69
    };
    key.Query(true, 348, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[14];
    numArray9[11] = (byte) 157;
    numArray9[0] = (byte) 6;
    numArray9[1] = (byte) 116;
    numArray9[3] = (byte) 152;
    numArray9[5] = (byte) 66;
    numArray9[2] = (byte) 156;
    numArray9[10] = (byte) 227;
    numArray9[8] = (byte) 174;
    numArray9[7] = (byte) 192 /*0xC0*/;
    numArray9[9] = (byte) 72;
    numArray9[12] = (byte) 201;
    numArray9[6] = (byte) 43;
    numArray9[4] = (byte) 187;
    numArray9[13] = (byte) 122;
    byte[] numArray10 = new byte[14]
    {
      (byte) 34,
      (byte) 226,
      (byte) 235,
      (byte) 55,
      (byte) 130,
      (byte) 125,
      (byte) 22,
      (byte) 24,
      (byte) 80 /*0x50*/,
      (byte) 153,
      (byte) 161,
      (byte) 52,
      (byte) 210,
      (byte) 44
    };
    key.Query(true, 348, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 14);
    for (int index = 0; index < 14; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[53];
    byte[] response = new byte[53];
    Array.Copy((Array) sc_7192.sspq, 0, (Array) numArray11, 0, 53);
    key.Query(true, 348, numArray11, response);
    Array.Copy((Array) sc_7192.sspr, 0, (Array) numArray11, 0, 53);
    for (int index = 0; index < numArray11.Length; ++index)
    {
      if ((int) numArray11[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray6);
  }
}
