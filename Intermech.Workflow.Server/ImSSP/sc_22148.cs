// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22148
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22148
{
  private static byte[] sspq = new byte[71]
  {
    (byte) 176 /*0xB0*/,
    (byte) 105,
    (byte) 247,
    (byte) 162,
    (byte) 127 /*0x7F*/,
    (byte) 173,
    (byte) 246,
    (byte) 47,
    (byte) 234,
    (byte) 84,
    (byte) 152,
    (byte) 36,
    (byte) 184,
    (byte) 220,
    (byte) 118,
    (byte) 33,
    (byte) 172,
    (byte) 230,
    (byte) 30,
    (byte) 25,
    (byte) 54,
    (byte) 144 /*0x90*/,
    (byte) 181,
    (byte) 120,
    (byte) 212,
    (byte) 250,
    (byte) 205,
    (byte) 132,
    (byte) 41,
    (byte) 152,
    (byte) 164,
    (byte) 58,
    (byte) 235,
    (byte) 147,
    (byte) 95,
    (byte) 222,
    (byte) 2,
    (byte) 58,
    (byte) 15,
    (byte) 244,
    (byte) 173,
    (byte) 89,
    (byte) 217,
    (byte) 217,
    (byte) 15,
    (byte) 33,
    (byte) 214,
    (byte) 170,
    (byte) 92,
    (byte) 22,
    (byte) 177,
    (byte) 160 /*0xA0*/,
    (byte) 171,
    (byte) 80 /*0x50*/,
    (byte) 151,
    (byte) 219,
    (byte) 131,
    (byte) 6,
    (byte) 146,
    (byte) 73,
    (byte) 176 /*0xB0*/,
    (byte) 177,
    (byte) 171,
    (byte) 247,
    (byte) 175,
    (byte) 5,
    (byte) 150,
    (byte) 109,
    (byte) 133,
    (byte) 224 /*0xE0*/,
    (byte) 42
  };
  private static byte[] sspr = new byte[71]
  {
    (byte) 173,
    (byte) 110,
    (byte) 129,
    (byte) 127 /*0x7F*/,
    (byte) 1,
    (byte) 152,
    (byte) 250,
    (byte) 48 /*0x30*/,
    (byte) 0,
    (byte) 171,
    (byte) 96 /*0x60*/,
    (byte) 55,
    (byte) 174,
    (byte) 152,
    (byte) 31 /*0x1F*/,
    (byte) 129,
    (byte) 179,
    (byte) 18,
    (byte) 106,
    (byte) 205,
    (byte) 33,
    (byte) 194,
    (byte) 43,
    (byte) 142,
    (byte) 245,
    (byte) 146,
    (byte) 250,
    (byte) 63 /*0x3F*/,
    (byte) 112 /*0x70*/,
    (byte) 34,
    (byte) 56,
    (byte) 226,
    (byte) 87,
    (byte) 192 /*0xC0*/,
    (byte) 44,
    (byte) 76,
    (byte) 68,
    (byte) 201,
    (byte) 234,
    (byte) 117,
    (byte) 6,
    (byte) 126,
    (byte) 23,
    (byte) 48 /*0x30*/,
    (byte) 95,
    (byte) 197,
    (byte) 63 /*0x3F*/,
    (byte) 95,
    (byte) 127 /*0x7F*/,
    (byte) 177,
    (byte) 108,
    (byte) 160 /*0xA0*/,
    (byte) 204,
    (byte) 220,
    (byte) 150,
    (byte) 45,
    (byte) 96 /*0x60*/,
    (byte) 204,
    (byte) 182,
    (byte) 126,
    (byte) 166,
    (byte) 206,
    (byte) 202,
    (byte) 36,
    (byte) 8,
    (byte) 25,
    (byte) 57,
    (byte) 169,
    (byte) 126,
    (byte) 150,
    (byte) 51
  };

  internal static string ssp_workflow_server_22149()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 35,
        (byte) 209,
        (byte) 58
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 141,
        (byte) 251,
        (byte) 136
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_22148.sspq, 0, (Array) numArray4, 0, 26);
      key.Query(true, 365, numArray4, response);
      Array.Copy((Array) sc_22148.sspr, 0, (Array) numArray4, 0, 26);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[3];
    byte[] numArray6 = new byte[3]
    {
      (byte) 132,
      (byte) 94,
      (byte) 139
    };
    byte[] numArray7 = new byte[3]
    {
      (byte) 0,
      (byte) 12,
      (byte) 0
    };
    numArray7[0] = (byte) 136;
    numArray7[2] = (byte) 58;
    key.Query(true, 365, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_workflow_server_22150()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 18,
        (byte) 183,
        (byte) 57,
        (byte) 196,
        (byte) 63 /*0x3F*/,
        (byte) 92,
        (byte) 128 /*0x80*/,
        (byte) 148,
        (byte) 161,
        (byte) 42,
        (byte) 158,
        (byte) 109,
        (byte) 173,
        (byte) 176 /*0xB0*/,
        (byte) 142,
        (byte) 196,
        byte.MaxValue,
        (byte) 39
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 133,
        (byte) 141,
        (byte) 135,
        (byte) 58,
        (byte) 205,
        (byte) 78,
        (byte) 183,
        (byte) 176 /*0xB0*/,
        (byte) 63 /*0x3F*/,
        (byte) 105,
        (byte) 183,
        (byte) 39,
        (byte) 86,
        (byte) 188,
        (byte) 146,
        (byte) 42,
        (byte) 220,
        (byte) 94
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_22148.sspq, 26, (Array) numArray4, 0, 25);
      key.Query(true, 365, numArray4, response);
      Array.Copy((Array) sc_22148.sspr, 26, (Array) numArray4, 0, 25);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[18];
    byte[] numArray6 = new byte[18]
    {
      (byte) 219,
      (byte) 34,
      (byte) 165,
      (byte) 5,
      (byte) 157,
      (byte) 182,
      (byte) 185,
      (byte) 136,
      (byte) 146,
      (byte) 233,
      (byte) 180,
      (byte) 155,
      (byte) 167,
      (byte) 184,
      (byte) 70,
      (byte) 169,
      (byte) 13,
      (byte) 202
    };
    byte[] numArray7 = new byte[18]
    {
      (byte) 111,
      (byte) 41,
      (byte) 131,
      (byte) 16 /*0x10*/,
      byte.MaxValue,
      (byte) 157,
      (byte) 29,
      (byte) 84,
      (byte) 184,
      (byte) 233,
      (byte) 127 /*0x7F*/,
      (byte) 96 /*0x60*/,
      (byte) 31 /*0x1F*/,
      (byte) 13,
      (byte) 89,
      (byte) 113,
      (byte) 98,
      (byte) 231
    };
    key.Query(true, 365, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_workflow_server_22151()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 153,
        (byte) 22,
        (byte) 165,
        (byte) 77,
        (byte) 73,
        (byte) 17,
        (byte) 146,
        (byte) 168,
        (byte) 35,
        (byte) 40,
        (byte) 18,
        (byte) 230,
        (byte) 201,
        (byte) 23,
        (byte) 69,
        (byte) 142,
        (byte) 193,
        (byte) 40
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 42,
        (byte) 76,
        (byte) 163,
        (byte) 73,
        (byte) 110,
        (byte) 29,
        (byte) 165,
        (byte) 60,
        (byte) 83,
        (byte) 133,
        (byte) 76,
        (byte) 196,
        (byte) 121,
        (byte) 201,
        (byte) 206,
        (byte) 180,
        (byte) 57,
        (byte) 46
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[0] = (byte) 11;
    numArray5[14] = (byte) 142;
    numArray5[4] = (byte) 33;
    numArray5[6] = (byte) 120;
    numArray5[7] = (byte) 200;
    numArray5[2] = (byte) 190;
    numArray5[1] = (byte) 249;
    numArray5[3] = (byte) 49;
    numArray5[5] = (byte) 200;
    numArray5[9] = (byte) 157;
    numArray5[10] = (byte) 170;
    numArray5[11] = (byte) 62;
    numArray5[12] = (byte) 58;
    numArray5[13] = (byte) 137;
    numArray5[17] = (byte) 108;
    numArray5[15] = (byte) 95;
    numArray5[16 /*0x10*/] = (byte) 79;
    numArray5[8] = (byte) 223;
    byte[] numArray6 = new byte[18];
    numArray6[10] = (byte) 135;
    numArray6[1] = (byte) 171;
    numArray6[2] = (byte) 240 /*0xF0*/;
    numArray6[5] = (byte) 41;
    numArray6[8] = (byte) 228;
    numArray6[12] = (byte) 18;
    numArray6[6] = (byte) 6;
    numArray6[4] = (byte) 11;
    numArray6[17] = (byte) 137;
    numArray6[9] = (byte) 20;
    numArray6[3] = (byte) 133;
    numArray6[11] = (byte) 207;
    numArray6[14] = (byte) 57;
    numArray6[7] = (byte) 204;
    numArray6[15] = (byte) 58;
    numArray6[13] = (byte) 34;
    numArray6[16 /*0x10*/] = (byte) 104;
    numArray6[0] = (byte) 235;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22152()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 112 /*0x70*/,
        (byte) 49,
        (byte) 153,
        (byte) 21,
        (byte) 73,
        (byte) 214,
        (byte) 5,
        (byte) 135,
        (byte) 125,
        (byte) 21,
        (byte) 250,
        (byte) 113,
        (byte) 120,
        (byte) 52,
        (byte) 67,
        (byte) 95,
        (byte) 219,
        (byte) 10,
        (byte) 130
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 176 /*0xB0*/,
        (byte) 90,
        (byte) 223,
        (byte) 234,
        (byte) 71,
        (byte) 70,
        (byte) 246,
        (byte) 80 /*0x50*/,
        (byte) 177,
        (byte) 253,
        (byte) 173,
        (byte) 195,
        (byte) 148,
        (byte) 207,
        (byte) 145,
        (byte) 91,
        (byte) 235,
        (byte) 83,
        (byte) 127 /*0x7F*/
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_22148.sspq, 51, (Array) numArray4, 0, 20);
      key.Query(true, 365, numArray4, response);
      Array.Copy((Array) sc_22148.sspr, 51, (Array) numArray4, 0, 20);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19];
    numArray6[9] = (byte) 194;
    numArray6[17] = (byte) 62;
    numArray6[2] = (byte) 70;
    numArray6[3] = (byte) 182;
    numArray6[4] = (byte) 61;
    numArray6[5] = (byte) 119;
    numArray6[0] = (byte) 121;
    numArray6[18] = (byte) 42;
    numArray6[8] = (byte) 136;
    numArray6[14] = (byte) 248;
    numArray6[10] = (byte) 108;
    numArray6[13] = (byte) 209;
    numArray6[12] = (byte) 87;
    numArray6[6] = (byte) 63 /*0x3F*/;
    numArray6[1] = (byte) 148;
    numArray6[15] = (byte) 105;
    numArray6[16 /*0x10*/] = (byte) 248;
    numArray6[11] = (byte) 20;
    numArray6[7] = (byte) 223;
    byte[] numArray7 = new byte[19]
    {
      (byte) 183,
      (byte) 14,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 132,
      (byte) 246,
      (byte) 153,
      (byte) 16 /*0x10*/,
      (byte) 5,
      (byte) 197,
      (byte) 102,
      (byte) 131,
      (byte) 27,
      (byte) 232,
      (byte) 74,
      (byte) 155,
      (byte) 86,
      (byte) 119,
      (byte) 121
    };
    key.Query(true, 365, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
