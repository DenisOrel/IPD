// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_690
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_690
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 68,
    (byte) 221,
    (byte) 33,
    (byte) 223,
    (byte) 161,
    (byte) 95,
    (byte) 150,
    (byte) 149,
    (byte) 146,
    (byte) 228,
    (byte) 214,
    (byte) 242,
    (byte) 93,
    (byte) 139,
    (byte) 141,
    (byte) 189,
    (byte) 183,
    (byte) 243,
    (byte) 51,
    (byte) 102,
    (byte) 99,
    (byte) 156,
    (byte) 237,
    (byte) 51,
    (byte) 22,
    (byte) 187,
    (byte) 80 /*0x50*/,
    (byte) 164,
    (byte) 91,
    (byte) 198,
    (byte) 241,
    (byte) 201,
    (byte) 13,
    (byte) 72,
    (byte) 171
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 73,
    (byte) 35,
    (byte) 249,
    (byte) 71,
    (byte) 96 /*0x60*/,
    (byte) 63 /*0x3F*/,
    (byte) 181,
    (byte) 152,
    (byte) 165,
    (byte) 155,
    (byte) 47,
    (byte) 34,
    (byte) 204,
    (byte) 215,
    (byte) 213,
    (byte) 112 /*0x70*/,
    (byte) 40,
    (byte) 138,
    (byte) 126,
    (byte) 91,
    (byte) 144 /*0x90*/,
    (byte) 101,
    (byte) 210,
    (byte) 3,
    (byte) 127 /*0x7F*/,
    (byte) 50,
    (byte) 151,
    (byte) 211,
    (byte) 167,
    (byte) 5,
    (byte) 254,
    (byte) 200,
    (byte) 52,
    (byte) 187,
    (byte) 188
  };

  internal static string ssp_automatch_691()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[4] = (byte) 204;
      numArray2[18] = (byte) 205;
      numArray2[16 /*0x10*/] = (byte) 215;
      numArray2[10] = (byte) 67;
      numArray2[22] = (byte) 25;
      numArray2[5] = (byte) 150;
      numArray2[6] = (byte) 85;
      numArray2[7] = (byte) 64 /*0x40*/;
      numArray2[11] = (byte) 162;
      numArray2[9] = (byte) 236;
      numArray2[1] = (byte) 175;
      numArray2[15] = (byte) 211;
      numArray2[12] = (byte) 51;
      numArray2[19] = (byte) 169;
      numArray2[14] = (byte) 21;
      numArray2[2] = (byte) 203;
      numArray2[17] = (byte) 159;
      numArray2[3] = (byte) 45;
      numArray2[13] = (byte) 186;
      numArray2[8] = (byte) 124;
      numArray2[20] = (byte) 66;
      numArray2[21] = (byte) 72;
      numArray2[0] = (byte) 168;
      byte[] numArray3 = new byte[23]
      {
        (byte) 111,
        (byte) 238,
        (byte) 44,
        (byte) 244,
        (byte) 163,
        (byte) 191,
        (byte) 231,
        (byte) 67,
        (byte) 223,
        (byte) 48 /*0x30*/,
        (byte) 88,
        (byte) 91,
        (byte) 163,
        (byte) 37,
        (byte) 181,
        (byte) 127 /*0x7F*/,
        (byte) 145,
        (byte) 38,
        (byte) 39,
        (byte) 222,
        (byte) 233,
        (byte) 68,
        (byte) 43
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      byte[] response = new byte[35];
      Array.Copy((Array) sc_690.sspq, 0, (Array) numArray4, 0, 35);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_690.sspr, 0, (Array) numArray4, 0, 35);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23]
    {
      (byte) 129,
      (byte) 101,
      (byte) 10,
      (byte) 159,
      (byte) 38,
      (byte) 24,
      (byte) 218,
      (byte) 168,
      (byte) 204,
      (byte) 195,
      (byte) 160 /*0xA0*/,
      (byte) 41,
      (byte) 109,
      (byte) 230,
      (byte) 30,
      (byte) 28,
      (byte) 3,
      (byte) 194,
      (byte) 238,
      (byte) 54,
      (byte) 168,
      (byte) 239,
      (byte) 238
    };
    byte[] numArray7 = new byte[23];
    numArray7[1] = (byte) 241;
    numArray7[19] = (byte) 70;
    numArray7[9] = (byte) 120;
    numArray7[4] = (byte) 89;
    numArray7[3] = (byte) 104;
    numArray7[5] = (byte) 63 /*0x3F*/;
    numArray7[6] = (byte) 169;
    numArray7[7] = (byte) 136;
    numArray7[8] = (byte) 99;
    numArray7[20] = (byte) 12;
    numArray7[17] = (byte) 55;
    numArray7[22] = (byte) 175;
    numArray7[12] = (byte) 230;
    numArray7[13] = (byte) 53;
    numArray7[14] = (byte) 149;
    numArray7[15] = (byte) 89;
    numArray7[16 /*0x10*/] = (byte) 236;
    numArray7[10] = (byte) 53;
    numArray7[18] = (byte) 254;
    numArray7[11] = (byte) 208 /*0xD0*/;
    numArray7[2] = (byte) 73;
    numArray7[21] = (byte) 244;
    numArray7[0] = (byte) 167;
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_automatch_692()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 237,
        (byte) 6,
        (byte) 31 /*0x1F*/,
        (byte) 17,
        (byte) 3,
        (byte) 146,
        (byte) 118,
        (byte) 65,
        (byte) 185,
        (byte) 22,
        (byte) 39,
        (byte) 160 /*0xA0*/,
        (byte) 220,
        (byte) 218,
        (byte) 182,
        (byte) 186,
        (byte) 19,
        (byte) 141,
        (byte) 218,
        (byte) 198,
        (byte) 111,
        (byte) 176 /*0xB0*/,
        (byte) 181
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 2,
        (byte) 146,
        (byte) 190,
        (byte) 198,
        (byte) 34,
        (byte) 163,
        (byte) 118,
        (byte) 75,
        (byte) 14,
        (byte) 42,
        (byte) 192 /*0xC0*/,
        (byte) 82,
        (byte) 175,
        (byte) 186,
        (byte) 93,
        (byte) 39,
        (byte) 252,
        (byte) 157,
        (byte) 13,
        (byte) 111,
        (byte) 69,
        (byte) 133,
        (byte) 13
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 206,
      (byte) 127 /*0x7F*/,
      (byte) 83,
      (byte) 83,
      (byte) 45,
      (byte) 51,
      (byte) 253,
      (byte) 33,
      (byte) 5,
      (byte) 6,
      (byte) 198,
      (byte) 3,
      (byte) 220,
      (byte) 122,
      (byte) 224 /*0xE0*/,
      (byte) 107,
      (byte) 154,
      (byte) 246,
      (byte) 38,
      (byte) 233,
      (byte) 24,
      (byte) 71,
      (byte) 184
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 141,
      (byte) 99,
      (byte) 239,
      (byte) 141,
      (byte) 191,
      (byte) 167,
      (byte) 194,
      (byte) 141,
      (byte) 84,
      (byte) 45,
      (byte) 10,
      (byte) 95,
      (byte) 176 /*0xB0*/,
      (byte) 234,
      (byte) 33,
      (byte) 58,
      (byte) 157,
      (byte) 59,
      (byte) 85,
      (byte) 64 /*0x40*/,
      (byte) 245,
      (byte) 242,
      (byte) 241
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
