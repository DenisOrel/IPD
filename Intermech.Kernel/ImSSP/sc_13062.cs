// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13062
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13062
{
  internal static string ssp_appserver_13063()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[60];
      byte[] numArray2 = new byte[55];
      numArray2[51] = (byte) 16 /*0x10*/;
      numArray2[0] = (byte) 113;
      numArray2[49] = (byte) 60;
      numArray2[5] = (byte) 36;
      numArray2[4] = (byte) 71;
      numArray2[22] = (byte) 13;
      numArray2[38] = (byte) 68;
      numArray2[6] = (byte) 97;
      numArray2[8] = (byte) 187;
      numArray2[1] = (byte) 247;
      numArray2[10] = (byte) 78;
      numArray2[11] = (byte) 214;
      numArray2[12] = (byte) 56;
      numArray2[3] = (byte) 19;
      numArray2[48 /*0x30*/] = (byte) 87;
      numArray2[27] = (byte) 129;
      numArray2[44] = (byte) 77;
      numArray2[17] = (byte) 230;
      numArray2[9] = (byte) 120;
      numArray2[18] = (byte) 164;
      numArray2[20] = (byte) 157;
      numArray2[16 /*0x10*/] = (byte) 98;
      numArray2[34] = (byte) 191;
      numArray2[23] = (byte) 222;
      numArray2[21] = (byte) 125;
      numArray2[52] = (byte) 155;
      numArray2[50] = (byte) 45;
      numArray2[14] = (byte) 252;
      numArray2[28] = (byte) 249;
      numArray2[24] = (byte) 90;
      numArray2[30] = (byte) 55;
      numArray2[31 /*0x1F*/] = (byte) 33;
      numArray2[32 /*0x20*/] = (byte) 195;
      numArray2[33] = (byte) 187;
      numArray2[25] = (byte) 18;
      numArray2[35] = (byte) 175;
      numArray2[36] = (byte) 43;
      numArray2[2] = (byte) 171;
      numArray2[40] = (byte) 3;
      numArray2[39] = (byte) 62;
      numArray2[7] = (byte) 67;
      numArray2[41] = (byte) 122;
      numArray2[42] = (byte) 63 /*0x3F*/;
      numArray2[43] = (byte) 46;
      numArray2[15] = (byte) 131;
      numArray2[13] = (byte) 234;
      numArray2[46] = (byte) 190;
      numArray2[47] = (byte) 183;
      numArray2[26] = (byte) 27;
      numArray2[37] = (byte) 146;
      numArray2[54] = (byte) 30;
      numArray2[19] = (byte) 210;
      numArray2[29] = (byte) 213;
      numArray2[53] = (byte) 163;
      numArray2[45] = (byte) 193;
      byte[] numArray3 = new byte[55];
      numArray3[11] = (byte) 32 /*0x20*/;
      numArray3[1] = (byte) 171;
      numArray3[39] = (byte) 54;
      numArray3[44] = (byte) 238;
      numArray3[4] = (byte) 195;
      numArray3[8] = (byte) 217;
      numArray3[6] = (byte) 210;
      numArray3[7] = (byte) 20;
      numArray3[9] = (byte) 44;
      numArray3[45] = (byte) 98;
      numArray3[12] = (byte) 119;
      numArray3[47] = (byte) 56;
      numArray3[34] = (byte) 224 /*0xE0*/;
      numArray3[51] = (byte) 200;
      numArray3[31 /*0x1F*/] = (byte) 131;
      numArray3[2] = (byte) 145;
      numArray3[16 /*0x10*/] = (byte) 44;
      numArray3[17] = (byte) 134;
      numArray3[18] = (byte) 169;
      numArray3[3] = (byte) 89;
      numArray3[14] = (byte) 159;
      numArray3[21] = (byte) 120;
      numArray3[43] = (byte) 143;
      numArray3[53] = (byte) 54;
      numArray3[20] = (byte) 35;
      numArray3[25] = (byte) 180;
      numArray3[26] = (byte) 9;
      numArray3[27] = (byte) 235;
      numArray3[28] = (byte) 52;
      numArray3[19] = (byte) 32 /*0x20*/;
      numArray3[10] = (byte) 141;
      numArray3[29] = (byte) 229;
      numArray3[15] = (byte) 165;
      numArray3[50] = (byte) 232;
      numArray3[32 /*0x20*/] = (byte) 111;
      numArray3[35] = (byte) 83;
      numArray3[24] = (byte) 141;
      numArray3[37] = (byte) 114;
      numArray3[33] = (byte) 26;
      numArray3[23] = (byte) 28;
      numArray3[49] = (byte) 244;
      numArray3[41] = (byte) 148;
      numArray3[42] = (byte) 24;
      numArray3[0] = (byte) 67;
      numArray3[30] = (byte) 63 /*0x3F*/;
      numArray3[52] = (byte) 77;
      numArray3[5] = (byte) 177;
      numArray3[46] = (byte) 162;
      numArray3[36] = (byte) 9;
      numArray3[48 /*0x30*/] = (byte) 154;
      numArray3[38] = (byte) 222;
      numArray3[40] = (byte) 113;
      numArray3[22] = (byte) 194;
      numArray3[13] = (byte) 118;
      numArray3[54] = (byte) 215;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[5]
      {
        (byte) 203,
        (byte) 52,
        (byte) 208 /*0xD0*/,
        (byte) 206,
        (byte) 152
      };
      byte[] numArray5 = new byte[5]
      {
        (byte) 167,
        (byte) 113,
        (byte) 58,
        (byte) 35,
        (byte) 18
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[60];
    byte[] numArray7 = new byte[55]
    {
      (byte) 85,
      (byte) 168,
      (byte) 60,
      (byte) 75,
      (byte) 205,
      (byte) 217,
      (byte) 61,
      (byte) 102,
      (byte) 159,
      (byte) 64 /*0x40*/,
      (byte) 78,
      (byte) 200,
      (byte) 29,
      (byte) 247,
      (byte) 49,
      (byte) 175,
      (byte) 170,
      (byte) 155,
      (byte) 239,
      (byte) 228,
      (byte) 221,
      (byte) 77,
      (byte) 35,
      (byte) 154,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 85,
      (byte) 133,
      (byte) 92,
      (byte) 194,
      (byte) 211,
      (byte) 240 /*0xF0*/,
      (byte) 40,
      (byte) 115,
      (byte) 35,
      (byte) 17,
      (byte) 37,
      (byte) 224 /*0xE0*/,
      (byte) 181,
      (byte) 16 /*0x10*/,
      (byte) 40,
      (byte) 117,
      (byte) 101,
      (byte) 231,
      (byte) 185,
      (byte) 10,
      (byte) 251,
      (byte) 211,
      (byte) 2,
      (byte) 33,
      (byte) 36,
      (byte) 253,
      (byte) 196,
      (byte) 14,
      (byte) 130
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 65,
      (byte) 219,
      (byte) 240 /*0xF0*/,
      (byte) 81,
      (byte) 43,
      (byte) 72,
      (byte) 184,
      (byte) 71,
      (byte) 214,
      (byte) 121,
      (byte) 198,
      (byte) 248,
      (byte) 209,
      (byte) 62,
      (byte) 145,
      (byte) 53,
      (byte) 117,
      (byte) 68,
      (byte) 144 /*0x90*/,
      (byte) 177,
      (byte) 121,
      (byte) 143,
      (byte) 229,
      (byte) 93,
      (byte) 146,
      (byte) 7,
      (byte) 32 /*0x20*/,
      (byte) 250,
      (byte) 81,
      (byte) 192 /*0xC0*/,
      (byte) 11,
      (byte) 85,
      (byte) 116,
      (byte) 251,
      (byte) 191,
      (byte) 74,
      (byte) 119,
      (byte) 199,
      (byte) 204,
      (byte) 82,
      (byte) 91,
      (byte) 209,
      (byte) 228,
      (byte) 93,
      (byte) 236,
      (byte) 196,
      (byte) 34,
      (byte) 235,
      (byte) 50,
      (byte) 58,
      (byte) 66,
      (byte) 50,
      (byte) 203,
      (byte) 196,
      (byte) 8
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[5]
    {
      (byte) 162,
      (byte) 10,
      (byte) 44,
      (byte) 131,
      (byte) 184
    };
    byte[] numArray10 = new byte[5]
    {
      (byte) 0,
      (byte) 85,
      (byte) 0,
      (byte) 32 /*0x20*/,
      (byte) 0
    };
    numArray10[0] = (byte) 236;
    numArray10[2] = (byte) 17;
    numArray10[4] = (byte) 127 /*0x7F*/;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 5);
    for (int index = 0; index < 5; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13064()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27];
      numArray2[24] = (byte) 107;
      numArray2[8] = (byte) 158;
      numArray2[3] = (byte) 54;
      numArray2[26] = byte.MaxValue;
      numArray2[12] = (byte) 240 /*0xF0*/;
      numArray2[5] = (byte) 129;
      numArray2[6] = (byte) 130;
      numArray2[7] = (byte) 59;
      numArray2[14] = (byte) 140;
      numArray2[0] = (byte) 232;
      numArray2[22] = (byte) 121;
      numArray2[25] = (byte) 80 /*0x50*/;
      numArray2[21] = (byte) 135;
      numArray2[20] = (byte) 107;
      numArray2[9] = (byte) 214;
      numArray2[15] = (byte) 130;
      numArray2[16 /*0x10*/] = (byte) 214;
      numArray2[17] = (byte) 136;
      numArray2[10] = (byte) 44;
      numArray2[19] = (byte) 111;
      numArray2[2] = (byte) 224 /*0xE0*/;
      numArray2[13] = (byte) 27;
      numArray2[1] = (byte) 60;
      numArray2[23] = (byte) 91;
      numArray2[18] = (byte) 140;
      numArray2[4] = (byte) 165;
      numArray2[11] = (byte) 25;
      byte[] numArray3 = new byte[27]
      {
        (byte) 190,
        (byte) 242,
        (byte) 13,
        (byte) 72,
        (byte) 206,
        (byte) 143,
        (byte) 140,
        (byte) 248,
        (byte) 33,
        (byte) 33,
        (byte) 123,
        (byte) 132,
        (byte) 132,
        (byte) 55,
        (byte) 126,
        (byte) 63 /*0x3F*/,
        (byte) 198,
        (byte) 237,
        (byte) 168,
        (byte) 197,
        (byte) 229,
        (byte) 189,
        (byte) 238,
        (byte) 69,
        (byte) 202,
        (byte) 214,
        (byte) 246
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[25] = (byte) 77;
    numArray5[24] = (byte) 71;
    numArray5[10] = (byte) 215;
    numArray5[1] = (byte) 203;
    numArray5[4] = (byte) 160 /*0xA0*/;
    numArray5[20] = (byte) 226;
    numArray5[16 /*0x10*/] = (byte) 138;
    numArray5[12] = (byte) 67;
    numArray5[5] = (byte) 163;
    numArray5[9] = (byte) 198;
    numArray5[8] = (byte) 70;
    numArray5[11] = (byte) 251;
    numArray5[17] = (byte) 57;
    numArray5[13] = (byte) 192 /*0xC0*/;
    numArray5[14] = (byte) 2;
    numArray5[15] = (byte) 0;
    numArray5[0] = (byte) 133;
    numArray5[22] = (byte) 229;
    numArray5[18] = (byte) 156;
    numArray5[7] = (byte) 191;
    numArray5[3] = (byte) 129;
    numArray5[21] = (byte) 41;
    numArray5[2] = (byte) 84;
    numArray5[19] = (byte) 72;
    numArray5[6] = (byte) 67;
    numArray5[23] = (byte) 150;
    numArray5[26] = (byte) 144 /*0x90*/;
    byte[] numArray6 = new byte[27];
    numArray6[21] = (byte) 34;
    numArray6[1] = (byte) 127 /*0x7F*/;
    numArray6[7] = (byte) 227;
    numArray6[3] = (byte) 207;
    numArray6[14] = (byte) 191;
    numArray6[5] = (byte) 180;
    numArray6[26] = (byte) 208 /*0xD0*/;
    numArray6[13] = (byte) 186;
    numArray6[0] = (byte) 219;
    numArray6[9] = (byte) 23;
    numArray6[10] = (byte) 163;
    numArray6[11] = (byte) 188;
    numArray6[2] = (byte) 21;
    numArray6[19] = (byte) 159;
    numArray6[12] = (byte) 3;
    numArray6[16 /*0x10*/] = (byte) 182;
    numArray6[4] = (byte) 52;
    numArray6[18] = (byte) 178;
    numArray6[15] = (byte) 220;
    numArray6[23] = (byte) 94;
    numArray6[20] = (byte) 1;
    numArray6[17] = (byte) 75;
    numArray6[22] = (byte) 138;
    numArray6[6] = (byte) 47;
    numArray6[24] = (byte) 232;
    numArray6[25] = (byte) 218;
    numArray6[8] = (byte) 197;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
