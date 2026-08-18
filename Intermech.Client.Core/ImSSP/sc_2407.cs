
// Type: ImSSP.sc_2407
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2407
{
  private static byte[] sspq = new byte[92]
  {
    (byte) 105,
    (byte) 113,
    (byte) 115,
    (byte) 54,
    (byte) 234,
    (byte) 139,
    byte.MaxValue,
    (byte) 74,
    (byte) 73,
    (byte) 138,
    (byte) 202,
    (byte) 214,
    (byte) 38,
    (byte) 175,
    (byte) 224 /*0xE0*/,
    (byte) 48 /*0x30*/,
    (byte) 239,
    (byte) 108,
    (byte) 183,
    (byte) 165,
    (byte) 243,
    (byte) 227,
    (byte) 113,
    (byte) 96 /*0x60*/,
    (byte) 5,
    (byte) 213,
    (byte) 65,
    (byte) 227,
    (byte) 128 /*0x80*/,
    (byte) 55,
    (byte) 115,
    (byte) 218,
    (byte) 75,
    (byte) 194,
    (byte) 187,
    (byte) 137,
    (byte) 117,
    (byte) 187,
    (byte) 7,
    (byte) 63 /*0x3F*/,
    (byte) 213,
    (byte) 62,
    (byte) 77,
    (byte) 168,
    (byte) 165,
    (byte) 155,
    (byte) 45,
    (byte) 216,
    (byte) 28,
    (byte) 194,
    (byte) 172,
    (byte) 159,
    (byte) 46,
    (byte) 31 /*0x1F*/,
    (byte) 241,
    (byte) 177,
    (byte) 220,
    (byte) 51,
    (byte) 91,
    (byte) 163,
    (byte) 14,
    (byte) 89,
    (byte) 169,
    (byte) 131,
    (byte) 233,
    (byte) 43,
    (byte) 154,
    (byte) 126,
    (byte) 31 /*0x1F*/,
    (byte) 203,
    (byte) 83,
    (byte) 114,
    (byte) 81,
    (byte) 7,
    (byte) 130,
    (byte) 14,
    (byte) 6,
    (byte) 137,
    (byte) 2,
    (byte) 62,
    (byte) 196,
    (byte) 70,
    (byte) 63 /*0x3F*/,
    (byte) 43,
    (byte) 233,
    (byte) 44,
    (byte) 193,
    (byte) 178,
    (byte) 108,
    (byte) 14,
    (byte) 148,
    (byte) 102
  };
  private static byte[] sspr = new byte[92]
  {
    (byte) 68,
    (byte) 65,
    (byte) 37,
    (byte) 78,
    (byte) 70,
    (byte) 66,
    (byte) 241,
    (byte) 4,
    (byte) 199,
    (byte) 163,
    (byte) 222,
    (byte) 169,
    (byte) 198,
    (byte) 92,
    (byte) 231,
    (byte) 42,
    (byte) 244,
    (byte) 228,
    (byte) 234,
    (byte) 225,
    (byte) 33,
    (byte) 129,
    (byte) 50,
    (byte) 176 /*0xB0*/,
    (byte) 54,
    (byte) 17,
    (byte) 244,
    (byte) 181,
    (byte) 189,
    (byte) 160 /*0xA0*/,
    (byte) 36,
    (byte) 211,
    (byte) 28,
    (byte) 76,
    (byte) 240 /*0xF0*/,
    (byte) 69,
    (byte) 112 /*0x70*/,
    (byte) 170,
    (byte) 114,
    (byte) 162,
    (byte) 118,
    (byte) 209,
    (byte) 63 /*0x3F*/,
    (byte) 254,
    (byte) 241,
    (byte) 115,
    (byte) 196,
    (byte) 169,
    (byte) 195,
    (byte) 78,
    (byte) 169,
    (byte) 137,
    (byte) 47,
    (byte) 142,
    (byte) 1,
    (byte) 82,
    (byte) 180,
    (byte) 112 /*0x70*/,
    (byte) 43,
    (byte) 95,
    (byte) 80 /*0x50*/,
    (byte) 156,
    (byte) 149,
    (byte) 74,
    (byte) 63 /*0x3F*/,
    (byte) 81,
    (byte) 231,
    (byte) 58,
    (byte) 89,
    (byte) 131,
    (byte) 220,
    (byte) 33,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 84,
    (byte) 211,
    (byte) 68,
    (byte) 19,
    (byte) 110,
    (byte) 111,
    (byte) 203,
    (byte) 187,
    (byte) 249,
    (byte) 55,
    (byte) 254,
    (byte) 31 /*0x1F*/,
    (byte) 108,
    (byte) 154,
    (byte) 203,
    (byte) 242,
    (byte) 155,
    (byte) 171
  };

  internal static string ssp_imclient_2408()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 71,
        (byte) 77,
        (byte) 96 /*0x60*/,
        (byte) 13,
        (byte) 64 /*0x40*/,
        (byte) 204,
        (byte) 39,
        (byte) 149,
        (byte) 0,
        (byte) 221,
        (byte) 125,
        (byte) 95,
        (byte) 161,
        (byte) 5,
        byte.MaxValue,
        (byte) 133,
        (byte) 194,
        (byte) 98,
        (byte) 49,
        (byte) 40
      };
      byte[] numArray3 = new byte[20];
      numArray3[15] = (byte) 100;
      numArray3[5] = (byte) 43;
      numArray3[2] = (byte) 4;
      numArray3[14] = (byte) 232;
      numArray3[9] = (byte) 230;
      numArray3[18] = (byte) 97;
      numArray3[16 /*0x10*/] = (byte) 175;
      numArray3[7] = (byte) 3;
      numArray3[6] = (byte) 27;
      numArray3[17] = (byte) 146;
      numArray3[0] = (byte) 41;
      numArray3[11] = (byte) 209;
      numArray3[4] = (byte) 133;
      numArray3[13] = (byte) 222;
      numArray3[3] = (byte) 182;
      numArray3[1] = (byte) 233;
      numArray3[10] = (byte) 21;
      numArray3[8] = (byte) 25;
      numArray3[12] = (byte) 252;
      numArray3[19] = (byte) 224 /*0xE0*/;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/];
      byte[] response = new byte[31 /*0x1F*/];
      Array.Copy((Array) sc_2407.sspq, 0, (Array) numArray4, 0, 31 /*0x1F*/);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_2407.sspr, 0, (Array) numArray4, 0, 31 /*0x1F*/);
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
    byte[] numArray5 = new byte[20];
    byte[] numArray6 = new byte[20]
    {
      (byte) 252,
      (byte) 82,
      (byte) 191,
      (byte) 71,
      (byte) 187,
      (byte) 156,
      (byte) 166,
      (byte) 208 /*0xD0*/,
      (byte) 81,
      (byte) 91,
      (byte) 159,
      (byte) 80 /*0x50*/,
      (byte) 99,
      (byte) 118,
      (byte) 40,
      (byte) 3,
      (byte) 149,
      (byte) 254,
      (byte) 177,
      (byte) 11
    };
    byte[] numArray7 = new byte[20]
    {
      (byte) 96 /*0x60*/,
      (byte) 149,
      (byte) 6,
      (byte) 142,
      (byte) 57,
      (byte) 227,
      (byte) 154,
      (byte) 208 /*0xD0*/,
      (byte) 92,
      (byte) 4,
      (byte) 253,
      (byte) 81,
      (byte) 58,
      (byte) 228,
      (byte) 22,
      (byte) 182,
      (byte) 131,
      (byte) 78,
      (byte) 211,
      (byte) 4
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_2409()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[28];
      byte[] numArray2 = new byte[28]
      {
        (byte) 133,
        byte.MaxValue,
        (byte) 18,
        (byte) 49,
        (byte) 20,
        (byte) 74,
        (byte) 27,
        (byte) 202,
        (byte) 17,
        (byte) 22,
        (byte) 205,
        (byte) 131,
        (byte) 103,
        (byte) 103,
        (byte) 37,
        (byte) 148,
        (byte) 72,
        (byte) 155,
        (byte) 101,
        (byte) 73,
        (byte) 84,
        (byte) 147,
        (byte) 13,
        (byte) 234,
        (byte) 220,
        (byte) 209,
        (byte) 171,
        (byte) 229
      };
      byte[] numArray3 = new byte[28]
      {
        (byte) 43,
        (byte) 200,
        (byte) 23,
        (byte) 8,
        (byte) 187,
        (byte) 241,
        (byte) 26,
        (byte) 53,
        (byte) 51,
        (byte) 167,
        (byte) 140,
        (byte) 128 /*0x80*/,
        (byte) 13,
        (byte) 116,
        (byte) 46,
        (byte) 85,
        (byte) 201,
        (byte) 6,
        (byte) 186,
        (byte) 31 /*0x1F*/,
        (byte) 126,
        (byte) 7,
        (byte) 98,
        (byte) 32 /*0x20*/,
        (byte) 221,
        (byte) 244,
        (byte) 193,
        (byte) 158
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24];
      byte[] response = new byte[24];
      Array.Copy((Array) sc_2407.sspq, 31 /*0x1F*/, (Array) numArray4, 0, 24);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_2407.sspr, 31 /*0x1F*/, (Array) numArray4, 0, 24);
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
    byte[] numArray5 = new byte[28];
    byte[] numArray6 = new byte[28]
    {
      (byte) 242,
      (byte) 40,
      (byte) 84,
      (byte) 67,
      (byte) 159,
      (byte) 53,
      (byte) 250,
      (byte) 229,
      (byte) 234,
      (byte) 214,
      (byte) 169,
      (byte) 28,
      (byte) 165,
      (byte) 105,
      (byte) 11,
      (byte) 211,
      (byte) 223,
      (byte) 94,
      (byte) 9,
      (byte) 126,
      (byte) 84,
      (byte) 192 /*0xC0*/,
      (byte) 65,
      (byte) 55,
      (byte) 54,
      (byte) 125,
      (byte) 12,
      (byte) 178
    };
    byte[] numArray7 = new byte[28]
    {
      (byte) 24,
      (byte) 105,
      (byte) 108,
      (byte) 105,
      (byte) 124,
      (byte) 180,
      (byte) 201,
      (byte) 165,
      (byte) 84,
      (byte) 10,
      (byte) 139,
      (byte) 230,
      (byte) 13,
      (byte) 18,
      (byte) 63 /*0x3F*/,
      (byte) 84,
      (byte) 123,
      (byte) 30,
      (byte) 35,
      (byte) 125,
      (byte) 220,
      (byte) 56,
      (byte) 72,
      (byte) 239,
      (byte) 49,
      (byte) 63 /*0x3F*/,
      (byte) 98,
      (byte) 253
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 28);
    for (int index = 0; index < 28; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_2410()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[11] = (byte) 210;
      numArray2[0] = (byte) 43;
      numArray2[3] = (byte) 97;
      numArray2[1] = (byte) 208 /*0xD0*/;
      numArray2[4] = (byte) 33;
      numArray2[10] = (byte) 98;
      numArray2[6] = (byte) 103;
      numArray2[5] = (byte) 120;
      numArray2[8] = (byte) 218;
      numArray2[9] = (byte) 89;
      numArray2[7] = (byte) 10;
      numArray2[2] = (byte) 201;
      numArray2[12] = (byte) 251;
      numArray2[13] = (byte) 127 /*0x7F*/;
      byte[] numArray3 = new byte[14]
      {
        (byte) 69,
        (byte) 146,
        (byte) 47,
        (byte) 199,
        (byte) 147,
        (byte) 253,
        (byte) 60,
        byte.MaxValue,
        (byte) 171,
        (byte) 103,
        (byte) 192 /*0xC0*/,
        (byte) 249,
        (byte) 99,
        (byte) 79
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14];
    numArray5[13] = (byte) 94;
    numArray5[12] = (byte) 251;
    numArray5[0] = (byte) 107;
    numArray5[11] = (byte) 164;
    numArray5[1] = (byte) 126;
    numArray5[4] = (byte) 212;
    numArray5[6] = (byte) 169;
    numArray5[7] = (byte) 178;
    numArray5[8] = (byte) 102;
    numArray5[3] = (byte) 240 /*0xF0*/;
    numArray5[10] = byte.MaxValue;
    numArray5[9] = (byte) 246;
    numArray5[2] = (byte) 253;
    numArray5[5] = (byte) 160 /*0xA0*/;
    byte[] numArray6 = new byte[14]
    {
      (byte) 32 /*0x20*/,
      (byte) 90,
      (byte) 211,
      (byte) 94,
      (byte) 141,
      (byte) 83,
      (byte) 171,
      (byte) 201,
      (byte) 213,
      (byte) 5,
      (byte) 41,
      (byte) 135,
      (byte) 28,
      (byte) 166
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2411()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 146,
        (byte) 222,
        (byte) 246,
        (byte) 83,
        (byte) 253,
        (byte) 142,
        (byte) 180,
        (byte) 206,
        (byte) 103,
        (byte) 59,
        (byte) 103,
        (byte) 74,
        (byte) 37,
        (byte) 252
      };
      byte[] numArray3 = new byte[14];
      numArray3[11] = (byte) 115;
      numArray3[9] = (byte) 198;
      numArray3[2] = (byte) 78;
      numArray3[3] = (byte) 130;
      numArray3[1] = (byte) 244;
      numArray3[4] = (byte) 136;
      numArray3[6] = (byte) 52;
      numArray3[5] = (byte) 73;
      numArray3[8] = (byte) 224 /*0xE0*/;
      numArray3[0] = (byte) 190;
      numArray3[7] = (byte) 109;
      numArray3[13] = (byte) 45;
      numArray3[12] = (byte) 254;
      numArray3[10] = (byte) 200;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 250,
      (byte) 176 /*0xB0*/,
      (byte) 30,
      (byte) 17,
      (byte) 28,
      (byte) 103,
      (byte) 27,
      (byte) 62,
      (byte) 40,
      (byte) 63 /*0x3F*/,
      (byte) 207,
      (byte) 57,
      (byte) 63 /*0x3F*/,
      (byte) 73
    };
    byte[] numArray6 = new byte[14];
    numArray6[8] = (byte) 35;
    numArray6[5] = (byte) 58;
    numArray6[7] = (byte) 190;
    numArray6[1] = (byte) 191;
    numArray6[4] = (byte) 230;
    numArray6[0] = (byte) 80 /*0x50*/;
    numArray6[6] = (byte) 197;
    numArray6[2] = (byte) 197;
    numArray6[13] = (byte) 183;
    numArray6[9] = (byte) 91;
    numArray6[10] = (byte) 206;
    numArray6[11] = (byte) 220;
    numArray6[3] = (byte) 94;
    numArray6[12] = (byte) 143;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2412()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 56,
        (byte) 142,
        (byte) 109,
        (byte) 153,
        (byte) 6,
        (byte) 234,
        (byte) 132,
        (byte) 107,
        (byte) 90,
        (byte) 161,
        (byte) 44,
        (byte) 237,
        (byte) 187,
        (byte) 109
      };
      byte[] numArray3 = new byte[14]
      {
        (byte) 23,
        (byte) 217,
        (byte) 133,
        (byte) 189,
        (byte) 116,
        (byte) 194,
        (byte) 26,
        (byte) 21,
        (byte) 97,
        (byte) 128 /*0x80*/,
        (byte) 50,
        (byte) 166,
        (byte) 68,
        (byte) 69
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 20,
      (byte) 215,
      (byte) 13,
      (byte) 67,
      (byte) 42,
      (byte) 136,
      (byte) 227,
      (byte) 151,
      (byte) 199,
      (byte) 38,
      (byte) 61,
      (byte) 43,
      (byte) 47,
      (byte) 183
    };
    byte[] numArray6 = new byte[14]
    {
      (byte) 73,
      (byte) 158,
      (byte) 78,
      (byte) 92,
      (byte) 213,
      (byte) 101,
      (byte) 7,
      (byte) 1,
      (byte) 233,
      (byte) 164,
      (byte) 36,
      (byte) 111,
      (byte) 111,
      (byte) 226
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[37];
    byte[] response = new byte[37];
    Array.Copy((Array) sc_2407.sspq, 55, (Array) numArray7, 0, 37);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2407.sspr, 55, (Array) numArray7, 0, 37);
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

  internal static string ssp_imclient_2413()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[9] = (byte) 30;
      numArray2[1] = (byte) 254;
      numArray2[4] = (byte) 4;
      numArray2[8] = (byte) 168;
      numArray2[0] = (byte) 51;
      numArray2[10] = (byte) 95;
      numArray2[6] = (byte) 107;
      numArray2[7] = (byte) 45;
      numArray2[3] = (byte) 128 /*0x80*/;
      numArray2[13] = (byte) 174;
      numArray2[5] = (byte) 249;
      numArray2[11] = (byte) 219;
      numArray2[12] = (byte) 69;
      numArray2[2] = (byte) 233;
      byte[] numArray3 = new byte[14]
      {
        (byte) 52,
        (byte) 52,
        (byte) 65,
        (byte) 5,
        (byte) 175,
        (byte) 106,
        (byte) 225,
        (byte) 6,
        (byte) 67,
        (byte) 203,
        (byte) 228,
        (byte) 51,
        (byte) 138,
        (byte) 27
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14];
    numArray5[7] = (byte) 1;
    numArray5[13] = (byte) 223;
    numArray5[0] = (byte) 55;
    numArray5[1] = (byte) 237;
    numArray5[4] = (byte) 9;
    numArray5[12] = (byte) 40;
    numArray5[3] = (byte) 29;
    numArray5[9] = (byte) 150;
    numArray5[8] = (byte) 143;
    numArray5[2] = (byte) 184;
    numArray5[10] = (byte) 17;
    numArray5[11] = (byte) 28;
    numArray5[5] = (byte) 152;
    numArray5[6] = (byte) 142;
    byte[] numArray6 = new byte[14]
    {
      (byte) 113,
      (byte) 69,
      (byte) 176 /*0xB0*/,
      (byte) 49,
      (byte) 175,
      (byte) 7,
      (byte) 56,
      (byte) 75,
      (byte) 117,
      (byte) 88,
      (byte) 35,
      (byte) 79,
      (byte) 13,
      (byte) 41
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
