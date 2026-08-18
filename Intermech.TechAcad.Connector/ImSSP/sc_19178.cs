// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19178
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19178
{
  private static byte[] sspq = new byte[23]
  {
    (byte) 237,
    (byte) 99,
    (byte) 144 /*0x90*/,
    (byte) 123,
    (byte) 39,
    (byte) 82,
    (byte) 19,
    (byte) 61,
    (byte) 2,
    (byte) 85,
    (byte) 239,
    (byte) 219,
    (byte) 93,
    (byte) 52,
    (byte) 61,
    (byte) 37,
    (byte) 209,
    (byte) 65,
    (byte) 208 /*0xD0*/,
    (byte) 204,
    (byte) 103,
    (byte) 253,
    (byte) 189
  };
  private static byte[] sspr = new byte[23]
  {
    byte.MaxValue,
    (byte) 150,
    (byte) 145,
    (byte) 74,
    (byte) 80 /*0x50*/,
    (byte) 84,
    (byte) 187,
    (byte) 169,
    (byte) 132,
    (byte) 128 /*0x80*/,
    (byte) 24,
    (byte) 197,
    (byte) 195,
    (byte) 18,
    (byte) 150,
    (byte) 179,
    (byte) 18,
    (byte) 166,
    (byte) 5,
    (byte) 46,
    (byte) 134,
    (byte) 93,
    (byte) 183
  };

  internal static string ssp_techacad_19179()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35];
      numArray2[2] = (byte) 3;
      numArray2[1] = (byte) 190;
      numArray2[25] = (byte) 132;
      numArray2[31 /*0x1F*/] = (byte) 143;
      numArray2[4] = (byte) 206;
      numArray2[22] = (byte) 233;
      numArray2[32 /*0x20*/] = (byte) 94;
      numArray2[7] = (byte) 105;
      numArray2[8] = (byte) 195;
      numArray2[9] = (byte) 86;
      numArray2[10] = (byte) 213;
      numArray2[11] = (byte) 179;
      numArray2[26] = (byte) 251;
      numArray2[21] = (byte) 64 /*0x40*/;
      numArray2[14] = (byte) 253;
      numArray2[13] = (byte) 153;
      numArray2[23] = (byte) 210;
      numArray2[6] = (byte) 5;
      numArray2[18] = (byte) 52;
      numArray2[0] = (byte) 196;
      numArray2[19] = (byte) 76;
      numArray2[12] = (byte) 117;
      numArray2[30] = (byte) 113;
      numArray2[15] = (byte) 25;
      numArray2[24] = (byte) 166;
      numArray2[29] = (byte) 13;
      numArray2[3] = (byte) 17;
      numArray2[28] = (byte) 16 /*0x10*/;
      numArray2[20] = (byte) 6;
      numArray2[27] = (byte) 15;
      numArray2[16 /*0x10*/] = (byte) 191;
      numArray2[17] = (byte) 116;
      numArray2[5] = (byte) 235;
      numArray2[33] = (byte) 162;
      numArray2[34] = (byte) 33;
      byte[] numArray3 = new byte[35]
      {
        (byte) 73,
        (byte) 23,
        (byte) 17,
        (byte) 232,
        (byte) 240 /*0xF0*/,
        (byte) 181,
        (byte) 24,
        (byte) 38,
        (byte) 90,
        (byte) 197,
        (byte) 239,
        (byte) 111,
        (byte) 72,
        (byte) 96 /*0x60*/,
        (byte) 149,
        (byte) 51,
        (byte) 129,
        (byte) 216,
        (byte) 139,
        (byte) 105,
        (byte) 86,
        (byte) 239,
        (byte) 199,
        (byte) 24,
        (byte) 176 /*0xB0*/,
        (byte) 248,
        (byte) 157,
        (byte) 190,
        (byte) 140,
        (byte) 168,
        (byte) 48 /*0x30*/,
        (byte) 183,
        (byte) 198,
        (byte) 86,
        (byte) 236
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 139,
      (byte) 75,
      (byte) 190,
      (byte) 171,
      (byte) 54,
      (byte) 68,
      (byte) 7,
      (byte) 186,
      (byte) 116,
      (byte) 206,
      (byte) 7,
      (byte) 162,
      (byte) 167,
      (byte) 67,
      (byte) 0,
      (byte) 227,
      (byte) 156,
      (byte) 101,
      (byte) 89,
      (byte) 139,
      (byte) 130,
      (byte) 185,
      (byte) 10,
      (byte) 238,
      (byte) 223,
      (byte) 206,
      (byte) 71,
      (byte) 73,
      (byte) 247,
      (byte) 14,
      (byte) 125,
      (byte) 123,
      (byte) 161,
      (byte) 35,
      (byte) 191
    };
    byte[] numArray6 = new byte[35]
    {
      (byte) 10,
      (byte) 221,
      (byte) 14,
      (byte) 229,
      (byte) 250,
      (byte) 142,
      (byte) 102,
      (byte) 192 /*0xC0*/,
      (byte) 131,
      (byte) 188,
      (byte) 30,
      (byte) 22,
      (byte) 159,
      (byte) 222,
      (byte) 150,
      (byte) 236,
      (byte) 39,
      (byte) 45,
      (byte) 32 /*0x20*/,
      (byte) 232,
      (byte) 230,
      (byte) 31 /*0x1F*/,
      (byte) 128 /*0x80*/,
      (byte) 29,
      (byte) 251,
      (byte) 149,
      (byte) 196,
      (byte) 252,
      (byte) 67,
      (byte) 222,
      (byte) 1,
      (byte) 241,
      (byte) 76,
      (byte) 104,
      (byte) 92
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_19178.sspq, 0, (Array) numArray7, 0, 23);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19178.sspr, 0, (Array) numArray7, 0, 23);
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

  internal static string ssp_techacad_19180()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[29] = (byte) 12;
      numArray2[12] = (byte) 241;
      numArray2[11] = (byte) 39;
      numArray2[10] = (byte) 108;
      numArray2[21] = (byte) 51;
      numArray2[7] = (byte) 226;
      numArray2[6] = (byte) 238;
      numArray2[3] = (byte) 1;
      numArray2[8] = (byte) 195;
      numArray2[33] = (byte) 205;
      numArray2[26] = (byte) 238;
      numArray2[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
      numArray2[22] = (byte) 32 /*0x20*/;
      numArray2[13] = (byte) 62;
      numArray2[9] = (byte) 199;
      numArray2[28] = (byte) 49;
      numArray2[16 /*0x10*/] = (byte) 203;
      numArray2[1] = (byte) 80 /*0x50*/;
      numArray2[18] = (byte) 68;
      numArray2[17] = (byte) 145;
      numArray2[0] = (byte) 101;
      numArray2[35] = (byte) 215;
      numArray2[25] = (byte) 253;
      numArray2[23] = (byte) 71;
      numArray2[24] = (byte) 99;
      numArray2[32 /*0x20*/] = (byte) 166;
      numArray2[20] = (byte) 7;
      numArray2[27] = (byte) 208 /*0xD0*/;
      numArray2[14] = (byte) 226;
      numArray2[15] = (byte) 155;
      numArray2[30] = (byte) 54;
      numArray2[5] = (byte) 131;
      numArray2[2] = (byte) 140;
      numArray2[19] = (byte) 244;
      numArray2[34] = (byte) 21;
      numArray2[4] = (byte) 132;
      byte[] numArray3 = new byte[36]
      {
        (byte) 38,
        (byte) 190,
        (byte) 116,
        (byte) 248,
        (byte) 221,
        (byte) 185,
        (byte) 106,
        (byte) 1,
        (byte) 58,
        (byte) 30,
        (byte) 219,
        (byte) 74,
        (byte) 24,
        byte.MaxValue,
        (byte) 119,
        (byte) 92,
        (byte) 88,
        (byte) 55,
        (byte) 87,
        (byte) 178,
        (byte) 183,
        (byte) 116,
        (byte) 201,
        (byte) 128 /*0x80*/,
        (byte) 226,
        (byte) 34,
        (byte) 253,
        (byte) 107,
        (byte) 19,
        (byte) 18,
        (byte) 60,
        (byte) 254,
        (byte) 65,
        (byte) 144 /*0x90*/,
        (byte) 113,
        (byte) 252
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[36];
    byte[] numArray5 = new byte[36]
    {
      (byte) 250,
      (byte) 27,
      (byte) 139,
      (byte) 127 /*0x7F*/,
      (byte) 77,
      (byte) 201,
      (byte) 91,
      (byte) 113,
      (byte) 252,
      (byte) 12,
      (byte) 96 /*0x60*/,
      (byte) 55,
      (byte) 221,
      (byte) 18,
      (byte) 227,
      (byte) 167,
      (byte) 128 /*0x80*/,
      (byte) 115,
      (byte) 50,
      (byte) 233,
      (byte) 72,
      (byte) 91,
      (byte) 96 /*0x60*/,
      (byte) 4,
      (byte) 76,
      (byte) 148,
      (byte) 16 /*0x10*/,
      (byte) 105,
      (byte) 84,
      (byte) 94,
      (byte) 69,
      (byte) 253,
      (byte) 243,
      (byte) 57,
      (byte) 192 /*0xC0*/,
      (byte) 22
    };
    byte[] numArray6 = new byte[36]
    {
      (byte) 55,
      (byte) 81,
      (byte) 83,
      (byte) 24,
      (byte) 203,
      (byte) 123,
      (byte) 0,
      (byte) 8,
      (byte) 165,
      (byte) 175,
      (byte) 62,
      (byte) 80 /*0x50*/,
      (byte) 3,
      (byte) 156,
      (byte) 62,
      (byte) 207,
      (byte) 189,
      (byte) 239,
      (byte) 64 /*0x40*/,
      (byte) 233,
      (byte) 222,
      (byte) 234,
      (byte) 206,
      (byte) 154,
      (byte) 109,
      (byte) 152,
      (byte) 230,
      (byte) 203,
      (byte) 91,
      (byte) 172,
      (byte) 46,
      (byte) 15,
      (byte) 215,
      (byte) 15,
      (byte) 176 /*0xB0*/,
      (byte) 141
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19181()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[38];
      byte[] numArray2 = new byte[38]
      {
        (byte) 58,
        (byte) 172,
        (byte) 180,
        (byte) 171,
        (byte) 48 /*0x30*/,
        (byte) 68,
        (byte) 163,
        (byte) 16 /*0x10*/,
        (byte) 69,
        (byte) 98,
        (byte) 85,
        (byte) 70,
        (byte) 166,
        (byte) 219,
        (byte) 69,
        (byte) 158,
        (byte) 186,
        (byte) 134,
        (byte) 51,
        (byte) 185,
        (byte) 185,
        (byte) 241,
        (byte) 29,
        (byte) 158,
        (byte) 198,
        (byte) 99,
        (byte) 67,
        (byte) 164,
        (byte) 82,
        (byte) 204,
        (byte) 3,
        (byte) 186,
        (byte) 35,
        (byte) 117,
        (byte) 175,
        (byte) 156,
        (byte) 167,
        (byte) 181
      };
      byte[] numArray3 = new byte[38]
      {
        (byte) 26,
        (byte) 154,
        (byte) 233,
        (byte) 87,
        (byte) 242,
        (byte) 246,
        (byte) 66,
        (byte) 130,
        (byte) 149,
        (byte) 198,
        (byte) 2,
        (byte) 1,
        (byte) 113,
        (byte) 13,
        (byte) 246,
        (byte) 115,
        (byte) 210,
        (byte) 201,
        (byte) 210,
        (byte) 62,
        (byte) 71,
        (byte) 197,
        (byte) 121,
        (byte) 47,
        (byte) 53,
        (byte) 43,
        (byte) 90,
        (byte) 41,
        (byte) 228,
        (byte) 95,
        (byte) 97,
        (byte) 145,
        (byte) 72,
        (byte) 245,
        (byte) 78,
        (byte) 222,
        (byte) 82,
        (byte) 11
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[38];
    byte[] numArray5 = new byte[38]
    {
      byte.MaxValue,
      (byte) 36,
      (byte) 241,
      (byte) 9,
      (byte) 254,
      (byte) 86,
      (byte) 23,
      (byte) 244,
      (byte) 33,
      (byte) 92,
      (byte) 29,
      (byte) 147,
      (byte) 150,
      (byte) 2,
      (byte) 72,
      (byte) 142,
      (byte) 22,
      (byte) 228,
      (byte) 205,
      (byte) 0,
      byte.MaxValue,
      (byte) 190,
      (byte) 121,
      (byte) 95,
      (byte) 154,
      (byte) 35,
      (byte) 48 /*0x30*/,
      (byte) 44,
      (byte) 168,
      (byte) 247,
      (byte) 5,
      (byte) 178,
      (byte) 43,
      (byte) 201,
      (byte) 186,
      (byte) 205,
      (byte) 44,
      (byte) 149
    };
    byte[] numArray6 = new byte[38];
    numArray6[32 /*0x20*/] = (byte) 232;
    numArray6[3] = (byte) 24;
    numArray6[2] = (byte) 254;
    numArray6[14] = (byte) 80 /*0x50*/;
    numArray6[5] = (byte) 49;
    numArray6[10] = (byte) 229;
    numArray6[6] = (byte) 75;
    numArray6[7] = (byte) 243;
    numArray6[8] = (byte) 6;
    numArray6[9] = (byte) 118;
    numArray6[21] = (byte) 81;
    numArray6[11] = (byte) 135;
    numArray6[29] = (byte) 143;
    numArray6[12] = (byte) 225;
    numArray6[16 /*0x10*/] = (byte) 34;
    numArray6[15] = (byte) 24;
    numArray6[23] = (byte) 196;
    numArray6[17] = (byte) 3;
    numArray6[31 /*0x1F*/] = (byte) 145;
    numArray6[19] = (byte) 191;
    numArray6[20] = (byte) 77;
    numArray6[0] = (byte) 176 /*0xB0*/;
    numArray6[22] = (byte) 61;
    numArray6[30] = (byte) 210;
    numArray6[24] = (byte) 252;
    numArray6[26] = (byte) 28;
    numArray6[1] = (byte) 129;
    numArray6[34] = (byte) 195;
    numArray6[28] = (byte) 29;
    numArray6[35] = (byte) 163;
    numArray6[37] = (byte) 2;
    numArray6[4] = (byte) 217;
    numArray6[25] = (byte) 82;
    numArray6[33] = (byte) 55;
    numArray6[13] = (byte) 48 /*0x30*/;
    numArray6[18] = (byte) 218;
    numArray6[36] = (byte) 136;
    numArray6[27] = (byte) 18;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 38);
    for (int index = 0; index < 38; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
