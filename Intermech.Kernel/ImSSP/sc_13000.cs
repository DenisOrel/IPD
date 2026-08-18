// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13000
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13000
{
  private static byte[] sspq = new byte[36]
  {
    (byte) 211,
    (byte) 25,
    (byte) 30,
    (byte) 77,
    (byte) 222,
    (byte) 214,
    (byte) 31 /*0x1F*/,
    (byte) 33,
    (byte) 43,
    (byte) 76,
    (byte) 173,
    (byte) 31 /*0x1F*/,
    (byte) 227,
    (byte) 3,
    (byte) 182,
    (byte) 51,
    (byte) 123,
    (byte) 162,
    (byte) 135,
    (byte) 175,
    (byte) 20,
    (byte) 125,
    (byte) 246,
    (byte) 148,
    (byte) 148,
    (byte) 148,
    (byte) 254,
    (byte) 158,
    (byte) 189,
    (byte) 129,
    (byte) 113,
    (byte) 239,
    (byte) 155,
    (byte) 97,
    (byte) 167,
    (byte) 128 /*0x80*/
  };
  private static byte[] sspr = new byte[36]
  {
    (byte) 67,
    (byte) 79,
    (byte) 32 /*0x20*/,
    (byte) 90,
    (byte) 86,
    (byte) 204,
    (byte) 115,
    (byte) 157,
    (byte) 216,
    (byte) 144 /*0x90*/,
    (byte) 205,
    (byte) 209,
    (byte) 193,
    (byte) 48 /*0x30*/,
    (byte) 176 /*0xB0*/,
    (byte) 89,
    (byte) 96 /*0x60*/,
    (byte) 69,
    (byte) 172,
    (byte) 107,
    (byte) 232,
    (byte) 17,
    (byte) 26,
    (byte) 185,
    (byte) 83,
    (byte) 148,
    (byte) 219,
    (byte) 159,
    (byte) 175,
    (byte) 241,
    (byte) 119,
    (byte) 46,
    (byte) 67,
    (byte) 227,
    (byte) 31 /*0x1F*/,
    (byte) 91
  };

  internal static string ssp_appserver_13001()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[30];
      byte[] numArray2 = new byte[30]
      {
        (byte) 213,
        (byte) 213,
        (byte) 207,
        (byte) 94,
        (byte) 161,
        (byte) 12,
        (byte) 224 /*0xE0*/,
        (byte) 183,
        (byte) 172,
        (byte) 218,
        (byte) 166,
        (byte) 28,
        (byte) 251,
        (byte) 232,
        (byte) 135,
        (byte) 80 /*0x50*/,
        (byte) 114,
        (byte) 83,
        (byte) 83,
        (byte) 99,
        (byte) 248,
        (byte) 209,
        (byte) 48 /*0x30*/,
        (byte) 10,
        (byte) 67,
        (byte) 107,
        (byte) 152,
        (byte) 7,
        (byte) 6,
        (byte) 84
      };
      byte[] numArray3 = new byte[30]
      {
        (byte) 33,
        (byte) 54,
        (byte) 178,
        (byte) 254,
        (byte) 164,
        (byte) 173,
        (byte) 174,
        (byte) 51,
        (byte) 90,
        (byte) 79,
        (byte) 26,
        (byte) 39,
        (byte) 241,
        (byte) 175,
        (byte) 70,
        (byte) 184,
        (byte) 114,
        (byte) 149,
        (byte) 186,
        (byte) 230,
        (byte) 252,
        (byte) 221,
        (byte) 93,
        (byte) 83,
        (byte) 39,
        (byte) 30,
        (byte) 188,
        (byte) 22,
        (byte) 187,
        (byte) 124
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[30];
    byte[] numArray5 = new byte[30]
    {
      (byte) 39,
      (byte) 207,
      (byte) 105,
      (byte) 2,
      (byte) 147,
      (byte) 124,
      (byte) 51,
      (byte) 65,
      (byte) 205,
      (byte) 133,
      (byte) 131,
      (byte) 235,
      (byte) 96 /*0x60*/,
      (byte) 104,
      (byte) 162,
      (byte) 138,
      (byte) 202,
      (byte) 134,
      (byte) 215,
      (byte) 181,
      (byte) 21,
      (byte) 188,
      (byte) 3,
      (byte) 164,
      (byte) 197,
      (byte) 26,
      (byte) 53,
      (byte) 116,
      (byte) 217,
      (byte) 177
    };
    byte[] numArray6 = new byte[30]
    {
      (byte) 200,
      (byte) 90,
      (byte) 118,
      (byte) 240 /*0xF0*/,
      (byte) 168,
      (byte) 99,
      (byte) 41,
      (byte) 105,
      (byte) 133,
      (byte) 37,
      (byte) 248,
      (byte) 128 /*0x80*/,
      (byte) 166,
      (byte) 124,
      (byte) 227,
      byte.MaxValue,
      (byte) 143,
      (byte) 101,
      (byte) 176 /*0xB0*/,
      (byte) 118,
      (byte) 237,
      (byte) 141,
      (byte) 205,
      (byte) 70,
      (byte) 48 /*0x30*/,
      (byte) 250,
      (byte) 52,
      (byte) 160 /*0xA0*/,
      (byte) 217,
      (byte) 34
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 30);
    for (int index = 0; index < 30; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[14];
    byte[] response = new byte[14];
    Array.Copy((Array) sc_13000.sspq, 0, (Array) numArray7, 0, 14);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13000.sspr, 0, (Array) numArray7, 0, 14);
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

  internal static string ssp_appserver_13002()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 124,
        (byte) 60,
        (byte) 205,
        (byte) 247,
        (byte) 46,
        (byte) 208 /*0xD0*/,
        (byte) 106,
        (byte) 203,
        (byte) 196,
        (byte) 60
      };
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 16 /*0x10*/;
      numArray3[7] = (byte) 178;
      numArray3[4] = (byte) 15;
      numArray3[8] = (byte) 173;
      numArray3[2] = (byte) 254;
      numArray3[1] = (byte) 71;
      numArray3[6] = (byte) 62;
      numArray3[3] = (byte) 39;
      numArray3[5] = (byte) 8;
      numArray3[9] = (byte) 49;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 167,
      (byte) 1,
      (byte) 149,
      (byte) 243,
      (byte) 145,
      (byte) 182,
      (byte) 247,
      (byte) 166,
      (byte) 54,
      (byte) 49
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 20,
      (byte) 244,
      (byte) 205,
      (byte) 227,
      (byte) 171,
      (byte) 162,
      (byte) 204,
      (byte) 163,
      (byte) 5,
      (byte) 195
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13003()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[7] = (byte) 101;
      numArray2[1] = (byte) 148;
      numArray2[2] = (byte) 102;
      numArray2[6] = (byte) 16 /*0x10*/;
      numArray2[3] = (byte) 104;
      numArray2[5] = (byte) 89;
      numArray2[4] = (byte) 77;
      numArray2[9] = (byte) 97;
      numArray2[8] = (byte) 142;
      numArray2[0] = (byte) 19;
      byte[] numArray3 = new byte[10]
      {
        (byte) 25,
        (byte) 194,
        (byte) 177,
        (byte) 135,
        (byte) 168,
        (byte) 71,
        (byte) 237,
        (byte) 36,
        (byte) 65,
        (byte) 176 /*0xB0*/
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
      (byte) 220,
      (byte) 217,
      (byte) 20,
      (byte) 205,
      (byte) 150,
      (byte) 66,
      (byte) 22,
      (byte) 63 /*0x3F*/,
      (byte) 11,
      (byte) 166
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 53,
      (byte) 236,
      (byte) 125,
      (byte) 239,
      (byte) 90,
      (byte) 216,
      (byte) 143,
      (byte) 43,
      (byte) 48 /*0x30*/,
      (byte) 134
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13004(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 124;
    sourceArray1[1] = (byte) 19;
    sourceArray1[13] = (byte) 19;
    sourceArray1[11] = (byte) 124;
    sourceArray1[39] = (byte) 100;
    sourceArray1[35] = (byte) 252;
    sourceArray1[34] = (byte) 48 /*0x30*/;
    sourceArray1[7] = (byte) 129;
    sourceArray1[8] = (byte) 151;
    sourceArray1[45] = (byte) 198;
    sourceArray1[46] = (byte) 82;
    sourceArray1[3] = (byte) 66;
    sourceArray1[38] = (byte) 131;
    sourceArray1[0] = (byte) 59;
    sourceArray1[14] = (byte) 76;
    sourceArray1[15] = (byte) 181;
    sourceArray1[23] = (byte) 122;
    sourceArray1[17] = (byte) 29;
    sourceArray1[28] = (byte) 105;
    sourceArray1[5] = (byte) 52;
    sourceArray1[20] = (byte) 143;
    sourceArray1[21] = (byte) 56;
    sourceArray1[9] = (byte) 194;
    sourceArray1[4] = (byte) 114;
    sourceArray1[22] = (byte) 140;
    sourceArray1[25] = (byte) 120;
    sourceArray1[26] = (byte) 33;
    sourceArray1[27] = (byte) 174;
    sourceArray1[24] = (byte) 34;
    sourceArray1[10] = (byte) 175;
    sourceArray1[30] = (byte) 208 /*0xD0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 98;
    sourceArray1[32 /*0x20*/] = (byte) 247;
    sourceArray1[12] = (byte) 195;
    sourceArray1[18] = (byte) 251;
    sourceArray1[41] = (byte) 88;
    sourceArray1[36] = (byte) 84;
    sourceArray1[2] = (byte) 107;
    sourceArray1[42] = (byte) 125;
    sourceArray1[33] = (byte) 66;
    sourceArray1[40] = (byte) 163;
    sourceArray1[16 /*0x10*/] = (byte) 100;
    sourceArray1[19] = (byte) 138;
    sourceArray1[43] = (byte) 10;
    sourceArray1[44] = (byte) 230;
    sourceArray1[37] = (byte) 227;
    sourceArray1[6] = (byte) 57;
    sourceArray1[47] = (byte) 44;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[25] = (byte) 84;
    sourceArray2[18] = (byte) 25;
    sourceArray2[2] = (byte) 65;
    sourceArray2[19] = (byte) 169;
    sourceArray2[7] = (byte) 41;
    sourceArray2[47] = (byte) 48 /*0x30*/;
    sourceArray2[6] = (byte) 121;
    sourceArray2[43] = (byte) 245;
    sourceArray2[14] = (byte) 232;
    sourceArray2[0] = (byte) 96 /*0x60*/;
    sourceArray2[10] = (byte) 58;
    sourceArray2[11] = (byte) 199;
    sourceArray2[20] = (byte) 50;
    sourceArray2[13] = (byte) 109;
    sourceArray2[39] = (byte) 105;
    sourceArray2[5] = (byte) 22;
    sourceArray2[21] = (byte) 247;
    sourceArray2[32 /*0x20*/] = (byte) 31 /*0x1F*/;
    sourceArray2[29] = (byte) 60;
    sourceArray2[1] = (byte) 237;
    sourceArray2[33] = (byte) 43;
    sourceArray2[8] = (byte) 59;
    sourceArray2[46] = (byte) 165;
    sourceArray2[23] = (byte) 195;
    sourceArray2[44] = byte.MaxValue;
    sourceArray2[12] = (byte) 1;
    sourceArray2[41] = (byte) 247;
    sourceArray2[27] = (byte) 17;
    sourceArray2[28] = (byte) 198;
    sourceArray2[4] = (byte) 17;
    sourceArray2[26] = (byte) 170;
    sourceArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    sourceArray2[30] = (byte) 205;
    sourceArray2[3] = (byte) 211;
    sourceArray2[34] = (byte) 170;
    sourceArray2[35] = (byte) 140;
    sourceArray2[36] = (byte) 68;
    sourceArray2[37] = (byte) 37;
    sourceArray2[15] = (byte) 32 /*0x20*/;
    sourceArray2[38] = (byte) 45;
    sourceArray2[40] = (byte) 140;
    sourceArray2[24] = (byte) 123;
    sourceArray2[17] = (byte) 194;
    sourceArray2[16 /*0x10*/] = (byte) 40;
    sourceArray2[42] = (byte) 124;
    sourceArray2[22] = (byte) 87;
    sourceArray2[9] = (byte) 122;
    sourceArray2[45] = (byte) 105;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[22];
    byte[] response2 = new byte[22];
    Array.Copy((Array) sc_13000.sspq, 14, (Array) numArray2, 0, 22);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13000.sspr, 14, (Array) numArray2, 0, 22);
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
}
