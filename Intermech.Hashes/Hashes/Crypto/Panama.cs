// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Panama
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Panama : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private uint[][] stages;
  private uint[] state;
  private uint[] theta;
  private uint[] gamma;
  private uint[] pi;
  private uint[] work_buffer;
  private int tap;

  public Panama()
    : base(32 /*0x20*/, 32 /*0x20*/)
  {
    this.tap = 0;
    this.state = new uint[17];
    this.theta = new uint[17];
    this.gamma = new uint[17];
    this.pi = new uint[17];
    this.work_buffer = new uint[17];
    Array.Resize<uint[]>(ref this.stages, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      this.stages[index] = new uint[8];
  }

  public override IHash Clone()
  {
    Panama panama = new Panama();
    panama.buffer = this.buffer.Clone();
    panama.processed_bytes = this.processed_bytes;
    panama.tap = this.tap;
    panama.state = this.state.DeepCopy();
    panama.theta = this.theta.DeepCopy();
    panama.gamma = this.gamma.DeepCopy();
    panama.pi = this.pi.DeepCopy();
    Array.Resize<uint[]>(ref this.stages, 32 /*0x20*/);
    for (uint index = 0; index < 32U /*0x20*/; ++index)
      Intermech.Hashes.Utils.Utils.Memcopy(ref panama.stages[(int) index], this.stages[(int) index], this.stages[(int) index].Length);
    panama.BufferSize = this.BufferSize;
    return (IHash) panama;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.state);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      ArrayUtils.ZeroFill(ref this.stages[index]);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[32 /*0x20*/];
    fixed (uint* src = &this.state[9])
      fixed (byte* dest = result)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override unsafe void Finish()
  {
    int a_length = 32 /*0x20*/ - (int) ((long) this.processed_bytes & 31L /*0x1F*/);
    byte[] a_data = new byte[a_length];
    a_data[0] = (byte) 1;
    this.TransformBytes(a_data, 0, a_length);
    uint[] numArray = new uint[17];
    fixed (uint* a_theta = numArray)
    {
      for (int index1 = 0; index1 < 32 /*0x20*/; ++index1)
      {
        int index2 = this.tap + 4 & 31 /*0x1F*/;
        int index3 = this.tap + 16 /*0x10*/ & 31 /*0x1F*/;
        this.tap = this.tap - 1 & 31 /*0x1F*/;
        int index4 = this.tap + 25 & 31 /*0x1F*/;
        this.GPT(a_theta);
        this.stages[index4][0] = this.stages[index4][0] ^ this.stages[this.tap][2];
        this.stages[index4][1] = this.stages[index4][1] ^ this.stages[this.tap][3];
        this.stages[index4][2] = this.stages[index4][2] ^ this.stages[this.tap][4];
        this.stages[index4][3] = this.stages[index4][3] ^ this.stages[this.tap][5];
        this.stages[index4][4] = this.stages[index4][4] ^ this.stages[this.tap][6];
        this.stages[index4][5] = this.stages[index4][5] ^ this.stages[this.tap][7];
        this.stages[index4][6] = this.stages[index4][6] ^ this.stages[this.tap][0];
        this.stages[index4][7] = this.stages[index4][7] ^ this.stages[this.tap][1];
        this.stages[this.tap][0] = this.stages[this.tap][0] ^ this.state[1];
        this.stages[this.tap][1] = this.stages[this.tap][1] ^ this.state[2];
        this.stages[this.tap][2] = this.stages[this.tap][2] ^ this.state[3];
        this.stages[this.tap][3] = this.stages[this.tap][3] ^ this.state[4];
        this.stages[this.tap][4] = this.stages[this.tap][4] ^ this.state[5];
        this.stages[this.tap][5] = this.stages[this.tap][5] ^ this.state[6];
        this.stages[this.tap][6] = this.stages[this.tap][6] ^ this.state[7];
        this.stages[this.tap][7] = this.stages[this.tap][7] ^ this.state[8];
        this.state[0] = numArray[0] ^ 1U;
        this.state[1] = numArray[1] ^ this.stages[index2][0];
        this.state[2] = numArray[2] ^ this.stages[index2][1];
        this.state[3] = numArray[3] ^ this.stages[index2][2];
        this.state[4] = numArray[4] ^ this.stages[index2][3];
        this.state[5] = numArray[5] ^ this.stages[index2][4];
        this.state[6] = numArray[6] ^ this.stages[index2][5];
        this.state[7] = numArray[7] ^ this.stages[index2][6];
        this.state[8] = numArray[8] ^ this.stages[index2][7];
        this.state[9] = numArray[9] ^ this.stages[index3][0];
        this.state[10] = numArray[10] ^ this.stages[index3][1];
        this.state[11] = numArray[11] ^ this.stages[index3][2];
        this.state[12] = numArray[12] ^ this.stages[index3][3];
        this.state[13] = numArray[13] ^ this.stages[index3][4];
        this.state[14] = numArray[14] ^ this.stages[index3][5];
        this.state[15] = numArray[15] ^ this.stages[index3][6];
        this.state[16 /*0x10*/] = numArray[16 /*0x10*/] ^ this.stages[index3][7];
      }
    }
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    fixed (uint* a_theta = this.theta)
      fixed (uint* dest = this.work_buffer)
      {
        Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 32 /*0x20*/);
        uint index1 = (uint) (this.tap + 16 /*0x10*/ & 31 /*0x1F*/);
        this.tap = this.tap - 1 & 31 /*0x1F*/;
        uint index2 = (uint) (this.tap + 25 & 31 /*0x1F*/);
        this.GPT(a_theta);
        this.stages[(int) index2][0] = this.stages[(int) index2][0] ^ this.stages[this.tap][2];
        this.stages[(int) index2][1] = this.stages[(int) index2][1] ^ this.stages[this.tap][3];
        this.stages[(int) index2][2] = this.stages[(int) index2][2] ^ this.stages[this.tap][4];
        this.stages[(int) index2][3] = this.stages[(int) index2][3] ^ this.stages[this.tap][5];
        this.stages[(int) index2][4] = this.stages[(int) index2][4] ^ this.stages[this.tap][6];
        this.stages[(int) index2][5] = this.stages[(int) index2][5] ^ this.stages[this.tap][7];
        this.stages[(int) index2][6] = this.stages[(int) index2][6] ^ this.stages[this.tap][0];
        this.stages[(int) index2][7] = this.stages[(int) index2][7] ^ this.stages[this.tap][1];
        this.stages[this.tap][0] = this.stages[this.tap][0] ^ this.work_buffer[0];
        this.stages[this.tap][1] = this.stages[this.tap][1] ^ this.work_buffer[1];
        this.stages[this.tap][2] = this.stages[this.tap][2] ^ this.work_buffer[2];
        this.stages[this.tap][3] = this.stages[this.tap][3] ^ this.work_buffer[3];
        this.stages[this.tap][4] = this.stages[this.tap][4] ^ this.work_buffer[4];
        this.stages[this.tap][5] = this.stages[this.tap][5] ^ this.work_buffer[5];
        this.stages[this.tap][6] = this.stages[this.tap][6] ^ this.work_buffer[6];
        this.stages[this.tap][7] = this.stages[this.tap][7] ^ this.work_buffer[7];
        this.state[0] = this.theta[0] ^ 1U;
        this.state[1] = this.theta[1] ^ this.work_buffer[0];
        this.state[2] = this.theta[2] ^ this.work_buffer[1];
        this.state[3] = this.theta[3] ^ this.work_buffer[2];
        this.state[4] = this.theta[4] ^ this.work_buffer[3];
        this.state[5] = this.theta[5] ^ this.work_buffer[4];
        this.state[6] = this.theta[6] ^ this.work_buffer[5];
        this.state[7] = this.theta[7] ^ this.work_buffer[6];
        this.state[8] = this.theta[8] ^ this.work_buffer[7];
        this.state[9] = this.theta[9] ^ this.stages[(int) index1][0];
        this.state[10] = this.theta[10] ^ this.stages[(int) index1][1];
        this.state[11] = this.theta[11] ^ this.stages[(int) index1][2];
        this.state[12] = this.theta[12] ^ this.stages[(int) index1][3];
        this.state[13] = this.theta[13] ^ this.stages[(int) index1][4];
        this.state[14] = this.theta[14] ^ this.stages[(int) index1][5];
        this.state[15] = this.theta[15] ^ this.stages[(int) index1][6];
        this.state[16 /*0x10*/] = this.theta[16 /*0x10*/] ^ this.stages[(int) index1][7];
        Intermech.Hashes.Utils.Utils.Memset(ref this.work_buffer, (byte) 0);
      }
  }

  private unsafe void GPT(uint* a_theta)
  {
    this.gamma[0] = this.state[0] ^ (this.state[1] | ~this.state[2]);
    this.gamma[1] = this.state[1] ^ (this.state[2] | ~this.state[3]);
    this.gamma[2] = this.state[2] ^ (this.state[3] | ~this.state[4]);
    this.gamma[3] = this.state[3] ^ (this.state[4] | ~this.state[5]);
    this.gamma[4] = this.state[4] ^ (this.state[5] | ~this.state[6]);
    this.gamma[5] = this.state[5] ^ (this.state[6] | ~this.state[7]);
    this.gamma[6] = this.state[6] ^ (this.state[7] | ~this.state[8]);
    this.gamma[7] = this.state[7] ^ (this.state[8] | ~this.state[9]);
    this.gamma[8] = this.state[8] ^ (this.state[9] | ~this.state[10]);
    this.gamma[9] = this.state[9] ^ (this.state[10] | ~this.state[11]);
    this.gamma[10] = this.state[10] ^ (this.state[11] | ~this.state[12]);
    this.gamma[11] = this.state[11] ^ (this.state[12] | ~this.state[13]);
    this.gamma[12] = this.state[12] ^ (this.state[13] | ~this.state[14]);
    this.gamma[13] = this.state[13] ^ (this.state[14] | ~this.state[15]);
    this.gamma[14] = this.state[14] ^ (this.state[15] | ~this.state[16 /*0x10*/]);
    this.gamma[15] = this.state[15] ^ (this.state[16 /*0x10*/] | ~this.state[0]);
    this.gamma[16 /*0x10*/] = this.state[16 /*0x10*/] ^ (this.state[0] | ~this.state[1]);
    this.pi[0] = this.gamma[0];
    this.pi[1] = Bits.RotateLeft32(this.gamma[7], 1);
    this.pi[2] = Bits.RotateLeft32(this.gamma[14], 3);
    this.pi[3] = Bits.RotateLeft32(this.gamma[4], 6);
    this.pi[4] = Bits.RotateLeft32(this.gamma[11], 10);
    this.pi[5] = Bits.RotateLeft32(this.gamma[1], 15);
    this.pi[6] = Bits.RotateLeft32(this.gamma[8], 21);
    this.pi[7] = Bits.RotateLeft32(this.gamma[15], 28);
    this.pi[8] = Bits.RotateLeft32(this.gamma[5], 4);
    this.pi[9] = Bits.RotateLeft32(this.gamma[12], 13);
    this.pi[10] = Bits.RotateLeft32(this.gamma[2], 23);
    this.pi[11] = Bits.RotateLeft32(this.gamma[9], 2);
    this.pi[12] = Bits.RotateLeft32(this.gamma[16 /*0x10*/], 14);
    this.pi[13] = Bits.RotateLeft32(this.gamma[6], 27);
    this.pi[14] = Bits.RotateLeft32(this.gamma[13], 9);
    this.pi[15] = Bits.RotateLeft32(this.gamma[3], 24);
    this.pi[16 /*0x10*/] = Bits.RotateLeft32(this.gamma[10], 8);
    *a_theta = this.pi[0] ^ this.pi[1] ^ this.pi[4];
    a_theta[1] = this.pi[1] ^ this.pi[2] ^ this.pi[5];
    a_theta[2] = this.pi[2] ^ this.pi[3] ^ this.pi[6];
    a_theta[3] = this.pi[3] ^ this.pi[4] ^ this.pi[7];
    a_theta[4] = this.pi[4] ^ this.pi[5] ^ this.pi[8];
    a_theta[5] = this.pi[5] ^ this.pi[6] ^ this.pi[9];
    a_theta[6] = this.pi[6] ^ this.pi[7] ^ this.pi[10];
    a_theta[7] = this.pi[7] ^ this.pi[8] ^ this.pi[11];
    a_theta[8] = this.pi[8] ^ this.pi[9] ^ this.pi[12];
    a_theta[9] = this.pi[9] ^ this.pi[10] ^ this.pi[13];
    a_theta[10] = this.pi[10] ^ this.pi[11] ^ this.pi[14];
    a_theta[11] = this.pi[11] ^ this.pi[12] ^ this.pi[15];
    a_theta[12] = this.pi[12] ^ this.pi[13] ^ this.pi[16 /*0x10*/];
    a_theta[13] = this.pi[13] ^ this.pi[14] ^ this.pi[0];
    a_theta[14] = this.pi[14] ^ this.pi[15] ^ this.pi[1];
    a_theta[15] = this.pi[15] ^ this.pi[16 /*0x10*/] ^ this.pi[2];
    a_theta[16 /*0x10*/] = this.pi[16 /*0x10*/] ^ this.pi[0] ^ this.pi[3];
  }
}
