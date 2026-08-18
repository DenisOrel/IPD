// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2SConfigurations.Blake2STreeConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2SConfigurations;

public sealed class Blake2STreeConfig : IBlake2STreeConfig
{
  public static readonly string InvalidFanOutParameter = "FanOut Value Should be Between [0 .. 255] for Blake2S";
  public static readonly string InvalidNodeDepthParameter = "NodeDepth Value Should be Between [0 .. 255] for Blake2S";
  public static readonly string InvalidInnerHashSizeParameter = "InnerHashSize Value Should be Between [0 .. 32] for Blake2S";
  public static readonly string InvalidNodeOffsetParameter = "NodeOffset Value Should be Between [0 .. (2^48-1)] for Blake2S";
  private byte fanOut;
  private byte nodeDepth;
  private byte innerHashSize;
  private ulong nodeOffset;

  public byte FanOut
  {
    get => this.fanOut;
    set
    {
      this.ValidateFanOut(value);
      this.fanOut = value;
    }
  }

  public byte MaxDepth { get; set; }

  public byte NodeDepth
  {
    get => this.nodeDepth;
    set
    {
      this.ValidateNodeDepth(value);
      this.nodeDepth = value;
    }
  }

  public byte InnerHashSize
  {
    get => this.innerHashSize;
    set
    {
      this.ValidateInnerHashSize(value);
      this.innerHashSize = value;
    }
  }

  public uint LeafSize { get; set; }

  public ulong NodeOffset
  {
    get => this.nodeOffset;
    set
    {
      this.ValidateNodeOffset(value);
      this.nodeOffset = value;
    }
  }

  public bool IsLastNode { get; set; }

  public Blake2STreeConfig()
  {
    this.FanOut = (byte) 0;
    this.MaxDepth = (byte) 0;
    this.LeafSize = 32U /*0x20*/;
    this.NodeOffset = 0UL;
    this.NodeDepth = (byte) 0;
    this.InnerHashSize = (byte) 32 /*0x20*/;
    this.IsLastNode = false;
  }

  public static IBlake2STreeConfig GetSequentialTreeConfig()
  {
    return (IBlake2STreeConfig) new Blake2STreeConfig()
    {
      FanOut = (byte) 1,
      MaxDepth = (byte) 1,
      LeafSize = 0U,
      NodeOffset = 0UL,
      NodeDepth = (byte) 0,
      InnerHashSize = (byte) 0,
      IsLastNode = false
    };
  }

  public IBlake2STreeConfig Clone()
  {
    return (IBlake2STreeConfig) new Blake2STreeConfig()
    {
      FanOut = this.FanOut,
      InnerHashSize = this.InnerHashSize,
      MaxDepth = this.MaxDepth,
      NodeDepth = this.NodeDepth,
      LeafSize = this.LeafSize,
      NodeOffset = this.NodeOffset,
      IsLastNode = this.IsLastNode
    };
  }

  private void ValidateFanOut(byte a_FanOut)
  {
    if (a_FanOut < (byte) 0 || a_FanOut > byte.MaxValue)
      throw new ArgumentInvalidHashLibException(Blake2STreeConfig.InvalidFanOutParameter);
  }

  private void ValidateInnerHashSize(byte a_InnerHashSize)
  {
    if (a_InnerHashSize < (byte) 0 || a_InnerHashSize > (byte) 32 /*0x20*/)
      throw new ArgumentInvalidHashLibException(Blake2STreeConfig.InvalidInnerHashSizeParameter);
  }

  private void ValidateNodeDepth(byte a_NodeDepth)
  {
    if (a_NodeDepth < (byte) 0 || a_NodeDepth > byte.MaxValue)
      throw new ArgumentInvalidHashLibException(Blake2STreeConfig.InvalidNodeDepthParameter);
  }

  private void ValidateNodeOffset(ulong a_NodeOffset)
  {
    if (a_NodeOffset > 281474976710655UL /*0xFFFFFFFFFFFF*/)
      throw new ArgumentInvalidHashLibException(Blake2STreeConfig.InvalidNodeOffsetParameter);
  }
}
