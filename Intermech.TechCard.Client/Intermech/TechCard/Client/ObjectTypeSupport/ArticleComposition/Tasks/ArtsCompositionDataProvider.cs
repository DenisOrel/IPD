// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.ArtsCompositionDataProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Localization;
using Intermech.TechCard.Client.Classes.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>Провайдер данных для собираемых</summary>
public class ArtsCompositionDataProvider
{
  /// <summary>
  /// 
  /// </summary>
  private ObjInfoItem _artObjInfo;
  /// <summary>
  /// 
  /// </summary>
  private ObjInfoItem _techObjInfo;
  /// <summary>
  /// 
  /// </summary>
  private readonly AsyncTaskBase<ObjInfoItem, DataTable> _taskDesign;
  /// <summary>
  /// 
  /// </summary>
  private readonly AsyncTaskBase<ObjInfoItem, DataTable> _taskTech;
  /// <summary>
  /// Процент выполнения задания по разворачиванию состава КСЕ
  /// </summary>
  private int _percentDesign;
  /// <summary>
  /// Процент выполнения задания по разворачиванию состава ТП
  /// </summary>
  private int _percentTech;
  /// <summary>
  /// Класс, который позволяет принудительно включать допустимые замены в составе
  /// </summary>
  internal static readonly ArtsCompositionTaskDataTransfer PluginData = new ArtsCompositionTaskDataTransfer(new long[2]
  {
    0L,
    1L
  }, new long[2]{ 0L, 2L }, new long[3]{ 0L, 1L, 2L });

  /// <summary>
  /// 
  /// </summary>
  private void DoBeforeLoadData()
  {
    EventHandler beforeLoadData = this.BeforeLoadData;
    if (beforeLoadData == null)
      return;
    beforeLoadData((object) this, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DoAfterLoadData()
  {
    EventHandler afterLoadData = this.AfterLoadData;
    if (afterLoadData == null)
      return;
    afterLoadData((object) this, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="progressValue"></param>
  private void DoProcessChanged(int progressValue)
  {
    ProgressChangedEventHandler progressChanged = this.ProgressChanged;
    if (progressChanged == null)
      return;
    progressChanged((object) this, new ProgressChangedEventArgs(progressValue, (object) null));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  private void DoThrowException(Exception e)
  {
    ExceptionHelper.ExceptionService.ShowException(e);
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string category = LocalizationHolder.rm.GetString("TechCard.Client_390");
    service.Activate(category);
    service.WriteString(category, string.Format(LocalizationHolder.rm.GetString(sc_19423.ssp_techcard_19424()), (object) e.Message));
    service.ShowView();
  }

  /// <summary>Прервать разворачивание состава КСЕ</summary>
  private void CancelLoadDesignData()
  {
    this._percentDesign = 0;
    this._taskDesign.CancellationSource.Cancel();
    this.TableDesign = (DataTable) null;
  }

  /// <summary>Стартовать разворачивание состава КСЕ</summary>
  private Task StartLoadDesignData()
  {
    if (this.LoadedDesignData)
      return (Task) null;
    this.CancelLoadDesignData();
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this._taskDesign.EditingContext = fixEditingContext.EditingContext;
      return this._taskDesign.Execute(this._artObjInfo);
    }
  }

  /// <summary>Прервать разворачивание состава ТП</summary>
  private void CancelLoadTechData()
  {
    this._percentTech = 0;
    this._taskTech.CancellationSource.Cancel();
    this.TableTech = (DataTable) null;
  }

  /// <summary>Стартовать разворачивание состава ТП</summary>
  private Task StartLoadTechData()
  {
    if (this.LoadedTechData)
      return (Task) null;
    this.CancelLoadTechData();
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this._taskTech.EditingContext = fixEditingContext.EditingContext;
      return this._taskTech.Execute(this._techObjInfo);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal ArtsCompositionDataProvider(
    AsyncTaskBase<ObjInfoItem, DataTable> taskDesign,
    AsyncTaskBase<ObjInfoItem, DataTable> taskTech)
  {
    this._taskDesign = taskDesign;
    this._taskDesign.HandleException += new ExceptionHandler(this.OnTaskHandleException);
    this._taskDesign.ProgressChanged += new ProgressChangedEventHandler(this.OnTaskDesignProgressChanged);
    this._taskDesign.TaskCompleted += new AsyncTaskBase<ObjInfoItem, DataTable>.TaskCompletedEventHandler(this.OnTaskDesignCompleted);
    this._taskTech = taskTech;
    this._taskTech.HandleException += new ExceptionHandler(this.OnTaskHandleException);
    this._taskTech.ProgressChanged += new ProgressChangedEventHandler(this.OnTaskTechProgressChanged);
    this._taskTech.TaskCompleted += new AsyncTaskBase<ObjInfoItem, DataTable>.TaskCompletedEventHandler(this.OnTaskTechCompleted);
  }

  /// <summary>
  /// 
  /// </summary>
  public void StartLoadData(ObjInfoItem artObjInfo, ObjInfoItem techInfoItem)
  {
    this._artObjInfo = artObjInfo;
    this._techObjInfo = techInfoItem;
    this.DoBeforeLoadData();
    List<Task> taskList = new List<Task>();
    Task task1 = this.StartLoadTechData();
    if (task1 != null)
      taskList.Add(task1);
    Task task2 = this.StartLoadDesignData();
    if (task2 != null)
      taskList.Add(task2);
    Task[] array = taskList.ToArray();
    bool flag = false;
    try
    {
      Task.WhenAll(array).ContinueWith((Action<Task>) (t =>
      {
        if (t.Exception != null)
          return;
        this.TableTech = this.TableTech ?? this._taskTech.Result;
        this.TableDesign = this.TableDesign ?? this._taskDesign.Result;
      })).Wait();
    }
    catch (Exception ex)
    {
      if (ex is TaskCanceledException)
        flag = true;
      else
        throw;
    }
    if (flag)
      return;
    this.DoAfterLoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  public void CancelLoadData()
  {
    this.DoProcessChanged(0);
    this.CancelLoadDesignData();
    this.CancelLoadTechData();
  }

  /// <summary>Развёрнутый состав изделия</summary>
  public DataTable TableDesign { get; private set; }

  /// <summary>Развёрнутый состав ТП</summary>
  public DataTable TableTech { get; private set; }

  /// <summary>
  /// Признак наличия загруженных данных / необходимость обновления состава ТП
  /// </summary>
  public bool LoadedTechData { get; set; }

  /// <summary>
  /// Признак наличия загруженных данных / необходимость обновления состава КСЕ
  /// </summary>
  public bool LoadedDesignData { get; set; }

  /// <summary>Вызывается при изменении прогресса задачи</summary>
  public event ProgressChangedEventHandler ProgressChanged;

  /// <summary>Вызывается перед началом работы задачи</summary>
  public event EventHandler BeforeLoadData;

  /// <summary>Вызывается после окончания работы задачи</summary>
  public event EventHandler AfterLoadData;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  private void OnTaskTechCompleted(
    object sender,
    AsyncTaskBase<ObjInfoItem, DataTable>.TaskCompleteEventArgs args)
  {
    this.TableTech = args.Result;
    this.LoadedTechData = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTaskTechProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this._percentTech = e.ProgressPercentage;
    this.DoProcessChanged((this._percentDesign + this._percentTech) / 2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTaskHandleException(object sender, ExceptionEventArgs e)
  {
    this.DoThrowException(e.Exception);
  }

  private void OnTaskDesignCompleted(
    object sender,
    AsyncTaskBase<ObjInfoItem, DataTable>.TaskCompleteEventArgs args)
  {
    this.TableDesign = args.Result;
    this.LoadedDesignData = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTaskDesignProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this._percentDesign = e.ProgressPercentage;
    this.DoProcessChanged((this._percentDesign + this._percentTech) / 2);
  }
}
