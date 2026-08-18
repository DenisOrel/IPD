// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12548
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12548
{
  internal static int ssp_appserver_12549(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 201,
      (byte) 202,
      (byte) 129,
      (byte) 227,
      (byte) 239,
      (byte) 227,
      (byte) 169,
      (byte) 24,
      (byte) 205,
      (byte) 178,
      (byte) 150,
      (byte) 114,
      (byte) 40,
      (byte) 111,
      (byte) 125,
      (byte) 129,
      (byte) 66,
      (byte) 199,
      (byte) 239,
      (byte) 129,
      (byte) 144 /*0x90*/,
      (byte) 62,
      (byte) 105,
      (byte) 123,
      (byte) 220,
      (byte) 186,
      (byte) 121,
      (byte) 104,
      (byte) 151,
      (byte) 202,
      (byte) 158,
      (byte) 51,
      (byte) 99,
      (byte) 230,
      (byte) 213,
      (byte) 127 /*0x7F*/,
      (byte) 97,
      (byte) 219,
      (byte) 68,
      (byte) 16 /*0x10*/,
      (byte) 58,
      (byte) 188,
      (byte) 125,
      (byte) 28,
      (byte) 110,
      (byte) 34,
      (byte) 226,
      (byte) 223
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 242,
      (byte) 6,
      (byte) 211,
      (byte) 179,
      (byte) 12,
      (byte) 233,
      (byte) 124,
      (byte) 65,
      (byte) 112 /*0x70*/,
      (byte) 116,
      (byte) 247,
      (byte) 52,
      (byte) 229,
      (byte) 78,
      (byte) 144 /*0x90*/,
      (byte) 39,
      (byte) 214,
      (byte) 38,
      (byte) 232,
      (byte) 139,
      (byte) 20,
      (byte) 209,
      (byte) 224 /*0xE0*/,
      (byte) 253,
      (byte) 134,
      (byte) 212,
      (byte) 181,
      (byte) 166,
      (byte) 231,
      (byte) 101,
      (byte) 21,
      (byte) 87,
      (byte) 74,
      (byte) 60,
      (byte) 250,
      (byte) 106,
      (byte) 211,
      (byte) 247,
      (byte) 177,
      (byte) 13,
      (byte) 22,
      (byte) 175,
      (byte) 69,
      (byte) 3,
      (byte) 100,
      (byte) 164,
      (byte) 42,
      (byte) 5
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12550(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 28,
      (byte) 154,
      (byte) 138,
      (byte) 96 /*0x60*/,
      (byte) 5,
      (byte) 6,
      (byte) 53,
      (byte) 106,
      (byte) 65,
      (byte) 128 /*0x80*/,
      (byte) 2,
      (byte) 20,
      (byte) 99,
      (byte) 14,
      (byte) 66,
      (byte) 153,
      (byte) 254,
      (byte) 31 /*0x1F*/,
      (byte) 102,
      (byte) 130,
      (byte) 223,
      (byte) 71,
      (byte) 212,
      (byte) 192 /*0xC0*/,
      (byte) 77,
      (byte) 12,
      (byte) 185,
      (byte) 215,
      (byte) 154,
      (byte) 179,
      (byte) 33,
      (byte) 207,
      (byte) 221,
      (byte) 49,
      (byte) 71,
      (byte) 75,
      (byte) 75,
      (byte) 30,
      (byte) 91,
      (byte) 213,
      (byte) 15,
      (byte) 166,
      (byte) 249,
      (byte) 59,
      (byte) 123,
      (byte) 8,
      (byte) 11,
      (byte) 20
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 173,
      (byte) 88,
      (byte) 23,
      (byte) 53,
      (byte) 208 /*0xD0*/,
      (byte) 192 /*0xC0*/,
      (byte) 137,
      (byte) 143,
      (byte) 253,
      (byte) 36,
      (byte) 167,
      (byte) 196,
      (byte) 12,
      (byte) 176 /*0xB0*/,
      (byte) 190,
      (byte) 40,
      (byte) 101,
      (byte) 63 /*0x3F*/,
      (byte) 247,
      (byte) 89,
      (byte) 56,
      (byte) 46,
      (byte) 156,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 122,
      (byte) 131,
      (byte) 5,
      (byte) 74,
      (byte) 140,
      (byte) 75,
      (byte) 78,
      (byte) 56,
      (byte) 230,
      (byte) 54,
      (byte) 21,
      (byte) 206,
      (byte) 49,
      (byte) 198,
      (byte) 43,
      (byte) 170,
      (byte) 209,
      (byte) 200,
      (byte) 25,
      (byte) 186,
      (byte) 202,
      (byte) 236,
      (byte) 6
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12551(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 62,
      (byte) 34,
      (byte) 213,
      (byte) 197,
      (byte) 201,
      (byte) 19,
      (byte) 249,
      (byte) 76,
      (byte) 94,
      (byte) 208 /*0xD0*/,
      (byte) 142,
      (byte) 95,
      (byte) 50,
      (byte) 54,
      (byte) 49,
      (byte) 199,
      (byte) 246,
      (byte) 78,
      (byte) 224 /*0xE0*/,
      (byte) 195,
      (byte) 145,
      (byte) 115,
      (byte) 57,
      (byte) 77,
      (byte) 153,
      (byte) 69,
      (byte) 69,
      (byte) 1,
      (byte) 165,
      (byte) 54,
      (byte) 83,
      (byte) 252,
      (byte) 201,
      (byte) 59,
      (byte) 159,
      (byte) 15,
      (byte) 113,
      (byte) 134,
      (byte) 129,
      (byte) 11,
      (byte) 98,
      (byte) 136,
      (byte) 177,
      (byte) 214,
      (byte) 107,
      (byte) 219,
      (byte) 72,
      (byte) 209
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 51,
      (byte) 1,
      (byte) 92,
      (byte) 180,
      (byte) 118,
      (byte) 2,
      (byte) 141,
      (byte) 228,
      (byte) 53,
      (byte) 69,
      (byte) 43,
      (byte) 88,
      (byte) 185,
      (byte) 32 /*0x20*/,
      (byte) 54,
      (byte) 176 /*0xB0*/,
      (byte) 248,
      (byte) 224 /*0xE0*/,
      (byte) 58,
      (byte) 122,
      (byte) 112 /*0x70*/,
      (byte) 8,
      (byte) 194,
      (byte) 33,
      (byte) 214,
      (byte) 109,
      (byte) 224 /*0xE0*/,
      (byte) 144 /*0x90*/,
      (byte) 236,
      (byte) 20,
      (byte) 29,
      (byte) 207,
      (byte) 24,
      (byte) 164,
      (byte) 208 /*0xD0*/,
      (byte) 4,
      (byte) 222,
      (byte) 115,
      (byte) 172,
      (byte) 110,
      (byte) 62,
      (byte) 68,
      (byte) 67,
      (byte) 131,
      (byte) 203,
      (byte) 211,
      (byte) 80 /*0x50*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
