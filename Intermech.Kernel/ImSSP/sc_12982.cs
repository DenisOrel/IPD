// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12982
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12982
{
  private static byte[] sspq = new byte[17]
  {
    (byte) 189,
    (byte) 158,
    (byte) 158,
    (byte) 237,
    (byte) 114,
    (byte) 246,
    (byte) 199,
    (byte) 125,
    (byte) 125,
    (byte) 7,
    (byte) 239,
    (byte) 161,
    (byte) 144 /*0x90*/,
    (byte) 75,
    (byte) 248,
    (byte) 244,
    (byte) 26
  };
  private static byte[] sspr = new byte[17]
  {
    (byte) 221,
    (byte) 64 /*0x40*/,
    (byte) 109,
    (byte) 251,
    (byte) 7,
    (byte) 153,
    (byte) 146,
    (byte) 235,
    (byte) 132,
    (byte) 97,
    (byte) 63 /*0x3F*/,
    (byte) 136,
    (byte) 44,
    (byte) 179,
    (byte) 160 /*0xA0*/,
    (byte) 52,
    (byte) 10
  };

  internal static string ssp_appserver_12983()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 112 /*0x70*/;
      numArray2[3] = (byte) 207;
      numArray2[7] = (byte) 23;
      numArray2[8] = (byte) 120;
      numArray2[4] = (byte) 105;
      numArray2[1] = (byte) 14;
      numArray2[0] = (byte) 150;
      numArray2[2] = (byte) 18;
      numArray2[5] = (byte) 22;
      numArray2[9] = (byte) 249;
      byte[] numArray3 = new byte[10]
      {
        (byte) 182,
        (byte) 245,
        (byte) 125,
        (byte) 188,
        (byte) 32 /*0x20*/,
        (byte) 83,
        (byte) 43,
        (byte) 50,
        (byte) 120,
        (byte) 101
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[2] = (byte) 218;
    numArray5[0] = (byte) 191;
    numArray5[4] = (byte) 244;
    numArray5[1] = (byte) 172;
    numArray5[3] = (byte) 36;
    numArray5[5] = (byte) 92;
    numArray5[6] = (byte) 113;
    numArray5[7] = (byte) 160 /*0xA0*/;
    numArray5[8] = (byte) 72;
    numArray5[9] = (byte) 121;
    byte[] numArray6 = new byte[10];
    numArray6[5] = (byte) 40;
    numArray6[7] = (byte) 107;
    numArray6[9] = (byte) 214;
    numArray6[6] = (byte) 95;
    numArray6[4] = (byte) 89;
    numArray6[0] = (byte) 158;
    numArray6[1] = (byte) 18;
    numArray6[3] = (byte) 244;
    numArray6[2] = (byte) 30;
    numArray6[8] = (byte) 155;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12984()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 83,
        (byte) 35,
        (byte) 142,
        (byte) 242,
        (byte) 124,
        (byte) 186,
        (byte) 7,
        (byte) 89,
        (byte) 204,
        (byte) 244
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 209,
        (byte) 16 /*0x10*/,
        (byte) 171,
        (byte) 183,
        (byte) 80 /*0x50*/,
        (byte) 164,
        (byte) 177,
        (byte) 88,
        (byte) 14,
        (byte) 134
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[4] = (byte) 217;
    numArray5[6] = (byte) 10;
    numArray5[2] = (byte) 156;
    numArray5[9] = (byte) 27;
    numArray5[1] = (byte) 196;
    numArray5[5] = (byte) 219;
    numArray5[7] = (byte) 90;
    numArray5[0] = (byte) 47;
    numArray5[8] = (byte) 214;
    numArray5[3] = (byte) 167;
    byte[] numArray6 = new byte[10];
    numArray6[8] = (byte) 153;
    numArray6[4] = (byte) 51;
    numArray6[6] = (byte) 16 /*0x10*/;
    numArray6[3] = (byte) 70;
    numArray6[1] = (byte) 214;
    numArray6[5] = (byte) 118;
    numArray6[0] = (byte) 103;
    numArray6[7] = (byte) 194;
    numArray6[9] = (byte) 224 /*0xE0*/;
    numArray6[2] = (byte) 224 /*0xE0*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[17];
    byte[] response = new byte[17];
    Array.Copy((Array) sc_12982.sspq, 0, (Array) numArray7, 0, 17);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12982.sspr, 0, (Array) numArray7, 0, 17);
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
