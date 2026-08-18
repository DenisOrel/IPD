// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21910
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21910
{
  internal static int ssp_workflow_21911(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 112 /*0x70*/,
      (byte) 36,
      (byte) 85,
      (byte) 128 /*0x80*/,
      (byte) 50,
      (byte) 206,
      (byte) 20,
      (byte) 75,
      (byte) 126,
      (byte) 65,
      (byte) 123,
      (byte) 20,
      (byte) 251,
      (byte) 35,
      (byte) 4,
      (byte) 224 /*0xE0*/,
      (byte) 119,
      (byte) 121,
      (byte) 122,
      (byte) 92,
      (byte) 248,
      (byte) 83,
      (byte) 13,
      (byte) 233,
      (byte) 174,
      (byte) 144 /*0x90*/,
      (byte) 228,
      (byte) 89,
      (byte) 23,
      (byte) 80 /*0x50*/,
      (byte) 247,
      (byte) 197,
      (byte) 163,
      (byte) 168,
      (byte) 75,
      (byte) 157,
      (byte) 188,
      (byte) 226,
      (byte) 192 /*0xC0*/,
      (byte) 173,
      (byte) 89,
      (byte) 226,
      (byte) 134,
      (byte) 242,
      (byte) 204,
      (byte) 185,
      (byte) 112 /*0x70*/,
      (byte) 207
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 125,
      (byte) 15,
      (byte) 250,
      (byte) 76,
      (byte) 213,
      (byte) 245,
      (byte) 53,
      (byte) 196,
      (byte) 0,
      (byte) 30,
      (byte) 110,
      (byte) 233,
      (byte) 58,
      (byte) 125,
      (byte) 99,
      (byte) 142,
      (byte) 16 /*0x10*/,
      (byte) 83,
      (byte) 19,
      (byte) 38,
      (byte) 66,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 225,
      (byte) 204,
      (byte) 206,
      (byte) 86,
      (byte) 16 /*0x10*/,
      (byte) 77,
      (byte) 164,
      (byte) 139,
      (byte) 204,
      (byte) 146,
      (byte) 81,
      (byte) 12,
      (byte) 112 /*0x70*/,
      (byte) 165,
      (byte) 159,
      (byte) 122,
      (byte) 16 /*0x10*/,
      (byte) 197,
      (byte) 185,
      (byte) 142,
      (byte) 219,
      byte.MaxValue,
      (byte) 206,
      (byte) 9,
      (byte) 74
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
