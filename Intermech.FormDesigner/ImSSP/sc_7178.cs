// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7178
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7178
{
  internal static string ssp_imclient_7179()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17];
      numArray2[3] = (byte) 254;
      numArray2[11] = (byte) 247;
      numArray2[2] = (byte) 9;
      numArray2[0] = (byte) 226;
      numArray2[12] = (byte) 62;
      numArray2[9] = (byte) 104;
      numArray2[1] = (byte) 45;
      numArray2[13] = (byte) 169;
      numArray2[8] = (byte) 137;
      numArray2[4] = (byte) 234;
      numArray2[6] = (byte) 132;
      numArray2[7] = (byte) 12;
      numArray2[10] = (byte) 212;
      numArray2[5] = (byte) 96 /*0x60*/;
      numArray2[14] = (byte) 214;
      numArray2[15] = (byte) 43;
      numArray2[16 /*0x10*/] = (byte) 134;
      byte[] numArray3 = new byte[17];
      numArray3[1] = (byte) 90;
      numArray3[0] = (byte) 87;
      numArray3[2] = (byte) 74;
      numArray3[12] = (byte) 184;
      numArray3[4] = (byte) 212;
      numArray3[9] = (byte) 95;
      numArray3[13] = (byte) 121;
      numArray3[14] = (byte) 85;
      numArray3[8] = (byte) 248;
      numArray3[7] = (byte) 172;
      numArray3[10] = (byte) 75;
      numArray3[6] = (byte) 239;
      numArray3[11] = (byte) 37;
      numArray3[3] = (byte) 32 /*0x20*/;
      numArray3[15] = (byte) 116;
      numArray3[5] = (byte) 71;
      numArray3[16 /*0x10*/] = (byte) 40;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 175,
      (byte) 253,
      (byte) 170,
      (byte) 37,
      (byte) 145,
      (byte) 102,
      (byte) 194,
      (byte) 136,
      (byte) 8,
      (byte) 247,
      (byte) 187,
      (byte) 149,
      (byte) 140,
      (byte) 10,
      (byte) 171,
      (byte) 8,
      (byte) 80 /*0x50*/
    };
    byte[] numArray6 = new byte[17];
    numArray6[9] = (byte) 10;
    numArray6[5] = (byte) 58;
    numArray6[12] = (byte) 33;
    numArray6[3] = (byte) 22;
    numArray6[1] = (byte) 33;
    numArray6[14] = (byte) 135;
    numArray6[6] = (byte) 147;
    numArray6[7] = (byte) 61;
    numArray6[11] = (byte) 10;
    numArray6[10] = (byte) 129;
    numArray6[2] = (byte) 210;
    numArray6[15] = (byte) 197;
    numArray6[0] = (byte) 20;
    numArray6[13] = (byte) 198;
    numArray6[4] = (byte) 167;
    numArray6[8] = (byte) 4;
    numArray6[16 /*0x10*/] = (byte) 145;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
