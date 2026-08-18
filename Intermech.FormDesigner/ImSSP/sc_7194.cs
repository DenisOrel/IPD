// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7194
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7194
{
  private static byte[] sspq = new byte[18]
  {
    (byte) 44,
    (byte) 135,
    (byte) 162,
    (byte) 37,
    (byte) 114,
    (byte) 130,
    (byte) 219,
    (byte) 179,
    (byte) 120,
    (byte) 130,
    (byte) 167,
    (byte) 220,
    (byte) 131,
    (byte) 175,
    (byte) 73,
    (byte) 46,
    (byte) 68,
    (byte) 73
  };
  private static byte[] sspr = new byte[18]
  {
    (byte) 191,
    (byte) 214,
    (byte) 175,
    (byte) 211,
    (byte) 99,
    (byte) 136,
    (byte) 122,
    (byte) 2,
    (byte) 6,
    (byte) 205,
    (byte) 177,
    (byte) 55,
    (byte) 221,
    (byte) 110,
    (byte) 162,
    (byte) 142,
    (byte) 237,
    (byte) 171
  };

  internal static string ssp_imclient_7195()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 175,
        (byte) 122,
        (byte) 201,
        (byte) 240 /*0xF0*/,
        (byte) 165,
        (byte) 155,
        (byte) 183,
        (byte) 187,
        (byte) 191,
        (byte) 142,
        (byte) 25,
        (byte) 224 /*0xE0*/,
        (byte) 147,
        (byte) 162,
        (byte) 156,
        (byte) 239
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[10] = (byte) 36;
      numArray3[1] = (byte) 27;
      numArray3[0] = (byte) 214;
      numArray3[3] = (byte) 23;
      numArray3[13] = (byte) 234;
      numArray3[5] = (byte) 77;
      numArray3[11] = (byte) 90;
      numArray3[9] = (byte) 45;
      numArray3[7] = (byte) 226;
      numArray3[6] = (byte) 36;
      numArray3[8] = (byte) 111;
      numArray3[4] = (byte) 17;
      numArray3[12] = (byte) 92;
      numArray3[2] = (byte) 84;
      numArray3[14] = (byte) 208 /*0xD0*/;
      numArray3[15] = (byte) 98;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18];
      byte[] response = new byte[18];
      Array.Copy((Array) sc_7194.sspq, 0, (Array) numArray4, 0, 18);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7194.sspr, 0, (Array) numArray4, 0, 18);
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
      (byte) 15,
      (byte) 153,
      (byte) 172,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 70,
      (byte) 35,
      (byte) 15,
      (byte) 35,
      (byte) 157,
      (byte) 35,
      (byte) 64 /*0x40*/,
      (byte) 62,
      (byte) 11,
      (byte) 206,
      (byte) 3
    };
    byte[] numArray7 = new byte[16 /*0x10*/];
    numArray7[4] = (byte) 177;
    numArray7[1] = (byte) 213;
    numArray7[14] = (byte) 36;
    numArray7[7] = (byte) 69;
    numArray7[0] = (byte) 114;
    numArray7[5] = (byte) 200;
    numArray7[6] = (byte) 137;
    numArray7[11] = (byte) 216;
    numArray7[8] = (byte) 96 /*0x60*/;
    numArray7[9] = (byte) 124;
    numArray7[10] = (byte) 214;
    numArray7[13] = (byte) 237;
    numArray7[3] = (byte) 94;
    numArray7[12] = (byte) 45;
    numArray7[15] = (byte) 53;
    numArray7[2] = (byte) 183;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
