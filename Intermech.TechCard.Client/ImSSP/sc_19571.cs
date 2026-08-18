// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19571
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19571
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 72,
    (byte) 151,
    (byte) 94,
    (byte) 5,
    (byte) 75,
    (byte) 220,
    (byte) 26,
    (byte) 15,
    (byte) 51,
    (byte) 5,
    (byte) 223,
    (byte) 93,
    (byte) 183,
    (byte) 160 /*0xA0*/,
    (byte) 127 /*0x7F*/,
    (byte) 83,
    (byte) 47,
    (byte) 24,
    (byte) 223,
    (byte) 196,
    (byte) 185,
    (byte) 137,
    (byte) 86,
    (byte) 40,
    (byte) 205,
    (byte) 81,
    (byte) 123,
    (byte) 126,
    (byte) 79,
    (byte) 13,
    (byte) 23,
    (byte) 52,
    (byte) 139,
    (byte) 67,
    (byte) 31 /*0x1F*/
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 76,
    (byte) 20,
    (byte) 82,
    (byte) 52,
    (byte) 108,
    (byte) 6,
    (byte) 203,
    (byte) 156,
    (byte) 239,
    (byte) 206,
    (byte) 239,
    (byte) 196,
    (byte) 179,
    (byte) 213,
    (byte) 42,
    (byte) 76,
    (byte) 189,
    (byte) 147,
    (byte) 56,
    (byte) 91,
    (byte) 244,
    (byte) 118,
    (byte) 48 /*0x30*/,
    (byte) 71,
    (byte) 47,
    (byte) 208 /*0xD0*/,
    (byte) 134,
    (byte) 100,
    (byte) 153,
    (byte) 113,
    (byte) 84,
    (byte) 249,
    (byte) 101,
    (byte) 112 /*0x70*/,
    (byte) 168
  };

  internal static string ssp_techcard_19572()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/]
      {
        (byte) 153,
        (byte) 77,
        (byte) 157,
        (byte) 170,
        (byte) 48 /*0x30*/,
        (byte) 141,
        (byte) 34,
        (byte) 126,
        (byte) 186,
        (byte) 121,
        (byte) 166,
        (byte) 37,
        (byte) 100,
        (byte) 169,
        (byte) 9,
        (byte) 147,
        (byte) 212,
        (byte) 151,
        (byte) 7,
        (byte) 163,
        (byte) 14,
        (byte) 22,
        (byte) 180,
        (byte) 35,
        (byte) 118,
        (byte) 229,
        (byte) 231,
        (byte) 50,
        (byte) 177,
        (byte) 120,
        (byte) 215,
        (byte) 173,
        (byte) 116,
        (byte) 56,
        (byte) 7,
        (byte) 63 /*0x3F*/,
        (byte) 153,
        (byte) 89,
        (byte) 103,
        (byte) 32 /*0x20*/,
        (byte) 83,
        (byte) 58,
        (byte) 121,
        (byte) 236,
        (byte) 252,
        (byte) 43,
        (byte) 217,
        (byte) 218
      };
      byte[] numArray3 = new byte[48 /*0x30*/]
      {
        (byte) 9,
        (byte) 156,
        (byte) 131,
        (byte) 174,
        (byte) 186,
        (byte) 186,
        (byte) 191,
        (byte) 29,
        (byte) 110,
        (byte) 37,
        (byte) 58,
        (byte) 80 /*0x50*/,
        (byte) 94,
        (byte) 129,
        (byte) 233,
        (byte) 244,
        (byte) 128 /*0x80*/,
        (byte) 15,
        (byte) 131,
        (byte) 227,
        (byte) 242,
        (byte) 34,
        (byte) 198,
        (byte) 73,
        (byte) 215,
        (byte) 97,
        (byte) 137,
        (byte) 82,
        (byte) 86,
        (byte) 185,
        (byte) 6,
        (byte) 164,
        (byte) 114,
        (byte) 19,
        (byte) 7,
        (byte) 97,
        (byte) 191,
        (byte) 110,
        (byte) 35,
        (byte) 83,
        (byte) 202,
        (byte) 104,
        (byte) 116,
        (byte) 250,
        (byte) 248,
        (byte) 158,
        (byte) 142,
        (byte) 12
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[48 /*0x30*/];
    byte[] numArray5 = new byte[48 /*0x30*/];
    numArray5[34] = (byte) 181;
    numArray5[1] = (byte) 178;
    numArray5[2] = (byte) 242;
    numArray5[15] = (byte) 176 /*0xB0*/;
    numArray5[4] = (byte) 130;
    numArray5[39] = (byte) 166;
    numArray5[6] = (byte) 121;
    numArray5[7] = (byte) 51;
    numArray5[32 /*0x20*/] = (byte) 168;
    numArray5[16 /*0x10*/] = (byte) 44;
    numArray5[41] = (byte) 37;
    numArray5[5] = (byte) 153;
    numArray5[12] = (byte) 66;
    numArray5[31 /*0x1F*/] = (byte) 53;
    numArray5[14] = (byte) 249;
    numArray5[23] = (byte) 79;
    numArray5[37] = (byte) 92;
    numArray5[17] = (byte) 162;
    numArray5[13] = (byte) 161;
    numArray5[19] = (byte) 250;
    numArray5[8] = (byte) 74;
    numArray5[21] = (byte) 47;
    numArray5[22] = (byte) 231;
    numArray5[9] = (byte) 165;
    numArray5[24] = (byte) 212;
    numArray5[3] = (byte) 219;
    numArray5[26] = (byte) 199;
    numArray5[27] = (byte) 38;
    numArray5[20] = (byte) 122;
    numArray5[29] = (byte) 12;
    numArray5[30] = (byte) 12;
    numArray5[28] = (byte) 75;
    numArray5[11] = (byte) 205;
    numArray5[33] = (byte) 163;
    numArray5[43] = (byte) 122;
    numArray5[35] = (byte) 251;
    numArray5[42] = (byte) 28;
    numArray5[10] = (byte) 240 /*0xF0*/;
    numArray5[46] = (byte) 54;
    numArray5[45] = (byte) 108;
    numArray5[40] = (byte) 31 /*0x1F*/;
    numArray5[47] = (byte) 74;
    numArray5[38] = (byte) 132;
    numArray5[0] = (byte) 33;
    numArray5[36] = (byte) 129;
    numArray5[25] = (byte) 157;
    numArray5[44] = (byte) 99;
    numArray5[18] = (byte) 202;
    byte[] numArray6 = new byte[48 /*0x30*/]
    {
      (byte) 159,
      (byte) 253,
      (byte) 102,
      (byte) 93,
      (byte) 195,
      (byte) 52,
      (byte) 33,
      (byte) 72,
      (byte) 20,
      (byte) 218,
      (byte) 14,
      (byte) 70,
      (byte) 141,
      (byte) 189,
      (byte) 118,
      (byte) 193,
      (byte) 24,
      (byte) 195,
      (byte) 30,
      (byte) 64 /*0x40*/,
      (byte) 184,
      (byte) 158,
      (byte) 65,
      (byte) 141,
      (byte) 7,
      (byte) 128 /*0x80*/,
      (byte) 227,
      (byte) 55,
      (byte) 130,
      (byte) 45,
      (byte) 202,
      (byte) 145,
      (byte) 130,
      (byte) 253,
      (byte) 35,
      (byte) 69,
      (byte) 253,
      (byte) 118,
      (byte) 131,
      (byte) 173,
      (byte) 148,
      (byte) 52,
      (byte) 50,
      (byte) 194,
      (byte) 232,
      (byte) 20,
      (byte) 40,
      (byte) 167
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_19571.sspq, 0, (Array) numArray7, 0, 35);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19571.sspr, 0, (Array) numArray7, 0, 35);
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

  internal static string ssp_techcard_19573()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 30,
        (byte) 29,
        (byte) 132,
        (byte) 130,
        (byte) 185,
        (byte) 242,
        (byte) 103,
        (byte) 66,
        (byte) 228,
        (byte) 210,
        (byte) 38,
        (byte) 180,
        (byte) 31 /*0x1F*/,
        (byte) 128 /*0x80*/,
        (byte) 157,
        (byte) 36,
        (byte) 254,
        (byte) 29,
        (byte) 239
      };
      byte[] numArray3 = new byte[19];
      numArray3[4] = (byte) 128 /*0x80*/;
      numArray3[1] = (byte) 229;
      numArray3[13] = (byte) 51;
      numArray3[3] = (byte) 96 /*0x60*/;
      numArray3[14] = (byte) 93;
      numArray3[8] = (byte) 43;
      numArray3[6] = (byte) 62;
      numArray3[15] = (byte) 127 /*0x7F*/;
      numArray3[0] = (byte) 78;
      numArray3[7] = (byte) 195;
      numArray3[10] = (byte) 127 /*0x7F*/;
      numArray3[9] = (byte) 186;
      numArray3[12] = (byte) 183;
      numArray3[2] = byte.MaxValue;
      numArray3[5] = (byte) 15;
      numArray3[17] = (byte) 48 /*0x30*/;
      numArray3[16 /*0x10*/] = (byte) 234;
      numArray3[11] = (byte) 27;
      numArray3[18] = (byte) 66;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[9] = (byte) 53;
    numArray5[1] = (byte) 67;
    numArray5[0] = (byte) 19;
    numArray5[16 /*0x10*/] = (byte) 138;
    numArray5[3] = (byte) 42;
    numArray5[5] = (byte) 95;
    numArray5[17] = (byte) 33;
    numArray5[6] = (byte) 206;
    numArray5[8] = (byte) 243;
    numArray5[15] = (byte) 228;
    numArray5[10] = (byte) 109;
    numArray5[12] = (byte) 96 /*0x60*/;
    numArray5[4] = (byte) 198;
    numArray5[13] = (byte) 141;
    numArray5[14] = (byte) 232;
    numArray5[2] = (byte) 250;
    numArray5[7] = (byte) 112 /*0x70*/;
    numArray5[11] = (byte) 83;
    numArray5[18] = (byte) 29;
    byte[] numArray6 = new byte[19];
    numArray6[13] = (byte) 88;
    numArray6[1] = (byte) 83;
    numArray6[2] = (byte) 214;
    numArray6[3] = (byte) 135;
    numArray6[4] = (byte) 244;
    numArray6[0] = (byte) 133;
    numArray6[16 /*0x10*/] = (byte) 161;
    numArray6[7] = (byte) 144 /*0x90*/;
    numArray6[8] = (byte) 93;
    numArray6[9] = (byte) 117;
    numArray6[6] = (byte) 143;
    numArray6[10] = (byte) 188;
    numArray6[18] = (byte) 240 /*0xF0*/;
    numArray6[5] = (byte) 223;
    numArray6[12] = (byte) 244;
    numArray6[11] = (byte) 247;
    numArray6[15] = (byte) 3;
    numArray6[17] = (byte) 102;
    numArray6[14] = (byte) 186;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19574()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[39];
      byte[] numArray2 = new byte[39];
      numArray2[29] = (byte) 232;
      numArray2[1] = (byte) 229;
      numArray2[2] = (byte) 99;
      numArray2[19] = (byte) 19;
      numArray2[4] = (byte) 90;
      numArray2[5] = (byte) 43;
      numArray2[18] = (byte) 145;
      numArray2[23] = (byte) 133;
      numArray2[8] = (byte) 245;
      numArray2[6] = (byte) 197;
      numArray2[10] = (byte) 186;
      numArray2[30] = (byte) 67;
      numArray2[12] = (byte) 224 /*0xE0*/;
      numArray2[31 /*0x1F*/] = (byte) 87;
      numArray2[35] = (byte) 207;
      numArray2[28] = (byte) 110;
      numArray2[14] = (byte) 139;
      numArray2[15] = (byte) 91;
      numArray2[16 /*0x10*/] = (byte) 196;
      numArray2[21] = (byte) 254;
      numArray2[20] = (byte) 75;
      numArray2[25] = (byte) 38;
      numArray2[24] = (byte) 103;
      numArray2[11] = (byte) 135;
      numArray2[0] = (byte) 131;
      numArray2[37] = (byte) 226;
      numArray2[26] = (byte) 148;
      numArray2[27] = (byte) 186;
      numArray2[3] = (byte) 147;
      numArray2[32 /*0x20*/] = (byte) 6;
      numArray2[7] = (byte) 96 /*0x60*/;
      numArray2[13] = (byte) 66;
      numArray2[17] = (byte) 155;
      numArray2[33] = (byte) 76;
      numArray2[34] = (byte) 109;
      numArray2[9] = (byte) 166;
      numArray2[36] = (byte) 149;
      numArray2[22] = (byte) 92;
      numArray2[38] = (byte) 10;
      byte[] numArray3 = new byte[39]
      {
        (byte) 214,
        (byte) 78,
        (byte) 249,
        (byte) 130,
        (byte) 213,
        (byte) 121,
        (byte) 90,
        (byte) 47,
        (byte) 175,
        (byte) 117,
        (byte) 139,
        (byte) 82,
        (byte) 79,
        (byte) 150,
        (byte) 164,
        (byte) 19,
        (byte) 81,
        (byte) 101,
        (byte) 9,
        (byte) 64 /*0x40*/,
        (byte) 171,
        (byte) 61,
        (byte) 209,
        (byte) 187,
        (byte) 47,
        (byte) 210,
        (byte) 22,
        (byte) 137,
        (byte) 179,
        (byte) 55,
        (byte) 65,
        (byte) 196,
        (byte) 2,
        (byte) 95,
        (byte) 32 /*0x20*/,
        (byte) 76,
        (byte) 240 /*0xF0*/,
        (byte) 69,
        (byte) 141
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[39];
    byte[] numArray5 = new byte[39];
    numArray5[36] = (byte) 231;
    numArray5[1] = (byte) 99;
    numArray5[6] = (byte) 135;
    numArray5[14] = (byte) 79;
    numArray5[21] = (byte) 125;
    numArray5[27] = (byte) 198;
    numArray5[4] = (byte) 228;
    numArray5[7] = (byte) 167;
    numArray5[8] = (byte) 232;
    numArray5[15] = (byte) 207;
    numArray5[10] = (byte) 183;
    numArray5[11] = (byte) 181;
    numArray5[0] = (byte) 67;
    numArray5[5] = (byte) 111;
    numArray5[37] = (byte) 104;
    numArray5[9] = (byte) 70;
    numArray5[20] = (byte) 119;
    numArray5[17] = (byte) 35;
    numArray5[18] = (byte) 236;
    numArray5[19] = (byte) 50;
    numArray5[31 /*0x1F*/] = (byte) 168;
    numArray5[32 /*0x20*/] = (byte) 120;
    numArray5[22] = (byte) 154;
    numArray5[3] = (byte) 163;
    numArray5[38] = (byte) 126;
    numArray5[25] = (byte) 204;
    numArray5[26] = (byte) 76;
    numArray5[24] = (byte) 99;
    numArray5[28] = (byte) 27;
    numArray5[29] = (byte) 222;
    numArray5[30] = (byte) 180;
    numArray5[16 /*0x10*/] = (byte) 75;
    numArray5[2] = (byte) 11;
    numArray5[33] = (byte) 25;
    numArray5[34] = (byte) 82;
    numArray5[35] = (byte) 167;
    numArray5[12] = (byte) 214;
    numArray5[23] = (byte) 157;
    numArray5[13] = (byte) 95;
    byte[] numArray6 = new byte[39]
    {
      (byte) 80 /*0x50*/,
      (byte) 238,
      (byte) 188,
      (byte) 68,
      (byte) 86,
      (byte) 31 /*0x1F*/,
      (byte) 190,
      (byte) 34,
      (byte) 8,
      (byte) 199,
      (byte) 238,
      (byte) 97,
      (byte) 210,
      (byte) 177,
      (byte) 40,
      (byte) 71,
      (byte) 172,
      (byte) 105,
      (byte) 139,
      (byte) 49,
      (byte) 129,
      (byte) 188,
      (byte) 94,
      (byte) 180,
      (byte) 153,
      (byte) 99,
      (byte) 217,
      (byte) 90,
      (byte) 176 /*0xB0*/,
      (byte) 150,
      (byte) 14,
      (byte) 36,
      (byte) 218,
      (byte) 113,
      (byte) 68,
      (byte) 47,
      (byte) 20,
      (byte) 33,
      (byte) 236
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 39);
    for (int index = 0; index < 39; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
