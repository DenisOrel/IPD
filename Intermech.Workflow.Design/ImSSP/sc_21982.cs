// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21982
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21982
{
  private static byte[] sspq = new byte[47]
  {
    (byte) 112 /*0x70*/,
    (byte) 136,
    (byte) 130,
    (byte) 27,
    (byte) 48 /*0x30*/,
    (byte) 66,
    (byte) 152,
    (byte) 183,
    (byte) 46,
    (byte) 247,
    (byte) 106,
    (byte) 78,
    (byte) 142,
    (byte) 155,
    (byte) 32 /*0x20*/,
    (byte) 20,
    (byte) 89,
    (byte) 125,
    (byte) 97,
    (byte) 227,
    (byte) 94,
    (byte) 133,
    (byte) 198,
    (byte) 140,
    (byte) 212,
    (byte) 36,
    (byte) 226,
    (byte) 226,
    (byte) 253,
    (byte) 23,
    (byte) 245,
    (byte) 70,
    (byte) 234,
    (byte) 191,
    (byte) 209,
    (byte) 137,
    (byte) 50,
    (byte) 86,
    (byte) 208 /*0xD0*/,
    (byte) 169,
    (byte) 213,
    (byte) 237,
    (byte) 83,
    (byte) 196,
    (byte) 36,
    (byte) 119,
    (byte) 24
  };
  private static byte[] sspr = new byte[47]
  {
    (byte) 65,
    (byte) 181,
    (byte) 89,
    (byte) 79,
    (byte) 46,
    (byte) 240 /*0xF0*/,
    (byte) 203,
    (byte) 38,
    (byte) 45,
    (byte) 190,
    (byte) 117,
    (byte) 84,
    (byte) 176 /*0xB0*/,
    (byte) 87,
    (byte) 169,
    (byte) 196,
    (byte) 252,
    (byte) 152,
    (byte) 72,
    (byte) 175,
    (byte) 6,
    (byte) 235,
    (byte) 86,
    (byte) 168,
    (byte) 37,
    (byte) 168,
    (byte) 221,
    (byte) 183,
    (byte) 41,
    (byte) 251,
    (byte) 164,
    (byte) 128 /*0x80*/,
    (byte) 212,
    (byte) 74,
    (byte) 112 /*0x70*/,
    (byte) 71,
    (byte) 181,
    (byte) 111,
    (byte) 227,
    (byte) 30,
    (byte) 199,
    (byte) 129,
    (byte) 152,
    (byte) 2,
    (byte) 190,
    (byte) 100,
    (byte) 142
  };

  internal static int ssp_workflow_21983(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 182,
      (byte) 251,
      (byte) 232,
      (byte) 103,
      (byte) 120,
      (byte) 128 /*0x80*/,
      (byte) 36,
      (byte) 218,
      (byte) 4,
      (byte) 1,
      (byte) 221,
      (byte) 50,
      (byte) 201,
      (byte) 204,
      (byte) 126,
      (byte) 78,
      (byte) 81,
      (byte) 3,
      (byte) 8,
      (byte) 131,
      (byte) 27,
      (byte) 154,
      (byte) 203,
      (byte) 165,
      (byte) 207,
      (byte) 193,
      (byte) 81,
      (byte) 45,
      (byte) 112 /*0x70*/,
      (byte) 181,
      (byte) 115,
      (byte) 65,
      (byte) 18,
      (byte) 181,
      (byte) 114,
      (byte) 1,
      (byte) 236,
      (byte) 109,
      (byte) 165,
      (byte) 130,
      (byte) 188,
      (byte) 142,
      (byte) 198,
      (byte) 166,
      (byte) 195,
      (byte) 236,
      (byte) 195,
      (byte) 57
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 177,
      (byte) 139,
      (byte) 90,
      (byte) 186,
      (byte) 208 /*0xD0*/,
      (byte) 237,
      (byte) 126,
      (byte) 141,
      (byte) 213,
      (byte) 85,
      (byte) 140,
      (byte) 231,
      (byte) 24,
      (byte) 120,
      (byte) 72,
      (byte) 49,
      (byte) 242,
      (byte) 19,
      (byte) 129,
      (byte) 228,
      (byte) 211,
      (byte) 171,
      (byte) 71,
      (byte) 240 /*0xF0*/,
      (byte) 64 /*0x40*/,
      (byte) 30,
      (byte) 186,
      (byte) 132,
      (byte) 245,
      (byte) 33,
      (byte) 152,
      (byte) 110,
      (byte) 81,
      (byte) 108,
      (byte) 204,
      (byte) 91,
      (byte) 67,
      (byte) 50,
      (byte) 96 /*0x60*/,
      (byte) 101,
      (byte) 127 /*0x7F*/,
      (byte) 186,
      (byte) 69,
      (byte) 209,
      (byte) 40,
      (byte) 119,
      (byte) 51,
      (byte) 28
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 366, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[47];
    byte[] response2 = new byte[47];
    Array.Copy((Array) sc_21982.sspq, 0, (Array) numArray2, 0, 47);
    key.Query(true, 366, numArray2, response2);
    Array.Copy((Array) sc_21982.sspr, 0, (Array) numArray2, 0, 47);
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
