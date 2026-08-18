// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19376
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19376
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 247,
    (byte) 42,
    (byte) 142,
    (byte) 227,
    (byte) 154,
    (byte) 30,
    (byte) 93,
    (byte) 44,
    (byte) 199,
    (byte) 80 /*0x50*/,
    (byte) 45,
    (byte) 107,
    (byte) 49,
    (byte) 153,
    (byte) 5,
    (byte) 36,
    (byte) 95,
    (byte) 114,
    (byte) 236,
    (byte) 37,
    (byte) 91,
    (byte) 191,
    (byte) 131,
    (byte) 231,
    (byte) 56,
    (byte) 145,
    (byte) 3,
    (byte) 24,
    (byte) 160 /*0xA0*/,
    (byte) 139,
    (byte) 182,
    (byte) 172,
    (byte) 221,
    (byte) 78,
    (byte) 215
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 49,
    (byte) 160 /*0xA0*/,
    (byte) 211,
    (byte) 117,
    (byte) 133,
    (byte) 3,
    (byte) 186,
    (byte) 232,
    (byte) 15,
    (byte) 16 /*0x10*/,
    (byte) 5,
    (byte) 103,
    (byte) 114,
    (byte) 221,
    (byte) 145,
    (byte) 28,
    (byte) 68,
    (byte) 161,
    (byte) 170,
    (byte) 170,
    (byte) 104,
    (byte) 41,
    (byte) 139,
    (byte) 214,
    (byte) 6,
    (byte) 117,
    (byte) 77,
    (byte) 74,
    (byte) 216,
    (byte) 232,
    (byte) 104,
    (byte) 177,
    (byte) 233,
    (byte) 113,
    (byte) 210
  };

  internal static string ssp_techcard_19377()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[15] = (byte) 207;
      numArray2[13] = (byte) 0;
      numArray2[2] = (byte) 22;
      numArray2[5] = (byte) 250;
      numArray2[18] = (byte) 207;
      numArray2[4] = (byte) 206;
      numArray2[9] = (byte) 173;
      numArray2[3] = (byte) 44;
      numArray2[0] = (byte) 41;
      numArray2[6] = (byte) 50;
      numArray2[12] = (byte) 31 /*0x1F*/;
      numArray2[11] = (byte) 239;
      numArray2[10] = (byte) 192 /*0xC0*/;
      numArray2[17] = (byte) 223;
      numArray2[14] = (byte) 230;
      numArray2[1] = (byte) 147;
      numArray2[16 /*0x10*/] = (byte) 70;
      numArray2[7] = (byte) 11;
      numArray2[8] = (byte) 206;
      byte[] numArray3 = new byte[19];
      numArray3[3] = (byte) 19;
      numArray3[8] = (byte) 191;
      numArray3[12] = (byte) 122;
      numArray3[11] = (byte) 82;
      numArray3[5] = (byte) 63 /*0x3F*/;
      numArray3[16 /*0x10*/] = (byte) 59;
      numArray3[17] = (byte) 82;
      numArray3[0] = (byte) 2;
      numArray3[18] = (byte) 57;
      numArray3[9] = (byte) 33;
      numArray3[10] = (byte) 174;
      numArray3[2] = (byte) 105;
      numArray3[4] = (byte) 129;
      numArray3[13] = (byte) 161;
      numArray3[14] = (byte) 124;
      numArray3[15] = (byte) 59;
      numArray3[1] = (byte) 145;
      numArray3[6] = (byte) 133;
      numArray3[7] = (byte) 96 /*0x60*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[5] = (byte) 131;
    numArray5[16 /*0x10*/] = (byte) 110;
    numArray5[4] = (byte) 136;
    numArray5[11] = (byte) 209;
    numArray5[3] = (byte) 207;
    numArray5[0] = (byte) 155;
    numArray5[1] = (byte) 145;
    numArray5[7] = (byte) 223;
    numArray5[17] = (byte) 249;
    numArray5[9] = (byte) 10;
    numArray5[10] = (byte) 18;
    numArray5[6] = (byte) 247;
    numArray5[18] = (byte) 247;
    numArray5[13] = (byte) 0;
    numArray5[14] = (byte) 176 /*0xB0*/;
    numArray5[15] = (byte) 63 /*0x3F*/;
    numArray5[2] = (byte) 81;
    numArray5[8] = (byte) 102;
    numArray5[12] = (byte) 145;
    byte[] numArray6 = new byte[19]
    {
      (byte) 132,
      (byte) 226,
      (byte) 99,
      (byte) 219,
      (byte) 224 /*0xE0*/,
      (byte) 164,
      (byte) 151,
      (byte) 105,
      (byte) 80 /*0x50*/,
      (byte) 164,
      (byte) 252,
      (byte) 182,
      (byte) 93,
      (byte) 242,
      (byte) 36,
      (byte) 72,
      (byte) 117,
      (byte) 160 /*0xA0*/,
      (byte) 119
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[14];
    byte[] response = new byte[14];
    Array.Copy((Array) sc_19376.sspq, 0, (Array) numArray7, 0, 14);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19376.sspr, 0, (Array) numArray7, 0, 14);
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

  internal static string ssp_techcard_19378()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 55,
        (byte) 133,
        (byte) 142,
        (byte) 100,
        (byte) 12,
        (byte) 10,
        (byte) 1,
        (byte) 121,
        (byte) 122,
        (byte) 12,
        (byte) 119,
        (byte) 204,
        (byte) 206,
        (byte) 209,
        (byte) 183,
        (byte) 50,
        (byte) 128 /*0x80*/,
        (byte) 5,
        (byte) 73
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 113,
        (byte) 3,
        (byte) 57,
        (byte) 82,
        (byte) 107,
        (byte) 208 /*0xD0*/,
        (byte) 29,
        (byte) 29,
        (byte) 217,
        (byte) 35,
        (byte) 151,
        (byte) 142,
        (byte) 191,
        (byte) 16 /*0x10*/,
        (byte) 101,
        (byte) 51,
        (byte) 111,
        (byte) 112 /*0x70*/,
        (byte) 250
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[10] = (byte) 154;
    numArray5[1] = (byte) 95;
    numArray5[2] = (byte) 0;
    numArray5[8] = (byte) 89;
    numArray5[4] = (byte) 167;
    numArray5[18] = (byte) 246;
    numArray5[3] = (byte) 140;
    numArray5[12] = (byte) 53;
    numArray5[7] = (byte) 106;
    numArray5[6] = (byte) 64 /*0x40*/;
    numArray5[9] = (byte) 232;
    numArray5[15] = (byte) 18;
    numArray5[11] = (byte) 186;
    numArray5[5] = (byte) 157;
    numArray5[14] = (byte) 56;
    numArray5[0] = (byte) 103;
    numArray5[16 /*0x10*/] = (byte) 50;
    numArray5[17] = (byte) 174;
    numArray5[13] = (byte) 48 /*0x30*/;
    byte[] numArray6 = new byte[19]
    {
      (byte) 163,
      (byte) 251,
      (byte) 247,
      (byte) 106,
      (byte) 84,
      (byte) 91,
      (byte) 187,
      (byte) 193,
      (byte) 130,
      (byte) 12,
      (byte) 173,
      (byte) 248,
      (byte) 6,
      (byte) 137,
      (byte) 155,
      (byte) 5,
      (byte) 236,
      (byte) 150,
      (byte) 232
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19379()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 178,
        (byte) 143,
        (byte) 50,
        (byte) 20,
        (byte) 155,
        (byte) 134,
        (byte) 249,
        (byte) 109,
        (byte) 210,
        (byte) 154,
        (byte) 19,
        (byte) 142,
        (byte) 118,
        (byte) 144 /*0x90*/,
        (byte) 61,
        (byte) 175,
        (byte) 253,
        (byte) 76,
        (byte) 142
      };
      byte[] numArray3 = new byte[19];
      numArray3[12] = (byte) 45;
      numArray3[4] = (byte) 206;
      numArray3[2] = (byte) 131;
      numArray3[7] = (byte) 70;
      numArray3[3] = (byte) 227;
      numArray3[0] = (byte) 27;
      numArray3[15] = (byte) 40;
      numArray3[14] = (byte) 154;
      numArray3[8] = (byte) 83;
      numArray3[6] = (byte) 177;
      numArray3[10] = (byte) 57;
      numArray3[11] = (byte) 231;
      numArray3[5] = (byte) 16 /*0x10*/;
      numArray3[13] = (byte) 0;
      numArray3[9] = (byte) 150;
      numArray3[16 /*0x10*/] = (byte) 60;
      numArray3[18] = (byte) 12;
      numArray3[17] = (byte) 169;
      numArray3[1] = (byte) 204;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[10] = (byte) 22;
    numArray5[1] = (byte) 147;
    numArray5[13] = (byte) 81;
    numArray5[7] = (byte) 241;
    numArray5[4] = (byte) 88;
    numArray5[17] = (byte) 65;
    numArray5[12] = (byte) 22;
    numArray5[0] = (byte) 63 /*0x3F*/;
    numArray5[5] = (byte) 54;
    numArray5[9] = (byte) 130;
    numArray5[8] = (byte) 180;
    numArray5[11] = (byte) 212;
    numArray5[2] = (byte) 78;
    numArray5[18] = (byte) 18;
    numArray5[14] = (byte) 191;
    numArray5[15] = (byte) 143;
    numArray5[16 /*0x10*/] = (byte) 39;
    numArray5[3] = (byte) 123;
    numArray5[6] = (byte) 141;
    byte[] numArray6 = new byte[19];
    numArray6[13] = (byte) 78;
    numArray6[17] = (byte) 176 /*0xB0*/;
    numArray6[2] = (byte) 196;
    numArray6[3] = (byte) 8;
    numArray6[16 /*0x10*/] = (byte) 179;
    numArray6[5] = (byte) 206;
    numArray6[12] = (byte) 230;
    numArray6[7] = (byte) 175;
    numArray6[10] = (byte) 171;
    numArray6[9] = (byte) 219;
    numArray6[4] = (byte) 215;
    numArray6[11] = (byte) 91;
    numArray6[14] = (byte) 174;
    numArray6[0] = (byte) 69;
    numArray6[6] = (byte) 87;
    numArray6[8] = (byte) 194;
    numArray6[1] = (byte) 138;
    numArray6[18] = (byte) 137;
    numArray6[15] = (byte) 52;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19380()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 32 /*0x20*/,
        (byte) 71,
        (byte) 36,
        byte.MaxValue,
        (byte) 138,
        (byte) 206,
        (byte) 24,
        (byte) 211,
        (byte) 95,
        (byte) 222,
        (byte) 152,
        (byte) 198,
        (byte) 192 /*0xC0*/,
        (byte) 238,
        (byte) 216,
        (byte) 194,
        (byte) 240 /*0xF0*/,
        (byte) 24,
        (byte) 193
      };
      byte[] numArray3 = new byte[19];
      numArray3[10] = (byte) 179;
      numArray3[18] = (byte) 37;
      numArray3[2] = (byte) 20;
      numArray3[3] = (byte) 13;
      numArray3[11] = (byte) 218;
      numArray3[7] = (byte) 150;
      numArray3[6] = (byte) 214;
      numArray3[1] = (byte) 204;
      numArray3[17] = (byte) 15;
      numArray3[9] = (byte) 91;
      numArray3[8] = (byte) 219;
      numArray3[12] = (byte) 145;
      numArray3[13] = (byte) 26;
      numArray3[0] = (byte) 227;
      numArray3[14] = (byte) 214;
      numArray3[15] = (byte) 210;
      numArray3[16 /*0x10*/] = (byte) 108;
      numArray3[5] = (byte) 134;
      numArray3[4] = (byte) 83;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[21];
      byte[] response = new byte[21];
      Array.Copy((Array) sc_19376.sspq, 14, (Array) numArray4, 0, 21);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19376.sspr, 14, (Array) numArray4, 0, 21);
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
    byte[] numArray6 = new byte[19];
    numArray6[2] = (byte) 3;
    numArray6[0] = (byte) 115;
    numArray6[5] = (byte) 138;
    numArray6[3] = (byte) 143;
    numArray6[4] = (byte) 51;
    numArray6[14] = (byte) 130;
    numArray6[12] = (byte) 226;
    numArray6[9] = (byte) 109;
    numArray6[8] = (byte) 192 /*0xC0*/;
    numArray6[6] = (byte) 8;
    numArray6[1] = (byte) 45;
    numArray6[11] = (byte) 104;
    numArray6[10] = (byte) 209;
    numArray6[13] = (byte) 2;
    numArray6[7] = (byte) 234;
    numArray6[15] = (byte) 56;
    numArray6[16 /*0x10*/] = (byte) 215;
    numArray6[17] = (byte) 241;
    numArray6[18] = (byte) 203;
    byte[] numArray7 = new byte[19];
    numArray7[15] = (byte) 56;
    numArray7[18] = (byte) 227;
    numArray7[11] = (byte) 2;
    numArray7[4] = (byte) 175;
    numArray7[14] = (byte) 149;
    numArray7[5] = (byte) 130;
    numArray7[12] = (byte) 17;
    numArray7[1] = (byte) 163;
    numArray7[8] = (byte) 104;
    numArray7[9] = (byte) 110;
    numArray7[6] = (byte) 9;
    numArray7[16 /*0x10*/] = (byte) 34;
    numArray7[13] = (byte) 241;
    numArray7[17] = (byte) 51;
    numArray7[10] = (byte) 107;
    numArray7[3] = (byte) 29;
    numArray7[0] = (byte) 1;
    numArray7[2] = (byte) 121;
    numArray7[7] = (byte) 9;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
