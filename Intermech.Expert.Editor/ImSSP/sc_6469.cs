// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_6469
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_6469
{
  private static byte[] sspq = new byte[29]
  {
    (byte) 64 /*0x40*/,
    (byte) 112 /*0x70*/,
    (byte) 247,
    (byte) 98,
    (byte) 215,
    (byte) 77,
    (byte) 182,
    (byte) 148,
    (byte) 21,
    (byte) 46,
    (byte) 174,
    (byte) 96 /*0x60*/,
    (byte) 70,
    (byte) 118,
    (byte) 238,
    (byte) 131,
    (byte) 34,
    (byte) 129,
    (byte) 21,
    (byte) 143,
    (byte) 190,
    (byte) 167,
    (byte) 192 /*0xC0*/,
    (byte) 170,
    (byte) 109,
    (byte) 160 /*0xA0*/,
    (byte) 89,
    (byte) 64 /*0x40*/,
    (byte) 19
  };
  private static byte[] sspr = new byte[29]
  {
    (byte) 147,
    (byte) 159,
    (byte) 252,
    (byte) 8,
    (byte) 201,
    (byte) 17,
    (byte) 92,
    (byte) 163,
    (byte) 160 /*0xA0*/,
    (byte) 6,
    (byte) 101,
    (byte) 82,
    (byte) 140,
    (byte) 251,
    (byte) 184,
    (byte) 175,
    (byte) 222,
    (byte) 211,
    (byte) 107,
    (byte) 163,
    (byte) 192 /*0xC0*/,
    (byte) 30,
    (byte) 253,
    (byte) 83,
    (byte) 49,
    (byte) 87,
    (byte) 227,
    (byte) 93,
    (byte) 29
  };

  internal static string ssp_expert_6470()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 173,
        (byte) 214,
        (byte) 180,
        (byte) 229,
        (byte) 123,
        (byte) 50,
        (byte) 106,
        (byte) 228,
        (byte) 127 /*0x7F*/,
        (byte) 78,
        (byte) 247,
        (byte) 163,
        (byte) 139,
        (byte) 196,
        (byte) 57,
        (byte) 109,
        (byte) 219
      };
      byte[] numArray3 = new byte[17]
      {
        (byte) 86,
        (byte) 134,
        (byte) 123,
        (byte) 7,
        (byte) 44,
        (byte) 139,
        (byte) 49,
        (byte) 124,
        (byte) 231,
        (byte) 73,
        (byte) 95,
        (byte) 167,
        (byte) 54,
        (byte) 20,
        (byte) 105,
        (byte) 203,
        (byte) 0
      };
      key.Query(true, 342, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 131,
      (byte) 22,
      (byte) 173,
      (byte) 83,
      (byte) 135,
      (byte) 98,
      (byte) 95,
      (byte) 227,
      (byte) 14,
      (byte) 112 /*0x70*/,
      (byte) 152,
      (byte) 16 /*0x10*/,
      (byte) 83,
      (byte) 182,
      (byte) 43,
      (byte) 62,
      (byte) 143
    };
    byte[] numArray6 = new byte[17]
    {
      (byte) 98,
      (byte) 88,
      (byte) 113,
      (byte) 23,
      (byte) 128 /*0x80*/,
      (byte) 65,
      (byte) 72,
      (byte) 130,
      (byte) 164,
      (byte) 234,
      (byte) 201,
      (byte) 227,
      (byte) 162,
      (byte) 227,
      (byte) 53,
      (byte) 11,
      (byte) 231
    };
    key.Query(true, 342, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_6471()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 195,
        (byte) 146,
        (byte) 217,
        (byte) 103,
        (byte) 101,
        (byte) 100,
        (byte) 86,
        (byte) 163,
        (byte) 157,
        (byte) 121,
        (byte) 53,
        (byte) 57,
        (byte) 5,
        byte.MaxValue,
        (byte) 151,
        (byte) 40,
        (byte) 97
      };
      byte[] numArray3 = new byte[17]
      {
        (byte) 106,
        (byte) 227,
        (byte) 68,
        (byte) 180,
        (byte) 24,
        (byte) 81,
        (byte) 35,
        (byte) 225,
        (byte) 162,
        (byte) 226,
        (byte) 27,
        (byte) 6,
        (byte) 39,
        (byte) 173,
        (byte) 80 /*0x50*/,
        (byte) 15,
        (byte) 212
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 135,
      (byte) 213,
      (byte) 14,
      (byte) 182,
      (byte) 230,
      (byte) 80 /*0x50*/,
      (byte) 212,
      (byte) 152,
      (byte) 251,
      (byte) 221,
      (byte) 207,
      (byte) 200,
      (byte) 152,
      (byte) 171,
      (byte) 191,
      (byte) 131,
      (byte) 211
    };
    byte[] numArray6 = new byte[17]
    {
      (byte) 166,
      (byte) 91,
      (byte) 126,
      (byte) 200,
      (byte) 95,
      (byte) 226,
      (byte) 182,
      (byte) 177,
      (byte) 215,
      (byte) 201,
      (byte) 245,
      (byte) 1,
      (byte) 77,
      (byte) 97,
      (byte) 183,
      (byte) 45,
      (byte) 65
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[29];
    byte[] response = new byte[29];
    Array.Copy((Array) sc_6469.sspq, 0, (Array) numArray7, 0, 29);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_6469.sspr, 0, (Array) numArray7, 0, 29);
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

  internal static int ssp_expert_6472(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[6] = (byte) 84;
    sourceArray1[22] = (byte) 110;
    sourceArray1[15] = (byte) 81;
    sourceArray1[42] = (byte) 19;
    sourceArray1[4] = (byte) 100;
    sourceArray1[29] = (byte) 243;
    sourceArray1[0] = (byte) 59;
    sourceArray1[24] = (byte) 152;
    sourceArray1[20] = (byte) 106;
    sourceArray1[9] = (byte) 29;
    sourceArray1[10] = (byte) 3;
    sourceArray1[8] = (byte) 2;
    sourceArray1[2] = (byte) 202;
    sourceArray1[13] = (byte) 32 /*0x20*/;
    sourceArray1[5] = (byte) 23;
    sourceArray1[11] = (byte) 230;
    sourceArray1[16 /*0x10*/] = (byte) 53;
    sourceArray1[17] = (byte) 209;
    sourceArray1[18] = byte.MaxValue;
    sourceArray1[41] = (byte) 113;
    sourceArray1[39] = (byte) 108;
    sourceArray1[21] = (byte) 12;
    sourceArray1[19] = (byte) 176 /*0xB0*/;
    sourceArray1[23] = (byte) 74;
    sourceArray1[28] = (byte) 54;
    sourceArray1[25] = (byte) 176 /*0xB0*/;
    sourceArray1[26] = (byte) 76;
    sourceArray1[27] = (byte) 245;
    sourceArray1[36] = (byte) 124;
    sourceArray1[34] = (byte) 115;
    sourceArray1[30] = (byte) 118;
    sourceArray1[7] = (byte) 15;
    sourceArray1[32 /*0x20*/] = (byte) 192 /*0xC0*/;
    sourceArray1[33] = (byte) 184;
    sourceArray1[31 /*0x1F*/] = (byte) 128 /*0x80*/;
    sourceArray1[47] = (byte) 137;
    sourceArray1[1] = (byte) 27;
    sourceArray1[40] = (byte) 206;
    sourceArray1[38] = (byte) 137;
    sourceArray1[12] = (byte) 175;
    sourceArray1[35] = (byte) 69;
    sourceArray1[3] = (byte) 70;
    sourceArray1[14] = (byte) 104;
    sourceArray1[43] = (byte) 114;
    sourceArray1[44] = (byte) 186;
    sourceArray1[45] = (byte) 193;
    sourceArray1[46] = (byte) 16 /*0x10*/;
    sourceArray1[37] = (byte) 47;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 178;
    sourceArray2[45] = (byte) 47;
    sourceArray2[23] = (byte) 190;
    sourceArray2[2] = (byte) 12;
    sourceArray2[4] = (byte) 190;
    sourceArray2[7] = (byte) 134;
    sourceArray2[30] = (byte) 16 /*0x10*/;
    sourceArray2[15] = (byte) 64 /*0x40*/;
    sourceArray2[8] = (byte) 44;
    sourceArray2[9] = (byte) 29;
    sourceArray2[5] = (byte) 52;
    sourceArray2[11] = (byte) 4;
    sourceArray2[46] = (byte) 169;
    sourceArray2[19] = (byte) 17;
    sourceArray2[25] = (byte) 46;
    sourceArray2[40] = (byte) 39;
    sourceArray2[14] = (byte) 139;
    sourceArray2[26] = (byte) 27;
    sourceArray2[1] = (byte) 177;
    sourceArray2[21] = (byte) 165;
    sourceArray2[20] = (byte) 150;
    sourceArray2[12] = (byte) 220;
    sourceArray2[22] = (byte) 235;
    sourceArray2[42] = (byte) 199;
    sourceArray2[38] = (byte) 33;
    sourceArray2[0] = (byte) 120;
    sourceArray2[17] = (byte) 245;
    sourceArray2[16 /*0x10*/] = (byte) 225;
    sourceArray2[28] = (byte) 44;
    sourceArray2[29] = (byte) 215;
    sourceArray2[34] = (byte) 247;
    sourceArray2[31 /*0x1F*/] = (byte) 8;
    sourceArray2[3] = (byte) 166;
    sourceArray2[33] = (byte) 70;
    sourceArray2[10] = (byte) 116;
    sourceArray2[35] = (byte) 20;
    sourceArray2[36] = (byte) 163;
    sourceArray2[27] = (byte) 87;
    sourceArray2[13] = (byte) 52;
    sourceArray2[39] = (byte) 233;
    sourceArray2[6] = (byte) 142;
    sourceArray2[41] = (byte) 179;
    sourceArray2[32 /*0x20*/] = (byte) 152;
    sourceArray2[24] = (byte) 69;
    sourceArray2[44] = (byte) 164;
    sourceArray2[18] = (byte) 198;
    sourceArray2[37] = (byte) 104;
    sourceArray2[47] = (byte) 240 /*0xF0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 342, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_expert_6473(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 2,
      (byte) 165,
      (byte) 195,
      (byte) 44,
      (byte) 32 /*0x20*/,
      (byte) 146,
      (byte) 209,
      (byte) 115,
      (byte) 86,
      (byte) 161,
      (byte) 153,
      (byte) 180,
      (byte) 142,
      (byte) 79,
      (byte) 178,
      (byte) 63 /*0x3F*/,
      (byte) 129,
      (byte) 152,
      (byte) 153,
      (byte) 142,
      (byte) 33,
      (byte) 94,
      (byte) 133,
      (byte) 143,
      (byte) 189,
      (byte) 42,
      (byte) 174,
      (byte) 174,
      (byte) 90,
      (byte) 163,
      (byte) 205,
      (byte) 216,
      (byte) 176 /*0xB0*/,
      (byte) 88,
      (byte) 112 /*0x70*/,
      (byte) 186,
      (byte) 50,
      (byte) 3,
      (byte) 55,
      (byte) 170,
      (byte) 179,
      (byte) 114,
      (byte) 184,
      (byte) 203,
      (byte) 23,
      (byte) 45,
      (byte) 93
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 67,
      (byte) 220,
      (byte) 139,
      (byte) 131,
      (byte) 41,
      (byte) 34,
      (byte) 59,
      (byte) 93,
      (byte) 121,
      (byte) 70,
      (byte) 121,
      (byte) 111,
      (byte) 207,
      (byte) 251,
      (byte) 160 /*0xA0*/,
      (byte) 253,
      (byte) 17,
      (byte) 204,
      (byte) 93,
      (byte) 63 /*0x3F*/,
      (byte) 222,
      (byte) 38,
      (byte) 193,
      (byte) 146,
      (byte) 48 /*0x30*/,
      (byte) 230,
      (byte) 234,
      (byte) 143,
      (byte) 14,
      (byte) 161,
      (byte) 24,
      (byte) 34,
      (byte) 109,
      (byte) 184,
      (byte) 142,
      (byte) 66,
      (byte) 215,
      (byte) 8,
      (byte) 235,
      (byte) 25,
      (byte) 0,
      (byte) 173,
      (byte) 14,
      (byte) 130,
      (byte) 132,
      (byte) 178,
      (byte) 42,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 342, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_expert_6474(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 17,
      (byte) 51,
      (byte) 120,
      (byte) 163,
      (byte) 32 /*0x20*/,
      (byte) 52,
      (byte) 111,
      (byte) 246,
      (byte) 227,
      (byte) 35,
      (byte) 25,
      (byte) 178,
      (byte) 211,
      (byte) 192 /*0xC0*/,
      (byte) 4,
      (byte) 53,
      (byte) 223,
      (byte) 248,
      (byte) 13,
      (byte) 125,
      (byte) 96 /*0x60*/,
      (byte) 239,
      (byte) 218,
      (byte) 174,
      (byte) 166,
      (byte) 185,
      (byte) 119,
      (byte) 144 /*0x90*/,
      (byte) 204,
      (byte) 163,
      (byte) 198,
      (byte) 139,
      (byte) 62,
      (byte) 89,
      (byte) 199,
      (byte) 124,
      (byte) 224 /*0xE0*/,
      (byte) 204,
      (byte) 153,
      (byte) 253,
      (byte) 148,
      (byte) 64 /*0x40*/,
      (byte) 198,
      (byte) 144 /*0x90*/,
      (byte) 89,
      (byte) 6,
      (byte) 247,
      (byte) 86
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 182;
    sourceArray2[30] = (byte) 132;
    sourceArray2[2] = (byte) 134;
    sourceArray2[3] = (byte) 84;
    sourceArray2[4] = (byte) 13;
    sourceArray2[16 /*0x10*/] = (byte) 162;
    sourceArray2[6] = (byte) 90;
    sourceArray2[35] = (byte) 156;
    sourceArray2[8] = (byte) 37;
    sourceArray2[37] = (byte) 119;
    sourceArray2[10] = (byte) 54;
    sourceArray2[45] = (byte) 153;
    sourceArray2[25] = (byte) 106;
    sourceArray2[7] = (byte) 130;
    sourceArray2[14] = (byte) 51;
    sourceArray2[19] = (byte) 215;
    sourceArray2[28] = (byte) 224 /*0xE0*/;
    sourceArray2[17] = (byte) 27;
    sourceArray2[1] = (byte) 54;
    sourceArray2[27] = (byte) 70;
    sourceArray2[15] = (byte) 8;
    sourceArray2[21] = (byte) 116;
    sourceArray2[22] = (byte) 175;
    sourceArray2[23] = (byte) 146;
    sourceArray2[24] = (byte) 58;
    sourceArray2[31 /*0x1F*/] = (byte) 242;
    sourceArray2[20] = (byte) 222;
    sourceArray2[13] = (byte) 142;
    sourceArray2[26] = (byte) 104;
    sourceArray2[29] = (byte) 204;
    sourceArray2[11] = (byte) 126;
    sourceArray2[32 /*0x20*/] = (byte) 175;
    sourceArray2[46] = (byte) 56;
    sourceArray2[40] = (byte) 155;
    sourceArray2[0] = (byte) 91;
    sourceArray2[9] = (byte) 3;
    sourceArray2[36] = (byte) 237;
    sourceArray2[44] = (byte) 16 /*0x10*/;
    sourceArray2[38] = (byte) 76;
    sourceArray2[39] = (byte) 212;
    sourceArray2[34] = (byte) 236;
    sourceArray2[41] = (byte) 9;
    sourceArray2[12] = (byte) 63 /*0x3F*/;
    sourceArray2[33] = (byte) 109;
    sourceArray2[18] = (byte) 64 /*0x40*/;
    sourceArray2[43] = (byte) 95;
    sourceArray2[42] = (byte) 51;
    sourceArray2[47] = (byte) 199;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 342, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
