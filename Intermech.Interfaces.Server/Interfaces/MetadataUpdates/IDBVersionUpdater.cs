// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MetadataUpdates.IDBVersionUpdater
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Server;

#nullable disable
namespace Intermech.Interfaces.MetadataUpdates;

public interface IDBVersionUpdater
{
  bool IsNeedUpdateModule(
    IDbManager dbManager,
    IEventLogHelper eventLogHelper,
    string moduleName,
    string moduleCaption,
    int version);

  void UpdateModuleVersion(
    IDbManager dbManager,
    IEventLogHelper eventLogHelper,
    string moduleName,
    string moduleCaption,
    int version);
}
