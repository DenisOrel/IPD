// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19595
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19595
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 229,
    (byte) 211,
    (byte) 2,
    (byte) 9,
    (byte) 185,
    (byte) 118,
    (byte) 157,
    (byte) 125,
    (byte) 9,
    (byte) 102,
    (byte) 26,
    (byte) 183,
    (byte) 226,
    (byte) 252,
    (byte) 134,
    (byte) 73,
    (byte) 26,
    (byte) 51,
    (byte) 228,
    (byte) 127 /*0x7F*/,
    (byte) 110,
    (byte) 55,
    (byte) 126,
    (byte) 63 /*0x3F*/,
    (byte) 154,
    (byte) 31 /*0x1F*/,
    (byte) 133,
    (byte) 87,
    (byte) 219,
    (byte) 218,
    (byte) 74,
    (byte) 186,
    (byte) 129,
    (byte) 4,
    (byte) 31 /*0x1F*/
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 233,
    (byte) 58,
    (byte) 19,
    (byte) 225,
    (byte) 143,
    (byte) 141,
    (byte) 222,
    (byte) 81,
    (byte) 71,
    (byte) 22,
    (byte) 122,
    (byte) 63 /*0x3F*/,
    (byte) 70,
    (byte) 215,
    (byte) 46,
    (byte) 191,
    (byte) 106,
    (byte) 197,
    (byte) 101,
    (byte) 69,
    (byte) 67,
    (byte) 96 /*0x60*/,
    (byte) 158,
    (byte) 84,
    (byte) 41,
    (byte) 127 /*0x7F*/,
    (byte) 135,
    (byte) 238,
    (byte) 168,
    (byte) 92,
    (byte) 50,
    (byte) 65,
    (byte) 196,
    (byte) 12,
    (byte) 134
  };

  internal static string ssp_techcard_19596()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[2] = (byte) 16 /*0x10*/;
      numArray2[15] = (byte) 100;
      numArray2[7] = (byte) 219;
      numArray2[3] = (byte) 193;
      numArray2[4] = (byte) 120;
      numArray2[5] = (byte) 240 /*0xF0*/;
      numArray2[6] = (byte) 198;
      numArray2[1] = (byte) 24;
      numArray2[8] = (byte) 162;
      numArray2[9] = (byte) 215;
      numArray2[10] = (byte) 4;
      numArray2[11] = (byte) 217;
      numArray2[12] = (byte) 245;
      numArray2[13] = (byte) 7;
      numArray2[14] = (byte) 35;
      numArray2[0] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 37;
      numArray2[17] = (byte) 63 /*0x3F*/;
      numArray2[18] = (byte) 33;
      byte[] numArray3 = new byte[19]
      {
        (byte) 119,
        (byte) 185,
        (byte) 70,
        (byte) 206,
        (byte) 80 /*0x50*/,
        (byte) 8,
        (byte) 114,
        (byte) 240 /*0xF0*/,
        (byte) 234,
        (byte) 0,
        (byte) 123,
        (byte) 28,
        (byte) 86,
        (byte) 97,
        (byte) 175,
        (byte) 157,
        (byte) 107,
        (byte) 142,
        (byte) 225
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      byte[] response = new byte[35];
      Array.Copy((Array) sc_19595.sspq, 0, (Array) numArray4, 0, 35);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19595.sspr, 0, (Array) numArray4, 0, 35);
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
      (byte) 191,
      (byte) 196,
      (byte) 157,
      (byte) 139,
      (byte) 149,
      (byte) 168,
      (byte) 239,
      (byte) 96 /*0x60*/,
      (byte) 242,
      (byte) 169,
      (byte) 135,
      (byte) 243,
      (byte) 133,
      (byte) 68,
      (byte) 63 /*0x3F*/,
      (byte) 49,
      (byte) 193,
      (byte) 130,
      (byte) 205
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 254,
      (byte) 206,
      (byte) 136,
      (byte) 197,
      (byte) 241,
      (byte) 39,
      (byte) 209,
      (byte) 75,
      (byte) 79,
      (byte) 234,
      (byte) 168,
      (byte) 173,
      (byte) 111,
      (byte) 195,
      (byte) 89,
      (byte) 209,
      (byte) 133,
      (byte) 129,
      (byte) 93
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
