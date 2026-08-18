
// Type: ImSSP.sc_4561
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4561
{
  private static byte[] sspq = new byte[11]
  {
    (byte) 166,
    (byte) 108,
    (byte) 248,
    (byte) 11,
    (byte) 148,
    (byte) 101,
    (byte) 13,
    (byte) 44,
    (byte) 2,
    (byte) 159,
    (byte) 1
  };
  private static byte[] sspr = new byte[11]
  {
    (byte) 107,
    (byte) 159,
    (byte) 165,
    (byte) 220,
    (byte) 211,
    (byte) 47,
    (byte) 228,
    (byte) 19,
    (byte) 217,
    (byte) 253,
    (byte) 11
  };

  internal static string ssp_imclient_4562()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 169,
        (byte) 237
      };
      numArray2[1] = (byte) 96 /*0x60*/;
      numArray2[0] = (byte) 249;
      numArray2[2] = (byte) 197;
      byte[] numArray3 = new byte[5]
      {
        (byte) 212,
        (byte) 127 /*0x7F*/,
        (byte) 39,
        (byte) 191,
        (byte) 145
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[5];
    byte[] numArray5 = new byte[5]
    {
      (byte) 0,
      (byte) 201,
      (byte) 0,
      (byte) 16 /*0x10*/,
      (byte) 0
    };
    numArray5[2] = (byte) 36;
    numArray5[0] = (byte) 203;
    numArray5[4] = (byte) 119;
    byte[] numArray6 = new byte[5]
    {
      (byte) 194,
      (byte) 194,
      (byte) 222,
      (byte) 239,
      (byte) 215
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4563()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 35,
        (byte) 148,
        (byte) 200,
        (byte) 18,
        (byte) 72
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 124,
        (byte) 190,
        (byte) 79,
        (byte) 156,
        (byte) 65
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_4561.sspq, 0, (Array) numArray4, 0, 11);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4561.sspr, 0, (Array) numArray4, 0, 11);
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
    byte[] numArray5 = new byte[5];
    byte[] numArray6 = new byte[5]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 56,
      (byte) 0
    };
    numArray6[0] = (byte) 163;
    numArray6[1] = (byte) 51;
    numArray6[2] = (byte) 239;
    numArray6[4] = (byte) 126;
    byte[] numArray7 = new byte[5]
    {
      (byte) 242,
      (byte) 217,
      (byte) 135,
      (byte) 239,
      (byte) 178
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_4564()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 250,
        (byte) 173,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 139,
        (byte) 188
      };
      numArray2[4] = (byte) 3;
      numArray2[3] = (byte) 190;
      numArray2[2] = (byte) 243;
      byte[] numArray3 = new byte[7]
      {
        (byte) 140,
        (byte) 5,
        (byte) 164,
        (byte) 205,
        (byte) 218,
        (byte) 89,
        (byte) 100
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 155,
      (byte) 138,
      (byte) 139,
      (byte) 187,
      (byte) 240 /*0xF0*/,
      (byte) 165,
      (byte) 143
    };
    byte[] numArray6 = new byte[7]
    {
      (byte) 252,
      (byte) 119,
      (byte) 144 /*0x90*/,
      (byte) 66,
      (byte) 121,
      (byte) 227,
      (byte) 58
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4565()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7];
      numArray2[4] = (byte) 152;
      numArray2[1] = (byte) 152;
      numArray2[2] = (byte) 125;
      numArray2[3] = (byte) 237;
      numArray2[0] = (byte) 29;
      numArray2[5] = (byte) 41;
      numArray2[6] = (byte) 153;
      byte[] numArray3 = new byte[7];
      numArray3[4] = (byte) 147;
      numArray3[0] = (byte) 249;
      numArray3[2] = (byte) 223;
      numArray3[5] = (byte) 72;
      numArray3[3] = (byte) 197;
      numArray3[1] = (byte) 212;
      numArray3[6] = (byte) 213;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7];
    numArray5[1] = (byte) 211;
    numArray5[6] = (byte) 42;
    numArray5[4] = (byte) 8;
    numArray5[3] = (byte) 10;
    numArray5[2] = (byte) 39;
    numArray5[5] = (byte) 100;
    numArray5[0] = (byte) 244;
    byte[] numArray6 = new byte[7]
    {
      (byte) 82,
      (byte) 136,
      (byte) 175,
      (byte) 38,
      (byte) 238,
      (byte) 152,
      (byte) 227
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
