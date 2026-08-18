// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19614
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19614
{
  private static byte[] sspq = new byte[48 /*0x30*/]
  {
    (byte) 113,
    (byte) 0,
    (byte) 225,
    (byte) 172,
    (byte) 182,
    (byte) 200,
    (byte) 141,
    (byte) 190,
    (byte) 77,
    (byte) 67,
    (byte) 178,
    (byte) 214,
    (byte) 138,
    (byte) 1,
    (byte) 122,
    (byte) 217,
    (byte) 0,
    (byte) 79,
    (byte) 249,
    (byte) 232,
    (byte) 27,
    (byte) 117,
    (byte) 85,
    (byte) 88,
    (byte) 119,
    (byte) 200,
    (byte) 212,
    (byte) 2,
    (byte) 253,
    (byte) 205,
    (byte) 206,
    (byte) 46,
    (byte) 139,
    (byte) 91,
    (byte) 178,
    (byte) 16 /*0x10*/,
    (byte) 90,
    (byte) 74,
    (byte) 137,
    (byte) 8,
    (byte) 56,
    (byte) 70,
    (byte) 90,
    (byte) 250,
    (byte) 235,
    (byte) 174,
    (byte) 138,
    (byte) 115
  };
  private static byte[] sspr = new byte[48 /*0x30*/]
  {
    (byte) 98,
    (byte) 149,
    (byte) 205,
    (byte) 177,
    (byte) 155,
    (byte) 107,
    (byte) 21,
    (byte) 31 /*0x1F*/,
    (byte) 67,
    (byte) 154,
    (byte) 253,
    (byte) 88,
    (byte) 170,
    (byte) 230,
    (byte) 86,
    (byte) 24,
    (byte) 157,
    (byte) 74,
    (byte) 30,
    (byte) 217,
    (byte) 224 /*0xE0*/,
    (byte) 182,
    (byte) 88,
    (byte) 158,
    (byte) 192 /*0xC0*/,
    (byte) 204,
    (byte) 61,
    (byte) 89,
    (byte) 197,
    (byte) 92,
    (byte) 198,
    (byte) 74,
    (byte) 157,
    (byte) 107,
    (byte) 115,
    (byte) 184,
    (byte) 215,
    (byte) 109,
    (byte) 240 /*0xF0*/,
    (byte) 243,
    (byte) 32 /*0x20*/,
    (byte) 168,
    (byte) 147,
    (byte) 30,
    (byte) 177,
    (byte) 150,
    (byte) 96 /*0x60*/,
    (byte) 158
  };

  internal static string ssp_techcard_19615()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[2];
      byte[] numArray2 = new byte[2]
      {
        (byte) 205,
        (byte) 76
      };
      byte[] numArray3 = new byte[2]
      {
        (byte) 207,
        (byte) 33
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[2];
    byte[] numArray5 = new byte[2]{ (byte) 161, (byte) 213 };
    byte[] numArray6 = new byte[2]{ (byte) 10, (byte) 51 };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 2);
    for (int index = 0; index < 2; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_19614.sspq, 0, (Array) numArray7, 0, 20);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19614.sspr, 0, (Array) numArray7, 0, 20);
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

  internal static string ssp_techcard_19616()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 66,
        (byte) 116,
        (byte) 133,
        (byte) 250,
        (byte) 166,
        (byte) 109,
        (byte) 67,
        (byte) 52,
        (byte) 69,
        (byte) 11,
        (byte) 108,
        (byte) 124,
        (byte) 143,
        (byte) 22,
        (byte) 203,
        (byte) 78,
        (byte) 140,
        (byte) 199,
        (byte) 233
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 233,
        (byte) 142,
        (byte) 0,
        (byte) 145,
        (byte) 191,
        (byte) 66,
        (byte) 49,
        (byte) 64 /*0x40*/,
        (byte) 134,
        (byte) 184,
        (byte) 21,
        (byte) 147,
        (byte) 67,
        (byte) 194,
        (byte) 159,
        (byte) 23,
        (byte) 198,
        (byte) 190,
        (byte) 150
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28];
      byte[] response = new byte[28];
      Array.Copy((Array) sc_19614.sspq, 20, (Array) numArray4, 0, 28);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19614.sspr, 20, (Array) numArray4, 0, 28);
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
      (byte) 136,
      (byte) 16 /*0x10*/,
      (byte) 53,
      (byte) 218,
      (byte) 70,
      (byte) 140,
      (byte) 106,
      (byte) 132,
      (byte) 80 /*0x50*/,
      (byte) 21,
      (byte) 203,
      (byte) 108,
      (byte) 112 /*0x70*/,
      (byte) 8,
      (byte) 110,
      (byte) 174,
      (byte) 193,
      (byte) 231,
      (byte) 63 /*0x3F*/
    };
    byte[] numArray7 = new byte[19];
    numArray7[10] = (byte) 78;
    numArray7[6] = (byte) 92;
    numArray7[2] = (byte) 219;
    numArray7[3] = (byte) 150;
    numArray7[4] = (byte) 181;
    numArray7[5] = (byte) 217;
    numArray7[8] = (byte) 61;
    numArray7[7] = (byte) 243;
    numArray7[13] = (byte) 229;
    numArray7[14] = (byte) 248;
    numArray7[12] = (byte) 65;
    numArray7[1] = (byte) 41;
    numArray7[11] = (byte) 101;
    numArray7[18] = (byte) 71;
    numArray7[16 /*0x10*/] = (byte) 53;
    numArray7[15] = (byte) 133;
    numArray7[0] = (byte) 54;
    numArray7[17] = (byte) 249;
    numArray7[9] = (byte) 2;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techcard_19617()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 191,
        (byte) 69,
        (byte) 127 /*0x7F*/,
        (byte) 29,
        (byte) 152,
        (byte) 47,
        (byte) 215,
        (byte) 171,
        (byte) 56,
        (byte) 3,
        (byte) 111,
        (byte) 151,
        (byte) 152,
        (byte) 209,
        (byte) 129,
        (byte) 204,
        (byte) 216,
        (byte) 83,
        (byte) 219
      };
      byte[] numArray3 = new byte[19];
      numArray3[1] = (byte) 175;
      numArray3[3] = (byte) 22;
      numArray3[10] = (byte) 73;
      numArray3[15] = (byte) 158;
      numArray3[14] = (byte) 212;
      numArray3[5] = (byte) 10;
      numArray3[17] = (byte) 144 /*0x90*/;
      numArray3[11] = (byte) 151;
      numArray3[8] = (byte) 143;
      numArray3[0] = (byte) 185;
      numArray3[13] = (byte) 16 /*0x10*/;
      numArray3[9] = (byte) 10;
      numArray3[12] = (byte) 124;
      numArray3[6] = (byte) 20;
      numArray3[4] = (byte) 25;
      numArray3[7] = (byte) 52;
      numArray3[16 /*0x10*/] = (byte) 13;
      numArray3[2] = (byte) 193;
      numArray3[18] = (byte) 80 /*0x50*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[7] = (byte) 107;
    numArray5[0] = (byte) 111;
    numArray5[2] = (byte) 105;
    numArray5[3] = (byte) 89;
    numArray5[8] = (byte) 204;
    numArray5[11] = (byte) 35;
    numArray5[5] = (byte) 200;
    numArray5[10] = (byte) 148;
    numArray5[13] = (byte) 197;
    numArray5[17] = (byte) 239;
    numArray5[18] = (byte) 137;
    numArray5[4] = (byte) 166;
    numArray5[12] = (byte) 91;
    numArray5[14] = (byte) 202;
    numArray5[6] = (byte) 169;
    numArray5[15] = (byte) 156;
    numArray5[16 /*0x10*/] = (byte) 124;
    numArray5[9] = (byte) 27;
    numArray5[1] = (byte) 34;
    byte[] numArray6 = new byte[19];
    numArray6[1] = (byte) 206;
    numArray6[14] = (byte) 118;
    numArray6[3] = (byte) 222;
    numArray6[9] = (byte) 131;
    numArray6[12] = (byte) 126;
    numArray6[4] = (byte) 199;
    numArray6[6] = (byte) 188;
    numArray6[7] = (byte) 243;
    numArray6[8] = (byte) 227;
    numArray6[5] = (byte) 208 /*0xD0*/;
    numArray6[10] = (byte) 9;
    numArray6[11] = (byte) 179;
    numArray6[18] = (byte) 228;
    numArray6[13] = (byte) 178;
    numArray6[2] = (byte) 146;
    numArray6[15] = (byte) 95;
    numArray6[16 /*0x10*/] = (byte) 170;
    numArray6[17] = (byte) 241;
    numArray6[0] = (byte) 87;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
