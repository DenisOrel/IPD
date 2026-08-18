// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerConfiguration
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using Intermech.Threading;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

public static class DbManagerConfiguration
{
  private static AtomicInt32 _normalCommandTimeout = new AtomicInt32(300);
  private static readonly DBManagerLoggers _loggers = new DBManagerLoggers();
  private static AtomicRef<IEventLogHelper> _eventLogHelper = new AtomicRef<IEventLogHelper>((IEventLogHelper) null);

  public static int NormalCommandTimeout
  {
    [DebuggerStepThrough] get => DbManagerConfiguration._normalCommandTimeout.Value;
    [DebuggerStepThrough] set
    {
      DbManagerConfiguration._normalCommandTimeout.Value = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }
  }

  public static DBManagerLoggers Loggers
  {
    [DebuggerStepThrough] get => DbManagerConfiguration._loggers;
  }

  public static IEventLogHelper EventLogHelper
  {
    [DebuggerStepThrough] get => DbManagerConfiguration._eventLogHelper.Value;
    [DebuggerStepThrough] set => DbManagerConfiguration._eventLogHelper.Value = value;
  }
}
