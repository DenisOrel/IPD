// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7218
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7218
{
  private static byte[] sspq = new byte[44]
  {
    (byte) 38,
    (byte) 70,
    (byte) 156,
    (byte) 169,
    (byte) 156,
    (byte) 187,
    (byte) 133,
    (byte) 38,
    (byte) 73,
    (byte) 215,
    (byte) 152,
    (byte) 183,
    (byte) 53,
    (byte) 63 /*0x3F*/,
    (byte) 200,
    (byte) 239,
    (byte) 52,
    (byte) 215,
    (byte) 66,
    (byte) 130,
    (byte) 252,
    (byte) 172,
    (byte) 159,
    (byte) 11,
    (byte) 63 /*0x3F*/,
    (byte) 155,
    (byte) 201,
    (byte) 70,
    (byte) 19,
    (byte) 97,
    (byte) 147,
    (byte) 76,
    (byte) 113,
    (byte) 127 /*0x7F*/,
    (byte) 77,
    (byte) 75,
    (byte) 18,
    (byte) 113,
    (byte) 194,
    (byte) 241,
    (byte) 8,
    byte.MaxValue,
    (byte) 20,
    (byte) 168
  };
  private static byte[] sspr = new byte[44]
  {
    (byte) 56,
    (byte) 127 /*0x7F*/,
    (byte) 253,
    (byte) 84,
    (byte) 23,
    (byte) 234,
    (byte) 124,
    (byte) 56,
    (byte) 80 /*0x50*/,
    (byte) 213,
    (byte) 123,
    (byte) 101,
    (byte) 25,
    (byte) 82,
    (byte) 63 /*0x3F*/,
    (byte) 195,
    (byte) 120,
    (byte) 168,
    (byte) 28,
    (byte) 7,
    (byte) 251,
    (byte) 140,
    (byte) 73,
    (byte) 136,
    (byte) 132,
    (byte) 115,
    (byte) 153,
    (byte) 69,
    (byte) 155,
    (byte) 61,
    (byte) 169,
    (byte) 136,
    (byte) 58,
    (byte) 138,
    (byte) 66,
    (byte) 160 /*0xA0*/,
    (byte) 188,
    (byte) 226,
    (byte) 85,
    (byte) 58,
    (byte) 73,
    (byte) 118,
    (byte) 174,
    (byte) 2
  };

  internal static string ssp_imclient_7219()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 146,
        (byte) 175,
        (byte) 27,
        (byte) 95,
        (byte) 172,
        (byte) 141,
        (byte) 37,
        (byte) 93,
        (byte) 127 /*0x7F*/,
        (byte) 73,
        (byte) 22,
        (byte) 250,
        (byte) 248,
        (byte) 66,
        (byte) 166,
        byte.MaxValue
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 59,
        (byte) 23,
        (byte) 141,
        (byte) 28,
        (byte) 217,
        (byte) 85,
        (byte) 7,
        (byte) 157,
        (byte) 229,
        (byte) 85,
        (byte) 250,
        (byte) 61,
        (byte) 141,
        (byte) 60,
        (byte) 2,
        (byte) 133
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[44];
      byte[] response = new byte[44];
      Array.Copy((Array) sc_7218.sspq, 0, (Array) numArray4, 0, 44);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7218.sspr, 0, (Array) numArray4, 0, 44);
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
      (byte) 176 /*0xB0*/,
      (byte) 95,
      (byte) 197,
      (byte) 184,
      (byte) 121,
      (byte) 182,
      (byte) 203,
      (byte) 56,
      (byte) 172,
      (byte) 148,
      (byte) 193,
      (byte) 121,
      (byte) 167,
      (byte) 144 /*0x90*/,
      (byte) 106,
      (byte) 1
    };
    byte[] numArray7 = new byte[16 /*0x10*/];
    numArray7[5] = (byte) 210;
    numArray7[1] = (byte) 211;
    numArray7[9] = (byte) 172;
    numArray7[3] = (byte) 18;
    numArray7[4] = (byte) 51;
    numArray7[7] = (byte) 244;
    numArray7[6] = (byte) 101;
    numArray7[13] = (byte) 103;
    numArray7[12] = (byte) 7;
    numArray7[8] = (byte) 138;
    numArray7[10] = (byte) 170;
    numArray7[11] = (byte) 98;
    numArray7[2] = (byte) 164;
    numArray7[0] = (byte) 126;
    numArray7[15] = (byte) 41;
    numArray7[14] = (byte) 221;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
