// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12693
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12693
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 235,
    (byte) 180,
    (byte) 119,
    (byte) 98,
    (byte) 182,
    (byte) 160 /*0xA0*/,
    (byte) 85,
    (byte) 137,
    (byte) 87,
    (byte) 119,
    (byte) 222,
    (byte) 242,
    (byte) 193,
    (byte) 9,
    (byte) 233,
    (byte) 80 /*0x50*/,
    (byte) 182,
    (byte) 231,
    (byte) 56,
    (byte) 204,
    (byte) 233,
    (byte) 103,
    (byte) 23,
    (byte) 245,
    (byte) 115,
    (byte) 126,
    (byte) 56,
    (byte) 190,
    (byte) 191,
    (byte) 58,
    (byte) 153,
    (byte) 172,
    (byte) 146,
    (byte) 98,
    (byte) 9,
    (byte) 204,
    (byte) 42,
    (byte) 14
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 92,
    (byte) 56,
    (byte) 115,
    (byte) 153,
    (byte) 205,
    (byte) 16 /*0x10*/,
    (byte) 55,
    (byte) 11,
    (byte) 80 /*0x50*/,
    (byte) 152,
    (byte) 118,
    (byte) 15,
    (byte) 129,
    (byte) 182,
    (byte) 3,
    (byte) 36,
    (byte) 89,
    (byte) 87,
    (byte) 185,
    (byte) 94,
    (byte) 118,
    (byte) 225,
    (byte) 137,
    (byte) 3,
    (byte) 125,
    (byte) 108,
    (byte) 159,
    (byte) 38,
    (byte) 162,
    (byte) 223,
    (byte) 234,
    (byte) 139,
    (byte) 72,
    (byte) 231,
    (byte) 221,
    (byte) 147,
    (byte) 60,
    (byte) 86
  };

  internal static int ssp_appserver_12694(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 162;
    sourceArray1[26] = (byte) 63 /*0x3F*/;
    sourceArray1[24] = (byte) 219;
    sourceArray1[3] = (byte) 164;
    sourceArray1[11] = (byte) 182;
    sourceArray1[2] = (byte) 59;
    sourceArray1[6] = (byte) 90;
    sourceArray1[40] = (byte) 0;
    sourceArray1[8] = (byte) 91;
    sourceArray1[9] = (byte) 240 /*0xF0*/;
    sourceArray1[29] = (byte) 51;
    sourceArray1[4] = (byte) 19;
    sourceArray1[37] = (byte) 87;
    sourceArray1[12] = (byte) 250;
    sourceArray1[14] = (byte) 113;
    sourceArray1[31 /*0x1F*/] = (byte) 76;
    sourceArray1[34] = (byte) 15;
    sourceArray1[45] = (byte) 94;
    sourceArray1[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    sourceArray1[1] = (byte) 77;
    sourceArray1[33] = (byte) 15;
    sourceArray1[35] = (byte) 122;
    sourceArray1[42] = (byte) 15;
    sourceArray1[17] = (byte) 187;
    sourceArray1[22] = (byte) 60;
    sourceArray1[20] = (byte) 254;
    sourceArray1[5] = (byte) 175;
    sourceArray1[27] = (byte) 107;
    sourceArray1[19] = (byte) 119;
    sourceArray1[46] = (byte) 127 /*0x7F*/;
    sourceArray1[30] = (byte) 243;
    sourceArray1[7] = (byte) 64 /*0x40*/;
    sourceArray1[21] = (byte) 121;
    sourceArray1[16 /*0x10*/] = (byte) 35;
    sourceArray1[0] = (byte) 142;
    sourceArray1[23] = (byte) 27;
    sourceArray1[36] = (byte) 146;
    sourceArray1[38] = (byte) 192 /*0xC0*/;
    sourceArray1[25] = (byte) 226;
    sourceArray1[39] = (byte) 212;
    sourceArray1[28] = (byte) 233;
    sourceArray1[41] = (byte) 227;
    sourceArray1[10] = (byte) 48 /*0x30*/;
    sourceArray1[43] = (byte) 162;
    sourceArray1[44] = (byte) 238;
    sourceArray1[18] = (byte) 133;
    sourceArray1[13] = (byte) 111;
    sourceArray1[47] = (byte) 15;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 38,
      (byte) 220,
      (byte) 145,
      (byte) 213,
      (byte) 82,
      (byte) 139,
      (byte) 196,
      (byte) 100,
      (byte) 51,
      (byte) 26,
      (byte) 214,
      (byte) 18,
      (byte) 168,
      (byte) 147,
      (byte) 37,
      (byte) 50,
      (byte) 85,
      (byte) 46,
      (byte) 150,
      (byte) 89,
      (byte) 250,
      (byte) 135,
      (byte) 248,
      (byte) 191,
      (byte) 185,
      (byte) 7,
      (byte) 143,
      (byte) 224 /*0xE0*/,
      (byte) 114,
      (byte) 227,
      (byte) 186,
      (byte) 56,
      (byte) 41,
      (byte) 135,
      (byte) 187,
      (byte) 144 /*0x90*/,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 89,
      (byte) 186,
      (byte) 236,
      (byte) 254,
      (byte) 122,
      (byte) 227,
      (byte) 38,
      (byte) 204,
      (byte) 19,
      (byte) 130
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12695(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 214;
    sourceArray1[1] = (byte) 175;
    sourceArray1[2] = (byte) 63 /*0x3F*/;
    sourceArray1[17] = (byte) 13;
    sourceArray1[4] = (byte) 77;
    sourceArray1[5] = (byte) 45;
    sourceArray1[33] = (byte) 228;
    sourceArray1[42] = (byte) 165;
    sourceArray1[8] = (byte) 39;
    sourceArray1[9] = (byte) 171;
    sourceArray1[10] = (byte) 118;
    sourceArray1[28] = (byte) 101;
    sourceArray1[3] = (byte) 140;
    sourceArray1[35] = (byte) 127 /*0x7F*/;
    sourceArray1[0] = (byte) 149;
    sourceArray1[13] = (byte) 205;
    sourceArray1[25] = (byte) 60;
    sourceArray1[32 /*0x20*/] = (byte) 35;
    sourceArray1[18] = (byte) 49;
    sourceArray1[19] = (byte) 132;
    sourceArray1[20] = (byte) 36;
    sourceArray1[21] = (byte) 29;
    sourceArray1[16 /*0x10*/] = (byte) 10;
    sourceArray1[23] = (byte) 141;
    sourceArray1[15] = (byte) 185;
    sourceArray1[37] = (byte) 192 /*0xC0*/;
    sourceArray1[11] = (byte) 241;
    sourceArray1[30] = (byte) 129;
    sourceArray1[24] = (byte) 157;
    sourceArray1[29] = (byte) 143;
    sourceArray1[12] = (byte) 75;
    sourceArray1[36] = (byte) 245;
    sourceArray1[26] = (byte) 107;
    sourceArray1[41] = (byte) 203;
    sourceArray1[34] = (byte) 205;
    sourceArray1[27] = (byte) 75;
    sourceArray1[45] = (byte) 128 /*0x80*/;
    sourceArray1[31 /*0x1F*/] = (byte) 244;
    sourceArray1[38] = (byte) 204;
    sourceArray1[39] = (byte) 158;
    sourceArray1[40] = (byte) 36;
    sourceArray1[14] = (byte) 64 /*0x40*/;
    sourceArray1[22] = (byte) 101;
    sourceArray1[43] = (byte) 78;
    sourceArray1[44] = (byte) 243;
    sourceArray1[6] = (byte) 59;
    sourceArray1[46] = (byte) 67;
    sourceArray1[47] = (byte) 194;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[11] = (byte) 107;
    sourceArray2[16 /*0x10*/] = (byte) 38;
    sourceArray2[1] = (byte) 25;
    sourceArray2[41] = (byte) 201;
    sourceArray2[32 /*0x20*/] = (byte) 235;
    sourceArray2[0] = (byte) 176 /*0xB0*/;
    sourceArray2[36] = (byte) 76;
    sourceArray2[13] = (byte) 208 /*0xD0*/;
    sourceArray2[25] = (byte) 50;
    sourceArray2[9] = (byte) 80 /*0x50*/;
    sourceArray2[46] = (byte) 213;
    sourceArray2[6] = (byte) 139;
    sourceArray2[12] = (byte) 41;
    sourceArray2[38] = (byte) 81;
    sourceArray2[3] = (byte) 69;
    sourceArray2[15] = (byte) 32 /*0x20*/;
    sourceArray2[2] = (byte) 157;
    sourceArray2[17] = (byte) 61;
    sourceArray2[18] = (byte) 228;
    sourceArray2[19] = (byte) 240 /*0xF0*/;
    sourceArray2[14] = (byte) 30;
    sourceArray2[20] = (byte) 224 /*0xE0*/;
    sourceArray2[22] = (byte) 241;
    sourceArray2[23] = (byte) 1;
    sourceArray2[24] = (byte) 79;
    sourceArray2[40] = (byte) 2;
    sourceArray2[26] = (byte) 220;
    sourceArray2[44] = (byte) 98;
    sourceArray2[42] = (byte) 205;
    sourceArray2[4] = (byte) 58;
    sourceArray2[35] = (byte) 157;
    sourceArray2[27] = (byte) 181;
    sourceArray2[10] = (byte) 117;
    sourceArray2[33] = (byte) 37;
    sourceArray2[34] = (byte) 153;
    sourceArray2[31 /*0x1F*/] = (byte) 134;
    sourceArray2[30] = (byte) 64 /*0x40*/;
    sourceArray2[37] = (byte) 161;
    sourceArray2[5] = (byte) 208 /*0xD0*/;
    sourceArray2[39] = (byte) 37;
    sourceArray2[28] = (byte) 54;
    sourceArray2[29] = (byte) 6;
    sourceArray2[7] = (byte) 252;
    sourceArray2[43] = (byte) 135;
    sourceArray2[21] = (byte) 205;
    sourceArray2[45] = (byte) 82;
    sourceArray2[8] = (byte) 121;
    sourceArray2[47] = (byte) 105;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12696()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 165,
        (byte) 196,
        (byte) 106,
        (byte) 245,
        (byte) 129,
        (byte) 161,
        (byte) 113,
        (byte) 130,
        (byte) 229,
        (byte) 25
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 227;
      numArray3[9] = (byte) 108;
      numArray3[6] = (byte) 93;
      numArray3[3] = (byte) 188;
      numArray3[4] = (byte) 18;
      numArray3[2] = (byte) 243;
      numArray3[0] = (byte) 100;
      numArray3[5] = (byte) 67;
      numArray3[8] = (byte) 214;
      numArray3[7] = (byte) 23;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_12693.sspq, 0, (Array) numArray4, 0, 38);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12693.sspr, 0, (Array) numArray4, 0, 38);
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
      (byte) 204,
      (byte) 68,
      (byte) 196,
      (byte) 79,
      (byte) 186,
      (byte) 71,
      (byte) 56,
      (byte) 57,
      (byte) 57,
      (byte) 19
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 249,
      (byte) 2,
      (byte) 135,
      (byte) 54,
      (byte) 89,
      (byte) 246,
      (byte) 193,
      (byte) 12,
      (byte) 240 /*0xF0*/,
      (byte) 240 /*0xF0*/
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12697(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 103;
    sourceArray1[1] = (byte) 30;
    sourceArray1[30] = (byte) 206;
    sourceArray1[3] = (byte) 157;
    sourceArray1[4] = (byte) 129;
    sourceArray1[23] = (byte) 173;
    sourceArray1[6] = (byte) 55;
    sourceArray1[38] = (byte) 168;
    sourceArray1[15] = byte.MaxValue;
    sourceArray1[43] = (byte) 133;
    sourceArray1[10] = (byte) 28;
    sourceArray1[9] = (byte) 201;
    sourceArray1[12] = (byte) 50;
    sourceArray1[44] = (byte) 157;
    sourceArray1[14] = (byte) 94;
    sourceArray1[0] = (byte) 101;
    sourceArray1[31 /*0x1F*/] = (byte) 1;
    sourceArray1[26] = (byte) 58;
    sourceArray1[18] = (byte) 168;
    sourceArray1[19] = (byte) 27;
    sourceArray1[20] = (byte) 244;
    sourceArray1[25] = (byte) 189;
    sourceArray1[16 /*0x10*/] = (byte) 28;
    sourceArray1[42] = (byte) 45;
    sourceArray1[24] = (byte) 207;
    sourceArray1[13] = (byte) 95;
    sourceArray1[47] = (byte) 217;
    sourceArray1[27] = (byte) 69;
    sourceArray1[28] = (byte) 37;
    sourceArray1[29] = (byte) 17;
    sourceArray1[5] = (byte) 238;
    sourceArray1[37] = (byte) 47;
    sourceArray1[32 /*0x20*/] = (byte) 26;
    sourceArray1[21] = (byte) 8;
    sourceArray1[34] = (byte) 45;
    sourceArray1[35] = (byte) 68;
    sourceArray1[36] = (byte) 134;
    sourceArray1[11] = (byte) 221;
    sourceArray1[17] = (byte) 38;
    sourceArray1[8] = (byte) 150;
    sourceArray1[40] = (byte) 148;
    sourceArray1[46] = (byte) 5;
    sourceArray1[39] = (byte) 31 /*0x1F*/;
    sourceArray1[33] = (byte) 235;
    sourceArray1[41] = (byte) 220;
    sourceArray1[45] = (byte) 106;
    sourceArray1[22] = (byte) 153;
    sourceArray1[2] = (byte) 131;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 72;
    sourceArray2[1] = (byte) 195;
    sourceArray2[46] = (byte) 183;
    sourceArray2[3] = (byte) 114;
    sourceArray2[4] = (byte) 92;
    sourceArray2[30] = (byte) 248;
    sourceArray2[6] = (byte) 83;
    sourceArray2[40] = (byte) 56;
    sourceArray2[8] = (byte) 133;
    sourceArray2[24] = (byte) 236;
    sourceArray2[18] = (byte) 211;
    sourceArray2[11] = (byte) 142;
    sourceArray2[12] = (byte) 13;
    sourceArray2[41] = (byte) 163;
    sourceArray2[14] = (byte) 27;
    sourceArray2[15] = (byte) 137;
    sourceArray2[21] = (byte) 253;
    sourceArray2[0] = (byte) 110;
    sourceArray2[42] = (byte) 21;
    sourceArray2[31 /*0x1F*/] = (byte) 216;
    sourceArray2[20] = (byte) 36;
    sourceArray2[13] = (byte) 24;
    sourceArray2[5] = (byte) 114;
    sourceArray2[23] = (byte) 59;
    sourceArray2[36] = (byte) 95;
    sourceArray2[10] = (byte) 197;
    sourceArray2[26] = (byte) 44;
    sourceArray2[27] = (byte) 61;
    sourceArray2[28] = (byte) 131;
    sourceArray2[29] = (byte) 0;
    sourceArray2[22] = (byte) 23;
    sourceArray2[34] = (byte) 212;
    sourceArray2[32 /*0x20*/] = (byte) 121;
    sourceArray2[33] = (byte) 43;
    sourceArray2[17] = (byte) 247;
    sourceArray2[35] = (byte) 73;
    sourceArray2[16 /*0x10*/] = (byte) 59;
    sourceArray2[7] = (byte) 196;
    sourceArray2[47] = (byte) 183;
    sourceArray2[39] = (byte) 142;
    sourceArray2[38] = (byte) 221;
    sourceArray2[25] = (byte) 184;
    sourceArray2[43] = (byte) 148;
    sourceArray2[37] = (byte) 54;
    sourceArray2[9] = (byte) 163;
    sourceArray2[45] = (byte) 166;
    sourceArray2[2] = (byte) 73;
    sourceArray2[44] = (byte) 5;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
