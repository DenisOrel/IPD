// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5511
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_5511
{
  private static byte[] sspq = new byte[85]
  {
    (byte) 48 /*0x30*/,
    (byte) 250,
    (byte) 28,
    (byte) 188,
    (byte) 104,
    (byte) 143,
    (byte) 122,
    (byte) 102,
    (byte) 197,
    (byte) 193,
    (byte) 160 /*0xA0*/,
    (byte) 96 /*0x60*/,
    (byte) 39,
    (byte) 63 /*0x3F*/,
    (byte) 130,
    (byte) 70,
    (byte) 131,
    (byte) 107,
    (byte) 194,
    (byte) 217,
    (byte) 151,
    (byte) 173,
    (byte) 21,
    (byte) 222,
    (byte) 36,
    (byte) 144 /*0x90*/,
    (byte) 95,
    (byte) 37,
    (byte) 126,
    (byte) 11,
    (byte) 240 /*0xF0*/,
    (byte) 165,
    (byte) 165,
    (byte) 73,
    (byte) 100,
    (byte) 54,
    (byte) 227,
    (byte) 223,
    (byte) 96 /*0x60*/,
    (byte) 211,
    (byte) 113,
    (byte) 218,
    (byte) 242,
    (byte) 73,
    (byte) 215,
    (byte) 233,
    (byte) 135,
    (byte) 230,
    (byte) 240 /*0xF0*/,
    (byte) 74,
    (byte) 10,
    (byte) 17,
    (byte) 141,
    (byte) 76,
    (byte) 33,
    (byte) 254,
    (byte) 215,
    (byte) 119,
    (byte) 67,
    (byte) 181,
    (byte) 11,
    (byte) 154,
    (byte) 196,
    (byte) 213,
    (byte) 99,
    (byte) 119,
    (byte) 144 /*0x90*/,
    (byte) 159,
    (byte) 80 /*0x50*/,
    (byte) 248,
    (byte) 249,
    (byte) 188,
    (byte) 93,
    (byte) 129,
    (byte) 74,
    (byte) 75,
    (byte) 202,
    (byte) 97,
    (byte) 150,
    (byte) 16 /*0x10*/,
    (byte) 90,
    (byte) 229,
    (byte) 164,
    (byte) 69,
    (byte) 7
  };
  private static byte[] sspr = new byte[85]
  {
    (byte) 78,
    (byte) 184,
    (byte) 165,
    (byte) 61,
    (byte) 129,
    (byte) 63 /*0x3F*/,
    (byte) 109,
    (byte) 215,
    (byte) 114,
    (byte) 127 /*0x7F*/,
    (byte) 47,
    (byte) 112 /*0x70*/,
    (byte) 252,
    (byte) 77,
    (byte) 127 /*0x7F*/,
    (byte) 170,
    (byte) 89,
    (byte) 179,
    (byte) 110,
    (byte) 148,
    (byte) 22,
    (byte) 95,
    (byte) 143,
    (byte) 35,
    (byte) 41,
    (byte) 162,
    (byte) 28,
    (byte) 72,
    (byte) 154,
    (byte) 2,
    (byte) 119,
    (byte) 50,
    (byte) 9,
    (byte) 60,
    (byte) 194,
    (byte) 135,
    (byte) 195,
    (byte) 14,
    (byte) 221,
    (byte) 201,
    (byte) 37,
    (byte) 202,
    (byte) 77,
    (byte) 187,
    (byte) 10,
    (byte) 187,
    (byte) 176 /*0xB0*/,
    (byte) 136,
    (byte) 22,
    (byte) 13,
    (byte) 238,
    (byte) 200,
    (byte) 235,
    (byte) 120,
    (byte) 210,
    (byte) 168,
    (byte) 11,
    (byte) 47,
    (byte) 199,
    (byte) 8,
    (byte) 129,
    (byte) 45,
    (byte) 35,
    (byte) 224 /*0xE0*/,
    (byte) 47,
    (byte) 81,
    (byte) 223,
    (byte) 100,
    (byte) 128 /*0x80*/,
    (byte) 252,
    (byte) 86,
    (byte) 45,
    (byte) 50,
    (byte) 75,
    (byte) 208 /*0xD0*/,
    (byte) 252,
    (byte) 104,
    (byte) 214,
    (byte) 146,
    (byte) 165,
    (byte) 71,
    (byte) 30,
    (byte) 247,
    (byte) 2,
    (byte) 130
  };

  internal static int ssp_appserver_5512(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 7,
      (byte) 152,
      (byte) 137,
      (byte) 16 /*0x10*/,
      (byte) 94,
      (byte) 199,
      (byte) 63 /*0x3F*/,
      (byte) 227,
      (byte) 129,
      (byte) 105,
      (byte) 178,
      (byte) 72,
      (byte) 157,
      (byte) 43,
      (byte) 210,
      (byte) 42,
      (byte) 75,
      (byte) 9,
      (byte) 46,
      (byte) 158,
      (byte) 44,
      (byte) 196,
      (byte) 48 /*0x30*/,
      (byte) 198,
      (byte) 59,
      (byte) 171,
      (byte) 128 /*0x80*/,
      (byte) 32 /*0x20*/,
      (byte) 174,
      (byte) 92,
      (byte) 163,
      (byte) 218,
      (byte) 210,
      (byte) 30,
      (byte) 199,
      (byte) 10,
      (byte) 174,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 251,
      (byte) 89,
      (byte) 56,
      (byte) 33,
      (byte) 40,
      (byte) 42,
      (byte) 166,
      (byte) 121,
      (byte) 127 /*0x7F*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 86;
    sourceArray2[47] = (byte) 188;
    sourceArray2[2] = (byte) 216;
    sourceArray2[25] = (byte) 225;
    sourceArray2[4] = (byte) 37;
    sourceArray2[0] = (byte) 124;
    sourceArray2[6] = (byte) 75;
    sourceArray2[46] = (byte) 65;
    sourceArray2[8] = (byte) 20;
    sourceArray2[12] = (byte) 208 /*0xD0*/;
    sourceArray2[5] = (byte) 161;
    sourceArray2[9] = (byte) 100;
    sourceArray2[7] = (byte) 54;
    sourceArray2[13] = (byte) 235;
    sourceArray2[14] = (byte) 185;
    sourceArray2[15] = (byte) 152;
    sourceArray2[35] = (byte) 61;
    sourceArray2[17] = (byte) 122;
    sourceArray2[20] = (byte) 239;
    sourceArray2[19] = (byte) 160 /*0xA0*/;
    sourceArray2[40] = (byte) 48 /*0x30*/;
    sourceArray2[31 /*0x1F*/] = (byte) 205;
    sourceArray2[32 /*0x20*/] = (byte) 198;
    sourceArray2[23] = (byte) 47;
    sourceArray2[45] = (byte) 151;
    sourceArray2[37] = (byte) 41;
    sourceArray2[26] = (byte) 186;
    sourceArray2[27] = (byte) 122;
    sourceArray2[11] = (byte) 93;
    sourceArray2[29] = (byte) 118;
    sourceArray2[30] = (byte) 121;
    sourceArray2[24] = (byte) 137;
    sourceArray2[3] = (byte) 183;
    sourceArray2[33] = (byte) 104;
    sourceArray2[34] = (byte) 118;
    sourceArray2[10] = (byte) 25;
    sourceArray2[36] = (byte) 161;
    sourceArray2[1] = (byte) 135;
    sourceArray2[38] = (byte) 8;
    sourceArray2[16 /*0x10*/] = (byte) 109;
    sourceArray2[42] = (byte) 73;
    sourceArray2[18] = (byte) 104;
    sourceArray2[22] = (byte) 249;
    sourceArray2[28] = (byte) 208 /*0xD0*/;
    sourceArray2[44] = (byte) 70;
    sourceArray2[43] = (byte) 118;
    sourceArray2[21] = (byte) 205;
    sourceArray2[41] = (byte) 137;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_5511.sspq, 0, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_5511.sspr, 0, (Array) numArray2, 0, 42);
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

  internal static int ssp_appserver_5513(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 228,
      (byte) 66,
      (byte) 196,
      (byte) 203,
      (byte) 149,
      (byte) 109,
      (byte) 182,
      (byte) 152,
      (byte) 59,
      (byte) 137,
      (byte) 187,
      (byte) 130,
      (byte) 229,
      (byte) 61,
      (byte) 151,
      (byte) 215,
      (byte) 47,
      (byte) 84,
      (byte) 231,
      (byte) 185,
      (byte) 208 /*0xD0*/,
      (byte) 250,
      (byte) 186,
      (byte) 221,
      (byte) 219,
      (byte) 75,
      (byte) 98,
      (byte) 164,
      (byte) 10,
      (byte) 238,
      (byte) 104,
      (byte) 188,
      (byte) 171,
      (byte) 109,
      (byte) 250,
      (byte) 226,
      (byte) 204,
      (byte) 148,
      (byte) 143,
      (byte) 17,
      (byte) 85,
      (byte) 88,
      (byte) 106,
      (byte) 27,
      (byte) 141,
      (byte) 226,
      (byte) 81,
      (byte) 166
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 43,
      (byte) 122,
      (byte) 79,
      (byte) 24,
      (byte) 239,
      (byte) 98,
      (byte) 211,
      (byte) 234,
      (byte) 218,
      (byte) 107,
      (byte) 32 /*0x20*/,
      (byte) 190,
      (byte) 253,
      (byte) 85,
      (byte) 10,
      (byte) 222,
      (byte) 182,
      (byte) 116,
      (byte) 149,
      (byte) 128 /*0x80*/,
      (byte) 174,
      (byte) 181,
      (byte) 219,
      (byte) 108,
      (byte) 30,
      (byte) 161,
      (byte) 23,
      (byte) 52,
      (byte) 217,
      (byte) 190,
      (byte) 128 /*0x80*/,
      (byte) 236,
      (byte) 99,
      (byte) 103,
      (byte) 230,
      (byte) 108,
      (byte) 7,
      (byte) 152,
      (byte) 104,
      (byte) 83,
      (byte) 108,
      (byte) 180,
      (byte) 112 /*0x70*/,
      (byte) 49,
      (byte) 229,
      (byte) 248,
      (byte) 102,
      (byte) 96 /*0x60*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[43];
    byte[] response2 = new byte[43];
    Array.Copy((Array) sc_5511.sspq, 42, (Array) numArray2, 0, 43);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_5511.sspr, 42, (Array) numArray2, 0, 43);
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
