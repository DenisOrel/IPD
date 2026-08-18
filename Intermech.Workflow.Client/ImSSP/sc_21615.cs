// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21615
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21615
{
  internal static string ssp_workflow_21616()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 33,
        (byte) 243,
        (byte) 38,
        (byte) 232,
        (byte) 106,
        (byte) 221,
        (byte) 183,
        (byte) 88,
        (byte) 19,
        (byte) 222,
        (byte) 78
      };
      byte[] numArray3 = new byte[11];
      numArray3[8] = (byte) 42;
      numArray3[1] = (byte) 103;
      numArray3[10] = (byte) 120;
      numArray3[0] = (byte) 10;
      numArray3[3] = (byte) 124;
      numArray3[2] = (byte) 103;
      numArray3[6] = (byte) 78;
      numArray3[7] = (byte) 205;
      numArray3[4] = (byte) 191;
      numArray3[9] = (byte) 157;
      numArray3[5] = (byte) 254;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[1] = (byte) 68;
    numArray5[7] = (byte) 124;
    numArray5[2] = (byte) 157;
    numArray5[3] = (byte) 200;
    numArray5[4] = (byte) 97;
    numArray5[5] = (byte) 161;
    numArray5[6] = (byte) 150;
    numArray5[8] = (byte) 162;
    numArray5[10] = (byte) 37;
    numArray5[9] = (byte) 10;
    numArray5[0] = (byte) 205;
    byte[] numArray6 = new byte[11]
    {
      (byte) 134,
      (byte) 184,
      (byte) 212,
      (byte) 161,
      (byte) 115,
      (byte) 11,
      (byte) 206,
      (byte) 105,
      (byte) 105,
      (byte) 139,
      (byte) 28
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
