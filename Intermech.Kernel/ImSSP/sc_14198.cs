// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14198
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14198
{
  internal static string ssp_appserver_14199()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[54];
      byte[] numArray2 = new byte[54]
      {
        (byte) 29,
        (byte) 198,
        (byte) 191,
        (byte) 241,
        (byte) 80 /*0x50*/,
        (byte) 175,
        (byte) 239,
        (byte) 125,
        (byte) 247,
        (byte) 186,
        (byte) 198,
        (byte) 25,
        (byte) 3,
        (byte) 99,
        (byte) 82,
        (byte) 74,
        (byte) 228,
        (byte) 224 /*0xE0*/,
        (byte) 32 /*0x20*/,
        (byte) 254,
        (byte) 231,
        (byte) 244,
        (byte) 55,
        (byte) 36,
        (byte) 32 /*0x20*/,
        (byte) 73,
        (byte) 35,
        (byte) 43,
        (byte) 109,
        (byte) 89,
        (byte) 100,
        (byte) 79,
        (byte) 204,
        (byte) 125,
        (byte) 106,
        (byte) 117,
        (byte) 10,
        (byte) 94,
        (byte) 76,
        (byte) 175,
        (byte) 204,
        (byte) 126,
        (byte) 205,
        (byte) 138,
        (byte) 113,
        (byte) 8,
        (byte) 230,
        (byte) 235,
        (byte) 31 /*0x1F*/,
        (byte) 95,
        (byte) 228,
        (byte) 41,
        (byte) 63 /*0x3F*/,
        (byte) 19
      };
      byte[] numArray3 = new byte[54]
      {
        (byte) 83,
        (byte) 114,
        (byte) 215,
        (byte) 47,
        (byte) 240 /*0xF0*/,
        (byte) 154,
        (byte) 235,
        (byte) 95,
        (byte) 134,
        byte.MaxValue,
        (byte) 176 /*0xB0*/,
        (byte) 162,
        (byte) 51,
        (byte) 106,
        (byte) 5,
        (byte) 196,
        (byte) 137,
        (byte) 176 /*0xB0*/,
        (byte) 186,
        (byte) 174,
        (byte) 146,
        (byte) 218,
        (byte) 221,
        (byte) 116,
        (byte) 141,
        (byte) 43,
        (byte) 206,
        (byte) 97,
        (byte) 133,
        (byte) 206,
        (byte) 91,
        (byte) 40,
        (byte) 88,
        (byte) 30,
        (byte) 119,
        (byte) 240 /*0xF0*/,
        (byte) 1,
        (byte) 114,
        (byte) 232,
        (byte) 222,
        (byte) 25,
        (byte) 58,
        (byte) 43,
        (byte) 199,
        (byte) 47,
        (byte) 228,
        (byte) 249,
        (byte) 199,
        (byte) 147,
        (byte) 42,
        (byte) 183,
        (byte) 175,
        (byte) 6,
        (byte) 190
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[54];
    byte[] numArray5 = new byte[54]
    {
      (byte) 154,
      (byte) 209,
      (byte) 7,
      (byte) 139,
      (byte) 195,
      (byte) 16 /*0x10*/,
      (byte) 158,
      (byte) 85,
      (byte) 118,
      (byte) 76,
      (byte) 133,
      (byte) 248,
      (byte) 221,
      (byte) 63 /*0x3F*/,
      (byte) 134,
      (byte) 148,
      (byte) 112 /*0x70*/,
      (byte) 49,
      (byte) 105,
      (byte) 39,
      (byte) 173,
      (byte) 178,
      (byte) 23,
      (byte) 98,
      (byte) 56,
      (byte) 12,
      (byte) 14,
      (byte) 116,
      (byte) 59,
      (byte) 16 /*0x10*/,
      (byte) 185,
      (byte) 171,
      (byte) 65,
      (byte) 230,
      (byte) 112 /*0x70*/,
      (byte) 141,
      (byte) 69,
      (byte) 35,
      (byte) 9,
      (byte) 110,
      (byte) 46,
      (byte) 52,
      (byte) 251,
      (byte) 187,
      (byte) 96 /*0x60*/,
      (byte) 243,
      (byte) 65,
      (byte) 31 /*0x1F*/,
      (byte) 94,
      (byte) 149,
      (byte) 206,
      (byte) 225,
      (byte) 105,
      (byte) 2
    };
    byte[] numArray6 = new byte[54];
    numArray6[52] = (byte) 218;
    numArray6[44] = (byte) 9;
    numArray6[2] = (byte) 191;
    numArray6[5] = (byte) 23;
    numArray6[23] = (byte) 231;
    numArray6[51] = (byte) 139;
    numArray6[13] = (byte) 132;
    numArray6[36] = (byte) 123;
    numArray6[8] = (byte) 240 /*0xF0*/;
    numArray6[31 /*0x1F*/] = (byte) 195;
    numArray6[40] = (byte) 159;
    numArray6[33] = (byte) 53;
    numArray6[12] = (byte) 32 /*0x20*/;
    numArray6[0] = (byte) 99;
    numArray6[14] = (byte) 170;
    numArray6[9] = (byte) 103;
    numArray6[19] = (byte) 178;
    numArray6[17] = (byte) 126;
    numArray6[18] = (byte) 70;
    numArray6[45] = (byte) 236;
    numArray6[20] = (byte) 111;
    numArray6[21] = (byte) 181;
    numArray6[22] = (byte) 18;
    numArray6[43] = (byte) 177;
    numArray6[35] = (byte) 101;
    numArray6[25] = (byte) 158;
    numArray6[26] = (byte) 219;
    numArray6[27] = (byte) 110;
    numArray6[15] = (byte) 135;
    numArray6[47] = (byte) 100;
    numArray6[29] = (byte) 71;
    numArray6[3] = (byte) 61;
    numArray6[1] = (byte) 81;
    numArray6[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    numArray6[34] = (byte) 44;
    numArray6[11] = (byte) 7;
    numArray6[32 /*0x20*/] = (byte) 68;
    numArray6[37] = (byte) 91;
    numArray6[38] = (byte) 45;
    numArray6[7] = (byte) 194;
    numArray6[28] = (byte) 126;
    numArray6[41] = (byte) 151;
    numArray6[42] = (byte) 89;
    numArray6[10] = (byte) 251;
    numArray6[39] = (byte) 2;
    numArray6[50] = (byte) 231;
    numArray6[46] = (byte) 121;
    numArray6[30] = (byte) 39;
    numArray6[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray6[49] = (byte) 244;
    numArray6[24] = (byte) 125;
    numArray6[6] = (byte) 46;
    numArray6[53] = (byte) 73;
    numArray6[4] = (byte) 133;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 54);
    for (int index = 0; index < 54; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
