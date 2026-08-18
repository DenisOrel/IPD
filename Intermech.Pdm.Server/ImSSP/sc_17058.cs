// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_17058
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_17058
{
  internal static int ssp_pdm_server_17059(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 217,
      (byte) 41,
      (byte) 213,
      (byte) 138,
      (byte) 5,
      (byte) 233,
      (byte) 96 /*0x60*/,
      (byte) 65,
      (byte) 60,
      (byte) 50,
      (byte) 90,
      (byte) 68,
      (byte) 167,
      (byte) 154,
      (byte) 37,
      (byte) 77,
      (byte) 57,
      (byte) 122,
      (byte) 37,
      (byte) 251,
      (byte) 35,
      (byte) 45,
      (byte) 119,
      (byte) 135,
      (byte) 167,
      (byte) 165,
      (byte) 198,
      (byte) 128 /*0x80*/,
      (byte) 227,
      (byte) 137,
      (byte) 187,
      (byte) 184,
      (byte) 67,
      (byte) 227,
      (byte) 196,
      (byte) 106,
      (byte) 3,
      (byte) 159,
      (byte) 104,
      (byte) 26,
      (byte) 41,
      (byte) 116,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 183,
      (byte) 183,
      (byte) 244,
      (byte) 243
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 31 /*0x1F*/,
      (byte) 230,
      (byte) 98,
      (byte) 31 /*0x1F*/,
      (byte) 85,
      (byte) 164,
      (byte) 221,
      (byte) 153,
      (byte) 116,
      (byte) 243,
      (byte) 19,
      (byte) 201,
      (byte) 178,
      (byte) 37,
      (byte) 117,
      (byte) 185,
      (byte) 204,
      (byte) 16 /*0x10*/,
      (byte) 34,
      (byte) 112 /*0x70*/,
      (byte) 36,
      (byte) 165,
      (byte) 67,
      (byte) 18,
      (byte) 204,
      (byte) 107,
      (byte) 133,
      (byte) 77,
      (byte) 80 /*0x50*/,
      (byte) 159,
      (byte) 254,
      (byte) 69,
      (byte) 148,
      (byte) 43,
      (byte) 145,
      (byte) 81,
      (byte) 132,
      (byte) 75,
      (byte) 253,
      (byte) 185,
      (byte) 45,
      (byte) 106,
      (byte) 165,
      (byte) 193,
      (byte) 167,
      (byte) 160 /*0xA0*/,
      (byte) 190
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 350, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_pdm_server_17060(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[36] = (byte) 24;
    sourceArray1[37] = (byte) 183;
    sourceArray1[31 /*0x1F*/] = (byte) 78;
    sourceArray1[9] = (byte) 94;
    sourceArray1[16 /*0x10*/] = (byte) 187;
    sourceArray1[5] = (byte) 226;
    sourceArray1[39] = (byte) 189;
    sourceArray1[24] = (byte) 65;
    sourceArray1[8] = (byte) 223;
    sourceArray1[20] = (byte) 10;
    sourceArray1[10] = (byte) 179;
    sourceArray1[41] = (byte) 247;
    sourceArray1[12] = (byte) 161;
    sourceArray1[13] = (byte) 1;
    sourceArray1[14] = (byte) 195;
    sourceArray1[15] = (byte) 214;
    sourceArray1[17] = (byte) 74;
    sourceArray1[35] = (byte) 234;
    sourceArray1[18] = (byte) 153;
    sourceArray1[23] = (byte) 173;
    sourceArray1[38] = (byte) 180;
    sourceArray1[21] = (byte) 210;
    sourceArray1[1] = (byte) 248;
    sourceArray1[30] = (byte) 67;
    sourceArray1[4] = (byte) 78;
    sourceArray1[25] = (byte) 207;
    sourceArray1[26] = (byte) 240 /*0xF0*/;
    sourceArray1[3] = (byte) 178;
    sourceArray1[32 /*0x20*/] = (byte) 157;
    sourceArray1[29] = (byte) 81;
    sourceArray1[27] = (byte) 177;
    sourceArray1[33] = (byte) 232;
    sourceArray1[45] = (byte) 190;
    sourceArray1[11] = (byte) 119;
    sourceArray1[34] = (byte) 2;
    sourceArray1[46] = (byte) 21;
    sourceArray1[22] = (byte) 174;
    sourceArray1[28] = (byte) 210;
    sourceArray1[7] = (byte) 35;
    sourceArray1[0] = (byte) 65;
    sourceArray1[40] = (byte) 8;
    sourceArray1[19] = (byte) 17;
    sourceArray1[42] = (byte) 73;
    sourceArray1[2] = (byte) 172;
    sourceArray1[44] = (byte) 204;
    sourceArray1[6] = (byte) 95;
    sourceArray1[43] = (byte) 22;
    sourceArray1[47] = (byte) 182;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 90,
      (byte) 115,
      (byte) 178,
      (byte) 210,
      (byte) 223,
      (byte) 141,
      (byte) 184,
      (byte) 81,
      (byte) 24,
      (byte) 128 /*0x80*/,
      (byte) 208 /*0xD0*/,
      (byte) 218,
      (byte) 118,
      (byte) 253,
      (byte) 220,
      (byte) 115,
      (byte) 155,
      (byte) 198,
      (byte) 77,
      (byte) 168,
      (byte) 145,
      (byte) 49,
      (byte) 237,
      (byte) 21,
      (byte) 253,
      (byte) 128 /*0x80*/,
      (byte) 114,
      (byte) 46,
      (byte) 181,
      (byte) 84,
      (byte) 130,
      (byte) 223,
      (byte) 198,
      (byte) 79,
      (byte) 63 /*0x3F*/,
      (byte) 67,
      (byte) 72,
      (byte) 73,
      (byte) 89,
      (byte) 203,
      (byte) 83,
      (byte) 97,
      (byte) 82,
      (byte) 177,
      (byte) 33,
      (byte) 117,
      (byte) 42,
      (byte) 122
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 350, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_pdm_server_17061()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 228,
        (byte) 92,
        (byte) 183,
        (byte) 87,
        (byte) 95,
        (byte) 113,
        (byte) 191,
        (byte) 196,
        (byte) 3,
        (byte) 178,
        (byte) 230,
        (byte) 177,
        (byte) 29,
        (byte) 238,
        (byte) 197,
        (byte) 183,
        (byte) 45,
        (byte) 154,
        (byte) 177,
        (byte) 45,
        (byte) 181,
        (byte) 73,
        (byte) 150,
        (byte) 106,
        (byte) 56,
        (byte) 164,
        (byte) 188,
        (byte) 96 /*0x60*/,
        (byte) 16 /*0x10*/,
        (byte) 174,
        (byte) 106,
        (byte) 205,
        (byte) 157,
        (byte) 120,
        (byte) 157,
        (byte) 93,
        (byte) 130,
        (byte) 41,
        (byte) 85,
        (byte) 107,
        (byte) 45,
        (byte) 234,
        (byte) 140,
        (byte) 111
      };
      byte[] numArray3 = new byte[44];
      numArray3[4] = (byte) 123;
      numArray3[28] = (byte) 220;
      numArray3[2] = (byte) 252;
      numArray3[10] = (byte) 181;
      numArray3[3] = (byte) 116;
      numArray3[42] = (byte) 142;
      numArray3[6] = (byte) 234;
      numArray3[43] = (byte) 251;
      numArray3[13] = (byte) 10;
      numArray3[27] = (byte) 173;
      numArray3[11] = (byte) 128 /*0x80*/;
      numArray3[37] = (byte) 38;
      numArray3[41] = (byte) 225;
      numArray3[0] = (byte) 144 /*0x90*/;
      numArray3[25] = (byte) 213;
      numArray3[23] = (byte) 24;
      numArray3[14] = (byte) 49;
      numArray3[17] = (byte) 225;
      numArray3[18] = (byte) 175;
      numArray3[29] = (byte) 208 /*0xD0*/;
      numArray3[20] = (byte) 143;
      numArray3[40] = (byte) 239;
      numArray3[8] = (byte) 56;
      numArray3[19] = (byte) 81;
      numArray3[24] = (byte) 186;
      numArray3[9] = (byte) 187;
      numArray3[26] = (byte) 225;
      numArray3[36] = (byte) 88;
      numArray3[5] = (byte) 186;
      numArray3[16 /*0x10*/] = (byte) 84;
      numArray3[30] = (byte) 142;
      numArray3[31 /*0x1F*/] = (byte) 140;
      numArray3[1] = (byte) 183;
      numArray3[35] = (byte) 17;
      numArray3[34] = (byte) 100;
      numArray3[12] = (byte) 84;
      numArray3[15] = (byte) 238;
      numArray3[7] = (byte) 57;
      numArray3[38] = (byte) 217;
      numArray3[39] = (byte) 223;
      numArray3[22] = (byte) 100;
      numArray3[21] = (byte) 51;
      numArray3[33] = (byte) 149;
      numArray3[32 /*0x20*/] = (byte) 21;
      key.Query(true, 350, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[44];
    byte[] numArray5 = new byte[44]
    {
      (byte) 54,
      (byte) 10,
      (byte) 155,
      (byte) 182,
      (byte) 206,
      (byte) 54,
      (byte) 55,
      (byte) 39,
      (byte) 148,
      (byte) 218,
      (byte) 85,
      (byte) 7,
      (byte) 9,
      (byte) 223,
      (byte) 40,
      (byte) 200,
      (byte) 195,
      (byte) 205,
      (byte) 231,
      (byte) 221,
      (byte) 157,
      (byte) 142,
      (byte) 213,
      (byte) 17,
      (byte) 81,
      (byte) 112 /*0x70*/,
      (byte) 183,
      (byte) 210,
      (byte) 234,
      (byte) 84,
      (byte) 68,
      (byte) 223,
      (byte) 78,
      (byte) 207,
      (byte) 253,
      (byte) 250,
      (byte) 169,
      (byte) 143,
      (byte) 49,
      (byte) 149,
      (byte) 133,
      byte.MaxValue,
      (byte) 40,
      (byte) 111
    };
    byte[] numArray6 = new byte[44]
    {
      (byte) 249,
      (byte) 152,
      (byte) 14,
      (byte) 219,
      (byte) 0,
      (byte) 237,
      (byte) 37,
      (byte) 36,
      (byte) 28,
      (byte) 117,
      (byte) 45,
      (byte) 156,
      (byte) 186,
      (byte) 163,
      (byte) 235,
      (byte) 153,
      (byte) 19,
      (byte) 136,
      (byte) 29,
      (byte) 27,
      (byte) 77,
      (byte) 0,
      (byte) 159,
      (byte) 61,
      (byte) 179,
      (byte) 230,
      (byte) 177,
      (byte) 56,
      (byte) 155,
      (byte) 166,
      (byte) 187,
      (byte) 253,
      (byte) 23,
      (byte) 80 /*0x50*/,
      (byte) 88,
      (byte) 143,
      (byte) 157,
      (byte) 159,
      (byte) 48 /*0x30*/,
      (byte) 86,
      (byte) 112 /*0x70*/,
      (byte) 79,
      (byte) 86,
      (byte) 228
    };
    key.Query(true, 350, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_pdm_server_17062(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 149,
      (byte) 0,
      (byte) 2,
      (byte) 222,
      (byte) 36,
      (byte) 250,
      (byte) 167,
      (byte) 53,
      (byte) 138,
      (byte) 100,
      (byte) 93,
      (byte) 127 /*0x7F*/,
      (byte) 100,
      (byte) 61,
      (byte) 64 /*0x40*/,
      (byte) 150,
      (byte) 118,
      (byte) 184,
      (byte) 57,
      (byte) 225,
      (byte) 45,
      (byte) 30,
      (byte) 51,
      (byte) 155,
      (byte) 81,
      (byte) 88,
      (byte) 158,
      (byte) 102,
      (byte) 101,
      (byte) 189,
      (byte) 244,
      (byte) 138,
      (byte) 155,
      (byte) 120,
      (byte) 84,
      (byte) 84,
      (byte) 65,
      (byte) 0,
      (byte) 41,
      (byte) 213,
      (byte) 86,
      (byte) 197,
      (byte) 31 /*0x1F*/,
      (byte) 42,
      (byte) 212,
      (byte) 22,
      (byte) 32 /*0x20*/,
      (byte) 194
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 85,
      (byte) 153,
      (byte) 80 /*0x50*/,
      (byte) 229,
      (byte) 170,
      (byte) 36,
      (byte) 175,
      (byte) 22,
      (byte) 235,
      (byte) 21,
      (byte) 239,
      (byte) 189,
      (byte) 157,
      (byte) 30,
      (byte) 140,
      (byte) 128 /*0x80*/,
      (byte) 18,
      (byte) 221,
      (byte) 232,
      (byte) 102,
      (byte) 34,
      (byte) 135,
      (byte) 52,
      (byte) 22,
      (byte) 161,
      (byte) 106,
      (byte) 14,
      (byte) 110,
      (byte) 32 /*0x20*/,
      (byte) 171,
      (byte) 67,
      (byte) 247,
      (byte) 206,
      (byte) 135,
      (byte) 12,
      (byte) 168,
      (byte) 92,
      (byte) 152,
      (byte) 252,
      (byte) 109,
      (byte) 161,
      (byte) 94,
      (byte) 247,
      (byte) 173,
      (byte) 213,
      (byte) 30,
      (byte) 86,
      (byte) 236
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 350, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_pdm_server_17063(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 58,
      (byte) 44,
      (byte) 118,
      (byte) 204,
      (byte) 103,
      (byte) 140,
      (byte) 204,
      (byte) 91,
      (byte) 65,
      (byte) 207,
      (byte) 204,
      (byte) 112 /*0x70*/,
      (byte) 211,
      (byte) 227,
      (byte) 235,
      (byte) 232,
      (byte) 236,
      (byte) 84,
      (byte) 117,
      (byte) 143,
      (byte) 100,
      (byte) 139,
      (byte) 104,
      byte.MaxValue,
      (byte) 8,
      (byte) 199,
      (byte) 59,
      (byte) 55,
      (byte) 220,
      (byte) 125,
      (byte) 139,
      (byte) 224 /*0xE0*/,
      (byte) 81,
      (byte) 221,
      (byte) 96 /*0x60*/,
      (byte) 76,
      (byte) 222,
      (byte) 135,
      (byte) 218,
      (byte) 116,
      (byte) 92,
      (byte) 121,
      (byte) 21,
      (byte) 245,
      (byte) 82,
      (byte) 186,
      (byte) 0,
      (byte) 50
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 211;
    sourceArray2[31 /*0x1F*/] = (byte) 13;
    sourceArray2[6] = (byte) 123;
    sourceArray2[3] = (byte) 218;
    sourceArray2[32 /*0x20*/] = (byte) 234;
    sourceArray2[5] = (byte) 39;
    sourceArray2[22] = (byte) 196;
    sourceArray2[23] = (byte) 209;
    sourceArray2[41] = (byte) 243;
    sourceArray2[9] = (byte) 51;
    sourceArray2[43] = (byte) 22;
    sourceArray2[11] = (byte) 31 /*0x1F*/;
    sourceArray2[12] = (byte) 227;
    sourceArray2[17] = (byte) 159;
    sourceArray2[14] = (byte) 217;
    sourceArray2[36] = (byte) 25;
    sourceArray2[16 /*0x10*/] = (byte) 131;
    sourceArray2[45] = (byte) 202;
    sourceArray2[18] = (byte) 150;
    sourceArray2[19] = (byte) 115;
    sourceArray2[10] = (byte) 69;
    sourceArray2[2] = (byte) 23;
    sourceArray2[47] = (byte) 107;
    sourceArray2[8] = (byte) 86;
    sourceArray2[24] = (byte) 19;
    sourceArray2[25] = (byte) 146;
    sourceArray2[15] = (byte) 219;
    sourceArray2[27] = (byte) 22;
    sourceArray2[37] = (byte) 197;
    sourceArray2[29] = (byte) 208 /*0xD0*/;
    sourceArray2[30] = (byte) 160 /*0xA0*/;
    sourceArray2[20] = (byte) 99;
    sourceArray2[35] = (byte) 170;
    sourceArray2[28] = (byte) 154;
    sourceArray2[34] = (byte) 252;
    sourceArray2[0] = (byte) 205;
    sourceArray2[42] = (byte) 25;
    sourceArray2[1] = (byte) 32 /*0x20*/;
    sourceArray2[26] = (byte) 22;
    sourceArray2[39] = (byte) 0;
    sourceArray2[13] = (byte) 141;
    sourceArray2[7] = (byte) 51;
    sourceArray2[40] = (byte) 95;
    sourceArray2[21] = (byte) 171;
    sourceArray2[44] = (byte) 209;
    sourceArray2[33] = (byte) 217;
    sourceArray2[46] = (byte) 161;
    sourceArray2[4] = (byte) 238;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 350, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
