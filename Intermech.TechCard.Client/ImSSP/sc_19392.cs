// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19392
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19392
{
  private static byte[] sspq = new byte[30]
  {
    (byte) 84,
    (byte) 54,
    (byte) 10,
    (byte) 196,
    (byte) 19,
    (byte) 114,
    (byte) 57,
    (byte) 231,
    (byte) 130,
    (byte) 156,
    (byte) 252,
    (byte) 218,
    (byte) 206,
    (byte) 180,
    (byte) 101,
    (byte) 117,
    (byte) 119,
    (byte) 104,
    (byte) 242,
    (byte) 166,
    (byte) 169,
    (byte) 131,
    (byte) 76,
    (byte) 188,
    (byte) 131,
    (byte) 39,
    (byte) 142,
    (byte) 106,
    (byte) 57,
    (byte) 112 /*0x70*/
  };
  private static byte[] sspr = new byte[30]
  {
    (byte) 100,
    (byte) 139,
    (byte) 110,
    (byte) 107,
    (byte) 83,
    (byte) 178,
    (byte) 148,
    (byte) 214,
    (byte) 41,
    (byte) 179,
    (byte) 198,
    (byte) 19,
    (byte) 150,
    (byte) 149,
    (byte) 75,
    (byte) 146,
    (byte) 95,
    (byte) 224 /*0xE0*/,
    (byte) 240 /*0xF0*/,
    (byte) 252,
    (byte) 43,
    (byte) 111,
    (byte) 48 /*0x30*/,
    (byte) 28,
    (byte) 67,
    (byte) 144 /*0x90*/,
    (byte) 78,
    (byte) 227,
    (byte) 217,
    (byte) 196
  };

  internal static string ssp_techcard_19393()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[1] = (byte) 13;
      numArray2[2] = (byte) 133;
      numArray2[15] = (byte) 52;
      numArray2[11] = byte.MaxValue;
      numArray2[6] = (byte) 158;
      numArray2[7] = (byte) 41;
      numArray2[5] = (byte) 228;
      numArray2[4] = (byte) 250;
      numArray2[8] = (byte) 202;
      numArray2[9] = (byte) 95;
      numArray2[10] = (byte) 224 /*0xE0*/;
      numArray2[0] = (byte) 227;
      numArray2[12] = (byte) 80 /*0x50*/;
      numArray2[17] = (byte) 39;
      numArray2[14] = (byte) 89;
      numArray2[13] = (byte) 170;
      numArray2[16 /*0x10*/] = (byte) 216;
      numArray2[3] = (byte) 93;
      byte[] numArray3 = new byte[18]
      {
        (byte) 231,
        (byte) 126,
        (byte) 184,
        (byte) 6,
        (byte) 163,
        (byte) 121,
        (byte) 105,
        (byte) 198,
        (byte) 88,
        (byte) 152,
        (byte) 149,
        (byte) 29,
        (byte) 37,
        (byte) 72,
        (byte) 122,
        (byte) 232,
        (byte) 54,
        (byte) 243
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 99,
      (byte) 164,
      (byte) 22,
      (byte) 33,
      (byte) 47,
      (byte) 47,
      (byte) 127 /*0x7F*/,
      (byte) 53,
      (byte) 236,
      (byte) 173,
      (byte) 104,
      (byte) 41,
      (byte) 156,
      (byte) 114,
      (byte) 128 /*0x80*/,
      (byte) 202,
      (byte) 126,
      (byte) 84
    };
    byte[] numArray6 = new byte[18];
    numArray6[11] = (byte) 1;
    numArray6[1] = (byte) 158;
    numArray6[7] = (byte) 110;
    numArray6[2] = (byte) 147;
    numArray6[14] = (byte) 121;
    numArray6[16 /*0x10*/] = (byte) 174;
    numArray6[6] = (byte) 231;
    numArray6[9] = (byte) 178;
    numArray6[8] = (byte) 215;
    numArray6[4] = (byte) 66;
    numArray6[10] = (byte) 98;
    numArray6[17] = (byte) 151;
    numArray6[5] = (byte) 65;
    numArray6[13] = (byte) 139;
    numArray6[3] = (byte) 128 /*0x80*/;
    numArray6[15] = (byte) 34;
    numArray6[12] = (byte) 51;
    numArray6[0] = (byte) 152;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[30];
    byte[] response = new byte[30];
    Array.Copy((Array) sc_19392.sspq, 0, (Array) numArray7, 0, 30);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19392.sspr, 0, (Array) numArray7, 0, 30);
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
}
