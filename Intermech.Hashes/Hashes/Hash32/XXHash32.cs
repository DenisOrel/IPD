// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.XXHash32
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class XXHash32 : Hash, IHash32, IHash, IHashWithKey, IWithKey, ITransformBlock
{
  private uint key;
  private uint hash;
  private static readonly uint CKEY = 0;
  private static readonly uint PRIME32_1 = 2654435761;
  private static readonly uint PRIME32_2 = 2246822519;
  private static readonly uint PRIME32_3 = 3266489917;
  private static readonly uint PRIME32_4 = 668265263;
  private static readonly uint PRIME32_5 = 374761393;
  private XXHash32.XXH_State state;
  private static string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public XXHash32()
    : base(4, 16 /*0x10*/)
  {
    this.key = XXHash32.CKEY;
    this.state.memory = new byte[16 /*0x10*/];
  }

  public override void Initialize()
  {
    this.hash = 0U;
    this.state.v1 = this.key + XXHash32.PRIME32_1 + XXHash32.PRIME32_2;
    this.state.v2 = this.key + XXHash32.PRIME32_2;
    this.state.v3 = this.key;
    this.state.v4 = this.key - XXHash32.PRIME32_1;
    this.state.total_len = 0UL;
    this.state.memsize = 0U;
  }

  public override IHash Clone()
  {
    XXHash32 xxHash32 = new XXHash32();
    xxHash32.key = this.key;
    xxHash32.hash = this.hash;
    xxHash32.state = this.state.Clone();
    xxHash32.BufferSize = this.BufferSize;
    return (IHash) xxHash32;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    fixed (byte* numPtr1 = a_data)
      fixed (byte* numPtr2 = this.state.memory)
      {
        byte* numPtr3 = numPtr1 + a_index;
        this.state.total_len += (ulong) a_length;
        if (this.state.memsize + (uint) a_length < 16U /*0x10*/)
        {
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + this.state.memsize), (IntPtr) (void*) numPtr3, a_length);
          this.state.memsize += (uint) a_length;
        }
        else
        {
          byte* numPtr4 = numPtr3 + (uint) a_length;
          if (this.state.memsize > 0U)
          {
            Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + this.state.memsize), (IntPtr) (void*) numPtr3, 16 /*0x10*/ - (int) this.state.memsize);
            this.state.v1 = XXHash32.PRIME32_1 * Bits.RotateLeft32(this.state.v1 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr2, 0), 13);
            this.state.v2 = XXHash32.PRIME32_1 * Bits.RotateLeft32(this.state.v2 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr2, 4), 13);
            this.state.v3 = XXHash32.PRIME32_1 * Bits.RotateLeft32(this.state.v3 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr2, 8), 13);
            this.state.v4 = XXHash32.PRIME32_1 * Bits.RotateLeft32(this.state.v4 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr2, 12), 13);
            numPtr3 += 16U /*0x10*/ - this.state.memsize;
            this.state.memsize = 0U;
          }
          if (numPtr3 <= numPtr4 - 16 /*0x10*/)
          {
            uint num1 = this.state.v1;
            uint num2 = this.state.v2;
            uint num3 = this.state.v3;
            uint num4 = this.state.v4;
            byte* numPtr5 = numPtr4 - 16 /*0x10*/;
            do
            {
              num1 = XXHash32.PRIME32_1 * Bits.RotateLeft32(num1 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr3, 0), 13);
              num2 = XXHash32.PRIME32_1 * Bits.RotateLeft32(num2 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr3, 4), 13);
              num3 = XXHash32.PRIME32_1 * Bits.RotateLeft32(num3 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr3, 8), 13);
              num4 = XXHash32.PRIME32_1 * Bits.RotateLeft32(num4 + XXHash32.PRIME32_2 * Converters.ReadBytesAsUInt32LE((IntPtr) (void*) numPtr3, 12), 13);
              numPtr3 += 16 /*0x10*/;
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
      this.hash = this.state.total_len < 16UL /*0x10*/ ? this.key + XXHash32.PRIME32_5 : Bits.RotateLeft32(this.state.v1, 1) + Bits.RotateLeft32(this.state.v2, 7) + Bits.RotateLeft32(this.state.v3, 12) + Bits.RotateLeft32(this.state.v4, 18);
      this.hash += (uint) this.state.total_len;
      byte* a_in = numPtr1;
      byte* numPtr2;
      for (numPtr2 = a_in + this.state.memsize; a_in + 4 <= numPtr2; a_in += 4)
      {
        this.hash += Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0) * XXHash32.PRIME32_3;
        this.hash = Bits.RotateLeft32(this.hash, 17) * XXHash32.PRIME32_4;
      }
      for (; a_in < numPtr2; ++a_in)
      {
        this.hash += (uint) *a_in * XXHash32.PRIME32_5;
        this.hash = Bits.RotateLeft32(this.hash, 11) * XXHash32.PRIME32_1;
      }
      this.hash ^= this.hash >> 15;
      this.hash *= XXHash32.PRIME32_2;
      this.hash ^= this.hash >> 13;
      this.hash *= XXHash32.PRIME32_3;
      this.hash ^= this.hash >> 16 /*0x10*/;
    }
    HashResult hashResult = new HashResult(this.hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public int? KeyLength => new int?(4);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt32AsBytesLE(this.key);
    set
    {
      if (value.Empty())
      {
        this.key = XXHash32.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(XXHash32.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
      }
    }
  }

  private struct XXH_State
  {
    public ulong total_len;
    public uint memsize;
    public uint v1;
    public uint v2;
    public uint v3;
    public uint v4;
    public byte[] memory;

    public XXHash32.XXH_State Clone()
    {
      return new XXHash32.XXH_State()
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
