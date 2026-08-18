// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19752
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19752
{
  private static byte[] sspq = new byte[17]
  {
    (byte) 10,
    (byte) 96 /*0x60*/,
    (byte) 204,
    (byte) 118,
    (byte) 94,
    (byte) 121,
    (byte) 109,
    (byte) 99,
    byte.MaxValue,
    (byte) 31 /*0x1F*/,
    (byte) 147,
    (byte) 131,
    (byte) 36,
    (byte) 5,
    (byte) 21,
    (byte) 104,
    (byte) 108
  };
  private static byte[] sspr = new byte[17]
  {
    (byte) 20,
    (byte) 160 /*0xA0*/,
    (byte) 73,
    (byte) 99,
    (byte) 237,
    (byte) 215,
    (byte) 43,
    (byte) 218,
    (byte) 249,
    (byte) 229,
    (byte) 122,
    (byte) 84,
    (byte) 154,
    (byte) 14,
    (byte) 115,
    (byte) 203,
    (byte) 225
  };

  internal static string ssp_techcard_19753()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[4] = (byte) 78;
      numArray2[15] = (byte) 28;
      numArray2[2] = (byte) 195;
      numArray2[13] = (byte) 33;
      numArray2[8] = (byte) 155;
      numArray2[5] = (byte) 20;
      numArray2[6] = (byte) 212;
      numArray2[16 /*0x10*/] = (byte) 207;
      numArray2[14] = (byte) 121;
      numArray2[9] = (byte) 62;
      numArray2[10] = (byte) 200;
      numArray2[11] = (byte) 87;
      numArray2[12] = (byte) 114;
      numArray2[3] = (byte) 4;
      numArray2[7] = (byte) 25;
      numArray2[1] = (byte) 187;
      numArray2[0] = (byte) 33;
      numArray2[17] = (byte) 103;
      numArray2[18] = (byte) 192 /*0xC0*/;
      byte[] numArray3 = new byte[19]
      {
        (byte) 197,
        (byte) 221,
        (byte) 30,
        (byte) 219,
        (byte) 247,
        (byte) 118,
        (byte) 240 /*0xF0*/,
        (byte) 245,
        (byte) 61,
        (byte) 193,
        (byte) 161,
        (byte) 197,
        (byte) 222,
        (byte) 73,
        (byte) 39,
        (byte) 242,
        (byte) 249,
        (byte) 214,
        (byte) 72
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17];
      byte[] response = new byte[17];
      Array.Copy((Array) sc_19752.sspq, 0, (Array) numArray4, 0, 17);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19752.sspr, 0, (Array) numArray4, 0, 17);
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
    numArray6[18] = (byte) 68;
    numArray6[1] = (byte) 193;
    numArray6[12] = (byte) 134;
    numArray6[9] = (byte) 15;
    numArray6[4] = (byte) 8;
    numArray6[5] = (byte) 160 /*0xA0*/;
    numArray6[2] = (byte) 86;
    numArray6[7] = (byte) 208 /*0xD0*/;
    numArray6[17] = (byte) 17;
    numArray6[10] = (byte) 216;
    numArray6[15] = (byte) 206;
    numArray6[11] = (byte) 51;
    numArray6[13] = (byte) 204;
    numArray6[6] = (byte) 73;
    numArray6[14] = (byte) 245;
    numArray6[8] = (byte) 143;
    numArray6[16 /*0x10*/] = (byte) 172;
    numArray6[0] = (byte) 115;
    numArray6[3] = (byte) 7;
    byte[] numArray7 = new byte[19];
    numArray7[0] = (byte) 54;
    numArray7[1] = (byte) 40;
    numArray7[10] = (byte) 245;
    numArray7[3] = (byte) 233;
    numArray7[16 /*0x10*/] = (byte) 48 /*0x30*/;
    numArray7[2] = (byte) 183;
    numArray7[4] = (byte) 88;
    numArray7[7] = (byte) 177;
    numArray7[8] = (byte) 196;
    numArray7[9] = (byte) 122;
    numArray7[12] = (byte) 137;
    numArray7[11] = (byte) 147;
    numArray7[13] = (byte) 0;
    numArray7[17] = (byte) 200;
    numArray7[5] = (byte) 124;
    numArray7[15] = (byte) 225;
    numArray7[6] = (byte) 123;
    numArray7[14] = (byte) 171;
    numArray7[18] = (byte) 179;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
