// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12507
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12507
{
  private static byte[] sspq = new byte[134]
  {
    (byte) 100,
    (byte) 105,
    (byte) 175,
    (byte) 184,
    (byte) 107,
    (byte) 156,
    (byte) 43,
    (byte) 176 /*0xB0*/,
    (byte) 93,
    (byte) 246,
    (byte) 179,
    (byte) 41,
    (byte) 39,
    (byte) 66,
    (byte) 112 /*0x70*/,
    (byte) 140,
    (byte) 173,
    (byte) 35,
    (byte) 65,
    (byte) 195,
    (byte) 105,
    (byte) 205,
    (byte) 66,
    (byte) 112 /*0x70*/,
    (byte) 31 /*0x1F*/,
    (byte) 45,
    (byte) 26,
    (byte) 216,
    (byte) 253,
    (byte) 49,
    (byte) 220,
    (byte) 171,
    (byte) 193,
    (byte) 60,
    (byte) 234,
    (byte) 201,
    (byte) 131,
    (byte) 132,
    (byte) 118,
    (byte) 104,
    (byte) 132,
    (byte) 238,
    (byte) 158,
    (byte) 139,
    (byte) 213,
    (byte) 16 /*0x10*/,
    (byte) 167,
    (byte) 197,
    (byte) 197,
    (byte) 46,
    (byte) 146,
    (byte) 158,
    (byte) 235,
    (byte) 110,
    (byte) 150,
    (byte) 57,
    (byte) 129,
    (byte) 211,
    (byte) 231,
    (byte) 137,
    (byte) 193,
    (byte) 14,
    (byte) 111,
    (byte) 15,
    (byte) 54,
    (byte) 80 /*0x50*/,
    (byte) 209,
    (byte) 137,
    (byte) 198,
    (byte) 83,
    (byte) 68,
    (byte) 82,
    (byte) 18,
    (byte) 102,
    (byte) 163,
    (byte) 183,
    (byte) 169,
    (byte) 190,
    (byte) 225,
    (byte) 77,
    (byte) 125,
    (byte) 72,
    (byte) 61,
    (byte) 252,
    (byte) 65,
    (byte) 149,
    (byte) 97,
    (byte) 13,
    (byte) 151,
    (byte) 174,
    (byte) 230,
    (byte) 246,
    (byte) 117,
    (byte) 87,
    (byte) 127 /*0x7F*/,
    (byte) 203,
    (byte) 149,
    (byte) 34,
    (byte) 219,
    (byte) 122,
    (byte) 127 /*0x7F*/,
    (byte) 35,
    (byte) 254,
    (byte) 107,
    (byte) 57,
    (byte) 8,
    (byte) 109,
    (byte) 163,
    (byte) 177,
    (byte) 128 /*0x80*/,
    (byte) 252,
    (byte) 50,
    (byte) 232,
    (byte) 9,
    (byte) 111,
    (byte) 65,
    (byte) 87,
    (byte) 24,
    (byte) 59,
    (byte) 181,
    (byte) 67,
    (byte) 169,
    (byte) 94,
    (byte) 253,
    (byte) 219,
    (byte) 178,
    (byte) 106,
    (byte) 244,
    (byte) 138,
    (byte) 106,
    (byte) 223,
    (byte) 212,
    (byte) 44,
    (byte) 187
  };
  private static byte[] sspr = new byte[134]
  {
    (byte) 227,
    (byte) 71,
    (byte) 10,
    (byte) 62,
    (byte) 34,
    (byte) 35,
    (byte) 9,
    (byte) 40,
    (byte) 54,
    (byte) 15,
    (byte) 25,
    (byte) 5,
    (byte) 61,
    (byte) 150,
    byte.MaxValue,
    (byte) 81,
    (byte) 253,
    (byte) 53,
    (byte) 219,
    (byte) 154,
    (byte) 213,
    (byte) 160 /*0xA0*/,
    (byte) 80 /*0x50*/,
    (byte) 224 /*0xE0*/,
    (byte) 80 /*0x50*/,
    (byte) 43,
    (byte) 121,
    (byte) 9,
    (byte) 38,
    (byte) 247,
    (byte) 100,
    (byte) 91,
    (byte) 15,
    (byte) 9,
    (byte) 20,
    (byte) 159,
    (byte) 74,
    (byte) 250,
    (byte) 74,
    (byte) 8,
    (byte) 185,
    (byte) 56,
    (byte) 158,
    (byte) 134,
    (byte) 184,
    (byte) 69,
    (byte) 253,
    (byte) 78,
    (byte) 197,
    (byte) 165,
    (byte) 30,
    (byte) 70,
    (byte) 226,
    (byte) 243,
    (byte) 231,
    (byte) 82,
    (byte) 156,
    (byte) 207,
    (byte) 163,
    (byte) 231,
    (byte) 55,
    (byte) 119,
    (byte) 225,
    (byte) 140,
    (byte) 1,
    (byte) 176 /*0xB0*/,
    (byte) 185,
    (byte) 142,
    (byte) 196,
    (byte) 47,
    (byte) 233,
    (byte) 138,
    (byte) 135,
    (byte) 140,
    (byte) 184,
    (byte) 64 /*0x40*/,
    (byte) 84,
    (byte) 121,
    (byte) 121,
    (byte) 77,
    (byte) 231,
    (byte) 136,
    (byte) 1,
    (byte) 45,
    (byte) 129,
    (byte) 188,
    (byte) 155,
    (byte) 32 /*0x20*/,
    (byte) 135,
    (byte) 92,
    (byte) 106,
    (byte) 232,
    (byte) 6,
    (byte) 115,
    (byte) 66,
    (byte) 167,
    (byte) 15,
    (byte) 164,
    (byte) 69,
    (byte) 238,
    (byte) 119,
    (byte) 245,
    (byte) 223,
    (byte) 182,
    (byte) 124,
    (byte) 173,
    (byte) 85,
    (byte) 67,
    (byte) 35,
    (byte) 250,
    (byte) 89,
    (byte) 251,
    (byte) 103,
    (byte) 174,
    (byte) 17,
    (byte) 84,
    (byte) 241,
    (byte) 9,
    (byte) 139,
    (byte) 151,
    (byte) 3,
    (byte) 148,
    (byte) 160 /*0xA0*/,
    (byte) 147,
    (byte) 63 /*0x3F*/,
    (byte) 115,
    (byte) 114,
    (byte) 15,
    (byte) 126,
    (byte) 223,
    (byte) 93,
    (byte) 77,
    (byte) 48 /*0x30*/,
    (byte) 84
  };

  internal static int ssp_appserver_12508(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 97;
    sourceArray1[35] = (byte) 33;
    sourceArray1[0] = (byte) 144 /*0x90*/;
    sourceArray1[7] = (byte) 214;
    sourceArray1[13] = (byte) 62;
    sourceArray1[3] = (byte) 47;
    sourceArray1[19] = (byte) 154;
    sourceArray1[4] = (byte) 247;
    sourceArray1[8] = (byte) 12;
    sourceArray1[39] = (byte) 54;
    sourceArray1[10] = (byte) 115;
    sourceArray1[11] = (byte) 201;
    sourceArray1[30] = (byte) 74;
    sourceArray1[2] = (byte) 144 /*0x90*/;
    sourceArray1[1] = (byte) 103;
    sourceArray1[15] = (byte) 105;
    sourceArray1[16 /*0x10*/] = (byte) 216;
    sourceArray1[9] = (byte) 94;
    sourceArray1[40] = (byte) 168;
    sourceArray1[14] = (byte) 105;
    sourceArray1[17] = (byte) 4;
    sourceArray1[34] = (byte) 127 /*0x7F*/;
    sourceArray1[36] = (byte) 166;
    sourceArray1[23] = (byte) 160 /*0xA0*/;
    sourceArray1[24] = (byte) 246;
    sourceArray1[25] = (byte) 217;
    sourceArray1[26] = (byte) 164;
    sourceArray1[20] = (byte) 100;
    sourceArray1[28] = (byte) 221;
    sourceArray1[29] = (byte) 216;
    sourceArray1[31 /*0x1F*/] = (byte) 70;
    sourceArray1[46] = (byte) 16 /*0x10*/;
    sourceArray1[32 /*0x20*/] = (byte) 38;
    sourceArray1[33] = (byte) 247;
    sourceArray1[5] = (byte) 112 /*0x70*/;
    sourceArray1[27] = (byte) 196;
    sourceArray1[45] = (byte) 43;
    sourceArray1[37] = (byte) 54;
    sourceArray1[12] = (byte) 86;
    sourceArray1[44] = (byte) 32 /*0x20*/;
    sourceArray1[18] = (byte) 123;
    sourceArray1[41] = (byte) 141;
    sourceArray1[42] = (byte) 193;
    sourceArray1[43] = (byte) 117;
    sourceArray1[22] = (byte) 110;
    sourceArray1[21] = (byte) 37;
    sourceArray1[6] = (byte) 253;
    sourceArray1[47] = (byte) 49;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[17] = (byte) 16 /*0x10*/;
    sourceArray2[12] = (byte) 159;
    sourceArray2[40] = (byte) 25;
    sourceArray2[3] = (byte) 219;
    sourceArray2[7] = (byte) 149;
    sourceArray2[11] = (byte) 250;
    sourceArray2[15] = (byte) 208 /*0xD0*/;
    sourceArray2[28] = (byte) 214;
    sourceArray2[8] = (byte) 76;
    sourceArray2[9] = (byte) 167;
    sourceArray2[47] = (byte) 53;
    sourceArray2[30] = (byte) 254;
    sourceArray2[32 /*0x20*/] = (byte) 73;
    sourceArray2[0] = (byte) 245;
    sourceArray2[27] = (byte) 181;
    sourceArray2[14] = (byte) 50;
    sourceArray2[4] = byte.MaxValue;
    sourceArray2[6] = (byte) 174;
    sourceArray2[22] = (byte) 82;
    sourceArray2[19] = (byte) 198;
    sourceArray2[20] = (byte) 169;
    sourceArray2[21] = (byte) 64 /*0x40*/;
    sourceArray2[18] = (byte) 102;
    sourceArray2[23] = byte.MaxValue;
    sourceArray2[5] = (byte) 3;
    sourceArray2[25] = (byte) 89;
    sourceArray2[26] = (byte) 217;
    sourceArray2[1] = (byte) 137;
    sourceArray2[13] = (byte) 241;
    sourceArray2[29] = (byte) 250;
    sourceArray2[39] = (byte) 178;
    sourceArray2[10] = (byte) 216;
    sourceArray2[31 /*0x1F*/] = (byte) 220;
    sourceArray2[33] = (byte) 181;
    sourceArray2[34] = (byte) 143;
    sourceArray2[35] = (byte) 75;
    sourceArray2[2] = (byte) 62;
    sourceArray2[24] = (byte) 249;
    sourceArray2[37] = (byte) 216;
    sourceArray2[38] = (byte) 37;
    sourceArray2[36] = (byte) 115;
    sourceArray2[41] = (byte) 109;
    sourceArray2[42] = (byte) 17;
    sourceArray2[43] = (byte) 214;
    sourceArray2[44] = (byte) 215;
    sourceArray2[45] = (byte) 103;
    sourceArray2[46] = (byte) 251;
    sourceArray2[16 /*0x10*/] = (byte) 151;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12509(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 17,
      (byte) 112 /*0x70*/,
      (byte) 159,
      (byte) 104,
      (byte) 149,
      (byte) 120,
      (byte) 94,
      (byte) 74,
      (byte) 199,
      (byte) 33,
      (byte) 7,
      (byte) 130,
      byte.MaxValue,
      (byte) 118,
      (byte) 53,
      (byte) 228,
      (byte) 49,
      (byte) 106,
      (byte) 226,
      (byte) 40,
      (byte) 61,
      (byte) 88,
      (byte) 5,
      (byte) 3,
      (byte) 61,
      (byte) 229,
      (byte) 27,
      (byte) 103,
      (byte) 150,
      (byte) 157,
      (byte) 249,
      (byte) 197,
      (byte) 18,
      (byte) 115,
      (byte) 200,
      (byte) 18,
      (byte) 163,
      (byte) 109,
      (byte) 0,
      (byte) 20,
      (byte) 229,
      (byte) 205,
      (byte) 57,
      (byte) 215,
      (byte) 232,
      (byte) 100,
      (byte) 231,
      (byte) 233
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 212,
      (byte) 56,
      (byte) 251,
      byte.MaxValue,
      (byte) 96 /*0x60*/,
      (byte) 120,
      (byte) 231,
      (byte) 221,
      (byte) 33,
      (byte) 23,
      (byte) 62,
      (byte) 55,
      (byte) 217,
      (byte) 95,
      (byte) 10,
      (byte) 25,
      (byte) 42,
      (byte) 197,
      (byte) 94,
      (byte) 142,
      (byte) 0,
      (byte) 250,
      (byte) 22,
      (byte) 101,
      (byte) 27,
      (byte) 206,
      (byte) 184,
      (byte) 30,
      (byte) 242,
      (byte) 135,
      (byte) 145,
      (byte) 122,
      (byte) 207,
      (byte) 211,
      (byte) 19,
      (byte) 229,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 67,
      (byte) 35,
      (byte) 25,
      (byte) 155,
      (byte) 237,
      (byte) 240 /*0xF0*/,
      (byte) 63 /*0x3F*/,
      (byte) 90,
      (byte) 229
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[34];
    byte[] response2 = new byte[34];
    Array.Copy((Array) sc_12507.sspq, 0, (Array) numArray2, 0, 34);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12507.sspr, 0, (Array) numArray2, 0, 34);
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

  internal static int ssp_appserver_12510(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 195,
      (byte) 15,
      (byte) 166,
      (byte) 6,
      (byte) 65,
      (byte) 80 /*0x50*/,
      (byte) 183,
      (byte) 239,
      (byte) 25,
      (byte) 15,
      (byte) 176 /*0xB0*/,
      (byte) 166,
      (byte) 244,
      (byte) 91,
      (byte) 60,
      (byte) 103,
      (byte) 169,
      (byte) 24,
      (byte) 82,
      (byte) 151,
      (byte) 38,
      (byte) 63 /*0x3F*/,
      (byte) 153,
      (byte) 62,
      (byte) 130,
      (byte) 83,
      (byte) 178,
      (byte) 43,
      (byte) 25,
      (byte) 8,
      (byte) 104,
      (byte) 234,
      (byte) 225,
      (byte) 198,
      (byte) 200,
      (byte) 157,
      (byte) 124,
      (byte) 58,
      (byte) 129,
      (byte) 24,
      (byte) 21,
      (byte) 83,
      (byte) 87,
      (byte) 15,
      (byte) 125,
      (byte) 115,
      (byte) 227,
      (byte) 162
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 65,
      (byte) 79,
      (byte) 201,
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 158,
      (byte) 179,
      (byte) 18,
      (byte) 120,
      (byte) 24,
      (byte) 40,
      (byte) 17,
      (byte) 73,
      (byte) 61,
      (byte) 65,
      (byte) 186,
      (byte) 31 /*0x1F*/,
      (byte) 31 /*0x1F*/,
      (byte) 234,
      (byte) 59,
      (byte) 226,
      (byte) 232,
      (byte) 16 /*0x10*/,
      (byte) 117,
      (byte) 1,
      (byte) 139,
      (byte) 198,
      (byte) 152,
      (byte) 65,
      (byte) 135,
      (byte) 85,
      (byte) 141,
      (byte) 139,
      (byte) 52,
      (byte) 57,
      (byte) 185,
      (byte) 148,
      (byte) 125,
      (byte) 1,
      (byte) 149,
      (byte) 15,
      (byte) 237,
      (byte) 14,
      (byte) 87,
      (byte) 197,
      (byte) 114,
      (byte) 121,
      (byte) 16 /*0x10*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[31 /*0x1F*/];
    byte[] response2 = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_12507.sspq, 34, (Array) numArray2, 0, 31 /*0x1F*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12507.sspr, 34, (Array) numArray2, 0, 31 /*0x1F*/);
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

  internal static int ssp_appserver_12511(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 146;
    sourceArray1[17] = (byte) 16 /*0x10*/;
    sourceArray1[2] = (byte) 23;
    sourceArray1[6] = (byte) 19;
    sourceArray1[4] = (byte) 68;
    sourceArray1[18] = (byte) 87;
    sourceArray1[28] = (byte) 118;
    sourceArray1[47] = (byte) 33;
    sourceArray1[5] = (byte) 39;
    sourceArray1[9] = (byte) 122;
    sourceArray1[10] = (byte) 137;
    sourceArray1[27] = (byte) 167;
    sourceArray1[22] = (byte) 176 /*0xB0*/;
    sourceArray1[13] = (byte) 104;
    sourceArray1[14] = (byte) 183;
    sourceArray1[41] = (byte) 184;
    sourceArray1[16 /*0x10*/] = (byte) 180;
    sourceArray1[43] = (byte) 191;
    sourceArray1[35] = (byte) 197;
    sourceArray1[1] = (byte) 0;
    sourceArray1[31 /*0x1F*/] = (byte) 85;
    sourceArray1[21] = (byte) 18;
    sourceArray1[12] = (byte) 71;
    sourceArray1[11] = (byte) 125;
    sourceArray1[15] = (byte) 174;
    sourceArray1[25] = (byte) 156;
    sourceArray1[30] = (byte) 143;
    sourceArray1[37] = (byte) 235;
    sourceArray1[7] = (byte) 181;
    sourceArray1[29] = (byte) 126;
    sourceArray1[36] = (byte) 173;
    sourceArray1[45] = (byte) 85;
    sourceArray1[3] = (byte) 67;
    sourceArray1[33] = (byte) 9;
    sourceArray1[34] = (byte) 232;
    sourceArray1[23] = (byte) 72;
    sourceArray1[32 /*0x20*/] = (byte) 48 /*0x30*/;
    sourceArray1[19] = (byte) 137;
    sourceArray1[38] = (byte) 78;
    sourceArray1[39] = (byte) 118;
    sourceArray1[40] = (byte) 215;
    sourceArray1[46] = (byte) 160 /*0xA0*/;
    sourceArray1[0] = (byte) 190;
    sourceArray1[20] = (byte) 146;
    sourceArray1[44] = (byte) 247;
    sourceArray1[26] = (byte) 236;
    sourceArray1[42] = (byte) 3;
    sourceArray1[24] = (byte) 175;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 106,
      (byte) 8,
      (byte) 167,
      (byte) 229,
      (byte) 139,
      (byte) 14,
      (byte) 34,
      (byte) 121,
      (byte) 42,
      (byte) 104,
      (byte) 98,
      (byte) 60,
      (byte) 225,
      (byte) 79,
      (byte) 246,
      (byte) 128 /*0x80*/,
      (byte) 82,
      (byte) 241,
      (byte) 165,
      (byte) 153,
      (byte) 110,
      (byte) 18,
      (byte) 66,
      (byte) 250,
      (byte) 186,
      (byte) 165,
      (byte) 157,
      (byte) 218,
      (byte) 245,
      (byte) 235,
      (byte) 36,
      (byte) 19,
      (byte) 37,
      (byte) 20,
      (byte) 35,
      (byte) 234,
      (byte) 150,
      (byte) 173,
      (byte) 120,
      (byte) 128 /*0x80*/,
      (byte) 196,
      (byte) 211,
      (byte) 76,
      (byte) 53,
      (byte) 129,
      (byte) 37,
      (byte) 60,
      (byte) 190
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12512(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 161,
      (byte) 23,
      (byte) 95,
      (byte) 241,
      (byte) 61,
      byte.MaxValue,
      (byte) 103,
      (byte) 153,
      (byte) 156,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 57,
      (byte) 171,
      (byte) 112 /*0x70*/,
      (byte) 206,
      (byte) 41,
      (byte) 80 /*0x50*/,
      (byte) 91,
      (byte) 190,
      (byte) 54,
      (byte) 167,
      (byte) 66,
      (byte) 125,
      (byte) 35,
      (byte) 112 /*0x70*/,
      (byte) 27,
      (byte) 165,
      (byte) 72,
      (byte) 96 /*0x60*/,
      (byte) 31 /*0x1F*/,
      (byte) 28,
      (byte) 141,
      (byte) 80 /*0x50*/,
      (byte) 153,
      (byte) 41,
      (byte) 134,
      (byte) 182,
      (byte) 209,
      (byte) 82,
      (byte) 118,
      (byte) 174,
      (byte) 252,
      (byte) 160 /*0xA0*/,
      (byte) 21,
      (byte) 94,
      (byte) 30,
      (byte) 251
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[14] = (byte) 248;
    sourceArray2[1] = (byte) 137;
    sourceArray2[2] = (byte) 187;
    sourceArray2[3] = (byte) 100;
    sourceArray2[28] = (byte) 162;
    sourceArray2[19] = (byte) 1;
    sourceArray2[20] = (byte) 168;
    sourceArray2[26] = (byte) 215;
    sourceArray2[29] = (byte) 242;
    sourceArray2[17] = (byte) 208 /*0xD0*/;
    sourceArray2[16 /*0x10*/] = (byte) 62;
    sourceArray2[11] = (byte) 56;
    sourceArray2[40] = (byte) 228;
    sourceArray2[13] = (byte) 193;
    sourceArray2[5] = (byte) 235;
    sourceArray2[15] = (byte) 5;
    sourceArray2[7] = (byte) 28;
    sourceArray2[44] = (byte) 4;
    sourceArray2[18] = (byte) 4;
    sourceArray2[6] = (byte) 111;
    sourceArray2[45] = (byte) 54;
    sourceArray2[4] = (byte) 223;
    sourceArray2[43] = (byte) 184;
    sourceArray2[23] = (byte) 95;
    sourceArray2[24] = (byte) 92;
    sourceArray2[25] = (byte) 54;
    sourceArray2[46] = (byte) 27;
    sourceArray2[27] = (byte) 49;
    sourceArray2[35] = (byte) 210;
    sourceArray2[33] = (byte) 43;
    sourceArray2[8] = (byte) 116;
    sourceArray2[31 /*0x1F*/] = (byte) 235;
    sourceArray2[0] = (byte) 98;
    sourceArray2[21] = (byte) 73;
    sourceArray2[34] = (byte) 57;
    sourceArray2[9] = (byte) 227;
    sourceArray2[36] = (byte) 97;
    sourceArray2[37] = (byte) 49;
    sourceArray2[10] = (byte) 162;
    sourceArray2[39] = (byte) 113;
    sourceArray2[22] = (byte) 221;
    sourceArray2[41] = (byte) 165;
    sourceArray2[47] = (byte) 76;
    sourceArray2[30] = (byte) 249;
    sourceArray2[12] = (byte) 127 /*0x7F*/;
    sourceArray2[32 /*0x20*/] = (byte) 233;
    sourceArray2[42] = (byte) 11;
    sourceArray2[38] = (byte) 173;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12513(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 53,
      (byte) 233,
      (byte) 132,
      (byte) 83,
      (byte) 221,
      (byte) 33,
      (byte) 112 /*0x70*/,
      (byte) 188,
      (byte) 74,
      (byte) 17,
      (byte) 188,
      (byte) 49,
      (byte) 118,
      (byte) 46,
      (byte) 0,
      (byte) 127 /*0x7F*/,
      (byte) 114,
      (byte) 39,
      (byte) 252,
      (byte) 159,
      (byte) 104,
      (byte) 107,
      (byte) 53,
      (byte) 123,
      (byte) 92,
      (byte) 247,
      (byte) 45,
      (byte) 214,
      (byte) 43,
      (byte) 249,
      (byte) 231,
      (byte) 125,
      (byte) 150,
      (byte) 89,
      (byte) 28,
      (byte) 55,
      (byte) 85,
      (byte) 89,
      (byte) 52,
      (byte) 95,
      (byte) 50,
      (byte) 205,
      (byte) 50,
      (byte) 203,
      (byte) 14,
      (byte) 187,
      (byte) 4,
      (byte) 33
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 178,
      (byte) 117,
      (byte) 211,
      (byte) 51,
      (byte) 92,
      (byte) 241,
      (byte) 217,
      (byte) 146,
      (byte) 97,
      (byte) 36,
      (byte) 202,
      (byte) 215,
      (byte) 118,
      (byte) 48 /*0x30*/,
      (byte) 253,
      (byte) 204,
      (byte) 26,
      (byte) 14,
      (byte) 105,
      (byte) 172,
      (byte) 206,
      (byte) 137,
      (byte) 25,
      (byte) 235,
      (byte) 65,
      (byte) 189,
      (byte) 10,
      (byte) 131,
      (byte) 3,
      (byte) 87,
      (byte) 154,
      (byte) 58,
      (byte) 225,
      (byte) 243,
      (byte) 209,
      (byte) 42,
      (byte) 89,
      (byte) 4,
      (byte) 133,
      (byte) 190,
      (byte) 67,
      (byte) 80 /*0x50*/,
      (byte) 151,
      (byte) 206,
      (byte) 121,
      (byte) 74
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12514(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 3,
      (byte) 201,
      (byte) 180,
      (byte) 9,
      (byte) 246,
      (byte) 229,
      (byte) 243,
      (byte) 154,
      (byte) 145,
      (byte) 81,
      (byte) 41,
      (byte) 62,
      (byte) 154,
      (byte) 29,
      (byte) 18,
      (byte) 201,
      (byte) 42,
      (byte) 25,
      (byte) 243,
      (byte) 157,
      (byte) 109,
      (byte) 155,
      (byte) 56,
      (byte) 105,
      (byte) 108,
      (byte) 161,
      (byte) 242,
      (byte) 11,
      (byte) 166,
      (byte) 141,
      (byte) 220,
      (byte) 75,
      (byte) 85,
      (byte) 100,
      (byte) 85,
      (byte) 82,
      (byte) 131,
      (byte) 76,
      (byte) 98,
      (byte) 78,
      (byte) 107,
      (byte) 152,
      (byte) 59,
      (byte) 224 /*0xE0*/,
      (byte) 161,
      (byte) 207,
      (byte) 32 /*0x20*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 65;
    sourceArray2[37] = (byte) 40;
    sourceArray2[15] = (byte) 244;
    sourceArray2[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
    sourceArray2[6] = (byte) 159;
    sourceArray2[5] = (byte) 253;
    sourceArray2[3] = (byte) 240 /*0xF0*/;
    sourceArray2[45] = (byte) 229;
    sourceArray2[8] = (byte) 89;
    sourceArray2[36] = (byte) 133;
    sourceArray2[28] = (byte) 25;
    sourceArray2[35] = (byte) 177;
    sourceArray2[11] = (byte) 113;
    sourceArray2[13] = (byte) 154;
    sourceArray2[2] = (byte) 61;
    sourceArray2[10] = (byte) 53;
    sourceArray2[34] = (byte) 165;
    sourceArray2[17] = (byte) 176 /*0xB0*/;
    sourceArray2[18] = (byte) 95;
    sourceArray2[4] = (byte) 22;
    sourceArray2[20] = (byte) 220;
    sourceArray2[27] = (byte) 153;
    sourceArray2[41] = (byte) 68;
    sourceArray2[19] = (byte) 149;
    sourceArray2[24] = (byte) 15;
    sourceArray2[23] = (byte) 38;
    sourceArray2[26] = byte.MaxValue;
    sourceArray2[7] = (byte) 209;
    sourceArray2[9] = (byte) 252;
    sourceArray2[29] = (byte) 128 /*0x80*/;
    sourceArray2[30] = (byte) 31 /*0x1F*/;
    sourceArray2[46] = (byte) 115;
    sourceArray2[21] = (byte) 131;
    sourceArray2[33] = (byte) 34;
    sourceArray2[12] = (byte) 199;
    sourceArray2[1] = (byte) 121;
    sourceArray2[0] = (byte) 131;
    sourceArray2[25] = (byte) 172;
    sourceArray2[38] = (byte) 102;
    sourceArray2[39] = (byte) 214;
    sourceArray2[40] = (byte) 246;
    sourceArray2[32 /*0x20*/] = (byte) 44;
    sourceArray2[22] = (byte) 178;
    sourceArray2[43] = (byte) 78;
    sourceArray2[44] = (byte) 111;
    sourceArray2[42] = (byte) 248;
    sourceArray2[14] = (byte) 198;
    sourceArray2[47] = (byte) 5;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12515(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 230,
      (byte) 234,
      (byte) 31 /*0x1F*/,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 148,
      (byte) 76,
      (byte) 2,
      (byte) 89,
      (byte) 239,
      (byte) 8,
      (byte) 57,
      (byte) 130,
      byte.MaxValue,
      (byte) 212,
      (byte) 232,
      (byte) 236,
      (byte) 180,
      (byte) 232,
      (byte) 154,
      (byte) 57,
      (byte) 31 /*0x1F*/,
      (byte) 150,
      (byte) 218,
      (byte) 253,
      (byte) 13,
      (byte) 46,
      (byte) 168,
      (byte) 74,
      (byte) 175,
      (byte) 48 /*0x30*/,
      (byte) 39,
      (byte) 235,
      (byte) 4,
      (byte) 182,
      (byte) 151,
      (byte) 235,
      (byte) 171,
      (byte) 30,
      (byte) 134,
      (byte) 141,
      (byte) 162,
      (byte) 112 /*0x70*/,
      (byte) 176 /*0xB0*/,
      (byte) 228,
      (byte) 136,
      (byte) 182,
      (byte) 175
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 106,
      (byte) 58,
      (byte) 117,
      (byte) 41,
      (byte) 135,
      (byte) 58,
      (byte) 111,
      (byte) 171,
      (byte) 105,
      (byte) 149,
      (byte) 16 /*0x10*/,
      (byte) 232,
      (byte) 187,
      (byte) 92,
      (byte) 131,
      (byte) 207,
      (byte) 124,
      (byte) 147,
      (byte) 51,
      (byte) 116,
      (byte) 71,
      (byte) 135,
      (byte) 91,
      (byte) 210,
      byte.MaxValue,
      (byte) 27,
      (byte) 29,
      (byte) 226,
      (byte) 176 /*0xB0*/,
      (byte) 170,
      (byte) 124,
      (byte) 225,
      (byte) 5,
      (byte) 240 /*0xF0*/,
      (byte) 177,
      (byte) 64 /*0x40*/,
      (byte) 102,
      (byte) 216,
      (byte) 170,
      (byte) 86,
      (byte) 188,
      (byte) 222,
      (byte) 119,
      (byte) 221,
      (byte) 69,
      (byte) 75,
      (byte) 101,
      (byte) 198
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[29];
    byte[] response2 = new byte[29];
    Array.Copy((Array) sc_12507.sspq, 65, (Array) numArray2, 0, 29);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12507.sspr, 65, (Array) numArray2, 0, 29);
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

  internal static string ssp_appserver_12516()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 111,
        (byte) 215,
        (byte) 25,
        (byte) 65,
        (byte) 214,
        (byte) 26,
        (byte) 122,
        (byte) 240 /*0xF0*/,
        (byte) 67,
        (byte) 166
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 160 /*0xA0*/,
        (byte) 59,
        (byte) 213,
        (byte) 148,
        (byte) 228,
        (byte) 163,
        (byte) 108,
        (byte) 41,
        (byte) 115,
        (byte) 130
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[4] = (byte) 2;
    numArray5[6] = (byte) 56;
    numArray5[2] = (byte) 198;
    numArray5[3] = (byte) 241;
    numArray5[1] = (byte) 234;
    numArray5[5] = (byte) 58;
    numArray5[0] = (byte) 234;
    numArray5[7] = (byte) 102;
    numArray5[8] = (byte) 252;
    numArray5[9] = (byte) 118;
    byte[] numArray6 = new byte[10]
    {
      (byte) 165,
      (byte) 175,
      (byte) 129,
      (byte) 78,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 87,
      (byte) 187,
      (byte) 36,
      (byte) 12
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12517(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 10,
      (byte) 50,
      (byte) 3,
      (byte) 88,
      (byte) 249,
      (byte) 86,
      (byte) 231,
      (byte) 137,
      (byte) 65,
      (byte) 34,
      (byte) 173,
      (byte) 232,
      (byte) 244,
      (byte) 243,
      (byte) 58,
      (byte) 30,
      (byte) 24,
      (byte) 114,
      (byte) 97,
      (byte) 196,
      (byte) 231,
      (byte) 232,
      (byte) 86,
      (byte) 136,
      (byte) 217,
      (byte) 214,
      (byte) 87,
      (byte) 124,
      (byte) 80 /*0x50*/,
      (byte) 57,
      (byte) 181,
      (byte) 9,
      (byte) 54,
      (byte) 116,
      (byte) 247,
      (byte) 8,
      (byte) 53,
      (byte) 14,
      (byte) 2,
      (byte) 31 /*0x1F*/,
      (byte) 108,
      (byte) 207,
      (byte) 172,
      (byte) 161,
      (byte) 68,
      (byte) 47,
      (byte) 120,
      (byte) 90
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 162,
      (byte) 209,
      (byte) 182,
      (byte) 150,
      (byte) 76,
      (byte) 148,
      (byte) 213,
      (byte) 221,
      (byte) 224 /*0xE0*/,
      (byte) 189,
      (byte) 230,
      (byte) 4,
      (byte) 200,
      (byte) 191,
      (byte) 92,
      (byte) 213,
      (byte) 16 /*0x10*/,
      (byte) 79,
      (byte) 16 /*0x10*/,
      (byte) 244,
      (byte) 125,
      (byte) 56,
      (byte) 242,
      (byte) 128 /*0x80*/,
      (byte) 150,
      (byte) 254,
      (byte) 16 /*0x10*/,
      (byte) 78,
      (byte) 202,
      (byte) 208 /*0xD0*/,
      (byte) 239,
      (byte) 28,
      (byte) 105,
      (byte) 93,
      (byte) 119,
      (byte) 244,
      (byte) 235,
      (byte) 0,
      (byte) 148,
      (byte) 222,
      (byte) 40,
      (byte) 213,
      (byte) 86,
      (byte) 219,
      (byte) 152,
      (byte) 224 /*0xE0*/,
      (byte) 128 /*0x80*/,
      (byte) 72
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[40];
    byte[] response2 = new byte[40];
    Array.Copy((Array) sc_12507.sspq, 94, (Array) numArray2, 0, 40);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12507.sspr, 94, (Array) numArray2, 0, 40);
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

  internal static int ssp_appserver_12519(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[17] = (byte) 38;
    sourceArray1[42] = (byte) 159;
    sourceArray1[9] = (byte) 110;
    sourceArray1[3] = (byte) 163;
    sourceArray1[4] = (byte) 52;
    sourceArray1[46] = (byte) 162;
    sourceArray1[32 /*0x20*/] = (byte) 122;
    sourceArray1[7] = (byte) 247;
    sourceArray1[8] = (byte) 93;
    sourceArray1[5] = (byte) 160 /*0xA0*/;
    sourceArray1[14] = (byte) 115;
    sourceArray1[36] = (byte) 129;
    sourceArray1[12] = (byte) 81;
    sourceArray1[13] = (byte) 61;
    sourceArray1[11] = (byte) 197;
    sourceArray1[15] = (byte) 125;
    sourceArray1[33] = (byte) 144 /*0x90*/;
    sourceArray1[21] = (byte) 1;
    sourceArray1[37] = (byte) 151;
    sourceArray1[19] = (byte) 213;
    sourceArray1[20] = (byte) 197;
    sourceArray1[44] = (byte) 253;
    sourceArray1[22] = (byte) 144 /*0x90*/;
    sourceArray1[23] = (byte) 134;
    sourceArray1[1] = (byte) 1;
    sourceArray1[10] = (byte) 181;
    sourceArray1[26] = (byte) 25;
    sourceArray1[27] = (byte) 71;
    sourceArray1[25] = (byte) 121;
    sourceArray1[29] = (byte) 39;
    sourceArray1[35] = (byte) 86;
    sourceArray1[31 /*0x1F*/] = (byte) 45;
    sourceArray1[24] = (byte) 158;
    sourceArray1[16 /*0x10*/] = (byte) 230;
    sourceArray1[45] = (byte) 164;
    sourceArray1[30] = (byte) 6;
    sourceArray1[28] = (byte) 161;
    sourceArray1[0] = (byte) 32 /*0x20*/;
    sourceArray1[38] = (byte) 129;
    sourceArray1[39] = (byte) 162;
    sourceArray1[40] = (byte) 44;
    sourceArray1[41] = (byte) 102;
    sourceArray1[18] = (byte) 54;
    sourceArray1[43] = (byte) 108;
    sourceArray1[2] = (byte) 150;
    sourceArray1[34] = (byte) 32 /*0x20*/;
    sourceArray1[6] = (byte) 125;
    sourceArray1[47] = (byte) 76;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 226,
      (byte) 59,
      (byte) 206,
      (byte) 156,
      (byte) 157,
      (byte) 53,
      (byte) 122,
      (byte) 159,
      (byte) 7,
      (byte) 224 /*0xE0*/,
      (byte) 37,
      (byte) 131,
      (byte) 204,
      (byte) 195,
      (byte) 193,
      (byte) 140,
      (byte) 209,
      (byte) 36,
      (byte) 139,
      (byte) 9,
      (byte) 111,
      (byte) 70,
      (byte) 45,
      (byte) 173,
      (byte) 105,
      (byte) 84,
      (byte) 206,
      (byte) 73,
      (byte) 176 /*0xB0*/,
      (byte) 52,
      (byte) 138,
      (byte) 240 /*0xF0*/,
      (byte) 159,
      (byte) 38,
      (byte) 95,
      (byte) 10,
      (byte) 151,
      (byte) 16 /*0x10*/,
      (byte) 179,
      (byte) 31 /*0x1F*/,
      (byte) 35,
      (byte) 146,
      (byte) 120,
      (byte) 95,
      (byte) 209,
      (byte) 203,
      (byte) 94
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12520(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 224 /*0xE0*/,
      (byte) 47,
      (byte) 217,
      (byte) 102,
      (byte) 159,
      (byte) 97,
      (byte) 240 /*0xF0*/,
      (byte) 17,
      (byte) 188,
      (byte) 190,
      (byte) 77,
      (byte) 136,
      (byte) 11,
      (byte) 227,
      (byte) 185,
      (byte) 229,
      (byte) 23,
      (byte) 253,
      (byte) 104,
      (byte) 223,
      (byte) 138,
      (byte) 76,
      (byte) 27,
      (byte) 57,
      (byte) 139,
      (byte) 188,
      (byte) 222,
      (byte) 119,
      (byte) 178,
      (byte) 78,
      (byte) 248,
      (byte) 32 /*0x20*/,
      (byte) 18,
      (byte) 159,
      (byte) 246,
      (byte) 157,
      (byte) 119,
      (byte) 217,
      (byte) 154,
      (byte) 158,
      (byte) 184,
      (byte) 175,
      (byte) 185,
      (byte) 66,
      (byte) 150,
      (byte) 232,
      (byte) 134,
      (byte) 226
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 61,
      (byte) 211,
      (byte) 144 /*0x90*/,
      (byte) 226,
      (byte) 86,
      (byte) 75,
      (byte) 139,
      (byte) 71,
      (byte) 197,
      (byte) 154,
      (byte) 102,
      (byte) 226,
      (byte) 117,
      (byte) 185,
      (byte) 122,
      (byte) 248,
      (byte) 180,
      (byte) 44,
      (byte) 11,
      (byte) 193,
      (byte) 100,
      (byte) 238,
      (byte) 119,
      (byte) 142,
      (byte) 161,
      (byte) 112 /*0x70*/,
      (byte) 150,
      (byte) 72,
      (byte) 62,
      (byte) 178,
      (byte) 223,
      (byte) 190,
      (byte) 62,
      (byte) 32 /*0x20*/,
      (byte) 9,
      (byte) 46,
      (byte) 217,
      (byte) 78,
      (byte) 123,
      (byte) 187,
      (byte) 130,
      (byte) 96 /*0x60*/,
      (byte) 61,
      (byte) 184,
      (byte) 175,
      (byte) 42,
      (byte) 17,
      (byte) 68
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12521(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[32 /*0x20*/] = (byte) 140;
    sourceArray1[34] = (byte) 124;
    sourceArray1[41] = (byte) 27;
    sourceArray1[46] = (byte) 104;
    sourceArray1[6] = (byte) 226;
    sourceArray1[5] = (byte) 218;
    sourceArray1[11] = (byte) 198;
    sourceArray1[33] = (byte) 133;
    sourceArray1[8] = (byte) 120;
    sourceArray1[27] = (byte) 53;
    sourceArray1[10] = (byte) 109;
    sourceArray1[45] = (byte) 124;
    sourceArray1[4] = (byte) 159;
    sourceArray1[13] = (byte) 117;
    sourceArray1[35] = (byte) 208 /*0xD0*/;
    sourceArray1[15] = (byte) 237;
    sourceArray1[16 /*0x10*/] = (byte) 28;
    sourceArray1[17] = (byte) 109;
    sourceArray1[18] = (byte) 61;
    sourceArray1[19] = (byte) 98;
    sourceArray1[3] = (byte) 20;
    sourceArray1[9] = (byte) 94;
    sourceArray1[37] = (byte) 137;
    sourceArray1[1] = (byte) 207;
    sourceArray1[24] = (byte) 216;
    sourceArray1[23] = (byte) 147;
    sourceArray1[43] = (byte) 80 /*0x50*/;
    sourceArray1[7] = (byte) 131;
    sourceArray1[28] = (byte) 251;
    sourceArray1[29] = (byte) 21;
    sourceArray1[26] = (byte) 162;
    sourceArray1[38] = (byte) 215;
    sourceArray1[47] = (byte) 115;
    sourceArray1[31 /*0x1F*/] = (byte) 86;
    sourceArray1[22] = (byte) 219;
    sourceArray1[21] = (byte) 39;
    sourceArray1[20] = (byte) 202;
    sourceArray1[0] = (byte) 150;
    sourceArray1[2] = (byte) 147;
    sourceArray1[39] = (byte) 254;
    sourceArray1[40] = (byte) 183;
    sourceArray1[30] = (byte) 161;
    sourceArray1[12] = (byte) 45;
    sourceArray1[25] = (byte) 162;
    sourceArray1[44] = (byte) 76;
    sourceArray1[36] = (byte) 98;
    sourceArray1[14] = (byte) 107;
    sourceArray1[42] = (byte) 63 /*0x3F*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 169;
    sourceArray2[1] = (byte) 48 /*0x30*/;
    sourceArray2[2] = (byte) 144 /*0x90*/;
    sourceArray2[7] = (byte) 65;
    sourceArray2[12] = (byte) 213;
    sourceArray2[5] = (byte) 22;
    sourceArray2[4] = (byte) 174;
    sourceArray2[13] = (byte) 211;
    sourceArray2[8] = (byte) 185;
    sourceArray2[9] = (byte) 56;
    sourceArray2[10] = (byte) 233;
    sourceArray2[11] = (byte) 1;
    sourceArray2[41] = (byte) 207;
    sourceArray2[47] = (byte) 247;
    sourceArray2[14] = (byte) 206;
    sourceArray2[15] = (byte) 188;
    sourceArray2[32 /*0x20*/] = (byte) 169;
    sourceArray2[17] = (byte) 163;
    sourceArray2[44] = (byte) 42;
    sourceArray2[6] = (byte) 58;
    sourceArray2[0] = (byte) 83;
    sourceArray2[30] = (byte) 2;
    sourceArray2[40] = (byte) 223;
    sourceArray2[3] = (byte) 27;
    sourceArray2[46] = (byte) 72;
    sourceArray2[16 /*0x10*/] = (byte) 88;
    sourceArray2[25] = (byte) 108;
    sourceArray2[21] = (byte) 31 /*0x1F*/;
    sourceArray2[28] = (byte) 173;
    sourceArray2[29] = (byte) 229;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[42] = (byte) 165;
    sourceArray2[18] = (byte) 239;
    sourceArray2[33] = (byte) 193;
    sourceArray2[34] = (byte) 15;
    sourceArray2[23] = (byte) 5;
    sourceArray2[38] = (byte) 217;
    sourceArray2[37] = (byte) 236;
    sourceArray2[24] = (byte) 51;
    sourceArray2[39] = (byte) 226;
    sourceArray2[43] = (byte) 55;
    sourceArray2[19] = (byte) 219;
    sourceArray2[36] = (byte) 13;
    sourceArray2[27] = (byte) 8;
    sourceArray2[22] = (byte) 126;
    sourceArray2[45] = (byte) 139;
    sourceArray2[26] = (byte) 21;
    sourceArray2[35] = (byte) 106;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12522(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 191,
      (byte) 121,
      (byte) 17,
      (byte) 73,
      (byte) 210,
      (byte) 186,
      (byte) 138,
      (byte) 203,
      (byte) 143,
      (byte) 193,
      (byte) 65,
      (byte) 111,
      (byte) 81,
      (byte) 3,
      (byte) 190,
      (byte) 6,
      (byte) 124,
      (byte) 111,
      (byte) 168,
      (byte) 197,
      (byte) 74,
      (byte) 82,
      (byte) 246,
      (byte) 230,
      (byte) 119,
      (byte) 207,
      (byte) 90,
      (byte) 146,
      (byte) 98,
      (byte) 101,
      (byte) 72,
      (byte) 128 /*0x80*/,
      (byte) 213,
      (byte) 165,
      (byte) 185,
      (byte) 38,
      (byte) 10,
      (byte) 193,
      (byte) 37,
      (byte) 86,
      (byte) 142,
      (byte) 173,
      (byte) 38,
      (byte) 218,
      (byte) 89,
      (byte) 172,
      (byte) 158,
      (byte) 28
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 202;
    sourceArray2[13] = (byte) 228;
    sourceArray2[2] = (byte) 106;
    sourceArray2[22] = (byte) 111;
    sourceArray2[16 /*0x10*/] = (byte) 204;
    sourceArray2[5] = (byte) 160 /*0xA0*/;
    sourceArray2[6] = (byte) 177;
    sourceArray2[44] = (byte) 181;
    sourceArray2[47] = (byte) 8;
    sourceArray2[40] = (byte) 3;
    sourceArray2[10] = (byte) 187;
    sourceArray2[28] = (byte) 136;
    sourceArray2[12] = (byte) 95;
    sourceArray2[31 /*0x1F*/] = (byte) 88;
    sourceArray2[32 /*0x20*/] = (byte) 165;
    sourceArray2[15] = (byte) 141;
    sourceArray2[26] = (byte) 174;
    sourceArray2[17] = (byte) 220;
    sourceArray2[46] = (byte) 241;
    sourceArray2[19] = (byte) 112 /*0x70*/;
    sourceArray2[20] = (byte) 78;
    sourceArray2[39] = (byte) 89;
    sourceArray2[33] = (byte) 93;
    sourceArray2[3] = (byte) 206;
    sourceArray2[9] = (byte) 241;
    sourceArray2[25] = (byte) 149;
    sourceArray2[18] = (byte) 194;
    sourceArray2[8] = (byte) 149;
    sourceArray2[35] = (byte) 218;
    sourceArray2[29] = (byte) 36;
    sourceArray2[30] = (byte) 167;
    sourceArray2[11] = (byte) 197;
    sourceArray2[14] = (byte) 27;
    sourceArray2[24] = (byte) 237;
    sourceArray2[34] = (byte) 40;
    sourceArray2[23] = (byte) 14;
    sourceArray2[0] = (byte) 187;
    sourceArray2[37] = (byte) 79;
    sourceArray2[38] = (byte) 253;
    sourceArray2[1] = (byte) 205;
    sourceArray2[21] = (byte) 199;
    sourceArray2[7] = (byte) 132;
    sourceArray2[42] = (byte) 24;
    sourceArray2[43] = (byte) 131;
    sourceArray2[27] = (byte) 74;
    sourceArray2[36] = (byte) 237;
    sourceArray2[4] = (byte) 17;
    sourceArray2[41] = (byte) 32 /*0x20*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
