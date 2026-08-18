// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.PBKDF_Argon2NotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Crypto;
using Intermech.Hashes.Crypto.Blake2BConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.KDF;

internal sealed class PBKDF_Argon2NotBuildInAdapter : 
  KDFNotBuiltIn,
  IPBKDF_Argon2NotBuiltIn,
  IPBKDF_Argon2,
  IKDFNotBuiltIn,
  IKDF
{
  private const int ARGON2_BLOCK_SIZE = 1024 /*0x0400*/;
  private const int ARGON2_QWORDS_IN_BLOCK = 128 /*0x80*/;
  private const int ARGON2_ADDRESSES_IN_BLOCK = 128 /*0x80*/;
  private const int ARGON2_PREHASH_DIGEST_LENGTH = 64 /*0x40*/;
  private const int ARGON2_PREHASH_SEED_LENGTH = 72;
  private const int ARGON2_SYNC_POINTS = 4;
  private const int MIN_PARALLELISM = 1;
  private const int MAX_PARALLELISM = 16777216 /*0x01000000*/;
  private const int MIN_OUTLEN = 4;
  private const int MIN_ITERATIONS = 1;
  private PBKDF_Argon2NotBuildInAdapter.Block[] Memory;
  private int SegmentLength;
  private int LaneLength;
  private IArgon2Parameters Parameters;
  private byte[] Password;
  private byte[] Result;

  private PBKDF_Argon2NotBuildInAdapter()
  {
  }

  internal PBKDF_Argon2NotBuildInAdapter(byte[] a_Password, IArgon2Parameters a_Parameters)
  {
    if (a_Password == null)
      throw new ArgumentNullHashLibException(nameof (a_Password));
    PBKDF_Argon2NotBuildInAdapter.ValidatePBKDF_Argon2Inputs(a_Parameters);
    this.Password = a_Password.DeepCopy();
    this.Parameters = a_Parameters;
    if (this.Parameters.Lanes < 1)
      throw new ArgumentInvalidHashLibException(string.Format(Global.LanesTooSmall, (object) 1));
    if (this.Parameters.Lanes > 16777216 /*0x01000000*/)
      throw new ArgumentInvalidHashLibException(string.Format(Global.LanesTooBig, (object) 16777216 /*0x01000000*/));
    if (this.Parameters.Memory < 2 * this.Parameters.Lanes)
      throw new ArgumentInvalidHashLibException(string.Format(Global.MemoryTooSmall, (object) (2 * this.Parameters.Lanes), (object) (2 * this.Parameters.Lanes)));
    if (this.Parameters.Iterations < 1)
      throw new ArgumentInvalidHashLibException(string.Format(Global.IterationsTooSmall, (object) 1));
    this.DoInit(a_Parameters);
  }

  ~PBKDF_Argon2NotBuildInAdapter() => this.Clear();

  public override byte[] GetBytes(int a_ByteCount)
  {
    if (a_ByteCount <= 4)
      throw new ArgumentHashLibException(string.Format(Global.InvalidOutputByteCount, (object) 4));
    this.Initialize(this.Password, a_ByteCount);
    PBKDF_Argon2NotBuildInAdapter.Position position = PBKDF_Argon2NotBuildInAdapter.Position.CreatePosition();
    PBKDF_Argon2NotBuildInAdapter.DataContainer a_DataContainer = new PBKDF_Argon2NotBuildInAdapter.DataContainer();
    try
    {
      a_DataContainer.Position = position;
      this.DoParallelFillMemoryBlocks(ref a_DataContainer);
    }
    finally
    {
      PBKDF_Argon2NotBuildInAdapter.DataContainer dataContainer = new PBKDF_Argon2NotBuildInAdapter.DataContainer();
    }
    this.Digest(a_ByteCount);
    byte[] dest = new byte[a_ByteCount];
    Intermech.Hashes.Utils.Utils.Memmove(ref dest, this.Result, a_ByteCount);
    this.Reset();
    return dest;
  }

  public override string Name => this.GetType().Name;

  public override string ToString() => this.Name;

  public override IKDFNotBuiltIn Clone()
  {
    return (IKDFNotBuiltIn) new PBKDF_Argon2NotBuildInAdapter()
    {
      Result = this.Result.DeepCopy(),
      Password = this.Password.DeepCopy(),
      Memory = PBKDF_Argon2NotBuildInAdapter.DeepCopyBlockArray(this.Memory),
      Parameters = this.Parameters.Clone(),
      SegmentLength = this.SegmentLength,
      LaneLength = this.LaneLength
    };
  }

  private static PBKDF_Argon2NotBuildInAdapter.Block[] DeepCopyBlockArray(
    PBKDF_Argon2NotBuildInAdapter.Block[] blocks)
  {
    PBKDF_Argon2NotBuildInAdapter.Block[] blockArray = blocks != null ? new PBKDF_Argon2NotBuildInAdapter.Block[blocks.Length] : throw new ArgumentNullHashLibException(nameof (blocks));
    for (int index = 0; index < blockArray.Length; ++index)
      blockArray[index] = blocks[index].Clone();
    return blockArray;
  }

  public override void Clear() => ArrayUtils.ZeroFill(ref this.Password);

  private byte[] InitialHash(IArgon2Parameters a_Parameters, int a_OutputLength, byte[] a_Password)
  {
    IHash a_Hash = PBKDF_Argon2NotBuildInAdapter.MakeBlake2BInstanceAndInitialize(64 /*0x40*/);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, a_Parameters.Lanes);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, a_OutputLength);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, a_Parameters.Memory);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, a_Parameters.Iterations);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, (int) a_Parameters.Version);
    PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, (int) a_Parameters.Type);
    PBKDF_Argon2NotBuildInAdapter.AddByteString(a_Hash, a_Password);
    PBKDF_Argon2NotBuildInAdapter.AddByteString(a_Hash, a_Parameters.Salt);
    PBKDF_Argon2NotBuildInAdapter.AddByteString(a_Hash, a_Parameters.Secret);
    PBKDF_Argon2NotBuildInAdapter.AddByteString(a_Hash, a_Parameters.Additional);
    return a_Hash.TransformFinal().GetBytes();
  }

  private byte[] GetInitialHashLong(byte[] a_InitialHash, byte[] a_Appendix)
  {
    byte[] dest = new byte[72];
    Intermech.Hashes.Utils.Utils.Memmove(ref dest, a_InitialHash, 64 /*0x40*/);
    Intermech.Hashes.Utils.Utils.Memmove(ref dest, a_Appendix, 4, indexDest: 64 /*0x40*/);
    return dest;
  }

  private byte[] Hash(byte[] a_Input, int a_OutputLength)
  {
    byte[] dest = new byte[a_OutputLength];
    byte[] a_data = Converters.ReadUInt32AsBytesLE((uint) a_OutputLength);
    int a_HashSize = 64 /*0x40*/;
    if (a_OutputLength <= a_HashSize)
    {
      IHash hash = PBKDF_Argon2NotBuildInAdapter.MakeBlake2BInstanceAndInitialize(a_OutputLength);
      hash.TransformBytes(a_data, 0, a_data.Length);
      hash.TransformBytes(a_Input, 0, a_Input.Length);
      dest = hash.TransformFinal().GetBytes();
    }
    else
    {
      IHash hash1 = PBKDF_Argon2NotBuildInAdapter.MakeBlake2BInstanceAndInitialize(a_HashSize);
      byte[] numArray = new byte[a_HashSize];
      hash1.TransformBytes(a_data, 0, a_data.Length);
      hash1.TransformBytes(a_Input, 0, a_Input.Length);
      byte[] bytes1 = hash1.TransformFinal().GetBytes();
      Intermech.Hashes.Utils.Utils.Memmove(ref dest, bytes1, a_HashSize / 2);
      int num1 = (a_OutputLength + 31 /*0x1F*/) / 32 /*0x20*/ - 2;
      int indexDest = a_HashSize / 2;
      int num2 = 2;
      while (num2 <= num1)
      {
        hash1.TransformBytes(bytes1, 0, bytes1.Length);
        bytes1 = hash1.TransformFinal().GetBytes();
        Intermech.Hashes.Utils.Utils.Memmove(ref dest, bytes1, a_HashSize / 2, indexDest: indexDest);
        ++num2;
        indexDest += a_HashSize / 2;
      }
      int num3 = a_OutputLength - 32 /*0x20*/ * num1;
      IHash hash2 = PBKDF_Argon2NotBuildInAdapter.MakeBlake2BInstanceAndInitialize(num3);
      hash2.TransformBytes(bytes1, 0, bytes1.Length);
      byte[] bytes2 = hash2.TransformFinal().GetBytes();
      Intermech.Hashes.Utils.Utils.Memmove(ref dest, bytes2, num3, indexDest: indexDest);
    }
    return dest;
  }

  private void Digest(int a_OutputLength)
  {
    PBKDF_Argon2NotBuildInAdapter.Block block = this.Memory[this.LaneLength - 1];
    for (int index1 = 1; index1 < this.Parameters.Lanes; ++index1)
    {
      int index2 = index1 * this.LaneLength + (this.LaneLength - 1);
      block.XorWith(this.Memory[index2]);
    }
    this.Result = this.Hash(block.ToBytes(), a_OutputLength);
  }

  private void FillFirstBlocks(byte[] a_InitialHash)
  {
    byte[] a_Appendix1 = new byte[4];
    byte[] a_Appendix2 = new byte[4]
    {
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    byte[] initialHashLong1 = this.GetInitialHashLong(a_InitialHash, a_Appendix1);
    byte[] initialHashLong2 = this.GetInitialHashLong(a_InitialHash, a_Appendix2);
    for (int a_Input1 = 0; a_Input1 < this.Parameters.Lanes; ++a_Input1)
    {
      Converters.ReadUInt32AsBytesLE((uint) a_Input1, ref initialHashLong1, 68);
      Converters.ReadUInt32AsBytesLE((uint) a_Input1, ref initialHashLong2, 68);
      byte[] a_Input2 = this.Hash(initialHashLong1, 1024 /*0x0400*/);
      this.Memory[a_Input1 * this.LaneLength].FromBytes(a_Input2);
      byte[] a_Input3 = this.Hash(initialHashLong2, 1024 /*0x0400*/);
      this.Memory[a_Input1 * this.LaneLength + 1].FromBytes(a_Input3);
    }
  }

  private bool IsDataIndependentAddressing(PBKDF_Argon2NotBuildInAdapter.Position a_Position)
  {
    if (this.Parameters.Type == Argon2Type.a2tARGON2_i)
      return true;
    return this.Parameters.Type == Argon2Type.a2tARGON2_id && a_Position.Pass == 0 && a_Position.Slice < 2;
  }

  private void Initialize(byte[] a_Password, int a_OutputLength)
  {
    this.FillFirstBlocks(this.InitialHash(this.Parameters, a_OutputLength, a_Password));
  }

  private void FillSegment(int a_Idx, PBKDF_Argon2NotBuildInAdapter.Position a_Position)
  {
    a_Position.Lane = a_Idx;
    PBKDF_Argon2NotBuildInAdapter.TFillBlock fillBlock = PBKDF_Argon2NotBuildInAdapter.TFillBlock.CreateFillBlock();
    bool a_DataIndependentAddressing = this.IsDataIndependentAddressing(a_Position);
    int startingIndex = PBKDF_Argon2NotBuildInAdapter.GetStartingIndex(a_Position);
    int a_CurrentOffset = a_Position.Lane * this.LaneLength + a_Position.Slice * this.SegmentLength + startingIndex;
    int a_PrevOffset1 = this.GetPrevOffset(a_CurrentOffset);
    PBKDF_Argon2NotBuildInAdapter.Block a_AddressBlock = new PBKDF_Argon2NotBuildInAdapter.Block();
    PBKDF_Argon2NotBuildInAdapter.Block a_InputBlock = new PBKDF_Argon2NotBuildInAdapter.Block();
    PBKDF_Argon2NotBuildInAdapter.Block a_ZeroBlock = new PBKDF_Argon2NotBuildInAdapter.Block();
    if (a_DataIndependentAddressing)
    {
      a_AddressBlock = fillBlock.AddressBlock.Clear();
      a_ZeroBlock = fillBlock.ZeroBlock.Clear();
      a_InputBlock = fillBlock.InputBlock.Clear();
      this.InitAddressBlocks(fillBlock, a_Position, a_ZeroBlock, ref a_InputBlock, ref a_AddressBlock);
    }
    a_Position.Index = startingIndex;
    while (a_Position.Index < this.SegmentLength)
    {
      int a_PrevOffset2 = this.RotatePrevOffset(a_CurrentOffset, a_PrevOffset1);
      ulong pseudoRandom = this.GetPseudoRandom(fillBlock, a_Position, a_AddressBlock, a_InputBlock, a_ZeroBlock, a_PrevOffset2, a_DataIndependentAddressing);
      int refLane = this.GetRefLane(a_Position, pseudoRandom);
      int refColumn = this.GetRefColumn(a_Position, pseudoRandom, refLane == a_Position.Lane);
      PBKDF_Argon2NotBuildInAdapter.Block a_x = this.Memory[a_PrevOffset2];
      PBKDF_Argon2NotBuildInAdapter.Block a_y = this.Memory[this.LaneLength * refLane + refColumn];
      PBKDF_Argon2NotBuildInAdapter.Block a_CurrentBlock = this.Memory[a_CurrentOffset];
      bool a_WithXor = this.IsWithXor(a_Position);
      fillBlock.FillBlock(a_x, a_y, ref a_CurrentBlock, a_WithXor);
      ++a_Position.Index;
      ++a_CurrentOffset;
      a_PrevOffset1 = a_PrevOffset2 + 1;
    }
  }

  private void InitializeMemory(int a_MemoryBlocks)
  {
    this.Memory = new PBKDF_Argon2NotBuildInAdapter.Block[a_MemoryBlocks];
    for (int index = 0; index < this.Memory.Length; ++index)
      this.Memory[index] = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock();
  }

  private void DoInit(IArgon2Parameters a_Parameters)
  {
    int num = a_Parameters.Memory;
    if (num < 8 * a_Parameters.Lanes)
      num = 8 * a_Parameters.Lanes;
    this.SegmentLength = num / (this.Parameters.Lanes * 4);
    this.LaneLength = this.SegmentLength * 4;
    this.InitializeMemory(this.SegmentLength * (a_Parameters.Lanes * 4));
  }

  private void NextAddresses(
    PBKDF_Argon2NotBuildInAdapter.TFillBlock a_Filler,
    PBKDF_Argon2NotBuildInAdapter.Block a_ZeroBlock,
    PBKDF_Argon2NotBuildInAdapter.Block a_InputBlock,
    ref PBKDF_Argon2NotBuildInAdapter.Block a_AddressBlock)
  {
    ++a_InputBlock.v[6];
    a_Filler.FillBlock(a_ZeroBlock, a_InputBlock, ref a_AddressBlock, false);
    a_Filler.FillBlock(a_ZeroBlock, a_AddressBlock, ref a_AddressBlock, false);
  }

  private void FillMemoryBlocks(
    int a_Idx,
    ref PBKDF_Argon2NotBuildInAdapter.DataContainer a_DataContainer)
  {
    PBKDF_Argon2NotBuildInAdapter.Position position = a_DataContainer.Position;
    this.FillSegment(a_Idx, position);
  }

  private void DoParallelFillMemoryBlocks(
    ref PBKDF_Argon2NotBuildInAdapter.DataContainer a_DataContainer)
  {
    int iterations = this.Parameters.Iterations;
    int lanes = this.Parameters.Lanes;
    for (int a_Pass = 0; a_Pass < iterations; ++a_Pass)
    {
      for (int a_Slice = 0; a_Slice < 4; ++a_Slice)
      {
        for (int index = 0; index < lanes; ++index)
        {
          a_DataContainer.Position.Update(a_Pass, index, a_Slice, 0);
          this.FillMemoryBlocks(index, ref a_DataContainer);
        }
      }
    }
  }

  private void InitAddressBlocks(
    PBKDF_Argon2NotBuildInAdapter.TFillBlock a_Filler,
    PBKDF_Argon2NotBuildInAdapter.Position a_Position,
    PBKDF_Argon2NotBuildInAdapter.Block a_ZeroBlock,
    ref PBKDF_Argon2NotBuildInAdapter.Block a_InputBlock,
    ref PBKDF_Argon2NotBuildInAdapter.Block a_AddressBlock)
  {
    a_InputBlock.v[0] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64(a_Position.Pass);
    a_InputBlock.v[1] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64(a_Position.Lane);
    a_InputBlock.v[2] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64(a_Position.Slice);
    a_InputBlock.v[3] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64(this.Memory.Length);
    a_InputBlock.v[4] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64(this.Parameters.Iterations);
    a_InputBlock.v[5] = PBKDF_Argon2NotBuildInAdapter.IntToUInt64((int) this.Parameters.Type);
    if (a_Position.Pass != 0 || a_Position.Slice != 0)
      return;
    this.NextAddresses(a_Filler, a_ZeroBlock, a_InputBlock, ref a_AddressBlock);
  }

  private bool IsWithXor(PBKDF_Argon2NotBuildInAdapter.Position a_Position)
  {
    return a_Position.Pass != 0 && this.Parameters.Version != Argon2Version.a2vARGON2_VERSION_10;
  }

  private int GetPrevOffset(int a_CurrentOffset)
  {
    return a_CurrentOffset % this.LaneLength == 0 ? a_CurrentOffset + this.LaneLength - 1 : a_CurrentOffset - 1;
  }

  private int RotatePrevOffset(int a_CurrentOffset, int a_PrevOffset)
  {
    if (a_CurrentOffset % this.LaneLength == 1)
      a_PrevOffset = a_CurrentOffset - 1;
    return a_PrevOffset;
  }

  private static ulong IntToUInt64(int a_x) => (ulong) a_x & (ulong) uint.MaxValue;

  private void Reset()
  {
    for (int index = 0; index < this.Memory.Length; ++index)
    {
      this.Memory[index].Clear();
      this.Memory[index] = new PBKDF_Argon2NotBuildInAdapter.Block();
    }
    this.Memory = (PBKDF_Argon2NotBuildInAdapter.Block[]) null;
    ArrayUtils.ZeroFill(ref this.Result);
  }

  private void fBlaMka(PBKDF_Argon2NotBuildInAdapter.Block a_Block, int a_x, int a_y)
  {
    uint maxValue = uint.MaxValue;
    ulong num = (ulong) (((long) a_Block.v[a_x] & (long) maxValue) * ((long) a_Block.v[a_y] & (long) maxValue));
    a_Block.v[a_x] = (ulong) ((long) a_Block.v[a_x] + (long) a_Block.v[a_y] + 2L * (long) num);
  }

  private void Rotr64(PBKDF_Argon2NotBuildInAdapter.Block a_Block, int a_v, int a_w, int a_c)
  {
    ulong a_value = a_Block.v[a_v] ^ a_Block.v[a_w];
    a_Block.v[a_v] = Bits.RotateRight64(a_value, a_c);
  }

  private void F(PBKDF_Argon2NotBuildInAdapter.Block a_Block, int a_a, int a_b, int a_c, int a_d)
  {
    this.fBlaMka(a_Block, a_a, a_b);
    this.Rotr64(a_Block, a_d, a_a, 32 /*0x20*/);
    this.fBlaMka(a_Block, a_c, a_d);
    this.Rotr64(a_Block, a_b, a_c, 24);
    this.fBlaMka(a_Block, a_a, a_b);
    this.Rotr64(a_Block, a_d, a_a, 16 /*0x10*/);
    this.fBlaMka(a_Block, a_c, a_d);
    this.Rotr64(a_Block, a_b, a_c, 63 /*0x3F*/);
  }

  private void RoundFunction(
    PBKDF_Argon2NotBuildInAdapter.Block a_Block,
    int a_v0,
    int a_v1,
    int a_v2,
    int a_v3,
    int a_v4,
    int a_v5,
    int a_v6,
    int a_v7,
    int a_v8,
    int a_v9,
    int a_v10,
    int a_v11,
    int a_v12,
    int a_v13,
    int a_v14,
    int a_v15)
  {
    this.F(a_Block, a_v0, a_v4, a_v8, a_v12);
    this.F(a_Block, a_v1, a_v5, a_v9, a_v13);
    this.F(a_Block, a_v2, a_v6, a_v10, a_v14);
    this.F(a_Block, a_v3, a_v7, a_v11, a_v15);
    this.F(a_Block, a_v0, a_v5, a_v10, a_v15);
    this.F(a_Block, a_v1, a_v6, a_v11, a_v12);
    this.F(a_Block, a_v2, a_v7, a_v8, a_v13);
    this.F(a_Block, a_v3, a_v4, a_v9, a_v14);
  }

  private void FillBlock(
    PBKDF_Argon2NotBuildInAdapter.Block a_x,
    PBKDF_Argon2NotBuildInAdapter.Block a_y,
    PBKDF_Argon2NotBuildInAdapter.Block a_CurrentBlock,
    bool a_WithXor)
  {
    PBKDF_Argon2NotBuildInAdapter.Block block1 = new PBKDF_Argon2NotBuildInAdapter.Block();
    PBKDF_Argon2NotBuildInAdapter.Block block2 = new PBKDF_Argon2NotBuildInAdapter.Block();
    PBKDF_Argon2NotBuildInAdapter.Block a_Block = block1.Clone();
    for (int index = 0; index < 8; ++index)
      this.RoundFunction(a_Block, 16 /*0x10*/ * index, 16 /*0x10*/ * index + 1, 16 /*0x10*/ * index + 2, 16 /*0x10*/ * index + 3, 16 /*0x10*/ * index + 4, 16 /*0x10*/ * index + 5, 16 /*0x10*/ * index + 6, 16 /*0x10*/ * index + 7, 16 /*0x10*/ * index + 8, 16 /*0x10*/ * index + 9, 16 /*0x10*/ * index + 10, 16 /*0x10*/ * index + 11, 16 /*0x10*/ * index + 12, 16 /*0x10*/ * index + 13, 16 /*0x10*/ * index + 14, 16 /*0x10*/ * index + 15);
    for (int index = 0; index < 8; ++index)
      this.RoundFunction(a_Block, 2 * index, 2 * index + 1, 2 * index + 16 /*0x10*/, 2 * index + 17, 2 * index + 32 /*0x20*/, 2 * index + 33, 2 * index + 48 /*0x30*/, 2 * index + 49, 2 * index + 64 /*0x40*/, 2 * index + 65, 2 * index + 80 /*0x50*/, 2 * index + 81, 2 * index + 96 /*0x60*/, 2 * index + 97, 2 * index + 112 /*0x70*/, 2 * index + 113);
  }

  private ulong GetPseudoRandom(
    PBKDF_Argon2NotBuildInAdapter.TFillBlock a_Filler,
    PBKDF_Argon2NotBuildInAdapter.Position a_Position,
    PBKDF_Argon2NotBuildInAdapter.Block a_AddressBlock,
    PBKDF_Argon2NotBuildInAdapter.Block a_InputBlock,
    PBKDF_Argon2NotBuildInAdapter.Block a_ZeroBlock,
    int a_PrevOffset,
    bool a_DataIndependentAddressing)
  {
    if (!a_DataIndependentAddressing)
      return this.Memory[a_PrevOffset].v[0];
    if (a_Position.Index % 128 /*0x80*/ == 0)
      this.NextAddresses(a_Filler, a_ZeroBlock, a_InputBlock, ref a_AddressBlock);
    return a_AddressBlock.v[a_Position.Index % 128 /*0x80*/];
  }

  private int GetRefLane(PBKDF_Argon2NotBuildInAdapter.Position a_Position, ulong a_PseudoRandom)
  {
    int refLane = (int) ((a_PseudoRandom >> 32 /*0x20*/) % (ulong) this.Parameters.Lanes);
    if (a_Position.Pass == 0 && a_Position.Slice == 0)
      refLane = a_Position.Lane;
    return refLane;
  }

  private int GetRefColumn(
    PBKDF_Argon2NotBuildInAdapter.Position a_Position,
    ulong a_PseudoRandom,
    bool a_SameLane)
  {
    int num1;
    int num2;
    if (a_Position.Pass == 0)
    {
      num1 = 0;
      if (a_SameLane)
      {
        num2 = a_Position.Slice * this.SegmentLength + a_Position.Index - 1;
      }
      else
      {
        int num3 = a_Position.Index != 0 ? 0 : -1;
        num2 = a_Position.Slice * this.SegmentLength + num3;
      }
    }
    else
    {
      num1 = (a_Position.Slice + 1) * this.SegmentLength % this.LaneLength;
      num2 = !a_SameLane ? this.LaneLength - this.SegmentLength + (a_Position.Index != 0 ? 0 : -1) : this.LaneLength - this.SegmentLength + a_Position.Index - 1;
    }
    ulong num4 = a_PseudoRandom & (ulong) uint.MaxValue;
    ulong num5 = num4 * num4 >> 32 /*0x20*/;
    ulong num6 = (ulong) num2 - 1UL - ((ulong) num2 * num5 >> 32 /*0x20*/);
    return (int) (((ulong) num1 + num6) % (ulong) this.LaneLength);
  }

  private static void ValidatePBKDF_Argon2Inputs(IArgon2Parameters a_Argon2Parameters)
  {
    if (a_Argon2Parameters == null)
      throw new ArgumentNullHashLibException(Global.Argon2ParameterBuilderNotInitialized);
  }

  private static void AddIntToLittleEndian(IHash a_Hash, int a_n)
  {
    a_Hash.TransformBytes(Converters.ReadUInt32AsBytesLE((uint) a_n));
  }

  private static void AddByteString(IHash a_Hash, byte[] a_Octets)
  {
    if (!a_Octets.Empty())
    {
      PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, a_Octets.Length);
      a_Hash.TransformBytes(a_Octets, 0, a_Octets.Length);
    }
    else
      PBKDF_Argon2NotBuildInAdapter.AddIntToLittleEndian(a_Hash, 0);
  }

  private static IHash MakeBlake2BInstanceAndInitialize(int a_HashSize)
  {
    Blake2B blake2B = new Blake2B((IBlake2BConfig) new Blake2BConfig(a_HashSize));
    blake2B.Initialize();
    return (IHash) blake2B;
  }

  private static int GetStartingIndex(PBKDF_Argon2NotBuildInAdapter.Position a_Position)
  {
    return a_Position.Pass == 0 && a_Position.Slice == 0 ? 2 : 0;
  }

  private struct Block
  {
    public const int SIZE = 128 /*0x80*/;
    public ulong[] v;
    public bool Initialized;

    private Block(ulong[] _v, bool _Initialized)
    {
      this.v = _v.DeepCopy();
      this.Initialized = _Initialized;
    }

    private void CheckAreBlocksInitialized(PBKDF_Argon2NotBuildInAdapter.Block[] a_Blocks)
    {
      foreach (PBKDF_Argon2NotBuildInAdapter.Block aBlock in a_Blocks)
      {
        if (!aBlock.Initialized)
          throw new ArgumentNullHashLibException(Global.BlockInstanceNotInitialized);
      }
    }

    public void CopyBlock(PBKDF_Argon2NotBuildInAdapter.Block a_Other)
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[2]
      {
        this,
        a_Other
      });
      this.v = a_Other.v.DeepCopy();
    }

    public void Xor(
      PBKDF_Argon2NotBuildInAdapter.Block a_B1,
      PBKDF_Argon2NotBuildInAdapter.Block a_B2)
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[3]
      {
        this,
        a_B1,
        a_B2
      });
      for (int index = 0; index < 128 /*0x80*/; ++index)
        this.v[index] = a_B1.v[index] ^ a_B2.v[index];
    }

    public void XorWith(PBKDF_Argon2NotBuildInAdapter.Block a_Other)
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[2]
      {
        this,
        a_Other
      });
      for (int index = 0; index < this.v.Length; ++index)
        this.v[index] = this.v[index] ^ a_Other.v[index];
    }

    public static PBKDF_Argon2NotBuildInAdapter.Block CreateBlock()
    {
      return new PBKDF_Argon2NotBuildInAdapter.Block()
      {
        v = new ulong[128 /*0x80*/],
        Initialized = true
      };
    }

    public PBKDF_Argon2NotBuildInAdapter.Block Clear()
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[1]
      {
        this
      });
      ArrayUtils.ZeroFill(ref this.v);
      return this;
    }

    public void Xor(
      PBKDF_Argon2NotBuildInAdapter.Block a_B1,
      PBKDF_Argon2NotBuildInAdapter.Block a_B2,
      PBKDF_Argon2NotBuildInAdapter.Block a_B3)
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[4]
      {
        this,
        a_B1,
        a_B2,
        a_B3
      });
      for (int index = 0; index < 128 /*0x80*/; ++index)
        this.v[index] = a_B1.v[index] ^ a_B2.v[index] ^ a_B3.v[index];
    }

    public unsafe void FromBytes(byte[] a_Input)
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[1]
      {
        this
      });
      if (a_Input.Length != 1024 /*0x0400*/)
        throw new ArgumentHashLibException(string.Format(Global.InputLengthInvalid, (object) a_Input.Length, (object) 1024 /*0x0400*/));
      fixed (byte* a_in = a_Input)
      {
        for (int index = 0; index < 128 /*0x80*/; ++index)
          this.v[index] = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, index * 8);
      }
    }

    public byte[] ToBytes()
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[1]
      {
        this
      });
      byte[] a_out = new byte[1024 /*0x0400*/];
      for (int index = 0; index < 128 /*0x80*/; ++index)
        Converters.ReadUInt64AsBytesLE(this.v[index], ref a_out, index * 8);
      return a_out;
    }

    public override string ToString()
    {
      this.CheckAreBlocksInitialized(new PBKDF_Argon2NotBuildInAdapter.Block[1]
      {
        this
      });
      string str = "";
      for (int index = 0; index < 128 /*0x80*/; ++index)
        str += Converters.ConvertBytesToHexString(Converters.ReadUInt64AsBytesLE(this.v[index]), false);
      return str;
    }

    public PBKDF_Argon2NotBuildInAdapter.Block Clone()
    {
      return new PBKDF_Argon2NotBuildInAdapter.Block()
      {
        v = this.v.DeepCopy(),
        Initialized = this.Initialized
      };
    }
  }

  private struct Position
  {
    public int Pass { get; set; }

    public int Lane { get; set; }

    public int Slice { get; set; }

    public int Index { get; set; }

    public static PBKDF_Argon2NotBuildInAdapter.Position CreatePosition()
    {
      return new PBKDF_Argon2NotBuildInAdapter.Position();
    }

    public void Update(int a_Pass, int a_Lane, int a_Slice, int a_Index)
    {
      this.Pass = a_Pass;
      this.Lane = a_Lane;
      this.Slice = a_Slice;
      this.Index = a_Index;
    }
  }

  private struct TFillBlock
  {
    public PBKDF_Argon2NotBuildInAdapter.Block R;
    public PBKDF_Argon2NotBuildInAdapter.Block Z;
    public PBKDF_Argon2NotBuildInAdapter.Block AddressBlock;
    public PBKDF_Argon2NotBuildInAdapter.Block ZeroBlock;
    public PBKDF_Argon2NotBuildInAdapter.Block InputBlock;

    public static void BlaMka(ref PBKDF_Argon2NotBuildInAdapter.Block a_Block, int a_x, int a_y)
    {
      uint maxValue = uint.MaxValue;
      ulong num = (ulong) (((long) a_Block.v[a_x] & (long) maxValue) * ((long) a_Block.v[a_y] & (long) maxValue));
      a_Block.v[a_x] = (ulong) ((long) a_Block.v[a_x] + (long) a_Block.v[a_y] + 2L * (long) num);
    }

    public static void Rotr64(
      ref PBKDF_Argon2NotBuildInAdapter.Block a_Block,
      int a_v,
      int a_w,
      int a_c)
    {
      ulong a_value = a_Block.v[a_v] ^ a_Block.v[a_w];
      a_Block.v[a_v] = Bits.RotateRight64(a_value, a_c);
    }

    public static void F(
      ref PBKDF_Argon2NotBuildInAdapter.Block a_Block,
      int a_a,
      int a_b,
      int a_c,
      int a_d)
    {
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.BlaMka(ref a_Block, a_a, a_b);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.Rotr64(ref a_Block, a_d, a_a, 32 /*0x20*/);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.BlaMka(ref a_Block, a_c, a_d);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.Rotr64(ref a_Block, a_b, a_c, 24);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.BlaMka(ref a_Block, a_a, a_b);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.Rotr64(ref a_Block, a_d, a_a, 16 /*0x10*/);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.BlaMka(ref a_Block, a_c, a_d);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.Rotr64(ref a_Block, a_b, a_c, 63 /*0x3F*/);
    }

    public static void RoundFunction(
      ref PBKDF_Argon2NotBuildInAdapter.Block a_Block,
      int a_v0,
      int a_v1,
      int a_v2,
      int a_v3,
      int a_v4,
      int a_v5,
      int a_v6,
      int a_v7,
      int a_v8,
      int a_v9,
      int a_v10,
      int a_v11,
      int a_v12,
      int a_v13,
      int a_v14,
      int a_v15)
    {
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v0, a_v4, a_v8, a_v12);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v1, a_v5, a_v9, a_v13);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v2, a_v6, a_v10, a_v14);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v3, a_v7, a_v11, a_v15);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v0, a_v5, a_v10, a_v15);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v1, a_v6, a_v11, a_v12);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v2, a_v7, a_v8, a_v13);
      PBKDF_Argon2NotBuildInAdapter.TFillBlock.F(ref a_Block, a_v3, a_v4, a_v9, a_v14);
    }

    private void ApplyBlake()
    {
      for (int index = 0; index < 8; ++index)
      {
        int a_v0 = 16 /*0x10*/ * index;
        PBKDF_Argon2NotBuildInAdapter.TFillBlock.RoundFunction(ref this.Z, a_v0, a_v0 + 1, a_v0 + 2, a_v0 + 3, a_v0 + 4, a_v0 + 5, a_v0 + 6, a_v0 + 7, a_v0 + 8, a_v0 + 9, a_v0 + 10, a_v0 + 11, a_v0 + 12, a_v0 + 13, a_v0 + 14, a_v0 + 15);
      }
      for (int index = 0; index < 8; ++index)
      {
        int a_v0 = 2 * index;
        PBKDF_Argon2NotBuildInAdapter.TFillBlock.RoundFunction(ref this.Z, a_v0, a_v0 + 1, a_v0 + 16 /*0x10*/, a_v0 + 17, a_v0 + 32 /*0x20*/, a_v0 + 33, a_v0 + 48 /*0x30*/, a_v0 + 49, a_v0 + 64 /*0x40*/, a_v0 + 65, a_v0 + 80 /*0x50*/, a_v0 + 81, a_v0 + 96 /*0x60*/, a_v0 + 97, a_v0 + 112 /*0x70*/, a_v0 + 113);
      }
    }

    public static PBKDF_Argon2NotBuildInAdapter.TFillBlock CreateFillBlock()
    {
      return new PBKDF_Argon2NotBuildInAdapter.TFillBlock()
      {
        R = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock(),
        Z = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock(),
        AddressBlock = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock(),
        ZeroBlock = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock(),
        InputBlock = PBKDF_Argon2NotBuildInAdapter.Block.CreateBlock()
      };
    }

    public void FillBlock(
      PBKDF_Argon2NotBuildInAdapter.Block a_x,
      PBKDF_Argon2NotBuildInAdapter.Block a_y,
      ref PBKDF_Argon2NotBuildInAdapter.Block a_CurrentBlock,
      bool a_WithXor)
    {
      this.R.Xor(a_x, a_y);
      this.Z.CopyBlock(this.R);
      this.ApplyBlake();
      if (a_WithXor)
        a_CurrentBlock.Xor(this.R, this.Z, a_CurrentBlock);
      else
        a_CurrentBlock.Xor(this.R, this.Z);
    }
  }

  private struct DataContainer
  {
    public PBKDF_Argon2NotBuildInAdapter.Position Position;
  }
}
