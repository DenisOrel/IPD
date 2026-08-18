// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_8122
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_8122
{
  internal static string ssp_appserver_8123()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[29];
      byte[] numArray2 = new byte[29]
      {
        (byte) 240 /*0xF0*/,
        (byte) 104,
        (byte) 213,
        (byte) 42,
        (byte) 130,
        (byte) 230,
        (byte) 131,
        (byte) 42,
        (byte) 64 /*0x40*/,
        (byte) 232,
        (byte) 139,
        (byte) 175,
        (byte) 99,
        (byte) 175,
        (byte) 196,
        (byte) 142,
        (byte) 115,
        (byte) 2,
        (byte) 105,
        (byte) 110,
        (byte) 197,
        (byte) 88,
        (byte) 82,
        (byte) 12,
        (byte) 76,
        (byte) 157,
        (byte) 232,
        (byte) 181,
        (byte) 23
      };
      byte[] numArray3 = new byte[29];
      numArray3[4] = (byte) 63 /*0x3F*/;
      numArray3[1] = (byte) 98;
      numArray3[2] = (byte) 193;
      numArray3[3] = (byte) 135;
      numArray3[18] = (byte) 121;
      numArray3[5] = (byte) 26;
      numArray3[23] = (byte) 57;
      numArray3[14] = (byte) 163;
      numArray3[9] = (byte) 252;
      numArray3[8] = (byte) 171;
      numArray3[11] = (byte) 47;
      numArray3[16 /*0x10*/] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 119;
      numArray3[19] = (byte) 71;
      numArray3[7] = (byte) 138;
      numArray3[15] = (byte) 15;
      numArray3[13] = (byte) 234;
      numArray3[17] = (byte) 17;
      numArray3[0] = (byte) 41;
      numArray3[10] = (byte) 196;
      numArray3[22] = (byte) 193;
      numArray3[21] = (byte) 234;
      numArray3[24] = (byte) 204;
      numArray3[12] = (byte) 187;
      numArray3[20] = (byte) 146;
      numArray3[25] = (byte) 9;
      numArray3[26] = (byte) 15;
      numArray3[27] = (byte) 103;
      numArray3[28] = (byte) 195;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[29];
    byte[] numArray5 = new byte[29];
    numArray5[16 /*0x10*/] = (byte) 137;
    numArray5[1] = (byte) 19;
    numArray5[14] = (byte) 44;
    numArray5[24] = (byte) 13;
    numArray5[2] = (byte) 190;
    numArray5[26] = (byte) 46;
    numArray5[6] = (byte) 150;
    numArray5[25] = (byte) 16 /*0x10*/;
    numArray5[11] = (byte) 52;
    numArray5[9] = (byte) 57;
    numArray5[10] = (byte) 15;
    numArray5[3] = (byte) 163;
    numArray5[12] = (byte) 49;
    numArray5[13] = (byte) 31 /*0x1F*/;
    numArray5[21] = (byte) 78;
    numArray5[15] = (byte) 55;
    numArray5[4] = (byte) 32 /*0x20*/;
    numArray5[27] = (byte) 147;
    numArray5[18] = (byte) 170;
    numArray5[8] = (byte) 155;
    numArray5[20] = (byte) 34;
    numArray5[7] = (byte) 250;
    numArray5[22] = (byte) 82;
    numArray5[17] = (byte) 99;
    numArray5[0] = (byte) 205;
    numArray5[23] = (byte) 50;
    numArray5[5] = (byte) 214;
    numArray5[19] = (byte) 90;
    numArray5[28] = (byte) 28;
    byte[] numArray6 = new byte[29];
    numArray6[1] = (byte) 101;
    numArray6[26] = (byte) 107;
    numArray6[9] = (byte) 219;
    numArray6[27] = (byte) 107;
    numArray6[2] = (byte) 124;
    numArray6[0] = (byte) 71;
    numArray6[6] = (byte) 28;
    numArray6[7] = (byte) 216;
    numArray6[8] = (byte) 176 /*0xB0*/;
    numArray6[15] = (byte) 180;
    numArray6[25] = (byte) 61;
    numArray6[11] = (byte) 50;
    numArray6[10] = (byte) 142;
    numArray6[13] = (byte) 185;
    numArray6[14] = (byte) 101;
    numArray6[17] = (byte) 158;
    numArray6[16 /*0x10*/] = (byte) 39;
    numArray6[3] = (byte) 230;
    numArray6[18] = (byte) 166;
    numArray6[19] = (byte) 92;
    numArray6[20] = (byte) 210;
    numArray6[28] = (byte) 190;
    numArray6[5] = (byte) 15;
    numArray6[23] = (byte) 235;
    numArray6[24] = (byte) 244;
    numArray6[12] = (byte) 58;
    numArray6[22] = (byte) 112 /*0x70*/;
    numArray6[21] = (byte) 20;
    numArray6[4] = (byte) 124;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 29);
    for (int index = 0; index < 29; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
