// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.BackgroundTask.TechCardBackgroundTask
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.BackgroundTask;

/// <summary>
/// Класс фоновой задачи для представления в окне фоновых задач
/// </summary>
/// <remarks>Оставил для совместимости со старым кодом</remarks>
public abstract class TechCardBackgroundTask : CustomThreadBackgroundTask
{
  /// <summary>Категория</summary>
  private string _category;

  /// <summary>Конструктор</summary>
  /// <param name="category">Наименование категории</param>
  protected TechCardBackgroundTask(string category)
    : base((Control) null)
  {
    this._category = category;
  }

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitializeData()
  {
    base.InitializeData();
    this._canStop = false;
    this._canPause = true;
    this._canResume = true;
    this._state = BackgroundTaskState.Running;
    DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._mainThreadControl = (Control) service.DocumentContainer ?? (Control) service.ActiveDockControl;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void DoThrowException(Exception e)
  {
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      service.Activate(this._category);
      service.WriteString(this._category, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_79"), (object) this._name));
      service.WriteString(this._category, string.Format(LocalizationHolder.rm.GetString(sc_19214.ssp_techcard_19215()), (object) e.Message));
      service.ShowView();
    }
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_19214.ssp_techcard_19216()), this._name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void DoWriteOutput(string text)
  {
  }
}
