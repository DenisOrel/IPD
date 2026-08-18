// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Kernel.Tasks.XmlExchangeImportTask
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Interfaces.XmlExchange.Services;
using Intermech.Interfaces.XmlExchange.Services.Import;
using Intermech.Localization;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Timers;

#nullable disable
namespace Intermech.XmlExchange.Client.Kernel.Tasks;

/// <summary>Класс фоновой задачи для импорта данных</summary>
internal sealed class XmlExchangeImportTask : XmlExchangeTask
{
  /// <summary>Директория для импорта данных (ZIP-архив или каталог)</summary>
  private readonly string _importDir;
  /// <summary>true - importDir указывает на файл с архивом</summary>
  private readonly bool _isZip;
  /// <summary>Идентификатор объекта с конфигурацией импорта</summary>
  private readonly long _configurationId;
  /// <summary>
  /// 
  /// </summary>
  private IXmlExchangeImportTask _xmlImportTask;
  /// <summary>
  /// 
  /// </summary>
  private readonly System.Timers.Timer _timerRefresh = new System.Timers.Timer();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  private void Write2OutputView(string message)
  {
    ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false)?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_15"), message);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void timerRefresh_Tick(object sender, EventArgs e)
  {
    if (this._xmlImportTask == null)
      return;
    XmlExchangeTaskStatus taskStatus = this._xmlImportTask?.TaskStatus;
    if (taskStatus == null)
      return;
    this._event.WaitOne();
    this.Value = (object) (taskStatus.Progress + 20);
    this.Name = $"{this._importDir} - {taskStatus.Message}";
  }

  /// <summary>Конструктор</summary>
  /// <param name="importDir">Директория для импорта данных (ZIP-архив или каталог)</param>
  /// <param name="isZip">true - importDir указывает на файл с архивом</param>
  /// <param name="configurationId">Идентификатор объекта с конфигурацией импорта</param>
  public XmlExchangeImportTask(string importDir, bool isZip, long configurationId)
  {
    this._importDir = importDir;
    this._isZip = isZip;
    this._configurationId = configurationId;
    this._thread = new Thread(new ThreadStart(((CustomThreadBackgroundTask) this).ThreadProc));
    this.Start();
  }

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitializeData()
  {
    base.InitializeData();
    this._canStop = false;
    this._canPause = false;
    this._canResume = false;
    this._value = 0;
    this._minValue = 0;
    this._maxValue = 120;
    this._name = this._category = LocalizationHolder.rm.GetString("XmlExchange.Client_15");
    this._timerRefresh.Interval = 1000.0;
    this._timerRefresh.Elapsed += new ElapsedEventHandler(this.timerRefresh_Tick);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void CustomThreadProc()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.Value = (object) 0;
      this.Name = "";
      IXmlExchangeService service = ServiceUtils.GetService<IXmlExchangeService>((object) sessionKeeper.Session, true);
      this.Write2OutputView(LocalizationHolder.rm.GetString("XmlExchange.Client_24"));
      this._xmlImportTask = service.CreateImportTask(sessionKeeper.Session.SessionGUID);
      if (this._xmlImportTask == null)
        return;
      try
      {
        try
        {
          Thread.Sleep(20);
          this._event.WaitOne();
          this.Value = (object) ((int) this.Value + 1);
          this.Name = this._importDir + " - Загрузка архива на сервер приложений ...";
          this.Write2OutputView(this.Name);
          if (this._isZip)
          {
            using (FileStream fileStream = File.OpenRead(this._importDir))
            {
              byte[] buffer = new byte[524288 /*0x080000*/];
              for (int bufferSize = fileStream.Read(buffer, 0, buffer.Length); bufferSize > 0; bufferSize = fileStream.Read(buffer, 0, buffer.Length))
                this._xmlImportTask.UploadData(this._xmlImportTask.TaskGuid.ToString() + ".zip", buffer, bufferSize, true);
            }
            this._timerRefresh.Start();
            Thread.Sleep(20);
            this._event.WaitOne();
            this.Name = this._importDir + " - Запуск задачи импорта ...";
            this.Write2OutputView(this.Name);
            this._xmlImportTask.Execute(new XmlExchangeImportTaskParams(this._configurationId));
          }
          this.Name = !this._xmlImportTask.HasError ? this._importDir + " - Импорт успешно завершён" : this._importDir + " - Импорт прерван из-за ошибки (детальное описание в протоколе серверной задачи)";
          this.Write2OutputView(this.Name);
        }
        finally
        {
          this._timerRefresh.Stop();
          string log = this._xmlImportTask.Log;
          string str = Environment.NewLine + LocalizationHolder.rm.GetString("XmlExchange.Client_25") + Environment.NewLine + Environment.NewLine + log;
          this.Write2OutputView(LocalizationHolder.rm.GetString("XmlExchange.Client_25"));
          this.Write2OutputView(log);
          if (this._xmlImportTask.HasError)
            throw new TargetInvocationException(LocalizationHolder.rm.GetString("XmlExchange.Client_26") + str, this._xmlImportTask.Exception);
          this.Value = (object) this.MaximumValue;
        }
      }
      finally
      {
        service.DisposeImportTask(this._xmlImportTask.TaskGuid);
        this._xmlImportTask = (IXmlExchangeImportTask) null;
      }
    }
  }
}
