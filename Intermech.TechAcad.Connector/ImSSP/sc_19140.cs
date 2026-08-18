// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19140
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19140
{
  internal static string ssp_techacad_19141()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 239,
        (byte) 13,
        (byte) 42,
        (byte) 76,
        (byte) 228,
        (byte) 129,
        (byte) 8,
        (byte) 231,
        (byte) 142,
        (byte) 175,
        (byte) 151,
        (byte) 30,
        (byte) 199,
        (byte) 122,
        (byte) 136,
        (byte) 37,
        (byte) 68,
        (byte) 156,
        (byte) 29,
        (byte) 233,
        (byte) 161
      };
      byte[] numArray3 = new byte[21];
      numArray3[10] = (byte) 167;
      numArray3[9] = (byte) 169;
      numArray3[0] = (byte) 70;
      numArray3[8] = (byte) 147;
      numArray3[4] = (byte) 41;
      numArray3[2] = (byte) 180;
      numArray3[6] = (byte) 131;
      numArray3[1] = (byte) 153;
      numArray3[13] = (byte) 3;
      numArray3[17] = (byte) 10;
      numArray3[7] = (byte) 220;
      numArray3[11] = (byte) 111;
      numArray3[15] = (byte) 219;
      numArray3[18] = (byte) 29;
      numArray3[14] = (byte) 201;
      numArray3[19] = (byte) 235;
      numArray3[16 /*0x10*/] = (byte) 125;
      numArray3[5] = (byte) 191;
      numArray3[12] = (byte) 6;
      numArray3[3] = (byte) 98;
      numArray3[20] = (byte) 74;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 248,
      (byte) 219,
      (byte) 166,
      byte.MaxValue,
      (byte) 220,
      (byte) 67,
      (byte) 83,
      (byte) 197,
      (byte) 143,
      (byte) 134,
      (byte) 194,
      (byte) 89,
      (byte) 98,
      (byte) 233,
      (byte) 188,
      (byte) 90,
      (byte) 26,
      (byte) 76,
      (byte) 251,
      (byte) 66,
      (byte) 50
    };
    byte[] numArray6 = new byte[21]
    {
      (byte) 117,
      (byte) 139,
      (byte) 172,
      (byte) 13,
      (byte) 43,
      (byte) 162,
      (byte) 16 /*0x10*/,
      (byte) 7,
      (byte) 175,
      (byte) 56,
      (byte) 170,
      (byte) 13,
      (byte) 89,
      (byte) 95,
      (byte) 230,
      (byte) 133,
      (byte) 162,
      (byte) 14,
      (byte) 197,
      (byte) 101,
      (byte) 111
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
