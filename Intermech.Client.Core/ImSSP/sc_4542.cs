
// Type: ImSSP.sc_4542
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4542
{
  private static byte[] sspq = new byte[260]
  {
    (byte) 115,
    (byte) 143,
    (byte) 46,
    (byte) 63 /*0x3F*/,
    (byte) 161,
    (byte) 95,
    (byte) 126,
    (byte) 47,
    (byte) 247,
    (byte) 164,
    (byte) 45,
    (byte) 174,
    (byte) 41,
    (byte) 244,
    (byte) 151,
    (byte) 154,
    (byte) 161,
    (byte) 173,
    (byte) 165,
    (byte) 28,
    (byte) 90,
    (byte) 88,
    (byte) 136,
    (byte) 134,
    (byte) 172,
    (byte) 51,
    (byte) 235,
    (byte) 235,
    (byte) 201,
    (byte) 206,
    (byte) 219,
    (byte) 245,
    (byte) 128 /*0x80*/,
    (byte) 246,
    (byte) 166,
    (byte) 108,
    (byte) 126,
    (byte) 130,
    (byte) 68,
    (byte) 129,
    (byte) 254,
    (byte) 161,
    (byte) 205,
    (byte) 134,
    (byte) 245,
    (byte) 67,
    (byte) 151,
    (byte) 175,
    (byte) 157,
    (byte) 187,
    (byte) 158,
    (byte) 137,
    (byte) 123,
    (byte) 4,
    (byte) 22,
    (byte) 140,
    (byte) 109,
    (byte) 6,
    (byte) 83,
    (byte) 202,
    (byte) 146,
    (byte) 124,
    (byte) 128 /*0x80*/,
    (byte) 140,
    (byte) 98,
    (byte) 86,
    (byte) 82,
    (byte) 121,
    (byte) 123,
    (byte) 224 /*0xE0*/,
    (byte) 26,
    (byte) 254,
    (byte) 120,
    (byte) 47,
    (byte) 121,
    (byte) 238,
    (byte) 249,
    (byte) 196,
    (byte) 183,
    (byte) 139,
    (byte) 128 /*0x80*/,
    (byte) 171,
    (byte) 196,
    (byte) 184,
    (byte) 169,
    (byte) 208 /*0xD0*/,
    (byte) 217,
    (byte) 167,
    (byte) 80 /*0x50*/,
    (byte) 224 /*0xE0*/,
    (byte) 4,
    (byte) 222,
    (byte) 67,
    (byte) 212,
    (byte) 124,
    (byte) 220,
    (byte) 6,
    (byte) 112 /*0x70*/,
    (byte) 234,
    (byte) 101,
    (byte) 202,
    (byte) 50,
    (byte) 180,
    (byte) 194,
    (byte) 247,
    (byte) 151,
    (byte) 13,
    (byte) 152,
    (byte) 165,
    (byte) 62,
    (byte) 197,
    (byte) 191,
    (byte) 97,
    (byte) 8,
    (byte) 130,
    (byte) 153,
    (byte) 133,
    (byte) 224 /*0xE0*/,
    (byte) 88,
    (byte) 47,
    (byte) 234,
    (byte) 161,
    (byte) 37,
    (byte) 13,
    (byte) 209,
    (byte) 47,
    (byte) 88,
    (byte) 4,
    (byte) 206,
    (byte) 57,
    (byte) 180,
    (byte) 39,
    (byte) 4,
    (byte) 71,
    (byte) 223,
    (byte) 50,
    (byte) 16 /*0x10*/,
    (byte) 24,
    (byte) 48 /*0x30*/,
    (byte) 142,
    (byte) 193,
    (byte) 92,
    (byte) 250,
    (byte) 64 /*0x40*/,
    (byte) 191,
    (byte) 93,
    (byte) 80 /*0x50*/,
    (byte) 179,
    (byte) 53,
    (byte) 202,
    (byte) 143,
    (byte) 166,
    (byte) 52,
    (byte) 79,
    (byte) 160 /*0xA0*/,
    (byte) 153,
    (byte) 17,
    (byte) 27,
    (byte) 124,
    (byte) 176 /*0xB0*/,
    (byte) 136,
    (byte) 148,
    (byte) 174,
    (byte) 34,
    (byte) 88,
    (byte) 75,
    (byte) 2,
    (byte) 33,
    (byte) 22,
    (byte) 18,
    (byte) 186,
    (byte) 163,
    (byte) 52,
    (byte) 142,
    (byte) 19,
    (byte) 160 /*0xA0*/,
    (byte) 87,
    (byte) 222,
    (byte) 176 /*0xB0*/,
    (byte) 224 /*0xE0*/,
    (byte) 184,
    (byte) 207,
    (byte) 2,
    (byte) 14,
    (byte) 59,
    (byte) 237,
    (byte) 140,
    (byte) 127 /*0x7F*/,
    (byte) 65,
    (byte) 169,
    (byte) 131,
    (byte) 21,
    (byte) 233,
    (byte) 238,
    (byte) 193,
    (byte) 150,
    (byte) 116,
    (byte) 12,
    (byte) 169,
    (byte) 79,
    (byte) 77,
    (byte) 216,
    (byte) 181,
    (byte) 129,
    (byte) 111,
    (byte) 59,
    (byte) 51,
    (byte) 182,
    (byte) 102,
    (byte) 248,
    (byte) 41,
    (byte) 139,
    (byte) 108,
    (byte) 133,
    (byte) 134,
    (byte) 97,
    (byte) 214,
    (byte) 221,
    (byte) 165,
    (byte) 36,
    (byte) 74,
    (byte) 229,
    (byte) 99,
    (byte) 100,
    (byte) 242,
    (byte) 147,
    (byte) 151,
    (byte) 214,
    (byte) 201,
    (byte) 149,
    (byte) 134,
    (byte) 242,
    (byte) 241,
    (byte) 164,
    (byte) 112 /*0x70*/,
    (byte) 82,
    (byte) 249,
    (byte) 69,
    (byte) 179,
    (byte) 28,
    (byte) 139,
    (byte) 176 /*0xB0*/,
    (byte) 148,
    (byte) 193,
    (byte) 15,
    (byte) 63 /*0x3F*/,
    (byte) 235,
    (byte) 205,
    (byte) 217,
    (byte) 124,
    (byte) 213,
    (byte) 64 /*0x40*/,
    (byte) 130,
    (byte) 244,
    (byte) 144 /*0x90*/,
    (byte) 38,
    (byte) 92,
    (byte) 17,
    (byte) 39,
    (byte) 64 /*0x40*/
  };
  private static byte[] sspr = new byte[260]
  {
    (byte) 84,
    (byte) 86,
    (byte) 99,
    (byte) 252,
    (byte) 11,
    (byte) 194,
    (byte) 150,
    (byte) 12,
    (byte) 155,
    (byte) 190,
    (byte) 178,
    (byte) 57,
    (byte) 58,
    (byte) 77,
    (byte) 139,
    (byte) 61,
    (byte) 26,
    (byte) 198,
    (byte) 142,
    (byte) 239,
    (byte) 104,
    (byte) 187,
    (byte) 233,
    (byte) 131,
    (byte) 30,
    (byte) 51,
    (byte) 41,
    (byte) 185,
    (byte) 12,
    (byte) 224 /*0xE0*/,
    (byte) 221,
    (byte) 17,
    (byte) 179,
    (byte) 109,
    (byte) 44,
    (byte) 195,
    (byte) 120,
    (byte) 107,
    (byte) 194,
    (byte) 29,
    (byte) 178,
    (byte) 234,
    (byte) 178,
    (byte) 249,
    (byte) 40,
    (byte) 183,
    (byte) 187,
    (byte) 64 /*0x40*/,
    (byte) 44,
    (byte) 232,
    (byte) 188,
    (byte) 86,
    (byte) 183,
    (byte) 14,
    (byte) 59,
    (byte) 217,
    (byte) 36,
    (byte) 171,
    (byte) 140,
    (byte) 36,
    (byte) 234,
    (byte) 81,
    (byte) 4,
    (byte) 252,
    (byte) 246,
    (byte) 157,
    (byte) 72,
    (byte) 29,
    (byte) 198,
    (byte) 251,
    (byte) 99,
    (byte) 67,
    (byte) 56,
    (byte) 18,
    (byte) 191,
    (byte) 204,
    (byte) 75,
    (byte) 4,
    (byte) 28,
    (byte) 53,
    (byte) 200,
    (byte) 249,
    (byte) 242,
    (byte) 35,
    (byte) 214,
    (byte) 201,
    (byte) 214,
    (byte) 188,
    (byte) 88,
    (byte) 15,
    (byte) 152,
    (byte) 126,
    (byte) 181,
    (byte) 56,
    (byte) 109,
    (byte) 183,
    (byte) 27,
    (byte) 118,
    (byte) 32 /*0x20*/,
    (byte) 154,
    (byte) 15,
    (byte) 163,
    (byte) 215,
    (byte) 235,
    (byte) 9,
    (byte) 238,
    (byte) 69,
    (byte) 170,
    (byte) 154,
    (byte) 40,
    (byte) 96 /*0x60*/,
    (byte) 78,
    (byte) 216,
    (byte) 159,
    (byte) 54,
    (byte) 97,
    (byte) 240 /*0xF0*/,
    (byte) 22,
    (byte) 201,
    (byte) 100,
    (byte) 111,
    (byte) 87,
    (byte) 83,
    (byte) 162,
    (byte) 244,
    (byte) 131,
    (byte) 82,
    (byte) 55,
    (byte) 128 /*0x80*/,
    (byte) 9,
    (byte) 98,
    (byte) 28,
    (byte) 125,
    (byte) 227,
    (byte) 20,
    (byte) 68,
    (byte) 180,
    (byte) 214,
    (byte) 38,
    (byte) 105,
    (byte) 184,
    (byte) 108,
    (byte) 152,
    (byte) 189,
    (byte) 229,
    (byte) 122,
    (byte) 127 /*0x7F*/,
    (byte) 111,
    (byte) 107,
    (byte) 202,
    (byte) 183,
    (byte) 252,
    (byte) 196,
    (byte) 67,
    (byte) 79,
    (byte) 139,
    (byte) 235,
    (byte) 101,
    (byte) 200,
    (byte) 9,
    (byte) 129,
    (byte) 218,
    (byte) 143,
    (byte) 109,
    (byte) 23,
    (byte) 15,
    (byte) 113,
    (byte) 48 /*0x30*/,
    (byte) 16 /*0x10*/,
    (byte) 153,
    (byte) 51,
    (byte) 182,
    (byte) 104,
    (byte) 67,
    (byte) 197,
    (byte) 127 /*0x7F*/,
    (byte) 158,
    (byte) 90,
    (byte) 149,
    (byte) 86,
    (byte) 221,
    (byte) 192 /*0xC0*/,
    (byte) 231,
    (byte) 180,
    (byte) 171,
    (byte) 173,
    (byte) 232,
    (byte) 133,
    byte.MaxValue,
    (byte) 159,
    (byte) 118,
    (byte) 16 /*0x10*/,
    (byte) 144 /*0x90*/,
    (byte) 68,
    (byte) 115,
    (byte) 183,
    (byte) 91,
    (byte) 122,
    (byte) 29,
    (byte) 217,
    (byte) 72,
    (byte) 21,
    (byte) 188,
    (byte) 173,
    (byte) 57,
    (byte) 134,
    (byte) 220,
    (byte) 109,
    (byte) 238,
    (byte) 65,
    (byte) 96 /*0x60*/,
    (byte) 78,
    (byte) 55,
    (byte) 109,
    (byte) 183,
    (byte) 188,
    (byte) 103,
    (byte) 238,
    (byte) 75,
    (byte) 45,
    (byte) 247,
    (byte) 241,
    (byte) 95,
    (byte) 195,
    (byte) 156,
    (byte) 202,
    (byte) 218,
    (byte) 201,
    (byte) 14,
    (byte) 103,
    (byte) 230,
    (byte) 39,
    (byte) 76,
    (byte) 40,
    (byte) 251,
    (byte) 181,
    (byte) 237,
    (byte) 158,
    (byte) 28,
    (byte) 58,
    (byte) 204,
    (byte) 242,
    (byte) 228,
    (byte) 192 /*0xC0*/,
    (byte) 248,
    (byte) 182,
    (byte) 67,
    (byte) 12,
    (byte) 135,
    (byte) 221,
    (byte) 161,
    (byte) 23,
    (byte) 106,
    (byte) 135,
    (byte) 25,
    (byte) 82,
    (byte) 204,
    (byte) 131,
    (byte) 165,
    (byte) 50
  };

  internal static string ssp_imclient_4543()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[8] = (byte) 69;
      numArray2[1] = (byte) 40;
      numArray2[0] = (byte) 125;
      numArray2[3] = (byte) 66;
      numArray2[4] = (byte) 178;
      numArray2[5] = (byte) 99;
      numArray2[6] = (byte) 103;
      numArray2[7] = (byte) 245;
      numArray2[10] = (byte) 54;
      numArray2[9] = (byte) 217;
      numArray2[14] = (byte) 137;
      numArray2[2] = (byte) 155;
      numArray2[13] = (byte) 85;
      numArray2[11] = (byte) 191;
      numArray2[12] = (byte) 128 /*0x80*/;
      byte[] numArray3 = new byte[15];
      numArray3[2] = (byte) 167;
      numArray3[1] = (byte) 158;
      numArray3[7] = (byte) 199;
      numArray3[13] = (byte) 164;
      numArray3[6] = (byte) 122;
      numArray3[5] = (byte) 29;
      numArray3[3] = (byte) 184;
      numArray3[0] = (byte) 72;
      numArray3[8] = (byte) 84;
      numArray3[9] = (byte) 36;
      numArray3[10] = (byte) 201;
      numArray3[11] = (byte) 241;
      numArray3[12] = (byte) 119;
      numArray3[4] = (byte) 201;
      numArray3[14] = (byte) 9;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 185,
      (byte) 20,
      (byte) 200,
      (byte) 178,
      (byte) 119,
      (byte) 52,
      (byte) 142,
      (byte) 161,
      (byte) 209,
      (byte) 226,
      (byte) 172,
      (byte) 127 /*0x7F*/,
      (byte) 1,
      (byte) 19,
      (byte) 62
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 82,
      (byte) 190,
      (byte) 96 /*0x60*/,
      (byte) 62,
      (byte) 150,
      (byte) 75,
      (byte) 203,
      (byte) 66,
      (byte) 182,
      (byte) 122,
      (byte) 169,
      (byte) 94,
      (byte) 78,
      (byte) 161,
      (byte) 182
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_4542.sspq, 0, (Array) numArray7, 0, 20);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4542.sspr, 0, (Array) numArray7, 0, 20);
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

  internal static string ssp_imclient_4544()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 126,
        (byte) 36,
        (byte) 138,
        (byte) 101,
        (byte) 206,
        (byte) 195,
        (byte) 111,
        (byte) 10,
        (byte) 124,
        (byte) 103,
        (byte) 168,
        (byte) 104,
        (byte) 76,
        (byte) 202,
        (byte) 59
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 181,
        (byte) 226,
        (byte) 26,
        (byte) 155,
        (byte) 29,
        (byte) 114,
        (byte) 15,
        (byte) 240 /*0xF0*/,
        (byte) 158,
        (byte) 52,
        (byte) 138,
        (byte) 73,
        (byte) 209,
        (byte) 253,
        (byte) 1
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
      (byte) 82,
      (byte) 248,
      (byte) 4,
      (byte) 150,
      (byte) 253,
      (byte) 163,
      (byte) 52,
      (byte) 130,
      (byte) 138,
      (byte) 106,
      (byte) 121,
      (byte) 156,
      (byte) 33,
      (byte) 219,
      (byte) 134
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 158,
      (byte) 141,
      (byte) 118,
      (byte) 221,
      (byte) 208 /*0xD0*/,
      (byte) 253,
      (byte) 226,
      (byte) 88,
      (byte) 216,
      (byte) 177,
      (byte) 221,
      (byte) 162,
      (byte) 232,
      (byte) 122,
      (byte) 225
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4545()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 217,
        (byte) 211,
        (byte) 156,
        (byte) 14,
        (byte) 37,
        (byte) 123,
        (byte) 60,
        (byte) 132,
        (byte) 69,
        (byte) 220,
        (byte) 42,
        (byte) 91,
        (byte) 15,
        (byte) 204,
        (byte) 135
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 215,
        (byte) 215,
        (byte) 221,
        (byte) 188,
        (byte) 131,
        (byte) 157,
        (byte) 125,
        (byte) 35,
        (byte) 177,
        (byte) 145,
        (byte) 69,
        (byte) 144 /*0x90*/,
        (byte) 224 /*0xE0*/,
        (byte) 8,
        (byte) 47
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
      (byte) 231,
      (byte) 38,
      (byte) 230,
      (byte) 48 /*0x30*/,
      (byte) 75,
      (byte) 57,
      (byte) 5,
      (byte) 144 /*0x90*/,
      (byte) 224 /*0xE0*/,
      (byte) 70,
      (byte) 205,
      (byte) 225,
      (byte) 153,
      (byte) 213,
      (byte) 208 /*0xD0*/
    };
    byte[] numArray6 = new byte[15];
    numArray6[8] = (byte) 113;
    numArray6[2] = (byte) 227;
    numArray6[10] = (byte) 60;
    numArray6[3] = (byte) 120;
    numArray6[9] = (byte) 5;
    numArray6[5] = (byte) 89;
    numArray6[1] = (byte) 164;
    numArray6[0] = (byte) 115;
    numArray6[14] = (byte) 35;
    numArray6[4] = (byte) 98;
    numArray6[7] = (byte) 213;
    numArray6[11] = (byte) 50;
    numArray6[12] = (byte) 247;
    numArray6[13] = (byte) 143;
    numArray6[6] = (byte) 181;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4546()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[9] = (byte) 246;
      numArray2[13] = (byte) 84;
      numArray2[4] = (byte) 135;
      numArray2[3] = (byte) 169;
      numArray2[1] = (byte) 68;
      numArray2[5] = (byte) 98;
      numArray2[6] = (byte) 9;
      numArray2[7] = (byte) 148;
      numArray2[2] = (byte) 189;
      numArray2[10] = (byte) 182;
      numArray2[0] = (byte) 13;
      numArray2[11] = (byte) 100;
      numArray2[12] = (byte) 67;
      numArray2[8] = (byte) 155;
      numArray2[14] = (byte) 139;
      byte[] numArray3 = new byte[15]
      {
        (byte) 23,
        (byte) 201,
        (byte) 247,
        (byte) 132,
        (byte) 224 /*0xE0*/,
        (byte) 0,
        (byte) 246,
        (byte) 167,
        (byte) 209,
        (byte) 141,
        (byte) 243,
        (byte) 134,
        (byte) 141,
        (byte) 10,
        (byte) 47
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[6] = (byte) 83;
    numArray5[1] = (byte) 199;
    numArray5[8] = (byte) 74;
    numArray5[0] = (byte) 77;
    numArray5[3] = (byte) 41;
    numArray5[10] = (byte) 202;
    numArray5[2] = (byte) 139;
    numArray5[7] = (byte) 224 /*0xE0*/;
    numArray5[11] = (byte) 104;
    numArray5[14] = (byte) 11;
    numArray5[5] = (byte) 204;
    numArray5[4] = (byte) 82;
    numArray5[12] = (byte) 138;
    numArray5[13] = (byte) 98;
    numArray5[9] = (byte) 128 /*0x80*/;
    byte[] numArray6 = new byte[15]
    {
      (byte) 167,
      (byte) 78,
      (byte) 235,
      (byte) 58,
      (byte) 48 /*0x30*/,
      (byte) 62,
      (byte) 21,
      (byte) 40,
      (byte) 203,
      (byte) 150,
      (byte) 171,
      (byte) 43,
      (byte) 124,
      (byte) 12,
      (byte) 150
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4547()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 246,
        (byte) 141,
        byte.MaxValue,
        (byte) 254,
        (byte) 126,
        (byte) 103,
        (byte) 73,
        (byte) 90,
        (byte) 62,
        (byte) 75,
        (byte) 155,
        (byte) 94,
        (byte) 183,
        (byte) 67,
        (byte) 205
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 22,
        (byte) 187,
        (byte) 217,
        (byte) 123,
        (byte) 183,
        (byte) 53,
        (byte) 127 /*0x7F*/,
        (byte) 120,
        (byte) 182,
        (byte) 199,
        (byte) 152,
        (byte) 89,
        (byte) 48 /*0x30*/,
        (byte) 173,
        (byte) 115
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46];
      byte[] response = new byte[46];
      Array.Copy((Array) sc_4542.sspq, 20, (Array) numArray4, 0, 46);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4542.sspr, 20, (Array) numArray4, 0, 46);
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
      (byte) 26,
      (byte) 103,
      (byte) 17,
      (byte) 214,
      (byte) 79,
      (byte) 241,
      (byte) 160 /*0xA0*/,
      (byte) 142,
      (byte) 70,
      (byte) 185,
      (byte) 139,
      (byte) 2,
      (byte) 145,
      (byte) 104,
      (byte) 155
    };
    byte[] numArray7 = new byte[15]
    {
      (byte) 48 /*0x30*/,
      (byte) 3,
      (byte) 57,
      (byte) 73,
      (byte) 49,
      (byte) 205,
      (byte) 200,
      (byte) 192 /*0xC0*/,
      (byte) 170,
      (byte) 97,
      (byte) 224 /*0xE0*/,
      (byte) 52,
      (byte) 245,
      (byte) 194,
      (byte) 21
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[10];
    byte[] response1 = new byte[10];
    Array.Copy((Array) sc_4542.sspq, 66, (Array) numArray8, 0, 10);
    key.Query(true, 348, numArray8, response1);
    Array.Copy((Array) sc_4542.sspr, 66, (Array) numArray8, 0, 10);
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

  internal static string ssp_imclient_4548()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[4] = (byte) 41;
      numArray2[2] = (byte) 35;
      numArray2[11] = (byte) 237;
      numArray2[3] = (byte) 158;
      numArray2[0] = (byte) 32 /*0x20*/;
      numArray2[5] = (byte) 48 /*0x30*/;
      numArray2[9] = (byte) 172;
      numArray2[7] = (byte) 199;
      numArray2[8] = (byte) 107;
      numArray2[12] = (byte) 189;
      numArray2[10] = (byte) 233;
      numArray2[6] = (byte) 158;
      numArray2[1] = (byte) 64 /*0x40*/;
      numArray2[13] = (byte) 92;
      numArray2[14] = (byte) 1;
      byte[] numArray3 = new byte[15];
      numArray3[0] = (byte) 68;
      numArray3[2] = (byte) 99;
      numArray3[3] = (byte) 36;
      numArray3[14] = (byte) 56;
      numArray3[4] = (byte) 39;
      numArray3[8] = (byte) 1;
      numArray3[5] = (byte) 132;
      numArray3[7] = (byte) 121;
      numArray3[6] = (byte) 157;
      numArray3[9] = (byte) 58;
      numArray3[10] = (byte) 150;
      numArray3[11] = (byte) 37;
      numArray3[12] = (byte) 172;
      numArray3[13] = (byte) 48 /*0x30*/;
      numArray3[1] = (byte) 251;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 25,
      (byte) 148,
      (byte) 3,
      (byte) 68,
      (byte) 73,
      (byte) 152,
      (byte) 83,
      (byte) 174,
      (byte) 228,
      (byte) 19,
      (byte) 193,
      (byte) 138,
      (byte) 168,
      (byte) 166,
      (byte) 238
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 2,
      (byte) 148,
      (byte) 53,
      (byte) 218,
      (byte) 81,
      (byte) 88,
      (byte) 130,
      (byte) 117,
      (byte) 31 /*0x1F*/,
      (byte) 154,
      (byte) 103,
      (byte) 83,
      (byte) 9,
      (byte) 71,
      (byte) 235
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[42];
    byte[] response = new byte[42];
    Array.Copy((Array) sc_4542.sspq, 76, (Array) numArray7, 0, 42);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4542.sspr, 76, (Array) numArray7, 0, 42);
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

  internal static string ssp_imclient_4549()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[1] = (byte) 11;
      numArray2[2] = (byte) 108;
      numArray2[10] = (byte) 230;
      numArray2[5] = (byte) 164;
      numArray2[4] = (byte) 170;
      numArray2[13] = (byte) 217;
      numArray2[3] = (byte) 112 /*0x70*/;
      numArray2[0] = (byte) 222;
      numArray2[8] = (byte) 191;
      numArray2[9] = (byte) 110;
      numArray2[6] = (byte) 200;
      numArray2[11] = (byte) 70;
      numArray2[12] = (byte) 136;
      numArray2[14] = (byte) 81;
      numArray2[7] = (byte) 171;
      byte[] numArray3 = new byte[15];
      numArray3[11] = (byte) 136;
      numArray3[1] = (byte) 79;
      numArray3[2] = (byte) 159;
      numArray3[3] = (byte) 125;
      numArray3[10] = (byte) 185;
      numArray3[6] = (byte) 183;
      numArray3[0] = (byte) 195;
      numArray3[7] = (byte) 158;
      numArray3[5] = (byte) 54;
      numArray3[9] = (byte) 139;
      numArray3[13] = (byte) 128 /*0x80*/;
      numArray3[8] = (byte) 63 /*0x3F*/;
      numArray3[12] = (byte) 194;
      numArray3[4] = (byte) 74;
      numArray3[14] = (byte) 55;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 16 /*0x10*/,
      (byte) 71,
      (byte) 52,
      (byte) 163,
      (byte) 131,
      (byte) 31 /*0x1F*/,
      (byte) 151,
      (byte) 27,
      (byte) 202,
      (byte) 106,
      (byte) 93,
      (byte) 142,
      (byte) 94,
      (byte) 76,
      (byte) 111
    };
    byte[] numArray6 = new byte[15];
    numArray6[8] = (byte) 96 /*0x60*/;
    numArray6[7] = (byte) 182;
    numArray6[6] = (byte) 140;
    numArray6[1] = (byte) 105;
    numArray6[3] = (byte) 97;
    numArray6[5] = (byte) 86;
    numArray6[0] = (byte) 73;
    numArray6[13] = (byte) 142;
    numArray6[2] = (byte) 27;
    numArray6[4] = (byte) 176 /*0xB0*/;
    numArray6[10] = (byte) 212;
    numArray6[11] = (byte) 228;
    numArray6[12] = (byte) 4;
    numArray6[9] = (byte) 196;
    numArray6[14] = (byte) 88;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4550()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 107,
        (byte) 243,
        (byte) 29,
        (byte) 64 /*0x40*/,
        (byte) 212,
        (byte) 7,
        (byte) 46,
        (byte) 239,
        (byte) 142,
        (byte) 243,
        (byte) 100,
        (byte) 123,
        (byte) 191,
        (byte) 225,
        (byte) 124
      };
      byte[] numArray3 = new byte[15];
      numArray3[3] = (byte) 28;
      numArray3[7] = (byte) 96 /*0x60*/;
      numArray3[1] = (byte) 82;
      numArray3[6] = (byte) 82;
      numArray3[4] = (byte) 121;
      numArray3[0] = (byte) 185;
      numArray3[5] = (byte) 161;
      numArray3[14] = (byte) 93;
      numArray3[8] = (byte) 152;
      numArray3[2] = (byte) 249;
      numArray3[10] = (byte) 141;
      numArray3[11] = (byte) 24;
      numArray3[12] = (byte) 186;
      numArray3[13] = (byte) 190;
      numArray3[9] = (byte) 30;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[2] = (byte) 123;
    numArray5[0] = byte.MaxValue;
    numArray5[7] = (byte) 146;
    numArray5[3] = (byte) 81;
    numArray5[13] = (byte) 35;
    numArray5[5] = (byte) 186;
    numArray5[14] = (byte) 177;
    numArray5[1] = (byte) 224 /*0xE0*/;
    numArray5[4] = (byte) 74;
    numArray5[9] = (byte) 105;
    numArray5[10] = (byte) 110;
    numArray5[11] = (byte) 39;
    numArray5[12] = (byte) 111;
    numArray5[6] = (byte) 24;
    numArray5[8] = (byte) 222;
    byte[] numArray6 = new byte[15];
    numArray6[8] = (byte) 66;
    numArray6[12] = (byte) 125;
    numArray6[7] = (byte) 62;
    numArray6[14] = (byte) 195;
    numArray6[4] = (byte) 121;
    numArray6[2] = (byte) 197;
    numArray6[3] = (byte) 74;
    numArray6[11] = (byte) 213;
    numArray6[0] = (byte) 250;
    numArray6[9] = (byte) 152;
    numArray6[10] = (byte) 149;
    numArray6[1] = (byte) 191;
    numArray6[6] = (byte) 207;
    numArray6[13] = (byte) 36;
    numArray6[5] = (byte) 148;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4551()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 200,
        (byte) 18,
        (byte) 58,
        (byte) 23,
        (byte) 0,
        (byte) 76,
        (byte) 182,
        (byte) 141,
        (byte) 108,
        (byte) 220,
        (byte) 193,
        (byte) 134,
        (byte) 156,
        (byte) 213,
        (byte) 149
      };
      byte[] numArray3 = new byte[15];
      numArray3[0] = (byte) 89;
      numArray3[1] = (byte) 128 /*0x80*/;
      numArray3[2] = (byte) 56;
      numArray3[14] = (byte) 85;
      numArray3[9] = (byte) 26;
      numArray3[4] = (byte) 31 /*0x1F*/;
      numArray3[5] = (byte) 34;
      numArray3[7] = (byte) 13;
      numArray3[8] = (byte) 4;
      numArray3[12] = (byte) 123;
      numArray3[10] = (byte) 56;
      numArray3[6] = (byte) 124;
      numArray3[3] = (byte) 191;
      numArray3[13] = (byte) 138;
      numArray3[11] = (byte) 150;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 160 /*0xA0*/,
      (byte) 244,
      (byte) 53,
      (byte) 160 /*0xA0*/,
      (byte) 180,
      (byte) 33,
      (byte) 126,
      (byte) 10,
      (byte) 115,
      (byte) 147,
      (byte) 177,
      (byte) 146,
      (byte) 239,
      (byte) 34,
      (byte) 18
    };
    byte[] numArray6 = new byte[15];
    numArray6[0] = (byte) 179;
    numArray6[1] = (byte) 57;
    numArray6[5] = (byte) 5;
    numArray6[2] = (byte) 159;
    numArray6[3] = (byte) 102;
    numArray6[14] = (byte) 210;
    numArray6[12] = (byte) 231;
    numArray6[7] = (byte) 21;
    numArray6[6] = (byte) 10;
    numArray6[9] = (byte) 129;
    numArray6[13] = (byte) 84;
    numArray6[11] = (byte) 147;
    numArray6[4] = (byte) 106;
    numArray6[8] = (byte) 224 /*0xE0*/;
    numArray6[10] = (byte) 66;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4552()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 251,
        (byte) 225,
        (byte) 95,
        (byte) 236,
        (byte) 238,
        (byte) 227,
        (byte) 37,
        (byte) 88,
        (byte) 61,
        (byte) 21,
        (byte) 86,
        (byte) 145,
        (byte) 170,
        (byte) 97,
        (byte) 104
      };
      byte[] numArray3 = new byte[15];
      numArray3[9] = (byte) 140;
      numArray3[11] = (byte) 168;
      numArray3[2] = (byte) 17;
      numArray3[14] = (byte) 151;
      numArray3[1] = (byte) 59;
      numArray3[4] = (byte) 145;
      numArray3[6] = (byte) 121;
      numArray3[7] = (byte) 184;
      numArray3[8] = (byte) 108;
      numArray3[5] = (byte) 116;
      numArray3[10] = (byte) 143;
      numArray3[3] = (byte) 25;
      numArray3[0] = (byte) 119;
      numArray3[13] = (byte) 54;
      numArray3[12] = (byte) 46;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 31 /*0x1F*/,
      (byte) 38,
      (byte) 49,
      (byte) 67,
      (byte) 52,
      (byte) 229,
      (byte) 151,
      (byte) 125,
      (byte) 194,
      (byte) 236,
      (byte) 10,
      (byte) 181,
      (byte) 132,
      (byte) 210,
      (byte) 26
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 200,
      (byte) 169,
      (byte) 229,
      (byte) 219,
      (byte) 80 /*0x50*/,
      (byte) 115,
      (byte) 64 /*0x40*/,
      (byte) 87,
      (byte) 49,
      (byte) 69,
      (byte) 119,
      (byte) 9,
      (byte) 109,
      (byte) 166,
      (byte) 153
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4553()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[8] = (byte) 62;
      numArray2[7] = (byte) 182;
      numArray2[0] = (byte) 111;
      numArray2[1] = (byte) 226;
      numArray2[4] = (byte) 12;
      numArray2[5] = (byte) 53;
      numArray2[2] = (byte) 53;
      numArray2[10] = (byte) 244;
      numArray2[13] = (byte) 141;
      numArray2[9] = (byte) 121;
      numArray2[11] = (byte) 179;
      numArray2[3] = (byte) 132;
      numArray2[12] = (byte) 85;
      numArray2[6] = (byte) 98;
      numArray2[14] = (byte) 127 /*0x7F*/;
      byte[] numArray3 = new byte[15]
      {
        (byte) 43,
        (byte) 196,
        (byte) 63 /*0x3F*/,
        (byte) 140,
        (byte) 29,
        (byte) 133,
        (byte) 16 /*0x10*/,
        (byte) 138,
        (byte) 160 /*0xA0*/,
        (byte) 0,
        (byte) 112 /*0x70*/,
        (byte) 232,
        (byte) 53,
        (byte) 132,
        (byte) 91
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_4542.sspq, 118, (Array) numArray4, 0, 43);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4542.sspr, 118, (Array) numArray4, 0, 43);
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
      (byte) 27,
      (byte) 10,
      (byte) 81,
      (byte) 22,
      (byte) 77,
      (byte) 0,
      (byte) 30,
      (byte) 170,
      (byte) 16 /*0x10*/,
      (byte) 24,
      (byte) 11,
      (byte) 127 /*0x7F*/,
      (byte) 202,
      (byte) 110,
      (byte) 141
    };
    byte[] numArray7 = new byte[15]
    {
      (byte) 67,
      (byte) 227,
      (byte) 48 /*0x30*/,
      (byte) 89,
      (byte) 121,
      (byte) 165,
      (byte) 124,
      (byte) 161,
      (byte) 134,
      (byte) 167,
      (byte) 125,
      (byte) 15,
      (byte) 40,
      (byte) 89,
      (byte) 170
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_4554()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[1] = (byte) 165;
      numArray2[3] = (byte) 3;
      numArray2[0] = (byte) 222;
      numArray2[2] = (byte) 206;
      numArray2[4] = (byte) 170;
      numArray2[8] = (byte) 166;
      numArray2[7] = (byte) 181;
      numArray2[9] = (byte) 70;
      numArray2[11] = (byte) 245;
      numArray2[6] = (byte) 243;
      numArray2[10] = (byte) 32 /*0x20*/;
      numArray2[12] = (byte) 156;
      numArray2[13] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 14;
      numArray2[14] = (byte) 5;
      byte[] numArray3 = new byte[15]
      {
        (byte) 30,
        (byte) 252,
        (byte) 30,
        (byte) 100,
        (byte) 224 /*0xE0*/,
        (byte) 191,
        (byte) 50,
        (byte) 125,
        (byte) 6,
        (byte) 26,
        (byte) 75,
        (byte) 25,
        (byte) 237,
        (byte) 252,
        (byte) 42
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[1] = (byte) 209;
    numArray5[11] = (byte) 240 /*0xF0*/;
    numArray5[9] = (byte) 119;
    numArray5[3] = (byte) 77;
    numArray5[4] = (byte) 6;
    numArray5[5] = (byte) 167;
    numArray5[6] = (byte) 8;
    numArray5[10] = (byte) 100;
    numArray5[7] = (byte) 90;
    numArray5[8] = (byte) 75;
    numArray5[13] = (byte) 111;
    numArray5[2] = (byte) 227;
    numArray5[12] = (byte) 35;
    numArray5[0] = (byte) 7;
    numArray5[14] = byte.MaxValue;
    byte[] numArray6 = new byte[15]
    {
      (byte) 240 /*0xF0*/,
      (byte) 4,
      (byte) 152,
      (byte) 82,
      (byte) 27,
      (byte) 78,
      (byte) 183,
      (byte) 52,
      (byte) 1,
      (byte) 234,
      (byte) 199,
      (byte) 211,
      (byte) 187,
      (byte) 141,
      (byte) 154
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[13];
    byte[] response = new byte[13];
    Array.Copy((Array) sc_4542.sspq, 161, (Array) numArray7, 0, 13);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4542.sspr, 161, (Array) numArray7, 0, 13);
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

  internal static string ssp_imclient_4555()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 38,
        (byte) 165,
        (byte) 24,
        (byte) 214,
        (byte) 244,
        (byte) 115,
        (byte) 240 /*0xF0*/,
        (byte) 241,
        (byte) 36,
        (byte) 71,
        (byte) 163,
        (byte) 181,
        (byte) 128 /*0x80*/,
        (byte) 33,
        (byte) 209
      };
      byte[] numArray3 = new byte[15];
      numArray3[11] = (byte) 198;
      numArray3[1] = (byte) 51;
      numArray3[13] = (byte) 37;
      numArray3[7] = (byte) 103;
      numArray3[4] = (byte) 244;
      numArray3[5] = (byte) 163;
      numArray3[3] = (byte) 62;
      numArray3[2] = (byte) 192 /*0xC0*/;
      numArray3[8] = (byte) 118;
      numArray3[9] = (byte) 52;
      numArray3[6] = (byte) 249;
      numArray3[10] = (byte) 43;
      numArray3[12] = (byte) 175;
      numArray3[0] = (byte) 127 /*0x7F*/;
      numArray3[14] = (byte) 94;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 18,
      (byte) 192 /*0xC0*/,
      (byte) 227,
      (byte) 187,
      (byte) 189,
      (byte) 94,
      (byte) 127 /*0x7F*/,
      (byte) 55,
      (byte) 163,
      (byte) 137,
      (byte) 219,
      (byte) 181,
      (byte) 150,
      (byte) 129,
      (byte) 0
    };
    byte[] numArray6 = new byte[15];
    numArray6[14] = (byte) 234;
    numArray6[3] = (byte) 231;
    numArray6[2] = (byte) 118;
    numArray6[5] = (byte) 13;
    numArray6[4] = (byte) 58;
    numArray6[6] = (byte) 146;
    numArray6[9] = (byte) 129;
    numArray6[11] = (byte) 12;
    numArray6[8] = (byte) 222;
    numArray6[7] = (byte) 116;
    numArray6[12] = (byte) 240 /*0xF0*/;
    numArray6[1] = (byte) 129;
    numArray6[0] = (byte) 148;
    numArray6[13] = (byte) 212;
    numArray6[10] = (byte) 92;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4556()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 110,
        (byte) 96 /*0x60*/,
        (byte) 237,
        (byte) 188,
        (byte) 38,
        (byte) 178,
        (byte) 125,
        (byte) 195,
        (byte) 55,
        (byte) 199,
        (byte) 199,
        (byte) 186,
        (byte) 90,
        (byte) 121,
        (byte) 152
      };
      byte[] numArray3 = new byte[15];
      numArray3[4] = (byte) 202;
      numArray3[6] = (byte) 244;
      numArray3[0] = (byte) 17;
      numArray3[1] = (byte) 29;
      numArray3[12] = (byte) 190;
      numArray3[3] = (byte) 21;
      numArray3[11] = (byte) 9;
      numArray3[9] = (byte) 106;
      numArray3[5] = (byte) 193;
      numArray3[2] = (byte) 134;
      numArray3[10] = (byte) 106;
      numArray3[8] = (byte) 202;
      numArray3[7] = (byte) 201;
      numArray3[13] = (byte) 100;
      numArray3[14] = (byte) 87;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50];
      byte[] response = new byte[50];
      Array.Copy((Array) sc_4542.sspq, 174, (Array) numArray4, 0, 50);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4542.sspr, 174, (Array) numArray4, 0, 50);
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
    numArray6[9] = (byte) 96 /*0x60*/;
    numArray6[1] = (byte) 66;
    numArray6[10] = (byte) 146;
    numArray6[2] = (byte) 162;
    numArray6[4] = (byte) 225;
    numArray6[3] = (byte) 153;
    numArray6[6] = (byte) 43;
    numArray6[7] = (byte) 74;
    numArray6[0] = (byte) 51;
    numArray6[13] = (byte) 129;
    numArray6[8] = (byte) 52;
    numArray6[5] = (byte) 125;
    numArray6[12] = (byte) 25;
    numArray6[11] = (byte) 224 /*0xE0*/;
    numArray6[14] = (byte) 158;
    byte[] numArray7 = new byte[15];
    numArray7[7] = (byte) 214;
    numArray7[8] = (byte) 158;
    numArray7[2] = (byte) 12;
    numArray7[1] = (byte) 35;
    numArray7[0] = (byte) 232;
    numArray7[10] = (byte) 186;
    numArray7[6] = (byte) 3;
    numArray7[12] = (byte) 181;
    numArray7[14] = (byte) 20;
    numArray7[9] = (byte) 96 /*0x60*/;
    numArray7[3] = (byte) 115;
    numArray7[11] = (byte) 118;
    numArray7[4] = (byte) 94;
    numArray7[13] = (byte) 205;
    numArray7[5] = (byte) 214;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_4557()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[12] = (byte) 147;
      numArray2[14] = (byte) 175;
      numArray2[1] = (byte) 106;
      numArray2[3] = (byte) 204;
      numArray2[7] = (byte) 152;
      numArray2[5] = (byte) 229;
      numArray2[10] = (byte) 129;
      numArray2[0] = (byte) 165;
      numArray2[8] = (byte) 63 /*0x3F*/;
      numArray2[9] = (byte) 49;
      numArray2[6] = (byte) 59;
      numArray2[2] = (byte) 117;
      numArray2[11] = (byte) 189;
      numArray2[13] = (byte) 227;
      numArray2[4] = (byte) 26;
      byte[] numArray3 = new byte[15];
      numArray3[5] = (byte) 130;
      numArray3[0] = (byte) 92;
      numArray3[7] = (byte) 184;
      numArray3[2] = (byte) 41;
      numArray3[4] = (byte) 171;
      numArray3[12] = (byte) 165;
      numArray3[6] = (byte) 109;
      numArray3[3] = (byte) 0;
      numArray3[8] = (byte) 97;
      numArray3[1] = (byte) 207;
      numArray3[10] = (byte) 194;
      numArray3[11] = (byte) 95;
      numArray3[9] = (byte) 245;
      numArray3[13] = (byte) 93;
      numArray3[14] = (byte) 96 /*0x60*/;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 167,
      (byte) 175,
      (byte) 199,
      (byte) 56,
      (byte) 128 /*0x80*/,
      (byte) 33,
      (byte) 204,
      (byte) 62,
      (byte) 73,
      (byte) 115,
      (byte) 156,
      (byte) 158,
      (byte) 69,
      (byte) 154,
      (byte) 250
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 124,
      (byte) 25,
      (byte) 34,
      (byte) 63 /*0x3F*/,
      (byte) 52,
      (byte) 95,
      (byte) 77,
      (byte) 185,
      (byte) 62,
      (byte) 111,
      (byte) 168,
      (byte) 82,
      (byte) 138,
      (byte) 62,
      (byte) 92
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4558()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 58,
        (byte) 34,
        (byte) 148,
        (byte) 203,
        (byte) 143,
        (byte) 18,
        (byte) 92,
        (byte) 33,
        (byte) 221,
        (byte) 13,
        (byte) 184,
        (byte) 173,
        (byte) 4,
        (byte) 66,
        (byte) 165
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 204,
        (byte) 50,
        (byte) 129,
        (byte) 212,
        (byte) 49,
        (byte) 72,
        (byte) 147,
        (byte) 76,
        (byte) 137,
        (byte) 87,
        (byte) 21,
        (byte) 81,
        (byte) 89,
        (byte) 219,
        (byte) 157
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
      (byte) 137,
      (byte) 152,
      (byte) 73,
      (byte) 140,
      (byte) 130,
      (byte) 209,
      (byte) 27,
      (byte) 254,
      (byte) 187,
      (byte) 38,
      (byte) 119,
      (byte) 153,
      (byte) 139,
      (byte) 249,
      (byte) 235
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 121,
      (byte) 233,
      (byte) 11,
      (byte) 27,
      (byte) 102,
      (byte) 119,
      (byte) 50,
      (byte) 35,
      (byte) 214,
      (byte) 61,
      (byte) 174,
      (byte) 58,
      (byte) 65,
      (byte) 3,
      (byte) 62
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4559()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 227,
        (byte) 56,
        (byte) 38,
        (byte) 35,
        (byte) 0,
        (byte) 239,
        (byte) 48 /*0x30*/,
        (byte) 144 /*0x90*/,
        (byte) 198,
        (byte) 152,
        (byte) 92,
        (byte) 160 /*0xA0*/,
        (byte) 161,
        (byte) 12,
        (byte) 37
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 202,
        (byte) 129,
        (byte) 8,
        (byte) 42,
        (byte) 239,
        (byte) 14,
        (byte) 247,
        (byte) 238,
        (byte) 69,
        (byte) 20,
        (byte) 0,
        (byte) 46,
        (byte) 197,
        (byte) 145,
        (byte) 129
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[8] = (byte) 67;
    numArray5[1] = (byte) 159;
    numArray5[5] = (byte) 87;
    numArray5[4] = (byte) 145;
    numArray5[7] = (byte) 169;
    numArray5[11] = (byte) 194;
    numArray5[6] = (byte) 53;
    numArray5[0] = (byte) 174;
    numArray5[12] = (byte) 43;
    numArray5[2] = (byte) 195;
    numArray5[3] = (byte) 176 /*0xB0*/;
    numArray5[9] = (byte) 130;
    numArray5[14] = (byte) 204;
    numArray5[10] = (byte) 191;
    numArray5[13] = (byte) 67;
    byte[] numArray6 = new byte[15]
    {
      (byte) 160 /*0xA0*/,
      (byte) 170,
      (byte) 52,
      (byte) 180,
      (byte) 161,
      (byte) 214,
      (byte) 43,
      (byte) 247,
      (byte) 220,
      (byte) 151,
      (byte) 204,
      (byte) 88,
      (byte) 201,
      (byte) 236,
      (byte) 110
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4560()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 80 /*0x50*/,
        (byte) 56,
        (byte) 79,
        (byte) 51,
        (byte) 166,
        (byte) 206,
        (byte) 238,
        (byte) 86,
        (byte) 171,
        (byte) 173,
        (byte) 208 /*0xD0*/,
        (byte) 3,
        (byte) 108,
        (byte) 25,
        (byte) 211
      };
      byte[] numArray3 = new byte[15];
      numArray3[3] = (byte) 78;
      numArray3[11] = (byte) 213;
      numArray3[8] = (byte) 120;
      numArray3[1] = (byte) 195;
      numArray3[4] = (byte) 151;
      numArray3[13] = (byte) 51;
      numArray3[9] = (byte) 79;
      numArray3[6] = (byte) 29;
      numArray3[2] = (byte) 76;
      numArray3[5] = (byte) 141;
      numArray3[10] = (byte) 16 /*0x10*/;
      numArray3[12] = (byte) 53;
      numArray3[0] = (byte) 123;
      numArray3[7] = (byte) 25;
      numArray3[14] = (byte) 8;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_4542.sspq, 224 /*0xE0*/, (Array) numArray4, 0, 36);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4542.sspr, 224 /*0xE0*/, (Array) numArray4, 0, 36);
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
      (byte) 34,
      (byte) 43,
      (byte) 234,
      (byte) 234,
      (byte) 212,
      (byte) 206,
      (byte) 32 /*0x20*/,
      (byte) 168,
      (byte) 17,
      (byte) 8,
      (byte) 48 /*0x30*/,
      (byte) 179,
      (byte) 251,
      (byte) 183,
      (byte) 43
    };
    byte[] numArray7 = new byte[15];
    numArray7[0] = (byte) 235;
    numArray7[1] = (byte) 89;
    numArray7[2] = (byte) 175;
    numArray7[3] = (byte) 230;
    numArray7[4] = (byte) 75;
    numArray7[8] = (byte) 127 /*0x7F*/;
    numArray7[5] = (byte) 128 /*0x80*/;
    numArray7[12] = (byte) 161;
    numArray7[7] = (byte) 86;
    numArray7[9] = (byte) 254;
    numArray7[10] = (byte) 52;
    numArray7[11] = (byte) 41;
    numArray7[6] = (byte) 241;
    numArray7[13] = (byte) 51;
    numArray7[14] = (byte) 88;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
