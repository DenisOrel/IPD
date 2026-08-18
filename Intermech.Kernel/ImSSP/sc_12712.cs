// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12712
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12712
{
  internal static string ssp_appserver_12713()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[83];
      byte[] numArray2 = new byte[55]
      {
        (byte) 110,
        (byte) 125,
        (byte) 81,
        (byte) 20,
        (byte) 96 /*0x60*/,
        (byte) 77,
        (byte) 31 /*0x1F*/,
        (byte) 88,
        (byte) 100,
        (byte) 88,
        (byte) 33,
        (byte) 30,
        (byte) 11,
        (byte) 12,
        (byte) 115,
        (byte) 38,
        (byte) 0,
        (byte) 49,
        (byte) 3,
        (byte) 237,
        (byte) 77,
        (byte) 140,
        (byte) 160 /*0xA0*/,
        (byte) 58,
        (byte) 2,
        (byte) 251,
        (byte) 172,
        (byte) 150,
        (byte) 117,
        (byte) 49,
        (byte) 81,
        (byte) 184,
        (byte) 61,
        (byte) 97,
        (byte) 115,
        (byte) 31 /*0x1F*/,
        (byte) 35,
        (byte) 51,
        (byte) 74,
        (byte) 31 /*0x1F*/,
        (byte) 54,
        (byte) 6,
        (byte) 178,
        (byte) 112 /*0x70*/,
        (byte) 47,
        byte.MaxValue,
        (byte) 129,
        (byte) 17,
        (byte) 238,
        (byte) 34,
        (byte) 164,
        (byte) 53,
        (byte) 169,
        (byte) 174,
        (byte) 131
      };
      byte[] numArray3 = new byte[55];
      numArray3[13] = (byte) 101;
      numArray3[1] = (byte) 63 /*0x3F*/;
      numArray3[2] = (byte) 183;
      numArray3[18] = (byte) 233;
      numArray3[4] = (byte) 179;
      numArray3[5] = (byte) 220;
      numArray3[25] = (byte) 96 /*0x60*/;
      numArray3[51] = (byte) 59;
      numArray3[3] = (byte) 38;
      numArray3[9] = (byte) 50;
      numArray3[10] = (byte) 27;
      numArray3[11] = (byte) 143;
      numArray3[26] = (byte) 237;
      numArray3[28] = (byte) 88;
      numArray3[45] = (byte) 46;
      numArray3[15] = (byte) 19;
      numArray3[43] = (byte) 82;
      numArray3[7] = (byte) 120;
      numArray3[17] = (byte) 123;
      numArray3[19] = (byte) 91;
      numArray3[20] = (byte) 106;
      numArray3[39] = (byte) 234;
      numArray3[22] = (byte) 154;
      numArray3[23] = (byte) 95;
      numArray3[24] = (byte) 41;
      numArray3[16 /*0x10*/] = (byte) 56;
      numArray3[31 /*0x1F*/] = (byte) 204;
      numArray3[27] = (byte) 183;
      numArray3[48 /*0x30*/] = (byte) 231;
      numArray3[46] = (byte) 116;
      numArray3[30] = (byte) 83;
      numArray3[8] = (byte) 120;
      numArray3[35] = (byte) 121;
      numArray3[33] = (byte) 187;
      numArray3[34] = (byte) 217;
      numArray3[32 /*0x20*/] = (byte) 73;
      numArray3[38] = (byte) 35;
      numArray3[37] = (byte) 110;
      numArray3[47] = (byte) 16 /*0x10*/;
      numArray3[0] = (byte) 162;
      numArray3[40] = (byte) 162;
      numArray3[6] = (byte) 64 /*0x40*/;
      numArray3[42] = (byte) 59;
      numArray3[44] = (byte) 114;
      numArray3[21] = (byte) 142;
      numArray3[36] = (byte) 69;
      numArray3[54] = (byte) 190;
      numArray3[12] = (byte) 229;
      numArray3[41] = (byte) 4;
      numArray3[50] = (byte) 9;
      numArray3[29] = (byte) 23;
      numArray3[14] = (byte) 213;
      numArray3[52] = (byte) 124;
      numArray3[53] = (byte) 113;
      numArray3[49] = (byte) 48 /*0x30*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28]
      {
        (byte) 11,
        (byte) 13,
        (byte) 235,
        (byte) 5,
        (byte) 226,
        (byte) 143,
        (byte) 165,
        (byte) 235,
        (byte) 88,
        (byte) 81,
        (byte) 202,
        (byte) 6,
        (byte) 16 /*0x10*/,
        (byte) 246,
        (byte) 171,
        (byte) 2,
        (byte) 100,
        (byte) 10,
        (byte) 34,
        (byte) 90,
        (byte) 197,
        (byte) 68,
        (byte) 229,
        (byte) 161,
        (byte) 34,
        (byte) 11,
        (byte) 52,
        (byte) 181
      };
      byte[] numArray5 = new byte[28]
      {
        (byte) 233,
        (byte) 104,
        (byte) 77,
        (byte) 125,
        (byte) 61,
        (byte) 214,
        (byte) 62,
        (byte) 242,
        (byte) 181,
        (byte) 181,
        (byte) 203,
        (byte) 250,
        (byte) 247,
        (byte) 207,
        (byte) 147,
        (byte) 7,
        (byte) 179,
        (byte) 186,
        (byte) 166,
        (byte) 2,
        (byte) 247,
        (byte) 42,
        (byte) 140,
        (byte) 233,
        (byte) 212,
        (byte) 24,
        (byte) 243,
        (byte) 166
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[83];
    byte[] numArray7 = new byte[55]
    {
      (byte) 160 /*0xA0*/,
      (byte) 137,
      (byte) 150,
      (byte) 1,
      (byte) 19,
      (byte) 250,
      (byte) 121,
      (byte) 202,
      (byte) 148,
      (byte) 218,
      (byte) 126,
      (byte) 109,
      (byte) 205,
      (byte) 13,
      (byte) 113,
      (byte) 139,
      (byte) 40,
      (byte) 43,
      (byte) 75,
      (byte) 96 /*0x60*/,
      (byte) 236,
      (byte) 26,
      (byte) 173,
      (byte) 231,
      (byte) 198,
      (byte) 40,
      (byte) 73,
      (byte) 227,
      (byte) 214,
      (byte) 91,
      (byte) 239,
      (byte) 28,
      (byte) 14,
      (byte) 184,
      (byte) 41,
      (byte) 124,
      (byte) 122,
      (byte) 245,
      (byte) 151,
      (byte) 8,
      (byte) 235,
      (byte) 64 /*0x40*/,
      (byte) 142,
      (byte) 127 /*0x7F*/,
      (byte) 70,
      (byte) 214,
      (byte) 23,
      (byte) 147,
      (byte) 176 /*0xB0*/,
      (byte) 128 /*0x80*/,
      (byte) 11,
      (byte) 137,
      (byte) 130,
      (byte) 233,
      (byte) 172
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 184,
      (byte) 123,
      (byte) 240 /*0xF0*/,
      (byte) 42,
      (byte) 78,
      (byte) 61,
      (byte) 143,
      (byte) 251,
      (byte) 231,
      (byte) 167,
      (byte) 217,
      (byte) 170,
      (byte) 196,
      (byte) 105,
      (byte) 64 /*0x40*/,
      (byte) 86,
      (byte) 34,
      (byte) 171,
      (byte) 211,
      (byte) 130,
      (byte) 217,
      (byte) 102,
      (byte) 29,
      (byte) 11,
      (byte) 10,
      (byte) 17,
      (byte) 139,
      (byte) 7,
      (byte) 158,
      (byte) 66,
      (byte) 161,
      (byte) 153,
      (byte) 163,
      (byte) 116,
      (byte) 7,
      (byte) 141,
      (byte) 15,
      (byte) 74,
      (byte) 3,
      (byte) 165,
      (byte) 40,
      (byte) 162,
      (byte) 176 /*0xB0*/,
      (byte) 193,
      (byte) 239,
      (byte) 55,
      (byte) 177,
      (byte) 21,
      (byte) 18,
      (byte) 232,
      (byte) 47,
      (byte) 68,
      (byte) 39,
      (byte) 76,
      (byte) 199
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[28];
    numArray9[10] = (byte) 20;
    numArray9[7] = (byte) 234;
    numArray9[2] = (byte) 235;
    numArray9[17] = (byte) 116;
    numArray9[20] = (byte) 45;
    numArray9[5] = (byte) 243;
    numArray9[0] = (byte) 174;
    numArray9[22] = (byte) 240 /*0xF0*/;
    numArray9[8] = (byte) 97;
    numArray9[19] = (byte) 140;
    numArray9[14] = (byte) 50;
    numArray9[24] = (byte) 181;
    numArray9[12] = (byte) 244;
    numArray9[13] = (byte) 16 /*0x10*/;
    numArray9[6] = (byte) 56;
    numArray9[15] = (byte) 147;
    numArray9[25] = (byte) 50;
    numArray9[3] = (byte) 176 /*0xB0*/;
    numArray9[18] = (byte) 171;
    numArray9[26] = (byte) 85;
    numArray9[9] = (byte) 95;
    numArray9[27] = (byte) 79;
    numArray9[1] = (byte) 58;
    numArray9[11] = (byte) 227;
    numArray9[4] = (byte) 149;
    numArray9[21] = (byte) 160 /*0xA0*/;
    numArray9[23] = (byte) 16 /*0x10*/;
    numArray9[16 /*0x10*/] = (byte) 67;
    byte[] numArray10 = new byte[28];
    numArray10[1] = (byte) 179;
    numArray10[4] = (byte) 131;
    numArray10[2] = (byte) 135;
    numArray10[6] = (byte) 222;
    numArray10[25] = (byte) 192 /*0xC0*/;
    numArray10[5] = (byte) 42;
    numArray10[23] = (byte) 122;
    numArray10[16 /*0x10*/] = (byte) 117;
    numArray10[8] = (byte) 142;
    numArray10[9] = (byte) 170;
    numArray10[10] = (byte) 74;
    numArray10[11] = (byte) 137;
    numArray10[19] = (byte) 100;
    numArray10[0] = (byte) 131;
    numArray10[14] = (byte) 12;
    numArray10[15] = (byte) 56;
    numArray10[24] = (byte) 99;
    numArray10[21] = (byte) 128 /*0x80*/;
    numArray10[18] = (byte) 43;
    numArray10[27] = (byte) 196;
    numArray10[20] = (byte) 233;
    numArray10[13] = (byte) 157;
    numArray10[22] = (byte) 134;
    numArray10[12] = (byte) 244;
    numArray10[7] = (byte) 4;
    numArray10[26] = (byte) 203;
    numArray10[3] = (byte) 72;
    numArray10[17] = (byte) 124;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 28);
    for (int index = 0; index < 28; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
