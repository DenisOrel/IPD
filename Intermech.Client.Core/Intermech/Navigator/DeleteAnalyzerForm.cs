
// Type: Intermech.Navigator.DeleteAnalyzerForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Форма, выполняющая анализ списка удаляемых объектов в фоновом потоке
/// </summary>
public class DeleteAnalyzerForm : Form
{
  /// <summary>Параметры</summary>
  private DeleteAnalyzerOptions options;
  /// <summary>Список удаляемых объектов</summary>
  private DeletingObjects delObjects;
  /// <summary>
  /// Уникальный идентификатор задания по анализу списка удаляемых объектов, которое выполняется на сервере
  /// </summary>
  private Guid jobID;
  /// <summary>Состояние текущей задачи</summary>
  private DeleteAnalyzerJobStatus jobStatus;
  /// <summary>
  /// Объект для потокобезопасного доступа к переменным при фоновом обращении к статусу задания
  /// </summary>
  private object lockForm = (object) new Guid();
  /// <summary>
  /// Фоновый поток, в рамках которого выполняется обращение к серверу за статусом задания
  /// </summary>
  private Thread thread;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private System.Windows.Forms.Timer timerRefresh;
  private Button btnCancel;
  private Label labelInfo;
  private PictureBox pictureInfo;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="delObjects">Список удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  public DeleteAnalyzerForm(DeletingObjects delObjects, DeleteAnalyzerOptions options)
  {
    this.InitializeComponent();
    this.Init(delObjects, options);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="delObjects">Список удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  protected virtual void Init(DeletingObjects delObjects, DeleteAnalyzerOptions options)
  {
    this.delObjects = delObjects;
    this.jobID = Guid.Empty;
    this.options = options;
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.StartThread();
    this.UpdateControls();
  }

  /// <summary>Вызвать форму "Настройка интерфейса пользователя"</summary>
  /// <param name="delObjects">Список удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  /// <returns>Результаты анализа или null, если анализ был прерван или его невозможно выполнить</returns>
  public static DeleteAnalyzerJobStatus Execute(
    DeletingObjects delObjects,
    DeleteAnalyzerOptions options)
  {
    using (DeleteAnalyzerForm deleteAnalyzerForm = new DeleteAnalyzerForm(delObjects, options))
      return deleteAnalyzerForm.ShowDialog() != DialogResult.OK ? (DeleteAnalyzerJobStatus) null : deleteAnalyzerForm.jobStatus;
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteAnalyzerForm_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteAnalyzerForm_FormClosed(object sender, FormClosedEventArgs e)
  {
  }

  /// <summary>Попытка закрыть форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteAnalyzerForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
      return;
    this.DoCancelAnalyze(sender, (EventArgs) null);
  }

  /// <summary>
  /// Остановить фоновый поток, обращающийся к серверу приложений
  /// </summary>
  private void StopThread()
  {
    if (this.thread != null)
      this.thread.Abort();
    this.thread = (Thread) null;
  }

  /// <summary>
  /// Запустить фоновый поток, обращающийся к серверу приложений
  /// </summary>
  private void StartThread()
  {
    this.StopThread();
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this.thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(this.ThreadMethod)));
      this.thread.IsBackground = true;
      this.thread.Name = "Navigator.DeleteAnalyzerForm";
      this.thread.Start();
    }
    this.timerRefresh.Enabled = true;
  }

  /// <summary>Фоновое обращение к серверу приложений</summary>
  protected virtual void ThreadMethod()
  {
    lock (this.lockForm)
      this.jobStatus = (DeleteAnalyzerJobStatus) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService customService))
      {
        lock (this.lockForm)
          this.jobStatus = (DeleteAnalyzerJobStatus) null;
        this.thread = (Thread) null;
        return;
      }
      this.jobID = customService.Analyze(sessionKeeper.Session.SessionGUID, this.delObjects, this.options);
      while (!(this.jobID == Guid.Empty))
      {
        DeleteAnalyzerJobStatus analyzerJobStatus = customService.QueryJobStatus(this.jobID);
        lock (this.lockForm)
          this.jobStatus = analyzerJobStatus;
        if (analyzerJobStatus != null)
        {
          if (analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.NotStarted)
          {
            if (analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Working)
              break;
          }
          Thread.Sleep(1000);
        }
        else
          break;
      }
    }
    this.thread = (Thread) null;
  }

  /// <summary>События от таймера</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void timerRefresh_Tick(object sender, EventArgs e)
  {
    this.timerRefresh.Enabled = false;
    lock (this.lockForm)
    {
      if (this.thread != null)
      {
        if (this.jobStatus != null)
        {
          if (this.jobStatus.Progress != DeleteAnalyzerJobProgress.NotStarted)
          {
            if (this.jobStatus.Progress == DeleteAnalyzerJobProgress.Working)
              goto label_9;
          }
          else
            goto label_9;
        }
        else
          goto label_9;
      }
      this.StopThread();
      this.DialogResult = DialogResult.OK;
      return;
    }
label_9:
    this.timerRefresh.Enabled = true;
  }

  /// <summary>Прервать анализ списка удаляемых объектов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoCancelAnalyze(object sender, EventArgs e)
  {
    if (this.jobID == Guid.Empty)
      return;
    lock (this.lockForm)
    {
      this.StopThread();
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService customService)
        customService.CancelJob(this.jobID);
      this.jobStatus = (DeleteAnalyzerJobStatus) null;
      this.jobID = Guid.Empty;
      if (e == null)
        return;
      this.DialogResult = DialogResult.Cancel;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteAnalyzerForm));
    this.timerRefresh = new System.Windows.Forms.Timer(this.components);
    this.btnCancel = new Button();
    this.labelInfo = new Label();
    this.pictureInfo = new PictureBox();
    ((ISupportInitialize) this.pictureInfo).BeginInit();
    this.SuspendLayout();
    this.timerRefresh.Interval = 1000;
    this.timerRefresh.Tick += new EventHandler(this.timerRefresh_Tick);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.DoCancelAnalyze);
    componentResourceManager.ApplyResources((object) this.labelInfo, "labelInfo");
    this.labelInfo.Name = "labelInfo";
    componentResourceManager.ApplyResources((object) this.pictureInfo, "pictureInfo");
    this.pictureInfo.Name = "pictureInfo";
    this.pictureInfo.TabStop = false;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.pictureInfo);
    this.Controls.Add((Control) this.labelInfo);
    this.Controls.Add((Control) this.btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DeleteAnalyzerForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosing += new FormClosingEventHandler(this.DeleteAnalyzerForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.DeleteAnalyzerForm_FormClosed);
    this.Load += new EventHandler(this.DeleteAnalyzerForm_Load);
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.ResumeLayout(false);
  }
}
