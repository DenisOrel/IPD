// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12264
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12264
{
  private static byte[] sspq = new byte[28]
  {
    (byte) 63 /*0x3F*/,
    (byte) 123,
    (byte) 12,
    (byte) 107,
    (byte) 20,
    (byte) 155,
    (byte) 244,
    (byte) 115,
    (byte) 112 /*0x70*/,
    (byte) 9,
    (byte) 165,
    (byte) 33,
    (byte) 139,
    (byte) 230,
    (byte) 221,
    (byte) 147,
    (byte) 218,
    (byte) 19,
    (byte) 142,
    (byte) 12,
    (byte) 41,
    (byte) 182,
    (byte) 35,
    (byte) 242,
    (byte) 122,
    (byte) 115,
    (byte) 34,
    (byte) 134
  };
  private static byte[] sspr = new byte[28]
  {
    (byte) 114,
    (byte) 121,
    (byte) 189,
    (byte) 143,
    (byte) 135,
    (byte) 253,
    byte.MaxValue,
    (byte) 153,
    (byte) 42,
    (byte) 66,
    (byte) 200,
    (byte) 75,
    (byte) 177,
    (byte) 150,
    (byte) 92,
    (byte) 13,
    (byte) 11,
    (byte) 80 /*0x50*/,
    (byte) 164,
    (byte) 231,
    (byte) 28,
    (byte) 219,
    (byte) 67,
    (byte) 227,
    (byte) 199,
    (byte) 9,
    (byte) 33,
    (byte) 196
  };

  internal static int ssp_appserver_12265(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 160 /*0xA0*/,
      (byte) 12,
      (byte) 174,
      (byte) 79,
      (byte) 209,
      (byte) 127 /*0x7F*/,
      (byte) 36,
      (byte) 171,
      (byte) 6,
      (byte) 154,
      (byte) 230,
      (byte) 165,
      (byte) 194,
      (byte) 58,
      (byte) 65,
      (byte) 63 /*0x3F*/,
      (byte) 128 /*0x80*/,
      (byte) 8,
      (byte) 130,
      (byte) 3,
      (byte) 113,
      (byte) 138,
      (byte) 248,
      (byte) 212,
      (byte) 203,
      (byte) 103,
      (byte) 2,
      (byte) 196,
      (byte) 76,
      (byte) 145,
      (byte) 74,
      (byte) 160 /*0xA0*/,
      (byte) 184,
      (byte) 98,
      (byte) 46,
      (byte) 109,
      (byte) 83,
      (byte) 113,
      (byte) 245,
      (byte) 114,
      (byte) 242,
      (byte) 84,
      (byte) 26,
      (byte) 6,
      (byte) 133,
      (byte) 45,
      (byte) 219,
      (byte) 24
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 7;
    sourceArray2[26] = (byte) 187;
    sourceArray2[2] = (byte) 90;
    sourceArray2[42] = (byte) 49;
    sourceArray2[22] = (byte) 199;
    sourceArray2[5] = (byte) 125;
    sourceArray2[4] = (byte) 150;
    sourceArray2[37] = (byte) 89;
    sourceArray2[8] = (byte) 214;
    sourceArray2[47] = (byte) 170;
    sourceArray2[12] = (byte) 212;
    sourceArray2[3] = (byte) 99;
    sourceArray2[11] = (byte) 144 /*0x90*/;
    sourceArray2[28] = (byte) 148;
    sourceArray2[14] = (byte) 49;
    sourceArray2[21] = (byte) 226;
    sourceArray2[20] = (byte) 222;
    sourceArray2[13] = (byte) 163;
    sourceArray2[0] = (byte) 57;
    sourceArray2[34] = (byte) 99;
    sourceArray2[31 /*0x1F*/] = (byte) 105;
    sourceArray2[1] = (byte) 242;
    sourceArray2[16 /*0x10*/] = (byte) 0;
    sourceArray2[18] = (byte) 196;
    sourceArray2[24] = (byte) 102;
    sourceArray2[25] = (byte) 112 /*0x70*/;
    sourceArray2[17] = (byte) 172;
    sourceArray2[27] = (byte) 175;
    sourceArray2[15] = (byte) 248;
    sourceArray2[29] = (byte) 76;
    sourceArray2[30] = (byte) 164;
    sourceArray2[7] = (byte) 254;
    sourceArray2[32 /*0x20*/] = (byte) 104;
    sourceArray2[33] = (byte) 227;
    sourceArray2[6] = (byte) 163;
    sourceArray2[35] = (byte) 78;
    sourceArray2[36] = (byte) 147;
    sourceArray2[19] = (byte) 228;
    sourceArray2[9] = (byte) 247;
    sourceArray2[39] = (byte) 24;
    sourceArray2[40] = (byte) 211;
    sourceArray2[41] = (byte) 177;
    sourceArray2[23] = (byte) 217;
    sourceArray2[43] = (byte) 105;
    sourceArray2[38] = (byte) 70;
    sourceArray2[45] = (byte) 24;
    sourceArray2[46] = (byte) 93;
    sourceArray2[44] = (byte) 226;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[28];
    byte[] response2 = new byte[28];
    Array.Copy((Array) sc_12264.sspq, 0, (Array) numArray2, 0, 28);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12264.sspr, 0, (Array) numArray2, 0, 28);
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

  internal static int ssp_appserver_12266(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 40,
      (byte) 203,
      (byte) 60,
      (byte) 15,
      (byte) 94,
      (byte) 17,
      (byte) 221,
      (byte) 26,
      (byte) 55,
      (byte) 75,
      (byte) 79,
      (byte) 168,
      (byte) 198,
      (byte) 50,
      (byte) 116,
      (byte) 56,
      (byte) 237,
      (byte) 7,
      (byte) 174,
      (byte) 98,
      (byte) 183,
      (byte) 225,
      (byte) 23,
      (byte) 169,
      (byte) 22,
      (byte) 137,
      (byte) 87,
      (byte) 26,
      (byte) 240 /*0xF0*/,
      (byte) 161,
      (byte) 120,
      (byte) 109,
      (byte) 89,
      (byte) 242,
      (byte) 225,
      (byte) 217,
      (byte) 228,
      (byte) 124,
      (byte) 135,
      (byte) 204,
      (byte) 181,
      (byte) 192 /*0xC0*/,
      (byte) 220,
      (byte) 66,
      (byte) 188,
      (byte) 128 /*0x80*/,
      (byte) 240 /*0xF0*/,
      (byte) 241
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 124,
      (byte) 192 /*0xC0*/,
      (byte) 16 /*0x10*/,
      (byte) 183,
      (byte) 242,
      (byte) 94,
      (byte) 186,
      (byte) 53,
      (byte) 78,
      (byte) 4,
      (byte) 109,
      (byte) 250,
      (byte) 183,
      (byte) 215,
      (byte) 97,
      (byte) 11,
      (byte) 152,
      (byte) 44,
      (byte) 216,
      (byte) 126,
      (byte) 196,
      (byte) 237,
      (byte) 143,
      (byte) 161,
      (byte) 41,
      (byte) 98,
      (byte) 95,
      (byte) 78,
      (byte) 12,
      (byte) 245,
      (byte) 94,
      (byte) 76,
      (byte) 220,
      (byte) 64 /*0x40*/,
      (byte) 66,
      (byte) 156,
      (byte) 8,
      (byte) 9,
      (byte) 243,
      (byte) 96 /*0x60*/,
      (byte) 110,
      (byte) 88,
      (byte) 82,
      (byte) 94,
      (byte) 95,
      (byte) 224 /*0xE0*/,
      (byte) 80 /*0x50*/,
      (byte) 1
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12267(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 183,
      (byte) 198,
      (byte) 70,
      (byte) 180,
      (byte) 147,
      (byte) 249,
      (byte) 212,
      (byte) 185,
      (byte) 195,
      (byte) 41,
      (byte) 149,
      (byte) 210,
      (byte) 225,
      (byte) 79,
      (byte) 211,
      (byte) 81,
      (byte) 154,
      (byte) 83,
      (byte) 113,
      (byte) 127 /*0x7F*/,
      (byte) 226,
      (byte) 172,
      (byte) 247,
      (byte) 5,
      (byte) 212,
      (byte) 236,
      (byte) 149,
      (byte) 153,
      (byte) 102,
      (byte) 155,
      (byte) 175,
      (byte) 27,
      (byte) 93,
      (byte) 150,
      (byte) 161,
      (byte) 44,
      (byte) 154,
      (byte) 37,
      (byte) 81,
      (byte) 131,
      (byte) 179,
      (byte) 18,
      (byte) 242,
      (byte) 150,
      (byte) 70,
      (byte) 111,
      (byte) 219,
      (byte) 49
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 159,
      (byte) 228,
      (byte) 176 /*0xB0*/,
      (byte) 215,
      (byte) 16 /*0x10*/,
      (byte) 144 /*0x90*/,
      (byte) 81,
      (byte) 53,
      (byte) 85,
      (byte) 77,
      (byte) 225,
      (byte) 123,
      (byte) 123,
      (byte) 75,
      (byte) 161,
      (byte) 3,
      (byte) 84,
      (byte) 51,
      (byte) 216,
      (byte) 66,
      (byte) 18,
      (byte) 209,
      (byte) 216,
      (byte) 143,
      (byte) 132,
      (byte) 128 /*0x80*/,
      (byte) 236,
      (byte) 53,
      (byte) 226,
      (byte) 146,
      (byte) 5,
      (byte) 166,
      (byte) 121,
      (byte) 227,
      (byte) 3,
      (byte) 72,
      (byte) 61,
      (byte) 111,
      (byte) 196,
      (byte) 21,
      (byte) 97,
      (byte) 113,
      (byte) 28,
      (byte) 145,
      (byte) 227,
      (byte) 150,
      (byte) 247,
      (byte) 221
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
