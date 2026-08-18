// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2SP
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Blake2SP : Hash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private Blake2S[] LeafHashes;
  private byte[] Buffer;
  private byte[] Key;
  private static readonly int BlockSizeInBytes = 64 /*0x40*/;
  private static readonly int OutSizeInBytes = 32 /*0x20*/;
  private static readonly int ParallelismDegree = 8;

  private Blake2S RootHash { get; set; }

  private ulong BufferLength { get; set; }

  public Blake2SP(int a_HashSize, byte[] a_Key)
    : base(a_HashSize, Blake2SP.BlockSizeInBytes)
  {
    this.Buffer = new byte[Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes];
    this.LeafHashes = new Blake2S[Blake2SP.ParallelismDegree];
    this.Key = a_Key.DeepCopy();
    this.RootHash = this.Blake2SPCreateRoot();
    for (int a_Offset = 0; a_Offset < Blake2SP.ParallelismDegree; ++a_Offset)
      this.LeafHashes[a_Offset] = this.Blake2SPCreateLeaf((ulong) a_Offset);
  }

  ~Blake2SP() => this.Clear();

  public override IHash Clone()
  {
    Blake2SP blake2Sp = new Blake2SP(this.HashSize);
    blake2Sp.Key = this.Key.DeepCopy();
    blake2Sp.RootHash = (Blake2S) this.RootHash?.Clone();
    if (this.LeafHashes != null)
    {
      blake2Sp.LeafHashes = new Blake2S[this.LeafHashes.Length];
      for (int index = 0; index < this.LeafHashes.Length; ++index)
        blake2Sp.LeafHashes[index] = (Blake2S) this.LeafHashes[index].Clone();
    }
    blake2Sp.Buffer = this.Buffer.DeepCopy();
    blake2Sp.BufferLength = this.BufferLength;
    blake2Sp.BufferSize = this.BufferSize;
    return (IHash) blake2Sp;
  }

  public override void Initialize()
  {
    this.RootHash.Initialize();
    for (int index = 0; index < Blake2SP.ParallelismDegree; ++index)
    {
      this.LeafHashes[index].Initialize();
      this.LeafHashes[index].HashSize = Blake2SP.OutSizeInBytes;
    }
    ArrayUtils.ZeroFill(ref this.Buffer);
    this.BufferLength = 0UL;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    Blake2SP.DataContainer a_DataContainer = new Blake2SP.DataContainer();
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
          for (int index = 0; index < Blake2SP.ParallelismDegree; ++index)
            this.LeafHashes[index].TransformBytes(this.Buffer, index * Blake2SP.BlockSizeInBytes, Blake2SP.BlockSizeInBytes);
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
        byte* src2 = src1 + (num1 - num1 % (ulong) (Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes));
        ulong n2 = num1 % (ulong) (Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes);
        if (n2 > 0UL)
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (numPtr2 + num2), (IntPtr) (void*) src2, (int) n2);
        this.BufferLength = (ulong) ((uint) num2 + (uint) n2);
      }
  }

  public override IHashResult TransformFinal()
  {
    byte[][] numArray = new byte[Blake2SP.ParallelismDegree][];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = new byte[Blake2SP.OutSizeInBytes];
    for (int index = 0; index < Blake2SP.ParallelismDegree; ++index)
    {
      if (this.BufferLength > (ulong) (index * Blake2SP.BlockSizeInBytes))
      {
        ulong a_length = this.BufferLength - (ulong) (index * Blake2SP.BlockSizeInBytes);
        if (a_length > (ulong) Blake2SP.BlockSizeInBytes)
          a_length = (ulong) Blake2SP.BlockSizeInBytes;
        this.LeafHashes[index].TransformBytes(this.Buffer, index * Blake2SP.BlockSizeInBytes, (int) a_length);
      }
      numArray[index] = this.LeafHashes[index].TransformFinal().GetBytes();
    }
    for (int index = 0; index < Blake2SP.ParallelismDegree; ++index)
      this.RootHash.TransformBytes(numArray[index], 0, Blake2SP.OutSizeInBytes);
    IHashResult hashResult = this.RootHash.TransformFinal();
    this.Initialize();
    return hashResult;
  }

  public override string Name => $"{this.GetType().Name}_{this.HashSize * 8}";

  private Blake2SP(int a_HashSize)
    : base(a_HashSize, Blake2SP.BlockSizeInBytes)
  {
  }

  private Blake2S Blake2SPCreateLeafParam(
    IBlake2SConfig a_Blake2SConfig,
    IBlake2STreeConfig a_Blake2STreeConfig)
  {
    return new Blake2S(a_Blake2SConfig, a_Blake2STreeConfig);
  }

  private Blake2S Blake2SPCreateLeaf(ulong a_Offset)
  {
    IBlake2SConfig a_Blake2SConfig = (IBlake2SConfig) new Blake2SConfig(this.HashSize);
    a_Blake2SConfig.Key = this.Key.DeepCopy();
    IBlake2STreeConfig a_Blake2STreeConfig = (IBlake2STreeConfig) new Blake2STreeConfig();
    a_Blake2STreeConfig.FanOut = (byte) Blake2SP.ParallelismDegree;
    a_Blake2STreeConfig.MaxDepth = (byte) 2;
    a_Blake2STreeConfig.NodeDepth = (byte) 0;
    a_Blake2STreeConfig.LeafSize = 0U;
    a_Blake2STreeConfig.NodeOffset = a_Offset;
    a_Blake2STreeConfig.InnerHashSize = (byte) Blake2SP.OutSizeInBytes;
    if ((long) a_Offset == (long) (Blake2SP.ParallelismDegree - 1))
      a_Blake2STreeConfig.IsLastNode = true;
    return this.Blake2SPCreateLeafParam(a_Blake2SConfig, a_Blake2STreeConfig);
  }

  private Blake2S Blake2SPCreateRoot()
  {
    Blake2SConfig a_Config = new Blake2SConfig(this.HashSize);
    a_Config.Key = this.Key.DeepCopy();
    IBlake2STreeConfig a_TreeConfig = (IBlake2STreeConfig) new Blake2STreeConfig();
    a_TreeConfig.FanOut = (byte) Blake2SP.ParallelismDegree;
    a_TreeConfig.MaxDepth = (byte) 2;
    a_TreeConfig.NodeDepth = (byte) 1;
    a_TreeConfig.LeafSize = 0U;
    a_TreeConfig.NodeOffset = 0UL;
    a_TreeConfig.InnerHashSize = (byte) Blake2SP.OutSizeInBytes;
    a_TreeConfig.IsLastNode = true;
    return new Blake2S((IBlake2SConfig) a_Config, a_TreeConfig, false);
  }

  private unsafe void ParallelComputation(int Idx, ref Blake2SP.DataContainer a_DataContainer)
  {
    byte[] a_data = new byte[Blake2SP.BlockSizeInBytes];
    byte* ptrData = (byte*) (void*) a_DataContainer.PtrData;
    ulong counter = a_DataContainer.Counter;
    byte* src = ptrData + Idx * Blake2SP.BlockSizeInBytes;
    while (counter >= (ulong) (Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes))
    {
      fixed (byte* dest = a_data)
      {
        Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, Blake2SP.BlockSizeInBytes);
        this.LeafHashes[Idx].TransformBytes(a_data, 0, Blake2SP.BlockSizeInBytes);
        src += (ulong) (Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes);
        counter -= (ulong) (Blake2SP.ParallelismDegree * Blake2SP.BlockSizeInBytes);
      }
    }
  }

  private void DoParallelComputation(ref Blake2SP.DataContainer a_DataContainer)
  {
    for (int Idx = 0; Idx < Blake2SP.ParallelismDegree; ++Idx)
      this.ParallelComputation(Idx, ref a_DataContainer);
  }

  private void Clear() => ArrayUtils.ZeroFill(ref this.Key);

  private struct DataContainer
  {
    public IntPtr PtrData;
    public ulong Counter;
  }
}
