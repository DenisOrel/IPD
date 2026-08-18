// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13015
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13015
{
  private static byte[] sspq = new byte[37]
  {
    (byte) 173,
    (byte) 59,
    (byte) 92,
    (byte) 17,
    (byte) 71,
    (byte) 76,
    (byte) 143,
    (byte) 235,
    (byte) 83,
    (byte) 28,
    (byte) 226,
    (byte) 74,
    (byte) 220,
    (byte) 80 /*0x50*/,
    (byte) 182,
    (byte) 44,
    (byte) 131,
    (byte) 140,
    (byte) 170,
    (byte) 24,
    (byte) 77,
    (byte) 10,
    (byte) 231,
    (byte) 45,
    (byte) 252,
    (byte) 158,
    (byte) 206,
    (byte) 32 /*0x20*/,
    (byte) 163,
    (byte) 69,
    (byte) 121,
    (byte) 172,
    (byte) 48 /*0x30*/,
    (byte) 74,
    (byte) 60,
    (byte) 199,
    (byte) 32 /*0x20*/
  };
  private static byte[] sspr = new byte[37]
  {
    (byte) 203,
    (byte) 38,
    (byte) 71,
    (byte) 156,
    (byte) 114,
    (byte) 29,
    (byte) 231,
    (byte) 47,
    (byte) 146,
    (byte) 176 /*0xB0*/,
    (byte) 188,
    (byte) 208 /*0xD0*/,
    (byte) 127 /*0x7F*/,
    (byte) 150,
    (byte) 198,
    (byte) 62,
    (byte) 143,
    (byte) 26,
    (byte) 238,
    (byte) 11,
    (byte) 1,
    (byte) 109,
    (byte) 93,
    (byte) 51,
    (byte) 4,
    (byte) 66,
    (byte) 200,
    (byte) 146,
    (byte) 206,
    (byte) 124,
    (byte) 154,
    (byte) 67,
    (byte) 214,
    (byte) 44,
    (byte) 28,
    (byte) 28,
    (byte) 95
  };

  internal static string ssp_appserver_13016()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 249;
      numArray2[9] = (byte) 95;
      numArray2[2] = (byte) 118;
      numArray2[1] = (byte) 143;
      numArray2[4] = (byte) 90;
      numArray2[5] = (byte) 244;
      numArray2[6] = (byte) 35;
      numArray2[3] = (byte) 19;
      numArray2[8] = (byte) 130;
      numArray2[7] = (byte) 203;
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 112 /*0x70*/;
      numArray3[2] = (byte) 185;
      numArray3[7] = (byte) 68;
      numArray3[3] = (byte) 248;
      numArray3[4] = (byte) 63 /*0x3F*/;
      numArray3[5] = (byte) 70;
      numArray3[6] = (byte) 134;
      numArray3[0] = (byte) 163;
      numArray3[8] = (byte) 223;
      numArray3[9] = (byte) 69;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 88,
      (byte) 156,
      (byte) 228,
      (byte) 161,
      (byte) 56,
      (byte) 254,
      (byte) 59,
      (byte) 147,
      (byte) 17,
      (byte) 95
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 132,
      (byte) 84,
      (byte) 104,
      (byte) 85,
      (byte) 154,
      (byte) 226,
      (byte) 163,
      (byte) 148,
      (byte) 170,
      (byte) 150
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13017(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[11] = (byte) 94;
    sourceArray1[18] = (byte) 219;
    sourceArray1[2] = (byte) 129;
    sourceArray1[27] = (byte) 141;
    sourceArray1[1] = (byte) 249;
    sourceArray1[5] = (byte) 30;
    sourceArray1[6] = (byte) 124;
    sourceArray1[41] = (byte) 206;
    sourceArray1[8] = (byte) 166;
    sourceArray1[3] = (byte) 197;
    sourceArray1[23] = (byte) 250;
    sourceArray1[4] = (byte) 122;
    sourceArray1[0] = (byte) 133;
    sourceArray1[13] = (byte) 156;
    sourceArray1[35] = (byte) 225;
    sourceArray1[15] = (byte) 8;
    sourceArray1[17] = (byte) 93;
    sourceArray1[33] = (byte) 253;
    sourceArray1[30] = (byte) 170;
    sourceArray1[19] = (byte) 12;
    sourceArray1[7] = (byte) 205;
    sourceArray1[21] = (byte) 240 /*0xF0*/;
    sourceArray1[22] = (byte) 42;
    sourceArray1[46] = (byte) 188;
    sourceArray1[24] = (byte) 138;
    sourceArray1[25] = (byte) 192 /*0xC0*/;
    sourceArray1[10] = (byte) 232;
    sourceArray1[14] = (byte) 240 /*0xF0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 248;
    sourceArray1[16 /*0x10*/] = (byte) 152;
    sourceArray1[45] = (byte) 160 /*0xA0*/;
    sourceArray1[28] = (byte) 204;
    sourceArray1[38] = (byte) 123;
    sourceArray1[36] = (byte) 248;
    sourceArray1[34] = (byte) 1;
    sourceArray1[44] = (byte) 80 /*0x50*/;
    sourceArray1[26] = (byte) 116;
    sourceArray1[37] = (byte) 2;
    sourceArray1[42] = (byte) 61;
    sourceArray1[39] = (byte) 113;
    sourceArray1[40] = (byte) 219;
    sourceArray1[32 /*0x20*/] = (byte) 108;
    sourceArray1[29] = (byte) 111;
    sourceArray1[43] = (byte) 165;
    sourceArray1[9] = (byte) 252;
    sourceArray1[12] = (byte) 126;
    sourceArray1[20] = (byte) 21;
    sourceArray1[47] = (byte) 247;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 248,
      (byte) 172,
      (byte) 226,
      (byte) 0,
      (byte) 142,
      (byte) 166,
      (byte) 227,
      (byte) 160 /*0xA0*/,
      (byte) 54,
      (byte) 201,
      (byte) 108,
      (byte) 83,
      (byte) 233,
      (byte) 106,
      (byte) 134,
      (byte) 203,
      (byte) 233,
      (byte) 238,
      (byte) 252,
      (byte) 30,
      (byte) 147,
      (byte) 208 /*0xD0*/,
      (byte) 67,
      (byte) 225,
      (byte) 120,
      (byte) 236,
      (byte) 7,
      (byte) 121,
      (byte) 99,
      (byte) 159,
      (byte) 203,
      (byte) 123,
      (byte) 2,
      (byte) 117,
      (byte) 118,
      (byte) 251,
      (byte) 207,
      (byte) 132,
      (byte) 143,
      (byte) 192 /*0xC0*/,
      (byte) 216,
      (byte) 62,
      (byte) 250,
      (byte) 252,
      (byte) 63 /*0x3F*/,
      (byte) 188,
      (byte) 91,
      (byte) 97
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_13015.sspq, 0, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13015.sspr, 0, (Array) numArray2, 0, 37);
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
}
