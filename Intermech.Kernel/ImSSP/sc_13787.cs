// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13787
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13787
{
  private static byte[] sspq = new byte[42]
  {
    (byte) 103,
    (byte) 87,
    (byte) 68,
    (byte) 88,
    (byte) 181,
    (byte) 198,
    (byte) 237,
    (byte) 170,
    (byte) 130,
    (byte) 141,
    (byte) 168,
    (byte) 160 /*0xA0*/,
    (byte) 117,
    (byte) 32 /*0x20*/,
    (byte) 37,
    (byte) 60,
    (byte) 135,
    (byte) 233,
    (byte) 6,
    (byte) 183,
    (byte) 146,
    (byte) 11,
    (byte) 24,
    (byte) 248,
    (byte) 102,
    (byte) 92,
    (byte) 25,
    (byte) 175,
    (byte) 71,
    (byte) 162,
    (byte) 132,
    (byte) 197,
    (byte) 153,
    (byte) 194,
    (byte) 245,
    (byte) 109,
    (byte) 153,
    (byte) 244,
    (byte) 180,
    (byte) 148,
    (byte) 51,
    (byte) 83
  };
  private static byte[] sspr = new byte[42]
  {
    (byte) 124,
    (byte) 104,
    (byte) 81,
    (byte) 4,
    (byte) 163,
    (byte) 189,
    (byte) 82,
    (byte) 137,
    (byte) 229,
    (byte) 200,
    (byte) 181,
    (byte) 227,
    (byte) 13,
    (byte) 214,
    (byte) 232,
    (byte) 127 /*0x7F*/,
    (byte) 64 /*0x40*/,
    (byte) 231,
    (byte) 152,
    (byte) 65,
    (byte) 24,
    (byte) 237,
    (byte) 67,
    (byte) 53,
    (byte) 101,
    (byte) 32 /*0x20*/,
    (byte) 92,
    (byte) 173,
    (byte) 192 /*0xC0*/,
    (byte) 69,
    (byte) 39,
    (byte) 68,
    (byte) 241,
    (byte) 165,
    (byte) 119,
    (byte) 43,
    (byte) 16 /*0x10*/,
    (byte) 247,
    (byte) 231,
    (byte) 125,
    (byte) 66,
    (byte) 165
  };

  internal static string ssp_appserver_13788()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 77,
        (byte) 210,
        (byte) 220,
        (byte) 108,
        (byte) 134,
        (byte) 93,
        (byte) 196,
        (byte) 221,
        (byte) 76,
        (byte) 106,
        (byte) 90,
        (byte) 33,
        (byte) 254,
        (byte) 46,
        (byte) 38,
        (byte) 153,
        (byte) 9,
        (byte) 104,
        (byte) 0,
        (byte) 45,
        (byte) 228,
        (byte) 170
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 59,
        (byte) 173,
        (byte) 74,
        (byte) 116,
        (byte) 119,
        (byte) 188,
        (byte) 220,
        (byte) 117,
        (byte) 226,
        (byte) 151,
        (byte) 173,
        (byte) 187,
        (byte) 73,
        (byte) 136,
        (byte) 223,
        (byte) 37,
        (byte) 8,
        (byte) 244,
        (byte) 214,
        (byte) 197,
        (byte) 212,
        (byte) 57
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_13787.sspq, 0, (Array) numArray4, 0, 42);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13787.sspr, 0, (Array) numArray4, 0, 42);
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
    byte[] numArray5 = new byte[22];
    byte[] numArray6 = new byte[22]
    {
      (byte) 172,
      (byte) 153,
      (byte) 240 /*0xF0*/,
      (byte) 105,
      (byte) 242,
      (byte) 224 /*0xE0*/,
      (byte) 8,
      (byte) 168,
      (byte) 35,
      (byte) 100,
      (byte) 16 /*0x10*/,
      (byte) 27,
      (byte) 49,
      (byte) 91,
      (byte) 63 /*0x3F*/,
      (byte) 75,
      (byte) 241,
      (byte) 89,
      (byte) 172,
      (byte) 62,
      (byte) 98,
      (byte) 223
    };
    byte[] numArray7 = new byte[22]
    {
      (byte) 131,
      (byte) 102,
      (byte) 196,
      (byte) 135,
      (byte) 234,
      (byte) 89,
      (byte) 64 /*0x40*/,
      (byte) 106,
      (byte) 102,
      (byte) 46,
      (byte) 109,
      (byte) 144 /*0x90*/,
      (byte) 252,
      (byte) 19,
      (byte) 10,
      (byte) 106,
      (byte) 214,
      (byte) 220,
      (byte) 229,
      (byte) 149,
      (byte) 243,
      (byte) 51
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13789()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[8] = (byte) 94;
      numArray2[1] = (byte) 183;
      numArray2[2] = (byte) 162;
      numArray2[3] = (byte) 146;
      numArray2[4] = (byte) 52;
      numArray2[6] = (byte) 131;
      numArray2[5] = (byte) 114;
      numArray2[7] = (byte) 62;
      numArray2[0] = (byte) 42;
      numArray2[9] = (byte) 127 /*0x7F*/;
      byte[] numArray3 = new byte[10];
      numArray3[5] = (byte) 90;
      numArray3[9] = (byte) 72;
      numArray3[2] = (byte) 159;
      numArray3[3] = (byte) 88;
      numArray3[4] = (byte) 166;
      numArray3[1] = (byte) 11;
      numArray3[7] = (byte) 54;
      numArray3[6] = (byte) 151;
      numArray3[8] = (byte) 152;
      numArray3[0] = (byte) 141;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[6] = (byte) 158;
    numArray5[1] = (byte) 181;
    numArray5[2] = (byte) 80 /*0x50*/;
    numArray5[8] = (byte) 242;
    numArray5[4] = (byte) 144 /*0x90*/;
    numArray5[0] = (byte) 63 /*0x3F*/;
    numArray5[5] = (byte) 47;
    numArray5[3] = (byte) 5;
    numArray5[7] = (byte) 151;
    numArray5[9] = (byte) 253;
    byte[] numArray6 = new byte[10]
    {
      (byte) 119,
      (byte) 76,
      (byte) 187,
      (byte) 17,
      (byte) 182,
      (byte) 162,
      (byte) 75,
      (byte) 162,
      (byte) 171,
      (byte) 35
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13790(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 1;
    sourceArray1[30] = (byte) 30;
    sourceArray1[2] = (byte) 138;
    sourceArray1[31 /*0x1F*/] = (byte) 36;
    sourceArray1[37] = (byte) 9;
    sourceArray1[5] = (byte) 145;
    sourceArray1[7] = (byte) 250;
    sourceArray1[41] = (byte) 73;
    sourceArray1[3] = (byte) 141;
    sourceArray1[9] = (byte) 41;
    sourceArray1[10] = (byte) 131;
    sourceArray1[33] = (byte) 176 /*0xB0*/;
    sourceArray1[1] = (byte) 217;
    sourceArray1[13] = (byte) 237;
    sourceArray1[25] = (byte) 46;
    sourceArray1[15] = (byte) 22;
    sourceArray1[38] = (byte) 165;
    sourceArray1[18] = (byte) 156;
    sourceArray1[20] = (byte) 9;
    sourceArray1[12] = (byte) 153;
    sourceArray1[22] = (byte) 107;
    sourceArray1[21] = (byte) 13;
    sourceArray1[36] = (byte) 19;
    sourceArray1[23] = (byte) 115;
    sourceArray1[0] = (byte) 143;
    sourceArray1[26] = (byte) 108;
    sourceArray1[16 /*0x10*/] = (byte) 230;
    sourceArray1[27] = (byte) 168;
    sourceArray1[28] = (byte) 223;
    sourceArray1[29] = (byte) 218;
    sourceArray1[11] = (byte) 86;
    sourceArray1[43] = (byte) 198;
    sourceArray1[32 /*0x20*/] = (byte) 199;
    sourceArray1[39] = (byte) 107;
    sourceArray1[47] = (byte) 114;
    sourceArray1[19] = (byte) 110;
    sourceArray1[17] = (byte) 158;
    sourceArray1[40] = (byte) 209;
    sourceArray1[6] = (byte) 238;
    sourceArray1[24] = (byte) 94;
    sourceArray1[8] = (byte) 153;
    sourceArray1[4] = (byte) 77;
    sourceArray1[42] = (byte) 91;
    sourceArray1[35] = (byte) 86;
    sourceArray1[44] = (byte) 240 /*0xF0*/;
    sourceArray1[45] = (byte) 64 /*0x40*/;
    sourceArray1[46] = (byte) 41;
    sourceArray1[14] = (byte) 240 /*0xF0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 170,
      (byte) 130,
      (byte) 14,
      (byte) 62,
      (byte) 249,
      (byte) 128 /*0x80*/,
      (byte) 76,
      (byte) 132,
      (byte) 51,
      (byte) 27,
      (byte) 121,
      (byte) 76,
      (byte) 211,
      (byte) 212,
      (byte) 77,
      (byte) 33,
      (byte) 85,
      (byte) 231,
      (byte) 115,
      (byte) 126,
      (byte) 225,
      (byte) 172,
      (byte) 131,
      (byte) 156,
      (byte) 189,
      (byte) 149,
      (byte) 21,
      (byte) 95,
      (byte) 196,
      (byte) 15,
      (byte) 119,
      (byte) 146,
      (byte) 141,
      (byte) 85,
      (byte) 21,
      (byte) 54,
      (byte) 122,
      (byte) 59,
      (byte) 29,
      (byte) 46,
      (byte) 71,
      (byte) 146,
      (byte) 206,
      (byte) 18,
      (byte) 105,
      (byte) 146,
      (byte) 184,
      (byte) 75
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
