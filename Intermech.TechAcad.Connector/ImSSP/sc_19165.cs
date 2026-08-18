// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19165
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19165
{
  private static byte[] sspq = new byte[161]
  {
    (byte) 32 /*0x20*/,
    (byte) 35,
    (byte) 143,
    (byte) 65,
    (byte) 18,
    (byte) 129,
    (byte) 110,
    (byte) 25,
    (byte) 210,
    (byte) 13,
    (byte) 204,
    (byte) 99,
    (byte) 66,
    (byte) 155,
    (byte) 87,
    (byte) 127 /*0x7F*/,
    (byte) 163,
    (byte) 90,
    (byte) 88,
    (byte) 25,
    (byte) 241,
    (byte) 131,
    (byte) 25,
    (byte) 124,
    (byte) 28,
    (byte) 92,
    (byte) 150,
    (byte) 237,
    (byte) 224 /*0xE0*/,
    (byte) 156,
    (byte) 240 /*0xF0*/,
    (byte) 205,
    (byte) 107,
    (byte) 155,
    (byte) 31 /*0x1F*/,
    (byte) 98,
    (byte) 249,
    (byte) 189,
    (byte) 77,
    byte.MaxValue,
    (byte) 212,
    (byte) 11,
    (byte) 170,
    (byte) 160 /*0xA0*/,
    (byte) 191,
    (byte) 9,
    (byte) 19,
    (byte) 236,
    (byte) 252,
    (byte) 172,
    (byte) 116,
    (byte) 31 /*0x1F*/,
    (byte) 164,
    (byte) 171,
    (byte) 45,
    (byte) 6,
    (byte) 83,
    (byte) 167,
    (byte) 197,
    (byte) 157,
    (byte) 118,
    (byte) 85,
    (byte) 253,
    (byte) 173,
    (byte) 68,
    (byte) 151,
    (byte) 209,
    (byte) 95,
    (byte) 59,
    (byte) 200,
    (byte) 130,
    (byte) 34,
    (byte) 71,
    (byte) 63 /*0x3F*/,
    (byte) 131,
    (byte) 138,
    (byte) 183,
    (byte) 199,
    (byte) 200,
    (byte) 224 /*0xE0*/,
    (byte) 15,
    (byte) 84,
    (byte) 86,
    (byte) 101,
    (byte) 115,
    (byte) 207,
    (byte) 56,
    (byte) 62,
    (byte) 58,
    (byte) 241,
    (byte) 234,
    (byte) 218,
    (byte) 155,
    (byte) 251,
    (byte) 172,
    (byte) 132,
    (byte) 200,
    (byte) 94,
    (byte) 245,
    (byte) 182,
    (byte) 215,
    (byte) 251,
    (byte) 93,
    (byte) 44,
    (byte) 208 /*0xD0*/,
    (byte) 195,
    (byte) 145,
    (byte) 182,
    (byte) 62,
    (byte) 79,
    (byte) 245,
    (byte) 37,
    (byte) 208 /*0xD0*/,
    (byte) 102,
    (byte) 158,
    (byte) 41,
    (byte) 150,
    (byte) 34,
    (byte) 98,
    (byte) 251,
    (byte) 50,
    (byte) 21,
    (byte) 182,
    (byte) 22,
    (byte) 68,
    (byte) 157,
    (byte) 235,
    (byte) 199,
    (byte) 215,
    (byte) 22,
    (byte) 201,
    (byte) 214,
    (byte) 53,
    (byte) 100,
    (byte) 189,
    (byte) 252,
    (byte) 131,
    (byte) 51,
    (byte) 212,
    (byte) 57,
    (byte) 47,
    (byte) 64 /*0x40*/,
    (byte) 20,
    (byte) 213,
    (byte) 76,
    (byte) 102,
    (byte) 179,
    (byte) 137,
    (byte) 172,
    (byte) 208 /*0xD0*/,
    (byte) 61,
    (byte) 9,
    (byte) 180,
    (byte) 217,
    (byte) 45,
    (byte) 203,
    (byte) 110,
    (byte) 122,
    (byte) 134,
    (byte) 108,
    (byte) 171
  };
  private static byte[] sspr = new byte[161]
  {
    (byte) 189,
    (byte) 154,
    (byte) 75,
    (byte) 92,
    (byte) 241,
    (byte) 222,
    (byte) 107,
    (byte) 141,
    (byte) 105,
    (byte) 0,
    (byte) 69,
    (byte) 21,
    (byte) 89,
    (byte) 2,
    (byte) 116,
    (byte) 130,
    (byte) 155,
    (byte) 240 /*0xF0*/,
    (byte) 212,
    (byte) 207,
    (byte) 116,
    (byte) 171,
    (byte) 152,
    (byte) 91,
    (byte) 241,
    (byte) 202,
    (byte) 47,
    (byte) 168,
    (byte) 175,
    (byte) 161,
    (byte) 12,
    (byte) 10,
    (byte) 169,
    (byte) 192 /*0xC0*/,
    (byte) 96 /*0x60*/,
    (byte) 144 /*0x90*/,
    (byte) 124,
    (byte) 145,
    (byte) 96 /*0x60*/,
    (byte) 229,
    (byte) 72,
    (byte) 153,
    (byte) 62,
    (byte) 123,
    (byte) 236,
    (byte) 71,
    (byte) 33,
    (byte) 191,
    (byte) 112 /*0x70*/,
    (byte) 144 /*0x90*/,
    (byte) 192 /*0xC0*/,
    (byte) 162,
    (byte) 99,
    (byte) 143,
    (byte) 39,
    (byte) 227,
    (byte) 98,
    (byte) 150,
    (byte) 76,
    (byte) 127 /*0x7F*/,
    (byte) 48 /*0x30*/,
    (byte) 245,
    (byte) 109,
    (byte) 246,
    (byte) 143,
    (byte) 221,
    (byte) 183,
    (byte) 124,
    (byte) 9,
    (byte) 38,
    (byte) 28,
    (byte) 77,
    (byte) 20,
    (byte) 7,
    (byte) 202,
    (byte) 201,
    (byte) 188,
    (byte) 151,
    (byte) 51,
    (byte) 222,
    (byte) 84,
    (byte) 237,
    (byte) 126,
    (byte) 122,
    (byte) 126,
    (byte) 208 /*0xD0*/,
    (byte) 92,
    (byte) 101,
    (byte) 181,
    (byte) 47,
    (byte) 124,
    (byte) 119,
    (byte) 160 /*0xA0*/,
    (byte) 19,
    (byte) 135,
    (byte) 110,
    (byte) 126,
    (byte) 203,
    (byte) 179,
    (byte) 181,
    (byte) 135,
    (byte) 117,
    (byte) 179,
    (byte) 144 /*0x90*/,
    (byte) 169,
    (byte) 213,
    (byte) 74,
    (byte) 120,
    (byte) 52,
    (byte) 103,
    (byte) 1,
    (byte) 249,
    (byte) 23,
    (byte) 63 /*0x3F*/,
    (byte) 7,
    (byte) 74,
    (byte) 138,
    (byte) 245,
    (byte) 137,
    (byte) 122,
    (byte) 152,
    (byte) 111,
    (byte) 123,
    (byte) 186,
    (byte) 120,
    (byte) 6,
    (byte) 69,
    (byte) 230,
    (byte) 195,
    (byte) 190,
    (byte) 6,
    (byte) 179,
    (byte) 71,
    (byte) 49,
    (byte) 178,
    (byte) 155,
    (byte) 157,
    (byte) 247,
    (byte) 66,
    (byte) 144 /*0x90*/,
    (byte) 8,
    (byte) 148,
    (byte) 141,
    (byte) 12,
    (byte) 53,
    (byte) 49,
    (byte) 75,
    (byte) 58,
    (byte) 102,
    (byte) 63 /*0x3F*/,
    (byte) 29,
    (byte) 119,
    (byte) 182,
    (byte) 51,
    (byte) 93,
    (byte) 111,
    (byte) 209,
    (byte) 116,
    (byte) 171,
    (byte) 169,
    (byte) 187
  };

  internal static string ssp_techacad_19166()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 155,
        (byte) 200,
        (byte) 201,
        (byte) 2,
        byte.MaxValue,
        (byte) 160 /*0xA0*/,
        (byte) 234,
        (byte) 239,
        (byte) 192 /*0xC0*/,
        (byte) 233,
        (byte) 148,
        (byte) 177,
        (byte) 17,
        (byte) 137,
        (byte) 213,
        (byte) 192 /*0xC0*/,
        byte.MaxValue,
        (byte) 94,
        (byte) 6,
        (byte) 30,
        (byte) 18
      };
      byte[] numArray3 = new byte[21]
      {
        (byte) 1,
        (byte) 215,
        (byte) 204,
        (byte) 121,
        (byte) 77,
        (byte) 77,
        (byte) 154,
        (byte) 56,
        (byte) 188,
        (byte) 47,
        (byte) 152,
        (byte) 22,
        (byte) 248,
        (byte) 103,
        byte.MaxValue,
        (byte) 249,
        (byte) 251,
        (byte) 43,
        byte.MaxValue,
        (byte) 238,
        (byte) 223
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 244,
      (byte) 50,
      (byte) 60,
      (byte) 167,
      (byte) 84,
      (byte) 104,
      (byte) 105,
      (byte) 240 /*0xF0*/,
      (byte) 87,
      (byte) 177,
      (byte) 103,
      (byte) 178,
      (byte) 71,
      (byte) 132,
      (byte) 243,
      (byte) 31 /*0x1F*/,
      (byte) 157,
      (byte) 252,
      (byte) 109,
      (byte) 129,
      (byte) 92
    };
    byte[] numArray6 = new byte[21]
    {
      (byte) 210,
      (byte) 111,
      (byte) 89,
      (byte) 25,
      (byte) 173,
      (byte) 186,
      (byte) 150,
      (byte) 5,
      (byte) 124,
      (byte) 197,
      (byte) 252,
      (byte) 167,
      (byte) 28,
      (byte) 28,
      (byte) 234,
      (byte) 166,
      (byte) 237,
      (byte) 69,
      (byte) 164,
      (byte) 151,
      (byte) 217
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[18];
    byte[] response = new byte[18];
    Array.Copy((Array) sc_19165.sspq, 0, (Array) numArray7, 0, 18);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19165.sspr, 0, (Array) numArray7, 0, 18);
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

  internal static string ssp_techacad_19167()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 33,
        (byte) 34,
        (byte) 121,
        (byte) 122,
        (byte) 107,
        (byte) 231,
        (byte) 128 /*0x80*/,
        (byte) 22,
        (byte) 192 /*0xC0*/,
        (byte) 45,
        (byte) 154,
        (byte) 63 /*0x3F*/,
        (byte) 63 /*0x3F*/,
        (byte) 8,
        (byte) 221,
        (byte) 52,
        (byte) 68,
        (byte) 218,
        (byte) 174,
        (byte) 68,
        (byte) 48 /*0x30*/
      };
      byte[] numArray3 = new byte[21]
      {
        (byte) 161,
        (byte) 43,
        (byte) 24,
        (byte) 7,
        (byte) 157,
        (byte) 41,
        (byte) 234,
        (byte) 99,
        (byte) 140,
        (byte) 36,
        (byte) 206,
        (byte) 4,
        (byte) 104,
        (byte) 26,
        (byte) 212,
        (byte) 81,
        (byte) 19,
        (byte) 207,
        (byte) 160 /*0xA0*/,
        (byte) 254,
        (byte) 166
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 118,
      (byte) 39,
      (byte) 187,
      (byte) 90,
      (byte) 97,
      (byte) 21,
      (byte) 2,
      (byte) 185,
      (byte) 90,
      (byte) 147,
      (byte) 18,
      (byte) 167,
      (byte) 167,
      (byte) 169,
      (byte) 36,
      (byte) 90,
      (byte) 145,
      (byte) 107,
      (byte) 174,
      (byte) 235,
      (byte) 55
    };
    byte[] numArray6 = new byte[21];
    numArray6[12] = (byte) 190;
    numArray6[1] = (byte) 175;
    numArray6[3] = (byte) 60;
    numArray6[13] = (byte) 53;
    numArray6[7] = (byte) 203;
    numArray6[4] = (byte) 148;
    numArray6[6] = (byte) 233;
    numArray6[16 /*0x10*/] = (byte) 85;
    numArray6[8] = (byte) 232;
    numArray6[9] = (byte) 135;
    numArray6[10] = (byte) 66;
    numArray6[11] = (byte) 200;
    numArray6[14] = (byte) 98;
    numArray6[17] = (byte) 193;
    numArray6[0] = (byte) 173;
    numArray6[15] = (byte) 158;
    numArray6[5] = (byte) 164;
    numArray6[2] = (byte) 48 /*0x30*/;
    numArray6[18] = (byte) 149;
    numArray6[19] = (byte) 155;
    numArray6[20] = (byte) 158;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19168()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 140,
        (byte) 154,
        (byte) 182,
        (byte) 84,
        (byte) 178,
        (byte) 196,
        (byte) 74,
        (byte) 173,
        (byte) 61,
        (byte) 168,
        (byte) 154,
        (byte) 96 /*0x60*/,
        (byte) 245,
        (byte) 250,
        (byte) 2,
        (byte) 92,
        (byte) 196,
        (byte) 69,
        (byte) 100,
        (byte) 234
      };
      byte[] numArray3 = new byte[20];
      numArray3[17] = (byte) 106;
      numArray3[1] = (byte) 78;
      numArray3[19] = (byte) 77;
      numArray3[3] = (byte) 0;
      numArray3[4] = (byte) 237;
      numArray3[10] = (byte) 193;
      numArray3[11] = (byte) 75;
      numArray3[7] = (byte) 189;
      numArray3[2] = (byte) 141;
      numArray3[5] = (byte) 96 /*0x60*/;
      numArray3[8] = (byte) 109;
      numArray3[12] = (byte) 156;
      numArray3[16 /*0x10*/] = (byte) 76;
      numArray3[13] = (byte) 10;
      numArray3[6] = (byte) 5;
      numArray3[15] = (byte) 163;
      numArray3[18] = (byte) 171;
      numArray3[14] = (byte) 163;
      numArray3[0] = (byte) 131;
      numArray3[9] = (byte) 178;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 91,
      (byte) 169,
      (byte) 202,
      (byte) 48 /*0x30*/,
      (byte) 187,
      (byte) 168,
      (byte) 207,
      (byte) 28,
      (byte) 159,
      (byte) 71,
      (byte) 12,
      (byte) 23,
      (byte) 232,
      (byte) 245,
      (byte) 214,
      (byte) 36,
      (byte) 70,
      (byte) 14,
      (byte) 71,
      (byte) 237
    };
    byte[] numArray6 = new byte[20];
    numArray6[16 /*0x10*/] = (byte) 24;
    numArray6[3] = (byte) 101;
    numArray6[2] = (byte) 81;
    numArray6[10] = (byte) 122;
    numArray6[8] = (byte) 69;
    numArray6[5] = (byte) 137;
    numArray6[6] = (byte) 16 /*0x10*/;
    numArray6[7] = (byte) 69;
    numArray6[4] = (byte) 218;
    numArray6[18] = (byte) 139;
    numArray6[0] = (byte) 87;
    numArray6[13] = (byte) 1;
    numArray6[12] = (byte) 106;
    numArray6[17] = (byte) 211;
    numArray6[14] = (byte) 232;
    numArray6[9] = (byte) 251;
    numArray6[11] = (byte) 18;
    numArray6[15] = (byte) 71;
    numArray6[1] = (byte) 180;
    numArray6[19] = (byte) 133;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[51];
    byte[] response = new byte[51];
    Array.Copy((Array) sc_19165.sspq, 18, (Array) numArray7, 0, 51);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19165.sspr, 18, (Array) numArray7, 0, 51);
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

  internal static string ssp_techacad_19169()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 64 /*0x40*/,
        (byte) 140,
        (byte) 76,
        (byte) 88,
        (byte) 68,
        (byte) 230,
        (byte) 216,
        (byte) 120,
        (byte) 189,
        (byte) 18,
        (byte) 72,
        (byte) 169,
        (byte) 240 /*0xF0*/,
        (byte) 51,
        (byte) 40,
        (byte) 209,
        (byte) 41,
        (byte) 191,
        (byte) 109,
        (byte) 24
      };
      byte[] numArray3 = new byte[20];
      numArray3[17] = byte.MaxValue;
      numArray3[1] = (byte) 19;
      numArray3[11] = (byte) 131;
      numArray3[3] = (byte) 172;
      numArray3[4] = (byte) 20;
      numArray3[5] = (byte) 190;
      numArray3[0] = (byte) 121;
      numArray3[6] = (byte) 121;
      numArray3[12] = (byte) 212;
      numArray3[9] = (byte) 2;
      numArray3[15] = (byte) 38;
      numArray3[13] = (byte) 136;
      numArray3[2] = (byte) 68;
      numArray3[10] = (byte) 119;
      numArray3[14] = (byte) 121;
      numArray3[18] = (byte) 13;
      numArray3[16 /*0x10*/] = (byte) 142;
      numArray3[8] = (byte) 241;
      numArray3[7] = (byte) 150;
      numArray3[19] = (byte) 54;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 178,
      (byte) 85,
      (byte) 204,
      (byte) 100,
      (byte) 201,
      (byte) 133,
      (byte) 115,
      (byte) 109,
      (byte) 17,
      (byte) 241,
      (byte) 35,
      (byte) 128 /*0x80*/,
      (byte) 238,
      (byte) 148,
      (byte) 157,
      (byte) 207,
      (byte) 197,
      (byte) 215,
      (byte) 220,
      (byte) 29
    };
    byte[] numArray6 = new byte[20]
    {
      (byte) 48 /*0x30*/,
      (byte) 207,
      (byte) 192 /*0xC0*/,
      (byte) 124,
      (byte) 137,
      (byte) 251,
      (byte) 253,
      (byte) 157,
      (byte) 250,
      (byte) 161,
      (byte) 12,
      (byte) 190,
      (byte) 214,
      (byte) 100,
      (byte) 162,
      (byte) 74,
      (byte) 142,
      (byte) 25,
      (byte) 77,
      (byte) 217
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19170()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[6] = (byte) 38;
      numArray2[0] = (byte) 130;
      numArray2[2] = (byte) 63 /*0x3F*/;
      numArray2[3] = byte.MaxValue;
      numArray2[4] = (byte) 221;
      numArray2[5] = (byte) 88;
      numArray2[13] = (byte) 26;
      numArray2[8] = (byte) 51;
      numArray2[7] = (byte) 234;
      numArray2[9] = (byte) 103;
      numArray2[10] = (byte) 69;
      numArray2[17] = (byte) 186;
      numArray2[12] = (byte) 188;
      numArray2[11] = (byte) 90;
      numArray2[16 /*0x10*/] = (byte) 232;
      numArray2[14] = (byte) 68;
      numArray2[1] = (byte) 199;
      numArray2[15] = (byte) 118;
      numArray2[18] = (byte) 39;
      numArray2[19] = (byte) 193;
      byte[] numArray3 = new byte[20]
      {
        (byte) 176 /*0xB0*/,
        (byte) 179,
        (byte) 29,
        (byte) 177,
        (byte) 168,
        (byte) 195,
        (byte) 105,
        (byte) 88,
        (byte) 72,
        (byte) 150,
        (byte) 243,
        (byte) 96 /*0x60*/,
        (byte) 202,
        (byte) 26,
        (byte) 25,
        (byte) 58,
        (byte) 56,
        (byte) 216,
        (byte) 87,
        (byte) 154
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 134,
      (byte) 78,
      (byte) 67,
      (byte) 18,
      (byte) 171,
      (byte) 147,
      (byte) 101,
      (byte) 53,
      (byte) 219,
      (byte) 179,
      (byte) 211,
      (byte) 106,
      (byte) 149,
      (byte) 73,
      (byte) 235,
      (byte) 202,
      (byte) 96 /*0x60*/,
      (byte) 183,
      (byte) 182,
      (byte) 153
    };
    byte[] numArray6 = new byte[20];
    numArray6[3] = (byte) 140;
    numArray6[15] = (byte) 187;
    numArray6[7] = (byte) 194;
    numArray6[13] = (byte) 117;
    numArray6[4] = (byte) 251;
    numArray6[5] = (byte) 88;
    numArray6[8] = (byte) 239;
    numArray6[14] = (byte) 52;
    numArray6[12] = (byte) 254;
    numArray6[9] = (byte) 44;
    numArray6[11] = (byte) 38;
    numArray6[1] = (byte) 135;
    numArray6[2] = (byte) 91;
    numArray6[10] = (byte) 30;
    numArray6[6] = (byte) 48 /*0x30*/;
    numArray6[0] = (byte) 66;
    numArray6[16 /*0x10*/] = (byte) 241;
    numArray6[17] = (byte) 36;
    numArray6[18] = (byte) 126;
    numArray6[19] = (byte) 249;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19171()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[14] = (byte) 42;
      numArray2[7] = (byte) 0;
      numArray2[2] = (byte) 76;
      numArray2[17] = (byte) 52;
      numArray2[3] = (byte) 185;
      numArray2[9] = (byte) 158;
      numArray2[4] = (byte) 90;
      numArray2[12] = (byte) 226;
      numArray2[8] = (byte) 20;
      numArray2[1] = (byte) 105;
      numArray2[10] = (byte) 215;
      numArray2[11] = (byte) 115;
      numArray2[5] = (byte) 169;
      numArray2[13] = (byte) 169;
      numArray2[6] = (byte) 47;
      numArray2[15] = (byte) 113;
      numArray2[16 /*0x10*/] = (byte) 81;
      numArray2[19] = (byte) 177;
      numArray2[18] = (byte) 192 /*0xC0*/;
      numArray2[0] = (byte) 236;
      byte[] numArray3 = new byte[20]
      {
        (byte) 205,
        (byte) 216,
        (byte) 185,
        (byte) 9,
        (byte) 190,
        (byte) 229,
        (byte) 177,
        (byte) 219,
        (byte) 154,
        (byte) 137,
        (byte) 195,
        (byte) 117,
        (byte) 67,
        (byte) 2,
        (byte) 147,
        (byte) 39,
        (byte) 168,
        (byte) 197,
        (byte) 47,
        (byte) 238
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[51];
      byte[] response = new byte[51];
      Array.Copy((Array) sc_19165.sspq, 69, (Array) numArray4, 0, 51);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19165.sspr, 69, (Array) numArray4, 0, 51);
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
    byte[] numArray6 = new byte[20];
    numArray6[8] = (byte) 147;
    numArray6[10] = (byte) 193;
    numArray6[2] = (byte) 167;
    numArray6[1] = (byte) 46;
    numArray6[4] = (byte) 221;
    numArray6[11] = (byte) 89;
    numArray6[6] = (byte) 135;
    numArray6[7] = (byte) 76;
    numArray6[17] = (byte) 42;
    numArray6[9] = byte.MaxValue;
    numArray6[18] = (byte) 149;
    numArray6[3] = (byte) 197;
    numArray6[0] = (byte) 193;
    numArray6[13] = (byte) 18;
    numArray6[14] = (byte) 9;
    numArray6[15] = (byte) 20;
    numArray6[16 /*0x10*/] = (byte) 246;
    numArray6[12] = (byte) 210;
    numArray6[5] = (byte) 167;
    numArray6[19] = (byte) 175;
    byte[] numArray7 = new byte[20];
    numArray7[2] = (byte) 206;
    numArray7[12] = (byte) 86;
    numArray7[0] = (byte) 224 /*0xE0*/;
    numArray7[14] = (byte) 77;
    numArray7[3] = (byte) 122;
    numArray7[15] = (byte) 187;
    numArray7[4] = (byte) 121;
    numArray7[5] = (byte) 8;
    numArray7[11] = (byte) 12;
    numArray7[9] = (byte) 49;
    numArray7[19] = (byte) 132;
    numArray7[7] = (byte) 163;
    numArray7[10] = (byte) 180;
    numArray7[13] = (byte) 173;
    numArray7[1] = (byte) 227;
    numArray7[6] = (byte) 214;
    numArray7[16 /*0x10*/] = (byte) 61;
    numArray7[17] = (byte) 177;
    numArray7[18] = (byte) 13;
    numArray7[8] = (byte) 32 /*0x20*/;
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19172()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 17,
        (byte) 27,
        (byte) 93,
        (byte) 216,
        (byte) 29,
        (byte) 95,
        (byte) 192 /*0xC0*/,
        (byte) 94,
        (byte) 219,
        (byte) 109,
        (byte) 115,
        (byte) 248,
        (byte) 95,
        (byte) 192 /*0xC0*/,
        (byte) 93,
        (byte) 94,
        (byte) 114,
        (byte) 75,
        (byte) 215,
        (byte) 209
      };
      byte[] numArray3 = new byte[20];
      numArray3[19] = (byte) 194;
      numArray3[14] = (byte) 41;
      numArray3[7] = (byte) 1;
      numArray3[1] = (byte) 164;
      numArray3[15] = (byte) 96 /*0x60*/;
      numArray3[5] = (byte) 110;
      numArray3[6] = (byte) 47;
      numArray3[16 /*0x10*/] = (byte) 11;
      numArray3[17] = (byte) 64 /*0x40*/;
      numArray3[0] = (byte) 146;
      numArray3[10] = (byte) 254;
      numArray3[3] = (byte) 83;
      numArray3[12] = (byte) 1;
      numArray3[13] = (byte) 82;
      numArray3[11] = (byte) 222;
      numArray3[18] = (byte) 159;
      numArray3[9] = (byte) 209;
      numArray3[4] = (byte) 30;
      numArray3[2] = (byte) 182;
      numArray3[8] = (byte) 75;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 32 /*0x20*/,
      (byte) 63 /*0x3F*/,
      (byte) 197,
      (byte) 224 /*0xE0*/,
      (byte) 109,
      (byte) 94,
      (byte) 107,
      (byte) 165,
      (byte) 81,
      (byte) 123,
      (byte) 31 /*0x1F*/,
      (byte) 193,
      (byte) 143,
      (byte) 2,
      (byte) 20,
      (byte) 157,
      (byte) 105,
      (byte) 229,
      (byte) 40,
      (byte) 144 /*0x90*/
    };
    byte[] numArray6 = new byte[20]
    {
      (byte) 128 /*0x80*/,
      (byte) 28,
      (byte) 162,
      (byte) 218,
      (byte) 173,
      (byte) 205,
      (byte) 57,
      (byte) 148,
      (byte) 86,
      (byte) 141,
      (byte) 200,
      (byte) 57,
      (byte) 42,
      (byte) 121,
      (byte) 91,
      (byte) 250,
      (byte) 10,
      (byte) 138,
      (byte) 56,
      (byte) 69
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19173()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 77,
        (byte) 146,
        (byte) 235,
        (byte) 31 /*0x1F*/,
        (byte) 248,
        (byte) 171,
        (byte) 217,
        (byte) 6,
        (byte) 124,
        (byte) 240 /*0xF0*/,
        (byte) 249,
        (byte) 9,
        (byte) 118,
        (byte) 212,
        (byte) 13,
        (byte) 57,
        (byte) 99,
        (byte) 144 /*0x90*/,
        (byte) 56,
        (byte) 109
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 51,
        (byte) 76,
        (byte) 6,
        (byte) 131,
        (byte) 168,
        (byte) 221,
        (byte) 116,
        (byte) 210,
        (byte) 114,
        (byte) 159,
        (byte) 37,
        (byte) 240 /*0xF0*/,
        (byte) 49,
        (byte) 23,
        (byte) 23,
        (byte) 5,
        (byte) 49,
        (byte) 127 /*0x7F*/,
        (byte) 47,
        (byte) 97
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[5] = (byte) 162;
    numArray5[1] = (byte) 42;
    numArray5[11] = (byte) 146;
    numArray5[2] = (byte) 220;
    numArray5[6] = (byte) 67;
    numArray5[18] = (byte) 25;
    numArray5[7] = (byte) 236;
    numArray5[9] = (byte) 180;
    numArray5[8] = (byte) 61;
    numArray5[0] = (byte) 111;
    numArray5[17] = (byte) 162;
    numArray5[15] = (byte) 172;
    numArray5[12] = (byte) 218;
    numArray5[13] = (byte) 114;
    numArray5[14] = (byte) 75;
    numArray5[10] = (byte) 32 /*0x20*/;
    numArray5[16 /*0x10*/] = (byte) 166;
    numArray5[3] = (byte) 55;
    numArray5[4] = (byte) 24;
    numArray5[19] = (byte) 68;
    byte[] numArray6 = new byte[20];
    numArray6[3] = (byte) 57;
    numArray6[1] = (byte) 35;
    numArray6[2] = (byte) 1;
    numArray6[15] = (byte) 39;
    numArray6[4] = (byte) 152;
    numArray6[5] = (byte) 85;
    numArray6[13] = (byte) 74;
    numArray6[17] = (byte) 7;
    numArray6[6] = (byte) 132;
    numArray6[9] = (byte) 35;
    numArray6[10] = (byte) 146;
    numArray6[11] = (byte) 236;
    numArray6[8] = (byte) 207;
    numArray6[0] = (byte) 34;
    numArray6[14] = (byte) 32 /*0x20*/;
    numArray6[12] = (byte) 66;
    numArray6[7] = (byte) 45;
    numArray6[16 /*0x10*/] = (byte) 129;
    numArray6[18] = (byte) 185;
    numArray6[19] = (byte) 163;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19174()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[20] = (byte) 22;
      numArray2[8] = (byte) 224 /*0xE0*/;
      numArray2[2] = (byte) 148;
      numArray2[17] = (byte) 41;
      numArray2[4] = (byte) 246;
      numArray2[3] = (byte) 230;
      numArray2[16 /*0x10*/] = (byte) 165;
      numArray2[1] = (byte) 176 /*0xB0*/;
      numArray2[18] = (byte) 21;
      numArray2[19] = (byte) 47;
      numArray2[10] = (byte) 12;
      numArray2[11] = (byte) 155;
      numArray2[12] = (byte) 77;
      numArray2[0] = (byte) 150;
      numArray2[14] = (byte) 209;
      numArray2[15] = (byte) 81;
      numArray2[7] = (byte) 49;
      numArray2[9] = (byte) 181;
      numArray2[5] = (byte) 85;
      numArray2[13] = (byte) 181;
      numArray2[6] = (byte) 143;
      byte[] numArray3 = new byte[21];
      numArray3[11] = (byte) 39;
      numArray3[18] = (byte) 172;
      numArray3[5] = (byte) 132;
      numArray3[3] = (byte) 74;
      numArray3[15] = (byte) 12;
      numArray3[20] = (byte) 138;
      numArray3[6] = (byte) 34;
      numArray3[7] = (byte) 38;
      numArray3[8] = (byte) 248;
      numArray3[9] = (byte) 249;
      numArray3[10] = (byte) 249;
      numArray3[2] = (byte) 181;
      numArray3[19] = (byte) 149;
      numArray3[13] = (byte) 205;
      numArray3[14] = (byte) 193;
      numArray3[4] = (byte) 183;
      numArray3[17] = (byte) 32 /*0x20*/;
      numArray3[1] = (byte) 93;
      numArray3[16 /*0x10*/] = (byte) 81;
      numArray3[12] = (byte) 166;
      numArray3[0] = (byte) 175;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21];
    numArray5[1] = (byte) 38;
    numArray5[6] = (byte) 89;
    numArray5[18] = (byte) 231;
    numArray5[11] = (byte) 239;
    numArray5[4] = (byte) 163;
    numArray5[14] = (byte) 253;
    numArray5[0] = (byte) 112 /*0x70*/;
    numArray5[7] = (byte) 82;
    numArray5[19] = (byte) 39;
    numArray5[10] = (byte) 104;
    numArray5[8] = (byte) 218;
    numArray5[15] = (byte) 108;
    numArray5[2] = (byte) 247;
    numArray5[5] = (byte) 90;
    numArray5[9] = (byte) 181;
    numArray5[3] = (byte) 45;
    numArray5[16 /*0x10*/] = (byte) 133;
    numArray5[17] = (byte) 128 /*0x80*/;
    numArray5[12] = (byte) 242;
    numArray5[13] = (byte) 143;
    numArray5[20] = (byte) 31 /*0x1F*/;
    byte[] numArray6 = new byte[21]
    {
      (byte) 34,
      (byte) 75,
      (byte) 59,
      (byte) 41,
      (byte) 66,
      (byte) 40,
      (byte) 143,
      (byte) 45,
      (byte) 59,
      (byte) 127 /*0x7F*/,
      (byte) 231,
      (byte) 33,
      (byte) 173,
      (byte) 90,
      (byte) 44,
      (byte) 191,
      (byte) 244,
      (byte) 42,
      (byte) 241,
      (byte) 58,
      (byte) 9
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19175()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 176 /*0xB0*/,
        (byte) 98,
        (byte) 227,
        (byte) 19,
        (byte) 162,
        (byte) 223,
        (byte) 119,
        (byte) 221,
        (byte) 204,
        (byte) 239,
        (byte) 125,
        (byte) 125,
        (byte) 228,
        (byte) 152,
        (byte) 0,
        (byte) 107,
        (byte) 16 /*0x10*/,
        (byte) 174,
        (byte) 179,
        (byte) 221,
        (byte) 142
      };
      byte[] numArray3 = new byte[21];
      numArray3[9] = (byte) 241;
      numArray3[1] = (byte) 234;
      numArray3[2] = (byte) 225;
      numArray3[16 /*0x10*/] = (byte) 30;
      numArray3[4] = (byte) 197;
      numArray3[20] = (byte) 178;
      numArray3[5] = (byte) 233;
      numArray3[17] = (byte) 245;
      numArray3[7] = (byte) 30;
      numArray3[12] = (byte) 143;
      numArray3[11] = (byte) 191;
      numArray3[15] = (byte) 152;
      numArray3[0] = (byte) 121;
      numArray3[13] = (byte) 85;
      numArray3[8] = (byte) 217;
      numArray3[14] = (byte) 213;
      numArray3[10] = (byte) 216;
      numArray3[19] = (byte) 49;
      numArray3[18] = (byte) 147;
      numArray3[6] = (byte) 185;
      numArray3[3] = (byte) 132;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 245,
      (byte) 209,
      (byte) 20,
      (byte) 62,
      (byte) 196,
      (byte) 158,
      (byte) 32 /*0x20*/,
      (byte) 20,
      (byte) 253,
      (byte) 222,
      (byte) 208 /*0xD0*/,
      (byte) 198,
      (byte) 215,
      (byte) 25,
      (byte) 211,
      (byte) 11,
      (byte) 207,
      (byte) 230,
      (byte) 91,
      (byte) 61,
      (byte) 156
    };
    byte[] numArray6 = new byte[21];
    numArray6[13] = (byte) 81;
    numArray6[19] = (byte) 111;
    numArray6[2] = (byte) 15;
    numArray6[11] = (byte) 74;
    numArray6[7] = (byte) 100;
    numArray6[16 /*0x10*/] = (byte) 75;
    numArray6[6] = (byte) 35;
    numArray6[8] = (byte) 180;
    numArray6[3] = (byte) 210;
    numArray6[5] = (byte) 75;
    numArray6[10] = (byte) 7;
    numArray6[4] = (byte) 57;
    numArray6[12] = (byte) 129;
    numArray6[15] = (byte) 180;
    numArray6[1] = (byte) 218;
    numArray6[14] = (byte) 9;
    numArray6[9] = (byte) 156;
    numArray6[17] = (byte) 207;
    numArray6[18] = (byte) 55;
    numArray6[0] = (byte) 98;
    numArray6[20] = (byte) 169;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_19165.sspq, 120, (Array) numArray7, 0, 41);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19165.sspr, 120, (Array) numArray7, 0, 41);
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

  internal static string ssp_techacad_19176()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[9] = (byte) 109;
      numArray2[1] = (byte) 237;
      numArray2[0] = (byte) 191;
      numArray2[11] = (byte) 80 /*0x50*/;
      numArray2[4] = (byte) 52;
      numArray2[2] = (byte) 51;
      numArray2[6] = (byte) 20;
      numArray2[19] = (byte) 97;
      numArray2[18] = (byte) 234;
      numArray2[3] = (byte) 232;
      numArray2[10] = (byte) 41;
      numArray2[8] = (byte) 72;
      numArray2[12] = (byte) 1;
      numArray2[13] = (byte) 242;
      numArray2[14] = (byte) 0;
      numArray2[15] = (byte) 224 /*0xE0*/;
      numArray2[16 /*0x10*/] = (byte) 202;
      numArray2[17] = (byte) 221;
      numArray2[5] = (byte) 206;
      numArray2[7] = (byte) 148;
      byte[] numArray3 = new byte[20]
      {
        (byte) 204,
        (byte) 173,
        (byte) 95,
        (byte) 129,
        (byte) 103,
        (byte) 189,
        (byte) 138,
        (byte) 89,
        byte.MaxValue,
        (byte) 114,
        (byte) 60,
        (byte) 189,
        (byte) 89,
        (byte) 193,
        (byte) 32 /*0x20*/,
        (byte) 89,
        (byte) 174,
        (byte) 83,
        (byte) 245,
        (byte) 24
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 208 /*0xD0*/,
      (byte) 223,
      (byte) 99,
      (byte) 215,
      (byte) 59,
      (byte) 202,
      (byte) 141,
      (byte) 176 /*0xB0*/,
      (byte) 168,
      (byte) 130,
      (byte) 73,
      (byte) 180,
      (byte) 184,
      (byte) 20,
      (byte) 115,
      (byte) 198,
      (byte) 216,
      (byte) 151,
      (byte) 34,
      (byte) 7
    };
    byte[] numArray6 = new byte[20];
    numArray6[13] = (byte) 55;
    numArray6[14] = (byte) 83;
    numArray6[16 /*0x10*/] = (byte) 27;
    numArray6[17] = (byte) 226;
    numArray6[4] = (byte) 213;
    numArray6[5] = (byte) 51;
    numArray6[2] = (byte) 30;
    numArray6[1] = (byte) 177;
    numArray6[7] = (byte) 114;
    numArray6[9] = (byte) 232;
    numArray6[0] = (byte) 98;
    numArray6[11] = (byte) 192 /*0xC0*/;
    numArray6[12] = (byte) 181;
    numArray6[19] = (byte) 110;
    numArray6[3] = (byte) 9;
    numArray6[15] = (byte) 23;
    numArray6[6] = (byte) 63 /*0x3F*/;
    numArray6[8] = (byte) 8;
    numArray6[18] = (byte) 237;
    numArray6[10] = (byte) 166;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19177()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 67,
        (byte) 170,
        (byte) 200,
        (byte) 18,
        (byte) 46,
        (byte) 157,
        (byte) 89,
        (byte) 77,
        (byte) 83,
        (byte) 16 /*0x10*/,
        (byte) 73,
        (byte) 15,
        (byte) 29,
        (byte) 160 /*0xA0*/,
        (byte) 54,
        (byte) 201,
        (byte) 240 /*0xF0*/,
        (byte) 79,
        (byte) 73,
        (byte) 252,
        (byte) 125,
        (byte) 93,
        (byte) 177
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 204,
        (byte) 179,
        (byte) 96 /*0x60*/,
        (byte) 117,
        (byte) 155,
        (byte) 199,
        (byte) 229,
        (byte) 110,
        (byte) 91,
        (byte) 0,
        (byte) 190,
        (byte) 67,
        (byte) 44,
        (byte) 191,
        (byte) 182,
        (byte) 196,
        (byte) 78,
        (byte) 222,
        (byte) 50,
        (byte) 196,
        (byte) 243,
        (byte) 45,
        (byte) 217
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[11] = (byte) 198;
    numArray5[1] = (byte) 83;
    numArray5[2] = (byte) 155;
    numArray5[0] = (byte) 8;
    numArray5[21] = (byte) 29;
    numArray5[5] = (byte) 229;
    numArray5[4] = (byte) 7;
    numArray5[8] = (byte) 127 /*0x7F*/;
    numArray5[6] = (byte) 188;
    numArray5[9] = (byte) 247;
    numArray5[10] = (byte) 168;
    numArray5[3] = (byte) 250;
    numArray5[12] = (byte) 16 /*0x10*/;
    numArray5[17] = (byte) 243;
    numArray5[14] = (byte) 82;
    numArray5[15] = (byte) 183;
    numArray5[18] = (byte) 53;
    numArray5[22] = (byte) 78;
    numArray5[13] = (byte) 201;
    numArray5[19] = (byte) 249;
    numArray5[20] = (byte) 195;
    numArray5[16 /*0x10*/] = (byte) 153;
    numArray5[7] = (byte) 221;
    byte[] numArray6 = new byte[23]
    {
      (byte) 223,
      (byte) 5,
      (byte) 11,
      (byte) 59,
      (byte) 109,
      (byte) 220,
      (byte) 117,
      (byte) 95,
      (byte) 237,
      (byte) 128 /*0x80*/,
      (byte) 63 /*0x3F*/,
      (byte) 11,
      (byte) 46,
      (byte) 97,
      (byte) 72,
      (byte) 86,
      (byte) 90,
      (byte) 248,
      (byte) 137,
      (byte) 84,
      (byte) 226,
      (byte) 234,
      (byte) 159
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
