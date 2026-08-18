
// Type: ImSSP.sc_4620
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4620
{
  private static byte[] sspq = new byte[26]
  {
    (byte) 124,
    (byte) 118,
    (byte) 13,
    (byte) 233,
    (byte) 33,
    (byte) 36,
    (byte) 241,
    (byte) 72,
    (byte) 236,
    (byte) 228,
    (byte) 10,
    (byte) 156,
    (byte) 212,
    (byte) 115,
    (byte) 183,
    (byte) 0,
    (byte) 147,
    (byte) 24,
    (byte) 209,
    (byte) 49,
    (byte) 57,
    (byte) 90,
    (byte) 11,
    (byte) 111,
    (byte) 222,
    (byte) 218
  };
  private static byte[] sspr = new byte[26]
  {
    (byte) 21,
    (byte) 108,
    (byte) 204,
    (byte) 105,
    (byte) 209,
    (byte) 199,
    (byte) 150,
    (byte) 169,
    (byte) 68,
    (byte) 101,
    (byte) 89,
    (byte) 212,
    (byte) 3,
    (byte) 239,
    (byte) 19,
    (byte) 225,
    (byte) 18,
    (byte) 113,
    (byte) 70,
    (byte) 33,
    (byte) 112 /*0x70*/,
    (byte) 30,
    (byte) 15,
    (byte) 200,
    (byte) 113,
    (byte) 122
  };

  internal static string ssp_imclient_4621()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 120,
        (byte) 56,
        (byte) 22,
        (byte) 60,
        (byte) 27,
        (byte) 219,
        (byte) 94,
        (byte) 13,
        (byte) 151
      };
      byte[] numArray3 = new byte[9];
      numArray3[4] = (byte) 100;
      numArray3[1] = (byte) 230;
      numArray3[2] = (byte) 110;
      numArray3[0] = (byte) 175;
      numArray3[8] = (byte) 119;
      numArray3[3] = (byte) 2;
      numArray3[6] = (byte) 229;
      numArray3[7] = (byte) 161;
      numArray3[5] = (byte) 183;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 249,
      (byte) 88,
      (byte) 252,
      (byte) 18,
      (byte) 0,
      (byte) 0,
      (byte) 183,
      (byte) 0,
      (byte) 0
    };
    numArray5[4] = (byte) 194;
    numArray5[5] = (byte) 203;
    numArray5[7] = (byte) 230;
    numArray5[8] = (byte) 225;
    byte[] numArray6 = new byte[9]
    {
      (byte) 204,
      (byte) 15,
      (byte) 58,
      (byte) 152,
      (byte) 31 /*0x1F*/,
      (byte) 205,
      (byte) 174,
      (byte) 139,
      (byte) 177
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4622()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[13] = (byte) 224 /*0xE0*/;
      numArray2[9] = (byte) 23;
      numArray2[2] = (byte) 5;
      numArray2[3] = (byte) 217;
      numArray2[1] = (byte) 133;
      numArray2[11] = (byte) 133;
      numArray2[6] = (byte) 244;
      numArray2[7] = (byte) 152;
      numArray2[8] = (byte) 42;
      numArray2[0] = (byte) 219;
      numArray2[10] = (byte) 17;
      numArray2[4] = (byte) 169;
      numArray2[12] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 146;
      numArray2[14] = (byte) 25;
      byte[] numArray3 = new byte[15]
      {
        (byte) 223,
        (byte) 94,
        (byte) 163,
        (byte) 230,
        (byte) 10,
        (byte) 77,
        (byte) 8,
        (byte) 253,
        (byte) 74,
        (byte) 132,
        (byte) 38,
        (byte) 103,
        (byte) 251,
        (byte) 154,
        (byte) 138
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_4620.sspq, 0, (Array) numArray4, 0, 26);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4620.sspr, 0, (Array) numArray4, 0, 26);
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
    numArray6[4] = (byte) 111;
    numArray6[11] = (byte) 111;
    numArray6[2] = (byte) 104;
    numArray6[3] = (byte) 12;
    numArray6[1] = (byte) 54;
    numArray6[5] = (byte) 56;
    numArray6[6] = (byte) 15;
    numArray6[8] = (byte) 100;
    numArray6[0] = (byte) 1;
    numArray6[9] = (byte) 163;
    numArray6[12] = (byte) 238;
    numArray6[13] = (byte) 131;
    numArray6[10] = (byte) 28;
    numArray6[7] = (byte) 117;
    numArray6[14] = (byte) 53;
    byte[] numArray7 = new byte[15]
    {
      (byte) 63 /*0x3F*/,
      (byte) 30,
      (byte) 52,
      (byte) 249,
      (byte) 97,
      (byte) 238,
      (byte) 220,
      (byte) 73,
      (byte) 210,
      (byte) 202,
      (byte) 82,
      (byte) 89,
      (byte) 23,
      (byte) 110,
      (byte) 218
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_4623()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 35,
        (byte) 187,
        (byte) 224 /*0xE0*/,
        (byte) 224 /*0xE0*/,
        (byte) 117,
        (byte) 209,
        (byte) 141,
        (byte) 94,
        (byte) 15,
        (byte) 20,
        (byte) 227,
        (byte) 36
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 189,
        (byte) 36,
        (byte) 11,
        (byte) 6,
        (byte) 49,
        (byte) 154,
        (byte) 252,
        (byte) 82,
        (byte) 113,
        (byte) 122,
        (byte) 121,
        (byte) 10
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 203,
      (byte) 197,
      (byte) 117,
      (byte) 228,
      (byte) 128 /*0x80*/,
      (byte) 224 /*0xE0*/,
      (byte) 227,
      (byte) 48 /*0x30*/,
      (byte) 55,
      (byte) 129,
      (byte) 222,
      (byte) 49
    };
    byte[] numArray6 = new byte[12];
    numArray6[4] = (byte) 16 /*0x10*/;
    numArray6[8] = (byte) 183;
    numArray6[2] = (byte) 51;
    numArray6[6] = (byte) 30;
    numArray6[3] = (byte) 145;
    numArray6[0] = (byte) 157;
    numArray6[5] = (byte) 40;
    numArray6[7] = (byte) 211;
    numArray6[1] = (byte) 39;
    numArray6[9] = (byte) 172;
    numArray6[10] = (byte) 18;
    numArray6[11] = (byte) 31 /*0x1F*/;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
