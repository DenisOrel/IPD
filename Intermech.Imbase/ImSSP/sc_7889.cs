// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7889
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7889
{
  private static byte[] sspq = new byte[65]
  {
    (byte) 81,
    (byte) 230,
    (byte) 238,
    (byte) 81,
    (byte) 17,
    (byte) 180,
    (byte) 77,
    (byte) 142,
    (byte) 42,
    (byte) 167,
    (byte) 4,
    (byte) 71,
    (byte) 20,
    (byte) 143,
    (byte) 254,
    (byte) 122,
    (byte) 142,
    (byte) 79,
    (byte) 187,
    (byte) 113,
    (byte) 124,
    (byte) 113,
    (byte) 189,
    (byte) 78,
    (byte) 187,
    (byte) 234,
    (byte) 97,
    (byte) 53,
    (byte) 187,
    (byte) 22,
    (byte) 168,
    (byte) 57,
    (byte) 78,
    (byte) 211,
    (byte) 116,
    (byte) 161,
    (byte) 193,
    (byte) 127 /*0x7F*/,
    (byte) 125,
    (byte) 228,
    (byte) 250,
    (byte) 183,
    (byte) 127 /*0x7F*/,
    (byte) 197,
    (byte) 226,
    (byte) 19,
    (byte) 215,
    (byte) 160 /*0xA0*/,
    (byte) 187,
    (byte) 45,
    (byte) 155,
    (byte) 81,
    (byte) 97,
    (byte) 7,
    (byte) 246,
    (byte) 38,
    byte.MaxValue,
    (byte) 13,
    (byte) 42,
    (byte) 139,
    (byte) 13,
    (byte) 136,
    (byte) 229,
    (byte) 66,
    (byte) 128 /*0x80*/
  };
  private static byte[] sspr = new byte[65]
  {
    (byte) 228,
    (byte) 163,
    (byte) 86,
    (byte) 174,
    (byte) 195,
    (byte) 130,
    (byte) 136,
    (byte) 86,
    (byte) 123,
    (byte) 72,
    (byte) 157,
    (byte) 94,
    (byte) 214,
    (byte) 41,
    (byte) 232,
    (byte) 123,
    (byte) 88,
    (byte) 101,
    (byte) 181,
    (byte) 186,
    (byte) 60,
    (byte) 182,
    (byte) 127 /*0x7F*/,
    (byte) 182,
    (byte) 48 /*0x30*/,
    (byte) 117,
    (byte) 205,
    (byte) 245,
    (byte) 108,
    (byte) 138,
    (byte) 210,
    (byte) 80 /*0x50*/,
    (byte) 165,
    (byte) 124,
    (byte) 89,
    (byte) 218,
    (byte) 187,
    (byte) 54,
    (byte) 162,
    (byte) 110,
    (byte) 16 /*0x10*/,
    (byte) 16 /*0x10*/,
    (byte) 239,
    (byte) 31 /*0x1F*/,
    (byte) 90,
    (byte) 49,
    (byte) 244,
    (byte) 167,
    (byte) 175,
    (byte) 164,
    (byte) 149,
    (byte) 116,
    (byte) 176 /*0xB0*/,
    (byte) 120,
    (byte) 72,
    (byte) 129,
    (byte) 132,
    (byte) 123,
    (byte) 238,
    (byte) 55,
    (byte) 52,
    (byte) 180,
    (byte) 152,
    (byte) 205,
    (byte) 141
  };

  internal static string ssp_imbase_7890()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[29];
      byte[] numArray2 = new byte[29]
      {
        (byte) 37,
        (byte) 157,
        (byte) 119,
        (byte) 35,
        (byte) 8,
        (byte) 73,
        (byte) 68,
        (byte) 166,
        (byte) 174,
        (byte) 141,
        (byte) 162,
        (byte) 240 /*0xF0*/,
        (byte) 197,
        (byte) 136,
        (byte) 218,
        (byte) 163,
        (byte) 141,
        (byte) 234,
        (byte) 179,
        (byte) 12,
        (byte) 119,
        (byte) 104,
        (byte) 78,
        (byte) 188,
        (byte) 57,
        (byte) 166,
        (byte) 19,
        (byte) 136,
        (byte) 144 /*0x90*/
      };
      byte[] numArray3 = new byte[29]
      {
        (byte) 210,
        (byte) 48 /*0x30*/,
        (byte) 201,
        (byte) 184,
        (byte) 79,
        (byte) 226,
        (byte) 195,
        (byte) 29,
        (byte) 111,
        (byte) 98,
        (byte) 123,
        (byte) 188,
        (byte) 31 /*0x1F*/,
        (byte) 156,
        (byte) 3,
        (byte) 64 /*0x40*/,
        (byte) 215,
        (byte) 204,
        (byte) 54,
        (byte) 83,
        (byte) 89,
        (byte) 55,
        (byte) 62,
        (byte) 223,
        (byte) 146,
        (byte) 97,
        (byte) 234,
        (byte) 228,
        (byte) 7
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_7889.sspq, 0, (Array) numArray4, 0, 36);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7889.sspr, 0, (Array) numArray4, 0, 36);
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
    byte[] numArray5 = new byte[29];
    byte[] numArray6 = new byte[29];
    numArray6[1] = (byte) 82;
    numArray6[0] = (byte) 236;
    numArray6[12] = (byte) 20;
    numArray6[3] = (byte) 53;
    numArray6[4] = (byte) 102;
    numArray6[5] = (byte) 125;
    numArray6[6] = (byte) 76;
    numArray6[7] = (byte) 193;
    numArray6[11] = (byte) 22;
    numArray6[16 /*0x10*/] = (byte) 153;
    numArray6[15] = (byte) 182;
    numArray6[20] = (byte) 41;
    numArray6[17] = (byte) 226;
    numArray6[13] = (byte) 14;
    numArray6[14] = (byte) 1;
    numArray6[24] = (byte) 83;
    numArray6[10] = (byte) 37;
    numArray6[9] = (byte) 117;
    numArray6[18] = (byte) 159;
    numArray6[19] = (byte) 224 /*0xE0*/;
    numArray6[27] = (byte) 20;
    numArray6[23] = (byte) 39;
    numArray6[21] = (byte) 159;
    numArray6[8] = (byte) 74;
    numArray6[26] = (byte) 211;
    numArray6[25] = (byte) 23;
    numArray6[2] = (byte) 123;
    numArray6[22] = (byte) 150;
    numArray6[28] = byte.MaxValue;
    byte[] numArray7 = new byte[29]
    {
      (byte) 109,
      (byte) 78,
      (byte) 173,
      (byte) 211,
      (byte) 194,
      (byte) 131,
      (byte) 141,
      (byte) 88,
      (byte) 165,
      (byte) 155,
      (byte) 118,
      (byte) 95,
      (byte) 200,
      (byte) 141,
      (byte) 135,
      (byte) 122,
      (byte) 19,
      (byte) 31 /*0x1F*/,
      (byte) 30,
      (byte) 194,
      (byte) 56,
      (byte) 193,
      (byte) 99,
      (byte) 4,
      (byte) 247,
      (byte) 94,
      (byte) 141,
      (byte) 221,
      (byte) 108
    };
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 29);
    for (int index = 0; index < 29; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[29];
    byte[] response1 = new byte[29];
    Array.Copy((Array) sc_7889.sspq, 36, (Array) numArray8, 0, 29);
    key.Query(true, 343, numArray8, response1);
    Array.Copy((Array) sc_7889.sspr, 36, (Array) numArray8, 0, 29);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }
}
