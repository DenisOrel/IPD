// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.XXHash64
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal sealed class XXHash64 : Hash, IHash64, IHash, IHashWithKey, IWithKey, ITransformBlock
{
  private ulong key;
  private ulong hash;
  private static readonly uint CKEY = 0;
  private static readonly ulong PRIME64_1 = 11400714785074694791;
  private static readonly ulong PRIME64_2 = 14029467366897019727;
  private static readonly ulong PRIME64_3 = 1609587929392839161;
  private static readonly ulong PRIME64_4 = 9650029242287828579;
  private static readonly ulong PRIME64_5 = 2870177450012600261;
  private XXHash64.XXH_State state;
  private static string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public XXHash64()
    : base(8, 32 /*0x20*/)
  {
    this.key = (ulong) XXHash64.CKEY;
    Array.Resize<byte>(ref this.state.memory, 32 /*0x20*/);
  }

  public override void Initialize()
  {
    this.hash = 0UL;
    this.state.v1 = this.key + XXHash64.PRIME64_1 + XXHash64.PRIME64_2;
    this.state.v2 = this.key + XXHash64.PRIME64_2;
    this.state.v3 = this.key;
    this.state.v4 = this.key - XXHash64.PRIME64_1;
    this.state.total_len = 0UL;
    this.state.memsize = 0U;
  }

  public override IHash Clone()
  {
    XXHash64 xxHash64 = new XXHash64();
    xxHash64.key = this.key;
    xxHash64.hash = this.hash;
    xxHash64.state = this.state.Clone();
    xxHash64.BufferSize = this.BufferSize;
    return (IHash) xxHash64;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.state.total_len += (ulong) a_length;
    fixed (byte* numPtr1 = a_data)
      fixed (byte* numPtr2 = this.state.memory)
      {
        byte* numPtr3 = numPtr1 + a_index;
        if (this.state.memsize + (uint) a_length < 32U /*0x20*/)
        {
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + this.state.memsize), (IntPtr) (void*) numPtr3, a_length);
          this.state.memsize += (uint) a_length;
        }
        else
        {
          byte* numPtr4 = numPtr3 + (uint) a_length;
          if (this.state.memsize > 0U)
          {
            Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + this.state.memsize), (IntPtr) (void*) numPtr3, 32 /*0x20*/ - (int) this.state.memsize);
            this.state.v1 = XXHash64.PRIME64_1 * Bits.RotateLeft64(this.state.v1 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr2, 0), 31 /*0x1F*/);
            this.state.v2 = XXHash64.PRIME64_1 * Bits.RotateLeft64(this.state.v2 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr2, 8), 31 /*0x1F*/);
            this.state.v3 = XXHash64.PRIME64_1 * Bits.RotateLeft64(this.state.v3 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr2, 16 /*0x10*/), 31 /*0x1F*/);
            this.state.v4 = XXHash64.PRIME64_1 * Bits.RotateLeft64(this.state.v4 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr2, 24), 31 /*0x1F*/);
            numPtr3 += 32U /*0x20*/ - this.state.memsize;
            this.state.memsize = 0U;
          }
          if (numPtr3 <= numPtr4 - 32 /*0x20*/)
          {
            ulong num1 = this.state.v1;
            ulong num2 = this.state.v2;
            ulong num3 = this.state.v3;
            ulong num4 = this.state.v4;
            byte* numPtr5 = numPtr4 - 32 /*0x20*/;
            do
            {
              num1 = XXHash64.PRIME64_1 * Bits.RotateLeft64(num1 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr3, 0), 31 /*0x1F*/);
              num2 = XXHash64.PRIME64_1 * Bits.RotateLeft64(num2 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr3, 8), 31 /*0x1F*/);
              num3 = XXHash64.PRIME64_1 * Bits.RotateLeft64(num3 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr3, 16 /*0x10*/), 31 /*0x1F*/);
              num4 = XXHash64.PRIME64_1 * Bits.RotateLeft64(num4 + XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) numPtr3, 24), 31 /*0x1F*/);
              numPtr3 += 32 /*0x20*/;
            }
            while (numPtr3 <= numPtr5);
            this.state.v1 = num1;
            this.state.v2 = num2;
            this.state.v3 = num3;
            this.state.v4 = num4;
          }
          if (numPtr3 < numPtr4)
          {
            Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) numPtr2, (IntPtr) (void*) numPtr3, (int) (numPtr4 - numPtr3));
            this.state.memsize = (uint) (numPtr4 - numPtr3);
          }
          // ISSUE: __unpin statement
          __unpin(numPtr1);
          // ISSUE: __unpin statement
          __unpin(numPtr2);
        }
      }
  }

  public override unsafe IHashResult TransformFinal()
  {
    fixed (byte* numPtr1 = this.state.memory)
    {
      if (this.state.total_len >= 32UL /*0x20*/)
      {
        ulong v1 = this.state.v1;
        ulong v2 = this.state.v2;
        ulong v3 = this.state.v3;
        ulong v4 = this.state.v4;
        this.hash = Bits.RotateLeft64(v1, 1) + Bits.RotateLeft64(v2, 7) + Bits.RotateLeft64(v3, 12) + Bits.RotateLeft64(v4, 18);
        this.hash = (this.hash ^ Bits.RotateLeft64(v1 * XXHash64.PRIME64_2, 31 /*0x1F*/) * XXHash64.PRIME64_1) * XXHash64.PRIME64_1 + XXHash64.PRIME64_4;
        this.hash = (this.hash ^ Bits.RotateLeft64(v2 * XXHash64.PRIME64_2, 31 /*0x1F*/) * XXHash64.PRIME64_1) * XXHash64.PRIME64_1 + XXHash64.PRIME64_4;
        this.hash = (this.hash ^ Bits.RotateLeft64(v3 * XXHash64.PRIME64_2, 31 /*0x1F*/) * XXHash64.PRIME64_1) * XXHash64.PRIME64_1 + XXHash64.PRIME64_4;
        this.hash = (this.hash ^ Bits.RotateLeft64(v4 * XXHash64.PRIME64_2, 31 /*0x1F*/) * XXHash64.PRIME64_1) * XXHash64.PRIME64_1 + XXHash64.PRIME64_4;
      }
      else
        this.hash = this.key + XXHash64.PRIME64_5;
      this.hash += this.state.total_len;
      byte* a_in = numPtr1;
      byte* numPtr2;
      for (numPtr2 = a_in + this.state.memsize; a_in + 8 <= numPtr2; a_in += 8)
      {
        this.hash ^= XXHash64.PRIME64_1 * Bits.RotateLeft64(XXHash64.PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0), 31 /*0x1F*/);
        this.hash = Bits.RotateLeft64(this.hash, 27) * XXHash64.PRIME64_1 + XXHash64.PRIME64_4;
      }
      if (a_in + 4 <= numPtr2)
      {
        this.hash ^= (ulong) Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0) * XXHash64.PRIME64_1;
        this.hash = Bits.RotateLeft64(this.hash, 23) * XXHash64.PRIME64_2 + XXHash64.PRIME64_3;
        a_in += 4;
      }
      for (; a_in < numPtr2; ++a_in)
      {
        this.hash ^= (ulong) *a_in * XXHash64.PRIME64_5;
        this.hash = Bits.RotateLeft64(this.hash, 11) * XXHash64.PRIME64_1;
      }
      this.hash ^= this.hash >> 33;
      this.hash *= XXHash64.PRIME64_2;
      this.hash ^= this.hash >> 29;
      this.hash *= XXHash64.PRIME64_3;
      this.hash ^= this.hash >> 32 /*0x20*/;
    }
    HashResult hashResult = new HashResult(this.hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public int? KeyLength => new int?(8);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt64AsBytesLE(this.key);
    set
    {
      if (value == null || value.Length == 0)
      {
        this.key = (ulong) XXHash64.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(XXHash64.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0);
      }
    }
  }

  private struct XXH_State
  {
    public ulong total_len;
    public ulong v1;
    public ulong v2;
    public ulong v3;
    public ulong v4;
    public uint memsize;
    public byte[] memory;

    public XXHash64.XXH_State Clone()
    {
      return new XXHash64.XXH_State()
      {
        total_len = this.total_len,
        memsize = this.memsize,
        v1 = this.v1,
        v2 = this.v2,
        v3 = this.v3,
        v4 = this.v4,
        memory = this.memory.DeepCopy()
      };
    }
  }
}
