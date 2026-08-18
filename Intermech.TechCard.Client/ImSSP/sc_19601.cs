// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19601
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19601
{
  internal static string ssp_techcard_19602()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 110,
        (byte) 153,
        (byte) 241,
        (byte) 69,
        (byte) 162,
        (byte) 47,
        (byte) 28,
        (byte) 96 /*0x60*/,
        (byte) 53,
        (byte) 222,
        (byte) 221,
        (byte) 199,
        (byte) 132,
        (byte) 124,
        (byte) 168,
        (byte) 78,
        (byte) 214,
        (byte) 127 /*0x7F*/,
        (byte) 214
      };
      byte[] numArray3 = new byte[19];
      numArray3[8] = (byte) 160 /*0xA0*/;
      numArray3[1] = (byte) 107;
      numArray3[2] = (byte) 17;
      numArray3[4] = (byte) 116;
      numArray3[18] = (byte) 157;
      numArray3[5] = (byte) 10;
      numArray3[10] = (byte) 129;
      numArray3[7] = (byte) 201;
      numArray3[13] = (byte) 6;
      numArray3[15] = (byte) 116;
      numArray3[16 /*0x10*/] = (byte) 40;
      numArray3[11] = (byte) 211;
      numArray3[0] = (byte) 8;
      numArray3[9] = (byte) 46;
      numArray3[14] = (byte) 21;
      numArray3[3] = (byte) 184;
      numArray3[12] = (byte) 36;
      numArray3[17] = (byte) 44;
      numArray3[6] = (byte) 163;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 73,
      (byte) 81,
      (byte) 20,
      (byte) 168,
      (byte) 125,
      (byte) 186,
      (byte) 180,
      (byte) 24,
      (byte) 52,
      (byte) 246,
      (byte) 141,
      (byte) 214,
      (byte) 200,
      (byte) 45,
      (byte) 91,
      (byte) 109,
      (byte) 241,
      (byte) 51,
      (byte) 7
    };
    byte[] numArray6 = new byte[19];
    numArray6[8] = (byte) 251;
    numArray6[18] = (byte) 155;
    numArray6[2] = (byte) 97;
    numArray6[11] = (byte) 151;
    numArray6[6] = (byte) 117;
    numArray6[5] = (byte) 186;
    numArray6[16 /*0x10*/] = (byte) 105;
    numArray6[7] = (byte) 94;
    numArray6[1] = (byte) 48 /*0x30*/;
    numArray6[9] = (byte) 222;
    numArray6[14] = (byte) 113;
    numArray6[12] = (byte) 119;
    numArray6[0] = (byte) 48 /*0x30*/;
    numArray6[3] = (byte) 236;
    numArray6[10] = (byte) 183;
    numArray6[15] = (byte) 0;
    numArray6[4] = (byte) 115;
    numArray6[17] = (byte) 120;
    numArray6[13] = (byte) 137;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_techcard_19603(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 190,
      (byte) 106,
      (byte) 155,
      (byte) 161,
      (byte) 212,
      (byte) 211,
      (byte) 132,
      (byte) 178,
      (byte) 48 /*0x30*/,
      (byte) 69,
      (byte) 170,
      (byte) 65,
      (byte) 33,
      (byte) 208 /*0xD0*/,
      (byte) 46,
      (byte) 61,
      (byte) 32 /*0x20*/,
      (byte) 55,
      (byte) 102,
      (byte) 97,
      (byte) 137,
      (byte) 130,
      (byte) 91,
      (byte) 48 /*0x30*/,
      (byte) 17,
      (byte) 225,
      (byte) 154,
      (byte) 85,
      (byte) 155,
      (byte) 158,
      (byte) 38,
      (byte) 145,
      (byte) 35,
      (byte) 37,
      (byte) 194,
      (byte) 124,
      (byte) 207,
      (byte) 187,
      (byte) 174,
      (byte) 122,
      (byte) 50,
      (byte) 247,
      (byte) 67,
      (byte) 236,
      (byte) 251,
      (byte) 116,
      (byte) 43,
      (byte) 240 /*0xF0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 142,
      (byte) 179,
      (byte) 159,
      (byte) 224 /*0xE0*/,
      (byte) 129,
      (byte) 61,
      (byte) 144 /*0x90*/,
      (byte) 113,
      (byte) 122,
      (byte) 46,
      (byte) 116,
      (byte) 139,
      (byte) 29,
      (byte) 39,
      (byte) 65,
      (byte) 52,
      (byte) 60,
      (byte) 92,
      (byte) 111,
      (byte) 90,
      (byte) 167,
      (byte) 11,
      (byte) 97,
      (byte) 212,
      (byte) 10,
      (byte) 20,
      (byte) 82,
      (byte) 164,
      (byte) 178,
      (byte) 90,
      (byte) 212,
      (byte) 160 /*0xA0*/,
      (byte) 182,
      (byte) 163,
      (byte) 26,
      (byte) 115,
      (byte) 131,
      (byte) 75,
      (byte) 116,
      (byte) 82,
      (byte) 105,
      (byte) 205,
      (byte) 62,
      (byte) 104,
      (byte) 160 /*0xA0*/,
      (byte) 247,
      (byte) 130
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_techcard_19604()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 141,
        (byte) 15,
        (byte) 121,
        (byte) 60,
        (byte) 15,
        (byte) 106,
        (byte) 143,
        (byte) 243,
        (byte) 57,
        (byte) 46,
        (byte) 23,
        (byte) 145,
        (byte) 31 /*0x1F*/,
        (byte) 24,
        (byte) 30,
        (byte) 251,
        (byte) 14,
        (byte) 178,
        (byte) 110
      };
      byte[] numArray3 = new byte[19];
      numArray3[16 /*0x10*/] = (byte) 135;
      numArray3[15] = (byte) 177;
      numArray3[2] = (byte) 233;
      numArray3[5] = (byte) 246;
      numArray3[4] = (byte) 99;
      numArray3[12] = (byte) 96 /*0x60*/;
      numArray3[0] = (byte) 149;
      numArray3[7] = (byte) 234;
      numArray3[1] = (byte) 57;
      numArray3[9] = (byte) 185;
      numArray3[10] = (byte) 35;
      numArray3[17] = (byte) 233;
      numArray3[13] = (byte) 81;
      numArray3[8] = (byte) 174;
      numArray3[14] = (byte) 92;
      numArray3[6] = (byte) 6;
      numArray3[11] = (byte) 3;
      numArray3[3] = (byte) 132;
      numArray3[18] = (byte) 34;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[10] = (byte) 55;
    numArray5[1] = (byte) 65;
    numArray5[2] = (byte) 4;
    numArray5[3] = (byte) 224 /*0xE0*/;
    numArray5[9] = (byte) 226;
    numArray5[12] = (byte) 15;
    numArray5[6] = (byte) 226;
    numArray5[0] = (byte) 187;
    numArray5[8] = (byte) 47;
    numArray5[15] = (byte) 224 /*0xE0*/;
    numArray5[16 /*0x10*/] = (byte) 231;
    numArray5[11] = (byte) 171;
    numArray5[5] = (byte) 205;
    numArray5[13] = (byte) 31 /*0x1F*/;
    numArray5[14] = (byte) 161;
    numArray5[7] = (byte) 86;
    numArray5[4] = (byte) 177;
    numArray5[17] = (byte) 254;
    numArray5[18] = (byte) 161;
    byte[] numArray6 = new byte[19];
    numArray6[16 /*0x10*/] = (byte) 236;
    numArray6[8] = (byte) 67;
    numArray6[2] = (byte) 189;
    numArray6[9] = (byte) 44;
    numArray6[4] = (byte) 181;
    numArray6[0] = (byte) 146;
    numArray6[13] = (byte) 192 /*0xC0*/;
    numArray6[7] = (byte) 68;
    numArray6[10] = (byte) 141;
    numArray6[17] = (byte) 34;
    numArray6[1] = (byte) 17;
    numArray6[15] = (byte) 142;
    numArray6[12] = (byte) 97;
    numArray6[5] = (byte) 117;
    numArray6[14] = (byte) 55;
    numArray6[11] = (byte) 232;
    numArray6[6] = (byte) 158;
    numArray6[3] = (byte) 73;
    numArray6[18] = (byte) 153;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
