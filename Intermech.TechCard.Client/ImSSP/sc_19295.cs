// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19295
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19295
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 107,
    (byte) 44,
    (byte) 18,
    (byte) 81,
    (byte) 10,
    (byte) 64 /*0x40*/,
    (byte) 38,
    (byte) 146,
    (byte) 135,
    (byte) 19,
    (byte) 127 /*0x7F*/,
    (byte) 24,
    (byte) 145,
    (byte) 53,
    (byte) 76,
    (byte) 159,
    (byte) 63 /*0x3F*/,
    (byte) 188,
    (byte) 105,
    (byte) 103,
    (byte) 70,
    (byte) 122,
    (byte) 58,
    (byte) 49,
    (byte) 182,
    (byte) 158,
    (byte) 197,
    (byte) 241,
    (byte) 199,
    (byte) 32 /*0x20*/,
    (byte) 71,
    (byte) 89,
    (byte) 240 /*0xF0*/,
    (byte) 119,
    (byte) 116
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 152,
    (byte) 145,
    (byte) 180,
    (byte) 207,
    (byte) 163,
    (byte) 163,
    (byte) 74,
    (byte) 8,
    (byte) 224 /*0xE0*/,
    (byte) 206,
    (byte) 41,
    (byte) 164,
    (byte) 53,
    (byte) 246,
    (byte) 217,
    (byte) 15,
    (byte) 254,
    (byte) 10,
    (byte) 249,
    (byte) 130,
    (byte) 80 /*0x50*/,
    (byte) 131,
    (byte) 42,
    (byte) 238,
    (byte) 74,
    (byte) 217,
    (byte) 98,
    (byte) 95,
    (byte) 147,
    (byte) 96 /*0x60*/,
    (byte) 8,
    (byte) 45,
    (byte) 91,
    (byte) 90,
    (byte) 152
  };

  internal static string ssp_techcard_19296()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 229,
        (byte) 227,
        (byte) 143,
        (byte) 17,
        (byte) 160 /*0xA0*/,
        (byte) 181,
        (byte) 87,
        (byte) 132,
        (byte) 231,
        (byte) 41,
        (byte) 19,
        (byte) 19,
        (byte) 203,
        (byte) 61,
        (byte) 122,
        (byte) 70,
        (byte) 168,
        (byte) 245,
        (byte) 143
      };
      byte[] numArray3 = new byte[19];
      numArray3[3] = (byte) 209;
      numArray3[1] = (byte) 89;
      numArray3[2] = (byte) 74;
      numArray3[17] = (byte) 69;
      numArray3[8] = (byte) 213;
      numArray3[12] = (byte) 245;
      numArray3[6] = (byte) 155;
      numArray3[7] = (byte) 114;
      numArray3[10] = (byte) 182;
      numArray3[9] = (byte) 69;
      numArray3[16 /*0x10*/] = (byte) 251;
      numArray3[0] = (byte) 78;
      numArray3[4] = (byte) 9;
      numArray3[13] = (byte) 90;
      numArray3[14] = (byte) 153;
      numArray3[15] = (byte) 118;
      numArray3[5] = (byte) 193;
      numArray3[11] = (byte) 199;
      numArray3[18] = (byte) 224 /*0xE0*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[10] = (byte) 136;
    numArray5[1] = (byte) 226;
    numArray5[2] = (byte) 226;
    numArray5[3] = (byte) 77;
    numArray5[4] = (byte) 42;
    numArray5[15] = (byte) 204;
    numArray5[6] = (byte) 36;
    numArray5[16 /*0x10*/] = (byte) 163;
    numArray5[0] = (byte) 230;
    numArray5[12] = (byte) 71;
    numArray5[11] = (byte) 163;
    numArray5[17] = (byte) 129;
    numArray5[5] = (byte) 216;
    numArray5[13] = (byte) 173;
    numArray5[14] = (byte) 109;
    numArray5[9] = (byte) 250;
    numArray5[7] = (byte) 114;
    numArray5[18] = (byte) 20;
    numArray5[8] = (byte) 106;
    byte[] numArray6 = new byte[19];
    numArray6[18] = (byte) 45;
    numArray6[10] = (byte) 107;
    numArray6[17] = (byte) 52;
    numArray6[15] = (byte) 62;
    numArray6[4] = (byte) 67;
    numArray6[3] = (byte) 84;
    numArray6[6] = (byte) 184;
    numArray6[7] = (byte) 192 /*0xC0*/;
    numArray6[5] = (byte) 93;
    numArray6[9] = (byte) 62;
    numArray6[2] = (byte) 238;
    numArray6[14] = (byte) 112 /*0x70*/;
    numArray6[1] = (byte) 244;
    numArray6[13] = (byte) 159;
    numArray6[8] = (byte) 249;
    numArray6[16 /*0x10*/] = (byte) 123;
    numArray6[11] = (byte) 119;
    numArray6[0] = (byte) 63 /*0x3F*/;
    numArray6[12] = (byte) 106;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19297()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[9] = (byte) 151;
      numArray2[7] = (byte) 148;
      numArray2[4] = (byte) 169;
      numArray2[14] = (byte) 143;
      numArray2[12] = (byte) 98;
      numArray2[5] = (byte) 81;
      numArray2[6] = (byte) 95;
      numArray2[11] = (byte) 26;
      numArray2[8] = (byte) 215;
      numArray2[0] = (byte) 155;
      numArray2[10] = (byte) 3;
      numArray2[1] = (byte) 164;
      numArray2[2] = (byte) 114;
      numArray2[13] = (byte) 188;
      numArray2[3] = (byte) 240 /*0xF0*/;
      numArray2[15] = (byte) 32 /*0x20*/;
      numArray2[18] = (byte) 62;
      numArray2[17] = (byte) 15;
      numArray2[16 /*0x10*/] = (byte) 224 /*0xE0*/;
      byte[] numArray3 = new byte[19];
      numArray3[18] = (byte) 158;
      numArray3[2] = (byte) 134;
      numArray3[6] = (byte) 56;
      numArray3[3] = (byte) 90;
      numArray3[8] = (byte) 205;
      numArray3[16 /*0x10*/] = (byte) 110;
      numArray3[11] = (byte) 9;
      numArray3[4] = (byte) 67;
      numArray3[14] = (byte) 194;
      numArray3[9] = (byte) 47;
      numArray3[10] = (byte) 74;
      numArray3[7] = (byte) 98;
      numArray3[12] = (byte) 180;
      numArray3[13] = (byte) 51;
      numArray3[5] = (byte) 69;
      numArray3[15] = (byte) 215;
      numArray3[1] = (byte) 70;
      numArray3[17] = (byte) 17;
      numArray3[0] = (byte) 161;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[6] = (byte) 8;
    numArray5[1] = (byte) 59;
    numArray5[10] = (byte) 150;
    numArray5[3] = (byte) 189;
    numArray5[12] = (byte) 112 /*0x70*/;
    numArray5[14] = (byte) 75;
    numArray5[11] = (byte) 73;
    numArray5[9] = (byte) 8;
    numArray5[16 /*0x10*/] = (byte) 210;
    numArray5[2] = (byte) 69;
    numArray5[18] = (byte) 165;
    numArray5[4] = (byte) 236;
    numArray5[13] = (byte) 119;
    numArray5[15] = (byte) 94;
    numArray5[8] = (byte) 238;
    numArray5[7] = (byte) 219;
    numArray5[0] = (byte) 22;
    numArray5[17] = (byte) 163;
    numArray5[5] = (byte) 90;
    byte[] numArray6 = new byte[19]
    {
      (byte) 181,
      (byte) 34,
      (byte) 86,
      (byte) 244,
      (byte) 180,
      (byte) 116,
      (byte) 207,
      (byte) 56,
      (byte) 12,
      (byte) 152,
      (byte) 176 /*0xB0*/,
      (byte) 110,
      (byte) 164,
      (byte) 59,
      (byte) 151,
      (byte) 204,
      (byte) 254,
      (byte) 204,
      (byte) 115
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_19295.sspq, 0, (Array) numArray7, 0, 35);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19295.sspr, 0, (Array) numArray7, 0, 35);
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
