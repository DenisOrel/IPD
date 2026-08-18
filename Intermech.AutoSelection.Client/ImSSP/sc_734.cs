// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_734
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_734
{
  private static byte[] sspq = new byte[240 /*0xF0*/]
  {
    (byte) 33,
    (byte) 10,
    (byte) 38,
    (byte) 57,
    (byte) 79,
    (byte) 92,
    (byte) 27,
    (byte) 82,
    (byte) 78,
    (byte) 33,
    (byte) 87,
    (byte) 253,
    (byte) 4,
    (byte) 38,
    (byte) 218,
    (byte) 46,
    (byte) 101,
    (byte) 23,
    (byte) 47,
    (byte) 189,
    byte.MaxValue,
    (byte) 137,
    (byte) 207,
    (byte) 131,
    (byte) 5,
    (byte) 109,
    (byte) 216,
    (byte) 151,
    (byte) 29,
    (byte) 223,
    (byte) 251,
    (byte) 104,
    (byte) 24,
    (byte) 183,
    (byte) 246,
    (byte) 57,
    (byte) 55,
    (byte) 203,
    (byte) 223,
    (byte) 5,
    (byte) 199,
    (byte) 195,
    (byte) 140,
    (byte) 1,
    (byte) 160 /*0xA0*/,
    (byte) 129,
    (byte) 141,
    (byte) 114,
    (byte) 39,
    (byte) 226,
    (byte) 190,
    (byte) 227,
    (byte) 196,
    (byte) 235,
    (byte) 36,
    (byte) 172,
    (byte) 153,
    (byte) 0,
    (byte) 242,
    (byte) 154,
    (byte) 107,
    (byte) 192 /*0xC0*/,
    (byte) 175,
    (byte) 3,
    (byte) 30,
    (byte) 166,
    (byte) 35,
    (byte) 204,
    (byte) 150,
    (byte) 89,
    (byte) 197,
    (byte) 156,
    (byte) 54,
    (byte) 138,
    (byte) 131,
    (byte) 159,
    (byte) 116,
    (byte) 101,
    (byte) 238,
    (byte) 64 /*0x40*/,
    (byte) 24,
    (byte) 201,
    (byte) 76,
    (byte) 175,
    (byte) 182,
    (byte) 62,
    (byte) 70,
    (byte) 126,
    (byte) 90,
    (byte) 133,
    (byte) 234,
    (byte) 31 /*0x1F*/,
    (byte) 225,
    (byte) 226,
    (byte) 101,
    (byte) 161,
    (byte) 84,
    (byte) 154,
    (byte) 196,
    (byte) 185,
    (byte) 197,
    (byte) 228,
    (byte) 115,
    (byte) 39,
    (byte) 0,
    (byte) 179,
    (byte) 185,
    (byte) 141,
    (byte) 203,
    (byte) 131,
    (byte) 107,
    (byte) 4,
    (byte) 13,
    (byte) 188,
    (byte) 218,
    (byte) 219,
    (byte) 42,
    (byte) 43,
    (byte) 233,
    (byte) 136,
    (byte) 57,
    (byte) 141,
    (byte) 170,
    (byte) 49,
    (byte) 126,
    (byte) 176 /*0xB0*/,
    (byte) 95,
    (byte) 72,
    (byte) 105,
    (byte) 61,
    (byte) 253,
    (byte) 251,
    (byte) 120,
    (byte) 166,
    (byte) 7,
    (byte) 116,
    (byte) 166,
    (byte) 89,
    (byte) 9,
    (byte) 222,
    (byte) 187,
    (byte) 155,
    (byte) 129,
    (byte) 199,
    (byte) 130,
    (byte) 117,
    (byte) 16 /*0x10*/,
    (byte) 188,
    (byte) 248,
    (byte) 42,
    (byte) 119,
    (byte) 72,
    (byte) 132,
    (byte) 219,
    (byte) 246,
    (byte) 167,
    (byte) 104,
    (byte) 21,
    (byte) 248,
    (byte) 49,
    (byte) 152,
    (byte) 9,
    (byte) 106,
    (byte) 9,
    (byte) 8,
    (byte) 21,
    (byte) 31 /*0x1F*/,
    (byte) 190,
    (byte) 22,
    (byte) 224 /*0xE0*/,
    (byte) 173,
    (byte) 177,
    (byte) 177,
    (byte) 35,
    (byte) 11,
    (byte) 109,
    (byte) 52,
    (byte) 86,
    (byte) 170,
    (byte) 172,
    (byte) 114,
    (byte) 169,
    (byte) 196,
    (byte) 16 /*0x10*/,
    (byte) 228,
    (byte) 141,
    (byte) 123,
    (byte) 124,
    (byte) 184,
    (byte) 12,
    (byte) 113,
    (byte) 89,
    (byte) 201,
    (byte) 24,
    (byte) 105,
    (byte) 222,
    (byte) 10,
    (byte) 61,
    (byte) 172,
    (byte) 238,
    (byte) 24,
    (byte) 27,
    (byte) 91,
    (byte) 46,
    (byte) 208 /*0xD0*/,
    (byte) 50,
    (byte) 145,
    (byte) 117,
    (byte) 142,
    (byte) 43,
    (byte) 4,
    (byte) 195,
    (byte) 87,
    (byte) 185,
    (byte) 214,
    (byte) 7,
    (byte) 186,
    (byte) 60,
    (byte) 50,
    (byte) 46,
    (byte) 235,
    (byte) 29,
    (byte) 98,
    (byte) 61,
    (byte) 131,
    (byte) 92,
    (byte) 170,
    (byte) 224 /*0xE0*/,
    (byte) 53,
    (byte) 212,
    (byte) 90,
    (byte) 153,
    (byte) 157,
    (byte) 123,
    (byte) 214,
    (byte) 188,
    (byte) 202,
    (byte) 60,
    (byte) 225,
    (byte) 78
  };
  private static byte[] sspr = new byte[240 /*0xF0*/]
  {
    (byte) 182,
    (byte) 89,
    (byte) 59,
    (byte) 44,
    (byte) 30,
    (byte) 216,
    (byte) 9,
    (byte) 106,
    (byte) 128 /*0x80*/,
    (byte) 201,
    (byte) 89,
    (byte) 103,
    (byte) 1,
    (byte) 198,
    (byte) 121,
    (byte) 218,
    (byte) 237,
    (byte) 7,
    (byte) 77,
    (byte) 200,
    (byte) 229,
    (byte) 196,
    (byte) 221,
    (byte) 46,
    (byte) 82,
    (byte) 171,
    (byte) 183,
    (byte) 83,
    (byte) 212,
    (byte) 53,
    (byte) 209,
    (byte) 221,
    (byte) 15,
    (byte) 65,
    (byte) 189,
    (byte) 88,
    (byte) 55,
    (byte) 108,
    (byte) 18,
    (byte) 55,
    (byte) 233,
    (byte) 36,
    (byte) 91,
    (byte) 26,
    (byte) 151,
    (byte) 7,
    (byte) 213,
    (byte) 63 /*0x3F*/,
    (byte) 43,
    (byte) 101,
    (byte) 98,
    (byte) 193,
    (byte) 11,
    (byte) 85,
    (byte) 179,
    (byte) 194,
    (byte) 212,
    (byte) 231,
    (byte) 28,
    (byte) 208 /*0xD0*/,
    (byte) 229,
    (byte) 207,
    (byte) 116,
    (byte) 182,
    (byte) 110,
    (byte) 80 /*0x50*/,
    (byte) 177,
    (byte) 117,
    (byte) 48 /*0x30*/,
    (byte) 107,
    (byte) 50,
    (byte) 142,
    (byte) 211,
    (byte) 221,
    (byte) 123,
    (byte) 98,
    (byte) 10,
    (byte) 138,
    (byte) 73,
    (byte) 185,
    (byte) 230,
    (byte) 135,
    (byte) 38,
    (byte) 179,
    (byte) 201,
    (byte) 39,
    (byte) 250,
    (byte) 252,
    (byte) 155,
    (byte) 11,
    (byte) 22,
    (byte) 73,
    (byte) 165,
    (byte) 184,
    (byte) 240 /*0xF0*/,
    (byte) 52,
    (byte) 92,
    (byte) 142,
    (byte) 206,
    (byte) 191,
    (byte) 186,
    (byte) 139,
    (byte) 7,
    (byte) 64 /*0x40*/,
    (byte) 70,
    (byte) 200,
    (byte) 29,
    (byte) 7,
    (byte) 6,
    (byte) 132,
    (byte) 219,
    (byte) 251,
    (byte) 187,
    (byte) 107,
    (byte) 120,
    (byte) 66,
    (byte) 17,
    (byte) 166,
    (byte) 214,
    (byte) 2,
    (byte) 186,
    (byte) 208 /*0xD0*/,
    (byte) 31 /*0x1F*/,
    (byte) 25,
    (byte) 48 /*0x30*/,
    (byte) 193,
    (byte) 229,
    (byte) 0,
    (byte) 6,
    (byte) 131,
    (byte) 207,
    (byte) 251,
    (byte) 159,
    (byte) 195,
    (byte) 129,
    (byte) 91,
    (byte) 195,
    (byte) 226,
    (byte) 107,
    (byte) 189,
    (byte) 91,
    (byte) 225,
    (byte) 74,
    (byte) 19,
    (byte) 100,
    (byte) 8,
    (byte) 111,
    (byte) 232,
    (byte) 136,
    (byte) 140,
    (byte) 168,
    (byte) 15,
    (byte) 107,
    (byte) 66,
    (byte) 231,
    (byte) 73,
    (byte) 201,
    (byte) 247,
    (byte) 188,
    (byte) 9,
    (byte) 165,
    (byte) 204,
    (byte) 43,
    (byte) 1,
    (byte) 76,
    (byte) 6,
    (byte) 212,
    (byte) 184,
    (byte) 135,
    (byte) 216,
    (byte) 67,
    (byte) 222,
    (byte) 4,
    (byte) 78,
    (byte) 52,
    (byte) 235,
    (byte) 241,
    (byte) 197,
    (byte) 218,
    (byte) 41,
    (byte) 173,
    (byte) 6,
    (byte) 61,
    (byte) 215,
    (byte) 11,
    (byte) 108,
    (byte) 160 /*0xA0*/,
    (byte) 48 /*0x30*/,
    (byte) 5,
    (byte) 33,
    (byte) 252,
    (byte) 108,
    (byte) 253,
    (byte) 191,
    (byte) 55,
    (byte) 155,
    (byte) 168,
    (byte) 201,
    (byte) 94,
    (byte) 194,
    (byte) 159,
    (byte) 55,
    (byte) 14,
    (byte) 181,
    (byte) 214,
    (byte) 39,
    (byte) 173,
    (byte) 250,
    (byte) 154,
    (byte) 96 /*0x60*/,
    (byte) 133,
    (byte) 83,
    (byte) 222,
    (byte) 1,
    (byte) 130,
    (byte) 149,
    (byte) 133,
    (byte) 48 /*0x30*/,
    (byte) 244,
    (byte) 31 /*0x1F*/,
    (byte) 143,
    (byte) 236,
    (byte) 191,
    (byte) 194,
    (byte) 250,
    (byte) 95,
    (byte) 231,
    (byte) 248,
    (byte) 127 /*0x7F*/,
    (byte) 226,
    (byte) 20,
    (byte) 25,
    (byte) 98,
    (byte) 88,
    byte.MaxValue,
    (byte) 249,
    (byte) 137,
    (byte) 249,
    (byte) 144 /*0x90*/,
    (byte) 129
  };

  internal static string ssp_automatch_735()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[9] = (byte) 171;
      numArray2[1] = (byte) 21;
      numArray2[15] = (byte) 74;
      numArray2[0] = (byte) 236;
      numArray2[22] = (byte) 42;
      numArray2[10] = (byte) 146;
      numArray2[6] = (byte) 195;
      numArray2[7] = (byte) 194;
      numArray2[8] = (byte) 55;
      numArray2[4] = (byte) 141;
      numArray2[18] = (byte) 144 /*0x90*/;
      numArray2[11] = (byte) 46;
      numArray2[12] = (byte) 40;
      numArray2[21] = (byte) 34;
      numArray2[14] = (byte) 122;
      numArray2[3] = (byte) 95;
      numArray2[2] = (byte) 222;
      numArray2[17] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 185;
      numArray2[19] = (byte) 55;
      numArray2[20] = (byte) 226;
      numArray2[5] = (byte) 46;
      numArray2[13] = (byte) 206;
      byte[] numArray3 = new byte[23]
      {
        (byte) 175,
        (byte) 244,
        (byte) 79,
        (byte) 81,
        (byte) 216,
        (byte) 4,
        (byte) 2,
        (byte) 53,
        (byte) 104,
        (byte) 45,
        (byte) 78,
        (byte) 162,
        (byte) 98,
        (byte) 158,
        (byte) 5,
        (byte) 170,
        (byte) 0,
        (byte) 1,
        (byte) 110,
        (byte) 25,
        (byte) 102,
        (byte) 219,
        (byte) 3
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 206,
      (byte) 180,
      (byte) 127 /*0x7F*/,
      (byte) 81,
      (byte) 24,
      (byte) 180,
      (byte) 202,
      (byte) 234,
      (byte) 62,
      (byte) 30,
      (byte) 140,
      (byte) 106,
      (byte) 70,
      (byte) 145,
      (byte) 160 /*0xA0*/,
      (byte) 21,
      (byte) 101,
      (byte) 242,
      (byte) 123,
      (byte) 133,
      (byte) 254,
      (byte) 113,
      (byte) 153
    };
    byte[] numArray6 = new byte[23];
    numArray6[2] = byte.MaxValue;
    numArray6[6] = (byte) 70;
    numArray6[3] = (byte) 44;
    numArray6[15] = (byte) 43;
    numArray6[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray6[1] = (byte) 108;
    numArray6[12] = (byte) 160 /*0xA0*/;
    numArray6[0] = (byte) 127 /*0x7F*/;
    numArray6[21] = (byte) 252;
    numArray6[9] = (byte) 8;
    numArray6[4] = (byte) 163;
    numArray6[11] = (byte) 228;
    numArray6[7] = (byte) 230;
    numArray6[13] = (byte) 82;
    numArray6[14] = (byte) 7;
    numArray6[8] = (byte) 134;
    numArray6[17] = (byte) 63 /*0x3F*/;
    numArray6[5] = (byte) 4;
    numArray6[18] = (byte) 113;
    numArray6[19] = (byte) 125;
    numArray6[20] = (byte) 249;
    numArray6[10] = (byte) 87;
    numArray6[22] = (byte) 79;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[53];
    byte[] response = new byte[53];
    Array.Copy((Array) sc_734.sspq, 0, (Array) numArray7, 0, 53);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_734.sspr, 0, (Array) numArray7, 0, 53);
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

  internal static string ssp_automatch_736()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[15] = (byte) 160 /*0xA0*/;
      numArray2[1] = (byte) 151;
      numArray2[2] = (byte) 58;
      numArray2[17] = (byte) 153;
      numArray2[9] = (byte) 111;
      numArray2[5] = (byte) 226;
      numArray2[11] = (byte) 239;
      numArray2[6] = (byte) 132;
      numArray2[18] = (byte) 55;
      numArray2[13] = (byte) 77;
      numArray2[10] = (byte) 38;
      numArray2[7] = (byte) 131;
      numArray2[3] = (byte) 163;
      numArray2[8] = (byte) 53;
      numArray2[14] = (byte) 47;
      numArray2[0] = (byte) 46;
      numArray2[20] = (byte) 246;
      numArray2[12] = (byte) 229;
      numArray2[16 /*0x10*/] = (byte) 246;
      numArray2[19] = (byte) 42;
      numArray2[4] = (byte) 52;
      numArray2[21] = (byte) 60;
      numArray2[22] = (byte) 253;
      byte[] numArray3 = new byte[23]
      {
        (byte) 28,
        (byte) 195,
        (byte) 51,
        (byte) 246,
        (byte) 12,
        (byte) 165,
        (byte) 9,
        (byte) 253,
        (byte) 4,
        (byte) 117,
        (byte) 161,
        (byte) 200,
        (byte) 112 /*0x70*/,
        (byte) 140,
        (byte) 161,
        (byte) 94,
        (byte) 101,
        (byte) 102,
        (byte) 208 /*0xD0*/,
        (byte) 227,
        (byte) 18,
        (byte) 97,
        (byte) 76
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_734.sspq, 53, (Array) numArray4, 0, 12);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_734.sspr, 53, (Array) numArray4, 0, 12);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23];
    numArray6[2] = (byte) 78;
    numArray6[5] = (byte) 195;
    numArray6[9] = (byte) 97;
    numArray6[17] = (byte) 48 /*0x30*/;
    numArray6[13] = (byte) 178;
    numArray6[0] = (byte) 19;
    numArray6[6] = (byte) 196;
    numArray6[7] = (byte) 209;
    numArray6[8] = (byte) 160 /*0xA0*/;
    numArray6[22] = (byte) 79;
    numArray6[20] = (byte) 34;
    numArray6[11] = (byte) 59;
    numArray6[12] = (byte) 170;
    numArray6[10] = (byte) 2;
    numArray6[14] = (byte) 175;
    numArray6[15] = (byte) 223;
    numArray6[16 /*0x10*/] = (byte) 243;
    numArray6[3] = (byte) 15;
    numArray6[1] = (byte) 250;
    numArray6[19] = (byte) 159;
    numArray6[4] = (byte) 74;
    numArray6[21] = (byte) 233;
    numArray6[18] = (byte) 231;
    byte[] numArray7 = new byte[23];
    numArray7[8] = (byte) 151;
    numArray7[5] = (byte) 11;
    numArray7[6] = (byte) 169;
    numArray7[15] = (byte) 253;
    numArray7[11] = (byte) 229;
    numArray7[17] = (byte) 147;
    numArray7[19] = (byte) 66;
    numArray7[18] = (byte) 77;
    numArray7[22] = (byte) 214;
    numArray7[21] = (byte) 246;
    numArray7[0] = (byte) 168;
    numArray7[2] = (byte) 123;
    numArray7[12] = (byte) 50;
    numArray7[13] = (byte) 250;
    numArray7[10] = (byte) 104;
    numArray7[3] = (byte) 26;
    numArray7[1] = (byte) 204;
    numArray7[9] = (byte) 132;
    numArray7[7] = (byte) 89;
    numArray7[14] = (byte) 174;
    numArray7[20] = (byte) 54;
    numArray7[16 /*0x10*/] = (byte) 65;
    numArray7[4] = (byte) 238;
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_automatch_737()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[1] = (byte) 35;
      numArray2[17] = (byte) 230;
      numArray2[2] = (byte) 177;
      numArray2[22] = (byte) 76;
      numArray2[4] = (byte) 196;
      numArray2[10] = (byte) 8;
      numArray2[9] = (byte) 83;
      numArray2[7] = (byte) 27;
      numArray2[0] = (byte) 14;
      numArray2[3] = (byte) 2;
      numArray2[11] = (byte) 28;
      numArray2[15] = (byte) 102;
      numArray2[12] = (byte) 67;
      numArray2[14] = (byte) 148;
      numArray2[13] = (byte) 31 /*0x1F*/;
      numArray2[19] = (byte) 39;
      numArray2[16 /*0x10*/] = (byte) 6;
      numArray2[18] = (byte) 126;
      numArray2[6] = (byte) 250;
      numArray2[5] = (byte) 218;
      numArray2[20] = (byte) 39;
      numArray2[21] = (byte) 53;
      numArray2[8] = (byte) 210;
      byte[] numArray3 = new byte[23]
      {
        (byte) 80 /*0x50*/,
        (byte) 219,
        (byte) 142,
        (byte) 243,
        (byte) 72,
        (byte) 130,
        (byte) 192 /*0xC0*/,
        (byte) 230,
        (byte) 156,
        (byte) 12,
        (byte) 22,
        (byte) 152,
        (byte) 156,
        (byte) 43,
        (byte) 109,
        (byte) 247,
        (byte) 129,
        (byte) 175,
        (byte) 155,
        (byte) 78,
        (byte) 199,
        (byte) 64 /*0x40*/,
        (byte) 226
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 225,
      (byte) 123,
      (byte) 54,
      (byte) 146,
      (byte) 1,
      (byte) 163,
      (byte) 217,
      (byte) 214,
      (byte) 77,
      (byte) 41,
      (byte) 201,
      (byte) 212,
      (byte) 204,
      (byte) 116,
      (byte) 6,
      (byte) 46,
      (byte) 94,
      (byte) 205,
      (byte) 125,
      (byte) 101,
      (byte) 84,
      (byte) 28,
      (byte) 223
    };
    byte[] numArray6 = new byte[23];
    numArray6[12] = (byte) 98;
    numArray6[0] = (byte) 64 /*0x40*/;
    numArray6[1] = (byte) 144 /*0x90*/;
    numArray6[3] = (byte) 45;
    numArray6[4] = (byte) 131;
    numArray6[5] = (byte) 110;
    numArray6[7] = (byte) 44;
    numArray6[10] = (byte) 133;
    numArray6[8] = (byte) 226;
    numArray6[9] = (byte) 36;
    numArray6[16 /*0x10*/] = (byte) 82;
    numArray6[19] = (byte) 243;
    numArray6[21] = (byte) 52;
    numArray6[14] = (byte) 7;
    numArray6[11] = (byte) 250;
    numArray6[15] = (byte) 29;
    numArray6[6] = (byte) 14;
    numArray6[2] = (byte) 97;
    numArray6[18] = (byte) 170;
    numArray6[17] = (byte) 212;
    numArray6[20] = (byte) 108;
    numArray6[13] = (byte) 137;
    numArray6[22] = (byte) 176 /*0xB0*/;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_734.sspq, 65, (Array) numArray7, 0, 49);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_734.sspr, 65, (Array) numArray7, 0, 49);
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

  internal static string ssp_automatch_738()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[22] = (byte) 36;
      numArray2[1] = (byte) 144 /*0x90*/;
      numArray2[2] = byte.MaxValue;
      numArray2[17] = (byte) 160 /*0xA0*/;
      numArray2[4] = (byte) 109;
      numArray2[5] = (byte) 95;
      numArray2[12] = (byte) 83;
      numArray2[7] = (byte) 113;
      numArray2[8] = (byte) 17;
      numArray2[13] = (byte) 36;
      numArray2[19] = (byte) 123;
      numArray2[11] = (byte) 187;
      numArray2[3] = (byte) 19;
      numArray2[6] = (byte) 164;
      numArray2[14] = (byte) 225;
      numArray2[15] = (byte) 75;
      numArray2[18] = (byte) 53;
      numArray2[16 /*0x10*/] = (byte) 217;
      numArray2[0] = (byte) 216;
      numArray2[20] = (byte) 51;
      numArray2[10] = (byte) 165;
      numArray2[21] = (byte) 85;
      numArray2[9] = (byte) 110;
      byte[] numArray3 = new byte[23]
      {
        (byte) 113,
        (byte) 239,
        (byte) 99,
        (byte) 76,
        (byte) 83,
        (byte) 31 /*0x1F*/,
        (byte) 180,
        (byte) 74,
        (byte) 222,
        (byte) 209,
        (byte) 61,
        (byte) 238,
        (byte) 108,
        (byte) 194,
        (byte) 188,
        (byte) 84,
        (byte) 3,
        (byte) 92,
        (byte) 235,
        (byte) 254,
        (byte) 215,
        (byte) 244,
        (byte) 233
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[8] = (byte) 80 /*0x50*/;
    numArray5[19] = (byte) 152;
    numArray5[17] = (byte) 150;
    numArray5[3] = (byte) 216;
    numArray5[4] = (byte) 65;
    numArray5[5] = (byte) 209;
    numArray5[6] = (byte) 131;
    numArray5[11] = (byte) 167;
    numArray5[13] = (byte) 154;
    numArray5[9] = (byte) 15;
    numArray5[0] = (byte) 55;
    numArray5[10] = (byte) 232;
    numArray5[14] = (byte) 248;
    numArray5[7] = (byte) 107;
    numArray5[1] = (byte) 145;
    numArray5[15] = (byte) 135;
    numArray5[16 /*0x10*/] = (byte) 147;
    numArray5[12] = (byte) 240 /*0xF0*/;
    numArray5[18] = (byte) 118;
    numArray5[2] = (byte) 99;
    numArray5[20] = (byte) 208 /*0xD0*/;
    numArray5[21] = (byte) 246;
    numArray5[22] = (byte) 82;
    byte[] numArray6 = new byte[23]
    {
      (byte) 124,
      (byte) 215,
      (byte) 14,
      (byte) 173,
      (byte) 212,
      (byte) 173,
      (byte) 166,
      (byte) 106,
      (byte) 59,
      (byte) 192 /*0xC0*/,
      (byte) 225,
      (byte) 242,
      (byte) 180,
      (byte) 146,
      (byte) 77,
      (byte) 133,
      (byte) 3,
      (byte) 22,
      (byte) 157,
      (byte) 57,
      (byte) 186,
      (byte) 162,
      (byte) 228
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_739()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 198,
        (byte) 97,
        (byte) 238,
        (byte) 208 /*0xD0*/,
        (byte) 25,
        (byte) 64 /*0x40*/,
        (byte) 63 /*0x3F*/,
        (byte) 25,
        (byte) 251,
        (byte) 230,
        (byte) 16 /*0x10*/,
        (byte) 144 /*0x90*/,
        byte.MaxValue,
        (byte) 252,
        (byte) 3,
        (byte) 200,
        (byte) 88,
        (byte) 38,
        (byte) 249,
        (byte) 112 /*0x70*/,
        (byte) 4,
        (byte) 90,
        (byte) 178
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 225,
        (byte) 129,
        (byte) 148,
        (byte) 196,
        (byte) 136,
        (byte) 53,
        (byte) 38,
        (byte) 237,
        (byte) 129,
        (byte) 123,
        (byte) 198,
        (byte) 82,
        (byte) 159,
        (byte) 69,
        (byte) 203,
        (byte) 61,
        (byte) 27,
        (byte) 117,
        (byte) 231,
        (byte) 34,
        (byte) 63 /*0x3F*/,
        (byte) 146,
        (byte) 165
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_734.sspq, 114, (Array) numArray4, 0, 26);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_734.sspr, 114, (Array) numArray4, 0, 26);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23];
    numArray6[10] = (byte) 208 /*0xD0*/;
    numArray6[1] = (byte) 45;
    numArray6[8] = (byte) 101;
    numArray6[6] = (byte) 239;
    numArray6[12] = (byte) 38;
    numArray6[5] = (byte) 65;
    numArray6[3] = (byte) 174;
    numArray6[7] = (byte) 77;
    numArray6[22] = (byte) 155;
    numArray6[14] = (byte) 197;
    numArray6[17] = (byte) 151;
    numArray6[11] = (byte) 186;
    numArray6[2] = (byte) 108;
    numArray6[13] = (byte) 7;
    numArray6[19] = (byte) 117;
    numArray6[15] = (byte) 110;
    numArray6[16 /*0x10*/] = (byte) 88;
    numArray6[0] = (byte) 249;
    numArray6[18] = (byte) 218;
    numArray6[4] = (byte) 131;
    numArray6[20] = (byte) 154;
    numArray6[21] = (byte) 40;
    numArray6[9] = (byte) 62;
    byte[] numArray7 = new byte[23]
    {
      (byte) 217,
      (byte) 214,
      (byte) 236,
      byte.MaxValue,
      (byte) 13,
      (byte) 67,
      (byte) 21,
      (byte) 171,
      (byte) 100,
      (byte) 176 /*0xB0*/,
      (byte) 169,
      (byte) 186,
      (byte) 95,
      (byte) 170,
      (byte) 127 /*0x7F*/,
      (byte) 109,
      (byte) 38,
      (byte) 212,
      (byte) 3,
      (byte) 6,
      (byte) 63 /*0x3F*/,
      (byte) 88,
      (byte) 84
    };
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_automatch_740()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[1] = (byte) 14;
      numArray2[20] = (byte) 77;
      numArray2[22] = (byte) 233;
      numArray2[16 /*0x10*/] = (byte) 152;
      numArray2[3] = (byte) 147;
      numArray2[5] = (byte) 209;
      numArray2[9] = (byte) 19;
      numArray2[7] = (byte) 79;
      numArray2[8] = (byte) 248;
      numArray2[0] = (byte) 27;
      numArray2[4] = (byte) 67;
      numArray2[2] = (byte) 126;
      numArray2[12] = (byte) 97;
      numArray2[14] = (byte) 8;
      numArray2[11] = (byte) 108;
      numArray2[15] = byte.MaxValue;
      numArray2[13] = (byte) 0;
      numArray2[17] = (byte) 27;
      numArray2[18] = (byte) 120;
      numArray2[6] = (byte) 146;
      numArray2[10] = (byte) 43;
      numArray2[21] = (byte) 92;
      numArray2[19] = (byte) 185;
      byte[] numArray3 = new byte[23]
      {
        (byte) 107,
        (byte) 231,
        (byte) 146,
        (byte) 117,
        (byte) 203,
        (byte) 99,
        (byte) 159,
        (byte) 150,
        (byte) 109,
        (byte) 155,
        (byte) 93,
        (byte) 142,
        (byte) 74,
        (byte) 54,
        (byte) 162,
        (byte) 182,
        (byte) 240 /*0xF0*/,
        (byte) 177,
        (byte) 6,
        (byte) 240 /*0xF0*/,
        (byte) 31 /*0x1F*/,
        (byte) 52,
        (byte) 254
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[5] = (byte) 74;
    numArray5[1] = (byte) 169;
    numArray5[2] = (byte) 184;
    numArray5[7] = (byte) 37;
    numArray5[0] = (byte) 109;
    numArray5[22] = (byte) 10;
    numArray5[6] = (byte) 191;
    numArray5[12] = (byte) 34;
    numArray5[8] = (byte) 225;
    numArray5[15] = (byte) 119;
    numArray5[10] = (byte) 187;
    numArray5[11] = (byte) 140;
    numArray5[13] = (byte) 159;
    numArray5[21] = (byte) 172;
    numArray5[20] = (byte) 226;
    numArray5[14] = (byte) 134;
    numArray5[16 /*0x10*/] = (byte) 133;
    numArray5[17] = (byte) 201;
    numArray5[18] = (byte) 12;
    numArray5[4] = (byte) 39;
    numArray5[19] = (byte) 182;
    numArray5[9] = (byte) 223;
    numArray5[3] = (byte) 248;
    byte[] numArray6 = new byte[23]
    {
      (byte) 138,
      (byte) 254,
      (byte) 65,
      (byte) 139,
      (byte) 191,
      (byte) 118,
      (byte) 168,
      (byte) 181,
      (byte) 233,
      (byte) 79,
      (byte) 3,
      (byte) 117,
      (byte) 11,
      (byte) 83,
      (byte) 76,
      (byte) 116,
      (byte) 206,
      (byte) 165,
      (byte) 151,
      (byte) 66,
      (byte) 120,
      (byte) 111,
      (byte) 109
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_741()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[19] = (byte) 189;
      numArray2[1] = (byte) 139;
      numArray2[20] = (byte) 148;
      numArray2[3] = (byte) 44;
      numArray2[15] = (byte) 228;
      numArray2[5] = (byte) 193;
      numArray2[17] = (byte) 212;
      numArray2[18] = (byte) 93;
      numArray2[10] = (byte) 205;
      numArray2[7] = (byte) 99;
      numArray2[22] = (byte) 77;
      numArray2[4] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 214;
      numArray2[9] = (byte) 231;
      numArray2[13] = (byte) 85;
      numArray2[11] = (byte) 101;
      numArray2[16 /*0x10*/] = (byte) 42;
      numArray2[8] = (byte) 62;
      numArray2[14] = (byte) 127 /*0x7F*/;
      numArray2[2] = (byte) 164;
      numArray2[6] = (byte) 159;
      numArray2[21] = (byte) 216;
      numArray2[0] = (byte) 140;
      byte[] numArray3 = new byte[23]
      {
        (byte) 87,
        (byte) 24,
        (byte) 54,
        (byte) 66,
        (byte) 150,
        (byte) 172,
        byte.MaxValue,
        (byte) 253,
        (byte) 167,
        (byte) 90,
        (byte) 185,
        (byte) 208 /*0xD0*/,
        (byte) 153,
        (byte) 252,
        (byte) 79,
        (byte) 153,
        (byte) 214,
        (byte) 135,
        (byte) 102,
        (byte) 188,
        (byte) 20,
        (byte) 78,
        (byte) 105
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 38,
      (byte) 6,
      (byte) 130,
      (byte) 212,
      (byte) 190,
      (byte) 141,
      (byte) 179,
      (byte) 61,
      (byte) 161,
      byte.MaxValue,
      (byte) 94,
      (byte) 134,
      (byte) 66,
      (byte) 26,
      (byte) 181,
      (byte) 254,
      (byte) 129,
      (byte) 136,
      (byte) 169,
      (byte) 85,
      (byte) 172,
      (byte) 177,
      (byte) 245
    };
    byte[] numArray6 = new byte[23];
    numArray6[20] = (byte) 142;
    numArray6[1] = (byte) 92;
    numArray6[5] = (byte) 236;
    numArray6[13] = (byte) 32 /*0x20*/;
    numArray6[4] = (byte) 174;
    numArray6[14] = (byte) 43;
    numArray6[2] = (byte) 124;
    numArray6[8] = (byte) 129;
    numArray6[0] = (byte) 61;
    numArray6[9] = (byte) 216;
    numArray6[16 /*0x10*/] = (byte) 189;
    numArray6[10] = (byte) 123;
    numArray6[12] = (byte) 86;
    numArray6[15] = (byte) 97;
    numArray6[22] = (byte) 124;
    numArray6[11] = (byte) 117;
    numArray6[3] = (byte) 20;
    numArray6[17] = (byte) 50;
    numArray6[18] = (byte) 227;
    numArray6[19] = (byte) 77;
    numArray6[6] = (byte) 34;
    numArray6[21] = (byte) 51;
    numArray6[7] = (byte) 53;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[42];
    byte[] response = new byte[42];
    Array.Copy((Array) sc_734.sspq, 140, (Array) numArray7, 0, 42);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_734.sspr, 140, (Array) numArray7, 0, 42);
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

  internal static string ssp_automatch_742()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[19] = (byte) 166;
      numArray2[10] = (byte) 135;
      numArray2[12] = (byte) 177;
      numArray2[3] = (byte) 17;
      numArray2[4] = (byte) 182;
      numArray2[5] = (byte) 64 /*0x40*/;
      numArray2[15] = (byte) 191;
      numArray2[7] = (byte) 146;
      numArray2[11] = (byte) 253;
      numArray2[9] = (byte) 108;
      numArray2[8] = (byte) 209;
      numArray2[1] = (byte) 39;
      numArray2[0] = (byte) 243;
      numArray2[14] = (byte) 119;
      numArray2[6] = (byte) 8;
      numArray2[13] = (byte) 48 /*0x30*/;
      numArray2[21] = (byte) 124;
      numArray2[17] = (byte) 85;
      numArray2[18] = (byte) 82;
      numArray2[16 /*0x10*/] = (byte) 108;
      numArray2[20] = (byte) 23;
      numArray2[22] = (byte) 198;
      numArray2[2] = (byte) 4;
      byte[] numArray3 = new byte[23];
      numArray3[11] = (byte) 149;
      numArray3[5] = (byte) 126;
      numArray3[0] = (byte) 40;
      numArray3[3] = (byte) 208 /*0xD0*/;
      numArray3[12] = (byte) 31 /*0x1F*/;
      numArray3[1] = (byte) 96 /*0x60*/;
      numArray3[20] = (byte) 191;
      numArray3[7] = (byte) 107;
      numArray3[8] = (byte) 209;
      numArray3[13] = (byte) 24;
      numArray3[10] = (byte) 147;
      numArray3[21] = (byte) 171;
      numArray3[15] = (byte) 131;
      numArray3[6] = (byte) 148;
      numArray3[14] = (byte) 167;
      numArray3[16 /*0x10*/] = (byte) 181;
      numArray3[4] = (byte) 126;
      numArray3[17] = (byte) 236;
      numArray3[18] = (byte) 198;
      numArray3[19] = (byte) 220;
      numArray3[2] = (byte) 109;
      numArray3[9] = (byte) 64 /*0x40*/;
      numArray3[22] = (byte) 81;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 236,
      (byte) 212,
      (byte) 153,
      (byte) 201,
      (byte) 159,
      (byte) 130,
      (byte) 108,
      (byte) 57,
      (byte) 118,
      (byte) 76,
      (byte) 181,
      (byte) 122,
      (byte) 44,
      (byte) 244,
      (byte) 159,
      (byte) 235,
      (byte) 34,
      byte.MaxValue,
      (byte) 130,
      (byte) 149,
      (byte) 82,
      (byte) 130,
      (byte) 198
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 186,
      (byte) 28,
      (byte) 3,
      (byte) 146,
      (byte) 233,
      (byte) 108,
      (byte) 130,
      (byte) 7,
      (byte) 58,
      (byte) 196,
      (byte) 157,
      (byte) 194,
      (byte) 194,
      (byte) 233,
      (byte) 5,
      (byte) 75,
      (byte) 232,
      (byte) 20,
      (byte) 82,
      (byte) 51,
      (byte) 143,
      (byte) 210,
      (byte) 114
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_743()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 98,
        (byte) 24,
        (byte) 51,
        (byte) 130,
        (byte) 39,
        (byte) 219,
        (byte) 213,
        (byte) 105,
        (byte) 75,
        (byte) 106,
        (byte) 157,
        (byte) 215,
        (byte) 222,
        (byte) 1,
        (byte) 119,
        (byte) 112 /*0x70*/,
        (byte) 107,
        (byte) 224 /*0xE0*/,
        (byte) 6,
        (byte) 58,
        (byte) 44,
        (byte) 39,
        (byte) 34
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 241,
        (byte) 249,
        byte.MaxValue,
        (byte) 247,
        (byte) 202,
        byte.MaxValue,
        (byte) 207,
        (byte) 218,
        (byte) 31 /*0x1F*/,
        (byte) 31 /*0x1F*/,
        (byte) 197,
        (byte) 200,
        (byte) 33,
        (byte) 179,
        (byte) 179,
        (byte) 149,
        (byte) 11,
        (byte) 162,
        (byte) 47,
        (byte) 110,
        (byte) 161,
        (byte) 1,
        (byte) 47
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[20] = (byte) 164;
    numArray5[1] = (byte) 21;
    numArray5[2] = (byte) 106;
    numArray5[3] = (byte) 14;
    numArray5[4] = (byte) 33;
    numArray5[5] = (byte) 253;
    numArray5[6] = (byte) 41;
    numArray5[7] = (byte) 94;
    numArray5[8] = (byte) 9;
    numArray5[9] = (byte) 33;
    numArray5[12] = (byte) 141;
    numArray5[11] = (byte) 201;
    numArray5[10] = (byte) 66;
    numArray5[13] = (byte) 158;
    numArray5[22] = (byte) 219;
    numArray5[16 /*0x10*/] = (byte) 84;
    numArray5[15] = (byte) 138;
    numArray5[21] = (byte) 148;
    numArray5[18] = (byte) 133;
    numArray5[19] = (byte) 39;
    numArray5[14] = (byte) 86;
    numArray5[0] = (byte) 37;
    numArray5[17] = (byte) 129;
    byte[] numArray6 = new byte[23];
    numArray6[13] = (byte) 101;
    numArray6[1] = (byte) 182;
    numArray6[16 /*0x10*/] = (byte) 164;
    numArray6[3] = (byte) 220;
    numArray6[4] = (byte) 57;
    numArray6[5] = (byte) 87;
    numArray6[11] = (byte) 146;
    numArray6[12] = (byte) 200;
    numArray6[8] = (byte) 129;
    numArray6[9] = (byte) 69;
    numArray6[10] = (byte) 4;
    numArray6[7] = (byte) 207;
    numArray6[2] = (byte) 214;
    numArray6[15] = (byte) 32 /*0x20*/;
    numArray6[14] = (byte) 24;
    numArray6[21] = (byte) 4;
    numArray6[17] = (byte) 1;
    numArray6[0] = (byte) 0;
    numArray6[18] = (byte) 3;
    numArray6[19] = (byte) 39;
    numArray6[20] = (byte) 190;
    numArray6[6] = (byte) 8;
    numArray6[22] = (byte) 149;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_744()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[21] = (byte) 104;
      numArray2[6] = (byte) 185;
      numArray2[9] = (byte) 87;
      numArray2[1] = (byte) 120;
      numArray2[19] = (byte) 138;
      numArray2[20] = (byte) 189;
      numArray2[22] = (byte) 192 /*0xC0*/;
      numArray2[7] = (byte) 67;
      numArray2[8] = (byte) 93;
      numArray2[2] = (byte) 36;
      numArray2[10] = (byte) 84;
      numArray2[5] = (byte) 228;
      numArray2[12] = (byte) 86;
      numArray2[13] = (byte) 177;
      numArray2[14] = (byte) 53;
      numArray2[15] = (byte) 165;
      numArray2[16 /*0x10*/] = (byte) 36;
      numArray2[3] = (byte) 155;
      numArray2[18] = (byte) 166;
      numArray2[17] = (byte) 133;
      numArray2[0] = (byte) 228;
      numArray2[4] = (byte) 133;
      numArray2[11] = (byte) 103;
      byte[] numArray3 = new byte[23]
      {
        (byte) 232,
        (byte) 121,
        (byte) 68,
        (byte) 92,
        (byte) 48 /*0x30*/,
        (byte) 176 /*0xB0*/,
        (byte) 86,
        (byte) 9,
        (byte) 149,
        (byte) 164,
        (byte) 86,
        (byte) 157,
        (byte) 2,
        (byte) 108,
        (byte) 94,
        (byte) 23,
        (byte) 27,
        (byte) 121,
        (byte) 104,
        (byte) 65,
        (byte) 153,
        (byte) 112 /*0x70*/,
        (byte) 193
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46];
      byte[] response = new byte[46];
      Array.Copy((Array) sc_734.sspq, 182, (Array) numArray4, 0, 46);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_734.sspr, 182, (Array) numArray4, 0, 46);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23]
    {
      (byte) 221,
      (byte) 251,
      (byte) 138,
      (byte) 212,
      (byte) 186,
      (byte) 229,
      (byte) 73,
      (byte) 175,
      (byte) 119,
      (byte) 150,
      (byte) 64 /*0x40*/,
      (byte) 177,
      (byte) 252,
      (byte) 5,
      (byte) 237,
      (byte) 140,
      (byte) 7,
      (byte) 174,
      (byte) 1,
      (byte) 16 /*0x10*/,
      (byte) 66,
      (byte) 98,
      (byte) 36
    };
    byte[] numArray7 = new byte[23];
    numArray7[5] = (byte) 15;
    numArray7[16 /*0x10*/] = (byte) 101;
    numArray7[12] = (byte) 12;
    numArray7[22] = (byte) 19;
    numArray7[11] = (byte) 87;
    numArray7[14] = (byte) 117;
    numArray7[3] = (byte) 230;
    numArray7[7] = (byte) 185;
    numArray7[8] = (byte) 71;
    numArray7[9] = (byte) 186;
    numArray7[1] = (byte) 232;
    numArray7[15] = (byte) 226;
    numArray7[10] = (byte) 40;
    numArray7[20] = (byte) 82;
    numArray7[17] = (byte) 11;
    numArray7[21] = (byte) 79;
    numArray7[4] = (byte) 204;
    numArray7[13] = (byte) 116;
    numArray7[18] = (byte) 179;
    numArray7[19] = (byte) 130;
    numArray7[2] = (byte) 104;
    numArray7[0] = (byte) 145;
    numArray7[6] = (byte) 83;
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[12];
    byte[] response1 = new byte[12];
    Array.Copy((Array) sc_734.sspq, 228, (Array) numArray8, 0, 12);
    key.Query(true, 338, numArray8, response1);
    Array.Copy((Array) sc_734.sspr, 228, (Array) numArray8, 0, 12);
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

  internal static string ssp_automatch_745()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[14] = (byte) 0;
      numArray2[17] = (byte) 75;
      numArray2[0] = (byte) 89;
      numArray2[12] = (byte) 27;
      numArray2[5] = (byte) 109;
      numArray2[9] = (byte) 166;
      numArray2[20] = (byte) 228;
      numArray2[7] = (byte) 236;
      numArray2[8] = (byte) 216;
      numArray2[2] = (byte) 204;
      numArray2[3] = (byte) 40;
      numArray2[15] = (byte) 210;
      numArray2[1] = (byte) 234;
      numArray2[13] = (byte) 210;
      numArray2[4] = (byte) 75;
      numArray2[16 /*0x10*/] = (byte) 137;
      numArray2[11] = (byte) 160 /*0xA0*/;
      numArray2[21] = (byte) 232;
      numArray2[19] = (byte) 203;
      numArray2[6] = (byte) 89;
      numArray2[10] = (byte) 143;
      numArray2[22] = (byte) 214;
      numArray2[18] = (byte) 30;
      byte[] numArray3 = new byte[23];
      numArray3[4] = (byte) 21;
      numArray3[1] = (byte) 173;
      numArray3[2] = (byte) 197;
      numArray3[3] = (byte) 178;
      numArray3[17] = byte.MaxValue;
      numArray3[15] = (byte) 42;
      numArray3[6] = (byte) 253;
      numArray3[7] = (byte) 149;
      numArray3[8] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 156;
      numArray3[10] = (byte) 40;
      numArray3[0] = (byte) 3;
      numArray3[16 /*0x10*/] = (byte) 10;
      numArray3[11] = (byte) 103;
      numArray3[12] = (byte) 177;
      numArray3[5] = (byte) 27;
      numArray3[19] = (byte) 143;
      numArray3[13] = (byte) 58;
      numArray3[9] = (byte) 224 /*0xE0*/;
      numArray3[20] = (byte) 20;
      numArray3[18] = (byte) 80 /*0x50*/;
      numArray3[21] = (byte) 137;
      numArray3[22] = (byte) 1;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 101,
      (byte) 166,
      (byte) 245,
      (byte) 252,
      (byte) 138,
      (byte) 167,
      (byte) 78,
      (byte) 246,
      (byte) 145,
      (byte) 116,
      (byte) 231,
      (byte) 249,
      (byte) 37,
      (byte) 218,
      (byte) 205,
      (byte) 158,
      (byte) 31 /*0x1F*/,
      (byte) 197,
      (byte) 36,
      (byte) 124,
      (byte) 31 /*0x1F*/,
      (byte) 175,
      (byte) 39
    };
    byte[] numArray6 = new byte[23];
    numArray6[13] = (byte) 242;
    numArray6[6] = (byte) 217;
    numArray6[2] = (byte) 17;
    numArray6[3] = (byte) 234;
    numArray6[11] = (byte) 210;
    numArray6[19] = (byte) 230;
    numArray6[5] = (byte) 228;
    numArray6[0] = (byte) 84;
    numArray6[8] = (byte) 113;
    numArray6[9] = (byte) 131;
    numArray6[20] = (byte) 229;
    numArray6[21] = (byte) 81;
    numArray6[12] = (byte) 187;
    numArray6[10] = (byte) 243;
    numArray6[14] = (byte) 236;
    numArray6[15] = (byte) 243;
    numArray6[7] = (byte) 105;
    numArray6[17] = (byte) 151;
    numArray6[18] = (byte) 63 /*0x3F*/;
    numArray6[16 /*0x10*/] = (byte) 133;
    numArray6[1] = (byte) 218;
    numArray6[4] = (byte) 194;
    numArray6[22] = (byte) 148;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
