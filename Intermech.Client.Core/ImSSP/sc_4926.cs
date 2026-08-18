
// Type: ImSSP.sc_4926
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4926
{
  internal static string ssp_imclient_4927()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 242,
        (byte) 77,
        (byte) 102,
        (byte) 102,
        (byte) 184,
        (byte) 72,
        (byte) 4,
        (byte) 92,
        (byte) 245,
        (byte) 185,
        (byte) 138,
        (byte) 102,
        (byte) 63 /*0x3F*/,
        (byte) 139,
        (byte) 168,
        (byte) 94,
        (byte) 150,
        (byte) 160 /*0xA0*/,
        (byte) 194,
        (byte) 184,
        (byte) 204,
        (byte) 219,
        (byte) 100
      };
      byte[] numArray3 = new byte[23];
      numArray3[20] = (byte) 71;
      numArray3[18] = (byte) 217;
      numArray3[17] = (byte) 173;
      numArray3[1] = (byte) 39;
      numArray3[13] = (byte) 229;
      numArray3[5] = (byte) 237;
      numArray3[6] = (byte) 200;
      numArray3[2] = (byte) 185;
      numArray3[8] = (byte) 12;
      numArray3[9] = (byte) 187;
      numArray3[10] = (byte) 223;
      numArray3[15] = (byte) 230;
      numArray3[12] = (byte) 151;
      numArray3[7] = (byte) 38;
      numArray3[14] = (byte) 174;
      numArray3[22] = (byte) 149;
      numArray3[0] = (byte) 71;
      numArray3[21] = (byte) 216;
      numArray3[11] = (byte) 187;
      numArray3[19] = (byte) 178;
      numArray3[4] = (byte) 196;
      numArray3[16 /*0x10*/] = (byte) 147;
      numArray3[3] = (byte) 160 /*0xA0*/;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 85,
      (byte) 137,
      (byte) 25,
      (byte) 14,
      (byte) 227,
      (byte) 65,
      (byte) 22,
      (byte) 232,
      (byte) 166,
      (byte) 227,
      (byte) 201,
      (byte) 198,
      (byte) 199,
      (byte) 75,
      (byte) 160 /*0xA0*/,
      (byte) 142,
      (byte) 218,
      (byte) 34,
      (byte) 106,
      (byte) 77,
      (byte) 190,
      (byte) 67,
      (byte) 30
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 194,
      (byte) 180,
      (byte) 167,
      (byte) 35,
      (byte) 6,
      (byte) 220,
      (byte) 143,
      (byte) 232,
      (byte) 134,
      (byte) 181,
      (byte) 199,
      (byte) 148,
      (byte) 103,
      (byte) 185,
      (byte) 192 /*0xC0*/,
      (byte) 192 /*0xC0*/,
      (byte) 115,
      (byte) 96 /*0x60*/,
      (byte) 18,
      (byte) 235,
      (byte) 1,
      (byte) 210,
      (byte) 163
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4928()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[18] = (byte) 187;
      numArray2[12] = (byte) 147;
      numArray2[19] = (byte) 217;
      numArray2[3] = (byte) 65;
      numArray2[4] = (byte) 206;
      numArray2[0] = (byte) 152;
      numArray2[22] = (byte) 54;
      numArray2[6] = (byte) 89;
      numArray2[21] = (byte) 162;
      numArray2[1] = (byte) 158;
      numArray2[10] = (byte) 161;
      numArray2[11] = (byte) 95;
      numArray2[7] = (byte) 187;
      numArray2[13] = (byte) 39;
      numArray2[16 /*0x10*/] = (byte) 148;
      numArray2[15] = (byte) 117;
      numArray2[14] = (byte) 1;
      numArray2[2] = (byte) 230;
      numArray2[8] = (byte) 203;
      numArray2[17] = (byte) 175;
      numArray2[5] = (byte) 146;
      numArray2[20] = (byte) 153;
      numArray2[9] = (byte) 17;
      byte[] numArray3 = new byte[23]
      {
        (byte) 1,
        (byte) 36,
        (byte) 77,
        (byte) 140,
        (byte) 236,
        (byte) 201,
        (byte) 17,
        (byte) 254,
        (byte) 77,
        (byte) 135,
        (byte) 53,
        (byte) 245,
        (byte) 0,
        (byte) 10,
        (byte) 21,
        byte.MaxValue,
        (byte) 47,
        (byte) 185,
        (byte) 135,
        (byte) 180,
        (byte) 119,
        (byte) 19,
        (byte) 158
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[9] = (byte) 36;
    numArray5[14] = (byte) 96 /*0x60*/;
    numArray5[21] = (byte) 181;
    numArray5[0] = (byte) 206;
    numArray5[4] = (byte) 61;
    numArray5[10] = (byte) 25;
    numArray5[6] = (byte) 246;
    numArray5[13] = (byte) 169;
    numArray5[8] = (byte) 232;
    numArray5[12] = (byte) 50;
    numArray5[5] = (byte) 78;
    numArray5[11] = (byte) 240 /*0xF0*/;
    numArray5[7] = (byte) 104;
    numArray5[2] = (byte) 120;
    numArray5[20] = (byte) 184;
    numArray5[15] = (byte) 31 /*0x1F*/;
    numArray5[16 /*0x10*/] = (byte) 210;
    numArray5[17] = (byte) 147;
    numArray5[18] = (byte) 57;
    numArray5[19] = (byte) 169;
    numArray5[3] = (byte) 131;
    numArray5[1] = (byte) 137;
    numArray5[22] = (byte) 243;
    byte[] numArray6 = new byte[23];
    numArray6[19] = (byte) 221;
    numArray6[1] = (byte) 84;
    numArray6[0] = (byte) 144 /*0x90*/;
    numArray6[21] = (byte) 39;
    numArray6[4] = (byte) 66;
    numArray6[5] = (byte) 97;
    numArray6[17] = (byte) 217;
    numArray6[18] = (byte) 126;
    numArray6[3] = (byte) 217;
    numArray6[9] = (byte) 53;
    numArray6[7] = (byte) 187;
    numArray6[11] = (byte) 54;
    numArray6[14] = (byte) 128 /*0x80*/;
    numArray6[13] = (byte) 199;
    numArray6[10] = (byte) 26;
    numArray6[15] = (byte) 222;
    numArray6[16 /*0x10*/] = (byte) 110;
    numArray6[22] = (byte) 223;
    numArray6[2] = (byte) 200;
    numArray6[6] = (byte) 132;
    numArray6[20] = (byte) 14;
    numArray6[8] = (byte) 244;
    numArray6[12] = (byte) 190;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
