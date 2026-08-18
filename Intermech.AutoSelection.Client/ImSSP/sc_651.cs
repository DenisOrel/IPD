// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_651
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_651
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 74,
    (byte) 129,
    (byte) 27,
    (byte) 160 /*0xA0*/,
    (byte) 197,
    (byte) 122,
    (byte) 24,
    (byte) 197,
    (byte) 188,
    (byte) 2,
    (byte) 58,
    (byte) 133,
    (byte) 67,
    (byte) 59,
    (byte) 30,
    (byte) 138,
    (byte) 24,
    (byte) 21,
    (byte) 155,
    (byte) 85,
    (byte) 144 /*0x90*/,
    (byte) 168,
    (byte) 43,
    (byte) 251,
    (byte) 101,
    (byte) 0,
    (byte) 110,
    (byte) 64 /*0x40*/,
    (byte) 210,
    (byte) 78,
    (byte) 3,
    (byte) 170,
    (byte) 85,
    (byte) 28,
    (byte) 236,
    (byte) 214,
    (byte) 168,
    (byte) 101
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 190,
    (byte) 145,
    (byte) 209,
    (byte) 81,
    (byte) 162,
    (byte) 112 /*0x70*/,
    (byte) 64 /*0x40*/,
    (byte) 198,
    (byte) 145,
    (byte) 87,
    (byte) 129,
    (byte) 141,
    (byte) 108,
    (byte) 187,
    (byte) 113,
    (byte) 103,
    (byte) 101,
    (byte) 42,
    (byte) 62,
    (byte) 43,
    (byte) 153,
    (byte) 131,
    (byte) 42,
    (byte) 6,
    (byte) 18,
    (byte) 215,
    (byte) 104,
    (byte) 246,
    (byte) 35,
    (byte) 42,
    (byte) 93,
    (byte) 139,
    (byte) 208 /*0xD0*/,
    (byte) 141,
    (byte) 176 /*0xB0*/,
    (byte) 99,
    (byte) 185,
    (byte) 50
  };

  internal static string ssp_automatch_652()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 243,
        (byte) 133,
        (byte) 42,
        (byte) 91,
        (byte) 203,
        (byte) 105,
        (byte) 126,
        (byte) 34,
        (byte) 30,
        (byte) 15,
        (byte) 116,
        (byte) 76,
        (byte) 232,
        (byte) 246,
        (byte) 213,
        (byte) 232,
        (byte) 13,
        (byte) 6,
        (byte) 122,
        (byte) 219,
        (byte) 210,
        (byte) 11,
        (byte) 182
      };
      byte[] numArray3 = new byte[23];
      numArray3[12] = (byte) 214;
      numArray3[15] = (byte) 46;
      numArray3[2] = (byte) 188;
      numArray3[5] = (byte) 250;
      numArray3[4] = (byte) 50;
      numArray3[22] = (byte) 0;
      numArray3[3] = (byte) 151;
      numArray3[7] = (byte) 126;
      numArray3[9] = (byte) 64 /*0x40*/;
      numArray3[10] = (byte) 129;
      numArray3[0] = (byte) 210;
      numArray3[11] = (byte) 88;
      numArray3[19] = (byte) 119;
      numArray3[13] = (byte) 93;
      numArray3[14] = (byte) 6;
      numArray3[17] = (byte) 132;
      numArray3[16 /*0x10*/] = (byte) 72;
      numArray3[18] = (byte) 233;
      numArray3[1] = (byte) 221;
      numArray3[8] = (byte) 207;
      numArray3[20] = (byte) 91;
      numArray3[21] = (byte) 42;
      numArray3[6] = (byte) 166;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[2] = (byte) 224 /*0xE0*/;
    numArray5[12] = (byte) 44;
    numArray5[9] = (byte) 130;
    numArray5[0] = (byte) 183;
    numArray5[8] = (byte) 125;
    numArray5[5] = (byte) 75;
    numArray5[13] = (byte) 158;
    numArray5[14] = (byte) 238;
    numArray5[16 /*0x10*/] = (byte) 73;
    numArray5[6] = (byte) 232;
    numArray5[17] = (byte) 174;
    numArray5[11] = (byte) 118;
    numArray5[10] = (byte) 94;
    numArray5[3] = (byte) 100;
    numArray5[1] = (byte) 12;
    numArray5[15] = (byte) 164;
    numArray5[4] = (byte) 152;
    numArray5[18] = (byte) 41;
    numArray5[22] = (byte) 22;
    numArray5[19] = (byte) 205;
    numArray5[20] = (byte) 223;
    numArray5[21] = (byte) 35;
    numArray5[7] = (byte) 206;
    byte[] numArray6 = new byte[23];
    numArray6[18] = (byte) 218;
    numArray6[21] = (byte) 44;
    numArray6[1] = (byte) 245;
    numArray6[3] = (byte) 53;
    numArray6[4] = (byte) 7;
    numArray6[19] = (byte) 91;
    numArray6[6] = (byte) 243;
    numArray6[17] = (byte) 94;
    numArray6[16 /*0x10*/] = (byte) 150;
    numArray6[11] = (byte) 122;
    numArray6[10] = (byte) 99;
    numArray6[8] = (byte) 144 /*0x90*/;
    numArray6[22] = (byte) 37;
    numArray6[2] = (byte) 77;
    numArray6[14] = (byte) 127 /*0x7F*/;
    numArray6[15] = (byte) 181;
    numArray6[12] = (byte) 148;
    numArray6[20] = (byte) 133;
    numArray6[7] = (byte) 88;
    numArray6[0] = (byte) 78;
    numArray6[13] = (byte) 131;
    numArray6[9] = (byte) 224 /*0xE0*/;
    numArray6[5] = (byte) 62;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_653()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 182,
        (byte) 141,
        (byte) 185,
        (byte) 220,
        (byte) 161,
        (byte) 93,
        (byte) 112 /*0x70*/,
        (byte) 117,
        (byte) 38,
        (byte) 188,
        (byte) 206,
        (byte) 211,
        (byte) 148,
        (byte) 173,
        (byte) 97,
        (byte) 253,
        (byte) 56,
        (byte) 45,
        (byte) 45,
        (byte) 156,
        byte.MaxValue,
        (byte) 212,
        (byte) 68
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 78,
        (byte) 129,
        (byte) 146,
        (byte) 237,
        (byte) 67,
        (byte) 101,
        (byte) 227,
        (byte) 82,
        (byte) 236,
        (byte) 150,
        (byte) 223,
        (byte) 28,
        (byte) 182,
        (byte) 77,
        (byte) 87,
        (byte) 58,
        (byte) 223,
        (byte) 116,
        byte.MaxValue,
        (byte) 174,
        (byte) 223,
        (byte) 61,
        (byte) 115
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[10] = (byte) 0;
    numArray5[14] = (byte) 185;
    numArray5[2] = (byte) 168;
    numArray5[17] = (byte) 63 /*0x3F*/;
    numArray5[0] = (byte) 62;
    numArray5[5] = (byte) 8;
    numArray5[6] = (byte) 70;
    numArray5[16 /*0x10*/] = (byte) 120;
    numArray5[8] = (byte) 20;
    numArray5[3] = (byte) 180;
    numArray5[7] = (byte) 33;
    numArray5[11] = (byte) 183;
    numArray5[12] = (byte) 85;
    numArray5[22] = (byte) 210;
    numArray5[20] = (byte) 142;
    numArray5[13] = byte.MaxValue;
    numArray5[1] = (byte) 150;
    numArray5[4] = (byte) 100;
    numArray5[18] = (byte) 16 /*0x10*/;
    numArray5[19] = (byte) 14;
    numArray5[9] = (byte) 84;
    numArray5[21] = (byte) 56;
    numArray5[15] = (byte) 144 /*0x90*/;
    byte[] numArray6 = new byte[23];
    numArray6[19] = (byte) 98;
    numArray6[0] = (byte) 191;
    numArray6[10] = (byte) 101;
    numArray6[3] = (byte) 27;
    numArray6[4] = (byte) 222;
    numArray6[5] = (byte) 86;
    numArray6[6] = (byte) 21;
    numArray6[7] = (byte) 90;
    numArray6[18] = (byte) 27;
    numArray6[9] = (byte) 248;
    numArray6[2] = (byte) 114;
    numArray6[11] = (byte) 199;
    numArray6[17] = (byte) 109;
    numArray6[13] = (byte) 177;
    numArray6[14] = (byte) 58;
    numArray6[8] = (byte) 32 /*0x20*/;
    numArray6[16 /*0x10*/] = (byte) 76;
    numArray6[1] = (byte) 233;
    numArray6[15] = (byte) 238;
    numArray6[12] = (byte) 166;
    numArray6[20] = (byte) 39;
    numArray6[22] = (byte) 16 /*0x10*/;
    numArray6[21] = (byte) 250;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_651.sspq, 0, (Array) numArray7, 0, 38);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_651.sspr, 0, (Array) numArray7, 0, 38);
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
