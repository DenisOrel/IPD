// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7783
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7783
{
  internal static string ssp_imbase_7784()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 99,
        (byte) 55,
        (byte) 49,
        (byte) 63 /*0x3F*/,
        (byte) 185,
        (byte) 222,
        (byte) 232
      };
      byte[] numArray3 = new byte[7];
      numArray3[6] = (byte) 57;
      numArray3[4] = (byte) 81;
      numArray3[3] = (byte) 163;
      numArray3[1] = (byte) 59;
      numArray3[2] = (byte) 125;
      numArray3[0] = (byte) 133;
      numArray3[5] = (byte) 147;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7];
    numArray5[1] = (byte) 98;
    numArray5[5] = (byte) 185;
    numArray5[4] = (byte) 202;
    numArray5[2] = (byte) 53;
    numArray5[0] = (byte) 131;
    numArray5[3] = (byte) 140;
    numArray5[6] = (byte) 153;
    byte[] numArray6 = new byte[7];
    numArray6[4] = (byte) 107;
    numArray6[1] = (byte) 11;
    numArray6[2] = (byte) 187;
    numArray6[0] = (byte) 68;
    numArray6[6] = (byte) 114;
    numArray6[5] = (byte) 243;
    numArray6[3] = (byte) 109;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7785()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 59,
        (byte) 71,
        (byte) 82,
        (byte) 20,
        (byte) 174,
        (byte) 216,
        (byte) 240 /*0xF0*/,
        (byte) 65,
        (byte) 139,
        (byte) 55,
        (byte) 14,
        (byte) 28,
        (byte) 153,
        (byte) 153,
        (byte) 125,
        (byte) 152,
        (byte) 120
      };
      byte[] numArray3 = new byte[17];
      numArray3[6] = (byte) 139;
      numArray3[1] = (byte) 142;
      numArray3[2] = (byte) 130;
      numArray3[9] = (byte) 15;
      numArray3[4] = (byte) 187;
      numArray3[16 /*0x10*/] = (byte) 205;
      numArray3[14] = (byte) 26;
      numArray3[7] = (byte) 216;
      numArray3[3] = (byte) 48 /*0x30*/;
      numArray3[5] = (byte) 53;
      numArray3[10] = (byte) 14;
      numArray3[11] = (byte) 41;
      numArray3[0] = (byte) 200;
      numArray3[13] = (byte) 33;
      numArray3[12] = (byte) 70;
      numArray3[15] = (byte) 111;
      numArray3[8] = (byte) 23;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 179,
      (byte) 144 /*0x90*/,
      (byte) 143,
      (byte) 133,
      (byte) 2,
      (byte) 213,
      (byte) 156,
      (byte) 165,
      (byte) 45,
      (byte) 46,
      (byte) 59,
      (byte) 21,
      (byte) 233,
      (byte) 189,
      (byte) 93,
      (byte) 153,
      (byte) 166
    };
    byte[] numArray6 = new byte[17];
    numArray6[12] = (byte) 108;
    numArray6[1] = (byte) 184;
    numArray6[2] = (byte) 81;
    numArray6[3] = (byte) 242;
    numArray6[4] = (byte) 105;
    numArray6[5] = (byte) 4;
    numArray6[7] = (byte) 146;
    numArray6[11] = (byte) 95;
    numArray6[14] = (byte) 97;
    numArray6[6] = (byte) 25;
    numArray6[10] = (byte) 240 /*0xF0*/;
    numArray6[8] = (byte) 213;
    numArray6[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    numArray6[13] = (byte) 111;
    numArray6[0] = (byte) 76;
    numArray6[15] = (byte) 230;
    numArray6[9] = (byte) 102;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7786()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 239,
        (byte) 104,
        (byte) 181,
        (byte) 184,
        (byte) 198,
        (byte) 114,
        (byte) 141,
        (byte) 104,
        (byte) 245,
        (byte) 199,
        (byte) 78,
        (byte) 185,
        (byte) 118,
        (byte) 168,
        (byte) 144 /*0x90*/,
        (byte) 124,
        (byte) 82
      };
      byte[] numArray3 = new byte[17];
      numArray3[6] = (byte) 120;
      numArray3[0] = (byte) 169;
      numArray3[5] = (byte) 184;
      numArray3[16 /*0x10*/] = (byte) 68;
      numArray3[1] = (byte) 44;
      numArray3[8] = (byte) 189;
      numArray3[2] = (byte) 222;
      numArray3[7] = (byte) 172;
      numArray3[10] = (byte) 52;
      numArray3[9] = (byte) 53;
      numArray3[3] = (byte) 198;
      numArray3[11] = (byte) 2;
      numArray3[14] = (byte) 69;
      numArray3[13] = (byte) 111;
      numArray3[4] = (byte) 48 /*0x30*/;
      numArray3[15] = (byte) 56;
      numArray3[12] = (byte) 68;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[11] = (byte) 122;
    numArray5[13] = byte.MaxValue;
    numArray5[15] = (byte) 103;
    numArray5[2] = (byte) 153;
    numArray5[3] = (byte) 128 /*0x80*/;
    numArray5[5] = (byte) 109;
    numArray5[7] = (byte) 62;
    numArray5[1] = (byte) 89;
    numArray5[8] = (byte) 2;
    numArray5[9] = (byte) 136;
    numArray5[10] = (byte) 79;
    numArray5[6] = (byte) 63 /*0x3F*/;
    numArray5[0] = (byte) 60;
    numArray5[16 /*0x10*/] = (byte) 102;
    numArray5[14] = (byte) 49;
    numArray5[4] = (byte) 11;
    numArray5[12] = (byte) 142;
    byte[] numArray6 = new byte[17]
    {
      (byte) 177,
      (byte) 223,
      (byte) 70,
      (byte) 185,
      (byte) 110,
      (byte) 104,
      (byte) 57,
      (byte) 159,
      (byte) 243,
      (byte) 2,
      (byte) 123,
      (byte) 200,
      (byte) 189,
      (byte) 2,
      (byte) 63 /*0x3F*/,
      (byte) 35,
      (byte) 122
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
