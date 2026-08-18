// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19558
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19558
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 12,
    (byte) 184,
    (byte) 241,
    (byte) 230,
    (byte) 78,
    (byte) 94,
    (byte) 28,
    (byte) 50,
    (byte) 248,
    (byte) 124,
    (byte) 175,
    (byte) 231,
    (byte) 241,
    (byte) 182,
    (byte) 124,
    (byte) 224 /*0xE0*/,
    (byte) 205,
    (byte) 77,
    (byte) 211,
    (byte) 59,
    (byte) 143,
    (byte) 151,
    (byte) 134,
    (byte) 78,
    (byte) 7,
    (byte) 106,
    (byte) 32 /*0x20*/,
    (byte) 62,
    (byte) 22,
    (byte) 243,
    (byte) 130,
    (byte) 206,
    (byte) 211,
    (byte) 94,
    (byte) 168,
    (byte) 191,
    (byte) 217,
    (byte) 251
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 130,
    (byte) 19,
    (byte) 88,
    (byte) 87,
    (byte) 110,
    (byte) 245,
    (byte) 21,
    (byte) 6,
    (byte) 115,
    (byte) 88,
    (byte) 140,
    (byte) 81,
    (byte) 224 /*0xE0*/,
    (byte) 15,
    (byte) 69,
    (byte) 44,
    (byte) 84,
    (byte) 140,
    (byte) 238,
    (byte) 168,
    (byte) 82,
    (byte) 231,
    (byte) 91,
    (byte) 149,
    (byte) 165,
    (byte) 38,
    (byte) 124,
    (byte) 139,
    (byte) 188,
    (byte) 185,
    (byte) 165,
    (byte) 240 /*0xF0*/,
    (byte) 22,
    (byte) 218,
    (byte) 67,
    (byte) 224 /*0xE0*/,
    (byte) 83,
    (byte) 240 /*0xF0*/
  };

  internal static string ssp_techcard_19559()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 108,
        (byte) 244,
        (byte) 171,
        (byte) 176 /*0xB0*/,
        (byte) 51,
        (byte) 87,
        (byte) 82,
        (byte) 48 /*0x30*/,
        (byte) 118,
        (byte) 172,
        (byte) 128 /*0x80*/,
        (byte) 206,
        (byte) 149,
        (byte) 15,
        (byte) 17,
        (byte) 114,
        (byte) 193,
        (byte) 101,
        (byte) 75
      };
      byte[] numArray3 = new byte[19];
      numArray3[7] = (byte) 89;
      numArray3[14] = (byte) 189;
      numArray3[1] = (byte) 60;
      numArray3[9] = (byte) 68;
      numArray3[3] = (byte) 124;
      numArray3[5] = (byte) 35;
      numArray3[6] = (byte) 75;
      numArray3[0] = (byte) 2;
      numArray3[12] = (byte) 215;
      numArray3[10] = (byte) 72;
      numArray3[13] = (byte) 27;
      numArray3[11] = (byte) 18;
      numArray3[4] = (byte) 125;
      numArray3[2] = (byte) 221;
      numArray3[8] = (byte) 95;
      numArray3[15] = (byte) 136;
      numArray3[16 /*0x10*/] = (byte) 7;
      numArray3[17] = (byte) 122;
      numArray3[18] = (byte) 210;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_19558.sspq, 0, (Array) numArray4, 0, 38);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19558.sspr, 0, (Array) numArray4, 0, 38);
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
      (byte) 151,
      (byte) 38,
      (byte) 250,
      (byte) 110,
      (byte) 230,
      (byte) 169,
      (byte) 130,
      (byte) 223,
      (byte) 58,
      (byte) 56,
      (byte) 87,
      (byte) 122,
      (byte) 0,
      (byte) 149,
      (byte) 169,
      (byte) 97,
      (byte) 127 /*0x7F*/,
      (byte) 117,
      (byte) 49
    };
    byte[] numArray7 = new byte[19];
    numArray7[7] = (byte) 83;
    numArray7[0] = (byte) 106;
    numArray7[14] = (byte) 149;
    numArray7[9] = (byte) 188;
    numArray7[4] = (byte) 204;
    numArray7[5] = (byte) 36;
    numArray7[10] = (byte) 3;
    numArray7[2] = (byte) 38;
    numArray7[8] = (byte) 111;
    numArray7[11] = (byte) 194;
    numArray7[6] = (byte) 205;
    numArray7[12] = (byte) 233;
    numArray7[15] = (byte) 63 /*0x3F*/;
    numArray7[13] = (byte) 125;
    numArray7[3] = (byte) 4;
    numArray7[16 /*0x10*/] = (byte) 119;
    numArray7[18] = (byte) 214;
    numArray7[17] = (byte) 0;
    numArray7[1] = (byte) 173;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
