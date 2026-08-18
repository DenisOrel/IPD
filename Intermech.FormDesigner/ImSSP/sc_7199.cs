// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7199
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7199
{
  private static byte[] sspq = new byte[11]
  {
    (byte) 99,
    (byte) 6,
    (byte) 135,
    (byte) 49,
    (byte) 115,
    (byte) 252,
    (byte) 68,
    (byte) 62,
    (byte) 183,
    (byte) 27,
    (byte) 161
  };
  private static byte[] sspr = new byte[11]
  {
    (byte) 88,
    (byte) 82,
    (byte) 198,
    (byte) 48 /*0x30*/,
    (byte) 108,
    (byte) 117,
    (byte) 132,
    (byte) 48 /*0x30*/,
    (byte) 209,
    (byte) 239,
    (byte) 122
  };

  internal static string ssp_imclient_7200()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 91,
        (byte) 140,
        (byte) 44,
        (byte) 204,
        (byte) 120,
        (byte) 121,
        (byte) 36,
        (byte) 120,
        (byte) 169,
        (byte) 140,
        (byte) 94,
        (byte) 7,
        (byte) 121,
        (byte) 105,
        (byte) 4,
        (byte) 142
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 33,
        (byte) 74,
        (byte) 152,
        (byte) 50,
        (byte) 121,
        (byte) 113,
        (byte) 232,
        (byte) 155,
        (byte) 104,
        (byte) 161,
        (byte) 156,
        (byte) 19,
        (byte) 59,
        (byte) 142,
        (byte) 222,
        (byte) 79
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_7199.sspq, 0, (Array) numArray4, 0, 11);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7199.sspr, 0, (Array) numArray4, 0, 11);
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
      (byte) 131,
      (byte) 56,
      (byte) 185,
      (byte) 186,
      (byte) 91,
      (byte) 15,
      (byte) 147,
      (byte) 63 /*0x3F*/,
      (byte) 116,
      (byte) 112 /*0x70*/,
      (byte) 130,
      (byte) 224 /*0xE0*/,
      (byte) 59,
      (byte) 136,
      (byte) 20,
      (byte) 19
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 226,
      (byte) 60,
      (byte) 189,
      (byte) 235,
      (byte) 159,
      (byte) 211,
      (byte) 132,
      (byte) 9,
      (byte) 149,
      (byte) 235,
      (byte) 156,
      (byte) 153,
      (byte) 193,
      (byte) 246,
      (byte) 100,
      (byte) 31 /*0x1F*/
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
