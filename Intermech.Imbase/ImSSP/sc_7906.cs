// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7906
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7906
{
  private static byte[] sspq = new byte[12]
  {
    (byte) 82,
    (byte) 184,
    (byte) 93,
    (byte) 205,
    (byte) 187,
    (byte) 87,
    (byte) 184,
    (byte) 213,
    (byte) 1,
    (byte) 118,
    (byte) 69,
    (byte) 241
  };
  private static byte[] sspr = new byte[12]
  {
    (byte) 153,
    (byte) 99,
    (byte) 78,
    (byte) 229,
    (byte) 74,
    (byte) 129,
    (byte) 119,
    (byte) 153,
    (byte) 214,
    (byte) 72,
    (byte) 57,
    (byte) 11
  };

  internal static string ssp_expert_7907()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35]
      {
        (byte) 19,
        (byte) 28,
        (byte) 51,
        (byte) 17,
        (byte) 231,
        (byte) 5,
        (byte) 37,
        (byte) 194,
        (byte) 72,
        (byte) 51,
        (byte) 171,
        (byte) 117,
        (byte) 229,
        (byte) 227,
        (byte) 198,
        (byte) 169,
        (byte) 236,
        (byte) 50,
        (byte) 158,
        (byte) 124,
        (byte) 9,
        (byte) 150,
        (byte) 186,
        (byte) 8,
        (byte) 131,
        (byte) 222,
        (byte) 165,
        (byte) 214,
        (byte) 97,
        (byte) 253,
        (byte) 139,
        (byte) 85,
        (byte) 111,
        (byte) 28,
        (byte) 106
      };
      byte[] numArray3 = new byte[35]
      {
        (byte) 118,
        (byte) 149,
        (byte) 249,
        (byte) 151,
        (byte) 75,
        (byte) 155,
        (byte) 131,
        (byte) 119,
        (byte) 225,
        (byte) 187,
        (byte) 139,
        (byte) 156,
        (byte) 10,
        (byte) 136,
        (byte) 123,
        (byte) 181,
        (byte) 130,
        (byte) 171,
        (byte) 121,
        (byte) 160 /*0xA0*/,
        (byte) 189,
        (byte) 51,
        (byte) 170,
        (byte) 41,
        (byte) 153,
        (byte) 196,
        (byte) 61,
        (byte) 185,
        (byte) 248,
        (byte) 224 /*0xE0*/,
        (byte) 174,
        (byte) 11,
        (byte) 133,
        (byte) 171,
        (byte) 34
      };
      key.Query(true, 342, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35];
    numArray5[17] = (byte) 124;
    numArray5[12] = (byte) 192 /*0xC0*/;
    numArray5[2] = (byte) 144 /*0x90*/;
    numArray5[3] = (byte) 192 /*0xC0*/;
    numArray5[4] = (byte) 174;
    numArray5[1] = (byte) 189;
    numArray5[6] = (byte) 241;
    numArray5[23] = (byte) 78;
    numArray5[8] = (byte) 178;
    numArray5[9] = (byte) 71;
    numArray5[5] = (byte) 145;
    numArray5[29] = (byte) 201;
    numArray5[24] = (byte) 97;
    numArray5[13] = (byte) 22;
    numArray5[33] = (byte) 29;
    numArray5[14] = (byte) 199;
    numArray5[11] = (byte) 164;
    numArray5[16 /*0x10*/] = (byte) 198;
    numArray5[18] = (byte) 97;
    numArray5[19] = (byte) 43;
    numArray5[28] = (byte) 5;
    numArray5[21] = (byte) 9;
    numArray5[22] = (byte) 95;
    numArray5[0] = (byte) 159;
    numArray5[7] = (byte) 227;
    numArray5[27] = (byte) 85;
    numArray5[26] = (byte) 167;
    numArray5[15] = (byte) 70;
    numArray5[10] = (byte) 251;
    numArray5[32 /*0x20*/] = (byte) 218;
    numArray5[30] = (byte) 120;
    numArray5[31 /*0x1F*/] = (byte) 122;
    numArray5[20] = (byte) 141;
    numArray5[25] = (byte) 74;
    numArray5[34] = (byte) 146;
    byte[] numArray6 = new byte[35];
    numArray6[21] = (byte) 232;
    numArray6[17] = (byte) 236;
    numArray6[1] = (byte) 142;
    numArray6[25] = (byte) 7;
    numArray6[19] = (byte) 31 /*0x1F*/;
    numArray6[5] = (byte) 91;
    numArray6[3] = (byte) 192 /*0xC0*/;
    numArray6[7] = (byte) 93;
    numArray6[8] = (byte) 46;
    numArray6[9] = (byte) 124;
    numArray6[10] = (byte) 11;
    numArray6[11] = (byte) 48 /*0x30*/;
    numArray6[29] = (byte) 21;
    numArray6[13] = (byte) 117;
    numArray6[14] = (byte) 65;
    numArray6[32 /*0x20*/] = (byte) 242;
    numArray6[16 /*0x10*/] = (byte) 56;
    numArray6[4] = (byte) 59;
    numArray6[0] = (byte) 46;
    numArray6[27] = (byte) 146;
    numArray6[22] = (byte) 232;
    numArray6[20] = (byte) 29;
    numArray6[31 /*0x1F*/] = (byte) 28;
    numArray6[6] = (byte) 127 /*0x7F*/;
    numArray6[24] = (byte) 199;
    numArray6[23] = (byte) 246;
    numArray6[12] = (byte) 249;
    numArray6[34] = (byte) 186;
    numArray6[28] = (byte) 22;
    numArray6[15] = (byte) 114;
    numArray6[30] = (byte) 161;
    numArray6[26] = (byte) 155;
    numArray6[2] = (byte) 85;
    numArray6[18] = (byte) 130;
    numArray6[33] = (byte) 0;
    key.Query(true, 342, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[12];
    byte[] response = new byte[12];
    Array.Copy((Array) sc_7906.sspq, 0, (Array) numArray7, 0, 12);
    key.Query(true, 342, numArray7, response);
    Array.Copy((Array) sc_7906.sspr, 0, (Array) numArray7, 0, 12);
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

  internal static string ssp_expert_7908()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 196,
        (byte) 135,
        (byte) 97,
        (byte) 175,
        (byte) 148,
        (byte) 17,
        (byte) 250,
        (byte) 153,
        (byte) 43,
        (byte) 156,
        (byte) 113,
        (byte) 25,
        (byte) 178,
        (byte) 113,
        (byte) 64 /*0x40*/,
        (byte) 30,
        (byte) 13
      };
      byte[] numArray3 = new byte[17];
      numArray3[14] = (byte) 126;
      numArray3[1] = (byte) 186;
      numArray3[2] = (byte) 150;
      numArray3[5] = (byte) 150;
      numArray3[4] = (byte) 183;
      numArray3[16 /*0x10*/] = (byte) 140;
      numArray3[0] = (byte) 227;
      numArray3[7] = (byte) 163;
      numArray3[11] = (byte) 194;
      numArray3[12] = (byte) 230;
      numArray3[10] = (byte) 26;
      numArray3[8] = (byte) 134;
      numArray3[15] = (byte) 189;
      numArray3[13] = (byte) 200;
      numArray3[6] = byte.MaxValue;
      numArray3[9] = (byte) 143;
      numArray3[3] = (byte) 37;
      key.Query(true, 342, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[11] = (byte) 119;
    numArray5[15] = (byte) 21;
    numArray5[2] = (byte) 72;
    numArray5[3] = (byte) 183;
    numArray5[8] = (byte) 179;
    numArray5[4] = (byte) 246;
    numArray5[12] = (byte) 183;
    numArray5[7] = (byte) 235;
    numArray5[6] = (byte) 28;
    numArray5[5] = (byte) 38;
    numArray5[10] = (byte) 229;
    numArray5[9] = (byte) 8;
    numArray5[0] = (byte) 31 /*0x1F*/;
    numArray5[13] = (byte) 243;
    numArray5[1] = (byte) 67;
    numArray5[14] = (byte) 219;
    numArray5[16 /*0x10*/] = (byte) 19;
    byte[] numArray6 = new byte[17];
    numArray6[8] = (byte) 134;
    numArray6[5] = (byte) 246;
    numArray6[2] = (byte) 71;
    numArray6[7] = (byte) 48 /*0x30*/;
    numArray6[15] = (byte) 38;
    numArray6[3] = (byte) 250;
    numArray6[13] = (byte) 190;
    numArray6[16 /*0x10*/] = (byte) 107;
    numArray6[1] = (byte) 4;
    numArray6[9] = (byte) 57;
    numArray6[6] = (byte) 157;
    numArray6[11] = (byte) 171;
    numArray6[12] = (byte) 150;
    numArray6[10] = (byte) 199;
    numArray6[14] = (byte) 109;
    numArray6[4] = (byte) 140;
    numArray6[0] = (byte) 112 /*0x70*/;
    key.Query(true, 342, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
