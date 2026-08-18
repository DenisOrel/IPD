// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Services.SyncServices
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Services;

internal class SyncServices
{
  internal static void RegisterServices()
  {
    ApplicationServices.Container.AddService<IEventLoggerService>((IEventLoggerService) new EventLoggerService());
    ApplicationServices.Container.AddService<IDelayedEvents>((IDelayedEvents) new DelayedEvents());
    ApplicationServices.Container.AddService<IChangedTableIndexer>((IChangedTableIndexer) new ChangedTableIndexer());
  }
}
