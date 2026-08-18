
// Type: ImSSP.sc_2517
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2517
{
  private static byte[] sspq = new byte[23]
  {
    (byte) 13,
    (byte) 148,
    (byte) 199,
    (byte) 58,
    (byte) 193,
    (byte) 205,
    (byte) 75,
    (byte) 235,
    (byte) 253,
    (byte) 48 /*0x30*/,
    (byte) 239,
    (byte) 50,
    (byte) 22,
    (byte) 205,
    (byte) 45,
    (byte) 184,
    (byte) 204,
    (byte) 174,
    (byte) 132,
    (byte) 198,
    (byte) 105,
    (byte) 4,
    (byte) 166
  };
  private static byte[] sspr = new byte[23]
  {
    (byte) 213,
    (byte) 7,
    (byte) 124,
    (byte) 50,
    (byte) 214,
    (byte) 88,
    (byte) 146,
    (byte) 192 /*0xC0*/,
    (byte) 164,
    (byte) 54,
    (byte) 47,
    (byte) 132,
    (byte) 192 /*0xC0*/,
    (byte) 3,
    (byte) 86,
    (byte) 13,
    (byte) 58,
    (byte) 223,
    (byte) 119,
    (byte) 43,
    (byte) 13,
    (byte) 129,
    (byte) 2
  };

  internal static string ssp_imclient_2518()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 6,
        (byte) 51,
        (byte) 241,
        (byte) 87,
        (byte) 57,
        (byte) 108,
        (byte) 31 /*0x1F*/,
        (byte) 39,
        (byte) 93,
        (byte) 94,
        (byte) 69,
        (byte) 68,
        (byte) 46,
        (byte) 67,
        (byte) 226,
        (byte) 23,
        (byte) 243,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 203,
        (byte) 91,
        (byte) 52,
        (byte) 50,
        (byte) 198,
        (byte) 237,
        (byte) 218,
        (byte) 54,
        (byte) 68,
        (byte) 127 /*0x7F*/,
        (byte) 161,
        (byte) 0,
        (byte) 114,
        (byte) 52,
        (byte) 153,
        (byte) 191,
        (byte) 186,
        (byte) 173,
        (byte) 237,
        (byte) 24,
        (byte) 129,
        (byte) 235,
        (byte) 212,
        (byte) 112 /*0x70*/
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 140,
        (byte) 211,
        (byte) 212,
        (byte) 152,
        (byte) 79,
        (byte) 167,
        (byte) 51,
        (byte) 96 /*0x60*/,
        (byte) 170,
        (byte) 105,
        (byte) 153,
        (byte) 129,
        (byte) 40,
        (byte) 8,
        (byte) 247,
        (byte) 162,
        (byte) 120,
        (byte) 166,
        (byte) 46,
        (byte) 89,
        (byte) 63 /*0x3F*/,
        (byte) 100,
        (byte) 124,
        (byte) 26,
        (byte) 143,
        (byte) 51,
        (byte) 88,
        (byte) 36,
        (byte) 117,
        (byte) 85,
        (byte) 214,
        (byte) 173,
        (byte) 68,
        (byte) 182,
        (byte) 48 /*0x30*/,
        (byte) 17,
        (byte) 71,
        (byte) 51,
        (byte) 210,
        (byte) 122,
        (byte) 135,
        (byte) 202,
        (byte) 42
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[40] = (byte) 136;
    numArray5[3] = (byte) 198;
    numArray5[33] = (byte) 250;
    numArray5[36] = (byte) 162;
    numArray5[29] = (byte) 134;
    numArray5[5] = (byte) 94;
    numArray5[6] = (byte) 234;
    numArray5[20] = (byte) 15;
    numArray5[14] = (byte) 53;
    numArray5[17] = (byte) 187;
    numArray5[10] = (byte) 0;
    numArray5[7] = (byte) 21;
    numArray5[31 /*0x1F*/] = (byte) 68;
    numArray5[32 /*0x20*/] = (byte) 0;
    numArray5[15] = (byte) 219;
    numArray5[12] = (byte) 106;
    numArray5[42] = (byte) 230;
    numArray5[1] = (byte) 164;
    numArray5[11] = (byte) 42;
    numArray5[19] = (byte) 210;
    numArray5[9] = (byte) 210;
    numArray5[21] = (byte) 148;
    numArray5[8] = (byte) 179;
    numArray5[34] = (byte) 148;
    numArray5[24] = (byte) 106;
    numArray5[25] = (byte) 6;
    numArray5[18] = (byte) 93;
    numArray5[27] = (byte) 238;
    numArray5[0] = (byte) 70;
    numArray5[23] = (byte) 39;
    numArray5[30] = (byte) 196;
    numArray5[2] = (byte) 161;
    numArray5[26] = (byte) 54;
    numArray5[22] = (byte) 132;
    numArray5[38] = (byte) 13;
    numArray5[16 /*0x10*/] = (byte) 167;
    numArray5[4] = (byte) 148;
    numArray5[37] = (byte) 184;
    numArray5[28] = (byte) 224 /*0xE0*/;
    numArray5[39] = (byte) 161;
    numArray5[13] = (byte) 94;
    numArray5[41] = (byte) 31 /*0x1F*/;
    numArray5[35] = (byte) 145;
    byte[] numArray6 = new byte[43];
    numArray6[17] = (byte) 135;
    numArray6[1] = (byte) 23;
    numArray6[15] = (byte) 43;
    numArray6[3] = (byte) 16 /*0x10*/;
    numArray6[37] = (byte) 115;
    numArray6[23] = (byte) 187;
    numArray6[6] = (byte) 164;
    numArray6[41] = (byte) 117;
    numArray6[0] = (byte) 225;
    numArray6[9] = (byte) 101;
    numArray6[22] = (byte) 155;
    numArray6[39] = (byte) 126;
    numArray6[16 /*0x10*/] = (byte) 222;
    numArray6[7] = (byte) 67;
    numArray6[14] = (byte) 193;
    numArray6[40] = (byte) 167;
    numArray6[2] = (byte) 147;
    numArray6[8] = (byte) 148;
    numArray6[18] = (byte) 144 /*0x90*/;
    numArray6[36] = (byte) 192 /*0xC0*/;
    numArray6[12] = (byte) 142;
    numArray6[21] = (byte) 163;
    numArray6[29] = (byte) 50;
    numArray6[10] = (byte) 117;
    numArray6[24] = (byte) 148;
    numArray6[13] = (byte) 111;
    numArray6[25] = (byte) 189;
    numArray6[27] = (byte) 155;
    numArray6[28] = (byte) 155;
    numArray6[32 /*0x20*/] = (byte) 167;
    numArray6[30] = (byte) 226;
    numArray6[4] = (byte) 40;
    numArray6[34] = (byte) 117;
    numArray6[33] = (byte) 202;
    numArray6[5] = (byte) 171;
    numArray6[11] = (byte) 254;
    numArray6[26] = (byte) 54;
    numArray6[20] = (byte) 204;
    numArray6[38] = (byte) 154;
    numArray6[31 /*0x1F*/] = (byte) 147;
    numArray6[19] = (byte) 124;
    numArray6[35] = (byte) 214;
    numArray6[42] = (byte) 17;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2519()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43];
      numArray2[26] = (byte) 200;
      numArray2[28] = (byte) 217;
      numArray2[21] = (byte) 246;
      numArray2[12] = (byte) 232;
      numArray2[16 /*0x10*/] = (byte) 206;
      numArray2[35] = (byte) 230;
      numArray2[6] = (byte) 27;
      numArray2[7] = (byte) 7;
      numArray2[8] = (byte) 224 /*0xE0*/;
      numArray2[25] = (byte) 117;
      numArray2[10] = (byte) 35;
      numArray2[11] = (byte) 222;
      numArray2[0] = (byte) 112 /*0x70*/;
      numArray2[13] = (byte) 30;
      numArray2[40] = (byte) 183;
      numArray2[5] = (byte) 248;
      numArray2[9] = (byte) 97;
      numArray2[42] = (byte) 38;
      numArray2[19] = (byte) 230;
      numArray2[27] = (byte) 171;
      numArray2[20] = (byte) 223;
      numArray2[17] = (byte) 52;
      numArray2[22] = (byte) 60;
      numArray2[23] = (byte) 184;
      numArray2[24] = (byte) 6;
      numArray2[1] = (byte) 141;
      numArray2[33] = (byte) 207;
      numArray2[29] = (byte) 146;
      numArray2[18] = (byte) 131;
      numArray2[2] = (byte) 113;
      numArray2[32 /*0x20*/] = (byte) 15;
      numArray2[31 /*0x1F*/] = (byte) 235;
      numArray2[4] = (byte) 250;
      numArray2[14] = (byte) 231;
      numArray2[34] = (byte) 154;
      numArray2[39] = (byte) 109;
      numArray2[36] = (byte) 153;
      numArray2[37] = (byte) 28;
      numArray2[38] = (byte) 93;
      numArray2[3] = (byte) 141;
      numArray2[30] = (byte) 239;
      numArray2[41] = (byte) 247;
      numArray2[15] = (byte) 116;
      byte[] numArray3 = new byte[43]
      {
        (byte) 77,
        (byte) 216,
        (byte) 36,
        byte.MaxValue,
        (byte) 39,
        (byte) 119,
        (byte) 18,
        (byte) 21,
        (byte) 95,
        (byte) 198,
        (byte) 79,
        (byte) 115,
        (byte) 229,
        (byte) 204,
        (byte) 115,
        (byte) 13,
        (byte) 65,
        (byte) 252,
        (byte) 240 /*0xF0*/,
        (byte) 186,
        (byte) 70,
        (byte) 13,
        (byte) 75,
        (byte) 136,
        (byte) 97,
        (byte) 188,
        (byte) 119,
        (byte) 15,
        (byte) 69,
        (byte) 244,
        (byte) 31 /*0x1F*/,
        (byte) 215,
        (byte) 36,
        (byte) 154,
        (byte) 179,
        (byte) 8,
        (byte) 59,
        (byte) 147,
        (byte) 42,
        (byte) 24,
        (byte) 70,
        (byte) 9,
        (byte) 11
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 31 /*0x1F*/,
      (byte) 149,
      (byte) 225,
      (byte) 149,
      (byte) 216,
      (byte) 60,
      (byte) 70,
      (byte) 200,
      (byte) 215,
      (byte) 72,
      (byte) 254,
      (byte) 59,
      (byte) 47,
      (byte) 109,
      (byte) 169,
      (byte) 221,
      (byte) 193,
      (byte) 175,
      (byte) 109,
      (byte) 221,
      (byte) 241,
      (byte) 170,
      (byte) 207,
      (byte) 54,
      (byte) 138,
      (byte) 88,
      (byte) 89,
      (byte) 111,
      (byte) 13,
      (byte) 47,
      (byte) 10,
      (byte) 208 /*0xD0*/,
      (byte) 204,
      (byte) 35,
      (byte) 100,
      (byte) 107,
      (byte) 250,
      (byte) 27,
      (byte) 254,
      (byte) 218,
      (byte) 232,
      (byte) 31 /*0x1F*/,
      (byte) 248
    };
    byte[] numArray6 = new byte[43];
    numArray6[6] = (byte) 39;
    numArray6[1] = (byte) 50;
    numArray6[2] = (byte) 58;
    numArray6[3] = (byte) 142;
    numArray6[4] = (byte) 45;
    numArray6[34] = (byte) 253;
    numArray6[17] = (byte) 161;
    numArray6[38] = (byte) 149;
    numArray6[8] = (byte) 218;
    numArray6[9] = (byte) 22;
    numArray6[10] = (byte) 109;
    numArray6[0] = (byte) 219;
    numArray6[12] = (byte) 251;
    numArray6[31 /*0x1F*/] = (byte) 101;
    numArray6[20] = (byte) 185;
    numArray6[15] = (byte) 177;
    numArray6[5] = (byte) 155;
    numArray6[33] = (byte) 169;
    numArray6[18] = (byte) 89;
    numArray6[26] = (byte) 253;
    numArray6[16 /*0x10*/] = (byte) 110;
    numArray6[41] = (byte) 225;
    numArray6[22] = (byte) 182;
    numArray6[23] = (byte) 9;
    numArray6[11] = (byte) 51;
    numArray6[13] = (byte) 114;
    numArray6[36] = (byte) 124;
    numArray6[42] = (byte) 102;
    numArray6[28] = (byte) 1;
    numArray6[29] = (byte) 137;
    numArray6[30] = (byte) 80 /*0x50*/;
    numArray6[19] = (byte) 186;
    numArray6[40] = (byte) 144 /*0x90*/;
    numArray6[14] = (byte) 142;
    numArray6[24] = (byte) 106;
    numArray6[21] = (byte) 24;
    numArray6[35] = (byte) 137;
    numArray6[37] = (byte) 227;
    numArray6[25] = (byte) 235;
    numArray6[32 /*0x20*/] = (byte) 153;
    numArray6[27] = (byte) 231;
    numArray6[7] = (byte) 241;
    numArray6[39] = (byte) 203;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2520()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43];
      numArray2[1] = (byte) 79;
      numArray2[39] = (byte) 227;
      numArray2[2] = (byte) 16 /*0x10*/;
      numArray2[0] = (byte) 165;
      numArray2[10] = (byte) 81;
      numArray2[5] = (byte) 17;
      numArray2[35] = (byte) 148;
      numArray2[37] = (byte) 50;
      numArray2[20] = (byte) 193;
      numArray2[9] = (byte) 86;
      numArray2[21] = (byte) 47;
      numArray2[26] = (byte) 190;
      numArray2[12] = (byte) 96 /*0x60*/;
      numArray2[13] = (byte) 24;
      numArray2[14] = (byte) 94;
      numArray2[15] = (byte) 184;
      numArray2[19] = (byte) 50;
      numArray2[17] = (byte) 102;
      numArray2[3] = (byte) 126;
      numArray2[33] = (byte) 25;
      numArray2[29] = (byte) 236;
      numArray2[4] = (byte) 247;
      numArray2[27] = (byte) 239;
      numArray2[23] = (byte) 67;
      numArray2[8] = (byte) 251;
      numArray2[25] = (byte) 242;
      numArray2[42] = (byte) 154;
      numArray2[7] = (byte) 80 /*0x50*/;
      numArray2[28] = (byte) 214;
      numArray2[30] = (byte) 86;
      numArray2[11] = (byte) 240 /*0xF0*/;
      numArray2[31 /*0x1F*/] = (byte) 199;
      numArray2[22] = (byte) 247;
      numArray2[24] = (byte) 35;
      numArray2[32 /*0x20*/] = (byte) 236;
      numArray2[6] = (byte) 101;
      numArray2[36] = (byte) 149;
      numArray2[16 /*0x10*/] = (byte) 7;
      numArray2[38] = (byte) 156;
      numArray2[34] = (byte) 152;
      numArray2[40] = (byte) 108;
      numArray2[41] = (byte) 98;
      numArray2[18] = (byte) 46;
      byte[] numArray3 = new byte[43]
      {
        (byte) 46,
        (byte) 37,
        (byte) 24,
        (byte) 162,
        (byte) 168,
        (byte) 176 /*0xB0*/,
        (byte) 214,
        (byte) 132,
        (byte) 74,
        (byte) 203,
        (byte) 170,
        (byte) 95,
        (byte) 32 /*0x20*/,
        (byte) 203,
        (byte) 77,
        (byte) 44,
        (byte) 85,
        (byte) 134,
        (byte) 190,
        (byte) 75,
        (byte) 137,
        (byte) 12,
        (byte) 167,
        (byte) 219,
        (byte) 141,
        (byte) 222,
        (byte) 10,
        (byte) 207,
        (byte) 240 /*0xF0*/,
        (byte) 163,
        (byte) 192 /*0xC0*/,
        (byte) 135,
        (byte) 195,
        (byte) 132,
        (byte) 78,
        (byte) 77,
        (byte) 215,
        (byte) 80 /*0x50*/,
        (byte) 137,
        (byte) 65,
        (byte) 250,
        (byte) 72,
        (byte) 54
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[9] = (byte) 91;
    numArray5[1] = (byte) 60;
    numArray5[15] = (byte) 245;
    numArray5[3] = (byte) 215;
    numArray5[4] = (byte) 152;
    numArray5[38] = (byte) 50;
    numArray5[40] = (byte) 208 /*0xD0*/;
    numArray5[5] = (byte) 143;
    numArray5[21] = (byte) 225;
    numArray5[29] = (byte) 108;
    numArray5[10] = (byte) 236;
    numArray5[34] = (byte) 92;
    numArray5[31 /*0x1F*/] = (byte) 60;
    numArray5[13] = (byte) 129;
    numArray5[33] = (byte) 92;
    numArray5[24] = (byte) 162;
    numArray5[22] = (byte) 187;
    numArray5[17] = (byte) 166;
    numArray5[14] = (byte) 161;
    numArray5[6] = (byte) 246;
    numArray5[20] = (byte) 206;
    numArray5[41] = (byte) 119;
    numArray5[19] = (byte) 37;
    numArray5[23] = (byte) 0;
    numArray5[16 /*0x10*/] = (byte) 115;
    numArray5[25] = (byte) 160 /*0xA0*/;
    numArray5[39] = (byte) 187;
    numArray5[27] = (byte) 243;
    numArray5[2] = (byte) 200;
    numArray5[8] = (byte) 139;
    numArray5[30] = (byte) 25;
    numArray5[11] = (byte) 102;
    numArray5[32 /*0x20*/] = (byte) 4;
    numArray5[42] = (byte) 9;
    numArray5[7] = (byte) 232;
    numArray5[12] = (byte) 71;
    numArray5[18] = (byte) 45;
    numArray5[28] = (byte) 188;
    numArray5[36] = (byte) 91;
    numArray5[35] = (byte) 142;
    numArray5[37] = (byte) 253;
    numArray5[26] = (byte) 149;
    numArray5[0] = (byte) 34;
    byte[] numArray6 = new byte[43]
    {
      (byte) 166,
      (byte) 224 /*0xE0*/,
      (byte) 37,
      (byte) 15,
      (byte) 57,
      (byte) 11,
      (byte) 11,
      (byte) 103,
      (byte) 48 /*0x30*/,
      (byte) 99,
      (byte) 221,
      (byte) 211,
      (byte) 52,
      (byte) 154,
      (byte) 211,
      (byte) 106,
      (byte) 215,
      (byte) 249,
      (byte) 175,
      (byte) 127 /*0x7F*/,
      (byte) 116,
      (byte) 119,
      (byte) 155,
      (byte) 123,
      (byte) 141,
      (byte) 206,
      (byte) 170,
      (byte) 66,
      (byte) 253,
      (byte) 43,
      (byte) 82,
      (byte) 4,
      (byte) 111,
      (byte) 126,
      (byte) 40,
      (byte) 22,
      (byte) 81,
      (byte) 212,
      (byte) 249,
      (byte) 240 /*0xF0*/,
      (byte) 91,
      (byte) 87,
      (byte) 233
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_2517.sspq, 0, (Array) numArray7, 0, 23);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2517.sspr, 0, (Array) numArray7, 0, 23);
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
}
