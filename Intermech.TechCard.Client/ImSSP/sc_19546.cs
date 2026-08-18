// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19546
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19546
{
  internal static string ssp_techcard_19547()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[6] = (byte) 95;
      numArray2[1] = (byte) 180;
      numArray2[2] = (byte) 61;
      numArray2[3] = (byte) 209;
      numArray2[4] = (byte) 250;
      numArray2[5] = (byte) 130;
      numArray2[14] = (byte) 148;
      numArray2[7] = (byte) 125;
      numArray2[8] = (byte) 121;
      numArray2[11] = (byte) 29;
      numArray2[12] = (byte) 111;
      numArray2[10] = (byte) 179;
      numArray2[13] = (byte) 139;
      numArray2[9] = (byte) 149;
      numArray2[0] = (byte) 229;
      numArray2[15] = (byte) 2;
      numArray2[17] = (byte) 140;
      numArray2[16 /*0x10*/] = (byte) 219;
      numArray2[18] = (byte) 17;
      byte[] numArray3 = new byte[19]
      {
        (byte) 50,
        (byte) 48 /*0x30*/,
        (byte) 183,
        (byte) 22,
        (byte) 154,
        (byte) 163,
        (byte) 66,
        (byte) 54,
        (byte) 199,
        (byte) 154,
        (byte) 174,
        (byte) 15,
        (byte) 128 /*0x80*/,
        (byte) 37,
        (byte) 18,
        (byte) 8,
        (byte) 77,
        (byte) 11,
        (byte) 150
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[7] = (byte) 229;
    numArray5[1] = (byte) 163;
    numArray5[11] = (byte) 11;
    numArray5[10] = (byte) 186;
    numArray5[4] = (byte) 244;
    numArray5[8] = (byte) 164;
    numArray5[6] = (byte) 148;
    numArray5[3] = (byte) 131;
    numArray5[0] = (byte) 129;
    numArray5[9] = (byte) 235;
    numArray5[15] = (byte) 209;
    numArray5[16 /*0x10*/] = (byte) 115;
    numArray5[12] = (byte) 158;
    numArray5[13] = (byte) 248;
    numArray5[14] = (byte) 141;
    numArray5[2] = (byte) 126;
    numArray5[5] = (byte) 111;
    numArray5[17] = (byte) 150;
    numArray5[18] = (byte) 208 /*0xD0*/;
    byte[] numArray6 = new byte[19]
    {
      (byte) 85,
      (byte) 43,
      (byte) 231,
      (byte) 228,
      (byte) 129,
      (byte) 41,
      (byte) 237,
      (byte) 171,
      (byte) 134,
      (byte) 180,
      (byte) 95,
      (byte) 21,
      (byte) 183,
      (byte) 30,
      (byte) 244,
      (byte) 171,
      (byte) 118,
      (byte) 243,
      (byte) 94
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19548()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 28,
        (byte) 246,
        (byte) 157,
        (byte) 27,
        (byte) 178,
        (byte) 23,
        (byte) 112 /*0x70*/,
        (byte) 187,
        (byte) 154,
        (byte) 190,
        (byte) 102,
        (byte) 71,
        (byte) 91,
        (byte) 197,
        (byte) 10,
        (byte) 64 /*0x40*/,
        (byte) 246,
        (byte) 39,
        (byte) 15
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 193,
        (byte) 218,
        (byte) 242,
        (byte) 180,
        (byte) 142,
        (byte) 15,
        (byte) 101,
        (byte) 245,
        (byte) 57,
        (byte) 122,
        (byte) 207,
        (byte) 110,
        (byte) 12,
        (byte) 75,
        (byte) 62,
        (byte) 33,
        (byte) 41,
        (byte) 94,
        (byte) 26
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 101,
      (byte) 100,
      (byte) 99,
      (byte) 216,
      (byte) 72,
      (byte) 217,
      (byte) 108,
      (byte) 44,
      (byte) 80 /*0x50*/,
      (byte) 21,
      (byte) 85,
      (byte) 96 /*0x60*/,
      (byte) 159,
      (byte) 161,
      (byte) 159,
      (byte) 113,
      (byte) 27,
      (byte) 72,
      (byte) 29
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 36,
      (byte) 38,
      (byte) 138,
      (byte) 145,
      (byte) 16 /*0x10*/,
      (byte) 108,
      (byte) 33,
      (byte) 51,
      (byte) 131,
      (byte) 138,
      (byte) 127 /*0x7F*/,
      (byte) 216,
      (byte) 133,
      (byte) 42,
      (byte) 203,
      (byte) 169,
      (byte) 120,
      (byte) 205,
      (byte) 122
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19549()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[7] = (byte) 170;
      numArray2[3] = (byte) 170;
      numArray2[9] = (byte) 47;
      numArray2[6] = (byte) 31 /*0x1F*/;
      numArray2[4] = (byte) 35;
      numArray2[5] = (byte) 79;
      numArray2[12] = (byte) 57;
      numArray2[18] = (byte) 109;
      numArray2[8] = (byte) 196;
      numArray2[1] = (byte) 106;
      numArray2[10] = (byte) 109;
      numArray2[11] = (byte) 145;
      numArray2[16 /*0x10*/] = (byte) 150;
      numArray2[13] = (byte) 184;
      numArray2[2] = (byte) 234;
      numArray2[15] = (byte) 207;
      numArray2[0] = (byte) 96 /*0x60*/;
      numArray2[17] = (byte) 78;
      numArray2[14] = (byte) 217;
      byte[] numArray3 = new byte[19]
      {
        (byte) 24,
        (byte) 116,
        (byte) 138,
        (byte) 93,
        (byte) 246,
        (byte) 88,
        (byte) 21,
        (byte) 73,
        (byte) 14,
        (byte) 215,
        (byte) 106,
        (byte) 47,
        (byte) 162,
        (byte) 204,
        (byte) 76,
        (byte) 134,
        (byte) 182,
        (byte) 35,
        (byte) 139
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[6] = (byte) 184;
    numArray5[5] = (byte) 102;
    numArray5[13] = (byte) 158;
    numArray5[3] = (byte) 49;
    numArray5[4] = (byte) 16 /*0x10*/;
    numArray5[0] = (byte) 106;
    numArray5[1] = (byte) 144 /*0x90*/;
    numArray5[7] = (byte) 66;
    numArray5[8] = (byte) 121;
    numArray5[9] = (byte) 134;
    numArray5[12] = (byte) 88;
    numArray5[10] = (byte) 254;
    numArray5[15] = (byte) 111;
    numArray5[11] = (byte) 238;
    numArray5[17] = (byte) 64 /*0x40*/;
    numArray5[14] = (byte) 109;
    numArray5[16 /*0x10*/] = (byte) 196;
    numArray5[18] = (byte) 130;
    numArray5[2] = (byte) 48 /*0x30*/;
    byte[] numArray6 = new byte[19]
    {
      (byte) 253,
      (byte) 118,
      (byte) 168,
      (byte) 197,
      (byte) 14,
      (byte) 236,
      (byte) 90,
      (byte) 43,
      (byte) 242,
      (byte) 122,
      (byte) 181,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 156,
      (byte) 3,
      (byte) 158,
      (byte) 43,
      (byte) 138,
      (byte) 155
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
