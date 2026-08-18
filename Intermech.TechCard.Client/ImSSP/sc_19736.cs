// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19736
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19736
{
  internal static string ssp_techcard_19737()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[16 /*0x10*/] = (byte) 172;
      numArray2[10] = (byte) 140;
      numArray2[14] = (byte) 188;
      numArray2[3] = (byte) 67;
      numArray2[4] = (byte) 197;
      numArray2[5] = (byte) 105;
      numArray2[6] = byte.MaxValue;
      numArray2[9] = (byte) 213;
      numArray2[7] = (byte) 75;
      numArray2[12] = (byte) 111;
      numArray2[11] = (byte) 73;
      numArray2[13] = (byte) 52;
      numArray2[2] = (byte) 246;
      numArray2[17] = (byte) 178;
      numArray2[8] = (byte) 79;
      numArray2[1] = (byte) 239;
      numArray2[15] = (byte) 192 /*0xC0*/;
      numArray2[0] = (byte) 202;
      numArray2[18] = (byte) 74;
      byte[] numArray3 = new byte[19]
      {
        (byte) 179,
        (byte) 166,
        (byte) 88,
        (byte) 62,
        (byte) 238,
        (byte) 228,
        (byte) 214,
        (byte) 110,
        (byte) 80 /*0x50*/,
        (byte) 64 /*0x40*/,
        (byte) 139,
        (byte) 79,
        (byte) 98,
        (byte) 192 /*0xC0*/,
        (byte) 203,
        (byte) 238,
        (byte) 66,
        (byte) 2,
        (byte) 162
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[5] = (byte) 192 /*0xC0*/;
    numArray5[1] = (byte) 229;
    numArray5[2] = (byte) 139;
    numArray5[16 /*0x10*/] = (byte) 126;
    numArray5[4] = (byte) 194;
    numArray5[8] = (byte) 96 /*0x60*/;
    numArray5[6] = (byte) 114;
    numArray5[3] = (byte) 100;
    numArray5[11] = (byte) 202;
    numArray5[0] = (byte) 27;
    numArray5[10] = (byte) 117;
    numArray5[15] = (byte) 220;
    numArray5[9] = (byte) 115;
    numArray5[13] = (byte) 58;
    numArray5[14] = (byte) 214;
    numArray5[18] = (byte) 192 /*0xC0*/;
    numArray5[7] = (byte) 58;
    numArray5[12] = (byte) 106;
    numArray5[17] = (byte) 144 /*0x90*/;
    byte[] numArray6 = new byte[19]
    {
      (byte) 177,
      (byte) 241,
      (byte) 218,
      (byte) 173,
      (byte) 149,
      (byte) 28,
      (byte) 74,
      (byte) 105,
      (byte) 76,
      (byte) 247,
      (byte) 11,
      (byte) 26,
      (byte) 76,
      (byte) 250,
      (byte) 135,
      (byte) 30,
      (byte) 172,
      (byte) 157,
      (byte) 20
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
