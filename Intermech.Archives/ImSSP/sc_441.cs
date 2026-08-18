// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_441
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_441
{
  private static byte[] sspq = new byte[82]
  {
    (byte) 72,
    (byte) 249,
    (byte) 197,
    (byte) 167,
    (byte) 74,
    (byte) 173,
    (byte) 43,
    (byte) 120,
    (byte) 126,
    (byte) 67,
    (byte) 86,
    (byte) 141,
    (byte) 235,
    (byte) 247,
    (byte) 47,
    (byte) 137,
    (byte) 224 /*0xE0*/,
    (byte) 220,
    (byte) 84,
    (byte) 39,
    (byte) 181,
    (byte) 130,
    (byte) 221,
    (byte) 102,
    (byte) 168,
    (byte) 172,
    (byte) 204,
    (byte) 115,
    (byte) 24,
    (byte) 61,
    (byte) 67,
    (byte) 76,
    (byte) 12,
    (byte) 199,
    (byte) 116,
    (byte) 10,
    (byte) 11,
    (byte) 150,
    (byte) 7,
    (byte) 133,
    (byte) 1,
    (byte) 160 /*0xA0*/,
    (byte) 2,
    (byte) 198,
    (byte) 59,
    (byte) 211,
    (byte) 77,
    (byte) 221,
    (byte) 172,
    (byte) 212,
    (byte) 77,
    (byte) 21,
    (byte) 80 /*0x50*/,
    (byte) 213,
    (byte) 99,
    (byte) 34,
    (byte) 165,
    (byte) 87,
    (byte) 4,
    (byte) 131,
    (byte) 165,
    (byte) 72,
    (byte) 99,
    (byte) 246,
    (byte) 152,
    (byte) 117,
    (byte) 8,
    (byte) 47,
    (byte) 209,
    (byte) 247,
    (byte) 34,
    (byte) 67,
    (byte) 159,
    (byte) 103,
    (byte) 26,
    (byte) 108,
    (byte) 45,
    (byte) 160 /*0xA0*/,
    (byte) 22,
    (byte) 193,
    (byte) 154,
    (byte) 98
  };
  private static byte[] sspr = new byte[82]
  {
    (byte) 119,
    (byte) 132,
    (byte) 135,
    (byte) 73,
    (byte) 154,
    (byte) 36,
    (byte) 112 /*0x70*/,
    (byte) 187,
    (byte) 10,
    (byte) 200,
    (byte) 112 /*0x70*/,
    (byte) 98,
    (byte) 55,
    (byte) 53,
    (byte) 167,
    (byte) 9,
    (byte) 205,
    (byte) 95,
    (byte) 148,
    (byte) 198,
    (byte) 159,
    (byte) 250,
    (byte) 202,
    (byte) 200,
    (byte) 38,
    (byte) 157,
    (byte) 3,
    (byte) 89,
    (byte) 248,
    (byte) 91,
    (byte) 239,
    (byte) 177,
    (byte) 198,
    (byte) 65,
    (byte) 127 /*0x7F*/,
    (byte) 58,
    (byte) 137,
    (byte) 48 /*0x30*/,
    (byte) 192 /*0xC0*/,
    (byte) 36,
    (byte) 225,
    (byte) 84,
    (byte) 210,
    (byte) 150,
    (byte) 130,
    (byte) 152,
    (byte) 26,
    (byte) 41,
    (byte) 137,
    (byte) 92,
    (byte) 251,
    (byte) 73,
    (byte) 75,
    (byte) 142,
    (byte) 80 /*0x50*/,
    (byte) 168,
    (byte) 166,
    (byte) 188,
    (byte) 254,
    (byte) 220,
    (byte) 199,
    (byte) 168,
    (byte) 232,
    (byte) 148,
    (byte) 152,
    (byte) 28,
    (byte) 21,
    (byte) 65,
    (byte) 134,
    (byte) 20,
    (byte) 40,
    (byte) 81,
    (byte) 179,
    (byte) 249,
    (byte) 243,
    (byte) 238,
    (byte) 140,
    (byte) 117,
    (byte) 66,
    (byte) 43,
    (byte) 254,
    (byte) 75
  };

  internal static string ssp_archives_442()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 45,
        (byte) 24,
        (byte) 129,
        (byte) 10,
        (byte) 146,
        (byte) 96 /*0x60*/,
        (byte) 56,
        (byte) 213,
        (byte) 19,
        (byte) 145,
        (byte) 159
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 139,
        (byte) 33,
        (byte) 136,
        (byte) 227,
        (byte) 5,
        (byte) 1,
        (byte) 41,
        (byte) 183,
        (byte) 141,
        (byte) 227,
        (byte) 202
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_441.sspq, 0, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_441.sspr, 0, (Array) numArray4, 0, 48 /*0x30*/);
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
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11];
    numArray6[2] = (byte) 191;
    numArray6[1] = (byte) 8;
    numArray6[8] = (byte) 28;
    numArray6[3] = (byte) 71;
    numArray6[9] = (byte) 95;
    numArray6[5] = (byte) 251;
    numArray6[6] = (byte) 166;
    numArray6[7] = (byte) 197;
    numArray6[0] = (byte) 228;
    numArray6[4] = (byte) 112 /*0x70*/;
    numArray6[10] = byte.MaxValue;
    byte[] numArray7 = new byte[11]
    {
      (byte) 99,
      (byte) 224 /*0xE0*/,
      (byte) 32 /*0x20*/,
      (byte) 136,
      (byte) 33,
      (byte) 23,
      (byte) 4,
      (byte) 76,
      (byte) 60,
      (byte) 162,
      (byte) 182
    };
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_archives_443()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[5] = (byte) 212;
      numArray2[32 /*0x20*/] = (byte) 115;
      numArray2[10] = (byte) 126;
      numArray2[2] = (byte) 127 /*0x7F*/;
      numArray2[4] = (byte) 42;
      numArray2[26] = (byte) 83;
      numArray2[17] = (byte) 171;
      numArray2[7] = (byte) 238;
      numArray2[35] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 16 /*0x10*/;
      numArray2[20] = (byte) 164;
      numArray2[14] = (byte) 33;
      numArray2[12] = (byte) 219;
      numArray2[13] = (byte) 17;
      numArray2[33] = (byte) 160 /*0xA0*/;
      numArray2[24] = (byte) 197;
      numArray2[16 /*0x10*/] = (byte) 238;
      numArray2[3] = (byte) 22;
      numArray2[8] = (byte) 143;
      numArray2[19] = (byte) 106;
      numArray2[23] = (byte) 193;
      numArray2[21] = (byte) 73;
      numArray2[18] = (byte) 151;
      numArray2[28] = (byte) 90;
      numArray2[22] = (byte) 63 /*0x3F*/;
      numArray2[25] = (byte) 49;
      numArray2[15] = (byte) 174;
      numArray2[11] = (byte) 251;
      numArray2[1] = (byte) 21;
      numArray2[6] = (byte) 95;
      numArray2[30] = (byte) 137;
      numArray2[31 /*0x1F*/] = (byte) 116;
      numArray2[0] = (byte) 37;
      numArray2[29] = (byte) 191;
      numArray2[34] = (byte) 214;
      numArray2[27] = (byte) 42;
      byte[] numArray3 = new byte[36]
      {
        (byte) 184,
        (byte) 2,
        (byte) 246,
        (byte) 113,
        (byte) 154,
        (byte) 230,
        (byte) 147,
        (byte) 64 /*0x40*/,
        (byte) 87,
        (byte) 31 /*0x1F*/,
        (byte) 110,
        (byte) 65,
        (byte) 36,
        (byte) 50,
        (byte) 60,
        (byte) 93,
        (byte) 148,
        (byte) 140,
        (byte) 193,
        (byte) 142,
        (byte) 32 /*0x20*/,
        (byte) 29,
        (byte) 192 /*0xC0*/,
        (byte) 147,
        (byte) 108,
        (byte) 57,
        (byte) 112 /*0x70*/,
        (byte) 239,
        (byte) 62,
        (byte) 157,
        (byte) 97,
        (byte) 240 /*0xF0*/,
        (byte) 59,
        (byte) 230,
        (byte) 14,
        (byte) 62
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[36];
    byte[] numArray5 = new byte[36];
    numArray5[15] = (byte) 186;
    numArray5[7] = (byte) 39;
    numArray5[2] = (byte) 240 /*0xF0*/;
    numArray5[3] = byte.MaxValue;
    numArray5[18] = (byte) 60;
    numArray5[29] = (byte) 108;
    numArray5[24] = (byte) 185;
    numArray5[1] = (byte) 32 /*0x20*/;
    numArray5[17] = (byte) 180;
    numArray5[12] = (byte) 8;
    numArray5[6] = (byte) 228;
    numArray5[11] = (byte) 193;
    numArray5[10] = (byte) 182;
    numArray5[13] = (byte) 117;
    numArray5[8] = (byte) 132;
    numArray5[14] = (byte) 251;
    numArray5[16 /*0x10*/] = (byte) 135;
    numArray5[9] = (byte) 136;
    numArray5[26] = (byte) 243;
    numArray5[30] = (byte) 78;
    numArray5[20] = (byte) 9;
    numArray5[21] = (byte) 94;
    numArray5[28] = (byte) 113;
    numArray5[5] = (byte) 3;
    numArray5[22] = (byte) 89;
    numArray5[25] = (byte) 125;
    numArray5[33] = (byte) 62;
    numArray5[27] = (byte) 52;
    numArray5[23] = (byte) 201;
    numArray5[0] = (byte) 177;
    numArray5[4] = (byte) 192 /*0xC0*/;
    numArray5[31 /*0x1F*/] = (byte) 92;
    numArray5[32 /*0x20*/] = (byte) 70;
    numArray5[19] = (byte) 170;
    numArray5[34] = (byte) 121;
    numArray5[35] = (byte) 186;
    byte[] numArray6 = new byte[36]
    {
      (byte) 177,
      (byte) 64 /*0x40*/,
      (byte) 12,
      (byte) 139,
      (byte) 120,
      (byte) 115,
      (byte) 236,
      (byte) 150,
      (byte) 245,
      (byte) 253,
      (byte) 186,
      (byte) 68,
      (byte) 225,
      (byte) 148,
      (byte) 183,
      (byte) 88,
      (byte) 133,
      (byte) 52,
      (byte) 176 /*0xB0*/,
      (byte) 73,
      (byte) 213,
      (byte) 113,
      (byte) 94,
      (byte) 154,
      (byte) 170,
      (byte) 60,
      (byte) 54,
      (byte) 199,
      (byte) 200,
      (byte) 163,
      (byte) 48 /*0x30*/,
      (byte) 62,
      (byte) 158,
      (byte) 176 /*0xB0*/,
      (byte) 195,
      (byte) 205
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_444()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13];
      numArray2[12] = (byte) 29;
      numArray2[0] = (byte) 78;
      numArray2[6] = (byte) 245;
      numArray2[3] = (byte) 113;
      numArray2[4] = (byte) 253;
      numArray2[1] = (byte) 17;
      numArray2[8] = (byte) 140;
      numArray2[5] = (byte) 45;
      numArray2[10] = (byte) 33;
      numArray2[9] = (byte) 25;
      numArray2[11] = (byte) 132;
      numArray2[2] = (byte) 249;
      numArray2[7] = (byte) 154;
      byte[] numArray3 = new byte[13]
      {
        (byte) 107,
        (byte) 39,
        (byte) 32 /*0x20*/,
        (byte) 100,
        (byte) 132,
        (byte) 117,
        (byte) 66,
        (byte) 130,
        (byte) 0,
        (byte) 20,
        (byte) 88,
        (byte) 10,
        (byte) 85
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13]
    {
      (byte) 23,
      (byte) 16 /*0x10*/,
      (byte) 242,
      (byte) 179,
      (byte) 37,
      (byte) 4,
      (byte) 182,
      (byte) 45,
      (byte) 212,
      (byte) 69,
      (byte) 227,
      (byte) 186,
      (byte) 27
    };
    byte[] numArray6 = new byte[13]
    {
      (byte) 33,
      (byte) 182,
      (byte) 164,
      (byte) 141,
      (byte) 96 /*0x60*/,
      (byte) 57,
      (byte) 100,
      (byte) 250,
      (byte) 58,
      (byte) 14,
      (byte) 220,
      (byte) 168,
      (byte) 83
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_441.sspq, 48 /*0x30*/, (Array) numArray7, 0, 23);
    key.Query(true, 336, numArray7, response);
    Array.Copy((Array) sc_441.sspr, 48 /*0x30*/, (Array) numArray7, 0, 23);
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

  internal static string ssp_archives_445()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 144 /*0x90*/,
        (byte) 163,
        (byte) 7,
        (byte) 1,
        (byte) 49,
        (byte) 59,
        (byte) 107,
        (byte) 237,
        (byte) 60,
        (byte) 214,
        (byte) 8
      };
      byte[] numArray3 = new byte[11];
      numArray3[3] = (byte) 51;
      numArray3[1] = (byte) 55;
      numArray3[2] = (byte) 145;
      numArray3[0] = (byte) 193;
      numArray3[4] = (byte) 108;
      numArray3[9] = (byte) 202;
      numArray3[6] = (byte) 85;
      numArray3[5] = (byte) 25;
      numArray3[8] = (byte) 53;
      numArray3[7] = (byte) 183;
      numArray3[10] = (byte) 140;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 195,
      (byte) 130,
      (byte) 21,
      (byte) 23,
      (byte) 9,
      (byte) 37,
      (byte) 180,
      (byte) 178,
      (byte) 17,
      (byte) 132,
      (byte) 172
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 226,
      (byte) 92,
      (byte) 251,
      (byte) 143,
      (byte) 80 /*0x50*/,
      (byte) 238,
      (byte) 169,
      (byte) 194,
      (byte) 11,
      (byte) 30,
      (byte) 235
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_446()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[7] = (byte) 172;
      numArray2[1] = (byte) 23;
      numArray2[3] = (byte) 206;
      numArray2[8] = (byte) 174;
      numArray2[6] = (byte) 70;
      numArray2[5] = (byte) 110;
      numArray2[2] = (byte) 71;
      numArray2[0] = (byte) 235;
      numArray2[10] = (byte) 159;
      numArray2[9] = (byte) 206;
      numArray2[4] = (byte) 63 /*0x3F*/;
      byte[] numArray3 = new byte[11]
      {
        (byte) 131,
        (byte) 86,
        (byte) 159,
        (byte) 91,
        (byte) 54,
        (byte) 184,
        (byte) 114,
        (byte) 239,
        (byte) 198,
        (byte) 17,
        (byte) 41
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_441.sspq, 71, (Array) numArray4, 0, 11);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_441.sspr, 71, (Array) numArray4, 0, 11);
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
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11]
    {
      (byte) 96 /*0x60*/,
      (byte) 190,
      (byte) 17,
      (byte) 125,
      (byte) 214,
      (byte) 103,
      (byte) 73,
      (byte) 106,
      (byte) 229,
      (byte) 60,
      (byte) 60
    };
    byte[] numArray7 = new byte[11];
    numArray7[0] = (byte) 167;
    numArray7[8] = (byte) 150;
    numArray7[1] = (byte) 100;
    numArray7[3] = (byte) 104;
    numArray7[4] = (byte) 43;
    numArray7[5] = (byte) 7;
    numArray7[10] = (byte) 3;
    numArray7[7] = (byte) 44;
    numArray7[6] = (byte) 2;
    numArray7[9] = (byte) 134;
    numArray7[2] = (byte) 86;
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
