// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12676
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12676
{
  internal static string ssp_appserver_12677()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/];
      numArray2[3] = (byte) 170;
      numArray2[1] = (byte) 27;
      numArray2[2] = (byte) 42;
      numArray2[28] = (byte) 191;
      numArray2[4] = (byte) 103;
      numArray2[13] = (byte) 110;
      numArray2[6] = (byte) 139;
      numArray2[7] = (byte) 91;
      numArray2[12] = (byte) 133;
      numArray2[11] = (byte) 222;
      numArray2[31 /*0x1F*/] = (byte) 57;
      numArray2[20] = (byte) 134;
      numArray2[24] = (byte) 110;
      numArray2[25] = (byte) 188;
      numArray2[14] = (byte) 206;
      numArray2[30] = (byte) 73;
      numArray2[0] = (byte) 245;
      numArray2[17] = (byte) 44;
      numArray2[18] = (byte) 56;
      numArray2[15] = (byte) 222;
      numArray2[27] = (byte) 36;
      numArray2[21] = (byte) 91;
      numArray2[19] = (byte) 186;
      numArray2[23] = (byte) 124;
      numArray2[10] = (byte) 158;
      numArray2[9] = (byte) 8;
      numArray2[26] = (byte) 199;
      numArray2[16 /*0x10*/] = (byte) 166;
      numArray2[5] = (byte) 137;
      numArray2[22] = (byte) 184;
      numArray2[8] = (byte) 224 /*0xE0*/;
      numArray2[29] = (byte) 71;
      byte[] numArray3 = new byte[32 /*0x20*/]
      {
        (byte) 205,
        (byte) 248,
        (byte) 81,
        (byte) 215,
        (byte) 6,
        (byte) 220,
        (byte) 16 /*0x10*/,
        (byte) 139,
        (byte) 188,
        (byte) 173,
        (byte) 141,
        (byte) 53,
        (byte) 49,
        (byte) 254,
        (byte) 64 /*0x40*/,
        (byte) 239,
        (byte) 161,
        (byte) 140,
        (byte) 2,
        (byte) 146,
        (byte) 127 /*0x7F*/,
        (byte) 6,
        (byte) 215,
        (byte) 116,
        (byte) 165,
        (byte) 71,
        (byte) 0,
        (byte) 235,
        (byte) 104,
        (byte) 141,
        (byte) 61,
        (byte) 184
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[32 /*0x20*/];
    byte[] numArray5 = new byte[32 /*0x20*/];
    numArray5[7] = (byte) 14;
    numArray5[31 /*0x1F*/] = (byte) 89;
    numArray5[15] = (byte) 145;
    numArray5[25] = (byte) 252;
    numArray5[4] = (byte) 109;
    numArray5[18] = (byte) 49;
    numArray5[6] = (byte) 149;
    numArray5[10] = (byte) 160 /*0xA0*/;
    numArray5[2] = (byte) 102;
    numArray5[12] = (byte) 35;
    numArray5[20] = (byte) 136;
    numArray5[11] = (byte) 243;
    numArray5[30] = (byte) 54;
    numArray5[13] = (byte) 147;
    numArray5[14] = (byte) 69;
    numArray5[8] = (byte) 222;
    numArray5[16 /*0x10*/] = (byte) 184;
    numArray5[17] = (byte) 161;
    numArray5[5] = (byte) 45;
    numArray5[19] = (byte) 145;
    numArray5[22] = (byte) 194;
    numArray5[0] = (byte) 163;
    numArray5[9] = (byte) 139;
    numArray5[23] = (byte) 5;
    numArray5[21] = (byte) 152;
    numArray5[24] = (byte) 174;
    numArray5[26] = (byte) 84;
    numArray5[27] = (byte) 224 /*0xE0*/;
    numArray5[28] = (byte) 68;
    numArray5[29] = (byte) 11;
    numArray5[3] = (byte) 129;
    numArray5[1] = (byte) 18;
    byte[] numArray6 = new byte[32 /*0x20*/]
    {
      (byte) 73,
      (byte) 220,
      (byte) 73,
      (byte) 78,
      (byte) 86,
      (byte) 148,
      (byte) 217,
      (byte) 147,
      (byte) 158,
      (byte) 147,
      (byte) 22,
      (byte) 41,
      (byte) 100,
      (byte) 192 /*0xC0*/,
      (byte) 206,
      (byte) 99,
      (byte) 105,
      (byte) 139,
      (byte) 112 /*0x70*/,
      (byte) 111,
      (byte) 88,
      (byte) 130,
      (byte) 112 /*0x70*/,
      (byte) 0,
      (byte) 17,
      (byte) 205,
      (byte) 154,
      (byte) 203,
      (byte) 234,
      (byte) 61,
      (byte) 109,
      (byte) 60
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12678()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 12,
        (byte) 214,
        (byte) 101,
        (byte) 52,
        (byte) 104,
        (byte) 69,
        (byte) 125,
        (byte) 131,
        (byte) 44,
        (byte) 179,
        (byte) 67,
        (byte) 243,
        (byte) 228,
        (byte) 60,
        (byte) 99,
        (byte) 111,
        (byte) 40,
        (byte) 62,
        (byte) 203,
        (byte) 80 /*0x50*/,
        (byte) 80 /*0x50*/,
        (byte) 123,
        (byte) 30,
        (byte) 202,
        (byte) 176 /*0xB0*/,
        (byte) 199,
        (byte) 17,
        (byte) 94,
        (byte) 59,
        (byte) 153,
        (byte) 185,
        (byte) 123,
        (byte) 236,
        (byte) 253,
        (byte) 10,
        (byte) 166,
        (byte) 112 /*0x70*/,
        (byte) 175,
        (byte) 143,
        (byte) 72,
        (byte) 86
      };
      byte[] numArray3 = new byte[41];
      numArray3[21] = (byte) 101;
      numArray3[1] = (byte) 240 /*0xF0*/;
      numArray3[2] = (byte) 207;
      numArray3[11] = (byte) 40;
      numArray3[4] = (byte) 137;
      numArray3[8] = (byte) 226;
      numArray3[0] = (byte) 211;
      numArray3[7] = (byte) 34;
      numArray3[19] = (byte) 166;
      numArray3[10] = (byte) 240 /*0xF0*/;
      numArray3[30] = (byte) 106;
      numArray3[6] = (byte) 210;
      numArray3[12] = (byte) 92;
      numArray3[37] = (byte) 9;
      numArray3[39] = (byte) 134;
      numArray3[15] = (byte) 114;
      numArray3[16 /*0x10*/] = (byte) 238;
      numArray3[17] = (byte) 41;
      numArray3[18] = (byte) 26;
      numArray3[33] = (byte) 121;
      numArray3[29] = (byte) 41;
      numArray3[22] = (byte) 111;
      numArray3[13] = (byte) 136;
      numArray3[5] = (byte) 1;
      numArray3[20] = (byte) 202;
      numArray3[25] = (byte) 192 /*0xC0*/;
      numArray3[3] = (byte) 136;
      numArray3[26] = (byte) 191;
      numArray3[28] = (byte) 170;
      numArray3[27] = (byte) 240 /*0xF0*/;
      numArray3[24] = (byte) 161;
      numArray3[31 /*0x1F*/] = (byte) 250;
      numArray3[32 /*0x20*/] = (byte) 243;
      numArray3[35] = (byte) 9;
      numArray3[23] = (byte) 16 /*0x10*/;
      numArray3[36] = (byte) 219;
      numArray3[38] = (byte) 129;
      numArray3[34] = (byte) 244;
      numArray3[9] = (byte) 142;
      numArray3[14] = (byte) 58;
      numArray3[40] = (byte) 125;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 162,
      (byte) 0,
      (byte) 110,
      (byte) 51,
      (byte) 121,
      (byte) 238,
      (byte) 213,
      (byte) 44,
      (byte) 238,
      (byte) 20,
      (byte) 55,
      (byte) 237,
      (byte) 76,
      (byte) 162,
      (byte) 87,
      (byte) 217,
      (byte) 164,
      (byte) 107,
      (byte) 147,
      (byte) 91,
      (byte) 49,
      (byte) 103,
      (byte) 247,
      (byte) 131,
      (byte) 174,
      (byte) 244,
      (byte) 54,
      (byte) 238,
      (byte) 178,
      (byte) 101,
      (byte) 191,
      (byte) 16 /*0x10*/,
      (byte) 102,
      (byte) 163,
      (byte) 87,
      (byte) 198,
      (byte) 152,
      (byte) 179,
      (byte) 183,
      (byte) 207,
      (byte) 42
    };
    byte[] numArray6 = new byte[41]
    {
      (byte) 97,
      (byte) 197,
      (byte) 107,
      (byte) 82,
      (byte) 254,
      (byte) 6,
      (byte) 139,
      (byte) 149,
      (byte) 241,
      (byte) 109,
      (byte) 161,
      (byte) 56,
      (byte) 14,
      (byte) 100,
      (byte) 33,
      (byte) 212,
      (byte) 186,
      (byte) 223,
      (byte) 47,
      (byte) 57,
      (byte) 213,
      (byte) 175,
      (byte) 192 /*0xC0*/,
      (byte) 23,
      (byte) 67,
      (byte) 171,
      (byte) 138,
      (byte) 143,
      (byte) 20,
      (byte) 177,
      (byte) 93,
      (byte) 20,
      (byte) 80 /*0x50*/,
      (byte) 150,
      (byte) 200,
      (byte) 114,
      (byte) 105,
      (byte) 15,
      (byte) 64 /*0x40*/,
      (byte) 10,
      (byte) 76
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
