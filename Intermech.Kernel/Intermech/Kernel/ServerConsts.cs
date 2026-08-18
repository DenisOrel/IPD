// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerConsts
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Data;


namespace Intermech.Kernel;

public class ServerConsts
{
  internal const string CreateObjectLogFileName = "CreateObject.log";
  internal static bool CreateObjectLogging = false;
  internal static bool CreateRelationLogging = false;
  internal static bool MandateAccess = true;
  internal static bool BackupEventlogRecords = false;
  internal const int DelayedUpdaterInterval = 1000;
  internal const int AddToTraceInterval = 25;
  internal static bool AutomaticAccessLevelUp = false;
  internal static bool EnableSecret2Public = false;
  internal static bool CopyAuthenticalFiles = false;
  internal static volatile bool SendAttrs2DelayedNotificationMode = false;
  internal static volatile bool AnnulAllVersions = true;
  private static string _ShortenedConnectionString = string.Empty;
  internal static bool OldUniqueAttributesCheck = false;
  internal static int ServerAliveUpdatePeriod = 6;
  internal static int ServerDeadPeriod = 48 /*0x30*/;
  public static int RemotingServerPort = 0;
  internal static bool UseSearchWorkcopyFiles = false;
  internal static int SessionSmartCacheTime = 2;
  internal static int PeakMemoryUsageNotify = 0;
  internal static int MaxDataTableRowsCount = 200000;
  internal static volatile bool CheckAttributeLCStepSecurity = false;
  internal static readonly TimeSpan OldSessionsInactivityInterval = TimeSpan.FromHours(12.0);
  internal static readonly TimeSpan OldSessionsCheckInterval = TimeSpan.FromHours(1.0);
  internal static volatile int WrongPasswordsLimit = 0;
  internal static volatile char CryptMethod = CryptHelper.SHA1Crypt;
  internal static string IndexTablespaceName = string.Empty;
  public static bool CopyProjectVisibility = false;
  public static bool CopyArcVisibility = false;
  internal static bool EnableSyncCheckin = false;
  internal static bool SetProjectOnCreateRelation = true;

  public static string ShortenedConnectionString
  {
    get => ServerConsts._ShortenedConnectionString;
    internal set
    {
      ServerConsts._ShortenedConnectionString = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public static void ValidateTableMaxRows(DataTable tbl)
  {
    if (tbl.Rows.Count > ServerConsts.MaxDataTableRowsCount)
      throw new KernelException("Попытка получить слишком большой объем данных из базы. Операция прервана.");
  }
}
