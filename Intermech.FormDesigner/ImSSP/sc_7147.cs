// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7147
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7147
{
  internal static string ssp_imclient_7148()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[2] = (byte) 117;
      numArray2[1] = (byte) 55;
      numArray2[5] = (byte) 230;
      numArray2[4] = (byte) 51;
      numArray2[6] = (byte) 175;
      numArray2[15] = (byte) 145;
      numArray2[14] = (byte) 236;
      numArray2[7] = (byte) 111;
      numArray2[8] = (byte) 90;
      numArray2[9] = (byte) 109;
      numArray2[10] = (byte) 155;
      numArray2[12] = (byte) 246;
      numArray2[11] = (byte) 148;
      numArray2[13] = (byte) 26;
      numArray2[0] = (byte) 109;
      numArray2[3] = (byte) 79;
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[11] = (byte) 97;
      numArray3[3] = (byte) 121;
      numArray3[8] = (byte) 185;
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[6] = (byte) 132;
      numArray3[13] = (byte) 226;
      numArray3[0] = (byte) 240 /*0xF0*/;
      numArray3[7] = (byte) 37;
      numArray3[1] = (byte) 155;
      numArray3[5] = (byte) 18;
      numArray3[12] = (byte) 105;
      numArray3[10] = (byte) 45;
      numArray3[9] = (byte) 44;
      numArray3[4] = (byte) 184;
      numArray3[14] = (byte) 198;
      numArray3[15] = (byte) 153;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[3] = (byte) 59;
    numArray5[8] = (byte) 166;
    numArray5[2] = (byte) 90;
    numArray5[15] = (byte) 35;
    numArray5[4] = (byte) 235;
    numArray5[5] = (byte) 122;
    numArray5[6] = (byte) 123;
    numArray5[7] = (byte) 102;
    numArray5[11] = (byte) 166;
    numArray5[13] = (byte) 14;
    numArray5[10] = (byte) 65;
    numArray5[14] = (byte) 51;
    numArray5[0] = (byte) 143;
    numArray5[12] = (byte) 167;
    numArray5[1] = (byte) 47;
    numArray5[9] = (byte) 158;
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[1] = (byte) 87;
    numArray6[5] = (byte) 166;
    numArray6[6] = (byte) 193;
    numArray6[14] = (byte) 16 /*0x10*/;
    numArray6[4] = (byte) 180;
    numArray6[3] = (byte) 249;
    numArray6[0] = (byte) 98;
    numArray6[7] = (byte) 189;
    numArray6[8] = (byte) 183;
    numArray6[9] = (byte) 54;
    numArray6[10] = (byte) 213;
    numArray6[11] = (byte) 190;
    numArray6[13] = (byte) 186;
    numArray6[2] = (byte) 76;
    numArray6[12] = (byte) 33;
    numArray6[15] = (byte) 33;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
