// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.SipHash
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal abstract class SipHash : Hash, IHash64, IHash, IHashWithKey, IWithKey, ITransformBlock
{
  protected ulong v0;
  protected ulong v1;
  protected ulong v2;
  protected ulong v3;
  protected ulong key0;
  protected ulong key1;
  protected ulong total_length;
  protected ulong m;
  protected int cr;
  protected int fr;
  protected int idx;
  protected byte[] buf;
  private static readonly ulong V0 = 8317987319222330741;
  private static readonly ulong V1 = 7237128888997146477;
  private static readonly ulong V2 = 7816392313619706465;
  private static readonly ulong V3 = 8387220255154660723;
  private static readonly ulong KEY0 = 506097522914230528 /*0x0706050403020100*/;
  private static readonly ulong KEY1 = 1084818905618843912;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public SipHash(int a_compression_rounds = 2, int a_finalization_rounds = 4)
    : base(8, 8)
  {
    this.key0 = SipHash.KEY0;
    this.key1 = SipHash.KEY1;
    this.cr = a_compression_rounds;
    this.fr = a_finalization_rounds;
    Array.Resize<byte>(ref this.buf, 8);
  }

  public override void Initialize()
  {
    this.v0 = SipHash.V0;
    this.v1 = SipHash.V1;
    this.v2 = SipHash.V2;
    this.v3 = SipHash.V3;
    this.total_length = 0UL;
    this.idx = 0;
    this.v3 ^= this.key1;
    this.v2 ^= this.key0;
    this.v1 ^= this.key1;
    this.v0 ^= this.key0;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int num1 = a_length;
    int num2 = a_index;
    this.total_length += (ulong) (uint) num1;
    fixed (byte* a_in1 = a_data)
      fixed (byte* a_in2 = this.buf)
      {
        if (this.idx != 0 && a_length != 0)
        {
          for (; this.idx < 8 && num1 != 0; --num1)
          {
            this.buf[this.idx] = a_in1[a_index];
            ++this.idx;
            ++a_index;
          }
          if (this.idx == 8)
          {
            this.m = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in2, 0);
            this.ProcessBlock(this.m);
            this.idx = 0;
          }
        }
        else
          num2 = 0;
        for (int index = num1 >> 3; num2 < index; ++num2)
        {
          this.m = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in1, a_index + num2 * 8);
          this.ProcessBlock(this.m);
        }
        for (int index = a_index + num2 * 8; index < num1 + a_index; ++index)
          this.ByteUpdate(a_data[index]);
      }
  }

  public override IHashResult TransformFinal()
  {
    this.Finish();
    byte[] a_out = new byte[this.HashSize];
    Converters.ReadUInt64AsBytesLE(this.v0 ^ this.v1 ^ this.v2 ^ this.v3, ref a_out, 0);
    HashResult hashResult = new HashResult(a_out);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  private void Compress()
  {
    this.v0 += this.v1;
    this.v2 += this.v3;
    this.v1 = Bits.RotateLeft64(this.v1, 13);
    this.v3 = Bits.RotateLeft64(this.v3, 16 /*0x10*/);
    this.v1 ^= this.v0;
    this.v3 ^= this.v2;
    this.v0 = Bits.RotateLeft64(this.v0, 32 /*0x20*/);
    this.v2 += this.v1;
    this.v0 += this.v3;
    this.v1 = Bits.RotateLeft64(this.v1, 17);
    this.v3 = Bits.RotateLeft64(this.v3, 21);
    this.v1 ^= this.v2;
    this.v3 ^= this.v0;
    this.v2 = Bits.RotateLeft64(this.v2, 32 /*0x20*/);
  }

  private void CompressTimes(int a_times)
  {
    for (int index = 0; index < a_times; ++index)
      this.Compress();
  }

  private void ProcessBlock(ulong a_m)
  {
    this.v3 ^= a_m;
    this.CompressTimes(this.cr);
    this.v0 ^= a_m;
  }

  private unsafe void ByteUpdate(byte a_b)
  {
    this.buf[this.idx] = a_b;
    ++this.idx;
    if (this.idx < 8)
      return;
    fixed (byte* a_in = this.buf)
    {
      this.ProcessBlock(Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0));
      this.idx = 0;
    }
  }

  private void Finish()
  {
    ulong num = (ulong) (((long) this.total_length & (long) byte.MaxValue) << 56);
    if (this.idx != 0)
    {
      switch (this.idx)
      {
        case 1:
          num |= (ulong) this.buf[0];
          break;
        case 2:
          num = num | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
        case 3:
          num = num | (ulong) this.buf[2] << 16 /*0x10*/ | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
        case 4:
          num = num | (ulong) this.buf[3] << 24 | (ulong) this.buf[2] << 16 /*0x10*/ | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
        case 5:
          num = num | (ulong) this.buf[4] << 32 /*0x20*/ | (ulong) this.buf[3] << 24 | (ulong) this.buf[2] << 16 /*0x10*/ | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
        case 6:
          num = num | (ulong) this.buf[5] << 40 | (ulong) this.buf[4] << 32 /*0x20*/ | (ulong) this.buf[3] << 24 | (ulong) this.buf[2] << 16 /*0x10*/ | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
        case 7:
          num = num | (ulong) this.buf[6] << 48 /*0x30*/ | (ulong) this.buf[5] << 40 | (ulong) this.buf[4] << 32 /*0x20*/ | (ulong) this.buf[3] << 24 | (ulong) this.buf[2] << 16 /*0x10*/ | (ulong) this.buf[1] << 8 | (ulong) this.buf[0];
          break;
      }
    }
    this.v3 ^= num;
    this.CompressTimes(this.cr);
    this.v0 ^= num;
    this.v2 ^= (ulong) byte.MaxValue;
    this.CompressTimes(this.fr);
  }

  public virtual int? KeyLength => new int?(16 /*0x10*/);

  public virtual unsafe byte[] Key
  {
    get
    {
      byte[] a_out = new byte[this.KeyLength.Value];
      Converters.ReadUInt64AsBytesLE(this.key0, ref a_out, 0);
      Converters.ReadUInt64AsBytesLE(this.key1, ref a_out, 8);
      return a_out;
    }
    set
    {
      if (value == null || value.Length == 0)
      {
        this.key0 = SipHash.KEY0;
        this.key1 = SipHash.KEY1;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(SipHash.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
        {
          this.key0 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0);
          this.key1 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 8);
        }
      }
    }
  }
}
