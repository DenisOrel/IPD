// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7205
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7205
{
  private static byte[] sspq = new byte[43]
  {
    (byte) 250,
    (byte) 150,
    (byte) 242,
    (byte) 214,
    (byte) 111,
    (byte) 48 /*0x30*/,
    (byte) 119,
    (byte) 91,
    (byte) 189,
    (byte) 85,
    (byte) 224 /*0xE0*/,
    (byte) 253,
    (byte) 179,
    (byte) 166,
    (byte) 81,
    (byte) 253,
    (byte) 187,
    (byte) 72,
    (byte) 197,
    (byte) 211,
    (byte) 18,
    (byte) 180,
    (byte) 167,
    (byte) 6,
    (byte) 159,
    (byte) 67,
    (byte) 162,
    (byte) 230,
    (byte) 170,
    (byte) 43,
    (byte) 254,
    (byte) 62,
    (byte) 194,
    (byte) 53,
    (byte) 254,
    (byte) 160 /*0xA0*/,
    (byte) 57,
    (byte) 238,
    (byte) 247,
    (byte) 116,
    (byte) 52,
    (byte) 218,
    (byte) 225
  };
  private static byte[] sspr = new byte[43]
  {
    (byte) 111,
    (byte) 47,
    (byte) 249,
    (byte) 90,
    (byte) 226,
    (byte) 82,
    (byte) 214,
    (byte) 72,
    (byte) 60,
    (byte) 236,
    (byte) 198,
    (byte) 40,
    (byte) 127 /*0x7F*/,
    (byte) 150,
    (byte) 235,
    (byte) 77,
    (byte) 179,
    (byte) 81,
    (byte) 41,
    (byte) 45,
    (byte) 16 /*0x10*/,
    (byte) 160 /*0xA0*/,
    (byte) 32 /*0x20*/,
    (byte) 208 /*0xD0*/,
    (byte) 93,
    (byte) 73,
    (byte) 9,
    (byte) 177,
    (byte) 226,
    byte.MaxValue,
    (byte) 79,
    (byte) 89,
    (byte) 77,
    (byte) 234,
    (byte) 187,
    (byte) 169,
    (byte) 243,
    (byte) 8,
    (byte) 18,
    (byte) 117,
    (byte) 127 /*0x7F*/,
    (byte) 89,
    byte.MaxValue
  };

  internal static string ssp_imclient_7206()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 126,
        (byte) 72,
        (byte) 82,
        (byte) 106,
        (byte) 69,
        (byte) 207,
        (byte) 84,
        (byte) 140,
        (byte) 31 /*0x1F*/,
        (byte) 211,
        (byte) 251,
        (byte) 252,
        (byte) 25,
        (byte) 150,
        (byte) 122,
        (byte) 249
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[10] = (byte) 62;
      numArray3[1] = (byte) 159;
      numArray3[13] = (byte) 56;
      numArray3[3] = (byte) 49;
      numArray3[15] = (byte) 252;
      numArray3[5] = (byte) 56;
      numArray3[4] = (byte) 64 /*0x40*/;
      numArray3[14] = (byte) 178;
      numArray3[8] = (byte) 249;
      numArray3[9] = (byte) 11;
      numArray3[6] = (byte) 61;
      numArray3[11] = (byte) 160 /*0xA0*/;
      numArray3[12] = (byte) 254;
      numArray3[0] = (byte) 24;
      numArray3[7] = (byte) 189;
      numArray3[2] = (byte) 37;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_7205.sspq, 0, (Array) numArray4, 0, 43);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7205.sspr, 0, (Array) numArray4, 0, 43);
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
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[13] = (byte) 239;
    numArray6[7] = (byte) 20;
    numArray6[2] = (byte) 50;
    numArray6[10] = (byte) 0;
    numArray6[4] = (byte) 67;
    numArray6[5] = (byte) 231;
    numArray6[6] = (byte) 43;
    numArray6[14] = (byte) 236;
    numArray6[8] = (byte) 180;
    numArray6[1] = (byte) 94;
    numArray6[0] = (byte) 108;
    numArray6[11] = (byte) 50;
    numArray6[15] = (byte) 54;
    numArray6[12] = (byte) 70;
    numArray6[9] = (byte) 21;
    numArray6[3] = (byte) 93;
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 24,
      (byte) 50,
      (byte) 62,
      (byte) 191,
      (byte) 198,
      (byte) 106,
      (byte) 228,
      (byte) 250,
      (byte) 181,
      (byte) 191,
      (byte) 203,
      byte.MaxValue,
      (byte) 77,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 199
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
