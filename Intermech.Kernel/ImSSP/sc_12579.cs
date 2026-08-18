// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12579
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12579
{
  private static byte[] sspq = new byte[20]
  {
    (byte) 94,
    (byte) 103,
    (byte) 251,
    (byte) 31 /*0x1F*/,
    (byte) 159,
    (byte) 231,
    (byte) 210,
    (byte) 97,
    (byte) 88,
    (byte) 49,
    (byte) 123,
    (byte) 242,
    (byte) 108,
    (byte) 190,
    (byte) 28,
    (byte) 196,
    (byte) 136,
    (byte) 6,
    (byte) 108,
    (byte) 223
  };
  private static byte[] sspr = new byte[20]
  {
    (byte) 16 /*0x10*/,
    (byte) 230,
    (byte) 42,
    (byte) 181,
    (byte) 124,
    (byte) 252,
    (byte) 220,
    (byte) 252,
    (byte) 138,
    (byte) 150,
    (byte) 129,
    (byte) 49,
    (byte) 217,
    (byte) 191,
    (byte) 246,
    (byte) 7,
    (byte) 166,
    (byte) 117,
    (byte) 89,
    (byte) 89
  };

  internal static string ssp_appserver_12580()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[56];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 126;
      numArray2[0] = (byte) 176 /*0xB0*/;
      numArray2[26] = (byte) 86;
      numArray2[3] = (byte) 56;
      numArray2[28] = (byte) 155;
      numArray2[27] = (byte) 16 /*0x10*/;
      numArray2[15] = (byte) 0;
      numArray2[7] = (byte) 105;
      numArray2[50] = (byte) 218;
      numArray2[9] = (byte) 209;
      numArray2[10] = (byte) 20;
      numArray2[11] = (byte) 132;
      numArray2[47] = (byte) 154;
      numArray2[13] = (byte) 172;
      numArray2[14] = (byte) 54;
      numArray2[41] = (byte) 124;
      numArray2[37] = (byte) 252;
      numArray2[17] = (byte) 21;
      numArray2[53] = (byte) 241;
      numArray2[19] = (byte) 104;
      numArray2[54] = (byte) 15;
      numArray2[21] = (byte) 56;
      numArray2[6] = (byte) 40;
      numArray2[18] = (byte) 67;
      numArray2[5] = (byte) 115;
      numArray2[25] = (byte) 119;
      numArray2[12] = (byte) 70;
      numArray2[38] = (byte) 47;
      numArray2[22] = (byte) 109;
      numArray2[42] = (byte) 0;
      numArray2[20] = byte.MaxValue;
      numArray2[31 /*0x1F*/] = (byte) 17;
      numArray2[32 /*0x20*/] = (byte) 13;
      numArray2[33] = (byte) 183;
      numArray2[23] = (byte) 248;
      numArray2[35] = (byte) 64 /*0x40*/;
      numArray2[36] = (byte) 141;
      numArray2[24] = (byte) 169;
      numArray2[1] = (byte) 66;
      numArray2[39] = (byte) 50;
      numArray2[40] = (byte) 43;
      numArray2[49] = (byte) 33;
      numArray2[34] = (byte) 65;
      numArray2[30] = (byte) 157;
      numArray2[44] = (byte) 28;
      numArray2[2] = (byte) 55;
      numArray2[46] = (byte) 100;
      numArray2[51] = (byte) 58;
      numArray2[48 /*0x30*/] = (byte) 101;
      numArray2[4] = (byte) 192 /*0xC0*/;
      numArray2[45] = (byte) 177;
      numArray2[8] = (byte) 254;
      numArray2[52] = (byte) 184;
      numArray2[16 /*0x10*/] = (byte) 41;
      numArray2[43] = (byte) 216;
      byte[] numArray3 = new byte[55];
      numArray3[52] = (byte) 122;
      numArray3[1] = (byte) 83;
      numArray3[30] = (byte) 13;
      numArray3[16 /*0x10*/] = (byte) 62;
      numArray3[24] = (byte) 77;
      numArray3[13] = (byte) 105;
      numArray3[54] = (byte) 216;
      numArray3[10] = (byte) 197;
      numArray3[34] = (byte) 83;
      numArray3[9] = (byte) 11;
      numArray3[22] = (byte) 68;
      numArray3[11] = (byte) 55;
      numArray3[41] = (byte) 91;
      numArray3[38] = (byte) 214;
      numArray3[42] = (byte) 43;
      numArray3[8] = (byte) 242;
      numArray3[12] = (byte) 237;
      numArray3[17] = (byte) 142;
      numArray3[18] = (byte) 172;
      numArray3[19] = (byte) 136;
      numArray3[45] = (byte) 138;
      numArray3[53] = (byte) 171;
      numArray3[0] = (byte) 155;
      numArray3[28] = (byte) 254;
      numArray3[51] = (byte) 147;
      numArray3[15] = (byte) 25;
      numArray3[5] = (byte) 102;
      numArray3[27] = (byte) 23;
      numArray3[23] = (byte) 60;
      numArray3[25] = (byte) 15;
      numArray3[14] = (byte) 254;
      numArray3[31 /*0x1F*/] = (byte) 118;
      numArray3[26] = (byte) 13;
      numArray3[3] = (byte) 143;
      numArray3[36] = (byte) 235;
      numArray3[6] = (byte) 247;
      numArray3[39] = (byte) 202;
      numArray3[37] = (byte) 93;
      numArray3[46] = (byte) 200;
      numArray3[7] = (byte) 213;
      numArray3[29] = (byte) 149;
      numArray3[32 /*0x20*/] = (byte) 241;
      numArray3[2] = (byte) 14;
      numArray3[43] = (byte) 124;
      numArray3[44] = (byte) 115;
      numArray3[47] = (byte) 235;
      numArray3[40] = (byte) 223;
      numArray3[4] = (byte) 44;
      numArray3[48 /*0x30*/] = (byte) 43;
      numArray3[49] = (byte) 17;
      numArray3[33] = (byte) 218;
      numArray3[20] = (byte) 97;
      numArray3[21] = (byte) 183;
      numArray3[50] = (byte) 82;
      numArray3[35] = (byte) 52;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[1]{ (byte) 161 };
      byte[] numArray5 = new byte[1]{ (byte) 220 };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[56];
    byte[] numArray7 = new byte[55];
    numArray7[44] = (byte) 4;
    numArray7[13] = (byte) 132;
    numArray7[30] = (byte) 73;
    numArray7[3] = (byte) 195;
    numArray7[29] = (byte) 217;
    numArray7[5] = (byte) 243;
    numArray7[52] = (byte) 252;
    numArray7[46] = (byte) 100;
    numArray7[8] = (byte) 97;
    numArray7[39] = (byte) 236;
    numArray7[6] = (byte) 201;
    numArray7[11] = (byte) 81;
    numArray7[7] = (byte) 25;
    numArray7[21] = (byte) 87;
    numArray7[0] = (byte) 50;
    numArray7[2] = (byte) 229;
    numArray7[24] = (byte) 185;
    numArray7[17] = (byte) 83;
    numArray7[18] = (byte) 221;
    numArray7[48 /*0x30*/] = (byte) 68;
    numArray7[19] = (byte) 75;
    numArray7[51] = (byte) 78;
    numArray7[22] = (byte) 65;
    numArray7[23] = (byte) 8;
    numArray7[25] = (byte) 4;
    numArray7[54] = (byte) 213;
    numArray7[36] = (byte) 192 /*0xC0*/;
    numArray7[31 /*0x1F*/] = (byte) 251;
    numArray7[28] = (byte) 254;
    numArray7[12] = (byte) 71;
    numArray7[1] = (byte) 134;
    numArray7[50] = (byte) 50;
    numArray7[32 /*0x20*/] = (byte) 179;
    numArray7[33] = (byte) 222;
    numArray7[15] = (byte) 17;
    numArray7[35] = (byte) 214;
    numArray7[34] = (byte) 115;
    numArray7[37] = (byte) 14;
    numArray7[14] = (byte) 117;
    numArray7[10] = (byte) 233;
    numArray7[40] = (byte) 38;
    numArray7[41] = (byte) 116;
    numArray7[42] = (byte) 130;
    numArray7[43] = (byte) 81;
    numArray7[26] = (byte) 222;
    numArray7[45] = (byte) 2;
    numArray7[16 /*0x10*/] = (byte) 29;
    numArray7[38] = (byte) 117;
    numArray7[27] = (byte) 97;
    numArray7[49] = (byte) 120;
    numArray7[20] = byte.MaxValue;
    numArray7[4] = (byte) 249;
    numArray7[47] = (byte) 4;
    numArray7[53] = (byte) 175;
    numArray7[9] = (byte) 56;
    byte[] numArray8 = new byte[55];
    numArray8[35] = (byte) 44;
    numArray8[1] = (byte) 98;
    numArray8[5] = (byte) 154;
    numArray8[47] = (byte) 70;
    numArray8[4] = (byte) 164;
    numArray8[39] = (byte) 227;
    numArray8[36] = (byte) 177;
    numArray8[2] = (byte) 116;
    numArray8[13] = (byte) 254;
    numArray8[9] = (byte) 67;
    numArray8[3] = (byte) 90;
    numArray8[11] = (byte) 223;
    numArray8[12] = (byte) 41;
    numArray8[17] = (byte) 200;
    numArray8[30] = (byte) 72;
    numArray8[14] = (byte) 137;
    numArray8[16 /*0x10*/] = (byte) 56;
    numArray8[28] = (byte) 207;
    numArray8[22] = (byte) 231;
    numArray8[19] = (byte) 190;
    numArray8[8] = (byte) 82;
    numArray8[54] = (byte) 100;
    numArray8[0] = (byte) 193;
    numArray8[23] = (byte) 89;
    numArray8[41] = (byte) 102;
    numArray8[45] = (byte) 123;
    numArray8[10] = (byte) 68;
    numArray8[27] = (byte) 144 /*0x90*/;
    numArray8[40] = (byte) 119;
    numArray8[29] = (byte) 249;
    numArray8[34] = (byte) 15;
    numArray8[31 /*0x1F*/] = (byte) 96 /*0x60*/;
    numArray8[44] = (byte) 86;
    numArray8[20] = (byte) 117;
    numArray8[7] = (byte) 171;
    numArray8[33] = (byte) 81;
    numArray8[21] = (byte) 160 /*0xA0*/;
    numArray8[25] = (byte) 240 /*0xF0*/;
    numArray8[38] = (byte) 47;
    numArray8[26] = (byte) 222;
    numArray8[49] = (byte) 220;
    numArray8[51] = (byte) 240 /*0xF0*/;
    numArray8[42] = (byte) 118;
    numArray8[43] = (byte) 85;
    numArray8[50] = (byte) 165;
    numArray8[18] = (byte) 124;
    numArray8[46] = (byte) 146;
    numArray8[37] = (byte) 68;
    numArray8[48 /*0x30*/] = (byte) 202;
    numArray8[24] = (byte) 231;
    numArray8[32 /*0x20*/] = (byte) 75;
    numArray8[6] = (byte) 8;
    numArray8[52] = (byte) 209;
    numArray8[53] = (byte) 203;
    numArray8[15] = (byte) 209;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[1]{ (byte) 234 };
    byte[] numArray10 = new byte[1]{ (byte) 60 };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 1);
    for (int index = 0; index < 1; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_12579.sspq, 0, (Array) numArray11, 0, 20);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12579.sspr, 0, (Array) numArray11, 0, 20);
    for (int index = 0; index < numArray11.Length; ++index)
    {
      if ((int) numArray11[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray6);
  }
}
