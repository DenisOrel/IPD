
// Type: ImSSP.sc_4289
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4289
{
  private static byte[] sspq = new byte[50]
  {
    (byte) 122,
    (byte) 70,
    (byte) 161,
    (byte) 152,
    (byte) 1,
    (byte) 94,
    (byte) 62,
    (byte) 60,
    (byte) 52,
    (byte) 172,
    (byte) 95,
    (byte) 131,
    (byte) 103,
    (byte) 223,
    (byte) 76,
    (byte) 222,
    (byte) 1,
    (byte) 234,
    (byte) 69,
    (byte) 190,
    (byte) 133,
    (byte) 82,
    (byte) 27,
    (byte) 221,
    (byte) 250,
    (byte) 52,
    (byte) 215,
    (byte) 34,
    (byte) 133,
    (byte) 49,
    (byte) 121,
    (byte) 160 /*0xA0*/,
    (byte) 209,
    (byte) 55,
    (byte) 126,
    (byte) 98,
    (byte) 33,
    (byte) 60,
    (byte) 195,
    (byte) 51,
    (byte) 193,
    (byte) 204,
    (byte) 16 /*0x10*/,
    (byte) 102,
    (byte) 39,
    (byte) 132,
    (byte) 73,
    (byte) 134,
    (byte) 127 /*0x7F*/,
    (byte) 20
  };
  private static byte[] sspr = new byte[50]
  {
    (byte) 229,
    (byte) 138,
    (byte) 150,
    (byte) 47,
    (byte) 148,
    (byte) 237,
    (byte) 150,
    (byte) 187,
    (byte) 231,
    (byte) 218,
    (byte) 141,
    (byte) 20,
    (byte) 202,
    (byte) 105,
    (byte) 48 /*0x30*/,
    (byte) 2,
    (byte) 54,
    (byte) 108,
    (byte) 88,
    (byte) 24,
    (byte) 157,
    (byte) 46,
    (byte) 99,
    (byte) 48 /*0x30*/,
    (byte) 22,
    (byte) 92,
    (byte) 129,
    (byte) 105,
    (byte) 140,
    (byte) 99,
    (byte) 112 /*0x70*/,
    (byte) 86,
    (byte) 168,
    (byte) 65,
    (byte) 69,
    (byte) 182,
    (byte) 107,
    (byte) 203,
    (byte) 8,
    (byte) 79,
    (byte) 236,
    (byte) 229,
    (byte) 141,
    (byte) 69,
    (byte) 90,
    (byte) 27,
    (byte) 125,
    (byte) 239,
    (byte) 23,
    (byte) 186
  };

  internal static string ssp_imclient_4290()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 243,
        (byte) 122,
        (byte) 122,
        (byte) 177,
        (byte) 13,
        (byte) 97,
        (byte) 222,
        (byte) 166,
        (byte) 183,
        (byte) 82,
        (byte) 9,
        (byte) 30,
        (byte) 199,
        (byte) 111,
        (byte) 171
      };
      byte[] numArray3 = new byte[15];
      numArray3[1] = (byte) 215;
      numArray3[3] = (byte) 106;
      numArray3[2] = (byte) 212;
      numArray3[0] = (byte) 127 /*0x7F*/;
      numArray3[4] = (byte) 178;
      numArray3[11] = (byte) 203;
      numArray3[10] = (byte) 196;
      numArray3[7] = (byte) 120;
      numArray3[8] = (byte) 214;
      numArray3[9] = (byte) 116;
      numArray3[14] = (byte) 160 /*0xA0*/;
      numArray3[5] = (byte) 41;
      numArray3[12] = (byte) 91;
      numArray3[13] = (byte) 111;
      numArray3[6] = (byte) 86;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 5,
      (byte) 154,
      (byte) 230,
      (byte) 156,
      (byte) 45,
      (byte) 181,
      (byte) 19,
      (byte) 0,
      (byte) 151,
      (byte) 233,
      (byte) 228,
      (byte) 215,
      (byte) 245,
      (byte) 161,
      (byte) 214
    };
    byte[] numArray6 = new byte[15];
    numArray6[8] = (byte) 231;
    numArray6[11] = (byte) 17;
    numArray6[2] = (byte) 145;
    numArray6[12] = (byte) 23;
    numArray6[3] = (byte) 211;
    numArray6[10] = (byte) 143;
    numArray6[6] = (byte) 105;
    numArray6[7] = (byte) 126;
    numArray6[1] = (byte) 52;
    numArray6[9] = (byte) 187;
    numArray6[13] = (byte) 133;
    numArray6[0] = (byte) 226;
    numArray6[5] = (byte) 220;
    numArray6[4] = (byte) 173;
    numArray6[14] = (byte) 30;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4291()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 194,
        (byte) 212,
        (byte) 196,
        (byte) 146,
        byte.MaxValue,
        (byte) 9,
        (byte) 68,
        (byte) 18,
        (byte) 134,
        (byte) 7,
        (byte) 176 /*0xB0*/,
        (byte) 101,
        (byte) 204,
        (byte) 243,
        (byte) 172
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 166,
        (byte) 125,
        (byte) 173,
        (byte) 122,
        (byte) 181,
        (byte) 101,
        (byte) 47,
        (byte) 242,
        (byte) 3,
        (byte) 134,
        (byte) 70,
        (byte) 78,
        (byte) 40,
        (byte) 39,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50];
      byte[] response = new byte[50];
      Array.Copy((Array) sc_4289.sspq, 0, (Array) numArray4, 0, 50);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4289.sspr, 0, (Array) numArray4, 0, 50);
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
    byte[] numArray6 = new byte[15];
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[13] = (byte) 2;
    numArray6[2] = (byte) 82;
    numArray6[3] = (byte) 213;
    numArray6[0] = (byte) 171;
    numArray6[5] = (byte) 39;
    numArray6[8] = (byte) 157;
    numArray6[7] = (byte) 195;
    numArray6[6] = (byte) 42;
    numArray6[9] = (byte) 204;
    numArray6[10] = (byte) 135;
    numArray6[4] = (byte) 192 /*0xC0*/;
    numArray6[11] = (byte) 27;
    numArray6[12] = (byte) 183;
    numArray6[14] = (byte) 50;
    byte[] numArray7 = new byte[15];
    numArray7[10] = (byte) 129;
    numArray7[1] = (byte) 162;
    numArray7[2] = (byte) 146;
    numArray7[3] = (byte) 214;
    numArray7[4] = (byte) 62;
    numArray7[8] = (byte) 81;
    numArray7[0] = (byte) 115;
    numArray7[5] = (byte) 217;
    numArray7[7] = (byte) 57;
    numArray7[9] = (byte) 206;
    numArray7[6] = (byte) 186;
    numArray7[11] = (byte) 180;
    numArray7[13] = (byte) 4;
    numArray7[12] = (byte) 18;
    numArray7[14] = (byte) 88;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
