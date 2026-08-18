// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19371
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19371
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 34,
    (byte) 223,
    (byte) 25,
    (byte) 15,
    (byte) 83,
    (byte) 98,
    (byte) 52,
    (byte) 29,
    (byte) 177,
    (byte) 7,
    (byte) 125,
    (byte) 122,
    (byte) 127 /*0x7F*/,
    (byte) 48 /*0x30*/,
    (byte) 58,
    (byte) 168,
    (byte) 43,
    (byte) 243,
    (byte) 58,
    (byte) 179,
    (byte) 164,
    (byte) 210,
    (byte) 99,
    (byte) 63 /*0x3F*/,
    byte.MaxValue,
    (byte) 15,
    (byte) 177,
    (byte) 19,
    (byte) 238,
    (byte) 151,
    (byte) 89,
    (byte) 4,
    (byte) 185,
    (byte) 141,
    (byte) 236,
    (byte) 187,
    (byte) 128 /*0x80*/,
    (byte) 12
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 217,
    (byte) 198,
    (byte) 7,
    (byte) 5,
    (byte) 137,
    (byte) 61,
    (byte) 93,
    (byte) 115,
    (byte) 16 /*0x10*/,
    (byte) 253,
    (byte) 83,
    (byte) 226,
    (byte) 106,
    (byte) 198,
    (byte) 25,
    (byte) 156,
    (byte) 197,
    (byte) 137,
    (byte) 143,
    (byte) 179,
    (byte) 16 /*0x10*/,
    (byte) 65,
    (byte) 111,
    (byte) 211,
    (byte) 142,
    (byte) 179,
    (byte) 183,
    (byte) 252,
    (byte) 25,
    (byte) 249,
    (byte) 94,
    (byte) 28,
    (byte) 77,
    (byte) 124,
    (byte) 8,
    (byte) 210,
    (byte) 155,
    (byte) 36
  };

  internal static string ssp_techcard_19372()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 245,
        (byte) 170,
        (byte) 163,
        (byte) 127 /*0x7F*/,
        (byte) 127 /*0x7F*/,
        (byte) 18,
        (byte) 91,
        (byte) 123,
        (byte) 95,
        (byte) 14,
        (byte) 139,
        (byte) 125,
        (byte) 156,
        (byte) 250,
        (byte) 41,
        (byte) 203,
        (byte) 210,
        (byte) 214,
        (byte) 64 /*0x40*/
      };
      byte[] numArray3 = new byte[19];
      numArray3[16 /*0x10*/] = (byte) 244;
      numArray3[4] = (byte) 10;
      numArray3[6] = (byte) 154;
      numArray3[18] = (byte) 197;
      numArray3[8] = (byte) 33;
      numArray3[13] = (byte) 60;
      numArray3[14] = (byte) 49;
      numArray3[3] = (byte) 64 /*0x40*/;
      numArray3[11] = (byte) 58;
      numArray3[9] = (byte) 27;
      numArray3[1] = (byte) 121;
      numArray3[12] = (byte) 142;
      numArray3[7] = (byte) 140;
      numArray3[5] = (byte) 79;
      numArray3[2] = (byte) 131;
      numArray3[15] = (byte) 139;
      numArray3[0] = (byte) 3;
      numArray3[17] = (byte) 178;
      numArray3[10] = (byte) 23;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 93,
      (byte) 253,
      (byte) 29,
      (byte) 115,
      (byte) 77,
      (byte) 205,
      (byte) 48 /*0x30*/,
      (byte) 55,
      (byte) 218,
      (byte) 133,
      (byte) 177,
      (byte) 5,
      (byte) 31 /*0x1F*/,
      (byte) 159,
      (byte) 232,
      (byte) 113,
      (byte) 36,
      (byte) 241,
      (byte) 84
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 134,
      (byte) 242,
      (byte) 98,
      (byte) 179,
      (byte) 217,
      (byte) 198,
      (byte) 195,
      (byte) 202,
      (byte) 154,
      (byte) 26,
      (byte) 5,
      (byte) 64 /*0x40*/,
      (byte) 171,
      (byte) 70,
      (byte) 101,
      (byte) 252,
      (byte) 94,
      (byte) 211,
      (byte) 211
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_techcard_19373(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 104;
    sourceArray1[1] = (byte) 174;
    sourceArray1[32 /*0x20*/] = (byte) 87;
    sourceArray1[9] = (byte) 244;
    sourceArray1[4] = (byte) 198;
    sourceArray1[21] = (byte) 30;
    sourceArray1[6] = (byte) 20;
    sourceArray1[37] = (byte) 7;
    sourceArray1[3] = (byte) 40;
    sourceArray1[19] = (byte) 51;
    sourceArray1[10] = (byte) 92;
    sourceArray1[5] = (byte) 244;
    sourceArray1[12] = (byte) 27;
    sourceArray1[42] = (byte) 80 /*0x50*/;
    sourceArray1[14] = (byte) 64 /*0x40*/;
    sourceArray1[11] = (byte) 14;
    sourceArray1[39] = (byte) 229;
    sourceArray1[17] = (byte) 31 /*0x1F*/;
    sourceArray1[18] = (byte) 120;
    sourceArray1[15] = (byte) 94;
    sourceArray1[41] = (byte) 91;
    sourceArray1[8] = (byte) 180;
    sourceArray1[22] = (byte) 175;
    sourceArray1[23] = (byte) 234;
    sourceArray1[24] = (byte) 233;
    sourceArray1[25] = (byte) 241;
    sourceArray1[45] = (byte) 218;
    sourceArray1[27] = (byte) 205;
    sourceArray1[28] = (byte) 44;
    sourceArray1[29] = (byte) 43;
    sourceArray1[30] = (byte) 145;
    sourceArray1[40] = (byte) 113;
    sourceArray1[26] = (byte) 178;
    sourceArray1[13] = (byte) 11;
    sourceArray1[7] = (byte) 86;
    sourceArray1[35] = (byte) 159;
    sourceArray1[36] = (byte) 187;
    sourceArray1[33] = (byte) 165;
    sourceArray1[38] = (byte) 113;
    sourceArray1[31 /*0x1F*/] = (byte) 52;
    sourceArray1[44] = (byte) 66;
    sourceArray1[2] = (byte) 18;
    sourceArray1[34] = (byte) 11;
    sourceArray1[43] = (byte) 43;
    sourceArray1[16 /*0x10*/] = (byte) 130;
    sourceArray1[20] = (byte) 229;
    sourceArray1[46] = (byte) 86;
    sourceArray1[47] = (byte) 25;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 232,
      (byte) 109,
      (byte) 155,
      (byte) 133,
      (byte) 97,
      (byte) 51,
      (byte) 227,
      (byte) 153,
      (byte) 188,
      (byte) 218,
      (byte) 36,
      (byte) 58,
      (byte) 79,
      (byte) 18,
      (byte) 239,
      (byte) 118,
      (byte) 191,
      (byte) 58,
      (byte) 132,
      (byte) 238,
      (byte) 154,
      (byte) 245,
      (byte) 248,
      (byte) 124,
      (byte) 215,
      (byte) 111,
      (byte) 72,
      (byte) 63 /*0x3F*/,
      (byte) 46,
      (byte) 23,
      (byte) 254,
      (byte) 172,
      (byte) 161,
      (byte) 106,
      (byte) 208 /*0xD0*/,
      (byte) 231,
      (byte) 86,
      (byte) 183,
      (byte) 36,
      (byte) 206,
      (byte) 85,
      (byte) 187,
      (byte) 59,
      (byte) 143,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 89,
      (byte) 61
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 359, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[38];
    byte[] response2 = new byte[38];
    Array.Copy((Array) sc_19371.sspq, 0, (Array) numArray2, 0, 38);
    key.Query(true, 359, numArray2, response2);
    Array.Copy((Array) sc_19371.sspr, 0, (Array) numArray2, 0, 38);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }
}
