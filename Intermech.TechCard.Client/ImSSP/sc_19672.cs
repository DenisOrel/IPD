// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19672
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19672
{
  private static byte[] sspq = new byte[14]
  {
    (byte) 175,
    (byte) 231,
    (byte) 210,
    (byte) 40,
    (byte) 154,
    (byte) 48 /*0x30*/,
    (byte) 220,
    (byte) 55,
    (byte) 251,
    (byte) 228,
    (byte) 27,
    (byte) 183,
    (byte) 20,
    (byte) 161
  };
  private static byte[] sspr = new byte[14]
  {
    (byte) 168,
    (byte) 12,
    (byte) 102,
    (byte) 61,
    (byte) 107,
    (byte) 163,
    (byte) 95,
    (byte) 201,
    (byte) 56,
    (byte) 16 /*0x10*/,
    (byte) 1,
    (byte) 39,
    (byte) 163,
    (byte) 57
  };

  internal static string ssp_techcard_19673()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 118,
        (byte) 130,
        (byte) 224 /*0xE0*/,
        (byte) 179,
        (byte) 120,
        (byte) 75,
        (byte) 91,
        (byte) 208 /*0xD0*/,
        (byte) 188,
        (byte) 230,
        (byte) 253,
        (byte) 230,
        (byte) 163,
        (byte) 35,
        (byte) 30,
        (byte) 210,
        (byte) 175,
        (byte) 26,
        (byte) 23
      };
      byte[] numArray3 = new byte[19];
      numArray3[10] = (byte) 108;
      numArray3[11] = (byte) 211;
      numArray3[2] = (byte) 132;
      numArray3[7] = (byte) 127 /*0x7F*/;
      numArray3[1] = (byte) 98;
      numArray3[5] = (byte) 179;
      numArray3[3] = (byte) 113;
      numArray3[14] = (byte) 55;
      numArray3[8] = (byte) 231;
      numArray3[9] = (byte) 242;
      numArray3[12] = (byte) 99;
      numArray3[6] = (byte) 11;
      numArray3[16 /*0x10*/] = (byte) 30;
      numArray3[13] = (byte) 99;
      numArray3[0] = (byte) 90;
      numArray3[15] = (byte) 170;
      numArray3[4] = (byte) 106;
      numArray3[17] = (byte) 213;
      numArray3[18] = (byte) 188;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 69,
      (byte) 213,
      (byte) 215,
      (byte) 155,
      (byte) 185,
      (byte) 57,
      (byte) 179,
      (byte) 250,
      (byte) 137,
      (byte) 57,
      (byte) 119,
      (byte) 121,
      (byte) 203,
      (byte) 243,
      (byte) 89,
      (byte) 39,
      (byte) 86,
      (byte) 213,
      (byte) 71
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 201,
      byte.MaxValue,
      (byte) 247,
      (byte) 172,
      (byte) 120,
      (byte) 0,
      (byte) 188,
      (byte) 0,
      (byte) 216,
      (byte) 213,
      (byte) 96 /*0x60*/,
      (byte) 56,
      (byte) 161,
      (byte) 58,
      (byte) 101,
      (byte) 9,
      (byte) 24,
      (byte) 67,
      (byte) 205
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19674()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 47,
        (byte) 151,
        (byte) 225,
        (byte) 142,
        (byte) 1,
        (byte) 219,
        (byte) 125,
        (byte) 224 /*0xE0*/,
        (byte) 205,
        (byte) 17,
        (byte) 13,
        (byte) 180,
        (byte) 72,
        (byte) 130,
        (byte) 238,
        (byte) 36,
        (byte) 248,
        (byte) 106,
        (byte) 247
      };
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 240 /*0xF0*/;
      numArray3[7] = (byte) 225;
      numArray3[11] = (byte) 206;
      numArray3[17] = (byte) 157;
      numArray3[4] = (byte) 198;
      numArray3[1] = (byte) 180;
      numArray3[16 /*0x10*/] = (byte) 238;
      numArray3[14] = (byte) 197;
      numArray3[0] = (byte) 160 /*0xA0*/;
      numArray3[18] = (byte) 19;
      numArray3[10] = (byte) 190;
      numArray3[3] = (byte) 207;
      numArray3[12] = (byte) 96 /*0x60*/;
      numArray3[13] = (byte) 144 /*0x90*/;
      numArray3[2] = (byte) 177;
      numArray3[15] = (byte) 188;
      numArray3[5] = (byte) 195;
      numArray3[9] = (byte) 117;
      numArray3[6] = (byte) 250;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 88,
      (byte) 170,
      (byte) 104,
      (byte) 189,
      (byte) 63 /*0x3F*/,
      (byte) 16 /*0x10*/,
      (byte) 165,
      (byte) 103,
      (byte) 177,
      (byte) 78,
      (byte) 195,
      (byte) 113,
      (byte) 104,
      (byte) 24,
      (byte) 240 /*0xF0*/,
      (byte) 171,
      (byte) 168,
      (byte) 109,
      (byte) 56
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 178,
      (byte) 212,
      (byte) 43,
      (byte) 76,
      (byte) 251,
      (byte) 87,
      (byte) 104,
      (byte) 46,
      (byte) 120,
      (byte) 174,
      (byte) 247,
      (byte) 9,
      (byte) 140,
      (byte) 98,
      (byte) 119,
      (byte) 113,
      (byte) 159,
      (byte) 15,
      (byte) 250
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19675()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 56,
        (byte) 198,
        (byte) 69,
        (byte) 65,
        (byte) 218,
        (byte) 234,
        (byte) 77,
        (byte) 77,
        (byte) 183,
        (byte) 215,
        (byte) 99,
        (byte) 93,
        (byte) 148,
        (byte) 132,
        (byte) 123,
        (byte) 226,
        (byte) 183,
        (byte) 118,
        (byte) 151
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 224 /*0xE0*/,
        (byte) 106,
        (byte) 195,
        (byte) 112 /*0x70*/,
        (byte) 57,
        (byte) 36,
        (byte) 109,
        (byte) 83,
        (byte) 187,
        (byte) 141,
        (byte) 78,
        (byte) 27,
        (byte) 148,
        (byte) 234,
        (byte) 74,
        (byte) 48 /*0x30*/,
        (byte) 21,
        (byte) 195,
        (byte) 177
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
      (byte) 213,
      (byte) 6,
      (byte) 238,
      (byte) 183,
      (byte) 88,
      (byte) 46,
      (byte) 60,
      (byte) 121,
      (byte) 95,
      (byte) 140,
      (byte) 112 /*0x70*/,
      (byte) 227,
      (byte) 48 /*0x30*/,
      (byte) 247,
      (byte) 249,
      (byte) 207,
      (byte) 202,
      (byte) 125,
      (byte) 99
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 210,
      (byte) 134,
      (byte) 105,
      (byte) 106,
      (byte) 81,
      (byte) 5,
      (byte) 148,
      (byte) 140,
      (byte) 32 /*0x20*/,
      (byte) 2,
      (byte) 117,
      (byte) 10,
      (byte) 152,
      (byte) 237,
      (byte) 101,
      (byte) 78,
      (byte) 76,
      (byte) 134,
      (byte) 159
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19676()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[4] = (byte) 139;
      numArray2[15] = (byte) 145;
      numArray2[2] = (byte) 38;
      numArray2[3] = (byte) 45;
      numArray2[13] = (byte) 82;
      numArray2[9] = (byte) 16 /*0x10*/;
      numArray2[5] = (byte) 129;
      numArray2[7] = (byte) 232;
      numArray2[8] = (byte) 159;
      numArray2[1] = (byte) 91;
      numArray2[16 /*0x10*/] = (byte) 30;
      numArray2[11] = (byte) 240 /*0xF0*/;
      numArray2[12] = (byte) 63 /*0x3F*/;
      numArray2[18] = (byte) 62;
      numArray2[14] = (byte) 35;
      numArray2[10] = (byte) 34;
      numArray2[0] = (byte) 187;
      numArray2[6] = (byte) 74;
      numArray2[17] = (byte) 17;
      byte[] numArray3 = new byte[19];
      numArray3[18] = (byte) 210;
      numArray3[17] = (byte) 68;
      numArray3[2] = (byte) 218;
      numArray3[15] = (byte) 112 /*0x70*/;
      numArray3[4] = (byte) 80 /*0x50*/;
      numArray3[5] = (byte) 222;
      numArray3[13] = (byte) 234;
      numArray3[7] = (byte) 172;
      numArray3[1] = (byte) 76;
      numArray3[14] = (byte) 75;
      numArray3[8] = (byte) 163;
      numArray3[11] = (byte) 127 /*0x7F*/;
      numArray3[12] = (byte) 165;
      numArray3[10] = (byte) 170;
      numArray3[0] = (byte) 31 /*0x1F*/;
      numArray3[9] = (byte) 97;
      numArray3[16 /*0x10*/] = (byte) 53;
      numArray3[3] = (byte) 143;
      numArray3[6] = (byte) 97;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[1] = (byte) 158;
    numArray5[12] = (byte) 96 /*0x60*/;
    numArray5[2] = (byte) 206;
    numArray5[0] = (byte) 153;
    numArray5[10] = (byte) 96 /*0x60*/;
    numArray5[5] = (byte) 24;
    numArray5[6] = (byte) 181;
    numArray5[16 /*0x10*/] = (byte) 26;
    numArray5[8] = (byte) 31 /*0x1F*/;
    numArray5[9] = (byte) 198;
    numArray5[7] = (byte) 61;
    numArray5[11] = (byte) 30;
    numArray5[14] = (byte) 100;
    numArray5[13] = (byte) 71;
    numArray5[4] = (byte) 90;
    numArray5[3] = (byte) 186;
    numArray5[15] = (byte) 51;
    numArray5[17] = (byte) 222;
    numArray5[18] = (byte) 70;
    byte[] numArray6 = new byte[19]
    {
      (byte) 65,
      (byte) 80 /*0x50*/,
      (byte) 215,
      (byte) 82,
      (byte) 87,
      (byte) 131,
      (byte) 252,
      (byte) 220,
      (byte) 93,
      (byte) 103,
      (byte) 156,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 135,
      (byte) 66,
      (byte) 248,
      (byte) 230,
      (byte) 212,
      (byte) 209
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19677()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 93,
        (byte) 224 /*0xE0*/,
        (byte) 139,
        (byte) 39,
        (byte) 143,
        (byte) 133,
        (byte) 188,
        (byte) 249,
        (byte) 180,
        (byte) 203,
        (byte) 140,
        (byte) 45,
        (byte) 115,
        (byte) 46,
        (byte) 153,
        (byte) 159,
        (byte) 27,
        (byte) 148,
        (byte) 217
      };
      byte[] numArray3 = new byte[19];
      numArray3[2] = (byte) 233;
      numArray3[0] = (byte) 37;
      numArray3[12] = (byte) 200;
      numArray3[1] = (byte) 97;
      numArray3[4] = (byte) 4;
      numArray3[5] = (byte) 234;
      numArray3[3] = (byte) 36;
      numArray3[11] = (byte) 177;
      numArray3[8] = (byte) 145;
      numArray3[13] = (byte) 108;
      numArray3[6] = (byte) 41;
      numArray3[15] = (byte) 135;
      numArray3[7] = (byte) 10;
      numArray3[10] = (byte) 225;
      numArray3[14] = (byte) 197;
      numArray3[9] = (byte) 121;
      numArray3[16 /*0x10*/] = (byte) 114;
      numArray3[17] = (byte) 75;
      numArray3[18] = (byte) 59;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_19672.sspq, 0, (Array) numArray4, 0, 14);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19672.sspr, 0, (Array) numArray4, 0, 14);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 204,
      (byte) 138,
      (byte) 95,
      (byte) 62,
      (byte) 156,
      (byte) 207,
      (byte) 156,
      (byte) 177,
      (byte) 101,
      (byte) 59,
      (byte) 2,
      (byte) 192 /*0xC0*/,
      (byte) 32 /*0x20*/,
      (byte) 117,
      (byte) 251,
      (byte) 164,
      (byte) 94,
      (byte) 32 /*0x20*/,
      (byte) 60
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 109,
      (byte) 215,
      (byte) 118,
      (byte) 193,
      (byte) 27,
      (byte) 19,
      (byte) 76,
      (byte) 216,
      (byte) 86,
      (byte) 87,
      (byte) 227,
      (byte) 249,
      (byte) 121,
      (byte) 247,
      (byte) 81,
      (byte) 74,
      (byte) 26,
      (byte) 144 /*0x90*/,
      (byte) 22
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
