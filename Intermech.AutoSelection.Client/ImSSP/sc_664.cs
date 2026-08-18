// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_664
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_664
{
  internal static string ssp_automatch_665()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[5] = (byte) 127 /*0x7F*/;
      numArray2[1] = (byte) 209;
      numArray2[22] = (byte) 215;
      numArray2[20] = (byte) 177;
      numArray2[4] = (byte) 46;
      numArray2[12] = (byte) 92;
      numArray2[14] = (byte) 50;
      numArray2[8] = (byte) 76;
      numArray2[6] = (byte) 180;
      numArray2[9] = (byte) 209;
      numArray2[10] = (byte) 115;
      numArray2[3] = (byte) 225;
      numArray2[0] = (byte) 18;
      numArray2[13] = (byte) 20;
      numArray2[19] = (byte) 13;
      numArray2[15] = (byte) 122;
      numArray2[16 /*0x10*/] = (byte) 231;
      numArray2[17] = (byte) 175;
      numArray2[18] = (byte) 50;
      numArray2[11] = (byte) 70;
      numArray2[2] = (byte) 47;
      numArray2[21] = (byte) 239;
      numArray2[7] = (byte) 15;
      byte[] numArray3 = new byte[23]
      {
        (byte) 12,
        (byte) 3,
        (byte) 51,
        (byte) 170,
        (byte) 26,
        (byte) 15,
        (byte) 215,
        (byte) 193,
        (byte) 207,
        (byte) 69,
        (byte) 174,
        (byte) 179,
        (byte) 52,
        (byte) 163,
        (byte) 117,
        (byte) 112 /*0x70*/,
        (byte) 72,
        (byte) 209,
        (byte) 99,
        (byte) 160 /*0xA0*/,
        (byte) 60,
        (byte) 99,
        (byte) 107
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[0] = (byte) 102;
    numArray5[1] = (byte) 139;
    numArray5[2] = (byte) 144 /*0x90*/;
    numArray5[12] = (byte) 182;
    numArray5[4] = (byte) 56;
    numArray5[20] = (byte) 241;
    numArray5[6] = (byte) 35;
    numArray5[16 /*0x10*/] = (byte) 163;
    numArray5[18] = (byte) 137;
    numArray5[22] = (byte) 61;
    numArray5[5] = (byte) 184;
    numArray5[11] = (byte) 162;
    numArray5[3] = (byte) 156;
    numArray5[13] = (byte) 127 /*0x7F*/;
    numArray5[10] = (byte) 114;
    numArray5[7] = (byte) 249;
    numArray5[15] = (byte) 118;
    numArray5[17] = (byte) 154;
    numArray5[21] = (byte) 103;
    numArray5[19] = (byte) 39;
    numArray5[9] = (byte) 131;
    numArray5[14] = (byte) 180;
    numArray5[8] = (byte) 85;
    byte[] numArray6 = new byte[23];
    numArray6[3] = (byte) 204;
    numArray6[11] = (byte) 2;
    numArray6[9] = (byte) 120;
    numArray6[12] = (byte) 133;
    numArray6[18] = (byte) 24;
    numArray6[5] = (byte) 186;
    numArray6[6] = (byte) 12;
    numArray6[7] = (byte) 218;
    numArray6[10] = (byte) 112 /*0x70*/;
    numArray6[15] = (byte) 164;
    numArray6[20] = (byte) 2;
    numArray6[1] = (byte) 132;
    numArray6[14] = (byte) 63 /*0x3F*/;
    numArray6[13] = (byte) 206;
    numArray6[19] = (byte) 76;
    numArray6[4] = (byte) 85;
    numArray6[16 /*0x10*/] = (byte) 236;
    numArray6[17] = (byte) 241;
    numArray6[2] = (byte) 110;
    numArray6[21] = (byte) 148;
    numArray6[0] = (byte) 240 /*0xF0*/;
    numArray6[8] = (byte) 101;
    numArray6[22] = (byte) 42;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_666()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24]
      {
        (byte) 111,
        (byte) 92,
        (byte) 169,
        (byte) 171,
        (byte) 22,
        (byte) 210,
        (byte) 188,
        (byte) 189,
        (byte) 189,
        (byte) 57,
        (byte) 180,
        (byte) 198,
        (byte) 241,
        (byte) 23,
        (byte) 117,
        (byte) 105,
        (byte) 41,
        (byte) 190,
        (byte) 65,
        (byte) 61,
        (byte) 199,
        (byte) 217,
        (byte) 225,
        (byte) 216
      };
      byte[] numArray3 = new byte[24]
      {
        (byte) 237,
        (byte) 236,
        (byte) 26,
        (byte) 68,
        (byte) 120,
        (byte) 226,
        (byte) 208 /*0xD0*/,
        (byte) 212,
        (byte) 121,
        (byte) 205,
        (byte) 145,
        (byte) 93,
        (byte) 107,
        (byte) 129,
        (byte) 82,
        (byte) 22,
        (byte) 45,
        (byte) 186,
        (byte) 66,
        (byte) 151,
        (byte) 64 /*0x40*/,
        (byte) 27,
        (byte) 45,
        (byte) 57
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 93,
      (byte) 208 /*0xD0*/,
      (byte) 99,
      (byte) 88,
      (byte) 41,
      (byte) 199,
      (byte) 67,
      (byte) 158,
      (byte) 67,
      (byte) 167,
      (byte) 83,
      (byte) 54,
      (byte) 182,
      (byte) 56,
      (byte) 239,
      (byte) 130,
      (byte) 175,
      (byte) 186,
      (byte) 144 /*0x90*/,
      (byte) 55,
      (byte) 53,
      (byte) 23,
      (byte) 146,
      (byte) 44
    };
    byte[] numArray6 = new byte[24];
    numArray6[10] = (byte) 253;
    numArray6[1] = (byte) 144 /*0x90*/;
    numArray6[2] = (byte) 147;
    numArray6[6] = (byte) 66;
    numArray6[0] = (byte) 142;
    numArray6[5] = (byte) 198;
    numArray6[22] = (byte) 12;
    numArray6[15] = (byte) 179;
    numArray6[21] = (byte) 91;
    numArray6[11] = (byte) 176 /*0xB0*/;
    numArray6[8] = (byte) 76;
    numArray6[14] = (byte) 179;
    numArray6[12] = (byte) 135;
    numArray6[13] = (byte) 220;
    numArray6[9] = (byte) 96 /*0x60*/;
    numArray6[7] = (byte) 223;
    numArray6[3] = (byte) 165;
    numArray6[4] = (byte) 46;
    numArray6[18] = (byte) 86;
    numArray6[19] = (byte) 63 /*0x3F*/;
    numArray6[20] = (byte) 21;
    numArray6[16 /*0x10*/] = (byte) 102;
    numArray6[17] = (byte) 70;
    numArray6[23] = (byte) 61;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
