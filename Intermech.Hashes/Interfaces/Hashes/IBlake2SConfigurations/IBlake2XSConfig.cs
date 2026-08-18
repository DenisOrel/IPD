// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IBlake2SConfigurations.IBlake2XSConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Interfaces.Hashes.IBlake2SConfigurations;

public interface IBlake2XSConfig
{
  IBlake2SConfig Blake2SConfig { get; set; }

  IBlake2STreeConfig Blake2STreeConfig { get; set; }

  IBlake2XSConfig Clone();
}
