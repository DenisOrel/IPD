// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19306
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19306
{
  private static byte[] sspq = new byte[102]
  {
    (byte) 99,
    (byte) 111,
    (byte) 82,
    (byte) 144 /*0x90*/,
    byte.MaxValue,
    (byte) 51,
    (byte) 76,
    (byte) 93,
    (byte) 30,
    (byte) 25,
    (byte) 138,
    (byte) 22,
    (byte) 158,
    (byte) 141,
    (byte) 91,
    (byte) 195,
    (byte) 249,
    (byte) 25,
    (byte) 198,
    (byte) 87,
    (byte) 209,
    (byte) 81,
    (byte) 222,
    (byte) 40,
    (byte) 146,
    (byte) 84,
    (byte) 16 /*0x10*/,
    (byte) 174,
    (byte) 171,
    (byte) 214,
    (byte) 135,
    (byte) 209,
    (byte) 232,
    (byte) 146,
    (byte) 174,
    (byte) 38,
    (byte) 0,
    (byte) 222,
    (byte) 111,
    (byte) 106,
    (byte) 109,
    (byte) 51,
    (byte) 170,
    (byte) 99,
    (byte) 139,
    (byte) 165,
    (byte) 59,
    (byte) 217,
    (byte) 168,
    (byte) 97,
    (byte) 107,
    (byte) 39,
    (byte) 229,
    (byte) 32 /*0x20*/,
    (byte) 233,
    (byte) 60,
    (byte) 147,
    (byte) 171,
    (byte) 26,
    (byte) 226,
    (byte) 98,
    (byte) 144 /*0x90*/,
    (byte) 144 /*0x90*/,
    (byte) 48 /*0x30*/,
    (byte) 127 /*0x7F*/,
    (byte) 72,
    (byte) 206,
    (byte) 48 /*0x30*/,
    (byte) 91,
    (byte) 58,
    (byte) 25,
    (byte) 134,
    (byte) 160 /*0xA0*/,
    (byte) 6,
    (byte) 17,
    (byte) 230,
    (byte) 182,
    (byte) 211,
    (byte) 178,
    (byte) 22,
    (byte) 151,
    (byte) 118,
    (byte) 30,
    (byte) 20,
    (byte) 40,
    (byte) 109,
    (byte) 194,
    (byte) 156,
    (byte) 169,
    (byte) 175,
    (byte) 2,
    (byte) 188,
    (byte) 194,
    (byte) 79,
    (byte) 105,
    (byte) 214,
    (byte) 184,
    (byte) 19,
    (byte) 32 /*0x20*/,
    (byte) 163,
    (byte) 107,
    (byte) 181
  };
  private static byte[] sspr = new byte[102]
  {
    (byte) 75,
    (byte) 73,
    (byte) 221,
    (byte) 64 /*0x40*/,
    (byte) 235,
    (byte) 70,
    (byte) 19,
    (byte) 124,
    (byte) 35,
    (byte) 80 /*0x50*/,
    (byte) 66,
    (byte) 211,
    (byte) 157,
    (byte) 151,
    (byte) 60,
    (byte) 160 /*0xA0*/,
    (byte) 236,
    byte.MaxValue,
    (byte) 35,
    (byte) 244,
    (byte) 3,
    (byte) 62,
    (byte) 152,
    (byte) 12,
    (byte) 101,
    (byte) 221,
    (byte) 183,
    (byte) 36,
    (byte) 156,
    (byte) 92,
    (byte) 42,
    (byte) 153,
    (byte) 222,
    (byte) 53,
    (byte) 71,
    (byte) 222,
    (byte) 253,
    (byte) 130,
    (byte) 208 /*0xD0*/,
    (byte) 132,
    (byte) 129,
    (byte) 170,
    (byte) 198,
    (byte) 226,
    (byte) 65,
    (byte) 32 /*0x20*/,
    (byte) 119,
    (byte) 169,
    (byte) 113,
    (byte) 102,
    (byte) 31 /*0x1F*/,
    (byte) 177,
    (byte) 109,
    (byte) 213,
    (byte) 254,
    (byte) 99,
    (byte) 188,
    (byte) 15,
    (byte) 83,
    (byte) 173,
    (byte) 169,
    (byte) 125,
    (byte) 136,
    (byte) 57,
    (byte) 209,
    (byte) 25,
    (byte) 141,
    (byte) 76,
    (byte) 187,
    (byte) 34,
    (byte) 125,
    (byte) 114,
    (byte) 210,
    (byte) 87,
    (byte) 180,
    (byte) 170,
    (byte) 205,
    (byte) 177,
    (byte) 72,
    (byte) 35,
    (byte) 153,
    (byte) 58,
    (byte) 22,
    (byte) 213,
    (byte) 209,
    (byte) 106,
    (byte) 151,
    (byte) 56,
    (byte) 198,
    (byte) 82,
    (byte) 54,
    (byte) 140,
    (byte) 146,
    (byte) 232,
    (byte) 244,
    (byte) 61,
    (byte) 68,
    (byte) 19,
    (byte) 198,
    (byte) 42,
    (byte) 142,
    (byte) 147
  };

  internal static string ssp_techcard_19307()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[2] = (byte) 200;
      numArray2[5] = (byte) 4;
      numArray2[3] = (byte) 16 /*0x10*/;
      numArray2[1] = (byte) 205;
      numArray2[7] = (byte) 202;
      numArray2[4] = (byte) 136;
      numArray2[6] = (byte) 125;
      numArray2[0] = (byte) 59;
      numArray2[8] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 223;
      numArray2[10] = (byte) 46;
      numArray2[11] = (byte) 172;
      numArray2[12] = (byte) 254;
      numArray2[13] = (byte) 46;
      numArray2[14] = (byte) 89;
      numArray2[15] = (byte) 225;
      numArray2[16 /*0x10*/] = (byte) 7;
      numArray2[17] = (byte) 118;
      numArray2[18] = (byte) 211;
      byte[] numArray3 = new byte[19];
      numArray3[6] = (byte) 157;
      numArray3[12] = (byte) 137;
      numArray3[2] = (byte) 5;
      numArray3[3] = (byte) 129;
      numArray3[4] = (byte) 164;
      numArray3[5] = (byte) 129;
      numArray3[11] = (byte) 186;
      numArray3[13] = (byte) 227;
      numArray3[8] = (byte) 251;
      numArray3[9] = (byte) 170;
      numArray3[1] = (byte) 218;
      numArray3[15] = (byte) 81;
      numArray3[0] = (byte) 35;
      numArray3[14] = (byte) 230;
      numArray3[10] = (byte) 1;
      numArray3[18] = (byte) 105;
      numArray3[16 /*0x10*/] = (byte) 77;
      numArray3[7] = (byte) 11;
      numArray3[17] = (byte) 34;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_19306.sspq, 0, (Array) numArray4, 0, 54);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19306.sspr, 0, (Array) numArray4, 0, 54);
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
      (byte) 208 /*0xD0*/,
      (byte) 18,
      (byte) 144 /*0x90*/,
      (byte) 42,
      (byte) 254,
      (byte) 171,
      (byte) 35,
      (byte) 157,
      (byte) 178,
      (byte) 71,
      (byte) 145,
      (byte) 184,
      (byte) 45,
      (byte) 11,
      (byte) 223,
      (byte) 52,
      (byte) 194,
      (byte) 26,
      (byte) 169
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 113,
      (byte) 157,
      (byte) 126,
      (byte) 27,
      (byte) 102,
      (byte) 162,
      (byte) 13,
      (byte) 84,
      (byte) 193,
      (byte) 203,
      (byte) 190,
      (byte) 84,
      (byte) 138,
      (byte) 153,
      (byte) 187,
      (byte) 190,
      (byte) 187,
      (byte) 75,
      (byte) 116
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19308()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[12] = (byte) 156;
      numArray2[0] = (byte) 122;
      numArray2[3] = (byte) 25;
      numArray2[9] = (byte) 236;
      numArray2[2] = (byte) 236;
      numArray2[4] = (byte) 55;
      numArray2[6] = (byte) 74;
      numArray2[7] = (byte) 107;
      numArray2[14] = (byte) 214;
      numArray2[15] = (byte) 224 /*0xE0*/;
      numArray2[10] = (byte) 199;
      numArray2[1] = (byte) 157;
      numArray2[5] = (byte) 6;
      numArray2[13] = (byte) 149;
      numArray2[8] = (byte) 95;
      numArray2[11] = (byte) 194;
      numArray2[16 /*0x10*/] = (byte) 195;
      numArray2[17] = (byte) 71;
      byte[] numArray3 = new byte[18];
      numArray3[14] = (byte) 47;
      numArray3[1] = (byte) 60;
      numArray3[7] = (byte) 29;
      numArray3[16 /*0x10*/] = (byte) 27;
      numArray3[10] = (byte) 27;
      numArray3[5] = (byte) 64 /*0x40*/;
      numArray3[6] = (byte) 112 /*0x70*/;
      numArray3[4] = (byte) 10;
      numArray3[3] = (byte) 129;
      numArray3[0] = (byte) 83;
      numArray3[12] = (byte) 145;
      numArray3[11] = (byte) 133;
      numArray3[2] = (byte) 10;
      numArray3[13] = (byte) 215;
      numArray3[17] = (byte) 76;
      numArray3[15] = (byte) 130;
      numArray3[8] = (byte) 41;
      numArray3[9] = (byte) 229;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_19306.sspq, 54, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19306.sspr, 54, (Array) numArray4, 0, 48 /*0x30*/);
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
    byte[] numArray5 = new byte[18];
    byte[] numArray6 = new byte[18]
    {
      (byte) 184,
      (byte) 222,
      (byte) 4,
      (byte) 188,
      (byte) 7,
      (byte) 196,
      (byte) 99,
      (byte) 3,
      (byte) 133,
      (byte) 191,
      (byte) 248,
      (byte) 4,
      (byte) 74,
      (byte) 236,
      (byte) 113,
      (byte) 123,
      (byte) 7,
      (byte) 17
    };
    byte[] numArray7 = new byte[18];
    numArray7[4] = (byte) 120;
    numArray7[1] = (byte) 57;
    numArray7[9] = (byte) 95;
    numArray7[2] = (byte) 131;
    numArray7[8] = (byte) 40;
    numArray7[5] = (byte) 201;
    numArray7[3] = (byte) 196;
    numArray7[7] = (byte) 194;
    numArray7[17] = (byte) 133;
    numArray7[13] = (byte) 109;
    numArray7[12] = (byte) 2;
    numArray7[0] = (byte) 62;
    numArray7[11] = (byte) 221;
    numArray7[6] = (byte) 116;
    numArray7[14] = (byte) 67;
    numArray7[15] = (byte) 121;
    numArray7[16 /*0x10*/] = (byte) 217;
    numArray7[10] = (byte) 138;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19309()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[6] = (byte) 4;
      numArray2[2] = (byte) 41;
      numArray2[0] = (byte) 57;
      numArray2[3] = (byte) 66;
      numArray2[9] = (byte) 61;
      numArray2[4] = (byte) 196;
      numArray2[16 /*0x10*/] = (byte) 167;
      numArray2[1] = (byte) 165;
      numArray2[8] = (byte) 100;
      numArray2[7] = (byte) 124;
      numArray2[10] = (byte) 11;
      numArray2[17] = (byte) 167;
      numArray2[5] = (byte) 190;
      numArray2[12] = (byte) 28;
      numArray2[14] = (byte) 45;
      numArray2[15] = (byte) 11;
      numArray2[11] = (byte) 14;
      numArray2[13] = (byte) 57;
      byte[] numArray3 = new byte[18]
      {
        (byte) 137,
        (byte) 165,
        (byte) 141,
        (byte) 52,
        (byte) 241,
        (byte) 114,
        (byte) 2,
        (byte) 139,
        (byte) 116,
        (byte) 110,
        (byte) 191,
        (byte) 116,
        (byte) 186,
        (byte) 240 /*0xF0*/,
        (byte) 128 /*0x80*/,
        (byte) 93,
        (byte) 205,
        (byte) 63 /*0x3F*/
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[11] = (byte) 152;
    numArray5[1] = (byte) 76;
    numArray5[2] = (byte) 33;
    numArray5[10] = (byte) 178;
    numArray5[4] = (byte) 239;
    numArray5[5] = (byte) 151;
    numArray5[12] = (byte) 104;
    numArray5[3] = (byte) 214;
    numArray5[9] = (byte) 180;
    numArray5[8] = (byte) 112 /*0x70*/;
    numArray5[15] = (byte) 215;
    numArray5[7] = (byte) 242;
    numArray5[0] = (byte) 5;
    numArray5[6] = (byte) 165;
    numArray5[14] = (byte) 175;
    numArray5[13] = (byte) 115;
    numArray5[16 /*0x10*/] = (byte) 17;
    numArray5[17] = (byte) 207;
    byte[] numArray6 = new byte[18]
    {
      (byte) 134,
      (byte) 216,
      (byte) 7,
      (byte) 58,
      (byte) 119,
      (byte) 60,
      (byte) 162,
      (byte) 251,
      (byte) 238,
      (byte) 31 /*0x1F*/,
      (byte) 248,
      (byte) 121,
      (byte) 150,
      (byte) 54,
      (byte) 202,
      (byte) 254,
      (byte) 221,
      (byte) 139
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19310()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[6] = (byte) 1;
      numArray2[1] = (byte) 245;
      numArray2[2] = (byte) 232;
      numArray2[9] = (byte) 176 /*0xB0*/;
      numArray2[13] = (byte) 189;
      numArray2[14] = (byte) 187;
      numArray2[17] = (byte) 211;
      numArray2[7] = (byte) 40;
      numArray2[4] = (byte) 124;
      numArray2[0] = (byte) 102;
      numArray2[10] = (byte) 230;
      numArray2[3] = (byte) 180;
      numArray2[12] = (byte) 60;
      numArray2[8] = (byte) 251;
      numArray2[11] = (byte) 18;
      numArray2[5] = (byte) 151;
      numArray2[16 /*0x10*/] = (byte) 176 /*0xB0*/;
      numArray2[15] = (byte) 160 /*0xA0*/;
      byte[] numArray3 = new byte[18]
      {
        (byte) 184,
        (byte) 109,
        (byte) 37,
        (byte) 193,
        (byte) 86,
        (byte) 9,
        (byte) 112 /*0x70*/,
        (byte) 131,
        (byte) 134,
        (byte) 133,
        (byte) 202,
        (byte) 54,
        (byte) 197,
        (byte) 199,
        (byte) 194,
        (byte) 99,
        (byte) 166,
        (byte) 167
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 155,
      (byte) 209,
      (byte) 72,
      (byte) 102,
      (byte) 227,
      (byte) 90,
      (byte) 57,
      (byte) 75,
      (byte) 245,
      (byte) 85,
      (byte) 82,
      (byte) 184,
      (byte) 49,
      (byte) 174,
      (byte) 201,
      (byte) 35,
      (byte) 36,
      (byte) 150
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 195,
      (byte) 28,
      (byte) 119,
      (byte) 244,
      (byte) 172,
      (byte) 112 /*0x70*/,
      (byte) 17,
      (byte) 249,
      (byte) 92,
      (byte) 204,
      (byte) 239,
      (byte) 237,
      (byte) 171,
      (byte) 96 /*0x60*/,
      (byte) 212,
      (byte) 20,
      (byte) 191,
      (byte) 47
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19311()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[38];
      byte[] numArray2 = new byte[38];
      numArray2[0] = (byte) 169;
      numArray2[1] = (byte) 168;
      numArray2[33] = (byte) 233;
      numArray2[3] = (byte) 223;
      numArray2[4] = (byte) 38;
      numArray2[5] = (byte) 40;
      numArray2[12] = (byte) 6;
      numArray2[7] = (byte) 237;
      numArray2[20] = (byte) 239;
      numArray2[34] = (byte) 39;
      numArray2[18] = (byte) 46;
      numArray2[2] = (byte) 181;
      numArray2[30] = (byte) 194;
      numArray2[13] = (byte) 75;
      numArray2[16 /*0x10*/] = (byte) 122;
      numArray2[11] = (byte) 2;
      numArray2[17] = (byte) 212;
      numArray2[32 /*0x20*/] = (byte) 127 /*0x7F*/;
      numArray2[36] = (byte) 177;
      numArray2[19] = (byte) 222;
      numArray2[24] = (byte) 159;
      numArray2[21] = (byte) 2;
      numArray2[9] = (byte) 36;
      numArray2[23] = (byte) 141;
      numArray2[14] = (byte) 222;
      numArray2[15] = (byte) 219;
      numArray2[26] = (byte) 31 /*0x1F*/;
      numArray2[27] = (byte) 217;
      numArray2[28] = (byte) 171;
      numArray2[22] = (byte) 45;
      numArray2[8] = (byte) 212;
      numArray2[31 /*0x1F*/] = (byte) 155;
      numArray2[10] = (byte) 201;
      numArray2[25] = (byte) 18;
      numArray2[29] = (byte) 79;
      numArray2[35] = (byte) 160 /*0xA0*/;
      numArray2[6] = (byte) 63 /*0x3F*/;
      numArray2[37] = (byte) 4;
      byte[] numArray3 = new byte[38]
      {
        (byte) 168,
        (byte) 206,
        (byte) 76,
        (byte) 175,
        (byte) 128 /*0x80*/,
        (byte) 203,
        (byte) 106,
        (byte) 101,
        (byte) 104,
        (byte) 214,
        (byte) 141,
        (byte) 209,
        (byte) 107,
        (byte) 251,
        (byte) 223,
        (byte) 219,
        (byte) 151,
        (byte) 29,
        (byte) 170,
        (byte) 114,
        (byte) 136,
        (byte) 44,
        (byte) 174,
        (byte) 115,
        (byte) 238,
        (byte) 185,
        (byte) 149,
        (byte) 33,
        (byte) 23,
        (byte) 74,
        (byte) 96 /*0x60*/,
        (byte) 53,
        (byte) 181,
        (byte) 184,
        (byte) 5,
        (byte) 124,
        (byte) 186,
        (byte) 166
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[38];
    byte[] numArray5 = new byte[38]
    {
      (byte) 112 /*0x70*/,
      (byte) 106,
      (byte) 161,
      (byte) 149,
      (byte) 99,
      (byte) 51,
      (byte) 221,
      (byte) 126,
      (byte) 11,
      (byte) 39,
      (byte) 128 /*0x80*/,
      (byte) 37,
      (byte) 137,
      (byte) 197,
      (byte) 32 /*0x20*/,
      (byte) 7,
      (byte) 165,
      (byte) 198,
      (byte) 253,
      (byte) 202,
      (byte) 193,
      (byte) 172,
      (byte) 138,
      (byte) 89,
      (byte) 5,
      (byte) 29,
      (byte) 165,
      (byte) 69,
      byte.MaxValue,
      (byte) 161,
      (byte) 218,
      (byte) 37,
      (byte) 121,
      (byte) 137,
      (byte) 206,
      (byte) 169,
      (byte) 103,
      (byte) 208 /*0xD0*/
    };
    byte[] numArray6 = new byte[38]
    {
      (byte) 188,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 130,
      (byte) 166,
      (byte) 33,
      (byte) 147,
      (byte) 209,
      (byte) 51,
      (byte) 217,
      (byte) 27,
      (byte) 190,
      (byte) 61,
      (byte) 24,
      (byte) 231,
      (byte) 11,
      (byte) 40,
      (byte) 199,
      (byte) 74,
      (byte) 170,
      (byte) 133,
      (byte) 245,
      (byte) 38,
      (byte) 38,
      (byte) 76,
      (byte) 38,
      (byte) 152,
      (byte) 17,
      (byte) 98,
      (byte) 35,
      (byte) 183,
      (byte) 249,
      (byte) 69,
      (byte) 107,
      (byte) 151,
      (byte) 96 /*0x60*/,
      (byte) 193,
      (byte) 171
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 38);
    for (int index = 0; index < 38; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
