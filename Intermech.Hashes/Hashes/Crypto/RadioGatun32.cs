// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.RadioGatun32
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class RadioGatun32 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private uint[] mill;
  private uint[][] belt;

  public RadioGatun32()
    : base(32 /*0x20*/, 12)
  {
    this.mill = new uint[19];
    Array.Resize<uint[]>(ref this.belt, 13);
    for (int index = 0; index < 13; ++index)
      this.belt[index] = new uint[3];
  }

  public override IHash Clone()
  {
    RadioGatun32 radioGatun32 = new RadioGatun32();
    radioGatun32.buffer = this.buffer.Clone();
    radioGatun32.processed_bytes = this.processed_bytes;
    radioGatun32.mill = this.mill.DeepCopy();
    Array.Resize<uint[]>(ref this.belt, 13);
    for (int index = 0; index < 13; ++index)
      Intermech.Hashes.Utils.Utils.Memcopy(ref radioGatun32.belt[index], this.belt[index], this.belt[index].Length);
    radioGatun32.BufferSize = this.BufferSize;
    return (IHash) radioGatun32;
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
    uint[] numArray1 = new uint[8];
    byte[] result = new byte[32 /*0x20*/];
    uint[] numArray2 = numArray1;
    uint* src = numArray1 == null || numArray2.Length == 0 ? (uint*) null : &numArray2[0];
    fixed (uint* numPtr = this.mill)
    {
      fixed (byte* dest = result)
      {
        for (int index = 0; index < 4; ++index)
        {
          this.RoundFunction();
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (src + index * 2), (IntPtr) (void*) (numPtr + 1), 8);
        }
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
      }
      numArray2 = (uint[]) null;
    }
    return result;
  }

  protected override void Finish()
  {
    int a_length = 12 - (int) (this.processed_bytes % 12UL);
    byte[] a_data = new byte[a_length];
    a_data[0] = (byte) 1;
    this.TransformBytes(a_data, 0, a_length);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      this.RoundFunction();
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] buffer = new uint[3];
    fixed (uint* dest = buffer)
    {
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 12);
      for (int index = 0; index < 3; ++index)
      {
        this.mill[index + 16 /*0x10*/] = this.mill[index + 16 /*0x10*/] ^ buffer[index];
        this.belt[0][index] = this.belt[0][index] ^ buffer[index];
      }
      this.RoundFunction();
      ArrayUtils.ZeroFill(ref buffer);
    }
  }

  private void RoundFunction()
  {
    uint[] numArray1 = new uint[19];
    uint[] numArray2 = this.belt[12];
    for (int index = 12; index > 0; --index)
      this.belt[index] = this.belt[index - 1];
    this.belt[0] = numArray2;
    for (int index = 0; index < 12; ++index)
      this.belt[index + 1][index % 3] = this.belt[index + 1][index % 3] ^ this.mill[index + 1];
    for (int index = 0; index < 19; ++index)
      numArray1[index] = this.mill[index] ^ (this.mill[(index + 1) % 19] | ~this.mill[(index + 2) % 19]);
    for (int index = 0; index < 19; ++index)
      this.mill[index] = Bits.RotateRight32(numArray1[7 * index % 19], index * (index + 1) >> 1);
    for (int index = 0; index < 19; ++index)
      numArray1[index] = this.mill[index] ^ this.mill[(index + 1) % 19] ^ this.mill[(index + 4) % 19];
    numArray1[0] = numArray1[0] ^ 1U;
    for (int index = 0; index < 19; ++index)
      this.mill[index] = numArray1[index];
    for (int index = 0; index < 3; ++index)
      this.mill[index + 13] = this.mill[index + 13] ^ numArray2[index];
  }
}
