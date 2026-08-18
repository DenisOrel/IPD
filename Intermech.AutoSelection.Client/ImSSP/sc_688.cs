// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_688
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_688
{
  internal static string ssp_automatch_689()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[17] = (byte) 131;
      numArray2[4] = (byte) 209;
      numArray2[0] = (byte) 26;
      numArray2[6] = (byte) 38;
      numArray2[15] = (byte) 86;
      numArray2[5] = (byte) 131;
      numArray2[1] = (byte) 171;
      numArray2[7] = (byte) 130;
      numArray2[14] = (byte) 183;
      numArray2[9] = (byte) 171;
      numArray2[10] = (byte) 138;
      numArray2[11] = (byte) 133;
      numArray2[19] = (byte) 73;
      numArray2[13] = (byte) 97;
      numArray2[2] = (byte) 9;
      numArray2[12] = (byte) 72;
      numArray2[8] = (byte) 181;
      numArray2[16 /*0x10*/] = (byte) 175;
      numArray2[18] = (byte) 214;
      numArray2[3] = (byte) 38;
      numArray2[20] = (byte) 214;
      numArray2[21] = (byte) 183;
      numArray2[22] = (byte) 240 /*0xF0*/;
      byte[] numArray3 = new byte[23];
      numArray3[5] = (byte) 207;
      numArray3[8] = (byte) 151;
      numArray3[21] = (byte) 229;
      numArray3[3] = (byte) 164;
      numArray3[17] = (byte) 138;
      numArray3[16 /*0x10*/] = (byte) 148;
      numArray3[6] = (byte) 111;
      numArray3[7] = (byte) 77;
      numArray3[11] = (byte) 168;
      numArray3[20] = (byte) 79;
      numArray3[10] = (byte) 118;
      numArray3[0] = (byte) 158;
      numArray3[12] = (byte) 227;
      numArray3[2] = (byte) 230;
      numArray3[14] = (byte) 123;
      numArray3[15] = (byte) 197;
      numArray3[4] = (byte) 9;
      numArray3[19] = (byte) 190;
      numArray3[18] = (byte) 63 /*0x3F*/;
      numArray3[9] = (byte) 117;
      numArray3[22] = (byte) 90;
      numArray3[1] = (byte) 77;
      numArray3[13] = (byte) 52;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[19] = (byte) 42;
    numArray5[15] = (byte) 237;
    numArray5[11] = (byte) 221;
    numArray5[22] = (byte) 224 /*0xE0*/;
    numArray5[9] = (byte) 131;
    numArray5[1] = (byte) 94;
    numArray5[6] = (byte) 41;
    numArray5[7] = (byte) 217;
    numArray5[8] = (byte) 140;
    numArray5[16 /*0x10*/] = (byte) 27;
    numArray5[3] = (byte) 10;
    numArray5[17] = (byte) 237;
    numArray5[12] = (byte) 124;
    numArray5[18] = (byte) 102;
    numArray5[13] = (byte) 92;
    numArray5[10] = (byte) 155;
    numArray5[21] = (byte) 62;
    numArray5[5] = (byte) 253;
    numArray5[4] = (byte) 89;
    numArray5[2] = (byte) 55;
    numArray5[0] = (byte) 166;
    numArray5[20] = (byte) 34;
    numArray5[14] = (byte) 153;
    byte[] numArray6 = new byte[23];
    numArray6[5] = (byte) 191;
    numArray6[2] = (byte) 237;
    numArray6[9] = (byte) 202;
    numArray6[10] = (byte) 32 /*0x20*/;
    numArray6[4] = (byte) 229;
    numArray6[18] = (byte) 3;
    numArray6[6] = (byte) 73;
    numArray6[0] = (byte) 246;
    numArray6[16 /*0x10*/] = (byte) 138;
    numArray6[8] = (byte) 36;
    numArray6[7] = (byte) 145;
    numArray6[14] = (byte) 234;
    numArray6[12] = (byte) 195;
    numArray6[13] = (byte) 104;
    numArray6[1] = (byte) 83;
    numArray6[15] = (byte) 56;
    numArray6[3] = (byte) 198;
    numArray6[17] = (byte) 245;
    numArray6[11] = (byte) 168;
    numArray6[19] = (byte) 252;
    numArray6[20] = (byte) 161;
    numArray6[21] = (byte) 4;
    numArray6[22] = (byte) 102;
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
