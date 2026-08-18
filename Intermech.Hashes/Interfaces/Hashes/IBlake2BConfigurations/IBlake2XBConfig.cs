// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IBlake2BConfigurations.IBlake2XBConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Interfaces.Hashes.IBlake2BConfigurations;

public interface IBlake2XBConfig
{
  IBlake2BConfig Blake2BConfig { get; set; }

  IBlake2BTreeConfig Blake2BTreeConfig { get; set; }

  IBlake2XBConfig Clone();
}
