// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13502
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13502
{
  internal static int ssp_appserver_13503(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 191,
      (byte) 146,
      (byte) 236,
      (byte) 55,
      (byte) 12,
      (byte) 217,
      (byte) 67,
      (byte) 101,
      (byte) 253,
      (byte) 76,
      (byte) 180,
      (byte) 105,
      (byte) 135,
      (byte) 222,
      (byte) 75,
      (byte) 48 /*0x30*/,
      (byte) 26,
      (byte) 113,
      (byte) 131,
      (byte) 96 /*0x60*/,
      (byte) 253,
      (byte) 14,
      (byte) 142,
      (byte) 190,
      (byte) 2,
      (byte) 233,
      (byte) 234,
      (byte) 97,
      (byte) 67,
      (byte) 27,
      (byte) 233,
      (byte) 248,
      (byte) 33,
      (byte) 242,
      (byte) 90,
      (byte) 103,
      (byte) 105,
      (byte) 58,
      (byte) 6,
      (byte) 179,
      (byte) 32 /*0x20*/,
      (byte) 162,
      (byte) 147,
      (byte) 21,
      (byte) 198,
      (byte) 232,
      (byte) 118,
      (byte) 22
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 76,
      (byte) 20,
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 21,
      (byte) 174,
      (byte) 130,
      (byte) 161,
      (byte) 28,
      (byte) 153,
      (byte) 155,
      (byte) 133,
      (byte) 44,
      (byte) 91,
      (byte) 95,
      (byte) 224 /*0xE0*/,
      (byte) 29,
      (byte) 143,
      (byte) 176 /*0xB0*/,
      (byte) 1,
      (byte) 197,
      (byte) 6,
      (byte) 154,
      (byte) 73,
      (byte) 222,
      (byte) 160 /*0xA0*/,
      (byte) 110,
      (byte) 26,
      (byte) 122,
      (byte) 178,
      (byte) 79,
      (byte) 66,
      (byte) 166,
      (byte) 49,
      (byte) 116,
      (byte) 178,
      (byte) 75,
      (byte) 95,
      (byte) 65,
      (byte) 117,
      (byte) 39,
      (byte) 245,
      (byte) 58,
      (byte) 127 /*0x7F*/,
      (byte) 182,
      (byte) 208 /*0xD0*/,
      (byte) 207,
      (byte) 167
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13504(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 196,
      (byte) 177,
      (byte) 176 /*0xB0*/,
      (byte) 3,
      (byte) 45,
      (byte) 52,
      (byte) 35,
      (byte) 238,
      (byte) 225,
      (byte) 34,
      (byte) 225,
      (byte) 99,
      (byte) 187,
      (byte) 71,
      (byte) 149,
      (byte) 132,
      (byte) 227,
      (byte) 198,
      (byte) 223,
      (byte) 107,
      (byte) 207,
      (byte) 64 /*0x40*/,
      (byte) 91,
      (byte) 122,
      (byte) 121,
      (byte) 248,
      (byte) 26,
      (byte) 114,
      (byte) 212,
      (byte) 45,
      (byte) 242,
      (byte) 12,
      (byte) 98,
      (byte) 143,
      (byte) 92,
      (byte) 65,
      (byte) 169,
      (byte) 99,
      (byte) 60,
      (byte) 5,
      (byte) 227,
      (byte) 196,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 178,
      (byte) 177,
      (byte) 10,
      (byte) 160 /*0xA0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 111,
      (byte) 100,
      (byte) 144 /*0x90*/,
      (byte) 197,
      (byte) 157,
      (byte) 71,
      (byte) 229,
      (byte) 224 /*0xE0*/,
      (byte) 251,
      (byte) 43,
      (byte) 133,
      (byte) 97,
      (byte) 5,
      (byte) 8,
      (byte) 172,
      (byte) 31 /*0x1F*/,
      (byte) 80 /*0x50*/,
      (byte) 224 /*0xE0*/,
      (byte) 151,
      (byte) 156,
      (byte) 201,
      (byte) 164,
      (byte) 163,
      (byte) 19,
      (byte) 45,
      (byte) 146,
      (byte) 139,
      (byte) 6,
      (byte) 168,
      (byte) 52,
      (byte) 174,
      (byte) 198,
      (byte) 176 /*0xB0*/,
      (byte) 88,
      (byte) 189,
      (byte) 12,
      (byte) 64 /*0x40*/,
      (byte) 220,
      (byte) 59,
      (byte) 55,
      (byte) 108,
      (byte) 12,
      (byte) 114,
      (byte) 112 /*0x70*/,
      (byte) 64 /*0x40*/,
      (byte) 43,
      (byte) 215,
      (byte) 71
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13505(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 27,
      (byte) 152,
      (byte) 132,
      (byte) 86,
      (byte) 53,
      (byte) 137,
      (byte) 53,
      (byte) 26,
      (byte) 176 /*0xB0*/,
      (byte) 26,
      (byte) 180,
      (byte) 47,
      (byte) 213,
      (byte) 0,
      (byte) 44,
      (byte) 235,
      (byte) 84,
      (byte) 218,
      (byte) 128 /*0x80*/,
      (byte) 138,
      (byte) 218,
      (byte) 135,
      (byte) 10,
      (byte) 1,
      (byte) 218,
      (byte) 200,
      (byte) 199,
      (byte) 237,
      (byte) 121,
      (byte) 145,
      (byte) 66,
      (byte) 220,
      (byte) 103,
      (byte) 221,
      (byte) 70,
      (byte) 246,
      (byte) 216,
      (byte) 235,
      (byte) 115,
      (byte) 217,
      (byte) 6,
      (byte) 234,
      (byte) 51,
      (byte) 170,
      (byte) 138,
      (byte) 80 /*0x50*/,
      (byte) 221,
      (byte) 177
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 44,
      (byte) 54,
      (byte) 193,
      (byte) 208 /*0xD0*/,
      (byte) 97,
      (byte) 156,
      (byte) 6,
      (byte) 188,
      (byte) 206,
      (byte) 88,
      (byte) 140,
      (byte) 147,
      (byte) 49,
      (byte) 136,
      (byte) 101,
      (byte) 9,
      (byte) 94,
      (byte) 110,
      (byte) 13,
      (byte) 71,
      (byte) 43,
      (byte) 235,
      (byte) 14,
      (byte) 0,
      (byte) 61,
      (byte) 129,
      (byte) 4,
      (byte) 86,
      (byte) 148,
      (byte) 180,
      (byte) 90,
      (byte) 162,
      (byte) 171,
      (byte) 237,
      (byte) 147,
      (byte) 214,
      (byte) 242,
      (byte) 212,
      (byte) 151,
      (byte) 1,
      (byte) 158,
      (byte) 171,
      (byte) 129,
      (byte) 196,
      (byte) 245,
      (byte) 69,
      (byte) 72,
      byte.MaxValue
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13506()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[134];
      byte[] numArray2 = new byte[55]
      {
        (byte) 208 /*0xD0*/,
        (byte) 0,
        (byte) 84,
        (byte) 148,
        (byte) 142,
        (byte) 130,
        (byte) 123,
        (byte) 109,
        (byte) 194,
        (byte) 71,
        (byte) 161,
        (byte) 31 /*0x1F*/,
        (byte) 215,
        (byte) 216,
        (byte) 200,
        (byte) 247,
        (byte) 185,
        (byte) 53,
        (byte) 18,
        (byte) 105,
        (byte) 103,
        (byte) 166,
        (byte) 131,
        (byte) 239,
        (byte) 84,
        (byte) 95,
        (byte) 119,
        (byte) 137,
        (byte) 235,
        (byte) 22,
        (byte) 15,
        (byte) 195,
        (byte) 4,
        (byte) 138,
        (byte) 11,
        (byte) 103,
        (byte) 136,
        (byte) 178,
        (byte) 104,
        (byte) 85,
        (byte) 111,
        (byte) 6,
        (byte) 23,
        (byte) 191,
        (byte) 73,
        (byte) 19,
        (byte) 211,
        (byte) 177,
        (byte) 136,
        (byte) 229,
        (byte) 247,
        (byte) 169,
        (byte) 163,
        (byte) 35,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 163,
        (byte) 145,
        (byte) 10,
        (byte) 142,
        (byte) 174,
        (byte) 123,
        (byte) 251,
        (byte) 130,
        (byte) 32 /*0x20*/,
        (byte) 227,
        (byte) 205,
        (byte) 245,
        (byte) 7,
        (byte) 240 /*0xF0*/,
        (byte) 35,
        (byte) 107,
        (byte) 43,
        (byte) 195,
        (byte) 159,
        (byte) 118,
        (byte) 214,
        (byte) 5,
        (byte) 237,
        (byte) 40,
        (byte) 80 /*0x50*/,
        (byte) 14,
        (byte) 10,
        (byte) 13,
        (byte) 24,
        (byte) 217,
        (byte) 36,
        (byte) 212,
        (byte) 38,
        (byte) 6,
        (byte) 132,
        (byte) 178,
        (byte) 0,
        (byte) 89,
        (byte) 85,
        (byte) 131,
        (byte) 215,
        (byte) 176 /*0xB0*/,
        (byte) 139,
        (byte) 156,
        (byte) 142,
        (byte) 7,
        (byte) 43,
        (byte) 204,
        (byte) 196,
        (byte) 182,
        (byte) 185,
        (byte) 197,
        (byte) 55,
        (byte) 213,
        (byte) 247
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 100,
        (byte) 63 /*0x3F*/,
        (byte) 46,
        (byte) 11,
        (byte) 241,
        (byte) 129,
        (byte) 171,
        (byte) 56,
        (byte) 221,
        (byte) 27,
        (byte) 76,
        (byte) 204,
        (byte) 97,
        (byte) 110,
        (byte) 6,
        (byte) 204,
        (byte) 188,
        (byte) 249,
        (byte) 96 /*0x60*/,
        (byte) 142,
        (byte) 195,
        (byte) 166,
        (byte) 64 /*0x40*/,
        (byte) 140,
        (byte) 197,
        (byte) 0,
        (byte) 5,
        (byte) 30,
        (byte) 102,
        (byte) 32 /*0x20*/,
        (byte) 224 /*0xE0*/,
        (byte) 58,
        (byte) 225,
        (byte) 233,
        (byte) 35,
        (byte) 115,
        (byte) 58,
        (byte) 74,
        (byte) 125,
        (byte) 132,
        (byte) 107,
        (byte) 135,
        (byte) 107,
        (byte) 248,
        (byte) 134,
        (byte) 229,
        (byte) 39,
        (byte) 119,
        (byte) 240 /*0xF0*/,
        (byte) 221,
        (byte) 230,
        (byte) 41,
        (byte) 239,
        (byte) 155,
        (byte) 185
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 16 /*0x10*/,
        (byte) 230,
        (byte) 198,
        (byte) 184,
        (byte) 238,
        (byte) 142,
        (byte) 50,
        (byte) 233,
        (byte) 98,
        (byte) 218,
        (byte) 54,
        (byte) 146,
        (byte) 168,
        (byte) 39,
        (byte) 158,
        (byte) 173,
        (byte) 35,
        (byte) 113,
        (byte) 24,
        (byte) 22,
        (byte) 171,
        (byte) 190,
        (byte) 160 /*0xA0*/,
        (byte) 182,
        (byte) 188,
        (byte) 106,
        (byte) 116,
        (byte) 76,
        (byte) 247,
        (byte) 201,
        (byte) 150,
        (byte) 59,
        (byte) 165,
        (byte) 38,
        (byte) 75,
        (byte) 222,
        (byte) 59,
        (byte) 67,
        (byte) 20,
        (byte) 94,
        (byte) 62,
        (byte) 37,
        (byte) 40,
        (byte) 162,
        (byte) 243,
        (byte) 209,
        (byte) 220,
        (byte) 200,
        (byte) 166,
        (byte) 43,
        (byte) 85,
        (byte) 38,
        (byte) 76,
        (byte) 67,
        (byte) 53
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[24];
      numArray6[3] = (byte) 123;
      numArray6[1] = (byte) 70;
      numArray6[2] = (byte) 241;
      numArray6[15] = (byte) 124;
      numArray6[0] = (byte) 161;
      numArray6[17] = (byte) 34;
      numArray6[18] = (byte) 60;
      numArray6[7] = (byte) 58;
      numArray6[5] = (byte) 209;
      numArray6[6] = (byte) 188;
      numArray6[23] = (byte) 217;
      numArray6[11] = (byte) 74;
      numArray6[12] = (byte) 147;
      numArray6[13] = (byte) 145;
      numArray6[14] = (byte) 114;
      numArray6[8] = (byte) 18;
      numArray6[16 /*0x10*/] = (byte) 219;
      numArray6[20] = (byte) 71;
      numArray6[10] = (byte) 119;
      numArray6[19] = (byte) 59;
      numArray6[4] = (byte) 140;
      numArray6[21] = (byte) 6;
      numArray6[22] = (byte) 223;
      numArray6[9] = (byte) 234;
      byte[] numArray7 = new byte[24]
      {
        (byte) 27,
        (byte) 169,
        (byte) 150,
        (byte) 10,
        (byte) 21,
        (byte) 206,
        (byte) 58,
        (byte) 92,
        (byte) 171,
        (byte) 237,
        (byte) 113,
        (byte) 19,
        (byte) 181,
        (byte) 115,
        (byte) 6,
        (byte) 69,
        (byte) 87,
        (byte) 167,
        (byte) 32 /*0x20*/,
        (byte) 170,
        (byte) 207,
        (byte) 240 /*0xF0*/,
        (byte) 44,
        (byte) 97
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[134];
    byte[] numArray9 = new byte[55]
    {
      (byte) 121,
      (byte) 125,
      (byte) 130,
      (byte) 183,
      (byte) 154,
      (byte) 102,
      (byte) 23,
      (byte) 237,
      (byte) 94,
      (byte) 72,
      (byte) 131,
      (byte) 132,
      (byte) 200,
      (byte) 165,
      (byte) 71,
      (byte) 228,
      (byte) 31 /*0x1F*/,
      (byte) 247,
      (byte) 109,
      (byte) 240 /*0xF0*/,
      (byte) 210,
      (byte) 67,
      (byte) 134,
      (byte) 140,
      (byte) 137,
      (byte) 147,
      (byte) 229,
      (byte) 40,
      (byte) 98,
      (byte) 208 /*0xD0*/,
      (byte) 61,
      (byte) 197,
      (byte) 9,
      (byte) 53,
      (byte) 204,
      (byte) 178,
      (byte) 226,
      (byte) 188,
      byte.MaxValue,
      (byte) 158,
      (byte) 116,
      (byte) 28,
      (byte) 127 /*0x7F*/,
      (byte) 108,
      (byte) 43,
      (byte) 57,
      (byte) 171,
      (byte) 85,
      (byte) 105,
      (byte) 71,
      (byte) 62,
      (byte) 83,
      (byte) 6,
      (byte) 189,
      (byte) 66
    };
    byte[] numArray10 = new byte[55];
    numArray10[53] = (byte) 173;
    numArray10[1] = (byte) 213;
    numArray10[50] = (byte) 216;
    numArray10[2] = (byte) 80 /*0x50*/;
    numArray10[4] = (byte) 105;
    numArray10[15] = (byte) 56;
    numArray10[6] = (byte) 193;
    numArray10[38] = (byte) 147;
    numArray10[21] = (byte) 121;
    numArray10[37] = (byte) 182;
    numArray10[10] = (byte) 188;
    numArray10[3] = (byte) 241;
    numArray10[12] = (byte) 119;
    numArray10[26] = (byte) 206;
    numArray10[14] = (byte) 184;
    numArray10[40] = (byte) 252;
    numArray10[29] = (byte) 185;
    numArray10[17] = (byte) 229;
    numArray10[47] = (byte) 66;
    numArray10[19] = (byte) 238;
    numArray10[20] = (byte) 159;
    numArray10[30] = (byte) 173;
    numArray10[22] = (byte) 9;
    numArray10[23] = (byte) 200;
    numArray10[24] = (byte) 185;
    numArray10[9] = (byte) 239;
    numArray10[35] = (byte) 77;
    numArray10[44] = (byte) 217;
    numArray10[28] = (byte) 160 /*0xA0*/;
    numArray10[31 /*0x1F*/] = (byte) 219;
    numArray10[51] = (byte) 248;
    numArray10[33] = (byte) 205;
    numArray10[32 /*0x20*/] = (byte) 39;
    numArray10[41] = (byte) 97;
    numArray10[0] = (byte) 155;
    numArray10[54] = (byte) 9;
    numArray10[18] = (byte) 190;
    numArray10[48 /*0x30*/] = (byte) 132;
    numArray10[16 /*0x10*/] = (byte) 225;
    numArray10[39] = (byte) 138;
    numArray10[5] = (byte) 96 /*0x60*/;
    numArray10[49] = (byte) 50;
    numArray10[42] = (byte) 125;
    numArray10[43] = (byte) 18;
    numArray10[36] = (byte) 191;
    numArray10[11] = (byte) 21;
    numArray10[46] = (byte) 251;
    numArray10[7] = (byte) 183;
    numArray10[34] = (byte) 6;
    numArray10[25] = (byte) 134;
    numArray10[13] = (byte) 135;
    numArray10[45] = (byte) 239;
    numArray10[52] = (byte) 211;
    numArray10[8] = (byte) 24;
    numArray10[27] = (byte) 193;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 149,
      (byte) 221,
      (byte) 30,
      (byte) 241,
      (byte) 46,
      (byte) 111,
      (byte) 251,
      (byte) 33,
      byte.MaxValue,
      (byte) 46,
      (byte) 158,
      (byte) 65,
      (byte) 216,
      (byte) 21,
      (byte) 211,
      byte.MaxValue,
      (byte) 178,
      (byte) 245,
      (byte) 36,
      (byte) 210,
      (byte) 183,
      (byte) 243,
      (byte) 144 /*0x90*/,
      (byte) 207,
      (byte) 18,
      (byte) 235,
      (byte) 213,
      (byte) 183,
      (byte) 141,
      (byte) 135,
      (byte) 10,
      (byte) 104,
      (byte) 37,
      (byte) 223,
      (byte) 86,
      (byte) 177,
      (byte) 217,
      (byte) 43,
      (byte) 86,
      (byte) 33,
      (byte) 245,
      (byte) 64 /*0x40*/,
      (byte) 110,
      (byte) 144 /*0x90*/,
      (byte) 122,
      (byte) 59,
      (byte) 252,
      (byte) 48 /*0x30*/,
      (byte) 8,
      (byte) 92,
      (byte) 131,
      (byte) 75,
      (byte) 102,
      (byte) 159,
      (byte) 37
    };
    byte[] numArray12 = new byte[55];
    numArray12[33] = (byte) 246;
    numArray12[10] = (byte) 125;
    numArray12[30] = (byte) 132;
    numArray12[3] = (byte) 27;
    numArray12[4] = (byte) 235;
    numArray12[5] = (byte) 250;
    numArray12[24] = (byte) 139;
    numArray12[7] = (byte) 147;
    numArray12[8] = (byte) 123;
    numArray12[41] = (byte) 234;
    numArray12[47] = (byte) 143;
    numArray12[44] = (byte) 246;
    numArray12[12] = (byte) 102;
    numArray12[51] = (byte) 220;
    numArray12[14] = (byte) 54;
    numArray12[15] = (byte) 162;
    numArray12[27] = (byte) 202;
    numArray12[9] = (byte) 118;
    numArray12[18] = (byte) 216;
    numArray12[19] = (byte) 105;
    numArray12[20] = (byte) 81;
    numArray12[37] = (byte) 102;
    numArray12[22] = (byte) 83;
    numArray12[23] = (byte) 180;
    numArray12[16 /*0x10*/] = (byte) 90;
    numArray12[25] = (byte) 20;
    numArray12[17] = (byte) 157;
    numArray12[28] = (byte) 2;
    numArray12[40] = (byte) 23;
    numArray12[29] = (byte) 148;
    numArray12[2] = (byte) 25;
    numArray12[31 /*0x1F*/] = (byte) 156;
    numArray12[54] = (byte) 153;
    numArray12[53] = (byte) 3;
    numArray12[34] = (byte) 180;
    numArray12[11] = (byte) 44;
    numArray12[21] = (byte) 73;
    numArray12[38] = (byte) 217;
    numArray12[52] = (byte) 0;
    numArray12[50] = (byte) 194;
    numArray12[36] = (byte) 99;
    numArray12[48 /*0x30*/] = (byte) 20;
    numArray12[6] = (byte) 188;
    numArray12[43] = (byte) 31 /*0x1F*/;
    numArray12[0] = (byte) 26;
    numArray12[32 /*0x20*/] = (byte) 48 /*0x30*/;
    numArray12[46] = (byte) 246;
    numArray12[35] = (byte) 218;
    numArray12[39] = (byte) 136;
    numArray12[49] = (byte) 201;
    numArray12[1] = (byte) 181;
    numArray12[13] = (byte) 18;
    numArray12[26] = (byte) 188;
    numArray12[42] = (byte) 231;
    numArray12[45] = (byte) 223;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[24];
    numArray13[23] = (byte) 234;
    numArray13[1] = (byte) 8;
    numArray13[16 /*0x10*/] = (byte) 125;
    numArray13[3] = (byte) 245;
    numArray13[5] = (byte) 97;
    numArray13[19] = (byte) 141;
    numArray13[6] = (byte) 115;
    numArray13[8] = (byte) 53;
    numArray13[4] = (byte) 149;
    numArray13[9] = (byte) 149;
    numArray13[10] = (byte) 62;
    numArray13[11] = (byte) 10;
    numArray13[20] = (byte) 147;
    numArray13[13] = (byte) 155;
    numArray13[2] = (byte) 85;
    numArray13[12] = (byte) 189;
    numArray13[18] = (byte) 202;
    numArray13[17] = (byte) 42;
    numArray13[21] = (byte) 17;
    numArray13[15] = (byte) 139;
    numArray13[0] = (byte) 250;
    numArray13[14] = (byte) 59;
    numArray13[22] = (byte) 183;
    numArray13[7] = (byte) 101;
    byte[] numArray14 = new byte[24];
    numArray14[12] = (byte) 157;
    numArray14[16 /*0x10*/] = (byte) 182;
    numArray14[5] = (byte) 81;
    numArray14[23] = (byte) 114;
    numArray14[4] = (byte) 124;
    numArray14[13] = (byte) 175;
    numArray14[18] = (byte) 126;
    numArray14[7] = (byte) 161;
    numArray14[10] = (byte) 114;
    numArray14[8] = (byte) 1;
    numArray14[1] = (byte) 207;
    numArray14[11] = (byte) 79;
    numArray14[0] = (byte) 103;
    numArray14[9] = (byte) 135;
    numArray14[14] = (byte) 112 /*0x70*/;
    numArray14[22] = (byte) 190;
    numArray14[6] = (byte) 194;
    numArray14[17] = (byte) 223;
    numArray14[15] = (byte) 152;
    numArray14[19] = (byte) 212;
    numArray14[2] = (byte) 204;
    numArray14[21] = (byte) 160 /*0xA0*/;
    numArray14[3] = (byte) 42;
    numArray14[20] = (byte) 119;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 24);
    for (int index = 0; index < 24; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
