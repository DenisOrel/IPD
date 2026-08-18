// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22118
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22118
{
  internal static string ssp_workflow_server_22119()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[3] = (byte) 89;
      numArray2[8] = (byte) 209;
      numArray2[1] = (byte) 25;
      numArray2[4] = (byte) 189;
      numArray2[0] = (byte) 241;
      numArray2[5] = (byte) 167;
      numArray2[6] = (byte) 107;
      numArray2[2] = (byte) 230;
      numArray2[7] = (byte) 112 /*0x70*/;
      byte[] numArray3 = new byte[9];
      numArray3[4] = (byte) 27;
      numArray3[8] = (byte) 40;
      numArray3[2] = (byte) 95;
      numArray3[0] = (byte) 58;
      numArray3[3] = (byte) 6;
      numArray3[5] = (byte) 1;
      numArray3[6] = (byte) 235;
      numArray3[7] = (byte) 25;
      numArray3[1] = (byte) 166;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 143,
      (byte) 79,
      (byte) 197,
      (byte) 21,
      (byte) 29,
      (byte) 206,
      (byte) 158,
      (byte) 224 /*0xE0*/,
      (byte) 253
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 16 /*0x10*/,
      (byte) 251,
      (byte) 194,
      (byte) 90,
      (byte) 216,
      (byte) 192 /*0xC0*/,
      (byte) 127 /*0x7F*/,
      (byte) 198,
      (byte) 241
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22120()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13];
      numArray2[6] = (byte) 127 /*0x7F*/;
      numArray2[9] = (byte) 254;
      numArray2[2] = (byte) 243;
      numArray2[1] = (byte) 223;
      numArray2[4] = (byte) 25;
      numArray2[5] = (byte) 71;
      numArray2[0] = (byte) 45;
      numArray2[7] = (byte) 214;
      numArray2[8] = (byte) 87;
      numArray2[3] = (byte) 204;
      numArray2[10] = (byte) 49;
      numArray2[11] = (byte) 108;
      numArray2[12] = (byte) 140;
      byte[] numArray3 = new byte[13];
      numArray3[8] = (byte) 120;
      numArray3[1] = (byte) 253;
      numArray3[2] = (byte) 98;
      numArray3[3] = (byte) 49;
      numArray3[4] = (byte) 27;
      numArray3[11] = (byte) 20;
      numArray3[12] = (byte) 146;
      numArray3[5] = (byte) 134;
      numArray3[0] = (byte) 85;
      numArray3[7] = (byte) 38;
      numArray3[6] = (byte) 193;
      numArray3[10] = (byte) 237;
      numArray3[9] = (byte) 173;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13]
    {
      (byte) 55,
      (byte) 81,
      (byte) 148,
      (byte) 168,
      (byte) 145,
      (byte) 239,
      (byte) 178,
      (byte) 84,
      (byte) 52,
      (byte) 208 /*0xD0*/,
      (byte) 131,
      (byte) 215,
      (byte) 165
    };
    byte[] numArray6 = new byte[13]
    {
      (byte) 132,
      (byte) 112 /*0x70*/,
      (byte) 49,
      (byte) 100,
      (byte) 191,
      (byte) 159,
      byte.MaxValue,
      (byte) 243,
      (byte) 202,
      (byte) 50,
      (byte) 236,
      (byte) 63 /*0x3F*/,
      (byte) 48 /*0x30*/
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22121()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[227];
      byte[] numArray2 = new byte[55]
      {
        (byte) 5,
        (byte) 96 /*0x60*/,
        (byte) 249,
        (byte) 31 /*0x1F*/,
        (byte) 0,
        (byte) 149,
        (byte) 239,
        (byte) 61,
        (byte) 117,
        (byte) 209,
        (byte) 209,
        (byte) 178,
        (byte) 161,
        (byte) 196,
        (byte) 236,
        (byte) 75,
        (byte) 88,
        (byte) 49,
        (byte) 4,
        (byte) 184,
        (byte) 164,
        (byte) 130,
        (byte) 247,
        (byte) 212,
        (byte) 99,
        (byte) 34,
        (byte) 152,
        (byte) 190,
        (byte) 88,
        (byte) 113,
        (byte) 73,
        (byte) 146,
        (byte) 50,
        (byte) 168,
        (byte) 223,
        (byte) 59,
        (byte) 217,
        (byte) 50,
        (byte) 54,
        (byte) 188,
        (byte) 164,
        (byte) 107,
        (byte) 28,
        (byte) 30,
        (byte) 224 /*0xE0*/,
        (byte) 161,
        (byte) 146,
        (byte) 12,
        (byte) 150,
        (byte) 139,
        (byte) 76,
        (byte) 146,
        (byte) 131,
        (byte) 238,
        (byte) 219
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 215,
        (byte) 154,
        (byte) 27,
        (byte) 229,
        (byte) 14,
        (byte) 141,
        (byte) 235,
        (byte) 235,
        (byte) 195,
        (byte) 42,
        (byte) 203,
        (byte) 146,
        (byte) 224 /*0xE0*/,
        (byte) 28,
        (byte) 119,
        (byte) 170,
        (byte) 7,
        (byte) 86,
        (byte) 56,
        (byte) 108,
        (byte) 216,
        (byte) 87,
        (byte) 95,
        (byte) 47,
        (byte) 119,
        byte.MaxValue,
        (byte) 104,
        (byte) 119,
        (byte) 118,
        (byte) 132,
        (byte) 140,
        (byte) 90,
        (byte) 104,
        (byte) 232,
        (byte) 43,
        (byte) 221,
        (byte) 173,
        (byte) 201,
        (byte) 12,
        (byte) 63 /*0x3F*/,
        (byte) 8,
        (byte) 23,
        (byte) 51,
        (byte) 132,
        (byte) 181,
        (byte) 212,
        (byte) 72,
        (byte) 191,
        (byte) 159,
        (byte) 160 /*0xA0*/,
        (byte) 54,
        (byte) 251,
        (byte) 142,
        (byte) 213,
        (byte) 81
      };
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 36,
        (byte) 187,
        (byte) 222,
        (byte) 253,
        (byte) 49,
        (byte) 228,
        (byte) 4,
        (byte) 135,
        (byte) 62,
        (byte) 128 /*0x80*/,
        (byte) 249,
        (byte) 228,
        (byte) 177,
        (byte) 113,
        (byte) 38,
        (byte) 250,
        (byte) 72,
        (byte) 20,
        (byte) 56,
        (byte) 102,
        (byte) 216,
        (byte) 243,
        (byte) 193,
        (byte) 144 /*0x90*/,
        (byte) 140,
        (byte) 40,
        (byte) 204,
        (byte) 36,
        (byte) 183,
        (byte) 14,
        (byte) 68,
        (byte) 204,
        (byte) 93,
        (byte) 209,
        (byte) 181,
        (byte) 118,
        (byte) 88,
        (byte) 190,
        (byte) 114,
        (byte) 134,
        (byte) 24,
        (byte) 151,
        (byte) 161,
        (byte) 33,
        (byte) 152,
        (byte) 39,
        (byte) 228,
        (byte) 25,
        (byte) 38,
        (byte) 151,
        (byte) 58,
        (byte) 217,
        (byte) 162,
        (byte) 28,
        (byte) 144 /*0x90*/
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 179,
        (byte) 87,
        (byte) 174,
        (byte) 9,
        (byte) 3,
        (byte) 4,
        (byte) 155,
        (byte) 185,
        (byte) 42,
        (byte) 117,
        (byte) 27,
        (byte) 220,
        (byte) 217,
        (byte) 52,
        (byte) 23,
        (byte) 173,
        (byte) 5,
        (byte) 101,
        (byte) 250,
        (byte) 72,
        (byte) 70,
        (byte) 144 /*0x90*/,
        (byte) 40,
        (byte) 57,
        (byte) 56,
        (byte) 97,
        (byte) 121,
        (byte) 30,
        (byte) 81,
        (byte) 183,
        (byte) 173,
        (byte) 5,
        (byte) 207,
        (byte) 84,
        (byte) 11,
        (byte) 41,
        (byte) 78,
        (byte) 177,
        (byte) 224 /*0xE0*/,
        (byte) 221,
        (byte) 254,
        (byte) 182,
        (byte) 8,
        (byte) 231,
        (byte) 147,
        (byte) 46,
        (byte) 31 /*0x1F*/,
        (byte) 140,
        (byte) 152,
        (byte) 243,
        (byte) 236,
        (byte) 208 /*0xD0*/,
        (byte) 214,
        (byte) 20,
        (byte) 158
      };
      key.Query(true, 365, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 77,
        (byte) 113,
        (byte) 9,
        (byte) 23,
        (byte) 223,
        (byte) 208 /*0xD0*/,
        (byte) 48 /*0x30*/,
        (byte) 59,
        (byte) 45,
        (byte) 134,
        (byte) 223,
        (byte) 188,
        (byte) 175,
        (byte) 161,
        (byte) 138,
        (byte) 161,
        (byte) 178,
        (byte) 78,
        (byte) 65,
        (byte) 81,
        (byte) 160 /*0xA0*/,
        (byte) 248,
        (byte) 100,
        (byte) 67,
        (byte) 178,
        (byte) 254,
        (byte) 31 /*0x1F*/,
        (byte) 212,
        (byte) 106,
        (byte) 41,
        (byte) 65,
        (byte) 65,
        (byte) 132,
        (byte) 123,
        (byte) 11,
        (byte) 77,
        (byte) 105,
        (byte) 110,
        (byte) 56,
        (byte) 198,
        (byte) 113,
        (byte) 229,
        (byte) 106,
        (byte) 161,
        (byte) 5,
        (byte) 93,
        (byte) 118,
        (byte) 245,
        (byte) 176 /*0xB0*/,
        (byte) 55,
        (byte) 240 /*0xF0*/,
        (byte) 205,
        (byte) 79,
        (byte) 223,
        (byte) 90
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 133,
        (byte) 194,
        (byte) 186,
        (byte) 111,
        (byte) 233,
        (byte) 136,
        (byte) 104,
        (byte) 251,
        (byte) 182,
        (byte) 70,
        (byte) 219,
        (byte) 8,
        (byte) 114,
        (byte) 88,
        (byte) 29,
        (byte) 129,
        (byte) 11,
        (byte) 65,
        (byte) 101,
        (byte) 94,
        (byte) 89,
        (byte) 216,
        (byte) 248,
        (byte) 113,
        (byte) 202,
        (byte) 80 /*0x50*/,
        (byte) 91,
        (byte) 190,
        (byte) 194,
        (byte) 15,
        (byte) 44,
        (byte) 130,
        (byte) 198,
        (byte) 133,
        (byte) 191,
        (byte) 155,
        (byte) 28,
        (byte) 104,
        (byte) 87,
        (byte) 204,
        (byte) 78,
        (byte) 169,
        (byte) 45,
        (byte) 196,
        (byte) 156,
        (byte) 117,
        (byte) 78,
        (byte) 200,
        (byte) 53,
        (byte) 201,
        (byte) 93,
        (byte) 68,
        (byte) 136,
        (byte) 206,
        (byte) 205
      };
      key.Query(true, 365, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[53] = (byte) 200;
      numArray8[47] = byte.MaxValue;
      numArray8[8] = (byte) 158;
      numArray8[36] = (byte) 87;
      numArray8[46] = (byte) 115;
      numArray8[38] = (byte) 22;
      numArray8[6] = (byte) 3;
      numArray8[22] = (byte) 85;
      numArray8[1] = (byte) 100;
      numArray8[9] = (byte) 56;
      numArray8[29] = (byte) 241;
      numArray8[12] = (byte) 61;
      numArray8[23] = (byte) 135;
      numArray8[37] = (byte) 22;
      numArray8[14] = (byte) 67;
      numArray8[31 /*0x1F*/] = (byte) 33;
      numArray8[19] = (byte) 141;
      numArray8[17] = (byte) 223;
      numArray8[26] = (byte) 51;
      numArray8[34] = (byte) 163;
      numArray8[39] = (byte) 21;
      numArray8[0] = (byte) 197;
      numArray8[3] = (byte) 176 /*0xB0*/;
      numArray8[18] = (byte) 186;
      numArray8[32 /*0x20*/] = (byte) 181;
      numArray8[25] = (byte) 35;
      numArray8[5] = (byte) 208 /*0xD0*/;
      numArray8[27] = (byte) 57;
      numArray8[20] = (byte) 194;
      numArray8[7] = (byte) 69;
      numArray8[30] = (byte) 164;
      numArray8[44] = (byte) 66;
      numArray8[49] = (byte) 184;
      numArray8[33] = (byte) 63 /*0x3F*/;
      numArray8[50] = (byte) 213;
      numArray8[35] = (byte) 9;
      numArray8[16 /*0x10*/] = (byte) 122;
      numArray8[42] = (byte) 102;
      numArray8[24] = (byte) 42;
      numArray8[4] = (byte) 91;
      numArray8[40] = (byte) 103;
      numArray8[41] = (byte) 91;
      numArray8[2] = (byte) 224 /*0xE0*/;
      numArray8[43] = (byte) 37;
      numArray8[21] = (byte) 61;
      numArray8[28] = (byte) 115;
      numArray8[45] = (byte) 94;
      numArray8[11] = (byte) 150;
      numArray8[48 /*0x30*/] = (byte) 173;
      numArray8[10] = (byte) 52;
      numArray8[15] = (byte) 21;
      numArray8[51] = byte.MaxValue;
      numArray8[52] = (byte) 159;
      numArray8[13] = (byte) 56;
      numArray8[54] = (byte) 74;
      byte[] numArray9 = new byte[55];
      numArray9[23] = (byte) 208 /*0xD0*/;
      numArray9[1] = (byte) 114;
      numArray9[33] = (byte) 237;
      numArray9[8] = (byte) 217;
      numArray9[4] = (byte) 164;
      numArray9[53] = (byte) 168;
      numArray9[6] = (byte) 112 /*0x70*/;
      numArray9[7] = (byte) 90;
      numArray9[51] = (byte) 194;
      numArray9[9] = (byte) 196;
      numArray9[20] = (byte) 225;
      numArray9[11] = (byte) 171;
      numArray9[10] = (byte) 14;
      numArray9[48 /*0x30*/] = (byte) 103;
      numArray9[37] = (byte) 73;
      numArray9[34] = (byte) 146;
      numArray9[16 /*0x10*/] = (byte) 154;
      numArray9[13] = (byte) 59;
      numArray9[3] = (byte) 127 /*0x7F*/;
      numArray9[29] = (byte) 237;
      numArray9[41] = (byte) 75;
      numArray9[31 /*0x1F*/] = (byte) 34;
      numArray9[22] = (byte) 173;
      numArray9[30] = (byte) 68;
      numArray9[24] = (byte) 245;
      numArray9[25] = (byte) 148;
      numArray9[26] = (byte) 1;
      numArray9[21] = (byte) 82;
      numArray9[28] = (byte) 32 /*0x20*/;
      numArray9[15] = (byte) 217;
      numArray9[27] = (byte) 168;
      numArray9[18] = (byte) 18;
      numArray9[32 /*0x20*/] = (byte) 97;
      numArray9[35] = (byte) 72;
      numArray9[47] = (byte) 242;
      numArray9[0] = (byte) 206;
      numArray9[17] = (byte) 51;
      numArray9[36] = (byte) 169;
      numArray9[38] = (byte) 70;
      numArray9[39] = (byte) 117;
      numArray9[40] = (byte) 251;
      numArray9[49] = (byte) 33;
      numArray9[42] = (byte) 253;
      numArray9[43] = (byte) 100;
      numArray9[44] = (byte) 201;
      numArray9[45] = (byte) 41;
      numArray9[46] = (byte) 42;
      numArray9[14] = (byte) 145;
      numArray9[19] = (byte) 205;
      numArray9[5] = (byte) 73;
      numArray9[50] = (byte) 82;
      numArray9[12] = (byte) 119;
      numArray9[2] = (byte) 86;
      numArray9[52] = (byte) 173;
      numArray9[54] = (byte) 196;
      key.Query(true, 365, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[7]
      {
        byte.MaxValue,
        (byte) 107,
        (byte) 77,
        (byte) 162,
        (byte) 38,
        (byte) 85,
        (byte) 228
      };
      byte[] numArray11 = new byte[7]
      {
        (byte) 111,
        (byte) 122,
        (byte) 134,
        (byte) 152,
        (byte) 220,
        (byte) 195,
        (byte) 13
      };
      key.Query(true, 365, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[227];
    byte[] numArray13 = new byte[55];
    numArray13[38] = (byte) 161;
    numArray13[16 /*0x10*/] = (byte) 133;
    numArray13[2] = (byte) 35;
    numArray13[17] = (byte) 13;
    numArray13[4] = (byte) 252;
    numArray13[5] = (byte) 148;
    numArray13[13] = (byte) 203;
    numArray13[37] = (byte) 102;
    numArray13[8] = (byte) 107;
    numArray13[9] = (byte) 243;
    numArray13[10] = (byte) 252;
    numArray13[14] = (byte) 212;
    numArray13[3] = (byte) 212;
    numArray13[54] = (byte) 119;
    numArray13[12] = (byte) 196;
    numArray13[15] = (byte) 227;
    numArray13[1] = (byte) 12;
    numArray13[28] = (byte) 56;
    numArray13[18] = (byte) 35;
    numArray13[19] = (byte) 185;
    numArray13[20] = (byte) 48 /*0x30*/;
    numArray13[52] = (byte) 92;
    numArray13[22] = (byte) 215;
    numArray13[51] = (byte) 215;
    numArray13[25] = (byte) 106;
    numArray13[21] = (byte) 13;
    numArray13[50] = (byte) 208 /*0xD0*/;
    numArray13[11] = (byte) 116;
    numArray13[24] = (byte) 235;
    numArray13[29] = (byte) 29;
    numArray13[30] = (byte) 68;
    numArray13[31 /*0x1F*/] = (byte) 34;
    numArray13[27] = (byte) 107;
    numArray13[33] = (byte) 225;
    numArray13[34] = (byte) 122;
    numArray13[41] = (byte) 240 /*0xF0*/;
    numArray13[36] = (byte) 16 /*0x10*/;
    numArray13[26] = (byte) 9;
    numArray13[0] = (byte) 244;
    numArray13[47] = (byte) 129;
    numArray13[6] = (byte) 31 /*0x1F*/;
    numArray13[35] = (byte) 230;
    numArray13[42] = (byte) 153;
    numArray13[32 /*0x20*/] = (byte) 150;
    numArray13[44] = (byte) 66;
    numArray13[45] = (byte) 138;
    numArray13[49] = (byte) 66;
    numArray13[23] = (byte) 219;
    numArray13[48 /*0x30*/] = (byte) 205;
    numArray13[46] = (byte) 167;
    numArray13[43] = (byte) 160 /*0xA0*/;
    numArray13[39] = (byte) 181;
    numArray13[7] = (byte) 41;
    numArray13[53] = (byte) 79;
    numArray13[40] = (byte) 173;
    byte[] numArray14 = new byte[55];
    numArray14[15] = (byte) 66;
    numArray14[54] = (byte) 137;
    numArray14[2] = (byte) 4;
    numArray14[3] = (byte) 6;
    numArray14[32 /*0x20*/] = (byte) 185;
    numArray14[5] = (byte) 25;
    numArray14[6] = (byte) 13;
    numArray14[19] = (byte) 145;
    numArray14[1] = (byte) 155;
    numArray14[9] = (byte) 209;
    numArray14[37] = (byte) 176 /*0xB0*/;
    numArray14[43] = (byte) 79;
    numArray14[51] = (byte) 77;
    numArray14[13] = (byte) 201;
    numArray14[40] = (byte) 198;
    numArray14[33] = (byte) 77;
    numArray14[16 /*0x10*/] = (byte) 51;
    numArray14[22] = (byte) 36;
    numArray14[18] = (byte) 45;
    numArray14[7] = (byte) 237;
    numArray14[14] = (byte) 100;
    numArray14[21] = (byte) 248;
    numArray14[8] = (byte) 26;
    numArray14[23] = (byte) 219;
    numArray14[17] = (byte) 53;
    numArray14[44] = (byte) 132;
    numArray14[11] = (byte) 156;
    numArray14[4] = (byte) 156;
    numArray14[50] = (byte) 157;
    numArray14[29] = (byte) 17;
    numArray14[10] = (byte) 239;
    numArray14[31 /*0x1F*/] = (byte) 212;
    numArray14[36] = (byte) 3;
    numArray14[28] = (byte) 70;
    numArray14[24] = (byte) 26;
    numArray14[35] = (byte) 219;
    numArray14[27] = (byte) 47;
    numArray14[26] = (byte) 36;
    numArray14[25] = (byte) 58;
    numArray14[39] = (byte) 28;
    numArray14[0] = (byte) 205;
    numArray14[52] = (byte) 197;
    numArray14[42] = (byte) 4;
    numArray14[30] = (byte) 114;
    numArray14[20] = (byte) 226;
    numArray14[45] = (byte) 11;
    numArray14[46] = (byte) 82;
    numArray14[47] = (byte) 63 /*0x3F*/;
    numArray14[12] = (byte) 20;
    numArray14[34] = (byte) 77;
    numArray14[38] = (byte) 252;
    numArray14[49] = (byte) 80 /*0x50*/;
    numArray14[41] = (byte) 138;
    numArray14[53] = (byte) 84;
    numArray14[48 /*0x30*/] = (byte) 55;
    key.Query(true, 365, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[36] = (byte) 112 /*0x70*/;
    numArray15[32 /*0x20*/] = (byte) 213;
    numArray15[2] = (byte) 208 /*0xD0*/;
    numArray15[22] = (byte) 161;
    numArray15[4] = (byte) 88;
    numArray15[3] = (byte) 171;
    numArray15[7] = (byte) 63 /*0x3F*/;
    numArray15[54] = (byte) 36;
    numArray15[29] = (byte) 45;
    numArray15[9] = (byte) 148;
    numArray15[23] = (byte) 177;
    numArray15[11] = (byte) 157;
    numArray15[10] = (byte) 69;
    numArray15[1] = (byte) 209;
    numArray15[14] = (byte) 170;
    numArray15[15] = (byte) 28;
    numArray15[16 /*0x10*/] = (byte) 28;
    numArray15[17] = (byte) 47;
    numArray15[6] = (byte) 101;
    numArray15[5] = (byte) 117;
    numArray15[26] = (byte) 32 /*0x20*/;
    numArray15[38] = (byte) 138;
    numArray15[53] = (byte) 109;
    numArray15[44] = (byte) 7;
    numArray15[24] = (byte) 228;
    numArray15[25] = (byte) 25;
    numArray15[19] = (byte) 221;
    numArray15[27] = (byte) 138;
    numArray15[42] = (byte) 23;
    numArray15[20] = (byte) 148;
    numArray15[30] = (byte) 209;
    numArray15[31 /*0x1F*/] = (byte) 62;
    numArray15[12] = (byte) 135;
    numArray15[37] = (byte) 123;
    numArray15[0] = (byte) 126;
    numArray15[52] = (byte) 181;
    numArray15[45] = (byte) 213;
    numArray15[34] = (byte) 156;
    numArray15[51] = (byte) 2;
    numArray15[39] = (byte) 141;
    numArray15[40] = (byte) 228;
    numArray15[41] = (byte) 113;
    numArray15[21] = (byte) 220;
    numArray15[43] = (byte) 127 /*0x7F*/;
    numArray15[28] = (byte) 243;
    numArray15[18] = (byte) 63 /*0x3F*/;
    numArray15[46] = (byte) 28;
    numArray15[47] = (byte) 119;
    numArray15[35] = (byte) 36;
    numArray15[49] = (byte) 242;
    numArray15[50] = (byte) 141;
    numArray15[48 /*0x30*/] = (byte) 102;
    numArray15[13] = (byte) 239;
    numArray15[33] = (byte) 251;
    numArray15[8] = (byte) 60;
    byte[] numArray16 = new byte[55];
    numArray16[18] = (byte) 156;
    numArray16[1] = (byte) 140;
    numArray16[2] = (byte) 104;
    numArray16[44] = (byte) 245;
    numArray16[25] = (byte) 75;
    numArray16[36] = (byte) 82;
    numArray16[45] = (byte) 42;
    numArray16[7] = (byte) 238;
    numArray16[48 /*0x30*/] = (byte) 249;
    numArray16[30] = (byte) 159;
    numArray16[26] = (byte) 160 /*0xA0*/;
    numArray16[4] = (byte) 164;
    numArray16[6] = (byte) 106;
    numArray16[13] = (byte) 114;
    numArray16[14] = (byte) 162;
    numArray16[15] = (byte) 192 /*0xC0*/;
    numArray16[38] = (byte) 215;
    numArray16[17] = (byte) 166;
    numArray16[16 /*0x10*/] = (byte) 85;
    numArray16[19] = (byte) 80 /*0x50*/;
    numArray16[20] = (byte) 18;
    numArray16[8] = (byte) 125;
    numArray16[47] = (byte) 38;
    numArray16[29] = (byte) 110;
    numArray16[54] = (byte) 125;
    numArray16[10] = (byte) 151;
    numArray16[3] = (byte) 134;
    numArray16[27] = (byte) 166;
    numArray16[9] = (byte) 233;
    numArray16[21] = (byte) 218;
    numArray16[42] = (byte) 200;
    numArray16[31 /*0x1F*/] = (byte) 169;
    numArray16[46] = (byte) 21;
    numArray16[5] = (byte) 122;
    numArray16[0] = (byte) 228;
    numArray16[35] = (byte) 234;
    numArray16[39] = (byte) 1;
    numArray16[37] = (byte) 217;
    numArray16[32 /*0x20*/] = (byte) 21;
    numArray16[12] = (byte) 104;
    numArray16[41] = (byte) 111;
    numArray16[33] = (byte) 71;
    numArray16[51] = (byte) 224 /*0xE0*/;
    numArray16[43] = (byte) 37;
    numArray16[40] = (byte) 93;
    numArray16[28] = (byte) 37;
    numArray16[11] = (byte) 136;
    numArray16[53] = (byte) 16 /*0x10*/;
    numArray16[23] = (byte) 247;
    numArray16[49] = (byte) 244;
    numArray16[50] = (byte) 34;
    numArray16[34] = (byte) 83;
    numArray16[52] = (byte) 3;
    numArray16[22] = (byte) 56;
    numArray16[24] = (byte) 203;
    key.Query(true, 365, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[26] = (byte) 208 /*0xD0*/;
    numArray17[12] = (byte) 170;
    numArray17[29] = (byte) 250;
    numArray17[14] = (byte) 240 /*0xF0*/;
    numArray17[6] = (byte) 188;
    numArray17[2] = (byte) 213;
    numArray17[19] = (byte) 35;
    numArray17[3] = (byte) 39;
    numArray17[24] = (byte) 184;
    numArray17[9] = (byte) 107;
    numArray17[10] = (byte) 222;
    numArray17[11] = (byte) 58;
    numArray17[7] = (byte) 129;
    numArray17[4] = (byte) 117;
    numArray17[40] = (byte) 221;
    numArray17[0] = (byte) 202;
    numArray17[16 /*0x10*/] = (byte) 235;
    numArray17[17] = (byte) 156;
    numArray17[18] = (byte) 32 /*0x20*/;
    numArray17[38] = (byte) 221;
    numArray17[34] = (byte) 17;
    numArray17[8] = (byte) 247;
    numArray17[22] = (byte) 82;
    numArray17[23] = (byte) 146;
    numArray17[27] = (byte) 179;
    numArray17[15] = (byte) 81;
    numArray17[32 /*0x20*/] = (byte) 253;
    numArray17[48 /*0x30*/] = (byte) 15;
    numArray17[28] = (byte) 112 /*0x70*/;
    numArray17[5] = (byte) 142;
    numArray17[43] = (byte) 67;
    numArray17[35] = (byte) 69;
    numArray17[20] = (byte) 158;
    numArray17[33] = (byte) 63 /*0x3F*/;
    numArray17[39] = (byte) 77;
    numArray17[21] = (byte) 38;
    numArray17[13] = (byte) 126;
    numArray17[37] = (byte) 100;
    numArray17[30] = (byte) 109;
    numArray17[41] = (byte) 70;
    numArray17[53] = (byte) 252;
    numArray17[47] = (byte) 108;
    numArray17[42] = (byte) 140;
    numArray17[44] = (byte) 252;
    numArray17[25] = (byte) 241;
    numArray17[45] = (byte) 145;
    numArray17[46] = (byte) 166;
    numArray17[36] = (byte) 51;
    numArray17[1] = (byte) 182;
    numArray17[49] = (byte) 176 /*0xB0*/;
    numArray17[50] = (byte) 159;
    numArray17[51] = (byte) 232;
    numArray17[52] = (byte) 228;
    numArray17[31 /*0x1F*/] = (byte) 209;
    numArray17[54] = (byte) 9;
    byte[] numArray18 = new byte[55]
    {
      (byte) 150,
      (byte) 166,
      (byte) 112 /*0x70*/,
      (byte) 36,
      (byte) 138,
      (byte) 13,
      (byte) 126,
      (byte) 246,
      (byte) 13,
      (byte) 199,
      (byte) 182,
      (byte) 140,
      (byte) 121,
      (byte) 138,
      (byte) 50,
      (byte) 239,
      (byte) 34,
      (byte) 245,
      (byte) 59,
      (byte) 61,
      (byte) 126,
      (byte) 69,
      (byte) 116,
      (byte) 45,
      (byte) 232,
      (byte) 114,
      (byte) 2,
      (byte) 109,
      (byte) 178,
      (byte) 254,
      (byte) 82,
      (byte) 182,
      (byte) 33,
      (byte) 153,
      (byte) 241,
      (byte) 150,
      byte.MaxValue,
      (byte) 64 /*0x40*/,
      (byte) 228,
      (byte) 99,
      (byte) 60,
      (byte) 153,
      (byte) 176 /*0xB0*/,
      (byte) 225,
      (byte) 55,
      (byte) 103,
      (byte) 227,
      (byte) 55,
      (byte) 53,
      (byte) 186,
      (byte) 178,
      (byte) 116,
      (byte) 132,
      (byte) 103,
      (byte) 63 /*0x3F*/
    };
    key.Query(true, 365, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 173,
      (byte) 72,
      (byte) 55,
      (byte) 89,
      (byte) 100,
      (byte) 81,
      (byte) 162,
      (byte) 120,
      (byte) 32 /*0x20*/,
      (byte) 60,
      (byte) 52,
      byte.MaxValue,
      (byte) 201,
      (byte) 46,
      (byte) 146,
      (byte) 101,
      (byte) 67,
      (byte) 114,
      (byte) 33,
      (byte) 9,
      (byte) 201,
      (byte) 166,
      (byte) 211,
      (byte) 16 /*0x10*/,
      (byte) 100,
      (byte) 62,
      (byte) 45,
      (byte) 66,
      (byte) 91,
      (byte) 219,
      (byte) 74,
      (byte) 131,
      (byte) 97,
      (byte) 139,
      (byte) 66,
      (byte) 172,
      (byte) 34,
      (byte) 53,
      (byte) 111,
      (byte) 227,
      (byte) 237,
      (byte) 205,
      (byte) 206,
      (byte) 23,
      (byte) 6,
      byte.MaxValue,
      (byte) 155,
      (byte) 99,
      (byte) 133,
      (byte) 195,
      (byte) 205,
      (byte) 68,
      (byte) 144 /*0x90*/,
      (byte) 1,
      (byte) 140
    };
    byte[] numArray20 = new byte[55];
    numArray20[0] = (byte) 71;
    numArray20[1] = (byte) 51;
    numArray20[38] = (byte) 41;
    numArray20[31 /*0x1F*/] = (byte) 27;
    numArray20[6] = (byte) 44;
    numArray20[5] = (byte) 170;
    numArray20[41] = (byte) 147;
    numArray20[7] = (byte) 72;
    numArray20[3] = (byte) 16 /*0x10*/;
    numArray20[39] = (byte) 183;
    numArray20[14] = (byte) 16 /*0x10*/;
    numArray20[11] = (byte) 205;
    numArray20[25] = (byte) 107;
    numArray20[37] = (byte) 215;
    numArray20[46] = (byte) 143;
    numArray20[15] = (byte) 95;
    numArray20[16 /*0x10*/] = (byte) 163;
    numArray20[17] = (byte) 49;
    numArray20[27] = (byte) 156;
    numArray20[19] = (byte) 12;
    numArray20[20] = (byte) 224 /*0xE0*/;
    numArray20[12] = (byte) 110;
    numArray20[44] = (byte) 191;
    numArray20[23] = (byte) 217;
    numArray20[13] = (byte) 32 /*0x20*/;
    numArray20[10] = (byte) 190;
    numArray20[26] = (byte) 152;
    numArray20[43] = (byte) 29;
    numArray20[28] = (byte) 39;
    numArray20[29] = (byte) 245;
    numArray20[30] = (byte) 226;
    numArray20[52] = (byte) 239;
    numArray20[9] = (byte) 87;
    numArray20[21] = (byte) 90;
    numArray20[33] = (byte) 39;
    numArray20[35] = (byte) 73;
    numArray20[36] = (byte) 2;
    numArray20[51] = (byte) 109;
    numArray20[34] = (byte) 17;
    numArray20[24] = (byte) 247;
    numArray20[40] = (byte) 89;
    numArray20[42] = (byte) 89;
    numArray20[18] = (byte) 243;
    numArray20[2] = (byte) 93;
    numArray20[8] = (byte) 42;
    numArray20[45] = (byte) 235;
    numArray20[47] = (byte) 163;
    numArray20[50] = (byte) 178;
    numArray20[48 /*0x30*/] = (byte) 31 /*0x1F*/;
    numArray20[49] = (byte) 29;
    numArray20[22] = (byte) 3;
    numArray20[4] = (byte) 56;
    numArray20[32 /*0x20*/] = (byte) 228;
    numArray20[53] = (byte) 180;
    numArray20[54] = (byte) 227;
    key.Query(true, 365, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[7]
    {
      (byte) 29,
      (byte) 145,
      (byte) 130,
      (byte) 89,
      (byte) 163,
      (byte) 133,
      (byte) 54
    };
    byte[] numArray22 = new byte[7]
    {
      (byte) 199,
      (byte) 187,
      (byte) 170,
      (byte) 99,
      (byte) 101,
      (byte) 21,
      (byte) 155
    };
    key.Query(true, 365, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 7);
    for (int index = 0; index < 7; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static int ssp_workflow_server_22122(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[39] = (byte) 84;
    sourceArray1[4] = (byte) 125;
    sourceArray1[2] = (byte) 206;
    sourceArray1[14] = (byte) 22;
    sourceArray1[37] = (byte) 136;
    sourceArray1[5] = (byte) 143;
    sourceArray1[22] = (byte) 222;
    sourceArray1[23] = (byte) 174;
    sourceArray1[8] = (byte) 149;
    sourceArray1[9] = (byte) 180;
    sourceArray1[0] = (byte) 173;
    sourceArray1[45] = (byte) 59;
    sourceArray1[11] = (byte) 70;
    sourceArray1[13] = (byte) 28;
    sourceArray1[35] = (byte) 58;
    sourceArray1[10] = (byte) 2;
    sourceArray1[15] = (byte) 78;
    sourceArray1[25] = (byte) 102;
    sourceArray1[18] = (byte) 123;
    sourceArray1[34] = (byte) 242;
    sourceArray1[20] = (byte) 14;
    sourceArray1[21] = (byte) 126;
    sourceArray1[36] = (byte) 241;
    sourceArray1[43] = (byte) 104;
    sourceArray1[24] = (byte) 199;
    sourceArray1[12] = (byte) 82;
    sourceArray1[26] = (byte) 233;
    sourceArray1[27] = (byte) 42;
    sourceArray1[16 /*0x10*/] = (byte) 197;
    sourceArray1[29] = (byte) 59;
    sourceArray1[28] = (byte) 83;
    sourceArray1[31 /*0x1F*/] = (byte) 35;
    sourceArray1[32 /*0x20*/] = (byte) 30;
    sourceArray1[1] = (byte) 71;
    sourceArray1[33] = (byte) 200;
    sourceArray1[41] = (byte) 223;
    sourceArray1[17] = (byte) 159;
    sourceArray1[7] = (byte) 78;
    sourceArray1[19] = (byte) 111;
    sourceArray1[3] = (byte) 185;
    sourceArray1[40] = (byte) 67;
    sourceArray1[6] = (byte) 76;
    sourceArray1[42] = (byte) 35;
    sourceArray1[38] = (byte) 213;
    sourceArray1[44] = (byte) 142;
    sourceArray1[30] = (byte) 150;
    sourceArray1[46] = (byte) 7;
    sourceArray1[47] = (byte) 135;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 146,
      (byte) 129,
      (byte) 141,
      (byte) 202,
      (byte) 59,
      (byte) 250,
      (byte) 169,
      (byte) 72,
      (byte) 148,
      (byte) 58,
      (byte) 174,
      (byte) 83,
      (byte) 177,
      (byte) 247,
      (byte) 182,
      (byte) 202,
      (byte) 236,
      (byte) 50,
      (byte) 37,
      (byte) 175,
      (byte) 149,
      (byte) 157,
      (byte) 191,
      (byte) 128 /*0x80*/,
      (byte) 185,
      (byte) 195,
      (byte) 195,
      (byte) 82,
      (byte) 221,
      (byte) 98,
      (byte) 133,
      (byte) 62,
      (byte) 192 /*0xC0*/,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 125,
      (byte) 0,
      (byte) 81,
      (byte) 109,
      (byte) 212,
      (byte) 91,
      (byte) 164,
      (byte) 224 /*0xE0*/,
      (byte) 190,
      (byte) 136,
      (byte) 6,
      (byte) 183,
      (byte) 6
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_server_22123()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[13] = (byte) 101;
      numArray2[12] = (byte) 0;
      numArray2[4] = (byte) 137;
      numArray2[0] = (byte) 19;
      numArray2[15] = (byte) 68;
      numArray2[6] = (byte) 84;
      numArray2[2] = (byte) 93;
      numArray2[5] = (byte) 231;
      numArray2[3] = (byte) 146;
      numArray2[9] = (byte) 115;
      numArray2[7] = (byte) 155;
      numArray2[11] = (byte) 243;
      numArray2[8] = (byte) 163;
      numArray2[10] = (byte) 212;
      numArray2[14] = (byte) 184;
      numArray2[1] = (byte) 125;
      numArray2[16 /*0x10*/] = (byte) 101;
      numArray2[17] = (byte) 141;
      byte[] numArray3 = new byte[18];
      numArray3[13] = (byte) 238;
      numArray3[0] = (byte) 210;
      numArray3[5] = (byte) 68;
      numArray3[14] = (byte) 158;
      numArray3[2] = (byte) 95;
      numArray3[4] = (byte) 106;
      numArray3[10] = (byte) 209;
      numArray3[9] = (byte) 45;
      numArray3[8] = (byte) 177;
      numArray3[11] = (byte) 98;
      numArray3[12] = (byte) 183;
      numArray3[1] = (byte) 188;
      numArray3[16 /*0x10*/] = (byte) 195;
      numArray3[7] = (byte) 172;
      numArray3[6] = (byte) 132;
      numArray3[15] = (byte) 27;
      numArray3[3] = (byte) 77;
      numArray3[17] = (byte) 232;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[16 /*0x10*/] = (byte) 19;
    numArray5[12] = (byte) 215;
    numArray5[10] = (byte) 145;
    numArray5[3] = (byte) 65;
    numArray5[4] = (byte) 72;
    numArray5[9] = (byte) 200;
    numArray5[6] = (byte) 154;
    numArray5[7] = (byte) 41;
    numArray5[0] = (byte) 247;
    numArray5[2] = (byte) 252;
    numArray5[5] = (byte) 35;
    numArray5[15] = (byte) 170;
    numArray5[1] = (byte) 135;
    numArray5[13] = (byte) 63 /*0x3F*/;
    numArray5[11] = (byte) 237;
    numArray5[8] = (byte) 67;
    numArray5[14] = (byte) 59;
    numArray5[17] = (byte) 131;
    byte[] numArray6 = new byte[18]
    {
      (byte) 124,
      (byte) 219,
      (byte) 64 /*0x40*/,
      (byte) 46,
      (byte) 211,
      (byte) 147,
      (byte) 167,
      (byte) 243,
      (byte) 150,
      (byte) 67,
      (byte) 95,
      (byte) 91,
      (byte) 202,
      (byte) 196,
      (byte) 15,
      (byte) 181,
      (byte) 236,
      (byte) 86
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_workflow_server_22124()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[11] = (byte) 32 /*0x20*/;
      numArray2[9] = (byte) 172;
      numArray2[2] = (byte) 172;
      numArray2[6] = (byte) 45;
      numArray2[4] = (byte) 63 /*0x3F*/;
      numArray2[1] = (byte) 138;
      numArray2[16 /*0x10*/] = (byte) 89;
      numArray2[7] = (byte) 43;
      numArray2[14] = (byte) 108;
      numArray2[17] = (byte) 65;
      numArray2[10] = (byte) 115;
      numArray2[5] = (byte) 66;
      numArray2[12] = (byte) 122;
      numArray2[15] = (byte) 128 /*0x80*/;
      numArray2[3] = (byte) 21;
      numArray2[0] = (byte) 18;
      numArray2[8] = (byte) 129;
      numArray2[13] = (byte) 16 /*0x10*/;
      byte[] numArray3 = new byte[18];
      numArray3[9] = (byte) 223;
      numArray3[1] = (byte) 196;
      numArray3[10] = (byte) 181;
      numArray3[8] = (byte) 254;
      numArray3[4] = (byte) 213;
      numArray3[2] = (byte) 5;
      numArray3[0] = (byte) 68;
      numArray3[6] = (byte) 173;
      numArray3[15] = (byte) 212;
      numArray3[7] = (byte) 242;
      numArray3[13] = (byte) 241;
      numArray3[11] = (byte) 144 /*0x90*/;
      numArray3[5] = (byte) 182;
      numArray3[3] = (byte) 143;
      numArray3[14] = (byte) 184;
      numArray3[12] = (byte) 181;
      numArray3[16 /*0x10*/] = (byte) 137;
      numArray3[17] = (byte) 21;
      key.Query(true, 365, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[4] = (byte) 29;
    numArray5[1] = (byte) 212;
    numArray5[11] = (byte) 247;
    numArray5[8] = (byte) 78;
    numArray5[0] = (byte) 114;
    numArray5[5] = (byte) 248;
    numArray5[16 /*0x10*/] = (byte) 151;
    numArray5[13] = (byte) 180;
    numArray5[7] = (byte) 232;
    numArray5[12] = (byte) 71;
    numArray5[10] = (byte) 100;
    numArray5[15] = (byte) 196;
    numArray5[2] = (byte) 235;
    numArray5[14] = (byte) 66;
    numArray5[9] = (byte) 119;
    numArray5[6] = (byte) 232;
    numArray5[3] = (byte) 145;
    numArray5[17] = (byte) 105;
    byte[] numArray6 = new byte[18]
    {
      (byte) 109,
      (byte) 244,
      (byte) 44,
      (byte) 162,
      (byte) 14,
      (byte) 205,
      (byte) 177,
      (byte) 44,
      (byte) 202,
      (byte) 82,
      (byte) 1,
      (byte) 54,
      (byte) 50,
      (byte) 173,
      (byte) 90,
      (byte) 94,
      (byte) 180,
      (byte) 77
    };
    key.Query(true, 365, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
