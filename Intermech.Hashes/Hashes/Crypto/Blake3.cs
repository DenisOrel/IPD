// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake3
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal class Blake3 : Hash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  public static readonly string InvalidXOFSize = "XOFSize in Bits must be Multiples of 8 and be Greater than Zero Bytes";
  public static readonly string InvalidKeyLength = "\"Key\" Length Must Not Be Greater Than {0}, \"{1}\"";
  public static readonly string MaximumOutputLengthExceeded = "Maximum Output Length is 2^64 Bytes";
  public static readonly string OutputBufferTooShort = "Output Buffer Too Short";
  public static readonly string OutputLengthInvalid = "Output Length is above the Digest Length";
  public static readonly string WritetoXofAfterReadError = "\"{0}\" Write to Xof after Read not Allowed";
  private const int ChunkSize = 1024 /*0x0400*/;
  private const int BlockSizeInBytes = 64 /*0x40*/;
  internal const int KeyLengthInBytes = 32 /*0x20*/;
  private const uint flagChunkStart = 1;
  private const uint flagChunkEnd = 2;
  private const uint flagParent = 4;
  private const uint flagRoot = 8;
  protected const uint flagKeyedHash = 16 /*0x10*/;
  private const uint flagDeriveKeyContext = 32 /*0x20*/;
  private const uint flagDeriveKeyMaterial = 64 /*0x40*/;
  private const ulong MaxDigestLengthInBytes = 18446744073709551615 /*0xFFFFFFFFFFFFFFFF*/;
  internal static readonly uint[] IV = new uint[8]
  {
    1779033703U,
    3144134277U,
    1013904242U,
    2773480762U,
    1359893119U,
    2600822924U,
    528734635U,
    1541459225U
  };
  protected Blake3.Blake3ChunkState CS;
  protected Blake3.Blake3OutputReader OutputReader;
  protected uint[] Key;
  protected uint Flags;
  protected uint[][] Stack;
  protected ulong Used;

  private Blake3.Blake3Node RootNode()
  {
    Blake3.Blake3Node blake3Node = this.CS.Node();
    uint[] a_Result = new uint[8];
    int num1 = Blake3.TrailingZeros64(this.Used);
    int num2 = Blake3.Len64(this.Used);
    for (int a_Idx = num1; a_Idx < num2; ++a_Idx)
    {
      if (this.HasSubTreeAtHeight(a_Idx))
      {
        blake3Node.ChainingValue(ref a_Result);
        blake3Node = Blake3.Blake3Node.ParentNode(this.Stack[a_Idx], a_Result, this.Key, this.Flags);
      }
    }
    blake3Node.Flags |= 8U;
    return blake3Node;
  }

  private bool HasSubTreeAtHeight(int a_Idx) => (this.Used & (ulong) (uint) (1 << a_Idx)) > 0UL;

  private void AddChunkChainingValue(uint[] a_CV)
  {
    int a_Idx;
    for (a_Idx = 0; this.HasSubTreeAtHeight(a_Idx); ++a_Idx)
      Blake3.Blake3Node.ParentNode(this.Stack[a_Idx], a_CV, this.Key, this.Flags).ChainingValue(ref a_CV);
    this.Stack[a_Idx] = a_CV.DeepCopy();
    ++this.Used;
  }

  private static int Len64(ulong a_Value)
  {
    int num = 0;
    if (a_Value >= 1UL)
    {
      a_Value >>= 32 /*0x20*/;
      num = 32 /*0x20*/;
    }
    if (a_Value >= 65536UL /*0x010000*/)
    {
      a_Value >>= 16 /*0x10*/;
      num += 16 /*0x10*/;
    }
    if (a_Value >= 256UL /*0x0100*/)
    {
      a_Value >>= 8;
      num += 8;
    }
    return num + (int) Blake3.Len8((byte) a_Value);
  }

  private static byte Len8(byte a_Value)
  {
    byte num = 0;
    while (a_Value != (byte) 0)
    {
      a_Value >>= 1;
      ++num;
    }
    return num;
  }

  private static int TrailingZeros64(ulong a_Value)
  {
    if (a_Value == 0UL)
      return 64 /*0x40*/;
    int num = 0;
    while (((long) a_Value & 1L) == 0L)
    {
      a_Value >>= 1;
      ++num;
    }
    return num;
  }

  public override string Name => $"{this.GetType().Name}_{this.HashSize * 8}";

  protected void InternalDoOutput(
    ref byte[] a_Destination,
    ulong a_DestinationOffset,
    ulong a_OutputLength)
  {
    this.OutputReader.Read(ref a_Destination, a_DestinationOffset, a_OutputLength);
  }

  protected void Finish() => this.OutputReader.N = this.RootNode();

  public static unsafe Blake3 CreateBlake3(int a_HashSize, byte[] a_Key)
  {
    uint[] a_KeyWords1 = new uint[8];
    Blake3 blake3;
    if (a_Key.Empty())
    {
      uint[] a_KeyWords2 = Blake3.IV.DeepCopy();
      blake3 = new Blake3(a_HashSize, a_KeyWords2, 0U);
    }
    else
    {
      int length = a_Key.Length;
      if (length != 32 /*0x20*/)
        throw new ArgumentOutOfRangeHashLibException(string.Format(Blake3.InvalidKeyLength, (object) 32 /*0x20*/, (object) length));
      fixed (byte* src = a_Key)
        fixed (uint* dest = a_KeyWords1)
          Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, length);
      blake3 = new Blake3(a_HashSize, a_KeyWords1, 16U /*0x10*/);
    }
    return blake3;
  }

  public Blake3(int a_HashSize, uint[] a_KeyWords, uint a_Flags)
    : base(a_HashSize, 64 /*0x40*/)
  {
    this.Key = a_KeyWords.DeepCopy();
    this.Flags = a_Flags;
    this.Stack = new uint[54][];
    for (int index = 0; index < this.Stack.Length; ++index)
      this.Stack[index] = new uint[8];
  }

  public static Blake3 CreateBlake3(HashSizeEnum a_HashSize = HashSizeEnum.HashSize256, byte[] a_Key = null)
  {
    return Blake3.CreateBlake3((int) a_HashSize, a_Key);
  }

  public override void Initialize()
  {
    this.CS = Blake3.Blake3ChunkState.CreateBlake3ChunkState(this.Key, 0UL, this.Flags);
    this.OutputReader = Blake3.Blake3OutputReader.DefaultBlake3OutputReader();
    for (int index = 0; index < this.Stack.Length; ++index)
      ArrayUtils.ZeroFill(ref this.Stack[index]);
    this.Used = 0UL;
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    uint[] a_Result = new uint[8];
    fixed (uint* numPtr1 = a_Result)
    {
      uint* numPtr2 = numPtr1;
      fixed (byte* numPtr3 = a_data)
      {
        byte* dataPtr = numPtr3 + a_index;
        int a_DataLength;
        for (; a_length > 0; a_length -= a_DataLength)
        {
          if (this.CS.Complete())
          {
            this.CS.Node().ChainingValue(ref a_Result);
            this.AddChunkChainingValue(a_Result);
            this.CS = Blake3.Blake3ChunkState.CreateBlake3ChunkState(this.Key, this.CS.ChunkCounter() + 1UL, this.Flags);
          }
          a_DataLength = Math.Min(1024 /*0x0400*/ - this.CS.BytesConsumed, a_length);
          this.CS.Update(dataPtr, a_DataLength);
          dataPtr += a_DataLength;
        }
      }
    }
  }

  public override IHashResult TransformFinal()
  {
    this.Finish();
    byte[] a_Destination = new byte[this.HashSize];
    this.InternalDoOutput(ref a_Destination, 0UL, (ulong) a_Destination.Length);
    HashResult hashResult = new HashResult(a_Destination);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public override IHash Clone()
  {
    Blake3 blake3 = new Blake3(this.HashSize, this.Key, this.Flags);
    blake3.CS = this.CS.Clone();
    blake3.OutputReader = this.OutputReader.Clone();
    for (int index = 0; index < this.Stack.Length; ++index)
      blake3.Stack[index] = this.Stack[index].DeepCopy();
    blake3.Used = this.Used;
    blake3.BufferSize = this.BufferSize;
    return (IHash) blake3;
  }

  public static unsafe void DeriveKey(byte[] a_SrcKey, byte[] a_Ctx, byte[] a_SubKey)
  {
    uint[] a_KeyWords = Blake3.IV.DeepCopy();
    fixed (byte* src = new Blake3(32 /*0x20*/, a_KeyWords, 32U /*0x20*/).ComputeBytes(a_Ctx).GetBytes())
      fixed (uint* dest = a_KeyWords)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, 32 /*0x20*/);
    Blake3XOF blake3Xof = new Blake3XOF(32 /*0x20*/, a_KeyWords, 64U /*0x40*/);
    blake3Xof.XOFSizeInBits = (ulong) a_SubKey.Length * 8UL;
    blake3Xof.Initialize();
    blake3Xof.TransformBytes(a_SrcKey);
    blake3Xof.DoOutput(ref a_SubKey, 0UL, (ulong) a_SubKey.Length);
    blake3Xof.Initialize();
  }

  protected struct Blake3Node
  {
    public uint[] CV;
    public uint[] Block;
    public ulong Counter;
    public uint BlockLen;
    public uint Flags;

    public Blake3.Blake3Node Clone()
    {
      return Blake3.Blake3Node.DefaultBlake3Node() with
      {
        CV = this.CV.DeepCopy(),
        Block = this.Block.DeepCopy(),
        Counter = this.Counter,
        BlockLen = this.BlockLen,
        Flags = this.Flags
      };
    }

    public void ChainingValue(ref uint[] a_Result)
    {
      uint[] a_PtrState = new uint[16 /*0x10*/];
      this.Compress(ref a_PtrState);
      Intermech.Hashes.Utils.Utils.Memmove(ref a_Result, a_PtrState, 8);
    }

    public void Compress(ref uint[] a_PtrState)
    {
      a_PtrState[0] = this.CV[0];
      a_PtrState[1] = this.CV[1];
      a_PtrState[2] = this.CV[2];
      a_PtrState[3] = this.CV[3];
      a_PtrState[4] = this.CV[4];
      a_PtrState[5] = this.CV[5];
      a_PtrState[6] = this.CV[6];
      a_PtrState[7] = this.CV[7];
      a_PtrState[8] = Blake3.IV[0];
      a_PtrState[9] = Blake3.IV[1];
      a_PtrState[10] = Blake3.IV[2];
      a_PtrState[11] = Blake3.IV[3];
      a_PtrState[12] = (uint) this.Counter;
      a_PtrState[13] = (uint) (this.Counter >> 32 /*0x20*/);
      a_PtrState[14] = this.BlockLen;
      a_PtrState[15] = this.Flags;
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[0], this.Block[1]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[2], this.Block[3]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[4], this.Block[5]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[6], this.Block[7]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[8], this.Block[9]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[10], this.Block[11]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[12], this.Block[13]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[14], this.Block[15]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[2], this.Block[6]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[3], this.Block[10]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[7], this.Block[0]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[4], this.Block[13]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[1], this.Block[11]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[12], this.Block[5]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[9], this.Block[14]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[15], this.Block[8]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[3], this.Block[4]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[10], this.Block[12]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[13], this.Block[2]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[7], this.Block[14]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[6], this.Block[5]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[9], this.Block[0]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[11], this.Block[15]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[8], this.Block[1]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[10], this.Block[7]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[12], this.Block[9]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[14], this.Block[3]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[13], this.Block[15]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[4], this.Block[0]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[11], this.Block[2]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[5], this.Block[8]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[1], this.Block[6]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[12], this.Block[13]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[9], this.Block[11]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[15], this.Block[10]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[14], this.Block[8]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[7], this.Block[2]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[5], this.Block[3]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[0], this.Block[1]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[6], this.Block[4]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[9], this.Block[14]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[11], this.Block[5]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[8], this.Block[12]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[15], this.Block[1]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[13], this.Block[3]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[0], this.Block[10]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[2], this.Block[6]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[4], this.Block[7]);
      this.G(ref a_PtrState, 0U, 4U, 8U, 12U, this.Block[11], this.Block[15]);
      this.G(ref a_PtrState, 1U, 5U, 9U, 13U, this.Block[5], this.Block[0]);
      this.G(ref a_PtrState, 2U, 6U, 10U, 14U, this.Block[1], this.Block[9]);
      this.G(ref a_PtrState, 3U, 7U, 11U, 15U, this.Block[8], this.Block[6]);
      this.G(ref a_PtrState, 0U, 5U, 10U, 15U, this.Block[14], this.Block[10]);
      this.G(ref a_PtrState, 1U, 6U, 11U, 12U, this.Block[2], this.Block[12]);
      this.G(ref a_PtrState, 2U, 7U, 8U, 13U, this.Block[3], this.Block[4]);
      this.G(ref a_PtrState, 3U, 4U, 9U, 14U, this.Block[7], this.Block[13]);
      a_PtrState[0] = a_PtrState[0] ^ a_PtrState[8];
      a_PtrState[1] = a_PtrState[1] ^ a_PtrState[9];
      a_PtrState[2] = a_PtrState[2] ^ a_PtrState[10];
      a_PtrState[3] = a_PtrState[3] ^ a_PtrState[11];
      a_PtrState[4] = a_PtrState[4] ^ a_PtrState[12];
      a_PtrState[5] = a_PtrState[5] ^ a_PtrState[13];
      a_PtrState[6] = a_PtrState[6] ^ a_PtrState[14];
      a_PtrState[7] = a_PtrState[7] ^ a_PtrState[15];
      a_PtrState[8] = a_PtrState[8] ^ this.CV[0];
      a_PtrState[9] = a_PtrState[9] ^ this.CV[1];
      a_PtrState[10] = a_PtrState[10] ^ this.CV[2];
      a_PtrState[11] = a_PtrState[11] ^ this.CV[3];
      a_PtrState[12] = a_PtrState[12] ^ this.CV[4];
      a_PtrState[13] = a_PtrState[13] ^ this.CV[5];
      a_PtrState[14] = a_PtrState[14] ^ this.CV[6];
      a_PtrState[15] = a_PtrState[15] ^ this.CV[7];
    }

    private void G(ref uint[] a_PtrState, uint A, uint B, uint C, uint D, uint X, uint Y)
    {
      uint num1 = a_PtrState[(int) A];
      uint num2 = a_PtrState[(int) B];
      uint num3 = a_PtrState[(int) C];
      uint num4 = a_PtrState[(int) D];
      uint num5 = num1 + num2 + X;
      uint num6 = Bits.RotateRight32(num4 ^ num5, 16 /*0x10*/);
      uint num7 = num3 + num6;
      uint num8 = Bits.RotateRight32(num2 ^ num7, 12);
      uint num9 = num5 + num8 + Y;
      uint num10 = Bits.RotateRight32(num6 ^ num9, 8);
      uint num11 = num7 + num10;
      uint num12 = Bits.RotateRight32(num8 ^ num11, 7);
      a_PtrState[(int) A] = num9;
      a_PtrState[(int) B] = num12;
      a_PtrState[(int) C] = num11;
      a_PtrState[(int) D] = num10;
    }

    public static Blake3.Blake3Node DefaultBlake3Node()
    {
      return new Blake3.Blake3Node()
      {
        CV = new uint[8],
        Block = new uint[16 /*0x10*/],
        Counter = 0,
        BlockLen = 0,
        Flags = 0
      };
    }

    public static Blake3.Blake3Node CreateBlake3Node(
      uint[] a_CV,
      uint[] a_Block,
      ulong a_Counter,
      uint a_BlockLen,
      uint a_Flags)
    {
      return Blake3.Blake3Node.DefaultBlake3Node() with
      {
        CV = a_CV.DeepCopy(),
        Block = a_Block.DeepCopy(),
        Counter = a_Counter,
        BlockLen = a_BlockLen,
        Flags = a_Flags
      };
    }

    public static Blake3.Blake3Node ParentNode(
      uint[] a_Left,
      uint[] a_Right,
      uint[] a_Key,
      uint a_Flags)
    {
      uint[] a_Block = Intermech.Hashes.Utils.Utils.Concat(a_Left, a_Right);
      return Blake3.Blake3Node.CreateBlake3Node(a_Key, a_Block, 0UL, 64U /*0x40*/, a_Flags | 4U);
    }
  }

  protected struct Blake3ChunkState
  {
    private Blake3.Blake3Node N;
    private byte[] Block;
    public int BlockLen;
    public int BytesConsumed;

    public Blake3.Blake3ChunkState Clone()
    {
      return Blake3.Blake3ChunkState.DefaultBlake3ChunkState() with
      {
        N = this.N.Clone(),
        Block = this.Block.DeepCopy(),
        BlockLen = this.BlockLen,
        BytesConsumed = this.BytesConsumed
      };
    }

    public ulong ChunkCounter() => this.N.Counter;

    public bool Complete() => this.BytesConsumed == 1024 /*0x0400*/;

    public unsafe Blake3.Blake3Node Node()
    {
      Blake3.Blake3Node blake3Node = this.N.Clone();
      fixed (byte* src = this.Block)
        fixed (uint* dest = blake3Node.Block)
        {
          Intermech.Hashes.Utils.Utils.Memset((IntPtr) (void*) src + this.BlockLen, (byte) 0, this.Block.Length - this.BlockLen);
          Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
        }
      blake3Node.BlockLen = (uint) this.BlockLen;
      blake3Node.Flags |= 2U;
      return blake3Node;
    }

    public unsafe void Update(byte* dataPtr, int a_DataLength)
    {
      int num = 0;
      fixed (byte* src = this.Block)
        fixed (uint* dest = this.N.Block)
          fixed (uint* numPtr1 = this.N.CV)
          {
            uint* numPtr2 = numPtr1;
            int n;
            for (; a_DataLength > 0; a_DataLength -= n)
            {
              if (this.BlockLen == 64 /*0x40*/)
              {
                Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
                this.N.ChainingValue(ref this.N.CV);
                this.N.Flags &= this.N.Flags ^ 1U;
                this.BlockLen = 0;
              }
              n = Math.Min(64 /*0x40*/ - this.BlockLen, a_DataLength);
              Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) (src + this.BlockLen), (IntPtr) (void*) (dataPtr + num), n);
              this.BlockLen += n;
              this.BytesConsumed += n;
              num += n;
            }
          }
    }

    public static Blake3.Blake3ChunkState DefaultBlake3ChunkState()
    {
      return new Blake3.Blake3ChunkState()
      {
        N = Blake3.Blake3Node.DefaultBlake3Node(),
        Block = new byte[64 /*0x40*/],
        BlockLen = 0,
        BytesConsumed = 0
      };
    }

    public static Blake3.Blake3ChunkState CreateBlake3ChunkState(
      uint[] a_IV,
      ulong a_ChunkCounter,
      uint a_Flags)
    {
      Blake3.Blake3ChunkState blake3ChunkState = Blake3.Blake3ChunkState.DefaultBlake3ChunkState();
      blake3ChunkState.N.CV = a_IV.DeepCopy();
      blake3ChunkState.N.Counter = a_ChunkCounter;
      blake3ChunkState.N.BlockLen = 64U /*0x40*/;
      blake3ChunkState.N.Flags = a_Flags | 1U;
      return blake3ChunkState;
    }
  }

  protected struct Blake3OutputReader
  {
    public Blake3.Blake3Node N;
    public byte[] Block;
    public ulong Offset;

    public Blake3.Blake3OutputReader Clone()
    {
      return Blake3.Blake3OutputReader.DefaultBlake3OutputReader() with
      {
        N = this.N.Clone(),
        Block = this.Block.DeepCopy(),
        Offset = this.Offset
      };
    }

    public unsafe void Read(
      ref byte[] a_Destination,
      ulong a_DestinationOffset,
      ulong a_OutputLength)
    {
      uint[] a_PtrState = new uint[16 /*0x10*/];
      if (this.Offset == ulong.MaxValue)
        throw new ArgumentOutOfRangeHashLibException(Blake3.MaximumOutputLengthExceeded);
      ulong num = ulong.MaxValue - this.Offset;
      if (a_OutputLength > num)
        a_OutputLength = num;
      fixed (uint* src = a_PtrState)
        fixed (byte* dest = this.Block)
        {
          while (a_OutputLength > 0UL)
          {
            if (((long) this.Offset & 63L /*0x3F*/) == 0L)
            {
              this.N.Counter = this.Offset / 64UL /*0x40*/;
              this.N.Compress(ref a_PtrState);
              Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, 64 /*0x40*/);
            }
            ulong indexSrc = this.Offset & 63UL /*0x3F*/;
            ulong val2 = (ulong) this.Block.Length - indexSrc;
            int n = (int) Math.Min(a_OutputLength, val2);
            Intermech.Hashes.Utils.Utils.Memmove(ref a_Destination, this.Block, n, (int) indexSrc, (int) a_DestinationOffset);
            a_OutputLength -= (ulong) n;
            a_DestinationOffset += (ulong) n;
            this.Offset += (ulong) n;
          }
        }
    }

    public static Blake3.Blake3OutputReader DefaultBlake3OutputReader()
    {
      return new Blake3.Blake3OutputReader()
      {
        Block = new byte[64 /*0x40*/],
        N = Blake3.Blake3Node.DefaultBlake3Node(),
        Offset = 0
      };
    }
  }
}
