// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19562
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19562
{
  private static byte[] sspq = new byte[50]
  {
    (byte) 91,
    (byte) 237,
    (byte) 77,
    (byte) 92,
    (byte) 136,
    (byte) 122,
    (byte) 72,
    (byte) 234,
    (byte) 56,
    (byte) 214,
    (byte) 178,
    (byte) 165,
    (byte) 121,
    (byte) 216,
    (byte) 186,
    (byte) 214,
    (byte) 132,
    (byte) 244,
    (byte) 155,
    (byte) 119,
    (byte) 237,
    (byte) 166,
    (byte) 211,
    (byte) 133,
    (byte) 17,
    (byte) 111,
    (byte) 178,
    (byte) 128 /*0x80*/,
    (byte) 198,
    (byte) 205,
    (byte) 157,
    (byte) 101,
    (byte) 131,
    (byte) 232,
    (byte) 112 /*0x70*/,
    (byte) 176 /*0xB0*/,
    (byte) 179,
    (byte) 227,
    (byte) 103,
    (byte) 100,
    (byte) 114,
    (byte) 84,
    (byte) 248,
    (byte) 92,
    (byte) 203,
    (byte) 221,
    (byte) 9,
    (byte) 15,
    (byte) 47,
    (byte) 135
  };
  private static byte[] sspr = new byte[50]
  {
    (byte) 175,
    (byte) 62,
    (byte) 109,
    (byte) 19,
    (byte) 135,
    (byte) 184,
    (byte) 5,
    (byte) 2,
    (byte) 29,
    (byte) 70,
    (byte) 142,
    (byte) 218,
    (byte) 208 /*0xD0*/,
    (byte) 79,
    (byte) 172,
    (byte) 41,
    byte.MaxValue,
    (byte) 34,
    (byte) 162,
    (byte) 179,
    (byte) 221,
    (byte) 40,
    (byte) 184,
    (byte) 181,
    (byte) 161,
    (byte) 215,
    (byte) 230,
    (byte) 33,
    (byte) 146,
    (byte) 108,
    (byte) 153,
    (byte) 185,
    (byte) 73,
    (byte) 119,
    (byte) 243,
    (byte) 77,
    (byte) 107,
    (byte) 124,
    (byte) 63 /*0x3F*/,
    (byte) 161,
    (byte) 43,
    (byte) 165,
    (byte) 108,
    (byte) 27,
    (byte) 136,
    (byte) 79,
    (byte) 136,
    (byte) 26,
    (byte) 130,
    (byte) 187
  };

  internal static string ssp_techcard_19563()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 47,
        (byte) 232,
        (byte) 231,
        (byte) 155,
        (byte) 242,
        (byte) 237,
        (byte) 37,
        (byte) 113,
        (byte) 223,
        (byte) 51,
        (byte) 142,
        (byte) 54,
        (byte) 29,
        (byte) 179,
        (byte) 232,
        (byte) 29,
        (byte) 82,
        (byte) 224 /*0xE0*/,
        (byte) 175
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 211,
        (byte) 87,
        (byte) 102,
        (byte) 214,
        (byte) 134,
        (byte) 182,
        (byte) 10,
        (byte) 182,
        (byte) 21,
        (byte) 191,
        (byte) 190,
        (byte) 169,
        (byte) 157,
        (byte) 30,
        (byte) 231,
        (byte) 162,
        (byte) 205,
        (byte) 107,
        (byte) 233
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
      (byte) 42,
      (byte) 154,
      (byte) 115,
      (byte) 46,
      (byte) 222,
      (byte) 251,
      (byte) 33,
      (byte) 94,
      (byte) 196,
      (byte) 84,
      (byte) 214,
      (byte) 182,
      (byte) 180,
      (byte) 84,
      (byte) 246,
      (byte) 128 /*0x80*/,
      (byte) 18,
      (byte) 12,
      (byte) 186
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 238,
      (byte) 30,
      (byte) 20,
      (byte) 250,
      (byte) 156,
      (byte) 227,
      (byte) 118,
      (byte) 140,
      (byte) 56,
      (byte) 217,
      (byte) 194,
      (byte) 111,
      (byte) 95,
      (byte) 23,
      (byte) 22,
      (byte) 90,
      (byte) 28,
      (byte) 161,
      (byte) 126
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19564()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 208 /*0xD0*/,
        (byte) 180,
        (byte) 3,
        (byte) 196,
        (byte) 34,
        (byte) 206,
        (byte) 208 /*0xD0*/,
        (byte) 46,
        (byte) 41,
        (byte) 93,
        (byte) 135,
        (byte) 190,
        (byte) 76,
        (byte) 39,
        (byte) 195,
        (byte) 14,
        (byte) 42,
        (byte) 80 /*0x50*/,
        (byte) 129
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 100,
        (byte) 180,
        (byte) 178,
        (byte) 125,
        (byte) 210,
        (byte) 210,
        (byte) 34,
        (byte) 254,
        (byte) 186,
        (byte) 13,
        (byte) 47,
        (byte) 30,
        (byte) 94,
        (byte) 125,
        (byte) 77,
        (byte) 61,
        (byte) 89,
        (byte) 135,
        (byte) 113
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[11] = (byte) 109;
    numArray5[13] = (byte) 122;
    numArray5[2] = (byte) 148;
    numArray5[3] = (byte) 39;
    numArray5[4] = (byte) 210;
    numArray5[9] = (byte) 245;
    numArray5[5] = (byte) 206;
    numArray5[7] = (byte) 231;
    numArray5[8] = (byte) 40;
    numArray5[15] = (byte) 54;
    numArray5[12] = (byte) 163;
    numArray5[17] = (byte) 144 /*0x90*/;
    numArray5[0] = (byte) 102;
    numArray5[14] = (byte) 219;
    numArray5[10] = (byte) 79;
    numArray5[6] = (byte) 162;
    numArray5[16 /*0x10*/] = (byte) 254;
    numArray5[1] = byte.MaxValue;
    numArray5[18] = (byte) 244;
    byte[] numArray6 = new byte[19];
    numArray6[5] = (byte) 92;
    numArray6[14] = (byte) 37;
    numArray6[2] = (byte) 238;
    numArray6[3] = (byte) 199;
    numArray6[4] = (byte) 122;
    numArray6[6] = (byte) 214;
    numArray6[16 /*0x10*/] = (byte) 119;
    numArray6[0] = (byte) 46;
    numArray6[8] = (byte) 172;
    numArray6[9] = (byte) 206;
    numArray6[17] = (byte) 24;
    numArray6[11] = (byte) 6;
    numArray6[13] = (byte) 31 /*0x1F*/;
    numArray6[10] = (byte) 233;
    numArray6[12] = (byte) 41;
    numArray6[15] = (byte) 237;
    numArray6[1] = (byte) 112 /*0x70*/;
    numArray6[7] = (byte) 101;
    numArray6[18] = (byte) 57;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19565()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 164,
        (byte) 87,
        (byte) 214,
        (byte) 196,
        (byte) 228,
        (byte) 121,
        (byte) 51,
        (byte) 44,
        (byte) 215,
        (byte) 6,
        (byte) 3,
        (byte) 148,
        (byte) 242,
        (byte) 154,
        (byte) 106,
        (byte) 35,
        (byte) 154,
        (byte) 39,
        (byte) 202
      };
      byte[] numArray3 = new byte[19];
      numArray3[14] = (byte) 171;
      numArray3[10] = (byte) 60;
      numArray3[2] = (byte) 20;
      numArray3[9] = (byte) 212;
      numArray3[4] = (byte) 179;
      numArray3[17] = (byte) 145;
      numArray3[13] = (byte) 138;
      numArray3[7] = (byte) 34;
      numArray3[8] = (byte) 167;
      numArray3[16 /*0x10*/] = (byte) 162;
      numArray3[18] = (byte) 158;
      numArray3[6] = (byte) 180;
      numArray3[12] = (byte) 126;
      numArray3[1] = (byte) 61;
      numArray3[5] = (byte) 140;
      numArray3[15] = (byte) 60;
      numArray3[3] = (byte) 2;
      numArray3[11] = (byte) 196;
      numArray3[0] = (byte) 143;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50];
      byte[] response = new byte[50];
      Array.Copy((Array) sc_19562.sspq, 0, (Array) numArray4, 0, 50);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19562.sspr, 0, (Array) numArray4, 0, 50);
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
      (byte) 73,
      (byte) 150,
      (byte) 202,
      (byte) 217,
      (byte) 47,
      (byte) 181,
      (byte) 26,
      (byte) 200,
      (byte) 74,
      (byte) 25,
      (byte) 199,
      (byte) 200,
      (byte) 36,
      (byte) 107,
      (byte) 57,
      (byte) 76,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 104
    };
    byte[] numArray7 = new byte[19];
    numArray7[11] = (byte) 246;
    numArray7[15] = (byte) 39;
    numArray7[0] = (byte) 234;
    numArray7[12] = (byte) 175;
    numArray7[4] = (byte) 14;
    numArray7[13] = (byte) 30;
    numArray7[6] = (byte) 157;
    numArray7[14] = (byte) 156;
    numArray7[5] = (byte) 199;
    numArray7[3] = (byte) 52;
    numArray7[2] = (byte) 211;
    numArray7[1] = (byte) 217;
    numArray7[16 /*0x10*/] = (byte) 51;
    numArray7[10] = (byte) 218;
    numArray7[8] = (byte) 228;
    numArray7[7] = (byte) 176 /*0xB0*/;
    numArray7[9] = (byte) 226;
    numArray7[17] = (byte) 65;
    numArray7[18] = (byte) 149;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
