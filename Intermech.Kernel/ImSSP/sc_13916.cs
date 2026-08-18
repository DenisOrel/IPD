// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13916
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13916
{
  private static byte[] sspq = new byte[131]
  {
    (byte) 94,
    (byte) 118,
    (byte) 175,
    (byte) 228,
    (byte) 136,
    (byte) 127 /*0x7F*/,
    (byte) 200,
    (byte) 51,
    (byte) 126,
    (byte) 244,
    (byte) 229,
    (byte) 156,
    (byte) 181,
    (byte) 36,
    (byte) 37,
    (byte) 20,
    (byte) 185,
    (byte) 65,
    (byte) 62,
    (byte) 69,
    (byte) 229,
    (byte) 242,
    (byte) 198,
    (byte) 96 /*0x60*/,
    (byte) 46,
    (byte) 153,
    (byte) 184,
    (byte) 79,
    (byte) 115,
    (byte) 236,
    (byte) 89,
    (byte) 48 /*0x30*/,
    (byte) 138,
    (byte) 168,
    (byte) 216,
    (byte) 97,
    (byte) 152,
    (byte) 134,
    (byte) 96 /*0x60*/,
    (byte) 4,
    (byte) 65,
    (byte) 221,
    (byte) 15,
    (byte) 175,
    (byte) 254,
    (byte) 162,
    (byte) 117,
    (byte) 125,
    (byte) 34,
    (byte) 73,
    (byte) 9,
    (byte) 187,
    (byte) 151,
    (byte) 107,
    (byte) 136,
    (byte) 98,
    (byte) 163,
    (byte) 147,
    (byte) 248,
    (byte) 104,
    (byte) 254,
    (byte) 117,
    (byte) 82,
    (byte) 46,
    (byte) 210,
    (byte) 51,
    (byte) 193,
    (byte) 177,
    (byte) 157,
    (byte) 34,
    (byte) 17,
    (byte) 204,
    (byte) 113,
    (byte) 20,
    (byte) 236,
    (byte) 176 /*0xB0*/,
    (byte) 191,
    (byte) 51,
    (byte) 181,
    (byte) 190,
    (byte) 186,
    (byte) 109,
    (byte) 84,
    (byte) 1,
    (byte) 165,
    (byte) 219,
    (byte) 165,
    (byte) 32 /*0x20*/,
    (byte) 2,
    (byte) 7,
    (byte) 21,
    (byte) 153,
    (byte) 181,
    (byte) 199,
    (byte) 127 /*0x7F*/,
    (byte) 54,
    (byte) 228,
    (byte) 100,
    byte.MaxValue,
    (byte) 176 /*0xB0*/,
    (byte) 33,
    (byte) 110,
    (byte) 153,
    (byte) 193,
    (byte) 128 /*0x80*/,
    (byte) 107,
    (byte) 92,
    (byte) 195,
    (byte) 236,
    (byte) 126,
    (byte) 250,
    (byte) 51,
    (byte) 23,
    (byte) 3,
    (byte) 31 /*0x1F*/,
    (byte) 230,
    (byte) 161,
    (byte) 10,
    (byte) 137,
    (byte) 168,
    (byte) 225,
    (byte) 63 /*0x3F*/,
    (byte) 163,
    (byte) 59,
    (byte) 106,
    (byte) 156,
    (byte) 253,
    (byte) 130,
    (byte) 78,
    (byte) 236,
    (byte) 238
  };
  private static byte[] sspr = new byte[131]
  {
    (byte) 165,
    (byte) 183,
    (byte) 149,
    (byte) 14,
    (byte) 87,
    (byte) 65,
    (byte) 212,
    (byte) 110,
    (byte) 231,
    (byte) 22,
    (byte) 233,
    (byte) 233,
    (byte) 72,
    (byte) 221,
    (byte) 15,
    (byte) 173,
    (byte) 227,
    (byte) 246,
    (byte) 38,
    (byte) 52,
    (byte) 217,
    (byte) 76,
    (byte) 179,
    (byte) 224 /*0xE0*/,
    (byte) 34,
    (byte) 49,
    (byte) 152,
    (byte) 74,
    (byte) 220,
    (byte) 151,
    (byte) 50,
    (byte) 242,
    (byte) 145,
    (byte) 104,
    (byte) 127 /*0x7F*/,
    (byte) 111,
    (byte) 152,
    (byte) 113,
    (byte) 97,
    (byte) 217,
    (byte) 133,
    (byte) 188,
    (byte) 136,
    (byte) 151,
    (byte) 25,
    (byte) 136,
    (byte) 203,
    (byte) 193,
    (byte) 179,
    (byte) 103,
    (byte) 145,
    (byte) 9,
    (byte) 200,
    (byte) 32 /*0x20*/,
    (byte) 197,
    (byte) 203,
    (byte) 242,
    (byte) 175,
    (byte) 81,
    (byte) 26,
    (byte) 231,
    (byte) 247,
    (byte) 161,
    (byte) 156,
    (byte) 1,
    (byte) 253,
    (byte) 188,
    (byte) 138,
    (byte) 254,
    (byte) 79,
    (byte) 42,
    (byte) 57,
    (byte) 199,
    (byte) 14,
    (byte) 17,
    (byte) 230,
    (byte) 58,
    (byte) 121,
    (byte) 163,
    (byte) 210,
    (byte) 254,
    (byte) 32 /*0x20*/,
    (byte) 27,
    (byte) 82,
    (byte) 14,
    (byte) 0,
    (byte) 138,
    (byte) 97,
    (byte) 247,
    (byte) 9,
    (byte) 147,
    (byte) 207,
    (byte) 110,
    (byte) 84,
    (byte) 114,
    (byte) 171,
    (byte) 176 /*0xB0*/,
    (byte) 155,
    (byte) 91,
    (byte) 24,
    (byte) 121,
    (byte) 191,
    (byte) 177,
    (byte) 145,
    (byte) 168,
    (byte) 238,
    (byte) 79,
    (byte) 119,
    (byte) 235,
    (byte) 25,
    (byte) 123,
    (byte) 223,
    (byte) 216,
    (byte) 67,
    (byte) 195,
    (byte) 185,
    (byte) 170,
    (byte) 78,
    (byte) 91,
    (byte) 67,
    (byte) 115,
    (byte) 245,
    (byte) 95,
    (byte) 11,
    (byte) 236,
    (byte) 238,
    (byte) 201,
    (byte) 46,
    (byte) 111,
    (byte) 43,
    (byte) 224 /*0xE0*/
  };

  internal static int ssp_appserver_13917(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 170,
      (byte) 243,
      (byte) 106,
      (byte) 187,
      (byte) 185,
      (byte) 91,
      (byte) 42,
      (byte) 11,
      (byte) 246,
      (byte) 62,
      (byte) 149,
      (byte) 42,
      (byte) 226,
      (byte) 45,
      (byte) 182,
      (byte) 138,
      (byte) 42,
      (byte) 241,
      (byte) 77,
      (byte) 99,
      (byte) 62,
      (byte) 161,
      (byte) 145,
      (byte) 250,
      (byte) 211,
      (byte) 153,
      (byte) 253,
      (byte) 167,
      (byte) 235,
      (byte) 31 /*0x1F*/,
      (byte) 133,
      (byte) 147,
      (byte) 121,
      (byte) 185,
      (byte) 39,
      (byte) 189,
      (byte) 6,
      (byte) 35,
      (byte) 168,
      (byte) 10,
      (byte) 38,
      (byte) 219,
      (byte) 125,
      (byte) 203,
      (byte) 62,
      (byte) 15,
      (byte) 159,
      (byte) 181
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 200;
    sourceArray2[1] = (byte) 156;
    sourceArray2[5] = (byte) 125;
    sourceArray2[44] = (byte) 15;
    sourceArray2[2] = (byte) 201;
    sourceArray2[0] = (byte) 23;
    sourceArray2[10] = (byte) 110;
    sourceArray2[32 /*0x20*/] = (byte) 158;
    sourceArray2[8] = (byte) 228;
    sourceArray2[9] = (byte) 249;
    sourceArray2[27] = (byte) 192 /*0xC0*/;
    sourceArray2[11] = (byte) 187;
    sourceArray2[6] = (byte) 116;
    sourceArray2[26] = (byte) 182;
    sourceArray2[14] = (byte) 91;
    sourceArray2[15] = (byte) 142;
    sourceArray2[16 /*0x10*/] = (byte) 95;
    sourceArray2[29] = (byte) 88;
    sourceArray2[39] = (byte) 153;
    sourceArray2[40] = (byte) 80 /*0x50*/;
    sourceArray2[20] = (byte) 112 /*0x70*/;
    sourceArray2[17] = (byte) 33;
    sourceArray2[22] = (byte) 136;
    sourceArray2[45] = (byte) 156;
    sourceArray2[4] = (byte) 75;
    sourceArray2[46] = (byte) 128 /*0x80*/;
    sourceArray2[25] = (byte) 171;
    sourceArray2[21] = (byte) 127 /*0x7F*/;
    sourceArray2[28] = (byte) 11;
    sourceArray2[36] = (byte) 121;
    sourceArray2[30] = (byte) 46;
    sourceArray2[18] = (byte) 142;
    sourceArray2[41] = (byte) 56;
    sourceArray2[33] = (byte) 65;
    sourceArray2[34] = (byte) 67;
    sourceArray2[35] = (byte) 74;
    sourceArray2[7] = (byte) 40;
    sourceArray2[24] = (byte) 136;
    sourceArray2[38] = (byte) 195;
    sourceArray2[3] = (byte) 78;
    sourceArray2[31 /*0x1F*/] = (byte) 6;
    sourceArray2[23] = (byte) 13;
    sourceArray2[42] = (byte) 59;
    sourceArray2[43] = (byte) 203;
    sourceArray2[12] = (byte) 215;
    sourceArray2[19] = (byte) 161;
    sourceArray2[37] = (byte) 164;
    sourceArray2[47] = (byte) 246;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[23];
    byte[] response2 = new byte[23];
    Array.Copy((Array) sc_13916.sspq, 0, (Array) numArray2, 0, 23);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13916.sspr, 0, (Array) numArray2, 0, 23);
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

  internal static int ssp_appserver_13918(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 238,
      (byte) 204,
      (byte) 192 /*0xC0*/,
      (byte) 150,
      (byte) 153,
      (byte) 167,
      (byte) 124,
      (byte) 180,
      (byte) 127 /*0x7F*/,
      (byte) 100,
      (byte) 132,
      (byte) 152,
      (byte) 229,
      (byte) 85,
      (byte) 25,
      (byte) 172,
      (byte) 234,
      (byte) 63 /*0x3F*/,
      (byte) 109,
      (byte) 56,
      (byte) 190,
      (byte) 224 /*0xE0*/,
      (byte) 23,
      (byte) 99,
      (byte) 152,
      (byte) 234,
      (byte) 121,
      (byte) 104,
      (byte) 204,
      (byte) 87,
      (byte) 108,
      (byte) 175,
      (byte) 200,
      (byte) 71,
      (byte) 154,
      (byte) 179,
      (byte) 7,
      (byte) 156,
      (byte) 228,
      (byte) 77,
      (byte) 104,
      (byte) 232,
      (byte) 53,
      (byte) 191,
      (byte) 160 /*0xA0*/,
      (byte) 124,
      (byte) 211,
      (byte) 236
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 46,
      (byte) 108,
      (byte) 93,
      (byte) 251,
      (byte) 209,
      (byte) 50,
      (byte) 108,
      (byte) 160 /*0xA0*/,
      (byte) 78,
      (byte) 57,
      (byte) 188,
      (byte) 136,
      (byte) 3,
      (byte) 7,
      (byte) 230,
      (byte) 27,
      (byte) 35,
      (byte) 80 /*0x50*/,
      (byte) 237,
      (byte) 22,
      (byte) 26,
      (byte) 52,
      (byte) 182,
      (byte) 121,
      (byte) 103,
      (byte) 205,
      (byte) 83,
      (byte) 49,
      (byte) 135,
      (byte) 119,
      (byte) 207,
      (byte) 1,
      (byte) 44,
      (byte) 158,
      (byte) 204,
      (byte) 4,
      (byte) 101,
      (byte) 160 /*0xA0*/,
      (byte) 174,
      (byte) 8,
      (byte) 109,
      (byte) 187,
      (byte) 233,
      (byte) 150,
      (byte) 133,
      (byte) 254,
      (byte) 240 /*0xF0*/,
      (byte) 103
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13919(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 174,
      (byte) 222,
      (byte) 13,
      (byte) 180,
      (byte) 92,
      (byte) 254,
      (byte) 175,
      (byte) 46,
      (byte) 20,
      (byte) 231,
      (byte) 40,
      (byte) 248,
      (byte) 212,
      (byte) 208 /*0xD0*/,
      (byte) 157,
      (byte) 187,
      (byte) 102,
      (byte) 164,
      (byte) 77,
      (byte) 90,
      (byte) 32 /*0x20*/,
      (byte) 207,
      (byte) 214,
      (byte) 170,
      (byte) 87,
      (byte) 194,
      (byte) 27,
      (byte) 22,
      (byte) 141,
      (byte) 67,
      (byte) 51,
      (byte) 91,
      (byte) 184,
      (byte) 85,
      (byte) 25,
      (byte) 105,
      (byte) 140,
      (byte) 254,
      (byte) 171,
      (byte) 158,
      (byte) 53,
      (byte) 30,
      (byte) 84,
      (byte) 1,
      (byte) 13,
      (byte) 199,
      (byte) 204,
      (byte) 171
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 209,
      (byte) 82,
      (byte) 3,
      (byte) 98,
      (byte) 106,
      (byte) 221,
      (byte) 186,
      (byte) 24,
      (byte) 80 /*0x50*/,
      (byte) 92,
      (byte) 106,
      (byte) 161,
      (byte) 226,
      (byte) 49,
      (byte) 102,
      (byte) 188,
      (byte) 253,
      (byte) 184,
      (byte) 224 /*0xE0*/,
      (byte) 31 /*0x1F*/,
      (byte) 77,
      (byte) 250,
      (byte) 41,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 142,
      (byte) 129,
      (byte) 231,
      (byte) 209,
      (byte) 30,
      (byte) 170,
      (byte) 137,
      (byte) 7,
      (byte) 131,
      (byte) 80 /*0x50*/,
      (byte) 26,
      (byte) 146,
      (byte) 24,
      (byte) 159,
      (byte) 201,
      (byte) 192 /*0xC0*/,
      (byte) 47,
      (byte) 57,
      (byte) 248,
      (byte) 2,
      (byte) 234,
      (byte) 73
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13920(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 28;
    sourceArray1[20] = (byte) 174;
    sourceArray1[38] = (byte) 41;
    sourceArray1[3] = (byte) 8;
    sourceArray1[2] = (byte) 222;
    sourceArray1[5] = (byte) 93;
    sourceArray1[47] = (byte) 135;
    sourceArray1[25] = (byte) 156;
    sourceArray1[40] = (byte) 17;
    sourceArray1[28] = (byte) 111;
    sourceArray1[16 /*0x10*/] = (byte) 52;
    sourceArray1[11] = (byte) 179;
    sourceArray1[29] = (byte) 224 /*0xE0*/;
    sourceArray1[14] = (byte) 126;
    sourceArray1[4] = (byte) 99;
    sourceArray1[15] = (byte) 182;
    sourceArray1[24] = (byte) 213;
    sourceArray1[17] = (byte) 46;
    sourceArray1[46] = (byte) 65;
    sourceArray1[19] = (byte) 249;
    sourceArray1[34] = (byte) 96 /*0x60*/;
    sourceArray1[21] = (byte) 36;
    sourceArray1[8] = (byte) 152;
    sourceArray1[23] = (byte) 123;
    sourceArray1[37] = (byte) 97;
    sourceArray1[6] = (byte) 153;
    sourceArray1[26] = (byte) 144 /*0x90*/;
    sourceArray1[27] = (byte) 246;
    sourceArray1[12] = (byte) 204;
    sourceArray1[18] = (byte) 104;
    sourceArray1[30] = (byte) 92;
    sourceArray1[31 /*0x1F*/] = (byte) 132;
    sourceArray1[32 /*0x20*/] = (byte) 62;
    sourceArray1[33] = (byte) 173;
    sourceArray1[0] = (byte) 88;
    sourceArray1[22] = (byte) 1;
    sourceArray1[36] = (byte) 58;
    sourceArray1[41] = (byte) 188;
    sourceArray1[9] = (byte) 161;
    sourceArray1[39] = (byte) 160 /*0xA0*/;
    sourceArray1[45] = (byte) 77;
    sourceArray1[10] = (byte) 209;
    sourceArray1[42] = (byte) 156;
    sourceArray1[1] = (byte) 57;
    sourceArray1[44] = (byte) 131;
    sourceArray1[13] = (byte) 63 /*0x3F*/;
    sourceArray1[7] = (byte) 226;
    sourceArray1[43] = (byte) 125;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 127 /*0x7F*/,
      (byte) 53,
      (byte) 132,
      (byte) 196,
      (byte) 184,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 35,
      (byte) 72,
      (byte) 249,
      (byte) 34,
      (byte) 180,
      (byte) 120,
      (byte) 80 /*0x50*/,
      (byte) 252,
      (byte) 240 /*0xF0*/,
      (byte) 58,
      (byte) 84,
      (byte) 165,
      (byte) 134,
      (byte) 214,
      (byte) 23,
      (byte) 232,
      (byte) 113,
      (byte) 185,
      (byte) 175,
      (byte) 186,
      (byte) 192 /*0xC0*/,
      (byte) 183,
      (byte) 75,
      (byte) 63 /*0x3F*/,
      (byte) 251,
      (byte) 137,
      (byte) 220,
      (byte) 50,
      (byte) 23,
      (byte) 248,
      (byte) 245,
      (byte) 190,
      (byte) 7,
      (byte) 195,
      (byte) 237,
      (byte) 138,
      (byte) 164,
      (byte) 40,
      (byte) 53,
      (byte) 110,
      byte.MaxValue
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13921(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 34,
      (byte) 168,
      (byte) 235,
      (byte) 121,
      (byte) 47,
      (byte) 253,
      (byte) 143,
      (byte) 19,
      (byte) 13,
      (byte) 172,
      (byte) 179,
      (byte) 106,
      (byte) 171,
      (byte) 107,
      (byte) 196,
      (byte) 250,
      (byte) 247,
      (byte) 218,
      (byte) 235,
      (byte) 30,
      (byte) 50,
      (byte) 112 /*0x70*/,
      (byte) 179,
      (byte) 125,
      (byte) 163,
      (byte) 164,
      (byte) 161,
      (byte) 244,
      (byte) 122,
      (byte) 168,
      (byte) 163,
      (byte) 75,
      (byte) 193,
      (byte) 175,
      (byte) 56,
      (byte) 96 /*0x60*/,
      (byte) 189,
      (byte) 1,
      (byte) 174,
      (byte) 91,
      (byte) 158,
      (byte) 47,
      (byte) 160 /*0xA0*/,
      (byte) 127 /*0x7F*/,
      (byte) 48 /*0x30*/,
      (byte) 109,
      (byte) 130,
      (byte) 247
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 61;
    sourceArray2[1] = (byte) 129;
    sourceArray2[21] = (byte) 251;
    sourceArray2[3] = (byte) 37;
    sourceArray2[20] = (byte) 196;
    sourceArray2[5] = (byte) 45;
    sourceArray2[6] = (byte) 143;
    sourceArray2[26] = (byte) 151;
    sourceArray2[7] = (byte) 175;
    sourceArray2[10] = (byte) 231;
    sourceArray2[32 /*0x20*/] = (byte) 98;
    sourceArray2[11] = (byte) 160 /*0xA0*/;
    sourceArray2[12] = (byte) 151;
    sourceArray2[13] = (byte) 23;
    sourceArray2[14] = (byte) 51;
    sourceArray2[25] = (byte) 202;
    sourceArray2[4] = (byte) 116;
    sourceArray2[17] = (byte) 147;
    sourceArray2[42] = (byte) 156;
    sourceArray2[2] = (byte) 75;
    sourceArray2[27] = (byte) 194;
    sourceArray2[18] = (byte) 124;
    sourceArray2[31 /*0x1F*/] = (byte) 42;
    sourceArray2[36] = (byte) 15;
    sourceArray2[24] = (byte) 128 /*0x80*/;
    sourceArray2[46] = (byte) 139;
    sourceArray2[16 /*0x10*/] = (byte) 253;
    sourceArray2[0] = (byte) 64 /*0x40*/;
    sourceArray2[41] = (byte) 181;
    sourceArray2[29] = (byte) 161;
    sourceArray2[8] = (byte) 70;
    sourceArray2[19] = (byte) 238;
    sourceArray2[39] = (byte) 18;
    sourceArray2[33] = (byte) 196;
    sourceArray2[34] = (byte) 99;
    sourceArray2[35] = (byte) 160 /*0xA0*/;
    sourceArray2[44] = (byte) 162;
    sourceArray2[22] = (byte) 17;
    sourceArray2[9] = (byte) 206;
    sourceArray2[38] = (byte) 222;
    sourceArray2[40] = (byte) 62;
    sourceArray2[30] = (byte) 179;
    sourceArray2[23] = (byte) 203;
    sourceArray2[28] = (byte) 90;
    sourceArray2[15] = (byte) 44;
    sourceArray2[45] = (byte) 215;
    sourceArray2[37] = (byte) 69;
    sourceArray2[47] = (byte) 163;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13922(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[39] = (byte) 188;
    sourceArray1[1] = (byte) 196;
    sourceArray1[34] = (byte) 154;
    sourceArray1[3] = (byte) 53;
    sourceArray1[32 /*0x20*/] = (byte) 132;
    sourceArray1[10] = (byte) 193;
    sourceArray1[6] = (byte) 188;
    sourceArray1[28] = (byte) 124;
    sourceArray1[8] = (byte) 163;
    sourceArray1[9] = (byte) 224 /*0xE0*/;
    sourceArray1[44] = (byte) 169;
    sourceArray1[41] = (byte) 18;
    sourceArray1[24] = (byte) 237;
    sourceArray1[13] = (byte) 105;
    sourceArray1[14] = (byte) 177;
    sourceArray1[45] = (byte) 46;
    sourceArray1[16 /*0x10*/] = (byte) 145;
    sourceArray1[40] = (byte) 67;
    sourceArray1[26] = (byte) 9;
    sourceArray1[18] = (byte) 110;
    sourceArray1[20] = (byte) 254;
    sourceArray1[21] = (byte) 241;
    sourceArray1[22] = (byte) 172;
    sourceArray1[23] = (byte) 249;
    sourceArray1[33] = (byte) 113;
    sourceArray1[25] = (byte) 202;
    sourceArray1[17] = (byte) 14;
    sourceArray1[27] = (byte) 143;
    sourceArray1[7] = (byte) 151;
    sourceArray1[36] = (byte) 59;
    sourceArray1[30] = (byte) 150;
    sourceArray1[31 /*0x1F*/] = (byte) 188;
    sourceArray1[35] = (byte) 229;
    sourceArray1[42] = (byte) 102;
    sourceArray1[15] = (byte) 181;
    sourceArray1[2] = (byte) 169;
    sourceArray1[12] = (byte) 158;
    sourceArray1[37] = (byte) 210;
    sourceArray1[11] = (byte) 32 /*0x20*/;
    sourceArray1[46] = (byte) 141;
    sourceArray1[19] = (byte) 104;
    sourceArray1[29] = (byte) 107;
    sourceArray1[5] = (byte) 221;
    sourceArray1[43] = (byte) 165;
    sourceArray1[38] = (byte) 250;
    sourceArray1[4] = (byte) 91;
    sourceArray1[0] = (byte) 135;
    sourceArray1[47] = (byte) 186;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 202,
      (byte) 32 /*0x20*/,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 194,
      (byte) 85,
      (byte) 216,
      (byte) 237,
      (byte) 46,
      (byte) 100,
      (byte) 141,
      (byte) 4,
      (byte) 13,
      (byte) 188,
      (byte) 248,
      (byte) 168,
      (byte) 209,
      (byte) 26,
      (byte) 138,
      (byte) 185,
      (byte) 233,
      (byte) 128 /*0x80*/,
      (byte) 80 /*0x50*/,
      (byte) 50,
      (byte) 179,
      (byte) 148,
      (byte) 230,
      (byte) 234,
      (byte) 59,
      (byte) 93,
      (byte) 73,
      (byte) 3,
      (byte) 127 /*0x7F*/,
      (byte) 163,
      (byte) 244,
      (byte) 71,
      (byte) 44,
      (byte) 98,
      (byte) 121,
      (byte) 98,
      (byte) 157,
      (byte) 179,
      (byte) 112 /*0x70*/,
      (byte) 169,
      (byte) 195,
      (byte) 146,
      (byte) 85,
      (byte) 92
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13923(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 95,
      (byte) 235,
      (byte) 131,
      (byte) 211,
      (byte) 70,
      (byte) 23,
      (byte) 85,
      (byte) 59,
      (byte) 218,
      (byte) 12,
      (byte) 17,
      (byte) 95,
      (byte) 135,
      (byte) 84,
      (byte) 133,
      (byte) 235,
      (byte) 235,
      (byte) 209,
      (byte) 224 /*0xE0*/,
      (byte) 81,
      (byte) 121,
      (byte) 130,
      (byte) 203,
      (byte) 254,
      (byte) 238,
      (byte) 51,
      (byte) 170,
      (byte) 201,
      (byte) 14,
      (byte) 243,
      (byte) 91,
      (byte) 171,
      (byte) 173,
      (byte) 45,
      (byte) 220,
      (byte) 99,
      (byte) 190,
      (byte) 91,
      (byte) 32 /*0x20*/,
      (byte) 82,
      (byte) 94,
      (byte) 232,
      (byte) 208 /*0xD0*/,
      (byte) 247,
      (byte) 187,
      (byte) 44,
      (byte) 3,
      (byte) 21
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 154;
    sourceArray2[34] = (byte) 133;
    sourceArray2[5] = (byte) 147;
    sourceArray2[3] = (byte) 50;
    sourceArray2[35] = (byte) 51;
    sourceArray2[37] = (byte) 119;
    sourceArray2[27] = (byte) 201;
    sourceArray2[9] = (byte) 8;
    sourceArray2[15] = (byte) 101;
    sourceArray2[0] = (byte) 15;
    sourceArray2[41] = (byte) 165;
    sourceArray2[11] = (byte) 117;
    sourceArray2[19] = (byte) 32 /*0x20*/;
    sourceArray2[13] = (byte) 162;
    sourceArray2[21] = (byte) 100;
    sourceArray2[29] = (byte) 95;
    sourceArray2[10] = (byte) 138;
    sourceArray2[8] = (byte) 206;
    sourceArray2[1] = (byte) 243;
    sourceArray2[36] = (byte) 54;
    sourceArray2[20] = (byte) 221;
    sourceArray2[4] = (byte) 96 /*0x60*/;
    sourceArray2[22] = (byte) 175;
    sourceArray2[23] = (byte) 161;
    sourceArray2[7] = (byte) 202;
    sourceArray2[25] = (byte) 145;
    sourceArray2[26] = (byte) 9;
    sourceArray2[17] = (byte) 124;
    sourceArray2[46] = (byte) 3;
    sourceArray2[28] = (byte) 86;
    sourceArray2[12] = (byte) 164;
    sourceArray2[14] = (byte) 254;
    sourceArray2[32 /*0x20*/] = (byte) 81;
    sourceArray2[33] = (byte) 116;
    sourceArray2[30] = (byte) 21;
    sourceArray2[18] = (byte) 215;
    sourceArray2[42] = (byte) 70;
    sourceArray2[31 /*0x1F*/] = (byte) 78;
    sourceArray2[38] = (byte) 104;
    sourceArray2[39] = (byte) 48 /*0x30*/;
    sourceArray2[40] = (byte) 159;
    sourceArray2[16 /*0x10*/] = (byte) 3;
    sourceArray2[44] = (byte) 244;
    sourceArray2[43] = (byte) 174;
    sourceArray2[2] = (byte) 101;
    sourceArray2[45] = (byte) 177;
    sourceArray2[6] = (byte) 240 /*0xF0*/;
    sourceArray2[47] = (byte) 42;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13924(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 168,
      (byte) 158,
      (byte) 65,
      (byte) 254,
      (byte) 124,
      (byte) 172,
      (byte) 237,
      (byte) 95,
      (byte) 135,
      (byte) 43,
      (byte) 77,
      (byte) 65,
      (byte) 35,
      (byte) 225,
      (byte) 216,
      (byte) 37,
      (byte) 74,
      byte.MaxValue,
      (byte) 179,
      (byte) 53,
      (byte) 86,
      (byte) 68,
      (byte) 120,
      (byte) 223,
      (byte) 243,
      (byte) 25,
      (byte) 163,
      (byte) 168,
      (byte) 33,
      (byte) 213,
      (byte) 200,
      (byte) 140,
      (byte) 238,
      (byte) 70,
      (byte) 136,
      (byte) 246,
      (byte) 37,
      (byte) 227,
      (byte) 106,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 252,
      (byte) 142,
      (byte) 211,
      (byte) 245,
      (byte) 248,
      (byte) 83,
      (byte) 44
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 74,
      (byte) 4,
      (byte) 131,
      (byte) 9,
      (byte) 198,
      (byte) 123,
      (byte) 10,
      (byte) 94,
      (byte) 147,
      (byte) 58,
      (byte) 156,
      (byte) 124,
      (byte) 100,
      (byte) 196,
      (byte) 154,
      (byte) 182,
      (byte) 193,
      (byte) 0,
      (byte) 57,
      (byte) 95,
      (byte) 154,
      (byte) 240 /*0xF0*/,
      (byte) 119,
      (byte) 188,
      (byte) 155,
      (byte) 124,
      (byte) 136,
      (byte) 83,
      (byte) 215,
      (byte) 150,
      (byte) 158,
      (byte) 222,
      (byte) 36,
      (byte) 167,
      (byte) 192 /*0xC0*/,
      (byte) 176 /*0xB0*/,
      (byte) 151,
      (byte) 36,
      (byte) 159,
      (byte) 86,
      (byte) 231,
      (byte) 69,
      (byte) 245,
      (byte) 217,
      (byte) 168,
      (byte) 60,
      (byte) 151,
      (byte) 28
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13925(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 34;
    sourceArray1[0] = (byte) 31 /*0x1F*/;
    sourceArray1[2] = (byte) 100;
    sourceArray1[3] = (byte) 38;
    sourceArray1[31 /*0x1F*/] = (byte) 101;
    sourceArray1[5] = (byte) 11;
    sourceArray1[6] = (byte) 99;
    sourceArray1[7] = (byte) 9;
    sourceArray1[25] = (byte) 233;
    sourceArray1[19] = (byte) 148;
    sourceArray1[30] = (byte) 117;
    sourceArray1[11] = (byte) 99;
    sourceArray1[12] = (byte) 193;
    sourceArray1[36] = (byte) 35;
    sourceArray1[14] = (byte) 220;
    sourceArray1[15] = (byte) 89;
    sourceArray1[41] = (byte) 70;
    sourceArray1[17] = (byte) 0;
    sourceArray1[18] = (byte) 103;
    sourceArray1[26] = (byte) 251;
    sourceArray1[45] = (byte) 112 /*0x70*/;
    sourceArray1[8] = (byte) 68;
    sourceArray1[47] = (byte) 164;
    sourceArray1[1] = (byte) 36;
    sourceArray1[44] = (byte) 220;
    sourceArray1[9] = (byte) 61;
    sourceArray1[40] = (byte) 7;
    sourceArray1[27] = (byte) 197;
    sourceArray1[28] = (byte) 118;
    sourceArray1[46] = (byte) 52;
    sourceArray1[16 /*0x10*/] = (byte) 226;
    sourceArray1[10] = (byte) 177;
    sourceArray1[43] = (byte) 190;
    sourceArray1[33] = (byte) 248;
    sourceArray1[22] = (byte) 90;
    sourceArray1[38] = (byte) 195;
    sourceArray1[34] = (byte) 206;
    sourceArray1[20] = (byte) 98;
    sourceArray1[13] = (byte) 83;
    sourceArray1[39] = (byte) 215;
    sourceArray1[29] = (byte) 156;
    sourceArray1[23] = (byte) 182;
    sourceArray1[42] = byte.MaxValue;
    sourceArray1[37] = (byte) 208 /*0xD0*/;
    sourceArray1[4] = (byte) 98;
    sourceArray1[21] = (byte) 242;
    sourceArray1[32 /*0x20*/] = (byte) 222;
    sourceArray1[24] = (byte) 164;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 174,
      (byte) 187,
      (byte) 72,
      (byte) 71,
      (byte) 119,
      (byte) 101,
      (byte) 121,
      (byte) 43,
      (byte) 144 /*0x90*/,
      (byte) 8,
      (byte) 96 /*0x60*/,
      (byte) 203,
      (byte) 199,
      (byte) 97,
      (byte) 204,
      (byte) 86,
      (byte) 58,
      (byte) 27,
      (byte) 38,
      (byte) 126,
      (byte) 41,
      (byte) 162,
      (byte) 252,
      (byte) 242,
      (byte) 92,
      (byte) 89,
      (byte) 14,
      (byte) 45,
      (byte) 104,
      (byte) 105,
      (byte) 238,
      (byte) 242,
      (byte) 98,
      (byte) 133,
      (byte) 91,
      (byte) 1,
      (byte) 112 /*0x70*/,
      (byte) 138,
      (byte) 126,
      (byte) 9,
      (byte) 151,
      (byte) 182,
      (byte) 104,
      (byte) 8,
      (byte) 191,
      (byte) 236,
      (byte) 78,
      (byte) 252
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13926(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 108,
      (byte) 32 /*0x20*/,
      (byte) 188,
      (byte) 157,
      (byte) 24,
      (byte) 109,
      (byte) 120,
      (byte) 201,
      (byte) 20,
      (byte) 58,
      (byte) 45,
      (byte) 211,
      (byte) 78,
      (byte) 196,
      (byte) 186,
      (byte) 78,
      (byte) 239,
      (byte) 53,
      (byte) 254,
      (byte) 238,
      (byte) 252,
      (byte) 0,
      (byte) 227,
      (byte) 96 /*0x60*/,
      (byte) 191,
      (byte) 194,
      (byte) 243,
      (byte) 200,
      (byte) 34,
      (byte) 104,
      (byte) 155,
      (byte) 167,
      (byte) 72,
      (byte) 71,
      (byte) 72,
      (byte) 54,
      (byte) 146,
      (byte) 5,
      (byte) 89,
      (byte) 91,
      (byte) 228,
      (byte) 190,
      (byte) 178,
      (byte) 102,
      (byte) 44,
      (byte) 162,
      (byte) 0,
      (byte) 134
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 11,
      (byte) 109,
      (byte) 137,
      (byte) 101,
      (byte) 86,
      (byte) 222,
      (byte) 209,
      (byte) 174,
      (byte) 104,
      (byte) 159,
      (byte) 149,
      (byte) 134,
      (byte) 193,
      (byte) 114,
      (byte) 8,
      (byte) 18,
      (byte) 243,
      (byte) 28,
      (byte) 68,
      (byte) 214,
      (byte) 152,
      (byte) 113,
      (byte) 242,
      (byte) 93,
      (byte) 80 /*0x50*/,
      (byte) 228,
      (byte) 56,
      (byte) 119,
      (byte) 26,
      (byte) 137,
      (byte) 6,
      (byte) 101,
      (byte) 154,
      (byte) 87,
      (byte) 120,
      (byte) 211,
      (byte) 41,
      (byte) 132,
      (byte) 48 /*0x30*/,
      (byte) 189,
      (byte) 212,
      (byte) 26,
      (byte) 252,
      (byte) 219,
      (byte) 4,
      (byte) 125,
      (byte) 62
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13927(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 176 /*0xB0*/;
    sourceArray1[19] = (byte) 219;
    sourceArray1[14] = (byte) 6;
    sourceArray1[31 /*0x1F*/] = (byte) 164;
    sourceArray1[2] = (byte) 107;
    sourceArray1[5] = (byte) 222;
    sourceArray1[13] = (byte) 138;
    sourceArray1[7] = (byte) 122;
    sourceArray1[16 /*0x10*/] = (byte) 131;
    sourceArray1[9] = (byte) 166;
    sourceArray1[26] = (byte) 28;
    sourceArray1[11] = (byte) 158;
    sourceArray1[22] = (byte) 62;
    sourceArray1[43] = (byte) 23;
    sourceArray1[46] = (byte) 166;
    sourceArray1[15] = (byte) 123;
    sourceArray1[24] = (byte) 124;
    sourceArray1[17] = (byte) 188;
    sourceArray1[18] = (byte) 233;
    sourceArray1[6] = (byte) 85;
    sourceArray1[30] = (byte) 7;
    sourceArray1[32 /*0x20*/] = (byte) 122;
    sourceArray1[27] = (byte) 193;
    sourceArray1[28] = (byte) 108;
    sourceArray1[23] = (byte) 78;
    sourceArray1[40] = (byte) 4;
    sourceArray1[4] = (byte) 92;
    sourceArray1[0] = (byte) 162;
    sourceArray1[41] = (byte) 225;
    sourceArray1[39] = (byte) 39;
    sourceArray1[20] = (byte) 162;
    sourceArray1[21] = (byte) 10;
    sourceArray1[12] = (byte) 133;
    sourceArray1[33] = (byte) 176 /*0xB0*/;
    sourceArray1[34] = (byte) 166;
    sourceArray1[35] = (byte) 88;
    sourceArray1[3] = (byte) 60;
    sourceArray1[37] = (byte) 249;
    sourceArray1[38] = (byte) 54;
    sourceArray1[25] = (byte) 52;
    sourceArray1[29] = (byte) 187;
    sourceArray1[47] = (byte) 24;
    sourceArray1[42] = (byte) 52;
    sourceArray1[1] = (byte) 238;
    sourceArray1[44] = (byte) 6;
    sourceArray1[45] = (byte) 200;
    sourceArray1[10] = (byte) 172;
    sourceArray1[36] = (byte) 221;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 98,
      (byte) 4,
      (byte) 74,
      (byte) 227,
      (byte) 112 /*0x70*/,
      (byte) 20,
      (byte) 201,
      (byte) 154,
      (byte) 214,
      (byte) 36,
      (byte) 48 /*0x30*/,
      (byte) 178,
      (byte) 60,
      (byte) 9,
      (byte) 72,
      (byte) 155,
      (byte) 41,
      (byte) 29,
      (byte) 57,
      (byte) 221,
      (byte) 80 /*0x50*/,
      (byte) 239,
      (byte) 92,
      (byte) 53,
      (byte) 68,
      (byte) 100,
      (byte) 59,
      (byte) 126,
      (byte) 203,
      (byte) 202,
      (byte) 132,
      (byte) 88,
      (byte) 40,
      (byte) 20,
      (byte) 5,
      (byte) 234,
      (byte) 110,
      (byte) 22,
      (byte) 138,
      (byte) 137,
      (byte) 237,
      (byte) 66,
      (byte) 4,
      (byte) 189,
      (byte) 8,
      (byte) 17,
      (byte) 64 /*0x40*/,
      (byte) 201
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13928(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 208 /*0xD0*/,
      (byte) 52,
      (byte) 77,
      (byte) 107,
      (byte) 254,
      (byte) 217,
      (byte) 24,
      (byte) 110,
      (byte) 146,
      (byte) 193,
      (byte) 137,
      (byte) 212,
      (byte) 12,
      (byte) 153,
      (byte) 190,
      (byte) 205,
      (byte) 171,
      (byte) 194,
      (byte) 246,
      (byte) 63 /*0x3F*/,
      (byte) 239,
      (byte) 121,
      (byte) 212,
      (byte) 4,
      (byte) 222,
      (byte) 82,
      (byte) 119,
      (byte) 93,
      (byte) 103,
      (byte) 238,
      (byte) 151,
      (byte) 173,
      (byte) 74,
      (byte) 15,
      (byte) 189,
      (byte) 207,
      (byte) 121,
      (byte) 247,
      (byte) 50,
      (byte) 206,
      (byte) 177,
      (byte) 197,
      (byte) 202,
      (byte) 78,
      (byte) 30,
      (byte) 235,
      (byte) 43,
      (byte) 117
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[0] = (byte) 62;
    sourceArray2[33] = (byte) 234;
    sourceArray2[39] = (byte) 197;
    sourceArray2[3] = (byte) 226;
    sourceArray2[31 /*0x1F*/] = (byte) 68;
    sourceArray2[29] = (byte) 10;
    sourceArray2[7] = (byte) 95;
    sourceArray2[21] = (byte) 193;
    sourceArray2[8] = (byte) 164;
    sourceArray2[9] = (byte) 202;
    sourceArray2[42] = (byte) 71;
    sourceArray2[11] = (byte) 213;
    sourceArray2[12] = (byte) 224 /*0xE0*/;
    sourceArray2[13] = (byte) 74;
    sourceArray2[26] = (byte) 220;
    sourceArray2[15] = (byte) 2;
    sourceArray2[30] = (byte) 236;
    sourceArray2[17] = (byte) 49;
    sourceArray2[18] = (byte) 42;
    sourceArray2[19] = (byte) 207;
    sourceArray2[20] = (byte) 196;
    sourceArray2[22] = (byte) 111;
    sourceArray2[27] = (byte) 119;
    sourceArray2[40] = (byte) 111;
    sourceArray2[24] = (byte) 66;
    sourceArray2[36] = (byte) 37;
    sourceArray2[41] = (byte) 97;
    sourceArray2[28] = (byte) 125;
    sourceArray2[1] = (byte) 137;
    sourceArray2[6] = (byte) 87;
    sourceArray2[4] = (byte) 212;
    sourceArray2[45] = (byte) 188;
    sourceArray2[32 /*0x20*/] = (byte) 63 /*0x3F*/;
    sourceArray2[43] = (byte) 211;
    sourceArray2[34] = (byte) 216;
    sourceArray2[35] = (byte) 210;
    sourceArray2[14] = (byte) 153;
    sourceArray2[37] = (byte) 45;
    sourceArray2[38] = (byte) 222;
    sourceArray2[46] = (byte) 208 /*0xD0*/;
    sourceArray2[44] = (byte) 230;
    sourceArray2[23] = (byte) 168;
    sourceArray2[2] = (byte) 99;
    sourceArray2[16 /*0x10*/] = (byte) 7;
    sourceArray2[25] = (byte) 51;
    sourceArray2[5] = (byte) 106;
    sourceArray2[10] = (byte) 229;
    sourceArray2[47] = (byte) 158;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13916.sspq, 23, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13916.sspr, 23, (Array) numArray2, 0, 42);
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

  internal static int ssp_appserver_13929(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 189,
      (byte) 152,
      (byte) 207,
      (byte) 209,
      byte.MaxValue,
      (byte) 12,
      (byte) 242,
      (byte) 124,
      (byte) 73,
      (byte) 20,
      (byte) 146,
      (byte) 98,
      (byte) 139,
      (byte) 36,
      (byte) 58,
      (byte) 45,
      (byte) 42,
      (byte) 226,
      (byte) 158,
      (byte) 153,
      (byte) 31 /*0x1F*/,
      (byte) 32 /*0x20*/,
      (byte) 117,
      (byte) 190,
      (byte) 5,
      (byte) 141,
      (byte) 248,
      (byte) 5,
      (byte) 204,
      (byte) 138,
      (byte) 225,
      (byte) 221,
      (byte) 163,
      (byte) 209,
      (byte) 183,
      (byte) 229,
      (byte) 82,
      (byte) 119,
      (byte) 10,
      (byte) 174,
      (byte) 165,
      (byte) 238,
      (byte) 62,
      (byte) 78,
      (byte) 16 /*0x10*/,
      (byte) 49,
      (byte) 108,
      (byte) 67
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 55,
      (byte) 66,
      (byte) 19,
      (byte) 237,
      (byte) 60,
      (byte) 35,
      (byte) 63 /*0x3F*/,
      (byte) 10,
      (byte) 120,
      (byte) 230,
      (byte) 241,
      (byte) 124,
      (byte) 85,
      (byte) 144 /*0x90*/,
      (byte) 45,
      (byte) 70,
      (byte) 203,
      (byte) 158,
      (byte) 93,
      (byte) 108,
      (byte) 218,
      (byte) 203,
      (byte) 49,
      (byte) 93,
      (byte) 88,
      (byte) 122,
      (byte) 21,
      (byte) 100,
      (byte) 206,
      (byte) 89,
      (byte) 0,
      (byte) 117,
      (byte) 37,
      (byte) 65,
      (byte) 191,
      (byte) 235,
      (byte) 145,
      (byte) 194,
      (byte) 214,
      (byte) 238,
      (byte) 39,
      (byte) 118,
      (byte) 115,
      (byte) 204,
      (byte) 112 /*0x70*/,
      (byte) 46,
      (byte) 86
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[18];
    byte[] response2 = new byte[18];
    Array.Copy((Array) sc_13916.sspq, 65, (Array) numArray2, 0, 18);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13916.sspr, 65, (Array) numArray2, 0, 18);
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

  internal static int ssp_appserver_13930(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 216,
      (byte) 239,
      (byte) 244,
      (byte) 48 /*0x30*/,
      (byte) 225,
      (byte) 39,
      (byte) 173,
      (byte) 212,
      (byte) 33,
      (byte) 180,
      (byte) 64 /*0x40*/,
      (byte) 131,
      (byte) 97,
      (byte) 150,
      (byte) 169,
      (byte) 225,
      (byte) 31 /*0x1F*/,
      (byte) 25,
      (byte) 195,
      (byte) 65,
      (byte) 171,
      (byte) 152,
      (byte) 154,
      (byte) 184,
      (byte) 61,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 91,
      (byte) 114,
      (byte) 178,
      (byte) 214,
      (byte) 112 /*0x70*/,
      (byte) 110,
      (byte) 118,
      (byte) 114,
      (byte) 68,
      (byte) 205,
      (byte) 128 /*0x80*/,
      (byte) 188,
      (byte) 251,
      (byte) 99,
      (byte) 247,
      (byte) 80 /*0x50*/,
      (byte) 95,
      (byte) 172,
      (byte) 45,
      (byte) 94,
      (byte) 156
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 167,
      (byte) 1,
      (byte) 1,
      (byte) 36,
      (byte) 241,
      (byte) 9,
      (byte) 159,
      (byte) 38,
      (byte) 64 /*0x40*/,
      (byte) 25,
      (byte) 136,
      (byte) 196,
      (byte) 117,
      (byte) 22,
      (byte) 134,
      (byte) 195,
      (byte) 80 /*0x50*/,
      (byte) 212,
      (byte) 242,
      (byte) 249,
      (byte) 172,
      (byte) 229,
      (byte) 178,
      (byte) 229,
      (byte) 120,
      (byte) 65,
      (byte) 79,
      (byte) 16 /*0x10*/,
      (byte) 16 /*0x10*/,
      (byte) 236,
      (byte) 47,
      (byte) 113,
      (byte) 157,
      (byte) 163,
      (byte) 42,
      (byte) 72,
      (byte) 238,
      (byte) 221,
      (byte) 199,
      (byte) 145,
      (byte) 99,
      (byte) 164,
      (byte) 174,
      (byte) 213,
      (byte) 108,
      (byte) 139,
      (byte) 172
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13931(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 152,
      (byte) 6,
      (byte) 64 /*0x40*/,
      (byte) 84,
      (byte) 59,
      (byte) 243,
      (byte) 98,
      (byte) 12,
      (byte) 82,
      (byte) 98,
      (byte) 110,
      (byte) 221,
      (byte) 208 /*0xD0*/,
      (byte) 21,
      (byte) 234,
      (byte) 35,
      (byte) 101,
      (byte) 19,
      (byte) 159,
      (byte) 93,
      (byte) 199,
      (byte) 222,
      (byte) 179,
      (byte) 71,
      (byte) 58,
      (byte) 11,
      (byte) 16 /*0x10*/,
      (byte) 33,
      (byte) 126,
      (byte) 79,
      (byte) 149,
      (byte) 24,
      (byte) 130,
      (byte) 222,
      (byte) 213,
      (byte) 111,
      (byte) 49,
      (byte) 239,
      (byte) 146,
      (byte) 141,
      (byte) 90,
      (byte) 239,
      (byte) 16 /*0x10*/,
      (byte) 149,
      (byte) 226,
      (byte) 130,
      (byte) 219,
      (byte) 23
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 77,
      (byte) 90,
      (byte) 72,
      (byte) 81,
      (byte) 243,
      (byte) 7,
      (byte) 74,
      (byte) 239,
      (byte) 205,
      (byte) 112 /*0x70*/,
      (byte) 43,
      (byte) 47,
      (byte) 179,
      (byte) 77,
      (byte) 43,
      (byte) 54,
      (byte) 128 /*0x80*/,
      (byte) 1,
      (byte) 227,
      (byte) 54,
      (byte) 195,
      (byte) 238,
      (byte) 14,
      (byte) 228,
      (byte) 217,
      (byte) 65,
      (byte) 1,
      (byte) 154,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 97,
      (byte) 222,
      (byte) 32 /*0x20*/,
      (byte) 149,
      (byte) 184,
      (byte) 116,
      (byte) 144 /*0x90*/,
      (byte) 21,
      (byte) 137,
      (byte) 150,
      (byte) 148,
      (byte) 59,
      (byte) 12,
      (byte) 35,
      (byte) 47,
      (byte) 177,
      (byte) 57,
      (byte) 26
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13932(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 5;
    sourceArray1[40] = (byte) 98;
    sourceArray1[43] = (byte) 97;
    sourceArray1[16 /*0x10*/] = (byte) 123;
    sourceArray1[0] = (byte) 246;
    sourceArray1[5] = (byte) 26;
    sourceArray1[2] = (byte) 186;
    sourceArray1[14] = (byte) 241;
    sourceArray1[8] = (byte) 65;
    sourceArray1[9] = (byte) 141;
    sourceArray1[18] = (byte) 118;
    sourceArray1[26] = (byte) 184;
    sourceArray1[12] = (byte) 86;
    sourceArray1[3] = (byte) 82;
    sourceArray1[23] = (byte) 205;
    sourceArray1[15] = (byte) 63 /*0x3F*/;
    sourceArray1[20] = (byte) 17;
    sourceArray1[17] = (byte) 241;
    sourceArray1[42] = (byte) 25;
    sourceArray1[19] = (byte) 129;
    sourceArray1[22] = (byte) 216;
    sourceArray1[21] = (byte) 3;
    sourceArray1[38] = (byte) 56;
    sourceArray1[27] = (byte) 48 /*0x30*/;
    sourceArray1[24] = (byte) 30;
    sourceArray1[25] = (byte) 101;
    sourceArray1[31 /*0x1F*/] = (byte) 169;
    sourceArray1[6] = (byte) 47;
    sourceArray1[28] = (byte) 248;
    sourceArray1[29] = (byte) 173;
    sourceArray1[11] = (byte) 205;
    sourceArray1[47] = (byte) 39;
    sourceArray1[32 /*0x20*/] = (byte) 203;
    sourceArray1[10] = (byte) 2;
    sourceArray1[34] = (byte) 140;
    sourceArray1[35] = (byte) 21;
    sourceArray1[36] = (byte) 27;
    sourceArray1[37] = (byte) 28;
    sourceArray1[1] = (byte) 201;
    sourceArray1[13] = (byte) 90;
    sourceArray1[33] = (byte) 29;
    sourceArray1[41] = (byte) 170;
    sourceArray1[7] = (byte) 226;
    sourceArray1[45] = (byte) 200;
    sourceArray1[44] = (byte) 13;
    sourceArray1[4] = (byte) 249;
    sourceArray1[46] = (byte) 140;
    sourceArray1[39] = (byte) 212;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 187,
      (byte) 203,
      (byte) 40,
      (byte) 2,
      (byte) 221,
      (byte) 5,
      (byte) 140,
      (byte) 171,
      (byte) 207,
      (byte) 96 /*0x60*/,
      (byte) 227,
      (byte) 4,
      (byte) 218,
      (byte) 252,
      (byte) 59,
      (byte) 0,
      (byte) 64 /*0x40*/,
      (byte) 189,
      (byte) 218,
      (byte) 39,
      (byte) 81,
      (byte) 177,
      (byte) 147,
      (byte) 173,
      (byte) 124,
      (byte) 56,
      (byte) 197,
      (byte) 241,
      (byte) 191,
      (byte) 230,
      (byte) 104,
      (byte) 221,
      (byte) 219,
      (byte) 153,
      (byte) 198,
      (byte) 190,
      (byte) 224 /*0xE0*/,
      (byte) 0,
      (byte) 37,
      (byte) 17,
      (byte) 26,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 84,
      (byte) 186,
      (byte) 247,
      (byte) 212
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13933(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 83,
      (byte) 89,
      (byte) 13,
      (byte) 178,
      (byte) 154,
      (byte) 205,
      (byte) 141,
      (byte) 41,
      (byte) 129,
      (byte) 8,
      (byte) 120,
      (byte) 44,
      (byte) 222,
      (byte) 187,
      (byte) 208 /*0xD0*/,
      (byte) 185,
      (byte) 200,
      (byte) 222,
      (byte) 10,
      (byte) 222,
      (byte) 127 /*0x7F*/,
      (byte) 4,
      (byte) 52,
      (byte) 183,
      (byte) 246,
      (byte) 117,
      (byte) 20,
      (byte) 46,
      (byte) 188,
      (byte) 167,
      (byte) 100,
      (byte) 159,
      (byte) 11,
      (byte) 143,
      (byte) 133,
      (byte) 56,
      (byte) 56,
      (byte) 110,
      (byte) 26,
      (byte) 176 /*0xB0*/,
      (byte) 237,
      (byte) 89,
      (byte) 178,
      (byte) 145,
      (byte) 172,
      (byte) 1,
      (byte) 10,
      (byte) 233
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 169,
      (byte) 238,
      (byte) 237,
      (byte) 79,
      (byte) 8,
      (byte) 16 /*0x10*/,
      (byte) 245,
      (byte) 176 /*0xB0*/,
      (byte) 125,
      (byte) 198,
      (byte) 127 /*0x7F*/,
      (byte) 249,
      (byte) 0,
      (byte) 128 /*0x80*/,
      (byte) 47,
      (byte) 151,
      (byte) 106,
      (byte) 207,
      (byte) 127 /*0x7F*/,
      (byte) 113,
      (byte) 138,
      (byte) 169,
      (byte) 109,
      (byte) 216,
      (byte) 34,
      (byte) 10,
      (byte) 92,
      (byte) 7,
      (byte) 0,
      (byte) 5,
      (byte) 173,
      (byte) 36,
      (byte) 20,
      (byte) 205,
      (byte) 128 /*0x80*/,
      (byte) 172,
      (byte) 31 /*0x1F*/,
      (byte) 58,
      (byte) 126,
      (byte) 114,
      (byte) 183,
      (byte) 19,
      (byte) 188,
      (byte) 141,
      (byte) 173,
      (byte) 180,
      (byte) 97,
      (byte) 79
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13934(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 107;
    sourceArray1[2] = (byte) 78;
    sourceArray1[4] = (byte) 86;
    sourceArray1[47] = (byte) 224 /*0xE0*/;
    sourceArray1[14] = (byte) 78;
    sourceArray1[25] = (byte) 10;
    sourceArray1[6] = (byte) 36;
    sourceArray1[7] = (byte) 185;
    sourceArray1[18] = (byte) 90;
    sourceArray1[23] = (byte) 82;
    sourceArray1[12] = (byte) 217;
    sourceArray1[9] = (byte) 108;
    sourceArray1[1] = (byte) 71;
    sourceArray1[13] = (byte) 33;
    sourceArray1[45] = (byte) 237;
    sourceArray1[0] = (byte) 214;
    sourceArray1[16 /*0x10*/] = (byte) 151;
    sourceArray1[24] = (byte) 162;
    sourceArray1[38] = (byte) 226;
    sourceArray1[19] = (byte) 195;
    sourceArray1[10] = (byte) 218;
    sourceArray1[21] = (byte) 240 /*0xF0*/;
    sourceArray1[22] = (byte) 38;
    sourceArray1[39] = (byte) 178;
    sourceArray1[5] = (byte) 188;
    sourceArray1[31 /*0x1F*/] = (byte) 175;
    sourceArray1[43] = (byte) 236;
    sourceArray1[34] = (byte) 52;
    sourceArray1[28] = (byte) 43;
    sourceArray1[29] = (byte) 138;
    sourceArray1[30] = (byte) 19;
    sourceArray1[26] = (byte) 244;
    sourceArray1[32 /*0x20*/] = (byte) 189;
    sourceArray1[3] = (byte) 37;
    sourceArray1[11] = (byte) 163;
    sourceArray1[35] = (byte) 72;
    sourceArray1[36] = (byte) 54;
    sourceArray1[37] = (byte) 19;
    sourceArray1[15] = (byte) 53;
    sourceArray1[8] = (byte) 95;
    sourceArray1[20] = (byte) 90;
    sourceArray1[17] = (byte) 128 /*0x80*/;
    sourceArray1[42] = (byte) 142;
    sourceArray1[44] = (byte) 216;
    sourceArray1[41] = (byte) 246;
    sourceArray1[33] = (byte) 209;
    sourceArray1[46] = (byte) 210;
    sourceArray1[40] = (byte) 245;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 81,
      (byte) 25,
      (byte) 177,
      (byte) 168,
      (byte) 90,
      (byte) 235,
      (byte) 218,
      (byte) 189,
      (byte) 224 /*0xE0*/,
      (byte) 187,
      (byte) 169,
      (byte) 170,
      (byte) 167,
      (byte) 112 /*0x70*/,
      (byte) 31 /*0x1F*/,
      (byte) 166,
      (byte) 83,
      (byte) 176 /*0xB0*/,
      (byte) 46,
      (byte) 187,
      (byte) 78,
      (byte) 47,
      (byte) 250,
      (byte) 146,
      (byte) 207,
      (byte) 43,
      (byte) 117,
      (byte) 244,
      (byte) 167,
      (byte) 3,
      (byte) 14,
      (byte) 84,
      (byte) 70,
      (byte) 117,
      (byte) 101,
      (byte) 121,
      (byte) 2,
      (byte) 157,
      (byte) 61,
      (byte) 31 /*0x1F*/,
      (byte) 97,
      (byte) 109,
      (byte) 134,
      (byte) 200,
      (byte) 77,
      (byte) 36,
      (byte) 6,
      (byte) 195
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13935(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[14] = (byte) 179;
    sourceArray1[5] = (byte) 139;
    sourceArray1[22] = (byte) 158;
    sourceArray1[3] = (byte) 180;
    sourceArray1[41] = (byte) 197;
    sourceArray1[2] = (byte) 181;
    sourceArray1[6] = (byte) 245;
    sourceArray1[37] = (byte) 195;
    sourceArray1[8] = (byte) 15;
    sourceArray1[9] = (byte) 132;
    sourceArray1[10] = (byte) 231;
    sourceArray1[11] = (byte) 130;
    sourceArray1[47] = (byte) 130;
    sourceArray1[13] = (byte) 19;
    sourceArray1[23] = (byte) 223;
    sourceArray1[28] = (byte) 146;
    sourceArray1[32 /*0x20*/] = (byte) 203;
    sourceArray1[4] = (byte) 135;
    sourceArray1[1] = (byte) 242;
    sourceArray1[19] = (byte) 24;
    sourceArray1[20] = (byte) 11;
    sourceArray1[36] = (byte) 250;
    sourceArray1[26] = (byte) 7;
    sourceArray1[24] = (byte) 196;
    sourceArray1[18] = (byte) 110;
    sourceArray1[25] = (byte) 243;
    sourceArray1[40] = (byte) 28;
    sourceArray1[27] = (byte) 189;
    sourceArray1[44] = (byte) 63 /*0x3F*/;
    sourceArray1[29] = (byte) 250;
    sourceArray1[30] = (byte) 167;
    sourceArray1[31 /*0x1F*/] = (byte) 165;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[33] = (byte) 174;
    sourceArray1[15] = (byte) 10;
    sourceArray1[35] = (byte) 33;
    sourceArray1[34] = (byte) 108;
    sourceArray1[12] = (byte) 204;
    sourceArray1[38] = (byte) 247;
    sourceArray1[7] = (byte) 21;
    sourceArray1[21] = (byte) 169;
    sourceArray1[17] = (byte) 85;
    sourceArray1[39] = (byte) 95;
    sourceArray1[43] = (byte) 202;
    sourceArray1[42] = (byte) 79;
    sourceArray1[45] = (byte) 184;
    sourceArray1[46] = (byte) 196;
    sourceArray1[0] = (byte) 203;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 57,
      (byte) 117,
      (byte) 164,
      (byte) 234,
      (byte) 71,
      (byte) 78,
      (byte) 154,
      (byte) 20,
      (byte) 81,
      (byte) 94,
      (byte) 38,
      (byte) 59,
      (byte) 115,
      (byte) 139,
      (byte) 161,
      (byte) 240 /*0xF0*/,
      (byte) 66,
      (byte) 5,
      (byte) 241,
      (byte) 24,
      (byte) 120,
      (byte) 183,
      (byte) 95,
      (byte) 160 /*0xA0*/,
      (byte) 247,
      (byte) 110,
      (byte) 26,
      (byte) 44,
      (byte) 246,
      (byte) 118,
      (byte) 223,
      (byte) 67,
      (byte) 167,
      (byte) 147,
      (byte) 19,
      (byte) 93,
      (byte) 6,
      (byte) 133,
      (byte) 200,
      (byte) 210,
      (byte) 97,
      (byte) 14,
      (byte) 47,
      (byte) 194,
      (byte) 143,
      (byte) 195,
      (byte) 15,
      (byte) 213
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[48 /*0x30*/];
    byte[] response2 = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_13916.sspq, 83, (Array) numArray2, 0, 48 /*0x30*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13916.sspr, 83, (Array) numArray2, 0, 48 /*0x30*/);
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

  internal static int ssp_appserver_13936(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 52,
      (byte) 32 /*0x20*/,
      (byte) 203,
      (byte) 84,
      (byte) 60,
      (byte) 129,
      (byte) 58,
      (byte) 126,
      (byte) 225,
      (byte) 52,
      (byte) 132,
      (byte) 12,
      (byte) 226,
      (byte) 130,
      (byte) 177,
      (byte) 168,
      (byte) 152,
      (byte) 149,
      (byte) 218,
      (byte) 219,
      (byte) 87,
      (byte) 206,
      (byte) 226,
      (byte) 203,
      (byte) 94,
      (byte) 86,
      (byte) 227,
      (byte) 109,
      (byte) 217,
      (byte) 117,
      byte.MaxValue,
      (byte) 143,
      (byte) 251,
      (byte) 88,
      (byte) 227,
      (byte) 227,
      (byte) 84,
      (byte) 146,
      (byte) 100,
      (byte) 136,
      (byte) 78,
      (byte) 41,
      (byte) 0,
      (byte) 11,
      (byte) 30,
      (byte) 46,
      (byte) 136,
      (byte) 25
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 16 /*0x10*/;
    sourceArray2[30] = (byte) 192 /*0xC0*/;
    sourceArray2[2] = (byte) 28;
    sourceArray2[3] = (byte) 111;
    sourceArray2[25] = (byte) 158;
    sourceArray2[5] = (byte) 203;
    sourceArray2[44] = (byte) 203;
    sourceArray2[7] = (byte) 14;
    sourceArray2[8] = (byte) 180;
    sourceArray2[9] = (byte) 209;
    sourceArray2[47] = (byte) 140;
    sourceArray2[11] = (byte) 41;
    sourceArray2[12] = (byte) 225;
    sourceArray2[42] = (byte) 205;
    sourceArray2[14] = (byte) 67;
    sourceArray2[15] = (byte) 166;
    sourceArray2[13] = (byte) 150;
    sourceArray2[17] = (byte) 31 /*0x1F*/;
    sourceArray2[6] = (byte) 96 /*0x60*/;
    sourceArray2[24] = (byte) 119;
    sourceArray2[18] = byte.MaxValue;
    sourceArray2[21] = (byte) 132;
    sourceArray2[22] = (byte) 91;
    sourceArray2[38] = (byte) 44;
    sourceArray2[10] = (byte) 234;
    sourceArray2[23] = (byte) 100;
    sourceArray2[36] = (byte) 80 /*0x50*/;
    sourceArray2[29] = (byte) 145;
    sourceArray2[27] = (byte) 29;
    sourceArray2[1] = (byte) 94;
    sourceArray2[41] = (byte) 226;
    sourceArray2[45] = (byte) 54;
    sourceArray2[0] = (byte) 19;
    sourceArray2[33] = (byte) 91;
    sourceArray2[4] = (byte) 75;
    sourceArray2[26] = (byte) 88;
    sourceArray2[19] = (byte) 13;
    sourceArray2[31 /*0x1F*/] = (byte) 212;
    sourceArray2[35] = (byte) 6;
    sourceArray2[39] = (byte) 74;
    sourceArray2[40] = (byte) 49;
    sourceArray2[16 /*0x10*/] = (byte) 104;
    sourceArray2[20] = (byte) 237;
    sourceArray2[43] = (byte) 204;
    sourceArray2[37] = (byte) 74;
    sourceArray2[32 /*0x20*/] = (byte) 110;
    sourceArray2[34] = (byte) 169;
    sourceArray2[46] = (byte) 111;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13937(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      byte.MaxValue,
      (byte) 122,
      (byte) 126,
      (byte) 135,
      (byte) 175,
      (byte) 38,
      (byte) 195,
      (byte) 149,
      (byte) 86,
      (byte) 151,
      (byte) 103,
      (byte) 83,
      (byte) 198,
      (byte) 25,
      (byte) 212,
      (byte) 171,
      (byte) 166,
      (byte) 171,
      (byte) 140,
      (byte) 123,
      (byte) 56,
      (byte) 181,
      (byte) 33,
      (byte) 41,
      byte.MaxValue,
      (byte) 105,
      (byte) 178,
      (byte) 102,
      (byte) 11,
      (byte) 169,
      (byte) 56,
      (byte) 0,
      (byte) 130,
      (byte) 18,
      (byte) 206,
      (byte) 79,
      (byte) 163,
      (byte) 119,
      (byte) 17,
      (byte) 129,
      (byte) 183,
      (byte) 67,
      (byte) 126,
      (byte) 230,
      (byte) 60,
      (byte) 0,
      (byte) 214,
      (byte) 100
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 215;
    sourceArray2[1] = (byte) 167;
    sourceArray2[2] = (byte) 249;
    sourceArray2[3] = (byte) 239;
    sourceArray2[5] = (byte) 161;
    sourceArray2[11] = (byte) 126;
    sourceArray2[6] = (byte) 155;
    sourceArray2[7] = (byte) 221;
    sourceArray2[8] = (byte) 221;
    sourceArray2[9] = (byte) 155;
    sourceArray2[15] = (byte) 207;
    sourceArray2[39] = (byte) 175;
    sourceArray2[0] = (byte) 221;
    sourceArray2[25] = (byte) 253;
    sourceArray2[45] = (byte) 30;
    sourceArray2[24] = (byte) 82;
    sourceArray2[31 /*0x1F*/] = (byte) 174;
    sourceArray2[17] = (byte) 181;
    sourceArray2[37] = (byte) 203;
    sourceArray2[33] = (byte) 2;
    sourceArray2[13] = (byte) 142;
    sourceArray2[21] = (byte) 178;
    sourceArray2[22] = (byte) 166;
    sourceArray2[28] = (byte) 80 /*0x50*/;
    sourceArray2[27] = (byte) 136;
    sourceArray2[10] = (byte) 111;
    sourceArray2[26] = (byte) 202;
    sourceArray2[47] = (byte) 186;
    sourceArray2[18] = (byte) 204;
    sourceArray2[29] = (byte) 198;
    sourceArray2[30] = (byte) 231;
    sourceArray2[12] = (byte) 96 /*0x60*/;
    sourceArray2[32 /*0x20*/] = (byte) 193;
    sourceArray2[20] = (byte) 10;
    sourceArray2[34] = (byte) 157;
    sourceArray2[35] = (byte) 48 /*0x30*/;
    sourceArray2[36] = (byte) 166;
    sourceArray2[38] = (byte) 15;
    sourceArray2[43] = (byte) 71;
    sourceArray2[23] = (byte) 243;
    sourceArray2[40] = (byte) 38;
    sourceArray2[41] = (byte) 243;
    sourceArray2[42] = (byte) 57;
    sourceArray2[14] = (byte) 120;
    sourceArray2[44] = (byte) 68;
    sourceArray2[19] = (byte) 149;
    sourceArray2[46] = (byte) 171;
    sourceArray2[4] = (byte) 199;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
