// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19300
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19300
{
  private static byte[] sspq = new byte[31 /*0x1F*/]
  {
    (byte) 133,
    (byte) 238,
    (byte) 191,
    (byte) 82,
    (byte) 37,
    (byte) 186,
    (byte) 131,
    (byte) 235,
    (byte) 95,
    (byte) 9,
    (byte) 43,
    (byte) 165,
    (byte) 58,
    (byte) 98,
    (byte) 114,
    (byte) 148,
    (byte) 95,
    (byte) 12,
    (byte) 47,
    (byte) 81,
    (byte) 251,
    (byte) 183,
    (byte) 37,
    (byte) 175,
    (byte) 201,
    (byte) 33,
    (byte) 60,
    (byte) 185,
    (byte) 90,
    (byte) 128 /*0x80*/,
    (byte) 219
  };
  private static byte[] sspr = new byte[31 /*0x1F*/]
  {
    (byte) 194,
    (byte) 146,
    (byte) 19,
    (byte) 133,
    (byte) 162,
    (byte) 138,
    (byte) 17,
    (byte) 224 /*0xE0*/,
    (byte) 232,
    (byte) 162,
    (byte) 184,
    (byte) 60,
    (byte) 239,
    (byte) 46,
    (byte) 180,
    (byte) 23,
    (byte) 25,
    (byte) 224 /*0xE0*/,
    (byte) 130,
    (byte) 36,
    (byte) 83,
    (byte) 244,
    (byte) 180,
    (byte) 244,
    (byte) 131,
    (byte) 126,
    (byte) 253,
    (byte) 13,
    (byte) 169,
    (byte) 143,
    (byte) 79
  };

  internal static string ssp_techcard_19301()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[16 /*0x10*/] = (byte) 227;
      numArray2[1] = (byte) 44;
      numArray2[2] = (byte) 38;
      numArray2[6] = (byte) 236;
      numArray2[15] = (byte) 160 /*0xA0*/;
      numArray2[3] = (byte) 177;
      numArray2[8] = (byte) 229;
      numArray2[17] = (byte) 146;
      numArray2[18] = (byte) 126;
      numArray2[9] = (byte) 122;
      numArray2[4] = (byte) 122;
      numArray2[11] = (byte) 123;
      numArray2[5] = (byte) 2;
      numArray2[13] = (byte) 247;
      numArray2[14] = (byte) 114;
      numArray2[10] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 13;
      numArray2[7] = (byte) 186;
      numArray2[0] = (byte) 147;
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 129;
      numArray3[1] = (byte) 102;
      numArray3[2] = (byte) 54;
      numArray3[3] = (byte) 132;
      numArray3[15] = (byte) 177;
      numArray3[5] = (byte) 94;
      numArray3[6] = (byte) 188;
      numArray3[12] = (byte) 127 /*0x7F*/;
      numArray3[14] = (byte) 15;
      numArray3[4] = (byte) 138;
      numArray3[0] = (byte) 135;
      numArray3[11] = (byte) 184;
      numArray3[10] = (byte) 202;
      numArray3[13] = (byte) 94;
      numArray3[7] = (byte) 141;
      numArray3[9] = (byte) 180;
      numArray3[16 /*0x10*/] = (byte) 89;
      numArray3[17] = (byte) 47;
      numArray3[18] = (byte) 27;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/];
      byte[] response = new byte[31 /*0x1F*/];
      Array.Copy((Array) sc_19300.sspq, 0, (Array) numArray4, 0, 31 /*0x1F*/);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19300.sspr, 0, (Array) numArray4, 0, 31 /*0x1F*/);
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
      (byte) 127 /*0x7F*/,
      (byte) 167,
      (byte) 12,
      (byte) 8,
      (byte) 145,
      (byte) 141,
      (byte) 233,
      (byte) 57,
      (byte) 143,
      (byte) 96 /*0x60*/,
      (byte) 228,
      (byte) 78,
      (byte) 157,
      (byte) 74,
      (byte) 184,
      (byte) 190,
      (byte) 34,
      (byte) 94,
      (byte) 28
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 5,
      (byte) 174,
      (byte) 182,
      (byte) 28,
      (byte) 179,
      (byte) 194,
      (byte) 115,
      (byte) 117,
      (byte) 251,
      (byte) 56,
      (byte) 247,
      (byte) 194,
      (byte) 205,
      (byte) 206,
      (byte) 33,
      (byte) 151,
      (byte) 166,
      (byte) 141,
      (byte) 147
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
