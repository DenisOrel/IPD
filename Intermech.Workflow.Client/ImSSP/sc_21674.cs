// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21674
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21674
{
  private static byte[] sspq = new byte[40]
  {
    byte.MaxValue,
    (byte) 10,
    (byte) 101,
    (byte) 236,
    (byte) 65,
    (byte) 138,
    (byte) 137,
    (byte) 10,
    (byte) 110,
    (byte) 137,
    (byte) 9,
    (byte) 95,
    (byte) 89,
    (byte) 103,
    (byte) 16 /*0x10*/,
    (byte) 116,
    (byte) 8,
    (byte) 235,
    (byte) 111,
    (byte) 84,
    (byte) 62,
    (byte) 25,
    (byte) 53,
    (byte) 73,
    (byte) 228,
    (byte) 59,
    (byte) 226,
    (byte) 217,
    (byte) 157,
    (byte) 178,
    (byte) 169,
    (byte) 139,
    (byte) 58,
    (byte) 182,
    (byte) 234,
    (byte) 72,
    (byte) 23,
    (byte) 194,
    (byte) 48 /*0x30*/,
    (byte) 209
  };
  private static byte[] sspr = new byte[40]
  {
    (byte) 194,
    (byte) 127 /*0x7F*/,
    (byte) 68,
    (byte) 229,
    (byte) 155,
    (byte) 83,
    (byte) 89,
    (byte) 246,
    (byte) 249,
    (byte) 166,
    (byte) 99,
    (byte) 180,
    (byte) 160 /*0xA0*/,
    (byte) 54,
    (byte) 28,
    (byte) 132,
    (byte) 110,
    (byte) 198,
    (byte) 227,
    (byte) 2,
    (byte) 67,
    (byte) 161,
    (byte) 14,
    (byte) 177,
    (byte) 71,
    (byte) 153,
    (byte) 246,
    (byte) 76,
    (byte) 202,
    (byte) 36,
    (byte) 178,
    (byte) 131,
    (byte) 99,
    (byte) 81,
    (byte) 210,
    (byte) 66,
    (byte) 83,
    (byte) 198,
    (byte) 227,
    (byte) 160 /*0xA0*/
  };

  internal static string ssp_workflow_21675()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 198,
        (byte) 222,
        (byte) 129,
        (byte) 146,
        (byte) 70,
        (byte) 182,
        (byte) 99,
        (byte) 2,
        (byte) 245,
        (byte) 90
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 114,
        (byte) 108,
        (byte) 87,
        (byte) 6,
        (byte) 209,
        (byte) 7,
        (byte) 94,
        (byte) 33,
        (byte) 5,
        (byte) 226
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 72,
      (byte) 48 /*0x30*/,
      (byte) 82,
      (byte) 145,
      (byte) 103,
      (byte) 0,
      (byte) 225,
      (byte) 25,
      (byte) 73,
      (byte) 46
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 59,
      (byte) 186,
      (byte) 159,
      (byte) 79,
      (byte) 81,
      (byte) 188,
      (byte) 201,
      (byte) 107,
      (byte) 159,
      (byte) 11
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_21676()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 154,
        (byte) 40,
        (byte) 0,
        (byte) 253,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 241
      };
      numArray2[4] = (byte) 26;
      numArray2[2] = (byte) 91;
      numArray2[6] = (byte) 99;
      numArray2[5] = (byte) 81;
      byte[] numArray3 = new byte[8];
      numArray3[3] = (byte) 8;
      numArray3[1] = (byte) 110;
      numArray3[6] = (byte) 16 /*0x10*/;
      numArray3[7] = (byte) 13;
      numArray3[2] = (byte) 198;
      numArray3[5] = (byte) 96 /*0x60*/;
      numArray3[4] = (byte) 23;
      numArray3[0] = (byte) 25;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[8];
    byte[] numArray5 = new byte[8]
    {
      (byte) 82,
      (byte) 125,
      (byte) 175,
      (byte) 113,
      (byte) 91,
      (byte) 176 /*0xB0*/,
      (byte) 183,
      (byte) 200
    };
    byte[] numArray6 = new byte[8];
    numArray6[6] = (byte) 94;
    numArray6[1] = (byte) 36;
    numArray6[2] = (byte) 120;
    numArray6[3] = (byte) 17;
    numArray6[7] = (byte) 120;
    numArray6[5] = (byte) 20;
    numArray6[0] = (byte) 61;
    numArray6[4] = (byte) 1;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[40];
    byte[] response = new byte[40];
    Array.Copy((Array) sc_21674.sspq, 0, (Array) numArray7, 0, 40);
    key.Query(true, 366, numArray7, response);
    Array.Copy((Array) sc_21674.sspr, 0, (Array) numArray7, 0, 40);
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
