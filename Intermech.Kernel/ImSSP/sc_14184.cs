// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14184
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14184
{
  private static byte[] sspq = new byte[44]
  {
    (byte) 230,
    (byte) 132,
    (byte) 83,
    (byte) 93,
    (byte) 252,
    (byte) 238,
    (byte) 210,
    (byte) 163,
    (byte) 243,
    (byte) 9,
    (byte) 202,
    (byte) 3,
    (byte) 240 /*0xF0*/,
    (byte) 12,
    (byte) 12,
    (byte) 180,
    (byte) 58,
    (byte) 25,
    (byte) 97,
    (byte) 253,
    (byte) 209,
    (byte) 173,
    (byte) 207,
    (byte) 148,
    (byte) 247,
    (byte) 175,
    (byte) 78,
    (byte) 177,
    (byte) 60,
    (byte) 58,
    (byte) 168,
    (byte) 49,
    (byte) 145,
    (byte) 83,
    (byte) 163,
    (byte) 233,
    (byte) 215,
    (byte) 7,
    (byte) 158,
    (byte) 23,
    (byte) 104,
    (byte) 116,
    (byte) 2,
    (byte) 8
  };
  private static byte[] sspr = new byte[44]
  {
    (byte) 37,
    (byte) 174,
    (byte) 130,
    (byte) 48 /*0x30*/,
    (byte) 121,
    (byte) 37,
    (byte) 170,
    (byte) 243,
    (byte) 159,
    (byte) 43,
    (byte) 182,
    (byte) 209,
    (byte) 184,
    (byte) 53,
    (byte) 81,
    (byte) 241,
    (byte) 37,
    (byte) 72,
    (byte) 29,
    (byte) 194,
    (byte) 24,
    (byte) 114,
    (byte) 213,
    (byte) 99,
    (byte) 90,
    (byte) 81,
    (byte) 19,
    (byte) 87,
    (byte) 38,
    (byte) 111,
    (byte) 133,
    (byte) 75,
    (byte) 45,
    (byte) 120,
    (byte) 47,
    (byte) 52,
    (byte) 53,
    (byte) 33,
    (byte) 102,
    (byte) 137,
    (byte) 190,
    (byte) 100,
    (byte) 12,
    (byte) 119
  };

  internal static string ssp_appserver_14185()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[26];
      byte[] numArray2 = new byte[26];
      numArray2[19] = (byte) 102;
      numArray2[23] = (byte) 225;
      numArray2[2] = (byte) 102;
      numArray2[3] = (byte) 191;
      numArray2[7] = (byte) 244;
      numArray2[5] = (byte) 145;
      numArray2[4] = (byte) 158;
      numArray2[18] = (byte) 179;
      numArray2[6] = (byte) 160 /*0xA0*/;
      numArray2[9] = (byte) 225;
      numArray2[10] = (byte) 219;
      numArray2[11] = (byte) 200;
      numArray2[12] = (byte) 193;
      numArray2[15] = (byte) 123;
      numArray2[16 /*0x10*/] = (byte) 222;
      numArray2[1] = (byte) 138;
      numArray2[0] = (byte) 94;
      numArray2[17] = (byte) 160 /*0xA0*/;
      numArray2[13] = (byte) 185;
      numArray2[14] = (byte) 150;
      numArray2[8] = (byte) 37;
      numArray2[21] = (byte) 233;
      numArray2[22] = (byte) 163;
      numArray2[20] = (byte) 56;
      numArray2[24] = (byte) 107;
      numArray2[25] = (byte) 64 /*0x40*/;
      byte[] numArray3 = new byte[26]
      {
        (byte) 20,
        (byte) 193,
        (byte) 118,
        (byte) 196,
        (byte) 14,
        (byte) 99,
        (byte) 122,
        (byte) 158,
        (byte) 164,
        (byte) 65,
        (byte) 147,
        (byte) 74,
        (byte) 27,
        (byte) 31 /*0x1F*/,
        (byte) 112 /*0x70*/,
        (byte) 122,
        (byte) 63 /*0x3F*/,
        (byte) 208 /*0xD0*/,
        (byte) 234,
        (byte) 230,
        (byte) 6,
        (byte) 181,
        (byte) 172,
        (byte) 181,
        (byte) 207,
        (byte) 37
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[44];
      byte[] response = new byte[44];
      Array.Copy((Array) sc_14184.sspq, 0, (Array) numArray4, 0, 44);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_14184.sspr, 0, (Array) numArray4, 0, 44);
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
    byte[] numArray5 = new byte[26];
    byte[] numArray6 = new byte[26]
    {
      (byte) 13,
      (byte) 69,
      (byte) 244,
      (byte) 87,
      (byte) 100,
      (byte) 144 /*0x90*/,
      (byte) 120,
      (byte) 15,
      (byte) 108,
      (byte) 47,
      (byte) 163,
      (byte) 56,
      (byte) 180,
      (byte) 190,
      (byte) 102,
      (byte) 77,
      (byte) 159,
      (byte) 32 /*0x20*/,
      (byte) 103,
      (byte) 185,
      (byte) 96 /*0x60*/,
      (byte) 152,
      (byte) 114,
      (byte) 156,
      (byte) 105,
      (byte) 35
    };
    byte[] numArray7 = new byte[26];
    numArray7[4] = (byte) 133;
    numArray7[13] = (byte) 190;
    numArray7[11] = (byte) 98;
    numArray7[15] = (byte) 94;
    numArray7[17] = (byte) 29;
    numArray7[5] = (byte) 174;
    numArray7[6] = (byte) 40;
    numArray7[3] = (byte) 142;
    numArray7[7] = (byte) 181;
    numArray7[12] = (byte) 208 /*0xD0*/;
    numArray7[22] = (byte) 195;
    numArray7[23] = (byte) 183;
    numArray7[18] = (byte) 56;
    numArray7[10] = (byte) 74;
    numArray7[1] = (byte) 195;
    numArray7[8] = (byte) 225;
    numArray7[0] = (byte) 65;
    numArray7[2] = (byte) 241;
    numArray7[19] = (byte) 183;
    numArray7[16 /*0x10*/] = (byte) 130;
    numArray7[20] = (byte) 164;
    numArray7[21] = (byte) 156;
    numArray7[14] = (byte) 238;
    numArray7[9] = (byte) 27;
    numArray7[24] = (byte) 79;
    numArray7[25] = (byte) 31 /*0x1F*/;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 26);
    for (int index = 0; index < 26; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
