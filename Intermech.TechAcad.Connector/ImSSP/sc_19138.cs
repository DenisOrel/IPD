// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19138
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19138
{
  internal static string ssp_techacad_19139()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 169,
        (byte) 12,
        (byte) 224 /*0xE0*/,
        (byte) 198,
        (byte) 43,
        (byte) 67,
        (byte) 218,
        (byte) 57,
        (byte) 252,
        (byte) 201,
        (byte) 83,
        (byte) 21,
        (byte) 248,
        (byte) 67,
        (byte) 95,
        (byte) 14,
        (byte) 121,
        (byte) 202,
        (byte) 72,
        (byte) 117
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 24,
        (byte) 14,
        (byte) 172,
        (byte) 207,
        (byte) 193,
        (byte) 130,
        (byte) 46,
        (byte) 189,
        (byte) 39,
        (byte) 77,
        (byte) 151,
        (byte) 124,
        (byte) 158,
        (byte) 152,
        (byte) 234,
        byte.MaxValue,
        (byte) 38,
        (byte) 113,
        (byte) 181,
        (byte) 175
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 72,
      (byte) 167,
      (byte) 100,
      (byte) 200,
      (byte) 82,
      (byte) 19,
      (byte) 183,
      (byte) 44,
      (byte) 54,
      (byte) 44,
      (byte) 87,
      (byte) 21,
      (byte) 172,
      (byte) 71,
      (byte) 23,
      (byte) 162,
      (byte) 32 /*0x20*/,
      (byte) 136,
      (byte) 1,
      (byte) 135
    };
    byte[] numArray6 = new byte[20]
    {
      (byte) 195,
      (byte) 184,
      (byte) 210,
      (byte) 109,
      (byte) 181,
      (byte) 135,
      (byte) 47,
      (byte) 44,
      (byte) 195,
      (byte) 27,
      (byte) 67,
      (byte) 89,
      (byte) 132,
      (byte) 160 /*0xA0*/,
      (byte) 126,
      (byte) 140,
      (byte) 250,
      (byte) 230,
      (byte) 208 /*0xD0*/,
      (byte) 92
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
