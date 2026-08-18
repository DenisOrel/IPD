// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13818
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13818
{
  private static byte[] sspq = new byte[33]
  {
    (byte) 76,
    (byte) 27,
    (byte) 135,
    (byte) 242,
    (byte) 53,
    (byte) 125,
    (byte) 7,
    (byte) 151,
    (byte) 55,
    (byte) 162,
    (byte) 88,
    (byte) 206,
    (byte) 229,
    (byte) 52,
    (byte) 70,
    (byte) 80 /*0x50*/,
    (byte) 122,
    (byte) 15,
    (byte) 18,
    (byte) 132,
    (byte) 9,
    (byte) 230,
    (byte) 70,
    (byte) 202,
    (byte) 248,
    (byte) 11,
    (byte) 120,
    (byte) 245,
    (byte) 83,
    (byte) 26,
    (byte) 225,
    (byte) 201,
    (byte) 153
  };
  private static byte[] sspr = new byte[33]
  {
    (byte) 176 /*0xB0*/,
    (byte) 50,
    (byte) 16 /*0x10*/,
    (byte) 162,
    (byte) 187,
    (byte) 205,
    (byte) 135,
    (byte) 207,
    (byte) 78,
    (byte) 189,
    (byte) 97,
    (byte) 169,
    (byte) 220,
    (byte) 35,
    (byte) 157,
    (byte) 249,
    (byte) 222,
    (byte) 89,
    (byte) 211,
    (byte) 109,
    (byte) 218,
    (byte) 108,
    (byte) 86,
    (byte) 252,
    (byte) 98,
    (byte) 26,
    (byte) 5,
    (byte) 225,
    (byte) 121,
    (byte) 60,
    (byte) 215,
    (byte) 150,
    (byte) 136
  };

  internal static string ssp_appserver_13819()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[147];
      byte[] numArray2 = new byte[55]
      {
        (byte) 217,
        (byte) 141,
        (byte) 79,
        (byte) 88,
        (byte) 80 /*0x50*/,
        (byte) 91,
        (byte) 43,
        (byte) 219,
        (byte) 88,
        (byte) 24,
        (byte) 6,
        (byte) 4,
        (byte) 136,
        (byte) 84,
        (byte) 228,
        (byte) 38,
        (byte) 157,
        (byte) 191,
        (byte) 165,
        (byte) 111,
        (byte) 241,
        (byte) 217,
        (byte) 179,
        (byte) 89,
        (byte) 41,
        (byte) 14,
        (byte) 31 /*0x1F*/,
        (byte) 46,
        (byte) 27,
        (byte) 124,
        (byte) 9,
        (byte) 124,
        (byte) 137,
        (byte) 86,
        (byte) 130,
        (byte) 11,
        (byte) 80 /*0x50*/,
        (byte) 21,
        (byte) 232,
        (byte) 144 /*0x90*/,
        (byte) 68,
        (byte) 191,
        (byte) 192 /*0xC0*/,
        (byte) 204,
        (byte) 170,
        (byte) 62,
        (byte) 189,
        (byte) 95,
        (byte) 223,
        (byte) 240 /*0xF0*/,
        (byte) 166,
        (byte) 139,
        (byte) 83,
        (byte) 145,
        (byte) 208 /*0xD0*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 110,
        (byte) 49,
        (byte) 139,
        (byte) 154,
        (byte) 250,
        (byte) 243,
        (byte) 140,
        (byte) 73,
        (byte) 183,
        (byte) 10,
        (byte) 248,
        (byte) 159,
        (byte) 8,
        (byte) 155,
        (byte) 193,
        (byte) 185,
        (byte) 122,
        (byte) 7,
        (byte) 102,
        (byte) 215,
        (byte) 141,
        (byte) 5,
        (byte) 98,
        (byte) 80 /*0x50*/,
        (byte) 116,
        (byte) 38,
        (byte) 184,
        (byte) 160 /*0xA0*/,
        (byte) 244,
        (byte) 188,
        (byte) 249,
        (byte) 79,
        (byte) 103,
        (byte) 139,
        (byte) 212,
        (byte) 99,
        (byte) 96 /*0x60*/,
        (byte) 99,
        (byte) 26,
        (byte) 204,
        (byte) 56,
        (byte) 61,
        (byte) 123,
        (byte) 238,
        (byte) 23,
        (byte) 200,
        (byte) 19,
        (byte) 202,
        (byte) 239,
        (byte) 53,
        (byte) 144 /*0x90*/,
        (byte) 28,
        (byte) 69,
        (byte) 31 /*0x1F*/,
        (byte) 158
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[6] = (byte) 220;
      numArray4[30] = (byte) 247;
      numArray4[36] = (byte) 166;
      numArray4[3] = (byte) 59;
      numArray4[40] = (byte) 227;
      numArray4[4] = (byte) 253;
      numArray4[50] = (byte) 61;
      numArray4[20] = (byte) 230;
      numArray4[52] = (byte) 157;
      numArray4[13] = (byte) 214;
      numArray4[10] = (byte) 50;
      numArray4[11] = (byte) 210;
      numArray4[12] = (byte) 217;
      numArray4[44] = (byte) 139;
      numArray4[14] = (byte) 136;
      numArray4[15] = (byte) 214;
      numArray4[43] = (byte) 195;
      numArray4[7] = (byte) 15;
      numArray4[29] = (byte) 40;
      numArray4[19] = (byte) 25;
      numArray4[53] = (byte) 230;
      numArray4[21] = (byte) 175;
      numArray4[22] = (byte) 234;
      numArray4[9] = (byte) 122;
      numArray4[24] = (byte) 192 /*0xC0*/;
      numArray4[25] = (byte) 78;
      numArray4[26] = (byte) 43;
      numArray4[38] = (byte) 70;
      numArray4[45] = (byte) 141;
      numArray4[27] = (byte) 214;
      numArray4[28] = (byte) 125;
      numArray4[31 /*0x1F*/] = (byte) 134;
      numArray4[32 /*0x20*/] = (byte) 130;
      numArray4[17] = (byte) 42;
      numArray4[34] = (byte) 90;
      numArray4[16 /*0x10*/] = (byte) 21;
      numArray4[33] = (byte) 49;
      numArray4[5] = (byte) 192 /*0xC0*/;
      numArray4[54] = (byte) 243;
      numArray4[39] = byte.MaxValue;
      numArray4[8] = (byte) 138;
      numArray4[41] = (byte) 189;
      numArray4[42] = (byte) 31 /*0x1F*/;
      numArray4[23] = (byte) 80 /*0x50*/;
      numArray4[35] = (byte) 230;
      numArray4[49] = (byte) 181;
      numArray4[46] = (byte) 5;
      numArray4[47] = (byte) 25;
      numArray4[48 /*0x30*/] = (byte) 30;
      numArray4[0] = (byte) 245;
      numArray4[2] = (byte) 66;
      numArray4[51] = (byte) 103;
      numArray4[18] = (byte) 50;
      numArray4[37] = (byte) 85;
      numArray4[1] = (byte) 46;
      byte[] numArray5 = new byte[55]
      {
        (byte) 179,
        (byte) 229,
        (byte) 119,
        (byte) 139,
        (byte) 98,
        (byte) 151,
        (byte) 5,
        (byte) 34,
        (byte) 201,
        (byte) 76,
        (byte) 71,
        (byte) 82,
        (byte) 92,
        (byte) 209,
        (byte) 214,
        (byte) 170,
        (byte) 56,
        (byte) 139,
        (byte) 44,
        (byte) 102,
        (byte) 87,
        (byte) 50,
        (byte) 93,
        (byte) 41,
        (byte) 104,
        (byte) 135,
        (byte) 224 /*0xE0*/,
        (byte) 43,
        (byte) 210,
        (byte) 171,
        (byte) 99,
        (byte) 197,
        (byte) 43,
        (byte) 240 /*0xF0*/,
        (byte) 81,
        (byte) 182,
        (byte) 176 /*0xB0*/,
        (byte) 74,
        (byte) 106,
        (byte) 185,
        (byte) 23,
        (byte) 100,
        (byte) 35,
        (byte) 91,
        (byte) 45,
        (byte) 150,
        (byte) 63 /*0x3F*/,
        (byte) 130,
        (byte) 123,
        (byte) 6,
        (byte) 176 /*0xB0*/,
        (byte) 104,
        (byte) 17,
        (byte) 136,
        (byte) 179
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[37]
      {
        (byte) 44,
        (byte) 71,
        (byte) 102,
        (byte) 131,
        (byte) 78,
        (byte) 201,
        (byte) 119,
        (byte) 243,
        (byte) 195,
        (byte) 181,
        (byte) 107,
        (byte) 103,
        (byte) 254,
        (byte) 124,
        (byte) 229,
        (byte) 76,
        (byte) 200,
        (byte) 88,
        (byte) 252,
        (byte) 209,
        (byte) 231,
        (byte) 230,
        (byte) 246,
        (byte) 235,
        (byte) 115,
        (byte) 59,
        (byte) 68,
        (byte) 109,
        (byte) 12,
        (byte) 125,
        (byte) 234,
        (byte) 114,
        (byte) 92,
        (byte) 134,
        (byte) 135,
        (byte) 146,
        (byte) 49
      };
      byte[] numArray7 = new byte[37]
      {
        (byte) 175,
        (byte) 86,
        (byte) 151,
        (byte) 32 /*0x20*/,
        (byte) 36,
        (byte) 87,
        (byte) 245,
        (byte) 9,
        (byte) 118,
        (byte) 195,
        (byte) 15,
        (byte) 37,
        (byte) 154,
        (byte) 88,
        (byte) 76,
        (byte) 46,
        (byte) 80 /*0x50*/,
        (byte) 140,
        (byte) 160 /*0xA0*/,
        (byte) 162,
        (byte) 237,
        (byte) 72,
        (byte) 117,
        (byte) 147,
        (byte) 197,
        (byte) 4,
        (byte) 190,
        (byte) 177,
        (byte) 197,
        (byte) 67,
        (byte) 143,
        (byte) 180,
        (byte) 71,
        (byte) 35,
        (byte) 116,
        (byte) 69,
        (byte) 150
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_13818.sspq, 0, (Array) numArray8, 0, 33);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13818.sspr, 0, (Array) numArray8, 0, 33);
      for (int index = 0; index < numArray8.Length; ++index)
      {
        if ((int) numArray8[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray9 = new byte[147];
    byte[] numArray10 = new byte[55];
    numArray10[31 /*0x1F*/] = (byte) 193;
    numArray10[30] = (byte) 29;
    numArray10[6] = (byte) 189;
    numArray10[3] = (byte) 110;
    numArray10[4] = (byte) 249;
    numArray10[47] = (byte) 147;
    numArray10[28] = (byte) 17;
    numArray10[7] = (byte) 61;
    numArray10[8] = (byte) 213;
    numArray10[49] = (byte) 180;
    numArray10[42] = (byte) 52;
    numArray10[11] = (byte) 145;
    numArray10[9] = (byte) 212;
    numArray10[48 /*0x30*/] = (byte) 39;
    numArray10[18] = (byte) 216;
    numArray10[15] = (byte) 64 /*0x40*/;
    numArray10[27] = (byte) 5;
    numArray10[17] = (byte) 183;
    numArray10[32 /*0x20*/] = (byte) 143;
    numArray10[19] = (byte) 124;
    numArray10[20] = (byte) 222;
    numArray10[35] = (byte) 154;
    numArray10[33] = (byte) 52;
    numArray10[23] = (byte) 212;
    numArray10[24] = (byte) 188;
    numArray10[25] = (byte) 249;
    numArray10[40] = (byte) 84;
    numArray10[5] = (byte) 61;
    numArray10[50] = (byte) 17;
    numArray10[45] = (byte) 57;
    numArray10[34] = (byte) 89;
    numArray10[44] = (byte) 106;
    numArray10[38] = (byte) 129;
    numArray10[29] = (byte) 68;
    numArray10[10] = (byte) 219;
    numArray10[14] = (byte) 81;
    numArray10[36] = (byte) 123;
    numArray10[37] = (byte) 226;
    numArray10[53] = (byte) 76;
    numArray10[39] = (byte) 209;
    numArray10[2] = (byte) 203;
    numArray10[13] = (byte) 119;
    numArray10[41] = (byte) 71;
    numArray10[43] = (byte) 17;
    numArray10[21] = (byte) 60;
    numArray10[22] = (byte) 102;
    numArray10[46] = (byte) 84;
    numArray10[1] = (byte) 154;
    numArray10[0] = (byte) 139;
    numArray10[16 /*0x10*/] = (byte) 65;
    numArray10[52] = (byte) 8;
    numArray10[51] = (byte) 99;
    numArray10[12] = (byte) 164;
    numArray10[26] = (byte) 42;
    numArray10[54] = (byte) 187;
    byte[] numArray11 = new byte[55]
    {
      (byte) 24,
      (byte) 254,
      (byte) 75,
      (byte) 211,
      (byte) 10,
      (byte) 26,
      (byte) 86,
      (byte) 1,
      (byte) 10,
      (byte) 35,
      (byte) 104,
      (byte) 254,
      (byte) 189,
      (byte) 77,
      (byte) 146,
      (byte) 5,
      (byte) 177,
      (byte) 149,
      (byte) 234,
      (byte) 70,
      (byte) 194,
      (byte) 18,
      (byte) 252,
      (byte) 148,
      (byte) 246,
      (byte) 241,
      (byte) 231,
      (byte) 239,
      (byte) 28,
      (byte) 162,
      (byte) 220,
      (byte) 193,
      (byte) 148,
      (byte) 210,
      (byte) 158,
      (byte) 214,
      (byte) 204,
      (byte) 184,
      (byte) 116,
      (byte) 174,
      (byte) 26,
      (byte) 57,
      (byte) 229,
      (byte) 150,
      (byte) 158,
      (byte) 70,
      (byte) 173,
      (byte) 3,
      (byte) 188,
      (byte) 251,
      (byte) 1,
      (byte) 42,
      (byte) 31 /*0x1F*/,
      (byte) 119,
      (byte) 54
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[11] = (byte) 18;
    numArray12[44] = (byte) 236;
    numArray12[54] = (byte) 230;
    numArray12[3] = (byte) 124;
    numArray12[38] = (byte) 193;
    numArray12[2] = (byte) 90;
    numArray12[4] = (byte) 218;
    numArray12[7] = (byte) 254;
    numArray12[8] = (byte) 98;
    numArray12[9] = (byte) 192 /*0xC0*/;
    numArray12[10] = (byte) 247;
    numArray12[33] = (byte) 133;
    numArray12[12] = (byte) 81;
    numArray12[45] = (byte) 98;
    numArray12[14] = (byte) 46;
    numArray12[15] = (byte) 180;
    numArray12[16 /*0x10*/] = (byte) 72;
    numArray12[17] = (byte) 139;
    numArray12[1] = (byte) 46;
    numArray12[34] = (byte) 247;
    numArray12[27] = (byte) 128 /*0x80*/;
    numArray12[21] = (byte) 25;
    numArray12[39] = (byte) 76;
    numArray12[23] = (byte) 192 /*0xC0*/;
    numArray12[19] = (byte) 100;
    numArray12[25] = (byte) 110;
    numArray12[31 /*0x1F*/] = (byte) 83;
    numArray12[30] = (byte) 28;
    numArray12[28] = (byte) 140;
    numArray12[29] = (byte) 182;
    numArray12[47] = (byte) 174;
    numArray12[32 /*0x20*/] = (byte) 196;
    numArray12[48 /*0x30*/] = (byte) 131;
    numArray12[36] = (byte) 38;
    numArray12[51] = (byte) 154;
    numArray12[35] = (byte) 2;
    numArray12[5] = (byte) 206;
    numArray12[37] = (byte) 65;
    numArray12[43] = (byte) 119;
    numArray12[24] = (byte) 33;
    numArray12[40] = (byte) 70;
    numArray12[13] = (byte) 64 /*0x40*/;
    numArray12[42] = (byte) 238;
    numArray12[22] = (byte) 76;
    numArray12[53] = (byte) 226;
    numArray12[20] = (byte) 250;
    numArray12[46] = (byte) 30;
    numArray12[18] = (byte) 123;
    numArray12[6] = (byte) 93;
    numArray12[49] = (byte) 120;
    numArray12[50] = (byte) 185;
    numArray12[41] = (byte) 91;
    numArray12[52] = (byte) 169;
    numArray12[26] = (byte) 109;
    numArray12[0] = (byte) 140;
    byte[] numArray13 = new byte[55]
    {
      (byte) 222,
      (byte) 165,
      (byte) 203,
      (byte) 78,
      (byte) 49,
      (byte) 223,
      (byte) 165,
      (byte) 248,
      (byte) 32 /*0x20*/,
      (byte) 2,
      (byte) 181,
      (byte) 243,
      (byte) 130,
      (byte) 9,
      (byte) 218,
      (byte) 18,
      (byte) 233,
      (byte) 11,
      (byte) 167,
      (byte) 18,
      (byte) 158,
      (byte) 110,
      (byte) 91,
      (byte) 219,
      (byte) 128 /*0x80*/,
      (byte) 62,
      (byte) 253,
      (byte) 198,
      (byte) 119,
      (byte) 192 /*0xC0*/,
      (byte) 15,
      (byte) 65,
      (byte) 42,
      (byte) 152,
      (byte) 108,
      (byte) 52,
      (byte) 95,
      (byte) 175,
      (byte) 147,
      (byte) 7,
      (byte) 53,
      (byte) 3,
      (byte) 186,
      (byte) 79,
      (byte) 139,
      (byte) 244,
      (byte) 42,
      (byte) 173,
      (byte) 169,
      (byte) 232,
      (byte) 30,
      (byte) 250,
      (byte) 84,
      (byte) 12,
      (byte) 53
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[37]
    {
      (byte) 198,
      (byte) 157,
      (byte) 29,
      (byte) 162,
      (byte) 223,
      (byte) 93,
      (byte) 174,
      (byte) 32 /*0x20*/,
      (byte) 249,
      (byte) 108,
      (byte) 44,
      (byte) 196,
      (byte) 10,
      (byte) 1,
      (byte) 31 /*0x1F*/,
      (byte) 116,
      (byte) 67,
      (byte) 179,
      (byte) 179,
      (byte) 232,
      (byte) 1,
      (byte) 138,
      (byte) 169,
      (byte) 252,
      (byte) 89,
      (byte) 165,
      (byte) 41,
      (byte) 74,
      (byte) 178,
      (byte) 47,
      (byte) 29,
      (byte) 87,
      (byte) 59,
      (byte) 20,
      (byte) 197,
      (byte) 73,
      (byte) 55
    };
    byte[] numArray15 = new byte[37]
    {
      (byte) 105,
      (byte) 156,
      (byte) 188,
      (byte) 152,
      byte.MaxValue,
      (byte) 238,
      (byte) 142,
      (byte) 33,
      (byte) 200,
      (byte) 201,
      (byte) 68,
      (byte) 223,
      (byte) 113,
      (byte) 76,
      byte.MaxValue,
      (byte) 176 /*0xB0*/,
      (byte) 207,
      (byte) 141,
      (byte) 1,
      (byte) 13,
      (byte) 62,
      (byte) 17,
      (byte) 125,
      (byte) 166,
      (byte) 226,
      (byte) 74,
      (byte) 79,
      (byte) 222,
      (byte) 33,
      (byte) 226,
      (byte) 61,
      (byte) 7,
      (byte) 248,
      (byte) 205,
      (byte) 31 /*0x1F*/,
      (byte) 103,
      (byte) 66
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 37);
    for (int index = 0; index < 37; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }
}
