// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12563
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12563
{
  private static byte[] sspq = new byte[49]
  {
    (byte) 166,
    (byte) 166,
    (byte) 253,
    (byte) 186,
    (byte) 98,
    (byte) 35,
    (byte) 108,
    (byte) 84,
    (byte) 25,
    (byte) 10,
    (byte) 80 /*0x50*/,
    (byte) 159,
    (byte) 20,
    (byte) 189,
    (byte) 204,
    (byte) 27,
    (byte) 232,
    (byte) 161,
    (byte) 200,
    (byte) 5,
    (byte) 207,
    (byte) 129,
    (byte) 32 /*0x20*/,
    (byte) 125,
    (byte) 38,
    (byte) 114,
    (byte) 159,
    (byte) 25,
    (byte) 146,
    (byte) 82,
    (byte) 113,
    (byte) 111,
    (byte) 190,
    (byte) 145,
    (byte) 104,
    (byte) 87,
    (byte) 176 /*0xB0*/,
    (byte) 199,
    (byte) 96 /*0x60*/,
    (byte) 14,
    (byte) 164,
    (byte) 170,
    (byte) 227,
    (byte) 37,
    (byte) 237,
    (byte) 77,
    (byte) 214,
    (byte) 87,
    (byte) 150
  };
  private static byte[] sspr = new byte[49]
  {
    (byte) 54,
    (byte) 160 /*0xA0*/,
    (byte) 26,
    (byte) 172,
    (byte) 3,
    (byte) 111,
    (byte) 161,
    (byte) 179,
    (byte) 133,
    (byte) 67,
    (byte) 234,
    (byte) 125,
    (byte) 244,
    (byte) 215,
    (byte) 142,
    (byte) 21,
    (byte) 181,
    (byte) 10,
    (byte) 238,
    (byte) 98,
    (byte) 155,
    (byte) 85,
    (byte) 24,
    (byte) 214,
    (byte) 106,
    (byte) 152,
    (byte) 174,
    (byte) 215,
    (byte) 116,
    (byte) 97,
    (byte) 22,
    (byte) 188,
    (byte) 241,
    (byte) 40,
    (byte) 146,
    (byte) 32 /*0x20*/,
    (byte) 144 /*0x90*/,
    (byte) 246,
    (byte) 113,
    (byte) 163,
    (byte) 161,
    (byte) 129,
    (byte) 180,
    (byte) 116,
    (byte) 170,
    (byte) 239,
    (byte) 103,
    (byte) 4,
    (byte) 59
  };

  internal static int ssp_appserver_12564(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 196,
      (byte) 151,
      (byte) 188,
      (byte) 218,
      (byte) 1,
      (byte) 125,
      (byte) 4,
      (byte) 179,
      (byte) 213,
      (byte) 12,
      (byte) 132,
      (byte) 14,
      (byte) 131,
      (byte) 48 /*0x30*/,
      (byte) 210,
      (byte) 21,
      (byte) 37,
      (byte) 66,
      (byte) 157,
      (byte) 198,
      (byte) 77,
      (byte) 84,
      (byte) 251,
      (byte) 63 /*0x3F*/,
      (byte) 139,
      (byte) 92,
      (byte) 53,
      (byte) 212,
      (byte) 214,
      (byte) 180,
      (byte) 253,
      (byte) 147,
      (byte) 122,
      (byte) 42,
      (byte) 203,
      (byte) 87,
      (byte) 195,
      (byte) 152,
      (byte) 251,
      (byte) 91,
      (byte) 28,
      (byte) 75,
      (byte) 94,
      (byte) 115,
      (byte) 125,
      (byte) 254,
      (byte) 73,
      (byte) 138
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 128 /*0x80*/;
    sourceArray2[33] = (byte) 205;
    sourceArray2[9] = (byte) 213;
    sourceArray2[3] = (byte) 196;
    sourceArray2[39] = (byte) 74;
    sourceArray2[5] = (byte) 137;
    sourceArray2[24] = (byte) 144 /*0x90*/;
    sourceArray2[28] = (byte) 159;
    sourceArray2[8] = (byte) 114;
    sourceArray2[43] = (byte) 49;
    sourceArray2[10] = (byte) 155;
    sourceArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    sourceArray2[12] = (byte) 84;
    sourceArray2[45] = (byte) 8;
    sourceArray2[14] = (byte) 82;
    sourceArray2[15] = (byte) 61;
    sourceArray2[35] = (byte) 212;
    sourceArray2[42] = (byte) 65;
    sourceArray2[25] = (byte) 75;
    sourceArray2[21] = (byte) 32 /*0x20*/;
    sourceArray2[20] = (byte) 157;
    sourceArray2[27] = (byte) 148;
    sourceArray2[17] = (byte) 16 /*0x10*/;
    sourceArray2[23] = (byte) 68;
    sourceArray2[44] = (byte) 209;
    sourceArray2[38] = (byte) 213;
    sourceArray2[26] = (byte) 62;
    sourceArray2[0] = (byte) 139;
    sourceArray2[1] = (byte) 135;
    sourceArray2[19] = (byte) 34;
    sourceArray2[30] = (byte) 179;
    sourceArray2[29] = (byte) 60;
    sourceArray2[36] = (byte) 199;
    sourceArray2[16 /*0x10*/] = (byte) 185;
    sourceArray2[34] = (byte) 118;
    sourceArray2[37] = (byte) 239;
    sourceArray2[11] = (byte) 168;
    sourceArray2[40] = (byte) 76;
    sourceArray2[2] = (byte) 177;
    sourceArray2[46] = (byte) 119;
    sourceArray2[4] = (byte) 149;
    sourceArray2[41] = (byte) 83;
    sourceArray2[22] = (byte) 47;
    sourceArray2[47] = (byte) 158;
    sourceArray2[6] = (byte) 189;
    sourceArray2[18] = (byte) 44;
    sourceArray2[13] = (byte) 70;
    sourceArray2[32 /*0x20*/] = (byte) 113;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12565(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 240 /*0xF0*/,
      (byte) 36,
      (byte) 176 /*0xB0*/,
      (byte) 145,
      (byte) 61,
      (byte) 114,
      (byte) 179,
      (byte) 93,
      (byte) 20,
      (byte) 122,
      (byte) 20,
      (byte) 186,
      (byte) 194,
      (byte) 146,
      (byte) 156,
      (byte) 134,
      (byte) 235,
      (byte) 54,
      (byte) 203,
      (byte) 84,
      (byte) 31 /*0x1F*/,
      (byte) 69,
      (byte) 10,
      (byte) 190,
      (byte) 12,
      (byte) 68,
      (byte) 240 /*0xF0*/,
      (byte) 46,
      (byte) 188,
      (byte) 147,
      (byte) 184,
      (byte) 36,
      (byte) 55,
      (byte) 33,
      (byte) 34,
      (byte) 134,
      (byte) 249,
      (byte) 96 /*0x60*/,
      (byte) 172,
      (byte) 209,
      (byte) 233,
      (byte) 22,
      byte.MaxValue,
      (byte) 225,
      (byte) 167,
      (byte) 140,
      (byte) 133,
      (byte) 196
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 116,
      (byte) 5,
      (byte) 220,
      (byte) 129,
      (byte) 245,
      (byte) 172,
      (byte) 11,
      (byte) 159,
      (byte) 182,
      (byte) 19,
      (byte) 161,
      (byte) 131,
      (byte) 85,
      (byte) 13,
      (byte) 27,
      (byte) 69,
      (byte) 184,
      (byte) 114,
      (byte) 118,
      (byte) 109,
      (byte) 178,
      (byte) 41,
      byte.MaxValue,
      (byte) 229,
      (byte) 129,
      (byte) 93,
      (byte) 184,
      (byte) 59,
      (byte) 200,
      (byte) 5,
      (byte) 58,
      (byte) 50,
      (byte) 198,
      (byte) 244,
      (byte) 118,
      (byte) 225,
      (byte) 68,
      (byte) 167,
      (byte) 192 /*0xC0*/,
      (byte) 37,
      (byte) 32 /*0x20*/,
      (byte) 117,
      (byte) 148,
      (byte) 81,
      (byte) 110,
      (byte) 179,
      (byte) 153,
      (byte) 230
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12566()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 151,
        (byte) 55,
        (byte) 76,
        (byte) 170,
        (byte) 25,
        (byte) 241,
        (byte) 133,
        (byte) 249,
        (byte) 130,
        (byte) 133
      };
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 86;
      numArray3[2] = (byte) 168;
      numArray3[0] = (byte) 95;
      numArray3[3] = (byte) 107;
      numArray3[4] = (byte) 170;
      numArray3[6] = (byte) 52;
      numArray3[5] = (byte) 196;
      numArray3[8] = (byte) 84;
      numArray3[1] = (byte) 80 /*0x50*/;
      numArray3[9] = (byte) 162;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 86;
    numArray5[1] = (byte) 70;
    numArray5[2] = (byte) 41;
    numArray5[3] = (byte) 23;
    numArray5[4] = (byte) 128 /*0x80*/;
    numArray5[9] = (byte) 17;
    numArray5[6] = (byte) 43;
    numArray5[5] = (byte) 190;
    numArray5[0] = (byte) 8;
    numArray5[7] = (byte) 182;
    byte[] numArray6 = new byte[10]
    {
      (byte) 111,
      (byte) 98,
      (byte) 70,
      (byte) 103,
      (byte) 192 /*0xC0*/,
      (byte) 210,
      (byte) 160 /*0xA0*/,
      (byte) 110,
      (byte) 46,
      (byte) 206
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12567(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 9,
      (byte) 228,
      (byte) 84,
      (byte) 97,
      (byte) 217,
      (byte) 247,
      (byte) 123,
      (byte) 157,
      (byte) 205,
      (byte) 67,
      (byte) 100,
      (byte) 239,
      (byte) 218,
      (byte) 4,
      (byte) 58,
      (byte) 86,
      (byte) 206,
      (byte) 74,
      (byte) 111,
      (byte) 14,
      (byte) 100,
      (byte) 131,
      (byte) 204,
      (byte) 248,
      (byte) 168,
      (byte) 132,
      (byte) 174,
      (byte) 215,
      (byte) 171,
      (byte) 6,
      (byte) 147,
      (byte) 200,
      (byte) 9,
      (byte) 96 /*0x60*/,
      (byte) 118,
      (byte) 218,
      (byte) 215,
      (byte) 85,
      (byte) 125,
      (byte) 25,
      (byte) 55,
      (byte) 238,
      (byte) 50,
      (byte) 145,
      (byte) 19,
      (byte) 125,
      (byte) 45,
      (byte) 214
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 65,
      (byte) 4,
      (byte) 191,
      (byte) 161,
      (byte) 214,
      (byte) 169,
      (byte) 2,
      (byte) 233,
      (byte) 182,
      (byte) 101,
      (byte) 79,
      (byte) 128 /*0x80*/,
      (byte) 241,
      (byte) 79,
      (byte) 31 /*0x1F*/,
      (byte) 27,
      (byte) 86,
      (byte) 87,
      (byte) 173,
      (byte) 26,
      (byte) 198,
      (byte) 12,
      (byte) 202,
      (byte) 109,
      (byte) 219,
      (byte) 202,
      (byte) 62,
      (byte) 252,
      (byte) 131,
      (byte) 25,
      (byte) 7,
      (byte) 144 /*0x90*/,
      (byte) 127 /*0x7F*/,
      (byte) 26,
      (byte) 190,
      (byte) 223,
      (byte) 66,
      (byte) 105,
      (byte) 53,
      (byte) 247,
      (byte) 84,
      (byte) 224 /*0xE0*/,
      (byte) 26,
      (byte) 9,
      (byte) 178,
      (byte) 189,
      (byte) 234,
      (byte) 70
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12568(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 86;
    sourceArray1[28] = (byte) 216;
    sourceArray1[4] = (byte) 92;
    sourceArray1[15] = (byte) 141;
    sourceArray1[10] = (byte) 198;
    sourceArray1[5] = (byte) 184;
    sourceArray1[6] = (byte) 84;
    sourceArray1[7] = (byte) 72;
    sourceArray1[32 /*0x20*/] = (byte) 104;
    sourceArray1[25] = (byte) 228;
    sourceArray1[38] = (byte) 123;
    sourceArray1[40] = (byte) 40;
    sourceArray1[12] = (byte) 155;
    sourceArray1[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray1[2] = (byte) 134;
    sourceArray1[47] = (byte) 151;
    sourceArray1[16 /*0x10*/] = (byte) 96 /*0x60*/;
    sourceArray1[43] = (byte) 253;
    sourceArray1[18] = (byte) 120;
    sourceArray1[45] = (byte) 225;
    sourceArray1[20] = (byte) 85;
    sourceArray1[26] = (byte) 43;
    sourceArray1[22] = (byte) 117;
    sourceArray1[23] = (byte) 80 /*0x50*/;
    sourceArray1[46] = (byte) 119;
    sourceArray1[33] = (byte) 185;
    sourceArray1[0] = (byte) 17;
    sourceArray1[19] = (byte) 205;
    sourceArray1[37] = (byte) 195;
    sourceArray1[29] = (byte) 95;
    sourceArray1[30] = (byte) 179;
    sourceArray1[8] = (byte) 164;
    sourceArray1[36] = (byte) 240 /*0xF0*/;
    sourceArray1[21] = (byte) 215;
    sourceArray1[9] = (byte) 241;
    sourceArray1[35] = (byte) 12;
    sourceArray1[3] = (byte) 23;
    sourceArray1[24] = (byte) 59;
    sourceArray1[11] = (byte) 176 /*0xB0*/;
    sourceArray1[39] = (byte) 157;
    sourceArray1[34] = (byte) 248;
    sourceArray1[41] = (byte) 208 /*0xD0*/;
    sourceArray1[42] = (byte) 146;
    sourceArray1[13] = (byte) 229;
    sourceArray1[44] = (byte) 107;
    sourceArray1[1] = (byte) 83;
    sourceArray1[17] = (byte) 127 /*0x7F*/;
    sourceArray1[14] = (byte) 122;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 254;
    sourceArray2[1] = (byte) 236;
    sourceArray2[47] = (byte) 94;
    sourceArray2[33] = (byte) 212;
    sourceArray2[4] = (byte) 79;
    sourceArray2[45] = (byte) 217;
    sourceArray2[6] = (byte) 193;
    sourceArray2[7] = (byte) 229;
    sourceArray2[8] = (byte) 243;
    sourceArray2[46] = (byte) 153;
    sourceArray2[0] = (byte) 203;
    sourceArray2[11] = (byte) 8;
    sourceArray2[12] = (byte) 198;
    sourceArray2[13] = (byte) 243;
    sourceArray2[9] = (byte) 96 /*0x60*/;
    sourceArray2[15] = (byte) 9;
    sourceArray2[26] = (byte) 104;
    sourceArray2[17] = (byte) 25;
    sourceArray2[18] = (byte) 29;
    sourceArray2[5] = (byte) 140;
    sourceArray2[34] = (byte) 155;
    sourceArray2[28] = (byte) 96 /*0x60*/;
    sourceArray2[22] = (byte) 132;
    sourceArray2[23] = (byte) 129;
    sourceArray2[24] = (byte) 194;
    sourceArray2[14] = (byte) 188;
    sourceArray2[43] = (byte) 120;
    sourceArray2[38] = (byte) 6;
    sourceArray2[42] = (byte) 198;
    sourceArray2[19] = (byte) 102;
    sourceArray2[44] = byte.MaxValue;
    sourceArray2[31 /*0x1F*/] = (byte) 172;
    sourceArray2[21] = (byte) 50;
    sourceArray2[16 /*0x10*/] = (byte) 253;
    sourceArray2[3] = (byte) 180;
    sourceArray2[35] = (byte) 30;
    sourceArray2[2] = (byte) 79;
    sourceArray2[37] = (byte) 27;
    sourceArray2[30] = (byte) 88;
    sourceArray2[39] = (byte) 98;
    sourceArray2[25] = (byte) 59;
    sourceArray2[41] = (byte) 166;
    sourceArray2[27] = (byte) 225;
    sourceArray2[40] = (byte) 223;
    sourceArray2[32 /*0x20*/] = (byte) 44;
    sourceArray2[10] = (byte) 31 /*0x1F*/;
    sourceArray2[29] = (byte) 178;
    sourceArray2[36] = (byte) 19;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12569()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 250,
        (byte) 22,
        (byte) 206,
        (byte) 126,
        (byte) 80 /*0x50*/,
        (byte) 187,
        (byte) 20,
        (byte) 93,
        (byte) 179,
        (byte) 124
      };
      byte[] numArray3 = new byte[10];
      numArray3[6] = (byte) 241;
      numArray3[2] = (byte) 15;
      numArray3[9] = (byte) 166;
      numArray3[3] = (byte) 129;
      numArray3[4] = (byte) 33;
      numArray3[1] = (byte) 218;
      numArray3[5] = (byte) 168;
      numArray3[7] = (byte) 38;
      numArray3[0] = (byte) 67;
      numArray3[8] = (byte) 138;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 254,
      (byte) 253,
      (byte) 47,
      (byte) 61,
      (byte) 237,
      (byte) 234,
      (byte) 20,
      (byte) 99,
      (byte) 117,
      (byte) 119
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 252,
      (byte) 148,
      (byte) 25,
      (byte) 166,
      (byte) 150,
      (byte) 42,
      (byte) 20,
      (byte) 32 /*0x20*/,
      (byte) 123,
      (byte) 88
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_12563.sspq, 0, (Array) numArray7, 0, 49);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12563.sspr, 0, (Array) numArray7, 0, 49);
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
