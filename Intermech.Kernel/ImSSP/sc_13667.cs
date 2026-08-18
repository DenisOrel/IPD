// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13667
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13667
{
  private static byte[] sspq = new byte[201]
  {
    (byte) 161,
    (byte) 90,
    (byte) 210,
    (byte) 57,
    (byte) 63 /*0x3F*/,
    (byte) 26,
    (byte) 28,
    (byte) 121,
    (byte) 103,
    (byte) 2,
    (byte) 136,
    (byte) 83,
    (byte) 61,
    (byte) 242,
    (byte) 182,
    (byte) 184,
    (byte) 239,
    (byte) 58,
    (byte) 234,
    (byte) 44,
    (byte) 143,
    (byte) 0,
    (byte) 92,
    (byte) 15,
    (byte) 37,
    (byte) 218,
    (byte) 172,
    (byte) 68,
    (byte) 69,
    (byte) 170,
    (byte) 214,
    (byte) 81,
    (byte) 59,
    (byte) 127 /*0x7F*/,
    (byte) 190,
    (byte) 172,
    (byte) 78,
    (byte) 35,
    (byte) 70,
    (byte) 71,
    (byte) 235,
    (byte) 230,
    (byte) 96 /*0x60*/,
    (byte) 124,
    byte.MaxValue,
    (byte) 105,
    (byte) 19,
    (byte) 55,
    (byte) 23,
    (byte) 189,
    (byte) 52,
    (byte) 10,
    (byte) 105,
    (byte) 204,
    (byte) 244,
    (byte) 162,
    (byte) 65,
    (byte) 169,
    (byte) 160 /*0xA0*/,
    (byte) 224 /*0xE0*/,
    (byte) 15,
    (byte) 187,
    (byte) 199,
    (byte) 190,
    (byte) 96 /*0x60*/,
    (byte) 87,
    (byte) 204,
    (byte) 186,
    (byte) 94,
    (byte) 70,
    (byte) 193,
    (byte) 59,
    (byte) 168,
    (byte) 47,
    (byte) 174,
    (byte) 107,
    (byte) 159,
    (byte) 17,
    (byte) 189,
    (byte) 122,
    (byte) 37,
    (byte) 189,
    (byte) 253,
    (byte) 204,
    (byte) 210,
    (byte) 251,
    (byte) 243,
    (byte) 164,
    (byte) 246,
    (byte) 29,
    (byte) 21,
    (byte) 218,
    (byte) 68,
    (byte) 236,
    (byte) 66,
    (byte) 48 /*0x30*/,
    (byte) 46,
    (byte) 10,
    (byte) 120,
    (byte) 218,
    (byte) 91,
    (byte) 59,
    (byte) 129,
    (byte) 168,
    (byte) 92,
    (byte) 43,
    (byte) 222,
    (byte) 186,
    (byte) 200,
    (byte) 79,
    (byte) 50,
    (byte) 59,
    (byte) 89,
    (byte) 77,
    (byte) 133,
    (byte) 195,
    (byte) 254,
    (byte) 54,
    (byte) 223,
    (byte) 190,
    (byte) 6,
    (byte) 239,
    (byte) 150,
    (byte) 64 /*0x40*/,
    (byte) 194,
    (byte) 45,
    (byte) 114,
    (byte) 160 /*0xA0*/,
    (byte) 73,
    (byte) 229,
    (byte) 205,
    (byte) 175,
    (byte) 154,
    (byte) 23,
    (byte) 248,
    (byte) 104,
    (byte) 202,
    (byte) 2,
    (byte) 220,
    (byte) 135,
    (byte) 228,
    (byte) 136,
    (byte) 233,
    (byte) 184,
    (byte) 128 /*0x80*/,
    (byte) 185,
    (byte) 5,
    (byte) 174,
    (byte) 62,
    (byte) 61,
    (byte) 102,
    (byte) 150,
    (byte) 95,
    (byte) 57,
    (byte) 77,
    (byte) 43,
    (byte) 252,
    (byte) 3,
    (byte) 210,
    (byte) 19,
    (byte) 4,
    (byte) 63 /*0x3F*/,
    (byte) 172,
    (byte) 98,
    (byte) 135,
    (byte) 205,
    (byte) 219,
    (byte) 17,
    (byte) 97,
    (byte) 89,
    (byte) 220,
    (byte) 18,
    (byte) 213,
    (byte) 19,
    (byte) 205,
    (byte) 246,
    (byte) 217,
    (byte) 136,
    (byte) 67,
    (byte) 153,
    (byte) 121,
    (byte) 212,
    (byte) 199,
    (byte) 171,
    (byte) 81,
    (byte) 233,
    (byte) 89,
    (byte) 203,
    (byte) 96 /*0x60*/,
    (byte) 18,
    (byte) 130,
    (byte) 249,
    (byte) 172,
    (byte) 232,
    (byte) 82,
    (byte) 187,
    (byte) 236,
    (byte) 246,
    (byte) 16 /*0x10*/,
    (byte) 245,
    (byte) 40
  };
  private static byte[] sspr = new byte[201]
  {
    (byte) 40,
    (byte) 154,
    (byte) 17,
    (byte) 91,
    (byte) 33,
    (byte) 20,
    (byte) 88,
    (byte) 168,
    (byte) 247,
    (byte) 213,
    (byte) 135,
    (byte) 88,
    (byte) 100,
    (byte) 182,
    (byte) 172,
    (byte) 136,
    (byte) 89,
    (byte) 101,
    (byte) 117,
    (byte) 112 /*0x70*/,
    (byte) 188,
    (byte) 108,
    (byte) 184,
    (byte) 146,
    (byte) 121,
    (byte) 53,
    (byte) 126,
    (byte) 30,
    (byte) 43,
    (byte) 185,
    (byte) 125,
    (byte) 161,
    (byte) 173,
    (byte) 249,
    (byte) 164,
    (byte) 252,
    (byte) 125,
    (byte) 4,
    (byte) 138,
    (byte) 77,
    (byte) 199,
    (byte) 44,
    (byte) 83,
    (byte) 214,
    (byte) 192 /*0xC0*/,
    (byte) 189,
    (byte) 213,
    (byte) 236,
    (byte) 83,
    (byte) 46,
    (byte) 232,
    (byte) 34,
    (byte) 114,
    (byte) 102,
    (byte) 182,
    (byte) 4,
    (byte) 106,
    (byte) 211,
    (byte) 7,
    (byte) 34,
    (byte) 33,
    (byte) 152,
    (byte) 26,
    (byte) 211,
    (byte) 200,
    (byte) 11,
    (byte) 237,
    (byte) 29,
    (byte) 125,
    (byte) 19,
    (byte) 163,
    (byte) 76,
    (byte) 234,
    (byte) 227,
    (byte) 43,
    (byte) 228,
    (byte) 120,
    (byte) 187,
    (byte) 144 /*0x90*/,
    (byte) 86,
    (byte) 96 /*0x60*/,
    (byte) 51,
    (byte) 40,
    (byte) 64 /*0x40*/,
    (byte) 96 /*0x60*/,
    (byte) 70,
    (byte) 199,
    (byte) 245,
    (byte) 193,
    (byte) 167,
    (byte) 179,
    (byte) 172,
    (byte) 240 /*0xF0*/,
    (byte) 86,
    (byte) 240 /*0xF0*/,
    (byte) 166,
    (byte) 167,
    (byte) 71,
    (byte) 22,
    (byte) 190,
    (byte) 227,
    (byte) 219,
    (byte) 17,
    (byte) 247,
    (byte) 61,
    (byte) 228,
    (byte) 174,
    (byte) 50,
    (byte) 198,
    (byte) 89,
    (byte) 20,
    (byte) 198,
    (byte) 180,
    (byte) 184,
    (byte) 157,
    (byte) 209,
    (byte) 73,
    (byte) 227,
    (byte) 142,
    (byte) 110,
    (byte) 174,
    (byte) 61,
    (byte) 238,
    (byte) 37,
    (byte) 227,
    (byte) 128 /*0x80*/,
    (byte) 94,
    (byte) 235,
    (byte) 29,
    (byte) 251,
    (byte) 204,
    (byte) 211,
    (byte) 130,
    (byte) 193,
    (byte) 105,
    (byte) 71,
    (byte) 90,
    (byte) 84,
    (byte) 125,
    (byte) 156,
    (byte) 188,
    (byte) 97,
    (byte) 51,
    (byte) 177,
    (byte) 16 /*0x10*/,
    (byte) 9,
    (byte) 41,
    (byte) 125,
    (byte) 206,
    (byte) 134,
    (byte) 193,
    (byte) 159,
    (byte) 4,
    (byte) 48 /*0x30*/,
    (byte) 35,
    (byte) 249,
    (byte) 199,
    (byte) 229,
    (byte) 118,
    (byte) 71,
    (byte) 27,
    (byte) 159,
    (byte) 172,
    (byte) 184,
    (byte) 199,
    (byte) 165,
    (byte) 215,
    (byte) 152,
    (byte) 149,
    byte.MaxValue,
    (byte) 219,
    (byte) 0,
    (byte) 139,
    (byte) 198,
    (byte) 227,
    (byte) 208 /*0xD0*/,
    (byte) 20,
    (byte) 81,
    (byte) 199,
    (byte) 41,
    (byte) 205,
    (byte) 246,
    (byte) 235,
    (byte) 21,
    (byte) 66,
    (byte) 89,
    (byte) 222,
    (byte) 119,
    (byte) 36,
    (byte) 150,
    (byte) 189,
    (byte) 43,
    (byte) 235,
    (byte) 222,
    (byte) 49,
    (byte) 233,
    (byte) 101,
    (byte) 37,
    (byte) 64 /*0x40*/,
    (byte) 214,
    (byte) 246
  };

  internal static string ssp_appserver_13668()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 76,
        (byte) 52,
        (byte) 239,
        (byte) 200,
        (byte) 219,
        (byte) 151,
        (byte) 226,
        (byte) 43,
        (byte) 141,
        (byte) 223
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 182,
        (byte) 18,
        (byte) 235,
        (byte) 178,
        (byte) 135,
        (byte) 1,
        (byte) 41,
        (byte) 117,
        (byte) 247,
        (byte) 86
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[1] = (byte) 93;
    numArray5[4] = (byte) 6;
    numArray5[9] = (byte) 7;
    numArray5[0] = (byte) 46;
    numArray5[3] = (byte) 95;
    numArray5[5] = (byte) 33;
    numArray5[6] = (byte) 92;
    numArray5[8] = (byte) 98;
    numArray5[7] = (byte) 134;
    numArray5[2] = (byte) 116;
    byte[] numArray6 = new byte[10]
    {
      (byte) 16 /*0x10*/,
      (byte) 57,
      (byte) 129,
      (byte) 133,
      (byte) 127 /*0x7F*/,
      (byte) 197,
      (byte) 128 /*0x80*/,
      (byte) 241,
      (byte) 48 /*0x30*/,
      (byte) 4
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[47];
    byte[] response = new byte[47];
    Array.Copy((Array) sc_13667.sspq, 0, (Array) numArray7, 0, 47);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13667.sspr, 0, (Array) numArray7, 0, 47);
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

  internal static string ssp_appserver_13669()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 228,
        (byte) 237,
        (byte) 222,
        (byte) 28,
        (byte) 243,
        (byte) 245,
        (byte) 100,
        (byte) 160 /*0xA0*/,
        (byte) 253,
        (byte) 194,
        (byte) 20,
        (byte) 101,
        (byte) 109
      };
      byte[] numArray3 = new byte[13]
      {
        (byte) 227,
        (byte) 162,
        (byte) 98,
        (byte) 186,
        (byte) 237,
        (byte) 78,
        (byte) 136,
        (byte) 188,
        (byte) 55,
        (byte) 66,
        (byte) 246,
        (byte) 73,
        (byte) 26
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_13667.sspq, 47, (Array) numArray4, 0, 23);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13667.sspr, 47, (Array) numArray4, 0, 23);
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
    byte[] numArray5 = new byte[13];
    byte[] numArray6 = new byte[13];
    numArray6[9] = (byte) 162;
    numArray6[1] = (byte) 29;
    numArray6[8] = (byte) 177;
    numArray6[0] = (byte) 26;
    numArray6[2] = (byte) 174;
    numArray6[3] = (byte) 139;
    numArray6[6] = (byte) 218;
    numArray6[7] = (byte) 20;
    numArray6[10] = (byte) 79;
    numArray6[5] = (byte) 225;
    numArray6[12] = (byte) 81;
    numArray6[11] = (byte) 200;
    numArray6[4] = (byte) 14;
    byte[] numArray7 = new byte[13]
    {
      (byte) 90,
      (byte) 30,
      (byte) 69,
      (byte) 162,
      (byte) 128 /*0x80*/,
      (byte) 21,
      (byte) 156,
      (byte) 118,
      (byte) 183,
      (byte) 71,
      (byte) 125,
      (byte) 118,
      (byte) 147
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13670(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 216;
    sourceArray1[36] = (byte) 128 /*0x80*/;
    sourceArray1[2] = (byte) 209;
    sourceArray1[42] = (byte) 21;
    sourceArray1[43] = (byte) 137;
    sourceArray1[15] = (byte) 106;
    sourceArray1[32 /*0x20*/] = (byte) 213;
    sourceArray1[7] = (byte) 189;
    sourceArray1[9] = (byte) 199;
    sourceArray1[19] = (byte) 211;
    sourceArray1[40] = (byte) 166;
    sourceArray1[11] = (byte) 247;
    sourceArray1[6] = (byte) 221;
    sourceArray1[13] = (byte) 144 /*0x90*/;
    sourceArray1[14] = (byte) 71;
    sourceArray1[41] = (byte) 84;
    sourceArray1[0] = (byte) 113;
    sourceArray1[17] = (byte) 5;
    sourceArray1[16 /*0x10*/] = (byte) 113;
    sourceArray1[8] = (byte) 18;
    sourceArray1[37] = (byte) 178;
    sourceArray1[20] = (byte) 186;
    sourceArray1[22] = (byte) 13;
    sourceArray1[18] = (byte) 135;
    sourceArray1[5] = (byte) 91;
    sourceArray1[3] = (byte) 162;
    sourceArray1[26] = (byte) 145;
    sourceArray1[28] = (byte) 182;
    sourceArray1[12] = (byte) 254;
    sourceArray1[29] = (byte) 67;
    sourceArray1[30] = (byte) 44;
    sourceArray1[31 /*0x1F*/] = (byte) 157;
    sourceArray1[44] = (byte) 135;
    sourceArray1[33] = (byte) 176 /*0xB0*/;
    sourceArray1[1] = (byte) 217;
    sourceArray1[35] = (byte) 239;
    sourceArray1[23] = (byte) 146;
    sourceArray1[21] = (byte) 146;
    sourceArray1[38] = (byte) 162;
    sourceArray1[39] = (byte) 125;
    sourceArray1[25] = (byte) 29;
    sourceArray1[24] = (byte) 95;
    sourceArray1[34] = (byte) 221;
    sourceArray1[10] = (byte) 44;
    sourceArray1[4] = (byte) 23;
    sourceArray1[45] = (byte) 195;
    sourceArray1[46] = (byte) 2;
    sourceArray1[47] = (byte) 140;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      byte.MaxValue,
      (byte) 233,
      (byte) 111,
      (byte) 170,
      (byte) 224 /*0xE0*/,
      (byte) 237,
      (byte) 204,
      (byte) 187,
      (byte) 103,
      (byte) 229,
      (byte) 16 /*0x10*/,
      (byte) 133,
      (byte) 112 /*0x70*/,
      (byte) 237,
      (byte) 12,
      (byte) 140,
      (byte) 117,
      (byte) 213,
      (byte) 79,
      (byte) 173,
      (byte) 55,
      (byte) 96 /*0x60*/,
      (byte) 183,
      (byte) 217,
      (byte) 190,
      (byte) 247,
      (byte) 215,
      (byte) 119,
      (byte) 53,
      (byte) 30,
      (byte) 105,
      (byte) 168,
      (byte) 143,
      (byte) 228,
      (byte) 63 /*0x3F*/,
      (byte) 212,
      (byte) 219,
      (byte) 189,
      (byte) 213,
      (byte) 95,
      (byte) 43,
      (byte) 113,
      (byte) 140,
      (byte) 140,
      (byte) 180,
      (byte) 251,
      (byte) 229,
      (byte) 0
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13671()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 160 /*0xA0*/,
        (byte) 180,
        (byte) 217,
        (byte) 19,
        (byte) 4,
        (byte) 109,
        (byte) 68,
        (byte) 229,
        (byte) 218,
        (byte) 209,
        (byte) 98,
        (byte) 71,
        (byte) 52,
        (byte) 167,
        (byte) 222,
        (byte) 196,
        (byte) 152,
        (byte) 63 /*0x3F*/,
        (byte) 183,
        (byte) 22,
        (byte) 225
      };
      byte[] numArray3 = new byte[21]
      {
        (byte) 42,
        (byte) 76,
        (byte) 28,
        (byte) 39,
        (byte) 220,
        (byte) 11,
        (byte) 217,
        (byte) 179,
        (byte) 74,
        (byte) 138,
        (byte) 74,
        (byte) 88,
        (byte) 84,
        (byte) 191,
        (byte) 124,
        (byte) 109,
        (byte) 35,
        (byte) 134,
        (byte) 91,
        (byte) 151,
        (byte) 189
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21];
    numArray5[5] = (byte) 141;
    numArray5[1] = (byte) 168;
    numArray5[12] = (byte) 44;
    numArray5[14] = (byte) 195;
    numArray5[4] = (byte) 86;
    numArray5[13] = (byte) 88;
    numArray5[6] = (byte) 25;
    numArray5[7] = (byte) 54;
    numArray5[8] = (byte) 149;
    numArray5[9] = (byte) 19;
    numArray5[10] = (byte) 251;
    numArray5[11] = (byte) 102;
    numArray5[17] = (byte) 201;
    numArray5[20] = (byte) 156;
    numArray5[19] = (byte) 168;
    numArray5[0] = (byte) 52;
    numArray5[2] = (byte) 79;
    numArray5[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray5[18] = (byte) 241;
    numArray5[3] = (byte) 187;
    numArray5[15] = (byte) 170;
    byte[] numArray6 = new byte[21]
    {
      (byte) 27,
      (byte) 181,
      (byte) 99,
      (byte) 8,
      (byte) 177,
      (byte) 87,
      (byte) 66,
      (byte) 241,
      (byte) 237,
      (byte) 191,
      (byte) 159,
      (byte) 231,
      (byte) 158,
      (byte) 164,
      (byte) 214,
      (byte) 50,
      (byte) 135,
      (byte) 170,
      (byte) 10,
      (byte) 27,
      (byte) 233
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13672()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 101;
      numArray2[1] = (byte) 63 /*0x3F*/;
      numArray2[2] = (byte) 124;
      numArray2[8] = (byte) 24;
      numArray2[5] = (byte) 23;
      numArray2[0] = (byte) 152;
      numArray2[3] = (byte) 12;
      numArray2[7] = (byte) 44;
      numArray2[4] = (byte) 215;
      numArray2[9] = (byte) 88;
      byte[] numArray3 = new byte[10]
      {
        (byte) 46,
        (byte) 84,
        (byte) 89,
        (byte) 246,
        (byte) 106,
        (byte) 91,
        (byte) 33,
        (byte) 94,
        (byte) 79,
        (byte) 109
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
      (byte) 102,
      (byte) 140,
      (byte) 181,
      (byte) 59,
      (byte) 163,
      (byte) 240 /*0xF0*/,
      (byte) 112 /*0x70*/,
      (byte) 41,
      (byte) 236,
      (byte) 27
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 49,
      (byte) 106,
      (byte) 16 /*0x10*/,
      (byte) 235,
      (byte) 146,
      (byte) 101,
      (byte) 183,
      (byte) 188,
      (byte) 80 /*0x50*/,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13673()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 130,
        (byte) 0,
        byte.MaxValue,
        (byte) 228,
        (byte) 179,
        (byte) 12,
        (byte) 134,
        (byte) 193,
        (byte) 150,
        (byte) 152
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 76;
      numArray3[2] = (byte) 43;
      numArray3[9] = (byte) 12;
      numArray3[0] = (byte) 76;
      numArray3[4] = (byte) 252;
      numArray3[5] = (byte) 101;
      numArray3[6] = (byte) 64 /*0x40*/;
      numArray3[7] = (byte) 118;
      numArray3[8] = (byte) 168;
      numArray3[3] = (byte) 113;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24];
      byte[] response = new byte[24];
      Array.Copy((Array) sc_13667.sspq, 70, (Array) numArray4, 0, 24);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13667.sspr, 70, (Array) numArray4, 0, 24);
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
    byte[] numArray6 = new byte[10]
    {
      (byte) 52,
      (byte) 199,
      (byte) 0,
      (byte) 0,
      (byte) 197,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 222,
      (byte) 34
    };
    numArray6[5] = (byte) 1;
    numArray6[6] = (byte) 123;
    numArray6[7] = (byte) 2;
    numArray6[3] = (byte) 96 /*0x60*/;
    numArray6[2] = (byte) 32 /*0x20*/;
    byte[] numArray7 = new byte[10]
    {
      (byte) 127 /*0x7F*/,
      (byte) 206,
      (byte) 119,
      (byte) 130,
      (byte) 144 /*0x90*/,
      (byte) 235,
      (byte) 217,
      (byte) 133,
      (byte) 246,
      (byte) 164
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13674()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[30];
      byte[] numArray2 = new byte[30]
      {
        (byte) 30,
        (byte) 149,
        (byte) 211,
        (byte) 113,
        (byte) 137,
        (byte) 4,
        (byte) 199,
        (byte) 231,
        (byte) 246,
        (byte) 187,
        (byte) 250,
        (byte) 231,
        (byte) 75,
        (byte) 62,
        (byte) 75,
        (byte) 187,
        byte.MaxValue,
        (byte) 35,
        (byte) 222,
        (byte) 218,
        (byte) 169,
        (byte) 90,
        (byte) 151,
        (byte) 156,
        (byte) 145,
        (byte) 203,
        (byte) 36,
        (byte) 0,
        (byte) 207,
        (byte) 19
      };
      byte[] numArray3 = new byte[30]
      {
        (byte) 226,
        (byte) 78,
        (byte) 109,
        (byte) 159,
        (byte) 144 /*0x90*/,
        (byte) 1,
        (byte) 0,
        (byte) 133,
        (byte) 149,
        (byte) 46,
        (byte) 161,
        (byte) 87,
        (byte) 70,
        (byte) 163,
        (byte) 151,
        (byte) 108,
        (byte) 246,
        (byte) 231,
        (byte) 246,
        (byte) 178,
        (byte) 222,
        (byte) 106,
        (byte) 200,
        (byte) 136,
        (byte) 4,
        (byte) 98,
        (byte) 32 /*0x20*/,
        (byte) 190,
        (byte) 133,
        (byte) 208 /*0xD0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_13667.sspq, 94, (Array) numArray4, 0, 26);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13667.sspr, 94, (Array) numArray4, 0, 26);
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
    byte[] numArray5 = new byte[30];
    byte[] numArray6 = new byte[30];
    numArray6[28] = (byte) 107;
    numArray6[25] = (byte) 81;
    numArray6[2] = (byte) 24;
    numArray6[3] = (byte) 227;
    numArray6[9] = (byte) 27;
    numArray6[5] = (byte) 253;
    numArray6[6] = (byte) 71;
    numArray6[7] = (byte) 90;
    numArray6[4] = (byte) 46;
    numArray6[0] = (byte) 224 /*0xE0*/;
    numArray6[19] = (byte) 101;
    numArray6[24] = (byte) 225;
    numArray6[12] = (byte) 170;
    numArray6[13] = (byte) 4;
    numArray6[15] = (byte) 109;
    numArray6[29] = (byte) 126;
    numArray6[1] = (byte) 198;
    numArray6[17] = (byte) 238;
    numArray6[18] = (byte) 63 /*0x3F*/;
    numArray6[10] = (byte) 141;
    numArray6[20] = (byte) 237;
    numArray6[21] = (byte) 106;
    numArray6[11] = (byte) 245;
    numArray6[23] = (byte) 127 /*0x7F*/;
    numArray6[22] = (byte) 56;
    numArray6[16 /*0x10*/] = (byte) 217;
    numArray6[26] = (byte) 164;
    numArray6[14] = (byte) 126;
    numArray6[8] = (byte) 252;
    numArray6[27] = (byte) 82;
    byte[] numArray7 = new byte[30];
    numArray7[11] = (byte) 94;
    numArray7[1] = (byte) 230;
    numArray7[2] = (byte) 52;
    numArray7[3] = (byte) 34;
    numArray7[4] = (byte) 101;
    numArray7[26] = (byte) 92;
    numArray7[6] = (byte) 13;
    numArray7[10] = (byte) 22;
    numArray7[8] = (byte) 85;
    numArray7[9] = (byte) 60;
    numArray7[18] = (byte) 115;
    numArray7[16 /*0x10*/] = (byte) 204;
    numArray7[12] = (byte) 50;
    numArray7[28] = (byte) 103;
    numArray7[29] = byte.MaxValue;
    numArray7[15] = (byte) 149;
    numArray7[7] = (byte) 247;
    numArray7[17] = (byte) 88;
    numArray7[25] = (byte) 13;
    numArray7[19] = (byte) 173;
    numArray7[27] = (byte) 142;
    numArray7[0] = (byte) 73;
    numArray7[22] = (byte) 207;
    numArray7[23] = (byte) 67;
    numArray7[24] = (byte) 32 /*0x20*/;
    numArray7[21] = (byte) 75;
    numArray7[14] = (byte) 224 /*0xE0*/;
    numArray7[20] = (byte) 227;
    numArray7[5] = (byte) 166;
    numArray7[13] = (byte) 245;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 30);
    for (int index = 0; index < 30; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[19];
    byte[] response1 = new byte[19];
    Array.Copy((Array) sc_13667.sspq, 120, (Array) numArray8, 0, 19);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13667.sspr, 120, (Array) numArray8, 0, 19);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13675()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 28,
        (byte) 9,
        (byte) 112 /*0x70*/,
        (byte) 154,
        (byte) 233,
        (byte) 199,
        (byte) 149,
        (byte) 26,
        (byte) 7,
        (byte) 135
      };
      byte[] numArray3 = new byte[10];
      numArray3[2] = (byte) 57;
      numArray3[4] = (byte) 69;
      numArray3[1] = (byte) 37;
      numArray3[3] = (byte) 26;
      numArray3[0] = (byte) 198;
      numArray3[5] = (byte) 122;
      numArray3[6] = (byte) 47;
      numArray3[8] = (byte) 188;
      numArray3[9] = (byte) 123;
      numArray3[7] = (byte) 236;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[10];
      byte[] response = new byte[10];
      Array.Copy((Array) sc_13667.sspq, 139, (Array) numArray4, 0, 10);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13667.sspr, 139, (Array) numArray4, 0, 10);
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
    byte[] numArray6 = new byte[10]
    {
      (byte) 127 /*0x7F*/,
      (byte) 253,
      (byte) 126,
      (byte) 252,
      (byte) 87,
      (byte) 250,
      (byte) 152,
      (byte) 88,
      (byte) 152,
      (byte) 194
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 23,
      (byte) 243,
      (byte) 148,
      (byte) 7,
      (byte) 102,
      (byte) 22,
      (byte) 207,
      (byte) 88,
      (byte) 6,
      (byte) 46
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[52];
    byte[] response1 = new byte[52];
    Array.Copy((Array) sc_13667.sspq, 149, (Array) numArray8, 0, 52);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13667.sspr, 149, (Array) numArray8, 0, 52);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }
}
