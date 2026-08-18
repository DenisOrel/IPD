// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7755
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7755
{
  private static byte[] sspq = new byte[31 /*0x1F*/]
  {
    (byte) 83,
    (byte) 210,
    (byte) 153,
    (byte) 24,
    (byte) 40,
    (byte) 35,
    (byte) 11,
    (byte) 46,
    (byte) 137,
    (byte) 228,
    (byte) 6,
    (byte) 248,
    (byte) 1,
    (byte) 196,
    (byte) 68,
    (byte) 152,
    (byte) 43,
    (byte) 208 /*0xD0*/,
    (byte) 170,
    (byte) 108,
    (byte) 53,
    (byte) 148,
    (byte) 175,
    (byte) 221,
    (byte) 74,
    (byte) 130,
    (byte) 86,
    (byte) 109,
    (byte) 84,
    (byte) 156,
    (byte) 48 /*0x30*/
  };
  private static byte[] sspr = new byte[31 /*0x1F*/]
  {
    (byte) 211,
    (byte) 23,
    (byte) 166,
    (byte) 41,
    (byte) 70,
    (byte) 181,
    (byte) 117,
    (byte) 49,
    (byte) 71,
    (byte) 253,
    (byte) 48 /*0x30*/,
    (byte) 200,
    (byte) 214,
    (byte) 26,
    (byte) 33,
    (byte) 173,
    (byte) 252,
    (byte) 154,
    (byte) 59,
    (byte) 245,
    (byte) 166,
    (byte) 190,
    (byte) 52,
    (byte) 130,
    (byte) 251,
    (byte) 39,
    (byte) 97,
    (byte) 90,
    (byte) 188,
    (byte) 205,
    (byte) 28
  };

  internal static string ssp_imbase_7756()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 229,
        (byte) 224 /*0xE0*/,
        (byte) 181,
        (byte) 169,
        (byte) 217,
        (byte) 200,
        (byte) 188,
        (byte) 214,
        (byte) 217,
        (byte) 120,
        (byte) 52,
        (byte) 221,
        (byte) 168,
        (byte) 118,
        (byte) 170,
        (byte) 248
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[7] = (byte) 57;
      numArray3[10] = (byte) 179;
      numArray3[0] = (byte) 252;
      numArray3[15] = (byte) 134;
      numArray3[4] = (byte) 163;
      numArray3[5] = (byte) 219;
      numArray3[6] = (byte) 239;
      numArray3[2] = (byte) 98;
      numArray3[8] = (byte) 6;
      numArray3[9] = (byte) 195;
      numArray3[1] = (byte) 130;
      numArray3[11] = (byte) 246;
      numArray3[13] = (byte) 215;
      numArray3[3] = (byte) 108;
      numArray3[12] = (byte) 0;
      numArray3[14] = (byte) 114;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/];
      byte[] response = new byte[31 /*0x1F*/];
      Array.Copy((Array) sc_7755.sspq, 0, (Array) numArray4, 0, 31 /*0x1F*/);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7755.sspr, 0, (Array) numArray4, 0, 31 /*0x1F*/);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 77,
      (byte) 148,
      (byte) 234,
      (byte) 165,
      (byte) 70,
      (byte) 125,
      (byte) 16 /*0x10*/,
      (byte) 206,
      (byte) 176 /*0xB0*/,
      (byte) 109,
      (byte) 124,
      (byte) 152,
      (byte) 21,
      (byte) 244,
      (byte) 133,
      (byte) 56
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 26,
      (byte) 46,
      (byte) 144 /*0x90*/,
      (byte) 116,
      (byte) 121,
      (byte) 99,
      (byte) 0,
      (byte) 13,
      (byte) 181,
      (byte) 52,
      (byte) 228,
      (byte) 213,
      (byte) 138,
      (byte) 41,
      (byte) 21,
      (byte) 1
    };
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
