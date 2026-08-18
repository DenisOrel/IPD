// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21782
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21782
{
  internal static int ssp_workflow_21783(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 214,
      (byte) 58,
      (byte) 67,
      (byte) 84,
      (byte) 24,
      (byte) 151,
      (byte) 72,
      (byte) 235,
      (byte) 106,
      (byte) 51,
      (byte) 71,
      (byte) 51,
      (byte) 215,
      (byte) 170,
      (byte) 77,
      (byte) 71,
      (byte) 161,
      (byte) 44,
      (byte) 223,
      (byte) 18,
      (byte) 147,
      (byte) 15,
      (byte) 119,
      (byte) 218,
      (byte) 108,
      (byte) 1,
      (byte) 10,
      (byte) 77,
      (byte) 58,
      (byte) 84,
      (byte) 82,
      (byte) 49,
      (byte) 231,
      (byte) 224 /*0xE0*/,
      (byte) 79,
      (byte) 114,
      (byte) 155,
      (byte) 163,
      (byte) 241,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 5,
      (byte) 207,
      (byte) 136,
      (byte) 140,
      (byte) 50,
      (byte) 5,
      (byte) 166
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 144 /*0x90*/,
      (byte) 15,
      (byte) 81,
      (byte) 116,
      (byte) 23,
      (byte) 46,
      (byte) 88,
      byte.MaxValue,
      (byte) 178,
      (byte) 128 /*0x80*/,
      (byte) 96 /*0x60*/,
      (byte) 152,
      (byte) 113,
      (byte) 175,
      (byte) 210,
      (byte) 253,
      (byte) 95,
      (byte) 44,
      (byte) 233,
      (byte) 154,
      (byte) 143,
      (byte) 153,
      (byte) 5,
      (byte) 120,
      (byte) 232,
      (byte) 205,
      (byte) 21,
      (byte) 239,
      (byte) 111,
      (byte) 90,
      (byte) 102,
      (byte) 46,
      (byte) 98,
      (byte) 190,
      (byte) 60,
      (byte) 5,
      (byte) 185,
      (byte) 66,
      (byte) 140,
      (byte) 212,
      (byte) 64 /*0x40*/,
      (byte) 203,
      (byte) 107,
      (byte) 155,
      (byte) 238,
      (byte) 176 /*0xB0*/,
      (byte) 98
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
