// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IBlake2BConfigurations.IBlake2BConfig
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Interfaces.Hashes.IBlake2BConfigurations;

public interface IBlake2BConfig
{
  byte[] Personalisation { get; set; }

  byte[] Salt { get; set; }

  byte[] Key { get; set; }

  int HashSize { get; set; }

  IBlake2BConfig Clone();

  void Clear();
}
