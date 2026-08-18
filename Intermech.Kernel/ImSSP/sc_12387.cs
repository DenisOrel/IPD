// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12387
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12387
{
  private static byte[] sspq = new byte[154]
  {
    (byte) 131,
    (byte) 248,
    (byte) 247,
    (byte) 82,
    (byte) 221,
    (byte) 180,
    (byte) 206,
    (byte) 222,
    (byte) 90,
    (byte) 151,
    (byte) 15,
    (byte) 75,
    (byte) 8,
    (byte) 61,
    (byte) 215,
    (byte) 211,
    (byte) 102,
    (byte) 153,
    (byte) 177,
    (byte) 98,
    (byte) 218,
    (byte) 138,
    (byte) 32 /*0x20*/,
    (byte) 10,
    (byte) 110,
    (byte) 30,
    (byte) 141,
    (byte) 105,
    (byte) 239,
    (byte) 110,
    (byte) 8,
    (byte) 149,
    (byte) 3,
    (byte) 242,
    (byte) 100,
    (byte) 155,
    (byte) 123,
    (byte) 68,
    (byte) 67,
    (byte) 35,
    (byte) 207,
    (byte) 55,
    (byte) 93,
    (byte) 230,
    (byte) 187,
    (byte) 100,
    (byte) 123,
    (byte) 184,
    (byte) 183,
    (byte) 52,
    (byte) 125,
    (byte) 59,
    (byte) 109,
    (byte) 103,
    (byte) 17,
    (byte) 107,
    (byte) 220,
    (byte) 138,
    (byte) 248,
    (byte) 167,
    (byte) 245,
    (byte) 1,
    (byte) 223,
    (byte) 123,
    (byte) 219,
    (byte) 91,
    (byte) 229,
    (byte) 223,
    (byte) 212,
    (byte) 110,
    (byte) 61,
    (byte) 162,
    (byte) 59,
    (byte) 239,
    (byte) 4,
    (byte) 31 /*0x1F*/,
    (byte) 78,
    (byte) 191,
    (byte) 107,
    (byte) 48 /*0x30*/,
    (byte) 106,
    (byte) 172,
    (byte) 52,
    (byte) 223,
    (byte) 125,
    (byte) 103,
    (byte) 213,
    (byte) 185,
    (byte) 95,
    (byte) 196,
    (byte) 230,
    (byte) 44,
    (byte) 81,
    (byte) 37,
    (byte) 236,
    (byte) 62,
    (byte) 246,
    (byte) 55,
    (byte) 224 /*0xE0*/,
    (byte) 150,
    (byte) 231,
    (byte) 72,
    (byte) 198,
    (byte) 115,
    (byte) 187,
    (byte) 28,
    (byte) 181,
    (byte) 6,
    (byte) 173,
    (byte) 238,
    (byte) 183,
    (byte) 197,
    (byte) 243,
    (byte) 206,
    (byte) 162,
    (byte) 143,
    (byte) 230,
    (byte) 148,
    (byte) 24,
    (byte) 25,
    (byte) 62,
    (byte) 198,
    (byte) 192 /*0xC0*/,
    (byte) 217,
    (byte) 192 /*0xC0*/,
    (byte) 71,
    (byte) 134,
    (byte) 72,
    (byte) 131,
    (byte) 34,
    (byte) 73,
    (byte) 56,
    (byte) 183,
    (byte) 77,
    (byte) 36,
    (byte) 219,
    (byte) 68,
    (byte) 133,
    (byte) 151,
    (byte) 7,
    (byte) 181,
    (byte) 151,
    (byte) 254,
    (byte) 22,
    (byte) 251,
    (byte) 211,
    (byte) 245,
    (byte) 129,
    (byte) 141,
    (byte) 131,
    (byte) 9,
    (byte) 64 /*0x40*/,
    (byte) 0,
    (byte) 153
  };
  private static byte[] sspr = new byte[154]
  {
    (byte) 10,
    (byte) 179,
    (byte) 193,
    (byte) 171,
    (byte) 80 /*0x50*/,
    (byte) 199,
    (byte) 83,
    (byte) 234,
    (byte) 242,
    (byte) 148,
    (byte) 159,
    (byte) 6,
    (byte) 95,
    (byte) 33,
    (byte) 77,
    (byte) 244,
    (byte) 69,
    (byte) 218,
    (byte) 1,
    (byte) 57,
    (byte) 120,
    (byte) 143,
    (byte) 129,
    (byte) 250,
    (byte) 242,
    (byte) 4,
    (byte) 101,
    (byte) 200,
    (byte) 192 /*0xC0*/,
    (byte) 130,
    (byte) 25,
    (byte) 143,
    (byte) 239,
    (byte) 238,
    (byte) 196,
    (byte) 167,
    (byte) 99,
    (byte) 42,
    (byte) 198,
    (byte) 181,
    (byte) 252,
    (byte) 154,
    (byte) 138,
    (byte) 118,
    (byte) 43,
    (byte) 222,
    (byte) 168,
    (byte) 217,
    (byte) 234,
    (byte) 214,
    (byte) 144 /*0x90*/,
    (byte) 48 /*0x30*/,
    (byte) 27,
    (byte) 167,
    (byte) 169,
    (byte) 91,
    (byte) 160 /*0xA0*/,
    (byte) 35,
    (byte) 200,
    (byte) 213,
    (byte) 254,
    (byte) 244,
    (byte) 164,
    (byte) 151,
    (byte) 18,
    (byte) 107,
    (byte) 203,
    (byte) 13,
    (byte) 170,
    (byte) 186,
    (byte) 100,
    (byte) 111,
    (byte) 57,
    (byte) 45,
    byte.MaxValue,
    (byte) 252,
    (byte) 66,
    (byte) 130,
    (byte) 207,
    (byte) 167,
    (byte) 229,
    (byte) 21,
    (byte) 73,
    (byte) 152,
    (byte) 9,
    (byte) 1,
    (byte) 199,
    (byte) 237,
    (byte) 5,
    (byte) 149,
    (byte) 224 /*0xE0*/,
    (byte) 240 /*0xF0*/,
    (byte) 86,
    (byte) 180,
    (byte) 112 /*0x70*/,
    (byte) 249,
    (byte) 9,
    (byte) 85,
    (byte) 139,
    (byte) 183,
    (byte) 128 /*0x80*/,
    (byte) 127 /*0x7F*/,
    (byte) 163,
    (byte) 176 /*0xB0*/,
    (byte) 45,
    (byte) 193,
    (byte) 24,
    (byte) 192 /*0xC0*/,
    (byte) 151,
    (byte) 40,
    (byte) 101,
    (byte) 152,
    (byte) 237,
    (byte) 198,
    (byte) 104,
    (byte) 29,
    (byte) 217,
    (byte) 104,
    (byte) 36,
    (byte) 22,
    (byte) 146,
    (byte) 241,
    (byte) 208 /*0xD0*/,
    (byte) 109,
    (byte) 169,
    (byte) 125,
    (byte) 67,
    (byte) 216,
    (byte) 172,
    (byte) 102,
    (byte) 55,
    (byte) 16 /*0x10*/,
    (byte) 80 /*0x50*/,
    (byte) 195,
    (byte) 30,
    (byte) 80 /*0x50*/,
    (byte) 140,
    (byte) 50,
    (byte) 195,
    (byte) 236,
    (byte) 199,
    (byte) 250,
    (byte) 34,
    (byte) 5,
    (byte) 227,
    (byte) 5,
    (byte) 137,
    byte.MaxValue,
    (byte) 54,
    (byte) 84,
    (byte) 219,
    (byte) 22,
    (byte) 138,
    (byte) 0
  };

  internal static int ssp_appserver_12388(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 3,
      (byte) 23,
      (byte) 83,
      (byte) 209,
      (byte) 148,
      (byte) 38,
      (byte) 113,
      (byte) 70,
      (byte) 251,
      (byte) 111,
      (byte) 165,
      (byte) 149,
      (byte) 31 /*0x1F*/,
      (byte) 137,
      (byte) 67,
      (byte) 213,
      (byte) 182,
      (byte) 75,
      (byte) 203,
      (byte) 162,
      (byte) 116,
      (byte) 28,
      (byte) 82,
      (byte) 92,
      (byte) 135,
      (byte) 232,
      (byte) 28,
      (byte) 171,
      (byte) 43,
      (byte) 97,
      (byte) 69,
      (byte) 151,
      (byte) 53,
      (byte) 229,
      (byte) 252,
      (byte) 201,
      (byte) 49,
      (byte) 203,
      (byte) 167,
      (byte) 229,
      (byte) 115,
      (byte) 4,
      (byte) 167,
      (byte) 59,
      (byte) 4,
      (byte) 66,
      (byte) 130,
      (byte) 227
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 65,
      (byte) 225,
      (byte) 237,
      (byte) 32 /*0x20*/,
      (byte) 221,
      (byte) 229,
      (byte) 14,
      (byte) 78,
      (byte) 37,
      (byte) 175,
      (byte) 213,
      (byte) 167,
      (byte) 50,
      (byte) 50,
      (byte) 56,
      (byte) 162,
      (byte) 46,
      (byte) 207,
      (byte) 12,
      (byte) 163,
      (byte) 189,
      (byte) 148,
      (byte) 231,
      (byte) 8,
      (byte) 165,
      (byte) 180,
      (byte) 131,
      (byte) 72,
      (byte) 58,
      (byte) 190,
      (byte) 68,
      (byte) 90,
      (byte) 49,
      (byte) 150,
      byte.MaxValue,
      (byte) 184,
      (byte) 234,
      (byte) 214,
      (byte) 202,
      (byte) 87,
      (byte) 126,
      (byte) 250,
      (byte) 37,
      (byte) 102,
      (byte) 193,
      (byte) 83,
      (byte) 4,
      (byte) 227
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12389(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 161,
      (byte) 230,
      (byte) 196,
      (byte) 246,
      (byte) 73,
      (byte) 172,
      (byte) 9,
      (byte) 197,
      (byte) 27,
      (byte) 154,
      (byte) 42,
      (byte) 14,
      (byte) 96 /*0x60*/,
      (byte) 58,
      (byte) 141,
      (byte) 122,
      (byte) 163,
      (byte) 234,
      (byte) 164,
      (byte) 229,
      (byte) 47,
      (byte) 111,
      (byte) 117,
      (byte) 23,
      (byte) 88,
      (byte) 69,
      (byte) 101,
      (byte) 57,
      (byte) 6,
      (byte) 80 /*0x50*/,
      (byte) 46,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 116,
      (byte) 68,
      (byte) 21,
      (byte) 4,
      (byte) 59,
      (byte) 129,
      (byte) 250,
      (byte) 220,
      (byte) 173,
      (byte) 233,
      (byte) 13,
      (byte) 17,
      (byte) 245,
      (byte) 226
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 102;
    sourceArray2[24] = (byte) 103;
    sourceArray2[15] = (byte) 81;
    sourceArray2[3] = (byte) 30;
    sourceArray2[7] = (byte) 150;
    sourceArray2[45] = (byte) 79;
    sourceArray2[6] = (byte) 55;
    sourceArray2[20] = (byte) 134;
    sourceArray2[8] = (byte) 68;
    sourceArray2[30] = (byte) 173;
    sourceArray2[5] = (byte) 131;
    sourceArray2[11] = (byte) 30;
    sourceArray2[19] = (byte) 219;
    sourceArray2[13] = (byte) 19;
    sourceArray2[14] = (byte) 196;
    sourceArray2[25] = (byte) 58;
    sourceArray2[0] = (byte) 130;
    sourceArray2[47] = (byte) 119;
    sourceArray2[33] = (byte) 173;
    sourceArray2[44] = (byte) 49;
    sourceArray2[10] = (byte) 107;
    sourceArray2[21] = (byte) 58;
    sourceArray2[22] = (byte) 249;
    sourceArray2[23] = (byte) 154;
    sourceArray2[40] = (byte) 132;
    sourceArray2[16 /*0x10*/] = (byte) 172;
    sourceArray2[39] = (byte) 219;
    sourceArray2[27] = (byte) 26;
    sourceArray2[28] = (byte) 120;
    sourceArray2[42] = (byte) 33;
    sourceArray2[26] = (byte) 191;
    sourceArray2[31 /*0x1F*/] = (byte) 241;
    sourceArray2[1] = (byte) 159;
    sourceArray2[12] = (byte) 242;
    sourceArray2[34] = (byte) 222;
    sourceArray2[43] = (byte) 72;
    sourceArray2[18] = (byte) 55;
    sourceArray2[35] = (byte) 191;
    sourceArray2[38] = (byte) 128 /*0x80*/;
    sourceArray2[2] = (byte) 16 /*0x10*/;
    sourceArray2[29] = (byte) 41;
    sourceArray2[41] = (byte) 49;
    sourceArray2[32 /*0x20*/] = (byte) 222;
    sourceArray2[17] = (byte) 181;
    sourceArray2[9] = (byte) 35;
    sourceArray2[4] = (byte) 230;
    sourceArray2[46] = (byte) 125;
    sourceArray2[36] = (byte) 106;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[11];
    byte[] response2 = new byte[11];
    Array.Copy((Array) sc_12387.sspq, 0, (Array) numArray2, 0, 11);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12387.sspr, 0, (Array) numArray2, 0, 11);
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

  internal static string ssp_appserver_12390()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 9,
        (byte) 166,
        (byte) 58,
        (byte) 0,
        (byte) 41,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      numArray2[3] = (byte) 162;
      numArray2[7] = (byte) 176 /*0xB0*/;
      numArray2[6] = (byte) 227;
      numArray2[5] = (byte) 236;
      byte[] numArray3 = new byte[8];
      numArray3[6] = (byte) 117;
      numArray3[1] = (byte) 22;
      numArray3[2] = (byte) 24;
      numArray3[7] = (byte) 169;
      numArray3[4] = (byte) 139;
      numArray3[5] = (byte) 20;
      numArray3[3] = (byte) 80 /*0x50*/;
      numArray3[0] = (byte) 120;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[51];
      byte[] response = new byte[51];
      Array.Copy((Array) sc_12387.sspq, 11, (Array) numArray4, 0, 51);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12387.sspr, 11, (Array) numArray4, 0, 51);
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
    byte[] numArray5 = new byte[8];
    byte[] numArray6 = new byte[8];
    numArray6[5] = (byte) 22;
    numArray6[1] = (byte) 92;
    numArray6[7] = (byte) 60;
    numArray6[3] = (byte) 146;
    numArray6[4] = (byte) 72;
    numArray6[2] = (byte) 253;
    numArray6[6] = (byte) 216;
    numArray6[0] = (byte) 123;
    byte[] numArray7 = new byte[8]
    {
      (byte) 240 /*0xF0*/,
      (byte) 155,
      (byte) 62,
      (byte) 84,
      (byte) 93,
      (byte) 106,
      (byte) 51,
      (byte) 218
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12391()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8];
      numArray2[2] = (byte) 241;
      numArray2[6] = (byte) 225;
      numArray2[7] = (byte) 249;
      numArray2[3] = (byte) 205;
      numArray2[4] = (byte) 40;
      numArray2[1] = (byte) 187;
      numArray2[5] = (byte) 155;
      numArray2[0] = (byte) 178;
      byte[] numArray3 = new byte[8]
      {
        (byte) 156,
        (byte) 115,
        (byte) 157,
        (byte) 94,
        (byte) 31 /*0x1F*/,
        (byte) 117,
        (byte) 247,
        (byte) 177
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_12387.sspq, 62, (Array) numArray4, 0, 45);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12387.sspr, 62, (Array) numArray4, 0, 45);
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
    byte[] numArray5 = new byte[8];
    byte[] numArray6 = new byte[8]
    {
      (byte) 92,
      (byte) 151,
      (byte) 14,
      (byte) 128 /*0x80*/,
      (byte) 252,
      (byte) 149,
      (byte) 184,
      (byte) 190
    };
    byte[] numArray7 = new byte[8];
    numArray7[2] = (byte) 111;
    numArray7[1] = (byte) 243;
    numArray7[6] = (byte) 90;
    numArray7[0] = (byte) 193;
    numArray7[3] = (byte) 229;
    numArray7[5] = (byte) 146;
    numArray7[4] = (byte) 90;
    numArray7[7] = (byte) 25;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12392(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 2,
      (byte) 198,
      (byte) 41,
      (byte) 22,
      (byte) 223,
      (byte) 213,
      (byte) 72,
      (byte) 33,
      (byte) 229,
      (byte) 193,
      (byte) 35,
      (byte) 97,
      (byte) 178,
      (byte) 120,
      (byte) 34,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 234,
      (byte) 151,
      (byte) 152,
      (byte) 102,
      (byte) 124,
      (byte) 14,
      (byte) 246,
      (byte) 58,
      (byte) 109,
      (byte) 16 /*0x10*/,
      (byte) 188,
      (byte) 200,
      (byte) 181,
      (byte) 235,
      (byte) 170,
      (byte) 203,
      (byte) 159,
      (byte) 90,
      (byte) 208 /*0xD0*/,
      (byte) 229,
      (byte) 105,
      (byte) 28,
      (byte) 192 /*0xC0*/,
      (byte) 112 /*0x70*/,
      (byte) 157,
      (byte) 138,
      (byte) 110,
      (byte) 220,
      (byte) 156,
      (byte) 192 /*0xC0*/,
      (byte) 12
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[40] = (byte) 243;
    sourceArray2[47] = (byte) 113;
    sourceArray2[24] = (byte) 112 /*0x70*/;
    sourceArray2[29] = (byte) 182;
    sourceArray2[4] = (byte) 12;
    sourceArray2[39] = (byte) 122;
    sourceArray2[0] = (byte) 2;
    sourceArray2[7] = (byte) 161;
    sourceArray2[18] = (byte) 92;
    sourceArray2[9] = (byte) 185;
    sourceArray2[2] = (byte) 158;
    sourceArray2[11] = (byte) 205;
    sourceArray2[10] = (byte) 221;
    sourceArray2[26] = (byte) 139;
    sourceArray2[14] = (byte) 78;
    sourceArray2[45] = (byte) 173;
    sourceArray2[13] = (byte) 26;
    sourceArray2[46] = (byte) 18;
    sourceArray2[8] = (byte) 233;
    sourceArray2[15] = (byte) 145;
    sourceArray2[42] = (byte) 161;
    sourceArray2[3] = (byte) 215;
    sourceArray2[22] = (byte) 189;
    sourceArray2[17] = (byte) 198;
    sourceArray2[23] = (byte) 176 /*0xB0*/;
    sourceArray2[21] = (byte) 159;
    sourceArray2[12] = (byte) 224 /*0xE0*/;
    sourceArray2[5] = (byte) 136;
    sourceArray2[16 /*0x10*/] = (byte) 69;
    sourceArray2[36] = (byte) 137;
    sourceArray2[30] = (byte) 231;
    sourceArray2[31 /*0x1F*/] = (byte) 159;
    sourceArray2[44] = (byte) 5;
    sourceArray2[33] = (byte) 15;
    sourceArray2[34] = (byte) 152;
    sourceArray2[35] = (byte) 171;
    sourceArray2[38] = (byte) 55;
    sourceArray2[37] = (byte) 139;
    sourceArray2[1] = (byte) 96 /*0x60*/;
    sourceArray2[6] = (byte) 158;
    sourceArray2[20] = (byte) 229;
    sourceArray2[41] = (byte) 182;
    sourceArray2[25] = (byte) 189;
    sourceArray2[32 /*0x20*/] = (byte) 94;
    sourceArray2[43] = (byte) 136;
    sourceArray2[27] = (byte) 222;
    sourceArray2[28] = (byte) 175;
    sourceArray2[19] = (byte) 252;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[47];
    byte[] response2 = new byte[47];
    Array.Copy((Array) sc_12387.sspq, 107, (Array) numArray2, 0, 47);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12387.sspr, 107, (Array) numArray2, 0, 47);
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

  internal static int ssp_appserver_12393(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 170;
    sourceArray1[16 /*0x10*/] = (byte) 252;
    sourceArray1[2] = (byte) 131;
    sourceArray1[3] = (byte) 18;
    sourceArray1[4] = (byte) 18;
    sourceArray1[30] = (byte) 55;
    sourceArray1[6] = (byte) 130;
    sourceArray1[7] = (byte) 138;
    sourceArray1[8] = (byte) 164;
    sourceArray1[0] = (byte) 59;
    sourceArray1[19] = (byte) 167;
    sourceArray1[11] = (byte) 165;
    sourceArray1[15] = (byte) 96 /*0x60*/;
    sourceArray1[37] = (byte) 132;
    sourceArray1[35] = (byte) 57;
    sourceArray1[28] = (byte) 24;
    sourceArray1[32 /*0x20*/] = byte.MaxValue;
    sourceArray1[31 /*0x1F*/] = (byte) 29;
    sourceArray1[1] = (byte) 10;
    sourceArray1[34] = (byte) 159;
    sourceArray1[20] = (byte) 199;
    sourceArray1[10] = (byte) 13;
    sourceArray1[40] = (byte) 21;
    sourceArray1[23] = (byte) 237;
    sourceArray1[24] = (byte) 81;
    sourceArray1[25] = (byte) 5;
    sourceArray1[5] = (byte) 27;
    sourceArray1[27] = (byte) 0;
    sourceArray1[36] = (byte) 2;
    sourceArray1[17] = (byte) 17;
    sourceArray1[9] = (byte) 127 /*0x7F*/;
    sourceArray1[22] = (byte) 221;
    sourceArray1[21] = (byte) 169;
    sourceArray1[44] = (byte) 197;
    sourceArray1[26] = (byte) 186;
    sourceArray1[42] = (byte) 113;
    sourceArray1[18] = (byte) 99;
    sourceArray1[14] = (byte) 75;
    sourceArray1[38] = (byte) 244;
    sourceArray1[45] = (byte) 79;
    sourceArray1[33] = (byte) 64 /*0x40*/;
    sourceArray1[41] = (byte) 115;
    sourceArray1[13] = (byte) 199;
    sourceArray1[43] = (byte) 124;
    sourceArray1[29] = (byte) 9;
    sourceArray1[46] = (byte) 215;
    sourceArray1[39] = (byte) 154;
    sourceArray1[47] = (byte) 198;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 92;
    sourceArray2[36] = (byte) 171;
    sourceArray2[2] = (byte) 156;
    sourceArray2[3] = (byte) 184;
    sourceArray2[29] = (byte) 238;
    sourceArray2[34] = (byte) 183;
    sourceArray2[6] = (byte) 125;
    sourceArray2[7] = (byte) 132;
    sourceArray2[46] = (byte) 36;
    sourceArray2[26] = (byte) 103;
    sourceArray2[10] = (byte) 21;
    sourceArray2[27] = (byte) 18;
    sourceArray2[0] = (byte) 81;
    sourceArray2[17] = (byte) 185;
    sourceArray2[14] = (byte) 16 /*0x10*/;
    sourceArray2[13] = (byte) 234;
    sourceArray2[8] = (byte) 95;
    sourceArray2[33] = (byte) 238;
    sourceArray2[18] = (byte) 180;
    sourceArray2[19] = (byte) 72;
    sourceArray2[20] = (byte) 244;
    sourceArray2[21] = (byte) 70;
    sourceArray2[22] = (byte) 40;
    sourceArray2[23] = (byte) 142;
    sourceArray2[5] = (byte) 243;
    sourceArray2[25] = (byte) 246;
    sourceArray2[1] = (byte) 232;
    sourceArray2[9] = (byte) 203;
    sourceArray2[28] = (byte) 50;
    sourceArray2[16 /*0x10*/] = (byte) 228;
    sourceArray2[30] = (byte) 24;
    sourceArray2[37] = (byte) 175;
    sourceArray2[41] = (byte) 57;
    sourceArray2[38] = (byte) 194;
    sourceArray2[15] = (byte) 254;
    sourceArray2[35] = (byte) 230;
    sourceArray2[31 /*0x1F*/] = (byte) 227;
    sourceArray2[24] = (byte) 183;
    sourceArray2[44] = (byte) 121;
    sourceArray2[39] = (byte) 201;
    sourceArray2[40] = (byte) 189;
    sourceArray2[4] = (byte) 147;
    sourceArray2[42] = (byte) 86;
    sourceArray2[43] = (byte) 157;
    sourceArray2[12] = (byte) 235;
    sourceArray2[45] = (byte) 148;
    sourceArray2[11] = (byte) 103;
    sourceArray2[47] = (byte) 126;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
