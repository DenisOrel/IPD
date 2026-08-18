// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22142
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22142
{
  internal static string ssp_workflow_server_22143()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 54,
        (byte) 58,
        (byte) 180,
        (byte) 177,
        (byte) 220,
        (byte) 4,
        (byte) 179,
        (byte) 39,
        (byte) 95,
        (byte) 109,
        (byte) 37,
        (byte) 104
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 82,
        (byte) 217,
        (byte) 233,
        (byte) 216,
        (byte) 169,
        (byte) 32 /*0x20*/,
        (byte) 216,
        (byte) 249,
        (byte) 231,
        (byte) 114,
        (byte) 98,
        (byte) 7
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 188,
      (byte) 217,
      (byte) 183,
      (byte) 46,
      (byte) 19,
      (byte) 85,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 40,
      (byte) 0,
      (byte) 0
    };
    numArray5[7] = (byte) 235;
    numArray5[10] = (byte) 62;
    numArray5[6] = (byte) 193;
    numArray5[8] = (byte) 45;
    numArray5[11] = (byte) 119;
    byte[] numArray6 = new byte[12]
    {
      (byte) 161,
      (byte) 49,
      (byte) 225,
      (byte) 155,
      (byte) 117,
      (byte) 209,
      (byte) 230,
      (byte) 95,
      (byte) 108,
      (byte) 222,
      (byte) 111,
      (byte) 187
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22144()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[12] = (byte) 116;
      numArray2[1] = (byte) 155;
      numArray2[2] = (byte) 181;
      numArray2[13] = (byte) 78;
      numArray2[10] = (byte) 87;
      numArray2[5] = (byte) 62;
      numArray2[0] = (byte) 36;
      numArray2[7] = (byte) 232;
      numArray2[3] = (byte) 220;
      numArray2[4] = (byte) 38;
      numArray2[8] = (byte) 167;
      numArray2[6] = (byte) 119;
      numArray2[11] = (byte) 173;
      numArray2[9] = (byte) 250;
      numArray2[14] = (byte) 134;
      numArray2[15] = (byte) 136;
      numArray2[16 /*0x10*/] = (byte) 158;
      numArray2[17] = (byte) 141;
      byte[] numArray3 = new byte[18];
      numArray3[9] = (byte) 182;
      numArray3[1] = (byte) 168;
      numArray3[17] = (byte) 22;
      numArray3[3] = (byte) 35;
      numArray3[13] = (byte) 70;
      numArray3[14] = (byte) 203;
      numArray3[6] = (byte) 186;
      numArray3[7] = (byte) 72;
      numArray3[8] = (byte) 35;
      numArray3[2] = (byte) 253;
      numArray3[10] = (byte) 218;
      numArray3[5] = (byte) 85;
      numArray3[4] = (byte) 128 /*0x80*/;
      numArray3[11] = (byte) 3;
      numArray3[12] = (byte) 51;
      numArray3[15] = (byte) 115;
      numArray3[16 /*0x10*/] = (byte) 130;
      numArray3[0] = (byte) 164;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 29,
      (byte) 242,
      (byte) 209,
      (byte) 51,
      (byte) 27,
      (byte) 245,
      (byte) 72,
      (byte) 213,
      (byte) 135,
      (byte) 79,
      (byte) 98,
      (byte) 134,
      (byte) 244,
      (byte) 20,
      (byte) 60,
      (byte) 59,
      (byte) 202,
      (byte) 41
    };
    byte[] numArray6 = new byte[18];
    numArray6[7] = (byte) 251;
    numArray6[8] = (byte) 70;
    numArray6[2] = (byte) 91;
    numArray6[9] = (byte) 67;
    numArray6[4] = (byte) 116;
    numArray6[5] = (byte) 49;
    numArray6[6] = (byte) 86;
    numArray6[12] = (byte) 170;
    numArray6[0] = (byte) 210;
    numArray6[3] = (byte) 46;
    numArray6[10] = (byte) 223;
    numArray6[11] = (byte) 62;
    numArray6[17] = (byte) 202;
    numArray6[13] = (byte) 116;
    numArray6[14] = (byte) 0;
    numArray6[15] = (byte) 126;
    numArray6[16 /*0x10*/] = (byte) 87;
    numArray6[1] = (byte) 250;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_workflow_server_22145(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 35,
      (byte) 242,
      (byte) 83,
      (byte) 98,
      (byte) 252,
      (byte) 229,
      (byte) 73,
      (byte) 126,
      (byte) 63 /*0x3F*/,
      (byte) 217,
      (byte) 224 /*0xE0*/,
      (byte) 117,
      (byte) 77,
      (byte) 49,
      (byte) 0,
      (byte) 170,
      (byte) 65,
      (byte) 172,
      (byte) 154,
      (byte) 95,
      (byte) 165,
      (byte) 43,
      (byte) 90,
      (byte) 250,
      (byte) 196,
      (byte) 89,
      (byte) 203,
      (byte) 190,
      (byte) 205,
      (byte) 188,
      (byte) 40,
      (byte) 129,
      (byte) 184,
      (byte) 88,
      (byte) 20,
      (byte) 42,
      (byte) 243,
      (byte) 45,
      (byte) 138,
      (byte) 155,
      (byte) 75,
      (byte) 37,
      (byte) 105,
      (byte) 83,
      (byte) 115,
      (byte) 40,
      (byte) 223,
      (byte) 247
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[40] = (byte) 147;
    sourceArray2[1] = (byte) 161;
    sourceArray2[31 /*0x1F*/] = (byte) 67;
    sourceArray2[16 /*0x10*/] = (byte) 82;
    sourceArray2[4] = (byte) 126;
    sourceArray2[42] = (byte) 114;
    sourceArray2[10] = (byte) 75;
    sourceArray2[44] = (byte) 30;
    sourceArray2[46] = (byte) 90;
    sourceArray2[9] = (byte) 4;
    sourceArray2[24] = (byte) 108;
    sourceArray2[6] = (byte) 13;
    sourceArray2[3] = (byte) 166;
    sourceArray2[17] = (byte) 68;
    sourceArray2[41] = (byte) 110;
    sourceArray2[15] = (byte) 55;
    sourceArray2[32 /*0x20*/] = (byte) 117;
    sourceArray2[20] = (byte) 208 /*0xD0*/;
    sourceArray2[18] = (byte) 233;
    sourceArray2[43] = (byte) 213;
    sourceArray2[14] = (byte) 164;
    sourceArray2[21] = (byte) 236;
    sourceArray2[34] = (byte) 11;
    sourceArray2[23] = (byte) 142;
    sourceArray2[47] = (byte) 26;
    sourceArray2[5] = (byte) 157;
    sourceArray2[19] = (byte) 88;
    sourceArray2[27] = (byte) 169;
    sourceArray2[8] = (byte) 179;
    sourceArray2[0] = (byte) 39;
    sourceArray2[30] = (byte) 11;
    sourceArray2[11] = (byte) 158;
    sourceArray2[12] = (byte) 190;
    sourceArray2[33] = (byte) 47;
    sourceArray2[2] = (byte) 9;
    sourceArray2[35] = (byte) 236;
    sourceArray2[36] = (byte) 168;
    sourceArray2[25] = (byte) 65;
    sourceArray2[38] = (byte) 194;
    sourceArray2[39] = (byte) 184;
    sourceArray2[37] = (byte) 230;
    sourceArray2[22] = (byte) 76;
    sourceArray2[7] = (byte) 164;
    sourceArray2[26] = (byte) 47;
    sourceArray2[13] = (byte) 122;
    sourceArray2[45] = (byte) 165;
    sourceArray2[28] = (byte) 44;
    sourceArray2[29] = (byte) 220;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_server_22146()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 107,
        (byte) 234,
        (byte) 219,
        (byte) 127 /*0x7F*/,
        (byte) 13,
        (byte) 43,
        (byte) 211,
        (byte) 15,
        (byte) 211,
        (byte) 206,
        (byte) 54,
        (byte) 144 /*0x90*/,
        (byte) 209,
        (byte) 2,
        (byte) 39,
        (byte) 70,
        (byte) 33,
        (byte) 168
      };
      byte[] numArray3 = new byte[18];
      numArray3[8] = (byte) 246;
      numArray3[5] = (byte) 64 /*0x40*/;
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[15] = (byte) 197;
      numArray3[4] = (byte) 147;
      numArray3[3] = (byte) 227;
      numArray3[6] = (byte) 181;
      numArray3[13] = (byte) 138;
      numArray3[14] = (byte) 164;
      numArray3[9] = (byte) 29;
      numArray3[10] = (byte) 188;
      numArray3[2] = (byte) 200;
      numArray3[12] = (byte) 83;
      numArray3[1] = (byte) 230;
      numArray3[11] = (byte) 119;
      numArray3[7] = (byte) 130;
      numArray3[17] = (byte) 19;
      numArray3[16 /*0x10*/] = (byte) 95;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[12] = (byte) 203;
    numArray5[10] = (byte) 58;
    numArray5[2] = (byte) 99;
    numArray5[8] = (byte) 141;
    numArray5[4] = (byte) 59;
    numArray5[14] = (byte) 36;
    numArray5[1] = (byte) 95;
    numArray5[7] = (byte) 200;
    numArray5[3] = (byte) 253;
    numArray5[9] = (byte) 192 /*0xC0*/;
    numArray5[5] = (byte) 230;
    numArray5[11] = (byte) 159;
    numArray5[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray5[13] = (byte) 223;
    numArray5[6] = (byte) 1;
    numArray5[15] = (byte) 40;
    numArray5[17] = byte.MaxValue;
    numArray5[0] = (byte) 82;
    byte[] numArray6 = new byte[18];
    numArray6[17] = (byte) 76;
    numArray6[1] = (byte) 43;
    numArray6[14] = (byte) 74;
    numArray6[4] = (byte) 140;
    numArray6[0] = (byte) 43;
    numArray6[3] = (byte) 169;
    numArray6[5] = (byte) 211;
    numArray6[7] = (byte) 63 /*0x3F*/;
    numArray6[11] = (byte) 110;
    numArray6[6] = (byte) 219;
    numArray6[9] = (byte) 157;
    numArray6[8] = (byte) 207;
    numArray6[12] = (byte) 8;
    numArray6[13] = (byte) 72;
    numArray6[10] = (byte) 186;
    numArray6[15] = (byte) 10;
    numArray6[16 /*0x10*/] = (byte) 201;
    numArray6[2] = (byte) 175;
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22147()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 124,
        (byte) 120,
        (byte) 140,
        (byte) 213,
        (byte) 105,
        (byte) 187,
        (byte) 41,
        (byte) 235,
        (byte) 104,
        (byte) 28,
        (byte) 43,
        (byte) 6,
        (byte) 159,
        (byte) 145,
        (byte) 80 /*0x50*/,
        (byte) 194,
        (byte) 251,
        (byte) 158
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 250,
        (byte) 241,
        (byte) 164,
        (byte) 80 /*0x50*/,
        (byte) 125,
        (byte) 102,
        (byte) 41,
        (byte) 174,
        (byte) 222,
        (byte) 168,
        (byte) 154,
        (byte) 141,
        (byte) 239,
        (byte) 74,
        (byte) 65,
        (byte) 243,
        (byte) 73,
        (byte) 66
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
      (byte) 206,
      (byte) 130,
      (byte) 93,
      (byte) 235,
      (byte) 20,
      (byte) 34,
      (byte) 5,
      (byte) 51,
      (byte) 139,
      (byte) 166,
      (byte) 9,
      (byte) 89,
      (byte) 184,
      (byte) 114,
      (byte) 54,
      (byte) 140,
      (byte) 107,
      (byte) 233
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 188,
      (byte) 133,
      (byte) 16 /*0x10*/,
      (byte) 100,
      (byte) 216,
      (byte) 58,
      (byte) 220,
      (byte) 156,
      (byte) 232,
      (byte) 199,
      (byte) 69,
      (byte) 177,
      (byte) 163,
      (byte) 88,
      (byte) 238,
      (byte) 7,
      (byte) 98,
      (byte) 1
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
