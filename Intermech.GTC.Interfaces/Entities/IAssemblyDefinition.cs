// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.Entities.IAssemblyDefinition
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

#nullable disable
namespace Intermech.GTC.Interfaces.Entities;

public interface IAssemblyDefinition : IBaseObject
{
  string Id { get; }

  string Name { get; }

  string AssemblyType { get; }

  IItemVersion AssociatedItemVersion { get; }
}
