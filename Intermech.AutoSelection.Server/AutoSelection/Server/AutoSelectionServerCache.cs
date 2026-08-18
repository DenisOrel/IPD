// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionServerCache
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.AutoSelection.Server;

public static class AutoSelectionServerCache
{
  public static IServiceProvider ServiceProvider;
  public static IDBTimedEvents DBTimedEvents;
  public static IEventLogHelper EventLogHelper;
}
