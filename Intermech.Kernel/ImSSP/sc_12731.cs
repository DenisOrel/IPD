// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12731
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12731
{
  private static byte[] sspq = new byte[52]
  {
    (byte) 225,
    (byte) 72,
    (byte) 122,
    (byte) 161,
    (byte) 43,
    (byte) 32 /*0x20*/,
    (byte) 46,
    (byte) 114,
    (byte) 81,
    (byte) 118,
    (byte) 36,
    (byte) 42,
    (byte) 109,
    (byte) 137,
    (byte) 235,
    (byte) 110,
    (byte) 174,
    (byte) 33,
    (byte) 4,
    (byte) 60,
    (byte) 197,
    (byte) 157,
    (byte) 104,
    (byte) 156,
    (byte) 234,
    (byte) 75,
    (byte) 207,
    (byte) 234,
    (byte) 157,
    (byte) 64 /*0x40*/,
    (byte) 227,
    (byte) 226,
    (byte) 166,
    (byte) 131,
    (byte) 200,
    (byte) 63 /*0x3F*/,
    (byte) 101,
    (byte) 166,
    (byte) 72,
    (byte) 64 /*0x40*/,
    (byte) 203,
    (byte) 162,
    (byte) 211,
    (byte) 33,
    (byte) 244,
    (byte) 181,
    (byte) 93,
    (byte) 29,
    (byte) 41,
    (byte) 172,
    (byte) 206,
    (byte) 194
  };
  private static byte[] sspr = new byte[52]
  {
    (byte) 33,
    (byte) 184,
    (byte) 132,
    (byte) 183,
    (byte) 56,
    (byte) 84,
    (byte) 171,
    (byte) 164,
    (byte) 18,
    (byte) 37,
    (byte) 195,
    (byte) 24,
    (byte) 168,
    (byte) 117,
    (byte) 216,
    (byte) 208 /*0xD0*/,
    (byte) 197,
    (byte) 34,
    (byte) 38,
    (byte) 123,
    (byte) 207,
    (byte) 40,
    (byte) 164,
    (byte) 213,
    (byte) 243,
    (byte) 89,
    (byte) 37,
    (byte) 49,
    (byte) 102,
    (byte) 171,
    (byte) 217,
    (byte) 97,
    (byte) 74,
    (byte) 191,
    (byte) 106,
    (byte) 61,
    (byte) 78,
    (byte) 110,
    (byte) 246,
    (byte) 118,
    (byte) 141,
    (byte) 171,
    (byte) 90,
    (byte) 201,
    (byte) 196,
    (byte) 104,
    (byte) 77,
    (byte) 38,
    (byte) 243,
    (byte) 119,
    (byte) 89,
    (byte) 247
  };

  internal static string ssp_appserver_12732()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 79,
        (byte) 246,
        (byte) 148,
        (byte) 102,
        (byte) 55,
        (byte) 218,
        (byte) 193,
        (byte) 0,
        (byte) 181,
        (byte) 202,
        (byte) 21
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 217,
        (byte) 146,
        (byte) 186,
        (byte) 147,
        (byte) 240 /*0xF0*/,
        (byte) 162,
        (byte) 1,
        (byte) 83,
        (byte) 142,
        (byte) 27,
        (byte) 118
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[10] = (byte) 97;
    numArray5[0] = (byte) 189;
    numArray5[2] = (byte) 46;
    numArray5[9] = (byte) 115;
    numArray5[4] = (byte) 121;
    numArray5[1] = (byte) 199;
    numArray5[6] = (byte) 136;
    numArray5[5] = (byte) 169;
    numArray5[7] = (byte) 224 /*0xE0*/;
    numArray5[3] = (byte) 101;
    numArray5[8] = (byte) 72;
    byte[] numArray6 = new byte[11]
    {
      (byte) 231,
      (byte) 165,
      (byte) 65,
      (byte) 40,
      (byte) 59,
      (byte) 22,
      (byte) 63 /*0x3F*/,
      (byte) 250,
      (byte) 221,
      (byte) 45,
      (byte) 103
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_12731.sspq, 0, (Array) numArray7, 0, 52);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12731.sspr, 0, (Array) numArray7, 0, 52);
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

  internal static string ssp_appserver_12733()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[55];
      byte[] numArray2 = new byte[55]
      {
        (byte) 130,
        (byte) 48 /*0x30*/,
        (byte) 67,
        (byte) 224 /*0xE0*/,
        (byte) 222,
        (byte) 182,
        (byte) 224 /*0xE0*/,
        (byte) 81,
        (byte) 67,
        (byte) 212,
        (byte) 16 /*0x10*/,
        (byte) 146,
        (byte) 65,
        (byte) 131,
        (byte) 234,
        (byte) 193,
        (byte) 171,
        (byte) 9,
        (byte) 7,
        (byte) 216,
        (byte) 196,
        (byte) 112 /*0x70*/,
        (byte) 34,
        (byte) 201,
        (byte) 70,
        (byte) 168,
        (byte) 53,
        (byte) 237,
        (byte) 178,
        (byte) 193,
        (byte) 241,
        (byte) 114,
        (byte) 33,
        (byte) 155,
        (byte) 36,
        (byte) 82,
        (byte) 132,
        (byte) 95,
        (byte) 135,
        (byte) 240 /*0xF0*/,
        (byte) 104,
        (byte) 239,
        (byte) 136,
        (byte) 206,
        (byte) 194,
        (byte) 142,
        (byte) 178,
        (byte) 49,
        (byte) 115,
        (byte) 91,
        (byte) 126,
        (byte) 163,
        (byte) 199,
        (byte) 233,
        (byte) 130
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 102,
        (byte) 223,
        (byte) 135,
        (byte) 240 /*0xF0*/,
        (byte) 117,
        (byte) 207,
        (byte) 219,
        (byte) 126,
        (byte) 76,
        (byte) 25,
        (byte) 33,
        (byte) 193,
        (byte) 143,
        (byte) 162,
        (byte) 248,
        (byte) 46,
        (byte) 6,
        (byte) 18,
        (byte) 26,
        (byte) 185,
        (byte) 184,
        (byte) 198,
        (byte) 111,
        (byte) 135,
        (byte) 222,
        (byte) 6,
        (byte) 88,
        (byte) 243,
        (byte) 200,
        (byte) 246,
        (byte) 107,
        (byte) 182,
        (byte) 32 /*0x20*/,
        (byte) 58,
        (byte) 123,
        (byte) 133,
        (byte) 242,
        (byte) 169,
        (byte) 148,
        (byte) 9,
        (byte) 152,
        (byte) 72,
        (byte) 118,
        (byte) 190,
        (byte) 50,
        (byte) 41,
        (byte) 92,
        (byte) 33,
        (byte) 207,
        (byte) 239,
        (byte) 96 /*0x60*/,
        (byte) 124,
        byte.MaxValue,
        (byte) 145,
        (byte) 101
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[55];
    byte[] numArray5 = new byte[55]
    {
      (byte) 154,
      (byte) 53,
      (byte) 128 /*0x80*/,
      (byte) 242,
      (byte) 159,
      (byte) 130,
      (byte) 227,
      (byte) 158,
      (byte) 113,
      (byte) 165,
      (byte) 95,
      (byte) 151,
      (byte) 60,
      (byte) 75,
      (byte) 129,
      (byte) 22,
      (byte) 112 /*0x70*/,
      (byte) 212,
      (byte) 232,
      (byte) 84,
      (byte) 6,
      (byte) 183,
      (byte) 245,
      (byte) 4,
      (byte) 122,
      (byte) 86,
      (byte) 67,
      (byte) 78,
      (byte) 170,
      (byte) 56,
      (byte) 25,
      (byte) 18,
      (byte) 69,
      (byte) 10,
      (byte) 197,
      (byte) 81,
      (byte) 111,
      (byte) 117,
      (byte) 79,
      (byte) 5,
      (byte) 234,
      (byte) 36,
      (byte) 29,
      (byte) 13,
      (byte) 210,
      (byte) 219,
      (byte) 31 /*0x1F*/,
      (byte) 50,
      (byte) 33,
      (byte) 176 /*0xB0*/,
      (byte) 120,
      (byte) 173,
      (byte) 40,
      (byte) 184,
      (byte) 115
    };
    byte[] numArray6 = new byte[55]
    {
      (byte) 39,
      (byte) 37,
      (byte) 40,
      (byte) 243,
      (byte) 27,
      (byte) 195,
      (byte) 128 /*0x80*/,
      (byte) 40,
      (byte) 155,
      (byte) 105,
      (byte) 251,
      (byte) 77,
      (byte) 213,
      (byte) 97,
      (byte) 191,
      (byte) 31 /*0x1F*/,
      (byte) 175,
      (byte) 124,
      (byte) 57,
      (byte) 92,
      (byte) 28,
      (byte) 126,
      (byte) 193,
      (byte) 206,
      (byte) 135,
      (byte) 195,
      (byte) 61,
      (byte) 207,
      (byte) 10,
      (byte) 196,
      (byte) 31 /*0x1F*/,
      (byte) 139,
      (byte) 45,
      (byte) 100,
      (byte) 79,
      (byte) 241,
      (byte) 45,
      (byte) 135,
      (byte) 211,
      (byte) 66,
      (byte) 112 /*0x70*/,
      (byte) 169,
      (byte) 223,
      (byte) 252,
      (byte) 147,
      (byte) 249,
      (byte) 163,
      (byte) 142,
      (byte) 187,
      (byte) 75,
      (byte) 208 /*0xD0*/,
      (byte) 158,
      (byte) 165,
      (byte) 150,
      (byte) 233
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12734()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[2] = (byte) 149;
      numArray2[8] = (byte) 50;
      numArray2[1] = (byte) 62;
      numArray2[3] = (byte) 243;
      numArray2[4] = (byte) 57;
      numArray2[9] = (byte) 134;
      numArray2[7] = (byte) 81;
      numArray2[5] = (byte) 140;
      numArray2[0] = (byte) 73;
      numArray2[6] = (byte) 76;
      numArray2[10] = (byte) 48 /*0x30*/;
      byte[] numArray3 = new byte[11];
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[5] = (byte) 238;
      numArray3[1] = (byte) 7;
      numArray3[3] = (byte) 56;
      numArray3[8] = (byte) 70;
      numArray3[7] = (byte) 209;
      numArray3[6] = (byte) 97;
      numArray3[4] = (byte) 249;
      numArray3[0] = (byte) 189;
      numArray3[9] = (byte) 117;
      numArray3[10] = (byte) 116;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 85,
      (byte) 49,
      (byte) 227,
      (byte) 128 /*0x80*/,
      (byte) 133,
      (byte) 138,
      (byte) 237,
      (byte) 171,
      (byte) 241,
      (byte) 9,
      (byte) 125
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 227,
      (byte) 237,
      (byte) 57,
      (byte) 78,
      (byte) 195,
      (byte) 179,
      (byte) 100,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 54,
      (byte) 2
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
