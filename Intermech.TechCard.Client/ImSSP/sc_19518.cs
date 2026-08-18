// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19518
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19518
{
  private static byte[] sspq = new byte[40]
  {
    (byte) 107,
    (byte) 134,
    (byte) 176 /*0xB0*/,
    (byte) 175,
    (byte) 10,
    (byte) 203,
    (byte) 105,
    (byte) 101,
    (byte) 131,
    (byte) 78,
    (byte) 163,
    (byte) 119,
    (byte) 209,
    (byte) 13,
    (byte) 227,
    (byte) 144 /*0x90*/,
    (byte) 41,
    (byte) 249,
    (byte) 209,
    (byte) 112 /*0x70*/,
    (byte) 83,
    (byte) 54,
    (byte) 36,
    (byte) 18,
    (byte) 184,
    (byte) 65,
    (byte) 185,
    (byte) 174,
    (byte) 133,
    (byte) 99,
    (byte) 144 /*0x90*/,
    (byte) 11,
    (byte) 65,
    (byte) 148,
    (byte) 5,
    (byte) 43,
    (byte) 194,
    (byte) 160 /*0xA0*/,
    (byte) 216,
    (byte) 109
  };
  private static byte[] sspr = new byte[40]
  {
    (byte) 241,
    (byte) 114,
    (byte) 4,
    (byte) 85,
    (byte) 104,
    (byte) 110,
    (byte) 189,
    (byte) 54,
    (byte) 46,
    (byte) 246,
    (byte) 173,
    (byte) 196,
    (byte) 237,
    (byte) 208 /*0xD0*/,
    (byte) 237,
    (byte) 122,
    (byte) 101,
    (byte) 200,
    (byte) 196,
    (byte) 231,
    (byte) 34,
    (byte) 25,
    (byte) 244,
    (byte) 238,
    (byte) 6,
    (byte) 250,
    (byte) 151,
    (byte) 67,
    (byte) 173,
    (byte) 192 /*0xC0*/,
    (byte) 136,
    (byte) 219,
    (byte) 232,
    (byte) 214,
    (byte) 20,
    (byte) 240 /*0xF0*/,
    (byte) 130,
    byte.MaxValue,
    (byte) 54,
    (byte) 16 /*0x10*/
  };

  internal static string ssp_techcard_19519()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 248,
        (byte) 112 /*0x70*/,
        (byte) 178,
        (byte) 115,
        (byte) 80 /*0x50*/,
        (byte) 226,
        (byte) 57,
        (byte) 112 /*0x70*/,
        (byte) 87,
        (byte) 108,
        (byte) 166,
        (byte) 219,
        (byte) 236,
        (byte) 222,
        (byte) 115,
        (byte) 59,
        (byte) 174,
        (byte) 89
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 160 /*0xA0*/,
        (byte) 212,
        (byte) 30,
        (byte) 50,
        (byte) 202,
        (byte) 79,
        (byte) 121,
        (byte) 243,
        (byte) 225,
        (byte) 0,
        (byte) 165,
        (byte) 92,
        (byte) 80 /*0x50*/,
        (byte) 56,
        (byte) 23,
        (byte) 209,
        (byte) 148,
        (byte) 131
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40];
      byte[] response = new byte[40];
      Array.Copy((Array) sc_19518.sspq, 0, (Array) numArray4, 0, 40);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19518.sspr, 0, (Array) numArray4, 0, 40);
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
    byte[] numArray5 = new byte[18];
    byte[] numArray6 = new byte[18];
    numArray6[0] = (byte) 7;
    numArray6[1] = (byte) 241;
    numArray6[2] = (byte) 232;
    numArray6[15] = (byte) 52;
    numArray6[17] = (byte) 158;
    numArray6[10] = (byte) 48 /*0x30*/;
    numArray6[4] = (byte) 56;
    numArray6[5] = (byte) 116;
    numArray6[12] = (byte) 191;
    numArray6[3] = (byte) 88;
    numArray6[9] = (byte) 29;
    numArray6[7] = (byte) 102;
    numArray6[13] = (byte) 215;
    numArray6[11] = (byte) 164;
    numArray6[14] = (byte) 17;
    numArray6[6] = (byte) 12;
    numArray6[16 /*0x10*/] = (byte) 97;
    numArray6[8] = (byte) 88;
    byte[] numArray7 = new byte[18]
    {
      (byte) 29,
      (byte) 108,
      (byte) 132,
      (byte) 68,
      (byte) 11,
      (byte) 51,
      (byte) 88,
      (byte) 81,
      (byte) 152,
      (byte) 126,
      (byte) 63 /*0x3F*/,
      (byte) 34,
      (byte) 167,
      (byte) 253,
      (byte) 49,
      (byte) 164,
      (byte) 20,
      (byte) 159
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
