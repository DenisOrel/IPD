// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.RadioGatun64
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class RadioGatun64 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private ulong[] mill;
  private ulong[][] belt;

  public RadioGatun64()
    : base(32 /*0x20*/, 24)
  {
    this.mill = new ulong[19];
    Array.Resize<ulong[]>(ref this.belt, 13);
    for (int index = 0; index < 13; ++index)
      this.belt[index] = new ulong[3];
  }

  public override IHash Clone()
  {
    RadioGatun64 radioGatun64 = new RadioGatun64();
    radioGatun64.buffer = this.buffer.Clone();
    radioGatun64.processed_bytes = this.processed_bytes;
    radioGatun64.mill = this.mill.DeepCopy();
    Array.Resize<ulong[]>(ref this.belt, 13);
    for (int index = 0; index < 13; ++index)
      Intermech.Hashes.Utils.Utils.Memcopy(ref radioGatun64.belt[index], this.belt[index], this.belt[index].Length);
    radioGatun64.BufferSize = this.BufferSize;
    return (IHash) radioGatun64;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.mill);
    for (int index = 0; index < 13; ++index)
      ArrayUtils.ZeroFill(ref this.belt[index]);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    ulong[] numArray1 = new ulong[4];
    byte[] result = new byte[32 /*0x20*/];
    ulong[] numArray2 = numArray1;
    ulong* src = numArray1 == null || numArray2.Length == 0 ? (ulong*) null : &numArray2[0];
    fixed (ulong* numPtr = this.mill)
    {
      fixed (byte* dest = result)
      {
        for (int index = 0; index < 2; ++index)
        {
          this.RoundFunction();
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (src + index * 2), (IntPtr) (void*) (numPtr + 1), 16 /*0x10*/);
        }
        Converters.le64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
      }
      numArray2 = (ulong[]) null;
    }
    return result;
  }

  protected override void Finish()
  {
    int a_length = 24 - (int) (this.processed_bytes % 24UL);
    byte[] a_data = new byte[a_length];
    a_data[0] = (byte) 1;
    this.TransformBytes(a_data, 0, a_length);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      this.RoundFunction();
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    ulong[] array = new ulong[3];
    fixed (ulong* dest = array)
    {
      Converters.le64_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 24);
      for (int index = 0; index < 3; ++index)
      {
        this.mill[index + 16 /*0x10*/] = this.mill[index + 16 /*0x10*/] ^ array[index];
        this.belt[0][index] = this.belt[0][index] ^ array[index];
      }
      this.RoundFunction();
      Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
    }
  }

  private void RoundFunction()
  {
    ulong[] numArray1 = new ulong[19];
    ulong[] numArray2 = this.belt[12];
    for (int index = 12; index > 0; --index)
      this.belt[index] = this.belt[index - 1];
    this.belt[0] = numArray2;
    for (int index = 0; index < 12; ++index)
      this.belt[index + 1][index % 3] = this.belt[index + 1][index % 3] ^ this.mill[index + 1];
    for (int index = 0; index < 19; ++index)
      numArray1[index] = this.mill[index] ^ (this.mill[(index + 1) % 19] | ~this.mill[(index + 2) % 19]);
    for (int index = 0; index < 19; ++index)
      this.mill[index] = Bits.RotateRight64(numArray1[7 * index % 19], index * (index + 1) >> 1);
    for (int index = 0; index < 19; ++index)
      numArray1[index] = this.mill[index] ^ this.mill[(index + 1) % 19] ^ this.mill[(index + 4) % 19];
    numArray1[0] = numArray1[0] ^ 1UL;
    for (int index = 0; index < 19; ++index)
      this.mill[index] = numArray1[index];
    for (int index = 0; index < 3; ++index)
      this.mill[index + 13] = this.mill[index + 13] ^ numArray2[index];
  }
}
