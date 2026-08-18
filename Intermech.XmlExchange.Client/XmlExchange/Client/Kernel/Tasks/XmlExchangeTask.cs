// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Kernel.Tasks.XmlExchangeTask
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.Client.Kernel.Tasks;

/// <summary>
/// Базовый класс для фоновых задач импорта / экспорта XML данных
/// </summary>
internal abstract class XmlExchangeTask : CustomThreadBackgroundTask
{
  /// <summary>Флаг наличия ошибок / сообщений</summary>
  private bool _hasErrors;
  /// <summary>Категория процесса (для вывода в IOutputView)</summary>
  protected string _category = string.Empty;

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitializeData()
  {
    base.InitializeData();
    this._canStop = true;
    this._canPause = true;
    this._canResume = true;
    this._canTerminate = true;
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
    ExceptionHelper.ExceptionService.ShowException(e);
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.WriteString(this._category, string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_5"), (object) this._name));
    service.WriteString(this._category, string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_6"), (object) e.Message));
    service.WriteString(this._category, e.StackTrace);
    service.Activate(this._category);
    service.ShowView();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void DoWriteOutput(string text)
  {
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.WriteString(this._category, text);
    service.Activate(this._category);
    if (this._hasErrors)
      return;
    service.ShowView();
    this._hasErrors = true;
  }
}
