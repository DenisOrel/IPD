// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21814
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21814
{
  internal static string ssp_workflow_21815()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[6] = (byte) 192 /*0xC0*/;
      numArray2[1] = (byte) 149;
      numArray2[8] = (byte) 138;
      numArray2[3] = (byte) 147;
      numArray2[4] = (byte) 100;
      numArray2[5] = (byte) 192 /*0xC0*/;
      numArray2[14] = (byte) 196;
      numArray2[7] = (byte) 0;
      numArray2[9] = (byte) 68;
      numArray2[2] = (byte) 234;
      numArray2[10] = (byte) 88;
      numArray2[0] = (byte) 21;
      numArray2[13] = (byte) 215;
      numArray2[16 /*0x10*/] = (byte) 137;
      numArray2[12] = (byte) 18;
      numArray2[15] = (byte) 240 /*0xF0*/;
      numArray2[11] = (byte) 46;
      numArray2[17] = (byte) 119;
      byte[] numArray3 = new byte[18]
      {
        (byte) 203,
        (byte) 177,
        (byte) 219,
        (byte) 54,
        (byte) 239,
        (byte) 174,
        (byte) 0,
        (byte) 56,
        (byte) 133,
        (byte) 104,
        (byte) 224 /*0xE0*/,
        (byte) 62,
        (byte) 94,
        (byte) 77,
        (byte) 24,
        (byte) 29,
        (byte) 88,
        (byte) 95
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 246,
      (byte) 159,
      (byte) 2,
      (byte) 82,
      (byte) 210,
      (byte) 181,
      (byte) 144 /*0x90*/,
      (byte) 68,
      (byte) 226,
      (byte) 93,
      (byte) 221,
      (byte) 98,
      (byte) 145,
      (byte) 13,
      (byte) 50,
      (byte) 171,
      (byte) 200,
      (byte) 49
    };
    byte[] numArray6 = new byte[18];
    numArray6[17] = (byte) 32 /*0x20*/;
    numArray6[1] = (byte) 231;
    numArray6[6] = (byte) 230;
    numArray6[3] = (byte) 10;
    numArray6[5] = (byte) 108;
    numArray6[0] = (byte) 227;
    numArray6[10] = (byte) 199;
    numArray6[7] = (byte) 51;
    numArray6[8] = (byte) 216;
    numArray6[9] = (byte) 40;
    numArray6[2] = (byte) 187;
    numArray6[11] = (byte) 71;
    numArray6[16 /*0x10*/] = (byte) 56;
    numArray6[13] = (byte) 185;
    numArray6[14] = (byte) 60;
    numArray6[15] = (byte) 81;
    numArray6[4] = (byte) 232;
    numArray6[12] = (byte) 94;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_21816()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 16 /*0x10*/,
        (byte) 180,
        (byte) 113,
        (byte) 186,
        (byte) 219,
        (byte) 249,
        (byte) 60,
        (byte) 40,
        (byte) 74,
        (byte) 241,
        (byte) 18,
        (byte) 168,
        (byte) 90,
        (byte) 171,
        (byte) 36,
        (byte) 138,
        (byte) 246,
        (byte) 187
      };
      byte[] numArray3 = new byte[18];
      numArray3[16 /*0x10*/] = (byte) 189;
      numArray3[1] = (byte) 18;
      numArray3[17] = (byte) 12;
      numArray3[3] = (byte) 171;
      numArray3[4] = (byte) 25;
      numArray3[11] = (byte) 34;
      numArray3[15] = (byte) 63 /*0x3F*/;
      numArray3[5] = (byte) 82;
      numArray3[8] = (byte) 180;
      numArray3[7] = (byte) 11;
      numArray3[13] = (byte) 69;
      numArray3[6] = (byte) 203;
      numArray3[12] = (byte) 74;
      numArray3[0] = (byte) 61;
      numArray3[14] = (byte) 15;
      numArray3[10] = (byte) 240 /*0xF0*/;
      numArray3[2] = (byte) 110;
      numArray3[9] = (byte) 124;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[14] = (byte) 53;
    numArray5[0] = (byte) 54;
    numArray5[2] = (byte) 249;
    numArray5[4] = (byte) 132;
    numArray5[17] = (byte) 230;
    numArray5[1] = (byte) 58;
    numArray5[6] = (byte) 66;
    numArray5[13] = (byte) 214;
    numArray5[8] = (byte) 63 /*0x3F*/;
    numArray5[9] = (byte) 196;
    numArray5[10] = (byte) 143;
    numArray5[11] = (byte) 168;
    numArray5[12] = (byte) 18;
    numArray5[15] = (byte) 235;
    numArray5[7] = (byte) 131;
    numArray5[3] = (byte) 103;
    numArray5[16 /*0x10*/] = (byte) 50;
    numArray5[5] = (byte) 233;
    byte[] numArray6 = new byte[18];
    numArray6[14] = (byte) 125;
    numArray6[11] = (byte) 125;
    numArray6[2] = (byte) 73;
    numArray6[3] = (byte) 227;
    numArray6[16 /*0x10*/] = (byte) 157;
    numArray6[5] = (byte) 46;
    numArray6[6] = (byte) 69;
    numArray6[9] = (byte) 108;
    numArray6[8] = (byte) 248;
    numArray6[13] = (byte) 192 /*0xC0*/;
    numArray6[10] = (byte) 128 /*0x80*/;
    numArray6[17] = (byte) 181;
    numArray6[12] = (byte) 85;
    numArray6[0] = (byte) 189;
    numArray6[1] = (byte) 77;
    numArray6[15] = (byte) 13;
    numArray6[7] = (byte) 223;
    numArray6[4] = (byte) 119;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
