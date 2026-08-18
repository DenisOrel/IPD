// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowErrorFormWithProcessesViewAndDeleting
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class WorkflowErrorFormWithProcessesViewAndDeleting : Form
{
  private long[] _processesID;
  private long _schemeID = -1;
  private WorkflowMakeBaseVersionException _exception;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _textBox;
  private ObjectsViewBase _objectsViewBase;
  private Button delBtn;
  private Button continueBtn;
  private Button createVersionBtn;

  public WorkflowErrorFormWithProcessesViewAndDeleting(
    string caption,
    string errorText,
    long[] processesID)
  {
    this.InitializeComponent();
    this.Text = caption;
    this._textBox.Text = errorText;
    this._processesID = processesID;
  }

  /// <summary>
  /// Конструктор формы для вывода ошибки редактирования шаблона процесса, чтобы можно было дополнительно показать кнопку по созданию базовой версии шаблона
  /// </summary>
  public WorkflowErrorFormWithProcessesViewAndDeleting(bool createVersion = false)
  {
    this.InitializeComponent();
    this.createVersionBtn.Visible = createVersion;
  }

  private void WorkflowErrorFormWithProcessesViewAndDeleting_Load(object sender, EventArgs e)
  {
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, -1, "Созданные процессы по шаблону", (IList) this._processesID);
    ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.TrashMode | ObjectsSelectionOptions.LocalTypesMode);
    ServiceContainer services = new ServiceContainer((System.IServiceProvider) ApplicationServices.Container);
    services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
    this._objectsViewBase.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) services);
    this._objectsViewBase.PageViewsManager.Services = (System.IServiceProvider) ApplicationServices.Container;
    this._objectsViewBase.Activate((IView) null);
  }

  public WorkflowMakeBaseVersionException Exception
  {
    get => this._exception;
    set
    {
      if (this._exception == value)
        return;
      this._exception = value;
      if (this._exception == null)
        return;
      this.Text = this._exception.Caption;
      this._textBox.Text = this._exception.ErrorText;
      this._processesID = this._exception.ObjectsID;
      this._schemeID = this._exception.SchemeID;
    }
  }

  private void continueBtn_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Удаляем сразу все процессы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void delBtn_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IObjectsDeleteService customService = sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteService)) as IObjectsDeleteService;
      INotificationService service = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
      if (customService != null)
      {
        DeletingObjects items = new DeletingObjects();
        foreach (long objectID in this._processesID)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
          items.Add(0L, dbObject.ID, dbObject.ObjectID, true);
          DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsRemoved", dbObject.ObjectID);
          service?.FireEvent((object) null, (NotificationEventArgs) e1);
        }
        DeleteObjectsJobStatus objectsJobStatus = DeleteProgressForm.Execute(items);
        if (objectsJobStatus.Progress == DeleteObjectsJobProgress.Error)
        {
          if (objectsJobStatus.Exception != null)
            ExceptionHelper.ExceptionService.ShowException(objectsJobStatus.Exception);
        }
      }
    }
    this.Close();
  }

  /// <summary>
  /// Создаем версию шаблона, закрываем текущее окно и открываем созданную версию на редактирование
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void createVersionBtn_Click(object sender, EventArgs e)
  {
    if (this._schemeID == -1L)
      return;
    INotificationService service = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject version = sessionKeeper.Session.GetObjectCollection(wfConsts.SchemesTypeID).CreateVersion(this._schemeID);
      version.CommitCreation(true, false);
      if (service != null)
      {
        DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsCreated", version.ObjectID);
        service.FireEvent((object) null, (NotificationEventArgs) e1);
      }
      this.Close();
      wfFunx.EditProcess(version.ObjectID);
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
    this._textBox = new TextBox();
    this.delBtn = new Button();
    this.continueBtn = new Button();
    this.createVersionBtn = new Button();
    this._objectsViewBase = new ObjectsViewBase();
    this.SuspendLayout();
    this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textBox.Location = new Point(13, 12);
    this._textBox.Multiline = true;
    this._textBox.Name = "_textBox";
    this._textBox.ReadOnly = true;
    this._textBox.Size = new Size(537, 74);
    this._textBox.TabIndex = 3;
    this.delBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.delBtn.Location = new Point(328, 354);
    this.delBtn.Name = "delBtn";
    this.delBtn.Size = new Size(141, 23);
    this.delBtn.TabIndex = 4;
    this.delBtn.Text = "Удалить все процессы";
    this.delBtn.UseVisualStyleBackColor = true;
    this.delBtn.Click += new EventHandler(this.delBtn_Click);
    this.continueBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.continueBtn.DialogResult = DialogResult.Cancel;
    this.continueBtn.Location = new Point(475, 354);
    this.continueBtn.Name = "continueBtn";
    this.continueBtn.Size = new Size(76, 23);
    this.continueBtn.TabIndex = 4;
    this.continueBtn.Text = "Пропустить";
    this.continueBtn.UseVisualStyleBackColor = true;
    this.continueBtn.Click += new EventHandler(this.continueBtn_Click);
    this.createVersionBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.createVersionBtn.Location = new Point(168, 354);
    this.createVersionBtn.Name = "createVersionBtn";
    this.createVersionBtn.Size = new Size(154, 23);
    this.createVersionBtn.TabIndex = 4;
    this.createVersionBtn.Text = "Создать версию шаблона";
    this.createVersionBtn.UseVisualStyleBackColor = true;
    this.createVersionBtn.Visible = false;
    this.createVersionBtn.Click += new EventHandler(this.createVersionBtn_Click);
    this._objectsViewBase.AllowCustomGroupValues = true;
    this._objectsViewBase.AllowEditing = true;
    this._objectsViewBase.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._objectsViewBase.Control = (object) this._objectsViewBase;
    this._objectsViewBase.DisableKeyDownEvents = false;
    this._objectsViewBase.EditingMode = false;
    this._objectsViewBase.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._objectsViewBase.Font = new Font("Tahoma", 8.25f);
    this._objectsViewBase.Location = new Point(13, 93);
    this._objectsViewBase.Name = "_objectsViewBase";
    this._objectsViewBase.Size = new Size(537, (int) byte.MaxValue);
    this._objectsViewBase.TabIndex = 1;
    this._objectsViewBase.ViewContentType = ContentType.NonFolders;
    this._objectsViewBase.Load += new EventHandler(this.WorkflowErrorFormWithProcessesViewAndDeleting_Load);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.continueBtn;
    this.ClientSize = new Size(562, 389);
    this.Controls.Add((Control) this.continueBtn);
    this.Controls.Add((Control) this.createVersionBtn);
    this.Controls.Add((Control) this.delBtn);
    this.Controls.Add((Control) this._textBox);
    this.Controls.Add((Control) this._objectsViewBase);
    this.MinimumSize = new Size(578, 428);
    this.Name = nameof (WorkflowErrorFormWithProcessesViewAndDeleting);
    this.Text = nameof (WorkflowErrorFormWithProcessesViewAndDeleting);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
