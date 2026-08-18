// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7911
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7911
{
  private static byte[] sspq = new byte[28]
  {
    (byte) 131,
    (byte) 226,
    (byte) 174,
    (byte) 168,
    (byte) 219,
    (byte) 117,
    (byte) 39,
    (byte) 111,
    (byte) 252,
    (byte) 109,
    (byte) 11,
    (byte) 198,
    (byte) 108,
    (byte) 6,
    (byte) 102,
    (byte) 53,
    (byte) 198,
    (byte) 7,
    (byte) 13,
    (byte) 163,
    (byte) 154,
    (byte) 197,
    (byte) 222,
    (byte) 249,
    (byte) 28,
    (byte) 106,
    (byte) 211,
    (byte) 164
  };
  private static byte[] sspr = new byte[28]
  {
    (byte) 50,
    (byte) 94,
    (byte) 226,
    (byte) 44,
    (byte) 177,
    (byte) 167,
    (byte) 160 /*0xA0*/,
    (byte) 184,
    (byte) 15,
    (byte) 83,
    (byte) 93,
    (byte) 53,
    (byte) 131,
    (byte) 39,
    (byte) 45,
    (byte) 98,
    (byte) 130,
    (byte) 46,
    (byte) 47,
    (byte) 124,
    (byte) 218,
    (byte) 44,
    (byte) 183,
    (byte) 160 /*0xA0*/,
    (byte) 149,
    (byte) 219,
    (byte) 96 /*0x60*/,
    (byte) 31 /*0x1F*/
  };

  internal static string ssp_imbase_7912()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[13] = (byte) 5;
      numArray2[1] = (byte) 220;
      numArray2[4] = (byte) 1;
      numArray2[5] = (byte) 187;
      numArray2[2] = (byte) 99;
      numArray2[3] = (byte) 67;
      numArray2[6] = (byte) 75;
      numArray2[12] = (byte) 224 /*0xE0*/;
      numArray2[8] = (byte) 2;
      numArray2[9] = (byte) 181;
      numArray2[14] = (byte) 131;
      numArray2[10] = (byte) 74;
      numArray2[0] = (byte) 77;
      numArray2[7] = (byte) 197;
      numArray2[11] = (byte) 160 /*0xA0*/;
      numArray2[15] = (byte) 46;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 69,
        (byte) 180,
        (byte) 129,
        (byte) 200,
        (byte) 126,
        (byte) 116,
        (byte) 237,
        (byte) 90,
        (byte) 164,
        (byte) 135,
        (byte) 144 /*0x90*/,
        (byte) 7,
        (byte) 67,
        (byte) 169,
        (byte) 62,
        (byte) 104
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28];
      byte[] response = new byte[28];
      Array.Copy((Array) sc_7911.sspq, 0, (Array) numArray4, 0, 28);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7911.sspr, 0, (Array) numArray4, 0, 28);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 212,
      (byte) 74,
      (byte) 239,
      (byte) 22,
      (byte) 101,
      (byte) 7,
      (byte) 171,
      (byte) 11,
      (byte) 142,
      (byte) 44,
      (byte) 67,
      (byte) 137,
      (byte) 130,
      (byte) 211,
      (byte) 162,
      (byte) 247
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 117,
      (byte) 198,
      (byte) 77,
      (byte) 88,
      (byte) 47,
      (byte) 19,
      (byte) 77,
      (byte) 135,
      (byte) 123,
      (byte) 166,
      (byte) 74,
      (byte) 243,
      (byte) 79,
      (byte) 244,
      (byte) 15,
      (byte) 208 /*0xD0*/
    };
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_imbase_7913(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 172,
      (byte) 56,
      (byte) 170,
      (byte) 188,
      (byte) 253,
      (byte) 225,
      (byte) 190,
      (byte) 3,
      (byte) 180,
      (byte) 140,
      (byte) 112 /*0x70*/,
      (byte) 162,
      (byte) 130,
      (byte) 154,
      (byte) 177,
      (byte) 126,
      (byte) 245,
      (byte) 97,
      (byte) 100,
      (byte) 91,
      (byte) 91,
      (byte) 166,
      (byte) 154,
      (byte) 196,
      (byte) 132,
      (byte) 33,
      (byte) 199,
      (byte) 34,
      (byte) 231,
      (byte) 253,
      (byte) 68,
      (byte) 245,
      (byte) 232,
      (byte) 174,
      (byte) 40,
      (byte) 18,
      (byte) 69,
      (byte) 90,
      (byte) 103,
      (byte) 38,
      (byte) 237,
      (byte) 7,
      (byte) 218,
      (byte) 160 /*0xA0*/,
      (byte) 210,
      (byte) 230,
      byte.MaxValue,
      (byte) 56
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 250;
    sourceArray2[26] = (byte) 241;
    sourceArray2[40] = (byte) 38;
    sourceArray2[3] = (byte) 109;
    sourceArray2[4] = (byte) 98;
    sourceArray2[5] = (byte) 45;
    sourceArray2[41] = (byte) 20;
    sourceArray2[17] = (byte) 92;
    sourceArray2[8] = (byte) 100;
    sourceArray2[9] = (byte) 246;
    sourceArray2[30] = (byte) 148;
    sourceArray2[11] = (byte) 224 /*0xE0*/;
    sourceArray2[12] = (byte) 107;
    sourceArray2[21] = (byte) 155;
    sourceArray2[14] = (byte) 148;
    sourceArray2[13] = (byte) 162;
    sourceArray2[16 /*0x10*/] = (byte) 175;
    sourceArray2[23] = (byte) 246;
    sourceArray2[18] = (byte) 101;
    sourceArray2[24] = (byte) 135;
    sourceArray2[46] = (byte) 216;
    sourceArray2[7] = (byte) 164;
    sourceArray2[22] = (byte) 234;
    sourceArray2[25] = (byte) 194;
    sourceArray2[44] = (byte) 31 /*0x1F*/;
    sourceArray2[20] = (byte) 207;
    sourceArray2[27] = (byte) 189;
    sourceArray2[36] = (byte) 33;
    sourceArray2[28] = (byte) 43;
    sourceArray2[33] = (byte) 250;
    sourceArray2[15] = (byte) 8;
    sourceArray2[31 /*0x1F*/] = (byte) 222;
    sourceArray2[32 /*0x20*/] = (byte) 8;
    sourceArray2[10] = (byte) 80 /*0x50*/;
    sourceArray2[34] = (byte) 76;
    sourceArray2[19] = (byte) 87;
    sourceArray2[39] = (byte) 22;
    sourceArray2[37] = (byte) 47;
    sourceArray2[2] = (byte) 50;
    sourceArray2[0] = (byte) 41;
    sourceArray2[1] = (byte) 233;
    sourceArray2[38] = (byte) 22;
    sourceArray2[42] = (byte) 9;
    sourceArray2[35] = (byte) 96 /*0x60*/;
    sourceArray2[29] = (byte) 14;
    sourceArray2[45] = (byte) 92;
    sourceArray2[6] = (byte) 205;
    sourceArray2[47] = (byte) 62;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_imbase_7914()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 44,
        (byte) 43,
        (byte) 197,
        (byte) 82,
        (byte) 64 /*0x40*/,
        (byte) 107,
        (byte) 124,
        (byte) 210,
        (byte) 34,
        (byte) 70,
        (byte) 58,
        (byte) 164,
        (byte) 243,
        (byte) 154,
        (byte) 185,
        (byte) 138,
        (byte) 105
      };
      byte[] numArray3 = new byte[17];
      numArray3[13] = (byte) 38;
      numArray3[6] = (byte) 150;
      numArray3[9] = (byte) 100;
      numArray3[3] = (byte) 94;
      numArray3[4] = (byte) 251;
      numArray3[5] = (byte) 38;
      numArray3[7] = (byte) 116;
      numArray3[8] = (byte) 150;
      numArray3[2] = (byte) 115;
      numArray3[14] = (byte) 181;
      numArray3[10] = (byte) 1;
      numArray3[11] = (byte) 88;
      numArray3[1] = (byte) 10;
      numArray3[12] = (byte) 104;
      numArray3[15] = (byte) 227;
      numArray3[0] = (byte) 167;
      numArray3[16 /*0x10*/] = (byte) 132;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[14] = (byte) 60;
    numArray5[7] = (byte) 231;
    numArray5[2] = (byte) 139;
    numArray5[5] = (byte) 53;
    numArray5[0] = (byte) 223;
    numArray5[9] = (byte) 108;
    numArray5[12] = (byte) 130;
    numArray5[3] = (byte) 81;
    numArray5[8] = (byte) 208 /*0xD0*/;
    numArray5[13] = (byte) 231;
    numArray5[10] = (byte) 135;
    numArray5[1] = (byte) 46;
    numArray5[11] = (byte) 211;
    numArray5[16 /*0x10*/] = (byte) 222;
    numArray5[4] = (byte) 17;
    numArray5[15] = (byte) 102;
    numArray5[6] = (byte) 204;
    byte[] numArray6 = new byte[17]
    {
      (byte) 179,
      (byte) 12,
      (byte) 234,
      (byte) 210,
      (byte) 187,
      (byte) 119,
      (byte) 205,
      (byte) 36,
      (byte) 225,
      (byte) 95,
      (byte) 82,
      (byte) 126,
      (byte) 111,
      (byte) 140,
      (byte) 211,
      (byte) 56,
      (byte) 191
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
