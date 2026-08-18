// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash128.MurmurHash3_x86_128
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash128;

internal sealed class MurmurHash3_x86_128 : 
  Hash,
  IHash128,
  IHash,
  IHashWithKey,
  IWithKey,
  ITransformBlock
{
  private uint key;
  private uint h1;
  private uint h2;
  private uint h3;
  private uint h4;
  private uint total_length;
  private int idx;
  private byte[] buf;
  private static readonly uint CKEY = 0;
  private static readonly uint C1 = 597399067;
  private static readonly uint C2 = 2869860233;
  private static readonly uint C3 = 951274213;
  private static readonly uint C4 = 2716044179;
  private static readonly uint C5 = 2246822507;
  private static readonly uint C6 = 3266489909;
  private static readonly uint C7 = 1444728091;
  private static readonly uint C8 = 197830471;
  private static readonly uint C9 = 2530024501;
  private static readonly uint C10 = 850148119;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public MurmurHash3_x86_128()
    : base(16 /*0x10*/, 16 /*0x10*/)
  {
    this.key = MurmurHash3_x86_128.CKEY;
    this.buf = new byte[16 /*0x10*/];
  }

  public override IHash Clone()
  {
    MurmurHash3_x86_128 murmurHash3X86128 = new MurmurHash3_x86_128();
    murmurHash3X86128.key = this.key;
    murmurHash3X86128.h1 = this.h1;
    murmurHash3X86128.h2 = this.h2;
    murmurHash3X86128.h3 = this.h3;
    murmurHash3X86128.h4 = this.h4;
    murmurHash3X86128.total_length = this.total_length;
    murmurHash3X86128.idx = this.idx;
    murmurHash3X86128.buf = this.buf.DeepCopy();
    murmurHash3X86128.BufferSize = this.BufferSize;
    return (IHash) murmurHash3X86128;
  }

  public override void Initialize()
  {
    this.h1 = this.key;
    this.h2 = this.key;
    this.h3 = this.key;
    this.h4 = this.key;
    this.total_length = 0U;
    this.idx = 0;
  }

  public override unsafe IHashResult TransformFinal()
  {
    this.Finish();
    uint[] numArray1 = new uint[4]
    {
      this.h1,
      this.h2,
      this.h3,
      this.h4
    };
    byte[] a_hash = new byte[numArray1.Length * 4];
    uint[] numArray2 = numArray1;
    fixed (byte* dest = a_hash)
    {
      Converters.be32_copy((IntPtr) (numArray1 == null || numArray2.Length == 0 ? (void*) null : (void*) &numArray2[0]), 0, (IntPtr) (void*) dest, 0, a_hash.Length);
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
    this.total_length += (uint) num1;
    fixed (byte* a_in = a_data)
    {
      if (this.idx != 0 && num1 != 0)
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
        uint num4 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, a_index + num3);
        int num5 = num3 + 4;
        uint num6 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, a_index + num5);
        int num7 = num5 + 4;
        uint num8 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, a_index + num7);
        int num9 = num7 + 4;
        uint num10 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, a_index + num9);
        num3 = num9 + 4;
        this.h1 ^= Bits.RotateLeft32(num4 * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
        this.h1 = Bits.RotateLeft32(this.h1, 19);
        this.h1 += this.h2;
        this.h1 = this.h1 * 5U + MurmurHash3_x86_128.C7;
        this.h2 ^= Bits.RotateLeft32(num6 * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
        this.h2 = Bits.RotateLeft32(this.h2, 17);
        this.h2 += this.h3;
        this.h2 = this.h2 * 5U + MurmurHash3_x86_128.C8;
        this.h3 ^= Bits.RotateLeft32(num8 * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
        this.h3 = Bits.RotateLeft32(this.h3, 15);
        this.h3 += this.h4;
        this.h3 = this.h3 * 5U + MurmurHash3_x86_128.C9;
        this.h4 ^= Bits.RotateLeft32(num10 * MurmurHash3_x86_128.C4, 18) * MurmurHash3_x86_128.C1;
        this.h4 = Bits.RotateLeft32(this.h4, 13);
        this.h4 += this.h1;
        this.h4 = this.h4 * 5U + MurmurHash3_x86_128.C10;
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
        this.key = MurmurHash3_x86_128.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(MurmurHash3_x86_128.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
      }
    }
  }

  private void ByteUpdate(byte a_b)
  {
    this.buf[this.idx] = a_b;
    ++this.idx;
    this.ProcessPendings();
  }

  private unsafe void ProcessPendings()
  {
    fixed (byte* a_in = this.buf)
    {
      if (this.idx >= 16 /*0x10*/)
      {
        uint num1 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
        uint num2 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 4);
        uint num3 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 8);
        uint num4 = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 12);
        this.h1 ^= Bits.RotateLeft32(num1 * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
        this.h1 = Bits.RotateLeft32(this.h1, 19);
        this.h1 += this.h2;
        this.h1 = this.h1 * 5U + MurmurHash3_x86_128.C7;
        this.h2 ^= Bits.RotateLeft32(num2 * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
        this.h2 = Bits.RotateLeft32(this.h2, 17);
        this.h2 += this.h3;
        this.h2 = this.h2 * 5U + MurmurHash3_x86_128.C8;
        this.h3 ^= Bits.RotateLeft32(num3 * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
        this.h3 = Bits.RotateLeft32(this.h3, 15);
        this.h3 += this.h4;
        this.h3 = this.h3 * 5U + MurmurHash3_x86_128.C9;
        this.h4 ^= Bits.RotateLeft32(num4 * MurmurHash3_x86_128.C4, 18) * MurmurHash3_x86_128.C1;
        this.h4 = Bits.RotateLeft32(this.h4, 13);
        this.h4 += this.h1;
        this.h4 = this.h4 * 5U + MurmurHash3_x86_128.C10;
        this.idx = 0;
      }
    }
  }

  private void Finish()
  {
    uint num1 = 0;
    uint num2 = 0;
    uint num3 = 0;
    uint num4 = 0;
    int num5 = this.idx;
    switch (num5)
    {
      case 0:
label_25:
        this.h1 ^= this.total_length;
        this.h2 ^= this.total_length;
        this.h3 ^= this.total_length;
        this.h4 ^= this.total_length;
        this.h1 += this.h2;
        this.h1 += this.h3;
        this.h1 += this.h4;
        this.h2 += this.h1;
        this.h3 += this.h1;
        this.h4 += this.h1;
        this.h1 ^= this.h1 >> 16 /*0x10*/;
        this.h1 *= MurmurHash3_x86_128.C5;
        this.h1 ^= this.h1 >> 13;
        this.h1 *= MurmurHash3_x86_128.C6;
        this.h1 ^= this.h1 >> 16 /*0x10*/;
        this.h2 ^= this.h2 >> 16 /*0x10*/;
        this.h2 *= MurmurHash3_x86_128.C5;
        this.h2 ^= this.h2 >> 13;
        this.h2 *= MurmurHash3_x86_128.C6;
        this.h2 ^= this.h2 >> 16 /*0x10*/;
        this.h3 ^= this.h3 >> 16 /*0x10*/;
        this.h3 *= MurmurHash3_x86_128.C5;
        this.h3 ^= this.h3 >> 13;
        this.h3 *= MurmurHash3_x86_128.C6;
        this.h3 ^= this.h3 >> 16 /*0x10*/;
        this.h4 ^= this.h4 >> 16 /*0x10*/;
        this.h4 *= MurmurHash3_x86_128.C5;
        this.h4 ^= this.h4 >> 13;
        this.h4 *= MurmurHash3_x86_128.C6;
        this.h4 ^= this.h4 >> 16 /*0x10*/;
        this.h1 += this.h2;
        this.h1 += this.h3;
        this.h1 += this.h4;
        this.h2 += this.h1;
        this.h3 += this.h1;
        this.h4 += this.h1;
        break;
      case 13:
        this.h4 ^= Bits.RotateLeft32((num4 ^ (uint) this.buf[12]) * MurmurHash3_x86_128.C4, 18) * MurmurHash3_x86_128.C1;
        goto default;
      case 14:
        this.h4 ^= Bits.RotateLeft32((num4 ^ (uint) this.buf[13] << 8 ^ (uint) this.buf[12]) * MurmurHash3_x86_128.C4, 18) * MurmurHash3_x86_128.C1;
        goto default;
      case 15:
        this.h4 ^= Bits.RotateLeft32((num4 ^ (uint) this.buf[14] << 16 /*0x10*/ ^ (uint) this.buf[13] << 8 ^ (uint) this.buf[12]) * MurmurHash3_x86_128.C4, 18) * MurmurHash3_x86_128.C1;
        goto default;
      default:
        if (num5 > 12)
          num5 = 12;
        switch (num5)
        {
          case 9:
            this.h3 ^= Bits.RotateLeft32((num3 ^ (uint) this.buf[8]) * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
            break;
          case 10:
            this.h3 ^= Bits.RotateLeft32((num3 ^ (uint) this.buf[9] << 8 ^ (uint) this.buf[8]) * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
            break;
          case 11:
            this.h3 ^= Bits.RotateLeft32((num3 ^ (uint) this.buf[10] << 16 /*0x10*/ ^ (uint) this.buf[9] << 8 ^ (uint) this.buf[8]) * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
            break;
          case 12:
            this.h3 ^= Bits.RotateLeft32((num3 ^ (uint) this.buf[11] << 24 ^ (uint) this.buf[10] << 16 /*0x10*/ ^ (uint) this.buf[9] << 8 ^ (uint) this.buf[8]) * MurmurHash3_x86_128.C3, 17) * MurmurHash3_x86_128.C4;
            break;
        }
        if (num5 > 8)
          num5 = 8;
        switch (num5)
        {
          case 5:
            this.h2 ^= Bits.RotateLeft32((num2 ^ (uint) this.buf[4]) * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
            break;
          case 6:
            this.h2 ^= Bits.RotateLeft32((num2 ^ (uint) this.buf[5] << 8 ^ (uint) this.buf[4]) * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
            break;
          case 7:
            this.h2 ^= Bits.RotateLeft32((num2 ^ (uint) this.buf[6] << 16 /*0x10*/ ^ (uint) this.buf[5] << 8 ^ (uint) this.buf[4]) * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
            break;
          case 8:
            this.h2 ^= Bits.RotateLeft32((num2 ^ (uint) this.buf[7] << 24 ^ (uint) this.buf[6] << 16 /*0x10*/ ^ (uint) this.buf[5] << 8 ^ (uint) this.buf[4]) * MurmurHash3_x86_128.C2, 16 /*0x10*/) * MurmurHash3_x86_128.C3;
            break;
        }
        if (num5 > 4)
          num5 = 4;
        switch (num5)
        {
          case 1:
            this.h1 ^= Bits.RotateLeft32((num1 ^ (uint) this.buf[0]) * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
            goto label_25;
          case 2:
            this.h1 ^= Bits.RotateLeft32((num1 ^ (uint) this.buf[1] << 8 ^ (uint) this.buf[0]) * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
            goto label_25;
          case 3:
            this.h1 ^= Bits.RotateLeft32((num1 ^ (uint) this.buf[2] << 16 /*0x10*/ ^ (uint) this.buf[1] << 8 ^ (uint) this.buf[0]) * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
            goto label_25;
          case 4:
            this.h1 ^= Bits.RotateLeft32((num1 ^ (uint) this.buf[3] << 24 ^ (uint) this.buf[2] << 16 /*0x10*/ ^ (uint) this.buf[1] << 8 ^ (uint) this.buf[0]) * MurmurHash3_x86_128.C1, 15) * MurmurHash3_x86_128.C2;
            goto label_25;
          default:
            goto label_25;
        }
    }
  }
}
