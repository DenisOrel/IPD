// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IMetadataUpdateKeeper
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces;

public interface IMetadataUpdateKeeper : IDisposable
{
  [NotNull]
  string ModuleName { get; }

  int UpdateVersion { get; }

  int UpdateRevision { get; }

  bool SaveNewDbVersion { get; }

  [CanBeNull]
  ITransactionKeeper Transaction { get; }
}
