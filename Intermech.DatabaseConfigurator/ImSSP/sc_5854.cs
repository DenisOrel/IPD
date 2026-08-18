// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5854
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5854
{
  internal static string ssp_imclient_5855()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[22] = (byte) 196;
      numArray2[8] = (byte) 92;
      numArray2[3] = (byte) 101;
      numArray2[6] = (byte) 81;
      numArray2[1] = (byte) 102;
      numArray2[0] = (byte) 194;
      numArray2[20] = (byte) 254;
      numArray2[7] = (byte) 25;
      numArray2[16 /*0x10*/] = (byte) 179;
      numArray2[9] = (byte) 173;
      numArray2[11] = (byte) 200;
      numArray2[5] = (byte) 1;
      numArray2[4] = (byte) 253;
      numArray2[13] = (byte) 215;
      numArray2[14] = (byte) 132;
      numArray2[15] = (byte) 42;
      numArray2[2] = (byte) 217;
      numArray2[10] = (byte) 178;
      numArray2[18] = (byte) 194;
      numArray2[19] = (byte) 205;
      numArray2[12] = (byte) 175;
      numArray2[21] = (byte) 203;
      numArray2[17] = (byte) 85;
      byte[] numArray3 = new byte[23];
      numArray3[6] = (byte) 234;
      numArray3[1] = (byte) 211;
      numArray3[21] = (byte) 243;
      numArray3[17] = (byte) 91;
      numArray3[12] = (byte) 249;
      numArray3[4] = (byte) 95;
      numArray3[3] = (byte) 224 /*0xE0*/;
      numArray3[7] = (byte) 126;
      numArray3[0] = (byte) 190;
      numArray3[9] = (byte) 1;
      numArray3[8] = (byte) 158;
      numArray3[19] = (byte) 30;
      numArray3[20] = (byte) 212;
      numArray3[10] = (byte) 73;
      numArray3[14] = (byte) 0;
      numArray3[15] = (byte) 45;
      numArray3[16 /*0x10*/] = (byte) 89;
      numArray3[22] = (byte) 193;
      numArray3[18] = (byte) 220;
      numArray3[5] = (byte) 47;
      numArray3[2] = (byte) 58;
      numArray3[13] = (byte) 173;
      numArray3[11] = (byte) 245;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[20] = (byte) 241;
    numArray5[1] = (byte) 59;
    numArray5[12] = (byte) 45;
    numArray5[14] = (byte) 109;
    numArray5[19] = (byte) 64 /*0x40*/;
    numArray5[2] = (byte) 200;
    numArray5[3] = (byte) 4;
    numArray5[11] = (byte) 223;
    numArray5[5] = (byte) 57;
    numArray5[0] = (byte) 68;
    numArray5[10] = (byte) 133;
    numArray5[7] = (byte) 29;
    numArray5[8] = (byte) 49;
    numArray5[9] = (byte) 109;
    numArray5[13] = (byte) 88;
    numArray5[18] = (byte) 96 /*0x60*/;
    numArray5[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray5[17] = (byte) 124;
    numArray5[6] = (byte) 201;
    numArray5[4] = (byte) 129;
    numArray5[15] = (byte) 124;
    numArray5[21] = (byte) 29;
    numArray5[22] = (byte) 218;
    byte[] numArray6 = new byte[23]
    {
      (byte) 89,
      (byte) 177,
      (byte) 141,
      (byte) 120,
      (byte) 205,
      (byte) 110,
      (byte) 25,
      (byte) 63 /*0x3F*/,
      (byte) 179,
      (byte) 54,
      (byte) 190,
      (byte) 254,
      (byte) 183,
      (byte) 8,
      (byte) 180,
      (byte) 101,
      (byte) 180,
      (byte) 111,
      (byte) 159,
      (byte) 44,
      (byte) 189,
      (byte) 2,
      (byte) 234
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_5856()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[0] = (byte) 28;
      numArray2[3] = (byte) 84;
      numArray2[2] = (byte) 34;
      numArray2[10] = (byte) 202;
      numArray2[12] = (byte) 180;
      numArray2[22] = (byte) 6;
      numArray2[6] = (byte) 97;
      numArray2[5] = (byte) 38;
      numArray2[21] = (byte) 34;
      numArray2[8] = (byte) 14;
      numArray2[16 /*0x10*/] = (byte) 62;
      numArray2[11] = (byte) 167;
      numArray2[15] = (byte) 238;
      numArray2[13] = (byte) 5;
      numArray2[9] = (byte) 72;
      numArray2[19] = (byte) 253;
      numArray2[1] = (byte) 232;
      numArray2[17] = (byte) 241;
      numArray2[18] = (byte) 97;
      numArray2[14] = (byte) 233;
      numArray2[20] = (byte) 248;
      numArray2[7] = (byte) 118;
      numArray2[4] = (byte) 241;
      numArray2[23] = (byte) 247;
      byte[] numArray3 = new byte[24];
      numArray3[3] = (byte) 156;
      numArray3[1] = (byte) 122;
      numArray3[4] = (byte) 19;
      numArray3[16 /*0x10*/] = (byte) 142;
      numArray3[2] = (byte) 246;
      numArray3[5] = (byte) 45;
      numArray3[6] = (byte) 253;
      numArray3[7] = (byte) 165;
      numArray3[0] = (byte) 79;
      numArray3[19] = (byte) 145;
      numArray3[23] = (byte) 51;
      numArray3[17] = (byte) 128 /*0x80*/;
      numArray3[12] = (byte) 151;
      numArray3[18] = (byte) 175;
      numArray3[14] = (byte) 212;
      numArray3[15] = (byte) 133;
      numArray3[20] = (byte) 165;
      numArray3[10] = (byte) 81;
      numArray3[9] = (byte) 77;
      numArray3[11] = (byte) 156;
      numArray3[13] = (byte) 64 /*0x40*/;
      numArray3[21] = (byte) 224 /*0xE0*/;
      numArray3[22] = (byte) 22;
      numArray3[8] = (byte) 185;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24];
    numArray5[18] = (byte) 135;
    numArray5[1] = (byte) 194;
    numArray5[14] = (byte) 21;
    numArray5[19] = (byte) 5;
    numArray5[4] = (byte) 208 /*0xD0*/;
    numArray5[5] = (byte) 35;
    numArray5[0] = (byte) 234;
    numArray5[10] = (byte) 99;
    numArray5[12] = (byte) 202;
    numArray5[6] = (byte) 158;
    numArray5[7] = (byte) 70;
    numArray5[11] = (byte) 75;
    numArray5[22] = (byte) 32 /*0x20*/;
    numArray5[13] = (byte) 87;
    numArray5[21] = (byte) 187;
    numArray5[3] = (byte) 90;
    numArray5[15] = (byte) 185;
    numArray5[17] = (byte) 172;
    numArray5[9] = (byte) 234;
    numArray5[16 /*0x10*/] = (byte) 73;
    numArray5[20] = (byte) 73;
    numArray5[2] = (byte) 216;
    numArray5[23] = (byte) 58;
    numArray5[8] = (byte) 229;
    byte[] numArray6 = new byte[24]
    {
      (byte) 160 /*0xA0*/,
      (byte) 82,
      (byte) 72,
      (byte) 163,
      (byte) 88,
      (byte) 154,
      (byte) 97,
      (byte) 239,
      (byte) 143,
      (byte) 134,
      (byte) 12,
      (byte) 236,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 96 /*0x60*/,
      (byte) 60,
      (byte) 52,
      (byte) 203,
      (byte) 117,
      (byte) 103,
      (byte) 221,
      (byte) 223,
      (byte) 101,
      (byte) 195
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
