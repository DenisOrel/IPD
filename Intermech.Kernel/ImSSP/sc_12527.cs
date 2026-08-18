// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12527
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12527
{
  private static byte[] sspq = new byte[15]
  {
    (byte) 97,
    (byte) 219,
    (byte) 145,
    (byte) 136,
    (byte) 83,
    (byte) 111,
    (byte) 169,
    (byte) 118,
    (byte) 100,
    (byte) 32 /*0x20*/,
    (byte) 65,
    (byte) 7,
    (byte) 135,
    (byte) 223,
    (byte) 166
  };
  private static byte[] sspr = new byte[15]
  {
    (byte) 204,
    (byte) 223,
    (byte) 58,
    (byte) 205,
    (byte) 177,
    (byte) 130,
    (byte) 91,
    (byte) 113,
    (byte) 45,
    (byte) 179,
    (byte) 220,
    (byte) 99,
    (byte) 126,
    (byte) 184,
    (byte) 164
  };

  internal static int ssp_appserver_12528(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[44] = (byte) 175;
    sourceArray1[15] = (byte) 203;
    sourceArray1[1] = (byte) 188;
    sourceArray1[3] = (byte) 92;
    sourceArray1[13] = (byte) 160 /*0xA0*/;
    sourceArray1[8] = (byte) 245;
    sourceArray1[42] = (byte) 28;
    sourceArray1[11] = (byte) 36;
    sourceArray1[14] = (byte) 191;
    sourceArray1[9] = (byte) 243;
    sourceArray1[19] = (byte) 30;
    sourceArray1[38] = (byte) 63 /*0x3F*/;
    sourceArray1[12] = (byte) 155;
    sourceArray1[21] = (byte) 59;
    sourceArray1[26] = (byte) 52;
    sourceArray1[0] = (byte) 155;
    sourceArray1[45] = (byte) 93;
    sourceArray1[17] = (byte) 51;
    sourceArray1[28] = (byte) 233;
    sourceArray1[5] = (byte) 109;
    sourceArray1[20] = (byte) 16 /*0x10*/;
    sourceArray1[7] = (byte) 238;
    sourceArray1[30] = (byte) 90;
    sourceArray1[23] = (byte) 34;
    sourceArray1[24] = (byte) 237;
    sourceArray1[25] = (byte) 122;
    sourceArray1[27] = (byte) 193;
    sourceArray1[47] = (byte) 235;
    sourceArray1[16 /*0x10*/] = (byte) 250;
    sourceArray1[29] = (byte) 197;
    sourceArray1[33] = (byte) 65;
    sourceArray1[31 /*0x1F*/] = (byte) 244;
    sourceArray1[32 /*0x20*/] = (byte) 40;
    sourceArray1[40] = (byte) 18;
    sourceArray1[34] = (byte) 70;
    sourceArray1[35] = (byte) 25;
    sourceArray1[36] = (byte) 105;
    sourceArray1[37] = (byte) 121;
    sourceArray1[46] = (byte) 53;
    sourceArray1[39] = (byte) 149;
    sourceArray1[18] = (byte) 13;
    sourceArray1[41] = (byte) 170;
    sourceArray1[4] = (byte) 101;
    sourceArray1[43] = (byte) 46;
    sourceArray1[10] = (byte) 229;
    sourceArray1[6] = (byte) 123;
    sourceArray1[22] = (byte) 113;
    sourceArray1[2] = (byte) 29;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 130,
      (byte) 127 /*0x7F*/,
      (byte) 125,
      (byte) 68,
      (byte) 116,
      (byte) 38,
      (byte) 45,
      (byte) 89,
      (byte) 186,
      (byte) 165,
      (byte) 180,
      (byte) 82,
      (byte) 185,
      (byte) 148,
      (byte) 193,
      (byte) 111,
      (byte) 90,
      (byte) 103,
      (byte) 69,
      (byte) 251,
      (byte) 98,
      (byte) 11,
      (byte) 3,
      (byte) 10,
      (byte) 52,
      (byte) 100,
      (byte) 111,
      (byte) 17,
      (byte) 119,
      (byte) 170,
      (byte) 72,
      (byte) 14,
      (byte) 68,
      (byte) 61,
      (byte) 88,
      (byte) 17,
      (byte) 226,
      (byte) 189,
      (byte) 209,
      (byte) 39,
      (byte) 161,
      (byte) 182,
      (byte) 115,
      (byte) 167,
      (byte) 130,
      (byte) 160 /*0xA0*/,
      (byte) 43,
      (byte) 20
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[15];
    byte[] response2 = new byte[15];
    Array.Copy((Array) sc_12527.sspq, 0, (Array) numArray2, 0, 15);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12527.sspr, 0, (Array) numArray2, 0, 15);
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

  internal static string ssp_appserver_12529()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[9] = (byte) 243;
      numArray2[8] = (byte) 215;
      numArray2[2] = (byte) 91;
      numArray2[0] = (byte) 39;
      numArray2[5] = (byte) 67;
      numArray2[3] = (byte) 219;
      numArray2[6] = (byte) 159;
      numArray2[1] = (byte) 209;
      numArray2[7] = (byte) 115;
      numArray2[4] = (byte) 236;
      byte[] numArray3 = new byte[10];
      numArray3[4] = (byte) 195;
      numArray3[5] = (byte) 253;
      numArray3[3] = (byte) 181;
      numArray3[2] = (byte) 213;
      numArray3[1] = (byte) 90;
      numArray3[6] = (byte) 9;
      numArray3[0] = (byte) 91;
      numArray3[7] = (byte) 13;
      numArray3[8] = (byte) 171;
      numArray3[9] = (byte) 134;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[9] = (byte) 177;
    numArray5[1] = (byte) 49;
    numArray5[2] = (byte) 211;
    numArray5[3] = (byte) 145;
    numArray5[7] = (byte) 59;
    numArray5[6] = (byte) 29;
    numArray5[4] = (byte) 186;
    numArray5[5] = (byte) 210;
    numArray5[8] = (byte) 29;
    numArray5[0] = (byte) 108;
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 2;
    numArray6[3] = (byte) 71;
    numArray6[7] = (byte) 241;
    numArray6[0] = (byte) 114;
    numArray6[8] = (byte) 124;
    numArray6[4] = (byte) 38;
    numArray6[6] = (byte) 39;
    numArray6[5] = (byte) 115;
    numArray6[9] = (byte) 14;
    numArray6[2] = (byte) 135;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
