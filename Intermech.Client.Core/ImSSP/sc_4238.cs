
// Type: ImSSP.sc_4238
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4238
{
  private static byte[] sspq = new byte[21]
  {
    (byte) 121,
    (byte) 169,
    (byte) 205,
    (byte) 25,
    (byte) 70,
    (byte) 29,
    (byte) 222,
    (byte) 64 /*0x40*/,
    (byte) 71,
    (byte) 234,
    (byte) 225,
    (byte) 135,
    (byte) 188,
    (byte) 107,
    (byte) 30,
    (byte) 92,
    (byte) 100,
    (byte) 0,
    (byte) 153,
    (byte) 13,
    (byte) 230
  };
  private static byte[] sspr = new byte[21]
  {
    (byte) 56,
    (byte) 185,
    (byte) 57,
    (byte) 162,
    (byte) 113,
    (byte) 65,
    (byte) 248,
    (byte) 77,
    (byte) 201,
    (byte) 96 /*0x60*/,
    (byte) 203,
    (byte) 69,
    (byte) 98,
    (byte) 202,
    (byte) 187,
    (byte) 28,
    (byte) 96 /*0x60*/,
    (byte) 144 /*0x90*/,
    (byte) 185,
    (byte) 202,
    (byte) 83
  };

  internal static string ssp_imclient_4239()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 66,
        (byte) 162,
        (byte) 55,
        (byte) 101,
        (byte) 139,
        (byte) 100,
        (byte) 217,
        (byte) 204,
        (byte) 76,
        (byte) 37,
        (byte) 216,
        (byte) 239,
        (byte) 55,
        (byte) 202,
        (byte) 218,
        (byte) 83
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[10] = (byte) 172;
      numArray3[5] = (byte) 65;
      numArray3[2] = (byte) 233;
      numArray3[0] = (byte) 108;
      numArray3[4] = (byte) 28;
      numArray3[6] = (byte) 41;
      numArray3[1] = (byte) 42;
      numArray3[7] = (byte) 18;
      numArray3[11] = (byte) 129;
      numArray3[8] = (byte) 217;
      numArray3[14] = (byte) 223;
      numArray3[3] = (byte) 19;
      numArray3[12] = (byte) 53;
      numArray3[13] = (byte) 66;
      numArray3[9] = (byte) 247;
      numArray3[15] = (byte) 146;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 101,
      (byte) 251,
      (byte) 206,
      (byte) 95,
      (byte) 137,
      (byte) 11,
      (byte) 74,
      (byte) 189,
      (byte) 34,
      (byte) 26,
      (byte) 202,
      (byte) 26,
      (byte) 157,
      (byte) 113,
      (byte) 178,
      (byte) 5
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 39,
      (byte) 13,
      (byte) 72,
      (byte) 118,
      (byte) 135,
      (byte) 223,
      (byte) 125,
      (byte) 80 /*0x50*/,
      (byte) 90,
      (byte) 68,
      (byte) 239,
      (byte) 115,
      (byte) 208 /*0xD0*/,
      (byte) 165,
      (byte) 162,
      (byte) 137
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4240()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 142,
        (byte) 3,
        (byte) 29,
        (byte) 82,
        (byte) 96 /*0x60*/,
        (byte) 71,
        (byte) 238,
        (byte) 49,
        (byte) 249,
        (byte) 44,
        (byte) 159,
        (byte) 126,
        (byte) 116,
        (byte) 48 /*0x30*/,
        (byte) 116,
        (byte) 87
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[13] = (byte) 240 /*0xF0*/;
      numArray3[1] = (byte) 192 /*0xC0*/;
      numArray3[7] = (byte) 97;
      numArray3[15] = (byte) 200;
      numArray3[11] = (byte) 176 /*0xB0*/;
      numArray3[5] = (byte) 27;
      numArray3[6] = (byte) 13;
      numArray3[3] = (byte) 94;
      numArray3[8] = (byte) 87;
      numArray3[12] = (byte) 148;
      numArray3[10] = (byte) 119;
      numArray3[14] = (byte) 181;
      numArray3[9] = (byte) 178;
      numArray3[2] = (byte) 129;
      numArray3[0] = (byte) 94;
      numArray3[4] = (byte) 187;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 98,
      (byte) 221,
      (byte) 123,
      (byte) 254,
      (byte) 65,
      (byte) 28,
      (byte) 24,
      (byte) 196,
      (byte) 246,
      (byte) 122,
      (byte) 76,
      (byte) 166,
      (byte) 109,
      (byte) 252,
      (byte) 91,
      (byte) 8
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 58,
      (byte) 195,
      (byte) 181,
      (byte) 92,
      (byte) 174,
      (byte) 165,
      (byte) 218,
      (byte) 48 /*0x30*/,
      (byte) 206,
      (byte) 97,
      (byte) 100,
      (byte) 221,
      (byte) 162,
      (byte) 144 /*0x90*/,
      (byte) 59,
      (byte) 171
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4241()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 151,
        (byte) 134,
        (byte) 30,
        (byte) 9,
        (byte) 80 /*0x50*/,
        (byte) 200,
        (byte) 115,
        (byte) 74,
        (byte) 171,
        (byte) 176 /*0xB0*/,
        (byte) 93,
        (byte) 109,
        (byte) 101,
        (byte) 37,
        (byte) 17,
        (byte) 122
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 127 /*0x7F*/,
        (byte) 138,
        (byte) 1,
        (byte) 121,
        (byte) 231,
        (byte) 231,
        (byte) 175,
        (byte) 205,
        (byte) 53,
        (byte) 165,
        (byte) 23,
        (byte) 183,
        (byte) 244,
        (byte) 64 /*0x40*/,
        (byte) 180,
        byte.MaxValue
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 18,
      (byte) 31 /*0x1F*/,
      (byte) 50,
      (byte) 134,
      (byte) 132,
      (byte) 252,
      (byte) 105,
      (byte) 17,
      (byte) 125,
      (byte) 140,
      (byte) 69,
      (byte) 94,
      (byte) 177,
      (byte) 223,
      (byte) 145,
      (byte) 115
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 52,
      (byte) 81,
      (byte) 189,
      (byte) 138,
      (byte) 33,
      (byte) 105,
      (byte) 25,
      (byte) 141,
      (byte) 130,
      (byte) 24,
      (byte) 156,
      (byte) 117,
      (byte) 86,
      (byte) 187,
      (byte) 231,
      (byte) 195
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[21];
    byte[] response = new byte[21];
    Array.Copy((Array) sc_4238.sspq, 0, (Array) numArray7, 0, 21);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4238.sspr, 0, (Array) numArray7, 0, 21);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }
}
