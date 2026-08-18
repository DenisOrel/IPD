// Decompiled with JetBrains decompiler
// Type: Intermech.AutoUpdater.AutoUpdaterClientSettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Configuration;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoUpdater;

public sealed class AutoUpdaterClientSettings
{
  private Lazy<bool> allowAutoUpdate;
  private int serverCheckPeriod;

  public AutoUpdaterClientSettings()
  {
    this.allowAutoUpdate = new Lazy<bool>((Func<bool>) (() => AppSettingsHelper.GetBoolean("AutoExit", false)));
    this.serverCheckPeriod = (int) Math.Round(TimeSpan.FromMinutes(10.0).TotalMilliseconds);
  }

  public bool AllowAutoUpdate
  {
    [DebuggerStepThrough] get => this.allowAutoUpdate.Value;
  }

  public int ServerCheckPeriod
  {
    [DebuggerStepThrough] get => this.serverCheckPeriod;
  }
}
