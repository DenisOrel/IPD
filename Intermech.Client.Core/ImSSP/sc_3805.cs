
// Type: ImSSP.sc_3805
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3805
{
  private static byte[] sspq = new byte[32 /*0x20*/]
  {
    (byte) 222,
    (byte) 88,
    (byte) 135,
    (byte) 196,
    (byte) 39,
    (byte) 30,
    (byte) 253,
    (byte) 83,
    (byte) 79,
    (byte) 67,
    (byte) 230,
    (byte) 50,
    byte.MaxValue,
    (byte) 89,
    (byte) 181,
    (byte) 252,
    (byte) 0,
    (byte) 149,
    (byte) 168,
    (byte) 118,
    (byte) 170,
    (byte) 137,
    (byte) 174,
    (byte) 156,
    (byte) 35,
    (byte) 187,
    (byte) 151,
    (byte) 219,
    (byte) 231,
    (byte) 97,
    (byte) 206,
    (byte) 58
  };
  private static byte[] sspr = new byte[32 /*0x20*/]
  {
    (byte) 200,
    (byte) 81,
    (byte) 189,
    (byte) 145,
    (byte) 181,
    (byte) 72,
    (byte) 109,
    (byte) 87,
    (byte) 126,
    (byte) 189,
    (byte) 89,
    (byte) 110,
    (byte) 29,
    (byte) 109,
    (byte) 101,
    (byte) 181,
    (byte) 22,
    (byte) 83,
    (byte) 12,
    (byte) 77,
    (byte) 137,
    (byte) 245,
    (byte) 79,
    (byte) 172,
    (byte) 98,
    (byte) 50,
    (byte) 151,
    (byte) 241,
    (byte) 246,
    (byte) 241,
    (byte) 235,
    (byte) 79
  };

  internal static string ssp_imclient_3806()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[6] = (byte) 133;
      numArray2[3] = (byte) 212;
      numArray2[2] = (byte) 38;
      numArray2[0] = (byte) 49;
      numArray2[4] = (byte) 90;
      numArray2[1] = (byte) 115;
      numArray2[5] = (byte) 60;
      numArray2[7] = (byte) 60;
      numArray2[8] = (byte) 187;
      byte[] numArray3 = new byte[9]
      {
        (byte) 214,
        (byte) 58,
        (byte) 23,
        (byte) 119,
        (byte) 20,
        (byte) 96 /*0x60*/,
        (byte) 78,
        (byte) 24,
        (byte) 25
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 194,
      (byte) 162,
      (byte) 31 /*0x1F*/,
      (byte) 198,
      (byte) 42,
      (byte) 151,
      (byte) 87,
      (byte) 86,
      (byte) 46
    };
    byte[] numArray6 = new byte[9];
    numArray6[0] = (byte) 21;
    numArray6[1] = (byte) 53;
    numArray6[2] = (byte) 106;
    numArray6[6] = (byte) 186;
    numArray6[4] = (byte) 199;
    numArray6[5] = (byte) 24;
    numArray6[3] = (byte) 156;
    numArray6[7] = (byte) 50;
    numArray6[8] = (byte) 169;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3807()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 12,
        (byte) 221,
        (byte) 115,
        (byte) 75,
        (byte) 63 /*0x3F*/,
        (byte) 208 /*0xD0*/,
        (byte) 146,
        (byte) 7,
        (byte) 19,
        (byte) 120,
        (byte) 234,
        (byte) 12,
        (byte) 111,
        (byte) 208 /*0xD0*/,
        (byte) 7
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 132,
        (byte) 194,
        (byte) 36,
        (byte) 189,
        (byte) 250,
        (byte) 35,
        (byte) 214,
        (byte) 251,
        (byte) 194,
        (byte) 167,
        (byte) 25,
        (byte) 208 /*0xD0*/,
        (byte) 64 /*0x40*/,
        (byte) 209,
        (byte) 171
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
      (byte) 46,
      (byte) 229,
      (byte) 140,
      (byte) 99,
      (byte) 102,
      (byte) 201,
      (byte) 194,
      (byte) 144 /*0x90*/,
      (byte) 52,
      (byte) 110,
      (byte) 76,
      (byte) 123,
      (byte) 156,
      (byte) 3,
      (byte) 170
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 138,
      (byte) 58,
      (byte) 38,
      (byte) 125,
      (byte) 186,
      (byte) 167,
      (byte) 100,
      (byte) 182,
      (byte) 29,
      (byte) 111,
      (byte) 252,
      (byte) 68,
      (byte) 232,
      (byte) 142,
      (byte) 148
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[32 /*0x20*/];
    byte[] response = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_3805.sspq, 0, (Array) numArray7, 0, 32 /*0x20*/);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3805.sspr, 0, (Array) numArray7, 0, 32 /*0x20*/);
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

  internal static string ssp_imclient_3808()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[13] = (byte) 76;
      numArray2[7] = (byte) 105;
      numArray2[1] = (byte) 82;
      numArray2[3] = (byte) 166;
      numArray2[10] = (byte) 77;
      numArray2[5] = (byte) 101;
      numArray2[6] = (byte) 69;
      numArray2[0] = (byte) 187;
      numArray2[8] = (byte) 118;
      numArray2[9] = (byte) 114;
      numArray2[11] = (byte) 77;
      numArray2[12] = (byte) 60;
      numArray2[2] = (byte) 77;
      numArray2[4] = (byte) 33;
      numArray2[14] = (byte) 34;
      byte[] numArray3 = new byte[15];
      numArray3[13] = (byte) 214;
      numArray3[1] = (byte) 99;
      numArray3[9] = (byte) 21;
      numArray3[3] = (byte) 168;
      numArray3[4] = (byte) 112 /*0x70*/;
      numArray3[2] = (byte) 105;
      numArray3[5] = (byte) 171;
      numArray3[7] = (byte) 241;
      numArray3[8] = (byte) 214;
      numArray3[14] = (byte) 179;
      numArray3[6] = (byte) 13;
      numArray3[11] = (byte) 45;
      numArray3[12] = (byte) 82;
      numArray3[10] = (byte) 188;
      numArray3[0] = (byte) 195;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[7] = (byte) 19;
    numArray5[11] = (byte) 128 /*0x80*/;
    numArray5[13] = (byte) 190;
    numArray5[3] = (byte) 184;
    numArray5[4] = (byte) 195;
    numArray5[5] = (byte) 76;
    numArray5[6] = (byte) 249;
    numArray5[0] = (byte) 117;
    numArray5[8] = (byte) 253;
    numArray5[1] = (byte) 245;
    numArray5[9] = (byte) 163;
    numArray5[10] = (byte) 187;
    numArray5[12] = (byte) 249;
    numArray5[2] = (byte) 224 /*0xE0*/;
    numArray5[14] = (byte) 72;
    byte[] numArray6 = new byte[15]
    {
      (byte) 17,
      (byte) 182,
      (byte) 8,
      (byte) 185,
      (byte) 170,
      (byte) 246,
      (byte) 49,
      (byte) 97,
      (byte) 125,
      (byte) 71,
      (byte) 100,
      (byte) 196,
      (byte) 13,
      (byte) 0,
      (byte) 66
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3809()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 17,
        (byte) 44,
        (byte) 139,
        (byte) 53,
        (byte) 177,
        (byte) 95,
        (byte) 115,
        (byte) 48 /*0x30*/,
        (byte) 9,
        (byte) 197
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 251,
        (byte) 193,
        (byte) 115,
        (byte) 133,
        (byte) 94,
        (byte) 228,
        (byte) 77,
        (byte) 87,
        (byte) 35,
        (byte) 18
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 173;
    numArray5[1] = (byte) 196;
    numArray5[2] = (byte) 41;
    numArray5[9] = (byte) 119;
    numArray5[6] = (byte) 24;
    numArray5[0] = (byte) 164;
    numArray5[7] = (byte) 103;
    numArray5[3] = (byte) 145;
    numArray5[8] = (byte) 89;
    numArray5[4] = (byte) 137;
    byte[] numArray6 = new byte[10]
    {
      (byte) 193,
      (byte) 3,
      (byte) 164,
      (byte) 171,
      (byte) 229,
      (byte) 152,
      (byte) 66,
      (byte) 191,
      (byte) 188,
      (byte) 91
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3810()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[8] = (byte) 108;
      numArray2[4] = (byte) 214;
      numArray2[3] = (byte) 91;
      numArray2[0] = (byte) 79;
      numArray2[2] = (byte) 65;
      numArray2[14] = (byte) 191;
      numArray2[11] = (byte) 249;
      numArray2[7] = (byte) 22;
      numArray2[1] = (byte) 38;
      numArray2[9] = (byte) 165;
      numArray2[10] = (byte) 230;
      numArray2[6] = (byte) 184;
      numArray2[12] = (byte) 74;
      numArray2[13] = (byte) 173;
      numArray2[5] = (byte) 17;
      byte[] numArray3 = new byte[15]
      {
        (byte) 62,
        (byte) 230,
        (byte) 58,
        (byte) 254,
        (byte) 48 /*0x30*/,
        (byte) 112 /*0x70*/,
        (byte) 221,
        (byte) 253,
        (byte) 155,
        (byte) 252,
        (byte) 196,
        (byte) 62,
        (byte) 2,
        (byte) 156,
        (byte) 113
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
      (byte) 170,
      (byte) 65,
      (byte) 98,
      (byte) 108,
      (byte) 189,
      (byte) 145,
      (byte) 71,
      (byte) 190,
      (byte) 25,
      (byte) 128 /*0x80*/,
      (byte) 66,
      (byte) 219,
      (byte) 65,
      (byte) 84,
      (byte) 16 /*0x10*/
    };
    byte[] numArray6 = new byte[15];
    numArray6[2] = (byte) 126;
    numArray6[0] = (byte) 141;
    numArray6[7] = (byte) 93;
    numArray6[3] = (byte) 145;
    numArray6[9] = (byte) 56;
    numArray6[5] = (byte) 248;
    numArray6[13] = (byte) 14;
    numArray6[4] = (byte) 181;
    numArray6[8] = (byte) 146;
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[14] = (byte) 224 /*0xE0*/;
    numArray6[11] = (byte) 47;
    numArray6[6] = (byte) 120;
    numArray6[10] = (byte) 24;
    numArray6[12] = (byte) 61;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
