// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake3XOF
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Blake3XOF : Blake3, IXOF, IHash
{
  private bool Finalized;
  private ulong _XofSizeInBits;

  public ulong XOFSizeInBits
  {
    get => this._XofSizeInBits;
    set => this.SetXOFSizeInBitsInternal(value);
  }

  public static unsafe Blake3XOF CreateBlake3XOF(int a_HashSize, byte[] a_Key)
  {
    uint[] a_KeyWords1 = new uint[8];
    Blake3XOF blake3Xof;
    if (a_Key.Empty())
    {
      uint[] a_KeyWords2 = Blake3.IV.DeepCopy();
      blake3Xof = new Blake3XOF(a_HashSize, a_KeyWords2, 0U);
    }
    else
    {
      int length = a_Key.Length;
      if (length != 32 /*0x20*/)
        throw new ArgumentOutOfRangeHashLibException(string.Format(Blake3.InvalidKeyLength, (object) 32 /*0x20*/, (object) length));
      fixed (byte* src = a_Key)
        fixed (uint* dest = a_KeyWords1)
          Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, length);
      blake3Xof = new Blake3XOF(a_HashSize, a_KeyWords1, 16U /*0x10*/);
    }
    blake3Xof.Finalized = false;
    return blake3Xof;
  }

  public Blake3XOF(int a_HashSize, uint[] a_KeyWords, uint a_Flags)
    : base(a_HashSize, a_KeyWords, a_Flags)
  {
    this.Finalized = false;
  }

  public override string Name => this.GetType().Name;

  public override void Initialize()
  {
    this.Finalized = false;
    base.Initialize();
  }

  public override IHash Clone()
  {
    Blake3XOF blake3Xof1 = Blake3XOF.CreateBlake3XOF(this.HashSize, (byte[]) null);
    blake3Xof1.XOFSizeInBits = this.XOFSizeInBits;
    Blake3XOF blake3Xof2 = blake3Xof1 as Blake3XOF;
    blake3Xof2.Finalized = this.Finalized;
    blake3Xof2.CS = this.CS.Clone();
    blake3Xof2.OutputReader = this.OutputReader.Clone();
    for (int index = 0; index < this.Stack.Length; ++index)
      blake3Xof2.Stack[index] = this.Stack[index].DeepCopy();
    blake3Xof2.Used = this.Used;
    blake3Xof2.Flags = this.Flags;
    blake3Xof2.Key = this.Key.DeepCopy();
    blake3Xof2.BufferSize = this.BufferSize;
    return (IHash) blake3Xof2;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (this.Finalized)
      throw new InvalidOperationHashLibException(string.Format(Blake3.WritetoXofAfterReadError, (object) this.Name));
    base.TransformBytes(a_data, a_index, a_length);
  }

  public override IHashResult TransformFinal()
  {
    byte[] result = this.GetResult();
    this.Initialize();
    return (IHashResult) new HashResult(result);
  }

  public void DoOutput(ref byte[] a_destination, ulong a_destinationOffset, ulong a_outputLength)
  {
    if ((ulong) a_destination.Length - a_destinationOffset < a_outputLength)
      throw new ArgumentOutOfRangeHashLibException(Blake3.OutputBufferTooShort);
    if (this.OutputReader.Offset + a_outputLength > this.XOFSizeInBits >> 3)
      throw new ArgumentOutOfRangeHashLibException(Blake3.OutputLengthInvalid);
    if (!this.Finalized)
    {
      this.Finish();
      this.Finalized = true;
    }
    this.InternalDoOutput(ref a_destination, a_destinationOffset, a_outputLength);
  }

  private IXOF SetXOFSizeInBitsInternal(ulong a_XofSizeInBits)
  {
    ulong num = a_XofSizeInBits >> 3;
    this._XofSizeInBits = ((long) a_XofSizeInBits & 7L) == 0L && num >= 1UL ? a_XofSizeInBits : throw new ArgumentInvalidHashLibException(Blake3.InvalidXOFSize);
    return (IXOF) this;
  }

  private byte[] GetResult()
  {
    ulong a_outputLength = this.XOFSizeInBits >> 3;
    byte[] a_destination = new byte[a_outputLength];
    this.DoOutput(ref a_destination, 0UL, a_outputLength);
    return a_destination;
  }
}
