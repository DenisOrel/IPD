// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.MurmurHash3_x86_32
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class MurmurHash3_x86_32 : 
  Hash,
  IHash32,
  IHash,
  IHashWithKey,
  IWithKey,
  ITransformBlock
{
  private uint key;
  private uint h;
  private uint total_length;
  private int idx;
  private byte[] buf;
  private static readonly uint CKEY = 0;
  private static readonly uint C1 = 3432918353;
  private static readonly uint C2 = 461845907;
  private static readonly uint C3 = 3864292196;
  private static readonly uint C4 = 2246822507;
  private static readonly uint C5 = 3266489909;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public MurmurHash3_x86_32()
    : base(4, 4)
  {
    this.key = MurmurHash3_x86_32.CKEY;
    this.buf = new byte[4];
  }

  public override void Initialize()
  {
    this.h = this.key;
    this.total_length = 0U;
    this.idx = 0;
  }

  public override IHash Clone()
  {
    MurmurHash3_x86_32 murmurHash3X8632 = new MurmurHash3_x86_32();
    murmurHash3X8632.key = this.key;
    murmurHash3X8632.h = this.h;
    murmurHash3X8632.total_length = this.total_length;
    murmurHash3X8632.idx = this.idx;
    murmurHash3X8632.buf = this.buf.DeepCopy();
    murmurHash3X8632.BufferSize = this.BufferSize;
    return (IHash) murmurHash3X8632;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int num1 = a_length;
    int num2 = a_index;
    this.total_length += (uint) num1;
    fixed (byte* a_in1 = a_data)
      fixed (byte* a_in2 = this.buf)
      {
        if (this.idx != 0 && a_length != 0)
        {
          for (; this.idx < 4 && num1 != 0; --num1)
          {
            this.buf[this.idx++] = a_in1[a_index];
            ++a_index;
          }
          if (this.idx == 4)
          {
            this.TransformUInt32Fast(Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in2, 0));
            this.idx = 0;
          }
        }
        else
          num2 = 0;
        int num3 = num1 >> 2;
        for (; num2 < num3; ++num2)
          this.TransformUInt32Fast(Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in1, a_index + num2 * 4));
        for (int index = a_index + num2 * 4; index < num1 + a_index; ++index)
          this.ByteUpdate(a_data[index]);
      }
  }

  public override IHashResult TransformFinal()
  {
    this.Finish();
    HashResult hashResult = new HashResult(this.h);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  private void TransformUInt32Fast(uint a_data)
  {
    this.h ^= Bits.RotateLeft32(a_data * MurmurHash3_x86_32.C1, 15) * MurmurHash3_x86_32.C2;
    this.h = Bits.RotateLeft32(this.h, 13);
    this.h = this.h * 5U + MurmurHash3_x86_32.C3;
  }

  private unsafe void ByteUpdate(byte a_b)
  {
    this.buf[this.idx] = a_b;
    ++this.idx;
    if (this.idx < 4)
      return;
    uint a_data;
    fixed (byte* a_in = &this.buf[0])
      a_data = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
    this.TransformUInt32Fast(a_data);
    this.idx = 0;
  }

  private void Finish()
  {
    uint num = 0;
    if (this.idx != 0)
    {
      switch (this.idx)
      {
        case 1:
          this.h ^= Bits.RotateLeft32((num ^ (uint) this.buf[0]) * MurmurHash3_x86_32.C1, 15) * MurmurHash3_x86_32.C2;
          break;
        case 2:
          this.h ^= Bits.RotateLeft32((num ^ (uint) this.buf[1] << 8 ^ (uint) this.buf[0]) * MurmurHash3_x86_32.C1, 15) * MurmurHash3_x86_32.C2;
          break;
        case 3:
          this.h ^= Bits.RotateLeft32((num ^ (uint) this.buf[2] << 16 /*0x10*/ ^ (uint) this.buf[1] << 8 ^ (uint) this.buf[0]) * MurmurHash3_x86_32.C1, 15) * MurmurHash3_x86_32.C2;
          break;
      }
    }
    this.h ^= this.total_length;
    this.h ^= this.h >> 16 /*0x10*/;
    this.h *= MurmurHash3_x86_32.C4;
    this.h ^= this.h >> 13;
    this.h *= MurmurHash3_x86_32.C5;
    this.h ^= this.h >> 16 /*0x10*/;
  }

  public int? KeyLength => new int?(4);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt32AsBytesLE(this.key);
    set
    {
      if (value.Empty())
      {
        this.key = MurmurHash3_x86_32.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(MurmurHash3_x86_32.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
      }
    }
  }
}
