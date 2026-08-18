// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class Haval : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  protected int rounds;
  protected uint[] hash;
  public const int HAVAL_VERSION = 1;
  public static readonly string InvalidHavalRound = "Haval Round Must be 3, 4 | 5";
  public static readonly string InvalidHavalHashSize = "Haval HashSize Must be Either 128 bit(16 byte), 160 bit(20 byte), 192 bit(24 byte), 224 bit(28 byte) | 256 bit(32 byte)";

  protected Haval(HashRounds a_rounds, HashSizeEnum a_hash_size)
    : base((int) a_hash_size, 128 /*0x80*/)
  {
    this.rounds = (int) a_rounds;
    this.hash = new uint[8];
  }

  public override string Name => $"Haval_{this.rounds}_{this.HashSize * 8}";

  public override void Initialize()
  {
    this.hash[0] = 608135816U;
    this.hash[1] = 2242054355U;
    this.hash[2] = 320440878U;
    this.hash[3] = 57701188U;
    this.hash[4] = 2752067618U;
    this.hash[5] = 698298832U;
    this.hash[6] = 137296536U;
    this.hash[7] = 3964562569U;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    this.TailorDigestBits();
    byte[] result = new byte[(this.HashSize >> 2) * 4];
    fixed (uint* src = this.hash)
      fixed (byte* dest = result)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override void Finish()
  {
    long x = (long) this.processed_bytes * 8L;
    int index1 = this.buffer.Position >= 118 ? 246 - this.buffer.Position : 118 - this.buffer.Position;
    byte[] a_out = new byte[index1 + 10];
    a_out[0] = (byte) 1;
    a_out[index1] = (byte) (this.rounds << 3 | 1);
    int index2 = index1 + 1;
    a_out[index2] = (byte) (this.HashSize << 1);
    int a_index = index2 + 1;
    Converters.ReadUInt64AsBytesLE(Converters.le2me_64((ulong) x), ref a_out, a_index);
    int a_length = a_index + 8;
    this.TransformBytes(a_out, 0, a_length);
  }

  private void TailorDigestBits()
  {
    if (this.HashSize == 16 /*0x10*/)
    {
      this.hash[0] = this.hash[0] + Bits.RotateRight32((uint) ((int) this.hash[7] & (int) byte.MaxValue | (int) this.hash[6] & -16777216 /*0xFF000000*/ | (int) this.hash[5] & 16711680 /*0xFF0000*/ | (int) this.hash[4] & 65280), 8);
      this.hash[1] = this.hash[1] + Bits.RotateRight32((uint) ((int) this.hash[7] & 65280 | (int) this.hash[6] & (int) byte.MaxValue | (int) this.hash[5] & -16777216 /*0xFF000000*/ | (int) this.hash[4] & 16711680 /*0xFF0000*/), 16 /*0x10*/);
      this.hash[2] = this.hash[2] + Bits.RotateRight32((uint) ((int) this.hash[7] & 16711680 /*0xFF0000*/ | (int) this.hash[6] & 65280 | (int) this.hash[5] & (int) byte.MaxValue | (int) this.hash[4] & -16777216 /*0xFF000000*/), 24);
      this.hash[3] = this.hash[3] + (uint) ((int) this.hash[7] & -16777216 /*0xFF000000*/ | (int) this.hash[6] & 16711680 /*0xFF0000*/ | (int) this.hash[5] & 65280 | (int) this.hash[4] & (int) byte.MaxValue);
    }
    else if (this.HashSize == 20)
    {
      this.hash[0] = this.hash[0] + Bits.RotateRight32((uint) ((int) this.hash[7] & 63 /*0x3F*/ | (int) (uint) ((ulong) this.hash[6] & 18446744073675997184UL) | (int) this.hash[5] & 33030144 /*0x01F80000*/), 19);
      this.hash[1] = this.hash[1] + Bits.RotateRight32((uint) ((int) this.hash[7] & 4032 | (int) this.hash[6] & 63 /*0x3F*/) | (uint) ((ulong) this.hash[5] & 18446744073675997184UL), 25);
      this.hash[2] = this.hash[2] + (uint) ((int) this.hash[7] & 520192 /*0x07F000*/ | (int) this.hash[6] & 4032 | (int) this.hash[5] & 63 /*0x3F*/);
      this.hash[3] = this.hash[3] + (uint) (((int) this.hash[7] & 33030144 /*0x01F80000*/ | (int) this.hash[6] & 520192 /*0x07F000*/ | (int) this.hash[5] & 4032) >>> 6);
      this.hash[4] = this.hash[4] + (uint) (((int) this.hash[7] & -33554432 /*0xFE000000*/ | (int) this.hash[6] & 33030144 /*0x01F80000*/ | (int) this.hash[5] & 520192 /*0x07F000*/) >>> 12);
    }
    else if (this.HashSize == 24)
    {
      this.hash[0] = this.hash[0] + Bits.RotateRight32(this.hash[7] & 31U /*0x1F*/ | (uint) ((ulong) this.hash[6] & 18446744073642442752UL), 26);
      this.hash[1] = this.hash[1] + (uint) ((int) this.hash[7] & 992 | (int) this.hash[6] & 31 /*0x1F*/);
      this.hash[2] = this.hash[2] + (uint) (((int) this.hash[7] & 64512 | (int) this.hash[6] & 992) >>> 5);
      this.hash[3] = this.hash[3] + (uint) (((int) this.hash[7] & 2031616 /*0x1F0000*/ | (int) this.hash[6] & 64512) >>> 10);
      this.hash[4] = this.hash[4] + (uint) (((int) this.hash[7] & 65011712 /*0x03E00000*/ | (int) this.hash[6] & 2031616 /*0x1F0000*/) >>> 16 /*0x10*/);
      this.hash[5] = this.hash[5] + (((uint) ((ulong) this.hash[7] & 18446744073642442752UL) | this.hash[6] & 65011712U /*0x03E00000*/) >> 21);
    }
    else
    {
      if (this.HashSize != 28)
        return;
      this.hash[0] = this.hash[0] + (this.hash[7] >> 27 & 31U /*0x1F*/);
      this.hash[1] = this.hash[1] + (this.hash[7] >> 22 & 31U /*0x1F*/);
      this.hash[2] = this.hash[2] + (this.hash[7] >> 18 & 15U);
      this.hash[3] = this.hash[3] + (this.hash[7] >> 13 & 31U /*0x1F*/);
      this.hash[4] = this.hash[4] + (this.hash[7] >> 9 & 15U);
      this.hash[5] = this.hash[5] + (this.hash[7] >> 4 & 31U /*0x1F*/);
      this.hash[6] = this.hash[6] + (this.hash[7] & 15U);
    }
  }
}
