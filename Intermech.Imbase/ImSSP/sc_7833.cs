// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7833
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7833
{
  private static byte[] sspq = new byte[49]
  {
    (byte) 14,
    (byte) 108,
    (byte) 4,
    (byte) 143,
    (byte) 97,
    (byte) 9,
    (byte) 219,
    (byte) 157,
    (byte) 168,
    (byte) 87,
    (byte) 240 /*0xF0*/,
    (byte) 182,
    (byte) 163,
    (byte) 189,
    (byte) 180,
    (byte) 118,
    (byte) 30,
    (byte) 75,
    (byte) 88,
    (byte) 4,
    (byte) 57,
    (byte) 109,
    (byte) 57,
    (byte) 222,
    (byte) 132,
    (byte) 250,
    (byte) 93,
    (byte) 247,
    (byte) 104,
    (byte) 137,
    (byte) 212,
    (byte) 239,
    (byte) 123,
    (byte) 119,
    (byte) 49,
    byte.MaxValue,
    (byte) 72,
    (byte) 246,
    (byte) 247,
    (byte) 177,
    (byte) 220,
    (byte) 61,
    (byte) 73,
    (byte) 2,
    (byte) 213,
    (byte) 193,
    (byte) 183,
    (byte) 46,
    (byte) 153
  };
  private static byte[] sspr = new byte[49]
  {
    (byte) 224 /*0xE0*/,
    (byte) 161,
    (byte) 211,
    (byte) 213,
    (byte) 155,
    (byte) 186,
    (byte) 29,
    (byte) 2,
    (byte) 163,
    (byte) 37,
    (byte) 172,
    (byte) 145,
    (byte) 129,
    (byte) 96 /*0x60*/,
    (byte) 200,
    (byte) 187,
    (byte) 119,
    (byte) 25,
    (byte) 29,
    (byte) 7,
    (byte) 216,
    (byte) 7,
    (byte) 161,
    (byte) 215,
    (byte) 154,
    (byte) 253,
    (byte) 23,
    (byte) 90,
    (byte) 223,
    (byte) 218,
    (byte) 165,
    (byte) 30,
    (byte) 166,
    (byte) 209,
    byte.MaxValue,
    (byte) 13,
    (byte) 63 /*0x3F*/,
    (byte) 95,
    (byte) 252,
    (byte) 187,
    (byte) 125,
    (byte) 220,
    (byte) 205,
    (byte) 175,
    (byte) 228,
    (byte) 197,
    (byte) 202,
    (byte) 98,
    (byte) 127 /*0x7F*/
  };

  internal static string ssp_imbase_7834()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35]
      {
        (byte) 75,
        (byte) 126,
        (byte) 4,
        (byte) 11,
        (byte) 188,
        (byte) 46,
        (byte) 183,
        (byte) 237,
        (byte) 158,
        (byte) 198,
        (byte) 252,
        (byte) 245,
        (byte) 214,
        (byte) 181,
        (byte) 61,
        (byte) 213,
        (byte) 202,
        (byte) 210,
        (byte) 142,
        (byte) 139,
        (byte) 173,
        (byte) 197,
        (byte) 171,
        (byte) 189,
        (byte) 155,
        (byte) 245,
        (byte) 66,
        (byte) 105,
        (byte) 156,
        (byte) 245,
        (byte) 58,
        (byte) 67,
        (byte) 61,
        (byte) 32 /*0x20*/,
        (byte) 147
      };
      byte[] numArray3 = new byte[35];
      numArray3[5] = (byte) 250;
      numArray3[24] = (byte) 28;
      numArray3[0] = (byte) 51;
      numArray3[30] = (byte) 36;
      numArray3[13] = (byte) 75;
      numArray3[12] = (byte) 193;
      numArray3[6] = (byte) 35;
      numArray3[26] = (byte) 37;
      numArray3[14] = (byte) 135;
      numArray3[7] = (byte) 19;
      numArray3[33] = (byte) 219;
      numArray3[11] = (byte) 61;
      numArray3[28] = (byte) 46;
      numArray3[4] = (byte) 205;
      numArray3[1] = (byte) 110;
      numArray3[9] = (byte) 250;
      numArray3[16 /*0x10*/] = (byte) 213;
      numArray3[10] = (byte) 163;
      numArray3[18] = (byte) 75;
      numArray3[19] = (byte) 16 /*0x10*/;
      numArray3[20] = (byte) 165;
      numArray3[21] = (byte) 0;
      numArray3[22] = (byte) 72;
      numArray3[3] = (byte) 158;
      numArray3[23] = (byte) 176 /*0xB0*/;
      numArray3[25] = (byte) 144 /*0x90*/;
      numArray3[15] = (byte) 65;
      numArray3[27] = (byte) 45;
      numArray3[8] = (byte) 146;
      numArray3[29] = (byte) 12;
      numArray3[2] = (byte) 121;
      numArray3[31 /*0x1F*/] = (byte) 167;
      numArray3[32 /*0x20*/] = (byte) 20;
      numArray3[17] = (byte) 167;
      numArray3[34] = (byte) 17;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 163,
      (byte) 157,
      (byte) 89,
      (byte) 50,
      (byte) 178,
      (byte) 228,
      (byte) 227,
      (byte) 229,
      (byte) 180,
      (byte) 36,
      (byte) 168,
      (byte) 172,
      (byte) 107,
      (byte) 246,
      (byte) 18,
      (byte) 16 /*0x10*/,
      (byte) 13,
      (byte) 215,
      (byte) 61,
      (byte) 125,
      (byte) 60,
      (byte) 254,
      (byte) 160 /*0xA0*/,
      (byte) 3,
      (byte) 233,
      (byte) 109,
      (byte) 31 /*0x1F*/,
      byte.MaxValue,
      (byte) 90,
      (byte) 108,
      (byte) 178,
      (byte) 162,
      (byte) 108,
      byte.MaxValue,
      (byte) 169
    };
    byte[] numArray6 = new byte[35]
    {
      (byte) 144 /*0x90*/,
      (byte) 251,
      (byte) 134,
      (byte) 240 /*0xF0*/,
      (byte) 147,
      (byte) 253,
      (byte) 102,
      (byte) 237,
      (byte) 17,
      (byte) 152,
      (byte) 165,
      (byte) 4,
      (byte) 243,
      (byte) 184,
      (byte) 199,
      (byte) 60,
      (byte) 165,
      (byte) 182,
      (byte) 113,
      (byte) 116,
      (byte) 104,
      (byte) 46,
      (byte) 172,
      (byte) 248,
      (byte) 21,
      (byte) 17,
      (byte) 169,
      (byte) 121,
      (byte) 39,
      (byte) 144 /*0x90*/,
      (byte) 123,
      (byte) 95,
      (byte) 35,
      (byte) 60,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7835()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 98,
        (byte) 140,
        (byte) 225,
        (byte) 56,
        (byte) 40,
        (byte) 168,
        (byte) 253,
        (byte) 32 /*0x20*/,
        (byte) 195,
        (byte) 164,
        (byte) 165,
        (byte) 19,
        (byte) 187,
        (byte) 20,
        (byte) 94,
        (byte) 147,
        (byte) 164,
        (byte) 252,
        (byte) 85,
        (byte) 73,
        (byte) 68,
        (byte) 196,
        (byte) 228,
        (byte) 138,
        (byte) 71,
        (byte) 114,
        (byte) 5,
        (byte) 63 /*0x3F*/,
        (byte) 107,
        (byte) 76,
        (byte) 134,
        (byte) 89,
        (byte) 186,
        (byte) 183,
        (byte) 218,
        (byte) 241,
        (byte) 90,
        (byte) 198,
        (byte) 120,
        (byte) 90,
        (byte) 236,
        (byte) 77,
        (byte) 92,
        (byte) 90
      };
      byte[] numArray3 = new byte[44]
      {
        (byte) 8,
        (byte) 23,
        (byte) 251,
        (byte) 97,
        (byte) 169,
        (byte) 215,
        (byte) 31 /*0x1F*/,
        (byte) 119,
        (byte) 164,
        (byte) 219,
        (byte) 241,
        (byte) 17,
        (byte) 65,
        (byte) 33,
        (byte) 79,
        (byte) 171,
        (byte) 108,
        (byte) 22,
        (byte) 239,
        (byte) 96 /*0x60*/,
        (byte) 71,
        (byte) 253,
        (byte) 14,
        (byte) 197,
        (byte) 9,
        (byte) 169,
        (byte) 83,
        (byte) 159,
        (byte) 144 /*0x90*/,
        (byte) 129,
        (byte) 76,
        (byte) 39,
        (byte) 3,
        (byte) 63 /*0x3F*/,
        (byte) 28,
        (byte) 123,
        (byte) 6,
        (byte) 110,
        (byte) 244,
        (byte) 4,
        (byte) 15,
        (byte) 121,
        (byte) 156,
        (byte) 33
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_7833.sspq, 0, (Array) numArray4, 0, 49);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7833.sspr, 0, (Array) numArray4, 0, 49);
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
    byte[] numArray5 = new byte[44];
    byte[] numArray6 = new byte[44];
    numArray6[4] = (byte) 242;
    numArray6[1] = (byte) 60;
    numArray6[2] = (byte) 105;
    numArray6[22] = (byte) 39;
    numArray6[25] = (byte) 39;
    numArray6[5] = (byte) 84;
    numArray6[6] = (byte) 160 /*0xA0*/;
    numArray6[7] = (byte) 51;
    numArray6[8] = (byte) 37;
    numArray6[10] = (byte) 102;
    numArray6[32 /*0x20*/] = (byte) 46;
    numArray6[11] = (byte) 172;
    numArray6[35] = (byte) 77;
    numArray6[43] = (byte) 235;
    numArray6[14] = (byte) 30;
    numArray6[15] = (byte) 130;
    numArray6[42] = (byte) 34;
    numArray6[20] = (byte) 180;
    numArray6[17] = (byte) 59;
    numArray6[19] = (byte) 140;
    numArray6[33] = (byte) 94;
    numArray6[21] = (byte) 46;
    numArray6[39] = (byte) 35;
    numArray6[16 /*0x10*/] = (byte) 1;
    numArray6[24] = (byte) 167;
    numArray6[13] = (byte) 53;
    numArray6[26] = (byte) 252;
    numArray6[27] = (byte) 36;
    numArray6[31 /*0x1F*/] = (byte) 67;
    numArray6[12] = (byte) 227;
    numArray6[30] = (byte) 194;
    numArray6[3] = byte.MaxValue;
    numArray6[37] = (byte) 239;
    numArray6[23] = (byte) 84;
    numArray6[34] = (byte) 130;
    numArray6[18] = (byte) 43;
    numArray6[36] = (byte) 107;
    numArray6[28] = (byte) 54;
    numArray6[38] = (byte) 136;
    numArray6[41] = (byte) 203;
    numArray6[40] = (byte) 144 /*0x90*/;
    numArray6[9] = (byte) 11;
    numArray6[29] = (byte) 8;
    numArray6[0] = (byte) 229;
    byte[] numArray7 = new byte[44];
    numArray7[2] = (byte) 184;
    numArray7[30] = (byte) 158;
    numArray7[3] = (byte) 61;
    numArray7[18] = (byte) 28;
    numArray7[14] = (byte) 106;
    numArray7[1] = (byte) 105;
    numArray7[6] = (byte) 197;
    numArray7[7] = (byte) 54;
    numArray7[38] = (byte) 72;
    numArray7[8] = (byte) 37;
    numArray7[35] = (byte) 196;
    numArray7[13] = (byte) 218;
    numArray7[12] = (byte) 55;
    numArray7[39] = (byte) 82;
    numArray7[24] = (byte) 76;
    numArray7[15] = (byte) 0;
    numArray7[10] = (byte) 30;
    numArray7[17] = (byte) 17;
    numArray7[20] = (byte) 69;
    numArray7[19] = (byte) 35;
    numArray7[5] = (byte) 153;
    numArray7[21] = (byte) 239;
    numArray7[22] = (byte) 134;
    numArray7[36] = (byte) 1;
    numArray7[9] = (byte) 239;
    numArray7[25] = (byte) 201;
    numArray7[42] = (byte) 164;
    numArray7[27] = (byte) 183;
    numArray7[0] = (byte) 193;
    numArray7[29] = (byte) 167;
    numArray7[23] = (byte) 168;
    numArray7[31 /*0x1F*/] = (byte) 137;
    numArray7[40] = (byte) 23;
    numArray7[16 /*0x10*/] = (byte) 230;
    numArray7[11] = (byte) 52;
    numArray7[28] = (byte) 43;
    numArray7[26] = (byte) 245;
    numArray7[37] = (byte) 235;
    numArray7[32 /*0x20*/] = (byte) 208 /*0xD0*/;
    numArray7[33] = (byte) 31 /*0x1F*/;
    numArray7[34] = (byte) 91;
    numArray7[41] = (byte) 251;
    numArray7[43] = (byte) 131;
    numArray7[4] = (byte) 186;
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imbase_7836()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 159,
        (byte) 159,
        (byte) 77,
        (byte) 111,
        (byte) 103,
        (byte) 50,
        (byte) 229,
        (byte) 126,
        (byte) 30,
        (byte) 97,
        (byte) 4,
        (byte) 226,
        (byte) 34,
        (byte) 105,
        (byte) 145,
        (byte) 135,
        (byte) 28,
        (byte) 149,
        (byte) 0,
        (byte) 149,
        (byte) 103,
        (byte) 109,
        (byte) 194,
        (byte) 167,
        (byte) 74,
        (byte) 222,
        (byte) 73
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 177,
        (byte) 0,
        (byte) 53,
        (byte) 69,
        (byte) 128 /*0x80*/,
        (byte) 1,
        (byte) 53,
        (byte) 23,
        (byte) 41,
        (byte) 74,
        (byte) 7,
        (byte) 183,
        (byte) 69,
        (byte) 44,
        (byte) 139,
        (byte) 157,
        (byte) 211,
        (byte) 236,
        (byte) 186,
        (byte) 233,
        (byte) 170,
        (byte) 151,
        (byte) 199,
        (byte) 151,
        (byte) 60,
        (byte) 7,
        (byte) 17
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 61,
      (byte) 251,
      (byte) 153,
      (byte) 222,
      (byte) 194,
      (byte) 226,
      (byte) 249,
      (byte) 121,
      (byte) 165,
      (byte) 58,
      (byte) 12,
      (byte) 140,
      (byte) 201,
      (byte) 53,
      (byte) 189,
      (byte) 150,
      (byte) 210,
      (byte) 209,
      (byte) 86,
      (byte) 146,
      (byte) 233,
      (byte) 4,
      (byte) 85,
      (byte) 134,
      (byte) 85,
      (byte) 50,
      (byte) 220
    };
    byte[] numArray6 = new byte[27]
    {
      (byte) 78,
      (byte) 161,
      (byte) 119,
      (byte) 207,
      (byte) 73,
      (byte) 61,
      (byte) 108,
      (byte) 252,
      (byte) 64 /*0x40*/,
      (byte) 54,
      (byte) 148,
      (byte) 223,
      (byte) 34,
      (byte) 250,
      (byte) 196,
      (byte) 74,
      (byte) 84,
      (byte) 54,
      (byte) 109,
      (byte) 216,
      (byte) 68,
      (byte) 221,
      (byte) 205,
      (byte) 156,
      (byte) 21,
      (byte) 163,
      (byte) 87
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
