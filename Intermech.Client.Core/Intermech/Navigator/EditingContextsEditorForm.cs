
// Type: Intermech.Navigator.EditingContextsEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Форма, позволяющая изменять содержимое контекста редактирования
/// </summary>
public class EditingContextsEditorForm : Form
{
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _objtypesIcons;
  /// <summary>Информация о текущем пользователе</summary>
  private ICurrentUserAndRole _currUser;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private EditingContextsEditor editor;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnOK;

  /// <summary>Конструктор</summary>
  public EditingContextsEditorForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 772);
    if (this.DesignMode)
      return;
    this.Init();
  }

  /// <summary>Показывать связанные контексты</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool ShowLinkedContexts
  {
    [DebuggerStepThrough] get => this.editor.ShowLinkedContexts;
    set => this.editor.ShowLinkedContexts = value;
  }

  /// <summary>Права доступа к текущему контексту</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual EditingContextsAccessRights AccessRights
  {
    [DebuggerStepThrough] get => this.editor.AccessRights;
  }

  /// <summary>
  /// Идентификатор версии объекта текущего контекста редактирования
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual long CurrentContextObjectID
  {
    get => this.editor.CurrentContextObjectID;
    set => this.editor.CurrentContextObjectID = value;
  }

  /// <summary>Текущий контест редактирования</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual EditingContextsObjectContainer Context
  {
    get => this.editor.Context;
    set => this.editor.Context = value;
  }

  /// <summary>Контейнер сервисов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual System.IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.editor.Services;
    set => this.editor.Services = value;
  }

  /// <summary>Метод для инициализации формы</summary>
  protected virtual void Init()
  {
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 60, primaryWorkingArea.Height / 100 * 50);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._currUser = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.UpdateControls();
  }

  /// <summary>Обновить состояние контролов</summary>
  protected virtual void UpdateControls()
  {
    this.btnOK.Enabled = this.editor.IsChanged && this.editor.AccessRights == EditingContextsAccessRights.FullAccess;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Открыть редактор для указанного контекста</summary>
  /// <param name="contextID">Идентификатор версии контекста редактирования</param>
  /// <returns>Результаты работы редактора</returns>
  public static DialogResult Execute(long contextID)
  {
    if (contextID == 0L)
      return DialogResult.Cancel;
    using (EditingContextsEditorForm sender = new EditingContextsEditorForm())
    {
      sender.ShowLinkedContexts = false;
      sender.CurrentContextObjectID = contextID;
      DialogResult dialogResult = sender.ShowDialog();
      if (dialogResult == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService)
            customService.SetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, sender.Context, true);
        }
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) sender, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", contextID));
        ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
        ServicesManager.GetService(typeof (IFiltrationService));
        if (service.EditingContextID == contextID)
          service.EditingContextID = contextID;
      }
      return dialogResult;
    }
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void EditingContextsEditorForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void EditingContextsEditorForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoOK(object sender, EventArgs e)
  {
    if (!this.editor.IsChanged || this.editor.AccessRights != EditingContextsAccessRights.FullAccess)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Изменения в редакторе контекста</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnChanged(object sender, EventArgs e) => this.UpdateControls();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditingContextsEditorForm));
    this.editor = new EditingContextsEditor();
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.editor, "editor");
    this.editor.DisableHeader = false;
    this.editor.IsChanged = false;
    this.editor.MinimumSize = new Size(200, 100);
    this.editor.Name = "editor";
    this.editor.OnChanged += new EditingContextsEditor.EditingContextsChangedEventHandler(this.editor_OnChanged);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnOK);
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Cursor = Cursors.Default;
    this.btnOK.Name = "btnOK";
    this.btnOK.Click += new EventHandler(this.DoOK);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.editor);
    this.Controls.Add((Control) this.panelBottom);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditingContextsEditorForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.EditingContextsEditorForm_FormClosed);
    this.Load += new EventHandler(this.EditingContextsEditorForm_Load);
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
