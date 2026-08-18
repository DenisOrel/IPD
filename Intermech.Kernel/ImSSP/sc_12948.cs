// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12948
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12948
{
  private static byte[] sspq = new byte[42]
  {
    (byte) 219,
    (byte) 251,
    (byte) 4,
    (byte) 88,
    (byte) 90,
    (byte) 5,
    (byte) 251,
    (byte) 243,
    (byte) 228,
    (byte) 175,
    (byte) 161,
    (byte) 230,
    (byte) 28,
    (byte) 56,
    (byte) 38,
    (byte) 236,
    byte.MaxValue,
    (byte) 47,
    (byte) 47,
    (byte) 201,
    (byte) 204,
    (byte) 22,
    (byte) 140,
    (byte) 238,
    (byte) 40,
    (byte) 5,
    (byte) 174,
    (byte) 64 /*0x40*/,
    (byte) 119,
    (byte) 135,
    (byte) 193,
    (byte) 130,
    (byte) 26,
    (byte) 93,
    (byte) 163,
    (byte) 159,
    (byte) 139,
    (byte) 42,
    (byte) 216,
    (byte) 242,
    (byte) 41,
    (byte) 157
  };
  private static byte[] sspr = new byte[42]
  {
    (byte) 244,
    (byte) 51,
    (byte) 164,
    (byte) 131,
    (byte) 197,
    (byte) 183,
    (byte) 39,
    (byte) 118,
    (byte) 87,
    (byte) 65,
    (byte) 224 /*0xE0*/,
    (byte) 162,
    (byte) 3,
    (byte) 160 /*0xA0*/,
    (byte) 150,
    (byte) 175,
    (byte) 8,
    (byte) 98,
    (byte) 135,
    (byte) 160 /*0xA0*/,
    (byte) 200,
    (byte) 115,
    (byte) 15,
    (byte) 43,
    (byte) 135,
    (byte) 216,
    (byte) 28,
    (byte) 31 /*0x1F*/,
    (byte) 19,
    (byte) 129,
    (byte) 14,
    (byte) 114,
    (byte) 171,
    (byte) 92,
    (byte) 246,
    (byte) 120,
    (byte) 43,
    (byte) 84,
    (byte) 23,
    (byte) 95,
    (byte) 162,
    (byte) 93
  };

  internal static int ssp_appserver_12949(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[9] = (byte) 110;
    sourceArray1[1] = (byte) 58;
    sourceArray1[31 /*0x1F*/] = (byte) 139;
    sourceArray1[44] = (byte) 6;
    sourceArray1[39] = (byte) 199;
    sourceArray1[5] = (byte) 167;
    sourceArray1[6] = (byte) 249;
    sourceArray1[3] = (byte) 6;
    sourceArray1[8] = (byte) 94;
    sourceArray1[26] = (byte) 11;
    sourceArray1[10] = (byte) 18;
    sourceArray1[11] = (byte) 93;
    sourceArray1[38] = (byte) 70;
    sourceArray1[7] = (byte) 102;
    sourceArray1[22] = (byte) 21;
    sourceArray1[15] = (byte) 109;
    sourceArray1[12] = (byte) 37;
    sourceArray1[28] = (byte) 7;
    sourceArray1[41] = byte.MaxValue;
    sourceArray1[13] = (byte) 75;
    sourceArray1[4] = (byte) 121;
    sourceArray1[21] = (byte) 30;
    sourceArray1[2] = (byte) 58;
    sourceArray1[23] = (byte) 169;
    sourceArray1[24] = (byte) 157;
    sourceArray1[18] = (byte) 142;
    sourceArray1[29] = (byte) 191;
    sourceArray1[27] = (byte) 231;
    sourceArray1[25] = (byte) 80 /*0x50*/;
    sourceArray1[16 /*0x10*/] = (byte) 56;
    sourceArray1[30] = (byte) 95;
    sourceArray1[14] = (byte) 164;
    sourceArray1[32 /*0x20*/] = (byte) 71;
    sourceArray1[33] = (byte) 121;
    sourceArray1[35] = (byte) 220;
    sourceArray1[19] = (byte) 249;
    sourceArray1[36] = (byte) 118;
    sourceArray1[37] = (byte) 88;
    sourceArray1[34] = (byte) 117;
    sourceArray1[42] = (byte) 197;
    sourceArray1[40] = (byte) 189;
    sourceArray1[20] = (byte) 35;
    sourceArray1[17] = (byte) 226;
    sourceArray1[43] = (byte) 22;
    sourceArray1[45] = (byte) 145;
    sourceArray1[0] = (byte) 218;
    sourceArray1[46] = (byte) 104;
    sourceArray1[47] = (byte) 158;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[34] = (byte) 158;
    sourceArray2[1] = (byte) 238;
    sourceArray2[7] = (byte) 166;
    sourceArray2[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray2[4] = (byte) 140;
    sourceArray2[5] = (byte) 181;
    sourceArray2[6] = (byte) 129;
    sourceArray2[24] = (byte) 117;
    sourceArray2[17] = (byte) 65;
    sourceArray2[38] = (byte) 112 /*0x70*/;
    sourceArray2[10] = (byte) 143;
    sourceArray2[9] = (byte) 16 /*0x10*/;
    sourceArray2[12] = (byte) 44;
    sourceArray2[11] = (byte) 150;
    sourceArray2[14] = (byte) 99;
    sourceArray2[15] = (byte) 123;
    sourceArray2[16 /*0x10*/] = (byte) 88;
    sourceArray2[2] = (byte) 248;
    sourceArray2[18] = (byte) 68;
    sourceArray2[19] = (byte) 25;
    sourceArray2[28] = (byte) 74;
    sourceArray2[21] = (byte) 50;
    sourceArray2[36] = (byte) 247;
    sourceArray2[23] = (byte) 142;
    sourceArray2[44] = (byte) 64 /*0x40*/;
    sourceArray2[25] = (byte) 250;
    sourceArray2[30] = (byte) 172;
    sourceArray2[0] = (byte) 254;
    sourceArray2[33] = (byte) 178;
    sourceArray2[32 /*0x20*/] = (byte) 241;
    sourceArray2[29] = (byte) 34;
    sourceArray2[22] = (byte) 200;
    sourceArray2[20] = (byte) 9;
    sourceArray2[13] = (byte) 101;
    sourceArray2[42] = (byte) 154;
    sourceArray2[27] = (byte) 125;
    sourceArray2[3] = (byte) 157;
    sourceArray2[37] = (byte) 146;
    sourceArray2[35] = (byte) 130;
    sourceArray2[39] = (byte) 137;
    sourceArray2[40] = (byte) 204;
    sourceArray2[41] = (byte) 125;
    sourceArray2[26] = (byte) 19;
    sourceArray2[43] = (byte) 135;
    sourceArray2[8] = (byte) 90;
    sourceArray2[45] = (byte) 204;
    sourceArray2[46] = (byte) 21;
    sourceArray2[47] = (byte) 223;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_12948.sspq, 0, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12948.sspr, 0, (Array) numArray2, 0, 42);
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

  internal static int ssp_appserver_12950(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 56,
      (byte) 169,
      (byte) 27,
      (byte) 28,
      (byte) 35,
      (byte) 82,
      (byte) 41,
      (byte) 96 /*0x60*/,
      (byte) 45,
      (byte) 198,
      (byte) 196,
      (byte) 111,
      (byte) 22,
      (byte) 48 /*0x30*/,
      (byte) 84,
      (byte) 86,
      (byte) 186,
      (byte) 123,
      (byte) 93,
      (byte) 237,
      (byte) 31 /*0x1F*/,
      (byte) 204,
      (byte) 190,
      (byte) 149,
      (byte) 134,
      (byte) 96 /*0x60*/,
      (byte) 212,
      (byte) 87,
      (byte) 60,
      (byte) 160 /*0xA0*/,
      (byte) 47,
      (byte) 208 /*0xD0*/,
      (byte) 153,
      (byte) 215,
      (byte) 240 /*0xF0*/,
      (byte) 92,
      (byte) 146,
      (byte) 36,
      (byte) 83,
      (byte) 53,
      (byte) 152,
      (byte) 33,
      (byte) 237,
      (byte) 150,
      (byte) 222,
      (byte) 48 /*0x30*/,
      (byte) 117
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 93;
    sourceArray2[41] = (byte) 39;
    sourceArray2[7] = (byte) 63 /*0x3F*/;
    sourceArray2[3] = (byte) 201;
    sourceArray2[21] = (byte) 177;
    sourceArray2[38] = (byte) 22;
    sourceArray2[33] = (byte) 238;
    sourceArray2[5] = (byte) 84;
    sourceArray2[1] = (byte) 107;
    sourceArray2[9] = (byte) 168;
    sourceArray2[44] = (byte) 222;
    sourceArray2[15] = (byte) 130;
    sourceArray2[4] = (byte) 169;
    sourceArray2[6] = (byte) 56;
    sourceArray2[11] = (byte) 99;
    sourceArray2[32 /*0x20*/] = (byte) 49;
    sourceArray2[10] = (byte) 153;
    sourceArray2[47] = (byte) 128 /*0x80*/;
    sourceArray2[18] = (byte) 95;
    sourceArray2[19] = (byte) 25;
    sourceArray2[46] = (byte) 108;
    sourceArray2[22] = (byte) 191;
    sourceArray2[2] = (byte) 44;
    sourceArray2[23] = (byte) 36;
    sourceArray2[24] = (byte) 147;
    sourceArray2[25] = (byte) 115;
    sourceArray2[26] = (byte) 143;
    sourceArray2[27] = (byte) 36;
    sourceArray2[0] = (byte) 241;
    sourceArray2[13] = (byte) 155;
    sourceArray2[30] = (byte) 98;
    sourceArray2[31 /*0x1F*/] = (byte) 90;
    sourceArray2[14] = (byte) 39;
    sourceArray2[36] = (byte) 34;
    sourceArray2[34] = (byte) 181;
    sourceArray2[35] = (byte) 201;
    sourceArray2[12] = (byte) 32 /*0x20*/;
    sourceArray2[16 /*0x10*/] = (byte) 143;
    sourceArray2[28] = (byte) 183;
    sourceArray2[45] = (byte) 250;
    sourceArray2[8] = (byte) 3;
    sourceArray2[39] = (byte) 213;
    sourceArray2[37] = (byte) 163;
    sourceArray2[43] = (byte) 152;
    sourceArray2[40] = (byte) 245;
    sourceArray2[29] = (byte) 206;
    sourceArray2[42] = (byte) 117;
    sourceArray2[17] = (byte) 7;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
