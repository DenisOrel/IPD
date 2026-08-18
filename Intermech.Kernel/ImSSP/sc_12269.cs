// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12269
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12269
{
  private static byte[] sspq = new byte[308]
  {
    (byte) 28,
    (byte) 56,
    (byte) 170,
    (byte) 145,
    (byte) 85,
    (byte) 216,
    (byte) 19,
    (byte) 199,
    (byte) 105,
    (byte) 81,
    (byte) 161,
    (byte) 213,
    (byte) 157,
    (byte) 36,
    (byte) 111,
    (byte) 43,
    (byte) 242,
    (byte) 150,
    (byte) 177,
    (byte) 242,
    (byte) 134,
    (byte) 10,
    (byte) 193,
    (byte) 51,
    (byte) 91,
    (byte) 64 /*0x40*/,
    (byte) 49,
    (byte) 149,
    (byte) 219,
    (byte) 76,
    (byte) 193,
    (byte) 218,
    (byte) 19,
    (byte) 46,
    (byte) 47,
    (byte) 67,
    (byte) 87,
    (byte) 202,
    (byte) 163,
    (byte) 76,
    (byte) 148,
    (byte) 142,
    (byte) 165,
    (byte) 71,
    (byte) 216,
    (byte) 104,
    (byte) 7,
    (byte) 130,
    (byte) 186,
    (byte) 74,
    (byte) 140,
    (byte) 239,
    (byte) 28,
    (byte) 105,
    (byte) 111,
    (byte) 102,
    (byte) 60,
    (byte) 196,
    (byte) 140,
    (byte) 75,
    (byte) 141,
    (byte) 86,
    (byte) 141,
    (byte) 247,
    (byte) 4,
    (byte) 0,
    (byte) 96 /*0x60*/,
    (byte) 47,
    (byte) 184,
    (byte) 25,
    (byte) 199,
    (byte) 216,
    (byte) 95,
    (byte) 243,
    (byte) 147,
    (byte) 100,
    (byte) 192 /*0xC0*/,
    (byte) 230,
    (byte) 160 /*0xA0*/,
    (byte) 196,
    (byte) 112 /*0x70*/,
    (byte) 197,
    (byte) 72,
    (byte) 35,
    (byte) 170,
    (byte) 234,
    (byte) 198,
    (byte) 71,
    (byte) 217,
    (byte) 114,
    (byte) 202,
    (byte) 65,
    (byte) 135,
    (byte) 109,
    (byte) 233,
    (byte) 43,
    (byte) 64 /*0x40*/,
    (byte) 85,
    (byte) 69,
    (byte) 4,
    (byte) 113,
    (byte) 192 /*0xC0*/,
    (byte) 153,
    (byte) 213,
    (byte) 237,
    (byte) 142,
    (byte) 209,
    (byte) 116,
    (byte) 153,
    (byte) 225,
    (byte) 193,
    (byte) 200,
    (byte) 250,
    (byte) 163,
    (byte) 88,
    (byte) 8,
    (byte) 24,
    (byte) 71,
    (byte) 247,
    (byte) 197,
    (byte) 196,
    (byte) 134,
    (byte) 254,
    (byte) 179,
    (byte) 178,
    (byte) 100,
    (byte) 98,
    (byte) 236,
    (byte) 119,
    (byte) 135,
    (byte) 93,
    (byte) 231,
    (byte) 223,
    (byte) 64 /*0x40*/,
    (byte) 194,
    (byte) 18,
    (byte) 128 /*0x80*/,
    (byte) 195,
    (byte) 163,
    (byte) 198,
    (byte) 147,
    (byte) 86,
    (byte) 180,
    (byte) 243,
    (byte) 172,
    (byte) 134,
    (byte) 38,
    (byte) 14,
    (byte) 192 /*0xC0*/,
    (byte) 55,
    (byte) 51,
    (byte) 132,
    (byte) 201,
    (byte) 218,
    (byte) 180,
    (byte) 2,
    (byte) 10,
    (byte) 15,
    (byte) 23,
    (byte) 126,
    (byte) 56,
    (byte) 186,
    (byte) 12,
    (byte) 81,
    (byte) 69,
    (byte) 58,
    (byte) 90,
    (byte) 27,
    (byte) 245,
    (byte) 191,
    (byte) 88,
    (byte) 244,
    (byte) 225,
    (byte) 83,
    (byte) 25,
    (byte) 132,
    (byte) 169,
    (byte) 217,
    (byte) 202,
    (byte) 158,
    (byte) 85,
    (byte) 82,
    (byte) 45,
    (byte) 23,
    (byte) 120,
    (byte) 192 /*0xC0*/,
    (byte) 253,
    (byte) 199,
    (byte) 205,
    (byte) 100,
    (byte) 212,
    (byte) 21,
    (byte) 15,
    (byte) 55,
    (byte) 27,
    (byte) 50,
    (byte) 212,
    (byte) 180,
    (byte) 87,
    (byte) 152,
    (byte) 44,
    (byte) 127 /*0x7F*/,
    (byte) 199,
    (byte) 123,
    (byte) 34,
    (byte) 209,
    (byte) 142,
    (byte) 85,
    (byte) 24,
    (byte) 111,
    (byte) 135,
    (byte) 101,
    (byte) 97,
    (byte) 184,
    (byte) 191,
    (byte) 214,
    (byte) 171,
    (byte) 202,
    (byte) 153,
    (byte) 17,
    (byte) 66,
    (byte) 181,
    (byte) 138,
    (byte) 77,
    (byte) 110,
    (byte) 112 /*0x70*/,
    (byte) 99,
    (byte) 60,
    (byte) 108,
    (byte) 10,
    (byte) 194,
    (byte) 191,
    (byte) 157,
    (byte) 60,
    (byte) 114,
    (byte) 182,
    (byte) 116,
    (byte) 202,
    (byte) 121,
    (byte) 22,
    (byte) 46,
    (byte) 159,
    (byte) 56,
    (byte) 32 /*0x20*/,
    (byte) 223,
    (byte) 77,
    (byte) 128 /*0x80*/,
    (byte) 136,
    (byte) 141,
    (byte) 197,
    (byte) 187,
    (byte) 65,
    (byte) 18,
    (byte) 240 /*0xF0*/,
    (byte) 157,
    (byte) 67,
    (byte) 83,
    (byte) 63 /*0x3F*/,
    (byte) 174,
    (byte) 182,
    (byte) 191,
    (byte) 253,
    (byte) 115,
    (byte) 182,
    (byte) 108,
    (byte) 181,
    (byte) 213,
    (byte) 35,
    (byte) 23,
    (byte) 0,
    (byte) 143,
    (byte) 245,
    (byte) 16 /*0x10*/,
    (byte) 48 /*0x30*/,
    (byte) 5,
    (byte) 202,
    (byte) 245,
    (byte) 133,
    (byte) 73,
    (byte) 134,
    (byte) 17,
    (byte) 9,
    (byte) 130,
    (byte) 3,
    (byte) 140,
    (byte) 191,
    (byte) 5,
    (byte) 180,
    (byte) 208 /*0xD0*/,
    (byte) 250,
    (byte) 82,
    (byte) 191,
    (byte) 87,
    (byte) 239,
    (byte) 87,
    (byte) 19,
    (byte) 11,
    (byte) 128 /*0x80*/,
    (byte) 175,
    (byte) 137,
    (byte) 4,
    (byte) 50,
    (byte) 187,
    (byte) 33,
    (byte) 1,
    (byte) 114,
    (byte) 180,
    (byte) 11
  };
  private static byte[] sspr = new byte[308]
  {
    (byte) 97,
    (byte) 169,
    (byte) 143,
    (byte) 3,
    (byte) 132,
    (byte) 93,
    (byte) 254,
    (byte) 145,
    (byte) 241,
    (byte) 174,
    (byte) 182,
    (byte) 97,
    (byte) 119,
    (byte) 113,
    (byte) 157,
    (byte) 21,
    (byte) 35,
    (byte) 239,
    (byte) 30,
    (byte) 144 /*0x90*/,
    (byte) 121,
    (byte) 173,
    (byte) 63 /*0x3F*/,
    (byte) 38,
    (byte) 61,
    (byte) 68,
    (byte) 61,
    (byte) 147,
    (byte) 252,
    (byte) 79,
    (byte) 24,
    (byte) 56,
    (byte) 114,
    (byte) 152,
    (byte) 200,
    (byte) 204,
    (byte) 41,
    (byte) 56,
    (byte) 128 /*0x80*/,
    (byte) 169,
    (byte) 109,
    (byte) 241,
    (byte) 91,
    (byte) 127 /*0x7F*/,
    (byte) 144 /*0x90*/,
    (byte) 231,
    (byte) 155,
    (byte) 95,
    (byte) 122,
    (byte) 217,
    (byte) 115,
    (byte) 172,
    (byte) 100,
    (byte) 241,
    (byte) 221,
    (byte) 184,
    (byte) 242,
    (byte) 250,
    (byte) 186,
    (byte) 24,
    (byte) 91,
    (byte) 50,
    (byte) 207,
    (byte) 21,
    (byte) 91,
    (byte) 209,
    (byte) 16 /*0x10*/,
    (byte) 62,
    (byte) 247,
    (byte) 228,
    (byte) 61,
    (byte) 3,
    (byte) 212,
    (byte) 202,
    (byte) 200,
    (byte) 186,
    (byte) 71,
    (byte) 232,
    (byte) 234,
    (byte) 40,
    (byte) 130,
    (byte) 120,
    (byte) 178,
    (byte) 127 /*0x7F*/,
    (byte) 70,
    (byte) 77,
    (byte) 27,
    (byte) 36,
    (byte) 3,
    (byte) 206,
    (byte) 168,
    (byte) 105,
    (byte) 151,
    (byte) 35,
    (byte) 49,
    (byte) 112 /*0x70*/,
    (byte) 13,
    (byte) 57,
    (byte) 56,
    (byte) 120,
    (byte) 187,
    (byte) 156,
    (byte) 56,
    (byte) 119,
    (byte) 53,
    (byte) 111,
    (byte) 34,
    (byte) 69,
    (byte) 71,
    (byte) 163,
    (byte) 95,
    (byte) 195,
    (byte) 239,
    (byte) 175,
    (byte) 50,
    (byte) 49,
    (byte) 158,
    (byte) 201,
    (byte) 47,
    (byte) 108,
    (byte) 47,
    (byte) 245,
    (byte) 117,
    (byte) 15,
    (byte) 117,
    (byte) 123,
    (byte) 231,
    (byte) 238,
    (byte) 8,
    (byte) 203,
    (byte) 251,
    (byte) 219,
    (byte) 195,
    (byte) 106,
    (byte) 91,
    (byte) 120,
    (byte) 44,
    (byte) 240 /*0xF0*/,
    (byte) 62,
    (byte) 139,
    (byte) 21,
    (byte) 181,
    (byte) 170,
    (byte) 61,
    (byte) 199,
    (byte) 215,
    (byte) 242,
    (byte) 200,
    (byte) 176 /*0xB0*/,
    (byte) 44,
    (byte) 207,
    (byte) 218,
    (byte) 65,
    (byte) 119,
    (byte) 68,
    (byte) 140,
    (byte) 97,
    (byte) 80 /*0x50*/,
    (byte) 77,
    (byte) 39,
    (byte) 3,
    (byte) 123,
    (byte) 253,
    (byte) 60,
    (byte) 214,
    (byte) 6,
    (byte) 142,
    (byte) 164,
    (byte) 159,
    (byte) 76,
    (byte) 150,
    (byte) 209,
    (byte) 82,
    (byte) 120,
    (byte) 27,
    (byte) 105,
    (byte) 215,
    (byte) 175,
    (byte) 78,
    (byte) 63 /*0x3F*/,
    (byte) 169,
    (byte) 169,
    (byte) 100,
    (byte) 138,
    (byte) 207,
    (byte) 134,
    (byte) 114,
    (byte) 185,
    (byte) 151,
    (byte) 199,
    (byte) 215,
    (byte) 49,
    (byte) 213,
    (byte) 22,
    (byte) 68,
    (byte) 220,
    (byte) 129,
    (byte) 241,
    (byte) 134,
    (byte) 82,
    (byte) 229,
    (byte) 247,
    (byte) 230,
    (byte) 249,
    (byte) 187,
    (byte) 92,
    (byte) 122,
    (byte) 160 /*0xA0*/,
    (byte) 235,
    (byte) 78,
    (byte) 69,
    (byte) 156,
    (byte) 133,
    (byte) 241,
    (byte) 0,
    (byte) 125,
    (byte) 37,
    (byte) 177,
    (byte) 67,
    (byte) 68,
    (byte) 147,
    (byte) 40,
    (byte) 153,
    (byte) 204,
    (byte) 70,
    (byte) 60,
    (byte) 182,
    (byte) 184,
    (byte) 190,
    (byte) 211,
    (byte) 60,
    (byte) 7,
    (byte) 82,
    (byte) 11,
    (byte) 122,
    (byte) 80 /*0x50*/,
    (byte) 244,
    (byte) 109,
    (byte) 187,
    (byte) 124,
    (byte) 235,
    (byte) 245,
    (byte) 27,
    (byte) 187,
    (byte) 80 /*0x50*/,
    (byte) 23,
    (byte) 75,
    (byte) 78,
    (byte) 77,
    (byte) 68,
    byte.MaxValue,
    (byte) 112 /*0x70*/,
    (byte) 104,
    (byte) 184,
    (byte) 22,
    (byte) 70,
    (byte) 179,
    (byte) 145,
    (byte) 171,
    (byte) 27,
    (byte) 111,
    (byte) 16 /*0x10*/,
    (byte) 134,
    (byte) 108,
    (byte) 26,
    (byte) 114,
    (byte) 175,
    (byte) 72,
    (byte) 136,
    (byte) 154,
    (byte) 202,
    (byte) 244,
    (byte) 241,
    (byte) 153,
    (byte) 21,
    (byte) 12,
    (byte) 124,
    (byte) 69,
    (byte) 210,
    (byte) 22,
    (byte) 8,
    (byte) 167,
    (byte) 83,
    (byte) 93,
    (byte) 104,
    (byte) 17,
    (byte) 179,
    (byte) 151,
    (byte) 98,
    (byte) 204,
    (byte) 35,
    (byte) 174,
    (byte) 222,
    (byte) 211,
    (byte) 93,
    (byte) 199,
    (byte) 204,
    (byte) 208 /*0xD0*/,
    (byte) 72,
    (byte) 127 /*0x7F*/,
    (byte) 240 /*0xF0*/,
    (byte) 183,
    (byte) 61,
    (byte) 183,
    (byte) 234,
    (byte) 67,
    (byte) 95,
    (byte) 40
  };

  internal static int ssp_appserver_12270(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 155,
      (byte) 245,
      (byte) 11,
      (byte) 207,
      (byte) 65,
      (byte) 223,
      (byte) 167,
      (byte) 99,
      (byte) 59,
      (byte) 83,
      (byte) 39,
      (byte) 122,
      (byte) 121,
      (byte) 117,
      (byte) 75,
      (byte) 173,
      (byte) 167,
      (byte) 182,
      (byte) 116,
      (byte) 2,
      (byte) 238,
      (byte) 204,
      (byte) 207,
      (byte) 186,
      (byte) 96 /*0x60*/,
      (byte) 190,
      (byte) 101,
      (byte) 17,
      (byte) 100,
      (byte) 124,
      (byte) 198,
      (byte) 183,
      (byte) 193,
      (byte) 8,
      (byte) 194,
      (byte) 161,
      (byte) 150,
      (byte) 93,
      (byte) 88,
      (byte) 70,
      (byte) 170,
      (byte) 31 /*0x1F*/,
      (byte) 119,
      (byte) 191,
      (byte) 203,
      (byte) 28,
      (byte) 204,
      (byte) 250
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 228,
      (byte) 109,
      (byte) 243,
      (byte) 168,
      (byte) 215,
      (byte) 112 /*0x70*/,
      (byte) 45,
      (byte) 101,
      (byte) 90,
      (byte) 235,
      (byte) 52,
      (byte) 173,
      (byte) 169,
      (byte) 44,
      (byte) 205,
      (byte) 134,
      (byte) 19,
      (byte) 214,
      (byte) 28,
      (byte) 122,
      (byte) 13,
      (byte) 2,
      (byte) 111,
      (byte) 119,
      (byte) 244,
      (byte) 161,
      (byte) 95,
      (byte) 27,
      (byte) 170,
      (byte) 194,
      (byte) 27,
      (byte) 181,
      (byte) 91,
      (byte) 214,
      (byte) 225,
      (byte) 50,
      (byte) 94,
      (byte) 154,
      (byte) 175,
      (byte) 32 /*0x20*/,
      (byte) 73,
      (byte) 87,
      (byte) 252,
      (byte) 171,
      (byte) 87,
      (byte) 2,
      (byte) 53,
      (byte) 15
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12271()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 111,
        (byte) 235,
        (byte) 37,
        (byte) 207,
        (byte) 37,
        (byte) 80 /*0x50*/,
        (byte) 111,
        (byte) 131,
        (byte) 121,
        (byte) 149
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 87,
        (byte) 105,
        (byte) 208 /*0xD0*/,
        (byte) 39,
        (byte) 226,
        (byte) 191,
        (byte) 109,
        (byte) 177,
        (byte) 126,
        (byte) 55
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 111,
      (byte) 184,
      (byte) 113,
      (byte) 178,
      (byte) 170,
      (byte) 22,
      (byte) 74,
      (byte) 93,
      (byte) 46,
      (byte) 88
    };
    byte[] numArray6 = new byte[10];
    numArray6[6] = (byte) 70;
    numArray6[1] = (byte) 27;
    numArray6[0] = (byte) 212;
    numArray6[8] = (byte) 165;
    numArray6[7] = (byte) 65;
    numArray6[5] = (byte) 61;
    numArray6[3] = (byte) 216;
    numArray6[2] = (byte) 195;
    numArray6[9] = (byte) 216;
    numArray6[4] = (byte) 199;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12272()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[10] = (byte) 91;
      numArray2[1] = (byte) 123;
      numArray2[20] = (byte) 86;
      numArray2[8] = (byte) 185;
      numArray2[4] = (byte) 213;
      numArray2[18] = (byte) 65;
      numArray2[6] = (byte) 20;
      numArray2[7] = (byte) 32 /*0x20*/;
      numArray2[0] = (byte) 242;
      numArray2[9] = (byte) 50;
      numArray2[17] = (byte) 139;
      numArray2[5] = (byte) 204;
      numArray2[12] = (byte) 90;
      numArray2[11] = (byte) 99;
      numArray2[19] = (byte) 146;
      numArray2[15] = (byte) 109;
      numArray2[2] = (byte) 69;
      numArray2[13] = (byte) 71;
      numArray2[14] = (byte) 228;
      numArray2[16 /*0x10*/] = (byte) 42;
      numArray2[3] = (byte) 237;
      byte[] numArray3 = new byte[21]
      {
        (byte) 235,
        (byte) 230,
        (byte) 201,
        (byte) 169,
        (byte) 115,
        (byte) 238,
        (byte) 96 /*0x60*/,
        (byte) 144 /*0x90*/,
        (byte) 226,
        (byte) 121,
        (byte) 224 /*0xE0*/,
        (byte) 147,
        (byte) 249,
        (byte) 1,
        (byte) 1,
        (byte) 59,
        (byte) 203,
        (byte) 91,
        (byte) 132,
        (byte) 46,
        (byte) 241
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 224 /*0xE0*/,
      (byte) 151,
      (byte) 35,
      (byte) 156,
      (byte) 114,
      (byte) 220,
      (byte) 80 /*0x50*/,
      (byte) 7,
      (byte) 67,
      (byte) 140,
      (byte) 120,
      (byte) 102,
      (byte) 227,
      (byte) 194,
      (byte) 68,
      (byte) 189,
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 129,
      (byte) 52
    };
    byte[] numArray6 = new byte[21];
    numArray6[0] = (byte) 164;
    numArray6[1] = (byte) 55;
    numArray6[16 /*0x10*/] = (byte) 102;
    numArray6[3] = (byte) 182;
    numArray6[5] = (byte) 168;
    numArray6[4] = (byte) 172;
    numArray6[6] = (byte) 30;
    numArray6[18] = (byte) 166;
    numArray6[8] = (byte) 40;
    numArray6[9] = (byte) 39;
    numArray6[10] = (byte) 157;
    numArray6[11] = (byte) 136;
    numArray6[12] = (byte) 12;
    numArray6[13] = (byte) 6;
    numArray6[14] = (byte) 210;
    numArray6[15] = (byte) 61;
    numArray6[2] = (byte) 157;
    numArray6[17] = (byte) 115;
    numArray6[7] = (byte) 174;
    numArray6[19] = (byte) 244;
    numArray6[20] = (byte) 209;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12273()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 236,
        (byte) 153,
        (byte) 67,
        (byte) 164,
        (byte) 67,
        (byte) 173,
        (byte) 152,
        (byte) 93,
        (byte) 95,
        (byte) 94
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 69,
        (byte) 77,
        (byte) 146,
        (byte) 198,
        (byte) 53,
        (byte) 92,
        (byte) 8,
        (byte) 106,
        (byte) 31 /*0x1F*/,
        (byte) 211
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 48 /*0x30*/;
    numArray5[6] = (byte) 152;
    numArray5[2] = (byte) 76;
    numArray5[3] = (byte) 40;
    numArray5[4] = (byte) 244;
    numArray5[5] = (byte) 228;
    numArray5[9] = (byte) 119;
    numArray5[1] = (byte) 14;
    numArray5[8] = (byte) 21;
    numArray5[0] = (byte) 213;
    byte[] numArray6 = new byte[10]
    {
      (byte) 97,
      (byte) 219,
      (byte) 159,
      (byte) 20,
      (byte) 29,
      (byte) 167,
      (byte) 190,
      (byte) 214,
      (byte) 102,
      (byte) 121
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12274()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[8] = (byte) 78;
      numArray2[1] = (byte) 11;
      numArray2[3] = (byte) 70;
      numArray2[0] = (byte) 58;
      numArray2[4] = (byte) 217;
      numArray2[7] = (byte) 46;
      numArray2[6] = (byte) 236;
      numArray2[5] = (byte) 111;
      numArray2[2] = (byte) 249;
      numArray2[9] = (byte) 144 /*0x90*/;
      byte[] numArray3 = new byte[10]
      {
        (byte) 53,
        (byte) 245,
        (byte) 18,
        (byte) 99,
        (byte) 207,
        (byte) 90,
        (byte) 52,
        (byte) 137,
        (byte) 73,
        (byte) 162
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[6] = (byte) 91;
    numArray5[1] = (byte) 112 /*0x70*/;
    numArray5[2] = (byte) 60;
    numArray5[5] = (byte) 2;
    numArray5[4] = (byte) 124;
    numArray5[9] = (byte) 216;
    numArray5[3] = (byte) 75;
    numArray5[7] = (byte) 249;
    numArray5[8] = (byte) 37;
    numArray5[0] = (byte) 88;
    byte[] numArray6 = new byte[10]
    {
      (byte) 129,
      (byte) 148,
      (byte) 201,
      (byte) 239,
      (byte) 183,
      (byte) 143,
      (byte) 148,
      (byte) 46,
      (byte) 143,
      (byte) 17
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[40];
    byte[] response = new byte[40];
    Array.Copy((Array) sc_12269.sspq, 0, (Array) numArray7, 0, 40);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12269.sspr, 0, (Array) numArray7, 0, 40);
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

  internal static string ssp_appserver_12275()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 196,
        (byte) 132,
        (byte) 151,
        (byte) 73,
        (byte) 124,
        (byte) 185,
        (byte) 129,
        (byte) 27,
        (byte) 240 /*0xF0*/,
        (byte) 145
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 103,
        (byte) 80 /*0x50*/,
        (byte) 164,
        (byte) 159,
        (byte) 109,
        (byte) 155,
        (byte) 145,
        (byte) 200,
        (byte) 57,
        (byte) 35
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 134,
      (byte) 18,
      (byte) 118,
      (byte) 119,
      (byte) 53,
      (byte) 21,
      (byte) 167,
      (byte) 126,
      (byte) 236,
      (byte) 28
    };
    byte[] numArray6 = new byte[10];
    numArray6[6] = (byte) 1;
    numArray6[1] = (byte) 46;
    numArray6[2] = (byte) 99;
    numArray6[3] = (byte) 41;
    numArray6[0] = (byte) 190;
    numArray6[8] = (byte) 190;
    numArray6[4] = (byte) 216;
    numArray6[7] = (byte) 50;
    numArray6[5] = (byte) 64 /*0x40*/;
    numArray6[9] = (byte) 239;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12276()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 234,
        (byte) 223,
        (byte) 135,
        (byte) 175,
        (byte) 121,
        (byte) 235,
        (byte) 187,
        (byte) 95,
        (byte) 14,
        (byte) 169
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 79,
        (byte) 141,
        (byte) 92,
        (byte) 243,
        (byte) 175,
        (byte) 162,
        (byte) 138,
        (byte) 101,
        (byte) 243,
        (byte) 14
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_12269.sspq, 40, (Array) numArray4, 0, 38);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12269.sspr, 40, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10];
    numArray6[5] = (byte) 60;
    numArray6[8] = (byte) 206;
    numArray6[0] = (byte) 233;
    numArray6[3] = (byte) 103;
    numArray6[4] = (byte) 36;
    numArray6[6] = (byte) 35;
    numArray6[1] = (byte) 29;
    numArray6[9] = (byte) 82;
    numArray6[2] = (byte) 35;
    numArray6[7] = (byte) 138;
    byte[] numArray7 = new byte[10]
    {
      (byte) 247,
      (byte) 97,
      (byte) 133,
      (byte) 98,
      (byte) 117,
      (byte) 106,
      byte.MaxValue,
      (byte) 129,
      (byte) 217,
      (byte) 181
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12277()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/];
      numArray2[31 /*0x1F*/] = (byte) 18;
      numArray2[16 /*0x10*/] = (byte) 211;
      numArray2[38] = (byte) 250;
      numArray2[3] = (byte) 180;
      numArray2[4] = (byte) 143;
      numArray2[5] = (byte) 40;
      numArray2[6] = (byte) 213;
      numArray2[7] = (byte) 10;
      numArray2[17] = (byte) 122;
      numArray2[14] = (byte) 49;
      numArray2[10] = (byte) 219;
      numArray2[11] = (byte) 111;
      numArray2[22] = (byte) 172;
      numArray2[29] = (byte) 18;
      numArray2[1] = (byte) 160 /*0xA0*/;
      numArray2[47] = (byte) 46;
      numArray2[40] = (byte) 125;
      numArray2[32 /*0x20*/] = (byte) 21;
      numArray2[0] = (byte) 202;
      numArray2[19] = (byte) 14;
      numArray2[12] = (byte) 40;
      numArray2[21] = (byte) 17;
      numArray2[8] = (byte) 166;
      numArray2[24] = (byte) 71;
      numArray2[43] = (byte) 22;
      numArray2[25] = (byte) 222;
      numArray2[26] = (byte) 159;
      numArray2[46] = (byte) 88;
      numArray2[23] = (byte) 228;
      numArray2[44] = (byte) 7;
      numArray2[30] = (byte) 163;
      numArray2[9] = (byte) 85;
      numArray2[28] = (byte) 99;
      numArray2[33] = (byte) 75;
      numArray2[34] = (byte) 157;
      numArray2[35] = (byte) 131;
      numArray2[20] = (byte) 63 /*0x3F*/;
      numArray2[37] = (byte) 110;
      numArray2[36] = (byte) 185;
      numArray2[39] = (byte) 165;
      numArray2[13] = (byte) 37;
      numArray2[41] = (byte) 230;
      numArray2[27] = (byte) 86;
      numArray2[2] = (byte) 131;
      numArray2[15] = (byte) 80 /*0x50*/;
      numArray2[45] = (byte) 33;
      numArray2[18] = (byte) 50;
      numArray2[42] = (byte) 0;
      byte[] numArray3 = new byte[48 /*0x30*/];
      numArray3[26] = (byte) 107;
      numArray3[39] = (byte) 111;
      numArray3[30] = (byte) 150;
      numArray3[7] = (byte) 1;
      numArray3[21] = (byte) 117;
      numArray3[5] = (byte) 19;
      numArray3[12] = (byte) 136;
      numArray3[6] = (byte) 68;
      numArray3[8] = (byte) 103;
      numArray3[40] = (byte) 143;
      numArray3[38] = (byte) 129;
      numArray3[34] = (byte) 202;
      numArray3[41] = (byte) 185;
      numArray3[14] = (byte) 236;
      numArray3[0] = (byte) 176 /*0xB0*/;
      numArray3[13] = (byte) 204;
      numArray3[16 /*0x10*/] = (byte) 71;
      numArray3[24] = (byte) 102;
      numArray3[35] = (byte) 71;
      numArray3[19] = (byte) 113;
      numArray3[20] = (byte) 139;
      numArray3[11] = (byte) 85;
      numArray3[43] = (byte) 25;
      numArray3[33] = (byte) 25;
      numArray3[10] = (byte) 19;
      numArray3[25] = (byte) 14;
      numArray3[4] = (byte) 42;
      numArray3[27] = (byte) 208 /*0xD0*/;
      numArray3[28] = (byte) 229;
      numArray3[29] = (byte) 119;
      numArray3[1] = (byte) 170;
      numArray3[17] = (byte) 233;
      numArray3[32 /*0x20*/] = (byte) 64 /*0x40*/;
      numArray3[3] = (byte) 188;
      numArray3[15] = (byte) 191;
      numArray3[31 /*0x1F*/] = (byte) 95;
      numArray3[36] = byte.MaxValue;
      numArray3[37] = (byte) 101;
      numArray3[9] = (byte) 250;
      numArray3[22] = (byte) 173;
      numArray3[23] = (byte) 208 /*0xD0*/;
      numArray3[44] = (byte) 233;
      numArray3[42] = (byte) 55;
      numArray3[18] = (byte) 186;
      numArray3[2] = (byte) 130;
      numArray3[45] = (byte) 220;
      numArray3[46] = (byte) 200;
      numArray3[47] = (byte) 122;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[48 /*0x30*/];
    byte[] numArray5 = new byte[48 /*0x30*/];
    numArray5[39] = (byte) 146;
    numArray5[1] = (byte) 29;
    numArray5[0] = (byte) 187;
    numArray5[38] = (byte) 204;
    numArray5[37] = (byte) 200;
    numArray5[5] = (byte) 17;
    numArray5[6] = (byte) 142;
    numArray5[26] = (byte) 151;
    numArray5[16 /*0x10*/] = (byte) 213;
    numArray5[7] = (byte) 225;
    numArray5[10] = (byte) 1;
    numArray5[34] = (byte) 167;
    numArray5[43] = (byte) 182;
    numArray5[21] = (byte) 231;
    numArray5[8] = (byte) 137;
    numArray5[15] = (byte) 169;
    numArray5[46] = (byte) 251;
    numArray5[17] = (byte) 37;
    numArray5[18] = (byte) 4;
    numArray5[47] = (byte) 176 /*0xB0*/;
    numArray5[9] = (byte) 82;
    numArray5[41] = (byte) 190;
    numArray5[22] = (byte) 33;
    numArray5[23] = (byte) 75;
    numArray5[24] = (byte) 72;
    numArray5[28] = (byte) 9;
    numArray5[42] = (byte) 213;
    numArray5[27] = (byte) 66;
    numArray5[25] = (byte) 89;
    numArray5[29] = (byte) 137;
    numArray5[30] = (byte) 180;
    numArray5[31 /*0x1F*/] = (byte) 104;
    numArray5[14] = (byte) 17;
    numArray5[45] = (byte) 173;
    numArray5[19] = (byte) 46;
    numArray5[35] = (byte) 25;
    numArray5[36] = (byte) 105;
    numArray5[3] = (byte) 201;
    numArray5[2] = (byte) 5;
    numArray5[12] = (byte) 157;
    numArray5[40] = (byte) 161;
    numArray5[11] = (byte) 63 /*0x3F*/;
    numArray5[32 /*0x20*/] = (byte) 238;
    numArray5[13] = (byte) 18;
    numArray5[44] = (byte) 202;
    numArray5[20] = (byte) 32 /*0x20*/;
    numArray5[4] = (byte) 76;
    numArray5[33] = (byte) 32 /*0x20*/;
    byte[] numArray6 = new byte[48 /*0x30*/];
    numArray6[47] = (byte) 213;
    numArray6[1] = (byte) 61;
    numArray6[40] = (byte) 201;
    numArray6[15] = (byte) 135;
    numArray6[18] = (byte) 20;
    numArray6[5] = (byte) 144 /*0x90*/;
    numArray6[32 /*0x20*/] = (byte) 120;
    numArray6[7] = (byte) 223;
    numArray6[14] = (byte) 245;
    numArray6[34] = (byte) 21;
    numArray6[10] = (byte) 45;
    numArray6[4] = (byte) 202;
    numArray6[43] = (byte) 8;
    numArray6[13] = (byte) 130;
    numArray6[31 /*0x1F*/] = byte.MaxValue;
    numArray6[17] = (byte) 205;
    numArray6[2] = (byte) 104;
    numArray6[21] = (byte) 225;
    numArray6[12] = (byte) 197;
    numArray6[8] = (byte) 51;
    numArray6[20] = (byte) 178;
    numArray6[19] = (byte) 135;
    numArray6[22] = (byte) 182;
    numArray6[23] = (byte) 24;
    numArray6[24] = (byte) 45;
    numArray6[25] = (byte) 224 /*0xE0*/;
    numArray6[26] = (byte) 26;
    numArray6[37] = (byte) 182;
    numArray6[28] = (byte) 227;
    numArray6[29] = (byte) 171;
    numArray6[30] = (byte) 63 /*0x3F*/;
    numArray6[41] = (byte) 11;
    numArray6[16 /*0x10*/] = (byte) 11;
    numArray6[0] = (byte) 186;
    numArray6[35] = (byte) 208 /*0xD0*/;
    numArray6[9] = (byte) 204;
    numArray6[36] = (byte) 253;
    numArray6[27] = (byte) 79;
    numArray6[38] = (byte) 234;
    numArray6[39] = (byte) 247;
    numArray6[33] = (byte) 3;
    numArray6[45] = (byte) 198;
    numArray6[42] = (byte) 108;
    numArray6[6] = (byte) 218;
    numArray6[44] = (byte) 29;
    numArray6[11] = (byte) 71;
    numArray6[46] = (byte) 85;
    numArray6[3] = (byte) 178;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12278()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 68,
        (byte) 133,
        (byte) 203,
        (byte) 111,
        (byte) 129,
        (byte) 225,
        (byte) 200,
        (byte) 248,
        (byte) 232,
        (byte) 175,
        (byte) 175,
        (byte) 215,
        (byte) 10,
        (byte) 118,
        (byte) 106,
        (byte) 62,
        (byte) 92,
        (byte) 132,
        (byte) 61,
        (byte) 233,
        (byte) 168,
        (byte) 182,
        (byte) 242,
        (byte) 83,
        (byte) 128 /*0x80*/
      };
      byte[] numArray3 = new byte[25]
      {
        (byte) 133,
        (byte) 193,
        (byte) 252,
        (byte) 139,
        (byte) 226,
        (byte) 82,
        (byte) 243,
        (byte) 53,
        (byte) 211,
        (byte) 50,
        (byte) 212,
        (byte) 133,
        (byte) 132,
        (byte) 93,
        (byte) 167,
        (byte) 74,
        (byte) 113,
        (byte) 5,
        (byte) 249,
        (byte) 116,
        (byte) 95,
        (byte) 134,
        (byte) 161,
        (byte) 47,
        (byte) 241
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 236,
      (byte) 135,
      (byte) 132,
      (byte) 76,
      (byte) 27,
      (byte) 102,
      (byte) 58,
      (byte) 212,
      (byte) 106,
      (byte) 12,
      (byte) 186,
      (byte) 113,
      (byte) 134,
      (byte) 70,
      (byte) 246,
      (byte) 241,
      (byte) 135,
      (byte) 31 /*0x1F*/,
      (byte) 208 /*0xD0*/,
      (byte) 251,
      (byte) 2,
      (byte) 56,
      (byte) 178,
      (byte) 141,
      (byte) 79
    };
    byte[] numArray6 = new byte[25];
    numArray6[23] = (byte) 175;
    numArray6[14] = (byte) 52;
    numArray6[20] = (byte) 29;
    numArray6[16 /*0x10*/] = (byte) 58;
    numArray6[13] = (byte) 224 /*0xE0*/;
    numArray6[22] = (byte) 29;
    numArray6[1] = (byte) 68;
    numArray6[9] = (byte) 90;
    numArray6[2] = (byte) 121;
    numArray6[0] = (byte) 94;
    numArray6[10] = (byte) 37;
    numArray6[11] = (byte) 170;
    numArray6[12] = (byte) 220;
    numArray6[8] = (byte) 17;
    numArray6[6] = (byte) 136;
    numArray6[15] = (byte) 146;
    numArray6[19] = (byte) 150;
    numArray6[5] = (byte) 130;
    numArray6[18] = (byte) 223;
    numArray6[7] = (byte) 119;
    numArray6[17] = (byte) 68;
    numArray6[21] = (byte) 218;
    numArray6[3] = (byte) 228;
    numArray6[4] = (byte) 190;
    numArray6[24] = (byte) 127 /*0x7F*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12279()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 248,
        (byte) 179,
        (byte) 212,
        (byte) 246,
        (byte) 253,
        (byte) 144 /*0x90*/,
        (byte) 174,
        (byte) 173,
        (byte) 64 /*0x40*/,
        (byte) 242,
        (byte) 30,
        (byte) 178,
        (byte) 85,
        (byte) 185,
        (byte) 57,
        (byte) 149,
        (byte) 197,
        (byte) 35,
        (byte) 99,
        (byte) 43,
        (byte) 191,
        (byte) 109,
        (byte) 100
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 97,
        (byte) 92,
        (byte) 49,
        (byte) 248,
        (byte) 16 /*0x10*/,
        (byte) 207,
        (byte) 144 /*0x90*/,
        (byte) 68,
        (byte) 148,
        (byte) 143,
        (byte) 158,
        (byte) 0,
        (byte) 1,
        (byte) 77,
        byte.MaxValue,
        (byte) 254,
        (byte) 169,
        (byte) 229,
        (byte) 233,
        (byte) 134,
        (byte) 244,
        (byte) 191,
        (byte) 244
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[19] = (byte) 23;
    numArray5[7] = (byte) 80 /*0x50*/;
    numArray5[11] = (byte) 53;
    numArray5[17] = (byte) 225;
    numArray5[4] = (byte) 171;
    numArray5[0] = (byte) 40;
    numArray5[6] = (byte) 94;
    numArray5[1] = (byte) 85;
    numArray5[2] = (byte) 204;
    numArray5[9] = (byte) 89;
    numArray5[10] = (byte) 140;
    numArray5[14] = (byte) 245;
    numArray5[12] = (byte) 102;
    numArray5[13] = (byte) 43;
    numArray5[8] = (byte) 213;
    numArray5[15] = (byte) 141;
    numArray5[20] = (byte) 118;
    numArray5[3] = (byte) 146;
    numArray5[18] = (byte) 50;
    numArray5[16 /*0x10*/] = (byte) 84;
    numArray5[21] = (byte) 169;
    numArray5[5] = (byte) 140;
    numArray5[22] = (byte) 40;
    byte[] numArray6 = new byte[23];
    numArray6[11] = (byte) 119;
    numArray6[1] = (byte) 195;
    numArray6[4] = (byte) 2;
    numArray6[3] = (byte) 174;
    numArray6[21] = (byte) 33;
    numArray6[0] = (byte) 138;
    numArray6[16 /*0x10*/] = (byte) 198;
    numArray6[7] = (byte) 147;
    numArray6[8] = (byte) 5;
    numArray6[10] = (byte) 219;
    numArray6[9] = (byte) 218;
    numArray6[6] = (byte) 238;
    numArray6[22] = (byte) 199;
    numArray6[12] = (byte) 130;
    numArray6[13] = (byte) 222;
    numArray6[15] = (byte) 50;
    numArray6[14] = (byte) 171;
    numArray6[17] = (byte) 210;
    numArray6[18] = (byte) 114;
    numArray6[5] = (byte) 158;
    numArray6[20] = (byte) 171;
    numArray6[2] = (byte) 136;
    numArray6[19] = (byte) 152;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[31 /*0x1F*/];
    byte[] response = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_12269.sspq, 78, (Array) numArray7, 0, 31 /*0x1F*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12269.sspr, 78, (Array) numArray7, 0, 31 /*0x1F*/);
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

  internal static string ssp_appserver_12280()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[13] = (byte) 215;
      numArray2[1] = (byte) 234;
      numArray2[2] = (byte) 144 /*0x90*/;
      numArray2[0] = (byte) 113;
      numArray2[5] = (byte) 184;
      numArray2[6] = (byte) 229;
      numArray2[14] = (byte) 64 /*0x40*/;
      numArray2[15] = (byte) 217;
      numArray2[8] = (byte) 173;
      numArray2[11] = (byte) 154;
      numArray2[10] = (byte) 198;
      numArray2[12] = (byte) 107;
      numArray2[3] = (byte) 230;
      numArray2[9] = (byte) 23;
      numArray2[7] = (byte) 125;
      numArray2[4] = (byte) 155;
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[13] = (byte) 95;
      numArray3[5] = (byte) 184;
      numArray3[2] = (byte) 91;
      numArray3[3] = (byte) 92;
      numArray3[4] = (byte) 41;
      numArray3[14] = (byte) 153;
      numArray3[9] = (byte) 111;
      numArray3[10] = (byte) 21;
      numArray3[8] = (byte) 79;
      numArray3[11] = (byte) 76;
      numArray3[7] = (byte) 15;
      numArray3[1] = (byte) 126;
      numArray3[12] = (byte) 98;
      numArray3[6] = (byte) 147;
      numArray3[0] = (byte) 41;
      numArray3[15] = (byte) 71;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[6] = (byte) 244;
    numArray5[1] = (byte) 155;
    numArray5[0] = (byte) 82;
    numArray5[3] = (byte) 24;
    numArray5[11] = (byte) 71;
    numArray5[10] = (byte) 114;
    numArray5[13] = (byte) 254;
    numArray5[7] = (byte) 148;
    numArray5[8] = (byte) 235;
    numArray5[15] = (byte) 236;
    numArray5[9] = (byte) 140;
    numArray5[2] = (byte) 40;
    numArray5[4] = (byte) 32 /*0x20*/;
    numArray5[5] = (byte) 150;
    numArray5[14] = (byte) 116;
    numArray5[12] = (byte) 146;
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[7] = (byte) 0;
    numArray6[1] = (byte) 227;
    numArray6[4] = (byte) 145;
    numArray6[11] = (byte) 194;
    numArray6[6] = (byte) 152;
    numArray6[8] = (byte) 100;
    numArray6[5] = (byte) 221;
    numArray6[14] = (byte) 251;
    numArray6[12] = (byte) 75;
    numArray6[9] = (byte) 116;
    numArray6[10] = (byte) 250;
    numArray6[0] = (byte) 224 /*0xE0*/;
    numArray6[2] = (byte) 104;
    numArray6[13] = (byte) 221;
    numArray6[3] = (byte) 229;
    numArray6[15] = (byte) 246;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12281()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 53,
        (byte) 236,
        (byte) 57,
        (byte) 165,
        (byte) 242,
        (byte) 254,
        (byte) 111,
        (byte) 119,
        (byte) 111,
        (byte) 150,
        (byte) 25,
        (byte) 144 /*0x90*/,
        (byte) 35,
        (byte) 149,
        (byte) 36,
        (byte) 138,
        (byte) 55,
        (byte) 189,
        (byte) 102,
        (byte) 220,
        (byte) 246,
        (byte) 21,
        (byte) 106,
        (byte) 206,
        (byte) 198,
        (byte) 4,
        (byte) 86
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 164,
        (byte) 213,
        (byte) 48 /*0x30*/,
        (byte) 1,
        (byte) 98,
        (byte) 7,
        (byte) 60,
        (byte) 173,
        (byte) 115,
        (byte) 223,
        (byte) 153,
        (byte) 61,
        (byte) 105,
        (byte) 235,
        (byte) 194,
        (byte) 112 /*0x70*/,
        (byte) 169,
        (byte) 147,
        (byte) 111,
        (byte) 86,
        (byte) 72,
        (byte) 8,
        (byte) 218,
        (byte) 86,
        (byte) 32 /*0x20*/,
        (byte) 88,
        (byte) 197
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 147,
      (byte) 143,
      (byte) 29,
      (byte) 126,
      (byte) 167,
      (byte) 54,
      (byte) 206,
      (byte) 33,
      (byte) 196,
      (byte) 134,
      (byte) 203,
      (byte) 104,
      (byte) 218,
      (byte) 38,
      (byte) 243,
      (byte) 86,
      (byte) 128 /*0x80*/,
      (byte) 62,
      (byte) 86,
      (byte) 139,
      (byte) 247,
      (byte) 12,
      (byte) 132,
      (byte) 96 /*0x60*/,
      (byte) 244,
      (byte) 32 /*0x20*/,
      (byte) 129
    };
    byte[] numArray6 = new byte[27]
    {
      (byte) 47,
      (byte) 45,
      (byte) 122,
      (byte) 210,
      (byte) 190,
      (byte) 108,
      (byte) 40,
      (byte) 88,
      (byte) 58,
      (byte) 47,
      (byte) 136,
      (byte) 121,
      (byte) 245,
      (byte) 177,
      (byte) 33,
      (byte) 32 /*0x20*/,
      (byte) 130,
      (byte) 198,
      (byte) 183,
      (byte) 151,
      (byte) 186,
      (byte) 156,
      (byte) 224 /*0xE0*/,
      (byte) 122,
      (byte) 155,
      (byte) 231,
      (byte) 168
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12282(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 228,
      (byte) 120,
      (byte) 2,
      (byte) 137,
      (byte) 114,
      (byte) 164,
      (byte) 238,
      (byte) 238,
      (byte) 188,
      (byte) 22,
      (byte) 248,
      (byte) 224 /*0xE0*/,
      (byte) 16 /*0x10*/,
      (byte) 142,
      (byte) 58,
      (byte) 67,
      (byte) 219,
      (byte) 112 /*0x70*/,
      (byte) 132,
      (byte) 23,
      (byte) 215,
      (byte) 7,
      (byte) 0,
      (byte) 135,
      (byte) 10,
      (byte) 189,
      (byte) 137,
      (byte) 135,
      (byte) 180,
      (byte) 99,
      (byte) 66,
      (byte) 215,
      (byte) 222,
      (byte) 138,
      (byte) 131,
      (byte) 98,
      (byte) 36,
      (byte) 105,
      (byte) 228,
      (byte) 220,
      (byte) 79,
      (byte) 104,
      (byte) 207,
      (byte) 224 /*0xE0*/,
      (byte) 20,
      (byte) 164,
      (byte) 79,
      (byte) 102
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 176 /*0xB0*/,
      (byte) 73,
      (byte) 188,
      (byte) 202,
      (byte) 35,
      (byte) 47,
      (byte) 62,
      (byte) 160 /*0xA0*/,
      (byte) 147,
      (byte) 38,
      (byte) 179,
      (byte) 243,
      (byte) 39,
      (byte) 25,
      (byte) 126,
      (byte) 46,
      (byte) 37,
      (byte) 153,
      (byte) 119,
      (byte) 197,
      (byte) 9,
      (byte) 47,
      (byte) 93,
      (byte) 71,
      (byte) 183,
      (byte) 106,
      (byte) 235,
      (byte) 100,
      (byte) 38,
      (byte) 236,
      (byte) 144 /*0x90*/,
      (byte) 117,
      (byte) 185,
      (byte) 32 /*0x20*/,
      (byte) 12,
      (byte) 59,
      (byte) 171,
      (byte) 216,
      (byte) 4,
      (byte) 1,
      (byte) 143,
      (byte) 207,
      (byte) 234,
      (byte) 178,
      (byte) 7,
      (byte) 57,
      (byte) 133,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12283()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 105,
        (byte) 144 /*0x90*/,
        (byte) 105,
        (byte) 220,
        (byte) 120,
        (byte) 123,
        (byte) 126,
        (byte) 180,
        (byte) 26,
        (byte) 103,
        (byte) 58,
        (byte) 88,
        (byte) 211,
        (byte) 33,
        (byte) 1,
        (byte) 133,
        (byte) 10,
        (byte) 215,
        (byte) 31 /*0x1F*/,
        (byte) 113,
        (byte) 140,
        (byte) 168,
        (byte) 200,
        (byte) 110,
        (byte) 56
      };
      byte[] numArray3 = new byte[25];
      numArray3[8] = (byte) 78;
      numArray3[1] = (byte) 51;
      numArray3[13] = (byte) 1;
      numArray3[3] = (byte) 181;
      numArray3[4] = (byte) 18;
      numArray3[22] = (byte) 48 /*0x30*/;
      numArray3[5] = (byte) 85;
      numArray3[10] = (byte) 195;
      numArray3[2] = (byte) 188;
      numArray3[9] = (byte) 68;
      numArray3[19] = (byte) 217;
      numArray3[11] = (byte) 180;
      numArray3[0] = (byte) 161;
      numArray3[18] = (byte) 186;
      numArray3[7] = (byte) 132;
      numArray3[15] = (byte) 111;
      numArray3[16 /*0x10*/] = (byte) 222;
      numArray3[17] = (byte) 77;
      numArray3[6] = (byte) 208 /*0xD0*/;
      numArray3[24] = (byte) 115;
      numArray3[14] = (byte) 167;
      numArray3[21] = (byte) 32 /*0x20*/;
      numArray3[20] = (byte) 46;
      numArray3[23] = (byte) 51;
      numArray3[12] = (byte) 217;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_12269.sspq, 109, (Array) numArray4, 0, 38);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12269.sspr, 109, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[25];
    byte[] numArray6 = new byte[25];
    numArray6[12] = (byte) 101;
    numArray6[16 /*0x10*/] = (byte) 151;
    numArray6[4] = (byte) 231;
    numArray6[3] = (byte) 210;
    numArray6[23] = (byte) 136;
    numArray6[2] = (byte) 159;
    numArray6[20] = (byte) 185;
    numArray6[7] = (byte) 105;
    numArray6[6] = (byte) 35;
    numArray6[9] = (byte) 13;
    numArray6[0] = (byte) 3;
    numArray6[11] = (byte) 240 /*0xF0*/;
    numArray6[17] = (byte) 131;
    numArray6[8] = (byte) 240 /*0xF0*/;
    numArray6[19] = (byte) 232;
    numArray6[13] = (byte) 249;
    numArray6[1] = (byte) 146;
    numArray6[18] = (byte) 218;
    numArray6[10] = (byte) 62;
    numArray6[14] = (byte) 33;
    numArray6[15] = (byte) 167;
    numArray6[21] = (byte) 225;
    numArray6[22] = (byte) 70;
    numArray6[5] = (byte) 201;
    numArray6[24] = (byte) 84;
    byte[] numArray7 = new byte[25];
    numArray7[19] = (byte) 156;
    numArray7[1] = (byte) 30;
    numArray7[9] = (byte) 103;
    numArray7[3] = (byte) 94;
    numArray7[12] = (byte) 136;
    numArray7[17] = (byte) 184;
    numArray7[6] = (byte) 85;
    numArray7[7] = (byte) 33;
    numArray7[16 /*0x10*/] = (byte) 175;
    numArray7[4] = (byte) 104;
    numArray7[10] = (byte) 86;
    numArray7[11] = (byte) 179;
    numArray7[23] = (byte) 62;
    numArray7[13] = (byte) 110;
    numArray7[14] = (byte) 254;
    numArray7[15] = (byte) 64 /*0x40*/;
    numArray7[22] = (byte) 174;
    numArray7[0] = (byte) 105;
    numArray7[18] = (byte) 130;
    numArray7[2] = (byte) 107;
    numArray7[20] = (byte) 159;
    numArray7[21] = (byte) 57;
    numArray7[5] = (byte) 234;
    numArray7[8] = (byte) 97;
    numArray7[24] = (byte) 48 /*0x30*/;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12284()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 129,
        (byte) 102,
        (byte) 99,
        (byte) 190,
        (byte) 190,
        (byte) 181,
        (byte) 155
      };
      byte[] numArray3 = new byte[7]
      {
        (byte) 230,
        (byte) 209,
        (byte) 55,
        (byte) 55,
        (byte) 54,
        (byte) 106,
        (byte) 171
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_12269.sspq, 147, (Array) numArray4, 0, 12);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12269.sspr, 147, (Array) numArray4, 0, 12);
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
    byte[] numArray5 = new byte[7];
    byte[] numArray6 = new byte[7]
    {
      (byte) 51,
      (byte) 172,
      (byte) 222,
      (byte) 57,
      (byte) 97,
      (byte) 22,
      (byte) 111
    };
    byte[] numArray7 = new byte[7]
    {
      (byte) 137,
      (byte) 10,
      (byte) 8,
      (byte) 253,
      (byte) 225,
      (byte) 20,
      (byte) 23
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12285()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 0,
        (byte) 0,
        (byte) 53
      };
      numArray2[0] = (byte) 251;
      numArray2[1] = (byte) 220;
      byte[] numArray3 = new byte[3]
      {
        (byte) 19,
        (byte) 121,
        (byte) 54
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[3];
    byte[] numArray5 = new byte[3]
    {
      (byte) 84,
      (byte) 92,
      (byte) 184
    };
    byte[] numArray6 = new byte[3]
    {
      (byte) 192 /*0xC0*/,
      (byte) 38,
      (byte) 40
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12286()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[61];
      byte[] numArray2 = new byte[55];
      numArray2[33] = (byte) 222;
      numArray2[1] = (byte) 8;
      numArray2[28] = (byte) 214;
      numArray2[22] = (byte) 155;
      numArray2[4] = (byte) 60;
      numArray2[46] = (byte) 229;
      numArray2[26] = (byte) 219;
      numArray2[15] = (byte) 41;
      numArray2[8] = (byte) 18;
      numArray2[45] = (byte) 45;
      numArray2[11] = (byte) 57;
      numArray2[32 /*0x20*/] = (byte) 135;
      numArray2[6] = (byte) 79;
      numArray2[13] = (byte) 31 /*0x1F*/;
      numArray2[14] = (byte) 72;
      numArray2[44] = (byte) 237;
      numArray2[16 /*0x10*/] = (byte) 3;
      numArray2[17] = (byte) 240 /*0xF0*/;
      numArray2[18] = (byte) 59;
      numArray2[19] = (byte) 87;
      numArray2[20] = (byte) 79;
      numArray2[21] = (byte) 149;
      numArray2[34] = (byte) 11;
      numArray2[23] = (byte) 75;
      numArray2[2] = (byte) 90;
      numArray2[7] = (byte) 47;
      numArray2[53] = (byte) 131;
      numArray2[38] = (byte) 147;
      numArray2[54] = (byte) 232;
      numArray2[29] = (byte) 243;
      numArray2[30] = (byte) 207;
      numArray2[9] = (byte) 223;
      numArray2[27] = (byte) 194;
      numArray2[10] = (byte) 123;
      numArray2[52] = (byte) 111;
      numArray2[42] = (byte) 33;
      numArray2[36] = (byte) 185;
      numArray2[37] = (byte) 203;
      numArray2[35] = (byte) 128 /*0x80*/;
      numArray2[39] = (byte) 137;
      numArray2[40] = (byte) 140;
      numArray2[12] = (byte) 30;
      numArray2[47] = (byte) 20;
      numArray2[43] = (byte) 95;
      numArray2[25] = (byte) 6;
      numArray2[24] = (byte) 154;
      numArray2[51] = (byte) 31 /*0x1F*/;
      numArray2[41] = (byte) 5;
      numArray2[31 /*0x1F*/] = (byte) 142;
      numArray2[49] = (byte) 51;
      numArray2[50] = (byte) 180;
      numArray2[0] = (byte) 100;
      numArray2[48 /*0x30*/] = (byte) 12;
      numArray2[5] = (byte) 113;
      numArray2[3] = (byte) 63 /*0x3F*/;
      byte[] numArray3 = new byte[55]
      {
        (byte) 51,
        (byte) 190,
        (byte) 240 /*0xF0*/,
        (byte) 13,
        (byte) 23,
        (byte) 2,
        (byte) 97,
        (byte) 228,
        (byte) 74,
        (byte) 32 /*0x20*/,
        (byte) 129,
        (byte) 26,
        (byte) 164,
        (byte) 212,
        (byte) 223,
        (byte) 240 /*0xF0*/,
        (byte) 171,
        (byte) 115,
        (byte) 245,
        (byte) 151,
        (byte) 105,
        (byte) 110,
        (byte) 55,
        (byte) 170,
        (byte) 80 /*0x50*/,
        (byte) 74,
        (byte) 24,
        (byte) 20,
        (byte) 120,
        (byte) 82,
        (byte) 167,
        (byte) 46,
        (byte) 229,
        (byte) 15,
        (byte) 189,
        (byte) 97,
        (byte) 48 /*0x30*/,
        (byte) 16 /*0x10*/,
        (byte) 86,
        (byte) 184,
        (byte) 197,
        (byte) 228,
        (byte) 87,
        (byte) 189,
        (byte) 21,
        (byte) 134,
        (byte) 2,
        (byte) 229,
        (byte) 31 /*0x1F*/,
        (byte) 82,
        (byte) 179,
        (byte) 68,
        (byte) 36,
        (byte) 75,
        (byte) 196
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[6];
      numArray4[5] = (byte) 181;
      numArray4[0] = (byte) 177;
      numArray4[2] = (byte) 20;
      numArray4[1] = (byte) 55;
      numArray4[3] = (byte) 207;
      numArray4[4] = (byte) 215;
      byte[] numArray5 = new byte[6]
      {
        (byte) 116,
        (byte) 166,
        (byte) 63 /*0x3F*/,
        (byte) 139,
        (byte) 141,
        (byte) 62
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[61];
    byte[] numArray7 = new byte[55];
    numArray7[7] = (byte) 241;
    numArray7[44] = (byte) 101;
    numArray7[16 /*0x10*/] = (byte) 87;
    numArray7[31 /*0x1F*/] = (byte) 143;
    numArray7[49] = (byte) 99;
    numArray7[5] = (byte) 237;
    numArray7[6] = (byte) 93;
    numArray7[26] = (byte) 239;
    numArray7[8] = (byte) 124;
    numArray7[30] = (byte) 41;
    numArray7[32 /*0x20*/] = (byte) 252;
    numArray7[18] = (byte) 226;
    numArray7[46] = (byte) 27;
    numArray7[10] = (byte) 113;
    numArray7[11] = (byte) 224 /*0xE0*/;
    numArray7[14] = (byte) 96 /*0x60*/;
    numArray7[25] = (byte) 36;
    numArray7[17] = (byte) 226;
    numArray7[0] = (byte) 254;
    numArray7[19] = (byte) 248;
    numArray7[20] = (byte) 239;
    numArray7[21] = (byte) 73;
    numArray7[22] = (byte) 36;
    numArray7[23] = (byte) 40;
    numArray7[24] = (byte) 247;
    numArray7[1] = (byte) 189;
    numArray7[41] = (byte) 70;
    numArray7[27] = (byte) 50;
    numArray7[38] = (byte) 233;
    numArray7[29] = (byte) 92;
    numArray7[28] = (byte) 205;
    numArray7[34] = (byte) 196;
    numArray7[15] = (byte) 74;
    numArray7[33] = (byte) 106;
    numArray7[45] = (byte) 212;
    numArray7[4] = (byte) 113;
    numArray7[50] = (byte) 241;
    numArray7[37] = (byte) 1;
    numArray7[47] = (byte) 114;
    numArray7[39] = (byte) 66;
    numArray7[51] = (byte) 244;
    numArray7[9] = (byte) 198;
    numArray7[36] = (byte) 237;
    numArray7[43] = (byte) 8;
    numArray7[40] = (byte) 84;
    numArray7[3] = (byte) 156;
    numArray7[12] = (byte) 195;
    numArray7[48 /*0x30*/] = (byte) 110;
    numArray7[42] = (byte) 151;
    numArray7[2] = (byte) 203;
    numArray7[35] = (byte) 0;
    numArray7[13] = (byte) 237;
    numArray7[52] = (byte) 210;
    numArray7[53] = (byte) 206;
    numArray7[54] = (byte) 34;
    byte[] numArray8 = new byte[55]
    {
      (byte) 29,
      (byte) 201,
      (byte) 189,
      (byte) 121,
      (byte) 122,
      (byte) 1,
      (byte) 114,
      (byte) 244,
      (byte) 169,
      (byte) 231,
      (byte) 18,
      (byte) 219,
      (byte) 82,
      (byte) 30,
      (byte) 110,
      (byte) 122,
      (byte) 89,
      (byte) 185,
      (byte) 1,
      (byte) 46,
      (byte) 44,
      (byte) 175,
      (byte) 140,
      (byte) 185,
      (byte) 98,
      (byte) 65,
      (byte) 76,
      (byte) 233,
      (byte) 41,
      (byte) 22,
      (byte) 168,
      (byte) 51,
      (byte) 21,
      (byte) 9,
      (byte) 72,
      (byte) 104,
      (byte) 172,
      (byte) 212,
      (byte) 222,
      (byte) 183,
      (byte) 240 /*0xF0*/,
      (byte) 87,
      (byte) 72,
      (byte) 82,
      (byte) 118,
      (byte) 88,
      (byte) 220,
      (byte) 109,
      (byte) 234,
      (byte) 15,
      (byte) 61,
      (byte) 58,
      (byte) 50,
      (byte) 120,
      (byte) 132
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[6]
    {
      (byte) 223,
      (byte) 129,
      (byte) 67,
      (byte) 130,
      (byte) 102,
      (byte) 186
    };
    byte[] numArray10 = new byte[6]
    {
      (byte) 199,
      (byte) 199,
      (byte) 161,
      (byte) 189,
      (byte) 254,
      (byte) 42
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 6);
    for (int index = 0; index < 6; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12287()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[62];
      byte[] numArray2 = new byte[55]
      {
        (byte) 17,
        (byte) 25,
        (byte) 248,
        (byte) 18,
        (byte) 249,
        (byte) 91,
        (byte) 91,
        (byte) 122,
        (byte) 134,
        (byte) 58,
        (byte) 176 /*0xB0*/,
        (byte) 64 /*0x40*/,
        (byte) 50,
        (byte) 71,
        (byte) 49,
        (byte) 181,
        (byte) 98,
        (byte) 208 /*0xD0*/,
        (byte) 58,
        (byte) 138,
        (byte) 4,
        (byte) 206,
        (byte) 13,
        (byte) 234,
        (byte) 106,
        (byte) 219,
        (byte) 89,
        (byte) 193,
        (byte) 195,
        (byte) 104,
        (byte) 195,
        (byte) 4,
        (byte) 93,
        (byte) 58,
        (byte) 175,
        (byte) 49,
        (byte) 22,
        (byte) 245,
        (byte) 162,
        (byte) 159,
        (byte) 214,
        (byte) 175,
        (byte) 243,
        (byte) 200,
        (byte) 204,
        (byte) 159,
        (byte) 79,
        (byte) 252,
        (byte) 80 /*0x50*/,
        (byte) 176 /*0xB0*/,
        (byte) 88,
        (byte) 228,
        (byte) 45,
        (byte) 154,
        (byte) 177
      };
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 93;
      numArray3[12] = (byte) 244;
      numArray3[2] = (byte) 32 /*0x20*/;
      numArray3[14] = (byte) 171;
      numArray3[47] = (byte) 243;
      numArray3[11] = (byte) 250;
      numArray3[6] = (byte) 38;
      numArray3[49] = (byte) 52;
      numArray3[8] = (byte) 129;
      numArray3[9] = (byte) 20;
      numArray3[34] = (byte) 107;
      numArray3[35] = (byte) 56;
      numArray3[24] = (byte) 104;
      numArray3[44] = (byte) 114;
      numArray3[19] = (byte) 105;
      numArray3[15] = (byte) 57;
      numArray3[0] = (byte) 138;
      numArray3[17] = (byte) 182;
      numArray3[18] = (byte) 84;
      numArray3[42] = (byte) 60;
      numArray3[20] = (byte) 142;
      numArray3[53] = (byte) 155;
      numArray3[21] = (byte) 100;
      numArray3[43] = (byte) 80 /*0x50*/;
      numArray3[5] = (byte) 199;
      numArray3[54] = (byte) 12;
      numArray3[26] = (byte) 2;
      numArray3[27] = (byte) 28;
      numArray3[7] = (byte) 175;
      numArray3[29] = (byte) 115;
      numArray3[30] = (byte) 144 /*0x90*/;
      numArray3[3] = (byte) 19;
      numArray3[32 /*0x20*/] = (byte) 62;
      numArray3[33] = (byte) 37;
      numArray3[16 /*0x10*/] = (byte) 44;
      numArray3[28] = (byte) 48 /*0x30*/;
      numArray3[36] = (byte) 177;
      numArray3[37] = (byte) 26;
      numArray3[38] = (byte) 29;
      numArray3[39] = (byte) 183;
      numArray3[40] = (byte) 133;
      numArray3[25] = (byte) 9;
      numArray3[31 /*0x1F*/] = (byte) 130;
      numArray3[13] = (byte) 194;
      numArray3[41] = (byte) 209;
      numArray3[45] = (byte) 189;
      numArray3[50] = (byte) 124;
      numArray3[4] = (byte) 98;
      numArray3[52] = (byte) 184;
      numArray3[10] = (byte) 20;
      numArray3[1] = (byte) 132;
      numArray3[46] = (byte) 64 /*0x40*/;
      numArray3[22] = (byte) 53;
      numArray3[23] = (byte) 71;
      numArray3[48 /*0x30*/] = (byte) 240 /*0xF0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[7];
      numArray4[1] = (byte) 219;
      numArray4[0] = (byte) 240 /*0xF0*/;
      numArray4[4] = (byte) 40;
      numArray4[3] = (byte) 166;
      numArray4[5] = (byte) 164;
      numArray4[2] = (byte) 146;
      numArray4[6] = (byte) 14;
      byte[] numArray5 = new byte[7]
      {
        (byte) 17,
        (byte) 142,
        (byte) 180,
        (byte) 40,
        (byte) 131,
        (byte) 177,
        (byte) 178
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[62];
    byte[] numArray7 = new byte[55]
    {
      (byte) 119,
      (byte) 91,
      (byte) 5,
      (byte) 243,
      (byte) 252,
      (byte) 14,
      (byte) 51,
      (byte) 248,
      (byte) 33,
      (byte) 0,
      (byte) 36,
      (byte) 40,
      (byte) 68,
      (byte) 220,
      (byte) 43,
      (byte) 4,
      (byte) 94,
      (byte) 23,
      (byte) 114,
      (byte) 241,
      (byte) 246,
      (byte) 20,
      (byte) 84,
      (byte) 198,
      (byte) 185,
      (byte) 76,
      (byte) 157,
      (byte) 233,
      (byte) 57,
      (byte) 116,
      (byte) 123,
      (byte) 204,
      (byte) 11,
      (byte) 148,
      (byte) 110,
      (byte) 1,
      (byte) 98,
      (byte) 211,
      (byte) 244,
      (byte) 205,
      (byte) 95,
      (byte) 173,
      (byte) 7,
      (byte) 163,
      (byte) 233,
      (byte) 111,
      (byte) 143,
      (byte) 66,
      (byte) 66,
      (byte) 38,
      (byte) 124,
      (byte) 127 /*0x7F*/,
      (byte) 172,
      (byte) 184,
      (byte) 7
    };
    byte[] numArray8 = new byte[55];
    numArray8[43] = (byte) 52;
    numArray8[1] = (byte) 191;
    numArray8[34] = (byte) 109;
    numArray8[3] = (byte) 132;
    numArray8[4] = (byte) 214;
    numArray8[5] = (byte) 15;
    numArray8[6] = (byte) 41;
    numArray8[7] = (byte) 153;
    numArray8[27] = (byte) 50;
    numArray8[32 /*0x20*/] = (byte) 83;
    numArray8[28] = (byte) 54;
    numArray8[11] = (byte) 100;
    numArray8[12] = (byte) 59;
    numArray8[51] = (byte) 196;
    numArray8[14] = (byte) 85;
    numArray8[15] = (byte) 83;
    numArray8[52] = (byte) 46;
    numArray8[17] = (byte) 187;
    numArray8[31 /*0x1F*/] = (byte) 250;
    numArray8[46] = (byte) 199;
    numArray8[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray8[25] = (byte) 192 /*0xC0*/;
    numArray8[33] = (byte) 1;
    numArray8[13] = (byte) 146;
    numArray8[48 /*0x30*/] = (byte) 28;
    numArray8[36] = (byte) 195;
    numArray8[26] = (byte) 200;
    numArray8[19] = (byte) 12;
    numArray8[23] = (byte) 209;
    numArray8[30] = (byte) 31 /*0x1F*/;
    numArray8[29] = (byte) 242;
    numArray8[53] = (byte) 194;
    numArray8[45] = (byte) 68;
    numArray8[21] = (byte) 59;
    numArray8[54] = (byte) 117;
    numArray8[35] = (byte) 189;
    numArray8[10] = (byte) 129;
    numArray8[37] = (byte) 72;
    numArray8[38] = (byte) 195;
    numArray8[39] = (byte) 32 /*0x20*/;
    numArray8[40] = (byte) 130;
    numArray8[41] = (byte) 156;
    numArray8[42] = (byte) 17;
    numArray8[0] = (byte) 95;
    numArray8[44] = (byte) 225;
    numArray8[9] = (byte) 125;
    numArray8[50] = (byte) 209;
    numArray8[47] = (byte) 29;
    numArray8[20] = (byte) 49;
    numArray8[2] = (byte) 170;
    numArray8[8] = (byte) 78;
    numArray8[22] = (byte) 126;
    numArray8[24] = (byte) 131;
    numArray8[49] = (byte) 205;
    numArray8[18] = (byte) 48 /*0x30*/;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[7]
    {
      (byte) 97,
      (byte) 239,
      (byte) 243,
      (byte) 23,
      (byte) 161,
      (byte) 170,
      (byte) 181
    };
    byte[] numArray10 = new byte[7]
    {
      (byte) 222,
      (byte) 166,
      (byte) 136,
      (byte) 239,
      (byte) 12,
      (byte) 210,
      (byte) 114
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 7);
    for (int index = 0; index < 7; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12288()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[61];
      byte[] numArray2 = new byte[55];
      numArray2[12] = (byte) 221;
      numArray2[35] = (byte) 233;
      numArray2[13] = (byte) 133;
      numArray2[34] = (byte) 114;
      numArray2[4] = (byte) 222;
      numArray2[29] = (byte) 164;
      numArray2[48 /*0x30*/] = (byte) 18;
      numArray2[7] = (byte) 89;
      numArray2[8] = (byte) 178;
      numArray2[9] = (byte) 146;
      numArray2[10] = (byte) 75;
      numArray2[2] = (byte) 125;
      numArray2[33] = (byte) 187;
      numArray2[21] = (byte) 180;
      numArray2[52] = (byte) 33;
      numArray2[15] = (byte) 143;
      numArray2[16 /*0x10*/] = (byte) 5;
      numArray2[17] = (byte) 159;
      numArray2[42] = (byte) 164;
      numArray2[43] = (byte) 195;
      numArray2[20] = (byte) 206;
      numArray2[37] = (byte) 0;
      numArray2[11] = (byte) 248;
      numArray2[23] = (byte) 194;
      numArray2[22] = (byte) 164;
      numArray2[25] = (byte) 80 /*0x50*/;
      numArray2[3] = (byte) 241;
      numArray2[26] = (byte) 68;
      numArray2[28] = (byte) 68;
      numArray2[14] = (byte) 142;
      numArray2[6] = (byte) 123;
      numArray2[31 /*0x1F*/] = (byte) 88;
      numArray2[32 /*0x20*/] = (byte) 136;
      numArray2[50] = (byte) 176 /*0xB0*/;
      numArray2[51] = (byte) 233;
      numArray2[18] = (byte) 23;
      numArray2[24] = (byte) 170;
      numArray2[36] = (byte) 6;
      numArray2[38] = (byte) 125;
      numArray2[39] = (byte) 189;
      numArray2[40] = (byte) 170;
      numArray2[46] = (byte) 22;
      numArray2[27] = (byte) 116;
      numArray2[0] = (byte) 98;
      numArray2[44] = (byte) 22;
      numArray2[45] = (byte) 246;
      numArray2[5] = (byte) 73;
      numArray2[47] = (byte) 146;
      numArray2[41] = (byte) 101;
      numArray2[49] = (byte) 27;
      numArray2[1] = (byte) 47;
      numArray2[30] = (byte) 149;
      numArray2[19] = (byte) 44;
      numArray2[53] = (byte) 156;
      numArray2[54] = (byte) 85;
      byte[] numArray3 = new byte[55];
      numArray3[48 /*0x30*/] = (byte) 115;
      numArray3[40] = (byte) 129;
      numArray3[53] = (byte) 43;
      numArray3[32 /*0x20*/] = (byte) 175;
      numArray3[43] = (byte) 49;
      numArray3[49] = (byte) 244;
      numArray3[28] = (byte) 125;
      numArray3[3] = (byte) 20;
      numArray3[8] = (byte) 102;
      numArray3[9] = (byte) 16 /*0x10*/;
      numArray3[37] = (byte) 224 /*0xE0*/;
      numArray3[11] = (byte) 155;
      numArray3[1] = (byte) 128 /*0x80*/;
      numArray3[14] = (byte) 72;
      numArray3[30] = (byte) 210;
      numArray3[15] = (byte) 73;
      numArray3[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray3[44] = (byte) 173;
      numArray3[18] = (byte) 62;
      numArray3[7] = (byte) 254;
      numArray3[13] = (byte) 105;
      numArray3[21] = (byte) 144 /*0x90*/;
      numArray3[22] = (byte) 70;
      numArray3[23] = (byte) 142;
      numArray3[38] = (byte) 56;
      numArray3[25] = (byte) 62;
      numArray3[17] = (byte) 27;
      numArray3[26] = (byte) 50;
      numArray3[12] = (byte) 169;
      numArray3[47] = (byte) 121;
      numArray3[19] = (byte) 224 /*0xE0*/;
      numArray3[31 /*0x1F*/] = (byte) 137;
      numArray3[41] = (byte) 10;
      numArray3[33] = (byte) 20;
      numArray3[2] = (byte) 157;
      numArray3[35] = (byte) 74;
      numArray3[36] = (byte) 205;
      numArray3[39] = (byte) 253;
      numArray3[34] = (byte) 221;
      numArray3[0] = (byte) 253;
      numArray3[20] = (byte) 99;
      numArray3[24] = (byte) 72;
      numArray3[42] = (byte) 241;
      numArray3[46] = (byte) 139;
      numArray3[29] = (byte) 112 /*0x70*/;
      numArray3[45] = (byte) 64 /*0x40*/;
      numArray3[27] = (byte) 158;
      numArray3[4] = (byte) 79;
      numArray3[10] = (byte) 165;
      numArray3[5] = (byte) 217;
      numArray3[50] = (byte) 58;
      numArray3[51] = (byte) 115;
      numArray3[52] = (byte) 187;
      numArray3[6] = (byte) 231;
      numArray3[54] = (byte) 248;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[6]
      {
        (byte) 135,
        (byte) 103,
        (byte) 14,
        (byte) 32 /*0x20*/,
        (byte) 178,
        (byte) 85
      };
      byte[] numArray5 = new byte[6]
      {
        (byte) 0,
        (byte) 239,
        (byte) 0,
        (byte) 0,
        (byte) 171,
        (byte) 0
      };
      numArray5[2] = (byte) 215;
      numArray5[0] = (byte) 146;
      numArray5[5] = (byte) 226;
      numArray5[3] = (byte) 232;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[61];
    byte[] numArray7 = new byte[55];
    numArray7[5] = (byte) 165;
    numArray7[22] = (byte) 68;
    numArray7[49] = (byte) 26;
    numArray7[47] = (byte) 33;
    numArray7[4] = (byte) 249;
    numArray7[53] = (byte) 78;
    numArray7[0] = (byte) 151;
    numArray7[7] = (byte) 41;
    numArray7[29] = (byte) 245;
    numArray7[9] = (byte) 2;
    numArray7[45] = (byte) 20;
    numArray7[52] = (byte) 102;
    numArray7[12] = (byte) 233;
    numArray7[39] = (byte) 232;
    numArray7[14] = (byte) 72;
    numArray7[43] = (byte) 29;
    numArray7[31 /*0x1F*/] = (byte) 152;
    numArray7[21] = (byte) 139;
    numArray7[18] = (byte) 40;
    numArray7[13] = (byte) 77;
    numArray7[20] = (byte) 191;
    numArray7[46] = (byte) 229;
    numArray7[48 /*0x30*/] = (byte) 4;
    numArray7[23] = (byte) 75;
    numArray7[24] = (byte) 14;
    numArray7[3] = (byte) 181;
    numArray7[26] = (byte) 6;
    numArray7[27] = (byte) 25;
    numArray7[15] = (byte) 151;
    numArray7[37] = (byte) 30;
    numArray7[35] = (byte) 233;
    numArray7[10] = (byte) 204;
    numArray7[30] = (byte) 8;
    numArray7[25] = (byte) 175;
    numArray7[41] = (byte) 61;
    numArray7[28] = (byte) 81;
    numArray7[36] = (byte) 88;
    numArray7[2] = (byte) 226;
    numArray7[38] = (byte) 44;
    numArray7[33] = (byte) 59;
    numArray7[40] = (byte) 101;
    numArray7[19] = (byte) 180;
    numArray7[42] = (byte) 1;
    numArray7[8] = (byte) 13;
    numArray7[44] = (byte) 229;
    numArray7[32 /*0x20*/] = (byte) 81;
    numArray7[11] = (byte) 65;
    numArray7[17] = (byte) 0;
    numArray7[6] = (byte) 33;
    numArray7[50] = (byte) 156;
    numArray7[51] = (byte) 39;
    numArray7[1] = (byte) 60;
    numArray7[34] = (byte) 254;
    numArray7[16 /*0x10*/] = (byte) 166;
    numArray7[54] = (byte) 101;
    byte[] numArray8 = new byte[55]
    {
      (byte) 69,
      (byte) 30,
      (byte) 52,
      byte.MaxValue,
      (byte) 17,
      (byte) 241,
      (byte) 102,
      (byte) 204,
      (byte) 152,
      (byte) 5,
      (byte) 50,
      (byte) 240 /*0xF0*/,
      (byte) 84,
      (byte) 162,
      (byte) 203,
      (byte) 247,
      (byte) 113,
      (byte) 126,
      (byte) 19,
      (byte) 125,
      (byte) 142,
      (byte) 160 /*0xA0*/,
      (byte) 252,
      (byte) 24,
      (byte) 6,
      (byte) 153,
      (byte) 175,
      (byte) 128 /*0x80*/,
      (byte) 53,
      (byte) 135,
      (byte) 90,
      (byte) 177,
      (byte) 59,
      (byte) 47,
      (byte) 146,
      (byte) 220,
      (byte) 8,
      (byte) 149,
      (byte) 23,
      (byte) 240 /*0xF0*/,
      (byte) 252,
      (byte) 223,
      (byte) 174,
      (byte) 253,
      (byte) 14,
      (byte) 2,
      (byte) 190,
      (byte) 155,
      (byte) 249,
      (byte) 249,
      (byte) 173,
      (byte) 28,
      (byte) 75,
      (byte) 221,
      (byte) 128 /*0x80*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[6]
    {
      (byte) 50,
      (byte) 173,
      (byte) 127 /*0x7F*/,
      (byte) 186,
      (byte) 244,
      (byte) 161
    };
    byte[] numArray10 = new byte[6]
    {
      (byte) 0,
      (byte) 55,
      (byte) 159,
      (byte) 235,
      (byte) 153,
      (byte) 183
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 6);
    for (int index = 0; index < 6; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12289()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 216,
        (byte) 92,
        (byte) 108,
        (byte) 126,
        (byte) 16 /*0x10*/,
        (byte) 242,
        (byte) 247,
        (byte) 242,
        (byte) 28,
        (byte) 161
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 186,
        (byte) 22,
        (byte) 19,
        (byte) 181,
        (byte) 197,
        (byte) 36,
        (byte) 211,
        (byte) 194,
        (byte) 114,
        (byte) 216
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 146,
      (byte) 18,
      (byte) 110,
      (byte) 91,
      (byte) 68,
      (byte) 143,
      (byte) 10,
      (byte) 2,
      (byte) 240 /*0xF0*/,
      (byte) 113
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 132,
      (byte) 176 /*0xB0*/,
      (byte) 49,
      (byte) 19,
      (byte) 62,
      (byte) 180,
      (byte) 2,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 103
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12290()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[61];
      byte[] numArray2 = new byte[55]
      {
        (byte) 141,
        (byte) 103,
        (byte) 195,
        (byte) 41,
        (byte) 10,
        (byte) 176 /*0xB0*/,
        (byte) 54,
        (byte) 95,
        (byte) 118,
        (byte) 57,
        (byte) 92,
        (byte) 3,
        (byte) 95,
        (byte) 47,
        (byte) 13,
        (byte) 200,
        (byte) 223,
        (byte) 203,
        (byte) 127 /*0x7F*/,
        (byte) 215,
        (byte) 237,
        (byte) 168,
        (byte) 134,
        (byte) 69,
        (byte) 123,
        (byte) 137,
        (byte) 246,
        (byte) 234,
        (byte) 231,
        (byte) 234,
        (byte) 74,
        (byte) 240 /*0xF0*/,
        (byte) 70,
        (byte) 143,
        (byte) 105,
        (byte) 138,
        (byte) 88,
        (byte) 120,
        (byte) 250,
        (byte) 1,
        (byte) 158,
        (byte) 50,
        (byte) 155,
        (byte) 52,
        byte.MaxValue,
        (byte) 64 /*0x40*/,
        (byte) 47,
        (byte) 218,
        (byte) 245,
        (byte) 40,
        (byte) 146,
        (byte) 86,
        (byte) 14,
        (byte) 242,
        (byte) 117
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 129,
        (byte) 252,
        (byte) 29,
        (byte) 51,
        (byte) 109,
        (byte) 248,
        (byte) 117,
        (byte) 38,
        (byte) 93,
        (byte) 114,
        (byte) 153,
        (byte) 111,
        (byte) 44,
        (byte) 120,
        (byte) 60,
        (byte) 234,
        (byte) 200,
        (byte) 16 /*0x10*/,
        (byte) 225,
        (byte) 249,
        (byte) 48 /*0x30*/,
        (byte) 198,
        (byte) 245,
        (byte) 123,
        (byte) 17,
        (byte) 10,
        (byte) 33,
        (byte) 92,
        (byte) 105,
        (byte) 157,
        (byte) 120,
        (byte) 187,
        (byte) 230,
        (byte) 135,
        (byte) 160 /*0xA0*/,
        (byte) 83,
        (byte) 224 /*0xE0*/,
        (byte) 170,
        (byte) 182,
        (byte) 246,
        (byte) 123,
        (byte) 37,
        (byte) 129,
        byte.MaxValue,
        (byte) 115,
        (byte) 208 /*0xD0*/,
        (byte) 173,
        (byte) 75,
        (byte) 142,
        (byte) 55,
        (byte) 234,
        (byte) 8,
        (byte) 137,
        (byte) 211,
        (byte) 46
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[6]
      {
        (byte) 126,
        (byte) 174,
        (byte) 128 /*0x80*/,
        (byte) 186,
        (byte) 75,
        (byte) 119
      };
      byte[] numArray5 = new byte[6];
      numArray5[2] = (byte) 156;
      numArray5[0] = (byte) 112 /*0x70*/;
      numArray5[1] = (byte) 43;
      numArray5[3] = (byte) 75;
      numArray5[4] = (byte) 45;
      numArray5[5] = (byte) 111;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_12269.sspq, 159, (Array) numArray6, 0, 20);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12269.sspr, 159, (Array) numArray6, 0, 20);
      for (int index = 0; index < numArray6.Length; ++index)
      {
        if ((int) numArray6[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray7 = new byte[61];
    byte[] numArray8 = new byte[55];
    numArray8[9] = (byte) 20;
    numArray8[27] = (byte) 139;
    numArray8[2] = (byte) 82;
    numArray8[45] = (byte) 6;
    numArray8[16 /*0x10*/] = (byte) 187;
    numArray8[21] = (byte) 37;
    numArray8[37] = byte.MaxValue;
    numArray8[49] = (byte) 153;
    numArray8[8] = (byte) 94;
    numArray8[12] = (byte) 43;
    numArray8[10] = (byte) 114;
    numArray8[40] = (byte) 178;
    numArray8[3] = (byte) 187;
    numArray8[39] = (byte) 11;
    numArray8[13] = (byte) 133;
    numArray8[19] = (byte) 56;
    numArray8[35] = (byte) 54;
    numArray8[0] = (byte) 122;
    numArray8[33] = (byte) 156;
    numArray8[28] = (byte) 105;
    numArray8[20] = (byte) 115;
    numArray8[38] = (byte) 252;
    numArray8[22] = (byte) 158;
    numArray8[11] = (byte) 184;
    numArray8[5] = (byte) 69;
    numArray8[25] = (byte) 50;
    numArray8[26] = (byte) 250;
    numArray8[41] = (byte) 183;
    numArray8[6] = (byte) 147;
    numArray8[29] = (byte) 183;
    numArray8[23] = (byte) 86;
    numArray8[47] = (byte) 110;
    numArray8[18] = (byte) 143;
    numArray8[48 /*0x30*/] = (byte) 80 /*0x50*/;
    numArray8[34] = (byte) 87;
    numArray8[54] = (byte) 86;
    numArray8[36] = (byte) 229;
    numArray8[17] = (byte) 216;
    numArray8[14] = (byte) 144 /*0x90*/;
    numArray8[4] = (byte) 16 /*0x10*/;
    numArray8[42] = (byte) 252;
    numArray8[1] = (byte) 71;
    numArray8[43] = (byte) 7;
    numArray8[32 /*0x20*/] = (byte) 150;
    numArray8[44] = (byte) 131;
    numArray8[51] = (byte) 171;
    numArray8[30] = (byte) 66;
    numArray8[31 /*0x1F*/] = (byte) 217;
    numArray8[15] = (byte) 186;
    numArray8[46] = (byte) 45;
    numArray8[50] = (byte) 94;
    numArray8[7] = (byte) 131;
    numArray8[52] = (byte) 25;
    numArray8[53] = (byte) 161;
    numArray8[24] = (byte) 183;
    byte[] numArray9 = new byte[55];
    numArray9[47] = (byte) 186;
    numArray9[1] = (byte) 226;
    numArray9[53] = (byte) 119;
    numArray9[51] = (byte) 176 /*0xB0*/;
    numArray9[4] = (byte) 125;
    numArray9[5] = (byte) 160 /*0xA0*/;
    numArray9[6] = (byte) 207;
    numArray9[7] = (byte) 111;
    numArray9[21] = (byte) 62;
    numArray9[0] = (byte) 252;
    numArray9[48 /*0x30*/] = (byte) 84;
    numArray9[8] = (byte) 105;
    numArray9[12] = (byte) 148;
    numArray9[34] = (byte) 141;
    numArray9[14] = (byte) 62;
    numArray9[15] = (byte) 209;
    numArray9[41] = (byte) 182;
    numArray9[17] = (byte) 51;
    numArray9[20] = (byte) 151;
    numArray9[31 /*0x1F*/] = (byte) 39;
    numArray9[46] = (byte) 125;
    numArray9[42] = (byte) 206;
    numArray9[22] = (byte) 200;
    numArray9[23] = (byte) 71;
    numArray9[24] = (byte) 25;
    numArray9[54] = (byte) 108;
    numArray9[11] = (byte) 165;
    numArray9[52] = (byte) 76;
    numArray9[28] = (byte) 202;
    numArray9[13] = (byte) 110;
    numArray9[30] = (byte) 244;
    numArray9[18] = (byte) 150;
    numArray9[32 /*0x20*/] = (byte) 175;
    numArray9[33] = (byte) 52;
    numArray9[16 /*0x10*/] = (byte) 188;
    numArray9[35] = (byte) 156;
    numArray9[36] = (byte) 31 /*0x1F*/;
    numArray9[50] = (byte) 60;
    numArray9[19] = (byte) 176 /*0xB0*/;
    numArray9[10] = (byte) 227;
    numArray9[26] = (byte) 153;
    numArray9[3] = (byte) 229;
    numArray9[9] = (byte) 18;
    numArray9[27] = (byte) 60;
    numArray9[40] = (byte) 186;
    numArray9[29] = (byte) 19;
    numArray9[44] = (byte) 15;
    numArray9[45] = (byte) 56;
    numArray9[37] = (byte) 142;
    numArray9[43] = (byte) 134;
    numArray9[25] = (byte) 38;
    numArray9[38] = (byte) 226;
    numArray9[39] = (byte) 53;
    numArray9[2] = (byte) 251;
    numArray9[49] = (byte) 135;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[6]
    {
      (byte) 66,
      (byte) 134,
      (byte) 115,
      (byte) 115,
      (byte) 136,
      (byte) 61
    };
    byte[] numArray11 = new byte[6]
    {
      (byte) 253,
      (byte) 122,
      (byte) 126,
      (byte) 163,
      (byte) 12,
      (byte) 225
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 6);
    for (int index = 0; index < 6; ++index)
      numArray7[index + 55] ^= numArray11[index];
    byte[] numArray12 = new byte[53];
    byte[] response1 = new byte[53];
    Array.Copy((Array) sc_12269.sspq, 179, (Array) numArray12, 0, 53);
    key.Query(true, 335, numArray12, response1);
    Array.Copy((Array) sc_12269.sspr, 179, (Array) numArray12, 0, 53);
    for (int index = 0; index < numArray12.Length; ++index)
    {
      if ((int) numArray12[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static int ssp_appserver_12291(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 213,
      (byte) 138,
      (byte) 177,
      (byte) 245,
      (byte) 166,
      (byte) 50,
      (byte) 168,
      (byte) 175,
      (byte) 93,
      (byte) 221,
      (byte) 111,
      (byte) 180,
      (byte) 87,
      (byte) 119,
      (byte) 69,
      (byte) 135,
      (byte) 10,
      (byte) 26,
      (byte) 69,
      (byte) 85,
      (byte) 119,
      (byte) 245,
      (byte) 121,
      (byte) 13,
      (byte) 177,
      (byte) 109,
      (byte) 82,
      (byte) 232,
      (byte) 13,
      (byte) 173,
      (byte) 199,
      (byte) 14,
      (byte) 165,
      (byte) 70,
      (byte) 233,
      (byte) 75,
      (byte) 250,
      (byte) 213,
      (byte) 243,
      (byte) 223,
      (byte) 154,
      (byte) 5,
      (byte) 44,
      (byte) 112 /*0x70*/,
      (byte) 254,
      (byte) 188,
      (byte) 12
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 114;
    sourceArray2[36] = (byte) 24;
    sourceArray2[2] = (byte) 47;
    sourceArray2[3] = (byte) 98;
    sourceArray2[0] = (byte) 186;
    sourceArray2[18] = (byte) 84;
    sourceArray2[19] = (byte) 93;
    sourceArray2[20] = (byte) 198;
    sourceArray2[8] = (byte) 160 /*0xA0*/;
    sourceArray2[1] = (byte) 134;
    sourceArray2[10] = (byte) 31 /*0x1F*/;
    sourceArray2[29] = (byte) 218;
    sourceArray2[12] = (byte) 154;
    sourceArray2[45] = (byte) 109;
    sourceArray2[39] = (byte) 63 /*0x3F*/;
    sourceArray2[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    sourceArray2[27] = (byte) 160 /*0xA0*/;
    sourceArray2[17] = (byte) 64 /*0x40*/;
    sourceArray2[40] = (byte) 196;
    sourceArray2[14] = (byte) 154;
    sourceArray2[25] = (byte) 49;
    sourceArray2[21] = (byte) 184;
    sourceArray2[22] = (byte) 11;
    sourceArray2[23] = (byte) 242;
    sourceArray2[24] = (byte) 85;
    sourceArray2[9] = (byte) 67;
    sourceArray2[38] = (byte) 173;
    sourceArray2[41] = (byte) 209;
    sourceArray2[11] = (byte) 52;
    sourceArray2[35] = (byte) 159;
    sourceArray2[30] = (byte) 82;
    sourceArray2[7] = (byte) 19;
    sourceArray2[4] = (byte) 231;
    sourceArray2[33] = (byte) 166;
    sourceArray2[34] = (byte) 246;
    sourceArray2[43] = (byte) 6;
    sourceArray2[47] = (byte) 131;
    sourceArray2[15] = (byte) 123;
    sourceArray2[5] = (byte) 23;
    sourceArray2[6] = (byte) 91;
    sourceArray2[32 /*0x20*/] = (byte) 217;
    sourceArray2[46] = (byte) 177;
    sourceArray2[42] = (byte) 7;
    sourceArray2[26] = (byte) 210;
    sourceArray2[13] = (byte) 48 /*0x30*/;
    sourceArray2[44] = (byte) 168;
    sourceArray2[37] = (byte) 9;
    sourceArray2[16 /*0x10*/] = (byte) 50;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12292()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[34];
      byte[] numArray2 = new byte[34]
      {
        (byte) 158,
        (byte) 173,
        (byte) 251,
        (byte) 54,
        (byte) 44,
        (byte) 246,
        (byte) 131,
        (byte) 126,
        (byte) 170,
        (byte) 171,
        (byte) 136,
        (byte) 23,
        (byte) 8,
        (byte) 148,
        (byte) 78,
        (byte) 215,
        (byte) 80 /*0x50*/,
        (byte) 253,
        (byte) 23,
        (byte) 186,
        (byte) 243,
        (byte) 97,
        (byte) 208 /*0xD0*/,
        (byte) 195,
        (byte) 132,
        (byte) 191,
        (byte) 53,
        (byte) 163,
        (byte) 108,
        (byte) 101,
        (byte) 225,
        (byte) 13,
        (byte) 75,
        (byte) 174
      };
      byte[] numArray3 = new byte[34]
      {
        (byte) 175,
        (byte) 152,
        (byte) 168,
        (byte) 108,
        (byte) 245,
        (byte) 129,
        (byte) 44,
        (byte) 26,
        (byte) 62,
        (byte) 239,
        (byte) 57,
        (byte) 66,
        (byte) 235,
        (byte) 225,
        (byte) 206,
        (byte) 184,
        (byte) 191,
        (byte) 155,
        (byte) 8,
        (byte) 46,
        (byte) 209,
        (byte) 226,
        (byte) 149,
        (byte) 64 /*0x40*/,
        (byte) 135,
        (byte) 96 /*0x60*/,
        (byte) 171,
        (byte) 90,
        (byte) 82,
        (byte) 25,
        (byte) 163,
        (byte) 185,
        (byte) 238,
        (byte) 82
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[34];
    byte[] numArray5 = new byte[34]
    {
      (byte) 87,
      (byte) 149,
      (byte) 148,
      (byte) 114,
      (byte) 163,
      (byte) 84,
      (byte) 147,
      (byte) 196,
      (byte) 241,
      (byte) 50,
      (byte) 194,
      (byte) 67,
      (byte) 227,
      (byte) 175,
      (byte) 220,
      (byte) 77,
      (byte) 193,
      (byte) 102,
      (byte) 60,
      (byte) 221,
      (byte) 56,
      (byte) 178,
      (byte) 108,
      (byte) 192 /*0xC0*/,
      (byte) 223,
      (byte) 17,
      (byte) 68,
      (byte) 201,
      (byte) 123,
      (byte) 203,
      (byte) 90,
      (byte) 36,
      (byte) 7,
      (byte) 113
    };
    byte[] numArray6 = new byte[34]
    {
      (byte) 242,
      (byte) 15,
      (byte) 148,
      (byte) 181,
      (byte) 110,
      (byte) 213,
      (byte) 70,
      (byte) 76,
      (byte) 65,
      (byte) 127 /*0x7F*/,
      (byte) 143,
      (byte) 209,
      (byte) 101,
      (byte) 238,
      (byte) 7,
      (byte) 61,
      (byte) 71,
      (byte) 170,
      (byte) 204,
      (byte) 181,
      (byte) 16 /*0x10*/,
      (byte) 195,
      (byte) 169,
      (byte) 201,
      (byte) 123,
      (byte) 61,
      (byte) 7,
      (byte) 22,
      (byte) 50,
      (byte) 20,
      (byte) 40,
      (byte) 80 /*0x50*/,
      (byte) 34,
      (byte) 105
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 34);
    for (int index = 0; index < 34; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12293()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 80 /*0x50*/,
        (byte) 13,
        (byte) 61,
        byte.MaxValue,
        (byte) 146,
        (byte) 141,
        (byte) 115,
        (byte) 95,
        (byte) 137,
        (byte) 182,
        (byte) 4,
        (byte) 191,
        (byte) 138,
        (byte) 71,
        (byte) 52,
        (byte) 105,
        (byte) 45,
        (byte) 165,
        (byte) 40,
        (byte) 211,
        (byte) 146,
        (byte) 145,
        (byte) 16 /*0x10*/
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 45,
        (byte) 109,
        (byte) 45,
        (byte) 187,
        (byte) 206,
        (byte) 196,
        (byte) 96 /*0x60*/,
        (byte) 210,
        (byte) 87,
        (byte) 163,
        (byte) 243,
        (byte) 82,
        (byte) 195,
        (byte) 147,
        (byte) 43,
        (byte) 247,
        (byte) 116,
        (byte) 155,
        (byte) 177,
        (byte) 245,
        (byte) 106,
        (byte) 203,
        (byte) 190
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_12269.sspq, 232, (Array) numArray4, 0, 53);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12269.sspr, 232, (Array) numArray4, 0, 53);
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
      (byte) 228,
      (byte) 197,
      (byte) 173,
      (byte) 159,
      (byte) 47,
      (byte) 98,
      (byte) 53,
      (byte) 27,
      (byte) 195,
      (byte) 87,
      (byte) 49,
      (byte) 43,
      (byte) 130,
      (byte) 179,
      (byte) 41,
      (byte) 119,
      (byte) 93,
      (byte) 112 /*0x70*/,
      (byte) 141,
      (byte) 53,
      (byte) 198,
      (byte) 48 /*0x30*/,
      (byte) 225
    };
    byte[] numArray7 = new byte[23]
    {
      (byte) 137,
      (byte) 24,
      (byte) 33,
      (byte) 150,
      (byte) 231,
      (byte) 92,
      (byte) 64 /*0x40*/,
      (byte) 138,
      (byte) 150,
      (byte) 25,
      (byte) 166,
      (byte) 206,
      (byte) 36,
      (byte) 21,
      (byte) 76,
      (byte) 189,
      (byte) 210,
      (byte) 235,
      (byte) 246,
      (byte) 129,
      (byte) 160 /*0xA0*/,
      (byte) 250,
      (byte) 241
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12294()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[6] = (byte) 42;
      numArray2[5] = (byte) 112 /*0x70*/;
      numArray2[11] = (byte) 232;
      numArray2[7] = (byte) 136;
      numArray2[4] = (byte) 122;
      numArray2[3] = (byte) 181;
      numArray2[2] = (byte) 164;
      numArray2[15] = (byte) 77;
      numArray2[8] = (byte) 240 /*0xF0*/;
      numArray2[9] = (byte) 40;
      numArray2[13] = (byte) 90;
      numArray2[0] = (byte) 246;
      numArray2[10] = (byte) 132;
      numArray2[1] = (byte) 40;
      numArray2[14] = (byte) 217;
      numArray2[12] = (byte) 157;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 41,
        (byte) 61,
        (byte) 21,
        (byte) 39,
        (byte) 77,
        (byte) 137,
        (byte) 78,
        (byte) 89,
        (byte) 182,
        (byte) 76,
        (byte) 95,
        (byte) 123,
        (byte) 161,
        (byte) 205,
        (byte) 133,
        (byte) 100
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 243,
      (byte) 30,
      (byte) 161,
      (byte) 182,
      (byte) 187,
      (byte) 87,
      (byte) 43,
      (byte) 56,
      (byte) 78,
      (byte) 216,
      (byte) 136,
      (byte) 239,
      (byte) 219,
      (byte) 236,
      (byte) 227,
      (byte) 8
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 194,
      (byte) 95,
      (byte) 75,
      (byte) 32 /*0x20*/,
      (byte) 174,
      (byte) 202,
      (byte) 4,
      (byte) 166,
      (byte) 185,
      (byte) 81,
      (byte) 140,
      (byte) 238,
      (byte) 8,
      (byte) 68,
      (byte) 103,
      (byte) 70
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12295()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 214,
        (byte) 24,
        (byte) 239,
        (byte) 28,
        (byte) 134,
        (byte) 162,
        (byte) 99,
        (byte) 212,
        (byte) 13,
        (byte) 102
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 129,
        (byte) 3,
        (byte) 35,
        (byte) 167,
        (byte) 177,
        (byte) 65,
        (byte) 38,
        (byte) 1,
        (byte) 193,
        (byte) 199
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 165;
    numArray5[6] = (byte) 27;
    numArray5[8] = (byte) 231;
    numArray5[3] = (byte) 55;
    numArray5[4] = (byte) 29;
    numArray5[5] = (byte) 226;
    numArray5[2] = (byte) 47;
    numArray5[1] = (byte) 230;
    numArray5[0] = (byte) 28;
    numArray5[9] = (byte) 193;
    byte[] numArray6 = new byte[10];
    numArray6[7] = (byte) 111;
    numArray6[1] = (byte) 220;
    numArray6[2] = (byte) 61;
    numArray6[9] = (byte) 111;
    numArray6[0] = (byte) 40;
    numArray6[5] = (byte) 245;
    numArray6[4] = (byte) 248;
    numArray6[6] = (byte) 171;
    numArray6[8] = (byte) 130;
    numArray6[3] = (byte) 243;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_12269.sspq, 285, (Array) numArray7, 0, 23);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12269.sspr, 285, (Array) numArray7, 0, 23);
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
