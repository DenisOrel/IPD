// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12313
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12313
{
  private static byte[] sspq = new byte[230]
  {
    (byte) 93,
    (byte) 64 /*0x40*/,
    (byte) 240 /*0xF0*/,
    (byte) 142,
    (byte) 8,
    (byte) 229,
    (byte) 79,
    (byte) 158,
    (byte) 211,
    (byte) 165,
    (byte) 149,
    (byte) 251,
    (byte) 38,
    (byte) 173,
    (byte) 171,
    (byte) 226,
    (byte) 109,
    (byte) 120,
    (byte) 136,
    (byte) 194,
    (byte) 102,
    (byte) 116,
    (byte) 38,
    (byte) 66,
    (byte) 182,
    (byte) 157,
    (byte) 213,
    (byte) 181,
    (byte) 158,
    (byte) 66,
    (byte) 207,
    (byte) 170,
    (byte) 175,
    (byte) 58,
    (byte) 39,
    (byte) 98,
    (byte) 130,
    (byte) 156,
    (byte) 32 /*0x20*/,
    (byte) 11,
    (byte) 0,
    (byte) 132,
    (byte) 82,
    (byte) 4,
    (byte) 6,
    (byte) 168,
    (byte) 228,
    (byte) 165,
    (byte) 84,
    (byte) 198,
    (byte) 69,
    (byte) 28,
    (byte) 207,
    (byte) 1,
    (byte) 126,
    (byte) 39,
    (byte) 201,
    (byte) 39,
    (byte) 139,
    (byte) 118,
    (byte) 18,
    (byte) 221,
    (byte) 39,
    (byte) 230,
    (byte) 122,
    (byte) 135,
    (byte) 68,
    (byte) 177,
    (byte) 31 /*0x1F*/,
    (byte) 207,
    (byte) 135,
    (byte) 163,
    (byte) 245,
    (byte) 75,
    (byte) 138,
    (byte) 124,
    (byte) 90,
    (byte) 239,
    (byte) 132,
    (byte) 15,
    (byte) 253,
    (byte) 137,
    (byte) 121,
    (byte) 58,
    (byte) 58,
    (byte) 59,
    (byte) 46,
    (byte) 117,
    (byte) 202,
    (byte) 45,
    (byte) 139,
    (byte) 115,
    (byte) 236,
    (byte) 224 /*0xE0*/,
    (byte) 87,
    (byte) 220,
    (byte) 68,
    (byte) 4,
    (byte) 10,
    (byte) 178,
    (byte) 101,
    (byte) 206,
    (byte) 106,
    (byte) 127 /*0x7F*/,
    (byte) 99,
    (byte) 167,
    (byte) 209,
    (byte) 124,
    (byte) 75,
    (byte) 28,
    (byte) 104,
    (byte) 151,
    (byte) 170,
    (byte) 225,
    (byte) 252,
    (byte) 32 /*0x20*/,
    (byte) 237,
    (byte) 153,
    (byte) 249,
    (byte) 253,
    (byte) 167,
    (byte) 175,
    (byte) 122,
    (byte) 132,
    (byte) 118,
    (byte) 37,
    (byte) 214,
    (byte) 19,
    (byte) 38,
    (byte) 5,
    (byte) 187,
    (byte) 226,
    (byte) 49,
    (byte) 5,
    (byte) 83,
    (byte) 196,
    (byte) 31 /*0x1F*/,
    (byte) 183,
    (byte) 231,
    (byte) 247,
    (byte) 151,
    (byte) 106,
    (byte) 181,
    (byte) 226,
    (byte) 229,
    (byte) 2,
    (byte) 83,
    (byte) 254,
    (byte) 153,
    (byte) 184,
    (byte) 38,
    (byte) 142,
    (byte) 133,
    (byte) 192 /*0xC0*/,
    (byte) 118,
    (byte) 218,
    (byte) 123,
    (byte) 99,
    (byte) 71,
    (byte) 68,
    (byte) 169,
    (byte) 208 /*0xD0*/,
    (byte) 24,
    (byte) 230,
    (byte) 174,
    (byte) 87,
    (byte) 104,
    (byte) 34,
    (byte) 96 /*0x60*/,
    (byte) 113,
    (byte) 214,
    (byte) 254,
    (byte) 105,
    (byte) 185,
    (byte) 68,
    (byte) 86,
    (byte) 210,
    (byte) 102,
    (byte) 223,
    (byte) 174,
    (byte) 143,
    (byte) 56,
    (byte) 35,
    (byte) 244,
    (byte) 25,
    (byte) 65,
    (byte) 81,
    (byte) 139,
    (byte) 92,
    (byte) 135,
    (byte) 195,
    (byte) 200,
    (byte) 56,
    (byte) 163,
    (byte) 177,
    (byte) 220,
    (byte) 17,
    (byte) 130,
    (byte) 222,
    (byte) 160 /*0xA0*/,
    (byte) 109,
    (byte) 180,
    (byte) 157,
    (byte) 218,
    (byte) 52,
    (byte) 216,
    (byte) 175,
    (byte) 23,
    (byte) 148,
    (byte) 213,
    (byte) 195,
    (byte) 82,
    (byte) 138,
    (byte) 153,
    (byte) 84,
    (byte) 52,
    (byte) 80 /*0x50*/,
    (byte) 237,
    (byte) 203,
    (byte) 10,
    (byte) 172,
    (byte) 177,
    (byte) 80 /*0x50*/,
    (byte) 153,
    (byte) 242,
    (byte) 28,
    (byte) 195,
    (byte) 44,
    (byte) 83,
    (byte) 200
  };
  private static byte[] sspr = new byte[230]
  {
    (byte) 166,
    (byte) 12,
    (byte) 77,
    (byte) 105,
    (byte) 187,
    (byte) 248,
    (byte) 83,
    (byte) 127 /*0x7F*/,
    (byte) 93,
    (byte) 223,
    (byte) 125,
    (byte) 58,
    (byte) 106,
    (byte) 135,
    (byte) 225,
    (byte) 49,
    (byte) 135,
    (byte) 183,
    (byte) 70,
    (byte) 109,
    (byte) 217,
    (byte) 201,
    (byte) 0,
    (byte) 4,
    (byte) 54,
    (byte) 193,
    (byte) 28,
    (byte) 220,
    (byte) 20,
    (byte) 83,
    (byte) 3,
    (byte) 243,
    (byte) 93,
    (byte) 188,
    (byte) 50,
    (byte) 0,
    (byte) 132,
    (byte) 16 /*0x10*/,
    (byte) 208 /*0xD0*/,
    (byte) 212,
    (byte) 99,
    (byte) 73,
    (byte) 80 /*0x50*/,
    (byte) 123,
    (byte) 117,
    (byte) 187,
    (byte) 87,
    (byte) 193,
    (byte) 82,
    (byte) 5,
    (byte) 73,
    (byte) 39,
    (byte) 35,
    (byte) 216,
    (byte) 239,
    (byte) 28,
    (byte) 109,
    (byte) 141,
    (byte) 220,
    (byte) 112 /*0x70*/,
    (byte) 64 /*0x40*/,
    (byte) 55,
    (byte) 161,
    (byte) 239,
    (byte) 60,
    (byte) 56,
    (byte) 205,
    (byte) 175,
    (byte) 254,
    (byte) 161,
    (byte) 235,
    (byte) 5,
    (byte) 141,
    (byte) 38,
    (byte) 55,
    (byte) 152,
    (byte) 244,
    (byte) 156,
    (byte) 236,
    (byte) 244,
    (byte) 20,
    (byte) 154,
    (byte) 42,
    (byte) 56,
    (byte) 182,
    (byte) 205,
    (byte) 159,
    (byte) 232,
    (byte) 238,
    (byte) 158,
    (byte) 178,
    (byte) 74,
    (byte) 65,
    (byte) 71,
    (byte) 106,
    (byte) 182,
    (byte) 182,
    (byte) 153,
    (byte) 150,
    (byte) 187,
    (byte) 17,
    (byte) 114,
    (byte) 150,
    (byte) 168,
    (byte) 19,
    (byte) 52,
    (byte) 175,
    (byte) 60,
    (byte) 236,
    (byte) 46,
    (byte) 36,
    (byte) 72,
    (byte) 28,
    (byte) 218,
    (byte) 57,
    (byte) 100,
    (byte) 235,
    (byte) 160 /*0xA0*/,
    (byte) 16 /*0x10*/,
    (byte) 111,
    (byte) 215,
    (byte) 64 /*0x40*/,
    (byte) 250,
    (byte) 185,
    (byte) 253,
    (byte) 211,
    (byte) 11,
    (byte) 155,
    (byte) 222,
    (byte) 253,
    (byte) 84,
    (byte) 11,
    (byte) 56,
    (byte) 176 /*0xB0*/,
    (byte) 49,
    (byte) 73,
    (byte) 190,
    (byte) 177,
    (byte) 54,
    (byte) 66,
    (byte) 87,
    (byte) 92,
    (byte) 159,
    (byte) 59,
    (byte) 40,
    (byte) 243,
    (byte) 106,
    (byte) 18,
    (byte) 147,
    (byte) 189,
    (byte) 59,
    (byte) 201,
    (byte) 156,
    (byte) 136,
    (byte) 36,
    (byte) 206,
    (byte) 117,
    (byte) 133,
    (byte) 241,
    (byte) 17,
    (byte) 129,
    (byte) 153,
    (byte) 8,
    (byte) 75,
    (byte) 28,
    (byte) 222,
    (byte) 38,
    (byte) 135,
    (byte) 41,
    (byte) 80 /*0x50*/,
    (byte) 150,
    (byte) 60,
    (byte) 221,
    (byte) 182,
    (byte) 13,
    (byte) 131,
    (byte) 54,
    (byte) 76,
    (byte) 241,
    (byte) 133,
    (byte) 99,
    (byte) 158,
    (byte) 112 /*0x70*/,
    (byte) 25,
    (byte) 74,
    (byte) 26,
    (byte) 76,
    (byte) 207,
    (byte) 116,
    (byte) 136,
    (byte) 137,
    (byte) 169,
    (byte) 191,
    (byte) 103,
    (byte) 247,
    (byte) 5,
    (byte) 78,
    (byte) 18,
    (byte) 241,
    (byte) 220,
    (byte) 171,
    (byte) 10,
    (byte) 38,
    (byte) 89,
    (byte) 190,
    (byte) 147,
    (byte) 185,
    (byte) 202,
    (byte) 206,
    (byte) 83,
    (byte) 206,
    (byte) 166,
    (byte) 86,
    (byte) 232,
    (byte) 79,
    (byte) 124,
    (byte) 104,
    (byte) 117,
    (byte) 163,
    (byte) 232,
    (byte) 193,
    (byte) 190,
    (byte) 226,
    (byte) 175,
    (byte) 152,
    (byte) 70,
    (byte) 218,
    (byte) 254,
    (byte) 231,
    (byte) 185
  };

  internal static string ssp_appserver_12314()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[1] = (byte) 140;
      numArray2[5] = (byte) 178;
      numArray2[2] = (byte) 104;
      numArray2[3] = (byte) 211;
      numArray2[0] = (byte) 146;
      numArray2[7] = (byte) 12;
      numArray2[4] = (byte) 163;
      numArray2[8] = (byte) 133;
      numArray2[6] = (byte) 143;
      numArray2[9] = (byte) 84;
      byte[] numArray3 = new byte[10]
      {
        (byte) 232,
        (byte) 197,
        (byte) 40,
        (byte) 139,
        (byte) 161,
        (byte) 233,
        (byte) 105,
        (byte) 124,
        (byte) 58,
        (byte) 168
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 219;
    numArray5[5] = (byte) 42;
    numArray5[3] = (byte) 5;
    numArray5[6] = (byte) 23;
    numArray5[4] = (byte) 228;
    numArray5[2] = (byte) 125;
    numArray5[0] = (byte) 211;
    numArray5[1] = (byte) 216;
    numArray5[8] = (byte) 25;
    numArray5[9] = (byte) 75;
    byte[] numArray6 = new byte[10]
    {
      (byte) 229,
      (byte) 22,
      (byte) 24,
      (byte) 222,
      (byte) 146,
      (byte) 149,
      (byte) 231,
      (byte) 30,
      (byte) 26,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[21];
    byte[] response = new byte[21];
    Array.Copy((Array) sc_12313.sspq, 0, (Array) numArray7, 0, 21);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12313.sspr, 0, (Array) numArray7, 0, 21);
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

  internal static string ssp_appserver_12315()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 150,
        (byte) 64 /*0x40*/,
        (byte) 84,
        (byte) 225,
        (byte) 130,
        (byte) 204,
        (byte) 1,
        (byte) 179,
        (byte) 153,
        (byte) 148
      };
      byte[] numArray3 = new byte[10];
      numArray3[9] = (byte) 229;
      numArray3[4] = (byte) 120;
      numArray3[8] = (byte) 186;
      numArray3[3] = (byte) 106;
      numArray3[2] = (byte) 190;
      numArray3[5] = (byte) 81;
      numArray3[6] = (byte) 7;
      numArray3[0] = (byte) 97;
      numArray3[1] = (byte) 62;
      numArray3[7] = (byte) 115;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[29];
      byte[] response = new byte[29];
      Array.Copy((Array) sc_12313.sspq, 21, (Array) numArray4, 0, 29);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12313.sspr, 21, (Array) numArray4, 0, 29);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10]
    {
      (byte) 71,
      (byte) 233,
      (byte) 160 /*0xA0*/,
      (byte) 252,
      (byte) 238,
      (byte) 109,
      (byte) 92,
      (byte) 137,
      (byte) 75,
      (byte) 21
    };
    byte[] numArray7 = new byte[10];
    numArray7[8] = (byte) 240 /*0xF0*/;
    numArray7[0] = (byte) 198;
    numArray7[2] = (byte) 70;
    numArray7[6] = (byte) 136;
    numArray7[5] = (byte) 16 /*0x10*/;
    numArray7[4] = (byte) 79;
    numArray7[9] = (byte) 92;
    numArray7[7] = (byte) 53;
    numArray7[3] = (byte) 98;
    numArray7[1] = (byte) 113;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12316()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43];
      numArray2[17] = (byte) 249;
      numArray2[39] = (byte) 60;
      numArray2[2] = (byte) 33;
      numArray2[3] = (byte) 33;
      numArray2[0] = (byte) 67;
      numArray2[5] = (byte) 14;
      numArray2[41] = (byte) 148;
      numArray2[29] = (byte) 131;
      numArray2[8] = (byte) 97;
      numArray2[42] = (byte) 125;
      numArray2[10] = (byte) 251;
      numArray2[1] = (byte) 225;
      numArray2[24] = (byte) 43;
      numArray2[13] = (byte) 178;
      numArray2[12] = (byte) 9;
      numArray2[40] = (byte) 92;
      numArray2[4] = (byte) 59;
      numArray2[18] = (byte) 81;
      numArray2[14] = (byte) 76;
      numArray2[19] = (byte) 11;
      numArray2[20] = (byte) 71;
      numArray2[38] = (byte) 78;
      numArray2[22] = (byte) 172;
      numArray2[15] = (byte) 186;
      numArray2[9] = (byte) 215;
      numArray2[25] = (byte) 13;
      numArray2[37] = (byte) 58;
      numArray2[27] = (byte) 150;
      numArray2[28] = (byte) 249;
      numArray2[16 /*0x10*/] = (byte) 6;
      numArray2[11] = (byte) 229;
      numArray2[31 /*0x1F*/] = (byte) 195;
      numArray2[32 /*0x20*/] = (byte) 102;
      numArray2[33] = (byte) 181;
      numArray2[21] = (byte) 33;
      numArray2[35] = (byte) 12;
      numArray2[7] = (byte) 48 /*0x30*/;
      numArray2[34] = (byte) 185;
      numArray2[30] = (byte) 78;
      numArray2[6] = (byte) 185;
      numArray2[23] = (byte) 192 /*0xC0*/;
      numArray2[26] = (byte) 29;
      numArray2[36] = (byte) 75;
      byte[] numArray3 = new byte[43]
      {
        (byte) 35,
        (byte) 195,
        (byte) 177,
        (byte) 219,
        (byte) 46,
        (byte) 222,
        (byte) 135,
        (byte) 246,
        (byte) 237,
        (byte) 191,
        (byte) 143,
        (byte) 124,
        (byte) 213,
        (byte) 196,
        (byte) 112 /*0x70*/,
        (byte) 45,
        (byte) 37,
        (byte) 103,
        (byte) 57,
        (byte) 30,
        (byte) 235,
        (byte) 30,
        (byte) 154,
        (byte) 24,
        (byte) 139,
        (byte) 194,
        (byte) 123,
        (byte) 101,
        (byte) 122,
        (byte) 119,
        (byte) 17,
        (byte) 73,
        (byte) 100,
        (byte) 50,
        (byte) 19,
        (byte) 22,
        (byte) 54,
        (byte) 37,
        (byte) 44,
        (byte) 58,
        (byte) 231,
        (byte) 122,
        (byte) 226
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 152,
      (byte) 214,
      (byte) 234,
      (byte) 68,
      (byte) 31 /*0x1F*/,
      (byte) 76,
      (byte) 57,
      (byte) 93,
      (byte) 159,
      (byte) 119,
      (byte) 94,
      (byte) 117,
      (byte) 155,
      (byte) 223,
      (byte) 193,
      (byte) 130,
      (byte) 254,
      (byte) 212,
      (byte) 29,
      (byte) 201,
      (byte) 240 /*0xF0*/,
      (byte) 124,
      (byte) 138,
      (byte) 53,
      (byte) 190,
      (byte) 17,
      (byte) 171,
      (byte) 142,
      (byte) 47,
      (byte) 37,
      (byte) 186,
      (byte) 165,
      (byte) 144 /*0x90*/,
      (byte) 65,
      (byte) 96 /*0x60*/,
      (byte) 105,
      (byte) 132,
      (byte) 135,
      (byte) 139,
      (byte) 200,
      (byte) 116,
      (byte) 85,
      (byte) 5
    };
    byte[] numArray6 = new byte[43]
    {
      (byte) 150,
      (byte) 208 /*0xD0*/,
      (byte) 70,
      (byte) 94,
      (byte) 162,
      (byte) 247,
      (byte) 143,
      (byte) 2,
      (byte) 242,
      (byte) 232,
      (byte) 59,
      (byte) 54,
      (byte) 45,
      (byte) 25,
      (byte) 206,
      (byte) 34,
      (byte) 222,
      (byte) 34,
      (byte) 146,
      (byte) 157,
      (byte) 225,
      (byte) 1,
      (byte) 44,
      (byte) 114,
      (byte) 228,
      (byte) 107,
      (byte) 78,
      (byte) 92,
      (byte) 63 /*0x3F*/,
      (byte) 152,
      (byte) 221,
      (byte) 247,
      (byte) 18,
      (byte) 91,
      (byte) 24,
      (byte) 61,
      (byte) 11,
      (byte) 174,
      (byte) 37,
      (byte) 96 /*0x60*/,
      (byte) 77,
      (byte) 150,
      (byte) 180
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12317()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[10] = (byte) 180;
      numArray2[14] = (byte) 171;
      numArray2[1] = (byte) 210;
      numArray2[8] = (byte) 27;
      numArray2[12] = (byte) 13;
      numArray2[5] = (byte) 116;
      numArray2[6] = (byte) 36;
      numArray2[7] = (byte) 33;
      numArray2[2] = (byte) 133;
      numArray2[9] = (byte) 76;
      numArray2[15] = (byte) 46;
      numArray2[3] = (byte) 186;
      numArray2[13] = (byte) 160 /*0xA0*/;
      numArray2[0] = (byte) 101;
      numArray2[17] = (byte) 189;
      numArray2[18] = (byte) 185;
      numArray2[11] = (byte) 134;
      numArray2[16 /*0x10*/] = (byte) 122;
      numArray2[4] = (byte) 250;
      byte[] numArray3 = new byte[19]
      {
        (byte) 184,
        (byte) 95,
        (byte) 72,
        (byte) 57,
        (byte) 124,
        (byte) 243,
        (byte) 38,
        (byte) 168,
        (byte) 7,
        (byte) 175,
        (byte) 35,
        (byte) 94,
        (byte) 246,
        (byte) 71,
        (byte) 2,
        (byte) 133,
        (byte) 212,
        (byte) 230,
        (byte) 213
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_12313.sspq, 50, (Array) numArray4, 0, 45);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12313.sspr, 50, (Array) numArray4, 0, 45);
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
    byte[] numArray6 = new byte[19]
    {
      (byte) 30,
      (byte) 105,
      (byte) 95,
      (byte) 65,
      (byte) 34,
      (byte) 38,
      (byte) 14,
      (byte) 17,
      (byte) 156,
      (byte) 152,
      (byte) 161,
      (byte) 38,
      (byte) 132,
      (byte) 118,
      (byte) 1,
      (byte) 205,
      (byte) 217,
      (byte) 162,
      (byte) 123
    };
    byte[] numArray7 = new byte[19];
    numArray7[17] = (byte) 85;
    numArray7[1] = (byte) 86;
    numArray7[2] = (byte) 7;
    numArray7[0] = (byte) 223;
    numArray7[5] = (byte) 37;
    numArray7[12] = (byte) 164;
    numArray7[11] = (byte) 97;
    numArray7[14] = (byte) 118;
    numArray7[8] = (byte) 58;
    numArray7[6] = (byte) 156;
    numArray7[10] = (byte) 14;
    numArray7[16 /*0x10*/] = (byte) 155;
    numArray7[18] = (byte) 81;
    numArray7[13] = (byte) 227;
    numArray7[9] = (byte) 42;
    numArray7[7] = (byte) 161;
    numArray7[3] = (byte) 135;
    numArray7[4] = (byte) 130;
    numArray7[15] = (byte) 202;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12318()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 220,
        (byte) 161,
        (byte) 152,
        (byte) 20,
        (byte) 170,
        (byte) 254,
        (byte) 200,
        (byte) 95,
        (byte) 178,
        (byte) 208 /*0xD0*/
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 231,
        (byte) 121,
        (byte) 55,
        (byte) 220,
        (byte) 244,
        (byte) 241,
        (byte) 155,
        (byte) 135,
        (byte) 227,
        (byte) 155
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[6] = (byte) 39;
    numArray5[0] = (byte) 185;
    numArray5[2] = (byte) 179;
    numArray5[7] = (byte) 30;
    numArray5[4] = (byte) 202;
    numArray5[5] = (byte) 87;
    numArray5[1] = (byte) 124;
    numArray5[3] = (byte) 2;
    numArray5[9] = (byte) 132;
    numArray5[8] = (byte) 88;
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 87;
    numArray6[1] = byte.MaxValue;
    numArray6[0] = (byte) 215;
    numArray6[7] = (byte) 178;
    numArray6[4] = (byte) 56;
    numArray6[3] = (byte) 39;
    numArray6[6] = (byte) 7;
    numArray6[5] = (byte) 155;
    numArray6[8] = (byte) 218;
    numArray6[2] = (byte) 252;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12319()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 178,
        (byte) 149,
        (byte) 71,
        (byte) 171,
        (byte) 152,
        (byte) 146,
        (byte) 199,
        (byte) 88,
        (byte) 132,
        (byte) 28
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 248,
        (byte) 85,
        (byte) 85,
        (byte) 29,
        (byte) 165,
        (byte) 161,
        (byte) 89,
        (byte) 153,
        (byte) 89,
        (byte) 192 /*0xC0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[4] = (byte) 144 /*0x90*/;
    numArray5[9] = (byte) 49;
    numArray5[1] = (byte) 217;
    numArray5[8] = (byte) 50;
    numArray5[0] = (byte) 243;
    numArray5[5] = (byte) 224 /*0xE0*/;
    numArray5[6] = (byte) 222;
    numArray5[7] = (byte) 187;
    numArray5[2] = (byte) 141;
    numArray5[3] = (byte) 135;
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 178;
    numArray6[8] = (byte) 200;
    numArray6[3] = (byte) 77;
    numArray6[0] = (byte) 249;
    numArray6[6] = (byte) 249;
    numArray6[5] = (byte) 245;
    numArray6[1] = (byte) 180;
    numArray6[7] = (byte) 199;
    numArray6[4] = (byte) 147;
    numArray6[9] = (byte) 177;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[12];
    byte[] response = new byte[12];
    Array.Copy((Array) sc_12313.sspq, 95, (Array) numArray7, 0, 12);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12313.sspr, 95, (Array) numArray7, 0, 12);
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

  internal static string ssp_appserver_12320()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43];
      numArray2[9] = (byte) 128 /*0x80*/;
      numArray2[1] = (byte) 190;
      numArray2[2] = (byte) 204;
      numArray2[3] = (byte) 189;
      numArray2[11] = (byte) 7;
      numArray2[37] = (byte) 69;
      numArray2[6] = (byte) 159;
      numArray2[5] = (byte) 11;
      numArray2[27] = (byte) 149;
      numArray2[41] = (byte) 225;
      numArray2[0] = (byte) 11;
      numArray2[40] = (byte) 134;
      numArray2[12] = (byte) 232;
      numArray2[16 /*0x10*/] = (byte) 46;
      numArray2[4] = (byte) 218;
      numArray2[8] = (byte) 154;
      numArray2[28] = (byte) 254;
      numArray2[17] = (byte) 3;
      numArray2[29] = (byte) 216;
      numArray2[19] = (byte) 179;
      numArray2[35] = (byte) 2;
      numArray2[10] = (byte) 1;
      numArray2[22] = (byte) 196;
      numArray2[31 /*0x1F*/] = (byte) 228;
      numArray2[24] = (byte) 188;
      numArray2[23] = (byte) 136;
      numArray2[14] = (byte) 29;
      numArray2[26] = (byte) 62;
      numArray2[18] = (byte) 59;
      numArray2[25] = (byte) 144 /*0x90*/;
      numArray2[30] = (byte) 245;
      numArray2[39] = (byte) 89;
      numArray2[32 /*0x20*/] = (byte) 174;
      numArray2[33] = (byte) 44;
      numArray2[34] = (byte) 242;
      numArray2[13] = (byte) 225;
      numArray2[36] = (byte) 236;
      numArray2[21] = (byte) 170;
      numArray2[38] = (byte) 84;
      numArray2[15] = (byte) 3;
      numArray2[20] = (byte) 137;
      numArray2[7] = (byte) 177;
      numArray2[42] = (byte) 65;
      byte[] numArray3 = new byte[43];
      numArray3[23] = (byte) 230;
      numArray3[39] = (byte) 191;
      numArray3[37] = (byte) 89;
      numArray3[1] = (byte) 101;
      numArray3[27] = (byte) 193;
      numArray3[20] = (byte) 239;
      numArray3[15] = (byte) 29;
      numArray3[7] = (byte) 248;
      numArray3[5] = (byte) 94;
      numArray3[38] = (byte) 249;
      numArray3[10] = (byte) 127 /*0x7F*/;
      numArray3[34] = (byte) 142;
      numArray3[12] = (byte) 3;
      numArray3[3] = (byte) 149;
      numArray3[40] = (byte) 40;
      numArray3[17] = (byte) 140;
      numArray3[16 /*0x10*/] = (byte) 214;
      numArray3[2] = (byte) 111;
      numArray3[18] = (byte) 223;
      numArray3[19] = (byte) 114;
      numArray3[13] = (byte) 39;
      numArray3[24] = (byte) 112 /*0x70*/;
      numArray3[22] = (byte) 46;
      numArray3[21] = (byte) 174;
      numArray3[9] = (byte) 122;
      numArray3[30] = (byte) 164;
      numArray3[26] = (byte) 20;
      numArray3[11] = (byte) 167;
      numArray3[28] = (byte) 251;
      numArray3[4] = (byte) 203;
      numArray3[29] = (byte) 49;
      numArray3[31 /*0x1F*/] = (byte) 107;
      numArray3[32 /*0x20*/] = (byte) 97;
      numArray3[33] = (byte) 27;
      numArray3[0] = (byte) 146;
      numArray3[35] = (byte) 213;
      numArray3[36] = (byte) 244;
      numArray3[14] = (byte) 162;
      numArray3[25] = (byte) 227;
      numArray3[6] = (byte) 103;
      numArray3[8] = (byte) 12;
      numArray3[41] = (byte) 197;
      numArray3[42] = (byte) 232;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[16 /*0x10*/] = (byte) 134;
    numArray5[1] = (byte) 159;
    numArray5[18] = (byte) 86;
    numArray5[2] = (byte) 14;
    numArray5[10] = (byte) 158;
    numArray5[4] = (byte) 37;
    numArray5[36] = (byte) 57;
    numArray5[20] = (byte) 68;
    numArray5[17] = (byte) 121;
    numArray5[28] = (byte) 196;
    numArray5[24] = (byte) 138;
    numArray5[13] = (byte) 16 /*0x10*/;
    numArray5[22] = (byte) 64 /*0x40*/;
    numArray5[29] = (byte) 11;
    numArray5[14] = (byte) 233;
    numArray5[0] = (byte) 200;
    numArray5[7] = (byte) 31 /*0x1F*/;
    numArray5[40] = (byte) 215;
    numArray5[11] = (byte) 238;
    numArray5[19] = (byte) 17;
    numArray5[34] = (byte) 48 /*0x30*/;
    numArray5[21] = (byte) 65;
    numArray5[35] = (byte) 31 /*0x1F*/;
    numArray5[5] = (byte) 77;
    numArray5[31 /*0x1F*/] = (byte) 65;
    numArray5[25] = (byte) 55;
    numArray5[26] = (byte) 133;
    numArray5[27] = (byte) 227;
    numArray5[15] = (byte) 252;
    numArray5[9] = (byte) 239;
    numArray5[30] = (byte) 50;
    numArray5[23] = (byte) 224 /*0xE0*/;
    numArray5[32 /*0x20*/] = (byte) 210;
    numArray5[33] = (byte) 15;
    numArray5[6] = (byte) 69;
    numArray5[3] = (byte) 185;
    numArray5[12] = (byte) 152;
    numArray5[37] = (byte) 201;
    numArray5[38] = (byte) 237;
    numArray5[39] = (byte) 216;
    numArray5[8] = (byte) 227;
    numArray5[41] = (byte) 27;
    numArray5[42] = (byte) 19;
    byte[] numArray6 = new byte[43];
    numArray6[8] = (byte) 163;
    numArray6[9] = (byte) 68;
    numArray6[2] = (byte) 209;
    numArray6[10] = (byte) 155;
    numArray6[4] = (byte) 104;
    numArray6[0] = (byte) 254;
    numArray6[5] = (byte) 203;
    numArray6[17] = (byte) 128 /*0x80*/;
    numArray6[16 /*0x10*/] = (byte) 220;
    numArray6[37] = (byte) 162;
    numArray6[12] = (byte) 4;
    numArray6[11] = (byte) 89;
    numArray6[30] = (byte) 176 /*0xB0*/;
    numArray6[7] = (byte) 61;
    numArray6[20] = (byte) 205;
    numArray6[15] = (byte) 10;
    numArray6[3] = (byte) 228;
    numArray6[24] = (byte) 43;
    numArray6[31 /*0x1F*/] = (byte) 215;
    numArray6[6] = (byte) 233;
    numArray6[23] = (byte) 70;
    numArray6[21] = (byte) 60;
    numArray6[22] = (byte) 110;
    numArray6[13] = (byte) 42;
    numArray6[42] = (byte) 110;
    numArray6[25] = (byte) 28;
    numArray6[19] = (byte) 195;
    numArray6[26] = (byte) 114;
    numArray6[18] = (byte) 101;
    numArray6[29] = (byte) 68;
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[39] = (byte) 172;
    numArray6[32 /*0x20*/] = (byte) 231;
    numArray6[33] = (byte) 201;
    numArray6[14] = (byte) 243;
    numArray6[28] = (byte) 74;
    numArray6[36] = (byte) 90;
    numArray6[27] = (byte) 163;
    numArray6[38] = (byte) 176 /*0xB0*/;
    numArray6[34] = (byte) 44;
    numArray6[40] = (byte) 47;
    numArray6[41] = (byte) 26;
    numArray6[35] = (byte) 175;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12321()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 231,
        (byte) 125,
        (byte) 53,
        (byte) 100,
        (byte) 165,
        (byte) 54,
        (byte) 137,
        (byte) 5,
        (byte) 12,
        (byte) 61,
        (byte) 241,
        (byte) 78,
        (byte) 235,
        (byte) 146,
        (byte) 96 /*0x60*/,
        (byte) 131,
        (byte) 207,
        (byte) 115,
        (byte) 129
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 107,
        (byte) 62,
        (byte) 29,
        (byte) 141,
        (byte) 214,
        (byte) 117,
        (byte) 246,
        (byte) 87,
        (byte) 243,
        (byte) 215,
        (byte) 253,
        (byte) 92,
        (byte) 232,
        (byte) 8,
        (byte) 102,
        (byte) 14,
        (byte) 15,
        byte.MaxValue,
        (byte) 101
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[17] = (byte) 8;
    numArray5[5] = (byte) 64 /*0x40*/;
    numArray5[11] = (byte) 67;
    numArray5[3] = (byte) 175;
    numArray5[4] = (byte) 45;
    numArray5[18] = (byte) 98;
    numArray5[8] = (byte) 104;
    numArray5[7] = (byte) 110;
    numArray5[9] = (byte) 17;
    numArray5[15] = (byte) 221;
    numArray5[10] = (byte) 102;
    numArray5[1] = (byte) 32 /*0x20*/;
    numArray5[12] = (byte) 145;
    numArray5[13] = (byte) 188;
    numArray5[14] = (byte) 70;
    numArray5[2] = (byte) 232;
    numArray5[0] = (byte) 199;
    numArray5[6] = (byte) 195;
    numArray5[16 /*0x10*/] = (byte) 92;
    byte[] numArray6 = new byte[19]
    {
      (byte) 114,
      (byte) 72,
      (byte) 57,
      (byte) 65,
      (byte) 43,
      (byte) 209,
      (byte) 12,
      (byte) 180,
      (byte) 21,
      (byte) 37,
      (byte) 103,
      (byte) 179,
      (byte) 209,
      (byte) 182,
      (byte) 21,
      (byte) 211,
      (byte) 119,
      (byte) 145,
      (byte) 238
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12322()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[0] = (byte) 213;
      numArray2[1] = (byte) 223;
      numArray2[2] = (byte) 232;
      numArray2[4] = (byte) 0;
      numArray2[3] = (byte) 167;
      numArray2[5] = (byte) 164;
      numArray2[8] = (byte) 22;
      numArray2[6] = (byte) 61;
      numArray2[7] = (byte) 241;
      numArray2[10] = (byte) 235;
      numArray2[9] = (byte) 120;
      numArray2[11] = (byte) 48 /*0x30*/;
      byte[] numArray3 = new byte[12]
      {
        (byte) 167,
        (byte) 223,
        (byte) 165,
        (byte) 133,
        (byte) 68,
        (byte) 193,
        (byte) 204,
        (byte) 109,
        (byte) 132,
        (byte) 119,
        (byte) 14,
        (byte) 93
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 48 /*0x30*/,
      (byte) 98,
      (byte) 102,
      byte.MaxValue,
      (byte) 25,
      (byte) 32 /*0x20*/,
      (byte) 203,
      (byte) 157,
      (byte) 75,
      (byte) 0,
      (byte) 119,
      (byte) 36
    };
    byte[] numArray6 = new byte[12]
    {
      (byte) 127 /*0x7F*/,
      (byte) 247,
      (byte) 1,
      (byte) 175,
      (byte) 142,
      (byte) 135,
      (byte) 155,
      (byte) 178,
      (byte) 148,
      (byte) 64 /*0x40*/,
      (byte) 115,
      (byte) 113
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12323()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 98;
      numArray2[0] = (byte) 111;
      numArray2[2] = (byte) 206;
      numArray2[3] = (byte) 159;
      numArray2[5] = (byte) 32 /*0x20*/;
      numArray2[6] = (byte) 123;
      numArray2[7] = (byte) 101;
      numArray2[8] = (byte) 0;
      numArray2[1] = (byte) 185;
      numArray2[9] = (byte) 202;
      byte[] numArray3 = new byte[10]
      {
        (byte) 137,
        (byte) 133,
        (byte) 128 /*0x80*/,
        (byte) 47,
        (byte) 16 /*0x10*/,
        (byte) 134,
        (byte) 119,
        (byte) 138,
        (byte) 227,
        (byte) 128 /*0x80*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 16 /*0x10*/,
      (byte) 35,
      (byte) 181,
      (byte) 168,
      byte.MaxValue,
      (byte) 25,
      (byte) 227,
      (byte) 97,
      (byte) 239,
      (byte) 35
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 1,
      (byte) 210,
      (byte) 8,
      (byte) 189,
      (byte) 71,
      (byte) 223,
      (byte) 40,
      (byte) 22,
      (byte) 82,
      (byte) 141
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12324()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 99,
        (byte) 163,
        (byte) 15,
        (byte) 231,
        (byte) 1,
        byte.MaxValue,
        (byte) 78,
        (byte) 153,
        (byte) 253,
        (byte) 223
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 19;
      numArray3[3] = (byte) 14;
      numArray3[2] = (byte) 55;
      numArray3[9] = (byte) 223;
      numArray3[4] = (byte) 81;
      numArray3[5] = (byte) 26;
      numArray3[6] = (byte) 89;
      numArray3[7] = (byte) 74;
      numArray3[8] = (byte) 149;
      numArray3[0] = (byte) 20;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[52];
      byte[] response = new byte[52];
      Array.Copy((Array) sc_12313.sspq, 107, (Array) numArray4, 0, 52);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12313.sspr, 107, (Array) numArray4, 0, 52);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10]
    {
      (byte) 86,
      (byte) 214,
      (byte) 100,
      (byte) 85,
      (byte) 69,
      (byte) 96 /*0x60*/,
      (byte) 134,
      (byte) 101,
      (byte) 164,
      (byte) 141
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 39,
      (byte) 119,
      (byte) 94,
      (byte) 242,
      (byte) 10,
      (byte) 141,
      (byte) 33,
      (byte) 246,
      (byte) 80 /*0x50*/,
      (byte) 120
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12325()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[3] = (byte) 249;
      numArray2[5] = (byte) 158;
      numArray2[1] = (byte) 101;
      numArray2[6] = (byte) 116;
      numArray2[4] = (byte) 151;
      numArray2[0] = (byte) 2;
      numArray2[8] = (byte) 215;
      numArray2[7] = (byte) 210;
      numArray2[2] = (byte) 60;
      numArray2[9] = (byte) 122;
      byte[] numArray3 = new byte[10]
      {
        (byte) 143,
        (byte) 128 /*0x80*/,
        (byte) 109,
        (byte) 140,
        (byte) 1,
        (byte) 31 /*0x1F*/,
        (byte) 143,
        (byte) 92,
        (byte) 234,
        (byte) 144 /*0x90*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 20,
      (byte) 111,
      (byte) 130,
      (byte) 95,
      (byte) 157,
      (byte) 174,
      (byte) 112 /*0x70*/,
      (byte) 97,
      (byte) 183,
      (byte) 241
    };
    byte[] numArray6 = new byte[10];
    numArray6[0] = (byte) 76;
    numArray6[2] = (byte) 13;
    numArray6[9] = (byte) 225;
    numArray6[3] = (byte) 15;
    numArray6[1] = (byte) 72;
    numArray6[5] = (byte) 203;
    numArray6[8] = (byte) 25;
    numArray6[7] = (byte) 69;
    numArray6[6] = (byte) 19;
    numArray6[4] = (byte) 42;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12326()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/]
      {
        (byte) 41,
        (byte) 244,
        (byte) 22,
        (byte) 20,
        (byte) 238,
        (byte) 67,
        (byte) 196,
        (byte) 32 /*0x20*/,
        (byte) 226,
        (byte) 50,
        (byte) 39,
        (byte) 137,
        (byte) 20,
        (byte) 63 /*0x3F*/,
        (byte) 215,
        (byte) 35,
        (byte) 103,
        (byte) 24,
        (byte) 182,
        (byte) 237,
        (byte) 79,
        (byte) 218,
        (byte) 22,
        (byte) 84,
        (byte) 152,
        (byte) 101,
        (byte) 248,
        (byte) 157,
        (byte) 13,
        (byte) 33,
        (byte) 73,
        (byte) 44,
        (byte) 9,
        (byte) 78,
        (byte) 53,
        (byte) 168,
        (byte) 152,
        (byte) 92,
        (byte) 115,
        (byte) 116,
        (byte) 44,
        (byte) 189,
        (byte) 160 /*0xA0*/,
        (byte) 40,
        (byte) 18,
        (byte) 87,
        (byte) 207,
        (byte) 132
      };
      byte[] numArray3 = new byte[48 /*0x30*/];
      numArray3[2] = (byte) 97;
      numArray3[1] = (byte) 223;
      numArray3[16 /*0x10*/] = (byte) 182;
      numArray3[3] = (byte) 32 /*0x20*/;
      numArray3[4] = (byte) 161;
      numArray3[27] = (byte) 249;
      numArray3[15] = (byte) 54;
      numArray3[7] = (byte) 244;
      numArray3[8] = (byte) 29;
      numArray3[13] = (byte) 219;
      numArray3[10] = (byte) 202;
      numArray3[9] = (byte) 241;
      numArray3[37] = (byte) 189;
      numArray3[33] = (byte) 241;
      numArray3[14] = (byte) 8;
      numArray3[26] = (byte) 68;
      numArray3[6] = (byte) 216;
      numArray3[5] = (byte) 113;
      numArray3[19] = (byte) 118;
      numArray3[35] = (byte) 164;
      numArray3[36] = byte.MaxValue;
      numArray3[21] = (byte) 190;
      numArray3[22] = (byte) 144 /*0x90*/;
      numArray3[23] = (byte) 170;
      numArray3[24] = (byte) 192 /*0xC0*/;
      numArray3[20] = (byte) 250;
      numArray3[11] = (byte) 140;
      numArray3[41] = (byte) 17;
      numArray3[38] = (byte) 102;
      numArray3[29] = (byte) 126;
      numArray3[30] = (byte) 157;
      numArray3[31 /*0x1F*/] = (byte) 0;
      numArray3[32 /*0x20*/] = (byte) 123;
      numArray3[42] = (byte) 123;
      numArray3[34] = (byte) 217;
      numArray3[18] = (byte) 41;
      numArray3[0] = (byte) 231;
      numArray3[39] = (byte) 137;
      numArray3[17] = (byte) 20;
      numArray3[25] = (byte) 31 /*0x1F*/;
      numArray3[40] = (byte) 11;
      numArray3[28] = (byte) 238;
      numArray3[45] = (byte) 248;
      numArray3[43] = (byte) 205;
      numArray3[44] = (byte) 199;
      numArray3[46] = (byte) 160 /*0xA0*/;
      numArray3[12] = (byte) 71;
      numArray3[47] = (byte) 99;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[48 /*0x30*/];
    byte[] numArray5 = new byte[48 /*0x30*/]
    {
      (byte) 23,
      (byte) 215,
      (byte) 9,
      (byte) 123,
      (byte) 204,
      (byte) 214,
      (byte) 140,
      (byte) 44,
      (byte) 239,
      (byte) 184,
      (byte) 81,
      (byte) 20,
      (byte) 241,
      (byte) 128 /*0x80*/,
      (byte) 59,
      (byte) 243,
      (byte) 215,
      (byte) 132,
      (byte) 101,
      (byte) 54,
      (byte) 111,
      byte.MaxValue,
      (byte) 240 /*0xF0*/,
      (byte) 91,
      (byte) 35,
      (byte) 100,
      (byte) 179,
      (byte) 43,
      (byte) 186,
      (byte) 25,
      (byte) 186,
      (byte) 231,
      (byte) 90,
      (byte) 172,
      (byte) 41,
      (byte) 105,
      (byte) 140,
      (byte) 248,
      (byte) 213,
      (byte) 214,
      (byte) 254,
      (byte) 76,
      (byte) 11,
      (byte) 215,
      (byte) 25,
      (byte) 209,
      (byte) 217,
      (byte) 86
    };
    byte[] numArray6 = new byte[48 /*0x30*/]
    {
      (byte) 28,
      (byte) 78,
      (byte) 223,
      (byte) 109,
      (byte) 87,
      (byte) 30,
      (byte) 125,
      (byte) 251,
      (byte) 127 /*0x7F*/,
      (byte) 21,
      (byte) 94,
      (byte) 118,
      (byte) 186,
      (byte) 137,
      (byte) 198,
      (byte) 48 /*0x30*/,
      (byte) 76,
      (byte) 134,
      (byte) 144 /*0x90*/,
      (byte) 237,
      (byte) 57,
      (byte) 144 /*0x90*/,
      (byte) 46,
      (byte) 54,
      (byte) 210,
      (byte) 164,
      (byte) 253,
      (byte) 40,
      (byte) 145,
      (byte) 191,
      (byte) 94,
      (byte) 33,
      (byte) 91,
      (byte) 158,
      (byte) 103,
      (byte) 221,
      (byte) 226,
      (byte) 34,
      (byte) 196,
      (byte) 164,
      (byte) 146,
      (byte) 23,
      (byte) 57,
      (byte) 235,
      (byte) 213,
      (byte) 102,
      (byte) 207,
      (byte) 141
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[50];
    byte[] response = new byte[50];
    Array.Copy((Array) sc_12313.sspq, 159, (Array) numArray7, 0, 50);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12313.sspr, 159, (Array) numArray7, 0, 50);
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

  internal static int ssp_appserver_12327(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 146;
    sourceArray1[1] = (byte) 113;
    sourceArray1[3] = (byte) 181;
    sourceArray1[28] = (byte) 64 /*0x40*/;
    sourceArray1[6] = (byte) 35;
    sourceArray1[2] = (byte) 211;
    sourceArray1[40] = (byte) 238;
    sourceArray1[7] = (byte) 205;
    sourceArray1[10] = (byte) 221;
    sourceArray1[9] = (byte) 6;
    sourceArray1[36] = (byte) 101;
    sourceArray1[11] = (byte) 197;
    sourceArray1[4] = (byte) 112 /*0x70*/;
    sourceArray1[13] = (byte) 174;
    sourceArray1[14] = (byte) 14;
    sourceArray1[31 /*0x1F*/] = (byte) 246;
    sourceArray1[34] = (byte) 50;
    sourceArray1[17] = (byte) 7;
    sourceArray1[46] = (byte) 195;
    sourceArray1[12] = (byte) 217;
    sourceArray1[20] = (byte) 80 /*0x50*/;
    sourceArray1[21] = (byte) 136;
    sourceArray1[42] = (byte) 114;
    sourceArray1[23] = (byte) 65;
    sourceArray1[45] = (byte) 238;
    sourceArray1[25] = (byte) 154;
    sourceArray1[26] = (byte) 72;
    sourceArray1[27] = (byte) 191;
    sourceArray1[38] = (byte) 64 /*0x40*/;
    sourceArray1[8] = (byte) 108;
    sourceArray1[30] = (byte) 57;
    sourceArray1[19] = (byte) 68;
    sourceArray1[32 /*0x20*/] = (byte) 91;
    sourceArray1[24] = (byte) 219;
    sourceArray1[47] = (byte) 44;
    sourceArray1[18] = (byte) 104;
    sourceArray1[35] = (byte) 87;
    sourceArray1[22] = (byte) 63 /*0x3F*/;
    sourceArray1[16 /*0x10*/] = (byte) 198;
    sourceArray1[39] = (byte) 47;
    sourceArray1[5] = (byte) 173;
    sourceArray1[41] = (byte) 180;
    sourceArray1[0] = (byte) 144 /*0x90*/;
    sourceArray1[43] = (byte) 154;
    sourceArray1[44] = (byte) 144 /*0x90*/;
    sourceArray1[33] = (byte) 181;
    sourceArray1[37] = (byte) 237;
    sourceArray1[15] = (byte) 197;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 162,
      (byte) 126,
      (byte) 166,
      (byte) 101,
      (byte) 27,
      (byte) 19,
      (byte) 223,
      (byte) 92,
      (byte) 109,
      (byte) 232,
      (byte) 240 /*0xF0*/,
      (byte) 32 /*0x20*/,
      (byte) 210,
      (byte) 197,
      (byte) 37,
      (byte) 73,
      (byte) 224 /*0xE0*/,
      (byte) 2,
      (byte) 58,
      (byte) 86,
      (byte) 252,
      (byte) 70,
      (byte) 153,
      (byte) 61,
      (byte) 251,
      (byte) 19,
      (byte) 138,
      (byte) 50,
      (byte) 156,
      (byte) 113,
      (byte) 242,
      (byte) 122,
      (byte) 68,
      (byte) 205,
      (byte) 12,
      (byte) 11,
      (byte) 218,
      (byte) 63 /*0x3F*/,
      (byte) 204,
      (byte) 157,
      (byte) 21,
      (byte) 87,
      (byte) 250,
      (byte) 21,
      (byte) 145,
      (byte) 18,
      (byte) 187,
      (byte) 236
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12328()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[38];
      byte[] numArray2 = new byte[38]
      {
        (byte) 132,
        (byte) 159,
        (byte) 176 /*0xB0*/,
        (byte) 111,
        (byte) 178,
        (byte) 16 /*0x10*/,
        (byte) 72,
        (byte) 243,
        (byte) 102,
        (byte) 229,
        (byte) 242,
        (byte) 141,
        (byte) 224 /*0xE0*/,
        (byte) 237,
        (byte) 254,
        (byte) 72,
        (byte) 203,
        (byte) 96 /*0x60*/,
        (byte) 47,
        (byte) 163,
        (byte) 28,
        (byte) 202,
        (byte) 239,
        (byte) 81,
        (byte) 145,
        (byte) 141,
        (byte) 196,
        (byte) 92,
        (byte) 110,
        (byte) 204,
        (byte) 117,
        (byte) 125,
        (byte) 215,
        (byte) 7,
        (byte) 103,
        (byte) 55,
        (byte) 107,
        (byte) 40
      };
      byte[] numArray3 = new byte[38];
      numArray3[19] = (byte) 156;
      numArray3[24] = (byte) 202;
      numArray3[2] = (byte) 231;
      numArray3[30] = (byte) 128 /*0x80*/;
      numArray3[27] = (byte) 241;
      numArray3[32 /*0x20*/] = (byte) 126;
      numArray3[15] = (byte) 122;
      numArray3[7] = (byte) 151;
      numArray3[26] = (byte) 120;
      numArray3[9] = (byte) 49;
      numArray3[0] = (byte) 214;
      numArray3[8] = (byte) 215;
      numArray3[12] = (byte) 153;
      numArray3[13] = (byte) 242;
      numArray3[14] = (byte) 136;
      numArray3[11] = (byte) 58;
      numArray3[16 /*0x10*/] = (byte) 44;
      numArray3[17] = (byte) 10;
      numArray3[18] = (byte) 113;
      numArray3[6] = (byte) 213;
      numArray3[20] = (byte) 78;
      numArray3[22] = (byte) 34;
      numArray3[5] = (byte) 102;
      numArray3[21] = (byte) 144 /*0x90*/;
      numArray3[33] = (byte) 126;
      numArray3[28] = (byte) 84;
      numArray3[1] = (byte) 208 /*0xD0*/;
      numArray3[23] = (byte) 116;
      numArray3[25] = (byte) 72;
      numArray3[29] = (byte) 148;
      numArray3[34] = (byte) 245;
      numArray3[31 /*0x1F*/] = (byte) 139;
      numArray3[4] = (byte) 153;
      numArray3[3] = (byte) 93;
      numArray3[10] = (byte) 193;
      numArray3[35] = (byte) 172;
      numArray3[36] = (byte) 126;
      numArray3[37] = (byte) 145;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[38];
    byte[] numArray5 = new byte[38];
    numArray5[14] = (byte) 86;
    numArray5[35] = (byte) 138;
    numArray5[0] = (byte) 250;
    numArray5[1] = (byte) 154;
    numArray5[3] = (byte) 239;
    numArray5[5] = (byte) 14;
    numArray5[29] = (byte) 103;
    numArray5[19] = (byte) 224 /*0xE0*/;
    numArray5[8] = (byte) 172;
    numArray5[15] = (byte) 78;
    numArray5[10] = (byte) 173;
    numArray5[11] = (byte) 135;
    numArray5[6] = (byte) 175;
    numArray5[13] = (byte) 74;
    numArray5[22] = (byte) 25;
    numArray5[2] = (byte) 158;
    numArray5[16 /*0x10*/] = (byte) 243;
    numArray5[17] = (byte) 82;
    numArray5[26] = (byte) 27;
    numArray5[4] = (byte) 236;
    numArray5[20] = (byte) 195;
    numArray5[36] = (byte) 172;
    numArray5[25] = (byte) 165;
    numArray5[9] = (byte) 122;
    numArray5[12] = (byte) 13;
    numArray5[18] = (byte) 158;
    numArray5[32 /*0x20*/] = (byte) 16 /*0x10*/;
    numArray5[27] = (byte) 225;
    numArray5[24] = (byte) 165;
    numArray5[28] = (byte) 210;
    numArray5[21] = (byte) 225;
    numArray5[31 /*0x1F*/] = (byte) 40;
    numArray5[30] = (byte) 71;
    numArray5[33] = (byte) 68;
    numArray5[34] = (byte) 227;
    numArray5[7] = (byte) 21;
    numArray5[23] = (byte) 159;
    numArray5[37] = (byte) 252;
    byte[] numArray6 = new byte[38]
    {
      (byte) 206,
      (byte) 186,
      (byte) 251,
      (byte) 250,
      (byte) 68,
      (byte) 133,
      (byte) 45,
      (byte) 182,
      (byte) 33,
      (byte) 33,
      (byte) 214,
      (byte) 185,
      (byte) 134,
      (byte) 91,
      (byte) 115,
      (byte) 64 /*0x40*/,
      (byte) 101,
      (byte) 31 /*0x1F*/,
      (byte) 142,
      (byte) 19,
      (byte) 66,
      (byte) 31 /*0x1F*/,
      (byte) 60,
      (byte) 85,
      (byte) 249,
      (byte) 134,
      (byte) 118,
      (byte) 242,
      (byte) 219,
      (byte) 71,
      (byte) 216,
      (byte) 229,
      (byte) 114,
      (byte) 198,
      (byte) 52,
      (byte) 238,
      (byte) 81,
      (byte) 23
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 38);
    for (int index = 0; index < 38; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[21];
    byte[] response = new byte[21];
    Array.Copy((Array) sc_12313.sspq, 209, (Array) numArray7, 0, 21);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12313.sspr, 209, (Array) numArray7, 0, 21);
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

  internal static string ssp_appserver_12329()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[16 /*0x10*/] = (byte) 172;
      numArray2[1] = (byte) 194;
      numArray2[14] = (byte) 123;
      numArray2[3] = (byte) 215;
      numArray2[9] = (byte) 197;
      numArray2[5] = (byte) 225;
      numArray2[6] = (byte) 160 /*0xA0*/;
      numArray2[7] = (byte) 243;
      numArray2[8] = (byte) 63 /*0x3F*/;
      numArray2[4] = (byte) 57;
      numArray2[10] = (byte) 56;
      numArray2[2] = (byte) 164;
      numArray2[12] = (byte) 114;
      numArray2[18] = (byte) 80 /*0x50*/;
      numArray2[11] = (byte) 229;
      numArray2[15] = (byte) 184;
      numArray2[13] = (byte) 47;
      numArray2[17] = (byte) 222;
      numArray2[0] = (byte) 240 /*0xF0*/;
      byte[] numArray3 = new byte[19];
      numArray3[16 /*0x10*/] = (byte) 84;
      numArray3[1] = (byte) 253;
      numArray3[12] = (byte) 54;
      numArray3[18] = (byte) 0;
      numArray3[4] = (byte) 21;
      numArray3[3] = (byte) 39;
      numArray3[6] = (byte) 232;
      numArray3[7] = (byte) 29;
      numArray3[8] = (byte) 86;
      numArray3[9] = (byte) 221;
      numArray3[10] = (byte) 107;
      numArray3[11] = (byte) 181;
      numArray3[0] = (byte) 71;
      numArray3[13] = (byte) 97;
      numArray3[14] = (byte) 154;
      numArray3[15] = (byte) 144 /*0x90*/;
      numArray3[2] = (byte) 254;
      numArray3[17] = (byte) 182;
      numArray3[5] = (byte) 238;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 104,
      (byte) 114,
      (byte) 171,
      (byte) 43,
      (byte) 54,
      (byte) 197,
      (byte) 73,
      (byte) 162,
      (byte) 91,
      (byte) 235,
      (byte) 210,
      (byte) 234,
      (byte) 195,
      (byte) 253,
      (byte) 116,
      (byte) 225,
      (byte) 17,
      (byte) 124,
      (byte) 128 /*0x80*/
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 157,
      (byte) 114,
      (byte) 86,
      (byte) 27,
      (byte) 187,
      (byte) 92,
      (byte) 150,
      (byte) 86,
      (byte) 242,
      (byte) 219,
      (byte) 99,
      (byte) 99,
      (byte) 89,
      (byte) 199,
      (byte) 31 /*0x1F*/,
      (byte) 190,
      (byte) 132,
      (byte) 251,
      (byte) 166
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12330()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 227,
        (byte) 252,
        (byte) 65,
        (byte) 112 /*0x70*/,
        (byte) 249,
        (byte) 165,
        (byte) 127 /*0x7F*/,
        (byte) 142,
        (byte) 54,
        (byte) 45,
        (byte) 75,
        (byte) 234
      };
      byte[] numArray3 = new byte[12];
      numArray3[1] = (byte) 75;
      numArray3[0] = (byte) 215;
      numArray3[2] = (byte) 91;
      numArray3[4] = (byte) 29;
      numArray3[7] = (byte) 190;
      numArray3[11] = (byte) 30;
      numArray3[6] = (byte) 40;
      numArray3[5] = (byte) 42;
      numArray3[8] = (byte) 185;
      numArray3[9] = (byte) 76;
      numArray3[3] = (byte) 245;
      numArray3[10] = (byte) 133;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 67,
      (byte) 131,
      (byte) 46,
      (byte) 26,
      (byte) 66,
      (byte) 89,
      (byte) 188,
      (byte) 199,
      (byte) 46,
      (byte) 245,
      (byte) 84,
      (byte) 80 /*0x50*/
    };
    byte[] numArray6 = new byte[12];
    numArray6[6] = (byte) 122;
    numArray6[1] = (byte) 92;
    numArray6[10] = (byte) 90;
    numArray6[11] = (byte) 137;
    numArray6[4] = (byte) 69;
    numArray6[2] = (byte) 33;
    numArray6[3] = (byte) 247;
    numArray6[7] = (byte) 126;
    numArray6[8] = (byte) 187;
    numArray6[9] = (byte) 238;
    numArray6[0] = (byte) 193;
    numArray6[5] = (byte) 62;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12331()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 107,
        (byte) 0,
        (byte) 92
      };
      numArray2[1] = (byte) 207;
      numArray2[2] = (byte) 183;
      numArray2[4] = (byte) 203;
      numArray2[0] = (byte) 82;
      byte[] numArray3 = new byte[6]
      {
        (byte) 198,
        (byte) 127 /*0x7F*/,
        (byte) 23,
        (byte) 61,
        (byte) 205,
        (byte) 222
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6]
    {
      (byte) 25,
      (byte) 91,
      (byte) 50,
      (byte) 62,
      (byte) 49,
      (byte) 45
    };
    byte[] numArray6 = new byte[6]
    {
      (byte) 205,
      (byte) 8,
      (byte) 180,
      (byte) 7,
      (byte) 26,
      (byte) 61
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
