
// Type: Intermech.Navigator.ChangingAnalyzeForm
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
/// Форма, выполняющая анализ списка изменяемых объектов в фоновом потоке
/// </summary>
public class ChangingAnalyzeForm : Form
{
  /// <summary>Действия, выполняемые над объектами</summary>
  private ObjectChangingAction action;
  /// <summary>Список изменяемых объектов</summary>
  private ChangingObjects chObjects;
  /// <summary>
  /// Уникальный идентификатор задания по анализу списка изменяемых объектов, которое выполняется на сервере
  /// </summary>
  private Guid jobID;
  /// <summary>Состояние текущей задачи</summary>
  private ChangingAnalyzerJobStatus jobStatus;
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
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="chObjects">Список изменяемых объектов</param>
  public ChangingAnalyzeForm(ObjectChangingAction action, ChangingObjects chObjects)
  {
    this.InitializeComponent();
    this.Init(action, chObjects);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="chObjects">Список изменяемых объектов</param>
  protected virtual void Init(ObjectChangingAction action, ChangingObjects chObjects)
  {
    this.action = action;
    this.chObjects = chObjects;
    this.jobID = Guid.Empty;
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.StartThread();
    this.UpdateControls();
  }

  /// <summary>Вызвать форму "Настройка интерфейса пользователя"</summary>
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="chObjects">Список изменяемых объектов</param>
  /// <returns>Результаты анализа или null, если анализ был прерван или его невозможно выполнить</returns>
  public static ChangingAnalyzerJobStatus Execute(
    ObjectChangingAction action,
    ChangingObjects chObjects)
  {
    using (ChangingAnalyzeForm changingAnalyzeForm = new ChangingAnalyzeForm(action, chObjects))
      return changingAnalyzeForm.ShowDialog() != DialogResult.OK ? (ChangingAnalyzerJobStatus) null : changingAnalyzeForm.jobStatus;
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ChangingAnalyzerForm_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ChangingAnalyzerForm_FormClosed(object sender, FormClosedEventArgs e)
  {
  }

  /// <summary>Попытка закрыть форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ChangingAnalyzerForm_FormClosing(object sender, FormClosingEventArgs e)
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
      this.thread.Name = "Navigator.ChangingAnalyzeForm";
      this.thread.Start();
    }
    this.timerRefresh.Enabled = true;
  }

  /// <summary>Фоновое обращение к серверу приложений</summary>
  protected virtual void ThreadMethod()
  {
    lock (this.lockForm)
      this.jobStatus = (ChangingAnalyzerJobStatus) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectsChangingAnalyzerService)) is IObjectsChangingAnalyzerService customService))
      {
        lock (this.lockForm)
          this.jobStatus = (ChangingAnalyzerJobStatus) null;
        this.thread = (Thread) null;
        return;
      }
      this.jobID = customService.Analyze(this.action, sessionKeeper.Session.SessionGUID, this.chObjects);
      while (!(this.jobID == Guid.Empty))
      {
        ChangingAnalyzerJobStatus analyzerJobStatus = customService.QueryJobStatus(this.jobID);
        lock (this.lockForm)
          this.jobStatus = analyzerJobStatus;
        if (analyzerJobStatus != null)
        {
          if (analyzerJobStatus.Progress != ChangingAnalyzerJobProgress.NotStarted)
          {
            if (analyzerJobStatus.Progress != ChangingAnalyzerJobProgress.Working)
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
          if (this.jobStatus.Progress != ChangingAnalyzerJobProgress.NotStarted)
          {
            if (this.jobStatus.Progress == ChangingAnalyzerJobProgress.Working)
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

  /// <summary>Прервать анализ списка изменяемых объектов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoCancelAnalyze(object sender, EventArgs e)
  {
    if (this.jobID == Guid.Empty)
      return;
    lock (this.lockForm)
    {
      this.StopThread();
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IObjectsChangingAnalyzerService)) is IObjectsChangingAnalyzerService customService)
        customService.CancelJob(this.jobID);
      this.jobStatus = (ChangingAnalyzerJobStatus) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChangingAnalyzeForm));
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
    this.Name = nameof (ChangingAnalyzeForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosing += new FormClosingEventHandler(this.ChangingAnalyzerForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ChangingAnalyzerForm_FormClosed);
    this.Load += new EventHandler(this.ChangingAnalyzerForm_Load);
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.ResumeLayout(false);
  }
}
