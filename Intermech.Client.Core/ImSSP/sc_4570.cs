
// Type: ImSSP.sc_4570
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4570
{
  private static byte[] sspq = new byte[136]
  {
    (byte) 251,
    (byte) 148,
    (byte) 185,
    (byte) 32 /*0x20*/,
    (byte) 5,
    (byte) 199,
    (byte) 102,
    (byte) 240 /*0xF0*/,
    (byte) 249,
    (byte) 197,
    (byte) 2,
    (byte) 143,
    (byte) 175,
    (byte) 181,
    (byte) 67,
    (byte) 32 /*0x20*/,
    (byte) 114,
    (byte) 55,
    (byte) 165,
    (byte) 206,
    (byte) 113,
    (byte) 221,
    (byte) 25,
    (byte) 1,
    (byte) 78,
    (byte) 213,
    (byte) 105,
    (byte) 150,
    (byte) 36,
    (byte) 128 /*0x80*/,
    (byte) 8,
    (byte) 40,
    byte.MaxValue,
    (byte) 15,
    (byte) 189,
    (byte) 32 /*0x20*/,
    (byte) 141,
    (byte) 182,
    (byte) 203,
    (byte) 123,
    (byte) 80 /*0x50*/,
    (byte) 123,
    (byte) 4,
    (byte) 15,
    (byte) 128 /*0x80*/,
    (byte) 237,
    (byte) 75,
    (byte) 27,
    (byte) 69,
    (byte) 227,
    (byte) 48 /*0x30*/,
    (byte) 133,
    (byte) 53,
    (byte) 16 /*0x10*/,
    (byte) 65,
    (byte) 233,
    (byte) 141,
    (byte) 117,
    (byte) 241,
    (byte) 47,
    (byte) 163,
    (byte) 128 /*0x80*/,
    (byte) 17,
    (byte) 224 /*0xE0*/,
    (byte) 3,
    (byte) 29,
    (byte) 94,
    byte.MaxValue,
    (byte) 129,
    (byte) 146,
    (byte) 206,
    (byte) 119,
    (byte) 250,
    (byte) 124,
    (byte) 137,
    (byte) 3,
    (byte) 185,
    (byte) 126,
    (byte) 224 /*0xE0*/,
    (byte) 198,
    (byte) 1,
    (byte) 145,
    (byte) 182,
    (byte) 14,
    (byte) 254,
    (byte) 247,
    (byte) 154,
    (byte) 82,
    (byte) 221,
    (byte) 15,
    (byte) 191,
    (byte) 87,
    (byte) 111,
    (byte) 121,
    (byte) 168,
    (byte) 148,
    (byte) 157,
    (byte) 28,
    (byte) 0,
    (byte) 156,
    (byte) 17,
    (byte) 181,
    (byte) 247,
    (byte) 3,
    (byte) 249,
    (byte) 69,
    (byte) 108,
    (byte) 225,
    (byte) 204,
    (byte) 159,
    (byte) 224 /*0xE0*/,
    (byte) 176 /*0xB0*/,
    (byte) 194,
    (byte) 67,
    (byte) 75,
    (byte) 150,
    (byte) 42,
    (byte) 99,
    (byte) 187,
    (byte) 113,
    (byte) 192 /*0xC0*/,
    (byte) 168,
    (byte) 42,
    (byte) 39,
    (byte) 222,
    (byte) 157,
    (byte) 176 /*0xB0*/,
    (byte) 9,
    (byte) 229,
    (byte) 241,
    (byte) 160 /*0xA0*/,
    (byte) 19,
    (byte) 13,
    (byte) 248,
    (byte) 26,
    (byte) 120
  };
  private static byte[] sspr = new byte[136]
  {
    (byte) 188,
    (byte) 212,
    (byte) 192 /*0xC0*/,
    (byte) 226,
    (byte) 251,
    (byte) 185,
    (byte) 0,
    (byte) 158,
    (byte) 240 /*0xF0*/,
    (byte) 90,
    (byte) 15,
    (byte) 53,
    (byte) 173,
    (byte) 243,
    (byte) 138,
    (byte) 52,
    (byte) 240 /*0xF0*/,
    (byte) 158,
    (byte) 35,
    (byte) 133,
    (byte) 2,
    (byte) 173,
    (byte) 63 /*0x3F*/,
    (byte) 10,
    (byte) 146,
    (byte) 89,
    (byte) 91,
    (byte) 164,
    (byte) 194,
    (byte) 113,
    (byte) 209,
    (byte) 142,
    (byte) 161,
    (byte) 73,
    (byte) 72,
    (byte) 5,
    (byte) 56,
    (byte) 49,
    (byte) 84,
    (byte) 31 /*0x1F*/,
    (byte) 148,
    (byte) 80 /*0x50*/,
    (byte) 178,
    (byte) 126,
    (byte) 174,
    (byte) 223,
    (byte) 27,
    (byte) 209,
    (byte) 41,
    (byte) 156,
    (byte) 69,
    (byte) 25,
    (byte) 224 /*0xE0*/,
    (byte) 211,
    (byte) 163,
    (byte) 209,
    (byte) 224 /*0xE0*/,
    (byte) 36,
    (byte) 189,
    (byte) 182,
    (byte) 44,
    (byte) 238,
    (byte) 173,
    (byte) 121,
    (byte) 75,
    (byte) 110,
    (byte) 52,
    (byte) 196,
    (byte) 191,
    (byte) 24,
    (byte) 74,
    (byte) 164,
    (byte) 198,
    (byte) 212,
    (byte) 250,
    (byte) 58,
    (byte) 103,
    (byte) 221,
    (byte) 86,
    (byte) 150,
    (byte) 72,
    (byte) 221,
    (byte) 241,
    (byte) 59,
    (byte) 64 /*0x40*/,
    (byte) 74,
    (byte) 205,
    (byte) 192 /*0xC0*/,
    (byte) 107,
    (byte) 5,
    (byte) 199,
    (byte) 188,
    (byte) 136,
    (byte) 91,
    (byte) 82,
    (byte) 59,
    (byte) 37,
    (byte) 14,
    (byte) 152,
    (byte) 98,
    (byte) 196,
    (byte) 117,
    (byte) 70,
    (byte) 245,
    (byte) 38,
    (byte) 1,
    (byte) 60,
    (byte) 38,
    (byte) 100,
    (byte) 244,
    (byte) 101,
    (byte) 229,
    (byte) 128 /*0x80*/,
    (byte) 238,
    (byte) 40,
    (byte) 91,
    (byte) 176 /*0xB0*/,
    (byte) 71,
    (byte) 42,
    (byte) 159,
    (byte) 240 /*0xF0*/,
    (byte) 42,
    (byte) 181,
    (byte) 36,
    (byte) 81,
    (byte) 168,
    (byte) 159,
    (byte) 89,
    (byte) 58,
    (byte) 247,
    (byte) 64 /*0x40*/,
    (byte) 223,
    (byte) 68,
    (byte) 9,
    (byte) 2,
    (byte) 47
  };

  internal static string ssp_imclient_4571()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 49,
        (byte) 147,
        (byte) 156,
        (byte) 119
      };
      byte[] numArray3 = new byte[4]
      {
        (byte) 123,
        (byte) 138,
        (byte) 223,
        (byte) 121
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_4570.sspq, 0, (Array) numArray4, 0, 23);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4570.sspr, 0, (Array) numArray4, 0, 23);
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
    byte[] numArray5 = new byte[4];
    byte[] numArray6 = new byte[4]
    {
      (byte) 218,
      (byte) 132,
      (byte) 45,
      (byte) 36
    };
    byte[] numArray7 = new byte[4]
    {
      (byte) 118,
      (byte) 216,
      (byte) 53,
      (byte) 62
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[23];
    byte[] response1 = new byte[23];
    Array.Copy((Array) sc_4570.sspq, 23, (Array) numArray8, 0, 23);
    key.Query(true, 348, numArray8, response1);
    Array.Copy((Array) sc_4570.sspr, 23, (Array) numArray8, 0, 23);
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

  internal static string ssp_imclient_4572()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 30,
        (byte) 122,
        (byte) 223,
        (byte) 156,
        (byte) 216,
        (byte) 163,
        (byte) 201,
        byte.MaxValue,
        (byte) 124,
        (byte) 55,
        (byte) 0,
        (byte) 65,
        (byte) 217,
        (byte) 76,
        (byte) 172
      };
      byte[] numArray3 = new byte[15];
      numArray3[11] = (byte) 51;
      numArray3[7] = (byte) 33;
      numArray3[2] = (byte) 168;
      numArray3[4] = (byte) 36;
      numArray3[9] = (byte) 14;
      numArray3[5] = (byte) 188;
      numArray3[0] = (byte) 61;
      numArray3[6] = (byte) 215;
      numArray3[12] = (byte) 254;
      numArray3[8] = (byte) 227;
      numArray3[10] = (byte) 239;
      numArray3[1] = (byte) 58;
      numArray3[13] = (byte) 85;
      numArray3[3] = (byte) 76;
      numArray3[14] = (byte) 61;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_4570.sspq, 46, (Array) numArray4, 0, 49);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4570.sspr, 46, (Array) numArray4, 0, 49);
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
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15]
    {
      (byte) 245,
      (byte) 102,
      (byte) 157,
      (byte) 125,
      (byte) 67,
      (byte) 181,
      (byte) 76,
      (byte) 153,
      (byte) 178,
      (byte) 21,
      (byte) 184,
      (byte) 138,
      (byte) 184,
      (byte) 134,
      (byte) 61
    };
    byte[] numArray7 = new byte[15];
    numArray7[1] = (byte) 173;
    numArray7[4] = (byte) 141;
    numArray7[8] = (byte) 71;
    numArray7[13] = (byte) 90;
    numArray7[9] = (byte) 252;
    numArray7[12] = (byte) 94;
    numArray7[6] = (byte) 116;
    numArray7[0] = (byte) 222;
    numArray7[5] = (byte) 10;
    numArray7[3] = (byte) 17;
    numArray7[10] = (byte) 34;
    numArray7[11] = (byte) 249;
    numArray7[2] = (byte) 196;
    numArray7[7] = (byte) 188;
    numArray7[14] = (byte) 199;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[41];
    byte[] response1 = new byte[41];
    Array.Copy((Array) sc_4570.sspq, 95, (Array) numArray8, 0, 41);
    key.Query(true, 348, numArray8, response1);
    Array.Copy((Array) sc_4570.sspr, 95, (Array) numArray8, 0, 41);
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
