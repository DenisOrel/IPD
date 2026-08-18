// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7151
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7151
{
  private static byte[] sspq = new byte[48 /*0x30*/]
  {
    (byte) 138,
    (byte) 31 /*0x1F*/,
    (byte) 66,
    (byte) 56,
    (byte) 18,
    (byte) 155,
    (byte) 117,
    (byte) 62,
    (byte) 94,
    (byte) 76,
    (byte) 228,
    (byte) 48 /*0x30*/,
    (byte) 242,
    (byte) 80 /*0x50*/,
    (byte) 251,
    (byte) 61,
    (byte) 205,
    (byte) 70,
    (byte) 167,
    (byte) 224 /*0xE0*/,
    (byte) 183,
    (byte) 57,
    (byte) 148,
    (byte) 228,
    (byte) 227,
    (byte) 173,
    (byte) 238,
    (byte) 132,
    (byte) 28,
    (byte) 124,
    (byte) 8,
    (byte) 176 /*0xB0*/,
    (byte) 247,
    (byte) 244,
    (byte) 35,
    (byte) 94,
    (byte) 105,
    (byte) 66,
    (byte) 153,
    (byte) 252,
    (byte) 200,
    (byte) 198,
    (byte) 135,
    (byte) 185,
    (byte) 11,
    (byte) 140,
    (byte) 96 /*0x60*/,
    (byte) 6
  };
  private static byte[] sspr = new byte[48 /*0x30*/]
  {
    (byte) 147,
    (byte) 110,
    (byte) 19,
    (byte) 124,
    (byte) 214,
    (byte) 232,
    (byte) 220,
    (byte) 138,
    (byte) 162,
    (byte) 35,
    (byte) 24,
    (byte) 118,
    (byte) 152,
    (byte) 246,
    (byte) 36,
    (byte) 189,
    (byte) 155,
    (byte) 6,
    (byte) 30,
    (byte) 158,
    (byte) 48 /*0x30*/,
    (byte) 20,
    (byte) 52,
    (byte) 200,
    (byte) 247,
    (byte) 83,
    (byte) 27,
    (byte) 245,
    (byte) 51,
    (byte) 226,
    (byte) 123,
    (byte) 130,
    (byte) 108,
    (byte) 95,
    (byte) 213,
    (byte) 86,
    (byte) 194,
    (byte) 201,
    (byte) 225,
    (byte) 152,
    (byte) 113,
    (byte) 253,
    (byte) 199,
    (byte) 208 /*0xD0*/,
    (byte) 218,
    (byte) 36,
    (byte) 144 /*0x90*/,
    (byte) 182
  };

  internal static string ssp_imclient_7152()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 31 /*0x1F*/,
        (byte) 188,
        (byte) 31 /*0x1F*/,
        (byte) 154,
        (byte) 212,
        (byte) 159,
        (byte) 0,
        (byte) 68,
        (byte) 108,
        (byte) 250,
        (byte) 169,
        (byte) 21,
        (byte) 113,
        (byte) 237,
        (byte) 222,
        (byte) 174
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[1] = (byte) 76;
      numArray3[5] = (byte) 134;
      numArray3[2] = (byte) 222;
      numArray3[10] = (byte) 188;
      numArray3[4] = (byte) 177;
      numArray3[3] = (byte) 246;
      numArray3[6] = (byte) 137;
      numArray3[7] = (byte) 32 /*0x20*/;
      numArray3[8] = (byte) 236;
      numArray3[14] = (byte) 110;
      numArray3[0] = (byte) 212;
      numArray3[11] = (byte) 166;
      numArray3[9] = (byte) 2;
      numArray3[13] = (byte) 220;
      numArray3[12] = (byte) 49;
      numArray3[15] = (byte) 228;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_7151.sspq, 0, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7151.sspr, 0, (Array) numArray4, 0, 48 /*0x30*/);
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
      (byte) 235,
      (byte) 160 /*0xA0*/,
      (byte) 95,
      (byte) 198,
      (byte) 25,
      (byte) 22,
      (byte) 50,
      (byte) 80 /*0x50*/,
      (byte) 139,
      (byte) 93,
      (byte) 84,
      (byte) 100,
      (byte) 172,
      (byte) 240 /*0xF0*/,
      (byte) 33,
      (byte) 226
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 239,
      (byte) 180,
      (byte) 232,
      (byte) 184,
      (byte) 2,
      (byte) 228,
      (byte) 153,
      (byte) 166,
      (byte) 15,
      (byte) 24,
      (byte) 206,
      (byte) 51,
      (byte) 245,
      (byte) 87,
      (byte) 140,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
