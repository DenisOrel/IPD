
// Type: ImSSP.sc_2504
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2504
{
  private static byte[] sspq = new byte[45]
  {
    (byte) 58,
    (byte) 45,
    (byte) 12,
    (byte) 98,
    (byte) 60,
    (byte) 76,
    (byte) 105,
    (byte) 136,
    (byte) 53,
    (byte) 227,
    (byte) 42,
    (byte) 31 /*0x1F*/,
    (byte) 180,
    (byte) 143,
    (byte) 166,
    (byte) 113,
    (byte) 50,
    (byte) 2,
    (byte) 81,
    (byte) 204,
    (byte) 17,
    (byte) 187,
    (byte) 146,
    (byte) 47,
    (byte) 40,
    (byte) 37,
    (byte) 185,
    (byte) 242,
    (byte) 240 /*0xF0*/,
    (byte) 84,
    (byte) 154,
    (byte) 73,
    (byte) 23,
    (byte) 97,
    (byte) 90,
    (byte) 126,
    (byte) 225,
    (byte) 171,
    (byte) 69,
    (byte) 29,
    (byte) 80 /*0x50*/,
    (byte) 144 /*0x90*/,
    (byte) 180,
    (byte) 174,
    (byte) 226
  };
  private static byte[] sspr = new byte[45]
  {
    (byte) 81,
    (byte) 247,
    (byte) 195,
    (byte) 107,
    (byte) 208 /*0xD0*/,
    (byte) 194,
    (byte) 176 /*0xB0*/,
    (byte) 94,
    (byte) 177,
    (byte) 62,
    (byte) 183,
    (byte) 71,
    (byte) 100,
    (byte) 240 /*0xF0*/,
    (byte) 233,
    (byte) 157,
    (byte) 76,
    (byte) 76,
    (byte) 172,
    (byte) 214,
    (byte) 32 /*0x20*/,
    (byte) 16 /*0x10*/,
    (byte) 8,
    (byte) 245,
    (byte) 59,
    (byte) 228,
    (byte) 206,
    (byte) 120,
    (byte) 224 /*0xE0*/,
    (byte) 250,
    (byte) 116,
    (byte) 197,
    (byte) 139,
    (byte) 109,
    (byte) 172,
    (byte) 212,
    (byte) 97,
    (byte) 236,
    (byte) 248,
    (byte) 65,
    (byte) 23,
    (byte) 233,
    (byte) 219,
    (byte) 211,
    (byte) 104
  };

  internal static string ssp_imclient_2505()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 59,
        (byte) 51,
        (byte) 185,
        (byte) 35,
        (byte) 83,
        (byte) 95,
        (byte) 65,
        (byte) 132,
        (byte) 188,
        (byte) 134,
        (byte) 42,
        (byte) 164,
        (byte) 35,
        (byte) 59,
        (byte) 25,
        (byte) 65,
        (byte) 167,
        (byte) 14,
        (byte) 149,
        (byte) 83,
        (byte) 83,
        (byte) 17,
        (byte) 19,
        (byte) 5,
        (byte) 148,
        (byte) 172,
        (byte) 237,
        (byte) 14,
        (byte) 130,
        (byte) 106,
        (byte) 185,
        (byte) 25,
        (byte) 233,
        (byte) 145,
        (byte) 12,
        (byte) 241,
        (byte) 28,
        (byte) 237,
        (byte) 28,
        (byte) 165,
        (byte) 254
      };
      byte[] numArray3 = new byte[41]
      {
        (byte) 189,
        (byte) 68,
        (byte) 109,
        (byte) 181,
        (byte) 44,
        (byte) 188,
        (byte) 142,
        (byte) 119,
        (byte) 13,
        (byte) 107,
        (byte) 156,
        (byte) 207,
        (byte) 64 /*0x40*/,
        (byte) 148,
        (byte) 196,
        (byte) 151,
        (byte) 162,
        (byte) 208 /*0xD0*/,
        (byte) 156,
        (byte) 239,
        (byte) 92,
        (byte) 63 /*0x3F*/,
        (byte) 158,
        (byte) 136,
        (byte) 126,
        (byte) 97,
        (byte) 26,
        (byte) 75,
        (byte) 149,
        (byte) 125,
        (byte) 146,
        (byte) 43,
        (byte) 125,
        (byte) 132,
        (byte) 161,
        (byte) 73,
        (byte) 239,
        (byte) 199,
        (byte) 140,
        (byte) 5,
        (byte) 35
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_2504.sspq, 0, (Array) numArray4, 0, 45);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_2504.sspr, 0, (Array) numArray4, 0, 45);
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
    byte[] numArray5 = new byte[41];
    byte[] numArray6 = new byte[41];
    numArray6[25] = (byte) 144 /*0x90*/;
    numArray6[17] = (byte) 151;
    numArray6[2] = (byte) 94;
    numArray6[3] = (byte) 117;
    numArray6[19] = (byte) 72;
    numArray6[5] = (byte) 203;
    numArray6[6] = (byte) 182;
    numArray6[7] = (byte) 118;
    numArray6[23] = (byte) 52;
    numArray6[32 /*0x20*/] = (byte) 227;
    numArray6[38] = (byte) 188;
    numArray6[11] = (byte) 155;
    numArray6[36] = (byte) 90;
    numArray6[13] = (byte) 58;
    numArray6[18] = (byte) 224 /*0xE0*/;
    numArray6[0] = (byte) 210;
    numArray6[34] = (byte) 85;
    numArray6[20] = (byte) 224 /*0xE0*/;
    numArray6[14] = (byte) 23;
    numArray6[40] = (byte) 227;
    numArray6[8] = (byte) 47;
    numArray6[26] = (byte) 135;
    numArray6[9] = (byte) 146;
    numArray6[24] = (byte) 39;
    numArray6[15] = (byte) 146;
    numArray6[22] = (byte) 83;
    numArray6[4] = (byte) 51;
    numArray6[27] = (byte) 189;
    numArray6[28] = (byte) 52;
    numArray6[12] = (byte) 221;
    numArray6[21] = (byte) 7;
    numArray6[10] = (byte) 126;
    numArray6[31 /*0x1F*/] = (byte) 7;
    numArray6[33] = (byte) 76;
    numArray6[16 /*0x10*/] = (byte) 119;
    numArray6[35] = (byte) 189;
    numArray6[30] = (byte) 128 /*0x80*/;
    numArray6[37] = (byte) 124;
    numArray6[29] = (byte) 123;
    numArray6[39] = (byte) 137;
    numArray6[1] = (byte) 103;
    byte[] numArray7 = new byte[41]
    {
      (byte) 170,
      (byte) 234,
      (byte) 170,
      (byte) 4,
      (byte) 17,
      (byte) 37,
      (byte) 5,
      (byte) 69,
      (byte) 207,
      (byte) 183,
      (byte) 250,
      (byte) 101,
      (byte) 212,
      (byte) 189,
      (byte) 140,
      (byte) 49,
      (byte) 112 /*0x70*/,
      (byte) 164,
      (byte) 244,
      (byte) 57,
      (byte) 157,
      (byte) 81,
      (byte) 4,
      (byte) 47,
      (byte) 37,
      (byte) 126,
      (byte) 95,
      (byte) 223,
      (byte) 118,
      (byte) 153,
      (byte) 233,
      (byte) 171,
      (byte) 114,
      (byte) 165,
      (byte) 3,
      (byte) 17,
      (byte) 11,
      (byte) 207,
      (byte) 82,
      (byte) 24,
      (byte) 60
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
