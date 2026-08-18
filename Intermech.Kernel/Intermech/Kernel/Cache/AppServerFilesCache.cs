// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Cache.AppServerFilesCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using Intermech.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;


namespace Intermech.Kernel.Cache;

public sealed class AppServerFilesCache : IAppServerFilesCache
{
  private ConcurrentDictionary<string, DateTime> _FilesDict = new ConcurrentDictionary<string, DateTime>();
  private int _FilesLifetime = 72;
  private int _ClearInterval = 2;
  private FilesStorage _isoFileStorage;
  private const string skipClearFileCacheParam = "skipclearfilecache";

  public AppServerFilesCache(FilesStorage isoFile)
  {
    this._isoFileStorage = isoFile;
    string s1 = ConfigurationManager.AppSettings.Get("FileCacheLifetime");
    if (s1 != null)
      int.TryParse(s1, out this._FilesLifetime);
    string s2 = ConfigurationManager.AppSettings.Get("FileCacheClearInterval");
    if (s2 != null)
      int.TryParse(s2, out this._ClearInterval);
    bool flag = true;
    foreach (string commandLineArg in Environment.GetCommandLineArgs())
    {
      if (commandLineArg.ToLower().Contains("skipclearfilecache"))
      {
        flag = false;
        break;
      }
    }
    if (ConfigurationManager.AppSettings.Get("DisableClearFilesCache") == "1")
      flag = false;
    if (flag)
      this.ClearServerCache();
    if (this._ClearInterval <= 0)
      return;
    new Thread(new ThreadStart(this.ProcessCacheClearing))
    {
      Name = "File Cache clearing thread",
      IsBackground = true
    }.Start();
  }

  public FilesStorage FStorage => this._isoFileStorage;

  private void ProcessCacheClearing()
  {
    Thread.Sleep(TimeSpan.FromHours((double) this._FilesLifetime));
    while (true)
    {
      try
      {
        this.DeleteOldFiles();
        Thread.Sleep(TimeSpan.FromHours((double) this._ClearInterval));
      }
      catch (Exception ex)
      {
        if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
        {
          service.AddToTrace("Ошибка удаления устаревших файлов в кэше сервера приложений: " + ex.Message, Intermech.Consts.traceAlways);
          service.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways);
        }
        Thread.Sleep(TimeSpan.FromHours((double) this._ClearInterval));
      }
    }
  }

  public void AddFile(string fileName)
  {
    this._FilesDict.AddOrUpdate(fileName, DateTime.UtcNow, (Func<string, DateTime, DateTime>) ((key, oldValue) => DateTime.UtcNow));
  }

  public void DeleteOldFiles()
  {
    List<string> stringList = new List<string>();
    foreach (KeyValuePair<string, DateTime> keyValuePair in this._FilesDict.Where<KeyValuePair<string, DateTime>>((Func<KeyValuePair<string, DateTime>, bool>) (keyValue => keyValue.Value < DateTime.UtcNow - TimeSpan.FromHours((double) this._FilesLifetime))))
      stringList.Add(keyValuePair.Key);
    foreach (string str in stringList)
    {
      try
      {
        if (this._isoFileStorage.FileExists(str))
          this._isoFileStorage.DeleteFile(str);
        this._FilesDict.TryRemove(str, out DateTime _);
      }
      catch
      {
      }
    }
  }

  public void ClearServerCache()
  {
    string[] fileNames = this._isoFileStorage.GetFileNames("*");
    for (int index = 0; index < fileNames.Length; ++index)
    {
      try
      {
        this._isoFileStorage.DeleteFile(fileNames[index]);
      }
      catch (Exception ex)
      {
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Error deleting file {fileNames[index]} from isolated storage: {ex.Message}", Intermech.Consts.traceWarning, "");
      }
    }
  }
}
