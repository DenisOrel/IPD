// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13831
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13831
{
  private static byte[] sspq = new byte[33]
  {
    (byte) 190,
    (byte) 56,
    (byte) 6,
    (byte) 189,
    (byte) 219,
    (byte) 132,
    (byte) 135,
    (byte) 40,
    (byte) 29,
    (byte) 154,
    (byte) 39,
    (byte) 31 /*0x1F*/,
    (byte) 167,
    (byte) 165,
    (byte) 54,
    (byte) 174,
    (byte) 14,
    (byte) 42,
    (byte) 39,
    (byte) 53,
    (byte) 63 /*0x3F*/,
    (byte) 21,
    (byte) 246,
    (byte) 73,
    (byte) 196,
    (byte) 23,
    (byte) 192 /*0xC0*/,
    (byte) 70,
    (byte) 209,
    (byte) 100,
    (byte) 221,
    (byte) 28,
    (byte) 144 /*0x90*/
  };
  private static byte[] sspr = new byte[33]
  {
    (byte) 90,
    (byte) 156,
    (byte) 233,
    (byte) 86,
    (byte) 94,
    (byte) 94,
    (byte) 167,
    (byte) 78,
    (byte) 77,
    (byte) 179,
    (byte) 83,
    (byte) 211,
    (byte) 134,
    (byte) 7,
    (byte) 116,
    (byte) 191,
    (byte) 105,
    (byte) 14,
    (byte) 20,
    (byte) 41,
    (byte) 28,
    (byte) 141,
    (byte) 22,
    (byte) 177,
    (byte) 144 /*0x90*/,
    (byte) 43,
    (byte) 86,
    (byte) 94,
    (byte) 140,
    (byte) 79,
    (byte) 240 /*0xF0*/,
    (byte) 210,
    (byte) 126
  };

  internal static string ssp_appserver_13832()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[136];
      byte[] numArray2 = new byte[55];
      numArray2[15] = (byte) 251;
      numArray2[18] = (byte) 64 /*0x40*/;
      numArray2[48 /*0x30*/] = (byte) 148;
      numArray2[3] = (byte) 99;
      numArray2[22] = (byte) 181;
      numArray2[23] = (byte) 74;
      numArray2[6] = (byte) 109;
      numArray2[7] = (byte) 101;
      numArray2[21] = (byte) 230;
      numArray2[44] = (byte) 4;
      numArray2[26] = (byte) 241;
      numArray2[2] = (byte) 137;
      numArray2[32 /*0x20*/] = (byte) 29;
      numArray2[13] = (byte) 105;
      numArray2[14] = (byte) 107;
      numArray2[4] = (byte) 205;
      numArray2[16 /*0x10*/] = (byte) 59;
      numArray2[17] = (byte) 250;
      numArray2[0] = (byte) 206;
      numArray2[19] = (byte) 13;
      numArray2[20] = (byte) 159;
      numArray2[25] = (byte) 195;
      numArray2[9] = (byte) 46;
      numArray2[12] = (byte) 63 /*0x3F*/;
      numArray2[24] = (byte) 73;
      numArray2[10] = (byte) 177;
      numArray2[50] = (byte) 191;
      numArray2[27] = (byte) 226;
      numArray2[43] = (byte) 217;
      numArray2[29] = (byte) 178;
      numArray2[8] = (byte) 129;
      numArray2[45] = (byte) 146;
      numArray2[5] = (byte) 109;
      numArray2[33] = (byte) 186;
      numArray2[1] = (byte) 76;
      numArray2[35] = (byte) 159;
      numArray2[36] = (byte) 40;
      numArray2[46] = (byte) 70;
      numArray2[38] = (byte) 87;
      numArray2[11] = (byte) 22;
      numArray2[40] = (byte) 108;
      numArray2[34] = (byte) 223;
      numArray2[28] = (byte) 97;
      numArray2[41] = (byte) 104;
      numArray2[51] = (byte) 60;
      numArray2[31 /*0x1F*/] = (byte) 23;
      numArray2[37] = (byte) 28;
      numArray2[47] = (byte) 54;
      numArray2[53] = (byte) 23;
      numArray2[49] = (byte) 150;
      numArray2[42] = (byte) 163;
      numArray2[30] = (byte) 224 /*0xE0*/;
      numArray2[52] = (byte) 122;
      numArray2[39] = (byte) 143;
      numArray2[54] = (byte) 183;
      byte[] numArray3 = new byte[55]
      {
        (byte) 99,
        (byte) 90,
        (byte) 88,
        (byte) 9,
        (byte) 239,
        (byte) 4,
        (byte) 102,
        (byte) 26,
        (byte) 224 /*0xE0*/,
        (byte) 104,
        (byte) 190,
        (byte) 107,
        (byte) 85,
        (byte) 27,
        (byte) 212,
        (byte) 99,
        (byte) 163,
        (byte) 133,
        (byte) 10,
        (byte) 158,
        (byte) 172,
        (byte) 130,
        (byte) 43,
        (byte) 232,
        (byte) 218,
        (byte) 130,
        (byte) 245,
        (byte) 213,
        (byte) 81,
        (byte) 36,
        (byte) 134,
        (byte) 252,
        (byte) 109,
        (byte) 235,
        (byte) 159,
        (byte) 59,
        (byte) 81,
        (byte) 153,
        (byte) 81,
        (byte) 206,
        (byte) 155,
        (byte) 40,
        (byte) 142,
        (byte) 177,
        (byte) 36,
        (byte) 115,
        (byte) 126,
        (byte) 253,
        (byte) 226,
        (byte) 128 /*0x80*/,
        (byte) 207,
        (byte) 64 /*0x40*/,
        (byte) 155,
        (byte) 18,
        (byte) 30
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 249,
        (byte) 98,
        (byte) 141,
        (byte) 191,
        (byte) 62,
        (byte) 57,
        (byte) 221,
        (byte) 169,
        (byte) 252,
        (byte) 243,
        (byte) 130,
        (byte) 145,
        (byte) 196,
        (byte) 232,
        (byte) 27,
        (byte) 85,
        (byte) 156,
        (byte) 193,
        (byte) 210,
        (byte) 111,
        (byte) 78,
        (byte) 218,
        (byte) 30,
        (byte) 188,
        (byte) 14,
        (byte) 132,
        (byte) 6,
        (byte) 82,
        (byte) 9,
        (byte) 210,
        (byte) 144 /*0x90*/,
        (byte) 131,
        (byte) 83,
        (byte) 75,
        (byte) 240 /*0xF0*/,
        (byte) 160 /*0xA0*/,
        (byte) 191,
        (byte) 84,
        (byte) 162,
        (byte) 43,
        (byte) 246,
        (byte) 226,
        (byte) 110,
        (byte) 165,
        (byte) 62,
        (byte) 27,
        (byte) 3,
        (byte) 218,
        (byte) 132,
        (byte) 68,
        (byte) 45,
        (byte) 180,
        (byte) 172,
        (byte) 52,
        (byte) 109
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 225,
        (byte) 16 /*0x10*/,
        (byte) 239,
        (byte) 204,
        (byte) 11,
        (byte) 63 /*0x3F*/,
        (byte) 177,
        (byte) 84,
        (byte) 211,
        (byte) 37,
        (byte) 147,
        (byte) 200,
        (byte) 117,
        (byte) 210,
        (byte) 20,
        (byte) 238,
        (byte) 183,
        (byte) 200,
        (byte) 22,
        (byte) 180,
        (byte) 222,
        (byte) 41,
        (byte) 220,
        (byte) 38,
        (byte) 199,
        (byte) 84,
        (byte) 14,
        (byte) 101,
        (byte) 141,
        (byte) 115,
        (byte) 118,
        (byte) 49,
        (byte) 243,
        (byte) 152,
        (byte) 11,
        (byte) 49,
        (byte) 52,
        (byte) 219,
        (byte) 237,
        (byte) 114,
        (byte) 210,
        (byte) 195,
        (byte) 63 /*0x3F*/,
        (byte) 223,
        (byte) 92,
        (byte) 173,
        (byte) 74,
        (byte) 81,
        (byte) 66,
        (byte) 161,
        (byte) 196,
        (byte) 108,
        (byte) 92,
        (byte) 146,
        (byte) 121
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[26]
      {
        (byte) 208 /*0xD0*/,
        (byte) 177,
        (byte) 185,
        (byte) 50,
        (byte) 139,
        (byte) 160 /*0xA0*/,
        (byte) 41,
        (byte) 98,
        byte.MaxValue,
        (byte) 61,
        (byte) 121,
        (byte) 248,
        (byte) 123,
        (byte) 150,
        (byte) 72,
        (byte) 31 /*0x1F*/,
        (byte) 167,
        (byte) 121,
        (byte) 108,
        (byte) 107,
        (byte) 120,
        (byte) 126,
        (byte) 243,
        (byte) 128 /*0x80*/,
        (byte) 43,
        (byte) 205
      };
      byte[] numArray7 = new byte[26];
      numArray7[11] = (byte) 190;
      numArray7[1] = (byte) 0;
      numArray7[2] = (byte) 48 /*0x30*/;
      numArray7[17] = (byte) 48 /*0x30*/;
      numArray7[6] = (byte) 212;
      numArray7[5] = (byte) 72;
      numArray7[18] = (byte) 170;
      numArray7[12] = (byte) 217;
      numArray7[21] = (byte) 83;
      numArray7[9] = (byte) 52;
      numArray7[10] = (byte) 138;
      numArray7[15] = (byte) 190;
      numArray7[24] = (byte) 136;
      numArray7[13] = (byte) 84;
      numArray7[14] = (byte) 120;
      numArray7[16 /*0x10*/] = (byte) 156;
      numArray7[0] = (byte) 22;
      numArray7[3] = (byte) 190;
      numArray7[4] = (byte) 240 /*0xF0*/;
      numArray7[19] = (byte) 151;
      numArray7[23] = (byte) 200;
      numArray7[25] = (byte) 80 /*0x50*/;
      numArray7[22] = (byte) 105;
      numArray7[8] = (byte) 178;
      numArray7[20] = (byte) 196;
      numArray7[7] = (byte) 66;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_13831.sspq, 0, (Array) numArray8, 0, 33);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13831.sspr, 0, (Array) numArray8, 0, 33);
      for (int index = 0; index < numArray8.Length; ++index)
      {
        if ((int) numArray8[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray9 = new byte[136];
    byte[] numArray10 = new byte[55]
    {
      (byte) 156,
      (byte) 229,
      (byte) 175,
      (byte) 243,
      (byte) 173,
      (byte) 102,
      (byte) 192 /*0xC0*/,
      (byte) 187,
      (byte) 22,
      (byte) 192 /*0xC0*/,
      (byte) 239,
      (byte) 186,
      (byte) 233,
      (byte) 6,
      (byte) 63 /*0x3F*/,
      (byte) 94,
      (byte) 87,
      (byte) 172,
      (byte) 214,
      (byte) 140,
      (byte) 150,
      (byte) 19,
      (byte) 169,
      (byte) 66,
      (byte) 188,
      (byte) 249,
      (byte) 196,
      (byte) 195,
      (byte) 64 /*0x40*/,
      (byte) 87,
      (byte) 184,
      (byte) 188,
      (byte) 10,
      (byte) 143,
      (byte) 11,
      (byte) 192 /*0xC0*/,
      (byte) 134,
      (byte) 187,
      (byte) 96 /*0x60*/,
      (byte) 196,
      (byte) 103,
      (byte) 121,
      (byte) 41,
      (byte) 84,
      (byte) 6,
      (byte) 143,
      (byte) 219,
      (byte) 17,
      (byte) 129,
      (byte) 231,
      (byte) 122,
      (byte) 184,
      (byte) 7,
      (byte) 144 /*0x90*/,
      (byte) 249
    };
    byte[] numArray11 = new byte[55];
    numArray11[50] = (byte) 219;
    numArray11[1] = (byte) 22;
    numArray11[25] = (byte) 144 /*0x90*/;
    numArray11[51] = (byte) 0;
    numArray11[21] = (byte) 45;
    numArray11[0] = (byte) 97;
    numArray11[45] = (byte) 58;
    numArray11[7] = (byte) 181;
    numArray11[52] = (byte) 158;
    numArray11[9] = (byte) 83;
    numArray11[4] = (byte) 6;
    numArray11[11] = (byte) 83;
    numArray11[12] = (byte) 223;
    numArray11[13] = (byte) 7;
    numArray11[35] = (byte) 170;
    numArray11[15] = (byte) 147;
    numArray11[28] = (byte) 41;
    numArray11[17] = (byte) 66;
    numArray11[18] = (byte) 28;
    numArray11[19] = (byte) 191;
    numArray11[36] = (byte) 226;
    numArray11[2] = (byte) 1;
    numArray11[22] = (byte) 130;
    numArray11[23] = (byte) 192 /*0xC0*/;
    numArray11[24] = (byte) 179;
    numArray11[29] = (byte) 27;
    numArray11[26] = (byte) 227;
    numArray11[34] = (byte) 160 /*0xA0*/;
    numArray11[6] = (byte) 61;
    numArray11[16 /*0x10*/] = (byte) 210;
    numArray11[44] = (byte) 71;
    numArray11[54] = (byte) 102;
    numArray11[46] = (byte) 156;
    numArray11[49] = (byte) 253;
    numArray11[5] = (byte) 245;
    numArray11[37] = (byte) 101;
    numArray11[14] = (byte) 99;
    numArray11[38] = (byte) 174;
    numArray11[47] = (byte) 215;
    numArray11[10] = (byte) 148;
    numArray11[40] = (byte) 32 /*0x20*/;
    numArray11[41] = (byte) 22;
    numArray11[42] = (byte) 140;
    numArray11[33] = (byte) 83;
    numArray11[39] = (byte) 115;
    numArray11[20] = (byte) 176 /*0xB0*/;
    numArray11[8] = (byte) 13;
    numArray11[27] = (byte) 22;
    numArray11[48 /*0x30*/] = (byte) 72;
    numArray11[3] = (byte) 196;
    numArray11[43] = (byte) 232;
    numArray11[31 /*0x1F*/] = (byte) 188;
    numArray11[32 /*0x20*/] = (byte) 89;
    numArray11[53] = (byte) 39;
    numArray11[30] = (byte) 242;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 49,
      (byte) 9,
      (byte) 150,
      (byte) 239,
      (byte) 187,
      (byte) 35,
      (byte) 225,
      (byte) 7,
      (byte) 126,
      (byte) 53,
      (byte) 195,
      (byte) 119,
      (byte) 250,
      (byte) 191,
      (byte) 23,
      (byte) 158,
      (byte) 239,
      (byte) 186,
      (byte) 30,
      (byte) 127 /*0x7F*/,
      (byte) 6,
      (byte) 104,
      (byte) 77,
      (byte) 156,
      (byte) 156,
      (byte) 62,
      (byte) 72,
      (byte) 254,
      (byte) 146,
      (byte) 190,
      (byte) 14,
      (byte) 184,
      (byte) 227,
      (byte) 156,
      (byte) 231,
      (byte) 123,
      (byte) 21,
      (byte) 51,
      (byte) 175,
      (byte) 218,
      (byte) 137,
      (byte) 70,
      (byte) 85,
      (byte) 170,
      (byte) 190,
      (byte) 159,
      (byte) 8,
      (byte) 80 /*0x50*/,
      (byte) 199,
      (byte) 207,
      (byte) 6,
      (byte) 177,
      (byte) 190,
      (byte) 31 /*0x1F*/,
      (byte) 225
    };
    byte[] numArray13 = new byte[55];
    numArray13[8] = (byte) 141;
    numArray13[40] = (byte) 232;
    numArray13[4] = (byte) 82;
    numArray13[10] = (byte) 168;
    numArray13[17] = (byte) 71;
    numArray13[3] = (byte) 28;
    numArray13[5] = (byte) 126;
    numArray13[7] = (byte) 26;
    numArray13[18] = (byte) 161;
    numArray13[49] = (byte) 179;
    numArray13[9] = (byte) 58;
    numArray13[44] = (byte) 253;
    numArray13[54] = (byte) 142;
    numArray13[13] = (byte) 229;
    numArray13[35] = (byte) 72;
    numArray13[15] = (byte) 41;
    numArray13[16 /*0x10*/] = (byte) 14;
    numArray13[1] = (byte) 155;
    numArray13[30] = (byte) 37;
    numArray13[19] = (byte) 63 /*0x3F*/;
    numArray13[11] = (byte) 113;
    numArray13[21] = (byte) 167;
    numArray13[6] = (byte) 10;
    numArray13[23] = (byte) 118;
    numArray13[24] = (byte) 10;
    numArray13[22] = (byte) 42;
    numArray13[26] = (byte) 67;
    numArray13[27] = (byte) 227;
    numArray13[33] = (byte) 26;
    numArray13[29] = (byte) 53;
    numArray13[36] = (byte) 89;
    numArray13[31 /*0x1F*/] = (byte) 84;
    numArray13[32 /*0x20*/] = (byte) 16 /*0x10*/;
    numArray13[25] = (byte) 200;
    numArray13[34] = (byte) 171;
    numArray13[0] = (byte) 253;
    numArray13[28] = (byte) 119;
    numArray13[37] = (byte) 130;
    numArray13[38] = (byte) 31 /*0x1F*/;
    numArray13[14] = (byte) 227;
    numArray13[2] = (byte) 244;
    numArray13[41] = (byte) 234;
    numArray13[48 /*0x30*/] = (byte) 181;
    numArray13[42] = (byte) 123;
    numArray13[53] = (byte) 219;
    numArray13[45] = (byte) 15;
    numArray13[46] = (byte) 78;
    numArray13[47] = (byte) 163;
    numArray13[39] = (byte) 201;
    numArray13[12] = (byte) 230;
    numArray13[43] = (byte) 152;
    numArray13[51] = (byte) 160 /*0xA0*/;
    numArray13[52] = (byte) 145;
    numArray13[50] = (byte) 52;
    numArray13[20] = (byte) 25;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[26]
    {
      (byte) 28,
      (byte) 85,
      (byte) 69,
      (byte) 226,
      (byte) 170,
      (byte) 178,
      (byte) 6,
      (byte) 201,
      (byte) 17,
      (byte) 208 /*0xD0*/,
      (byte) 163,
      (byte) 186,
      (byte) 219,
      (byte) 109,
      (byte) 219,
      (byte) 216,
      (byte) 67,
      (byte) 79,
      (byte) 238,
      (byte) 125,
      (byte) 32 /*0x20*/,
      (byte) 42,
      (byte) 242,
      (byte) 140,
      (byte) 127 /*0x7F*/,
      (byte) 12
    };
    byte[] numArray15 = new byte[26]
    {
      (byte) 156,
      (byte) 153,
      (byte) 167,
      (byte) 39,
      (byte) 92,
      (byte) 228,
      (byte) 48 /*0x30*/,
      (byte) 151,
      (byte) 91,
      (byte) 201,
      (byte) 152,
      (byte) 128 /*0x80*/,
      (byte) 66,
      (byte) 56,
      (byte) 215,
      (byte) 147,
      (byte) 79,
      (byte) 90,
      (byte) 86,
      (byte) 102,
      (byte) 110,
      (byte) 217,
      (byte) 114,
      (byte) 117,
      (byte) 132,
      (byte) 123
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 26);
    for (int index = 0; index < 26; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }
}
