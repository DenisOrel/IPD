// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12366
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12366
{
  private static byte[] sspq = new byte[154]
  {
    (byte) 168,
    (byte) 44,
    (byte) 180,
    (byte) 127 /*0x7F*/,
    (byte) 217,
    (byte) 60,
    (byte) 244,
    (byte) 251,
    (byte) 193,
    (byte) 242,
    (byte) 203,
    byte.MaxValue,
    (byte) 158,
    (byte) 15,
    (byte) 63 /*0x3F*/,
    (byte) 198,
    (byte) 7,
    (byte) 77,
    (byte) 94,
    (byte) 71,
    (byte) 183,
    (byte) 124,
    (byte) 158,
    (byte) 11,
    (byte) 218,
    (byte) 2,
    (byte) 89,
    (byte) 238,
    (byte) 105,
    (byte) 64 /*0x40*/,
    (byte) 127 /*0x7F*/,
    (byte) 78,
    (byte) 49,
    (byte) 123,
    (byte) 123,
    (byte) 242,
    (byte) 20,
    (byte) 94,
    (byte) 206,
    (byte) 230,
    (byte) 215,
    (byte) 16 /*0x10*/,
    (byte) 44,
    (byte) 141,
    (byte) 169,
    (byte) 164,
    (byte) 214,
    (byte) 227,
    (byte) 6,
    (byte) 185,
    (byte) 89,
    (byte) 171,
    (byte) 97,
    (byte) 253,
    (byte) 112 /*0x70*/,
    (byte) 153,
    (byte) 15,
    (byte) 69,
    (byte) 8,
    (byte) 115,
    (byte) 200,
    (byte) 101,
    (byte) 78,
    (byte) 60,
    (byte) 31 /*0x1F*/,
    (byte) 189,
    (byte) 169,
    (byte) 135,
    (byte) 222,
    (byte) 251,
    (byte) 44,
    (byte) 9,
    (byte) 120,
    (byte) 131,
    (byte) 42,
    (byte) 29,
    (byte) 74,
    (byte) 251,
    (byte) 180,
    (byte) 237,
    (byte) 129,
    (byte) 126,
    (byte) 63 /*0x3F*/,
    (byte) 77,
    (byte) 139,
    (byte) 230,
    (byte) 198,
    (byte) 109,
    (byte) 66,
    (byte) 38,
    (byte) 24,
    (byte) 41,
    (byte) 161,
    (byte) 195,
    (byte) 1,
    (byte) 146,
    (byte) 90,
    (byte) 234,
    (byte) 238,
    (byte) 164,
    (byte) 182,
    (byte) 152,
    (byte) 234,
    (byte) 108,
    (byte) 161,
    (byte) 187,
    (byte) 146,
    (byte) 93,
    (byte) 192 /*0xC0*/,
    (byte) 18,
    (byte) 199,
    (byte) 9,
    (byte) 164,
    (byte) 138,
    (byte) 27,
    (byte) 42,
    (byte) 187,
    (byte) 242,
    (byte) 141,
    (byte) 160 /*0xA0*/,
    (byte) 116,
    (byte) 208 /*0xD0*/,
    (byte) 62,
    (byte) 88,
    (byte) 133,
    (byte) 44,
    (byte) 86,
    (byte) 2,
    (byte) 146,
    (byte) 99,
    (byte) 227,
    (byte) 49,
    (byte) 76,
    (byte) 86,
    (byte) 50,
    (byte) 55,
    (byte) 79,
    (byte) 251,
    (byte) 3,
    (byte) 174,
    (byte) 24,
    (byte) 127 /*0x7F*/,
    (byte) 107,
    (byte) 203,
    (byte) 33,
    (byte) 142,
    (byte) 208 /*0xD0*/,
    (byte) 12,
    (byte) 95,
    (byte) 18,
    (byte) 247,
    (byte) 137,
    (byte) 134,
    (byte) 0
  };
  private static byte[] sspr = new byte[154]
  {
    (byte) 150,
    (byte) 113,
    (byte) 117,
    (byte) 30,
    (byte) 172,
    (byte) 102,
    (byte) 193,
    (byte) 151,
    (byte) 81,
    (byte) 70,
    (byte) 212,
    (byte) 250,
    (byte) 159,
    (byte) 44,
    (byte) 26,
    (byte) 4,
    (byte) 206,
    (byte) 20,
    (byte) 242,
    (byte) 13,
    (byte) 18,
    (byte) 189,
    (byte) 72,
    (byte) 97,
    (byte) 186,
    (byte) 20,
    (byte) 230,
    (byte) 221,
    (byte) 109,
    (byte) 188,
    (byte) 8,
    (byte) 225,
    (byte) 35,
    (byte) 189,
    (byte) 155,
    (byte) 179,
    (byte) 113,
    (byte) 103,
    (byte) 11,
    (byte) 85,
    (byte) 42,
    (byte) 228,
    (byte) 172,
    (byte) 89,
    (byte) 131,
    (byte) 198,
    (byte) 209,
    (byte) 247,
    (byte) 49,
    (byte) 127 /*0x7F*/,
    (byte) 254,
    (byte) 17,
    (byte) 31 /*0x1F*/,
    (byte) 169,
    (byte) 65,
    (byte) 16 /*0x10*/,
    (byte) 30,
    (byte) 182,
    (byte) 38,
    (byte) 10,
    (byte) 108,
    (byte) 74,
    (byte) 194,
    (byte) 73,
    (byte) 76,
    (byte) 250,
    (byte) 228,
    (byte) 46,
    (byte) 137,
    (byte) 36,
    (byte) 31 /*0x1F*/,
    (byte) 140,
    (byte) 191,
    (byte) 17,
    (byte) 55,
    (byte) 138,
    (byte) 178,
    (byte) 102,
    (byte) 140,
    (byte) 42,
    (byte) 9,
    (byte) 15,
    (byte) 1,
    (byte) 232,
    (byte) 30,
    (byte) 8,
    (byte) 198,
    (byte) 54,
    (byte) 210,
    (byte) 132,
    (byte) 33,
    (byte) 117,
    (byte) 101,
    (byte) 141,
    (byte) 119,
    (byte) 121,
    (byte) 108,
    (byte) 176 /*0xB0*/,
    (byte) 166,
    (byte) 60,
    (byte) 42,
    (byte) 126,
    (byte) 85,
    (byte) 180,
    (byte) 249,
    (byte) 219,
    (byte) 164,
    (byte) 65,
    (byte) 47,
    (byte) 77,
    (byte) 130,
    (byte) 6,
    (byte) 248,
    (byte) 90,
    (byte) 224 /*0xE0*/,
    (byte) 237,
    (byte) 138,
    (byte) 113,
    (byte) 11,
    (byte) 174,
    (byte) 101,
    (byte) 138,
    (byte) 19,
    (byte) 87,
    (byte) 210,
    (byte) 34,
    (byte) 142,
    (byte) 71,
    (byte) 164,
    (byte) 89,
    (byte) 158,
    (byte) 207,
    byte.MaxValue,
    (byte) 199,
    (byte) 91,
    (byte) 56,
    (byte) 210,
    (byte) 28,
    (byte) 23,
    (byte) 217,
    (byte) 30,
    (byte) 155,
    (byte) 7,
    (byte) 133,
    (byte) 175,
    (byte) 123,
    (byte) 154,
    (byte) 50,
    (byte) 125,
    (byte) 117,
    (byte) 5,
    (byte) 188,
    (byte) 241,
    (byte) 129
  };

  internal static string ssp_appserver_12367()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 190,
        (byte) 43,
        (byte) 232,
        (byte) 239,
        (byte) 48 /*0x30*/,
        (byte) 41,
        (byte) 0,
        (byte) 44
      };
      byte[] numArray3 = new byte[8]
      {
        (byte) 142,
        (byte) 220,
        (byte) 71,
        (byte) 31 /*0x1F*/,
        (byte) 146,
        (byte) 7,
        (byte) 28,
        (byte) 196
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[8];
    byte[] numArray5 = new byte[8]
    {
      (byte) 170,
      (byte) 213,
      (byte) 33,
      (byte) 15,
      (byte) 63 /*0x3F*/,
      (byte) 229,
      (byte) 0,
      (byte) 206
    };
    numArray5[6] = (byte) 142;
    byte[] numArray6 = new byte[8];
    numArray6[0] = (byte) 23;
    numArray6[3] = (byte) 1;
    numArray6[2] = (byte) 58;
    numArray6[1] = (byte) 197;
    numArray6[6] = (byte) 82;
    numArray6[5] = (byte) 133;
    numArray6[4] = (byte) 108;
    numArray6[7] = (byte) 59;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12368(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 25,
      (byte) 213,
      (byte) 132,
      (byte) 35,
      (byte) 42,
      (byte) 216,
      (byte) 155,
      (byte) 83,
      (byte) 59,
      (byte) 134,
      (byte) 20,
      (byte) 189,
      (byte) 252,
      (byte) 211,
      (byte) 115,
      (byte) 57,
      (byte) 41,
      (byte) 65,
      (byte) 220,
      (byte) 244,
      (byte) 171,
      (byte) 56,
      (byte) 36,
      (byte) 2,
      (byte) 13,
      (byte) 76,
      (byte) 226,
      (byte) 118,
      (byte) 99,
      (byte) 194,
      (byte) 153,
      (byte) 235,
      (byte) 217,
      (byte) 42,
      (byte) 226,
      (byte) 151,
      (byte) 0,
      (byte) 222,
      (byte) 124,
      (byte) 58,
      (byte) 160 /*0xA0*/,
      (byte) 42,
      (byte) 140,
      (byte) 128 /*0x80*/,
      (byte) 16 /*0x10*/,
      (byte) 163,
      (byte) 148,
      (byte) 244
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 132;
    sourceArray2[1] = (byte) 214;
    sourceArray2[22] = (byte) 130;
    sourceArray2[3] = (byte) 87;
    sourceArray2[35] = (byte) 152;
    sourceArray2[5] = (byte) 151;
    sourceArray2[23] = (byte) 93;
    sourceArray2[7] = (byte) 239;
    sourceArray2[20] = (byte) 54;
    sourceArray2[9] = (byte) 211;
    sourceArray2[0] = (byte) 115;
    sourceArray2[29] = (byte) 78;
    sourceArray2[12] = (byte) 93;
    sourceArray2[13] = (byte) 91;
    sourceArray2[36] = (byte) 72;
    sourceArray2[15] = (byte) 170;
    sourceArray2[42] = (byte) 199;
    sourceArray2[43] = (byte) 198;
    sourceArray2[45] = (byte) 187;
    sourceArray2[11] = (byte) 20;
    sourceArray2[28] = (byte) 53;
    sourceArray2[25] = (byte) 252;
    sourceArray2[33] = (byte) 3;
    sourceArray2[19] = (byte) 60;
    sourceArray2[16 /*0x10*/] = (byte) 124;
    sourceArray2[21] = (byte) 131;
    sourceArray2[14] = (byte) 100;
    sourceArray2[30] = (byte) 95;
    sourceArray2[4] = (byte) 68;
    sourceArray2[6] = (byte) 111;
    sourceArray2[17] = (byte) 252;
    sourceArray2[31 /*0x1F*/] = (byte) 214;
    sourceArray2[32 /*0x20*/] = (byte) 215;
    sourceArray2[2] = (byte) 67;
    sourceArray2[34] = (byte) 91;
    sourceArray2[24] = (byte) 95;
    sourceArray2[8] = (byte) 17;
    sourceArray2[37] = (byte) 110;
    sourceArray2[38] = (byte) 133;
    sourceArray2[39] = (byte) 195;
    sourceArray2[40] = (byte) 0;
    sourceArray2[41] = (byte) 122;
    sourceArray2[18] = (byte) 134;
    sourceArray2[47] = (byte) 253;
    sourceArray2[44] = (byte) 149;
    sourceArray2[26] = (byte) 245;
    sourceArray2[46] = (byte) 63 /*0x3F*/;
    sourceArray2[27] = (byte) 11;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12369(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 3,
      (byte) 14,
      (byte) 75,
      (byte) 215,
      (byte) 160 /*0xA0*/,
      (byte) 124,
      (byte) 107,
      (byte) 119,
      (byte) 52,
      (byte) 13,
      (byte) 144 /*0x90*/,
      (byte) 171,
      (byte) 39,
      (byte) 8,
      (byte) 152,
      (byte) 17,
      (byte) 216,
      (byte) 185,
      (byte) 92,
      (byte) 202,
      (byte) 160 /*0xA0*/,
      (byte) 238,
      (byte) 246,
      (byte) 187,
      (byte) 129,
      (byte) 38,
      (byte) 92,
      (byte) 216,
      (byte) 71,
      (byte) 152,
      (byte) 16 /*0x10*/,
      (byte) 10,
      (byte) 169,
      (byte) 245,
      (byte) 234,
      (byte) 124,
      (byte) 146,
      (byte) 200,
      (byte) 147,
      (byte) 80 /*0x50*/,
      (byte) 173,
      (byte) 186,
      (byte) 206,
      (byte) 216,
      (byte) 236,
      (byte) 29,
      (byte) 119
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 141,
      (byte) 52,
      (byte) 126,
      (byte) 90,
      (byte) 178,
      (byte) 41,
      (byte) 228,
      (byte) 211,
      (byte) 221,
      (byte) 151,
      (byte) 24,
      (byte) 105,
      (byte) 142,
      (byte) 61,
      (byte) 10,
      (byte) 87,
      (byte) 83,
      (byte) 241,
      (byte) 194,
      (byte) 5,
      (byte) 252,
      (byte) 8,
      (byte) 198,
      (byte) 253,
      (byte) 175,
      (byte) 70,
      (byte) 199,
      (byte) 76,
      (byte) 113,
      (byte) 67,
      (byte) 246,
      (byte) 131,
      (byte) 194,
      (byte) 230,
      (byte) 3,
      (byte) 60,
      (byte) 95,
      (byte) 3,
      (byte) 146,
      (byte) 202,
      (byte) 235,
      (byte) 184,
      (byte) 186,
      (byte) 152,
      (byte) 242,
      (byte) 253,
      (byte) 230,
      (byte) 106
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12370(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 139,
      (byte) 245,
      (byte) 34,
      (byte) 20,
      (byte) 24,
      (byte) 144 /*0x90*/,
      (byte) 86,
      (byte) 122,
      (byte) 31 /*0x1F*/,
      (byte) 147,
      (byte) 47,
      (byte) 155,
      (byte) 252,
      (byte) 134,
      (byte) 1,
      (byte) 49,
      (byte) 29,
      (byte) 27,
      (byte) 127 /*0x7F*/,
      (byte) 16 /*0x10*/,
      (byte) 37,
      (byte) 11,
      (byte) 28,
      (byte) 229,
      (byte) 32 /*0x20*/,
      (byte) 29,
      (byte) 206,
      (byte) 99,
      (byte) 175,
      (byte) 32 /*0x20*/,
      (byte) 245,
      (byte) 31 /*0x1F*/,
      (byte) 92,
      (byte) 54,
      (byte) 127 /*0x7F*/,
      (byte) 179,
      (byte) 210,
      (byte) 214,
      (byte) 100,
      (byte) 2,
      (byte) 113,
      (byte) 25,
      (byte) 48 /*0x30*/,
      (byte) 25,
      (byte) 139,
      (byte) 50,
      (byte) 219,
      (byte) 102
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 43,
      (byte) 215,
      (byte) 19,
      (byte) 104,
      (byte) 240 /*0xF0*/,
      (byte) 225,
      (byte) 5,
      (byte) 46,
      (byte) 111,
      (byte) 112 /*0x70*/,
      (byte) 173,
      (byte) 135,
      (byte) 79,
      (byte) 114,
      (byte) 225,
      (byte) 46,
      (byte) 46,
      (byte) 161,
      (byte) 191,
      (byte) 10,
      (byte) 67,
      (byte) 26,
      (byte) 153,
      (byte) 146,
      (byte) 40,
      (byte) 178,
      (byte) 240 /*0xF0*/,
      (byte) 184,
      (byte) 113,
      (byte) 229,
      (byte) 81,
      (byte) 85,
      (byte) 125,
      (byte) 67,
      (byte) 204,
      (byte) 170,
      (byte) 142,
      (byte) 63 /*0x3F*/,
      (byte) 35,
      (byte) 45,
      (byte) 221,
      (byte) 136,
      (byte) 188,
      (byte) 159,
      (byte) 231,
      (byte) 174,
      (byte) 103,
      (byte) 100
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[28];
    byte[] response2 = new byte[28];
    Array.Copy((Array) sc_12366.sspq, 0, (Array) numArray2, 0, 28);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12366.sspr, 0, (Array) numArray2, 0, 28);
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

  internal static string ssp_appserver_12371()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 104,
        (byte) 71,
        (byte) 105,
        (byte) 1,
        (byte) 45,
        (byte) 118,
        (byte) 47,
        (byte) 129
      };
      byte[] numArray3 = new byte[8]
      {
        (byte) 153,
        (byte) 12,
        (byte) 30,
        (byte) 35,
        (byte) 78,
        (byte) 220,
        (byte) 233,
        (byte) 10
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_12366.sspq, 28, (Array) numArray4, 0, 49);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12366.sspr, 28, (Array) numArray4, 0, 49);
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
    byte[] numArray5 = new byte[8];
    byte[] numArray6 = new byte[8];
    numArray6[6] = (byte) 175;
    numArray6[1] = (byte) 215;
    numArray6[2] = (byte) 90;
    numArray6[0] = (byte) 162;
    numArray6[5] = (byte) 219;
    numArray6[4] = (byte) 178;
    numArray6[3] = (byte) 4;
    numArray6[7] = (byte) 230;
    byte[] numArray7 = new byte[8];
    numArray7[5] = (byte) 82;
    numArray7[1] = (byte) 3;
    numArray7[7] = (byte) 87;
    numArray7[3] = (byte) 75;
    numArray7[4] = (byte) 27;
    numArray7[0] = (byte) 250;
    numArray7[6] = (byte) 205;
    numArray7[2] = (byte) 93;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12372(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 222,
      (byte) 211,
      (byte) 83,
      (byte) 78,
      (byte) 109,
      (byte) 109,
      (byte) 173,
      (byte) 111,
      (byte) 22,
      (byte) 137,
      (byte) 158,
      (byte) 85,
      (byte) 75,
      (byte) 209,
      (byte) 117,
      (byte) 124,
      (byte) 252,
      (byte) 24,
      (byte) 93,
      (byte) 178,
      (byte) 28,
      (byte) 101,
      (byte) 183,
      (byte) 91,
      (byte) 90,
      (byte) 51,
      (byte) 236,
      (byte) 94,
      (byte) 180,
      (byte) 174,
      (byte) 208 /*0xD0*/,
      (byte) 34,
      (byte) 150,
      (byte) 161,
      (byte) 14,
      (byte) 96 /*0x60*/,
      (byte) 183,
      (byte) 147,
      (byte) 116,
      (byte) 97,
      (byte) 169,
      (byte) 236,
      (byte) 7,
      (byte) 232,
      (byte) 35,
      (byte) 154,
      (byte) 35
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 15;
    sourceArray2[1] = (byte) 163;
    sourceArray2[15] = (byte) 219;
    sourceArray2[3] = (byte) 15;
    sourceArray2[4] = (byte) 213;
    sourceArray2[14] = (byte) 213;
    sourceArray2[6] = (byte) 125;
    sourceArray2[7] = (byte) 108;
    sourceArray2[8] = (byte) 203;
    sourceArray2[2] = (byte) 202;
    sourceArray2[44] = (byte) 20;
    sourceArray2[11] = (byte) 157;
    sourceArray2[22] = (byte) 107;
    sourceArray2[13] = (byte) 163;
    sourceArray2[12] = (byte) 94;
    sourceArray2[32 /*0x20*/] = (byte) 217;
    sourceArray2[31 /*0x1F*/] = (byte) 244;
    sourceArray2[17] = (byte) 168;
    sourceArray2[30] = (byte) 6;
    sourceArray2[47] = (byte) 168;
    sourceArray2[40] = (byte) 156;
    sourceArray2[21] = (byte) 35;
    sourceArray2[43] = (byte) 251;
    sourceArray2[23] = (byte) 68;
    sourceArray2[24] = (byte) 209;
    sourceArray2[20] = (byte) 68;
    sourceArray2[26] = (byte) 139;
    sourceArray2[36] = (byte) 253;
    sourceArray2[45] = (byte) 111;
    sourceArray2[29] = (byte) 116;
    sourceArray2[46] = (byte) 196;
    sourceArray2[42] = (byte) 210;
    sourceArray2[10] = (byte) 224 /*0xE0*/;
    sourceArray2[33] = (byte) 223;
    sourceArray2[25] = (byte) 205;
    sourceArray2[35] = (byte) 195;
    sourceArray2[38] = (byte) 120;
    sourceArray2[16 /*0x10*/] = (byte) 221;
    sourceArray2[27] = (byte) 237;
    sourceArray2[39] = (byte) 208 /*0xD0*/;
    sourceArray2[0] = (byte) 207;
    sourceArray2[41] = (byte) 2;
    sourceArray2[34] = (byte) 152;
    sourceArray2[18] = (byte) 0;
    sourceArray2[19] = (byte) 106;
    sourceArray2[9] = (byte) 112 /*0x70*/;
    sourceArray2[5] = (byte) 58;
    sourceArray2[37] = (byte) 208 /*0xD0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12373(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 218,
      (byte) 121,
      (byte) 86,
      (byte) 114,
      (byte) 14,
      (byte) 159,
      (byte) 77,
      (byte) 51,
      (byte) 147,
      (byte) 229,
      (byte) 26,
      (byte) 75,
      (byte) 191,
      (byte) 51,
      (byte) 42,
      (byte) 174,
      (byte) 119,
      (byte) 216,
      (byte) 194,
      (byte) 217,
      (byte) 136,
      (byte) 198,
      (byte) 31 /*0x1F*/,
      (byte) 45,
      (byte) 207,
      (byte) 4,
      (byte) 201,
      (byte) 106,
      (byte) 28,
      (byte) 175,
      (byte) 254,
      (byte) 190,
      (byte) 160 /*0xA0*/,
      (byte) 143,
      (byte) 229,
      (byte) 150,
      (byte) 123,
      (byte) 101,
      (byte) 211,
      (byte) 6,
      (byte) 145,
      (byte) 191,
      (byte) 100,
      (byte) 165,
      (byte) 122,
      (byte) 248,
      (byte) 1,
      (byte) 107
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 58;
    sourceArray2[1] = (byte) 112 /*0x70*/;
    sourceArray2[37] = (byte) 178;
    sourceArray2[3] = (byte) 211;
    sourceArray2[19] = (byte) 175;
    sourceArray2[5] = (byte) 223;
    sourceArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    sourceArray2[45] = (byte) 224 /*0xE0*/;
    sourceArray2[8] = (byte) 193;
    sourceArray2[0] = (byte) 39;
    sourceArray2[13] = (byte) 198;
    sourceArray2[11] = (byte) 106;
    sourceArray2[32 /*0x20*/] = (byte) 58;
    sourceArray2[26] = (byte) 238;
    sourceArray2[41] = (byte) 204;
    sourceArray2[15] = (byte) 83;
    sourceArray2[16 /*0x10*/] = (byte) 60;
    sourceArray2[25] = (byte) 234;
    sourceArray2[18] = (byte) 166;
    sourceArray2[39] = (byte) 91;
    sourceArray2[20] = (byte) 48 /*0x30*/;
    sourceArray2[44] = (byte) 252;
    sourceArray2[22] = (byte) 165;
    sourceArray2[23] = (byte) 172;
    sourceArray2[34] = (byte) 229;
    sourceArray2[14] = (byte) 29;
    sourceArray2[38] = (byte) 200;
    sourceArray2[7] = (byte) 163;
    sourceArray2[28] = (byte) 103;
    sourceArray2[10] = (byte) 185;
    sourceArray2[30] = (byte) 50;
    sourceArray2[17] = (byte) 210;
    sourceArray2[21] = (byte) 40;
    sourceArray2[33] = (byte) 197;
    sourceArray2[9] = (byte) 93;
    sourceArray2[35] = (byte) 104;
    sourceArray2[36] = (byte) 151;
    sourceArray2[27] = (byte) 117;
    sourceArray2[24] = (byte) 52;
    sourceArray2[43] = (byte) 205;
    sourceArray2[40] = (byte) 55;
    sourceArray2[12] = (byte) 253;
    sourceArray2[4] = (byte) 162;
    sourceArray2[42] = (byte) 153;
    sourceArray2[2] = (byte) 39;
    sourceArray2[6] = (byte) 129;
    sourceArray2[46] = (byte) 45;
    sourceArray2[47] = (byte) 14;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12374(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 49;
    sourceArray1[1] = (byte) 29;
    sourceArray1[14] = (byte) 119;
    sourceArray1[11] = (byte) 118;
    sourceArray1[12] = (byte) 204;
    sourceArray1[20] = (byte) 97;
    sourceArray1[45] = (byte) 220;
    sourceArray1[7] = (byte) 13;
    sourceArray1[44] = (byte) 91;
    sourceArray1[32 /*0x20*/] = (byte) 149;
    sourceArray1[42] = (byte) 26;
    sourceArray1[21] = (byte) 191;
    sourceArray1[30] = (byte) 224 /*0xE0*/;
    sourceArray1[13] = (byte) 6;
    sourceArray1[36] = (byte) 224 /*0xE0*/;
    sourceArray1[10] = (byte) 224 /*0xE0*/;
    sourceArray1[16 /*0x10*/] = (byte) 43;
    sourceArray1[8] = (byte) 86;
    sourceArray1[18] = (byte) 65;
    sourceArray1[19] = (byte) 123;
    sourceArray1[0] = (byte) 209;
    sourceArray1[24] = (byte) 224 /*0xE0*/;
    sourceArray1[33] = (byte) 137;
    sourceArray1[23] = (byte) 87;
    sourceArray1[15] = (byte) 136;
    sourceArray1[17] = (byte) 23;
    sourceArray1[9] = (byte) 179;
    sourceArray1[27] = (byte) 139;
    sourceArray1[28] = (byte) 153;
    sourceArray1[26] = (byte) 58;
    sourceArray1[3] = (byte) 253;
    sourceArray1[31 /*0x1F*/] = (byte) 186;
    sourceArray1[2] = (byte) 122;
    sourceArray1[47] = (byte) 194;
    sourceArray1[22] = (byte) 124;
    sourceArray1[6] = (byte) 33;
    sourceArray1[4] = (byte) 208 /*0xD0*/;
    sourceArray1[35] = (byte) 63 /*0x3F*/;
    sourceArray1[38] = (byte) 251;
    sourceArray1[39] = (byte) 80 /*0x50*/;
    sourceArray1[40] = (byte) 191;
    sourceArray1[41] = (byte) 60;
    sourceArray1[37] = (byte) 237;
    sourceArray1[43] = (byte) 30;
    sourceArray1[25] = (byte) 79;
    sourceArray1[5] = (byte) 84;
    sourceArray1[46] = (byte) 246;
    sourceArray1[29] = (byte) 32 /*0x20*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 107,
      (byte) 168,
      (byte) 34,
      (byte) 158,
      (byte) 239,
      (byte) 68,
      (byte) 140,
      (byte) 177,
      (byte) 40,
      (byte) 248,
      (byte) 87,
      (byte) 90,
      (byte) 4,
      (byte) 111,
      (byte) 82,
      (byte) 94,
      (byte) 200,
      (byte) 49,
      (byte) 252,
      (byte) 198,
      (byte) 204,
      (byte) 21,
      (byte) 202,
      (byte) 161,
      (byte) 93,
      (byte) 148,
      (byte) 65,
      (byte) 106,
      (byte) 107,
      (byte) 4,
      (byte) 169,
      (byte) 37,
      (byte) 213,
      (byte) 93,
      (byte) 174,
      (byte) 174,
      (byte) 33,
      (byte) 206,
      (byte) 169,
      (byte) 222,
      (byte) 42,
      (byte) 210,
      (byte) 219,
      (byte) 113,
      (byte) 183,
      (byte) 254,
      (byte) 178,
      (byte) 20
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_12366.sspq, 77, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12366.sspr, 77, (Array) numArray2, 0, 37);
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

  internal static int ssp_appserver_12375(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 98,
      (byte) 42,
      (byte) 177,
      (byte) 12,
      (byte) 215,
      (byte) 177,
      (byte) 92,
      (byte) 150,
      (byte) 184,
      (byte) 81,
      (byte) 148,
      (byte) 102,
      (byte) 198,
      (byte) 81,
      (byte) 241,
      (byte) 74,
      (byte) 13,
      (byte) 15,
      (byte) 55,
      (byte) 232,
      (byte) 147,
      (byte) 211,
      (byte) 171,
      (byte) 208 /*0xD0*/,
      (byte) 83,
      (byte) 104,
      (byte) 172,
      (byte) 47,
      (byte) 95,
      (byte) 15,
      (byte) 132,
      (byte) 23,
      (byte) 113,
      (byte) 12,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 56,
      (byte) 225,
      (byte) 204,
      (byte) 4,
      (byte) 60,
      (byte) 84,
      (byte) 179,
      (byte) 80 /*0x50*/,
      (byte) 38,
      (byte) 44,
      (byte) 151,
      (byte) 111
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 30,
      (byte) 60,
      (byte) 71,
      (byte) 28,
      (byte) 40,
      (byte) 200,
      (byte) 56,
      (byte) 69,
      (byte) 46,
      (byte) 76,
      (byte) 74,
      (byte) 14,
      (byte) 225,
      (byte) 0,
      (byte) 59,
      (byte) 102,
      (byte) 16 /*0x10*/,
      (byte) 191,
      (byte) 90,
      (byte) 113,
      (byte) 180,
      (byte) 103,
      (byte) 121,
      (byte) 23,
      (byte) 228,
      (byte) 219,
      (byte) 83,
      (byte) 29,
      (byte) 169,
      (byte) 138,
      (byte) 61,
      (byte) 144 /*0x90*/,
      (byte) 105,
      (byte) 36,
      (byte) 233,
      (byte) 184,
      (byte) 198,
      (byte) 195,
      (byte) 29,
      (byte) 33,
      (byte) 168,
      (byte) 221,
      (byte) 101,
      (byte) 6,
      (byte) 178,
      (byte) 136,
      (byte) 216,
      (byte) 207
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12376(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 105,
      (byte) 96 /*0x60*/,
      (byte) 143,
      (byte) 126,
      (byte) 225,
      (byte) 185,
      (byte) 111,
      (byte) 5,
      (byte) 44,
      (byte) 55,
      (byte) 169,
      (byte) 204,
      (byte) 125,
      (byte) 244,
      (byte) 185,
      (byte) 33,
      (byte) 193,
      (byte) 17,
      (byte) 43,
      (byte) 66,
      (byte) 51,
      (byte) 171,
      (byte) 63 /*0x3F*/,
      (byte) 31 /*0x1F*/,
      (byte) 91,
      (byte) 169,
      (byte) 38,
      (byte) 209,
      (byte) 60,
      (byte) 14,
      (byte) 226,
      (byte) 18,
      (byte) 239,
      (byte) 196,
      (byte) 8,
      (byte) 171,
      (byte) 160 /*0xA0*/,
      (byte) 177,
      (byte) 87,
      (byte) 40,
      (byte) 93,
      byte.MaxValue,
      (byte) 16 /*0x10*/,
      (byte) 29,
      (byte) 10,
      (byte) 131,
      (byte) 169,
      (byte) 55
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[23] = (byte) 192 /*0xC0*/;
    sourceArray2[15] = (byte) 60;
    sourceArray2[39] = (byte) 8;
    sourceArray2[38] = (byte) 74;
    sourceArray2[41] = (byte) 59;
    sourceArray2[5] = (byte) 108;
    sourceArray2[6] = (byte) 100;
    sourceArray2[27] = (byte) 112 /*0x70*/;
    sourceArray2[33] = (byte) 163;
    sourceArray2[1] = (byte) 101;
    sourceArray2[10] = (byte) 159;
    sourceArray2[11] = (byte) 87;
    sourceArray2[12] = (byte) 73;
    sourceArray2[20] = (byte) 37;
    sourceArray2[14] = (byte) 164;
    sourceArray2[7] = (byte) 74;
    sourceArray2[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    sourceArray2[17] = (byte) 49;
    sourceArray2[40] = (byte) 5;
    sourceArray2[19] = (byte) 147;
    sourceArray2[35] = (byte) 80 /*0x50*/;
    sourceArray2[24] = (byte) 68;
    sourceArray2[22] = (byte) 194;
    sourceArray2[30] = (byte) 202;
    sourceArray2[25] = (byte) 24;
    sourceArray2[21] = (byte) 190;
    sourceArray2[26] = (byte) 241;
    sourceArray2[3] = (byte) 231;
    sourceArray2[31 /*0x1F*/] = (byte) 53;
    sourceArray2[29] = (byte) 65;
    sourceArray2[46] = (byte) 70;
    sourceArray2[0] = (byte) 31 /*0x1F*/;
    sourceArray2[2] = (byte) 226;
    sourceArray2[8] = (byte) 109;
    sourceArray2[34] = (byte) 15;
    sourceArray2[9] = (byte) 126;
    sourceArray2[36] = (byte) 107;
    sourceArray2[37] = (byte) 31 /*0x1F*/;
    sourceArray2[32 /*0x20*/] = (byte) 0;
    sourceArray2[45] = (byte) 0;
    sourceArray2[4] = (byte) 51;
    sourceArray2[47] = (byte) 170;
    sourceArray2[44] = (byte) 145;
    sourceArray2[43] = (byte) 189;
    sourceArray2[13] = (byte) 152;
    sourceArray2[42] = (byte) 130;
    sourceArray2[18] = (byte) 62;
    sourceArray2[28] = (byte) 13;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12377(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 241,
      (byte) 74,
      (byte) 207,
      (byte) 206,
      (byte) 79,
      (byte) 177,
      (byte) 205,
      (byte) 108,
      (byte) 235,
      (byte) 35,
      (byte) 254,
      (byte) 91,
      (byte) 74,
      (byte) 56,
      (byte) 163,
      (byte) 170,
      (byte) 218,
      (byte) 110,
      (byte) 222,
      (byte) 84,
      (byte) 63 /*0x3F*/,
      (byte) 18,
      (byte) 162,
      (byte) 131,
      (byte) 80 /*0x50*/,
      (byte) 128 /*0x80*/,
      (byte) 154,
      (byte) 109,
      (byte) 39,
      (byte) 253,
      (byte) 168,
      (byte) 90,
      (byte) 161,
      (byte) 49,
      (byte) 13,
      (byte) 164,
      (byte) 36,
      (byte) 31 /*0x1F*/,
      (byte) 95,
      (byte) 197,
      (byte) 116,
      (byte) 49,
      (byte) 185,
      (byte) 38,
      (byte) 140,
      (byte) 213,
      (byte) 68,
      (byte) 230
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[30] = (byte) 125;
    sourceArray2[15] = (byte) 231;
    sourceArray2[45] = (byte) 191;
    sourceArray2[14] = (byte) 153;
    sourceArray2[5] = (byte) 230;
    sourceArray2[8] = (byte) 203;
    sourceArray2[33] = (byte) 82;
    sourceArray2[47] = (byte) 188;
    sourceArray2[23] = (byte) 6;
    sourceArray2[29] = (byte) 102;
    sourceArray2[10] = (byte) 16 /*0x10*/;
    sourceArray2[42] = (byte) 149;
    sourceArray2[12] = (byte) 59;
    sourceArray2[13] = (byte) 246;
    sourceArray2[32 /*0x20*/] = (byte) 111;
    sourceArray2[19] = (byte) 33;
    sourceArray2[35] = (byte) 135;
    sourceArray2[17] = (byte) 37;
    sourceArray2[37] = (byte) 88;
    sourceArray2[6] = (byte) 84;
    sourceArray2[20] = (byte) 195;
    sourceArray2[7] = (byte) 170;
    sourceArray2[22] = (byte) 142;
    sourceArray2[1] = (byte) 171;
    sourceArray2[24] = (byte) 68;
    sourceArray2[25] = (byte) 144 /*0x90*/;
    sourceArray2[26] = (byte) 220;
    sourceArray2[27] = (byte) 184;
    sourceArray2[3] = (byte) 35;
    sourceArray2[44] = (byte) 119;
    sourceArray2[41] = (byte) 72;
    sourceArray2[9] = (byte) 108;
    sourceArray2[46] = (byte) 180;
    sourceArray2[11] = (byte) 35;
    sourceArray2[28] = (byte) 230;
    sourceArray2[31 /*0x1F*/] = (byte) 177;
    sourceArray2[36] = (byte) 41;
    sourceArray2[21] = (byte) 185;
    sourceArray2[0] = (byte) 105;
    sourceArray2[39] = (byte) 59;
    sourceArray2[40] = (byte) 119;
    sourceArray2[38] = (byte) 162;
    sourceArray2[18] = (byte) 17;
    sourceArray2[43] = (byte) 142;
    sourceArray2[34] = (byte) 210;
    sourceArray2[2] = (byte) 254;
    sourceArray2[16 /*0x10*/] = (byte) 75;
    sourceArray2[4] = (byte) 194;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[40];
    byte[] response2 = new byte[40];
    Array.Copy((Array) sc_12366.sspq, 114, (Array) numArray2, 0, 40);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12366.sspr, 114, (Array) numArray2, 0, 40);
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

  internal static int ssp_appserver_12378(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 67,
      (byte) 113,
      (byte) 169,
      (byte) 207,
      (byte) 130,
      (byte) 159,
      (byte) 253,
      (byte) 74,
      (byte) 49,
      (byte) 187,
      (byte) 126,
      (byte) 85,
      (byte) 183,
      (byte) 228,
      (byte) 218,
      (byte) 191,
      (byte) 239,
      (byte) 223,
      (byte) 135,
      (byte) 244,
      (byte) 101,
      (byte) 115,
      (byte) 54,
      (byte) 247,
      (byte) 91,
      (byte) 73,
      (byte) 163,
      (byte) 128 /*0x80*/,
      (byte) 107,
      (byte) 90,
      (byte) 170,
      (byte) 76,
      (byte) 166,
      (byte) 145,
      (byte) 243,
      (byte) 222,
      (byte) 82,
      (byte) 51,
      (byte) 44,
      (byte) 121,
      (byte) 130,
      (byte) 32 /*0x20*/,
      (byte) 153,
      (byte) 102,
      (byte) 15,
      (byte) 122,
      (byte) 84,
      (byte) 123
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 77;
    sourceArray2[30] = (byte) 239;
    sourceArray2[47] = (byte) 115;
    sourceArray2[29] = (byte) 4;
    sourceArray2[4] = (byte) 137;
    sourceArray2[37] = (byte) 130;
    sourceArray2[6] = (byte) 76;
    sourceArray2[27] = (byte) 249;
    sourceArray2[2] = (byte) 226;
    sourceArray2[19] = (byte) 71;
    sourceArray2[22] = (byte) 142;
    sourceArray2[11] = (byte) 3;
    sourceArray2[12] = (byte) 37;
    sourceArray2[31 /*0x1F*/] = (byte) 90;
    sourceArray2[10] = (byte) 198;
    sourceArray2[15] = (byte) 37;
    sourceArray2[21] = (byte) 81;
    sourceArray2[28] = (byte) 142;
    sourceArray2[9] = (byte) 18;
    sourceArray2[3] = (byte) 27;
    sourceArray2[0] = (byte) 235;
    sourceArray2[23] = (byte) 93;
    sourceArray2[7] = (byte) 237;
    sourceArray2[20] = (byte) 74;
    sourceArray2[24] = (byte) 183;
    sourceArray2[25] = (byte) 176 /*0xB0*/;
    sourceArray2[26] = (byte) 145;
    sourceArray2[42] = (byte) 7;
    sourceArray2[17] = (byte) 250;
    sourceArray2[35] = (byte) 224 /*0xE0*/;
    sourceArray2[44] = (byte) 181;
    sourceArray2[41] = (byte) 248;
    sourceArray2[8] = (byte) 116;
    sourceArray2[18] = (byte) 145;
    sourceArray2[34] = (byte) 118;
    sourceArray2[32 /*0x20*/] = (byte) 98;
    sourceArray2[36] = (byte) 54;
    sourceArray2[1] = (byte) 43;
    sourceArray2[38] = (byte) 218;
    sourceArray2[39] = (byte) 225;
    sourceArray2[40] = (byte) 214;
    sourceArray2[14] = (byte) 68;
    sourceArray2[13] = (byte) 230;
    sourceArray2[43] = (byte) 225;
    sourceArray2[5] = (byte) 43;
    sourceArray2[45] = (byte) 143;
    sourceArray2[33] = (byte) 8;
    sourceArray2[46] = (byte) 37;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12379(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[22] = (byte) 205;
    sourceArray1[41] = (byte) 161;
    sourceArray1[1] = (byte) 138;
    sourceArray1[3] = (byte) 202;
    sourceArray1[4] = (byte) 221;
    sourceArray1[46] = (byte) 44;
    sourceArray1[6] = (byte) 106;
    sourceArray1[26] = (byte) 191;
    sourceArray1[32 /*0x20*/] = (byte) 9;
    sourceArray1[9] = (byte) 245;
    sourceArray1[10] = (byte) 106;
    sourceArray1[13] = (byte) 67;
    sourceArray1[45] = (byte) 213;
    sourceArray1[0] = (byte) 46;
    sourceArray1[14] = (byte) 212;
    sourceArray1[5] = (byte) 243;
    sourceArray1[2] = (byte) 58;
    sourceArray1[17] = (byte) 22;
    sourceArray1[18] = (byte) 12;
    sourceArray1[25] = (byte) 253;
    sourceArray1[30] = (byte) 24;
    sourceArray1[21] = (byte) 208 /*0xD0*/;
    sourceArray1[47] = (byte) 175;
    sourceArray1[23] = (byte) 88;
    sourceArray1[24] = (byte) 205;
    sourceArray1[35] = (byte) 157;
    sourceArray1[11] = (byte) 202;
    sourceArray1[27] = (byte) 145;
    sourceArray1[28] = (byte) 109;
    sourceArray1[37] = (byte) 105;
    sourceArray1[19] = (byte) 94;
    sourceArray1[31 /*0x1F*/] = (byte) 121;
    sourceArray1[8] = (byte) 247;
    sourceArray1[33] = (byte) 21;
    sourceArray1[34] = (byte) 198;
    sourceArray1[29] = (byte) 3;
    sourceArray1[36] = (byte) 13;
    sourceArray1[43] = (byte) 46;
    sourceArray1[16 /*0x10*/] = (byte) 171;
    sourceArray1[39] = (byte) 16 /*0x10*/;
    sourceArray1[38] = (byte) 227;
    sourceArray1[15] = (byte) 52;
    sourceArray1[40] = (byte) 105;
    sourceArray1[20] = (byte) 196;
    sourceArray1[44] = (byte) 57;
    sourceArray1[42] = (byte) 94;
    sourceArray1[12] = (byte) 146;
    sourceArray1[7] = (byte) 73;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 16 /*0x10*/,
      (byte) 168,
      (byte) 159,
      (byte) 248,
      (byte) 239,
      (byte) 74,
      (byte) 133,
      (byte) 62,
      (byte) 147,
      (byte) 109,
      (byte) 21,
      (byte) 29,
      (byte) 138,
      (byte) 5,
      (byte) 150,
      (byte) 186,
      (byte) 135,
      (byte) 91,
      (byte) 24,
      (byte) 225,
      (byte) 213,
      (byte) 74,
      (byte) 59,
      (byte) 222,
      (byte) 19,
      (byte) 250,
      (byte) 163,
      (byte) 169,
      (byte) 245,
      (byte) 26,
      (byte) 112 /*0x70*/,
      (byte) 237,
      (byte) 77,
      (byte) 204,
      (byte) 57,
      (byte) 203,
      (byte) 91,
      (byte) 45,
      (byte) 36,
      (byte) 174,
      (byte) 158,
      (byte) 128 /*0x80*/,
      (byte) 158,
      (byte) 224 /*0xE0*/,
      (byte) 43,
      (byte) 76,
      (byte) 72,
      (byte) 199
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12380(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 175,
      (byte) 50,
      (byte) 114,
      (byte) 170,
      (byte) 82,
      (byte) 121,
      (byte) 77,
      (byte) 138,
      (byte) 227,
      (byte) 235,
      (byte) 30,
      (byte) 194,
      (byte) 4,
      (byte) 15,
      (byte) 36,
      (byte) 239,
      (byte) 63 /*0x3F*/,
      (byte) 132,
      (byte) 112 /*0x70*/,
      (byte) 192 /*0xC0*/,
      (byte) 54,
      (byte) 110,
      (byte) 21,
      (byte) 85,
      (byte) 250,
      (byte) 214,
      (byte) 46,
      (byte) 31 /*0x1F*/,
      (byte) 100,
      (byte) 94,
      (byte) 7,
      (byte) 77,
      (byte) 215,
      (byte) 228,
      (byte) 32 /*0x20*/,
      (byte) 221,
      (byte) 118,
      (byte) 242,
      (byte) 27,
      (byte) 242,
      (byte) 226,
      byte.MaxValue,
      (byte) 43,
      (byte) 124,
      (byte) 194,
      (byte) 105,
      (byte) 103,
      (byte) 244
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 93,
      (byte) 184,
      (byte) 137,
      (byte) 11,
      (byte) 10,
      (byte) 243,
      (byte) 195,
      (byte) 7,
      (byte) 166,
      (byte) 112 /*0x70*/,
      (byte) 144 /*0x90*/,
      (byte) 45,
      (byte) 242,
      (byte) 141,
      (byte) 246,
      (byte) 189,
      (byte) 20,
      (byte) 208 /*0xD0*/,
      (byte) 158,
      (byte) 42,
      (byte) 221,
      (byte) 254,
      (byte) 133,
      (byte) 158,
      (byte) 113,
      (byte) 92,
      (byte) 245,
      (byte) 61,
      (byte) 97,
      (byte) 3,
      (byte) 132,
      (byte) 168,
      (byte) 31 /*0x1F*/,
      (byte) 209,
      (byte) 145,
      (byte) 246,
      (byte) 51,
      (byte) 30,
      (byte) 234,
      (byte) 125,
      (byte) 209,
      (byte) 224 /*0xE0*/,
      (byte) 219,
      (byte) 74,
      (byte) 111,
      (byte) 188,
      (byte) 117
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12381(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 136,
      (byte) 221,
      (byte) 65,
      (byte) 29,
      (byte) 29,
      (byte) 176 /*0xB0*/,
      (byte) 123,
      (byte) 75,
      (byte) 45,
      (byte) 38,
      (byte) 132,
      (byte) 228,
      (byte) 208 /*0xD0*/,
      (byte) 235,
      (byte) 32 /*0x20*/,
      (byte) 239,
      (byte) 71,
      (byte) 121,
      (byte) 77,
      (byte) 104,
      (byte) 126,
      (byte) 222,
      (byte) 54,
      (byte) 66,
      (byte) 152,
      (byte) 146,
      (byte) 144 /*0x90*/,
      (byte) 110,
      (byte) 58,
      (byte) 104,
      (byte) 194,
      (byte) 216,
      (byte) 32 /*0x20*/,
      (byte) 109,
      (byte) 79,
      (byte) 6,
      (byte) 222,
      (byte) 191,
      (byte) 89,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 167,
      (byte) 180,
      (byte) 217,
      (byte) 167,
      (byte) 152,
      (byte) 242,
      (byte) 126
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[31 /*0x1F*/] = (byte) 34;
    sourceArray2[11] = (byte) 47;
    sourceArray2[20] = (byte) 252;
    sourceArray2[3] = (byte) 58;
    sourceArray2[6] = (byte) 144 /*0x90*/;
    sourceArray2[5] = (byte) 133;
    sourceArray2[1] = (byte) 107;
    sourceArray2[9] = (byte) 56;
    sourceArray2[32 /*0x20*/] = (byte) 223;
    sourceArray2[19] = (byte) 253;
    sourceArray2[15] = (byte) 244;
    sourceArray2[24] = (byte) 240 /*0xF0*/;
    sourceArray2[12] = (byte) 31 /*0x1F*/;
    sourceArray2[10] = (byte) 234;
    sourceArray2[46] = (byte) 63 /*0x3F*/;
    sourceArray2[29] = (byte) 40;
    sourceArray2[25] = (byte) 110;
    sourceArray2[17] = (byte) 180;
    sourceArray2[43] = (byte) 168;
    sourceArray2[45] = (byte) 165;
    sourceArray2[37] = (byte) 206;
    sourceArray2[21] = (byte) 95;
    sourceArray2[4] = (byte) 144 /*0x90*/;
    sourceArray2[2] = (byte) 28;
    sourceArray2[22] = (byte) 218;
    sourceArray2[30] = (byte) 205;
    sourceArray2[41] = (byte) 118;
    sourceArray2[0] = (byte) 65;
    sourceArray2[28] = (byte) 120;
    sourceArray2[7] = (byte) 162;
    sourceArray2[16 /*0x10*/] = (byte) 97;
    sourceArray2[8] = (byte) 214;
    sourceArray2[23] = (byte) 71;
    sourceArray2[13] = (byte) 117;
    sourceArray2[34] = (byte) 125;
    sourceArray2[35] = (byte) 15;
    sourceArray2[36] = (byte) 173;
    sourceArray2[26] = (byte) 185;
    sourceArray2[38] = (byte) 157;
    sourceArray2[39] = (byte) 104;
    sourceArray2[40] = (byte) 174;
    sourceArray2[33] = (byte) 241;
    sourceArray2[42] = (byte) 128 /*0x80*/;
    sourceArray2[18] = (byte) 42;
    sourceArray2[44] = (byte) 92;
    sourceArray2[14] = (byte) 63 /*0x3F*/;
    sourceArray2[27] = (byte) 198;
    sourceArray2[47] = (byte) 25;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12382(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[13] = (byte) 234;
    sourceArray1[40] = (byte) 115;
    sourceArray1[2] = (byte) 45;
    sourceArray1[3] = (byte) 244;
    sourceArray1[38] = (byte) 15;
    sourceArray1[5] = (byte) 7;
    sourceArray1[6] = (byte) 45;
    sourceArray1[10] = (byte) 73;
    sourceArray1[14] = (byte) 13;
    sourceArray1[1] = (byte) 226;
    sourceArray1[44] = (byte) 23;
    sourceArray1[30] = (byte) 58;
    sourceArray1[36] = (byte) 249;
    sourceArray1[29] = (byte) 65;
    sourceArray1[42] = (byte) 129;
    sourceArray1[9] = (byte) 128 /*0x80*/;
    sourceArray1[16 /*0x10*/] = (byte) 219;
    sourceArray1[19] = (byte) 11;
    sourceArray1[18] = (byte) 200;
    sourceArray1[47] = (byte) 228;
    sourceArray1[20] = (byte) 32 /*0x20*/;
    sourceArray1[21] = (byte) 86;
    sourceArray1[22] = (byte) 246;
    sourceArray1[8] = (byte) 138;
    sourceArray1[24] = (byte) 48 /*0x30*/;
    sourceArray1[26] = (byte) 81;
    sourceArray1[31 /*0x1F*/] = (byte) 231;
    sourceArray1[27] = (byte) 232;
    sourceArray1[34] = (byte) 16 /*0x10*/;
    sourceArray1[11] = (byte) 100;
    sourceArray1[33] = (byte) 44;
    sourceArray1[4] = (byte) 232;
    sourceArray1[32 /*0x20*/] = (byte) 159;
    sourceArray1[46] = (byte) 182;
    sourceArray1[28] = (byte) 54;
    sourceArray1[39] = (byte) 148;
    sourceArray1[35] = (byte) 47;
    sourceArray1[17] = (byte) 205;
    sourceArray1[0] = (byte) 144 /*0x90*/;
    sourceArray1[7] = (byte) 180;
    sourceArray1[15] = (byte) 229;
    sourceArray1[41] = (byte) 239;
    sourceArray1[37] = (byte) 64 /*0x40*/;
    sourceArray1[43] = (byte) 36;
    sourceArray1[23] = (byte) 185;
    sourceArray1[45] = (byte) 123;
    sourceArray1[25] = (byte) 76;
    sourceArray1[12] = (byte) 132;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 63 /*0x3F*/,
      (byte) 44,
      (byte) 107,
      (byte) 129,
      (byte) 1,
      (byte) 188,
      (byte) 32 /*0x20*/,
      (byte) 42,
      (byte) 53,
      (byte) 9,
      (byte) 28,
      (byte) 28,
      (byte) 93,
      (byte) 227,
      (byte) 46,
      (byte) 37,
      (byte) 102,
      (byte) 189,
      (byte) 66,
      (byte) 181,
      (byte) 228,
      (byte) 65,
      (byte) 109,
      (byte) 129,
      (byte) 26,
      (byte) 240 /*0xF0*/,
      (byte) 216,
      (byte) 154,
      (byte) 252,
      (byte) 172,
      (byte) 170,
      (byte) 19,
      (byte) 90,
      (byte) 220,
      (byte) 111,
      (byte) 128 /*0x80*/,
      (byte) 70,
      (byte) 220,
      (byte) 70,
      (byte) 61,
      (byte) 222,
      (byte) 36,
      (byte) 91,
      (byte) 114,
      (byte) 236,
      (byte) 200,
      (byte) 105
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
