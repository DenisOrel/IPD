// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12993
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12993
{
  internal static string ssp_appserver_12994()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[9] = (byte) 88;
      numArray2[0] = (byte) 180;
      numArray2[2] = (byte) 122;
      numArray2[3] = (byte) 70;
      numArray2[6] = (byte) 80 /*0x50*/;
      numArray2[5] = (byte) 124;
      numArray2[4] = (byte) 178;
      numArray2[1] = (byte) 72;
      numArray2[8] = (byte) 111;
      numArray2[7] = (byte) 101;
      byte[] numArray3 = new byte[10]
      {
        (byte) 192 /*0xC0*/,
        (byte) 150,
        (byte) 221,
        (byte) 176 /*0xB0*/,
        (byte) 21,
        (byte) 48 /*0x30*/,
        (byte) 151,
        (byte) 41,
        (byte) 61,
        (byte) 185
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
      (byte) 211,
      (byte) 107,
      (byte) 9,
      (byte) 48 /*0x30*/,
      (byte) 182,
      (byte) 227,
      (byte) 31 /*0x1F*/,
      (byte) 185,
      (byte) 142,
      (byte) 1
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 192 /*0xC0*/,
      (byte) 46,
      (byte) 46,
      (byte) 138,
      (byte) 164,
      (byte) 9,
      (byte) 108,
      (byte) 126,
      (byte) 1,
      (byte) 114
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12995()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 119,
        (byte) 235,
        (byte) 33,
        (byte) 157,
        (byte) 13,
        (byte) 134,
        (byte) 110,
        (byte) 165,
        (byte) 195,
        (byte) 137
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 209;
      numArray3[3] = (byte) 216;
      numArray3[2] = (byte) 105;
      numArray3[4] = (byte) 135;
      numArray3[7] = (byte) 21;
      numArray3[5] = (byte) 192 /*0xC0*/;
      numArray3[9] = (byte) 6;
      numArray3[8] = (byte) 191;
      numArray3[0] = (byte) 246;
      numArray3[6] = (byte) 165;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 76,
      (byte) 153,
      (byte) 129,
      (byte) 80 /*0x50*/,
      (byte) 159,
      (byte) 73,
      (byte) 183,
      (byte) 168,
      (byte) 51,
      (byte) 201
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 16 /*0x10*/,
      (byte) 159,
      (byte) 178,
      (byte) 187,
      (byte) 164,
      (byte) 198,
      (byte) 2,
      (byte) 9,
      (byte) 202,
      (byte) 170
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12996()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[6] = (byte) 128 /*0x80*/;
      numArray2[4] = (byte) 45;
      numArray2[2] = (byte) 122;
      numArray2[5] = (byte) 168;
      numArray2[10] = (byte) 133;
      numArray2[3] = (byte) 110;
      numArray2[8] = (byte) 125;
      numArray2[7] = (byte) 79;
      numArray2[1] = (byte) 173;
      numArray2[9] = byte.MaxValue;
      numArray2[0] = (byte) 38;
      byte[] numArray3 = new byte[11];
      numArray3[10] = (byte) 53;
      numArray3[0] = (byte) 102;
      numArray3[8] = (byte) 191;
      numArray3[3] = (byte) 247;
      numArray3[4] = (byte) 66;
      numArray3[5] = (byte) 69;
      numArray3[1] = (byte) 112 /*0x70*/;
      numArray3[7] = (byte) 92;
      numArray3[2] = (byte) 214;
      numArray3[9] = (byte) 137;
      numArray3[6] = (byte) 167;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[10] = (byte) 232;
    numArray5[3] = (byte) 50;
    numArray5[2] = (byte) 122;
    numArray5[0] = (byte) 56;
    numArray5[4] = (byte) 39;
    numArray5[5] = (byte) 253;
    numArray5[9] = (byte) 109;
    numArray5[6] = (byte) 28;
    numArray5[1] = (byte) 253;
    numArray5[7] = (byte) 130;
    numArray5[8] = (byte) 202;
    byte[] numArray6 = new byte[11];
    numArray6[4] = (byte) 156;
    numArray6[1] = (byte) 5;
    numArray6[2] = (byte) 153;
    numArray6[3] = (byte) 113;
    numArray6[0] = (byte) 10;
    numArray6[9] = (byte) 161;
    numArray6[7] = (byte) 88;
    numArray6[10] = (byte) 169;
    numArray6[8] = (byte) 225;
    numArray6[6] = (byte) 231;
    numArray6[5] = (byte) 30;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
