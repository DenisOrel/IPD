// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12581
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12581
{
  private static byte[] sspq = new byte[13]
  {
    (byte) 236,
    (byte) 93,
    (byte) 216,
    (byte) 37,
    (byte) 205,
    (byte) 11,
    (byte) 63 /*0x3F*/,
    (byte) 84,
    (byte) 138,
    (byte) 69,
    (byte) 5,
    (byte) 148,
    (byte) 19
  };
  private static byte[] sspr = new byte[13]
  {
    (byte) 192 /*0xC0*/,
    (byte) 252,
    (byte) 81,
    (byte) 46,
    (byte) 234,
    (byte) 199,
    (byte) 147,
    (byte) 12,
    (byte) 243,
    (byte) 99,
    (byte) 236,
    (byte) 47,
    (byte) 10
  };

  internal static string ssp_appserver_12582()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[52];
      byte[] numArray2 = new byte[52]
      {
        (byte) 105,
        (byte) 121,
        byte.MaxValue,
        (byte) 86,
        (byte) 165,
        (byte) 99,
        (byte) 113,
        (byte) 32 /*0x20*/,
        (byte) 179,
        (byte) 123,
        (byte) 73,
        (byte) 161,
        (byte) 141,
        (byte) 188,
        (byte) 8,
        (byte) 44,
        (byte) 171,
        (byte) 253,
        (byte) 146,
        (byte) 197,
        (byte) 89,
        (byte) 193,
        (byte) 225,
        (byte) 119,
        (byte) 48 /*0x30*/,
        (byte) 173,
        (byte) 111,
        (byte) 173,
        (byte) 38,
        (byte) 118,
        (byte) 170,
        (byte) 230,
        (byte) 242,
        (byte) 148,
        (byte) 244,
        (byte) 237,
        (byte) 251,
        (byte) 201,
        (byte) 249,
        (byte) 131,
        (byte) 96 /*0x60*/,
        (byte) 64 /*0x40*/,
        (byte) 164,
        (byte) 181,
        (byte) 50,
        (byte) 81,
        (byte) 222,
        (byte) 2,
        (byte) 19,
        (byte) 151,
        (byte) 221,
        (byte) 9
      };
      byte[] numArray3 = new byte[52]
      {
        (byte) 22,
        (byte) 250,
        (byte) 174,
        (byte) 71,
        (byte) 12,
        (byte) 72,
        (byte) 189,
        (byte) 217,
        (byte) 176 /*0xB0*/,
        (byte) 128 /*0x80*/,
        (byte) 148,
        (byte) 12,
        (byte) 204,
        (byte) 207,
        (byte) 235,
        (byte) 107,
        (byte) 226,
        (byte) 117,
        (byte) 19,
        (byte) 105,
        (byte) 254,
        (byte) 205,
        (byte) 230,
        (byte) 45,
        (byte) 81,
        (byte) 39,
        (byte) 243,
        (byte) 128 /*0x80*/,
        (byte) 106,
        (byte) 12,
        (byte) 244,
        (byte) 128 /*0x80*/,
        (byte) 16 /*0x10*/,
        (byte) 183,
        (byte) 82,
        (byte) 91,
        (byte) 201,
        (byte) 42,
        (byte) 113,
        (byte) 163,
        (byte) 63 /*0x3F*/,
        (byte) 95,
        (byte) 105,
        (byte) 228,
        (byte) 165,
        (byte) 8,
        (byte) 90,
        (byte) 58,
        (byte) 235,
        (byte) 228,
        (byte) 60,
        (byte) 45
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[52];
    byte[] numArray5 = new byte[52];
    numArray5[5] = (byte) 244;
    numArray5[20] = (byte) 24;
    numArray5[2] = (byte) 135;
    numArray5[1] = (byte) 151;
    numArray5[31 /*0x1F*/] = (byte) 12;
    numArray5[3] = (byte) 37;
    numArray5[6] = (byte) 196;
    numArray5[7] = (byte) 250;
    numArray5[8] = (byte) 60;
    numArray5[9] = (byte) 208 /*0xD0*/;
    numArray5[30] = (byte) 161;
    numArray5[26] = (byte) 109;
    numArray5[42] = (byte) 104;
    numArray5[51] = (byte) 5;
    numArray5[50] = (byte) 251;
    numArray5[0] = (byte) 168;
    numArray5[16 /*0x10*/] = (byte) 225;
    numArray5[17] = (byte) 3;
    numArray5[18] = (byte) 15;
    numArray5[37] = (byte) 163;
    numArray5[19] = (byte) 101;
    numArray5[41] = (byte) 69;
    numArray5[22] = (byte) 43;
    numArray5[32 /*0x20*/] = (byte) 216;
    numArray5[24] = (byte) 223;
    numArray5[4] = (byte) 248;
    numArray5[34] = (byte) 76;
    numArray5[23] = (byte) 69;
    numArray5[28] = (byte) 142;
    numArray5[29] = (byte) 153;
    numArray5[12] = (byte) 249;
    numArray5[11] = (byte) 174;
    numArray5[14] = (byte) 26;
    numArray5[33] = (byte) 75;
    numArray5[21] = (byte) 28;
    numArray5[35] = (byte) 86;
    numArray5[13] = (byte) 199;
    numArray5[38] = (byte) 1;
    numArray5[39] = (byte) 142;
    numArray5[15] = (byte) 48 /*0x30*/;
    numArray5[27] = (byte) 153;
    numArray5[25] = (byte) 206;
    numArray5[44] = (byte) 207;
    numArray5[43] = (byte) 229;
    numArray5[36] = (byte) 116;
    numArray5[45] = (byte) 163;
    numArray5[46] = (byte) 228;
    numArray5[47] = (byte) 58;
    numArray5[48 /*0x30*/] = (byte) 249;
    numArray5[49] = (byte) 103;
    numArray5[40] = (byte) 156;
    numArray5[10] = (byte) 98;
    byte[] numArray6 = new byte[52]
    {
      (byte) 70,
      (byte) 253,
      (byte) 218,
      (byte) 69,
      (byte) 95,
      (byte) 4,
      (byte) 21,
      (byte) 75,
      (byte) 115,
      (byte) 7,
      (byte) 215,
      (byte) 106,
      (byte) 202,
      (byte) 13,
      (byte) 231,
      (byte) 145,
      (byte) 210,
      (byte) 13,
      (byte) 170,
      (byte) 176 /*0xB0*/,
      (byte) 161,
      (byte) 201,
      (byte) 141,
      (byte) 28,
      (byte) 169,
      (byte) 65,
      (byte) 55,
      (byte) 200,
      (byte) 158,
      (byte) 173,
      (byte) 244,
      (byte) 56,
      (byte) 46,
      (byte) 163,
      (byte) 150,
      (byte) 65,
      (byte) 185,
      (byte) 106,
      (byte) 58,
      (byte) 83,
      (byte) 82,
      (byte) 117,
      (byte) 6,
      (byte) 4,
      (byte) 221,
      (byte) 143,
      (byte) 177,
      (byte) 3,
      (byte) 115,
      (byte) 62,
      (byte) 96 /*0x60*/,
      (byte) 180
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 52);
    for (int index = 0; index < 52; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[13];
    byte[] response = new byte[13];
    Array.Copy((Array) sc_12581.sspq, 0, (Array) numArray7, 0, 13);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12581.sspr, 0, (Array) numArray7, 0, 13);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }
}
