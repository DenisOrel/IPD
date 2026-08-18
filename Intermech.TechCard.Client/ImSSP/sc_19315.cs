// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19315
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19315
{
  private static byte[] sspq = new byte[72]
  {
    (byte) 68,
    (byte) 172,
    (byte) 10,
    (byte) 201,
    (byte) 31 /*0x1F*/,
    (byte) 64 /*0x40*/,
    (byte) 152,
    (byte) 194,
    (byte) 238,
    (byte) 49,
    (byte) 13,
    (byte) 168,
    (byte) 72,
    (byte) 109,
    (byte) 159,
    (byte) 186,
    (byte) 7,
    (byte) 117,
    (byte) 87,
    (byte) 57,
    (byte) 215,
    (byte) 240 /*0xF0*/,
    (byte) 202,
    (byte) 143,
    (byte) 142,
    (byte) 5,
    (byte) 75,
    (byte) 207,
    (byte) 90,
    (byte) 244,
    (byte) 195,
    (byte) 234,
    (byte) 236,
    (byte) 153,
    (byte) 234,
    (byte) 104,
    (byte) 95,
    (byte) 36,
    (byte) 35,
    (byte) 235,
    (byte) 96 /*0x60*/,
    (byte) 16 /*0x10*/,
    (byte) 34,
    (byte) 217,
    (byte) 20,
    (byte) 239,
    (byte) 20,
    (byte) 114,
    (byte) 113,
    (byte) 46,
    (byte) 244,
    (byte) 247,
    (byte) 128 /*0x80*/,
    (byte) 194,
    (byte) 56,
    (byte) 68,
    (byte) 16 /*0x10*/,
    (byte) 214,
    (byte) 223,
    (byte) 122,
    (byte) 146,
    (byte) 106,
    (byte) 101,
    (byte) 126,
    (byte) 89,
    (byte) 149,
    (byte) 221,
    (byte) 185,
    (byte) 247,
    (byte) 17,
    (byte) 31 /*0x1F*/,
    (byte) 104
  };
  private static byte[] sspr = new byte[72]
  {
    (byte) 18,
    (byte) 113,
    (byte) 122,
    (byte) 225,
    (byte) 183,
    (byte) 108,
    (byte) 220,
    (byte) 167,
    (byte) 145,
    (byte) 241,
    (byte) 224 /*0xE0*/,
    (byte) 239,
    (byte) 222,
    (byte) 247,
    (byte) 29,
    (byte) 196,
    (byte) 45,
    (byte) 165,
    (byte) 176 /*0xB0*/,
    (byte) 186,
    (byte) 241,
    (byte) 126,
    (byte) 110,
    (byte) 18,
    (byte) 68,
    (byte) 32 /*0x20*/,
    (byte) 197,
    (byte) 181,
    (byte) 30,
    (byte) 226,
    (byte) 17,
    (byte) 210,
    (byte) 207,
    (byte) 242,
    (byte) 103,
    (byte) 198,
    (byte) 243,
    (byte) 38,
    (byte) 5,
    (byte) 164,
    (byte) 47,
    (byte) 163,
    (byte) 190,
    (byte) 237,
    (byte) 236,
    (byte) 89,
    (byte) 177,
    (byte) 165,
    (byte) 254,
    (byte) 130,
    (byte) 26,
    (byte) 211,
    (byte) 203,
    (byte) 117,
    (byte) 164,
    (byte) 181,
    (byte) 229,
    (byte) 105,
    (byte) 236,
    (byte) 127 /*0x7F*/,
    (byte) 185,
    (byte) 164,
    (byte) 28,
    (byte) 75,
    (byte) 80 /*0x50*/,
    (byte) 20,
    (byte) 198,
    (byte) 50,
    (byte) 253,
    (byte) 254,
    (byte) 209,
    (byte) 16 /*0x10*/
  };

  internal static string ssp_techcard_19316()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 119,
        (byte) 245,
        (byte) 163,
        (byte) 24,
        (byte) 160 /*0xA0*/,
        (byte) 177,
        (byte) 126,
        (byte) 22,
        (byte) 44,
        (byte) 33,
        (byte) 132,
        (byte) 53,
        (byte) 220,
        (byte) 196,
        (byte) 223,
        (byte) 72,
        (byte) 144 /*0x90*/,
        (byte) 70,
        (byte) 142
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 127 /*0x7F*/,
        (byte) 108,
        (byte) 0,
        (byte) 52,
        (byte) 139,
        (byte) 26,
        (byte) 210,
        (byte) 240 /*0xF0*/,
        (byte) 70,
        (byte) 124,
        (byte) 61,
        (byte) 27,
        (byte) 212,
        (byte) 239,
        (byte) 56,
        (byte) 131,
        (byte) 175,
        (byte) 213,
        (byte) 199
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
      (byte) 76,
      (byte) 91,
      (byte) 171,
      (byte) 0,
      (byte) 246,
      (byte) 119,
      (byte) 160 /*0xA0*/,
      (byte) 213,
      (byte) 231,
      (byte) 172,
      (byte) 69,
      (byte) 17,
      (byte) 52,
      (byte) 148,
      (byte) 182,
      (byte) 236,
      (byte) 125,
      (byte) 244,
      (byte) 199
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 185,
      (byte) 40,
      (byte) 64 /*0x40*/,
      (byte) 16 /*0x10*/,
      (byte) 127 /*0x7F*/,
      (byte) 57,
      (byte) 116,
      (byte) 78,
      (byte) 112 /*0x70*/,
      (byte) 7,
      (byte) 20,
      (byte) 103,
      (byte) 51,
      (byte) 34,
      (byte) 169,
      (byte) 9,
      (byte) 88,
      (byte) 30,
      (byte) 136
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[24];
    byte[] response = new byte[24];
    Array.Copy((Array) sc_19315.sspq, 0, (Array) numArray7, 0, 24);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19315.sspr, 0, (Array) numArray7, 0, 24);
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

  internal static string ssp_techcard_19317()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 146,
        (byte) 163,
        (byte) 200,
        (byte) 153,
        (byte) 207,
        (byte) 149,
        (byte) 246,
        (byte) 148,
        (byte) 185,
        (byte) 103,
        (byte) 84,
        (byte) 235,
        (byte) 173,
        (byte) 129,
        (byte) 148,
        (byte) 79,
        (byte) 234,
        (byte) 86,
        (byte) 224 /*0xE0*/
      };
      byte[] numArray3 = new byte[19];
      numArray3[15] = (byte) 6;
      numArray3[1] = (byte) 208 /*0xD0*/;
      numArray3[2] = (byte) 189;
      numArray3[4] = (byte) 197;
      numArray3[16 /*0x10*/] = (byte) 20;
      numArray3[13] = (byte) 199;
      numArray3[3] = (byte) 222;
      numArray3[7] = (byte) 244;
      numArray3[8] = (byte) 179;
      numArray3[17] = (byte) 56;
      numArray3[12] = (byte) 119;
      numArray3[11] = (byte) 184;
      numArray3[5] = (byte) 116;
      numArray3[9] = (byte) 70;
      numArray3[6] = (byte) 9;
      numArray3[0] = (byte) 116;
      numArray3[10] = (byte) 184;
      numArray3[14] = (byte) 152;
      numArray3[18] = (byte) 195;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_19315.sspq, 24, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19315.sspr, 24, (Array) numArray4, 0, 48 /*0x30*/);
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
      (byte) 121,
      (byte) 227,
      (byte) 199,
      (byte) 14,
      (byte) 121,
      (byte) 61,
      (byte) 0,
      (byte) 158,
      (byte) 240 /*0xF0*/,
      (byte) 216,
      (byte) 93,
      (byte) 44,
      byte.MaxValue,
      (byte) 203,
      (byte) 105,
      (byte) 42,
      (byte) 97,
      (byte) 165,
      (byte) 88
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 68,
      (byte) 173,
      (byte) 66,
      (byte) 134,
      (byte) 52,
      (byte) 229,
      (byte) 218,
      (byte) 169,
      (byte) 12,
      (byte) 57,
      (byte) 183,
      (byte) 59,
      (byte) 227,
      (byte) 157,
      (byte) 41,
      (byte) 203,
      (byte) 86,
      (byte) 221,
      (byte) 169
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
