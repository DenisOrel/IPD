// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_16997
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_16997
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 163,
    (byte) 223,
    (byte) 209,
    (byte) 178,
    (byte) 100,
    (byte) 130,
    (byte) 29,
    (byte) 228,
    (byte) 230,
    (byte) 214,
    (byte) 216,
    (byte) 205,
    (byte) 65,
    (byte) 246,
    (byte) 97,
    (byte) 158,
    (byte) 71,
    (byte) 87,
    (byte) 180,
    (byte) 67,
    (byte) 191,
    (byte) 202,
    (byte) 246,
    (byte) 241,
    (byte) 25,
    (byte) 192 /*0xC0*/,
    (byte) 66,
    (byte) 200,
    (byte) 14,
    (byte) 21,
    (byte) 216,
    (byte) 130,
    (byte) 31 /*0x1F*/,
    (byte) 193,
    (byte) 246
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 77,
    (byte) 194,
    (byte) 165,
    (byte) 129,
    (byte) 36,
    (byte) 130,
    (byte) 217,
    (byte) 232,
    (byte) 231,
    (byte) 201,
    (byte) 133,
    (byte) 62,
    (byte) 161,
    (byte) 80 /*0x50*/,
    (byte) 254,
    (byte) 98,
    (byte) 60,
    (byte) 124,
    (byte) 84,
    (byte) 117,
    (byte) 147,
    (byte) 161,
    (byte) 138,
    (byte) 217,
    (byte) 20,
    (byte) 151,
    (byte) 236,
    (byte) 214,
    (byte) 71,
    (byte) 157,
    (byte) 33,
    (byte) 141,
    (byte) 205,
    (byte) 61,
    (byte) 96 /*0x60*/
  };

  internal static string ssp_pdm_server_16998()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 71,
        (byte) 144 /*0x90*/,
        (byte) 3,
        (byte) 122,
        (byte) 103,
        (byte) 233,
        (byte) 125,
        (byte) 160 /*0xA0*/,
        (byte) 218,
        (byte) 196,
        (byte) 6,
        (byte) 81
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 55,
        (byte) 134,
        (byte) 129,
        (byte) 215,
        (byte) 128 /*0x80*/,
        (byte) 179,
        (byte) 122,
        (byte) 57,
        (byte) 163,
        (byte) 202,
        (byte) 144 /*0x90*/,
        (byte) 28
      };
      key.Query(true, 350, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[4] = (byte) 32 /*0x20*/;
    numArray5[1] = (byte) 103;
    numArray5[3] = (byte) 148;
    numArray5[2] = (byte) 130;
    numArray5[6] = (byte) 5;
    numArray5[5] = (byte) 141;
    numArray5[0] = (byte) 98;
    numArray5[11] = (byte) 146;
    numArray5[8] = (byte) 161;
    numArray5[9] = (byte) 220;
    numArray5[10] = (byte) 32 /*0x20*/;
    numArray5[7] = (byte) 154;
    byte[] numArray6 = new byte[12]
    {
      (byte) 237,
      (byte) 244,
      (byte) 246,
      (byte) 5,
      (byte) 64 /*0x40*/,
      (byte) 39,
      (byte) 11,
      (byte) 238,
      (byte) 135,
      (byte) 231,
      (byte) 20,
      (byte) 223
    };
    key.Query(true, 350, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_16997.sspq, 0, (Array) numArray7, 0, 35);
    key.Query(true, 350, numArray7, response);
    Array.Copy((Array) sc_16997.sspr, 0, (Array) numArray7, 0, 35);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }
}
