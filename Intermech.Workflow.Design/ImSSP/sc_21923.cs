// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21923
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21923
{
  private static byte[] sspq = new byte[16 /*0x10*/]
  {
    (byte) 127 /*0x7F*/,
    (byte) 171,
    (byte) 184,
    (byte) 56,
    (byte) 37,
    (byte) 239,
    (byte) 6,
    (byte) 155,
    (byte) 50,
    (byte) 98,
    (byte) 130,
    (byte) 98,
    (byte) 223,
    (byte) 203,
    (byte) 12,
    (byte) 117
  };
  private static byte[] sspr = new byte[16 /*0x10*/]
  {
    (byte) 198,
    (byte) 190,
    (byte) 224 /*0xE0*/,
    (byte) 117,
    (byte) 105,
    (byte) 51,
    (byte) 100,
    (byte) 183,
    (byte) 171,
    (byte) 163,
    (byte) 121,
    (byte) 49,
    (byte) 70,
    (byte) 175,
    (byte) 242,
    (byte) 168
  };

  internal static string ssp_workflow_21924()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 19,
        (byte) 23,
        (byte) 94,
        (byte) 218,
        (byte) 198,
        (byte) 72,
        (byte) 201,
        (byte) 190,
        (byte) 23,
        (byte) 239,
        (byte) 139,
        (byte) 164,
        (byte) 108,
        (byte) 186,
        (byte) 31 /*0x1F*/,
        (byte) 135,
        (byte) 41,
        (byte) 205,
        (byte) 165
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 100,
        (byte) 126,
        (byte) 125,
        (byte) 131,
        (byte) 156,
        (byte) 28,
        (byte) 185,
        (byte) 238,
        (byte) 72,
        (byte) 96 /*0x60*/,
        (byte) 51,
        (byte) 186,
        (byte) 117,
        (byte) 194,
        (byte) 47,
        (byte) 130,
        (byte) 102,
        (byte) 83,
        (byte) 93
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/];
      byte[] response = new byte[16 /*0x10*/];
      Array.Copy((Array) sc_21923.sspq, 0, (Array) numArray4, 0, 16 /*0x10*/);
      key.Query(true, 366, numArray4, response);
      Array.Copy((Array) sc_21923.sspr, 0, (Array) numArray4, 0, 16 /*0x10*/);
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
      (byte) 214,
      (byte) 214,
      (byte) 212,
      (byte) 50,
      (byte) 254,
      (byte) 59,
      (byte) 72,
      (byte) 240 /*0xF0*/,
      (byte) 247,
      (byte) 48 /*0x30*/,
      (byte) 27,
      (byte) 61,
      (byte) 120,
      (byte) 235,
      (byte) 229,
      (byte) 93,
      (byte) 148,
      (byte) 240 /*0xF0*/,
      (byte) 222
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 167,
      (byte) 137,
      (byte) 246,
      (byte) 137,
      (byte) 54,
      (byte) 164,
      (byte) 139,
      (byte) 59,
      (byte) 223,
      (byte) 89,
      (byte) 86,
      (byte) 33,
      (byte) 119,
      (byte) 83,
      (byte) 83,
      (byte) 5,
      (byte) 87,
      (byte) 74,
      (byte) 45
    };
    key.Query(true, 366, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_workflow_21925()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 207,
        (byte) 16 /*0x10*/,
        (byte) 84,
        (byte) 88,
        (byte) 66,
        (byte) 233,
        (byte) 88,
        (byte) 64 /*0x40*/,
        (byte) 188,
        (byte) 28,
        (byte) 88,
        (byte) 207,
        (byte) 63 /*0x3F*/,
        (byte) 76,
        (byte) 76,
        (byte) 40,
        (byte) 68,
        (byte) 98,
        (byte) 200,
        (byte) 64 /*0x40*/,
        (byte) 244,
        (byte) 212,
        (byte) 200
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 45,
        (byte) 229,
        (byte) 253,
        (byte) 190,
        (byte) 195,
        (byte) 207,
        (byte) 192 /*0xC0*/,
        (byte) 143,
        (byte) 193,
        (byte) 198,
        (byte) 239,
        (byte) 6,
        (byte) 199,
        (byte) 78,
        (byte) 187,
        (byte) 133,
        (byte) 193,
        (byte) 100,
        (byte) 240 /*0xF0*/,
        (byte) 236,
        (byte) 93,
        (byte) 21,
        (byte) 244
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 37,
      (byte) 205,
      (byte) 238,
      (byte) 236,
      (byte) 221,
      (byte) 102,
      (byte) 198,
      (byte) 50,
      (byte) 4,
      (byte) 33,
      (byte) 170,
      (byte) 226,
      (byte) 6,
      (byte) 6,
      (byte) 159,
      (byte) 240 /*0xF0*/,
      (byte) 94,
      (byte) 33,
      (byte) 32 /*0x20*/,
      (byte) 28,
      (byte) 166,
      (byte) 17,
      (byte) 72
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 65,
      (byte) 114,
      (byte) 242,
      (byte) 248,
      (byte) 174,
      (byte) 101,
      (byte) 139,
      (byte) 57,
      (byte) 167,
      (byte) 198,
      (byte) 5,
      (byte) 38,
      (byte) 186,
      (byte) 109,
      (byte) 30,
      (byte) 3,
      (byte) 82,
      (byte) 168,
      byte.MaxValue,
      (byte) 66,
      (byte) 162,
      (byte) 60,
      (byte) 14
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_workflow_21926(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 167;
    sourceArray1[46] = (byte) 19;
    sourceArray1[2] = (byte) 72;
    sourceArray1[3] = (byte) 50;
    sourceArray1[12] = (byte) 9;
    sourceArray1[5] = (byte) 198;
    sourceArray1[6] = (byte) 196;
    sourceArray1[42] = (byte) 158;
    sourceArray1[8] = (byte) 81;
    sourceArray1[40] = (byte) 66;
    sourceArray1[26] = (byte) 214;
    sourceArray1[30] = (byte) 82;
    sourceArray1[35] = (byte) 9;
    sourceArray1[13] = (byte) 12;
    sourceArray1[14] = (byte) 71;
    sourceArray1[25] = (byte) 42;
    sourceArray1[47] = (byte) 153;
    sourceArray1[17] = (byte) 98;
    sourceArray1[18] = (byte) 92;
    sourceArray1[22] = (byte) 117;
    sourceArray1[28] = (byte) 133;
    sourceArray1[0] = (byte) 176 /*0xB0*/;
    sourceArray1[10] = (byte) 63 /*0x3F*/;
    sourceArray1[36] = (byte) 217;
    sourceArray1[11] = (byte) 241;
    sourceArray1[4] = (byte) 17;
    sourceArray1[41] = (byte) 20;
    sourceArray1[21] = (byte) 4;
    sourceArray1[19] = (byte) 193;
    sourceArray1[29] = (byte) 38;
    sourceArray1[1] = (byte) 251;
    sourceArray1[9] = (byte) 207;
    sourceArray1[32 /*0x20*/] = (byte) 32 /*0x20*/;
    sourceArray1[24] = (byte) 1;
    sourceArray1[34] = (byte) 130;
    sourceArray1[16 /*0x10*/] = (byte) 126;
    sourceArray1[15] = (byte) 42;
    sourceArray1[27] = (byte) 19;
    sourceArray1[38] = (byte) 208 /*0xD0*/;
    sourceArray1[39] = (byte) 64 /*0x40*/;
    sourceArray1[44] = (byte) 225;
    sourceArray1[45] = (byte) 47;
    sourceArray1[37] = (byte) 119;
    sourceArray1[43] = (byte) 142;
    sourceArray1[20] = (byte) 161;
    sourceArray1[7] = (byte) 138;
    sourceArray1[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    sourceArray1[33] = (byte) 245;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 218;
    sourceArray2[1] = (byte) 204;
    sourceArray2[2] = (byte) 216;
    sourceArray2[17] = (byte) 244;
    sourceArray2[44] = (byte) 177;
    sourceArray2[18] = (byte) 114;
    sourceArray2[37] = (byte) 242;
    sourceArray2[7] = (byte) 214;
    sourceArray2[8] = (byte) 113;
    sourceArray2[46] = (byte) 109;
    sourceArray2[10] = (byte) 155;
    sourceArray2[40] = (byte) 215;
    sourceArray2[12] = (byte) 24;
    sourceArray2[47] = (byte) 125;
    sourceArray2[45] = (byte) 156;
    sourceArray2[0] = (byte) 183;
    sourceArray2[21] = (byte) 81;
    sourceArray2[36] = (byte) 48 /*0x30*/;
    sourceArray2[14] = (byte) 238;
    sourceArray2[19] = (byte) 197;
    sourceArray2[3] = (byte) 97;
    sourceArray2[13] = (byte) 76;
    sourceArray2[22] = (byte) 236;
    sourceArray2[23] = (byte) 111;
    sourceArray2[24] = (byte) 21;
    sourceArray2[25] = (byte) 97;
    sourceArray2[26] = (byte) 32 /*0x20*/;
    sourceArray2[38] = (byte) 232;
    sourceArray2[28] = (byte) 85;
    sourceArray2[29] = (byte) 180;
    sourceArray2[30] = (byte) 146;
    sourceArray2[31 /*0x1F*/] = (byte) 111;
    sourceArray2[27] = (byte) 122;
    sourceArray2[33] = (byte) 179;
    sourceArray2[34] = (byte) 201;
    sourceArray2[16 /*0x10*/] = (byte) 24;
    sourceArray2[15] = (byte) 70;
    sourceArray2[32 /*0x20*/] = (byte) 152;
    sourceArray2[6] = (byte) 85;
    sourceArray2[11] = (byte) 116;
    sourceArray2[20] = (byte) 65;
    sourceArray2[43] = (byte) 160 /*0xA0*/;
    sourceArray2[41] = (byte) 242;
    sourceArray2[35] = (byte) 224 /*0xE0*/;
    sourceArray2[39] = (byte) 175;
    sourceArray2[5] = (byte) 131;
    sourceArray2[42] = (byte) 214;
    sourceArray2[9] = (byte) 145;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_21927()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25];
      numArray2[15] = (byte) 26;
      numArray2[14] = (byte) 135;
      numArray2[13] = (byte) 253;
      numArray2[3] = (byte) 112 /*0x70*/;
      numArray2[4] = (byte) 36;
      numArray2[5] = (byte) 174;
      numArray2[24] = (byte) 1;
      numArray2[11] = (byte) 178;
      numArray2[8] = (byte) 180;
      numArray2[6] = (byte) 38;
      numArray2[10] = (byte) 209;
      numArray2[9] = (byte) 89;
      numArray2[22] = (byte) 132;
      numArray2[17] = (byte) 82;
      numArray2[12] = (byte) 131;
      numArray2[20] = (byte) 234;
      numArray2[1] = (byte) 157;
      numArray2[18] = (byte) 202;
      numArray2[23] = (byte) 168;
      numArray2[19] = (byte) 202;
      numArray2[2] = (byte) 44;
      numArray2[21] = (byte) 184;
      numArray2[7] = (byte) 40;
      numArray2[16 /*0x10*/] = (byte) 34;
      numArray2[0] = (byte) 25;
      byte[] numArray3 = new byte[25];
      numArray3[12] = (byte) 122;
      numArray3[3] = (byte) 224 /*0xE0*/;
      numArray3[20] = (byte) 22;
      numArray3[0] = (byte) 13;
      numArray3[1] = (byte) 132;
      numArray3[10] = (byte) 249;
      numArray3[11] = (byte) 128 /*0x80*/;
      numArray3[7] = (byte) 226;
      numArray3[8] = (byte) 18;
      numArray3[22] = (byte) 175;
      numArray3[23] = (byte) 165;
      numArray3[6] = (byte) 208 /*0xD0*/;
      numArray3[4] = (byte) 127 /*0x7F*/;
      numArray3[24] = (byte) 45;
      numArray3[14] = (byte) 207;
      numArray3[15] = (byte) 136;
      numArray3[16 /*0x10*/] = (byte) 6;
      numArray3[17] = (byte) 108;
      numArray3[5] = (byte) 25;
      numArray3[19] = (byte) 76;
      numArray3[13] = (byte) 151;
      numArray3[18] = (byte) 192 /*0xC0*/;
      numArray3[21] = (byte) 85;
      numArray3[2] = (byte) 123;
      numArray3[9] = (byte) 49;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 187,
      (byte) 24,
      (byte) 156,
      (byte) 118,
      (byte) 120,
      (byte) 37,
      (byte) 33,
      (byte) 230,
      (byte) 187,
      (byte) 43,
      (byte) 216,
      (byte) 47,
      (byte) 22,
      (byte) 58,
      (byte) 159,
      (byte) 93,
      (byte) 102,
      (byte) 4,
      (byte) 194,
      (byte) 211,
      (byte) 159,
      (byte) 240 /*0xF0*/,
      (byte) 244,
      (byte) 106,
      (byte) 10
    };
    byte[] numArray6 = new byte[25]
    {
      (byte) 102,
      (byte) 154,
      (byte) 131,
      (byte) 148,
      (byte) 196,
      (byte) 204,
      (byte) 118,
      (byte) 27,
      (byte) 193,
      (byte) 184,
      (byte) 103,
      (byte) 39,
      (byte) 226,
      (byte) 25,
      (byte) 123,
      (byte) 46,
      (byte) 94,
      (byte) 209,
      (byte) 192 /*0xC0*/,
      (byte) 53,
      (byte) 40,
      (byte) 42,
      (byte) 76,
      (byte) 228,
      (byte) 191
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
