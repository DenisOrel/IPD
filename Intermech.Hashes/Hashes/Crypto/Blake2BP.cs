// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2BP
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

internal sealed class Blake2BP : Hash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private Blake2B[] LeafHashes;
  private byte[] Buffer;
  private byte[] Key;
  private static readonly int BlockSizeInBytes = 128 /*0x80*/;
  private static readonly int OutSizeInBytes = 64 /*0x40*/;
  private static readonly int ParallelismDegree = 4;

  private Blake2B RootHash { get; set; }

  private ulong BufferLength { get; set; }

  public Blake2BP(int a_HashSize, byte[] a_Key)
    : base(a_HashSize, Blake2BP.BlockSizeInBytes)
  {
    this.Buffer = new byte[Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes];
    this.LeafHashes = new Blake2B[Blake2BP.ParallelismDegree];
    this.Key = a_Key.DeepCopy();
    this.RootHash = this.Blake2BPCreateRoot();
    for (int a_Offset = 0; a_Offset < Blake2BP.ParallelismDegree; ++a_Offset)
      this.LeafHashes[a_Offset] = this.Blake2BPCreateLeaf((ulong) a_Offset);
  }

  ~Blake2BP() => this.Clear();

  public override IHash Clone()
  {
    Blake2BP blake2Bp = new Blake2BP(this.HashSize);
    blake2Bp.Key = this.Key.DeepCopy();
    blake2Bp.RootHash = (Blake2B) this.RootHash?.Clone();
    if (this.LeafHashes != null)
    {
      blake2Bp.LeafHashes = new Blake2B[this.LeafHashes.Length];
      for (int index = 0; index < this.LeafHashes.Length; ++index)
        blake2Bp.LeafHashes[index] = (Blake2B) this.LeafHashes[index].Clone();
    }
    blake2Bp.Buffer = this.Buffer.DeepCopy();
    blake2Bp.BufferLength = this.BufferLength;
    blake2Bp.BufferSize = this.BufferSize;
    return (IHash) blake2Bp;
  }

  public override void Initialize()
  {
    this.RootHash.Initialize();
    for (int index = 0; index < Blake2BP.ParallelismDegree; ++index)
    {
      this.LeafHashes[index].Initialize();
      this.LeafHashes[index].HashSize = Blake2BP.OutSizeInBytes;
    }
    ArrayUtils.ZeroFill(ref this.Buffer);
    this.BufferLength = 0UL;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    Blake2BP.DataContainer a_DataContainer = new Blake2BP.DataContainer();
    if (a_data.Empty())
      return;
    ulong num1 = (ulong) a_length;
    fixed (byte* numPtr1 = a_data)
      fixed (byte* numPtr2 = this.Buffer)
      {
        byte* src1 = numPtr1 + a_index;
        ulong num2 = this.BufferLength;
        ulong n1 = (ulong) this.Buffer.Length - num2;
        if (num2 > 0UL && num1 >= n1)
        {
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + num2), (IntPtr) (void*) src1, (int) n1);
          for (int index = 0; index < Blake2BP.ParallelismDegree; ++index)
            this.LeafHashes[index].TransformBytes(this.Buffer, index * Blake2BP.BlockSizeInBytes, Blake2BP.BlockSizeInBytes);
          src1 += n1;
          num1 -= n1;
          num2 = 0UL;
        }
        try
        {
          a_DataContainer.PtrData = (IntPtr) (void*) src1;
          a_DataContainer.Counter = num1;
          this.DoParallelComputation(ref a_DataContainer);
        }
        catch (Exception ex)
        {
        }
        byte* src2 = src1 + (num1 - num1 % (ulong) (Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes));
        ulong n2 = num1 % (ulong) (Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes);
        if (n2 > 0UL)
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + num2), (IntPtr) (void*) src2, (int) n2);
        this.BufferLength = (ulong) ((uint) num2 + (uint) n2);
      }
  }

  public override IHashResult TransformFinal()
  {
    byte[][] numArray = new byte[Blake2BP.ParallelismDegree][];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = new byte[Blake2BP.OutSizeInBytes];
    for (int index = 0; index < Blake2BP.ParallelismDegree; ++index)
    {
      if (this.BufferLength > (ulong) (index * Blake2BP.BlockSizeInBytes))
      {
        ulong a_length = this.BufferLength - (ulong) (index * Blake2BP.BlockSizeInBytes);
        if (a_length > (ulong) Blake2BP.BlockSizeInBytes)
          a_length = (ulong) Blake2BP.BlockSizeInBytes;
        this.LeafHashes[index].TransformBytes(this.Buffer, index * Blake2BP.BlockSizeInBytes, (int) a_length);
      }
      numArray[index] = this.LeafHashes[index].TransformFinal().GetBytes();
    }
    for (int index = 0; index < Blake2BP.ParallelismDegree; ++index)
      this.RootHash.TransformBytes(numArray[index], 0, Blake2BP.OutSizeInBytes);
    IHashResult hashResult = this.RootHash.TransformFinal();
    this.Initialize();
    return hashResult;
  }

  public override string Name => $"{this.GetType().Name}_{this.HashSize * 8}";

  private Blake2BP(int a_HashSize)
    : base(a_HashSize, Blake2BP.BlockSizeInBytes)
  {
  }

  private Blake2B Blake2BPCreateLeafParam(
    IBlake2BConfig a_Blake2BConfig,
    IBlake2BTreeConfig a_Blake2BTreeConfig)
  {
    return new Blake2B(a_Blake2BConfig, a_Blake2BTreeConfig);
  }

  private Blake2B Blake2BPCreateLeaf(ulong a_Offset)
  {
    IBlake2BConfig a_Blake2BConfig = (IBlake2BConfig) new Blake2BConfig(this.HashSize);
    a_Blake2BConfig.Key = this.Key.DeepCopy();
    IBlake2BTreeConfig a_Blake2BTreeConfig = (IBlake2BTreeConfig) new Blake2BTreeConfig();
    a_Blake2BTreeConfig.FanOut = (byte) Blake2BP.ParallelismDegree;
    a_Blake2BTreeConfig.MaxDepth = (byte) 2;
    a_Blake2BTreeConfig.NodeDepth = (byte) 0;
    a_Blake2BTreeConfig.LeafSize = 0U;
    a_Blake2BTreeConfig.NodeOffset = a_Offset;
    a_Blake2BTreeConfig.InnerHashSize = (byte) Blake2BP.OutSizeInBytes;
    if ((long) a_Offset == (long) (Blake2BP.ParallelismDegree - 1))
      a_Blake2BTreeConfig.IsLastNode = true;
    return this.Blake2BPCreateLeafParam(a_Blake2BConfig, a_Blake2BTreeConfig);
  }

  private Blake2B Blake2BPCreateRoot()
  {
    Blake2BConfig a_Config = new Blake2BConfig(this.HashSize);
    a_Config.Key = this.Key.DeepCopy();
    IBlake2BTreeConfig a_TreeConfig = (IBlake2BTreeConfig) new Blake2BTreeConfig();
    a_TreeConfig.FanOut = (byte) Blake2BP.ParallelismDegree;
    a_TreeConfig.MaxDepth = (byte) 2;
    a_TreeConfig.NodeDepth = (byte) 1;
    a_TreeConfig.LeafSize = 0U;
    a_TreeConfig.NodeOffset = 0UL;
    a_TreeConfig.InnerHashSize = (byte) Blake2BP.OutSizeInBytes;
    a_TreeConfig.IsLastNode = true;
    return new Blake2B((IBlake2BConfig) a_Config, a_TreeConfig, false);
  }

  private unsafe void ParallelComputation(int Idx, ref Blake2BP.DataContainer a_DataContainer)
  {
    byte[] a_data = new byte[Blake2BP.BlockSizeInBytes];
    byte* ptrData = (byte*) (void*) a_DataContainer.PtrData;
    ulong counter = a_DataContainer.Counter;
    byte* src = ptrData + Idx * Blake2BP.BlockSizeInBytes;
    while (counter >= (ulong) (Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes))
    {
      fixed (byte* dest = a_data)
      {
        Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, Blake2BP.BlockSizeInBytes);
        this.LeafHashes[Idx].TransformBytes(a_data, 0, Blake2BP.BlockSizeInBytes);
        src += (ulong) (Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes);
        counter -= (ulong) (Blake2BP.ParallelismDegree * Blake2BP.BlockSizeInBytes);
      }
    }
  }

  private void DoParallelComputation(ref Blake2BP.DataContainer a_DataContainer)
  {
    for (int Idx = 0; Idx < Blake2BP.ParallelismDegree; ++Idx)
      this.ParallelComputation(Idx, ref a_DataContainer);
  }

  private void Clear() => ArrayUtils.ZeroFill(ref this.Key);

  private struct DataContainer
  {
    public IntPtr PtrData;
    public ulong Counter;
  }
}
