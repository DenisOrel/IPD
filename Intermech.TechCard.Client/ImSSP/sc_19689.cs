// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19689
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19689
{
  private static byte[] sspq = new byte[36]
  {
    (byte) 148,
    (byte) 231,
    (byte) 144 /*0x90*/,
    (byte) 212,
    (byte) 29,
    (byte) 38,
    (byte) 199,
    (byte) 127 /*0x7F*/,
    (byte) 32 /*0x20*/,
    (byte) 214,
    (byte) 229,
    (byte) 168,
    (byte) 88,
    (byte) 61,
    (byte) 173,
    (byte) 51,
    (byte) 79,
    (byte) 236,
    (byte) 207,
    (byte) 164,
    (byte) 231,
    (byte) 230,
    (byte) 183,
    (byte) 126,
    (byte) 35,
    (byte) 226,
    (byte) 11,
    (byte) 179,
    (byte) 214,
    (byte) 170,
    (byte) 239,
    (byte) 98,
    (byte) 40,
    (byte) 44,
    (byte) 154,
    (byte) 198
  };
  private static byte[] sspr = new byte[36]
  {
    (byte) 40,
    (byte) 30,
    (byte) 77,
    (byte) 216,
    (byte) 254,
    (byte) 114,
    (byte) 58,
    (byte) 86,
    (byte) 30,
    (byte) 207,
    (byte) 86,
    (byte) 239,
    (byte) 202,
    (byte) 54,
    (byte) 141,
    (byte) 150,
    (byte) 246,
    (byte) 68,
    (byte) 157,
    (byte) 153,
    (byte) 153,
    (byte) 178,
    (byte) 59,
    (byte) 139,
    (byte) 46,
    (byte) 175,
    (byte) 64 /*0x40*/,
    (byte) 128 /*0x80*/,
    (byte) 158,
    (byte) 138,
    (byte) 175,
    (byte) 2,
    (byte) 207,
    (byte) 2,
    (byte) 87,
    (byte) 38
  };

  internal static int ssp_techcard_19690(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 97,
      (byte) 222,
      (byte) 121,
      (byte) 68,
      (byte) 4,
      (byte) 67,
      (byte) 246,
      (byte) 79,
      (byte) 235,
      (byte) 168,
      (byte) 162,
      (byte) 194,
      (byte) 99,
      byte.MaxValue,
      (byte) 132,
      (byte) 38,
      (byte) 88,
      (byte) 186,
      (byte) 219,
      (byte) 62,
      (byte) 109,
      (byte) 139,
      (byte) 106,
      (byte) 42,
      (byte) 216,
      (byte) 168,
      (byte) 250,
      (byte) 203,
      (byte) 46,
      (byte) 222,
      (byte) 170,
      (byte) 55,
      (byte) 173,
      (byte) 80 /*0x50*/,
      (byte) 171,
      (byte) 105,
      (byte) 73,
      (byte) 220,
      (byte) 49,
      (byte) 167,
      (byte) 223,
      (byte) 79,
      (byte) 154,
      (byte) 159,
      (byte) 52,
      (byte) 169,
      (byte) 250,
      (byte) 228
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 206;
    sourceArray2[30] = (byte) 193;
    sourceArray2[2] = (byte) 27;
    sourceArray2[3] = (byte) 152;
    sourceArray2[4] = (byte) 157;
    sourceArray2[5] = (byte) 75;
    sourceArray2[0] = (byte) 30;
    sourceArray2[31 /*0x1F*/] = (byte) 71;
    sourceArray2[1] = (byte) 129;
    sourceArray2[28] = (byte) 19;
    sourceArray2[40] = (byte) 145;
    sourceArray2[37] = (byte) 52;
    sourceArray2[33] = (byte) 43;
    sourceArray2[16 /*0x10*/] = (byte) 126;
    sourceArray2[21] = (byte) 43;
    sourceArray2[36] = (byte) 232;
    sourceArray2[34] = (byte) 202;
    sourceArray2[17] = (byte) 232;
    sourceArray2[18] = (byte) 200;
    sourceArray2[14] = (byte) 122;
    sourceArray2[20] = (byte) 189;
    sourceArray2[24] = (byte) 156;
    sourceArray2[22] = byte.MaxValue;
    sourceArray2[8] = (byte) 174;
    sourceArray2[47] = (byte) 29;
    sourceArray2[25] = (byte) 59;
    sourceArray2[10] = (byte) 54;
    sourceArray2[13] = (byte) 182;
    sourceArray2[27] = (byte) 159;
    sourceArray2[7] = (byte) 6;
    sourceArray2[19] = (byte) 188;
    sourceArray2[6] = (byte) 104;
    sourceArray2[32 /*0x20*/] = (byte) 246;
    sourceArray2[9] = (byte) 88;
    sourceArray2[11] = (byte) 197;
    sourceArray2[35] = (byte) 38;
    sourceArray2[12] = (byte) 9;
    sourceArray2[15] = (byte) 241;
    sourceArray2[38] = (byte) 166;
    sourceArray2[39] = (byte) 45;
    sourceArray2[29] = (byte) 83;
    sourceArray2[41] = (byte) 83;
    sourceArray2[42] = (byte) 159;
    sourceArray2[43] = (byte) 185;
    sourceArray2[44] = (byte) 125;
    sourceArray2[45] = (byte) 200;
    sourceArray2[46] = (byte) 154;
    sourceArray2[23] = (byte) 63 /*0x3F*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 359, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[36];
    byte[] response2 = new byte[36];
    Array.Copy((Array) sc_19689.sspq, 0, (Array) numArray2, 0, 36);
    key.Query(true, 359, numArray2, response2);
    Array.Copy((Array) sc_19689.sspr, 0, (Array) numArray2, 0, 36);
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

  internal static int ssp_techcard_19691(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 22,
      (byte) 145,
      (byte) 4,
      (byte) 14,
      (byte) 133,
      (byte) 239,
      (byte) 77,
      (byte) 162,
      (byte) 51,
      (byte) 39,
      (byte) 43,
      (byte) 101,
      (byte) 65,
      (byte) 191,
      (byte) 253,
      (byte) 226,
      (byte) 173,
      (byte) 47,
      (byte) 17,
      (byte) 177,
      (byte) 59,
      (byte) 26,
      (byte) 20,
      (byte) 168,
      (byte) 118,
      (byte) 230,
      (byte) 131,
      (byte) 127 /*0x7F*/,
      (byte) 44,
      (byte) 162,
      (byte) 231,
      (byte) 173,
      (byte) 71,
      (byte) 214,
      (byte) 184,
      (byte) 19,
      (byte) 226,
      (byte) 216,
      (byte) 50,
      (byte) 143,
      (byte) 214,
      (byte) 64 /*0x40*/,
      (byte) 216,
      (byte) 139,
      (byte) 171,
      (byte) 69,
      (byte) 173
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 120;
    sourceArray2[7] = (byte) 47;
    sourceArray2[2] = (byte) 90;
    sourceArray2[3] = (byte) 95;
    sourceArray2[4] = (byte) 199;
    sourceArray2[27] = (byte) 6;
    sourceArray2[0] = (byte) 197;
    sourceArray2[17] = (byte) 230;
    sourceArray2[32 /*0x20*/] = (byte) 186;
    sourceArray2[40] = (byte) 82;
    sourceArray2[10] = (byte) 49;
    sourceArray2[11] = (byte) 205;
    sourceArray2[12] = (byte) 138;
    sourceArray2[39] = (byte) 188;
    sourceArray2[14] = (byte) 70;
    sourceArray2[35] = (byte) 132;
    sourceArray2[8] = (byte) 119;
    sourceArray2[19] = (byte) 220;
    sourceArray2[18] = (byte) 44;
    sourceArray2[6] = (byte) 179;
    sourceArray2[41] = (byte) 195;
    sourceArray2[21] = (byte) 119;
    sourceArray2[16 /*0x10*/] = (byte) 112 /*0x70*/;
    sourceArray2[23] = (byte) 218;
    sourceArray2[9] = (byte) 230;
    sourceArray2[25] = (byte) 102;
    sourceArray2[26] = (byte) 164;
    sourceArray2[47] = (byte) 238;
    sourceArray2[28] = (byte) 210;
    sourceArray2[29] = (byte) 224 /*0xE0*/;
    sourceArray2[30] = (byte) 156;
    sourceArray2[15] = (byte) 200;
    sourceArray2[1] = (byte) 228;
    sourceArray2[33] = (byte) 122;
    sourceArray2[38] = (byte) 113;
    sourceArray2[42] = (byte) 15;
    sourceArray2[36] = (byte) 26;
    sourceArray2[37] = (byte) 17;
    sourceArray2[5] = (byte) 45;
    sourceArray2[24] = (byte) 85;
    sourceArray2[31 /*0x1F*/] = (byte) 239;
    sourceArray2[22] = (byte) 105;
    sourceArray2[34] = (byte) 30;
    sourceArray2[20] = (byte) 173;
    sourceArray2[44] = (byte) 46;
    sourceArray2[45] = (byte) 42;
    sourceArray2[13] = (byte) 133;
    sourceArray2[46] = (byte) 233;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19692(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[42] = (byte) 118;
    sourceArray1[17] = (byte) 148;
    sourceArray1[2] = (byte) 156;
    sourceArray1[24] = (byte) 84;
    sourceArray1[4] = (byte) 73;
    sourceArray1[1] = (byte) 66;
    sourceArray1[0] = (byte) 39;
    sourceArray1[16 /*0x10*/] = (byte) 122;
    sourceArray1[8] = (byte) 123;
    sourceArray1[18] = (byte) 218;
    sourceArray1[6] = (byte) 153;
    sourceArray1[43] = (byte) 27;
    sourceArray1[19] = (byte) 239;
    sourceArray1[13] = (byte) 102;
    sourceArray1[36] = (byte) 67;
    sourceArray1[15] = (byte) 138;
    sourceArray1[35] = (byte) 246;
    sourceArray1[30] = (byte) 48 /*0x30*/;
    sourceArray1[14] = (byte) 101;
    sourceArray1[12] = (byte) 95;
    sourceArray1[47] = (byte) 218;
    sourceArray1[21] = (byte) 43;
    sourceArray1[22] = (byte) 112 /*0x70*/;
    sourceArray1[23] = (byte) 73;
    sourceArray1[39] = (byte) 157;
    sourceArray1[27] = (byte) 104;
    sourceArray1[26] = (byte) 148;
    sourceArray1[40] = (byte) 183;
    sourceArray1[28] = (byte) 94;
    sourceArray1[44] = (byte) 2;
    sourceArray1[10] = (byte) 186;
    sourceArray1[7] = (byte) 242;
    sourceArray1[32 /*0x20*/] = (byte) 61;
    sourceArray1[33] = (byte) 207;
    sourceArray1[34] = (byte) 9;
    sourceArray1[38] = (byte) 152;
    sourceArray1[31 /*0x1F*/] = (byte) 249;
    sourceArray1[37] = (byte) 17;
    sourceArray1[11] = (byte) 244;
    sourceArray1[20] = (byte) 69;
    sourceArray1[9] = (byte) 223;
    sourceArray1[41] = (byte) 70;
    sourceArray1[25] = (byte) 122;
    sourceArray1[3] = (byte) 65;
    sourceArray1[5] = (byte) 24;
    sourceArray1[45] = (byte) 160 /*0xA0*/;
    sourceArray1[46] = (byte) 38;
    sourceArray1[29] = (byte) 210;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 96 /*0x60*/,
      (byte) 14,
      (byte) 241,
      (byte) 136,
      (byte) 78,
      (byte) 84,
      (byte) 129,
      (byte) 122,
      (byte) 230,
      (byte) 136,
      (byte) 221,
      (byte) 77,
      (byte) 128 /*0x80*/,
      (byte) 94,
      (byte) 250,
      (byte) 197,
      (byte) 179,
      (byte) 74,
      (byte) 222,
      (byte) 153,
      (byte) 193,
      (byte) 207,
      (byte) 152,
      (byte) 209,
      (byte) 132,
      (byte) 58,
      (byte) 58,
      (byte) 77,
      byte.MaxValue,
      (byte) 91,
      (byte) 13,
      (byte) 230,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 231,
      (byte) 88,
      (byte) 154,
      (byte) 73,
      (byte) 74,
      (byte) 218,
      (byte) 224 /*0xE0*/,
      (byte) 59,
      (byte) 202,
      (byte) 234,
      (byte) 112 /*0x70*/,
      (byte) 243,
      (byte) 231,
      (byte) 58
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19693(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 150,
      (byte) 124,
      (byte) 191,
      (byte) 82,
      (byte) 37,
      (byte) 58,
      (byte) 49,
      (byte) 34,
      (byte) 233,
      (byte) 71,
      (byte) 125,
      (byte) 12,
      (byte) 58,
      (byte) 123,
      (byte) 201,
      (byte) 247,
      (byte) 206,
      (byte) 100,
      (byte) 180,
      (byte) 53,
      (byte) 213,
      (byte) 169,
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 86,
      (byte) 68,
      (byte) 51,
      (byte) 34,
      (byte) 178,
      (byte) 75,
      (byte) 231,
      (byte) 232,
      (byte) 5,
      (byte) 202,
      (byte) 189,
      (byte) 61,
      (byte) 122,
      (byte) 115,
      (byte) 30,
      (byte) 192 /*0xC0*/,
      (byte) 90,
      (byte) 191,
      (byte) 253,
      (byte) 196,
      (byte) 167,
      (byte) 227,
      (byte) 233,
      (byte) 77
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[31 /*0x1F*/] = (byte) 154;
    sourceArray2[1] = (byte) 196;
    sourceArray2[2] = (byte) 39;
    sourceArray2[3] = (byte) 205;
    sourceArray2[15] = (byte) 246;
    sourceArray2[33] = (byte) 151;
    sourceArray2[6] = (byte) 172;
    sourceArray2[7] = (byte) 149;
    sourceArray2[22] = (byte) 1;
    sourceArray2[28] = (byte) 148;
    sourceArray2[10] = (byte) 191;
    sourceArray2[9] = (byte) 18;
    sourceArray2[24] = (byte) 171;
    sourceArray2[23] = (byte) 241;
    sourceArray2[40] = (byte) 121;
    sourceArray2[12] = (byte) 146;
    sourceArray2[17] = (byte) 245;
    sourceArray2[30] = (byte) 72;
    sourceArray2[18] = (byte) 42;
    sourceArray2[38] = (byte) 142;
    sourceArray2[27] = (byte) 80 /*0x50*/;
    sourceArray2[19] = (byte) 68;
    sourceArray2[45] = (byte) 96 /*0x60*/;
    sourceArray2[29] = (byte) 174;
    sourceArray2[0] = (byte) 19;
    sourceArray2[25] = (byte) 6;
    sourceArray2[26] = (byte) 38;
    sourceArray2[8] = (byte) 29;
    sourceArray2[20] = (byte) 128 /*0x80*/;
    sourceArray2[5] = (byte) 135;
    sourceArray2[21] = (byte) 187;
    sourceArray2[13] = (byte) 104;
    sourceArray2[35] = (byte) 176 /*0xB0*/;
    sourceArray2[16 /*0x10*/] = (byte) 234;
    sourceArray2[4] = (byte) 62;
    sourceArray2[14] = (byte) 234;
    sourceArray2[11] = (byte) 34;
    sourceArray2[37] = (byte) 19;
    sourceArray2[43] = (byte) 66;
    sourceArray2[32 /*0x20*/] = (byte) 218;
    sourceArray2[36] = (byte) 55;
    sourceArray2[41] = (byte) 246;
    sourceArray2[42] = (byte) 10;
    sourceArray2[39] = (byte) 222;
    sourceArray2[44] = (byte) 240 /*0xF0*/;
    sourceArray2[34] = (byte) 163;
    sourceArray2[46] = (byte) 7;
    sourceArray2[47] = (byte) 28;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
