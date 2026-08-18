// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ViewQueueFilePresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ViewQueueFilePresenter : ExtendedPresenter<IQueueFileView>
{
  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.ShowToast(LocalizationHolder.rm.GetString("SR_259"));
  }

  protected override void OnStartBackgroundTask()
  {
    base.OnStartBackgroundTask();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetFilesTask), (object) null);
  }

  private void GetFilesTask(object state0)
  {
    try
    {
      QueueFile queueFile = QueueFileServices.ReadQueue();
      IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
      List<Tuple<string, int>> files = new List<Tuple<string, int>>(1024 /*0x0400*/);
      List<string> fileNames = new List<string>(1024 /*0x0400*/);
      int stageIndex = 0;
      foreach (IImportStage importStage in (IEnumerable<IImportStage>) queueFile.ImportStages)
      {
        fileNames.Clear();
        foreach (ICollection<string> bucket in (IEnumerable<ICollection<string>>) importStage.Buckets)
          fileNames.AddRange((IEnumerable<string>) bucket);
        List<FileOrigin> fileOrigins = service.WorkArea.GetFileOrigins((IList<string>) fileNames, true);
        fileOrigins.RemoveAll((Predicate<FileOrigin>) (origin => origin.OriginType != 0));
        files.AddRange((IEnumerable<Tuple<string, int>>) fileOrigins.ConvertAll<Tuple<string, int>>((Converter<FileOrigin, Tuple<string, int>>) (origin => new Tuple<string, int>(origin.FileName, stageIndex))));
        stageIndex++;
      }
      this.SendToViewThread((Action) (() =>
      {
        this.View.SetFileList((ICollection<Tuple<string, int>>) files);
        this.View.HideToast();
      }));
    }
    catch (ThreadAbortException ex)
    {
    }
    catch (Exception ex)
    {
      this.SendToViewThread((Action) (() =>
      {
        this.View.ShowToast(LocalizationHolder.rm.GetString("SR_293"));
        MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(string.Format(LocalizationHolder.rm.GetString("SR_291"), (object) ex.Message), LocalizationHolder.rm.GetString("Tools.Client_209"), MessageIcon.Error));
      }));
    }
  }
}
