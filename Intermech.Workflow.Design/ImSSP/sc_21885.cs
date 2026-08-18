// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21885
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21885
{
  private static byte[] sspq = new byte[39]
  {
    (byte) 198,
    (byte) 241,
    (byte) 217,
    (byte) 108,
    (byte) 246,
    (byte) 201,
    (byte) 39,
    (byte) 89,
    (byte) 65,
    (byte) 48 /*0x30*/,
    (byte) 124,
    (byte) 251,
    (byte) 216,
    (byte) 105,
    (byte) 227,
    (byte) 77,
    (byte) 89,
    (byte) 203,
    (byte) 52,
    (byte) 180,
    (byte) 187,
    (byte) 193,
    (byte) 17,
    (byte) 98,
    (byte) 243,
    (byte) 124,
    (byte) 109,
    (byte) 46,
    (byte) 5,
    (byte) 52,
    (byte) 71,
    (byte) 206,
    (byte) 107,
    (byte) 243,
    (byte) 16 /*0x10*/,
    (byte) 203,
    (byte) 252,
    (byte) 98,
    (byte) 5
  };
  private static byte[] sspr = new byte[39]
  {
    (byte) 206,
    (byte) 147,
    (byte) 187,
    (byte) 184,
    (byte) 243,
    (byte) 133,
    (byte) 163,
    (byte) 238,
    (byte) 184,
    (byte) 73,
    (byte) 135,
    (byte) 215,
    (byte) 99,
    (byte) 103,
    (byte) 174,
    (byte) 166,
    (byte) 151,
    (byte) 7,
    (byte) 197,
    (byte) 18,
    (byte) 107,
    (byte) 61,
    (byte) 238,
    (byte) 7,
    (byte) 86,
    (byte) 124,
    (byte) 156,
    (byte) 3,
    (byte) 114,
    (byte) 93,
    (byte) 10,
    (byte) 65,
    (byte) 96 /*0x60*/,
    (byte) 25,
    (byte) 50,
    (byte) 188,
    (byte) 252,
    (byte) 146,
    (byte) 70
  };

  internal static int ssp_workflow_21886(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[17] = (byte) 45;
    sourceArray1[37] = (byte) 191;
    sourceArray1[47] = (byte) 56;
    sourceArray1[3] = (byte) 6;
    sourceArray1[34] = (byte) 143;
    sourceArray1[42] = (byte) 214;
    sourceArray1[6] = (byte) 133;
    sourceArray1[7] = (byte) 221;
    sourceArray1[2] = (byte) 228;
    sourceArray1[19] = (byte) 146;
    sourceArray1[28] = (byte) 21;
    sourceArray1[12] = (byte) 219;
    sourceArray1[45] = (byte) 238;
    sourceArray1[13] = (byte) 238;
    sourceArray1[1] = (byte) 201;
    sourceArray1[15] = (byte) 117;
    sourceArray1[10] = (byte) 123;
    sourceArray1[18] = (byte) 193;
    sourceArray1[4] = (byte) 230;
    sourceArray1[16 /*0x10*/] = (byte) 170;
    sourceArray1[20] = (byte) 155;
    sourceArray1[21] = (byte) 237;
    sourceArray1[41] = (byte) 13;
    sourceArray1[23] = (byte) 57;
    sourceArray1[5] = (byte) 49;
    sourceArray1[8] = (byte) 157;
    sourceArray1[26] = (byte) 14;
    sourceArray1[14] = (byte) 101;
    sourceArray1[44] = (byte) 101;
    sourceArray1[29] = (byte) 15;
    sourceArray1[30] = (byte) 183;
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[32 /*0x20*/] = (byte) 119;
    sourceArray1[33] = (byte) 68;
    sourceArray1[22] = (byte) 178;
    sourceArray1[0] = (byte) 129;
    sourceArray1[36] = (byte) 82;
    sourceArray1[11] = (byte) 159;
    sourceArray1[38] = (byte) 207;
    sourceArray1[24] = (byte) 197;
    sourceArray1[40] = (byte) 195;
    sourceArray1[9] = (byte) 158;
    sourceArray1[25] = (byte) 70;
    sourceArray1[43] = (byte) 151;
    sourceArray1[27] = (byte) 92;
    sourceArray1[35] = (byte) 68;
    sourceArray1[46] = (byte) 136;
    sourceArray1[39] = (byte) 239;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 145,
      (byte) 160 /*0xA0*/,
      (byte) 44,
      (byte) 181,
      (byte) 80 /*0x50*/,
      (byte) 45,
      (byte) 213,
      (byte) 13,
      (byte) 87,
      (byte) 145,
      (byte) 34,
      (byte) 154,
      (byte) 68,
      (byte) 7,
      (byte) 90,
      (byte) 243,
      (byte) 236,
      (byte) 193,
      (byte) 61,
      (byte) 175,
      (byte) 203,
      (byte) 144 /*0x90*/,
      (byte) 139,
      (byte) 17,
      (byte) 247,
      (byte) 20,
      (byte) 30,
      byte.MaxValue,
      (byte) 193,
      (byte) 170,
      (byte) 52,
      (byte) 201,
      (byte) 211,
      (byte) 108,
      (byte) 47,
      (byte) 230,
      (byte) 111,
      (byte) 44,
      (byte) 38,
      (byte) 143,
      (byte) 188,
      (byte) 129,
      (byte) 167,
      (byte) 120,
      (byte) 101,
      (byte) 169,
      (byte) 90
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 366, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_21885.sspq, 0, (Array) numArray2, 0, 39);
    key.Query(true, 366, numArray2, response2);
    Array.Copy((Array) sc_21885.sspr, 0, (Array) numArray2, 0, 39);
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
}
