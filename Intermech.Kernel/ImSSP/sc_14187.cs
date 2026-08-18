// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14187
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14187
{
  internal static string ssp_appserver_14188()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 211,
        (byte) 87,
        (byte) 238,
        (byte) 64 /*0x40*/,
        (byte) 180,
        (byte) 69,
        (byte) 148,
        (byte) 34,
        (byte) 154,
        (byte) 71,
        (byte) 141
      };
      byte[] numArray3 = new byte[11];
      numArray3[5] = (byte) 73;
      numArray3[1] = (byte) 193;
      numArray3[2] = (byte) 70;
      numArray3[8] = (byte) 142;
      numArray3[4] = (byte) 121;
      numArray3[3] = (byte) 196;
      numArray3[6] = (byte) 242;
      numArray3[7] = (byte) 169;
      numArray3[9] = (byte) 129;
      numArray3[0] = (byte) 62;
      numArray3[10] = (byte) 93;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 195,
      (byte) 145,
      (byte) 42,
      (byte) 209,
      (byte) 76,
      (byte) 78,
      (byte) 98,
      (byte) 31 /*0x1F*/,
      (byte) 19,
      (byte) 222,
      (byte) 99
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 137,
      (byte) 146,
      (byte) 239,
      (byte) 3,
      (byte) 114,
      (byte) 0,
      (byte) 32 /*0x20*/,
      (byte) 254,
      (byte) 224 /*0xE0*/,
      (byte) 225,
      (byte) 188
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14189()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[0] = (byte) 5;
      numArray2[1] = (byte) 162;
      numArray2[3] = (byte) 105;
      numArray2[6] = (byte) 224 /*0xE0*/;
      numArray2[4] = (byte) 221;
      numArray2[5] = (byte) 71;
      numArray2[8] = (byte) 190;
      numArray2[7] = (byte) 236;
      numArray2[10] = (byte) 116;
      numArray2[2] = (byte) 199;
      numArray2[9] = (byte) 208 /*0xD0*/;
      byte[] numArray3 = new byte[11];
      numArray3[2] = (byte) 240 /*0xF0*/;
      numArray3[9] = (byte) 66;
      numArray3[3] = (byte) 108;
      numArray3[0] = (byte) 149;
      numArray3[10] = (byte) 73;
      numArray3[6] = (byte) 54;
      numArray3[1] = (byte) 245;
      numArray3[7] = (byte) 45;
      numArray3[8] = (byte) 81;
      numArray3[4] = (byte) 171;
      numArray3[5] = (byte) 224 /*0xE0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 73,
      (byte) 131,
      (byte) 33,
      (byte) 166,
      (byte) 227,
      (byte) 164,
      (byte) 108,
      (byte) 0,
      (byte) 216,
      (byte) 251,
      (byte) 16 /*0x10*/
    };
    byte[] numArray6 = new byte[11];
    numArray6[4] = (byte) 47;
    numArray6[1] = (byte) 74;
    numArray6[2] = (byte) 49;
    numArray6[10] = (byte) 46;
    numArray6[5] = (byte) 219;
    numArray6[0] = (byte) 125;
    numArray6[6] = (byte) 67;
    numArray6[3] = (byte) 66;
    numArray6[8] = (byte) 78;
    numArray6[9] = (byte) 48 /*0x30*/;
    numArray6[7] = (byte) 38;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14190()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 22,
        (byte) 143,
        (byte) 49,
        (byte) 172,
        (byte) 144 /*0x90*/,
        (byte) 213,
        (byte) 30,
        (byte) 110,
        (byte) 121,
        (byte) 231,
        (byte) 206
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 133,
        (byte) 133,
        (byte) 35,
        (byte) 35,
        (byte) 190,
        (byte) 29,
        (byte) 238,
        (byte) 108,
        (byte) 236,
        (byte) 62,
        (byte) 241
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 253,
      (byte) 80 /*0x50*/,
      (byte) 117,
      (byte) 252,
      (byte) 26,
      (byte) 143,
      (byte) 177,
      (byte) 187,
      (byte) 54,
      (byte) 133,
      (byte) 14
    };
    byte[] numArray6 = new byte[11];
    numArray6[2] = (byte) 176 /*0xB0*/;
    numArray6[1] = (byte) 131;
    numArray6[8] = (byte) 71;
    numArray6[6] = (byte) 200;
    numArray6[4] = (byte) 195;
    numArray6[5] = (byte) 184;
    numArray6[0] = (byte) 192 /*0xC0*/;
    numArray6[3] = (byte) 131;
    numArray6[7] = (byte) 166;
    numArray6[9] = (byte) 134;
    numArray6[10] = (byte) 37;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
