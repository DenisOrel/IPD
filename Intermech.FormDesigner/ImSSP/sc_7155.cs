// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7155
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7155
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 4,
    (byte) 55,
    (byte) 161,
    (byte) 18,
    (byte) 189,
    (byte) 246,
    (byte) 145,
    (byte) 74,
    (byte) 131,
    (byte) 249,
    (byte) 45,
    (byte) 181,
    (byte) 14,
    (byte) 246,
    (byte) 126,
    (byte) 217,
    (byte) 118,
    (byte) 117,
    (byte) 188,
    (byte) 36,
    (byte) 166,
    (byte) 66,
    (byte) 173,
    (byte) 90,
    (byte) 209,
    (byte) 193,
    (byte) 76,
    (byte) 46,
    (byte) 242,
    (byte) 199,
    (byte) 9,
    (byte) 195,
    (byte) 143,
    (byte) 196,
    (byte) 171
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 52,
    (byte) 217,
    (byte) 52,
    (byte) 115,
    (byte) 86,
    (byte) 59,
    (byte) 228,
    (byte) 194,
    (byte) 218,
    (byte) 200,
    (byte) 23,
    (byte) 204,
    (byte) 61,
    (byte) 207,
    (byte) 199,
    (byte) 97,
    (byte) 21,
    (byte) 218,
    (byte) 68,
    (byte) 83,
    (byte) 199,
    (byte) 92,
    (byte) 62,
    (byte) 71,
    (byte) 63 /*0x3F*/,
    (byte) 133,
    (byte) 143,
    (byte) 50,
    (byte) 190,
    (byte) 62,
    (byte) 111,
    (byte) 193,
    (byte) 252,
    (byte) 35,
    (byte) 88
  };

  internal static string ssp_imclient_7156()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 89,
        (byte) 201,
        (byte) 200,
        (byte) 157,
        (byte) 119,
        (byte) 91,
        (byte) 64 /*0x40*/,
        (byte) 75,
        (byte) 13,
        (byte) 101,
        (byte) 1,
        (byte) 4,
        (byte) 233,
        (byte) 14,
        (byte) 73,
        (byte) 175
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[2] = (byte) 227;
      numArray3[1] = (byte) 33;
      numArray3[11] = (byte) 178;
      numArray3[3] = (byte) 73;
      numArray3[5] = (byte) 116;
      numArray3[0] = (byte) 195;
      numArray3[6] = (byte) 45;
      numArray3[4] = (byte) 31 /*0x1F*/;
      numArray3[9] = (byte) 22;
      numArray3[7] = (byte) 120;
      numArray3[10] = (byte) 180;
      numArray3[12] = (byte) 208 /*0xD0*/;
      numArray3[13] = (byte) 109;
      numArray3[15] = (byte) 78;
      numArray3[14] = (byte) 238;
      numArray3[8] = (byte) 150;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      byte[] response = new byte[35];
      Array.Copy((Array) sc_7155.sspq, 0, (Array) numArray4, 0, 35);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_7155.sspr, 0, (Array) numArray4, 0, 35);
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
    numArray6[10] = (byte) 212;
    numArray6[12] = (byte) 8;
    numArray6[1] = (byte) 135;
    numArray6[13] = (byte) 96 /*0x60*/;
    numArray6[4] = (byte) 137;
    numArray6[5] = (byte) 163;
    numArray6[6] = (byte) 216;
    numArray6[3] = (byte) 37;
    numArray6[8] = (byte) 247;
    numArray6[0] = (byte) 16 /*0x10*/;
    numArray6[11] = (byte) 113;
    numArray6[14] = (byte) 154;
    numArray6[9] = (byte) 239;
    numArray6[2] = (byte) 20;
    numArray6[7] = (byte) 2;
    numArray6[15] = (byte) 194;
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 87,
      (byte) 171,
      (byte) 75,
      (byte) 230,
      (byte) 148,
      (byte) 223,
      (byte) 159,
      (byte) 31 /*0x1F*/,
      (byte) 142,
      (byte) 206,
      (byte) 165,
      (byte) 65,
      (byte) 159,
      (byte) 48 /*0x30*/,
      (byte) 215,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
