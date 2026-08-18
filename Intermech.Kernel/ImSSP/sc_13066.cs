// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13066
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13066
{
  private static byte[] sspq = new byte[155]
  {
    (byte) 220,
    (byte) 189,
    (byte) 26,
    (byte) 187,
    (byte) 78,
    (byte) 77,
    (byte) 231,
    (byte) 175,
    (byte) 14,
    (byte) 187,
    (byte) 65,
    (byte) 23,
    (byte) 212,
    (byte) 91,
    (byte) 9,
    (byte) 38,
    (byte) 89,
    (byte) 243,
    (byte) 187,
    (byte) 52,
    (byte) 252,
    (byte) 196,
    (byte) 38,
    (byte) 19,
    (byte) 246,
    (byte) 69,
    (byte) 108,
    (byte) 14,
    (byte) 230,
    (byte) 179,
    (byte) 84,
    (byte) 54,
    (byte) 238,
    (byte) 231,
    (byte) 38,
    (byte) 57,
    (byte) 192 /*0xC0*/,
    (byte) 221,
    (byte) 222,
    (byte) 96 /*0x60*/,
    (byte) 246,
    (byte) 41,
    (byte) 13,
    (byte) 186,
    (byte) 4,
    (byte) 74,
    (byte) 168,
    (byte) 196,
    (byte) 108,
    (byte) 66,
    (byte) 140,
    (byte) 188,
    (byte) 248,
    (byte) 208 /*0xD0*/,
    (byte) 71,
    (byte) 175,
    (byte) 146,
    (byte) 177,
    (byte) 72,
    (byte) 123,
    (byte) 76,
    (byte) 133,
    (byte) 143,
    (byte) 67,
    (byte) 54,
    (byte) 21,
    (byte) 144 /*0x90*/,
    (byte) 139,
    (byte) 221,
    (byte) 56,
    (byte) 91,
    (byte) 5,
    (byte) 204,
    (byte) 197,
    (byte) 61,
    (byte) 251,
    (byte) 36,
    (byte) 41,
    (byte) 2,
    (byte) 50,
    (byte) 87,
    (byte) 119,
    (byte) 46,
    (byte) 86,
    (byte) 254,
    (byte) 171,
    (byte) 227,
    (byte) 119,
    (byte) 116,
    (byte) 136,
    (byte) 56,
    (byte) 231,
    (byte) 203,
    (byte) 63 /*0x3F*/,
    (byte) 126,
    (byte) 20,
    (byte) 111,
    (byte) 186,
    (byte) 148,
    (byte) 63 /*0x3F*/,
    (byte) 75,
    (byte) 37,
    (byte) 9,
    (byte) 188,
    (byte) 6,
    (byte) 240 /*0xF0*/,
    (byte) 231,
    (byte) 188,
    (byte) 9,
    (byte) 209,
    (byte) 181,
    byte.MaxValue,
    (byte) 152,
    (byte) 156,
    (byte) 17,
    (byte) 52,
    (byte) 183,
    (byte) 24,
    (byte) 174,
    (byte) 32 /*0x20*/,
    (byte) 250,
    (byte) 30,
    (byte) 221,
    (byte) 141,
    (byte) 31 /*0x1F*/,
    (byte) 120,
    (byte) 127 /*0x7F*/,
    (byte) 13,
    (byte) 28,
    (byte) 236,
    (byte) 33,
    (byte) 64 /*0x40*/,
    (byte) 148,
    (byte) 8,
    (byte) 130,
    (byte) 208 /*0xD0*/,
    (byte) 194,
    (byte) 28,
    (byte) 250,
    (byte) 222,
    (byte) 124,
    (byte) 58,
    (byte) 12,
    byte.MaxValue,
    (byte) 11,
    (byte) 160 /*0xA0*/,
    (byte) 61,
    (byte) 147,
    (byte) 151,
    (byte) 217,
    (byte) 179,
    (byte) 200,
    (byte) 237,
    (byte) 45,
    (byte) 196
  };
  private static byte[] sspr = new byte[155]
  {
    (byte) 235,
    (byte) 163,
    (byte) 11,
    (byte) 108,
    (byte) 165,
    (byte) 158,
    (byte) 205,
    (byte) 161,
    (byte) 120,
    (byte) 67,
    (byte) 105,
    (byte) 175,
    (byte) 60,
    (byte) 74,
    (byte) 67,
    (byte) 225,
    (byte) 182,
    (byte) 55,
    (byte) 143,
    (byte) 112 /*0x70*/,
    (byte) 0,
    (byte) 141,
    (byte) 227,
    (byte) 230,
    (byte) 215,
    (byte) 93,
    (byte) 55,
    (byte) 254,
    (byte) 94,
    (byte) 85,
    (byte) 182,
    (byte) 229,
    (byte) 212,
    (byte) 248,
    (byte) 65,
    (byte) 67,
    (byte) 77,
    (byte) 249,
    (byte) 18,
    (byte) 114,
    (byte) 202,
    (byte) 20,
    (byte) 192 /*0xC0*/,
    (byte) 136,
    (byte) 71,
    (byte) 103,
    (byte) 5,
    (byte) 25,
    (byte) 178,
    (byte) 214,
    (byte) 243,
    (byte) 127 /*0x7F*/,
    (byte) 219,
    (byte) 61,
    (byte) 157,
    (byte) 42,
    (byte) 27,
    (byte) 184,
    (byte) 23,
    (byte) 210,
    (byte) 42,
    (byte) 44,
    (byte) 108,
    (byte) 114,
    (byte) 131,
    (byte) 197,
    (byte) 204,
    (byte) 235,
    (byte) 141,
    (byte) 37,
    (byte) 235,
    (byte) 66,
    (byte) 142,
    (byte) 38,
    (byte) 106,
    (byte) 241,
    (byte) 114,
    (byte) 119,
    (byte) 230,
    (byte) 204,
    (byte) 68,
    (byte) 164,
    (byte) 92,
    byte.MaxValue,
    (byte) 99,
    (byte) 91,
    (byte) 86,
    (byte) 21,
    (byte) 163,
    (byte) 129,
    (byte) 20,
    (byte) 164,
    (byte) 200,
    (byte) 67,
    (byte) 127 /*0x7F*/,
    (byte) 3,
    (byte) 137,
    (byte) 62,
    (byte) 3,
    (byte) 89,
    (byte) 164,
    (byte) 61,
    (byte) 229,
    (byte) 169,
    (byte) 142,
    (byte) 3,
    (byte) 34,
    (byte) 142,
    (byte) 73,
    (byte) 236,
    (byte) 43,
    (byte) 206,
    (byte) 67,
    (byte) 216,
    (byte) 76,
    (byte) 128 /*0x80*/,
    (byte) 232,
    (byte) 92,
    (byte) 11,
    (byte) 197,
    (byte) 59,
    (byte) 203,
    (byte) 12,
    (byte) 64 /*0x40*/,
    (byte) 98,
    (byte) 174,
    (byte) 2,
    (byte) 161,
    (byte) 92,
    (byte) 66,
    (byte) 33,
    (byte) 164,
    (byte) 0,
    (byte) 123,
    (byte) 62,
    (byte) 30,
    (byte) 130,
    (byte) 229,
    (byte) 188,
    (byte) 92,
    (byte) 48 /*0x30*/,
    (byte) 120,
    (byte) 35,
    (byte) 225,
    (byte) 204,
    (byte) 17,
    (byte) 187,
    (byte) 223,
    (byte) 144 /*0x90*/,
    (byte) 242,
    (byte) 104,
    (byte) 70,
    (byte) 230,
    (byte) 137,
    (byte) 99
  };

  internal static string ssp_appserver_13067()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[38];
      byte[] numArray2 = new byte[38];
      numArray2[15] = (byte) 130;
      numArray2[28] = (byte) 176 /*0xB0*/;
      numArray2[8] = (byte) 66;
      numArray2[17] = (byte) 196;
      numArray2[5] = (byte) 33;
      numArray2[30] = (byte) 201;
      numArray2[6] = (byte) 116;
      numArray2[32 /*0x20*/] = (byte) 173;
      numArray2[4] = (byte) 225;
      numArray2[29] = (byte) 85;
      numArray2[13] = (byte) 0;
      numArray2[11] = (byte) 207;
      numArray2[12] = (byte) 57;
      numArray2[37] = (byte) 24;
      numArray2[14] = (byte) 39;
      numArray2[22] = (byte) 238;
      numArray2[16 /*0x10*/] = (byte) 146;
      numArray2[0] = (byte) 245;
      numArray2[9] = (byte) 23;
      numArray2[19] = (byte) 143;
      numArray2[20] = (byte) 189;
      numArray2[2] = (byte) 168;
      numArray2[7] = (byte) 69;
      numArray2[23] = (byte) 202;
      numArray2[24] = (byte) 212;
      numArray2[25] = (byte) 30;
      numArray2[26] = (byte) 192 /*0xC0*/;
      numArray2[27] = (byte) 9;
      numArray2[21] = (byte) 109;
      numArray2[10] = (byte) 32 /*0x20*/;
      numArray2[18] = (byte) 246;
      numArray2[31 /*0x1F*/] = (byte) 99;
      numArray2[36] = (byte) 146;
      numArray2[33] = (byte) 230;
      numArray2[34] = (byte) 200;
      numArray2[35] = byte.MaxValue;
      numArray2[3] = (byte) 165;
      numArray2[1] = (byte) 187;
      byte[] numArray3 = new byte[38]
      {
        (byte) 71,
        (byte) 250,
        (byte) 115,
        (byte) 127 /*0x7F*/,
        (byte) 165,
        (byte) 199,
        (byte) 102,
        (byte) 63 /*0x3F*/,
        (byte) 139,
        (byte) 65,
        (byte) 154,
        (byte) 139,
        (byte) 25,
        (byte) 199,
        (byte) 116,
        (byte) 190,
        (byte) 156,
        (byte) 200,
        (byte) 73,
        (byte) 168,
        (byte) 60,
        (byte) 95,
        (byte) 176 /*0xB0*/,
        (byte) 26,
        (byte) 179,
        byte.MaxValue,
        (byte) 69,
        (byte) 137,
        (byte) 31 /*0x1F*/,
        (byte) 114,
        (byte) 192 /*0xC0*/,
        (byte) 103,
        (byte) 145,
        (byte) 68,
        (byte) 53,
        (byte) 248,
        (byte) 210,
        (byte) 82
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[38];
    byte[] numArray5 = new byte[38];
    numArray5[4] = (byte) 168;
    numArray5[1] = (byte) 25;
    numArray5[2] = (byte) 155;
    numArray5[9] = (byte) 214;
    numArray5[30] = (byte) 22;
    numArray5[17] = (byte) 47;
    numArray5[6] = (byte) 68;
    numArray5[7] = (byte) 227;
    numArray5[13] = (byte) 70;
    numArray5[0] = (byte) 152;
    numArray5[8] = (byte) 226;
    numArray5[11] = (byte) 50;
    numArray5[3] = (byte) 125;
    numArray5[20] = (byte) 110;
    numArray5[29] = (byte) 149;
    numArray5[32 /*0x20*/] = (byte) 56;
    numArray5[16 /*0x10*/] = (byte) 238;
    numArray5[10] = (byte) 64 /*0x40*/;
    numArray5[23] = (byte) 237;
    numArray5[19] = (byte) 11;
    numArray5[31 /*0x1F*/] = (byte) 125;
    numArray5[21] = (byte) 26;
    numArray5[22] = (byte) 177;
    numArray5[18] = (byte) 117;
    numArray5[24] = (byte) 99;
    numArray5[36] = (byte) 197;
    numArray5[5] = (byte) 234;
    numArray5[27] = (byte) 97;
    numArray5[12] = (byte) 180;
    numArray5[26] = (byte) 240 /*0xF0*/;
    numArray5[14] = (byte) 220;
    numArray5[33] = (byte) 62;
    numArray5[28] = (byte) 98;
    numArray5[15] = (byte) 118;
    numArray5[37] = (byte) 237;
    numArray5[35] = (byte) 170;
    numArray5[34] = (byte) 57;
    numArray5[25] = (byte) 23;
    byte[] numArray6 = new byte[38]
    {
      (byte) 144 /*0x90*/,
      (byte) 160 /*0xA0*/,
      (byte) 203,
      (byte) 146,
      (byte) 19,
      (byte) 216,
      (byte) 77,
      (byte) 229,
      (byte) 115,
      (byte) 185,
      (byte) 131,
      (byte) 157,
      (byte) 100,
      (byte) 116,
      (byte) 107,
      (byte) 46,
      (byte) 83,
      (byte) 216,
      (byte) 65,
      (byte) 124,
      (byte) 194,
      (byte) 3,
      (byte) 242,
      (byte) 174,
      (byte) 24,
      (byte) 139,
      (byte) 103,
      (byte) 112 /*0x70*/,
      (byte) 250,
      (byte) 32 /*0x20*/,
      (byte) 62,
      (byte) 135,
      (byte) 14,
      (byte) 93,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 75,
      (byte) 22
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 38);
    for (int index = 0; index < 38; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[26];
    byte[] response = new byte[26];
    Array.Copy((Array) sc_13066.sspq, 0, (Array) numArray7, 0, 26);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13066.sspr, 0, (Array) numArray7, 0, 26);
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

  internal static string ssp_appserver_13068()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 35,
        (byte) 135,
        (byte) 154,
        (byte) 163,
        (byte) 51,
        (byte) 184,
        (byte) 171,
        (byte) 180,
        (byte) 60,
        (byte) 200
      };
      byte[] numArray3 = new byte[10];
      numArray3[5] = (byte) 73;
      numArray3[3] = (byte) 215;
      numArray3[7] = (byte) 98;
      numArray3[2] = (byte) 111;
      numArray3[9] = (byte) 108;
      numArray3[1] = (byte) 91;
      numArray3[6] = (byte) 228;
      numArray3[4] = (byte) 181;
      numArray3[8] = (byte) 192 /*0xC0*/;
      numArray3[0] = (byte) 187;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 60,
      (byte) 230,
      (byte) 121,
      (byte) 193,
      (byte) 112 /*0x70*/,
      (byte) 23,
      (byte) 150,
      (byte) 6,
      (byte) 196,
      (byte) 8
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 31 /*0x1F*/,
      (byte) 125,
      (byte) 129,
      (byte) 111,
      (byte) 47,
      (byte) 229,
      (byte) 239,
      (byte) 226,
      (byte) 253,
      (byte) 254
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13069(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 14,
      (byte) 95,
      (byte) 40,
      (byte) 167,
      (byte) 204,
      (byte) 93,
      (byte) 147,
      (byte) 245,
      (byte) 73,
      (byte) 88,
      (byte) 179,
      (byte) 92,
      (byte) 213,
      (byte) 34,
      (byte) 93,
      (byte) 25,
      (byte) 160 /*0xA0*/,
      (byte) 185,
      (byte) 172,
      (byte) 94,
      (byte) 189,
      (byte) 186,
      (byte) 246,
      (byte) 59,
      (byte) 156,
      (byte) 166,
      (byte) 194,
      (byte) 0,
      (byte) 244,
      (byte) 172,
      (byte) 22,
      (byte) 33,
      (byte) 244,
      (byte) 57,
      (byte) 226,
      (byte) 57,
      (byte) 4,
      (byte) 62,
      (byte) 156,
      (byte) 125,
      (byte) 168,
      (byte) 241,
      (byte) 47,
      (byte) 105,
      (byte) 189,
      (byte) 187,
      (byte) 193,
      (byte) 227
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 134,
      (byte) 143,
      (byte) 82,
      (byte) 42,
      (byte) 108,
      (byte) 231,
      (byte) 63 /*0x3F*/,
      (byte) 28,
      (byte) 28,
      (byte) 13,
      (byte) 74,
      (byte) 181,
      (byte) 122,
      (byte) 52,
      (byte) 24,
      (byte) 72,
      byte.MaxValue,
      (byte) 54,
      (byte) 202,
      (byte) 26,
      (byte) 117,
      (byte) 87,
      (byte) 183,
      (byte) 12,
      (byte) 59,
      (byte) 32 /*0x20*/,
      (byte) 54,
      (byte) 196,
      (byte) 6,
      (byte) 101,
      (byte) 6,
      (byte) 119,
      (byte) 206,
      (byte) 181,
      (byte) 35,
      (byte) 147,
      (byte) 250,
      (byte) 150,
      (byte) 153,
      (byte) 115,
      (byte) 91,
      (byte) 3,
      (byte) 125,
      (byte) 157,
      (byte) 239,
      (byte) 216,
      (byte) 2,
      (byte) 19
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13070(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 20,
      (byte) 90,
      (byte) 84,
      (byte) 105,
      (byte) 236,
      (byte) 11,
      (byte) 38,
      (byte) 155,
      (byte) 147,
      (byte) 0,
      (byte) 156,
      (byte) 65,
      (byte) 146,
      (byte) 103,
      (byte) 155,
      (byte) 64 /*0x40*/,
      (byte) 35,
      (byte) 21,
      (byte) 97,
      (byte) 28,
      (byte) 46,
      (byte) 182,
      (byte) 203,
      (byte) 138,
      (byte) 141,
      (byte) 126,
      (byte) 224 /*0xE0*/,
      (byte) 228,
      (byte) 222,
      (byte) 153,
      (byte) 5,
      (byte) 134,
      (byte) 11,
      (byte) 164,
      (byte) 224 /*0xE0*/,
      (byte) 222,
      (byte) 142,
      (byte) 208 /*0xD0*/,
      (byte) 23,
      (byte) 51,
      (byte) 118,
      (byte) 226,
      (byte) 20,
      (byte) 240 /*0xF0*/,
      (byte) 147,
      (byte) 39,
      (byte) 43,
      (byte) 83
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 230,
      (byte) 143,
      (byte) 167,
      (byte) 203,
      (byte) 159,
      (byte) 217,
      (byte) 23,
      (byte) 161,
      (byte) 229,
      (byte) 3,
      (byte) 143,
      (byte) 253,
      (byte) 65,
      (byte) 157,
      (byte) 144 /*0x90*/,
      (byte) 107,
      (byte) 134,
      (byte) 191,
      (byte) 14,
      (byte) 195,
      (byte) 105,
      (byte) 192 /*0xC0*/,
      (byte) 231,
      (byte) 52,
      (byte) 71,
      (byte) 48 /*0x30*/,
      (byte) 56,
      (byte) 40,
      (byte) 43,
      (byte) 49,
      (byte) 201,
      (byte) 14,
      (byte) 190,
      (byte) 19,
      (byte) 0,
      (byte) 81,
      (byte) 124,
      (byte) 162,
      (byte) 14,
      (byte) 170,
      (byte) 249,
      (byte) 88,
      (byte) 79,
      (byte) 203,
      (byte) 209,
      (byte) 226,
      (byte) 182,
      (byte) 9
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13071()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 196,
        (byte) 238,
        (byte) 77,
        (byte) 93,
        (byte) 143,
        (byte) 201,
        (byte) 219,
        (byte) 110,
        (byte) 42,
        (byte) 65,
        (byte) 188,
        (byte) 250,
        (byte) 251,
        (byte) 22,
        (byte) 54,
        (byte) 237,
        (byte) 135,
        (byte) 189,
        (byte) 177,
        (byte) 83,
        (byte) 254,
        (byte) 46,
        (byte) 166
      };
      byte[] numArray3 = new byte[23];
      numArray3[9] = (byte) 227;
      numArray3[1] = (byte) 245;
      numArray3[10] = (byte) 153;
      numArray3[3] = (byte) 253;
      numArray3[4] = (byte) 71;
      numArray3[5] = (byte) 46;
      numArray3[20] = (byte) 208 /*0xD0*/;
      numArray3[7] = (byte) 58;
      numArray3[6] = (byte) 132;
      numArray3[15] = (byte) 21;
      numArray3[12] = (byte) 9;
      numArray3[2] = (byte) 1;
      numArray3[11] = (byte) 108;
      numArray3[13] = (byte) 1;
      numArray3[18] = (byte) 86;
      numArray3[0] = (byte) 123;
      numArray3[14] = (byte) 191;
      numArray3[17] = (byte) 139;
      numArray3[8] = (byte) 184;
      numArray3[19] = (byte) 44;
      numArray3[22] = (byte) 135;
      numArray3[21] = (byte) 217;
      numArray3[16 /*0x10*/] = (byte) 72;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[15] = (byte) 129;
    numArray5[1] = (byte) 204;
    numArray5[2] = (byte) 104;
    numArray5[11] = (byte) 211;
    numArray5[13] = (byte) 27;
    numArray5[19] = (byte) 84;
    numArray5[6] = (byte) 150;
    numArray5[9] = (byte) 188;
    numArray5[8] = (byte) 1;
    numArray5[4] = (byte) 240 /*0xF0*/;
    numArray5[5] = (byte) 220;
    numArray5[12] = (byte) 117;
    numArray5[0] = (byte) 61;
    numArray5[17] = (byte) 164;
    numArray5[14] = (byte) 157;
    numArray5[16 /*0x10*/] = (byte) 128 /*0x80*/;
    numArray5[10] = (byte) 180;
    numArray5[7] = (byte) 72;
    numArray5[18] = (byte) 207;
    numArray5[21] = (byte) 150;
    numArray5[20] = (byte) 24;
    numArray5[3] = (byte) 241;
    numArray5[22] = (byte) 4;
    byte[] numArray6 = new byte[23]
    {
      (byte) 108,
      (byte) 163,
      (byte) 109,
      (byte) 153,
      (byte) 196,
      (byte) 140,
      (byte) 186,
      (byte) 136,
      (byte) 167,
      (byte) 238,
      (byte) 250,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 21,
      (byte) 9,
      (byte) 187,
      (byte) 7,
      (byte) 216,
      (byte) 6,
      (byte) 134,
      (byte) 18,
      (byte) 214,
      (byte) 172
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13072(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 118,
      (byte) 37,
      (byte) 40,
      (byte) 216,
      (byte) 167,
      (byte) 200,
      (byte) 250,
      (byte) 4,
      (byte) 145,
      (byte) 134,
      (byte) 113,
      (byte) 214,
      (byte) 218,
      (byte) 53,
      (byte) 206,
      (byte) 89,
      (byte) 0,
      (byte) 5,
      (byte) 221,
      (byte) 42,
      (byte) 55,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 42,
      (byte) 67,
      (byte) 64 /*0x40*/,
      (byte) 62,
      (byte) 210,
      (byte) 252,
      (byte) 222,
      (byte) 199,
      (byte) 185,
      (byte) 190,
      (byte) 129,
      (byte) 16 /*0x10*/,
      (byte) 191,
      (byte) 81,
      (byte) 156,
      (byte) 110,
      (byte) 248,
      (byte) 93,
      (byte) 69,
      (byte) 211,
      (byte) 200,
      (byte) 185,
      (byte) 254,
      (byte) 123,
      (byte) 145
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 190,
      (byte) 80 /*0x50*/,
      (byte) 106,
      (byte) 209,
      (byte) 144 /*0x90*/,
      (byte) 227,
      (byte) 215,
      (byte) 67,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 78,
      (byte) 209,
      (byte) 121,
      (byte) 121,
      (byte) 172,
      (byte) 252,
      (byte) 38,
      (byte) 9,
      (byte) 177,
      (byte) 101,
      (byte) 194,
      (byte) 64 /*0x40*/,
      (byte) 109,
      (byte) 4,
      (byte) 182,
      (byte) 183,
      (byte) 108,
      (byte) 139,
      (byte) 3,
      (byte) 185,
      (byte) 198,
      (byte) 202,
      (byte) 123,
      (byte) 142,
      (byte) 27,
      (byte) 101,
      (byte) 245,
      (byte) 210,
      (byte) 63 /*0x3F*/,
      (byte) 55,
      (byte) 136,
      (byte) 140,
      (byte) 193,
      (byte) 85,
      (byte) 208 /*0xD0*/,
      (byte) 182,
      (byte) 157,
      (byte) 207
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13073()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[80 /*0x50*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 142,
        (byte) 136,
        (byte) 110,
        (byte) 55,
        (byte) 164,
        (byte) 254,
        (byte) 149,
        (byte) 103,
        (byte) 137,
        (byte) 40,
        (byte) 8,
        (byte) 179,
        (byte) 190,
        (byte) 183,
        (byte) 23,
        (byte) 89,
        (byte) 36,
        (byte) 15,
        (byte) 7,
        (byte) 99,
        (byte) 151,
        (byte) 228,
        (byte) 205,
        (byte) 241,
        (byte) 107,
        (byte) 184,
        (byte) 112 /*0x70*/,
        (byte) 68,
        (byte) 197,
        (byte) 173,
        (byte) 5,
        (byte) 99,
        (byte) 94,
        (byte) 63 /*0x3F*/,
        (byte) 43,
        (byte) 54,
        (byte) 19,
        (byte) 217,
        (byte) 26,
        (byte) 83,
        (byte) 7,
        (byte) 7,
        (byte) 110,
        (byte) 52,
        (byte) 61,
        (byte) 32 /*0x20*/,
        (byte) 9,
        (byte) 1,
        (byte) 230,
        (byte) 34,
        (byte) 108,
        (byte) 189,
        (byte) 144 /*0x90*/,
        (byte) 28,
        (byte) 241
      };
      byte[] numArray3 = new byte[55];
      numArray3[0] = (byte) 168;
      numArray3[21] = (byte) 21;
      numArray3[33] = (byte) 134;
      numArray3[43] = (byte) 167;
      numArray3[4] = (byte) 113;
      numArray3[5] = (byte) 95;
      numArray3[6] = (byte) 248;
      numArray3[7] = (byte) 175;
      numArray3[8] = (byte) 77;
      numArray3[37] = (byte) 24;
      numArray3[14] = (byte) 20;
      numArray3[20] = (byte) 145;
      numArray3[12] = (byte) 248;
      numArray3[13] = (byte) 95;
      numArray3[34] = (byte) 245;
      numArray3[46] = (byte) 63 /*0x3F*/;
      numArray3[9] = (byte) 40;
      numArray3[17] = (byte) 157;
      numArray3[24] = (byte) 104;
      numArray3[36] = (byte) 186;
      numArray3[38] = (byte) 75;
      numArray3[48 /*0x30*/] = (byte) 245;
      numArray3[35] = (byte) 6;
      numArray3[18] = (byte) 140;
      numArray3[31 /*0x1F*/] = (byte) 209;
      numArray3[25] = (byte) 162;
      numArray3[32 /*0x20*/] = (byte) 45;
      numArray3[11] = (byte) 95;
      numArray3[28] = (byte) 30;
      numArray3[29] = (byte) 209;
      numArray3[3] = (byte) 92;
      numArray3[19] = (byte) 25;
      numArray3[45] = (byte) 141;
      numArray3[16 /*0x10*/] = (byte) 247;
      numArray3[27] = (byte) 49;
      numArray3[1] = (byte) 225;
      numArray3[23] = (byte) 205;
      numArray3[2] = (byte) 21;
      numArray3[53] = (byte) 214;
      numArray3[39] = (byte) 105;
      numArray3[15] = (byte) 20;
      numArray3[40] = (byte) 166;
      numArray3[42] = (byte) 109;
      numArray3[10] = (byte) 4;
      numArray3[44] = (byte) 43;
      numArray3[22] = (byte) 110;
      numArray3[26] = (byte) 196;
      numArray3[47] = (byte) 106;
      numArray3[30] = (byte) 5;
      numArray3[49] = (byte) 49;
      numArray3[50] = (byte) 184;
      numArray3[51] = (byte) 235;
      numArray3[52] = (byte) 35;
      numArray3[41] = (byte) 191;
      numArray3[54] = (byte) 186;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25]
      {
        (byte) 199,
        (byte) 129,
        (byte) 60,
        (byte) 30,
        (byte) 242,
        byte.MaxValue,
        (byte) 155,
        (byte) 218,
        (byte) 209,
        (byte) 17,
        (byte) 48 /*0x30*/,
        (byte) 9,
        (byte) 101,
        (byte) 174,
        (byte) 61,
        (byte) 198,
        (byte) 237,
        (byte) 164,
        (byte) 42,
        (byte) 227,
        (byte) 134,
        (byte) 27,
        (byte) 127 /*0x7F*/,
        (byte) 37,
        (byte) 248
      };
      byte[] numArray5 = new byte[25];
      numArray5[15] = (byte) 60;
      numArray5[1] = (byte) 43;
      numArray5[23] = (byte) 103;
      numArray5[9] = (byte) 184;
      numArray5[13] = (byte) 213;
      numArray5[2] = (byte) 158;
      numArray5[6] = (byte) 2;
      numArray5[0] = (byte) 106;
      numArray5[8] = (byte) 200;
      numArray5[12] = (byte) 32 /*0x20*/;
      numArray5[5] = (byte) 74;
      numArray5[11] = (byte) 77;
      numArray5[4] = (byte) 81;
      numArray5[10] = (byte) 215;
      numArray5[7] = (byte) 6;
      numArray5[16 /*0x10*/] = (byte) 175;
      numArray5[14] = (byte) 27;
      numArray5[17] = (byte) 80 /*0x50*/;
      numArray5[18] = (byte) 13;
      numArray5[19] = (byte) 164;
      numArray5[20] = (byte) 151;
      numArray5[21] = (byte) 215;
      numArray5[22] = (byte) 28;
      numArray5[3] = (byte) 173;
      numArray5[24] = byte.MaxValue;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[80 /*0x50*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 79,
      (byte) 155,
      (byte) 171,
      (byte) 2,
      (byte) 23,
      (byte) 38,
      (byte) 153,
      (byte) 56,
      (byte) 125,
      (byte) 90,
      (byte) 218,
      (byte) 143,
      (byte) 66,
      (byte) 185,
      (byte) 12,
      (byte) 223,
      (byte) 178,
      (byte) 211,
      (byte) 70,
      (byte) 223,
      (byte) 196,
      (byte) 236,
      (byte) 78,
      (byte) 84,
      (byte) 196,
      (byte) 202,
      (byte) 139,
      (byte) 185,
      (byte) 153,
      (byte) 57,
      (byte) 125,
      (byte) 8,
      (byte) 102,
      (byte) 182,
      byte.MaxValue,
      (byte) 189,
      (byte) 35,
      (byte) 137,
      (byte) 218,
      (byte) 128 /*0x80*/,
      (byte) 42,
      (byte) 45,
      (byte) 121,
      (byte) 221,
      (byte) 237,
      (byte) 42,
      (byte) 23,
      (byte) 179,
      (byte) 146,
      (byte) 78,
      (byte) 50,
      (byte) 46,
      (byte) 167,
      (byte) 60,
      (byte) 114
    };
    byte[] numArray8 = new byte[55];
    numArray8[32 /*0x20*/] = (byte) 68;
    numArray8[29] = (byte) 250;
    numArray8[12] = (byte) 26;
    numArray8[17] = (byte) 125;
    numArray8[22] = (byte) 120;
    numArray8[5] = (byte) 93;
    numArray8[6] = (byte) 231;
    numArray8[7] = (byte) 117;
    numArray8[28] = (byte) 8;
    numArray8[9] = (byte) 198;
    numArray8[10] = (byte) 21;
    numArray8[42] = (byte) 194;
    numArray8[51] = (byte) 60;
    numArray8[33] = (byte) 152;
    numArray8[14] = (byte) 180;
    numArray8[19] = (byte) 202;
    numArray8[16 /*0x10*/] = (byte) 30;
    numArray8[20] = (byte) 249;
    numArray8[18] = (byte) 99;
    numArray8[50] = (byte) 251;
    numArray8[15] = (byte) 10;
    numArray8[21] = (byte) 109;
    numArray8[36] = (byte) 71;
    numArray8[23] = (byte) 21;
    numArray8[54] = (byte) 108;
    numArray8[25] = (byte) 11;
    numArray8[34] = (byte) 62;
    numArray8[26] = (byte) 1;
    numArray8[3] = (byte) 247;
    numArray8[1] = (byte) 180;
    numArray8[4] = (byte) 145;
    numArray8[30] = (byte) 190;
    numArray8[0] = (byte) 188;
    numArray8[43] = (byte) 167;
    numArray8[35] = (byte) 206;
    numArray8[8] = (byte) 51;
    numArray8[31 /*0x1F*/] = (byte) 42;
    numArray8[37] = (byte) 27;
    numArray8[38] = (byte) 186;
    numArray8[39] = (byte) 137;
    numArray8[40] = (byte) 95;
    numArray8[41] = (byte) 13;
    numArray8[11] = (byte) 198;
    numArray8[24] = (byte) 74;
    numArray8[44] = (byte) 185;
    numArray8[45] = (byte) 144 /*0x90*/;
    numArray8[46] = (byte) 93;
    numArray8[47] = (byte) 142;
    numArray8[13] = (byte) 194;
    numArray8[27] = (byte) 149;
    numArray8[2] = (byte) 89;
    numArray8[49] = (byte) 44;
    numArray8[52] = (byte) 4;
    numArray8[53] = (byte) 203;
    numArray8[48 /*0x30*/] = (byte) 98;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[25]
    {
      (byte) 240 /*0xF0*/,
      (byte) 163,
      (byte) 153,
      (byte) 195,
      (byte) 186,
      (byte) 84,
      (byte) 60,
      (byte) 142,
      (byte) 114,
      byte.MaxValue,
      (byte) 71,
      (byte) 29,
      (byte) 222,
      (byte) 240 /*0xF0*/,
      (byte) 221,
      (byte) 107,
      (byte) 225,
      (byte) 107,
      (byte) 8,
      (byte) 62,
      (byte) 112 /*0x70*/,
      (byte) 76,
      (byte) 152,
      (byte) 40,
      (byte) 222
    };
    byte[] numArray10 = new byte[25]
    {
      (byte) 172,
      (byte) 13,
      (byte) 220,
      (byte) 80 /*0x50*/,
      (byte) 40,
      (byte) 242,
      (byte) 184,
      (byte) 136,
      (byte) 73,
      (byte) 19,
      (byte) 45,
      (byte) 203,
      (byte) 76,
      (byte) 104,
      (byte) 62,
      (byte) 213,
      (byte) 162,
      (byte) 212,
      (byte) 113,
      (byte) 113,
      (byte) 196,
      (byte) 106,
      (byte) 156,
      (byte) 107,
      (byte) 92
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 25);
    for (int index = 0; index < 25; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_13074(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 92;
    sourceArray1[1] = (byte) 104;
    sourceArray1[2] = (byte) 172;
    sourceArray1[8] = (byte) 143;
    sourceArray1[25] = (byte) 163;
    sourceArray1[19] = (byte) 201;
    sourceArray1[6] = (byte) 244;
    sourceArray1[0] = (byte) 163;
    sourceArray1[20] = (byte) 0;
    sourceArray1[9] = (byte) 58;
    sourceArray1[10] = (byte) 248;
    sourceArray1[37] = (byte) 74;
    sourceArray1[12] = (byte) 168;
    sourceArray1[42] = (byte) 220;
    sourceArray1[33] = (byte) 243;
    sourceArray1[15] = (byte) 219;
    sourceArray1[11] = (byte) 114;
    sourceArray1[39] = (byte) 53;
    sourceArray1[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
    sourceArray1[27] = (byte) 112 /*0x70*/;
    sourceArray1[16 /*0x10*/] = (byte) 100;
    sourceArray1[21] = (byte) 27;
    sourceArray1[22] = (byte) 211;
    sourceArray1[23] = (byte) 177;
    sourceArray1[24] = (byte) 106;
    sourceArray1[30] = (byte) 155;
    sourceArray1[26] = (byte) 10;
    sourceArray1[14] = (byte) 67;
    sourceArray1[28] = (byte) 229;
    sourceArray1[4] = (byte) 219;
    sourceArray1[5] = (byte) 168;
    sourceArray1[17] = (byte) 161;
    sourceArray1[32 /*0x20*/] = (byte) 116;
    sourceArray1[3] = (byte) 148;
    sourceArray1[44] = (byte) 70;
    sourceArray1[35] = (byte) 223;
    sourceArray1[36] = (byte) 112 /*0x70*/;
    sourceArray1[13] = (byte) 2;
    sourceArray1[38] = (byte) 20;
    sourceArray1[29] = (byte) 215;
    sourceArray1[40] = (byte) 119;
    sourceArray1[41] = (byte) 191;
    sourceArray1[45] = (byte) 254;
    sourceArray1[18] = (byte) 44;
    sourceArray1[7] = (byte) 33;
    sourceArray1[43] = (byte) 206;
    sourceArray1[46] = (byte) 164;
    sourceArray1[47] = (byte) 150;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[46] = (byte) 101;
    sourceArray2[17] = (byte) 185;
    sourceArray2[2] = (byte) 16 /*0x10*/;
    sourceArray2[3] = (byte) 125;
    sourceArray2[45] = (byte) 4;
    sourceArray2[14] = (byte) 65;
    sourceArray2[27] = (byte) 169;
    sourceArray2[10] = (byte) 108;
    sourceArray2[24] = (byte) 174;
    sourceArray2[9] = (byte) 202;
    sourceArray2[13] = byte.MaxValue;
    sourceArray2[11] = (byte) 37;
    sourceArray2[19] = (byte) 106;
    sourceArray2[6] = (byte) 167;
    sourceArray2[35] = (byte) 60;
    sourceArray2[36] = (byte) 166;
    sourceArray2[0] = (byte) 5;
    sourceArray2[15] = (byte) 201;
    sourceArray2[34] = (byte) 73;
    sourceArray2[16 /*0x10*/] = (byte) 67;
    sourceArray2[39] = (byte) 33;
    sourceArray2[21] = (byte) 135;
    sourceArray2[22] = (byte) 4;
    sourceArray2[4] = (byte) 124;
    sourceArray2[32 /*0x20*/] = (byte) 229;
    sourceArray2[25] = (byte) 214;
    sourceArray2[26] = (byte) 75;
    sourceArray2[23] = (byte) 88;
    sourceArray2[28] = (byte) 150;
    sourceArray2[40] = (byte) 91;
    sourceArray2[30] = (byte) 26;
    sourceArray2[31 /*0x1F*/] = (byte) 101;
    sourceArray2[33] = (byte) 29;
    sourceArray2[7] = (byte) 220;
    sourceArray2[38] = (byte) 31 /*0x1F*/;
    sourceArray2[20] = (byte) 116;
    sourceArray2[44] = (byte) 54;
    sourceArray2[37] = (byte) 209;
    sourceArray2[1] = (byte) 1;
    sourceArray2[5] = (byte) 233;
    sourceArray2[18] = (byte) 244;
    sourceArray2[8] = (byte) 37;
    sourceArray2[42] = (byte) 120;
    sourceArray2[43] = (byte) 119;
    sourceArray2[41] = (byte) 171;
    sourceArray2[12] = (byte) 8;
    sourceArray2[29] = (byte) 26;
    sourceArray2[47] = (byte) 73;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13075(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 250,
      (byte) 43,
      (byte) 195,
      (byte) 20,
      (byte) 65,
      (byte) 37,
      (byte) 175,
      (byte) 31 /*0x1F*/,
      (byte) 103,
      (byte) 159,
      (byte) 106,
      (byte) 218,
      (byte) 123,
      (byte) 158,
      (byte) 129,
      (byte) 44,
      (byte) 184,
      (byte) 179,
      (byte) 81,
      (byte) 41,
      (byte) 166,
      (byte) 237,
      (byte) 11,
      (byte) 108,
      (byte) 33,
      (byte) 185,
      (byte) 181,
      (byte) 9,
      (byte) 69,
      (byte) 93,
      (byte) 19,
      (byte) 152,
      (byte) 12,
      (byte) 241,
      (byte) 100,
      (byte) 145,
      (byte) 87,
      (byte) 193,
      (byte) 156,
      (byte) 71,
      (byte) 96 /*0x60*/,
      (byte) 99,
      (byte) 162,
      (byte) 172,
      (byte) 136,
      (byte) 31 /*0x1F*/,
      (byte) 23
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 28,
      (byte) 236,
      (byte) 63 /*0x3F*/,
      (byte) 27,
      (byte) 4,
      (byte) 137,
      (byte) 126,
      (byte) 212,
      (byte) 247,
      (byte) 121,
      (byte) 234,
      (byte) 239,
      (byte) 218,
      (byte) 20,
      (byte) 26,
      (byte) 66,
      (byte) 158,
      (byte) 159,
      (byte) 130,
      (byte) 86,
      (byte) 234,
      (byte) 50,
      (byte) 212,
      (byte) 23,
      (byte) 5,
      (byte) 69,
      (byte) 71,
      (byte) 30,
      (byte) 191,
      (byte) 80 /*0x50*/,
      (byte) 77,
      (byte) 163,
      (byte) 116,
      (byte) 227,
      (byte) 54,
      (byte) 19,
      (byte) 79,
      (byte) 171,
      (byte) 37,
      (byte) 132,
      (byte) 150,
      (byte) 224 /*0xE0*/,
      (byte) 22,
      (byte) 185,
      (byte) 25,
      (byte) 181,
      (byte) 254,
      (byte) 81
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13076(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 229;
    sourceArray1[10] = (byte) 218;
    sourceArray1[0] = (byte) 16 /*0x10*/;
    sourceArray1[1] = (byte) 121;
    sourceArray1[43] = (byte) 186;
    sourceArray1[5] = (byte) 86;
    sourceArray1[18] = (byte) 212;
    sourceArray1[7] = (byte) 135;
    sourceArray1[8] = (byte) 111;
    sourceArray1[9] = (byte) 165;
    sourceArray1[11] = (byte) 10;
    sourceArray1[36] = (byte) 41;
    sourceArray1[12] = (byte) 140;
    sourceArray1[40] = (byte) 206;
    sourceArray1[38] = (byte) 114;
    sourceArray1[2] = (byte) 199;
    sourceArray1[45] = (byte) 151;
    sourceArray1[44] = (byte) 137;
    sourceArray1[17] = (byte) 239;
    sourceArray1[4] = (byte) 31 /*0x1F*/;
    sourceArray1[27] = (byte) 2;
    sourceArray1[21] = (byte) 10;
    sourceArray1[22] = (byte) 201;
    sourceArray1[47] = (byte) 182;
    sourceArray1[24] = (byte) 74;
    sourceArray1[25] = (byte) 95;
    sourceArray1[26] = (byte) 228;
    sourceArray1[32 /*0x20*/] = (byte) 89;
    sourceArray1[14] = (byte) 205;
    sourceArray1[29] = (byte) 47;
    sourceArray1[15] = (byte) 138;
    sourceArray1[6] = (byte) 243;
    sourceArray1[3] = (byte) 12;
    sourceArray1[33] = (byte) 226;
    sourceArray1[34] = (byte) 135;
    sourceArray1[35] = (byte) 146;
    sourceArray1[46] = (byte) 32 /*0x20*/;
    sourceArray1[23] = (byte) 66;
    sourceArray1[41] = (byte) 21;
    sourceArray1[39] = (byte) 1;
    sourceArray1[42] = (byte) 215;
    sourceArray1[16 /*0x10*/] = (byte) 58;
    sourceArray1[20] = (byte) 22;
    sourceArray1[37] = (byte) 88;
    sourceArray1[30] = (byte) 84;
    sourceArray1[19] = (byte) 246;
    sourceArray1[13] = (byte) 210;
    sourceArray1[28] = (byte) 162;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 162;
    sourceArray2[1] = (byte) 131;
    sourceArray2[2] = (byte) 15;
    sourceArray2[40] = (byte) 12;
    sourceArray2[17] = (byte) 191;
    sourceArray2[24] = (byte) 117;
    sourceArray2[6] = (byte) 6;
    sourceArray2[7] = (byte) 7;
    sourceArray2[8] = (byte) 246;
    sourceArray2[9] = (byte) 115;
    sourceArray2[10] = (byte) 229;
    sourceArray2[11] = (byte) 36;
    sourceArray2[12] = (byte) 31 /*0x1F*/;
    sourceArray2[47] = (byte) 226;
    sourceArray2[14] = (byte) 162;
    sourceArray2[3] = (byte) 46;
    sourceArray2[4] = (byte) 235;
    sourceArray2[44] = (byte) 183;
    sourceArray2[37] = (byte) 155;
    sourceArray2[23] = (byte) 94;
    sourceArray2[30] = (byte) 185;
    sourceArray2[45] = (byte) 181;
    sourceArray2[43] = (byte) 212;
    sourceArray2[25] = (byte) 89;
    sourceArray2[13] = (byte) 36;
    sourceArray2[29] = (byte) 92;
    sourceArray2[26] = (byte) 47;
    sourceArray2[42] = (byte) 201;
    sourceArray2[28] = (byte) 92;
    sourceArray2[19] = (byte) 30;
    sourceArray2[18] = (byte) 2;
    sourceArray2[15] = (byte) 95;
    sourceArray2[32 /*0x20*/] = (byte) 206;
    sourceArray2[33] = (byte) 173;
    sourceArray2[0] = (byte) 144 /*0x90*/;
    sourceArray2[35] = (byte) 75;
    sourceArray2[31 /*0x1F*/] = (byte) 56;
    sourceArray2[22] = (byte) 52;
    sourceArray2[38] = (byte) 215;
    sourceArray2[39] = (byte) 111;
    sourceArray2[16 /*0x10*/] = (byte) 253;
    sourceArray2[36] = (byte) 13;
    sourceArray2[20] = (byte) 114;
    sourceArray2[5] = (byte) 22;
    sourceArray2[21] = (byte) 16 /*0x10*/;
    sourceArray2[34] = (byte) 137;
    sourceArray2[46] = (byte) 198;
    sourceArray2[41] = (byte) 253;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13077(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 95;
    sourceArray1[37] = (byte) 99;
    sourceArray1[0] = (byte) 73;
    sourceArray1[36] = (byte) 114;
    sourceArray1[4] = (byte) 167;
    sourceArray1[26] = (byte) 247;
    sourceArray1[32 /*0x20*/] = (byte) 196;
    sourceArray1[22] = (byte) 167;
    sourceArray1[8] = (byte) 161;
    sourceArray1[9] = (byte) 191;
    sourceArray1[10] = (byte) 113;
    sourceArray1[11] = (byte) 30;
    sourceArray1[12] = (byte) 124;
    sourceArray1[13] = (byte) 114;
    sourceArray1[43] = (byte) 217;
    sourceArray1[14] = (byte) 153;
    sourceArray1[5] = (byte) 250;
    sourceArray1[17] = (byte) 251;
    sourceArray1[47] = (byte) 48 /*0x30*/;
    sourceArray1[19] = (byte) 66;
    sourceArray1[40] = (byte) 73;
    sourceArray1[16 /*0x10*/] = (byte) 55;
    sourceArray1[21] = (byte) 130;
    sourceArray1[23] = (byte) 231;
    sourceArray1[7] = (byte) 150;
    sourceArray1[25] = (byte) 123;
    sourceArray1[18] = (byte) 166;
    sourceArray1[27] = (byte) 250;
    sourceArray1[28] = (byte) 47;
    sourceArray1[29] = (byte) 28;
    sourceArray1[30] = (byte) 21;
    sourceArray1[46] = (byte) 201;
    sourceArray1[42] = (byte) 32 /*0x20*/;
    sourceArray1[33] = (byte) 154;
    sourceArray1[3] = (byte) 56;
    sourceArray1[35] = (byte) 173;
    sourceArray1[6] = (byte) 131;
    sourceArray1[15] = (byte) 70;
    sourceArray1[39] = (byte) 42;
    sourceArray1[1] = (byte) 157;
    sourceArray1[31 /*0x1F*/] = (byte) 226;
    sourceArray1[41] = (byte) 50;
    sourceArray1[24] = (byte) 229;
    sourceArray1[38] = (byte) 253;
    sourceArray1[44] = (byte) 119;
    sourceArray1[45] = (byte) 89;
    sourceArray1[34] = (byte) 250;
    sourceArray1[20] = (byte) 184;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 135,
      (byte) 168,
      (byte) 156,
      (byte) 187,
      (byte) 10,
      (byte) 148,
      (byte) 101,
      (byte) 166,
      (byte) 127 /*0x7F*/,
      (byte) 21,
      (byte) 158,
      (byte) 212,
      (byte) 200,
      (byte) 71,
      (byte) 91,
      (byte) 160 /*0xA0*/,
      (byte) 59,
      (byte) 141,
      (byte) 106,
      (byte) 124,
      (byte) 186,
      (byte) 67,
      (byte) 11,
      (byte) 6,
      (byte) 109,
      (byte) 10,
      (byte) 27,
      (byte) 184,
      (byte) 120,
      (byte) 89,
      (byte) 189,
      (byte) 33,
      (byte) 158,
      (byte) 46,
      (byte) 193,
      (byte) 94,
      (byte) 99,
      (byte) 129,
      (byte) 25,
      (byte) 154,
      (byte) 30,
      (byte) 222,
      (byte) 202,
      (byte) 132,
      (byte) 45,
      (byte) 145,
      (byte) 197
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13078(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 184,
      (byte) 227,
      (byte) 150,
      (byte) 161,
      (byte) 72,
      (byte) 183,
      (byte) 76,
      (byte) 241,
      (byte) 101,
      (byte) 40,
      (byte) 6,
      (byte) 199,
      (byte) 122,
      (byte) 60,
      (byte) 94,
      (byte) 161,
      (byte) 222,
      (byte) 218,
      (byte) 223,
      (byte) 219,
      (byte) 191,
      (byte) 68,
      (byte) 52,
      (byte) 216,
      (byte) 196,
      (byte) 44,
      (byte) 84,
      (byte) 56,
      (byte) 186,
      (byte) 198,
      (byte) 156,
      (byte) 135,
      (byte) 140,
      (byte) 202,
      (byte) 14,
      (byte) 70,
      (byte) 46,
      (byte) 4,
      (byte) 134,
      (byte) 248,
      (byte) 50,
      (byte) 49,
      (byte) 206,
      (byte) 143,
      (byte) 244,
      (byte) 72,
      (byte) 86
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 70,
      (byte) 183,
      (byte) 189,
      (byte) 46,
      (byte) 39,
      (byte) 10,
      (byte) 155,
      (byte) 131,
      (byte) 110,
      (byte) 238,
      (byte) 31 /*0x1F*/,
      (byte) 160 /*0xA0*/,
      (byte) 184,
      (byte) 99,
      (byte) 162,
      (byte) 251,
      (byte) 200,
      (byte) 72,
      (byte) 152,
      (byte) 206,
      (byte) 65,
      (byte) 246,
      (byte) 26,
      (byte) 151,
      (byte) 205,
      (byte) 80 /*0x50*/,
      (byte) 28,
      (byte) 103,
      (byte) 25,
      (byte) 85,
      (byte) 89,
      (byte) 68,
      (byte) 115,
      (byte) 112 /*0x70*/,
      (byte) 143,
      (byte) 177,
      (byte) 73,
      (byte) 126,
      (byte) 112 /*0x70*/,
      (byte) 30,
      (byte) 79,
      (byte) 187,
      (byte) 148,
      (byte) 238,
      (byte) 121,
      (byte) 198,
      (byte) 120,
      (byte) 32 /*0x20*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13079(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 9,
      (byte) 35,
      (byte) 40,
      (byte) 116,
      (byte) 175,
      (byte) 190,
      (byte) 221,
      (byte) 27,
      (byte) 98,
      (byte) 19,
      (byte) 55,
      (byte) 159,
      (byte) 139,
      (byte) 126,
      (byte) 58,
      (byte) 176 /*0xB0*/,
      (byte) 45,
      (byte) 23,
      (byte) 114,
      (byte) 217,
      (byte) 83,
      (byte) 231,
      (byte) 169,
      (byte) 141,
      (byte) 117,
      (byte) 164,
      (byte) 228,
      (byte) 124,
      (byte) 213,
      (byte) 104,
      (byte) 214,
      (byte) 143,
      (byte) 9,
      (byte) 70,
      (byte) 126,
      (byte) 105,
      (byte) 161,
      (byte) 153,
      (byte) 151,
      (byte) 112 /*0x70*/,
      (byte) 39,
      (byte) 107,
      (byte) 149,
      (byte) 30,
      (byte) 250,
      (byte) 46,
      (byte) 3,
      (byte) 117
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 75;
    sourceArray2[21] = (byte) 203;
    sourceArray2[6] = (byte) 195;
    sourceArray2[3] = (byte) 235;
    sourceArray2[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    sourceArray2[13] = (byte) 209;
    sourceArray2[1] = (byte) 40;
    sourceArray2[41] = (byte) 246;
    sourceArray2[20] = (byte) 30;
    sourceArray2[47] = (byte) 89;
    sourceArray2[10] = (byte) 240 /*0xF0*/;
    sourceArray2[40] = (byte) 118;
    sourceArray2[12] = (byte) 60;
    sourceArray2[4] = (byte) 223;
    sourceArray2[14] = (byte) 230;
    sourceArray2[15] = (byte) 38;
    sourceArray2[36] = (byte) 161;
    sourceArray2[42] = (byte) 86;
    sourceArray2[23] = (byte) 219;
    sourceArray2[19] = (byte) 248;
    sourceArray2[44] = (byte) 172;
    sourceArray2[31 /*0x1F*/] = (byte) 151;
    sourceArray2[22] = (byte) 217;
    sourceArray2[35] = (byte) 90;
    sourceArray2[24] = (byte) 110;
    sourceArray2[38] = (byte) 221;
    sourceArray2[33] = (byte) 58;
    sourceArray2[27] = (byte) 254;
    sourceArray2[2] = (byte) 180;
    sourceArray2[29] = (byte) 49;
    sourceArray2[30] = (byte) 149;
    sourceArray2[39] = (byte) 232;
    sourceArray2[5] = (byte) 137;
    sourceArray2[9] = (byte) 225;
    sourceArray2[34] = (byte) 235;
    sourceArray2[25] = (byte) 192 /*0xC0*/;
    sourceArray2[17] = (byte) 214;
    sourceArray2[37] = (byte) 236;
    sourceArray2[11] = (byte) 89;
    sourceArray2[26] = (byte) 82;
    sourceArray2[32 /*0x20*/] = (byte) 250;
    sourceArray2[0] = (byte) 153;
    sourceArray2[8] = (byte) 160 /*0xA0*/;
    sourceArray2[43] = (byte) 176 /*0xB0*/;
    sourceArray2[28] = (byte) 149;
    sourceArray2[7] = (byte) 106;
    sourceArray2[46] = (byte) 173;
    sourceArray2[18] = (byte) 37;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13080(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[41] = (byte) 246;
    sourceArray1[42] = (byte) 40;
    sourceArray1[34] = (byte) 226;
    sourceArray1[13] = (byte) 105;
    sourceArray1[2] = (byte) 59;
    sourceArray1[5] = (byte) 202;
    sourceArray1[36] = (byte) 51;
    sourceArray1[7] = (byte) 48 /*0x30*/;
    sourceArray1[1] = (byte) 117;
    sourceArray1[46] = (byte) 227;
    sourceArray1[39] = (byte) 193;
    sourceArray1[10] = (byte) 238;
    sourceArray1[12] = (byte) 183;
    sourceArray1[9] = (byte) 207;
    sourceArray1[8] = (byte) 27;
    sourceArray1[33] = (byte) 174;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[17] = (byte) 79;
    sourceArray1[18] = (byte) 177;
    sourceArray1[19] = (byte) 207;
    sourceArray1[14] = (byte) 107;
    sourceArray1[21] = (byte) 1;
    sourceArray1[22] = (byte) 162;
    sourceArray1[6] = (byte) 181;
    sourceArray1[3] = (byte) 150;
    sourceArray1[25] = (byte) 248;
    sourceArray1[26] = (byte) 243;
    sourceArray1[32 /*0x20*/] = (byte) 64 /*0x40*/;
    sourceArray1[11] = (byte) 7;
    sourceArray1[29] = (byte) 194;
    sourceArray1[30] = (byte) 182;
    sourceArray1[31 /*0x1F*/] = (byte) 168;
    sourceArray1[44] = (byte) 1;
    sourceArray1[27] = (byte) 108;
    sourceArray1[24] = (byte) 8;
    sourceArray1[35] = (byte) 249;
    sourceArray1[28] = (byte) 235;
    sourceArray1[15] = (byte) 241;
    sourceArray1[38] = (byte) 129;
    sourceArray1[0] = (byte) 190;
    sourceArray1[40] = (byte) 34;
    sourceArray1[4] = (byte) 41;
    sourceArray1[45] = byte.MaxValue;
    sourceArray1[43] = (byte) 141;
    sourceArray1[37] = (byte) 152;
    sourceArray1[20] = (byte) 111;
    sourceArray1[23] = (byte) 225;
    sourceArray1[47] = (byte) 35;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 80 /*0x50*/;
    sourceArray2[1] = (byte) 239;
    sourceArray2[20] = byte.MaxValue;
    sourceArray2[8] = (byte) 124;
    sourceArray2[4] = byte.MaxValue;
    sourceArray2[35] = (byte) 195;
    sourceArray2[6] = (byte) 169;
    sourceArray2[15] = (byte) 112 /*0x70*/;
    sourceArray2[14] = (byte) 3;
    sourceArray2[16 /*0x10*/] = (byte) 76;
    sourceArray2[10] = (byte) 189;
    sourceArray2[13] = (byte) 151;
    sourceArray2[12] = (byte) 125;
    sourceArray2[44] = (byte) 6;
    sourceArray2[18] = (byte) 229;
    sourceArray2[2] = (byte) 95;
    sourceArray2[26] = (byte) 23;
    sourceArray2[34] = (byte) 183;
    sourceArray2[5] = (byte) 12;
    sourceArray2[19] = (byte) 223;
    sourceArray2[0] = (byte) 62;
    sourceArray2[23] = (byte) 226;
    sourceArray2[22] = (byte) 67;
    sourceArray2[7] = (byte) 93;
    sourceArray2[29] = (byte) 92;
    sourceArray2[41] = (byte) 149;
    sourceArray2[25] = (byte) 118;
    sourceArray2[47] = (byte) 124;
    sourceArray2[28] = (byte) 134;
    sourceArray2[33] = (byte) 92;
    sourceArray2[30] = (byte) 33;
    sourceArray2[31 /*0x1F*/] = (byte) 140;
    sourceArray2[27] = (byte) 13;
    sourceArray2[37] = (byte) 189;
    sourceArray2[21] = (byte) 83;
    sourceArray2[3] = (byte) 151;
    sourceArray2[36] = (byte) 85;
    sourceArray2[39] = (byte) 150;
    sourceArray2[38] = (byte) 90;
    sourceArray2[24] = (byte) 63 /*0x3F*/;
    sourceArray2[40] = (byte) 140;
    sourceArray2[17] = (byte) 167;
    sourceArray2[42] = (byte) 11;
    sourceArray2[43] = (byte) 1;
    sourceArray2[9] = (byte) 228;
    sourceArray2[45] = (byte) 156;
    sourceArray2[46] = (byte) 3;
    sourceArray2[11] = (byte) 4;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13081(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 226,
      (byte) 208 /*0xD0*/,
      (byte) 129,
      (byte) 43,
      (byte) 198,
      (byte) 237,
      (byte) 49,
      (byte) 85,
      (byte) 58,
      (byte) 108,
      (byte) 50,
      (byte) 31 /*0x1F*/,
      (byte) 254,
      (byte) 58,
      (byte) 0,
      (byte) 232,
      (byte) 217,
      (byte) 27,
      (byte) 86,
      (byte) 183,
      (byte) 226,
      (byte) 211,
      (byte) 127 /*0x7F*/,
      (byte) 50,
      (byte) 131,
      (byte) 190,
      (byte) 149,
      (byte) 30,
      (byte) 177,
      (byte) 32 /*0x20*/,
      (byte) 226,
      (byte) 136,
      (byte) 239,
      (byte) 74,
      (byte) 35,
      (byte) 3,
      (byte) 71,
      (byte) 222,
      (byte) 219,
      (byte) 54,
      (byte) 158,
      (byte) 228,
      (byte) 249,
      (byte) 28,
      (byte) 202,
      (byte) 1,
      (byte) 240 /*0xF0*/,
      (byte) 253
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 226,
      (byte) 32 /*0x20*/,
      (byte) 34,
      (byte) 35,
      (byte) 226,
      (byte) 130,
      (byte) 95,
      (byte) 98,
      (byte) 148,
      (byte) 37,
      (byte) 137,
      (byte) 16 /*0x10*/,
      (byte) 134,
      (byte) 235,
      (byte) 29,
      (byte) 48 /*0x30*/,
      (byte) 83,
      (byte) 78,
      (byte) 127 /*0x7F*/,
      (byte) 58,
      (byte) 141,
      (byte) 6,
      (byte) 222,
      (byte) 245,
      (byte) 170,
      (byte) 222,
      (byte) 254,
      (byte) 173,
      (byte) 115,
      (byte) 230,
      (byte) 159,
      (byte) 11,
      (byte) 207,
      (byte) 103,
      (byte) 11,
      (byte) 218,
      (byte) 157,
      (byte) 51,
      (byte) 204,
      (byte) 6,
      (byte) 236,
      (byte) 70,
      (byte) 129,
      (byte) 28,
      (byte) 240 /*0xF0*/,
      (byte) 12,
      (byte) 36,
      (byte) 140
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13082(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 81,
      (byte) 15,
      (byte) 81,
      (byte) 78,
      (byte) 167,
      (byte) 7,
      (byte) 88,
      (byte) 18,
      (byte) 163,
      (byte) 25,
      (byte) 138,
      (byte) 170,
      (byte) 48 /*0x30*/,
      (byte) 179,
      (byte) 23,
      (byte) 252,
      (byte) 160 /*0xA0*/,
      (byte) 251,
      (byte) 63 /*0x3F*/,
      (byte) 32 /*0x20*/,
      (byte) 24,
      (byte) 253,
      (byte) 217,
      (byte) 98,
      (byte) 103,
      (byte) 129,
      (byte) 206,
      (byte) 151,
      (byte) 53,
      (byte) 197,
      (byte) 184,
      (byte) 147,
      (byte) 129,
      (byte) 34,
      (byte) 248,
      (byte) 19,
      (byte) 178,
      (byte) 248,
      (byte) 54,
      (byte) 211,
      (byte) 222,
      (byte) 139,
      (byte) 64 /*0x40*/,
      (byte) 224 /*0xE0*/,
      (byte) 146,
      (byte) 31 /*0x1F*/,
      (byte) 209
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 232,
      (byte) 212,
      (byte) 221,
      (byte) 177,
      (byte) 53,
      (byte) 125,
      (byte) 240 /*0xF0*/,
      (byte) 64 /*0x40*/,
      (byte) 140,
      (byte) 26,
      (byte) 3,
      (byte) 18,
      (byte) 65,
      (byte) 127 /*0x7F*/,
      (byte) 146,
      (byte) 30,
      (byte) 100,
      (byte) 53,
      (byte) 243,
      (byte) 63 /*0x3F*/,
      (byte) 22,
      (byte) 202,
      (byte) 183,
      (byte) 36,
      (byte) 72,
      (byte) 217,
      (byte) 223,
      (byte) 149,
      (byte) 45,
      (byte) 197,
      (byte) 5,
      (byte) 15,
      (byte) 1,
      (byte) 68,
      (byte) 154,
      (byte) 210,
      (byte) 58,
      (byte) 62,
      (byte) 139,
      (byte) 149,
      (byte) 189,
      (byte) 153,
      (byte) 20,
      (byte) 200,
      (byte) 165,
      (byte) 109,
      (byte) 38,
      (byte) 66
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[36];
    byte[] response2 = new byte[36];
    Array.Copy((Array) sc_13066.sspq, 26, (Array) numArray2, 0, 36);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13066.sspr, 26, (Array) numArray2, 0, 36);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_13083(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 161;
    sourceArray1[30] = (byte) 20;
    sourceArray1[8] = (byte) 98;
    sourceArray1[17] = (byte) 167;
    sourceArray1[41] = (byte) 222;
    sourceArray1[15] = (byte) 162;
    sourceArray1[21] = (byte) 152;
    sourceArray1[46] = (byte) 236;
    sourceArray1[33] = (byte) 159;
    sourceArray1[5] = (byte) 154;
    sourceArray1[19] = (byte) 163;
    sourceArray1[11] = (byte) 246;
    sourceArray1[10] = (byte) 155;
    sourceArray1[9] = (byte) 127 /*0x7F*/;
    sourceArray1[14] = (byte) 226;
    sourceArray1[35] = (byte) 39;
    sourceArray1[2] = (byte) 195;
    sourceArray1[12] = (byte) 195;
    sourceArray1[27] = (byte) 15;
    sourceArray1[37] = (byte) 151;
    sourceArray1[20] = (byte) 39;
    sourceArray1[39] = (byte) 210;
    sourceArray1[22] = (byte) 61;
    sourceArray1[23] = (byte) 29;
    sourceArray1[4] = (byte) 15;
    sourceArray1[0] = (byte) 164;
    sourceArray1[26] = (byte) 167;
    sourceArray1[18] = (byte) 162;
    sourceArray1[28] = (byte) 148;
    sourceArray1[42] = (byte) 89;
    sourceArray1[13] = (byte) 129;
    sourceArray1[3] = (byte) 16 /*0x10*/;
    sourceArray1[32 /*0x20*/] = (byte) 141;
    sourceArray1[29] = (byte) 98;
    sourceArray1[34] = (byte) 22;
    sourceArray1[45] = (byte) 80 /*0x50*/;
    sourceArray1[36] = (byte) 134;
    sourceArray1[1] = (byte) 220;
    sourceArray1[38] = (byte) 180;
    sourceArray1[31 /*0x1F*/] = (byte) 113;
    sourceArray1[24] = (byte) 159;
    sourceArray1[7] = (byte) 174;
    sourceArray1[25] = (byte) 119;
    sourceArray1[43] = (byte) 121;
    sourceArray1[44] = (byte) 53;
    sourceArray1[40] = (byte) 36;
    sourceArray1[6] = (byte) 29;
    sourceArray1[47] = (byte) 16 /*0x10*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 222,
      (byte) 19,
      (byte) 43,
      (byte) 233,
      (byte) 82,
      (byte) 98,
      (byte) 144 /*0x90*/,
      (byte) 58,
      (byte) 172,
      (byte) 231,
      (byte) 64 /*0x40*/,
      (byte) 183,
      (byte) 71,
      (byte) 220,
      (byte) 115,
      (byte) 82,
      (byte) 115,
      (byte) 252,
      (byte) 158,
      (byte) 89,
      (byte) 121,
      (byte) 159,
      (byte) 85,
      (byte) 208 /*0xD0*/,
      (byte) 216,
      (byte) 1,
      (byte) 147,
      (byte) 24,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 36,
      (byte) 205,
      (byte) 214,
      (byte) 228,
      (byte) 110,
      (byte) 120,
      (byte) 89,
      (byte) 24,
      (byte) 242,
      (byte) 246,
      (byte) 96 /*0x60*/,
      (byte) 250,
      (byte) 75,
      (byte) 102,
      (byte) 198,
      (byte) 170,
      (byte) 158,
      (byte) 4
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13084(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 199,
      (byte) 165,
      (byte) 19,
      (byte) 137,
      (byte) 207,
      (byte) 176 /*0xB0*/,
      (byte) 19,
      (byte) 120,
      (byte) 254,
      (byte) 205,
      (byte) 219,
      (byte) 189,
      (byte) 34,
      (byte) 13,
      (byte) 92,
      (byte) 205,
      (byte) 42,
      (byte) 118,
      (byte) 250,
      (byte) 155,
      (byte) 211,
      (byte) 204,
      (byte) 96 /*0x60*/,
      (byte) 51,
      (byte) 58,
      (byte) 227,
      (byte) 215,
      (byte) 240 /*0xF0*/,
      (byte) 103,
      (byte) 133,
      (byte) 229,
      (byte) 181,
      (byte) 221,
      (byte) 103,
      (byte) 195,
      (byte) 240 /*0xF0*/,
      (byte) 44,
      (byte) 101,
      (byte) 186,
      (byte) 191,
      (byte) 122,
      (byte) 29,
      (byte) 89,
      (byte) 60,
      (byte) 172,
      (byte) 36,
      (byte) 3,
      (byte) 13
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 23,
      (byte) 69,
      (byte) 27,
      (byte) 234,
      (byte) 61,
      (byte) 241,
      (byte) 175,
      (byte) 30,
      (byte) 118,
      (byte) 235,
      (byte) 247,
      (byte) 20,
      (byte) 114,
      (byte) 246,
      (byte) 234,
      (byte) 107,
      (byte) 199,
      (byte) 30,
      (byte) 123,
      (byte) 72,
      (byte) 171,
      (byte) 211,
      (byte) 34,
      (byte) 66,
      (byte) 127 /*0x7F*/,
      (byte) 20,
      (byte) 143,
      (byte) 180,
      (byte) 78,
      (byte) 136,
      (byte) 170,
      (byte) 191,
      (byte) 217,
      (byte) 214,
      (byte) 155,
      (byte) 69,
      (byte) 150,
      (byte) 201,
      (byte) 216,
      (byte) 111,
      (byte) 163,
      (byte) 31 /*0x1F*/,
      (byte) 248,
      (byte) 170,
      (byte) 42,
      (byte) 207,
      (byte) 9,
      (byte) 170
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13085(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 114;
    sourceArray1[36] = (byte) 199;
    sourceArray1[42] = (byte) 87;
    sourceArray1[3] = (byte) 69;
    sourceArray1[0] = (byte) 111;
    sourceArray1[5] = (byte) 78;
    sourceArray1[6] = (byte) 235;
    sourceArray1[27] = (byte) 223;
    sourceArray1[8] = (byte) 35;
    sourceArray1[4] = (byte) 224 /*0xE0*/;
    sourceArray1[35] = (byte) 83;
    sourceArray1[11] = (byte) 231;
    sourceArray1[34] = (byte) 109;
    sourceArray1[13] = (byte) 223;
    sourceArray1[18] = (byte) 197;
    sourceArray1[15] = (byte) 74;
    sourceArray1[39] = (byte) 23;
    sourceArray1[26] = (byte) 22;
    sourceArray1[20] = (byte) 58;
    sourceArray1[19] = (byte) 188;
    sourceArray1[45] = (byte) 85;
    sourceArray1[21] = (byte) 32 /*0x20*/;
    sourceArray1[22] = (byte) 182;
    sourceArray1[24] = (byte) 35;
    sourceArray1[12] = (byte) 37;
    sourceArray1[37] = (byte) 171;
    sourceArray1[10] = (byte) 121;
    sourceArray1[38] = (byte) 76;
    sourceArray1[28] = (byte) 201;
    sourceArray1[29] = (byte) 109;
    sourceArray1[9] = (byte) 16 /*0x10*/;
    sourceArray1[31 /*0x1F*/] = (byte) 236;
    sourceArray1[32 /*0x20*/] = (byte) 3;
    sourceArray1[43] = (byte) 12;
    sourceArray1[40] = (byte) 206;
    sourceArray1[2] = (byte) 251;
    sourceArray1[14] = (byte) 187;
    sourceArray1[33] = (byte) 161;
    sourceArray1[17] = (byte) 160 /*0xA0*/;
    sourceArray1[16 /*0x10*/] = (byte) 107;
    sourceArray1[25] = (byte) 134;
    sourceArray1[41] = (byte) 230;
    sourceArray1[1] = (byte) 20;
    sourceArray1[7] = (byte) 217;
    sourceArray1[44] = (byte) 224 /*0xE0*/;
    sourceArray1[30] = (byte) 29;
    sourceArray1[46] = (byte) 135;
    sourceArray1[47] = (byte) 68;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 77,
      (byte) 104,
      (byte) 217,
      (byte) 124,
      (byte) 131,
      (byte) 79,
      (byte) 109,
      (byte) 176 /*0xB0*/,
      (byte) 40,
      (byte) 76,
      (byte) 220,
      (byte) 187,
      (byte) 36,
      (byte) 76,
      (byte) 227,
      (byte) 205,
      (byte) 145,
      (byte) 156,
      (byte) 184,
      (byte) 88,
      (byte) 182,
      (byte) 246,
      (byte) 178,
      (byte) 85,
      (byte) 197,
      (byte) 239,
      (byte) 81,
      (byte) 3,
      (byte) 202,
      (byte) 79,
      (byte) 35,
      (byte) 152,
      (byte) 196,
      (byte) 235,
      (byte) 143,
      (byte) 29,
      (byte) 39,
      (byte) 43,
      (byte) 93,
      (byte) 127 /*0x7F*/,
      (byte) 154,
      (byte) 11,
      (byte) 128 /*0x80*/,
      (byte) 201,
      (byte) 120,
      (byte) 168,
      (byte) 124,
      (byte) 129
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13086(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 3,
      (byte) 210,
      (byte) 225,
      (byte) 63 /*0x3F*/,
      (byte) 88,
      (byte) 86,
      (byte) 37,
      (byte) 56,
      (byte) 133,
      (byte) 133,
      (byte) 180,
      (byte) 247,
      (byte) 82,
      (byte) 91,
      (byte) 101,
      (byte) 116,
      (byte) 9,
      (byte) 96 /*0x60*/,
      (byte) 127 /*0x7F*/,
      (byte) 117,
      (byte) 7,
      (byte) 188,
      (byte) 83,
      (byte) 114,
      (byte) 69,
      (byte) 53,
      (byte) 97,
      (byte) 206,
      (byte) 42,
      (byte) 125,
      (byte) 39,
      (byte) 143,
      (byte) 63 /*0x3F*/,
      (byte) 94,
      (byte) 251,
      (byte) 0,
      (byte) 181,
      (byte) 199,
      (byte) 253,
      (byte) 29,
      (byte) 242,
      (byte) 164,
      (byte) 66,
      (byte) 153,
      (byte) 65,
      (byte) 70,
      (byte) 55,
      (byte) 150
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[22] = (byte) 0;
    sourceArray2[30] = (byte) 186;
    sourceArray2[2] = (byte) 249;
    sourceArray2[3] = (byte) 221;
    sourceArray2[4] = (byte) 71;
    sourceArray2[29] = (byte) 247;
    sourceArray2[6] = (byte) 149;
    sourceArray2[7] = (byte) 27;
    sourceArray2[23] = (byte) 102;
    sourceArray2[46] = (byte) 51;
    sourceArray2[0] = (byte) 246;
    sourceArray2[11] = (byte) 223;
    sourceArray2[43] = (byte) 219;
    sourceArray2[13] = (byte) 84;
    sourceArray2[14] = (byte) 8;
    sourceArray2[15] = (byte) 102;
    sourceArray2[16 /*0x10*/] = (byte) 133;
    sourceArray2[17] = (byte) 97;
    sourceArray2[40] = (byte) 12;
    sourceArray2[19] = (byte) 94;
    sourceArray2[21] = (byte) 203;
    sourceArray2[41] = (byte) 227;
    sourceArray2[12] = (byte) 194;
    sourceArray2[18] = (byte) 22;
    sourceArray2[38] = (byte) 254;
    sourceArray2[37] = (byte) 108;
    sourceArray2[26] = (byte) 199;
    sourceArray2[45] = (byte) 145;
    sourceArray2[28] = (byte) 211;
    sourceArray2[9] = (byte) 7;
    sourceArray2[33] = (byte) 216;
    sourceArray2[1] = (byte) 168;
    sourceArray2[32 /*0x20*/] = (byte) 136;
    sourceArray2[8] = (byte) 124;
    sourceArray2[34] = (byte) 23;
    sourceArray2[47] = (byte) 92;
    sourceArray2[35] = (byte) 186;
    sourceArray2[10] = (byte) 19;
    sourceArray2[25] = (byte) 169;
    sourceArray2[39] = (byte) 82;
    sourceArray2[36] = (byte) 33;
    sourceArray2[27] = (byte) 147;
    sourceArray2[42] = (byte) 230;
    sourceArray2[31 /*0x1F*/] = (byte) 71;
    sourceArray2[44] = (byte) 63 /*0x3F*/;
    sourceArray2[5] = (byte) 206;
    sourceArray2[20] = (byte) 211;
    sourceArray2[24] = (byte) 111;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[51];
    byte[] response2 = new byte[51];
    Array.Copy((Array) sc_13066.sspq, 62, (Array) numArray2, 0, 51);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13066.sspr, 62, (Array) numArray2, 0, 51);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_13087(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 105;
    sourceArray1[45] = (byte) 181;
    sourceArray1[18] = (byte) 195;
    sourceArray1[21] = (byte) 110;
    sourceArray1[4] = (byte) 139;
    sourceArray1[5] = (byte) 49;
    sourceArray1[6] = (byte) 25;
    sourceArray1[7] = (byte) 22;
    sourceArray1[38] = (byte) 8;
    sourceArray1[0] = (byte) 195;
    sourceArray1[31 /*0x1F*/] = (byte) 78;
    sourceArray1[25] = (byte) 195;
    sourceArray1[29] = (byte) 211;
    sourceArray1[13] = (byte) 70;
    sourceArray1[14] = (byte) 41;
    sourceArray1[15] = (byte) 139;
    sourceArray1[34] = (byte) 187;
    sourceArray1[17] = (byte) 149;
    sourceArray1[32 /*0x20*/] = (byte) 102;
    sourceArray1[9] = (byte) 0;
    sourceArray1[20] = (byte) 49;
    sourceArray1[41] = (byte) 15;
    sourceArray1[3] = (byte) 68;
    sourceArray1[23] = (byte) 80 /*0x50*/;
    sourceArray1[24] = (byte) 14;
    sourceArray1[40] = (byte) 50;
    sourceArray1[46] = (byte) 90;
    sourceArray1[27] = (byte) 109;
    sourceArray1[28] = (byte) 163;
    sourceArray1[33] = (byte) 92;
    sourceArray1[19] = (byte) 230;
    sourceArray1[11] = (byte) 58;
    sourceArray1[37] = (byte) 118;
    sourceArray1[1] = (byte) 238;
    sourceArray1[39] = (byte) 13;
    sourceArray1[35] = (byte) 38;
    sourceArray1[36] = (byte) 54;
    sourceArray1[30] = (byte) 57;
    sourceArray1[10] = (byte) 115;
    sourceArray1[2] = (byte) 56;
    sourceArray1[12] = (byte) 141;
    sourceArray1[8] = (byte) 84;
    sourceArray1[42] = (byte) 181;
    sourceArray1[44] = (byte) 146;
    sourceArray1[26] = (byte) 39;
    sourceArray1[22] = (byte) 114;
    sourceArray1[43] = (byte) 188;
    sourceArray1[47] = (byte) 118;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[25] = (byte) 52;
    sourceArray2[1] = (byte) 42;
    sourceArray2[46] = (byte) 88;
    sourceArray2[40] = (byte) 220;
    sourceArray2[27] = (byte) 195;
    sourceArray2[12] = (byte) 2;
    sourceArray2[6] = (byte) 226;
    sourceArray2[7] = (byte) 124;
    sourceArray2[2] = (byte) 17;
    sourceArray2[8] = (byte) 53;
    sourceArray2[13] = (byte) 54;
    sourceArray2[26] = (byte) 96 /*0x60*/;
    sourceArray2[0] = (byte) 51;
    sourceArray2[9] = (byte) 82;
    sourceArray2[4] = (byte) 102;
    sourceArray2[15] = (byte) 147;
    sourceArray2[16 /*0x10*/] = (byte) 10;
    sourceArray2[45] = (byte) 213;
    sourceArray2[41] = (byte) 76;
    sourceArray2[19] = (byte) 94;
    sourceArray2[20] = (byte) 236;
    sourceArray2[29] = (byte) 36;
    sourceArray2[31 /*0x1F*/] = (byte) 22;
    sourceArray2[23] = (byte) 59;
    sourceArray2[18] = (byte) 137;
    sourceArray2[36] = (byte) 128 /*0x80*/;
    sourceArray2[24] = (byte) 21;
    sourceArray2[3] = (byte) 123;
    sourceArray2[28] = (byte) 30;
    sourceArray2[38] = (byte) 221;
    sourceArray2[43] = (byte) 116;
    sourceArray2[17] = (byte) 98;
    sourceArray2[5] = (byte) 78;
    sourceArray2[33] = (byte) 235;
    sourceArray2[39] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 237;
    sourceArray2[22] = (byte) 56;
    sourceArray2[37] = (byte) 204;
    sourceArray2[32 /*0x20*/] = (byte) 156;
    sourceArray2[34] = (byte) 147;
    sourceArray2[30] = (byte) 83;
    sourceArray2[35] = (byte) 224 /*0xE0*/;
    sourceArray2[42] = (byte) 241;
    sourceArray2[11] = (byte) 248;
    sourceArray2[44] = (byte) 246;
    sourceArray2[21] = (byte) 154;
    sourceArray2[10] = (byte) 202;
    sourceArray2[47] = (byte) 13;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13088(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 169,
      (byte) 102,
      (byte) 134,
      (byte) 57,
      (byte) 254,
      (byte) 191,
      (byte) 245,
      (byte) 163,
      (byte) 199,
      (byte) 133,
      (byte) 233,
      (byte) 165,
      (byte) 145,
      (byte) 109,
      (byte) 155,
      (byte) 9,
      (byte) 22,
      (byte) 5,
      (byte) 218,
      (byte) 102,
      (byte) 30,
      (byte) 148,
      (byte) 112 /*0x70*/,
      (byte) 169,
      (byte) 203,
      (byte) 4,
      (byte) 84,
      (byte) 206,
      (byte) 105,
      (byte) 89,
      (byte) 116,
      (byte) 227,
      (byte) 80 /*0x50*/,
      (byte) 105,
      (byte) 9,
      (byte) 199,
      byte.MaxValue,
      (byte) 75,
      (byte) 193,
      (byte) 145,
      (byte) 129,
      (byte) 156,
      (byte) 253,
      (byte) 41,
      (byte) 130,
      (byte) 137,
      (byte) 97,
      (byte) 235
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 61;
    sourceArray2[29] = byte.MaxValue;
    sourceArray2[2] = (byte) 211;
    sourceArray2[3] = (byte) 108;
    sourceArray2[4] = (byte) 118;
    sourceArray2[41] = (byte) 231;
    sourceArray2[6] = (byte) 68;
    sourceArray2[7] = (byte) 210;
    sourceArray2[8] = (byte) 67;
    sourceArray2[47] = (byte) 72;
    sourceArray2[19] = (byte) 2;
    sourceArray2[0] = (byte) 109;
    sourceArray2[14] = (byte) 45;
    sourceArray2[38] = (byte) 166;
    sourceArray2[37] = (byte) 79;
    sourceArray2[45] = (byte) 162;
    sourceArray2[16 /*0x10*/] = (byte) 138;
    sourceArray2[28] = (byte) 113;
    sourceArray2[15] = (byte) 184;
    sourceArray2[43] = (byte) 76;
    sourceArray2[20] = (byte) 219;
    sourceArray2[33] = (byte) 125;
    sourceArray2[22] = (byte) 217;
    sourceArray2[40] = (byte) 70;
    sourceArray2[24] = (byte) 175;
    sourceArray2[36] = (byte) 202;
    sourceArray2[26] = (byte) 71;
    sourceArray2[27] = (byte) 62;
    sourceArray2[32 /*0x20*/] = (byte) 165;
    sourceArray2[17] = (byte) 62;
    sourceArray2[30] = (byte) 140;
    sourceArray2[12] = (byte) 29;
    sourceArray2[1] = (byte) 70;
    sourceArray2[21] = (byte) 142;
    sourceArray2[34] = (byte) 160 /*0xA0*/;
    sourceArray2[35] = (byte) 54;
    sourceArray2[10] = (byte) 73;
    sourceArray2[42] = (byte) 216;
    sourceArray2[23] = (byte) 3;
    sourceArray2[39] = (byte) 44;
    sourceArray2[25] = (byte) 13;
    sourceArray2[13] = (byte) 218;
    sourceArray2[46] = (byte) 120;
    sourceArray2[9] = (byte) 66;
    sourceArray2[31 /*0x1F*/] = (byte) 132;
    sourceArray2[11] = (byte) 164;
    sourceArray2[18] = (byte) 162;
    sourceArray2[5] = (byte) 192 /*0xC0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13089(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 57,
      (byte) 135,
      (byte) 175,
      (byte) 8,
      (byte) 231,
      (byte) 155,
      (byte) 0,
      (byte) 23,
      (byte) 64 /*0x40*/,
      (byte) 127 /*0x7F*/,
      (byte) 174,
      (byte) 182,
      (byte) 43,
      (byte) 86,
      (byte) 2,
      (byte) 220,
      (byte) 226,
      (byte) 199,
      (byte) 124,
      (byte) 107,
      byte.MaxValue,
      (byte) 129,
      (byte) 111,
      (byte) 1,
      (byte) 231,
      (byte) 237,
      (byte) 124,
      (byte) 34,
      (byte) 138,
      (byte) 32 /*0x20*/,
      (byte) 6,
      (byte) 47,
      (byte) 169,
      (byte) 14,
      (byte) 106,
      (byte) 225,
      (byte) 155,
      (byte) 78,
      (byte) 190,
      (byte) 250,
      (byte) 154,
      (byte) 61,
      (byte) 101,
      (byte) 116,
      (byte) 35,
      (byte) 107,
      (byte) 178,
      (byte) 112 /*0x70*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 206,
      (byte) 224 /*0xE0*/,
      (byte) 106,
      (byte) 103,
      (byte) 4,
      (byte) 0,
      (byte) 115,
      (byte) 134,
      (byte) 168,
      (byte) 230,
      (byte) 52,
      (byte) 175,
      (byte) 172,
      (byte) 216,
      (byte) 47,
      (byte) 76,
      (byte) 49,
      (byte) 66,
      (byte) 211,
      (byte) 153,
      (byte) 105,
      byte.MaxValue,
      (byte) 182,
      (byte) 123,
      (byte) 8,
      (byte) 43,
      (byte) 90,
      (byte) 142,
      (byte) 2,
      (byte) 223,
      (byte) 226,
      (byte) 126,
      (byte) 117,
      (byte) 49,
      (byte) 240 /*0xF0*/,
      (byte) 59,
      (byte) 130,
      (byte) 141,
      (byte) 250,
      (byte) 204,
      (byte) 65,
      (byte) 179,
      (byte) 42,
      (byte) 27,
      (byte) 225,
      (byte) 114,
      (byte) 33,
      (byte) 21
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13066.sspq, 113, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13066.sspr, 113, (Array) numArray2, 0, 42);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static string ssp_appserver_13090()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[234];
      byte[] numArray2 = new byte[55]
      {
        (byte) 188,
        (byte) 176 /*0xB0*/,
        (byte) 69,
        (byte) 252,
        (byte) 164,
        (byte) 93,
        (byte) 150,
        (byte) 131,
        (byte) 2,
        (byte) 61,
        (byte) 235,
        (byte) 192 /*0xC0*/,
        (byte) 25,
        (byte) 65,
        (byte) 114,
        (byte) 155,
        (byte) 29,
        (byte) 207,
        (byte) 164,
        (byte) 191,
        (byte) 18,
        (byte) 221,
        (byte) 129,
        (byte) 127 /*0x7F*/,
        (byte) 58,
        (byte) 236,
        (byte) 118,
        (byte) 67,
        (byte) 57,
        (byte) 88,
        (byte) 83,
        (byte) 102,
        (byte) 67,
        (byte) 166,
        (byte) 19,
        (byte) 17,
        (byte) 214,
        (byte) 254,
        (byte) 58,
        (byte) 31 /*0x1F*/,
        (byte) 164,
        (byte) 136,
        (byte) 253,
        (byte) 206,
        (byte) 180,
        (byte) 125,
        (byte) 121,
        (byte) 86,
        (byte) 102,
        (byte) 24,
        (byte) 171,
        (byte) 29,
        (byte) 137,
        (byte) 5,
        (byte) 201
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 10,
        (byte) 195,
        (byte) 107,
        (byte) 162,
        (byte) 17,
        (byte) 212,
        (byte) 16 /*0x10*/,
        (byte) 57,
        (byte) 83,
        (byte) 136,
        (byte) 99,
        (byte) 77,
        (byte) 188,
        (byte) 237,
        (byte) 46,
        (byte) 187,
        (byte) 17,
        (byte) 38,
        (byte) 112 /*0x70*/,
        (byte) 15,
        (byte) 168,
        (byte) 123,
        (byte) 114,
        (byte) 178,
        (byte) 77,
        (byte) 82,
        (byte) 205,
        byte.MaxValue,
        (byte) 245,
        (byte) 163,
        (byte) 104,
        (byte) 240 /*0xF0*/,
        (byte) 89,
        (byte) 12,
        (byte) 94,
        (byte) 184,
        (byte) 235,
        (byte) 224 /*0xE0*/,
        (byte) 146,
        (byte) 75,
        (byte) 145,
        (byte) 52,
        (byte) 237,
        (byte) 232,
        (byte) 1,
        (byte) 27,
        (byte) 236,
        (byte) 75,
        byte.MaxValue,
        (byte) 105,
        (byte) 232,
        (byte) 4,
        (byte) 63 /*0x3F*/,
        (byte) 62,
        (byte) 23
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 198,
        (byte) 193,
        (byte) 183,
        (byte) 231,
        (byte) 82,
        (byte) 74,
        (byte) 169,
        (byte) 233,
        (byte) 132,
        (byte) 124,
        (byte) 115,
        (byte) 45,
        (byte) 97,
        (byte) 196,
        (byte) 31 /*0x1F*/,
        (byte) 149,
        (byte) 132,
        (byte) 26,
        (byte) 137,
        (byte) 224 /*0xE0*/,
        (byte) 178,
        (byte) 202,
        (byte) 189,
        (byte) 114,
        (byte) 31 /*0x1F*/,
        (byte) 220,
        (byte) 33,
        (byte) 172,
        (byte) 53,
        (byte) 221,
        (byte) 62,
        (byte) 220,
        (byte) 52,
        (byte) 226,
        (byte) 15,
        (byte) 60,
        (byte) 130,
        (byte) 55,
        (byte) 82,
        (byte) 62,
        (byte) 83,
        (byte) 228,
        (byte) 47,
        (byte) 1,
        (byte) 5,
        (byte) 40,
        (byte) 5,
        (byte) 83,
        (byte) 140,
        (byte) 136,
        (byte) 4,
        (byte) 234,
        (byte) 64 /*0x40*/,
        (byte) 254,
        (byte) 124
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 161,
        (byte) 62,
        (byte) 35,
        (byte) 216,
        (byte) 50,
        (byte) 104,
        (byte) 4,
        (byte) 146,
        (byte) 102,
        (byte) 56,
        (byte) 170,
        (byte) 41,
        (byte) 126,
        (byte) 78,
        (byte) 250,
        (byte) 150,
        (byte) 96 /*0x60*/,
        (byte) 189,
        (byte) 92,
        (byte) 207,
        (byte) 132,
        (byte) 125,
        (byte) 168,
        (byte) 49,
        (byte) 194,
        (byte) 204,
        (byte) 225,
        (byte) 35,
        (byte) 105,
        (byte) 194,
        (byte) 241,
        (byte) 75,
        (byte) 245,
        (byte) 111,
        (byte) 156,
        (byte) 93,
        (byte) 47,
        (byte) 127 /*0x7F*/,
        (byte) 45,
        (byte) 175,
        (byte) 240 /*0xF0*/,
        (byte) 148,
        (byte) 144 /*0x90*/,
        (byte) 105,
        (byte) 106,
        (byte) 149,
        (byte) 116,
        byte.MaxValue,
        (byte) 75,
        (byte) 151,
        (byte) 103,
        (byte) 24,
        (byte) 62,
        (byte) 15,
        (byte) 181
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 69,
        (byte) 199,
        (byte) 118,
        (byte) 40,
        (byte) 253,
        (byte) 179,
        (byte) 167,
        (byte) 61,
        (byte) 150,
        (byte) 248,
        (byte) 126,
        (byte) 181,
        (byte) 226,
        (byte) 19,
        (byte) 76,
        (byte) 200,
        (byte) 75,
        (byte) 141,
        (byte) 206,
        (byte) 154,
        (byte) 188,
        (byte) 108,
        (byte) 247,
        (byte) 215,
        (byte) 206,
        (byte) 32 /*0x20*/,
        (byte) 173,
        (byte) 216,
        (byte) 84,
        (byte) 156,
        (byte) 54,
        (byte) 102,
        (byte) 18,
        (byte) 56,
        (byte) 61,
        (byte) 112 /*0x70*/,
        (byte) 219,
        (byte) 21,
        (byte) 86,
        (byte) 48 /*0x30*/,
        (byte) 136,
        (byte) 195,
        (byte) 111,
        (byte) 48 /*0x30*/,
        (byte) 134,
        (byte) 157,
        (byte) 170,
        (byte) 116,
        (byte) 183,
        (byte) 33,
        (byte) 244,
        (byte) 113,
        (byte) 174,
        (byte) 98,
        (byte) 71
      };
      byte[] numArray7 = new byte[55];
      numArray7[2] = (byte) 118;
      numArray7[42] = (byte) 5;
      numArray7[35] = (byte) 207;
      numArray7[48 /*0x30*/] = (byte) 35;
      numArray7[0] = (byte) 95;
      numArray7[33] = (byte) 225;
      numArray7[28] = (byte) 170;
      numArray7[38] = (byte) 215;
      numArray7[40] = (byte) 76;
      numArray7[9] = (byte) 173;
      numArray7[34] = (byte) 241;
      numArray7[8] = (byte) 220;
      numArray7[12] = (byte) 103;
      numArray7[13] = (byte) 51;
      numArray7[53] = (byte) 206;
      numArray7[15] = (byte) 213;
      numArray7[16 /*0x10*/] = (byte) 62;
      numArray7[5] = (byte) 29;
      numArray7[43] = (byte) 105;
      numArray7[19] = (byte) 188;
      numArray7[20] = (byte) 211;
      numArray7[21] = (byte) 241;
      numArray7[22] = (byte) 158;
      numArray7[23] = (byte) 189;
      numArray7[24] = (byte) 169;
      numArray7[52] = (byte) 70;
      numArray7[41] = (byte) 60;
      numArray7[27] = (byte) 21;
      numArray7[25] = (byte) 110;
      numArray7[29] = (byte) 15;
      numArray7[4] = (byte) 101;
      numArray7[31 /*0x1F*/] = (byte) 172;
      numArray7[32 /*0x20*/] = (byte) 154;
      numArray7[3] = (byte) 93;
      numArray7[26] = (byte) 16 /*0x10*/;
      numArray7[10] = (byte) 114;
      numArray7[37] = (byte) 22;
      numArray7[47] = (byte) 129;
      numArray7[6] = (byte) 135;
      numArray7[39] = (byte) 128 /*0x80*/;
      numArray7[36] = (byte) 31 /*0x1F*/;
      numArray7[1] = (byte) 35;
      numArray7[30] = (byte) 158;
      numArray7[14] = (byte) 235;
      numArray7[44] = (byte) 136;
      numArray7[45] = (byte) 237;
      numArray7[18] = (byte) 141;
      numArray7[46] = (byte) 145;
      numArray7[49] = (byte) 200;
      numArray7[17] = (byte) 1;
      numArray7[50] = (byte) 95;
      numArray7[51] = (byte) 246;
      numArray7[11] = (byte) 48 /*0x30*/;
      numArray7[7] = (byte) 84;
      numArray7[54] = (byte) 38;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 78,
        (byte) 41,
        (byte) 102,
        (byte) 204,
        (byte) 173,
        (byte) 183,
        (byte) 81,
        (byte) 253,
        (byte) 96 /*0x60*/,
        (byte) 241,
        (byte) 184,
        (byte) 113,
        (byte) 61,
        (byte) 176 /*0xB0*/,
        (byte) 122,
        (byte) 189,
        (byte) 114,
        (byte) 87,
        (byte) 31 /*0x1F*/,
        (byte) 135,
        (byte) 89,
        (byte) 226,
        (byte) 53,
        (byte) 117,
        (byte) 154,
        (byte) 70,
        (byte) 252,
        (byte) 233,
        (byte) 138,
        (byte) 166,
        (byte) 96 /*0x60*/,
        (byte) 162,
        (byte) 137,
        (byte) 149,
        (byte) 238,
        (byte) 145,
        (byte) 18,
        (byte) 94,
        (byte) 13,
        (byte) 34,
        (byte) 35,
        (byte) 112 /*0x70*/,
        (byte) 13,
        (byte) 9,
        (byte) 72,
        (byte) 45,
        (byte) 157,
        (byte) 133,
        (byte) 67,
        (byte) 125,
        (byte) 158,
        (byte) 249,
        (byte) 47,
        (byte) 82,
        (byte) 176 /*0xB0*/
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 236,
        (byte) 74,
        (byte) 15,
        (byte) 72,
        (byte) 83,
        (byte) 36,
        (byte) 242,
        (byte) 174,
        (byte) 185,
        (byte) 212,
        (byte) 87,
        (byte) 212,
        (byte) 48 /*0x30*/,
        (byte) 177,
        (byte) 166,
        (byte) 20,
        (byte) 222,
        (byte) 67,
        (byte) 10,
        (byte) 227,
        (byte) 170,
        (byte) 234,
        (byte) 177,
        (byte) 154,
        (byte) 45,
        (byte) 151,
        (byte) 71,
        (byte) 70,
        (byte) 171,
        (byte) 190,
        (byte) 69,
        (byte) 234,
        (byte) 44,
        (byte) 85,
        (byte) 59,
        (byte) 69,
        (byte) 120,
        (byte) 145,
        (byte) 18,
        (byte) 104,
        (byte) 195,
        (byte) 8,
        (byte) 59,
        (byte) 33,
        (byte) 37,
        (byte) 7,
        (byte) 50,
        (byte) 1,
        (byte) 2,
        (byte) 184,
        (byte) 235,
        (byte) 163,
        (byte) 155,
        (byte) 79,
        (byte) 0
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[14];
      numArray10[3] = (byte) 186;
      numArray10[6] = (byte) 185;
      numArray10[0] = (byte) 241;
      numArray10[10] = (byte) 167;
      numArray10[4] = (byte) 104;
      numArray10[12] = (byte) 233;
      numArray10[5] = (byte) 115;
      numArray10[2] = (byte) 103;
      numArray10[8] = (byte) 22;
      numArray10[9] = (byte) 231;
      numArray10[7] = (byte) 145;
      numArray10[11] = (byte) 194;
      numArray10[13] = (byte) 170;
      numArray10[1] = (byte) 50;
      byte[] numArray11 = new byte[14];
      numArray11[11] = (byte) 208 /*0xD0*/;
      numArray11[7] = (byte) 205;
      numArray11[1] = (byte) 220;
      numArray11[2] = (byte) 240 /*0xF0*/;
      numArray11[4] = (byte) 90;
      numArray11[5] = (byte) 246;
      numArray11[6] = (byte) 35;
      numArray11[0] = (byte) 207;
      numArray11[12] = (byte) 125;
      numArray11[9] = (byte) 81;
      numArray11[10] = (byte) 118;
      numArray11[8] = (byte) 173;
      numArray11[3] = (byte) 7;
      numArray11[13] = (byte) 222;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[234];
    byte[] numArray13 = new byte[55];
    numArray13[48 /*0x30*/] = (byte) 248;
    numArray13[1] = (byte) 90;
    numArray13[14] = (byte) 194;
    numArray13[3] = (byte) 139;
    numArray13[43] = (byte) 212;
    numArray13[26] = (byte) 148;
    numArray13[6] = (byte) 238;
    numArray13[10] = (byte) 197;
    numArray13[36] = (byte) 57;
    numArray13[30] = (byte) 179;
    numArray13[51] = (byte) 77;
    numArray13[11] = (byte) 197;
    numArray13[4] = (byte) 175;
    numArray13[13] = (byte) 66;
    numArray13[41] = (byte) 29;
    numArray13[38] = (byte) 183;
    numArray13[16 /*0x10*/] = (byte) 177;
    numArray13[21] = (byte) 64 /*0x40*/;
    numArray13[18] = (byte) 163;
    numArray13[5] = (byte) 50;
    numArray13[27] = (byte) 168;
    numArray13[35] = (byte) 74;
    numArray13[22] = (byte) 76;
    numArray13[23] = (byte) 137;
    numArray13[15] = (byte) 102;
    numArray13[25] = (byte) 164;
    numArray13[19] = (byte) 176 /*0xB0*/;
    numArray13[50] = (byte) 161;
    numArray13[28] = (byte) 235;
    numArray13[29] = (byte) 129;
    numArray13[0] = (byte) 195;
    numArray13[31 /*0x1F*/] = (byte) 250;
    numArray13[7] = (byte) 199;
    numArray13[37] = (byte) 125;
    numArray13[33] = (byte) 177;
    numArray13[8] = (byte) 150;
    numArray13[54] = (byte) 52;
    numArray13[12] = (byte) 191;
    numArray13[20] = (byte) 194;
    numArray13[39] = (byte) 64 /*0x40*/;
    numArray13[40] = (byte) 71;
    numArray13[34] = (byte) 180;
    numArray13[42] = (byte) 235;
    numArray13[17] = (byte) 42;
    numArray13[53] = (byte) 149;
    numArray13[46] = (byte) 230;
    numArray13[32 /*0x20*/] = (byte) 39;
    numArray13[47] = (byte) 37;
    numArray13[45] = (byte) 102;
    numArray13[49] = (byte) 150;
    numArray13[2] = (byte) 211;
    numArray13[9] = (byte) 200;
    numArray13[24] = (byte) 231;
    numArray13[44] = (byte) 91;
    numArray13[52] = (byte) 143;
    byte[] numArray14 = new byte[55]
    {
      (byte) 124,
      (byte) 83,
      (byte) 71,
      (byte) 182,
      (byte) 106,
      (byte) 179,
      (byte) 241,
      (byte) 113,
      (byte) 73,
      (byte) 52,
      (byte) 44,
      (byte) 25,
      (byte) 11,
      (byte) 205,
      (byte) 26,
      (byte) 192 /*0xC0*/,
      (byte) 68,
      (byte) 185,
      (byte) 46,
      (byte) 184,
      (byte) 158,
      (byte) 52,
      (byte) 206,
      (byte) 169,
      (byte) 237,
      (byte) 219,
      (byte) 172,
      (byte) 213,
      (byte) 11,
      (byte) 118,
      (byte) 208 /*0xD0*/,
      (byte) 190,
      (byte) 29,
      (byte) 69,
      (byte) 102,
      (byte) 243,
      (byte) 179,
      (byte) 21,
      (byte) 19,
      (byte) 219,
      (byte) 223,
      (byte) 80 /*0x50*/,
      (byte) 241,
      (byte) 228,
      (byte) 167,
      (byte) 79,
      (byte) 228,
      (byte) 0,
      (byte) 145,
      (byte) 65,
      (byte) 61,
      (byte) 13,
      (byte) 209,
      (byte) 25,
      (byte) 245
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 27,
      (byte) 33,
      (byte) 77,
      (byte) 48 /*0x30*/,
      (byte) 150,
      (byte) 135,
      (byte) 53,
      (byte) 174,
      (byte) 94,
      (byte) 12,
      (byte) 144 /*0x90*/,
      (byte) 91,
      (byte) 59,
      (byte) 62,
      (byte) 90,
      (byte) 197,
      (byte) 193,
      (byte) 103,
      (byte) 99,
      (byte) 127 /*0x7F*/,
      (byte) 183,
      (byte) 156,
      (byte) 185,
      (byte) 228,
      (byte) 214,
      (byte) 47,
      (byte) 236,
      (byte) 114,
      (byte) 139,
      (byte) 63 /*0x3F*/,
      (byte) 101,
      (byte) 237,
      (byte) 3,
      (byte) 164,
      (byte) 211,
      (byte) 184,
      (byte) 95,
      (byte) 113,
      (byte) 106,
      (byte) 186,
      (byte) 160 /*0xA0*/,
      (byte) 17,
      (byte) 50,
      (byte) 187,
      (byte) 72,
      (byte) 60,
      (byte) 68,
      (byte) 42,
      (byte) 19,
      (byte) 21,
      (byte) 106,
      (byte) 197,
      (byte) 44,
      (byte) 174,
      (byte) 223
    };
    byte[] numArray16 = new byte[55];
    numArray16[43] = (byte) 85;
    numArray16[42] = (byte) 47;
    numArray16[2] = (byte) 222;
    numArray16[25] = (byte) 35;
    numArray16[1] = (byte) 199;
    numArray16[34] = (byte) 160 /*0xA0*/;
    numArray16[6] = (byte) 120;
    numArray16[31 /*0x1F*/] = (byte) 92;
    numArray16[30] = (byte) 90;
    numArray16[38] = (byte) 181;
    numArray16[10] = (byte) 224 /*0xE0*/;
    numArray16[0] = (byte) 215;
    numArray16[12] = (byte) 59;
    numArray16[13] = (byte) 237;
    numArray16[14] = (byte) 10;
    numArray16[9] = (byte) 56;
    numArray16[8] = (byte) 244;
    numArray16[19] = (byte) 46;
    numArray16[44] = (byte) 76;
    numArray16[49] = (byte) 155;
    numArray16[17] = (byte) 199;
    numArray16[21] = (byte) 150;
    numArray16[32 /*0x20*/] = (byte) 57;
    numArray16[35] = (byte) 133;
    numArray16[5] = (byte) 240 /*0xF0*/;
    numArray16[15] = (byte) 2;
    numArray16[26] = (byte) 40;
    numArray16[27] = (byte) 162;
    numArray16[28] = (byte) 116;
    numArray16[29] = (byte) 217;
    numArray16[23] = (byte) 204;
    numArray16[20] = (byte) 100;
    numArray16[24] = (byte) 237;
    numArray16[33] = (byte) 188;
    numArray16[39] = (byte) 91;
    numArray16[47] = (byte) 174;
    numArray16[36] = (byte) 33;
    numArray16[37] = (byte) 242;
    numArray16[22] = (byte) 18;
    numArray16[7] = (byte) 1;
    numArray16[40] = (byte) 13;
    numArray16[54] = (byte) 172;
    numArray16[41] = (byte) 250;
    numArray16[4] = (byte) 102;
    numArray16[11] = (byte) 186;
    numArray16[45] = (byte) 214;
    numArray16[46] = (byte) 158;
    numArray16[52] = (byte) 12;
    numArray16[48 /*0x30*/] = (byte) 207;
    numArray16[50] = (byte) 41;
    numArray16[16 /*0x10*/] = byte.MaxValue;
    numArray16[3] = (byte) 78;
    numArray16[18] = (byte) 188;
    numArray16[53] = (byte) 114;
    numArray16[51] = (byte) 192 /*0xC0*/;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 205,
      (byte) 191,
      (byte) 125,
      (byte) 217,
      (byte) 99,
      (byte) 173,
      (byte) 74,
      (byte) 7,
      (byte) 226,
      (byte) 2,
      (byte) 209,
      (byte) 23,
      (byte) 80 /*0x50*/,
      (byte) 112 /*0x70*/,
      (byte) 56,
      (byte) 250,
      (byte) 137,
      (byte) 1,
      (byte) 183,
      (byte) 87,
      (byte) 234,
      (byte) 246,
      (byte) 181,
      (byte) 60,
      (byte) 2,
      (byte) 238,
      (byte) 198,
      (byte) 186,
      (byte) 215,
      (byte) 61,
      (byte) 133,
      (byte) 138,
      (byte) 62,
      (byte) 231,
      (byte) 39,
      (byte) 88,
      (byte) 132,
      (byte) 70,
      (byte) 121,
      (byte) 64 /*0x40*/,
      (byte) 200,
      (byte) 12,
      (byte) 193,
      (byte) 225,
      (byte) 219,
      (byte) 168,
      (byte) 32 /*0x20*/,
      (byte) 62,
      (byte) 225,
      (byte) 206,
      (byte) 249,
      (byte) 205,
      (byte) 130,
      (byte) 189,
      (byte) 127 /*0x7F*/
    };
    byte[] numArray18 = new byte[55];
    numArray18[52] = (byte) 47;
    numArray18[1] = (byte) 73;
    numArray18[7] = (byte) 69;
    numArray18[3] = (byte) 212;
    numArray18[11] = (byte) 135;
    numArray18[28] = (byte) 103;
    numArray18[27] = (byte) 130;
    numArray18[16 /*0x10*/] = (byte) 149;
    numArray18[8] = (byte) 105;
    numArray18[5] = (byte) 172;
    numArray18[46] = (byte) 127 /*0x7F*/;
    numArray18[50] = (byte) 87;
    numArray18[33] = (byte) 7;
    numArray18[36] = (byte) 83;
    numArray18[14] = (byte) 54;
    numArray18[32 /*0x20*/] = (byte) 220;
    numArray18[20] = (byte) 226;
    numArray18[18] = (byte) 16 /*0x10*/;
    numArray18[21] = (byte) 188;
    numArray18[19] = (byte) 117;
    numArray18[12] = (byte) 222;
    numArray18[43] = (byte) 71;
    numArray18[40] = (byte) 154;
    numArray18[6] = (byte) 222;
    numArray18[31 /*0x1F*/] = (byte) 209;
    numArray18[25] = (byte) 178;
    numArray18[26] = (byte) 188;
    numArray18[2] = (byte) 202;
    numArray18[10] = (byte) 32 /*0x20*/;
    numArray18[29] = (byte) 130;
    numArray18[30] = (byte) 217;
    numArray18[9] = (byte) 158;
    numArray18[24] = (byte) 35;
    numArray18[15] = (byte) 34;
    numArray18[34] = (byte) 74;
    numArray18[35] = (byte) 183;
    numArray18[4] = (byte) 136;
    numArray18[37] = (byte) 98;
    numArray18[38] = (byte) 116;
    numArray18[39] = (byte) 17;
    numArray18[0] = (byte) 228;
    numArray18[41] = (byte) 79;
    numArray18[17] = (byte) 197;
    numArray18[22] = (byte) 144 /*0x90*/;
    numArray18[44] = (byte) 40;
    numArray18[45] = (byte) 135;
    numArray18[13] = (byte) 83;
    numArray18[47] = (byte) 112 /*0x70*/;
    numArray18[48 /*0x30*/] = (byte) 201;
    numArray18[49] = (byte) 55;
    numArray18[42] = (byte) 76;
    numArray18[51] = (byte) 238;
    numArray18[23] = (byte) 29;
    numArray18[53] = (byte) 168;
    numArray18[54] = (byte) 214;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[40] = (byte) 185;
    numArray19[29] = (byte) 251;
    numArray19[2] = (byte) 168;
    numArray19[44] = (byte) 182;
    numArray19[41] = (byte) 160 /*0xA0*/;
    numArray19[39] = (byte) 179;
    numArray19[6] = (byte) 98;
    numArray19[26] = (byte) 175;
    numArray19[54] = (byte) 199;
    numArray19[5] = (byte) 39;
    numArray19[10] = (byte) 0;
    numArray19[11] = (byte) 248;
    numArray19[46] = (byte) 111;
    numArray19[13] = (byte) 49;
    numArray19[14] = (byte) 212;
    numArray19[15] = (byte) 133;
    numArray19[16 /*0x10*/] = (byte) 27;
    numArray19[17] = (byte) 86;
    numArray19[43] = (byte) 121;
    numArray19[25] = (byte) 216;
    numArray19[20] = (byte) 110;
    numArray19[36] = (byte) 91;
    numArray19[22] = (byte) 138;
    numArray19[23] = (byte) 22;
    numArray19[24] = (byte) 146;
    numArray19[48 /*0x30*/] = (byte) 226;
    numArray19[49] = (byte) 192 /*0xC0*/;
    numArray19[27] = (byte) 141;
    numArray19[21] = (byte) 204;
    numArray19[0] = (byte) 223;
    numArray19[30] = (byte) 83;
    numArray19[31 /*0x1F*/] = (byte) 199;
    numArray19[32 /*0x20*/] = (byte) 174;
    numArray19[1] = (byte) 124;
    numArray19[42] = (byte) 85;
    numArray19[50] = (byte) 240 /*0xF0*/;
    numArray19[33] = (byte) 24;
    numArray19[37] = (byte) 252;
    numArray19[38] = (byte) 131;
    numArray19[12] = (byte) 146;
    numArray19[35] = (byte) 242;
    numArray19[34] = (byte) 100;
    numArray19[19] = (byte) 24;
    numArray19[28] = (byte) 114;
    numArray19[3] = (byte) 93;
    numArray19[45] = (byte) 253;
    numArray19[18] = (byte) 2;
    numArray19[47] = (byte) 39;
    numArray19[51] = (byte) 216;
    numArray19[53] = (byte) 137;
    numArray19[7] = (byte) 196;
    numArray19[8] = (byte) 63 /*0x3F*/;
    numArray19[52] = (byte) 159;
    numArray19[4] = (byte) 182;
    numArray19[9] = (byte) 86;
    byte[] numArray20 = new byte[55];
    numArray20[53] = (byte) 204;
    numArray20[1] = (byte) 153;
    numArray20[10] = (byte) 110;
    numArray20[17] = (byte) 193;
    numArray20[19] = (byte) 220;
    numArray20[29] = (byte) 182;
    numArray20[0] = (byte) 213;
    numArray20[34] = (byte) 247;
    numArray20[8] = (byte) 248;
    numArray20[21] = (byte) 248;
    numArray20[42] = (byte) 241;
    numArray20[37] = (byte) 91;
    numArray20[2] = (byte) 10;
    numArray20[6] = (byte) 108;
    numArray20[41] = (byte) 90;
    numArray20[15] = (byte) 226;
    numArray20[16 /*0x10*/] = (byte) 243;
    numArray20[50] = (byte) 123;
    numArray20[18] = (byte) 229;
    numArray20[48 /*0x30*/] = (byte) 51;
    numArray20[20] = (byte) 119;
    numArray20[7] = (byte) 186;
    numArray20[22] = (byte) 8;
    numArray20[49] = (byte) 18;
    numArray20[24] = (byte) 21;
    numArray20[23] = (byte) 166;
    numArray20[14] = (byte) 160 /*0xA0*/;
    numArray20[27] = (byte) 105;
    numArray20[43] = (byte) 248;
    numArray20[5] = (byte) 234;
    numArray20[12] = (byte) 171;
    numArray20[40] = (byte) 35;
    numArray20[32 /*0x20*/] = (byte) 9;
    numArray20[28] = (byte) 162;
    numArray20[30] = (byte) 99;
    numArray20[35] = (byte) 2;
    numArray20[36] = (byte) 121;
    numArray20[3] = (byte) 202;
    numArray20[38] = (byte) 197;
    numArray20[39] = (byte) 83;
    numArray20[26] = (byte) 92;
    numArray20[33] = (byte) 179;
    numArray20[11] = (byte) 139;
    numArray20[31 /*0x1F*/] = (byte) 85;
    numArray20[44] = (byte) 251;
    numArray20[45] = (byte) 11;
    numArray20[52] = (byte) 52;
    numArray20[47] = (byte) 146;
    numArray20[4] = (byte) 61;
    numArray20[46] = (byte) 40;
    numArray20[13] = (byte) 231;
    numArray20[9] = (byte) 178;
    numArray20[51] = (byte) 77;
    numArray20[25] = (byte) 190;
    numArray20[54] = (byte) 34;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[14]
    {
      (byte) 80 /*0x50*/,
      (byte) 83,
      (byte) 1,
      (byte) 194,
      (byte) 127 /*0x7F*/,
      (byte) 130,
      (byte) 254,
      (byte) 19,
      (byte) 57,
      (byte) 44,
      (byte) 208 /*0xD0*/,
      (byte) 196,
      (byte) 194,
      (byte) 200
    };
    byte[] numArray22 = new byte[14]
    {
      (byte) 80 /*0x50*/,
      (byte) 180,
      (byte) 177,
      (byte) 39,
      (byte) 20,
      (byte) 108,
      (byte) 159,
      (byte) 17,
      (byte) 164,
      (byte) 196,
      (byte) 144 /*0x90*/,
      (byte) 228,
      (byte) 93,
      (byte) 219
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 14);
    for (int index = 0; index < 14; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }
}
