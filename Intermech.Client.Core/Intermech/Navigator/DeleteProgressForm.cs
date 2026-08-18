
// Type: Intermech.Navigator.DeleteProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Форма, выполняющая удаление объектов в фоновом потоке</summary>
public class DeleteProgressForm : Form
{
  /// <summary>Описания удаляемых объектов</summary>
  private DeletingObjects items;
  /// <summary>Фоновый поток для удаления объектов</summary>
  private Thread thread;
  /// <summary>
  /// Уникальный идентификатор задания по анализу списка удаляемых объектов, которое выполняется на сервере
  /// </summary>
  private Guid jobID;
  /// <summary>Состояние текущей задачи</summary>
  private DeleteObjectsJobStatus jobStatus;
  /// <summary>
  /// Объект для потокобезопасного доступа к переменным при фоновом обращении к статусу задания
  /// </summary>
  private object lockForm = (object) new Guid();
  /// <summary>Список с изображениями</summary>
  private List<PictureBox> pictures = new List<PictureBox>();
  /// <summary>Текущий индекс рисунка</summary>
  private int pictureIndex;
  /// <summary>Текущий рисунок</summary>
  private PictureBox currPicture;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pictureInfo0;
  private Label labelInfo;
  private Button btnCancel;
  private ProgressBar progressBar;
  private System.Windows.Forms.Timer timerRefresh;
  private PictureBox pictureInfo1;
  private PictureBox pictureInfo2;
  private PictureBox pictureInfo4;
  private PictureBox pictureInfo3;
  private PictureBox pictureInfo6;
  private PictureBox pictureInfo5;
  private System.Windows.Forms.Timer timerAnimation;
  private StatusStrip statusBar;
  private ToolStripStatusLabel labelObjectesDeleted;
  private ToolStripStatusLabel labelObjectesDeletedCount;
  private ToolStripStatusLabel labelObjectsSkipped;
  private ToolStripStatusLabel labelSkippedCount;
  private ToolStripStatusLabel labelRelationsDeleted;
  private ToolStripStatusLabel labelRelationsDeletedCount;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="items">Коллекция идентификаторов версий удаляемых объектов</param>
  public DeleteProgressForm(DeletingObjects items)
  {
    this.InitializeComponent();
    this.Init(items);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="items">Коллекция описаний удаляемых объектов</param>
  protected virtual void Init(DeletingObjects items)
  {
    this.items = items;
    this.jobID = Guid.Empty;
    this.progressBar.Value = 0;
    this.progressBar.Maximum = items.Count;
    this.pictures.Add(this.pictureInfo0);
    this.pictures.Add(this.pictureInfo1);
    this.pictures.Add(this.pictureInfo2);
    this.pictures.Add(this.pictureInfo3);
    this.pictures.Add(this.pictureInfo4);
    this.pictures.Add(this.pictureInfo5);
    this.pictures.Add(this.pictureInfo6);
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.StartThread();
    this.UpdateControls();
  }

  /// <summary>Вызвать форму "Удаление объектов"</summary>
  /// <param name="items">Коллекция идентификаторов версий удаляемых объектов</param>
  /// <returns>Список идентификаторов удалённых объектов</returns>
  public static DeleteObjectsJobStatus Execute(DeletingObjects items)
  {
    using (DeleteProgressForm deleteProgressForm = new DeleteProgressForm(items))
    {
      int num = (int) deleteProgressForm.ShowDialog();
      return deleteProgressForm.jobStatus;
    }
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteProgressForm_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteProgressForm_FormClosed(object sender, FormClosedEventArgs e)
  {
  }

  /// <summary>Попытка закрыть форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteProgressForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
      return;
    this.btnCancel_Click(sender, (EventArgs) null);
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
    this.thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.thread.IsBackground = true;
    this.thread.Name = "Navigator.DeleteObjectsForm";
    this.thread.Start();
    this.timerRefresh.Enabled = true;
    this.timerAnimation.Enabled = true;
  }

  /// <summary>Фоновое обращение к серверу приложений</summary>
  protected virtual void ThreadMethod()
  {
    lock (this.lockForm)
      this.jobStatus = (DeleteObjectsJobStatus) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService))
      {
        lock (this.lockForm)
          this.jobStatus = (DeleteObjectsJobStatus) null;
        this.thread = (Thread) null;
        return;
      }
      this.jobID = customService.Delete(sessionKeeper.Session.SessionGUID, this.items, DeleteObjectsJobMode.AscOnError);
      DeleteObjectsJobStatus jobStat = (DeleteObjectsJobStatus) null;
      while (!(this.jobID == Guid.Empty))
      {
        jobStat = customService.QueryJobStatus(this.jobID);
        lock (this.lockForm)
          this.jobStatus = jobStat;
        if (jobStat != null)
        {
          if (jobStat.Progress == DeleteObjectsJobProgress.Idle)
          {
            IInvokeService service = ServicesManager.GetService(typeof (IInvokeService)) as IInvokeService;
            bool flag = true;
            while (flag)
            {
              if (this.items.Count == 1 && jobStat.Exception != null)
              {
                service.InvokeAction(-1, (Action) (() => ExceptionHelper.ExceptionService.ShowException(jobStat.Exception)));
                jobStat = customService.CancelJob(this.jobID);
                break;
              }
              flag = false;
              List<IMMessageBoxButton> buttons = new List<IMMessageBoxButton>();
              buttons.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_578"), DialogResult.Abort));
              buttons.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_579"), DialogResult.Ignore));
              buttons.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_580"), DialogResult.Retry));
              if (jobStat.Exception != null)
                buttons.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_581"), DialogResult.No));
              DialogResult dialogResult = (DialogResult) service.InvokeFunc<object>(-1, (Func<object>) (() => IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_582"), buttons.ToArray(), IMMessageBoxImage.Question, (Form) this)));
              if (dialogResult == DialogResult.No)
              {
                service.InvokeAction(-1, (Action) (() => ExceptionHelper.ExceptionService.ShowException(jobStat.Exception)));
                flag = true;
              }
              else
              {
                if (dialogResult == DialogResult.Abort)
                  jobStat = customService.ResumeJob(this.jobID, DeleteObjectsJobMode.AbortOnError);
                if (dialogResult == DialogResult.Ignore)
                  jobStat = customService.ResumeJob(this.jobID, DeleteObjectsJobMode.AscOnError);
                if (dialogResult == DialogResult.Retry)
                  jobStat = customService.ResumeJob(this.jobID, DeleteObjectsJobMode.IgnoreErrors);
              }
            }
            lock (this.lockForm)
              this.jobStatus = jobStat;
          }
          if (jobStat.Progress != DeleteObjectsJobProgress.NotStarted)
          {
            if (jobStat.Progress != DeleteObjectsJobProgress.Working)
              break;
          }
          Thread.Sleep(500);
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
    this.ShowPicture();
    lock (this.lockForm)
    {
      if (this.thread == null || this.jobStatus != null && this.jobStatus.Progress != DeleteObjectsJobProgress.NotStarted && this.jobStatus.Progress != DeleteObjectsJobProgress.Working && this.jobStatus.Progress != DeleteObjectsJobProgress.Idle)
      {
        this.StopThread();
        this.DialogResult = DialogResult.OK;
        return;
      }
      if (this.jobStatus != null)
      {
        this.progressBar.Maximum = Convert.ToInt32(Math.Max(this.items.Count, this.jobStatus.Objects + this.jobStatus.Skipped));
        this.progressBar.Value = this.jobStatus.Objects + this.jobStatus.Skipped;
        this.labelObjectesDeletedCount.Text = this.jobStatus.Objects.ToString();
        this.labelSkippedCount.Text = this.jobStatus.Skipped.ToString();
        this.labelRelationsDeletedCount.Text = this.jobStatus.RelationsCount.ToString();
        this.progressBar.Update();
      }
    }
    this.timerRefresh.Enabled = true;
  }

  /// <summary>Прервать пакетное удаление объектов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.jobID == Guid.Empty)
      return;
    lock (this.lockForm)
    {
      this.StopThread();
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IObjectsDeleteService)) is IObjectsDeleteService customService)
        this.jobStatus = customService.CancelJob(this.jobID);
      this.jobID = Guid.Empty;
      if (e == null)
        return;
      this.DialogResult = DialogResult.Cancel;
    }
  }

  /// <summary>Обновить рисунок с корзинкой</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void timerAnimation_Tick(object sender, EventArgs e) => this.ShowPicture();

  /// <summary>
  /// Показать изображение с корзинкой, пересчитать значение индекса
  /// </summary>
  protected void ShowPicture()
  {
    if (this.currPicture != null)
      this.currPicture.Visible = false;
    ++this.pictureIndex;
    if (this.pictureIndex >= this.pictures.Count)
      this.pictureIndex = 0;
    this.currPicture = this.pictures[this.pictureIndex];
    this.currPicture.Visible = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteProgressForm));
    this.pictureInfo0 = new PictureBox();
    this.labelInfo = new Label();
    this.btnCancel = new Button();
    this.progressBar = new ProgressBar();
    this.timerRefresh = new System.Windows.Forms.Timer(this.components);
    this.pictureInfo1 = new PictureBox();
    this.pictureInfo2 = new PictureBox();
    this.pictureInfo4 = new PictureBox();
    this.pictureInfo3 = new PictureBox();
    this.pictureInfo6 = new PictureBox();
    this.pictureInfo5 = new PictureBox();
    this.timerAnimation = new System.Windows.Forms.Timer(this.components);
    this.statusBar = new StatusStrip();
    this.labelObjectesDeleted = new ToolStripStatusLabel();
    this.labelObjectesDeletedCount = new ToolStripStatusLabel();
    this.labelObjectsSkipped = new ToolStripStatusLabel();
    this.labelSkippedCount = new ToolStripStatusLabel();
    this.labelRelationsDeleted = new ToolStripStatusLabel();
    this.labelRelationsDeletedCount = new ToolStripStatusLabel();
    ((ISupportInitialize) this.pictureInfo0).BeginInit();
    ((ISupportInitialize) this.pictureInfo1).BeginInit();
    ((ISupportInitialize) this.pictureInfo2).BeginInit();
    ((ISupportInitialize) this.pictureInfo4).BeginInit();
    ((ISupportInitialize) this.pictureInfo3).BeginInit();
    ((ISupportInitialize) this.pictureInfo6).BeginInit();
    ((ISupportInitialize) this.pictureInfo5).BeginInit();
    this.statusBar.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pictureInfo0, "pictureInfo0");
    this.pictureInfo0.Name = "pictureInfo0";
    this.pictureInfo0.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelInfo, "labelInfo");
    this.labelInfo.Name = "labelInfo";
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.progressBar, "progressBar");
    this.progressBar.Name = "progressBar";
    this.progressBar.Step = 1;
    this.progressBar.Style = ProgressBarStyle.Continuous;
    this.timerRefresh.Interval = 1000;
    this.timerRefresh.Tick += new EventHandler(this.timerRefresh_Tick);
    componentResourceManager.ApplyResources((object) this.pictureInfo1, "pictureInfo1");
    this.pictureInfo1.Name = "pictureInfo1";
    this.pictureInfo1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureInfo2, "pictureInfo2");
    this.pictureInfo2.Name = "pictureInfo2";
    this.pictureInfo2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureInfo4, "pictureInfo4");
    this.pictureInfo4.Name = "pictureInfo4";
    this.pictureInfo4.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureInfo3, "pictureInfo3");
    this.pictureInfo3.Name = "pictureInfo3";
    this.pictureInfo3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureInfo6, "pictureInfo6");
    this.pictureInfo6.Name = "pictureInfo6";
    this.pictureInfo6.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureInfo5, "pictureInfo5");
    this.pictureInfo5.Name = "pictureInfo5";
    this.pictureInfo5.TabStop = false;
    this.timerAnimation.Interval = 777;
    this.timerAnimation.Tick += new EventHandler(this.timerAnimation_Tick);
    this.statusBar.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.labelObjectesDeleted,
      (ToolStripItem) this.labelObjectesDeletedCount,
      (ToolStripItem) this.labelObjectsSkipped,
      (ToolStripItem) this.labelSkippedCount,
      (ToolStripItem) this.labelRelationsDeleted,
      (ToolStripItem) this.labelRelationsDeletedCount
    });
    componentResourceManager.ApplyResources((object) this.statusBar, "statusBar");
    this.statusBar.Name = "statusBar";
    this.statusBar.SizingGrip = false;
    this.labelObjectesDeleted.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.labelObjectesDeleted.Name = "labelObjectesDeleted";
    componentResourceManager.ApplyResources((object) this.labelObjectesDeleted, "labelObjectesDeleted");
    this.labelObjectesDeletedCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.labelObjectesDeletedCount.Name = "labelObjectesDeletedCount";
    componentResourceManager.ApplyResources((object) this.labelObjectesDeletedCount, "labelObjectesDeletedCount");
    this.labelObjectsSkipped.BorderSides = ToolStripStatusLabelBorderSides.Left;
    this.labelObjectsSkipped.Name = "labelObjectsSkipped";
    componentResourceManager.ApplyResources((object) this.labelObjectsSkipped, "labelObjectsSkipped");
    this.labelSkippedCount.Name = "labelSkippedCount";
    componentResourceManager.ApplyResources((object) this.labelSkippedCount, "labelSkippedCount");
    this.labelRelationsDeleted.BorderSides = ToolStripStatusLabelBorderSides.Left;
    this.labelRelationsDeleted.Name = "labelRelationsDeleted";
    componentResourceManager.ApplyResources((object) this.labelRelationsDeleted, "labelRelationsDeleted");
    this.labelRelationsDeletedCount.Name = "labelRelationsDeletedCount";
    componentResourceManager.ApplyResources((object) this.labelRelationsDeletedCount, "labelRelationsDeletedCount");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.statusBar);
    this.Controls.Add((Control) this.pictureInfo6);
    this.Controls.Add((Control) this.pictureInfo5);
    this.Controls.Add((Control) this.pictureInfo4);
    this.Controls.Add((Control) this.pictureInfo3);
    this.Controls.Add((Control) this.pictureInfo2);
    this.Controls.Add((Control) this.pictureInfo1);
    this.Controls.Add((Control) this.progressBar);
    this.Controls.Add((Control) this.pictureInfo0);
    this.Controls.Add((Control) this.labelInfo);
    this.Controls.Add((Control) this.btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DeleteProgressForm);
    this.FormClosing += new FormClosingEventHandler(this.DeleteProgressForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.DeleteProgressForm_FormClosed);
    this.Load += new EventHandler(this.DeleteProgressForm_Load);
    ((ISupportInitialize) this.pictureInfo0).EndInit();
    ((ISupportInitialize) this.pictureInfo1).EndInit();
    ((ISupportInitialize) this.pictureInfo2).EndInit();
    ((ISupportInitialize) this.pictureInfo4).EndInit();
    ((ISupportInitialize) this.pictureInfo3).EndInit();
    ((ISupportInitialize) this.pictureInfo6).EndInit();
    ((ISupportInitialize) this.pictureInfo5).EndInit();
    this.statusBar.ResumeLayout(false);
    this.statusBar.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
