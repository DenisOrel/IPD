// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13480
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13480
{
  private static byte[] sspq = new byte[21]
  {
    (byte) 167,
    (byte) 239,
    (byte) 75,
    (byte) 173,
    (byte) 251,
    (byte) 107,
    (byte) 225,
    (byte) 13,
    (byte) 179,
    (byte) 153,
    (byte) 248,
    (byte) 253,
    (byte) 65,
    (byte) 223,
    (byte) 170,
    (byte) 176 /*0xB0*/,
    (byte) 174,
    (byte) 181,
    (byte) 96 /*0x60*/,
    (byte) 32 /*0x20*/,
    (byte) 17
  };
  private static byte[] sspr = new byte[21]
  {
    (byte) 77,
    (byte) 123,
    (byte) 82,
    (byte) 176 /*0xB0*/,
    (byte) 239,
    (byte) 10,
    (byte) 232,
    (byte) 26,
    (byte) 29,
    (byte) 160 /*0xA0*/,
    (byte) 84,
    (byte) 248,
    (byte) 41,
    (byte) 185,
    (byte) 89,
    (byte) 37,
    (byte) 169,
    (byte) 109,
    (byte) 2,
    (byte) 187,
    (byte) 212
  };

  internal static int ssp_appserver_13481(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 238;
    sourceArray1[1] = (byte) 174;
    sourceArray1[24] = (byte) 166;
    sourceArray1[9] = (byte) 248;
    sourceArray1[4] = (byte) 184;
    sourceArray1[13] = (byte) 136;
    sourceArray1[12] = (byte) 36;
    sourceArray1[7] = (byte) 36;
    sourceArray1[8] = (byte) 155;
    sourceArray1[34] = (byte) 40;
    sourceArray1[10] = (byte) 155;
    sourceArray1[11] = (byte) 209;
    sourceArray1[6] = (byte) 79;
    sourceArray1[40] = (byte) 2;
    sourceArray1[42] = (byte) 141;
    sourceArray1[41] = (byte) 11;
    sourceArray1[16 /*0x10*/] = (byte) 89;
    sourceArray1[17] = (byte) 126;
    sourceArray1[3] = (byte) 226;
    sourceArray1[19] = (byte) 231;
    sourceArray1[14] = (byte) 57;
    sourceArray1[21] = (byte) 199;
    sourceArray1[43] = (byte) 172;
    sourceArray1[2] = (byte) 194;
    sourceArray1[22] = (byte) 244;
    sourceArray1[25] = (byte) 126;
    sourceArray1[44] = (byte) 123;
    sourceArray1[23] = (byte) 50;
    sourceArray1[28] = (byte) 107;
    sourceArray1[29] = (byte) 21;
    sourceArray1[20] = (byte) 160 /*0xA0*/;
    sourceArray1[26] = (byte) 213;
    sourceArray1[30] = (byte) 3;
    sourceArray1[33] = (byte) 121;
    sourceArray1[18] = (byte) 40;
    sourceArray1[35] = (byte) 238;
    sourceArray1[36] = (byte) 147;
    sourceArray1[37] = (byte) 124;
    sourceArray1[5] = (byte) 207;
    sourceArray1[39] = (byte) 48 /*0x30*/;
    sourceArray1[32 /*0x20*/] = (byte) 171;
    sourceArray1[46] = (byte) 94;
    sourceArray1[0] = (byte) 118;
    sourceArray1[15] = (byte) 77;
    sourceArray1[27] = (byte) 172;
    sourceArray1[45] = (byte) 77;
    sourceArray1[31 /*0x1F*/] = (byte) 170;
    sourceArray1[47] = (byte) 66;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 58,
      (byte) 61,
      (byte) 112 /*0x70*/,
      (byte) 61,
      (byte) 52,
      (byte) 83,
      (byte) 11,
      (byte) 141,
      (byte) 194,
      (byte) 9,
      (byte) 71,
      (byte) 227,
      (byte) 69,
      (byte) 254,
      (byte) 227,
      (byte) 200,
      (byte) 235,
      (byte) 86,
      (byte) 202,
      (byte) 39,
      (byte) 242,
      (byte) 53,
      (byte) 202,
      (byte) 238,
      (byte) 172,
      (byte) 141,
      (byte) 121,
      (byte) 98,
      (byte) 126,
      (byte) 7,
      (byte) 4,
      (byte) 93,
      (byte) 213,
      (byte) 253,
      (byte) 39,
      (byte) 30,
      (byte) 86,
      (byte) 200,
      (byte) 131,
      (byte) 252,
      (byte) 169,
      (byte) 55,
      (byte) 233,
      (byte) 1,
      (byte) 227,
      (byte) 160 /*0xA0*/,
      (byte) 105
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13482(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 180,
      (byte) 225,
      (byte) 128 /*0x80*/,
      (byte) 152,
      (byte) 169,
      (byte) 142,
      (byte) 91,
      (byte) 126,
      (byte) 64 /*0x40*/,
      (byte) 116,
      (byte) 113,
      (byte) 216,
      (byte) 226,
      (byte) 26,
      (byte) 102,
      (byte) 193,
      (byte) 160 /*0xA0*/,
      (byte) 247,
      (byte) 15,
      (byte) 154,
      (byte) 220,
      (byte) 7,
      (byte) 230,
      (byte) 21,
      (byte) 182,
      (byte) 155,
      (byte) 219,
      (byte) 194,
      (byte) 133,
      (byte) 67,
      (byte) 50,
      (byte) 145,
      (byte) 45,
      (byte) 49,
      (byte) 107,
      (byte) 177,
      (byte) 97,
      (byte) 128 /*0x80*/,
      (byte) 230,
      (byte) 87,
      (byte) 250,
      (byte) 201,
      (byte) 98,
      (byte) 5,
      (byte) 196,
      (byte) 43,
      (byte) 95,
      (byte) 153
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 132,
      (byte) 28,
      (byte) 234,
      (byte) 52,
      (byte) 77,
      (byte) 38,
      (byte) 11,
      (byte) 85,
      (byte) 21,
      (byte) 224 /*0xE0*/,
      (byte) 11,
      (byte) 99,
      (byte) 195,
      (byte) 152,
      (byte) 96 /*0x60*/,
      (byte) 196,
      (byte) 148,
      (byte) 193,
      (byte) 236,
      (byte) 132,
      (byte) 251,
      (byte) 200,
      (byte) 161,
      (byte) 61,
      (byte) 60,
      (byte) 76,
      (byte) 174,
      (byte) 64 /*0x40*/,
      (byte) 37,
      (byte) 197,
      (byte) 198,
      (byte) 180,
      (byte) 65,
      (byte) 139,
      (byte) 189,
      (byte) 137,
      (byte) 222,
      (byte) 18,
      (byte) 209,
      (byte) 244,
      (byte) 234,
      (byte) 104,
      (byte) 211,
      (byte) 225,
      (byte) 183,
      (byte) 211,
      (byte) 216,
      (byte) 145
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[21];
    byte[] response2 = new byte[21];
    Array.Copy((Array) sc_13480.sspq, 0, (Array) numArray2, 0, 21);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13480.sspr, 0, (Array) numArray2, 0, 21);
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

  internal static string ssp_appserver_13483()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 159;
      numArray2[2] = (byte) 164;
      numArray2[6] = (byte) 59;
      numArray2[5] = (byte) 139;
      numArray2[4] = (byte) 80 /*0x50*/;
      numArray2[7] = (byte) 65;
      numArray2[1] = (byte) 18;
      numArray2[3] = (byte) 225;
      numArray2[8] = (byte) 242;
      numArray2[9] = (byte) 232;
      byte[] numArray3 = new byte[10]
      {
        (byte) 121,
        (byte) 136,
        (byte) 164,
        (byte) 39,
        (byte) 212,
        (byte) 126,
        (byte) 251,
        (byte) 110,
        (byte) 66,
        (byte) 100
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
      (byte) 101,
      (byte) 195,
      (byte) 228,
      (byte) 128 /*0x80*/,
      (byte) 174,
      (byte) 235,
      (byte) 25,
      (byte) 60,
      (byte) 144 /*0x90*/,
      byte.MaxValue
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 95,
      (byte) 172,
      (byte) 101,
      (byte) 213,
      (byte) 45,
      (byte) 134,
      (byte) 63 /*0x3F*/,
      (byte) 87,
      (byte) 183,
      (byte) 116
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13484(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 26,
      (byte) 86,
      (byte) 210,
      (byte) 113,
      (byte) 166,
      (byte) 16 /*0x10*/,
      (byte) 106,
      (byte) 31 /*0x1F*/,
      (byte) 246,
      (byte) 109,
      (byte) 203,
      (byte) 240 /*0xF0*/,
      (byte) 254,
      (byte) 107,
      (byte) 130,
      (byte) 56,
      (byte) 20,
      (byte) 62,
      (byte) 65,
      (byte) 63 /*0x3F*/,
      (byte) 29,
      (byte) 108,
      (byte) 10,
      (byte) 140,
      (byte) 86,
      (byte) 217,
      (byte) 171,
      (byte) 2,
      (byte) 35,
      (byte) 250,
      (byte) 236,
      (byte) 173,
      (byte) 182,
      (byte) 156,
      (byte) 219,
      (byte) 72,
      (byte) 6,
      (byte) 50,
      (byte) 186,
      (byte) 192 /*0xC0*/,
      (byte) 251,
      (byte) 163,
      (byte) 237,
      (byte) 19,
      (byte) 227,
      (byte) 250,
      (byte) 27,
      (byte) 209
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 93,
      (byte) 110,
      (byte) 173,
      (byte) 205,
      (byte) 15,
      (byte) 7,
      (byte) 17,
      (byte) 129,
      (byte) 106,
      (byte) 46,
      (byte) 80 /*0x50*/,
      (byte) 53,
      (byte) 204,
      (byte) 139,
      (byte) 84,
      (byte) 121,
      (byte) 9,
      (byte) 6,
      (byte) 14,
      (byte) 219,
      (byte) 107,
      (byte) 60,
      (byte) 47,
      (byte) 56,
      (byte) 178,
      (byte) 148,
      (byte) 254,
      (byte) 162,
      (byte) 145,
      (byte) 70,
      (byte) 247,
      (byte) 151,
      (byte) 188,
      (byte) 127 /*0x7F*/,
      (byte) 8,
      (byte) 213,
      (byte) 220,
      (byte) 25,
      (byte) 121,
      (byte) 218,
      (byte) 129,
      (byte) 117,
      (byte) 223,
      (byte) 65,
      (byte) 56,
      (byte) 75,
      (byte) 244,
      (byte) 59
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
