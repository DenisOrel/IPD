// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7159
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7159
{
  internal static string ssp_imclient_7160()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[1] = (byte) 53;
      numArray2[8] = (byte) 37;
      numArray2[0] = (byte) 10;
      numArray2[6] = (byte) 196;
      numArray2[15] = (byte) 16 /*0x10*/;
      numArray2[5] = (byte) 153;
      numArray2[2] = (byte) 207;
      numArray2[7] = (byte) 137;
      numArray2[4] = (byte) 227;
      numArray2[13] = (byte) 186;
      numArray2[10] = (byte) 247;
      numArray2[3] = (byte) 15;
      numArray2[12] = (byte) 223;
      numArray2[11] = (byte) 132;
      numArray2[14] = (byte) 240 /*0xF0*/;
      numArray2[9] = (byte) 247;
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[8] = (byte) 8;
      numArray3[1] = (byte) 247;
      numArray3[6] = (byte) 106;
      numArray3[9] = (byte) 140;
      numArray3[4] = (byte) 176 /*0xB0*/;
      numArray3[5] = (byte) 245;
      numArray3[2] = (byte) 199;
      numArray3[12] = (byte) 165;
      numArray3[0] = (byte) 230;
      numArray3[7] = (byte) 122;
      numArray3[10] = (byte) 57;
      numArray3[11] = (byte) 44;
      numArray3[3] = (byte) 60;
      numArray3[13] = (byte) 202;
      numArray3[14] = (byte) 22;
      numArray3[15] = (byte) 157;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[2] = (byte) 49;
    numArray5[9] = (byte) 4;
    numArray5[3] = (byte) 96 /*0x60*/;
    numArray5[0] = (byte) 129;
    numArray5[4] = (byte) 212;
    numArray5[5] = (byte) 134;
    numArray5[13] = (byte) 132;
    numArray5[14] = (byte) 100;
    numArray5[8] = (byte) 8;
    numArray5[7] = (byte) 102;
    numArray5[6] = (byte) 204;
    numArray5[11] = (byte) 34;
    numArray5[12] = (byte) 17;
    numArray5[15] = (byte) 31 /*0x1F*/;
    numArray5[10] = (byte) 248;
    numArray5[1] = (byte) 34;
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[3] = (byte) 107;
    numArray6[12] = (byte) 207;
    numArray6[5] = (byte) 137;
    numArray6[9] = (byte) 174;
    numArray6[4] = (byte) 208 /*0xD0*/;
    numArray6[13] = (byte) 17;
    numArray6[6] = (byte) 21;
    numArray6[15] = (byte) 29;
    numArray6[1] = (byte) 75;
    numArray6[0] = (byte) 88;
    numArray6[10] = (byte) 187;
    numArray6[11] = (byte) 236;
    numArray6[7] = (byte) 84;
    numArray6[8] = (byte) 252;
    numArray6[14] = (byte) 224 /*0xE0*/;
    numArray6[2] = (byte) 164;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
