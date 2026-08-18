// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21919
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21919
{
  private static byte[] sspq = new byte[49]
  {
    (byte) 50,
    (byte) 156,
    (byte) 238,
    (byte) 47,
    (byte) 165,
    (byte) 244,
    (byte) 110,
    (byte) 156,
    (byte) 70,
    (byte) 96 /*0x60*/,
    (byte) 76,
    (byte) 16 /*0x10*/,
    (byte) 9,
    (byte) 197,
    (byte) 183,
    (byte) 179,
    (byte) 200,
    (byte) 21,
    (byte) 70,
    (byte) 153,
    (byte) 20,
    (byte) 234,
    (byte) 117,
    (byte) 146,
    (byte) 108,
    (byte) 225,
    (byte) 195,
    (byte) 93,
    (byte) 0,
    (byte) 117,
    (byte) 246,
    (byte) 41,
    (byte) 231,
    (byte) 60,
    (byte) 31 /*0x1F*/,
    (byte) 160 /*0xA0*/,
    (byte) 147,
    (byte) 73,
    (byte) 238,
    (byte) 198,
    (byte) 237,
    (byte) 92,
    (byte) 178,
    (byte) 80 /*0x50*/,
    (byte) 162,
    (byte) 102,
    (byte) 70,
    (byte) 145,
    (byte) 251
  };
  private static byte[] sspr = new byte[49]
  {
    (byte) 116,
    (byte) 21,
    (byte) 135,
    (byte) 3,
    (byte) 52,
    (byte) 18,
    (byte) 139,
    (byte) 199,
    (byte) 195,
    (byte) 68,
    (byte) 83,
    (byte) 10,
    (byte) 217,
    (byte) 192 /*0xC0*/,
    (byte) 233,
    (byte) 153,
    (byte) 75,
    (byte) 95,
    (byte) 241,
    (byte) 113,
    (byte) 125,
    (byte) 87,
    (byte) 76,
    (byte) 37,
    (byte) 39,
    (byte) 242,
    (byte) 148,
    (byte) 161,
    (byte) 83,
    (byte) 98,
    (byte) 228,
    (byte) 138,
    (byte) 125,
    (byte) 17,
    (byte) 7,
    (byte) 126,
    (byte) 111,
    (byte) 153,
    (byte) 156,
    (byte) 53,
    (byte) 143,
    (byte) 219,
    (byte) 219,
    (byte) 45,
    (byte) 71,
    (byte) 80 /*0x50*/,
    (byte) 252,
    (byte) 163,
    (byte) 174
  };

  internal static int ssp_workflow_21920(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 52,
      (byte) 19,
      (byte) 29,
      (byte) 23,
      (byte) 107,
      (byte) 210,
      (byte) 162,
      (byte) 233,
      (byte) 187,
      (byte) 52,
      (byte) 46,
      (byte) 73,
      (byte) 181,
      (byte) 113,
      (byte) 82,
      (byte) 216,
      (byte) 103,
      (byte) 36,
      (byte) 149,
      (byte) 184,
      (byte) 205,
      (byte) 3,
      (byte) 21,
      (byte) 204,
      (byte) 26,
      (byte) 29,
      (byte) 244,
      (byte) 32 /*0x20*/,
      (byte) 20,
      (byte) 76,
      (byte) 44,
      (byte) 202,
      (byte) 15,
      (byte) 59,
      (byte) 111,
      (byte) 149,
      (byte) 45,
      (byte) 133,
      (byte) 80 /*0x50*/,
      (byte) 15,
      (byte) 30,
      (byte) 195,
      (byte) 158,
      (byte) 174,
      (byte) 88,
      (byte) 169,
      (byte) 22,
      (byte) 72
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 18,
      (byte) 247,
      (byte) 30,
      (byte) 174,
      (byte) 152,
      (byte) 100,
      (byte) 162,
      (byte) 4,
      (byte) 208 /*0xD0*/,
      (byte) 233,
      (byte) 67,
      (byte) 198,
      (byte) 171,
      (byte) 147,
      (byte) 30,
      (byte) 234,
      (byte) 228,
      (byte) 141,
      (byte) 42,
      (byte) 138,
      (byte) 2,
      (byte) 106,
      (byte) 90,
      (byte) 236,
      (byte) 96 /*0x60*/,
      (byte) 99,
      (byte) 116,
      (byte) 58,
      (byte) 105,
      (byte) 110,
      (byte) 152,
      (byte) 118,
      (byte) 113,
      (byte) 153,
      (byte) 242,
      (byte) 97,
      (byte) 225,
      (byte) 17,
      (byte) 180,
      (byte) 43,
      (byte) 241,
      (byte) 185,
      (byte) 170,
      (byte) 203,
      (byte) 120,
      (byte) 164,
      (byte) 36
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_21921()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 240 /*0xF0*/,
        (byte) 47,
        (byte) 189,
        (byte) 49,
        (byte) 253,
        (byte) 80 /*0x50*/,
        (byte) 253,
        (byte) 78,
        (byte) 221,
        (byte) 130,
        (byte) 213,
        (byte) 110,
        (byte) 8,
        (byte) 100,
        (byte) 58,
        (byte) 122,
        (byte) 2,
        (byte) 189,
        (byte) 170
      };
      byte[] numArray3 = new byte[19];
      numArray3[11] = (byte) 167;
      numArray3[1] = (byte) 116;
      numArray3[2] = (byte) 34;
      numArray3[6] = (byte) 200;
      numArray3[16 /*0x10*/] = (byte) 106;
      numArray3[5] = (byte) 8;
      numArray3[4] = (byte) 44;
      numArray3[7] = (byte) 148;
      numArray3[8] = (byte) 174;
      numArray3[9] = (byte) 234;
      numArray3[13] = (byte) 184;
      numArray3[10] = (byte) 5;
      numArray3[18] = (byte) 100;
      numArray3[14] = (byte) 132;
      numArray3[0] = (byte) 250;
      numArray3[15] = (byte) 179;
      numArray3[3] = (byte) 208 /*0xD0*/;
      numArray3[17] = (byte) 73;
      numArray3[12] = (byte) 133;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_21919.sspq, 0, (Array) numArray4, 0, 11);
      key.Query(true, 366, numArray4, response);
      Array.Copy((Array) sc_21919.sspr, 0, (Array) numArray4, 0, 11);
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
      (byte) 196,
      (byte) 243,
      (byte) 107,
      (byte) 149,
      (byte) 179,
      (byte) 203,
      (byte) 150,
      (byte) 220,
      (byte) 139,
      (byte) 72,
      (byte) 50,
      (byte) 127 /*0x7F*/,
      (byte) 183,
      (byte) 199,
      (byte) 163,
      (byte) 137,
      (byte) 91,
      (byte) 252,
      (byte) 196
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 192 /*0xC0*/,
      (byte) 19,
      (byte) 155,
      (byte) 82,
      (byte) 47,
      (byte) 233,
      (byte) 235,
      (byte) 222,
      (byte) 134,
      (byte) 160 /*0xA0*/,
      (byte) 102,
      (byte) 132,
      (byte) 26,
      (byte) 20,
      (byte) 136,
      (byte) 189,
      (byte) 241,
      (byte) 41,
      (byte) 171
    };
    key.Query(true, 366, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_workflow_21922()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[18] = (byte) 0;
      numArray2[11] = (byte) 227;
      numArray2[16 /*0x10*/] = (byte) 114;
      numArray2[9] = (byte) 162;
      numArray2[10] = (byte) 209;
      numArray2[5] = (byte) 128 /*0x80*/;
      numArray2[6] = (byte) 2;
      numArray2[7] = (byte) 105;
      numArray2[2] = (byte) 27;
      numArray2[1] = (byte) 253;
      numArray2[4] = (byte) 193;
      numArray2[8] = (byte) 101;
      numArray2[0] = (byte) 82;
      numArray2[13] = (byte) 111;
      numArray2[14] = (byte) 74;
      numArray2[15] = (byte) 201;
      numArray2[3] = (byte) 53;
      numArray2[17] = (byte) 1;
      numArray2[12] = (byte) 113;
      numArray2[19] = (byte) 252;
      byte[] numArray3 = new byte[20]
      {
        (byte) 92,
        (byte) 24,
        (byte) 226,
        (byte) 228,
        (byte) 49,
        (byte) 6,
        (byte) 45,
        (byte) 136,
        (byte) 243,
        (byte) 125,
        (byte) 61,
        (byte) 162,
        (byte) 197,
        (byte) 153,
        (byte) 63 /*0x3F*/,
        (byte) 168,
        (byte) 11,
        (byte) 122,
        (byte) 48 /*0x30*/,
        (byte) 254
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[15] = (byte) 205;
    numArray5[1] = (byte) 229;
    numArray5[17] = (byte) 21;
    numArray5[11] = (byte) 23;
    numArray5[0] = (byte) 221;
    numArray5[9] = (byte) 205;
    numArray5[2] = (byte) 228;
    numArray5[7] = (byte) 81;
    numArray5[5] = (byte) 16 /*0x10*/;
    numArray5[8] = (byte) 35;
    numArray5[10] = (byte) 91;
    numArray5[6] = (byte) 50;
    numArray5[12] = (byte) 82;
    numArray5[13] = (byte) 224 /*0xE0*/;
    numArray5[14] = (byte) 228;
    numArray5[4] = (byte) 26;
    numArray5[16 /*0x10*/] = (byte) 191;
    numArray5[3] = (byte) 49;
    numArray5[18] = (byte) 239;
    numArray5[19] = (byte) 201;
    byte[] numArray6 = new byte[20]
    {
      (byte) 115,
      (byte) 165,
      (byte) 15,
      (byte) 21,
      (byte) 177,
      (byte) 229,
      (byte) 145,
      (byte) 8,
      (byte) 88,
      (byte) 94,
      (byte) 215,
      (byte) 195,
      (byte) 35,
      (byte) 227,
      (byte) 195,
      (byte) 208 /*0xD0*/,
      (byte) 207,
      (byte) 194,
      (byte) 221,
      (byte) 53
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_21919.sspq, 11, (Array) numArray7, 0, 38);
    key.Query(true, 366, numArray7, response);
    Array.Copy((Array) sc_21919.sspr, 11, (Array) numArray7, 0, 38);
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
