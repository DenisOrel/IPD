// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13024
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13024
{
  private static byte[] sspq = new byte[21]
  {
    (byte) 66,
    (byte) 208 /*0xD0*/,
    (byte) 211,
    (byte) 36,
    (byte) 222,
    (byte) 167,
    (byte) 44,
    (byte) 175,
    (byte) 35,
    (byte) 102,
    (byte) 2,
    (byte) 18,
    (byte) 224 /*0xE0*/,
    (byte) 19,
    (byte) 71,
    (byte) 137,
    (byte) 228,
    (byte) 202,
    (byte) 229,
    (byte) 131,
    (byte) 117
  };
  private static byte[] sspr = new byte[21]
  {
    (byte) 208 /*0xD0*/,
    (byte) 219,
    (byte) 249,
    (byte) 44,
    (byte) 54,
    (byte) 244,
    (byte) 32 /*0x20*/,
    (byte) 94,
    (byte) 228,
    (byte) 60,
    (byte) 165,
    (byte) 113,
    (byte) 6,
    (byte) 60,
    (byte) 80 /*0x50*/,
    (byte) 248,
    (byte) 73,
    (byte) 39,
    (byte) 123,
    (byte) 148,
    (byte) 108
  };

  internal static string ssp_appserver_13025()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[58];
      byte[] numArray2 = new byte[55];
      numArray2[40] = (byte) 216;
      numArray2[22] = (byte) 210;
      numArray2[2] = (byte) 208 /*0xD0*/;
      numArray2[47] = (byte) 41;
      numArray2[44] = (byte) 207;
      numArray2[17] = (byte) 137;
      numArray2[27] = (byte) 32 /*0x20*/;
      numArray2[35] = (byte) 206;
      numArray2[37] = (byte) 189;
      numArray2[52] = (byte) 136;
      numArray2[7] = (byte) 14;
      numArray2[3] = (byte) 206;
      numArray2[12] = (byte) 84;
      numArray2[14] = (byte) 131;
      numArray2[13] = (byte) 55;
      numArray2[8] = (byte) 221;
      numArray2[16 /*0x10*/] = (byte) 148;
      numArray2[48 /*0x30*/] = (byte) 236;
      numArray2[0] = (byte) 105;
      numArray2[19] = (byte) 67;
      numArray2[20] = (byte) 68;
      numArray2[21] = (byte) 208 /*0xD0*/;
      numArray2[15] = (byte) 136;
      numArray2[31 /*0x1F*/] = (byte) 210;
      numArray2[24] = (byte) 151;
      numArray2[33] = (byte) 125;
      numArray2[26] = (byte) 205;
      numArray2[4] = (byte) 129;
      numArray2[5] = (byte) 78;
      numArray2[29] = (byte) 245;
      numArray2[30] = (byte) 59;
      numArray2[1] = (byte) 11;
      numArray2[32 /*0x20*/] = (byte) 111;
      numArray2[11] = (byte) 160 /*0xA0*/;
      numArray2[34] = (byte) 241;
      numArray2[10] = (byte) 6;
      numArray2[36] = (byte) 91;
      numArray2[28] = (byte) 11;
      numArray2[38] = (byte) 108;
      numArray2[49] = (byte) 181;
      numArray2[39] = (byte) 232;
      numArray2[41] = (byte) 160 /*0xA0*/;
      numArray2[42] = (byte) 130;
      numArray2[43] = (byte) 181;
      numArray2[6] = (byte) 244;
      numArray2[18] = (byte) 64 /*0x40*/;
      numArray2[46] = (byte) 47;
      numArray2[9] = (byte) 123;
      numArray2[23] = (byte) 98;
      numArray2[25] = (byte) 117;
      numArray2[50] = (byte) 212;
      numArray2[51] = (byte) 220;
      numArray2[45] = (byte) 71;
      numArray2[53] = (byte) 163;
      numArray2[54] = (byte) 249;
      byte[] numArray3 = new byte[55];
      numArray3[2] = (byte) 149;
      numArray3[37] = (byte) 53;
      numArray3[34] = (byte) 20;
      numArray3[28] = (byte) 11;
      numArray3[4] = (byte) 26;
      numArray3[5] = (byte) 174;
      numArray3[6] = (byte) 245;
      numArray3[35] = (byte) 130;
      numArray3[48 /*0x30*/] = (byte) 160 /*0xA0*/;
      numArray3[13] = (byte) 97;
      numArray3[10] = (byte) 111;
      numArray3[7] = (byte) 115;
      numArray3[39] = (byte) 211;
      numArray3[29] = (byte) 121;
      numArray3[31 /*0x1F*/] = (byte) 98;
      numArray3[15] = (byte) 209;
      numArray3[16 /*0x10*/] = (byte) 248;
      numArray3[17] = (byte) 141;
      numArray3[18] = (byte) 49;
      numArray3[53] = (byte) 128 /*0x80*/;
      numArray3[20] = (byte) 242;
      numArray3[1] = (byte) 122;
      numArray3[22] = byte.MaxValue;
      numArray3[33] = (byte) 12;
      numArray3[24] = (byte) 199;
      numArray3[25] = (byte) 61;
      numArray3[21] = (byte) 154;
      numArray3[27] = (byte) 11;
      numArray3[12] = (byte) 84;
      numArray3[0] = (byte) 191;
      numArray3[14] = (byte) 152;
      numArray3[8] = byte.MaxValue;
      numArray3[45] = (byte) 18;
      numArray3[11] = (byte) 98;
      numArray3[42] = (byte) 152;
      numArray3[3] = (byte) 37;
      numArray3[36] = (byte) 40;
      numArray3[32 /*0x20*/] = (byte) 140;
      numArray3[38] = (byte) 55;
      numArray3[19] = (byte) 58;
      numArray3[9] = (byte) 74;
      numArray3[51] = (byte) 121;
      numArray3[41] = (byte) 177;
      numArray3[43] = (byte) 164;
      numArray3[44] = (byte) 224 /*0xE0*/;
      numArray3[30] = (byte) 187;
      numArray3[46] = (byte) 136;
      numArray3[47] = (byte) 173;
      numArray3[26] = (byte) 241;
      numArray3[49] = (byte) 27;
      numArray3[50] = (byte) 161;
      numArray3[23] = (byte) 32 /*0x20*/;
      numArray3[52] = (byte) 31 /*0x1F*/;
      numArray3[40] = (byte) 164;
      numArray3[54] = (byte) 206;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[3]
      {
        (byte) 227,
        (byte) 174,
        (byte) 217
      };
      byte[] numArray5 = new byte[3]
      {
        (byte) 60,
        (byte) 106,
        (byte) 102
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[21];
      byte[] response = new byte[21];
      Array.Copy((Array) sc_13024.sspq, 0, (Array) numArray6, 0, 21);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13024.sspr, 0, (Array) numArray6, 0, 21);
      for (int index = 0; index < numArray6.Length; ++index)
      {
        if ((int) numArray6[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray7 = new byte[58];
    byte[] numArray8 = new byte[55]
    {
      (byte) 156,
      (byte) 218,
      (byte) 193,
      (byte) 220,
      (byte) 120,
      (byte) 2,
      (byte) 46,
      (byte) 241,
      (byte) 192 /*0xC0*/,
      (byte) 110,
      (byte) 192 /*0xC0*/,
      (byte) 19,
      (byte) 48 /*0x30*/,
      (byte) 80 /*0x50*/,
      (byte) 223,
      (byte) 220,
      (byte) 227,
      (byte) 23,
      (byte) 111,
      (byte) 28,
      (byte) 254,
      (byte) 223,
      (byte) 216,
      (byte) 206,
      (byte) 62,
      (byte) 181,
      (byte) 98,
      (byte) 156,
      (byte) 36,
      (byte) 244,
      (byte) 81,
      (byte) 190,
      (byte) 91,
      (byte) 45,
      (byte) 165,
      (byte) 246,
      (byte) 18,
      (byte) 72,
      (byte) 102,
      (byte) 165,
      (byte) 254,
      (byte) 190,
      (byte) 98,
      (byte) 248,
      (byte) 66,
      byte.MaxValue,
      (byte) 176 /*0xB0*/,
      (byte) 194,
      (byte) 103,
      (byte) 173,
      (byte) 1,
      (byte) 179,
      (byte) 117,
      (byte) 102,
      (byte) 119
    };
    byte[] numArray9 = new byte[55];
    numArray9[54] = (byte) 217;
    numArray9[1] = (byte) 172;
    numArray9[12] = (byte) 215;
    numArray9[3] = (byte) 209;
    numArray9[22] = (byte) 169;
    numArray9[35] = (byte) 135;
    numArray9[6] = (byte) 197;
    numArray9[19] = (byte) 132;
    numArray9[8] = (byte) 245;
    numArray9[9] = (byte) 6;
    numArray9[15] = (byte) 127 /*0x7F*/;
    numArray9[11] = (byte) 228;
    numArray9[10] = (byte) 95;
    numArray9[13] = (byte) 56;
    numArray9[31 /*0x1F*/] = (byte) 123;
    numArray9[24] = (byte) 10;
    numArray9[16 /*0x10*/] = (byte) 66;
    numArray9[49] = byte.MaxValue;
    numArray9[0] = (byte) 235;
    numArray9[23] = (byte) 49;
    numArray9[20] = (byte) 54;
    numArray9[37] = (byte) 102;
    numArray9[7] = (byte) 68;
    numArray9[4] = (byte) 183;
    numArray9[18] = (byte) 219;
    numArray9[39] = (byte) 88;
    numArray9[26] = (byte) 212;
    numArray9[27] = (byte) 214;
    numArray9[28] = (byte) 92;
    numArray9[42] = (byte) 202;
    numArray9[34] = (byte) 192 /*0xC0*/;
    numArray9[33] = (byte) 205;
    numArray9[14] = (byte) 141;
    numArray9[43] = (byte) 154;
    numArray9[41] = (byte) 172;
    numArray9[52] = (byte) 234;
    numArray9[36] = (byte) 8;
    numArray9[30] = (byte) 18;
    numArray9[47] = (byte) 46;
    numArray9[46] = (byte) 64 /*0x40*/;
    numArray9[40] = (byte) 165;
    numArray9[21] = (byte) 222;
    numArray9[53] = (byte) 100;
    numArray9[17] = (byte) 113;
    numArray9[44] = (byte) 146;
    numArray9[45] = (byte) 110;
    numArray9[2] = (byte) 23;
    numArray9[5] = (byte) 251;
    numArray9[48 /*0x30*/] = (byte) 174;
    numArray9[38] = (byte) 122;
    numArray9[50] = (byte) 216;
    numArray9[51] = (byte) 5;
    numArray9[29] = (byte) 123;
    numArray9[25] = (byte) 222;
    numArray9[32 /*0x20*/] = (byte) 178;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[3]
    {
      (byte) 0,
      (byte) 93,
      (byte) 0
    };
    numArray10[0] = (byte) 101;
    numArray10[2] = (byte) 190;
    byte[] numArray11 = new byte[3]
    {
      (byte) 227,
      (byte) 0,
      (byte) 56
    };
    numArray11[1] = (byte) 69;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 3);
    for (int index = 0; index < 3; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13026()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55]
      {
        (byte) 169,
        (byte) 240 /*0xF0*/,
        (byte) 13,
        (byte) 197,
        (byte) 176 /*0xB0*/,
        (byte) 78,
        (byte) 229,
        (byte) 5,
        (byte) 76,
        (byte) 248,
        (byte) 87,
        (byte) 135,
        (byte) 182,
        (byte) 147,
        (byte) 140,
        (byte) 155,
        (byte) 157,
        (byte) 171,
        (byte) 126,
        (byte) 167,
        (byte) 211,
        (byte) 186,
        (byte) 76,
        (byte) 228,
        (byte) 3,
        (byte) 17,
        (byte) 155,
        (byte) 191,
        (byte) 92,
        (byte) 134,
        (byte) 131,
        (byte) 97,
        (byte) 85,
        (byte) 138,
        (byte) 58,
        (byte) 118,
        (byte) 244,
        (byte) 229,
        (byte) 152,
        (byte) 233,
        (byte) 91,
        (byte) 27,
        (byte) 25,
        (byte) 100,
        (byte) 102,
        (byte) 121,
        (byte) 165,
        (byte) 193,
        (byte) 75,
        (byte) 227,
        (byte) 27,
        (byte) 23,
        (byte) 205,
        (byte) 26,
        (byte) 32 /*0x20*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 181,
        (byte) 149,
        (byte) 119,
        (byte) 8,
        (byte) 35,
        (byte) 83,
        (byte) 193,
        (byte) 249,
        (byte) 204,
        (byte) 167,
        (byte) 88,
        (byte) 106,
        (byte) 119,
        (byte) 3,
        (byte) 63 /*0x3F*/,
        (byte) 201,
        (byte) 166,
        (byte) 110,
        (byte) 164,
        (byte) 95,
        (byte) 144 /*0x90*/,
        (byte) 94,
        (byte) 152,
        (byte) 99,
        (byte) 133,
        (byte) 51,
        (byte) 99,
        (byte) 73,
        (byte) 82,
        (byte) 161,
        (byte) 183,
        (byte) 91,
        (byte) 153,
        (byte) 18,
        (byte) 197,
        (byte) 191,
        (byte) 236,
        (byte) 4,
        (byte) 213,
        (byte) 119,
        (byte) 212,
        (byte) 14,
        (byte) 157,
        (byte) 90,
        (byte) 84,
        (byte) 16 /*0x10*/,
        (byte) 80 /*0x50*/,
        (byte) 19,
        (byte) 213,
        (byte) 201,
        (byte) 235,
        (byte) 165,
        (byte) 233,
        (byte) 111,
        (byte) 206
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18]
      {
        (byte) 40,
        (byte) 193,
        (byte) 159,
        (byte) 178,
        (byte) 37,
        (byte) 169,
        (byte) 197,
        (byte) 117,
        (byte) 245,
        (byte) 49,
        (byte) 43,
        (byte) 89,
        (byte) 22,
        (byte) 166,
        (byte) 184,
        (byte) 19,
        (byte) 149,
        (byte) 76
      };
      byte[] numArray5 = new byte[18];
      numArray5[14] = (byte) 104;
      numArray5[4] = (byte) 178;
      numArray5[9] = (byte) 232;
      numArray5[10] = (byte) 9;
      numArray5[1] = (byte) 187;
      numArray5[5] = (byte) 125;
      numArray5[6] = (byte) 12;
      numArray5[7] = (byte) 120;
      numArray5[8] = (byte) 65;
      numArray5[16 /*0x10*/] = (byte) 132;
      numArray5[3] = (byte) 183;
      numArray5[11] = (byte) 251;
      numArray5[12] = (byte) 139;
      numArray5[15] = (byte) 109;
      numArray5[13] = (byte) 211;
      numArray5[17] = (byte) 38;
      numArray5[2] = (byte) 245;
      numArray5[0] = (byte) 27;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55];
    numArray7[28] = (byte) 67;
    numArray7[1] = (byte) 83;
    numArray7[2] = (byte) 239;
    numArray7[38] = (byte) 66;
    numArray7[47] = (byte) 2;
    numArray7[26] = (byte) 193;
    numArray7[6] = (byte) 104;
    numArray7[7] = (byte) 52;
    numArray7[11] = (byte) 56;
    numArray7[5] = (byte) 122;
    numArray7[10] = (byte) 4;
    numArray7[18] = (byte) 52;
    numArray7[20] = (byte) 177;
    numArray7[32 /*0x20*/] = (byte) 94;
    numArray7[40] = (byte) 114;
    numArray7[15] = (byte) 187;
    numArray7[46] = (byte) 109;
    numArray7[17] = (byte) 55;
    numArray7[21] = (byte) 47;
    numArray7[41] = (byte) 63 /*0x3F*/;
    numArray7[45] = (byte) 91;
    numArray7[29] = (byte) 218;
    numArray7[22] = (byte) 90;
    numArray7[23] = (byte) 10;
    numArray7[24] = (byte) 98;
    numArray7[25] = (byte) 246;
    numArray7[34] = (byte) 69;
    numArray7[9] = (byte) 98;
    numArray7[13] = (byte) 104;
    numArray7[36] = (byte) 248;
    numArray7[4] = (byte) 71;
    numArray7[54] = (byte) 131;
    numArray7[30] = (byte) 149;
    numArray7[8] = (byte) 60;
    numArray7[49] = (byte) 23;
    numArray7[35] = (byte) 76;
    numArray7[0] = (byte) 114;
    numArray7[37] = (byte) 12;
    numArray7[39] = (byte) 240 /*0xF0*/;
    numArray7[44] = (byte) 102;
    numArray7[27] = (byte) 14;
    numArray7[12] = (byte) 206;
    numArray7[42] = (byte) 60;
    numArray7[43] = (byte) 8;
    numArray7[14] = (byte) 78;
    numArray7[16 /*0x10*/] = (byte) 171;
    numArray7[31 /*0x1F*/] = (byte) 26;
    numArray7[33] = (byte) 22;
    numArray7[48 /*0x30*/] = (byte) 252;
    numArray7[3] = (byte) 112 /*0x70*/;
    numArray7[50] = (byte) 32 /*0x20*/;
    numArray7[51] = (byte) 236;
    numArray7[52] = (byte) 196;
    numArray7[53] = (byte) 94;
    numArray7[19] = (byte) 216;
    byte[] numArray8 = new byte[55];
    numArray8[12] = (byte) 166;
    numArray8[38] = (byte) 61;
    numArray8[2] = (byte) 107;
    numArray8[47] = (byte) 45;
    numArray8[0] = (byte) 0;
    numArray8[5] = (byte) 5;
    numArray8[6] = (byte) 159;
    numArray8[17] = (byte) 163;
    numArray8[48 /*0x30*/] = (byte) 30;
    numArray8[29] = (byte) 46;
    numArray8[10] = (byte) 32 /*0x20*/;
    numArray8[21] = (byte) 76;
    numArray8[22] = (byte) 105;
    numArray8[16 /*0x10*/] = (byte) 36;
    numArray8[7] = (byte) 156;
    numArray8[15] = (byte) 248;
    numArray8[8] = (byte) 63 /*0x3F*/;
    numArray8[13] = (byte) 225;
    numArray8[19] = (byte) 10;
    numArray8[18] = (byte) 110;
    numArray8[35] = (byte) 122;
    numArray8[4] = (byte) 236;
    numArray8[40] = (byte) 106;
    numArray8[23] = (byte) 40;
    numArray8[24] = (byte) 149;
    numArray8[37] = (byte) 143;
    numArray8[9] = (byte) 91;
    numArray8[27] = (byte) 58;
    numArray8[28] = (byte) 221;
    numArray8[1] = (byte) 186;
    numArray8[30] = (byte) 253;
    numArray8[31 /*0x1F*/] = (byte) 132;
    numArray8[32 /*0x20*/] = (byte) 128 /*0x80*/;
    numArray8[49] = (byte) 39;
    numArray8[34] = (byte) 229;
    numArray8[51] = (byte) 57;
    numArray8[36] = (byte) 97;
    numArray8[45] = (byte) 222;
    numArray8[41] = (byte) 12;
    numArray8[39] = (byte) 236;
    numArray8[33] = (byte) 106;
    numArray8[26] = (byte) 179;
    numArray8[42] = (byte) 106;
    numArray8[43] = (byte) 73;
    numArray8[44] = (byte) 2;
    numArray8[25] = (byte) 168;
    numArray8[46] = (byte) 46;
    numArray8[11] = (byte) 3;
    numArray8[3] = (byte) 72;
    numArray8[20] = (byte) 58;
    numArray8[50] = (byte) 169;
    numArray8[14] = (byte) 72;
    numArray8[52] = (byte) 102;
    numArray8[53] = (byte) 184;
    numArray8[54] = (byte) 204;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18]
    {
      (byte) 27,
      (byte) 129,
      (byte) 243,
      (byte) 25,
      (byte) 48 /*0x30*/,
      (byte) 237,
      (byte) 113,
      (byte) 116,
      (byte) 189,
      (byte) 210,
      (byte) 161,
      (byte) 106,
      (byte) 2,
      (byte) 221,
      (byte) 168,
      (byte) 215,
      (byte) 204,
      (byte) 124
    };
    byte[] numArray10 = new byte[18];
    numArray10[15] = (byte) 35;
    numArray10[17] = (byte) 2;
    numArray10[10] = (byte) 233;
    numArray10[3] = (byte) 127 /*0x7F*/;
    numArray10[4] = (byte) 48 /*0x30*/;
    numArray10[5] = (byte) 147;
    numArray10[6] = (byte) 42;
    numArray10[0] = (byte) 210;
    numArray10[8] = (byte) 155;
    numArray10[1] = (byte) 218;
    numArray10[16 /*0x10*/] = (byte) 84;
    numArray10[11] = (byte) 169;
    numArray10[12] = (byte) 28;
    numArray10[7] = (byte) 33;
    numArray10[14] = (byte) 68;
    numArray10[13] = (byte) 89;
    numArray10[2] = (byte) 66;
    numArray10[9] = (byte) 74;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13027()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[168];
      byte[] numArray2 = new byte[55]
      {
        (byte) 191,
        (byte) 161,
        (byte) 44,
        (byte) 213,
        (byte) 239,
        (byte) 85,
        (byte) 73,
        (byte) 199,
        (byte) 57,
        (byte) 30,
        (byte) 169,
        (byte) 47,
        (byte) 81,
        (byte) 78,
        (byte) 142,
        (byte) 224 /*0xE0*/,
        (byte) 163,
        (byte) 177,
        (byte) 249,
        (byte) 103,
        (byte) 187,
        (byte) 29,
        (byte) 185,
        (byte) 93,
        (byte) 118,
        (byte) 12,
        (byte) 237,
        (byte) 232,
        (byte) 39,
        (byte) 51,
        (byte) 160 /*0xA0*/,
        (byte) 166,
        (byte) 219,
        byte.MaxValue,
        (byte) 40,
        (byte) 96 /*0x60*/,
        (byte) 15,
        (byte) 9,
        (byte) 82,
        (byte) 201,
        (byte) 127 /*0x7F*/,
        (byte) 106,
        (byte) 26,
        (byte) 156,
        (byte) 182,
        (byte) 44,
        (byte) 67,
        (byte) 47,
        (byte) 140,
        (byte) 213,
        (byte) 210,
        (byte) 133,
        (byte) 44,
        (byte) 56,
        (byte) 223
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 78,
        (byte) 14,
        (byte) 130,
        (byte) 157,
        (byte) 238,
        (byte) 24,
        (byte) 24,
        (byte) 148,
        (byte) 234,
        (byte) 85,
        (byte) 41,
        (byte) 15,
        (byte) 6,
        (byte) 189,
        (byte) 54,
        (byte) 200,
        (byte) 127 /*0x7F*/,
        (byte) 89,
        (byte) 86,
        (byte) 228,
        (byte) 252,
        (byte) 194,
        (byte) 56,
        (byte) 172,
        (byte) 54,
        (byte) 7,
        (byte) 170,
        (byte) 137,
        (byte) 200,
        (byte) 104,
        (byte) 218,
        (byte) 11,
        (byte) 13,
        (byte) 184,
        (byte) 193,
        (byte) 152,
        (byte) 56,
        (byte) 207,
        (byte) 185,
        (byte) 249,
        (byte) 87,
        (byte) 89,
        (byte) 177,
        (byte) 180,
        (byte) 237,
        (byte) 254,
        (byte) 9,
        (byte) 46,
        (byte) 231,
        (byte) 23,
        (byte) 73,
        (byte) 147,
        (byte) 151,
        (byte) 239,
        (byte) 213
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[9] = (byte) 83;
      numArray4[20] = (byte) 29;
      numArray4[2] = (byte) 227;
      numArray4[3] = (byte) 104;
      numArray4[50] = (byte) 251;
      numArray4[5] = (byte) 160 /*0xA0*/;
      numArray4[51] = (byte) 81;
      numArray4[1] = (byte) 94;
      numArray4[8] = (byte) 3;
      numArray4[19] = (byte) 3;
      numArray4[10] = (byte) 82;
      numArray4[11] = (byte) 37;
      numArray4[12] = (byte) 46;
      numArray4[15] = (byte) 95;
      numArray4[42] = (byte) 178;
      numArray4[21] = (byte) 167;
      numArray4[16 /*0x10*/] = (byte) 48 /*0x30*/;
      numArray4[17] = (byte) 231;
      numArray4[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
      numArray4[18] = (byte) 81;
      numArray4[47] = (byte) 5;
      numArray4[24] = (byte) 189;
      numArray4[22] = (byte) 73;
      numArray4[23] = (byte) 200;
      numArray4[48 /*0x30*/] = (byte) 56;
      numArray4[25] = (byte) 92;
      numArray4[26] = (byte) 81;
      numArray4[14] = (byte) 153;
      numArray4[28] = (byte) 26;
      numArray4[29] = (byte) 121;
      numArray4[30] = (byte) 162;
      numArray4[37] = (byte) 96 /*0x60*/;
      numArray4[32 /*0x20*/] = (byte) 134;
      numArray4[44] = (byte) 241;
      numArray4[13] = (byte) 208 /*0xD0*/;
      numArray4[35] = (byte) 170;
      numArray4[36] = (byte) 110;
      numArray4[39] = (byte) 201;
      numArray4[38] = (byte) 29;
      numArray4[0] = (byte) 206;
      numArray4[40] = (byte) 19;
      numArray4[41] = (byte) 30;
      numArray4[27] = (byte) 96 /*0x60*/;
      numArray4[6] = (byte) 6;
      numArray4[4] = (byte) 97;
      numArray4[45] = (byte) 9;
      numArray4[46] = (byte) 93;
      numArray4[33] = (byte) 121;
      numArray4[43] = (byte) 217;
      numArray4[7] = (byte) 126;
      numArray4[52] = (byte) 142;
      numArray4[49] = (byte) 37;
      numArray4[34] = (byte) 64 /*0x40*/;
      numArray4[53] = (byte) 157;
      numArray4[54] = (byte) 202;
      byte[] numArray5 = new byte[55]
      {
        (byte) 171,
        (byte) 76,
        (byte) 45,
        (byte) 163,
        (byte) 6,
        (byte) 49,
        (byte) 90,
        (byte) 87,
        (byte) 31 /*0x1F*/,
        (byte) 87,
        (byte) 74,
        byte.MaxValue,
        (byte) 210,
        (byte) 64 /*0x40*/,
        (byte) 100,
        (byte) 204,
        (byte) 7,
        (byte) 16 /*0x10*/,
        (byte) 157,
        (byte) 137,
        (byte) 77,
        (byte) 87,
        (byte) 82,
        (byte) 247,
        (byte) 152,
        (byte) 121,
        (byte) 15,
        (byte) 106,
        (byte) 158,
        (byte) 18,
        (byte) 235,
        (byte) 163,
        (byte) 43,
        (byte) 120,
        (byte) 172,
        (byte) 102,
        (byte) 252,
        (byte) 126,
        (byte) 233,
        (byte) 54,
        (byte) 166,
        (byte) 200,
        (byte) 126,
        (byte) 209,
        (byte) 142,
        (byte) 81,
        (byte) 26,
        (byte) 238,
        (byte) 210,
        (byte) 223,
        (byte) 21,
        (byte) 173,
        (byte) 57,
        (byte) 71,
        (byte) 169
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[45] = byte.MaxValue;
      numArray6[13] = (byte) 157;
      numArray6[2] = (byte) 7;
      numArray6[20] = (byte) 70;
      numArray6[29] = (byte) 138;
      numArray6[16 /*0x10*/] = (byte) 12;
      numArray6[31 /*0x1F*/] = (byte) 227;
      numArray6[1] = (byte) 248;
      numArray6[8] = (byte) 161;
      numArray6[15] = (byte) 177;
      numArray6[10] = (byte) 6;
      numArray6[24] = (byte) 215;
      numArray6[12] = (byte) 89;
      numArray6[35] = (byte) 188;
      numArray6[14] = (byte) 26;
      numArray6[19] = (byte) 203;
      numArray6[46] = (byte) 240 /*0xF0*/;
      numArray6[17] = (byte) 130;
      numArray6[50] = (byte) 250;
      numArray6[43] = (byte) 180;
      numArray6[38] = (byte) 239;
      numArray6[21] = (byte) 23;
      numArray6[40] = (byte) 30;
      numArray6[23] = (byte) 85;
      numArray6[37] = (byte) 121;
      numArray6[25] = (byte) 222;
      numArray6[26] = (byte) 192 /*0xC0*/;
      numArray6[42] = (byte) 29;
      numArray6[28] = (byte) 201;
      numArray6[30] = (byte) 59;
      numArray6[6] = (byte) 11;
      numArray6[53] = (byte) 102;
      numArray6[32 /*0x20*/] = (byte) 231;
      numArray6[27] = (byte) 82;
      numArray6[34] = (byte) 14;
      numArray6[7] = (byte) 94;
      numArray6[36] = (byte) 16 /*0x10*/;
      numArray6[33] = (byte) 240 /*0xF0*/;
      numArray6[48 /*0x30*/] = (byte) 16 /*0x10*/;
      numArray6[9] = (byte) 181;
      numArray6[5] = (byte) 110;
      numArray6[49] = (byte) 215;
      numArray6[18] = (byte) 68;
      numArray6[22] = (byte) 72;
      numArray6[44] = (byte) 162;
      numArray6[11] = (byte) 118;
      numArray6[0] = (byte) 68;
      numArray6[47] = (byte) 150;
      numArray6[39] = (byte) 76;
      numArray6[3] = (byte) 1;
      numArray6[41] = (byte) 202;
      numArray6[51] = (byte) 42;
      numArray6[52] = (byte) 134;
      numArray6[4] = (byte) 125;
      numArray6[54] = (byte) 94;
      byte[] numArray7 = new byte[55]
      {
        (byte) 94,
        (byte) 206,
        (byte) 169,
        (byte) 76,
        (byte) 45,
        (byte) 135,
        (byte) 87,
        (byte) 21,
        (byte) 114,
        (byte) 97,
        (byte) 195,
        (byte) 95,
        (byte) 25,
        (byte) 91,
        (byte) 63 /*0x3F*/,
        (byte) 82,
        (byte) 64 /*0x40*/,
        (byte) 123,
        (byte) 250,
        (byte) 119,
        (byte) 206,
        (byte) 26,
        (byte) 216,
        (byte) 237,
        (byte) 86,
        (byte) 1,
        (byte) 194,
        (byte) 91,
        (byte) 27,
        (byte) 116,
        (byte) 156,
        (byte) 130,
        (byte) 54,
        (byte) 30,
        (byte) 0,
        (byte) 59,
        (byte) 66,
        (byte) 223,
        (byte) 147,
        (byte) 63 /*0x3F*/,
        (byte) 64 /*0x40*/,
        (byte) 31 /*0x1F*/,
        (byte) 253,
        (byte) 65,
        (byte) 154,
        (byte) 46,
        (byte) 243,
        (byte) 61,
        (byte) 237,
        (byte) 225,
        (byte) 196,
        (byte) 46,
        (byte) 99,
        (byte) 10,
        (byte) 232
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[3]
      {
        (byte) 254,
        (byte) 82,
        (byte) 178
      };
      byte[] numArray9 = new byte[3]
      {
        (byte) 102,
        (byte) 95,
        (byte) 126
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[168];
    byte[] numArray11 = new byte[55];
    numArray11[28] = (byte) 95;
    numArray11[1] = (byte) 105;
    numArray11[38] = (byte) 176 /*0xB0*/;
    numArray11[26] = (byte) 175;
    numArray11[4] = (byte) 215;
    numArray11[42] = (byte) 37;
    numArray11[40] = (byte) 98;
    numArray11[0] = (byte) 136;
    numArray11[8] = (byte) 66;
    numArray11[23] = (byte) 206;
    numArray11[10] = (byte) 89;
    numArray11[48 /*0x30*/] = (byte) 195;
    numArray11[12] = (byte) 71;
    numArray11[52] = (byte) 99;
    numArray11[14] = (byte) 88;
    numArray11[3] = (byte) 13;
    numArray11[16 /*0x10*/] = (byte) 103;
    numArray11[17] = (byte) 101;
    numArray11[19] = (byte) 142;
    numArray11[9] = (byte) 66;
    numArray11[20] = (byte) 187;
    numArray11[43] = (byte) 85;
    numArray11[2] = (byte) 197;
    numArray11[22] = (byte) 121;
    numArray11[39] = (byte) 90;
    numArray11[50] = (byte) 234;
    numArray11[6] = (byte) 80 /*0x50*/;
    numArray11[35] = (byte) 209;
    numArray11[31 /*0x1F*/] = (byte) 24;
    numArray11[29] = (byte) 253;
    numArray11[30] = (byte) 157;
    numArray11[11] = (byte) 71;
    numArray11[32 /*0x20*/] = (byte) 239;
    numArray11[15] = (byte) 51;
    numArray11[34] = (byte) 125;
    numArray11[25] = (byte) 175;
    numArray11[33] = (byte) 145;
    numArray11[37] = (byte) 98;
    numArray11[5] = (byte) 173;
    numArray11[54] = (byte) 231;
    numArray11[18] = (byte) 33;
    numArray11[41] = (byte) 244;
    numArray11[24] = (byte) 199;
    numArray11[21] = (byte) 38;
    numArray11[44] = (byte) 52;
    numArray11[45] = (byte) 114;
    numArray11[47] = (byte) 60;
    numArray11[51] = (byte) 76;
    numArray11[27] = (byte) 240 /*0xF0*/;
    numArray11[49] = (byte) 203;
    numArray11[7] = (byte) 65;
    numArray11[13] = (byte) 245;
    numArray11[53] = (byte) 231;
    numArray11[36] = (byte) 186;
    numArray11[46] = (byte) 155;
    byte[] numArray12 = new byte[55]
    {
      (byte) 30,
      (byte) 200,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 167,
      (byte) 119,
      (byte) 65,
      (byte) 47,
      (byte) 132,
      (byte) 182,
      (byte) 20,
      (byte) 78,
      (byte) 110,
      (byte) 94,
      (byte) 59,
      (byte) 140,
      (byte) 59,
      (byte) 222,
      (byte) 57,
      (byte) 104,
      (byte) 192 /*0xC0*/,
      (byte) 39,
      (byte) 155,
      (byte) 199,
      (byte) 39,
      (byte) 210,
      (byte) 164,
      (byte) 104,
      (byte) 120,
      (byte) 55,
      (byte) 205,
      (byte) 96 /*0x60*/,
      (byte) 117,
      (byte) 32 /*0x20*/,
      (byte) 228,
      (byte) 229,
      (byte) 45,
      (byte) 75,
      (byte) 151,
      (byte) 244,
      (byte) 58,
      (byte) 218,
      (byte) 128 /*0x80*/,
      (byte) 224 /*0xE0*/,
      (byte) 215,
      (byte) 212,
      (byte) 178,
      (byte) 114,
      (byte) 51,
      (byte) 147,
      (byte) 158,
      (byte) 102,
      (byte) 11,
      (byte) 191,
      (byte) 181
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[14] = (byte) 232;
    numArray13[29] = (byte) 25;
    numArray13[2] = (byte) 19;
    numArray13[22] = (byte) 58;
    numArray13[4] = (byte) 51;
    numArray13[33] = (byte) 21;
    numArray13[53] = (byte) 64 /*0x40*/;
    numArray13[6] = (byte) 51;
    numArray13[42] = (byte) 103;
    numArray13[9] = (byte) 79;
    numArray13[10] = (byte) 13;
    numArray13[11] = (byte) 92;
    numArray13[52] = (byte) 109;
    numArray13[13] = (byte) 180;
    numArray13[1] = (byte) 241;
    numArray13[15] = (byte) 44;
    numArray13[16 /*0x10*/] = (byte) 106;
    numArray13[17] = (byte) 42;
    numArray13[18] = (byte) 180;
    numArray13[7] = (byte) 83;
    numArray13[39] = (byte) 196;
    numArray13[21] = (byte) 166;
    numArray13[0] = (byte) 93;
    numArray13[23] = (byte) 47;
    numArray13[24] = (byte) 174;
    numArray13[25] = (byte) 240 /*0xF0*/;
    numArray13[26] = (byte) 64 /*0x40*/;
    numArray13[27] = (byte) 44;
    numArray13[5] = (byte) 77;
    numArray13[51] = (byte) 144 /*0x90*/;
    numArray13[30] = (byte) 77;
    numArray13[50] = (byte) 48 /*0x30*/;
    numArray13[32 /*0x20*/] = (byte) 186;
    numArray13[8] = (byte) 139;
    numArray13[34] = (byte) 102;
    numArray13[35] = (byte) 42;
    numArray13[45] = (byte) 103;
    numArray13[54] = (byte) 88;
    numArray13[38] = (byte) 207;
    numArray13[20] = (byte) 115;
    numArray13[40] = (byte) 172;
    numArray13[41] = (byte) 92;
    numArray13[37] = (byte) 210;
    numArray13[43] = (byte) 203;
    numArray13[49] = (byte) 106;
    numArray13[28] = (byte) 185;
    numArray13[36] = (byte) 116;
    numArray13[47] = (byte) 159;
    numArray13[12] = (byte) 165;
    numArray13[19] = (byte) 32 /*0x20*/;
    numArray13[3] = (byte) 138;
    numArray13[46] = (byte) 150;
    numArray13[31 /*0x1F*/] = (byte) 136;
    numArray13[48 /*0x30*/] = (byte) 252;
    numArray13[44] = (byte) 215;
    byte[] numArray14 = new byte[55];
    numArray14[31 /*0x1F*/] = (byte) 7;
    numArray14[11] = (byte) 160 /*0xA0*/;
    numArray14[2] = (byte) 252;
    numArray14[3] = (byte) 169;
    numArray14[49] = (byte) 211;
    numArray14[34] = (byte) 202;
    numArray14[30] = (byte) 124;
    numArray14[7] = (byte) 74;
    numArray14[37] = (byte) 60;
    numArray14[9] = (byte) 193;
    numArray14[33] = (byte) 236;
    numArray14[32 /*0x20*/] = (byte) 167;
    numArray14[12] = (byte) 1;
    numArray14[13] = (byte) 208 /*0xD0*/;
    numArray14[50] = (byte) 66;
    numArray14[19] = (byte) 68;
    numArray14[8] = (byte) 212;
    numArray14[17] = (byte) 175;
    numArray14[18] = (byte) 108;
    numArray14[1] = (byte) 170;
    numArray14[20] = (byte) 195;
    numArray14[40] = (byte) 217;
    numArray14[22] = (byte) 66;
    numArray14[54] = (byte) 191;
    numArray14[24] = (byte) 82;
    numArray14[25] = (byte) 179;
    numArray14[26] = (byte) 161;
    numArray14[14] = (byte) 141;
    numArray14[53] = (byte) 85;
    numArray14[29] = (byte) 48 /*0x30*/;
    numArray14[28] = (byte) 150;
    numArray14[5] = (byte) 146;
    numArray14[0] = (byte) 246;
    numArray14[36] = (byte) 49;
    numArray14[4] = (byte) 14;
    numArray14[35] = (byte) 0;
    numArray14[42] = (byte) 147;
    numArray14[23] = (byte) 249;
    numArray14[38] = (byte) 61;
    numArray14[39] = (byte) 13;
    numArray14[52] = (byte) 252;
    numArray14[41] = (byte) 206;
    numArray14[15] = (byte) 113;
    numArray14[16 /*0x10*/] = (byte) 154;
    numArray14[44] = (byte) 20;
    numArray14[45] = (byte) 151;
    numArray14[46] = (byte) 41;
    numArray14[47] = (byte) 234;
    numArray14[48 /*0x30*/] = (byte) 144 /*0x90*/;
    numArray14[27] = (byte) 126;
    numArray14[21] = (byte) 31 /*0x1F*/;
    numArray14[51] = (byte) 33;
    numArray14[10] = (byte) 195;
    numArray14[43] = (byte) 57;
    numArray14[6] = (byte) 199;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 137,
      (byte) 18,
      (byte) 148,
      (byte) 27,
      (byte) 228,
      (byte) 175,
      (byte) 57,
      (byte) 189,
      (byte) 204,
      (byte) 121,
      (byte) 87,
      (byte) 102,
      (byte) 32 /*0x20*/,
      (byte) 37,
      (byte) 62,
      (byte) 41,
      (byte) 234,
      (byte) 253,
      (byte) 223,
      (byte) 220,
      (byte) 32 /*0x20*/,
      (byte) 27,
      (byte) 228,
      (byte) 33,
      (byte) 155,
      byte.MaxValue,
      (byte) 17,
      (byte) 88,
      (byte) 30,
      (byte) 86,
      (byte) 175,
      (byte) 146,
      (byte) 6,
      (byte) 136,
      (byte) 120,
      (byte) 80 /*0x50*/,
      (byte) 15,
      (byte) 48 /*0x30*/,
      (byte) 234,
      (byte) 199,
      (byte) 50,
      (byte) 199,
      (byte) 28,
      (byte) 35,
      (byte) 218,
      (byte) 78,
      (byte) 209,
      (byte) 101,
      (byte) 185,
      (byte) 62,
      (byte) 91,
      (byte) 0,
      (byte) 11,
      (byte) 84,
      (byte) 129
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 62,
      (byte) 220,
      (byte) 169,
      (byte) 127 /*0x7F*/,
      (byte) 1,
      (byte) 171,
      (byte) 252,
      (byte) 71,
      (byte) 109,
      (byte) 82,
      (byte) 178,
      (byte) 161,
      (byte) 218,
      (byte) 114,
      (byte) 90,
      (byte) 156,
      (byte) 194,
      (byte) 87,
      (byte) 44,
      (byte) 235,
      (byte) 82,
      (byte) 163,
      (byte) 124,
      (byte) 92,
      (byte) 149,
      (byte) 253,
      (byte) 1,
      (byte) 237,
      (byte) 20,
      (byte) 230,
      (byte) 87,
      (byte) 68,
      (byte) 103,
      (byte) 122,
      (byte) 228,
      (byte) 50,
      (byte) 162,
      (byte) 8,
      (byte) 253,
      (byte) 243,
      (byte) 240 /*0xF0*/,
      (byte) 37,
      (byte) 29,
      (byte) 175,
      (byte) 199,
      (byte) 167,
      (byte) 122,
      (byte) 114,
      (byte) 160 /*0xA0*/,
      (byte) 231,
      (byte) 69,
      (byte) 184,
      (byte) 177,
      (byte) 93,
      (byte) 7
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[3]
    {
      (byte) 115,
      (byte) 127 /*0x7F*/,
      (byte) 229
    };
    byte[] numArray18 = new byte[3]
    {
      (byte) 0,
      (byte) 108,
      (byte) 0
    };
    numArray18[0] = (byte) 202;
    numArray18[2] = (byte) 36;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 3);
    for (int index = 0; index < 3; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }
}
