
// Type: ImSSP.sc_3902
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3902
{
  private static byte[] sspq = new byte[90]
  {
    (byte) 136,
    (byte) 198,
    (byte) 25,
    (byte) 114,
    (byte) 68,
    (byte) 67,
    (byte) 150,
    (byte) 79,
    (byte) 208 /*0xD0*/,
    (byte) 202,
    (byte) 147,
    (byte) 58,
    (byte) 188,
    (byte) 180,
    (byte) 62,
    (byte) 130,
    (byte) 159,
    (byte) 225,
    (byte) 178,
    (byte) 217,
    (byte) 214,
    (byte) 160 /*0xA0*/,
    (byte) 210,
    (byte) 89,
    (byte) 44,
    (byte) 27,
    (byte) 51,
    (byte) 9,
    (byte) 10,
    (byte) 36,
    (byte) 249,
    (byte) 139,
    (byte) 130,
    (byte) 200,
    (byte) 170,
    (byte) 28,
    (byte) 108,
    (byte) 36,
    (byte) 43,
    (byte) 57,
    (byte) 208 /*0xD0*/,
    (byte) 148,
    (byte) 197,
    (byte) 123,
    (byte) 90,
    (byte) 234,
    (byte) 95,
    (byte) 165,
    (byte) 138,
    (byte) 249,
    (byte) 58,
    (byte) 59,
    (byte) 98,
    (byte) 248,
    (byte) 191,
    (byte) 226,
    (byte) 237,
    (byte) 169,
    (byte) 89,
    (byte) 90,
    (byte) 182,
    (byte) 253,
    (byte) 1,
    (byte) 130,
    (byte) 132,
    (byte) 13,
    (byte) 163,
    (byte) 241,
    (byte) 226,
    (byte) 103,
    (byte) 174,
    (byte) 167,
    (byte) 56,
    (byte) 227,
    (byte) 139,
    (byte) 103,
    (byte) 199,
    (byte) 237,
    (byte) 58,
    (byte) 180,
    (byte) 217,
    (byte) 235,
    (byte) 46,
    (byte) 37,
    (byte) 201,
    (byte) 48 /*0x30*/,
    (byte) 65,
    (byte) 172,
    (byte) 15,
    (byte) 48 /*0x30*/
  };
  private static byte[] sspr = new byte[90]
  {
    (byte) 65,
    (byte) 77,
    (byte) 100,
    (byte) 150,
    (byte) 205,
    (byte) 8,
    (byte) 139,
    (byte) 27,
    (byte) 23,
    (byte) 61,
    (byte) 155,
    (byte) 125,
    (byte) 110,
    (byte) 153,
    (byte) 5,
    (byte) 84,
    (byte) 148,
    (byte) 136,
    (byte) 100,
    (byte) 218,
    (byte) 146,
    (byte) 38,
    (byte) 77,
    (byte) 54,
    (byte) 161,
    (byte) 166,
    (byte) 158,
    (byte) 128 /*0x80*/,
    (byte) 248,
    (byte) 150,
    (byte) 37,
    (byte) 175,
    (byte) 134,
    (byte) 2,
    (byte) 105,
    (byte) 217,
    (byte) 208 /*0xD0*/,
    (byte) 6,
    (byte) 33,
    (byte) 81,
    (byte) 211,
    (byte) 114,
    (byte) 253,
    (byte) 235,
    (byte) 178,
    (byte) 51,
    (byte) 223,
    (byte) 243,
    (byte) 180,
    (byte) 233,
    (byte) 224 /*0xE0*/,
    (byte) 57,
    (byte) 44,
    (byte) 176 /*0xB0*/,
    (byte) 177,
    (byte) 241,
    (byte) 210,
    (byte) 65,
    (byte) 84,
    (byte) 21,
    (byte) 251,
    (byte) 214,
    (byte) 102,
    (byte) 235,
    (byte) 78,
    (byte) 12,
    (byte) 151,
    (byte) 23,
    (byte) 149,
    (byte) 189,
    (byte) 88,
    (byte) 15,
    (byte) 59,
    (byte) 133,
    (byte) 55,
    (byte) 239,
    (byte) 226,
    (byte) 36,
    (byte) 228,
    (byte) 172,
    (byte) 227,
    (byte) 77,
    (byte) 22,
    (byte) 99,
    (byte) 15,
    (byte) 103,
    (byte) 192 /*0xC0*/,
    (byte) 82,
    (byte) 222,
    (byte) 155
  };

  internal static string ssp_imclient_3903()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 184,
        (byte) 85,
        (byte) 246,
        (byte) 160 /*0xA0*/,
        (byte) 35,
        (byte) 201,
        (byte) 98,
        (byte) 236,
        (byte) 250,
        (byte) 90,
        (byte) 167,
        (byte) 72,
        (byte) 107,
        (byte) 99,
        (byte) 46
      };
      byte[] numArray3 = new byte[15];
      numArray3[6] = (byte) 206;
      numArray3[1] = (byte) 227;
      numArray3[5] = (byte) 152;
      numArray3[3] = (byte) 19;
      numArray3[2] = (byte) 154;
      numArray3[10] = (byte) 229;
      numArray3[13] = (byte) 40;
      numArray3[12] = (byte) 187;
      numArray3[8] = (byte) 78;
      numArray3[9] = (byte) 46;
      numArray3[7] = (byte) 110;
      numArray3[4] = (byte) 10;
      numArray3[11] = (byte) 108;
      numArray3[0] = (byte) 68;
      numArray3[14] = (byte) 249;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/];
      byte[] response = new byte[16 /*0x10*/];
      Array.Copy((Array) sc_3902.sspq, 0, (Array) numArray4, 0, 16 /*0x10*/);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_3902.sspr, 0, (Array) numArray4, 0, 16 /*0x10*/);
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
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15];
    numArray6[3] = (byte) 30;
    numArray6[5] = (byte) 248;
    numArray6[14] = (byte) 169;
    numArray6[0] = (byte) 72;
    numArray6[4] = (byte) 12;
    numArray6[12] = (byte) 138;
    numArray6[6] = (byte) 202;
    numArray6[7] = (byte) 67;
    numArray6[1] = (byte) 197;
    numArray6[9] = (byte) 249;
    numArray6[10] = (byte) 203;
    numArray6[11] = (byte) 246;
    numArray6[2] = (byte) 63 /*0x3F*/;
    numArray6[8] = (byte) 47;
    numArray6[13] = (byte) 231;
    byte[] numArray7 = new byte[15];
    numArray7[1] = (byte) 135;
    numArray7[5] = (byte) 87;
    numArray7[2] = (byte) 145;
    numArray7[3] = (byte) 240 /*0xF0*/;
    numArray7[11] = (byte) 141;
    numArray7[4] = (byte) 49;
    numArray7[6] = (byte) 96 /*0x60*/;
    numArray7[10] = (byte) 59;
    numArray7[13] = (byte) 151;
    numArray7[9] = (byte) 240 /*0xF0*/;
    numArray7[7] = byte.MaxValue;
    numArray7[8] = (byte) 159;
    numArray7[12] = (byte) 15;
    numArray7[0] = (byte) 1;
    numArray7[14] = (byte) 14;
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[20];
    byte[] response1 = new byte[20];
    Array.Copy((Array) sc_3902.sspq, 16 /*0x10*/, (Array) numArray8, 0, 20);
    key.Query(true, 348, numArray8, response1);
    Array.Copy((Array) sc_3902.sspr, 16 /*0x10*/, (Array) numArray8, 0, 20);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_imclient_3904()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[1] = (byte) 134;
      numArray2[0] = (byte) 129;
      numArray2[2] = (byte) 181;
      numArray2[3] = (byte) 75;
      numArray2[10] = (byte) 20;
      numArray2[5] = (byte) 12;
      numArray2[6] = (byte) 51;
      numArray2[4] = (byte) 108;
      numArray2[13] = (byte) 128 /*0x80*/;
      numArray2[9] = (byte) 98;
      numArray2[14] = (byte) 84;
      numArray2[11] = (byte) 6;
      numArray2[8] = (byte) 155;
      numArray2[7] = (byte) 32 /*0x20*/;
      numArray2[12] = (byte) 158;
      byte[] numArray3 = new byte[15]
      {
        (byte) 103,
        (byte) 147,
        (byte) 99,
        (byte) 43,
        (byte) 134,
        (byte) 32 /*0x20*/,
        (byte) 108,
        (byte) 213,
        (byte) 180,
        (byte) 18,
        (byte) 182,
        (byte) 62,
        (byte) 63 /*0x3F*/,
        (byte) 83,
        (byte) 86
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 99,
      (byte) 29,
      (byte) 91,
      (byte) 252,
      (byte) 141,
      (byte) 105,
      (byte) 148,
      (byte) 158,
      (byte) 56,
      (byte) 144 /*0x90*/,
      (byte) 184,
      (byte) 31 /*0x1F*/,
      (byte) 3,
      (byte) 108,
      (byte) 163
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 130,
      (byte) 29,
      (byte) 95,
      (byte) 145,
      (byte) 140,
      (byte) 68,
      (byte) 53,
      (byte) 190,
      (byte) 245,
      (byte) 174,
      (byte) 140,
      (byte) 192 /*0xC0*/,
      (byte) 218,
      (byte) 246,
      (byte) 141
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[54];
    byte[] response = new byte[54];
    Array.Copy((Array) sc_3902.sspq, 36, (Array) numArray7, 0, 54);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3902.sspr, 36, (Array) numArray7, 0, 54);
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
