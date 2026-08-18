// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22158
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_22158
{
  internal static int ssp_workflow_server_22159(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 135,
      (byte) 67,
      (byte) 157,
      (byte) 83,
      (byte) 37,
      (byte) 182,
      (byte) 156,
      (byte) 157,
      (byte) 47,
      (byte) 91,
      (byte) 220,
      (byte) 179,
      (byte) 245,
      (byte) 227,
      (byte) 133,
      (byte) 239,
      (byte) 180,
      (byte) 42,
      (byte) 143,
      (byte) 8,
      (byte) 20,
      (byte) 173,
      (byte) 241,
      (byte) 211,
      (byte) 105,
      (byte) 234,
      (byte) 236,
      (byte) 3,
      (byte) 94,
      (byte) 228,
      (byte) 197,
      (byte) 191,
      (byte) 80 /*0x50*/,
      (byte) 121,
      (byte) 41,
      (byte) 125,
      (byte) 42,
      (byte) 53,
      (byte) 39,
      (byte) 249,
      (byte) 58,
      (byte) 214,
      (byte) 229,
      (byte) 31 /*0x1F*/,
      (byte) 100,
      (byte) 58,
      (byte) 241,
      (byte) 41
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 181,
      (byte) 236,
      (byte) 95,
      (byte) 252,
      (byte) 60,
      (byte) 116,
      (byte) 129,
      (byte) 87,
      (byte) 56,
      (byte) 63 /*0x3F*/,
      (byte) 110,
      (byte) 116,
      (byte) 232,
      (byte) 183,
      (byte) 155,
      (byte) 112 /*0x70*/,
      (byte) 158,
      (byte) 57,
      (byte) 138,
      byte.MaxValue,
      (byte) 78,
      (byte) 52,
      (byte) 209,
      (byte) 200,
      (byte) 55,
      (byte) 244,
      (byte) 105,
      (byte) 72,
      (byte) 68,
      (byte) 156,
      (byte) 171,
      (byte) 100,
      (byte) 138,
      (byte) 7,
      (byte) 247,
      (byte) 93,
      (byte) 235,
      (byte) 107,
      (byte) 61,
      (byte) 18,
      (byte) 155,
      (byte) 114,
      (byte) 2,
      (byte) 148,
      (byte) 59,
      (byte) 77,
      (byte) 221,
      (byte) 151
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
