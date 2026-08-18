// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13205
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13205
{
  private static byte[] sspq = new byte[85]
  {
    (byte) 81,
    (byte) 120,
    (byte) 94,
    (byte) 14,
    (byte) 158,
    (byte) 71,
    (byte) 74,
    (byte) 149,
    (byte) 182,
    (byte) 149,
    (byte) 2,
    (byte) 154,
    (byte) 176 /*0xB0*/,
    (byte) 12,
    (byte) 156,
    (byte) 140,
    (byte) 186,
    (byte) 31 /*0x1F*/,
    (byte) 129,
    (byte) 31 /*0x1F*/,
    (byte) 36,
    (byte) 95,
    (byte) 16 /*0x10*/,
    (byte) 227,
    (byte) 183,
    (byte) 172,
    (byte) 119,
    (byte) 121,
    (byte) 38,
    (byte) 107,
    (byte) 140,
    (byte) 201,
    (byte) 190,
    (byte) 38,
    (byte) 31 /*0x1F*/,
    (byte) 66,
    (byte) 150,
    (byte) 96 /*0x60*/,
    (byte) 19,
    (byte) 24,
    (byte) 232,
    (byte) 223,
    (byte) 211,
    (byte) 194,
    (byte) 148,
    (byte) 196,
    (byte) 77,
    (byte) 228,
    (byte) 152,
    (byte) 30,
    (byte) 164,
    (byte) 133,
    (byte) 166,
    (byte) 193,
    (byte) 182,
    (byte) 116,
    (byte) 16 /*0x10*/,
    (byte) 214,
    (byte) 87,
    (byte) 17,
    (byte) 251,
    (byte) 218,
    (byte) 152,
    (byte) 216,
    (byte) 68,
    (byte) 155,
    (byte) 103,
    (byte) 213,
    (byte) 94,
    (byte) 150,
    (byte) 124,
    (byte) 33,
    (byte) 129,
    (byte) 144 /*0x90*/,
    (byte) 221,
    (byte) 145,
    (byte) 177,
    (byte) 165,
    (byte) 208 /*0xD0*/,
    (byte) 204,
    (byte) 70,
    (byte) 135,
    (byte) 176 /*0xB0*/,
    (byte) 193,
    (byte) 2
  };
  private static byte[] sspr = new byte[85]
  {
    (byte) 77,
    (byte) 208 /*0xD0*/,
    (byte) 81,
    (byte) 154,
    (byte) 173,
    (byte) 36,
    (byte) 135,
    (byte) 43,
    (byte) 110,
    (byte) 22,
    (byte) 30,
    (byte) 146,
    (byte) 53,
    (byte) 144 /*0x90*/,
    (byte) 150,
    (byte) 84,
    (byte) 42,
    (byte) 26,
    (byte) 148,
    (byte) 52,
    (byte) 31 /*0x1F*/,
    (byte) 30,
    (byte) 54,
    (byte) 57,
    (byte) 6,
    (byte) 190,
    (byte) 66,
    (byte) 106,
    (byte) 75,
    (byte) 73,
    (byte) 248,
    (byte) 146,
    (byte) 42,
    (byte) 227,
    (byte) 84,
    (byte) 7,
    (byte) 145,
    (byte) 228,
    (byte) 190,
    (byte) 252,
    (byte) 10,
    (byte) 31 /*0x1F*/,
    (byte) 140,
    (byte) 96 /*0x60*/,
    (byte) 156,
    (byte) 9,
    (byte) 1,
    (byte) 22,
    (byte) 213,
    (byte) 171,
    (byte) 14,
    (byte) 7,
    (byte) 163,
    (byte) 115,
    (byte) 85,
    (byte) 227,
    (byte) 73,
    (byte) 98,
    (byte) 67,
    (byte) 179,
    (byte) 68,
    (byte) 17,
    (byte) 157,
    (byte) 212,
    (byte) 72,
    (byte) 189,
    (byte) 16 /*0x10*/,
    (byte) 78,
    (byte) 234,
    (byte) 188,
    (byte) 40,
    (byte) 69,
    (byte) 96 /*0x60*/,
    (byte) 248,
    (byte) 243,
    (byte) 198,
    (byte) 44,
    (byte) 118,
    (byte) 74,
    (byte) 190,
    (byte) 49,
    (byte) 242,
    (byte) 27,
    (byte) 92,
    (byte) 245
  };

  internal static string ssp_appserver_13206()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 7,
        (byte) 49,
        (byte) 250,
        (byte) 48 /*0x30*/,
        (byte) 250,
        (byte) 23,
        (byte) 152,
        (byte) 65,
        (byte) 176 /*0xB0*/,
        (byte) 228,
        (byte) 130,
        (byte) 10,
        (byte) 27,
        (byte) 123,
        (byte) 120,
        (byte) 213,
        (byte) 66,
        (byte) 164,
        (byte) 213,
        (byte) 51,
        (byte) 246,
        (byte) 101,
        (byte) 178,
        (byte) 53,
        (byte) 219,
        byte.MaxValue,
        (byte) 97,
        (byte) 113,
        (byte) 56,
        (byte) 243,
        (byte) 104,
        (byte) 217,
        (byte) 226,
        (byte) 211,
        (byte) 101,
        (byte) 85,
        (byte) 10,
        (byte) 242,
        (byte) 84,
        (byte) 200,
        (byte) 88,
        (byte) 203,
        (byte) 192 /*0xC0*/,
        (byte) 123
      };
      byte[] numArray3 = new byte[44]
      {
        (byte) 19,
        (byte) 150,
        (byte) 129,
        (byte) 174,
        (byte) 62,
        (byte) 159,
        (byte) 223,
        (byte) 244,
        (byte) 160 /*0xA0*/,
        (byte) 194,
        (byte) 71,
        (byte) 89,
        (byte) 245,
        (byte) 191,
        (byte) 58,
        (byte) 254,
        (byte) 242,
        (byte) 240 /*0xF0*/,
        (byte) 107,
        (byte) 111,
        (byte) 192 /*0xC0*/,
        (byte) 129,
        (byte) 44,
        (byte) 220,
        (byte) 62,
        (byte) 234,
        (byte) 152,
        (byte) 35,
        (byte) 57,
        (byte) 206,
        (byte) 3,
        (byte) 95,
        (byte) 88,
        (byte) 38,
        (byte) 56,
        (byte) 144 /*0x90*/,
        (byte) 112 /*0x70*/,
        (byte) 204,
        (byte) 232,
        (byte) 236,
        (byte) 60,
        (byte) 26,
        (byte) 76,
        (byte) 85
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[44];
    byte[] numArray5 = new byte[44]
    {
      (byte) 232,
      (byte) 49,
      (byte) 152,
      (byte) 127 /*0x7F*/,
      (byte) 106,
      (byte) 66,
      (byte) 214,
      (byte) 13,
      (byte) 162,
      (byte) 126,
      (byte) 197,
      (byte) 85,
      (byte) 251,
      (byte) 182,
      (byte) 38,
      (byte) 9,
      (byte) 188,
      (byte) 123,
      (byte) 169,
      (byte) 40,
      (byte) 71,
      (byte) 21,
      (byte) 34,
      (byte) 203,
      (byte) 80 /*0x50*/,
      (byte) 79,
      (byte) 13,
      (byte) 219,
      (byte) 43,
      (byte) 164,
      (byte) 55,
      (byte) 230,
      (byte) 234,
      (byte) 120,
      (byte) 121,
      (byte) 63 /*0x3F*/,
      (byte) 160 /*0xA0*/,
      (byte) 116,
      (byte) 254,
      (byte) 150,
      (byte) 11,
      (byte) 198,
      (byte) 195,
      (byte) 215
    };
    byte[] numArray6 = new byte[44];
    numArray6[21] = (byte) 158;
    numArray6[8] = (byte) 42;
    numArray6[2] = (byte) 59;
    numArray6[20] = (byte) 43;
    numArray6[28] = (byte) 32 /*0x20*/;
    numArray6[22] = (byte) 168;
    numArray6[7] = (byte) 136;
    numArray6[39] = (byte) 14;
    numArray6[4] = (byte) 120;
    numArray6[38] = (byte) 86;
    numArray6[10] = (byte) 214;
    numArray6[11] = (byte) 72;
    numArray6[12] = (byte) 112 /*0x70*/;
    numArray6[41] = (byte) 139;
    numArray6[24] = (byte) 147;
    numArray6[15] = (byte) 176 /*0xB0*/;
    numArray6[16 /*0x10*/] = (byte) 148;
    numArray6[17] = (byte) 97;
    numArray6[18] = (byte) 124;
    numArray6[13] = (byte) 196;
    numArray6[30] = (byte) 198;
    numArray6[43] = (byte) 109;
    numArray6[31 /*0x1F*/] = (byte) 186;
    numArray6[27] = (byte) 14;
    numArray6[14] = (byte) 23;
    numArray6[9] = (byte) 35;
    numArray6[26] = (byte) 246;
    numArray6[29] = (byte) 161;
    numArray6[36] = (byte) 240 /*0xF0*/;
    numArray6[1] = (byte) 242;
    numArray6[25] = (byte) 78;
    numArray6[40] = (byte) 195;
    numArray6[32 /*0x20*/] = (byte) 211;
    numArray6[6] = (byte) 49;
    numArray6[34] = (byte) 117;
    numArray6[35] = (byte) 196;
    numArray6[23] = (byte) 7;
    numArray6[37] = (byte) 191;
    numArray6[0] = (byte) 69;
    numArray6[19] = (byte) 92;
    numArray6[5] = (byte) 114;
    numArray6[3] = (byte) 138;
    numArray6[42] = (byte) 114;
    numArray6[33] = (byte) 206;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13207()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 69;
      numArray2[3] = (byte) 16 /*0x10*/;
      numArray2[2] = (byte) 97;
      numArray2[4] = (byte) 228;
      numArray2[1] = (byte) 227;
      numArray2[5] = (byte) 197;
      numArray2[6] = (byte) 144 /*0x90*/;
      numArray2[7] = (byte) 102;
      numArray2[8] = (byte) 50;
      numArray2[9] = (byte) 214;
      byte[] numArray3 = new byte[10]
      {
        (byte) 97,
        (byte) 46,
        (byte) 11,
        (byte) 26,
        (byte) 192 /*0xC0*/,
        (byte) 89,
        (byte) 131,
        (byte) 205,
        (byte) 129,
        (byte) 149
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[34];
      byte[] response = new byte[34];
      Array.Copy((Array) sc_13205.sspq, 0, (Array) numArray4, 0, 34);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13205.sspr, 0, (Array) numArray4, 0, 34);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10];
    numArray6[4] = (byte) 199;
    numArray6[8] = (byte) 147;
    numArray6[2] = (byte) 103;
    numArray6[3] = (byte) 130;
    numArray6[6] = (byte) 139;
    numArray6[5] = (byte) 234;
    numArray6[1] = (byte) 107;
    numArray6[7] = (byte) 64 /*0x40*/;
    numArray6[9] = (byte) 194;
    numArray6[0] = (byte) 191;
    byte[] numArray7 = new byte[10];
    numArray7[5] = (byte) 188;
    numArray7[8] = (byte) 144 /*0x90*/;
    numArray7[2] = (byte) 179;
    numArray7[0] = (byte) 246;
    numArray7[4] = (byte) 49;
    numArray7[7] = (byte) 56;
    numArray7[6] = (byte) 244;
    numArray7[3] = (byte) 216;
    numArray7[1] = (byte) 16 /*0x10*/;
    numArray7[9] = (byte) 167;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13208()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 76,
        (byte) 49,
        (byte) 37,
        (byte) 56,
        (byte) 87,
        (byte) 151,
        (byte) 148,
        (byte) 228,
        (byte) 203,
        (byte) 77
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 65,
        (byte) 32 /*0x20*/,
        (byte) 142,
        (byte) 110,
        (byte) 158,
        (byte) 20,
        (byte) 90,
        (byte) 220,
        (byte) 204,
        (byte) 40
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[0] = (byte) 215;
    numArray5[5] = (byte) 207;
    numArray5[1] = (byte) 88;
    numArray5[3] = (byte) 98;
    numArray5[4] = (byte) 100;
    numArray5[8] = (byte) 205;
    numArray5[6] = (byte) 185;
    numArray5[9] = (byte) 3;
    numArray5[7] = (byte) 17;
    numArray5[2] = (byte) 51;
    byte[] numArray6 = new byte[10];
    numArray6[0] = (byte) 124;
    numArray6[3] = (byte) 227;
    numArray6[9] = (byte) 171;
    numArray6[6] = (byte) 213;
    numArray6[2] = (byte) 137;
    numArray6[5] = (byte) 246;
    numArray6[1] = (byte) 137;
    numArray6[7] = (byte) 211;
    numArray6[4] = (byte) 111;
    numArray6[8] = (byte) 46;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13209()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[8] = (byte) 127 /*0x7F*/;
      numArray2[1] = (byte) 120;
      numArray2[6] = (byte) 34;
      numArray2[3] = (byte) 232;
      numArray2[4] = (byte) 165;
      numArray2[7] = (byte) 120;
      numArray2[5] = (byte) 104;
      numArray2[0] = (byte) 249;
      numArray2[2] = (byte) 59;
      numArray2[9] = (byte) 45;
      byte[] numArray3 = new byte[10];
      numArray3[5] = (byte) 210;
      numArray3[2] = (byte) 149;
      numArray3[8] = (byte) 203;
      numArray3[3] = (byte) 38;
      numArray3[4] = (byte) 124;
      numArray3[0] = (byte) 116;
      numArray3[6] = (byte) 42;
      numArray3[7] = (byte) 242;
      numArray3[1] = (byte) 30;
      numArray3[9] = (byte) 90;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[51];
      byte[] response = new byte[51];
      Array.Copy((Array) sc_13205.sspq, 34, (Array) numArray4, 0, 51);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13205.sspr, 34, (Array) numArray4, 0, 51);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10]
    {
      (byte) 100,
      (byte) 246,
      (byte) 227,
      (byte) 246,
      (byte) 171,
      (byte) 49,
      (byte) 192 /*0xC0*/,
      (byte) 8,
      (byte) 2,
      (byte) 28
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 98,
      (byte) 117,
      (byte) 216,
      (byte) 182,
      (byte) 146,
      (byte) 217,
      (byte) 72,
      (byte) 174,
      (byte) 69,
      (byte) 211
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
