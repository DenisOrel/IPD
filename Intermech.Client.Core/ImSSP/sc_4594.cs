
// Type: ImSSP.sc_4594
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4594
{
  private static byte[] sspq = new byte[49]
  {
    (byte) 243,
    (byte) 123,
    (byte) 1,
    (byte) 58,
    (byte) 145,
    (byte) 178,
    (byte) 6,
    (byte) 110,
    (byte) 43,
    (byte) 238,
    (byte) 76,
    (byte) 142,
    (byte) 17,
    (byte) 116,
    (byte) 41,
    (byte) 81,
    (byte) 176 /*0xB0*/,
    (byte) 68,
    (byte) 80 /*0x50*/,
    (byte) 25,
    (byte) 104,
    (byte) 135,
    (byte) 148,
    (byte) 164,
    (byte) 10,
    (byte) 253,
    (byte) 196,
    (byte) 147,
    (byte) 246,
    (byte) 107,
    (byte) 220,
    (byte) 118,
    (byte) 101,
    (byte) 221,
    (byte) 243,
    (byte) 91,
    (byte) 89,
    (byte) 230,
    (byte) 138,
    (byte) 185,
    (byte) 234,
    (byte) 252,
    (byte) 179,
    (byte) 71,
    (byte) 190,
    (byte) 140,
    (byte) 135,
    (byte) 249,
    (byte) 6
  };
  private static byte[] sspr = new byte[49]
  {
    (byte) 96 /*0x60*/,
    (byte) 18,
    (byte) 229,
    (byte) 177,
    (byte) 182,
    (byte) 74,
    (byte) 202,
    (byte) 100,
    (byte) 18,
    (byte) 80 /*0x50*/,
    (byte) 98,
    (byte) 81,
    (byte) 74,
    (byte) 89,
    (byte) 194,
    (byte) 10,
    (byte) 251,
    (byte) 141,
    (byte) 226,
    (byte) 1,
    (byte) 162,
    (byte) 68,
    (byte) 69,
    (byte) 118,
    (byte) 32 /*0x20*/,
    (byte) 153,
    (byte) 72,
    (byte) 201,
    (byte) 15,
    (byte) 120,
    (byte) 234,
    (byte) 4,
    (byte) 161,
    (byte) 154,
    (byte) 155,
    (byte) 28,
    (byte) 70,
    (byte) 79,
    (byte) 194,
    (byte) 18,
    (byte) 81,
    (byte) 211,
    (byte) 90,
    (byte) 1,
    (byte) 97,
    (byte) 20,
    (byte) 75,
    (byte) 44,
    (byte) 117
  };

  internal static string ssp_imclient_4595()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 25,
        (byte) 145,
        (byte) 182,
        (byte) 104,
        (byte) 200,
        (byte) 48 /*0x30*/,
        (byte) 12,
        (byte) 134,
        (byte) 122,
        (byte) 126,
        (byte) 176 /*0xB0*/,
        (byte) 180,
        (byte) 88,
        (byte) 68,
        (byte) 176 /*0xB0*/
      };
      byte[] numArray3 = new byte[15];
      numArray3[5] = (byte) 2;
      numArray3[6] = (byte) 124;
      numArray3[2] = (byte) 131;
      numArray3[3] = (byte) 85;
      numArray3[12] = (byte) 7;
      numArray3[13] = (byte) 9;
      numArray3[7] = (byte) 105;
      numArray3[8] = (byte) 201;
      numArray3[9] = (byte) 124;
      numArray3[4] = (byte) 219;
      numArray3[10] = (byte) 61;
      numArray3[11] = (byte) 249;
      numArray3[0] = (byte) 139;
      numArray3[1] = (byte) 254;
      numArray3[14] = (byte) 68;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 101,
      (byte) 201,
      (byte) 175,
      (byte) 37,
      (byte) 149,
      (byte) 50,
      (byte) 195,
      (byte) 87,
      (byte) 32 /*0x20*/,
      (byte) 245,
      (byte) 58,
      (byte) 12,
      (byte) 190,
      (byte) 230,
      (byte) 214
    };
    byte[] numArray6 = new byte[15];
    numArray6[10] = (byte) 224 /*0xE0*/;
    numArray6[1] = (byte) 23;
    numArray6[2] = (byte) 78;
    numArray6[8] = (byte) 96 /*0x60*/;
    numArray6[4] = (byte) 0;
    numArray6[3] = (byte) 235;
    numArray6[5] = (byte) 145;
    numArray6[7] = (byte) 234;
    numArray6[11] = (byte) 162;
    numArray6[9] = (byte) 76;
    numArray6[0] = (byte) 15;
    numArray6[6] = (byte) 213;
    numArray6[12] = (byte) 131;
    numArray6[13] = (byte) 156;
    numArray6[14] = (byte) 165;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_4594.sspq, 0, (Array) numArray7, 0, 49);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4594.sspr, 0, (Array) numArray7, 0, 49);
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
