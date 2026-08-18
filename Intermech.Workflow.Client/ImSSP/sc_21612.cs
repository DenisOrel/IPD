// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21612
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21612
{
  internal static int ssp_workflow_21613(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 62,
      (byte) 202,
      (byte) 30,
      (byte) 50,
      (byte) 68,
      (byte) 107,
      (byte) 231,
      (byte) 194,
      (byte) 185,
      (byte) 221,
      (byte) 71,
      (byte) 122,
      (byte) 36,
      (byte) 94,
      (byte) 54,
      (byte) 192 /*0xC0*/,
      (byte) 114,
      (byte) 32 /*0x20*/,
      (byte) 60,
      (byte) 110,
      (byte) 163,
      (byte) 86,
      (byte) 95,
      (byte) 64 /*0x40*/,
      (byte) 123,
      (byte) 236,
      (byte) 87,
      (byte) 8,
      (byte) 115,
      (byte) 44,
      (byte) 59,
      (byte) 43,
      (byte) 24,
      (byte) 101,
      (byte) 144 /*0x90*/,
      (byte) 4,
      (byte) 233,
      (byte) 22,
      (byte) 175,
      (byte) 163,
      (byte) 200,
      (byte) 183,
      (byte) 216,
      (byte) 146,
      (byte) 252,
      (byte) 221,
      (byte) 35,
      (byte) 246
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 252,
      (byte) 171,
      (byte) 114,
      (byte) 137,
      (byte) 82,
      (byte) 54,
      (byte) 200,
      (byte) 13,
      (byte) 103,
      (byte) 182,
      (byte) 151,
      (byte) 139,
      (byte) 78,
      (byte) 187,
      (byte) 137,
      (byte) 162,
      (byte) 149,
      (byte) 40,
      (byte) 131,
      (byte) 235,
      (byte) 15,
      (byte) 36,
      (byte) 223,
      (byte) 113,
      (byte) 222,
      (byte) 138,
      (byte) 35,
      (byte) 118,
      byte.MaxValue,
      (byte) 125,
      (byte) 66,
      (byte) 217,
      (byte) 243,
      (byte) 82,
      (byte) 35,
      (byte) 208 /*0xD0*/,
      (byte) 54,
      (byte) 51,
      (byte) 201,
      (byte) 128 /*0x80*/,
      (byte) 67,
      (byte) 144 /*0x90*/,
      (byte) 240 /*0xF0*/,
      (byte) 229,
      (byte) 48 /*0x30*/,
      (byte) 58,
      (byte) 159,
      (byte) 81
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
