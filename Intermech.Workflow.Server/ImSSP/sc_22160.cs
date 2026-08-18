// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22160
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22160
{
  private static byte[] sspq = new byte[60]
  {
    (byte) 90,
    (byte) 81,
    (byte) 216,
    (byte) 204,
    (byte) 35,
    (byte) 85,
    (byte) 181,
    (byte) 125,
    (byte) 34,
    (byte) 116,
    (byte) 66,
    (byte) 108,
    (byte) 65,
    (byte) 177,
    (byte) 183,
    (byte) 101,
    (byte) 222,
    (byte) 117,
    (byte) 207,
    (byte) 129,
    (byte) 245,
    (byte) 141,
    (byte) 112 /*0x70*/,
    (byte) 211,
    (byte) 186,
    (byte) 139,
    (byte) 109,
    (byte) 125,
    (byte) 198,
    (byte) 1,
    (byte) 82,
    (byte) 244,
    (byte) 99,
    (byte) 65,
    (byte) 72,
    (byte) 96 /*0x60*/,
    (byte) 7,
    (byte) 218,
    (byte) 232,
    (byte) 120,
    (byte) 240 /*0xF0*/,
    (byte) 40,
    (byte) 123,
    (byte) 145,
    (byte) 2,
    (byte) 198,
    (byte) 194,
    (byte) 1,
    (byte) 222,
    (byte) 149,
    (byte) 4,
    (byte) 56,
    (byte) 153,
    (byte) 7,
    (byte) 235,
    (byte) 154,
    (byte) 166,
    (byte) 109,
    (byte) 217,
    (byte) 136
  };
  private static byte[] sspr = new byte[60]
  {
    (byte) 184,
    (byte) 24,
    (byte) 178,
    (byte) 74,
    (byte) 78,
    (byte) 200,
    (byte) 237,
    (byte) 110,
    (byte) 6,
    (byte) 242,
    (byte) 147,
    (byte) 108,
    (byte) 37,
    (byte) 38,
    (byte) 23,
    (byte) 96 /*0x60*/,
    (byte) 223,
    (byte) 227,
    (byte) 169,
    (byte) 25,
    (byte) 85,
    (byte) 247,
    (byte) 97,
    (byte) 103,
    (byte) 46,
    (byte) 7,
    (byte) 138,
    (byte) 125,
    (byte) 98,
    (byte) 53,
    (byte) 123,
    (byte) 73,
    (byte) 10,
    (byte) 56,
    (byte) 1,
    (byte) 101,
    (byte) 173,
    (byte) 155,
    (byte) 41,
    (byte) 181,
    (byte) 252,
    (byte) 195,
    (byte) 168,
    (byte) 159,
    (byte) 182,
    (byte) 217,
    (byte) 236,
    (byte) 240 /*0xF0*/,
    (byte) 63 /*0x3F*/,
    (byte) 75,
    (byte) 47,
    (byte) 137,
    (byte) 86,
    (byte) 240 /*0xF0*/,
    (byte) 219,
    (byte) 252,
    (byte) 33,
    (byte) 200,
    (byte) 242,
    (byte) 251
  };

  internal static string ssp_workflow_server_22161()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 180,
        (byte) 29,
        (byte) 245,
        (byte) 218,
        (byte) 81,
        (byte) 201,
        (byte) 233,
        (byte) 58,
        (byte) 201,
        (byte) 152,
        (byte) 63 /*0x3F*/,
        (byte) 75,
        (byte) 149,
        (byte) 28,
        (byte) 98,
        (byte) 123,
        (byte) 185,
        (byte) 7
      };
      byte[] numArray3 = new byte[18];
      numArray3[15] = (byte) 238;
      numArray3[1] = (byte) 76;
      numArray3[2] = (byte) 62;
      numArray3[13] = (byte) 48 /*0x30*/;
      numArray3[7] = (byte) 143;
      numArray3[5] = (byte) 227;
      numArray3[9] = (byte) 215;
      numArray3[17] = (byte) 159;
      numArray3[6] = (byte) 68;
      numArray3[8] = (byte) 105;
      numArray3[10] = (byte) 120;
      numArray3[3] = (byte) 116;
      numArray3[12] = (byte) 212;
      numArray3[11] = (byte) 177;
      numArray3[4] = (byte) 213;
      numArray3[14] = (byte) 27;
      numArray3[16 /*0x10*/] = (byte) 29;
      numArray3[0] = (byte) 67;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[6] = (byte) 153;
    numArray5[1] = (byte) 76;
    numArray5[0] = (byte) 96 /*0x60*/;
    numArray5[11] = (byte) 111;
    numArray5[4] = (byte) 161;
    numArray5[5] = (byte) 150;
    numArray5[3] = (byte) 47;
    numArray5[7] = (byte) 137;
    numArray5[14] = (byte) 38;
    numArray5[17] = (byte) 79;
    numArray5[10] = (byte) 117;
    numArray5[8] = (byte) 71;
    numArray5[12] = (byte) 216;
    numArray5[9] = (byte) 215;
    numArray5[2] = (byte) 236;
    numArray5[15] = (byte) 54;
    numArray5[16 /*0x10*/] = (byte) 31 /*0x1F*/;
    numArray5[13] = (byte) 230;
    byte[] numArray6 = new byte[18]
    {
      (byte) 109,
      (byte) 79,
      (byte) 253,
      (byte) 55,
      (byte) 221,
      (byte) 254,
      (byte) 167,
      (byte) 1,
      (byte) 54,
      (byte) 90,
      (byte) 63 /*0x3F*/,
      (byte) 62,
      (byte) 90,
      (byte) 83,
      (byte) 251,
      (byte) 1,
      (byte) 166,
      (byte) 170
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22162()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 46,
        (byte) 73,
        (byte) 39,
        (byte) 87,
        (byte) 131,
        (byte) 83,
        (byte) 74,
        (byte) 204,
        (byte) 24,
        (byte) 84,
        (byte) 235,
        (byte) 227,
        (byte) 204,
        (byte) 61,
        (byte) 172,
        (byte) 6,
        (byte) 147,
        (byte) 171
      };
      byte[] numArray3 = new byte[18];
      numArray3[13] = (byte) 58;
      numArray3[1] = (byte) 124;
      numArray3[3] = (byte) 56;
      numArray3[8] = (byte) 60;
      numArray3[10] = (byte) 171;
      numArray3[5] = (byte) 61;
      numArray3[7] = (byte) 184;
      numArray3[15] = (byte) 12;
      numArray3[4] = (byte) 158;
      numArray3[9] = (byte) 200;
      numArray3[6] = (byte) 77;
      numArray3[11] = (byte) 4;
      numArray3[12] = (byte) 152;
      numArray3[17] = (byte) 128 /*0x80*/;
      numArray3[14] = (byte) 159;
      numArray3[0] = (byte) 71;
      numArray3[16 /*0x10*/] = (byte) 231;
      numArray3[2] = (byte) 224 /*0xE0*/;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[4] = (byte) 181;
    numArray5[1] = (byte) 43;
    numArray5[15] = (byte) 156;
    numArray5[5] = (byte) 39;
    numArray5[2] = (byte) 192 /*0xC0*/;
    numArray5[6] = (byte) 77;
    numArray5[9] = (byte) 48 /*0x30*/;
    numArray5[7] = (byte) 43;
    numArray5[8] = (byte) 45;
    numArray5[0] = (byte) 179;
    numArray5[10] = (byte) 94;
    numArray5[12] = (byte) 107;
    numArray5[3] = (byte) 243;
    numArray5[13] = (byte) 60;
    numArray5[14] = (byte) 0;
    numArray5[11] = (byte) 159;
    numArray5[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    numArray5[17] = (byte) 201;
    byte[] numArray6 = new byte[18]
    {
      (byte) 135,
      (byte) 238,
      (byte) 63 /*0x3F*/,
      (byte) 221,
      (byte) 92,
      (byte) 129,
      (byte) 0,
      (byte) 225,
      (byte) 51,
      (byte) 227,
      (byte) 176 /*0xB0*/,
      (byte) 149,
      (byte) 27,
      (byte) 219,
      (byte) 115,
      (byte) 204,
      (byte) 17,
      (byte) 123
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[31 /*0x1F*/];
    byte[] response = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_22160.sspq, 0, (Array) numArray7, 0, 31 /*0x1F*/);
    key.Query(true, 365, numArray7, response);
    Array.Copy((Array) sc_22160.sspr, 0, (Array) numArray7, 0, 31 /*0x1F*/);
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

  internal static string ssp_workflow_server_22163()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[15] = (byte) 127 /*0x7F*/;
      numArray2[1] = (byte) 227;
      numArray2[9] = (byte) 179;
      numArray2[17] = (byte) 16 /*0x10*/;
      numArray2[10] = (byte) 157;
      numArray2[12] = (byte) 22;
      numArray2[6] = (byte) 39;
      numArray2[2] = (byte) 85;
      numArray2[8] = (byte) 126;
      numArray2[11] = (byte) 70;
      numArray2[18] = (byte) 33;
      numArray2[4] = (byte) 118;
      numArray2[0] = byte.MaxValue;
      numArray2[13] = (byte) 9;
      numArray2[14] = (byte) 145;
      numArray2[7] = (byte) 206;
      numArray2[16 /*0x10*/] = (byte) 54;
      numArray2[5] = (byte) 129;
      numArray2[3] = (byte) 180;
      byte[] numArray3 = new byte[19];
      numArray3[2] = (byte) 31 /*0x1F*/;
      numArray3[13] = (byte) 0;
      numArray3[15] = (byte) 48 /*0x30*/;
      numArray3[16 /*0x10*/] = (byte) 204;
      numArray3[14] = (byte) 101;
      numArray3[1] = (byte) 139;
      numArray3[6] = (byte) 156;
      numArray3[3] = (byte) 29;
      numArray3[8] = (byte) 184;
      numArray3[17] = (byte) 244;
      numArray3[10] = (byte) 230;
      numArray3[11] = (byte) 174;
      numArray3[4] = (byte) 117;
      numArray3[9] = (byte) 89;
      numArray3[18] = (byte) 151;
      numArray3[5] = (byte) 225;
      numArray3[12] = (byte) 190;
      numArray3[0] = (byte) 166;
      numArray3[7] = (byte) 223;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 111,
      (byte) 143,
      (byte) 231,
      (byte) 71,
      (byte) 252,
      (byte) 124,
      (byte) 36,
      (byte) 218,
      (byte) 157,
      (byte) 51,
      (byte) 36,
      (byte) 51,
      (byte) 33,
      (byte) 196,
      (byte) 134,
      (byte) 201,
      (byte) 70,
      (byte) 214,
      (byte) 221
    };
    byte[] numArray6 = new byte[19];
    numArray6[4] = (byte) 118;
    numArray6[16 /*0x10*/] = (byte) 128 /*0x80*/;
    numArray6[0] = (byte) 232;
    numArray6[3] = (byte) 238;
    numArray6[11] = (byte) 124;
    numArray6[5] = (byte) 1;
    numArray6[6] = (byte) 219;
    numArray6[7] = (byte) 58;
    numArray6[10] = (byte) 199;
    numArray6[9] = (byte) 0;
    numArray6[2] = (byte) 252;
    numArray6[14] = (byte) 136;
    numArray6[12] = (byte) 27;
    numArray6[13] = (byte) 182;
    numArray6[8] = (byte) 86;
    numArray6[15] = (byte) 173;
    numArray6[17] = (byte) 223;
    numArray6[1] = (byte) 126;
    numArray6[18] = (byte) 83;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22164()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[13] = (byte) 13;
      numArray2[0] = (byte) 19;
      numArray2[15] = (byte) 237;
      numArray2[3] = (byte) 20;
      numArray2[12] = (byte) 40;
      numArray2[5] = (byte) 26;
      numArray2[6] = (byte) 246;
      numArray2[1] = (byte) 198;
      numArray2[7] = (byte) 113;
      numArray2[9] = (byte) 244;
      numArray2[2] = (byte) 201;
      numArray2[8] = (byte) 112 /*0x70*/;
      numArray2[16 /*0x10*/] = (byte) 87;
      numArray2[10] = (byte) 171;
      numArray2[14] = (byte) 241;
      numArray2[11] = (byte) 27;
      numArray2[4] = (byte) 148;
      numArray2[17] = (byte) 251;
      byte[] numArray3 = new byte[18]
      {
        (byte) 24,
        (byte) 31 /*0x1F*/,
        (byte) 132,
        (byte) 231,
        (byte) 103,
        (byte) 42,
        (byte) 135,
        (byte) 99,
        (byte) 40,
        (byte) 205,
        (byte) 97,
        (byte) 120,
        (byte) 21,
        (byte) 38,
        (byte) 66,
        (byte) 84,
        (byte) 187,
        (byte) 142
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 38,
      (byte) 174,
      (byte) 143,
      (byte) 69,
      (byte) 159,
      (byte) 39,
      (byte) 19,
      (byte) 85,
      (byte) 224 /*0xE0*/,
      (byte) 206,
      (byte) 44,
      (byte) 99,
      (byte) 73,
      (byte) 56,
      (byte) 134,
      (byte) 8,
      (byte) 43,
      (byte) 38
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 177,
      (byte) 103,
      (byte) 79,
      (byte) 235,
      (byte) 166,
      (byte) 234,
      (byte) 55,
      (byte) 24,
      (byte) 67,
      (byte) 128 /*0x80*/,
      (byte) 19,
      (byte) 17,
      (byte) 66,
      (byte) 211,
      (byte) 164,
      (byte) 184,
      (byte) 158,
      (byte) 22
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_workflow_server_22165(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 227,
      (byte) 99,
      (byte) 65,
      (byte) 105,
      (byte) 193,
      (byte) 190,
      (byte) 25,
      (byte) 188,
      (byte) 123,
      (byte) 142,
      (byte) 19,
      (byte) 208 /*0xD0*/,
      (byte) 74,
      (byte) 162,
      (byte) 0,
      (byte) 42,
      (byte) 239,
      (byte) 78,
      (byte) 246,
      (byte) 249,
      (byte) 132,
      (byte) 40,
      (byte) 70,
      (byte) 49,
      (byte) 43,
      (byte) 94,
      (byte) 35,
      (byte) 176 /*0xB0*/,
      (byte) 185,
      (byte) 203,
      (byte) 124,
      (byte) 227,
      (byte) 127 /*0x7F*/,
      (byte) 205,
      (byte) 97,
      (byte) 59,
      (byte) 230,
      (byte) 170,
      (byte) 148,
      (byte) 33,
      (byte) 180,
      (byte) 111,
      (byte) 232,
      (byte) 186,
      (byte) 36,
      (byte) 181,
      (byte) 190
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 227;
    sourceArray2[38] = (byte) 142;
    sourceArray2[2] = (byte) 65;
    sourceArray2[17] = (byte) 76;
    sourceArray2[4] = (byte) 152;
    sourceArray2[5] = (byte) 30;
    sourceArray2[12] = (byte) 39;
    sourceArray2[7] = (byte) 62;
    sourceArray2[8] = (byte) 185;
    sourceArray2[31 /*0x1F*/] = (byte) 133;
    sourceArray2[25] = (byte) 92;
    sourceArray2[35] = (byte) 166;
    sourceArray2[47] = (byte) 124;
    sourceArray2[13] = (byte) 219;
    sourceArray2[30] = (byte) 146;
    sourceArray2[15] = (byte) 79;
    sourceArray2[40] = (byte) 39;
    sourceArray2[0] = (byte) 220;
    sourceArray2[27] = (byte) 55;
    sourceArray2[19] = (byte) 158;
    sourceArray2[28] = (byte) 115;
    sourceArray2[18] = (byte) 110;
    sourceArray2[9] = (byte) 228;
    sourceArray2[23] = (byte) 88;
    sourceArray2[6] = (byte) 193;
    sourceArray2[1] = (byte) 68;
    sourceArray2[10] = (byte) 27;
    sourceArray2[22] = (byte) 39;
    sourceArray2[11] = (byte) 10;
    sourceArray2[14] = (byte) 151;
    sourceArray2[43] = (byte) 186;
    sourceArray2[20] = (byte) 158;
    sourceArray2[32 /*0x20*/] = (byte) 24;
    sourceArray2[24] = (byte) 183;
    sourceArray2[3] = (byte) 232;
    sourceArray2[33] = (byte) 47;
    sourceArray2[36] = (byte) 90;
    sourceArray2[37] = (byte) 81;
    sourceArray2[26] = (byte) 224 /*0xE0*/;
    sourceArray2[39] = (byte) 165;
    sourceArray2[34] = (byte) 224 /*0xE0*/;
    sourceArray2[41] = (byte) 253;
    sourceArray2[42] = (byte) 180;
    sourceArray2[45] = (byte) 97;
    sourceArray2[44] = byte.MaxValue;
    sourceArray2[16 /*0x10*/] = (byte) 139;
    sourceArray2[46] = (byte) 210;
    sourceArray2[21] = (byte) 82;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 365, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[29];
    byte[] response2 = new byte[29];
    Array.Copy((Array) sc_22160.sspq, 31 /*0x1F*/, (Array) numArray2, 0, 29);
    key.Query(true, 365, numArray2, response2);
    Array.Copy((Array) sc_22160.sspr, 31 /*0x1F*/, (Array) numArray2, 0, 29);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static string ssp_workflow_server_22166()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[1] = (byte) 167;
      numArray2[9] = (byte) 101;
      numArray2[2] = (byte) 7;
      numArray2[11] = (byte) 242;
      numArray2[6] = (byte) 94;
      numArray2[5] = (byte) 19;
      numArray2[14] = (byte) 104;
      numArray2[7] = (byte) 80 /*0x50*/;
      numArray2[8] = (byte) 42;
      numArray2[3] = (byte) 227;
      numArray2[10] = (byte) 208 /*0xD0*/;
      numArray2[15] = (byte) 42;
      numArray2[12] = (byte) 30;
      numArray2[13] = (byte) 89;
      numArray2[4] = (byte) 24;
      numArray2[0] = (byte) 249;
      numArray2[16 /*0x10*/] = (byte) 141;
      numArray2[17] = (byte) 212;
      byte[] numArray3 = new byte[18];
      numArray3[16 /*0x10*/] = (byte) 5;
      numArray3[6] = (byte) 172;
      numArray3[2] = (byte) 51;
      numArray3[3] = (byte) 148;
      numArray3[13] = (byte) 83;
      numArray3[8] = (byte) 69;
      numArray3[15] = (byte) 253;
      numArray3[1] = (byte) 202;
      numArray3[14] = (byte) 117;
      numArray3[10] = (byte) 190;
      numArray3[0] = (byte) 48 /*0x30*/;
      numArray3[11] = (byte) 147;
      numArray3[12] = (byte) 9;
      numArray3[9] = byte.MaxValue;
      numArray3[7] = (byte) 102;
      numArray3[4] = (byte) 59;
      numArray3[5] = (byte) 181;
      numArray3[17] = (byte) 60;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 210,
      (byte) 115,
      (byte) 190,
      (byte) 183,
      (byte) 9,
      (byte) 162,
      (byte) 112 /*0x70*/,
      (byte) 203,
      (byte) 206,
      (byte) 146,
      (byte) 243,
      (byte) 99,
      (byte) 231,
      (byte) 182,
      (byte) 223,
      (byte) 235,
      (byte) 242,
      (byte) 11
    };
    byte[] numArray6 = new byte[18];
    numArray6[0] = (byte) 231;
    numArray6[1] = (byte) 39;
    numArray6[2] = (byte) 56;
    numArray6[5] = (byte) 162;
    numArray6[6] = (byte) 58;
    numArray6[3] = (byte) 78;
    numArray6[14] = (byte) 54;
    numArray6[12] = (byte) 201;
    numArray6[8] = (byte) 209;
    numArray6[7] = (byte) 244;
    numArray6[9] = (byte) 66;
    numArray6[16 /*0x10*/] = (byte) 107;
    numArray6[11] = (byte) 39;
    numArray6[13] = (byte) 24;
    numArray6[10] = (byte) 169;
    numArray6[15] = (byte) 44;
    numArray6[17] = (byte) 93;
    numArray6[4] = (byte) 63 /*0x3F*/;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22167()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[0] = (byte) 67;
      numArray2[1] = (byte) 101;
      numArray2[2] = (byte) 101;
      numArray2[7] = (byte) 184;
      numArray2[4] = (byte) 135;
      numArray2[6] = (byte) 155;
      numArray2[5] = (byte) 73;
      numArray2[11] = (byte) 14;
      numArray2[9] = (byte) 173;
      numArray2[8] = (byte) 57;
      numArray2[10] = (byte) 64 /*0x40*/;
      numArray2[3] = (byte) 179;
      byte[] numArray3 = new byte[12];
      numArray3[9] = (byte) 167;
      numArray3[1] = (byte) 55;
      numArray3[7] = (byte) 103;
      numArray3[4] = (byte) 164;
      numArray3[0] = (byte) 107;
      numArray3[6] = (byte) 146;
      numArray3[11] = (byte) 172;
      numArray3[5] = (byte) 115;
      numArray3[8] = (byte) 161;
      numArray3[2] = (byte) 38;
      numArray3[10] = (byte) 108;
      numArray3[3] = (byte) 112 /*0x70*/;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 100,
      (byte) 54,
      (byte) 134,
      (byte) 101,
      (byte) 84,
      (byte) 141,
      (byte) 55,
      (byte) 108,
      (byte) 149,
      (byte) 132,
      (byte) 207,
      (byte) 240 /*0xF0*/
    };
    byte[] numArray6 = new byte[12]
    {
      (byte) 227,
      (byte) 195,
      (byte) 22,
      (byte) 204,
      (byte) 88,
      (byte) 110,
      (byte) 60,
      (byte) 63 /*0x3F*/,
      (byte) 232,
      (byte) 72,
      (byte) 143,
      (byte) 204
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
