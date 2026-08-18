// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19459
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19459
{
  private static byte[] sspq = new byte[41]
  {
    (byte) 83,
    (byte) 186,
    (byte) 111,
    (byte) 183,
    (byte) 207,
    (byte) 236,
    (byte) 85,
    (byte) 61,
    (byte) 213,
    (byte) 85,
    (byte) 55,
    (byte) 245,
    (byte) 213,
    (byte) 225,
    (byte) 134,
    (byte) 221,
    (byte) 123,
    (byte) 228,
    (byte) 160 /*0xA0*/,
    (byte) 82,
    (byte) 46,
    (byte) 98,
    (byte) 159,
    (byte) 181,
    (byte) 105,
    (byte) 243,
    (byte) 58,
    (byte) 124,
    (byte) 175,
    (byte) 71,
    (byte) 6,
    (byte) 206,
    (byte) 222,
    (byte) 130,
    (byte) 199,
    (byte) 118,
    (byte) 120,
    (byte) 60,
    (byte) 18,
    (byte) 127 /*0x7F*/,
    (byte) 52
  };
  private static byte[] sspr = new byte[41]
  {
    (byte) 246,
    (byte) 71,
    (byte) 243,
    (byte) 117,
    (byte) 145,
    (byte) 19,
    (byte) 22,
    (byte) 10,
    (byte) 123,
    (byte) 0,
    (byte) 151,
    (byte) 118,
    (byte) 164,
    (byte) 93,
    (byte) 106,
    (byte) 185,
    (byte) 73,
    (byte) 223,
    (byte) 206,
    (byte) 233,
    (byte) 90,
    (byte) 223,
    (byte) 250,
    (byte) 137,
    (byte) 102,
    (byte) 46,
    (byte) 242,
    (byte) 41,
    (byte) 136,
    (byte) 219,
    (byte) 178,
    (byte) 84,
    (byte) 2,
    (byte) 5,
    (byte) 222,
    (byte) 83,
    (byte) 248,
    (byte) 136,
    (byte) 123,
    (byte) 33,
    (byte) 164
  };

  internal static string ssp_techcard_19460()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[11] = (byte) 157;
      numArray2[16 /*0x10*/] = (byte) 132;
      numArray2[2] = (byte) 13;
      numArray2[1] = (byte) 169;
      numArray2[5] = (byte) 63 /*0x3F*/;
      numArray2[17] = (byte) 222;
      numArray2[6] = (byte) 65;
      numArray2[7] = (byte) 217;
      numArray2[4] = (byte) 41;
      numArray2[9] = (byte) 194;
      numArray2[10] = (byte) 225;
      numArray2[3] = (byte) 156;
      numArray2[0] = (byte) 77;
      numArray2[13] = (byte) 235;
      numArray2[14] = (byte) 14;
      numArray2[8] = (byte) 8;
      numArray2[15] = (byte) 228;
      numArray2[18] = (byte) 237;
      numArray2[12] = (byte) 65;
      byte[] numArray3 = new byte[19]
      {
        (byte) 193,
        (byte) 226,
        (byte) 128 /*0x80*/,
        (byte) 59,
        (byte) 143,
        (byte) 14,
        (byte) 39,
        (byte) 10,
        (byte) 210,
        (byte) 212,
        (byte) 164,
        (byte) 185,
        (byte) 202,
        (byte) 149,
        (byte) 6,
        (byte) 231,
        (byte) 6,
        (byte) 111,
        (byte) 212
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
      (byte) 169,
      (byte) 146,
      (byte) 106,
      (byte) 100,
      (byte) 131,
      (byte) 225,
      (byte) 120,
      (byte) 44,
      (byte) 244,
      (byte) 189,
      (byte) 171,
      (byte) 249,
      (byte) 239,
      (byte) 221,
      (byte) 68,
      (byte) 150,
      (byte) 127 /*0x7F*/,
      (byte) 170,
      (byte) 143
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 199,
      (byte) 102,
      (byte) 0,
      (byte) 47,
      (byte) 147,
      (byte) 212,
      (byte) 202,
      (byte) 172,
      (byte) 106,
      (byte) 183,
      (byte) 117,
      (byte) 219,
      (byte) 72,
      (byte) 202,
      (byte) 66,
      (byte) 195,
      (byte) 245,
      (byte) 88,
      (byte) 85
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19461()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 251,
        (byte) 183,
        (byte) 197,
        (byte) 111,
        (byte) 197,
        (byte) 12,
        (byte) 219,
        (byte) 4,
        (byte) 88,
        (byte) 63 /*0x3F*/,
        (byte) 128 /*0x80*/,
        (byte) 37,
        (byte) 117,
        (byte) 169,
        (byte) 41,
        (byte) 109,
        (byte) 185,
        (byte) 185,
        (byte) 238
      };
      byte[] numArray3 = new byte[19];
      numArray3[18] = (byte) 87;
      numArray3[1] = (byte) 188;
      numArray3[2] = (byte) 162;
      numArray3[3] = (byte) 246;
      numArray3[9] = (byte) 211;
      numArray3[5] = (byte) 87;
      numArray3[13] = (byte) 62;
      numArray3[8] = (byte) 204;
      numArray3[4] = (byte) 127 /*0x7F*/;
      numArray3[15] = (byte) 98;
      numArray3[11] = (byte) 158;
      numArray3[7] = (byte) 42;
      numArray3[12] = (byte) 23;
      numArray3[16 /*0x10*/] = (byte) 187;
      numArray3[14] = (byte) 252;
      numArray3[10] = (byte) 11;
      numArray3[6] = (byte) 15;
      numArray3[17] = (byte) 106;
      numArray3[0] = (byte) 120;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_19459.sspq, 0, (Array) numArray4, 0, 41);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19459.sspr, 0, (Array) numArray4, 0, 41);
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
      (byte) 152,
      (byte) 32 /*0x20*/,
      (byte) 74,
      (byte) 147,
      (byte) 198,
      (byte) 172,
      (byte) 141,
      (byte) 39,
      (byte) 224 /*0xE0*/,
      (byte) 246,
      (byte) 205,
      (byte) 128 /*0x80*/,
      (byte) 198,
      (byte) 137,
      (byte) 241,
      (byte) 26,
      (byte) 186,
      (byte) 78,
      (byte) 219
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 38,
      (byte) 10,
      (byte) 14,
      (byte) 183,
      (byte) 213,
      (byte) 2,
      (byte) 240 /*0xF0*/,
      (byte) 23,
      (byte) 199,
      (byte) 46,
      (byte) 151,
      (byte) 21,
      (byte) 14,
      (byte) 99,
      (byte) 51,
      (byte) 152,
      (byte) 214,
      (byte) 50,
      (byte) 31 /*0x1F*/
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
