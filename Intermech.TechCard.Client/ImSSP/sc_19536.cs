// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19536
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19536
{
  private static byte[] sspq = new byte[27]
  {
    (byte) 253,
    (byte) 140,
    (byte) 74,
    (byte) 252,
    (byte) 139,
    (byte) 175,
    (byte) 30,
    (byte) 5,
    (byte) 75,
    (byte) 192 /*0xC0*/,
    (byte) 83,
    (byte) 92,
    (byte) 95,
    (byte) 231,
    (byte) 109,
    (byte) 115,
    (byte) 192 /*0xC0*/,
    (byte) 84,
    (byte) 94,
    (byte) 8,
    (byte) 26,
    (byte) 57,
    (byte) 237,
    (byte) 183,
    (byte) 81,
    (byte) 254,
    (byte) 31 /*0x1F*/
  };
  private static byte[] sspr = new byte[27]
  {
    (byte) 83,
    (byte) 186,
    (byte) 202,
    (byte) 242,
    (byte) 217,
    (byte) 19,
    (byte) 20,
    (byte) 7,
    (byte) 155,
    (byte) 40,
    (byte) 44,
    (byte) 17,
    (byte) 97,
    (byte) 0,
    (byte) 142,
    (byte) 53,
    (byte) 3,
    (byte) 21,
    (byte) 200,
    (byte) 253,
    (byte) 26,
    (byte) 187,
    (byte) 7,
    (byte) 128 /*0x80*/,
    (byte) 185,
    (byte) 160 /*0xA0*/,
    (byte) 219
  };

  internal static string ssp_techcard_19537()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 32 /*0x20*/,
        (byte) 212,
        (byte) 150,
        (byte) 32 /*0x20*/,
        (byte) 244,
        (byte) 149,
        (byte) 201,
        (byte) 54,
        (byte) 185,
        (byte) 126,
        (byte) 128 /*0x80*/,
        (byte) 88,
        (byte) 163,
        (byte) 252,
        (byte) 141,
        (byte) 0,
        (byte) 170,
        (byte) 223,
        (byte) 206
      };
      byte[] numArray3 = new byte[19];
      numArray3[18] = (byte) 69;
      numArray3[1] = (byte) 229;
      numArray3[16 /*0x10*/] = (byte) 230;
      numArray3[3] = (byte) 195;
      numArray3[5] = (byte) 178;
      numArray3[15] = (byte) 203;
      numArray3[10] = (byte) 91;
      numArray3[6] = (byte) 41;
      numArray3[8] = (byte) 73;
      numArray3[9] = (byte) 184;
      numArray3[2] = (byte) 126;
      numArray3[14] = (byte) 92;
      numArray3[4] = (byte) 153;
      numArray3[13] = (byte) 235;
      numArray3[7] = (byte) 109;
      numArray3[12] = (byte) 108;
      numArray3[11] = (byte) 63 /*0x3F*/;
      numArray3[17] = (byte) 121;
      numArray3[0] = (byte) 70;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[27];
      byte[] response = new byte[27];
      Array.Copy((Array) sc_19536.sspq, 0, (Array) numArray4, 0, 27);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19536.sspr, 0, (Array) numArray4, 0, 27);
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
    byte[] numArray6 = new byte[19];
    numArray6[3] = (byte) 75;
    numArray6[14] = (byte) 23;
    numArray6[2] = (byte) 1;
    numArray6[12] = (byte) 135;
    numArray6[18] = (byte) 169;
    numArray6[9] = (byte) 215;
    numArray6[6] = (byte) 62;
    numArray6[7] = (byte) 59;
    numArray6[8] = (byte) 218;
    numArray6[4] = (byte) 192 /*0xC0*/;
    numArray6[1] = (byte) 230;
    numArray6[11] = (byte) 20;
    numArray6[17] = (byte) 116;
    numArray6[13] = (byte) 161;
    numArray6[15] = (byte) 52;
    numArray6[5] = (byte) 131;
    numArray6[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    numArray6[0] = (byte) 194;
    numArray6[10] = (byte) 99;
    byte[] numArray7 = new byte[19]
    {
      (byte) 57,
      (byte) 143,
      (byte) 74,
      (byte) 150,
      (byte) 218,
      (byte) 138,
      (byte) 53,
      (byte) 122,
      (byte) 56,
      (byte) 136,
      (byte) 187,
      (byte) 95,
      (byte) 120,
      (byte) 222,
      (byte) 124,
      (byte) 75,
      (byte) 80 /*0x50*/,
      (byte) 122,
      (byte) 136
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19538()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 146,
        (byte) 155,
        (byte) 93,
        (byte) 64 /*0x40*/,
        (byte) 214,
        (byte) 103,
        (byte) 3,
        (byte) 4,
        (byte) 28,
        (byte) 66,
        (byte) 168,
        (byte) 184,
        (byte) 42,
        (byte) 99,
        (byte) 63 /*0x3F*/,
        (byte) 25,
        (byte) 12,
        (byte) 16 /*0x10*/,
        (byte) 98
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 29,
        (byte) 212,
        (byte) 252,
        (byte) 166,
        (byte) 78,
        (byte) 56,
        (byte) 85,
        (byte) 196,
        (byte) 110,
        (byte) 249,
        (byte) 244,
        (byte) 123,
        (byte) 17,
        (byte) 190,
        (byte) 56,
        (byte) 223,
        (byte) 36,
        (byte) 24,
        (byte) 158
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
      (byte) 68,
      (byte) 185,
      (byte) 135,
      (byte) 247,
      (byte) 68,
      (byte) 247,
      (byte) 235,
      (byte) 91,
      (byte) 225,
      (byte) 208 /*0xD0*/,
      (byte) 36,
      (byte) 72,
      (byte) 204,
      (byte) 11,
      (byte) 139,
      (byte) 254,
      (byte) 217,
      (byte) 185,
      (byte) 212
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 163,
      (byte) 192 /*0xC0*/,
      (byte) 220,
      (byte) 130,
      (byte) 204,
      (byte) 243,
      (byte) 40,
      (byte) 189,
      (byte) 57,
      (byte) 200,
      (byte) 180,
      (byte) 71,
      (byte) 36,
      (byte) 184,
      (byte) 35,
      (byte) 35,
      (byte) 215,
      (byte) 60,
      (byte) 171
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
