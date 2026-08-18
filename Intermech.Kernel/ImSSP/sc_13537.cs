// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13537
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13537
{
  private static byte[] sspq = new byte[91]
  {
    (byte) 7,
    (byte) 36,
    (byte) 53,
    (byte) 125,
    (byte) 232,
    (byte) 230,
    (byte) 184,
    (byte) 193,
    (byte) 108,
    (byte) 3,
    (byte) 197,
    (byte) 9,
    (byte) 52,
    (byte) 169,
    (byte) 185,
    (byte) 61,
    (byte) 173,
    (byte) 103,
    (byte) 107,
    (byte) 190,
    (byte) 40,
    (byte) 37,
    (byte) 164,
    (byte) 61,
    (byte) 104,
    (byte) 197,
    (byte) 200,
    (byte) 226,
    (byte) 93,
    (byte) 220,
    (byte) 223,
    (byte) 177,
    (byte) 52,
    (byte) 126,
    (byte) 120,
    (byte) 195,
    (byte) 91,
    (byte) 104,
    (byte) 137,
    (byte) 254,
    (byte) 68,
    (byte) 214,
    (byte) 223,
    (byte) 230,
    (byte) 197,
    (byte) 168,
    (byte) 82,
    (byte) 250,
    (byte) 73,
    (byte) 68,
    (byte) 126,
    (byte) 229,
    (byte) 119,
    (byte) 76,
    (byte) 127 /*0x7F*/,
    (byte) 88,
    (byte) 216,
    (byte) 73,
    (byte) 220,
    (byte) 9,
    (byte) 102,
    (byte) 202,
    (byte) 49,
    (byte) 222,
    (byte) 217,
    (byte) 65,
    (byte) 125,
    (byte) 235,
    (byte) 182,
    (byte) 16 /*0x10*/,
    (byte) 11,
    (byte) 208 /*0xD0*/,
    (byte) 185,
    (byte) 93,
    (byte) 191,
    (byte) 189,
    (byte) 201,
    (byte) 226,
    (byte) 2,
    (byte) 22,
    (byte) 226,
    (byte) 51,
    (byte) 253,
    (byte) 228,
    (byte) 4,
    (byte) 72,
    (byte) 99,
    (byte) 228,
    (byte) 145,
    (byte) 94,
    (byte) 177
  };
  private static byte[] sspr = new byte[91]
  {
    (byte) 140,
    (byte) 2,
    (byte) 60,
    (byte) 145,
    (byte) 159,
    (byte) 178,
    (byte) 187,
    (byte) 151,
    (byte) 135,
    (byte) 29,
    (byte) 240 /*0xF0*/,
    (byte) 143,
    (byte) 23,
    (byte) 156,
    (byte) 149,
    (byte) 254,
    (byte) 149,
    (byte) 15,
    (byte) 224 /*0xE0*/,
    (byte) 165,
    (byte) 2,
    (byte) 129,
    (byte) 247,
    (byte) 107,
    (byte) 44,
    (byte) 237,
    (byte) 112 /*0x70*/,
    (byte) 155,
    (byte) 242,
    (byte) 209,
    (byte) 43,
    byte.MaxValue,
    (byte) 180,
    (byte) 220,
    (byte) 190,
    (byte) 91,
    (byte) 42,
    (byte) 246,
    (byte) 5,
    (byte) 124,
    (byte) 112 /*0x70*/,
    (byte) 240 /*0xF0*/,
    (byte) 243,
    (byte) 79,
    (byte) 21,
    (byte) 111,
    (byte) 1,
    (byte) 228,
    (byte) 26,
    (byte) 128 /*0x80*/,
    (byte) 77,
    (byte) 29,
    (byte) 75,
    (byte) 180,
    (byte) 194,
    (byte) 93,
    (byte) 26,
    (byte) 139,
    (byte) 88,
    (byte) 26,
    (byte) 161,
    (byte) 216,
    (byte) 189,
    (byte) 156,
    (byte) 188,
    (byte) 25,
    (byte) 206,
    (byte) 117,
    (byte) 232,
    (byte) 203,
    (byte) 227,
    (byte) 59,
    (byte) 63 /*0x3F*/,
    (byte) 116,
    (byte) 113,
    (byte) 127 /*0x7F*/,
    (byte) 122,
    (byte) 18,
    (byte) 91,
    (byte) 223,
    (byte) 47,
    (byte) 108,
    (byte) 80 /*0x50*/,
    (byte) 192 /*0xC0*/,
    (byte) 72,
    (byte) 224 /*0xE0*/,
    (byte) 191,
    (byte) 56,
    (byte) 150,
    (byte) 1,
    (byte) 232
  };

  internal static string ssp_appserver_13538()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/]
      {
        (byte) 148,
        (byte) 31 /*0x1F*/,
        (byte) 46,
        (byte) 65,
        (byte) 156,
        (byte) 134,
        (byte) 21,
        (byte) 101,
        (byte) 229,
        (byte) 79,
        (byte) 199,
        (byte) 80 /*0x50*/,
        (byte) 233,
        (byte) 228,
        (byte) 66,
        (byte) 168,
        (byte) 153,
        (byte) 229,
        (byte) 78,
        (byte) 52,
        (byte) 219,
        (byte) 24,
        (byte) 139,
        (byte) 227,
        (byte) 161,
        (byte) 109,
        (byte) 30,
        (byte) 200,
        (byte) 4,
        (byte) 199,
        (byte) 194,
        (byte) 132,
        (byte) 235,
        (byte) 189,
        (byte) 10,
        (byte) 163,
        (byte) 189,
        (byte) 194,
        (byte) 20,
        (byte) 234,
        (byte) 228,
        (byte) 134,
        (byte) 98,
        (byte) 52,
        (byte) 159,
        (byte) 108,
        (byte) 81,
        (byte) 15
      };
      byte[] numArray3 = new byte[48 /*0x30*/];
      numArray3[35] = (byte) 225;
      numArray3[22] = (byte) 107;
      numArray3[2] = (byte) 141;
      numArray3[3] = (byte) 125;
      numArray3[38] = (byte) 201;
      numArray3[26] = (byte) 193;
      numArray3[32 /*0x20*/] = (byte) 187;
      numArray3[27] = (byte) 136;
      numArray3[45] = (byte) 24;
      numArray3[9] = (byte) 4;
      numArray3[10] = (byte) 225;
      numArray3[37] = (byte) 73;
      numArray3[30] = (byte) 124;
      numArray3[5] = (byte) 93;
      numArray3[14] = (byte) 161;
      numArray3[15] = (byte) 188;
      numArray3[16 /*0x10*/] = (byte) 109;
      numArray3[29] = (byte) 32 /*0x20*/;
      numArray3[18] = (byte) 72;
      numArray3[17] = (byte) 212;
      numArray3[20] = (byte) 207;
      numArray3[13] = (byte) 126;
      numArray3[6] = (byte) 149;
      numArray3[46] = (byte) 64 /*0x40*/;
      numArray3[24] = (byte) 92;
      numArray3[0] = (byte) 218;
      numArray3[8] = (byte) 224 /*0xE0*/;
      numArray3[19] = (byte) 150;
      numArray3[28] = (byte) 118;
      numArray3[21] = (byte) 16 /*0x10*/;
      numArray3[4] = (byte) 187;
      numArray3[31 /*0x1F*/] = (byte) 214;
      numArray3[36] = (byte) 94;
      numArray3[25] = (byte) 52;
      numArray3[34] = (byte) 144 /*0x90*/;
      numArray3[39] = (byte) 238;
      numArray3[12] = (byte) 183;
      numArray3[40] = (byte) 94;
      numArray3[33] = (byte) 222;
      numArray3[11] = (byte) 155;
      numArray3[23] = (byte) 115;
      numArray3[41] = (byte) 151;
      numArray3[42] = (byte) 223;
      numArray3[47] = (byte) 225;
      numArray3[44] = (byte) 112 /*0x70*/;
      numArray3[1] = (byte) 149;
      numArray3[7] = (byte) 179;
      numArray3[43] = (byte) 156;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_13537.sspq, 0, (Array) numArray4, 0, 11);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13537.sspr, 0, (Array) numArray4, 0, 11);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[48 /*0x30*/];
    byte[] numArray6 = new byte[48 /*0x30*/];
    numArray6[26] = (byte) 44;
    numArray6[1] = (byte) 7;
    numArray6[24] = (byte) 247;
    numArray6[0] = (byte) 60;
    numArray6[5] = (byte) 162;
    numArray6[41] = (byte) 50;
    numArray6[6] = (byte) 48 /*0x30*/;
    numArray6[7] = (byte) 18;
    numArray6[8] = (byte) 84;
    numArray6[9] = (byte) 229;
    numArray6[39] = (byte) 243;
    numArray6[34] = (byte) 211;
    numArray6[2] = (byte) 107;
    numArray6[13] = (byte) 143;
    numArray6[14] = (byte) 121;
    numArray6[15] = (byte) 179;
    numArray6[46] = (byte) 234;
    numArray6[17] = (byte) 226;
    numArray6[12] = (byte) 74;
    numArray6[19] = (byte) 47;
    numArray6[47] = (byte) 238;
    numArray6[45] = (byte) 93;
    numArray6[22] = (byte) 231;
    numArray6[10] = (byte) 54;
    numArray6[3] = (byte) 18;
    numArray6[25] = (byte) 218;
    numArray6[11] = (byte) 199;
    numArray6[20] = (byte) 113;
    numArray6[28] = (byte) 36;
    numArray6[29] = (byte) 127 /*0x7F*/;
    numArray6[30] = (byte) 156;
    numArray6[31 /*0x1F*/] = (byte) 194;
    numArray6[36] = (byte) 182;
    numArray6[21] = (byte) 176 /*0xB0*/;
    numArray6[16 /*0x10*/] = (byte) 234;
    numArray6[35] = (byte) 105;
    numArray6[27] = (byte) 72;
    numArray6[37] = (byte) 210;
    numArray6[38] = (byte) 41;
    numArray6[33] = (byte) 200;
    numArray6[40] = (byte) 245;
    numArray6[32 /*0x20*/] = (byte) 97;
    numArray6[42] = (byte) 82;
    numArray6[43] = (byte) 118;
    numArray6[44] = (byte) 209;
    numArray6[18] = (byte) 185;
    numArray6[4] = (byte) 88;
    numArray6[23] = (byte) 44;
    byte[] numArray7 = new byte[48 /*0x30*/]
    {
      (byte) 234,
      (byte) 188,
      (byte) 131,
      (byte) 158,
      (byte) 222,
      (byte) 118,
      (byte) 66,
      (byte) 37,
      (byte) 197,
      (byte) 31 /*0x1F*/,
      (byte) 85,
      (byte) 167,
      (byte) 119,
      (byte) 133,
      (byte) 147,
      (byte) 149,
      (byte) 33,
      (byte) 207,
      (byte) 63 /*0x3F*/,
      (byte) 71,
      (byte) 159,
      (byte) 194,
      (byte) 160 /*0xA0*/,
      (byte) 206,
      (byte) 177,
      (byte) 104,
      (byte) 239,
      (byte) 141,
      (byte) 128 /*0x80*/,
      (byte) 152,
      (byte) 221,
      (byte) 125,
      (byte) 4,
      (byte) 6,
      (byte) 38,
      (byte) 174,
      (byte) 203,
      (byte) 99,
      (byte) 82,
      (byte) 238,
      (byte) 93,
      (byte) 193,
      (byte) 62,
      (byte) 193,
      (byte) 99,
      (byte) 189,
      (byte) 219,
      (byte) 133
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13539(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 104,
      (byte) 50,
      (byte) 94,
      (byte) 54,
      (byte) 181,
      (byte) 38,
      (byte) 237,
      (byte) 153,
      (byte) 20,
      (byte) 45,
      (byte) 175,
      (byte) 228,
      (byte) 129,
      (byte) 143,
      (byte) 55,
      (byte) 14,
      (byte) 251,
      (byte) 34,
      (byte) 140,
      (byte) 99,
      (byte) 215,
      (byte) 13,
      (byte) 115,
      (byte) 198,
      (byte) 133,
      (byte) 97,
      (byte) 14,
      (byte) 71,
      (byte) 45,
      (byte) 250,
      (byte) 224 /*0xE0*/,
      (byte) 67,
      (byte) 253,
      (byte) 162,
      (byte) 135,
      (byte) 11,
      (byte) 122,
      (byte) 187,
      (byte) 252,
      (byte) 150,
      (byte) 34,
      (byte) 244,
      (byte) 95,
      (byte) 27,
      (byte) 153,
      (byte) 147,
      (byte) 53,
      (byte) 27
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 7,
      (byte) 158,
      (byte) 208 /*0xD0*/,
      (byte) 206,
      (byte) 42,
      (byte) 225,
      (byte) 85,
      (byte) 254,
      (byte) 209,
      (byte) 79,
      (byte) 102,
      (byte) 207,
      (byte) 138,
      (byte) 59,
      (byte) 14,
      (byte) 131,
      (byte) 224 /*0xE0*/,
      (byte) 240 /*0xF0*/,
      (byte) 95,
      (byte) 213,
      (byte) 92,
      (byte) 247,
      (byte) 163,
      (byte) 191,
      (byte) 130,
      (byte) 178,
      (byte) 3,
      (byte) 184,
      (byte) 128 /*0x80*/,
      (byte) 250,
      (byte) 207,
      (byte) 126,
      (byte) 8,
      (byte) 175,
      (byte) 17,
      (byte) 182,
      (byte) 33,
      (byte) 22,
      (byte) 224 /*0xE0*/,
      (byte) 26,
      (byte) 118,
      (byte) 238,
      (byte) 96 /*0x60*/,
      (byte) 21,
      (byte) 228,
      (byte) 235,
      (byte) 170
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13540(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 250,
      (byte) 32 /*0x20*/,
      (byte) 91,
      (byte) 191,
      (byte) 152,
      (byte) 176 /*0xB0*/,
      (byte) 84,
      (byte) 24,
      (byte) 165,
      (byte) 108,
      (byte) 26,
      (byte) 76,
      (byte) 107,
      (byte) 230,
      (byte) 223,
      (byte) 8,
      (byte) 43,
      (byte) 148,
      (byte) 63 /*0x3F*/,
      (byte) 38,
      (byte) 246,
      (byte) 64 /*0x40*/,
      (byte) 227,
      (byte) 173,
      (byte) 88,
      (byte) 44,
      (byte) 241,
      (byte) 44,
      (byte) 242,
      (byte) 0,
      (byte) 46,
      (byte) 177,
      (byte) 134,
      (byte) 163,
      (byte) 3,
      (byte) 129,
      (byte) 43,
      (byte) 76,
      (byte) 226,
      (byte) 10,
      (byte) 167,
      (byte) 126,
      (byte) 125,
      (byte) 135,
      (byte) 69,
      (byte) 207,
      (byte) 46,
      (byte) 173
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 163,
      (byte) 185,
      (byte) 52,
      (byte) 191,
      (byte) 55,
      (byte) 145,
      (byte) 52,
      (byte) 130,
      (byte) 22,
      (byte) 99,
      (byte) 144 /*0x90*/,
      (byte) 1,
      (byte) 68,
      (byte) 140,
      (byte) 71,
      (byte) 125,
      (byte) 194,
      (byte) 90,
      (byte) 0,
      (byte) 107,
      (byte) 32 /*0x20*/,
      (byte) 180,
      (byte) 148,
      (byte) 68,
      (byte) 202,
      (byte) 154,
      (byte) 136,
      (byte) 125,
      (byte) 214,
      (byte) 45,
      (byte) 152,
      (byte) 36,
      (byte) 187,
      (byte) 25,
      (byte) 178,
      (byte) 16 /*0x10*/,
      (byte) 128 /*0x80*/,
      (byte) 6,
      (byte) 6,
      (byte) 95,
      (byte) 61,
      (byte) 38,
      (byte) 144 /*0x90*/,
      (byte) 209,
      (byte) 46,
      (byte) 230,
      (byte) 147
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[51];
    byte[] response2 = new byte[51];
    Array.Copy((Array) sc_13537.sspq, 11, (Array) numArray2, 0, 51);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13537.sspr, 11, (Array) numArray2, 0, 51);
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

  internal static int ssp_appserver_13541(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 59,
      (byte) 184,
      (byte) 212,
      (byte) 181,
      (byte) 48 /*0x30*/,
      (byte) 246,
      (byte) 225,
      (byte) 69,
      (byte) 221,
      (byte) 108,
      (byte) 122,
      (byte) 81,
      (byte) 69,
      (byte) 190,
      (byte) 120,
      (byte) 192 /*0xC0*/,
      (byte) 115,
      (byte) 205,
      (byte) 152,
      (byte) 24,
      (byte) 94,
      (byte) 250,
      (byte) 58,
      (byte) 232,
      (byte) 254,
      (byte) 32 /*0x20*/,
      (byte) 234,
      (byte) 17,
      (byte) 139,
      (byte) 127 /*0x7F*/,
      (byte) 152,
      (byte) 122,
      (byte) 222,
      (byte) 168,
      (byte) 170,
      (byte) 12,
      (byte) 211,
      (byte) 156,
      (byte) 201,
      (byte) 83,
      (byte) 99,
      (byte) 56,
      (byte) 7,
      (byte) 11,
      (byte) 209,
      (byte) 46,
      (byte) 18,
      (byte) 25
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 127 /*0x7F*/,
      (byte) 163,
      (byte) 40,
      (byte) 37,
      (byte) 62,
      (byte) 37,
      (byte) 212,
      (byte) 105,
      (byte) 216,
      (byte) 44,
      (byte) 105,
      (byte) 113,
      (byte) 42,
      (byte) 234,
      (byte) 126,
      (byte) 208 /*0xD0*/,
      (byte) 249,
      (byte) 230,
      (byte) 196,
      (byte) 217,
      (byte) 226,
      (byte) 57,
      (byte) 29,
      (byte) 248,
      (byte) 200,
      (byte) 136,
      (byte) 103,
      (byte) 247,
      (byte) 225,
      (byte) 168,
      (byte) 172,
      (byte) 89,
      (byte) 45,
      (byte) 142,
      (byte) 243,
      (byte) 173,
      (byte) 140,
      (byte) 170,
      (byte) 188,
      (byte) 116,
      (byte) 199,
      (byte) 67,
      (byte) 228,
      (byte) 71,
      (byte) 26,
      (byte) 165,
      (byte) 18,
      (byte) 241
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[12];
    byte[] response2 = new byte[12];
    Array.Copy((Array) sc_13537.sspq, 62, (Array) numArray2, 0, 12);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13537.sspr, 62, (Array) numArray2, 0, 12);
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

  internal static int ssp_appserver_13542(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 114,
      (byte) 157,
      (byte) 20,
      (byte) 107,
      (byte) 237,
      (byte) 214,
      (byte) 4,
      (byte) 69,
      (byte) 181,
      (byte) 37,
      (byte) 242,
      (byte) 47,
      (byte) 22,
      (byte) 208 /*0xD0*/,
      (byte) 13,
      (byte) 209,
      (byte) 64 /*0x40*/,
      (byte) 112 /*0x70*/,
      (byte) 234,
      (byte) 0,
      (byte) 73,
      byte.MaxValue,
      (byte) 207,
      (byte) 105,
      (byte) 142,
      (byte) 231,
      (byte) 230,
      (byte) 184,
      (byte) 71,
      (byte) 147,
      (byte) 127 /*0x7F*/,
      (byte) 169,
      (byte) 55,
      (byte) 210,
      (byte) 187,
      (byte) 116,
      (byte) 5,
      (byte) 101,
      (byte) 46,
      (byte) 191,
      (byte) 147,
      (byte) 235,
      (byte) 41,
      (byte) 179,
      (byte) 78,
      (byte) 78,
      (byte) 212,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 70,
      (byte) 40,
      (byte) 153,
      (byte) 77,
      (byte) 205,
      (byte) 253,
      (byte) 240 /*0xF0*/,
      (byte) 91,
      (byte) 188,
      (byte) 194,
      (byte) 26,
      (byte) 71,
      (byte) 230,
      (byte) 60,
      (byte) 160 /*0xA0*/,
      (byte) 118,
      (byte) 102,
      (byte) 180,
      (byte) 65,
      (byte) 132,
      (byte) 59,
      (byte) 205,
      (byte) 127 /*0x7F*/,
      (byte) 190,
      (byte) 134,
      (byte) 222,
      (byte) 242,
      byte.MaxValue,
      (byte) 17,
      (byte) 104,
      (byte) 56,
      (byte) 85,
      (byte) 189,
      (byte) 10,
      (byte) 26,
      (byte) 46,
      (byte) 219,
      (byte) 78,
      (byte) 146,
      (byte) 57,
      (byte) 74,
      (byte) 186,
      (byte) 119,
      (byte) 93,
      (byte) 128 /*0x80*/,
      (byte) 28,
      (byte) 233,
      (byte) 141
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13543(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 182,
      (byte) 114,
      (byte) 206,
      (byte) 170,
      (byte) 220,
      (byte) 5,
      (byte) 240 /*0xF0*/,
      (byte) 75,
      (byte) 253,
      (byte) 34,
      (byte) 166,
      (byte) 72,
      (byte) 247,
      (byte) 68,
      (byte) 21,
      (byte) 152,
      (byte) 203,
      (byte) 219,
      (byte) 66,
      (byte) 43,
      (byte) 109,
      (byte) 93,
      (byte) 214,
      (byte) 27,
      (byte) 26,
      (byte) 64 /*0x40*/,
      (byte) 18,
      (byte) 0,
      (byte) 181,
      (byte) 13,
      (byte) 3,
      (byte) 53,
      (byte) 54,
      (byte) 100,
      (byte) 28,
      (byte) 72,
      (byte) 166,
      (byte) 90,
      (byte) 136,
      (byte) 40,
      (byte) 206,
      (byte) 71,
      (byte) 238,
      (byte) 6,
      (byte) 13,
      (byte) 5,
      (byte) 86
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 170,
      (byte) 14,
      (byte) 165,
      (byte) 56,
      (byte) 94,
      (byte) 61,
      (byte) 216,
      (byte) 209,
      (byte) 61,
      (byte) 148,
      (byte) 66,
      (byte) 23,
      (byte) 2,
      (byte) 20,
      (byte) 27,
      (byte) 234,
      (byte) 73,
      (byte) 49,
      (byte) 14,
      (byte) 6,
      (byte) 125,
      (byte) 198,
      (byte) 193,
      (byte) 50,
      (byte) 53,
      (byte) 178,
      (byte) 227,
      (byte) 171,
      (byte) 181,
      (byte) 76,
      (byte) 162,
      (byte) 97,
      (byte) 118,
      (byte) 247,
      (byte) 123,
      (byte) 74,
      (byte) 6,
      (byte) 76,
      (byte) 138,
      (byte) 105,
      (byte) 248,
      (byte) 76,
      (byte) 207,
      (byte) 28,
      (byte) 65,
      (byte) 191,
      (byte) 77,
      (byte) 120
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13544(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 7,
      (byte) 72,
      (byte) 101,
      (byte) 89,
      (byte) 156,
      (byte) 179,
      (byte) 144 /*0x90*/,
      (byte) 156,
      (byte) 242,
      (byte) 240 /*0xF0*/,
      (byte) 149,
      (byte) 239,
      (byte) 31 /*0x1F*/,
      (byte) 112 /*0x70*/,
      (byte) 18,
      (byte) 146,
      (byte) 40,
      (byte) 204,
      (byte) 242,
      (byte) 112 /*0x70*/,
      (byte) 82,
      (byte) 25,
      (byte) 92,
      (byte) 37,
      (byte) 110,
      (byte) 208 /*0xD0*/,
      (byte) 13,
      (byte) 20,
      (byte) 233,
      (byte) 130,
      (byte) 48 /*0x30*/,
      (byte) 75,
      (byte) 158,
      (byte) 78,
      (byte) 46,
      (byte) 237,
      byte.MaxValue,
      (byte) 195,
      (byte) 172,
      (byte) 242,
      (byte) 202,
      (byte) 235,
      (byte) 53,
      (byte) 222,
      (byte) 177,
      (byte) 254,
      (byte) 231,
      (byte) 157
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 132,
      (byte) 41,
      (byte) 2,
      (byte) 141,
      (byte) 22,
      (byte) 95,
      (byte) 91,
      (byte) 28,
      (byte) 140,
      (byte) 182,
      (byte) 150,
      (byte) 149,
      (byte) 33,
      (byte) 251,
      (byte) 218,
      (byte) 142,
      (byte) 238,
      (byte) 83,
      (byte) 196,
      (byte) 16 /*0x10*/,
      (byte) 19,
      (byte) 167,
      (byte) 37,
      (byte) 203,
      (byte) 194,
      (byte) 7,
      (byte) 247,
      (byte) 1,
      (byte) 112 /*0x70*/,
      (byte) 233,
      (byte) 52,
      (byte) 146,
      (byte) 39,
      (byte) 79,
      (byte) 181,
      (byte) 74,
      (byte) 239,
      (byte) 234,
      (byte) 90,
      (byte) 202,
      (byte) 220,
      (byte) 105,
      (byte) 171,
      (byte) 221,
      (byte) 84,
      (byte) 117,
      (byte) 140,
      (byte) 202
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13545(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 254,
      (byte) 98,
      (byte) 237,
      (byte) 235,
      (byte) 64 /*0x40*/,
      (byte) 234,
      (byte) 203,
      (byte) 166,
      (byte) 123,
      (byte) 179,
      (byte) 112 /*0x70*/,
      (byte) 192 /*0xC0*/,
      (byte) 74,
      (byte) 81,
      (byte) 208 /*0xD0*/,
      (byte) 65,
      (byte) 37,
      (byte) 147,
      (byte) 101,
      (byte) 32 /*0x20*/,
      (byte) 0,
      (byte) 166,
      (byte) 102,
      (byte) 228,
      (byte) 126,
      (byte) 115,
      (byte) 111,
      (byte) 233,
      (byte) 111,
      (byte) 33,
      (byte) 37,
      (byte) 25,
      (byte) 44,
      (byte) 210,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 65,
      (byte) 1,
      (byte) 191,
      (byte) 216,
      (byte) 224 /*0xE0*/,
      (byte) 76,
      (byte) 78,
      (byte) 56,
      (byte) 196,
      (byte) 29,
      (byte) 103,
      (byte) 173
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 2;
    sourceArray2[7] = (byte) 124;
    sourceArray2[29] = (byte) 135;
    sourceArray2[3] = (byte) 249;
    sourceArray2[37] = (byte) 200;
    sourceArray2[12] = (byte) 172;
    sourceArray2[5] = (byte) 184;
    sourceArray2[0] = (byte) 243;
    sourceArray2[1] = (byte) 154;
    sourceArray2[9] = (byte) 113;
    sourceArray2[31 /*0x1F*/] = (byte) 104;
    sourceArray2[4] = (byte) 6;
    sourceArray2[13] = (byte) 117;
    sourceArray2[41] = (byte) 42;
    sourceArray2[14] = (byte) 74;
    sourceArray2[15] = (byte) 174;
    sourceArray2[2] = (byte) 61;
    sourceArray2[17] = byte.MaxValue;
    sourceArray2[22] = (byte) 96 /*0x60*/;
    sourceArray2[19] = (byte) 228;
    sourceArray2[20] = (byte) 2;
    sourceArray2[16 /*0x10*/] = (byte) 243;
    sourceArray2[6] = (byte) 12;
    sourceArray2[26] = (byte) 204;
    sourceArray2[23] = (byte) 156;
    sourceArray2[25] = (byte) 134;
    sourceArray2[40] = (byte) 106;
    sourceArray2[38] = (byte) 115;
    sourceArray2[28] = (byte) 166;
    sourceArray2[44] = (byte) 64 /*0x40*/;
    sourceArray2[30] = (byte) 154;
    sourceArray2[46] = (byte) 203;
    sourceArray2[47] = (byte) 12;
    sourceArray2[33] = (byte) 60;
    sourceArray2[10] = (byte) 181;
    sourceArray2[35] = (byte) 207;
    sourceArray2[36] = (byte) 184;
    sourceArray2[34] = (byte) 154;
    sourceArray2[8] = (byte) 179;
    sourceArray2[39] = (byte) 133;
    sourceArray2[32 /*0x20*/] = (byte) 17;
    sourceArray2[27] = (byte) 149;
    sourceArray2[42] = (byte) 216;
    sourceArray2[43] = (byte) 88;
    sourceArray2[24] = (byte) 105;
    sourceArray2[18] = (byte) 84;
    sourceArray2[45] = (byte) 27;
    sourceArray2[11] = (byte) 139;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13546(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 239,
      (byte) 252,
      (byte) 50,
      (byte) 91,
      (byte) 170,
      (byte) 92,
      (byte) 118,
      (byte) 163,
      (byte) 195,
      (byte) 1,
      (byte) 205,
      (byte) 233,
      (byte) 12,
      (byte) 192 /*0xC0*/,
      (byte) 183,
      (byte) 15,
      (byte) 34,
      (byte) 6,
      (byte) 152,
      (byte) 63 /*0x3F*/,
      (byte) 73,
      (byte) 29,
      (byte) 105,
      (byte) 242,
      (byte) 200,
      (byte) 142,
      (byte) 225,
      (byte) 46,
      (byte) 65,
      (byte) 158,
      (byte) 98,
      (byte) 130,
      (byte) 178,
      (byte) 214,
      (byte) 29,
      (byte) 198,
      (byte) 160 /*0xA0*/,
      (byte) 184,
      (byte) 168,
      (byte) 152,
      (byte) 160 /*0xA0*/,
      (byte) 166,
      (byte) 150,
      (byte) 70,
      (byte) 36,
      (byte) 78,
      (byte) 135,
      (byte) 49
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 49;
    sourceArray2[1] = (byte) 95;
    sourceArray2[22] = (byte) 50;
    sourceArray2[7] = (byte) 106;
    sourceArray2[13] = (byte) 230;
    sourceArray2[5] = (byte) 78;
    sourceArray2[37] = (byte) 147;
    sourceArray2[26] = (byte) 146;
    sourceArray2[8] = (byte) 177;
    sourceArray2[46] = (byte) 74;
    sourceArray2[4] = (byte) 131;
    sourceArray2[11] = (byte) 74;
    sourceArray2[47] = (byte) 233;
    sourceArray2[25] = (byte) 36;
    sourceArray2[14] = (byte) 201;
    sourceArray2[19] = (byte) 173;
    sourceArray2[45] = (byte) 211;
    sourceArray2[17] = (byte) 146;
    sourceArray2[28] = (byte) 175;
    sourceArray2[21] = (byte) 249;
    sourceArray2[38] = (byte) 15;
    sourceArray2[6] = (byte) 237;
    sourceArray2[9] = (byte) 228;
    sourceArray2[23] = (byte) 212;
    sourceArray2[43] = (byte) 232;
    sourceArray2[20] = (byte) 100;
    sourceArray2[18] = (byte) 68;
    sourceArray2[27] = (byte) 4;
    sourceArray2[12] = (byte) 197;
    sourceArray2[24] = (byte) 43;
    sourceArray2[30] = (byte) 235;
    sourceArray2[3] = (byte) 188;
    sourceArray2[35] = (byte) 89;
    sourceArray2[32 /*0x20*/] = (byte) 219;
    sourceArray2[33] = (byte) 103;
    sourceArray2[34] = (byte) 145;
    sourceArray2[36] = (byte) 201;
    sourceArray2[40] = (byte) 236;
    sourceArray2[0] = (byte) 70;
    sourceArray2[39] = (byte) 37;
    sourceArray2[15] = (byte) 34;
    sourceArray2[41] = (byte) 49;
    sourceArray2[42] = (byte) 173;
    sourceArray2[31 /*0x1F*/] = (byte) 241;
    sourceArray2[44] = (byte) 134;
    sourceArray2[10] = (byte) 169;
    sourceArray2[29] = (byte) 144 /*0x90*/;
    sourceArray2[2] = (byte) 205;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13547(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 122;
    sourceArray1[18] = (byte) 41;
    sourceArray1[37] = (byte) 210;
    sourceArray1[21] = (byte) 160 /*0xA0*/;
    sourceArray1[29] = (byte) 94;
    sourceArray1[43] = byte.MaxValue;
    sourceArray1[6] = (byte) 18;
    sourceArray1[14] = (byte) 202;
    sourceArray1[32 /*0x20*/] = (byte) 245;
    sourceArray1[17] = (byte) 122;
    sourceArray1[41] = (byte) 245;
    sourceArray1[45] = (byte) 241;
    sourceArray1[12] = (byte) 25;
    sourceArray1[23] = (byte) 189;
    sourceArray1[5] = (byte) 143;
    sourceArray1[7] = (byte) 142;
    sourceArray1[0] = (byte) 81;
    sourceArray1[4] = (byte) 138;
    sourceArray1[20] = (byte) 179;
    sourceArray1[19] = (byte) 218;
    sourceArray1[3] = (byte) 128 /*0x80*/;
    sourceArray1[13] = (byte) 20;
    sourceArray1[22] = (byte) 88;
    sourceArray1[26] = (byte) 208 /*0xD0*/;
    sourceArray1[24] = (byte) 12;
    sourceArray1[11] = (byte) 112 /*0x70*/;
    sourceArray1[1] = (byte) 142;
    sourceArray1[2] = (byte) 180;
    sourceArray1[28] = (byte) 69;
    sourceArray1[10] = (byte) 112 /*0x70*/;
    sourceArray1[30] = (byte) 20;
    sourceArray1[9] = (byte) 75;
    sourceArray1[15] = (byte) 190;
    sourceArray1[38] = (byte) 32 /*0x20*/;
    sourceArray1[34] = (byte) 181;
    sourceArray1[33] = (byte) 98;
    sourceArray1[36] = (byte) 212;
    sourceArray1[8] = (byte) 156;
    sourceArray1[25] = (byte) 67;
    sourceArray1[39] = (byte) 229;
    sourceArray1[40] = (byte) 93;
    sourceArray1[27] = (byte) 225;
    sourceArray1[16 /*0x10*/] = (byte) 14;
    sourceArray1[42] = (byte) 249;
    sourceArray1[44] = (byte) 195;
    sourceArray1[35] = (byte) 33;
    sourceArray1[46] = (byte) 11;
    sourceArray1[47] = (byte) 120;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[35] = (byte) 89;
    sourceArray2[46] = (byte) 235;
    sourceArray2[2] = (byte) 197;
    sourceArray2[6] = (byte) 149;
    sourceArray2[4] = (byte) 177;
    sourceArray2[37] = (byte) 5;
    sourceArray2[15] = (byte) 74;
    sourceArray2[9] = (byte) 31 /*0x1F*/;
    sourceArray2[8] = (byte) 56;
    sourceArray2[17] = (byte) 207;
    sourceArray2[10] = (byte) 156;
    sourceArray2[7] = (byte) 165;
    sourceArray2[36] = (byte) 132;
    sourceArray2[0] = (byte) 24;
    sourceArray2[14] = (byte) 112 /*0x70*/;
    sourceArray2[28] = (byte) 236;
    sourceArray2[32 /*0x20*/] = (byte) 117;
    sourceArray2[5] = (byte) 35;
    sourceArray2[18] = (byte) 243;
    sourceArray2[19] = (byte) 133;
    sourceArray2[24] = (byte) 196;
    sourceArray2[21] = (byte) 125;
    sourceArray2[22] = (byte) 31 /*0x1F*/;
    sourceArray2[23] = (byte) 193;
    sourceArray2[44] = (byte) 123;
    sourceArray2[31 /*0x1F*/] = (byte) 229;
    sourceArray2[26] = (byte) 249;
    sourceArray2[27] = (byte) 20;
    sourceArray2[20] = (byte) 12;
    sourceArray2[45] = (byte) 91;
    sourceArray2[30] = (byte) 162;
    sourceArray2[11] = (byte) 62;
    sourceArray2[25] = (byte) 221;
    sourceArray2[1] = (byte) 36;
    sourceArray2[34] = (byte) 40;
    sourceArray2[42] = (byte) 149;
    sourceArray2[43] = (byte) 176 /*0xB0*/;
    sourceArray2[16 /*0x10*/] = (byte) 135;
    sourceArray2[29] = (byte) 171;
    sourceArray2[39] = (byte) 209;
    sourceArray2[40] = (byte) 118;
    sourceArray2[41] = (byte) 74;
    sourceArray2[38] = (byte) 30;
    sourceArray2[3] = (byte) 235;
    sourceArray2[12] = (byte) 203;
    sourceArray2[33] = (byte) 99;
    sourceArray2[13] = (byte) 159;
    sourceArray2[47] = (byte) 175;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13548(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 71;
    sourceArray1[7] = (byte) 154;
    sourceArray1[2] = (byte) 16 /*0x10*/;
    sourceArray1[41] = (byte) 73;
    sourceArray1[13] = (byte) 29;
    sourceArray1[0] = (byte) 191;
    sourceArray1[6] = (byte) 242;
    sourceArray1[4] = (byte) 176 /*0xB0*/;
    sourceArray1[1] = (byte) 137;
    sourceArray1[9] = (byte) 237;
    sourceArray1[10] = (byte) 162;
    sourceArray1[33] = (byte) 180;
    sourceArray1[11] = (byte) 29;
    sourceArray1[47] = (byte) 162;
    sourceArray1[46] = (byte) 103;
    sourceArray1[42] = (byte) 126;
    sourceArray1[39] = (byte) 91;
    sourceArray1[17] = (byte) 78;
    sourceArray1[38] = (byte) 222;
    sourceArray1[18] = (byte) 207;
    sourceArray1[20] = (byte) 41;
    sourceArray1[21] = (byte) 41;
    sourceArray1[12] = (byte) 27;
    sourceArray1[23] = (byte) 160 /*0xA0*/;
    sourceArray1[24] = (byte) 232;
    sourceArray1[28] = (byte) 98;
    sourceArray1[26] = (byte) 58;
    sourceArray1[27] = (byte) 15;
    sourceArray1[22] = (byte) 191;
    sourceArray1[29] = (byte) 96 /*0x60*/;
    sourceArray1[30] = (byte) 175;
    sourceArray1[31 /*0x1F*/] = (byte) 43;
    sourceArray1[32 /*0x20*/] = (byte) 213;
    sourceArray1[8] = (byte) 191;
    sourceArray1[34] = (byte) 94;
    sourceArray1[35] = (byte) 199;
    sourceArray1[36] = (byte) 0;
    sourceArray1[14] = (byte) 48 /*0x30*/;
    sourceArray1[15] = (byte) 150;
    sourceArray1[19] = (byte) 179;
    sourceArray1[3] = (byte) 164;
    sourceArray1[16 /*0x10*/] = (byte) 30;
    sourceArray1[40] = (byte) 177;
    sourceArray1[43] = (byte) 245;
    sourceArray1[44] = (byte) 89;
    sourceArray1[45] = (byte) 16 /*0x10*/;
    sourceArray1[25] = (byte) 149;
    sourceArray1[5] = (byte) 210;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 10,
      (byte) 121,
      (byte) 211,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 0,
      (byte) 119,
      (byte) 203,
      (byte) 75,
      (byte) 196,
      (byte) 208 /*0xD0*/,
      (byte) 107,
      (byte) 18,
      (byte) 212,
      (byte) 89,
      (byte) 36,
      (byte) 39,
      (byte) 7,
      (byte) 150,
      (byte) 22,
      (byte) 253,
      (byte) 150,
      (byte) 193,
      (byte) 195,
      (byte) 195,
      (byte) 10,
      (byte) 231,
      (byte) 223,
      (byte) 168,
      (byte) 166,
      (byte) 105,
      (byte) 186,
      (byte) 23,
      (byte) 195,
      (byte) 93,
      (byte) 156,
      (byte) 218,
      (byte) 192 /*0xC0*/,
      (byte) 191,
      (byte) 75,
      (byte) 63 /*0x3F*/,
      (byte) 163,
      (byte) 155,
      (byte) 39,
      (byte) 213,
      (byte) 35,
      (byte) 46,
      (byte) 99
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13549(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 51;
    sourceArray1[44] = (byte) 253;
    sourceArray1[2] = (byte) 219;
    sourceArray1[3] = (byte) 42;
    sourceArray1[42] = (byte) 80 /*0x50*/;
    sourceArray1[37] = (byte) 118;
    sourceArray1[47] = (byte) 213;
    sourceArray1[30] = (byte) 139;
    sourceArray1[9] = (byte) 235;
    sourceArray1[12] = (byte) 228;
    sourceArray1[38] = (byte) 128 /*0x80*/;
    sourceArray1[11] = (byte) 40;
    sourceArray1[40] = (byte) 123;
    sourceArray1[10] = (byte) 0;
    sourceArray1[14] = byte.MaxValue;
    sourceArray1[7] = (byte) 80 /*0x50*/;
    sourceArray1[16 /*0x10*/] = (byte) 151;
    sourceArray1[41] = (byte) 49;
    sourceArray1[8] = (byte) 194;
    sourceArray1[19] = (byte) 92;
    sourceArray1[20] = (byte) 197;
    sourceArray1[21] = (byte) 84;
    sourceArray1[5] = (byte) 79;
    sourceArray1[23] = (byte) 107;
    sourceArray1[13] = (byte) 237;
    sourceArray1[0] = (byte) 0;
    sourceArray1[27] = (byte) 176 /*0xB0*/;
    sourceArray1[6] = (byte) 158;
    sourceArray1[46] = (byte) 166;
    sourceArray1[29] = (byte) 207;
    sourceArray1[18] = (byte) 78;
    sourceArray1[31 /*0x1F*/] = (byte) 18;
    sourceArray1[35] = (byte) 180;
    sourceArray1[4] = (byte) 4;
    sourceArray1[34] = (byte) 45;
    sourceArray1[32 /*0x20*/] = (byte) 100;
    sourceArray1[36] = (byte) 238;
    sourceArray1[26] = (byte) 173;
    sourceArray1[1] = (byte) 227;
    sourceArray1[25] = (byte) 76;
    sourceArray1[39] = (byte) 161;
    sourceArray1[17] = (byte) 217;
    sourceArray1[15] = (byte) 24;
    sourceArray1[43] = (byte) 221;
    sourceArray1[24] = (byte) 5;
    sourceArray1[45] = (byte) 104;
    sourceArray1[22] = (byte) 246;
    sourceArray1[33] = (byte) 15;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 188,
      (byte) 6,
      (byte) 9,
      (byte) 202,
      (byte) 222,
      (byte) 193,
      (byte) 186,
      (byte) 23,
      (byte) 232,
      (byte) 193,
      (byte) 232,
      (byte) 85,
      (byte) 126,
      (byte) 24,
      (byte) 7,
      (byte) 11,
      (byte) 243,
      (byte) 2,
      (byte) 251,
      (byte) 224 /*0xE0*/,
      (byte) 250,
      (byte) 9,
      (byte) 86,
      (byte) 46,
      (byte) 125,
      (byte) 173,
      (byte) 241,
      (byte) 245,
      (byte) 144 /*0x90*/,
      (byte) 64 /*0x40*/,
      (byte) 224 /*0xE0*/,
      (byte) 184,
      (byte) 108,
      (byte) 143,
      (byte) 43,
      (byte) 186,
      (byte) 83,
      (byte) 196,
      (byte) 94,
      (byte) 44,
      (byte) 240 /*0xF0*/,
      (byte) 25,
      (byte) 252,
      (byte) 166,
      (byte) 245,
      (byte) 2,
      (byte) 164
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13550(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 209,
      (byte) 20,
      (byte) 121,
      (byte) 139,
      (byte) 87,
      (byte) 236,
      (byte) 187,
      (byte) 165,
      (byte) 111,
      (byte) 167,
      (byte) 211,
      (byte) 1,
      (byte) 136,
      (byte) 19,
      (byte) 201,
      (byte) 2,
      (byte) 158,
      (byte) 156,
      (byte) 25,
      (byte) 67,
      (byte) 230,
      (byte) 199,
      (byte) 63 /*0x3F*/,
      (byte) 131,
      (byte) 171,
      (byte) 208 /*0xD0*/,
      (byte) 146,
      (byte) 173,
      (byte) 84,
      (byte) 127 /*0x7F*/,
      (byte) 167,
      (byte) 156,
      (byte) 94,
      (byte) 248,
      (byte) 212,
      (byte) 195,
      (byte) 113,
      (byte) 243,
      (byte) 254,
      (byte) 89,
      (byte) 238,
      (byte) 56,
      (byte) 157,
      (byte) 217,
      (byte) 243,
      (byte) 125,
      (byte) 200,
      (byte) 180
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 159,
      (byte) 23,
      (byte) 154,
      (byte) 117,
      (byte) 131,
      (byte) 156,
      (byte) 126,
      (byte) 11,
      (byte) 206,
      (byte) 224 /*0xE0*/,
      (byte) 92,
      (byte) 179,
      (byte) 193,
      (byte) 154,
      (byte) 60,
      (byte) 96 /*0x60*/,
      (byte) 47,
      (byte) 224 /*0xE0*/,
      (byte) 211,
      (byte) 193,
      (byte) 163,
      (byte) 115,
      (byte) 75,
      (byte) 224 /*0xE0*/,
      (byte) 139,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 8,
      (byte) 33,
      (byte) 112 /*0x70*/,
      (byte) 98,
      (byte) 136,
      (byte) 4,
      (byte) 124,
      (byte) 197,
      (byte) 203,
      (byte) 13,
      (byte) 183,
      (byte) 4,
      (byte) 88,
      (byte) 168,
      (byte) 74,
      (byte) 57,
      (byte) 118,
      (byte) 211,
      (byte) 91,
      (byte) 67
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13551(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 222;
    sourceArray1[1] = (byte) 190;
    sourceArray1[2] = (byte) 9;
    sourceArray1[3] = (byte) 213;
    sourceArray1[4] = (byte) 245;
    sourceArray1[12] = (byte) 55;
    sourceArray1[32 /*0x20*/] = (byte) 229;
    sourceArray1[33] = (byte) 56;
    sourceArray1[24] = (byte) 101;
    sourceArray1[9] = (byte) 81;
    sourceArray1[10] = (byte) 239;
    sourceArray1[15] = (byte) 62;
    sourceArray1[16 /*0x10*/] = (byte) 6;
    sourceArray1[13] = (byte) 162;
    sourceArray1[14] = (byte) 144 /*0x90*/;
    sourceArray1[11] = (byte) 0;
    sourceArray1[19] = (byte) 157;
    sourceArray1[17] = (byte) 145;
    sourceArray1[38] = (byte) 123;
    sourceArray1[47] = (byte) 218;
    sourceArray1[44] = (byte) 173;
    sourceArray1[21] = (byte) 84;
    sourceArray1[22] = (byte) 174;
    sourceArray1[25] = (byte) 144 /*0x90*/;
    sourceArray1[29] = (byte) 120;
    sourceArray1[39] = (byte) 19;
    sourceArray1[26] = (byte) 46;
    sourceArray1[27] = (byte) 7;
    sourceArray1[34] = (byte) 145;
    sourceArray1[43] = (byte) 231;
    sourceArray1[8] = (byte) 13;
    sourceArray1[31 /*0x1F*/] = (byte) 9;
    sourceArray1[5] = (byte) 223;
    sourceArray1[35] = (byte) 57;
    sourceArray1[42] = (byte) 224 /*0xE0*/;
    sourceArray1[45] = (byte) 217;
    sourceArray1[6] = (byte) 135;
    sourceArray1[23] = (byte) 41;
    sourceArray1[7] = (byte) 89;
    sourceArray1[30] = (byte) 201;
    sourceArray1[40] = (byte) 119;
    sourceArray1[41] = (byte) 0;
    sourceArray1[18] = (byte) 171;
    sourceArray1[0] = (byte) 204;
    sourceArray1[20] = (byte) 191;
    sourceArray1[37] = (byte) 238;
    sourceArray1[46] = (byte) 188;
    sourceArray1[36] = (byte) 203;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 183,
      (byte) 26,
      (byte) 2,
      (byte) 139,
      (byte) 164,
      (byte) 94,
      (byte) 234,
      (byte) 146,
      (byte) 224 /*0xE0*/,
      (byte) 100,
      (byte) 193,
      (byte) 253,
      (byte) 107,
      (byte) 183,
      (byte) 111,
      (byte) 228,
      (byte) 193,
      (byte) 206,
      (byte) 124,
      (byte) 192 /*0xC0*/,
      (byte) 183,
      (byte) 99,
      (byte) 150,
      (byte) 122,
      (byte) 119,
      (byte) 197,
      (byte) 228,
      (byte) 131,
      (byte) 154,
      (byte) 207,
      (byte) 37,
      (byte) 28,
      (byte) 223,
      (byte) 98,
      (byte) 161,
      (byte) 157,
      (byte) 213,
      (byte) 244,
      (byte) 185,
      (byte) 7,
      (byte) 159,
      (byte) 157,
      (byte) 119,
      (byte) 243,
      (byte) 13,
      (byte) 162,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[17];
    byte[] response2 = new byte[17];
    Array.Copy((Array) sc_13537.sspq, 74, (Array) numArray2, 0, 17);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13537.sspr, 74, (Array) numArray2, 0, 17);
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

  internal static int ssp_appserver_13552(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 181,
      (byte) 168,
      (byte) 87,
      (byte) 106,
      (byte) 247,
      (byte) 44,
      (byte) 135,
      (byte) 154,
      (byte) 0,
      (byte) 53,
      (byte) 47,
      (byte) 59,
      (byte) 40,
      (byte) 103,
      (byte) 180,
      (byte) 203,
      (byte) 22,
      (byte) 138,
      (byte) 236,
      (byte) 102,
      (byte) 50,
      (byte) 238,
      (byte) 11,
      (byte) 196,
      (byte) 176 /*0xB0*/,
      (byte) 21,
      (byte) 225,
      (byte) 136,
      (byte) 153,
      (byte) 131,
      (byte) 3,
      (byte) 68,
      (byte) 113,
      byte.MaxValue,
      (byte) 170,
      (byte) 30,
      (byte) 103,
      (byte) 83,
      (byte) 210,
      (byte) 181,
      (byte) 122,
      (byte) 124,
      (byte) 226,
      (byte) 253,
      (byte) 205,
      (byte) 168,
      (byte) 219,
      (byte) 225
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 227,
      (byte) 134,
      (byte) 230,
      (byte) 239,
      (byte) 220,
      (byte) 109,
      (byte) 213,
      (byte) 59,
      (byte) 156,
      (byte) 0,
      (byte) 93,
      (byte) 20,
      (byte) 88,
      (byte) 97,
      (byte) 158,
      (byte) 147,
      (byte) 5,
      (byte) 130,
      (byte) 249,
      (byte) 239,
      (byte) 16 /*0x10*/,
      (byte) 206,
      (byte) 149,
      (byte) 28,
      (byte) 46,
      (byte) 194,
      (byte) 81,
      (byte) 63 /*0x3F*/,
      (byte) 253,
      (byte) 0,
      (byte) 45,
      (byte) 155,
      (byte) 8,
      (byte) 88,
      (byte) 137,
      (byte) 79,
      (byte) 64 /*0x40*/,
      (byte) 183,
      (byte) 139,
      (byte) 212,
      (byte) 73,
      (byte) 81,
      (byte) 45,
      (byte) 83,
      (byte) 86,
      (byte) 218,
      (byte) 8,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13553(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 173,
      (byte) 25,
      (byte) 199,
      (byte) 157,
      (byte) 146,
      (byte) 185,
      (byte) 225,
      (byte) 75,
      byte.MaxValue,
      (byte) 0,
      (byte) 124,
      (byte) 50,
      (byte) 234,
      (byte) 201,
      (byte) 40,
      (byte) 189,
      (byte) 150,
      (byte) 100,
      (byte) 194,
      (byte) 183,
      (byte) 54,
      (byte) 235,
      (byte) 181,
      (byte) 12,
      (byte) 13,
      (byte) 247,
      (byte) 7,
      (byte) 57,
      byte.MaxValue,
      (byte) 217,
      (byte) 129,
      (byte) 8,
      (byte) 47,
      (byte) 127 /*0x7F*/,
      (byte) 196,
      (byte) 230,
      (byte) 226,
      (byte) 129,
      (byte) 212,
      (byte) 153,
      (byte) 240 /*0xF0*/,
      (byte) 106,
      (byte) 123,
      (byte) 124,
      (byte) 101,
      (byte) 203,
      (byte) 226,
      (byte) 223
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 191,
      (byte) 99,
      (byte) 190,
      (byte) 7,
      (byte) 200,
      (byte) 247,
      (byte) 23,
      (byte) 125,
      (byte) 123,
      (byte) 5,
      (byte) 224 /*0xE0*/,
      (byte) 18,
      (byte) 168,
      (byte) 142,
      (byte) 60,
      (byte) 55,
      (byte) 160 /*0xA0*/,
      (byte) 18,
      (byte) 169,
      (byte) 14,
      (byte) 48 /*0x30*/,
      (byte) 117,
      (byte) 227,
      (byte) 119,
      (byte) 31 /*0x1F*/,
      (byte) 236,
      (byte) 149,
      (byte) 155,
      (byte) 0,
      (byte) 234,
      (byte) 138,
      (byte) 162,
      (byte) 216,
      (byte) 249,
      (byte) 181,
      (byte) 111,
      (byte) 239,
      (byte) 29,
      (byte) 124,
      (byte) 124,
      (byte) 145,
      (byte) 97,
      (byte) 250,
      (byte) 172,
      (byte) 147,
      (byte) 20,
      (byte) 54,
      (byte) 145
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13554(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 28,
      (byte) 143,
      (byte) 193,
      (byte) 99,
      (byte) 186,
      (byte) 143,
      (byte) 155,
      (byte) 178,
      (byte) 167,
      (byte) 75,
      (byte) 37,
      (byte) 126,
      (byte) 116,
      (byte) 2,
      (byte) 107,
      (byte) 5,
      (byte) 65,
      (byte) 218,
      (byte) 20,
      (byte) 143,
      (byte) 41,
      (byte) 199,
      (byte) 204,
      (byte) 111,
      (byte) 57,
      (byte) 166,
      (byte) 66,
      (byte) 21,
      (byte) 40,
      (byte) 134,
      (byte) 218,
      (byte) 193,
      (byte) 205,
      (byte) 78,
      (byte) 19,
      (byte) 239,
      (byte) 2,
      (byte) 131,
      (byte) 40,
      (byte) 241,
      (byte) 57,
      (byte) 231,
      (byte) 72,
      (byte) 30,
      (byte) 85,
      (byte) 253,
      (byte) 241,
      (byte) 200
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 4,
      (byte) 54,
      (byte) 49,
      (byte) 189,
      (byte) 43,
      (byte) 183,
      (byte) 117,
      (byte) 200,
      (byte) 54,
      (byte) 63 /*0x3F*/,
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 110,
      (byte) 178,
      (byte) 167,
      (byte) 41,
      (byte) 208 /*0xD0*/,
      (byte) 74,
      (byte) 227,
      (byte) 17,
      (byte) 76,
      (byte) 184,
      (byte) 182,
      (byte) 137,
      (byte) 144 /*0x90*/,
      (byte) 9,
      (byte) 232,
      (byte) 196,
      (byte) 247,
      (byte) 62,
      (byte) 22,
      (byte) 37,
      (byte) 253,
      (byte) 41,
      (byte) 234,
      (byte) 154,
      (byte) 75,
      (byte) 223,
      (byte) 163,
      (byte) 124,
      (byte) 109,
      (byte) 85,
      (byte) 98,
      (byte) 220,
      (byte) 122,
      (byte) 210,
      (byte) 19,
      (byte) 203
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13555(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 36;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[26] = (byte) 59;
    sourceArray1[21] = (byte) 85;
    sourceArray1[24] = (byte) 173;
    sourceArray1[3] = (byte) 195;
    sourceArray1[7] = (byte) 77;
    sourceArray1[4] = (byte) 81;
    sourceArray1[2] = (byte) 85;
    sourceArray1[9] = (byte) 138;
    sourceArray1[35] = (byte) 190;
    sourceArray1[43] = (byte) 92;
    sourceArray1[28] = (byte) 121;
    sourceArray1[39] = (byte) 89;
    sourceArray1[14] = (byte) 37;
    sourceArray1[15] = (byte) 239;
    sourceArray1[16 /*0x10*/] = (byte) 124;
    sourceArray1[17] = (byte) 2;
    sourceArray1[8] = (byte) 134;
    sourceArray1[19] = (byte) 178;
    sourceArray1[34] = (byte) 168;
    sourceArray1[20] = (byte) 35;
    sourceArray1[22] = (byte) 65;
    sourceArray1[23] = (byte) 126;
    sourceArray1[18] = (byte) 190;
    sourceArray1[44] = (byte) 39;
    sourceArray1[32 /*0x20*/] = (byte) 144 /*0x90*/;
    sourceArray1[27] = (byte) 181;
    sourceArray1[46] = (byte) 241;
    sourceArray1[29] = (byte) 158;
    sourceArray1[38] = (byte) 176 /*0xB0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 21;
    sourceArray1[5] = (byte) 42;
    sourceArray1[11] = (byte) 112 /*0x70*/;
    sourceArray1[33] = (byte) 189;
    sourceArray1[13] = (byte) 177;
    sourceArray1[6] = (byte) 19;
    sourceArray1[37] = (byte) 44;
    sourceArray1[47] = (byte) 76;
    sourceArray1[36] = (byte) 189;
    sourceArray1[40] = (byte) 20;
    sourceArray1[41] = (byte) 164;
    sourceArray1[42] = (byte) 179;
    sourceArray1[25] = (byte) 26;
    sourceArray1[30] = (byte) 78;
    sourceArray1[45] = (byte) 173;
    sourceArray1[10] = (byte) 33;
    sourceArray1[12] = (byte) 100;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 154,
      (byte) 51,
      (byte) 242,
      (byte) 89,
      (byte) 223,
      (byte) 110,
      (byte) 126,
      (byte) 102,
      (byte) 155,
      (byte) 74,
      (byte) 33,
      (byte) 91,
      (byte) 127 /*0x7F*/,
      (byte) 79,
      (byte) 162,
      (byte) 34,
      (byte) 66,
      (byte) 96 /*0x60*/,
      (byte) 7,
      (byte) 68,
      (byte) 179,
      (byte) 218,
      (byte) 141,
      (byte) 54,
      (byte) 33,
      (byte) 37,
      (byte) 169,
      (byte) 161,
      (byte) 46,
      (byte) 46,
      (byte) 195,
      (byte) 240 /*0xF0*/,
      (byte) 144 /*0x90*/,
      (byte) 95,
      (byte) 52,
      (byte) 60,
      (byte) 159,
      (byte) 249,
      (byte) 174,
      (byte) 102,
      (byte) 153,
      (byte) 14,
      (byte) 60,
      (byte) 34,
      (byte) 87,
      (byte) 130,
      (byte) 225,
      (byte) 235
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
