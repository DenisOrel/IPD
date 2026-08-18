
// Type: ImSSP.sc_3885
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3885
{
  private static byte[] sspq = new byte[10]
  {
    (byte) 187,
    (byte) 184,
    (byte) 137,
    (byte) 44,
    (byte) 143,
    (byte) 251,
    (byte) 74,
    (byte) 152,
    (byte) 67,
    (byte) 5
  };
  private static byte[] sspr = new byte[10]
  {
    (byte) 142,
    (byte) 99,
    (byte) 149,
    (byte) 229,
    (byte) 93,
    (byte) 119,
    (byte) 9,
    (byte) 232,
    (byte) 244,
    (byte) 155
  };

  internal static string ssp_imclient_3886()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 1,
        (byte) 160 /*0xA0*/,
        (byte) 246,
        (byte) 80 /*0x50*/,
        (byte) 200
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 0,
        (byte) 0,
        (byte) 162,
        (byte) 0,
        (byte) 0
      };
      numArray3[1] = (byte) 247;
      numArray3[4] = (byte) 16 /*0x10*/;
      numArray3[3] = (byte) 230;
      numArray3[0] = (byte) 3;
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
      (byte) 0,
      (byte) 0,
      (byte) 69,
      (byte) 0
    };
    numArray5[0] = (byte) 105;
    numArray5[1] = (byte) 112 /*0x70*/;
    numArray5[2] = (byte) 182;
    numArray5[4] = (byte) 241;
    byte[] numArray6 = new byte[5]
    {
      (byte) 70,
      (byte) 220,
      (byte) 162,
      (byte) 92,
      (byte) 193
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_3885.sspq, 0, (Array) numArray7, 0, 10);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3885.sspr, 0, (Array) numArray7, 0, 10);
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

  internal static string ssp_imclient_3887()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 146,
        (byte) 117,
        (byte) 222,
        (byte) 61,
        (byte) 204,
        (byte) 63 /*0x3F*/,
        (byte) 129,
        (byte) 242,
        (byte) 239,
        (byte) 1,
        (byte) 164,
        (byte) 74,
        (byte) 11,
        (byte) 251,
        (byte) 25
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 203,
        (byte) 74,
        (byte) 203,
        (byte) 80 /*0x50*/,
        (byte) 4,
        (byte) 99,
        (byte) 161,
        (byte) 146,
        (byte) 154,
        (byte) 235,
        (byte) 96 /*0x60*/,
        (byte) 75,
        (byte) 84,
        (byte) 11,
        (byte) 116
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 139,
      (byte) 176 /*0xB0*/,
      (byte) 239,
      (byte) 242,
      (byte) 81,
      (byte) 21,
      (byte) 218,
      (byte) 224 /*0xE0*/,
      (byte) 68,
      (byte) 38,
      (byte) 74,
      (byte) 167,
      (byte) 150,
      (byte) 64 /*0x40*/,
      (byte) 27
    };
    byte[] numArray6 = new byte[15];
    numArray6[1] = (byte) 189;
    numArray6[9] = (byte) 78;
    numArray6[13] = (byte) 24;
    numArray6[3] = (byte) 9;
    numArray6[0] = (byte) 187;
    numArray6[5] = (byte) 166;
    numArray6[6] = (byte) 91;
    numArray6[7] = (byte) 135;
    numArray6[8] = (byte) 173;
    numArray6[4] = (byte) 225;
    numArray6[10] = (byte) 176 /*0xB0*/;
    numArray6[14] = (byte) 96 /*0x60*/;
    numArray6[12] = (byte) 94;
    numArray6[11] = (byte) 50;
    numArray6[2] = (byte) 20;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
