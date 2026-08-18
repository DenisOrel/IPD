// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.CompositionTaskActionBase
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.TechCard.Client.Classes.Tasks;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>Базовое действие задачи по развороту состава</summary>
internal abstract class CompositionTaskActionBase : IAsyncTaskAction<ObjInfoItem, DataTable>
{
  /// <summary>
  /// 
  /// </summary>
  private AsyncTaskBase<ObjInfoItem, DataTable> _techcardAsyncTask;
  /// <summary>
  /// 
  /// </summary>
  protected ObjInfoItem _objInfoItem;
  /// <summary>Guid фоновой задачи по разворачиванию состава</summary>
  protected readonly Guid _taskGuid = Guid.NewGuid();

  /// <summary>
  /// Развернуть состав конструкторской сборочной единицы (КСЕ)
  /// </summary>
  private DataTable DoExecute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionService compositionService = this.StartCompositionService(sessionKeeper.Session);
      if (compositionService == null)
        return (DataTable) null;
      DataTable dataTable = (DataTable) null;
      CompositionInfo info;
      do
      {
        Task.Delay(500);
        if (this._techcardAsyncTask.CancellationSource.IsCancellationRequested)
        {
          compositionService.CancelSelect(this._taskGuid);
          return (DataTable) null;
        }
        info = compositionService.GetInfo(this._taskGuid);
        if (info != null)
        {
          this._techcardAsyncTask.OnProgressChanged(new ProgressChangedEventArgs(info.Percent, (object) null));
          dataTable = info.Result as DataTable;
        }
      }
      while (info != null && !info.ErrorPresent && info.Percent < 100);
      if (info != null)
      {
        int num = info.ErrorPresent ? 1 : 0;
      }
      return dataTable;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected abstract ICompositionService StartCompositionService(IUserSession session);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="techcardAsyncTask"></param>
  /// <param name="objInfoItem"></param>
  /// <returns></returns>
  public DataTable Execute(
    AsyncTaskBase<ObjInfoItem, DataTable> techcardAsyncTask,
    ObjInfoItem objInfoItem)
  {
    this._objInfoItem = objInfoItem;
    this._techcardAsyncTask = techcardAsyncTask;
    this._techcardAsyncTask.OnProgressChanged(new ProgressChangedEventArgs(0, (object) null));
    try
    {
      return ObjInfoItem.IsEmpty((ITypedInfoItem) this._objInfoItem) || this._techcardAsyncTask.CancellationSource.IsCancellationRequested ? (DataTable) null : this.DoExecute();
    }
    finally
    {
      this._techcardAsyncTask.OnProgressChanged(new ProgressChangedEventArgs(100, (object) null));
    }
  }
}
