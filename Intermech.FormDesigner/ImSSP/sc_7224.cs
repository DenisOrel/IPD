// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7224
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7224
{
  internal static string ssp_imclient_7225()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 214,
        (byte) 17,
        (byte) 204,
        (byte) 92,
        (byte) 150,
        (byte) 91,
        (byte) 160 /*0xA0*/,
        (byte) 220,
        (byte) 190,
        (byte) 212,
        (byte) 163,
        (byte) 129,
        (byte) 7,
        (byte) 193,
        byte.MaxValue,
        (byte) 200
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[6] = (byte) 238;
      numArray3[1] = (byte) 189;
      numArray3[13] = (byte) 191;
      numArray3[8] = (byte) 209;
      numArray3[9] = (byte) 144 /*0x90*/;
      numArray3[5] = (byte) 241;
      numArray3[3] = (byte) 209;
      numArray3[2] = (byte) 63 /*0x3F*/;
      numArray3[14] = (byte) 166;
      numArray3[0] = (byte) 48 /*0x30*/;
      numArray3[10] = (byte) 5;
      numArray3[11] = (byte) 189;
      numArray3[12] = (byte) 96 /*0x60*/;
      numArray3[7] = (byte) 64 /*0x40*/;
      numArray3[4] = (byte) 239;
      numArray3[15] = (byte) 188;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[12] = (byte) 83;
    numArray5[1] = (byte) 115;
    numArray5[2] = (byte) 182;
    numArray5[6] = (byte) 105;
    numArray5[4] = (byte) 232;
    numArray5[5] = (byte) 172;
    numArray5[14] = (byte) 177;
    numArray5[13] = (byte) 80 /*0x50*/;
    numArray5[8] = (byte) 29;
    numArray5[9] = (byte) 217;
    numArray5[10] = (byte) 91;
    numArray5[11] = (byte) 187;
    numArray5[7] = (byte) 247;
    numArray5[0] = (byte) 224 /*0xE0*/;
    numArray5[3] = (byte) 81;
    numArray5[15] = (byte) 17;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 211,
      (byte) 7,
      (byte) 93,
      (byte) 58,
      (byte) 151,
      (byte) 187,
      (byte) 232,
      (byte) 118,
      (byte) 67,
      (byte) 60,
      (byte) 163,
      (byte) 219,
      (byte) 239,
      (byte) 253,
      (byte) 208 /*0xD0*/,
      (byte) 16 /*0x10*/
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
