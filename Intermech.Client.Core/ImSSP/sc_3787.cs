
// Type: ImSSP.sc_3787
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3787
{
  internal static string ssp_imclient_3788()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[6] = (byte) 242;
      numArray2[8] = (byte) 218;
      numArray2[2] = (byte) 163;
      numArray2[3] = (byte) 106;
      numArray2[13] = (byte) 89;
      numArray2[10] = (byte) 30;
      numArray2[4] = (byte) 174;
      numArray2[7] = (byte) 17;
      numArray2[0] = (byte) 107;
      numArray2[9] = (byte) 197;
      numArray2[5] = (byte) 114;
      numArray2[11] = (byte) 56;
      numArray2[14] = (byte) 193;
      numArray2[12] = (byte) 1;
      numArray2[1] = (byte) 124;
      byte[] numArray3 = new byte[15];
      numArray3[3] = (byte) 138;
      numArray3[1] = (byte) 104;
      numArray3[2] = (byte) 26;
      numArray3[11] = (byte) 131;
      numArray3[8] = (byte) 211;
      numArray3[10] = (byte) 6;
      numArray3[4] = (byte) 106;
      numArray3[7] = (byte) 81;
      numArray3[9] = (byte) 64 /*0x40*/;
      numArray3[5] = (byte) 85;
      numArray3[14] = (byte) 131;
      numArray3[6] = (byte) 124;
      numArray3[12] = (byte) 209;
      numArray3[13] = (byte) 86;
      numArray3[0] = (byte) 46;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 247,
      (byte) 36,
      (byte) 132,
      (byte) 209,
      (byte) 23,
      (byte) 32 /*0x20*/,
      (byte) 212,
      (byte) 142,
      (byte) 163,
      (byte) 89,
      (byte) 176 /*0xB0*/,
      (byte) 54,
      (byte) 237,
      (byte) 96 /*0x60*/,
      (byte) 143
    };
    byte[] numArray6 = new byte[15];
    numArray6[8] = (byte) 135;
    numArray6[1] = (byte) 39;
    numArray6[4] = (byte) 78;
    numArray6[3] = (byte) 219;
    numArray6[7] = (byte) 232;
    numArray6[13] = (byte) 53;
    numArray6[10] = (byte) 32 /*0x20*/;
    numArray6[14] = (byte) 24;
    numArray6[5] = (byte) 116;
    numArray6[11] = (byte) 130;
    numArray6[0] = (byte) 116;
    numArray6[2] = (byte) 230;
    numArray6[12] = (byte) 245;
    numArray6[9] = (byte) 195;
    numArray6[6] = (byte) 59;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3789()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 155,
        (byte) 93,
        (byte) 127 /*0x7F*/,
        (byte) 247,
        (byte) 246,
        (byte) 242,
        (byte) 145,
        (byte) 103,
        (byte) 168,
        (byte) 79,
        (byte) 147,
        (byte) 252,
        (byte) 225,
        (byte) 68,
        (byte) 52
      };
      byte[] numArray3 = new byte[15];
      numArray3[14] = (byte) 1;
      numArray3[12] = (byte) 151;
      numArray3[5] = (byte) 214;
      numArray3[3] = (byte) 155;
      numArray3[4] = (byte) 96 /*0x60*/;
      numArray3[2] = (byte) 39;
      numArray3[6] = (byte) 180;
      numArray3[7] = (byte) 105;
      numArray3[8] = (byte) 25;
      numArray3[0] = (byte) 178;
      numArray3[10] = (byte) 161;
      numArray3[11] = (byte) 177;
      numArray3[1] = (byte) 181;
      numArray3[13] = (byte) 132;
      numArray3[9] = (byte) 152;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[4] = (byte) 5;
    numArray5[1] = (byte) 197;
    numArray5[2] = (byte) 49;
    numArray5[3] = (byte) 88;
    numArray5[5] = (byte) 24;
    numArray5[6] = (byte) 50;
    numArray5[7] = (byte) 9;
    numArray5[0] = (byte) 44;
    numArray5[12] = (byte) 101;
    numArray5[9] = (byte) 205;
    numArray5[10] = (byte) 213;
    numArray5[11] = (byte) 10;
    numArray5[13] = (byte) 53;
    numArray5[8] = (byte) 252;
    numArray5[14] = (byte) 35;
    byte[] numArray6 = new byte[15]
    {
      (byte) 247,
      (byte) 171,
      (byte) 190,
      (byte) 115,
      (byte) 38,
      (byte) 196,
      (byte) 186,
      (byte) 40,
      (byte) 131,
      (byte) 28,
      (byte) 31 /*0x1F*/,
      (byte) 124,
      (byte) 198,
      (byte) 169,
      (byte) 250
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3790()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 180,
        (byte) 127 /*0x7F*/,
        (byte) 2,
        (byte) 12,
        (byte) 99,
        (byte) 115,
        (byte) 67,
        (byte) 79,
        (byte) 101,
        (byte) 179,
        (byte) 41,
        (byte) 219,
        (byte) 9,
        (byte) 203,
        (byte) 148
      };
      byte[] numArray3 = new byte[15];
      numArray3[2] = (byte) 115;
      numArray3[0] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 224 /*0xE0*/;
      numArray3[10] = (byte) 20;
      numArray3[4] = (byte) 10;
      numArray3[5] = (byte) 114;
      numArray3[3] = (byte) 24;
      numArray3[7] = (byte) 205;
      numArray3[8] = (byte) 4;
      numArray3[9] = (byte) 183;
      numArray3[14] = (byte) 125;
      numArray3[1] = (byte) 213;
      numArray3[12] = (byte) 16 /*0x10*/;
      numArray3[13] = (byte) 71;
      numArray3[11] = (byte) 53;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[7] = (byte) 101;
    numArray5[1] = (byte) 129;
    numArray5[14] = (byte) 42;
    numArray5[3] = (byte) 123;
    numArray5[6] = (byte) 175;
    numArray5[2] = (byte) 163;
    numArray5[0] = (byte) 26;
    numArray5[10] = (byte) 103;
    numArray5[8] = (byte) 220;
    numArray5[9] = (byte) 250;
    numArray5[5] = (byte) 167;
    numArray5[11] = (byte) 52;
    numArray5[12] = (byte) 188;
    numArray5[4] = (byte) 46;
    numArray5[13] = (byte) 126;
    byte[] numArray6 = new byte[15];
    numArray6[10] = (byte) 104;
    numArray6[1] = (byte) 57;
    numArray6[2] = (byte) 71;
    numArray6[5] = (byte) 51;
    numArray6[0] = (byte) 164;
    numArray6[14] = (byte) 204;
    numArray6[12] = (byte) 99;
    numArray6[4] = (byte) 58;
    numArray6[8] = (byte) 61;
    numArray6[9] = (byte) 162;
    numArray6[11] = (byte) 63 /*0x3F*/;
    numArray6[6] = (byte) 91;
    numArray6[7] = (byte) 13;
    numArray6[3] = (byte) 0;
    numArray6[13] = (byte) 58;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
