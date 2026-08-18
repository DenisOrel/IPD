// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21802
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21802
{
  internal static int ssp_workflow_21803(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 97,
      (byte) 135,
      (byte) 198,
      (byte) 194,
      (byte) 105,
      (byte) 231,
      (byte) 23,
      (byte) 141,
      (byte) 251,
      (byte) 176 /*0xB0*/,
      (byte) 21,
      (byte) 224 /*0xE0*/,
      (byte) 22,
      (byte) 76,
      (byte) 247,
      (byte) 90,
      (byte) 218,
      (byte) 172,
      (byte) 254,
      (byte) 58,
      (byte) 209,
      (byte) 146,
      (byte) 141,
      (byte) 64 /*0x40*/,
      (byte) 239,
      (byte) 77,
      (byte) 58,
      (byte) 251,
      (byte) 99,
      (byte) 49,
      (byte) 126,
      (byte) 41,
      (byte) 37,
      (byte) 170,
      (byte) 133,
      (byte) 89,
      (byte) 136,
      (byte) 176 /*0xB0*/,
      (byte) 50,
      (byte) 187,
      (byte) 218,
      (byte) 128 /*0x80*/,
      (byte) 132,
      (byte) 84,
      (byte) 185,
      (byte) 167,
      (byte) 118,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 127 /*0x7F*/,
      (byte) 230,
      (byte) 250,
      (byte) 23,
      (byte) 111,
      (byte) 179,
      (byte) 157,
      (byte) 172,
      (byte) 224 /*0xE0*/,
      (byte) 179,
      (byte) 237,
      (byte) 73,
      (byte) 228,
      (byte) 135,
      (byte) 185,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      (byte) 228,
      (byte) 140,
      (byte) 60,
      (byte) 124,
      (byte) 110,
      (byte) 19,
      (byte) 6,
      (byte) 175,
      (byte) 146,
      (byte) 87,
      (byte) 36,
      (byte) 179,
      (byte) 211,
      (byte) 21,
      (byte) 34,
      (byte) 187,
      (byte) 194,
      (byte) 80 /*0x50*/,
      (byte) 196,
      (byte) 108,
      (byte) 37,
      (byte) 226,
      (byte) 191,
      (byte) 83,
      (byte) 142,
      (byte) 203,
      (byte) 75,
      (byte) 62,
      (byte) 13,
      (byte) 45,
      (byte) 212
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_21804(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 243,
      (byte) 181,
      (byte) 206,
      (byte) 249,
      (byte) 134,
      (byte) 210,
      (byte) 27,
      (byte) 173,
      (byte) 27,
      (byte) 41,
      (byte) 201,
      (byte) 50,
      (byte) 236,
      (byte) 187,
      (byte) 108,
      (byte) 199,
      (byte) 158,
      (byte) 209,
      (byte) 42,
      (byte) 122,
      (byte) 66,
      (byte) 95,
      (byte) 80 /*0x50*/,
      (byte) 14,
      (byte) 57,
      (byte) 134,
      (byte) 71,
      (byte) 215,
      (byte) 84,
      (byte) 237,
      (byte) 23,
      (byte) 241,
      (byte) 110,
      (byte) 252,
      (byte) 49,
      (byte) 244,
      (byte) 182,
      (byte) 95,
      (byte) 2,
      (byte) 221,
      (byte) 22,
      (byte) 14,
      (byte) 190,
      (byte) 87,
      (byte) 186,
      (byte) 33,
      (byte) 205,
      (byte) 236
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 68,
      (byte) 91,
      (byte) 138,
      (byte) 106,
      (byte) 93,
      (byte) 163,
      (byte) 101,
      (byte) 36,
      (byte) 144 /*0x90*/,
      (byte) 134,
      (byte) 246,
      (byte) 187,
      (byte) 74,
      (byte) 210,
      (byte) 25,
      (byte) 180,
      (byte) 216,
      (byte) 171,
      (byte) 124,
      (byte) 9,
      (byte) 180,
      (byte) 119,
      (byte) 25,
      (byte) 44,
      (byte) 204,
      (byte) 19,
      (byte) 79,
      (byte) 234,
      (byte) 85,
      (byte) 120,
      (byte) 161,
      (byte) 246,
      (byte) 241,
      (byte) 174,
      (byte) 225,
      (byte) 49,
      (byte) 80 /*0x50*/,
      (byte) 214,
      (byte) 42,
      (byte) 154,
      (byte) 254,
      (byte) 222,
      (byte) 83,
      (byte) 61,
      (byte) 36,
      (byte) 87,
      (byte) 27
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
