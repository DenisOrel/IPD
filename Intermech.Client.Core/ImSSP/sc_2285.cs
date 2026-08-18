
// Type: ImSSP.sc_2285
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2285
{
  private static byte[] sspq = new byte[78]
  {
    (byte) 60,
    (byte) 164,
    (byte) 230,
    (byte) 6,
    (byte) 214,
    (byte) 2,
    (byte) 43,
    (byte) 132,
    (byte) 87,
    (byte) 18,
    (byte) 199,
    (byte) 233,
    (byte) 209,
    (byte) 253,
    (byte) 59,
    (byte) 154,
    (byte) 215,
    (byte) 154,
    (byte) 103,
    (byte) 150,
    (byte) 65,
    (byte) 35,
    (byte) 150,
    (byte) 148,
    (byte) 28,
    (byte) 87,
    (byte) 68,
    (byte) 226,
    (byte) 134,
    (byte) 148,
    (byte) 197,
    (byte) 113,
    (byte) 147,
    (byte) 225,
    (byte) 144 /*0x90*/,
    (byte) 28,
    (byte) 150,
    (byte) 71,
    (byte) 102,
    (byte) 35,
    (byte) 190,
    (byte) 78,
    (byte) 169,
    (byte) 131,
    (byte) 102,
    (byte) 196,
    (byte) 219,
    (byte) 212,
    (byte) 70,
    (byte) 96 /*0x60*/,
    (byte) 116,
    (byte) 139,
    (byte) 158,
    (byte) 204,
    (byte) 66,
    (byte) 205,
    (byte) 148,
    (byte) 92,
    (byte) 97,
    (byte) 90,
    (byte) 7,
    (byte) 223,
    (byte) 58,
    (byte) 25,
    (byte) 129,
    (byte) 223,
    (byte) 168,
    (byte) 131,
    (byte) 238,
    (byte) 148,
    (byte) 7,
    (byte) 108,
    (byte) 247,
    (byte) 66,
    (byte) 210,
    (byte) 138,
    (byte) 77,
    (byte) 96 /*0x60*/
  };
  private static byte[] sspr = new byte[78]
  {
    (byte) 200,
    (byte) 181,
    (byte) 171,
    (byte) 136,
    (byte) 147,
    (byte) 108,
    (byte) 39,
    (byte) 160 /*0xA0*/,
    (byte) 91,
    (byte) 242,
    (byte) 163,
    (byte) 47,
    (byte) 27,
    (byte) 47,
    (byte) 219,
    byte.MaxValue,
    (byte) 118,
    (byte) 132,
    (byte) 175,
    (byte) 223,
    (byte) 177,
    (byte) 82,
    (byte) 121,
    (byte) 201,
    (byte) 136,
    (byte) 212,
    (byte) 57,
    (byte) 13,
    (byte) 44,
    (byte) 30,
    (byte) 43,
    (byte) 120,
    (byte) 112 /*0x70*/,
    (byte) 110,
    (byte) 48 /*0x30*/,
    (byte) 71,
    (byte) 127 /*0x7F*/,
    (byte) 80 /*0x50*/,
    (byte) 60,
    (byte) 124,
    (byte) 105,
    (byte) 196,
    (byte) 29,
    (byte) 147,
    (byte) 21,
    (byte) 21,
    (byte) 59,
    (byte) 20,
    (byte) 128 /*0x80*/,
    (byte) 49,
    (byte) 209,
    (byte) 173,
    (byte) 237,
    (byte) 176 /*0xB0*/,
    (byte) 162,
    (byte) 37,
    (byte) 2,
    (byte) 190,
    (byte) 86,
    (byte) 128 /*0x80*/,
    (byte) 131,
    (byte) 32 /*0x20*/,
    (byte) 156,
    (byte) 91,
    (byte) 115,
    (byte) 205,
    (byte) 75,
    (byte) 110,
    (byte) 57,
    (byte) 152,
    (byte) 0,
    (byte) 208 /*0xD0*/,
    (byte) 99,
    (byte) 248,
    (byte) 45,
    (byte) 198,
    (byte) 216,
    (byte) 65
  };

  internal static string ssp_imclient_2286()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[26];
      byte[] numArray2 = new byte[26]
      {
        (byte) 209,
        (byte) 40,
        (byte) 202,
        (byte) 179,
        (byte) 149,
        (byte) 176 /*0xB0*/,
        (byte) 94,
        (byte) 195,
        (byte) 235,
        (byte) 7,
        (byte) 206,
        (byte) 139,
        (byte) 56,
        (byte) 27,
        (byte) 23,
        (byte) 157,
        (byte) 211,
        (byte) 173,
        (byte) 75,
        (byte) 68,
        (byte) 245,
        (byte) 77,
        (byte) 194,
        (byte) 227,
        (byte) 208 /*0xD0*/,
        (byte) 42
      };
      byte[] numArray3 = new byte[26];
      numArray3[9] = (byte) 105;
      numArray3[1] = (byte) 127 /*0x7F*/;
      numArray3[0] = (byte) 194;
      numArray3[23] = (byte) 21;
      numArray3[4] = (byte) 176 /*0xB0*/;
      numArray3[21] = (byte) 238;
      numArray3[7] = (byte) 192 /*0xC0*/;
      numArray3[19] = (byte) 154;
      numArray3[8] = (byte) 140;
      numArray3[3] = (byte) 36;
      numArray3[22] = (byte) 189;
      numArray3[11] = (byte) 188;
      numArray3[12] = (byte) 228;
      numArray3[13] = (byte) 185;
      numArray3[5] = (byte) 226;
      numArray3[14] = (byte) 165;
      numArray3[20] = (byte) 148;
      numArray3[24] = (byte) 91;
      numArray3[18] = (byte) 244;
      numArray3[10] = (byte) 229;
      numArray3[6] = (byte) 83;
      numArray3[2] = (byte) 194;
      numArray3[16 /*0x10*/] = (byte) 39;
      numArray3[17] = (byte) 139;
      numArray3[15] = (byte) 210;
      numArray3[25] = (byte) 91;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[26];
    byte[] numArray5 = new byte[26]
    {
      (byte) 131,
      (byte) 102,
      (byte) 231,
      (byte) 226,
      (byte) 106,
      (byte) 155,
      (byte) 135,
      (byte) 80 /*0x50*/,
      (byte) 100,
      (byte) 130,
      (byte) 36,
      (byte) 62,
      (byte) 118,
      (byte) 193,
      (byte) 174,
      (byte) 139,
      (byte) 109,
      (byte) 235,
      (byte) 157,
      (byte) 244,
      (byte) 188,
      (byte) 23,
      (byte) 66,
      (byte) 37,
      (byte) 126,
      (byte) 91
    };
    byte[] numArray6 = new byte[26]
    {
      (byte) 75,
      (byte) 12,
      (byte) 37,
      (byte) 179,
      (byte) 253,
      (byte) 185,
      (byte) 249,
      (byte) 27,
      (byte) 62,
      (byte) 80 /*0x50*/,
      (byte) 133,
      (byte) 24,
      (byte) 155,
      (byte) 52,
      (byte) 46,
      (byte) 49,
      (byte) 169,
      (byte) 227,
      (byte) 160 /*0xA0*/,
      (byte) 124,
      (byte) 52,
      (byte) 25,
      (byte) 23,
      (byte) 39,
      (byte) 224 /*0xE0*/,
      (byte) 13
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 26);
    for (int index = 0; index < 26; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[14];
    byte[] response = new byte[14];
    Array.Copy((Array) sc_2285.sspq, 0, (Array) numArray7, 0, 14);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2285.sspr, 0, (Array) numArray7, 0, 14);
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

  internal static string ssp_imclient_2287()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 19,
        (byte) 88,
        (byte) 80 /*0x50*/,
        (byte) 109,
        (byte) 86,
        (byte) 100,
        (byte) 93,
        (byte) 103,
        (byte) 113,
        byte.MaxValue,
        (byte) 107,
        (byte) 35,
        (byte) 194,
        (byte) 99,
        (byte) 14,
        (byte) 152,
        (byte) 204,
        (byte) 191,
        (byte) 175,
        (byte) 69,
        (byte) 127 /*0x7F*/,
        (byte) 88,
        (byte) 111,
        (byte) 235,
        (byte) 171,
        (byte) 95,
        (byte) 233
      };
      byte[] numArray3 = new byte[27];
      numArray3[2] = (byte) 7;
      numArray3[1] = (byte) 22;
      numArray3[26] = (byte) 161;
      numArray3[3] = (byte) 209;
      numArray3[5] = (byte) 182;
      numArray3[17] = (byte) 97;
      numArray3[6] = (byte) 42;
      numArray3[15] = (byte) 145;
      numArray3[9] = (byte) 190;
      numArray3[4] = (byte) 203;
      numArray3[12] = (byte) 140;
      numArray3[11] = (byte) 243;
      numArray3[19] = (byte) 226;
      numArray3[18] = (byte) 200;
      numArray3[8] = (byte) 174;
      numArray3[14] = (byte) 147;
      numArray3[16 /*0x10*/] = (byte) 218;
      numArray3[24] = (byte) 178;
      numArray3[13] = (byte) 91;
      numArray3[10] = (byte) 181;
      numArray3[20] = (byte) 150;
      numArray3[21] = (byte) 130;
      numArray3[22] = (byte) 44;
      numArray3[23] = (byte) 7;
      numArray3[0] = (byte) 28;
      numArray3[7] = (byte) 217;
      numArray3[25] = (byte) 185;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[20] = (byte) 104;
    numArray5[19] = (byte) 227;
    numArray5[2] = (byte) 247;
    numArray5[1] = (byte) 193;
    numArray5[4] = (byte) 201;
    numArray5[16 /*0x10*/] = (byte) 94;
    numArray5[5] = (byte) 220;
    numArray5[7] = (byte) 44;
    numArray5[10] = (byte) 54;
    numArray5[15] = (byte) 133;
    numArray5[3] = (byte) 123;
    numArray5[11] = (byte) 248;
    numArray5[12] = (byte) 11;
    numArray5[14] = (byte) 127 /*0x7F*/;
    numArray5[0] = (byte) 101;
    numArray5[9] = (byte) 222;
    numArray5[13] = (byte) 229;
    numArray5[17] = (byte) 102;
    numArray5[18] = (byte) 66;
    numArray5[23] = (byte) 246;
    numArray5[6] = (byte) 68;
    numArray5[21] = (byte) 127 /*0x7F*/;
    numArray5[22] = (byte) 204;
    numArray5[24] = (byte) 200;
    numArray5[26] = (byte) 16 /*0x10*/;
    numArray5[25] = (byte) 75;
    numArray5[8] = (byte) 37;
    byte[] numArray6 = new byte[27];
    numArray6[2] = (byte) 232;
    numArray6[12] = (byte) 41;
    numArray6[1] = (byte) 201;
    numArray6[3] = (byte) 129;
    numArray6[0] = (byte) 57;
    numArray6[4] = (byte) 54;
    numArray6[25] = (byte) 84;
    numArray6[22] = (byte) 22;
    numArray6[8] = (byte) 181;
    numArray6[15] = (byte) 197;
    numArray6[10] = (byte) 13;
    numArray6[11] = (byte) 65;
    numArray6[19] = (byte) 127 /*0x7F*/;
    numArray6[5] = (byte) 108;
    numArray6[14] = (byte) 233;
    numArray6[13] = (byte) 54;
    numArray6[16 /*0x10*/] = (byte) 175;
    numArray6[17] = (byte) 68;
    numArray6[9] = (byte) 246;
    numArray6[7] = (byte) 73;
    numArray6[20] = (byte) 86;
    numArray6[21] = (byte) 67;
    numArray6[18] = (byte) 39;
    numArray6[23] = (byte) 55;
    numArray6[24] = (byte) 140;
    numArray6[26] = (byte) 237;
    numArray6[6] = (byte) 43;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[13];
    byte[] response = new byte[13];
    Array.Copy((Array) sc_2285.sspq, 14, (Array) numArray7, 0, 13);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2285.sspr, 14, (Array) numArray7, 0, 13);
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

  internal static string ssp_imclient_2288()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41]
      {
        (byte) 110,
        (byte) 221,
        (byte) 76,
        (byte) 251,
        (byte) 136,
        (byte) 98,
        (byte) 57,
        byte.MaxValue,
        (byte) 245,
        (byte) 35,
        (byte) 243,
        (byte) 171,
        (byte) 42,
        (byte) 61,
        (byte) 17,
        (byte) 112 /*0x70*/,
        (byte) 80 /*0x50*/,
        (byte) 92,
        (byte) 128 /*0x80*/,
        (byte) 159,
        (byte) 21,
        (byte) 41,
        (byte) 114,
        (byte) 86,
        (byte) 50,
        (byte) 92,
        (byte) 0,
        (byte) 102,
        (byte) 186,
        (byte) 77,
        (byte) 151,
        (byte) 54,
        (byte) 245,
        (byte) 26,
        (byte) 84,
        (byte) 195,
        (byte) 193,
        (byte) 128 /*0x80*/,
        (byte) 45,
        (byte) 161,
        (byte) 39
      };
      byte[] numArray3 = new byte[41]
      {
        (byte) 159,
        (byte) 64 /*0x40*/,
        (byte) 96 /*0x60*/,
        (byte) 79,
        (byte) 251,
        (byte) 83,
        (byte) 34,
        (byte) 171,
        (byte) 225,
        (byte) 96 /*0x60*/,
        (byte) 10,
        (byte) 68,
        (byte) 9,
        (byte) 14,
        (byte) 190,
        (byte) 84,
        (byte) 32 /*0x20*/,
        (byte) 96 /*0x60*/,
        (byte) 26,
        (byte) 157,
        (byte) 17,
        (byte) 15,
        (byte) 194,
        (byte) 218,
        (byte) 167,
        (byte) 38,
        (byte) 40,
        (byte) 144 /*0x90*/,
        (byte) 94,
        (byte) 160 /*0xA0*/,
        (byte) 6,
        (byte) 106,
        (byte) 37,
        (byte) 99,
        (byte) 26,
        (byte) 127 /*0x7F*/,
        (byte) 91,
        (byte) 16 /*0x10*/,
        (byte) 160 /*0xA0*/,
        (byte) 124,
        (byte) 216
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41];
    numArray5[39] = (byte) 148;
    numArray5[1] = (byte) 187;
    numArray5[25] = (byte) 77;
    numArray5[38] = (byte) 84;
    numArray5[0] = (byte) 166;
    numArray5[33] = (byte) 189;
    numArray5[4] = (byte) 17;
    numArray5[2] = (byte) 108;
    numArray5[8] = (byte) 215;
    numArray5[9] = (byte) 164;
    numArray5[29] = (byte) 222;
    numArray5[11] = (byte) 69;
    numArray5[32 /*0x20*/] = (byte) 132;
    numArray5[13] = (byte) 246;
    numArray5[40] = (byte) 10;
    numArray5[28] = (byte) 47;
    numArray5[26] = (byte) 174;
    numArray5[23] = (byte) 15;
    numArray5[15] = (byte) 44;
    numArray5[27] = (byte) 207;
    numArray5[6] = (byte) 67;
    numArray5[19] = (byte) 167;
    numArray5[22] = (byte) 182;
    numArray5[5] = (byte) 38;
    numArray5[24] = (byte) 232;
    numArray5[10] = (byte) 154;
    numArray5[3] = (byte) 4;
    numArray5[14] = (byte) 200;
    numArray5[21] = (byte) 189;
    numArray5[17] = (byte) 211;
    numArray5[30] = (byte) 120;
    numArray5[31 /*0x1F*/] = (byte) 217;
    numArray5[35] = (byte) 118;
    numArray5[7] = (byte) 191;
    numArray5[34] = (byte) 175;
    numArray5[20] = (byte) 139;
    numArray5[36] = (byte) 240 /*0xF0*/;
    numArray5[37] = (byte) 252;
    numArray5[16 /*0x10*/] = (byte) 97;
    numArray5[12] = (byte) 107;
    numArray5[18] = (byte) 68;
    byte[] numArray6 = new byte[41]
    {
      (byte) 203,
      (byte) 70,
      (byte) 73,
      (byte) 65,
      (byte) 77,
      (byte) 210,
      (byte) 19,
      (byte) 192 /*0xC0*/,
      (byte) 236,
      (byte) 240 /*0xF0*/,
      (byte) 249,
      (byte) 79,
      (byte) 78,
      (byte) 141,
      (byte) 60,
      (byte) 203,
      (byte) 239,
      (byte) 141,
      (byte) 157,
      (byte) 217,
      (byte) 219,
      (byte) 63 /*0x3F*/,
      (byte) 216,
      (byte) 31 /*0x1F*/,
      (byte) 115,
      (byte) 218,
      (byte) 192 /*0xC0*/,
      (byte) 41,
      (byte) 104,
      (byte) 244,
      (byte) 105,
      (byte) 37,
      (byte) 29,
      (byte) 154,
      (byte) 245,
      (byte) 121,
      (byte) 119,
      (byte) 161,
      (byte) 150,
      (byte) 178,
      (byte) 189
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[51];
    byte[] response = new byte[51];
    Array.Copy((Array) sc_2285.sspq, 27, (Array) numArray7, 0, 51);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2285.sspr, 27, (Array) numArray7, 0, 51);
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

  internal static string ssp_imclient_2289()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33]
      {
        (byte) 107,
        (byte) 194,
        (byte) 80 /*0x50*/,
        (byte) 155,
        (byte) 38,
        (byte) 32 /*0x20*/,
        (byte) 58,
        (byte) 211,
        (byte) 199,
        (byte) 159,
        (byte) 195,
        (byte) 135,
        (byte) 57,
        (byte) 230,
        (byte) 11,
        (byte) 28,
        (byte) 222,
        (byte) 20,
        (byte) 144 /*0x90*/,
        (byte) 1,
        (byte) 134,
        (byte) 27,
        (byte) 87,
        (byte) 230,
        (byte) 177,
        (byte) 89,
        (byte) 8,
        (byte) 147,
        (byte) 91,
        (byte) 250,
        (byte) 183,
        (byte) 128 /*0x80*/,
        (byte) 51
      };
      byte[] numArray3 = new byte[33]
      {
        (byte) 55,
        (byte) 195,
        (byte) 116,
        (byte) 137,
        (byte) 219,
        (byte) 170,
        (byte) 107,
        (byte) 45,
        (byte) 110,
        (byte) 186,
        (byte) 171,
        (byte) 82,
        (byte) 200,
        (byte) 92,
        (byte) 44,
        (byte) 42,
        (byte) 65,
        (byte) 0,
        (byte) 154,
        (byte) 37,
        (byte) 212,
        (byte) 18,
        (byte) 188,
        (byte) 208 /*0xD0*/,
        (byte) 25,
        (byte) 25,
        (byte) 178,
        (byte) 188,
        (byte) 43,
        (byte) 208 /*0xD0*/,
        (byte) 42,
        (byte) 150,
        (byte) 172
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 183,
      (byte) 68,
      (byte) 19,
      (byte) 248,
      (byte) 71,
      (byte) 12,
      (byte) 225,
      (byte) 228,
      (byte) 43,
      (byte) 12,
      (byte) 205,
      (byte) 133,
      (byte) 236,
      (byte) 38,
      (byte) 243,
      (byte) 118,
      (byte) 188,
      (byte) 94,
      (byte) 42,
      (byte) 79,
      (byte) 106,
      (byte) 105,
      (byte) 233,
      (byte) 205,
      (byte) 23,
      (byte) 166,
      (byte) 119,
      (byte) 205,
      (byte) 231,
      (byte) 67,
      (byte) 84,
      (byte) 141,
      (byte) 240 /*0xF0*/
    };
    byte[] numArray6 = new byte[33]
    {
      (byte) 147,
      (byte) 141,
      (byte) 83,
      (byte) 86,
      (byte) 221,
      (byte) 74,
      (byte) 16 /*0x10*/,
      (byte) 2,
      (byte) 35,
      (byte) 10,
      (byte) 170,
      (byte) 111,
      (byte) 238,
      (byte) 24,
      (byte) 61,
      (byte) 174,
      (byte) 57,
      (byte) 190,
      (byte) 62,
      (byte) 240 /*0xF0*/,
      (byte) 16 /*0x10*/,
      (byte) 24,
      (byte) 129,
      (byte) 140,
      (byte) 110,
      (byte) 49,
      (byte) 62,
      (byte) 133,
      (byte) 87,
      (byte) 218,
      (byte) 86,
      (byte) 247,
      (byte) 111
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
