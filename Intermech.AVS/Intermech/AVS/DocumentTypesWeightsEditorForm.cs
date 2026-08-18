// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsEditorForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Редактор "весов" типов документов</summary>
public class DocumentTypesWeightsEditorForm : Form
{
  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по умолчанию)
  /// 1 - на закладке "Навигатора"
  /// </summary>
  public int ParentMode;
  /// <summary>
  /// Для особых случаев надо запретить и спрятать кнопки "Применить" и "Отмена"
  /// </summary>
  public bool HideApplyCancel;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService notificationSvc;
  /// <summary>
  /// Флажок будет установлен в true, если пользователь возьмёт объект на редактирование нажатием кнопки
  /// </summary>
  public bool AutoCheckedOut;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private DocumentTypesWeightsEditor editor;
  private ImageList imageList;
  protected Panel panelInfo;
  private TextBox textInfo;
  private Button btnCheckOut;
  private PictureBox pictureInfo;

  /// <summary>Конструктор</summary>
  public DocumentTypesWeightsEditorForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1519);
  }

  /// <summary>Конструктор</summary>
  /// <param name="items">Редактируемая коллекция типов документов</param>
  /// <param name="parentMode">Где размещена наша форма:
  /// 0 - самостоятельная форма (по умолчанию),
  /// 1 - на закладке "Навигатора"</param>
  public DocumentTypesWeightsEditorForm(DocumentTypeWeightCollection items, int parentMode)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1519);
    if (this.IsDesignerHosted())
      return;
    this.Init(items, parentMode);
  }

  /// <summary>
  /// Свойство позволяет узнать, можно ли выполнять редактирование "веса" в списке типов объектов-документов
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, можно ли выполнять редактирование \"веса\" в списке типов объектов-документов")]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.editor.ReadOnly;
    [DebuggerStepThrough] set
    {
      this.editor.ReadOnly = value;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Свойство позволяет узнать, были ли изменения в редактируемой коллекции
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, были ли изменения в редактируемой коллекции")]
  public virtual bool IsChanged
  {
    [DebuggerStepThrough] get => this.editor.IsChanged;
    [DebuggerStepThrough] set
    {
      this.editor.IsChanged = value;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Коллекция типов объектов-документов, которая редактируется в данном элементе управления
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Коллекция типов объектов-документов, которая редактируется в данном элементе управления")]
  public virtual DocumentTypeWeightCollection Items
  {
    [DebuggerStepThrough] get => this.editor.Items;
    set => this.Init(value, this.ParentMode);
  }

  /// <summary>
  /// Событие возникает, если в редакторе "весов" типов объектов происходят изменения
  /// </summary>
  [Description("Событие возникает, если в редакторе \"весов\" типов объектов происходят изменения")]
  public event DocumentTypesWeightsChangedEventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    DocumentTypesWeightsChangedEventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, new DocumentTypesWeightsEventArgs(this.editor.Items));
  }

  /// <summary>
  /// Событие возникает, если в редакторе "весов" типов объектов нажата кнопка "Применить"
  /// </summary>
  [Description("Событие возникает, если в редакторе \"весов\" типов объектов нажата кнопка \"Применить\"")]
  public event DocumentTypesWeightsChangedEventHandler OnApplyPressed;

  /// <summary>Сгенерировать событие "OnApplyPressed"</summary>
  protected virtual void RaiseOnApplyPressed()
  {
    DocumentTypesWeightsChangedEventHandler onApplyPressed = this.OnApplyPressed;
    if (onApplyPressed == null)
      return;
    onApplyPressed((object) this, new DocumentTypesWeightsEventArgs(this.editor.Items));
  }

  /// <summary>
  /// Событие возникает, если в редакторе "весов" типов объектов нажата кнопка "Отменить"
  /// </summary>
  [Description("Событие возникает, если в редакторе \"весов\" типов объектов нажата кнопка \"Отменить\"")]
  public event DocumentTypesWeightsChangedEventHandler OnCancelPressed;

  /// <summary>Сгенерировать событие "OnCancelPressed"</summary>
  protected virtual void RaiseOnCancelPressed()
  {
    DocumentTypesWeightsChangedEventHandler onCancelPressed = this.OnCancelPressed;
    if (onCancelPressed == null)
      return;
    onCancelPressed((object) this, new DocumentTypesWeightsEventArgs(this.editor.Items));
  }

  /// <summary>
  /// Вызвать редактор "весов" для типов объектов-документов
  /// </summary>
  /// <param name="items">Коллекция типов объектов-документов</param>
  /// <returns>Результаты вызова формы</returns>
  public static DialogResult Execute(DocumentTypeWeightCollection items)
  {
    if (items == null)
      return DialogResult.Cancel;
    using (DocumentTypesWeightsEditorForm weightsEditorForm = new DocumentTypesWeightsEditorForm(items, 0))
    {
      DialogResult dialogResult = weightsEditorForm.ShowDialog();
      if (dialogResult != DialogResult.OK)
      {
        if (weightsEditorForm.AutoCheckedOut)
          weightsEditorForm.CancelChangesMainSpecTemplatePressed();
        return dialogResult;
      }
      items.Assign(weightsEditorForm.Items);
      if (weightsEditorForm.AutoCheckedOut)
        weightsEditorForm.CheckInMainSpecTemplatePressed();
      return dialogResult;
    }
  }

  /// <summary>
  /// Вызвать редактор "весов" для редактирования системной коллекции "весов" типов объектов-документов
  /// </summary>
  /// <returns>Результаты вызова формы</returns>
  public static DialogResult EditSystemCollection()
  {
    if (DocumentTypeWeightHelper.items == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        DocumentTypeWeightHelper.LoadSystemCollection(sessionKeeper.Session);
    }
    using (DocumentTypesWeightsEditorForm weightsEditorForm = new DocumentTypesWeightsEditorForm(DocumentTypeWeightHelper.items, 0))
    {
      DialogResult dialogResult = weightsEditorForm.ShowDialog();
      if (dialogResult != DialogResult.OK)
      {
        if (weightsEditorForm.AutoCheckedOut)
          weightsEditorForm.CancelChangesMainSpecTemplatePressed();
        return dialogResult;
      }
      DocumentTypeWeightHelper.items.Assign(weightsEditorForm.Items);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        DocumentTypeWeightHelper.SaveSystemCollection(sessionKeeper.Session);
      if (weightsEditorForm.AutoCheckedOut)
        weightsEditorForm.CheckInMainSpecTemplatePressed();
      return dialogResult;
    }
  }

  /// <summary>Инициализировать форму</summary>
  /// <param name="items">Редактируемая коллекция типов документов</param>
  /// <param name="parentMode">Где размещена наша форма:
  /// 0 - самостоятельная форма (по умолчанию),
  /// 1 - на закладке "Навигатора"</param>
  public void Init(DocumentTypeWeightCollection items, int parentMode)
  {
    if (parentMode == 0)
    {
      Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
      this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
      this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
      FormStorage.LoadLayout((Control) this);
    }
    this.editor.Init(items);
    this.ParentMode = parentMode;
    this.btnApply.DialogResult = this.ParentMode == 0 ? DialogResult.OK : DialogResult.None;
    this.btnCancel.DialogResult = this.ParentMode == 0 ? DialogResult.Cancel : DialogResult.None;
    this.CancelButton = this.ParentMode == 0 ? (IButtonControl) this.btnCancel : (IButtonControl) null;
    switch (this.ParentMode)
    {
      case 0:
        this.btnApply.Text = "ОК";
        break;
      case 1:
        this.btnApply.Text = "Применить";
        break;
    }
    this.UpdateControls();
    this.notificationSvc = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"), false);
      IDBAttribute attributeByGuid = dbObject?.GetAttributeByGuid(new Guid("cad00292-306c-11d8-b4e9-00304f19f545"));
      this.editor.ReadOnly = dbObject.ObjectModifyMode != ObjectModifyModes.InBase && (dbObject.ObjectModifyMode != ObjectModifyModes.Checkout || dbObject.CheckoutBy != sessionKeeper.Session.UserID) || attributeByGuid == null || attributeByGuid.ReadOnly;
      if (!this.editor.ReadOnly)
        return;
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy != sessionKeeper.Session.UserID && dbObject.CheckoutBy != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(dbObject.CheckoutBy);
        this.textInfo.Text = $"Сортировать документы по типам нельзя. Объект \"{dbObject.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".";
        this.textInfo.SelectionLength = 0;
      }
      else
      {
        this.textInfo.Text = $"Сортировать документы по типам нельзя. Возможно, следует взять объект \"{dbObject.Caption}\" на редактирование.";
        this.textInfo.SelectionLength = 0;
      }
    }
  }

  /// <summary>Форму закрывают</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DocumentTypesWeightsEditorForm_FormClosed(
    object sender,
    FormClosedEventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Обновить контролы в форме</summary>
  protected virtual void UpdateControls()
  {
    this.btnApply.Enabled = !this.editor.ReadOnly && this.editor.IsChanged;
    this.btnCancel.Enabled = !this.editor.ReadOnly && this.editor.IsChanged || this.ParentMode == 0;
    this.panelBottom.Visible = !this.HideApplyCancel;
    this.panelInfo.Visible = this.editor.ReadOnly;
    this.btnCheckOut.Enabled = this.editor.ReadOnly;
  }

  /// <summary>Корректно назначить контрол-предок для формы</summary>
  /// <param name="aParent">Родительский элемент управления</param>
  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
    this.UpdateControls();
  }

  /// <summary>Возможно, есть изменения в редакторе "весов"</summary>
  /// <param name="sender">Редактор весов</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnChanged(object sender, DocumentTypesWeightsEventArgs e)
  {
    this.UpdateControls();
    this.RaiseOnChanged();
  }

  /// <summary>Нажата кнопка "Применить"/"ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoApplyOK(object sender, EventArgs e)
  {
    if (this.ParentMode == 0)
    {
      if (this.ReadOnly)
        return;
      this.DialogResult = DialogResult.OK;
    }
    else
      this.RaiseOnApplyPressed();
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e)
  {
    if (this.ParentMode == 0)
      this.DialogResult = DialogResult.Cancel;
    else
      this.RaiseOnCancelPressed();
  }

  /// <summary>
  /// Взять на изменение объект "Основной шаблон спецификаций"
  /// </summary>
  private void btnCheckOut_Click(object sender, EventArgs e)
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"));
      IDBAttribute attributeById = dbObject.GetAttributeByID(DocumentTypeWeightHelper.attrDocumentTypesWeights);
      if ((dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == sessionKeeper.Session.UserID ? (attributeById == null ? 0 : (!attributeById.ReadOnly ? 1 : 0)) : 0) == 0 && dbObject.CheckoutBy != sessionKeeper.Session.UserID && dbObject.CheckoutBy != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(dbObject.CheckoutBy);
        int num = (int) MessageBox.Show($"Сортировать документы по типам нельзя. Объект \"{dbObject.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        long objectId1 = dbObject.ObjectID;
        long objectId2 = dbObject.CheckOut(true).ObjectID;
        List<long> objectIDs = new List<long>(1);
        List<long> newObjectIDs = new List<long>(1);
        objectIDs.Add(objectId1);
        newObjectIDs.Add(objectId2);
        this.AutoCheckedOut = true;
        this.editor.ReadOnly = false;
        this.UpdateControls();
        if (objectId1.IsUndefinedId())
          return;
        this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
      }
    }
  }

  /// <summary>
  /// Завершить изменения в объекте "Основной шаблон спецификаций"
  /// </summary>
  protected virtual void CheckInMainSpecTemplatePressed()
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"));
      long num = 0;
      if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
      {
        num = dbObject.ObjectID;
        dbObject.CheckIn();
      }
      this.AutoCheckedOut = false;
      this.editor.ReadOnly = true;
      this.UpdateControls();
      if (num.IsUndefinedId())
        return;
      this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", num));
    }
  }

  /// <summary>
  /// Отменить изменения в объекте "Основной шаблон спецификаций"
  /// </summary>
  protected virtual void CancelChangesMainSpecTemplatePressed()
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"));
      long num = 0;
      if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
      {
        num = dbObject.ObjectID;
        dbObject.CancelChanges();
      }
      this.AutoCheckedOut = false;
      this.editor.ReadOnly = true;
      this.UpdateControls();
      if (num.IsUndefinedId())
        return;
      this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", num));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentTypesWeightsEditorForm));
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.editor = new DocumentTypesWeightsEditor();
    this.imageList = new ImageList(this.components);
    this.panelInfo = new Panel();
    this.textInfo = new TextBox();
    this.btnCheckOut = new Button();
    this.pictureInfo = new PictureBox();
    this.panelBottom.SuspendLayout();
    this.panelInfo.SuspendLayout();
    ((ISupportInitialize) this.pictureInfo).BeginInit();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.DoApplyOK);
    componentResourceManager.ApplyResources((object) this.editor, "editor");
    this.editor.IsChanged = false;
    this.editor.Name = "editor";
    this.editor.ReadOnly = true;
    this.editor.OnChanged += new DocumentTypesWeightsChangedEventHandler(this.editor_OnChanged);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "warning.png");
    this.panelInfo.BackColor = SystemColors.Info;
    this.panelInfo.BorderStyle = BorderStyle.Fixed3D;
    this.panelInfo.Controls.Add((Control) this.textInfo);
    this.panelInfo.Controls.Add((Control) this.btnCheckOut);
    this.panelInfo.Controls.Add((Control) this.pictureInfo);
    componentResourceManager.ApplyResources((object) this.panelInfo, "panelInfo");
    this.panelInfo.ForeColor = SystemColors.InfoText;
    this.panelInfo.Name = "panelInfo";
    componentResourceManager.ApplyResources((object) this.textInfo, "textInfo");
    this.textInfo.BackColor = SystemColors.Info;
    this.textInfo.ForeColor = SystemColors.InfoText;
    this.textInfo.Name = "textInfo";
    this.textInfo.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnCheckOut, "btnCheckOut");
    this.btnCheckOut.Cursor = Cursors.Default;
    this.btnCheckOut.Name = "btnCheckOut";
    this.btnCheckOut.Click += new EventHandler(this.btnCheckOut_Click);
    this.pictureInfo.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.pictureInfo, "pictureInfo");
    this.pictureInfo.Name = "pictureInfo";
    this.pictureInfo.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.editor);
    this.Controls.Add((Control) this.panelInfo);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DocumentTypesWeightsEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.DocumentTypesWeightsEditorForm_FormClosed);
    this.panelBottom.ResumeLayout(false);
    this.panelInfo.ResumeLayout(false);
    this.panelInfo.PerformLayout();
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.ResumeLayout(false);
  }
}
