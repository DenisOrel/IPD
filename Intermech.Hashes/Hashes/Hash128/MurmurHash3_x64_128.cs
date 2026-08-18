// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash128.MurmurHash3_x64_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash128;

internal sealed class MurmurHash3_x64_128 : 
  Hash,
  IHash128,
  IHash,
  IHashWithKey,
  IWithKey,
  ITransformBlock
{
  private ulong h1;
  private ulong h2;
  private ulong total_length;
  private uint key;
  private int idx;
  private byte[] buf;
  private static readonly uint CKEY = 0;
  private static readonly ulong C1 = 9782798678568883157;
  private static readonly ulong C5 = 18397679294719823053;
  private static readonly ulong C6 = 14181476777654086739;
  private static readonly ulong C2 = 5545529020109919103;
  private static readonly uint C3 = 1390208809;
  private static readonly uint C4 = 944331445;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public MurmurHash3_x64_128()
    : base(16 /*0x10*/, 16 /*0x10*/)
  {
    this.key = MurmurHash3_x64_128.CKEY;
    this.buf = new byte[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    MurmurHash3_x64_128 murmurHash3X64128 = new MurmurHash3_x64_128();
    murmurHash3X64128.h1 = this.h1;
    murmurHash3X64128.h2 = this.h2;
    murmurHash3X64128.total_length = this.total_length;
    murmurHash3X64128.key = this.key;
    murmurHash3X64128.idx = this.idx;
    murmurHash3X64128.buf = this.buf.DeepCopy();
    murmurHash3X64128.BufferSize = this.BufferSize;
    return (IHash) murmurHash3X64128;
  }

  public override void Initialize()
  {
    this.h1 = (ulong) this.key;
    this.h2 = (ulong) this.key;
    this.total_length = 0UL;
    this.idx = 0;
  }

  public override unsafe IHashResult TransformFinal()
  {
    this.Finish();
    ulong[] numArray1 = new ulong[2]{ this.h1, this.h2 };
    byte[] a_hash = new byte[numArray1.Length * 8];
    ulong[] numArray2 = numArray1;
    fixed (byte* dest = a_hash)
    {
      Converters.be64_copy((IntPtr) (numArray1 == null || numArray2.Length == 0 ? (void*) null : (void*) &numArray2[0]), 0, (IntPtr) (void*) dest, 0, a_hash.Length);
      HashResult hashResult = new HashResult(a_hash);
      this.Initialize();
      return (IHashResult) hashResult;
    }
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int num1 = a_length;
    int num2 = a_index;
    int num3 = 0;
    this.total_length += (ulong) (uint) num1;
    fixed (byte* a_in = a_data)
    {
      if (this.idx != 0 && a_length != 0)
      {
        for (; this.idx < 16 /*0x10*/ && num1 != 0; --num1)
        {
          this.buf[this.idx++] = a_in[a_index];
          ++a_index;
        }
        if (this.idx == 16 /*0x10*/)
          this.ProcessPendings();
      }
      else
        num2 = 0;
      for (int index = num1 >> 4; num2 < index; ++num2)
      {
        ulong num4 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, a_index + num3);
        int num5 = num3 + 8;
        ulong num6 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, a_index + num5);
        num3 = num5 + 8;
        this.h1 ^= Bits.RotateLeft64(num4 * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
        this.h1 = Bits.RotateLeft64(this.h1, 27);
        this.h1 += this.h2;
        this.h1 = this.h1 * 5UL + (ulong) MurmurHash3_x64_128.C3;
        this.h2 ^= Bits.RotateLeft64(num6 * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        this.h2 = Bits.RotateLeft64(this.h2, 31 /*0x1F*/);
        this.h2 += this.h1;
        this.h2 = this.h2 * 5UL + (ulong) MurmurHash3_x64_128.C4;
      }
      for (int index = a_index + num2 * 16 /*0x10*/; index < a_index + num1; ++index)
        this.ByteUpdate(a_data[index]);
    }
  }

  public int? KeyLength => new int?(4);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt32AsBytesLE(this.key);
    set
    {
      if (value.Empty())
      {
        this.key = MurmurHash3_x64_128.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(MurmurHash3_x64_128.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = value)
          this.key = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
      }
    }
  }

  private void ByteUpdate(byte a_b)
  {
    this.buf[this.idx++] = a_b;
    this.ProcessPendings();
  }

  private unsafe void ProcessPendings()
  {
    fixed (byte* a_in = this.buf)
    {
      if (this.idx >= 16 /*0x10*/)
      {
        ulong num1 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0);
        ulong num2 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 8);
        this.h1 ^= (ulong) (uint) (Bits.RotateLeft64(num1 * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2);
        this.h1 = (ulong) (uint) Bits.RotateLeft64(this.h1, 27);
        this.h1 += this.h2;
        this.h1 = this.h1 * 5UL + (ulong) MurmurHash3_x64_128.C3;
        this.h2 ^= (ulong) (uint) ((ulong) (uint) Bits.RotateLeft64(num2 * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1);
        this.h2 = (ulong) (uint) Bits.RotateLeft64(this.h2, 31 /*0x1F*/);
        this.h2 += this.h1;
        this.h2 = this.h2 * 5UL + (ulong) MurmurHash3_x64_128.C4;
        this.idx = 0;
      }
    }
  }

  private void Finish()
  {
    ulong num1 = 0;
    ulong num2 = 0;
    int num3 = this.idx;
    switch (num3)
    {
      case 0:
label_19:
        this.h1 ^= this.total_length;
        this.h2 ^= this.total_length;
        this.h1 += this.h2;
        this.h2 += this.h1;
        this.h1 ^= this.h1 >> 33;
        this.h1 *= MurmurHash3_x64_128.C5;
        this.h1 ^= this.h1 >> 33;
        this.h1 *= MurmurHash3_x64_128.C6;
        this.h1 ^= this.h1 >> 33;
        this.h2 ^= this.h2 >> 33;
        this.h2 *= MurmurHash3_x64_128.C5;
        this.h2 ^= this.h2 >> 33;
        this.h2 *= MurmurHash3_x64_128.C6;
        this.h2 ^= this.h2 >> 33;
        this.h1 += this.h2;
        this.h2 += this.h1;
        break;
      case 9:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 10:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 11:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[10] << 16 /*0x10*/ ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 12:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[11] << 24 ^ (ulong) this.buf[10] << 16 /*0x10*/ ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 13:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[12] << 32 /*0x20*/ ^ (ulong) this.buf[11] << 24 ^ (ulong) this.buf[10] << 16 /*0x10*/ ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 14:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[13] << 40 ^ (ulong) this.buf[12] << 32 /*0x20*/ ^ (ulong) this.buf[11] << 24 ^ (ulong) this.buf[10] << 16 /*0x10*/ ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      case 15:
        this.h2 ^= Bits.RotateLeft64((num2 ^ (ulong) this.buf[14] << 48 /*0x30*/ ^ (ulong) this.buf[13] << 40 ^ (ulong) this.buf[12] << 32 /*0x20*/ ^ (ulong) this.buf[11] << 24 ^ (ulong) this.buf[10] << 16 /*0x10*/ ^ (ulong) this.buf[9] << 8 ^ (ulong) this.buf[8]) * MurmurHash3_x64_128.C2, 33) * MurmurHash3_x64_128.C1;
        goto default;
      default:
        if (num3 > 8)
          num3 = 8;
        switch (num3)
        {
          case 1:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 2:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 3:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 4:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[3] << 24 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 5:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[4] << 32 /*0x20*/ ^ (ulong) this.buf[3] << 24 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 6:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[5] << 40 ^ (ulong) this.buf[4] << 32 /*0x20*/ ^ (ulong) this.buf[3] << 24 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 7:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[6] << 48 /*0x30*/ ^ (ulong) this.buf[5] << 40 ^ (ulong) this.buf[4] << 32 /*0x20*/ ^ (ulong) this.buf[3] << 24 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          case 8:
            this.h1 ^= Bits.RotateLeft64((num1 ^ (ulong) this.buf[7] << 56 ^ (ulong) this.buf[6] << 48 /*0x30*/ ^ (ulong) this.buf[5] << 40 ^ (ulong) this.buf[4] << 32 /*0x20*/ ^ (ulong) this.buf[3] << 24 ^ (ulong) this.buf[2] << 16 /*0x10*/ ^ (ulong) this.buf[1] << 8 ^ (ulong) this.buf[0]) * MurmurHash3_x64_128.C1, 31 /*0x1F*/) * MurmurHash3_x64_128.C2;
            goto label_19;
          default:
            goto label_19;
        }
    }
  }
}
