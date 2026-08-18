// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21763
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21763
{
  internal static string ssp_workflow_21764()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[0] = (byte) 146;
      numArray2[1] = (byte) 21;
      numArray2[17] = (byte) 198;
      numArray2[2] = (byte) 28;
      numArray2[3] = (byte) 98;
      numArray2[5] = (byte) 110;
      numArray2[6] = (byte) 22;
      numArray2[7] = (byte) 20;
      numArray2[4] = (byte) 55;
      numArray2[9] = (byte) 183;
      numArray2[10] = (byte) 57;
      numArray2[8] = (byte) 246;
      numArray2[12] = (byte) 160 /*0xA0*/;
      numArray2[16 /*0x10*/] = (byte) 98;
      numArray2[13] = (byte) 158;
      numArray2[15] = (byte) 205;
      numArray2[18] = (byte) 144 /*0x90*/;
      numArray2[14] = (byte) 14;
      numArray2[11] = (byte) 30;
      byte[] numArray3 = new byte[19]
      {
        (byte) 139,
        (byte) 19,
        (byte) 252,
        (byte) 185,
        (byte) 239,
        (byte) 227,
        (byte) 251,
        (byte) 65,
        (byte) 225,
        (byte) 207,
        (byte) 120,
        (byte) 161,
        (byte) 141,
        (byte) 129,
        (byte) 61,
        (byte) 251,
        (byte) 50,
        (byte) 105,
        (byte) 13
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[11] = (byte) 247;
    numArray5[7] = (byte) 31 /*0x1F*/;
    numArray5[16 /*0x10*/] = (byte) 223;
    numArray5[3] = (byte) 144 /*0x90*/;
    numArray5[13] = (byte) 254;
    numArray5[10] = (byte) 38;
    numArray5[6] = (byte) 159;
    numArray5[18] = (byte) 142;
    numArray5[8] = (byte) 109;
    numArray5[9] = (byte) 241;
    numArray5[5] = (byte) 78;
    numArray5[4] = (byte) 235;
    numArray5[12] = (byte) 243;
    numArray5[1] = (byte) 42;
    numArray5[14] = (byte) 41;
    numArray5[15] = (byte) 140;
    numArray5[0] = (byte) 99;
    numArray5[17] = (byte) 223;
    numArray5[2] = (byte) 102;
    byte[] numArray6 = new byte[19];
    numArray6[16 /*0x10*/] = (byte) 8;
    numArray6[1] = (byte) 135;
    numArray6[2] = (byte) 83;
    numArray6[3] = (byte) 209;
    numArray6[4] = (byte) 250;
    numArray6[5] = (byte) 227;
    numArray6[18] = (byte) 184;
    numArray6[9] = (byte) 123;
    numArray6[8] = (byte) 245;
    numArray6[7] = (byte) 226;
    numArray6[13] = (byte) 140;
    numArray6[6] = (byte) 118;
    numArray6[14] = (byte) 116;
    numArray6[12] = (byte) 3;
    numArray6[11] = (byte) 211;
    numArray6[15] = (byte) 207;
    numArray6[0] = (byte) 84;
    numArray6[17] = (byte) 130;
    numArray6[10] = (byte) 44;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
