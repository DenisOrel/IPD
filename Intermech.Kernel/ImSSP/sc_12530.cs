// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12530
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12530
{
  internal static string ssp_appserver_12531()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 243;
      numArray2[1] = (byte) 128 /*0x80*/;
      numArray2[2] = (byte) 236;
      numArray2[6] = (byte) 17;
      numArray2[5] = (byte) 227;
      numArray2[7] = (byte) 45;
      numArray2[3] = (byte) 161;
      numArray2[0] = (byte) 176 /*0xB0*/;
      numArray2[8] = (byte) 154;
      numArray2[9] = (byte) 88;
      byte[] numArray3 = new byte[10]
      {
        (byte) 14,
        (byte) 18,
        (byte) 122,
        (byte) 57,
        (byte) 24,
        (byte) 148,
        (byte) 80 /*0x50*/,
        (byte) 9,
        (byte) 134,
        (byte) 57
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 225,
      (byte) 154,
      (byte) 243,
      (byte) 116,
      (byte) 46,
      (byte) 52,
      (byte) 78,
      (byte) 160 /*0xA0*/,
      (byte) 83,
      (byte) 170
    };
    byte[] numArray6 = new byte[10];
    numArray6[6] = (byte) 233;
    numArray6[1] = (byte) 77;
    numArray6[3] = (byte) 62;
    numArray6[5] = (byte) 56;
    numArray6[2] = (byte) 77;
    numArray6[0] = (byte) 69;
    numArray6[4] = (byte) 69;
    numArray6[7] = byte.MaxValue;
    numArray6[8] = (byte) 160 /*0xA0*/;
    numArray6[9] = (byte) 21;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12532(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 76,
      (byte) 205,
      (byte) 56,
      (byte) 162,
      (byte) 246,
      (byte) 212,
      (byte) 129,
      (byte) 123,
      (byte) 108,
      (byte) 11,
      (byte) 6,
      byte.MaxValue,
      (byte) 111,
      (byte) 227,
      (byte) 248,
      (byte) 76,
      (byte) 183,
      (byte) 43,
      (byte) 195,
      (byte) 243,
      (byte) 136,
      (byte) 114,
      (byte) 196,
      (byte) 41,
      (byte) 7,
      (byte) 39,
      (byte) 218,
      (byte) 97,
      (byte) 150,
      (byte) 80 /*0x50*/,
      (byte) 110,
      (byte) 18,
      (byte) 55,
      (byte) 73,
      (byte) 225,
      (byte) 103,
      (byte) 22,
      (byte) 254,
      (byte) 93,
      (byte) 213,
      (byte) 149,
      (byte) 46,
      (byte) 196,
      (byte) 104,
      (byte) 102,
      (byte) 103,
      (byte) 127 /*0x7F*/,
      (byte) 187
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[18] = (byte) 198;
    sourceArray2[26] = (byte) 10;
    sourceArray2[6] = (byte) 203;
    sourceArray2[3] = (byte) 46;
    sourceArray2[1] = (byte) 170;
    sourceArray2[47] = (byte) 245;
    sourceArray2[34] = (byte) 245;
    sourceArray2[24] = (byte) 31 /*0x1F*/;
    sourceArray2[8] = (byte) 248;
    sourceArray2[9] = (byte) 163;
    sourceArray2[25] = (byte) 52;
    sourceArray2[11] = (byte) 92;
    sourceArray2[12] = (byte) 38;
    sourceArray2[5] = (byte) 159;
    sourceArray2[28] = (byte) 67;
    sourceArray2[15] = (byte) 191;
    sourceArray2[16 /*0x10*/] = (byte) 85;
    sourceArray2[17] = (byte) 196;
    sourceArray2[2] = (byte) 25;
    sourceArray2[19] = (byte) 206;
    sourceArray2[40] = (byte) 202;
    sourceArray2[33] = (byte) 42;
    sourceArray2[0] = (byte) 214;
    sourceArray2[23] = (byte) 181;
    sourceArray2[21] = (byte) 184;
    sourceArray2[41] = (byte) 51;
    sourceArray2[7] = (byte) 157;
    sourceArray2[27] = (byte) 22;
    sourceArray2[10] = (byte) 218;
    sourceArray2[29] = (byte) 31 /*0x1F*/;
    sourceArray2[30] = (byte) 66;
    sourceArray2[44] = (byte) 0;
    sourceArray2[32 /*0x20*/] = (byte) 232;
    sourceArray2[46] = (byte) 39;
    sourceArray2[31 /*0x1F*/] = (byte) 132;
    sourceArray2[35] = (byte) 172;
    sourceArray2[22] = (byte) 31 /*0x1F*/;
    sourceArray2[37] = (byte) 189;
    sourceArray2[38] = (byte) 40;
    sourceArray2[4] = (byte) 19;
    sourceArray2[39] = (byte) 191;
    sourceArray2[13] = (byte) 252;
    sourceArray2[20] = (byte) 241;
    sourceArray2[43] = (byte) 139;
    sourceArray2[36] = (byte) 123;
    sourceArray2[45] = (byte) 74;
    sourceArray2[14] = (byte) 70;
    sourceArray2[42] = (byte) 75;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12533(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 90,
      (byte) 185,
      (byte) 132,
      (byte) 27,
      (byte) 82,
      (byte) 120,
      (byte) 214,
      (byte) 107,
      (byte) 71,
      (byte) 44,
      (byte) 173,
      (byte) 75,
      (byte) 250,
      (byte) 39,
      (byte) 220,
      (byte) 212,
      (byte) 158,
      (byte) 216,
      (byte) 116,
      (byte) 165,
      (byte) 89,
      (byte) 124,
      (byte) 251,
      (byte) 51,
      (byte) 45,
      (byte) 86,
      (byte) 63 /*0x3F*/,
      (byte) 18,
      (byte) 231,
      (byte) 15,
      (byte) 5,
      (byte) 143,
      (byte) 80 /*0x50*/,
      (byte) 1,
      (byte) 77,
      (byte) 100,
      (byte) 199,
      (byte) 42,
      (byte) 240 /*0xF0*/,
      (byte) 175,
      (byte) 119,
      (byte) 2,
      (byte) 96 /*0x60*/,
      (byte) 217,
      (byte) 177,
      (byte) 188,
      (byte) 104,
      (byte) 103
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 34,
      (byte) 183,
      (byte) 182,
      (byte) 142,
      (byte) 146,
      (byte) 73,
      (byte) 55,
      (byte) 204,
      (byte) 46,
      (byte) 208 /*0xD0*/,
      (byte) 3,
      (byte) 163,
      (byte) 71,
      (byte) 160 /*0xA0*/,
      (byte) 103,
      (byte) 217,
      (byte) 79,
      (byte) 193,
      (byte) 243,
      (byte) 152,
      (byte) 243,
      (byte) 117,
      (byte) 79,
      (byte) 100,
      (byte) 80 /*0x50*/,
      (byte) 29,
      (byte) 53,
      (byte) 58,
      (byte) 221,
      (byte) 111,
      (byte) 54,
      (byte) 227,
      (byte) 95,
      (byte) 43,
      (byte) 62,
      (byte) 131,
      (byte) 89,
      (byte) 176 /*0xB0*/,
      (byte) 225,
      (byte) 106,
      (byte) 197,
      (byte) 237,
      (byte) 5,
      (byte) 197,
      (byte) 104,
      (byte) 129,
      (byte) 182,
      (byte) 86
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
