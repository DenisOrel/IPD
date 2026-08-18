// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileStatisticsPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Settings;
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class FileStatisticsPresenter : ExtendedPresenter<IFileStatisticsView>
{
  private string rootDirectory;

  public string RootDirectory
  {
    get => this.rootDirectory;
    set
    {
      this.CheckAllowPropertyChange();
      this.rootDirectory = value;
    }
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.ToggleProgressBar(true);
    this.View.SetMessage(LocalizationHolder.rm.GetString("SR_259"));
    this.View.SetTotalFiles(LocalizationHolder.rm.GetString("SR_289"));
    this.View.SetImportedFiles(LocalizationHolder.rm.GetString("SR_289"));
    this.View.SetInProgressFiles(LocalizationHolder.rm.GetString("SR_289"));
    this.View.SetNotImportedFiles(LocalizationHolder.rm.GetString("SR_289"));
  }

  protected override void OnStartBackgroundTask()
  {
    base.OnStartBackgroundTask();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.CollectStatisticsInBackground), (object) null);
  }

  private void CollectStatisticsInBackground(object state0)
  {
    try
    {
      List<string> fileNames = new List<string>(1024 /*0x0400*/);
      foreach (string safeEnumerateFile in ArchiveFiles.SafeEnumerateFiles(this.rootDirectory))
        fileNames.Add(PathUtils.GetRelativePath(safeEnumerateFile, (string) (ValueCell<string>) ArchiveParameters.Common.Location, RelativePathOptions.ThrowIfNotPossible));
      PathCollection lockedFiles = LockFileServices.GetLockedFiles();
      List<FileOrigin> fileOrigins = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).WorkArea.GetFileOrigins((IList<string>) fileNames, true);
      int allFiles = 0;
      int imported = 0;
      int inProgress = 0;
      int notImported = 0;
      foreach (FileOrigin fileOrigin in fileOrigins)
      {
        allFiles++;
        if (fileOrigin.OriginType == FileOriginType.WorkFile || fileOrigin.OriginType == FileOriginType.DetachedFile)
          imported++;
        else if (lockedFiles.Contains(fileOrigin.FileName))
          inProgress++;
        else
          notImported++;
      }
      this.SendToViewThread((Action) (() => this.DisplayStatistics(allFiles, imported, inProgress, notImported)));
    }
    catch (ThreadAbortException ex)
    {
    }
    catch (Exception ex)
    {
      this.SendToViewThread((Action) (() => this.DisplayError(ex)));
    }
  }

  private void DisplayStatistics(int allFiles, int imported, int inProgress, int notImported)
  {
    this.View.ToggleProgressBar(false);
    this.View.SetMessage(string.Format(LocalizationHolder.rm.GetString("SR_290"), (object) (100.0 * (double) imported / (double) allFiles)));
    this.View.SetTotalFiles(allFiles.ToString());
    this.View.SetImportedFiles(imported.ToString());
    this.View.SetInProgressFiles(inProgress.ToString());
    this.View.SetNotImportedFiles(notImported.ToString());
  }

  private void DisplayError(Exception x)
  {
    this.View.ToggleProgressBar(false);
    this.View.SetMessage(string.Format(LocalizationHolder.rm.GetString("SR_291"), (object) x.Message));
    this.View.SetTotalFiles(LocalizationHolder.rm.GetString("SR_292"));
    this.View.SetImportedFiles(LocalizationHolder.rm.GetString("SR_292"));
    this.View.SetInProgressFiles(LocalizationHolder.rm.GetString("SR_292"));
    this.View.SetNotImportedFiles(LocalizationHolder.rm.GetString("SR_292"));
  }
}
