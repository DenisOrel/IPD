// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_8003
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_8003
{
  internal static string ssp_imbase_8004()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 155,
        (byte) 88,
        (byte) 103,
        (byte) 49,
        (byte) 196,
        (byte) 178,
        (byte) 45,
        (byte) 9,
        (byte) 25,
        (byte) 128 /*0x80*/,
        (byte) 138,
        (byte) 113,
        (byte) 193,
        (byte) 132,
        (byte) 201,
        (byte) 140,
        (byte) 172,
        (byte) 233,
        (byte) 209,
        (byte) 54,
        (byte) 149,
        (byte) 238,
        (byte) 81,
        (byte) 149,
        (byte) 51,
        (byte) 6,
        (byte) 151,
        (byte) 160 /*0xA0*/,
        (byte) 81,
        (byte) 155,
        (byte) 181,
        (byte) 210,
        (byte) 96 /*0x60*/,
        (byte) 236,
        (byte) 101,
        (byte) 218,
        (byte) 195,
        (byte) 203,
        (byte) 160 /*0xA0*/,
        (byte) 58,
        (byte) 104
      };
      byte[] numArray3 = new byte[41];
      numArray3[38] = (byte) 167;
      numArray3[30] = (byte) 193;
      numArray3[27] = (byte) 179;
      numArray3[3] = (byte) 168;
      numArray3[35] = (byte) 250;
      numArray3[23] = (byte) 128 /*0x80*/;
      numArray3[6] = (byte) 177;
      numArray3[32 /*0x20*/] = (byte) 18;
      numArray3[7] = (byte) 193;
      numArray3[14] = (byte) 67;
      numArray3[10] = (byte) 237;
      numArray3[5] = (byte) 198;
      numArray3[18] = (byte) 86;
      numArray3[13] = (byte) 164;
      numArray3[9] = (byte) 151;
      numArray3[11] = (byte) 196;
      numArray3[16 /*0x10*/] = (byte) 40;
      numArray3[17] = (byte) 110;
      numArray3[25] = (byte) 120;
      numArray3[15] = (byte) 95;
      numArray3[26] = (byte) 138;
      numArray3[21] = (byte) 103;
      numArray3[20] = (byte) 135;
      numArray3[0] = (byte) 95;
      numArray3[29] = (byte) 248;
      numArray3[22] = (byte) 233;
      numArray3[8] = (byte) 249;
      numArray3[39] = (byte) 248;
      numArray3[28] = (byte) 66;
      numArray3[31 /*0x1F*/] = (byte) 118;
      numArray3[40] = (byte) 58;
      numArray3[1] = (byte) 127 /*0x7F*/;
      numArray3[4] = (byte) 195;
      numArray3[33] = (byte) 248;
      numArray3[34] = (byte) 99;
      numArray3[19] = (byte) 231;
      numArray3[36] = (byte) 27;
      numArray3[24] = (byte) 208 /*0xD0*/;
      numArray3[12] = (byte) 142;
      numArray3[37] = (byte) 120;
      numArray3[2] = (byte) 6;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 200,
      (byte) 122,
      (byte) 72,
      (byte) 241,
      (byte) 184,
      (byte) 211,
      (byte) 240 /*0xF0*/,
      (byte) 205,
      (byte) 226,
      (byte) 30,
      (byte) 218,
      (byte) 66,
      (byte) 167,
      (byte) 249,
      (byte) 211,
      (byte) 154,
      (byte) 52,
      (byte) 57,
      (byte) 24,
      (byte) 79,
      (byte) 37,
      (byte) 164,
      (byte) 191,
      (byte) 72,
      (byte) 95,
      (byte) 228,
      (byte) 120,
      (byte) 201,
      (byte) 119,
      (byte) 193,
      (byte) 220,
      (byte) 243,
      (byte) 128 /*0x80*/,
      (byte) 233,
      (byte) 1,
      (byte) 61,
      (byte) 201,
      (byte) 227,
      (byte) 102,
      (byte) 26,
      (byte) 216
    };
    byte[] numArray6 = new byte[41];
    numArray6[27] = (byte) 114;
    numArray6[1] = (byte) 18;
    numArray6[2] = (byte) 100;
    numArray6[31 /*0x1F*/] = (byte) 246;
    numArray6[22] = (byte) 202;
    numArray6[7] = (byte) 217;
    numArray6[32 /*0x20*/] = (byte) 66;
    numArray6[39] = (byte) 246;
    numArray6[11] = (byte) 114;
    numArray6[0] = (byte) 91;
    numArray6[5] = (byte) 8;
    numArray6[30] = (byte) 229;
    numArray6[12] = (byte) 127 /*0x7F*/;
    numArray6[13] = (byte) 11;
    numArray6[33] = (byte) 17;
    numArray6[15] = (byte) 180;
    numArray6[16 /*0x10*/] = (byte) 33;
    numArray6[40] = (byte) 58;
    numArray6[18] = (byte) 239;
    numArray6[34] = (byte) 211;
    numArray6[29] = (byte) 142;
    numArray6[21] = (byte) 120;
    numArray6[36] = (byte) 131;
    numArray6[23] = (byte) 106;
    numArray6[24] = (byte) 37;
    numArray6[25] = (byte) 157;
    numArray6[26] = (byte) 136;
    numArray6[4] = (byte) 210;
    numArray6[28] = (byte) 176 /*0xB0*/;
    numArray6[19] = (byte) 35;
    numArray6[38] = (byte) 244;
    numArray6[17] = (byte) 12;
    numArray6[6] = (byte) 94;
    numArray6[9] = (byte) 35;
    numArray6[10] = (byte) 165;
    numArray6[35] = (byte) 245;
    numArray6[20] = (byte) 62;
    numArray6[37] = (byte) 171;
    numArray6[14] = (byte) 218;
    numArray6[3] = (byte) 205;
    numArray6[8] = (byte) 213;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_8005()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[5] = (byte) 201;
      numArray2[14] = (byte) 151;
      numArray2[2] = (byte) 122;
      numArray2[3] = (byte) 221;
      numArray2[11] = (byte) 68;
      numArray2[15] = (byte) 98;
      numArray2[0] = (byte) 113;
      numArray2[7] = (byte) 146;
      numArray2[9] = (byte) 177;
      numArray2[1] = (byte) 151;
      numArray2[6] = (byte) 82;
      numArray2[8] = (byte) 191;
      numArray2[10] = (byte) 190;
      numArray2[13] = (byte) 12;
      numArray2[12] = (byte) 55;
      numArray2[4] = (byte) 130;
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[4] = (byte) 243;
      numArray3[12] = (byte) 178;
      numArray3[2] = (byte) 189;
      numArray3[6] = (byte) 7;
      numArray3[1] = (byte) 87;
      numArray3[5] = (byte) 126;
      numArray3[14] = (byte) 148;
      numArray3[10] = (byte) 71;
      numArray3[8] = (byte) 230;
      numArray3[9] = (byte) 85;
      numArray3[3] = byte.MaxValue;
      numArray3[11] = (byte) 41;
      numArray3[13] = (byte) 207;
      numArray3[0] = (byte) 125;
      numArray3[7] = (byte) 148;
      numArray3[15] = (byte) 240 /*0xF0*/;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[7] = (byte) 248;
    numArray5[1] = (byte) 111;
    numArray5[14] = (byte) 243;
    numArray5[3] = (byte) 166;
    numArray5[4] = (byte) 237;
    numArray5[5] = (byte) 38;
    numArray5[9] = (byte) 29;
    numArray5[0] = (byte) 244;
    numArray5[13] = (byte) 30;
    numArray5[10] = (byte) 199;
    numArray5[6] = (byte) 27;
    numArray5[11] = (byte) 130;
    numArray5[12] = (byte) 72;
    numArray5[8] = (byte) 13;
    numArray5[2] = (byte) 128 /*0x80*/;
    numArray5[15] = (byte) 27;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 218,
      (byte) 129,
      (byte) 31 /*0x1F*/,
      (byte) 5,
      (byte) 190,
      (byte) 207,
      (byte) 52,
      (byte) 72,
      (byte) 136,
      (byte) 139,
      (byte) 136,
      (byte) 95,
      (byte) 119,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 124
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_8006()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41];
      numArray2[12] = (byte) 94;
      numArray2[1] = (byte) 195;
      numArray2[9] = (byte) 190;
      numArray2[3] = (byte) 3;
      numArray2[21] = (byte) 7;
      numArray2[20] = (byte) 91;
      numArray2[17] = (byte) 65;
      numArray2[39] = (byte) 97;
      numArray2[37] = (byte) 106;
      numArray2[24] = (byte) 121;
      numArray2[10] = (byte) 245;
      numArray2[11] = (byte) 166;
      numArray2[28] = (byte) 186;
      numArray2[2] = (byte) 135;
      numArray2[14] = (byte) 65;
      numArray2[15] = (byte) 177;
      numArray2[16 /*0x10*/] = (byte) 0;
      numArray2[0] = (byte) 167;
      numArray2[18] = (byte) 196;
      numArray2[34] = (byte) 115;
      numArray2[5] = (byte) 33;
      numArray2[33] = (byte) 151;
      numArray2[7] = (byte) 234;
      numArray2[23] = (byte) 167;
      numArray2[36] = (byte) 14;
      numArray2[40] = (byte) 133;
      numArray2[32 /*0x20*/] = (byte) 127 /*0x7F*/;
      numArray2[27] = (byte) 167;
      numArray2[25] = (byte) 15;
      numArray2[29] = (byte) 170;
      numArray2[30] = (byte) 209;
      numArray2[31 /*0x1F*/] = (byte) 23;
      numArray2[19] = (byte) 104;
      numArray2[26] = (byte) 14;
      numArray2[22] = (byte) 180;
      numArray2[35] = (byte) 9;
      numArray2[4] = (byte) 39;
      numArray2[6] = (byte) 6;
      numArray2[38] = (byte) 247;
      numArray2[8] = (byte) 54;
      numArray2[13] = (byte) 222;
      byte[] numArray3 = new byte[41];
      numArray3[10] = (byte) 177;
      numArray3[1] = (byte) 169;
      numArray3[12] = (byte) 108;
      numArray3[3] = (byte) 46;
      numArray3[4] = (byte) 169;
      numArray3[5] = (byte) 191;
      numArray3[6] = (byte) 23;
      numArray3[29] = (byte) 221;
      numArray3[7] = (byte) 44;
      numArray3[9] = (byte) 52;
      numArray3[39] = (byte) 158;
      numArray3[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
      numArray3[23] = (byte) 48 /*0x30*/;
      numArray3[8] = (byte) 241;
      numArray3[30] = (byte) 67;
      numArray3[38] = (byte) 193;
      numArray3[16 /*0x10*/] = (byte) 240 /*0xF0*/;
      numArray3[40] = (byte) 151;
      numArray3[20] = (byte) 228;
      numArray3[26] = (byte) 191;
      numArray3[32 /*0x20*/] = (byte) 68;
      numArray3[19] = (byte) 34;
      numArray3[21] = (byte) 6;
      numArray3[22] = (byte) 180;
      numArray3[24] = (byte) 238;
      numArray3[25] = (byte) 189;
      numArray3[34] = (byte) 129;
      numArray3[27] = (byte) 10;
      numArray3[28] = (byte) 91;
      numArray3[18] = (byte) 154;
      numArray3[13] = (byte) 108;
      numArray3[17] = (byte) 235;
      numArray3[11] = (byte) 57;
      numArray3[33] = (byte) 232;
      numArray3[14] = (byte) 41;
      numArray3[35] = (byte) 79;
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[37] = (byte) 173;
      numArray3[15] = (byte) 78;
      numArray3[36] = (byte) 111;
      numArray3[2] = (byte) 138;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41];
    numArray5[19] = (byte) 107;
    numArray5[0] = (byte) 181;
    numArray5[2] = (byte) 21;
    numArray5[12] = (byte) 251;
    numArray5[40] = (byte) 84;
    numArray5[15] = (byte) 163;
    numArray5[6] = (byte) 57;
    numArray5[38] = (byte) 151;
    numArray5[8] = (byte) 148;
    numArray5[9] = (byte) 122;
    numArray5[37] = (byte) 151;
    numArray5[14] = (byte) 60;
    numArray5[22] = (byte) 39;
    numArray5[13] = (byte) 8;
    numArray5[29] = (byte) 1;
    numArray5[11] = (byte) 149;
    numArray5[16 /*0x10*/] = (byte) 246;
    numArray5[4] = (byte) 54;
    numArray5[31 /*0x1F*/] = (byte) 9;
    numArray5[21] = (byte) 202;
    numArray5[5] = (byte) 14;
    numArray5[1] = (byte) 107;
    numArray5[17] = (byte) 200;
    numArray5[33] = (byte) 154;
    numArray5[24] = (byte) 76;
    numArray5[18] = (byte) 248;
    numArray5[26] = (byte) 184;
    numArray5[27] = (byte) 188;
    numArray5[28] = (byte) 63 /*0x3F*/;
    numArray5[7] = (byte) 229;
    numArray5[30] = (byte) 184;
    numArray5[3] = (byte) 187;
    numArray5[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    numArray5[10] = (byte) 97;
    numArray5[34] = (byte) 208 /*0xD0*/;
    numArray5[35] = (byte) 186;
    numArray5[36] = (byte) 243;
    numArray5[20] = (byte) 166;
    numArray5[25] = (byte) 213;
    numArray5[39] = (byte) 204;
    numArray5[23] = (byte) 60;
    byte[] numArray6 = new byte[41]
    {
      (byte) 216,
      (byte) 25,
      (byte) 47,
      (byte) 121,
      (byte) 6,
      (byte) 148,
      (byte) 213,
      (byte) 34,
      (byte) 168,
      (byte) 165,
      (byte) 35,
      (byte) 140,
      (byte) 238,
      (byte) 240 /*0xF0*/,
      (byte) 131,
      (byte) 0,
      (byte) 88,
      (byte) 220,
      (byte) 159,
      (byte) 224 /*0xE0*/,
      (byte) 118,
      (byte) 143,
      (byte) 101,
      (byte) 1,
      (byte) 135,
      (byte) 213,
      (byte) 161,
      (byte) 109,
      (byte) 239,
      (byte) 157,
      (byte) 176 /*0xB0*/,
      (byte) 248,
      (byte) 71,
      (byte) 233,
      (byte) 251,
      (byte) 102,
      (byte) 188,
      (byte) 40,
      (byte) 55,
      (byte) 219,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
