// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IBlake2SConfigurations.IBlake2STreeConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Interfaces.Hashes.IBlake2SConfigurations;

public interface IBlake2STreeConfig
{
  byte FanOut { get; set; }

  byte MaxDepth { get; set; }

  byte NodeDepth { get; set; }

  byte InnerHashSize { get; set; }

  uint LeafSize { get; set; }

  ulong NodeOffset { get; set; }

  bool IsLastNode { get; set; }

  IBlake2STreeConfig Clone();
}
