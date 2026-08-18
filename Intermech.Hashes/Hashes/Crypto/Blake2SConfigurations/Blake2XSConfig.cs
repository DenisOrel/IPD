// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2SConfigurations.Blake2XSConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes.IBlake2SConfigurations;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2SConfigurations;

public sealed class Blake2XSConfig : IBlake2XSConfig
{
  public IBlake2SConfig Blake2SConfig { get; set; }

  public IBlake2STreeConfig Blake2STreeConfig { get; set; }

  public Blake2XSConfig(IBlake2SConfig a_Blake2SConfig = null, IBlake2STreeConfig a_Blake2STreeConfig = null)
  {
    this.Blake2SConfig = a_Blake2SConfig;
    this.Blake2STreeConfig = a_Blake2STreeConfig;
  }

  public IBlake2XSConfig Clone()
  {
    return (IBlake2XSConfig) new Blake2XSConfig(this.Blake2SConfig?.Clone(), this.Blake2STreeConfig?.Clone());
  }
}
