
// Type: ImSSP.sc_3406
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3406
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 68,
    (byte) 35,
    (byte) 189,
    (byte) 176 /*0xB0*/,
    (byte) 211,
    (byte) 164,
    (byte) 163,
    (byte) 138,
    (byte) 96 /*0x60*/,
    (byte) 208 /*0xD0*/,
    (byte) 103,
    (byte) 123,
    (byte) 109,
    (byte) 238,
    (byte) 132,
    (byte) 124,
    (byte) 45,
    (byte) 239,
    (byte) 162,
    (byte) 100,
    (byte) 253,
    (byte) 32 /*0x20*/,
    (byte) 59,
    (byte) 224 /*0xE0*/,
    (byte) 156,
    (byte) 85,
    (byte) 22,
    (byte) 31 /*0x1F*/,
    (byte) 43,
    (byte) 82,
    (byte) 162,
    (byte) 81,
    (byte) 55,
    (byte) 173,
    (byte) 164,
    (byte) 143,
    (byte) 213,
    (byte) 48 /*0x30*/
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 133,
    (byte) 15,
    (byte) 165,
    (byte) 72,
    (byte) 160 /*0xA0*/,
    (byte) 172,
    (byte) 227,
    (byte) 70,
    (byte) 18,
    (byte) 35,
    (byte) 173,
    (byte) 24,
    (byte) 247,
    (byte) 118,
    (byte) 104,
    (byte) 134,
    (byte) 60,
    (byte) 78,
    (byte) 131,
    (byte) 139,
    (byte) 137,
    (byte) 79,
    (byte) 67,
    (byte) 19,
    (byte) 77,
    (byte) 235,
    (byte) 9,
    (byte) 83,
    (byte) 108,
    (byte) 233,
    (byte) 108,
    (byte) 29,
    (byte) 87,
    (byte) 224 /*0xE0*/,
    (byte) 22,
    (byte) 198,
    (byte) 10,
    (byte) 136
  };

  internal static string ssp_imclient_3407()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[13] = (byte) 51;
      numArray2[1] = (byte) 55;
      numArray2[2] = (byte) 184;
      numArray2[7] = (byte) 139;
      numArray2[5] = (byte) 196;
      numArray2[4] = (byte) 95;
      numArray2[9] = (byte) 198;
      numArray2[3] = (byte) 251;
      numArray2[8] = (byte) 116;
      numArray2[14] = (byte) 149;
      numArray2[12] = (byte) 148;
      numArray2[11] = (byte) 13;
      numArray2[0] = (byte) 208 /*0xD0*/;
      numArray2[10] = (byte) 247;
      numArray2[6] = (byte) 250;
      byte[] numArray3 = new byte[15]
      {
        (byte) 185,
        (byte) 104,
        (byte) 98,
        (byte) 169,
        (byte) 50,
        (byte) 203,
        (byte) 47,
        (byte) 178,
        (byte) 71,
        (byte) 254,
        (byte) 109,
        (byte) 204,
        (byte) 216,
        (byte) 235,
        (byte) 86
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[3] = (byte) 5;
    numArray5[6] = (byte) 61;
    numArray5[12] = (byte) 103;
    numArray5[0] = (byte) 233;
    numArray5[7] = (byte) 140;
    numArray5[5] = (byte) 191;
    numArray5[4] = (byte) 72;
    numArray5[10] = (byte) 222;
    numArray5[1] = (byte) 245;
    numArray5[9] = (byte) 213;
    numArray5[8] = (byte) 106;
    numArray5[11] = (byte) 205;
    numArray5[13] = (byte) 77;
    numArray5[2] = (byte) 239;
    numArray5[14] = (byte) 225;
    byte[] numArray6 = new byte[15];
    numArray6[10] = (byte) 131;
    numArray6[1] = (byte) 31 /*0x1F*/;
    numArray6[2] = (byte) 6;
    numArray6[9] = (byte) 174;
    numArray6[8] = (byte) 136;
    numArray6[13] = (byte) 0;
    numArray6[3] = (byte) 63 /*0x3F*/;
    numArray6[7] = (byte) 47;
    numArray6[14] = (byte) 100;
    numArray6[0] = (byte) 232;
    numArray6[6] = (byte) 238;
    numArray6[11] = (byte) 65;
    numArray6[4] = (byte) 186;
    numArray6[5] = (byte) 165;
    numArray6[12] = (byte) 142;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3408()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[11] = (byte) 53;
      numArray2[1] = (byte) 3;
      numArray2[4] = (byte) 181;
      numArray2[12] = (byte) 155;
      numArray2[3] = (byte) 236;
      numArray2[5] = (byte) 85;
      numArray2[6] = (byte) 229;
      numArray2[7] = (byte) 163;
      numArray2[8] = (byte) 161;
      numArray2[9] = (byte) 149;
      numArray2[10] = (byte) 15;
      numArray2[13] = (byte) 235;
      numArray2[0] = (byte) 179;
      numArray2[2] = (byte) 160 /*0xA0*/;
      numArray2[14] = (byte) 47;
      byte[] numArray3 = new byte[15]
      {
        (byte) 178,
        (byte) 101,
        (byte) 123,
        (byte) 193,
        (byte) 28,
        (byte) 17,
        (byte) 46,
        (byte) 124,
        (byte) 223,
        (byte) 189,
        (byte) 129,
        (byte) 136,
        (byte) 107,
        (byte) 241,
        (byte) 225
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 96 /*0x60*/,
      (byte) 236,
      (byte) 56,
      (byte) 249,
      (byte) 125,
      (byte) 187,
      (byte) 145,
      (byte) 197,
      (byte) 155,
      (byte) 4,
      (byte) 124,
      (byte) 214,
      (byte) 145,
      (byte) 173,
      (byte) 175
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 68,
      (byte) 208 /*0xD0*/,
      (byte) 44,
      (byte) 203,
      (byte) 48 /*0x30*/,
      (byte) 250,
      (byte) 22,
      (byte) 135,
      (byte) 254,
      (byte) 209,
      (byte) 37,
      (byte) 134,
      (byte) 254,
      (byte) 156,
      (byte) 137
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3409()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 219,
        (byte) 229,
        (byte) 158,
        (byte) 179,
        (byte) 201,
        (byte) 77,
        (byte) 67,
        (byte) 44,
        (byte) 31 /*0x1F*/,
        (byte) 65,
        (byte) 251,
        (byte) 94,
        (byte) 160 /*0xA0*/,
        (byte) 51,
        (byte) 53,
        (byte) 58
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[12] = (byte) 193;
      numArray3[1] = (byte) 128 /*0x80*/;
      numArray3[5] = (byte) 121;
      numArray3[3] = (byte) 112 /*0x70*/;
      numArray3[4] = (byte) 233;
      numArray3[13] = (byte) 200;
      numArray3[6] = (byte) 119;
      numArray3[7] = (byte) 40;
      numArray3[10] = (byte) 61;
      numArray3[9] = (byte) 171;
      numArray3[0] = (byte) 242;
      numArray3[11] = (byte) 77;
      numArray3[2] = (byte) 184;
      numArray3[8] = (byte) 146;
      numArray3[14] = (byte) 100;
      numArray3[15] = (byte) 22;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[11] = (byte) 219;
    numArray5[8] = (byte) 132;
    numArray5[2] = (byte) 57;
    numArray5[4] = (byte) 171;
    numArray5[0] = (byte) 26;
    numArray5[5] = (byte) 57;
    numArray5[6] = (byte) 143;
    numArray5[13] = (byte) 91;
    numArray5[3] = (byte) 111;
    numArray5[9] = (byte) 159;
    numArray5[10] = (byte) 166;
    numArray5[12] = (byte) 163;
    numArray5[7] = (byte) 113;
    numArray5[1] = (byte) 194;
    numArray5[14] = (byte) 114;
    numArray5[15] = (byte) 175;
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[7] = (byte) 58;
    numArray6[1] = (byte) 212;
    numArray6[2] = (byte) 131;
    numArray6[3] = (byte) 223;
    numArray6[15] = (byte) 186;
    numArray6[4] = (byte) 65;
    numArray6[0] = (byte) 80 /*0x50*/;
    numArray6[6] = (byte) 25;
    numArray6[8] = (byte) 162;
    numArray6[10] = (byte) 215;
    numArray6[9] = (byte) 153;
    numArray6[11] = (byte) 55;
    numArray6[12] = (byte) 227;
    numArray6[13] = (byte) 224 /*0xE0*/;
    numArray6[14] = (byte) 39;
    numArray6[5] = (byte) 69;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3410()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 14,
        (byte) 90,
        (byte) 19,
        (byte) 18,
        (byte) 43,
        (byte) 130,
        (byte) 100,
        (byte) 217,
        (byte) 13,
        (byte) 62,
        (byte) 216,
        (byte) 181,
        (byte) 221,
        (byte) 245,
        (byte) 17,
        (byte) 112 /*0x70*/
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 112 /*0x70*/,
        (byte) 161,
        (byte) 79,
        (byte) 252,
        (byte) 50,
        (byte) 54,
        (byte) 101,
        (byte) 53,
        (byte) 253,
        (byte) 23,
        (byte) 117,
        (byte) 31 /*0x1F*/,
        (byte) 78,
        (byte) 18,
        (byte) 44,
        (byte) 98
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 179,
      (byte) 147,
      (byte) 224 /*0xE0*/,
      (byte) 106,
      (byte) 112 /*0x70*/,
      (byte) 173,
      (byte) 45,
      (byte) 97,
      (byte) 40,
      (byte) 249,
      (byte) 112 /*0x70*/,
      (byte) 232,
      (byte) 193,
      (byte) 150,
      (byte) 177,
      (byte) 162
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 174,
      (byte) 101,
      (byte) 157,
      (byte) 222,
      (byte) 62,
      (byte) 139,
      (byte) 154,
      (byte) 29,
      (byte) 110,
      (byte) 90,
      (byte) 184,
      (byte) 181,
      (byte) 189,
      (byte) 92,
      (byte) 133,
      (byte) 249
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_3406.sspq, 0, (Array) numArray7, 0, 38);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_3406.sspr, 0, (Array) numArray7, 0, 38);
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

  internal static string ssp_imclient_3411()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[13] = (byte) 124;
      numArray2[5] = (byte) 145;
      numArray2[3] = (byte) 1;
      numArray2[1] = (byte) 10;
      numArray2[4] = (byte) 87;
      numArray2[12] = (byte) 127 /*0x7F*/;
      numArray2[6] = (byte) 245;
      numArray2[10] = (byte) 53;
      numArray2[8] = (byte) 211;
      numArray2[0] = (byte) 204;
      numArray2[7] = (byte) 123;
      numArray2[11] = (byte) 116;
      numArray2[2] = (byte) 163;
      numArray2[9] = (byte) 17;
      numArray2[14] = (byte) 164;
      byte[] numArray3 = new byte[15]
      {
        (byte) 252,
        (byte) 31 /*0x1F*/,
        (byte) 229,
        (byte) 158,
        (byte) 229,
        (byte) 71,
        (byte) 27,
        (byte) 81,
        (byte) 16 /*0x10*/,
        (byte) 134,
        (byte) 245,
        (byte) 35,
        (byte) 45,
        (byte) 4,
        (byte) 195
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 89,
      (byte) 214,
      (byte) 90,
      (byte) 90,
      (byte) 31 /*0x1F*/,
      (byte) 227,
      (byte) 79,
      (byte) 166,
      (byte) 218,
      (byte) 66,
      (byte) 14,
      (byte) 245,
      (byte) 221,
      (byte) 34,
      (byte) 212
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 206,
      (byte) 35,
      (byte) 76,
      (byte) 148,
      (byte) 112 /*0x70*/,
      (byte) 180,
      (byte) 143,
      (byte) 238,
      (byte) 107,
      (byte) 83,
      (byte) 229,
      (byte) 228,
      (byte) 23,
      (byte) 157,
      (byte) 149
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_3412()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 248,
        (byte) 27,
        (byte) 23,
        (byte) 62,
        (byte) 134,
        (byte) 67,
        (byte) 244,
        (byte) 41,
        (byte) 122,
        (byte) 177,
        (byte) 187,
        (byte) 138,
        (byte) 27,
        (byte) 143,
        (byte) 119
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 221,
        (byte) 216,
        (byte) 69,
        (byte) 216,
        (byte) 165,
        (byte) 27,
        (byte) 171,
        (byte) 243,
        (byte) 236,
        (byte) 6,
        (byte) 218,
        (byte) 122,
        (byte) 73,
        (byte) 30,
        (byte) 238
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 243,
      (byte) 4,
      (byte) 4,
      (byte) 142,
      (byte) 109,
      (byte) 164,
      (byte) 75,
      (byte) 116,
      (byte) 105,
      (byte) 47,
      (byte) 11,
      (byte) 65,
      (byte) 176 /*0xB0*/,
      (byte) 97,
      (byte) 200
    };
    byte[] numArray6 = new byte[15];
    numArray6[9] = (byte) 25;
    numArray6[11] = (byte) 188;
    numArray6[4] = (byte) 109;
    numArray6[3] = (byte) 199;
    numArray6[1] = (byte) 133;
    numArray6[5] = (byte) 243;
    numArray6[6] = (byte) 244;
    numArray6[12] = (byte) 195;
    numArray6[8] = (byte) 40;
    numArray6[10] = (byte) 72;
    numArray6[2] = (byte) 65;
    numArray6[0] = (byte) 135;
    numArray6[13] = (byte) 54;
    numArray6[7] = (byte) 214;
    numArray6[14] = (byte) 151;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
