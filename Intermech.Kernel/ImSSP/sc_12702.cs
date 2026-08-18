// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12702
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12702
{
  private static byte[] sspq = new byte[39]
  {
    (byte) 49,
    (byte) 96 /*0x60*/,
    (byte) 139,
    (byte) 141,
    (byte) 193,
    (byte) 246,
    (byte) 49,
    (byte) 203,
    (byte) 57,
    (byte) 33,
    (byte) 169,
    (byte) 108,
    (byte) 52,
    (byte) 171,
    (byte) 81,
    (byte) 228,
    (byte) 87,
    (byte) 144 /*0x90*/,
    (byte) 198,
    (byte) 206,
    (byte) 36,
    (byte) 133,
    (byte) 4,
    (byte) 217,
    (byte) 17,
    (byte) 46,
    (byte) 1,
    (byte) 69,
    (byte) 159,
    (byte) 170,
    (byte) 142,
    (byte) 98,
    (byte) 227,
    (byte) 182,
    (byte) 240 /*0xF0*/,
    (byte) 137,
    (byte) 210,
    (byte) 185,
    (byte) 130
  };
  private static byte[] sspr = new byte[39]
  {
    (byte) 73,
    (byte) 171,
    (byte) 59,
    (byte) 196,
    (byte) 1,
    (byte) 188,
    (byte) 140,
    (byte) 234,
    (byte) 77,
    (byte) 192 /*0xC0*/,
    (byte) 204,
    (byte) 10,
    (byte) 229,
    (byte) 87,
    (byte) 244,
    (byte) 134,
    (byte) 16 /*0x10*/,
    (byte) 100,
    (byte) 185,
    (byte) 55,
    (byte) 83,
    (byte) 149,
    (byte) 217,
    (byte) 36,
    (byte) 190,
    (byte) 62,
    (byte) 240 /*0xF0*/,
    (byte) 138,
    (byte) 41,
    (byte) 177,
    (byte) 131,
    (byte) 104,
    (byte) 218,
    (byte) 208 /*0xD0*/,
    (byte) 176 /*0xB0*/,
    (byte) 162,
    (byte) 183,
    (byte) 184,
    (byte) 176 /*0xB0*/
  };

  internal static string ssp_appserver_12703()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 251,
        (byte) 102,
        (byte) 250,
        (byte) 80 /*0x50*/,
        (byte) 220,
        (byte) 147,
        (byte) 246,
        (byte) 224 /*0xE0*/,
        (byte) 233,
        (byte) 84
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 254,
        (byte) 35,
        (byte) 57,
        (byte) 32 /*0x20*/,
        (byte) 184,
        (byte) 191,
        (byte) 25,
        (byte) 52,
        (byte) 72,
        (byte) 206
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[39];
      byte[] response = new byte[39];
      Array.Copy((Array) sc_12702.sspq, 0, (Array) numArray4, 0, 39);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12702.sspr, 0, (Array) numArray4, 0, 39);
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
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 47,
      (byte) 45,
      (byte) 216,
      (byte) 166,
      (byte) 112 /*0x70*/,
      (byte) 95,
      (byte) 74,
      (byte) 142
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 74,
      (byte) 106,
      (byte) 12,
      (byte) 58,
      (byte) 4,
      (byte) 228,
      (byte) 238,
      (byte) 252,
      (byte) 48 /*0x30*/,
      (byte) 184
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12704(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[44] = (byte) 69;
    sourceArray1[4] = (byte) 124;
    sourceArray1[40] = (byte) 250;
    sourceArray1[3] = (byte) 178;
    sourceArray1[19] = (byte) 214;
    sourceArray1[5] = (byte) 147;
    sourceArray1[18] = (byte) 13;
    sourceArray1[7] = (byte) 235;
    sourceArray1[43] = (byte) 158;
    sourceArray1[37] = (byte) 32 /*0x20*/;
    sourceArray1[10] = (byte) 147;
    sourceArray1[11] = (byte) 71;
    sourceArray1[14] = (byte) 186;
    sourceArray1[13] = (byte) 125;
    sourceArray1[15] = (byte) 200;
    sourceArray1[31 /*0x1F*/] = (byte) 169;
    sourceArray1[29] = (byte) 154;
    sourceArray1[17] = (byte) 157;
    sourceArray1[47] = (byte) 226;
    sourceArray1[42] = (byte) 3;
    sourceArray1[20] = (byte) 238;
    sourceArray1[21] = (byte) 244;
    sourceArray1[28] = (byte) 182;
    sourceArray1[6] = (byte) 245;
    sourceArray1[16 /*0x10*/] = (byte) 17;
    sourceArray1[23] = (byte) 43;
    sourceArray1[35] = (byte) 68;
    sourceArray1[27] = (byte) 182;
    sourceArray1[30] = (byte) 173;
    sourceArray1[1] = (byte) 171;
    sourceArray1[12] = (byte) 136;
    sourceArray1[2] = (byte) 91;
    sourceArray1[0] = (byte) 108;
    sourceArray1[33] = (byte) 5;
    sourceArray1[34] = (byte) 74;
    sourceArray1[9] = (byte) 142;
    sourceArray1[36] = (byte) 45;
    sourceArray1[26] = (byte) 206;
    sourceArray1[38] = (byte) 61;
    sourceArray1[8] = (byte) 44;
    sourceArray1[46] = (byte) 231;
    sourceArray1[41] = (byte) 244;
    sourceArray1[25] = (byte) 160 /*0xA0*/;
    sourceArray1[39] = (byte) 239;
    sourceArray1[24] = (byte) 50;
    sourceArray1[45] = (byte) 95;
    sourceArray1[22] = (byte) 88;
    sourceArray1[32 /*0x20*/] = (byte) 212;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 154,
      (byte) 176 /*0xB0*/,
      (byte) 235,
      (byte) 214,
      (byte) 110,
      (byte) 77,
      (byte) 110,
      (byte) 213,
      (byte) 1,
      (byte) 0,
      (byte) 147,
      (byte) 88,
      (byte) 209,
      (byte) 120,
      (byte) 180,
      (byte) 165,
      (byte) 207,
      (byte) 92,
      (byte) 152,
      (byte) 126,
      (byte) 127 /*0x7F*/,
      (byte) 49,
      (byte) 87,
      (byte) 212,
      (byte) 65,
      (byte) 6,
      (byte) 235,
      (byte) 151,
      (byte) 246,
      (byte) 241,
      (byte) 248,
      (byte) 74,
      (byte) 159,
      (byte) 133,
      (byte) 142,
      (byte) 198,
      (byte) 85,
      (byte) 237,
      (byte) 44,
      (byte) 50,
      (byte) 53,
      (byte) 13,
      (byte) 185,
      (byte) 119,
      (byte) 120,
      (byte) 119,
      (byte) 178,
      (byte) 61
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
