// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19625
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19625
{
  private static byte[] sspq = new byte[172]
  {
    (byte) 145,
    (byte) 25,
    (byte) 13,
    (byte) 107,
    (byte) 80 /*0x50*/,
    (byte) 145,
    (byte) 246,
    (byte) 59,
    (byte) 235,
    (byte) 96 /*0x60*/,
    (byte) 90,
    (byte) 5,
    (byte) 16 /*0x10*/,
    (byte) 110,
    (byte) 84,
    (byte) 204,
    (byte) 71,
    (byte) 155,
    (byte) 184,
    (byte) 127 /*0x7F*/,
    (byte) 13,
    (byte) 160 /*0xA0*/,
    (byte) 212,
    (byte) 4,
    (byte) 84,
    (byte) 27,
    (byte) 240 /*0xF0*/,
    (byte) 165,
    (byte) 107,
    (byte) 134,
    (byte) 47,
    (byte) 165,
    (byte) 78,
    (byte) 158,
    (byte) 145,
    (byte) 240 /*0xF0*/,
    (byte) 98,
    (byte) 86,
    (byte) 208 /*0xD0*/,
    (byte) 146,
    (byte) 35,
    (byte) 220,
    (byte) 233,
    (byte) 242,
    (byte) 126,
    (byte) 179,
    (byte) 227,
    (byte) 205,
    (byte) 168,
    (byte) 78,
    (byte) 202,
    (byte) 241,
    (byte) 59,
    (byte) 49,
    (byte) 82,
    (byte) 232,
    (byte) 32 /*0x20*/,
    (byte) 81,
    (byte) 216,
    (byte) 220,
    (byte) 200,
    (byte) 123,
    (byte) 250,
    (byte) 179,
    (byte) 70,
    (byte) 150,
    (byte) 172,
    (byte) 205,
    (byte) 147,
    (byte) 47,
    (byte) 10,
    (byte) 153,
    (byte) 166,
    (byte) 117,
    (byte) 193,
    (byte) 160 /*0xA0*/,
    (byte) 170,
    (byte) 226,
    (byte) 134,
    (byte) 161,
    (byte) 22,
    (byte) 129,
    (byte) 84,
    (byte) 187,
    (byte) 15,
    (byte) 27,
    (byte) 73,
    (byte) 143,
    (byte) 186,
    (byte) 168,
    (byte) 181,
    (byte) 76,
    (byte) 218,
    (byte) 223,
    (byte) 218,
    (byte) 195,
    (byte) 218,
    (byte) 237,
    (byte) 4,
    (byte) 68,
    (byte) 226,
    (byte) 24,
    (byte) 94,
    (byte) 82,
    (byte) 211,
    (byte) 39,
    (byte) 204,
    (byte) 79,
    (byte) 147,
    (byte) 146,
    (byte) 180,
    (byte) 91,
    (byte) 28,
    (byte) 245,
    (byte) 128 /*0x80*/,
    (byte) 61,
    (byte) 126,
    (byte) 162,
    (byte) 77,
    (byte) 196,
    (byte) 217,
    (byte) 218,
    (byte) 148,
    (byte) 60,
    (byte) 52,
    (byte) 185,
    (byte) 203,
    (byte) 69,
    (byte) 13,
    (byte) 218,
    (byte) 223,
    (byte) 6,
    (byte) 115,
    (byte) 53,
    (byte) 65,
    (byte) 102,
    (byte) 42,
    (byte) 112 /*0x70*/,
    (byte) 148,
    (byte) 167,
    (byte) 56,
    (byte) 182,
    (byte) 222,
    (byte) 213,
    (byte) 224 /*0xE0*/,
    (byte) 133,
    (byte) 151,
    (byte) 58,
    (byte) 210,
    (byte) 156,
    (byte) 177,
    (byte) 159,
    (byte) 219,
    (byte) 237,
    (byte) 153,
    (byte) 166,
    (byte) 76,
    byte.MaxValue,
    (byte) 5,
    (byte) 125,
    (byte) 234,
    (byte) 129,
    (byte) 159,
    (byte) 133,
    (byte) 134,
    (byte) 125,
    (byte) 174,
    (byte) 232,
    (byte) 216,
    (byte) 246,
    (byte) 93,
    (byte) 208 /*0xD0*/
  };
  private static byte[] sspr = new byte[172]
  {
    (byte) 36,
    (byte) 218,
    (byte) 197,
    (byte) 204,
    (byte) 232,
    (byte) 79,
    (byte) 146,
    (byte) 106,
    (byte) 18,
    (byte) 157,
    (byte) 52,
    (byte) 69,
    (byte) 235,
    (byte) 181,
    (byte) 77,
    (byte) 46,
    (byte) 72,
    (byte) 247,
    (byte) 118,
    (byte) 213,
    (byte) 109,
    (byte) 142,
    (byte) 80 /*0x50*/,
    (byte) 16 /*0x10*/,
    (byte) 155,
    (byte) 147,
    (byte) 250,
    (byte) 78,
    (byte) 86,
    (byte) 232,
    (byte) 9,
    (byte) 222,
    (byte) 195,
    (byte) 81,
    (byte) 69,
    (byte) 195,
    (byte) 245,
    (byte) 177,
    (byte) 228,
    (byte) 13,
    (byte) 222,
    (byte) 54,
    (byte) 250,
    (byte) 89,
    (byte) 219,
    (byte) 223,
    (byte) 65,
    (byte) 10,
    (byte) 187,
    (byte) 207,
    (byte) 68,
    (byte) 158,
    (byte) 72,
    (byte) 47,
    (byte) 62,
    (byte) 211,
    (byte) 231,
    (byte) 171,
    (byte) 35,
    (byte) 155,
    (byte) 222,
    (byte) 190,
    (byte) 38,
    (byte) 168,
    (byte) 66,
    (byte) 98,
    (byte) 196,
    (byte) 151,
    (byte) 131,
    (byte) 168,
    (byte) 252,
    (byte) 151,
    (byte) 193,
    (byte) 121,
    (byte) 139,
    (byte) 147,
    (byte) 247,
    (byte) 144 /*0x90*/,
    (byte) 33,
    (byte) 43,
    (byte) 186,
    (byte) 16 /*0x10*/,
    (byte) 178,
    (byte) 134,
    (byte) 39,
    (byte) 102,
    (byte) 51,
    (byte) 27,
    (byte) 109,
    (byte) 60,
    (byte) 223,
    (byte) 134,
    (byte) 56,
    (byte) 115,
    (byte) 27,
    (byte) 231,
    (byte) 253,
    (byte) 83,
    (byte) 100,
    (byte) 213,
    (byte) 134,
    (byte) 203,
    (byte) 24,
    (byte) 118,
    (byte) 187,
    (byte) 217,
    (byte) 241,
    (byte) 131,
    (byte) 68,
    (byte) 32 /*0x20*/,
    (byte) 2,
    (byte) 189,
    (byte) 224 /*0xE0*/,
    (byte) 175,
    (byte) 140,
    (byte) 43,
    (byte) 85,
    (byte) 48 /*0x30*/,
    (byte) 39,
    (byte) 40,
    (byte) 158,
    (byte) 38,
    (byte) 79,
    (byte) 6,
    (byte) 4,
    (byte) 187,
    (byte) 69,
    (byte) 198,
    (byte) 26,
    (byte) 252,
    (byte) 228,
    (byte) 103,
    (byte) 44,
    (byte) 243,
    (byte) 126,
    (byte) 248,
    (byte) 4,
    (byte) 173,
    (byte) 139,
    (byte) 139,
    (byte) 46,
    (byte) 138,
    (byte) 81,
    (byte) 221,
    (byte) 251,
    (byte) 38,
    (byte) 63 /*0x3F*/,
    (byte) 239,
    (byte) 36,
    (byte) 113,
    (byte) 116,
    (byte) 85,
    (byte) 14,
    (byte) 28,
    (byte) 31 /*0x1F*/,
    (byte) 85,
    (byte) 196,
    (byte) 35,
    (byte) 37,
    (byte) 90,
    (byte) 11,
    (byte) 39,
    (byte) 71,
    (byte) 248,
    (byte) 56,
    (byte) 189,
    (byte) 133,
    (byte) 45,
    (byte) 230,
    (byte) 60,
    (byte) 197,
    (byte) 196
  };

  internal static string ssp_techcard_19626()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 206,
        (byte) 51,
        (byte) 233,
        (byte) 39,
        (byte) 155,
        (byte) 200,
        (byte) 195,
        (byte) 204,
        (byte) 248,
        (byte) 93,
        (byte) 211,
        (byte) 103,
        (byte) 66,
        (byte) 187,
        (byte) 63 /*0x3F*/,
        (byte) 76,
        (byte) 183,
        (byte) 152,
        (byte) 19
      };
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 34;
      numArray3[1] = (byte) 141;
      numArray3[11] = (byte) 68;
      numArray3[2] = (byte) 147;
      numArray3[9] = (byte) 94;
      numArray3[10] = (byte) 150;
      numArray3[17] = (byte) 214;
      numArray3[6] = (byte) 178;
      numArray3[0] = (byte) 76;
      numArray3[3] = (byte) 145;
      numArray3[15] = (byte) 1;
      numArray3[7] = (byte) 54;
      numArray3[12] = (byte) 71;
      numArray3[13] = (byte) 104;
      numArray3[14] = (byte) 232;
      numArray3[4] = (byte) 23;
      numArray3[16 /*0x10*/] = (byte) 193;
      numArray3[5] = (byte) 8;
      numArray3[18] = (byte) 46;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 27,
      (byte) 51,
      (byte) 79,
      (byte) 153,
      (byte) 141,
      (byte) 144 /*0x90*/,
      (byte) 36,
      (byte) 105,
      (byte) 141,
      (byte) 0,
      (byte) 204,
      (byte) 21,
      (byte) 36,
      (byte) 216,
      (byte) 32 /*0x20*/,
      (byte) 167,
      (byte) 116,
      (byte) 42,
      (byte) 117
    };
    byte[] numArray6 = new byte[19];
    numArray6[3] = (byte) 45;
    numArray6[0] = (byte) 7;
    numArray6[1] = (byte) 108;
    numArray6[4] = (byte) 22;
    numArray6[13] = (byte) 176 /*0xB0*/;
    numArray6[5] = (byte) 168;
    numArray6[6] = (byte) 101;
    numArray6[7] = (byte) 36;
    numArray6[10] = (byte) 213;
    numArray6[8] = (byte) 171;
    numArray6[11] = (byte) 91;
    numArray6[2] = (byte) 16 /*0x10*/;
    numArray6[12] = (byte) 251;
    numArray6[9] = (byte) 6;
    numArray6[14] = (byte) 218;
    numArray6[15] = (byte) 211;
    numArray6[16 /*0x10*/] = (byte) 17;
    numArray6[17] = (byte) 227;
    numArray6[18] = (byte) 10;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[29];
    byte[] response = new byte[29];
    Array.Copy((Array) sc_19625.sspq, 0, (Array) numArray7, 0, 29);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19625.sspr, 0, (Array) numArray7, 0, 29);
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

  internal static string ssp_techcard_19627()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 186,
        (byte) 223,
        (byte) 248,
        (byte) 77,
        (byte) 28,
        (byte) 197,
        (byte) 117,
        (byte) 231,
        (byte) 161,
        (byte) 70,
        (byte) 174,
        (byte) 32 /*0x20*/,
        (byte) 1,
        (byte) 203,
        (byte) 110,
        (byte) 187,
        (byte) 248,
        (byte) 122,
        (byte) 38
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 230,
        (byte) 227,
        (byte) 168,
        (byte) 122,
        (byte) 191,
        (byte) 185,
        (byte) 68,
        (byte) 68,
        (byte) 180,
        (byte) 88,
        byte.MaxValue,
        (byte) 113,
        (byte) 218,
        (byte) 119,
        (byte) 212,
        (byte) 109,
        (byte) 70,
        (byte) 56,
        (byte) 220
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[12] = (byte) 166;
    numArray5[18] = (byte) 86;
    numArray5[4] = (byte) 68;
    numArray5[8] = (byte) 160 /*0xA0*/;
    numArray5[0] = (byte) 221;
    numArray5[5] = (byte) 9;
    numArray5[1] = (byte) 214;
    numArray5[7] = (byte) 52;
    numArray5[3] = (byte) 241;
    numArray5[6] = (byte) 22;
    numArray5[10] = (byte) 208 /*0xD0*/;
    numArray5[2] = (byte) 121;
    numArray5[11] = (byte) 82;
    numArray5[13] = (byte) 123;
    numArray5[14] = (byte) 12;
    numArray5[15] = (byte) 195;
    numArray5[16 /*0x10*/] = (byte) 81;
    numArray5[17] = (byte) 143;
    numArray5[9] = (byte) 81;
    byte[] numArray6 = new byte[19]
    {
      (byte) 192 /*0xC0*/,
      (byte) 115,
      (byte) 22,
      (byte) 154,
      (byte) 238,
      (byte) 238,
      (byte) 91,
      (byte) 86,
      (byte) 206,
      (byte) 202,
      (byte) 236,
      (byte) 183,
      (byte) 132,
      (byte) 248,
      (byte) 248,
      (byte) 80 /*0x50*/,
      (byte) 243,
      (byte) 134,
      (byte) 208 /*0xD0*/
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19628()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 165,
        (byte) 93,
        (byte) 80 /*0x50*/,
        (byte) 175,
        (byte) 37,
        (byte) 243,
        (byte) 174,
        (byte) 53,
        (byte) 40,
        (byte) 150,
        (byte) 25,
        (byte) 52,
        (byte) 218,
        (byte) 107,
        (byte) 5,
        (byte) 140,
        (byte) 82,
        (byte) 59,
        (byte) 115
      };
      byte[] numArray3 = new byte[19];
      numArray3[18] = (byte) 212;
      numArray3[1] = (byte) 167;
      numArray3[13] = (byte) 220;
      numArray3[3] = (byte) 147;
      numArray3[5] = (byte) 80 /*0x50*/;
      numArray3[8] = (byte) 48 /*0x30*/;
      numArray3[10] = (byte) 47;
      numArray3[12] = (byte) 82;
      numArray3[0] = (byte) 230;
      numArray3[4] = (byte) 134;
      numArray3[15] = (byte) 125;
      numArray3[2] = (byte) 131;
      numArray3[6] = (byte) 191;
      numArray3[9] = (byte) 144 /*0x90*/;
      numArray3[14] = (byte) 6;
      numArray3[11] = (byte) 1;
      numArray3[16 /*0x10*/] = (byte) 95;
      numArray3[17] = (byte) 85;
      numArray3[7] = (byte) 43;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 237,
      (byte) 78,
      (byte) 233,
      (byte) 226,
      (byte) 208 /*0xD0*/,
      (byte) 11,
      (byte) 149,
      (byte) 29,
      (byte) 71,
      (byte) 216,
      (byte) 69,
      (byte) 249,
      (byte) 83,
      (byte) 91,
      (byte) 122,
      (byte) 139,
      (byte) 123,
      (byte) 96 /*0x60*/,
      (byte) 101
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 21,
      (byte) 90,
      (byte) 135,
      (byte) 68,
      (byte) 144 /*0x90*/,
      (byte) 48 /*0x30*/,
      (byte) 161,
      (byte) 168,
      (byte) 124,
      (byte) 2,
      (byte) 126,
      (byte) 189,
      (byte) 198,
      (byte) 37,
      (byte) 104,
      (byte) 21,
      (byte) 199,
      (byte) 178,
      (byte) 68
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[28];
    byte[] response = new byte[28];
    Array.Copy((Array) sc_19625.sspq, 29, (Array) numArray7, 0, 28);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19625.sspr, 29, (Array) numArray7, 0, 28);
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

  internal static string ssp_techcard_19629()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 127 /*0x7F*/,
        (byte) 41,
        (byte) 38,
        (byte) 167,
        (byte) 76,
        (byte) 98,
        (byte) 178,
        (byte) 42,
        (byte) 189,
        (byte) 247,
        (byte) 139,
        (byte) 86,
        (byte) 117,
        (byte) 238,
        (byte) 40,
        (byte) 192 /*0xC0*/,
        (byte) 55,
        (byte) 158,
        (byte) 162
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 193,
        (byte) 3,
        (byte) 226,
        (byte) 60,
        (byte) 159,
        (byte) 220,
        (byte) 178,
        (byte) 25,
        (byte) 45,
        (byte) 84,
        (byte) 14,
        (byte) 238,
        (byte) 162,
        (byte) 117,
        (byte) 229,
        (byte) 10,
        (byte) 36,
        (byte) 152,
        (byte) 213
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 149,
      (byte) 246,
      (byte) 157,
      (byte) 179,
      (byte) 188,
      (byte) 61,
      (byte) 105,
      (byte) 28,
      (byte) 76,
      (byte) 168,
      (byte) 208 /*0xD0*/,
      (byte) 211,
      (byte) 215,
      (byte) 166,
      (byte) 108,
      (byte) 123,
      (byte) 143,
      (byte) 134,
      (byte) 63 /*0x3F*/
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 197,
      (byte) 168,
      (byte) 185,
      (byte) 35,
      (byte) 166,
      (byte) 219,
      (byte) 120,
      (byte) 112 /*0x70*/,
      (byte) 132,
      (byte) 122,
      (byte) 156,
      (byte) 95,
      (byte) 209,
      (byte) 120,
      (byte) 40,
      (byte) 163,
      (byte) 49,
      (byte) 35,
      (byte) 12
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[37];
    byte[] response = new byte[37];
    Array.Copy((Array) sc_19625.sspq, 57, (Array) numArray7, 0, 37);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19625.sspr, 57, (Array) numArray7, 0, 37);
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

  internal static string ssp_techcard_19630()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[1] = (byte) 34;
      numArray2[4] = (byte) 57;
      numArray2[5] = (byte) 39;
      numArray2[3] = (byte) 98;
      numArray2[12] = (byte) 202;
      numArray2[14] = (byte) 86;
      numArray2[2] = (byte) 152;
      numArray2[13] = (byte) 145;
      numArray2[8] = (byte) 60;
      numArray2[9] = (byte) 113;
      numArray2[10] = (byte) 245;
      numArray2[11] = (byte) 224 /*0xE0*/;
      numArray2[6] = (byte) 10;
      numArray2[16 /*0x10*/] = (byte) 94;
      numArray2[0] = (byte) 237;
      numArray2[15] = (byte) 66;
      numArray2[7] = (byte) 18;
      numArray2[17] = (byte) 202;
      numArray2[18] = (byte) 54;
      byte[] numArray3 = new byte[19];
      numArray3[16 /*0x10*/] = (byte) 167;
      numArray3[1] = (byte) 82;
      numArray3[6] = (byte) 68;
      numArray3[10] = (byte) 7;
      numArray3[4] = (byte) 25;
      numArray3[3] = (byte) 229;
      numArray3[13] = (byte) 138;
      numArray3[7] = (byte) 207;
      numArray3[8] = (byte) 195;
      numArray3[15] = (byte) 216;
      numArray3[11] = (byte) 137;
      numArray3[2] = (byte) 78;
      numArray3[12] = (byte) 183;
      numArray3[0] = (byte) 221;
      numArray3[14] = (byte) 163;
      numArray3[9] = (byte) 135;
      numArray3[5] = (byte) 46;
      numArray3[17] = (byte) 23;
      numArray3[18] = (byte) 131;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[0] = (byte) 9;
    numArray5[15] = (byte) 215;
    numArray5[7] = (byte) 202;
    numArray5[1] = (byte) 247;
    numArray5[4] = (byte) 180;
    numArray5[2] = (byte) 39;
    numArray5[6] = (byte) 25;
    numArray5[12] = (byte) 125;
    numArray5[8] = (byte) 189;
    numArray5[14] = (byte) 185;
    numArray5[5] = (byte) 54;
    numArray5[11] = (byte) 222;
    numArray5[10] = (byte) 47;
    numArray5[13] = (byte) 187;
    numArray5[9] = (byte) 216;
    numArray5[18] = (byte) 139;
    numArray5[16 /*0x10*/] = (byte) 16 /*0x10*/;
    numArray5[17] = (byte) 245;
    numArray5[3] = (byte) 194;
    byte[] numArray6 = new byte[19];
    numArray6[4] = (byte) 50;
    numArray6[14] = (byte) 8;
    numArray6[2] = (byte) 5;
    numArray6[3] = (byte) 235;
    numArray6[12] = (byte) 56;
    numArray6[11] = (byte) 104;
    numArray6[6] = (byte) 241;
    numArray6[7] = (byte) 17;
    numArray6[13] = (byte) 110;
    numArray6[9] = (byte) 158;
    numArray6[5] = (byte) 191;
    numArray6[17] = (byte) 147;
    numArray6[10] = (byte) 8;
    numArray6[16 /*0x10*/] = (byte) 196;
    numArray6[0] = (byte) 249;
    numArray6[15] = (byte) 31 /*0x1F*/;
    numArray6[8] = (byte) 117;
    numArray6[1] = (byte) 152;
    numArray6[18] = (byte) 35;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19631()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[9] = (byte) 144 /*0x90*/;
      numArray2[14] = (byte) 25;
      numArray2[3] = (byte) 62;
      numArray2[0] = (byte) 109;
      numArray2[15] = (byte) 224 /*0xE0*/;
      numArray2[5] = (byte) 151;
      numArray2[6] = (byte) 28;
      numArray2[4] = (byte) 16 /*0x10*/;
      numArray2[8] = (byte) 165;
      numArray2[11] = (byte) 96 /*0x60*/;
      numArray2[10] = (byte) 187;
      numArray2[16 /*0x10*/] = (byte) 33;
      numArray2[12] = (byte) 168;
      numArray2[2] = (byte) 176 /*0xB0*/;
      numArray2[7] = (byte) 74;
      numArray2[1] = (byte) 249;
      numArray2[13] = (byte) 204;
      numArray2[17] = (byte) 77;
      numArray2[18] = (byte) 172;
      byte[] numArray3 = new byte[19];
      numArray3[7] = (byte) 33;
      numArray3[1] = (byte) 166;
      numArray3[4] = (byte) 40;
      numArray3[12] = (byte) 128 /*0x80*/;
      numArray3[3] = (byte) 106;
      numArray3[5] = (byte) 113;
      numArray3[6] = (byte) 102;
      numArray3[15] = (byte) 188;
      numArray3[10] = (byte) 71;
      numArray3[9] = (byte) 170;
      numArray3[14] = (byte) 167;
      numArray3[11] = (byte) 130;
      numArray3[18] = (byte) 38;
      numArray3[0] = (byte) 197;
      numArray3[17] = (byte) 158;
      numArray3[8] = (byte) 230;
      numArray3[16 /*0x10*/] = (byte) 69;
      numArray3[2] = (byte) 120;
      numArray3[13] = (byte) 138;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      byte[] response = new byte[37];
      Array.Copy((Array) sc_19625.sspq, 94, (Array) numArray4, 0, 37);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19625.sspr, 94, (Array) numArray4, 0, 37);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 216,
      (byte) 246,
      (byte) 74,
      (byte) 221,
      (byte) 100,
      (byte) 166,
      (byte) 182,
      (byte) 84,
      (byte) 46,
      (byte) 36,
      (byte) 243,
      (byte) 88,
      (byte) 142,
      (byte) 201,
      (byte) 254,
      (byte) 120,
      (byte) 101,
      (byte) 26,
      (byte) 156
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 198,
      (byte) 52,
      (byte) 69,
      (byte) 91,
      (byte) 176 /*0xB0*/,
      (byte) 115,
      (byte) 155,
      (byte) 3,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 130,
      (byte) 37,
      (byte) 139,
      (byte) 149,
      (byte) 47,
      (byte) 220,
      (byte) 112 /*0x70*/,
      (byte) 223,
      (byte) 70
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19632()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 107,
        (byte) 8,
        (byte) 132,
        (byte) 33,
        (byte) 30,
        (byte) 25,
        (byte) 92,
        (byte) 135,
        (byte) 65,
        (byte) 185,
        (byte) 178,
        (byte) 220,
        (byte) 101,
        (byte) 209,
        (byte) 24,
        (byte) 27,
        (byte) 150,
        (byte) 200,
        (byte) 77
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 249,
        (byte) 79,
        (byte) 39,
        (byte) 191,
        (byte) 224 /*0xE0*/,
        (byte) 248,
        (byte) 29,
        (byte) 239,
        (byte) 188,
        (byte) 233,
        (byte) 47,
        (byte) 149,
        (byte) 86,
        (byte) 232,
        (byte) 111,
        (byte) 80 /*0x50*/,
        (byte) 187,
        (byte) 101,
        (byte) 101
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_19625.sspq, 131, (Array) numArray4, 0, 41);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19625.sspr, 131, (Array) numArray4, 0, 41);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 36,
      (byte) 10,
      (byte) 163,
      (byte) 175,
      (byte) 243,
      (byte) 66,
      (byte) 58,
      (byte) 17,
      (byte) 48 /*0x30*/,
      (byte) 18,
      (byte) 75,
      (byte) 3,
      (byte) 52,
      (byte) 226,
      (byte) 42,
      (byte) 133,
      (byte) 156,
      (byte) 155,
      (byte) 25
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 206,
      (byte) 85,
      (byte) 34,
      (byte) 88,
      (byte) 66,
      (byte) 36,
      (byte) 35,
      (byte) 210,
      (byte) 89,
      (byte) 182,
      (byte) 52,
      (byte) 190,
      (byte) 78,
      (byte) 146,
      (byte) 77,
      (byte) 173,
      (byte) 233,
      (byte) 52,
      (byte) 160 /*0xA0*/
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
