// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19699
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19699
{
  internal static string ssp_techcard_19700()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[9] = (byte) 252;
      numArray2[1] = (byte) 121;
      numArray2[2] = (byte) 143;
      numArray2[3] = (byte) 195;
      numArray2[15] = (byte) 32 /*0x20*/;
      numArray2[0] = (byte) 219;
      numArray2[6] = (byte) 33;
      numArray2[7] = (byte) 196;
      numArray2[11] = (byte) 160 /*0xA0*/;
      numArray2[14] = (byte) 135;
      numArray2[10] = (byte) 64 /*0x40*/;
      numArray2[17] = (byte) 76;
      numArray2[12] = (byte) 209;
      numArray2[13] = (byte) 136;
      numArray2[4] = (byte) 18;
      numArray2[16 /*0x10*/] = (byte) 59;
      numArray2[8] = (byte) 49;
      numArray2[5] = (byte) 137;
      numArray2[18] = (byte) 54;
      byte[] numArray3 = new byte[19]
      {
        (byte) 125,
        (byte) 252,
        (byte) 61,
        (byte) 60,
        (byte) 200,
        (byte) 179,
        (byte) 134,
        (byte) 246,
        (byte) 145,
        (byte) 19,
        (byte) 252,
        (byte) 202,
        (byte) 39,
        (byte) 174,
        (byte) 198,
        (byte) 199,
        (byte) 5,
        (byte) 38,
        (byte) 114
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[10] = (byte) 3;
    numArray5[1] = (byte) 199;
    numArray5[2] = (byte) 186;
    numArray5[4] = (byte) 113;
    numArray5[8] = (byte) 153;
    numArray5[5] = (byte) 52;
    numArray5[6] = (byte) 238;
    numArray5[3] = (byte) 144 /*0x90*/;
    numArray5[7] = (byte) 181;
    numArray5[9] = (byte) 15;
    numArray5[15] = (byte) 192 /*0xC0*/;
    numArray5[11] = (byte) 40;
    numArray5[12] = (byte) 131;
    numArray5[13] = (byte) 174;
    numArray5[14] = (byte) 204;
    numArray5[17] = (byte) 72;
    numArray5[16 /*0x10*/] = (byte) 163;
    numArray5[0] = (byte) 107;
    numArray5[18] = (byte) 155;
    byte[] numArray6 = new byte[19];
    numArray6[14] = (byte) 47;
    numArray6[3] = (byte) 113;
    numArray6[2] = (byte) 49;
    numArray6[8] = (byte) 130;
    numArray6[4] = (byte) 206;
    numArray6[5] = (byte) 110;
    numArray6[10] = (byte) 37;
    numArray6[7] = (byte) 110;
    numArray6[0] = (byte) 222;
    numArray6[9] = (byte) 115;
    numArray6[18] = (byte) 224 /*0xE0*/;
    numArray6[15] = (byte) 181;
    numArray6[1] = (byte) 56;
    numArray6[13] = (byte) 19;
    numArray6[6] = (byte) 28;
    numArray6[11] = (byte) 152;
    numArray6[16 /*0x10*/] = (byte) 143;
    numArray6[17] = (byte) 23;
    numArray6[12] = (byte) 183;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19701()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[15] = (byte) 233;
      numArray2[7] = (byte) 182;
      numArray2[2] = (byte) 207;
      numArray2[8] = (byte) 133;
      numArray2[5] = (byte) 207;
      numArray2[11] = (byte) 63 /*0x3F*/;
      numArray2[6] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 32 /*0x20*/;
      numArray2[3] = (byte) 226;
      numArray2[9] = (byte) 194;
      numArray2[10] = (byte) 71;
      numArray2[0] = (byte) 253;
      numArray2[12] = (byte) 88;
      numArray2[13] = (byte) 176 /*0xB0*/;
      numArray2[14] = (byte) 193;
      numArray2[18] = (byte) 209;
      numArray2[1] = (byte) 14;
      numArray2[17] = (byte) 112 /*0x70*/;
      numArray2[4] = (byte) 200;
      byte[] numArray3 = new byte[19]
      {
        (byte) 23,
        (byte) 133,
        (byte) 110,
        (byte) 25,
        (byte) 111,
        (byte) 63 /*0x3F*/,
        (byte) 211,
        (byte) 67,
        (byte) 47,
        (byte) 224 /*0xE0*/,
        (byte) 250,
        (byte) 196,
        (byte) 84,
        (byte) 3,
        (byte) 91,
        (byte) 73,
        (byte) 126,
        (byte) 16 /*0x10*/,
        (byte) 166
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 43,
      (byte) 69,
      (byte) 93,
      (byte) 108,
      (byte) 134,
      (byte) 29,
      (byte) 134,
      (byte) 132,
      (byte) 248,
      (byte) 5,
      (byte) 21,
      (byte) 202,
      (byte) 34,
      (byte) 149,
      (byte) 215,
      (byte) 137,
      (byte) 185,
      (byte) 122,
      (byte) 4
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 140,
      (byte) 1,
      (byte) 184,
      (byte) 25,
      (byte) 139,
      (byte) 195,
      (byte) 204,
      (byte) 152,
      (byte) 173,
      (byte) 174,
      (byte) 75,
      (byte) 68,
      (byte) 69,
      (byte) 92,
      (byte) 230,
      (byte) 218,
      (byte) 73,
      (byte) 224 /*0xE0*/,
      (byte) 47
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19702()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 74,
        (byte) 135,
        (byte) 46,
        (byte) 9,
        (byte) 185,
        (byte) 123,
        (byte) 24,
        (byte) 130,
        (byte) 116,
        (byte) 124,
        (byte) 51,
        (byte) 50,
        (byte) 225,
        (byte) 39,
        (byte) 169,
        (byte) 224 /*0xE0*/,
        (byte) 166,
        (byte) 155,
        (byte) 184
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 151,
        (byte) 43,
        (byte) 125,
        (byte) 223,
        (byte) 247,
        (byte) 97,
        (byte) 16 /*0x10*/,
        (byte) 167,
        (byte) 114,
        (byte) 1,
        (byte) 166,
        (byte) 63 /*0x3F*/,
        (byte) 116,
        (byte) 182,
        (byte) 146,
        (byte) 254,
        (byte) 39,
        (byte) 3,
        (byte) 172
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 245,
      (byte) 238,
      (byte) 185,
      (byte) 31 /*0x1F*/,
      (byte) 189,
      (byte) 23,
      (byte) 35,
      (byte) 247,
      (byte) 167,
      (byte) 223,
      (byte) 140,
      (byte) 224 /*0xE0*/,
      (byte) 19,
      (byte) 228,
      (byte) 88,
      (byte) 20,
      (byte) 205,
      (byte) 9,
      (byte) 224 /*0xE0*/
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 240 /*0xF0*/,
      (byte) 145,
      (byte) 120,
      (byte) 217,
      (byte) 80 /*0x50*/,
      (byte) 99,
      (byte) 84,
      (byte) 99,
      (byte) 19,
      (byte) 86,
      (byte) 65,
      (byte) 38,
      (byte) 167,
      (byte) 15,
      (byte) 211,
      (byte) 31 /*0x1F*/,
      (byte) 104,
      (byte) 50,
      (byte) 137
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
