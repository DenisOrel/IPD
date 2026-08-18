// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2XB
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Crypto.Blake2BConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Blake2XB : Blake2B, IXOF, IHash
{
  private IBlake2XBConfig Blake2XBConfig;
  private ulong DigestPosition;
  private IBlake2XBConfig RootConfig;
  private IBlake2XBConfig OutputConfig;
  private byte[] RootHashDigest;
  private byte[] Blake2XBBuffer;
  private bool Finalized;
  private ulong xofSizeInBits;
  private static readonly int Blake2BHashSize = 64 /*0x40*/;
  private static readonly ulong UnknownDigestLengthInBytes = (ulong) uint.MaxValue;
  private static readonly ulong MaxNumberBlocks = 4294967296 /*0x0100000000*/;
  private static readonly ulong UnknownMaxDigestLengthInBytes = Blake2XB.MaxNumberBlocks * (ulong) Blake2XB.Blake2BHashSize;

  public ulong XOFSizeInBits
  {
    get => this.xofSizeInBits;
    set => this.SetXOFSizeInBitsInternal(value);
  }

  public override string Name => this.GetType().Name;

  public unsafe void DoOutput(
    ref byte[] a_destination,
    ulong a_destinationOffset,
    ulong a_outputLength)
  {
    if ((ulong) a_destination.Length - a_destinationOffset < a_outputLength)
      throw new ArgumentOutOfRangeHashLibException(Blake2B.OutputBufferTooShort);
    if ((long) (this.XOFSizeInBits >> 3) != (long) Blake2XB.UnknownDigestLengthInBytes)
    {
      if (this.DigestPosition + a_outputLength > this.XOFSizeInBits >> 3)
        throw new ArgumentOutOfRangeHashLibException(Blake2B.OutputLengthInvalid);
    }
    else if (this.DigestPosition << 5 >= Blake2XB.UnknownMaxDigestLengthInBytes)
      throw new ArgumentOutOfRangeHashLibException(Blake2B.MaximumOutputLengthExceeded);
    if (!this.Finalized)
    {
      this.Finish();
      this.Finalized = true;
    }
    if (this.RootHashDigest.Empty())
    {
      this.RootHashDigest = new byte[Blake2XB.Blake2BHashSize];
      fixed (ulong* src = this.State)
        fixed (byte* dest = this.RootHashDigest)
          Converters.le64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, this.RootHashDigest.Length);
    }
    while (a_outputLength > 0UL)
    {
      if (((long) this.DigestPosition & (long) (Blake2XB.Blake2BHashSize - 1)) == 0L)
      {
        this.OutputConfig.Blake2BConfig.HashSize = this.ComputeStepLength();
        this.OutputConfig.Blake2BTreeConfig.InnerHashSize = (byte) Blake2XB.Blake2BHashSize;
        this.Blake2XBBuffer = new Blake2B(this.OutputConfig.Blake2BConfig, this.OutputConfig.Blake2BTreeConfig).ComputeBytes(this.RootHashDigest).GetBytes();
        ++this.OutputConfig.Blake2BTreeConfig.NodeOffset;
      }
      ulong indexSrc = this.DigestPosition & (ulong) (Blake2XB.Blake2BHashSize - 1);
      ulong val2 = (ulong) this.Blake2XBBuffer.Length - indexSrc;
      ulong n = Math.Min(a_outputLength, val2);
      Intermech.Hashes.Utils.Utils.Memmove(ref a_destination, this.Blake2XBBuffer, (int) n, (int) indexSrc, (int) a_destinationOffset);
      a_outputLength -= n;
      a_destinationOffset += n;
      this.DigestPosition += n;
    }
  }

  public Blake2XB(IBlake2XBConfig a_Blake2XBConfig)
  {
    this.Blake2XBConfig = a_Blake2XBConfig;
    this.RootConfig = (IBlake2XBConfig) new Intermech.Hashes.Crypto.Blake2BConfigurations.Blake2XBConfig();
    this.RootConfig.Blake2BConfig = this.Blake2XBConfig.Blake2BConfig;
    if (this.RootConfig.Blake2BConfig == null)
    {
      this.RootConfig.Blake2BConfig = (IBlake2BConfig) new Blake2BConfig();
    }
    else
    {
      this.RootConfig.Blake2BConfig.Key = this.Blake2XBConfig.Blake2BConfig.Key;
      this.RootConfig.Blake2BConfig.Salt = this.Blake2XBConfig.Blake2BConfig.Salt;
      this.RootConfig.Blake2BConfig.Personalisation = this.Blake2XBConfig.Blake2BConfig.Personalisation;
    }
    this.RootConfig.Blake2BTreeConfig = this.Blake2XBConfig.Blake2BTreeConfig;
    if (this.RootConfig.Blake2BTreeConfig == null)
    {
      this.RootConfig.Blake2BTreeConfig = (IBlake2BTreeConfig) new Blake2BTreeConfig();
      this.RootConfig.Blake2BTreeConfig.FanOut = (byte) 1;
      this.RootConfig.Blake2BTreeConfig.MaxDepth = (byte) 1;
      this.RootConfig.Blake2BTreeConfig.LeafSize = 0U;
      this.RootConfig.Blake2BTreeConfig.NodeOffset = 0UL;
      this.RootConfig.Blake2BTreeConfig.NodeDepth = (byte) 0;
      this.RootConfig.Blake2BTreeConfig.InnerHashSize = (byte) 0;
      this.RootConfig.Blake2BTreeConfig.IsLastNode = false;
    }
    this.OutputConfig = (IBlake2XBConfig) new Intermech.Hashes.Crypto.Blake2BConfigurations.Blake2XBConfig();
    this.OutputConfig.Blake2BConfig = (IBlake2BConfig) new Blake2BConfig();
    this.OutputConfig.Blake2BConfig.Salt = this.RootConfig.Blake2BConfig.Salt;
    this.OutputConfig.Blake2BConfig.Personalisation = this.RootConfig.Blake2BConfig.Personalisation;
    this.OutputConfig.Blake2BTreeConfig = (IBlake2BTreeConfig) new Blake2BTreeConfig();
    this.Config = this.RootConfig.Blake2BConfig;
    this.TreeConfig = this.RootConfig.Blake2BTreeConfig;
    this.HashSize = this.Config.HashSize;
    this.Blake2XBBuffer = new byte[Blake2XB.Blake2BHashSize];
  }

  public override void Initialize()
  {
    ulong a_XOFSizeInBytes = this.XOFSizeInBits >> 3;
    this.RootConfig.Blake2BTreeConfig.NodeOffset = this.NodeOffsetWithXOFDigestLength(a_XOFSizeInBytes);
    this.OutputConfig.Blake2BTreeConfig.NodeOffset = this.NodeOffsetWithXOFDigestLength(a_XOFSizeInBytes);
    this.RootHashDigest = (byte[]) null;
    this.DigestPosition = 0UL;
    this.Finalized = false;
    ArrayUtils.ZeroFill(ref this.Blake2XBBuffer);
    base.Initialize();
  }

  public override IHash Clone()
  {
    Blake2XB blake2Xb1 = new Blake2XB(this.Blake2XBConfig);
    blake2Xb1.XOFSizeInBits = this.XOFSizeInBits;
    Blake2XB blake2Xb2 = blake2Xb1 as Blake2XB;
    blake2Xb2.Blake2XBConfig = this.Blake2XBConfig.Clone();
    blake2Xb2.DigestPosition = this.DigestPosition;
    blake2Xb2.RootConfig = this.RootConfig.Clone();
    blake2Xb2.OutputConfig = this.OutputConfig.Clone();
    blake2Xb2.RootHashDigest = this.RootHashDigest.DeepCopy();
    blake2Xb2.Blake2XBBuffer = this.Blake2XBBuffer.DeepCopy();
    blake2Xb2.Finalized = this.Finalized;
    blake2Xb2.M = this.M.DeepCopy();
    blake2Xb2.State = this.State.DeepCopy();
    blake2Xb2.Buffer = this.Buffer.DeepCopy();
    blake2Xb2.FilledBufferCount = this.FilledBufferCount;
    blake2Xb2.Counter0 = this.Counter0;
    blake2Xb2.Counter1 = this.Counter1;
    blake2Xb2.FinalizationFlag0 = this.FinalizationFlag0;
    blake2Xb2.FinalizationFlag1 = this.FinalizationFlag1;
    blake2Xb2.BufferSize = this.BufferSize;
    return (IHash) blake2Xb2;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (this.Finalized)
      throw new InvalidOperationHashLibException(string.Format(Blake2B.WritetoXofAfterReadError, (object) this.Name));
    base.TransformBytes(a_data, a_index, a_length);
  }

  public override IHashResult TransformFinal()
  {
    byte[] result = this.GetResult();
    this.Initialize();
    return (IHashResult) new HashResult(result);
  }

  private IXOF SetXOFSizeInBitsInternal(ulong a_XofSizeInBits)
  {
    ulong num = a_XofSizeInBits >> 3;
    if (((long) a_XofSizeInBits & 7L) != 0L || num < 1UL || num > Blake2XB.UnknownDigestLengthInBytes)
      throw new ArgumentInvalidHashLibException(string.Format(Blake2B.InvalidXOFSize, (object) 1, (object) Blake2XB.UnknownDigestLengthInBytes));
    this.xofSizeInBits = a_XofSizeInBits;
    return (IXOF) this;
  }

  private ulong NodeOffsetWithXOFDigestLength(ulong a_XOFSizeInBytes)
  {
    return a_XOFSizeInBytes << 32 /*0x20*/;
  }

  private int ComputeStepLength()
  {
    long num = (long) (this.XOFSizeInBits >> 3);
    ulong val2 = (ulong) num - this.DigestPosition;
    return num == (long) Blake2XB.UnknownDigestLengthInBytes ? Blake2XB.Blake2BHashSize : (int) Math.Min((ulong) Blake2XB.Blake2BHashSize, val2);
  }

  private byte[] GetResult()
  {
    ulong a_outputLength = this.XOFSizeInBits >> 3;
    byte[] a_destination = new byte[a_outputLength];
    this.DoOutput(ref a_destination, 0UL, a_outputLength);
    return a_destination;
  }
}
