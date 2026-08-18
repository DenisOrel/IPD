// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19253
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19253
{
  private static byte[] sspq = new byte[12]
  {
    (byte) 16 /*0x10*/,
    (byte) 243,
    (byte) 134,
    (byte) 169,
    (byte) 125,
    (byte) 26,
    (byte) 233,
    (byte) 186,
    (byte) 101,
    (byte) 159,
    (byte) 219,
    (byte) 30
  };
  private static byte[] sspr = new byte[12]
  {
    (byte) 65,
    (byte) 122,
    (byte) 107,
    (byte) 69,
    (byte) 218,
    (byte) 53,
    (byte) 20,
    (byte) 44,
    (byte) 107,
    (byte) 71,
    (byte) 140,
    (byte) 27
  };

  internal static string ssp_techcard_19254()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 46,
        (byte) 191,
        (byte) 166,
        (byte) 156,
        (byte) 181,
        (byte) 139,
        (byte) 156,
        (byte) 34,
        (byte) 41,
        (byte) 136,
        (byte) 141,
        (byte) 97,
        (byte) 247,
        (byte) 155,
        (byte) 94,
        (byte) 5,
        (byte) 212,
        (byte) 175,
        (byte) 33
      };
      byte[] numArray3 = new byte[19];
      numArray3[12] = (byte) 64 /*0x40*/;
      numArray3[11] = (byte) 188;
      numArray3[6] = (byte) 68;
      numArray3[3] = (byte) 113;
      numArray3[4] = (byte) 175;
      numArray3[5] = (byte) 120;
      numArray3[16 /*0x10*/] = (byte) 108;
      numArray3[14] = (byte) 242;
      numArray3[8] = (byte) 73;
      numArray3[1] = (byte) 253;
      numArray3[10] = (byte) 149;
      numArray3[7] = (byte) 159;
      numArray3[9] = (byte) 0;
      numArray3[2] = (byte) 58;
      numArray3[0] = (byte) 101;
      numArray3[15] = (byte) 43;
      numArray3[13] = (byte) 29;
      numArray3[17] = (byte) 72;
      numArray3[18] = (byte) 133;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 238,
      (byte) 5,
      (byte) 49,
      (byte) 99,
      (byte) 122,
      (byte) 44,
      (byte) 182,
      (byte) 160 /*0xA0*/,
      (byte) 38,
      (byte) 218,
      (byte) 214,
      (byte) 191,
      (byte) 161,
      (byte) 73,
      (byte) 211,
      (byte) 212,
      (byte) 150,
      (byte) 238,
      (byte) 119
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 80 /*0x50*/,
      (byte) 22,
      (byte) 211,
      (byte) 60,
      (byte) 35,
      (byte) 88,
      (byte) 164,
      (byte) 127 /*0x7F*/,
      (byte) 4,
      (byte) 205,
      (byte) 102,
      (byte) 135,
      (byte) 245,
      (byte) 33,
      (byte) 74,
      (byte) 127 /*0x7F*/,
      (byte) 222,
      (byte) 33,
      (byte) 207
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19255()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 66,
        (byte) 146,
        (byte) 6,
        (byte) 91,
        (byte) 240 /*0xF0*/,
        (byte) 211,
        (byte) 151,
        (byte) 196,
        (byte) 197,
        (byte) 203,
        (byte) 26,
        (byte) 160 /*0xA0*/,
        (byte) 85,
        (byte) 185,
        (byte) 172,
        (byte) 51,
        (byte) 18,
        (byte) 154,
        (byte) 16 /*0x10*/
      };
      byte[] numArray3 = new byte[19];
      numArray3[3] = (byte) 98;
      numArray3[15] = (byte) 145;
      numArray3[2] = (byte) 108;
      numArray3[5] = (byte) 241;
      numArray3[4] = (byte) 242;
      numArray3[8] = (byte) 211;
      numArray3[6] = (byte) 129;
      numArray3[10] = (byte) 112 /*0x70*/;
      numArray3[1] = (byte) 126;
      numArray3[9] = (byte) 60;
      numArray3[16 /*0x10*/] = (byte) 58;
      numArray3[11] = (byte) 244;
      numArray3[12] = (byte) 199;
      numArray3[13] = (byte) 35;
      numArray3[14] = (byte) 150;
      numArray3[17] = (byte) 4;
      numArray3[0] = (byte) 24;
      numArray3[7] = (byte) 81;
      numArray3[18] = (byte) 107;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_19253.sspq, 0, (Array) numArray4, 0, 12);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19253.sspr, 0, (Array) numArray4, 0, 12);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 12,
      (byte) 210,
      (byte) 86,
      (byte) 77,
      (byte) 9,
      (byte) 24,
      (byte) 28,
      (byte) 190,
      (byte) 163,
      (byte) 84,
      (byte) 175,
      (byte) 80 /*0x50*/,
      (byte) 62,
      (byte) 254,
      (byte) 202,
      (byte) 219,
      (byte) 192 /*0xC0*/,
      (byte) 65,
      (byte) 212
    };
    byte[] numArray7 = new byte[19];
    numArray7[5] = (byte) 121;
    numArray7[1] = (byte) 84;
    numArray7[9] = (byte) 198;
    numArray7[10] = (byte) 245;
    numArray7[8] = (byte) 30;
    numArray7[17] = (byte) 240 /*0xF0*/;
    numArray7[13] = (byte) 229;
    numArray7[7] = (byte) 234;
    numArray7[4] = (byte) 170;
    numArray7[2] = (byte) 147;
    numArray7[3] = (byte) 215;
    numArray7[11] = (byte) 214;
    numArray7[12] = (byte) 48 /*0x30*/;
    numArray7[15] = (byte) 241;
    numArray7[14] = (byte) 112 /*0x70*/;
    numArray7[18] = (byte) 244;
    numArray7[6] = (byte) 144 /*0x90*/;
    numArray7[0] = (byte) 215;
    numArray7[16 /*0x10*/] = (byte) 69;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
