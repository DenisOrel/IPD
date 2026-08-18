// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21938
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21938
{
  private static byte[] sspq = new byte[40]
  {
    (byte) 179,
    (byte) 189,
    (byte) 25,
    (byte) 189,
    (byte) 50,
    (byte) 174,
    (byte) 102,
    (byte) 24,
    (byte) 130,
    (byte) 199,
    (byte) 202,
    (byte) 121,
    (byte) 111,
    (byte) 231,
    (byte) 229,
    (byte) 107,
    (byte) 254,
    (byte) 100,
    (byte) 79,
    (byte) 30,
    (byte) 196,
    (byte) 45,
    (byte) 25,
    (byte) 216,
    (byte) 36,
    (byte) 46,
    (byte) 152,
    (byte) 96 /*0x60*/,
    (byte) 241,
    (byte) 244,
    (byte) 118,
    (byte) 59,
    (byte) 42,
    (byte) 7,
    (byte) 154,
    (byte) 187,
    (byte) 142,
    (byte) 125,
    (byte) 166,
    (byte) 54
  };
  private static byte[] sspr = new byte[40]
  {
    (byte) 86,
    (byte) 47,
    (byte) 185,
    (byte) 37,
    (byte) 52,
    (byte) 41,
    (byte) 186,
    (byte) 242,
    (byte) 154,
    (byte) 174,
    (byte) 189,
    (byte) 150,
    (byte) 188,
    (byte) 12,
    (byte) 232,
    (byte) 33,
    (byte) 133,
    (byte) 199,
    (byte) 13,
    (byte) 22,
    (byte) 115,
    (byte) 112 /*0x70*/,
    (byte) 24,
    (byte) 92,
    (byte) 23,
    (byte) 199,
    (byte) 182,
    (byte) 162,
    (byte) 182,
    (byte) 250,
    (byte) 164,
    (byte) 182,
    (byte) 37,
    (byte) 201,
    (byte) 126,
    (byte) 124,
    (byte) 199,
    (byte) 208 /*0xD0*/,
    (byte) 65,
    (byte) 226
  };

  internal static int ssp_workflow_21939(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 218,
      (byte) 222,
      byte.MaxValue,
      (byte) 170,
      (byte) 176 /*0xB0*/,
      (byte) 163,
      (byte) 167,
      (byte) 197,
      (byte) 38,
      (byte) 221,
      (byte) 53,
      (byte) 95,
      (byte) 19,
      (byte) 57,
      (byte) 61,
      (byte) 51,
      (byte) 133,
      (byte) 140,
      (byte) 123,
      (byte) 4,
      (byte) 122,
      (byte) 28,
      (byte) 172,
      (byte) 80 /*0x50*/,
      (byte) 68,
      (byte) 152,
      (byte) 32 /*0x20*/,
      (byte) 224 /*0xE0*/,
      (byte) 251,
      (byte) 172,
      (byte) 110,
      (byte) 77,
      (byte) 121,
      (byte) 167,
      (byte) 59,
      (byte) 216,
      (byte) 228,
      (byte) 102,
      (byte) 200,
      (byte) 78,
      (byte) 234,
      (byte) 161,
      (byte) 248,
      (byte) 125,
      (byte) 184,
      (byte) 227,
      (byte) 167,
      (byte) 44
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 105,
      (byte) 157,
      (byte) 249,
      (byte) 75,
      (byte) 143,
      (byte) 245,
      (byte) 143,
      (byte) 152,
      (byte) 194,
      (byte) 82,
      (byte) 238,
      (byte) 183,
      (byte) 212,
      (byte) 227,
      (byte) 153,
      (byte) 5,
      (byte) 221,
      (byte) 147,
      (byte) 165,
      (byte) 134,
      (byte) 173,
      byte.MaxValue,
      (byte) 61,
      (byte) 102,
      (byte) 134,
      (byte) 244,
      (byte) 71,
      (byte) 177,
      (byte) 200,
      (byte) 190,
      (byte) 13,
      (byte) 240 /*0xF0*/,
      (byte) 97,
      (byte) 16 /*0x10*/,
      (byte) 168,
      (byte) 23,
      (byte) 77,
      (byte) 146,
      (byte) 80 /*0x50*/,
      (byte) 230,
      (byte) 165,
      (byte) 241,
      (byte) 76,
      (byte) 78,
      (byte) 126,
      (byte) 236,
      (byte) 242,
      (byte) 105
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 366, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[40];
    byte[] response2 = new byte[40];
    Array.Copy((Array) sc_21938.sspq, 0, (Array) numArray2, 0, 40);
    key.Query(true, 366, numArray2, response2);
    Array.Copy((Array) sc_21938.sspr, 0, (Array) numArray2, 0, 40);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_workflow_21940(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 203,
      (byte) 34,
      (byte) 215,
      (byte) 126,
      (byte) 14,
      (byte) 208 /*0xD0*/,
      (byte) 254,
      (byte) 60,
      (byte) 45,
      (byte) 4,
      (byte) 112 /*0x70*/,
      (byte) 99,
      (byte) 162,
      (byte) 134,
      (byte) 163,
      (byte) 64 /*0x40*/,
      (byte) 152,
      (byte) 3,
      (byte) 21,
      (byte) 6,
      (byte) 39,
      (byte) 141,
      (byte) 80 /*0x50*/,
      (byte) 143,
      (byte) 78,
      (byte) 104,
      (byte) 77,
      (byte) 41,
      (byte) 220,
      (byte) 128 /*0x80*/,
      (byte) 190,
      (byte) 24,
      (byte) 30,
      (byte) 20,
      (byte) 97,
      (byte) 246,
      (byte) 1,
      (byte) 166,
      (byte) 32 /*0x20*/,
      (byte) 200,
      (byte) 97,
      (byte) 180,
      (byte) 199,
      (byte) 114,
      (byte) 202,
      (byte) 199,
      (byte) 100,
      (byte) 19
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 79;
    sourceArray2[1] = (byte) 126;
    sourceArray2[10] = (byte) 48 /*0x30*/;
    sourceArray2[12] = (byte) 160 /*0xA0*/;
    sourceArray2[29] = (byte) 2;
    sourceArray2[5] = (byte) 246;
    sourceArray2[3] = (byte) 46;
    sourceArray2[28] = (byte) 33;
    sourceArray2[8] = (byte) 68;
    sourceArray2[9] = (byte) 235;
    sourceArray2[43] = (byte) 164;
    sourceArray2[11] = (byte) 210;
    sourceArray2[22] = (byte) 184;
    sourceArray2[13] = (byte) 64 /*0x40*/;
    sourceArray2[14] = (byte) 187;
    sourceArray2[15] = (byte) 159;
    sourceArray2[16 /*0x10*/] = (byte) 156;
    sourceArray2[32 /*0x20*/] = (byte) 50;
    sourceArray2[45] = (byte) 166;
    sourceArray2[19] = (byte) 37;
    sourceArray2[34] = (byte) 121;
    sourceArray2[21] = (byte) 203;
    sourceArray2[36] = byte.MaxValue;
    sourceArray2[18] = (byte) 95;
    sourceArray2[24] = (byte) 249;
    sourceArray2[25] = (byte) 78;
    sourceArray2[26] = (byte) 112 /*0x70*/;
    sourceArray2[27] = (byte) 162;
    sourceArray2[17] = (byte) 6;
    sourceArray2[44] = (byte) 27;
    sourceArray2[30] = (byte) 98;
    sourceArray2[31 /*0x1F*/] = (byte) 69;
    sourceArray2[35] = (byte) 165;
    sourceArray2[4] = (byte) 114;
    sourceArray2[2] = (byte) 22;
    sourceArray2[47] = (byte) 209;
    sourceArray2[40] = (byte) 221;
    sourceArray2[37] = (byte) 79;
    sourceArray2[38] = (byte) 123;
    sourceArray2[39] = (byte) 7;
    sourceArray2[20] = (byte) 85;
    sourceArray2[41] = (byte) 225;
    sourceArray2[0] = (byte) 212;
    sourceArray2[7] = (byte) 59;
    sourceArray2[6] = (byte) 128 /*0x80*/;
    sourceArray2[46] = (byte) 159;
    sourceArray2[33] = (byte) 58;
    sourceArray2[23] = (byte) 163;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_21941()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[4] = (byte) 14;
      numArray2[10] = (byte) 119;
      numArray2[8] = (byte) 84;
      numArray2[0] = (byte) 130;
      numArray2[1] = (byte) 214;
      numArray2[16 /*0x10*/] = (byte) 194;
      numArray2[11] = (byte) 243;
      numArray2[14] = (byte) 163;
      numArray2[18] = (byte) 17;
      numArray2[15] = (byte) 73;
      numArray2[5] = (byte) 241;
      numArray2[9] = (byte) 22;
      numArray2[6] = (byte) 191;
      numArray2[13] = (byte) 168;
      numArray2[7] = (byte) 212;
      numArray2[3] = (byte) 107;
      numArray2[2] = (byte) 238;
      numArray2[17] = (byte) 208 /*0xD0*/;
      numArray2[12] = (byte) 131;
      byte[] numArray3 = new byte[19];
      numArray3[2] = (byte) 4;
      numArray3[15] = (byte) 249;
      numArray3[5] = (byte) 162;
      numArray3[4] = (byte) 242;
      numArray3[9] = (byte) 84;
      numArray3[8] = (byte) 102;
      numArray3[6] = (byte) 65;
      numArray3[0] = (byte) 227;
      numArray3[13] = (byte) 124;
      numArray3[7] = (byte) 21;
      numArray3[10] = (byte) 3;
      numArray3[11] = (byte) 174;
      numArray3[12] = (byte) 127 /*0x7F*/;
      numArray3[16 /*0x10*/] = (byte) 184;
      numArray3[14] = (byte) 234;
      numArray3[1] = (byte) 213;
      numArray3[3] = (byte) 9;
      numArray3[17] = (byte) 1;
      numArray3[18] = (byte) 121;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 117,
      (byte) 87,
      (byte) 204,
      (byte) 60,
      (byte) 248,
      (byte) 203,
      (byte) 64 /*0x40*/,
      (byte) 49,
      (byte) 164,
      (byte) 118,
      (byte) 253,
      (byte) 28,
      (byte) 80 /*0x50*/,
      (byte) 103,
      (byte) 205,
      (byte) 60,
      (byte) 77,
      (byte) 140,
      (byte) 85
    };
    byte[] numArray6 = new byte[19];
    numArray6[1] = (byte) 114;
    numArray6[0] = (byte) 230;
    numArray6[18] = (byte) 107;
    numArray6[14] = (byte) 14;
    numArray6[10] = (byte) 158;
    numArray6[5] = (byte) 250;
    numArray6[11] = (byte) 5;
    numArray6[7] = (byte) 123;
    numArray6[8] = (byte) 89;
    numArray6[9] = (byte) 122;
    numArray6[6] = (byte) 213;
    numArray6[4] = (byte) 105;
    numArray6[12] = (byte) 191;
    numArray6[16 /*0x10*/] = (byte) 104;
    numArray6[2] = (byte) 40;
    numArray6[15] = (byte) 73;
    numArray6[3] = (byte) 203;
    numArray6[17] = (byte) 92;
    numArray6[13] = (byte) 147;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
