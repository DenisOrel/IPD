// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12431
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12431
{
  private static byte[] sspq = new byte[264]
  {
    (byte) 174,
    (byte) 28,
    (byte) 224 /*0xE0*/,
    (byte) 71,
    (byte) 235,
    (byte) 178,
    (byte) 13,
    (byte) 162,
    (byte) 180,
    (byte) 29,
    (byte) 145,
    (byte) 47,
    (byte) 164,
    (byte) 133,
    (byte) 194,
    (byte) 225,
    (byte) 90,
    (byte) 55,
    (byte) 176 /*0xB0*/,
    (byte) 221,
    (byte) 2,
    (byte) 102,
    (byte) 125,
    (byte) 171,
    (byte) 69,
    (byte) 59,
    (byte) 32 /*0x20*/,
    (byte) 203,
    (byte) 115,
    (byte) 228,
    (byte) 199,
    (byte) 157,
    (byte) 207,
    (byte) 218,
    (byte) 52,
    (byte) 166,
    (byte) 31 /*0x1F*/,
    (byte) 185,
    (byte) 51,
    (byte) 123,
    (byte) 163,
    (byte) 80 /*0x50*/,
    (byte) 146,
    (byte) 121,
    (byte) 33,
    (byte) 215,
    (byte) 251,
    (byte) 111,
    (byte) 215,
    (byte) 60,
    (byte) 241,
    (byte) 165,
    (byte) 55,
    (byte) 22,
    (byte) 66,
    (byte) 211,
    (byte) 211,
    (byte) 138,
    (byte) 125,
    (byte) 139,
    (byte) 241,
    (byte) 44,
    (byte) 29,
    (byte) 194,
    (byte) 11,
    (byte) 86,
    (byte) 176 /*0xB0*/,
    (byte) 110,
    (byte) 40,
    (byte) 248,
    (byte) 119,
    (byte) 184,
    (byte) 103,
    (byte) 98,
    (byte) 64 /*0x40*/,
    (byte) 248,
    (byte) 208 /*0xD0*/,
    (byte) 83,
    (byte) 89,
    (byte) 123,
    (byte) 101,
    (byte) 243,
    (byte) 13,
    (byte) 204,
    (byte) 26,
    (byte) 0,
    (byte) 142,
    (byte) 234,
    (byte) 12,
    (byte) 43,
    (byte) 223,
    (byte) 182,
    (byte) 122,
    (byte) 195,
    (byte) 240 /*0xF0*/,
    (byte) 35,
    (byte) 131,
    (byte) 53,
    (byte) 150,
    (byte) 147,
    (byte) 218,
    (byte) 162,
    (byte) 139,
    (byte) 125,
    (byte) 112 /*0x70*/,
    (byte) 21,
    (byte) 24,
    (byte) 154,
    (byte) 139,
    (byte) 31 /*0x1F*/,
    (byte) 73,
    (byte) 76,
    (byte) 46,
    (byte) 222,
    (byte) 193,
    (byte) 213,
    (byte) 109,
    (byte) 55,
    (byte) 107,
    (byte) 33,
    (byte) 161,
    (byte) 48 /*0x30*/,
    (byte) 120,
    (byte) 47,
    (byte) 201,
    (byte) 43,
    (byte) 185,
    (byte) 92,
    (byte) 135,
    (byte) 17,
    (byte) 32 /*0x20*/,
    (byte) 209,
    (byte) 157,
    (byte) 85,
    (byte) 114,
    (byte) 153,
    (byte) 158,
    (byte) 124,
    (byte) 85,
    (byte) 19,
    (byte) 238,
    (byte) 71,
    (byte) 147,
    (byte) 47,
    (byte) 82,
    (byte) 180,
    (byte) 68,
    (byte) 250,
    (byte) 206,
    (byte) 142,
    (byte) 41,
    (byte) 71,
    (byte) 20,
    (byte) 233,
    (byte) 42,
    (byte) 21,
    (byte) 47,
    (byte) 137,
    (byte) 54,
    (byte) 246,
    (byte) 65,
    (byte) 90,
    (byte) 116,
    (byte) 180,
    (byte) 93,
    (byte) 106,
    (byte) 21,
    (byte) 70,
    (byte) 95,
    (byte) 210,
    (byte) 147,
    (byte) 210,
    (byte) 223,
    (byte) 1,
    (byte) 35,
    (byte) 192 /*0xC0*/,
    (byte) 54,
    (byte) 230,
    (byte) 127 /*0x7F*/,
    (byte) 72,
    (byte) 55,
    (byte) 101,
    (byte) 188,
    (byte) 182,
    (byte) 208 /*0xD0*/,
    (byte) 129,
    (byte) 252,
    (byte) 232,
    (byte) 11,
    (byte) 22,
    (byte) 38,
    (byte) 211,
    (byte) 138,
    (byte) 167,
    (byte) 164,
    (byte) 105,
    (byte) 148,
    (byte) 110,
    (byte) 204,
    (byte) 202,
    (byte) 89,
    (byte) 58,
    (byte) 56,
    (byte) 222,
    (byte) 81,
    (byte) 95,
    (byte) 167,
    (byte) 13,
    (byte) 141,
    (byte) 127 /*0x7F*/,
    (byte) 197,
    (byte) 152,
    (byte) 218,
    (byte) 107,
    (byte) 95,
    (byte) 164,
    (byte) 93,
    (byte) 58,
    (byte) 4,
    (byte) 67,
    (byte) 2,
    (byte) 219,
    (byte) 34,
    (byte) 226,
    (byte) 142,
    (byte) 174,
    (byte) 156,
    (byte) 40,
    (byte) 103,
    (byte) 2,
    (byte) 4,
    (byte) 140,
    (byte) 230,
    (byte) 156,
    (byte) 169,
    (byte) 127 /*0x7F*/,
    (byte) 47,
    (byte) 73,
    (byte) 23,
    (byte) 207,
    (byte) 137,
    (byte) 121,
    (byte) 197,
    (byte) 111,
    (byte) 58,
    (byte) 214,
    (byte) 78,
    (byte) 202,
    (byte) 93,
    (byte) 12,
    (byte) 195,
    (byte) 210,
    (byte) 159,
    (byte) 36,
    (byte) 223,
    (byte) 235,
    (byte) 108,
    (byte) 68,
    (byte) 228,
    (byte) 101,
    (byte) 251,
    (byte) 22,
    (byte) 76,
    (byte) 74
  };
  private static byte[] sspr = new byte[264]
  {
    (byte) 120,
    (byte) 144 /*0x90*/,
    (byte) 12,
    (byte) 51,
    (byte) 246,
    (byte) 49,
    (byte) 11,
    (byte) 124,
    (byte) 179,
    (byte) 35,
    (byte) 28,
    (byte) 23,
    (byte) 11,
    (byte) 105,
    (byte) 72,
    (byte) 59,
    (byte) 45,
    (byte) 183,
    (byte) 189,
    (byte) 81,
    (byte) 232,
    (byte) 171,
    (byte) 147,
    (byte) 4,
    (byte) 212,
    (byte) 199,
    (byte) 193,
    (byte) 233,
    (byte) 1,
    (byte) 114,
    (byte) 140,
    (byte) 66,
    (byte) 242,
    (byte) 198,
    (byte) 49,
    (byte) 172,
    (byte) 75,
    (byte) 226,
    (byte) 69,
    (byte) 24,
    (byte) 68,
    (byte) 202,
    (byte) 210,
    (byte) 216,
    (byte) 100,
    (byte) 142,
    (byte) 114,
    (byte) 211,
    (byte) 112 /*0x70*/,
    (byte) 181,
    (byte) 81,
    (byte) 61,
    (byte) 109,
    (byte) 117,
    (byte) 32 /*0x20*/,
    (byte) 140,
    (byte) 72,
    (byte) 5,
    (byte) 154,
    (byte) 202,
    (byte) 30,
    (byte) 183,
    (byte) 99,
    (byte) 225,
    (byte) 90,
    (byte) 122,
    (byte) 237,
    (byte) 45,
    (byte) 47,
    (byte) 86,
    (byte) 44,
    (byte) 16 /*0x10*/,
    (byte) 60,
    (byte) 52,
    (byte) 58,
    (byte) 119,
    (byte) 88,
    (byte) 57,
    (byte) 110,
    (byte) 98,
    (byte) 85,
    (byte) 130,
    (byte) 223,
    (byte) 209,
    (byte) 31 /*0x1F*/,
    (byte) 187,
    (byte) 49,
    (byte) 155,
    (byte) 117,
    (byte) 109,
    (byte) 232,
    (byte) 226,
    (byte) 19,
    (byte) 215,
    (byte) 182,
    (byte) 7,
    (byte) 243,
    (byte) 69,
    (byte) 147,
    (byte) 162,
    (byte) 136,
    (byte) 120,
    (byte) 93,
    (byte) 57,
    (byte) 113,
    (byte) 226,
    (byte) 149,
    (byte) 74,
    (byte) 127 /*0x7F*/,
    (byte) 243,
    (byte) 131,
    (byte) 28,
    (byte) 66,
    (byte) 160 /*0xA0*/,
    (byte) 174,
    (byte) 155,
    (byte) 120,
    (byte) 72,
    (byte) 213,
    (byte) 76,
    (byte) 207,
    (byte) 191,
    (byte) 81,
    (byte) 101,
    (byte) 38,
    (byte) 113,
    (byte) 99,
    (byte) 191,
    (byte) 212,
    (byte) 57,
    (byte) 98,
    (byte) 166,
    (byte) 158,
    (byte) 198,
    (byte) 70,
    (byte) 222,
    (byte) 117,
    (byte) 159,
    (byte) 253,
    (byte) 227,
    (byte) 161,
    (byte) 57,
    (byte) 19,
    (byte) 115,
    (byte) 171,
    (byte) 78,
    (byte) 202,
    (byte) 34,
    (byte) 168,
    (byte) 68,
    (byte) 89,
    (byte) 86,
    (byte) 111,
    (byte) 78,
    (byte) 187,
    (byte) 121,
    (byte) 28,
    (byte) 165,
    (byte) 193,
    (byte) 163,
    (byte) 130,
    (byte) 33,
    (byte) 34,
    (byte) 217,
    (byte) 80 /*0x50*/,
    (byte) 239,
    (byte) 135,
    (byte) 247,
    (byte) 115,
    (byte) 246,
    (byte) 208 /*0xD0*/,
    (byte) 67,
    (byte) 226,
    (byte) 152,
    (byte) 183,
    (byte) 200,
    (byte) 249,
    (byte) 117,
    (byte) 191,
    (byte) 215,
    (byte) 189,
    (byte) 22,
    (byte) 44,
    (byte) 242,
    (byte) 59,
    (byte) 218,
    (byte) 169,
    (byte) 199,
    (byte) 148,
    (byte) 43,
    (byte) 151,
    (byte) 137,
    (byte) 75,
    (byte) 92,
    (byte) 106,
    (byte) 14,
    (byte) 164,
    (byte) 233,
    (byte) 204,
    (byte) 46,
    (byte) 231,
    (byte) 174,
    (byte) 145,
    (byte) 108,
    (byte) 134,
    (byte) 24,
    (byte) 148,
    (byte) 60,
    (byte) 171,
    (byte) 116,
    (byte) 55,
    (byte) 31 /*0x1F*/,
    (byte) 209,
    (byte) 37,
    (byte) 181,
    (byte) 153,
    (byte) 22,
    (byte) 21,
    (byte) 230,
    (byte) 143,
    (byte) 45,
    (byte) 183,
    (byte) 113,
    (byte) 160 /*0xA0*/,
    (byte) 54,
    (byte) 112 /*0x70*/,
    (byte) 71,
    (byte) 223,
    (byte) 107,
    (byte) 190,
    (byte) 187,
    (byte) 129,
    (byte) 181,
    (byte) 125,
    (byte) 181,
    (byte) 198,
    (byte) 150,
    (byte) 232,
    (byte) 93,
    (byte) 241,
    (byte) 192 /*0xC0*/,
    (byte) 108,
    (byte) 222,
    (byte) 240 /*0xF0*/,
    (byte) 247,
    (byte) 129,
    (byte) 51,
    (byte) 72,
    (byte) 169,
    (byte) 31 /*0x1F*/,
    (byte) 216,
    (byte) 30,
    (byte) 121,
    (byte) 28,
    (byte) 0,
    (byte) 154,
    (byte) 83,
    (byte) 204,
    (byte) 231,
    (byte) 151,
    (byte) 188,
    (byte) 162,
    (byte) 33,
    (byte) 0
  };

  internal static string ssp_appserver_12432()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 133,
        (byte) 54,
        (byte) 185,
        (byte) 30,
        (byte) 125,
        (byte) 152,
        (byte) 107,
        (byte) 178,
        (byte) 138,
        (byte) 147
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 112 /*0x70*/,
        (byte) 229,
        (byte) 202,
        (byte) 236,
        (byte) 222,
        (byte) 27,
        (byte) 26,
        (byte) 13,
        (byte) 137,
        (byte) 85
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15];
      byte[] response = new byte[15];
      Array.Copy((Array) sc_12431.sspq, 0, (Array) numArray4, 0, 15);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12431.sspr, 0, (Array) numArray4, 0, 15);
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
      (byte) 31 /*0x1F*/,
      (byte) 110,
      (byte) 2,
      (byte) 185,
      (byte) 24,
      (byte) 22,
      (byte) 253,
      (byte) 62,
      (byte) 221,
      (byte) 224 /*0xE0*/
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 106,
      byte.MaxValue,
      (byte) 191,
      (byte) 164,
      (byte) 209,
      (byte) 204,
      (byte) 137,
      (byte) 4,
      (byte) 193,
      (byte) 20
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12433()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 148,
        (byte) 82,
        (byte) 30,
        (byte) 75,
        (byte) 221,
        (byte) 231,
        (byte) 243,
        (byte) 69,
        (byte) 58,
        (byte) 92,
        (byte) 153,
        (byte) 95,
        (byte) 227,
        (byte) 129,
        (byte) 155,
        (byte) 141,
        (byte) 77,
        (byte) 244,
        (byte) 229
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 19,
        (byte) 120,
        (byte) 214,
        (byte) 53,
        (byte) 104,
        (byte) 147,
        (byte) 175,
        (byte) 253,
        (byte) 195,
        (byte) 108,
        (byte) 0,
        (byte) 136,
        (byte) 217,
        (byte) 241,
        (byte) 90,
        (byte) 181,
        (byte) 28,
        (byte) 240 /*0xF0*/,
        (byte) 254
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_12431.sspq, 15, (Array) numArray4, 0, 41);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12431.sspr, 15, (Array) numArray4, 0, 41);
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
      (byte) 222,
      (byte) 238,
      (byte) 25,
      (byte) 167,
      (byte) 97,
      (byte) 207,
      (byte) 25,
      (byte) 105,
      (byte) 45,
      (byte) 71,
      (byte) 32 /*0x20*/,
      (byte) 145,
      (byte) 240 /*0xF0*/,
      (byte) 211,
      (byte) 193,
      (byte) 160 /*0xA0*/,
      (byte) 4,
      (byte) 52,
      (byte) 57
    };
    byte[] numArray7 = new byte[19];
    numArray7[7] = (byte) 54;
    numArray7[4] = (byte) 125;
    numArray7[2] = (byte) 205;
    numArray7[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray7[12] = (byte) 246;
    numArray7[5] = (byte) 249;
    numArray7[6] = (byte) 207;
    numArray7[17] = (byte) 9;
    numArray7[8] = (byte) 4;
    numArray7[1] = (byte) 248;
    numArray7[10] = (byte) 230;
    numArray7[11] = (byte) 101;
    numArray7[3] = (byte) 70;
    numArray7[13] = (byte) 251;
    numArray7[14] = (byte) 239;
    numArray7[0] = (byte) 162;
    numArray7[15] = (byte) 57;
    numArray7[9] = (byte) 239;
    numArray7[18] = (byte) 247;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12434()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 75,
        (byte) 217,
        (byte) 183,
        (byte) 106,
        (byte) 127 /*0x7F*/,
        (byte) 3,
        (byte) 123,
        (byte) 80 /*0x50*/,
        (byte) 160 /*0xA0*/,
        (byte) 2,
        (byte) 220,
        (byte) 143,
        byte.MaxValue,
        (byte) 218,
        (byte) 99,
        (byte) 171,
        (byte) 104
      };
      byte[] numArray3 = new byte[17];
      numArray3[11] = (byte) 31 /*0x1F*/;
      numArray3[9] = (byte) 192 /*0xC0*/;
      numArray3[0] = (byte) 30;
      numArray3[13] = (byte) 107;
      numArray3[4] = (byte) 38;
      numArray3[1] = (byte) 245;
      numArray3[6] = (byte) 246;
      numArray3[7] = (byte) 70;
      numArray3[8] = (byte) 155;
      numArray3[5] = (byte) 25;
      numArray3[10] = (byte) 145;
      numArray3[3] = (byte) 127 /*0x7F*/;
      numArray3[12] = (byte) 161;
      numArray3[2] = (byte) 19;
      numArray3[14] = (byte) 241;
      numArray3[15] = (byte) 39;
      numArray3[16 /*0x10*/] = (byte) 88;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[9] = (byte) 98;
    numArray5[6] = (byte) 99;
    numArray5[2] = (byte) 240 /*0xF0*/;
    numArray5[15] = (byte) 247;
    numArray5[4] = (byte) 163;
    numArray5[5] = (byte) 128 /*0x80*/;
    numArray5[12] = (byte) 245;
    numArray5[7] = (byte) 219;
    numArray5[8] = (byte) 8;
    numArray5[3] = (byte) 187;
    numArray5[1] = (byte) 49;
    numArray5[11] = (byte) 205;
    numArray5[10] = (byte) 237;
    numArray5[13] = (byte) 34;
    numArray5[0] = (byte) 212;
    numArray5[14] = (byte) 105;
    numArray5[16 /*0x10*/] = (byte) 83;
    byte[] numArray6 = new byte[17]
    {
      (byte) 66,
      (byte) 229,
      (byte) 58,
      (byte) 161,
      (byte) 222,
      (byte) 19,
      (byte) 111,
      (byte) 140,
      (byte) 187,
      (byte) 220,
      (byte) 141,
      (byte) 150,
      (byte) 27,
      (byte) 172,
      (byte) 102,
      (byte) 75,
      (byte) 0
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_12431.sspq, 56, (Array) numArray7, 0, 38);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12431.sspr, 56, (Array) numArray7, 0, 38);
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

  internal static string ssp_appserver_12435()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[159];
      byte[] numArray2 = new byte[55]
      {
        (byte) 195,
        (byte) 217,
        (byte) 40,
        (byte) 68,
        (byte) 209,
        (byte) 55,
        (byte) 7,
        (byte) 236,
        (byte) 92,
        (byte) 169,
        (byte) 250,
        (byte) 141,
        (byte) 48 /*0x30*/,
        (byte) 62,
        (byte) 241,
        (byte) 87,
        (byte) 127 /*0x7F*/,
        (byte) 49,
        (byte) 2,
        (byte) 38,
        (byte) 253,
        (byte) 31 /*0x1F*/,
        (byte) 81,
        (byte) 167,
        (byte) 10,
        (byte) 199,
        (byte) 197,
        (byte) 98,
        (byte) 252,
        (byte) 24,
        (byte) 193,
        (byte) 62,
        (byte) 163,
        (byte) 226,
        (byte) 125,
        (byte) 31 /*0x1F*/,
        (byte) 167,
        (byte) 15,
        (byte) 135,
        (byte) 67,
        (byte) 94,
        (byte) 7,
        (byte) 176 /*0xB0*/,
        (byte) 34,
        (byte) 32 /*0x20*/,
        (byte) 115,
        (byte) 94,
        (byte) 172,
        (byte) 26,
        (byte) 212,
        (byte) 95,
        (byte) 132,
        (byte) 119,
        (byte) 157,
        (byte) 131
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 102,
        (byte) 197,
        (byte) 228,
        (byte) 213,
        (byte) 126,
        (byte) 55,
        (byte) 185,
        (byte) 90,
        (byte) 102,
        (byte) 202,
        (byte) 125,
        (byte) 120,
        (byte) 139,
        (byte) 157,
        (byte) 152,
        (byte) 63 /*0x3F*/,
        (byte) 149,
        (byte) 225,
        (byte) 9,
        (byte) 142,
        (byte) 82,
        (byte) 6,
        (byte) 221,
        (byte) 182,
        (byte) 239,
        (byte) 110,
        (byte) 124,
        (byte) 141,
        (byte) 8,
        (byte) 186,
        (byte) 143,
        (byte) 153,
        (byte) 11,
        (byte) 70,
        (byte) 130,
        (byte) 54,
        (byte) 125,
        (byte) 254,
        (byte) 74,
        (byte) 122,
        (byte) 133,
        (byte) 131,
        (byte) 47,
        (byte) 76,
        (byte) 39,
        (byte) 225,
        (byte) 236,
        (byte) 227,
        (byte) 71,
        (byte) 69,
        (byte) 2,
        (byte) 180,
        (byte) 28,
        (byte) 79,
        (byte) 167
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 128 /*0x80*/,
        (byte) 57,
        (byte) 10,
        (byte) 114,
        (byte) 165,
        (byte) 239,
        (byte) 68,
        (byte) 231,
        (byte) 187,
        (byte) 233,
        (byte) 78,
        (byte) 192 /*0xC0*/,
        (byte) 210,
        (byte) 175,
        (byte) 112 /*0x70*/,
        (byte) 169,
        (byte) 190,
        (byte) 199,
        (byte) 245,
        (byte) 110,
        (byte) 47,
        (byte) 135,
        (byte) 150,
        (byte) 105,
        (byte) 24,
        (byte) 71,
        (byte) 39,
        (byte) 184,
        (byte) 61,
        (byte) 198,
        (byte) 5,
        (byte) 95,
        (byte) 216,
        (byte) 150,
        (byte) 38,
        (byte) 213,
        (byte) 125,
        (byte) 209,
        (byte) 185,
        (byte) 24,
        (byte) 108,
        (byte) 58,
        (byte) 100,
        (byte) 137,
        (byte) 15,
        (byte) 235,
        (byte) 71,
        (byte) 37,
        (byte) 239,
        (byte) 218,
        (byte) 184,
        (byte) 214,
        (byte) 141,
        (byte) 54,
        (byte) 89
      };
      byte[] numArray5 = new byte[55];
      numArray5[24] = (byte) 232;
      numArray5[47] = (byte) 21;
      numArray5[45] = (byte) 85;
      numArray5[3] = (byte) 193;
      numArray5[4] = (byte) 77;
      numArray5[7] = (byte) 68;
      numArray5[22] = (byte) 143;
      numArray5[42] = (byte) 206;
      numArray5[28] = (byte) 207;
      numArray5[9] = (byte) 107;
      numArray5[10] = (byte) 56;
      numArray5[15] = (byte) 43;
      numArray5[8] = (byte) 182;
      numArray5[0] = (byte) 109;
      numArray5[14] = (byte) 116;
      numArray5[48 /*0x30*/] = (byte) 143;
      numArray5[16 /*0x10*/] = (byte) 86;
      numArray5[34] = (byte) 121;
      numArray5[18] = (byte) 199;
      numArray5[2] = (byte) 145;
      numArray5[20] = (byte) 200;
      numArray5[21] = (byte) 213;
      numArray5[41] = (byte) 105;
      numArray5[23] = (byte) 89;
      numArray5[53] = (byte) 116;
      numArray5[25] = (byte) 182;
      numArray5[26] = (byte) 208 /*0xD0*/;
      numArray5[27] = (byte) 5;
      numArray5[5] = (byte) 113;
      numArray5[29] = (byte) 128 /*0x80*/;
      numArray5[1] = (byte) 138;
      numArray5[31 /*0x1F*/] = (byte) 178;
      numArray5[11] = (byte) 254;
      numArray5[33] = (byte) 181;
      numArray5[6] = (byte) 125;
      numArray5[50] = (byte) 119;
      numArray5[36] = (byte) 170;
      numArray5[37] = (byte) 245;
      numArray5[39] = (byte) 64 /*0x40*/;
      numArray5[32 /*0x20*/] = (byte) 116;
      numArray5[40] = (byte) 239;
      numArray5[44] = (byte) 65;
      numArray5[52] = (byte) 82;
      numArray5[43] = (byte) 173;
      numArray5[51] = (byte) 105;
      numArray5[49] = (byte) 6;
      numArray5[46] = (byte) 45;
      numArray5[13] = (byte) 39;
      numArray5[30] = (byte) 136;
      numArray5[19] = (byte) 4;
      numArray5[17] = (byte) 65;
      numArray5[35] = (byte) 188;
      numArray5[54] = (byte) 27;
      numArray5[12] = (byte) 62;
      numArray5[38] = (byte) 63 /*0x3F*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[49];
      numArray6[26] = (byte) 180;
      numArray6[1] = (byte) 44;
      numArray6[22] = (byte) 41;
      numArray6[3] = (byte) 197;
      numArray6[4] = (byte) 193;
      numArray6[5] = (byte) 72;
      numArray6[39] = (byte) 70;
      numArray6[12] = (byte) 104;
      numArray6[7] = (byte) 180;
      numArray6[11] = (byte) 91;
      numArray6[37] = (byte) 138;
      numArray6[10] = (byte) 254;
      numArray6[25] = (byte) 117;
      numArray6[13] = (byte) 81;
      numArray6[14] = (byte) 117;
      numArray6[15] = (byte) 178;
      numArray6[29] = (byte) 33;
      numArray6[28] = (byte) 103;
      numArray6[18] = (byte) 100;
      numArray6[0] = (byte) 71;
      numArray6[20] = (byte) 103;
      numArray6[23] = (byte) 243;
      numArray6[8] = (byte) 28;
      numArray6[16 /*0x10*/] = (byte) 141;
      numArray6[19] = (byte) 179;
      numArray6[6] = (byte) 230;
      numArray6[36] = (byte) 253;
      numArray6[27] = (byte) 67;
      numArray6[2] = (byte) 94;
      numArray6[21] = (byte) 56;
      numArray6[30] = (byte) 110;
      numArray6[31 /*0x1F*/] = (byte) 216;
      numArray6[17] = (byte) 31 /*0x1F*/;
      numArray6[33] = (byte) 157;
      numArray6[34] = (byte) 147;
      numArray6[9] = (byte) 69;
      numArray6[47] = (byte) 92;
      numArray6[38] = (byte) 32 /*0x20*/;
      numArray6[32 /*0x20*/] = (byte) 87;
      numArray6[45] = (byte) 65;
      numArray6[40] = (byte) 137;
      numArray6[41] = (byte) 241;
      numArray6[42] = (byte) 4;
      numArray6[43] = (byte) 190;
      numArray6[44] = (byte) 12;
      numArray6[46] = (byte) 235;
      numArray6[24] = (byte) 102;
      numArray6[35] = (byte) 75;
      numArray6[48 /*0x30*/] = (byte) 85;
      byte[] numArray7 = new byte[49]
      {
        (byte) 141,
        (byte) 100,
        (byte) 185,
        (byte) 88,
        (byte) 49,
        (byte) 83,
        (byte) 193,
        (byte) 210,
        (byte) 18,
        (byte) 246,
        (byte) 104,
        (byte) 183,
        (byte) 8,
        (byte) 200,
        (byte) 12,
        (byte) 224 /*0xE0*/,
        (byte) 41,
        (byte) 207,
        (byte) 252,
        (byte) 164,
        (byte) 28,
        (byte) 72,
        (byte) 0,
        (byte) 108,
        (byte) 80 /*0x50*/,
        (byte) 141,
        (byte) 224 /*0xE0*/,
        (byte) 147,
        (byte) 49,
        (byte) 76,
        (byte) 157,
        (byte) 107,
        (byte) 56,
        (byte) 183,
        (byte) 184,
        (byte) 155,
        (byte) 184,
        (byte) 170,
        (byte) 202,
        (byte) 77,
        (byte) 115,
        (byte) 82,
        (byte) 59,
        (byte) 1,
        (byte) 194,
        (byte) 28,
        (byte) 183,
        (byte) 240 /*0xF0*/,
        (byte) 58
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[159];
    byte[] numArray9 = new byte[55]
    {
      (byte) 32 /*0x20*/,
      (byte) 159,
      (byte) 43,
      (byte) 96 /*0x60*/,
      (byte) 31 /*0x1F*/,
      (byte) 21,
      (byte) 225,
      (byte) 249,
      (byte) 184,
      (byte) 219,
      (byte) 151,
      (byte) 131,
      (byte) 58,
      (byte) 192 /*0xC0*/,
      (byte) 60,
      (byte) 181,
      (byte) 250,
      (byte) 251,
      (byte) 126,
      (byte) 77,
      (byte) 206,
      (byte) 92,
      (byte) 56,
      (byte) 224 /*0xE0*/,
      (byte) 34,
      (byte) 216,
      (byte) 41,
      (byte) 107,
      (byte) 67,
      (byte) 208 /*0xD0*/,
      (byte) 43,
      (byte) 132,
      (byte) 0,
      (byte) 231,
      (byte) 106,
      (byte) 184,
      (byte) 194,
      (byte) 240 /*0xF0*/,
      (byte) 223,
      (byte) 67,
      (byte) 246,
      (byte) 59,
      (byte) 182,
      (byte) 169,
      (byte) 178,
      (byte) 14,
      (byte) 240 /*0xF0*/,
      (byte) 20,
      (byte) 118,
      (byte) 48 /*0x30*/,
      (byte) 170,
      (byte) 13,
      (byte) 104,
      (byte) 152,
      (byte) 110
    };
    byte[] numArray10 = new byte[55];
    numArray10[38] = (byte) 178;
    numArray10[1] = (byte) 232;
    numArray10[5] = (byte) 17;
    numArray10[11] = (byte) 207;
    numArray10[26] = (byte) 221;
    numArray10[7] = (byte) 40;
    numArray10[6] = (byte) 54;
    numArray10[43] = (byte) 34;
    numArray10[28] = (byte) 86;
    numArray10[46] = (byte) 118;
    numArray10[10] = (byte) 190;
    numArray10[13] = (byte) 110;
    numArray10[12] = (byte) 224 /*0xE0*/;
    numArray10[33] = (byte) 179;
    numArray10[22] = (byte) 96 /*0x60*/;
    numArray10[15] = (byte) 90;
    numArray10[16 /*0x10*/] = (byte) 86;
    numArray10[31 /*0x1F*/] = (byte) 150;
    numArray10[20] = (byte) 177;
    numArray10[19] = (byte) 105;
    numArray10[50] = (byte) 254;
    numArray10[21] = (byte) 67;
    numArray10[18] = (byte) 236;
    numArray10[23] = (byte) 203;
    numArray10[24] = (byte) 190;
    numArray10[25] = (byte) 116;
    numArray10[49] = (byte) 82;
    numArray10[27] = (byte) 166;
    numArray10[41] = (byte) 202;
    numArray10[29] = (byte) 64 /*0x40*/;
    numArray10[42] = (byte) 32 /*0x20*/;
    numArray10[17] = (byte) 54;
    numArray10[32 /*0x20*/] = (byte) 9;
    numArray10[8] = (byte) 62;
    numArray10[3] = (byte) 196;
    numArray10[9] = (byte) 62;
    numArray10[36] = (byte) 41;
    numArray10[45] = (byte) 12;
    numArray10[37] = (byte) 5;
    numArray10[53] = (byte) 206;
    numArray10[34] = (byte) 31 /*0x1F*/;
    numArray10[40] = (byte) 249;
    numArray10[52] = (byte) 50;
    numArray10[35] = (byte) 124;
    numArray10[44] = (byte) 212;
    numArray10[14] = (byte) 245;
    numArray10[39] = (byte) 150;
    numArray10[47] = (byte) 24;
    numArray10[48 /*0x30*/] = (byte) 96 /*0x60*/;
    numArray10[0] = (byte) 225;
    numArray10[2] = (byte) 125;
    numArray10[51] = (byte) 59;
    numArray10[4] = (byte) 216;
    numArray10[30] = (byte) 197;
    numArray10[54] = (byte) 56;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 29,
      (byte) 43,
      (byte) 96 /*0x60*/,
      (byte) 214,
      (byte) 129,
      (byte) 162,
      (byte) 193,
      (byte) 146,
      (byte) 245,
      (byte) 27,
      (byte) 218,
      (byte) 176 /*0xB0*/,
      (byte) 50,
      (byte) 124,
      (byte) 87,
      (byte) 132,
      (byte) 226,
      (byte) 215,
      (byte) 13,
      (byte) 214,
      (byte) 149,
      (byte) 142,
      (byte) 135,
      (byte) 73,
      (byte) 87,
      (byte) 2,
      (byte) 213,
      (byte) 133,
      (byte) 44,
      (byte) 238,
      (byte) 39,
      (byte) 157,
      (byte) 135,
      (byte) 121,
      (byte) 89,
      (byte) 134,
      (byte) 55,
      (byte) 129,
      (byte) 253,
      (byte) 227,
      (byte) 13,
      (byte) 103,
      (byte) 170,
      (byte) 112 /*0x70*/,
      (byte) 195,
      (byte) 13,
      (byte) 191,
      (byte) 193,
      (byte) 72,
      (byte) 109,
      (byte) 75,
      (byte) 105,
      (byte) 206,
      (byte) 72,
      (byte) 66
    };
    byte[] numArray12 = new byte[55];
    numArray12[49] = (byte) 201;
    numArray12[1] = (byte) 59;
    numArray12[2] = (byte) 30;
    numArray12[34] = (byte) 76;
    numArray12[4] = (byte) 38;
    numArray12[5] = (byte) 29;
    numArray12[20] = (byte) 93;
    numArray12[37] = (byte) 142;
    numArray12[8] = (byte) 56;
    numArray12[19] = (byte) 139;
    numArray12[43] = (byte) 83;
    numArray12[11] = (byte) 45;
    numArray12[41] = (byte) 202;
    numArray12[50] = (byte) 70;
    numArray12[14] = (byte) 219;
    numArray12[15] = (byte) 72;
    numArray12[16 /*0x10*/] = (byte) 133;
    numArray12[31 /*0x1F*/] = (byte) 145;
    numArray12[18] = (byte) 253;
    numArray12[0] = (byte) 79;
    numArray12[30] = (byte) 10;
    numArray12[54] = (byte) 39;
    numArray12[22] = (byte) 154;
    numArray12[35] = (byte) 55;
    numArray12[23] = (byte) 71;
    numArray12[9] = (byte) 170;
    numArray12[26] = (byte) 144 /*0x90*/;
    numArray12[6] = (byte) 135;
    numArray12[47] = (byte) 115;
    numArray12[29] = (byte) 187;
    numArray12[42] = (byte) 136;
    numArray12[17] = (byte) 225;
    numArray12[13] = (byte) 6;
    numArray12[33] = (byte) 179;
    numArray12[53] = (byte) 136;
    numArray12[32 /*0x20*/] = (byte) 165;
    numArray12[38] = (byte) 24;
    numArray12[24] = (byte) 32 /*0x20*/;
    numArray12[36] = (byte) 233;
    numArray12[21] = (byte) 149;
    numArray12[40] = (byte) 89;
    numArray12[7] = (byte) 244;
    numArray12[27] = (byte) 157;
    numArray12[25] = (byte) 211;
    numArray12[44] = (byte) 12;
    numArray12[45] = (byte) 185;
    numArray12[46] = (byte) 97;
    numArray12[10] = (byte) 142;
    numArray12[48 /*0x30*/] = (byte) 217;
    numArray12[12] = (byte) 234;
    numArray12[39] = (byte) 230;
    numArray12[51] = (byte) 80 /*0x50*/;
    numArray12[52] = (byte) 106;
    numArray12[28] = (byte) 236;
    numArray12[3] = (byte) 99;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[49]
    {
      (byte) 143,
      (byte) 53,
      (byte) 60,
      (byte) 185,
      (byte) 254,
      (byte) 166,
      (byte) 100,
      (byte) 54,
      (byte) 110,
      (byte) 233,
      (byte) 128 /*0x80*/,
      (byte) 87,
      (byte) 237,
      (byte) 43,
      (byte) 94,
      (byte) 91,
      (byte) 20,
      (byte) 247,
      (byte) 43,
      (byte) 64 /*0x40*/,
      (byte) 153,
      (byte) 0,
      (byte) 101,
      (byte) 59,
      (byte) 117,
      (byte) 197,
      (byte) 158,
      (byte) 183,
      (byte) 45,
      (byte) 41,
      (byte) 192 /*0xC0*/,
      (byte) 35,
      (byte) 31 /*0x1F*/,
      (byte) 85,
      (byte) 145,
      (byte) 205,
      (byte) 65,
      (byte) 91,
      (byte) 210,
      (byte) 91,
      (byte) 62,
      (byte) 70,
      (byte) 22,
      (byte) 250,
      (byte) 134,
      (byte) 70,
      (byte) 137,
      (byte) 245,
      (byte) 140
    };
    byte[] numArray14 = new byte[49]
    {
      (byte) 112 /*0x70*/,
      (byte) 60,
      (byte) 179,
      (byte) 210,
      (byte) 105,
      (byte) 185,
      (byte) 85,
      (byte) 153,
      (byte) 193,
      (byte) 29,
      (byte) 88,
      (byte) 183,
      (byte) 30,
      (byte) 173,
      (byte) 233,
      (byte) 139,
      (byte) 44,
      (byte) 50,
      (byte) 162,
      (byte) 92,
      (byte) 51,
      (byte) 54,
      (byte) 58,
      (byte) 107,
      (byte) 115,
      (byte) 150,
      (byte) 188,
      (byte) 130,
      (byte) 52,
      (byte) 143,
      (byte) 19,
      (byte) 195,
      (byte) 52,
      (byte) 2,
      (byte) 217,
      (byte) 107,
      (byte) 49,
      (byte) 50,
      (byte) 170,
      (byte) 248,
      (byte) 165,
      (byte) 157,
      (byte) 29,
      (byte) 236,
      (byte) 132,
      (byte) 108,
      (byte) 84,
      (byte) 202,
      (byte) 154
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 49);
    for (int index = 0; index < 49; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12436()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 106,
        (byte) 243,
        (byte) 142,
        (byte) 207,
        (byte) 1,
        (byte) 68,
        (byte) 57,
        (byte) 7,
        (byte) 173,
        (byte) 8,
        (byte) 69,
        (byte) 41,
        (byte) 93,
        (byte) 56,
        (byte) 152,
        (byte) 153,
        (byte) 145,
        (byte) 25,
        (byte) 184,
        (byte) 36,
        (byte) 208 /*0xD0*/,
        (byte) 1,
        (byte) 83,
        (byte) 234,
        (byte) 107,
        (byte) 180,
        (byte) 46,
        (byte) 59,
        (byte) 168,
        (byte) 42,
        (byte) 249
      };
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[17] = (byte) 178;
      numArray3[1] = (byte) 74;
      numArray3[27] = (byte) 17;
      numArray3[13] = (byte) 183;
      numArray3[4] = (byte) 214;
      numArray3[5] = (byte) 177;
      numArray3[6] = (byte) 92;
      numArray3[0] = (byte) 169;
      numArray3[8] = (byte) 113;
      numArray3[29] = (byte) 36;
      numArray3[14] = (byte) 236;
      numArray3[24] = (byte) 175;
      numArray3[2] = (byte) 158;
      numArray3[18] = (byte) 92;
      numArray3[7] = (byte) 120;
      numArray3[12] = (byte) 61;
      numArray3[16 /*0x10*/] = (byte) 160 /*0xA0*/;
      numArray3[3] = (byte) 119;
      numArray3[30] = (byte) 58;
      numArray3[10] = (byte) 236;
      numArray3[11] = (byte) 144 /*0x90*/;
      numArray3[19] = (byte) 89;
      numArray3[22] = (byte) 102;
      numArray3[9] = (byte) 202;
      numArray3[20] = (byte) 30;
      numArray3[25] = (byte) 52;
      numArray3[26] = (byte) 241;
      numArray3[15] = (byte) 253;
      numArray3[21] = (byte) 70;
      numArray3[28] = (byte) 184;
      numArray3[23] = (byte) 134;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22];
      byte[] response = new byte[22];
      Array.Copy((Array) sc_12431.sspq, 94, (Array) numArray4, 0, 22);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12431.sspr, 94, (Array) numArray4, 0, 22);
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
    byte[] numArray5 = new byte[31 /*0x1F*/];
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 34,
      (byte) 75,
      (byte) 195,
      (byte) 94,
      (byte) 152,
      (byte) 242,
      (byte) 0,
      (byte) 154,
      (byte) 244,
      (byte) 14,
      (byte) 47,
      (byte) 37,
      (byte) 224 /*0xE0*/,
      (byte) 5,
      (byte) 138,
      (byte) 188,
      (byte) 198,
      (byte) 103,
      (byte) 64 /*0x40*/,
      (byte) 53,
      (byte) 215,
      (byte) 154,
      (byte) 248,
      (byte) 138,
      (byte) 139,
      (byte) 202,
      (byte) 193,
      (byte) 247,
      (byte) 180,
      (byte) 18,
      (byte) 83
    };
    byte[] numArray7 = new byte[31 /*0x1F*/];
    numArray7[17] = (byte) 50;
    numArray7[25] = (byte) 173;
    numArray7[2] = (byte) 88;
    numArray7[11] = (byte) 78;
    numArray7[4] = (byte) 105;
    numArray7[5] = (byte) 78;
    numArray7[6] = (byte) 175;
    numArray7[27] = (byte) 60;
    numArray7[8] = (byte) 76;
    numArray7[22] = (byte) 126;
    numArray7[10] = (byte) 70;
    numArray7[30] = (byte) 7;
    numArray7[12] = (byte) 159;
    numArray7[1] = (byte) 198;
    numArray7[0] = (byte) 202;
    numArray7[9] = (byte) 132;
    numArray7[16 /*0x10*/] = (byte) 96 /*0x60*/;
    numArray7[13] = (byte) 15;
    numArray7[18] = (byte) 236;
    numArray7[19] = (byte) 175;
    numArray7[7] = (byte) 254;
    numArray7[3] = (byte) 100;
    numArray7[23] = (byte) 207;
    numArray7[21] = (byte) 164;
    numArray7[24] = (byte) 13;
    numArray7[15] = (byte) 28;
    numArray7[26] = (byte) 137;
    numArray7[20] = (byte) 185;
    numArray7[28] = (byte) 198;
    numArray7[29] = (byte) 148;
    numArray7[14] = (byte) 55;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12437()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 58,
        (byte) 196,
        (byte) 4,
        (byte) 82,
        (byte) 74,
        (byte) 142,
        (byte) 108,
        (byte) 88,
        (byte) 147,
        (byte) 229
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 25;
      numArray3[6] = (byte) 242;
      numArray3[9] = (byte) 187;
      numArray3[3] = (byte) 67;
      numArray3[5] = (byte) 133;
      numArray3[2] = (byte) 179;
      numArray3[7] = (byte) 130;
      numArray3[4] = (byte) 99;
      numArray3[8] = (byte) 246;
      numArray3[0] = (byte) 248;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 121,
      (byte) 229,
      (byte) 57,
      (byte) 101,
      (byte) 45,
      (byte) 183,
      (byte) 152,
      (byte) 11,
      (byte) 167,
      (byte) 166
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 145,
      (byte) 123,
      (byte) 184,
      (byte) 41,
      (byte) 88,
      (byte) 108,
      (byte) 191,
      (byte) 43,
      (byte) 213,
      (byte) 243
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12438()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 66,
        (byte) 66,
        (byte) 247,
        (byte) 90,
        (byte) 70,
        (byte) 247,
        (byte) 24,
        (byte) 118,
        (byte) 188,
        (byte) 147
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 200,
        (byte) 115,
        (byte) 95,
        (byte) 135,
        (byte) 9,
        (byte) 87,
        (byte) 219,
        (byte) 217,
        (byte) 75,
        (byte) 45
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
      (byte) 166,
      (byte) 189,
      (byte) 85,
      (byte) 46,
      (byte) 109,
      (byte) 219,
      (byte) 168,
      (byte) 138,
      (byte) 109,
      (byte) 17
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 228,
      (byte) 8,
      (byte) 98,
      (byte) 60,
      (byte) 96 /*0x60*/,
      (byte) 136,
      (byte) 43,
      (byte) 116,
      (byte) 1,
      (byte) 229
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12439()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[101];
      byte[] numArray2 = new byte[55]
      {
        (byte) 218,
        (byte) 105,
        (byte) 240 /*0xF0*/,
        (byte) 190,
        (byte) 223,
        (byte) 27,
        (byte) 183,
        (byte) 32 /*0x20*/,
        (byte) 229,
        (byte) 148,
        (byte) 49,
        (byte) 75,
        (byte) 103,
        (byte) 4,
        (byte) 2,
        (byte) 41,
        (byte) 166,
        (byte) 43,
        (byte) 112 /*0x70*/,
        (byte) 192 /*0xC0*/,
        (byte) 242,
        (byte) 78,
        (byte) 82,
        (byte) 119,
        (byte) 121,
        (byte) 221,
        (byte) 77,
        (byte) 248,
        (byte) 212,
        (byte) 49,
        (byte) 206,
        (byte) 27,
        (byte) 165,
        (byte) 253,
        (byte) 62,
        (byte) 12,
        (byte) 203,
        (byte) 213,
        (byte) 90,
        (byte) 161,
        (byte) 62,
        (byte) 134,
        (byte) 215,
        (byte) 193,
        (byte) 252,
        (byte) 34,
        (byte) 115,
        (byte) 163,
        (byte) 135,
        (byte) 68,
        (byte) 107,
        (byte) 254,
        (byte) 206,
        (byte) 224 /*0xE0*/,
        (byte) 8
      };
      byte[] numArray3 = new byte[55];
      numArray3[9] = (byte) 129;
      numArray3[1] = (byte) 74;
      numArray3[51] = (byte) 67;
      numArray3[36] = (byte) 130;
      numArray3[40] = (byte) 150;
      numArray3[5] = (byte) 200;
      numArray3[34] = (byte) 35;
      numArray3[2] = (byte) 81;
      numArray3[19] = (byte) 234;
      numArray3[8] = (byte) 15;
      numArray3[10] = (byte) 134;
      numArray3[26] = (byte) 122;
      numArray3[53] = (byte) 181;
      numArray3[13] = (byte) 89;
      numArray3[14] = (byte) 71;
      numArray3[46] = (byte) 37;
      numArray3[16 /*0x10*/] = (byte) 184;
      numArray3[7] = (byte) 69;
      numArray3[17] = (byte) 169;
      numArray3[38] = (byte) 43;
      numArray3[20] = (byte) 24;
      numArray3[0] = (byte) 231;
      numArray3[24] = (byte) 245;
      numArray3[23] = (byte) 170;
      numArray3[4] = (byte) 194;
      numArray3[28] = (byte) 212;
      numArray3[22] = (byte) 75;
      numArray3[18] = (byte) 94;
      numArray3[29] = (byte) 212;
      numArray3[27] = (byte) 119;
      numArray3[33] = (byte) 164;
      numArray3[15] = (byte) 201;
      numArray3[32 /*0x20*/] = (byte) 80 /*0x50*/;
      numArray3[21] = (byte) 164;
      numArray3[37] = (byte) 217;
      numArray3[35] = (byte) 231;
      numArray3[25] = (byte) 202;
      numArray3[49] = (byte) 171;
      numArray3[30] = (byte) 143;
      numArray3[39] = (byte) 216;
      numArray3[3] = (byte) 171;
      numArray3[41] = (byte) 182;
      numArray3[42] = (byte) 218;
      numArray3[43] = (byte) 44;
      numArray3[44] = (byte) 179;
      numArray3[45] = (byte) 78;
      numArray3[47] = (byte) 180;
      numArray3[31 /*0x1F*/] = (byte) 54;
      numArray3[48 /*0x30*/] = (byte) 249;
      numArray3[11] = (byte) 49;
      numArray3[50] = (byte) 88;
      numArray3[12] = (byte) 74;
      numArray3[52] = (byte) 5;
      numArray3[6] = (byte) 251;
      numArray3[54] = (byte) 19;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46];
      numArray4[27] = (byte) 43;
      numArray4[31 /*0x1F*/] = (byte) 87;
      numArray4[2] = (byte) 151;
      numArray4[3] = (byte) 37;
      numArray4[4] = (byte) 79;
      numArray4[11] = (byte) 19;
      numArray4[29] = (byte) 76;
      numArray4[19] = (byte) 83;
      numArray4[17] = (byte) 2;
      numArray4[9] = (byte) 225;
      numArray4[10] = (byte) 117;
      numArray4[38] = (byte) 47;
      numArray4[30] = (byte) 102;
      numArray4[13] = (byte) 9;
      numArray4[14] = (byte) 64 /*0x40*/;
      numArray4[15] = (byte) 118;
      numArray4[22] = (byte) 219;
      numArray4[5] = (byte) 186;
      numArray4[18] = (byte) 49;
      numArray4[35] = (byte) 161;
      numArray4[8] = (byte) 201;
      numArray4[16 /*0x10*/] = (byte) 251;
      numArray4[39] = (byte) 127 /*0x7F*/;
      numArray4[23] = (byte) 96 /*0x60*/;
      numArray4[24] = (byte) 203;
      numArray4[44] = (byte) 147;
      numArray4[28] = (byte) 162;
      numArray4[34] = (byte) 86;
      numArray4[1] = (byte) 238;
      numArray4[41] = (byte) 6;
      numArray4[26] = (byte) 169;
      numArray4[0] = (byte) 253;
      numArray4[25] = (byte) 223;
      numArray4[33] = (byte) 109;
      numArray4[12] = (byte) 2;
      numArray4[6] = (byte) 238;
      numArray4[36] = (byte) 194;
      numArray4[37] = (byte) 173;
      numArray4[43] = (byte) 153;
      numArray4[32 /*0x20*/] = (byte) 156;
      numArray4[40] = (byte) 52;
      numArray4[42] = (byte) 29;
      numArray4[20] = (byte) 149;
      numArray4[21] = (byte) 148;
      numArray4[7] = (byte) 245;
      numArray4[45] = (byte) 202;
      byte[] numArray5 = new byte[46];
      numArray5[39] = (byte) 212;
      numArray5[1] = (byte) 157;
      numArray5[2] = (byte) 209;
      numArray5[20] = (byte) 50;
      numArray5[4] = (byte) 136;
      numArray5[5] = (byte) 189;
      numArray5[14] = (byte) 6;
      numArray5[24] = (byte) 196;
      numArray5[25] = (byte) 68;
      numArray5[8] = (byte) 82;
      numArray5[10] = (byte) 89;
      numArray5[33] = (byte) 185;
      numArray5[40] = (byte) 8;
      numArray5[3] = (byte) 64 /*0x40*/;
      numArray5[0] = (byte) 123;
      numArray5[42] = (byte) 31 /*0x1F*/;
      numArray5[16 /*0x10*/] = (byte) 176 /*0xB0*/;
      numArray5[15] = (byte) 153;
      numArray5[28] = (byte) 122;
      numArray5[32 /*0x20*/] = (byte) 65;
      numArray5[9] = (byte) 172;
      numArray5[21] = (byte) 213;
      numArray5[43] = (byte) 223;
      numArray5[22] = (byte) 65;
      numArray5[36] = (byte) 19;
      numArray5[6] = (byte) 6;
      numArray5[26] = (byte) 29;
      numArray5[27] = (byte) 212;
      numArray5[30] = (byte) 9;
      numArray5[29] = (byte) 15;
      numArray5[18] = (byte) 141;
      numArray5[31 /*0x1F*/] = (byte) 203;
      numArray5[34] = (byte) 211;
      numArray5[7] = (byte) 71;
      numArray5[19] = (byte) 181;
      numArray5[35] = (byte) 157;
      numArray5[11] = (byte) 240 /*0xF0*/;
      numArray5[37] = (byte) 162;
      numArray5[38] = (byte) 139;
      numArray5[13] = (byte) 138;
      numArray5[12] = (byte) 118;
      numArray5[41] = (byte) 149;
      numArray5[17] = (byte) 110;
      numArray5[23] = (byte) 55;
      numArray5[44] = (byte) 223;
      numArray5[45] = (byte) 127 /*0x7F*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[101];
    byte[] numArray7 = new byte[55];
    numArray7[40] = (byte) 0;
    numArray7[18] = (byte) 217;
    numArray7[2] = (byte) 27;
    numArray7[6] = (byte) 250;
    numArray7[0] = (byte) 51;
    numArray7[8] = (byte) 40;
    numArray7[11] = (byte) 108;
    numArray7[45] = (byte) 107;
    numArray7[7] = (byte) 160 /*0xA0*/;
    numArray7[5] = (byte) 32 /*0x20*/;
    numArray7[16 /*0x10*/] = (byte) 100;
    numArray7[41] = (byte) 114;
    numArray7[12] = (byte) 174;
    numArray7[54] = (byte) 14;
    numArray7[52] = (byte) 15;
    numArray7[37] = (byte) 200;
    numArray7[20] = (byte) 117;
    numArray7[17] = (byte) 168;
    numArray7[47] = (byte) 16 /*0x10*/;
    numArray7[19] = (byte) 188;
    numArray7[25] = (byte) 174;
    numArray7[32 /*0x20*/] = (byte) 162;
    numArray7[22] = (byte) 246;
    numArray7[38] = (byte) 83;
    numArray7[24] = (byte) 25;
    numArray7[51] = (byte) 111;
    numArray7[26] = (byte) 20;
    numArray7[27] = (byte) 170;
    numArray7[28] = (byte) 202;
    numArray7[21] = (byte) 117;
    numArray7[30] = (byte) 215;
    numArray7[31 /*0x1F*/] = (byte) 126;
    numArray7[29] = (byte) 164;
    numArray7[34] = (byte) 253;
    numArray7[3] = (byte) 252;
    numArray7[35] = (byte) 104;
    numArray7[36] = (byte) 252;
    numArray7[14] = (byte) 180;
    numArray7[9] = (byte) 129;
    numArray7[33] = (byte) 236;
    numArray7[43] = (byte) 43;
    numArray7[50] = (byte) 60;
    numArray7[42] = (byte) 26;
    numArray7[4] = (byte) 189;
    numArray7[44] = (byte) 143;
    numArray7[39] = (byte) 8;
    numArray7[46] = (byte) 115;
    numArray7[15] = (byte) 227;
    numArray7[48 /*0x30*/] = (byte) 249;
    numArray7[49] = (byte) 114;
    numArray7[13] = (byte) 69;
    numArray7[23] = (byte) 133;
    numArray7[10] = (byte) 76;
    numArray7[53] = (byte) 213;
    numArray7[1] = (byte) 53;
    byte[] numArray8 = new byte[55]
    {
      (byte) 112 /*0x70*/,
      (byte) 16 /*0x10*/,
      (byte) 38,
      (byte) 197,
      (byte) 112 /*0x70*/,
      (byte) 187,
      (byte) 232,
      (byte) 211,
      (byte) 22,
      (byte) 36,
      (byte) 113,
      (byte) 249,
      (byte) 33,
      (byte) 25,
      (byte) 143,
      (byte) 2,
      (byte) 17,
      (byte) 81,
      (byte) 102,
      (byte) 242,
      (byte) 46,
      (byte) 82,
      (byte) 33,
      byte.MaxValue,
      (byte) 253,
      (byte) 232,
      (byte) 62,
      (byte) 34,
      (byte) 152,
      (byte) 186,
      (byte) 89,
      (byte) 83,
      (byte) 250,
      (byte) 173,
      (byte) 114,
      (byte) 55,
      (byte) 200,
      (byte) 33,
      (byte) 14,
      (byte) 146,
      (byte) 232,
      (byte) 150,
      (byte) 91,
      (byte) 178,
      (byte) 184,
      (byte) 63 /*0x3F*/,
      (byte) 63 /*0x3F*/,
      (byte) 74,
      (byte) 121,
      (byte) 43,
      (byte) 107,
      (byte) 30,
      (byte) 246,
      (byte) 231,
      (byte) 83
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[46]
    {
      (byte) 159,
      (byte) 51,
      (byte) 212,
      (byte) 9,
      (byte) 158,
      (byte) 126,
      (byte) 27,
      (byte) 66,
      (byte) 95,
      (byte) 253,
      (byte) 228,
      (byte) 9,
      (byte) 36,
      (byte) 128 /*0x80*/,
      (byte) 181,
      (byte) 16 /*0x10*/,
      (byte) 213,
      (byte) 10,
      (byte) 101,
      (byte) 196,
      (byte) 73,
      (byte) 52,
      (byte) 13,
      (byte) 136,
      (byte) 58,
      (byte) 3,
      (byte) 203,
      (byte) 199,
      (byte) 76,
      (byte) 47,
      (byte) 178,
      (byte) 210,
      (byte) 220,
      (byte) 179,
      (byte) 198,
      (byte) 178,
      (byte) 112 /*0x70*/,
      (byte) 241,
      (byte) 18,
      (byte) 29,
      (byte) 122,
      (byte) 63 /*0x3F*/,
      (byte) 97,
      (byte) 164,
      (byte) 205,
      (byte) 191
    };
    byte[] numArray10 = new byte[46];
    numArray10[37] = (byte) 230;
    numArray10[35] = (byte) 158;
    numArray10[2] = (byte) 14;
    numArray10[40] = (byte) 103;
    numArray10[24] = (byte) 75;
    numArray10[8] = (byte) 74;
    numArray10[43] = (byte) 125;
    numArray10[39] = (byte) 0;
    numArray10[20] = (byte) 94;
    numArray10[9] = (byte) 181;
    numArray10[25] = (byte) 119;
    numArray10[6] = (byte) 193;
    numArray10[12] = (byte) 20;
    numArray10[13] = (byte) 73;
    numArray10[14] = (byte) 230;
    numArray10[34] = (byte) 10;
    numArray10[11] = (byte) 244;
    numArray10[21] = (byte) 108;
    numArray10[18] = (byte) 114;
    numArray10[19] = (byte) 40;
    numArray10[30] = (byte) 81;
    numArray10[15] = (byte) 179;
    numArray10[3] = (byte) 211;
    numArray10[23] = (byte) 207;
    numArray10[10] = (byte) 202;
    numArray10[0] = (byte) 202;
    numArray10[26] = (byte) 254;
    numArray10[7] = (byte) 50;
    numArray10[27] = (byte) 172;
    numArray10[29] = (byte) 72;
    numArray10[16 /*0x10*/] = (byte) 28;
    numArray10[1] = (byte) 26;
    numArray10[32 /*0x20*/] = (byte) 80 /*0x50*/;
    numArray10[33] = (byte) 136;
    numArray10[22] = (byte) 210;
    numArray10[17] = (byte) 193;
    numArray10[36] = (byte) 158;
    numArray10[28] = (byte) 43;
    numArray10[4] = (byte) 76;
    numArray10[5] = (byte) 32 /*0x20*/;
    numArray10[41] = (byte) 142;
    numArray10[38] = (byte) 228;
    numArray10[42] = (byte) 77;
    numArray10[31 /*0x1F*/] = (byte) 186;
    numArray10[44] = (byte) 210;
    numArray10[45] = (byte) 40;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 46);
    for (int index = 0; index < 46; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12440(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 49,
      (byte) 89,
      (byte) 187,
      (byte) 146,
      (byte) 25,
      (byte) 79,
      (byte) 231,
      (byte) 106,
      (byte) 133,
      (byte) 23,
      (byte) 94,
      (byte) 84,
      (byte) 33,
      (byte) 151,
      (byte) 189,
      (byte) 143,
      (byte) 84,
      (byte) 31 /*0x1F*/,
      (byte) 242,
      (byte) 118,
      (byte) 179,
      (byte) 105,
      (byte) 167,
      (byte) 229,
      (byte) 238,
      (byte) 231,
      (byte) 95,
      (byte) 119,
      (byte) 122,
      (byte) 194,
      (byte) 127 /*0x7F*/,
      (byte) 221,
      (byte) 35,
      (byte) 192 /*0xC0*/,
      (byte) 161,
      (byte) 15,
      byte.MaxValue,
      (byte) 232,
      (byte) 97,
      (byte) 112 /*0x70*/,
      (byte) 246,
      (byte) 216,
      (byte) 65,
      (byte) 139,
      (byte) 205,
      (byte) 233,
      (byte) 103,
      (byte) 195
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 37,
      (byte) 197,
      (byte) 42,
      (byte) 70,
      (byte) 126,
      (byte) 8,
      (byte) 184,
      (byte) 104,
      (byte) 252,
      (byte) 181,
      (byte) 223,
      (byte) 207,
      (byte) 2,
      (byte) 30,
      (byte) 219,
      (byte) 107,
      (byte) 201,
      (byte) 82,
      (byte) 196,
      (byte) 183,
      (byte) 106,
      (byte) 175,
      (byte) 56,
      (byte) 54,
      (byte) 73,
      (byte) 81,
      (byte) 249,
      (byte) 91,
      (byte) 101,
      (byte) 225,
      (byte) 196,
      (byte) 140,
      (byte) 23,
      (byte) 232,
      (byte) 251,
      (byte) 17,
      (byte) 141,
      (byte) 186,
      (byte) 45,
      (byte) 139,
      (byte) 212,
      (byte) 7,
      (byte) 217,
      (byte) 208 /*0xD0*/,
      (byte) 154,
      (byte) 120,
      (byte) 102,
      (byte) 221
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12441()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[10] = (byte) 239;
      numArray2[1] = (byte) 175;
      numArray2[2] = (byte) 150;
      numArray2[7] = (byte) 49;
      numArray2[13] = (byte) 129;
      numArray2[15] = (byte) 96 /*0x60*/;
      numArray2[6] = (byte) 163;
      numArray2[11] = (byte) 195;
      numArray2[17] = (byte) 173;
      numArray2[9] = (byte) 111;
      numArray2[4] = (byte) 178;
      numArray2[5] = (byte) 213;
      numArray2[12] = (byte) 152;
      numArray2[0] = (byte) 120;
      numArray2[3] = (byte) 200;
      numArray2[18] = (byte) 143;
      numArray2[16 /*0x10*/] = (byte) 246;
      numArray2[8] = (byte) 240 /*0xF0*/;
      numArray2[14] = (byte) 49;
      byte[] numArray3 = new byte[19]
      {
        (byte) 221,
        (byte) 134,
        (byte) 72,
        (byte) 111,
        (byte) 163,
        (byte) 161,
        (byte) 3,
        (byte) 81,
        (byte) 165,
        (byte) 97,
        (byte) 22,
        (byte) 191,
        (byte) 171,
        (byte) 70,
        (byte) 4,
        (byte) 184,
        (byte) 83,
        (byte) 120,
        (byte) 177
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[14] = (byte) 246;
    numArray5[0] = (byte) 48 /*0x30*/;
    numArray5[17] = (byte) 160 /*0xA0*/;
    numArray5[3] = (byte) 232;
    numArray5[4] = (byte) 126;
    numArray5[10] = (byte) 32 /*0x20*/;
    numArray5[12] = (byte) 97;
    numArray5[7] = (byte) 184;
    numArray5[8] = (byte) 7;
    numArray5[5] = (byte) 243;
    numArray5[15] = (byte) 47;
    numArray5[1] = (byte) 128 /*0x80*/;
    numArray5[6] = (byte) 75;
    numArray5[13] = (byte) 155;
    numArray5[2] = (byte) 134;
    numArray5[9] = (byte) 204;
    numArray5[11] = (byte) 5;
    numArray5[16 /*0x10*/] = (byte) 187;
    numArray5[18] = (byte) 150;
    byte[] numArray6 = new byte[19];
    numArray6[3] = (byte) 156;
    numArray6[1] = (byte) 187;
    numArray6[0] = (byte) 75;
    numArray6[10] = (byte) 117;
    numArray6[14] = (byte) 9;
    numArray6[13] = (byte) 173;
    numArray6[6] = (byte) 141;
    numArray6[16 /*0x10*/] = (byte) 151;
    numArray6[7] = (byte) 8;
    numArray6[9] = (byte) 150;
    numArray6[12] = (byte) 157;
    numArray6[11] = (byte) 56;
    numArray6[15] = (byte) 53;
    numArray6[5] = (byte) 227;
    numArray6[2] = (byte) 187;
    numArray6[18] = (byte) 225;
    numArray6[4] = (byte) 57;
    numArray6[17] = (byte) 57;
    numArray6[8] = (byte) 73;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12442()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 46,
        (byte) 250,
        (byte) 209,
        (byte) 175,
        (byte) 208 /*0xD0*/,
        (byte) 159,
        (byte) 58,
        (byte) 14,
        (byte) 246,
        (byte) 53,
        (byte) 201,
        (byte) 246,
        (byte) 109,
        (byte) 238,
        (byte) 82,
        (byte) 56,
        (byte) 132
      };
      byte[] numArray3 = new byte[17];
      numArray3[2] = (byte) 10;
      numArray3[9] = (byte) 206;
      numArray3[1] = (byte) 205;
      numArray3[8] = (byte) 175;
      numArray3[4] = (byte) 103;
      numArray3[5] = (byte) 19;
      numArray3[7] = (byte) 101;
      numArray3[15] = (byte) 13;
      numArray3[0] = (byte) 143;
      numArray3[12] = (byte) 58;
      numArray3[10] = (byte) 175;
      numArray3[11] = (byte) 184;
      numArray3[6] = (byte) 30;
      numArray3[13] = (byte) 153;
      numArray3[14] = (byte) 37;
      numArray3[3] = (byte) 177;
      numArray3[16 /*0x10*/] = (byte) 131;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 32 /*0x20*/,
      (byte) 184,
      (byte) 65,
      (byte) 87,
      (byte) 123,
      (byte) 119,
      (byte) 14,
      (byte) 165,
      (byte) 67,
      (byte) 88,
      (byte) 126,
      (byte) 82,
      (byte) 69,
      (byte) 168,
      (byte) 128 /*0x80*/,
      (byte) 182,
      (byte) 115
    };
    byte[] numArray6 = new byte[17];
    numArray6[6] = (byte) 215;
    numArray6[7] = (byte) 42;
    numArray6[9] = (byte) 155;
    numArray6[3] = (byte) 49;
    numArray6[1] = (byte) 81;
    numArray6[5] = (byte) 37;
    numArray6[16 /*0x10*/] = (byte) 15;
    numArray6[15] = (byte) 137;
    numArray6[8] = (byte) 15;
    numArray6[0] = (byte) 164;
    numArray6[10] = (byte) 109;
    numArray6[11] = (byte) 149;
    numArray6[12] = (byte) 70;
    numArray6[13] = (byte) 172;
    numArray6[14] = (byte) 97;
    numArray6[4] = (byte) 217;
    numArray6[2] = (byte) 170;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[48 /*0x30*/];
    byte[] response = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_12431.sspq, 116, (Array) numArray7, 0, 48 /*0x30*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12431.sspr, 116, (Array) numArray7, 0, 48 /*0x30*/);
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

  internal static string ssp_appserver_12443()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 241;
      numArray2[2] = (byte) 14;
      numArray2[1] = (byte) 243;
      numArray2[3] = (byte) 20;
      numArray2[5] = (byte) 70;
      numArray2[0] = (byte) 46;
      numArray2[9] = (byte) 50;
      numArray2[7] = (byte) 225;
      numArray2[4] = (byte) 78;
      numArray2[8] = (byte) 87;
      byte[] numArray3 = new byte[10]
      {
        (byte) 58,
        (byte) 131,
        (byte) 57,
        (byte) 246,
        (byte) 103,
        (byte) 22,
        (byte) 225,
        (byte) 62,
        (byte) 12,
        (byte) 206
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[21];
      byte[] response = new byte[21];
      Array.Copy((Array) sc_12431.sspq, 164, (Array) numArray4, 0, 21);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12431.sspr, 164, (Array) numArray4, 0, 21);
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
      (byte) 119,
      (byte) 230,
      (byte) 172,
      (byte) 33,
      (byte) 15,
      (byte) 130,
      (byte) 204,
      (byte) 123,
      (byte) 203,
      (byte) 84
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 196,
      (byte) 181,
      (byte) 89,
      (byte) 218,
      (byte) 233,
      (byte) 26,
      (byte) 52,
      (byte) 212,
      (byte) 198,
      (byte) 130
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12444()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 228,
        (byte) 48 /*0x30*/,
        (byte) 171,
        (byte) 130,
        (byte) 133,
        (byte) 158,
        (byte) 134,
        (byte) 190,
        (byte) 71,
        (byte) 185
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 243,
        (byte) 26,
        (byte) 109,
        (byte) 199,
        (byte) 17,
        (byte) 242,
        (byte) 207,
        (byte) 15,
        (byte) 40,
        (byte) 65
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
      (byte) 14,
      (byte) 177,
      (byte) 176 /*0xB0*/,
      (byte) 116,
      (byte) 1,
      (byte) 253,
      (byte) 91,
      (byte) 229,
      (byte) 132,
      (byte) 104
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 110,
      (byte) 191,
      (byte) 86,
      (byte) 128 /*0x80*/,
      (byte) 189,
      (byte) 47,
      (byte) 66,
      (byte) 218,
      (byte) 168,
      (byte) 196
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12445()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 87,
        (byte) 19,
        (byte) 173,
        (byte) 178,
        (byte) 128 /*0x80*/,
        (byte) 83,
        (byte) 47,
        (byte) 155,
        (byte) 23,
        (byte) 139
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 96 /*0x60*/,
        (byte) 200,
        (byte) 182,
        (byte) 218,
        (byte) 189,
        (byte) 74,
        (byte) 180,
        (byte) 35,
        (byte) 27,
        (byte) 52
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
      (byte) 73,
      (byte) 185,
      (byte) 158,
      (byte) 243,
      (byte) 205,
      (byte) 94,
      (byte) 169,
      (byte) 190,
      (byte) 201,
      (byte) 27
    };
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 254;
    numArray6[8] = (byte) 225;
    numArray6[2] = (byte) 220;
    numArray6[7] = (byte) 115;
    numArray6[4] = (byte) 135;
    numArray6[5] = (byte) 152;
    numArray6[1] = (byte) 182;
    numArray6[6] = (byte) 23;
    numArray6[3] = (byte) 172;
    numArray6[0] = (byte) 188;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12446()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 6,
        (byte) 210,
        (byte) 93,
        (byte) 169,
        (byte) 114,
        (byte) 153,
        (byte) 36,
        (byte) 144 /*0x90*/,
        (byte) 201,
        (byte) 230,
        (byte) 17,
        (byte) 43,
        (byte) 229,
        (byte) 116,
        (byte) 86,
        (byte) 241,
        (byte) 167,
        (byte) 9,
        (byte) 142,
        (byte) 248,
        (byte) 204,
        (byte) 66,
        (byte) 62,
        (byte) 203,
        (byte) 93
      };
      byte[] numArray3 = new byte[25]
      {
        (byte) 201,
        (byte) 126,
        (byte) 187,
        (byte) 171,
        (byte) 84,
        (byte) 199,
        (byte) 237,
        (byte) 60,
        (byte) 100,
        (byte) 93,
        (byte) 41,
        (byte) 86,
        (byte) 26,
        (byte) 56,
        (byte) 86,
        (byte) 193,
        (byte) 92,
        (byte) 129,
        (byte) 94,
        (byte) 83,
        (byte) 21,
        (byte) 197,
        (byte) 1,
        (byte) 72,
        (byte) 31 /*0x1F*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 177,
      (byte) 2,
      (byte) 63 /*0x3F*/,
      (byte) 26,
      (byte) 14,
      (byte) 5,
      (byte) 236,
      (byte) 251,
      (byte) 1,
      (byte) 0,
      (byte) 75,
      (byte) 47,
      (byte) 246,
      (byte) 79,
      (byte) 165,
      (byte) 91,
      (byte) 118,
      (byte) 165,
      (byte) 122,
      (byte) 83,
      (byte) 48 /*0x30*/,
      (byte) 64 /*0x40*/,
      (byte) 141,
      (byte) 205,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray6 = new byte[25];
    numArray6[0] = (byte) 221;
    numArray6[1] = (byte) 21;
    numArray6[2] = (byte) 241;
    numArray6[16 /*0x10*/] = (byte) 3;
    numArray6[3] = (byte) 123;
    numArray6[23] = (byte) 24;
    numArray6[6] = (byte) 198;
    numArray6[4] = (byte) 170;
    numArray6[19] = (byte) 38;
    numArray6[22] = (byte) 227;
    numArray6[9] = (byte) 217;
    numArray6[11] = (byte) 14;
    numArray6[10] = (byte) 235;
    numArray6[13] = (byte) 19;
    numArray6[14] = (byte) 32 /*0x20*/;
    numArray6[15] = (byte) 0;
    numArray6[5] = (byte) 193;
    numArray6[17] = (byte) 179;
    numArray6[12] = (byte) 163;
    numArray6[8] = (byte) 33;
    numArray6[20] = (byte) 86;
    numArray6[21] = (byte) 136;
    numArray6[24] = (byte) 140;
    numArray6[7] = (byte) 81;
    numArray6[18] = (byte) 214;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12447()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25];
      numArray2[24] = (byte) 15;
      numArray2[1] = (byte) 112 /*0x70*/;
      numArray2[17] = (byte) 126;
      numArray2[11] = (byte) 20;
      numArray2[4] = (byte) 121;
      numArray2[5] = (byte) 92;
      numArray2[6] = (byte) 56;
      numArray2[3] = (byte) 114;
      numArray2[8] = (byte) 102;
      numArray2[9] = (byte) 111;
      numArray2[0] = (byte) 212;
      numArray2[7] = (byte) 40;
      numArray2[12] = (byte) 228;
      numArray2[21] = (byte) 213;
      numArray2[14] = (byte) 112 /*0x70*/;
      numArray2[20] = (byte) 203;
      numArray2[16 /*0x10*/] = (byte) 181;
      numArray2[22] = (byte) 81;
      numArray2[2] = (byte) 116;
      numArray2[10] = (byte) 179;
      numArray2[15] = (byte) 222;
      numArray2[18] = (byte) 136;
      numArray2[19] = (byte) 155;
      numArray2[23] = (byte) 230;
      numArray2[13] = (byte) 214;
      byte[] numArray3 = new byte[25]
      {
        (byte) 157,
        (byte) 239,
        (byte) 80 /*0x50*/,
        (byte) 70,
        (byte) 60,
        (byte) 155,
        (byte) 192 /*0xC0*/,
        (byte) 217,
        (byte) 19,
        (byte) 32 /*0x20*/,
        (byte) 236,
        (byte) 36,
        (byte) 117,
        (byte) 179,
        (byte) 52,
        (byte) 55,
        (byte) 18,
        (byte) 33,
        (byte) 190,
        (byte) 42,
        (byte) 243,
        (byte) 81,
        (byte) 181,
        (byte) 112 /*0x70*/,
        (byte) 206
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25];
    numArray5[18] = (byte) 129;
    numArray5[22] = (byte) 107;
    numArray5[6] = (byte) 54;
    numArray5[0] = (byte) 37;
    numArray5[2] = byte.MaxValue;
    numArray5[15] = (byte) 139;
    numArray5[23] = (byte) 224 /*0xE0*/;
    numArray5[9] = (byte) 0;
    numArray5[1] = (byte) 47;
    numArray5[11] = (byte) 49;
    numArray5[8] = (byte) 232;
    numArray5[5] = (byte) 98;
    numArray5[7] = (byte) 171;
    numArray5[10] = (byte) 181;
    numArray5[14] = (byte) 147;
    numArray5[13] = (byte) 157;
    numArray5[16 /*0x10*/] = (byte) 1;
    numArray5[17] = (byte) 139;
    numArray5[3] = (byte) 31 /*0x1F*/;
    numArray5[19] = byte.MaxValue;
    numArray5[20] = (byte) 200;
    numArray5[21] = (byte) 243;
    numArray5[4] = (byte) 141;
    numArray5[12] = (byte) 31 /*0x1F*/;
    numArray5[24] = (byte) 208 /*0xD0*/;
    byte[] numArray6 = new byte[25];
    numArray6[24] = (byte) 254;
    numArray6[1] = (byte) 239;
    numArray6[2] = (byte) 54;
    numArray6[3] = (byte) 86;
    numArray6[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray6[5] = (byte) 14;
    numArray6[9] = (byte) 193;
    numArray6[12] = (byte) 60;
    numArray6[4] = (byte) 179;
    numArray6[11] = (byte) 0;
    numArray6[6] = (byte) 224 /*0xE0*/;
    numArray6[18] = (byte) 130;
    numArray6[19] = (byte) 155;
    numArray6[13] = (byte) 66;
    numArray6[10] = (byte) 4;
    numArray6[15] = (byte) 185;
    numArray6[23] = (byte) 64 /*0x40*/;
    numArray6[17] = (byte) 142;
    numArray6[8] = (byte) 12;
    numArray6[0] = (byte) 132;
    numArray6[20] = (byte) 131;
    numArray6[21] = (byte) 211;
    numArray6[22] = (byte) 58;
    numArray6[14] = (byte) 131;
    numArray6[7] = (byte) 140;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12448(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 165;
    sourceArray1[39] = (byte) 95;
    sourceArray1[4] = (byte) 56;
    sourceArray1[7] = (byte) 144 /*0x90*/;
    sourceArray1[23] = (byte) 115;
    sourceArray1[32 /*0x20*/] = (byte) 124;
    sourceArray1[27] = (byte) 252;
    sourceArray1[5] = (byte) 54;
    sourceArray1[26] = (byte) 108;
    sourceArray1[24] = (byte) 97;
    sourceArray1[10] = (byte) 94;
    sourceArray1[20] = (byte) 132;
    sourceArray1[46] = (byte) 178;
    sourceArray1[8] = (byte) 0;
    sourceArray1[14] = (byte) 211;
    sourceArray1[15] = (byte) 218;
    sourceArray1[16 /*0x10*/] = (byte) 240 /*0xF0*/;
    sourceArray1[47] = (byte) 154;
    sourceArray1[18] = (byte) 200;
    sourceArray1[19] = (byte) 15;
    sourceArray1[43] = (byte) 44;
    sourceArray1[21] = (byte) 27;
    sourceArray1[22] = (byte) 193;
    sourceArray1[28] = (byte) 30;
    sourceArray1[1] = (byte) 87;
    sourceArray1[25] = (byte) 88;
    sourceArray1[0] = (byte) 31 /*0x1F*/;
    sourceArray1[45] = (byte) 16 /*0x10*/;
    sourceArray1[6] = (byte) 217;
    sourceArray1[29] = (byte) 179;
    sourceArray1[30] = (byte) 254;
    sourceArray1[31 /*0x1F*/] = (byte) 182;
    sourceArray1[13] = (byte) 43;
    sourceArray1[33] = (byte) 206;
    sourceArray1[34] = (byte) 122;
    sourceArray1[2] = (byte) 227;
    sourceArray1[36] = (byte) 138;
    sourceArray1[37] = (byte) 62;
    sourceArray1[3] = (byte) 230;
    sourceArray1[17] = (byte) 25;
    sourceArray1[40] = (byte) 87;
    sourceArray1[38] = (byte) 108;
    sourceArray1[41] = (byte) 57;
    sourceArray1[9] = (byte) 31 /*0x1F*/;
    sourceArray1[44] = (byte) 20;
    sourceArray1[11] = (byte) 46;
    sourceArray1[12] = (byte) 0;
    sourceArray1[42] = (byte) 184;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 136,
      (byte) 51,
      (byte) 179,
      (byte) 133,
      (byte) 212,
      (byte) 138,
      (byte) 155,
      (byte) 93,
      (byte) 197,
      (byte) 251,
      (byte) 156,
      (byte) 9,
      (byte) 240 /*0xF0*/,
      (byte) 120,
      (byte) 0,
      (byte) 12,
      (byte) 99,
      (byte) 66,
      (byte) 69,
      (byte) 117,
      (byte) 184,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 180,
      (byte) 26,
      (byte) 98,
      (byte) 245,
      (byte) 153,
      (byte) 29,
      (byte) 81,
      (byte) 250,
      (byte) 115,
      (byte) 191,
      (byte) 148,
      (byte) 36,
      (byte) 107,
      (byte) 105,
      (byte) 106,
      (byte) 169,
      (byte) 45,
      (byte) 149,
      (byte) 222,
      (byte) 156,
      (byte) 52,
      (byte) 90,
      (byte) 174,
      (byte) 236,
      (byte) 7
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12449(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 128 /*0x80*/,
      (byte) 234,
      (byte) 173,
      (byte) 141,
      (byte) 17,
      (byte) 109,
      (byte) 218,
      (byte) 217,
      (byte) 177,
      (byte) 161,
      (byte) 1,
      (byte) 15,
      (byte) 51,
      (byte) 58,
      (byte) 247,
      (byte) 188,
      (byte) 49,
      (byte) 92,
      (byte) 60,
      (byte) 51,
      (byte) 198,
      (byte) 95,
      (byte) 57,
      (byte) 30,
      (byte) 53,
      (byte) 247,
      (byte) 32 /*0x20*/,
      (byte) 211,
      (byte) 62,
      (byte) 131,
      (byte) 247,
      (byte) 137,
      (byte) 242,
      (byte) 242,
      (byte) 37,
      (byte) 231,
      (byte) 114,
      (byte) 179,
      (byte) 222,
      (byte) 119,
      (byte) 152,
      (byte) 16 /*0x10*/,
      (byte) 32 /*0x20*/,
      (byte) 213,
      (byte) 13,
      (byte) 10,
      (byte) 134,
      (byte) 82
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 155,
      (byte) 184,
      (byte) 34,
      (byte) 244,
      (byte) 159,
      (byte) 213,
      (byte) 190,
      (byte) 213,
      (byte) 225,
      (byte) 212,
      (byte) 56,
      (byte) 32 /*0x20*/,
      (byte) 253,
      (byte) 240 /*0xF0*/,
      (byte) 84,
      (byte) 46,
      (byte) 111,
      (byte) 69,
      (byte) 186,
      (byte) 115,
      (byte) 163,
      (byte) 157,
      (byte) 70,
      (byte) 80 /*0x50*/,
      (byte) 63 /*0x3F*/,
      (byte) 179,
      (byte) 103,
      (byte) 202,
      (byte) 187,
      (byte) 148,
      (byte) 83,
      (byte) 74,
      (byte) 21,
      (byte) 126,
      (byte) 6,
      (byte) 74,
      (byte) 152,
      (byte) 237,
      (byte) 212,
      (byte) 185,
      (byte) 158,
      (byte) 46,
      (byte) 49,
      (byte) 181,
      (byte) 122,
      (byte) 144 /*0x90*/,
      (byte) 148
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12450(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 55;
    sourceArray1[34] = (byte) 186;
    sourceArray1[47] = (byte) 237;
    sourceArray1[9] = (byte) 155;
    sourceArray1[44] = (byte) 243;
    sourceArray1[5] = (byte) 88;
    sourceArray1[22] = (byte) 181;
    sourceArray1[7] = (byte) 76;
    sourceArray1[8] = (byte) 7;
    sourceArray1[10] = (byte) 45;
    sourceArray1[14] = (byte) 65;
    sourceArray1[45] = (byte) 43;
    sourceArray1[1] = (byte) 152;
    sourceArray1[13] = (byte) 29;
    sourceArray1[18] = (byte) 125;
    sourceArray1[12] = (byte) 120;
    sourceArray1[16 /*0x10*/] = (byte) 16 /*0x10*/;
    sourceArray1[2] = (byte) 32 /*0x20*/;
    sourceArray1[26] = (byte) 42;
    sourceArray1[19] = (byte) 126;
    sourceArray1[39] = (byte) 91;
    sourceArray1[46] = (byte) 149;
    sourceArray1[0] = (byte) 56;
    sourceArray1[23] = (byte) 62;
    sourceArray1[24] = (byte) 100;
    sourceArray1[15] = (byte) 21;
    sourceArray1[21] = (byte) 155;
    sourceArray1[17] = (byte) 74;
    sourceArray1[11] = (byte) 170;
    sourceArray1[29] = (byte) 52;
    sourceArray1[30] = (byte) 128 /*0x80*/;
    sourceArray1[32 /*0x20*/] = (byte) 232;
    sourceArray1[43] = (byte) 116;
    sourceArray1[33] = (byte) 187;
    sourceArray1[3] = (byte) 176 /*0xB0*/;
    sourceArray1[28] = (byte) 205;
    sourceArray1[31 /*0x1F*/] = (byte) 236;
    sourceArray1[20] = (byte) 197;
    sourceArray1[38] = (byte) 227;
    sourceArray1[27] = (byte) 113;
    sourceArray1[36] = (byte) 4;
    sourceArray1[41] = (byte) 42;
    sourceArray1[42] = (byte) 145;
    sourceArray1[40] = (byte) 196;
    sourceArray1[25] = (byte) 17;
    sourceArray1[35] = (byte) 90;
    sourceArray1[6] = (byte) 26;
    sourceArray1[4] = (byte) 143;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 85;
    sourceArray2[1] = (byte) 165;
    sourceArray2[2] = (byte) 145;
    sourceArray2[36] = (byte) 25;
    sourceArray2[44] = (byte) 210;
    sourceArray2[18] = (byte) 228;
    sourceArray2[6] = (byte) 27;
    sourceArray2[23] = (byte) 152;
    sourceArray2[38] = (byte) 184;
    sourceArray2[9] = (byte) 15;
    sourceArray2[43] = (byte) 99;
    sourceArray2[11] = (byte) 47;
    sourceArray2[30] = (byte) 232;
    sourceArray2[34] = (byte) 114;
    sourceArray2[14] = (byte) 252;
    sourceArray2[15] = (byte) 90;
    sourceArray2[16 /*0x10*/] = (byte) 119;
    sourceArray2[17] = (byte) 201;
    sourceArray2[4] = (byte) 186;
    sourceArray2[0] = (byte) 182;
    sourceArray2[20] = (byte) 81;
    sourceArray2[21] = (byte) 18;
    sourceArray2[22] = (byte) 202;
    sourceArray2[13] = (byte) 254;
    sourceArray2[24] = (byte) 53;
    sourceArray2[28] = (byte) 118;
    sourceArray2[31 /*0x1F*/] = (byte) 177;
    sourceArray2[39] = (byte) 153;
    sourceArray2[19] = (byte) 177;
    sourceArray2[3] = (byte) 84;
    sourceArray2[12] = (byte) 51;
    sourceArray2[29] = (byte) 193;
    sourceArray2[33] = (byte) 45;
    sourceArray2[46] = (byte) 65;
    sourceArray2[7] = (byte) 127 /*0x7F*/;
    sourceArray2[32 /*0x20*/] = (byte) 35;
    sourceArray2[42] = (byte) 36;
    sourceArray2[37] = (byte) 222;
    sourceArray2[40] = (byte) 13;
    sourceArray2[35] = (byte) 15;
    sourceArray2[25] = (byte) 221;
    sourceArray2[41] = (byte) 250;
    sourceArray2[5] = byte.MaxValue;
    sourceArray2[27] = (byte) 27;
    sourceArray2[26] = (byte) 160 /*0xA0*/;
    sourceArray2[10] = (byte) 86;
    sourceArray2[8] = (byte) 180;
    sourceArray2[47] = (byte) 250;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12451()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37];
      numArray2[0] = (byte) 99;
      numArray2[5] = (byte) 76;
      numArray2[20] = (byte) 65;
      numArray2[3] = (byte) 187;
      numArray2[28] = (byte) 230;
      numArray2[24] = (byte) 138;
      numArray2[6] = (byte) 69;
      numArray2[16 /*0x10*/] = (byte) 123;
      numArray2[8] = (byte) 205;
      numArray2[9] = (byte) 68;
      numArray2[10] = (byte) 237;
      numArray2[23] = (byte) 174;
      numArray2[35] = (byte) 185;
      numArray2[18] = (byte) 187;
      numArray2[12] = (byte) 199;
      numArray2[15] = (byte) 54;
      numArray2[1] = (byte) 59;
      numArray2[26] = (byte) 11;
      numArray2[17] = (byte) 72;
      numArray2[19] = (byte) 252;
      numArray2[14] = (byte) 1;
      numArray2[21] = (byte) 93;
      numArray2[2] = (byte) 158;
      numArray2[22] = (byte) 64 /*0x40*/;
      numArray2[4] = (byte) 147;
      numArray2[25] = (byte) 57;
      numArray2[7] = (byte) 0;
      numArray2[27] = (byte) 143;
      numArray2[31 /*0x1F*/] = (byte) 154;
      numArray2[29] = (byte) 197;
      numArray2[30] = (byte) 189;
      numArray2[13] = (byte) 10;
      numArray2[32 /*0x20*/] = (byte) 98;
      numArray2[33] = (byte) 20;
      numArray2[34] = (byte) 157;
      numArray2[11] = (byte) 61;
      numArray2[36] = (byte) 0;
      byte[] numArray3 = new byte[37]
      {
        (byte) 4,
        (byte) 247,
        (byte) 3,
        (byte) 91,
        (byte) 64 /*0x40*/,
        (byte) 5,
        (byte) 141,
        (byte) 16 /*0x10*/,
        (byte) 197,
        (byte) 72,
        (byte) 50,
        (byte) 89,
        (byte) 228,
        (byte) 47,
        (byte) 134,
        (byte) 164,
        (byte) 187,
        (byte) 24,
        (byte) 123,
        (byte) 63 /*0x3F*/,
        (byte) 186,
        (byte) 225,
        (byte) 117,
        (byte) 67,
        (byte) 249,
        (byte) 50,
        (byte) 104,
        (byte) 156,
        byte.MaxValue,
        (byte) 144 /*0x90*/,
        (byte) 144 /*0x90*/,
        (byte) 237,
        (byte) 129,
        (byte) 205,
        (byte) 132,
        (byte) 185,
        (byte) 1
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[37];
    byte[] numArray5 = new byte[37];
    numArray5[33] = (byte) 126;
    numArray5[34] = (byte) 39;
    numArray5[2] = (byte) 99;
    numArray5[7] = (byte) 251;
    numArray5[5] = (byte) 180;
    numArray5[4] = (byte) 20;
    numArray5[26] = (byte) 146;
    numArray5[19] = (byte) 181;
    numArray5[13] = (byte) 189;
    numArray5[9] = (byte) 128 /*0x80*/;
    numArray5[10] = (byte) 210;
    numArray5[0] = (byte) 211;
    numArray5[31 /*0x1F*/] = (byte) 230;
    numArray5[11] = (byte) 124;
    numArray5[14] = (byte) 16 /*0x10*/;
    numArray5[23] = (byte) 76;
    numArray5[22] = (byte) 141;
    numArray5[8] = (byte) 125;
    numArray5[6] = (byte) 155;
    numArray5[16 /*0x10*/] = (byte) 94;
    numArray5[15] = (byte) 3;
    numArray5[21] = (byte) 230;
    numArray5[35] = (byte) 34;
    numArray5[30] = (byte) 249;
    numArray5[24] = (byte) 118;
    numArray5[25] = (byte) 138;
    numArray5[32 /*0x20*/] = (byte) 37;
    numArray5[27] = (byte) 21;
    numArray5[28] = (byte) 130;
    numArray5[29] = (byte) 175;
    numArray5[1] = (byte) 205;
    numArray5[18] = (byte) 159;
    numArray5[12] = (byte) 151;
    numArray5[3] = (byte) 15;
    numArray5[17] = (byte) 90;
    numArray5[20] = (byte) 205;
    numArray5[36] = (byte) 206;
    byte[] numArray6 = new byte[37];
    numArray6[0] = (byte) 150;
    numArray6[31 /*0x1F*/] = (byte) 222;
    numArray6[28] = (byte) 207;
    numArray6[32 /*0x20*/] = (byte) 178;
    numArray6[10] = (byte) 216;
    numArray6[5] = (byte) 216;
    numArray6[6] = (byte) 178;
    numArray6[19] = (byte) 207;
    numArray6[8] = (byte) 159;
    numArray6[9] = (byte) 234;
    numArray6[23] = (byte) 113;
    numArray6[13] = (byte) 13;
    numArray6[22] = (byte) 44;
    numArray6[30] = (byte) 14;
    numArray6[2] = (byte) 228;
    numArray6[3] = (byte) 85;
    numArray6[18] = (byte) 26;
    numArray6[14] = (byte) 159;
    numArray6[12] = (byte) 154;
    numArray6[1] = (byte) 109;
    numArray6[20] = (byte) 225;
    numArray6[21] = (byte) 238;
    numArray6[17] = (byte) 155;
    numArray6[16 /*0x10*/] = (byte) 78;
    numArray6[24] = (byte) 224 /*0xE0*/;
    numArray6[25] = (byte) 222;
    numArray6[11] = (byte) 114;
    numArray6[27] = (byte) 3;
    numArray6[7] = (byte) 170;
    numArray6[29] = (byte) 101;
    numArray6[4] = (byte) 58;
    numArray6[15] = (byte) 55;
    numArray6[26] = (byte) 47;
    numArray6[33] = (byte) 3;
    numArray6[34] = (byte) 71;
    numArray6[35] = (byte) 231;
    numArray6[36] = (byte) 109;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12452()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[229];
      byte[] numArray2 = new byte[55]
      {
        (byte) 25,
        (byte) 150,
        (byte) 57,
        (byte) 79,
        (byte) 83,
        (byte) 14,
        (byte) 238,
        (byte) 84,
        (byte) 91,
        (byte) 10,
        (byte) 125,
        (byte) 100,
        (byte) 138,
        (byte) 191,
        (byte) 61,
        (byte) 44,
        (byte) 163,
        (byte) 111,
        (byte) 39,
        (byte) 95,
        (byte) 250,
        (byte) 19,
        (byte) 183,
        (byte) 48 /*0x30*/,
        (byte) 98,
        (byte) 155,
        (byte) 114,
        (byte) 226,
        (byte) 204,
        (byte) 43,
        (byte) 94,
        (byte) 227,
        (byte) 87,
        (byte) 207,
        (byte) 197,
        (byte) 98,
        (byte) 90,
        (byte) 234,
        (byte) 248,
        (byte) 13,
        (byte) 241,
        (byte) 103,
        (byte) 152,
        (byte) 45,
        (byte) 145,
        (byte) 183,
        (byte) 13,
        (byte) 171,
        (byte) 72,
        (byte) 204,
        (byte) 108,
        (byte) 21,
        (byte) 171,
        (byte) 250,
        (byte) 217
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 162,
        (byte) 149,
        (byte) 179,
        (byte) 109,
        (byte) 26,
        (byte) 48 /*0x30*/,
        (byte) 10,
        (byte) 39,
        (byte) 103,
        (byte) 29,
        (byte) 211,
        (byte) 223,
        (byte) 116,
        (byte) 237,
        (byte) 86,
        (byte) 127 /*0x7F*/,
        (byte) 67,
        (byte) 193,
        (byte) 201,
        (byte) 91,
        (byte) 42,
        (byte) 25,
        (byte) 110,
        (byte) 177,
        (byte) 237,
        (byte) 58,
        (byte) 222,
        (byte) 128 /*0x80*/,
        (byte) 100,
        (byte) 200,
        (byte) 243,
        (byte) 194,
        (byte) 217,
        (byte) 180,
        (byte) 118,
        (byte) 236,
        (byte) 237,
        (byte) 132,
        (byte) 181,
        (byte) 244,
        (byte) 229,
        (byte) 116,
        (byte) 155,
        (byte) 246,
        (byte) 7,
        (byte) 176 /*0xB0*/,
        (byte) 8,
        (byte) 41,
        (byte) 228,
        (byte) 87,
        (byte) 27,
        (byte) 98,
        (byte) 3,
        (byte) 170,
        (byte) 14
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 9,
        (byte) 237,
        (byte) 184,
        (byte) 156,
        (byte) 12,
        (byte) 135,
        (byte) 48 /*0x30*/,
        (byte) 171,
        (byte) 153,
        (byte) 13,
        (byte) 187,
        (byte) 249,
        (byte) 218,
        (byte) 209,
        (byte) 184,
        (byte) 119,
        (byte) 25,
        (byte) 109,
        (byte) 146,
        (byte) 123,
        (byte) 137,
        (byte) 159,
        (byte) 170,
        (byte) 141,
        (byte) 198,
        (byte) 153,
        (byte) 0,
        (byte) 190,
        (byte) 73,
        (byte) 56,
        (byte) 93,
        (byte) 245,
        (byte) 192 /*0xC0*/,
        (byte) 131,
        (byte) 81,
        (byte) 253,
        (byte) 63 /*0x3F*/,
        (byte) 113,
        (byte) 228,
        (byte) 224 /*0xE0*/,
        (byte) 104,
        (byte) 130,
        (byte) 247,
        (byte) 170,
        (byte) 81,
        (byte) 178,
        (byte) 110,
        (byte) 250,
        (byte) 179,
        (byte) 157,
        (byte) 146,
        (byte) 140,
        (byte) 71,
        (byte) 138,
        (byte) 86
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 185,
        (byte) 125,
        (byte) 202,
        (byte) 168,
        (byte) 41,
        (byte) 79,
        (byte) 240 /*0xF0*/,
        (byte) 182,
        (byte) 156,
        (byte) 220,
        (byte) 82,
        (byte) 145,
        (byte) 25,
        (byte) 29,
        (byte) 111,
        (byte) 126,
        (byte) 73,
        (byte) 127 /*0x7F*/,
        (byte) 50,
        (byte) 236,
        (byte) 118,
        (byte) 147,
        (byte) 109,
        (byte) 201,
        (byte) 253,
        (byte) 73,
        (byte) 234,
        (byte) 189,
        (byte) 233,
        (byte) 198,
        (byte) 233,
        (byte) 104,
        (byte) 97,
        (byte) 235,
        (byte) 222,
        (byte) 148,
        (byte) 102,
        (byte) 238,
        (byte) 78,
        (byte) 136,
        (byte) 63 /*0x3F*/,
        (byte) 38,
        (byte) 123,
        (byte) 200,
        (byte) 31 /*0x1F*/,
        (byte) 182,
        (byte) 97,
        (byte) 12,
        (byte) 25,
        (byte) 96 /*0x60*/,
        (byte) 91,
        (byte) 245,
        (byte) 215,
        (byte) 33,
        (byte) 66
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 42,
        (byte) 213,
        (byte) 115,
        (byte) 135,
        (byte) 113,
        (byte) 98,
        (byte) 97,
        (byte) 176 /*0xB0*/,
        (byte) 197,
        (byte) 56,
        (byte) 87,
        (byte) 128 /*0x80*/,
        (byte) 186,
        (byte) 55,
        (byte) 168,
        (byte) 137,
        (byte) 178,
        (byte) 18,
        (byte) 6,
        (byte) 167,
        (byte) 0,
        (byte) 60,
        (byte) 20,
        (byte) 43,
        (byte) 6,
        (byte) 11,
        (byte) 171,
        (byte) 166,
        (byte) 208 /*0xD0*/,
        (byte) 174,
        (byte) 54,
        (byte) 44,
        (byte) 231,
        (byte) 187,
        (byte) 21,
        (byte) 156,
        (byte) 113,
        (byte) 88,
        (byte) 246,
        (byte) 31 /*0x1F*/,
        (byte) 150,
        (byte) 249,
        (byte) 139,
        (byte) 121,
        (byte) 22,
        (byte) 121,
        (byte) 248,
        (byte) 102,
        (byte) 233,
        (byte) 2,
        (byte) 148,
        (byte) 119,
        (byte) 79,
        (byte) 194,
        (byte) 65
      };
      byte[] numArray7 = new byte[55];
      numArray7[5] = (byte) 102;
      numArray7[1] = (byte) 24;
      numArray7[41] = (byte) 175;
      numArray7[3] = (byte) 85;
      numArray7[4] = (byte) 108;
      numArray7[8] = (byte) 24;
      numArray7[52] = (byte) 250;
      numArray7[7] = (byte) 38;
      numArray7[6] = (byte) 128 /*0x80*/;
      numArray7[9] = (byte) 243;
      numArray7[29] = (byte) 183;
      numArray7[43] = (byte) 239;
      numArray7[12] = (byte) 86;
      numArray7[2] = (byte) 54;
      numArray7[20] = (byte) 68;
      numArray7[13] = (byte) 20;
      numArray7[39] = (byte) 232;
      numArray7[17] = (byte) 97;
      numArray7[18] = (byte) 110;
      numArray7[19] = (byte) 90;
      numArray7[45] = (byte) 7;
      numArray7[21] = (byte) 193;
      numArray7[22] = (byte) 207;
      numArray7[11] = (byte) 117;
      numArray7[24] = (byte) 222;
      numArray7[40] = (byte) 73;
      numArray7[50] = (byte) 183;
      numArray7[27] = (byte) 90;
      numArray7[35] = (byte) 38;
      numArray7[28] = (byte) 125;
      numArray7[34] = (byte) 245;
      numArray7[31 /*0x1F*/] = (byte) 98;
      numArray7[49] = (byte) 68;
      numArray7[33] = byte.MaxValue;
      numArray7[44] = (byte) 3;
      numArray7[16 /*0x10*/] = (byte) 226;
      numArray7[36] = (byte) 79;
      numArray7[25] = (byte) 77;
      numArray7[38] = (byte) 197;
      numArray7[48 /*0x30*/] = (byte) 220;
      numArray7[14] = (byte) 234;
      numArray7[15] = (byte) 199;
      numArray7[42] = (byte) 70;
      numArray7[37] = (byte) 251;
      numArray7[23] = (byte) 32 /*0x20*/;
      numArray7[32 /*0x20*/] = (byte) 28;
      numArray7[46] = (byte) 50;
      numArray7[47] = (byte) 231;
      numArray7[30] = (byte) 85;
      numArray7[10] = (byte) 128 /*0x80*/;
      numArray7[26] = (byte) 158;
      numArray7[51] = (byte) 237;
      numArray7[0] = (byte) 107;
      numArray7[53] = (byte) 124;
      numArray7[54] = (byte) 94;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[29] = (byte) 117;
      numArray8[44] = (byte) 114;
      numArray8[51] = (byte) 124;
      numArray8[3] = (byte) 113;
      numArray8[22] = (byte) 172;
      numArray8[5] = (byte) 17;
      numArray8[10] = (byte) 134;
      numArray8[2] = (byte) 83;
      numArray8[8] = (byte) 77;
      numArray8[0] = (byte) 51;
      numArray8[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
      numArray8[15] = (byte) 140;
      numArray8[54] = (byte) 1;
      numArray8[13] = (byte) 239;
      numArray8[50] = (byte) 234;
      numArray8[12] = (byte) 48 /*0x30*/;
      numArray8[16 /*0x10*/] = (byte) 125;
      numArray8[9] = (byte) 117;
      numArray8[18] = (byte) 237;
      numArray8[19] = (byte) 59;
      numArray8[53] = (byte) 2;
      numArray8[33] = (byte) 181;
      numArray8[49] = (byte) 206;
      numArray8[30] = (byte) 114;
      numArray8[24] = (byte) 0;
      numArray8[11] = (byte) 251;
      numArray8[26] = (byte) 227;
      numArray8[27] = (byte) 222;
      numArray8[28] = (byte) 100;
      numArray8[42] = (byte) 158;
      numArray8[41] = (byte) 51;
      numArray8[14] = (byte) 130;
      numArray8[32 /*0x20*/] = (byte) 232;
      numArray8[43] = (byte) 106;
      numArray8[34] = (byte) 244;
      numArray8[25] = (byte) 222;
      numArray8[17] = (byte) 53;
      numArray8[48 /*0x30*/] = (byte) 185;
      numArray8[38] = (byte) 220;
      numArray8[39] = (byte) 8;
      numArray8[40] = (byte) 119;
      numArray8[35] = (byte) 226;
      numArray8[6] = (byte) 253;
      numArray8[4] = (byte) 254;
      numArray8[37] = (byte) 247;
      numArray8[23] = (byte) 25;
      numArray8[21] = (byte) 40;
      numArray8[47] = (byte) 250;
      numArray8[20] = (byte) 36;
      numArray8[36] = (byte) 60;
      numArray8[7] = (byte) 67;
      numArray8[46] = (byte) 173;
      numArray8[52] = (byte) 76;
      numArray8[45] = (byte) 73;
      numArray8[1] = (byte) 244;
      byte[] numArray9 = new byte[55]
      {
        (byte) 65,
        (byte) 142,
        (byte) 19,
        (byte) 75,
        (byte) 4,
        (byte) 58,
        (byte) 161,
        (byte) 187,
        (byte) 192 /*0xC0*/,
        (byte) 249,
        (byte) 82,
        (byte) 217,
        (byte) 18,
        (byte) 152,
        (byte) 162,
        (byte) 143,
        byte.MaxValue,
        (byte) 149,
        (byte) 186,
        (byte) 230,
        (byte) 127 /*0x7F*/,
        (byte) 100,
        (byte) 170,
        (byte) 116,
        (byte) 139,
        (byte) 251,
        (byte) 207,
        (byte) 120,
        (byte) 179,
        (byte) 25,
        (byte) 155,
        (byte) 145,
        (byte) 80 /*0x50*/,
        (byte) 63 /*0x3F*/,
        (byte) 109,
        (byte) 146,
        (byte) 33,
        (byte) 196,
        (byte) 120,
        (byte) 93,
        (byte) 117,
        (byte) 248,
        (byte) 138,
        (byte) 171,
        (byte) 205,
        (byte) 196,
        (byte) 160 /*0xA0*/,
        (byte) 229,
        (byte) 123,
        (byte) 113,
        (byte) 79,
        (byte) 27,
        (byte) 135,
        (byte) 254,
        (byte) 147
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[9];
      numArray10[4] = (byte) 249;
      numArray10[5] = (byte) 73;
      numArray10[1] = (byte) 185;
      numArray10[3] = (byte) 70;
      numArray10[0] = (byte) 147;
      numArray10[6] = (byte) 77;
      numArray10[2] = (byte) 240 /*0xF0*/;
      numArray10[8] = (byte) 84;
      numArray10[7] = (byte) 13;
      byte[] numArray11 = new byte[9]
      {
        (byte) 181,
        (byte) 72,
        (byte) 106,
        (byte) 234,
        (byte) 7,
        (byte) 5,
        (byte) 2,
        (byte) 103,
        (byte) 137
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[15];
      byte[] response = new byte[15];
      Array.Copy((Array) sc_12431.sspq, 185, (Array) numArray12, 0, 15);
      key.Query(true, 335, numArray12, response);
      Array.Copy((Array) sc_12431.sspr, 185, (Array) numArray12, 0, 15);
      for (int index = 0; index < numArray12.Length; ++index)
      {
        if ((int) numArray12[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray13 = new byte[229];
    byte[] numArray14 = new byte[55]
    {
      (byte) 148,
      (byte) 94,
      (byte) 12,
      (byte) 121,
      (byte) 89,
      (byte) 215,
      (byte) 9,
      (byte) 110,
      (byte) 238,
      (byte) 20,
      (byte) 49,
      (byte) 100,
      (byte) 132,
      (byte) 93,
      (byte) 209,
      (byte) 117,
      (byte) 189,
      (byte) 43,
      (byte) 24,
      (byte) 56,
      (byte) 243,
      (byte) 93,
      (byte) 238,
      (byte) 50,
      (byte) 39,
      (byte) 57,
      (byte) 201,
      (byte) 133,
      (byte) 33,
      (byte) 167,
      (byte) 112 /*0x70*/,
      (byte) 223,
      (byte) 51,
      (byte) 211,
      (byte) 118,
      (byte) 14,
      (byte) 143,
      (byte) 74,
      (byte) 129,
      (byte) 163,
      (byte) 120,
      (byte) 142,
      (byte) 249,
      (byte) 246,
      (byte) 229,
      (byte) 194,
      (byte) 79,
      (byte) 44,
      (byte) 214,
      (byte) 46,
      (byte) 172,
      (byte) 104,
      (byte) 86,
      (byte) 148,
      (byte) 215
    };
    byte[] numArray15 = new byte[55]
    {
      (byte) 237,
      (byte) 69,
      (byte) 179,
      (byte) 75,
      (byte) 124,
      (byte) 38,
      (byte) 227,
      (byte) 21,
      (byte) 147,
      (byte) 147,
      (byte) 41,
      (byte) 71,
      (byte) 117,
      (byte) 135,
      (byte) 87,
      (byte) 14,
      (byte) 70,
      (byte) 232,
      (byte) 42,
      (byte) 154,
      (byte) 137,
      (byte) 215,
      (byte) 34,
      (byte) 10,
      (byte) 191,
      (byte) 244,
      (byte) 192 /*0xC0*/,
      (byte) 142,
      (byte) 204,
      (byte) 25,
      (byte) 165,
      (byte) 138,
      (byte) 71,
      (byte) 149,
      (byte) 92,
      (byte) 190,
      (byte) 238,
      (byte) 124,
      (byte) 157,
      (byte) 75,
      (byte) 31 /*0x1F*/,
      (byte) 79,
      (byte) 14,
      (byte) 198,
      (byte) 190,
      (byte) 191,
      (byte) 218,
      (byte) 73,
      (byte) 254,
      (byte) 126,
      (byte) 112 /*0x70*/,
      (byte) 83,
      (byte) 116,
      (byte) 127 /*0x7F*/,
      (byte) 212
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray13, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index] ^= numArray15[index];
    byte[] numArray16 = new byte[55];
    numArray16[30] = (byte) 158;
    numArray16[9] = (byte) 48 /*0x30*/;
    numArray16[13] = (byte) 230;
    numArray16[3] = (byte) 14;
    numArray16[19] = (byte) 99;
    numArray16[33] = (byte) 158;
    numArray16[24] = (byte) 156;
    numArray16[7] = (byte) 164;
    numArray16[54] = (byte) 121;
    numArray16[1] = (byte) 82;
    numArray16[10] = (byte) 176 /*0xB0*/;
    numArray16[2] = (byte) 31 /*0x1F*/;
    numArray16[28] = (byte) 7;
    numArray16[53] = (byte) 210;
    numArray16[14] = (byte) 115;
    numArray16[31 /*0x1F*/] = (byte) 252;
    numArray16[16 /*0x10*/] = (byte) 116;
    numArray16[15] = (byte) 170;
    numArray16[18] = (byte) 101;
    numArray16[8] = (byte) 81;
    numArray16[20] = (byte) 247;
    numArray16[21] = (byte) 76;
    numArray16[46] = (byte) 18;
    numArray16[23] = (byte) 234;
    numArray16[6] = (byte) 71;
    numArray16[25] = (byte) 175;
    numArray16[26] = (byte) 243;
    numArray16[39] = (byte) 66;
    numArray16[22] = (byte) 100;
    numArray16[51] = (byte) 212;
    numArray16[35] = (byte) 247;
    numArray16[50] = (byte) 225;
    numArray16[45] = (byte) 70;
    numArray16[52] = (byte) 113;
    numArray16[37] = (byte) 149;
    numArray16[0] = (byte) 217;
    numArray16[36] = (byte) 97;
    numArray16[11] = (byte) 230;
    numArray16[38] = (byte) 244;
    numArray16[5] = (byte) 76;
    numArray16[40] = (byte) 178;
    numArray16[41] = (byte) 82;
    numArray16[42] = (byte) 147;
    numArray16[34] = (byte) 181;
    numArray16[44] = (byte) 158;
    numArray16[29] = (byte) 136;
    numArray16[32 /*0x20*/] = (byte) 64 /*0x40*/;
    numArray16[47] = (byte) 71;
    numArray16[48 /*0x30*/] = (byte) 57;
    numArray16[49] = (byte) 190;
    numArray16[17] = (byte) 192 /*0xC0*/;
    numArray16[27] = (byte) 251;
    numArray16[12] = (byte) 195;
    numArray16[4] = (byte) 132;
    numArray16[43] = (byte) 145;
    byte[] numArray17 = new byte[55];
    numArray17[10] = (byte) 238;
    numArray17[1] = (byte) 49;
    numArray17[12] = (byte) 10;
    numArray17[3] = (byte) 254;
    numArray17[13] = (byte) 96 /*0x60*/;
    numArray17[5] = (byte) 83;
    numArray17[6] = (byte) 51;
    numArray17[7] = (byte) 102;
    numArray17[45] = (byte) 89;
    numArray17[16 /*0x10*/] = (byte) 252;
    numArray17[4] = (byte) 223;
    numArray17[15] = (byte) 215;
    numArray17[18] = (byte) 114;
    numArray17[51] = (byte) 150;
    numArray17[14] = (byte) 101;
    numArray17[21] = (byte) 142;
    numArray17[0] = (byte) 1;
    numArray17[17] = (byte) 85;
    numArray17[11] = (byte) 133;
    numArray17[19] = (byte) 159;
    numArray17[20] = (byte) 38;
    numArray17[9] = (byte) 142;
    numArray17[22] = (byte) 205;
    numArray17[8] = (byte) 83;
    numArray17[32 /*0x20*/] = (byte) 78;
    numArray17[25] = (byte) 251;
    numArray17[42] = (byte) 179;
    numArray17[27] = (byte) 166;
    numArray17[40] = (byte) 116;
    numArray17[37] = (byte) 53;
    numArray17[30] = (byte) 32 /*0x20*/;
    numArray17[46] = (byte) 53;
    numArray17[23] = (byte) 104;
    numArray17[33] = (byte) 137;
    numArray17[31 /*0x1F*/] = (byte) 168;
    numArray17[54] = (byte) 209;
    numArray17[36] = (byte) 94;
    numArray17[29] = (byte) 243;
    numArray17[38] = (byte) 94;
    numArray17[50] = (byte) 199;
    numArray17[26] = (byte) 43;
    numArray17[41] = (byte) 168;
    numArray17[52] = (byte) 139;
    numArray17[43] = (byte) 160 /*0xA0*/;
    numArray17[44] = byte.MaxValue;
    numArray17[47] = (byte) 110;
    numArray17[34] = (byte) 184;
    numArray17[39] = (byte) 140;
    numArray17[48 /*0x30*/] = (byte) 135;
    numArray17[49] = (byte) 245;
    numArray17[35] = (byte) 28;
    numArray17[2] = (byte) 242;
    numArray17[24] = (byte) 243;
    numArray17[53] = (byte) 134;
    numArray17[28] = (byte) 216;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray13, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 55] ^= numArray17[index];
    byte[] numArray18 = new byte[55];
    numArray18[28] = (byte) 101;
    numArray18[11] = (byte) 179;
    numArray18[20] = (byte) 156;
    numArray18[3] = (byte) 26;
    numArray18[53] = (byte) 159;
    numArray18[51] = (byte) 100;
    numArray18[18] = (byte) 142;
    numArray18[27] = (byte) 95;
    numArray18[8] = (byte) 237;
    numArray18[9] = (byte) 236;
    numArray18[10] = (byte) 175;
    numArray18[7] = (byte) 6;
    numArray18[47] = (byte) 125;
    numArray18[0] = (byte) 135;
    numArray18[31 /*0x1F*/] = (byte) 108;
    numArray18[34] = (byte) 187;
    numArray18[16 /*0x10*/] = (byte) 0;
    numArray18[17] = (byte) 112 /*0x70*/;
    numArray18[42] = (byte) 210;
    numArray18[19] = (byte) 69;
    numArray18[25] = (byte) 183;
    numArray18[21] = (byte) 161;
    numArray18[22] = (byte) 79;
    numArray18[43] = (byte) 122;
    numArray18[48 /*0x30*/] = (byte) 145;
    numArray18[54] = (byte) 170;
    numArray18[2] = (byte) 138;
    numArray18[13] = (byte) 218;
    numArray18[40] = (byte) 43;
    numArray18[29] = (byte) 40;
    numArray18[26] = (byte) 122;
    numArray18[23] = (byte) 193;
    numArray18[50] = (byte) 19;
    numArray18[33] = (byte) 170;
    numArray18[35] = (byte) 163;
    numArray18[24] = (byte) 133;
    numArray18[36] = (byte) 92;
    numArray18[37] = (byte) 31 /*0x1F*/;
    numArray18[38] = (byte) 85;
    numArray18[39] = (byte) 224 /*0xE0*/;
    numArray18[14] = (byte) 180;
    numArray18[41] = (byte) 49;
    numArray18[6] = (byte) 124;
    numArray18[5] = (byte) 128 /*0x80*/;
    numArray18[44] = (byte) 62;
    numArray18[45] = (byte) 91;
    numArray18[4] = (byte) 134;
    numArray18[15] = (byte) 198;
    numArray18[1] = (byte) 100;
    numArray18[49] = (byte) 149;
    numArray18[30] = (byte) 120;
    numArray18[46] = (byte) 204;
    numArray18[52] = (byte) 85;
    numArray18[32 /*0x20*/] = (byte) 112 /*0x70*/;
    numArray18[12] = (byte) 128 /*0x80*/;
    byte[] numArray19 = new byte[55];
    numArray19[25] = (byte) 35;
    numArray19[17] = (byte) 135;
    numArray19[0] = (byte) 238;
    numArray19[26] = (byte) 245;
    numArray19[32 /*0x20*/] = (byte) 117;
    numArray19[5] = (byte) 14;
    numArray19[6] = (byte) 136;
    numArray19[29] = (byte) 165;
    numArray19[8] = (byte) 222;
    numArray19[7] = (byte) 30;
    numArray19[54] = (byte) 155;
    numArray19[11] = (byte) 153;
    numArray19[22] = (byte) 145;
    numArray19[38] = (byte) 41;
    numArray19[14] = (byte) 117;
    numArray19[15] = (byte) 120;
    numArray19[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    numArray19[4] = (byte) 115;
    numArray19[18] = (byte) 219;
    numArray19[50] = (byte) 128 /*0x80*/;
    numArray19[3] = (byte) 144 /*0x90*/;
    numArray19[21] = (byte) 4;
    numArray19[43] = (byte) 137;
    numArray19[23] = (byte) 229;
    numArray19[9] = (byte) 154;
    numArray19[34] = (byte) 98;
    numArray19[48 /*0x30*/] = (byte) 168;
    numArray19[35] = (byte) 244;
    numArray19[46] = (byte) 239;
    numArray19[53] = (byte) 142;
    numArray19[20] = (byte) 124;
    numArray19[31 /*0x1F*/] = (byte) 123;
    numArray19[27] = (byte) 65;
    numArray19[33] = (byte) 78;
    numArray19[30] = (byte) 147;
    numArray19[2] = (byte) 217;
    numArray19[24] = (byte) 181;
    numArray19[37] = (byte) 34;
    numArray19[51] = (byte) 115;
    numArray19[42] = (byte) 55;
    numArray19[40] = (byte) 210;
    numArray19[41] = (byte) 220;
    numArray19[19] = (byte) 2;
    numArray19[44] = (byte) 155;
    numArray19[39] = (byte) 87;
    numArray19[45] = (byte) 97;
    numArray19[12] = (byte) 42;
    numArray19[47] = (byte) 200;
    numArray19[28] = (byte) 5;
    numArray19[49] = (byte) 124;
    numArray19[10] = (byte) 77;
    numArray19[13] = (byte) 245;
    numArray19[52] = (byte) 167;
    numArray19[1] = (byte) 193;
    numArray19[36] = (byte) 134;
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray13, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 110] ^= numArray19[index];
    byte[] numArray20 = new byte[55]
    {
      (byte) 190,
      (byte) 88,
      (byte) 241,
      (byte) 106,
      (byte) 1,
      (byte) 42,
      (byte) 205,
      (byte) 149,
      (byte) 186,
      (byte) 38,
      (byte) 20,
      (byte) 138,
      (byte) 49,
      (byte) 137,
      (byte) 54,
      (byte) 243,
      (byte) 3,
      (byte) 74,
      (byte) 26,
      (byte) 103,
      (byte) 39,
      (byte) 182,
      (byte) 107,
      (byte) 210,
      (byte) 137,
      (byte) 77,
      (byte) 81,
      (byte) 7,
      (byte) 22,
      (byte) 158,
      (byte) 209,
      (byte) 92,
      (byte) 107,
      (byte) 221,
      (byte) 142,
      (byte) 198,
      (byte) 176 /*0xB0*/,
      (byte) 102,
      (byte) 191,
      (byte) 149,
      (byte) 141,
      (byte) 16 /*0x10*/,
      (byte) 21,
      (byte) 127 /*0x7F*/,
      (byte) 12,
      (byte) 138,
      (byte) 206,
      (byte) 203,
      (byte) 164,
      (byte) 192 /*0xC0*/,
      (byte) 206,
      (byte) 46,
      (byte) 74,
      (byte) 236,
      (byte) 73
    };
    byte[] numArray21 = new byte[55]
    {
      (byte) 42,
      (byte) 20,
      (byte) 232,
      (byte) 177,
      (byte) 75,
      (byte) 117,
      (byte) 48 /*0x30*/,
      (byte) 42,
      (byte) 47,
      (byte) 223,
      (byte) 142,
      (byte) 81,
      (byte) 88,
      (byte) 254,
      (byte) 20,
      (byte) 49,
      (byte) 15,
      (byte) 5,
      (byte) 98,
      (byte) 53,
      (byte) 63 /*0x3F*/,
      (byte) 56,
      (byte) 141,
      (byte) 137,
      (byte) 55,
      (byte) 218,
      (byte) 204,
      (byte) 152,
      (byte) 40,
      (byte) 218,
      (byte) 95,
      (byte) 190,
      (byte) 14,
      (byte) 107,
      (byte) 252,
      (byte) 127 /*0x7F*/,
      (byte) 162,
      (byte) 29,
      (byte) 169,
      (byte) 69,
      (byte) 113,
      (byte) 55,
      (byte) 118,
      (byte) 91,
      (byte) 137,
      (byte) 31 /*0x1F*/,
      (byte) 106,
      (byte) 13,
      (byte) 120,
      (byte) 202,
      (byte) 197,
      (byte) 106,
      (byte) 191,
      (byte) 57,
      (byte) 117
    };
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray13, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 165] ^= numArray21[index];
    byte[] numArray22 = new byte[9];
    numArray22[1] = (byte) 224 /*0xE0*/;
    numArray22[2] = (byte) 179;
    numArray22[6] = (byte) 28;
    numArray22[3] = (byte) 217;
    numArray22[4] = (byte) 198;
    numArray22[0] = (byte) 30;
    numArray22[7] = (byte) 84;
    numArray22[5] = (byte) 127 /*0x7F*/;
    numArray22[8] = (byte) 184;
    byte[] numArray23 = new byte[9]
    {
      (byte) 207,
      (byte) 92,
      (byte) 214,
      (byte) 85,
      (byte) 115,
      (byte) 225,
      (byte) 103,
      (byte) 81,
      (byte) 9
    };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray13, 220, 9);
    for (int index = 0; index < 9; ++index)
      numArray13[index + 220] ^= numArray23[index];
    return Encoding.UTF8.GetString(numArray13);
  }

  internal static string ssp_appserver_12453()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[204];
      byte[] numArray2 = new byte[55]
      {
        byte.MaxValue,
        (byte) 127 /*0x7F*/,
        (byte) 133,
        (byte) 99,
        (byte) 85,
        (byte) 208 /*0xD0*/,
        (byte) 162,
        (byte) 17,
        (byte) 126,
        (byte) 148,
        (byte) 134,
        (byte) 217,
        (byte) 145,
        (byte) 133,
        (byte) 11,
        (byte) 187,
        (byte) 199,
        (byte) 144 /*0x90*/,
        (byte) 249,
        (byte) 42,
        (byte) 84,
        (byte) 106,
        (byte) 122,
        (byte) 208 /*0xD0*/,
        (byte) 52,
        (byte) 125,
        (byte) 49,
        (byte) 163,
        (byte) 197,
        (byte) 19,
        (byte) 50,
        (byte) 103,
        (byte) 1,
        (byte) 89,
        (byte) 225,
        (byte) 51,
        (byte) 83,
        (byte) 98,
        (byte) 42,
        (byte) 75,
        (byte) 178,
        (byte) 166,
        (byte) 161,
        (byte) 191,
        (byte) 116,
        (byte) 150,
        (byte) 177,
        (byte) 23,
        (byte) 127 /*0x7F*/,
        (byte) 200,
        (byte) 17,
        (byte) 134,
        (byte) 166,
        (byte) 193,
        (byte) 113
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 12,
        (byte) 152,
        (byte) 60,
        (byte) 39,
        (byte) 243,
        (byte) 108,
        (byte) 183,
        (byte) 89,
        (byte) 132,
        (byte) 163,
        (byte) 243,
        (byte) 168,
        (byte) 102,
        (byte) 148,
        (byte) 78,
        (byte) 217,
        (byte) 93,
        (byte) 112 /*0x70*/,
        (byte) 139,
        (byte) 65,
        (byte) 69,
        (byte) 181,
        (byte) 52,
        (byte) 223,
        (byte) 215,
        (byte) 221,
        (byte) 251,
        (byte) 148,
        (byte) 133,
        (byte) 146,
        (byte) 41,
        (byte) 110,
        (byte) 123,
        (byte) 149,
        (byte) 168,
        (byte) 141,
        (byte) 15,
        (byte) 104,
        (byte) 177,
        (byte) 197,
        (byte) 154,
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 205,
        (byte) 218,
        (byte) 246,
        (byte) 142,
        (byte) 214,
        (byte) 183,
        byte.MaxValue,
        (byte) 204,
        (byte) 82,
        (byte) 218,
        (byte) 192 /*0xC0*/,
        (byte) 105
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[39] = (byte) 91;
      numArray4[2] = (byte) 215;
      numArray4[26] = (byte) 253;
      numArray4[3] = (byte) 158;
      numArray4[16 /*0x10*/] = (byte) 173;
      numArray4[37] = (byte) 172;
      numArray4[8] = (byte) 30;
      numArray4[40] = (byte) 54;
      numArray4[6] = (byte) 106;
      numArray4[9] = (byte) 216;
      numArray4[15] = (byte) 101;
      numArray4[11] = (byte) 164;
      numArray4[41] = (byte) 94;
      numArray4[30] = (byte) 63 /*0x3F*/;
      numArray4[14] = (byte) 101;
      numArray4[13] = (byte) 104;
      numArray4[32 /*0x20*/] = (byte) 76;
      numArray4[17] = (byte) 120;
      numArray4[0] = (byte) 181;
      numArray4[19] = (byte) 201;
      numArray4[22] = (byte) 158;
      numArray4[21] = (byte) 92;
      numArray4[12] = (byte) 216;
      numArray4[23] = (byte) 203;
      numArray4[45] = (byte) 115;
      numArray4[33] = (byte) 46;
      numArray4[25] = (byte) 35;
      numArray4[27] = (byte) 2;
      numArray4[46] = (byte) 167;
      numArray4[29] = (byte) 192 /*0xC0*/;
      numArray4[44] = (byte) 121;
      numArray4[43] = (byte) 189;
      numArray4[42] = (byte) 85;
      numArray4[49] = (byte) 32 /*0x20*/;
      numArray4[34] = (byte) 158;
      numArray4[18] = (byte) 173;
      numArray4[36] = (byte) 83;
      numArray4[10] = (byte) 56;
      numArray4[4] = byte.MaxValue;
      numArray4[38] = (byte) 178;
      numArray4[7] = (byte) 97;
      numArray4[24] = (byte) 25;
      numArray4[31 /*0x1F*/] = (byte) 231;
      numArray4[1] = (byte) 46;
      numArray4[47] = (byte) 96 /*0x60*/;
      numArray4[35] = (byte) 51;
      numArray4[5] = (byte) 75;
      numArray4[52] = (byte) 196;
      numArray4[48 /*0x30*/] = (byte) 172;
      numArray4[20] = (byte) 186;
      numArray4[50] = (byte) 248;
      numArray4[51] = (byte) 73;
      numArray4[28] = (byte) 172;
      numArray4[53] = (byte) 204;
      numArray4[54] = (byte) 74;
      byte[] numArray5 = new byte[55];
      numArray5[22] = (byte) 114;
      numArray5[1] = (byte) 51;
      numArray5[28] = (byte) 135;
      numArray5[26] = (byte) 1;
      numArray5[52] = (byte) 6;
      numArray5[32 /*0x20*/] = (byte) 34;
      numArray5[6] = (byte) 226;
      numArray5[4] = (byte) 14;
      numArray5[8] = (byte) 118;
      numArray5[9] = (byte) 108;
      numArray5[54] = (byte) 206;
      numArray5[37] = (byte) 43;
      numArray5[12] = (byte) 226;
      numArray5[45] = (byte) 25;
      numArray5[49] = (byte) 223;
      numArray5[18] = (byte) 122;
      numArray5[16 /*0x10*/] = (byte) 158;
      numArray5[42] = (byte) 129;
      numArray5[15] = (byte) 113;
      numArray5[33] = (byte) 228;
      numArray5[48 /*0x30*/] = (byte) 124;
      numArray5[21] = (byte) 222;
      numArray5[3] = (byte) 144 /*0x90*/;
      numArray5[23] = (byte) 136;
      numArray5[24] = (byte) 150;
      numArray5[25] = (byte) 138;
      numArray5[7] = (byte) 91;
      numArray5[11] = (byte) 215;
      numArray5[40] = (byte) 53;
      numArray5[29] = (byte) 60;
      numArray5[30] = (byte) 57;
      numArray5[31 /*0x1F*/] = (byte) 178;
      numArray5[0] = (byte) 224 /*0xE0*/;
      numArray5[13] = (byte) 59;
      numArray5[14] = (byte) 81;
      numArray5[35] = (byte) 139;
      numArray5[36] = (byte) 229;
      numArray5[51] = (byte) 91;
      numArray5[27] = (byte) 224 /*0xE0*/;
      numArray5[34] = (byte) 80 /*0x50*/;
      numArray5[2] = (byte) 148;
      numArray5[39] = (byte) 91;
      numArray5[10] = (byte) 122;
      numArray5[43] = (byte) 142;
      numArray5[44] = (byte) 210;
      numArray5[41] = (byte) 103;
      numArray5[46] = (byte) 122;
      numArray5[47] = (byte) 1;
      numArray5[19] = (byte) 229;
      numArray5[20] = (byte) 243;
      numArray5[50] = (byte) 89;
      numArray5[17] = (byte) 205;
      numArray5[5] = (byte) 2;
      numArray5[53] = (byte) 21;
      numArray5[38] = (byte) 150;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 144 /*0x90*/,
        (byte) 82,
        (byte) 226,
        (byte) 172,
        (byte) 56,
        (byte) 171,
        (byte) 196,
        (byte) 49,
        (byte) 123,
        (byte) 145,
        (byte) 208 /*0xD0*/,
        (byte) 143,
        (byte) 153,
        (byte) 60,
        (byte) 143,
        (byte) 120,
        (byte) 234,
        (byte) 9,
        (byte) 51,
        (byte) 173,
        (byte) 59,
        (byte) 44,
        (byte) 73,
        (byte) 70,
        (byte) 242,
        (byte) 237,
        (byte) 58,
        (byte) 178,
        (byte) 220,
        (byte) 132,
        (byte) 209,
        (byte) 187,
        (byte) 177,
        (byte) 0,
        (byte) 253,
        (byte) 247,
        (byte) 148,
        (byte) 174,
        (byte) 130,
        (byte) 211,
        (byte) 157,
        (byte) 79,
        (byte) 128 /*0x80*/,
        (byte) 176 /*0xB0*/,
        (byte) 103,
        (byte) 131,
        (byte) 83,
        (byte) 6,
        (byte) 142,
        (byte) 28,
        (byte) 66,
        (byte) 106,
        (byte) 7,
        (byte) 149,
        (byte) 184
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 147,
        (byte) 175,
        (byte) 73,
        (byte) 57,
        (byte) 228,
        (byte) 151,
        (byte) 213,
        (byte) 157,
        (byte) 227,
        (byte) 46,
        (byte) 182,
        (byte) 9,
        (byte) 246,
        (byte) 85,
        (byte) 165,
        (byte) 17,
        (byte) 112 /*0x70*/,
        (byte) 210,
        (byte) 15,
        (byte) 86,
        (byte) 23,
        (byte) 141,
        (byte) 136,
        (byte) 239,
        (byte) 19,
        (byte) 40,
        (byte) 21,
        (byte) 37,
        (byte) 218,
        (byte) 233,
        (byte) 207,
        (byte) 15,
        (byte) 42,
        (byte) 102,
        (byte) 130,
        (byte) 52,
        (byte) 240 /*0xF0*/,
        (byte) 105,
        (byte) 58,
        (byte) 84,
        (byte) 116,
        (byte) 125,
        (byte) 159,
        (byte) 92,
        (byte) 26,
        (byte) 130,
        (byte) 185,
        (byte) 196,
        (byte) 166,
        (byte) 114,
        (byte) 79,
        (byte) 71,
        (byte) 91,
        (byte) 241,
        (byte) 11
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[39]
      {
        (byte) 155,
        (byte) 186,
        (byte) 74,
        (byte) 113,
        (byte) 18,
        (byte) 85,
        (byte) 246,
        (byte) 192 /*0xC0*/,
        (byte) 21,
        (byte) 221,
        (byte) 153,
        (byte) 63 /*0x3F*/,
        (byte) 152,
        (byte) 228,
        (byte) 60,
        (byte) 135,
        (byte) 95,
        (byte) 15,
        (byte) 235,
        (byte) 171,
        (byte) 153,
        (byte) 223,
        (byte) 110,
        (byte) 153,
        (byte) 52,
        (byte) 35,
        (byte) 191,
        (byte) 66,
        (byte) 102,
        (byte) 179,
        (byte) 28,
        (byte) 72,
        (byte) 153,
        (byte) 238,
        (byte) 248,
        (byte) 244,
        (byte) 55,
        (byte) 98,
        (byte) 138
      };
      byte[] numArray9 = new byte[39]
      {
        (byte) 19,
        (byte) 211,
        (byte) 72,
        (byte) 182,
        (byte) 34,
        byte.MaxValue,
        (byte) 235,
        (byte) 130,
        (byte) 45,
        (byte) 88,
        (byte) 232,
        (byte) 185,
        (byte) 173,
        (byte) 223,
        (byte) 57,
        (byte) 135,
        (byte) 16 /*0x10*/,
        (byte) 16 /*0x10*/,
        (byte) 38,
        (byte) 251,
        (byte) 122,
        (byte) 203,
        (byte) 100,
        (byte) 77,
        (byte) 204,
        (byte) 160 /*0xA0*/,
        (byte) 34,
        (byte) 163,
        (byte) 209,
        (byte) 54,
        (byte) 88,
        (byte) 56,
        (byte) 249,
        (byte) 220,
        (byte) 40,
        (byte) 75,
        (byte) 156,
        (byte) 77,
        (byte) 102
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[204];
    byte[] numArray11 = new byte[55];
    numArray11[16 /*0x10*/] = (byte) 61;
    numArray11[1] = (byte) 165;
    numArray11[12] = (byte) 110;
    numArray11[7] = (byte) 227;
    numArray11[28] = (byte) 79;
    numArray11[5] = (byte) 56;
    numArray11[6] = (byte) 6;
    numArray11[33] = (byte) 169;
    numArray11[26] = (byte) 235;
    numArray11[0] = (byte) 82;
    numArray11[8] = (byte) 167;
    numArray11[39] = (byte) 135;
    numArray11[49] = (byte) 134;
    numArray11[34] = (byte) 140;
    numArray11[23] = (byte) 80 /*0x50*/;
    numArray11[15] = (byte) 177;
    numArray11[3] = (byte) 198;
    numArray11[17] = (byte) 239;
    numArray11[18] = (byte) 149;
    numArray11[19] = (byte) 179;
    numArray11[54] = (byte) 150;
    numArray11[53] = (byte) 214;
    numArray11[22] = (byte) 193;
    numArray11[2] = (byte) 186;
    numArray11[52] = (byte) 101;
    numArray11[25] = (byte) 170;
    numArray11[20] = (byte) 89;
    numArray11[27] = (byte) 71;
    numArray11[50] = (byte) 36;
    numArray11[29] = (byte) 253;
    numArray11[11] = (byte) 184;
    numArray11[31 /*0x1F*/] = (byte) 241;
    numArray11[24] = (byte) 166;
    numArray11[4] = (byte) 246;
    numArray11[48 /*0x30*/] = (byte) 168;
    numArray11[35] = (byte) 200;
    numArray11[36] = (byte) 223;
    numArray11[37] = (byte) 109;
    numArray11[38] = (byte) 45;
    numArray11[30] = (byte) 133;
    numArray11[40] = (byte) 60;
    numArray11[41] = (byte) 131;
    numArray11[42] = (byte) 12;
    numArray11[10] = (byte) 204;
    numArray11[44] = (byte) 124;
    numArray11[45] = (byte) 224 /*0xE0*/;
    numArray11[46] = (byte) 127 /*0x7F*/;
    numArray11[47] = (byte) 59;
    numArray11[9] = (byte) 133;
    numArray11[14] = (byte) 75;
    numArray11[32 /*0x20*/] = (byte) 33;
    numArray11[51] = (byte) 155;
    numArray11[21] = (byte) 97;
    numArray11[13] = (byte) 188;
    numArray11[43] = (byte) 151;
    byte[] numArray12 = new byte[55]
    {
      (byte) 24,
      (byte) 242,
      (byte) 121,
      (byte) 236,
      (byte) 174,
      (byte) 55,
      (byte) 94,
      (byte) 236,
      (byte) 218,
      (byte) 205,
      (byte) 33,
      (byte) 88,
      (byte) 122,
      (byte) 43,
      (byte) 41,
      (byte) 62,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 102,
      (byte) 3,
      (byte) 96 /*0x60*/,
      (byte) 125,
      (byte) 97,
      (byte) 77,
      (byte) 100,
      (byte) 137,
      (byte) 191,
      (byte) 52,
      (byte) 230,
      (byte) 44,
      (byte) 68,
      (byte) 210,
      (byte) 22,
      (byte) 12,
      (byte) 143,
      (byte) 136,
      (byte) 26,
      (byte) 85,
      (byte) 177,
      (byte) 219,
      (byte) 4,
      (byte) 232,
      (byte) 135,
      (byte) 141,
      (byte) 141,
      (byte) 128 /*0x80*/,
      (byte) 70,
      (byte) 188,
      (byte) 129,
      (byte) 74,
      (byte) 96 /*0x60*/,
      (byte) 157,
      (byte) 17,
      (byte) 58,
      (byte) 144 /*0x90*/
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[8] = (byte) 79;
    numArray13[6] = (byte) 208 /*0xD0*/;
    numArray13[2] = (byte) 33;
    numArray13[10] = (byte) 179;
    numArray13[29] = (byte) 53;
    numArray13[5] = (byte) 203;
    numArray13[44] = (byte) 6;
    numArray13[7] = (byte) 21;
    numArray13[30] = (byte) 234;
    numArray13[9] = (byte) 77;
    numArray13[4] = (byte) 30;
    numArray13[11] = (byte) 147;
    numArray13[17] = (byte) 248;
    numArray13[13] = (byte) 92;
    numArray13[14] = (byte) 130;
    numArray13[1] = (byte) 90;
    numArray13[16 /*0x10*/] = (byte) 212;
    numArray13[12] = (byte) 159;
    numArray13[18] = (byte) 231;
    numArray13[48 /*0x30*/] = (byte) 211;
    numArray13[20] = (byte) 147;
    numArray13[33] = (byte) 42;
    numArray13[22] = (byte) 125;
    numArray13[23] = (byte) 112 /*0x70*/;
    numArray13[24] = (byte) 163;
    numArray13[25] = (byte) 122;
    numArray13[26] = (byte) 19;
    numArray13[27] = (byte) 131;
    numArray13[28] = (byte) 17;
    numArray13[0] = (byte) 149;
    numArray13[47] = (byte) 87;
    numArray13[45] = (byte) 120;
    numArray13[54] = (byte) 38;
    numArray13[50] = (byte) 130;
    numArray13[38] = (byte) 95;
    numArray13[52] = (byte) 216;
    numArray13[35] = (byte) 243;
    numArray13[46] = (byte) 134;
    numArray13[36] = (byte) 192 /*0xC0*/;
    numArray13[39] = (byte) 34;
    numArray13[40] = (byte) 146;
    numArray13[41] = (byte) 128 /*0x80*/;
    numArray13[21] = (byte) 51;
    numArray13[15] = (byte) 224 /*0xE0*/;
    numArray13[37] = (byte) 157;
    numArray13[43] = (byte) 251;
    numArray13[3] = (byte) 157;
    numArray13[51] = (byte) 102;
    numArray13[42] = (byte) 81;
    numArray13[49] = (byte) 248;
    numArray13[53] = (byte) 170;
    numArray13[31 /*0x1F*/] = (byte) 82;
    numArray13[34] = (byte) 223;
    numArray13[19] = (byte) 241;
    numArray13[32 /*0x20*/] = byte.MaxValue;
    byte[] numArray14 = new byte[55]
    {
      (byte) 64 /*0x40*/,
      (byte) 176 /*0xB0*/,
      (byte) 128 /*0x80*/,
      (byte) 254,
      (byte) 47,
      (byte) 0,
      (byte) 47,
      (byte) 142,
      (byte) 219,
      (byte) 57,
      (byte) 147,
      (byte) 99,
      (byte) 25,
      (byte) 212,
      (byte) 99,
      (byte) 206,
      (byte) 217,
      (byte) 113,
      (byte) 97,
      (byte) 186,
      (byte) 154,
      (byte) 112 /*0x70*/,
      (byte) 120,
      (byte) 164,
      (byte) 236,
      (byte) 18,
      (byte) 227,
      (byte) 236,
      (byte) 230,
      (byte) 203,
      (byte) 5,
      (byte) 137,
      (byte) 77,
      (byte) 24,
      (byte) 90,
      (byte) 112 /*0x70*/,
      (byte) 141,
      (byte) 1,
      (byte) 108,
      (byte) 59,
      (byte) 191,
      (byte) 217,
      (byte) 111,
      (byte) 201,
      (byte) 210,
      (byte) 216,
      (byte) 5,
      (byte) 57,
      (byte) 1,
      (byte) 230,
      (byte) 83,
      (byte) 85,
      (byte) 64 /*0x40*/,
      (byte) 103,
      (byte) 3
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 71,
      (byte) 207,
      (byte) 176 /*0xB0*/,
      (byte) 251,
      (byte) 156,
      (byte) 235,
      (byte) 103,
      (byte) 222,
      (byte) 231,
      (byte) 63 /*0x3F*/,
      (byte) 113,
      byte.MaxValue,
      (byte) 120,
      (byte) 17,
      (byte) 147,
      (byte) 222,
      (byte) 90,
      (byte) 25,
      (byte) 150,
      (byte) 169,
      (byte) 233,
      (byte) 195,
      (byte) 69,
      (byte) 226,
      (byte) 225,
      (byte) 108,
      (byte) 65,
      (byte) 102,
      (byte) 182,
      (byte) 101,
      (byte) 8,
      (byte) 239,
      (byte) 253,
      (byte) 188,
      (byte) 200,
      (byte) 211,
      (byte) 10,
      (byte) 110,
      (byte) 169,
      (byte) 144 /*0x90*/,
      (byte) 207,
      (byte) 186,
      (byte) 190,
      (byte) 165,
      (byte) 160 /*0xA0*/,
      (byte) 219,
      (byte) 203,
      (byte) 198,
      (byte) 184,
      (byte) 239,
      (byte) 185,
      (byte) 50,
      (byte) 125,
      (byte) 214,
      (byte) 112 /*0x70*/
    };
    byte[] numArray16 = new byte[55];
    numArray16[41] = (byte) 119;
    numArray16[14] = (byte) 63 /*0x3F*/;
    numArray16[2] = (byte) 178;
    numArray16[11] = (byte) 231;
    numArray16[4] = (byte) 205;
    numArray16[29] = (byte) 125;
    numArray16[18] = (byte) 209;
    numArray16[9] = (byte) 184;
    numArray16[7] = (byte) 208 /*0xD0*/;
    numArray16[1] = (byte) 127 /*0x7F*/;
    numArray16[19] = (byte) 190;
    numArray16[42] = (byte) 14;
    numArray16[12] = (byte) 77;
    numArray16[13] = (byte) 98;
    numArray16[25] = (byte) 142;
    numArray16[15] = (byte) 156;
    numArray16[52] = (byte) 115;
    numArray16[17] = (byte) 87;
    numArray16[34] = (byte) 75;
    numArray16[47] = (byte) 65;
    numArray16[23] = (byte) 27;
    numArray16[21] = (byte) 153;
    numArray16[10] = (byte) 120;
    numArray16[43] = (byte) 20;
    numArray16[24] = (byte) 128 /*0x80*/;
    numArray16[5] = (byte) 240 /*0xF0*/;
    numArray16[40] = (byte) 201;
    numArray16[27] = (byte) 8;
    numArray16[46] = (byte) 193;
    numArray16[39] = (byte) 50;
    numArray16[32 /*0x20*/] = (byte) 215;
    numArray16[0] = (byte) 34;
    numArray16[30] = (byte) 221;
    numArray16[48 /*0x30*/] = (byte) 247;
    numArray16[6] = (byte) 198;
    numArray16[22] = (byte) 212;
    numArray16[36] = (byte) 140;
    numArray16[37] = (byte) 50;
    numArray16[38] = (byte) 226;
    numArray16[20] = (byte) 140;
    numArray16[8] = (byte) 61;
    numArray16[26] = (byte) 91;
    numArray16[3] = (byte) 38;
    numArray16[31 /*0x1F*/] = (byte) 56;
    numArray16[33] = (byte) 246;
    numArray16[16 /*0x10*/] = (byte) 92;
    numArray16[45] = (byte) 119;
    numArray16[35] = (byte) 94;
    numArray16[44] = (byte) 65;
    numArray16[49] = (byte) 83;
    numArray16[50] = (byte) 128 /*0x80*/;
    numArray16[51] = (byte) 133;
    numArray16[28] = (byte) 70;
    numArray16[53] = (byte) 53;
    numArray16[54] = (byte) 247;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[39]
    {
      (byte) 139,
      (byte) 19,
      (byte) 142,
      (byte) 46,
      (byte) 83,
      (byte) 96 /*0x60*/,
      (byte) 214,
      (byte) 96 /*0x60*/,
      (byte) 15,
      (byte) 218,
      (byte) 112 /*0x70*/,
      (byte) 4,
      (byte) 117,
      (byte) 150,
      (byte) 150,
      (byte) 246,
      (byte) 30,
      (byte) 11,
      (byte) 155,
      (byte) 201,
      (byte) 48 /*0x30*/,
      (byte) 35,
      (byte) 44,
      (byte) 145,
      (byte) 254,
      (byte) 181,
      (byte) 170,
      (byte) 221,
      (byte) 19,
      (byte) 249,
      (byte) 43,
      (byte) 9,
      (byte) 198,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 64 /*0x40*/,
      (byte) 234,
      (byte) 183,
      (byte) 122
    };
    byte[] numArray18 = new byte[39]
    {
      (byte) 248,
      (byte) 25,
      (byte) 112 /*0x70*/,
      (byte) 203,
      (byte) 75,
      (byte) 1,
      byte.MaxValue,
      (byte) 113,
      (byte) 147,
      (byte) 118,
      (byte) 190,
      (byte) 208 /*0xD0*/,
      (byte) 226,
      (byte) 155,
      (byte) 169,
      (byte) 135,
      (byte) 98,
      (byte) 229,
      (byte) 31 /*0x1F*/,
      (byte) 115,
      (byte) 34,
      (byte) 15,
      (byte) 107,
      (byte) 80 /*0x50*/,
      (byte) 14,
      (byte) 142,
      (byte) 103,
      (byte) 214,
      (byte) 54,
      (byte) 144 /*0x90*/,
      (byte) 68,
      (byte) 105,
      (byte) 22,
      (byte) 208 /*0xD0*/,
      (byte) 124,
      (byte) 174,
      (byte) 1,
      (byte) 54,
      (byte) 150
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 39);
    for (int index = 0; index < 39; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_12454()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 55;
      numArray2[7] = (byte) 89;
      numArray2[4] = (byte) 133;
      numArray2[2] = (byte) 120;
      numArray2[0] = byte.MaxValue;
      numArray2[1] = (byte) 159;
      numArray2[6] = (byte) 204;
      numArray2[3] = (byte) 130;
      numArray2[8] = (byte) 240 /*0xF0*/;
      numArray2[9] = (byte) 166;
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[1] = (byte) 26;
      numArray3[2] = (byte) 16 /*0x10*/;
      numArray3[5] = (byte) 205;
      numArray3[3] = (byte) 46;
      numArray3[6] = (byte) 66;
      numArray3[9] = (byte) 57;
      numArray3[7] = (byte) 215;
      numArray3[8] = (byte) 19;
      numArray3[4] = (byte) 186;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 70;
    numArray5[9] = (byte) 58;
    numArray5[2] = (byte) 242;
    numArray5[3] = (byte) 133;
    numArray5[8] = (byte) 160 /*0xA0*/;
    numArray5[5] = (byte) 36;
    numArray5[0] = (byte) 20;
    numArray5[4] = (byte) 50;
    numArray5[1] = (byte) 44;
    numArray5[6] = (byte) 166;
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 146;
    numArray6[1] = (byte) 205;
    numArray6[9] = (byte) 162;
    numArray6[3] = (byte) 249;
    numArray6[8] = (byte) 188;
    numArray6[5] = (byte) 62;
    numArray6[4] = (byte) 39;
    numArray6[0] = (byte) 46;
    numArray6[6] = (byte) 147;
    numArray6[7] = (byte) 7;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12455(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 186,
      (byte) 10,
      (byte) 171,
      (byte) 107,
      (byte) 145,
      (byte) 81,
      (byte) 5,
      (byte) 116,
      (byte) 115,
      (byte) 2,
      (byte) 44,
      (byte) 109,
      (byte) 154,
      (byte) 252,
      (byte) 29,
      (byte) 224 /*0xE0*/,
      (byte) 180,
      (byte) 237,
      (byte) 246,
      (byte) 93,
      (byte) 26,
      (byte) 220,
      (byte) 185,
      (byte) 75,
      (byte) 196,
      (byte) 132,
      (byte) 196,
      (byte) 134,
      (byte) 174,
      (byte) 219,
      (byte) 39,
      (byte) 192 /*0xC0*/,
      (byte) 14,
      (byte) 246,
      (byte) 158,
      (byte) 226,
      (byte) 219,
      (byte) 112 /*0x70*/,
      (byte) 234,
      (byte) 27,
      (byte) 189,
      (byte) 133,
      (byte) 220,
      (byte) 54,
      (byte) 13,
      (byte) 0,
      (byte) 5,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 136,
      (byte) 147,
      (byte) 47,
      (byte) 98,
      (byte) 80 /*0x50*/,
      (byte) 146,
      (byte) 96 /*0x60*/,
      (byte) 111,
      (byte) 183,
      (byte) 9,
      (byte) 55,
      (byte) 168,
      (byte) 235,
      (byte) 170,
      (byte) 161,
      (byte) 36,
      (byte) 204,
      (byte) 214,
      (byte) 62,
      (byte) 54,
      (byte) 70,
      (byte) 46,
      (byte) 204,
      (byte) 6,
      (byte) 237,
      (byte) 7,
      (byte) 25,
      (byte) 176 /*0xB0*/,
      (byte) 23,
      byte.MaxValue,
      (byte) 19,
      (byte) 130,
      (byte) 57,
      (byte) 72,
      (byte) 94,
      (byte) 19,
      (byte) 129,
      (byte) 191,
      (byte) 103,
      (byte) 49,
      (byte) 167,
      (byte) 95,
      (byte) 134,
      (byte) 247,
      (byte) 80 /*0x50*/,
      (byte) 130,
      (byte) 10,
      (byte) 244
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[22];
    byte[] response2 = new byte[22];
    Array.Copy((Array) sc_12431.sspq, 200, (Array) numArray2, 0, 22);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12431.sspr, 200, (Array) numArray2, 0, 22);
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

  internal static string ssp_appserver_12456()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 149,
        byte.MaxValue,
        (byte) 130,
        (byte) 61,
        (byte) 196,
        (byte) 126,
        (byte) 223,
        (byte) 227,
        (byte) 157,
        (byte) 48 /*0x30*/
      };
      byte[] numArray3 = new byte[10];
      numArray3[2] = (byte) 150;
      numArray3[6] = (byte) 26;
      numArray3[4] = (byte) 46;
      numArray3[3] = (byte) 178;
      numArray3[8] = (byte) 149;
      numArray3[5] = (byte) 41;
      numArray3[1] = (byte) 102;
      numArray3[7] = (byte) 122;
      numArray3[0] = (byte) 95;
      numArray3[9] = (byte) 188;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 91,
      (byte) 161,
      (byte) 160 /*0xA0*/,
      (byte) 97,
      (byte) 136,
      (byte) 104,
      (byte) 12,
      (byte) 103,
      (byte) 76,
      (byte) 126
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 235,
      (byte) 77,
      (byte) 33,
      (byte) 61,
      (byte) 62,
      (byte) 103,
      (byte) 240 /*0xF0*/,
      (byte) 196,
      (byte) 177,
      (byte) 4
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[13];
    byte[] response = new byte[13];
    Array.Copy((Array) sc_12431.sspq, 222, (Array) numArray7, 0, 13);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12431.sspr, 222, (Array) numArray7, 0, 13);
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

  internal static string ssp_appserver_12457()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25];
      numArray2[13] = (byte) 201;
      numArray2[1] = (byte) 36;
      numArray2[21] = (byte) 231;
      numArray2[3] = (byte) 99;
      numArray2[4] = (byte) 214;
      numArray2[20] = (byte) 89;
      numArray2[2] = (byte) 77;
      numArray2[5] = (byte) 187;
      numArray2[8] = (byte) 36;
      numArray2[12] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 29;
      numArray2[15] = (byte) 232;
      numArray2[10] = (byte) 86;
      numArray2[11] = (byte) 123;
      numArray2[7] = (byte) 155;
      numArray2[14] = (byte) 139;
      numArray2[16 /*0x10*/] = (byte) 57;
      numArray2[17] = (byte) 26;
      numArray2[18] = (byte) 225;
      numArray2[19] = (byte) 194;
      numArray2[6] = (byte) 38;
      numArray2[0] = (byte) 122;
      numArray2[22] = (byte) 79;
      numArray2[23] = (byte) 223;
      numArray2[24] = (byte) 160 /*0xA0*/;
      byte[] numArray3 = new byte[25];
      numArray3[6] = (byte) 185;
      numArray3[17] = (byte) 165;
      numArray3[2] = (byte) 80 /*0x50*/;
      numArray3[21] = (byte) 162;
      numArray3[4] = (byte) 81;
      numArray3[5] = (byte) 19;
      numArray3[22] = (byte) 182;
      numArray3[7] = (byte) 201;
      numArray3[8] = (byte) 160 /*0xA0*/;
      numArray3[9] = (byte) 133;
      numArray3[15] = (byte) 219;
      numArray3[23] = (byte) 7;
      numArray3[18] = (byte) 211;
      numArray3[13] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 129;
      numArray3[11] = (byte) 242;
      numArray3[16 /*0x10*/] = (byte) 224 /*0xE0*/;
      numArray3[24] = (byte) 196;
      numArray3[1] = (byte) 54;
      numArray3[19] = (byte) 81;
      numArray3[10] = (byte) 145;
      numArray3[0] = (byte) 12;
      numArray3[12] = (byte) 51;
      numArray3[20] = (byte) 8;
      numArray3[3] = (byte) 215;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25];
    numArray5[8] = (byte) 48 /*0x30*/;
    numArray5[1] = (byte) 127 /*0x7F*/;
    numArray5[2] = byte.MaxValue;
    numArray5[24] = (byte) 194;
    numArray5[3] = (byte) 61;
    numArray5[5] = (byte) 128 /*0x80*/;
    numArray5[9] = (byte) 214;
    numArray5[7] = (byte) 175;
    numArray5[21] = (byte) 236;
    numArray5[22] = (byte) 100;
    numArray5[14] = (byte) 116;
    numArray5[10] = (byte) 84;
    numArray5[12] = (byte) 82;
    numArray5[13] = (byte) 195;
    numArray5[4] = (byte) 225;
    numArray5[19] = (byte) 173;
    numArray5[11] = (byte) 90;
    numArray5[17] = (byte) 218;
    numArray5[18] = (byte) 135;
    numArray5[6] = (byte) 192 /*0xC0*/;
    numArray5[15] = (byte) 142;
    numArray5[20] = (byte) 207;
    numArray5[0] = (byte) 136;
    numArray5[23] = (byte) 138;
    numArray5[16 /*0x10*/] = (byte) 68;
    byte[] numArray6 = new byte[25];
    numArray6[21] = (byte) 66;
    numArray6[1] = (byte) 149;
    numArray6[2] = (byte) 138;
    numArray6[24] = (byte) 223;
    numArray6[4] = (byte) 192 /*0xC0*/;
    numArray6[5] = (byte) 4;
    numArray6[6] = (byte) 182;
    numArray6[15] = (byte) 202;
    numArray6[8] = (byte) 130;
    numArray6[9] = (byte) 168;
    numArray6[10] = (byte) 175;
    numArray6[13] = (byte) 126;
    numArray6[12] = (byte) 15;
    numArray6[16 /*0x10*/] = (byte) 166;
    numArray6[11] = (byte) 51;
    numArray6[0] = (byte) 59;
    numArray6[22] = (byte) 197;
    numArray6[17] = (byte) 170;
    numArray6[3] = (byte) 89;
    numArray6[18] = (byte) 1;
    numArray6[20] = (byte) 62;
    numArray6[19] = (byte) 91;
    numArray6[7] = (byte) 251;
    numArray6[23] = (byte) 148;
    numArray6[14] = (byte) 147;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12458()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25];
      numArray2[17] = (byte) 192 /*0xC0*/;
      numArray2[12] = (byte) 203;
      numArray2[13] = (byte) 38;
      numArray2[8] = (byte) 239;
      numArray2[11] = (byte) 39;
      numArray2[5] = (byte) 130;
      numArray2[6] = (byte) 142;
      numArray2[2] = (byte) 113;
      numArray2[16 /*0x10*/] = (byte) 225;
      numArray2[9] = (byte) 234;
      numArray2[0] = (byte) 64 /*0x40*/;
      numArray2[7] = (byte) 15;
      numArray2[1] = (byte) 2;
      numArray2[10] = (byte) 72;
      numArray2[14] = (byte) 31 /*0x1F*/;
      numArray2[22] = (byte) 183;
      numArray2[3] = (byte) 195;
      numArray2[4] = (byte) 192 /*0xC0*/;
      numArray2[18] = (byte) 6;
      numArray2[19] = (byte) 221;
      numArray2[20] = (byte) 190;
      numArray2[21] = (byte) 146;
      numArray2[15] = (byte) 98;
      numArray2[23] = (byte) 21;
      numArray2[24] = (byte) 92;
      byte[] numArray3 = new byte[25]
      {
        (byte) 147,
        (byte) 95,
        (byte) 1,
        (byte) 225,
        (byte) 60,
        (byte) 29,
        (byte) 101,
        (byte) 233,
        (byte) 117,
        (byte) 201,
        (byte) 47,
        (byte) 72,
        (byte) 14,
        (byte) 252,
        (byte) 85,
        (byte) 72,
        (byte) 102,
        (byte) 3,
        (byte) 225,
        (byte) 86,
        (byte) 84,
        (byte) 160 /*0xA0*/,
        (byte) 81,
        (byte) 204,
        (byte) 252
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 160 /*0xA0*/,
      (byte) 19,
      (byte) 173,
      (byte) 74,
      (byte) 91,
      (byte) 102,
      (byte) 80 /*0x50*/,
      (byte) 172,
      (byte) 59,
      (byte) 205,
      (byte) 209,
      (byte) 186,
      (byte) 94,
      (byte) 139,
      (byte) 41,
      (byte) 234,
      (byte) 84,
      (byte) 183,
      (byte) 54,
      (byte) 80 /*0x50*/,
      (byte) 85,
      (byte) 98,
      (byte) 213,
      (byte) 146,
      (byte) 71
    };
    byte[] numArray6 = new byte[25];
    numArray6[23] = (byte) 18;
    numArray6[15] = (byte) 2;
    numArray6[2] = (byte) 229;
    numArray6[11] = (byte) 30;
    numArray6[18] = (byte) 44;
    numArray6[1] = (byte) 222;
    numArray6[5] = (byte) 224 /*0xE0*/;
    numArray6[6] = (byte) 90;
    numArray6[8] = (byte) 227;
    numArray6[9] = (byte) 158;
    numArray6[10] = (byte) 182;
    numArray6[17] = (byte) 155;
    numArray6[24] = (byte) 76;
    numArray6[13] = (byte) 99;
    numArray6[14] = (byte) 122;
    numArray6[16 /*0x10*/] = (byte) 195;
    numArray6[0] = (byte) 179;
    numArray6[3] = (byte) 49;
    numArray6[4] = (byte) 122;
    numArray6[19] = (byte) 141;
    numArray6[20] = (byte) 6;
    numArray6[21] = (byte) 155;
    numArray6[7] = (byte) 107;
    numArray6[12] = (byte) 26;
    numArray6[22] = (byte) 104;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12459(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 61,
      (byte) 62,
      (byte) 27,
      (byte) 3,
      (byte) 220,
      (byte) 19,
      (byte) 99,
      (byte) 83,
      (byte) 184,
      (byte) 50,
      (byte) 47,
      (byte) 167,
      (byte) 31 /*0x1F*/,
      (byte) 143,
      (byte) 154,
      (byte) 93,
      (byte) 211,
      (byte) 104,
      (byte) 241,
      (byte) 48 /*0x30*/,
      (byte) 80 /*0x50*/,
      (byte) 36,
      (byte) 185,
      (byte) 100,
      (byte) 146,
      (byte) 71,
      (byte) 212,
      (byte) 146,
      (byte) 199,
      (byte) 9,
      (byte) 170,
      (byte) 93,
      (byte) 70,
      (byte) 232,
      (byte) 189,
      (byte) 80 /*0x50*/,
      (byte) 114,
      (byte) 125,
      (byte) 114,
      (byte) 48 /*0x30*/,
      (byte) 188,
      (byte) 177,
      (byte) 134,
      (byte) 233,
      (byte) 254,
      (byte) 14,
      (byte) 24,
      (byte) 238
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 126;
    sourceArray2[20] = (byte) 190;
    sourceArray2[6] = (byte) 168;
    sourceArray2[17] = (byte) 248;
    sourceArray2[16 /*0x10*/] = (byte) 8;
    sourceArray2[5] = (byte) 150;
    sourceArray2[32 /*0x20*/] = (byte) 31 /*0x1F*/;
    sourceArray2[38] = (byte) 140;
    sourceArray2[31 /*0x1F*/] = (byte) 124;
    sourceArray2[9] = (byte) 197;
    sourceArray2[33] = (byte) 80 /*0x50*/;
    sourceArray2[37] = (byte) 53;
    sourceArray2[12] = (byte) 131;
    sourceArray2[18] = (byte) 182;
    sourceArray2[39] = (byte) 80 /*0x50*/;
    sourceArray2[15] = (byte) 145;
    sourceArray2[46] = (byte) 95;
    sourceArray2[42] = (byte) 62;
    sourceArray2[23] = (byte) 189;
    sourceArray2[19] = (byte) 188;
    sourceArray2[47] = (byte) 43;
    sourceArray2[13] = (byte) 35;
    sourceArray2[22] = (byte) 50;
    sourceArray2[21] = (byte) 88;
    sourceArray2[24] = (byte) 179;
    sourceArray2[25] = (byte) 184;
    sourceArray2[26] = (byte) 202;
    sourceArray2[3] = (byte) 191;
    sourceArray2[28] = (byte) 144 /*0x90*/;
    sourceArray2[29] = (byte) 212;
    sourceArray2[30] = (byte) 127 /*0x7F*/;
    sourceArray2[2] = (byte) 247;
    sourceArray2[0] = (byte) 188;
    sourceArray2[27] = (byte) 230;
    sourceArray2[34] = (byte) 251;
    sourceArray2[35] = (byte) 129;
    sourceArray2[36] = (byte) 150;
    sourceArray2[14] = (byte) 222;
    sourceArray2[10] = (byte) 78;
    sourceArray2[1] = (byte) 172;
    sourceArray2[45] = (byte) 30;
    sourceArray2[40] = (byte) 143;
    sourceArray2[7] = (byte) 230;
    sourceArray2[43] = (byte) 10;
    sourceArray2[44] = (byte) 45;
    sourceArray2[11] = (byte) 184;
    sourceArray2[8] = (byte) 164;
    sourceArray2[4] = (byte) 98;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12460()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 172,
        (byte) 225,
        (byte) 28,
        (byte) 117,
        (byte) 73,
        (byte) 80 /*0x50*/,
        (byte) 145,
        (byte) 145,
        (byte) 40,
        (byte) 103
      };
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 167;
      numArray3[9] = (byte) 25;
      numArray3[2] = (byte) 107;
      numArray3[3] = (byte) 190;
      numArray3[4] = (byte) 123;
      numArray3[6] = (byte) 64 /*0x40*/;
      numArray3[1] = (byte) 8;
      numArray3[7] = (byte) 151;
      numArray3[5] = (byte) 245;
      numArray3[8] = (byte) 166;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_12431.sspq, 235, (Array) numArray4, 0, 14);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12431.sspr, 235, (Array) numArray4, 0, 14);
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
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 145;
    numArray6[0] = (byte) 72;
    numArray6[1] = (byte) 18;
    numArray6[3] = (byte) 119;
    numArray6[6] = (byte) 39;
    numArray6[9] = (byte) 31 /*0x1F*/;
    numArray6[4] = (byte) 128 /*0x80*/;
    numArray6[7] = (byte) 22;
    numArray6[5] = (byte) 122;
    numArray6[8] = (byte) 106;
    byte[] numArray7 = new byte[10]
    {
      (byte) 39,
      (byte) 198,
      (byte) 56,
      (byte) 50,
      (byte) 188,
      (byte) 107,
      (byte) 234,
      (byte) 130,
      (byte) 139,
      (byte) 170
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12461()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[191];
      byte[] numArray2 = new byte[55]
      {
        (byte) 194,
        (byte) 159,
        (byte) 237,
        (byte) 183,
        (byte) 69,
        (byte) 22,
        (byte) 22,
        (byte) 22,
        (byte) 142,
        (byte) 161,
        (byte) 177,
        (byte) 57,
        (byte) 210,
        (byte) 80 /*0x50*/,
        (byte) 55,
        (byte) 218,
        (byte) 6,
        (byte) 104,
        (byte) 196,
        (byte) 42,
        (byte) 157,
        (byte) 54,
        (byte) 157,
        (byte) 77,
        (byte) 56,
        (byte) 114,
        (byte) 215,
        (byte) 225,
        (byte) 194,
        (byte) 220,
        (byte) 103,
        (byte) 46,
        (byte) 10,
        (byte) 23,
        (byte) 181,
        (byte) 225,
        (byte) 140,
        (byte) 112 /*0x70*/,
        (byte) 209,
        (byte) 83,
        (byte) 9,
        (byte) 99,
        (byte) 10,
        (byte) 74,
        (byte) 121,
        (byte) 83,
        (byte) 41,
        (byte) 17,
        (byte) 189,
        (byte) 177,
        (byte) 231,
        (byte) 141,
        (byte) 85,
        (byte) 221,
        (byte) 220
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 245,
        (byte) 103,
        (byte) 73,
        (byte) 155,
        (byte) 137,
        (byte) 197,
        (byte) 248,
        (byte) 179,
        (byte) 223,
        (byte) 228,
        (byte) 229,
        (byte) 34,
        (byte) 83,
        (byte) 82,
        (byte) 8,
        (byte) 84,
        (byte) 181,
        (byte) 10,
        (byte) 94,
        (byte) 18,
        (byte) 73,
        (byte) 54,
        (byte) 249,
        (byte) 87,
        (byte) 6,
        (byte) 45,
        (byte) 106,
        (byte) 2,
        (byte) 99,
        (byte) 180,
        (byte) 78,
        (byte) 254,
        (byte) 137,
        (byte) 202,
        (byte) 123,
        (byte) 167,
        (byte) 166,
        (byte) 110,
        (byte) 196,
        (byte) 91,
        (byte) 114,
        (byte) 222,
        (byte) 198,
        (byte) 222,
        (byte) 99,
        (byte) 175,
        (byte) 31 /*0x1F*/,
        (byte) 209,
        (byte) 31 /*0x1F*/,
        (byte) 175,
        (byte) 189,
        (byte) 229,
        (byte) 84,
        (byte) 213,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[27] = (byte) 244;
      numArray4[53] = (byte) 94;
      numArray4[2] = (byte) 210;
      numArray4[3] = (byte) 63 /*0x3F*/;
      numArray4[4] = (byte) 52;
      numArray4[5] = (byte) 83;
      numArray4[44] = (byte) 110;
      numArray4[39] = (byte) 15;
      numArray4[17] = (byte) 33;
      numArray4[54] = (byte) 173;
      numArray4[9] = (byte) 46;
      numArray4[36] = (byte) 254;
      numArray4[12] = (byte) 85;
      numArray4[41] = (byte) 16 /*0x10*/;
      numArray4[14] = (byte) 150;
      numArray4[15] = (byte) 151;
      numArray4[16 /*0x10*/] = (byte) 113;
      numArray4[6] = (byte) 178;
      numArray4[10] = (byte) 187;
      numArray4[11] = (byte) 155;
      numArray4[20] = (byte) 83;
      numArray4[28] = (byte) 217;
      numArray4[22] = (byte) 229;
      numArray4[29] = (byte) 216;
      numArray4[26] = (byte) 16 /*0x10*/;
      numArray4[19] = (byte) 44;
      numArray4[18] = (byte) 58;
      numArray4[31 /*0x1F*/] = (byte) 59;
      numArray4[7] = (byte) 57;
      numArray4[42] = (byte) 246;
      numArray4[30] = (byte) 45;
      numArray4[34] = (byte) 237;
      numArray4[47] = (byte) 100;
      numArray4[33] = (byte) 224 /*0xE0*/;
      numArray4[1] = (byte) 126;
      numArray4[8] = (byte) 131;
      numArray4[0] = (byte) 159;
      numArray4[24] = (byte) 197;
      numArray4[25] = (byte) 36;
      numArray4[32 /*0x20*/] = (byte) 42;
      numArray4[40] = (byte) 88;
      numArray4[38] = (byte) 212;
      numArray4[35] = (byte) 241;
      numArray4[43] = (byte) 36;
      numArray4[48 /*0x30*/] = (byte) 80 /*0x50*/;
      numArray4[45] = (byte) 118;
      numArray4[46] = (byte) 103;
      numArray4[37] = (byte) 20;
      numArray4[13] = (byte) 77;
      numArray4[49] = (byte) 246;
      numArray4[50] = (byte) 228;
      numArray4[51] = (byte) 187;
      numArray4[52] = (byte) 161;
      numArray4[23] = (byte) 72;
      numArray4[21] = (byte) 212;
      byte[] numArray5 = new byte[55];
      numArray5[24] = (byte) 22;
      numArray5[1] = (byte) 219;
      numArray5[28] = (byte) 45;
      numArray5[3] = (byte) 244;
      numArray5[4] = (byte) 232;
      numArray5[51] = (byte) 95;
      numArray5[43] = (byte) 23;
      numArray5[2] = (byte) 158;
      numArray5[5] = (byte) 126;
      numArray5[13] = (byte) 238;
      numArray5[29] = (byte) 10;
      numArray5[30] = (byte) 57;
      numArray5[12] = (byte) 32 /*0x20*/;
      numArray5[15] = (byte) 124;
      numArray5[34] = (byte) 127 /*0x7F*/;
      numArray5[8] = (byte) 173;
      numArray5[17] = (byte) 236;
      numArray5[23] = (byte) 69;
      numArray5[18] = (byte) 180;
      numArray5[11] = (byte) 71;
      numArray5[20] = (byte) 161;
      numArray5[21] = (byte) 89;
      numArray5[22] = (byte) 156;
      numArray5[54] = (byte) 45;
      numArray5[10] = (byte) 165;
      numArray5[25] = (byte) 22;
      numArray5[0] = (byte) 57;
      numArray5[27] = (byte) 94;
      numArray5[48 /*0x30*/] = (byte) 201;
      numArray5[9] = (byte) 159;
      numArray5[47] = (byte) 183;
      numArray5[31 /*0x1F*/] = (byte) 14;
      numArray5[32 /*0x20*/] = (byte) 162;
      numArray5[33] = (byte) 24;
      numArray5[53] = (byte) 179;
      numArray5[35] = (byte) 249;
      numArray5[40] = (byte) 70;
      numArray5[37] = (byte) 128 /*0x80*/;
      numArray5[14] = (byte) 35;
      numArray5[39] = (byte) 118;
      numArray5[36] = (byte) 74;
      numArray5[41] = (byte) 19;
      numArray5[42] = (byte) 210;
      numArray5[7] = (byte) 77;
      numArray5[44] = (byte) 71;
      numArray5[45] = (byte) 202;
      numArray5[46] = (byte) 56;
      numArray5[19] = (byte) 198;
      numArray5[38] = (byte) 128 /*0x80*/;
      numArray5[49] = (byte) 71;
      numArray5[50] = (byte) 148;
      numArray5[16 /*0x10*/] = (byte) 208 /*0xD0*/;
      numArray5[52] = (byte) 115;
      numArray5[26] = (byte) 52;
      numArray5[6] = (byte) 233;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 196,
        (byte) 226,
        (byte) 137,
        (byte) 240 /*0xF0*/,
        (byte) 33,
        (byte) 247,
        (byte) 73,
        (byte) 140,
        (byte) 195,
        (byte) 100,
        (byte) 121,
        (byte) 12,
        (byte) 207,
        (byte) 95,
        (byte) 82,
        (byte) 23,
        (byte) 40,
        (byte) 184,
        (byte) 97,
        (byte) 251,
        (byte) 9,
        (byte) 225,
        (byte) 231,
        (byte) 27,
        (byte) 225,
        (byte) 155,
        (byte) 75,
        (byte) 166,
        (byte) 27,
        (byte) 8,
        (byte) 238,
        (byte) 243,
        (byte) 213,
        (byte) 67,
        (byte) 0,
        (byte) 140,
        (byte) 231,
        (byte) 68,
        (byte) 94,
        (byte) 58,
        (byte) 76,
        (byte) 7,
        (byte) 69,
        (byte) 252,
        (byte) 53,
        (byte) 185,
        (byte) 141,
        (byte) 218,
        (byte) 115,
        (byte) 207,
        (byte) 241,
        (byte) 153,
        (byte) 116,
        (byte) 98,
        (byte) 49
      };
      byte[] numArray7 = new byte[55];
      numArray7[52] = (byte) 84;
      numArray7[10] = (byte) 50;
      numArray7[43] = (byte) 135;
      numArray7[3] = (byte) 170;
      numArray7[4] = (byte) 147;
      numArray7[22] = (byte) 188;
      numArray7[21] = (byte) 120;
      numArray7[26] = (byte) 56;
      numArray7[20] = (byte) 227;
      numArray7[9] = (byte) 83;
      numArray7[7] = (byte) 196;
      numArray7[48 /*0x30*/] = (byte) 117;
      numArray7[12] = (byte) 195;
      numArray7[13] = (byte) 3;
      numArray7[14] = (byte) 252;
      numArray7[8] = (byte) 46;
      numArray7[38] = (byte) 250;
      numArray7[23] = (byte) 98;
      numArray7[19] = (byte) 12;
      numArray7[51] = (byte) 99;
      numArray7[44] = (byte) 45;
      numArray7[45] = (byte) 205;
      numArray7[33] = (byte) 45;
      numArray7[0] = (byte) 17;
      numArray7[24] = (byte) 253;
      numArray7[25] = (byte) 142;
      numArray7[1] = (byte) 220;
      numArray7[27] = (byte) 34;
      numArray7[28] = (byte) 173;
      numArray7[16 /*0x10*/] = (byte) 125;
      numArray7[18] = (byte) 15;
      numArray7[31 /*0x1F*/] = (byte) 205;
      numArray7[32 /*0x20*/] = (byte) 239;
      numArray7[2] = (byte) 136;
      numArray7[34] = (byte) 215;
      numArray7[37] = (byte) 244;
      numArray7[36] = (byte) 165;
      numArray7[35] = (byte) 243;
      numArray7[6] = (byte) 111;
      numArray7[39] = (byte) 50;
      numArray7[40] = (byte) 35;
      numArray7[41] = (byte) 211;
      numArray7[42] = (byte) 35;
      numArray7[5] = (byte) 169;
      numArray7[54] = (byte) 145;
      numArray7[17] = (byte) 231;
      numArray7[46] = (byte) 171;
      numArray7[49] = (byte) 87;
      numArray7[47] = (byte) 177;
      numArray7[15] = (byte) 223;
      numArray7[50] = (byte) 13;
      numArray7[29] = (byte) 210;
      numArray7[11] = (byte) 101;
      numArray7[53] = (byte) 55;
      numArray7[30] = (byte) 119;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[26]
      {
        (byte) 135,
        (byte) 230,
        (byte) 119,
        (byte) 58,
        (byte) 110,
        (byte) 37,
        (byte) 163,
        (byte) 33,
        (byte) 6,
        (byte) 195,
        (byte) 122,
        (byte) 64 /*0x40*/,
        (byte) 51,
        (byte) 182,
        (byte) 174,
        (byte) 129,
        (byte) 151,
        (byte) 3,
        (byte) 107,
        (byte) 17,
        (byte) 101,
        (byte) 55,
        (byte) 165,
        (byte) 13,
        (byte) 245,
        (byte) 134
      };
      byte[] numArray9 = new byte[26]
      {
        (byte) 192 /*0xC0*/,
        (byte) 224 /*0xE0*/,
        (byte) 187,
        (byte) 61,
        (byte) 110,
        (byte) 128 /*0x80*/,
        (byte) 70,
        (byte) 76,
        (byte) 53,
        (byte) 131,
        (byte) 150,
        (byte) 140,
        (byte) 75,
        (byte) 44,
        (byte) 208 /*0xD0*/,
        (byte) 87,
        (byte) 17,
        (byte) 148,
        (byte) 76,
        (byte) 28,
        (byte) 168,
        (byte) 116,
        (byte) 180,
        (byte) 178,
        (byte) 37,
        (byte) 118
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[191];
    byte[] numArray11 = new byte[55]
    {
      (byte) 49,
      (byte) 191,
      (byte) 226,
      (byte) 216,
      (byte) 131,
      (byte) 242,
      (byte) 78,
      (byte) 96 /*0x60*/,
      (byte) 84,
      (byte) 59,
      (byte) 112 /*0x70*/,
      (byte) 230,
      (byte) 100,
      (byte) 167,
      (byte) 190,
      (byte) 135,
      (byte) 59,
      (byte) 121,
      (byte) 233,
      (byte) 95,
      (byte) 190,
      (byte) 182,
      (byte) 175,
      (byte) 196,
      (byte) 52,
      (byte) 169,
      (byte) 79,
      (byte) 65,
      (byte) 106,
      (byte) 68,
      (byte) 232,
      (byte) 122,
      (byte) 153,
      (byte) 11,
      (byte) 213,
      (byte) 3,
      (byte) 151,
      (byte) 103,
      (byte) 176 /*0xB0*/,
      (byte) 195,
      (byte) 95,
      (byte) 32 /*0x20*/,
      (byte) 67,
      (byte) 200,
      (byte) 251,
      (byte) 42,
      (byte) 0,
      (byte) 143,
      (byte) 230,
      (byte) 134,
      (byte) 186,
      (byte) 12,
      (byte) 193,
      (byte) 170,
      (byte) 87
    };
    byte[] numArray12 = new byte[55];
    numArray12[43] = (byte) 109;
    numArray12[1] = (byte) 244;
    numArray12[2] = (byte) 85;
    numArray12[36] = (byte) 225;
    numArray12[11] = (byte) 166;
    numArray12[5] = (byte) 86;
    numArray12[10] = (byte) 66;
    numArray12[7] = (byte) 209;
    numArray12[29] = (byte) 98;
    numArray12[9] = (byte) 77;
    numArray12[52] = (byte) 106;
    numArray12[30] = (byte) 10;
    numArray12[12] = (byte) 251;
    numArray12[13] = (byte) 188;
    numArray12[42] = (byte) 151;
    numArray12[44] = (byte) 72;
    numArray12[16 /*0x10*/] = (byte) 46;
    numArray12[25] = (byte) 11;
    numArray12[18] = (byte) 181;
    numArray12[19] = (byte) 143;
    numArray12[0] = (byte) 173;
    numArray12[21] = (byte) 200;
    numArray12[53] = (byte) 174;
    numArray12[46] = (byte) 15;
    numArray12[24] = (byte) 103;
    numArray12[20] = (byte) 216;
    numArray12[26] = (byte) 141;
    numArray12[49] = (byte) 48 /*0x30*/;
    numArray12[14] = (byte) 121;
    numArray12[51] = (byte) 191;
    numArray12[6] = (byte) 91;
    numArray12[31 /*0x1F*/] = (byte) 220;
    numArray12[22] = (byte) 238;
    numArray12[33] = (byte) 171;
    numArray12[27] = (byte) 166;
    numArray12[32 /*0x20*/] = (byte) 169;
    numArray12[3] = (byte) 4;
    numArray12[37] = (byte) 2;
    numArray12[38] = (byte) 120;
    numArray12[39] = (byte) 182;
    numArray12[40] = (byte) 239;
    numArray12[35] = (byte) 16 /*0x10*/;
    numArray12[17] = (byte) 145;
    numArray12[23] = (byte) 87;
    numArray12[8] = (byte) 148;
    numArray12[45] = (byte) 98;
    numArray12[4] = (byte) 42;
    numArray12[50] = (byte) 168;
    numArray12[48 /*0x30*/] = (byte) 63 /*0x3F*/;
    numArray12[54] = (byte) 230;
    numArray12[15] = (byte) 241;
    numArray12[41] = (byte) 183;
    numArray12[28] = (byte) 136;
    numArray12[47] = (byte) 219;
    numArray12[34] = (byte) 116;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[30] = (byte) 67;
    numArray13[1] = (byte) 153;
    numArray13[21] = (byte) 195;
    numArray13[3] = (byte) 226;
    numArray13[44] = (byte) 2;
    numArray13[11] = (byte) 120;
    numArray13[25] = (byte) 46;
    numArray13[7] = (byte) 119;
    numArray13[23] = (byte) 1;
    numArray13[52] = (byte) 68;
    numArray13[42] = (byte) 200;
    numArray13[45] = (byte) 68;
    numArray13[38] = (byte) 158;
    numArray13[9] = (byte) 94;
    numArray13[47] = (byte) 33;
    numArray13[5] = (byte) 17;
    numArray13[15] = (byte) 241;
    numArray13[17] = (byte) 173;
    numArray13[32 /*0x20*/] = (byte) 81;
    numArray13[19] = (byte) 63 /*0x3F*/;
    numArray13[22] = (byte) 44;
    numArray13[14] = (byte) 15;
    numArray13[12] = (byte) 198;
    numArray13[0] = (byte) 84;
    numArray13[4] = (byte) 179;
    numArray13[28] = (byte) 151;
    numArray13[26] = (byte) 77;
    numArray13[27] = (byte) 163;
    numArray13[20] = (byte) 113;
    numArray13[29] = (byte) 29;
    numArray13[2] = (byte) 49;
    numArray13[31 /*0x1F*/] = (byte) 251;
    numArray13[51] = (byte) 253;
    numArray13[33] = (byte) 203;
    numArray13[34] = (byte) 37;
    numArray13[35] = (byte) 42;
    numArray13[36] = (byte) 165;
    numArray13[37] = (byte) 63 /*0x3F*/;
    numArray13[24] = (byte) 3;
    numArray13[39] = (byte) 24;
    numArray13[18] = (byte) 116;
    numArray13[41] = (byte) 254;
    numArray13[6] = (byte) 133;
    numArray13[43] = (byte) 127 /*0x7F*/;
    numArray13[46] = (byte) 101;
    numArray13[16 /*0x10*/] = (byte) 4;
    numArray13[40] = (byte) 173;
    numArray13[54] = (byte) 182;
    numArray13[48 /*0x30*/] = (byte) 215;
    numArray13[49] = (byte) 82;
    numArray13[50] = (byte) 151;
    numArray13[8] = (byte) 60;
    numArray13[10] = (byte) 165;
    numArray13[53] = (byte) 200;
    numArray13[13] = (byte) 117;
    byte[] numArray14 = new byte[55];
    numArray14[43] = (byte) 250;
    numArray14[22] = (byte) 191;
    numArray14[30] = (byte) 84;
    numArray14[36] = (byte) 17;
    numArray14[3] = (byte) 88;
    numArray14[39] = (byte) 133;
    numArray14[54] = (byte) 63 /*0x3F*/;
    numArray14[7] = (byte) 185;
    numArray14[8] = (byte) 208 /*0xD0*/;
    numArray14[32 /*0x20*/] = (byte) 19;
    numArray14[50] = (byte) 204;
    numArray14[24] = (byte) 194;
    numArray14[12] = (byte) 81;
    numArray14[13] = (byte) 178;
    numArray14[28] = (byte) 35;
    numArray14[15] = (byte) 55;
    numArray14[6] = (byte) 106;
    numArray14[40] = (byte) 111;
    numArray14[18] = (byte) 250;
    numArray14[19] = (byte) 11;
    numArray14[9] = (byte) 45;
    numArray14[4] = (byte) 99;
    numArray14[33] = (byte) 148;
    numArray14[14] = (byte) 201;
    numArray14[21] = (byte) 232;
    numArray14[31 /*0x1F*/] = (byte) 181;
    numArray14[0] = (byte) 103;
    numArray14[27] = (byte) 203;
    numArray14[29] = (byte) 86;
    numArray14[2] = (byte) 125;
    numArray14[25] = (byte) 118;
    numArray14[5] = (byte) 152;
    numArray14[23] = (byte) 191;
    numArray14[10] = (byte) 215;
    numArray14[37] = (byte) 233;
    numArray14[35] = (byte) 16 /*0x10*/;
    numArray14[47] = (byte) 96 /*0x60*/;
    numArray14[20] = (byte) 86;
    numArray14[38] = (byte) 100;
    numArray14[26] = (byte) 62;
    numArray14[17] = (byte) 19;
    numArray14[41] = (byte) 32 /*0x20*/;
    numArray14[42] = (byte) 249;
    numArray14[34] = (byte) 226;
    numArray14[44] = (byte) 218;
    numArray14[45] = (byte) 148;
    numArray14[46] = (byte) 101;
    numArray14[11] = (byte) 10;
    numArray14[48 /*0x30*/] = (byte) 178;
    numArray14[49] = (byte) 92;
    numArray14[16 /*0x10*/] = (byte) 149;
    numArray14[51] = (byte) 36;
    numArray14[52] = (byte) 1;
    numArray14[53] = (byte) 225;
    numArray14[1] = (byte) 43;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 137,
      (byte) 126,
      (byte) 92,
      (byte) 62,
      (byte) 60,
      (byte) 90,
      (byte) 228,
      (byte) 126,
      (byte) 37,
      (byte) 75,
      (byte) 20,
      (byte) 188,
      (byte) 42,
      (byte) 147,
      (byte) 36,
      (byte) 152,
      (byte) 77,
      (byte) 15,
      (byte) 208 /*0xD0*/,
      (byte) 66,
      (byte) 184,
      (byte) 66,
      (byte) 34,
      (byte) 219,
      (byte) 69,
      (byte) 13,
      (byte) 17,
      (byte) 70,
      (byte) 93,
      (byte) 237,
      (byte) 225,
      (byte) 227,
      (byte) 188,
      (byte) 131,
      (byte) 186,
      (byte) 194,
      (byte) 1,
      (byte) 124,
      (byte) 122,
      (byte) 41,
      (byte) 43,
      (byte) 16 /*0x10*/,
      (byte) 62,
      (byte) 51,
      (byte) 110,
      (byte) 88,
      (byte) 161,
      (byte) 155,
      (byte) 73,
      (byte) 97,
      (byte) 147,
      (byte) 97,
      (byte) 29,
      (byte) 0,
      (byte) 192 /*0xC0*/
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 23,
      (byte) 74,
      (byte) 172,
      (byte) 51,
      (byte) 189,
      (byte) 207,
      (byte) 83,
      (byte) 240 /*0xF0*/,
      (byte) 193,
      (byte) 58,
      (byte) 36,
      (byte) 191,
      (byte) 69,
      (byte) 207,
      (byte) 253,
      (byte) 174,
      (byte) 79,
      (byte) 80 /*0x50*/,
      (byte) 27,
      (byte) 73,
      (byte) 121,
      (byte) 226,
      (byte) 30,
      (byte) 127 /*0x7F*/,
      (byte) 194,
      (byte) 126,
      (byte) 215,
      (byte) 236,
      (byte) 112 /*0x70*/,
      (byte) 120,
      (byte) 123,
      (byte) 233,
      (byte) 39,
      (byte) 2,
      (byte) 93,
      (byte) 232,
      (byte) 27,
      (byte) 67,
      (byte) 175,
      (byte) 102,
      (byte) 55,
      (byte) 99,
      (byte) 227,
      (byte) 175,
      (byte) 81,
      (byte) 171,
      (byte) 225,
      (byte) 176 /*0xB0*/,
      (byte) 173,
      (byte) 111,
      (byte) 232,
      (byte) 232,
      (byte) 78,
      (byte) 106,
      (byte) 181
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[26]
    {
      (byte) 128 /*0x80*/,
      (byte) 249,
      (byte) 47,
      (byte) 72,
      (byte) 157,
      (byte) 32 /*0x20*/,
      (byte) 56,
      (byte) 106,
      (byte) 49,
      (byte) 0,
      (byte) 167,
      (byte) 216,
      (byte) 98,
      (byte) 150,
      (byte) 208 /*0xD0*/,
      (byte) 149,
      (byte) 166,
      (byte) 22,
      (byte) 167,
      (byte) 3,
      (byte) 239,
      (byte) 109,
      (byte) 236,
      (byte) 107,
      (byte) 118,
      (byte) 253
    };
    byte[] numArray18 = new byte[26];
    numArray18[16 /*0x10*/] = (byte) 77;
    numArray18[7] = (byte) 66;
    numArray18[22] = (byte) 151;
    numArray18[8] = (byte) 49;
    numArray18[19] = (byte) 254;
    numArray18[0] = (byte) 31 /*0x1F*/;
    numArray18[6] = (byte) 97;
    numArray18[17] = (byte) 53;
    numArray18[14] = (byte) 180;
    numArray18[9] = (byte) 12;
    numArray18[5] = (byte) 71;
    numArray18[11] = (byte) 193;
    numArray18[12] = (byte) 118;
    numArray18[13] = (byte) 197;
    numArray18[18] = (byte) 251;
    numArray18[15] = (byte) 98;
    numArray18[20] = (byte) 110;
    numArray18[2] = (byte) 200;
    numArray18[10] = (byte) 231;
    numArray18[23] = (byte) 200;
    numArray18[1] = (byte) 135;
    numArray18[4] = (byte) 235;
    numArray18[21] = (byte) 42;
    numArray18[3] = (byte) 58;
    numArray18[24] = (byte) 228;
    numArray18[25] = (byte) 28;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 26);
    for (int index = 0; index < 26; ++index)
      numArray10[index + 165] ^= numArray18[index];
    byte[] numArray19 = new byte[15];
    byte[] response = new byte[15];
    Array.Copy((Array) sc_12431.sspq, 249, (Array) numArray19, 0, 15);
    key.Query(true, 335, numArray19, response);
    Array.Copy((Array) sc_12431.sspr, 249, (Array) numArray19, 0, 15);
    for (int index = 0; index < numArray19.Length; ++index)
    {
      if ((int) numArray19[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_12462()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[211];
      byte[] numArray2 = new byte[55];
      numArray2[31 /*0x1F*/] = (byte) 118;
      numArray2[28] = (byte) 92;
      numArray2[2] = (byte) 254;
      numArray2[37] = (byte) 68;
      numArray2[1] = (byte) 183;
      numArray2[53] = (byte) 194;
      numArray2[35] = (byte) 98;
      numArray2[24] = (byte) 220;
      numArray2[52] = (byte) 213;
      numArray2[14] = (byte) 169;
      numArray2[47] = (byte) 114;
      numArray2[11] = (byte) 178;
      numArray2[20] = (byte) 14;
      numArray2[4] = (byte) 48 /*0x30*/;
      numArray2[51] = (byte) 168;
      numArray2[15] = (byte) 47;
      numArray2[38] = (byte) 226;
      numArray2[33] = (byte) 177;
      numArray2[18] = (byte) 119;
      numArray2[13] = (byte) 210;
      numArray2[34] = (byte) 96 /*0x60*/;
      numArray2[39] = (byte) 119;
      numArray2[22] = (byte) 203;
      numArray2[17] = (byte) 109;
      numArray2[6] = (byte) 113;
      numArray2[25] = (byte) 208 /*0xD0*/;
      numArray2[0] = (byte) 136;
      numArray2[45] = (byte) 57;
      numArray2[42] = (byte) 137;
      numArray2[9] = (byte) 50;
      numArray2[30] = (byte) 179;
      numArray2[41] = (byte) 29;
      numArray2[32 /*0x20*/] = (byte) 130;
      numArray2[8] = (byte) 22;
      numArray2[43] = (byte) 241;
      numArray2[7] = (byte) 180;
      numArray2[36] = (byte) 69;
      numArray2[49] = (byte) 54;
      numArray2[19] = (byte) 135;
      numArray2[3] = (byte) 39;
      numArray2[40] = (byte) 82;
      numArray2[29] = (byte) 235;
      numArray2[27] = (byte) 22;
      numArray2[26] = (byte) 19;
      numArray2[44] = (byte) 72;
      numArray2[10] = (byte) 150;
      numArray2[46] = (byte) 29;
      numArray2[16 /*0x10*/] = (byte) 250;
      numArray2[48 /*0x30*/] = (byte) 59;
      numArray2[23] = (byte) 38;
      numArray2[50] = (byte) 0;
      numArray2[21] = (byte) 238;
      numArray2[12] = (byte) 78;
      numArray2[5] = (byte) 149;
      numArray2[54] = (byte) 182;
      byte[] numArray3 = new byte[55]
      {
        (byte) 58,
        (byte) 59,
        (byte) 199,
        (byte) 87,
        (byte) 76,
        (byte) 99,
        (byte) 214,
        (byte) 79,
        (byte) 195,
        (byte) 44,
        (byte) 33,
        (byte) 58,
        (byte) 204,
        (byte) 155,
        (byte) 16 /*0x10*/,
        (byte) 68,
        (byte) 59,
        (byte) 114,
        (byte) 124,
        (byte) 84,
        (byte) 223,
        (byte) 167,
        (byte) 241,
        (byte) 141,
        (byte) 68,
        (byte) 228,
        (byte) 73,
        (byte) 38,
        (byte) 45,
        (byte) 103,
        (byte) 192 /*0xC0*/,
        (byte) 172,
        (byte) 55,
        (byte) 200,
        (byte) 81,
        (byte) 191,
        (byte) 80 /*0x50*/,
        (byte) 112 /*0x70*/,
        (byte) 126,
        (byte) 143,
        (byte) 131,
        (byte) 86,
        (byte) 88,
        (byte) 179,
        (byte) 154,
        (byte) 1,
        (byte) 28,
        (byte) 235,
        (byte) 34,
        (byte) 31 /*0x1F*/,
        (byte) 34,
        (byte) 187,
        (byte) 247,
        (byte) 215,
        (byte) 81
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 249,
        (byte) 167,
        (byte) 172,
        (byte) 225,
        (byte) 127 /*0x7F*/,
        (byte) 49,
        (byte) 63 /*0x3F*/,
        (byte) 115,
        (byte) 127 /*0x7F*/,
        (byte) 111,
        (byte) 161,
        (byte) 171,
        (byte) 208 /*0xD0*/,
        (byte) 125,
        (byte) 188,
        (byte) 7,
        (byte) 11,
        (byte) 61,
        (byte) 43,
        (byte) 6,
        (byte) 102,
        (byte) 182,
        (byte) 153,
        (byte) 50,
        (byte) 159,
        (byte) 231,
        (byte) 219,
        (byte) 161,
        (byte) 236,
        (byte) 204,
        (byte) 50,
        (byte) 53,
        (byte) 144 /*0x90*/,
        (byte) 101,
        (byte) 115,
        (byte) 82,
        (byte) 215,
        (byte) 19,
        (byte) 119,
        (byte) 149,
        (byte) 31 /*0x1F*/,
        byte.MaxValue,
        (byte) 167,
        (byte) 154,
        (byte) 223,
        (byte) 86,
        (byte) 35,
        (byte) 206,
        (byte) 141,
        (byte) 22,
        (byte) 201,
        (byte) 80 /*0x50*/,
        (byte) 154,
        (byte) 47,
        (byte) 170
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 173,
        (byte) 203,
        (byte) 175,
        (byte) 196,
        (byte) 145,
        (byte) 136,
        (byte) 106,
        (byte) 162,
        (byte) 208 /*0xD0*/,
        (byte) 86,
        (byte) 88,
        (byte) 209,
        (byte) 47,
        (byte) 49,
        (byte) 130,
        (byte) 79,
        (byte) 25,
        (byte) 118,
        (byte) 180,
        (byte) 119,
        (byte) 220,
        (byte) 16 /*0x10*/,
        (byte) 245,
        (byte) 144 /*0x90*/,
        (byte) 75,
        (byte) 104,
        (byte) 218,
        (byte) 197,
        (byte) 97,
        (byte) 98,
        (byte) 198,
        (byte) 173,
        (byte) 216,
        (byte) 169,
        (byte) 166,
        (byte) 16 /*0x10*/,
        (byte) 84,
        (byte) 210,
        (byte) 217,
        (byte) 34,
        (byte) 238,
        (byte) 232,
        (byte) 23,
        (byte) 6,
        (byte) 200,
        (byte) 186,
        (byte) 128 /*0x80*/,
        (byte) 112 /*0x70*/,
        (byte) 114,
        (byte) 75,
        (byte) 219,
        (byte) 93,
        (byte) 204,
        (byte) 217,
        (byte) 192 /*0xC0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 122,
        (byte) 66,
        (byte) 152,
        (byte) 86,
        (byte) 157,
        (byte) 134,
        (byte) 179,
        (byte) 60,
        (byte) 108,
        (byte) 26,
        (byte) 107,
        (byte) 9,
        (byte) 105,
        (byte) 48 /*0x30*/,
        (byte) 247,
        (byte) 197,
        (byte) 150,
        (byte) 231,
        (byte) 102,
        (byte) 15,
        byte.MaxValue,
        (byte) 82,
        (byte) 73,
        (byte) 195,
        (byte) 12,
        (byte) 20,
        (byte) 214,
        (byte) 2,
        (byte) 226,
        (byte) 155,
        (byte) 96 /*0x60*/,
        (byte) 186,
        (byte) 49,
        (byte) 69,
        (byte) 92,
        (byte) 209,
        (byte) 223,
        (byte) 62,
        (byte) 129,
        (byte) 121,
        (byte) 243,
        (byte) 93,
        (byte) 114,
        (byte) 59,
        (byte) 183,
        (byte) 196,
        (byte) 35,
        (byte) 230,
        (byte) 210,
        (byte) 139,
        (byte) 4,
        (byte) 72,
        (byte) 67,
        (byte) 50,
        (byte) 14
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 253,
        (byte) 71,
        (byte) 193,
        (byte) 123,
        (byte) 219,
        (byte) 132,
        (byte) 47,
        (byte) 173,
        (byte) 177,
        (byte) 186,
        (byte) 66,
        (byte) 77,
        (byte) 42,
        (byte) 117,
        (byte) 111,
        (byte) 112 /*0x70*/,
        (byte) 56,
        (byte) 140,
        (byte) 63 /*0x3F*/,
        (byte) 110,
        (byte) 79,
        (byte) 93,
        (byte) 173,
        (byte) 173,
        (byte) 179,
        (byte) 83,
        (byte) 38,
        (byte) 133,
        (byte) 128 /*0x80*/,
        (byte) 55,
        (byte) 142,
        (byte) 159,
        (byte) 26,
        (byte) 147,
        (byte) 247,
        (byte) 223,
        (byte) 42,
        (byte) 86,
        (byte) 7,
        (byte) 88,
        (byte) 14,
        (byte) 229,
        (byte) 161,
        (byte) 43,
        (byte) 51,
        (byte) 11,
        (byte) 236,
        (byte) 222,
        (byte) 159,
        (byte) 53,
        (byte) 242,
        (byte) 82,
        (byte) 173,
        (byte) 139,
        (byte) 195
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[46];
      numArray8[44] = byte.MaxValue;
      numArray8[1] = (byte) 73;
      numArray8[2] = (byte) 137;
      numArray8[3] = (byte) 176 /*0xB0*/;
      numArray8[31 /*0x1F*/] = (byte) 54;
      numArray8[35] = (byte) 245;
      numArray8[18] = (byte) 161;
      numArray8[7] = (byte) 186;
      numArray8[40] = (byte) 161;
      numArray8[12] = (byte) 38;
      numArray8[10] = (byte) 36;
      numArray8[11] = (byte) 211;
      numArray8[43] = (byte) 236;
      numArray8[45] = (byte) 19;
      numArray8[4] = (byte) 177;
      numArray8[15] = (byte) 101;
      numArray8[16 /*0x10*/] = (byte) 237;
      numArray8[0] = (byte) 213;
      numArray8[22] = (byte) 71;
      numArray8[19] = (byte) 223;
      numArray8[20] = (byte) 13;
      numArray8[21] = (byte) 11;
      numArray8[8] = (byte) 176 /*0xB0*/;
      numArray8[24] = (byte) 239;
      numArray8[41] = (byte) 129;
      numArray8[25] = (byte) 48 /*0x30*/;
      numArray8[6] = (byte) 209;
      numArray8[13] = (byte) 230;
      numArray8[26] = (byte) 86;
      numArray8[29] = (byte) 118;
      numArray8[9] = (byte) 215;
      numArray8[42] = (byte) 7;
      numArray8[27] = (byte) 150;
      numArray8[33] = (byte) 216;
      numArray8[30] = (byte) 254;
      numArray8[32 /*0x20*/] = (byte) 53;
      numArray8[14] = (byte) 87;
      numArray8[37] = (byte) 200;
      numArray8[17] = (byte) 27;
      numArray8[39] = (byte) 3;
      numArray8[23] = (byte) 240 /*0xF0*/;
      numArray8[28] = (byte) 24;
      numArray8[38] = (byte) 65;
      numArray8[5] = (byte) 90;
      numArray8[34] = (byte) 9;
      numArray8[36] = (byte) 248;
      byte[] numArray9 = new byte[46]
      {
        (byte) 80 /*0x50*/,
        (byte) 24,
        (byte) 42,
        (byte) 15,
        (byte) 77,
        (byte) 163,
        (byte) 12,
        (byte) 115,
        (byte) 0,
        (byte) 145,
        (byte) 86,
        (byte) 149,
        (byte) 105,
        (byte) 145,
        (byte) 151,
        (byte) 210,
        (byte) 209,
        (byte) 110,
        (byte) 10,
        (byte) 122,
        (byte) 113,
        (byte) 36,
        (byte) 109,
        (byte) 37,
        (byte) 191,
        (byte) 211,
        (byte) 11,
        (byte) 236,
        (byte) 89,
        (byte) 104,
        (byte) 20,
        (byte) 11,
        (byte) 229,
        (byte) 8,
        (byte) 245,
        (byte) 192 /*0xC0*/,
        (byte) 192 /*0xC0*/,
        (byte) 211,
        (byte) 252,
        (byte) 141,
        (byte) 151,
        (byte) 213,
        (byte) 249,
        (byte) 24,
        (byte) 149,
        (byte) 0
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[211];
    byte[] numArray11 = new byte[55]
    {
      (byte) 103,
      (byte) 223,
      (byte) 204,
      (byte) 19,
      (byte) 17,
      (byte) 118,
      (byte) 94,
      (byte) 200,
      (byte) 75,
      (byte) 117,
      (byte) 49,
      (byte) 61,
      (byte) 206,
      (byte) 9,
      (byte) 94,
      (byte) 196,
      (byte) 166,
      (byte) 168,
      (byte) 229,
      (byte) 253,
      (byte) 81,
      (byte) 80 /*0x50*/,
      (byte) 108,
      (byte) 25,
      (byte) 242,
      (byte) 35,
      (byte) 175,
      (byte) 96 /*0x60*/,
      (byte) 156,
      (byte) 109,
      (byte) 14,
      (byte) 28,
      (byte) 25,
      (byte) 158,
      (byte) 214,
      (byte) 173,
      (byte) 55,
      (byte) 71,
      (byte) 222,
      (byte) 101,
      (byte) 38,
      (byte) 234,
      (byte) 158,
      (byte) 191,
      (byte) 239,
      (byte) 170,
      (byte) 250,
      (byte) 228,
      (byte) 142,
      (byte) 123,
      (byte) 77,
      (byte) 182,
      (byte) 34,
      (byte) 195,
      (byte) 174
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 106,
      (byte) 60,
      (byte) 71,
      (byte) 161,
      (byte) 245,
      (byte) 192 /*0xC0*/,
      (byte) 65,
      (byte) 36,
      (byte) 214,
      (byte) 125,
      (byte) 178,
      (byte) 4,
      (byte) 57,
      (byte) 144 /*0x90*/,
      (byte) 109,
      (byte) 249,
      (byte) 8,
      (byte) 251,
      (byte) 219,
      (byte) 28,
      (byte) 128 /*0x80*/,
      (byte) 99,
      (byte) 33,
      (byte) 212,
      (byte) 213,
      (byte) 48 /*0x30*/,
      (byte) 170,
      (byte) 57,
      (byte) 174,
      (byte) 180,
      (byte) 43,
      (byte) 219,
      (byte) 203,
      (byte) 229,
      (byte) 9,
      (byte) 244,
      (byte) 15,
      (byte) 6,
      (byte) 7,
      (byte) 174,
      (byte) 119,
      (byte) 157,
      (byte) 153,
      (byte) 119,
      (byte) 157,
      (byte) 123,
      (byte) 104,
      (byte) 99,
      (byte) 30,
      (byte) 37,
      (byte) 198,
      (byte) 124,
      (byte) 144 /*0x90*/,
      (byte) 48 /*0x30*/,
      (byte) 223
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[2] = (byte) 238;
    numArray13[30] = (byte) 86;
    numArray13[38] = (byte) 40;
    numArray13[11] = (byte) 51;
    numArray13[4] = (byte) 253;
    numArray13[22] = (byte) 71;
    numArray13[23] = (byte) 40;
    numArray13[43] = (byte) 134;
    numArray13[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray13[3] = (byte) 198;
    numArray13[10] = (byte) 48 /*0x30*/;
    numArray13[45] = (byte) 176 /*0xB0*/;
    numArray13[33] = (byte) 59;
    numArray13[46] = (byte) 222;
    numArray13[14] = (byte) 95;
    numArray13[15] = (byte) 201;
    numArray13[13] = (byte) 140;
    numArray13[17] = (byte) 215;
    numArray13[18] = (byte) 103;
    numArray13[12] = (byte) 176 /*0xB0*/;
    numArray13[20] = (byte) 173;
    numArray13[0] = (byte) 179;
    numArray13[42] = (byte) 218;
    numArray13[7] = (byte) 103;
    numArray13[21] = (byte) 42;
    numArray13[44] = (byte) 202;
    numArray13[26] = (byte) 32 /*0x20*/;
    numArray13[27] = (byte) 31 /*0x1F*/;
    numArray13[28] = (byte) 88;
    numArray13[29] = (byte) 250;
    numArray13[39] = (byte) 190;
    numArray13[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray13[8] = (byte) 20;
    numArray13[52] = (byte) 70;
    numArray13[24] = (byte) 203;
    numArray13[35] = (byte) 38;
    numArray13[6] = (byte) 222;
    numArray13[37] = (byte) 159;
    numArray13[1] = (byte) 33;
    numArray13[34] = (byte) 132;
    numArray13[40] = (byte) 191;
    numArray13[41] = (byte) 88;
    numArray13[32 /*0x20*/] = (byte) 190;
    numArray13[19] = (byte) 178;
    numArray13[36] = (byte) 240 /*0xF0*/;
    numArray13[54] = (byte) 196;
    numArray13[25] = (byte) 209;
    numArray13[47] = (byte) 239;
    numArray13[48 /*0x30*/] = (byte) 238;
    numArray13[49] = (byte) 236;
    numArray13[50] = (byte) 163;
    numArray13[51] = (byte) 235;
    numArray13[5] = (byte) 44;
    numArray13[53] = (byte) 101;
    numArray13[9] = (byte) 35;
    byte[] numArray14 = new byte[55]
    {
      (byte) 180,
      (byte) 247,
      (byte) 93,
      (byte) 103,
      (byte) 79,
      (byte) 153,
      (byte) 216,
      (byte) 109,
      (byte) 77,
      (byte) 78,
      (byte) 145,
      (byte) 217,
      (byte) 137,
      (byte) 210,
      (byte) 53,
      (byte) 198,
      (byte) 156,
      (byte) 165,
      (byte) 6,
      (byte) 93,
      (byte) 126,
      (byte) 165,
      (byte) 180,
      (byte) 51,
      (byte) 188,
      (byte) 246,
      (byte) 142,
      (byte) 66,
      (byte) 55,
      (byte) 93,
      (byte) 79,
      (byte) 33,
      (byte) 184,
      (byte) 244,
      (byte) 80 /*0x50*/,
      (byte) 11,
      (byte) 90,
      (byte) 187,
      (byte) 2,
      (byte) 83,
      (byte) 225,
      (byte) 2,
      (byte) 236,
      (byte) 12,
      (byte) 115,
      (byte) 133,
      (byte) 185,
      (byte) 122,
      (byte) 49,
      (byte) 229,
      (byte) 208 /*0xD0*/,
      (byte) 107,
      (byte) 31 /*0x1F*/,
      (byte) 86,
      (byte) 114
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[38] = (byte) 116;
    numArray15[51] = (byte) 130;
    numArray15[2] = (byte) 56;
    numArray15[26] = (byte) 89;
    numArray15[30] = (byte) 229;
    numArray15[5] = (byte) 162;
    numArray15[1] = (byte) 223;
    numArray15[52] = (byte) 100;
    numArray15[8] = (byte) 150;
    numArray15[9] = (byte) 28;
    numArray15[33] = (byte) 52;
    numArray15[11] = (byte) 11;
    numArray15[24] = (byte) 73;
    numArray15[13] = (byte) 173;
    numArray15[14] = (byte) 59;
    numArray15[16 /*0x10*/] = (byte) 246;
    numArray15[32 /*0x20*/] = (byte) 194;
    numArray15[17] = (byte) 240 /*0xF0*/;
    numArray15[37] = (byte) 215;
    numArray15[28] = (byte) 45;
    numArray15[20] = (byte) 7;
    numArray15[6] = (byte) 195;
    numArray15[22] = (byte) 223;
    numArray15[4] = (byte) 141;
    numArray15[0] = (byte) 103;
    numArray15[25] = (byte) 123;
    numArray15[41] = (byte) 87;
    numArray15[27] = (byte) 147;
    numArray15[40] = (byte) 252;
    numArray15[54] = (byte) 16 /*0x10*/;
    numArray15[10] = (byte) 253;
    numArray15[23] = (byte) 137;
    numArray15[7] = (byte) 98;
    numArray15[31 /*0x1F*/] = (byte) 6;
    numArray15[34] = (byte) 196;
    numArray15[3] = (byte) 218;
    numArray15[47] = (byte) 101;
    numArray15[15] = (byte) 177;
    numArray15[12] = (byte) 235;
    numArray15[29] = (byte) 11;
    numArray15[39] = (byte) 166;
    numArray15[18] = (byte) 11;
    numArray15[42] = (byte) 244;
    numArray15[43] = (byte) 31 /*0x1F*/;
    numArray15[44] = (byte) 108;
    numArray15[45] = (byte) 219;
    numArray15[46] = (byte) 202;
    numArray15[35] = (byte) 196;
    numArray15[48 /*0x30*/] = (byte) 136;
    numArray15[49] = (byte) 139;
    numArray15[50] = (byte) 166;
    numArray15[36] = (byte) 196;
    numArray15[21] = (byte) 211;
    numArray15[53] = (byte) 171;
    numArray15[19] = (byte) 142;
    byte[] numArray16 = new byte[55]
    {
      (byte) 167,
      (byte) 73,
      (byte) 78,
      (byte) 96 /*0x60*/,
      (byte) 104,
      (byte) 59,
      (byte) 199,
      (byte) 151,
      (byte) 167,
      (byte) 106,
      (byte) 29,
      (byte) 225,
      (byte) 0,
      (byte) 98,
      (byte) 76,
      (byte) 235,
      (byte) 9,
      (byte) 239,
      (byte) 54,
      (byte) 7,
      (byte) 31 /*0x1F*/,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 168,
      (byte) 113,
      (byte) 31 /*0x1F*/,
      (byte) 2,
      (byte) 254,
      (byte) 217,
      (byte) 72,
      (byte) 248,
      (byte) 66,
      (byte) 199,
      (byte) 186,
      (byte) 36,
      (byte) 157,
      (byte) 133,
      (byte) 10,
      (byte) 157,
      (byte) 238,
      (byte) 61,
      (byte) 68,
      (byte) 79,
      (byte) 42,
      (byte) 112 /*0x70*/,
      (byte) 208 /*0xD0*/,
      (byte) 38,
      (byte) 61,
      (byte) 251,
      (byte) 63 /*0x3F*/,
      (byte) 102,
      (byte) 214,
      (byte) 108,
      (byte) 52,
      (byte) 228
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[46];
    numArray17[12] = (byte) 177;
    numArray17[7] = (byte) 210;
    numArray17[25] = (byte) 249;
    numArray17[4] = (byte) 37;
    numArray17[1] = (byte) 34;
    numArray17[5] = (byte) 146;
    numArray17[43] = (byte) 96 /*0x60*/;
    numArray17[2] = (byte) 96 /*0x60*/;
    numArray17[41] = (byte) 21;
    numArray17[21] = (byte) 168;
    numArray17[14] = (byte) 216;
    numArray17[11] = (byte) 184;
    numArray17[35] = (byte) 37;
    numArray17[13] = (byte) 215;
    numArray17[44] = (byte) 129;
    numArray17[15] = (byte) 25;
    numArray17[36] = (byte) 2;
    numArray17[17] = (byte) 197;
    numArray17[33] = (byte) 133;
    numArray17[40] = (byte) 68;
    numArray17[20] = (byte) 216;
    numArray17[3] = (byte) 85;
    numArray17[45] = (byte) 195;
    numArray17[23] = (byte) 157;
    numArray17[24] = (byte) 119;
    numArray17[6] = (byte) 37;
    numArray17[22] = (byte) 220;
    numArray17[16 /*0x10*/] = (byte) 7;
    numArray17[38] = (byte) 166;
    numArray17[8] = (byte) 84;
    numArray17[30] = (byte) 254;
    numArray17[31 /*0x1F*/] = (byte) 194;
    numArray17[0] = (byte) 146;
    numArray17[26] = (byte) 3;
    numArray17[34] = (byte) 130;
    numArray17[9] = (byte) 50;
    numArray17[27] = (byte) 220;
    numArray17[37] = (byte) 7;
    numArray17[29] = (byte) 163;
    numArray17[39] = (byte) 143;
    numArray17[18] = (byte) 141;
    numArray17[19] = (byte) 116;
    numArray17[42] = (byte) 32 /*0x20*/;
    numArray17[32 /*0x20*/] = (byte) 123;
    numArray17[10] = (byte) 146;
    numArray17[28] = (byte) 93;
    byte[] numArray18 = new byte[46];
    numArray18[22] = (byte) 164;
    numArray18[24] = (byte) 60;
    numArray18[18] = (byte) 65;
    numArray18[14] = (byte) 39;
    numArray18[42] = (byte) 152;
    numArray18[32 /*0x20*/] = (byte) 98;
    numArray18[6] = (byte) 252;
    numArray18[16 /*0x10*/] = (byte) 194;
    numArray18[8] = (byte) 138;
    numArray18[9] = (byte) 123;
    numArray18[10] = (byte) 178;
    numArray18[43] = (byte) 157;
    numArray18[12] = (byte) 184;
    numArray18[13] = (byte) 108;
    numArray18[5] = (byte) 224 /*0xE0*/;
    numArray18[40] = (byte) 168;
    numArray18[28] = (byte) 161;
    numArray18[21] = (byte) 89;
    numArray18[38] = (byte) 109;
    numArray18[23] = (byte) 225;
    numArray18[20] = (byte) 87;
    numArray18[2] = (byte) 209;
    numArray18[0] = (byte) 7;
    numArray18[4] = (byte) 229;
    numArray18[19] = (byte) 223;
    numArray18[25] = (byte) 151;
    numArray18[35] = (byte) 220;
    numArray18[27] = (byte) 49;
    numArray18[33] = (byte) 11;
    numArray18[11] = (byte) 195;
    numArray18[30] = (byte) 71;
    numArray18[31 /*0x1F*/] = (byte) 158;
    numArray18[39] = (byte) 11;
    numArray18[17] = (byte) 229;
    numArray18[34] = (byte) 216;
    numArray18[3] = (byte) 55;
    numArray18[1] = (byte) 197;
    numArray18[37] = (byte) 40;
    numArray18[26] = (byte) 60;
    numArray18[36] = (byte) 135;
    numArray18[15] = (byte) 45;
    numArray18[41] = (byte) 245;
    numArray18[7] = (byte) 119;
    numArray18[29] = (byte) 57;
    numArray18[44] = (byte) 164;
    numArray18[45] = (byte) 2;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 46);
    for (int index = 0; index < 46; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_12463(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 53,
      (byte) 22,
      (byte) 153,
      (byte) 254,
      (byte) 232,
      (byte) 173,
      (byte) 223,
      (byte) 158,
      (byte) 105,
      (byte) 162,
      (byte) 209,
      (byte) 132,
      (byte) 36,
      (byte) 179,
      (byte) 41,
      (byte) 252,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 218,
      (byte) 219,
      (byte) 132,
      (byte) 74,
      (byte) 120,
      (byte) 60,
      (byte) 252,
      (byte) 43,
      (byte) 135,
      (byte) 235,
      (byte) 101,
      (byte) 83,
      (byte) 19,
      (byte) 200,
      (byte) 214,
      (byte) 149,
      (byte) 136,
      (byte) 189,
      (byte) 62,
      (byte) 215,
      (byte) 187,
      (byte) 78,
      (byte) 85,
      (byte) 130,
      (byte) 175,
      (byte) 173,
      (byte) 152,
      (byte) 169,
      (byte) 65
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 73;
    sourceArray2[1] = (byte) 91;
    sourceArray2[2] = (byte) 129;
    sourceArray2[7] = (byte) 226;
    sourceArray2[4] = (byte) 48 /*0x30*/;
    sourceArray2[8] = (byte) 244;
    sourceArray2[32 /*0x20*/] = (byte) 190;
    sourceArray2[17] = (byte) 56;
    sourceArray2[13] = (byte) 64 /*0x40*/;
    sourceArray2[9] = (byte) 251;
    sourceArray2[10] = (byte) 32 /*0x20*/;
    sourceArray2[45] = (byte) 183;
    sourceArray2[28] = (byte) 64 /*0x40*/;
    sourceArray2[41] = (byte) 150;
    sourceArray2[24] = (byte) 20;
    sourceArray2[5] = (byte) 245;
    sourceArray2[33] = (byte) 120;
    sourceArray2[26] = (byte) 109;
    sourceArray2[43] = (byte) 178;
    sourceArray2[19] = (byte) 155;
    sourceArray2[20] = (byte) 96 /*0x60*/;
    sourceArray2[12] = (byte) 152;
    sourceArray2[22] = (byte) 75;
    sourceArray2[23] = (byte) 8;
    sourceArray2[3] = (byte) 234;
    sourceArray2[25] = (byte) 166;
    sourceArray2[21] = (byte) 159;
    sourceArray2[46] = (byte) 68;
    sourceArray2[6] = (byte) 127 /*0x7F*/;
    sourceArray2[29] = (byte) 82;
    sourceArray2[30] = (byte) 199;
    sourceArray2[27] = (byte) 187;
    sourceArray2[18] = (byte) 61;
    sourceArray2[11] = (byte) 151;
    sourceArray2[16 /*0x10*/] = (byte) 246;
    sourceArray2[35] = (byte) 130;
    sourceArray2[34] = (byte) 201;
    sourceArray2[0] = (byte) 244;
    sourceArray2[38] = (byte) 103;
    sourceArray2[40] = (byte) 102;
    sourceArray2[14] = (byte) 139;
    sourceArray2[15] = (byte) 137;
    sourceArray2[42] = (byte) 113;
    sourceArray2[39] = (byte) 149;
    sourceArray2[31 /*0x1F*/] = (byte) 164;
    sourceArray2[36] = (byte) 15;
    sourceArray2[44] = (byte) 224 /*0xE0*/;
    sourceArray2[47] = (byte) 50;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12464()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 143,
        (byte) 252,
        (byte) 178,
        (byte) 236,
        (byte) 154,
        (byte) 225,
        (byte) 69,
        (byte) 14,
        (byte) 178,
        (byte) 202
      };
      byte[] numArray3 = new byte[10];
      numArray3[3] = (byte) 77;
      numArray3[1] = (byte) 175;
      numArray3[0] = (byte) 107;
      numArray3[6] = (byte) 33;
      numArray3[8] = (byte) 143;
      numArray3[5] = (byte) 182;
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[7] = (byte) 125;
      numArray3[9] = (byte) 121;
      numArray3[4] = (byte) 35;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[4] = (byte) 117;
    numArray5[1] = (byte) 141;
    numArray5[2] = (byte) 76;
    numArray5[3] = (byte) 221;
    numArray5[0] = (byte) 238;
    numArray5[6] = (byte) 98;
    numArray5[5] = (byte) 216;
    numArray5[7] = (byte) 36;
    numArray5[9] = (byte) 2;
    numArray5[8] = (byte) 184;
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 144 /*0x90*/;
    numArray6[7] = (byte) 15;
    numArray6[6] = (byte) 126;
    numArray6[0] = (byte) 50;
    numArray6[4] = (byte) 91;
    numArray6[5] = (byte) 150;
    numArray6[2] = (byte) 13;
    numArray6[3] = (byte) 254;
    numArray6[8] = (byte) 79;
    numArray6[9] = (byte) 108;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
