// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12555
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12555
{
  private static byte[] sspq = new byte[109]
  {
    (byte) 56,
    (byte) 219,
    (byte) 138,
    (byte) 90,
    (byte) 19,
    (byte) 34,
    (byte) 24,
    (byte) 77,
    (byte) 36,
    (byte) 0,
    (byte) 170,
    (byte) 219,
    (byte) 17,
    (byte) 85,
    (byte) 35,
    (byte) 240 /*0xF0*/,
    (byte) 115,
    (byte) 254,
    (byte) 12,
    (byte) 94,
    (byte) 52,
    (byte) 17,
    (byte) 3,
    (byte) 210,
    (byte) 190,
    (byte) 180,
    (byte) 93,
    (byte) 193,
    (byte) 81,
    (byte) 63 /*0x3F*/,
    (byte) 254,
    (byte) 105,
    (byte) 193,
    (byte) 67,
    (byte) 109,
    (byte) 5,
    (byte) 3,
    (byte) 95,
    (byte) 204,
    (byte) 142,
    (byte) 206,
    (byte) 63 /*0x3F*/,
    (byte) 126,
    (byte) 80 /*0x50*/,
    (byte) 105,
    (byte) 134,
    (byte) 125,
    (byte) 156,
    (byte) 10,
    (byte) 251,
    (byte) 40,
    (byte) 240 /*0xF0*/,
    (byte) 77,
    (byte) 118,
    (byte) 104,
    (byte) 234,
    (byte) 66,
    (byte) 6,
    (byte) 76,
    (byte) 126,
    (byte) 176 /*0xB0*/,
    (byte) 115,
    (byte) 113,
    (byte) 68,
    (byte) 242,
    (byte) 227,
    (byte) 241,
    (byte) 97,
    (byte) 5,
    (byte) 3,
    (byte) 115,
    (byte) 192 /*0xC0*/,
    (byte) 72,
    (byte) 162,
    (byte) 250,
    (byte) 21,
    (byte) 214,
    (byte) 88,
    (byte) 96 /*0x60*/,
    (byte) 47,
    (byte) 188,
    (byte) 173,
    (byte) 178,
    (byte) 86,
    (byte) 62,
    (byte) 65,
    (byte) 252,
    (byte) 187,
    (byte) 90,
    (byte) 218,
    (byte) 171,
    (byte) 41,
    (byte) 93,
    (byte) 107,
    (byte) 144 /*0x90*/,
    (byte) 77,
    (byte) 236,
    (byte) 99,
    (byte) 208 /*0xD0*/,
    (byte) 66,
    (byte) 82,
    (byte) 250,
    (byte) 20,
    (byte) 35,
    (byte) 6,
    (byte) 191,
    (byte) 227,
    (byte) 5,
    (byte) 239
  };
  private static byte[] sspr = new byte[109]
  {
    (byte) 107,
    (byte) 248,
    (byte) 111,
    (byte) 75,
    (byte) 150,
    (byte) 238,
    (byte) 165,
    (byte) 46,
    (byte) 45,
    (byte) 203,
    (byte) 0,
    (byte) 137,
    (byte) 254,
    (byte) 94,
    (byte) 24,
    (byte) 187,
    (byte) 113,
    (byte) 142,
    (byte) 131,
    (byte) 201,
    (byte) 88,
    (byte) 151,
    (byte) 70,
    (byte) 44,
    (byte) 30,
    (byte) 125,
    (byte) 82,
    (byte) 93,
    (byte) 208 /*0xD0*/,
    (byte) 241,
    (byte) 193,
    (byte) 30,
    (byte) 183,
    (byte) 1,
    (byte) 197,
    (byte) 172,
    (byte) 4,
    (byte) 206,
    (byte) 139,
    (byte) 67,
    (byte) 3,
    (byte) 160 /*0xA0*/,
    (byte) 143,
    (byte) 143,
    (byte) 1,
    (byte) 210,
    (byte) 132,
    (byte) 150,
    (byte) 114,
    (byte) 163,
    (byte) 210,
    (byte) 172,
    (byte) 96 /*0x60*/,
    (byte) 49,
    (byte) 51,
    (byte) 46,
    (byte) 223,
    (byte) 61,
    (byte) 101,
    (byte) 216,
    (byte) 107,
    (byte) 167,
    (byte) 213,
    (byte) 254,
    (byte) 132,
    (byte) 240 /*0xF0*/,
    (byte) 192 /*0xC0*/,
    (byte) 41,
    (byte) 44,
    (byte) 218,
    (byte) 203,
    (byte) 14,
    (byte) 195,
    (byte) 231,
    (byte) 223,
    (byte) 238,
    (byte) 5,
    (byte) 117,
    (byte) 117,
    (byte) 228,
    (byte) 148,
    (byte) 9,
    (byte) 11,
    (byte) 140,
    (byte) 138,
    (byte) 44,
    (byte) 144 /*0x90*/,
    (byte) 0,
    (byte) 244,
    (byte) 24,
    (byte) 185,
    (byte) 149,
    (byte) 82,
    (byte) 188,
    (byte) 139,
    (byte) 47,
    (byte) 234,
    (byte) 34,
    (byte) 138,
    (byte) 140,
    (byte) 114,
    (byte) 147,
    (byte) 34,
    (byte) 156,
    (byte) 174,
    (byte) 250,
    (byte) 203,
    (byte) 205,
    (byte) 20
  };

  internal static int ssp_appserver_12556(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[20] = (byte) 90;
    sourceArray1[19] = (byte) 248;
    sourceArray1[2] = (byte) 92;
    sourceArray1[35] = (byte) 46;
    sourceArray1[34] = (byte) 217;
    sourceArray1[5] = (byte) 209;
    sourceArray1[42] = (byte) 166;
    sourceArray1[7] = (byte) 123;
    sourceArray1[17] = (byte) 90;
    sourceArray1[9] = (byte) 248;
    sourceArray1[0] = (byte) 137;
    sourceArray1[11] = (byte) 32 /*0x20*/;
    sourceArray1[44] = (byte) 154;
    sourceArray1[13] = (byte) 234;
    sourceArray1[29] = (byte) 154;
    sourceArray1[15] = (byte) 241;
    sourceArray1[16 /*0x10*/] = (byte) 71;
    sourceArray1[6] = (byte) 209;
    sourceArray1[27] = (byte) 72;
    sourceArray1[4] = (byte) 59;
    sourceArray1[32 /*0x20*/] = (byte) 195;
    sourceArray1[3] = (byte) 105;
    sourceArray1[23] = (byte) 89;
    sourceArray1[36] = (byte) 211;
    sourceArray1[24] = (byte) 181;
    sourceArray1[30] = (byte) 143;
    sourceArray1[45] = (byte) 194;
    sourceArray1[14] = (byte) 72;
    sourceArray1[28] = (byte) 209;
    sourceArray1[43] = (byte) 241;
    sourceArray1[26] = (byte) 147;
    sourceArray1[31 /*0x1F*/] = (byte) 24;
    sourceArray1[1] = (byte) 35;
    sourceArray1[33] = (byte) 178;
    sourceArray1[12] = (byte) 98;
    sourceArray1[21] = (byte) 40;
    sourceArray1[8] = (byte) 157;
    sourceArray1[37] = (byte) 186;
    sourceArray1[38] = (byte) 201;
    sourceArray1[39] = (byte) 109;
    sourceArray1[40] = (byte) 249;
    sourceArray1[18] = (byte) 126;
    sourceArray1[41] = (byte) 37;
    sourceArray1[22] = (byte) 43;
    sourceArray1[10] = (byte) 219;
    sourceArray1[25] = (byte) 94;
    sourceArray1[46] = (byte) 54;
    sourceArray1[47] = (byte) 169;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 83;
    sourceArray2[1] = (byte) 113;
    sourceArray2[44] = (byte) 62;
    sourceArray2[37] = (byte) 76;
    sourceArray2[3] = (byte) 224 /*0xE0*/;
    sourceArray2[0] = (byte) 9;
    sourceArray2[6] = (byte) 212;
    sourceArray2[35] = (byte) 125;
    sourceArray2[47] = (byte) 238;
    sourceArray2[9] = (byte) 3;
    sourceArray2[12] = (byte) 213;
    sourceArray2[11] = (byte) 67;
    sourceArray2[46] = (byte) 15;
    sourceArray2[13] = (byte) 200;
    sourceArray2[43] = (byte) 40;
    sourceArray2[15] = (byte) 214;
    sourceArray2[38] = (byte) 203;
    sourceArray2[17] = (byte) 131;
    sourceArray2[14] = (byte) 223;
    sourceArray2[19] = (byte) 66;
    sourceArray2[4] = (byte) 128 /*0x80*/;
    sourceArray2[7] = (byte) 174;
    sourceArray2[24] = (byte) 249;
    sourceArray2[26] = (byte) 161;
    sourceArray2[5] = (byte) 18;
    sourceArray2[22] = (byte) 178;
    sourceArray2[10] = (byte) 198;
    sourceArray2[25] = (byte) 222;
    sourceArray2[28] = (byte) 11;
    sourceArray2[29] = (byte) 195;
    sourceArray2[30] = (byte) 19;
    sourceArray2[31 /*0x1F*/] = (byte) 193;
    sourceArray2[32 /*0x20*/] = (byte) 8;
    sourceArray2[33] = (byte) 167;
    sourceArray2[34] = (byte) 52;
    sourceArray2[21] = (byte) 103;
    sourceArray2[36] = (byte) 59;
    sourceArray2[8] = (byte) 78;
    sourceArray2[41] = (byte) 112 /*0x70*/;
    sourceArray2[2] = (byte) 46;
    sourceArray2[40] = (byte) 69;
    sourceArray2[27] = (byte) 34;
    sourceArray2[42] = (byte) 195;
    sourceArray2[23] = (byte) 13;
    sourceArray2[39] = (byte) 37;
    sourceArray2[45] = (byte) 83;
    sourceArray2[18] = (byte) 44;
    sourceArray2[20] = (byte) 129;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[46];
    byte[] response2 = new byte[46];
    Array.Copy((Array) sc_12555.sspq, 0, (Array) numArray2, 0, 46);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12555.sspr, 0, (Array) numArray2, 0, 46);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_12557(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[9] = (byte) 184;
    sourceArray1[1] = (byte) 213;
    sourceArray1[23] = (byte) 19;
    sourceArray1[3] = (byte) 155;
    sourceArray1[4] = (byte) 108;
    sourceArray1[5] = (byte) 45;
    sourceArray1[6] = (byte) 14;
    sourceArray1[7] = (byte) 38;
    sourceArray1[39] = (byte) 148;
    sourceArray1[35] = (byte) 124;
    sourceArray1[10] = (byte) 155;
    sourceArray1[25] = (byte) 125;
    sourceArray1[32 /*0x20*/] = (byte) 234;
    sourceArray1[2] = (byte) 173;
    sourceArray1[8] = (byte) 216;
    sourceArray1[15] = (byte) 228;
    sourceArray1[14] = (byte) 251;
    sourceArray1[21] = (byte) 30;
    sourceArray1[18] = (byte) 25;
    sourceArray1[29] = (byte) 178;
    sourceArray1[20] = (byte) 231;
    sourceArray1[33] = (byte) 170;
    sourceArray1[22] = (byte) 237;
    sourceArray1[47] = (byte) 119;
    sourceArray1[24] = (byte) 49;
    sourceArray1[43] = (byte) 75;
    sourceArray1[16 /*0x10*/] = (byte) 1;
    sourceArray1[27] = (byte) 245;
    sourceArray1[28] = (byte) 147;
    sourceArray1[38] = (byte) 252;
    sourceArray1[30] = (byte) 104;
    sourceArray1[26] = (byte) 143;
    sourceArray1[37] = (byte) 99;
    sourceArray1[31 /*0x1F*/] = (byte) 56;
    sourceArray1[44] = (byte) 196;
    sourceArray1[0] = (byte) 219;
    sourceArray1[36] = (byte) 146;
    sourceArray1[45] = (byte) 110;
    sourceArray1[13] = (byte) 87;
    sourceArray1[19] = (byte) 205;
    sourceArray1[40] = (byte) 53;
    sourceArray1[34] = (byte) 229;
    sourceArray1[42] = (byte) 45;
    sourceArray1[12] = (byte) 197;
    sourceArray1[11] = (byte) 94;
    sourceArray1[41] = (byte) 70;
    sourceArray1[46] = (byte) 201;
    sourceArray1[17] = (byte) 65;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 188,
      (byte) 233,
      (byte) 217,
      (byte) 253,
      (byte) 41,
      (byte) 126,
      (byte) 129,
      (byte) 250,
      (byte) 1,
      (byte) 116,
      (byte) 26,
      (byte) 56,
      (byte) 24,
      (byte) 0,
      (byte) 48 /*0x30*/,
      (byte) 251,
      (byte) 128 /*0x80*/,
      (byte) 241,
      (byte) 16 /*0x10*/,
      (byte) 27,
      (byte) 14,
      (byte) 48 /*0x30*/,
      (byte) 252,
      (byte) 174,
      (byte) 71,
      (byte) 123,
      (byte) 152,
      (byte) 33,
      (byte) 216,
      (byte) 20,
      (byte) 202,
      (byte) 171,
      (byte) 56,
      (byte) 136,
      (byte) 17,
      (byte) 49,
      (byte) 42,
      (byte) 144 /*0x90*/,
      (byte) 85,
      (byte) 4,
      (byte) 171,
      (byte) 73,
      (byte) 73,
      (byte) 75,
      (byte) 122,
      (byte) 57,
      (byte) 254,
      (byte) 10
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12558(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 56;
    sourceArray1[1] = (byte) 5;
    sourceArray1[46] = (byte) 82;
    sourceArray1[6] = (byte) 232;
    sourceArray1[34] = (byte) 5;
    sourceArray1[35] = (byte) 18;
    sourceArray1[10] = (byte) 69;
    sourceArray1[7] = (byte) 100;
    sourceArray1[5] = (byte) 225;
    sourceArray1[9] = (byte) 204;
    sourceArray1[39] = (byte) 95;
    sourceArray1[11] = (byte) 74;
    sourceArray1[12] = (byte) 167;
    sourceArray1[38] = (byte) 176 /*0xB0*/;
    sourceArray1[14] = (byte) 132;
    sourceArray1[8] = (byte) 42;
    sourceArray1[33] = (byte) 200;
    sourceArray1[17] = (byte) 195;
    sourceArray1[28] = (byte) 124;
    sourceArray1[19] = (byte) 151;
    sourceArray1[20] = (byte) 134;
    sourceArray1[21] = (byte) 60;
    sourceArray1[24] = (byte) 90;
    sourceArray1[23] = (byte) 234;
    sourceArray1[22] = (byte) 72;
    sourceArray1[25] = (byte) 164;
    sourceArray1[36] = (byte) 237;
    sourceArray1[27] = (byte) 142;
    sourceArray1[2] = (byte) 196;
    sourceArray1[43] = (byte) 92;
    sourceArray1[30] = (byte) 233;
    sourceArray1[31 /*0x1F*/] = (byte) 167;
    sourceArray1[32 /*0x20*/] = (byte) 237;
    sourceArray1[15] = (byte) 103;
    sourceArray1[41] = (byte) 136;
    sourceArray1[13] = (byte) 58;
    sourceArray1[16 /*0x10*/] = (byte) 154;
    sourceArray1[37] = (byte) 178;
    sourceArray1[18] = (byte) 14;
    sourceArray1[3] = (byte) 71;
    sourceArray1[40] = (byte) 226;
    sourceArray1[29] = (byte) 238;
    sourceArray1[42] = (byte) 179;
    sourceArray1[4] = (byte) 178;
    sourceArray1[44] = (byte) 79;
    sourceArray1[45] = (byte) 174;
    sourceArray1[0] = (byte) 174;
    sourceArray1[47] = (byte) 209;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 195;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[33] = (byte) 178;
    sourceArray2[3] = (byte) 1;
    sourceArray2[18] = (byte) 253;
    sourceArray2[45] = (byte) 219;
    sourceArray2[6] = (byte) 200;
    sourceArray2[7] = (byte) 35;
    sourceArray2[43] = (byte) 95;
    sourceArray2[9] = (byte) 199;
    sourceArray2[47] = (byte) 24;
    sourceArray2[15] = (byte) 104;
    sourceArray2[4] = (byte) 28;
    sourceArray2[13] = (byte) 81;
    sourceArray2[21] = (byte) 217;
    sourceArray2[17] = (byte) 152;
    sourceArray2[16 /*0x10*/] = (byte) 11;
    sourceArray2[14] = (byte) 207;
    sourceArray2[26] = (byte) 215;
    sourceArray2[19] = (byte) 97;
    sourceArray2[11] = (byte) 180;
    sourceArray2[46] = (byte) 20;
    sourceArray2[28] = (byte) 79;
    sourceArray2[20] = (byte) 242;
    sourceArray2[23] = (byte) 32 /*0x20*/;
    sourceArray2[36] = (byte) 223;
    sourceArray2[25] = (byte) 55;
    sourceArray2[12] = (byte) 14;
    sourceArray2[38] = (byte) 80 /*0x50*/;
    sourceArray2[29] = (byte) 127 /*0x7F*/;
    sourceArray2[10] = (byte) 112 /*0x70*/;
    sourceArray2[30] = (byte) 180;
    sourceArray2[32 /*0x20*/] = (byte) 103;
    sourceArray2[8] = (byte) 96 /*0x60*/;
    sourceArray2[34] = (byte) 153;
    sourceArray2[35] = (byte) 244;
    sourceArray2[39] = (byte) 95;
    sourceArray2[5] = (byte) 215;
    sourceArray2[22] = (byte) 48 /*0x30*/;
    sourceArray2[0] = (byte) 248;
    sourceArray2[40] = (byte) 145;
    sourceArray2[41] = (byte) 139;
    sourceArray2[42] = (byte) 244;
    sourceArray2[37] = (byte) 135;
    sourceArray2[44] = (byte) 109;
    sourceArray2[2] = (byte) 115;
    sourceArray2[27] = (byte) 48 /*0x30*/;
    sourceArray2[31 /*0x1F*/] = (byte) 121;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12559(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 115,
      (byte) 74,
      (byte) 129,
      (byte) 15,
      (byte) 61,
      (byte) 206,
      (byte) 23,
      (byte) 235,
      (byte) 251,
      (byte) 227,
      (byte) 211,
      (byte) 12,
      (byte) 192 /*0xC0*/,
      (byte) 67,
      (byte) 233,
      (byte) 24,
      (byte) 213,
      (byte) 93,
      (byte) 86,
      (byte) 231,
      (byte) 220,
      (byte) 120,
      (byte) 33,
      (byte) 144 /*0x90*/,
      (byte) 193,
      (byte) 199,
      (byte) 101,
      (byte) 45,
      (byte) 168,
      (byte) 245,
      (byte) 151,
      (byte) 146,
      (byte) 159,
      (byte) 137,
      (byte) 133,
      (byte) 225,
      (byte) 219,
      (byte) 3,
      (byte) 53,
      (byte) 26,
      (byte) 66,
      (byte) 243,
      (byte) 206,
      (byte) 57,
      (byte) 187,
      (byte) 152,
      (byte) 181,
      (byte) 58
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 200,
      (byte) 104,
      (byte) 155,
      (byte) 226,
      (byte) 181,
      (byte) 44,
      (byte) 210,
      (byte) 64 /*0x40*/,
      (byte) 104,
      (byte) 158,
      (byte) 57,
      (byte) 78,
      (byte) 25,
      (byte) 89,
      (byte) 95,
      (byte) 63 /*0x3F*/,
      (byte) 62,
      (byte) 238,
      (byte) 147,
      (byte) 244,
      (byte) 22,
      (byte) 54,
      (byte) 135,
      (byte) 49,
      (byte) 13,
      (byte) 210,
      (byte) 132,
      (byte) 73,
      (byte) 152,
      (byte) 205,
      (byte) 190,
      (byte) 101,
      (byte) 17,
      (byte) 143,
      (byte) 22,
      (byte) 233,
      (byte) 216,
      (byte) 57,
      (byte) 176 /*0xB0*/,
      (byte) 155,
      (byte) 163,
      (byte) 73,
      (byte) 144 /*0x90*/,
      (byte) 205,
      (byte) 202,
      (byte) 196,
      (byte) 161,
      (byte) 44
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[13];
    byte[] response2 = new byte[13];
    Array.Copy((Array) sc_12555.sspq, 46, (Array) numArray2, 0, 13);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12555.sspr, 46, (Array) numArray2, 0, 13);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_12560(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 199;
    sourceArray1[3] = (byte) 61;
    sourceArray1[2] = (byte) 8;
    sourceArray1[41] = (byte) 247;
    sourceArray1[4] = (byte) 210;
    sourceArray1[26] = (byte) 191;
    sourceArray1[40] = (byte) 66;
    sourceArray1[5] = (byte) 40;
    sourceArray1[8] = (byte) 233;
    sourceArray1[39] = (byte) 254;
    sourceArray1[34] = (byte) 126;
    sourceArray1[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    sourceArray1[12] = (byte) 41;
    sourceArray1[13] = (byte) 18;
    sourceArray1[22] = (byte) 138;
    sourceArray1[7] = (byte) 164;
    sourceArray1[18] = (byte) 112 /*0x70*/;
    sourceArray1[17] = (byte) 31 /*0x1F*/;
    sourceArray1[11] = (byte) 213;
    sourceArray1[19] = (byte) 20;
    sourceArray1[28] = (byte) 88;
    sourceArray1[21] = (byte) 126;
    sourceArray1[47] = (byte) 99;
    sourceArray1[23] = (byte) 184;
    sourceArray1[0] = (byte) 54;
    sourceArray1[25] = (byte) 181;
    sourceArray1[6] = (byte) 59;
    sourceArray1[24] = (byte) 234;
    sourceArray1[14] = (byte) 43;
    sourceArray1[29] = (byte) 176 /*0xB0*/;
    sourceArray1[46] = (byte) 36;
    sourceArray1[31 /*0x1F*/] = (byte) 237;
    sourceArray1[32 /*0x20*/] = (byte) 122;
    sourceArray1[9] = (byte) 170;
    sourceArray1[10] = (byte) 232;
    sourceArray1[43] = (byte) 81;
    sourceArray1[15] = (byte) 9;
    sourceArray1[37] = (byte) 137;
    sourceArray1[38] = (byte) 3;
    sourceArray1[35] = (byte) 124;
    sourceArray1[1] = (byte) 189;
    sourceArray1[36] = (byte) 26;
    sourceArray1[42] = (byte) 32 /*0x20*/;
    sourceArray1[20] = (byte) 3;
    sourceArray1[44] = (byte) 64 /*0x40*/;
    sourceArray1[45] = (byte) 26;
    sourceArray1[33] = (byte) 43;
    sourceArray1[27] = (byte) 178;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 102;
    sourceArray2[8] = (byte) 167;
    sourceArray2[24] = (byte) 239;
    sourceArray2[3] = (byte) 239;
    sourceArray2[6] = (byte) 230;
    sourceArray2[16 /*0x10*/] = (byte) 168;
    sourceArray2[11] = (byte) 148;
    sourceArray2[47] = (byte) 134;
    sourceArray2[12] = (byte) 165;
    sourceArray2[15] = (byte) 21;
    sourceArray2[10] = (byte) 88;
    sourceArray2[36] = (byte) 194;
    sourceArray2[31 /*0x1F*/] = (byte) 51;
    sourceArray2[17] = (byte) 93;
    sourceArray2[20] = (byte) 226;
    sourceArray2[23] = (byte) 157;
    sourceArray2[4] = (byte) 71;
    sourceArray2[9] = (byte) 14;
    sourceArray2[18] = (byte) 253;
    sourceArray2[27] = (byte) 56;
    sourceArray2[41] = (byte) 117;
    sourceArray2[46] = (byte) 20;
    sourceArray2[22] = (byte) 12;
    sourceArray2[5] = (byte) 74;
    sourceArray2[2] = (byte) 74;
    sourceArray2[25] = (byte) 69;
    sourceArray2[26] = (byte) 140;
    sourceArray2[34] = (byte) 55;
    sourceArray2[28] = (byte) 107;
    sourceArray2[29] = (byte) 213;
    sourceArray2[30] = (byte) 206;
    sourceArray2[32 /*0x20*/] = (byte) 229;
    sourceArray2[21] = (byte) 73;
    sourceArray2[33] = (byte) 47;
    sourceArray2[14] = (byte) 64 /*0x40*/;
    sourceArray2[35] = (byte) 38;
    sourceArray2[19] = (byte) 20;
    sourceArray2[37] = (byte) 190;
    sourceArray2[38] = (byte) 226;
    sourceArray2[13] = (byte) 2;
    sourceArray2[40] = (byte) 88;
    sourceArray2[0] = (byte) 28;
    sourceArray2[42] = (byte) 205;
    sourceArray2[43] = (byte) 153;
    sourceArray2[44] = (byte) 160 /*0xA0*/;
    sourceArray2[45] = (byte) 187;
    sourceArray2[39] = (byte) 198;
    sourceArray2[1] = (byte) 94;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12561()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[153];
      byte[] numArray2 = new byte[55]
      {
        (byte) 91,
        (byte) 135,
        (byte) 167,
        (byte) 215,
        (byte) 252,
        (byte) 109,
        (byte) 90,
        (byte) 176 /*0xB0*/,
        (byte) 235,
        (byte) 2,
        (byte) 39,
        (byte) 205,
        (byte) 234,
        (byte) 149,
        (byte) 141,
        (byte) 67,
        (byte) 253,
        (byte) 228,
        (byte) 47,
        (byte) 163,
        (byte) 114,
        (byte) 32 /*0x20*/,
        (byte) 130,
        (byte) 42,
        (byte) 78,
        (byte) 167,
        (byte) 235,
        (byte) 67,
        (byte) 28,
        (byte) 213,
        (byte) 97,
        (byte) 23,
        (byte) 46,
        (byte) 108,
        (byte) 218,
        (byte) 84,
        (byte) 112 /*0x70*/,
        (byte) 137,
        (byte) 121,
        (byte) 93,
        (byte) 216,
        (byte) 96 /*0x60*/,
        (byte) 128 /*0x80*/,
        (byte) 136,
        (byte) 193,
        (byte) 184,
        (byte) 88,
        (byte) 128 /*0x80*/,
        (byte) 25,
        (byte) 175,
        (byte) 104,
        (byte) 135,
        (byte) 31 /*0x1F*/,
        (byte) 80 /*0x50*/,
        (byte) 65
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 200,
        (byte) 138,
        (byte) 237,
        (byte) 204,
        (byte) 114,
        (byte) 245,
        (byte) 13,
        (byte) 111,
        (byte) 50,
        (byte) 39,
        (byte) 3,
        (byte) 126,
        (byte) 30,
        (byte) 227,
        (byte) 58,
        (byte) 33,
        (byte) 193,
        (byte) 4,
        (byte) 48 /*0x30*/,
        (byte) 144 /*0x90*/,
        (byte) 25,
        (byte) 6,
        (byte) 182,
        (byte) 67,
        (byte) 174,
        (byte) 144 /*0x90*/,
        (byte) 153,
        (byte) 6,
        (byte) 22,
        (byte) 158,
        (byte) 147,
        (byte) 117,
        (byte) 94,
        (byte) 87,
        (byte) 0,
        (byte) 104,
        (byte) 204,
        (byte) 173,
        (byte) 220,
        (byte) 66,
        (byte) 245,
        (byte) 53,
        (byte) 204,
        (byte) 55,
        (byte) 109,
        (byte) 194,
        (byte) 60,
        (byte) 206,
        (byte) 115,
        (byte) 76,
        (byte) 236,
        (byte) 170,
        (byte) 128 /*0x80*/,
        (byte) 209,
        (byte) 148
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 124,
        (byte) 137,
        (byte) 84,
        (byte) 129,
        (byte) 23,
        (byte) 148,
        (byte) 21,
        (byte) 160 /*0xA0*/,
        (byte) 16 /*0x10*/,
        (byte) 158,
        (byte) 126,
        (byte) 186,
        (byte) 56,
        (byte) 210,
        (byte) 115,
        (byte) 106,
        (byte) 210,
        (byte) 203,
        (byte) 18,
        (byte) 159,
        (byte) 249,
        (byte) 105,
        (byte) 149,
        (byte) 79,
        (byte) 107,
        (byte) 41,
        (byte) 108,
        (byte) 109,
        (byte) 218,
        (byte) 248,
        (byte) 222,
        (byte) 42,
        (byte) 159,
        (byte) 79,
        (byte) 243,
        (byte) 0,
        (byte) 36,
        (byte) 219,
        (byte) 192 /*0xC0*/,
        (byte) 203,
        (byte) 106,
        (byte) 232,
        (byte) 176 /*0xB0*/,
        (byte) 25,
        (byte) 217,
        (byte) 197,
        (byte) 224 /*0xE0*/,
        (byte) 220,
        (byte) 244,
        (byte) 28,
        (byte) 76,
        (byte) 133,
        (byte) 46,
        (byte) 155,
        (byte) 11
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 58,
        (byte) 195,
        (byte) 54,
        (byte) 174,
        (byte) 114,
        (byte) 7,
        (byte) 66,
        (byte) 199,
        (byte) 163,
        (byte) 5,
        (byte) 179,
        (byte) 106,
        (byte) 50,
        (byte) 246,
        (byte) 6,
        (byte) 44,
        (byte) 16 /*0x10*/,
        (byte) 54,
        (byte) 91,
        (byte) 147,
        (byte) 48 /*0x30*/,
        (byte) 161,
        (byte) 253,
        (byte) 86,
        (byte) 61,
        (byte) 83,
        (byte) 151,
        (byte) 246,
        (byte) 77,
        (byte) 104,
        (byte) 238,
        (byte) 209,
        (byte) 193,
        (byte) 159,
        (byte) 211,
        (byte) 83,
        (byte) 218,
        (byte) 183,
        (byte) 81,
        (byte) 252,
        (byte) 148,
        (byte) 46,
        (byte) 213,
        (byte) 156,
        (byte) 75,
        (byte) 175,
        (byte) 34,
        (byte) 93,
        (byte) 213,
        (byte) 57,
        (byte) 48 /*0x30*/,
        (byte) 153,
        (byte) 232,
        (byte) 234,
        (byte) 27
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[43];
      numArray6[5] = (byte) 198;
      numArray6[1] = (byte) 21;
      numArray6[2] = (byte) 237;
      numArray6[34] = (byte) 31 /*0x1F*/;
      numArray6[38] = (byte) 195;
      numArray6[35] = (byte) 198;
      numArray6[6] = (byte) 24;
      numArray6[22] = (byte) 166;
      numArray6[8] = (byte) 83;
      numArray6[16 /*0x10*/] = (byte) 71;
      numArray6[10] = (byte) 233;
      numArray6[25] = (byte) 121;
      numArray6[33] = (byte) 12;
      numArray6[13] = (byte) 70;
      numArray6[3] = (byte) 39;
      numArray6[15] = (byte) 11;
      numArray6[20] = (byte) 124;
      numArray6[0] = (byte) 5;
      numArray6[18] = (byte) 17;
      numArray6[19] = (byte) 184;
      numArray6[24] = (byte) 144 /*0x90*/;
      numArray6[21] = (byte) 118;
      numArray6[42] = (byte) 133;
      numArray6[23] = (byte) 73;
      numArray6[14] = (byte) 246;
      numArray6[26] = (byte) 189;
      numArray6[17] = (byte) 53;
      numArray6[27] = (byte) 171;
      numArray6[39] = (byte) 34;
      numArray6[7] = (byte) 192 /*0xC0*/;
      numArray6[30] = (byte) 4;
      numArray6[29] = (byte) 251;
      numArray6[32 /*0x20*/] = (byte) 224 /*0xE0*/;
      numArray6[11] = (byte) 222;
      numArray6[28] = (byte) 89;
      numArray6[31 /*0x1F*/] = (byte) 68;
      numArray6[12] = (byte) 115;
      numArray6[37] = (byte) 163;
      numArray6[36] = (byte) 210;
      numArray6[9] = (byte) 27;
      numArray6[40] = (byte) 152;
      numArray6[41] = (byte) 88;
      numArray6[4] = (byte) 167;
      byte[] numArray7 = new byte[43]
      {
        (byte) 166,
        (byte) 47,
        (byte) 247,
        (byte) 209,
        (byte) 131,
        (byte) 244,
        (byte) 28,
        (byte) 158,
        (byte) 220,
        (byte) 241,
        (byte) 77,
        (byte) 223,
        (byte) 52,
        (byte) 164,
        (byte) 235,
        (byte) 249,
        (byte) 112 /*0x70*/,
        (byte) 227,
        (byte) 10,
        (byte) 19,
        (byte) 106,
        (byte) 19,
        (byte) 209,
        (byte) 216,
        (byte) 73,
        (byte) 45,
        (byte) 235,
        (byte) 250,
        (byte) 146,
        (byte) 83,
        (byte) 218,
        (byte) 218,
        (byte) 251,
        (byte) 163,
        (byte) 230,
        (byte) 30,
        (byte) 209,
        (byte) 74,
        (byte) 131,
        (byte) 110,
        (byte) 203,
        (byte) 193,
        (byte) 249
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[153];
    byte[] numArray9 = new byte[55]
    {
      (byte) 144 /*0x90*/,
      (byte) 87,
      (byte) 46,
      (byte) 12,
      (byte) 149,
      (byte) 25,
      (byte) 43,
      (byte) 241,
      (byte) 45,
      (byte) 145,
      (byte) 82,
      byte.MaxValue,
      (byte) 24,
      (byte) 175,
      (byte) 36,
      (byte) 65,
      (byte) 68,
      (byte) 244,
      (byte) 11,
      (byte) 58,
      (byte) 83,
      (byte) 158,
      (byte) 114,
      (byte) 82,
      (byte) 4,
      (byte) 52,
      (byte) 34,
      (byte) 22,
      (byte) 136,
      (byte) 67,
      (byte) 121,
      (byte) 90,
      (byte) 121,
      (byte) 209,
      (byte) 150,
      (byte) 217,
      (byte) 10,
      (byte) 150,
      (byte) 220,
      (byte) 213,
      (byte) 244,
      (byte) 50,
      (byte) 194,
      (byte) 106,
      byte.MaxValue,
      (byte) 147,
      (byte) 224 /*0xE0*/,
      (byte) 54,
      (byte) 49,
      (byte) 158,
      (byte) 207,
      (byte) 169,
      (byte) 62,
      (byte) 75,
      (byte) 66
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 254,
      (byte) 40,
      (byte) 14,
      (byte) 64 /*0x40*/,
      (byte) 206,
      (byte) 149,
      (byte) 56,
      (byte) 177,
      (byte) 30,
      (byte) 241,
      (byte) 83,
      (byte) 106,
      (byte) 227,
      (byte) 3,
      (byte) 185,
      (byte) 22,
      (byte) 47,
      (byte) 69,
      (byte) 6,
      (byte) 1,
      (byte) 129,
      (byte) 170,
      (byte) 65,
      (byte) 46,
      (byte) 59,
      (byte) 183,
      (byte) 147,
      (byte) 215,
      (byte) 114,
      (byte) 201,
      (byte) 27,
      (byte) 119,
      (byte) 59,
      (byte) 105,
      (byte) 89,
      (byte) 13,
      (byte) 182,
      (byte) 33,
      (byte) 86,
      (byte) 51,
      (byte) 43,
      (byte) 78,
      (byte) 248,
      (byte) 141,
      (byte) 140,
      (byte) 2,
      (byte) 68,
      (byte) 129,
      (byte) 148,
      (byte) 122,
      (byte) 121,
      (byte) 158,
      (byte) 95,
      (byte) 62,
      (byte) 68
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[47] = (byte) 103;
    numArray11[35] = (byte) 142;
    numArray11[2] = (byte) 96 /*0x60*/;
    numArray11[3] = (byte) 246;
    numArray11[54] = (byte) 220;
    numArray11[1] = (byte) 8;
    numArray11[36] = (byte) 124;
    numArray11[7] = (byte) 24;
    numArray11[37] = (byte) 223;
    numArray11[33] = (byte) 244;
    numArray11[45] = (byte) 29;
    numArray11[5] = (byte) 105;
    numArray11[26] = (byte) 233;
    numArray11[41] = (byte) 82;
    numArray11[14] = (byte) 174;
    numArray11[15] = (byte) 24;
    numArray11[12] = (byte) 75;
    numArray11[38] = (byte) 222;
    numArray11[4] = (byte) 21;
    numArray11[21] = (byte) 83;
    numArray11[20] = (byte) 7;
    numArray11[9] = (byte) 133;
    numArray11[8] = (byte) 248;
    numArray11[23] = (byte) 84;
    numArray11[24] = (byte) 235;
    numArray11[25] = (byte) 228;
    numArray11[19] = (byte) 219;
    numArray11[27] = (byte) 116;
    numArray11[16 /*0x10*/] = (byte) 53;
    numArray11[29] = (byte) 27;
    numArray11[30] = (byte) 46;
    numArray11[31 /*0x1F*/] = (byte) 143;
    numArray11[32 /*0x20*/] = (byte) 165;
    numArray11[18] = (byte) 33;
    numArray11[34] = (byte) 46;
    numArray11[52] = (byte) 33;
    numArray11[6] = (byte) 233;
    numArray11[48 /*0x30*/] = (byte) 33;
    numArray11[17] = (byte) 69;
    numArray11[39] = (byte) 201;
    numArray11[40] = (byte) 216;
    numArray11[50] = (byte) 9;
    numArray11[0] = (byte) 219;
    numArray11[43] = (byte) 13;
    numArray11[44] = (byte) 152;
    numArray11[42] = (byte) 6;
    numArray11[46] = (byte) 230;
    numArray11[10] = (byte) 71;
    numArray11[28] = (byte) 208 /*0xD0*/;
    numArray11[11] = (byte) 24;
    numArray11[13] = (byte) 54;
    numArray11[51] = (byte) 5;
    numArray11[53] = (byte) 21;
    numArray11[49] = (byte) 145;
    numArray11[22] = (byte) 112 /*0x70*/;
    byte[] numArray12 = new byte[55]
    {
      (byte) 24,
      (byte) 202,
      (byte) 250,
      (byte) 227,
      (byte) 171,
      (byte) 133,
      (byte) 1,
      (byte) 232,
      (byte) 64 /*0x40*/,
      (byte) 10,
      (byte) 107,
      (byte) 155,
      (byte) 149,
      (byte) 125,
      (byte) 74,
      (byte) 253,
      (byte) 96 /*0x60*/,
      (byte) 144 /*0x90*/,
      (byte) 232,
      (byte) 183,
      (byte) 35,
      (byte) 16 /*0x10*/,
      (byte) 145,
      (byte) 65,
      (byte) 193,
      (byte) 74,
      (byte) 185,
      (byte) 142,
      (byte) 93,
      (byte) 234,
      (byte) 46,
      (byte) 122,
      (byte) 136,
      (byte) 150,
      (byte) 67,
      (byte) 67,
      (byte) 19,
      (byte) 181,
      (byte) 213,
      (byte) 8,
      (byte) 32 /*0x20*/,
      (byte) 45,
      (byte) 115,
      (byte) 130,
      (byte) 223,
      (byte) 69,
      (byte) 126,
      (byte) 185,
      (byte) 190,
      (byte) 53,
      (byte) 159,
      (byte) 29,
      (byte) 47,
      (byte) 48 /*0x30*/,
      (byte) 215
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[43]
    {
      (byte) 79,
      (byte) 60,
      (byte) 52,
      (byte) 252,
      (byte) 33,
      (byte) 8,
      (byte) 203,
      (byte) 46,
      (byte) 73,
      (byte) 227,
      (byte) 61,
      (byte) 98,
      (byte) 40,
      (byte) 43,
      (byte) 129,
      (byte) 186,
      (byte) 13,
      (byte) 159,
      (byte) 170,
      (byte) 33,
      (byte) 173,
      (byte) 219,
      (byte) 35,
      (byte) 108,
      (byte) 35,
      (byte) 81,
      (byte) 73,
      (byte) 93,
      (byte) 139,
      (byte) 164,
      (byte) 125,
      (byte) 240 /*0xF0*/,
      (byte) 104,
      (byte) 194,
      (byte) 33,
      (byte) 151,
      (byte) 120,
      (byte) 77,
      (byte) 65,
      (byte) 2,
      (byte) 95,
      (byte) 135,
      (byte) 150
    };
    byte[] numArray14 = new byte[43];
    numArray14[31 /*0x1F*/] = (byte) 135;
    numArray14[21] = (byte) 172;
    numArray14[10] = (byte) 216;
    numArray14[0] = (byte) 48 /*0x30*/;
    numArray14[32 /*0x20*/] = (byte) 110;
    numArray14[3] = (byte) 184;
    numArray14[6] = (byte) 187;
    numArray14[7] = (byte) 98;
    numArray14[5] = (byte) 147;
    numArray14[9] = (byte) 97;
    numArray14[42] = (byte) 153;
    numArray14[25] = (byte) 245;
    numArray14[12] = (byte) 116;
    numArray14[13] = (byte) 240 /*0xF0*/;
    numArray14[1] = (byte) 238;
    numArray14[28] = (byte) 172;
    numArray14[16 /*0x10*/] = (byte) 38;
    numArray14[17] = (byte) 74;
    numArray14[18] = (byte) 12;
    numArray14[19] = (byte) 215;
    numArray14[20] = (byte) 241;
    numArray14[38] = (byte) 111;
    numArray14[22] = (byte) 205;
    numArray14[4] = (byte) 32 /*0x20*/;
    numArray14[2] = (byte) 128 /*0x80*/;
    numArray14[36] = (byte) 107;
    numArray14[26] = (byte) 118;
    numArray14[29] = (byte) 164;
    numArray14[24] = (byte) 116;
    numArray14[11] = (byte) 250;
    numArray14[33] = (byte) 242;
    numArray14[14] = (byte) 95;
    numArray14[23] = (byte) 182;
    numArray14[30] = (byte) 13;
    numArray14[34] = (byte) 37;
    numArray14[35] = (byte) 181;
    numArray14[8] = (byte) 22;
    numArray14[37] = (byte) 214;
    numArray14[39] = (byte) 254;
    numArray14[15] = (byte) 74;
    numArray14[40] = (byte) 112 /*0x70*/;
    numArray14[41] = (byte) 82;
    numArray14[27] = (byte) 78;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 43);
    for (int index = 0; index < 43; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[50];
    byte[] response = new byte[50];
    Array.Copy((Array) sc_12555.sspq, 59, (Array) numArray15, 0, 50);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12555.sspr, 59, (Array) numArray15, 0, 50);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12562()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[121];
      byte[] numArray2 = new byte[55]
      {
        (byte) 156,
        (byte) 245,
        (byte) 219,
        (byte) 125,
        (byte) 194,
        (byte) 3,
        (byte) 192 /*0xC0*/,
        (byte) 26,
        (byte) 5,
        (byte) 176 /*0xB0*/,
        (byte) 231,
        (byte) 54,
        (byte) 7,
        (byte) 42,
        (byte) 87,
        (byte) 73,
        (byte) 98,
        (byte) 221,
        (byte) 17,
        (byte) 173,
        (byte) 41,
        (byte) 115,
        (byte) 135,
        (byte) 30,
        (byte) 221,
        (byte) 193,
        (byte) 153,
        (byte) 143,
        (byte) 80 /*0x50*/,
        (byte) 126,
        (byte) 94,
        (byte) 246,
        (byte) 204,
        (byte) 101,
        (byte) 62,
        (byte) 182,
        (byte) 78,
        (byte) 224 /*0xE0*/,
        (byte) 169,
        (byte) 141,
        (byte) 140,
        (byte) 209,
        (byte) 93,
        (byte) 31 /*0x1F*/,
        (byte) 91,
        (byte) 28,
        (byte) 243,
        (byte) 234,
        (byte) 204,
        (byte) 188,
        (byte) 92,
        (byte) 6,
        (byte) 244,
        (byte) 65,
        (byte) 204
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 48 /*0x30*/,
        (byte) 117,
        (byte) 1,
        (byte) 79,
        (byte) 27,
        (byte) 160 /*0xA0*/,
        (byte) 164,
        (byte) 31 /*0x1F*/,
        (byte) 229,
        (byte) 16 /*0x10*/,
        (byte) 100,
        (byte) 173,
        (byte) 199,
        (byte) 203,
        (byte) 130,
        (byte) 197,
        (byte) 97,
        (byte) 30,
        (byte) 14,
        (byte) 113,
        (byte) 216,
        (byte) 213,
        (byte) 53,
        (byte) 33,
        (byte) 60,
        (byte) 29,
        (byte) 141,
        (byte) 80 /*0x50*/,
        (byte) 80 /*0x50*/,
        (byte) 76,
        (byte) 3,
        (byte) 175,
        (byte) 166,
        (byte) 89,
        (byte) 178,
        (byte) 210,
        (byte) 175,
        (byte) 128 /*0x80*/,
        (byte) 244,
        (byte) 222,
        (byte) 6,
        (byte) 119,
        (byte) 18,
        (byte) 108,
        (byte) 12,
        (byte) 121,
        (byte) 165,
        (byte) 112 /*0x70*/,
        (byte) 119,
        (byte) 106,
        (byte) 193,
        (byte) 6,
        (byte) 206,
        (byte) 171,
        (byte) 114
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 188,
        (byte) 160 /*0xA0*/,
        (byte) 0,
        (byte) 106,
        (byte) 49,
        (byte) 202,
        (byte) 135,
        (byte) 82,
        (byte) 240 /*0xF0*/,
        (byte) 106,
        (byte) 162,
        (byte) 235,
        (byte) 160 /*0xA0*/,
        (byte) 250,
        (byte) 130,
        (byte) 51,
        (byte) 131,
        (byte) 33,
        (byte) 88,
        (byte) 22,
        (byte) 104,
        (byte) 194,
        (byte) 192 /*0xC0*/,
        (byte) 206,
        (byte) 174,
        (byte) 195,
        (byte) 147,
        (byte) 33,
        (byte) 88,
        (byte) 2,
        (byte) 138,
        (byte) 153,
        (byte) 21,
        (byte) 73,
        (byte) 145,
        (byte) 63 /*0x3F*/,
        (byte) 168,
        (byte) 35,
        (byte) 91,
        (byte) 7,
        (byte) 214,
        (byte) 205,
        (byte) 106,
        (byte) 177,
        (byte) 81,
        (byte) 254,
        (byte) 43,
        (byte) 194,
        (byte) 218,
        (byte) 210,
        (byte) 112 /*0x70*/,
        (byte) 32 /*0x20*/,
        (byte) 116,
        (byte) 19,
        (byte) 5
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 137,
        (byte) 20,
        (byte) 9,
        (byte) 157,
        (byte) 80 /*0x50*/,
        (byte) 103,
        (byte) 41,
        (byte) 109,
        (byte) 144 /*0x90*/,
        (byte) 27,
        (byte) 200,
        (byte) 10,
        (byte) 199,
        (byte) 2,
        (byte) 244,
        (byte) 239,
        (byte) 242,
        (byte) 78,
        (byte) 69,
        (byte) 150,
        (byte) 193,
        (byte) 78,
        (byte) 103,
        (byte) 187,
        (byte) 215,
        (byte) 233,
        (byte) 169,
        (byte) 79,
        (byte) 207,
        (byte) 76,
        (byte) 49,
        (byte) 12,
        (byte) 2,
        (byte) 209,
        (byte) 223,
        (byte) 210,
        (byte) 210,
        (byte) 129,
        (byte) 121,
        (byte) 118,
        (byte) 158,
        (byte) 235,
        (byte) 70,
        (byte) 238,
        (byte) 63 /*0x3F*/,
        (byte) 196,
        (byte) 48 /*0x30*/,
        (byte) 79,
        (byte) 42,
        (byte) 176 /*0xB0*/,
        (byte) 204,
        (byte) 50,
        (byte) 30,
        (byte) 128 /*0x80*/,
        (byte) 237
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[11];
      numArray6[10] = (byte) 87;
      numArray6[7] = (byte) 223;
      numArray6[5] = (byte) 250;
      numArray6[3] = (byte) 54;
      numArray6[4] = (byte) 101;
      numArray6[8] = (byte) 8;
      numArray6[6] = (byte) 238;
      numArray6[0] = (byte) 50;
      numArray6[1] = (byte) 187;
      numArray6[2] = (byte) 110;
      numArray6[9] = (byte) 214;
      byte[] numArray7 = new byte[11]
      {
        (byte) 230,
        (byte) 20,
        (byte) 57,
        (byte) 16 /*0x10*/,
        (byte) 46,
        (byte) 4,
        (byte) 4,
        (byte) 95,
        (byte) 169,
        (byte) 107,
        (byte) 134
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[121];
    byte[] numArray9 = new byte[55]
    {
      (byte) 71,
      (byte) 133,
      (byte) 146,
      (byte) 106,
      (byte) 37,
      (byte) 2,
      (byte) 67,
      (byte) 151,
      (byte) 96 /*0x60*/,
      (byte) 118,
      (byte) 104,
      (byte) 237,
      (byte) 208 /*0xD0*/,
      (byte) 112 /*0x70*/,
      (byte) 211,
      (byte) 124,
      (byte) 210,
      (byte) 72,
      (byte) 39,
      (byte) 212,
      (byte) 89,
      (byte) 151,
      (byte) 71,
      (byte) 15,
      (byte) 90,
      (byte) 35,
      (byte) 211,
      (byte) 106,
      (byte) 161,
      (byte) 208 /*0xD0*/,
      (byte) 181,
      (byte) 10,
      (byte) 214,
      (byte) 101,
      (byte) 29,
      (byte) 116,
      (byte) 92,
      (byte) 117,
      (byte) 227,
      (byte) 212,
      (byte) 19,
      (byte) 209,
      (byte) 226,
      (byte) 20,
      (byte) 248,
      (byte) 217,
      (byte) 127 /*0x7F*/,
      (byte) 207,
      (byte) 199,
      (byte) 220,
      (byte) 60,
      (byte) 52,
      (byte) 136,
      (byte) 194,
      (byte) 119
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 10,
      (byte) 84,
      (byte) 125,
      (byte) 52,
      (byte) 211,
      (byte) 222,
      (byte) 178,
      (byte) 222,
      (byte) 49,
      (byte) 64 /*0x40*/,
      (byte) 162,
      (byte) 44,
      (byte) 80 /*0x50*/,
      (byte) 243,
      (byte) 47,
      (byte) 61,
      (byte) 174,
      (byte) 251,
      (byte) 249,
      (byte) 164,
      (byte) 87,
      (byte) 223,
      (byte) 42,
      (byte) 238,
      (byte) 148,
      (byte) 212,
      (byte) 197,
      (byte) 51,
      (byte) 194,
      (byte) 210,
      (byte) 165,
      (byte) 184,
      (byte) 144 /*0x90*/,
      (byte) 65,
      (byte) 79,
      (byte) 31 /*0x1F*/,
      (byte) 158,
      (byte) 217,
      (byte) 168,
      (byte) 12,
      (byte) 39,
      (byte) 204,
      (byte) 194,
      (byte) 172,
      (byte) 2,
      (byte) 154,
      (byte) 132,
      (byte) 147,
      (byte) 74,
      (byte) 46,
      (byte) 66,
      (byte) 86,
      (byte) 117,
      (byte) 242,
      (byte) 253
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[30] = (byte) 20;
    numArray11[1] = (byte) 16 /*0x10*/;
    numArray11[24] = (byte) 3;
    numArray11[39] = (byte) 219;
    numArray11[4] = (byte) 216;
    numArray11[33] = (byte) 54;
    numArray11[28] = (byte) 129;
    numArray11[7] = (byte) 193;
    numArray11[42] = (byte) 155;
    numArray11[9] = (byte) 22;
    numArray11[10] = (byte) 107;
    numArray11[41] = (byte) 82;
    numArray11[43] = (byte) 195;
    numArray11[3] = (byte) 47;
    numArray11[0] = (byte) 130;
    numArray11[15] = (byte) 100;
    numArray11[16 /*0x10*/] = (byte) 83;
    numArray11[52] = (byte) 238;
    numArray11[18] = (byte) 136;
    numArray11[26] = (byte) 197;
    numArray11[20] = (byte) 51;
    numArray11[36] = (byte) 225;
    numArray11[50] = (byte) 53;
    numArray11[23] = (byte) 163;
    numArray11[45] = (byte) 124;
    numArray11[40] = (byte) 82;
    numArray11[21] = (byte) 22;
    numArray11[27] = (byte) 147;
    numArray11[25] = (byte) 163;
    numArray11[29] = (byte) 129;
    numArray11[11] = (byte) 82;
    numArray11[31 /*0x1F*/] = (byte) 237;
    numArray11[2] = (byte) 1;
    numArray11[53] = (byte) 251;
    numArray11[34] = (byte) 217;
    numArray11[12] = (byte) 94;
    numArray11[51] = (byte) 1;
    numArray11[54] = (byte) 10;
    numArray11[38] = (byte) 58;
    numArray11[5] = (byte) 245;
    numArray11[6] = (byte) 144 /*0x90*/;
    numArray11[47] = (byte) 188;
    numArray11[14] = (byte) 92;
    numArray11[8] = (byte) 93;
    numArray11[44] = (byte) 20;
    numArray11[17] = (byte) 193;
    numArray11[46] = (byte) 104;
    numArray11[22] = (byte) 128 /*0x80*/;
    numArray11[48 /*0x30*/] = (byte) 223;
    numArray11[49] = (byte) 207;
    numArray11[13] = (byte) 31 /*0x1F*/;
    numArray11[19] = (byte) 181;
    numArray11[37] = (byte) 165;
    numArray11[32 /*0x20*/] = (byte) 109;
    numArray11[35] = (byte) 101;
    byte[] numArray12 = new byte[55]
    {
      (byte) 83,
      (byte) 32 /*0x20*/,
      (byte) 66,
      (byte) 131,
      (byte) 21,
      (byte) 70,
      (byte) 160 /*0xA0*/,
      (byte) 53,
      (byte) 156,
      (byte) 173,
      (byte) 86,
      (byte) 134,
      (byte) 147,
      (byte) 252,
      (byte) 1,
      (byte) 16 /*0x10*/,
      (byte) 195,
      (byte) 150,
      (byte) 153,
      (byte) 195,
      (byte) 236,
      (byte) 112 /*0x70*/,
      (byte) 84,
      (byte) 53,
      (byte) 174,
      (byte) 20,
      (byte) 24,
      (byte) 206,
      (byte) 175,
      (byte) 247,
      (byte) 139,
      (byte) 77,
      (byte) 195,
      (byte) 0,
      (byte) 38,
      (byte) 50,
      (byte) 209,
      (byte) 140,
      (byte) 3,
      (byte) 93,
      (byte) 59,
      (byte) 48 /*0x30*/,
      (byte) 223,
      (byte) 213,
      (byte) 71,
      (byte) 66,
      (byte) 156,
      (byte) 226,
      (byte) 9,
      (byte) 199,
      (byte) 37,
      (byte) 159,
      (byte) 81,
      (byte) 120,
      (byte) 138
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[11];
    numArray13[6] = (byte) 143;
    numArray13[9] = (byte) 114;
    numArray13[8] = (byte) 127 /*0x7F*/;
    numArray13[3] = (byte) 38;
    numArray13[7] = (byte) 239;
    numArray13[0] = (byte) 207;
    numArray13[5] = (byte) 142;
    numArray13[2] = (byte) 249;
    numArray13[4] = (byte) 71;
    numArray13[1] = (byte) 164;
    numArray13[10] = (byte) 253;
    byte[] numArray14 = new byte[11]
    {
      (byte) 54,
      (byte) 104,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 231,
      (byte) 60,
      (byte) 138,
      (byte) 242,
      (byte) 12,
      (byte) 45,
      (byte) 197
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 11);
    for (int index = 0; index < 11; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
