// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2BConfigurations.Blake2XBConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes.IBlake2BConfigurations;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2BConfigurations;

public sealed class Blake2XBConfig : IBlake2XBConfig
{
  public IBlake2BConfig Blake2BConfig { get; set; }

  public IBlake2BTreeConfig Blake2BTreeConfig { get; set; }

  public Blake2XBConfig(IBlake2BConfig a_Blake2BConfig = null, IBlake2BTreeConfig a_Blake2BTreeConfig = null)
  {
    this.Blake2BConfig = a_Blake2BConfig;
    this.Blake2BTreeConfig = a_Blake2BTreeConfig;
  }

  public IBlake2XBConfig Clone()
  {
    return (IBlake2XBConfig) new Blake2XBConfig(this.Blake2BConfig?.Clone(), this.Blake2BTreeConfig?.Clone());
  }
}
