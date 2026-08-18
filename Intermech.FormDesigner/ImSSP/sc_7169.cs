// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7169
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7169
{
  internal static string ssp_imclient_7170()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[6] = (byte) 66;
      numArray2[1] = (byte) 70;
      numArray2[2] = (byte) 155;
      numArray2[0] = (byte) 121;
      numArray2[5] = (byte) 175;
      numArray2[13] = (byte) 188;
      numArray2[3] = (byte) 205;
      numArray2[4] = (byte) 74;
      numArray2[8] = (byte) 66;
      numArray2[9] = (byte) 95;
      numArray2[10] = (byte) 181;
      numArray2[11] = (byte) 198;
      numArray2[7] = (byte) 42;
      numArray2[12] = (byte) 97;
      numArray2[14] = (byte) 59;
      byte[] numArray3 = new byte[15];
      numArray3[2] = (byte) 168;
      numArray3[1] = (byte) 223;
      numArray3[13] = (byte) 197;
      numArray3[3] = (byte) 133;
      numArray3[6] = (byte) 26;
      numArray3[8] = (byte) 13;
      numArray3[4] = (byte) 127 /*0x7F*/;
      numArray3[7] = (byte) 39;
      numArray3[9] = (byte) 205;
      numArray3[12] = (byte) 251;
      numArray3[10] = (byte) 13;
      numArray3[11] = (byte) 139;
      numArray3[0] = (byte) 164;
      numArray3[14] = (byte) 59;
      numArray3[5] = (byte) 77;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 128 /*0x80*/,
      (byte) 1,
      (byte) 83,
      (byte) 60,
      (byte) 236,
      (byte) 252,
      (byte) 24,
      (byte) 182,
      (byte) 22,
      (byte) 147,
      (byte) 78,
      (byte) 45,
      (byte) 68,
      (byte) 223,
      (byte) 251
    };
    byte[] numArray6 = new byte[15];
    numArray6[1] = (byte) 156;
    numArray6[2] = (byte) 63 /*0x3F*/;
    numArray6[6] = (byte) 181;
    numArray6[3] = (byte) 93;
    numArray6[13] = (byte) 209;
    numArray6[5] = (byte) 116;
    numArray6[12] = (byte) 70;
    numArray6[9] = (byte) 196;
    numArray6[8] = (byte) 9;
    numArray6[0] = (byte) 185;
    numArray6[10] = (byte) 164;
    numArray6[11] = (byte) 153;
    numArray6[7] = (byte) 12;
    numArray6[4] = (byte) 174;
    numArray6[14] = (byte) 120;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
