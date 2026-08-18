// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22131
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22131
{
  internal static string ssp_workflow_server_22132()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[1];
      byte[] numArray2 = new byte[1]{ (byte) 42 };
      byte[] numArray3 = new byte[1]{ (byte) 199 };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[1];
    byte[] numArray5 = new byte[1]{ (byte) 220 };
    byte[] numArray6 = new byte[1]{ (byte) 135 };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 1);
    for (int index = 0; index < 1; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22133()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13];
      numArray2[7] = (byte) 56;
      numArray2[1] = (byte) 224 /*0xE0*/;
      numArray2[11] = (byte) 199;
      numArray2[3] = (byte) 154;
      numArray2[12] = (byte) 15;
      numArray2[10] = (byte) 71;
      numArray2[8] = (byte) 41;
      numArray2[6] = (byte) 177;
      numArray2[9] = (byte) 84;
      numArray2[4] = (byte) 169;
      numArray2[2] = (byte) 2;
      numArray2[0] = byte.MaxValue;
      numArray2[5] = (byte) 83;
      byte[] numArray3 = new byte[13]
      {
        (byte) 155,
        (byte) 162,
        (byte) 60,
        (byte) 19,
        (byte) 36,
        (byte) 169,
        (byte) 166,
        (byte) 239,
        (byte) 90,
        (byte) 149,
        (byte) 125,
        (byte) 41,
        (byte) 51
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13]
    {
      (byte) 199,
      (byte) 166,
      (byte) 91,
      (byte) 248,
      (byte) 62,
      (byte) 103,
      (byte) 205,
      (byte) 189,
      (byte) 108,
      (byte) 167,
      (byte) 238,
      (byte) 225,
      (byte) 166
    };
    byte[] numArray6 = new byte[13]
    {
      (byte) 192 /*0xC0*/,
      (byte) 50,
      (byte) 250,
      (byte) 196,
      (byte) 64 /*0x40*/,
      (byte) 73,
      (byte) 50,
      (byte) 24,
      (byte) 122,
      (byte) 112 /*0x70*/,
      (byte) 166,
      (byte) 130,
      (byte) 197
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22134()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[3] = (byte) 225;
      numArray2[0] = (byte) 7;
      numArray2[8] = (byte) 44;
      numArray2[9] = (byte) 214;
      numArray2[4] = (byte) 212;
      numArray2[5] = (byte) 218;
      numArray2[6] = (byte) 240 /*0xF0*/;
      numArray2[7] = (byte) 17;
      numArray2[1] = (byte) 154;
      numArray2[10] = (byte) 103;
      numArray2[11] = (byte) 230;
      numArray2[2] = (byte) 155;
      byte[] numArray3 = new byte[12];
      numArray3[2] = (byte) 95;
      numArray3[1] = (byte) 47;
      numArray3[9] = (byte) 53;
      numArray3[5] = (byte) 66;
      numArray3[4] = (byte) 126;
      numArray3[11] = (byte) 163;
      numArray3[6] = (byte) 104;
      numArray3[10] = (byte) 215;
      numArray3[3] = (byte) 220;
      numArray3[7] = (byte) 249;
      numArray3[8] = (byte) 195;
      numArray3[0] = (byte) 246;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[6] = (byte) 214;
    numArray5[1] = (byte) 107;
    numArray5[2] = (byte) 231;
    numArray5[0] = (byte) 203;
    numArray5[4] = (byte) 144 /*0x90*/;
    numArray5[5] = (byte) 102;
    numArray5[8] = (byte) 184;
    numArray5[7] = (byte) 43;
    numArray5[3] = (byte) 18;
    numArray5[9] = (byte) 15;
    numArray5[10] = (byte) 25;
    numArray5[11] = (byte) 239;
    byte[] numArray6 = new byte[12];
    numArray6[7] = (byte) 184;
    numArray6[10] = (byte) 67;
    numArray6[0] = (byte) 216;
    numArray6[3] = (byte) 188;
    numArray6[4] = (byte) 167;
    numArray6[5] = (byte) 37;
    numArray6[6] = (byte) 59;
    numArray6[8] = (byte) 74;
    numArray6[1] = (byte) 214;
    numArray6[9] = (byte) 29;
    numArray6[2] = (byte) 141;
    numArray6[11] = (byte) 163;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
